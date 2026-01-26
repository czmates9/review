using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.ScannerProviderSerialPort
{
    public partial class ScannerSerialPortSettingsForm : Form
    {
        public string SPPortName
        {
            get { return cbPort.Text; }
            set { cbPort.Text = value; }
        }

        public ScannerSerialPortSettingsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;
        }

        private void finalize()
        {
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }
    }
}