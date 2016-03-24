using System;
using System.Net;
using System.Net.Sockets;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Client connection funcationality
    /// TODO: Add autodetect functionality
    /// </summary>
    public class ClientConnection
    {
        #region Private variables

        private TcpClient _client;
        private string _ipAddress;
        private int _port;

        #endregion


        #region Public Methods

        /// <summary>
        /// Connect to client
        /// </summary>
        public void Connect(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }

            _client = new TcpClient();
            var result = _client.BeginConnect(IPAddress.Parse(_ipAddress), _port, null, null);
            result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));        //timeout if no connection there                        
        }

        /// <summary>
        /// Disconnect from client
        /// </summary>
        public void Disconnect()
        {
            if (_client != null)
            {
                _client.Close();                                
                _client = null;
            }
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// Client connection state
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (_client != null && _client.Connected);
            }
        }
        
        /// <summary>
        /// Tcp client
        /// </summary>
        public TcpClient TcpClient
        {
            get 
            { 
                return _client; 
            }
        }

        #endregion
    }
}
