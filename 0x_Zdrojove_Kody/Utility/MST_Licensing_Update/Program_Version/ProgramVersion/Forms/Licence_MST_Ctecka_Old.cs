using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProgramVersion.LicenceCtecka
{
    public partial class Licence_MST_Ctecka_Old : Form
    {
        public Licence_MST_Ctecka_Old()
        {
            InitializeComponent();
        }

        private void Licence_MST_Ctecka_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo_FASK1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
                {
                    string path = Environment.GetCommandLineArgs()[0];
                    string absolutepath = Path.GetDirectoryName(path);

                    openFileDialog1.InitialDirectory = absolutepath;
                    openFileDialog1.Filter = "ini files (*.ini)|*.ini";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;

                    if ((openFileDialog1.ShowDialog() == DialogResult.OK))
                    {
                        textBoxPathFile.Text = openFileDialog1.FileName;

                        //if ((myStream = openFileDialog1.OpenFile()) != null)
                        //{
                        //    using (myStream)
                        //    {
                        //        ParseXML(myStream);
                        //    }
                        //}
                    }
                }


            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            


            //textBoxcompany.Text = lic.company;
            //textBoxcontact.Text = lic.contact;
            //textBoxnumberTerminal.Text = lic.numberTerminal;
            //textBoxexpiration.Text = lic.expiration;
            //textBoxencryprition.Text = lic.encryprition;
            //textBoxcreated.Text = lic.created;
            //textBoxisValid.Text = lic.isValid.ToString();
            //textBoxisExpirated.Text = lic.isExpirated.ToString();

            //cb_Events.Checked = lic.isEventsEnable;
            //cb_Expedice.Checked = lic.isEventsEnable;
            //cb_Prijem.Checked = lic.isEventsEnable;
            //cb_Prodej.Checked = lic.isEventsEnable;
            //cb_Servis.Checked = lic.isEventsEnable;
            //cb_Tasks.Checked = lic.isEventsEnable;
            //cb_Vydej.Checked = lic.isEventsEnable;

            //rb_Inventura1.Checked = lic.isInventura1Enable;
            //rb_Inventura2.Checked = lic.isInventura2Enable;


        }


    }
}
