using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Elektor.SignalAnalyzer;

namespace SignalAnalyzerApplication
{
    public partial class frmWaitFindDevices : Form
    {
        public frmWaitFindDevices()
        {
            InitializeComponent();
        }

        private void btnCancelFind_Click(object sender, EventArgs e)
        {
            NetworkScanner.CancelFind();
            this.DialogResult = DialogResult.Abort;
        }
    }
}
