using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Event arguments used for FFT events
    /// </summary>
    public class FFTEventArgs : EventArgs
    {
        public FFTEventArgs(FFTData fftData)
        {
            _scopeData = fftData;
        }
        private FFTData _scopeData;

        public FFTData FFTData
        {
            get { return _scopeData; }
            set { _scopeData = value; }
        }
    }
    
}
