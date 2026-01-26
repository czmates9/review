using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Fask.MST_W.Forms;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisDynamickaTabulka : Form
    {
        // Název tabulky, která se bude stahovat
        private string TableName { get; set; }

        /// <summary>
        /// Text, který se má zobrazit ve status baru
        /// </summary>
        public string StatusBarInfoText { get; set; }

        public ServisDynamickaTabulka(string popis, string tableName)
        {
            InitializeComponent();
            if (popis != null)
                this.Text = popis;

            TableName = tableName;
        }        

        public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow DynamicTableRow
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;

                }
                catch
                {
                    return null;
                }
            }
        }

        private void ServisDynamickaTabulka_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                CreateGridStyles();

                InitializeGrid();

                // natahnout stavy
                FillDynamicTable();
                ScannerStart();
                panelButtons_Resize(null, null);

                dataGrid1.Focus();
                statusBarInfo.Text = StatusBarInfoText != null ? StatusBarInfoText : string.Empty;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds_servis.CZMST_Servis_Dynamic_Table.TableName;

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.Caption;
            dg.MappingName = ds_servis.CZMST_Servis_Dynamic_Table.IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds_servis.CZMST_Servis_Dynamic_Table.OznaceniColumn.Caption;
            dg.MappingName = ds_servis.CZMST_Servis_Dynamic_Table.OznaceniColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = ds_servis.CZMST_Servis_Dynamic_Table.BarcodeColumn.Caption;
            dg.MappingName = ds_servis.CZMST_Servis_Dynamic_Table.BarcodeColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void ServisDynamickaTabulka_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformCancel()
        {
            finalize(); 
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        public void FillDynamicTable()
        {
            try
            {
				string path = Path.Combine(Main.DataDir, "Servis_Ciselniky" + TableName + MST_W.Main.PRD);

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky ConSerCis = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky(path))
				{
					ConSerCis.Fill_DynTab(ds_servis.CZMST_Servis_Dynamic_Table);
				}

				//Globals.globalObject.Controller_servis_Ciselniky.TaDynTab.Fill(ds_servis.CZMST_Servis_Dynamic_Table);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        #region scanner
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

        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod"></param>
        private void najdipolozku(string kod)
        {
            try
            {
                bsServis.Filter = string.Empty;

                var tables = ds_servis.CZMST_Servis_Dynamic_Table.Where(z => !z.IsBarcodeNull() && z.Barcode == kod);

                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Nenalezen záznam s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Count() > 1)
                {
                    bsServis.Filter = "Barcode='" + kod + "'";
                    MessageBoxBig.Show("Nalezeno více záznamů s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                { // nalezen prave jeden ...
                    if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                        MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                    bsServis.Filter = "Barcode='" + kod + "'";
                }
                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_ServisModul)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (DynamicTableRow == null)
                    return;

                
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {
            aktualizaceTabulky();
            FillDynamicTable();
        }

        /// <summary>
        /// Proběhne aktualizace nynější tabulky
        /// </summary>
        private void aktualizaceTabulky()
        {
            string path = string.Empty;

            path = Path.Combine(Main.DataDir, "Servis_Ciselniky" + TableName + ".sdf");
            ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Dynamic_Table(MST_Global.TerminalID, TableName);

            if (so.StatusText != "OK")
            {
                MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return; // TODO : wait ... 
            }

            // stahnout data do tmp ... 
            FileTransfer.Downloading.DownloadFileFromServer(path);
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length == 0)
            {
                bsServis.Filter = string.Empty;
                return;
            }
            //if (txtSearchZdroj.Text.Trim().Length < 2)
            //    return;
            bsServis.Filter = "Oznaceni like '%" + txtSearch.Text.Trim() + "%'";
        }

        private void miSordByID_Click(object sender, EventArgs e)
        {
            sortBy("ID");
        }

        /// <summary>
        /// Řazení podle názvu sloupce
        /// </summary>
        /// <param name="columnName">Název sloupce</param>
        private void sortBy(string columnName)
        {
            bsServis.Sort = columnName + " ASC";
        }

        private void miSortByOznaceni_Click(object sender, EventArgs e)
        {
            sortBy("Oznaceni");
        }

        private void miSortByBarcode_Click(object sender, EventArgs e)
        {
            sortBy("Barcode");
        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    int number = Convert.ToInt32(e.KeyChar.ToString());
                    dataGrid1.CurrentRowIndex = number - 1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if(DialogResult.No == MessageBoxBig.Show("Opravdu se chcete vrátit o krok zpět?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
                return;

                finalize();
                this.DialogResult = DialogResult.Retry;
            }
            catch
            {
            }
        }

        private void miScannerReactivate_Click(object sender, EventArgs e)
        {
            try
            {
                // zalogovani vsech pridelenych udalosti ... ???
                if (Program.mstw.Scanner == null)
                {
                    Logging.Log.Write("1) Application mstw.Scanner is null...");
                }
                Program.mstw.Scanner.Log_DataReady_Events();


                // Pokus o znovu aktivaci scanneru ...
                ScannerStop();
                scannserstart = true;
                ScannerStart();

                // zalogovani vsech pridelenych udalosti ... ???
                if (Program.mstw.Scanner == null)
                {
                    Logging.Log.Write("2) Application mstw.Scanner is null...");
                }
                Program.mstw.Scanner.Log_DataReady_Events();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, "Scanner reactivate", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

    }
}