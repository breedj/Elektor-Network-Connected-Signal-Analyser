using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    public class MeasurementStatistics
    {

        private const int MaxAverageCount = 1000;
        private readonly object lockObj = new object();

        /// <summary>
        /// Calculate statistics from samples
        /// </summary>
        /// <param name="samples"></param>
        public void Calculate(double[] samples, int? firstTriggerPoint, bool triggerEnabled)
        {
            
            Max = samples.Max();
            Min = samples.Min();
            Average = samples.Average();
            PeekToPeek = Math.Abs(Max - Min);

            int startValue = 0;
            bool doCalculate = true;
            if (triggerEnabled)
            {
                // When triggering enabled then only calculate min max when we have a trigger point.
                doCalculate = firstTriggerPoint.HasValue;
                if (doCalculate)
                    startValue = firstTriggerPoint.Value;                
            }                        

            if (doCalculate)
            {
                lock (lockObj)
                {
                    if (_minTrace.Length != samples.Length)
                        _minTrace = new double[samples.Length];
                    if (_maxTrace.Length != samples.Length)
                        _maxTrace = new double[samples.Length];
                    int p = 0;
                    for (int i = startValue; i < samples.Length; i++)
                    {
                        if (_minTrace[p] > samples[i])
                            _minTrace[p] = samples[i];
                        if (_maxTrace[p] < samples[i])
                            _maxTrace[p] = samples[i];
                        p++;
                    }
                }
            }                        
        }


        static double _max;
        public double Max
        {
            get
            {
                lock (lockObj)
                    return _max;
            }
            set
            {
                lock (lockObj)
                {
                    _max = value;
                    _avgMax.Add(value);
                    if (_avgMax.Count > MaxAverageCount)
                        _avgMax.RemoveAt(0);
                }
            }
        }

        static double _min;
        public double Min
        {
            get
            {
                lock (lockObj)
                   return _min;
            }
            set
            {
                lock (lockObj)
                {
                    _min = value;
                    _avgMin.Add(value);
                    if (_avgMin.Count > MaxAverageCount)
                        _avgMin.RemoveAt(0);
                }
            }
        }

        static double _average;
        public double Average
        {
            get
            {
                lock (lockObj)
                    return _average;
            }
            set
            {
                lock (lockObj)
                {
                    _average = value;
                    _avgAverage.Add(value);
                    if (_avgAverage.Count > MaxAverageCount)
                        _avgAverage.RemoveAt(0);
                }
            }
        }

        static double _peekToPeek;
        public double PeekToPeek
        {
            get
            {
                lock (lockObj)
                    return _peekToPeek;
            }
            set
            {
                lock (lockObj)
                {
                    _peekToPeek = value;
                    _avgPeekToPeek.Add(value);
                    if (_avgPeekToPeek.Count > MaxAverageCount)
                        _avgPeekToPeek.RemoveAt(0);
                }
            }
        }
        

        static double[] _minTrace = new double[0];
        public double[] MinTrace
        {
            get
            {
                lock (lockObj)
                    return _minTrace;
            }
            set
            {
                lock (lockObj)
                    _minTrace = value;
            }
        }


        static double[] _maxTrace = new double[0];
        public double[] MaxTrace
        {
            get
            {
                lock (lockObj)
                    return _maxTrace;
            }
            set
            {
                lock (lockObj)
                    _maxTrace = value;
            }
        }


        /// <summary>
        /// Amount of samples taken for averages
        /// </summary>
        public int AvgCount
        {
            get
            {
                lock (lockObj)
                    return _avgMax.Count;
            }
            private set { }
        }

        private static List<double> _avgMax = new List<double>();

        public double AvgMax
        {
            get
            {
                lock (lockObj)
                    return _avgMax.Any() ? _avgMax.Average() : 0;
            }
        }

        private static List<double> _avgMin = new List<double>();
        public double AvgMin
        {
            get
            {
                lock (lockObj)
                    return _avgMin.Any() ? _avgMin.Average() : 0;
            }
        }

        private static List<double> _avgAverage = new List<double>();
        public double AvgAverage
        {
            get
            {
                lock (lockObj)
                    return _avgAverage.Any() ? _avgAverage.Average() : 0;
            }
        }

        private static List<double> _avgPeekToPeek = new List<double>();
        public double AvgPeekToPeek
        {
            get
            {
                lock (lockObj)
                    return _avgPeekToPeek.Any() ? _avgPeekToPeek.Average() : 0;
            }
        }

      
      
        /// <summary>
        /// Reset averages
        /// </summary>
        public void Reset()
        {
            lock (lockObj)
            {
                _avgMin.Clear();
                _avgMax.Clear();
                _avgPeekToPeek.Clear();
                _avgAverage.Clear();                               
            }
            ResetMinTrace();
            ResetMaxTrace();
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void ResetMinTrace()
        {
            lock (lockObj)
            {
                for (int i = 0; i < _minTrace.Length; i++)
                    _minTrace[i] = 10;                
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void ResetMaxTrace()
        {
            lock (lockObj)
            {
                for (int i = 0; i < _maxTrace.Length; i++)
                    _maxTrace[i] = -10;
            }
        }

    }
}
