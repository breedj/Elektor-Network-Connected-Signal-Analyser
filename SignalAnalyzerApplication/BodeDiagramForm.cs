using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Elektor.SignalAnalyzer;

namespace SignalAnalyzerApplication
{
    public partial class BodeDiagramForm : Form
    {
        private const int Port = 4000;
        private readonly string[] _macAddresses = { "00:08:dc:00:ab:cd", "02:08:dc:00:ab:cd" };   // Find devices by these mac adresses when using DHCP
        private readonly string[] _staticIpAddresses = { "192.168.1.123", "192.168.0.123" }; // Find devices by these IP addresses when using static IP
        private readonly ClientConnection _connection = new ClientConnection();
        private readonly SignalAnalyzer _analyzer = new SignalAnalyzer();
        private readonly SignalGenerator _generator = new SignalGenerator();
        private string _ipAddress = string.Empty; // for now fixed. Put back detection funcationality.
        private bool _isCalibrating = false; // Is calibrating if true
        private Dictionary<int, double> _calibrationData = new Dictionary<int, double>();
        
        public BodeDiagramForm()
        {
            InitializeComponent();

            cmbIPAddresses.DisplayMember = "Text";
            cmbIPAddresses.ValueMember = "Value";

            _analyzer.Connection = _connection;
            _analyzer.TriggerLevel = 0;
            _analyzer.SamplesCount = 1000;
            _analyzer.SamplingSettings = new SamplingSettings { Fs = 504201, M = 60, N1 = 2, N2 = 2 };
            _analyzer.TriggerMode = TriggerModes.Normal;
            _analyzer.TriggerSlope = TriggerSlopes.RisingEdge;
            _analyzer.WindowType = WindowingTypes.Square;
            _analyzer.RenderScope += _analyzer_RenderScope;
            _analyzer.RenderFft += analyzer_RenderFft;
            _analyzer.ConversionAccuray = ADCBits.Bit10;

            spectrum.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            spectrum.Series["Series1"].Points.Clear();
            spectrum.Series["Series1"].IsXValueIndexed = false;

            _generator.Connection = _connection;
            LoadSettings();

            btnStartSweep.Enabled = false;
        }

        private void analyzer_RenderFft(object sender, FFTEventArgs e)
        {
            if (_isCalibrating)
                return;

            double dbm = e.FFTData.FreqDomain.Max();

            chartDbm.Series["Series1"].ChartArea = "ChartArea1";
            chartDbm.ChartAreas[0].AxisX.IsLogarithmic = true;
            chartDbm.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chartDbm.ChartAreas[0].AxisX.Minimum = 10;
            chartDbm.ChartAreas[0].AxisX.Maximum = 25000;
            chartDbm.Series["Series1"].IsXValueIndexed = false;
            chartDbm.Series["Series1"].ChartType = SeriesChartType.Line;
            chartDbm.Series["Series1"].MarkerSize = 3;
            chartDbm.ChartAreas["ChartArea1"].CursorX.Interval = 1.0;
            chartDbm.Series["Series1"].Points.AddXY(_generator.Frequency, dbm);          
        }

        private void _analyzer_RenderScope(object sender, ScopeEventArgs e)
        {
            if (e.ScopeData != null)
            {
                double v = 0;
                if (e.ScopeData.Triggered && e.ScopeData.TriggerSamples.Count > 4)
                {
                    // get peak voltage of multiple peaks
                    List<double> maxVoltages = new List<double>();
                    for (int i = 0; i < e.ScopeData.TriggerSamples.Count - 3; i++)
                    {
                        var chunk =
                            e.ScopeData.Voltages.Skip(e.ScopeData.TriggerSamples[i])
                                .Take(e.ScopeData.TriggerSamples[i + 1]);
                        maxVoltages.Add(chunk.Max());
                    }
                    v = maxVoltages.Average(); // Average of all peaks
                }
                else
                    v = e.ScopeData.Voltages.Max();

                if (_isCalibrating)
                    _calibrationData.Add(_generator.Frequency, v);
                else
                    v = 20*Math.Log10(v/_calibrationData[_generator.Frequency]);

                if (!_isCalibrating)
                {
                    spectrum.Series["Series1"].ChartArea = "ChartArea1";
                    spectrum.ChartAreas[0].AxisX.IsLogarithmic = true;
                    spectrum.ChartAreas[0].AxisX.IsStartedFromZero = false;
                    spectrum.ChartAreas[0].AxisX.Minimum = 10;
                    spectrum.ChartAreas[0].AxisX.Maximum = 25000;
                    spectrum.Series["Series1"].IsXValueIndexed = false;
                    spectrum.Series["Series1"].ChartType = SeriesChartType.Line;
                    spectrum.Series["Series1"].MarkerSize = 3;


                    spectrum.ChartAreas["ChartArea1"].CursorX.Interval = 1.0;
                    spectrum.Series["Series1"].Points.AddXY(_generator.Frequency, v);
                    // For this test take the maximum of the whole dataset.                              
                }
            }            
            Thread.Sleep(10);
            SetNextFrequency();
        }

        private void LoadSettings()
        {
            // Load ip address from settings
            _ipAddress = Properties.Settings.Default.IPanalyzer;
            if (!string.IsNullOrEmpty(_ipAddress))
            {
                cmbIPAddresses.Items.Add(new ComboboxItem { Text = _ipAddress, Value = _ipAddress });
                btnConnect.Enabled = true;
            }
            else
                cmbIPAddresses.Items.Add(new ComboboxItem { Text = "Click 'Find'", Value = string.Empty });
            cmbIPAddresses.SelectedIndex = 0;
        }

        private void btnStartSweep_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
                return;
            if (!_calibrationData.Any())
            {
                MessageBox.Show("Please calibrate first", "Calibrate", MessageBoxButtons.OK);
                return;
            }
            _isCalibrating = false;
            spectrum.ChartAreas[0].AxisX.IsLogarithmic = false;
            spectrum.Series["Series1"].Points.Clear();
            chartDbm.ChartAreas[0].AxisX.IsLogarithmic = false;
            chartDbm.Series["Series1"].Points.Clear();
            btnCalibrate.Enabled = false;
            btnStartSweep.Enabled = false;
            StartGenerator();
            SetNextFrequency();
        }

        

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
                return;
            _calibrationData.Clear();
            _isCalibrating = true;
            spectrum.ChartAreas[0].AxisX.IsLogarithmic = false;
            spectrum.Series["Series1"].Points.Clear();
            chartDbm.ChartAreas[0].AxisX.IsLogarithmic = false;
            chartDbm.Series["Series1"].Points.Clear();                        
            btnCalibrate.Enabled = false;
            btnStartSweep.Enabled = false;
            StartGenerator();
            SetNextFrequency();
        }

        /// <summary>
        /// Start the generator at 0 Hz
        /// </summary>
        private void StartGenerator()
        {
            _generator.Frequency = 0;
            _generator.Waveform = GeneratorWaveforms.Sinusoid;
            _generator.Enabled = true;
            Thread.Sleep(1000); // Give some time, otherwise it will stall
        }
        


        /// <summary>
        /// Measure the next frequency
        /// </summary>
        private void SetNextFrequency()
        {
            // Set to new frequency
            if (_generator.Frequency < 100)
                _generator.Frequency += 5;
            else if (_generator.Frequency < 1000)
                _generator.Frequency += 10;
            else if (_generator.Frequency < 10000)
                _generator.Frequency += 100;
            else if (_generator.Frequency < 100000)
                _generator.Frequency += 1000;

            if (_generator.Frequency > 20000) // When maximum frequency is reached, stop.
            {                
                _generator.Enabled = false;
                btnStartSweep.Enabled = true;
                btnCalibrate.Enabled = true;
                return;
            }

            // Calculate the sample rate and amount of samples
            int fs = _generator.Frequency*1000;
            if (fs > 504201) // Maximum Fs when generator is running.             
                fs = 504201;
            else
            {
                SamplingSettings settings = _analyzer.GetSpurFreeSampleFrequency(fs, false);
                fs = settings.Fs;
            }
            int sampleCount = fs/(int) Math.Ceiling(fs/20000.0); // Integer multiple of Fs but should be < 20000
            _analyzer.SamplesCount = sampleCount;
            lblFrequency.Text = $"{_generator.Frequency}Hz";
            _analyzer.SamplingSettings = new SamplingSettings {Fs = fs, ADCS = 4, M = 60, N1 = 2, N2 = 2};       
            _analyzer.AcquireState = AcquireStates.Immediate;
        }

        
        private void BodeDiagramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Disconnect();        
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
                cmbIPAddresses.DataSource = new BindingSource(devices.Select(d => new ComboboxItem { Text = d.IPAddress.ToString(), Value = d.IPAddress.ToString() }).ToList(), null);
                if (devices.Count > 1)
                {
                    cmbIPAddresses.Items.Insert(0,
                        new ComboboxItem { Text = "Select an IP address", Value = string.Empty });
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
                cmbIPAddresses.Items.Add(new ComboboxItem { Text = "Click 'Find'", Value = string.Empty });
                MessageBox.Show(
                    "Could not find any device. Be sure the Network Analyzer is powered on and connected to the network. When using DHCP it might take a while before the Network Analyzer has obtained an IP address.",
                    "No device found", MessageBoxButtons.OK);
            }
            cmbIPAddresses.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
            {
                _connection.Connect(_ipAddress, Port);
                if (_connection.IsConnected)
                {
                    btnConnect.Text = "Disconnect";
                    btnConnect.BackColor = Color.LightGreen;
                    cmbIPAddresses.Enabled = false;
                    btnFindDevices.Enabled = false;
                    btnStartSweep.Enabled = true;
                    btnCalibrate.Enabled = true;
                    btnStartSweep.Enabled = true;
                }
                else
                    MessageBox.Show("Could not connect.", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnStartSweep.Enabled = false;
                _analyzer.Stop();
                _connection.Disconnect();
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Gainsboro;
                _analyzer.AcquireState = AcquireStates.Stopped;
                cmbIPAddresses.Enabled = true;
                btnFindDevices.Enabled = true;
                btnCalibrate.Enabled = false;
                btnStartSweep.Enabled = false;
            }


        }

        
    }    
}
