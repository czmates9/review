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
    public partial class FormTiskarnyVyber : System.Windows.Forms.Form
    {
        public FormTiskarnyVyber()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            MyInitializeGrid();

            Cursor.Current = Cursors.Default;
        }

        private void FormTiskarnaVyber_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            TiskarnyRefresh();
        }

        private void TiskarnyRefresh()
        {
            try
            {

                this.tiskarny.Clear();

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny ConTisk = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny(Main.CiselnikTiskarnyDB))
				{
					ConTisk.Fill(this.tiskarny.CZMST_TISKARNA);	
				}

				//this.cZMSTTiskarnaTableAdapter.Connection.ConnectionString = "Data source=" + Main.CiselnikTiskarnyDB;
				//this.cZMSTTiskarnaTableAdapter.Fill(this.tiskarny.CZMST_TISKARNA);
                this.dataGrid1.CurrentRowIndex = 0;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            TiskarnaFindDefault();
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNARow Tiskarna
        {
            get
            {
                try
                {
                    return (this.cZMSTTiskarnaBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNARow;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message);
                    return null;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
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
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public void PerformCancel()
        {
            if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.FormsFormTiskarnyVyberPrerusitVyberDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                return;

            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (Tiskarna == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsFormTiskarnyVyberNeniVybranaTiskarna, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
        }

        private void ScannerStart()
        {
            if (!restartscanner)
                return;

            if (Program.mstw.Scanner != null)
            {
                Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Program.mstw.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Program.mstw.Scanner.Enable();
            }
        }

        private void ScannerStop()
        {
            if (Program.mstw.Scanner != null)
            {
                Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Program.mstw.Scanner.Disable();
            }
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
                TiskarnaFindByBarcode(barcode);
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
        }
        #endregion

        private void TiskarnaFindDefault()
        {
            try
            {
                int index = this.cZMSTTiskarnaBindingSource.Find("DEFAULT", 1);
                //if (index < 0)
                //{
                //    MessageBoxBig.Show("Deaultní tiskárna nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //    return;
                //}
                //this.cZMSTTiskarnaBindingSource.Position = index;
                this.dataGrid1.CurrentRowIndex = index;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            //Jestlize nalezl, tak jde az sem ...
            //PerformOK();
        }

        private void TiskarnaSetDefault()
        {
            try
            {
                Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNARow aktualni = Tiskarna;
                if (aktualni == null)
                    return;
                foreach (Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNARow tr in tiskarny.CZMST_TISKARNA)
                {
                    tr.DEFAULT = false;
                }
                aktualni.DEFAULT = true;
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny ConTisk = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Tiskarny(Main.CiselnikTiskarnyDB))
				{
					ConTisk.Update(this.tiskarny);
				}
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                Logging.Log.Write(ex);
            }
        }
        
        //slouzi k pamatovani posledniho hledaneho kodu ...
        private string barcodefind = string.Empty;
        private void TiskarnaFindByBarcode(string barcode)
        {
            try
            {
                barcodefind = barcode;
                int index = this.cZMSTTiskarnaBindingSource.Find("BARCODE", barcode);
                if (index < 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsFormTiskarnyVyberTiskarnaNenalezena, barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                //this.cZMSTTiskarnaBindingSource.Position = index;
                this.dataGrid1.CurrentRowIndex = index;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            // Jestlize nalezl, tak jde az sem ...
            PerformOK();
        }

        private void FormTiskarnaVyber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                PerformCancel();
            else if (e.KeyCode == Keys.Enter)
                PerformOK();
            else
                return;

            e.Handled = true;
        }

        private void menuItemActualize_Click(object sender, EventArgs e)
        {
            try
            {
                _WebRefernces_Globals.CiselnikServiceSession cservice = new _WebRefernces_Globals.CiselnikServiceSession();
                cservice.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                cservice.Timeout = MST_Global.ServiceTimeOut;
                cservice.UpdateWebServiceCredentials();

                var so = cservice.KatalogTiskarnyDBPrepare(MST_Global.TerminalID);

                if (so.Exception)
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }

                if (!so.Finished)
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikTiskarnyDB);

                TiskarnyRefresh();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void menuItemDefault_Click(object sender, EventArgs e)
        {
            TiskarnaSetDefault();
        }

        private void menuItemFindBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                //SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.FormsFormTiskarnyVyberZadejteCarKodTiskarny, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, barcodefind, this.tiskarny.CZMST_TISKARNA.BARCODEColumn.MaxLength);
				SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.FormsFormTiskarnyVyberZadejteCarKodTiskarny, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, barcodefind, (int)Fask.SQLiteDBs.Columns.Tiskarny.ColumnsInfo_CZMST_TISKARNA["BARCODE"].MaxLength);
                if (skf.ShowDialog() == DialogResult.Cancel)
                    return;
                TiskarnaFindByBarcode(skf.Kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void FormTiskarnyVyber_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void FormTiskarnyVyber_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}