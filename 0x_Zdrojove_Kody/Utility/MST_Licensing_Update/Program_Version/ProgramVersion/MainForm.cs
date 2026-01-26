using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProgramVersion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            using (VerzeProgramu_MST_Ctecka fr = new VerzeProgramu_MST_Ctecka())
            {
                this.Hide();
                fr.ShowDialog();

            }
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (LicenceServer.Licence_MST_Server fr = new LicenceServer.Licence_MST_Server())
            {
                this.Hide();
                fr.ShowDialog();
            }
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (LicenceCtecka.Licence_MST_Ctecka fr = new LicenceCtecka.Licence_MST_Ctecka())
            {
                this.Hide();
                fr.ShowDialog();

            }
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (LicenceKonzola.Licence_MST_Konzola fr = new LicenceKonzola.Licence_MST_Konzola())
            {
                this.Hide();
                fr.ShowDialog();

            }
            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;

        }

        private void konfiguraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Config cfg = new Config()) 
            {
                cfg.ShowDialog();
            }
        }


    }
}
