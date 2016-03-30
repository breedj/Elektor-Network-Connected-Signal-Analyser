namespace SignalAnalyzerApplication
{
    partial class MainForm
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
            this.btnSignalAnalyzer = new System.Windows.Forms.Button();
            this.btnSyntheticGenerator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSignalAnalyzer
            // 
            this.btnSignalAnalyzer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignalAnalyzer.Location = new System.Drawing.Point(22, 25);
            this.btnSignalAnalyzer.Name = "btnSignalAnalyzer";
            this.btnSignalAnalyzer.Size = new System.Drawing.Size(181, 169);
            this.btnSignalAnalyzer.TabIndex = 0;
            this.btnSignalAnalyzer.Text = "Signal analyzer";
            this.btnSignalAnalyzer.UseVisualStyleBackColor = true;
            this.btnSignalAnalyzer.Click += new System.EventHandler(this.btnSignalAnalyzer_Click);
            // 
            // btnSyntheticGenerator
            // 
            this.btnSyntheticGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyntheticGenerator.Location = new System.Drawing.Point(226, 25);
            this.btnSyntheticGenerator.Name = "btnSyntheticGenerator";
            this.btnSyntheticGenerator.Size = new System.Drawing.Size(181, 169);
            this.btnSyntheticGenerator.TabIndex = 1;
            this.btnSyntheticGenerator.Text = "Synthetic waveform generator";
            this.btnSyntheticGenerator.UseVisualStyleBackColor = true;
            this.btnSyntheticGenerator.Click += new System.EventHandler(this.btnSyntheticGenerator_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 221);
            this.Controls.Add(this.btnSyntheticGenerator);
            this.Controls.Add(this.btnSignalAnalyzer);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSignalAnalyzer;
        private System.Windows.Forms.Button btnSyntheticGenerator;
    }
}