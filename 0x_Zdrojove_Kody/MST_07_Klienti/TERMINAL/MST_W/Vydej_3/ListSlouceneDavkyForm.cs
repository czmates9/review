using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public partial class ListSlouceneDavkyForm : System.Windows.Forms.Form
    {
        private DataView pohled = null;

        private VydejService.Vydejky hlavicky = new VydejService.Vydejky();

        private VydejService.Vydejky.HlavickyRow vybranaDavka
        {
            get
            {
                CurrencyManager cm = (CurrencyManager)dataGrid1.BindingContext[dataGrid1.DataSource];
                DataRowView drv = cm.Current as DataRowView;
                VydejService.Vydejky.HlavickyRow hrow = drv.Row as VydejService.Vydejky.HlavickyRow;
                return hrow;
            }
        }

        private string[] fileNames;
        /// <summary>
        /// Preda formulari seznam souboru s davkama
        /// </summary>
        public string[] FileNames
        {
            set
            {
                fileNames = value;
            }
        }
        private string fileName = string.Empty;
        /// <summary>
        /// Vrati jmeno souboru se zvolenou davkou
        /// </summary>
        public string FileName
        {
            get
            {
                //return fileName;
                try
                {
                    CurrencyManager cm = (CurrencyManager)dataGrid1.BindingContext[dataGrid1.DataSource];
                    DataRowView drv = cm.Current as DataRowView;
                    VydejService.Vydejky.HlavickyRow hrow = drv.Row as VydejService.Vydejky.HlavickyRow;
                    if (hrow == null)
                        return string.Empty;

                    foreach (string file in fileNames)
                    {
                        string davkaf = System.IO.Path.GetFileNameWithoutExtension(file);
                        if (davkaf == hrow.CountEntries.ToString())
                            return file;
                    }
                    return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }

            }
        }

        private string filePathToActualSloucena = string.Empty;

        public ListSlouceneDavkyForm(string filePath)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();

            this.filePathToActualSloucena = filePath;

            pohled = new DataView();
            pohled.RowStateFilter = DataViewRowState.CurrentRows;
            this.dataGrid1.DataSource = pohled;

            //ToDo..upravit vyhledavani sloucenych davek...
            List<string> davky = FindSlouceneDavky(filePath);

            hlavicky.Hlavicky.Clear();

            foreach (string davka in davky)
            {
                hlavicky.Hlavicky.ImportRow(Hlavicky.HlavickaFind(davka)[0]);
            }

            pohled.Table = hlavicky.Hlavicky;
            //ToDo...zde nebo to dat nahoru...Initialize....
            InitializeDataGrid();
            MyInitializeGrid();

            Cursor.Current = Cursors.Default;
        }

        private static List<string> FindSlouceneDavky(string filePath)
        {
            List<string> davky = new List<string>();

			Fask.SQLiteDBs.DataSets.Vydej.SlouceneDataTable dtSE = Vydej.vydejInstance.globalObject.controller_vydej.GetData_Sloucene();

            foreach (var row in dtSE)
            {
                if (!davky.Contains(row.CountEntries.ToString()))
                    davky.Add(row.CountEntries.ToString());
            }

            return davky;
        }

        private void InitializeDataGrid()
        {
            this.dataGrid1.TableStyles.Clear();

            dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            DataGridTableStyle style = new DataGridTableStyle();
            style.MappingName = hlavicky.Hlavicky.TableName;

            //PriorityColumns.DataGrid2TextBoxColumn dgcsdavka = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn dgcsdavka = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            dgcsdavka.MappingName = hlavicky.Hlavicky.CountEntriesColumn.ColumnName;
            dgcsdavka.HeaderText = "Dávka";
            dgcsdavka.NullText = "-";
            dgcsdavka.Width = 50;
            dgcsdavka.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dgcsdavka);

            //PriorityColumns.DataGrid2TextBoxColumn dgcsobj = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2TextBoxColumn dgcsobj = new Fask.MST_W.Vydej_3.Graphics.DataGrid2TextBoxColumn();
            dgcsobj.MappingName = hlavicky.Hlavicky.SOPNUMBEColumn.ColumnName;
            dgcsobj.HeaderText = "Obj. è.";
            dgcsobj.NullText = "-";
            dgcsobj.Width = 100;
            dgcsobj.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dgcsobj);

            //PriorityColumns.DataGrid2TextBoxColumn rozpracovano = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2TextBoxColumn rozpracovano = new Fask.MST_W.Vydej_3.Graphics.DataGrid2TextBoxColumn();
            rozpracovano.MappingName = hlavicky.Hlavicky.ROZPRACOVANOColumn.ColumnName;
            rozpracovano.HeaderText = "Rozpracováno";
            rozpracovano.NullText = "-";
            rozpracovano.Width = 25;
            rozpracovano.Format = "0";
            rozpracovano.Alignment = StringAlignment.Center;
            rozpracovano.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(rozpracovano);

            //PriorityColumns.DataGrid2TextBoxColumn dgcspol = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn dgcspol = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            dgcspol.MappingName = hlavicky.Hlavicky.CntItemsColumn.ColumnName;
            dgcspol.HeaderText = "Položek";
            dgcspol.NullText = "-";
            dgcspol.Width = 60;
            dgcspol.Format = Settings.UIFormatDesCisel;
            dgcspol.Alignment = StringAlignment.Far;
            dgcspol.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dgcspol);

            //PriorityColumns.DataGrid2TextBoxColumn dgcssoucet = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn dgcssoucet = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            dgcssoucet.MappingName = hlavicky.Hlavicky.SumItemsColumn.ColumnName;
            dgcssoucet.HeaderText = "Souèet";
            dgcssoucet.NullText = "-";
            dgcssoucet.Width = 60;
            dgcssoucet.Format = Settings.UIFormatDesCisel;
            dgcssoucet.Alignment = StringAlignment.Far;
            dgcssoucet.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dgcssoucet);

            PriorityColumns.DataGrid2TextBoxColumn dgcPriority = new PriorityColumns.DataGrid2TextBoxColumn();
            dgcPriority.MappingName = hlavicky.Hlavicky.PRIORITYColumn.ColumnName;
            dgcPriority.HeaderText = "Priorita";
            dgcPriority.NullText = "-";
            dgcPriority.Width = 30;
            dgcPriority.Format = "0";
            dgcPriority.Alignment = StringAlignment.Far;
            dgcPriority.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dgcPriority);


            foreach (DataColumn dcol in this.hlavicky.Hlavicky.Columns)
            {
                if (!style.GridColumnStyles.Contains(dcol.ColumnName))
                {
                    //PriorityColumns.DataGrid2TextBoxColumn du = new PriorityColumns.DataGrid2TextBoxColumn();
                    Graphics.DataGrid2TextBoxColumn du = new Fask.MST_W.Vydej_3.Graphics.DataGrid2TextBoxColumn();
                    du.MappingName = dcol.ColumnName;
                    du.HeaderText = dcol.ColumnName;
                    du.NullText = "-";
                    du.Width = 45;
                    du.Grid = this.dataGrid1;
                    style.GridColumnStyles.Add(du);
                }
            }

            dataGrid1.TableStyles.Add(style);
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }


        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height);
            buttonOK.Size = nsize;
            buttonStorno.Size = nsize;
        }

        private void ListDavkamaForm3_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            this.dataGrid1.CurrentRowIndex = 0;
            ScannerStart();
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

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

        delegate void UpdateUIDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        void UpdateUI(Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                DataTable dt = pohled.ToTable(false, new string[] { hlavicky.Hlavicky.SOPNUMBEColumn.ColumnName });
                DataRow[] drows = dt.Select(hlavicky.Hlavicky.SOPNUMBEColumn.ColumnName + "='" + kod + "'", null, DataViewRowState.CurrentRows);
                if (drows.Length == 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormDavkaSKodemNenalezena, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else
                {
                    dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(drows[0]);
                }
                PerformOK();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new UpdateUIDelegate(UpdateUI), new object[] { e });
        }

        private void ListDavkamaForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonStorno_Click(null, null);
            }
            else if (e.KeyCode == Keys.D0)
            {
                SortOriginal();
            }
            else if (e.KeyCode == Keys.D1)
            {
                SortDavka();
            }
            else if (e.KeyCode == Keys.D2)
            {
                SortObjednavka();
            }
            else if (e.KeyCode == Keys.D3)
            {
                SortCntItems();
            }
            else if (e.KeyCode == Keys.D4)
            {
                SortSumItems();
            }
            else if (e.KeyCode == Keys.D7)
            {
                SortPriorita();
            }

            else
            {
                return;
            }
            e.Handled = true;
        }

        private void PerformOK()
        {
            if (this.FileName == string.Empty)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormNeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            Cursor.Current = Cursors.Default;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            if (addDavkaMode)
            {
                addDavkaMode = false;
                buttonOK.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormOKButton; // OK;
                buttonStorno.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormStornoButton; // Storno;
                pohled.RowFilter = string.Empty;

                hlavicky = hlavickyCopy;
                pohled.Table = hlavicky.Hlavicky;

            }
            else
            {
                PerformCancel();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (addDavkaMode)
            {
                VydejService.Vydejky.HlavickyRow prow = vybranaDavka;

                string sourceFileName = FileName;
                if (prow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormNeniVybranaDavkaKeSlouceni, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                if (prow.Sloucena)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormDavkaUzJeSloucena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                prow.Sloucena = true;

                hlavickyCopy.Hlavicky.ImportRow(prow);

                Vydej.vydejInstance.globalObject.controller_vydej.ImportSEDataByCountEntriesFromTo(sourceFileName, this.filePathToActualSloucena, int.Parse(prow.CountEntries));

                Hlavicky.SetSloucena(prow, true);

              
                hlavicky = hlavickyCopy;
                pohled.Table = hlavicky.Hlavicky;

                addDavkaMode = false;
                buttonOK.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormOKButton; // "OK";
                buttonStorno.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormStornoButton; // "Storno";
                pohled.RowFilter = string.Empty;
            }
            else
            {
                PerformOK();
            }
        }

        private bool sortsumitems = false;
        private void SortSumItems()
        {
            sortsumitems = !sortsumitems;
            if (sortsumitems)
            {
                this.Text = Fask.MST_W.Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniCelkem09;    //(Celkem [0-9])";
                pohled.Sort = "SumItems ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniCelkem90;   //(Celkem [9-0])";
                pohled.Sort = "SumItems DESC";
            }
        }

        private bool sortcntitems = false;
        private void SortCntItems()
        {
            sortcntitems = !sortcntitems;
            if (sortcntitems)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniPocetAZ;    //(Poèet [A-Z])";
                pohled.Sort = "CntItems ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniPocetZA;    //(Poèet [Z-A])";
                pohled.Sort = "CntItems DESC";
            }
        }

        private bool sortdavka = false;
        private void SortDavka()
        {
            sortdavka = !sortdavka;
            if (sortdavka)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniDavkaAZ;    //(Dávka [A-Z])";
                pohled.Sort = "CountEntries ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniDavkaZA;    //(Dávka [Z-A])";
                pohled.Sort = "CountEntries DESC";
            }
        }

        private bool sortobj = false;
        private void SortObjednavka()
        {
            sortobj = !sortobj;
            if (sortobj)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniObjednavkaAZ;   //(Obj.è. [A-Z])";
                pohled.Sort = "SOPNUMBE ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniObjednavkaZA;   //(Obj.è. [Z-A])";
                pohled.Sort = "SOPNUMBE DESC";
            }
        }

        private void SortOriginal()
        {
            pohled.Sort = string.Empty;
        }

        private void miRazeniDavka_Click(object sender, EventArgs e)
        {
            SortDavka();
        }

        private void miRazeniPozekCelkem_Click(object sender, EventArgs e)
        {
            SortSumItems();
        }

        private void miRazeniPolozek_Click(object sender, EventArgs e)
        {
            SortCntItems();
        }

        private void miRazeniObjednavka_Click(object sender, EventArgs e)
        {
            SortObjednavka();
        }

        private void miRazeniOrig_Click(object sender, EventArgs e)
        {
            SortOriginal();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentRowIndex);
        }

        private void ListDavkamaForm2_Closing(object sender, CancelEventArgs e)
        {
            ScannerStop();
        }

        private void ListDavkamaForm2_Resize(object sender, EventArgs e)
        {
            this.Menu = null;
            this.Menu = this.mainMenu1;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            SortPriorita();
        }

        private bool sortPriorita = false;
        private void SortPriorita()
        {
            sortPriorita = !sortPriorita;
            if (sortPriorita)
            {
                this.Text = Fask.MST_W.Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniPriorita09;  //(Priorita [0-9])";
                pohled.Sort = "Priority ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormRazeniPriorita90; //(Priorita [9-0])";
                pohled.Sort = "Priority DESC";
            }
        }

        private bool addDavkaMode = false;
        private VydejService.Vydejky hlavickyCopy = null;
        private void menuItem2_Click(object sender, EventArgs e)
        {
            hlavickyCopy = hlavicky;
            hlavicky = Hlavicky.Davky;

            pohled.Table = hlavicky.Hlavicky;
            pohled.RowFilter = "Sloucena = 'False' AND CountEntries not like 'S%'";
            addDavkaMode = true;
            buttonOK.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormPridatButton; // "Pøidat";
            buttonStorno.Text = Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormZpetButton;   // "Zpìt";
        }

        private void menuItem_Davky_Odstranit_Click(object sender, EventArgs e)
        {
            if (vybranaDavka == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormNeniVybranaDavkaKOdstraneni, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return;
            }

            DeleteDavkaVeSloucene(vybranaDavka.CountEntries);

            Hlavicky.SetSloucena(vybranaDavka, false);

            hlavicky.Hlavicky.RemoveHlavickyRow(vybranaDavka);

            MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListSlouceneDavkyFormDavkaUspesneOdstranena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }


        private void DeleteDavkaVeSloucene(string countEntries)
        {
            // TODO : a co ostatni tabulky ? SI, ... ?
            Vydej.vydejInstance.globalObject.controller_vydej.Sloucena_Smazat_Davku(this.filePathToActualSloucena, countEntries);
        }
    }
}