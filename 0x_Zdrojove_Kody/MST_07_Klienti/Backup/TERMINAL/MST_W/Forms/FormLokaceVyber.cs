using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Forms
{
    public partial class FormLokaceVyber : System.Windows.Forms.Form
    {
		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace controller_Lokace = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace(Main.CiselnikLokaceDB);
        private _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
        private string skladID = null;
        /// <summary>
        /// Zaznam, ktery se automaticky vybere.
        /// </summary>
        public string LokaceID { get; set; }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public FormLokaceVyber(string skladID)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.skladID = skladID;
                InitializeComponent();
                MyInitializeGrid();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        public FormLokaceVyber(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.skladID = sklad != null ? sklad.skl_id : string.Empty;
                InitializeComponent();
                MyInitializeGrid();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row Lokace
        {
            get
            {
                try
                {
                    return (this.bsLokace.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row;
                }
                catch
                {
                    return null;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            base.OnLoad(e);
            this.OnResize(e);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            this.ScannerStart();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Size bNew = new Size(panelButtons.Width / 2, panelButtons.Height);
            bStorno.Size = bNew;
        }

        private void finalize()
        {
            if (controller_Lokace != null)
            {
                controller_Lokace.Dispose();
                controller_Lokace = null;
            }
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public void PerformCancel()
        {
            if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberPrerusitVyberDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                return;

            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (Lokace == null)
            {
                MessageBoxBig.Show("Není vybrána lokace", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            if (barcode != string.Empty)
            {
                SkladFindByBarcode(barcode);
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
        }
        #endregion

        private void FormSkladVyber_Load(object sender, EventArgs e)
        {
            ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
            ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
            ciselnikS.Timeout = MST_Global.ServiceTimeOut;
            ciselnikS.UpdateWebServiceCredentials();

            if (!File.Exists(Main.CiselnikLokaceDB))
            {
                DialogResult dr = MessageBoxBig.Show("Èíselník lokací neexistuje, chcete ho stáhnout?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1, Color.DarkRed, true);
                if (dr == DialogResult.Yes)
                {
                    aktualizovatLokace();
                }
            }

            if (File.Exists(Main.CiselnikLokaceDB))
            {
                //this.taLokace.Fill(this.dsLokace.CZMST094);
                this.dsLokace.CZMST094.Clear();
                if (string.IsNullOrEmpty(skladID))
                    this.controller_Lokace.CZMST094_Fill(this.dsLokace.CZMST094);
                else
                    this.controller_Lokace.CZMST094_FillBySklID(this.dsLokace.CZMST094, skladID);
            }
            //else
                //MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberCiselnikSkladuNeexistuje, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1, Color.DarkRed, true);

            try
            {
                this.dataGrid1.CurrentRowIndex = LokaceFindByLocncode(LokaceID);
            }
            catch { }
            //this.dataGrid1.CurrentRowIndex = 0;
        }

        private int LokaceFindByLocncode(string locncode)
        {
            try
            {
                return this.bsLokace.Find("Locncode", locncode);
                //if (index < 0)
                //{
                //    MessageBoxBig.Show(string.Format("Lokace '{0}' nenalezena", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //    return;
                //}
                //this.bsLokace.Position = index;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            return 0;
        }

        private void SkladFindByBarcode(string barcode)
        {
            try
            {
                int index = this.bsLokace.Find("Barcode", barcode);
                if (index < 0)
                {
                    MessageBoxBig.Show(string.Format("Lokace '{0}' nenalezena", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                this.bsLokace.Position = index;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            // Jestlize nalezl, tak jde az sem ...
            PerformOK();
        }

        private void FormSkladVyber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                PerformCancel();
            else if (e.KeyCode == Keys.Enter)
                PerformOK();
            else
                return;

            e.Handled = true;
        }

        private void aktualizovatLokace()
        {
            try
            {
                stahnoutLokace();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void stahnoutLokace()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogLokace(ciselnikS, string.Empty);
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxBig.Show("Aktualizovat èíselník lokací?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;

                aktualizovatLokace();
                this.dsLokace.CZMST094.Clear();

                if (File.Exists(Main.CiselnikLokaceDB))
                {
                    //this.taLokace.Fill(this.sklady.CZMST093);
                    this.dsLokace.CZMST094.Clear();
                    if (string.IsNullOrEmpty(skladID))
						this.controller_Lokace.CZMST094_Fill(this.dsLokace.CZMST094);
                    else
                        this.controller_Lokace.CZMST094_FillBySklID(this.dsLokace.CZMST094, skladID);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}