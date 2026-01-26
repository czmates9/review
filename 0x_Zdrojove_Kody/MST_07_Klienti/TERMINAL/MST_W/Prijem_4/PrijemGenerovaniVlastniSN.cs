using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemGenerovaniVlastniSN : System.Windows.Forms.Form
    {
        public System.Collections.Generic.List<string> list_SN = new List<string>();
       
        private string SN = string.Empty;

        private int _Terminal = MST_Global.TerminalID;
        public int Terminal
        {
            get { return _Terminal; }
            set { _Terminal = value; }
        }

        private int _Posledni_gen_cislo;
        public int Posledni_gen_cislo
        {
            get { return _Posledni_gen_cislo; }
            set { _Posledni_gen_cislo = value; }
        }


        public PrijemGenerovaniVlastniSN()
        {
            InitializeComponent();
            TXT_pocet_SN.MaxLength = Globals.PocetCislicVlastniSN;
            SN = Generovani_nahodneho_SN(_Terminal);
            _Posledni_gen_cislo = Globals.PosledniGenSN;
            LBL_nalezene_volne_SN.Text = Gen_SN_Number(SN, _Posledni_gen_cislo);
        }

        private void PrijemGenerovaniVlastniSN_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
        }

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private string Generovani_nahodneho_SN(int terminal)
        {
            DateTime theDate = DateTime.Now;

            if ((Globals.Datum.Day != theDate.Day) || (Globals.Datum.Month != theDate.Month) || (Globals.Datum.Year != theDate.Year))
            {
                Globals.PosledniGenSN = 0;
                Globals.Datum = theDate;
            }

            string part_sn = theDate.ToString("yyMMdd");

            if (terminal >= 10)
            {
                part_sn = part_sn + terminal.ToString();
            }
            else
            {
                part_sn = part_sn + "0" + terminal.ToString();
            }

            return part_sn;
        }

        private string Gen_SN_Number(string sn, int cislo)
        {
            string ser_cisl;
            return ser_cisl = sn + cislo.ToString(new string('0', Globals.PocetCislicVlastniSN));
        }

        private void BTN_generate_vlastni_SN_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!IsNumber(TXT_pocet_SN.Text) || Convert.ToInt32(TXT_pocet_SN.Text) <= 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemGenerovaniVlastniSNSpatnyPocet, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    TXT_pocet_SN.Focus();
                    TXT_pocet_SN.SelectAll();
                    Cursor.Current = Cursors.Default;
                    return;
                }

                if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemGenerovaniVlastniSNGenerovatSNDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                {
                    TXT_pocet_SN.Focus();
                    Cursor.Current = Cursors.Default;
                    return;
                }

                int pocet = Convert.ToInt32(TXT_pocet_SN.Text);
                string sn;
                int cislo = _Posledni_gen_cislo;

                while (pocet > 0)
                {
                    sn = Gen_SN_Number(SN, cislo);
                    list_SN.Add(sn);
                    pocet--;
                    cislo++;
                }

                Prijem_4.Globals.PosledniGenSN = cislo;
                Cursor.Current = Cursors.Default;
                PerformOK();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                Cursor.Current = Cursors.Default;
                PerformCancel();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        bool IsNumber(string text)
        {
            try
            {
                Convert.ToDouble(text); return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            BTN_generate_vlastni_SN_Click(sender, e);
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(this.panel1.Width / 2, this.panel1.Height);
            button1.Width = nsize.Width;
        }

        private void BTN_generate_vlastni_SN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTN_generate_vlastni_SN_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformCancel();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PrijemGenerovaniVlastniSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }
    }
}