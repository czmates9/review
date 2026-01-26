using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;
using System.IO;

namespace Fask.MST_W.Inventura1_sqlce
{
    public partial class Nasnimane2 : System.Windows.Forms.Form
    {

        //defaultni select a parametry        
        private const string _select_ = "select * from czmst_i4 ";
        private const string _select_count = "select count(*) from czmst_i4 ";
        private string _select_current = string.Empty;

        //Slouzi pro odlozeny update selectu pri nacitani konfigurace
        private bool select_current_update = true;
        private void Select_Current_Update()
        {
            if (!select_current_update)
                return;

            string select = _select_;
            select += " order by ";
            if (RazeniHlavni != RazeniType.None)
            {
                switch (RazeniHlavni)
                {
                    case RazeniType.None:
                        break;
                    case RazeniType.Polozka:
                        select += "ITEMNMBR ";
                        break;
                    case RazeniType.SN:
                        select += "SERLNMBR ";
                        break;
                    case RazeniType.Lokace:
                        select += "LOCNCODE ";
                        break;
                    case RazeniType.Uzivatel:
                        select += "USERID ";
                        break;
                    default:
                        break;
                }
                switch (SetrizeniHlavni)
                {
                    case SetrizeniType.DESC:
                        select += " DESC";
                        break;
                    case SetrizeniType.ASC:
                    default:
                        select += " ASC";
                        break;
                }

                select += ", ";
            }

            if (RazeniVedlejsi != RazeniType.None)
            {
                switch (RazeniVedlejsi)
                {
                    case RazeniType.None:
                        break;
                    case RazeniType.Polozka:
                        select += "ITEMNMBR ";
                        break;
                    case RazeniType.SN:
                        select += "SERLNMBR ";
                        break;
                    case RazeniType.Lokace:
                        select += "LOCNCODE ";
                        break;
                    case RazeniType.Uzivatel:
                        select += "USERID ";
                        break;
                    default:
                        break;
                }
                switch (SetrizeniVedlejsi)
                {
                    case SetrizeniType.DESC:
                        select += " DESC";
                        break;
                    case SetrizeniType.ASC:
                    default:
                        select += " ASC";
                        break;
                }

                select += ", ";
            }

            select += " datedone, timedone";

            _select_current = select;

            SQLGetData();
        }
        
		int _itemsCount = -1;   //Pocet polozek ve vysledku dotazu na I1
		int _currentItem = -1;  //Aktualni poradove cislo v datagridu <0 = inicializovano
		int _firstItem = -1;    //Poradi ve vysledku dotazu prvni polozky zobrazene v datagridu
		int _pocetZobrazit = 10;//pocet polozek, ktere se budou nacitat z resultsetu

        //Aktualne nactene polozky z databaze
        private ListPolozkyNasnimane _list_polozky_nasnimane = new ListPolozkyNasnimane();
        
        private DataGridTableStyle _dg_style = null;
        private DataGridTextBoxColumn _dg_01 = null;
        private DataGridTextBoxColumn _dg_02 = null;
        private DataGridTextBoxColumn _dg_03 = null;
        private DataGridTextBoxColumn _dg_04 = null;
        private DataGridTextBoxColumn _dg_05 = null;
        private DataGridTextBoxColumn _dg_06 = null;
        private DataGridTextBoxColumn _dg_07 = null;
        private DataGridTextBoxColumn _dg_08 = null;
        private DataGridTextBoxColumn _dg_09 = null;
        private DataGridTextBoxColumn _dg_10 = null;
        private DataGridTextBoxColumn _dg_11 = null;
        private DataGridTextBoxColumn _dg_12 = null;
        private DataGridTextBoxColumn _dg_13 = null;
        private DataGridTextBoxColumn _dg_14 = null;
        private DataGridTextBoxColumn _dg_15 = null;
        private DataGridTextBoxColumn _dg_16 = null;
        private DataGridTextBoxColumn _dg_17 = null;
        private DataGridTextBoxColumn _dg_18 = null;
        private DataGridTextBoxColumn _dg_19 = null;
        
        private void GridStylesCreate()
        {
            try
            {
                List<DataGridColumnStyle> _dg_list = new List<DataGridColumnStyle>();

                _dg_style = new DataGridTableStyle();
                _dg_style.MappingName = _list_polozky_nasnimane.Nasnimane.TableName;

                _dg_01 = new DataGridTextBoxColumn();
                _dg_01.MappingName = _list_polozky_nasnimane.Nasnimane.CountEntriesColumn.ColumnName;
                _dg_01.HeaderText = "Dávka";
                _dg_01.NullText = "-";
                _dg_01.Width = Settings.Inventura1NasnimaneCOUNTENTRIESWidth;
                //_dg_style.GridColumnStyles.Add(_dg_01);
                _dg_list.Add(_dg_01);

                _dg_02 = new DataGridTextBoxColumn();
                _dg_02.MappingName = _list_polozky_nasnimane.Nasnimane.ITEMNMBRColumn.ColumnName;
                _dg_02.HeaderText = "Č. pol.";
                _dg_02.NullText = "-";
                _dg_02.Width = Settings.Inventura1NasnimaneITEMNMBRWidth;
                //_dg_style.GridColumnStyles.Add(_dg_02);
                _dg_list.Add(_dg_02);

                _dg_03 = new DataGridTextBoxColumn();
                _dg_03.MappingName = _list_polozky_nasnimane.Nasnimane.CZ_CarKodColumn.ColumnName;
                _dg_03.HeaderText = "Č.k. vlastní";
                _dg_03.NullText = "-";
                _dg_03.Width = Settings.Inventura1NasnimaneCZCARKODWidth;
                //_dg_style.GridColumnStyles.Add(_dg_03);
                _dg_list.Add(_dg_03);

                _dg_04 = new DataGridTextBoxColumn();
                _dg_04.MappingName = _list_polozky_nasnimane.Nasnimane.LOCNCODEColumn.ColumnName;
                _dg_04.HeaderText = "Lokace";
                _dg_04.NullText = "-";
                _dg_04.Width = Settings.Inventura1NasnimaneLOCNCODEWidth;
                //_dg_style.GridColumnStyles.Add(_dg_04);
                _dg_list.Add(_dg_04);

                _dg_05 = new DataGridTextBoxColumn();
                _dg_05.MappingName = _list_polozky_nasnimane.Nasnimane.VNDITNUMColumn.ColumnName;
                _dg_05.HeaderText = "Č.k.";
                _dg_05.NullText = "-";
                _dg_05.Width = Settings.Inventura1NasnimaneVNDITNUMWidth;
                //_dg_style.GridColumnStyles.Add(_dg_05);
                _dg_list.Add(_dg_05);

                _dg_06 = new DataGridTextBoxColumn();
                _dg_06.MappingName = _list_polozky_nasnimane.Nasnimane.QUANTITYColumn.ColumnName;
                _dg_06.HeaderText = "Množství";
                _dg_06.NullText = "-";
                _dg_06.Width = Settings.Inventura1NasnimaneQUANTITYWidth;
                _dg_06.Format = "0.##";
                //_dg_style.GridColumnStyles.Add(_dg_06);
                _dg_list.Add(_dg_06);

                _dg_07 = new DataGridTextBoxColumn();
                _dg_07.MappingName = _list_polozky_nasnimane.Nasnimane.QTYPACKColumn.ColumnName;
                _dg_07.HeaderText = "Balení";
                _dg_07.NullText = "-";
                _dg_07.Width = Settings.Inventura1NasnimaneQTYPACKWidth;
                _dg_07.Format = "0.##";
                //_dg_style.GridColumnStyles.Add(_dg_07);
                _dg_list.Add(_dg_07);

                _dg_08 = new DataGridTextBoxColumn();
                _dg_08.MappingName = _list_polozky_nasnimane.Nasnimane.SERLNMBRColumn.ColumnName;
                _dg_08.HeaderText = "SN";
                _dg_08.NullText = "-";
                _dg_08.Width = Settings.Inventura1NasnimaneSERLNMBRWidth;
                //_dg_style.GridColumnStyles.Add(_dg_08);
                _dg_list.Add(_dg_08);

                _dg_09 = new DataGridTextBoxColumn();
                _dg_09.MappingName = _list_polozky_nasnimane.Nasnimane.DATEDONEColumn.ColumnName;
                _dg_09.HeaderText = "Datum";
                _dg_09.NullText = "-";
                _dg_09.Width = Settings.Inventura1NasnimaneDATEDONEWidth;
                //_dg_style.GridColumnStyles.Add(_dg_09);
                _dg_list.Add(_dg_09);


                _dg_10 = new DataGridTextBoxColumn();
                _dg_10.MappingName = _list_polozky_nasnimane.Nasnimane.TIMEDONEColumn.ColumnName;
                _dg_10.HeaderText = "Čas";
                _dg_10.NullText = "-";
                _dg_10.Width = Settings.Inventura1NasnimaneTIMEDONEWidth;
                //_dg_style.GridColumnStyles.Add(_dg_10);
                _dg_list.Add(_dg_10);

                _dg_11 = new DataGridTextBoxColumn();
                _dg_11.MappingName = _list_polozky_nasnimane.Nasnimane.USERIDColumn.ColumnName;
                _dg_11.HeaderText = "Uživatel";
                _dg_11.NullText = "-";
                _dg_11.Width = Settings.Inventura1NasnimaneUSERIDWidth;
                //_dg_style.GridColumnStyles.Add(_dg_11);
                _dg_list.Add(_dg_11);

                _dg_12 = new DataGridTextBoxColumn();
                _dg_12.MappingName = _list_polozky_nasnimane.Nasnimane.DEX_ROW_IDColumn.ColumnName;
                _dg_12.HeaderText = "DEXROWID";
                _dg_12.NullText = "-";
                _dg_12.Width = Settings.Inventura1NasnimaneDEXROWIDWidth;
                //_dg_style.GridColumnStyles.Add(_dg_12);
                _dg_list.Add(_dg_12);


                _dg_13 = new DataGridTextBoxColumn();
                _dg_13.MappingName = _list_polozky_nasnimane.Nasnimane.GUIDColumn.ColumnName;
                _dg_13.HeaderText = "GUID";
                _dg_13.NullText = "-";
                _dg_13.Width = Settings.Inventura1NasnimaneGUIDWidth;
                //_dg_style.GridColumnStyles.Add(_dg_13);
                _dg_list.Add(_dg_13);

                _dg_14 = new DataGridTextBoxColumn();
                _dg_14.MappingName = _list_polozky_nasnimane.Nasnimane.ITEMDESCColumn.ColumnName;
                _dg_14.HeaderText = "Název";
                _dg_14.NullText = "-";
                _dg_14.Width = Settings.Inventura1NasnimaneITEMDESCWidth;
                //_dg_style.GridColumnStyles.Add(_dg_14);
                _dg_list.Add(_dg_14);

                _dg_15 = new DataGridTextBoxColumn();
                _dg_15.MappingName = _list_polozky_nasnimane.Nasnimane.skl_idColumn.ColumnName;
                _dg_15.HeaderText = "ID Sklad";
                _dg_15.NullText = "-";
                _dg_15.Width = Settings.Inventura1NasnimaneSkladIDWidth;
                //_dg_style.GridColumnStyles.Add(_dg_15);
                _dg_list.Add(_dg_15);

                _dg_16 = new DataGridTextBoxColumn();
                _dg_16.MappingName = _list_polozky_nasnimane.Nasnimane.skl_descColumn.ColumnName;
                _dg_16.HeaderText = "Sklad";
                _dg_16.NullText = "-";
                _dg_16.Width = Settings.Inventura1NasnimaneSkladDescWidth;
                //_dg_style.GridColumnStyles.Add(_dg_16);
                _dg_list.Add(_dg_16);

                _dg_17 = new DataGridTextBoxColumn();
                _dg_17.MappingName = _list_polozky_nasnimane.Nasnimane.QUANTITYMJColumn.ColumnName;
                _dg_17.HeaderText = "Množství MJ";
                _dg_17.NullText = "-";
                _dg_17.Width = Settings.Inventura1NasnimaneQUANTITYMJWidth;
                _dg_17.Format = "0.##";
                //_dg_style.GridColumnStyles.Add(_dg_17);
                _dg_list.Add(_dg_17);


                _dg_18 = new DataGridTextBoxColumn();
                _dg_18.MappingName = _list_polozky_nasnimane.Nasnimane.MJColumn.ColumnName;
                _dg_18.HeaderText = "MJ";
                _dg_18.NullText = "-";
                _dg_18.Width = Settings.Inventura1NasnimaneMJWidth;
                //_dg_style.GridColumnStyles.Add(_dg_18);
                _dg_list.Add(_dg_18);

                _dg_19 = new DataGridTextBoxColumn();
                _dg_19.MappingName = _list_polozky_nasnimane.Nasnimane.ExpiraceColumn.ColumnName;
                _dg_19.HeaderText = "Expirace";
                _dg_19.NullText = "-";
                _dg_19.Width = Settings.Inventura1NasnimaneExpiraceWidth;
                //_dg_style.GridColumnStyles.Add(_dg_19);
                _dg_19.Format = "d";    // short date pattern for datetime
                _dg_list.Add(_dg_19);

                //string[] poradi = Settings.PoradiSloupcuInventura1Nasnimane.Split(';');
                List<string> poradilist = new List<string>();
                poradilist.AddRange(Settings.PoradiSloupcuInventura1Nasnimane.Split(';'));
                
                _dg_list.Sort(new Classes.DataGridColumnStyleComparer(poradilist));

                foreach (DataGridTextBoxColumn dg_col in _dg_list)
                {
                    _dg_style.GridColumnStyles.Add(dg_col);
                }

                dataGridList.TableStyles.Add(_dg_style);

                dataGridList.RowHeightDefault = Settings.Inventura1NasnimanoRowHeigth;
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
        private ListPolozkyNasnimane.NasnimaneRow SelectedRow
        {
            get
            {
                try
                {
                    return ((DataRowView)dataGridList.BindingContext[_list_polozky_nasnimane.Nasnimane].Current).Row as ListPolozkyNasnimane.NasnimaneRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Nasnimane2()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            GridStylesCreate();

            Cursor.Current = Cursors.Default;
        }

        private void Nasnimane2_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            SettingsLoad(); //musi probehnout pred Select_Current_Update()

            Select_Current_Update();

            SQLGetData();

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

				_itemsCount = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CreateResultSet_GetGount(_select_count);

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

				var dt_I4 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CreateResultSet_Get_I4(_select_current, aktualItem, (aktualItem + _pocetZobrazit));

				FillGrid(dt_I4);

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

		/// <summary>
		/// Vyplnuje grid na zaklade hodnoty _db_idx_first
		/// _currentItem musi byt nastaveno predem, pripadne se zkoriguje, pokud je mimo rozsah
		/// </summary>
		private void FillGrid(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable dt_I4)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				_list_polozky_nasnimane.Clear();
				_list_polozky_nasnimane.Nasnimane.BeginLoadData();
				
				//Sklady nemusi byt k dispozici
				//try { Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.Ta_sklady.Connection.Open(); }
				//catch { }

				foreach (var row_i4 in dt_I4)
				{
					ListPolozkyNasnimane.NasnimaneRow prow = _list_polozky_nasnimane.Nasnimane.NewNasnimaneRow();

					prow.CountEntries = row_i4.CountEntries;
					prow.ITEMNMBR = row_i4.ITEMNMBR;
					prow.CZ_CarKod = row_i4.CZ_CarKod;
					prow.LOCNCODE = row_i4.LOCNCODE;
					prow.VNDITNUM = row_i4.VNDITNUM;
					prow.QUANTITY = row_i4.QUANTITY;
					prow.QTYPACK = row_i4.QTYPACK;
					prow.SERLNMBR = row_i4.SERLNMBR;
					prow.DATEDONE = row_i4.DATEDONE;
					prow.TIMEDONE = row_i4.TIMEDONE;
					prow.USERID = row_i4.USERID;
					prow.DEX_ROW_ID = row_i4.DEX_ROW_ID;
					prow.GUID = row_i4.GUID;
					prow.QUANTITYMJ = row_i4.QUANTITYMJ;
					prow.MJ = row_i4.MJ;
					prow.ITEMCODE = row_i4.ITEMCODE;
					prow.REZ_1 = row_i4.REZ_1;
					prow.REZ_2 = row_i4.REZ_2;

					if(row_i4.IsWEIGHTNull())
						prow.SetWEIGHTNull();
					else
						prow.WEIGHT = row_i4.WEIGHT;

                    if (row_i4.IsExpiraceNull())
                        prow.SetExpiraceNull();
                    else
                        prow.Expirace = row_i4.Expirace;

					try
					{
						object sklid = row_i4.skl_id;
						if (sklid is string && sklid != null)
						{
							prow.skl_id = (string)sklid;
							prow.skl_desc = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.GetSkladDesc(prow.skl_id);
						}
						else
						{
							prow.skl_id = null;
							prow.skl_desc = null;
						}

						if (prow.Isskl_descNull() || prow.skl_desc == null)
							prow.skl_desc = "-";
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
					}

					_list_polozky_nasnimane.Nasnimane.AddNasnimaneRow(prow);

					}

					//Sklady nemusi byt k dispozici
					//try { Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_sklady.Ta_sklady.Connection.Close(); }
					//catch { }
					_list_polozky_nasnimane.Nasnimane.EndLoadData();

				foreach (ListPolozkyNasnimane.NasnimaneRow prow in _list_polozky_nasnimane.Nasnimane)
				{
					try
					{
						string o = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.NazevPolozky_I1(prow.ITEMNMBR.Trim());
						if (!string.IsNullOrEmpty(o)) 
							prow.ITEMDESC = o;
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
					}
				}

				dataGridList.DataSource = _list_polozky_nasnimane.Nasnimane;
				dataGridList.Refresh();
				dataGridList.CurrentRowIndex = 0;
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
                Cursor.Current = Cursors.WaitCursor;


                df_Countentries.Data =
                df_Itemnmbr.Data =
                df_Itemdesc.Data =
                df_Quantity.Data =
                df_Locncode.Data =
                df_CZCarKod.Data =
                df_Vnditum.Data =
                df_serlnmbr.Data =
                df_expirace.Data =
                df_timedone.Data =
                df_datedone.Data =
                df_userid.Data =
                df_QuantityMJ.Data =
                df_Rez1.Data =
                df_Rez2.Data =
                df_MJ.Data =
                df_Weight.Data =
                "-";

                if (SelectedRow != null)
                {
                    ListPolozkyNasnimane.NasnimaneRow prow = SelectedRow;

                    // TODO : itemdesc zobrazuje prvni polozku z i1 a ne aktualni hodnotu vuci vybrane z i4...
                    df_Countentries.Data = prow.CountEntries.ToString();
                    df_Itemdesc.Data = prow.ITEMDESC.Trim();
                    df_Itemnmbr.Data = prow.ITEMNMBR.Trim();
                    df_CZCarKod.Data = prow.CZ_CarKod.Trim();
                    df_Quantity.Data = prow.QUANTITY.ToString(Settings.UIFormatDesCisel);
                    df_Locncode.Data = prow.LOCNCODE.Trim();
                    df_Vnditum.Data = prow.VNDITNUM.Trim();
                    df_serlnmbr.Data = prow.SERLNMBR.Trim();
                    df_expirace.Data = prow.IsExpiraceNull() ? "-" : prow.Expirace.ToShortDateString();
                    df_timedone.Data = prow.TIMEDONE.Trim();
                    df_datedone.Data = prow.DATEDONE.Trim();
                    df_Qtypack.Data = prow.QTYPACK.ToString(Settings.UIFormatDesCisel);
                    df_userid.Data = prow.USERID.ToString();
                    df_Sklad.Data = prow.skl_id.Trim() + ":" + prow.skl_desc.Trim();
                    df_QuantityMJ.Data = prow.QUANTITYMJ.ToString(Settings.UIFormatDesCisel);
                    df_MJ.Data = prow.MJ.Trim();
                    df_Itemcode.Data = prow.IsITEMCODENull() ? "-" : prow.ITEMCODE;
                    df_Rez1.Data = prow.IsREZ_1Null() ? "-" : prow.REZ_1;
                    df_Rez2.Data = prow.IsREZ_2Null() ? "-" : prow.REZ_2;
                    df_Weight.Data = (prow.IsWEIGHTNull() ? 0 : prow.WEIGHT).ToString(Settings.UIFormatDesCisel);
 
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

            int aktualitemporadi = (_itemsCount > 0 ? _currentItem + 1 : 0);
            statusBarInfo.Text = "Z:" + aktualitemporadi + " z " + _itemsCount;
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
                    // TODO : implementovat co s carovym kodem...
                    MessageBoxBig.Show(ck, Color.Green);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
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


        private void SettingsLoad()
        {
            //1. nacteni razeni a trideni
            select_current_update = false; //Vypne aktualizaci

            RazeniHlavni = Settings.Inventura1NasnimaneRazeniH;
            RazeniVedlejsi = Settings.Inventura1NasnimaneRazeniV;
            SetrizeniHlavni = Settings.Inventura1NasnimaneSetriditH;
            SetrizeniVedlejsi = Settings.Inventura1NasnimaneSetriditV;

            select_current_update = true; //zapne aktualizaci

        }

        private void SettingsSave()
        {
            //Ulozeni rozvrhu razeni
            Settings.Inventura1NasnimaneRazeniH = RazeniHlavni;
            Settings.Inventura1NasnimaneRazeniV = RazeniVedlejsi;
            Settings.Inventura1NasnimaneSetriditH = SetrizeniHlavni;
            Settings.Inventura1NasnimaneSetriditV = SetrizeniVedlejsi;

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
            Settings.PoradiSloupcuInventura1Nasnimane = poradi;

            Settings.Inventura1NasnimanoRowHeigth = dataGridList.RowHeightDefault;

            Settings.Inventura1NasnimaneCOUNTENTRIESWidth = _dg_01.Width;
            Settings.Inventura1NasnimaneITEMNMBRWidth = _dg_02.Width;
            Settings.Inventura1NasnimaneCZCARKODWidth = _dg_03.Width;
            Settings.Inventura1NasnimaneLOCNCODEWidth = _dg_04.Width;
            Settings.Inventura1NasnimaneVNDITNUMWidth = _dg_05.Width;
            Settings.Inventura1NasnimaneQUANTITYWidth = _dg_06.Width;
            Settings.Inventura1NasnimaneQTYPACKWidth = _dg_07.Width;
            Settings.Inventura1NasnimaneSERLNMBRWidth = _dg_08.Width;
            Settings.Inventura1NasnimaneDATEDONEWidth = _dg_09.Width;
            Settings.Inventura1NasnimaneTIMEDONEWidth = _dg_10.Width;
            Settings.Inventura1NasnimaneUSERIDWidth = _dg_11.Width;
            Settings.Inventura1NasnimaneDEXROWIDWidth = _dg_12.Width;
            Settings.Inventura1NasnimaneGUIDWidth = _dg_13.Width;
            Settings.Inventura1NasnimaneITEMDESCWidth = _dg_14.Width;
            Settings.Inventura1NasnimaneSkladIDWidth = _dg_15.Width;
            Settings.Inventura1NasnimaneSkladDescWidth = _dg_16.Width;
            Settings.Inventura1NasnimaneQUANTITYMJWidth = _dg_17.Width;
            Settings.Inventura1NasnimaneMJWidth = _dg_18.Width;
            Settings.Inventura1NasnimaneExpiraceWidth = _dg_19.Width;
        }

        private void PerformKonec(bool question)
        //private void PerformKonec()
        {
            if (question && MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Nasnimane2ZpetDoSeznamuPolozekDotaz, Fask.Localization.Localization.Inventura1Nasnimane2Dotaz,
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
                ListPolozkyNasnimane.NasnimaneRow prow = this.SelectedRow;
                if (prow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Inventura1Nasnimane2NeniVybranaPolozka, Fask.Localization.Localization.Inventura1Nasnimane2Smazat, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

                if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1Nasnimane2SmazatPolozkuDotaz, prow.ITEMDESC.Trim(), prow.QUANTITY.ToString(Settings.UIFormatDesCisel)), Fask.Localization.Localization.Inventura1Nasnimane2Smazat, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, Color.Red))
                {
                    return;
                }

                bool o_unchecked = false;
                if (!StaticMethods.OnlineUnCheck(prow.CountEntries, prow.ITEMNMBR, ref o_unchecked))
                    return;

                int deleted = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.DeleteByGUID_I4(prow.GUID);
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
            Polozka,
            SN,
            Lokace,
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
                menuItemRaditH_Lokace.Checked =
                    menuItemRaditH_Polozka.Checked =
                    menuItemRaditH_SN.Checked =
                    menuItemRaditH_Uzivatel.Checked = false;
                menuItemRaditV_Lokace.Enabled =
                    menuItemRaditV_Polozka.Enabled =
                    menuItemRaditV_SN.Enabled =
                    menuItemRaditV_Uzivatel.Enabled = true;
                switch (razeniHlavni)
                {
                    case RazeniType.Polozka:
                        menuItemRaditH_Polozka.Checked = true;
                        menuItemRaditV_Polozka.Enabled = false;
                        break;
                    case RazeniType.SN:
                        menuItemRaditH_SN.Checked = true;
                        menuItemRaditV_SN.Enabled = false;
                        break;
                    case RazeniType.Lokace:
                        menuItemRaditH_Lokace.Checked = true;
                        menuItemRaditV_Lokace.Enabled = false;
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
                menuItemRaditV_Lokace.Checked =
                    menuItemRaditV_Polozka.Checked =
                    menuItemRaditV_SN.Checked =
                    menuItemRaditV_Uzivatel.Checked = false;
                menuItemRaditH_Lokace.Enabled =
                    menuItemRaditH_Polozka.Enabled =
                    menuItemRaditH_SN.Enabled =
                    menuItemRaditH_Uzivatel.Enabled = true;
                switch (razeniVedlejsi)
                {
                    case RazeniType.Polozka:
                        menuItemRaditV_Polozka.Checked = true;
                        menuItemRaditH_Polozka.Enabled = false;
                        break;
                    case RazeniType.SN:
                        menuItemRaditV_SN.Checked = true;
                        menuItemRaditH_SN.Enabled = false;
                        break;
                    case RazeniType.Lokace:
                        menuItemRaditV_Lokace.Checked = true;
                        menuItemRaditH_Lokace.Enabled = false;
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
			_currentItem = -1;
			SQLGetData();
		}

		private void MoveLast()
		{
			_currentItem = _itemsCount - _pocetZobrazit + 1;
			SQLGetData();
		}

		private void MovePrev()
		{
			_currentItem -= _pocetZobrazit;
			SQLGetData();
			dataGridList.CurrentRowIndex = _list_polozky_nasnimane.Nasnimane.Rows.Count - 1; //na posledni index
		}

		private void MoveNext()
		{
			_currentItem = _firstItem + _pocetZobrazit;
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

        private void Nasnimane2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == MST_Global.DataGridScrollDown)
            {
				if (dataGridList.CurrentRowIndex == _list_polozky_nasnimane.Nasnimane.Rows.Count - 1 && (_firstItem + _list_polozky_nasnimane.Nasnimane.Count) < _itemsCount)
                { //jsem na posledni polozce
                    this.MoveNext();
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == MST_Global.DataGridScrollUp)
            {
				if (dataGridList.CurrentRowIndex == 0 && _firstItem > 0)
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
            }
            else if (e.KeyCode == Keys.Back)
            {
                //SmazPolozku(this.SelectedRow);
                DeleteSelectedItem();
            }
            else if (e.KeyCode == Keys.D1)
            {
                //ZobrazitVse();
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
                //vyhledejPolozkuNazev();
            }
            else if (e.KeyCode == Keys.F2)
            {
                //vyhledejPolozkuCarovyKod();
            }
            else if (e.KeyCode == Keys.F3)
            {
                //NajdiPolozkuPozice();
            }
            else
                return;

            e.Handled = true;
        }

        private void dataGridList_CurrentRowIndexChanged(object sender, EventArgs e)
        {
			_currentItem = _firstItem + dataGridList.CurrentRowIndex;
            this.UpdateForm();
        }

        private void dataGridList_CurrentCellChanged(object sender, EventArgs e)
        {
			_currentItem = _firstItem + dataGridList.CurrentRowIndex;
            this.UpdateForm();
        }

        private void menuItemRaditH_Polozka_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.Polozka)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.Polozka;
        }

        private void menuItemRaditH_SN_Click(object sender, EventArgs e)
        {
            if (RazeniHlavni == RazeniType.SN)
                RazeniHlavni = RazeniType.None;
            else
                RazeniHlavni = RazeniType.SN;
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

        private void menuItemRaditV_Polozka_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.Polozka)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.Polozka;
        }

        private void menuItemRaditV_SN_Click(object sender, EventArgs e)
        {
            if (RazeniVedlejsi == RazeniType.SN)
                RazeniVedlejsi = RazeniType.None;
            else
                RazeniVedlejsi = RazeniType.SN;
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

    }

}