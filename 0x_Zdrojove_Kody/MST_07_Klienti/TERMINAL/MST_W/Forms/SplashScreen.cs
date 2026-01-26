using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Fask.MST_W.Forms
{
    public partial class SplashScreen : System.Windows.Forms.Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.Size = Forms.FormLocation.ScreenResolution;
            //pictureBox1.Image = Properties.Resources.logo_FASK;
            imageControl1.Image = Properties.Resources.logo_FASK;
            AssemblyName assname = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            label1.Text = assname.Name + " (" + assname.Version.ToString() + ")";
            label2.Text = "OS : " + Environment.OSVersion.Platform.ToString() + " v:" + Environment.OSVersion.Version.ToString();
            label3.Text = Fask.MST_W.Properties.Resources.strStatus + "Spouštìní aplikace";
        }

        public string Status
        {
            set
            {
                label3.Text = Fask.MST_W.Properties.Resources.strStatus + value;
                this.Show();
                Application.DoEvents();
            }
        }

        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }
    }
}