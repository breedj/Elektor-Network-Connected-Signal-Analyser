using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Numerics;
using FFTWSharp;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using Elektor.SignalAnalyzer;      //user application settings with persistence
using System.Threading;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace SignalAnalyzerApplication
{

    public partial class SygnalAnalyzerForm : Form
    {
        #region Constants 

        private const int Port = 4000;
        private readonly string[] _macAddresses = { "00:08:dc:00:ab:cd", "02:08:dc:00:ab:cd"};   // Find devices by these mac adresses when using DHCP
        private readonly string[] _staticIpAddresses = { "192.168.1.123", "192.168.0.123", "192.168.2.123", "192.168.0.122" }; // Find devices by these IP addresses when using static IP
        
        #endregion

        private string ipAddress = string.Empty; // for now fixed. Put back detection funcationality.
        private int _scopeRenderInterval = 50;   // Initial value of scope render time in ms
        private DateTime _tmrWfs = DateTime.Now; // Update rate timing
        private readonly ClientConnection _connection = new ClientConnection();
        private readonly SignalAnalyzer _analyzer = new SignalAnalyzer();
        private readonly SignalGenerator _signalGenator = new SignalGenerator();
        private readonly Stopwatch _swScopeRender = new Stopwatch();      // To time the amount of time it takes to render the scope view
        
        public SygnalAnalyzerForm()
        {
            InitializeComponent();                  
        }

        
        private void chart_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    dataIn.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    dataIn.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = dataIn.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = dataIn.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = dataIn.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = dataIn.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = dataIn.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = dataIn.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    double posYStart = dataIn.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    double posYFinish = dataIn.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    if (Control.ModifierKeys == Keys.Shift)
                        dataIn.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                    else
                        dataIn.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch { }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            _analyzer.Connection = _connection;
            _analyzer.TriggerLevel = 0;
            _analyzer.SamplesCount = 5042;
            _analyzer.SamplingSettings = new SamplingSettings { Fs = 504201, M = 60, N1 = 2, N2 = 2 };
            _analyzer.TriggerMode = TriggerModes.Auto;
            _analyzer.TriggerSlope = TriggerSlopes.RisingEdge;
            _analyzer.WindowType = WindowingTypes.Square;
            _analyzer.RenderFft += signalAnalyzer_RenderFFT;
            _analyzer.RenderScope += analyzer_RenderScope;

            dataIn.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            dataIn.MouseWheel += chart_MouseWheel;
            dataIn.CursorPositionChanged += dataIn_CursorPositionChanged;
            dataIn.Series["Series1"].Points.Clear();

            spectrum.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            spectrum.MouseWheel += chart_MouseWheel;
            spectrum.CursorPositionChanged += spectrum_CursorPositionChanged;
            spectrum.Series["Series1"].Points.Clear();

            _signalGenator.Connection = _connection;
            _signalGenator.Enabled = false;
            _signalGenator.Frequency = 1000;
            _signalGenator.Waveform = GeneratorWaveforms.Sinusoid;

            cmbTriggerMode.SelectedIndex = 1; // Auto
            cmbTriggerSlope.SelectedIndex = 0; // Rising edge
            chkEnableSignalGenerator.Checked = false;
            cmbGeneratorWaveform.SelectedIndex = 0;
            txtGeneratorFreq.Text = "1000";
            cmbADCS.SelectedIndex = 4; // 4
            rbADC12Bit.Checked = true;

            lblSamplingSettings.Text = $"M:60 - N1:2 - fcpu:60.0 MHz";

            _swScopeRender.Start();

            LoadSettings();
            SetSamplingVariables();

            groupBox1.Click += ResetControlFocus;
            groupBox2.Click += ResetControlFocus;
            groupBox3.Click += ResetControlFocus;
            groupBox4.Click += ResetControlFocus;
            groupBox5.Click += ResetControlFocus;
            groupBox6.Click += ResetControlFocus;
            groupBox7.Click += ResetControlFocus;            
        }


        private void LoadSettings()
        {
            // Load ip address from settings
            ipAddress = Properties.Settings.Default.IPanalyzer;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                cmbIPAddresses.Items.Add(ipAddress);
                btnConnect.Enabled = true;
            }
            else
                cmbIPAddresses.Items.Add("Click 'Find'");
            cmbIPAddresses.SelectedIndex = 0;
        }

        /// <summary>
        /// Cursor changed event of scope view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataIn_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e)
        {
            ushort index = (ushort)((dataIn.ChartAreas["ChartArea1"].CursorX.Position) /   //find index of chart data point closest to cursor position
                               (dataIn.ChartAreas["ChartArea1"].CursorX.Interval));

            string xString = "";
            string yString = "";
            if (dataIn.Series[0].Points.Count >= index)
            {
                double x = dataIn.Series[0].Points[index].XValue;
                double y = dataIn.Series[0].Points[index].YValues[0];

                dataIn.ChartAreas["ChartArea1"].CursorY.Position = y;
                
                if (x < 1E-3)
                    xString = $"{(x * 1E6).ToString("F3")} μs";
                else if (x < 1)
                    xString = $"{(x * 1000).ToString("F3")} ms";
                else
                    xString = $"{x.ToString("F3")} s";
                
                if (y < 1E-3)
                    yString = $"{(y * 1E6).ToString("F3")} μV";
                else if (y < 1)
                    yString = $"{(y * 1000).ToString("F3")} mV";
                else
                    yString = $"{y.ToString("F3")} V";
            }
            lblScopeCursor.Text = $"{yString} @ {xString}";
        }

        /// <summary>
        /// Cursor changed event of FFT view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spectrum_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e)
        {
            ushort index = (ushort)((spectrum.ChartAreas["ChartArea1"].CursorX.Position) /   //find index of chart data point closest to cursor position
                                    (spectrum.ChartAreas["ChartArea1"].CursorX.Interval));

            if (spectrum.Series[0].Points.Count >= index)
            {
                double x = spectrum.Series[0].Points[index].XValue;                
                spectrum.ChartAreas["ChartArea1"].CursorY.Position = spectrum.Series[0].Points[index].YValues[0];

                string rmsString;
                string xString;
                if (x > 1E3)
                    xString = $"{(x/1E3).ToString("F3")} kHz";
                else
                    xString = $"{x.ToString("F3")} Hz";
                
                if (rbDbm.Checked)
                {
                    // y is in dbm.
                    double dBm = spectrum.Series[0].Points[index].YValues[0];
                    double vrms = Math.Sqrt(Math.Pow(10, (dBm/10))*.05);
                    if (vrms < 1E-3)
                        rmsString = $"{(vrms*1E6).ToString("F3")} μVrms";
                    else if (vrms < 1)
                        rmsString = $"{(vrms*1E3).ToString("F3")} mVrms";
                    else
                        rmsString = $"{vrms.ToString("F3")} Vrms";

                    lblFFTCursor.Text = $"{dBm.ToString("F3")} dBm / {rmsString} @ {xString}";
                }
                else
                {
                    // y is in Vrms                    
                    double Vrms = spectrum.Series[0].Points[index].YValues[0];
                    if (Vrms < 1E-3)
                        rmsString = $"{(Vrms * 1E6).ToString("F3")} μVrms";
                    else if (Vrms < 1)
                        rmsString = $"{(Vrms * 1E3).ToString("F3")} mVrms";
                    else
                        rmsString = $"{Vrms.ToString("F3")} Vrms";

                    // Vrms to dBm
                    double dbm = (10 * Math.Log10(Math.Pow(Vrms, 2) / 50.0)) + 30; // 50 Ohm , 1mW = 30dBm
                    lblFFTCursor.Text = $"{dbm.ToString("F3")} dBm / {rmsString} @ {xString}";
                }
            }
        }
        

        /// <summary>
        /// Event when closing the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_connection.IsConnected)
                {
                    _signalGenator.Enabled = false;
                    _analyzer.Stop();
                    _connection.Disconnect();
                }
                _analyzer.CleanUp();
            }
            catch (Exception)
            {
                //Suppress error, we are closing app
            }
        }

        /// <summary>
        /// trigger level changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTriggerLevel_ValueChanged(object sender, EventArgs e)
        {
            _analyzer.TriggerLevel = tbTriggerLevel.Value / 1000.0;
            lblTriggerLevel.Text = _analyzer.TriggerLevel.ToString("F3");            
            DrawTriggerLevelLine();
        }


        /// <summary>
        /// Draw trigger level dashed line in scope view.
        /// </summary>
        private void DrawTriggerLevelLine()
        {
            HorizontalLineAnnotation annotation = new HorizontalLineAnnotation
            {
                IsSizeAlwaysRelative = false,
                IsInfinitive = true,
                ClipToChartArea = "ChartArea1",
                AxisX = dataIn.ChartAreas["ChartArea1"].AxisX,
                AxisY = dataIn.ChartAreas["ChartArea1"].AxisY,
                AnchorY = _analyzer.TriggerLevel,
                LineWidth = 1,
                StartCap = LineAnchorCapStyle.None,
                EndCap = LineAnchorCapStyle.None,
                LineDashStyle = ChartDashStyle.Dash,
                LineColor = Color.Green
            };
            dataIn.Annotations.Clear();
            dataIn.Annotations.Add(annotation);
        }

        /// <summary>
        /// Trigger mode changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTriggerMode.SelectedIndex)
            {
                case 0:
                    _analyzer.TriggerMode = TriggerModes.Off;
                    break;
                case 1:
                    _analyzer.TriggerMode = TriggerModes.Auto;
                    break;
                case 2:
                    _analyzer.TriggerMode = TriggerModes.Normal;
                    break;
            }
        }

        /// <summary>
        /// Trigger slope changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTriggerSlope_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTriggerSlope.SelectedIndex)
            {
                case 0:
                    _analyzer.TriggerSlope = TriggerSlopes.RisingEdge;
                    break;
                case 1:
                    _analyzer.TriggerSlope = TriggerSlopes.FallingEdge;
                    break;
            }
        }

             
        /// <summary>
        /// Amount of averages changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetAverages_Click(object sender, EventArgs e)
        {
            ScopeData.ResetAverages();                        
        }
        

        /// <summary>
        /// Line type of scope view changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void visualisation_Click(object sender, EventArgs e)
        {
            if (radioVisLine.Checked)
                dataIn.Series["Series1"].ChartType = SeriesChartType.Line;
            else if (radioVisDots.Checked)
                dataIn.Series["Series1"].ChartType = SeriesChartType.FastPoint;
        }

        /// <summary>
        /// AC/DC Coupling changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbCoupling_Click(object sender, EventArgs e)
        {
            if (rbCouplingAC.Checked)
                _analyzer.Coupling = CouplingModes.AC;
            else
                _analyzer.Coupling = CouplingModes.DC;
        }

        /// <summary>
        /// Selected fft window type changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowType_Click(object sender, EventArgs e)
        {            
            if (WindowNone.Checked) _analyzer.WindowType = WindowingTypes.Square;
            if (WindowParzen.Checked) _analyzer.WindowType = WindowingTypes.Parzen;
            if (WindowWelch.Checked) _analyzer.WindowType = WindowingTypes.Welch;
            if (WindowHanning.Checked) _analyzer.WindowType = WindowingTypes.Hanning;
            if (WindowHamming.Checked) _analyzer.WindowType = WindowingTypes.Hamming;
            if (WindowBlackman.Checked) _analyzer.WindowType = WindowingTypes.Blackman;
            if (Window30dB.Checked) _analyzer.WindowType = WindowingTypes.Steeper30db;
        }

        /// <summary>
        /// Samples per second changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSamplesPerSecond_ValueChanged(object sender, EventArgs e)
        {
            if (chkPreventJitter.Checked)
            {
                SamplingSettings samplingSettings = _analyzer.GetSpurFreeSampleFrequency(tbSamplesPerSecond.Value, !chkEnableSignalGenerator.Checked);
                txtSamplesPerSecond.Text = samplingSettings.Fs.ToString();
                tbSamplesPerSecond.Value = samplingSettings.Fs;
            }
            else
                txtSamplesPerSecond.Text = tbSamplesPerSecond.Value.ToString();            
        }

        /// <summary>
        /// Change samples per second on keyup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSamplesPerSecond_KeyUp(object sender, KeyEventArgs e)
        {
            SetSamplingVariables();
        }

        /// <summary>
        /// Change samples per second on mouseup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSamplesPerSecond_MouseUp(object sender, MouseEventArgs e)
        {
            SetSamplingVariables();
        }

        /// <summary>
        /// Set the sampling values and sent do analyzer
        /// </summary>
        private void SetSamplingVariables()
        {
            int samplesPerSecond = tbSamplesPerSecond.Value;
            SamplingSettings samplingSettings;
            if (chkPreventJitter.Checked)      
                samplingSettings = _analyzer.GetSpurFreeSampleFrequency(samplesPerSecond, !chkEnableSignalGenerator.Checked);                            
            else
                samplingSettings = new SamplingSettings {Fs = samplesPerSecond, M = 60, N1 = 2, N2 = 2, Fcpu = 60000000 };
            if (cmbADCS.SelectedIndex == 0)
                samplingSettings.ADCS = null;
            else
                samplingSettings.ADCS = (byte)(cmbADCS.SelectedIndex + 2);
            _analyzer.SamplingSettings = samplingSettings; // Set analyzer with new values.
            if (samplingSettings.Fs <= tbSamplesPerSecond.Maximum)
                tbSamplesPerSecond.Value = samplingSettings.Fs;
            else
                tbSamplesPerSecond.Value = tbSamplesPerSecond.Maximum;                
            lblSamplingSettings.Text = $"M:{samplingSettings.M} - N1:{samplingSettings.N1} - fcpu:{(samplingSettings.Fcpu / 1000000.0).ToString("F1")} MHz";
            CheckIfOutOfSpec();
            ScopeData.ResetAverages();
        }


        /// <summary>
        /// Check if all within specs. If not show warning.
        /// </summary>
        private void CheckIfOutOfSpec()
        {
            if (!(_analyzer.IsADCSInSpec && _analyzer.IsSampleRateInSpec))
            {
                lblOutOfSpec.Text = rbADC12Bit.Checked ? "Out of spec: use 10 bit" : "Out of spec";
            }
            else
                lblOutOfSpec.Text = "";
            lblOutOfSpec.Visible = !(_analyzer.IsADCSInSpec && _analyzer.IsSampleRateInSpec);
        }

        
        private void txtSamplesPerSecond_Leave(object sender, EventArgs e)
        {
            if (txtSamplesPerSecond.Text.Length == 0)
                return;
            int value;
            if (int.TryParse(txtSamplesPerSecond.Text, out value)
                && value >= tbSamplesPerSecond.Minimum
                && value <= tbSamplesPerSecond.Maximum)
            {
                tbSamplesPerSecond.Value = value;
                SetSamplingVariables();
            }
        }


        /// <summary>
        /// ADCS changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbADCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSamplingVariables();
        }

        /// <summary>
        /// Handle Enter key in sample rate textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSamplesPerSecond_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSamplesPerSecond.Text.Length == 0)
                    return;
                int value;
                if (int.TryParse(txtSamplesPerSecond.Text, out value)
                    && value >= tbSamplesPerSecond.Minimum
                    && value <= tbSamplesPerSecond.Maximum)
                {
                    tbSamplesPerSecond.Value = value;
                    SetSamplingVariables();
                }
            }            
        }

        /// <summary>
        /// No of value changed in textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbNoOfSamples_ValueChanged(object sender, EventArgs e)
        {
            _analyzer.SamplesCount = tbNoOfSamples.Value;
            txtNoOfSamples.Text = tbNoOfSamples.Value.ToString();            
        }

        /// <summary>
        /// Handle changed value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoOfSamples_TextChanged(object sender, EventArgs e)
        {
            if (txtNoOfSamples.Text.Length == 0)
                return;
            int value;
            if (int.TryParse(txtNoOfSamples.Text, out value)
                && value >= tbNoOfSamples.Minimum
                && value <= tbNoOfSamples.Maximum)
                tbNoOfSamples.Value = value;    // Set value of slider, will fire tbNoOfSamples_ValueChanged
        }

        /// <summary>
        /// Handle Enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoOfSamples_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNoOfSamples.Text.Length == 0)
                    return;
                int value;
                if (int.TryParse(txtNoOfSamples.Text, out value)
                    && value >= tbNoOfSamples.Minimum
                    && value <= tbNoOfSamples.Maximum)
                {
                    tbNoOfSamples.Value = value;
                    SetSamplingVariables();
                }
            }
        }

        private void chkPreventJitter_Click(object sender, EventArgs e)
        {
            SetSamplingVariables();
        }

        private void udFFTAverages_ValueChanged(object sender, EventArgs e)
        {
            _analyzer.FftAverages = (int)udFFTAverages.Value;
        }

        private void rbADC12Bit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbADC10Bit.Checked)
            {
                _analyzer.ConversionAccuray = ADCBits.Bit10;
                cmbADCS.SelectedIndex = 2;
            }
            else
            {
                _analyzer.ConversionAccuray = ADCBits.Bit12;
                cmbADCS.SelectedIndex = 4;
            }
            CheckMaxSamplesPerSecond();
            CheckIfOutOfSpec();
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
            {
                ipAddress = cmbIPAddresses.Text;
                _connection.Connect(ipAddress, Port);
                if (_connection.IsConnected)
                {
                    Properties.Settings.Default.IPanalyzer = ipAddress;
                    Properties.Settings.Default.Save(); //persist ipaddress that works

                    chkEnableSignalGenerator.Enabled = true; // Enable checkbox;
                    _signalGenator.Enabled = false;
                    btnConnect.Text = "Disconnect";
                    btnConnect.BackColor = Color.LightGreen;
                    btnStartAcquisition.Enabled = true;
                    btnSinleShot.Enabled = true;
                    cmbIPAddresses.Enabled = false;
                    btnFindDevices.Enabled = false;
                }
                else
                    MessageBox.Show("Could not connect.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _analyzer.Stop();
                _connection.Disconnect();
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Gainsboro;
                _analyzer.AcquireState = AcquireStates.Stopped;
                btnStartAcquisition.Text = "Stop";
                btnStartAcquisition.BackColor = Color.Red;
                btnSinleShot.BackColor = Color.Gainsboro;
                btnStartAcquisition.Enabled = false;
                _signalGenator.Enabled = false;
                chkEnableSignalGenerator.Checked = false;
                chkEnableSignalGenerator.Enabled = false; // Enable checkbox;                
                btnSinleShot.Enabled = false;
                cmbIPAddresses.Enabled = true;
                btnFindDevices.Enabled = true;
            }
        }

        void btnStartAcquisition_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
            {
                MessageBox.Show("Not connected.");
                return;
            }

            if (_analyzer.AcquireState == AcquireStates.Stopped)
            {
                _analyzer.AcquireState = AcquireStates.Running;                
                btnStartAcquisition.Text = "Run";
                btnStartAcquisition.BackColor = Color.LightGreen;
                btnSinleShot.BackColor = Color.Gainsboro;
            }
            else if (_analyzer.AcquireState == AcquireStates.Running || _analyzer.AcquireState == AcquireStates.Single)
            {
                _analyzer.AcquireState = AcquireStates.Stopped;
                btnStartAcquisition.Text = "Stop";
                btnStartAcquisition.BackColor = Color.Red;
                btnSinleShot.BackColor = Color.Gainsboro;
            }            
        }


        void btnSinleShot_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
            {
                MessageBox.Show("Not connected.");
                return;
            }

            _analyzer.AcquireState = AcquireStates.Single;
            btnStartAcquisition.Text = "Run";
            btnStartAcquisition.BackColor = Color.LightGreen;
            btnSinleShot.BackColor = Color.LightGreen;
        }

               

        /// <summary>
        /// Event handler fired when Scope view has to be rendered
        /// </summary>        
        void analyzer_RenderScope(object sender, ScopeEventArgs e)
        {            
            // If the render time is longer than the time between calls of this event, skip rendering.
            if (_swScopeRender.ElapsedMilliseconds > _scopeRenderInterval)
            {
                _swScopeRender.Restart();
                RenderScopeData(e.ScopeData);
                _scopeRenderInterval = (int)_swScopeRender.ElapsedMilliseconds + 10;
                _scopeRenderInterval += (int)(_scopeRenderInterval * 0.1); // Add 10%
                _swScopeRender.Restart();
            }                       
        }
        
                
        /// <summary>
        /// Render scope data
        /// </summary>
        /// <param name="data"></param>
        void RenderScopeData(ScopeData data)
        {           
            try
            {
                //dataIn.Series.RemoveAt(0);
                while (dataIn.Series.Count > 0)
                    dataIn.Series.RemoveAt(0);
                dataIn.Series.Add("Series1");
                dataIn.Series["Series1"].ChartArea = "ChartArea1";                    
                dataIn.Series["Series1"].ChartType = radioVisLine.Checked ? SeriesChartType.Line : SeriesChartType.FastPoint;
                dataIn.Series["Series1"].MarkerSize = 1;
                double[] xPoints = null;
                double[] yPoints = null;

                if (data != null)
                {
                    if (data.Triggered)
                    {
                        // Triggered, display from first trigger point
                        int samplesToShow;
                        int firstSampleNumber;

                        if (data.TriggerSamples.Count > 2)
                        {
                            samplesToShow = data.TriggerSamples[data.TriggerSamples.Count - 1] - data.TriggerSamples[0];
                            firstSampleNumber = data.TriggerSamples[0];
                        }
                        else if (data.TriggerSamples.Count > 1)
                        {
                            samplesToShow = data.TriggerSamples[1] - data.TriggerSamples[0];
                            firstSampleNumber = data.TriggerSamples[0];
                        }
                        else
                        {
                            samplesToShow = data.SampleCount - data.TriggerSamples[0];
                            firstSampleNumber = 0;
                        }

                        yPoints = data.Voltages.Skip(firstSampleNumber).Take(samplesToShow).ToArray();
                        xPoints = Enumerable.Range(0, yPoints.Count()).Select(s => s * data.SampleInterval).ToArray();                            
                    }
                    else if (_analyzer.TriggerMode == TriggerModes.Auto || _analyzer.TriggerMode == TriggerModes.Off)
                    {
                        yPoints = data.Voltages.ToArray();
                        xPoints = Enumerable.Range(0, yPoints.Count()).Select(s => s * data.SampleInterval).ToArray();                            
                    }
                    // Draw points
                    dataIn.Series["Series1"].Points.DataBindXY(xPoints, yPoints);

                    // Draw min/max trace                   
                    // Draw min/max trace                   
                    if (chkDrawMinTrace.Checked)
                    {
                        dataIn.Series.Add("Series2");
                        dataIn.Series["Series2"].ChartArea = "ChartArea1";
                        dataIn.Series["Series2"].ChartType = SeriesChartType.Line;
                        dataIn.Series["Series2"].MarkerSize = 1;
                        dataIn.Series["Series2"].Color = Color.Orange;
                        dataIn.Series["Series2"].BorderDashStyle = ChartDashStyle.Dash;
                        dataIn.Series["Series2"].Points.DataBindXY(xPoints, data.Statistics.MinTrace.Take(xPoints.Length).ToArray());
                    }
                    if (chkDrawMaxTrace.Checked)
                    {
                        dataIn.Series.Add("Series3");
                        dataIn.Series["Series3"].ChartArea = "ChartArea1";
                        dataIn.Series["Series3"].ChartType = SeriesChartType.Line;
                        dataIn.Series["Series3"].MarkerSize = 1;
                        dataIn.Series["Series3"].Color = Color.Orange;
                        dataIn.Series["Series3"].BorderDashStyle = ChartDashStyle.Dash;
                        dataIn.Series["Series3"].Points.DataBindXY(xPoints, data.Statistics.MaxTrace.Take(xPoints.Length).ToArray());
                    }
                }
                    
                RenderScopeStats(data);
                RenderDisplayUpdateRate();

                // Stop button state after single shot
                if (_analyzer.AcquireState == AcquireStates.Stopped)
                {
                    btnStartAcquisition.Text = "Stop";
                    btnStartAcquisition.BackColor = Color.Red;
                }
            }
            catch (Exception)
            {
            } 
        }


        DateTime _lastUpdateRateRenderTime = DateTime.Now;
        readonly List<double> updateRates = new List<double>();
        /// <summary>
        /// Calculate and render the display update rate of the scope view.
        /// </summary>
        private void RenderDisplayUpdateRate()
        {            
            TimeSpan timeDiff = DateTime.Now.Subtract(_tmrWfs);
            if (DateTime.Now.Subtract(_lastUpdateRateRenderTime).TotalMilliseconds > 200 && updateRates.Any())
            {
                lblWaveformsSecond.Text = updateRates.Average().ToString("F0") + "Hz";
                updateRates.Clear();
                _lastUpdateRateRenderTime = DateTime.Now;
            }
            else
            {
                updateRates.Add(1000 / timeDiff.TotalMilliseconds);
            }
            _tmrWfs = DateTime.Now;
        }


        DateTime lastStatsRenderTime = DateTime.Now;
        /// <summary>
        /// Render the statistics
        /// </summary>
        /// <param name="data"></param>
        private void RenderScopeStats(ScopeData data)
        {
            // Show statistics data twice a second
            if (DateTime.Now.Subtract(lastStatsRenderTime).TotalMilliseconds > 500)
            {
                if (data != null && data.Triggered || _analyzer.TriggerMode == TriggerModes.Auto || _analyzer.TriggerMode == TriggerModes.Off)
                {
                    dataIn.ChartAreas["ChartArea1"].CursorX.Interval = data.SampleInterval;            //sets Xcursor step size to sample time            

                    if (data.BaseFrequency.HasValue)
                        lblScopeFrequency.Text = data.BaseFrequency.Value.ToString("F1") + "Hz";
                    else
                        lblScopeFrequency.Text = "?";

                    lblMinCurrent.Text = data.Statistics.Min.ToString("F4") + "V";
                    lblMinAvg.Text = data.Statistics.AvgMin.ToString("F4") + "V";
                    lblMaxCurrent.Text = data.Statistics.Max.ToString("F4") + "V";
                    lblMaxAvg.Text = data.Statistics.AvgMax.ToString("F4") + "V";
                    lblAverageCur.Text = data.Statistics.Average.ToString("F4") + "V";
                    lblAverageAvg.Text = data.Statistics.AvgAverage.ToString("F4") + "V";
                    lblPeekToPeekCur.Text = data.Statistics.PeekToPeek.ToString("F4") + "V";
                    lblPeekToPeekAvg.Text = data.Statistics.AvgPeekToPeek.ToString("F4") + "V";
                    lblNumberOfAverages.Text = data.Statistics.AvgCount.ToString();
                    if (data.DCVoltage < 1.0)
                        lblDCOffset.Text = "DC offset: " + (data.DCVoltage * 1000.0).ToString("F1") + " mV";
                    else
                        lblDCOffset.Text = "DC offset: " + data.DCVoltage.ToString("F4") + " V";
                }
                else
                {
                    lblScopeFrequency.Text = "?";
                    lblMinCurrent.Text = "?";
                    lblMinAvg.Text = data.Statistics.AvgMin.ToString("F4");
                    lblMaxCurrent.Text = "?";
                    lblMaxAvg.Text = data.Statistics.AvgMax.ToString("F4");
                    lblAverageCur.Text = "?";
                    lblAverageAvg.Text = data.Statistics.AvgAverage.ToString("F4");
                    lblPeekToPeekCur.Text = "?";
                    lblPeekToPeekAvg.Text = data.Statistics.AvgPeekToPeek.ToString("F4");
                    lblNumberOfAverages.Text = data.Statistics.AvgCount.ToString();
                    lblDCOffset.Text = "?";
                }
                lastStatsRenderTime = DateTime.Now;
            }
        }


        void signalAnalyzer_RenderFFT(object sender, FFTEventArgs e)
        {
            RenderFFTData(e.FFTData);            
        }

        

        void RenderFFTData(FFTData data)
        {           
            try
            {
                spectrum.Series.RemoveAt(0);
                spectrum.Series.Add("Series1");
                spectrum.Series["Series1"].ChartArea = "ChartArea1";
                spectrum.Series["Series1"].ChartType = SeriesChartType.Line;
                spectrum.Series["Series1"].MarkerSize = 1;                    

                if (data != null)
                {                        
                    double[] yPoints = rbDbm.Checked ? data.FreqDomain : data.Vrms;
                    double[] xPoints = Enumerable.Range(0, yPoints.Count()).Select(s => s * (double)data.SamplesPerSecond / data.FreqDomain.Length / 2).ToArray();
                                        
                    spectrum.ChartAreas["ChartArea1"].CursorX.Interval = data.SamplesPerSecond / (double)data.FreqDomain.Length / 2.0;        //matches cursor step size to X axis step size
                    spectrum.Series["Series1"].Points.DataBindXY(xPoints, yPoints);                        
                }                    
                RenderFftStats(data);
            }
            catch (Exception) { }            
        }



        DateTime _lastFftRenderTime = DateTime.Now;        
        /// <summary>
        /// Calculate and render the display update rate of the scope view.
        /// </summary>
        private void RenderFftStats(FFTData data)
        {            
            if (DateTime.Now.Subtract(_lastFftRenderTime).TotalMilliseconds > 200)
            {
                if (data != null)
                {
                    lblFFTResolution.Text = "Resolution: " + data.ResolutionBandWith.ToString("F0") + "Hz";
                    lblFFTFrequency.Text = "Base frequency: " + data.BaseFrequency.ToString("F0") + "Hz";                    
                }
                else
                {
                    lblFFTResolution.Text = "Resolution: ?";
                    lblFFTFrequency.Text = "Base frequency: ?";
                }
                _lastFftRenderTime = DateTime.Now;
            }            
        }

        /// <summary>
        /// Reset the trigger level to 0V
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetTriggerLevel_Click(object sender, EventArgs e)
        {
            _analyzer.TriggerLevel = 0;
            tbTriggerLevel.Value = 0;
        }


        /// <summary>
        /// Generator frequency textbox key pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }            
        }

        /// <summary>
        /// Signal generator enable/disble
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableSignalGenerator_CheckedChanged(object sender, EventArgs e)
        {
            _signalGenator.Enabled = chkEnableSignalGenerator.Checked;
            CheckMaxSamplesPerSecond();
            SetSamplingVariables();
        }


        private void CheckMaxSamplesPerSecond()
        {
            if (_signalGenator.Enabled)
            {

                if (rbADC12Bit.Checked && tbSamplesPerSecond.Value > 504201)
                {
                    tbSamplesPerSecond.Value = 504201;
                    txtSamplesPerSecond.Text = "504201";
                }
                if (rbADC12Bit.Checked && tbSamplesPerSecond.Value > 800000)
                {
                    tbSamplesPerSecond.Value = 800000;
                    txtSamplesPerSecond.Text = "800000";
                }
                tbSamplesPerSecond.Maximum = rbADC12Bit.Checked ? 504201 : 800000; // Limit samples per second                
                chkPreventJitter.Tag = chkPreventJitter.Checked; // Store current value
                chkPreventJitter.Checked = false;
                chkPreventJitter.Enabled = false;
            }
            else
            {
                tbSamplesPerSecond.Maximum = 1000000;
                chkPreventJitter.Checked = chkPreventJitter.Tag == null || (bool)chkPreventJitter.Tag; // Store current value
                chkPreventJitter.Checked = true;
                chkPreventJitter.Enabled = true;
            }
        }

        private void cmbGeneratorWaveform_SelectedIndexChanged(object sender, EventArgs e)
        {
            _signalGenator.Waveform = (GeneratorWaveforms)cmbGeneratorWaveform.SelectedIndex + 1;
        }

        private void btnSetGeneratorFreq_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGeneratorFreq.Text))
                return;
            int frequency;
            if (int.TryParse(txtGeneratorFreq.Text, out frequency))            
                _signalGenator.Frequency = frequency;
        }

        private void btnFindDevices_Click(object sender, EventArgs e)
        {
            frmWaitFindDevices dialog = new frmWaitFindDevices();
            dialog.Show(this);
            IList<DeviceNetworkAddress> devices = NetworkScanner.FindIpAddress(_macAddresses, _staticIpAddresses, Port).ToList();            
            dialog.Close();
            dialog.Dispose();

            cmbIPAddresses.Items.Clear();            
            if (devices.Any())
            {                                                
                //cmbIPAddresses.DataSource = new BindingSource(devices.Select(d => new ComboboxItem { Text = d.IPAddress.ToString(), Value = d.IPAddress.ToString() }).ToList(), null);                
                foreach (string address in devices.Select(d => d.IPAddress.ToString()))                
                    cmbIPAddresses.Items.Add(address);
                
                if (devices.Count > 1)
                {
                    cmbIPAddresses.Text = "Select an IP address";
                    MessageBox.Show(
                    "Found more than 1 device on your local network. Please select one in the list and click the 'Connect' button.",
                    "Multiple devices found", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(
                    "Found 1 device on your local network. Click the 'Connect button' to connect to it.",
                    "Device found", MessageBoxButtons.OK);
                }
            }
            else
            {
                cmbIPAddresses.Items.Add(new ComboboxItem {Text = "Click 'Find'", Value = string.Empty});
                MessageBox.Show(
                    "Could not find any device. Be sure the Network Analyzer is powered on and connected to the network. When using DHCP it might take a while before the Network Analyzer has obtained an IP address.",
                    "No device found", MessageBoxButtons.OK);
            }            
            cmbIPAddresses.SelectedIndex = 0;                        
        }

     
        /// <summary>
        /// Ip address is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbIPAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {                        
            if (cmbIPAddresses.SelectedIndex > -1)
            {
                string ip = cmbIPAddresses.Text;
                btnConnect.Enabled = !string.IsNullOrEmpty(ip);
                if (btnConnect.Enabled)
                {
                    ipAddress = ip;
                    Properties.Settings.Default.IPanalyzer = ip;
                    Properties.Settings.Default.Save(); //persist ipaddress that works
                }
            }
            else
                btnConnect.Enabled = false;
        }

        
        private void ResetControlFocus(object sender, EventArgs e)
        {            
            if (txtSamplesPerSecond.Focused || txtNoOfSamples.Focused)
                ActiveControl = null;
        }

        private void chkDrawMaxTrace_CheckedChanged(object sender, EventArgs e)
        {
            ScopeData.ResetMaxTrace();
        }

        private void chkDrawMinTrace_CheckedChanged(object sender, EventArgs e)
        {
            ScopeData.ResetMinTrace();
        }

        private void rbDbm_CheckedChanged(object sender, EventArgs e)
        {
            rbVrms.Checked = !rbDbm.Checked;
            spectrum.ChartAreas[0].AxisY.Minimum = rbDbm.Checked ? -120 : double.NaN;
        }
    }
    
}
