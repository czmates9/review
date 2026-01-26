using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.Graphic;
using Fask.ScannerProvider;

namespace Fask.MST_W.Inventura2
{
    public partial class Nasnimane : System.Windows.Forms.Form
    {
        //System.Data.SqlServerCe.SqlCeDataReader _sce_resultset = null;
        System.Data.SQLite.SQLiteDataReader _sce_resultset = null;
        //System.Data.SqlServerCe.SqlCeCommand _sce_command = null;
        System.Data.SQLite.SQLiteCommand _sce_command = null;

        //defaultni select a parametry        
        private const string _select_All = "select * from inventur ";
        private const string _select_All_count = "select count(*) from inventur ";
        //private string _select_current = string.Empty;
        private string _select_current = _select_All;
        private string _select_current_count = _select_All_count;
        private string _select_current_wherecondition = string.Empty;
        private string _select_current_orderbycondition = string.Empty;

        //Slouzi pro odlozeny update selectu pri nacitani konfigurace
        private bool select_current_update = true;
        private void Select_Current_Update()
        {
            Select_Current_Update(this._select_current_wherecondition);
        }
        private void Select_Current_Update(string select_where)
        {
            if (!select_current_update)
                return;

            this._select_current_wherecondition = select_where;

            //string select = _select_All;
            StringBuilder select = new StringBuilder();
            select.Append(" order by ");
            if (RazeniHlavni != RazeniType.None)
            {
                switch (RazeniHlavni)
                {
                    case RazeniType.None:
                        break;
                    case RazeniType.Kategorie:
                        select.Append("KATEGORIE ");
                        break;
                    case RazeniType.CisloPol:
                        select.Append("I_CISLO ");
                        break;
                    case RazeniType.Nazev:
                        select.Append("NAZEV ");
                        break;
                    case RazeniType.Lokace:
                        select.Append("KLIC_LOK ");
                        break;
                    case RazeniType.Kancelar:
                        select.Append("KANCELAR ");
                        break;
                    case RazeniType.Osoba:
                        select.Append("OSOBA ");
                        break;
                    case RazeniType.Stredisko:
                        select.Append("STRED ");
                        break;
                    case RazeniType.EAN:
                        select.Append("EAN ");
                        break;
                    case RazeniType.Uzivatel:
                        select.Append("OS_ZPR ");
                        break;
                    default:
                        break;
                }
                switch (SetrizeniHlavni)
                {
                    case SetrizeniType.DESC:
                        select.Append(" DESC");
                        break;
                    case SetrizeniType.ASC:
                    default:
                        select.Append(" ASC");
                        break;
                }

                select.Append(", ");
            }

            if (RazeniVedlejsi != RazeniType.None)
            {
                switch (RazeniVedlejsi)
                {
                    case RazeniType.None:
                        break;
                    case RazeniType.Kategorie:
                        select.Append("KATEGORIE ");
                        break;
                    case RazeniType.CisloPol:
                        select.Append("I_CISLO ");
                        break;
                    case RazeniType.Nazev:
                        select.Append("NAZEV ");
                        break;
                    case RazeniType.Lokace:
                        select.Append("KLIC_LOK ");
                        break;
                    case RazeniType.Kancelar:
                        select.Append("KANCELAR ");
                        break;
                    case RazeniType.Osoba:
                        select.Append("OSOBA ");
                        break;
                    case RazeniType.Stredisko:
                        select.Append("STRED ");
                        break;
                    case RazeniType.EAN:
                        select.Append("EAN ");
                        break;
                    case RazeniType.Uzivatel:
                        select.Append("OS_ZPR ");
                        break;
                    default:
                        break;
                }
                switch (SetrizeniVedlejsi)
                {
                    case SetrizeniType.DESC:
                        select.Append(" DESC");
                        break;
                    case SetrizeniType.ASC:
                    default:
                        select.Append(" ASC");
                        break;
                }

                select.Append(", ");
            }

            select.Append(" CAS_ZPR");
            //Doplneno razeni dle casu podle nastaveni hlavniho razeni
            switch (SetrizeniHlavni)
            {
                case SetrizeniType.DESC:
                    select.Append(" DESC");
                    break;
                case SetrizeniType.ASC:
                default:
                    select.Append(" ASC");
                    break;
            }

            this._select_current_orderbycondition = select.ToString();

            SQLGetData();
        }
        
        //pocet polozek k zobrazeni na jedne strance
        private const int _db_items_perview = 10;
        //pocet polozek aktualniho pohledu v databazi
        private int _db_items_count = 0;
        //indexy do databaze
        private int _db_idx_first = 0;
        private int _db_idx_last = 0;
        private int _db_idx_current = 0;

        //Aktualne nactene polozky z databaze
        private ListPolozkyNasnimaneDS _listPolozkyDS = new ListPolozkyNasnimaneDS();

        private DataGridTableStyle dgstyle = null;
        private DataGrid2TextBoxColumn dg_I_CISLO = null;
        private DataGrid2TextBoxColumn dg_NAZEV = null;
        private DataGrid2TextBoxColumn dg_ID = null;
        private DataGrid2TextBoxColumn dg_KATEGORIE = null;
        private DataGrid2TextBoxColumn dg_STRED = null;
        private DataGrid2TextBoxColumn dg_STRED_NAZEV = null;
        private DataGrid2TextBoxColumn dg_OSOBA = null;
        private DataGrid2TextBoxColumn dg_OSOBA_NAZEV = null;
        private DataGrid2TextBoxColumn dg_LOKACE1 = null;
        private DataGrid2TextBoxColumn dg_LOKACE2 = null;
        private DataGrid2TextBoxColumn dg_LOKACE_NAZEV = null;
        private DataGrid2TextBoxColumn dg_KANCELAR = null;
        private DataGrid2TextBoxColumn dg_KANCELAR_NAZEV = null;
        private DataGrid2TextBoxColumn dg_EAN = null;
        private DataGrid2TextBoxColumn dg_KUSU = null;
        private DataGrid2TextBoxColumn dg_KLIC_LOK = null;
        private DataGrid2TextBoxColumn dg_OSZPR = null;
        private DataGrid2TextBoxColumn dg_CASZPR = null;
        private DataGrid2TextBoxColumn dg_ID_MAJETEK = null;

        private void GridStylesCreate()
        {
            try
            {
                List<DataGridColumnStyle> dglist = new List<DataGridColumnStyle>();

                dgstyle = new DataGridTableStyle();
                dgstyle.MappingName = _listPolozkyDS.Polozky.TableName;

                dg_I_CISLO = new DataGrid2TextBoxColumn();
                dg_I_CISLO.MappingName = _listPolozkyDS.Polozky.I_CISLOColumn.ColumnName;
                dg_I_CISLO.HeaderText = "Číslo majetku inventární";
                dg_I_CISLO.NullText = "-";
                dg_I_CISLO.Width = Settings.Inventura2NI_CISLOWidth;
                //dgstyle.GridColumnStyles.Add(dgITEMDESC);
                dglist.Add(dg_I_CISLO);

                dg_NAZEV = new DataGrid2TextBoxColumn();
                dg_NAZEV.MappingName = _listPolozkyDS.Polozky.NAZEVColumn.ColumnName;
                dg_NAZEV.HeaderText = "Název majetku";
                dg_NAZEV.NullText = "-";
                dg_NAZEV.Width = Settings.Inventura2NNAZEVWidth;
                dglist.Add(dg_NAZEV);

                dg_ID = new DataGrid2TextBoxColumn();
                dg_ID.MappingName = _listPolozkyDS.Polozky.IDColumn.ColumnName;
                dg_ID.HeaderText = "ID";
                dg_ID.NullText = "-";
                dg_ID.Width = Settings.Inventura2NIDWidth;
                dglist.Add(dg_ID);

                dg_KATEGORIE = new DataGrid2TextBoxColumn();
                dg_KATEGORIE.MappingName = _listPolozkyDS.Polozky.KATEGORIEColumn.ColumnName;
                dg_KATEGORIE.HeaderText = "";
                dg_KATEGORIE.NullText = "-";
                dg_KATEGORIE.Width = Settings.Inventura2NKategorieWidth;
                dglist.Add(dg_KATEGORIE);

                dg_STRED = new DataGrid2TextBoxColumn();
                dg_STRED.MappingName = _listPolozkyDS.Polozky.STREDColumn.ColumnName;
                dg_STRED.HeaderText = "Středisko ID";
                dg_STRED.NullText = "-";
                dg_STRED.Width = Settings.Inventura2NStredWidth;
                dglist.Add(dg_STRED);

                dg_STRED_NAZEV = new DataGrid2TextBoxColumn();
                dg_STRED_NAZEV.MappingName = _listPolozkyDS.Polozky.STRED_NAZEVColumn.ColumnName;
                dg_STRED_NAZEV.HeaderText = "Středisko";
                dg_STRED_NAZEV.NullText = "-";
                dg_STRED_NAZEV.Width = Settings.Inventura2NStredNazevWidth;
                dglist.Add(dg_STRED_NAZEV);

                dg_OSOBA = new DataGrid2TextBoxColumn();
                dg_OSOBA.MappingName = _listPolozkyDS.Polozky.OSOBAColumn.ColumnName;
                dg_OSOBA.HeaderText = "Osoba ID";
                dg_OSOBA.NullText = "-";
                dg_OSOBA.Width = Settings.Inventura2NOsobaWidth;
                dglist.Add(dg_OSOBA);

                dg_OSOBA_NAZEV = new DataGrid2TextBoxColumn();
                dg_OSOBA_NAZEV.MappingName = _listPolozkyDS.Polozky.OSOBA_NAZEVColumn.ColumnName;
                dg_OSOBA_NAZEV.HeaderText = "Osoba";
                dg_OSOBA_NAZEV.NullText = "-";
                dg_OSOBA_NAZEV.Width = Settings.Inventura2NOsobaNazevWidth;
                dglist.Add(dg_OSOBA_NAZEV);

                dg_LOKACE1 = new DataGrid2TextBoxColumn();
                dg_LOKACE1.MappingName = _listPolozkyDS.Polozky.LOKACE1Column.ColumnName;
                dg_LOKACE1.HeaderText = "Lokace1";
                dg_LOKACE1.NullText = "-";
                dg_LOKACE1.Width = Settings.Inventura2NLokace1Width;
                dglist.Add(dg_LOKACE1);

                dg_LOKACE2 = new DataGrid2TextBoxColumn();
                dg_LOKACE2.MappingName = _listPolozkyDS.Polozky.LOKACE2Column.ColumnName;
                dg_LOKACE2.HeaderText = "Lokace2";
                dg_LOKACE2.NullText = "-";
                dg_LOKACE2.Width = Settings.Inventura2NLokace2Width;
                dglist.Add(dg_LOKACE2);

                dg_LOKACE_NAZEV = new DataGrid2TextBoxColumn();
                dg_LOKACE_NAZEV.MappingName = _listPolozkyDS.Polozky.LOKACE_NAZEVColumn.ColumnName;
                dg_LOKACE_NAZEV.HeaderText = "Lokace";
                dg_LOKACE_NAZEV.NullText = "-";
                dg_LOKACE_NAZEV.Width = Settings.Inventura2NLokaceNazevWidth;
                dglist.Add(dg_LOKACE_NAZEV);

                dg_KANCELAR = new DataGrid2TextBoxColumn();
                dg_KANCELAR.MappingName = _listPolozkyDS.Polozky.KANCELARColumn.ColumnName;
                dg_KANCELAR.HeaderText = "Kancelář ID";
                dg_KANCELAR.NullText = "-";
                dg_KANCELAR.Width = Settings.Inventura2NKancelarWidth;
                dglist.Add(dg_KANCELAR);

                dg_KANCELAR_NAZEV = new DataGrid2TextBoxColumn();
                dg_KANCELAR_NAZEV.MappingName = _listPolozkyDS.Polozky.KANCELAR_NAZEVColumn.ColumnName;
                dg_KANCELAR_NAZEV.HeaderText = "Kancelář";
                dg_KANCELAR_NAZEV.NullText = "-";
                dg_KANCELAR_NAZEV.Width = Settings.Inventura2NKancelarNazevWidth;
                dglist.Add(dg_KANCELAR_NAZEV);

                dg_EAN = new DataGrid2TextBoxColumn();
                dg_EAN.MappingName = _listPolozkyDS.Polozky.EANColumn.ColumnName;
                dg_EAN.HeaderText = "EAN";
                dg_EAN.NullText = "-";
                dg_EAN.Width = Settings.Inventura2NEanWidth;
                dglist.Add(dg_EAN);

                dg_KUSU = new DataGrid2TextBoxColumn();
                dg_KUSU.MappingName = _listPolozkyDS.Polozky.KUSUColumn.ColumnName;
                dg_KUSU.HeaderText = "Kusů";
                dg_KUSU.NullText = "-";
                dg_KUSU.Format = "0.###";
                dg_KUSU.Width = Settings.Inventura2NKusuWidth;
                dglist.Add(dg_KUSU);

                dg_KLIC_LOK = new DataGrid2TextBoxColumn();
                dg_KLIC_LOK.MappingName = _listPolozkyDS.Polozky.KLIC_LOKColumn.ColumnName;
                dg_KLIC_LOK.HeaderText = "Lokace ID";
                dg_KLIC_LOK.NullText = "-";
                dg_KLIC_LOK.Width = Settings.Inventura2NKlicLokWidth;
                dglist.Add(dg_KLIC_LOK);

                dg_OSZPR = new DataGrid2TextBoxColumn();
                dg_OSZPR.MappingName = _listPolozkyDS.Polozky.OS_ZPRColumn.ColumnName;
                dg_OSZPR.HeaderText = "Uživatel";
                dg_OSZPR.NullText = "-";
                dg_OSZPR.Width = Settings.Inventura2NOsZprWidth;
                dglist.Add(dg_OSZPR);

                dg_CASZPR = new DataGrid2TextBoxColumn();
                dg_CASZPR.MappingName = _listPolozkyDS.Polozky.CAS_ZPRColumn.ColumnName;
                dg_CASZPR.HeaderText = "Čas";
                dg_CASZPR.NullText = "-";
                dg_CASZPR.Format = "HH:mm:ss";
                dg_CASZPR.Width = Settings.Inventura2NCasZprWidth;
                dglist.Add(dg_CASZPR);

                dg_ID_MAJETEK = new DataGrid2TextBoxColumn();
                dg_ID_MAJETEK.MappingName = _listPolozkyDS.Polozky.ID_MAJETEKColumn.ColumnName;
                dg_ID_MAJETEK.HeaderText = "ID Majetek";
                dg_ID_MAJETEK.NullText = "-";
                dg_ID_MAJETEK.Width = Settings.Inventura2NID_MAJETEKWidth;
                dglist.Add(dg_ID_MAJETEK);

                List<string> poradilist = new List<string>();
                poradilist.AddRange(Settings.PoradiSloupcuInventura2NListPolozky.Split(';'));

                dglist.Sort(new Classes.DataGridColumnStyleComparer(poradilist));

                foreach (DataGrid2TextBoxColumn dg_col in dglist)
                {
                    dgstyle.GridColumnStyles.Add(dg_col);
                }

                dataGridList.TableStyles.Add(dgstyle);

                dataGridList.RowHeightDefault = Settings.Inventura2NRowHeigth;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }


        //Pokud se smaze nejaky zaznam, tak se zde nastavuje info o smazani
        private bool _deleted = false;
        /// <summary>
        /// Nastalo pri otervrenem dialogu smazani nejakeho zaznamu
        /// </summary>
        public bool Deleted
        {
            get {return _deleted; }
            private set { _deleted = value; }
        }


        /// <summary>
        /// Aktivni(vybrany) zaznam v datagridu
        /// </summary>
        private ListPolozkyNasnimaneDS.PolozkyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)dataGridList.BindingContext[_listPolozkyDS.Polozky].Current).Row as ListPolozkyNasnimaneDS.PolozkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Nasnimane()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            GridStylesCreate();

            MyInitializeGrid();
                
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeGrid()
        {
            this.dataGridList.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGridList.Font = new Font(this.dataGridList.Font.Name, Settings.UIGridFont, this.dataGridList.Font.Style);
            this.dataGridList.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }


        private void Nasnimane_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.Text += " " + MST_Global.Inventura2Name.Trim();

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            SettingsLoad(); //musi probehnout pred Select_Current_Update()

            Select_Current_Update(string.Empty);
            //SQLGetData(); <= vola se v ramci predchozi metody(Select_Current_Update();)

            panelList.Dock = DockStyle.Fill;
            panelDetail.Dock = DockStyle.Fill;

            Zobrazeni = ZobrazeniType.List;

            this.dataGridList.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGridList.KeyScrollUp = MST_Global.DataGridScrollUp;

            dataGridList.Focus();

            ScannerStart();

            Cursor.Current = Cursors.Default;
        }

        private void SQLGetData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                StringBuilder select_final = new StringBuilder();
                StringBuilder select_final_count = new StringBuilder();
                //main select
                select_final.Append(_select_All);
                select_final_count.Append(_select_All_count);
                //where condition
                select_final.Append(this._select_current_wherecondition);
                select_final_count.Append(this._select_current_wherecondition);
                //orderby condition
                select_final.Append(this._select_current_orderbycondition);
                //select_final_count.Append(this._select_current_orderbycondition);

                this._select_current = select_final.ToString();
                this._select_current_count = select_final_count.ToString();

                if (_sce_command == null)
                {
					_sce_command = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Connection.CreateCommand();
                }
                _sce_command.CommandText = this._select_current;
                _sce_command.CommandType = CommandType.Text;
                if (_sce_command.Connection.State == ConnectionState.Closed)
                    _sce_command.Connection.Open();
                //_sce_resultset = _sce_command.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Scrollable);
                _sce_resultset = _sce_command.ExecuteReader();

                //_db_items_count = ((System.Collections.ICollection)_sce_resultset.ResultSetView).Count;
                _sce_command.CommandText = this._select_current_count;
                _db_items_count = (int)_sce_command.ExecuteScalar();

                FillGrid();

                _sce_command.Connection.Close(); //Musi se uzavirat???

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                if (_sce_command != null && _sce_command.Connection != null && _sce_command.Connection.State == ConnectionState.Open)
                    _sce_command.Connection.Close(); //musi se uzavirta???

                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Vyplnuje grid na zaklade hodnoty _db_idx_first
        /// _db_idx_current musi byt nastaveno predem, pripadne se zkoriguje, pokud je mimo rozsah
        /// </summary>
        private void FillGrid()
        {
            //dataGrid.DataSource = _browsingResultSet;
            //dataGrid.CurrentRowIndex = _currentItem - 1;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _listPolozkyDS.Clear();

                //Korekce odkud nacitat
                if (_db_idx_first > _db_items_count - _db_items_perview)
                    _db_idx_first = _db_items_count - _db_items_perview;
                if (_db_idx_first < 0)
                    _db_idx_first = 0;

                int tmp = 0;
                do
                {
                    if (tmp < _db_idx_first && _sce_resultset.Read())
                        tmp++;
                    else
                        break;
                } while (true);

                //if (_sce_resultset.ReadAbsolute(_db_idx_first))
                if (_sce_resultset.Read())
                {
                    _listPolozkyDS.Polozky.BeginLoadData();

                    for (int i = _db_idx_first; i < (_db_idx_first + _db_items_perview); i++)
                    {
                        _db_idx_last = i;

                        ListPolozkyNasnimaneDS.PolozkyRow prow = _listPolozkyDS.Polozky.NewPolozkyRow();

                        prow.ID = (int)_sce_resultset["ID"];

                        if (_sce_resultset["I_CISLO"] is System.DBNull)
                            prow.SetI_CISLONull();
                        else
                            prow.I_CISLO = (string)_sce_resultset["I_CISLO"];

                        if (_sce_resultset["NAZEV"] is System.DBNull)
                            prow.SetNAZEVNull();
                        else
                            prow.NAZEV = (string)_sce_resultset["NAZEV"];

                        if (_sce_resultset["KATEGORIE"] is System.DBNull)
                            prow.SetKATEGORIENull();
                        else
                            prow.KATEGORIE = (string)_sce_resultset["KATEGORIE"];

                        if (_sce_resultset["STRED"] is System.DBNull)
                            prow.SetSTREDNull();
                        else
                            prow.STRED = (string)_sce_resultset["STRED"];

                        if (_sce_resultset["OSOBA"] is System.DBNull)
                            prow.SetOSOBANull();
                        else
                            prow.OSOBA = (int)_sce_resultset["OSOBA"];

                        if (_sce_resultset["LOKACE1"] is System.DBNull)
                            prow.SetLOKACE1Null();
                        else
                            prow.LOKACE1 = (string)_sce_resultset["LOKACE1"];

                        if (_sce_resultset["LOKACE2"] is System.DBNull)
                            prow.SetLOKACE2Null();
                        else
                            prow.LOKACE2 = (string)_sce_resultset["LOKACE2"];

                        if (_sce_resultset["KANCELAR"] is System.DBNull)
                            prow.SetKANCELARNull();
                        else
                            prow.KANCELAR = (string)_sce_resultset["KANCELAR"];

                        if (_sce_resultset["EAN"] is System.DBNull)
                            prow.SetEANNull();
                        else
                            prow.EAN = (string)_sce_resultset["EAN"];

                        if (_sce_resultset["KUSU"] is System.DBNull)
                            prow.SetKUSUNull();
                        else
                            prow.KUSU = (decimal)_sce_resultset["KUSU"];

                        if (_sce_resultset["KLIC_LOK"] is System.DBNull)
                            prow.SetKLIC_LOKNull();
                        else
                            prow.KLIC_LOK = (int)_sce_resultset["KLIC_LOK"];

                        if (_sce_resultset["OS_ZPR"] is System.DBNull)
                            prow.SetOS_ZPRNull();
                        else
                            prow.OS_ZPR = (string)_sce_resultset["OS_ZPR"];

                        if (_sce_resultset["CAS_ZPR"] is System.DBNull)
                            prow.SetCAS_ZPRNull();
                        else
                            prow.CAS_ZPR = (DateTime)_sce_resultset["CAS_ZPR"];

                        prow.ID_MAJETEK = (int)_sce_resultset["ID_MAJETEK"];

                        try
                        {
                            if (!prow.IsSTREDNull())
                            {
								Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt_stredisko = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByStredisko_Stredisko(prow.STRED);
                                if (dt_stredisko.Count > 0)
                                    prow.STRED_NAZEV = dt_stredisko[0].NAZEV.Trim();
                                else
                                    prow.SetSTRED_NAZEVNull();
                            }
                            else
                                prow.SetSTRED_NAZEVNull();
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Write(ex);
                        }


                        try
                        {
                            if (!prow.IsOSOBANull())
                            {
								Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt_osoby = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByOSOBA_ZODP_Osoby(prow.OSOBA);
                                if (dt_osoby.Count > 0)
                                    prow.OSOBA_NAZEV = dt_osoby[0].JMENO.Trim() + " " + dt_osoby[0].PRIJMENI.Trim();
                                else
                                    prow.SetOSOBA_NAZEVNull();
                            }
                            else
                                prow.SetOSOBA_NAZEVNull();
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Write(ex);
                        }

                        try
                        {
                            if (!prow.IsKANCELARNull())
                            {
								Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt_kancl = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKANCL_Kancl(prow.KANCELAR);
                                if (dt_kancl.Count > 0)
                                    prow.KANCELAR_NAZEV = dt_kancl[0].TEXT.Trim();
                                else
                                    prow.SetKANCELAR_NAZEVNull();
                            }
                            else
                                prow.SetKANCELAR_NAZEVNull();
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Write(ex);
                        }

                        try
                        {
                            if (!prow.IsKLIC_LOKNull())
                            {
								Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt_lokace = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKLIC_LOK_Lokace(prow.KLIC_LOK);
                                if (dt_lokace.Count > 0)
                                    prow.LOKACE_NAZEV = dt_lokace[0].NAZEV.Trim();
                                else
                                    prow.SetLOKACE_NAZEVNull();
                            }
                            else
                                prow.SetLOKACE_NAZEVNull();
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Write(ex);
                        }

                        _listPolozkyDS.Polozky.AddPolozkyRow(prow);

                        if (!_sce_resultset.Read())
                            break;
                    }
                    
                    _listPolozkyDS.Polozky.EndLoadData();
                }

                //Korekce Current inexu jestlize je mimo rozsah <_db_idx_first;_db_idx_last>
                if (_db_idx_current < _db_idx_first) //current je mensi nez first
                {
                    _db_idx_current = _db_idx_first;
                }
                else if (_db_idx_current > _db_idx_last) //current je vetsi nez last
                {
                    _db_idx_current = _db_idx_last;
                }

                dataGridList.DataSource = _listPolozkyDS.Polozky;
                dataGridList.Refresh();
                dataGridList.CurrentRowIndex = _db_idx_current - _db_idx_first;
                //DataGridCell dc = dataGridList.CurrentCell;
                //dataGridList.CurrentCell = new DataGridCell(dc.RowNumber, );
                //dataGridList.CurrentCell = new DataGridCell(dc.RowNumber, dc.ColumnNumber);
                UpdateForm();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateForm()
        {
            try
            {
                ListPolozkyNasnimaneDS.PolozkyRow prow = SelectedRow;

                df_NAZEV.Data = prow.IsNAZEVNull() ? "-" : prow.NAZEV.Trim();
                df_ICISLO.Data = prow.IsI_CISLONull() ? "-" : prow.I_CISLO.Trim();
                df_EAN.Data = prow.IsEANNull() ? "-" : prow.EAN.Trim();
                df_LOKACE.Data =
                    (prow.IsKLIC_LOKNull() ? "?" : prow.KLIC_LOK.ToString()) + "," +
                    (prow.IsLOKACE_NAZEVNull() ? "-" : prow.LOKACE_NAZEV.Trim());
                df_KANCL.Data =
                    (prow.IsKANCELARNull() ? "?" : prow.KANCELAR.Trim()) + "," +
                    (prow.IsKANCELAR_NAZEVNull() ? "-" : prow.KANCELAR_NAZEV.Trim());
                df_OSOBA.Data =
                    (prow.IsOSOBANull() ? "?" : prow.OSOBA.ToString()) + "," +
                    (prow.IsOSOBA_NAZEVNull() ? "-" : prow.OSOBA_NAZEV.Trim());
                df_STRED.Data =
                    (prow.IsSTREDNull() ? "?" : prow.STRED.Trim()) + "," +
                    (prow.IsSTRED_NAZEVNull() ? "-" : prow.STRED_NAZEV.Trim());
                df_KUSU.Data = prow.IsKUSUNull() ? "-" : prow.KUSU.ToString(Settings.UIFormatDesCisel);
                df_CASZPR.Data = prow.CAS_ZPR.ToString("HH:mm:ss");
				df_Davka.Data = Inventura2.Inventura2_Instance.globalObject.Davka.ToString();
                df_KATEGORIE.Data = prow.IsKATEGORIENull() ? "-" : prow.KATEGORIE.Trim();
                df_OSZPR.Data = prow.IsOS_ZPRNull() ? "-" : prow.OS_ZPR.Trim();
            }
            catch
            {
                df_NAZEV.Data = "?";
                df_ICISLO.Data = "?";
                df_EAN.Data = "?";
                df_LOKACE.Data = "?";
                df_KANCL.Data = "?";
                df_OSOBA.Data = "?";
                df_STRED.Data = "?";
                df_KUSU.Data = "?";
                df_CASZPR.Data = "?";
                df_Davka.Data = "?";
                df_KATEGORIE.Data = "?";
                df_OSZPR.Data = "?";
            }
            finally
            {
            }

            int aktualitemporadi = (_db_items_count > 0 ? _db_idx_current + 1 : 0);
            statusBarInfo.Text = "Z:" + aktualitemporadi + " z " + _db_items_count;
        }


        #region Scanner
        delegate void ScannerEventHandlerCall(ScannerEventArgs e);
        private void OnScannerEvent(ScannerEventArgs e)
        {
            try
            {
                string ck = e.BarcodeData.Trim();
                if (ck.Length > 0)
                {
                    NajdiPolozkuCarovyKod(ck);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Inventura2)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
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
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new ScannerEventHandlerCall(OnScannerEvent), new object[] { e });
        }
        #endregion


        private void SettingsLoad()
        {
            //1. nacteni razeni a trideni
            select_current_update = false; //Vypne aktualizaci

            RazeniHlavni = Settings.Inventura2NRazeniH;
            RazeniVedlejsi = Settings.Inventura2NRazeniV;
            SetrizeniHlavni = Settings.Inventura2NSetriditH;
            SetrizeniVedlejsi = Settings.Inventura2NSetriditV;

            select_current_update = true; //zapne aktualizaci

        }

        private void SettingsSave()
        {
            this.dataGridList.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            /*
            //Ulozeni rozvrhu razeni
            Settings.Inventura2NRazeniH = RazeniHlavni;
            Settings.Inventura2NRazeniV = RazeniVedlejsi;
            Settings.Inventura2NSetriditH = SetrizeniHlavni;
            Settings.Inventura2NSetriditV = SetrizeniVedlejsi;

            //Ulozeni nastaveni zobrazeni sloupcu v datagridu
            string poradi = string.Empty;
            if (dataGridList.TableStyles.Count > 0)
            {
                for (int i = 0; i < dataGridList.TableStyles[0].GridColumnStyles.Count; i++)
                {
                    poradi += dataGridList.TableStyles[0].GridColumnStyles[i].MappingName;
                    poradi += ";";
                }
            }
            Settings.PoradiSloupcuInventura2NListPolozky = poradi;

            Settings.Inventura2NRowHeigth = dataGridList.RowHeightDefault;

            Settings.Inventura2NI_CISLOWidth = dg_I_CISLO.Width;
            Settings.Inventura2NNAZEVWidth = dg_NAZEV.Width;
            Settings.Inventura2NIDWidth = dg_ID.Width;
            Settings.Inventura2NKategorieWidth = dg_KATEGORIE.Width;
            Settings.Inventura2NStredWidth = dg_STRED.Width;
            Settings.Inventura2NStredNazevWidth = dg_STRED_NAZEV.Width;
            Settings.Inventura2NOsobaWidth = dg_OSOBA.Width;
            Settings.Inventura2NOsobaNazevWidth = dg_OSOBA_NAZEV.Width;
            Settings.Inventura2NLokace1Width = dg_LOKACE1.Width;
            Settings.Inventura2NLokace2Width = dg_LOKACE2.Width;
            Settings.Inventura2NLokaceNazevWidth = dg_LOKACE_NAZEV.Width;
            Settings.Inventura2NKancelarWidth = dg_KANCELAR.Width;
            Settings.Inventura2NKancelarNazevWidth = dg_KANCELAR_NAZEV.Width;
            Settings.Inventura2NEanWidth = dg_EAN.Width;
            Settings.Inventura2NKusuWidth = dg_KUSU.Width;
            Settings.Inventura2NKlicLokWidth = dg_KLIC_LOK.Width;
            Settings.Inventura2NOsZprWidth = dg_OSZPR.Width;
            Settings.Inventura2NCasZprWidth = dg_CASZPR.Width;
            Settings.Inventura2NID_MAJETEKWidth = dg_ID_MAJETEK.Width;*/


        }

        private void PerformKonec(bool question)
        //private void PerformKonec()
        {
            if (question && MessageBoxBig.Show("Zpět do seznamu položek?", "Dotaz",
                MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            else
                DialogResult = DialogResult.OK;

            ScannerFinalize();

            SettingsSave();
        }

        private void DeleteSelectedItem()
        {
            try
            {
                ListPolozkyNasnimaneDS.PolozkyRow prow = this.SelectedRow;
                if (prow == null)
                {
                    MessageBoxBig.Show("Není vybrána položka", "Smazat", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                if (DialogResult.No == MessageBoxBig.Show("Opravdu smazat položku '" + prow.NAZEV.Trim() + "' [" + prow.KUSU.ToString(Settings.UIFormatDesCisel) + "]?", "Smazat", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, Color.Red))
                {
                    return;
                }

                //int raff = Globals.ta_majetek.UpdateNactenoByI_CISLO(-prow.KUSU, prow.I_CISLO);
                //int raff = Globals.ta_majetek.UpdateNactenoByZAZNAM(-prow.KUSU, prow.I_CISLO, prow.KATEGORIE);
				int raff = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.UpdateNactenoByID_Majetek(-prow.KUSU, prow.ID_MAJETEK);
				int deleted = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Delete_Inventur(prow.ID);                
                if (deleted > 0)
                    Deleted = true;

                SQLGetData();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        #region Zobrazeni
        private enum ZobrazeniType
        {
            Detail,
            List
        }
        private ZobrazeniType _zobrazeni = ZobrazeniType.List;
        private ZobrazeniType Zobrazeni
        {
            get { return _zobrazeni; }
            set
            {
                _zobrazeni = value;
                switch (_zobrazeni)
                {
                    case ZobrazeniType.Detail:
                        panelDetail.Show();
                        panelList.Hide();
                        break;
                    case ZobrazeniType.List:
                        panelDetail.Hide();
                        panelList.Show();
                        break;
                    default:
                        panelDetail.Hide();
                        panelList.Show();
                        break;
                }
            }
        }
        private void ZobrazeniSwitchRezim()
        {
            switch (_zobrazeni)
            {
                case ZobrazeniType.Detail:
                    Zobrazeni = ZobrazeniType.List;
                    break;
                case ZobrazeniType.List:
                    Zobrazeni = ZobrazeniType.Detail;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Razeni
        public enum RazeniType
        {
            None,
            Kategorie,
            CisloPol,
            Nazev,
            Stredisko,
            Lokace,
            Kancelar,
            Osoba,
            EAN,
            Uzivatel
        }
        public enum SetrizeniType
        {
            ASC,
            DESC
        }
        private RazeniType razeniHlavni = RazeniType.None;
        private RazeniType razeniVedlejsi = RazeniType.None;
        private SetrizeniType setrizeniHlavni = SetrizeniType.ASC;
        private SetrizeniType setrizeniVedlejsi = SetrizeniType.ASC;

        private RazeniType RazeniHlavni
        {
            get { return razeniHlavni; }
            set
            {
                if (razeniHlavni == value)
                    return;

                razeniHlavni = value;
                menuItemRaditH_CisloPol.Checked =
                    menuItemRaditH_Lokace.Checked =
                    menuItemRaditH_Nazev.Checked =
                    menuItemRaditH_EAN.Checked = 
                    menuItemRaditH_Kancelar.Checked =
                    menuItemRaditH_Osoba.Checked = 
                    menuItemRaditH_Stredisko.Checked =
                    menuItemRaditH_Uzivatel.Checked = false;
                menuItemRaditV_CisloPol.Enabled =
                    menuItemRaditV_Lokace.Enabled =
                    menuItemRaditV_Nazev.Enabled =
                    menuItemRaditV_EAN.Enabled =
                    menuItemRaditV_Kancelar.Enabled =
                    menuItemRaditV_Osoba.Enabled =
                    menuItemRaditV_Stredisko.Enabled =
                    menuItemRaditV_Uzivatel.Enabled = true;
                switch (razeniHlavni)
                {
                    case RazeniType.CisloPol:
                        menuItemRaditH_CisloPol.Checked = true;
                        menuItemRaditV_CisloPol.Enabled = false;
                        break;
                    case RazeniType.Nazev:
                        menuItemRaditH_Nazev.Checked = true;
                        menuItemRaditV_Nazev.Enabled = false;
                        break;
                    case RazeniType.Lokace:
                        menuItemRaditH_Lokace.Checked = true;
                        menuItemRaditV_Lokace.Enabled = false;
                        break;
                    case RazeniType.EAN:
                        menuItemRaditH_EAN.Checked = true;
                        menuItemRaditV_EAN.Enabled = false;
                        break;
                    case RazeniType.Kancelar:
                        menuItemRaditH_Kancelar.Checked = true;
                        menuItemRaditV_Kancelar.Enabled = false;
                        break;
                    case RazeniType.Osoba:
                        menuItemRaditH_Osoba.Checked = true;
                        menuItemRaditV_Osoba.Enabled = false;
                        break;
                    case RazeniType.Stredisko:
                        menuItemRaditH_Stredisko.Checked = true;
                        menuItemRaditV_Stredisko.Enabled = false;
                        break;
                    case RazeniType.Uzivatel:
                        menuItemRaditH_Uzivatel.Checked = true;
                        menuItemRaditV_Uzivatel.Enabled = false;
                        break;
                    case RazeniType.None:
                    default:
                        break;
                }

                Select_Current_Update();
            }
        }
        private RazeniType RazeniVedlejsi
        {
            get { return razeniVedlejsi; }
            set
            {
                if (razeniVedlejsi == value)
                    return;

                razeniVedlejsi = value;
                menuItemRaditV_CisloPol.Checked =
                    menuItemRaditV_Lokace.Checked =
                    menuItemRaditV_Nazev.Checked =
                    menuItemRaditV_EAN.Checked =
                    menuItemRaditV_Kancelar.Checked =
                    menuItemRaditV_Osoba.Checked =
                    menuItemRaditV_Stredisko.Checked =
                    menuItemRaditV_Uzivatel.Checked = false;
                menuItemRaditH_CisloPol.Enabled =
                    menuItemRaditH_Lokace.Enabled =
                    menuItemRaditH_Nazev.Enabled =
                    menuItemRaditH_EAN.Enabled =
                    menuItemRaditH_Kancelar.Enabled =
                    menuItemRaditH_Osoba.Enabled =
                    menuItemRaditH_Stredisko.Enabled =
                    menuItemRaditH_Uzivatel.Enabled = true;
                switch (razeniVedlejsi)
                {
                    case RazeniType.CisloPol:
                        menuItemRaditV_CisloPol.Checked = true;
                        menuItemRaditH_CisloPol.Enabled = false;
                        break;
                    case RazeniType.Nazev:
                        menuItemRaditV_Nazev.Checked = true;
                        menuItemRaditH_Nazev.Enabled = false;
                        break;
                    case RazeniType.Lokace:
                        menuItemRaditV_Lokace.Checked = true;
                        menuItemRaditH_Lokace.Enabled = false;
                        break;
                    case RazeniType.EAN:
                        menuItemRaditV_EAN.Checked = true;
                        menuItemRaditH_EAN.Enabled = false;
                        break;
                    case RazeniType.Kancelar:
                        menuItemRaditV_Kancelar.Checked = true;
                        menuItemRaditH_Kancelar.Enabled = false;
                        break;
                    case RazeniType.Osoba:
                        menuItemRaditV_Osoba.Checked = true;
                        menuItemRaditH_Osoba.Enabled = false;
                        break;
                    case RazeniType.Stredisko:
                        menuItemRaditV_Stredisko.Checked = true;
                        menuItemRaditH_Stredisko.Enabled = false;
                        break;
                    case RazeniType.Uzivatel:
                        menuItemRaditV_Uzivatel.Checked = true;
                        menuItemRaditH_Uzivatel.Enabled = false;
                        break;
                    case RazeniType.None:
                    default:
                        break;
                }

                Select_Current_Update();
            }
        }

        private SetrizeniType SetrizeniHlavni
        {
            get { return setrizeniHlavni; }
            set
            {
                if (setrizeniHlavni == value)
                    return;
                setrizeniHlavni = value;
                switch (setrizeniHlavni)
                {
                    case SetrizeniType.DESC:
                        menuItemRaditH_OdKonce.Checked = true;
                        break;
                    case SetrizeniType.ASC:
                    default:
                        menuItemRaditH_OdKonce.Checked = false;
                        break;
                }
                
                Select_Current_Update();
            }
        }

        private SetrizeniType SetrizeniVedlejsi
        {
            get { return setrizeniVedlejsi; }
            set
            {
                if (setrizeniVedlejsi == value)
                    return;
                setrizeniVedlejsi = value;
                switch (setrizeniVedlejsi)
                {
                    case SetrizeniType.DESC:
                        menuItemRaditV_OdKonce.Checked = true;
                        break;
                    case SetrizeniType.ASC:
                    default:
                        menuItemRaditV_OdKonce.Checked = false;
                        break;
                }
                
                Select_Current_Update();
            }
        }

        #endregion

        #region Posuvy
        private void MoveFirst()
        {
            _db_idx_current = _db_idx_first = 0;
            SQLGetData();
        }

        private void MoveLast()
        {
            _db_idx_current = _db_idx_first = _db_items_count - 1;
            SQLGetData();
        }

        private void MovePrev()
        {
            _db_idx_current = _db_idx_first - 1;
            _db_idx_first -= _db_items_perview;
            SQLGetData();
        }

        private void MoveNext()
        {
            _db_idx_current = _db_idx_last + 1;
            _db_idx_first += _db_items_perview;
            SQLGetData();
        }
        #endregion

        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformKonec(true);
        }

        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }

        private void menuItemListDetail_Click(object sender, EventArgs e)
        {
            ZobrazeniSwitchRezim();
        }

        private void Nasnimane_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == MST_Global.DataGridScrollDown)
            {
                if (dataGridList.CurrentRowIndex == _listPolozkyDS.Polozky.Rows.Count - 1 && (_db_idx_last + 1) < _db_items_count)
                { //jsem na posledni polozce
                    this.MoveNext();
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == MST_Global.DataGridScrollUp)
            {
                if (dataGridList.CurrentRowIndex == 0 && _db_idx_first > 0)
                {
                    this.MovePrev();
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec(true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //VyplnPolozku(this.SelectedRow);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                //SmazPolozku(this.SelectedRow);
                DeleteSelectedItem();
            }
            else if (e.KeyCode == Keys.D1)
            {
                ZobrazitVse();
            }
            else if (e.KeyCode == Keys.D2)
            {
                ZobrazeniSwitchRezim();
            }
            else if (e.KeyCode == Keys.D3)
            {
                //menuItemNasnimane_Click(null, null);
            }
            else if (e.KeyCode == Keys.F1)
            {
                vyhledejPolozkuNazev();
            }
            else if (e.KeyCode == Keys.F2)
            {
                vyhledejPolozkuCarovyKod();
            }
            else if (e.KeyCode == Keys.F3)
            {
                NajdiPolozkuPozice();
            }
            else
                return;

            e.Handled = true;
        }

        private void dataGridList_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            _db_idx_current = _db_idx_first + dataGridList.CurrentRowIndex;
            this.UpdateForm();
        }

        private void dataGridList_CurrentCellChanged(object sender, EventArgs e)
        {
            _db_idx_current = _db_idx_first + dataGridList.CurrentRowIndex;
            this.UpdateForm();
        }

        private void menuItemRaditH_Kategorie_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Kategorie)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Kategorie;
        }

        private void menuItemRaditH_CisloPol_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.CisloPol)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.CisloPol;
        }

        private void menuItemRaditH_Nazev_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Nazev)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Nazev;
        }

        private void menuItemRaditH_Lokace_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Lokace)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Lokace;
        }

        private void menuItemRaditH_Uzivatel_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Uzivatel)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Uzivatel;
        }

        private void menuItemRaditH_OdKonce_Click(object sender, EventArgs e)
        {
            if (SetrizeniHlavni == SetrizeniType.ASC)
                SetrizeniHlavni = SetrizeniType.DESC;
            else
                SetrizeniHlavni = SetrizeniType.ASC;
        }

        private void menuItemRaditV_Kategorie_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Kategorie)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Kategorie;
        }

        private void menuItemRaditV_CisloPol_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.CisloPol)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.CisloPol;
        }

        private void menuItemRaditV_Nazev_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Nazev)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Nazev;
        }

        private void menuItemRaditV_Lokace_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Lokace)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Lokace;
        }

        private void menuItemRaditV_Uzivatel_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Uzivatel)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Uzivatel;
        }

        private void menuItemRaditV_OdKonce_Click(object sender, EventArgs e)
        {
            if (SetrizeniVedlejsi == SetrizeniType.ASC)
                SetrizeniVedlejsi = SetrizeniType.DESC;
            else
                SetrizeniVedlejsi = SetrizeniType.ASC;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonFirst)
                MoveFirst();
            else if (e.Button == toolBarButtonLast)
                MoveLast();
            else if (e.Button == toolBarButtonPrev)
                MovePrev();
            else if (e.Button == toolBarButtonNext)
                MoveNext();
        }

        private void menuItemRaditH_Kancelar_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Kancelar)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Kancelar;
        }

        private void menuItemRaditH_Osoba_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Osoba)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Osoba;
        }

        private void menuItemRaditH_Stredisko_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Stredisko)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Stredisko;
        }

        private void menuItemRaditH_EAN_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.EAN)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.EAN;
        }

        private void menuItemRaditV_Kancelar_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Kancelar)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Kancelar;
        }

        private void menuItemRaditV_Osoba_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Osoba)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Osoba;
        }

        private void menuItemRaditV_Stredisko_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Stredisko)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Stredisko;
        }

        private void menuItemRaditV_EAN_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.EAN)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.EAN;
        }

        private void menuItemZobrazeniVse_Click(object sender, EventArgs e)
        {
            ZobrazitVse();
        }

        private void ZobrazitVse()
        {
            int tmpcurrentitem = _db_idx_current;
            _db_idx_current = _db_idx_first;
            Select_Current_Update(string.Empty); //bez podminky je vse ...
            _db_idx_current = tmpcurrentitem;
            dataGridList.CurrentRowIndex = _db_idx_current - 1 - _db_idx_first;
        }

        private void menuItemHledatNazev_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuNazev();
        }

        private void vyhledejPolozkuNazev()
        {
            try
            {
                ScannerStop();

                string nazev = string.Empty;
                using (Forms.SejmiKodForm skf = new SejmiKodForm("Název", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = "Hledat";
                    skf.Owner = this;
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    nazev = skf.Kod;
                }

                NajdiPolozkuNazev(nazev);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Logging.Log.Write(ex.Message, this.GetType().ToString());
            }
            finally
            {
                ScannerStart();
            }
        }

        /// <summary>
        /// Najde polozku dle nazvu a oznaci ji jako aktivni v datagridu
        /// </summary>
        /// <param name="carkod">carovy kod polozky</param>
        /// <param name="lastindex">posledni nalezeny index</param>
        /// <returns>index nalezene polozky, vetsi nez posledni nalezeny index</returns>
        private bool NajdiPolozkuNazev(string nazev)
        {
            bool founded = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _sce_command.CommandText = "select nazev from inventur where nazev like '" + nazev + "%'";

                bool prevStateConnectionOpened = _sce_command.Connection.State == ConnectionState.Open;
                if (_sce_command.Connection.State == ConnectionState.Closed)
                {
                    _sce_command.Connection.Open();
                }
                
                var polozkyReader = _sce_command.ExecuteReader(CommandBehavior.SingleRow);
                founded = polozkyReader.Read();
                if (!prevStateConnectionOpened)
                {
                    _sce_command.Connection.Close();
                }

                //Polozka nenalezena
                if (!founded)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show("Položka s názvem '" + nazev + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }

                //polozka nalezena, tak zobrazit stav
                _db_idx_current = 0;
                _db_idx_first = 0;
                Select_Current_Update("where NAZEV like '" + nazev + "%' ");
                return founded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex.Message, this.Text);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItemHledatCK_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuCarovyKod();
        }

        private void vyhledejPolozkuCarovyKod()
        {
            try
            {
                ScannerStop();

                string ck = string.Empty;
                using (Forms.SejmiKodForm skf = new SejmiKodForm("Čárový kód", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = "Hledat";
                    skf.Owner = this;
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    ck = skf.Kod;
                }

                NajdiPolozkuCarovyKod(ck);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Logging.Log.Write(ex.Message, this.GetType().ToString());
            }
            finally
            {
                ScannerStart();
            }
        }

        /// <summary>
        /// Najde polozku dle caroveho kodu a oznaci ji jako aktivni v datagridu
        /// </summary>
        /// <param name="carkod">carovy kod polozky</param>
        /// <param name="lastindex">posledni nalezeny index</param>
        /// <returns>index nalezene polozky, vetsi nez posledni nalezeny index</returns>
        private bool NajdiPolozkuCarovyKod(string carkod)
        {
            bool founded = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                _sce_command.CommandText = "select ean from inventur where ean like '" + carkod + "'";

                bool prevStateConnectionOpened = _sce_command.Connection.State == ConnectionState.Open;
                if (_sce_command.Connection.State == ConnectionState.Closed)
                {
                    _sce_command.Connection.Open();
                }
                
                var polozkyReader = _sce_command.ExecuteReader(CommandBehavior.SingleRow);
                founded = polozkyReader.Read();
                if (!prevStateConnectionOpened)
                {
                    _sce_command.Connection.Close();
                }

                //Polozka nenalezena
                if (!founded)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show("Položka s čárovým kódem '" + carkod + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }

                //polozka nalezena, tak zobrazit stav
                _db_idx_current = 0;
                _db_idx_first = 0;
                Select_Current_Update("where EAN like '" + carkod + "' ");
                return founded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex.Message, this.Text);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void NajdiPolozkuPozice()
        {
            try
            {
                ScannerStop();

                int pozice = _db_idx_current;
                using (SejmiKodForm skf = new SejmiKodForm("Pozice záznamu", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, (pozice).ToString()))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    pozice = int.Parse(skf.Kod);
                    if (pozice < 1 || _db_items_count <= pozice)
                    {
                        MessageBoxBig.Show("Pozice je mimo rozsah (1-" + _db_items_count + ")", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                    pozice--;
                }

                _db_idx_current = pozice;
                Select_Current_Update(); //aktualni filtr ...
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemHledatPozice_Click(object sender, EventArgs e)
        {
            NajdiPolozkuPozice();
        }

        private void Nasnimane_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void Nasnimane_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void Nasnimane_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
        }

    }

}