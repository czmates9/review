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
    public partial class FormSkladVyber : System.Windows.Forms.Form
    {
        private _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;

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

        public FormSkladVyber()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            MyInitializeGrid();
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row Sklad
        {
            get
            {
                try
                {
                    return (this.cZMST093BindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row;
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
            if (Sklad == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberNeniVybranSklad, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

            if (!File.Exists(Main.CiselnikSkladyDB))
            {
                DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberCiselnikSkladuNeexistujeStahnoutDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1, Color.DarkRed, true);
                if (dr == DialogResult.Yes)
                {
                    aktualizovatSklady();
                }
            }

			if (File.Exists(Main.CiselnikSkladyDB))
			{
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady cs = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB))
				{
					cs.Fill(this.sklady.CZMST093);
				}
			}

			//this.cZMST093TableAdapter.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
			//if (File.Exists(Main.CiselnikSkladyDB))
			//    this.cZMST093TableAdapter.Fill(this.sklady.CZMST093);
            //else
                //MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberCiselnikSkladuNeexistuje, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1, Color.DarkRed, true);

            this.dataGrid1.CurrentRowIndex = 0;
        }


        private void SkladFindByBarcode(string barcode)
        {
            try
            {
                int index = this.cZMST093BindingSource.Find("skl_carcode", barcode);
                if (index < 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsFormSkladVyberSkladNenalezen, barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                this.cZMST093BindingSource.Position = index;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            // Jestlize nalezl, tak jde az sem ...
            PerformOK();
        }

        public void SkladFindByID(string sklid)
        {
            try
            {
                int index = this.cZMST093BindingSource.Find("skl_id", sklid);
                if (index < 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsFormSkladVyberSkladNenalezen, sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                this.cZMST093BindingSource.Position = index;
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

        private void aktualizovatSklady()
        {
            try
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatSklady();
                }

                stahnoutSklady();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void exportovatSklady()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(ciselnikS);
        }

        private void stahnoutSklady()
        {
            // TODO : proc neni povoleno ... ??? je to blby, i kdyz nejsou filtry na sklady, tak byto chtelo mit sklady k dispozici pro zobrazeni informaci o nazvech skladu ...
            //if (Prodej.Globals.FiltrCiselnikSkladu)
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS);
            //else
            //    MessageBoxBig.Show("Filtry na èísla skladù nejsou povoleny", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberAktualizovatSkladyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;

                aktualizovatSklady();
                this.sklady.CZMST093.Clear();

				if (File.Exists(Main.CiselnikSkladyDB))
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady cs = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB))
					{
						cs.Fill(this.sklady.CZMST093);
					}
				}

				//this.cZMST093TableAdapter.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
				//if (File.Exists(Main.CiselnikSkladyDB))
				//    this.cZMST093TableAdapter.Fill(this.sklady.CZMST093);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}