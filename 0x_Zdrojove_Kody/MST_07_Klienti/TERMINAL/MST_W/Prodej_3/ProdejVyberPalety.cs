using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;
using System.IO;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejVyberPalety : System.Windows.Forms.Form
    {
        private Classes.ProdejPaleta _paleta = null;
        public Classes.ProdejPaleta Paleta
        {
            get { return _paleta; }
            set
            {
                _paleta = value;

                if (_paleta != null)
                {
                    //update ... 
                    Schema.TypyPalet.PaletyRow paletar = typyPalet.Palety.FindByID(_paleta.Typ);
                    if (paletar == null)
                        cbTyp.Text = _paleta.Typ;
                    else
                        cbTyp.SelectedItem = paletar;

                    tOznaceni.Text = _paleta.Cislo;
                    if (cbTyp.Text.Trim() == string.Empty)
                        cbTyp.Focus();
                    else
                    {
                        tOznaceni.Focus();
                        tOznaceni.SelectAll();
                    }
                }
                else
                {
                    cbTyp.Focus(); //paleta je null => vyber typem palety ...
                }
            }
        }

        public ProdejVyberPalety()
        {
            InitializeComponent();
            this.tOznaceni.Text = "1";

            TypyPaletLoad();
        }

        private void ProdejVyberPalety_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            ScannerStart();
        }

        private void TypyPaletLoad()
        {
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

            cbTyp.BeginUpdate();
            cbTyp.Items.Clear();
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

        private void finalize()
        {
            ScannerFinalize();
        }

        private void PerformOK()
        {
            if (!this.Check())
                return;

            this._paleta = new Fask.MST_W.Prodej_3.Classes.ProdejPaleta();
            if (cbTyp.SelectedItem != null)
                this._paleta.Typ = ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID;
            else
                this._paleta.Typ = cbTyp.Text.Trim();
            this._paleta.Cislo = tOznaceni.Text.Trim();

            finalize();
            this.DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private bool Check()
        {
            // TODO : doplnit do konfigurace check na typ palety, pokud neni, tak umoznit prazdnou hodnotu ...
            //if ((cbTyp.SelectedItem as Schema.TypyPalet.PaletyRow) == null)
            //{
            //    MessageBoxBig.Show("Není vybrán typ palety", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
            //    cbTyp.Focus();
            //    return false;
            //}

            if (tOznaceni.Text.Trim().Length == 0)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberPaletyVyplnteCislo, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                tOznaceni.Focus();
                tOznaceni.SelectAll();
                return false;
            }
            // TODO : doplnit do konfigurace test na cislo/textovou hodnotu ... ???
            ulong cisloPalety = 1;
            try
            {
                cisloPalety = ulong.Parse(tOznaceni.Text);
            }
            catch
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberPaletyVkladejtePouzeCisla, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                tOznaceni.Focus();
                tOznaceni.SelectAll();
                return false;
            }

            return true;
        }

        private void ProdejVyberPalety_KeyDown(object sender, KeyEventArgs e)
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

        #region Scanner start stop
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            scannserstart = false;
            ScannerStop();
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
            if (MST_Global.OnScannerSound_Prodej_3)
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

        private void menuItemAktualize_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.TypyPalet.Actualize_TypyPalet();

            TypyPaletLoad();
        }

        private void cbTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tOznaceni.Focused)
                tOznaceni.Focus();
            tOznaceni.SelectAll();
        }

        private void ProdejVyberPalety_Activated(object sender, EventArgs e)
        {
            // prepnuti na numerickou klavesnici
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }

    }
}