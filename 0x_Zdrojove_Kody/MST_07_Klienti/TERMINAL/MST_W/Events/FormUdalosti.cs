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
using Fask.MST_W;

namespace Fask.Events
{
    public partial class FormUdalosti : System.Windows.Forms.Form
    {
        //private Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter etta = new Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter();
		private Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsTypes controller_eventtypes = null;
        private Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable etdt = new Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable();
        private DataView dvEventsTypes = null;

        public Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesRow SelectedEventsTypesRowRow
        {
            get
            {
                return ((DataRowView)(dataGrid1.BindingContext[dataGrid1.DataSource].Current)).Row as Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesRow;
            }
        }

        public bool FindEventsTypesRowByBarcode(string barcode)
        {
            DataTable tmpdt = dvEventsTypes.ToTable(false, new string[] { etdt.ebarcodeColumn.ColumnName });
            DataRow[] tmprows = tmpdt.Select(etdt.ebarcodeColumn.ColumnName + "='" + barcode + "'");

            if (tmprows.Length > 0)
            {
                dataGrid1.CurrentRowIndex = tmpdt.Rows.IndexOf(tmprows[0]);
            }

            return tmprows.Length > 0;
        }

        public bool FindEventsTypesRowByStatuID(string eid, string etype)
        {
            DataTable tmpdt = dvEventsTypes.ToTable(false, new string[] { etdt.eidColumn.ColumnName, etdt.etypeColumn.ColumnName });
            DataRow[] tmprows = tmpdt.Select(etdt.eidColumn.ColumnName + "='" + eid + "' AND " + etdt.etypeColumn.ColumnName + "='" + etype + "'");

            if (tmprows.Length > 0)
            {
                dataGrid1.CurrentRowIndex = tmpdt.Rows.IndexOf(tmprows[0]);
            }

            return tmprows.Length > 0;
        }

        public FormUdalosti()
        {
            InitializeComponent();

            //etta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Fask.MST_W.Main.CiselnikEventsTypesDB);
            //etta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Fask.MST_W.Main.CiselnikEventsTypesDB);
            controller_eventtypes = new Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsTypes(Fask.MST_W.Main.CiselnikEventsTypesDB);

            InitializeDataGridTableStyle();
            MyInitializeGrid();

            RefreshData();

        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Fask.MST_W.Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Fask.MST_W.Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Fask.MST_W.Main.ConfigDir, this.GetType().ToString()));
        }



        private void InitializeDataGridTableStyle()
        {
            DataGridTableStyle dgts = new DataGridTableStyle();
            dgts.MappingName = etdt.TableName;

            DataGridTextBoxColumn dgcolstatusdesc = new DataGridTextBoxColumn();
            dgcolstatusdesc.MappingName = etdt.edescColumn.ColumnName;
            dgcolstatusdesc.HeaderText = "Popis";
            dgts.GridColumnStyles.Add(dgcolstatusdesc);

            DataGridTextBoxColumn dgcolstatusid = new DataGridTextBoxColumn();
            dgcolstatusid.MappingName = etdt.eidColumn.ColumnName;
            dgcolstatusid.HeaderText = "ID";
            dgts.GridColumnStyles.Add(dgcolstatusid);

            DataGridTextBoxColumn dgcolstatustype = new DataGridTextBoxColumn();
            dgcolstatustype.MappingName = etdt.etypeColumn.ColumnName;
            dgcolstatustype.HeaderText = "Type";
            dgts.GridColumnStyles.Add(dgcolstatustype);

            DataGridTextBoxColumn dgcolstatusbarcode = new DataGridTextBoxColumn();
            dgcolstatusbarcode.MappingName = etdt.ebarcodeColumn.ColumnName;
            dgcolstatusbarcode.HeaderText = "Èár. kód";
            dgts.GridColumnStyles.Add(dgcolstatusbarcode);

            dataGrid1.TableStyles.Add(dgts);
        }

        private void FormUdalosti_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();
            dataGrid1.Focus();
        }

        private void dataGrid1_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            //Data.VyrobaCEDataSet.StatusTypesRow strow = SelectedStatusTypesRow;
            //if (strow == null)
            //    textBoxKod.Text = string.Empty;
            //else
            //    textBoxKod.Text = strow.statusid;
        }

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

            Fask.MST_W.Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Fask.MST_W.Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Fask.MST_W.Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Fask.MST_W.Program.mstw.DisableScanner();
        }
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new ScannerEventHandlerCall(OnScannerEvent), new object[] { e });
        }
        delegate void ScannerEventHandlerCall(Fask.ScannerProvider.ScannerEventArgs e);
        private void OnScannerEvent(Fask.ScannerProvider.ScannerEventArgs e)
        {            
            string carkod = e.BarcodeData.Trim();
            if (carkod.Length == 0)
                return;

            //this.Kod = e.BarcodeData.Trim();
            if (FindEventsTypesRowByBarcode(carkod))
                this.PerformOK();
            else
                MessageBox.Show("Událost s ID '" + carkod + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); 
        }

        private void SettingsSave()
        {
            //Ulozeni nastaveni zobrazeni sloupcu v datagridu
            this.dataGrid1.Save(Path.Combine(Fask.MST_W.Main.ConfigDir, this.GetType().ToString()));
        }

        private void finalize()
        {
            //if (etta != null)
            //{
            //    etta.Dispose();
            //}
            if (controller_eventtypes != null)
            {
                controller_eventtypes.Dispose();
                controller_eventtypes = null;
            }
            ScannerFinalize();
            SettingsSave();
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (SelectedEventsTypesRowRow == null)
            {
                MessageBoxBig.Show("Není vybrána žádná událost", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            if (DialogResult.No == MessageBoxBig.Show("Uložit událost '" + SelectedEventsTypesRowRow.edesc + "' ?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
            {
                return;
            }

            try
            {
                Fask.MST_W.Program.mstw.eventsUser.add(new Event(
            Guid.NewGuid(), SelectedEventsTypesRowRow.eid, SelectedEventsTypesRowRow.etype, DateTime.Now, MST_W.MST_Global.TerminalID, MST_W.MST_Global.UserID,
            null, null, "u", null, null, null, null, null
            ));

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            // po ulozeni neukoncovat ... 
            //finalize();
            //DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormUdalosti_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void menuItemAktualize_Click(object sender, EventArgs e)
        {
            try
            {
                Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
                ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
                ciselnikS.Url = Fask.MST_W.MST_Global.ServerAddress + "Ciselnik.asmx";
                ciselnikS.Timeout = Fask.MST_W.MST_Global.ServiceTimeOut;
                ciselnikS.UpdateWebServiceCredentials();

                var so = ciselnikS.KatalogEventTypesDBPrepare(Fask.MST_W.MST_Global.TerminalID);

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

                Fask.MST_W.FileTransfer.Routines.DownloadDecompressDelete(Main.CiselnikEventsTypesDB);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                RefreshData();
            }
        }

        private void RefreshData()
        {
            try
            {
                //etta.Fill(etdt);
                
                //etdt.Clear();
                //controller_eventtypes.CZMST_EventsTypes_Fill(etdt);

                controller_eventtypes.CZMST_EventsTypes_Fill(etdt);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            dvEventsTypes = new DataView(etdt);
            dataGrid1.DataSource = dvEventsTypes;
        }

    }
}

