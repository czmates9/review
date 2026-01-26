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

namespace Fask.MST_W.Inventura2
{
    public partial class ZmenaKancl : System.Windows.Forms.Form
    {
        private int _db_records_count = 0;
        private int _db_record_actual = 0;
        private int _db_rocords_per_view = 10;

        private enum SearchMethodEnum
        {
            Nazev,
            None
        }

        private SearchMethodEnum _searchmethod = SearchMethodEnum.None;

        private readonly string _sSelectStr = "Select * from kancl";
        private readonly string _sSelectStrCount = "Select Count(*) from kancl";
        private string _sWhereStr = string.Empty;
        private string _sOrderStr = string.Empty;
        private string _sSelectActual = string.Empty;
        private string _sSelectActualCount = string.Empty;
        private string _filtrNazevActual = string.Empty;
        private const string _sortNazevAsc = "text asc";
        private const string _sortNazevDsc = "text desc";
        private const string _sortCarCodeAsc = "kancl asc";
        private const string _sortCarCodeDsc = "kancl desc";
        private void SetSelectActual()
        {
            _sSelectActual = _sSelectStr;
            _sSelectActualCount = _sSelectStrCount;
            _sWhereStr = string.Empty;
            if (_filtrNazevActual != string.Empty)
            {
                if (_sWhereStr != string.Empty) _sWhereStr += " AND ";
                _sWhereStr = _filtrNazevActual;
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

        //private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter _ta_kancl = null;

        private Fask.SQLiteDBs.DataSets.Inventura2 _inventura2 = null;
        private DataView _inventura2View = null;

        private DataGrid2TextBoxColumn _dg_kancl;
        private DataGrid2TextBoxColumn _dg_stre;
        private DataGrid2TextBoxColumn _dg_text;
        private DataGrid2TextBoxColumn _dg_nazev;
        private DataGrid2TextBoxColumn _dg_ean;

        private Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow _kancelar = null;
        public Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow Kancelar
        {
            get { return _kancelar; }
            set { _kancelar = value; }
        }

        private void FindInView(Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow value)
        {
            DataTable dt = _inventura2View.ToTable();
            DataRow[] rows = dt.Select("kancl=" + value.KANCL);
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

            this._kancelar = this.SelectedRow;

            UpdateForm();
        }

        private Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow SelectedRow
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[_inventura2View].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow;
                }
                catch 
                {
                    return null;
                }
            }
            set
            {
                _kancelar = value;
                FindInView(value);
            }

        }


        public ZmenaKancl()
        {
            InitializeComponent();

            this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2(); 
            _inventura2View = new DataView(this._inventura2.KANCL);
            dataGrid1.DataSource = _inventura2View;

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
            ts.MappingName = _inventura2.KANCL.TableName;

            _dg_kancl = new DataGrid2TextBoxColumn();
            _dg_kancl.HeaderText = "Kanceláø";
            _dg_kancl.MappingName = _inventura2.KANCL.KANCLColumn.ColumnName;
            _dg_kancl.NullText = "-";
            _dg_kancl.Width = Settings.Inventura2ZmenaKanclKanclWidth;
            ts.GridColumnStyles.Add(_dg_kancl);

            _dg_stre = new DataGrid2TextBoxColumn();
            _dg_stre.HeaderText = "Støedisko";
            _dg_stre.MappingName = _inventura2.KANCL.STREColumn.ColumnName;
            _dg_stre.NullText = "-";
            _dg_stre.Width = Settings.Inventura2ZmenaKanclStreWidth;
            ts.GridColumnStyles.Add(_dg_stre);

            _dg_text = new DataGrid2TextBoxColumn();
            _dg_text.HeaderText = "Text";
            _dg_text.MappingName = _inventura2.KANCL.TEXTColumn.ColumnName;
            _dg_text.NullText = "-";
            _dg_text.Width = Settings.Inventura2ZmenaKanclTextWidth;
            ts.GridColumnStyles.Add(_dg_text);

            _dg_nazev = new DataGrid2TextBoxColumn();
            _dg_nazev.HeaderText = "Název";
            _dg_nazev.MappingName = _inventura2.KANCL.NAZEVColumn.ColumnName;
            _dg_nazev.NullText = "-";
            _dg_nazev.Width = Settings.Inventura2ZmenaKanclNazevWidth;
            ts.GridColumnStyles.Add(_dg_nazev);

            _dg_ean = new DataGrid2TextBoxColumn();
            _dg_ean.HeaderText = "EAN";
            _dg_ean.MappingName = _inventura2.KANCL.EANColumn.ColumnName;
            _dg_ean.NullText = "-";
            _dg_ean.Width = Settings.Inventura2ZmenaKanclEANWidth;
            ts.GridColumnStyles.Add(_dg_ean);

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
                this.BeginInvoke(new StringDelegate(FindByBarcode), new object[] { (object)ck });
        }

        private void FindByBarcode(string carkod)
        {
            if (carkod.Length > 0)
            {
                _sSelectActual = _sSelectStr + " where kancl='" + carkod + "'" + ( _sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _sSelectActualCount = _sSelectStrCount + " where kancl='" + carkod + "'" + (_sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _db_record_actual = 0;
                try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                catch { }
                
                if (_inventura2.KANCL.Count == 1)
                {
                    this._kancelar = _inventura2.KANCL[0];
                    PerformOK(false);
                }
                else if (_inventura2.KANCL.Count > 1)
                {
                    this.SelectedRow = _inventura2.KANCL[0];
                }
                else
                {   //hledani podle EANu...
                    _sSelectActual = _sSelectStr + " where ean='" + carkod + "'" + (_sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                    _sSelectActualCount = _sSelectStrCount + " where ean='" + carkod + "'" + (_sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                    _db_record_actual = 0;
                    try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                    catch { }

                    if (_inventura2.KANCL.Count == 1)
                    {
                        this._kancelar = _inventura2.KANCL[0];
                        PerformOK(false);
                    }
                    else if (_inventura2.KANCL.Count > 1)
                    {
                        this.SelectedRow = _inventura2.KANCL[0];
                    }
                    else
                    {
                        MessageBoxBig.Show("Položka s è.k.'" + carkod + "' nenalezena.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    }
                }
            }
            if (MST_Global.OnScannerSound_Inventura2)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK(true);
        }

        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            this.ScannerFinalize();
        }

        private void PerformOK(bool fromKeyboard)
        {
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

        private void ZmenaKancl_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            panel1_Resize(null, null);

            timerLoad.Enabled = true;
            //ProdejVyberOdberatele_Shown(null, null);
        }

        private void ZmenaKancl_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			//_ta_kancl = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter();
			//_ta_kancl.ClearBeforeFill = true;
			////_ta_lokace.Connection.ConnectionString = "Data source=" + Main.CiselnikStrediskaDB;
			//_ta_kancl.Connection = Globals.active_connection;

            //System.Data.SqlServerCe.SqlCeDataAdapter sceda = new System.Data.SqlServerCe.SqlCeDataAdapter(
            //    "Select * from czmst090",
            //    "Data source=" + Main.CiselnikOdberateleDB
            //    );
            SetSelectActual();

            try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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

        private void ZmenaKancl_Closing(object sender, CancelEventArgs e)
        {
            /*
            Settings.Inventura2ZmenaKanclKanclWidth = _dg_kancl.Width;
            Settings.Inventura2ZmenaKanclStreWidth = _dg_stre.Width;
            Settings.Inventura2ZmenaKanclTextWidth = _dg_text.Width;*/
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));


            ScannerStop();
        }

        private void ZmenaKancl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (dataGrid1.CurrentRowIndex == 0 && _db_record_actual > 0)
                {
                    _db_record_actual -= _db_rocords_per_view;

                    if (_db_record_actual < 0)
                        _db_record_actual = 0;

                    try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                    catch { }
                    dataGrid1.CurrentRowIndex = _inventura2.KANCL.Count - 1;
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dataGrid1.CurrentRowIndex == _inventura2.KANCL.Count - 1 && (_db_record_actual + _db_rocords_per_view < _db_records_count))
                {
                    _db_record_actual += _db_rocords_per_view;

                    try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                    catch { }
                    e.Handled = true;
                    return;
                }
            }

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
            try
            {
                this.ScannerStop();

                string ck = string.Empty;

                using (SejmiKodForm skf = new SejmiKodForm("Vložte èárový kód", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    ck = skf.Kod;
                }

                this.FindByBarcode(ck);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
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

            try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            catch { }
        }

        /// <summary>
        /// Throws exception if not successfull
        /// </summary>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        private void LoadData(int indexStart, int indexEnd)
        {

			//bool connectionopened = false;

			//using (var scecommand = _ta_kancl.Connection.CreateCommand())
			//{
			//    try
			//    {
			//        connectionopened = scecommand.Connection.State == ConnectionState.Open;
			//        if (!connectionopened)
			//            scecommand.Connection.Open();

			//        // Records count
			//        scecommand.CommandText = _sSelectActualCount;
			//        scecommand.CommandType = CommandType.Text;
			//        _db_records_count = Convert.ToInt32(scecommand.ExecuteScalar());


			//        // Records
			//        scecommand.CommandText = _sSelectActual;
			//        scecommand.CommandType = CommandType.Text;

			//        //System.Data.SqlServerCe.SqlCeDataReader scedatareader = scecommand.ExecuteReader(CommandBehavior.CloseConnection);
			//        //System.Data.SqlServerCe.SqlCeResultSet sceresultset = scecommand.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Scrollable);
			//        var sceresultset = scecommand.ExecuteReader();

			//        _inventura2.KANCL.BeginLoadData();
			//        _inventura2.Clear();

			//        //if (sceresultset.ReadAbsolute(indexStart))
			//        if (true)
			//        {
			//            for (int index = 0; index < indexStart; index++)
			//            {
			//                if (!sceresultset.Read())
			//                    break;
			//            }

			//            int i = indexStart;
			//            do
			//            {
			//                //object[] values = new object[sceresultset.FieldCount];
			//                //sceresultset.GetValues(values);
			//                //_inventura2.KANCL.LoadDataRow(values, true);
			//                Fask.SQLiteDBs.Controllers._Routines.LoadRowFromReader(sceresultset, _inventura2.KANCL);

			//                //SqlCEDBs.DataSets.Inventura2.KANCLRow row = _inventura2.KANCL.NewLOKACERow();

			//                //row. = (int)sceresultset["DEX_ROW_ID"];
			//                //row.str_carcode = (string)sceresultset["str_carcode"] ?? string.Empty;
			//                //row.str_desc = (string)sceresultset["str_desc"] ?? string.Empty;
			//                //row.str_id = (string)sceresultset["str_id"] ?? string.Empty;
			//                //row.str_typ = (string)sceresultset["str_typ"] ?? string.Empty;

			//                //_inventura2.KANCL.AddCZMST091Row(row);

			//                i++;
			//                if (!sceresultset.Read())
			//                    break;
			//            } while (i < indexEnd);
			//        }
			//        _inventura2.KANCL.EndLoadData();

			//        sceresultset.Close();
			//    }
			//    catch (Exception ex)
			//    {
			//        //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			//        Logging.Log.Write(ex.Message, "LoadData");
			//        throw ex;
			//    }
			//    finally
			//    {
			//        if (!connectionopened && scecommand.Connection.State == ConnectionState.Open)
			//            scecommand.Connection.Close();
			//    }
			//}

			Fask.MST_W.Inventura2.Inventura2.Inventura2_Instance.globalObject.controller_inventura2.LoadKancl(_inventura2.KANCL, _sSelectActual, indexStart, indexEnd);

            try
            {
                dataGrid1.CurrentCell = new DataGridCell(0, 1);
                dataGrid1.CurrentCell = new DataGridCell(0, 0);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "LoadData");
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
                this.sbInfo.Text =
                    //"D:" + this._cislodavky.ToString() + ", " +
                    "Z:" + (this._db_record_actual + dataGrid1.CurrentRowIndex + 1).ToString() +
                    "(" + this._db_records_count.ToString() + "), " +
                    //"N:" + this._prodejTable.CZMST_DI.Count.ToString() + ", " +
                    //"O:" + (_odberatel != null ? this._odberatel.odb_desc.Trim() : "-");
                    "F:" +
                    (_filtrNazevActual != string.Empty ? "N" : "") +
                    //(_filtrStrIdActual != string.Empty ? "O" : "") +
                    //(_filtrTypActual != string.Empty ? ("T" + (_filtrTypActual == _filtrTypEmpty ? "!" : "=")) : "") + ", " +
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
                _filtrNazevActual = "text like '" + txtSearch.Text.Trim() + "%'";
            }
            else
            {
                _filtrNazevActual = string.Empty;
            }

            SetSelectActual();

            _db_record_actual = 0;

            try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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
            //FiltrChange(false);
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            //FiltrChange(true);
        }

        private void FiltrChange(bool filter)
        {
            //Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            //if (filter)
            //    _filtrTypActual = _filtrTypActual == _filtrTypNotEmpty ? _filtrTypEmpty : _filtrTypNotEmpty;
            //else
            //    _filtrTypActual = string.Empty;

            //SetSelectActual();

            //_db_record_actual = 0;

            //try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
            //catch (Exception ex)
            //{
            //    // Jina vyjimka nez abort
            //    Logging.Log.Write(ex.Message);
            //}

            //Cursor.Current = Cursors.Default;
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

            try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
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

        private void menuItem12_Click(object sender, EventArgs e)
        {
            _kancelar = null;
            this.PerformOK(false);
        }

        private void ZmenaKancl_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ZmenaKancl_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}