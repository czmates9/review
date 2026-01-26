using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ModuleIfc;

namespace FASK.SledovaniVyroby.Module.MST_Prodej
{
    public partial class frmMainMST_Prodej : Form, IModuleConnector
    {
        public frmMainMST_Prodej()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



        #region IModuleConnector Members
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        public void ReturnPortsToPreviousState()
        {
            //throw new NotImplementedException();
        }

        public void ClosePorts()
        {
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;

            //if (dataVyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
            //    return false;
            //}
            //else
            //{
            //    dataVyroba.SaveActualDataLogScreen();
            //    return true;
            //}
            //throw new NotImplementedException();
            return true;
        }

        #endregion
    }
}
