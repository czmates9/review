using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Pila
{
    public partial class frmPotvrzeniTisku : Form
    {
        //Konstruktor
        public frmPotvrzeniTisku(string pocetKusu, string cisloZakazky, string cisloPolozky, string delkaProfilu)
        {
            InitializeComponent();

            //Nastaveni umisteni a rozmeru dle konfigurace
            this.Size = global::Pila.Properties.Settings.Default.TiskSize;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Size", global::Pila.Properties.Settings.Default, "TiskSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Pila.Properties.Settings.Default.TiskLocation;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Pila.Properties.Settings.Default, "TiskLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

            //Inicializace dialogu
            txtPocetKusu.Text = pocetKusu;
            txtOrderNumber.Text = cisloZakazky;
            txtItemNumber.Text = cisloPolozky;
            txtProfileLength.Text = delkaProfilu;
        }

        //Vypublikovani poctu kusu
        public string PocetKusu
        {
            get { return this.txtPocetKusu.Text; }
            set { this.txtPocetKusu.Text = value; }
        }

        //Vypublikovani poznamky
        public string Poznamka
        {
            get { return this.txtPoznamka.Text; }
            set { this.txtPoznamka.Text = value; }
        }

        //OK
        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        //STORNO
        private void buttonStorno_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
