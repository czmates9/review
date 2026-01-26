using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;


namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemGenerovaniZakaznickeSN : System.Windows.Forms.Form
    {
        public PrijemGenerovaniZakaznickeSN()
        {
            InitializeComponent();
            TXT_zadane_SN.MaxLength = Globals.MaxDelkaZakazSN;
            TXT_pocet_SN2.MaxLength = Globals.MaxPocetZakazSN;
        }

        private string SN = string.Empty;
        public System.Collections.Generic.List<string> list_SN = new List<string>();

        private void PerformCancel()
        {
            this.finalize();
            DialogResult = DialogResult.Cancel;
        }

        
        private void finalize()
        {
            ScannerFinalize();
        }

        private void PerformOK()
        {
            this.finalize();
            DialogResult = DialogResult.OK;
        }

        private void BTN_generate_zazkaznicke_SN_Click(object sender, EventArgs e)
        {
            try
            {
                this.ScannerStop();

                Cursor.Current = Cursors.WaitCursor;

                bool spravne_SN = true;
                SN = TXT_zadane_SN.Text;
                int delka = SN.Length;
                int i = Prijem_4.Globals.DelkaKontrolySN;
                string sn;
                char[] pole;
                int cislo;
                int b = 0;

                if (!IsNumber(TXT_pocet_SN2.Text) || Convert.ToInt32(TXT_pocet_SN2.Text) <= 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemGenerovaniZakaznickeSNSpatnyPocet, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    TXT_pocet_SN2.Focus();
                    TXT_pocet_SN2.SelectAll();
                    this.ScannerStart();
                    return;
                }

                int pocet = Convert.ToInt32(TXT_pocet_SN2.Text);

                pole = SN.ToCharArray();

                if (delka - i < 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4GenerovaniZakaznickeSNSpatneSN, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    TXT_zadane_SN.Focus();
                    TXT_zadane_SN.SelectAll();
                    this.ScannerStart();
                    return;
                }

                for (; i > 0; i--) // projdeme pocet zadanych cisel od konce v SN a testujeme jestli jsou to cisla
                {
                    if (!(Convert.ToInt32(pole[delka - i]) < 58 && 47 < Convert.ToInt32(pole[delka - i])))
                    {
                        spravne_SN = false;
                    }
                }

                if (spravne_SN == false)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4GenerovaniZakaznickeSNNevyhovujeSN, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    TXT_zadane_SN.Focus();
                    TXT_zadane_SN.SelectAll();
                    this.ScannerStart();
                    return;
                }
                else//nabidka: opravdu chcete pokracovat v generovani cisel 
                {
                    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4GenerovaniZakaznickeSNGenSNDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    {
                        TXT_pocet_SN2.Focus();
                        this.ScannerStart();
                        return;
                    }
                }

                //dve casti SN, pom1 - cast s alfanumerickymy znaky, pom2 - numericka cast
                string pom1 = string.Empty;
                string pom2 = string.Empty;

                i = Prijem_4.Globals.DelkaKontrolySN;

                //naplnime je hodnotama
                for (; i > 0; i--)
                {
                    pom2 = pom2 + Convert.ToString(pole[delka - i]);
                }
                for (; b < delka - Prijem_4.Globals.DelkaKontrolySN; b++)
                {
                    pom1 = pom1 + Convert.ToString(pole[b]);
                }

                cislo = Convert.ToInt32(pom2);

                while (pocet > 0)
                {
                    sn = Gen_SN_Number(pom1, cislo);
                    list_SN.Add(sn);
                    pocet--;
                    cislo++;
                }

                Cursor.Current = Cursors.Default;
                PerformOK();
                
                return;
            }
            catch (Exception ex)
            {
                this.ScannerStart();

                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                TXT_pocet_SN2.Focus();
                return;
            }
            finally
            {
                this.ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        private string Gen_SN_Number(string sn, int cislo)
        {
            string ser_cisl;
            return ser_cisl = sn + cislo.ToString(new string('0', Prijem_4.Globals.DelkaKontrolySN));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        #region Scanner start stop
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }
        delegate void ScannerEventHandlerCall(Fask.ScannerProvider.ScannerEventArgs e);

        private void UpdateUI(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string ck = e.BarcodeData.Trim();
         
            if (ck.Length > 0)
            {
                this.TXT_zadane_SN.Text = ck;
            }
            if (MST_Global.OnScannerSound_Prijem_4)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }
        
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new ScannerEventHandlerCall(UpdateUI), new object[] { e });

            //if (IsNumber(TXT_pocet_SN2.Text))
            //    BTN_generate_zazkaznicke_SN_Click(sender, e);
        }
        #endregion

        private void PrijemGenerovaniZakaznickeSN_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            TXT_pocet_SN2.Focus();

            this.ScannerStart();
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
            BTN_generate_zazkaznicke_SN_Click(sender, e);
        }

        private void PrijemGenerovaniZakaznickeSN_KeyDown(object sender, KeyEventArgs e)
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

        private void panel1_Resize(object sender, EventArgs e)
        {
             Size nsize = new Size(this.panel1.Width / 2, this.panel1.Height);
             button1.Width = nsize.Width;
        }

        private void BTN_generate_zazkaznicke_SN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTN_generate_zazkaznicke_SN_Click(sender, e);
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
    }
}