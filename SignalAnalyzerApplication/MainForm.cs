using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SignalAnalyzerApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSignalAnalyzer_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate {
                var form = new SygnalAnalyzerForm();
                form.Show();
            });            
        }

        private void btnSyntheticGenerator_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate {
                var form = new SyntheticGeneratorForm();
                form.Show();
            });
        }
    }
}
