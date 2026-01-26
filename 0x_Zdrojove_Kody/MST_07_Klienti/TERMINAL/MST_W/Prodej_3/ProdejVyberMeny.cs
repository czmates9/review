using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.Graphic;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejVyberMeny : System.Windows.Forms.Form
    {
        //private int cislodavky = -1;

        private int _db_records_count = 0;
        private int _db_record_actual = 0;
        private int _db_rocords_per_view = 8;

        private enum SearchMethodEnum
        {
            Nazev,
            None
        }

        private SearchMethodEnum _searchmethod = SearchMethodEnum.None;

        private readonly string _sSelectStr = "Select * from czmst097";
        private readonly string _sSelectStrCount = "Select Count(*) from czmst097";
        private string _sWhereStr = string.Empty;
        private string _sOrderStr = string.Empty;
        private string _sSelectActual = string.Empty;
        private string _sSelectActualCount = string.Empty;
        private string _filtrNazevActual = string.Empty;
        private string _filtrStrIdActual = string.Empty;
        private string _filtrTypActual = string.Empty;
        private const string _filtrTypEmpty = "str_typ=''";
        private const string _filtrTypNotEmpty = "str_typ<>''";
        private const string _sortNazevAsc = "str_desc asc";
        private const string _sortNazevDsc = "str_desc desc";
        private const string _sortCarCodeAsc = "str_carcode asc";
        private const string _sortCarCodeDsc = "str_carcode desc";
        private void SetSelectActual()
        {
            _sSelectActual = _sSelectStr;
            _sSelectActualCount = _sSelectStrCount;
            _sWhereStr = string.Empty;
            if (_filtrStrIdActual != string.Empty)
            {
                _sWhereStr = _filtrStrIdActual;
            }
            if (_filtrNazevActual != string.Empty)
            {
                if (_sWhereStr != string.Empty) _sWhereStr += " AND ";
                _sWhereStr = _filtrNazevActual;
            }
            if (_filtrTypActual != string.Empty)
            {
                if (_sWhereStr != string.Empty) _sWhereStr += " AND ";
                _sWhereStr += _filtrTypActual;
            }
            if (_sWhereStr != string.Empty)
            {
                _sSelectActual += " where " + _sWhereStr;
                _sSelectActualCount += " where " + _sWhereStr;
            }
            if (_sOrderStr != string.Empty)
            {
                _sSelectActual += " order by " + _sOrderStr;
                _sSelectActualCount += " order by " + _sOrderStr;
            }
        }

        private Fask.SQLiteDBs.DataSets.Meny _meny = null;
        private DataView _menyView = null; 

        private DataGrid2TextBoxColumn _menaid;
        private DataGrid2TextBoxColumn _menadesc;
        private DataGrid2TextBoxColumn _menadefault;
        private DataGrid2TextBoxColumn _menakurz;
        private DataGrid2TextBoxColumn _menakurzdatum;

        public bool bezCiziMeny = false;

        private Fask.SQLiteDBs.DataSets.Meny.CZMST097Row _mena = null;
        public Fask.SQLiteDBs.DataSets.Meny.CZMST097Row Mena
        {
            get { return _mena; }
        }

        private void FindMenaInView(Fask.SQLiteDBs.DataSets.Meny.CZMST097Row value)
        {
            DataTable dt = _menyView.ToTable();
            DataRow[] rows = dt.Select("mena_ID='" + value.mena_ID + "'");
            if (rows.Length > 0)
            {
                dataGrid1.CurrentCell = new DataGridCell(dt.Rows.IndexOf(rows[0]), 0);
            }
            else
            {
                try
                {
                    dataGrid1.CurrentCell = new DataGridCell(0, 1);
                    dataGrid1.CurrentCell = new DataGridCell(0, 0);
                }
                catch { }
            }
        }

        private void FindMenaInView(string value)
        {
            DataTable dt = _menyView.ToTable();
            DataRow[] rows = dt.Select("mena_ID='" + value + "'");
            if (rows.Length > 0)
            {
                dataGrid1.CurrentCell = new DataGridCell(dt.Rows.IndexOf(rows[0]), 0);
            }
            else
            {
                try
                {
                    dataGrid1.CurrentCell = new DataGridCell(0, 1);
                    dataGrid1.CurrentCell = new DataGridCell(0, 0);
                }
                catch { }
            }
        }

        private int selectedrowindex = 0;

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            try { dataGrid1.UnSelect(selectedrowindex); }
            catch { }

            try
            {
                selectedrowindex = dataGrid1.CurrentRowIndex;
                dataGrid1.Select(selectedrowindex);
            }
            catch { }
            
            this._mena = this.SelectedMena;

            UpdateForm();
        }

        private string _SelectedMenaID;
        public string SelectedMenaID
        {
            set
            {
                _SelectedMenaID = value;
                FindMenaInView(value);
            }

        }

        public Fask.SQLiteDBs.DataSets.Meny.CZMST097Row SelectedMena
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[_menyView].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Meny.CZMST097Row;
                }
                catch 
                {
                    return null;
                }
            }
            set
            {
                _mena = value;
                FindMenaInView(value);
            }

        }


        public ProdejVyberMeny()
        {
            InitializeComponent();

            this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            _meny = new Fask.SQLiteDBs.DataSets.Meny();
            _menyView = new DataView(this._meny.CZMST097);
            dataGrid1.DataSource = _menyView;

            InitializeDataGridView();

            MyInitializeGrid();

            panel1.Visible = MST_Global.ShowPanelButtons;
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }


        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = _meny.CZMST097.TableName;

            _menaid = new DataGrid2TextBoxColumn();
            _menaid.HeaderText = "ID";
            _menaid.MappingName = _meny.CZMST097.mena_IDColumn.ColumnName;
            _menaid.NullText = "-";
            _menaid.Width = Settings.ProdejVyberStrediskaDocIdWidth;
            ts.GridColumnStyles.Add(_menaid);

            _menadesc = new DataGrid2TextBoxColumn();
            _menadesc.HeaderText = "Popis";
            _menadesc.MappingName = _meny.CZMST097.mena_textColumn.ColumnName;
            _menadesc.NullText = "-";
            _menadesc.Width = Settings.ProdejVyberStrediskaDocDescWidth;
            ts.GridColumnStyles.Add(_menadesc);

            _menadefault = new DataGrid2TextBoxColumn();
            _menadefault.HeaderText = "Výchozí";
            _menadefault.MappingName = _meny.CZMST097.mena_hlavniColumn.ColumnName;
            _menadefault.NullText = "-";
            _menadefault.Width = 10;
            ts.GridColumnStyles.Add(_menadefault);

            _menakurz = new DataGrid2TextBoxColumn();
            _menakurz.HeaderText = "Kurz";
            _menakurz.MappingName = _meny.CZMST097.mena_kurzColumn.ColumnName;
            _menakurz.NullText = "-";
            _menakurz.Width = 10;
            _menakurz.Format = Settings.UIFormatDesCisel;
            ts.GridColumnStyles.Add(_menakurz);

            _menakurzdatum = new DataGrid2TextBoxColumn();
            _menakurzdatum.HeaderText = "Datum kurzu";
            _menakurzdatum.MappingName = _meny.CZMST097.mena_kurzDatumColumn.ColumnName;
            _menakurzdatum.NullText = "-";
            _menakurzdatum.Width = 10;
            ts.GridColumnStyles.Add(_menakurzdatum);

            dataGrid1.TableStyles.Add(ts);
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

        private delegate void StringDelegate(string carkod);


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            string ck = e.BarcodeData.Trim();
            //if (ck != string.Empty)
            //    this.BeginInvoke(new StringDelegate(FindStrediskoByBarcode), new object[] { (object)ck });
        }
        /*
        private void FindMenaByBarcode(string carkod)
        {
            if (carkod.Length > 0)
            {
                _sSelectActual = _sSelectStr + " where str_carcode='" + carkod + "'" + ( _sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _sSelectActualCount = _sSelectStrCount + " where str_carcode='" + carkod + "'" + (_sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _db_record_actual = 0;
                try { LoadStrediska(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                catch { }
                
                if (_meny.CZMST097.Count == 1)
                {
                    this._mena = _meny.CZMST097[0];
                    PerformOK(false);
                }
                else if (_meny.CZMST097.Count > 1)
                {
                    this.SelectedMena = _meny.CZMST097[0];
                }
                else
                {
                    MessageBoxBig.Show("Položka s è.k.'" + carkod + "' nenalezena.");
                }
            }
        }
        */
        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK(true);
        }

        private void PerformOK(bool fromKeyboard)
        {
            if (this.SelectedMena == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberMenyNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            Settings.ProdejVyberStrediskaLastSort = _sOrderStr;
            this.ScannerFinalize();
        }

        private void ProdejVyberStrediska_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            _db_rocords_per_view = Prodej.Globals.GridViewRowCount;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            panel1_Resize(null, null);

            timerLoad.Enabled = true;
            //ProdejVyberOdberatele_Shown(null, null);
        }

        private void ProdejVyberStrediska_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            _sOrderStr = Settings.ProdejVyberMenyLastSort;

            SetSelectActual();

            try { LoadMeny(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(_SelectedMenaID))
                {
                    FindMenaInView(_SelectedMenaID);
                }
                else if (Mena != null)
                    FindMenaInView(_mena);
                else
                {
                    dataGrid1.CurrentCell = new DataGridCell(0, 1);
                    dataGrid1.CurrentCell = new DataGridCell(0, 0);
                }
            }
            catch { }

            this.ScannerStart();
            Cursor.Current = Cursors.Default; 
        }

        private void ProdejVyberStrediska_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            ScannerFinalize();
        }

        private void ProdejVyberStrediska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK(true);
            }
            else if (e.KeyCode == Keys.F1)
            {
                //najdiPolozkuCarovyKod();
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.menuItem3_Click(null, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                FiltrChange(false);
            }
            else if (e.KeyCode == Keys.F3)
            {
                FiltrChange(true);
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void menuItemNajit_Click(object sender, EventArgs e)
        {
            najdiPolozkuCarovyKod();
        }

        private void najdiPolozkuCarovyKod()
        {
            try
            {
                this.ScannerStop();

                string ck = string.Empty;

                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejVyberMenyVlozteCarovyKod, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    ck = skf.Kod;
                }

                //this.FindStrediskoByBarcode(ck);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2, panel1.Height);
            buttonStorno.Width = nsize.Width;
            buttonOK.Width = nsize.Width;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonFirst)
            {
                _db_record_actual = 0;
            }
            else if (e.Button == toolBarButtonPrev)
            {
                _db_record_actual -= _db_rocords_per_view;
                if (_db_record_actual < 0)
                    _db_record_actual = 0;
            }
            else if (e.Button == toolBarButtonNext)
            {
                _db_record_actual += _db_rocords_per_view;
                if (_db_record_actual > (_db_records_count - _db_rocords_per_view))
                    _db_record_actual = _db_records_count - _db_rocords_per_view;

                if (_db_record_actual < 0)
                    _db_record_actual = 0;
            }
            else if (e.Button == toolBarButtonLast)
            {
                _db_record_actual = _db_records_count - _db_rocords_per_view;

                if (_db_record_actual < 0)
                    _db_record_actual = 0;
            }

            try { LoadMeny(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch { }
        }

        /// <summary>
        /// Throws exception if not successfull
        /// </summary>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        private void LoadMeny(int indexStart, int indexEnd)
        {
#if DEBUG
            Logging.Log.Write("LoadMeny Start", "Debug");
#endif
            // pocet zaznamu
            _db_records_count = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_meny.Count(_sSelectActualCount);

            // nacteni zaznamu
            Prodej_3.ProdejMain.prodejInstance.globalObject.controller_meny.Load(_meny.CZMST097, _sSelectActual, indexStart, indexEnd);

            try
            {
                dataGrid1.CurrentCell = new DataGridCell(0, 1);
                dataGrid1.CurrentCell = new DataGridCell(0, 0);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "LoadMeny");
            }

            UpdateForm();

#if DEBUG
            Logging.Log.Write("LoadMeny End", "Debug");
#endif

        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == MST_Global.DataGridScrollDown)
            {
                if (dataGrid1.CurrentRowIndex == _db_rocords_per_view - 1)
                {
                    toolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(toolBarButtonNext));
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == MST_Global.DataGridScrollUp)
            {
                if (dataGrid1.CurrentRowIndex == 0)
                {
                    toolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(toolBarButtonPrev));
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                return;
            }

        }

        private void UpdateForm()
        {
            try
            {
                this.sbInfo.Text =
                    //"D:" + this._cislodavky.ToString() + ", " +
                    "Z:" + (this._db_record_actual + dataGrid1.CurrentRowIndex + 1).ToString() +
                    "(" + this._db_records_count.ToString() + "), " +
                    //"N:" + this._prodejTable.CZMST_DI.Count.ToString() + ", " +
                    //"O:" + (_odberatel != null ? this._odberatel.odb_desc.Trim() : "-");
                    "F:" +
                    (_filtrNazevActual != string.Empty ? "N" : "") +
                    (_filtrStrIdActual != string.Empty ? "O" : "") +
                    (_filtrTypActual != string.Empty ? ("T" + (_filtrTypActual == _filtrTypEmpty ? "!" : "=")) : "") + ", " +
                    "S:" + _sOrderStr;

            }
            catch (Exception ex)
            {
                this.sbInfo.Text = ex.Message;
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            PerformOK(true);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            if (_searchmethod == SearchMethodEnum.Nazev)
            {
                _searchmethod = SearchMethodEnum.None;
                _filtrNazevActual = string.Empty;
                txtSearch.Hide();
                dataGrid1.Focus();
            }
            else
            {
                _searchmethod = SearchMethodEnum.Nazev;
                txtSearch.Show();
                txtSearch.Focus();
            }

            UpdateForm();
        }

        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            //inputPanel1.Enabled = true;
        }

        private void NazevSearch()
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            if (_searchmethod == SearchMethodEnum.Nazev)
            {
                _filtrNazevActual = "str_desc like '%" + txtSearch.Text.Trim() + "%'";
            }
            else
            {
                _filtrNazevActual = string.Empty;
            }

            SetSelectActual();

            _db_record_actual = 0;

            try { LoadMeny(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch (Exception ex)
            {
                // Jina vyjimka nez abort
                Logging.Log.Write(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.NazevSearch();
            //}
            //else
            //    return;

            //e.Handled = true;
        }

        private System.Threading.Timer timerSearch = null;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (timerSearch == null)
                timerSearch = new System.Threading.Timer(new System.Threading.TimerCallback(NazevSearch), null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            timerSearch.Change(100, System.Threading.Timeout.Infinite);

            //this.NazevSearch();
        }

        delegate void VoidDelegate();

        private void NazevSearch(object state)
        {
            this.BeginInvoke(new VoidDelegate(NazevSearch));
            //this.NazevSearch();
        }

        #region Filtry
        private void menuItem13_Click(object sender, EventArgs e)
        {
            FiltrChange(false);
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            FiltrChange(true);
        }

        private void FiltrChange(bool filter)
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            if (filter)
                _filtrTypActual = _filtrTypActual == _filtrTypNotEmpty ? _filtrTypEmpty : _filtrTypNotEmpty;
            else
                _filtrTypActual = string.Empty;

            SetSelectActual();

            _db_record_actual = 0;

            try { LoadMeny(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch (Exception ex)
            {
                // Jina vyjimka nez abort
                Logging.Log.Write(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Razeni
        enum SortOrder
        {
            None,
            CarKod,
            Nazev
        }
        private void SortChange(SortOrder sortOrder)
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            if (sortOrder == SortOrder.CarKod)
                _sOrderStr = _sOrderStr == _sortCarCodeAsc ? _sortCarCodeDsc : _sortCarCodeAsc;
            else if (sortOrder == SortOrder.Nazev)
                _sOrderStr = _sOrderStr == _sortNazevAsc ? _sortNazevDsc : _sortNazevAsc;
            else
                _sOrderStr = string.Empty;


            SetSelectActual();

            _db_record_actual = 0;

            try { LoadMeny(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch (Exception ex)
            {
                // Jina vyjimka nez abort
                Logging.Log.Write(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void menuItem11_Click(object sender, EventArgs e)
        {
            SortChange(SortOrder.None);
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            SortChange(SortOrder.CarKod);
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            SortChange(SortOrder.Nazev);
        }

        private void menuItemAktualize_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
            ciselnikS = new _WebRefernces_Globals.CiselnikServiceSession();
            ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
            ciselnikS.Timeout = MST_Global.ServiceTimeOut;
            ciselnikS.UpdateWebServiceCredentials();
            
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMen(ciselnikS);

            this.ProdejVyberStrediska_Shown(null, null);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            bezCiziMeny = true;
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void ProdejVyberMeny_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ProdejVyberMeny_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}