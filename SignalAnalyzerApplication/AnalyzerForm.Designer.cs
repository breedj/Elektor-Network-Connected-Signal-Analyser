namespace SignalAnalyzerApplication
{
    partial class SygnalAnalyzerForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.spectrum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Window30dB = new System.Windows.Forms.RadioButton();
            this.WindowBlackman = new System.Windows.Forms.RadioButton();
            this.WindowHamming = new System.Windows.Forms.RadioButton();
            this.WindowHanning = new System.Windows.Forms.RadioButton();
            this.WindowWelch = new System.Windows.Forms.RadioButton();
            this.WindowParzen = new System.Windows.Forms.RadioButton();
            this.WindowNone = new System.Windows.Forms.RadioButton();
            this.btnStartAcquisition = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnResetTriggerLevel = new System.Windows.Forms.Button();
            this.lblTriggerLevel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbTriggerSlope = new System.Windows.Forms.ComboBox();
            this.cmbTriggerMode = new System.Windows.Forms.ComboBox();
            this.tbTriggerLevel = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDrawMaxTrace = new System.Windows.Forms.CheckBox();
            this.chkDrawMinTrace = new System.Windows.Forms.CheckBox();
            this.btnResetAverages = new System.Windows.Forms.Button();
            this.lblNumberOfAverages = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblPeekToPeekAvg = new System.Windows.Forms.Label();
            this.lblPeekToPeekCur = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblAverageAvg = new System.Windows.Forms.Label();
            this.lblAverageCur = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblMaxAvg = new System.Windows.Forms.Label();
            this.lblMaxCurrent = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblMinCurrent = new System.Windows.Forms.Label();
            this.lblMinAvg = new System.Windows.Forms.Label();
            this.btnSinleShot = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioVisDots = new System.Windows.Forms.RadioButton();
            this.radioVisLine = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbCouplingDC = new System.Windows.Forms.RadioButton();
            this.rbCouplingAC = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblADCSActualValue = new System.Windows.Forms.Label();
            this.cmbADCS = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSamplingSettings = new System.Windows.Forms.Label();
            this.txtNoOfSamples = new System.Windows.Forms.TextBox();
            this.txtSamplesPerSecond = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.rbADC12Bit = new System.Windows.Forms.RadioButton();
            this.rbADC10Bit = new System.Windows.Forms.RadioButton();
            this.lblOutOfSpec = new System.Windows.Forms.Label();
            this.chkPreventJitter = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbNoOfSamples = new System.Windows.Forms.TrackBar();
            this.tbSamplesPerSecond = new System.Windows.Forms.TrackBar();
            this.label29 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.udFFTAverages = new System.Windows.Forms.NumericUpDown();
            this.lblFFTResolution = new System.Windows.Forms.Label();
            this.lblFFTFrequency = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetGeneratorFreq = new System.Windows.Forms.Button();
            this.chkEnableSignalGenerator = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGeneratorFreq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGeneratorWaveform = new System.Windows.Forms.ComboBox();
            this.lblDCOffset = new System.Windows.Forms.Label();
            this.lblScopeFrequency = new System.Windows.Forms.Label();
            this.lblWaveformsSecond = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataIn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.lblScopeCursor = new System.Windows.Forms.Label();
            this.lblFFTCursor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbIPAddresses = new System.Windows.Forms.ComboBox();
            this.btnFindDevices = new System.Windows.Forms.Button();
            this.rbVrms = new System.Windows.Forms.RadioButton();
            this.rbDbm = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTriggerLevel)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNoOfSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamplesPerSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFFTAverages)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataIn)).BeginInit();
            this.SuspendLayout();
            // 
            // spectrum
            // 
            this.spectrum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Frequency-Hz";
            chartArea1.AxisY.Minimum = -120D;
            chartArea1.AxisY.Title = "Power-dBm";
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.spectrum.ChartAreas.Add(chartArea1);
            this.spectrum.Location = new System.Drawing.Point(12, 519);
            this.spectrum.Name = "spectrum";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            this.spectrum.Series.Add(series1);
            this.spectrum.Size = new System.Drawing.Size(1214, 338);
            this.spectrum.TabIndex = 2;
            title1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "FREQUENCY SPECTRUM";
            this.spectrum.Titles.Add(title1);
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
            this.groupBox1.Location = new System.Drawing.Point(1261, 530);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 182);
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
            // btnStartAcquisition
            // 
            this.btnStartAcquisition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartAcquisition.BackColor = System.Drawing.Color.Red;
            this.btnStartAcquisition.Enabled = false;
            this.btnStartAcquisition.Location = new System.Drawing.Point(1332, 36);
            this.btnStartAcquisition.Name = "btnStartAcquisition";
            this.btnStartAcquisition.Size = new System.Drawing.Size(45, 34);
            this.btnStartAcquisition.TabIndex = 42;
            this.btnStartAcquisition.Text = "Run";
            this.btnStartAcquisition.UseVisualStyleBackColor = false;
            this.btnStartAcquisition.Click += new System.EventHandler(this.btnStartAcquisition_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnResetTriggerLevel);
            this.groupBox3.Controls.Add(this.lblTriggerLevel);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cmbTriggerSlope);
            this.groupBox3.Controls.Add(this.cmbTriggerMode);
            this.groupBox3.Location = new System.Drawing.Point(1261, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 99);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trigger";
            // 
            // btnResetTriggerLevel
            // 
            this.btnResetTriggerLevel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnResetTriggerLevel.Location = new System.Drawing.Point(108, 72);
            this.btnResetTriggerLevel.Name = "btnResetTriggerLevel";
            this.btnResetTriggerLevel.Size = new System.Drawing.Size(47, 21);
            this.btnResetTriggerLevel.TabIndex = 72;
            this.btnResetTriggerLevel.Text = "Set 0V";
            this.btnResetTriggerLevel.UseVisualStyleBackColor = false;
            this.btnResetTriggerLevel.Click += new System.EventHandler(this.btnResetTriggerLevel_Click);
            // 
            // lblTriggerLevel
            // 
            this.lblTriggerLevel.AutoSize = true;
            this.lblTriggerLevel.Location = new System.Drawing.Point(61, 77);
            this.lblTriggerLevel.Name = "lblTriggerLevel";
            this.lblTriggerLevel.Size = new System.Drawing.Size(13, 13);
            this.lblTriggerLevel.TabIndex = 31;
            this.lblTriggerLevel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Level (V)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Slope";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Mode";
            // 
            // cmbTriggerSlope
            // 
            this.cmbTriggerSlope.FormattingEnabled = true;
            this.cmbTriggerSlope.Items.AddRange(new object[] {
            "Rising edge",
            "Falling edge"});
            this.cmbTriggerSlope.Location = new System.Drawing.Point(64, 48);
            this.cmbTriggerSlope.Name = "cmbTriggerSlope";
            this.cmbTriggerSlope.Size = new System.Drawing.Size(93, 21);
            this.cmbTriggerSlope.TabIndex = 1;
            this.cmbTriggerSlope.SelectedIndexChanged += new System.EventHandler(this.cmbTriggerSlope_SelectedIndexChanged);
            // 
            // cmbTriggerMode
            // 
            this.cmbTriggerMode.FormattingEnabled = true;
            this.cmbTriggerMode.Items.AddRange(new object[] {
            "Off",
            "Auto",
            "Normal"});
            this.cmbTriggerMode.Location = new System.Drawing.Point(64, 19);
            this.cmbTriggerMode.Name = "cmbTriggerMode";
            this.cmbTriggerMode.Size = new System.Drawing.Size(93, 21);
            this.cmbTriggerMode.TabIndex = 0;
            this.cmbTriggerMode.SelectedIndexChanged += new System.EventHandler(this.cmbTriggerMode_SelectedIndexChanged);
            // 
            // tbTriggerLevel
            // 
            this.tbTriggerLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTriggerLevel.BackColor = System.Drawing.Color.White;
            this.tbTriggerLevel.LargeChange = 10;
            this.tbTriggerLevel.Location = new System.Drawing.Point(1182, 100);
            this.tbTriggerLevel.Maximum = 250;
            this.tbTriggerLevel.Minimum = -250;
            this.tbTriggerLevel.Name = "tbTriggerLevel";
            this.tbTriggerLevel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbTriggerLevel.Size = new System.Drawing.Size(45, 413);
            this.tbTriggerLevel.TabIndex = 0;
            this.tbTriggerLevel.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbTriggerLevel.ValueChanged += new System.EventHandler(this.tbTriggerLevel_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkDrawMaxTrace);
            this.groupBox4.Controls.Add(this.chkDrawMinTrace);
            this.groupBox4.Controls.Add(this.btnResetAverages);
            this.groupBox4.Controls.Add(this.lblNumberOfAverages);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.lblPeekToPeekAvg);
            this.groupBox4.Controls.Add(this.lblPeekToPeekCur);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.lblAverageAvg);
            this.groupBox4.Controls.Add(this.lblAverageCur);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.lblMaxAvg);
            this.groupBox4.Controls.Add(this.lblMaxCurrent);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.lblMin);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.lblMinCurrent);
            this.groupBox4.Controls.Add(this.lblMinAvg);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1215, 84);
            this.groupBox4.TabIndex = 69;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Statistics";
            // 
            // chkDrawMaxTrace
            // 
            this.chkDrawMaxTrace.AutoSize = true;
            this.chkDrawMaxTrace.Location = new System.Drawing.Point(577, 32);
            this.chkDrawMaxTrace.Name = "chkDrawMaxTrace";
            this.chkDrawMaxTrace.Size = new System.Drawing.Size(100, 17);
            this.chkDrawMaxTrace.TabIndex = 84;
            this.chkDrawMaxTrace.Text = "Draw max trace";
            this.chkDrawMaxTrace.UseVisualStyleBackColor = true;
            this.chkDrawMaxTrace.CheckedChanged += new System.EventHandler(this.chkDrawMaxTrace_CheckedChanged);
            // 
            // chkDrawMinTrace
            // 
            this.chkDrawMinTrace.AutoSize = true;
            this.chkDrawMinTrace.Location = new System.Drawing.Point(577, 55);
            this.chkDrawMinTrace.Name = "chkDrawMinTrace";
            this.chkDrawMinTrace.Size = new System.Drawing.Size(97, 17);
            this.chkDrawMinTrace.TabIndex = 83;
            this.chkDrawMinTrace.Text = "Draw min trace";
            this.chkDrawMinTrace.UseVisualStyleBackColor = true;
            this.chkDrawMinTrace.CheckedChanged += new System.EventHandler(this.chkDrawMinTrace_CheckedChanged);
            // 
            // btnResetAverages
            // 
            this.btnResetAverages.Location = new System.Drawing.Point(477, 52);
            this.btnResetAverages.Name = "btnResetAverages";
            this.btnResetAverages.Size = new System.Drawing.Size(75, 23);
            this.btnResetAverages.TabIndex = 74;
            this.btnResetAverages.Text = "Reset";
            this.btnResetAverages.UseVisualStyleBackColor = true;
            this.btnResetAverages.Click += new System.EventHandler(this.btnResetAverages_Click);
            // 
            // lblNumberOfAverages
            // 
            this.lblNumberOfAverages.AutoSize = true;
            this.lblNumberOfAverages.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblNumberOfAverages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfAverages.Location = new System.Drawing.Point(517, 25);
            this.lblNumberOfAverages.Name = "lblNumberOfAverages";
            this.lblNumberOfAverages.Size = new System.Drawing.Size(10, 13);
            this.lblNumberOfAverages.TabIndex = 73;
            this.lblNumberOfAverages.Text = "-";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.DarkKhaki;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(482, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 72;
            this.label20.Text = "#Avg:";
            // 
            // lblPeekToPeekAvg
            // 
            this.lblPeekToPeekAvg.AutoSize = true;
            this.lblPeekToPeekAvg.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblPeekToPeekAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeekToPeekAvg.Location = new System.Drawing.Point(413, 59);
            this.lblPeekToPeekAvg.Name = "lblPeekToPeekAvg";
            this.lblPeekToPeekAvg.Size = new System.Drawing.Size(10, 13);
            this.lblPeekToPeekAvg.TabIndex = 71;
            this.lblPeekToPeekAvg.Text = "-";
            // 
            // lblPeekToPeekCur
            // 
            this.lblPeekToPeekCur.AutoSize = true;
            this.lblPeekToPeekCur.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblPeekToPeekCur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeekToPeekCur.Location = new System.Drawing.Point(413, 44);
            this.lblPeekToPeekCur.Name = "lblPeekToPeekCur";
            this.lblPeekToPeekCur.Size = new System.Drawing.Size(10, 13);
            this.lblPeekToPeekCur.TabIndex = 70;
            this.lblPeekToPeekCur.Text = "-";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.DarkKhaki;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(381, 59);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 13);
            this.label31.TabIndex = 69;
            this.label31.Text = "Avg:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.DarkKhaki;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(381, 44);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(26, 13);
            this.label32.TabIndex = 68;
            this.label32.Text = "Cur:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.DarkKhaki;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(381, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 13);
            this.label33.TabIndex = 67;
            this.label33.Text = "Pk-Pk";
            // 
            // lblAverageAvg
            // 
            this.lblAverageAvg.AutoSize = true;
            this.lblAverageAvg.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblAverageAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageAvg.Location = new System.Drawing.Point(273, 57);
            this.lblAverageAvg.Name = "lblAverageAvg";
            this.lblAverageAvg.Size = new System.Drawing.Size(10, 13);
            this.lblAverageAvg.TabIndex = 66;
            this.lblAverageAvg.Text = "-";
            // 
            // lblAverageCur
            // 
            this.lblAverageCur.AutoSize = true;
            this.lblAverageCur.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblAverageCur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageCur.Location = new System.Drawing.Point(273, 42);
            this.lblAverageCur.Name = "lblAverageCur";
            this.lblAverageCur.Size = new System.Drawing.Size(10, 13);
            this.lblAverageCur.TabIndex = 65;
            this.lblAverageCur.Text = "-";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.DarkKhaki;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(241, 57);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 13);
            this.label26.TabIndex = 64;
            this.label26.Text = "Avg:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.DarkKhaki;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(241, 42);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(26, 13);
            this.label27.TabIndex = 63;
            this.label27.Text = "Cur:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.DarkKhaki;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(241, 22);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 13);
            this.label28.TabIndex = 62;
            this.label28.Text = "Average";
            // 
            // lblMaxAvg
            // 
            this.lblMaxAvg.AutoSize = true;
            this.lblMaxAvg.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblMaxAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAvg.Location = new System.Drawing.Point(162, 57);
            this.lblMaxAvg.Name = "lblMaxAvg";
            this.lblMaxAvg.Size = new System.Drawing.Size(10, 13);
            this.lblMaxAvg.TabIndex = 61;
            this.lblMaxAvg.Text = "-";
            // 
            // lblMaxCurrent
            // 
            this.lblMaxCurrent.AutoSize = true;
            this.lblMaxCurrent.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblMaxCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxCurrent.Location = new System.Drawing.Point(162, 42);
            this.lblMaxCurrent.Name = "lblMaxCurrent";
            this.lblMaxCurrent.Size = new System.Drawing.Size(10, 13);
            this.lblMaxCurrent.TabIndex = 60;
            this.lblMaxCurrent.Text = "-";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.DarkKhaki;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(130, 57);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 13);
            this.label21.TabIndex = 59;
            this.label21.Text = "Avg:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.DarkKhaki;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(130, 42);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(26, 13);
            this.label22.TabIndex = 58;
            this.label22.Text = "Cur:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.DarkKhaki;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(130, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(30, 13);
            this.label23.TabIndex = 57;
            this.label23.Text = "Max";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(17, 22);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(27, 13);
            this.lblMin.TabIndex = 52;
            this.lblMin.Text = "Min";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.DarkKhaki;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(17, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 13);
            this.label17.TabIndex = 53;
            this.label17.Text = "Cur:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.DarkKhaki;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(17, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "Avg:";
            // 
            // lblMinCurrent
            // 
            this.lblMinCurrent.AutoSize = true;
            this.lblMinCurrent.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblMinCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinCurrent.Location = new System.Drawing.Point(49, 42);
            this.lblMinCurrent.Name = "lblMinCurrent";
            this.lblMinCurrent.Size = new System.Drawing.Size(10, 13);
            this.lblMinCurrent.TabIndex = 55;
            this.lblMinCurrent.Text = "-";
            // 
            // lblMinAvg
            // 
            this.lblMinAvg.AutoSize = true;
            this.lblMinAvg.BackColor = System.Drawing.Color.DarkKhaki;
            this.lblMinAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinAvg.Location = new System.Drawing.Point(49, 57);
            this.lblMinAvg.Name = "lblMinAvg";
            this.lblMinAvg.Size = new System.Drawing.Size(10, 13);
            this.lblMinAvg.TabIndex = 56;
            this.lblMinAvg.Text = "-";
            // 
            // btnSinleShot
            // 
            this.btnSinleShot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSinleShot.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSinleShot.Enabled = false;
            this.btnSinleShot.Location = new System.Drawing.Point(1379, 36);
            this.btnSinleShot.Name = "btnSinleShot";
            this.btnSinleShot.Size = new System.Drawing.Size(45, 34);
            this.btnSinleShot.TabIndex = 71;
            this.btnSinleShot.Text = "Single";
            this.btnSinleShot.UseVisualStyleBackColor = false;
            this.btnSinleShot.Click += new System.EventHandler(this.btnSinleShot_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(1259, 36);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(73, 34);
            this.btnConnect.TabIndex = 72;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioVisDots);
            this.groupBox5.Controls.Add(this.radioVisLine);
            this.groupBox5.Location = new System.Drawing.Point(1261, 118);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(163, 43);
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
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.rbCouplingDC);
            this.groupBox6.Controls.Add(this.rbCouplingAC);
            this.groupBox6.Location = new System.Drawing.Point(1261, 74);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(163, 43);
            this.groupBox6.TabIndex = 74;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Coupling";
            // 
            // rbCouplingDC
            // 
            this.rbCouplingDC.AutoSize = true;
            this.rbCouplingDC.Location = new System.Drawing.Point(66, 19);
            this.rbCouplingDC.Name = "rbCouplingDC";
            this.rbCouplingDC.Size = new System.Drawing.Size(40, 17);
            this.rbCouplingDC.TabIndex = 1;
            this.rbCouplingDC.Text = "DC";
            this.rbCouplingDC.UseVisualStyleBackColor = true;
            this.rbCouplingDC.Click += new System.EventHandler(this.rbCoupling_Click);
            // 
            // rbCouplingAC
            // 
            this.rbCouplingAC.AutoSize = true;
            this.rbCouplingAC.Checked = true;
            this.rbCouplingAC.Location = new System.Drawing.Point(13, 19);
            this.rbCouplingAC.Name = "rbCouplingAC";
            this.rbCouplingAC.Size = new System.Drawing.Size(39, 17);
            this.rbCouplingAC.TabIndex = 0;
            this.rbCouplingAC.TabStop = true;
            this.rbCouplingAC.Text = "AC";
            this.rbCouplingAC.UseVisualStyleBackColor = true;
            this.rbCouplingAC.Click += new System.EventHandler(this.rbCoupling_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.lblADCSActualValue);
            this.groupBox7.Controls.Add(this.cmbADCS);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.lblSamplingSettings);
            this.groupBox7.Controls.Add(this.txtNoOfSamples);
            this.groupBox7.Controls.Add(this.txtSamplesPerSecond);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.rbADC12Bit);
            this.groupBox7.Controls.Add(this.rbADC10Bit);
            this.groupBox7.Controls.Add(this.lblOutOfSpec);
            this.groupBox7.Controls.Add(this.chkPreventJitter);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.tbNoOfSamples);
            this.groupBox7.Controls.Add(this.tbSamplesPerSecond);
            this.groupBox7.Location = new System.Drawing.Point(1261, 272);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(163, 252);
            this.groupBox7.TabIndex = 37;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sampling";
            // 
            // lblADCSActualValue
            // 
            this.lblADCSActualValue.AutoSize = true;
            this.lblADCSActualValue.Location = new System.Drawing.Point(119, 178);
            this.lblADCSActualValue.Name = "lblADCSActualValue";
            this.lblADCSActualValue.Size = new System.Drawing.Size(13, 13);
            this.lblADCSActualValue.TabIndex = 96;
            this.lblADCSActualValue.Text = "6";
            // 
            // cmbADCS
            // 
            this.cmbADCS.FormattingEnabled = true;
            this.cmbADCS.Items.AddRange(new object[] {
            "Auto",
            "3",
            "4",
            "5",
            "6"});
            this.cmbADCS.Location = new System.Drawing.Point(51, 175);
            this.cmbADCS.Name = "cmbADCS";
            this.cmbADCS.Size = new System.Drawing.Size(62, 21);
            this.cmbADCS.TabIndex = 95;
            this.cmbADCS.SelectedIndexChanged += new System.EventHandler(this.cmbADCS_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 94;
            this.label7.Text = "ADCS";
            // 
            // lblSamplingSettings
            // 
            this.lblSamplingSettings.AutoSize = true;
            this.lblSamplingSettings.Location = new System.Drawing.Point(5, 200);
            this.lblSamplingSettings.Name = "lblSamplingSettings";
            this.lblSamplingSettings.Size = new System.Drawing.Size(16, 13);
            this.lblSamplingSettings.TabIndex = 93;
            this.lblSamplingSettings.Text = "M";
            // 
            // txtNoOfSamples
            // 
            this.txtNoOfSamples.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoOfSamples.Location = new System.Drawing.Point(90, 126);
            this.txtNoOfSamples.MaxLength = 6;
            this.txtNoOfSamples.Name = "txtNoOfSamples";
            this.txtNoOfSamples.Size = new System.Drawing.Size(67, 13);
            this.txtNoOfSamples.TabIndex = 92;
            this.txtNoOfSamples.Text = "5042";
            this.txtNoOfSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfSamples.WordWrap = false;
            this.txtNoOfSamples.TextChanged += new System.EventHandler(this.txtNoOfSamples_TextChanged);
            this.txtNoOfSamples.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfSamples_KeyDown);
            this.txtNoOfSamples.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkDigit_KeyPress);
            // 
            // txtSamplesPerSecond
            // 
            this.txtSamplesPerSecond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSamplesPerSecond.Location = new System.Drawing.Point(90, 51);
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
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 151);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 13);
            this.label30.TabIndex = 90;
            this.label30.Text = "Accuracy";
            // 
            // rbADC12Bit
            // 
            this.rbADC12Bit.AutoSize = true;
            this.rbADC12Bit.Checked = true;
            this.rbADC12Bit.Location = new System.Drawing.Point(110, 149);
            this.rbADC12Bit.Name = "rbADC12Bit";
            this.rbADC12Bit.Size = new System.Drawing.Size(48, 17);
            this.rbADC12Bit.TabIndex = 89;
            this.rbADC12Bit.TabStop = true;
            this.rbADC12Bit.Text = "12bit";
            this.rbADC12Bit.UseVisualStyleBackColor = true;
            this.rbADC12Bit.CheckedChanged += new System.EventHandler(this.rbADC12Bit_CheckedChanged);
            // 
            // rbADC10Bit
            // 
            this.rbADC10Bit.AutoSize = true;
            this.rbADC10Bit.Location = new System.Drawing.Point(59, 149);
            this.rbADC10Bit.Name = "rbADC10Bit";
            this.rbADC10Bit.Size = new System.Drawing.Size(48, 17);
            this.rbADC10Bit.TabIndex = 88;
            this.rbADC10Bit.Text = "10bit";
            this.rbADC10Bit.UseVisualStyleBackColor = true;
            // 
            // lblOutOfSpec
            // 
            this.lblOutOfSpec.BackColor = System.Drawing.Color.OrangeRed;
            this.lblOutOfSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOfSpec.Location = new System.Drawing.Point(8, 219);
            this.lblOutOfSpec.Name = "lblOutOfSpec";
            this.lblOutOfSpec.Padding = new System.Windows.Forms.Padding(3, 2, 1, 1);
            this.lblOutOfSpec.Size = new System.Drawing.Size(147, 21);
            this.lblOutOfSpec.TabIndex = 87;
            this.lblOutOfSpec.Text = "Out of spec: use 10 bit";
            this.lblOutOfSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOutOfSpec.Visible = false;
            // 
            // chkPreventJitter
            // 
            this.chkPreventJitter.AutoSize = true;
            this.chkPreventJitter.Checked = true;
            this.chkPreventJitter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreventJitter.Location = new System.Drawing.Point(6, 69);
            this.chkPreventJitter.Name = "chkPreventJitter";
            this.chkPreventJitter.Size = new System.Drawing.Size(88, 17);
            this.chkPreventJitter.TabIndex = 82;
            this.chkPreventJitter.Text = "Prevent Jitter";
            this.chkPreventJitter.UseVisualStyleBackColor = true;
            this.chkPreventJitter.Click += new System.EventHandler(this.chkPreventJitter_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(9, 126);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 13);
            this.label24.TabIndex = 78;
            this.label24.Text = "No. of samples";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(9, 51);
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
            this.tbNoOfSamples.Location = new System.Drawing.Point(4, 95);
            this.tbNoOfSamples.Maximum = 20000;
            this.tbNoOfSamples.Minimum = 1000;
            this.tbNoOfSamples.Name = "tbNoOfSamples";
            this.tbNoOfSamples.Size = new System.Drawing.Size(153, 45);
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
            this.tbSamplesPerSecond.Location = new System.Drawing.Point(4, 20);
            this.tbSamplesPerSecond.Maximum = 1000000;
            this.tbSamplesPerSecond.Minimum = 10000;
            this.tbSamplesPerSecond.Name = "tbSamplesPerSecond";
            this.tbSamplesPerSecond.Size = new System.Drawing.Size(153, 45);
            this.tbSamplesPerSecond.SmallChange = 2500;
            this.tbSamplesPerSecond.TabIndex = 75;
            this.tbSamplesPerSecond.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbSamplesPerSecond.Value = 504201;
            this.tbSamplesPerSecond.ValueChanged += new System.EventHandler(this.tbSamplesPerSecond_ValueChanged);
            this.tbSamplesPerSecond.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSamplesPerSecond_KeyUp);
            this.tbSamplesPerSecond.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbSamplesPerSecond_MouseUp);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(1259, 722);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(68, 13);
            this.label29.TabIndex = 88;
            this.label29.Text = "Show FFT of";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1367, 722);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(52, 13);
            this.label25.TabIndex = 87;
            this.label25.Text = "Averages";
            // 
            // udFFTAverages
            // 
            this.udFFTAverages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udFFTAverages.Location = new System.Drawing.Point(1328, 718);
            this.udFFTAverages.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udFFTAverages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFFTAverages.Name = "udFFTAverages";
            this.udFFTAverages.Size = new System.Drawing.Size(39, 20);
            this.udFFTAverages.TabIndex = 86;
            this.udFFTAverages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFFTAverages.ValueChanged += new System.EventHandler(this.udFFTAverages_ValueChanged);
            // 
            // lblFFTResolution
            // 
            this.lblFFTResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFFTResolution.AutoSize = true;
            this.lblFFTResolution.BackColor = System.Drawing.Color.White;
            this.lblFFTResolution.Location = new System.Drawing.Point(31, 831);
            this.lblFFTResolution.Name = "lblFFTResolution";
            this.lblFFTResolution.Size = new System.Drawing.Size(57, 13);
            this.lblFFTResolution.TabIndex = 92;
            this.lblFFTResolution.Text = "Resolution";
            // 
            // lblFFTFrequency
            // 
            this.lblFFTFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFFTFrequency.AutoSize = true;
            this.lblFFTFrequency.BackColor = System.Drawing.Color.White;
            this.lblFFTFrequency.Location = new System.Drawing.Point(170, 831);
            this.lblFFTFrequency.Name = "lblFFTFrequency";
            this.lblFFTFrequency.Size = new System.Drawing.Size(84, 13);
            this.lblFFTFrequency.TabIndex = 93;
            this.lblFFTFrequency.Text = "Base frequency:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSetGeneratorFreq);
            this.groupBox2.Controls.Add(this.chkEnableSignalGenerator);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtGeneratorFreq);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbGeneratorWaveform);
            this.groupBox2.Location = new System.Drawing.Point(1260, 741);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 113);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Signal generator";
            // 
            // btnSetGeneratorFreq
            // 
            this.btnSetGeneratorFreq.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSetGeneratorFreq.Location = new System.Drawing.Point(119, 78);
            this.btnSetGeneratorFreq.Name = "btnSetGeneratorFreq";
            this.btnSetGeneratorFreq.Size = new System.Drawing.Size(33, 21);
            this.btnSetGeneratorFreq.TabIndex = 84;
            this.btnSetGeneratorFreq.Text = "Set";
            this.btnSetGeneratorFreq.UseVisualStyleBackColor = false;
            this.btnSetGeneratorFreq.Click += new System.EventHandler(this.btnSetGeneratorFreq_Click);
            // 
            // chkEnableSignalGenerator
            // 
            this.chkEnableSignalGenerator.AutoSize = true;
            this.chkEnableSignalGenerator.Enabled = false;
            this.chkEnableSignalGenerator.Location = new System.Drawing.Point(7, 21);
            this.chkEnableSignalGenerator.Name = "chkEnableSignalGenerator";
            this.chkEnableSignalGenerator.Size = new System.Drawing.Size(65, 17);
            this.chkEnableSignalGenerator.TabIndex = 83;
            this.chkEnableSignalGenerator.Text = "Enabled";
            this.chkEnableSignalGenerator.UseVisualStyleBackColor = true;
            this.chkEnableSignalGenerator.CheckedChanged += new System.EventHandler(this.chkEnableSignalGenerator_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Frequency";
            // 
            // txtGeneratorFreq
            // 
            this.txtGeneratorFreq.Location = new System.Drawing.Point(65, 78);
            this.txtGeneratorFreq.MaxLength = 6;
            this.txtGeneratorFreq.Name = "txtGeneratorFreq";
            this.txtGeneratorFreq.Size = new System.Drawing.Size(51, 20);
            this.txtGeneratorFreq.TabIndex = 31;
            this.txtGeneratorFreq.Text = "1000";
            this.txtGeneratorFreq.WordWrap = false;
            this.txtGeneratorFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkDigit_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Waveform";
            // 
            // cmbGeneratorWaveform
            // 
            this.cmbGeneratorWaveform.FormattingEnabled = true;
            this.cmbGeneratorWaveform.Items.AddRange(new object[] {
            "Sinusoid",
            "Square wave",
            "Triangle wave",
            "Noise"});
            this.cmbGeneratorWaveform.Location = new System.Drawing.Point(65, 44);
            this.cmbGeneratorWaveform.Name = "cmbGeneratorWaveform";
            this.cmbGeneratorWaveform.Size = new System.Drawing.Size(87, 21);
            this.cmbGeneratorWaveform.TabIndex = 29;
            this.cmbGeneratorWaveform.SelectedIndexChanged += new System.EventHandler(this.cmbGeneratorWaveform_SelectedIndexChanged);
            // 
            // lblDCOffset
            // 
            this.lblDCOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDCOffset.AutoSize = true;
            this.lblDCOffset.BackColor = System.Drawing.Color.White;
            this.lblDCOffset.Location = new System.Drawing.Point(22, 486);
            this.lblDCOffset.Name = "lblDCOffset";
            this.lblDCOffset.Size = new System.Drawing.Size(54, 13);
            this.lblDCOffset.TabIndex = 94;
            this.lblDCOffset.Text = "DC offset:";
            // 
            // lblScopeFrequency
            // 
            this.lblScopeFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScopeFrequency.AutoSize = true;
            this.lblScopeFrequency.BackColor = System.Drawing.Color.White;
            this.lblScopeFrequency.Location = new System.Drawing.Point(284, 486);
            this.lblScopeFrequency.Name = "lblScopeFrequency";
            this.lblScopeFrequency.Size = new System.Drawing.Size(13, 13);
            this.lblScopeFrequency.TabIndex = 95;
            this.lblScopeFrequency.Text = "?";
            // 
            // lblWaveformsSecond
            // 
            this.lblWaveformsSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWaveformsSecond.AutoSize = true;
            this.lblWaveformsSecond.BackColor = System.Drawing.Color.White;
            this.lblWaveformsSecond.Location = new System.Drawing.Point(492, 486);
            this.lblWaveformsSecond.Name = "lblWaveformsSecond";
            this.lblWaveformsSecond.Size = new System.Drawing.Size(13, 13);
            this.lblWaveformsSecond.TabIndex = 96;
            this.lblWaveformsSecond.Text = "?";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(179, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Measured frequency:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(393, 486);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 98;
            this.label3.Text = "Display update rate:";
            // 
            // dataIn
            // 
            this.dataIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "Time-seconds";
            chartArea2.AxisY.Title = "Amplitude-volts";
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.dataIn.ChartAreas.Add(chartArea2);
            this.dataIn.Location = new System.Drawing.Point(12, 100);
            this.dataIn.Name = "dataIn";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.MarkerSize = 1;
            series2.Name = "Series1";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            this.dataIn.Series.Add(series2);
            this.dataIn.Size = new System.Drawing.Size(1173, 413);
            this.dataIn.TabIndex = 1;
            title2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "INPUT SIGNAL";
            this.dataIn.Titles.Add(title2);
            this.dataIn.CursorPositionChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.dataIn_CursorPositionChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(169, 114);
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
            this.lblScopeCursor.Location = new System.Drawing.Point(212, 114);
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
            this.lblFFTCursor.Location = new System.Drawing.Point(212, 534);
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
            this.label8.Location = new System.Drawing.Point(169, 534);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Cursor:";
            // 
            // cmbIPAddresses
            // 
            this.cmbIPAddresses.FormattingEnabled = true;
            this.cmbIPAddresses.Location = new System.Drawing.Point(1259, 9);
            this.cmbIPAddresses.Name = "cmbIPAddresses";
            this.cmbIPAddresses.Size = new System.Drawing.Size(118, 21);
            this.cmbIPAddresses.TabIndex = 104;
            this.cmbIPAddresses.SelectedIndexChanged += new System.EventHandler(this.cmbIPAddresses_SelectedIndexChanged);
            // 
            // btnFindDevices
            // 
            this.btnFindDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindDevices.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFindDevices.Location = new System.Drawing.Point(1379, 9);
            this.btnFindDevices.Name = "btnFindDevices";
            this.btnFindDevices.Size = new System.Drawing.Size(45, 21);
            this.btnFindDevices.TabIndex = 105;
            this.btnFindDevices.Text = "Find";
            this.btnFindDevices.UseVisualStyleBackColor = false;
            this.btnFindDevices.Click += new System.EventHandler(this.btnFindDevices_Click);
            // 
            // rbVrms
            // 
            this.rbVrms.AutoSize = true;
            this.rbVrms.BackColor = System.Drawing.Color.White;
            this.rbVrms.Location = new System.Drawing.Point(72, 533);
            this.rbVrms.Name = "rbVrms";
            this.rbVrms.Size = new System.Drawing.Size(48, 17);
            this.rbVrms.TabIndex = 106;
            this.rbVrms.Text = "Vrms";
            this.rbVrms.UseVisualStyleBackColor = false;
            this.rbVrms.CheckedChanged += new System.EventHandler(this.rbDbm_CheckedChanged);
            // 
            // rbDbm
            // 
            this.rbDbm.AutoSize = true;
            this.rbDbm.BackColor = System.Drawing.Color.White;
            this.rbDbm.Checked = true;
            this.rbDbm.Location = new System.Drawing.Point(19, 533);
            this.rbDbm.Name = "rbDbm";
            this.rbDbm.Size = new System.Drawing.Size(46, 17);
            this.rbDbm.TabIndex = 107;
            this.rbDbm.TabStop = true;
            this.rbDbm.Text = "dBm";
            this.rbDbm.UseVisualStyleBackColor = false;
            this.rbDbm.CheckedChanged += new System.EventHandler(this.rbDbm_CheckedChanged);
            // 
            // SygnalAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(1436, 863);
            this.Controls.Add(this.rbDbm);
            this.Controls.Add(this.rbVrms);
            this.Controls.Add(this.btnFindDevices);
            this.Controls.Add(this.cmbIPAddresses);
            this.Controls.Add(this.lblFFTCursor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblScopeCursor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblWaveformsSecond);
            this.Controls.Add(this.lblScopeFrequency);
            this.Controls.Add(this.lblDCOffset);
            this.Controls.Add(this.lblFFTFrequency);
            this.Controls.Add(this.lblFFTResolution);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.udFFTAverages);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSinleShot);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tbTriggerLevel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnStartAcquisition);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.spectrum);
            this.Controls.Add(this.dataIn);
            this.Name = "SygnalAnalyzerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Connected Signal Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.ResetControlFocus);
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTriggerLevel)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNoOfSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamplesPerSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFFTAverages)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button btnStartAcquisition;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar tbTriggerLevel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbTriggerSlope;
        private System.Windows.Forms.ComboBox cmbTriggerMode;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblNumberOfAverages;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblPeekToPeekAvg;
        private System.Windows.Forms.Label lblPeekToPeekCur;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblAverageAvg;
        private System.Windows.Forms.Label lblAverageCur;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblMaxAvg;
        private System.Windows.Forms.Label lblMaxCurrent;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblMinCurrent;
        private System.Windows.Forms.Label lblMinAvg;
        private System.Windows.Forms.Button btnSinleShot;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnResetAverages;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioVisLine;
        private System.Windows.Forms.RadioButton radioVisDots;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbCouplingDC;
        private System.Windows.Forms.RadioButton rbCouplingAC;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TrackBar tbNoOfSamples;
        private System.Windows.Forms.TrackBar tbSamplesPerSecond;
        private System.Windows.Forms.CheckBox chkPreventJitter;
        private System.Windows.Forms.Label lblOutOfSpec;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.RadioButton rbADC12Bit;
        private System.Windows.Forms.RadioButton rbADC10Bit;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown udFFTAverages;
        private System.Windows.Forms.Label lblFFTResolution;
        private System.Windows.Forms.Label lblFFTFrequency;
        private System.Windows.Forms.Label lblTriggerLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetTriggerLevel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkEnableSignalGenerator;
        private System.Windows.Forms.TextBox txtGeneratorFreq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbGeneratorWaveform;
        private System.Windows.Forms.Button btnSetGeneratorFreq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDCOffset;
        private System.Windows.Forms.Label lblScopeFrequency;
        private System.Windows.Forms.Label lblWaveformsSecond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataIn;
        private System.Windows.Forms.TextBox txtNoOfSamples;
        private System.Windows.Forms.TextBox txtSamplesPerSecond;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblScopeCursor;
        private System.Windows.Forms.Label lblFFTCursor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbIPAddresses;
        private System.Windows.Forms.Button btnFindDevices;
        private System.Windows.Forms.Label lblSamplingSettings;
        private System.Windows.Forms.ComboBox cmbADCS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblADCSActualValue;
        private System.Windows.Forms.CheckBox chkDrawMinTrace;
        private System.Windows.Forms.CheckBox chkDrawMaxTrace;
        private System.Windows.Forms.RadioButton rbVrms;
        private System.Windows.Forms.RadioButton rbDbm;
    }
}

