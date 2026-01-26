using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W.Vydej_3.Dialogs
{
    public partial class VydejDialogPocetVydavanych : System.Windows.Forms.Form
    {

        public string I_Itemnmbr = string.Empty;
		public string I_Itemcode = string.Empty;
        public string I_Itemdesc = string.Empty;
        public bool I_PreplneniPovolit = false;

        int _pocetJednotek = 0;
        public int O_PocetJednotek
        {
            get { return _pocetJednotek; }
            set
            {
                _pocetJednotek = value;
                dfPocetJednotek.Data = _pocetJednotek.ToString();
            }
        }

        decimal _mnozstviZadane = 0;
        public decimal I_MnozstviZadane
        {
            get { return _mnozstviZadane; }
            set { _mnozstviZadane = value; }
        }



        public VydejDialogPocetVydavanych()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            //this.ScannerStart();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;

			dfItemnmbr.Width = panelData.Width;
			dfItemdesc.Width = panelData.Width;
			dfitemcode.Width = panelData.Width;
			dfPocetJednotek.Width = panelData.Width;

			
        }

        private void finalize()
        {
            //ScannerFinalize();
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (!PerformValidate())
            {
                //MySystem.Audio.PlaySound(Main.SoundDir);
                dfPocetJednotek.Focus();
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        public bool PerformValidate()
        {
            // valildace, ze je zadano cislo ... 
            try { int.Parse(dfPocetJednotek.Data); }
            catch { return false; }

            return true;
        }

        private void PerformRefresh()
        {
            try
            {
                // prepocet hodnoty zbude
                try
                {
                    _pocetJednotek = int.Parse(dfPocetJednotek.Data);
                    dfPocetJednotek.DataBackColor = SystemColors.Window;
                }
                catch
                {
                    dfPocetJednotek.DataBackColor = Color.MistyRose;
                }


                #region Zobrazeni dat
                dfItemdesc.Data = I_Itemdesc;
                dfItemnmbr.Data = I_Itemnmbr;
				dfitemcode.Data = I_Itemcode;
				dfZadano.Data = I_MnozstviZadane.ToString(Settings.UIFormatDesCisel);
                #endregion

            }
            catch (Exception exRefresh)
            {
                Logging.Log.Write(exRefresh);
            }
        }


        private void bStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void VydejDialogPocetVydavanych_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.PerformRefresh();

            this.dfPocetJednotek.Focus();
        }

        #region Scanner
        //private bool scannserstart = true;
        //private void ScannerFinalize()
        //{
        //    this.ScannerStop();
        //    this.scannserstart = false;
        //}

        //private void ScannerStart()
        //{
        //    if (!scannserstart)
        //        return;

        //    Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
        //    Program.mstw.EnableScanner();
        //}

        //private void ScannerStop()
        //{
        //    Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
        //    Program.mstw.DisableScanner();
        //}


        //void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        //{
        //    this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        //}

        //private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        //private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        //{
        //    string barcode = e.BarcodeData.Trim();
        //    if (barcode != string.Empty)
        //    {
        //        // TODO : insert scanner action
        //    }
        //}
        #endregion


        private void dfPocetJednotek_DataChanged(string data)
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate() { PerformRefresh(); });
        }

        private void VydejDialogPocetVydavanych_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else
            {
                e.Handled = false;
            }

        }
    }
}