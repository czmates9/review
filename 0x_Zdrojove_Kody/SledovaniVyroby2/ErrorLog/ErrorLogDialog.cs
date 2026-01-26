using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;

namespace FASK.SledovaniVyroby.ErrorLog
{
    public partial class ErrorLogDialog : Form
    {
        public ErrorLogDialog()
        {
            InitializeComponent();
        }

        private void ErrorLogDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                buttonOK_Click(null, null);
            else
                return;

            e.Handled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            //TOD - MaR dodelat log
            //predavany parametr enum, vyber logovaciho souboru --enum je loglevel
            //Log.ClearLog();
            ErrorLogDialog_Load(null, null);
        }

        private void ErrorLogDialog_Load(object sender, EventArgs e)
        {
            //TOD - MaR dodelat log
            //predavany parametr enum, vyber logovaciho souboru --enum je loglevel
             richTextBoxErrorLog.Text = Fask.Logging.ExceptionHandler2.Handle_GetByFileName("Log_LV.txt");
        }
    }
}