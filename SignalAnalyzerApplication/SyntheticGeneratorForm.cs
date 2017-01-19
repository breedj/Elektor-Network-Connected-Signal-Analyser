using System;
using System.Collections;
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
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using NCalc;

namespace SignalAnalyzerApplication
{

    public partial class SyntheticGeneratorForm : Form
    {
        private readonly FftAnalyzer _fftAnalyzer = new FftAnalyzer();

        public SyntheticGeneratorForm()
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

                    if (ModifierKeys == Keys.Shift)
                        dataIn.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                    else
                        dataIn.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch { }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            dataIn.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            dataIn.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            dataIn.ChartAreas[0].CursorX.AutoScroll = true;
            dataIn.ChartAreas[0].CursorY.AutoScroll = true;
            dataIn.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            dataIn.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            dataIn.ChartAreas[0].CursorX.IsUserEnabled = true;
            dataIn.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            dataIn.ChartAreas[0].CursorY.IsUserEnabled = true;
            dataIn.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            dataIn.MouseWheel += chart_MouseWheel;
            dataIn.CursorPositionChanged += dataIn_CursorPositionChanged;
            dataIn.Series["Series1"].Points.Clear();

            spectrum.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            spectrum.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            spectrum.ChartAreas[0].CursorX.AutoScroll = true;
            spectrum.ChartAreas[0].CursorY.AutoScroll = true;
            spectrum.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            spectrum.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            spectrum.ChartAreas[0].CursorX.IsUserEnabled = true;
            spectrum.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            spectrum.ChartAreas[0].CursorY.IsUserEnabled = true;
            spectrum.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            spectrum.MouseWheel += chart_MouseWheel;
            spectrum.CursorPositionChanged += spectrum_CursorPositionChanged;
            spectrum.Series["Series1"].Points.Clear();
            
            groupBox1.Click += ResetControlFocus;
            groupBox5.Click += ResetControlFocus;
            groupBox7.Click += ResetControlFocus;

            _fftAnalyzer.FftReady += _fftAnalyzer_FFTReady;

            cmbSelectFormula.Items.Add(new ComboboxItem {Text = "AM modulated", Value = "(1 + (modlevel / 100) * Cos(2 * PI * fm * t)) * Cos(2 * PI * fc * t)" });
            cmbSelectFormula.Items.Add(new ComboboxItem { Text = "Square wave", Value = "4 / PI * (Cos(2 * PI * fc * t) - .3333 * Cos(2 * PI * 3 * fc * t) + .2 * Cos(2 * PI * 5 * fc * t) - .1429 * Cos(2 * PI * 7 * fc * t) + .1111 * Cos(2 * PI * 9 * fc * t))" });
            cmbSelectFormula.Items.Add(new ComboboxItem { Text = "PM modulated", Value = "Sin(2 * PI * fc * t + PI * (modlevel / 100) * Cos(2 * PI * fm * t))" });
            cmbSelectFormula.Items.Add(new ComboboxItem { Text = "Test Signal", Value = "Sin(2 * PI * 1000 * t) + .5 * Sin(2 * PI * 2000 * t + 3 * PI / 4)" });  //see "Understanding Digital Signal Processing", Lyons, p63            

            // Load a default signal
            cmbSelectFormula.SelectedIndex = 3;
            Evaluate();
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
        private void spectrum_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            ushort index = (ushort)((spectrum.ChartAreas["ChartArea1"].CursorX.Position) /   //find index of chart data point closest to cursor position
                                    (spectrum.ChartAreas["ChartArea1"].CursorX.Interval));

            string xString = "";
            string rmsString = "";
            double y = 0;
            if (spectrum.Series[0].Points.Count >= index)
            {
                double x = spectrum.Series[0].Points[index].XValue;
                y = spectrum.Series[0].Points[index].YValues[0];

                spectrum.ChartAreas["ChartArea1"].CursorY.Position = y;
                
                if (x > 1E3)
                    xString = $"{(x / 1E3).ToString("F3")} kHz";
                else
                    xString = $"{x.ToString("F3")} Hz";

                double vrms = Math.Sqrt(Math.Pow(10, (y / 10)) * .05);                
                if (vrms < 1E-3)
                    rmsString = $"{(vrms * 1E6).ToString("F3")} μVrms";
                else if (vrms < 1)
                    rmsString = $"{(vrms * 1E3).ToString("F3")} mVrms";
                else
                    rmsString = $"{vrms.ToString("F3")} Vrms";
            }
            lblFFTCursor.Text = $"{y.ToString("F3")} dBm / {rmsString} @ {xString}";            
        }
        

        /// <summary>
        /// Event when closing the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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
        /// Selected fft window type changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowType_Click(object sender, EventArgs e)
        {
            Evaluate();
        }

        /// <summary>
        /// Samples per second changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSamplesPerSecond_ValueChanged(object sender, EventArgs e)
        {
            txtSamplesPerSecond.Text = tbSamplesPerSecond.Value.ToString();
            Evaluate();
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
                Evaluate();
            }
        }

        
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
                    Evaluate();
                }
            }            
        }

        private void tbNoOfSamples_ValueChanged(object sender, EventArgs e)
        {        
            txtNoOfSamples.Text = tbNoOfSamples.Value.ToString();
            Evaluate();
        }

        private void txtNoOfSamples_TextChanged(object sender, EventArgs e)
        {
            if (txtNoOfSamples.Text.Length == 0)
                return;
            int value;
            if (int.TryParse(txtNoOfSamples.Text, out value)
                && value >= tbNoOfSamples.Minimum
                && value <= tbNoOfSamples.Maximum)
                tbNoOfSamples.Value = value;
        }



        /// <summary>
        /// Render scope data
        /// </summary>
        /// <param name="data"></param>
        void RenderScopeData(double[] data)
        {           
            try
            {
                dataIn.ChartAreas["ChartArea1"].CursorX.Interval = 1.0 / SamplesPerSecond;            //sets Xcursor step size to sample time            
                dataIn.Series.RemoveAt(0);
                dataIn.Series.Add("Series1");
                dataIn.Series["Series1"].ChartArea = "ChartArea1";                    
                dataIn.Series["Series1"].ChartType = radioVisLine.Checked ? SeriesChartType.Line : SeriesChartType.FastPoint;
                dataIn.Series["Series1"].MarkerSize = 1;

                if (data != null)
                {
                    double interval = 1/(double) SamplesPerSecond;
                    var yPoints = data.ToArray();
                    var xPoints = Enumerable.Range(0, yPoints.Count()).Select(s => s * interval).ToArray();                            
                    
                    // Draw points
                    dataIn.Series["Series1"].Points.DataBindXY(xPoints, yPoints);                    
                }                                    
            }
            catch (Exception)
            {
            } 
        }

        private void _fftAnalyzer_FFTReady(object sender, FFTEventArgs e)
        {
            RenderFFTData(e.FFTData.FreqDomain);
        }

        void RenderFFTData(double[] data)
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
                    double[] yPoints = data;
                    double[] xPoints = Enumerable.Range(0, yPoints.Count()).Select(s => s * (double)SamplesPerSecond / data.Length / 2).ToArray();
                       
                    spectrum.ChartAreas["ChartArea1"].CursorX.Interval = SamplesPerSecond / (double)data.Length / 2.0;        //matches cursor step size to X axis step size
                    spectrum.Series["Series1"].Points.DataBindXY(xPoints, yPoints);                        
                }                                    
            }
            catch (Exception) { }            
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
        
        
        private void ResetControlFocus(object sender, EventArgs e)
        {            
            if (txtSamplesPerSecond.Focused || txtNoOfSamples.Focused)
                ActiveControl = null;
        }


        /// <summary>
        /// Samples per second
        /// </summary>
        private int SamplesPerSecond => tbSamplesPerSecond.Value;

  
        private WindowingTypes WindowType
        {
            get
            {                
                WindowingTypes result = WindowingTypes.Square;               
                if (WindowNone.Checked) result = WindowingTypes.Square;
                else if (WindowParzen.Checked) result = WindowingTypes.Parzen;
                else if (WindowWelch.Checked) result = WindowingTypes.Welch;
                else if (WindowHanning.Checked) result = WindowingTypes.Hanning;
                else if (WindowHamming.Checked) result = WindowingTypes.Hamming;
                else if (WindowBlackman.Checked) result = WindowingTypes.Blackman;
                else if (Window30dB.Checked) result = WindowingTypes.Steeper30db;
                return result;
            }
        }

    
        private void tctFormula_TextChanged(object sender, EventArgs args)
        {
            
        }

        private void tctFormula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetVariables();
            }
        }


        private void GetVariables()
        {            
            foreach (Control ctrl in pnlVariables.Controls)
                ctrl.Visible = false;

            string message;
            var extractedParameters = SyntheticGenerator.ExtractVariables(tctFormula.Text, out message);
            if (message != null)
                lblMessage.Text = message;
            else
            {
                lblMessage.Text = "";
                foreach (
                    var param in
                        extractedParameters.Where(p => p.ToLowerInvariant() != "pi" && p.ToLowerInvariant() != "t"))
                {
                    string key = $"var_{param}";
                    if (!pnlVariables.Controls.ContainsKey(key))
                    {
                        // Add new 
                        Panel pnl = new Panel
                        {
                            BorderStyle = BorderStyle.FixedSingle,
                            Name = key,
                            Tag = param,
                            Size = new Size(143, 29)
                        };
                        pnlVariables.Controls.Add(pnl);

                        Label lbl = new Label
                        {
                            Text = param,
                            AutoSize = true,
                            Location = new Point(8, 8),
                            Name = $"varl_{param}"
                        };
                        pnl.Controls.Add(lbl);

                        TextBox txt = new TextBox
                        {
                            Text = "",
                            Location = new Point(66, 3),
                            Size = new Size(67, 20),
                            Name = $"vart_{param}"
                        };
                        txt.TextChanged += Txt_TextChanged;
                        txt.KeyDown += Txt_KeyDown;
                        txt.KeyPress += Txt_KeyPress;
                        pnl.Controls.Add(txt);
                    }
                    else
                    {
                        pnlVariables.Controls[key].Visible = true;
                    }
                }
                CheckEnableRun();
            }
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // For now only digits allowed
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetVariables();
            }
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            CheckEnableRun();
            Evaluate();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            try
            {
                GetVariables();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void CheckEnableRun()
        {
            bool enable = true;
            foreach (Control ctrl in pnlVariables.Controls)
            {
                if (!ctrl.Visible) continue;
                Panel pnl = ctrl as Panel;
                if (pnl != null)
                {
                    if (string.IsNullOrEmpty(pnl.Controls[1].Text))
                    {
                        enable = false;
                        break;
                    }
                }
            }            
            btnEvaluate.Enabled = enable;
            if (!enable)
                lblVariableMessage.Text = "Please enter a value for each variable";
            else            
                lblVariableMessage.Text = "";
                
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            Evaluate();
        }


        private void Evaluate()
        {
            if (!btnEvaluate.Enabled)
                return;

            Hashtable parameters = new Hashtable();
            foreach (Control ctrl in pnlVariables.Controls)
            {
                if (ctrl.Visible)
                {
                    Panel pnl = ctrl as Panel;
                    if (pnl != null)
                    {
                        parameters.Add(pnl.Tag, pnl.Controls[1].Text);
                    }
                }
            }
            string message;
            double[] data = SyntheticGenerator.GenerateData(tbNoOfSamples.Value, tbSamplesPerSecond.Value, tctFormula.Text, parameters, out message);
            if (message != null)
            {
                lblMessage.Text = message;
                spectrum.Series[0].Points.Clear();
            }
            else
            {
                lblMessage.Text = "";
                FftAnalyzer.Windowing(tbNoOfSamples.Value, data, WindowType, 1, data, false);
                RenderScopeData(data);
                _fftAnalyzer.ExecuteFftAsync(data, SamplesPerSecond, 1, WindowingTypes.Square); // already windowed.            
            }
        }

        private void cmbSelectFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            tctFormula.Text = ((ComboboxItem)cmbSelectFormula.SelectedItem).Value.ToString();
            GetVariables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SyntheticGeneratorHelp frm = new SyntheticGeneratorHelp();
            frm.ShowDialog();
        }
    }
    
}
