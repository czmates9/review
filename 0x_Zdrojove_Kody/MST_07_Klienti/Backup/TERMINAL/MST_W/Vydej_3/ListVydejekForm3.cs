using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Vydej_3
{
    public partial class ListVydejekForm3 : System.Windows.Forms.Form
    {
        private VydejService.Vydejky _vydejky = new VydejService.Vydejky();
        private DataView pohled = null;
        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _sklad = null;

        public VydejService.Vydejky.HlavickyRow Hlavicka
        {
            get
            {
                try
                {
                    CurrencyManager cm = (CurrencyManager)dataGrid1.BindingContext[dataGrid1.DataSource];
                    DataRowView drv = cm.Current as DataRowView;
                    VydejService.Vydejky.HlavickyRow hrow = drv.Row as VydejService.Vydejky.HlavickyRow;
                    return hrow;
                }
                catch //(Exception ex)
                {
                    return null;
                }
            }
        }


        public ListVydejekForm3(VydejService.Vydejky vydejky, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();

            this._vydejky = vydejky;
            this._sklad = sklad;

            updateForm();

            InitializeGridStyles();
            MyInitializeGrid();

            this.menuItem2.Enabled = MST_Global.VydejObjednavkaDetail;
            this.menuItem3.Enabled = MST_Global.VydejGenerovatDataPrikazuOnline;
            this.miStornovatVydejku.Enabled = this.menuItem3.Enabled;

            Cursor.Current = Cursors.Default;

            //Dvojklik dle konfigurace
            dataGrid1.SortByHeaderDoubleClick = MST_Global.VydejPovolitRazeniVydejek;
        }

        private void updateForm()
        {
            pohled = new DataView();
            pohled.RowStateFilter = DataViewRowState.CurrentRows;
            this.dataGrid1.DataSource = pohled;
            pohled.Table = _vydejky.Hlavicky;
        }

        private void InitializeGridStyles()
        {
            DataGridTableStyle style = new DataGridTableStyle();
            style.MappingName = _vydejky.Hlavicky.TableName;

            //PriorityColumns.DataGrid2TextBoxColumn davka = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn davka = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            davka.MappingName = _vydejky.Hlavicky.CountEntriesColumn.ColumnName;
            davka.HeaderText = "Dávka";
            davka.NullText = "-";
            davka.Width = 50; //100;
            davka.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(davka);

            //PriorityColumns.DataGrid2TextBoxColumn obj = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2TextBoxColumn obj = new Fask.MST_W.Vydej_3.Graphics.DataGrid2TextBoxColumn();
            obj.MappingName = _vydejky.Hlavicky.SOPNUMBEColumn.ColumnName;
            obj.HeaderText = "Obj. č.";
            obj.NullText = "-";
            obj.Width = 140;//180
            obj.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(obj);

            //PriorityColumns.DataGrid2TextBoxColumn pol = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn pol = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            pol.MappingName = _vydejky.Hlavicky.CntItemsColumn.ColumnName;
            pol.HeaderText = "Položek";
            pol.NullText = "-";
            pol.Width = 45;//60
            pol.Alignment = StringAlignment.Far;
            pol.Format = "0";
            pol.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(pol);

            //PriorityColumns.DataGrid2TextBoxColumn soucet = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn soucet = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            soucet.MappingName = _vydejky.Hlavicky.SumItemsColumn.ColumnName;
            soucet.HeaderText = "Součet";
            soucet.NullText = "-";
            soucet.Width = 45;//60
            soucet.Alignment = StringAlignment.Far;
            soucet.Format = Settings.UIFormatDesCisel;
            soucet.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(soucet);

            //PriorityColumns.DataGrid2TextBoxColumn dcrozprac = new PriorityColumns.DataGrid2TextBoxColumn();
            Graphics.DataGrid2NumberBoxColumn dcrozprac = new Fask.MST_W.Vydej_3.Graphics.DataGrid2NumberBoxColumn();
            dcrozprac.MappingName = _vydejky.Hlavicky.ROZPRACOVANOColumn.ColumnName;
            dcrozprac.HeaderText = "Rozpracováno";
            dcrozprac.NullText = "-";
            dcrozprac.Width = 45;//60
            dcrozprac.Alignment = StringAlignment.Center;
            dcrozprac.Format = "0";
            dcrozprac.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dcrozprac);

            PriorityColumns.DataGrid2TextBoxColumn dcpriority = new Fask.MST_W.Vydej_3.PriorityColumns.DataGrid2TextBoxColumn();
            dcpriority.MappingName = _vydejky.Hlavicky.PRIORITYColumn.ColumnName;
            dcpriority.HeaderText = "Priorita";
            dcpriority.NullText = "-";
            dcpriority.Width = 30;
            dcpriority.Alignment = StringAlignment.Center;
            //dcpriority.Format 
            dcpriority.Grid = this.dataGrid1;
            style.GridColumnStyles.Add(dcpriority);

            foreach (DataColumn dcol in _vydejky.Hlavicky.Columns)
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

            dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;
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
            buttonStahnout.Size = nsize;
            buttonStorno.Size = nsize;
        }

        private void ListVydejekForm3_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            dataGrid1.CurrentRowIndex = 0;

            ScannerStart();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformCancel()
        {
            this.finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            Cursor.Current = Cursors.Default;
        }

        private void buttonStahnout_Click(object sender, EventArgs e)
        {
            if (MST_Global.VydejDavkyVyberJenScannerem)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3VyberPouzeScannerem, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            Logging.TracId tid0 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "buttonStahnout_Click");
            Logging.Trace2.Write("Start", "PerformStahnout()", tid0);
            PerformStahnout();
            Logging.Trace2.Write("End", "PerformStahnout()", tid0);
        }

        private void PerformStahnout()
        {
            VydejService.Vydejky.HlavickyRow chosenRow = this.Hlavicka;
            if (chosenRow == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3NeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (!Hlavicky.HlavickaSelect(chosenRow))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3PolozkaJizBylaStahnuta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            Logging.TracId tid1 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "PerformStahnout");
            Logging.Trace2.Write("Start", "MST_Global.VydejStahnoutNejvyssiPrioritu", tid1);


            if (MST_Global.VydejStahnoutNejvyssiPrioritu)
            {
                bool checkpriority = true;
                // V pripade, ze existuje jiz rozpracovana davka (rozpracovano>0), tak umoznit stahnout a pracovat
                // protoze se jedna o druhe kolo (NEKUPTO)
                if (chosenRow.IsROZPRACOVANONull())
                { // je prvni stazeni => kontrola priorit
                    checkpriority = true;
                }
                else if (!chosenRow.IsROZPRACOVANONull() && chosenRow.ROZPRACOVANO <= 0)
                { // nic neni rozpracovano => kontrola priorit
                    checkpriority = true;
                }
                else
                { // jiz je rozpracovano => nekontrolavat priority
                    checkpriority = false;
                }

                if (checkpriority
                    &&
                    _vydejky.Hlavicky.Select("PRIORITY < " + chosenRow.PRIORITY + " AND (ROZPRACOVANO is NULL OR ROZPRACOVANO<=0)").Length > 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3DavkuNelzeStahnoutExistujeVyssiPriorita, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
            }
            Logging.Trace2.Write("End", "MST_Global.VydejStahnoutNejvyssiPrioritu", tid1);



            //byte[] dbfile = new byte[0];

            string filename = Path.Combine(Main.StorageDir, chosenRow.CountEntries + "." + Main.Ext_Vydej);
            //string zipFile = Path.Combine(Main.StorageDir, chosenRow.CountEntries + "." + Main.VydejExt + ".zip");

            try
            {
                Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Vydej3ListVydejekForm3StahujiSeDataVydejky);


                #region Nove stahovani

                Logging.TracId tid2 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, int.Parse(chosenRow.CountEntries), this.Name, "PerformStahnout");
                Logging.Trace2.Write("Start", "vydejservice.PrepareVydejkaDB", tid2);
                if (!Vydej.vydejInstance.globalObject.service_vydej.PrepareVydejkaDB(int.Parse(chosenRow.CountEntries), MST_Global.TerminalID, Program.mstw.itemType))
                {
                    throw (new Exception(Fask.Localization.Localization.Vydej3ListVydejekForm3ProblemPripravyVydejky));
                }
                Logging.Trace2.Write("End", "vydejservice.PrepareVydejkaDB", tid2);


                Logging.TracId tid3 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "PerformStahnout");
                Logging.Trace2.Write("Start", "DownloadFileFromServer", tid3);

                //string filename = Path.Combine(Main.StorageDir, chosenRow.CountEntries);
                FileTransfer.Routines.DownloadDecompressDelete(filename);

                if (!Vydej.vydejInstance.globalObject.service_vydej.GetVydejkaReceived(int.Parse(chosenRow.CountEntries), MST_Global.TerminalID, Program.mstw.itemType))
                {
                    if (File.Exists(filename))
                        File.Delete(filename);

                    throw (new Exception(Fask.Localization.Localization.Vydej3ListVydejekForm3ProblemStazeniVydejky));
                }

                Logging.Trace2.Write("End", "", tid3);
                #endregion

                Logging.TracId tid4 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "PerformStahnout");
                Logging.Trace2.Write("Start", "Hlavicky.HlavickaAdd", tid4);
                Hlavicky.HlavickaAdd(chosenRow);
                Logging.Trace2.Write("End", "Hlavicky.HlavickaAdd", tid4);

            }
            catch (Exception ex)
            {
                if (File.Exists(filename))
                    File.Delete(filename);

                //if (File.Exists(zipFile))
                //    File.Delete(zipFile);

                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message);
                return;
            }
            //byte[] dbfile = new byte[0];
            //try
            //{
            //    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Vydej3ListVydejekForm3StahujiSeDataVydejky);
            //    dbfile = vydejservice.GetVydejkaDBFile(int.Parse(chosenRow.CountEntries), MST_Global.TerminalID, Program.mstw.itemType);
            //    if (!vydejservice.GetVydejkaReceived(int.Parse(chosenRow.CountEntries), MST_Global.TerminalID, Program.mstw.itemType))
            //    {
            //        Program.mstw.mbw.EndPracujiForm();
            //        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3ProblemStazeniVydejky);
            //        return;
            //    }
            //    Hlavicky.HlavickaAdd(chosenRow);                
            //}
            //catch (Exception ex)
            //{
            //    Program.mstw.mbw.EndPracujiForm();
            //    MessageBoxBig.Show(ex.Message);
            //    return;
            //}
            try
            {
                //MySystem.FileOperations.DBSave(
                //    Path.Combine(MST_Global.Storage, chosenRow.CountEntries + "." + Main.VydejIExt),
                //    ref dbfile
                //    );
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3ProblemUlozeniVydejky + " " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }
            Program.mstw.mbw.EndPracujiForm();

            if (MST_Global.VydejSlucovaniDavek)
                Slouceni(chosenRow);

            //Odstrani zaznam ze seznamu hlavicek pro stazeni
            Logging.TracId tid5 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "PerformStahnout");
            Logging.Trace2.Write("Start", "chosenRow.Delete", tid5);
            chosenRow.Delete();
            Logging.Trace2.Write("End", "chosenRow.Delete", tid5);

            this.dataGrid1.CurrentRowIndex = this.dataGrid1.CurrentRowIndex;
        }

        private void Slouceni(VydejService.Vydejky.HlavickyRow chosenRow)
        {
            if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3SloucitStazenouDavkuDotaz, Fask.Localization.Localization.Vydej3ListVydejekForm3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;

            string fileName = Path.Combine(MST_Global.Storage, "S1" + "." + Main.Ext_Vydej);

            if (!File.Exists(fileName))
            { //vytvorime novy datatemplate...
                string srcFile = Path.Combine(Main.SQLiteDBsDir, "Vydej.prd");
                File.Copy(srcFile, fileName, false);

                //a radek do seznamu hlavicek...
                VydejService.Vydejky d = new Fask.MST_W.VydejService.Vydejky();
                VydejService.Vydejky.HlavickyRow drow = d.Hlavicky.AddHlavickyRow("S1", "0", 0, 0, "", "Sloucena", 0, 0, false);
                Hlavicky.HlavickaAdd(drow);
            }

            string sourceFilePath = Path.Combine(MST_Global.Storage, chosenRow.CountEntries + "." + Main.Ext_Vydej);

			Vydej.vydejInstance.globalObject.controller_vydej.ImportSEDataByCountEntriesFromTo(sourceFilePath, fileName, int.Parse(chosenRow.CountEntries));
			Vydej.vydejInstance.globalObject.controller_vydej.ImportSIDataByCountEntriesFromTo(sourceFilePath, fileName, int.Parse(chosenRow.CountEntries));
			Vydej.vydejInstance.globalObject.controller_vydej.ImportParametryDataFromTo(sourceFilePath, fileName);

            Hlavicky.SetSloucena(chosenRow, true);
        }


        private void ListVydejekForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //PerformStahnout();
                buttonStahnout_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                PerformCancel();
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
            else if (e.KeyCode == Keys.D5)
            {
                if (MST_Global.VydejObjednavkaDetail)
                    DetailObjednavka();
            }
            else if (e.KeyCode == Keys.D6)
            {
                if (MST_Global.VydejGenerovatDataPrikazuOnline)
                    GenerovatDataPrikazu(null);
            }
            else if (e.KeyCode == Keys.D7)
            {
                SortPriorita();
            }
            else if (e.KeyCode == Keys.D8)
            {
                VydejkaStorno();
            }
            else
                return;

            e.Handled = true;
        }

        private bool sortsumitems = false;
        private void SortSumItems()
        {
            sortsumitems = !sortsumitems;
            if (sortsumitems)
            {
                this.Text = Fask.MST_W.Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniCelkem09;     //" (Celkem [0-9])";
                pohled.Sort = "SumItems ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniCelkem90;    //(Celkem [9-0])";
                pohled.Sort = "SumItems DESC";
            }
        }

        private bool sortcntitems = false;
        private void SortCntItems()
        {
            sortcntitems = !sortcntitems;
            if (sortcntitems)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniPocetAZ;     //(Počet [A-Z])";
                pohled.Sort = "CntItems ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniPocetZA;    //(Počet [Z-A])";
                pohled.Sort = "CntItems DESC";
            }
        }

        private bool sortdavka = false;
        private void SortDavka()
        {
            sortdavka = !sortdavka;
            if (sortdavka)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniDavkaAZ;     //(Dávka [A-Z])";
                pohled.Sort = "CountEntries ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniDavkaZA;     //(Dávka [Z-A])";
                pohled.Sort = "CountEntries DESC";
            }
        }

        private bool sortobj = false;
        private void SortObjednavka()
        {
            sortobj = !sortobj;
            if (sortobj)
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniObjednavkaAZ;    //(Obj.č. [A-Z])";
                pohled.Sort = "SOPNUMBE ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniObjednavkaZA;    //(Obj.č. [Z-A])";
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            DetailObjednavka();
        }

        private void DetailObjednavka()
        {
            VydejService.Vydejky.HlavickyRow hrow = null;
            try
            {
                hrow = (dataGrid1.BindingContext[dataGrid1.DataSource].Current as DataRowView).Row as VydejService.Vydejky.HlavickyRow;
                using (Fask.MST_W.Vydej_3.Detail detail = new Fask.MST_W.Vydej_3.Detail(hrow.SOPNUMBE.Trim()))
                {
                    detail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            GenerovatDataPrikazu(null);
        }

        public int? countentries = null;

        private void GenerovatDataPrikazu(string sopnumbe)
        {
            try
            {
                ScannerStop();

                string val = string.Empty;

                if (string.IsNullOrEmpty(sopnumbe))
                {
                    if (InputBox.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3ZadejteCisloPrikazu, val, out val, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.Cancel)
                        return;
                }
                else val = sopnumbe;


                try
                {
                    string sklad = string.Empty;
                    if (MST_Global.VydejGenerovaniPrikazuZadatSklad)
                    {
                        if (MST_Global.VydejSkladPouzit)
                        {
                            if (!String.IsNullOrEmpty(MST_Global.VydejSkladID))
                            {
                                sklad = MST_Global.VydejSkladID;
                            }
                            else if (_sklad != null)
                            {
                                sklad = this._sklad.skl_id.Trim();
                            }
                            else
                            {
                                Logging.Log.Write("Je vyzadovan nastaveny sklad, ale neni nastaven", "Prijem.GenrovatDataPrikazu");
                                throw new Exception(Fask.Localization.Localization.Prijem4PrijemDavkyListSkladNenastaven);
                            }
                        }
                        else
                        {
                            if (DialogResult.Cancel == InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListCisloSkladu, sklad, out sklad, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric))
                                return;
                        }
                    }


                    // 19.4.2016 JiS => Dlouhotrvajici zpracovani generovani dat davky
                    #region Generovani nove davky serverem a cekani na vysledek

                    #region Puvodni kod ...
                    //int result = vydejservice.GenerateDavka(val.Trim(), sklad);
                    #endregion

                    //1) zjistit aktualni status generovani ...
                    Fask.MST_W.VydejService.StatusObject so = Vydej.vydejInstance.globalObject.service_vydej.GenerateDavkaStatus(val.Trim(), sklad);
                    if (so == null)
                    { // status neexistuje => 1. volani, tak pokracuje...
                    }
                    else if (so.Exception)
                    { // nastala vyjimka pri priprave
                        // => zobrazit informaci o vyjimce a dotaz, zda znovu generovat ANO / NE
                        // => NE: return
                        // => ANO: smazat status na serveru a pokracovat
                        DialogResult dlgResStatusEx = MessageBoxBig.Show(
                            so.StatusText +
                            "\n" + "Generovat dávku dokladu '" + val.Trim() + "' znovu?",
                            "Výdej - generování dávky",
                            MessageBoxButtons.YesNo,
                            MessageBoxBigIcon.Warning);
                        if (dlgResStatusEx == DialogResult.No)
                            return;
                        else
                            so = Vydej.vydejInstance.globalObject.service_vydej.GenerateDavkaStatusDelete(val.Trim(), sklad);
                    }
                    else if (so.Finished)
                    {
                        // generovani dokladu jiz skoncilo
                        // => zobrazit informaci o cisle vygenerovane davky a zda generovat znovu?
                        // => NE: return
                        // => ANO: smazat status na serveru a pokracovat
                        DialogResult dlgResStatusFin = MessageBoxBig.Show(
                            "Již generováno do dávky: '" + so.StatusText + "'" +
                            "\n" + "Generovat dávku dokladu '" + val.Trim() + "' znovu?",
                            "Výdej - generování dávky",
                            MessageBoxButtons.YesNo,
                            MessageBoxBigIcon.Warning);
                        if (dlgResStatusFin == DialogResult.No)
                            return;
                        else
                            so = Vydej.vydejInstance.globalObject.service_vydej.GenerateDavkaStatusDelete(val.Trim(), sklad);
                    }
                    else
                    {
                        // Gnerovani jeste probiha ...
                        // => pokracovat a cekat na dokonceni ...
                    }

                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Vydej3ListVydejekForm3GenerujiDataDavky);

                    so = Vydej.vydejInstance.globalObject.service_vydej.GenerateDavkaRequest(val.Trim(), sklad);
                    // cekat dokud nenastane vyjimka nebo neni dokonceno
                    while (!(so.Exception || so.Finished))
                    {
                        Program.mstw.mbw.Zprava = so.StatusText;
                        System.Threading.Thread.Sleep(1000); // 1sec nic nedelani ...
                        so = Vydej.vydejInstance.globalObject.service_vydej.GenerateDavkaStatus(val.Trim(), sklad);
                        if (so == null)
                            throw new Exception("Status generování dávky dokladu '" + val.Trim() + "' nenalezen!");
                    }

                    if (so.Exception)
                    {
                        throw new Exception(so.StatusText);
                    }

                    // pokud dojde az sem, tak je finished...
                    // zde se ocekava, ze v statustext bude cislo nove davky jako integer...
                    int result = int.Parse(so.StatusText);

                    if (result > 0)
                        countentries = result;
                    else
                    {
                        result = -result;
                        string statusinfo = string.Empty;
                        switch (result)
                        {
                            case 0: statusinfo = "OK"; break;
                            case 1: statusinfo = "Již existuje"; break;
                            case 2: statusinfo = "Neexistuje"; break;
                            case 3: statusinfo = "Bylo nahráno"; break;
                            default:
                                statusinfo = "Neznámý status";
                                break;
                        }
                        throw new Exception(statusinfo + " : " + val.Trim());
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                finally
                {
                    Program.mstw.mbw.EndPracujiForm();
                }

                try
                {
                    VydejService.Vydejky.HlavickyRow hrow = _vydejky.Hlavicky.NewHlavickyRow();
                    hrow.CountEntries = countentries.ToString();
                    hrow.SOPNUMBE = val.Trim();
                    hrow.PRIORITY = 3;
                    _vydejky.Hlavicky.AddHlavickyRow(hrow);

                    DataTable dt = pohled.ToTable(false, new string[] { _vydejky.Hlavicky.CountEntriesColumn.ColumnName });
                    DataRow[] drows = dt.Select("CountEntries=" + countentries);
                    if (drows.Length > 0)
                    {
                        dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(drows[0]);
                        this.PerformStahnout();
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                }

                //return countentries;

            }
            finally
            {
                ScannerStart();
            }
        }


        delegate void BarcodeReadedDelegate(ScannerEventArgs e);

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new BarcodeReadedDelegate(BarcodeReaded), new object[] { e });
        }

        void BarcodeReaded(ScannerEventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = e.BarcodeData.Trim();
                DataTable dt = pohled.ToTable(false, new string[] { _vydejky.Hlavicky.SOPNUMBEColumn.ColumnName });
                DataRow[] drows = dt.Select(_vydejky.Hlavicky.SOPNUMBEColumn.ColumnName + "='" + e.BarcodeData.Trim() + "'");
                if (drows.Length > 0)
                {
                    dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(drows[0]);
                    PerformStahnout();
                }
                else
                {
                    // ma probehnout generovani prijemky, pokud neni nalezena v seznamu
                    if (MST_Global.VydejGenerovatNenalezenouVydejku)
                    {
                        string davka;
                        // pokud davka existuje, neni mozne pokracovat ...
                        if (DavkaExistuje(kod, out davka))
                        {
                            //MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3PolozkaJizBylaStahnuta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaJizBylaStazena, kod, davka), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            return;
                        }

                        if (MST_Global.VydejDialogDavkaNenalezenaVygenerovat)
                        {
                            DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaNenalezenaVygenerovat, e.BarcodeData.Trim()), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                            if (dr == DialogResult.No)
                                return;
                            GenerovatDataPrikazu(kod);

                        }
                        else
                        {
                            GenerovatDataPrikazu(kod);
                        }

                        // generovani davky
                        // TODO : Tady konfiguračne ukončit form po vygenerovani davky
                        if (true)
                        {
                            PerformCancel();
                        }
                    }
                    else
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListVydejekForm3KodNenalezen, e.BarcodeData.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        private bool DavkaExistuje(string sopnumbe, out string davka)
        {
            davka = string.Empty;

            try
            {
                string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Vydej);
                if (fileNames.Length >= 0)
                {
                    //using (Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter se_ta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter())
                    {
                        // projiti vsech vydejek
                        //for (int x = 0; x < fileNames.Length; x++)
                        foreach (string fileName in fileNames)
                        {
                            //pi_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, davka + "." + Main.PrijemIExtData);                        
                            //se_ta.Connection.ConnectionString = "Data source=" + fileNames[x];

                            //int count = Convert.ToInt32(se_ta.CountQuerySopnumbe(sopnumbe));
                            int count = 0;
							using (var controller_vydej_davka = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(fileName))
                            {
                                count = controller_vydej_davka.CountQuerySopnumbe_SE(sopnumbe);
                            }

                            if (count > 0)
                            {
                                davka = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                return true;
                            }
                            //if (vydejky.Hlavicky[i].CountEntries.ToString() == davkaf)
                            //{
                            //    vydejky.Hlavicky[i].Delete();
                            //    break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                // chyba, vraci se true
                return true;
            }
            return false;
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

        private void menuItem6_Click(object sender, EventArgs e)
        {
            using (Forms.FormPodbarveniPriorit frmp = new FormPodbarveniPriorit())
            {
                if (frmp.ShowDialog() == DialogResult.Cancel)
                    return;
            }
        }

        private bool sortPriorita = false;
        private void SortPriorita()
        {
            sortPriorita = !sortPriorita;
            if (sortPriorita)
            {
                this.Text = Fask.MST_W.Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniPriorita09;   //(Priorita [0-9])";
                pohled.Sort = "Priority ASC";
            }
            else
            {
                this.Text = Properties.Resources.strSeznamDavek + " " + Fask.Localization.Localization.Vydej3ListVydejekForm3RazeniPriorita90;      //(Priorita [9-0])";
                pohled.Sort = "Priority DESC";
            }
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            SortPriorita();
        }

        private void ListVydejekForm3_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ListVydejekForm3_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        //private void VydejkaUzavrit()
        //{
        //    if (Hlavicka == null)
        //    {
        //        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3NeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListVydejekForm3UzavritDavkuDotaz, Hlavicka.CountEntries), Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
        //        == DialogResult.No)
        //            return;

        //        _WebRefernces_Globals.VydejServiceSession vservice = new Fask.MST_W._WebRefernces_Globals.VydejServiceSession();
        //        vservice.Url = MST_Global.ServerAddress + "Vydej.asmx";
        //        vservice.Timeout = MST_Global.ServiceTimeOut;
        //        vservice.UpdateWebServiceCredentials();

        //        string pswd = string.Empty;

        //        if (InputBox.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3ZadejteHeslo, pswd, out pswd) != DialogResult.OK)
        //            return;

        //        VydejService.StatusObject so = vservice.FinishVydejka(MST_Global.TerminalID, Hlavicka.CountEntries.ToString(), pswd);

        //        if (so.StatusText == "OK" && !so.Exception)
        //        {
        //            MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3VydejkaUspesneUzavrena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

        //            VydejService.Vydejky.HlavickyRow[] hrows = (VydejService.Vydejky.HlavickyRow[])this._vydejky.Hlavicky.Select("CountEntries='" + Hlavicka.CountEntries + "'");
        //            this._vydejky.Hlavicky.RemoveHlavickyRow(hrows[0]);

        //            updateForm();

        //            //PrijemService.PrijemDavky.HlavickyRow[] hrows = (PrijemService.PrijemDavky.HlavickyRow[])this.davky.Hlavicky.Select("CountEntries='" + SelectedRow.CountEntries + "'");
        //            //this.davky.Hlavicky.RemoveHlavickyRow(hrows[0]);

        //            //updateForm();
        //        }
        //        else
        //            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListVydejekForm3ChybaUzavreniVydejky, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

        //    }
        //    catch (Exception e)
        //    {
        //        Logging.Log.Write(e);
        //        MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Vydej3ListVydejekForm3UzavreniVydejky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
        //    }
        //}

        /// <summary>
        /// Online nastaveni CZ_Doslo na 100+.
        /// </summary>
        private void VydejkaStorno()
        {
            try
            {
                ScannerStop();

                if (Hlavicka == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListNeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListStornovatDavkuDotaz, Hlavicka.CountEntries), Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                string pswd = string.Empty;

                if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteHeslo, pswd, out pswd) != DialogResult.OK)
                    return;

                VydejService.StatusObject so = Vydej.vydejInstance.globalObject.service_vydej.StornoVydejka(MST_Global.TerminalID, Hlavicka.CountEntries, pswd);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListVydejekForm3VydejkaUspesneStornovana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    VydejService.Vydejky.HlavickyRow[] hrows = (VydejService.Vydejky.HlavickyRow[])this._vydejky.Hlavicky.Select("CountEntries='" + Hlavicka.CountEntries + "'");
                    this._vydejky.Hlavicky.RemoveHlavickyRow(hrows[0]);

                    updateForm();

                    //PrijemService.PrijemDavky.HlavickyRow[] hrows = (PrijemService.PrijemDavky.HlavickyRow[])this.davky.Hlavicky.Select("CountEntries='" + SelectedRow.CountEntries + "'");
                    //this.davky.Hlavicky.RemoveHlavickyRow(hrows[0]);

                    //updateForm();
                }
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListChybaStornovaniPrijemky, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Vydej3listvydejekform3StornoVydejky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void miStornovatVydejku_Click(object sender, EventArgs e)
        {
            VydejkaStorno();
        }

    }
}