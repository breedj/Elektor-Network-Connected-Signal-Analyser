namespace SignalAnalyzerApplication
{
    partial class BodeDiagramForm
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
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.spectrum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStartSweep = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindDevices = new System.Windows.Forms.Button();
            this.cmbIPAddresses = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.chartDbm = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDbm)).BeginInit();
            this.SuspendLayout();
            // 
            // spectrum
            // 
            this.spectrum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Maximum = 25000D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Frequency-Hz";
            chartArea1.AxisY.Title = "dBV";
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.spectrum.ChartAreas.Add(chartArea1);
            this.spectrum.Location = new System.Drawing.Point(12, 67);
            this.spectrum.Name = "spectrum";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            this.spectrum.Series.Add(series1);
            this.spectrum.Size = new System.Drawing.Size(1376, 341);
            this.spectrum.TabIndex = 4;
            title1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "FREQUENCY CHARACTERISTIC";
            this.spectrum.Titles.Add(title1);
            // 
            // btnStartSweep
            // 
            this.btnStartSweep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartSweep.Enabled = false;
            this.btnStartSweep.Location = new System.Drawing.Point(696, 839);
            this.btnStartSweep.Name = "btnStartSweep";
            this.btnStartSweep.Size = new System.Drawing.Size(88, 36);
            this.btnStartSweep.TabIndex = 5;
            this.btnStartSweep.Text = "Start sweep";
            this.btnStartSweep.UseVisualStyleBackColor = true;
            this.btnStartSweep.Click += new System.EventHandler(this.btnStartSweep_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(548, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 25);
            this.label1.TabIndex = 78;
            this.label1.Text = "Measuring frequency:";
            // 
            // btnFindDevices
            // 
            this.btnFindDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindDevices.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFindDevices.Location = new System.Drawing.Point(1343, 12);
            this.btnFindDevices.Name = "btnFindDevices";
            this.btnFindDevices.Size = new System.Drawing.Size(45, 21);
            this.btnFindDevices.TabIndex = 108;
            this.btnFindDevices.Text = "Find";
            this.btnFindDevices.UseVisualStyleBackColor = false;
            this.btnFindDevices.Click += new System.EventHandler(this.btnFindDevices_Click);
            // 
            // cmbIPAddresses
            // 
            this.cmbIPAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIPAddresses.FormattingEnabled = true;
            this.cmbIPAddresses.Location = new System.Drawing.Point(1219, 12);
            this.cmbIPAddresses.Name = "cmbIPAddresses";
            this.cmbIPAddresses.Size = new System.Drawing.Size(118, 21);
            this.cmbIPAddresses.TabIndex = 107;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(1126, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(73, 21);
            this.btnConnect.TabIndex = 106;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblFrequency
            // 
            this.lblFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.BackColor = System.Drawing.SystemColors.Control;
            this.lblFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrequency.Location = new System.Drawing.Point(765, 25);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(19, 25);
            this.lblFrequency.TabIndex = 109;
            this.lblFrequency.Text = "-";
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalibrate.Enabled = false;
            this.btnCalibrate.Location = new System.Drawing.Point(602, 839);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(88, 36);
            this.btnCalibrate.TabIndex = 111;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // chartDbm
            // 
            this.chartDbm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.Maximum = 25000D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "Frequency-Hz";
            chartArea2.AxisY.Title = "dBm";
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chartDbm.ChartAreas.Add(chartArea2);
            this.chartDbm.Location = new System.Drawing.Point(12, 414);
            this.chartDbm.Name = "chartDbm";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            this.chartDbm.Series.Add(series2);
            this.chartDbm.Size = new System.Drawing.Size(1376, 375);
            this.chartDbm.TabIndex = 112;
            title2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title1";
            title2.Text = "FREQUENCY CHARACTERISTIC";
            this.chartDbm.Titles.Add(title2);
            // 
            // BodeDiagramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 883);
            this.Controls.Add(this.chartDbm);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.btnFindDevices);
            this.Controls.Add(this.cmbIPAddresses);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartSweep);
            this.Controls.Add(this.spectrum);
            this.Name = "BodeDiagramForm";
            this.Text = "Frequency response";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BodeDiagramForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDbm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart spectrum;
        private System.Windows.Forms.Button btnStartSweep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindDevices;
        private System.Windows.Forms.ComboBox cmbIPAddresses;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDbm;
    }
}