using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Logging;

namespace FASK.SledovaniVyroby.Module.Vrtacka_stara
{
    public partial class frmPotvrzeniKusuStara : Form
    {
        public frmPotvrzeniKusuStara()
        {
            InitializeComponent();

            this.Size = global::Vrtacka_stara.Properties.Settings.Default.PotvrzeniSize;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Size", global::Vrtacka_stara.Properties.Settings.Default, "PotvrzeniSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Vrtacka_stara.Properties.Settings.Default.PotvrzeniLocation;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Vrtacka_stara.Properties.Settings.Default, "PotvrzeniLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
        }

        public int PocetKusu
        {
            get { return int.Parse(this.txtPocetKusu.Text); }
            set { this.txtPocetKusu.Text = value.ToString(); }
        }

        public string Poznamka
        {
            get { return this.txtPoznamka.Text; }
            set { this.txtPoznamka.Text = value; }
        }


        private void txtPocetKusu_TextChanged(object sender, EventArgs e)
        {
            txtPocetKusu.BackColor = SystemColors.Window;

            try
            {
                int kusu = int.Parse(txtPocetKusu.Text);
            }
            catch
            {
                txtPocetKusu.BackColor = Color.MistyRose;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int kusu = 0;

            try
            {
                kusu = int.Parse(txtPocetKusu.Text);
            }
            catch
            {
                MessageBox.Show("Poèet kusù není èíslo");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmPotvrzeniKusu_Load(object sender, EventArgs e)
        {
            this.UpateForm();
        }

        private void frmPotvrzeniKusu_Shown(object sender, EventArgs e)
        {
        }

        private void UpateForm()
        {
            labelLogin.Text = LogConfig.LoginString;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LogConfig.LogIn(false);

            UpateForm();
        }

    }
}