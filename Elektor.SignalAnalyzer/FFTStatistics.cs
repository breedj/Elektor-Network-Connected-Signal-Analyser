using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    public class FFTStatistics
    {

        private readonly object lockObj = new object();

        /// <summary>
        /// Calculate statistics from fft and rms values
        /// </summary>
        /// <param name="samples"></param>
        public void Calculate(double[] fft, double[] rms)
        {                                   
            lock (lockObj)
            {
                if (_maxTraceFFT.Length != fft.Length)
                {
                    _maxTraceFFT = new double[fft.Length];
                    for (int i = 0; i < fft.Count(); i++)
                        _maxTraceFFT[i] = -120;
                }
                if (_maxTraceRMS.Length != rms.Length)
                {
                    _maxTraceRMS = new double[rms.Length];
                    for (int i = 0; i < fft.Count(); i++)
                        _maxTraceRMS[i] = -120;
                }
                for (int i = 0; i < fft.Count(); i++)
                {
                    if (_maxTraceFFT[i] < fft[i])
                        _maxTraceFFT[i] = fft[i];                    
                }
                for (int i = 0; i < rms.Count(); i++)
                {
                    if (_maxTraceRMS[i] < rms[i])
                        _maxTraceRMS[i] = rms[i];                
                }
            }            
        }

        
        static double[] _maxTraceFFT = new double[0];
        public double[] MaxTraceFFT
        {
            get
            {
                lock (lockObj)
                    return _maxTraceFFT;
            }
            set
            {
                lock (lockObj)
                    _maxTraceFFT = value;
            }
        }

        static double[] _maxTraceRMS = new double[0];
        public double[] MaxTraceRMS
        {
            get
            {
                lock (lockObj)
                    return _maxTraceRMS;
            }
            set
            {
                lock (lockObj)
                    _maxTraceRMS = value;
            }
        }


        /// <summary>
        /// Reset averages
        /// </summary>
        public void Reset()
        {
            ResetMaxTrace();
        }

        
        /// <summary>
        /// Reset
        /// </summary>
        public void ResetMaxTrace()
        {
            lock (lockObj)
            {
                for (int i = 0; i < _maxTraceFFT.Length; i++)
                    _maxTraceFFT[i] = -120;
                for (int i = 0; i < _maxTraceRMS.Length; i++)
                    _maxTraceRMS[i] = -120;
            }
        }

    }
}
