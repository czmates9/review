using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;

namespace Fask.Upgrade
{
    public partial class Notification : Form
    {
        public delegate void StartUpdate();
        public delegate void FormLoaded();
        public delegate void StopUpdate();
        public delegate void AbortUpdate();
        public event FormLoaded NotificationFormReady;
        public event StartUpdate StartUpdateEvent;
        //public event StopUpdate StopUpdateEvent;
        public event AbortUpdate AbortUpdateEvent;
        private readonly Version ActualVersion;

        private readonly Assembly callingAssembly;
        public TransferManager.TransferProgress trans;
        private volatile bool updateStarted = false;

        public Notification()
        {
            InitializeComponent();
        }

        public Notification(String name, String message, String version, Version ver, Updater updater)
        {
            InitializeComponent();
            this.appname_label.Text = name;
            this.version_label.Text = version;
            this.message_textbox.Text = message;
            this.ActualVersion = ver;

            #region Graficke zobrazeni

            //Version Version = this.callingAssembly.GetName().Version;

            string[] substring = version.Split('.');

            Version newversion = new Version(int.Parse(substring[0]), int.Parse(substring[1]), int.Parse(substring[2]), int.Parse(substring[3]));

            if (newversion > ver)
            {
                this.appname_label.ForeColor = Color.Green;
                this.version_label.ForeColor = Color.Green;
            }
            else if (newversion < ver)
            {
                this.appname_label.ForeColor = Color.Red;
                this.version_label.ForeColor = Color.Red;
            }
            else
            {
                this.appname_label.ForeColor = Color.Black;
                this.version_label.ForeColor = Color.Black;
            }

            #endregion

            trans = new TransferManager.TransferProgress(UpdateProgressBar);
            updater.UpdateDoneEvent += new Updater.UpdateDone(updater_UpdateDoneEvent);
            updater.UpdateErrorEvent += new Updater.UpdateError(updater_UpdateErrorEvent);
            if (NotificationFormReady != null)
                NotificationFormReady();
        }


        public Notification(String name, String message, String version, Assembly callingAssem, Updater updater)
        {
            InitializeComponent();
            this.appname_label.Text = name;
            this.version_label.Text = version;
            this.message_textbox.Text = message;
            this.callingAssembly = callingAssem;
            trans = new TransferManager.TransferProgress(UpdateProgressBar);
            updater.UpdateDoneEvent += new Updater.UpdateDone(updater_UpdateDoneEvent);
            updater.UpdateErrorEvent += new Updater.UpdateError(updater_UpdateErrorEvent);
            if (NotificationFormReady != null)
                NotificationFormReady();
        }

        void updater_UpdateErrorEvent(Exception ex)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.updater_UpdateErrorEvent(ex); });
                return;
            }

            throw (ex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartUpdateEvent != null)
                {
                    Button button = (Button)sender;
                    button.Visible = false;
                    button.Dock = DockStyle.None;
                    progressBar1.Dock = DockStyle.Top;
                    progressBar1.Visible = true;
                    StartUpdateEvent();
                    updateStarted = true;
                }
            }
            catch (Exception ex) 
            {
                throw (ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (updateStarted)
            {
                if (AbortUpdateEvent != null)
                    AbortUpdateEvent();
            }
            this.DialogResult = DialogResult.No;
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (updateStarted)
            {
                this.DialogResult = DialogResult.Yes;
            }
        }

        public void UpdateProgressBar(int status)
        {
            if (this.InvokeRequired)
            {
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new TransferManager.TransferProgress(UpdateProgressBar), status);
                }
                return;
            }
            progressBar1.Value = (int)status;
        }

        void updater_UpdateDoneEvent()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Updater.UpdateDone(updater_UpdateDoneEvent), null);
                return;
            }
            button1.Visible = false;
            button2.Visible = false;
            progressBar1.Visible = false;
            buttonRestart.Visible = true;
            button1.Dock = DockStyle.None;
            button2.Dock = DockStyle.None;
            progressBar1.Dock = DockStyle.None;
            buttonRestart.Dock = DockStyle.Fill;
        }
    }
}