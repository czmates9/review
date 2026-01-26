using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.Collections.Generic;
using Fask.ScannerProvider;
using Fask.Graphic;
using Fask.MST_W.Classes;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class ListPolozky_sqlce : System.Windows.Forms.Form
    {
        //Typ vyberu - vice viz Classes.Enums (0 == nesnastaveno, ale nemelo by byt - znaci chybu v programu)
        private byte _input_mode = 0;

        #region Promenne
        int _itemsCount = -1;   //Pocet polozek ve vysledku dotazu na I1
        int _currentItem = -1;  //Aktualni poradove cislo v datagridu <0 = inicializovano
        int _firstItem = -1;    //Poradi ve vysledku dotazu prvni polozky zobrazene v datagridu
        int _pocetZobrazit = 10;//pocet polozek, ktere se budou nacitat z resultsetu

        //Vytazeni vsech polozek z I1
        private const string _select_All = "select * from czmst_i1";
        //Posledni pouzity dotaz, ktery naplnil datagrid
        private string _select_current = _select_All;
        private string _select_current_wherecondition = string.Empty;

        //Dataset, ktery obsahuje natazena data z databaze (minimum)
        private Fask.SQLiteDBs.DataSets.Inventura1 _inventura1 = new Fask.SQLiteDBs.DataSets.Inventura1();
        private ListPolozky _listPolozky = new ListPolozky();
        Fask.SQLiteDBs.DataSets.Inventura1.ParametryRow parrow = null;

        #endregion

        #region Vlastnosti
        private NaplnPolozku_sqlce _naplnPolozkuForm = null;
        private NaplnPolozku_sqlce NaplnPolozkuForm
        {
            get
            {
                if (_naplnPolozkuForm == null)
                    _naplnPolozkuForm = new NaplnPolozku_sqlce();
                if (_naplnPolozkuForm.IsDisposed)
                    _naplnPolozkuForm = new NaplnPolozku_sqlce();
                return _naplnPolozkuForm;
            }
        }

        private NaplnPolozkuSN _naplnPolozkuSNForm = null;
        private NaplnPolozkuSN NaplnPolozkuSNForm
        {
            get
            {
                if (_naplnPolozkuSNForm == null)
                    _naplnPolozkuSNForm = new NaplnPolozkuSN();
                if (_naplnPolozkuSNForm.IsDisposed)
                    _naplnPolozkuSNForm = new NaplnPolozkuSN();
                return _naplnPolozkuSNForm;
            }
        }


        /// <summary>
        /// Aktivni(vybrany) zaznam v datagridu
        /// </summary>
        private ListPolozky.PolozkyRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)dataGrid.BindingContext[_listPolozky.Polozky].Current).Row as ListPolozky.PolozkyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Konstruktor + Inicializace

        public ListPolozky_sqlce()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            this.miTisk.Enabled = MST_Global.PovolitPrintServer;

            Cursor.Current = Cursors.Default;
        }

        private DataGridTableStyle dgstyle = null;
        private DataGrid2TextBoxColumn dgITEMNMBR = null;
        private DataGrid2TextBoxColumn dgITEMDESC = null;
        private DataGrid2TextBoxColumn dgQUANTITY = null;
        private DataGrid2TextBoxColumn dgITEMCODE = null;
        private DataGrid2TextBoxColumn dgNASNIMANO = null;
        private DataGrid2TextBoxColumn dgLOCNCODE = null;
        private DataGrid2TextBoxColumn dgCZ_SERNUM_TRACK = null;
        private DataGrid2TextBoxColumn dgCZ_SERNUM_FIND = null;
        private DataGrid2TextBoxColumn dgCZ_CarKod = null;
        private DataGrid2TextBoxColumn dgSklad = null;
        private DataGrid2TextBoxColumn dgSkladID = null;
        private DataGrid2TextBoxColumn dgDMJ = null;
        private DataGrid2TextBoxColumn dgrez1 = null;
        private DataGrid2TextBoxColumn dgrez2 = null;

        private void GridStylesCreate()
        {
            try
            {
                //List<DataGridColumnStyle> dglist = new List<DataGridColumnStyle>();

                dgstyle = new DataGridTableStyle();
                dgstyle.MappingName = _listPolozky.Polozky.TableName;

                dgITEMDESC = new DataGrid2TextBoxColumn();
                dgITEMDESC.MappingName = _listPolozky.Polozky.ITEMDESCColumn.ColumnName;
                dgITEMDESC.HeaderText = "Název";
                dgITEMDESC.NullText = "-";
                dgITEMDESC.Width = Settings.Inventura1ITEMDESCWidth;
                dgstyle.GridColumnStyles.Add(dgITEMDESC);
                //dglist.Add(dgITEMDESC);


                dgITEMCODE = new DataGrid2TextBoxColumn();
                dgITEMCODE.MappingName = _listPolozky.Polozky.ITEMCODEColumn.ColumnName;
                dgITEMCODE.HeaderText = "Kód položky";
                dgITEMCODE.NullText = "-";
                dgITEMCODE.Width = Settings.Inventura1ITEMDESCWidth;
                dgstyle.GridColumnStyles.Add(dgITEMCODE);

                dgCZ_CarKod = new DataGrid2TextBoxColumn();
                dgCZ_CarKod.MappingName = _listPolozky.Polozky.CZ_CarKodColumn.ColumnName;
                dgCZ_CarKod.HeaderText = "Èár. kód";
                dgCZ_CarKod.NullText = "-";
                dgCZ_CarKod.Width = Settings.Inventura1CZ_CarKodWidth;
                dgstyle.GridColumnStyles.Add(dgCZ_CarKod);
                //dglist.Add(dgCZ_CarKod);

                dgNASNIMANO = new DataGrid2TextBoxColumn();
                dgNASNIMANO.MappingName = _listPolozky.Polozky.NASNIMANOColumn.ColumnName;
                dgNASNIMANO.HeaderText = "Nasnímáno";
                dgNASNIMANO.NullText = "-";
                dgNASNIMANO.Format = "0.00";
                dgNASNIMANO.Width = Settings.Inventura1NASNIMANOWidth;
                dgstyle.GridColumnStyles.Add(dgNASNIMANO);
                //dglist.Add(dgNASNIMANO);

                dgQUANTITY = new DataGrid2TextBoxColumn();
                dgQUANTITY.MappingName = _listPolozky.Polozky.QUANTITYColumn.ColumnName;
                dgQUANTITY.HeaderText = "Množství";
                dgQUANTITY.NullText = "-";
                dgQUANTITY.Format = "0.00";
                dgQUANTITY.Width = Settings.Inventura1QUANTITYWidth;
                if (parrow != null && !parrow.CFG_PovolitZobrazeniMnozstviNaSklade)
                {//neprida zobrazeni quantity                    
                }
                else
                {
                    dgstyle.GridColumnStyles.Add(dgQUANTITY);
                    //dglist.Add(dgQUANTITY);
                }


                dgLOCNCODE = new DataGrid2TextBoxColumn();
                dgLOCNCODE.MappingName = _listPolozky.Polozky.LOCNCODEColumn.ColumnName;
                dgLOCNCODE.HeaderText = "Lokace";
                dgLOCNCODE.NullText = "-";
                dgLOCNCODE.Width = Settings.Inventura1LOCNCODEWidth;
                dgstyle.GridColumnStyles.Add(dgLOCNCODE);
                //dglist.Add(dgLOCNCODE);

                dgCZ_SERNUM_TRACK = new DataGrid2TextBoxColumn();
                dgCZ_SERNUM_TRACK.MappingName = _listPolozky.Polozky.CZ_SERNUM_TRACKColumn.ColumnName;
                dgCZ_SERNUM_TRACK.HeaderText = "SN sledovat";
                dgCZ_SERNUM_TRACK.NullText = "-";
                dgCZ_SERNUM_TRACK.Width = Settings.Inventura1CZ_SERNUM_TRACKWidth;
                dgstyle.GridColumnStyles.Add(dgCZ_SERNUM_TRACK);
                //dglist.Add(dgCZ_SERNUM_TRACK);

                dgCZ_SERNUM_FIND = new DataGrid2TextBoxColumn();
                dgCZ_SERNUM_FIND.MappingName = _listPolozky.Polozky.CZ_SERNUM_FINDColumn.ColumnName;
                dgCZ_SERNUM_FIND.HeaderText = "SN dohledávat";
                dgCZ_SERNUM_FIND.NullText = "-";
                dgCZ_SERNUM_FIND.Width = Settings.Inventura1CZ_SERNUM_FINDWidth;
                dgstyle.GridColumnStyles.Add(dgCZ_SERNUM_FIND);
                //dglist.Add(dgCZ_SERNUM_FIND);

                dgITEMNMBR = new DataGrid2TextBoxColumn();
                dgITEMNMBR.MappingName = _listPolozky.Polozky.ITEMNMBRColumn.ColumnName;
                dgITEMNMBR.HeaderText = "È. pol.";
                dgITEMNMBR.NullText = "-";
                dgITEMNMBR.Width = Settings.Inventura1ITEMNMBRWidth;
                dgstyle.GridColumnStyles.Add(dgITEMNMBR);
                //dglist.Add(dgITEMNMBR);

                dgSklad = new DataGrid2TextBoxColumn();
                dgSklad.MappingName = _listPolozky.Polozky.SkladColumn.ColumnName;
                dgSklad.HeaderText = "Sklad";
                dgSklad.NullText = "-";
                dgSklad.Width = Settings.Inventura1SkladWidth;
                dgstyle.GridColumnStyles.Add(dgSklad);
                //dglist.Add(dgSklad);

                dgSkladID = new DataGrid2TextBoxColumn();
                dgSkladID.MappingName = _listPolozky.Polozky.SkladIDColumn.ColumnName;
                dgSkladID.HeaderText = "ID Sklad";
                dgSkladID.NullText = "-";
                dgSkladID.Width = Settings.Inventura1SkladIDWidth;
                dgstyle.GridColumnStyles.Add(dgSkladID);
                //dglist.Add(dgSkladID);

                dgDMJ = new DataGrid2TextBoxColumn();
                dgDMJ.MappingName = _listPolozky.Polozky.DMJColumn.ColumnName;
                dgDMJ.HeaderText = "DMJ";
                dgDMJ.NullText = "-";
                dgDMJ.Width = 50;
                dgstyle.GridColumnStyles.Add(dgDMJ);
                //dglist.Add(dgDMJ);

                dgrez1 = new DataGrid2TextBoxColumn();
                dgrez1.MappingName = _listPolozky.Polozky.REZ1Column.ColumnName;
                dgrez1.HeaderText = "REZ1";
                dgrez1.NullText = "-";
                dgrez1.Width = 50;
                dgstyle.GridColumnStyles.Add(dgrez1);

                dgrez2 = new DataGrid2TextBoxColumn();
                dgrez2.MappingName = _listPolozky.Polozky.REZ2Column.ColumnName;
                dgrez2.HeaderText = "REZ2";
                dgrez2.NullText = "-";
                dgrez2.Width = 50;
                dgstyle.GridColumnStyles.Add(dgrez2);

                dataGrid.TableStyles.Add(dgstyle);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void ListPolozky_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Size = Forms.FormLocation.ScreenResolution;

            if (!Settings.Online_BYZNYS)
            {
                mainMenu1.MenuItems.Remove(menuItemOnline);
            }
            var ih_dt = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetData_IH();
            if (ih_dt.Count <= 0)
            {
				ih_dt.AddCZMST_IHRow(Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value , Guid.NewGuid());
                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.Update_IH(ih_dt);
            }

            Fask.SQLiteDBs.DataSets.Inventura1.ParametryDataTable pardt = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetData_Parametry();
            parrow = pardt.Rows.Count > 0 ? pardt[0] : null;

            //Vytvoreni gridu ...
            GridStylesCreate();

            MyInitializeGrid();

            if (parrow != null && !parrow.CFG_PovolitZobrazeniMnozstviNaSklade)
                dataFieldQUANTITY.Visible = false;

            CreateResultSet(_select_All, string.Empty);

            panelGrid.Dock = DockStyle.Fill;
            panelDetail.Dock = DockStyle.Fill;

            Zobrazeni = ZobrazeniType.List;

            this.dataGrid.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid.KeyScrollUp = MST_Global.DataGridScrollUp;

            dataGrid.Focus();

            this.menuItemOnlineNovyEAN.Enabled = Settings.Online_BYZNYS;

            ScannerStart();
            UpdateForm();


        }

        private void MyInitializeGrid()
        {
            this.dataGrid.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid.Font = new Font(this.dataGrid.Font.Name, Settings.UIGridFont, this.dataGrid.Font.Style);
            this.dataGrid.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PerformKonec(bool question)
        {
            if (question && MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceUkoncitDotaz, Fask.Localization.Localization.Inventura1ListPolozkySqlceDotaz,
                MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            else
                DialogResult = DialogResult.OK;

            ScannerFinalize();


            //Ulozeni nastaveni zobrazeni sloupcu v datagridu
            this.dataGrid.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));

            if (_naplnPolozkuForm != null && !_naplnPolozkuForm.IsDisposed)
            {
                _naplnPolozkuForm.Dispose();
                _naplnPolozkuForm = null;
            }

            if (_naplnPolozkuSNForm != null && !_naplnPolozkuSNForm.IsDisposed)
            {
                _naplnPolozkuSNForm.Dispose();
                _naplnPolozkuSNForm = null;
            }
        }

        #endregion

        #region Zobrazeni

        private void CreateResultSet(string selectCommandExecute, string wherecondition) //, string orderbycondition)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    _select_current = selectCommandExecute;
                    _select_current_wherecondition = wherecondition;
                    string _select_current_count = "select count(*) as itemscount from czmst_i1 " + _select_current_wherecondition;

					_itemsCount = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CreateResultSet_GetGount(_select_current_count);


					if (_currentItem <= 0)
					{
						_currentItem = 1;
						_firstItem = _currentItem - 1;
					}
					if (_currentItem > _itemsCount)
					{
						_currentItem -= _pocetZobrazit;
					}
					int aktualItem = _currentItem - 1;
					_firstItem = aktualItem;

					var dt_I1 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CreateResultSet_Get_I1(_select_current + _select_current_wherecondition, aktualItem, (aktualItem + _pocetZobrazit));

					FillGrid(dt_I1);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

		/// <summary>
		/// Nove naplni list polozek z I1 dat...
		/// </summary>
		/// <param name="dt_I1"></param>
		private void FillGrid(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable dt_I1)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				_listPolozky.Clear();

				_listPolozky.Polozky.BeginLoadData();
				//Sklady nemusi byt k dispozici
				bool sta_093_exist = File.Exists(Main.CiselnikSkladyDB);
				if (sta_093_exist)
				{
					try { Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.Connection_Open(); }
					catch { }
				}

				foreach (var row_i1 in dt_I1)
				{
					ListPolozky.PolozkyRow prow = _listPolozky.Polozky.NewPolozkyRow();

					prow.ITEMNMBR = row_i1.ITEMNMBR;
					prow.ITEMDESC = row_i1.ITEMDESC;
					prow.QUANTITY = row_i1.QUANTITY;
					prow.LOCNCODE = row_i1.LOCNCODE;
					prow.CZ_CarKod = row_i1.CZ_CarKod;
					prow.CZ_SERNUM_FIND = row_i1.CZ_SerNum_Find;
					prow.CZ_SERNUM_TRACK = row_i1.CZ_SerNum_Track;
					prow.DMJ = row_i1.IsDMJNull() ? string.Empty : row_i1.DMJ;
					prow.REZ1 = row_i1.IsREZ_1Null() ? string.Empty : row_i1.REZ_1;
					prow.REZ2 = row_i1.IsREZ_2Null() ? string.Empty : row_i1.REZ_2;
					prow.CZ_REZ_1_TRACK = row_i1.IsCZ_REZ1_TrackNull() ? (byte)0 : row_i1.CZ_REZ1_Track;
					prow.CZ_REZ_2_TRACK = row_i1.IsCZ_REZ2_TrackNull() ? (byte)0 : row_i1.CZ_REZ2_Track;
					prow.ITEMCODE = row_i1.IsITEMCODENull() ? string.Empty : row_i1.ITEMCODE;
                    prow.CZ_Expirace_Track = row_i1.CZ_Expirace_Track;

					
					#region Dotazeni informaci o skladu
					try
					{
						if (!row_i1.Isskl_idNull())
						{
							prow.SkladID = row_i1.skl_id;
							if (sta_093_exist)
								prow.Sklad = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.GetSkladDesc(prow.SkladID);
							else
								prow.Sklad = null;
						}
						else
						{
							prow.SkladID = null;
							prow.Sklad = null;
						}

						if (prow.IsSkladNull() || prow.Sklad == null)
							prow.Sklad = "-";
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
					}
					#endregion

					if (String.IsNullOrEmpty(prow.SkladID))
						prow.NASNIMANO = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.NasnimanoQuantity_I4(prow.ITEMNMBR.Trim()) ?? 0;
					else
						prow.NASNIMANO = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.NasnimanoQuantity_I4(prow.ITEMNMBR.Trim(), prow.SkladID.Trim()) ?? 0;


					_listPolozky.Polozky.AddPolozkyRow(prow);
				}

				//Sklady nemusi byt k dispozici
				if (sta_093_exist)
				{
					try { Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.Connection_Close(); }
					catch { }
				}

				_listPolozky.Polozky.EndLoadData();

				dataGrid.DataSource = _listPolozky.Polozky;
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
            try
            {
                ListPolozky.PolozkyRow prow = SelectedRow;

                try { dataFieldCarKodVlastni.Data = prow.CZ_CarKod.Trim(); }
                catch { dataFieldCarKodVlastni.Data = "-"; }
                try { dataFieldITEMDESC.Data = prow.ITEMDESC.Trim(); }
                catch { dataFieldITEMDESC.Data = "-"; }
                try { dataFieldItemnmbr.Data = prow.ITEMNMBR.Trim(); }
                catch { dataFieldItemnmbr.Data = "-"; }
                try { dataFieldLocnCode.Data = prow.LOCNCODE.Trim(); }
                catch { dataFieldLocnCode.Data = "-"; }
                try { dataFieldQUANTITY.Data = prow.QUANTITY.ToString(Settings.UIFormatDesCisel); }
                catch { dataFieldQUANTITY.Data = "-"; }
                try { dataFieldSNFind.Data = prow.CZ_SERNUM_FIND.ToString(); }
                catch { dataFieldSNFind.Data = "-"; }
                try { dataFieldSNTrack.Data = prow.CZ_SERNUM_TRACK.ToString(); }
                catch { dataFieldSNTrack.Data = "-"; }
                //try { dataFieldNasnimano.Data = (Convert.ToDecimal(ita_i4.Nasnimano(dataFieldItemnmbr.Data) ?? 0)).ToString("0.00"); }
                try { dataFieldNasnimano.Data = prow.NASNIMANO.ToString(Settings.UIFormatDesCisel); }
                catch { dataFieldNasnimano.Data = "-"; }
                try { dataFieldSklad.Data = prow.SkladID.Trim() + ":" + prow.Sklad.Trim(); }
                catch { dataFieldSklad.Data = "-"; }
                try { dataFieldREZ1.Data = prow.REZ1.Trim(); }
                catch { dataFieldREZ1.Data = "-"; }
                try { dataFieldREZ2.Data = prow.REZ2.Trim(); }
                catch { dataFieldREZ2.Data = "-"; }
                try { dataFieldITEMCODE.Data = prow.ITEMCODE.Trim(); }
                catch { dataFieldITEMCODE.Data = "-"; }

                try
                {
                    dataFieldREZ1.Text = dgrez1.HeaderText;
                    dataFieldREZ2.Text = dgrez2.HeaderText;
                }
                catch
                {
                    dataFieldREZ1.Text = "REZ_1";
                    dataFieldREZ1.Text = "REZ_2";
                }

            }
            catch
            {
                dataFieldCarKodVlastni.Data = "-";
                dataFieldITEMDESC.Data = "-";
                dataFieldItemnmbr.Data = "-";
                dataFieldLocnCode.Data = "-";
                dataFieldQUANTITY.Data = "-";
                dataFieldSNFind.Data = "-";
                dataFieldSNTrack.Data = "-";
                dataFieldNasnimano.Data = "-";
                dataFieldREZ1.Data = "-";
                dataFieldREZ2.Data = "-";
                dataFieldITEMCODE.Data = "-";
                dataFieldREZ1.Text = "REZ_1";
                dataFieldREZ1.Text = "REZ_2";
            }
            finally
            {
            }

            sbInfo.Text = "Z:" + _currentItem + " z " + _itemsCount;
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
                ScannerStop();

                string ck = e.BarcodeData.Trim();
				Fask.Parsing.Codes.BaseCode code = null;
				if (MST_Global.Inventura1ParsovaniCarovehoKoduPovolit)
				{
					code = Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);

					// TODO : ? and GS1.Multiscan.Enabled ? 
					if (
						(code is Parsing.Codes.GS1) 
						|| (code is Parsing.Codes.SAB_GS1_Zavorky) 
						|| (code is Parsing.Codes.SAB_GS1_BALTON)
						|| (code is Parsing.Codes.SAB_GS1_BELDICO)
						)

					{
						try
						{
							this.ScannerStop();
							using (var mbscan = new Forms.MultiBarcode_Scan())
							{
								mbscan.ParseBarcode(ck);
								if (mbscan.ShowDialog() == DialogResult.Cancel)
									return;
								code = mbscan.Kod;
							}
						}
						finally
						{
							this.ScannerStart();
						}
					}


					if (code is Parsing.Codes.Interfaces.ICodeItemnmbr)
						ck = ((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr;
					else if (code is Parsing.Codes.Interfaces.ICodeBarcode)
						ck = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode;
					else if (code is Parsing.Codes.Interfaces.ICodeSerialNumber)
						ck = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
					else if (code is Parsing.Codes.Interfaces.ICodeSarze)
						ck = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
				}

				////Fask.Parsing.Codes.WeightCode wcode = Parsing.ParsingFactory.Parse(ck) as Fask.Parsing.Codes.WeightCode;
				//var code = Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);
				//if (code is Parsing.Codes.Interfaces.ICodeItemnmbr)
				//{ // jedna se o vahovy kod...
				//    ck = ((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr ?? string.Empty;
				//}


				//if (ck.Length > 0)
				//{
				//    ListPolozky.PolozkyRow prow = null;
				//    Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row = null;
				//    Fask.Parsing.Codes.BaseCode bc = null;
				//    if (NajdiPolozku(ck, out prow, out i3row, out bc))
				//    {
				//        _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
				//        VyplnPolozku(prow, i3row, bc);
				//    }
				//    else
				//        return;
				//}

				if (ck.Length > 0)
				{
					ListPolozky.PolozkyRow prow = null;
					Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row = null;
					if (NajdiPolozku(ck, out prow, out i3row))
					{
						_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
						VyplnPolozku(prow, i3row, code);
					}
					else
						return;
				}
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                ScannerStart();

                if (MST_Global.OnScannerSound_Inventura1_sqlc)
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
		///// <summary>
		///// Najde polozku a oznaci ji jako aktivni v datagridu
		///// </summary>
		///// <param name="carkod">carovy kod polozky</param>
		///// <param name="lastindex">posledni nalezeny index</param>
		///// <returns>index nalezene polozky, vetsi nez posledni nalezeny index</returns>
		//private bool NajdiPolozku(
		//    string carovykod,
		//    out ListPolozky.PolozkyRow prow,
		//    out Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row,
		//    out Fask.Parsing.Codes.BaseCode bss
		//    )
		//{
		//    prow = null;
		//    i3row = null;


		//    string carkod = carovykod;
		//    bss = Fask.Parsing.ParsingFactory.Parse(carovykod, Settings.Parsing_Config);

		//    try
		//    {
		//        Cursor.Current = Cursors.WaitCursor;
		//        ScannerStop();

		//        //1) najit polozky
		//        _inventura1.CZMST_I3.Clear();
		//        if ((bss is Fask.Parsing.Codes.Interfaces.ICodeBarcode))
		//        {
		//            Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByCarcode_I3(_inventura1.CZMST_I3, ((Fask.Parsing.Codes.Interfaces.ICodeBarcode)bss).Barcode); //Dotaz sestaven pomoci T-SQL UNION
		//        }
		//        else
		//        {
		//            Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByCarcode_I3(_inventura1.CZMST_I3, carkod); //Dotaz sestaven pomoci T-SQL UNION
		//        }

		//        if (_inventura1.CZMST_I3.Count == 0)
		//        { // polozky nenalezeny, hledam dle SN ...
		//            if ((bss is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr))
		//            {
		//                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillBySERLNMBR_I2(_inventura1.CZMST_I2, ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)bss).Serltnmbr);
		//            }
		//            else
		//            {
		//                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillBySERLNMBR_I2(_inventura1.CZMST_I2, carkod);
		//            }
                    
		//            //ita_i3.ClearBeforeFill = false;
		//            foreach (var item in _inventura1.CZMST_I2)
		//            {
		//                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByITEMNMBR_I3(_inventura1.CZMST_I3, item.ITEMNMBR);
		//            }
		//        }

		//        if (_inventura1.CZMST_I3.Rows.Count < 1) //nenalezeno
		//        {
		//            Cursor.Current = Cursors.Default;
		//            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaCarKodNenalezena, carkod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
		//            return false;
		//        }
		//        else if (_inventura1.CZMST_I3.Rows.Count > 1) //nalezeno vice zaznamu
		//        {
		//            var xi1dt = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I1(_inventura1.CZMST_I3[0].ITEMNMBR);
		//            Cursor.Current = Cursors.Default;
		//            using (ListPolozkyI3_sqlce lpi3 = new ListPolozkyI3_sqlce())
		//            {
		//                lpi3.I3DT = _inventura1.CZMST_I3;
		//                if (xi1dt.Count > 0) lpi3.FindMJInView(xi1dt[0].ITEMNMBR, xi1dt[0].DMJ);
		//                if (lpi3.ShowDialog() == DialogResult.Cancel)
		//                    return false;
		//                else
		//                    i3row = lpi3.I3Selected;
		//            }
		//        }
		//        else //je pouze jedna (0 byt uz nemuze)
		//        {
		//            i3row = _inventura1.CZMST_I3[0];
		//        }

		//        _currentItem = -1;
		//        CreateResultSet(_select_All, " where ITEMNMBR='" + i3row.ITEMNMBR + "'");

		//        //Dohledat v i1 vybrany zaznam
		//        prow = this.SelectedRow;

		//        if (prow == null) //neco se nepovedlo
		//        {
		//            Cursor.Current = Cursors.Default;
		//            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaNenalezena, i3row.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
		//            return false;
		//        }

		//        UpdateForm();

		//        if (_listPolozky.Polozky.Count > 1)
		//        {
		//            Cursor.Current = Cursors.Default;
		//            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNalezenoVicePolozekVyberDotaz, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//            return false;
		//        }

		//        return true;
		//    }
		//    catch (Exception ex)
		//    {
		//        MessageBox.Show(ex.Message);
		//        Logging.Log.Write(ex.Message, this.Text);
		//        return false;
		//    }
		//    finally
		//    {
		//        ScannerStart();
		//        Cursor.Current = Cursors.Default;
		//    }
		//}

        private void NajdiPolozkuPozice()
        {
            try
            {
                ScannerStop();

                int pozice = _currentItem;
                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Inventura1ListPolozkySqlcePoziceZaznamu, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, (pozice).ToString()))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    pozice = int.Parse(skf.Kod);
                    if (pozice < 0 || _itemsCount < pozice)
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePoziceMimoRozsah, _itemsCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

                string selectNazev = "select itemnmbr from czmst_i1 where itemdesc like '" + (Settings.Inventura1_HledatFulltext ? "%" : "") + nazev + "%'";

				founded = Fask.MST_W.Inventura1_sqlce.Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.dataExistsInI1(selectNazev);

                //Polozka nenalezena
                if (!founded)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaSNazvemNenalezena, nazev), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }

                //polozka nalezena, tak zobrazit stav
                _currentItem = -1;
                _firstItem = -1;
                CreateResultSet(_select_All, " where ITEMDESC like '" + (Settings.Inventura1_HledatFulltext ? "%" : "") + nazev + "%'");
                UpdateForm();
                return founded;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, this.Text);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
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
                using (Forms.SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Inventura1ListPolozkySqlceCarovyKod, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = Fask.Localization.Localization.Inventura1ListPolozkySqlceHledat;
                    skf.Owner = this;
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    ck = skf.Kod;
                }



                ListPolozky.PolozkyRow prow = null;
                Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row = null;
                NajdiPolozku(ck, out prow, out i3row);
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
                using (SejmiKodFormHledejNazev skf = new SejmiKodFormHledejNazev(Fask.Localization.Localization.Inventura1ListPolozkySqlceNazev, SejmiKodFormHledejNazev.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = Fask.Localization.Localization.Inventura1ListPolozkySqlceHledat;
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
        private void VyplnPolozku(ListPolozky.PolozkyRow prow)
        {

            try
            {
                if (prow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable i3dt = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I3(prow.ITEMNMBR);
                Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row = null;
                Cursor.Current = Cursors.Default;

                if (i3dt.Rows.Count < 1)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNenalezenZaznamVI3, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return;
                }
                else if (i3dt.Rows.Count > 1)
                {
                    using (ListPolozkyI3_sqlce lpi3 = new ListPolozkyI3_sqlce())
                    {
                        lpi3.I3DT = i3dt;
                        if (prow != null) lpi3.FindMJInView(prow.ITEMNMBR, prow.DMJ);
                        if (lpi3.ShowDialog() == DialogResult.Cancel)
                            return;
                        else
                            i3row = lpi3.I3Selected;
                    }
                }
                else
                {
                    i3row = i3dt[0]; //je tam jen jedna polozka a je na indexu 0
                }

                //jinak je to 1:1 a muzu to pustit dal

                VyplnPolozku(prow, i3row, null);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

     
		//private void VyplnPolozku(ListPolozky.PolozkyRow prow, Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row, Fask.Parsing.Codes.BaseCode bc)
		//{
		//    //Kontrola,zda je mozne zadavat i jinak nez scannerem
		//    if (!InputModeChecker.checkInputMode(MST_Global.Inventura1PolozkyVyberJenScannerem, _input_mode))
		//    {
		//        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//        return;
		//    }

		//    bool lokaceZadana = false;

		//    try
		//    {
		//        ScannerStop();

		//        decimal qty = 0;
		//        string sn = string.Empty;
		//        if (bc is Fask.Parsing.Codes.BarcodeSlashSarze)
		//            sn = ((Fask.Parsing.Codes.BarcodeSlashSarze)bc).sarze;

		//        //JiS: 15.4.2010 : Uprava pro FVK-Global
		//        //locncode pamatuje posledni zadany kod
		//        //pokud je u polozky locncode neni prazdny retezec nebo neni null, pak se aplikuje tento z polozky,
		//        //jinak se pouzije predchozi zadany
		//        // PeV: 25.1.2016 : Uprava pro Perlacasa, pridano do konfigurace aplikace moznost vypnuti predvyplneni posledni zvolene lokace
		//        // pokud je zapnuto, vyuzije se stara funkcionalita, pokud zapnuto, do locncode se ulozi/predvyplni lokace z nactene polozky
		//        if (MST_Global.inventura1ZadaniLocncodePamatovatPosledni)
		//        {
		//            string locncodeNew = prow.IsLOCNCODENull() ? string.Empty : prow.LOCNCODE.Trim();
		//            if (!String.IsNullOrEmpty(locncodeNew))
		//            {
		//                locncode = locncodeNew;
		//            }
		//        }
		//        else if (!MST_Global.inventura1ZadaniLocncodeJednou)
		//        {
		//            locncode = prow.IsLOCNCODENull() ? string.Empty : prow.LOCNCODE.Trim();
		//        }

		//        if (parrow != null && parrow.CFG_UpozornitNaPrebytek)
		//        {//cfg_upozornit na prebytek...
		//            //decimal nas = Convert.ToDecimal(ita_i4.Nasnimano(prow.ITEMNMBR) ?? 0);
		//            if (prow.NASNIMANO > prow.QUANTITY)
		//            {
		//                MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezNaSklade, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
		//            }
		//        }

		//        bool DalsiSN = true;
		//        while (DalsiSN)
		//        {
		//            if (MST_Global.Inventura1PolozkaNasnimatPouzeJednou)
		//            {
		//                int? pocetNasnimano = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.NasnimanoPocet_I4(prow.ITEMNMBR.Trim());
		//                if ((pocetNasnimano ?? 0 ) > 0)
		//                {
		//                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaJizBylaZadana, Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozeniPolozky, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
		//                    return;
		//                }
		//            }

		//            if (parrow != null && parrow.CFG_PovolitZmenuLokace)
		//            {
		//                if (MST_Global.inventura1ZadaniLocncodePredSN)
		//                {
		//                    bool zadat_lokaci = true;
		//                    if (MST_Global.inventura1ZadaniLocncodeJednou)
		//                        zadat_lokaci = !lokaceZadana;

		//                    if (zadat_lokaci)
		//                    {
		//                        using (SejmiKodFormHledejNazev skf = new SejmiKodFormHledejNazev(Fask.Localization.Localization.Inventura1ListPolozkySqlceLokace, SejmiKodFormHledejNazev.TypeOfCode.AlphaNumeric, 0, false, false, prow.LOCNCODE, _inventura1.CZMST_I4.Columns["LOCNCODE"].MaxLength))
		//                        {
		//                            skf.Kod = locncode;
		//                            if (skf.ShowDialog() == DialogResult.Cancel)
		//                                break;
		//                            locncode = skf.Kod;
		//                        }
		//                        lokaceZadana = true;
		//                    }
		//                }
		//            }

		//            if (prow.CZ_SERNUM_TRACK == 0) //sledovano na mnozstvi
		//            {
		//                NaplnPolozku_sqlce naplnp = this.NaplnPolozkuForm;
		//                bool baleni = i3row.QTYPACK > 0;
		//                naplnp.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
		//                naplnp.PROW = prow;
		//                naplnp.I3Row = i3row;
		//                naplnp.Owner = this;
		//                //naplnp.Popis = "Množství" + (baleni ? " balení" : string.Empty);
		//                naplnp.Popis = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstvi;
		//                naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
		//                naplnp.AllowEmpty = false;
		//                //naplnp.Text = "Vložte množství" + (baleni ? " balení" : string.Empty);
		//                naplnp.Text = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstvi;
		//                naplnp.Kod = string.Empty;
		//                naplnp.ScannerOff = false;
		//                if (parrow != null)
		//                {
		//                    if (!parrow.CFG_MnozstviScannerem)
		//                    {
		//                        naplnp.ScannerOff = true;
		//                    }
		//                    if (parrow.CFG_PredvyplnitMnozstvi)
		//                    {
		//                        if (parrow.CFG_PredvyplnitMnozstviOJedna)
		//                            naplnp.Kod = "1";
		//                        else if (parrow.CFG_PredvyplnitMnozstviZbyvajici)
		//                            naplnp.Kod = ((prow.QUANTITY - prow.NASNIMANO) / (i3row.QTYPACK > 0 ? i3row.QTYPACK : 1)).ToString(Settings.UIFormatDesCisel);
		//                    }
		//                }

		//                if (naplnp.ShowDialog() == DialogResult.Cancel)
		//                {
		//                    if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
		//                    {
		//                        if (prow.NASNIMANO < prow.QUANTITY)
		//                        {
		//                            if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniKompletniUkoncitDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                                continue;
		//                            else
		//                                break;
		//                        }
		//                    }
		//                    return;
		//                }

		//                if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
		//                {
		//                    if (prow.NASNIMANO + Convert.ToDecimal(naplnp.Kod) > prow.QUANTITY)
		//                    {
		//                        if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezZadanePokracovatDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                        {
		//                            continue;
		//                        }
		//                    }
		//                }

		//                if (parrow != null)
		//                {
		//                    if (!parrow.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
		//                    {
		//                        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytVetsiNez0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//                        continue;
		//                    }
		//                    else
		//                        qty = decimal.Parse(naplnp.Kod);
		//                }
		//                else
		//                {
		//                    if (Convert.ToDecimal(naplnp.Kod) == 0)
		//                    {
		//                        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytRuzneOd0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//                        continue;
		//                    }
		//                    else
		//                        qty = decimal.Parse(naplnp.Kod);
		//                }

		//            }
		//            else if (prow.CZ_SERNUM_TRACK == 1 || prow.CZ_SERNUM_TRACK == 2) //sledovano na seriova cisla
		//            {
		//                bool itemFoundedSN = false;
		//                do
		//                {
		//                    if ((prow.CZ_SERNUM_TRACK == 2) && (bc != null) && (bc is Fask.Parsing.Codes.BarcodeSlashSarze))
		//                    {
		//                        sn = ((Fask.Parsing.Codes.BarcodeSlashSarze)bc).sarze;
		//                    }
		//                    else
		//                    {
		//                        NaplnPolozkuSN naplnpsn = this.NaplnPolozkuSNForm;
		//                        naplnpsn.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
		//                        naplnpsn.PROW = prow;
		//                        naplnpsn.I3Row = i3row;
		//                        naplnpsn.Owner = this;
		//                        naplnpsn.Popis = MST_Global.SNName;
		//                        naplnpsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
		//                        naplnpsn.AllowEmpty = false;
		//                        //naplnpsn.Text = "Vložte " + MST_Global.SNName;
		//                        naplnpsn.Text = string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteSN, MST_Global.SNName);
		//                        naplnpsn.Kod = string.Empty;
		//                        naplnpsn.ScannerOff = false;
		//                        //naplnpsn.I2 = ita_i2.GetDataByITEMNMBR(i3row.ITEMNMBR);
		//                        //naplnpsn.I2 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CZMST_I2_GetDataByItemnmbr(i3row.ITEMNMBR);
		//                        naplnpsn.I2 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I2(i3row.ITEMNMBR);
		//                        if (naplnpsn.ShowDialog() == DialogResult.Cancel)
		//                            return;

		//                        sn = naplnpsn.Kod;
		//                    }
		//                    qty = 1;

		//                    if (parrow != null && !parrow.CFG_PovolitDuplicituSN)
		//                    {
		//                        bool itemFounded = false;


		//                        itemFounded = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.dataExistsInI4_ByITEMNMBR_SERLNMBR(prow.ITEMNMBR, sn);

		//                        if (itemFounded)
		//                        {
		//                            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Inventura1ListPolozkySqlceChyba, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
		//                            continue;
		//                        }
		//                    }

		//                    itemFoundedSN = false;
		//                    if (prow.CZ_SERNUM_FIND > 0)
		//                    {
		//                        itemFoundedSN = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.dataExistsInI2_ByITEMNMBR_SERLNMBR(prow.ITEMNMBR, sn);
		//                        if (itemFoundedSN)
		//                            break;
		//                        else
		//                        {
		//                            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceSNNenalezenoOpokovat, MST_Global.SNName, sn), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
		//                            continue;
		//                        }
		//                    }

		//                    break;

		//                } while (true);

		//                if (prow.CZ_SERNUM_TRACK == 2)//sledovano na sarzi a mnozstvi
		//                {
		//                    if ((bc != null) && (bc is Parsing.Codes.WeightCode))
		//                    { // pokud to je vahovy kod, tak se vlozi mn ...
		//                        Parsing.Codes.WeightCode wc = (Parsing.Codes.WeightCode)bc;
		//                        qty =
		//                            (wc.weight
		//                            / (i3row.IsWEIGHTNull() || (i3row.WEIGHT == 0) ? 1 : i3row.WEIGHT)
		//                            / (i3row.IsQTYPACKNull() || (i3row.QTYPACK == 0) ? 1 : i3row.QTYPACK)
		//                            );
		//                    }
		//                    else
		//                    {
		//                        #region naplneni mnozstvi

		//                        NaplnPolozku_sqlce naplnp = this.NaplnPolozkuForm;
		//                        bool baleni = i3row.QTYPACK > 0;
		//                        naplnp.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
		//                        naplnp.PROW = prow;
		//                        naplnp.I3Row = i3row;
		//                        naplnp.Owner = this;
		//                        //naplnp.Popis = "Množství" + (baleni ? " balení" : string.Empty);
		//                        naplnp.Popis = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstvi;
		//                        naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
		//                        naplnp.AllowEmpty = false;
		//                        //naplnp.Text = "Vložte množství" + (baleni ? " balení" : string.Empty);
		//                        naplnp.Text = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstvi;

		//                        naplnp.Kod = string.Empty;
		//                        // TODO : jak to udelat s mnozstvim SN?
		//                        //if (itemFoundedSN)
		//                        //{
		//                        //    decimal jednotky = (decimal)polozkyReaderSN["QTY"];
		//                        //    naplnp.Kod = baleni ?  .ToString(Settings.UIFormatDesCisel);
		//                        //}

		//                        naplnp.ScannerOff = false;
		//                        if (parrow != null)
		//                        {
		//                            if (!parrow.CFG_MnozstviScannerem)
		//                            {
		//                                naplnp.ScannerOff = true;
		//                            }
		//                            if (parrow.CFG_PredvyplnitMnozstvi)
		//                            {
		//                                if (parrow.CFG_PredvyplnitMnozstviOJedna)
		//                                    naplnp.Kod = "1";
		//                                else if (parrow.CFG_PredvyplnitMnozstviZbyvajici)
		//                                    //naplnp.Kod = (prow.QUANTITY - prow.NASNIMANO).ToString(Settings.UIFormatDesCisel);
		//                                    naplnp.Kod = ((prow.QUANTITY - prow.NASNIMANO) / (i3row.QTYPACK > 0 ? i3row.QTYPACK : 1)).ToString(Settings.UIFormatDesCisel);
		//                            }
		//                        }

		//                        if (naplnp.ShowDialog() == DialogResult.Cancel)
		//                        {
		//                            if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
		//                            {
		//                                if (prow.NASNIMANO < prow.QUANTITY)
		//                                {
		//                                    if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniKompletniUkoncitDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                                    {
		//                                        continue;
		//                                    }
		//                                    else
		//                                    {
		//                                        break;
		//                                    }
		//                                }
		//                            }

		//                            return;
		//                        }

		//                        if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
		//                        {
		//                            if (prow.NASNIMANO + Convert.ToDecimal(naplnp.Kod) > prow.QUANTITY)
		//                            {
		//                                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezZadanePokracovatDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                                {
		//                                    continue;
		//                                }
		//                            }
		//                        }

		//                        if (parrow != null)
		//                        {
		//                            if (!parrow.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
		//                            {
		//                                MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytVetsiNez0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//                                continue;
		//                            }
		//                            else
		//                                qty = decimal.Parse(naplnp.Kod);
		//                        }
		//                        else
		//                        {
		//                            if (Convert.ToDecimal(naplnp.Kod) == 0)
		//                            {
		//                                MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytRuzneOd0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		//                                continue;
		//                            }
		//                            else
		//                                qty = decimal.Parse(naplnp.Kod);
		//                        }
		//                        #endregion
		//                    }
		//                }
		//            }
		//            else
		//            {
		//                MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceSerNumTrackNeniPodporovan, prow.CZ_SERNUM_TRACK), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
		//                return;
		//            }

		//            decimal mnozstviMJ = qty;
		//            decimal mnozstvi = i3row.QTYPACK > 0 ? qty * i3row.QTYPACK : qty;

		//            if (parrow != null && parrow.CFG_PovolitZmenuLokace)
		//            {
		//                if (!MST_Global.inventura1ZadaniLocncodePredSN)
		//                {
		//                    bool zadat_lokaci = true;
		//                    if (MST_Global.inventura1ZadaniLocncodeJednou)
		//                        zadat_lokaci = !lokaceZadana;

		//                    if (zadat_lokaci)
		//                    {
		//                        using (SejmiKodFormHledejNazev skf = new SejmiKodFormHledejNazev(Fask.Localization.Localization.Inventura1ListPolozkySqlceLokace, SejmiKodFormHledejNazev.TypeOfCode.AlphaNumeric, 0, false, false, prow.LOCNCODE, _inventura1.CZMST_I4.Columns["LOCNCODE"].MaxLength))
		//                        {
		//                            skf.Kod = locncode;
		//                            if (skf.ShowDialog() == DialogResult.Cancel)
		//                                break;
		//                            locncode = skf.Kod;
		//                        }
		//                        lokaceZadana = true;
		//                    }
		//                }
		//            }

		//            string rez_1 = prow.REZ1.Trim();
		//            if (prow.CZ_REZ_1_TRACK > 0)
		//            { // TODO : zadani hodnoty rez1 ...
		//                using (SejmiKodForm skf = new SejmiKodForm("Dop. informace 1", MST_Global.Inventura1REZ1Nazev, MST_Global.Inventura1REZ1IsNumber ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, !MST_Global.Inventura1REZ1Mandatory, prow.REZ1, 0))
		//                {
		//                    if (skf.ShowDialog() == DialogResult.Cancel)
		//                        return;
		//                    else
		//                        rez_1 = skf.Kod.Trim();
		//                }
		//            }

		//            string rez_2 = prow.REZ2.Trim();
		//            if (prow.CZ_REZ_2_TRACK > 0)
		//            { // TODO : zadani hodnotay rez2
		//                using (SejmiKodForm skf = new SejmiKodForm("Dop. informace 2", MST_Global.Inventura1REZ2Nazev, MST_Global.Inventura1REZ2IsNumber ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, !MST_Global.Inventura1REZ2Mandatory, prow.REZ2, 0))
		//                {
		//                    if (skf.ShowDialog() == DialogResult.Cancel)
		//                        return;
		//                    else
		//                        rez_2 = skf.Kod.Trim();
		//                }
		//            }

		//            #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky
                    
		//            bool o_checked = false;
		//            if (!StaticMethods.OnlineCheck(Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value , prow.ITEMNMBR, ref o_checked))
		//                return;

		//            try
		//            {
		//                Cursor.Current = Cursors.WaitCursor;

		//                var dti4 = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable();

		//                var i4n = dti4.NewCZMST_I4Row();


		//                i4n.CountEntries = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value;
		//                i4n.CZ_CarKod = i3row.IsCZ_CarKodNull() ? string.Empty : i3row.CZ_CarKod;
		//                i4n.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
		//                i4n.DEX_ROW_ID = i3row.IsDEX_ROW_IDNull() ? -1 : i3row.DEX_ROW_ID;
		//                i4n.GUID = Guid.NewGuid();
		//                i4n.ID_TERMINAL = MST_Global.TerminalID;
		//                i4n.INPUT_MODE = _input_mode;
		//                i4n.ITEMCODE = prow.IsITEMCODENull() ? string.Empty : prow.ITEMCODE.Trim();
		//                i4n.ITEMNMBR = prow.IsITEMNMBRNull() ? string.Empty : prow.ITEMNMBR.Trim();
		//                i4n.LOCNCODE = String.IsNullOrEmpty(locncode) ? string.Empty : locncode.Trim();
		//                i4n.MJ = i3row.IsMJNull() ? string.Empty : i3row.MJ.Trim();
		//                i4n.O_Checked = o_checked;
		//                i4n.QTYPACK = i3row.IsQTYPACKNull() ? 0 : i3row.QTYPACK;
		//                i4n.QUANTITY = mnozstvi;
		//                i4n.QUANTITYMJ = mnozstviMJ;
		//                i4n.REZ_1 = String.IsNullOrEmpty(rez_1) ? string.Empty : rez_1.Trim();
		//                i4n.REZ_2 = String.IsNullOrEmpty(rez_2) ? string.Empty : rez_2.Trim();
		//                i4n.SERLNMBR = String.IsNullOrEmpty(sn) ? string.Empty : sn.Trim();
		//                i4n.skl_id = prow.IsSkladIDNull() ? string.Empty : prow.SkladID.Trim();
		//                i4n.TIMEDONE = DateTime.Now.ToString("HHmmss");
		//                i4n.USERID = MST_Global.UserID;
		//                i4n.VNDITNUM = i3row.IsVNDITNUMNull() ? string.Empty : i3row.VNDITNUM.Trim();
		//                if (!i3row.IsWEIGHTNull()) i4n.WEIGHT = i3row.WEIGHT;

		//                dti4.AddCZMST_I4Row(i4n);

		//                System.Diagnostics.Debug.Assert(i4n.RowState == DataRowState.Added);
		//                //Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CZMST_I4_Update(i4n);
		//                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.Update_I4(i4n);

		//                prow.NASNIMANO += mnozstvi;
		//            }
		//            finally
		//            {
		//                Cursor.Current = Cursors.Default;
		//            }
		//            #endregion


		//            DalsiSN = parrow != null && !parrow.CFG_PoZadaniSNZpetNaMN;

		//            #region Kontrola uplnosti polozky
		//            if (DalsiSN && (parrow != null && parrow.CFG_KontrolaUplnostiPolozky))
		//            {
		//                if (prow.NASNIMANO >= prow.QUANTITY)
		//                {
		//                    //if (MessageBoxBig.Show("Položka je kompletní, chcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                    if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaDlePredlohyKompletniPreplnitDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
		//                    {
		//                        break;
		//                    }
		//                }
		//            }
		//            #endregion
		//        }

		//        #region Kontrola uplnosti davky
		//        if (parrow != null && parrow.CFG_KontrolaUplnosti)
		//        {
		//            bool davkaUplna = false;
		//            try
		//            {
		//                Cursor.Current = Cursors.WaitCursor;

		//                // TODO : kontrola uplnosti na velkem mnozstvi dat selhava (viz. Inventura1_sqlce.stavInventury())
		//                //davkaUplna = kontrolaUplnostiDavky();
		//            }
		//            finally { Cursor.Current = Cursors.Default; }

		//            if (davkaUplna)
		//            {
		//                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceDavkaKompletniPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
		//                {
		//                    this.PerformKonec(false);
		//                }
		//            }
		//        }
		//        #endregion

		//    }
		//    catch (Exception ex)
		//    {
		//        MessageBoxBig.Show(ex.Message);
		//    }
		//    finally
		//    {
		//        ScannerStart();
		//    }

		//    UpdateForm();

		//}

        private void SmazPolozku(ListPolozky.PolozkyRow polozkyRow)
        {
            try
            {
                if (polozkyRow == null)
                    return;

                if (MessageBoxBig.Show(string.Format(Localization.Localization.Inventura1ListPolozkySqlceSmazatNasnimanaDataDotaz, polozkyRow.ITEMDESC), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No
                    )
                    return;

                bool o_unchecked = false;
				if (!StaticMethods.OnlineUnCheck(Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value, polozkyRow.ITEMNMBR, ref o_unchecked))
                    return;

                int raff = 0;
                if (String.IsNullOrEmpty(polozkyRow.SkladID))
                    raff = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.DeleteByITEMNMBR_I4(polozkyRow.ITEMNMBR.Trim());
                else
                    raff = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.DeleteBy_ITEMNMBR_SKL_ID_I4(polozkyRow.ITEMNMBR.Trim(), polozkyRow.SkladID.Trim());

                polozkyRow.NASNIMANO = 0;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
        #endregion

        #region Kontroly
        private bool kontrolaUplnostiDavky()
        {

			int? zbyva = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.ZbyvaPolozek_queries();
            return (zbyva ?? 0) == 0;
        }
        #endregion

        #region Udalosti uzivatelskeho vstupu
        private void ListPolozky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == MST_Global.DataGridScrollDown)
            {
				
				if (dataGrid.CurrentRowIndex == _listPolozky.Polozky.Rows.Count - 1 && (_firstItem + _listPolozky.Polozky.Count) < _itemsCount)
				{
					MoveStepDown();
					e.Handled = true;
					return;
				}
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == MST_Global.DataGridScrollUp)
            {
                if (dataGrid.CurrentRowIndex == 0 && _currentItem > 1)
                {
					MoveStepUp();
                    e.Handled = true;
                    dataGrid.CurrentRowIndex = _listPolozky.Polozky.Rows.Count - 1; //na posledni index
                    return;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec(true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
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
                vyhledejPolozkuKod();
            }
            else if (e.KeyCode == Keys.F5 && Settings.Online_BYZNYS)
            {
                NovyEAN();
            }
			else if (e.KeyCode == Keys.F6)
			{
				menuItemTisk_Click(null,null);
			}
            else
                return;

            e.Handled = true;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
			if (e.Button == toolBarButtonPrev)
				MovePrev();
			else if (e.Button == toolBarButtonNext)
				MoveNext();
			else if (e.Button == toolBarButtonFirst)
				MoveFirst();
			else if (e.Button == toolBarButtonLast)
				MoveLast();
        }

		#region Posuvy
		private void MoveFirst()
		{
			_currentItem = -1;
			CreateResultSet(_select_current, _select_current_wherecondition);
		}

		private void MoveLast()
		{
			_currentItem = _itemsCount - _pocetZobrazit + 1;
			CreateResultSet(_select_current, _select_current_wherecondition);
		}

		private void MovePrev()
		{
			_currentItem -= _pocetZobrazit;
			CreateResultSet(_select_current, _select_current_wherecondition);
			dataGrid.CurrentRowIndex = _listPolozky.Polozky.Rows.Count - 1; //na posledni index
		}

		private void MoveNext()
		{
			_currentItem = _firstItem + _pocetZobrazit;
			// _currentItem += _pocetZobrazit;
			CreateResultSet(_select_current, _select_current_wherecondition);
		}

		private void MoveStepDown()
		{
			_currentItem++;
			CreateResultSet(_select_current, _select_current_wherecondition);
		}

		private void MoveStepUp()
		{
			_currentItem -= _pocetZobrazit;
			CreateResultSet(_select_current, _select_current_wherecondition);
		}
		
		#endregion


        private void ListPolozky_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            ScannerFinalize();
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformKonec(true);
        }

        private void buttonZadat_Click(object sender, EventArgs e)
        {
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
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
            CreateResultSet(_select_All, string.Empty);
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
				using (Nasnimane2 naspol = new Nasnimane2())
                {
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

        private void menuItemOnlineNovyEAN_Click(object sender, EventArgs e)
        {
            NovyEAN();
        }

        private void NovyEAN()
        {
            try
            {
                this.ScannerStop();

                ListPolozky.PolozkyRow pol = this.SelectedRow;
                if (pol == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                Online.BYZNYS.Algorithms.InsertNewEAN(Convert.ToInt32(pol.ITEMNMBR), pol.ITEMDESC.Trim());
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            vyhledejPolozkuKod();
        }

        private void vyhledejPolozkuKod()
        {
            try
            {
                ScannerStop();

                string ck = string.Empty;
                using (Forms.SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Inventura1ListPolozkySqlceKodPolozky, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    skf.Text = Fask.Localization.Localization.Inventura1ListPolozkySqlceHledat;
                    skf.Owner = this;
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    ck = skf.Kod;
                }

                ListPolozky.PolozkyRow prow = null;
                NajdiPolozkuItemcode(ck, out prow);
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

        private bool NajdiPolozkuItemcode(string itemcode, out ListPolozky.PolozkyRow prow)
        {
            prow = null;

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                ScannerStop();

                Fask.SQLiteDBs.DataSets.Inventura1 inv = new Fask.SQLiteDBs.DataSets.Inventura1();
                Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1Row i1row = null;

                //ita_i1.FillByItemcode(inv.CZMST_I1, itemcode); //Dotaz sestaven pomoci T-SQL UNION
                //Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CZMST_I1_FillByItemcode(inv.CZMST_I1, itemcode);
                Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByITEMCODE_I1(inv.CZMST_I1, itemcode);
                if (inv.CZMST_I1.Rows.Count < 1) //nenalezeno
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaSKodemNenalezena, itemcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }
                else if (inv.CZMST_I1.Rows.Count > 1) //nalezeno vice zaznamu
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceNalezenoViceZaznamuVyber, itemcode), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }
                else //je pouze jedna (0 byt uz nemuze)
                {
                    i1row = inv.CZMST_I1[0];
                }

                _currentItem = -1;
                CreateResultSet(_select_All, " where ITEMNMBR='" + i1row.ITEMNMBR + "'");

                //Dohledat v i1 vybrany zaznam
                prow = this.SelectedRow;

                if (prow == null) //neco se nepovedlo
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaNenalezena, i1row.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return false;
                }

                UpdateForm();

                if (_listPolozky.Polozky.Count > 1)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNalezenoVicePolozekVyberDotaz, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return false;
                }

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

        private void menuItemTisk_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();
                ListPolozky.PolozkyRow i1 = this.SelectedRow;
                if (i1 == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniVybranZaznam, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                bool vytisteno = InventuraTisk.Print(i1, PrinterFactory.PrinterModules.InventuraPredloha);
                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "i", null, null, i1.ITEMNMBR.Trim(), vytisteno.ToString(), null));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void ListPolozky_sqlce_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ListPolozky_sqlce_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }
    }
}