using FFTWSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Elektor.SignalAnalyzer
{
    public class FftAnalyzer
    {
        private readonly BackgroundWorker _fftWorker;
        private static double[] _din;
        private static double[] _dout;
        private static double[,] _magnitudeSqrt;    //second dimension used for averaging magnitude
        private static double[,] _freqDomain;    //second dimension used for averaging multiple spectra
        //handles to managed arrays, keeps them pinned in memory
        private static GCHandle _hdin;
        private static GCHandle _hdout;
        //pointers to the FFTW plan object
        private static IntPtr _fplan;        
        private static readonly object PropertyLock = new object();
        private static WindowingTypes _windowType = WindowingTypes.Square;
        private static int _fftAvgCnt = 1;               //Spectra averaging variables; inits to no averaging
        private static int _fftAvgWant = 1;               //Spectra averaging variables; inits to no averaging
        private static int _fftSize;
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);

        #region Event Handlers

        /// <summary>
        /// Event which is fired when fft can be rendered
        /// </summary>
        public event EventHandler<FFTEventArgs> FftReady;

        /// <summary>
        /// Fire RenderFFT event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFftReady(FFTEventArgs e)
        {
            EventHandler<FFTEventArgs> handler = FftReady;
            handler?.Invoke(this, e);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FftAnalyzer()
        {
            _fftWorker = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            _fftWorker.DoWork += fftWorker_DoWork;
            _fftWorker.RunWorkerCompleted += fftWorker_RunWorkerCompleted;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cancel the worker thread
        /// </summary>
        public void Cancel()
        {
            if (_fftWorker.IsBusy)
            {
                _fftWorker.CancelAsync();
                _resetEvent.WaitOne(); // will block until _resetEvent.Set() call made
            }
        }


        /// <summary>
        /// Cleanup the memory allocation
        /// </summary>
        public static void CleanUp()
        {
            if (_fftSize > 0)
                MemoryPlanCleanUp();
        }

        /// <summary>
        /// Executes an FFT on another thread
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="samplesPerSecond"></param>
        /// <param name="wantedAverages"></param>
        /// <param name="windowType"></param>
        public void ExecuteFftAsync(double[] samples, int samplesPerSecond, int wantedAverages, WindowingTypes windowType)
        {
            if (_fftWorker.IsBusy)
                return;

            // Start the worker
            FftWorkerArguments args = new FftWorkerArguments { Samples = samples, SamplesPerSecond = samplesPerSecond, WantedAverages = wantedAverages, WindowType = windowType };
            _fftWorker.RunWorkerAsync(args);                
        }


        #endregion
        

        #region Private Functions

        /// <summary>
        /// Do fft calculations and averaging
        /// </summary>
        /// <param name="sender">Background worker</param>
        /// <param name="e">Arguments</param>
        private void fftWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                
                BackgroundWorker worker = sender as BackgroundWorker;
                FftWorkerArguments args = (FftWorkerArguments) e.Argument;

                lock (PropertyLock)
                {
                    if (_fftSize != args.Samples.Length || _fftAvgWant != args.WantedAverages)
                    {
                        if (_fftSize > 0)
                            MemoryPlanCleanUp(); // Plan changed, clean up
                        _fftSize = args.Samples.Length;
                        _fftAvgWant = args.WantedAverages;
                        MemoryPlansetup(_fftSize, _fftAvgWant);
                        _fftAvgCnt = 1; // reset averages
                    }
                    if (_windowType != args.WindowType)
                    {
                        _windowType = args.WindowType;
                        _fftAvgCnt = 1; // reset averages
                    }
                }
                int s = 0;
                for (int i = 0; i < 2*_fftSize; i = i + 2)
                {
                    _din[i] = args.Samples[s++]; // real part
                    _din[i + 1] = 0; // imaginary part
                }

                Windowing(_fftSize, _din, _windowType, 1, _din);
                fftw.execute(_fplan);

                //convert to dBm
                int j = 0;
                double scaling = 2/WindowGain(_fftSize, _windowType);
                //scaling for FFT N and window gain; no window WindowGain = N*N

                for (int i = 0; i < _fftSize; i++)
                {
                    int avgIndex = _fftAvgCnt%_fftAvgWant;
                    Complex temp = new Complex(_dout[j], _dout[j + 1]);
                    _magnitudeSqrt[i, avgIndex] = (temp.Magnitude*temp.Magnitude)*scaling;
                    //scaling FFT output by N and windowing correction
                    if (i == 0)
                        _magnitudeSqrt[i, avgIndex] = _magnitudeSqrt[i, avgIndex]/2;
                    //DC term Scales Differently                                
                    if (_magnitudeSqrt[i, avgIndex] <= 5e-14)
                        _magnitudeSqrt[i, avgIndex] = 5e-14; // Clamps to -120 dBm and avoids log10 of 0;
                    _freqDomain[i, avgIndex] = 10*Math.Log10(_magnitudeSqrt[i, avgIndex]/.05); // Power dBm = 10*log10(volt*volt/impedance/1mW) = 10*log10(volt*volt/50/.001)
                    _magnitudeSqrt[i, avgIndex] = Math.Sqrt(_magnitudeSqrt[i, avgIndex]);
                    j += 2;
                }

                if (_fftAvgCnt >= _fftAvgWant) // Wait until wanted amount collected.
                {
                    double[] fft = new double[_fftSize/2];
                    double[] rms = new double[_fftSize/2];
                    if (_fftAvgWant > 1)
                    {
                        // Calculate average
                        for (int i = 0; i < fft.Length; i++)
                        {
                            double avgTempDb = 0;
                            double avgTempMagnitude = 0;
                            for (int f = 0; f < _fftAvgWant; f++)
                            {
                                avgTempDb += _freqDomain[i, f]; //sum up FFTAvgWant Spectra to calculate average
                                avgTempMagnitude += _magnitudeSqrt[i, f];
                            }
                            fft[i] = avgTempDb/(_fftAvgWant);
                            rms[i] = avgTempMagnitude/(_fftAvgWant);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < fft.Length; i++)
                        {
                            fft[i] = _freqDomain[i, 0];
                            rms[i] = _magnitudeSqrt[i, 0];
                        }
                    }

                    if (worker == null || worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    FFTData fftData = new FFTData(fft, rms, args.SamplesPerSecond);
                    fftData.Statistics.Calculate(fft, rms);
                    e.Result = fftData;
                }
                _fftAvgCnt++; //increment pointer to ring buffer for spectra                        
            }
            catch (Exception) {}            
            finally
            {
                _resetEvent.Set();
            }
        }


        /// <summary>
        /// Event handler called when DoWork completes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fftWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            
            FFTData fftData = (FFTData)e.Result;
            OnFftReady(new FFTEventArgs(fftData));
        }



        /// <summary>
        /// Allocates memory and creates plan for N point FFT
        /// Pin in memory so GCC doesn't move them 
        /// </summary>
        /// <param name="amountSamples"></param>
        /// <param name="amountAverages"></param>      
        private static void MemoryPlansetup(int amountSamples, int amountAverages)
        {
            _din = new double[amountSamples * 2];            //stores incoming data samples
            _dout = new double[amountSamples * 2];           //stores FFT output 
            _freqDomain = new double[amountSamples, amountAverages];      //stores dBm frequency domain power
            _magnitudeSqrt = new double[amountSamples, amountAverages];  // Stores sqrt of magnitude

            _hdin = GCHandle.Alloc(_din, GCHandleType.Pinned);
            _hdout = GCHandle.Alloc(_dout, GCHandleType.Pinned);
            
            _fplan = fftw.dft_1d(amountSamples, _hdin.AddrOfPinnedObject(), _hdout.AddrOfPinnedObject(), fftw_direction.Forward, fftw_flags.Estimate);
        }


        /// <summary>
        /// Releases memory and destroys current FFT plan
        /// </summary>
        private static void MemoryPlanCleanUp()
        {
            try
            {
                fftw.destroy_plan(_fplan);
                _hdin.Free();
                _hdout.Free();
            }
            catch (Exception)
            { 
                // Ignore
            }            
        }




        public static void Windowing(int n, double[] din, WindowingTypes windowType, int scale, double[] wdin, bool containsComplex = true)
        {
            int k = 0;      //remember din is complex so input signal goes into real part
            for (int i = 0; i < n; i++)
            {
                switch (windowType)
                {
                    case WindowingTypes.Square: // square (no window)
                        wdin[k] = din[k] / scale;
                        break;
                    case WindowingTypes.Parzen: // parzen window
                        wdin[k] = din[k] * Parzen(i, n) / scale;
                        break;
                    case WindowingTypes.Welch: // welch window
                        wdin[k] = din[k] * Welch(i, n) / scale;
                        break;
                    case WindowingTypes.Hanning: // hanning window
                        wdin[k] = din[k] * Hanning(i, n) / scale;
                        break;
                    case WindowingTypes.Hamming: // hamming window
                        wdin[k] = din[k] * Hamming(i, n) / scale;
                        break;
                    case WindowingTypes.Blackman: // blackman window
                        wdin[k] = din[k] * Blackman(i, n) / scale;
                        break;
                    case WindowingTypes.Steeper30db: // steeper 30-dB/octave rolloff window
                        wdin[k] = din[k] * Steeper(i, n) / scale;
                        break;                    
                }
                if (containsComplex)
                    k += 2;
                else
                    k++;
            }
        }

        /* Reference: "Numerical Recipes in C" 2nd Ed.
         * by W.H.Press, S.A.Teukolsky, W.T.Vetterling, B.P.Flannery
         * (1992) Cambridge University Press.
         * ISBN 0-521-43108-5
         * Sec.13.4 - Data Windowing
         */
        private static double Parzen(int i, int nn)
        {
            return (1.0 - Math.Abs(((double)i - 0.5 * (double)(nn - 1))
                        / (0.5 * (double)(nn + 1))));
        }

        private static double Welch(int i, int nn)
        {
            return (1.0 - (((double)i - 0.5 * (double)(nn - 1))
                     / (0.5 * (double)(nn + 1)))
                * (((double)i - 0.5 * (double)(nn - 1))
                  / (0.5 * (double)(nn + 1))));
        }

        private static double Hanning(int i, int nn)
        {
            return (0.5 * (1.0 - Math.Cos(2.0 * Math.PI * (double)i / (double)(nn - 1))));
        }

        /* Reference: "Digital Filters and Signal Processing" 2nd Ed.
         * by L. B. Jackson. (1989) Kluwer Academic Publishers.
         * ISBN 0-89838-276-9
         * Sec.7.3 - Windows in Spectrum Analysis
         */
        private static double Hamming(int i, int nn)
        {
            return (0.54 - 0.46 * Math.Cos(2.0 * Math.PI * (double)i / (double)(nn - 1)));
        }

        private static double Blackman(int i, int nn)
        {
            return (0.42 - 0.5 * Math.Cos(2.0 * Math.PI * (double)i / (double)(nn - 1))
                + 0.08 * Math.Cos(4.0 * Math.PI * (double)i / (double)(nn - 1)));
        }

        private static double Steeper(int i, int nn)
        {
            return (0.375
                - 0.5 * Math.Cos(2.0 * Math.PI * (double)i / (double)(nn - 1))
                + 0.125 * Math.Cos(4.0 * Math.PI * (double)i / (double)(nn - 1)));
        }


        // returns CG (coherent gain) of window being used times N
        private static double WindowGain(int n, WindowingTypes windowType)
        {
            double density = 0;
            for (int i = 0; i < n; i++)
            {
                switch (windowType)
                {
                    case WindowingTypes.Square: // square (no window)
                        density += 1.0;
                        break;
                    case WindowingTypes.Parzen: // parzen window
                        density += Parzen(i, n) * Parzen(i, n);
                        break;

                    case WindowingTypes.Welch: // welch window
                        density += Welch(i, n) * Welch(i, n);
                        break;

                    case WindowingTypes.Hanning: // hanning window
                        density += Hanning(i, n) * Hanning(i, n);
                        break;

                    case WindowingTypes.Hamming: // hamming window
                        density += Hamming(i, n) * Hamming(i, n);
                        break;

                    case WindowingTypes.Blackman: // blackman window
                        density += Blackman(i, n) * Blackman(i, n);
                        break;

                    case WindowingTypes.Steeper30db: // steeper 30-dB/octave rolloff window
                        density += Steeper(i, n) * Steeper(i, n);
                        break;                    
                }
            }
            density *= n;
            return density;
        }
    }

    #endregion


    /// <summary>
    /// FFT arguments to use for thread start
    /// </summary>
    public class FftWorkerArguments
    {
        public double[] Samples { get; set; }
        public int SamplesPerSecond { get; set; }
        public int WantedAverages { get; set; }
        public WindowingTypes WindowType { get; set; }
    }
}
