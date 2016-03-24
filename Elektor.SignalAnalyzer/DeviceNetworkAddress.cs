using System.Net;

namespace Elektor.SignalAnalyzer
{
    public class DeviceNetworkAddress
    {
        /// <summary>
        /// Ip address of device
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// Mac Address
        /// </summary>
        public string MACAddress { get; set; }
    }
}