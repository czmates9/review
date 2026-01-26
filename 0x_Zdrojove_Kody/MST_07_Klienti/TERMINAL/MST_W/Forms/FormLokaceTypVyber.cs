using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;

// TODO : Lokalizovat formular ...

namespace Fask.MST_W.Forms
{
    public partial class FormLokaceTypVyber : System.Windows.Forms.Form
    {
		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace controller_Lokace = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace(Main.CiselnikLokaceDB);
        private _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
        /// <summary>
        /// Pokud je nastaveno, tak se aplikuje filtr na IS_DEFAULT sloupec 
        /// </summary>
        [DefaultValue(null)]
        public bool? LokaceTypeISDefault { get; set; }
        /// <summary>
        /// Zaznam, ktery se automaticky vybere, pokud bude v ramci filtru nalezen ...
        /// </summary>
        public string LokaceTypeID { get; set; }
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

        public FormLokaceTypVyber()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                InitializeComponent();
                MyInitializeGrid();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
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

        public Fask.SQLiteDBs.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow LokaceType
        {
            get
            {
                try
                {
                    return (this.bsLokace.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow;
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
            if (LokaceType == null)
            {
                // TODO : Lokalizovat 
                MessageBoxBig.Show("Není vybrán typ lokace", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
                LokaceTypeFindByBarcode(barcode);
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
        }
        
        #endregion

        private void FormLokaceTypVyber_Load(object sender, EventArgs e)
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
                    downloadLokace();
                }
            }

            refreshLokace();

            try
            {
                if (!String.IsNullOrEmpty(this.LokaceTypeID))
                {
                    //this.dataGrid1.CurrentRowIndex = LokaceTypeFindByType(this.LokaceTypeID);
                    this.bsLokace.Position = LokaceTypeFindByType(this.LokaceTypeID);
                }
            }
            catch { }
            //this.dataGrid1.CurrentRowIndex = 0;
        }

        private int LokaceTypeFindByType(string lokacetyp)
        {
            try
            {
                return this.bsLokace.Find("Type", lokacetyp);
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

        private void LokaceTypeFindByBarcode(string barcode)
        {
            try
            {
                int index = this.bsLokace.Find(dsLokace.CZMST_SkladLokace_LokaceTypy.TYPEColumn.ColumnName, barcode);
                if (index < 0)
                {
                    // TODO : lokalizovat
                    MessageBoxBig.Show(string.Format("Typ lokace '{0}' nenalezen", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
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

        private void FormLokaceTypVyber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                PerformCancel();
            else if (e.KeyCode == Keys.Enter)
                PerformOK();
            else
                return;

            e.Handled = true;
        }

        private void downloadLokace()
        {
            try
            {
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogLokace(ciselnikS, string.Empty);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void refreshLokace()
        {
            try
            {
                if (File.Exists(Main.CiselnikLokaceDB))
                {
                    this.dsLokace.CZMST_SkladLokace_LokaceTypy.Clear();
                    this.controller_Lokace.CZMST_SkladLokace_LokaceTypy_Fill(this.dsLokace.CZMST_SkladLokace_LokaceTypy);
                    if (this.LokaceTypeISDefault.HasValue)
                    {
                        this.bsLokace.Filter = "IS_DEFAULT=" + this.LokaceTypeISDefault.Value.ToString();
                    }
                    else
                    {
                        this.bsLokace.Filter = string.Empty;
                    }
                }

            }
            catch
            {
            }
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxBig.Show("Aktualizovat èíselník lokací?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;

                downloadLokace();

                refreshLokace();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}