using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace Elektor.SignalAnalyzer
{
    /// <summary>
    /// Data handling
    /// </summary>
    public class SampleCollector
    {
        #region Private Variables

        private ADCBits _conversionAccuracy = ADCBits.Bit12;
        private static AcquireStates _acquireState = AcquireStates.Stopped;

        private static readonly object stateLock = new object();    // Locking object
        private BackgroundWorker _worker;                           // Background worker (does not mean it runs at lower priority)
        private static ClientConnection _connection;                // Connection object
        private ScopeDataAnalyzer _scopeDataAnalyzer;               // Scope data analyzer object
        private Stopwatch _triggerTimeout;                          // Used in Auto mode
        private bool _triggeredLastTime;                            // Used in Normal mode
        private bool _settingsChanged;                      // Indicate new settings have to be set
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        static int _sampleCount = 5042;   //N FFT size        
        private static SamplingSettings _samplingSettings = new SamplingSettings {Fs = 504201, M = 60, N1 = 2, N2 = 2, ADCS = 6};
        static byte AD12B = 1, ADCS = 6;    //inits to 12bit
        static int _scaleFactor = 4095;

        #endregion

        #region Events 

        /// <summary>
        /// Event which is fired when data has been collected
        /// </summary>        
        public event EventHandler<ScopeEventArgs> DataReady;

        /// <summary>
        /// Fire DataReady event
        /// </summary>
        /// <param name="e">arguments</param>
        private void OnDataReady(ScopeEventArgs e)
        {
            EventHandler<ScopeEventArgs> handler = DataReady;
            handler?.Invoke(this, e);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public SampleCollector()
        {
            _scopeDataAnalyzer = new ScopeDataAnalyzer();
            // Create background worker
            CreateWorker();

            _triggerTimeout = new Stopwatch();
        }

        private void CreateWorker()
        {
            if (_worker != null)
            {
                _worker.CancelAsync();
                _worker.Dispose();
                _worker = null;
            }
            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        

        #endregion


        #region Public Methods


        /// <summary>
        /// Cancel the worker thread
        /// </summary>
        public void Cancel()
        {
            if (_worker.IsBusy)
            {
                _worker.CancelAsync(); // Cancel background thread                                        
                _resetEvent.WaitOne(); // will block until _resetEvent.Set() call made
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
            //if (withPllChange)   
            //    return GetSpurFreeSampleFrequency_WithPllRecomendation(desiredSampleFrequency);

            return GetSpurFreeSampleFrequency_Simple(desiredSampleFrequency);
        }

        /// <summary>
        /// Calculate suggested sample frequency with low jitter based in a desired sample frequency
        /// Based on the curren settings
        /// </summary>
        /// <param name="desiredSampleFrequency">The desired sample frequency</param>
        /// <returns></returns>
        private SamplingSettings GetSpurFreeSampleFrequency_Simple(int desiredSampleFrequency)
        {
            double actualFs;
            int K;
            if (ConversionAccuray == ADCBits.Bit12)
            {
                K = (int)(60000000 / desiredSampleFrequency) / (ADCS + 1) - 17;    //Fcys of uP/ Fs requested * (ADCS +1); gets rid of decimal part as well
                actualFs = (60000000.0 / (17 + K)) / (ADCS + 1);
            }
            else
            {
                K = (int)(60000000 / desiredSampleFrequency) / (ADCS + 1) - 14;    //Fcys of uP/ Fs requested * (ADCS +1); gets rid of decimal part as well
                if (K == -14)
                    actualFs = desiredSampleFrequency;
                else
                    actualFs = (60000000.0 / (14 + K)) / (ADCS + 1);
            }
            return new SamplingSettings { Fs = (int)actualFs, M = 60, N1 = 2, N2 = 2, Fcpu = 60000000 };
        }


        /// <summary>
        /// Calculate suggested sample frequency with low jitter based in a desired sample frequency
        /// PLL change is also suggested
        /// Based on the current settings
        /// </summary>
        /// <param name="desiredSampleFrequency">The desired sample frequency</param>
        /// <returns></returns>
        private SamplingSettings GetSpurFreeSampleFrequency_WithPllRecomendation(int desiredSampleFrequency)
        {
            int bestM = 0;
            byte bestN1 = 0, N2 = 2;  // Set best N2 = 2 for now.
            int minimumFClock = 50000000;    // Minimum cpu clock
            int wantedFcyc, actualFs = 1;
            int k, lowDelta = 100000;
            
            if (AD12B == 1)
            {
                k = (int)(60000000.0 / desiredSampleFrequency) / (ADCS + 1) - 17;
                //find K that gets youu close to Fcyc = 60 MHz; K is integer round down
                wantedFcyc = (17 + k) * (ADCS + 1) * desiredSampleFrequency;
                //desired Fcyc using K calculated above
            }
            else
            {
                k = (int)(60000000.0 / desiredSampleFrequency) / (ADCS + 1) - 14;
                //find K that gets you close to Fcyc = 60 MHz; K is integer round down
                wantedFcyc = (14 + k) * (ADCS + 1) * desiredSampleFrequency;
                //desired Fcyc using K calculated above
            }


            for (int N1 = 2; N1 < 34; N1++)
            {
                for (int M = 3; M < 514; M++)
                {
                    int delta = Math.Abs(M * 2000000 / N1 - wantedFcyc);
                    int actualFcyc = (int)((double)M/(N1*N2)*4000000);
                    //because N2=2 for now
                    if (delta <= lowDelta 
                        && actualFcyc >= minimumFClock)
                    {
                        bestM = M;
                        bestN1 = (byte)N1;
                        lowDelta = delta;
                    }
                }
            }
            if (AD12B == 0)
                actualFs = (2000000 * bestM) / bestN1 / (ADCS + 1) / (14 + k);    //find exact Fs that Best M and N1 will yield
            if (AD12B == 1)
                actualFs = (2000000 * bestM) / bestN1 / (ADCS + 1) / (17 + k);

            if (actualFs > 1000000)
                actualFs = 1000000; // Is maximum

            int cpu = (int) ((double) bestM/(bestN1*N2)*4000000);

            return new SamplingSettings { Fs = actualFs, M = bestM, N1 = bestN1, N2 = N2, Fcpu = cpu};
        }


        #endregion

        #region Public Properties

        /// <summary>
        /// Connection
        /// </summary>
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
        /// The current acquirestate
        /// </summary>
        public AcquireStates AcquireState
        {
            get
            {
                lock (stateLock)
                    return _acquireState;
            }
            set
            {
                lock (stateLock)
                    _acquireState = value;

                if (value == AcquireStates.Running || value == AcquireStates.Single || value == AcquireStates.Immediate)
                {   
                    if (_worker.IsBusy)            
                        CreateWorker();            
                    _worker.RunWorkerAsync();
                }
            }
        }

        

        /// <summary>
        /// Set the ADC accuracy
        /// </summary>
        public ADCBits ConversionAccuray
        {
            get
            {
                lock (stateLock)
                    return _conversionAccuracy;
            }
            set
            {
                lock (stateLock)
                {
                    _conversionAccuracy = value;
                    if (_conversionAccuracy == ADCBits.Bit10)
                    {
                        AD12B = 0;              //uP ADC AD12B = 2 for 10 bit 
                        _scaleFactor = 1023; // 2^10 - 1                        
                    }
                    else if (_conversionAccuracy == ADCBits.Bit12)
                    {
                        AD12B = 1;
                        _scaleFactor = 4095; // 2^12 - 1                        
                    }

                    DetermineADCS();
                }
            }
        }

        /// <summary>
        /// Check if sample rate is in spec with current settings
        /// </summary>
        public bool IsSampleRateInSpec
        {
            get
            {
                lock (stateLock)
                {
                    bool inSpec;
                    if (ConversionAccuray == ADCBits.Bit12)
                    {
                        inSpec = (_samplingSettings.Fs <= 504201); //max spec for ADC sampling 12 bit
                    }
                    else
                    {
                        inSpec = (_samplingSettings.Fs <= 800000); //max spec for ADC sampling 10 bit
                    }

                    return inSpec;
                }
            }
        }

        /// <summary>
        /// Check if ADCS in spec with current settings
        /// </summary>
        public bool IsADCSInSpec
        {
            get
            {
                lock (stateLock)
                {
                    bool inSpec;
                    float tad = (float)(ADCS + 1) / _samplingSettings.Fcpu;  //calc TAD for given ADCS

                    if (ConversionAccuray == ADCBits.Bit12)
                    {
                        inSpec = (tad >= 116e-9);   //12bit minimum TAD is 117 nsec                    
                    }
                    else
                    {
                        inSpec = (tad >= 75e-9);    //10bit minimum TAD is 75 nsec                    
                    }

                    return inSpec;
                }
            }
        }

        /// <summary>
        /// Get of set the samples per second
        /// </summary>
        public SamplingSettings SamplingSettings
        {
            get
            {
                lock (stateLock)
                    return _samplingSettings;
            }
            set
            {
                lock (stateLock)
                {
                    _samplingSettings = value;
                    _scopeDataAnalyzer.SamplesPerSecond = value.Fs;
                    _settingsChanged = true;
                    if (value.ADCS.HasValue)
                        ADCS = value.ADCS.Value;
                    else
                        DetermineADCS();
                }
            }
        }


        /// <summary>
        /// Get of current set ADCS
        /// </summary>
        public byte ActualADCS
        {
            get
            {
                lock (stateLock)
                    return ADCS;
            }           
        }

        /// <summary>
        /// Get of set the samples per second
        /// </summary>
        public int SamplesCount
        {
            get
            {
                lock (stateLock)
                    return _sampleCount;
            }
            set
            {
                lock (stateLock)
                    _sampleCount = value;
            }
        }

                
        /// <summary>
        /// Trigger mode
        /// </summary>
        public TriggerModes TriggerMode
        {
            get
            {
                lock (stateLock)
                    return _scopeDataAnalyzer.TriggerMode;
            }
            set
            {
                lock (stateLock)
                {
                    _scopeDataAnalyzer.TriggerMode = value;
                    _triggeredLastTime = true;
                }
            }
        }

        /// <summary>
        /// Trigger slope
        /// </summary>
        public TriggerSlopes TriggerSlope
        {
            get
            {
                lock (stateLock)
                    return _scopeDataAnalyzer.TriggerSlope;
            }
            set
            {
                lock (stateLock)
                    _scopeDataAnalyzer.TriggerSlope = value;
            }
        }

        /// <summary>
        /// Trigger level
        /// </summary>
        public double TriggerLevel
        {
            get
            {
                lock (stateLock)
                    return _scopeDataAnalyzer.TriggerLevel;
            }
            set
            {
                lock (stateLock)
                    _scopeDataAnalyzer.TriggerLevel = value;
            }
        }

        /// <summary>
        /// Trigger coupling
        /// </summary>
        public CouplingModes Coupling
        {
            get
            {
                lock (stateLock)
                    return _scopeDataAnalyzer.Coupling;
            }
            set
            {
                lock (stateLock)
                    _scopeDataAnalyzer.Coupling = value;
            }
        }
        #endregion

        #region Private Methods


        /// <summary>
        /// Collect samples from the Analyzer device
        /// This code runs on background thread.
        /// </summary>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker == null)
                    return;

                bool doReportProgress = false;
                NetworkStream clientStream = _connection.TcpClient.GetStream();
                clientStream.ReadTimeout = 10000;
                byte[] rxBuff;
                lock (stateLock)
                    rxBuff = new byte[_sampleCount*2];
                byte[] txBuff = new byte[128];

                _triggerTimeout.Reset();
                if (!_triggerTimeout.IsRunning)
                    _triggerTimeout.Start();

                while (!worker.CancellationPending)
                {
                    int sampleCount;
                    SamplingSettings samplingSettings;
                    lock (stateLock)
                    {                        
                        // Get current settings
                        sampleCount = _sampleCount;
                        if (rxBuff.Length != _sampleCount*2)
                            rxBuff = new byte[_sampleCount*2];
                        samplingSettings = _samplingSettings;
                        if (_settingsChanged)
                        {
                            // Currently not supported by firmware
                            //    // Set M, N1
                            //    // Comment next 3 lines to disable setting of M and N1
                            //    FillTxBuff(6, samplingSettings.M, 0, samplingSettings.N1, 0, txBuff);
                            //    clientStream.Write(txBuff, 0, 8);
                            //    clientStream.Flush();                        
                            //    _settingsChanged = false; // Reset flag
                            //    Thread.Sleep(1); // Wait for the PLL to settle                            
                        }
                    }
                    
                    // Collect samples
                    FillTxBuff(1, sampleCount, samplingSettings.Fs, AD12B, ADCS, txBuff);
                    //fill transmit buffer with command and parameter values
                    clientStream.Write(txBuff, 0, 8); //send command and parameters to uP
                    clientStream.Flush();
                    
                    // Receive data
                    int read = 0, offset = 0, toRead = rxBuff.Length;
                    while (toRead > 0 && (read = clientStream.Read(rxBuff, offset, toRead)) > 0)
                    {
                        toRead -= read;
                        offset += read;
                    }
                    
                    // 2 bytes received is one sample, convert samples to one integer each
                    double[] samples = new double[sampleCount];
                    int s = 0;
                    for (int j = 0; j < 2*sampleCount; j += 2)
                        samples[s++] = (rxBuff[j] | rxBuff[j + 1] << 8)*3.3/_scaleFactor;

                    // Convert samples to scope data
                    ScopeData scopeData = _scopeDataAnalyzer.ScopeDataFromSamples(samples);

                    if (_acquireState == AcquireStates.Single // In single shot mode
                        && TriggerMode != TriggerModes.Off // Trigger on
                        && scopeData.Triggered) // Currently triggered
                    {
                        Debug.WriteLine("return scopedata");
                        lock (stateLock)
                            _acquireState = AcquireStates.Stopped; // Stop acquiring                                                
                        e.Result = scopeData;
                        return;
                    }
                    
                    // Fire _worker_ProgressChanged with data. If trigger mode != Off then only 
                    if (_acquireState == AcquireStates.Immediate)
                    {
                        lock (stateLock)
                            _acquireState = AcquireStates.Stopped;
                        e.Result = scopeData;
                        return;
                    }
                    else if (TriggerMode == TriggerModes.Off)
                    {
                        doReportProgress = true;
                    }
                    else if (TriggerMode == TriggerModes.Auto
                             && (scopeData.Triggered || _triggerTimeout.ElapsedMilliseconds >= 200))
                        // In auto mode send data if triggered or when timeout occured
                    {
                        doReportProgress = true;
                        if (scopeData.Triggered)
                        {
                            _triggerTimeout.Reset();
                            _triggerTimeout.Start();
                        }
                    }
                    else if (TriggerMode == TriggerModes.Normal)
                    {
                        if (scopeData.Triggered)
                        {
                            doReportProgress = true;
                            _triggeredLastTime = true;
                        }
                        else if (_triggeredLastTime)
                        {
                            scopeData = null;
                            doReportProgress = true;
                            _triggeredLastTime = false; // Prevent sending null each iteration
                        }
                    }
                    
                    // Check if cancelled
                    if (worker.CancellationPending)
                        e.Cancel = true;

                    if (doReportProgress)
                        worker.ReportProgress(0, scopeData); // Fire ProgressChanged                              
                    doReportProgress = false;

                    lock (stateLock)
                    {
                        if (_acquireState == AcquireStates.Stopped || _acquireState == AcquireStates.Immediate)
                            return;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                e.Result = null;
            }
            finally
            {                
                lock (stateLock)
                    _acquireState = AcquireStates.Stopped;
                _resetEvent.Set();
            }
        }


        /// <summary>
        /// Event handler is fired when data is ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!_worker.CancellationPending)
            {
                // Get data from DoWork. This event runs in UI thread.
                ScopeData scopeData = (ScopeData) e.UserState;
                OnDataReady(new ScopeEventArgs(scopeData));
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Get data from DoWork. This event runs in UI thread.
            ScopeData scopeData = (ScopeData)e.Result;
            OnDataReady(new ScopeEventArgs(scopeData));
        }


        /// <summary>
        /// Determine ADCS based on the ADC accuract and the sample rate
        /// </summary>
        private void DetermineADCS()
        {
            if (ConversionAccuray == ADCBits.Bit10)
            {
                ADCS = 4;
                //if (SamplingSettings.Fs < 571000)
                //    ADCS = 6;
                //else if (SamplingSettings.Fs < 666000)
                //    ADCS = 5;
                //else if (SamplingSettings.Fs < 800000)
                //    ADCS = 4;
                //else
                //    ADCS = 3;
            }
            else
            {
                ADCS = 6;
                //if (SamplingSettings.Fs < 504000)
                //    ADCS = 6;
                //else if (SamplingSettings.Fs < 588000)
                //    ADCS = 5;
                //else if (SamplingSettings.Fs < 705000)
                //    ADCS = 4;
                //else
                //    ADCS = 3;
            }
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
            //TxBuff[6] = 0x01;      //12 bit ADC; scale data by 4095
            //sendbuf[6] = 0x00;      //10 bit data; scale data by 1023
            TxBuff[7] = ADCS;
            //if(AD12B->Value == 12)sendbuf[6] = 0x01;	//AD12B=1 in uP means 12 bit ADC
            //else sendbuf[6] = 0x00;					//AD12B=0 in uP means 10 bit ADC
        }




        #endregion
    }
}
