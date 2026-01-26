using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Forms
{
    public partial class PracujiForm : System.Windows.Forms.Form
    {
        private string zprava = "Pracuji";
        private string dots = " ";
        private const int numOfDots = 5;

        public PracujiForm()
        {
            InitializeComponent();
//            Location = vydej.Forms.FormMidLocation.GetFormLocation(this.Size);
            this.zprava_l.Text = zprava;
            this.waitTimer.Interval = 1000;
        }

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            if (!MySystem.MyBackgroundWorker.Processing)
            {
                try
                {
                    if (waitTimer != null)
                        waitTimer.Enabled = false;
                    
                    this.Close();
                }
                catch(Exception ex) 
                {
                    string chyba = ex.Message.ToString();

                }
                return;
            }

            if (dots.Length > numOfDots-1)
                dots = "*";
            else
                dots += "*";

            this.zprava_l.Text = zprava + "\n" + dots;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (waitTimer != null)
                waitTimer.Enabled = false;

            base.OnClosing(e);
        }

        public string Zprava
        {
            get { return zprava; }
            set { zprava = value; }
        }

        private void PracujiForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
   
        }

        private void PracujiForm_Closed(object sender, EventArgs e)
        {
            try
            {
                // Exception dvakrat dispose
                //this.Dispose();
            }
            catch
            {
            }
        }

    }
}