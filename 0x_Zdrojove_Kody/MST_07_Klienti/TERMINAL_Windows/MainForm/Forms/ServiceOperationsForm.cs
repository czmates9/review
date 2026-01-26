using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FASK.MST_WINDOWS.Forms
{
    public partial class ServiceOperationsForm : System.Windows.Forms.Form
    {
        public IAsyncResult aresult = null;

        public ServiceOperationsForm()
        {
            InitializeComponent();
        }

        public string Description
        {
            get { return labelDesc.Text; }
            set { labelDesc.Text = value; }
        }

        protected bool timerEnable = true;
        public bool TimerEnable
        {
            get { return timerEnable; }
            set { timerEnable = value; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        protected delegate void VoidDelegate();

        protected virtual void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        protected virtual void finalize()
        {
            this.timer1.Enabled = false;
        }
        private void ServiceOperationsForm_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            this.timer1.Enabled = timerEnable;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Text.Length >= 5)
                label1.Text = string.Empty;
            label1.Text += "*";
            this.Invalidate();
        }

        private void ServiceOperationsForm_Closing(object sender, CancelEventArgs e)
        {
            finalize();
        }

        private void ServiceOperationsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }
	
    }
}