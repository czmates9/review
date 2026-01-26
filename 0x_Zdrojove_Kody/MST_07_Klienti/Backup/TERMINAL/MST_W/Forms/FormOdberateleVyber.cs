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

namespace Fask.MST_W.Forms
{
    public partial class FormOdberateleVyber : System.Windows.Forms.Form
    {
        private int _db_records_count = 0;
        private int _db_record_actual = 0;
        private int _db_rocords_per_view = 8;

        private enum SearchMethodEnum
        {
            Nazev,
            None
        }

        private SearchMethodEnum _searchmethod = SearchMethodEnum.None;

        private readonly string _sSelectOdb = "Select * from czmst090";
        private readonly string _sSelectOdbCount = "Select Count(*) from czmst090";
        private string _sWhereOdb = string.Empty;
        private string _sOrderOdb = string.Empty;
        private string _sSelectActual = string.Empty;
        private string _sSelectActualCount = string.Empty;
        private string _filtrNazevActual = string.Empty;
        private string _filtrOdbIdActual = string.Empty;
        private string _filtrTypActual = string.Empty;
        private const string _filtrTypEmpty = "odb_typ=''";
        private const string _filtrTypNotEmpty = "odb_typ<>''";
        private const string _sortNazevAsc = "odb_desc asc";
        private const string _sortNazevDsc = "odb_desc desc";
        private const string _sortCarCodeAsc = "odb_carcode asc";
        private const string _sortCarCodeDsc = "odb_carcode desc";
        private void SetSelectActual()
        {
            _sSelectActual = _sSelectOdb;
            _sSelectActualCount = _sSelectOdbCount;
            _sWhereOdb = string.Empty;
            if (_filtrOdbIdActual != string.Empty)
            {
                _sWhereOdb = _filtrOdbIdActual;
            }
            if (_filtrNazevActual != string.Empty)
            {
                if (_sWhereOdb != string.Empty) _sWhereOdb += " AND ";
                _sWhereOdb = _filtrNazevActual;
            }
            if (_filtrTypActual != string.Empty)
            {
                if (_sWhereOdb != string.Empty) _sWhereOdb += " AND ";
                _sWhereOdb += _filtrTypActual;
            }
            if (_sWhereOdb != string.Empty)
            {
                _sSelectActual += " where " + _sWhereOdb;
                _sSelectActualCount += " where " + _sWhereOdb;
            }
            if (_sOrderOdb != string.Empty)
            {
                _sSelectActual += " order by " + _sOrderOdb;
                _sSelectActualCount += " order by " + _sOrderOdb;
            }
        }

        private Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele = null;

        private Fask.SQLiteDBs.DataSets.Odberatele _odberatele = null;
        private DataView _odberateleView = null;

        private DataGrid2TextBoxColumn _odbid;
        private DataGrid2TextBoxColumn _odbdesc;
        private DataGrid2TextBoxColumn _odbtyp;
        private DataGrid2TextBoxColumn _odbck;
        private DataGrid2TextBoxColumn _odbmena;

        private DataGrid2TextBoxColumn _odbico;
        private DataGrid2TextBoxColumn _odbdic;
        private DataGrid2TextBoxColumn _odbulice;
        private DataGrid2TextBoxColumn _odbcisloOr;
        private DataGrid2TextBoxColumn _odbpsc;
        private DataGrid2TextBoxColumn _odbJeOdb;
        private DataGrid2TextBoxColumn _odbJeDod;

        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row _odberatel = null;
        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Odberatel
        {
            get { return _odberatel; }
        }

        private void FindOdberatelInView(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row value)
        {
            DataTable dt = _odberateleView.ToTable();
            DataRow[] rows = dt.Select("odb_id='" + value.odb_id + "'");
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

            this._odberatel = this.SelectedOdberatel;

            UpdateForm();
        }
        
        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row SelectedOdberatel
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[_odberateleView].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row;
                }
                catch 
                {
                    return null;
                }
            }
            set
            {
                _odberatel = value;
                FindOdberatelInView(value);
            }

        }


        public FormOdberateleVyber()
        {
            InitializeComponent();

            dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            _odberatele = new Fask.SQLiteDBs.DataSets.Odberatele(); 
            _odberateleView = new DataView(this._odberatele.CZMST090);
            dataGrid1.DataSource = _odberateleView;

            InitializeDataGridView();

            MyInitializeGrid();

            panel1.Visible = MST_Global.ShowPanelButtons;
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = _odberatele.CZMST090.TableName;

            _odbid = new DataGrid2TextBoxColumn();
            _odbid.HeaderText = "ID";
            _odbid.MappingName = _odberatele.CZMST090.odb_idColumn.ColumnName;
            _odbid.NullText = "-";
            _odbid.Width = Settings.ProdejVyberOdberateleOdbIdWidth;
            ts.GridColumnStyles.Add(_odbid);

            _odbdesc = new DataGrid2TextBoxColumn();
            _odbdesc.HeaderText = "Popis";
            _odbdesc.MappingName = _odberatele.CZMST090.odb_descColumn.ColumnName;
            _odbdesc.NullText = "-";
            _odbdesc.Width = Settings.ProdejVyberOdberateleOdbDescWidth;
            ts.GridColumnStyles.Add(_odbdesc);

            _odbtyp = new DataGrid2TextBoxColumn();
            _odbtyp.HeaderText = "Typ";
            _odbtyp.MappingName = _odberatele.CZMST090.odb_typColumn.ColumnName;
            _odbtyp.NullText = "-";
            _odbtyp.Width = Settings.ProdejVyberOdberateleOdbTypWidth;
            ts.GridColumnStyles.Add(_odbtyp);

            _odbck = new DataGrid2TextBoxColumn();
            _odbck.HeaderText = "Èár. kód";
            _odbck.MappingName = _odberatele.CZMST090.odb_carcodeColumn.ColumnName;
            _odbck.NullText = "-";
            _odbck.Width = Settings.ProdejVyberOdberateleOdbCarKodWidth;
            ts.GridColumnStyles.Add(_odbck);

            _odbmena = new DataGrid2TextBoxColumn();
            _odbmena.HeaderText = "Mìna";
            _odbmena.MappingName = _odberatele.CZMST090.mena_IDColumn.ColumnName;
            _odbmena.NullText = "-";
            _odbmena.Width = Settings.ProdejVyberOdberateleOdbCarKodWidth;
            ts.GridColumnStyles.Add(_odbmena);

            _odbico = new DataGrid2TextBoxColumn();
            _odbico.HeaderText = "IÈO";
            _odbico.MappingName = _odberatele.CZMST090.odb_icoColumn.ColumnName;
            _odbico.NullText = "-";
            _odbico.Width = 10;
            ts.GridColumnStyles.Add(_odbico);

            _odbdic = new DataGrid2TextBoxColumn();
            _odbdic.HeaderText = "DIÈ";
            _odbdic.MappingName = _odberatele.CZMST090.odb_dicColumn.ColumnName;
            _odbdic.NullText = "-";
            _odbdic.Width = 10;
            ts.GridColumnStyles.Add(_odbdic);

            _odbulice = new DataGrid2TextBoxColumn();
            _odbulice.HeaderText = "Ulice";
            _odbulice.MappingName = _odberatele.CZMST090.odb_uliceColumn.ColumnName;
            _odbulice.NullText = "-";
            _odbulice.Width = 10;
            ts.GridColumnStyles.Add(_odbulice);

            _odbcisloOr = new DataGrid2TextBoxColumn();
            _odbcisloOr.HeaderText = "Or. èíslo";
            _odbcisloOr.MappingName = _odberatele.CZMST090.odb_cisloOrColumn.ColumnName;
            _odbcisloOr.NullText = "-";
            _odbcisloOr.Width = 10;
            ts.GridColumnStyles.Add(_odbcisloOr);

            _odbpsc = new DataGrid2TextBoxColumn();
            _odbpsc.HeaderText = "PSÈ";
            _odbpsc.MappingName = _odberatele.CZMST090.odb_pscColumn.ColumnName;
            _odbpsc.NullText = "-";
            _odbpsc.Width = 10;
            ts.GridColumnStyles.Add(_odbpsc);

            _odbJeOdb = new DataGrid2TextBoxColumn();
            _odbJeOdb.HeaderText = "Odbìratel";
            _odbJeOdb.MappingName = _odberatele.CZMST090.odb_OdberatelColumn.ColumnName;
            _odbJeOdb.NullText = "-";
            _odbJeOdb.Width = 10;
            ts.GridColumnStyles.Add(_odbJeOdb);

            _odbJeDod = new DataGrid2TextBoxColumn();
            _odbJeDod.HeaderText = "Dodavatel";
            _odbJeDod.MappingName = _odberatele.CZMST090.odb_DodavatelColumn.ColumnName;
            _odbJeDod.NullText = "-";
            _odbJeDod.Width = 10;
            ts.GridColumnStyles.Add(_odbJeDod);


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
            if (ck != string.Empty)
                this.BeginInvoke(new StringDelegate(FindOdberatelByBarcode), new object[] { (object)ck });
        }

        private void FindOdberatelByBarcode(string carkod)
        {
            if (carkod.Length > 0)
            {
                _sSelectActual = _sSelectOdb + " where odb_carcode='" + carkod + "'" + ( _sOrderOdb == string.Empty ? string.Empty : " order by " + _sOrderOdb);
                _sSelectActualCount = _sSelectOdbCount + " where odb_carcode='" + carkod + "'" + (_sOrderOdb == string.Empty ? string.Empty : " order by " + _sOrderOdb);
                _db_record_actual = 0;
                try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                catch { }
                
                if (_odberatele.CZMST090.Count == 1)
                {
                    this._odberatel = _odberatele.CZMST090[0];
                    PerformOK();
                }
                else if (_odberatele.CZMST090.Count > 1)
                {
                    this.SelectedOdberatel = _odberatele.CZMST090[0];
                }
                else
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberOdberatelePolozkaCarKodNenalezena, carkod));
                }
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            try
            {
                if (this._odberatel == null)
                {
                    MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejVyberOdberateleNeniVybratOdberatel, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Výbìr odbìratele", MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }

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
            if (controller_odberatele != null)
                controller_odberatele.Dispose();
            Settings.ProdejVyberOdberateleLastSort = _sOrderOdb;
            this.ScannerFinalize();
        }


        private void ProdejVyberOdberatele_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Size = Forms.FormLocation.ScreenResolution;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            _db_rocords_per_view = Prodej.Globals.GridViewRowCount;

            panel1_Resize(null, null);

            timerLoad.Enabled = true;
            //ProdejVyberOdberatele_Shown(null, null);
        }

        private void ProdejVyberOdberatele_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Main.CiselnikOdberateleDB);

            _sOrderOdb = Settings.ProdejVyberOdberateleLastSort;

            SetSelectActual();
            try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch { }

            try
            {
                dataGrid1.CurrentCell = new DataGridCell(0, 1);
                dataGrid1.CurrentCell = new DataGridCell(0, 0);
            }
            catch { }

            this.ScannerStart();
            Cursor.Current = Cursors.Default; 
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }


        private void ProdejVyberOdberatele_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            ScannerFinalize();
        }

        private void ProdejVyberOdberatele_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.F1)
            {
                najdiPolozkuCarovyKod();
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
            string ck = string.Empty;

            try
            {
                this.ScannerStop();

                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejVyberOdberateleVlozteCarovyKod, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    ck = skf.Kod;
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
                return;
            }
            finally
            {
                this.ScannerStart();
            }

            this.FindOdberatelByBarcode(ck);
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

            try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch { }
        }

        /// <summary>
        /// Throws exception if not successfull
        /// </summary>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        private void LoadOdberatele(int indexStart, int indexEnd)
        {

            _db_records_count = controller_odberatele.Count(_sSelectActualCount);

            controller_odberatele.Load(_odberatele.CZMST090, _sSelectActual, indexStart, indexEnd);

            try
            {
                dataGrid1.CurrentCell = new DataGridCell(0, 1);
                dataGrid1.CurrentCell = new DataGridCell(0, 0);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "LoadOdberatele");
            }

            UpdateForm();


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
                string sortorder = "";

                if (_sOrderOdb == "odb_desc asc")
                    sortorder = "Popis A-Z";
                else if (_sOrderOdb == "odb_desc desc")
                    sortorder = "Popis Z-A";
                else if (_sOrderOdb == "odb_carcode asc")
                    sortorder = "Èár. kód A-Z";
                else if (_sOrderOdb == "odb_carcode desc")
                    sortorder = "Èár. kód Z-A";

                this.sbInfo.Text =
                    //"D:" + this._cislodavky.ToString() + ", " +
                    "Z:" + (this._db_record_actual + dataGrid1.CurrentRowIndex + 1).ToString() +
                    "(" + this._db_records_count.ToString() + "), " +
                    //"N:" + this._prodejTable.CZMST_DI.Count.ToString() + ", " +
                    //"O:" + (_odberatel != null ? this._odberatel.odb_desc.Trim() : "-");
                    "F:" +
                    (_filtrNazevActual != string.Empty ? "N" : "") +
                    (_filtrOdbIdActual != string.Empty ? "O" : "") +
                    (_filtrTypActual != string.Empty ? ("T" + (_filtrTypActual == _filtrTypEmpty ? "!" : "=")) : "") + ", " +
                    "S:" + sortorder;
            }
            catch (Exception ex)
            {
                this.sbInfo.Text = ex.Message;
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            PerformOK();
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
                _filtrNazevActual = "odb_desc like '%" + txtSearch.Text.Trim() + "%'";
            }
            else
            {
                _filtrNazevActual = string.Empty;
            }

            SetSelectActual();

            _db_record_actual = 0;

            try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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
            FiltrChange(true);
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            FiltrChange(false);
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

            try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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
                _sOrderOdb = _sOrderOdb == _sortCarCodeAsc ? _sortCarCodeDsc : _sortCarCodeAsc;
            else if (sortOrder == SortOrder.Nazev)
                _sOrderOdb = _sOrderOdb == _sortNazevAsc ? _sortNazevDsc : _sortNazevAsc;
            else
                _sOrderOdb = string.Empty;


            SetSelectActual();

            _db_record_actual = 0;

            try { LoadOdberatele(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberatele(ciselnikS, Prodej.Globals.SkladID);

            this.ProdejVyberOdberatele_Shown(null, null);
        }

        private void ProdejVyberOdberatele_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ProdejVyberOdberatele_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }
    }
}