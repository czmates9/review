using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.Collections.Generic;
using Fask.Graphic;
using Fask.ScannerProvider;

namespace Fask.MST_W.Inventura2
{
    public partial class ListPolozky : System.Windows.Forms.Form
    {
        #region Promenne
        private string davka = string.Empty; //Cislo zpracovavane davky
        int _itemsCount = -1;   //Pocet polozek ve vysledku dotazu na I1
        int _currentItem = -1;  //Aktualni poradove cislo v datagridu <0 = inicializovano
        int _firstItem = -1;    //Poradi ve vysledku dotazu prvni polozky zobrazene v datagridu
        int _pocetZobrazit = 10;//pocet polozek, ktere se budou nacitat z resultsetu

        //Hlavni command pro dohledavani dodatecnych iformaci(overeni exitence)
        //private System.Data.SqlServerCe.SqlCeCommand icomm_browsing = null;
        private System.Data.SQLite.SQLiteCommand icomm_browsing = null;
        //ResultSet pro rychle natazeni zaznamu z databaze do datagridu (defaultne 10 zaznamu)
        //private System.Data.SqlServerCe.SqlCeResultSet _browsingResultSet = null;
        private System.Data.SQLite.SQLiteDataReader _browsingResultSet = null;

        //Vytazeni vsech polozek z majteke
        private const string _select_All = "select * from majetek ";
        //private const string _select_All = "select m.*, x.NACTENO from majetek m left join " +
        //    "(Select i.I_CISLO, Sum(i.KUSU) as NACTENO " +
        //    "from inventur i " +
        //    "group by i.I_CISLO) x " +
        //    "on x.I_CISLO=m.I_CISLO ";
        private const string _select_All_Count = "select count(*) as itemscount from majetek ";
        //private const string _select_All_Count = "select count(*) as itemscount from majetek m left join " +
        //    "(Select i.I_CISLO, Sum(i.KUSU) as NACTENO " +
        //    "from inventur i " +
        //    "group by i.I_CISLO) x " +
        //    "on x.I_CISLO=m.I_CISLO ";
        private string _select_current = _select_All;
        private string _select_current_wherecondition = string.Empty;

        //private  Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter ita_hlavicky = null;

        //Dataset, ktery obsahuje natazena data z databaze (minimum)
        private Fask.SQLiteDBs.DataSets.Inventura2 _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();
        private ListPolozkyDS _listPolozkyDS = new ListPolozkyDS();

        //Inventura1Service.Inventura1Service _i1_service = null;
        #endregion

        #region Vlastnosti
        private NaplnPolozku _naplnPolozkuForm = null;
        private NaplnPolozku NaplnPolozkuForm
        {
            get
            {
                if (_naplnPolozkuForm == null)
                    _naplnPolozkuForm = new NaplnPolozku();
                if (_naplnPolozkuForm.IsDisposed)
                    _naplnPolozkuForm = new NaplnPolozku();
                return _naplnPolozkuForm;
            }
        }

        /// <summary>
        /// Aktivni(vybrany) zaznam v datagridu
        /// </summary>
        private ListPolozkyDS.PolozkyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)dataGrid.BindingContext[_listPolozkyDS.Polozky].Current).Row as ListPolozkyDS.PolozkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Nastavi connection string k souboru databaze davky
        /// </summary>
        private string Davka
        {
            set
            {
                davka = value;
            }
        }
        #endregion

        #region Konstruktor + Inicializace
        public ListPolozky()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            menuItemRFID.Enabled = MST_Global.RFIDPovolitUHF;

            Cursor.Current = Cursors.Default;
        }

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
        private DataGrid2TextBoxColumn dg_KANCELAR_POPIS = null;
        private DataGrid2TextBoxColumn dg_EAN = null;
        private DataGrid2TextBoxColumn dg_KUSU = null;
        private DataGrid2TextBoxColumn dg_KLIC_LOK = null;
        private DataGrid2TextBoxColumn dg_NACTENO = null;
        private DataGrid2TextBoxColumn dg_ZBYVA = null;

        private void GridStylesCreate()
        {
            try
            {
                List<DataGridColumnStyle> dglist = new List<DataGridColumnStyle>();

                dgstyle = new DataGridTableStyle();
                dgstyle.MappingName = _listPolozkyDS.Polozky.TableName;

                dg_I_CISLO = new DataGrid2TextBoxColumn();
                dg_I_CISLO.MappingName = _listPolozkyDS.Polozky.I_CISLOColumn.ColumnName;
                dg_I_CISLO.HeaderText = "Èíslo majetku inventární";
                dg_I_CISLO.NullText = "-";
                dg_I_CISLO.Width = Settings.Inventura2I_CISLOWidth;
                //dgstyle.GridColumnStyles.Add(dgITEMDESC);
                dglist.Add(dg_I_CISLO);

                dg_NAZEV = new DataGrid2TextBoxColumn();
                dg_NAZEV.MappingName = _listPolozkyDS.Polozky.NAZEVColumn.ColumnName;
                dg_NAZEV.HeaderText = "Název majetku";
                dg_NAZEV.NullText = "-";
                dg_NAZEV.Width = Settings.Inventura2NAZEVWidth;
                dglist.Add(dg_NAZEV);

                dg_ID = new DataGrid2TextBoxColumn();
                dg_ID.MappingName = _listPolozkyDS.Polozky.IDColumn.ColumnName;
                dg_ID.HeaderText = "ID";
                dg_ID.NullText = "-";
                dg_ID.Width = Settings.Inventura2IDWidth;
                dglist.Add(dg_ID);

                dg_KATEGORIE = new DataGrid2TextBoxColumn();
                dg_KATEGORIE.MappingName = _listPolozkyDS.Polozky.KATEGORIEColumn.ColumnName;
                dg_KATEGORIE.HeaderText = "Kategorie";
                dg_KATEGORIE.NullText = "-";
                dg_KATEGORIE.Width = Settings.Inventura2KategorieWidth;
                dglist.Add(dg_KATEGORIE);

                dg_STRED = new DataGrid2TextBoxColumn();
                dg_STRED.MappingName = _listPolozkyDS.Polozky.STREDColumn.ColumnName;
                dg_STRED.HeaderText = "Støedisko ID";
                dg_STRED.NullText = "-";
                dg_STRED.Width = Settings.Inventura2StredWidth;
                dglist.Add(dg_STRED);

                dg_STRED_NAZEV = new DataGrid2TextBoxColumn();
                dg_STRED_NAZEV.MappingName = _listPolozkyDS.Polozky.STRED_NAZEVColumn.ColumnName;
                dg_STRED_NAZEV.HeaderText = "Støedisko";
                dg_STRED_NAZEV.NullText = "-";
                dg_STRED_NAZEV.Width = Settings.Inventura2StredNazevWidth;
                dglist.Add(dg_STRED_NAZEV);

                dg_OSOBA = new DataGrid2TextBoxColumn();
                dg_OSOBA.MappingName = _listPolozkyDS.Polozky.OSOBAColumn.ColumnName;
                dg_OSOBA.HeaderText = "Osoba ID";
                dg_OSOBA.NullText = "-";
                dg_OSOBA.Width = Settings.Inventura2OsobaWidth;
                dglist.Add(dg_OSOBA);

                dg_OSOBA_NAZEV = new DataGrid2TextBoxColumn();
                dg_OSOBA_NAZEV.MappingName = _listPolozkyDS.Polozky.OSOBA_NAZEVColumn.ColumnName;
                dg_OSOBA_NAZEV.HeaderText = "Osoba";
                dg_OSOBA_NAZEV.NullText = "-";
                dg_OSOBA_NAZEV.Width = Settings.Inventura2OsobaNazevWidth;
                dglist.Add(dg_OSOBA_NAZEV);

                dg_LOKACE1 = new DataGrid2TextBoxColumn();
                dg_LOKACE1.MappingName = _listPolozkyDS.Polozky.LOKACE1Column.ColumnName;
                dg_LOKACE1.HeaderText = "Lokace1";
                dg_LOKACE1.NullText = "-";
                dg_LOKACE1.Width = Settings.Inventura2Lokace1Width;
                dglist.Add(dg_LOKACE1);

                dg_LOKACE2 = new DataGrid2TextBoxColumn();
                dg_LOKACE2.MappingName = _listPolozkyDS.Polozky.LOKACE2Column.ColumnName;
                dg_LOKACE2.HeaderText = "Lokace2";
                dg_LOKACE2.NullText = "-";
                dg_LOKACE2.Width = Settings.Inventura2Lokace2Width;
                dglist.Add(dg_LOKACE2);

                dg_LOKACE_NAZEV = new DataGrid2TextBoxColumn();
                dg_LOKACE_NAZEV.MappingName = _listPolozkyDS.Polozky.LOKACE_NAZEVColumn.ColumnName;
                dg_LOKACE_NAZEV.HeaderText = "Lokace";
                dg_LOKACE_NAZEV.NullText = "-";
                dg_LOKACE_NAZEV.Width = Settings.Inventura2LokaceNazevWidth;
                dglist.Add(dg_LOKACE_NAZEV);

                dg_KANCELAR = new DataGrid2TextBoxColumn();
                dg_KANCELAR.MappingName = _listPolozkyDS.Polozky.KANCELARColumn.ColumnName;
                dg_KANCELAR.HeaderText = "Umístìní ID";
                dg_KANCELAR.NullText = "-";
                dg_KANCELAR.Width = Settings.Inventura2KancelarWidth;
                dglist.Add(dg_KANCELAR);

                dg_KANCELAR_NAZEV = new DataGrid2TextBoxColumn();
                dg_KANCELAR_NAZEV.MappingName = _listPolozkyDS.Polozky.KANCELAR_NAZEVColumn.ColumnName;
                dg_KANCELAR_NAZEV.HeaderText = "Umístìní";
                dg_KANCELAR_NAZEV.NullText = "-";
                dg_KANCELAR_NAZEV.Width = Settings.Inventura2KancelarNazevWidth;
                dglist.Add(dg_KANCELAR_NAZEV);


                dg_KANCELAR_POPIS = new DataGrid2TextBoxColumn();
                dg_KANCELAR_POPIS.MappingName = _listPolozkyDS.Polozky.KANCELAR_POPISColumn.ColumnName;
                dg_KANCELAR_POPIS.HeaderText = "Název umístìní";
                dg_KANCELAR_POPIS.NullText = "-";
                dg_KANCELAR_POPIS.Width = Settings.Inventura2KancelarPopisWidth;
                dglist.Add(dg_KANCELAR_POPIS);

                

                dg_EAN = new DataGrid2TextBoxColumn();
                dg_EAN.MappingName = _listPolozkyDS.Polozky.EANColumn.ColumnName;
                dg_EAN.HeaderText = "EAN";
                dg_EAN.NullText = "-";
                dg_EAN.Width = Settings.Inventura2EanWidth;
                dglist.Add(dg_EAN);

                dg_KUSU = new DataGrid2TextBoxColumn();
                dg_KUSU.MappingName = _listPolozkyDS.Polozky.KUSUColumn.ColumnName;
                dg_KUSU.HeaderText = "Kusù";
                dg_KUSU.NullText = "-";
                dg_KUSU.Format = "0.###";
                dg_KUSU.Width = Settings.Inventura2KusuWidth;
                dglist.Add(dg_KUSU);

                dg_KLIC_LOK = new DataGrid2TextBoxColumn();
                dg_KLIC_LOK.MappingName = _listPolozkyDS.Polozky.KLIC_LOKColumn.ColumnName;
                dg_KLIC_LOK.HeaderText = "Lokace ID";
                dg_KLIC_LOK.NullText = "-";
                dg_KLIC_LOK.Width = Settings.Inventura2KlicLokWidth;
                dglist.Add(dg_KLIC_LOK);

                dg_NACTENO = new DataGrid2TextBoxColumn();
                dg_NACTENO.MappingName = _listPolozkyDS.Polozky.NACTENOColumn.ColumnName;
                dg_NACTENO.HeaderText = "Naèteno";
                dg_NACTENO.NullText = "-";
                dg_NACTENO.Format = "0.###";
                dg_NACTENO.Width = Settings.Inventura2NactenoWidth;
                dglist.Add(dg_NACTENO);

                dg_ZBYVA = new DataGrid2TextBoxColumn();
                dg_ZBYVA.MappingName = _listPolozkyDS.Polozky.ZBYVAColumn.ColumnName;
                dg_ZBYVA.HeaderText = "Zbývá";
                dg_ZBYVA.NullText = "-";
                dg_ZBYVA.Format = "0.###";
                dg_ZBYVA.Width = Settings.Inventura2ZbyvaWidth;
                dglist.Add(dg_ZBYVA);

                List<string> poradilist = new List<string>();
                poradilist.AddRange(Settings.PoradiSloupcuInventura2ListPolozky.Split(';'));

                dglist.Sort(new Classes.DataGridColumnStyleComparer(poradilist));

                foreach (DataGrid2TextBoxColumn dg_col in dglist)
                {
                    dgstyle.GridColumnStyles.Add(dg_col);
                }

                dataGrid.TableStyles.Add(dgstyle);

                dataGrid.RowHeightDefault = Settings.Inventura2RowHeigth;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void ListPolozky_Load(object sender, EventArgs e)
        {
            this.Text += " " + MST_Global.Inventura2Name.Trim();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            //_i1_service = new Fask.MST_W.Inventura1Service.Inventura1Service();
            //_i1_service.Url = MST_Global.ServerAddress + "Inventura1.asmx";
            //_i1_service.Timeout = MST_Global.ServiceTimeOut;
            //_i1_service.UpdateWebServiceCredentials();

            //if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Open)
            //    Globals.active_connection.Close();

            //try
            //{
            //    Globals.active_connection.Open();
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex.Message, this.GetType().ToString());
            //}

			//icomm_browsing = new System.Data.SQLite.SQLiteCommand();
			//icomm_browsing.Connection = Globals.active_connection;
			//icomm_browsing.CommandType = CommandType.Text;
            
            GridStylesCreate();

            MyInitializeGrid();

            SetActualSelectCondition();

            //icomm_browsing.CommandText = _select_All;
            CreateResultSet(_select_All, _select_current_wherecondition);
            //CreateResultSet();

			Fask.SQLiteDBs.DataSets.Inventura2.ParametryDataTable pardt = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetData_Parametry();
			Inventura2.Inventura2_Instance.globalObject.active_parametry = pardt.Rows.Count > 0 ? pardt[0] : null;

            panelGrid.Dock = DockStyle.Fill;
            panelDetail.Dock = DockStyle.Fill;
            panelDetail.AutoScroll = true;

            Zobrazeni = ZobrazeniType.List;

            this.dataGrid.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid.KeyScrollUp = MST_Global.DataGridScrollUp;


            //ita_hlavicky = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter();

            //ita_hlavicky.Connection = Globals.active_connection;
            Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYDataTable h_dt =Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetData_Hlavicky();
            if (h_dt.Count <= 0)
            {
                //int.Parse(davka)
				Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Insert_Hlavicky((int)Inventura2.Inventura2_Instance.globalObject.Davka, Guid.NewGuid());
            }


            menuItemZmenaLokace.Enabled = MST_Global.Inventura2DotazLokace;
            menuItemZmenaStrediska.Enabled = MST_Global.Inventura2DotazStredisko;
            menuItemZmenaOsoby.Enabled = MST_Global.Inventura2DotazOsoba;
            menuItemZmenaKancl.Enabled = MST_Global.Inventura2DotazKancl;

            dataGrid.Focus();


            ScannerStart();
            UpdateForm();
        }


        private void MyInitializeGrid()
        {
            this.dataGrid.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid.Font = new Font(this.dataGrid.Font.Name, Settings.UIGridFont, this.dataGrid.Font.Style);
            this.dataGrid.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
        }


        private void PerformKonec(bool question)
        {
            if (question && MessageBoxBig.Show("Opravdu chcete ukonèit práci s daty inventury?", "Dotaz",
                MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            else
                DialogResult = DialogResult.OK;

            this.ScannerFinalize();

            /*
            //Ulozeni nastaveni zobrazeni sloupcu v datagridu
            string poradi = string.Empty;
            if (dataGrid.TableStyles.Count > 0)
            {
                for (int i = 0; i < dataGrid.TableStyles[0].GridColumnStyles.Count; i++)
                {
                    poradi += dataGrid.TableStyles[0].GridColumnStyles[i].MappingName;
                    poradi += ";";
                }
            }
            Settings.PoradiSloupcuInventura2ListPolozky = poradi;

            Settings.Inventura2I_CISLOWidth = dg_I_CISLO.Width;
            Settings.Inventura2NAZEVWidth = dg_NAZEV.Width;
            Settings.Inventura2EanWidth = dg_EAN.Width;
            Settings.Inventura2IDWidth = dg_ID.Width;
            Settings.Inventura2KancelarNazevWidth = dg_KANCELAR_NAZEV.Width;
            Settings.Inventura2KancelarWidth = dg_KANCELAR.Width;
            Settings.Inventura2KategorieWidth = dg_KATEGORIE.Width;
            Settings.Inventura2KlicLokWidth = dg_KLIC_LOK.Width;
            Settings.Inventura2KusuWidth = dg_KUSU.Width;
            Settings.Inventura2Lokace1Width = dg_LOKACE1.Width;
            Settings.Inventura2Lokace2Width = dg_LOKACE2.Width;
            Settings.Inventura2LokaceNazevWidth = dg_LOKACE_NAZEV.Width;
            Settings.Inventura2NactenoWidth = dg_NACTENO.Width;
            Settings.Inventura2OsobaNazevWidth = dg_OSOBA_NAZEV.Width;
            Settings.Inventura2OsobaWidth = dg_OSOBA.Width;
            Settings.Inventura2StredNazevWidth = dg_STRED_NAZEV.Width;
            Settings.Inventura2StredWidth = dg_STRED.Width;
            Settings.Inventura2ZbyvaWidth = dg_ZBYVA.Width;
            Settings.Inventura2RowHeigth = dataGrid.RowHeightDefault;
            */

            this.dataGrid.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

            if (_naplnPolozkuForm != null && !_naplnPolozkuForm.IsDisposed)
            {
                _naplnPolozkuForm.Dispose();
                _naplnPolozkuForm = null;
            }

            if (_browsingResultSet != null)
                _browsingResultSet.Close();
			//if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Open)
			//    Globals.active_connection.Close();
        }
        #endregion

        #region Zobrazeni

        private void SetActualSelectCondition()
        {
            _select_current_wherecondition = string.Empty;

			if (Inventura2.Inventura2_Instance.globalObject.active_lokace != null)
            {
                if (_select_current_wherecondition.Trim().Length > 0) { _select_current_wherecondition += " AND "; }
				_select_current_wherecondition += "KLIC_LOK=" + Inventura2.Inventura2_Instance.globalObject.active_lokace.KLIC_LOK;
            }
			if (Inventura2.Inventura2_Instance.globalObject.active_kancelar != null)
            {
                if (_select_current_wherecondition.Trim().Length > 0) { _select_current_wherecondition += " AND "; }
				_select_current_wherecondition += "KANCELAR='" + Inventura2.Inventura2_Instance.globalObject.active_kancelar.KANCL.Trim() + "'";
            }
			if (Inventura2.Inventura2_Instance.globalObject.active_osoba != null)
            {
                if (_select_current_wherecondition.Trim().Length > 0) { _select_current_wherecondition += " AND "; }
				_select_current_wherecondition += "OSOBA=" + Inventura2.Inventura2_Instance.globalObject.active_osoba.OSOBA_ZODP;
            }
			if (Inventura2.Inventura2_Instance.globalObject.active_stredisko != null)
            {
                if (_select_current_wherecondition.Trim().Length > 0) { _select_current_wherecondition += " AND "; }
				_select_current_wherecondition += "STRED='" + Inventura2.Inventura2_Instance.globalObject.active_stredisko.STREDISKO.Trim() + "'";
            }

            if (menuItemZobrazeniZbyvajici.Checked)
            {
                if (_select_current_wherecondition.Trim().Length > 0) { _select_current_wherecondition += " AND "; }
                _select_current_wherecondition += "(NACTENO<>KUSU)";
            }
        }

        private void CreateResultSet(string selectCommandExecute, string wherecondition)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //if (_browsingResultSet != null)
                //{
                //    _browsingResultSet.Close();
                //    _browsingResultSet.Dispose();
                //    _browsingResultSet = null;
                //}

                //if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Open)
                //{
                //    Globals.active_connection.Close();
                //    Globals.active_connection.Open();
                //}
                //else if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Closed)
                //{
                //    Globals.active_connection.Open();
                //}

				//if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Closed)
				//{
				//    Globals.active_connection.Open();
				//}

                try
                {
                    _select_current = selectCommandExecute;
                    _select_current_wherecondition = wherecondition;

                    string _select_current_count = _select_All_Count +
                        (_select_current_wherecondition.Trim().Length == 0 ? "" : " where " + _select_current_wherecondition);

                    icomm_browsing.CommandText = _select_current_count;
                    icomm_browsing.CommandType = CommandType.Text;
                    _itemsCount = (int)icomm_browsing.ExecuteScalar();

                    icomm_browsing.CommandText = _select_current + 
                        (_select_current_wherecondition.Trim().Length == 0 ? "" : " where " + _select_current_wherecondition);
                    icomm_browsing.CommandType = CommandType.Text;
                    //_browsingResultSet = icomm_browsing.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Scrollable | System.Data.SqlServerCe.ResultSetOptions.Insensitive);
                    //_browsingResultSet = icomm_browsing.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Insensitive);
                    _browsingResultSet = icomm_browsing.ExecuteReader();
                    
                    //Tato metoda je prilis pomala ...
                    //_itemsCount = ((System.Collections.ICollection)_browsingResultSet.ResultSetView).Count;
                    
                    //Zde je otazka kolikate spusteni to je (1.spusteni trva dlouho ...)
                    //_itemsCount = Convert.ToInt32(ita_q.ItemsCount() ?? 0);

                    if (_currentItem <= 0)
                    {
                        _currentItem = 1;
                        _firstItem = _currentItem - 1;
                    }

                    FillGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }

                //if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Open)
                //{
                //    Globals.active_connection.Close();
                //    //Globals.active_connection.Open();
                //}
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
        }
        private void FillGrid()
        {
            //dataGrid.DataSource = _browsingResultSet;
            //dataGrid.CurrentRowIndex = _currentItem - 1;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _listPolozkyDS.Clear();
                if (_currentItem > _itemsCount)
                {
                    _currentItem -= _pocetZobrazit;
                }

                int aktualItem = _currentItem - 1;

                int tmp = 0;
                do
                {
                    if (tmp < aktualItem && _browsingResultSet.Read())
                    {
                        tmp++;
                    }
                    else
                        break;
                } while (true);

                //if (_browsingResultSet.ReadAbsolute(aktualItem))
                if (_browsingResultSet.Read())
                {
                    _listPolozkyDS.Polozky.BeginLoadData();
                    //Sklady nemusi byt k dispozici
                    //try { sta_093.Connection.Open(); }
                    //catch { }
                    for (int i = (aktualItem); i < (aktualItem + _pocetZobrazit); i++)
                    {
                        ListPolozkyDS.PolozkyRow prow = _listPolozkyDS.Polozky.NewPolozkyRow();
                        
                        if (_browsingResultSet["I_CISLO"] is System.DBNull)
                            prow.SetI_CISLONull();
                        else
                            prow.I_CISLO = (string)_browsingResultSet["I_CISLO"];

                        if (_browsingResultSet["NAZEV"] is System.DBNull)
                            prow.SetNAZEVNull();
                        else
                            prow.NAZEV = (string)_browsingResultSet["NAZEV"];

                        prow.ID = (int)_browsingResultSet["ID"];

                        if (_browsingResultSet["KATEGORIE"] is System.DBNull)
                            prow.SetKATEGORIENull();
                        else
                            prow.KATEGORIE = (string)_browsingResultSet["KATEGORIE"];

                        if (_browsingResultSet["STRED"] is System.DBNull)
                            prow.SetSTREDNull();
                        else
                            prow.STRED = (string)_browsingResultSet["STRED"];
                        
                        if (_browsingResultSet["OSOBA"] is System.DBNull)
                            prow.SetOSOBANull();
                        else
                            prow.OSOBA = (int)_browsingResultSet["OSOBA"];

                        if (_browsingResultSet["LOKACE1"] is System.DBNull)
                            prow.SetLOKACE1Null();
                        else
                            prow.LOKACE1 = (string)_browsingResultSet["LOKACE1"];

                        if (_browsingResultSet["LOKACE2"] is System.DBNull)
                            prow.SetLOKACE2Null();
                        else
                            prow.LOKACE2 = (string)_browsingResultSet["LOKACE2"];

                        if (_browsingResultSet["KANCELAR"] is System.DBNull)
                            prow.SetKANCELARNull();
                        else
                            prow.KANCELAR = (string)_browsingResultSet["KANCELAR"];

                        if (_browsingResultSet["EAN"] is System.DBNull)
                            prow.SetEANNull();
                        else
                            prow.EAN = (string)_browsingResultSet["EAN"];

                        if (_browsingResultSet["KUSU"] is System.DBNull)
                            prow.SetKUSUNull();
                        else
                            prow.KUSU = (decimal)_browsingResultSet["KUSU"];

                        if (_browsingResultSet["KLIC_LOK"] is System.DBNull)
                            prow.SetKLIC_LOKNull();
                        else
                            prow.KLIC_LOK = (int)_browsingResultSet["KLIC_LOK"];

                        if (_browsingResultSet["NACTENO"] is System.DBNull)
                            //prow.SetNACTENONull();
                            prow.NACTENO = 0;
                        else
                            prow.NACTENO = (decimal)_browsingResultSet["NACTENO"];

                        //try
                        //{
                        //    if (!prow.IsI_CISLONull())
                        //        prow.NACTENO = Globals.ta_inventur.NasnimanoKusu(prow.I_CISLO) ?? 0;
                        //    else
                        //        prow.SetNACTENONull();
                        //}
                        //catch (Exception ex)
                        //{
                        //    Logging.Log.Write(ex);
                        //}

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
                                {
                                    prow.KANCELAR_NAZEV = dt_kancl[0].TEXT.Trim();
                                    prow.KANCELAR_POPIS = dt_kancl[0].NAZEV.Trim();
                                }
                                else
                                {
                                    prow.SetKANCELAR_NAZEVNull();
                                    prow.SetKANCELAR_POPISNull();
                                }
                            }
                            else
                            {
                                prow.SetKANCELAR_POPISNull();
                                prow.SetKANCELAR_NAZEVNull();
                            }
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

                        if (!_browsingResultSet.Read())
                            break;
                    }
                    //Sklady nemusi byt k dispozici
                    //try { if (sta_093.Connection.State == ConnectionState.Open) { sta_093.Connection.Close(); } }
                    //catch { }
                    _listPolozkyDS.Polozky.EndLoadData();
                }
                _firstItem = aktualItem;
                dataGrid.DataSource = _listPolozkyDS.Polozky;
                dataGrid.CurrentRowIndex = 0;
                UpdateForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void UpdateForm()
        {
            ListPolozkyDS.PolozkyRow prow = null;
            try
            {
                prow = SelectedRow;

                dataFieldITEMDESC.Data = prow.IsNAZEVNull() ? "-" : prow.NAZEV.Trim();
                dataFieldItemnmbr.Data = prow.IsI_CISLONull() ? "-" : prow.I_CISLO.Trim();
                dataFieldEAN.Data = prow.IsEANNull() ? "-" : prow.EAN.Trim();
                dataFieldLokace.Data =
                    (prow.IsKLIC_LOKNull() ? "?" : prow.KLIC_LOK.ToString()) + "," +
                    (prow.IsLOKACE_NAZEVNull() ? "-" : prow.LOKACE_NAZEV.Trim());
                dataFieldKancelar.Data =
                    (prow.IsKANCELARNull() ? "?" : prow.KANCELAR.Trim())  + "," +
                    (prow.IsKANCELAR_NAZEVNull() ? "-" : prow.KANCELAR_NAZEV.Trim());
                dataFieldOsoba.Data =
                    (prow.IsOSOBANull() ? "?" : prow.OSOBA.ToString()) + "," +
                    (prow.IsOSOBA_NAZEVNull() ? "-" : prow.OSOBA_NAZEV.Trim());
                dataFieldStredisko.Data =
                    (prow.IsSTREDNull() ? "?" : prow.STRED.Trim()) + "," +
                    (prow.IsSTRED_NAZEVNull() ? "-" : prow.STRED_NAZEV.Trim());

                dfKancelarNazev.Data = (prow.IsKANCELARNull() ? "?" : prow.IsKANCELAR_POPISNull() ? "-" : prow.KANCELAR_POPIS.Trim());

                dataFieldQUANTITY.Data = prow.IsKUSUNull() ? "-" : prow.KUSU.ToString(Settings.UIFormatDesCisel);
                dataFieldNasnimano.Data = prow.IsNACTENONull() ? "-" : prow.NACTENO.ToString(Settings.UIFormatDesCisel);
                dataFieldZbyva.Data = prow.IsZBYVANull() ? "-" : prow.ZBYVA.ToString(Settings.UIFormatDesCisel);
            }
            catch
            {
                dataFieldStredisko.Data = "-";
                dataFieldITEMDESC.Data = "-";
                dataFieldItemnmbr.Data = "-";
                dataFieldLokace.Data = "-";
                dataFieldQUANTITY.Data = "-";
                dataFieldEAN.Data = "-";
                dataFieldOsoba.Data = "-";
                dataFieldNasnimano.Data = "-";
                dataFieldZbyva.Data = "-";
            }
            finally
            {
            }

            sbInfo.Text = "Z:" + (_currentItem <= 0 ? 0 : _currentItem) + " z " + _itemsCount;
            sbInfo.Text += " F:" +
				(Inventura2.Inventura2_Instance.globalObject.active_lokace == null ? "l" : "L") +
				(Inventura2.Inventura2_Instance.globalObject.active_kancelar == null ? "k" : "K") +
				(Inventura2.Inventura2_Instance.globalObject.active_stredisko == null ? "s" : "S") +
				(Inventura2.Inventura2_Instance.globalObject.active_osoba == null ? "o" : "O") +
                (menuItemZobrazeniZbyvajici.Checked ? "(Z)" : string.Empty);
            if(prow != null)
                sbInfo.Text += " Umístìní:" + (prow.IsKANCELARNull() ? "" : prow.IsKANCELAR_POPISNull() ? "" : prow.KANCELAR_POPIS.Trim());
        }
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
                        panelGrid.Hide();
                        break;
                    case ZobrazeniType.List:
                        panelDetail.Hide();
                        panelGrid.Show();
                        break;
                    default:
                        panelDetail.Hide();
                        panelGrid.Show();
                        break;
                }
            }
        }
        private void SwitchRezim()
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

        #region Scanner
        delegate void ScannerEventHandlerCall(ScannerEventArgs e);
        private void OnScannerEvent(ScannerEventArgs e)
        {
            try
            {
                string ck = e.BarcodeData.Trim();
                if (ck.Length > 0)
                {
                    // TODO : dodelat hledani z naskenovani 
                    //ListPolozky.PolozkyRow prow = null;
                    Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;

                    if (NajdiPolozku(ck, out mrow))
                        VyplnPolozku(mrow);
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
        
        #region Hledani
        /// <summary>
        /// Najde polozku a oznaci ji jako aktivni v datagridu
        /// </summary>
        /// <param name="carkod">carovy kod polozky</param>
        /// <param name="lastindex">posledni nalezeny index</param>
        /// <returns>index nalezene polozky, vetsi nez posledni nalezeny index</returns>
        private bool NajdiPolozku(
            string carkod,
            out Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow
            )
        {
            mrow = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ScannerStop();

                //1) najit polozky
				Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByEAN_Majetek(carkod);
                
                if (dt_majetek.Count == 0) //nenalezeno
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show("Položka s èár. kódem '" + carkod + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }
                else if (dt_majetek.Count > 1) //nalezeno vice zaznamu
                {
                    MessageBoxBig.Show("Nalezeno více položek s è.k. '" + carkod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    _currentItem = -1;
                    CreateResultSet(_select_All, "EAN='" + carkod + "'");
                    return false;
                }
                else //je pouze jedna (0 byt uz nemuze)
                {
                    mrow = dt_majetek[0];
                }

                _currentItem = -1;
                CreateResultSet(_select_All, "ID=" + mrow.ID + "");

                UpdateForm();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex.Message, this.Text);
                return false;
            }
            finally
            {
                ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        private void NajdiPolozkuPozice()
        {
            try
            {
                ScannerStop();

                int pozice = _currentItem;
                using (SejmiKodForm skf = new SejmiKodForm("Pozice záznamu", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, (pozice).ToString()))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    pozice = int.Parse(skf.Kod);
                    if (pozice < 0 || _itemsCount < pozice)
                    {
                        MessageBoxBig.Show("Pozice je mimo rozsah (1-" + _itemsCount + ")", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                    //pozice--;
                }

                _currentItem = pozice;
                CreateResultSet(_select_current, _select_current_wherecondition);
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

                icomm_browsing.CommandText = "select nazev from majetek where nazev like '" + nazev + "%'";

                bool prevStateConnectionOpened = icomm_browsing.Connection.State == ConnectionState.Open;
                if (icomm_browsing.Connection.State == ConnectionState.Closed)
                {
                    icomm_browsing.Connection.Open();
                }
                
                var polozkyReader = icomm_browsing.ExecuteReader(CommandBehavior.SingleRow);
                founded = polozkyReader.Read();
                if (!prevStateConnectionOpened)
                {
                    icomm_browsing.Connection.Close();
                }

                //Polozka nenalezena
                if (!founded)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show("Položka s názvem '" + nazev + "' nenalezena", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }

                //polozka nalezena, tak zobrazit stav
                _currentItem = -1;
                _firstItem = -1;
                CreateResultSet(_select_All, "NAZEV like '" + nazev + "%'");
                UpdateForm();
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

        private void vyhledejPolozkuCarovyKod()
        {
            try
            {
                ScannerStop();

                string ck = string.Empty;
                using (Forms.SejmiKodForm skf = new SejmiKodForm("Èárový kód", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = "Hledat";
                    skf.Owner = this;
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    ck = skf.Kod;
                }

                //ListPolozky.PolozkyRow prow = null;
                Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
                NajdiPolozku(ck, out mrow);
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

        #endregion

        #region Pridani, mazani
        private void VyplnPolozku(ListPolozkyDS.PolozkyRow prow)
        {

            try
            {
                if (prow == null)
                {
                    MessageBoxBig.Show("Není vybrána položka!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
				Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByID_Majetek(prow.ID);
                Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
                Cursor.Current = Cursors.Default;

                if (dt_majetek.Count == 0)
                {
                    // TODO : zmenit text ... :)
                    MessageBoxBig.Show("Nenalezen záznam v tabulce I3!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return;
                }
                else if (dt_majetek.Count > 1)
                {
                    MessageBoxBig.Show("Nalezeno více øádkù se stejným ID !!!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                else
                {
                    mrow = dt_majetek[0]; //je tam jen jedna polozka a je na indexu 0
                }

                //jinak je to 1:1 a muzu to pustit dal

                VyplnPolozku(mrow);

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void VyplnPolozku(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        {

            try
            {
                ScannerStop();

				if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_UpozornitNaPrebytek)
                {//cfg_upozornit na prebytek...
                    //decimal nas = Convert.ToDecimal(ita_i4.Nasnimano(prow.ITEMNMBR) ?? 0);
                    if (mrow.NACTENO > mrow.KUSU)
                    {
                        MessageBoxBig.Show("Je nasnímáno vìtší množství než je v pøedloze!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    }
                }

                NaplnPolozku naplnp = this.NaplnPolozkuForm;
                naplnp.SetDefaultValues();
                naplnp.MajetekRow = mrow;
                naplnp.Owner = this;
                naplnp.Popis = "Množství";
                naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                naplnp.AllowEmpty = false;
                naplnp.Text = "Vložte množství";
                naplnp.Kod = string.Empty;
				if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
                {
					naplnp.ScannerOff = !Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_MnozstviScannerem;

					if (Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PredvyplnitMnozstvi)
                    {
						if (Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PredvyplnitMnozstviOJedna)
                            naplnp.Kod = "1";
						else if (Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PredvyplnitMnozstviZbyvajici)
                        {
                            //decimal nasnimano = Globals.ta_inventur.NasnimanoKusu(mrow.I_CISLO) ?? 0;
                            //decimal zbyva = nasnimano - mrow.KUSU;
                            decimal zbyva = mrow.ZBYVA;
                            naplnp.Kod = (zbyva > 0 ? zbyva : 0).ToString(Settings.UIFormatDesCisel);
                        }
                    }
                }

                #region Post kontroly zadanych hodnot
                //if (naplnp.ShowDialog() == DialogResult.Cancel)
                //    return;

                //Kontroly zadavani hodnot ...
                while (true)
                {
                    if (naplnp.ShowDialog() == DialogResult.Cancel)
                    {
						if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                        {
                            if (mrow.NACTENO < mrow.KUSU)
                            {
                                if (MessageBoxBig.Show("Není kompletní.\nChcete skonèit zadávání této položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                    continue;
                            }
                        }
                        return;
                    }

					if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                    {
                        if (mrow.NACTENO + Convert.ToDecimal(naplnp.Kod) > mrow.KUSU)
                        {
                            if (MessageBoxBig.Show("Zadané množství je vìtší než má být naèteno, chcete pokraèovat a data uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                continue;
                        }
                    }

					if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
                    {
						if (!Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
                        {
                            MessageBoxBig.Show("Zadané množství musí být kladné a vìtší jak 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            continue;
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(naplnp.Kod) == 0)
                        {
                            MessageBoxBig.Show("Zadané množství musí být rùzné od 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            continue;
                        }
                    }

                    break; //dostane-li se až sem, tak je vše ok ...
                }
                #endregion


                decimal mnozstvi = Convert.ToDecimal(naplnp.Kod);
                Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow newlokace = naplnp.Lokace;
                Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow newkancl = naplnp.Kancelar;
                Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow newosoba = naplnp.Osoba;
                Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow newstredisko = naplnp.Stredisko;

                #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    _inventura2.INVENTUR.Clear();
                    Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow newi2row = _inventura2.INVENTUR.NewINVENTURRow();

					newi2row.ID = ((Inventura2.Inventura2_Instance.globalObject.controller_inventura2.MaxID_Inventur() ?? 0) + 1);
                    if (!mrow.IsKATEGORIENull()) newi2row.KATEGORIE = mrow.KATEGORIE;
                    if (!mrow.IsI_CISLONull()) newi2row.I_CISLO = mrow.I_CISLO;
                    if (!mrow.IsNAZEVNull()) newi2row.NAZEV = mrow.NAZEV;

                    if (newstredisko == null) newi2row.SetSTREDNull();
                    else newi2row.STRED = newstredisko.STREDISKO;

                    if (newosoba == null) newi2row.SetOSOBANull();
                    else newi2row.OSOBA = newosoba.OSOBA_ZODP;

                    if (newlokace == null)
                    {
                        newi2row.SetLOKACE1Null();
                        newi2row.SetLOKACE2Null();
                        newi2row.SetKLIC_LOKNull();
                    }
                    else
                    {
                        newi2row.LOKACE1 = newlokace.LOKACE1;
                        newi2row.LOKACE2 = newlokace.LOKACE2;
                        newi2row.KLIC_LOK = newlokace.KLIC_LOK;
                    }

                    if (newkancl == null) newi2row.SetKANCELARNull();
                    else newi2row.KANCELAR = newkancl.KANCL;

                    if (!mrow.IsEANNull()) newi2row.EAN = mrow.EAN;
                    newi2row.KUSU = mnozstvi;

                    if (!mrow.IsID_INVNull()) newi2row.ID_INV = Convert.ToDecimal(mrow.ID_INV);
                    newi2row.OS_ZPR = MST_Global.UserID.ToString();
                    newi2row.ID_TERM = MST_Global.TerminalID;
                    newi2row.CAS_ZPR = DateTime.Now;
                    newi2row.ID_MAJETEK = mrow.ID;

                    _inventura2.INVENTUR.AddINVENTURRow(newi2row);
					Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Update_Inventur(newi2row);

                    UpdateNacteno(newi2row);

                    _inventura2.INVENTUR.Clear();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
            finally
            {
                ScannerStart();
            }
            
            UpdateForm();
        }

        private void UpdateNacteno(Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow i2row)
        {
            //ListPolozkyDS.PolozkyRow[] polozkyRows = (ListPolozkyDS.PolozkyRow[])_listPolozkyDS.Polozky.Select("I_CISLO='" + i2row.I_CISLO.Trim() + "'");
            //ListPolozkyDS.PolozkyRow[] polozkyRows = (ListPolozkyDS.PolozkyRow[])_listPolozkyDS.Polozky.Select("I_CISLO='" + i2row.I_CISLO.Trim() + "' and KATEGORIE='" + i2row.KATEGORIE.Trim() + "'");
            ListPolozkyDS.PolozkyRow[] polozkyRows = (ListPolozkyDS.PolozkyRow[])_listPolozkyDS.Polozky.Select("ID=" + i2row.ID_MAJETEK);
            for (int i = 0; i < polozkyRows.Length; i++)
            {
                polozkyRows[i].NACTENO += i2row.KUSU;
            }
            // >>> zde taky I_Cislo a Kategorie ...
            //int result = Globals.ta_majetek.UpdateNactenoByI_CISLO(i2row.KUSU, i2row.I_CISLO);
            //int result = Globals.ta_majetek.UpdateNactenoByZAZNAM(i2row.KUSU, i2row.I_CISLO, i2row.KATEGORIE);
			int result = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.UpdateNactenoByID_Majetek(i2row.KUSU, i2row.ID_MAJETEK);
            if (menuItemZobrazeniZbyvajici.Checked)
            {
                int tmpcurrentitem = _currentItem;
                _currentItem = _firstItem + 1;
                CreateResultSet(_select_current, _select_current_wherecondition);
                _currentItem = tmpcurrentitem;
                dataGrid.CurrentRowIndex = _currentItem - 1 - _firstItem;
            }
        }

        private void SmazPolozku(ListPolozkyDS.PolozkyRow polozkyRow)
        {
            try
            {
                if (polozkyRow == null)
                    return;

                if (MessageBoxBig.Show("Opravdu smazat nasnímaná data položky '" + polozkyRow.NAZEV + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No
                    )
                    return;

                //int raff = Globals.ta_inventur.DeleteByI_Cislo(polozkyRow.I_CISLO);
                //int raff = Globals.ta_inventur.DeleteByZAZNAM(polozkyRow.I_CISLO, polozkyRow.KATEGORIE);
				int raff = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.DeleteByID_MAJETEK_Inventur(polozkyRow.ID);
                //raff = Globals.ta_majetek.UpdateNactenoByI_CISLO(-polozkyRow.NACTENO, polozkyRow.I_CISLO);
                //raff = Globals.ta_majetek.UpdateNactenoByZAZNAM(-polozkyRow.NACTENO, polozkyRow.I_CISLO, polozkyRow.KATEGORIE);
				raff = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.UpdateNactenoByID_Majetek(-polozkyRow.NACTENO, polozkyRow.ID);
                polozkyRow.NACTENO -= polozkyRow.NACTENO;

                if (menuItemZobrazeniZbyvajici.Checked)
                {
                    int tmpcurrentitem = _currentItem;
                    _currentItem = _firstItem + 1;
                    CreateResultSet(_select_current, _select_current_wherecondition);
                    _currentItem = tmpcurrentitem;
                    dataGrid.CurrentRowIndex = _currentItem - 1 - _firstItem;
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
        #endregion

        #region Kontroly
        // TODO : nepouziva se, ale mohlo byse ...
        private bool kontrolaUplnostiDavky()
        {
            //int? zbyva = Globals.ta_queries.ZbyvaPolozek();
            //return (zbyva ?? 0) == 0;
            try
            {
				if (Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnosti)
                {
					int? zbyva = (int)Inventura2.Inventura2_Instance.globalObject.controller_inventura2.ZbyvaPolozek();

					if ((zbyva ?? 0) == 0)
						return true;
					else
						return false;
                }
                else 
                    return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return false;
            }
        }
        #endregion

        #region Udalosti uzivatelskeho vstupu
        private void ListPolozky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == MST_Global.DataGridScrollDown)
            {
                if (dataGrid.CurrentRowIndex == _listPolozkyDS.Polozky.Rows.Count - 1 && (_firstItem + _listPolozkyDS.Polozky.Count) < _itemsCount)
                { //jsem na posledni polozce
                    _currentItem++;
                    CreateResultSet(_select_current, _select_current_wherecondition);
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == MST_Global.DataGridScrollUp)
            {
                if (dataGrid.CurrentRowIndex == 0 && _currentItem > 1)
                {
                    _currentItem -= _pocetZobrazit;
                    CreateResultSet(_select_current, _select_current_wherecondition);
                    e.Handled = true;
                    dataGrid.CurrentRowIndex = _listPolozkyDS.Polozky.Rows.Count - 1; //na posledni index
                    return;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec(true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                VyplnPolozku(this.SelectedRow);
            }
            else if (e.KeyCode == Keys.Back)
            {
                SmazPolozku(this.SelectedRow);
            }
            else if (e.KeyCode == Keys.D1)
            {
                ZobrazitVse();
            }
            else if (e.KeyCode == Keys.D2)
            {
                SwitchRezim();
            }
            else if (e.KeyCode == Keys.D3)
            {
                menuItemNasnimane_Click(null, null);
            }
            else if (e.KeyCode == Keys.D4)
            {
                zmenaZbyvajici();
            }
            else if (e.KeyCode == Keys.D5)
            {
                if (MST_Global.Inventura2DotazLokace)
                    zmenaLokace();
            }
            else if (e.KeyCode == Keys.D6)
            {
                if (MST_Global.Inventura2DotazKancl)
                    zmenaKancelare();
            }
            else if (e.KeyCode == Keys.D7)
            {
                if (MST_Global.Inventura2DotazStredisko)
                    zmenaStrediska();
            }
            else if (e.KeyCode == Keys.D8)
            {
                if (MST_Global.Inventura2DotazOsoba)
                    zmenaOsoby();
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
            else if (e.KeyCode == Keys.F4)
            {
                RFID_Aktivuj();
            }
            else
                return;

            e.Handled = true;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonLeft)
            {
                _currentItem -= _pocetZobrazit;
                CreateResultSet(_select_current, _select_current_wherecondition);
                dataGrid.CurrentRowIndex = _listPolozkyDS.Polozky.Rows.Count - 1; //na posledni index
            }
            else if (e.Button == toolBarButtonRight)
            {
                _currentItem = _firstItem + _pocetZobrazit;
               // _currentItem += _pocetZobrazit;
                CreateResultSet(_select_current, _select_current_wherecondition);
            }
            else if (e.Button == toolBarButtonStart)
            {
                _currentItem = -1;
                CreateResultSet(_select_current, _select_current_wherecondition);
            }
            else if (e.Button == toolBarButtonEnd)
            {
                _currentItem = _itemsCount - _pocetZobrazit + 1;
                CreateResultSet(_select_current, _select_current_wherecondition);
            }
        }

        private void ListPolozky_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            ScannerStop();
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformKonec(true);
        }

        private void buttonZadat_Click(object sender, EventArgs e)
        {
            VyplnPolozku(this.SelectedRow);
        }

        private void buttonVyhledej_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuCarovyKod();
        }

        private void menuItemZobrazeniSwitchRezim_Click(object sender, EventArgs e)
        {
            SwitchRezim();
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            _currentItem = _firstItem + dataGrid.CurrentRowIndex + 1;
            this.UpdateForm();
        }

        private void menuItemZobrazeniVse_Click(object sender, EventArgs e)
        {
            ZobrazitVse();
        }

        private void ZobrazitVse()
        {
            int tmpcurrentitem = _currentItem;
            _currentItem = _firstItem + 1;
            SetActualSelectCondition();
            CreateResultSet(_select_All, _select_current_wherecondition);
            //CreateResultSet();
            _currentItem = tmpcurrentitem;
            dataGrid.CurrentRowIndex = _currentItem - 1 - _firstItem;
        }

        private void menuItemHledatCarKod_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuCarovyKod();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuNazev();
        }

        private void menuItemNasnimane_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();
                //using (Nasnimane_sqlce naspol = new Nasnimane_sqlce(Path.Combine(Main.StorageDir, davka.ToString() + "." + Main.Inventura1I1)))
                using (Nasnimane naspol = new Nasnimane())
                {
                    naspol.Owner = this;
                    naspol.ShowDialog();
                    if (naspol.Deleted)
                    {
                        int tmpcurrentitem = _currentItem;
                        _currentItem = _firstItem + 1;
                        CreateResultSet(_select_current, _select_current_wherecondition);
                        _currentItem = tmpcurrentitem;
                        dataGrid.CurrentRowIndex = _currentItem - 1 - _firstItem;
                    }
                }
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            SmazPolozku(this.SelectedRow);
        }

        private void menuItemHledatPozice_Click(object sender, EventArgs e)
        {
            NajdiPolozkuPozice();
        }

        #endregion

        private void menuItemZmenaLokace_Click(object sender, EventArgs e)
        {
            zmenaLokace();
        }

        private void zmenaLokace()
        {
            try
            {
                ScannerStop();

                using (ZmenaLokace fLokace = new ZmenaLokace())
                {
                    fLokace.Owner = this;
					fLokace.Lokace = Inventura2.Inventura2_Instance.globalObject.active_lokace;
                    if (fLokace.ShowDialog() == DialogResult.Cancel)
                        return;
					Inventura2.Inventura2_Instance.globalObject.active_lokace = fLokace.Lokace;

                    _currentItem = -1;
                    SetActualSelectCondition();
                    CreateResultSet(_select_current, _select_current_wherecondition);
                }

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

        private void menuItemZmenaKancl_Click(object sender, EventArgs e)
        {
            zmenaKancelare();
        }

        private void zmenaKancelare()
        {
            try
            {
                ScannerStop();

                using (ZmenaKancl fkancl = new ZmenaKancl())
                {
                    fkancl.Owner = this;
					fkancl.Kancelar = Inventura2.Inventura2_Instance.globalObject.active_kancelar;
                    if (fkancl.ShowDialog() == DialogResult.Cancel)
                        return;
					Inventura2.Inventura2_Instance.globalObject.active_kancelar = fkancl.Kancelar;

                    _currentItem = -1;
                    SetActualSelectCondition();
                    CreateResultSet(_select_current, _select_current_wherecondition);
                }
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

        private void menuItemZmenaStrediska_Click(object sender, EventArgs e)
        {
            zmenaStrediska();
        }

        private void zmenaStrediska()
        {
            try
            {
                ScannerStop(); 

                using (ZmenaStredisko fstredisko = new ZmenaStredisko())
                {
                    fstredisko.Owner = this;
					fstredisko.Stredisko = Inventura2.Inventura2_Instance.globalObject.active_stredisko;
                    if (fstredisko.ShowDialog() == DialogResult.Cancel)
                        return;
                    SetActualSelectCondition();
					Inventura2.Inventura2_Instance.globalObject.active_stredisko = fstredisko.Stredisko;

                    _currentItem = -1;
                    CreateResultSet(_select_current, _select_current_wherecondition);
                }
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

        private void menuItemZmenaOsoby_Click(object sender, EventArgs e)
        {
            zmenaOsoby();
        }

        private void zmenaOsoby()
        {
            try
            {
                ScannerStop();

                using (ZmenaOsoba fosoba = new ZmenaOsoba())
                {
                    fosoba.Owner = this;
					fosoba.Osoba = Inventura2.Inventura2_Instance.globalObject.active_osoba;
                    if (fosoba.ShowDialog() == DialogResult.Cancel)
                        return;
					Inventura2.Inventura2_Instance.globalObject.active_osoba = fosoba.Osoba;

                    _currentItem = -1;
                    SetActualSelectCondition();
                    CreateResultSet(_select_current, _select_current_wherecondition);
                }
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

        private void menuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                using (FiltryInfo finfo = new FiltryInfo())
                {
                    finfo.ShowDialog();
                }
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemZobrazeniZbyvajici_Click(object sender, EventArgs e)
        {
            zmenaZbyvajici();
        }

        private void zmenaZbyvajici()
        {
            menuItemZobrazeniZbyvajici.Checked = !menuItemZobrazeniZbyvajici.Checked;
            SetActualSelectCondition();
            CreateResultSet(_select_current, _select_current_wherecondition);
        }

        /// <summary>
        /// aktivace tlacitka v menu pro RFID dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemRFID_Click(object sender, EventArgs e)
        {
            RFID_Aktivuj();
        }

        private void RFID_Aktivuj()
        {
            try
            {
                ScannerStop();

                using (SnimatRFID rfid = new SnimatRFID())
                {
                    rfid.Owner = this;
                    rfid.ShowDialog();
                }

                ZobrazitVse();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void ListPolozky_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ListPolozky_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}