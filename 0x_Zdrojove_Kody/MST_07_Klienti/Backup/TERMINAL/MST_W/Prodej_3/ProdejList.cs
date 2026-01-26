//#define TEST

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
using Fask.MST_W.Classes;
using Fask.MST_W.ServerAccess;
using System.Linq;
using Fask.Parsing.Codes;
using Fask.Parsing.Codes.Interfaces;

namespace Fask.MST_W.Prodej_3
{
	/// <summary>
	/// Trida PRODEJ LIST
	/// </summary>
	public partial class ProdejList : System.Windows.Forms.Form
	{
		#region Lokalne promenne

		private Fask.MST_W.ExpediceService.SSCC nmbrpal = null; // cislo palety

		DateTime expiraceLast = DateTime.Now.AddDays(30);

		//Typ vyberu - vice viz Classes.Enums (0 == nesnastaveno, ale nemelo by byt - znaci chybu v programu)
		private byte _input_mode = 0;

		private SejmiKodForm skf = null;
		private ProdejPridatPolozku naplnpMnozstvi = null;
		private ProdejPridatPolozku naplnpSerialNumber = null;
		private ProdejPridatPolozku naplnpLocnCode = null;
		private ProdejVyberStrediska2 pvsForm = null;
		private ProdejVyberPracovnika2 pvpForm = null;
		private ProdejVyberPalety pvpaletyForm = null;

		private int _db_records_count = 0;
		private int _db_record_actual = 0;
		private int _db_records_per_view = 8;

		private string _db_sort = string.Empty;

		// Trideni
		const string cSortItemdescASC = "ITEMDESC ASC";
		const string cSortItemdescDESC = "ITEMDESC DESC";
		const string cSortItemnmbrASC = "ITEMNMBR ASC";
		const string cSortItemnmbrDESC = "ITEMNMBR DESC";

		//Filtrovani
		string _filtr_nazev = string.Empty;

		private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row _odberatel = null;
		private Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row _zbozi = null;
		private Fask.SQLiteDBs.DataSets.Strediska.CZMST091Row _stredisko = null;
		private Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row _typdokladu = null;
		private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _skladZdroj = null;    // zdrojovy sklad
		private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _skladCil = null;    // cilovy sklad
		private Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096Row _pracovnik = null;
		private Fask.SQLiteDBs.DataSets.Meny.CZMST097Row _mena = null;
		//private Fask.MST_W.ProdejService.Obecne.PaletyRow paletyRow = null;

		private Fask.SQLiteDBs.DataSets.Prodej _prodejTable = new Fask.SQLiteDBs.DataSets.Prodej();
		private Fask.SQLiteDBs.DataSets.Zbozi _katalogZbozi = null;


		private DataView _prodejTableView = null;
		private DataView _katalogZboziView = null;

		private Schema.Sklad nasklade;

		private int _cislodavky = -1;
		private string _cislodavkysqlfilename = string.Empty;
		private string serltnum = string.Empty;

		private Timer timer;

		private int selectedrowindex = 0;
		private int selectedrowindexN = 0;

		private string TextBase = string.Empty;

		enum HledaniStatus
		{
			Nenalezeno = 0,
			NalezenJedenZaznam = 1,
			NalezenoViceZaznamu = 2,
			NalezenoViceJakNastavenyPocetZaznamu = 3
		}

		#endregion

		#region Verejne promenne

		public enum ZobrazeniTyp
		{
			List,
			Detail,
			Nasnimane,
			Unknown,
			DetailNasnimane
		}

		private ZobrazeniTyp _zobrazeni = ZobrazeniTyp.Unknown;
		public ZobrazeniTyp Zobrazeni
		{
			get { return _zobrazeni; }
			set
			{
				if (_zobrazeni != value)
				{
					_zobrazeni = value;
					switch (_zobrazeni)
					{
						case ZobrazeniTyp.Nasnimane:
							panelDetail.Hide();
							panelList.Hide();
							panelDetailNasnimane.Hide();
							panelNasnimane.Show();
							panelNasnimane.Dock = DockStyle.Fill;
							dataGridNasnimane.Focus();
							break;
						case ZobrazeniTyp.Detail:
							panelList.Hide();
							panelNasnimane.Hide();
							panelDetailNasnimane.Hide();
							panelDetail.Show();
							panelDetail.Dock = DockStyle.Fill;
							dataGrid1.Focus();
							break;
						case ZobrazeniTyp.List:
						default:
							_zobrazeni = ZobrazeniTyp.List;
							panelDetail.Hide();
							panelNasnimane.Hide();
							panelDetailNasnimane.Hide();
							panelList.Show();
							panelList.Dock = DockStyle.Fill;
							dataGrid1.Focus();
							break;
						case ZobrazeniTyp.DetailNasnimane:
							panelList.Hide();
							panelNasnimane.Hide();
							panelDetail.Hide();
							panelDetailNasnimane.Show();
							panelDetailNasnimane.Dock = DockStyle.Fill;
							dataGridNasnimane.Focus();
							break;

					}
				}
			}
		}


		public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row SelectedZbozi
		{
			get
			{
				try
				{
					return (dataGrid1.BindingContext[_katalogZboziView].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row;
				}
				catch
				{
					return null;
				}
			}
		}

		public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow SelectedNasnimane
		{
			get
			{
				try
				{
					return (dataGridNasnimane.BindingContext[_prodejTableView].Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow;
				}
				catch
				{
					return null;
				}
			}
		}


		#endregion

		#region Form Eventy


		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="cislodavky">èísla dávky (CountEntries)</param>
		/// <param name="odberatel">øádek z CZMST090 (odberatel/dodavatel/...)</param>
		/// <param name="typdokladu">øádek z CZMST092 (typ dokladu)</param>
		/// <param name="skladZdroj"> øádek z CZMST093  SKLAD Zdroj</param>
		/// <param name="skladCil">øádek z CZMST092 SKLAD Cíl</param>
		/// <param name="str_id">ID Strediska</param>
		/// <param name="mena">øádek z CZMST097 Mìna</param>
		/// <param name="serltnum">seriove èíslo/ šarže</param>
		public ProdejList(int cislodavky, Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel, Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row typdokladu, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladZdroj, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladCil, string str_id, Fask.SQLiteDBs.DataSets.Meny.CZMST097Row mena, string serltnum)
		{
			InitializeComponent();

			labelItemnmbrN.Text = "-";
			labelQTYN.Text = "-";
			labelQTYPACKN.Text = "-";
			labelSNN.Text = "-";
			labelNactenoN.Text = "-";
			labelZakladN.Text = "-";
			labelSDPHN.Text = "-";
			this.dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
			this.dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

			this._katalogZbozi = new Fask.SQLiteDBs.DataSets.Zbozi();
			this._katalogZboziView = new DataView(this._katalogZbozi.CZMST095);
			this.dataGrid1.DataSource = this._katalogZboziView;

			this.menuItem8.Enabled = Prodej.Globals.OnlinePocetKusu;
			this.menuItem10.Enabled = Prodej.Globals.OnlinePocetKusuSklad;
			this.menuItem11.Enabled = Prodej.Globals.OnlineDetailPolozka;
			this.miPaletaZmenit.Enabled = false;    // TODO: konfigurace a implementovat
			this.miTisk.Enabled = MST_Global.PovolitPrintServer;

			// tisk palety, odstraneni ze seznamu, pokud je vypnuto
			miTiskPaleta.Enabled = MST_Global.PovolitPrintServer && ((typdokladu != null && !typdokladu.Iscfg_tisk_paletyNull() && typdokladu.cfg_tisk_palety > 0));
			if (!miTiskPaleta.Enabled && miTisk.MenuItems.Contains(this.miTiskPaleta))
				miTisk.MenuItems.Remove(this.miTiskPaleta);

			//DataColumn dc = this._prodejTable.CZMST_DI.Columns.Add("ITEMDESC", typeof(string));

			this._prodejTableView = new DataView(this._prodejTable.CZMST_DI);
			this.dataGridNasnimane.DataSource = this._prodejTableView;

			InitializeDataGridView();

			MyInitializeGrid();
			MyInitializePopisItems_DetailNasnimane();
			MyInitializePopisItems_Detail();

			this._cislodavky = cislodavky;
			this._cislodavkysqlfilename = Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.Ext_Prodej);
			this._odberatel = odberatel;
			this._typdokladu = typdokladu;
			this._mena = mena;
			this._skladZdroj = skladZdroj;
			this._skladCil = skladCil;
			this.serltnum = serltnum;
			//this.Text = MST_Global.ProdejName + " (" + cislodavky.ToString() + ")";
			if (_typdokladu != null && !_typdokladu.Isdoc_descNull() && !string.IsNullOrEmpty(_typdokladu.doc_desc.Trim()))
			{
				this.Text = _typdokladu.doc_desc.Trim() + " (" + cislodavky.ToString() + ")";
			}
			else
			{
				this.Text = MST_Global.ProdejName + " (" + cislodavky.ToString() + ")";
			}

			this.TextBase = this.Text;
			try
			{
				nasklade = new Fask.MST_W.Schema.Sklad();
				//nasklade.ReadXml(Main.CiselnikSkladuFileName);
			}
			catch
			{
			}
			try
			{
				timer = new Timer();
				timer.Interval = 60000;
				timer.Tick += new EventHandler(timer_Tick);
				timer.Enabled = true;
			}
			catch
			{
			}

			#region Nastaveni strediska
			if (str_id != null && str_id != string.Empty && !Prodej.Globals.StrediskoKPolozce && Prodej.Globals.StrediskoJednoNaDavku)
			{
				if (Prodej.Globals.StrediskoText)
				{
					StrediskoTextSet(str_id);
				}
				else
				{
					try
					{
						Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable dt_s = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_strediska.GetDataByStr_id(str_id);
						if (dt_s.Count > 0)
							_stredisko = dt_s[0];
						else
						{
							//MessageBoxBigTimeout.Show("Odpovídající støedisko s ID '" + str_id + "' nenalezeno!\n\nVyberte jiné ze seznamu", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListStrediskoNenalezenoVyberDotaz, str_id), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						}

					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
						MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						return;
					}
				}
				StrediskoMenuStateUpdate();
			}
			#endregion

			////jestlize je stredisko povoleno a je neni k polozce(je k davce), tak se dopta hned
			//if (this.menuItemStredisko.Enabled && !Prodej.Globals.StrediskoKPolozce)
			//    StrediskoSet();
			//else if (((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska) && !Prodej.Globals.StrediskoKPolozce && Prodej.Globals.StrediskoJednoNaDavku)
			//{
			//    StrediskoSet();
			//}
			if ((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska)
			{ // resit zadani strediska
				if (_stredisko == null)
				{
					if (!Prodej.Globals.StrediskoKPolozce)
						StrediskoSet();
					else
						StrediskoMenuStateUpdate();
				}
				else
					StrediskoMenuStateUpdate();
			}
			else
				StrediskoMenuStateUpdate();


			if (_typdokladu != null && _typdokladu.cfg_paleta_id > 0)
			{
				menuItem13.Enabled = true;
			}
			else
			{
				menuItem13.Enabled = false;
			}

			if (_typdokladu != null && !_typdokladu.Iscfg_sn_na_davkuNull() && _typdokladu.cfg_sn_na_davku > 0)
			{
				menuItemSarze.Enabled = true;
			}
			else
			{
				menuItemSarze.Enabled = false;
			}

			if (_typdokladu != null && _typdokladu.cfg_zakazka_id > 0)
			{
				menuItem14.Enabled = true;
			}
			else
			{
				menuItem14.Enabled = false;
			}
			//Nastaveni pracovnika (dle strediska...)
			if ((_typdokladu != null && _typdokladu.cfg_prac > 0) || Prodej.Globals.Pracovnici)
			{ // resit zadani strediska
				if (_pracovnik == null)
				{
					if (!Prodej.Globals.PracovniciKPolozce)
						PracovniciSet();
					else
						PracovniciMenuStateUpdate();
				}
				else
					PracovniciMenuStateUpdate();
			}
			else
				PracovniciMenuStateUpdate();

			#region palety, stara funkcionalita (zakomentovano)
			// 22.6.2016 PeV: zakomentovano, pouzivalo se pouze u jednoho zakaznika??
			//PaletaMenuStateUpdate();
			#endregion
		}

		/// <summary>
		/// Form Events Activated
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_Activated(object sender, EventArgs e)
		{
			// aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
			Components.KeyboardManager.LoadDefaultKeyboardMode();
		}

		/// <summary>
		/// Form Events Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_Closing(object sender, CancelEventArgs e)
		{
			finalize();

			//ScannerStop();

		}

		/// <summary>
		/// Form Events Deactivate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_Deactivate(object sender, EventArgs e)
		{
			// deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
			Components.KeyboardManager.SaveDefaultKeyboardMode();
		}

		/// <summary>
		/// Form Events KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				PerformKonec();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (_zobrazeni != ZobrazeniTyp.Nasnimane)
				{
					_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
					pridatPolozku();
				}
				else
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListVTomtoZobrazeniNelzeZadatMnozstvi, Fask.Localization.Localization.Prodej3ProdejListInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
			}
			else if (e.KeyCode == Keys.Back)
			{
				smazatNasnimanou();
			}
			else if (e.KeyCode == Keys.F1)
			{
				zobrazeniList();
			}
			else if (e.KeyCode == Keys.F2)
			{
				zobrazeniDetail();
			}
			else if (e.KeyCode == Keys.F3)
			{
				zobrazeniNasnimane();
			}
			else if (e.KeyCode == Keys.F4)
			{
				ShowStavProdeje();
			}
			else if (e.KeyCode == Keys.F5)
			{
				najdiCarovyKod();
			}
			else if (e.KeyCode == Keys.F6)
			{
				najdiNazev();
			}
			else if (e.KeyCode == Keys.F7)
			{
				najdiPolozkaCislo();
			}
			else if (e.KeyCode == Keys.F8)
			{
				menuItemVse_Click(null, null);
			}
			else if (e.KeyCode == Keys.F9)
			{
				if (Prodej.Globals.OnlinePocetKusu)
					DetailPocetKusu();
			}
			else if (e.KeyCode == Keys.F10)
			{
				#region TaD 9.4.2019 OLD
				//if (Prodej.Globals.OnlinePocetKusuSklad)
				//    DetailPocetKusuLokace();
				////CHECKITEMSTATE
				////try
				////{
				////    _zbozi = this.SelectedZbozi;
				////    if (_zbozi != null)
				////        checkitemstate(_zbozi.ITEMNMBR, "");
				////    //UpdateForm();
				////}
				////catch { } 
				#endregion

				if (Prodej.Globals.F10_OnlinePocetKusuSklad)
				{
					if (Prodej.Globals.OnlinePocetKusuSklad)
						DetailPocetKusuLokace();
				}
				else if (Prodej.Globals.F10_ZobrazitAlternativyLokaci)
				{
					if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0)
					{
						ZobrazitAlternativyLokaci();
					}
					else
					{
						MessageBoxBig.Show("Lokaèní mechanismus není povolen!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					}
				}

				else if (Prodej.Globals.F10_TiskEtiketa)
				{
					mi_TiskEtiketaAnoNE_Click(null, null);
				}


			}
			//else if (e.KeyCode == Keys.D1)
			//{
			//    najdiCarovyKodDoplnek();
			//}
			else if (e.KeyCode == Keys.D1)
			{
				filtrNazev();
			}
			else if (e.KeyCode == Keys.D2)
			{
				if (miPaletaGenerovat.Enabled)
					PerformGenerovatCisloPalety();
			}
			else if (e.KeyCode == Keys.D3)
			{
				if (MST_Global.PovolitPrintServer && ((_typdokladu != null && !_typdokladu.Iscfg_tisk_soupisNull() && _typdokladu.cfg_tisk_soupis > 0) || Prodej.Globals.PovolitTiskSoupisu))
					TiskSoupis();
			}
			else if (e.KeyCode == Keys.D4)
			{
				//if (MST_Global.PovolitPrintServer && ((_typdokladu != null && !_typdokladu.Iscfg_tisk_paletyNull() && _typdokladu.cfg_tisk_palety > 0)))
				//    PerformTiskPaleta();

				//TaD - Hanibal, uprava prepinani po jednom
				//Prodej.Globals.Mnozstvi1Auto = !Prodej.Globals.Mnozstvi1Auto;
				mi_RezimSNimani_Click(null, null);
			}
			else if (e.KeyCode == Keys.D5)
			{
				PerformZmenaSklad();
			}
			else
			{
				return;
			}

			e.Handled = true;
		}

		/// <summary>
		/// Form Events Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_Load(object sender, EventArgs e)
		{
			// nacteni lokalizace ze souboru
			Fask.Localization.LocalizationExtensionForm.Localize(this);

			this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
			this.Size = Forms.FormLocation.ScreenResolution;

			_db_records_per_view = Prodej.Globals.GridViewRowCount;

            _db_records_count = 0;
            if (Prodej.Globals.PouzitSklady && Prodej.Globals.FiltrCiselnikSkladu && this._skladZdroj != null)
            {
                _db_records_count = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.Get_Count_By_Sklid(this._skladZdroj.skl_id.Trim());
            }
            else
            {
                _db_records_count = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.Get_Count_All();
            }

			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable deh_dt = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DEH();
			if (deh_dt.Count <= 0)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Insert_DEH(_cislodavky, Guid.NewGuid());

			mi_RezimSNimani.Checked = Prodej.Globals.Mnozstvi1Auto;
			mi_TiskEtiketaAnoNE.Checked = Prodej.Globals.TiskEtiketyPoPridaniZbozi;
			mi_KonScan.Checked = Program.mstw.Scanner.ContinuousRead;

            timerLoad.Enabled = true;
            //ProdejList_Shown(null, null);
        }


		#region Shown je volany s LOAD pomoci timeru

		/// <summary>
		/// Metoda Shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProdejList_Shown(object sender, EventArgs e)
		{
			timerLoad.Enabled = false;
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			try
			{
				_db_sort = Settings.ProdejListLastSort;
				if (Prodej.Globals.ZobrazovatListPolozek)
					LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);

				SetSortText();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}

			try
			{
				if (_typdokladu != null && _typdokladu.cfg_palety == 0)
				{
					menuItemPaleta.Enabled = false;
				}

				// vygenerovani cisla palety
				if (_typdokladu != null && !_typdokladu.Iscfg_onl_palety_generovatNull() && _typdokladu.cfg_onl_palety_generovat > 0)
				{
					miPaletaGenerovat_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, ProdejList_Shown.Palety");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}

			try
			{
				dataGrid1.CurrentCell = new DataGridCell(0, 1);
				dataGrid1.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

            try
            {
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Fill_DI(_prodejTable.CZMST_DI);

                if (Prodej.Globals.PracovniciJedenNaDavku)
                { //Pouze najit z nasnimanych, pokud je jeden na davku abyto bylo stejne jako u predchozich, jinak to necham az na zadani ...
                    var pracdt = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_pracovnici.GetDataByPracID(_prodejTable.CZMST_DI[0].PRAC_ID);
                    if (pracdt.Count == 1) _pracovnik = pracdt[0];
                }

                // aktualizace itemdesc dat volneho pohybu z ciselniku zbozi
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.UpdateItemDesc(_prodejTable.CZMST_DI);
            }
            catch (Exception exDI)
            {
                Logging.Log.Write(exDI);
            }

			try
			{
				dataGridNasnimane.CurrentCell = new DataGridCell(0, 1);
				dataGridNasnimane.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

			UpdateForm();

			ScannerStart();

			Cursor.Current = Cursors.Default;
		}

		#endregion

		#endregion

		#region Inicializacne metody

		/// <summary>
		/// Metoda pro inicializaci hlavièek DataGridView
		/// </summary>
		private void InitializeDataGridView()
		{
			DataGridTableStyle ts = new DataGridTableStyle();
			ts.MappingName = _katalogZbozi.CZMST095.TableName;

			//DataGrid2TextBoxColumn dg = new DataGrid2TextBoxColumn();
			//dg.HeaderText = "";
			//dg.MappingName = _katalogZbozi.CZMST095..ColumnName;
			//dg.NullText = "-";
			//dg.Width = 50;
			//ts.GridColumnStyles.Add(dg);

			DataGrid2TextBoxColumn dgNazev = new DataGrid2TextBoxColumn();
			dgNazev.HeaderText = "Název";
			dgNazev.MappingName = _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
			dgNazev.NullText = "-";
			dgNazev.Width = 150;
			ts.GridColumnStyles.Add(dgNazev);

			DataGrid2TextBoxColumn dg1 = new DataGrid2TextBoxColumn();
			dg1.HeaderText = "Pol.è.";
			dg1.MappingName = _katalogZbozi.CZMST095.ITEMNMBRColumn.ColumnName;
			dg1.NullText = "-";
			dg1.Width = 50;
			ts.GridColumnStyles.Add(dg1);

			DataGrid2NumberBoxColumn dgCena = new DataGrid2NumberBoxColumn();
			try
			{
				dgCena.HeaderText = "Cena";
				dgCena.MappingName = _katalogZbozi.CZMST095.Columns["PRICE" + _odberatel.odb_typ.Trim()].ColumnName;
				dgCena.NullText = "-";
				dgCena.Width = 50;
				dgCena.Alignment = StringAlignment.Far;
				ts.GridColumnStyles.Add(dgCena);
			}
			catch
			{

				dgCena.HeaderText = "Cena";
				dgCena.MappingName = _katalogZbozi.CZMST095.Columns["PRICE0"].ColumnName;
				dgCena.NullText = "-";
				dgCena.Width = 50;
				dgCena.Alignment = StringAlignment.Far;
				ts.GridColumnStyles.Add(dgCena);


			}

			DataGrid2TextBoxColumn dgCarKod = new DataGrid2TextBoxColumn();
			dgCarKod.HeaderText = "Èár. kód";
			dgCarKod.MappingName = _katalogZbozi.CZMST095.VNDITNUMColumn.ColumnName;
			dgCarKod.NullText = "-";
			dgCarKod.Width = 150;
			ts.GridColumnStyles.Add(dgCarKod);

			DataGrid2TextBoxColumn dg2 = new DataGrid2TextBoxColumn();
			dg2.HeaderText = "CZ kód";
			dg2.MappingName = _katalogZbozi.CZMST095.CZ_CarKodColumn.ColumnName;
			dg2.NullText = "-";
			dg2.Width = 50;
			ts.GridColumnStyles.Add(dg2);

			DataGrid2TextBoxColumn dg3 = new DataGrid2TextBoxColumn();
			dg3.HeaderText = "Lokace";
			dg3.MappingName = _katalogZbozi.CZMST095.LOCNCODEColumn.ColumnName;
			dg3.NullText = "-";
			dg3.Width = 50;
			ts.GridColumnStyles.Add(dg3);

			DataGrid2TextBoxColumn dg10 = new DataGrid2TextBoxColumn();
			dg10.HeaderText = "Kód položky";
			dg10.MappingName = _katalogZbozi.CZMST095.ITEMCODEColumn.ColumnName;
			dg10.NullText = "-";
			dg10.Width = 50;
			ts.GridColumnStyles.Add(dg10);

			DataGrid2NumberBoxColumn dg4 = new DataGrid2NumberBoxColumn();
			dg4.HeaderText = "Kusù";
			dg4.MappingName = _katalogZbozi.CZMST095.QTYColumn.ColumnName;
			dg4.NullText = "-";
			dg4.Width = 50;
			dg4.Alignment = StringAlignment.Far;
			ts.GridColumnStyles.Add(dg4);

			DataGrid2NumberBoxColumn dg5 = new DataGrid2NumberBoxColumn();
			dg5.HeaderText = "Balení";
			dg5.MappingName = _katalogZbozi.CZMST095.QTYPACKColumn.ColumnName;
			dg5.NullText = "-";
			dg5.Width = 50;
			ts.GridColumnStyles.Add(dg5);

			DataGrid2TextBoxColumn dg6 = new DataGrid2TextBoxColumn();
			dg6.HeaderText = "Sklad ID";
			dg6.MappingName = _katalogZbozi.CZMST095.SKL_IDColumn.ColumnName;
			dg6.NullText = "-";
			dg6.Width = 50;
			ts.GridColumnStyles.Add(dg6);

			DataGrid2TextBoxColumn dg7 = new DataGrid2TextBoxColumn();
			dg7.HeaderText = "MJ";
			dg7.MappingName = _katalogZbozi.CZMST095.MJColumn.ColumnName;
			dg7.NullText = "-";
			dg7.Width = 50;
			ts.GridColumnStyles.Add(dg7);

			DataGrid2TextBoxColumn dg8 = new DataGrid2TextBoxColumn();
			dg8.HeaderText = "DMJ";
			dg8.MappingName = _katalogZbozi.CZMST095.DMJColumn.ColumnName;
			dg8.NullText = "-";
			dg8.Width = 50;
			ts.GridColumnStyles.Add(dg8);

			DataGrid2TextBoxColumn dg9 = new DataGrid2TextBoxColumn();
			dg9.HeaderText = "REZ 1";
			dg9.MappingName = _katalogZbozi.CZMST095.REZ1Column.ColumnName;
			dg9.NullText = "-";
			dg9.Width = 50;
			ts.GridColumnStyles.Add(dg9);

			DataGrid2TextBoxColumn dg90 = new DataGrid2TextBoxColumn();
			dg90.HeaderText = "REZ 2";
			dg90.MappingName = _katalogZbozi.CZMST095.REZ2Column.ColumnName;
			dg90.NullText = "-";
			dg90.Width = 50;
			ts.GridColumnStyles.Add(dg90);

			DataGrid2TextBoxColumn dg91 = new DataGrid2TextBoxColumn();
			dg91.HeaderText = "REZ 3";
			dg91.MappingName = _katalogZbozi.CZMST095.REZ3Column.ColumnName;
			dg91.NullText = "-";
			dg91.Width = 50;
			ts.GridColumnStyles.Add(dg91);

			DataGrid2TextBoxColumn dg92 = new DataGrid2TextBoxColumn();
			dg92.HeaderText = "REZ 4";
			dg92.MappingName = _katalogZbozi.CZMST095.REZ4Column.ColumnName;
			dg92.NullText = "-";
			dg92.Width = 50;
			ts.GridColumnStyles.Add(dg92);

			//nazev skladu 
			DataGrid2TextBoxColumn dg11 = new DataGrid2TextBoxColumn();
			dg11.HeaderText = "Sklad";
			dg11.MappingName = _katalogZbozi.CZMST095.SKL_DESCColumn.ColumnName;
			dg11.NullText = "-";
			dg11.Width = 50;
			ts.GridColumnStyles.Add(dg11);

			DataGrid2TextBoxColumn dg12 = new DataGrid2TextBoxColumn();
			dg12.HeaderText = "Odbìratel ID";
			dg12.MappingName = _katalogZbozi.CZMST095.ODB_IDColumn.ColumnName;
			dg12.NullText = "-";
			dg12.Width = 50;
			ts.GridColumnStyles.Add(dg12);

			DataGrid2TextBoxColumn dg13 = new DataGrid2TextBoxColumn();
			dg13.HeaderText = "SN";
			dg13.MappingName = _katalogZbozi.CZMST095.SERLTNUMColumn.ColumnName;
			dg13.NullText = "-";
			dg13.Width = 50;
			ts.GridColumnStyles.Add(dg13);

			DataGrid2TextBoxColumn dg93 = new DataGrid2TextBoxColumn();
			dg93.HeaderText = "MENA_ID";
			dg93.MappingName = _katalogZbozi.CZMST095.MENA_IDColumn.ColumnName;
			dg93.NullText = "-";
			dg93.Width = 50;
			ts.GridColumnStyles.Add(dg93);

			DataGrid2TextBoxColumn dgweight = new DataGrid2TextBoxColumn();
			dgweight.HeaderText = "Váha";
			dgweight.MappingName = _katalogZbozi.CZMST095.WEIGHTColumn.ColumnName;
			dgweight.NullText = "-";
			dgweight.Width = 50;
			ts.GridColumnStyles.Add(dgweight);


			dataGrid1.TableStyles.Add(ts);

			//Nasnimane
			DataGridTableStyle tsN = new DataGridTableStyle();
			tsN.MappingName = _prodejTable.CZMST_DI.TableName;

			DataGrid2TextBoxColumn nazevN = new DataGrid2TextBoxColumn();
			nazevN.HeaderText = "Název";
			nazevN.MappingName = _prodejTable.CZMST_DI.ITEMDESCColumn.ColumnName;
			nazevN.NullText = "-";
			nazevN.Width = 150;
			tsN.GridColumnStyles.Add(nazevN);

			DataGrid2TextBoxColumn dgN1 = new DataGrid2TextBoxColumn();
			dgN1.HeaderText = "Pol.è.";
			dgN1.MappingName = _prodejTable.CZMST_DI.ITEMNMBRColumn.ColumnName;
			dgN1.NullText = "-";
			dgN1.Width = 50;
			tsN.GridColumnStyles.Add(dgN1);

			DataGrid2TextBoxColumn dgN1a = new DataGrid2TextBoxColumn();
			dgN1a.HeaderText = "Kód položky";
			dgN1a.MappingName = _prodejTable.CZMST_DI.ITEMCODEColumn.ColumnName;
			dgN1a.NullText = "-";
			dgN1a.Width = 50;
			tsN.GridColumnStyles.Add(dgN1a);

			DataGrid2TextBoxColumn dgN2 = new DataGrid2TextBoxColumn();
			dgN2.HeaderText = "Lokace";
			dgN2.MappingName = _prodejTable.CZMST_DI.LOCNCODEColumn.ColumnName;
			dgN2.NullText = "-";
			dgN2.Width = 50;
			tsN.GridColumnStyles.Add(dgN2);

			DataGrid2NumberBoxColumn dgN3 = new DataGrid2NumberBoxColumn();
			dgN3.HeaderText = "Kusù";
			dgN3.MappingName = _prodejTable.CZMST_DI.QTYSHPPDColumn.ColumnName;
			dgN3.NullText = "-";
			dgN3.Width = 50;
			dgN3.Alignment = StringAlignment.Far;
			tsN.GridColumnStyles.Add(dgN3);

			DataGrid2NumberBoxColumn dgN4 = new DataGrid2NumberBoxColumn();
			dgN4.HeaderText = "Balení";
			dgN4.MappingName = _prodejTable.CZMST_DI.QTYPACKColumn.ColumnName;
			dgN4.NullText = "-";
			dgN4.Width = 50;
			dgN4.Alignment = StringAlignment.Far;
			tsN.GridColumnStyles.Add(dgN4);

			DataGrid2TextBoxColumn dgN5 = new DataGrid2TextBoxColumn();
			dgN5.HeaderText = "Ser. èísla";
			dgN5.MappingName = _prodejTable.CZMST_DI.SERLTNUMColumn.ColumnName;
			dgN5.NullText = "-";
			dgN5.Width = 150;
			tsN.GridColumnStyles.Add(dgN5);

			DataGrid2TextBoxColumn dgN6 = new DataGrid2TextBoxColumn();
			dgN6.HeaderText = "MJ";
			dgN6.MappingName = _prodejTable.CZMST_DI.MJColumn.ColumnName;
			dgN6.NullText = "-";
			dgN6.Width = 50;
			tsN.GridColumnStyles.Add(dgN6);

			DataGrid2NumberBoxColumn dgN7 = new DataGrid2NumberBoxColumn();
			dgN7.HeaderText = "Množství MJ";
			dgN7.MappingName = _prodejTable.CZMST_DI.QTYSHPPDMJColumn.ColumnName;
			dgN7.NullText = "-";
			dgN7.Width = 50;
			dgN7.Alignment = StringAlignment.Far;
			tsN.GridColumnStyles.Add(dgN7);

			DataGrid2TextBoxColumn dgN8 = new DataGrid2TextBoxColumn();
			dgN8.HeaderText = "È.k.";
			dgN8.MappingName = _prodejTable.CZMST_DI.VNDITNUMColumn.ColumnName;
			dgN8.NullText = "-";
			dgN8.Width = 50;
			tsN.GridColumnStyles.Add(dgN8);

			DataGrid2TextBoxColumn dgN9 = new DataGrid2TextBoxColumn();
			dgN9.HeaderText = "È.k. vlastní";
			dgN9.MappingName = _prodejTable.CZMST_DI.CZ_CarKodColumn.ColumnName;
			dgN9.NullText = "-";
			dgN9.Width = 50;
			tsN.GridColumnStyles.Add(dgN9);

			DataGrid2TextBoxColumn dgN10 = new DataGrid2TextBoxColumn();
			dgN10.HeaderText = "Pracovník ID";
			dgN10.MappingName = _prodejTable.CZMST_DI.PRAC_IDColumn.ColumnName;
			dgN10.NullText = "-";
			dgN10.Width = 25;
			tsN.GridColumnStyles.Add(dgN10);

			DataGrid2TextBoxColumn dgN11 = new DataGrid2TextBoxColumn();
			dgN11.HeaderText = "Støedisko ID";
			dgN11.MappingName = _prodejTable.CZMST_DI.STR_IDColumn.ColumnName;
			dgN11.NullText = "-";
			dgN11.Width = 25;
			tsN.GridColumnStyles.Add(dgN11);

			DataGrid2TextBoxColumn dgN12 = new DataGrid2TextBoxColumn();
			dgN12.HeaderText = "Typ palety";
			dgN12.MappingName = _prodejTable.CZMST_DI.TYPEPALColumn.ColumnName;
			dgN12.NullText = "-";
			dgN12.Width = 25;
			tsN.GridColumnStyles.Add(dgN12);

			DataGrid2TextBoxColumn dgN13 = new DataGrid2TextBoxColumn();
			dgN13.HeaderText = "SSCC palety";
			dgN13.MappingName = _prodejTable.CZMST_DI.NMBRPALColumn.ColumnName;
			dgN13.NullText = "-";
			dgN13.Width = 25;
			tsN.GridColumnStyles.Add(dgN13);

			DataGrid2TextBoxColumn dgN14 = new DataGrid2TextBoxColumn();
			dgN14.HeaderText = "REZ 1";
			dgN14.MappingName = _prodejTable.CZMST_DI.REZ_1Column.ColumnName;
			dgN14.NullText = "-";
			dgN14.Width = 10;
			tsN.GridColumnStyles.Add(dgN14);

			DataGrid2TextBoxColumn dgN15 = new DataGrid2TextBoxColumn();
			dgN15.HeaderText = "REZ 2";
			dgN15.MappingName = _prodejTable.CZMST_DI.REZ_2Column.ColumnName;
			dgN15.NullText = "-";
			dgN15.Width = 10;
			tsN.GridColumnStyles.Add(dgN15);

			DataGrid2TextBoxColumn dgN16 = new DataGrid2TextBoxColumn();
			dgN16.HeaderText = "REZ 3";
			dgN16.MappingName = _prodejTable.CZMST_DI.REZ_3Column.ColumnName;
			dgN16.NullText = "-";
			dgN16.Width = 10;
			tsN.GridColumnStyles.Add(dgN16);

			DataGrid2TextBoxColumn dgN17 = new DataGrid2TextBoxColumn();
			dgN17.HeaderText = "REZ 4";
			dgN17.MappingName = _prodejTable.CZMST_DI.REZ_4Column.ColumnName;
			dgN17.NullText = "-";
			dgN17.Width = 10;
			tsN.GridColumnStyles.Add(dgN17);

			//DataGrid2TextBoxColumn dgN18 = new DataGrid2TextBoxColumn();
			//dgN18.HeaderText = "MENA_ID";
			//dgN18.MappingName = _prodejTable.CZMST_DI.Mena_id.ColumnName;
			//dgN18.NullText = "-";
			//dgN18.Width = 10;
			//tsN.GridColumnStyles.Add(dgN18);



			dataGridNasnimane.TableStyles.Add(tsN);
		}

		/// <summary>
		/// Metoda pro inicializaci popisu v nasnimanich
		/// </summary>
		private void MyInitializePopisItems_DetailNasnimane()
		{
			try
			{
				df_Itemnmbr_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.ITEMNMBRColumn.ColumnName].HeaderText + " :";
				df_SN_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.SERLTNUMColumn.ColumnName].HeaderText + " :";
				df_QTY_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.QTYSHPPDColumn.ColumnName].HeaderText + " :";
				df_QTYPACK_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.QTYPACKColumn.ColumnName].HeaderText + " :";
				//df_Cena_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.AMOUNPIEColumn.ColumnName].HeaderText + " :";
				//df_DPH_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.TAXAMPIEMColumn.ColumnName].HeaderText + " :";
				df_REZ1_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.REZ_1Column.ColumnName].HeaderText + " :";
				df_REZ2_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.REZ_2Column.ColumnName].HeaderText + " :";
				df_REZ3_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.REZ_3Column.ColumnName].HeaderText + " :";
				df_REZ4_DN.Popis = dataGridNasnimane.TableStyles[0].GridColumnStyles[_prodejTable.CZMST_DI.REZ_4Column.ColumnName].HeaderText + " :";
			}
			catch (Exception ex)
			{
				string chyba = ex.Message;
				//throw;
			}
		}

		/// <summary>
		/// Metoda pro inicializaci popisu v detailu
		/// </summary>
		private void MyInitializePopisItems_Detail()
		{
			try
			{
				df_Itemnmbr_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.ITEMNMBRColumn.ColumnName].HeaderText + " :";
				df_Sklad.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.SKL_DESCColumn.ColumnName].HeaderText + " :";
				df_VNDITNUM.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.VNDITNUMColumn.ColumnName].HeaderText + " :";
				df_CZCarKod.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.CZ_CarKodColumn.ColumnName].HeaderText + " :";
				df_QTY_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.QTYColumn.ColumnName].HeaderText + " :";
				df_QTYPACK_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.QTYPACKColumn.ColumnName].HeaderText + " :";
				//df_TAXRATE.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.TAXRATEColumn.ColumnName].HeaderText + " :";
				df_PRICE.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.PRICE0Column.ColumnName].HeaderText + " :";
				df_SNTrack.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.SERLTNUMColumn.ColumnName].HeaderText + " :";
				//df_nasklade_l.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.QTYPACKColumn.ColumnName].HeaderText + " :";
				//df_datumnasklade_l.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.QTYPACKColumn.ColumnName].HeaderText + " :";
				df_WEIGHT.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.WEIGHTColumn.ColumnName].HeaderText + " :";
				df_MJ.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.MJColumn.ColumnName].HeaderText + " :";
				df_REZ1_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.REZ1Column.ColumnName].HeaderText + " :";
				df_REZ2_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.REZ2Column.ColumnName].HeaderText + " :";
				df_REZ3_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.REZ3Column.ColumnName].HeaderText + " :";
				df_REZ4_DN.Popis = dataGrid1.TableStyles[0].GridColumnStyles[_katalogZbozi.CZMST095.REZ4Column.ColumnName].HeaderText + " :";
			}
			catch (Exception ex)
			{
				string chyba = ex.Message;
				//throw;
			}
		}

		/// <summary>
		/// Metoda pro Inicializaci Gridu
		/// </summary>
		private void MyInitializeGrid()
		{
			this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
			this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
			this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));

			this.dataGridNasnimane.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
			this.dataGridNasnimane.Font = new Font(this.dataGridNasnimane.Font.Name, Settings.UIGridFont, this.dataGridNasnimane.Font.Style);
			this.dataGridNasnimane.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_2"));
		}

		#endregion

		#region Menu Click Eventy

		#region Dávka

		/// <summary>
		/// Click Event slouží pro pøidaní nové položky do dávky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemPolozkaPridat_Click(object sender, EventArgs e)
		{
			if (_zobrazeni != ZobrazeniTyp.Nasnimane)
			{
				_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
				pridatPolozku();
			}
			else
				MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListVTomtoZobrazeniNelzeZadatMnozstvi, Fask.Localization.Localization.Prodej3ProdejListInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		}

		/// <summary>
		/// Click Event slouží pro smazani položky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemSmazat_Click(object sender, EventArgs e)
		{
			smazatNasnimanou();
		}

		#region RFID

		/// <summary>
		/// Click Event slouží pro naèteni RFID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miRFIDNacist_Click(object sender, EventArgs e)
		{
			try
			{
				ScannerStop();


				// 1) nasnimat tagy
				Fask.SQLiteDBs.DataSets.Obecne dsNasnimaneTagy = null;
				using (RFID.SnimatRFID snimatRFID = new Fask.MST_W.Prodej_3.RFID.SnimatRFID())
				{
					// cist pouze TagID a ne vsechny pamete, zbytecne pomale
					//snimatRFID.menuItemAllMemories.Checked = true; 

					//snimatRFID.A_RSSIMinimum = -60//???
					if (DialogResult.Cancel == snimatRFID.ShowDialog())
						return;
					dsNasnimaneTagy = snimatRFID.A_DS_Nasnimane;
				}

				// 2) dohledat zbozi z ciselniku zbozi
				// - dle ean, itemnmbr
				//ta_zbozi.Connection.Open();
				//ta_zbozi.GetDataByPolozkacisloSklad(

				// 3) zobrazit prehled zbozi

				#region 16.2.2018 TaD ZZS upravy

				RFID.ZobrazitRFID dsZobrazitRFID = new Fask.MST_W.Prodej_3.RFID.ZobrazitRFID();
				//using (RFID.ZobrazRFID zobrazRFID = new Fask.MST_W.Prodej_3.RFID.ZobrazRFID(_skladZdroj))
				//{
				//    zobrazRFID.A_DS_Nasnimane = dsNasnimaneTagy;
				//    if (DialogResult.Cancel == zobrazRFID.ShowDialog())
				//        return;
				//    dsZobrazitRFID = zobrazRFID.A_DS_ZobrazitRFID;
				//}

				foreach (Fask.SQLiteDBs.DataSets.Obecne.RFIDRow item in dsNasnimaneTagy.RFID)
				{
					Fask.MST_W.ProdejService.Location ds = OnlineGetMaterial(string.Empty, _skladZdroj.skl_id.Trim(), item.ID);
					RFID.ZobrazitRFID.PolozkyRow row = dsZobrazitRFID.Polozky.NewPolozkyRow();
					if (ds.CZMST_SkladLokace_Stav.Count == 1)
					{
						//row.Ean = ds.CZMST_SkladLokace_Stav[0].??
						//TODO trim
						row.ItemDesc = ds.CZMST_SkladLokace_Stav[0].ITEMDESC.Trim();
						row.Itemnmbr = ds.CZMST_SkladLokace_Stav[0].ITEMNMBR.Trim();
						row.mnozstvi = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;
						row.Serialnmbr = ds.CZMST_SkladLokace_Stav[0].SERLTNUM.Trim();


						dsZobrazitRFID.Polozky.AddPolozkyRow(row);
					}
					else
					{
						//je ich vic nebo zadny
						//pokud zadna tak pridat?
						//pokud vic je to chyba?

						//Zde je možnost nahrá do Lok. Mechanizmu novou položku

					}
				}

				#endregion

				Cursor.Current = Cursors.WaitCursor;
				foreach (var item in dsZobrazitRFID.Polozky)
				{
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = _prodejTable.CZMST_DI.NewCZMST_DIRow();
					DateTime dtnow = DateTime.Now;
					Guid newGuid = Guid.NewGuid();
					di.CountEntries = _cislodavky;
					//di.ItemDescription = zbozi.ITEMDESC;
					di.ITEMNMBR = item.Itemnmbr.Trim();
					//TaD resit i lokace? pri zmene skladu?
					di.LOCNCODE = "1";       //string.Empty // TODO : lokace?
					di.LOCNCODEDEST = "1";   //string.Empty; // TODO : lokace
					di.ODB_ID = (_odberatel == null ? string.Empty : _odberatel.odb_id.Trim());
					di.STR_ID = (_stredisko == null ? string.Empty : _stredisko.str_id);
					di.PRAC_ID = (_pracovnik == null ? string.Empty : _pracovnik.prac_id);
					di.DOC_ID = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
					di.DOC_ID2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
					di.QTYPACK = 0; // qtypack ... ???
					di.REZ_1 = string.Empty; //???
					di.REZ_2 = string.Empty; //???
					di.REZ_3 = string.Empty; //???
					di.REZ_4 = string.Empty; //???
					di.SERLTNUM = item.Serialnmbr.Trim();
					di.DATEDONE = dtnow.ToString("yyyyMMdd");
					di.TIMEDONE = dtnow.ToString("HHmmss");
					di.USER_ID = MST_Global.UserID;
					di.guid = newGuid;
					di.ITEMDESC = item.IsItemDescNull() ? string.Empty : item.ItemDesc;
					di.VNDITNUM = item.IsEanNull() ? string.Empty : item.Ean.Trim(); // DBNull
					di.CZ_CarKod = item.IsEanNull() ? string.Empty : item.Ean.Trim();// DBNull
					di.INPUT_MODE = _input_mode;
					// TODO : dohledat???
					di.ITEMCODE = string.Empty; //zbozi.IsITEMCODENull() ? string.Empty : zbozi.ITEMCODE;   // 3.6.2016 PeV: Doplneno, neprobihalo nastaveni ITEMCODE
					di.ID_TERMINAL = MST_Global.TerminalID;
					// TODO : weight dohledat ...???
					//if (zbozi.IsWEIGHTNull())
					//    di.SetWEIGHTNull();
					//else
					//    di.WEIGHT = zbozi.WEIGHT;
					di.WEIGHT = 0;

					// TODO : sklad dest?
					//di.SKL_ID_DEST = sklad_id_dest;

					di.PRINTED = false;
					di.SKL_ID = _skladZdroj.skl_id.Trim();

					//TaD 16.2.2018
					di.SKL_ID_DEST = _skladCil.skl_id.Trim();

					// TODO : merna jednotka ... 
					//di.MJ = zbozi.MJ;
					di.MJ = string.Empty;
					// TODO : v jednotce ...
					di.QTYSHPPDMJ = item.mnozstvi;
					di.QTYSHPPD = item.mnozstvi;

					di.SetEXPIRACENull();

					if (nmbrpal != null)
					{
						if (nmbrpal.Code != null)
							di.NMBRPAL = nmbrpal.Code.Trim();

						di.TYPEPAL = string.Empty;  // TODO: dodelat ...
						// neni implementovano ...
						//if (nmbrpal.Code != null)
						//    di.TYPEPAL = nmbrpal.Type.Trim();
					}

					// TODO : meny doplnit doplneni men a cen se deje jinde v GLobals.zjistcenu, Globals.nastavcenu ...
					//di.mena_ID = _zbozi.MENA_ID;
					//di.mena_IDM = _mena == null ? null : _mena.mena_ID;
					//di.TAXAMPIEM = null;
					//di.AMOUNPIEM = null;

					Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi = null;
					Price price = new Price();
					var zbozipolozky = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.GetDataByPolozkacisloSkladLike(di.ITEMNMBR.Trim(), _skladZdroj.skl_id.Trim());
					if (zbozipolozky.Count() > 0)
						zbozi = zbozipolozky.First();
					if (zbozi != null)
						Prodej.Globals.zjisti_cenu(zbozi, _odberatel, _mena, price); // price je objekt, tedy odkazem => meni se vlastnosti ...
					else
						Logging.Log.Write("Zbozi nenalezeno, ceny nedohledany...");
					Prodej.Globals.nastav_cenu(di, price);

					_prodejTable.CZMST_DI.AddCZMST_DIRow(di);
					Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DI_DirectInsert(di);
				}
				Cursor.Current = Cursors.Default;




				Cursor.Current = Cursors.WaitCursor;
				// 7) Ulozeni rfid dat do vystupu ... RFID ...
				foreach (var i in dsNasnimaneTagy.RFID)
				{
					MST_W.RFID.EPC_SGTIN96 sgtin96;
					MST_W.RFID.USER_512b user512b;



					if (!i.IsEPCNull())
					{
						if (!string.IsNullOrEmpty(i.EPC))
							sgtin96 = new MST_W.RFID.EPC_SGTIN96(i.EPC);
						else
							sgtin96 = new MST_W.RFID.EPC_SGTIN96();
					}
					else
						sgtin96 = new MST_W.RFID.EPC_SGTIN96();

					if (!i.IsUSERNull())
					{
						if (!string.IsNullOrEmpty(i.USER))
							user512b = new MST_W.RFID.USER_512b(i.EPC);
						else
							user512b = new MST_W.RFID.USER_512b();
					}
					else
						user512b = new MST_W.RFID.USER_512b();

					//MST_W.RFID.USER_512b user512b = new MST_W.RFID.USER_512b(i.USER);

					Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Insert_DI_RFID(
						user512b.ItemNumber.Trim(),
						Convert.ToInt32(sgtin96.Serial),
						_skladZdroj == null ? string.Empty : _skladZdroj.skl_id,
						this._cislodavky,
						this._cislodavky.ToString(),
						null,
						user512b.SerltNumber.Trim(),
						Guid.NewGuid(),
						i.IsIDNull() ? "" : i.ID,
						i.IsTIDNull() ? "" : i.TID,
						i.IsEPCNull() ? "" : i.EPC,
						i.IsUSERNull() ? "" : i.USER,
						i.IsRESERVEDNull() ? "" : i.RESERVED,
						i.IsIDNull() ? "" : i.ID,
						i.IsTIDNull() ? "" : i.TID,
						i.IsEPCNull() ? "" : i.EPC,
						i.IsUSERNull() ? "" : i.USER,
						i.IsRESERVEDNull() ? "" : i.RESERVED,
						MST_Global.TerminalID,
						MST_Global.UserID,
						DateTime.Now
						);
				}
				Cursor.Current = Cursors.Default;

				MessageBoxBig.Show("Konec RFID\n" + "Uloženo: " + dsZobrazitRFID.Polozky.Count + " položek\nCelkem: " + dsNasnimaneTagy.RFID.Count + " jednotek.");
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message);
			}
			finally
			{
				ScannerStart();
				UpdateForm();
			}
		}



		#endregion

		#region Tisk

		/// <summary>
		/// Click Event slouží pro tisk Etikety
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemTiskEtiketa_Click(object sender, EventArgs e)
		{
			try
			{
				bool vytisteno = false;
				bool? TiskSCenou = null;
				ScannerStop();
				if (Zobrazeni == ZobrazeniTyp.Detail || Zobrazeni == ZobrazeniTyp.List)
				{
					//  tisk z prvního gridu
					Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row pe = this.SelectedZbozi;
					if (pe == null)
					{
						MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListZaznamNeniVybran, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return;
					}

					if (Prodej.Globals.EtiketaTiskDotazSCenou)
					{
						if (Prodej.Globals.EtiketaTiskDotazSCenou_ZobrazDialog)
						{
							DialogResult dres = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListTiskEtiketaSCenou, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

							if (dres == DialogResult.Yes)
								TiskSCenou = true;
							else { TiskSCenou = false; }
						}
						else
						{
							TiskSCenou = !Prodej.Globals.EtiketaTiskDotazSCenou_Cena;
						}
					}

					//vytisteno = ProdejTisk.Print(pe, MST_Global.PrintServerTemplateNameProdejPredloha);

					vytisteno = ProdejTisk.Print(pe, PrinterFactory.PrinterModules.ProdejPredloha, string.IsNullOrEmpty(Prodej.Globals.PredvyplneneMnozstviEtikety) ? (int?)null : Convert.ToInt32(Prodej.Globals.PredvyplneneMnozstviEtikety), TiskSCenou, pe.CZ_SerNum_Track.ToString());
					Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", null, null, pe.ITEMNMBR, vytisteno.ToString(), null));
				}
				if (Zobrazeni == ZobrazeniTyp.Nasnimane || Zobrazeni == ZobrazeniTyp.DetailNasnimane)
				{
					// tisk z druhého gridu (nasnímané)
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI = this.SelectedNasnimane;
					if (rowDI == null)
					{
						MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListZaznamNeniVybran, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return;
					}

					TiskEtikety(rowDI);

					//vytisteno = ProdejTisk.Print(rowDI, PrinterFactory.PrinterModules.ProdejNasnimane, string.IsNullOrEmpty(Prodej.Globals.PredvyplneneMnozstviEtikety) ? (int?)null : Convert.ToInt32(Prodej.Globals.PredvyplneneMnozstviEtikety));
					Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", rowDI.CountEntries, null, rowDI.ITEMNMBR, vytisteno.ToString(), null));
				}
				//Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", "print", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", pe.CountEntries, pe.PONUMBER, pe.ITEMNMBR, vytisteno.ToString(), null));
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

		/// <summary>
		/// Click Event slouží pro tisk Soupisu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miTiskSoupis_Click(object sender, EventArgs e)
		{
			TiskSoupis();
		}

		private void TiskSoupis()
		{
			try
			{
				//DateTime dtStart = DateTime.Now;

				bool vytisteno = false;
				//Logging.Log.WriteDebug("Start zpracování", "START miTiskSoupis");
				ScannerStop();
				//  tisk soupisu
				Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
				List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
				Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

				// Priprava parametru pro tisk soupisu ... 
				Fask.SQLiteDBs.DataSets.Meny.CZMST097Row menaHlavni = null; //hlavni mena
				Fask.SQLiteDBs.DataSets.Meny.CZMST097Row menaVedlejsi = null; //vedlejsi mena
				//Logging.Log.WriteDebug("START hlavicka");
				// Hlavicka
				dataHlavicka.Add("odb_desc", _odberatel != null ? _odberatel.odb_desc.Trim() : string.Empty);
				dataHlavicka.Add("odb_ico", _odberatel != null && !_odberatel.Isodb_icoNull() ? _odberatel.odb_ico.Trim() : string.Empty);
				dataHlavicka.Add("odb_carcode", _odberatel != null && !_odberatel.Isodb_carcodeNull() ? _odberatel.odb_carcode.Trim() : string.Empty);
				dataHlavicka.Add("odb_cisloOr", _odberatel != null && !_odberatel.Isodb_cisloOrNull() ? _odberatel.odb_cisloOr.Trim() : string.Empty);
				dataHlavicka.Add("odb_dic", _odberatel != null && !_odberatel.Isodb_dicNull() ? _odberatel.odb_dic.Trim() : string.Empty);
				dataHlavicka.Add("odb_Dodavatel", _odberatel != null && !_odberatel.Isodb_DodavatelNull() ? _odberatel.odb_Dodavatel.ToString() : string.Empty);
				dataHlavicka.Add("odb_misto", _odberatel != null && !_odberatel.Isodb_mistoNull() ? _odberatel.odb_misto.Trim() : string.Empty);
				dataHlavicka.Add("odb_Odberatel", _odberatel != null && !_odberatel.Isodb_OdberatelNull() ? _odberatel.odb_Odberatel.ToString() : string.Empty);
				dataHlavicka.Add("odb_psc", _odberatel != null && !_odberatel.Isodb_pscNull() ? _odberatel.odb_psc.Trim() : string.Empty);
				dataHlavicka.Add("odb_ulice", _odberatel != null && !_odberatel.Isodb_uliceNull() ? _odberatel.odb_ulice.Trim() : string.Empty);

				// jsou povolena strediska
				if ((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska)
				{
					dataHlavicka.Add("str_desc", _stredisko != null && !_stredisko.Isstr_descNull() ? _stredisko.str_desc.Trim() : string.Empty);
					dataHlavicka.Add("str_carcode", _stredisko != null && !_stredisko.Isstr_carcodeNull() ? _stredisko.str_carcode.Trim() : string.Empty);
				}

				// nacteni uzivatele
				dataHlavicka.Add("prac_desc", _pracovnik != null ? (_pracovnik.Isprac_descNull() ? string.Empty : _pracovnik.prac_desc.Trim()) : string.Empty);

				var dtUziv = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_users.GetDataByLogin(MST_Global.UserLoginName);
				if (dtUziv.Count > 0)
				{
					dataHlavicka.Add("LOGIN", MST_Global.UserLoginName ?? string.Empty);
					dataHlavicka.Add("FIRSTNAME", dtUziv.First().IsFIRSTNAMENull() ? string.Empty : dtUziv.First().FIRSTNAME.Trim());
					dataHlavicka.Add("SECONDNAME", dtUziv.First().IsSECONDNAMENull() ? string.Empty : dtUziv.First().SECONDNAME.Trim());
				}

				string mena_id = string.Empty;
				string mena_symbol = string.Empty;
				//decimal? mena_kurz;
				//DateTime? mena_kurzDatum;
				//mena_kurz = null;
				//mena_kurzDatum = null;

				string mena_idM = string.Empty;
				string mena_symbolM = string.Empty;
				decimal? mena_kurzM;
				//DateTime? mena_kurzDatumM;
				mena_kurzM = null;
				//mena_kurzDatumM = null;

				//if (_odberatel != null && !_odberatel.Ismena_IDNull() && String.IsNullOrEmpty(mena_id))
				//    mena_idM = _odberatel.mena_ID.Trim();
				//if (_mena != null)
				//{
				//    if (_mena.mena_hlavni)
				//        mena_id = _mena.mena_ID.Trim();
				//    else
				//    {
				//        mena_idM = _mena.mena_ID.Trim();
				//        var dtHlavniMena = ta_meny.GetDataByHlavni(true);
				//        if (dtHlavniMena.Count > 0)
				//            mena_id = dtHlavniMena[0].mena_ID.Trim();
				//    }
				//}

				if (string.IsNullOrEmpty(mena_id))
				{
					var dtHlavniMena = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_meny.GetDataByHlavni(true);
					if (dtHlavniMena.Count > 0)
					{
						menaHlavni = dtHlavniMena[0];
						mena_id = menaHlavni.mena_ID.Trim();
					}
				}

				if (String.IsNullOrEmpty(mena_idM))
				{
					var dtDIData = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DI();
					//int countNullMenaIDM = 0;
					//if ((countNullMenaIDM = dtDIData.Count(di => di.Ismena_IDMNull())) > 0)
					//{
					//    MessageBoxBig.Show("Existuje " + countNullMenaIDM + " položek bez udané mìny!", "Tisk
					//}
					//var dtDIDataMenaM = dtDIData.Where(di => !di.Ismena_IDMNull());
					//dtDIDataMenaM.Count(di => di.Ismena_IDMNull());
					if (dtDIData.Count(di => !di.Ismena_IDMNull()) > 0)
					{
						var drDIMenaM = dtDIData.First(di => !di.Ismena_IDMNull());
						if (drDIMenaM != null)
						{
							mena_idM = drDIMenaM.mena_IDM.Trim();
						}
					}
				}

				if (String.IsNullOrEmpty(mena_idM))
				{
					menaVedlejsi = menaHlavni;
					mena_idM = mena_id;
				}
				else
				{
					var dtMeny = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_meny.GetDataByMenaID(mena_idM);
					if (dtMeny.Count > 0)
					{
						menaVedlejsi = dtMeny.First();
						//mena_idM = menaVedlejsi.mena_ID.Trim(); // <= mena_idM je jiz nastaveno ...
					}
					else
					{
						menaVedlejsi = menaHlavni;
						mena_idM = mena_id;
					}
				}

				// nastaveni kurzu pro vedlejsi menu ...
				if (menaVedlejsi == null)
					mena_kurzM = null;
				else if (menaVedlejsi.mena_hlavni)
					mena_kurzM = 1; // Pokud je hlavni mena, tak se neprepocitava ...
				else if (!menaVedlejsi.Ismena_kurzNull())
					mena_kurzM = menaVedlejsi.mena_kurz;
				else
				{
					// TODO : ??? nastavit kurz ??? => muze se zmenit prepnutim na jinou menu...
					mena_kurzM = null; // defaultne se bude davat nula (0), jako ze neni kurz ...
				}


				//SqlCEDBs.DataSets.Meny.CZMST097Row menarow = null;
				//if (String.IsNullOrEmpty(mena_id))
				//{
				//    Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable menadt = menata.GetDataByHlavni(true);
				//    if (menadt.Count > 0)
				//    {
				//        menarow = menadt[0];
				//        mena_id = menarow.mena_ID.Trim();
				//        if (!menarow.Ismena_kurzNull())
				//            mena_kurz = menarow.mena_kurz;
				//        if (!menarow.Ismena_kurzDatumNull())
				//            mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}
				//else
				//{
				//    Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable menadt = menata.GetDataByMenaID(mena_id);
				//    if (menadt.Count > 0)
				//    {
				//        menarow = menadt[0];
				//        if (!menarow.Ismena_kurzNull())
				//            mena_kurz = menarow.mena_kurz;
				//        if (!menarow.Ismena_kurzDatumNull())
				//            mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}

				//if (menarow != null && !menarow.mena_hlavni)
				//{ //Pokud je nastavena mena a neni hlavni, tak dochazi k prepoctu kurzem do teto meny ...
				//    if (menarow.Ismena_kurzNull())
				//    { //Neni zadany kurz meny
				//        string mena_kurz_str = string.Empty;
				//        while (true)
				//        {
				//            if (DialogResult.Cancel == InputBox.Show("Zadejte kurz mìny '" + menarow.mena_text + "'", mena_kurz_str, out mena_kurz_str))
				//                return;
				//            try
				//            {
				//                menarow.mena_kurz = decimal.Parse(mena_kurz_str);
				//                menarow.mena_kurzDatum = DateTime.Now;
				//                break;
				//            }
				//            catch 
				//            {
				//            }
				//        }
				//        mena_kurz = menarow.mena_kurz;
				//        mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}

				// Mapovani men na zastupne symboly
				mena_symbol = _Translations.Meny.GetSymbol(mena_id);
				mena_symbolM = _Translations.Meny.GetSymbol(mena_idM);

				dataHlavicka.Add("mena_id", mena_id);
				dataHlavicka.Add("mena_symbol", mena_symbol);
				dataHlavicka.Add("mena_idM", mena_idM);
				dataHlavicka.Add("mena_symbolM", mena_symbolM);
				//dataHlavicka.Add("mena_kurz", mena_kurz.HasValue ? mena_kurz.Value.ToString("0.00") : "-");
				//dataHlavicka.Add("mena_kurzDatum", mena_kurzDatum.HasValue ? mena_kurzDatum.Value.ToString() : "-");

				// V jake mene se chce tisknout?
				string mena_id_print = string.Empty;
				if (_odberatel != null && !_odberatel.Ismena_IDNull())
					mena_id_print = _odberatel.mena_ID.Trim();
				if (_mena != null)
					mena_id_print = _mena.mena_ID.Trim();
				dataHlavicka.Add("mena_id_print", mena_id_print);

				// Radky
				// promenne pro paticku
				decimal p_sum_mn = 0; // celkem mnozstvi
				decimal p_tax = 0; //pouzita dan

				decimal p_sum_bdph = 0; // celkem cena bez DPH
				decimal p_sum_sdph = 0; // celkem cena s DPH
				decimal p_sum_dph = 0; // celkem DPH za vse

				decimal p_sum_bdphM = 0; // celkem cena bez DPH
				decimal p_sum_sdphM = 0; // celkem cena s DPH
				decimal p_sum_dphM = 0; // celkem DPH za vse

				// data radku ... 
				Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdi = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
				Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Fill_DI(dtdi);
				//Logging.Log.WriteDebug("KONEC hlavicka");
				//Logging.Log.WriteDebug("START nacitani (foreach) CZMST_DI, pocet zaznamu: " + dtdi.Count);

				//SqlCEDBs.DataSets.Zbozi.CZMST095DataTable dt095 = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
				//ta_zbozi.FillPolozkacisloPopis(dt095);

				if ((dtdi != null) && (dtdi.Count > 0))
				{
					dataHlavicka.Add("CountEntries", dtdi[0].CountEntries.ToString());
				}

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB))
				{
					#region ForeEach

					ConZbo.Connection_Open();

					try
					{
						//int cisloZaznamu = 0;
						foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow drdi in dtdi)
						{
							//++cisloZaznamu;                    
							//Logging.Log.WriteDebug("Zacatek zpracovani zaznamu c. " + cisloZaznamu);

							Dictionary<string, string> dataRadek = new Dictionary<string, string>();
							//dataRadek.Add("index", cisloZaznamu.ToString());
							dataRadek.Add("ITEMNMBR", drdi.ITEMNMBR.Trim());
							dataRadek.Add("CountEntries", drdi.CountEntries.ToString());
							dataRadek.Add("VNDITNUM", drdi.IsVNDITNUMNull() ? string.Empty : drdi.VNDITNUM.Trim());
							dataRadek.Add("CZ_CarKod", drdi.IsCZ_CarKodNull() ? string.Empty : drdi.CZ_CarKod.Trim());

							//dataRadek.Add("ITEMDESC", drdi.ITEMDESC.Trim());
							try
							{
								if (!drdi.IsITEMDESCNull())
								{
									dataRadek.Add("ITEMDESC", drdi.ITEMDESC.Trim());
								}
								else
									dataRadek.Add("ITEMDESC", ConZbo.GetDataByPolozkacisloLike(drdi.ITEMNMBR)[0].ITEMDESC.Trim());
								//dataRadek.Add("ITEMDESC", drdi.ITEMNMBR);

							}
							catch
							{
								dataRadek.Add("ITEMDESC", "?");
							}

							dataRadek.Add("ITEMCODE", drdi.IsITEMCODENull() ? string.Empty : drdi.ITEMCODE.Trim());
							dataRadek.Add("REZ_1", drdi.REZ_1.Trim());
							dataRadek.Add("REZ_2", drdi.REZ_2.Trim());
							dataRadek.Add("MJ", drdi.MJ.Trim());
							dataRadek.Add("QTYSHPPD", drdi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("QTYPACK", drdi.IsQTYPACKNull() ? string.Empty : drdi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("QTYSHPPDMJ", drdi.QTYSHPPDMJ.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("NMBRPAL", drdi.IsNMBRPALNull() ? string.Empty : drdi.NMBRPAL.Trim());

							decimal priceMJTaxWith = 0;       // cena/MJ s DPH[mena]
							decimal priceMJTaxWithout = 0;    // cena/MJ bez DPH[mena]
							decimal priceMJTax = 0;           // MJ DPH[mena]
							decimal priceTaxWith = 0;       // cena s DPH[mena]
							decimal priceTaxWithout = 0;    // cena bez DPH[mena]
							decimal priceTax = 0;           // DPH[mena]
							decimal priceMJTaxWithM = 0;       // cena/MJ s DPH[mena]
							decimal priceMJTaxWithoutM = 0;    // cena/MJ bez DPH[mena]
							decimal priceMJTaxM = 0;           // MJ DPH[mena]
							decimal priceTaxWithM = 0;       // cena s DPH[mena]
							decimal priceTaxWithoutM = 0;    // cena bez DPH[mena]
							decimal priceTaxM = 0;           // DPH[mena]
							decimal Tax = 0;                // vyse DPH[%]
							string MJ = string.Empty;

							MJ = drdi.MJ.Trim();

							if (!drdi.IsWITHTAXNull())
							{
								if (drdi.WITHTAX == 1)
								{
									priceMJTaxWith = drdi.AMOUNPIE;
									priceMJTaxWithout = drdi.AMOUNPIE - drdi.TAXAMPIE;
									priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD;
									priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD - drdi.TAXAMPIE * drdi.QTYSHPPD;

									if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
										&& mena_idM == drdi.mena_IDM.Trim())
									{ // je mena a je cena za kus a je dan za kus, tak spocitam toto
										// a mena se shoduje s touto menou ... 
										priceMJTaxWithM = drdi.AMOUNPIEM;
										priceMJTaxWithoutM = drdi.AMOUNPIEM - drdi.TAXAMPIEM;
										priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
										priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD - drdi.TAXAMPIEM * drdi.QTYSHPPD;
									}
									else
									{ // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
										// pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
										priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
										priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
										priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
										priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
									}
								}
								else
								{
									priceMJTaxWithout = drdi.AMOUNPIE;
									priceMJTaxWith = drdi.AMOUNPIE + drdi.TAXAMPIE;
									priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD;
									priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD + drdi.TAXAMPIE * drdi.QTYSHPPD;

									if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
										&& mena_idM == drdi.mena_IDM.Trim())
									{
										priceMJTaxWithoutM = drdi.AMOUNPIEM;
										priceMJTaxWithM = drdi.AMOUNPIEM + drdi.TAXAMPIEM;
										priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
										priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD + drdi.TAXAMPIEM * drdi.QTYSHPPD;
									}
									else
									{ // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
										// pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
										priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
										priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
										priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
										priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
									}
								}
								priceMJTax = priceMJTaxWith - priceMJTaxWithout;
								priceTax = priceTaxWith - priceTaxWithout;

								priceMJTaxM = priceMJTaxWithM - priceMJTaxWithoutM;
								priceTaxM = priceTaxWithM - priceTaxWithoutM;

								Tax = Math.Round((priceTax / (priceTaxWithout == 0 ? 1 : priceTaxWithout)) * 100);

								////Prepocet do meny
								//if (menarow != null && !menarow.mena_hlavni)
								//{ //Pokud je nastavena mena a neni hlavni, tak dochazi k prepoctu kurzem do teto meny ...
								//    priceMJTax = priceMJTax * menarow.mena_kurz;
								//    priceMJTaxWith = priceMJTaxWith * menarow.mena_kurz;
								//    priceMJTaxWithout = priceMJTaxWithout * menarow.mena_kurz;
								//    priceTax = priceTax * menarow.mena_kurz;
								//    priceTaxWith = priceTaxWith * menarow.mena_kurz;
								//    priceTaxWithout = priceTaxWithout * menarow.mena_kurz;
								//}

								// Cena MJ
								dataRadek.Add("pricemjtaxwith", priceMJTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxwithout", priceMJTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtax", priceMJTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena MJ v mene
								dataRadek.Add("pricemjtaxwithM", priceMJTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxwithoutM", priceMJTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxM", priceMJTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena celkem za radek
								dataRadek.Add("pricetaxwith", priceTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxwithout", priceTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetax", priceTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena celkem za radek v mene
								dataRadek.Add("pricetaxwithM", priceTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxwithoutM", priceTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxM", priceTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Sazba dane
								dataRadek.Add("tax", Tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));

								dataRadek.Add("mena_id", mena_id);
								dataRadek.Add("mena_symbol", mena_symbol);
								dataRadek.Add("mena_idM", mena_idM);
								dataRadek.Add("mena_symbolM", mena_symbolM);

								// V jake mene se bude tisknout
								dataRadek.Add("mena_id_print", mena_id_print);
							}
							dataRadky.Add(dataRadek);

							//vypocet sum pro paticku ...
							p_sum_mn += drdi.QTYSHPPD;
							p_tax = Tax;

							p_sum_bdph += priceTaxWithout;
							p_sum_sdph += priceTaxWith;
							p_sum_dph += priceTax;

							p_sum_bdphM += priceTaxWithoutM;
							p_sum_sdphM += priceTaxWithM;
							p_sum_dphM += priceTaxM;
						}
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
					}
					finally
					{
						ConZbo.Connection_Close();
					}
					#endregion
				}

				//Logging.Log.WriteDebug("KONEC nacitani (foreach) CZMST_DI");

				// Paticka
				dataPaticka.Add("sum_mn", p_sum_mn.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("tax", p_tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));
				// Suma
				dataPaticka.Add("sum_bdph", p_sum_bdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_sdph", p_sum_sdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_dph", p_sum_dph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				// Suma v mene
				dataPaticka.Add("sum_bdphM", p_sum_bdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_sdphM", p_sum_sdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_dphM", p_sum_dphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

				dataPaticka.Add("datetime", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("mena_id", mena_id);
				dataPaticka.Add("mena_symbol", mena_symbol);
				dataPaticka.Add("mena_idM", mena_idM);
				dataPaticka.Add("mena_symbolM", mena_symbolM);
				//dataPaticka.Add("mena_kurz", mena_kurz.HasValue ? mena_kurz.Value.ToString("0.00") : "-");
				//dataPaticka.Add("mena_kurzDatum", mena_kurzDatum.HasValue ? mena_kurzDatum.Value.ToString() : "-");

				// V jake mene se bude tisknout
				dataPaticka.Add("mena_id_print", mena_id_print);

				vytisteno = ProdejTisk.PrintSoupisSendToPrinter(dataHlavicka, dataRadky, dataPaticka, string.IsNullOrEmpty(Prodej.Globals.PredvyplneneMnozstviSoupisu) ? (int?)null : Convert.ToInt32(Prodej.Globals.PredvyplneneMnozstviSoupisu));
				Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", null, null, null, vytisteno.ToString(), null));
				//TimeSpan tsDiff = dtStart - DateTime.Now;
				//Logging.Log.WriteDebug("Konec zpracování, celkový èas: " + tsDiff.ToString(), "KONEC miTiskSoupis");
				//MessageBoxBig.Show("Konec zpracování, celkový èas: " + tsDiff.ToString() , this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

		/// <summary>
		/// Click Event slouží pro tisk Palety
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miTiskPaleta_Click(object sender, EventArgs e)
		{
			try
			{
				if (MST_Global.PovolitPrintServer && ((_typdokladu != null && !_typdokladu.Iscfg_tisk_paletyNull() && _typdokladu.cfg_tisk_palety > 0)))
					PerformTiskPaleta();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex, "miTiskPaleta_Click");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}

		/// <summary>
		/// Click Event slouží pro prepnuti tisku Etikety zda se ma tisknout nebo ne
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mi_TiskEtiketaAnoNE_Click(object sender, EventArgs e)
		{
			Prodej.Globals.TiskEtiketyPoPridaniZbozi = !Prodej.Globals.TiskEtiketyPoPridaniZbozi;
			mi_TiskEtiketaAnoNE.Checked = Prodej.Globals.TiskEtiketyPoPridaniZbozi;
			UpdateForm();
		}

		#endregion

		#region Online

		/// <summary>
		/// Click Event slouží pro online dohledani poètu kusù
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem8_Click(object sender, EventArgs e)
		{
			DetailPocetKusu();
		}

		/// <summary>
		/// Click Event slouží pro online dohledani poètu kusù na sklade
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem10_Click(object sender, EventArgs e)
		{
			DetailPocetKusuLokace();
		}

		/// <summary>
		/// Click Event slouží pro online dohledani detailu o položke
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem11_Click(object sender, EventArgs e)
		{
			DetailItemnumber();
		}

		#endregion

		#region Najít

		/// <summary>
		/// Click Event slouží pro najdeni položky podle èárového kodu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemHledatCarovyKod_Click(object sender, EventArgs e)
		{
			najdiCarovyKod();
		}

		/// <summary>
		/// Click Event slouží pro najdeni položky podle názvu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemHledatNazev_Click(object sender, EventArgs e)
		{
			najdiNazev();
		}

		/// <summary>
		/// Click Event slouží pro najdeni položky podle ID položky / ITEMNMBR
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemHledatPolozkaCislo_Click(object sender, EventArgs e)
		{
			najdiPolozkaCislo();
		}

		/// <summary>
		/// Click Event slouží pro najdeni položky podle Kodu položky / ITEMCODE
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem16_Click(object sender, EventArgs e)
		{
			najdiKodPolozky();
		}

		/// <summary>
		/// Click Event slouží pro najdeni položky podle RFID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemNajitRFID_Click(object sender, EventArgs e)
		{
			try
			{
				ScannerStop();

				using (RFID.SnimatRFID snimatRFID = new Fask.MST_W.Prodej_3.RFID.SnimatRFID())
				{
					snimatRFID.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				ScannerStart();
			}
		}

		/// <summary>
		/// Click Event slouží pro najdeni položky podle... zobrazi proste vše...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemVse_Click(object sender, EventArgs e)
		{
			LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);
		}

		#endregion

		#region Setøídit

		/// <summary>
		/// Click Event slouží pro puvodni setøizení
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemSortOriginal_Click(object sender, EventArgs e)
		{
			SortDefault();
		}

		/// <summary>
		/// Click Event slouží pro setøizeni podle názvu / ITEMDESC
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem3_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
			SortItemdesc();
			Cursor.Current = Cursors.Default;
		}

		/// <summary>
		/// Click Event slouží pro setøizeni podle ID položky / ITEMNMBR
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem4_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
			SortItemnmbr();
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region Filtr

		/// <summary>
		/// Click Event slouží pro filtrovani podle názvu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemFiltrNazev_Click(object sender, EventArgs e)
		{
			filtrNazev();
		}

		#endregion

		#region Zobrazení

		/// <summary>
		/// Click Event slouží pro zobrazení Listu položek
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemList_Click(object sender, EventArgs e)
		{
			zobrazeniList();
		}

		/// <summary>
		/// Click Event slouží pro zobrazení detailu položek
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemDetail_Click(object sender, EventArgs e)
		{
			zobrazeniDetail();
		}

		/// <summary>
		/// Click Event slouží pro zobrazení vloženích položek do dávky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemPolozkaVlozene_Click(object sender, EventArgs e)
		{
			zobrazeniNasnimane();
		}

		/// <summary>
		/// Click Event slouží pro zobrazení Stavu prodeje
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemStavProdeje_Click(object sender, EventArgs e)
		{
			ShowStavProdeje();
		}

		/// <summary>
		/// Click Event slouží pro zobrazení Sumaøe
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miZobrazeniSumar_Click(object sender, EventArgs e)
		{
			try
			{
				this.ScannerStop();

				using (Prodej_3.ProdejSumar sum = new Prodej_3.ProdejSumar())
				{
					sum.NMBRPAL = nmbrpal == null ? string.Empty : nmbrpal.Code.Trim();
					sum.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				this.ScannerStart();
			}
		}

		/// <summary>
		/// Click Event slouží pro zobrazení Sumaøe vybrane položky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miZobrazeniSumarPolozky_Click(object sender, EventArgs e)
		{
			try
			{
				this.ScannerStop();

				using (Prodej_3.ProdejSumar sum = new Prodej_3.ProdejSumar())
				{
					sum.NMBRPAL = (this.SelectedNasnimane == null || this.SelectedNasnimane.IsNMBRPALNull()) ? string.Empty : this.SelectedNasnimane.NMBRPAL.Trim();
					sum.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				this.ScannerStart();
			}
		}

		/// <summary>
		/// Click Event slouží pro zobrazeni/ skriti detailu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemZobrazeniDetailu_Click(object sender, EventArgs e)
		{
			menuItemZobrazeniDetailu.Checked = !menuItemZobrazeniDetailu.Checked;
			panelNasnimaneDetail.Visible =
				panelZboziDetail.Visible = menuItemZobrazeniDetailu.Checked;
		}

		#endregion

		/// <summary>
		/// Click Event slouží pro vyp/zap kontinualneho scanovani
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem20_Click(object sender, EventArgs e)
		{
			mi_KonScan.Checked = !mi_KonScan.Checked;
			Program.mstw.Scanner.ContinuousRead = mi_KonScan.Checked;
		}

		/// <summary>
		/// Click Event slouží pro ukonèení okna
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem2_Click(object sender, EventArgs e)
		{
			PerformKonec();
		}

		#endregion

		#region Zmìnit

		/// <summary>
		/// Click Event slouží pro zmenu èísla palety
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem13_Click(object sender, EventArgs e)
		{
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DIH();

			string paletaID = string.Empty;
			string skladID = string.Empty;
			if (dih_dt.Count > 0)
			{
				paletaID = dih_dt[0].Paleta_ID;
				skladID = dih_dt[0].SKL_ID;
			}

			if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejListZadejteCisloPalety, paletaID, out paletaID, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
			{
				string zakazkaID = "";
				if (dih_dt.Count > 0)
				{
					zakazkaID = dih_dt[0].Zakazka_ID;
					Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DeleteQuery_DIH();
				}

				Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, "", skladID, _cislodavky);
			}
			else
			{
				return;
			}
		}

		/// <summary>
		/// Click Event slouží pro zmìnu èísla zakazky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem14_Click(object sender, EventArgs e)
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DIH();

			string zakazkaID = string.Empty;
			string skladID = string.Empty;

			if (dih_dt.Count > 0)
			{
				zakazkaID = dih_dt[0].Zakazka_ID;
				skladID = dih_dt[0].SKL_ID;
			}

			if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejListZadejteCisloZakazky, zakazkaID, out zakazkaID, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
			{
				string idPaleta = "";
				if (dih_dt.Count > 0)
				{
					idPaleta = dih_dt[0].Paleta_ID;
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DeleteQuery_DIH();
				}

				Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Insert_DIH(zakazkaID, idPaleta, "", skladID, _cislodavky);
			}
			else
			{
				return;
			}
		}

		/// <summary>
		/// Click Event slouží pro zmìnu mìny
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem15_Click(object sender, EventArgs e)
		{
			
			try
			{
				ScannerStop();

				using (ProdejVyberMeny po = new ProdejVyberMeny())
				{
					if (_mena != null)
					{
						po.SelectedMenaID = _mena.mena_ID;
					}
					if (po.ShowDialog() == DialogResult.Cancel)
						return;

					if (po.bezCiziMeny)
						_mena = null;
					else
						_mena = po.Mena;

				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
			finally
			{
				ScannerStart();
			}



			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DIH();
			string zakazkaID = "";
			string idPaleta = "";
			string skladID = "";
			if (dih_dt.Count > 0)
			{
				zakazkaID = dih_dt[0].Zakazka_ID;
				idPaleta = dih_dt[0].Paleta_ID;
				skladID = dih_dt[0].SKL_ID;
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DeleteQuery_DIH();
			}
			Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Insert_DIH(zakazkaID, idPaleta, _mena == null ? "" : _mena.mena_ID, skladID, _cislodavky);

		}

		/// <summary>
		/// Click Event slouží pro zmenu Strediska
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemStredisko_Click(object sender, EventArgs e)
		{
			Fask.SQLiteDBs.DataSets.Strediska.CZMST091Row tmpStredisko = _stredisko;
			_stredisko = null;
			if (!StrediskoSet()) //pokud se stredisko nenastavi, tak vratit puvodni...
			{
				_stredisko = tmpStredisko;
			}
		}

		/// <summary>
		/// Click Event slouží pro zmenu Pracovnika
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemPracovnici_Click(object sender, EventArgs e)
		{
			Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096Row tmpPracovnik = _pracovnik;
			_pracovnik = null;
			if (!PracovniciSet()) //pokud se pracovnik nenastavi, tak vratit puvodni...
			{
				_pracovnik = tmpPracovnik;
			}
		}

		#region Paleta

		/// <summary>
		/// Click Event slouží pro vygenerovani noveho èisla palety
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miPaletaGenerovat_Click(object sender, EventArgs e)
		{
			try
			{
				PerformGenerovatCisloPalety();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, miPaletaGenerovat_Click");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
		}

		/// <summary>
		/// Click Event slouží pro zmenu èisla palety
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void miPaletaZmenit_Click(object sender, EventArgs e)
		{
			try
			{
				PerformZmenitCisloPalety();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformGenerovatCisloPalety");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
		}

		#endregion

		/// <summary>
		/// Click Event slouží pro zmenu šarže
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemSarze_Click(object sender, EventArgs e)
		{
			SarzeSet();
		}

		/// <summary>
		/// Click Event slouží pro zmenu Skladu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem18_Click(object sender, EventArgs e)
		{
			try
			{
				PerformZmenaSklad();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex, "miSklad_Click");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}

		/// <summary>
		/// Click Event slouží pro zmenu režimu snimani
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mi_RezimSNimani_Click(object sender, EventArgs e)
		{
			Prodej.Globals.Mnozstvi1Auto = !Prodej.Globals.Mnozstvi1Auto;
			mi_RezimSNimani.Checked = Prodej.Globals.Mnozstvi1Auto;
		}

		#endregion

		#endregion

		#region Private metody s logikou

		#region Dávka

		/// <summary>
		/// Metoda pro pridaní vybrané polozky v Datagridu
		/// </summary>
		private void pridatPolozku()
		{
			_zbozi = this.SelectedZbozi;
			pridatPolozku(_zbozi);
		}

		/// <summary>
		/// Metoda pro pridaní polozky rádek zboží
		/// </summary>
		/// <param name="zbozi"></param>
		private void pridatPolozku(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi)
		{
			pridatPolozku(zbozi, null);
		}

		/// <summary>
		/// Metoda pro pridani položky 
		/// TADY je cela logika volneho pohybu,
		/// TODO rozudelit ma menší submetody aby to bylo prehlednejší
		/// </summary>
		/// <param name="zbozi"></param>
		/// <param name="code"></param>
		private void pridatPolozku(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, BaseCode code)
		{
			#region Kontrola, zda je mozne vybirat i necim jinacim nez scannerem

			if (!InputModeChecker.checkInputMode(Prodej.Globals.PolozkyVyberJenScannerem, _input_mode))
			{
				MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkuJdeZadatPouzeScannerem, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
				return;
			}

			#endregion

			#region Kontrola existence objektu zbozi

			if (zbozi == null)
			{
				MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListNeniVybranoZbozi, Fask.Localization.Localization.Prodej3ProdejListPridatPolozku, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
				return;
			}

			#endregion

			#region kontrola ExistenceNasnimanePolozky

			if (Prodej.Globals.ExistenceNasnimanePolozky)
			{
				if (_prodejTable.CZMST_DI.Any(x => x.CZ_CarKod == zbozi.CZ_CarKod))
				{
					//TODO lokalizovat
					string message = "Položka již byla nasnímana." + Environment.NewLine + "Pokraèovat?";
					if (MessageBoxBig.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
						return;
				}
			}

			#endregion

			#region Parametry + okna

			string rez1 = string.Empty;
			string rez2 = string.Empty;
			string rez3 = string.Empty;
			string rez4 = string.Empty;
			string sklad_id = string.Empty;
			string sklad_id_dest = string.Empty;

			if (skf == null) skf = new SejmiKodForm();

			#endregion

			try
			{
				ScannerStop();

				#region Sklad / Zdrojovy sklad

				if ((_typdokladu != null && !_typdokladu.Iscfg_skladyNull() && _typdokladu.cfg_sklady > 0) || Prodej.Globals.PouzitSklady)
				{
					// na vystup se ulozi a proceduram se da id tohoto skladu
					sklad_id = _skladZdroj.skl_id.Trim();
				}
				else
				{
					// posle se id skladu ze zbozi
					sklad_id = zbozi.IsSKL_IDNull() ? string.Empty : zbozi.SKL_ID;
				}

				#endregion

				#region Cilovy sklad

				// ma se prevzit id skladu
				if (_typdokladu != null && !_typdokladu.Iscfg_skl_id_dest_prevzitNull() && _typdokladu.cfg_skl_id_dest_prevzit > 0)
				{
					sklad_id_dest = sklad_id;
				}
				else if (_skladCil != null) // je vybran cilovy sklad
				{
					sklad_id_dest = _skladCil.skl_id;
				}

				#endregion

				#region Parametry lokalne

				decimal qty = 0;
				string sn = string.Empty;
				DateTime? expirace = null;
				string sarza = string.Empty;

				#endregion

 				#region sarze z parsovaneho / ze zbozi

				// 27.10.2017 JiS => nastaveni SN z parsnute sarze ...
				//if (code is BarcodeSlashSarze)
				//    sn = ((BarcodeSlashSarze)code).sarze;
				//else
				//    sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();

				if (zbozi.CZ_SerNum_Track == 1)
				{
					if (
						_typdokladu != null &&
						!_typdokladu.Iscfg_sn_ONOFFNull() &&
						_typdokladu.cfg_sn_ONOFF > 0
						)
					{
						if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSerialNumber) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN))
							sn = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
						else
							sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();
					}
				}
				else if (zbozi.CZ_SerNum_Track == 2)
				{
					if (
						_typdokladu != null &&
						!_typdokladu.Iscfg_sarze_ONOFFNull() &&
						_typdokladu.cfg_sarze_ONOFF > 0
						)
					{
						if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSarze) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
							sn = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
						else
							sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();
					}

				}
				else if (zbozi.CZ_SerNum_Track == 10)
				{
					if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSarze) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
						sarza = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
					else
						sarza = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();

					if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSerialNumber) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN))
						sn = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
					else
						sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();
				}
				else if (zbozi.CZ_SerNum_Track == 11)
				{
					if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSarze) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
						sarza = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
					else
						sarza = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();

					if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeSerialNumber) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN))
						sn = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;
					else
						sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();
				}

				#endregion

				#region Lokace ze zbozi

				string locncode = zbozi.LOCNCODE; //28.6.2013 JiS: oprava melo by byt z zbozi a ne _zbozi

				#endregion

				#region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)
				// nacteni skladu online ...
				if (Prodej.Globals.NacistSkladIDOnline)
				{
					MST_W.ProdejService.STATUS status = OnlineGetSklad(_typdokladu != null ? _typdokladu.doc_id : string.Empty, zbozi.ITEMNMBR, zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM, out sklad_id, out sklad_id_dest);
					if (status == Fask.MST_W.ProdejService.STATUS.ERROR) // chyba, ukoncit ...
						return;
					else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
					{
						MessageBoxBig.Show(string.Format("Nepodaøilo se naèíst ID skladu online pro materiál '{0}'", zbozi.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						return;
					}
				}
				#endregion

				#region Doporucene palety

				if ((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety)
				{
					Fask.MST_W.ProdejService.Location ds = OnlineGetMaterial(zbozi.ITEMNMBR, sklad_id, sn);
					if (ds != null)
					{
						// vratily se nejake zaznamy
						if (ds.CZMST_SkladLokace_Stav.Count > 1)
						{
							using (ProdejVyberMaterialuList pvpl = new ProdejVyberMaterialuList(ds, _typdokladu.doc_id))
							{
								if (pvpl.ShowDialog() == DialogResult.OK)
								{
									zbozi.QTY = qty = pvpl.Qtyshppd;
									zbozi.SERLTNUM = sn = pvpl.Serltnum.Trim();
									zbozi.LOCNCODE = locncode = pvpl.Locncode.Trim();
									//zbozi.EXPIRACE = expirace = pvpl.Expirace;
								}
							}
						}
						else if (ds.CZMST_SkladLokace_Stav.Count == 1)
						{
							zbozi.QTY = qty = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;
							zbozi.SERLTNUM = sn = ds.CZMST_SkladLokace_Stav[0].SERLTNUM;
							zbozi.LOCNCODE = locncode = ds.CZMST_SkladLokace_Stav[0].LOCNCODE;
							//zbozi.EXPIRACE = expirace = ds.CZMST_SkladLokace_Stav[0].EXPIRATION;
						}
						else
						{
							// nebyl nalezen material, dotaz zdali presto pokracovat ...
							DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListDoporucenePaletyNenalezenyPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
							if (dr == DialogResult.No)
								return;
						}
					}
					else
					{
						// data se nepodarila nacist ze serveru, dotaz zdali pokracovat
						DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListKomunikaceServeruProblemPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
						if (dr == DialogResult.No)
							return;
					}
				}

				#endregion

				#region SN na davku

				// prednastaveni SN, pokud je povoleno
				if (_typdokladu != null && !_typdokladu.Iscfg_sn_na_davkuNull() && _typdokladu.cfg_sn_na_davku > 0)
					zbozi.SERLTNUM = sn = this.serltnum;

				#endregion

				#region Lokace z typu dokladu

				// pokud je vyplneno locncode, automaticky se pouzije ... (nehledne na povoleni zadani zdrojove lokace
				if (_typdokladu != null && !string.IsNullOrEmpty(_typdokladu.LOCNCODE.Trim()))
					locncode = _typdokladu.LOCNCODE.Trim();

				#endregion

				#region Lokace pred SN

				////string locncode = string.Empty;
				////string locncode = _zbozi.LOCNCODE; //25.4.2012 JiS: locncode se prednastavi z vybraneho zbozi
				//string locncode = zbozi.LOCNCODE; //28.6.2013 JiS: oprava melo by byt z zbozi a ne _zbozi
				if (Prodej.Globals.prodejZadaniLocncodePredSN && ((_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0) || Prodej.Globals.prodejPovolitZadaniLocncode))
				{
					if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
					{
						// rucni vyber lokace, pokud neni jiz zvolen
						using (Forms.FormLokaceVyber fsv = new FormLokaceVyber(sklad_id))
						{
							//fsv.Text = "Výbìr lokace";
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							locncode = fsv.Lokace.LOCNCODE;

							if (locncode == null)
							{
								Logging.Log.Write("Není vybrána zdrojová lokace z èíselníku, pøestože je vyžadována!");
								return;
							}
						}
					}
					else //if (!string.IsNullOrEmpty(locncode.Trim()))                 
					{
						locncode = SejmiLocncode(zbozi, true);
						if (locncode == "!@")
							return;
					}
				}

				#endregion

				#region Sledovani na Množství CZ_SerNum_Track == 0

				if (zbozi.CZ_SerNum_Track == 0) //sledovano na mnozstvi
				{
					if (!Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
					{
						WeightCode wc = (WeightCode)code;

						//qty = decimal.Parse(naplnpMnozstvi.Kod);
						qty =
							(wc.weight
							/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
							/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
							);
					}
					if (!Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode_12)
					{
						WeightCode_12 wc = (WeightCode_12)code;

						//qty = decimal.Parse(naplnpMnozstvi.Kod);
						qty =
							(wc.weight
							/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
							/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
							);
					}
					else if ((_typdokladu != null && !_typdokladu.Iscfg_mnozstvi_ze_zboziNull() && _typdokladu.cfg_mnozstvi_ze_zbozi > 0) && zbozi.QTY > 0)     // prevzit mnozstvi 
					{
						qty = zbozi.QTY;
					}
					else
					{
						if (naplnpMnozstvi == null) naplnpMnozstvi = new ProdejPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", null, null, Prodej.Globals.PovolitZadaniMnozstviScannerem);
						bool baleni = zbozi.QTYPACK > 0;
						naplnpMnozstvi.Odberatel = _odberatel;
						naplnpMnozstvi.Zbozi = zbozi;
						naplnpMnozstvi.Serltnum = sn;
						naplnpMnozstvi.Text = baleni ? Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
						naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
						naplnpMnozstvi.Volajici = Volajici_ProdejPridatPolozku.QTY;

						if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeQuantity) && ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
						{
							naplnpMnozstvi.Kod = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString();
						}						
						else if (Prodej.Globals.Mnozstvi1Auto)
						{
							naplnpMnozstvi.Kod = "1";
						}
						else
						{
							if (Prodej.Globals.MnozstviREZ1Vypln && !zbozi.IsREZ1Null())
							{
								naplnpMnozstvi.Kod = zbozi.REZ1.Trim();
							}
							else if (code is WeightCode)
							{
								WeightCode wc = (WeightCode)code;
								naplnpMnozstvi.Kod =
									(wc.weight
									/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
									/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
									).ToString(Settings.UIFormatDesCisel);
							}
							else if (code is WeightCode_12)
							{
								WeightCode_12 wc = (WeightCode_12)code;
								naplnpMnozstvi.Kod =
									(wc.weight
									/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
									/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
									).ToString(Settings.UIFormatDesCisel);
							}
							else if (_typdokladu != null && !_typdokladu.Iscfg_predvyplnit_mnozstviNull() && _typdokladu.cfg_predvyplnit_mnozstvi > 0)   //_typdokladu.cfg_predvyplnit_mnozstvi >0 .... dodelat
								//else if (((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety) && qtyDopPal != 0)
								naplnpMnozstvi.Kod = zbozi.QTY.ToString(Settings.UIFormatDesCisel);
							else
								naplnpMnozstvi.Kod = "";

							naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
							if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
								return;
						}

						if (Prodej.Globals.KontrolaStavuSkladu)     // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
						{
							if (Convert.ToDecimal(naplnpMnozstvi.Kod) > zbozi.QTY)
							{
								if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz, Fask.Localization.Localization.Prodej3ProdejListInfo, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
								{
									return;
								}
							}
						}

						qty = decimal.Parse(naplnpMnozstvi.Kod);
					}
				}
				#endregion

				#region Sledovani na Sarze/SN CZ_SerNumTrack == 1 || 2

				else if ((zbozi.CZ_SerNum_Track == 1) || (zbozi.CZ_SerNum_Track == 2)) //sledovano na seriova cisla
				{
					if (!Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
					{
						WeightCode wc = (WeightCode)code;

						//qty = decimal.Parse(naplnpMnozstvi.Kod);
						qty =
							(wc.weight
							/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
							/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
							);
					}
					if (!Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode_12)
					{
						WeightCode_12 wc = (WeightCode_12)code;

						//qty = decimal.Parse(naplnpMnozstvi.Kod);
						qty =
							(wc.weight
							/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
							/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
							);
					}
					else if ((_typdokladu != null && _typdokladu.cfg_mn2sn > 0) || Prodej.Globals.Vnditnum2Serltnum)
					{
						qty = 1;
						//Pokud se toto vklada, je nutne zmensit rozsah na delku sn( char 21)
						//sn = _zbozi.VNDITNUM;
						string vnditnum = zbozi.VNDITNUM.Trim();
						try
						{
							if (vnditnum.Length > (int)Fask.SQLiteDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength)
								sn = vnditnum.Substring(0, (int)Fask.SQLiteDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength);
							else
								sn = vnditnum;
						}
						catch { }
					}
					else
					{
						if ((zbozi.CZ_SerNum_Track == 2) && (code != null) && (code is Parsing.Codes.Interfaces.ICodeSarze) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
							sn = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
						else
						{
							#region zbozi.CZ_SerNum_Track == 1

							if (zbozi.CZ_SerNum_Track == 1)
							{
								if (
									_typdokladu != null &&
									!_typdokladu.Iscfg_sn_ONOFFNull() &&
									_typdokladu.cfg_sn_ONOFF > 0
								   )
								{
									string snOUT = string.Empty;

									var dr = Get_SN_Sarze_AtributToSN(out snOUT, 
										sn, 
										zbozi, 
										Fask.Localization.Localization.Prodej3ProdejListSerioveCislo, 
										Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo,
										false
										);

									if (dr == DialogResult.Cancel)
										return;
									else
										sn = snOUT;

									//if (naplnpSerialNumber == null) 
									//    naplnpSerialNumber = new ProdejPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListSerioveCislo, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", null, null);
									//naplnpSerialNumber.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
									//naplnpSerialNumber.Odberatel = _odberatel;
									//naplnpSerialNumber.Zbozi = zbozi;
									//naplnpSerialNumber.Popis = Fask.Localization.Localization.Prodej3ProdejListSerioveCislo;
									//naplnpSerialNumber.Text = Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo;
									//naplnpSerialNumber.Len = (decimal)(zbozi.CZ_SerNum_Delka == 0 ? (_typdokladu.cfg_delka_SN != null ? _typdokladu.cfg_delka_SN : 0) : zbozi.CZ_SerNum_Delka);
									//naplnpSerialNumber.Serltnum = sn;
									//naplnpSerialNumber.Kod = sn;
									//naplnpSerialNumber.Volajici = Volajici_ProdejPridatPolozku.SERLTNUM;

									//if (naplnpSerialNumber.ShowDialog() == DialogResult.Cancel)
									//    return;

									//sn = naplnpSerialNumber.Kod;
								}
							}

							#endregion

							#region zbozi.CZ_SerNum_Track == 2

							else if (zbozi.CZ_SerNum_Track == 2)
							{
								if (
									_typdokladu != null &&
									!_typdokladu.Iscfg_sarze_ONOFFNull() &&
									_typdokladu.cfg_sarze_ONOFF > 0
								   )
								{

									string snOUT = string.Empty;

									var dr = Get_SN_Sarze_AtributToSN(out snOUT,
										sn,
										zbozi,
										Fask.Localization.Localization.Prodej3ProdejListSarze,
										Fask.Localization.Localization.Prodej3ProdejListVlozteSarze,
										false
										);

									if (dr == DialogResult.Cancel)
										return;
									else
										sn = snOUT;


									//if (naplnpSerialNumber == null) naplnpSerialNumber = new ProdejPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListSarze, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", null, null);
									//naplnpSerialNumber.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
									//naplnpSerialNumber.Odberatel = _odberatel;
									//naplnpSerialNumber.Zbozi = zbozi;
									//naplnpSerialNumber.Popis =  Fask.Localization.Localization.Prodej3ProdejListSarze;
									//naplnpSerialNumber.Text =  Fask.Localization.Localization.Prodej3ProdejListVlozteSarze;
									////naplnpSerialNumber.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength;
									//naplnpSerialNumber.Len = (decimal)(zbozi.CZ_SerNum_Delka == 0 ? (_typdokladu.cfg_delka_SN != null ? _typdokladu.cfg_delka_SN : 0) : zbozi.CZ_SerNum_Delka);
									//naplnpSerialNumber.Serltnum = sn;
									////naplnpSerialNumber.Kod = "";
									////naplnpSerialNumber.Kod = ((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety) ? sn : string.Empty;
									//naplnpSerialNumber.Kod = sn;
									//naplnpSerialNumber.Volajici = Volajici_ProdejPridatPolozku.SERLTNUM;
									//if (naplnpSerialNumber.ShowDialog() == DialogResult.Cancel)
									//    return;

									//sn = naplnpSerialNumber.Kod;
								}
							}

							#endregion

						}

						qty = 1;

						// Test na existenci SN
						//DI diexist = null; // ProdejMain.prodejInstance.FindDI(_zbozi.ITEMNMBR, sn);
						//if (diexist != null)
						//{
						//    MessageBox.Show("Seriové èíslo pro tuto položku už bylo nasnímáno!");
						//    return;
						//}
					}

					#region Test na kontrolu existence SN ve vystupu

					//int pocetSN = 0;
					//System.Data.SqlServerCe.SqlCeCommand sncommand = null;
					//try
					//{
					//    sncommand = dita.Connection.CreateCommand();
					//    sncommand.CommandText = "Select count(*) from czmst_di where SERLTNUM='" + sn + "'";
					//    sncommand.Connection.Open();
					//    pocetSN = (int)sncommand.ExecuteScalar();
					//    if (pocetSN > 0)
					//    {
					//        MessageBox.Show("Seriové èíslo pro tuto položku již bylo nasnímáno!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
					//        return;
					//    }
					//}
					//catch (Exception ex)
					//{
					//    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					//    return;
					//}

					int pocetSN = 0;
					try
					{
						// 20160810 JiS - opraveno : test if (zbozi.czsernumtrac == 1 !!! <= SN ...
						if (zbozi.CZ_SerNum_Track == 1)
						{
							pocetSN = Convert.ToInt32(_prodejTable.CZMST_DI.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + zbozi.ITEMNMBR + "' AND SERLTNUM='" + sn + "'"));
							if (pocetSN > 0)
							{
								MessageBox.Show(Fask.Localization.Localization.Prodej3ProdejListSerioveCisloJizByloNasnimano, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
								return;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						return;
					}

					#endregion

					#region zbozi.CZ_SerNum_Track == 2
					//sledovano na sarze
					if (zbozi.CZ_SerNum_Track == 2)
					{// sejme mnozstvi k SN                        
						// ma se prebirat mnozstvi ze zbozi a soucasne je ruzne od 0
						if ((_typdokladu != null && !_typdokladu.Iscfg_mnozstvi_ze_zboziNull() && _typdokladu.cfg_mnozstvi_ze_zbozi > 0) && zbozi.QTY > 0)     // prevzit mnozstvi 
						{
							qty = zbozi.QTY;
						}
						else   // ma se zadat mnozstvi
						{
							if (naplnpMnozstvi == null) naplnpMnozstvi = new ProdejPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", null, null, Prodej.Globals.PovolitZadaniMnozstviScannerem);
							bool baleni = zbozi.QTYPACK > 0;
							naplnpMnozstvi.Odberatel = _odberatel;
							naplnpMnozstvi.Zbozi = zbozi;
							naplnpMnozstvi.Serltnum = sn;
							naplnpMnozstvi.Text = Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
							naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
							naplnpMnozstvi.Volajici = Volajici_ProdejPridatPolozku.QTY;
							//naplnpMnozstvi.Kod = "";
							//if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
							//    return;
							if (Prodej.Globals.Mnozstvi1Auto)
							{
								naplnpMnozstvi.Kod = "1";
							}
							else if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeQuantity) && ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
							{
								naplnpMnozstvi.Kod = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString();
							}
							else
							{
								if (Prodej.Globals.MnozstviREZ1Vypln && !zbozi.IsREZ1Null())
								{
									naplnpMnozstvi.Kod = zbozi.REZ1.Trim();
								}
								else if (code is WeightCode)
								{
									WeightCode wc = (WeightCode)code;
									naplnpMnozstvi.Kod =
									(wc.weight
										/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
										/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
										).ToString(Settings.UIFormatDesCisel);
								}
								else if (code is WeightCode_12)
								{
									WeightCode_12 wc = (WeightCode_12)code;
									naplnpMnozstvi.Kod =
									(wc.weight
										/ (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
										/ (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
										).ToString(Settings.UIFormatDesCisel);
								}
								else if (_typdokladu != null && !_typdokladu.Iscfg_predvyplnit_mnozstviNull() && _typdokladu.cfg_predvyplnit_mnozstvi > 0)   //_typdokladu.cfg_predvyplnit_mnozstvi >0 .... dodelat
									naplnpMnozstvi.Kod = zbozi.QTY.ToString(Settings.UIFormatDesCisel);
								else
									naplnpMnozstvi.Kod = "";

								naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
								if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
									return;
							}

							if (Prodej.Globals.KontrolaStavuSkladu) // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
							{
								if (Convert.ToDecimal(naplnpMnozstvi.Kod) > zbozi.QTY)
								{
									if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz, Fask.Localization.Localization.Prodej3ProdejListInfo, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
									{
										return;
									}
								}
							}

							qty = decimal.Parse(naplnpMnozstvi.Kod);
						}

					}

					#endregion

					#region sledovani Expirace

				if (
					_typdokladu != null &&
					!_typdokladu.Iscfg_expirace_ONOFFNull() &&
					_typdokladu.cfg_expirace_ONOFF > 0
					)
				{

					if (zbozi.CZ_Expirace_Track > 0)
					{

						if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeExpiration) && ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration.HasValue)
							expirace = ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration;

						if (!expirace.HasValue)
						{
							//string expirationStr = expiraceLast.ToString(Main.expirationFormat);
							//string expirationStr = string.Empty;
							expirace = expiraceLast;
							string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);

							while (true)
							{
								var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
								if (dResExpiration == DialogResult.Cancel)
									return;

								// validace
								try
								{
									expirace = MST_W.Main.Date_RRMMDD(expirationStr);
									//expirace = DateTime.ParseExact(expirationStr, Main.dateFormatRRMMDD, System.Globalization.DateTimeFormatInfo.InvariantInfo);
								}
								catch (Exception ex)
								{
									MessageBoxBig.Show(String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}

								expiraceLast = expirace.Value;
								break;
							}
						}
					}
				}
				
				#endregion

				}

				#endregion

				#region Sledovani na SN s atributem šarže CZ_SerNumTrack == 10

				else if (zbozi.CZ_SerNum_Track == 10)
				{
					qty = 1;
					
					#region Vyplneni SN

						if ((code is ICodeSerialNumber) && !string.IsNullOrEmpty(((ICodeSerialNumber)code).SN))
						{
							sn = ((ICodeSerialNumber)code).SN;
						}
						else
						{
							string snOUT = string.Empty;

							var dr = Get_SN_Sarze_AtributToSN(out snOUT,
								sn,
								zbozi,
								Fask.Localization.Localization.Prodej3ProdejListSerioveCislo,
								Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo,
								false
								);

							if (dr == DialogResult.Cancel)
								return;
							else
								sn = snOUT;
						}

						#endregion
	
					#region Test duplicity SN

					int pocetSN = 0;
					try
					{
						// 20160810 JiS - opraveno : test if (zbozi.czsernumtrac == 1 !!! <= SN ...
						//if (zbozi.CZ_SerNum_Track == 1)
						//{
							pocetSN = Convert.ToInt32(_prodejTable.CZMST_DI.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + zbozi.ITEMNMBR + "' AND SERLTNUM='" + sn + "'"));
							if (pocetSN > 0)
							{
								MessageBox.Show(Fask.Localization.Localization.Prodej3ProdejListSerioveCisloJizByloNasnimano, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
								return;
							}
						//}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						return;
					}

					#endregion
					
					#region Vyplneni Sarze

					if (
					_typdokladu != null &&
					!_typdokladu.Iscfg_AttributeToSN_ONOFFNull() &&
					_typdokladu.cfg_AttributeToSN_ONOFF> 0
					)
					{
						if ((code is ICodeSarze) && !string.IsNullOrEmpty(((ICodeSarze)code).Sarze))
						{
							sarza = ((ICodeSarze)code).Sarze;
						}
						else
						{
							string snOUT = string.Empty;

							var dr = Get_SN_Sarze_AtributToSN(out snOUT,
								sarza,
								zbozi,
								Fask.Localization.Localization.Prodej3ProdejListSarze,
								Fask.Localization.Localization.Prodej3ProdejListVlozteSarze,
								false
								);

							if (dr == DialogResult.Cancel)
								return;
							else
								sarza = snOUT;
						} 
					}

						#endregion

					#region sledovani Expirace

					if (
						_typdokladu != null &&
						!_typdokladu.Iscfg_expirace_ONOFFNull() &&
						_typdokladu.cfg_expirace_ONOFF > 0
						)
					{

						if (zbozi.CZ_Expirace_Track > 0)
						{

							if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeExpiration) && ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration.HasValue)
								expirace = ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration;

							if (!expirace.HasValue)
							{
								expirace = expiraceLast;
								string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);

								while (true)
								{
									var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
									if (dResExpiration == DialogResult.Cancel)
										return;

									try
									{
										expirace = MST_W.Main.Date_RRMMDD(expirationStr);
									}
									catch (Exception ex)
									{
										MessageBoxBig.Show(String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										continue;
									}

									expiraceLast = expirace.Value;
									break;
								}
							}
						}
					}


					#endregion
				}

				#endregion

				#region Sledovani na SN s atributem šarže CZ_SerNumTrack == 11

				else if (zbozi.CZ_SerNum_Track == 11)
				{
					qty = 1;

					#region Vyplneni SN

					if ((code is ICodeSerialNumber) && !string.IsNullOrEmpty(((ICodeSerialNumber)code).SN))
					{
						sn = ((ICodeSerialNumber)code).SN;
					}
					else
					{
						string snOUT = string.Empty;

						var dr = Get_SN_Sarze_AtributToSN(out snOUT,
							sn,
							zbozi,
							Fask.Localization.Localization.Prodej3ProdejListSerioveCislo,
							Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo,
							true
							);

						if (dr == DialogResult.Cancel)
							return;
						else
							sn = snOUT;
					}

					#endregion

					#region Test duplicity SN

					int pocetSN = 0;
					try
					{
						if (!string.IsNullOrEmpty(sn))
						{
							// 20160810 JiS - opraveno : test if (zbozi.czsernumtrac == 1 !!! <= SN ...
							//if (zbozi.CZ_SerNum_Track == 1)
							//{
							pocetSN = Convert.ToInt32(_prodejTable.CZMST_DI.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + zbozi.ITEMNMBR + "' AND SERLTNUM='" + sn + "'"));
							if (pocetSN > 0)
							{
								MessageBox.Show(Fask.Localization.Localization.Prodej3ProdejListSerioveCisloJizByloNasnimano, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
								return;
							}
							//} 
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						return;
					}

					#endregion

					#region Vyplneni Sarze

					if (
					_typdokladu != null &&
					!_typdokladu.Iscfg_AttributeToSN_ONOFFNull() &&
					_typdokladu.cfg_AttributeToSN_ONOFF > 0
					)
					{
						if ((code is ICodeSarze) && !string.IsNullOrEmpty(((ICodeSarze)code).Sarze))
						{
							sarza = ((ICodeSarze)code).Sarze;
						}
						else
						{
							string snOUT = string.Empty;

							var dr = Get_SN_Sarze_AtributToSN(out snOUT,
								sarza,
								zbozi,
								Fask.Localization.Localization.Prodej3ProdejListSarze,
								Fask.Localization.Localization.Prodej3ProdejListVlozteSarze,
								true
								);

							if (dr == DialogResult.Cancel)
								return;
							else
								sarza = snOUT;
						}
					}

					#endregion

					#region sledovani Expirace

					if (
						_typdokladu != null &&
						!_typdokladu.Iscfg_expirace_ONOFFNull() &&
						_typdokladu.cfg_expirace_ONOFF > 0
						)
					{

						if (zbozi.CZ_Expirace_Track > 0)
						{

							if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeExpiration) && ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration.HasValue)
								expirace = ((Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration;

							if (!expirace.HasValue)
							{
								expirace = expiraceLast;
								string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);

								while (true)
								{
									var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
									if (dResExpiration == DialogResult.Cancel)
										return;

									try
									{
										expirace = MST_W.Main.Date_RRMMDD(expirationStr);
									}
									catch (Exception ex)
									{
										MessageBoxBig.Show(String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										continue;
									}

									expiraceLast = expirace.Value;
									break;
								}
							}
						}
					}


					#endregion
				}

				#endregion
				
				#region OLD sledovani na sarze
					/*
				else if (_zbozi.CZ_SerNum_Track == 2)//sledovano na sarzi a mnozstvi
				{
					using (ProdejPridatPolozku naplnp = new ProdejPridatPolozku(Program.mstw.SNName, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, "", _odberatel, _zbozi))
					{
						naplnp.Text = "Vložte " + Program.mstw.SNName;
						if (naplnp.ShowDialog() == DialogResult.Cancel)
							return;

						sn = naplnp.Kod;
					}

					using (ProdejPridatPolozku naplnp = new ProdejPridatPolozku("Množství", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", _odberatel, _zbozi))
					{
						naplnp.Text = "Vložte množství";

						qty = decimal.Parse(naplnp.Kod);
					}
				}
				*/

				#endregion

				#region Lokace pred SN

				if (!Prodej.Globals.prodejZadaniLocncodePredSN && ((_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0) || Prodej.Globals.prodejPovolitZadaniLocncode))
				{
					if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
					{
						// rucni vyber lokace, pokud neni jiz zvolena
						using (Forms.FormLokaceVyber fsv = new FormLokaceVyber(sklad_id))
						{
							//fsv.Text = "Výbìr lokace";
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							locncode = fsv.Lokace.LOCNCODE;

							if (locncode == null)
							{
								Logging.Log.Write("Není vybrána zdrojová lokace z èíselníku, pøestože je vyžadována!");
								return;
							}
						}
					}
					else // if (!string.IsNullOrEmpty(locncode))
					{
						locncode = SejmiLocncode(zbozi, true);
						if (locncode == "!@")
							return;
					}
				}

				#endregion

				#region prepoèet QTY a QTYPACK

				Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = _prodejTable.CZMST_DI.NewCZMST_DIRow();
				di.QTYSHPPD = qty * (zbozi.QTYPACK > 0 ? zbozi.QTYPACK : 1);

				#endregion

				#region Kontrola disponibility Zdrojový - online

				if ((_typdokladu != null && _typdokladu.cfg_disp > 0) || Prodej.Globals.DisponibilityCheck)
				{
					if (!Online_DISP(zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id, string.Empty, string.Empty))
						return;
				}

				#endregion

				#region Kontrola disponibility Cilový - online

				if ((_typdokladu != null && _typdokladu.cfg_disp_dest > 0) || Prodej.Globals.DisponibilityCheck)
				{		
					if (!Online_DISP(zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id_dest, string.Empty, string.Empty))
						return;
				}
				#endregion

				#region Online overeni zdrojove lokace

				if ((_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokaceNull() && _typdokladu.cfg_onl_over_lokace > 0) || Prodej.Globals.OverovatZdrojovouLokaci)
				{
					string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
					Fask.MST_W.ProdejService.TypeOfRecord recordType;
					if (!string.IsNullOrEmpty(pohyb_type))
						recordType = (Fask.MST_W.ProdejService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.ProdejService.TypeOfRecord), pohyb_type, true);
					else recordType = Fask.MST_W.ProdejService.TypeOfRecord.E;

					Fask.MST_W.ProdejService.StatusOverLokace so = OnlineOverLokace(zbozi.ITEMNMBR, sn,expirace, locncode, sklad_id, di.QTYSHPPD, Fask.MST_W.ProdejService.TYPLokace.SOURCE, recordType);
					if (so != null)
					{
						switch (so.State)
						{
							case Fask.MST_W.ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
								break;
							case Fask.MST_W.ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
								DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPorusenoDoporucenePoradiPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
								if (dr == DialogResult.No)
									return;
								Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "zdrporadi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncode, ""));
								break;
							case Fask.MST_W.ProdejService.STATUSOverLokace.ERROR:
								MessageBoxBig.Show(so.Message.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
							default: // neni mozne pokracovat
								MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListNeniMozneBratZLokace, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
						}
					}
					else  // nic se nenacetlo
					{
						// chyba komunikace se serverem, dotaz zdali pokracovat
						DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListKomunikaceServeruProblemOverLokaciPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
						if (dr == DialogResult.No)
							return;
					}
				}

				#endregion

				#region Zde zacinaji doplnujici informace ...

				#region Strediska 

				if ((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska)
				{
					if (!StrediskoSet())
					{
						return;
					}
				}

				#endregion

				#region Pracovnici

				if ((_typdokladu != null && _typdokladu.cfg_prac > 0) || Prodej.Globals.Pracovnici)
				{
					if (!PracovniciSet())
					{
						return;
					}
				}

				#endregion

				#region Palety
				// 22.6.2016 PeV: zakomentovano, pouzivalo se pouze u jednoho zakaznika??
				//if (_paleta == null)
				//{
				//    if (!PaletaSet())
				//        return;
				//}
				if ((_typdokladu != null && _typdokladu.cfg_palety > 0 && nmbrpal == null))
				{
					// kontrola, zdali je paleta null, pokud ano, nepustit dal ...
					MessageBoxBig.Show("Není vybrána paleta", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}

				#endregion

				#region REZ1

				//Doplnit hodnotu rez1
				// 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
				rez1 = zbozi.IsREZ1Null() ? string.Empty : zbozi.REZ1.Trim();
				if (zbozi.CZ_Rez1_Track > 0)
				{
					skf.CodeType = Prodej.Globals.Rez1Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.AllowEmpty = !Prodej.Globals.Rez1Povinne;
					skf.CheckLen = false;
					skf.Kod = Prodej.Globals.Rez1Pamatovat ? Settings.ProdejRez1LastValue : rez1;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_1"].MaxLength;
					skf.Popis = MST_Global.REZ1_PROD_NAME;
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplòující hodnoty";
					if (skf.ShowDialog() == DialogResult.Cancel)
						return;
					rez1 = skf.Kod;
					if (Prodej.Globals.Rez1Pamatovat)
						Settings.ProdejRez1LastValue = rez1;
				}

				#endregion

				#region REZ 2

				//Doplnit hodnotu rez2
				// 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
				rez2 = zbozi.IsREZ2Null() ? string.Empty : zbozi.REZ2.Trim();
				if (zbozi.CZ_Rez2_Track > 0)
				{
					skf.CodeType = Prodej.Globals.Rez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.AllowEmpty = !Prodej.Globals.Rez2Povinne;
					skf.CheckLen = false;
					skf.Kod = Prodej.Globals.Rez2Pamatovat ? Settings.ProdejRez2LastValue : rez2;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_2"].MaxLength;
					skf.Popis = MST_Global.REZ2_PROD_NAME;
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplòující hodnoty";                  
					if (skf.ShowDialog() == DialogResult.Cancel)
						return;
					rez2 = skf.Kod;
				
					if (Prodej.Globals.Rez2Pamatovat) Settings.ProdejRez2LastValue = rez2;
				}

				#endregion

				#region REZ3

				//Doplnit hodnotu rez3
				// 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
				rez3 = zbozi.IsREZ3Null() ? string.Empty : zbozi.REZ3.Trim();
				if (zbozi.CZ_Rez3_Track > 0)
				{
					skf.CodeType = Prodej.Globals.Rez3Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.AllowEmpty = !Prodej.Globals.Rez3Povinne;
					skf.CheckLen = false;
					skf.Kod = Prodej.Globals.Rez3Pamatovat ? Settings.ProdejRez3LastValue : rez3;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_3"].MaxLength;
					skf.Popis = MST_Global.REZ3_PROD_NAME;
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplòující hodnoty";
					if (skf.ShowDialog() == DialogResult.Cancel)
						return;
					rez3 = skf.Kod;
					if (Prodej.Globals.Rez3Pamatovat) Settings.ProdejRez3LastValue = rez3;
				}

				#endregion

				#region REZ4

				//Doplnit hodnotu rez4
				// 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
				rez4 = zbozi.IsREZ4Null() ? string.Empty : zbozi.REZ4.Trim();
				if (zbozi.CZ_Rez4_Track > 0)
				{
					skf.CodeType = Prodej.Globals.Rez4Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.AllowEmpty = !Prodej.Globals.Rez4Povinne;
					skf.CheckLen = false;
					skf.Kod = Prodej.Globals.Rez4Pamatovat ? Settings.ProdejRez4LastValue : rez4;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_4"].MaxLength;
					skf.Popis = MST_Global.REZ4_PROD_NAME;
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplòující hodnoty";
					if (skf.ShowDialog() == DialogResult.Cancel)
						return;
					rez4 = skf.Kod;
					if (Prodej.Globals.Rez4Pamatovat) Settings.ProdejRez4LastValue = rez4;
				}

				#endregion

				#region Ceny

				////decimal cena = _zbozi.Cena(_odberatel.odb_typ);
				//bool jecenasdani = false; // = Globals.PriceXIsWithTax(_odberatel.odb_typ);
				//byte cenovahladina = 0; // = Globals.PriceX(_odberatel.odb_typ);
				//decimal cenasdani = 0;
				//decimal cenabezdane = 0;
				//decimal cenadan = 0;
				Price price = new Price();

				//Prodej.Globals.zjisti_cenu(zbozi, _odberatel, _mena, ref cenasdani, ref cenabezdane, ref cenadan, ref jecenasdani, ref cenovahladina);
				Prodej.Globals.zjisti_cenu(zbozi, _odberatel, _mena, price); // price je objekt, tedy odkazem => meni se vlastnosti ...

				//Prodej.Globals.nastav_cenu(di, cenasdani, cenabezdane, cenadan, jecenasdani, cenovahladina);
				Prodej.Globals.nastav_cenu(di, price);

				#endregion

				#region Odberatel

				string odb_id = string.Empty;
				if (_typdokladu == null)
				{
					if (_odberatel != null)
						odb_id = _odberatel.odb_id;
					else
						odb_id = string.Empty;
				}
				else
				{
					// PeV 29.9.2015 - zmena funkcionality skladid ... probiha predvyplneni skladu podle nej
					//if (_typdokladu.doc_typ.Trim() == "1")
					//    odb_id = Prodej.Globals.SkladID;
					//else 
					if (_odberatel != null)
						odb_id = _odberatel.odb_id;
					else
						odb_id = zbozi.IsODB_IDNull() ? string.Empty : zbozi.ODB_ID;
				}
				#endregion

				#region OLD Sklad

				//_sklad.skl_id - pokud je vybrany sklad. Pokud neni, tak pouzit zbozi
				//zbozi.SKL_ID
				// _sklad.skl_id , pokud nebude nastaveno, tak string.empty
				// ovìøení pohybu
				// pokud bude di.QTYSHPPD > OverPohyb(), tak dotaz, zdali chce pokracovat Ano/Ne (vetsi nez stav na skladu)
				#endregion

				#endregion

				#region Overovat pohyb

				if (Prodej.Globals.OverovatPohyb)
				{
					decimal outshppd = 0;
					decimal qtyshppdnacteno = Nacteno(zbozi.ITEMNMBR);
					if (!OverPohyb(sn, zbozi.ITEMNMBR, _skladZdroj == null ? string.Empty : _skladZdroj.skl_id, di.QTYSHPPD, qtyshppdnacteno, out outshppd))
						return;
					else if (_skladZdroj != null && (di.QTYSHPPD + qtyshppdnacteno) > outshppd)
					{
						DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListMnozstviVetsiNezStavSkladuPokracovatDotaz, di.QTYSHPPD, qtyshppdnacteno, outshppd), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
						if (dr == DialogResult.No)
							return;
					}
				}

				#endregion

				#region zadani ciloveho skladu
 
				if (string.IsNullOrEmpty(sklad_id_dest.Trim()) && _typdokladu != null && !_typdokladu.Iscfg_skl_id_destNull() && _typdokladu.cfg_skl_id_dest > 0)
				{
					Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladdest = null;//_skladCil;
					string skl_id_dst = !_typdokladu.Ispredvyplnit_skl_id_destNull() ? _typdokladu.predvyplnit_skl_id_dest.Trim() : string.Empty;

					try
					{
						// najiti skladu, pokud je v typu dokladu
						if (!string.IsNullOrEmpty(skl_id_dst))
						{
                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.GetDataBySkl_id(skl_id_dst);
                            if (dt_sklady.Count > 0)
								skladdest = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListCilovySkladNenalezenDotaz, skl_id_dst.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
						MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						return;
					}

					// rucni vyber skladu, pokud neni jiz zvolen
					if (skladdest == null)
					{
						using (Forms.FormSkladVyber fsv = new FormSkladVyber())
						{
							fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberCilovehoSkladu;
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							skladdest = fsv.Sklad;

							if (skladdest == null)
							{
								Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
								return;
							}
						}
					}
					sklad_id_dest = skladdest.skl_id;
				}

				#endregion

				#region Zadani cilove lokace

				string locncodedest = string.Empty;
				Fask.MST_W.PrijemService.Obecne dsdest = null;
				bool onlineKontrolaCilLokace = true;
				locncodedest = (_typdokladu == null || _typdokladu.Ispredvyplnit_locncodedestNull()) ? string.Empty : _typdokladu.predvyplnit_locncodedest.Trim();
				if (_typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
				{
					// vybrani cilove lokace z ciselniku lokaci
					if (string.IsNullOrEmpty(locncodedest.Trim()) && _typdokladu != null && !_typdokladu.Iscfg_lokace_dest_ciselnikNull() && _typdokladu.cfg_lokace_dest_ciselnik > 0)
					{
						#region neresi se
						// kontrola lokace s ciselnikem lokaci ... neresi se
						//Fask.MST_W.SqlCEDBs.DataSets.Lokace.CZMST094Row lokacedest = null;
						//try
						//{                            
						//    // najiti lokace, pokud je v typu dokladu
						//    if (!string.IsNullOrEmpty(locncodedest))
						//    {
						//        Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter ta_lokace = new Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter();
						//        ta_lokace.Connection.ConnectionString = "Data source=" + Main.CiselnikLokaceDB;
						//        Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dt_lokace = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
						//        // vyhledavani lokace podle caroveho kodu lokace
						//        if (_skladCil == null)
						//            dt_lokace = ta_lokace.GetDataByBarcode(locncodedest);
						//        else  // vyhledavani lokace podle caroveho kodu lokace a id skladu
						//            dt_lokace = ta_lokace.GetDataBySklidBarcode(_skladCil.skl_id, locncodedest);

						//        if (dt_lokace.Count == 1)
						//            lokacedest = dt_lokace[0];
						//        else
						//        {
						//            MessageBoxBigTimeout.Show("Odpovídající cílová lokace s èárovým kódem '" + locncodedest + "' " + (_skladCil == null ? string.Empty : ("ve skladu '" + _skladCil.skl_id.Trim() + "'")) + " nenalezena!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						//            return;
						//        }
						//    }
						//}
						//catch (Exception ex)
						//{
						//    Logging.Log.Write(ex);
						//    MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						//    return;
						//}
						#endregion

						// rucni vyber cilove lokace, pokud neni jiz zvolen
						using (Forms.FormLokaceVyber fsv = new FormLokaceVyber(sklad_id_dest))
						{
							fsv.Text = Fask.Localization.Localization.Prodej3ProdejListVyberCiloveLokace;
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							locncodedest = fsv.Lokace.LOCNCODE;

							if (fsv.Lokace == null)
							{
								Logging.Log.Write("Není vybrána cílová lokace z èíselníku, pøestože je vyžadována!");
								return;
							}
						}
					}
					else if ((!_typdokladu.Iscfg_onl_dop_lokace_destNull() && _typdokladu.cfg_onl_dop_lokace_dest > 0) || Prodej.Globals.DoporuceneCiloveLokace)
					{   // online doporucene cilove lokace
						// zobrazit seznam
						dsdest = OnlineGetDoporuceneCiloveLokace(zbozi.ITEMNMBR, sn, sklad_id_dest); //sklad_id);
						if (dsdest != null)
						{
							// vratily se nejake zaznamy
							if (dsdest.Lokace.Count > 0)
							{
								onlineKontrolaCilLokace = (_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0) || Prodej.Globals.OverovatCilovouLokaci;
								using (Fask.MST_W.Prijem_4.PrijemVyberLokaceList pvpl = new Fask.MST_W.Prijem_4.PrijemVyberLokaceList(dsdest, sn, zbozi.ITEMNMBR, onlineKontrolaCilLokace, sklad_id_dest, zbozi.QTY)) //_sklad != null ? _sklad.skl_id : string.Empty))
								{
									pvpl.Text = Fask.Localization.Localization.Prodej3ProdejListVyberCiloveLokace;  //"Výbìr cílové lokace";
									if (pvpl.ShowDialog() == DialogResult.OK)
									{
										//locncode = pvpl._lokaceRow.LOCNCODE;
										locncodedest = pvpl.ResLocncode;
										onlineKontrolaCilLokace = false;
									}
									else
										return;
								}
							}
							else
							{
								// nic se nevratilo, je treba zadat rucne ...
								locncodedest = SejmiLocncode(zbozi, false);
								if (locncodedest == "!@")
									return;
							}
						}
						else
						{
							// nic se nevratilo, je treba zadat rucne ...
							locncodedest = SejmiLocncode(zbozi, false);
							if (locncodedest == "!@")
								return;
						}
					}
					else
					{
						// lokace se vyplnuje rucne
						locncodedest = SejmiLocncode(zbozi, false);
						if (locncodedest == "!@")
							return;
					}
				}
				#endregion

				#region Online overeni cilove lokace

				if (((_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0) || Prodej.Globals.OverovatCilovouLokaci) && onlineKontrolaCilLokace)
				{
					string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
					Fask.MST_W.ProdejService.TypeOfRecord recordType;
					if (!string.IsNullOrEmpty(pohyb_type))
						recordType = (Fask.MST_W.ProdejService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.ProdejService.TypeOfRecord), pohyb_type, true);
					else recordType = Fask.MST_W.ProdejService.TypeOfRecord.E;

					Fask.MST_W.ProdejService.StatusOverLokace so = OnlineOverLokace(zbozi.ITEMNMBR, sn,expirace, locncodedest, sklad_id_dest, di.QTYSHPPD, Fask.MST_W.ProdejService.TYPLokace.DEST, recordType);
					if (so != null)
					{
						switch (so.State)
						{
							case Fask.MST_W.ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
								break;
							case Fask.MST_W.ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
								DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPorusenoDoporucenePoradiCilLokacePokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
								if (dr == DialogResult.No)
									return;
								Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "cilporadi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncodedest, ""));
								break;
							case Fask.MST_W.ProdejService.STATUSOverLokace.ERROR:
								MessageBoxBig.Show(so.Message.Trim(), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
							default: // neni mozne pokracovat
								MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListNeniMozneUlozitNaLokaci, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
						}
					}
					else  // nic se nenacetlo
					{
						// chyba komunikace se serverem
						DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListKomunikaceServeruProblemOverCilLokaciPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
						if (dr == DialogResult.No)
							return;
					}
				}

				#endregion

				#region Vyplneni DI jednoho øadku

				DateTime dtnow = DateTime.Now;
				Guid newGuid = Guid.NewGuid();
				di.CountEntries = _cislodavky;
				//di.ItemDescription = zbozi.ITEMDESC;
				di.ITEMNMBR = zbozi.ITEMNMBR;
				di.LOCNCODE = locncode;
				di.LOCNCODEDEST = locncodedest;
				di.ODB_ID = odb_id;
				di.STR_ID = (_stredisko == null ? string.Empty : _stredisko.str_id);
				di.PRAC_ID = (_pracovnik == null ? string.Empty : _pracovnik.prac_id);
				di.DOC_ID = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
				di.DOC_ID2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
				di.QTYPACK = zbozi.QTYPACK;
				#region Rez old
				//di.REZ_1 = "";
				//di.REZ_2 = "";
				//di.REZ_3 = "";
				//di.REZ_4 = "";
				//string reztmp = zbozi.VNDITNUM + (new String(' ', SERNUM_LEN + UDAJ_LEN + 1));
				//try { di.REZ_2 = reztmp.Substring(0, SERNUM_LEN).Trim(); }
				//catch { } //strncpy(pdinstruct->REZ_2, psinstruct->VNDITNUM, SERNUM_LEN); pdinstruct->REZ_2[SERNUM_LEN - 1] = '\0';
				//try { di.REZ_1 = reztmp.Substring(SERNUM_LEN, UDAJ_LEN).Trim();}
				//catch { } //strncpy(pdinstruct->REZ_1, (psinstruct->VNDITNUM) + SERNUM_LEN - 1, UDAJ_LEN); pdinstruct->REZ_1[UDAJ_LEN - 1] = '\0';
				//reztmp = zbozi.CZ_CarKod + (new String(' ', SERNUM_LEN + UDAJ_LEN + 1));
				//try { di.REZ_4 = reztmp.Substring(0, SERNUM_LEN).Trim(); }
				//catch { } //strncpy(pdinstruct->REZ_4, psinstruct->CZ_CarKod, SERNUM_LEN); pdinstruct->REZ_4[SERNUM_LEN - 1] = '\0';
				//try { di.REZ_3 = reztmp.Substring(SERNUM_LEN, UDAJ_LEN).Trim(); }
				//catch { } //strncpy(pdinstruct->REZ_3, (psinstruct->CZ_CarKod) + SERNUM_LEN - 1, UDAJ_LEN); pdinstruct->REZ_3[UDAJ_LEN - 1] = '\0';
				#endregion
				di.REZ_1 = rez1;
				di.REZ_2 = rez2;
				di.REZ_3 = rez3;
				di.REZ_4 = rez4;
				di.SERLTNUM = sn;
				di.DATEDONE = dtnow.ToString("yyyyMMdd");
				di.TIMEDONE = dtnow.ToString("HHmmss");
				di.USER_ID = MST_Global.UserID;
				di.guid = newGuid;
				di.ITEMDESC = zbozi.IsITEMDESCNull() ? string.Empty : zbozi.ITEMDESC;
				di.VNDITNUM = zbozi.VNDITNUM;
				di.CZ_CarKod = zbozi.CZ_CarKod;
				di.INPUT_MODE = _input_mode;
				di.ITEMCODE = zbozi.IsITEMCODENull() ? string.Empty : zbozi.ITEMCODE;   // 3.6.2016 PeV: Doplneno, neprobihalo nastaveni ITEMCODE
				di.ID_TERMINAL = MST_Global.TerminalID;
				if (zbozi.IsWEIGHTNull())
					di.SetWEIGHTNull();
				else
					di.WEIGHT = zbozi.WEIGHT;

				di.SKL_ID_DEST = sklad_id_dest;

				#region skl_id_dest old
				// 6.4.2016 PeV: resi konfigurace typu dokladu 'cfg_skl_id_dest_prevzit'
				//string skl_dest = string.Empty;
				//if (skladdest != null)
				//    // zadava se cilovy sklad
				//    skl_dest = skladdest.skl_id;
				//else
				//{
				//    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
				//    Fask.MST_W.ProdejService.TypeOfRecord recordType;
				//    if (!string.IsNullOrEmpty(pohyb_type))
				//        recordType = (Fask.MST_W.ProdejService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.ProdejService.TypeOfRecord), pohyb_type, true);
				//    else recordType = Fask.MST_W.ProdejService.TypeOfRecord.E;

				//    // cilovy sklad se nezadava
				//    // pri prelokovani se pouzije stejny sklad jako zdrojovy (presun mezi lokacemi v ramci stejneho skladu)
				//    if (recordType == Fask.MST_W.ProdejService.TypeOfRecord.D)
				//        skl_dest = sklad_id;
				//    else    // pri prijmu, vydeji, ... se nevyplni cilovy sklad
				//        skl_dest = string.Empty;
				//}
				//di.SKL_ID_DEST = skl_dest;
				#endregion

				di.PRINTED = false;
				di.SKL_ID = sklad_id;
				di.MJ = zbozi.MJ;
				di.QTYSHPPDMJ = qty;
				// 22.6.2016 PeV: zakomentovano, pouzivalo se pouze u jednoho zakaznika??
				//if (_paleta != null)
				//{
				//    if (_paleta.Typ != null)
				//        di.TYPEPAL = _paleta.Typ;
				//    if (_paleta.Cislo != null)
				//        di.NMBRPAL = _paleta.Cislo;
				//}
				if (nmbrpal != null)
				{
					if (nmbrpal.Code != null)
						di.NMBRPAL = nmbrpal.Code.Trim();

					di.TYPEPAL = string.Empty;  // TODO: dodelat ...
					// neni implementovano ...
					//if (nmbrpal.Code != null)
					//    di.TYPEPAL = nmbrpal.Type.Trim();
				}

				if (expirace.HasValue)
					di.EXPIRACE = expirace.Value;
				else
					di.SetEXPIRACENull();

				if (string.IsNullOrEmpty(sarza))
					di.SetAttributeToSNNull();
				else
					di.AttributeToSN = sarza;

				// TODO : meny doplnit doplneni men a cen se deje jinde v GLobals.zjistcenu, Globals.nastavcenu ...
				//di.mena_ID = _zbozi.MENA_ID;
				//di.mena_IDM = _mena == null ? null : _mena.mena_ID;
				//di.TAXAMPIEM = null;
				//di.AMOUNPIEM = null;

				#endregion

				#region Rozhodnuti Sklad/Expedice/Rozdelit

				RozhodovatSkladExpediceRozdelit(di);

				#endregion

				#region lokace

				// online ulozeni do lokacniho mechanismu, pokud je lokacni mechanismus povolen a jsou zapnute online pohyby
				if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0 &&
					!_typdokladu.Iscfg_lok_mech_online_pohybyNull() && _typdokladu.cfg_lok_mech_online_pohyby > 0)
				{
					Cursor.Current = Cursors.WaitCursor;
					Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
					pohybrow.ITEMNMBR = zbozi.ITEMNMBR;
					pohybrow.DOCUMENT_NUMBER = _typdokladu.doc_id;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS), pokud prodej, tak doc_id
					string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
					Fask.MST_W.LokaceService.TypeOfRecord recordType = (Fask.MST_W.LokaceService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.LokaceService.TypeOfRecord), pohyb_type, true);
					pohybrow.POHYB_TYPE = recordType;
					pohybrow.POHYB_SRC = "R";   // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
					pohybrow.SOURCE = "T";
					pohybrow.QTYSHPPD = di.QTYSHPPD;
					pohybrow.SERLTNUM = sn;
					pohybrow.SKL_ID_SRC = sklad_id;

					pohybrow.SKL_ID_DST = sklad_id_dest;
					//if (skladdest != null)
					//    // zadava se cilovy sklad
					//    pohybrow.SKL_ID_DST = skladdest.skl_id;
					//else
					//{
					//    // cilovy sklad se nezadava
					//    // pri prelokovani se pouzije stejny sklad jako zdrojovy (presun mezi lokacemi v ramci stejneho skladu)
					//    if (recordType == Fask.MST_W.LokaceService.TypeOfRecord.D)
					//        pohybrow.SKL_ID_DST = sklad_id;
					//    else    // pri prijmu, vydeji, ... se nevyplni cilovy sklad
					//        pohybrow.SKL_ID_DST = string.Empty;
					//}

					//pohybrow.SKL_ID_DST = skladdest != null ? skladdest.skl_id : sklad_id;      // pokud neni vybrany cilovy sklad, jedna se o pohyb v ramci stejneho skladu jako zdrojoveho
					pohybrow.LOCNCODE_SRC = locncode;
					pohybrow.LOCNCODE_DST = locncodedest;       // lokace, kam se prevadi ...
					pohybrow.UserID = MST_Global.UserID;
					pohybrow.TermID = MST_Global.TerminalID;
					pohybrow.guid = newGuid;
					//pohybrow.dateeveS = ...   // datum serveru se vyplnuje az na serveru
					pohybrow.Expiration = expirace;
					pohybrow.ITEMDESC = zbozi.IsITEMDESCNull() ? string.Empty : zbozi.ITEMDESC;
					pohybrow.CountEntries = _cislodavky;
					pohybrow.dateeveT = dtnow;   // datum terminalu


					try
					{
						Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - start", "LocationLog");
						Fask.MST_W.Classes.LokaceLog.writeBody(pohybrow);

						Fask.MST_W.LokaceService.StatusLokace sl = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_lokace.MoveItem(pohybrow);
						Cursor.Current = Cursors.Default;
						switch (sl.State)
						{
							case Fask.MST_W.LokaceService.States.OK:
								break;
							case Fask.MST_W.LokaceService.States.ERROR:
								MessageBoxBig.Show("Nepodaøilo se pøidat záznam lokace, záznam nebude pøidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
							default:
								MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se pøidat záznam do lokací. Záznam nebude pøidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
						}

						Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - end", "LocationLog");
					}
					catch (Exception ex)
					{
						Logging.Log.WriteDebug(ex.Message);
						Cursor.Current = Cursors.Default;
						if (MessageBoxBig.Show(ex.Message + "\nPøejete si pøesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
						{
							//Promenna ridici cyklus
							bool state = true;

							//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
							while (state)
							{
								try
								{
									// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
									// Volani sluzby pro odstraneni a kontrola navratveho stavu.
									Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

									Fask.MST_W.LokaceService.StatusLokace sl = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_lokace.DeleteRecordByGuid(pohybrow.guid, recordType);
									DialogResult dr = DialogResult.No;
									switch (sl.State)
									{
										case Fask.MST_W.LokaceService.States.OK:
											dr = DialogResult.Yes;
											break;
										case Fask.MST_W.LokaceService.States.ERROR:
											dr = MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n" + sl.ErrorMessage + "\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
											return;
										default:
											dr = MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznam v lokaèním systému!\n\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
											return;
									}

									Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

									// pokud ho chce ulozit, odejde z cyklu
									if (dr == DialogResult.Yes)
										break;
								}
								//Nejaka online chyba
								catch (Exception exex)
								{
									Logging.Log.Write("Chyba pøi mazání lokací : " + exex.Message);
									Cursor.Current = Cursors.Default;
									if (MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n'" + exex.Message + "'\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
									{
										//Ukonceni cyklu - chce zaznam ulozit
										break;
									}
								}
							}
						}
					}
				}

				#endregion

				di.DEX_ROW_ID = 0;

				#region Vlozeni jednoho radku do DI

				_prodejTable.CZMST_DI.AddCZMST_DIRow(di);

				try
				{
					dataGridNasnimane.CurrentCell = new DataGridCell(_prodejTable.CZMST_DI.Count - 1, 0);
				}
				catch { }

				while (true)
				{
					try
					{
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DI_DirectInsert(di);
						break;
					}
					catch (Exception ex)
					{
						Logging.Log.Write("dita.insert," + ex.Message, "Prodej");
						if (DialogResult.Yes != MessageBoxBig.Show(ex.Message + "\nPøejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
						{
							#region lokace
							// pokud ne, dojde online odmazani ...
							if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0 &&
								!_typdokladu.Iscfg_lok_mech_online_pohybyNull() && _typdokladu.cfg_lok_mech_online_pohyby > 0)
							{
								//Promenna ridici cyklus
								bool state = true;

								string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
								Fask.MST_W.LokaceService.TypeOfRecord recordType = (Fask.MST_W.LokaceService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.LokaceService.TypeOfRecord), pohyb_type, true);

								//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
								while (state)
								{
									try
									{
										//Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
										//Volani sluzby pro odstraneni a kontrola navratveho kodu.
										Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newGuid.ToString(), "LocationLog");

										Fask.MST_W.LokaceService.StatusLokace sl = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_lokace.DeleteRecordByGuid(newGuid, recordType);
										DialogResult dr = DialogResult.No;
										switch (sl.State)
										{
											case Fask.MST_W.LokaceService.States.OK:
												dr = DialogResult.Yes;
												break;
											case Fask.MST_W.LokaceService.States.ERROR:
												dr = MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n'" + sl.ErrorMessage + "'\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
												break;
											default:
												dr = MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznam v lokaèním systému!\n\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
												break;
										}

										// pokud ho chce ulozit, odejde z cyklu
										if (dr == DialogResult.Yes)
											break;
									}
									//Nejaka online chyba
									catch (Exception exex)
									{
										Logging.Log.Write("Chyba pøi mazání lokací : " + exex.Message);
										Cursor.Current = Cursors.Default;
										if (MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n" + exex.Message + "\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
										{
											//Ukonceni cyklu - chce zaznam ulozit
											break;
										}
									}
								}
							}
							else
							{
								// chyba pri ulozeni, pokud je lokacni mechanismus vypnuty
								throw ex;
							}
							#endregion
						}
					}
				}

				#endregion

				#region Zvuk po napipnuti

				if (!string.IsNullOrEmpty(Settings.PrijemSoundUspesneVlozeni))
				{
					if (File.Exists(Path.Combine(Main.SoundDir, Settings.PrijemSoundUspesneVlozeni)))
					{
						MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundUspesneVlozeni)); //ok, vlozeno pro informaci
					}
				}

				#endregion

				#region Tisk po pridani zboží

				// Zjištìní, zdali se má tisknout (podle cfg v DB)
				if (MST_Global.PovolitPrintServer && ((_typdokladu != null && _typdokladu.cfg_tisk > 0) || (Prodej.Globals.PovolitTiskEtikety && Prodej.Globals.TiskEtiketyPoPridaniZbozi)))
				{
					TiskEtikety(di);
				}

				#endregion

				this.UpdateForm();
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				ScannerStart();
				this.Show();
			}
		}

		private bool Online_DISP(string ITEMNMBR, decimal QTYSHPPD, string sklad_id, string LOCNCODE, string SERLTNUM)
		{

			Fask.MST_W.ProdejService.StatusResult info = null;
			bool opakovat;
			decimal mnozstvi;


			object MnozstviPredOBJ = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.SUM_QTYSHPPD_By_ITEMNMBR_from_DI(ITEMNMBR);
			decimal? MnozstviPredDEC = null;

			//BUG Tady je asi chyba pri objektu decimal...
			if ((MnozstviPredOBJ != null) && ( (MnozstviPredOBJ is Int64) || (MnozstviPredOBJ is Double)))
			{
				MnozstviPredDEC = Convert.ToDecimal(MnozstviPredOBJ);
			}

			opakovat = true;


			if (MnozstviPredDEC.HasValue)
				mnozstvi = QTYSHPPD + (decimal)MnozstviPredDEC;
			else
				mnozstvi = QTYSHPPD;
	
			while (opakovat)
			{
				opakovat = false;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					Application.DoEvents();

					Fask.MST_W.ProdejService.Disponibilita disponibilita = new Fask.MST_W.ProdejService.Disponibilita();

					disponibilita.DOC_ID = _typdokladu.doc_id;
					disponibilita.DOC_ID2 = _typdokladu.doc_id2;

					disponibilita.ITEMNMBR = ITEMNMBR;
					disponibilita.QTY = mnozstvi;

					disponibilita.SKL_ID = sklad_id;
					disponibilita.LOCNCODE = LOCNCODE;

					disponibilita.SERLTNUM = SERLTNUM;


					info = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_prodej.Disponibilita(disponibilita);

					Cursor.Current = Cursors.Default;
				}
				catch (Exception ex)
				{
					Cursor.Current = Cursors.Default;
					if (MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning) ==
						DialogResult.Retry)
					{
						opakovat = true;
					}
					else
					{
						return false;
					}
				}
			}

			// Je-li disponibilni, tak pokracovat, jinak stop
			if ((info == null) || (info.Status != Fask.MST_W.ProdejService.StatusResultEnum.OK))
			{

				if (Prodej.Globals.DisponibilityZvuk)
				{
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.ProdejSoundDisponibility));
				}

				if (Prodej.Globals.DisponibilityHlaska)
				{
					//Hanibal nechce hlašku ale sou tady ty informace...
					MessageBoxBig.Show("Položku nelze vydat!" + Environment.NewLine + info.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
				}

				return false;
			}


			return true;
		}

		/// <summary>
		/// Metoda pro smazani nasnimane polozky
		/// </summary>
		private void smazatNasnimanou()
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow _di = this.SelectedNasnimane;
			if (_di == null)
				return;

			if (DialogResult.No == MessageBoxBig.Show(
					string.Format(Fask.Localization.Localization.Prodej3ProdejListSmazatNasnimanouPolozkuDotaz, _di.ITEMDESC.Trim(), _di.QTYSHPPD.ToString(Settings.UIFormatDesCisel))
						, Fask.Localization.Localization.Prodej3ProdejListSmazaniPolozky
						, MessageBoxButtons.YesNo
						, MessageBoxBigIcon.Question))
				return;

			try
			{
				ScannerStop();

				#region lokace
				// smazani z lokacniho mechanismu, pokud je lokacni mechanismus zapnuty a jsou zapnuty online pohyby
				if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0 &&
					!_typdokladu.Iscfg_lok_mech_online_pohybyNull() && _typdokladu.cfg_lok_mech_online_pohyby > 0)
				{
					string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
					Fask.MST_W.LokaceService.TypeOfRecord recordType = (Fask.MST_W.LokaceService.TypeOfRecord)Enum.Parse(typeof(Fask.MST_W.LokaceService.TypeOfRecord), pohyb_type, true);

					//Volani sluzby a kontrola navratveho kodu.
					Logging.Log.WriteAdvanced(string.Empty, string.Empty);
					Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + _di.guid, "LocationLog");
					// odstraneni online zaznamu
					Fask.MST_W.LokaceService.StatusLokace sl = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_lokace.DeleteRecordByGuid(_di.guid, recordType);

					Cursor.Current = Cursors.Default;
					switch (sl.State)
					{
						case Fask.MST_W.LokaceService.States.OK:
							break;
						case Fask.MST_W.LokaceService.States.ERROR:
							MessageBoxBig.Show("Nepodaøilo se odstranit záznamy lokací - nasnímané množství nebude smazáno! - " + sl.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						default:
							MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznamy lokací - nasnímané množství nebude smazáno!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
					}

					Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");
				}
				#endregion

				// odstraneni
				int raff = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Delete_DI_ByGUID(_di.guid);
				_di.Delete();
				_prodejTable.AcceptChanges();

			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message + ex.StackTrace, "buttonSmazat_Click_2");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
			}
			finally
			{
				ScannerStart();
			}

			UpdateForm(); //aktualizuje 
		}

		#region TISK

		/// <summary>
		/// Metoda pro tisk Etikety
		/// </summary>
		/// <param name="di"></param>
		/// <returns></returns>
		private bool TiskEtikety(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
		{

			bool? TiskSCenou = null;
			bool vytisteno;
			string tmpSerNumTrack = null;

			if (Prodej.Globals.DialogTisk)
			{
				if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListTiskEtiketyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
					return false;
			}

			#region Priznak Sledovani


			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB))
			{
				tmpSerNumTrack = ConZbo.Get_CZ_SerNum_Track_By_ITEMNMBR(di.ITEMNMBR);
			}
			
			#endregion


			if (Prodej.Globals.EtiketaTiskDotazSCenou)
			{
				if (Prodej.Globals.EtiketaTiskDotazSCenou_ZobrazDialog)
				{
					DialogResult dres = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListTiskEtiketaSCenou, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

					if (dres == DialogResult.Yes)
						TiskSCenou = true;
					else { TiskSCenou = false; }
				}
				else
				{
					TiskSCenou = !Prodej.Globals.EtiketaTiskDotazSCenou_Cena;
				}
			}

			if (Prodej.Globals.Mnozstvi1Auto)
				vytisteno = ProdejTisk.Print(di, PrinterFactory.PrinterModules.ProdejNasnimane, 1, TiskSCenou, tmpSerNumTrack);
			else
			{
				if (Prodej.Globals.EtiketaTisk_PrebiratMnozstvi)
				{
					int TiskQTY = Convert.ToInt32(di.QTYSHPPD);
					Logging.Log.Write("Tisk Prodej , Prebirane Množstvi :" + TiskQTY.ToString());
					vytisteno = ProdejTisk.Print(di, PrinterFactory.PrinterModules.ProdejNasnimane, TiskQTY, TiskSCenou, tmpSerNumTrack);
				}
				else
				{
					vytisteno = ProdejTisk.Print(di, PrinterFactory.PrinterModules.ProdejNasnimane, string.IsNullOrEmpty(Prodej.Globals.PredvyplneneMnozstviEtikety) ? (int?)null : Convert.ToInt32(Prodej.Globals.PredvyplneneMnozstviEtikety), TiskSCenou, tmpSerNumTrack);
				}
			}


			Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", di.CountEntries, null, null, vytisteno.ToString(), null));

			return vytisteno;
		}

		/// <summary>
		/// Metoda tisk Paleta
		/// </summary>
		private void PerformTiskPaleta()
		{
			try
			{
				//DateTime dtStart = DateTime.Now;

				bool vytisteno = false;
				//Logging.Log.WriteDebug("Start zpracování", "START miTiskSoupis");
				ScannerStop();

				if (SelectedNasnimane == null)
				{
					MessageBoxBig.Show("Není vybrán nasnímaný záznam", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}

				//  tisk soupisu
				Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
				List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
				Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

				// Priprava parametru pro tisk soupisu ... 
				// Hlavicka
				dataHlavicka.Add("odb_desc", _odberatel != null ? _odberatel.odb_desc.Trim() : string.Empty);
				dataHlavicka.Add("odb_ico", _odberatel != null && !_odberatel.Isodb_icoNull() ? _odberatel.odb_ico.Trim() : string.Empty);
				dataHlavicka.Add("odb_carcode", _odberatel != null && !_odberatel.Isodb_carcodeNull() ? _odberatel.odb_carcode.Trim() : string.Empty);
				dataHlavicka.Add("odb_cisloOr", _odberatel != null && !_odberatel.Isodb_cisloOrNull() ? _odberatel.odb_cisloOr.Trim() : string.Empty);
				dataHlavicka.Add("odb_dic", _odberatel != null && !_odberatel.Isodb_dicNull() ? _odberatel.odb_dic.Trim() : string.Empty);
				dataHlavicka.Add("odb_Dodavatel", _odberatel != null && !_odberatel.Isodb_DodavatelNull() ? _odberatel.odb_Dodavatel.ToString() : string.Empty);
				dataHlavicka.Add("odb_misto", _odberatel != null && !_odberatel.Isodb_mistoNull() ? _odberatel.odb_misto.Trim() : string.Empty);
				dataHlavicka.Add("odb_Odberatel", _odberatel != null && !_odberatel.Isodb_OdberatelNull() ? _odberatel.odb_Odberatel.ToString() : string.Empty);
				dataHlavicka.Add("odb_psc", _odberatel != null && !_odberatel.Isodb_pscNull() ? _odberatel.odb_psc.Trim() : string.Empty);
				dataHlavicka.Add("odb_ulice", _odberatel != null && !_odberatel.Isodb_uliceNull() ? _odberatel.odb_ulice.Trim() : string.Empty);
				dataHlavicka.Add("NMBRPAL", (SelectedNasnimane != null && !SelectedNasnimane.IsNMBRPALNull()) ? SelectedNasnimane.NMBRPAL.Trim() : string.Empty);

				// jsou povolena strediska
				if ((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska)
				{
					dataHlavicka.Add("str_desc", _stredisko != null && !_stredisko.Isstr_descNull() ? _stredisko.str_desc.Trim() : string.Empty);
					dataHlavicka.Add("str_carcode", _stredisko != null && !_stredisko.Isstr_carcodeNull() ? _stredisko.str_carcode.Trim() : string.Empty);
				}

				// nacteni uzivatele
				dataHlavicka.Add("prac_desc", _pracovnik != null ? (_pracovnik.Isprac_descNull() ? string.Empty : _pracovnik.prac_desc.Trim()) : string.Empty);

				var dtUziv = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_users.GetDataByLogin(MST_Global.UserLoginName);
				if (dtUziv.Count > 0)
				{
					dataHlavicka.Add("LOGIN", MST_Global.UserLoginName ?? string.Empty);
					dataHlavicka.Add("FIRSTNAME", dtUziv.First().IsFIRSTNAMENull() ? string.Empty : dtUziv.First().FIRSTNAME.Trim());
					dataHlavicka.Add("SECONDNAME", dtUziv.First().IsSECONDNAMENull() ? string.Empty : dtUziv.First().SECONDNAME.Trim());
				}

				string mena_id = string.Empty;
				string mena_symbol = string.Empty;
				//decimal? mena_kurz;
				//DateTime? mena_kurzDatum;
				//mena_kurz = null;
				//mena_kurzDatum = null;

				string mena_idM = string.Empty;
				string mena_symbolM = string.Empty;
				decimal? mena_kurzM;
				//DateTime? mena_kurzDatumM;
				mena_kurzM = null;
				//mena_kurzDatumM = null;

				//if (_odberatel != null && !_odberatel.Ismena_IDNull() && String.IsNullOrEmpty(mena_id))
				//    mena_idM = _odberatel.mena_ID.Trim();
				//if (_mena != null)
				//{
				//    if (_mena.mena_hlavni)
				//        mena_id = _mena.mena_ID.Trim();
				//    else
				//    {
				//        mena_idM = _mena.mena_ID.Trim();
				//        var dtHlavniMena = ta_meny.GetDataByHlavni(true);
				//        if (dtHlavniMena.Count > 0)
				//            mena_id = dtHlavniMena[0].mena_ID.Trim();
				//    }
				//}

				//if (string.IsNullOrEmpty(mena_id))
				//{
				//    var dtHlavniMena = ta_meny.GetDataByHlavni(true);
				//    if (dtHlavniMena.Count > 0)
				//    {
				//        menaHlavni = dtHlavniMena[0];
				//        mena_id = menaHlavni.mena_ID.Trim();
				//    }
				//}

				if (String.IsNullOrEmpty(mena_idM))
				{
					var dtDIData = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.GetData_DI();
					//int countNullMenaIDM = 0;
					//if ((countNullMenaIDM = dtDIData.Count(di => di.Ismena_IDMNull())) > 0)
					//{
					//    MessageBoxBig.Show("Existuje " + countNullMenaIDM + " položek bez udané mìny!", "Tisk
					//}
					//var dtDIDataMenaM = dtDIData.Where(di => !di.Ismena_IDMNull());
					//dtDIDataMenaM.Count(di => di.Ismena_IDMNull());
					if (dtDIData.Count(di => !di.Ismena_IDMNull()) > 0)
					{
						var drDIMenaM = dtDIData.First(di => !di.Ismena_IDMNull());
						if (drDIMenaM != null)
						{
							mena_idM = drDIMenaM.mena_IDM.Trim();
						}
					}
				}

				//if (String.IsNullOrEmpty(mena_idM))
				//{
				//    menaVedlejsi = menaHlavni;
				//    mena_idM = mena_id;
				//}
				//else
				//{
				//    var dtMeny = ta_meny.GetDataByMenaID(mena_idM);
				//    if (dtMeny.Count > 0)
				//    {
				//        menaVedlejsi = dtMeny.First();
				//        //mena_idM = menaVedlejsi.mena_ID.Trim(); // <= mena_idM je jiz nastaveno ...
				//    }
				//    else
				//    {
				//        menaVedlejsi = menaHlavni;
				//        mena_idM = mena_id;
				//    }
				//}

				//// nastaveni kurzu pro vedlejsi menu ...
				//if (menaVedlejsi == null)
				//    mena_kurzM = null;
				//else if (menaVedlejsi.mena_hlavni)
				//    mena_kurzM = 1; // Pokud je hlavni mena, tak se neprepocitava ...
				//else if (!menaVedlejsi.Ismena_kurzNull())
				//    mena_kurzM = menaVedlejsi.mena_kurz;
				//else
				//{
				//    // TODO : ??? nastavit kurz ??? => muze se zmenit prepnutim na jinou menu...
				//    mena_kurzM = null; // defaultne se bude davat nula (0), jako ze neni kurz ...
				//}


				//SqlCEDBs.DataSets.Meny.CZMST097Row menarow = null;
				//if (String.IsNullOrEmpty(mena_id))
				//{
				//    Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable menadt = menata.GetDataByHlavni(true);
				//    if (menadt.Count > 0)
				//    {
				//        menarow = menadt[0];
				//        mena_id = menarow.mena_ID.Trim();
				//        if (!menarow.Ismena_kurzNull())
				//            mena_kurz = menarow.mena_kurz;
				//        if (!menarow.Ismena_kurzDatumNull())
				//            mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}
				//else
				//{
				//    Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable menadt = menata.GetDataByMenaID(mena_id);
				//    if (menadt.Count > 0)
				//    {
				//        menarow = menadt[0];
				//        if (!menarow.Ismena_kurzNull())
				//            mena_kurz = menarow.mena_kurz;
				//        if (!menarow.Ismena_kurzDatumNull())
				//            mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}

				//if (menarow != null && !menarow.mena_hlavni)
				//{ //Pokud je nastavena mena a neni hlavni, tak dochazi k prepoctu kurzem do teto meny ...
				//    if (menarow.Ismena_kurzNull())
				//    { //Neni zadany kurz meny
				//        string mena_kurz_str = string.Empty;
				//        while (true)
				//        {
				//            if (DialogResult.Cancel == InputBox.Show("Zadejte kurz mìny '" + menarow.mena_text + "'", mena_kurz_str, out mena_kurz_str))
				//                return;
				//            try
				//            {
				//                menarow.mena_kurz = decimal.Parse(mena_kurz_str);
				//                menarow.mena_kurzDatum = DateTime.Now;
				//                break;
				//            }
				//            catch 
				//            {
				//            }
				//        }
				//        mena_kurz = menarow.mena_kurz;
				//        mena_kurzDatum = menarow.mena_kurzDatum;
				//    }
				//}

				// Mapovani men na zastupne symboly
				mena_symbol = _Translations.Meny.GetSymbol(mena_id);
				mena_symbolM = _Translations.Meny.GetSymbol(mena_idM);

				dataHlavicka.Add("mena_id", mena_id);
				dataHlavicka.Add("mena_symbol", mena_symbol);
				dataHlavicka.Add("mena_idM", mena_idM);
				dataHlavicka.Add("mena_symbolM", mena_symbolM);
				//dataHlavicka.Add("mena_kurz", mena_kurz.HasValue ? mena_kurz.Value.ToString("0.00") : "-");
				//dataHlavicka.Add("mena_kurzDatum", mena_kurzDatum.HasValue ? mena_kurzDatum.Value.ToString() : "-");

				//V jake mene se chce tisknout?
				string mena_id_print = string.Empty;
				if (_odberatel != null && !_odberatel.Ismena_IDNull())
					mena_id_print = _odberatel.mena_ID.Trim();
				if (_mena != null)
					mena_id_print = _mena.mena_ID.Trim();
				dataHlavicka.Add("mena_id_print", mena_id_print);

				// Radky
				// promenne pro paticku
				decimal p_sum_mn = 0; // celkem mnozstvi
				decimal p_tax = 0; //pouzita dan

				decimal p_sum_bdph = 0; // celkem cena bez DPH
				decimal p_sum_sdph = 0; // celkem cena s DPH
				decimal p_sum_dph = 0; // celkem DPH za vse

				decimal p_sum_bdphM = 0; // celkem cena bez DPH
				decimal p_sum_sdphM = 0; // celkem cena s DPH
				decimal p_sum_dphM = 0; // celkem DPH za vse

				// data radku ... 
				Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdi = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
				//ta_di.Fill(dtdi);

				//fill by nmbrpal ...
				Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.Fill_DI_ByNmbrpal(dtdi, (SelectedNasnimane != null && !SelectedNasnimane.IsNMBRPALNull()) ? SelectedNasnimane.NMBRPAL.Trim() : string.Empty);
				//Logging.Log.WriteDebug("KONEC hlavicka");
				//Logging.Log.WriteDebug("START nacitani (foreach) CZMST_DI, pocet zaznamu: " + dtdi.Count);

				//SqlCEDBs.DataSets.Zbozi.CZMST095DataTable dt095 = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
				//ta_zbozi.FillPolozkacisloPopis(dt095);
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB))
				{
					#region ForEach

					ConZbo.Connection_Open();

					try
					{
						//int cisloZaznamu = 0;
						foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow drdi in dtdi)
						{
							//++cisloZaznamu;                    
							//Logging.Log.WriteDebug("Zacatek zpracovani zaznamu c. " + cisloZaznamu);

							Dictionary<string, string> dataRadek = new Dictionary<string, string>();
							//dataRadek.Add("index", cisloZaznamu.ToString());
							dataRadek.Add("ITEMNMBR", drdi.ITEMNMBR.Trim());
							dataRadek.Add("VNDITNUM", drdi.IsVNDITNUMNull() ? string.Empty : drdi.VNDITNUM.Trim());
							dataRadek.Add("CZ_CarKod", drdi.IsCZ_CarKodNull() ? string.Empty : drdi.CZ_CarKod.Trim());

							//dataRadek.Add("ITEMDESC", drdi.ITEMDESC.Trim());
							try
							{
								if (!drdi.IsITEMDESCNull())
								{
									dataRadek.Add("ITEMDESC", drdi.ITEMDESC.Trim());
								}
								else
									dataRadek.Add("ITEMDESC", ConZbo.GetDataByPolozkacisloLike(drdi.ITEMNMBR)[0].ITEMDESC.Trim());
								//dataRadek.Add("ITEMDESC", drdi.ITEMNMBR);

							}
							catch
							{
								dataRadek.Add("ITEMDESC", "?");
							}

							dataRadek.Add("ITEMCODE", drdi.IsITEMCODENull() ? string.Empty : drdi.ITEMCODE.Trim());
							dataRadek.Add("REZ_1", drdi.REZ_1.Trim());
							dataRadek.Add("REZ_2", drdi.REZ_2.Trim());
							dataRadek.Add("QTYSHPPD", drdi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("QTYPACK", drdi.IsQTYPACKNull() ? string.Empty : drdi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("QTYSHPPDMJ", drdi.QTYSHPPDMJ.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
							dataRadek.Add("NMBRPAL", drdi.IsNMBRPALNull() ? string.Empty : drdi.NMBRPAL.Trim());

							decimal priceMJTaxWith = 0;       // cena/MJ s DPH[mena]
							decimal priceMJTaxWithout = 0;    // cena/MJ bez DPH[mena]
							decimal priceMJTax = 0;           // MJ DPH[mena]
							decimal priceTaxWith = 0;       // cena s DPH[mena]
							decimal priceTaxWithout = 0;    // cena bez DPH[mena]
							decimal priceTax = 0;           // DPH[mena]
							decimal priceMJTaxWithM = 0;       // cena/MJ s DPH[mena]
							decimal priceMJTaxWithoutM = 0;    // cena/MJ bez DPH[mena]
							decimal priceMJTaxM = 0;           // MJ DPH[mena]
							decimal priceTaxWithM = 0;       // cena s DPH[mena]
							decimal priceTaxWithoutM = 0;    // cena bez DPH[mena]
							decimal priceTaxM = 0;           // DPH[mena]
							decimal Tax = 0;                // vyse DPH[%]
							string MJ = string.Empty;

							MJ = drdi.MJ.Trim();

							if (drdi.IsWITHTAXNull())
							{
								if (drdi.WITHTAX == 1)
								{
									priceMJTaxWith = drdi.AMOUNPIE;
									priceMJTaxWithout = drdi.AMOUNPIE - drdi.TAXAMPIE;
									priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD;
									priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD - drdi.TAXAMPIE * drdi.QTYSHPPD;

									if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
										&& mena_idM == drdi.mena_IDM.Trim())
									{ // je mena a je cena za kus a je dan za kus, tak spocitam toto
										// a mena se shoduje s touto menou ... 
										priceMJTaxWithM = drdi.AMOUNPIEM;
										priceMJTaxWithoutM = drdi.AMOUNPIEM - drdi.TAXAMPIEM;
										priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
										priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD - drdi.TAXAMPIEM * drdi.QTYSHPPD;
									}
									else
									{ // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
										// pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
										priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
										priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
										priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
										priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
									}
								}
								else
								{
									priceMJTaxWithout = drdi.AMOUNPIE;
									priceMJTaxWith = drdi.AMOUNPIE + drdi.TAXAMPIE;
									priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD;
									priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD + drdi.TAXAMPIE * drdi.QTYSHPPD;

									if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
										&& mena_idM == drdi.mena_IDM.Trim())
									{
										priceMJTaxWithoutM = drdi.AMOUNPIEM;
										priceMJTaxWithM = drdi.AMOUNPIEM + drdi.TAXAMPIEM;
										priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
										priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD + drdi.TAXAMPIEM * drdi.QTYSHPPD;
									}
									else
									{ // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
										// pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
										priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
										priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
										priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
										priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
									}
								}
								priceMJTax = priceMJTaxWith - priceMJTaxWithout;
								priceTax = priceTaxWith - priceTaxWithout;

								priceMJTaxM = priceMJTaxWithM - priceMJTaxWithoutM;
								priceTaxM = priceTaxWithM - priceTaxWithoutM;

								Tax = Math.Round((priceTax / (priceTaxWithout == 0 ? 1 : priceTaxWithout)) * 100);

								////Prepocet do meny
								//if (menarow != null && !menarow.mena_hlavni)
								//{ //Pokud je nastavena mena a neni hlavni, tak dochazi k prepoctu kurzem do teto meny ...
								//    priceMJTax = priceMJTax * menarow.mena_kurz;
								//    priceMJTaxWith = priceMJTaxWith * menarow.mena_kurz;
								//    priceMJTaxWithout = priceMJTaxWithout * menarow.mena_kurz;
								//    priceTax = priceTax * menarow.mena_kurz;
								//    priceTaxWith = priceTaxWith * menarow.mena_kurz;
								//    priceTaxWithout = priceTaxWithout * menarow.mena_kurz;
								//}

								// Cena MJ
								dataRadek.Add("pricemjtaxwith", priceMJTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxwithout", priceMJTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtax", priceMJTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena MJ v mene
								dataRadek.Add("pricemjtaxwithM", priceMJTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxwithoutM", priceMJTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricemjtaxM", priceMJTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena celkem za radek
								dataRadek.Add("pricetaxwith", priceTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxwithout", priceTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetax", priceTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Cena celkem za radek v mene
								dataRadek.Add("pricetaxwithM", priceTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxwithoutM", priceTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								dataRadek.Add("pricetaxM", priceTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
								// Sazba dane
								dataRadek.Add("tax", Tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));

								dataRadek.Add("mena_id", mena_id);
								dataRadek.Add("mena_symbol", mena_symbol);
								dataRadek.Add("mena_idM", mena_idM);
								dataRadek.Add("mena_symbolM", mena_symbolM);

								// V jake mene se bude tisknout
								//dataRadek.Add("mena_id_print", mena_id_print);
							}
							dataRadky.Add(dataRadek);

							//vypocet sum pro paticku ...
							p_sum_mn += drdi.QTYSHPPD;
							p_tax = Tax;

							p_sum_bdph += priceTaxWithout;
							p_sum_sdph += priceTaxWith;
							p_sum_dph += priceTax;

							p_sum_bdphM += priceTaxWithoutM;
							p_sum_sdphM += priceTaxWithM;
							p_sum_dphM += priceTaxM;
						}
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
					}
					finally
					{
						ConZbo.Connection_Close();
					}

					#endregion
				}

				//Logging.Log.WriteDebug("KONEC nacitani (foreach) CZMST_DI");

				// Paticka
				dataPaticka.Add("sum_mn", p_sum_mn.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("tax", p_tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));
				// Suma
				dataPaticka.Add("sum_bdph", p_sum_bdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_sdph", p_sum_sdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_dph", p_sum_dph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				// Suma v mene
				dataPaticka.Add("sum_bdphM", p_sum_bdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_sdphM", p_sum_sdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("sum_dphM", p_sum_dphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

				dataPaticka.Add("datetime", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
				dataPaticka.Add("mena_id", mena_id);
				dataPaticka.Add("mena_symbol", mena_symbol);
				dataPaticka.Add("mena_idM", mena_idM);
				dataPaticka.Add("mena_symbolM", mena_symbolM);
				//dataPaticka.Add("mena_kurz", mena_kurz.HasValue ? mena_kurz.Value.ToString("0.00") : "-");
				//dataPaticka.Add("mena_kurzDatum", mena_kurzDatum.HasValue ? mena_kurzDatum.Value.ToString() : "-");

				// V jake mene se bude tisknout
				//dataPaticka.Add("mena_id_print", mena_id_print);

				// 30.6.2016 PeV: na zadost JaS automaticke predvyplneni mnozstvi tisku 1, TODO: konfiguracne ...
				//vytisteno = ProdejTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, null);   // TODO: konfiguracne ... //string.IsNullOrEmpty(Prodej.Globals.PredvyplneneMnozstviSoupisu) ? (int?)null : Convert.ToInt32(Prodej.Globals.PredvyplneneMnozstviSoupisu));                
				vytisteno = ProdejTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, 1);
				Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "4", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", null, null, null, vytisteno.ToString(), null));
				//TimeSpan tsDiff = dtStart - DateTime.Now;
				//Logging.Log.WriteDebug("Konec zpracování, celkový èas: " + tsDiff.ToString(), "KONEC miTiskSoupis");
				//MessageBoxBig.Show("Konec zpracování, celkový èas: " + tsDiff.ToString() , this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex, "PerformTiskPaleta");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				ScannerStart();
			}
		}

		#endregion

		#region Online

		/// <summary>
		/// Metoda pro zobrazeni poctu kusu na vybrane polozce online
		/// </summary>
		private void DetailPocetKusu()
		{
			try
			{
				this.ScannerStop();
				if (SelectedZbozi == null)
				{
					//MessageBoxBig.Show("Není vybrána položka", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkaNeniVybrana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}
				string item = SelectedZbozi.ITEMNMBR.Trim();
				string itemdesc = SelectedZbozi.ITEMDESC.Trim();
				using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, itemdesc))
				{
					pkusu.ShowDialog();
				}
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

		/// <summary>
		/// Metoda pro zobrazeni poctu kusu na vybrane polozce s Lokaci online
		/// </summary>
		private void DetailPocetKusuLokace()
		{
			try
			{
				this.ScannerStop();
				if (SelectedZbozi == null)
				{
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkaNeniVybrana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}
				string item = SelectedZbozi.ITEMNMBR.Trim();
				string lokace = SelectedZbozi.LOCNCODE.Trim();
				string itemdesc = SelectedZbozi.ITEMDESC.Trim();
				using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, lokace, itemdesc))
				{
					pkusu.ShowDialog();
				}
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

		/// <summary>
		/// Metoda pro zobrazeni detailu o polozce online
		/// </summary>
		private void DetailItemnumber()
		{
			try
			{
				this.ScannerStop();
				if (SelectedZbozi == null)
				{
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListPolozkaNeniVybrana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}
				string item = SelectedZbozi.ITEMNMBR.Trim();
				//string dokl = _typdokladu == null ? string.Empty : _typdokladu.doc_id;
				string dokl = string.Empty;
				using (Informations.OnLineItemumberGrid detailpolozka = new Fask.MST_W.Informations.OnLineItemumberGrid(item, dokl))
				{
					detailpolozka.ShowDialog();
				}
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


		#endregion

		#region Najít

		/// <summary>
		/// Metoda pro hledani polozky podle Caroveho Kodu
		/// </summary>
		private void najdiCarovyKod()
		{
			//Vstupnim typem je vyhledani
			_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);

			try
			{
				ScannerStop();

				filtrRemove();

				using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejListZadejteCarovyKod, SejmiKodForm.TypeOfCode.AlphaNumeric))
				{
					if (skf.ShowDialog() == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						//_carovykod_posledni = skf.Kod;
						HledaniStatus found = NajdiPolozkuPodleCK(skf.Kod, null);
						Cursor.Current = Cursors.Default;

						if (found == HledaniStatus.Nenalezeno)
						{
							if (!Prodej.Globals.PovolitNovouPolozku)
							{
								MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNenalezena, skf.Kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
							else
							{
								pridatPolozkuNeexistujici(skf.Kod);
							}
						}
						else if (found == HledaniStatus.NalezenJedenZaznam)
						{
							pridatPolozku(this.SelectedZbozi);
						}
						else if (found == HledaniStatus.NalezenoViceZaznamu)
						{
							MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuVyberRucne, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							FindByDefaultMJ();
							zobrazeniList();
						}
						else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
						{
							MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani, Prodej.Globals.GridViewRowCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				ScannerStart();
			}

		}

		/// <summary>
		/// Metoda pro hledani polozky podle Nazvu
		/// </summary>
		private void najdiNazev()
		{
			//Vstupni metodou muze byt vyhledani, pokud je jedna polozka
			_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);

			try
			{
				ScannerStop();

				filtrRemove();

				using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejListZadejteCastNazvuPolozky, SejmiKodForm.TypeOfCode.AlphaNumeric))
				{
					if (skf.ShowDialog() == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
						HledaniStatus found = NajdiPolozkuPodleNazvu(skf.Kod);
						Cursor.Current = Cursors.Default;
						if (found == HledaniStatus.Nenalezeno)
						{
							MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaObsahujiciTextNenalezena, skf.Kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						}
						else if (found == HledaniStatus.NalezenJedenZaznam)
						{
							pridatPolozku(this.SelectedZbozi);
						}
						else if (found == HledaniStatus.NalezenoViceZaznamu)
						{
							MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuVyberRucne, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							FindByDefaultMJ();
							zobrazeniList();
						}
						else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
						{
							MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani, Prodej.Globals.GridViewRowCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro hledani polozky podle ITEMNMBR
		/// </summary>
		private void najdiPolozkaCislo()
		{
			//Muze byt vstupnim typem vyhledani, pokud opdovida jedne polozce
			_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);

			try
			{
				ScannerStop();

				filtrRemove();

				//8.8.2018 JiS Numeric zmenen na alfanumeric, dle testu pneusafr...
				using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejListZadejteCastCislaPolozky, SejmiKodForm.TypeOfCode.AlphaNumeric))
				{
					if (skf.ShowDialog() == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
						HledaniStatus found = NajdiPolozkuPodleCisla(skf.Kod);
						Cursor.Current = Cursors.Default;
						if (found == HledaniStatus.Nenalezeno)
						{
							MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaObsahujiciKodNenalezena, skf.Kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						}
						else if (found == HledaniStatus.NalezenJedenZaznam)
						{
							pridatPolozku(this.SelectedZbozi);
						}
						else if (found == HledaniStatus.NalezenoViceZaznamu)
						{
							MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuVyberRucne, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							FindByDefaultMJ();
							zobrazeniList();
						}
						else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
						{
							MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani, Prodej.Globals.GridViewRowCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro hledani polozky podle ITEMCODE
		/// </summary>
		private void najdiKodPolozky()
		{
			//Muze byt vstupnim typem vyhledani, pokud opdovida jedne polozce
			_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);

			try
			{
				ScannerStop();

				filtrRemove();

				//using (SejmiKodForm skf = new SejmiKodForm("Zadejte kód položky", SejmiKodForm.TypeOfCode.AlphaNumeric))
				using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejListZadejteKodPolozky, SejmiKodForm.TypeOfCode.AlphaNumeric))
				{
					if (skf.ShowDialog() == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
						HledaniStatus found = NajdiPolozkuPodleCode(skf.Kod);
						Cursor.Current = Cursors.Default;
						if (found == HledaniStatus.Nenalezeno)
						{
							//MessageBoxBig.Show("Položka obsahující kód '" + skf.Kod + "' nenalezena.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaObsahujiciKodNenalezena, skf.Kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						}
						else if (found == HledaniStatus.NalezenJedenZaznam)
						{
							pridatPolozku(this.SelectedZbozi);
						}
						else if (found == HledaniStatus.NalezenoViceZaznamu)
						{
							//MessageBoxBigTimeout.Show("Nalezeno více záznamù.\nVyberte odpovídající ze seznamu", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuVyberRucne, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							FindByDefaultMJ();
							zobrazeniList();
						}
						else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
						{
							MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani, Prodej.Globals.GridViewRowCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro naèteni všeho
		/// </summary>
		/// <param name="indexStart"></param>
		/// <param name="indexEnd"></param>
		private void LoadZbozi(int indexStart, int indexEnd)
		{

            Cursor.Current = Cursors.WaitCursor;

            string cmdsort = string.Empty;
            if (_db_sort != string.Empty)
            {
                cmdsort = " order by " + _db_sort;
                SetSortText();
            }

            try
            {
                if (Prodej.Globals.PouzitSklady && Prodej.Globals.FiltrCiselnikSkladu && this._skladZdroj != null)
                {
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.Load_By_Sklid(_katalogZbozi.CZMST095, this._skladZdroj.skl_id, cmdsort, indexStart, indexEnd);
                }
                else
                {
                    Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.Load_All(_katalogZbozi.CZMST095, cmdsort, indexStart, indexEnd);
                }

                // aktualizace nazvu skladu pro polozky zbozi
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

			try
			{
				dataGrid1.CurrentCell = new DataGridCell(0, 1);
				dataGrid1.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

		}

		/// <summary>
		/// Vyhledava polozky dle ITEMNMBR
		/// </summary>
		/// <param name="cislo"></param>
		/// <returns>0=nenalezeno, 1=nalezen jeden zaznam, 2=nalezeno vice zaznamu, 3=nalezeno vice jak 100zaznamu</returns>
		private HledaniStatus NajdiPolozkuPodleCisla(string cislo)
		{
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			string filtr = "%" + cislo + "%";

			int pocetzaznamu = 0;
			//if (this._sklad == null)
			//    pocetzaznamu = (int?)zta.CountByPolozkacislo(filtr) ?? 0;
			//else
			//    pocetzaznamu = (int?)zta.CountByPolozkacisloSklad(filtr, this._sklad.skl_id) ?? 0;

			// 28.6.2016 PeV: uprava, aby se zohlednoval filtr dle ciselniku skladu
			//if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
			if ((!Prodej.Globals.FiltrCiselnikSkladu || this._skladZdroj == null) && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
				pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkacislo(filtr) ?? 0;
			else if (this._skladZdroj == null && this._odberatel != null)
				pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkacisloOdbId(filtr, this._odberatel.odb_id) ?? 0;
			else if (this._skladZdroj != null) // && this._odberatel == null)
				pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkacisloSklad(filtr, this._skladZdroj.skl_id) ?? 0;
			else
			{
				//ToDo:....
			}


			if (pocetzaznamu > Prodej.Globals.GridViewRowCount)
				return HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu;
			else if (pocetzaznamu <= 0)
				return HledaniStatus.Nenalezeno;

			//if (this._sklad == null)
			//    zta.FillByPolozkacislo(_katalogZbozi.CZMST095, filtr);
			//else
			//    zta.FillByPolozkacisloSklad(_katalogZbozi.CZMST095, filtr, this._sklad.skl_id);

			if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
				Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkacisloLike(_katalogZbozi.CZMST095, filtr);
			else if (this._skladZdroj == null && this._odberatel != null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkaCisloAndOdbId(_katalogZbozi.CZMST095, filtr, this._odberatel.odb_id);
			else if (this._skladZdroj != null) // && this._odberatel == null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkacisloSkladLike(_katalogZbozi.CZMST095, filtr, this._skladZdroj.skl_id);
			else
			{
				//ToDo:....
			}

			// Aktualizace pomocnych popisku
            Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);


			if (_katalogZbozi.CZMST095.Rows.Count == 1)
				_zbozi = _katalogZbozi.CZMST095[0];
			else
				_zbozi = null;

			try
			{
				dataGrid1.CurrentCell = new DataGridCell(0, 1);
				dataGrid1.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

			Cursor.Current = Cursors.Default;

			//if (_zbozi != null)
			//{
			//    return true;
			//}
			//else if (_zbozi == null && _katalogZbozi.CZMST095.Rows.Count > 1)
			//{
			//    return true;
			//}
			//else
			//{
			//    return false;
			//}

			if (_katalogZbozi.CZMST095.Count == 0)
				return HledaniStatus.Nenalezeno;
			else if (_katalogZbozi.CZMST095.Count == 1)
				return HledaniStatus.NalezenJedenZaznam;
			else //if (_katalogZbozi.CZMST095.Count > 1)
				return HledaniStatus.NalezenoViceZaznamu;

		}

		/// <summary>
		/// Vyhledava polozky dle ITEMCODE
		/// </summary>
		/// <param name="cislo"></param>
		/// <returns>0=nenalezeno, 1=nalezen jeden zaznam, 2=nalezeno vice zaznamu, 3=nalezeno vice jak 100zaznamu</returns>
		private HledaniStatus NajdiPolozkuPodleCode(string code)
		{
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			string filtr = "%" + code + "%";

			int pocetzaznamu = 0;
			//if (this._sklad == null)
			//    pocetzaznamu = (int?)zta.CountByPolozkaCode(filtr) ?? 0;
			//else
			//    pocetzaznamu = (int?)zta.CountByPolozkaCodeSklad(filtr, this._sklad.skl_id) ?? 0;

			// 28.6.2016 PeV: uprava, aby se zohlednoval filtr dle ciselniku skladu
			//if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
			if ((!Prodej.Globals.FiltrCiselnikSkladu || this._skladZdroj == null) && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
                pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkaCode(filtr) ?? 0;
			else if (this._skladZdroj == null && this._odberatel != null)
                pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkaCodeOdbId(this._odberatel.odb_id, filtr) ?? 0;
			else if (this._skladZdroj != null) // && this._odberatel == null)
                pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByPolozkaCodeSklad(this._skladZdroj.skl_id, filtr) ?? 0;
			else
			{
				//ToDo:....
			}

			if (pocetzaznamu > Prodej.Globals.GridViewRowCount)
				return HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu;
			else if (pocetzaznamu <= 0)
				return HledaniStatus.Nenalezeno;

			//if (this._sklad == null)
			//    zta.FillByPolozkaCode(_katalogZbozi.CZMST095, filtr);
			//else
			//    zta.FillByPolozkaCodeSklad(_katalogZbozi.CZMST095, filtr, this._sklad.skl_id);

			if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkaCodeLike(_katalogZbozi.CZMST095, filtr);
			else if (this._skladZdroj == null && this._odberatel != null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkaCodeOdbIdLike(_katalogZbozi.CZMST095, filtr, this._odberatel.odb_id);
			else if (this._skladZdroj != null) // && this._odberatel == null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByPolozkaCodeSkladLike(_katalogZbozi.CZMST095, this._skladZdroj.skl_id, filtr);
			else
			{
				//ToDo:....
			}

			// Aktualizace pomocnych popisku
            Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);

			if (_katalogZbozi.CZMST095.Rows.Count == 1)
				_zbozi = _katalogZbozi.CZMST095[0];
			else
				_zbozi = null;

			try
			{
				dataGrid1.CurrentCell = new DataGridCell(0, 1);
				dataGrid1.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

			Cursor.Current = Cursors.Default;

			if (_katalogZbozi.CZMST095.Count == 0)
				return HledaniStatus.Nenalezeno;
			else if (_katalogZbozi.CZMST095.Count == 1)
				return HledaniStatus.NalezenJedenZaznam;
			else //if (_katalogZbozi.CZMST095.Count > 1)
				return HledaniStatus.NalezenoViceZaznamu;

		}

		/// <summary>
		/// Vyhledava polozky dle castecneho nazvu polozky
		/// </summary>
		/// <param name="cislo"></param>
		/// <returns>0=nenalezeno, 1=nalezen jeden zaznam, 2=nalezeno vice zaznamu, 3=nalezeno vice jak 100zaznamu</returns>
		private HledaniStatus NajdiPolozkuPodleNazvu(string nazev)
		{
			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			string filtr = "%" + nazev + "%";

			int pocetzaznamu = 0;


			//if (this._sklad == null)
			//    pocetzaznamu = (int?)zta.CountByNazev(filtr) ?? 0;
			//else
			//    pocetzaznamu = (int?)zta.CountByNazevSklad(filtr, this._sklad.skl_id) ?? 0;

			// 28.6.2016 PeV: uprava, aby se zohlednoval filtr dle ciselniku skladu
			//if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
			if ((!Prodej.Globals.FiltrCiselnikSkladu || this._skladZdroj == null) && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
				pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByNazev(filtr) ?? 0;
			else if (this._skladZdroj == null && this._odberatel != null)
                pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByNazevOdbId(filtr, this._odberatel.odb_id) ?? 0;
			else if (this._skladZdroj != null) // && this._odberatel == null)
				pocetzaznamu = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.CountByNazevSklad(filtr, this._skladZdroj.skl_id) ?? 0;
			else
			{
				//ToDo:....
			}

			if (pocetzaznamu > Prodej.Globals.GridViewRowCount)
				return HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu;
			else if (pocetzaznamu <= 0)
				return HledaniStatus.Nenalezeno;

			//if (this._sklad == null)
			//   zta.FillByNazev(_katalogZbozi.CZMST095, filtr);
			//else
			//zta.FillByNazevSklad(_katalogZbozi.CZMST095, filtr, this._sklad.skl_id);

			if (this._skladZdroj == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByNazev(_katalogZbozi.CZMST095, filtr);
			else if (this._skladZdroj == null && this._odberatel != null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByNazevOdbID(_katalogZbozi.CZMST095, filtr, this._odberatel.odb_id);
			else if (this._skladZdroj != null) // && this._odberatel == null)
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByNazevSklad(_katalogZbozi.CZMST095, filtr, this._skladZdroj.skl_id);
			else
			{
				//ToDo:....
			}

			// Aktualizace pomocnych popisku
            Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);

			if (_katalogZbozi.CZMST095.Rows.Count == 1)
				_zbozi = _katalogZbozi.CZMST095[0];
			else
				_zbozi = null;

			try
			{
				dataGrid1.CurrentCell = new DataGridCell(0, 1);
				dataGrid1.CurrentCell = new DataGridCell(0, 0);
			}
			catch { }

			Cursor.Current = Cursors.Default;

			//if (_zbozi != null)
			//{
			//    return true;
			//}
			//else if (_zbozi == null && _katalogZbozi.CZMST095.Rows.Count > 1)
			//{
			//    return true;
			//}
			//else
			//{
			//    return false;
			//}

			if (_katalogZbozi.CZMST095.Count == 0)
				return HledaniStatus.Nenalezeno;
			else if (_katalogZbozi.CZMST095.Count == 1)
				return HledaniStatus.NalezenJedenZaznam;
			else //if (_katalogZbozi.CZMST095.Count > 1)
				return HledaniStatus.NalezenoViceZaznamu;

		}

		/// <summary>
		/// Vyhledava polozky dle èaroveho kodu
		/// </summary>
		/// <param name="cislo"></param>
		/// <returns>0=nenalezeno, 1=nalezen jeden zaznam, 2=nalezeno vice zaznamu, 3=nalezeno vice jak 100zaznamu</returns>
		private HledaniStatus NajdiPolozkuPodleCK(string ck, Fask.Parsing.Codes.BaseCode code)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				//Application.DoEvents();

				#region old
				//if (miNajitCKFiltr.Checked)
				//{
				//    ck += '%';
				//    if (this._sklad == null)
				//    {
				//        if ((zta.CountByCarKodLike(ck, ck) ?? 0) > 100)
				//            return HledaniStatus.NalezenoViceJak100Zaznamu;
				//        zta.FillByCarKodLike(_katalogZbozi.CZMST095, ck, ck);
				//    }
				//    else
				//    {
				//        if ((zta.CountByCarKodSkladLike(ck, this._sklad.skl_id, ck) ?? 0) > 100)
				//            return HledaniStatus.NalezenoViceJak100Zaznamu;
				//        zta.FillByCarKodSkladLike(_katalogZbozi.CZMST095, ck, this._sklad.skl_id, ck);
				//    }
				//}
				//else
				//{ 
				#endregion

				if (Prodej.Globals.PolozkyVyhledatPomociSarze)
				{

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSarze) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze)))
						ck = ((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerialNumber) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN)))
						ck = ((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;


					if ((!Prodej.Globals.FiltrCiselnikSkladu || this._skladZdroj == null) && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKodSarze(_katalogZbozi.CZMST095, ck);
					else if (this._skladZdroj == null && this._odberatel != null)
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKodOdbIdSarze(_katalogZbozi.CZMST095, ck, this._odberatel.odb_id);
					else if (this._skladZdroj != null) // && this._odberatel == null)
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKodSkladSarze(_katalogZbozi.CZMST095, ck, this._skladZdroj.skl_id);
					else
					{
						//ToDo: ...sklad i odberatel jsou zvoleni...
					}
				}
				else
				{
					if ((code != null) && (code is Parsing.Codes.Interfaces.ICodeBarcode) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode))
						ck = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode;

					if ((!Prodej.Globals.FiltrCiselnikSkladu || this._skladZdroj == null) && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKod(_katalogZbozi.CZMST095, ck);
					else if (this._skladZdroj == null && this._odberatel != null)
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKodOdbId(_katalogZbozi.CZMST095, ck, this._odberatel.odb_id);
					else if (this._skladZdroj != null) // && this._odberatel == null)
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_zbozi.FillByCarKodSklad(_katalogZbozi.CZMST095, ck, this._skladZdroj.skl_id);
					else
					{
						//ToDo: ...sklad i odberatel jsou zvoleni...
					}
				}

				#region OLD
				//if (this._sklad == null && (this._odberatel == null || !Prodej.Globals.FiltrDodavatele))
				//    ta_zbozi.FillByCarKod(_katalogZbozi.CZMST095, ck);
				//else if (this._sklad == null && this._odberatel != null)
				//    ta_zbozi.FillByCarKodOdbId(_katalogZbozi.CZMST095, ck, this._odberatel.odb_id);
				//else if(this._sklad != null) // && this._odberatel == null)
				//    ta_zbozi.FillByCarKodSklad(_katalogZbozi.CZMST095, ck, this._sklad.skl_id);
				//else
				//{
				//    //ToDo: ...sklad i odberatel jsou zvoleni...
				//} 
				#endregion

				// Aktualizace pomocnych popisku
                Prodej_3.ProdejMain.prodejInstance.globalObject.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);

				if (_katalogZbozi.CZMST095.Rows.Count == 1)
					_zbozi = _katalogZbozi.CZMST095[0];
				else
					_zbozi = null;


				try
				{
					dataGrid1.CurrentCell = new DataGridCell(0, 1);
					dataGrid1.CurrentCell = new DataGridCell(0, 0);
				}
				catch { }

				//if (_zbozi != null)
				//{
				//    return true;
				//}
				//else if (_zbozi == null && _katalogZbozi.CZMST095.Rows.Count > 1)
				//{
				//    return true;
				//}
				//else
				//{
				//    return false;
				//}

				if (_katalogZbozi.CZMST095.Count == 0)
					return HledaniStatus.Nenalezeno;
				else if (_katalogZbozi.CZMST095.Count == 1)
					return HledaniStatus.NalezenJedenZaznam;
				else if (_katalogZbozi.CZMST095.Count > Prodej.Globals.GridViewRowCount)
					return HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu;
				else
					return HledaniStatus.NalezenoViceZaznamu;

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				return HledaniStatus.Nenalezeno;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion

		#region Setøídit

		private bool sortItemdesc = false;

		/// <summary>
		/// Metoda slouží pro setøitrizeni podle nazvu položky / ITEMDESC
		/// </summary>
		private void SortItemdesc()
		{
			sortItemdesc = !sortItemdesc;
			_db_sort = (sortItemdesc ? cSortItemdescASC : cSortItemdescDESC);
			LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);
			SetSortText();

			//sortItemdesc = !sortItemdesc;
			//_katalogZboziView.Sort = (sortItemdesc ? cSortItemdescASC : cSortItemdescDESC);
			//_db_sort = _katalogZboziView.Sort;
			//SetSortText();
		}

		private bool sortItemnmbr = false;

		/// <summary>
		/// Metoda slouží pro setøitrizeni podle ID položky / ITEMNMBR
		/// </summary>
		private void SortItemnmbr()
		{
			sortItemnmbr = !sortItemnmbr;
			_db_sort = (sortItemnmbr ? cSortItemnmbrASC : cSortItemnmbrDESC);
			LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);
			SetSortText();

			//sortItemnmbr = !sortItemnmbr;
			//_katalogZboziView.Sort = (sortItemnmbr ? cSortItemnmbrASC : cSortItemnmbrDESC);
			//_db_sort = _katalogZboziView.Sort;
			//SetSortText();
		}

		/// <summary>
		/// Metoda pro defaultny sort
		/// </summary>
		private void SortDefault()
		{
			_katalogZboziView.ApplyDefaultSort = true;
			_db_sort = _katalogZboziView.Sort;
			LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);
			this.Text = this.TextBase;

			//_katalogZboziView.ApplyDefaultSort = true;
			//_db_sort = _katalogZboziView.Sort;
			//this.Text = this.TextBase;
		}



		#endregion

		#region Filtr

		/// <summary>
		/// Metoda pro filtr dle nazvu
		/// </summary>
		private void filtrNazev()
		{
			try
			{
				ScannerStop();

				using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prodej3ProdejListZadejteCastNazvuPolozky, SejmiKodForm.TypeOfCode.AlphaNumeric))
				{
					skf.Kod = _filtr_nazev;
					skf.AllowEmpty = true;

					if (skf.ShowDialog() == DialogResult.OK)
					{
						string filtr = skf.Kod;
						filtrRemove();
						if (filtr.Trim().Length > 0)
						{
							_katalogZboziView.RowFilter = "ITEMDESC LIKE '%" + skf.Kod + "%'";
							_filtr_nazev = filtr;
							menuItemFiltrNazev.Checked = true;
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				ScannerStart();
			}
		}

		#endregion

		#region Zobrazeni

		/// <summary>
		/// Metoda pro nastaveni zobrazeni Listu
		/// </summary>
		private void zobrazeniList()
		{
			Zobrazeni = ZobrazeniTyp.List;
		}

		/// <summary>
		/// Metoda pro nastaveni zobrazeni detailu
		/// </summary>
		private void zobrazeniDetail()
		{
			if (Zobrazeni == ZobrazeniTyp.List)
				Zobrazeni = ZobrazeniTyp.Detail;
			else if (Zobrazeni == ZobrazeniTyp.Nasnimane)
				Zobrazeni = ZobrazeniTyp.DetailNasnimane;

		}

		/// <summary>
		/// Metoda pro nastaveni zobrazeni detailu nasnimanich položek
		/// </summary>
		private void zobrazeniDetailNasnimane()
		{
			Zobrazeni = ZobrazeniTyp.DetailNasnimane;
		}

		/// <summary>
		/// Metoda pro zobrazeni nasnimanych polozek
		/// </summary>
		private void zobrazeniNasnimane()
		{
			this.Zobrazeni = ZobrazeniTyp.Nasnimane;
		}

		/// <summary>
		/// Metoda pro zobrazeni Stavu prodeje
		/// </summary>
		private void ShowStavProdeje()
		{
			decimal cenasdani = 0;
			decimal cenabezdane = 0;

			Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.stav_prodeje(out cenasdani, out cenabezdane);
			//Prodej.Globals. stav_prodeje(this._cislodavkysqlfilename, out cenasdani, out cenabezdane);

			//string str_stavprodeje = string.Format("Základ: {0}\n S DPH: {1}", cenabezdane.ToString(".00"), cenasdani.ToString(".00"));
			string str_stavprodeje = string.Format(Fask.Localization.Localization.Prodej3ProdejListStavProdejeInfo, cenabezdane.ToString(".00"), cenasdani.ToString(".00"));

			MessageBoxBig.Show(str_stavprodeje, Fask.Localization.Localization.Prodej3ProdejListStavProdeje, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
		}


		#endregion

		/// <summary>
		/// Metoda pro ukonèeni formu
		/// </summary>
		private void PerformKonec()
		{
			//TaD 24.4.2018 ANC
			// 4-D
			//if (MessageBoxBig.Show("Ukonèit zpracování dávky '" + this._cislodavky + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
			if (Prodej.Globals.ProdejDialogUkonceniZpracobaniDavky)
			{
				if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListUkoncitZpracovaniDavkyDotaz, this._cislodavky), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.No)
					return;
			}

			if (MST_Global.PovolitPrintServer && ((_typdokladu != null && !_typdokladu.Iscfg_tisk_soupisNull() && _typdokladu.cfg_tisk_soupis > 0) || Prodej.Globals.PovolitTiskSoupisu) && Prodej.Globals.TiskSoupisuPriUzavreniDavky)
			{
				//if (MessageBoxBig.Show("Vytisknout soupis všech naètených položek?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
				if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejListTiskSoupisDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
				== DialogResult.Yes)
					TiskSoupis();
			}


			//Ukonceni okna pro strediska ...
			if (pvsForm != null)
			{
				try
				{
					pvsForm.Dispose();
					pvsForm = null;
				}
				catch { }
			}

			//Ukonceni okna pro pracovniky ...
			if (pvpForm != null)
			{
				try
				{
					pvpForm.Dispose();
					pvpForm = null;
				}
				catch { }
			}

			if (pvpaletyForm != null)
			{
				try
				{
					pvpaletyForm.Dispose();
					pvpaletyForm = null;
				}
				catch { }
			}

			if (skf != null)
			{
				try
				{
					skf.Dispose();
					skf = null;
				}
				catch { }
			}

			if (naplnpMnozstvi != null)
			{
				try
				{
					naplnpMnozstvi.Dispose();
					naplnpMnozstvi = null;
				}
				catch { }
			}

			if (naplnpSerialNumber != null)
			{
				try
				{
					naplnpSerialNumber.Dispose();
					naplnpSerialNumber = null;
				}
				catch { }
			}

			finalize();


			DialogResult = DialogResult.OK;
		}


		#endregion

		#region Zmenit

		/// <summary>
		/// Metoda pro online generovani cisla palety
		/// </summary>
		private void PerformGenerovatCisloPalety()
		{
			try
			{
				ScannerStop();
				Cursor.Current = Cursors.WaitCursor;
                nmbrpal = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_expedice.SSCC_Generovat(MST_Global.TerminalID, MST_Global.UserID, _skladZdroj != null ? _skladZdroj.skl_id : string.Empty);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformGenerovatCisloPalety");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				this.UpdateForm();
				ScannerStart();
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		/// Metoda pro zmenu cisla palety
		/// </summary>
		private void PerformZmenitCisloPalety()
		{
			try
			{
				ScannerStop();
				// zadani cisla palety
				string kod = string.Empty;
				using (SejmiKodForm skf = new SejmiKodForm("Èíslo palety", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
				{
					skf.Text = "Zadejte èíslo palety";
					DialogResult dr = skf.ShowDialog();

					if (dr != DialogResult.OK)
						return;

					kod = skf.Kod;
				}

				Cursor.Current = Cursors.WaitCursor;
				//nmbrpal = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, sklad != null ? sklad.skl_id : string.Empty, _Hlavicka.ID, pal);
				throw new NotImplementedException("Implementovat ... ");
				Fask.MST_W.ExpediceService.SSCC sscc = null;
				//Fask.MST_W.ExpediceService.SSCC sscc = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, _skladZdroj != null ? _skladZdroj.skl_id : string.Empty, _Hlavicka.ID, kod);

				// TODO: vyhledani cisla palety v davce




				if (sscc == null)
				{
					Cursor.Current = Cursors.Default;
					MessageBoxBig.Show(string.Format("Zadané èíslo palety '{0}' nebylo nalezeno", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
				}
				else
					nmbrpal = sscc;
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, PerformZmenitCisloPalety");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				this.UpdateForm();
				ScannerStart();
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		/// Metoda pro zmeneni sarze pro veskere nasnimane polozky.
		/// </summary>
		/// <returns></returns>
		private bool SarzeSet()
		{
			if (!SarzeEnabled())
				return true;

			try
			{
				ScannerStop();

				string sn = string.Empty;
				// zadani noveho serltnum
				SejmiKodForm skf = new SejmiKodForm();
				skf.Popis = "Šarže";
				skf.Text = "Zadaní šarže";
				skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
				//skf.MaxLength = Globals.LOCNCODE_LEN;
				//skf.Len = Globals.LOCNCODE_LEN;
				//skf.CheckLen = true;
				skf.AllowEmpty = false;
				skf.Kod = string.IsNullOrEmpty(this.serltnum) ? string.Empty : this.serltnum.Trim();

				if (skf.ShowDialog() == DialogResult.Cancel)
					return false;

				sn = skf.Kod;

				this.serltnum = sn;
				return true;
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Prodej.ProdejList, SarzeSet");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return false;
			}
			finally
			{
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro zisteni zda je povolen parametr cfg_sn_na_davku povolen
		/// </summary>
		/// <returns></returns>
		private bool SarzeEnabled()
		{

			if (_typdokladu == null)
			{ //neni typ dokladu => nastaveni modulu na terminalu
				return false;
			}
			else if (_typdokladu != null && !_typdokladu.Iscfg_sn_na_davkuNull() && _typdokladu.cfg_sn_na_davku > 0)
			{ //je typ dokladu a ma povoleno jedno sn na davku
				return true;
			}
			else //no neni povoleno ... konci
			{
				return false;
			}
		}

		/// <summary>
		/// Metoda pro zmeni skladu
		/// </summary>
		private void PerformZmenaSklad()
		{
			try
			{
				ScannerStop();

				// povolena zmena skladu
				if (((_typdokladu != null && !_typdokladu.Iscfg_skladyNull() && _typdokladu.cfg_sklady > 0) || Prodej.Globals.PouzitSklady)
					&& (_typdokladu != null && !_typdokladu.Iscfg_sklady_zmenaNull() && _typdokladu.cfg_sklady_zmena > 0)
					)
				{
					using (Forms.FormSkladVyber fsv = new FormSkladVyber())
					{
						if (fsv.ShowDialog() == DialogResult.Cancel)
							return;

						if (_skladZdroj == null)
						{
							Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
							MessageBoxBig.Show("Není vybrán sklad, pøestože je vyžadován!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							return;
						}

						_skladZdroj = fsv.Sklad;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex, "PerformZmenaSklad");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				ScannerStart();
				UpdateForm();
				//UpdateForm(_zobrazeni);
			}
		}

		/// <summary>
		/// Metoda pro zmenu Pracovniku
		/// </summary>
		/// <returns></returns>
		private bool PracovniciSet()
		{
			//bool scannerWasEnabled = false;
			try
			{
				//if (Program.mstw.Scanner != null)
				//    scannerWasEnabled = Program.mstw.Scanner.Enabled;
				//if (scannerWasEnabled)
				//    ScannerStop();

				ScannerStop();

				//Stredisko se zadava k davce a musi byt nastaveno na null
				if (!Prodej.Globals.PracovniciKPolozce && _pracovnik != null)
				{
					//Pokud se zadava stredisko k polozce a stredisko neni null,
					//tak bylo vybrano a pouzije se toto
					//return false;
					return true;
				}

				if (Prodej.Globals.PracovniciText)
				{
					if (skf == null) skf = new SejmiKodForm();
					skf.AllowEmpty = false;
					skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.CheckLen = false;
					skf.Kod = string.Empty;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["PRAC_ID"].MaxLength;
					skf.Popis = Fask.Localization.Localization.Prodej3ProdejListPracovnik;  // "Pracovník";
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; // "Zadání doplòující hodnoty";
					if (skf.ShowDialog() == DialogResult.Cancel)
						return false;

					string pracovnikid = skf.Kod;

					PracovniciTextSet(pracovnikid);

				}
				else
				{
					if (pvpForm == null) pvpForm = new ProdejVyberPracovnika2();

					if (pvpForm.ShowDialog() == DialogResult.Cancel)
						return false;

					_pracovnik = pvpForm.Pracovnik;
				}

				PracovniciMenuStateUpdate();

				return true;

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return false;
			}
			finally
			{
				//if (scannerWasEnabled)
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro nastaveni prazdneho pracovnika
		/// </summary>
		/// <param name="pracovnikid"></param>
		private void PracovniciTextSet(string pracovnikid)
		{
			Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable pracovnividt = new Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable();
			_pracovnik = pracovnividt.AddCZMST096Row(pracovnikid, pracovnikid, "0", string.Empty, 0);
		}

		/// <summary>
		/// Metoda pro zmenu stredisek
		/// </summary>
		/// <returns></returns>
		private bool StrediskoSet()
		{
			//bool scannerWasEnabled = false;
			try
			{
				//if (Program.mstw.Scanner != null)
				//    scannerWasEnabled = Program.mstw.Scanner.Enabled;
				//if (scannerWasEnabled)
				//    ScannerStop();

				ScannerStop();

				//Stredisko se zadava k davce a musi byt nastaveno na null
				if (!Prodej.Globals.StrediskoKPolozce && _stredisko != null)
				{
					//Pokud se zadava stredisko k polozce a stredisko neni null,
					//tak bylo vybrano a pouzije se toto
					//return false;
					return true;
				}

				if (Prodej.Globals.StrediskoText)
				{
					if (skf == null) skf = new SejmiKodForm();
					skf.AllowEmpty = false;
					skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					skf.CheckLen = false;
					skf.Kod = string.Empty;
					skf.Len = 0;
					//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["STR_ID"].MaxLength;
					skf.Popis = Fask.Localization.Localization.Prodej3ProdejListStredisko;  //"Støedisko";
					skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplòující hodnoty";
					if (skf.ShowDialog() == DialogResult.Cancel)
						return false;

					string strediskoid = skf.Kod;

					StrediskoTextSet(strediskoid);

				}
				else
				{
					if (pvsForm == null) pvsForm = new ProdejVyberStrediska2();

					if (pvsForm.ShowDialog() == DialogResult.Cancel)
						return false;

					_stredisko = pvsForm.Stredisko;
				}

				StrediskoMenuStateUpdate();

				return true;

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return false;
			}
			finally
			{
				//if (scannerWasEnabled)
				ScannerStart();
			}
		}

		/// <summary>
		/// Metoda pro nastaveni prazdneho strediska
		/// </summary>
		/// <param name="strediskoid"></param>
		private void StrediskoTextSet(string strediskoid)
		{
			Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable strediskadt = new Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable();
			_stredisko = strediskadt.AddCZMST091Row(strediskoid, strediskoid, "0", string.Empty, 0);
		}


		#endregion

		/// <summary>
		/// Online ziskani ID zdrojoveho a ciloveho skladu.
		/// </summary>
		/// <param name="doc_id">typ dokladu.</param>
		/// <param name="itemnmbr">itemnmbr.</param>
		/// <param name="serltnum">serltnum.</param>
		/// <param name="skl_id">skl_id (out parameter)</param>
		/// <param name="skl_id_dest">skl_id_dest (out parameter)</param>
		/// <returns>STATUS (OK, ERROR)</returns>
		private Fask.MST_W.ProdejService.STATUS OnlineGetSklad(string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
		{
			Fask.MST_W.ProdejService.STATUS status = Fask.MST_W.ProdejService.STATUS.ERROR;
			skl_id = string.Empty;
			skl_id_dest = string.Empty;

			try
			{
				Cursor.Current = Cursors.WaitCursor;

				_WebRefernces_Globals.ProdejServiceSession prodejService = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
				prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
				prodejService.Timeout = MST_Global.ServiceTimeOut;
				prodejService.UpdateWebServiceCredentials();

				status = prodejService.GetSklad(MST_Global.TerminalID, MST_Global.UserID, doc_id, itemnmbr, serltnum, out skl_id, out skl_id_dest);

				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Prodej.ProdejList, OnlineGetSklad");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

				return Fask.MST_W.ProdejService.STATUS.ERROR;
			}

			return Fask.MST_W.ProdejService.STATUS.OK;
		}

		#endregion

		#region Private Metody

		#region Timer, pro kontrolu stavu polozky

		/// <summary>
		/// Delegat pro zisteni stavu polozky
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <param name="f1"></param>
		delegate void DelegateStringStringFloat(string s1, string s2, float f1);


		/// <summary>
		/// Metoda volaná Timerem, pro zistovani stavu položky
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer_Tick(object sender, EventArgs e)
		{
			//CHECKITEMSTATE
			try
			{
				_zbozi = this.SelectedZbozi;
				if (_zbozi != null)
					checkitemstate(_zbozi.ITEMNMBR, "");
			}
			catch { }
			//UpdateForm();
		}

		/// <summary>
		/// Online volani stavu položky BEGIN
		/// </summary>
		/// <param name="itemnmbr"></param>
		/// <param name="location"></param>
		private void checkitemstate(string itemnmbr, string location)
		{
			object parameters = (object)(new string[] { itemnmbr, location });
			Prodej_3.ProdejMain.prodejInstance.globalObject.servis_information.BeginMnozstviNaSklade(itemnmbr, new AsyncCallback(this.checkitemstateend), parameters);
		}

		/// <summary>
		/// Online volani stavu položky END
		/// </summary>
		/// <param name="ares"></param>
		private void checkitemstateend(IAsyncResult ares)
		{
			string[] parameters = (string[])ares.AsyncState;
			try
			{
				float number = ProdejMain.prodejInstance.globalObject.servis_information.EndMnozstviNaSklade(ares);
				this.BeginInvoke(new DelegateStringStringFloat(this.checkitemstateUI), new object[] { parameters[0], parameters[1], number });
			}
			catch
			{
				try
				{
					this.BeginInvoke(new DelegateStringStringFloat(this.checkitemstateUI), new object[] { parameters[0], parameters[1], float.NaN });
				}
				catch (Exception ex)
				{
					Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, checkitemstateend");
				}
			}
		}

		/// <summary>
		/// Metoda pro zmenu UI statusu
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="location">Lokace</param>
		/// <param name="mnozstvi">Mnozstvi</param>
		private void checkitemstateUI(string itemnmbr, string location, float mnozstvi)
		{
			//CHECKITEMSTATE
			DateTime dtnow = DateTime.Now;
			try
			{
				//float mnozstvi = ProdejMain.prodejInstance.prodejs.MnozstviNaSklade(itemnmbr);
				if (float.IsNaN(mnozstvi))
					throw new Exception();

				Schema.Sklad.SkladMnozstviRow skladrow = nasklade.SkladMnozstvi.FindByITEMNMBRLocation(itemnmbr, location);
				if (skladrow == null)
				{
					nasklade.SkladMnozstvi.AddSkladMnozstviRow(itemnmbr, mnozstvi, dtnow, dtnow, location);
				}
				else
				{
					skladrow.QTY = mnozstvi;
					skladrow.LastDownloadSuccess = dtnow;
					skladrow.LastDownloadTry = dtnow;
				}
			}
			catch
			{
				Schema.Sklad.SkladMnozstviRow skladrow = nasklade.SkladMnozstvi.FindByITEMNMBRLocation(itemnmbr, location);
				if (skladrow == null)
				{
					nasklade.SkladMnozstvi.AddSkladMnozstviRow(itemnmbr, 0, DateTime.MinValue, dtnow, location);
				}
				else
				{
					skladrow.LastDownloadTry = dtnow;
				}
			}

			try
			{
				while (nasklade.SkladMnozstvi.Count > 100)
					nasklade.SkladMnozstvi.Rows.RemoveAt(0);
				nasklade.AcceptChanges();
			}
			catch
			{
			}
			UpdateForm();
		}


		#endregion

		#region Scanner

		private bool scannserstart = true;

		/// <summary>
		/// Metoda pro Finalize scanneru
		/// </summary>
		private void ScannerFinalize()
		{
			this.ScannerStop();
			this.scannserstart = false;
		}

		/// <summary>
		/// Metoda pro Start Scanneru
		/// </summary>
		private void ScannerStart()
		{
			if (!scannserstart)
				return;

			Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.EnableScanner();
		}

		/// <summary>
		/// Metoda pro STOP Scanneru
		/// </summary>
		private void ScannerStop()
		{
			Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.DisableScanner();
		}

		delegate void StringDelegate(string carkod);

		/// <summary>
		/// Metoda volanná z metody scanneru
		/// </summary>
		/// <param name="carkod"></param>
		private void UpdateUI(string carkod)
		{
			try
			{
				ScannerStop();

				filtrRemove();

				string ck = carkod.Trim();

				// parsovani vahoveho kodu
				Fask.Parsing.Codes.BaseCode code = null;
				if (_typdokladu != null && !_typdokladu.Iscfg_parsovat_ckNull() && _typdokladu.cfg_parsovat_ck > 0)
				{

					//code = Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);
					//if (code is Parsing.Codes.Interfaces.ICodeBarcode)
					//    ck = (code as Parsing.Codes.Interfaces.ICodeBarcode).Barcode;

					//multipars
					#region

					code = Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);
					//if (code is Parsing.Codes.GS1) // TODO : ? and GS1.Multiscan.Enabled ? 
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

					if (code is Parsing.Codes.Interfaces.ICodeBarcode)
						ck = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode ?? string.Empty;

					#endregion


				}

				if (ck.Length > 0)
				{
					HledaniStatus found = NajdiPolozkuPodleCK(ck, code);
					if (found == HledaniStatus.Nenalezeno)
					{
						if (Prodej.Globals.PovolitNovouPolozku)
						{
							pridatPolozkuNeexistujici(ck);
						}
						else
						{
							// je povoleno vyhledavani podle sarze a 
							if (Prodej.Globals.PolozkyVyhledatPomociSarze && ((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety))
							{
								string sklad_id = _skladZdroj != null ? _skladZdroj.skl_id : string.Empty;

								#region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)
								// nacteni skladu online ...
								if (Prodej.Globals.NacistSkladIDOnline)
								{

									string sklad_id_dest = string.Empty;

									MST_W.ProdejService.STATUS status = OnlineGetSklad(_typdokladu != null ? _typdokladu.doc_id : string.Empty, string.Empty, ck, out sklad_id, out sklad_id_dest);
									if (status == Fask.MST_W.ProdejService.STATUS.ERROR) // chyba, ukoncit ...
										return;
									else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
									{
										MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezenaVeSkladuOnline, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										return;
									}
								}
								#endregion

								Fask.MST_W.ProdejService.Location ds = OnlineGetMaterial(string.Empty, _skladZdroj != null ? _skladZdroj.skl_id : string.Empty, ck);
								if (ds != null)
								{
									// vratily se nejake zaznamy
									if (ds.CZMST_SkladLokace_Stav.Count > 0)
									{
										// vytvorit novy prvek pro vlozeni pridatpolozku(zbozi)
										Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zTmp = _katalogZbozi.CZMST095.NewCZMST095Row();
										zTmp.ITEMNMBR = ds.CZMST_SkladLokace_Stav[0].ITEMNMBR;
										zTmp.LOCNCODE = ds.CZMST_SkladLokace_Stav[0].IsLOCNCODENull() ? string.Empty : ds.CZMST_SkladLokace_Stav[0].LOCNCODE; // !! nastavit
										zTmp.SERLTNUM = ds.CZMST_SkladLokace_Stav[0].SERLTNUM; // !! nastavit
										zTmp.QTY = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;   // !! nastavit
										zTmp.SKL_ID = ds.CZMST_SkladLokace_Stav[0].IsSKL_IDNull() ? string.Empty : ds.CZMST_SkladLokace_Stav[0].SKL_ID;   // !! nastavit
										zTmp.QTYPACK = 0;
										zTmp.CZ_SerNum_Track = 2; //na sarze ...
										zTmp.CZ_SerNum_Delka = 0;
										zTmp.CZ_Rez1_Track = 0;
										zTmp.CZ_Rez2_Track = 0;
										zTmp.CZ_Rez3_Track = 0;
										zTmp.CZ_Rez4_Track = 0;
										zTmp.VNDITNUM = string.Empty;
										zTmp.CZ_CarKod = string.Empty;
										zTmp.MJ = string.Empty;
										// ... ???
										pridatPolozku(zTmp);
										return;
									}
									else // nenalezeny dop. palety 
										MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezenaOnline, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								} // else  // chyba v online funkci, hlaska se zobrazuje primo
							}
							else if (Prodej.Globals.PolozkyVyhledatPomociSarze)  // polozka nenalezena a je zapnute hledani pomoci sarze
							{
								//MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezena, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezena, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
							else // polozka nenalezena a hleda se pouze pomoci ck 
							{
								//MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNenalezena, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaSCarKodNenalezena, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
					}
					else if (found == HledaniStatus.NalezenJedenZaznam)
					{
						pridatPolozku(this.SelectedZbozi, code);
					}
					else if (found == HledaniStatus.NalezenoViceZaznamu)
					{
						MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuVyberRucne, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						FindByDefaultMJ();
						zobrazeniList();
					}
					else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
					{
						MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani, Prodej.Globals.GridViewRowCount), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					}

					//if (_zbozi != null)
					//{
					//    pridatPolozku(_zbozi);
					//}
					//else if (_katalogZbozi.CZMST095.Count > 1)
					//{
					//    FindByDefaultMJ();
					//}

				}

			}
			finally
			{
				ScannerStart();
				if (MST_Global.OnScannerSound_Prodej_3)
				{
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
				}
			}
		}

		/// <summary>
		/// Metoda nalinkovana do eventu scanneru
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
		{
			_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
			this.BeginInvoke(new StringDelegate(UpdateUI), new object[] { e.BarcodeData });

			//string ck = e.BarcodeData.Trim();
			//if (ck.Length > 0)
			//{
			//    if (NajdiPolozkuPodleCK(ck))
			//    {
			//        pridatPolozku();
			//    }
			//    else
			//    {
			//        MessageBoxBig.Show("Položka s è.k.'" + ck + "' nenalezena.");
			//    }
			//}
		}

		#endregion

		#region Pomocne metody

        [Obsolete("Na prodeji se musi pouzivat Prodejni webservice => PrijemService.Online_GetDoporuceneLokace predelat do ProdejService...", false)]
		/// <summary>
		/// Metoda pro online zisteni cilove lokace
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="serltnum">SN/ sarze</param>
		/// <param name="skl_id">ID skladu</param>
		/// <returns>naplnen dataset obecne</returns>
		private Fask.MST_W.PrijemService.Obecne OnlineGetDoporuceneCiloveLokace(string itemnmbr, string serltnum, string skl_id)
		{
			// TODO: do budoucna resit pres prodej.asmx, posilat typ dokladu, ...
			Fask.MST_W.PrijemService.Obecne ds = new Fask.MST_W.PrijemService.Obecne();

			try
			{
				Cursor.Current = Cursors.WaitCursor;
                ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_GetDoporuceneLokace(itemnmbr, skl_id, serltnum);
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Prodej.ProdejList, OnlineGetDoporuceneLokace");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

				return null;
			}
			return ds;
		}

		/// <summary>
		/// Metoda pro online overeni lokace
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="serltnum">SN/ sarže</param>
		/// <param name="locncode">Lokace</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="locationType">'S' - overovani zdrojove lokace, 'D' - overovani cilove lokace</param>
		/// <returns>naplnen StatusOverLokace</returns>
		private Fask.MST_W.ProdejService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum,DateTime? expirace, string locncode, string skl_id, decimal qtyshppd, Fask.MST_W.ProdejService.TYPLokace locationType, Fask.MST_W.ProdejService.TypeOfRecord recordType)
		{
			Fask.MST_W.ProdejService.StatusOverLokace so;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				_WebRefernces_Globals.ProdejServiceSession prodejService = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
				prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
				prodejService.Timeout = MST_Global.ServiceTimeOut;
				prodejService.UpdateWebServiceCredentials();

				so = prodejService.Online_OverLokace(itemnmbr, serltnum,expirace, locncode, skl_id, qtyshppd, _typdokladu != null ? _typdokladu.doc_id : string.Empty, locationType, recordType);
				Cursor.Current = Cursors.Default;
				//if (so.State != 0)  // nastala chyba
				//{
				//    throw new Exception(so.Error);
				//}
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Prodej.ProdejList, OnlineOverLokace");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return null;
			}
			return so;
		}

		/// <summary>
		/// Metoda pro online overeni GetMaterial (??lok mech??)
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="serltnum">SN / sarze</param>
		/// <returns>Dataset Location</returns>
		private Fask.MST_W.ProdejService.Location OnlineGetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			Fask.MST_W.ProdejService.Location ds = new Fask.MST_W.ProdejService.Location();

			try
			{
				Cursor.Current = Cursors.WaitCursor;
				_WebRefernces_Globals.ProdejServiceSession prodejService = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
				prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
				prodejService.Timeout = MST_Global.ServiceTimeOut;
				prodejService.UpdateWebServiceCredentials();

				ds = prodejService.Online_GetMaterial(itemnmbr, skl_id, serltnum, _typdokladu != null ? _typdokladu.doc_id : string.Empty, false);
				Cursor.Current = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex.Message, "Prodej.ProdejList, OnlineGetPalety");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

				return null;
			}
			return ds;
		}

		/// <summary>
		/// Metoda pro online ovìøení typu pohybu, zdali je možné pokraèovat v ukládání dat.
		/// </summary>
		/// <param name="sn">sériové èíslo</param>
		/// <param name="itemnmbr">FASK_ZASOBY.Row.ITEMNMBR</param>
		/// <param name="sklad_id">id skladu</param>
		/// <param name="qtyshppd">mnozstvi</param>
		/// <param name="qtyshppdNacteno">Doposud nactene mnozstvi</param>
		/// <returns></returns>
		private bool OverPohyb(string sn, string itemnmbr, string sklad_id, decimal qtyshppd, decimal qtyshppdNacteno, out decimal outqtyshppd)
		{
			try
			{
				qtyshppd = qtyshppd + qtyshppdNacteno;
				_WebRefernces_Globals.ProdejServiceSession prodejService = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
				prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
				prodejService.Timeout = MST_Global.ServiceTimeOut;
				prodejService.UpdateWebServiceCredentials();

				ProdejService.ProdejPohyb prodejPohyb = new ProdejService.ProdejPohyb();
				prodejPohyb.Doc_id = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
				prodejPohyb.Doc_id2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
				prodejPohyb.Itemnmbr = itemnmbr;
				prodejPohyb.Serltnum = sn;
				prodejPohyb.skl_id = sklad_id;
				prodejPohyb.qtyshppd = qtyshppd;

				//ProdejService.StatusOverPohyb statusOP = Prodej_3.ProdejMain.prodejInstance.prodejs.OverPohyb(prodejPohyb, MST_Global.TerminalID, MST_Global.UserID);
				ProdejService.StatusOverPohyb statusOP = prodejService.OverPohyb(prodejPohyb, MST_Global.TerminalID, MST_Global.UserID);

				if (!statusOP.PohybOK)
					throw new Exception(statusOP.Message);
				outqtyshppd = statusOP.a_dispozice;
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, OverPohyb");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				outqtyshppd = -1;
				return false;
			}
			return true;
		}

		/// <summary>
		/// Metoda pro odstraneni filtru
		/// </summary>
		private void filtrRemove()
		{
			_katalogZboziView.RowFilter = string.Empty;
			_filtr_nazev = string.Empty;
			menuItemFiltrNazev.Checked = false;
		}

		/// <summary>
		/// Metoda pro zmenu povoleni pracodniku na prodeji
		/// </summary>
		private void PracovniciMenuStateUpdate()
		{
			//Je povoleno vyber strediska?
			this.menuItemPracovnici.Enabled =
				((_typdokladu != null && _typdokladu.cfg_prac > 0) || Prodej.Globals.Pracovnici) && !Prodej.Globals.PracovniciJedenNaDavku;
		}

		/// <summary>
		/// Metoda pro zmenu povoleni stredisek na prodeji
		/// </summary>
		private void StrediskoMenuStateUpdate()
		{
			//Je povoleno vyber strediska?
			this.menuItemStredisko.Enabled =
				((_typdokladu != null && _typdokladu.cfg_str > 0) || Prodej.Globals.Strediska) && !Prodej.Globals.StrediskoJednoNaDavku;
		}

		/// <summary>
		/// Metoda pro vypoèet naèteno
		/// </summary>
		/// <param name="ITEMNMBR">ID polozky</param>
		/// <returns>decimal poèet naèteno</returns>
		public decimal Nacteno(string ITEMNMBR)
		{
			decimal nacteno = 0;
			foreach (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di in _prodejTable.CZMST_DI)
			{
				if (di.ITEMNMBR == ITEMNMBR)
				{
					nacteno += di.QTYSHPPD;
				}
			}
			return nacteno;
		}

		/// <summary>
		/// Metoda volana pred ukonèenim okna
		/// </summary>
		private void finalize()
		{
            if (timer != null)
                timer.Enabled = false;

			if (skf != null)
			{
				skf.Dispose();
				skf = null;
			}

			if (naplnpMnozstvi != null)
			{
				naplnpMnozstvi.Dispose();
				naplnpMnozstvi = null;
			}

			if (naplnpSerialNumber != null)
			{
				naplnpSerialNumber.Dispose();
				naplnpSerialNumber = null;
			}

			if (naplnpLocnCode != null)
			{
				naplnpLocnCode.Dispose();
				naplnpLocnCode = null;
			}

			if (pvsForm != null)
			{
				pvsForm.Dispose();
				pvsForm = null;
			}

			if (pvpForm != null)
			{
				pvpForm.Dispose();
				pvpForm = null;
			}

			if (pvpaletyForm != null)
			{
				pvpaletyForm.Dispose();
				pvpaletyForm = null;
			}

			this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_1"));
			this.dataGridNasnimane.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_2"));

			if (nasklade != null)
			{
				//nasklade.AcceptChanges();
				//nasklade.WriteXml(Main.CiselnikSkladuFileName);
				nasklade.Dispose();
				nasklade = null;
			}

			//Settings.ProdejCarovyKodFiltr = miNajitCKFiltr.Checked;
			Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
			Settings.ProdejListLastSort = _db_sort;

			ScannerFinalize();
		}

		/// <summary>
		/// Metoda pro zadavani lokace.
		/// </summary>
		/// <param name="zbozi">radek zbozi</param>
		/// <param name="sourceLoc">True - zdrojova lokace, False - cilova lokace</param>
		/// <returns>Nalezena lokace</returns>
		private string SejmiLocncode(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool sourceLoc)
		{
			return SejmiLocncode(zbozi, sourceLoc, null);
		}

		/// <summary>
		/// Metoda pro zadavani lokace.
		/// </summary>
		/// <param name="zbozi">radek zbozi</param>
		/// <param name="sourceLoc">True - zdrojova lokace, False - cilova lokace</param>
		/// <param name="prefilledVal">Predvyplnena hodnota v textboxu</param>
		/// <returns>Nalezena lokace</returns>
		private string SejmiLocncode(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool sourceLoc, string prefilledVal)
		{
			#region 23.4.2018 TaD nova uprava ANC

			while (true)
			{
				if (_typdokladu == null)
				{
					if (naplnpLocnCode == null) naplnpLocnCode = new ProdejPridatPolozku(sourceLoc ? Fask.Localization.Localization.Prodej3ProdejListLokace : Fask.Localization.Localization.Prodej3ProdejListCilovaLokace, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", null, null);
					naplnpLocnCode.Odberatel = _odberatel;
					naplnpLocnCode.Zbozi = zbozi;
					naplnpLocnCode.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					naplnpLocnCode.Text = Fask.Localization.Localization.Prodej3ProdejListVlozteSklad;  // "Vložte sklad";
					//naplnpLocnCode.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["LOCNCODE"].MaxLength;
					naplnpLocnCode.Kod = zbozi.LOCNCODE;
					naplnpLocnCode.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;
					if (naplnpLocnCode.ShowDialog() == DialogResult.Cancel)
						return "!@";

					//return naplnpLocnCode.Kod;
				}
				else
				{
					// 8.4.2016 PeV: doc_typ 1 zakomentovan
					//if (_typdokladu.doc_typ.Trim() == "1")
					//{
					//    if (_odberatel == null)
					//        return string.Empty;
					//    else
					//        return _odberatel.odb_typ;
					//}
					//else
					//{
					//  locncode = zbozi.LOCNCODE;
					if (naplnpLocnCode == null) naplnpLocnCode = new ProdejPridatPolozku(sourceLoc ? Fask.Localization.Localization.Prodej3ProdejListLokace : Fask.Localization.Localization.Prodej3ProdejListCilovaLokace, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", null, null);
					naplnpLocnCode.Odberatel = _odberatel;
					naplnpLocnCode.Zbozi = zbozi;
					naplnpLocnCode.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					//naplnpLocnCode.Text = "Vložte sklad";
					naplnpLocnCode.Text = sourceLoc ? Fask.Localization.Localization.Prodej3ProdejListVlozteLokaci : Fask.Localization.Localization.Prodej3ProdejListVlozteCilovouLokaci;
					//naplnpLocnCode.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["LOCNCODE"].MaxLength;
					//naplnpLocnCode.Kod = zbozi.LOCNCODE;
					// zadani ciclove lokace
					naplnpLocnCode.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;
					if (!sourceLoc && _typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
						naplnpLocnCode.Kod = string.Empty;
					else
						naplnpLocnCode.Kod = string.IsNullOrEmpty(prefilledVal) ? zbozi.LOCNCODE : prefilledVal;

					if (naplnpLocnCode.ShowDialog() == DialogResult.Cancel)
						return "!@";


					//}
				}

				//bool config_Show_locncode = true;
				string message = string.Format("Nasnímána Lokace: {0} \n Pokraèovat?", naplnpLocnCode.Kod);

				///konfiguracne zobrazovat nasnimanou lokaci
				if (MST_W.Prodej.Globals.ProdejDialogNasnimanaLokace)
				{
					if (MessageBoxBig.Show(message, "Nasnimana Lokace", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						return naplnpLocnCode.Kod;
					}
				}
				else
				{
					return naplnpLocnCode.Kod;
				}

			}

			#endregion
		}

		/// <summary>
		/// Zmena nazvu hlavièky formu
		/// </summary>
		private void SetSortText()
		{
			if (_db_sort == cSortItemdescASC)
				this.Text = this.TextBase + " Název(A-Z)";
			else if (_db_sort == cSortItemdescDESC)
				this.Text = this.TextBase + " Název(Z-A)";
			else if (_db_sort == cSortItemnmbrASC)
				this.Text = this.TextBase + " È.pol.(A-Z)";
			else if (_db_sort == cSortItemnmbrDESC)
				this.Text = this.TextBase + " È.pol.(Z-A)";

		}

		/// <summary>
		/// Metoda pro update UI
		/// nazvy, poèety...
		/// </summary>
		private void UpdateForm()
		{
			try
			{
				//Update list


#if TEST
                MessageBox.Show("1:Nacteni" );
#endif


				_zbozi = this.SelectedZbozi;

				//detail polozek maly
				labelItemnmbrL.Text = "-";
				labelVNDITNUML.Text = "-";
				labelPRICEL.Text = "-";

				//detail polozky velky
				labelNazev.Text = "-";

				df_Itemnmbr.Data = "-";
				df_Sklad.Data = "-";
				df_VNDITNUM.Data = "-";
				df_CZCarKod.Data = "-";
				df_QTY.Data = "-";
				df_QTYPACK.Data = "-";
				df_TAXRATE.Data = "-";
				df_PRICE.Data = "-";
				df_SNTrack.Data = "-";
				//df_nasklade_l // je nize v kodu
				//df_datumnasklade_l // je nize v kodu
				df_WEIGHT.Data = "-";
				df_MJ.Data = "-";
				df_REZ1.Data = "-";
				df_REZ2.Data = "-";
				df_REZ3.Data = "-";
				df_REZ4.Data = "-";

				try
				{
					if (_zbozi != null)
					{

						labelNazev.Text = _zbozi.IsITEMDESCNull() ? "-" : _zbozi.ITEMDESC;
						df_VNDITNUM.Data = _zbozi.IsVNDITNUMNull() ? "-" : _zbozi.VNDITNUM;
						labelVNDITNUML.Text = _zbozi.IsVNDITNUMNull() ? "-" : _zbozi.VNDITNUM;
						df_CZCarKod.Data = _zbozi.IsCZ_CarKodNull() ? "-" : _zbozi.CZ_CarKod;
						df_Itemnmbr.Data = (_zbozi.IsITEMCODENull() ? "-" : _zbozi.ITEMCODE) + " (" + _zbozi.ITEMNMBR.Trim() + ")";
						labelItemnmbrL.Text = (_zbozi.IsITEMCODENull() ? "-" : _zbozi.ITEMCODE) + " (" + _zbozi.ITEMNMBR.Trim() + ")";
						try
						{
							df_PRICE.Data =
								(_odberatel == null) ?
								//(0M).ToString(Settings.UIFormatDesCisel) : 
								((decimal)_zbozi["PRICE0"]).ToString(Settings.UIFormatDesCisel) :
								((decimal)_zbozi["PRICE" + (_odberatel.Isodb_typNull() ? "0" : _odberatel.odb_typ)]).ToString(Settings.UIFormatDesCisel);
						}
						catch { df_PRICE.Data = "-"; }
						labelPRICEL.Text = df_PRICE.Data;

						df_QTY.Data = _zbozi.IsQTYPACKNull() ? "-" : _zbozi.QTY.ToString(Settings.UIFormatDesCisel);
						df_QTYPACK.Data = _zbozi.IsQTYPACKNull() ? "-" : _zbozi.QTYPACK.ToString(Settings.UIFormatDesCisel);
						df_SNTrack.Data = _zbozi.CZ_SerNum_Track == 0 ? "Ne" : "Ano";
						df_TAXRATE.Data = _zbozi.IsTAXRATENull() ? "-" : _zbozi.TAXRATE.ToString(Settings.UIFormatDesCisel) + " %";
						df_MJ.Data = _zbozi.MJ.Trim();
						df_Sklad.Data = _zbozi.IsSKL_DESCNull() ? "-" : ((string.IsNullOrEmpty(_zbozi.SKL_DESC) ? "-" : _zbozi.SKL_DESC.Trim()) + "(" + (string.IsNullOrEmpty(_zbozi.SKL_ID) ? "-" : _zbozi.SKL_ID.Trim()) + ")");
						df_WEIGHT.Data = _zbozi.IsWEIGHTNull() ? "-" : _zbozi.WEIGHT.ToString(Settings.UIFormatDesCisel);

						df_REZ1.Data = _zbozi.IsREZ1Null() ? "-" : (string.IsNullOrEmpty(_zbozi.REZ1) ? "-" : _zbozi.REZ1.Trim());
						df_REZ2.Data = _zbozi.IsREZ2Null() ? "-" : (string.IsNullOrEmpty(_zbozi.REZ2) ? "-" : _zbozi.REZ2.Trim());
						df_REZ3.Data = _zbozi.IsREZ3Null() ? "-" : (string.IsNullOrEmpty(_zbozi.REZ3) ? "-" : _zbozi.REZ3.Trim());
						df_REZ4.Data = _zbozi.IsREZ4Null() ? "-" : (string.IsNullOrEmpty(_zbozi.REZ4) ? "-" : _zbozi.REZ4.Trim());
					}

#if TEST
                    MessageBox.Show("3.3:zbozi");
#endif

				}
				catch (Exception ex)
				{
					MessageBox.Show("1" + ex.Message);
				}

#if TEST
                MessageBox.Show("4:Nacnete detail");
#endif

				//Update nasnimane
				labelItemnmbrN.Text = "-";
				labelQTYN.Text = "-";
				labelQTYPACKN.Text = "-";
				labelSNN.Text = "-";
				labelNactenoN.Text = "-";
				labelZakladN.Text = "-";
				labelSDPHN.Text = "-";

#if TEST
                MessageBox.Show("5:Nacnete detail");
#endif

				Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow _di = this.SelectedNasnimane;
				try
				{
#if TEST
                    MessageBox.Show("6.1:Nacnete detail");
#endif
					if (_di != null)
					{
#if TEST
                        MessageBox.Show("6.2:Nacnete detail");
#endif

						//labelItemnmbrN.Text = _zbozi.IsITEMCODENull() ? "-" : _zbozi.ITEMCODE + " (" + _zbozi.ITEMNMBR.Trim() + ")" ;
						labelItemnmbrN.Text = (_di.IsITEMCODENull() ? "-" : _di.ITEMCODE.Trim()) + " (" + _di.ITEMNMBR.Trim() + ")";
						//MessageBox.Show(labelItemnmbrN.Text + "\n Location:" + labelItemnmbrN.Location.X + "," + labelItemnmbrN.Location.Y + " : Size:" + labelItemnmbrN.Size.Height + "," + labelItemnmbrN.Width);
						//DialogResult drrrrr = MessageBoxBig.Show(labelItemnmbrN.Text + "\n Location:" + labelItemnmbrN.Location.X + "," + labelItemnmbrN.Location.Y + " : Size:" + labelItemnmbrN.Size.Height + "," + labelItemnmbrN.Width, "dafsdf", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
						//if (drrrrr == DialogResult.Yes)
						//{
						//    labelItemnmbrN.Width = 150;
						//}
						labelQTYN.Text = _di.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
						labelQTYPACKN.Text = _di.QTYPACK.ToString(Settings.UIFormatDesCisel);
						labelSNN.Text = _di.SERLTNUM;
						labelNactenoN.Text = Nacteno(_di.ITEMNMBR).ToString(Settings.UIFormatDesCisel);

						decimal cenasdani = 0;
						decimal cenabezdane = 0;
						//Prodej.Globals.stav_prodeje(_cislodavkysqlfilename, out cenasdani, out cenabezdane);
						Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.stav_prodeje(out cenasdani, out cenabezdane);

						labelZakladN.Text = cenabezdane.ToString(Settings.UIFormatDesCisel);
						labelSDPHN.Text = cenasdani.ToString(Settings.UIFormatDesCisel);
					}
#if TEST
                    MessageBox.Show("6.3:Nacnete detail");
#endif

				}
				catch (Exception ex)
				{
					MessageBox.Show("2" + ex.Message);
				}

#if TEST
                MessageBox.Show("7:Nacnete detail nasnimane");
#endif

				//update Nasnimane Detail

				labelNazev_DN.Text = "-";
				df_Itemnmbr_DN.Data = "-";
				df_Nacteno_DN.Data = "-";
				df_QTY_DN.Data = "-";
				df_QTYPACK_DN.Data = "-";
				df_SN_DN.Data = "-";

				df_Cena_sDPH_DN.Data = "-";
				df_Cena_MJ_DN.Data = "-";

				df_REZ1_DN.Data = "-";
				df_REZ2_DN.Data = "-";
				df_REZ3_DN.Data = "-";
				df_REZ4_DN.Data = "-";



				try
				{
					if (_di != null)
					{
						labelNazev_DN.Text = _di.ITEMDESC;

						df_Itemnmbr_DN.Data = (_di.IsITEMCODENull() ? "-" : _di.ITEMCODE.Trim()) + " (" + _di.ITEMNMBR.Trim() + ")";
						df_QTY_DN.Data = _di.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
						df_QTYPACK_DN.Data = _di.QTYPACK.ToString(Settings.UIFormatDesCisel);
						df_SN_DN.Data = string.IsNullOrEmpty(_di.SERLTNUM) ? "-" : _di.SERLTNUM;
						df_Nacteno_DN.Data = Nacteno(_di.ITEMNMBR).ToString(Settings.UIFormatDesCisel);



						decimal cenasdani = 0;
						decimal cenabezdane = 0;

						if (_di.WITHTAX > 0)
						{

							cenabezdane = _di.AMOUNPIE;// *_di.QTYSHPPD;
							cenasdani = _di.AMOUNPIE * _di.QTYSHPPD;
						}
						else
						{
							cenabezdane = _di.AMOUNPIE + _di.TAXAMPIE; //*_di.QTYSHPPD;
							cenasdani = (_di.AMOUNPIE + _di.TAXAMPIE) * _di.QTYSHPPD;
						}

						//df_Zaklad_DN.Data= cenabezdane.ToString(Settings.UIFormatDesCisel);
						//df_SDPH_DN.Data = cenasdani.ToString(Settings.UIFormatDesCisel);



						df_Cena_sDPH_DN.Data = cenasdani.ToString(Settings.UIFormatDesCisel);
						df_Cena_MJ_DN.Data = cenabezdane.ToString(Settings.UIFormatDesCisel);


						df_REZ1_DN.Data = _di.IsREZ_1Null() ? "-" : (string.IsNullOrEmpty(_di.REZ_1) ? "-" : _di.REZ_1.Trim());
						df_REZ2_DN.Data = _di.IsREZ_2Null() ? "-" : (string.IsNullOrEmpty(_di.REZ_2) ? "-" : _di.REZ_2.Trim());
						df_REZ3_DN.Data = _di.IsREZ_3Null() ? "-" : (string.IsNullOrEmpty(_di.REZ_3) ? "-" : _di.REZ_3.Trim());
						df_REZ4_DN.Data = _di.IsREZ_4Null() ? "-" : (string.IsNullOrEmpty(_di.REZ_4) ? "-" : _di.REZ_4.Trim());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("3" + ex.Message);
				}


				try
				{
					//this.sbInfo.Text =
					//    "D:" + this._cislodavky.ToString() + ", " +
					//    "Z:" + (this._db_record_actual + dataGrid1.CurrentRowIndex + 1).ToString() + "(" + this._db_records_count.ToString() + "), " +
					//    "N:" + this._prodejTable.CZMST_DI.Count.ToString() + "(" + this._prodejTable.CZMST_DI.Sum(x => x.QTYSHPPD).ToString("0.#####") + "), " +
					//    (this.nmbrpal == null ? string.Empty : ("Pal:" + this.nmbrpal.Number + ", ")) +
					//    (this._skladZdroj == null ? string.Empty : "S:" + this._skladZdroj.skl_desc.Trim() + "(" + this._skladZdroj.skl_id.Trim() + "), ") +
					//    (this._odberatel == null ? string.Empty : "O:" + this._odberatel.odb_desc.Trim() + ", ") +
					//    (this._pracovnik == null ? string.Empty : "P:" + this._pracovnik.prac_desc.Trim() + "(" + this._pracovnik.prac_id.Trim() + ")")
					//    ;

					this.sbInfo.Text =
						"D:" + this._cislodavky.ToString() + ", " +
						"Z:" + (this._db_record_actual + dataGrid1.CurrentRowIndex + 1).ToString() + "(" + this._db_records_count.ToString() + "), " +
						"N:" + this._prodejTable.CZMST_DI.Count.ToString() + "(" + this._prodejTable.CZMST_DI.Sum(x => x.QTYSHPPD).ToString("0.#####") + "), " +
						(this.nmbrpal == null ? string.Empty : ("Pal:" + this.nmbrpal.Number + ", ")) +
						"T:" + (Prodej.Globals.TiskEtiketyPoPridaniZbozi ? "(1)," : "(0),") +
						(this._odberatel == null ? string.Empty : "O:" + this._odberatel.odb_desc.Trim() + ", ") +
						(this._pracovnik == null ? string.Empty : "P:" + this._pracovnik.prac_desc.Trim() + "(" + this._pracovnik.prac_id.Trim() + ")")
						;

				}
				catch (Exception ex)
				{
					this.sbInfo.Text = ex.Message;
				}


				df_nasklade_l.Data = "-";
				df_nasklade_l.ForeColor = Color.Black;
				df_datumnasklade_l.Data = "-";

				//CHECKITEMSTATE
				try
				{
					if (_zbozi != null)
					{
						Schema.Sklad.SkladMnozstviRow skladmnrow = nasklade.SkladMnozstvi.FindByITEMNMBRLocation(_zbozi.ITEMNMBR, "");
						if (skladmnrow != null)
						{

							df_nasklade_l.Data = skladmnrow.IsQTYNull() ? "-" : skladmnrow.QTY.ToString(Settings.UIFormatDesCisel);
							df_datumnasklade_l.Data = skladmnrow.LastDownloadSuccess.ToString("g");
							if (skladmnrow.LastDownloadSuccess < skladmnrow.LastDownloadTry)
							{
								df_nasklade_l.ForeColor = Color.Red;
							}
							else
							{
								df_nasklade_l.ForeColor = Color.Blue;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("4" + ex.Message);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("end" + ex.Message);
			}
		}

		/// <summary>
		/// Metoda pro dohledani Defaultnej/Doporuèenej... Mìrnej jednotky
		/// </summary>
		private void FindByDefaultMJ()
		{
			try
			{
				int indexfound = 0;
				if (Prodej.Globals.FindDefaultMJ)
				{
					string doklad = this._typdokladu == null ? string.Empty : this._typdokladu.doc_id.Trim();
					string mjdef = string.Empty;

					//// TODO : optimalizovat => udelat pomoci Binding source a metody find??? nebo jinak ... ale efektivneji !!!
					//for (int i = 0; i < this._katalogZbozi.CZMST095.Count; i++)
					//{
					//    dataGrid1.CurrentRowIndex = i;
					//    if (this.SelectedZbozi.DMJ.Contains(doklad))
					//        return;
					//}

					DataTable dtdmj = _katalogZboziView.ToTable(false, new string[] { this._katalogZbozi.CZMST095.DMJColumn.ColumnName });
					DataRow[] drmj = dtdmj.Select(this._katalogZbozi.CZMST095.DMJColumn.ColumnName + "='" + mjdef + "'");
					if (drmj.Length > 0)
						indexfound = dtdmj.Rows.IndexOf(drmj[0]);
				}
				// tady pokud nenajdu, tak nastavim na 1. zaznam ...
				dataGrid1.CurrentRowIndex = indexfound;

			}
			catch { }
		}

		/// <summary>
		/// Metoda pro pridani Neexistujici položky
		/// </summary>
		/// <param name="ck"></param>
		private void pridatPolozkuNeexistujici(string ck)
		{
			if (Prodej.Globals.DotazPridatNovaPolozka)
			{
				DialogResult dr = MessageBoxBig.Show(
					string.Format(Fask.Localization.Localization.Prodej3ProdejListPolozkaNenalezenaPridatNovouDotaz, ck),/*"Položka s è.k.'" + ck + "' nenalezena." + "\n\n" + "Chcete pøidat novou položku?",*/
					this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
				if (dr == DialogResult.No)
					return;
			}

			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row nzbozi = _katalogZbozi.CZMST095.NewCZMST095Row();
			nzbozi.CZ_CarKod = ck;
			nzbozi.CZ_Rez1_Track = 0;
			nzbozi.CZ_Rez2_Track = 0;
			nzbozi.CZ_Rez3_Track = 0;
			nzbozi.CZ_Rez4_Track = 0;
			nzbozi.CZ_SerNum_Delka = 0;
			nzbozi.CZ_SerNum_Track = 0;
			nzbozi.DEX_ROW_ID = 0;
			nzbozi.DMJ = string.Empty;
			nzbozi.ITEMDESC = ck;
			nzbozi.ITEMNMBR = ck;
			nzbozi.LOCNCODE = string.Empty;
			nzbozi.MJ = string.Empty;
			nzbozi.PRICE0 = 0;
			nzbozi.PRICE1 = 0;
			nzbozi.PRICE2 = 0;
			nzbozi.PRICE3 = 0;
			nzbozi.PRICE4 = 0;
			nzbozi.PRICE5 = 0;
			nzbozi.QTY = 0;
			nzbozi.QTYPACK = 0;
			nzbozi.REZ1 = string.Empty;
			nzbozi.SKL_ID = string.Empty;
			nzbozi.TAXRATE = 0;
			nzbozi.VNDITNUM = ck;
			_katalogZbozi.CZMST095.AddCZMST095Row(nzbozi);

			//zta.Update(_katalogZbozi.CZMST095); //Updatovat??? => prozatim ne ... zaplacaval byse prostor ...

			pridatPolozku(nzbozi);

		}


		#endregion

		#endregion

		#region Metody/udalosti na componenty

		/// <summary>
		/// Event KeyDown na DataGridu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridNasnimane_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == MST_Global.DataGridScrollDown)
			{
				if (dataGridNasnimane.CurrentRowIndex == _db_records_per_view - 1)
				{
					toolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(toolBarButtonNext));
					e.Handled = true;
					return;
				}
			}
			else if (e.KeyCode == MST_Global.DataGridScrollUp)
			{
				if (dataGridNasnimane.CurrentRowIndex == 0)
				{
					toolBar1_ButtonClick(this, new ToolBarButtonClickEventArgs(toolBarButtonPrev));
					e.Handled = true;
					return;
				}
			}
		}

		/// <summary>
		/// Event MouseUp na DataGridu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridNasnimane_MouseUp(object sender, MouseEventArgs e)
		{
			System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = dataGridNasnimane.HitTest(e.X, e.Y);
			if (hittestinfo.Type == DataGrid.HitTestType.RowResize)
			{
				int rowH = dataGrid1.RowHeightGet(hittestinfo.Row);
				dataGridNasnimane.RowHeightDefault = rowH;
			}
		}

		/// <summary>
		/// Event resize na panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelNasnimaneDetail_Resize(object sender, EventArgs e)
		{
			//string a = "H:" + panelNasnimaneDetail.Size.Height + "W:" + panelNasnimaneDetail.Width+ Environment.NewLine ;
			//a += "lH" + labelItemnmbrN.Size.Height + "lW" + labelItemnmbrN.Size.Width;
			//MessageBox.Show(a);

			labelItemnmbrN.Width = panelNasnimaneDetail.Width - label18.Width;
			labelQTYN.Width = panelNasnimaneDetail.Width - label16.Width;
			labelQTYPACKN.Width = panelNasnimaneDetail.Width - label8.Width;
			labelSNN.Width = panelNasnimaneDetail.Width - label17.Width;
			labelNactenoN.Width = panelNasnimaneDetail.Width - label7.Width;
			labelZakladN.Width = panelNasnimaneDetail.Width - label6.Width;
			labelSDPHN.Width = panelNasnimaneDetail.Width - label4.Width;

		}

		/// <summary>
		/// Event resize na panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelDetailPolozky_Resize(object sender, EventArgs e)
		{
			df_Itemnmbr.Width = panelDetailPolozky.Width;
			df_Sklad.Width = panelDetailPolozky.Width;
			df_VNDITNUM.Width = panelDetailPolozky.Width;
			df_CZCarKod.Width = panelDetailPolozky.Width;
			df_QTY.Width = panelDetailPolozky.Width;
			df_QTYPACK.Width = panelDetailPolozky.Width;
			df_TAXRATE.Width = panelDetailPolozky.Width;
			df_PRICE.Width = panelDetailPolozky.Width;
			df_SNTrack.Width = panelDetailPolozky.Width;
			df_nasklade_l.Width = panelDetailPolozky.Width;
			df_datumnasklade_l.Width = panelDetailPolozky.Width;
			df_WEIGHT.Width = panelDetailPolozky.Width;
			df_MJ.Width = panelDetailPolozky.Width;
			df_REZ1.Width = panelDetailPolozky.Width;
			df_REZ2.Width = panelDetailPolozky.Width;
			df_REZ3.Width = panelDetailPolozky.Width;
			df_REZ4.Width = panelDetailPolozky.Width;
		}

		/// <summary>
		/// Event resize na panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panel2_Resize(object sender, EventArgs e)
		{
			df_Itemnmbr_DN.Width = panel2.Width;
			df_Nacteno_DN.Width = panel2.Width;
			df_QTY_DN.Width = panel2.Width;
			df_QTYPACK_DN.Width = panel2.Width;
			df_SN_DN.Width = panel2.Width;

			df_Cena_sDPH_DN.Width = panel2.Width;
			df_Cena_MJ_DN.Width = panel2.Width;

			df_REZ1_DN.Width = panel2.Width;
			df_REZ2_DN.Width = panel2.Width;
			df_REZ3_DN.Width = panel2.Width;
			df_REZ4_DN.Width = panel2.Width;
		}

		/// <summary>
		/// Event resize na panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panelZboziDetail_Resize(object sender, EventArgs e)
		{
			labelItemnmbrL.Width = panelZboziDetail.Width - label2.Width;
			labelVNDITNUML.Width = panelZboziDetail.Width - label3.Width;
			labelPRICEL.Width = panelZboziDetail.Width - label5.Width;
		}

		/// <summary>
		/// Event MouseUp na DataGridu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGrid1_MouseUp(object sender, MouseEventArgs e)
		{
			System.Windows.Forms.DataGrid.HitTestInfo hittestinfo = dataGrid1.HitTest(e.X, e.Y);
			if (hittestinfo.Type == DataGrid.HitTestType.RowResize)
			{
				int rowH = dataGrid1.RowHeightGet(hittestinfo.Row);
				dataGrid1.RowHeightDefault = rowH;
			}
		}

		/// <summary>
		/// Event KeyDown na DataGridu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == MST_Global.DataGridScrollDown)
			{
				if (dataGrid1.CurrentRowIndex == _db_records_per_view - 1)
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
		}

		/// <summary>
		/// Event CurrentCellChanged na DataGrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
		{
			try
			{
				dataGrid1.UnSelect(selectedrowindex);
			}
			catch
			{
			}

			try
			{
				selectedrowindex = dataGrid1.CurrentRowIndex;
				dataGrid1.Select(selectedrowindex);
			}
			catch
			{
			}

			UpdateForm();
		}

		/// <summary>
		/// Event CurrentCellChanged na DataGrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridNasnimane_CurrentCellChanged(object sender, EventArgs e)
		{
			try
			{
				dataGridNasnimane.UnSelect(selectedrowindexN);
			}
			catch
			{
			}

			try
			{
				selectedrowindexN = dataGridNasnimane.CurrentRowIndex;
				dataGridNasnimane.Select(selectedrowindexN);
			}
			catch
			{
			}

			UpdateForm();
		}

		/// <summary>
		/// Event click na toolBaru
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == toolBarButtonFirst)
			{
				_db_record_actual = 0;
			}
			else if (e.Button == toolBarButtonPrev)
			{
				_db_record_actual -= _db_records_per_view;
				if (_db_record_actual < 0)
					_db_record_actual = 0;
			}
			else if (e.Button == toolBarButtonNext)
			{
				_db_record_actual += _db_records_per_view;
				if (_db_record_actual > (_db_records_count - _db_records_per_view))
					_db_record_actual = _db_records_count - _db_records_per_view;

				if (_db_record_actual < 0)
					_db_record_actual = 0;
			}
			else if (e.Button == toolBarButtonLast)
			{
				_db_record_actual = _db_records_count - _db_records_per_view;

				if (_db_record_actual < 0)
					_db_record_actual = 0;
			}

			LoadZbozi(_db_record_actual, _db_record_actual + _db_records_per_view);
		}


		#endregion

		#region Metody použivane v 'PridatPolozku'

		/// <summary>
		/// Metoda slouživi pro rozhodnuti zda položku dat na Sklad/Expedici alebo rozdelit
		/// </summary>
		private void RozhodovatSkladExpediceRozdelit(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
		{
			decimal Nasnimano = Nacteno(di.ITEMNMBR);
			//if (true)
			if (_typdokladu != null && !_typdokladu.Iscfg_NavrhNull() && _typdokladu.cfg_Navrh > 0)
			{

				decimal PrijimaneMnozstvi = di.QTYSHPPDMJ;

				decimal MnozstviDodavatelePozadovano = 0; ;
				decimal MnozstviDodavateleDodano = 0;
				decimal MnozstviDodavateleDodat = 0;
				decimal MnozstviOdberateliPozadovano = 0;
				decimal MnozstviOdberatelumDodano = 0;
				decimal MnozstviOdberatelumDodat = 0;
				decimal Vysledek = 0;

				//OnlineGetSkladExpedice(PERow.ITEMNMBR, ppp.Kod, "0", PERow.SERLTNUM);
				OnlineGetSkladExpedice(
									di.ITEMNMBR,
									PrijimaneMnozstvi,
									Nasnimano,
									out  MnozstviDodavatelePozadovano,
									out  MnozstviDodavateleDodano,
									out  MnozstviDodavateleDodat,
									out  MnozstviOdberateliPozadovano,
									out  MnozstviOdberatelumDodano,
									out  MnozstviOdberatelumDodat,
									out  Vysledek
									);

				decimal NaSklad = 0;
				decimal NaExpedici = 0;


				if (Vysledek >= PrijimaneMnozstvi)
				{ // ma se jeste dodat "Vysledek" a prijimam mene nez dodavam ...
					NaExpedici = PrijimaneMnozstvi;
					NaSklad = 0;
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundExpedice));
				}
				else if ((0 < Vysledek) && (Vysledek < PrijimaneMnozstvi))
				{
					NaExpedici = Vysledek;
					NaSklad = PrijimaneMnozstvi - Vysledek;
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSkladExpedice));
				}
				else if (Vysledek <= 0)
				{
					NaExpedici = 0;
					NaSklad = PrijimaneMnozstvi;
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSklad));
				}


				if (Prodej.Globals.ZobrazovatReport && !Prodej.Globals.Mnozstvi1Auto)
				{

					//TaD sledovani na mnozstvi 22.05.2018 Hanibal 
					using (Fask.MST_W.Forms.FormReport frmrep = new Fask.MST_W.Forms.FormReport())
					{
						//frmrep.PERow = PERow;
						//frmrep.ds = ds;


						frmrep.CZ_CarKod = di.CZ_CarKod;
						frmrep.ITEMDESC = di.ITEMDESC;
						frmrep.ITEMNMBR = di.ITEMNMBR;


						frmrep.NaExpedici = NaExpedici;
						frmrep.NaSklad = NaSklad;

						frmrep.MnozstviDodavatelePozadovano = MnozstviDodavatelePozadovano;
						frmrep.MnozstviDodavateleDodano = MnozstviDodavateleDodano;
						frmrep.MnozstviDodavateleDodat = MnozstviDodavateleDodat;
						frmrep.MnozstviOdberateliPozadovano = MnozstviOdberateliPozadovano;
						frmrep.MnozstviOdberatelumDodano = MnozstviOdberatelumDodano;
						frmrep.MnozstviOdberatelumDodat = MnozstviOdberatelumDodat;


						if (frmrep.ShowDialog() == DialogResult.Cancel)
							return;

					}
				}

			}
		}

		/// <summary>
		/// Metoda pro poskladani objektu a online zavolani a nasledne rozparsovani objektu... 
		/// </summary>
		/// <param name="ITEMNMBR">ID položky</param>
		/// <param name="MnozstviZadane">Mnozstvi Zadane</param>
		/// <param name="MnozstviNasnimane">Mnozstvi Nasnimane</param>
		/// <param name="MnozstviDodavatelePozadovano"> Mnozstvi Dodavatele Pozadovano</param>
		/// <param name="MnozstviDodavateleDodano">Mnozstvi Dodavatele Dodano</param>
		/// <param name="MnozstviDodavateleDodat">Mnozstvi Dodavatele Dodat</param>
		/// <param name="MnozstviOdberateliPozadovano">Mnozstvi Odberateli Pozadovano</param>
		/// <param name="MnozstviOdberatelumDodano">Mnozstvi Odberatelum Dodano</param>
		/// <param name="MnozstviOdberatelumDodat">Mnozstvi Odberatelum Dodat</param>
		/// <param name="Vysledek">Vysledek</param>
		/// <returns>true- OK, False- chyba</returns>
		private void OnlineGetSkladExpedice(
						string ITEMNMBR,
						decimal MnozstviZadane,
						decimal MnozstviNasnimane,
						out decimal MnozstviDodavatelePozadovano,
						out decimal MnozstviDodavateleDodano,
						out decimal MnozstviDodavateleDodat,
						out decimal MnozstviOdberateliPozadovano,
						out decimal MnozstviOdberatelumDodano,
						out decimal MnozstviOdberatelumDodat,
						out decimal Vysledek
						)
		{
			//DataSet ds = new DataSet();// PrijemService.Obecne();

			MnozstviDodavatelePozadovano =
			MnozstviDodavateleDodano =
			MnozstviDodavateleDodat =
			MnozstviOdberateliPozadovano =
			MnozstviOdberatelumDodano =
			MnozstviOdberatelumDodat =
			Vysledek = 0;

			try
			{
				ProdejService.ProdejService prodejService = new Fask.MST_W.ProdejService.ProdejService();
				prodejService.Url = MST_Global.ServerAddress + "Prodej.asmx";
				prodejService.Timeout = MST_Global.ServiceTimeOut;
				prodejService.UpdateWebServiceCredentials();


				Fask.MST_W.ProdejService.VstupniObjekt ObjektIN = new Fask.MST_W.ProdejService.VstupniObjekt();

				ObjektIN.DOC_ID = _typdokladu.doc_id;
				ObjektIN.DOC_ID2 = _typdokladu.doc_id2;
				ObjektIN.SKL_ID = _typdokladu.SKL_ID;

				ObjektIN.ITEMNMBR = ITEMNMBR.Trim();
				ObjektIN.MnozstviNasnimane = MnozstviNasnimane;
				ObjektIN.MnozstviZadane = MnozstviZadane;

				Fask.MST_W.ProdejService.VystupniObjekt ObjektOUT = prodejService.Online_UniverzalnyDotazNaCokoliv(ObjektIN);

				MnozstviDodavatelePozadovano = ObjektOUT.MnozstviDodavatelePozadovano;
				MnozstviDodavateleDodano = ObjektOUT.MnozstviDodavateleDodano;
				MnozstviDodavateleDodat = ObjektOUT.MnozstviDodavateleDodat;
				MnozstviOdberateliPozadovano = ObjektOUT.MnozstviOdberateliPozadovano;
				MnozstviOdberatelumDodano = ObjektOUT.MnozstviOdberatelumDodano;
				MnozstviOdberatelumDodat = ObjektOUT.MnozstviOdberatelumDodat;
				Vysledek = ObjektOUT.Vysledek;


			}

			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "Prijem.PrijemList, OnlineGetSkladExpedice");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

				//return false;
			}
			//return true;
		}


		private DialogResult Get_SN_Sarze_AtributToSN(
			out string sarzaOUT,
			string sn,
			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
			string Popis,
			string Text,
			bool AllowEmpty
			)
		{
			sarzaOUT = string.Empty;

			if (naplnpSerialNumber == null)
				naplnpSerialNumber = new ProdejPridatPolozku(Popis, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, AllowEmpty, "", null, null);


			naplnpSerialNumber.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
			naplnpSerialNumber.Odberatel = _odberatel;
			naplnpSerialNumber.Zbozi = zbozi;
			naplnpSerialNumber.Popis = Popis;
			naplnpSerialNumber.Text = Text;
			naplnpSerialNumber.Len = (decimal)(zbozi.CZ_SerNum_Delka == 0 ? (_typdokladu.cfg_delka_SN != null ? _typdokladu.cfg_delka_SN : 0) : zbozi.CZ_SerNum_Delka);
			naplnpSerialNumber.Serltnum = sn;
			naplnpSerialNumber.Kod = sn;
			naplnpSerialNumber.Volajici = Volajici_ProdejPridatPolozku.SERLTNUM;



			if (naplnpSerialNumber.ShowDialog() == DialogResult.Cancel)
			{
				return DialogResult.Cancel;
			}
			else
			{
				sarzaOUT = naplnpSerialNumber.Kod;
				return DialogResult.OK;
			}

			
		}

		#endregion

		#region Zobrazit Alet Lokaci

		private void ZobrazitAlternativyLokaci()
		{
			if (this.SelectedZbozi == null)
				return;

			if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0)
			{
				//OK

				try
				{
					ScannerStop();

					using (Alter_LokaciList lokacelist = new Alter_LokaciList())
					{
						lokacelist.ITEMNMBR = this.SelectedZbozi.ITEMNMBR;
						lokacelist.SKL_ID = this.SelectedZbozi.SKL_ID;
						lokacelist.QTY = 1;

						DialogResult dr = lokacelist.ShowDialog();

						if (dr == DialogResult.OK)
						{
							//V tomto bode by mnela nastat doplneni vybranej polozky....
						}
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
			else
			{
				return;
			}
		}

		#endregion
	}
}