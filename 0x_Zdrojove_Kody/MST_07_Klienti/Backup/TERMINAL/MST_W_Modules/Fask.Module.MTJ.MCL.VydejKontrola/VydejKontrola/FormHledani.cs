using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrola
{
    public partial class FormHledani : System.Windows.Forms.Form
    {
        private OpenNETCF.Media.SoundPlayer splayerShoda = null;
        private OpenNETCF.Media.SoundPlayer splayerNeShoda = null;

        private string _barcodeFindPrefix = string.Empty;
        private string _barcodeFind = string.Empty;
        public string BarcodeFind
        {
            get { return _barcodeFind; }
            set
            {
                _barcodeFind = value;
                _barcodeFindPrefix = _barcodeFind.Length > Globals.Configuration.ShodaPrefixZnaku ? _barcodeFind.Substring(0, Globals.Configuration.ShodaPrefixZnaku) : _barcodeFind;
                this.labelBarcode.Text = _barcodeFind;
                this.labelFind.Text = _barcodeFindPrefix;
            }
        }
        public string BarcodeFinded { get; set; }
        public DateTime BarcodeFindedDateTime { get; set; }

        public FormHledani()
        {
            InitializeComponent();

            try
            {
                //splayerShoda = new OpenNETCF.Media.SoundPlayer(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukShoda));
                splayerShoda = new OpenNETCF.Media.SoundPlayer(new FileStream(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukShoda), FileMode.Open));
            }
            catch
            {
            }
            try
            {
                //splayerNeShoda = new OpenNETCF.Media.SoundPlayer(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukNeShoda));
                splayerNeShoda = new OpenNETCF.Media.SoundPlayer(new FileStream(Path.Combine(Globals.SoundDir, Globals.Configuration.ZvukNeShoda), FileMode.Open));
            }
            catch 
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            ScannerInitialize();

            this.ScannerStart();
        }

        // Slouzi pro pamatovani posledniho aimtypu a navraceni pri ukonceni ...
        private int scanner_last_successbeeptime = 0;
        private Fask.ScannerProvider.AIMTYPE scanner_last_aimtype = Fask.ScannerProvider.AIMTYPE.UNKNOWN;
        private void ScannerInitialize()
        {
            scanner_last_successbeeptime = Globals.Scanner.SuccessBeepTime;
            scanner_last_aimtype = Globals.Scanner.AimType;

            Globals.Scanner.SuccessBeepTime = 0;
            Globals.Scanner.AimType = Fask.ScannerProvider.AIMTYPE.CONTINUOUS_READ;
        }
        private void ScannerTerminate()
        {
            Globals.Scanner.SuccessBeepTime = scanner_last_successbeeptime;
            Globals.Scanner.AimType = scanner_last_aimtype;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;
        }

        private void finalize()
        {
            ScannerFinalize();
            try
            {
                if (splayerShoda != null)
                {
                    splayerShoda.Dispose();
                    splayerShoda = null;
                }
            }
            catch
            {
            }

            try
            {
                if (splayerNeShoda != null)
                {
                    splayerNeShoda.Dispose();
                    splayerNeShoda = null;
                }
            }
            catch
            {
            }

        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (!BarcodeFinded.StartsWith(_barcodeFindPrefix))
            {
                Fask.Module.MTJ.MCL.VydejKontrola.Forms.MessageBoxBig.Show("Èárový kód požadovaný se neshoduje s nalezeným!", this.Text, MessageBoxButtons.OK, Fask.Module.MTJ.MCL.VydejKontrola.Forms.MessageBoxBigIcon.Critical);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void bStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        #region Scanner
        private bool restartscanner = true;
        private void ScannerFinalize()
        {
            ScannerStop();
            restartscanner = false;

            ScannerTerminate();
        }

        private void ScannerStart()
        {
            if (!restartscanner)
                return;

            if (Globals.Scanner != null)
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Enable();
            }
        }

        private void ScannerStop()
        {
            if (Globals.Scanner != null)
            {
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Disable();
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string barcode = e.BarcodeData.Trim();
                if (barcode != string.Empty)
                {
                    BarcodeFinded = barcode;
                    BarcodeFindedDateTime = DateTime.Now;

                    string b_prefix = barcode.Substring(0, Globals.Configuration.ShodaPrefixZnaku);
                    string b_data = barcode.Substring(Globals.Configuration.ShodaPrefixZnaku);
                    string b_formated = String.Format("{0} - {1}", b_prefix, b_data);
                    //if (BarcodeFind == barcode)
                    if (BarcodeFinded.StartsWith(_barcodeFindPrefix))
                    {
                        listBoxFinded.Items.Insert(0, b_formated + " <<< OK");
                        if (splayerShoda != null)
                            splayerShoda.Play();
                    }
                    else
                    {
                        listBoxFinded.Items.Insert(0, b_formated);
                        if (splayerNeShoda != null)
                            splayerNeShoda.Play();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, this.GetType().ToString());
            }
        }
        #endregion

        private void FaskFormBase_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);
        }

        private void FormHledani_KeyDown(object sender, KeyEventArgs e)
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
                return;

            e.Handled = true;
        }

    }
}