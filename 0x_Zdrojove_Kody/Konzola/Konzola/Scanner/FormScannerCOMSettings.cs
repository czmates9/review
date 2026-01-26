using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.Scanner
{
    public partial class FormScannerCOMSettings : Form
    {
        System.IO.Ports.SerialPort _serialPort = null;
        public System.IO.Ports.SerialPort SerialPort
        {
            get
            {
                return _serialPort;
            }
            set
            {
                _serialPort = value;
                pgObject.SelectedObject = _serialPort;
            }
        }

        public FormScannerCOMSettings()
        {
            InitializeComponent();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormScannerCOMSettings_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Konfigurace.Globals_Konfig_Konzola.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Konfigurace.Globals_Konfig_Konzola.ApplicationPosition);
            panelButtons_Resize(null, null);
        }

        public void PerformOK()
        {
            this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }
    }
}
