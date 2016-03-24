using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elektor.SignalAnalyzer
{    
    public static class NetworkScanner
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        const int Timeout = 1000;
        const int Attempts = 4;
        private static readonly CountdownEvent CountdownEvent = new CountdownEvent(2015);       // Keeps track of running threads
        private static readonly ConcurrentBag<PingResult> PingResults = new ConcurrentBag<PingResult>(); // Holds all pint results
        private static readonly CancellationTokenSource TokenSource = new CancellationTokenSource();

        public static IEnumerable<DeviceNetworkAddress> FindIpAddress(string[] macAdresses, string[] ipAddresses, int port)
        {
            List<DeviceNetworkAddress> result = new List<DeviceNetworkAddress>();

            // Find by mac address on other thread
            Task taskFind = new Task(FindNetworkAnalyzers, TokenSource.Token);
            taskFind.Start();

            // Try to connect to static ip addresses on other thread.
            Task taskConnectStatic = new Task(() =>
            {
                // Try to connect to static ips
                foreach (string ip in ipAddresses)
                {
                    IPAddress ipAddress = IPAddress.Parse(ip);
                    TcpClient client = new TcpClient();
                    IAsyncResult asyncResult = client.BeginConnect(ipAddress, port, null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1)); //timeout if no connection there
                    if (client.Connected)
                    {
                        //string mac = GetMacAddress(ipAddress);    // Static has unknown mac now. 
                        result.Add(new DeviceNetworkAddress { IPAddress = ipAddress, MACAddress = string.Empty});
                    }                    
                }
            }, TokenSource.Token);
            taskConnectStatic.Start();
            
            // Wait for bith tasks to finish
            taskFind.Wait();
            taskConnectStatic.Wait();

            // Merge results with static
            result.AddRange(PingResults
                .Where(p => macAdresses.Contains(p.MacAddress)) // Filter out mac we are interested in.
                .Select(p => new DeviceNetworkAddress { IPAddress = p.IpAddress, MACAddress = p.MacAddress })
                .DistinctBy(p => p.IPAddress));

            return result;
        }

        /// <summary>
        /// Cancel tasks
        /// </summary>
        public static void CancelFind()
        {
            TokenSource.Cancel();
        }

        /// <summary>
        /// Get the MAC address by IP address
        /// </summary>
        /// <param name="ip">Ip address to get MAC address of</param>
        /// <returns></returns>
        public static string GetMacAddress(IPAddress ip)
        {            
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;

            if (SendARP(BitConverter.ToInt32(ip.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                throw new InvalidOperationException("SendARP failed.");

            string[] str = new string[(int)macAddrLen];
            for (int i = 0; i < macAddrLen; i++)
                str[i] = macAddr[i].ToString("x2");

            return string.Join(":", str);
        }
        
        /// <summary>
        /// Get the current gateway address
        /// </summary>
        /// <returns></returns>
        static IPAddress NetworkGateway()
        {
            IPAddress ip = null;

            foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (f.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (GatewayIPAddressInformation d in f.GetIPProperties().GatewayAddresses)
                    {
                        if (d.Address.AddressFamily == AddressFamily.InterNetwork)
                            ip = d.Address;
                    }
                }
            }

            return ip;
        }

        public static void FindNetworkAnalyzers()
        {            
            IPAddress gatewayIpAddress = NetworkGateway();
            //Extracting and pinging all other ip's.
            string[] array = gatewayIpAddress.ToString().Split('.');

            for (int i = 2; i <= 255; i++)
            {
                IPAddress ip = IPAddress.Parse(array[0] + "." + array[1] + "." + array[2] + "." + i);
                Ping(ip, Attempts, Timeout);
            }

            // Wait for workers. Time out after 20s;
            CountdownEvent.Wait(20000);            
        }

        /// <summary>
        /// Ping a host
        /// </summary>
        /// <param name="host">Ip address of host</param>
        /// <param name="attempts">attempst</param>
        /// <param name="timeout">timeout</param>
        private static void Ping(IPAddress host, int attempts, int timeout)
        {
            for (int i = 0; i < attempts; i++)
            {
                new Thread(delegate ()
                {
                    try
                    {
                        Ping ping = new Ping();
                        ping.PingCompleted += PingCompleted;
                        ping.SendAsync(host, timeout, host);
                    }
                    catch
                    {
                        CountdownEvent.Signal(); // Does not fire completed event, so add to signal
                    }
                }).Start();
            }
        }


        private static void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            IPAddress ip = (IPAddress)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                string macaddres = GetMacAddress(ip);
                PingResult result = new PingResult {IpAddress = ip, MacAddress = macaddres};
                if (!PingResults.Contains(result))
                    PingResults.Add(result);
            }
            // Signal the CountdownEvent.
            CountdownEvent.Signal();
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }


    public class PingResult
    {
        public IPAddress IpAddress { get; set; }
        public string MacAddress { get; set; }        
    }
}
