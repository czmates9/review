using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramVersion
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            ///Load settings
            LoadSettings();
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            /// otazka zda ulozit
            /// 

        }

        private void LoadSettings()
        {
            txtHeslo_Ctecka.Text = Settings.LicenseCteckaPassword;
            txtMenoSouboru_Ctecka.Text = Settings.LicenseCteckaNameFile;
            txtPriponaSouboru_Ctecka.Text = Settings.LicenseCteckaPriponaFile;
            dtp_Exp_Ctecka.Value = Settings.LicenseCteckaExpiredDate;

            txtHeslo_Konzola.Text = Settings.LicenseKonzolaPassword;
            txtMenoSouboru_Konzola.Text = Settings.LicenseKonzolaNameFile;
            txtPriponaSouboru_Konzola.Text = Settings.LicenseKonzolaPriponaFile;
            dtp_Exp_Konzola.Value = Settings.LicenseKonzolaExpiredDate;

        }

        private void SaveSettings()
        {

            Settings.LicenseCteckaPassword = txtHeslo_Ctecka.Text;
            Settings.LicenseCteckaNameFile = txtMenoSouboru_Ctecka.Text;
            Settings.LicenseCteckaPriponaFile = txtPriponaSouboru_Ctecka.Text;
            Settings.LicenseCteckaExpiredDate = dtp_Exp_Ctecka.Value ;

            Settings.LicenseKonzolaPassword = txtHeslo_Konzola.Text;
            Settings.LicenseKonzolaNameFile = txtMenoSouboru_Konzola.Text;
            Settings.LicenseKonzolaPriponaFile = txtPriponaSouboru_Konzola.Text;
            Settings.LicenseKonzolaExpiredDate = dtp_Exp_Konzola.Value ;

            Settings.Update();

        }

        private void Config_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Shift && !e.Control && !e.Alt)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        PerformClose();
                    }
                    //if (e.KeyCode == Keys.Enter)
                    //{
                    //    Change();


                    //}
                    else
                        return;
                }
                else
                    return;

                e.Handled = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformClose()
        {
            if (MessageBox.Show("Ulozit pred zavřením?", "Uložit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                SaveSettings();
            }

            this.Close();

            //throw new NotImplementedException();
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            Config_KeyDown(sender, e);
        }


    }
}
