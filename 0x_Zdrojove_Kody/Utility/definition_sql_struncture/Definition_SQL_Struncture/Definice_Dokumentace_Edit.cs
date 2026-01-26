using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Definition_SQL_Struncture
{
    public partial class Definice_Dokumentace_Edit : Form
    {

        public string Schema 
        {
            get;
            set;
        }

        public string Typ
        {
            get;
            set;
        }

        public string Tabulka
        {
            get;
            set;
        }

        public string Stloupec
        {
            get;
            set;
        }

        public string Typ_Poznamky
        {
            get;
            set;
        }

        public string Poznamka_Original
        {
            get;
            set;
        }

        public Definice_Dokumentace_Edit()
        {
            InitializeComponent();
        }

        private void Definice_Dokumentace_Edit_Load(object sender, EventArgs e)
        {
            try
            {

                tb_Poznamka.Text = Poznamka_Original;
                tb_TypPoznamky.Text = Typ_Poznamky;

                L_Schema.Text = Schema;
                L_Typ.Text = Typ;
                L_Tabulka.Text = Tabulka;
                L_Stloupec.Text = Stloupec;



            }
            catch (System.Exception ex)
            {
                WriteError(ex);
            }
        }

        private void WriteError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void TextChanged_Poznamka(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (tb_Poznamka.Text.Trim() == Poznamka_Original.Trim())
                {
                    tb.BackColor = SystemColors.Window;
                }
                else
                {
                    tb.BackColor = Color.MistyRose;
                }
            }
        }

        private void TextChanged_TypPoznamka(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (tb_TypPoznamky.Text.Trim() == Typ_Poznamky.Trim())
                {
                    tb.BackColor = SystemColors.Window;
                }
                else
                {
                    tb.BackColor = Color.MistyRose;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
