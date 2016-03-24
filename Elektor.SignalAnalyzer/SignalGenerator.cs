using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// To control the hardware signal generator
    /// </summary>
    public class SignalGenerator
    {
        
        #region Public Properties

        /// <summary>
        /// Connection
        /// </summary>
        private ClientConnection _connection;
        public ClientConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        /// <summary>
        /// Signal generator enabled
        /// </summary>
        private bool _enabled;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                SetGenerator();
            }
        }

        /// <summary>
        /// Selected frequency
        /// </summary>
        private int _frequency;
        public int Frequency
        { 
            get
            {
                return _frequency;
            }
            set
            {
                _frequency = value;
                SetGenerator();                
            }
        }

        /// <summary>
        /// Selected waveform
        /// </summary>
        private GeneratorWaveforms _waveform = GeneratorWaveforms.Sinusoid;
        public GeneratorWaveforms Waveform
        {
            get
            {
                return _waveform;
            }
            set
            {
                _waveform = value;
                SetGenerator();
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Send command to the signal generator
        /// </summary>
        private void SetGenerator()
        {
            if (_connection == null || !_connection.IsConnected)
                return;

            NetworkStream clientStream = _connection.TcpClient.GetStream();
            byte[] txBuff = new byte[128];
            if (_enabled)
            {                
                if (_waveform == GeneratorWaveforms.Noise)
                    FillTxBuff(4, 0, _frequency, 0, 0, txBuff); // 2 is command to turn generator on; fill transmit buffer with command and parameter values                        
                else
                    FillTxBuff(2, 0, _frequency, 0, (byte)_waveform, txBuff); // 2 is command to turn generator on; fill transmit buffer with command and parameter values                                        
            }
            else
                FillTxBuff(3, 0, _frequency, 0, 0, txBuff);  // 3 is command to turn generator off; fill transmit buffer with command and parameter values            
            clientStream.Write(txBuff, 0, 8);       //send command and parameters to uP
            clientStream.Flush();
        }
        

        /// <summary>
        /// Fill transmit buffer
        /// </summary>
        /// <param name="command"></param>
        /// <param name="n"></param>
        /// <param name="Fs"></param>
        /// <param name="AD12B"></param>
        /// <param name="ADCS"></param>
        /// <param name="TxBuff"></param>
        static void FillTxBuff(byte command, int n, double Fs, byte AD12B, byte ADCS, byte[] TxBuff)
        {
            TxBuff[0] = command;
            TxBuff[1] = (byte)(0x000000FF & n);                    //N value to uP; also used when setting generator frequency
            TxBuff[2] = (byte)(0x000000FF & (n >> 8));
            TxBuff[3] = (byte)((0x000000FF & (int)Fs));			//Fs  or GeneratorFreq to uP
            TxBuff[4] = (byte)((0x0000FF00 & (int)Fs) >> 8);
            TxBuff[5] = (byte)((0x00FF0000 & (int)Fs) >> 16);
            TxBuff[6] = AD12B;
            TxBuff[7] = ADCS;            
        }

        #endregion

    }
}
