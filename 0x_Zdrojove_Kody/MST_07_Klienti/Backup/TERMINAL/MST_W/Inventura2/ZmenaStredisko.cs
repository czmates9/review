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
    public partial class ZmenaStredisko : System.Windows.Forms.Form
    {
        private int _db_records_count = 0;
        private int _db_record_actual = 0;
        private int _db_rocords_per_view = 10;

        private enum SearchMethodEnum
        {
            None,
            Nazev
        }

        enum SortOrder
        {
            None,            
            Nazev,
            Stredisko
        }

        private SearchMethodEnum _searchmethod = SearchMethodEnum.None;
        private SortOrder _sortorder = SortOrder.None;

        private readonly string _sSelectStr = "Select * from ucstr";
        private readonly string _sSelectStrCount = "Select Count(*) from ucstr";
        private string _sWhereStr = string.Empty;
        private string _sOrderStr = string.Empty;
        private string _sSelectActual = string.Empty;
        private string _sSelectActualCount = string.Empty;
        private string _filtrActual = string.Empty;
        private const string _sortNazevAsc = "nazev asc";
        private const string _sortNazevDsc = "nazev desc";
        private const string _sortStrediskoAsc = "stredisko asc";
        private const string _sortStrediskoDsc = "stredisko dsc";
        private void SetSelectActual()
        {
            _sSelectActual = _sSelectStr;
            _sSelectActualCount = _sSelectStrCount;
            _sWhereStr = string.Empty;
            if (_filtrActual != string.Empty)
            {
                if (_sWhereStr != string.Empty) _sWhereStr += " AND ";
                _sWhereStr = _filtrActual;
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

        //private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter _ta_stredisko = null;

		private Fask.SQLiteDBs.DataSets.Inventura2 _inventura2 = null;
        private DataView _inventura2View = null;

        private DataGrid2TextBoxColumn _dg_stredisko;
        private DataGrid2TextBoxColumn _dg_nazev;

		private Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow _stredisko = null;
		public Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow Stredisko
        {
            get { return _stredisko; }
            set { _stredisko = value; }
        }

        private void FindInView(Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow value)
        {
            DataTable dt = _inventura2View.ToTable();
            DataRow[] rows = dt.Select("klic_lok='" + value.STREDISKO.Trim() + "'");
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

            this._stredisko = this.SelectedRow;

            UpdateForm();
        }

        private Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow SelectedRow
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[_inventura2View].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow;
                }
                catch 
                {
                    return null;
                }
            }
            set
            {
                _stredisko = value;
                FindInView(value);
            }

        }



        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }


        public ZmenaStredisko()
        {
            InitializeComponent();

            this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2(); 
            _inventura2View = new DataView(this._inventura2.UCSTR);
            dataGrid1.DataSource = _inventura2View;

            InitializeDataGridView();

            MyInitializeGrid();

            panel1.Visible = MST_Global.ShowPanelButtons;
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = _inventura2.UCSTR.TableName;

            _dg_stredisko = new DataGrid2TextBoxColumn();
            _dg_stredisko.HeaderText = "Støedisko";
            _dg_stredisko.MappingName = _inventura2.UCSTR.STREDISKOColumn.ColumnName;
            _dg_stredisko.NullText = "-";
            _dg_stredisko.Width = Settings.Inventura2ZmenaStrediskoStrediskoWidth;
            ts.GridColumnStyles.Add(_dg_stredisko);


            _dg_nazev = new DataGrid2TextBoxColumn();
            _dg_nazev.HeaderText = "Název";
            _dg_nazev.MappingName = _inventura2.UCSTR.NAZEVColumn.ColumnName;
            _dg_nazev.NullText = "-";
            _dg_nazev.Width = Settings.Inventura2ZmenaStrediskoNazevWidth;
            ts.GridColumnStyles.Add(_dg_nazev);

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
                _sSelectActual = _sSelectStr + " where stredisko='" + carkod + "'" + ( _sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _sSelectActualCount = _sSelectStrCount + " where stredisko='" + carkod + "'" + (_sOrderStr == string.Empty ? string.Empty : " order by " + _sOrderStr);
                _db_record_actual = 0;
                try { LoadData(_db_record_actual, _db_record_actual + _db_rocords_per_view); }
                catch { }
                
                if (_inventura2.UCSTR.Count == 1)
                {
                    this._stredisko = _inventura2.UCSTR[0];
                    PerformOK(false);
                }
                else if (_inventura2.UCSTR.Count > 1)
                {
                    this.SelectedRow = _inventura2.UCSTR[0];
                }
                else
                {
                    MessageBoxBig.Show("Položka s è.k.'" + carkod + "' nenalezena.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
            this.finalize();
            DialogResult = DialogResult.OK;
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

        private void ZmenaStredisko_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            panel1_Resize(null, null);

            timerLoad.Enabled = true;
            //ProdejVyberOdberatele_Shown(null, null);
        }

        private void ZmenaStredisko_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            //_ta_stredisko = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter();
            //_ta_stredisko.ClearBeforeFill = true;
            //_ta_lokace.Connection.ConnectionString = "Data source=" + Main.CiselnikStrediskaDB;
            //_ta_stredisko.Connection = Globals.active_connection;

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

        private void ZmenaLokace_Closing(object sender, CancelEventArgs e)
        {
            //Settings.Inventura2ZmenaStrediskoStrediskoWidth = _dg_stredisko.Width;
            //Settings.Inventura2ZmenaStrediskoNazevWidth = _dg_nazev.Width;

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            ScannerStop();
        }

        private void ZmenaStredisko_KeyDown(object sender, KeyEventArgs e)
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
                    dataGrid1.CurrentRowIndex = _inventura2.UCSTR.Count - 1;
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dataGrid1.CurrentRowIndex == _inventura2.UCSTR.Count - 1 && (_db_record_actual + _db_rocords_per_view < _db_records_count))
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
                FiltrChange(SearchMethodEnum.None);
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

			//using (var scecommand = _ta_stredisko.Connection.CreateCommand())
			//{
			//    try
			//    {
			//        connectionopened = scecommand.Connection.State == ConnectionState.Open;
			//        if (!connectionopened)
			//            scecommand.Connection.Open();

			//        scecommand.CommandText = _sSelectActualCount;
			//        scecommand.CommandType = CommandType.Text;
			//        _db_records_count = Convert.ToInt32(scecommand.ExecuteScalar());

			//        scecommand.CommandText = _sSelectActual;
			//        scecommand.CommandType = CommandType.Text;
			//        var sceresultset = scecommand.ExecuteReader();

			//        _inventura2.UCSTR.BeginLoadData();
			//        _inventura2.Clear();

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
			//                //_inventura2.UCSTR.LoadDataRow(values, true);
			//                Fask.SQLiteDBs.Controllers._Routines.LoadRowFromReader(sceresultset, _inventura2.UCSTR);

			//                //SqlCEDBs.DataSets.Inventura2.UCSTRRow row = _inventura2.LOKACE.NewLOKACERow();

			//                //row. = (int)sceresultset["DEX_ROW_ID"];
			//                //row.str_carcode = (string)sceresultset["str_carcode"] ?? string.Empty;
			//                //row.str_desc = (string)sceresultset["str_desc"] ?? string.Empty;
			//                //row.str_id = (string)sceresultset["str_id"] ?? string.Empty;
			//                //row.str_typ = (string)sceresultset["str_typ"] ?? string.Empty;

			//                //_inventura2.LOKACE.AddCZMST091Row(row);

			//                i++;
			//                if (!sceresultset.Read())
			//                    break;
			//            } while (i < indexEnd);
			//        }
			//        _inventura2.UCSTR.EndLoadData();

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
			//        if (!connectionopened && ((scecommand.Connection.State & ConnectionState.Open) == ConnectionState.Open))
			//            scecommand.Connection.Close();
			//    }
            //}

			Fask.MST_W.Inventura2.Inventura2.Inventura2_Instance.globalObject.controller_inventura2.LoadStrediska(_inventura2.UCSTR, _sSelectActual, indexStart, indexEnd);


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
                    "F:" + (this._searchmethod.ToString()[0]) + ", " +
                    //(_filtrActual != string.Empty ? "N" : "") +
                    //(_filtrStrIdActual != string.Empty ? "O" : "") +
                    //(_filtrTypActual != string.Empty ? ("T" + (_filtrTypActual == _filtrTypEmpty ? "!" : "=")) : "") + ", " +
                    "S:" + this._sortorder.ToString()[0]; //_sOrderStr;

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
                _filtrActual = "nazev like '" + txtSearch.Text.Trim() + "%'";
            }
            else
            {
                _filtrActual = string.Empty;
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
            FiltrChange(SearchMethodEnum.Nazev);
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            FiltrChange(SearchMethodEnum.None);
        }

        private void FiltrChange(SearchMethodEnum smethod)
        {

            _searchmethod = smethod;

            if (_searchmethod == SearchMethodEnum.Nazev)
            {
                _searchmethod = SearchMethodEnum.Nazev;
                txtSearch.Show();
                txtSearch.Focus();
            }
            else
            {
                _filtrActual = string.Empty;
                txtSearch.Hide();
                dataGrid1.Focus();
            }

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
        private void SortChange(SortOrder sortOrder)
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            if (sortOrder == SortOrder.Stredisko)
                _sOrderStr = _sOrderStr == _sortStrediskoAsc ? _sortStrediskoDsc : _sortStrediskoAsc;
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
            SortChange(SortOrder.Stredisko);
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            SortChange(SortOrder.Nazev);
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            _stredisko = null;
            this.PerformOK(false);
        }

        private void ZmenaStredisko_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ZmenaStredisko_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}