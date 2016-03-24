using FFTWSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Data for scope display
    /// </summary>
    public class ScopeData
    {

        #region Constructors

        public ScopeData()
        {

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// DC Term
        /// </summary>
        public double DCterm { get; set; }

        /// <summary>
        /// DC term in voltage
        /// </summary>
        public double DCVoltage { get; set; }

        /// <summary>
        /// Sampled voltages
        /// </summary>
        public double[] Voltages { get; set; }

        /// <summary>
        /// Time between two samples in seconds
        /// </summary>
        public double SampleInterval { get; set; }

        /// <summary>
        /// Amount of samples taken
        /// </summary>
        public int SampleCount { get; set; }

        /// <summary>
        /// Sample numbers where trigger matches
        /// </summary>
        public List<int> TriggerSamples { get; set; }

        /// <summary>
        /// The base frequency calculated based on the triggerpoints
        /// </summary>
        public double? BaseFrequency { get; set; }

        /// <summary>
        /// Has found a trigger point
        /// </summary>
        public bool Triggered { get; set; }

        #endregion


        static MeasurementStatistics _statistics = new MeasurementStatistics();
        /// <summary>
        /// Measurment statistics
        /// </summary>
        public MeasurementStatistics Statistics
        {
            get
            {
                return _statistics;
            }
            private set
            {
                _statistics = value;
            }
        }

        /// <summary>
        /// Clear averages
        /// </summary>
        public static void ResetAverages()
        {
            _statistics.Reset();
        }


        /// <summary>
        /// Clear min trace
        /// </summary>
        public static void ResetMinTrace()
        {
            _statistics.ResetMinTrace();
        }

        /// <summary>
        /// Clear max trace
        /// </summary>
        public static void ResetMaxTrace()
        {
            _statistics.ResetMaxTrace();
        }
    }
}
