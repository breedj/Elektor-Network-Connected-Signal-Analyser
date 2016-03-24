using System;


namespace Elektor.SignalAnalyzer
{
    public class SignalAnalyzer
    {
        

        #region Private variables
                
        private readonly SampleCollector _sampleCollector;        
        private readonly FftAnalyzer _fftAnalyzer;        
        private static readonly object SampleDataLock = new object();
        private ScopeData _scopeData;

        #endregion


        #region Constructors

        public SignalAnalyzer()
        {
            _sampleCollector = new SampleCollector();
            _sampleCollector.DataReady += SampleCollector_DataReady;    // Fires when samples are collected and meets selected trigger settings.

            _fftAnalyzer = new FftAnalyzer();
            _fftAnalyzer.FftReady += FFTAnalyzer_FFTReady; // Fires when fft is ready to be rendered            
        }


        #endregion

        #region Events
               

        /// <summary>
        /// Event which is fired when new scope can be rendered
        /// </summary>
        public event EventHandler<ScopeEventArgs> RenderScope;

        /// <summary>
        /// Fire RenderScope event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderScope(ScopeEventArgs e)
        {
            EventHandler<ScopeEventArgs> handler = RenderScope;
            handler?.Invoke(this, e);
        }

                

        /// <summary>
        /// Event which is fired when fft can be rendered
        /// </summary>
        public event EventHandler<FFTEventArgs> RenderFft;


        /// <summary>
        /// Fire RenderFFT event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderFft(FFTEventArgs e)
        {
            EventHandler<FFTEventArgs> handler = RenderFft;
            handler?.Invoke(this, e);
        }

        #endregion


        #region Public Methods


        /// <summary>
        /// Stop the analyzer
        /// </summary>
        public void Stop()
        {
            AcquireState = AcquireStates.Stopped;
            _sampleCollector.Cancel();
            _fftAnalyzer.Cancel();           
        }



        /// <summary>
        /// Client connection
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
                _sampleCollector.Connection = _connection;  // set connection of sample collector
            }
        }


        public void CleanUp()
        {
            FftAnalyzer.CleanUp(); //Clean up memory map
        }



        /// <summary>
        /// Event handler
        /// Fired when data has been collected.
        /// </summary>
        private void SampleCollector_DataReady(object sender, ScopeEventArgs e)
        {
            lock (SampleDataLock)
            {
                _scopeData = e.ScopeData;
            }

            // Execute FFT form sollected data. Will call FFTAnalyzer_RenderFFT when ready.
            if (_scopeData != null)
                _fftAnalyzer.ExecuteFftAsync(_scopeData.Voltages, SamplingSettings.Fs, FftAverages, WindowType);
            else
                OnRenderFft(new FFTEventArgs(null)); // Scope data is null so render empty fft

            OnRenderScope(new ScopeEventArgs(_scopeData));
        }

        
        /// <summary>
        /// FTT is ready fire Render event
        /// </summary>
        private void FFTAnalyzer_FFTReady(object sender, FFTEventArgs e)
        {
            OnRenderFft(new FFTEventArgs(e.FFTData));
        }

        #endregion


        #region Public Properties


        /// <summary>
        /// Acquire state
        /// Running, Single, Stopped
        /// </summary>
        public AcquireStates AcquireState
        { 
            get 
            {
                return _sampleCollector.AcquireState;
            }
            set
            {
                _sampleCollector.AcquireState = value;
            }
        }


        /// <summary>
        /// Trigger mode
        /// Off, Auto, Normal
        /// </summary>
        public TriggerModes TriggerMode
        {
            get
            {
                return _sampleCollector.TriggerMode;
            }
            set
            {
                _sampleCollector.TriggerMode = value;
            }
        }


        /// <summary>
        /// Trigger slope
        /// Rising of Falling edge
        /// </summary>
        public TriggerSlopes TriggerSlope
        {
            get
            {
                return _sampleCollector.TriggerSlope;
            }
            set
            {
                _sampleCollector.TriggerSlope = value;
            }
        }

        /// <summary>
        /// Voltage level to trigger on
        /// </summary>
        public double TriggerLevel
        {
            get
            {
                return _sampleCollector.TriggerLevel;
            }
            set
            {
                _sampleCollector.TriggerLevel = value;
            }
        }

        /// <summary>
        /// Coupling
        /// AC or DC
        /// </summary>
        public CouplingModes Coupling
        {
            get
            {
                return _sampleCollector.Coupling;
            }
            set
            {
                _sampleCollector.Coupling = value;
            }
        }

        public WindowingTypes WindowType { get; set; } = WindowingTypes.Square;

        public int FftAverages { get; set; } = 1;

       
        /// <summary>
        /// Samples per second
        /// </summary>
        public SamplingSettings SamplingSettings
        {
            get
            {
                return _sampleCollector.SamplingSettings;
            }
            set
            {
                _sampleCollector.SamplingSettings = value;                
            }
        }

        /// <summary>
        /// Sampes to take
        /// </summary>
        public int SamplesCount
        {
            get
            {
                return _sampleCollector.SamplesCount;
            }
            set
            {
                _sampleCollector.SamplesCount = value;
            }
        }


        /// <summary>
        /// ADC amount of bits
        /// </summary>
        public ADCBits ConversionAccuray
        {
            get
            {
                return _sampleCollector.ConversionAccuray;
            }
            set
            {
                _sampleCollector.ConversionAccuray = value;
            }
        }


        /// <summary>
        /// Calculate suggested sample frequency with low jitter based in a desired sample frequency
        /// </summary>
        /// <param name="desiredSampleFrequency">The desired sample frequency</param>
        /// <param name="withPllChange">Allow pll change</param>
        /// <returns></returns>
        public SamplingSettings GetSpurFreeSampleFrequency(int desiredSampleFrequency, bool withPllChange)
        {
            return _sampleCollector.GetSpurFreeSampleFrequency(desiredSampleFrequency, withPllChange);
        }


        /// <summary>
        /// Check if analyzer is performing within spec regarding ADCS
        /// </summary>
        public bool IsADCSInSpec => _sampleCollector.IsADCSInSpec;

        /// <summary>
        /// Check if analyzer is performing within spec regarding sample rate
        /// </summary>
        public bool IsSampleRateInSpec => _sampleCollector.IsSampleRateInSpec;

        /// <summary>
        /// Get of current set ADCS
        /// </summary>
        public byte ActualADCS => _sampleCollector.ActualADCS;

        #endregion
    }
}
