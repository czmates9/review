using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Vydej_3
{
    public partial class TypOznaceniPaletyFormOld : System.Windows.Forms.Form
    {

        private string _typOznaceni;

        public string TypOznaceni
        {
            get { return _typOznaceni; }
            set
            {
                _typOznaceni = value;
                try
                {
                    string[] hodnoty = value.Split(new char[] { ':' });
                    tOznaceni.Text = hodnoty[1];
                    Schema.TypyPalet.PaletyRow paletar = typyPalet.Palety.FindByID(hodnoty[0]);
                    cbTyp.SelectedItem = paletar;
                    //cbTyp.Focus();
                    tOznaceni.Focus();
                    tOznaceni.SelectAll();
                }
                catch { }
            }
        }

        public int Cislo
        {
            get
            {
                return int.Parse(tOznaceni.Text);
            }
            set
            {
                this.tOznaceni.Text = value.ToString();
                this.tOznaceni.Focus();
                this.tOznaceni.SelectAll();
            }
        }


        public TypOznaceniPaletyFormOld()
        {
            InitializeComponent();
            ScannerStart();
            this.tOznaceni.Text = "1";
            try
            {
                //typyPalet = new Fask.MST_W.Schema.TypyPalet();
                typyPalet.Clear();
                typyPalet.ReadXml(MST_W.Main.ConfigTypyPalet, XmlReadMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
                Logging.Log.Write(ex.Message, this.Text);
            }

            UpdateTyp();

        }

        public TypOznaceniPaletyFormOld(bool visible)
        {
            InitializeComponent();

            this.tOznaceni.Text = "1";

            label2.Visible = visible;
            tOznaceni.Visible = visible;

            try
            {
                //typyPalet = new Fask.MST_W.Schema.TypyPalet();
                typyPalet.Clear();
                typyPalet.ReadXml(MST_W.Main.ConfigTypyPalet, XmlReadMode.IgnoreSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
                Logging.Log.Write(ex.Message, this.Text);
            }

            UpdateTyp();

        }

        private void TypOznaceniPaletyForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            ScannerStart();
        }

        private void UpdateTyp()
        {
            cbTyp.BeginUpdate();
            foreach (Schema.TypyPalet.PaletyRow prow in typyPalet.Palety)
            {
                cbTyp.Items.Add(prow);
            }
            cbTyp.EndUpdate();
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void PerformOK()
        {
            if (this.Check())
            {
                ScannerFinalize();
                string styp = ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID;
                _typOznaceni = String.Join(":", new string[] { styp, tOznaceni.Text.Trim() });
                this.DialogResult = DialogResult.OK;
            }
        }

        private void PerformCancel()
        {
            ScannerFinalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private bool Check()
        {
            //if (cbTyp.Text.Trim().Length == 0 && tOznaceni.Text.Trim().Length == 0)
            //{
            //    DialogResult dr = MessageBoxBig.Show("Není zadán typ a èíslo.\n\nChcete pokraèovat?", "", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
            //    return dr == DialogResult.Yes;
            //} 
            //else if (cbTyp.Text.Trim().Length == 0)
            //{
            //    MessageBoxBig.Show("Vypòte typ", Color.Red);
            //    return false;
            //}
            //else 

            if ((cbTyp.SelectedItem as Schema.TypyPalet.PaletyRow) == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldNeniVybranTypPalety, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                cbTyp.Focus();
                return false;
            }

            if (tOznaceni.Text.Trim().Length == 0)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldVyplnteCislo, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                tOznaceni.Focus();
                tOznaceni.SelectAll();
                return false;
            }

            ulong cisloPalety = 1;
            try
            {
                cisloPalety = ulong.Parse(tOznaceni.Text);
            }
            catch
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldVkladejtePouzeCisla, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                tOznaceni.Focus();
                tOznaceni.SelectAll();
                return false;
            }

            if (MST_Global.VydejDoplnitPrefix)
            {
                //Test zda se jedna o sscc
                string sscc = tOznaceni.Text.Trim();
                //if (sscc.IndexOf("00") == 0 && sscc.Length == MST_Global.VydejDelkaKoduPalety && CountParity(sscc.Substring(0, sscc.Length-1)) == sscc[sscc.Length-1].ToString())
                //{ //jedna se o platny sscc kod
                //    return true;
                //}

                //neni to platny sscc kod => doplnit podle zadani ...
                //if (MST_Global.VydejPrefix.Length + tOznaceni.Text.Trim().Length + 1 > MST_Global.VydejDelkaKoduPalety)
                //{
                //    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldCisloVetsiNezMaximum, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                //    return false;
                //}
                //else if(MST_Global.VydejPrefix.Length + tOznaceni.Text.Length < MST_Global.VydejDelkaKoduPalety)
                //{
                //    //pocet nul, ktere se maji generovat
                //    int pocetNul = MST_Global.VydejDelkaKoduPalety - MST_Global.VydejPrefix.Length - tOznaceni.Text.Trim().Length - 1;
                //    string nuly = string.Empty;
                //    for (int i = 0; i < pocetNul; i++) nuly += "0";

                //    string pom = string.Join(nuly, new string[] { MST_Global.VydejPrefix, tOznaceni.Text.Trim() });
                //    pom += CountParity(pom);
                //    tOznaceni.Text = pom;
                //}

            }

            return true;
        }

        private void TypOznaceniPaletyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
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

        private void ok_but_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbTyp_TextChanged(object sender, EventArgs e)
        {
            if (!tOznaceni.Focused)
                tOznaceni.Focus();
            tOznaceni.SelectAll();
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


        delegate void ScannerEventHandlerCall(ScannerEventArgs e);

        private void UpdateUI(ScannerEventArgs e)
        {
            string ck = e.BarcodeData.Trim();

            if (ck.Length > 0)
            {
                this.tOznaceni.Text = ck;
            }

            PerformOK();
            if (MST_Global.OnScannerSound_Vydej_3)
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

        /// <summary>
        /// Vypocet kontrolniho cisla : Modulo 10
        /// </summary>
        /// <param name="newSSCC"></param>
        /// <returns></returns>
        /// <remarks>Mod 10 Check Digit
        ///The calculations for determining the Mod 10 Check Digit character are as follows:
        ///1. Start at the first position and add the value of every other position together.
        ///0 + 2 + 4 + 6 + 8 + 0 = 20
        ///2. The result of Step 1 is multiplied by 3.
        ///20 x 3 = 60
        ///3. Start at the second position and add the value of every other position together.
        ///1 + 3 + 5 + 7 + 9 = 25
        ///4. The results of steps 1 and 3 are added together.
        ///60 + 25 = 85
        ///5. The check character (12th character) is the smallest number which, when added to the
        ///result in step 4, produces a multiple of 10.
        ///85 + X = 90 (next higher multiple of 10)
        ///X = 5 Check Character
        ///</remarks>
        private static string CountParity(string newSSCC)
        {
            int sumLiche = 0;
            int sumSude = 0;

            // index : hodnota
            // 0,1 : "0"
            // 2-19 : cisla
            // 20 : kontrolni cislo
            for (int i = 2; i < newSSCC.Length; i++)
            {
                if ((i + 1) % 2 == 0) //Sude poradove cislo
                    sumSude += int.Parse(newSSCC[i].ToString());
                else //je liche poradove cislo
                    sumLiche += int.Parse(newSSCC[i].ToString());
            }

            return ((10 - (sumLiche * 3 + sumSude) % 10) % 10).ToString();

        }

        private void TypOznaceniPaletyFormOld_Activated(object sender, EventArgs e)
        {
            // prepnuti na numerickou klavesnici
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }

    }
}