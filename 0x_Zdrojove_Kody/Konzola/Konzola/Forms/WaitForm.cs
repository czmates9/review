using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Konzola.Forms
{
    public partial class WaitForm : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Thread _thread;

        public WaitForm(string name, Thread thread)
        {
            InitializeComponent();

            _thread = thread;

            this.Text = name;
            lbEvent.Text = name;

            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();

            if (progressBar1.Value >= progressBar1.Maximum)
            {
                progressBar1.RightToLeftLayout = !progressBar1.RightToLeftLayout;

                progressBar1.Value = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if(_thread != null)
                    _thread.Abort();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            DialogResult = DialogResult.Cancel;
        }

        public void EndDialog(DialogResult res, string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { EndDialog(res, msg); });
                return;
            }

            if (!String.IsNullOrEmpty(msg))
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = res;
        }
    }
}
