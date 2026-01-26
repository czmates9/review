using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Logging;

namespace Module.DCDIdeal.InkJet
{
    public partial class frmPotvrzeniKusu : Form
    {
        frmMain vrtacka = null;

        public frmPotvrzeniKusu(frmMain _vrtacka)
        {
            vrtacka = _vrtacka;

            InitializeComponent();

            this.Size = Properties.Settings.Default.PotvrzeniSize;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Size", Properties.Settings.Default, "PotvrzeniSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = Properties.Settings.Default.PotvrzeniLocation;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", Properties.Settings.Default, "PotvrzeniLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
                int kusu = Int16.Parse(txtPocetKusu.Text);
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
            if (vrtacka != null)
            {
                try
                {
                    vrtacka.ClosePorts();
                }
                catch { }
            }

            LogConfig.LogIn(false);
            
            if (vrtacka != null)
            {
                try
                {
                    //TODO: zmena, porty se po prelogovani musi vratit do predchoziho stavu (overit!)
                    //vrtacka.ClosePorts();
                    vrtacka.ReturnPortsToPreviousState();
                }
                catch { }
            }

            UpateForm();
        }

    }
}