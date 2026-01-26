using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Forms
{
    public partial class PracujiForm2 : Form
    {
        private string zprava = "Pracuji";
        private string dots = " ";
        private const int numOfDots = 5;

        public PracujiForm2()
        {
            InitializeComponent();
            //Location = vydej.Forms.FormMidLocation.GetFormLocation(this.Size);
            this.zprava_l.Text = zprava;
            this.waitTimer.Interval = 1000;
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    //if (waitTimer != null)
        //    //    waitTimer.Enabled = false;

        //    base.OnClosing(e);
        //}

        public string Zprava
        {
            get { return zprava; }
            set { zprava = value; }
        }
        //private void PracujiForm2_Closed(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Exception dvakrat dispose
        //        //this.Dispose();
        //    }
        //    catch
        //    {
        //    }
        //}


        protected override void OnClosing(CancelEventArgs e)
        {
            if (waitTimer != null)
                waitTimer.Enabled = false;

            base.OnClosing(e);
        }



        private void PracujiForm2_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
        }

        //public void CloseForm()
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.CloseForm(); });
        //        return;
        //    }
            
        //    //this.DialogResult = DialogResult.OK;
        //    this.Close();
        //}

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            lock (MySystem.MyBackgroundWorker.processingLocker)
            {
                if (!MySystem.MyBackgroundWorker.processing)
                {
                    try
                    {
                        if (waitTimer != null)
                            waitTimer.Enabled = false;

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        string chyba = ex.Message.ToString();

                    }
                    return;
                }
            }

            if (dots.Length > numOfDots - 1)
                dots = "*";
            else
                dots += "*";

            this.zprava_l.Text = zprava + "\n" + dots;
        }
    }
}