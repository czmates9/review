using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using Fask.MST_W.Forms;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisOkruhList : System.Windows.Forms.Form
    {
        //private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter _taOkruh = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
        //private _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;

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

        public ServisOkruhList(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.odberatel = odberatel;
                InitializeComponent();

                CreateGridStyles();
                MyInitializeGrid();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsServis.CZMST_Servis_Okruh.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = dsServis.CZMST_Servis_Okruh.IDColumn.Caption;
            dg.MappingName = dsServis.CZMST_Servis_Okruh.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Oznaèení";
            dg.MappingName = dsServis.CZMST_Servis_Okruh.OznaceniColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Odbìratel ID";
            dg.MappingName = dsServis.CZMST_Servis_Okruh.ODB_IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "È.k.";
            dg.MappingName = dsServis.CZMST_Servis_Okruh.BarcodeColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Zdroj Seznam ID";
            dg.MappingName = dsServis.CZMST_Servis_Okruh.ZdrojSeznamIDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow Okruh
        {
            get
            {
                try
                {
                    return (this.bsServis.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow;
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
            if (Okruh == null)
            {
                MessageBoxBig.Show("Není vybrán okruh", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
                OkruhFindByBarcode(barcode);
            }
            if (MST_Global.OnScannerSound_ServisModul)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }
        #endregion

        private void FormSkladVyber_Load(object sender, EventArgs e)
        {
            try
            {
                //_taOkruh.Connection.ConnectionString = "Data source=" + Main.ServisCiselnikDB;
                if (odberatel == null)
                    Globals.globalObject.Controller_servis_Ciselniky.Fill_Okruh(this.dsServis.CZMST_Servis_Okruh);
                else
					Globals.globalObject.Controller_servis_Ciselniky.FillByOdbID_Okruh(this.dsServis.CZMST_Servis_Okruh, odberatel.odb_id);

                this.dataGrid1.CurrentRowIndex = 0;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "ServisOkruhList load", MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
        }


        private void OkruhFindByBarcode(string barcode)
        {
            try
            {
                int index = this.bsServis.Find("Barcode", barcode);
                if (index < 0)
                {
                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsFormSkladVyberSkladNenalezen, barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    MessageBoxBig.Show(string.Format("Okruh '{0}' nenalezen", barcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                this.bsServis.Position = index;
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

        //private void aktualizovatSklady()
        //{
        //    try
        //    {
        //        if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
        //        {
        //            exportovatSklady();
        //        }

        //        stahnoutSklady();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //    }
        //}

        //private void exportovatSklady()
        //{
        //    _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(ciselnikS);
        //}

        //private void stahnoutSklady()
        //{
        //    // TODO : proc neni povoleno ... ??? je to blby, i kdyz nejsou filtry na sklady, tak byto chtelo mit sklady k dispozici pro zobrazeni informaci o nazvech skladu ...
        //    //if (Prodej.Globals.FiltrCiselnikSkladu)
        //    _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS);
        //    //else
        //    //    MessageBoxBig.Show("Filtry na èísla skladù nejsou povoleny", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        //}

        //private void miAktualizovat_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBoxBig.Show(Fask.Localization.Localization.FormsFormSkladVyberAktualizovatSkladyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //            return;

        //        aktualizovatSklady();
        //        this.sklady.CZMST093.Clear();

        //        this.cZMST093TableAdapter.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
        //        if (File.Exists(Main.CiselnikSkladyDB))
        //            this.cZMST093TableAdapter.Fill(this.sklady.CZMST093);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //    }
        //}
    }
}