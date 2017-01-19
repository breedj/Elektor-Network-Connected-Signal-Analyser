namespace SignalAnalyzerApplication
{
    partial class SyntheticGeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.spectrum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Window30dB = new System.Windows.Forms.RadioButton();
            this.WindowBlackman = new System.Windows.Forms.RadioButton();
            this.WindowHamming = new System.Windows.Forms.RadioButton();
            this.WindowHanning = new System.Windows.Forms.RadioButton();
            this.WindowWelch = new System.Windows.Forms.RadioButton();
            this.WindowParzen = new System.Windows.Forms.RadioButton();
            this.WindowNone = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioVisDots = new System.Windows.Forms.RadioButton();
            this.radioVisLine = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtNoOfSamples = new System.Windows.Forms.TextBox();
            this.txtSamplesPerSecond = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbNoOfSamples = new System.Windows.Forms.TrackBar();
            this.tbSamplesPerSecond = new System.Windows.Forms.TrackBar();
            this.dataIn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.lblScopeCursor = new System.Windows.Forms.Label();
            this.lblFFTCursor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tctFormula = new System.Windows.Forms.TextBox();
            this.pnlVariables = new System.Windows.Forms.FlowLayoutPanel();
            this.btnParse = new System.Windows.Forms.Button();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmbSelectFormula = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVariableMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNoOfSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamplesPerSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIn)).BeginInit();
            this.SuspendLayout();
            // 
            // spectrum
            // 
            this.spectrum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.AxisX.IsStartedFromZero = false;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisX.Title = "Frequency-Hz";
            chartArea3.AxisY.Minimum = -120D;
            chartArea3.AxisY.Title = "Power-dBm";
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.Name = "ChartArea1";
            this.spectrum.ChartAreas.Add(chartArea3);
            this.spectrum.Location = new System.Drawing.Point(12, 259);
            this.spectrum.Name = "spectrum";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series3.Points.Add(dataPoint9);
            this.spectrum.Series.Add(series3);
            this.spectrum.Size = new System.Drawing.Size(1146, 262);
            this.spectrum.TabIndex = 2;
            title3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "FREQUENCY SPECTRUM";
            this.spectrum.Titles.Add(title3);
            this.spectrum.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.spectrum_CursorPositionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Window30dB);
            this.groupBox1.Controls.Add(this.WindowBlackman);
            this.groupBox1.Controls.Add(this.WindowHamming);
            this.groupBox1.Controls.Add(this.WindowHanning);
            this.groupBox1.Controls.Add(this.WindowWelch);
            this.groupBox1.Controls.Add(this.WindowParzen);
            this.groupBox1.Controls.Add(this.WindowNone);
            this.groupBox1.Location = new System.Drawing.Point(1168, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 182);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Type";
            // 
            // Window30dB
            // 
            this.Window30dB.AutoSize = true;
            this.Window30dB.Location = new System.Drawing.Point(6, 157);
            this.Window30dB.Name = "Window30dB";
            this.Window30dB.Size = new System.Drawing.Size(81, 17);
            this.Window30dB.TabIndex = 6;
            this.Window30dB.Text = "Steep-30dB";
            this.Window30dB.UseVisualStyleBackColor = true;
            this.Window30dB.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowBlackman
            // 
            this.WindowBlackman.AutoSize = true;
            this.WindowBlackman.Location = new System.Drawing.Point(6, 134);
            this.WindowBlackman.Name = "WindowBlackman";
            this.WindowBlackman.Size = new System.Drawing.Size(72, 17);
            this.WindowBlackman.TabIndex = 5;
            this.WindowBlackman.Text = "Blackman";
            this.WindowBlackman.UseVisualStyleBackColor = true;
            this.WindowBlackman.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowHamming
            // 
            this.WindowHamming.AutoSize = true;
            this.WindowHamming.Location = new System.Drawing.Point(6, 111);
            this.WindowHamming.Name = "WindowHamming";
            this.WindowHamming.Size = new System.Drawing.Size(69, 17);
            this.WindowHamming.TabIndex = 4;
            this.WindowHamming.Text = "Hamming";
            this.WindowHamming.UseVisualStyleBackColor = true;
            this.WindowHamming.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowHanning
            // 
            this.WindowHanning.AutoSize = true;
            this.WindowHanning.Location = new System.Drawing.Point(6, 88);
            this.WindowHanning.Name = "WindowHanning";
            this.WindowHanning.Size = new System.Drawing.Size(65, 17);
            this.WindowHanning.TabIndex = 3;
            this.WindowHanning.Text = "Hanning";
            this.WindowHanning.UseVisualStyleBackColor = true;
            this.WindowHanning.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowWelch
            // 
            this.WindowWelch.AutoSize = true;
            this.WindowWelch.Location = new System.Drawing.Point(6, 65);
            this.WindowWelch.Name = "WindowWelch";
            this.WindowWelch.Size = new System.Drawing.Size(56, 17);
            this.WindowWelch.TabIndex = 2;
            this.WindowWelch.Text = "Welch";
            this.WindowWelch.UseVisualStyleBackColor = true;
            this.WindowWelch.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowParzen
            // 
            this.WindowParzen.AutoSize = true;
            this.WindowParzen.Location = new System.Drawing.Point(6, 42);
            this.WindowParzen.Name = "WindowParzen";
            this.WindowParzen.Size = new System.Drawing.Size(58, 17);
            this.WindowParzen.TabIndex = 1;
            this.WindowParzen.Text = "Parzen";
            this.WindowParzen.UseVisualStyleBackColor = true;
            this.WindowParzen.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // WindowNone
            // 
            this.WindowNone.AutoSize = true;
            this.WindowNone.Checked = true;
            this.WindowNone.Location = new System.Drawing.Point(6, 19);
            this.WindowNone.Name = "WindowNone";
            this.WindowNone.Size = new System.Drawing.Size(92, 17);
            this.WindowNone.TabIndex = 0;
            this.WindowNone.TabStop = true;
            this.WindowNone.Text = "No windowing";
            this.WindowNone.UseVisualStyleBackColor = true;
            this.WindowNone.Click += new System.EventHandler(this.WindowType_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioVisDots);
            this.groupBox5.Controls.Add(this.radioVisLine);
            this.groupBox5.Location = new System.Drawing.Point(1168, 332);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(188, 43);
            this.groupBox5.TabIndex = 73;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Visualisation";
            // 
            // radioVisDots
            // 
            this.radioVisDots.AutoSize = true;
            this.radioVisDots.Location = new System.Drawing.Point(66, 19);
            this.radioVisDots.Name = "radioVisDots";
            this.radioVisDots.Size = new System.Drawing.Size(47, 17);
            this.radioVisDots.TabIndex = 1;
            this.radioVisDots.Text = "Dots";
            this.radioVisDots.UseVisualStyleBackColor = true;
            this.radioVisDots.Click += new System.EventHandler(this.visualisation_Click);
            // 
            // radioVisLine
            // 
            this.radioVisLine.AutoSize = true;
            this.radioVisLine.Checked = true;
            this.radioVisLine.Location = new System.Drawing.Point(13, 19);
            this.radioVisLine.Name = "radioVisLine";
            this.radioVisLine.Size = new System.Drawing.Size(45, 17);
            this.radioVisLine.TabIndex = 0;
            this.radioVisLine.TabStop = true;
            this.radioVisLine.Text = "Line";
            this.radioVisLine.UseVisualStyleBackColor = true;
            this.radioVisLine.Click += new System.EventHandler(this.visualisation_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.txtNoOfSamples);
            this.groupBox7.Controls.Add(this.txtSamplesPerSecond);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.tbNoOfSamples);
            this.groupBox7.Controls.Add(this.tbSamplesPerSecond);
            this.groupBox7.Location = new System.Drawing.Point(1168, 7);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(188, 131);
            this.groupBox7.TabIndex = 37;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sampling";
            // 
            // txtNoOfSamples
            // 
            this.txtNoOfSamples.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoOfSamples.Location = new System.Drawing.Point(113, 107);
            this.txtNoOfSamples.MaxLength = 6;
            this.txtNoOfSamples.Name = "txtNoOfSamples";
            this.txtNoOfSamples.Size = new System.Drawing.Size(67, 13);
            this.txtNoOfSamples.TabIndex = 92;
            this.txtNoOfSamples.Text = "5042";
            this.txtNoOfSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfSamples.WordWrap = false;
            this.txtNoOfSamples.TextChanged += new System.EventHandler(this.txtNoOfSamples_TextChanged);
            this.txtNoOfSamples.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkDigit_KeyPress);
            // 
            // txtSamplesPerSecond
            // 
            this.txtSamplesPerSecond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSamplesPerSecond.Location = new System.Drawing.Point(113, 51);
            this.txtSamplesPerSecond.MaxLength = 6;
            this.txtSamplesPerSecond.Name = "txtSamplesPerSecond";
            this.txtSamplesPerSecond.Size = new System.Drawing.Size(67, 13);
            this.txtSamplesPerSecond.TabIndex = 91;
            this.txtSamplesPerSecond.Text = "504201";
            this.txtSamplesPerSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSamplesPerSecond.WordWrap = false;
            this.txtSamplesPerSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSamplesPerSecond_KeyDown);
            this.txtSamplesPerSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkDigit_KeyPress);
            this.txtSamplesPerSecond.Leave += new System.EventHandler(this.txtSamplesPerSecond_Leave);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(12, 105);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 13);
            this.label24.TabIndex = 78;
            this.label24.Text = "No. of samples";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(12, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 77;
            this.label19.Text = "Samples/sec";
            // 
            // tbNoOfSamples
            // 
            this.tbNoOfSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNoOfSamples.BackColor = System.Drawing.Color.White;
            this.tbNoOfSamples.LargeChange = 1024;
            this.tbNoOfSamples.Location = new System.Drawing.Point(9, 75);
            this.tbNoOfSamples.Maximum = 20000;
            this.tbNoOfSamples.Minimum = 1000;
            this.tbNoOfSamples.Name = "tbNoOfSamples";
            this.tbNoOfSamples.Size = new System.Drawing.Size(173, 45);
            this.tbNoOfSamples.SmallChange = 128;
            this.tbNoOfSamples.TabIndex = 76;
            this.tbNoOfSamples.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbNoOfSamples.Value = 5042;
            this.tbNoOfSamples.ValueChanged += new System.EventHandler(this.tbNoOfSamples_ValueChanged);
            // 
            // tbSamplesPerSecond
            // 
            this.tbSamplesPerSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSamplesPerSecond.BackColor = System.Drawing.Color.White;
            this.tbSamplesPerSecond.LargeChange = 10000;
            this.tbSamplesPerSecond.Location = new System.Drawing.Point(9, 20);
            this.tbSamplesPerSecond.Maximum = 1000000;
            this.tbSamplesPerSecond.Minimum = 10119;
            this.tbSamplesPerSecond.Name = "tbSamplesPerSecond";
            this.tbSamplesPerSecond.Size = new System.Drawing.Size(173, 45);
            this.tbSamplesPerSecond.SmallChange = 1000;
            this.tbSamplesPerSecond.TabIndex = 75;
            this.tbSamplesPerSecond.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbSamplesPerSecond.Value = 504201;
            this.tbSamplesPerSecond.ValueChanged += new System.EventHandler(this.tbSamplesPerSecond_ValueChanged);
            // 
            // dataIn
            // 
            this.dataIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.AxisX.Minimum = 0D;
            chartArea4.AxisX.Title = "Time-seconds";
            chartArea4.AxisY.Title = "Amplitude-volts";
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.CursorY.IsUserEnabled = true;
            chartArea4.Name = "ChartArea1";
            this.dataIn.ChartAreas.Add(chartArea4);
            this.dataIn.Location = new System.Drawing.Point(12, 7);
            this.dataIn.Name = "dataIn";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.MarkerSize = 1;
            series4.Name = "Series1";
            series4.Points.Add(dataPoint10);
            series4.Points.Add(dataPoint11);
            series4.Points.Add(dataPoint12);
            this.dataIn.Series.Add(series4);
            this.dataIn.Size = new System.Drawing.Size(1146, 246);
            this.dataIn.TabIndex = 1;
            title4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "Title1";
            title4.Text = "INPUT SIGNAL";
            this.dataIn.Titles.Add(title4);
            this.dataIn.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.dataIn_CursorPositionChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(107, -96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 100;
            this.label6.Text = "Cursor:";
            // 
            // lblScopeCursor
            // 
            this.lblScopeCursor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScopeCursor.AutoSize = true;
            this.lblScopeCursor.BackColor = System.Drawing.Color.White;
            this.lblScopeCursor.Location = new System.Drawing.Point(150, 18);
            this.lblScopeCursor.Name = "lblScopeCursor";
            this.lblScopeCursor.Size = new System.Drawing.Size(13, 13);
            this.lblScopeCursor.TabIndex = 101;
            this.lblScopeCursor.Text = "?";
            // 
            // lblFFTCursor
            // 
            this.lblFFTCursor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFFTCursor.AutoSize = true;
            this.lblFFTCursor.BackColor = System.Drawing.Color.White;
            this.lblFFTCursor.Location = new System.Drawing.Point(150, 264);
            this.lblFFTCursor.Name = "lblFFTCursor";
            this.lblFFTCursor.Size = new System.Drawing.Size(13, 13);
            this.lblFFTCursor.TabIndex = 103;
            this.lblFFTCursor.Text = "?";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(107, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Cursor:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(107, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Cursor:";
            // 
            // tctFormula
            // 
            this.tctFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tctFormula.Location = new System.Drawing.Point(83, 552);
            this.tctFormula.Name = "tctFormula";
            this.tctFormula.Size = new System.Drawing.Size(851, 22);
            this.tctFormula.TabIndex = 106;
            this.tctFormula.TextChanged += new System.EventHandler(this.tctFormula_TextChanged);
            this.tctFormula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tctFormula_KeyDown);
            // 
            // pnlVariables
            // 
            this.pnlVariables.Location = new System.Drawing.Point(83, 580);
            this.pnlVariables.Name = "pnlVariables";
            this.pnlVariables.Size = new System.Drawing.Size(851, 120);
            this.pnlVariables.TabIndex = 108;
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(943, 552);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 109;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.BackColor = System.Drawing.Color.GreenYellow;
            this.btnEvaluate.Enabled = false;
            this.btnEvaluate.Location = new System.Drawing.Point(1024, 552);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 23);
            this.btnEvaluate.TabIndex = 110;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = false;
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(940, 580);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(261, 127);
            this.lblMessage.TabIndex = 111;
            // 
            // cmbSelectFormula
            // 
            this.cmbSelectFormula.FormattingEnabled = true;
            this.cmbSelectFormula.Location = new System.Drawing.Point(83, 527);
            this.cmbSelectFormula.Name = "cmbSelectFormula";
            this.cmbSelectFormula.Size = new System.Drawing.Size(160, 21);
            this.cmbSelectFormula.TabIndex = 112;
            this.cmbSelectFormula.Text = "Select a waveform";
            this.cmbSelectFormula.SelectedIndexChanged += new System.EventHandler(this.cmbSelectFormula_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(293, 530);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 18);
            this.label3.TabIndex = 113;
            this.label3.Text = "Use t for time and PI for 3.14....";
            // 
            // lblVariableMessage
            // 
            this.lblVariableMessage.AutoSize = true;
            this.lblVariableMessage.ForeColor = System.Drawing.Color.Red;
            this.lblVariableMessage.Location = new System.Drawing.Point(80, 703);
            this.lblVariableMessage.Name = "lblVariableMessage";
            this.lblVariableMessage.Size = new System.Drawing.Size(0, 13);
            this.lblVariableMessage.TabIndex = 114;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1220, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 115;
            this.button1.Text = "Help";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SyntheticGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(1368, 719);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblVariableMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSelectFormula);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnEvaluate);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.pnlVariables);
            this.Controls.Add(this.tctFormula);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFFTCursor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblScopeCursor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.spectrum);
            this.Controls.Add(this.dataIn);
            this.Name = "SyntheticGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synthetic Waveform Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.ResetControlFocus);
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNoOfSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamplesPerSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart spectrum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Window30dB;
        private System.Windows.Forms.RadioButton WindowBlackman;
        private System.Windows.Forms.RadioButton WindowHamming;
        private System.Windows.Forms.RadioButton WindowHanning;
        private System.Windows.Forms.RadioButton WindowWelch;
        private System.Windows.Forms.RadioButton WindowParzen;
        private System.Windows.Forms.RadioButton WindowNone;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioVisLine;
        private System.Windows.Forms.RadioButton radioVisDots;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TrackBar tbNoOfSamples;
        private System.Windows.Forms.TrackBar tbSamplesPerSecond;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataIn;
        private System.Windows.Forms.TextBox txtNoOfSamples;
        private System.Windows.Forms.TextBox txtSamplesPerSecond;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblScopeCursor;
        private System.Windows.Forms.Label lblFFTCursor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tctFormula;
        private System.Windows.Forms.FlowLayoutPanel pnlVariables;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cmbSelectFormula;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblVariableMessage;
        private System.Windows.Forms.Button button1;
    }
}

