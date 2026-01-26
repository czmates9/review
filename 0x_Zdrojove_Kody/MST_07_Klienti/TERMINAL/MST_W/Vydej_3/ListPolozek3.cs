using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using System.Media;
using Fask.Graphic;
using System.Data.SqlClient;
using Fask.MST_W.ServerAccess;
using Fask.MST_W.Classes;
using Fask.Parsing.Codes;
using System.Linq;
using Fask.Parsing.Codes.Interfaces;

namespace Fask.MST_W.Vydej_3
{
    public partial class ListPolozek3 : System.Windows.Forms.Form
    {
        //Typ vyberu - vice viz Classes.Enums (0 == nesnastaveno, ale nemelo by byt - znaci chybu v programu)
        private byte _input_mode = 0;

        /// <summary>
        /// Posledni pouzita expirace
        /// </summary>
        DateTime expiraceLast = DateTime.Now.AddDays(30); // TODO : konfiguracne delku expirace?

        delegate void DelegateString(string kod);

        decimal nasnimat = 0;
        decimal nasnimano = 0;
        int pol_nasnimat = 0;
        int pol_nasnimano = 0;

        private enum RezimZobrazeni
        {
            List,
            Detail
        }

        #region Promenne
        //string filename = string.Empty; //nazev sql ce databaze, ktera je aktualne pouzita

        public static ListPolozek3 Instance = null;

        private Fask.SQLiteDBs.DataSets.Vydej vydejDataParametry;   //Obsahuje nactena data parametru a docasne data predlohy(czmst_se)
        private Vydej_3.ListPolozekVydej listPolozekVydej;  //Pohled na data, obsahuje polozky vytridene podle klice Itemnmbr+Sopnumbe+Ord
        private DataView pohlad;                //Pohled na data listpolozkavydej
        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _sklad = null;

        // formulare pro rychlejsi zobrazovani ...
        SejmiKodInfoForm3 sejmiForm = new SejmiKodInfoForm3(string.Empty, SejmiKodForm.TypeOfCode.AlphaNumeric, null);
        SejmiKodInfoForm3 sejmiFormLokace = new SejmiKodInfoForm3(string.Empty, SejmiKodForm.TypeOfCode.AlphaNumeric, null);
        SejmiKodInfoForm4 sejmiForm4 = new SejmiKodInfoForm4(string.Empty, SejmiKodForm.TypeOfCode.AlphaNumeric, null);
        SejmiKodInfoFormSN sejmiFormSN = new SejmiKodInfoFormSN(string.Empty, SejmiKodFormDropdown.TypeOfCode.AlphaNumeric, null);

        private string statusInfoRazeni = string.Empty;
        #endregion

        #region Vlastnosti

        private RezimZobrazeni _zobrazeni = RezimZobrazeni.List;
        private RezimZobrazeni Zobrazeni
        {
            get { return _zobrazeni; }
            set
            {
                _zobrazeni = value;
                RezimZobrazeniUpdate();
            }
        }

        private Fask.MST_W.Classes.Paleta _paleta;
        /// <summary>
        /// Typ a oznaceni palety
        /// </summary>
        public Fask.MST_W.Classes.Paleta Paleta
        {
            get { return _paleta; }
            set { _paleta = value; }
        }

        private string _OdberatelID;
        /// <summary>
        /// ID vybraneho odberatele
        /// </summary>
        public string OdberatelID
        {
            get { return _OdberatelID; }
            set { _OdberatelID = value; }
        }

        private string _lokaceIDPolozka;
        public string LokaceIDPolozka
        {
            get { return _lokaceIDPolozka; }
            set { _lokaceIDPolozka = value; }
        }
        private string _lokaceNazevPolozka = string.Empty;
        public string LokaceNazevPolozka
        {
            get { return _lokaceNazevPolozka; }
            set { _lokaceNazevPolozka = value; }
        }

        private string _lokaceNazev = string.Empty;
        public string LokaceNazev
        {
            get { return _lokaceNazev; }
        }

        private string _lokaceID;
        public string LokaceID
        {
            get { return _lokaceID; }
            set
            {
                _lokaceID = value;
                try
                {
					object obj = Vydej.vydejInstance.globalObject.controller_lokace.CZMST094_GetLocDesc(_lokaceID, _sklad == null ? string.Empty : _sklad.skl_id);
                    _lokaceNazev = ((string)obj ?? string.Empty).Trim();
                }
                catch
                {
                    _lokaceNazev = string.Empty;
                }
            }
        }

        /// <summary>
        /// Vraci aktualni vybranou polozku v pohledu
        /// </summary>
        public Vydej_3.ListPolozekVydej.ListPolozekRow PolozkaAktualniVybrana
        {
            get
            {
                try
                {
                    return (dataGrid1.BindingContext[dataGrid1.DataSource].Current as DataRowView).Row as Vydej_3.ListPolozekVydej.ListPolozekRow;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion

        #region Construct

        DataGrid2NumberBoxColumn dgcsOstava = null;
        DataGrid2TextBoxColumn dgcsNazov = null;
        DataGrid2NumberBoxColumn dgcsMnozstvo = null;
        DataGrid2NumberBoxColumn dgcsNasnimano = null;
        DataGrid2TextBoxColumn dgcsCZ_CarKod = null;
        DataGrid2TextBoxColumn dgcsVNDITNUM = null;
        DataGrid2TextBoxColumn dgcsLokace = null;
        DataGrid2TextBoxColumn dgcsNote = null;

        public ListPolozek3(Fask.SQLiteDBs.DataSets.Vydej vydejDataParametry, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                InitializeComponent();
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);


                //df_Baleni.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Itemdesc.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Itemnmbr.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Locncode.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Nasnimano.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Quantity.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Sopnumbe.DataFont = new Font("Arial", 15, FontStyle.Bold);
                //df_VNDDOCNM.DataFont = new Font("Arial", 15, FontStyle.Bold);

                //df_Baleni.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Itemdesc.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Itemnmbr.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Locncode.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Nasnimano.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Quantity.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_Sopnumbe.PopisFont = new Font("Arial", 15, FontStyle.Bold);
                //df_VNDDOCNM.PopisFont = new Font("Arial", 15, FontStyle.Bold);




            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                //throw;
            }

            Instance = this;
            this._sklad = sklad;
            // nastaveni defaultniho rezimu kvuli predchozi inicializaci SejmiKodFormu
            Components.KeyboardManager.SetDefault();

            miTisk.Enabled = MST_Global.PovolitPrintServer;
            miPaleta.Enabled = MST_Global.VydejTypOznaceniPalety; //zakaze zmenu oznaceni palety
            menuItemZmenaLokace.Enabled = MST_Global.VydejLocationFiltrovatData; //zakaze/povoli zmenu filtru lokace
            //miDocipovat.Enabled = MST_Global.VydejPovolitOcipovani;      // povoli/zakaze ocipovani
            //menuItem10.Enabled = MST_Global.VydejPovolitOcipovani;      // povoli/zakaze ocipovani
            if (!MST_Global.VydejPovolitOcipovani || !MST_Global.RFIDPovolitUHF)
            {
                if (menuItem1.MenuItems.Contains(menuItemRFID))
                    menuItem1.MenuItems.Remove(menuItemRFID);
            }

            if (vydejDataParametry != null && vydejDataParametry.Parametry.Count > 0 && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
            { }
            else
            {
                if (menuItem1.MenuItems.Contains(miZobrazitAlternativyLokaci))
                    menuItem1.MenuItems.Remove(miZobrazitAlternativyLokaci);
            }

            miZmenaRezimuSN.Checked = MST_Global.Vydej_HromadneSN;
			miZmenaRezimuBaliku.Checked = MST_Global.Vydej_HromadneBaliky;
			menuItemPtatSeNaPamatovani.Checked = MST_Global.Vydej_PtatSeNaPamatovani;

            this.vydejDataParametry = vydejDataParametry;
            //this.filename = filename;

            //try
            //{
            //    if (MST_Global.VydejLocationPouzitCiselnik)
            //    {
            //        _ta_lokace.Connection.Open();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
            //}

            this.listPolozekVydej = new Fask.MST_W.Vydej_3.ListPolozekVydej();
            this.pohlad = new DataView(this.listPolozekVydej.ListPolozek);
            this.pohlad.RowStateFilter = DataViewRowState.CurrentRows;
            this.dataGrid1.DataSource = pohlad;

            CreateGridStyles();

            MyInitializeGrid();

            MyInitializePopisItems();

            LoadDataGrid();

            //FiltrVseNeuplne();
            filtrZobrazeniPolozek = Settings.VydejZobrazeniFiltrVseNeuplne;
            UpdateFilter();

            this.dataGrid1.CurrentRowIndex = 0;

            this.menuItem23.Enabled = MST_Global.VydejObjednavkaDetail;
            this.menuItemKusu.Enabled = MST_Global.VydejPocetKusuOnline;
            this.menuItemKusuNaSklade.Enabled = MST_Global.VydejPocetKusuNaSkladeOnline;
            this.menuItemDetailItemnumber.Enabled = MST_Global.VydejPolozkaDetailOnline;
            this.menuItem8.Enabled = MST_Global.VydejPovolitZmenuOdberatele;


            Cursor.Current = Cursors.Default;
        }

        private void MyInitializePopisItems()
        {
            df_Baleni.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.QTYPACKColumn.ColumnName].HeaderText + ":";
            df_Itemdesc.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.NazovColumn.ColumnName].HeaderText + ":";
            df_Itemnmbr.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.ItemnmbrColumn.ColumnName].HeaderText + ":";
            df_Locncode.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.LokaceColumn.ColumnName].HeaderText + ":";
            df_Nasnimano.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.PocetNasnimColumn.ColumnName].HeaderText + ":";
            df_Quantity.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.MnozstvoColumn.ColumnName].HeaderText + ":";
            df_Sopnumbe.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.SOPNUMBEColumn.ColumnName].HeaderText + ":";
            df_VNDDOCNM.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.VNDDOCNMColumn.ColumnName].HeaderText + ":";
            df_CarKod.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.CZ_CarKodColumn.ColumnName].HeaderText + ":";
            df_Note.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.NoteColumn.ColumnName].HeaderText + ":";
            df_MJ.Popis = dataGrid1.TableStyles[0].GridColumnStyles[listPolozekVydej.ListPolozek.MJColumn.ColumnName].HeaderText + ":";

        }

        private void CreateGridStyles()
        {
            DataGridTableStyle dgstyle = new DataGridTableStyle();
            dgstyle.MappingName = listPolozekVydej.ListPolozek.TableName;

            dgcsNazov = new DataGrid2TextBoxColumn();
            dgcsNazov.MappingName = listPolozekVydej.ListPolozek.NazovColumn.ColumnName;
            dgcsNazov.HeaderText = "Název Položky";
            dgcsNazov.NullText = "-";
            dgcsNazov.Width = 100;
            dgstyle.GridColumnStyles.Add(dgcsNazov);

			DataGrid2NumberBoxColumn kod = new DataGrid2NumberBoxColumn();
			kod.MappingName = listPolozekVydej.ListPolozek.ITEMCODEColumn.ColumnName;
			kod.HeaderText = "Kód";
			kod.NullText = "-";
			kod.Width = 80;
			dgstyle.GridColumnStyles.Add(kod);

            dgcsOstava = new DataGrid2NumberBoxColumn();
            dgcsOstava.MappingName = listPolozekVydej.ListPolozek.OstavaColumn.ColumnName;
            dgcsOstava.HeaderText = "Zbývá";
            dgcsOstava.NullText = "-";
            dgcsOstava.Width = 80;
            dgcsOstava.Alignment = StringAlignment.Far;
            dgcsOstava.Format = "N";
            dgstyle.GridColumnStyles.Add(dgcsOstava);

            dgcsMnozstvo = new DataGrid2NumberBoxColumn();
            dgcsMnozstvo.MappingName = listPolozekVydej.ListPolozek.MnozstvoColumn.ColumnName;
            dgcsMnozstvo.HeaderText = "Množství";
            dgcsMnozstvo.NullText = "-";
            dgcsMnozstvo.Alignment = StringAlignment.Far;
            dgcsMnozstvo.Width = 80;
            dgcsMnozstvo.Format = "N";
            dgstyle.GridColumnStyles.Add(dgcsMnozstvo);

            dgcsNasnimano = new DataGrid2NumberBoxColumn();
            dgcsNasnimano.MappingName = listPolozekVydej.ListPolozek.PocetNasnimColumn.ColumnName;
            dgcsNasnimano.HeaderText = "Nasnímáno";
            dgcsNasnimano.NullText = "-";
            dgcsNasnimano.Alignment = StringAlignment.Far;
            dgcsNasnimano.Width = 80;
            dgcsNasnimano.Format = "N";
            dgstyle.GridColumnStyles.Add(dgcsNasnimano);

            dgcsCZ_CarKod = new DataGrid2TextBoxColumn();
            dgcsCZ_CarKod.MappingName = listPolozekVydej.ListPolozek.CZ_CarKodColumn.ColumnName;
            dgcsCZ_CarKod.HeaderText = "Vlastní è.k.";
            dgcsCZ_CarKod.NullText = "-";
            dgcsCZ_CarKod.Width = 80;
            dgstyle.GridColumnStyles.Add(dgcsCZ_CarKod);

            dgcsVNDITNUM = new DataGrid2TextBoxColumn();
            dgcsVNDITNUM.MappingName = listPolozekVydej.ListPolozek.VNDITNUMColumn.ColumnName;
            dgcsVNDITNUM.HeaderText = "Èár. kód";
            dgcsVNDITNUM.NullText = "-";
            dgcsVNDITNUM.Width = 80;
            dgstyle.GridColumnStyles.Add(dgcsVNDITNUM);

            dgcsNote = new DataGrid2TextBoxColumn();
            dgcsNote.MappingName = listPolozekVydej.ListPolozek.NoteColumn.ColumnName;
            dgcsNote.HeaderText = "Pozn.";
            dgcsNote.NullText = "-";
            dgcsNote.Width = 80;
            dgstyle.GridColumnStyles.Add(dgcsNote);

            dgcsLokace = new DataGrid2TextBoxColumn();
            dgcsLokace.MappingName = listPolozekVydej.ListPolozek.LokaceColumn.ColumnName;
            dgcsLokace.HeaderText = MST_Global.LC_NAME;
            dgcsLokace.NullText = "-";
            dgcsLokace.Width = 80;
            dgstyle.GridColumnStyles.Add(dgcsLokace);

            DataGrid2TextBoxColumn dgitemnmbr = new DataGrid2TextBoxColumn();
            dgitemnmbr.MappingName = listPolozekVydej.ListPolozek.ItemnmbrColumn.ColumnName;
            dgitemnmbr.HeaderText = "Položka è.";
            dgitemnmbr.NullText = "-";
            dgitemnmbr.Width = 80;
            dgstyle.GridColumnStyles.Add(dgitemnmbr);

            DataGrid2TextBoxColumn dgOrd = new DataGrid2TextBoxColumn();
            dgOrd.MappingName = listPolozekVydej.ListPolozek.ORDColumn.ColumnName;
            dgOrd.HeaderText = "Poøadí";
            dgOrd.NullText = "-";
            dgOrd.Width = 80;
            dgOrd.Format = "0";
            dgstyle.GridColumnStyles.Add(dgOrd);

            DataGrid2TextBoxColumn dgSopnumbe = new DataGrid2TextBoxColumn();
            dgSopnumbe.MappingName = listPolozekVydej.ListPolozek.SOPNUMBEColumn.ColumnName;
            dgSopnumbe.HeaderText = "Objednávka";
            dgSopnumbe.NullText = "-";
            dgSopnumbe.Width = 80;
            dgstyle.GridColumnStyles.Add(dgSopnumbe);

            DataGrid2TextBoxColumn dgVnddocnm = new DataGrid2TextBoxColumn();
            dgVnddocnm.MappingName = listPolozekVydej.ListPolozek.VNDDOCNMColumn.ColumnName;
            dgVnddocnm.HeaderText = "Dokument";
            dgVnddocnm.NullText = "-";
            dgVnddocnm.Width = 80;
            dgstyle.GridColumnStyles.Add(dgVnddocnm);

            DataGrid2NumberBoxColumn dgQtypack = new DataGrid2NumberBoxColumn();
            dgQtypack.MappingName = listPolozekVydej.ListPolozek.QTYPACKColumn.ColumnName;
            dgQtypack.HeaderText = "Balení";
            dgQtypack.NullText = "-";
            dgQtypack.Width = 80;
            dgstyle.GridColumnStyles.Add(dgQtypack);

            DataGrid2NumberBoxColumn mj = new DataGrid2NumberBoxColumn();
            mj.MappingName = listPolozekVydej.ListPolozek.MJColumn.ColumnName;
            mj.HeaderText = "MJ";
            mj.NullText = "-";
            mj.Width = 80;
            dgstyle.GridColumnStyles.Add(mj);


            dataGrid1.TableStyles.Add(dgstyle);

            dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;
        }

        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        #endregion

        #region Functions

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

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;

            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            ScannerFinalize();

            if (sejmiForm != null)
            {
                sejmiForm.Dispose();
                sejmiForm = null;
            }

            if (sejmiForm4 != null)
            {
                sejmiForm4.Dispose();
                sejmiForm4 = null;
            }

            if (sejmiFormSN != null)
            {
                sejmiFormSN.Dispose();
                sejmiFormSN = null;
            }

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));

            //if (_ta_lokace != null && _ta_lokace.Connection.State == ConnectionState.Open)
            //    _ta_lokace.Connection.Close();

            Settings.VydejZobrazeniFiltrVseNeuplne = filtrZobrazeniPolozek;

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Pocet nactenych polozek vypocteny z pohledu na data z pole "Ostava"
        /// </summary>
        /// <param name="ITEMNMBR"></param>
        /// <param name="SOPNUMBE"></param>
        /// <param name="ORD"></param>
        /// <returns></returns>
        public decimal Nacteno(string ITEMNMBR, string SOPNUMBE, int ORD)
        {
            object res = listPolozekVydej.ListPolozek.Compute(
                "SUM(PocetNasnim)",
                "Itemnmbr='" + ITEMNMBR + "' and SOPNUMBE='" + SOPNUMBE + "' AND ORD=" + ORD
                );

            decimal nacteno2 = 0;
            try { nacteno2 = Convert.ToDecimal(res); }
            catch { }
            return nacteno2;

        }

        /// <summary>
        /// Vraci celkovy pocet nacteneho mnozstvi pro zadane itemnmbr (pouziti u hromadneho vydavani)
        /// </summary>
        /// <param name="ITEMNMBR"></param>
        /// <returns></returns>
        public decimal Nacteno(string ITEMNMBR)
        {
            object res = listPolozekVydej.ListPolozek.Compute(
                "SUM(PocetNasnim)",
                "Itemnmbr='" + ITEMNMBR + "'"
                );


            decimal nacteno2 = 0;
            try { nacteno2 = Convert.ToDecimal(res); }
            catch { }
            return nacteno2;

        }

        /// <summary>
        /// Celkovy pocet mnozstvi k nacteni pro zadane itemnmbr
        /// </summary>
        /// <param name="ITEMNMBR"></param>
        /// <returns></returns>
        public decimal Nacist(string ITEMNMBR)
        {
            object res = listPolozekVydej.ListPolozek.Compute(
                "SUM(Mnozstvo)",
                "Itemnmbr='" + ITEMNMBR + "'"
                );


            decimal nacteno2 = 0;
            try { nacteno2 = Convert.ToDecimal(res); }
            catch { }
            return nacteno2;

        }


        /// <summary>
        /// Zbyvajici pocet mnozstvi k nacteni pro zadane itemnmbr
        /// </summary>
        /// <param name="ITEMNMBR"></param>
        /// <returns></returns>
        public decimal Zbyva(string ITEMNMBR)
        {
            object res = listPolozekVydej.ListPolozek.Compute(
                "SUM(Ostava)",
                "Itemnmbr='" + ITEMNMBR + "'"
                );


            decimal nacteno2 = 0;
            try { nacteno2 = Convert.ToDecimal(res); }
            catch { }
            return nacteno2;

        }

        /// <summary>
        /// Odecte od kazde polozky v datagridu, ktera odpovida klici ITEMNMNBR+SOPNUMBE+ORD, hodnotu mnozstvi od hodnoty v poli "Ostava"
        /// </summary>
        /// <param name="mnozstvi">Pri odebirani polozky z nasnimanych, zde uvadet zapornou hodnotu</param>
        /// <param name="ITEMNMBR"></param>
        /// <param name="SOPNUMBE"></param>
        /// <param name="ORD"></param>
        public void UpdateDataGrid(decimal mnozstvi, string ITEMNMBR, string SOPNUMBE, int ORD)
        {
            Vydej_3.ListPolozekVydej.ListPolozekRow[] lprows =
                (Vydej_3.ListPolozekVydej.ListPolozekRow[])listPolozekVydej.ListPolozek.Select(
                "Itemnmbr='" + ITEMNMBR + "' and SOPNUMBE='" + SOPNUMBE + "' AND ORD=" + ORD,
                null,
                DataViewRowState.CurrentRows
                );
            foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lprow in lprows)
            {
                lprow.Ostava -= mnozstvi;
            }
        }

		/// <summary>
		/// Odecte od kazde polozky v datagridu, ktera odpovida klici ITEMNMNBR+SOPNUMBE+ORD, hodnotu mnozstvi od hodnoty v poli "Ostava"
		/// </summary>
		/// <param name="mnozstvi">Pri odebirani polozky z nasnimanych, zde uvadet zapornou hodnotu</param>
		/// <param name="ITEMNMBR"></param>
		/// <param name="SOPNUMBE"></param>
		/// <param name="ORD"></param>
		public void UpdateDataGrid_Zmena(decimal mnozstvi, string ITEMNMBR, string SOPNUMBE, int ORD)
		{
			Vydej_3.ListPolozekVydej.ListPolozekRow[] lprows =
				(Vydej_3.ListPolozekVydej.ListPolozekRow[])listPolozekVydej.ListPolozek.Select(
				"Itemnmbr='" + ITEMNMBR + "' and SOPNUMBE='" + SOPNUMBE + "' AND ORD=" + ORD,
				null,
				DataViewRowState.CurrentRows
				);
			foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lprow in lprows)
			{
				lprow.Ostava = lprow.Mnozstvo - mnozstvi;
				//lprow.PocetNasnim = mnozstvi;
			}
		}

        /// <summary>
        /// Inicializuje data datagridu ... casove narocnejsi operace
        /// </summary>
        /// <returns></returns>
        private bool LoadDataGrid()
        {
            listPolozekVydej.ListPolozek.Clear();
            try
            {
                Program.mstw.mbw.BeginPracujiForm();
                Program.mstw.mbw.Zprava = "Naèítání dat pøedlohy";
                //if (zachZobrazenie == true)
                //{
                Vydej_3.ListPolozekVydej.ListPolozekRow newRow = null;
                //Vydej_3.ListPolozekVydej.ListPolozekRow[] najdene = null;

                //VydejService.Vydej vydejD = new Fask.MST_W.VydejService.Vydej();
                Fask.SQLiteDBs.DataSets.Vydej vydejD = new Fask.SQLiteDBs.DataSets.Vydej();
                //using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
                //    "Select * from czmst_se",
                //    "Data source=" + filename
                //    ))
                //{                    
                //    sda.Fill(vydejD, vydejD.CZMST_SE.TableName);
                //}
                Vydej.vydejInstance.globalObject.controller_vydej.Fill_SE(vydejD.CZMST_SE);

                int x = 0;
                int y = vydejD.CZMST_SE.Count;
                listPolozekVydej.ListPolozek.BeginLoadData();
                //foreach (VydejService.Vydej.CZMST_SERow seRow in vydejD.CZMST_SE)
                foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow in vydejD.CZMST_SE)
                {
                    Program.mstw.mbw.Zprava = String.Format("Pøipravuji položku {0} z {1}\n{2:0.##}%", ++x, y, ((float)x / y * 100));

                    //najdene = (Vydej_3.ListPolozekVydej.ListPolozekRow[])
                    //    listPolozekVydej.ListPolozek.Select(
                    //    "Itemnmbr='" + seRow.ITEMNMBR + "' and SOPNUMBE='" + seRow.SOPNUMBE + "' and ORD=" + seRow.ORD,
                    //    null,
                    //    DataViewRowState.CurrentRows);
                    var najdene = listPolozekVydej.ListPolozek.Where(z => z.Itemnmbr == seRow.ITEMNMBR && z.SOPNUMBE == seRow.SOPNUMBE && z.ORD == seRow.ORD);

                    //if (najdene.Length == 0)
                    if (najdene.Count() == 0)
                    {
                        //decimal pocetVydanych = this.Nacteno(filename, seRow.ITEMNMBR, seRow.SOPNUMBE, seRow.ORD, false);
                        decimal pocetVydanych = Vydej.vydejInstance.globalObject.controller_vydej.Nacteno(seRow.ITEMNMBR, seRow.SOPNUMBE, seRow.ORD, false);

                        newRow = listPolozekVydej.ListPolozek.NewListPolozekRow();
                        newRow.Itemnmbr = seRow.ITEMNMBR;
                        newRow.SOPNUMBE = seRow.SOPNUMBE;
                        if (!seRow.IsITEMDESCNull()) newRow.Nazov = seRow.ITEMDESC;
                        newRow.Mnozstvo = seRow.QTYSHPPD;
                        newRow.Ostava = seRow.QTYSHPPD - pocetVydanych;
                        newRow.ORD = seRow.ORD;
                        newRow.DEX_ROW_ID = seRow.DEX_ROW_ID;
                        newRow.TypPalety = seRow.IsTYPEPALNull() ? string.Empty : seRow.TYPEPAL;
                        //newRow.SSCCCode = Paleta.sccc; // TaD 
                        newRow.CountEntries = seRow.CountEntries;
                        newRow.PocetKusuPaleta = seRow.IsQTYPALNull() ? 0 : Convert.ToInt32(seRow.QTYPAL);
                        newRow.CZ_CarKod = seRow.CZ_CarKod.Trim();
                        newRow.VNDITNUM = seRow.IsVNDITNUMNull() ? string.Empty : seRow.VNDITNUM.Trim();
                        newRow.Lokace = seRow.LOCNCODE.Trim();
                        newRow.VNDDOCNM = seRow.VNDDOCNM.Trim();
                        newRow.QTYPACK = seRow.QTYPACK;
                        //newRow.Note = seRow.IsNoteNull() ? null : seRow.Note.Trim();
                        newRow.Note = seRow.Note.Trim();
                        newRow.MJ = seRow.IsMJNull() ? string.Empty : seRow.MJ;

						newRow.ITEMCODE = seRow.IsITEMCODENull() ? string.Empty : seRow.ITEMCODE.Trim();

						//newRow.WEIGHT = seRow.IsWEIGHTNull() ? 0 : seRow.WEIGHT;

                        newRow.CZ_Expirace_Track = seRow.CZ_Expirace_Track;

                        if (!seRow.IsSKL_IDNull())
                            newRow.Sklad = seRow.SKL_ID;

                        listPolozekVydej.ListPolozek.AddListPolozekRow(newRow);
                    }

                }
                listPolozekVydej.ListPolozek.EndLoadData();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Forms.MessageBoxBig.Show(ex.Message, "Chyba naètení!");
                this.finalize();
                DialogResult = DialogResult.Cancel;
                return false;
            }
            finally
            {
                Vydej.vydejInstance.globalObject.controller_vydej.NactenoFinalize();
            }
            Program.mstw.mbw.EndPracujiForm();
            return true;
        }

        /// <summary>
        /// Zobrazuje dialog s detaily objednavky - online dotaz
        /// </summary>
        private void DetailPrint()
        {
            Vydej_3.ListPolozekVydej.ListPolozekRow lprow = null;
            try
            {
                lprow = this.PolozkaAktualniVybrana;
                if (lprow == null) return;
                this.ScannerStop();
                using (Fask.MST_W.Vydej_3.Detail detail = new Fask.MST_W.Vydej_3.Detail(lprow.SOPNUMBE))
                {
                    detail.ShowDialog();
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
        /// Vyhledava polozku na zaklade caroveho kodu
        /// </summary>
        /// <param name="EANKod"></param>
        private void NajdiPolozku(string EANKod)
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "NajdiPolozku");
            Logging.Trace2.Write("Start", "SQL nalezeni polozky", tid);

            Fask.Parsing.Codes.BaseCode code = null;
            
            #region Barcode Parsing
            if (MST_Global.VydejParsovaniCarovehoKoduPovolit)
            {
                code = Parsing.ParsingFactory.Parse(EANKod, Settings.Parsing_Config);
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
                            mbscan.ParseBarcode(EANKod);
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
                    EANKod = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode ?? string.Empty;
            }
            #endregion

            try
            {

                Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();

                // hledani dle ean v predloze
                Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByEAN(vydejData.CZMST_SE, EANKod);

                // pokud nenajde dle ean, pokusi se najit dle sarze v predloze sarzi
                if (vydejData.CZMST_SE.Count == 0)
                { // najde polozky dle sn ... 

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSarze) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze)))
						EANKod = ((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerialNumber) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN)))
						EANKod = ((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;

					//if ((code is Parsing.Codes.Interfaces.ICodeSerltnmbr) && !String.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
					//    EANKod = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;

                    Vydej.vydejInstance.globalObject.controller_vydej.FillBySERLNMBR_SE_SN(vydejData.CZMST_SE_SN, EANKod);
                    //Vydej.vydejInstance.globalObject.controller_vydej.Ta_se.ClearBeforeFill = false;
                    foreach (var item in vydejData.CZMST_SE_SN)
                    {
                        Vydej.vydejInstance.globalObject.controller_vydej.FillBySOPNUMBEITEMNMBRORD_SE(vydejData.CZMST_SE, item.SOPNUMBE, item.ITEMNMBR, item.ORD);
                    }
                    //Vydej.vydejInstance.globalObject.controller_vydej.Ta_se.ClearBeforeFill = true;
                }

                // pokud nenajde v predloze, pokusi se hledat online, pokud je online hledani povoleno a je povoleno online hledani materialu
                Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow vydej_online_item = null;
                if (vydejData.CZMST_SE.Count == 0)
                {
                    var go = Vydej.vydejInstance.globalObject;
                    vydej_online_item = Online.Material.Online_Material_Get(null, go.sklad == null ? null : go.sklad.skl_id, EANKod);
                    // dohledani polozky z predlohy
                    if (vydej_online_item != null)
                        Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByItemnmbr(vydejData.CZMST_SE, vydej_online_item.Itemnmbr);
                }

                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = null;

                if (vydejData.CZMST_SE.Count == 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3ZboziSCarKodNenalezeno, EANKod)
                        , this.Text
                        , MessageBoxButtons.OK
                        , MessageBoxBigIcon.Warning
                        );
                    return;
                }
                else if (vydejData.CZMST_SE.Count == 1)
                {
                    SERow = vydejData.CZMST_SE[0];
                }
                else
                {
                    if (MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne)
                    {
                        foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow sr in vydejData.CZMST_SE)
                        {
                            decimal nacteno = Nacteno(sr.ITEMNMBR, sr.SOPNUMBE, sr.ORD);
                            if (nacteno < sr.QTYSHPPD)
                            {
                                SERow = sr;
                                break;
                            }
                        }
                    }
                }

                Logging.Trace2.Write("End", "", tid);

                Logging.TracId tid0 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "vratitDavku");
                Logging.Trace2.Write("Start", "SetCurrentRow(SERow)", tid0);

                SetCurrentRow(SERow);
                Logging.Trace2.Write("End", "SetCurrentRow(SERow)", tid0);

                Logging.TracId tid1 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "vratitDavku");
                Logging.Trace2.Write("Start", "PerformInsertData(vydejData, ref SERow, code)", tid1);

                PerformInsertData(vydejData, ref SERow, code, vydej_online_item);

                Logging.Trace2.Write("End", "PerformInsertData(vydejData, ref SERow, code)", tid1);


            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex, "ListPolozek3.NajdiPolozku()");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        /// <summary>
        /// Najdi polozku zadanou/nalezenou jinak (napr onlinem)
        /// </summary>
        /// <param name="lokaceItem">Online nalezena polozka</param>
        ///// <param name="ITEMNMBR">Cislo polozky</param>
        ///// <param name="SERLTNUM">Sarze polozky</param>
        ///// <param name="LOCNCODE">Lokace</param>
        ///// <param name="QTY">Mnozstvi</param>
		//private void NajdiPolozku(string ITEMNMBR, string SERLTNUM, string LOCNCODE, decimal QTY)
        private void NajdiPolozku(Classes.LokaceItem lokaceItem)
		{
			try
			{
                if (lokaceItem == null)
                    throw new Exception("Není zadána položka");

				Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();

				// hledani dle ean v predloze
				Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByItemnmbr(vydejData.CZMST_SE, lokaceItem.ITEMNMBR);

				// pokud nenajde dle ean, pokusi se najit dle sarze v predloze sarzi
				if (vydejData.CZMST_SE.Count == 0)
				{ 
					//Tohle by nemnelo nastat...
					MessageBoxBig.Show(string.Format("Nenalezena položka :{0}", lokaceItem.ITEMNMBR)
						, this.Text
						, MessageBoxButtons.OK
						, MessageBoxBigIcon.Warning
						);
					return;
				}

				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = null;

				if (vydejData.CZMST_SE.Count == 1)
				{
					SERow = vydejData.CZMST_SE[0];
				}
				else
				{
					if (MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne)
					{
						foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow sr in vydejData.CZMST_SE)
						{
							decimal nacteno = Nacteno(sr.ITEMNMBR, sr.SOPNUMBE, sr.ORD);
							if (nacteno < sr.QTYSHPPD)
							{
								SERow = sr;
								break;
							}
						}
					}
				}
                // TODO : 

				SetCurrentRow(SERow);
				PerformInsertData(vydejData, ref SERow, null, null, lokaceItem);

			}
			catch (System.Data.SQLite.SQLiteException ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex, "ListPolozek3.NajdiPolozku()");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				if (MST_Global.OnScannerSound_Vydej_3)
				{
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
				}
			}
		}


        private string EANKod = string.Empty;
        private void NajdiPolozku()
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "NajdiPolozku");
            Logging.Trace2.Write("Start", "Forms.InputBox.Show...", tid);

            try
            {
                ScannerStop();
                if (DialogResult.Cancel == Forms.InputBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3ZadejteCarovyKod, EANKod, out EANKod, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha))
                    return;

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Vydej3ListPolozek3HledaniCarovyKod, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                ScannerStart();
            }
            Logging.Trace2.Write("End", "Forms.InputBox.Show...", tid);

            NajdiPolozku(EANKod);

        }


        private void NajdiPolozkuNazev()
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "NajdiPolozkuNazev");
            Logging.Trace2.Write("Start", "Forms.InputBox.Show...", tid);


            string nazev = string.Empty;
            try
            {
                ScannerStop();
                if (DialogResult.Cancel == Forms.InputBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3ZadejteCastNazvuPolozky, string.Empty, out nazev, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha))
                    return;

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Vydej3ListPolozek3HledaniNazev, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                ScannerStart();
            }
            Logging.Trace2.Write("End", "Forms.InputBox.Show...", tid);

            Logging.TracId tid0 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "NajdiPolozkuNazev");
            Logging.Trace2.Write("Start", "SQL prikaz : Select * from czmst_se where ITEMDESC...", tid0);

            try
            {
                Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();
                //using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
                //    "Select * from czmst_se where ITEMDESC like '%" + nazev + "%'",
                //    "Data source=" + filename
                //    ))
                //{
                //    sda.Fill(vydejData, vydejData.CZMST_SE.TableName);
                //}
                Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByItemdescLike(vydejData.CZMST_SE, nazev);

                if (vydejData.CZMST_SE.Count == 0)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3ZboziSNazvemNenalezeno, nazev), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                }
                else
                {
                    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = null;
                    PerformInsertData(vydejData, ref SERow);
                    SetCurrentRow(SERow);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            Logging.Trace2.Write("End", "SQL prikaz : Select * from czmst_se where ITEMDESC...", tid0);

        }

        /// <summary>
        /// Nastavi aktualni radek dle vyhledaneho EAN
        /// </summary>
        /// <param name="SERow"></param>
        /// <returns>True=nalezen v pohledu, nastaven; False=nenalezen, nenastaven</returns>
        private bool SetCurrentRow(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow)
        {
            try
            {
                DataTable dt = pohlad.ToTable(false, new string[] { listPolozekVydej.ListPolozek.ItemnmbrColumn.ColumnName, listPolozekVydej.ListPolozek.SOPNUMBEColumn.ColumnName, listPolozekVydej.ListPolozek.ORDColumn.ColumnName });
                DataRow[] drows = dt.Select("itemnmbr='" + SERow.ITEMNMBR + "' and sopnumbe='" + SERow.SOPNUMBE + "' and ord=" + SERow.ORD);
                if (drows.Length > 0)
                {
                    //selectedIndexRow = dt.Rows.IndexOf(drows[0]);
                    //dataGrid1.CurrentRowIndex = selectedIndexRow;
                    dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(drows[0]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void originalneRadenie()
        {
            statusInfoRazeni = string.Empty;
            pohlad.Sort = string.Empty;
            UpdateForm();
            //UpdateStatusBar();
        }

        private void Nasnimane()
        {
            this.ScannerStop();
            try
            {
                using (ListNasnimForm3 listnasnim = new ListNasnimForm3())
                {
                    listnasnim.ShowDialog();
                }
                stavVydeje();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void StavVydaje()
        {
            this.ScannerStop();
            try
            {
                this.stavVydeje();
                using (StavVydejeForm3 stavVydeje = new StavVydejeForm3(nasnimat, nasnimano, pol_nasnimat, pol_nasnimano))
                {
                    stavVydeje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        /// <summary>
        /// Zjisti stav vydeje
        /// </summary>  
        /// <returns>True: pokud je vydej dokoncen, False: pokud ne</returns>
        /// <remarks>
        /// Stav vydeje vychazi z informaci, ktere jsou prubezne modifikovany v seznamu polozek v datagridu.
        /// Konkretne z hodnot Ostava jednotlivych polozek. Dle klice ITEMNMBR+SOPNUMBE+ORD.
        /// </remarks>
        public bool stavVydeje()
        {
            bool result = false;
            //presunuto jako promenne tridy, pro optimalizaci dotazu ...
            //decimal nasnimat, nasnimano;
            //int pol_nasnimat, pol_nasnimano;
            result = Vydej.vydejInstance.globalObject.controller_vydej.stavVydeje(out nasnimat, out nasnimano, out pol_nasnimat, out pol_nasnimano);
            //aktualizace status baru ...
            UpdateStatusBar();
            return result;
        }

        // 5.2.2020 JiS - presunuto do controlleru vydeje
        ///// <summary>
        ///// Zjisti stav vydeje
        ///// </summary>
        ///// <param name="nasnimat">Pocet kusu celkem k nasnimani</param>
        ///// <param name="nasnimano">Pocet kusu celkem nasnimano</param>
        ///// <param name="pol_nasnimat">Pocet polozek k nasnimani</param>
        ///// <param name="pol_nasnimano">Pocet polozek nasnimano</param>
        ///// <returns>True: pokud je vydej dokoncen, False: pokud ne</returns>
        //public bool stavVydeje(out decimal nasnimat, out decimal nasnimano, out int pol_nasnimat, out int pol_nasnimano)
        //{
        //    nasnimat = nasnimano = 0;
        //    pol_nasnimat = pol_nasnimano = 0;

        //    foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lprow in listPolozekVydej.ListPolozek)
        //    {
        //        nasnimat += lprow.Mnozstvo;
        //        pol_nasnimat++;

        //        nasnimano += lprow.Mnozstvo - lprow.Ostava;
        //        if (lprow.Ostava <= 0)
        //            pol_nasnimano++;
        //    }

        //    if (pol_nasnimat == pol_nasnimano)
        //        return true;
        //    else
        //        return false;
        //}


        private void DetailItemnumber()
        {
            Vydej_3.ListPolozekVydej.ListPolozekRow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.PolozkaAktualniVybrana;
                if (lprow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                string item = lprow.Itemnmbr.Trim();
                string dokl = lprow.SOPNUMBE.Trim();
                using (Informations.OnLineItemumberGrid detailpolozka = new Fask.MST_W.Informations.OnLineItemumberGrid(item, dokl))
                {
                    detailpolozka.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void DetailPocetKusu()
        {
            Vydej_3.ListPolozekVydej.ListPolozekRow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.PolozkaAktualniVybrana;
                string item = lprow.Itemnmbr.Trim();
                string itemdesc = lprow.Nazov.Trim();
                using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, itemdesc))
                {
                    pkusu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

        private void DetailPocetKusuLokace()
        {
            Vydej_3.ListPolozekVydej.ListPolozekRow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.PolozkaAktualniVybrana;
                string item = lprow.Itemnmbr.Trim();
                string lokace = lprow.Lokace.Trim();
                string itemdesc = lprow.Nazov.Trim();
                using (Informations.OnLinePocetKusuSklad pkusu = new Fask.MST_W.Informations.OnLinePocetKusuSklad(item, lokace, itemdesc))
                {
                    pkusu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                this.ScannerStart();
            }
        }

		private void ZmenaPalety(bool automat, bool prvnitisk)
        {
            try
            {
                //this.ScannerStop();

                Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "ZmenaPalety");
                Logging.Trace2.Write("Start", "TypOznaceniPaletyFormOld volany form", tid);

				TiskPaleta(_paleta, automat, prvnitisk);

                //Vyber typu palet
                if (MST_Global.VydejTypOznaceniPalety)
                {
					if (!automat)
						this.ScannerStop();

                    using (TypOznaceniPaletyForm typoznpal = new TypOznaceniPaletyForm())
                    {
						typoznpal.Automat = automat;
                        //typoznpal.TypOznaceni = _paleta;
                        //typoznpal.TypOznaceni = _paleta;
                        if (typoznpal.ShowDialog() == DialogResult.Cancel)
                            return;
                        _paleta = typoznpal.TypOznaceni;
                        //_paleta = typoznpal.TypOznaceni;
                    }
                }
                Logging.Trace2.Write("End", "TypOznaceniPaletyFormOld volany form", tid);
            }
            catch (Exception ex)
            {

                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
				if (!automat)
					this.ScannerStart();
            }
        }

        // 22.8.2016 JiS - rozsireni filtru pro VICHR
        /// <summary>
        /// Vycet moznych filtru pro polozky
        /// </summary>
        public enum FiltrZobrazeniPolozek
        {
            /// <summary>
            /// proste vsechny zaznamy: filtr neni nastaven
            /// </summary>
            Vse,
            /// <summary>
            /// neuplne zadane polozky: Ostava neni 0 
            /// </summary>
            Neuplne,
            /// <summary>
            /// vsechny polozky, ktere dosud nebyly zadany: PocetNasnim je 0
            /// </summary>
            Zbyvajici,
            /// <summary>
            /// vsechny polozky, ktere jiz byly nejak zadany: PocetNasnim neni 0
            /// </summary>
            Zadane
        }
        //private bool filtrVse = true;
        private FiltrZobrazeniPolozek filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Vse;
        private void filtrSwitch()
        {
            switch (filtrZobrazeniPolozek)
            {
                case FiltrZobrazeniPolozek.Vse:
                    filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Neuplne;
                    break;
                case FiltrZobrazeniPolozek.Neuplne:
                    filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Zbyvajici;
                    break;
                case FiltrZobrazeniPolozek.Zbyvajici:
                    filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Zadane;
                    break;
                case FiltrZobrazeniPolozek.Zadane:
                    filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Vse;
                    break;
                default:
                    filtrZobrazeniPolozek = FiltrZobrazeniPolozek.Vse;
                    break;
            }

            UpdateFilter();
        }
        //private void FiltrVseNeuplne()
        //{
        //    filtrVse = !filtrVse;
        //    UpdateFilter();
        //}

        private void UpdateFilter()
        {
            string newRowFilter = string.Empty;

            string fVse = string.Empty;
            switch (filtrZobrazeniPolozek)
            {
                case FiltrZobrazeniPolozek.Neuplne:
                    fVse = "Ostava<>0";
                    break;
                case FiltrZobrazeniPolozek.Zbyvajici:
                    fVse = "PocetNasnim=0";
                    break;
                case FiltrZobrazeniPolozek.Zadane:
                    fVse = "PocetNasnim<>0";
                    break;
                case FiltrZobrazeniPolozek.Vse:
                default:
                    // filtr zustava prazdny ...
                    break;
            }

            string fLokace = string.Empty;
            if (MST_Global.VydejLocationFiltrovatData)
            {
                if (!menuItemLokaceVse.Checked)
                {
                    if (_lokaceID == null || _lokaceID == string.Empty)
                    {
                        fLokace = string.Empty;
                    }
                    else
                    {
                        fLokace = "Lokace='" + _lokaceID + "'";
                    }
                }
            }

            newRowFilter = fVse;
            if (fLokace != string.Empty)
                newRowFilter += (newRowFilter.Length > 0 ? " AND " : string.Empty) + fLokace;

            Cursor.Current = Cursors.WaitCursor;
            pohlad.RowFilter = newRowFilter;
            Cursor.Current = Cursors.Default;

            UpdateForm();
        }

        private void UpdateStatusBar()
        {
            try
            {
                string tStatus = string.Empty;

                //22.8.2016 JiS - zmena zobrazeni listu polozek - VICHR
                //if (filtrZobrazeniPolozek)
                //{
                //    tStatus += "F:V";
                //}
                //else
                //{
                //    tStatus += "F:N";
                //}
                switch (filtrZobrazeniPolozek)
                {
                    case FiltrZobrazeniPolozek.Neuplne:
                        tStatus += "F:N";
                        break;
                    case FiltrZobrazeniPolozek.Zbyvajici:
                        tStatus += "F:Zb";
                        break;
                    case FiltrZobrazeniPolozek.Zadane:
                        tStatus += "F:Za";
                        break;
                    case FiltrZobrazeniPolozek.Vse:
                    default:
                        tStatus += "F:V";
                        break;
                }

                tStatus += " Ø:";
                if (statusInfoRazeni == string.Empty)
                {
                    tStatus += "-";
                }
                else
                {
                    tStatus += statusInfoRazeni;
                }

                if (MST_Global.VydejLocationFiltrovatData)
                {
                    //if (_lokaceID == null || _lokaceID == string.Empty)
                    //{
                    //    tStatus += " L:-";
                    //}
                    //else
                    //{
                    //    tStatus += " L:" + _lokaceID.Trim();
                    //}
                    tStatus += " L";
                    tStatus += (_lokaceID == _lokaceIDPolozka) ? "=" : "!";

                    if (_lokaceNazevPolozka == null || _lokaceNazevPolozka == string.Empty)
                        tStatus += ":-";
                    else
                        tStatus += ":" + _lokaceNazevPolozka;
                }

                /* pro 1csc */
                if (vydejDataParametry != null && vydejDataParametry.Parametry.Count > 0 && vydejDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI)
                {
                    StringBuilder sSumace = new StringBuilder();
                    //tStatus += " s:" + pol_nasnimano + "/" + pol_nasnimat + "(" + nasnimano.ToString(Settings.UIFormatDesCisel) + "/" + nasnimat.ToString(Settings.UIFormatDesCisel) + ")";
                    if (Settings.VydejStatusBarPole1Allow && Settings.VydejStatusBarPole1Value != Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne)
                    {
                        sSumace.Append(Settings.VydejStatusBarPole1Prefix);
                        sSumace.Append(UpdateStatusBarMnozstvi(Settings.VydejStatusBarPole1Value));
                        sSumace.Append(Settings.VydejStatusBarPole1Postfix);
                    }
                    if (Settings.VydejStatusBarPole2Allow && Settings.VydejStatusBarPole2Value != Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne)
                    {
                        sSumace.Append(Settings.VydejStatusBarPole2Prefix);
                        sSumace.Append(UpdateStatusBarMnozstvi(Settings.VydejStatusBarPole2Value));
                        sSumace.Append(Settings.VydejStatusBarPole2Postfix);
                    }
                    if (Settings.VydejStatusBarPole3Allow && Settings.VydejStatusBarPole3Value != Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne)
                    {
                        sSumace.Append(Settings.VydejStatusBarPole3Prefix);
                        sSumace.Append(UpdateStatusBarMnozstvi(Settings.VydejStatusBarPole3Value));
                        sSumace.Append(Settings.VydejStatusBarPole3Postfix);
                    }
                    if (Settings.VydejStatusBarPole4Allow && Settings.VydejStatusBarPole4Value != Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne)
                    {
                        sSumace.Append(Settings.VydejStatusBarPole4Prefix);
                        sSumace.Append(UpdateStatusBarMnozstvi(Settings.VydejStatusBarPole4Value));
                        sSumace.Append(Settings.VydejStatusBarPole4Postfix);
                    }

                    if (sSumace.Length > 0)
                        sSumace.Insert(0, ", ");

                    tStatus += sSumace.ToString();
                }

                if (MST_Global.Vydej_HromadneSN)
                    tStatus += "SN:H";
                else
                    tStatus += "SN:S";

				if (MST_Global.Vydej_HromadneBaliky)
					tStatus += "B:H";
				else
					tStatus += "V:S";

				//Status rezimu zadavani mnozstvi
				//1 - zadava mnozstvi po jednom
				//0- vyzaduje zadavat mnozstvi
				if (MST_Global.Vydej_MnozstviAutoJedna)
					tStatus += ",S:1";
				else
					tStatus += ",S:0";

				if (MST_Global.Vydej_PtatSeNaPamatovani)
					tStatus += ",PP:1";
				else
					tStatus += ",PP:0";

                statusBar.Text = tStatus;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                statusBar.Text = ex.Message;
            }
        }

        private string UpdateStatusBarMnozstvi(Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi typHodnoty)
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "UpdateStatusBarMnozstvi");
            Logging.Trace2.Write("Start", "switch", tid);

            string val = string.Empty;
            switch (typHodnoty)
            {
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.PolozekPozadovano:
                    val = pol_nasnimat.ToString();
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.PolozekNasnimano:
                    val = pol_nasnimano.ToString();
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.PolozekZbyva:
                    val = (pol_nasnimat - pol_nasnimano).ToString();
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.MnozstviPozadovano:
                    val = nasnimat.ToString(Settings.UIFormatDesCisel);
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.MnozstviNasnimano:
                    val = nasnimano.ToString(Settings.UIFormatDesCisel);
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.MnozstviZbyva:
                    val = (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel);
                    break;
                case Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar.VydejStatusBarMnozstvi.Prazdne:
                default:
                    break;

            }

            Logging.Trace2.Write("End", "", tid);

            return val;
        }


        bool zoradene = false;
        private void radenieNazev()
        {
            zoradene = !zoradene;
            if (zoradene)
            {
                statusInfoRazeni = "Název([A-Z])";
                pohlad.Sort = "Nazov ASC";
            }
            else
            {
                statusInfoRazeni = "Název([Z-A])";
                pohlad.Sort = "Nazov DESC";
            }
            //UpdateStatusBar();
            UpdateForm();
        }

        bool zoradeneORD = false;
        private void radeniePodlaORD()
        {
            zoradeneORD = !zoradeneORD;
            if (zoradeneORD)
            {
                statusInfoRazeni = "(Poøadí [0-9])";
                pohlad.Sort = "ORD ASC";
            }
            else
            {
                statusInfoRazeni = "(Poøadí [9-0])";
                pohlad.Sort = "ORD DESC";
            }
            //UpdateStatusBar();
            UpdateForm();
        }

        #endregion

        #region Events


        private void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
			if (e.BarcodeName.Trim() == "ERR")
			{
				MessageBoxBig.Show(e.BarcodeData, "Error", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				return;
			}

            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "Scanner_DataReady");
            Logging.Trace2.Write("Start", "this.BeginInvoke pro NajdiPolozku ", tid);
            
            string EANKod = e.BarcodeData.Trim();
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
            this.BeginInvoke(new DelegateString(NajdiPolozku), new object[] { EANKod });
            
            Logging.Trace2.Write("End", "this.BeginInvoke pro NajdiPolozku ", tid);
        }


        private void ListPolozek_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            RezimZobrazeniUpdate();

            stavVydeje();

            UpdateFilter();
			try
			{

				//_ta_seh.Connection.ConnectionString = "Data source=" + filename;
                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHDataTable seh_dt = Vydej.vydejInstance.globalObject.controller_vydej.GetData_SEH();
				if (seh_dt.Count <= 0)
				{
					//string davka = System.IO.Path.GetFileNameWithoutExtension(filename);
                    string davka = Vydej.vydejInstance.globalObject.Davka;
                    Vydej.vydejInstance.globalObject.controller_vydej.Insert_SEH(int.Parse(davka), Guid.NewGuid());
				}
			}
			catch (System.Exception ex)
			{
				Logging.Log.Write("Chyba pri uprave hlavièek v Load:" + ex.Message);

			}

			ScannerStart();

        }
        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGrid1.UnSelect(selectedIndexRow);
            //}
            //catch { }

            //try
            //{
            //    selectedIndexRow = dataGrid1.CurrentRowIndex;
            //    //for (int i = 0; i < pohlad.Count; i++)
            //    //{
            //    //    dataGrid1.UnSelect(i);
            //    //}
            //    dataGrid1.Select(selectedIndexRow);
            //}
            //catch //(Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "Chyba výbìru!");
            //}

            UpdateForm();
        }

        private void UpdateForm()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Vydej_3.ListPolozekVydej.ListPolozekRow selectedrow = this.PolozkaAktualniVybrana;
                try { df_Itemnmbr.Data = selectedrow.Itemnmbr.Trim(); }
                catch { df_Itemnmbr.Data = "-"; }
                try { df_Baleni.Data = selectedrow.QTYPACK.ToString(Settings.UIFormatDesCisel); }
                catch { df_Baleni.Data = "-"; }
                try { df_Itemdesc.Data = selectedrow.Nazov.Trim(); }
                catch { df_Itemdesc.Data = "-"; }
                try { df_Locncode.Data = selectedrow.Lokace.Trim(); }
                catch { df_Locncode.Data = "-"; }
                try { df_Nasnimano.Data = selectedrow.PocetNasnim.ToString(Settings.UIFormatDesCisel); }
                catch { df_Nasnimano.Data = "-"; }
                try { df_Quantity.Data = selectedrow.Mnozstvo.ToString(Settings.UIFormatDesCisel); }
                catch { df_Quantity.Data = "-"; }
                try { df_Sopnumbe.Data = selectedrow.SOPNUMBE.Trim(); }
                catch { df_Sopnumbe.Data = "-"; }
                try { df_CarKod.Data = selectedrow.CZ_CarKod.Trim(); }
                catch { df_CarKod.Data = "-"; }
                try { df_MJ.Data = selectedrow.MJ.Trim(); }
                catch { df_MJ.Data = "-"; }
                try { df_Note.Data = selectedrow.Note.Trim(); }
                catch { df_Note.Data = "-"; }

                if (MST_Global.VydejLocationPouzitCiselnik)
                {
                    try
                    {
                        LokaceIDPolozka = df_Locncode.Data;
						LokaceNazevPolozka = ((string)Vydej.vydejInstance.globalObject.controller_lokace.CZMST094_GetLocDesc(df_Locncode.Data, _sklad == null ? string.Empty : _sklad.skl_id)) ?? string.Empty;
                        df_Locncode.Data += " : " + LokaceNazevPolozka;
                    }
                    catch
                    {
                        df_Locncode.Data += " : -";
                        LokaceNazevPolozka = "-";
                    }
                }

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            UpdateStatusBar();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //Spoledna metoda volna po stisku enteru nebo ekviv. vyberu z menu
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);

            try
            {
                if (listPolozekVydej.ListPolozek.Count != 0)
                {
                    //DataRowView drv = (dataGrid1.BindingContext[dataGrid1.DataSource].Current) as DataRowView;
                    //Vydej_2.ListPolozekVydej.ListPolozekRow lprow = drv.Row as Vydej_2.ListPolozekVydej.ListPolozekRow;
                    Vydej_3.ListPolozekVydej.ListPolozekRow lprow = this.PolozkaAktualniVybrana;

                    if (lprow == null)
                    {
                        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }

                    Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();
                    //using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
                    //    "Select * from czmst_se where Itemnmbr='" + lprow.Itemnmbr +
                    //    "' and sopnumbe='" + lprow.SOPNUMBE +
                    //    "' and ord=" + lprow.ORD,
                    //    "Data source=" + filename
                    //    ))
                    //{
                    //    sda.Fill(vydejData, vydejData.CZMST_SE.TableName);
                    //}
                    Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByI_SopnumbeItemnmbrOrd(vydejData.CZMST_SE, lprow.SOPNUMBE, lprow.Itemnmbr, lprow.ORD);
                    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = null;
                    PerformInsertData(vydejData, ref SERow);

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
 
        private void PerformInsertData(Fask.SQLiteDBs.DataSets.Vydej vydejData, ref Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow)
        {
            PerformInsertData(vydejData, ref seRow, null, null, null);
        }

		private void PerformInsertData(Fask.SQLiteDBs.DataSets.Vydej vydejData, ref Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow, BaseCode code, VydejService.Vydej_Items_Online.ItemsRow online_item)
		{
			PerformInsertData(vydejData, ref seRow, code, online_item, null);
		}

        /// <summary>
        /// Vlozeni polozky do vystupu a vsechny silene kontroly a doplnovacky
        /// </summary>
        /// <param name="vydejData">SE s listem polozek pro praci, dohledani, vyber ...</param>
        /// <param name="seRow">aktualni vybrany zaznam pro praci, pokud je null tak z vydejData...SE se vybere ruco</param>
        /// <param name="code">naskenovany carovy kod (parsovany)</param>
        /// <param name="online_item">online dotazena polozka pro FEFO/FIFO</param>
        /// <param name="lokaceItem">informace o polozce zvolene online vyberem z lokacniho mechanismu !!!prasarna pro DobrePodlahy >> melo byse resit pres onlineItem</param>
        ///// <param name="SERLTNUM">Sarze vybrana z vyberu lokacniho mechanismu. !!!prasarna pro DobrePodlahy >> melo byse resit pres onlineItem</param>
        ///// <param name="LOCNCODE">Lokace vybrana z vyberu lokacniho mechanismu. !!!prasarna pro DobrePodlahy >> melo byse resit pres onlineItem</param>
        ///// <param name="QTY">Mnozstvi vybrane z vyberu lokacniho mechanismu. !!!prasarna pro DobrePodlahy >> melo byse resit pres onlineItem</param>
        private void PerformInsertData(
			Fask.SQLiteDBs.DataSets.Vydej vydejData, 
			ref Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow seRow, 
			BaseCode code, 
			VydejService.Vydej_Items_Online.ItemsRow online_item,
            //string SERLTNUM,
            //string LOCNCODE,
            //decimal? QTY
            Classes.LokaceItem lokaceItem
			)
        {
            //Kontrola, zda je mozne zadavat i jinak nez scannerem
            if (!InputModeChecker.checkInputMode(MST_Global.VydejPolozkyVyberJenScannerem, _input_mode))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return;
            }

            try
            {
                #region Vyber polozky, pokud nebyla zvolena jinak
                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = seRow;
                if (SERow != null) //byla vybrana polozka
                {
                }
                else if (vydejData.CZMST_SE.Count == 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNenalezena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
                    return;
                }
                else if (vydejData.CZMST_SE.Count == 1)
                {
                    SERow = vydejData.CZMST_SE[0];
                }
                else
                {//nalezeno vice polozek
                    try
                    {
                        this.ScannerStop();

                        vydejData.Parametry.ImportRow(vydejDataParametry.Parametry[0]);

                        #region Hromandne plneni
                        // TODO : revidovat toto "Hromadne plneni" ... 
                        if (
                            MST_Global.VydejHromadneVyplneni &&
                            ((int)vydejData.CZMST_SE.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + vydejData.CZMST_SE[0].ITEMNMBR + "' AND CZ_SerNum_Track=0") == vydejData.CZMST_SE.Count)
                            )
                        {
                            DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NalezenoVicePolozekHromadneVydaniDotaz, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1);
                            if (dr == DialogResult.Cancel)
                                return;
                            else if (dr == DialogResult.Yes)
                            {
                                decimal mnozstvi = 0;
                                //decimal nacteno = Nacteno(vydejData.CZMST_SE[0].ITEMNMBR);
                                //decimal nacist = Nacist(vydejData.CZMST_SE[0].ITEMNMBR); //SETable[0].ITEMNMBR);
                                //Zmena serazeni dle cisla objednavky namisto DEX_ROW_ID
                                Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky =
                                    //(Vydej_2.ListPolozekVydej.ListPolozekRow[])listPolozekVydej.ListPolozek.Select("itemnmbr='" + vydejData.CZMST_SE[0].ITEMNMBR + "'", "DEX_ROW_ID");
                                    (Vydej_3.ListPolozekVydej.ListPolozekRow[])listPolozekVydej.ListPolozek.Select("itemnmbr='" + vydejData.CZMST_SE[0].ITEMNMBR + "'", "SOPNUMBE");

                                do
                                {

                                    Vydej_3.ListPolozekVydej.ListPolozekRow polozka = null;

                                    //if (sejmiMnozstviHromadne(out mnozstvi, nacteno, vydejData.CZMST_SE) != 0)
                                    //    return;
                                    //if (sejmiMnozstviHromadne(out mnozstvi, out polozka, nacteno, nacist, polozky) != 0)
                                    if (sejmiMnozstviHromadne(ref mnozstvi, out polozka, polozky, code) != 0)
                                        return;

                                    //TODO: insert do databaze....
                                    //TODO: testovani navratove hdnoty
                                    //TODO: kontrola preplnenosti...

                                    //if (!InsertDataToSITable(polozky, mnozstvi))
                                    if (!InsertDataToSITable(polozky, mnozstvi, polozka))
                                    {
                                        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3ChybaRozdeleniMnozstviDoPolozek, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                        return;
                                    }
                                    else
                                    {
                                        //nacteno += mnozstvi; //aby se nemuselo znovu hledat, kolik nacteno cyklem ...
                                    }

                                    // TODO : udelat kontrolu jinak ... 
                                    //if (nacteno >= nacist)
                                    //    break; //ukonci vnitrni cyklus, pokud je hotovo...
                                    //if (KontrolaDokoncenosti(ref polozky))
                                    //    break;
                                    bool dokonceno = true;
                                    foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lr in polozky)
                                    {
                                        if (lr.Ostava > 0)
                                        {
                                            dokonceno = false;
                                            break;
                                        }
                                    }
                                    if (dokonceno)
                                        break;

                                } while (true); //dokud je co delat nebo neni stornovano ...

                                //ToDo: tisk etket...
                                //TiskEtiketyNasnimane(mnozstvi, vydejData.CZMST_SE[0], newguid);

                                KontrolaDokoncenosti();

                                return;
                            }
                            //else if (dr == DialogResult.No)
                            //{
                            //    //pujdeme starou cestou...ListMnForm...
                            //}
                        }
                        #endregion

                        // TODO : vyjmout kompletne nasnimane ...
                        //if (!filtrVse && !MST_Global.vydejPovolitPreplneniPolozky) //filtr jen na neuplne
                        if (!MST_Global.vydejPovolitPreplneniPolozky) //filtr jen na neuplne
                        {
                            for (int i = vydejData.CZMST_SE.Rows.Count - 1; i >= 0; i--)
                            {
                                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow serow1 = vydejData.CZMST_SE[i];
                                if (Nacteno(serow1.ITEMNMBR, serow1.SOPNUMBE, serow1.ORD) >= serow1.QTYSHPPD)
                                {
                                    vydejData.CZMST_SE.Rows.Remove(serow1); //odstrani z pohledu data ...
                                }
                            }
                        }
                        using (ListMNForm3 listmn = new ListMNForm3(vydejData, !vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI))
                        {
                            if (listmn.ShowDialog() == DialogResult.Cancel)
                                return;
                            SERow = listmn.ChosenRow;
                        }
                    }
                    finally
                    {
                        this.ScannerStart();
                    }
                }

                seRow = SERow;

                // pokud nenajde v predloze, pokusi se hledat online, pokud je online hledani povoleno a je povoleno online hledani materialu
                //Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow vydej_online_item = null;
                if (online_item == null)
                {
                    var go = Vydej.vydejInstance.globalObject;
                    online_item = Online.Material.Online_Material_Get(seRow.ITEMNMBR.Trim(), go.sklad == null ? null : go.sklad.skl_id, null);
                } 
                #endregion

                bool vlozenoSN = false;
                try
                {
                    this.ScannerStop();

                    if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR) && !string.IsNullOrEmpty(lokaceItem.LOCNCODE))
					{
                        vlozenoSN = vkladaniSN(SERow, OdberatelID, code, online_item, lokaceItem);
					}
					else
					{
						vlozenoSN = vkladaniSN(SERow, OdberatelID, code, online_item);
					}

                    if (vlozenoSN && !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT)
                    {
                        if (MST_Global.VydejTimeDialogNasnimana)
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaUspesnePridana, SERow.ITEMDESC.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3Vlozeni, MessageBoxButtons.OK, MessageBoxBigIcon.Information, Color.Green, MST_Global.VydejTimeDialogInterval);
                        }
                    }
                }
                finally
                {
                    this.ScannerStart();
                }

                // kontrola dokoncenosti - mela by byt provedena az na konci cyklu dalsiMN
                if (vydejDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI && vlozenoSN)
                {
                    if (stavVydeje())
                    {
                        Fask.MST_W.VydejService.ProcessVydejState processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
                        if (!MST_Global.VydejItemTypeQuestion && MST_Global.VydejPokracovatNaJinemTerminalu)
                        {
                            //DialogResult dr = MessageBoxBig.Show(
                            //    "Výdej je kompletní." +
                            //    "\n Pøedloha: " + pol_nasnimat + " / " + nasnimat.ToString(Settings.UIFormatDesCisel) +
                            //    "\n   Zadáno: " + pol_nasnimano + " / " + nasnimano.ToString(Settings.UIFormatDesCisel) +
                            //    "\n    Zbývá: " + (pol_nasnimat - pol_nasnimano) + " / " + (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel) +
                            //    "\nPokraèovat v dalším zpracování pozdìji jiným terminálem?",
                            //    "Odeslání dávky", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
							if (MST_Global.VydejDialogPokracovatNaJinemTerm)
							{
								DialogResult dr = MessageBoxBig.Show(
							string.Format(Fask.Localization.Localization.Vydej3ListPolozek3VydejKompletniInfoPokracovatJinymTermDotaz,
							pol_nasnimat, nasnimat.ToString(Settings.UIFormatDesCisel),
							pol_nasnimano, nasnimano.ToString(Settings.UIFormatDesCisel),
							(pol_nasnimat - pol_nasnimano), (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel)),
							Fask.Localization.Localization.Vydej3ListPolozek3OdeslaniDavky, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
								if (dr == DialogResult.Cancel)
									return;
								else if (dr == DialogResult.No)
									processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
								else if (dr == DialogResult.Yes)
									processState = Fask.MST_W.VydejService.ProcessVydejState.ZpracovatAPokracovat; 
							}
							else
							{
								switch (MST_Global.Vydej_PokracovatJinyTerm_AnoNe)
								{
									case Potvrzeni_AnoNe.Ano:
										processState = Fask.MST_W.VydejService.ProcessVydejState.ZpracovatAPokracovat;
										break;
									case Potvrzeni_AnoNe.Ne:
										processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
										break;
								}
							}

                            odeslatdatadavky(processState);
                        }
                        else
                        {
                            //if (MessageBoxBig.Show(
                            //    "Výdej je kompletní." +
                            //    "\n Pøedloha: " + pol_nasnimat + " / " + nasnimat.ToString(Settings.UIFormatDesCisel) +
                            //    "\n   Zadáno: " + pol_nasnimano + " / " + nasnimano.ToString(Settings.UIFormatDesCisel) +
                            //    "\n    Zbývá: " + (pol_nasnimat - pol_nasnimano) + " / " + (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel) +
                            //    "\nChcete odeslat data?",
                            //    "Odeslání dávky", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                            //    == DialogResult.Yes)
                            if (MessageBoxBig.Show(
                                string.Format(Fask.Localization.Localization.Vydej3ListPolozek3VydejKompletniInfoOdeslatDataDotaz,
                                pol_nasnimat, nasnimat.ToString(Settings.UIFormatDesCisel),
                                pol_nasnimano, nasnimano.ToString(Settings.UIFormatDesCisel),
                                (pol_nasnimat - pol_nasnimano), (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel)),
                                Fask.Localization.Localization.Vydej3ListPolozek3OdeslaniDavky, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                                == DialogResult.Yes)
                            {
                                odeslatdatadavky(processState);
                            }
                            else
                                return;
                        }

                    }
                }
            }
            finally
            {
                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }

        }



        private void KontrolaDokoncenosti()
        {
            // kontrola dokoncenosti - mela by byt provedena az na konci cyklu dalsiMN
            if (vydejDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI)
            {
                if (stavVydeje())
                {
                    Fask.MST_W.VydejService.ProcessVydejState processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
                    if (!MST_Global.VydejItemTypeQuestion && MST_Global.VydejPokracovatNaJinemTerminalu)
                    {
                        //DialogResult dr = MessageBoxBig.Show(
                        //    "Výdej je kompletní." +
                        //    "\n Pøedloha: " + pol_nasnimat + " / " + nasnimat.ToString(Settings.UIFormatDesCisel) +
                        //    "\n   Zadáno: " + pol_nasnimano + " / " + nasnimano.ToString(Settings.UIFormatDesCisel) +
                        //    "\n    Zbývá: " + (pol_nasnimat - pol_nasnimano) + " / " + (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel) +
                        //    "\nPokraèovat v dalším zpracování pozdìji jiným terminálem?",
                        //    "Odeslání dávky", MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
						if (MST_Global.VydejDialogPokracovatNaJinemTerm)
						{
							DialogResult dr = MessageBoxBig.Show(
							 string.Format(Fask.Localization.Localization.Vydej3ListPolozek3VydejKompletniInfoPokracovatJinymTermDotaz,
							 pol_nasnimat, nasnimat.ToString(Settings.UIFormatDesCisel),
							 pol_nasnimano, nasnimano.ToString(Settings.UIFormatDesCisel),
							 (pol_nasnimat - pol_nasnimano), (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel)),
							 Fask.Localization.Localization.Vydej3ListPolozek3OdeslaniDavky, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
							if (dr == DialogResult.Cancel)
								return;
							else if (dr == DialogResult.No)
								processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
							else if (dr == DialogResult.Yes)
								processState = Fask.MST_W.VydejService.ProcessVydejState.ZpracovatAPokracovat;
							
						}
						else
						{
							switch (MST_Global.Vydej_PokracovatJinyTerm_AnoNe)
							{
								case Potvrzeni_AnoNe.Ano:
									processState = Fask.MST_W.VydejService.ProcessVydejState.ZpracovatAPokracovat;
									break;
								case Potvrzeni_AnoNe.Ne:
									processState = Fask.MST_W.VydejService.ProcessVydejState.Zpracovat;
									break;
							}
						}

                        odeslatdatadavky(processState);
                    }
                    else
                    {
                        //if (MessageBoxBig.Show(
                        //    "Výdej je kompletní." +
                        //    "\n Pøedloha: " + pol_nasnimat + " / " + nasnimat.ToString(Settings.UIFormatDesCisel) +
                        //    "\n   Zadáno: " + pol_nasnimano + " / " + nasnimano.ToString(Settings.UIFormatDesCisel) +
                        //    "\n    Zbývá: " + (pol_nasnimat - pol_nasnimano) + " / " + (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel) +
                        //    "\nChcete odeslat data?",
                        //    "Odeslání dávky", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                        //    == DialogResult.Yes)
                        if (MessageBoxBig.Show(
                            string.Format(Fask.Localization.Localization.Vydej3ListPolozek3VydejKompletniInfoOdeslatDataDotaz,
                            pol_nasnimat, nasnimat.ToString(Settings.UIFormatDesCisel),
                            pol_nasnimano, nasnimano.ToString(Settings.UIFormatDesCisel),
                            (pol_nasnimat - pol_nasnimano), (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel)),
                            Fask.Localization.Localization.Vydej3ListPolozek3OdeslaniDavky, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                            == DialogResult.Yes)
                        {
                            odeslatdatadavky(processState);
                        }
                        else
                            return;
                    }

                }
            }
        }

		/// <summary>
		/// Nepouziva se???
		/// </summary>
		/// <param name="polozky"></param>
		/// <param name="mnoz"></param>
		/// <returns></returns>
        private bool InsertDataToSITable(Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky, decimal mnoz)
        {
            //TODO: zadani lokace...
            //if (MST_Global.vydejZadaniLocncodePredSN || !MST_Global.vydejZadaniLocncodePredSN)
            //{
            //    ZadaniLocncode(vydejData.CZMST_SE[0], siRow);
            //}

            //!!! kontrola na preplnenost, pokud je zakazano tak neumoznit preplneni ... 


            decimal mnozstvi = mnoz;

            if (!MST_Global.vydejPovolitPreplneniPolozky)
            {
                if (mnozstvi > Zbyva(polozky[0].Itemnmbr))
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return true;
                }
            }

            foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lr in polozky)
            {
                if (mnozstvi <= 0)
                    break;

                if (lr.Ostava <= 0)
                    continue;

                if (lr.Ostava > mnozstvi)
                { //pridat mnozstvi k polozce

                    lr.Ostava -= mnozstvi;

                    if (!InsertRowToDatabaseSI(lr, mnozstvi))
                        return false;

                    mnozstvi = 0;
                    break;
                }
                else
                { //pridat rozdil a pokracovat
                    decimal mnsub = Math.Min(lr.Ostava, mnozstvi);
                    lr.Ostava -= mnsub;

                    if (!InsertRowToDatabaseSI(lr, mnsub))
                        return false;

                    mnozstvi -= mnsub;
                }
            }

            //todo : co kdyz jsou vsechny naplnene? => vzit posledni a tu preplnit, pokud je povoleno preplneni, jinak rvat ...!!!
            if (mnozstvi > 0)
            { // preplnit posledni polozku, jen pokud neco zbylo na doplneni ...
                polozky[polozky.Length - 1].Ostava -= mnozstvi;
                if (!InsertRowToDatabaseSI(polozky[polozky.Length - 1], mnozstvi))
                    return false;
            }

            return true;
        }

        private bool InsertDataToSITable(Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky, decimal mnoz, Vydej_3.ListPolozekVydej.ListPolozekRow polozka)
        {
            //TODO: zadani lokace...
            //if (MST_Global.vydejZadaniLocncodePredSN || !MST_Global.vydejZadaniLocncodePredSN)
            //{
            //    ZadaniLocncode(vydejData.CZMST_SE[0], siRow);
            //}

            //!!! kontrola na preplnenost, pokud je zakazano tak neumoznit preplneni ... 

            decimal mnozstvi = mnoz;

            if (!MST_Global.vydejPovolitPreplneniPolozky)
            {
                //if (mnozstvi > Zbyva(polozky[0].Itemnmbr))
                if (mnozstvi > polozka.Ostava)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return true;
                }
            }

            //foreach (Vydej_2.ListPolozekVydej.ListPolozekRow lr in polozky)
            //{
            //    if (mnozstvi <= 0)
            //        break;

            //    if (lr.Ostava <= 0)
            //        continue;

            //    if (lr.Ostava > mnozstvi)
            //    { //pridat mnozstvi k polozce

            //        lr.Ostava -= mnozstvi;

            //        if (!InsertRowToDatabaseSI(lr, mnozstvi))
            //            return false;

            //        mnozstvi = 0;
            //        break;
            //    }
            //    else
            //    { //pridat rozdil a pokracovat
            //        decimal mnsub = Math.Min(lr.Ostava, mnozstvi);
            //        lr.Ostava -= mnsub;

            //        if (!InsertRowToDatabaseSI(lr, mnsub))
            //            return false;

            //        mnozstvi -= mnsub;
            //    }
            //}

            ////todo : co kdyz jsou vsechny naplnene? => vzit posledni a tu preplnit, pokud je povoleno preplneni, jinak rvat ...!!!
            //if (mnozstvi > 0)
            //{ // preplnit posledni polozku, jen pokud neco zbylo na doplneni ...
            //    polozky[polozky.Length - 1].Ostava -= mnozstvi;
            //    if (!InsertRowToDatabaseSI(polozky[polozky.Length - 1], mnozstvi))
            //        return false;
            //}

            if (!InsertRowToDatabaseSI(polozka, mnozstvi))
                return false;
            polozka.Ostava -= mnozstvi;

            return true;
        }

        /// <summary>
        /// Vlozi radek do SI
        /// </summary>
        /// <param name="lr"></param>
        /// <param name="mnozstvi"></param>
        /// <returns></returns>
        private bool InsertRowToDatabaseSI(Fask.MST_W.Vydej_3.ListPolozekVydej.ListPolozekRow lr, decimal mnozstvi)
        {
            //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter setaSource = null;
            try
            {
                Guid newguid = Guid.NewGuid();

                //setaSource = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
                //setaSource.Connection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(filename));
                //setaSource.Connection.Open();

                Vydej_3.Vydej.vydejInstance.globalObject.controller_vydej.InsertQuery_SI(
                    lr.CountEntries
                    , lr.SOPNUMBE
                    , lr.Itemnmbr
                    , lr.ORD
                    , lr.VNDDOCNM
                    , lr.IsVNDITNUMNull() ? string.Empty :lr.VNDITNUM
                    , lr.CZ_CarKod
                    , lr.Lokace
                    , mnozstvi
                    , 0
                    , mnozstvi    //qtypack je 0 => QTYSHPPD==QTYSHPPDMJ
                    , ""
                    , "", "", "", this.OdberatelID
                    , DateTime.Now.ToString("yyyyMMdd")
                    , DateTime.Now.ToString("HHmmss")
                    , MST_Global.UserID
                    , lr.DEX_ROW_ID
                    , newguid
                    , ""
                    , ""
                    , false
                    , ""
                    , _input_mode
                    , MST_Global.TerminalID
                    , lr.IsSkladNull() ? string.Empty : lr.Sklad
                    , lr.IsMJNull() ? string.Empty : lr.MJ
                    , lr.IsITEMCODENull() ? string.Empty : lr.ITEMCODE // itemcode ?
					, lr.IsWEIGHTNull() ? (decimal?)null : lr.WEIGHT  // weight???
                    , (DateTime?)null   // TODO : jak tady s expiracemi???
                    );

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                //if (setaSource.Connection.State == ConnectionState.Open)
                //    setaSource.Connection.Close();
                //setaSource.Dispose();
            }
        }

        private void odeslatdatadavky(Fask.MST_W.VydejService.ProcessVydejState processState)
        {

            // TODO : Upravit logiku tisku pred odeslanim 
            TiskPaleta(Paleta, false, true);
            TiskSoupiskaKonecDotaz();

            //OdeslaniDat(processstate);
            odeslatAktualniDavku(processState);
        }

        //public bool OdeslaniDat(Fask.MST_W.VydejService.ProcessVydejState processstate)
        //{
        //    // TODO : revidovat odeslani z listu
        //    //List<string> filenames = Vydej_3.Vydej.vydejInstance.GetSloucenaDavkaListDavek(filename);
        //    List<string> filenames = Vydej_3.Vydej.vydejInstance.GetSloucenaDavkaListDavek(Vydej.vydejInstance.globalObject.DavkaFileNameFullPath);
        //    bool succ = false;
        //    for (int i = 0; i < filenames.Count; i++)
        //    {
        //        Vydej_3.Vydej.vydejInstance.ParseSloucenaDavka(Vydej.vydejInstance.globalObject.DavkaFileNameFullPath, filenames[i]);
        //        succ = Vydej_3.Vydej.vydejInstance.odeslatData(filenames[i], processstate);
        //    }
        //    if (succ)
        //    {
        //        if (Path.GetFileNameWithoutExtension(Vydej.vydejInstance.globalObject.DavkaFileNameFullPath).StartsWith("S"))
        //            File.Delete(Vydej.vydejInstance.globalObject.DavkaFileNameFullPath);
        //        this.finalize();
        //        DialogResult = DialogResult.OK;
        //    }
        //    Logging.Trace2.Write("End", "Odeslani Dat", tid);
        //    return succ;
        //}

        /// <summary>
        /// Odesle aktualni davku
        /// </summary>
        /// <returns>True=davka odeslana a jiz neexistuje, False=davka neodeslana a existuje, pokud je sloucena, mohl byt nejaky problem</returns>
        public bool odeslatAktualniDavku(Fask.MST_W.VydejService.ProcessVydejState processState)
        {
            string aktualniDavakaTmp = Vydej_3.Vydej.vydejInstance.globalObject.Davka;
            string aktualniDavkaFileNameTmp = Vydej_3.Vydej.vydejInstance.globalObject.DavkaFileNameFullPath;
            Vydej_3.Vydej.vydejInstance.globalObject.Davka = null; //uvolneni datoveho souboru davky
            if (Vydej_3.Vydej.vydejInstance.odeslatHotovouDavku(aktualniDavkaFileNameTmp, processState))
            {
                this.finalize();
                DialogResult = DialogResult.OK;
                return true;
            }
            else
            {
                Vydej_3.Vydej.vydejInstance.globalObject.Davka = aktualniDavakaTmp; // zpatky nastavit
                return false;
            }
        }


        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (MST_Global.VydejDialogOpusteniVydejky)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NavratDoMenuDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                {
                    return;
                }
            }

            if (true)
            {

                if (MessageBoxBig.Show(
        string.Format(" Pøedloha: {0} / {1} " + Environment.NewLine + " Zadáno: {2} / {3} " + Environment.NewLine + " Zbývá: {4} / {5} " + Environment.NewLine + " Chcete odeslat data?",
        pol_nasnimat, nasnimat.ToString(Settings.UIFormatDesCisel),
        pol_nasnimano, nasnimano.ToString(Settings.UIFormatDesCisel),
        (pol_nasnimat - pol_nasnimano), (nasnimat - nasnimano).ToString(Settings.UIFormatDesCisel)),
        Fask.Localization.Localization.Vydej3ListPolozek3OdeslaniDavky, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
        == DialogResult.Yes)
                {
                    odeslatdatadavky(Fask.MST_W.VydejService.ProcessVydejState.Zpracovat);

                    this.finalize();
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.finalize();
                    this.DialogResult = DialogResult.Abort;
                }
                //else
                //return;
            }
            else
            {

                //TiskPalListekKonecDotaz();
                TiskSoupiskaKonecDotaz();
                this.finalize();
                this.DialogResult = DialogResult.Cancel;
            }



        }



        private void button_radenie_Click(object sender, EventArgs e)
        {
            radenieNazev();
        }
        private void ListPolozek_KeyDown(object sender, KeyEventArgs e)
        {

            //Handlovani posunu v datagridu ma prednost pred zkratkovymi klavesami ...
            if (e.KeyCode == dataGrid1.KeyScrollUp || e.KeyCode == dataGrid1.KeyScrollDown)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                this.button_ok_Click(null, e);
            }
            else if (e.KeyCode == Keys.Escape)
                this.button_cancel_Click(null, e);
            else if (e.KeyCode == Keys.Back)
                this.menuItemSmazat_Click(null, null);
            else if (e.KeyCode == Keys.D0)
                radenieNazev();
            else if (e.KeyCode == Keys.D1)
            {
                //this.VseNeuplne(zachZobrazenie);
                //this.FiltrVseNeuplne();
                this.filtrSwitch();
            }
            else if (e.KeyCode == Keys.D2)
            {
                this.radeniePodlaORD();
            }
            else if (e.KeyCode == Keys.D3)
            {
                Nasnimane();
            }
            else if (e.KeyCode == Keys.D4)
            {
				//3.2.2021, prehodene D4 a D5

				#region Puvodni
				////if (MST_Global.VydejPovolitOcipovani)
				////    Ocipovat();
				////else 

				////MST_Global.Vydej_HromadneSN = !MST_Global.Vydej_HromadneSN;
				////UpdateStatusBar();   

				//return; 
				#endregion

				MST_Global.Vydej_MnozstviAutoJedna = !MST_Global.Vydej_MnozstviAutoJedna;
				UpdateStatusBar();
                
            }
            else if (e.KeyCode == Keys.D5) // ???
            {
				StavVydaje();
                
            }
            else if (e.KeyCode == Keys.D6)
            {
                RezimZobrazeniSwitch();
            }
            else if (e.KeyCode == Keys.D7)
            {
                originalneRadenie();
            }
            else if (e.KeyCode == Keys.D8)
            {
                if (MST_Global.VydejObjednavkaDetail)
                    DetailPrint();
            }
            else if (e.KeyCode == Keys.D9)
            {
                ZmenaPalety(false, true);
            }
            else if (e.KeyCode == Keys.F1)
            {
                if (MST_Global.VydejPocetKusuOnline)
                    DetailPocetKusu();
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (MST_Global.VydejPocetKusuNaSkladeOnline)
                    DetailPocetKusuLokace();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (MST_Global.VydejPolozkaDetailOnline)
                    DetailItemnumber();
            }
            else if (e.KeyCode == Keys.F4)
            {
                menuItemHledatCarovyKod_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                menuItemHledatNazev_Click(null, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (MST_Global.VydejPovolitZmenuOdberatele)
                    ZmenaOdberatele();
            }
            else if (e.KeyCode == Keys.F7)
            {
                ZmenaLokace();
            }
            else if (e.KeyCode == Keys.F8)
            {
                //zmena zobrazeni lokace vse
                menuItemLokaceVse_Click(null, null);
            }
            else if (e.KeyCode == Keys.F9)
            {
                TiskEtiketyPredloha();
            }
            else if (e.KeyCode == Keys.F10)
            {
				if (MST_Global.F10_ZobrazitAlternativyLokaci)
				{
					if (!vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
					{
						MessageBoxBig.Show("Lokaèní mechanismus není povolen!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					}
					else
					{
						miZobrazitAlternativyLokaci_Click(null, null);
					}
				}
				else if (MST_Global.F10_TiskPalListku)
				{
					miTiskPalListek_Click(null, null);
				}
                //miTiskPalListek_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        private void dataGrid1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGrid1.Select(selectedIndexRow);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Chyba výbìru!");
            //}
        }
        private void ListPolozek_Closing(object sender, CancelEventArgs e)
        {
            finalize();
        }
        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.button_ok_Click(null, e);
        }
        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.button_radenie_Click(null, e);
        }
        private void menuItem4_Click(object sender, EventArgs e)
        {
            this.button_cancel_Click(null, e);
        }
        private void menuItem5_Click(object sender, EventArgs e)
        {
            radeniePodlaORD();
        }
        //private void menuItem6_Click(object sender, EventArgs e)
        //{
        //    //this.VseNeuplne(zachZobrazenie);
        //    //this.FiltrVseNeuplne();
        //    this.filtrSwitch();
        //}
        private void menuItem11_Click(object sender, EventArgs e)
        {
            originalneRadenie();
        }
        private void menuItem12_Click(object sender, EventArgs e)
        {
            DetailPrint();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            radenieNazev();
        }
        private void menuItem17_Click(object sender, EventArgs e)
        {
            radeniePodlaORD();
        }
        private void menuItem18_Click(object sender, EventArgs e)
        {
            originalneRadenie();
        }
        private void menuItem21_Click(object sender, EventArgs e)
        {
            //FiltrVseNeuplne();
            this.filtrSwitch();
        }
        private void menuItem22_Click(object sender, EventArgs e)
        {
            Nasnimane();
        }
        private void menuItem23_Click(object sender, EventArgs e)
        {
            DetailPrint();
        }
        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            StavVydaje();
        }
        private void menuItemDetailItemnumber_Click(object sender, EventArgs e)
        {
            DetailItemnumber();
        }

        private void menuItemKusu_Click(object sender, EventArgs e)
        {
            DetailPocetKusu();
        }

        private void menuItemKusuNaSklade_Click(object sender, EventArgs e)
        {
            DetailPocetKusuLokace();
        }

        private void miPaleta_Click(object sender, EventArgs e)
        {
            ZmenaPalety(false, true);
        }

        private void menuItemHledatCarovyKod_Click(object sender, EventArgs e)
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "menuItemHledatCarovyKod_Click");
            Logging.Trace2.Write("Start", "NajdiPolozku()", tid);
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
            NajdiPolozku();
            Logging.Trace2.Write("End", "NajdiPolozku()", tid);

        }

        private void menuItemHledatNazev_Click(object sender, EventArgs e)
        {
            Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "menuItemHledatNazev_Click");
            Logging.Trace2.Write("Start", "NajdiPolozkuNazev()", tid);
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
            NajdiPolozkuNazev();
            Logging.Trace2.Write("End", "NajdiPolozkuNazev()", tid);
        }

        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            SmazPolozku(this.PolozkaAktualniVybrana);
        }

        private void miTisk_Click(object sender, EventArgs e)
        {
            TiskEtiketyPredloha();
        }

        private void miTiskPalListek_Click(object sender, EventArgs e)
        {
            if (this.PolozkaAktualniVybrana == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NeniVybranaZadnaPolozka, Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            TiskPaleta(Paleta, false, true);

            //TiskEtiketaPalListek(this.PolozkaAktualniVybrana.SOPNUMBE.Trim());
        }

        #endregion

        #region Ridici logika vkladani

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemdesc">Popis položky</param>
        /// <param name="Nacteno">poèet položek kolik už je naèteno</param>
        /// <param name="Predloha">poèet položek kolik je v predlohe</param>
        /// <returns></returns>
        private static List<string> HromadneSN(string itemdesc, decimal Nacteno, decimal Predloha)
        {

            Logging.TracId id = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, "ListPolozek3", "HromadneSN");
            Logging.Trace2.Write("HromadneSN", "Start", id);


            int N = MST_Global.Vydej_HromadneSN_N;

            string Format = string.Empty;

            for (int i = 0; i < N; i++)
            {
                Format += "0";
            }

            string OD;
            string DO;
            int pocet;
            string prefix;
            int odkud;

            using (SejmiSNHromadneForm frm = new SejmiSNHromadneForm())
            {
                frm.LabelNazev = itemdesc;
                frm.Nacteno = Nacteno;
                frm.Predloha = Predloha;
                frm.Zbiva = Predloha - Nacteno;


                if (frm.ShowDialog() == DialogResult.Cancel)
                { return null; }

                OD = frm.Kod_OD;  //SN text
                DO = frm.Kod_DO; // SN text
                pocet = int.Parse(frm.Kod_Pocet); // poèet SN
                prefix = frm.KodPrefix; // prefix
                odkud = frm.KodOd_Start;
            }

            Logging.Trace2.Write("HromadneSN", "Start Vypocet", id);

            List<string> SNList = new List<string>(pocet);

            //Cursor.Current = Cursors.WaitCursor;
            Program.mstw.mbw.BeginPracujiForm("Generuji SN...");

            try
            {
                for (int i = 0; i < pocet; i++)
                {
                    //Program.mstw.mbw.BeginPracujiForm(string.Format("Generuji SN : {0}/{1}", i, pocet));
                    Program.mstw.mbw.Zprava = string.Format("Generuji SN: {0}/{1}", i, pocet);
                    SNList.Add(prefix + odkud.ToString(Format));
                    odkud++;

                }

                Logging.Trace2.Write("HromadneSN", "End Vypocet", id);

                var snt = SNList.Count;

                Program.mstw.mbw.EndPracujiForm();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex);
                return null;
                //throw;
            }

            // Cursor.Current = Cursors.Default;

            return SNList;
        }

        private void VIRowREZ2_SetDefault(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow)
        {
            VIRow.REZ_2 = MST_Global.VydejRozsireni1Rezerva2Zadavat ? MST_Global.VydejRozsireni1Rezerva2Default : string.Empty;
        }

        private bool ZadaniLocncode(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow)
        {
            if (vydejDataParametry.Parametry[0].CONFIG_SNIM_LOCNCODE)
            {
                if (MST_Global.VydejLocationAllowAutocommit)
                {
                    if (_lokaceID.Trim() == VERow.LOCNCODE.Trim())
                    {
                        VIRow.LOCNCODE = _lokaceID;
                        return true;
                    }
                    else
                    {
                        string lokacepolozkanazev = ((string)Vydej.vydejInstance.globalObject.controller_lokace.CZMST094_GetLocDesc(VERow.LOCNCODE, _sklad == null ? string.Empty : _sklad.skl_id)) ?? string.Empty;
                        DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNaJineLokaciPresunoutDotaz, VERow.ITEMDESC.Trim(), lokacepolozkanazev.Trim(), LokaceNazev), Fask.Localization.Localization.Vydej3ListPolozek3Lokace, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            VIRow.LOCNCODE = _lokaceID;
                            return true;
                        }
                        else if (dr == DialogResult.No)
                        {
                            VIRow.LOCNCODE = VERow.LOCNCODE;
                            return true;
                        }
                        else //if (dr == DialogResult.Cancel)
                            return false;
                    }
                }

                if (MST_Global.VydejLocationPouzitCiselnik)
                {
                    using (Forms.FormLokaceVyber flokace = new FormLokaceVyber(_sklad != null ? _sklad.skl_id : string.Empty))
                    {
                        flokace.Owner = this;
                        flokace.LokaceID = VERow.LOCNCODE;
                        if (flokace.ShowDialog() == DialogResult.Cancel)
                            return false;

                        VIRow.LOCNCODE = flokace.Lokace.LOCNCODE.Trim();

                        return true;
                    }
                }
				else if ((!VIRow.IsLOCNCODENull() && (!string.IsNullOrEmpty(VIRow.LOCNCODE))))
				{
					// TaD 3.11.2020 nic... Lokaci už mam tak neøeším...
				}
				else
				{
					sejmiFormLokace.SetDefaultValues();
					sejmiFormLokace.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3ZadejPotvrdLokaci, MST_Global.LC_NAME);    // "Zadej/Potvrï " + MST_Global.LC_NAME;
					sejmiFormLokace.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					sejmiFormLokace.Len = 0;
					//sejmiFormLokace.MaxLength = (int)SqlCEDBs.Columns.Vydej.ColumnsInfo_CZMST_SI["LOCNCODE"].MaxLength; // (new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable()).LOCNCODEColumn.MaxLength;
					sejmiFormLokace.CheckLen = false;
					sejmiFormLokace.AllowEmpty = false;
					sejmiFormLokace.veRow = VERow;
					sejmiFormLokace.viRow = VIRow;
					sejmiFormLokace.Kod = VERow.LOCNCODE;
					sejmiFormLokace.ScannerCheckOnly = vydejDataParametry.Parametry[0].CONFIG_LOCNCODE_OVERIT_SCANEREM;
					// TODO: zobrazit tlacitko lokaci ...
					sejmiFormLokace.btnZobrazitAlternativniLokaceVisible = vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() ? false : vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT;

					if (sejmiFormLokace.ShowDialog() == DialogResult.Cancel)
						return false;

					VIRow.LOCNCODE = sejmiFormLokace.Kod;

					return true;
				}
            }
            else
            {
                VIRow.LOCNCODE = VERow.LOCNCODE;
            }
            return true;
        }

        /// <summary>
        /// Vyzve k vlozeni mnozstvi
        /// </summary>
        /// <param name="mnozstvi">Zde vrati vlozene mnozstvi</param>
        /// <param name="QTYPACK">Mnozstvi v baleni</param>
        /// <returns>-1 pri chybe, 0 pri OK</returns>
        private int sejmiMnozstvi(
            ref decimal mnozstvi,
            decimal Nacteno,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow SESNRow,
            ref string rez_2,
            Parsing.Codes.BaseCode code
            )
        {
				if (MST_Global.VydejRozsireni1Rezerva2Zadavat)
					return sejmiMnozstvi4(ref mnozstvi, Nacteno, VERow, VIRow, SESNRow, ref rez_2, code);
				else
					return sejmiMnozstvi3(ref mnozstvi, Nacteno, VERow, VIRow, SESNRow, code);
        }


        /// <summary>
        /// Vyzve k vlozeni mnozstvi
        /// </summary>
        /// <param name="mnozstvi">Zde vrati vlozene mnozstvi</param>
        /// <param name="QTYPACK">Mnozstvi v baleni</param>
        /// <returns>-1 pri chybe, 0 pri OK</returns>
        private int sejmiMnozstvi3(
            ref decimal mnozstvi,
            decimal Nacteno,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow SESNRow
            ,Parsing.Codes.BaseCode code
            )
        {
            mnozstvi = 0;
            string kodInit = string.Empty;

            if ((code is Parsing.Codes.Interfaces.ICodeQuantity) && (((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue))
            {
                mnozstvi = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
            }
            else
            {
                if (String.IsNullOrEmpty(kodInit) && vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT)
                {
                    if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA)
                        kodInit = 1.ToString(Settings.UIFormatDesCisel);
                    else if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI)
                    {
                        //decimal mn = ((Nacteno - VERow.QTYSHPPD) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
                        decimal mn = ((VERow.QTYSHPPD - Nacteno) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
                        if (mn > 0)
                            kodInit = mn.ToString(Settings.UIFormatDesCisel);
                    }
                }

                //Pokud je pozadovano nezadavani mnozstvi, pak pouze v pripade, ze:
                //1) kodInit != string.Empty
                //2) je povoleno nezadavani mnozstvi
                //3) pouze v pripade, ze se automaticky zadava Modelove cislo, protoze pri automatickem zadani SN by byl nekonecny cyklus

                sejmiForm.SetDefaultValues();
                //sejmiForm.Popis = "Zadej množství" + (VERow.QTYPACK != 0 ? " balení" : string.Empty);
                sejmiForm.Popis = (VERow.QTYPACK != 0 ? Fask.Localization.Localization.Vydej3ListPolozek3ZadejMnozstviBaleni : Fask.Localization.Localization.Vydej3ListPolozek3ZadejMnozstvi);
                sejmiForm.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                sejmiForm.Len = 0;
                sejmiForm.CheckLen = false;
                sejmiForm.AllowEmpty = false;
                sejmiForm.veRow = VERow;
                sejmiForm.viRow = VIRow;
                sejmiForm.sesnRow = SESNRow;
                sejmiForm.Kod = kodInit;
                if (!vydejDataParametry.Parametry[0].IsCONFIG_MNOZSTVI_SCANNEREMNull())
                    sejmiForm.ScannerOff = !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

                if (kodInit != string.Empty && !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && vydejDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
                {
                    //Nezadani mnozstvi a pouziti viz vrchni konfigurace
                }
                else
                {   //Je vyzadovano zadani mnozstvi ...
                    if (sejmiForm.ShowDialog() == DialogResult.Cancel)
                        return -1;
                }
                mnozstvi = Decimal.Parse(sejmiForm.Kod);
            }

            if (VERow.QTYPACK > 0)
                mnozstvi = mnozstvi * VERow.QTYPACK;

            return 0;
        }

        /// <summary>
        /// Vyzve k vlozeni mnozstvi - hromadne
        /// </summary>
        /// <param name="mnozstvi">Zde vrati vlozene mnozstvi</param>
        /// <param name="QTYPACK">Mnozstvi v baleni</param>
        /// <returns>-1 pri chybe, 0 pri OK</returns>
        private int sejmiMnozstviHromadne(
            ref decimal mnozstvi,
            out Vydej_3.ListPolozekVydej.ListPolozekRow polozka,
            //decimal Nacteno,
            //decimal Nacist, 
            //SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable SETable)
            Vydej_3.ListPolozekVydej.ListPolozekRow[] polozky
            , Parsing.Codes.BaseCode code
            )
        {
            // TODO : revidovat ... 
            // mnozstvi = 0;
            polozka = null;

            string kodInit = string.Empty;

            //if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT)
            //{
            //    if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA)
            //        kodInit = 1.ToString(Settings.UIFormatDesCisel);
            //    else if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI)
            //    {
            //        //ToDoS: jak pracovat s qtypack...
            //        //decimal mn = (((decimal)SETable.Compute("Sum(QTYSHPPD)", "") - Nacteno) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
            //        decimal mn = ((Nacist - Nacteno));
            //        if (mn > 0)
            //            kodInit = mn.ToString(Settings.UIFormatDesCisel);
            //    }
            //}

            //Pokud je pozadovano nezadavani mnozstvi, pak pouze v pripade, ze:
            //1) kodInit != string.Empty
            //2) je povoleno nezadavani mnozstvi
            //3) pouze v pripade, ze se automaticky zadava Modelove cislo, protoze pri automatickem zadani SN by byl nekonecny cyklus

            using (SejmiKodHromadneInfoForm zadejMnForm = new SejmiKodHromadneInfoForm(Fask.Localization.Localization.Vydej3ListPolozek3HromadnePlneni, SejmiKodForm.TypeOfCode.AlphaNumeric, null))
            {
                zadejMnForm.SetDefaultValues();
                //ToDoS: jak pracovat s qtypack...
                //zadejMnForm.Popis = "Zadej množství" + (VERow.QTYPACK != 0 ? " balení" : string.Empty);
                zadejMnForm.Popis = Fask.Localization.Localization.Vydej3ListPolozek3ZadejteMnozstvi;
                zadejMnForm.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                zadejMnForm.Len = 0;
                zadejMnForm.CheckLen = false;
                zadejMnForm.AllowEmpty = false;
                //zadejMnForm.SETable = SETable;
                zadejMnForm.polozky = polozky;
                //zadejMnForm.Kod = kodInit; //tento se nastavuje az uvnitr dialogu ...
                //zadejMnForm.NactenoMnozstvi = Nacteno;
                //zadejMnForm.CelkovePozadovaneMnozstvi = Nacist;
                zadejMnForm.VydejParametry = vydejDataParametry;

                if (!vydejDataParametry.Parametry[0].IsCONFIG_MNOZSTVI_SCANNEREMNull())
                    zadejMnForm.ScannerOff = !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

                //if (kodInit != string.Empty && !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && vydejDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
                //{
                //    //Nezadani mnozstvi a pouziti viz vrchni konfigurace
                //}
                //else
                //{   //Je vyzadovano zadani mnozstvi ...
                //    if (zadejMnForm.ShowDialog() == DialogResult.Cancel)
                //        return -1;
                //}

                if (zadejMnForm.ShowDialog() == DialogResult.Cancel)
                    return -1;

                mnozstvi = Decimal.Parse(zadejMnForm.Kod);
                polozka = zadejMnForm.PolozkaAktualniVybrana;
            }

            //ToDoS: jak pracovat s qtypack...
            //if (VERow.QTYPACK > 0)
            //    mnozstvi = mnozstvi * VERow.QTYPACK;

            return 0;
        }

        /// <summary>
        /// Vyzve k vlozeni mnozstvi
        /// </summary>
        /// <param name="mnozstvi">Zde vrati vlozene mnozstvi</param>
        /// <param name="QTYPACK">Mnozstvi v baleni</param>
        /// <returns>-1 pri chybe, 0 pri OK</returns>
        private int sejmiMnozstvi4(
            ref decimal mnozstvi,
            decimal Nacteno,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow,
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow SESNRow,
            ref string rez_2
            , Parsing.Codes.BaseCode code
            )
        {
            //mnozstvi = 0;
            string kodInit = string.Empty;

            if ((code is Parsing.Codes.Interfaces.ICodeQuantity) && (((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue))
            {
                mnozstvi = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
                kodInit = mnozstvi.ToString(Settings.UIFormatDesCisel);
            }
            //else // tady ne, protoze se chce zadavat doplnkova hodnota rez2 => ale pokud je mnozstvi, tak ho predvyplnim
            //{

            if (String.IsNullOrEmpty(kodInit) && vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT)
            {
                if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA)
                    kodInit = 1.ToString(Settings.UIFormatDesCisel);
                else if (vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI)
                {
                    //decimal mn = ((Nacteno - VERow.QTYSHPPD) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
                    decimal mn = ((VERow.QTYSHPPD - Nacteno) / (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1));
                    if (mn > 0)
                        kodInit = mn.ToString(Settings.UIFormatDesCisel);
                }
            }

            //Pokud je pozadovano nezadavani mnozstvi, pak pouze v pripade, ze:
            //1) kodInit != string.Empty
            //2) je povoleno nezadavani mnozstvi
            //3) pouze v pripade, ze se automaticky zadava Modelove cislo, protoze pri automatickem zadani SN by byl nekonecny cyklus

            sejmiForm4.SetDefaultValues();
            //sejmiForm4.Popis = "Zadej množství" + (VERow.QTYPACK != 0 ? " balení" : string.Empty);
            sejmiForm4.Popis = (VERow.QTYPACK != 0 ? Fask.Localization.Localization.Vydej3ListPolozek3ZadejMnozstviBaleni : Fask.Localization.Localization.Vydej3ListPolozek3ZadejMnozstvi);
            sejmiForm4.CodeType = SejmiKodForm.TypeOfCode.Numeric;
            sejmiForm4.Len = 0;
            sejmiForm4.CheckLen = false;
            sejmiForm4.AllowEmpty = false;
            sejmiForm4.veRow = VERow;
            sejmiForm4.viRow = VIRow;
            sejmiForm4.sesnRow = SESNRow;
            sejmiForm4.Kod = kodInit;
            sejmiForm4.REZ_2 = rez_2;
            if (!vydejDataParametry.Parametry[0].IsCONFIG_MNOZSTVI_SCANNEREMNull())
                sejmiForm4.ScannerOff = !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

            if (kodInit != string.Empty && !vydejDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && vydejDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
            {
                //Nezadani mnozstvi a pouziti viz vrchni konfigurace
            }
            else
            {   //Je vyzadovano zadani mnozstvi ...
                if (sejmiForm4.ShowDialog() == DialogResult.Cancel)
                    return -1;
            }
            mnozstvi = Decimal.Parse(sejmiForm4.Kod);

            rez_2 = sejmiForm4.REZ_2;
            //} else tady ne, protoze zadavani rez2

            if (VERow.QTYPACK > 0)
                mnozstvi = mnozstvi * VERow.QTYPACK;

            return 0;
        }

        private void menuItemZmenaOdberatele_Click(object sender, EventArgs e)
        {
            ZmenaOdberatele();
        }

        private void ZmenaOdberatele()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ScannerStop();
                using (ListOdberateliForm3 listOdb = new ListOdberateliForm3())
                {
                    Fask.SQLiteDBs.DataSets.Odberatele odbs = new Fask.SQLiteDBs.DataSets.Odberatele();
                    //SqlCEDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter odbta = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
                    //odbta.Connection.ConnectionString = "Data source=" + Main.CiselnikOdberateleDB;
                    //odbta.Fill(odbs.CZMST090);
                    Vydej.vydejInstance.globalObject.controller_odberatele.Fill(odbs.CZMST090);
                    listOdb.Ciselnik = odbs;

                    Cursor.Current = Cursors.Default;

                    if (listOdb.ShowDialog() == DialogResult.Cancel)
                        return;
                    this.OdberatelID = listOdb.ChosenID;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItemZmenaLokace_Click(object sender, EventArgs e)
        {
            ZmenaLokace();
        }

        private void ZmenaLokace()
        {
            if (!MST_Global.VydejLocationFiltrovatData)
                return;

            try
            {
                ScannerStop();

                if (MST_Global.VydejLocationPouzitCiselnik)
                {
                    using (Forms.FormLokaceVyber flokace = new FormLokaceVyber(_sklad))
                    {
                        flokace.Owner = this;
                        flokace.LokaceID = _lokaceID;
                        if (flokace.ShowDialog() == DialogResult.Cancel)
                            return;

                        LokaceID = flokace.LokaceID;
                        UpdateFilter();
                        //UpdateForm();
                    }
                }
                else
                {
                    using (Forms.SejmiKodForm sejmiForm = new SejmiKodForm())
                    {
                        Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable ldt = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
                        sejmiForm.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3ZadejPotvrdLokaci, MST_Global.LC_NAME); // "Zadej/Potvrï " + MST_Global.LC_NAME;
                        sejmiForm.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                        sejmiForm.Len = 0;
                        //sejmiForm.MaxLength = (int) Fask.SQLiteDBs.Columns.Lokace.ColumnsInfo_CZMST094["LOCNCODE"].MaxLength ; // ldt.LOCNCODEColumn.MaxLength;
                        sejmiForm.CheckLen = false;
                        sejmiForm.AllowEmpty = false;
                        sejmiForm.Kod = string.Empty;

                        if (sejmiForm.ShowDialog() == DialogResult.Cancel)
                            return;

                        LokaceID = sejmiForm.Kod;
                        UpdateFilter();
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

        private void menuItemListDetail_Click(object sender, EventArgs e)
        {
            RezimZobrazeniSwitch();
        }

        private void RezimZobrazeniSwitch()
        {
            switch (_zobrazeni)
            {
                case RezimZobrazeni.Detail:
                    _zobrazeni = RezimZobrazeni.List;
                    break;
                case RezimZobrazeni.List:
                default:
                    _zobrazeni = RezimZobrazeni.Detail;
                    break;
            }

            RezimZobrazeniUpdate();
        }

        private void RezimZobrazeniUpdate()
        {
            switch (_zobrazeni)
            {
                case RezimZobrazeni.Detail:
                    panelDetail.Show();
                    dataGrid1.Hide();
                    panelDetail.Dock = DockStyle.Fill;
                    break;
                case RezimZobrazeni.List:
                default:
                    panelDetail.Hide();
                    dataGrid1.Show();
                    dataGrid1.Dock = DockStyle.Fill;
                    break;
            }
            dataGrid1.Focus();
        }

        private void menuItemLokaceVse_Click(object sender, EventArgs e)
        {
            menuItemLokaceVse.Checked = !menuItemLokaceVse.Checked;
            UpdateFilter();
        }

        private void statusBar_ParentChanged(object sender, EventArgs e)
        {

        }

        private void menuItemZobrazeniStatusBar_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                using (Nastaveni.ListPolozekStatusBar lbstatusbar = new Fask.MST_W.Vydej_3.Nastaveni.ListPolozekStatusBar())
                {
                    lbstatusbar.Owner = this;
                    if (lbstatusbar.ShowDialog() == DialogResult.OK)
                    {
                        this.UpdateStatusBar();
                    }
                }
                this.Show();
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

        ///// <summary>
        ///// Generuje nove baleni - vytvori novy radek predlohy
        ///// </summary>
        ///// <returns>-1 pri chybe, 0 pri OK</returns>
        //private int generBalList(out VydejService.Vydej.CZMST_SERow VERow)
        //{
        //    VERow = vydejData.CZMST_SE.NewCZMST_SERow();
        //    VydejService.Vydej.CZMST_SERow[] VERows = null;
        //    SejmiKodForm sejmiMNForm = new SejmiKodForm(
        //        "Vlož " + MST_Global.MNName,
        //        SejmiKodForm.TypeOfCode.AlphaNumeric);
        //    bool MNok = false;
        //    while (!MNok)
        //    {
        //        if (sejmiMNForm.ShowDialog() == DialogResult.Cancel)
        //            return -1;

        //        string ModelNum = sejmiMNForm.Kod;

        //        //najde ModelNum v predloze
        //        VERows = (VydejService.Vydej.CZMST_SERow[])vydejData.CZMST_SE.Select(
        //            "CZ_CarKod='" + ModelNum + "'", null, DataViewRowState.CurrentRows);

        //        if (VERows.Length == 0)
        //        {
        //            MessageBoxBig.Show("Sejmuté " + MST_Global.MNName + " nenalezeno v pøedloze!");
        //        }
        //        else if (VERows[0].QTYPACK > 0)
        //        {// zadane MN nesmi nalezet jiz existujicimu baleni
        //            MessageBoxBig.Show("Sejmuté " + MST_Global.MNName + " náleží již existujícímu balení!");
        //        }
        //        else
        //        {
        //            MNok = true;
        //        }
        //    }
        //    /* vyhleda prvni nekompletni polozku s danym ITEMNMBR+SOPNUMBE+ORD,
        //       pro niz neni jeste nasnimano vse. Pokud takovou nenajde, vrati
        //       posledni strukturu s klicem MN+SOPNUMBE+ORD, ktera je v predloze */
        //    foreach (VydejService.Vydej.CZMST_SERow row in VERows)
        //    {
        //        if (this.Nacteno(row.ITEMNMBR, row.SOPNUMBE, row.ORD)
        //            < row.QTYSHPPD)
        //        {
        //            VERow.CountEntries = row.CountEntries;
        //            VERow.SOPNUMBE = row.SOPNUMBE;
        //            VERow.ITEMNMBR = row.ITEMNMBR;
        //            VERow.ITEMDESC = row.ITEMDESC;
        //            VERow.VNDDOCNM = row.VNDDOCNM;
        //            VERow.VNDITNUM = row.VNDITNUM;
        //            VERow.ORD = row.ORD;
        //            VERow.CZ_CarKod = row.CZ_CarKod;
        //            VERow.LOCNCODE = row.LOCNCODE;
        //            VERow.QTYSHPPD = row.QTYSHPPD;
        //            VERow.CZ_DatVyr_Delka = row.CZ_DatVyr_Delka;
        //            VERow.CZ_DatVyr_Track = row.CZ_DatVyr_Track;
        //            VERow.CZ_SerNum_Delka = row.CZ_SerNum_Delka;
        //            VERow.CZ_SerNum_Track = row.CZ_SerNum_Track;
        //            VERow.CZ_SW_Delka = row.CZ_SW_Delka;
        //            VERow.CZ_SW_Track = row.CZ_SW_Track;
        //            VERow.CZ_Doslo = 0;
        //            break;
        //        }
        //    }

        //    // zada pocet kusu v baleni
        //    SejmiKodForm sejmiMnozstviForm = new SejmiKodForm(
        //        "Vlož poèet kusù v balení",
        //        SejmiKodForm.TypeOfCode.Numeric);
        //    if (sejmiMnozstviForm.ShowDialog() == DialogResult.Cancel)
        //        return -1;

        //    VERow.QTYPACK = Decimal.Parse(sejmiMnozstviForm.Kod);

        //    // mame pozadovanou strukturu
        //    return 0;
        //}

        #endregion

        #region RFID

        private void RFID_Priradit_Cipy()
        {
            try
            {
                ScannerStop();

                if (!MST_Global.VydejPovolitOcipovani)
                    return;

                //string davka = System.IO.Path.GetFileNameWithoutExtension(filename);
                string davka = Vydej.vydejInstance.globalObject.Davka;

                do
                {

                    try
                    { // zajisti odchyceni vyjimky pri procesu ukladani ...

                        //1) zobrazit seznam zbozi, ktere nema dosud prirazeno cipy
                        // a volba zbozi pro prirazeni
                        RFID.DsRFID.PredlohaRow predlohaRow = null;
                        string ean = string.Empty;
                        using (RFID.VydejRFIDZboziKPrirazeniList form = new RFID.VydejRFIDZboziKPrirazeniList(davka))
                        {
                            if (DialogResult.Cancel == form.ShowDialog())
                                return;
                            predlohaRow = form.SelectedRow;
                            ean = form.EAN;
                        }

                        //2) zadani poctu kusu
                        int pocetkusu = 0;
                        string pocetkusustr = string.Empty;
                        if (DialogResult.Cancel == InputBox.Show("Pocet kusu", predlohaRow.zbyva.ToString(Settings.UIFormatDesCisel), out pocetkusustr))
                            //return;
                            continue; // vrati se na zacatek rfid ...
                        pocetkusu = Convert.ToInt32(Decimal.Parse(pocetkusustr));

                        //3) nacteni cipu
                        Fask.SQLiteDBs.DataSets.Obecne.RFIDDataTable dtRFIDNasnimane = null;
                        using (RFID.SnimatRFID sRFID = new Fask.MST_W.Vydej_3.RFID.SnimatRFID()) //(filename))
                        {
                            sRFID.A_OmezitPocetNactenychZaznamu = true;
                            sRFID.A_PocetZaznamu = pocetkusu;
                            if (DialogResult.Cancel == sRFID.ShowDialog())
                                //return;
                                continue; // vrati se na zacatek rfid ...
                            dtRFIDNasnimane = sRFID.A_DS_Nasnimane.RFID;
                        }

                        //4) priprava dat pro zapis ...
                        List<int> sequenceNumbers = new List<int>();
                        DialogResult dResSeq = DialogResult.None;
                        while (true)
                        {
                            try
                            {
                                int[] sequenceInts = Vydej.vydejInstance.globalObject.service_rfid.GetNextSerial(predlohaRow.ITEMNMBR, string.Empty, pocetkusu);
                                sequenceNumbers.AddRange(sequenceInts);
                                break;
                            }
                            catch (Exception exSequence)
                            {
                                Logging.Log.Write(exSequence);
                                dResSeq = MessageBoxBig.Show("Pøiøazení èísel sekvence se nezdaøilo.\nOpkovat?", "RFID sekvence", MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                                if (DialogResult.Cancel == dResSeq)
                                {
                                    //return;
                                    break; // vrati se na zacatek rfid ...
                                }
                            }
                        }
                        if (dResSeq == DialogResult.Cancel)
                            continue; // vrati se na zacatek rfid ...

                        // pripravit pro kazdy nacteny tag nova data k zapisu ...
                        Fask.SQLiteDBs.DataSets.Vydej dsVydej = new Fask.SQLiteDBs.DataSets.Vydej();
                        Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow rfn = null;
                        int sqIndex = 0;

                        foreach (var item in dtRFIDNasnimane)
                        {

                            rfn = dsVydej.CZMST_SI_RFID.NewCZMST_SI_RFIDRow();
                            rfn.CountEntries = predlohaRow.CountEntries;
                            rfn.DOCUMENTNMBR = predlohaRow.SOPNUMBE.Trim();
                            rfn.ITEMNMBR = predlohaRow.ITEMNMBR.Trim();
                            rfn.ITEMDESC = predlohaRow.ITEMDESC.Trim();
                            rfn.SERLNMBR = string.Empty; //prirazuje se pozdeji ...???
                            rfn.ORD = predlohaRow.ORD;
                            rfn.SKL_ID = _sklad == null ? string.Empty : _sklad.skl_id.Trim();

                            rfn.M_ID = item.ID.Trim();
                            rfn.M_TID = item.IsTIDNull() ? string.Empty : item.TID.Trim();
                            rfn.M_EPC = item.IsEPCNull() ? string.Empty : item.EPC.Trim();
                            rfn.M_USER = item.IsUSERNull() ? string.Empty : item.USER.Trim();
                            rfn.M_RESERVED = item.IsRESERVEDNull() ? string.Empty : item.RESERVED.Trim();

                            rfn.O_M_ID = rfn.M_ID;
                            rfn.O_M_TID = rfn.M_TID;
                            rfn.O_M_EPC = rfn.M_EPC;
                            rfn.O_M_USER = rfn.M_USER;
                            rfn.O_M_RESERVED = rfn.M_RESERVED;

                            rfn.SEQUENCENMBR = sequenceNumbers[sqIndex++];

                            rfn.Created_T = DateTime.Now;
                            rfn.guid = Guid.NewGuid();
                            rfn.TerminalID = MST_Global.TerminalID;
                            rfn.UserID = MST_Global.UserID;

                            dsVydej.CZMST_SI_RFID.AddCZMST_SI_RFIDRow(rfn);
                            dsVydej.AcceptChanges();
                        }

                        //5) zapsani informaci do cipu
                        Fask.SQLiteDBs.DataSets.Vydej dsZapsane = null;
                        using (RFID.ZapisRFID zRFID = new RFID.ZapisRFID()) //(this.filename))
                        {
                            zRFID.A_DS_Zapsat = dsVydej;
                            zRFID.A_EAN = ean;
                            if (DialogResult.Cancel == zRFID.ShowDialog())
                                //return;
                                continue; // vrati se na zacatek rfid ...


                            dsZapsane = zRFID.A_DS_Zapsane;
                        }

                        //6) Ulozit zapsane tagy ... 
                        Vydej.vydejInstance.globalObject.controller_vydej.Update_SI_RFID(dsZapsane.CZMST_SI_RFID);

                        Fask.MST_W.Forms.MessageBoxBig.Show("RFID zapsáno celkem " + dsZapsane.CZMST_SI_RFID.Count + " tagù", "Výdej RFID", MessageBoxButtons.OK);

                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    }
                } while (true);
                //6) konec 
                //return;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }


        #endregion

        private void ListPolozek3_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ListPolozek3_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void miRFIDPriradiCipy_Click(object sender, EventArgs e)
        {
            RFID_Priradit_Cipy();
        }


        private void miOcipovat(object sender, EventArgs e)
        {

        }

        private void miZobrazitAlternativyLokaci_Click(object sender, EventArgs e)
        {
            if (PolozkaAktualniVybrana == null)
                return;

            if (!vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
                return;

            try
            {
                ScannerStop();

				//using (VydejZobrazeniLokaciList lokacelist = new VydejZobrazeniLokaciList(PolozkaAktualniVybrana.Itemnmbr, string.Empty, _sklad != null ? _sklad.skl_id : string.Empty))
				//{
				//    lokacelist.ShowDialog();
				//}
				using (Alter_LokaciList lokacelist = new Alter_LokaciList())
				{
					lokacelist.ITEMNMBR = PolozkaAktualniVybrana.Itemnmbr;
					lokacelist.SKL_ID = PolozkaAktualniVybrana.Sklad;
					lokacelist.QTY = PolozkaAktualniVybrana.Ostava;
					DialogResult dr = lokacelist.ShowDialog();

					if (dr == DialogResult.OK)
					{
						//NajdiPolozku(lokacelist.ITEMNMBR, lokacelist.SERLTNUM, lokacelist.LOCNCODE, lokacelist.QTY);
                        NajdiPolozku(new LokaceItem()
                        {
                            ITEMNMBR = lokacelist.Radek.ITEMNMBR,
                            SERLNMBR = lokacelist.Radek.SERLTNUM,
                            LOCNCODE = lokacelist.Radek.LOCNCODE,
                            QTY = lokacelist.Radek.QTYSHPPD
                        });
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

        #region Tisk

        public void TiskPaleta(Paleta paleta, bool automat, bool prvnitisk)
        {
            try
            {

				if(!automat)
					ScannerStop();


                if (!MST_Global.PovolitPrintServer)
                    return;

				if (!Settings.Vydej_Baleni_Tisk_Enable)
					return;


				if (!automat)
				{
					DialogResult dr = MessageBoxBig.Show("Tisknout Potisk Baleni?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
					if (dr == DialogResult.No)
					{
						return;
					}
					
				}

                //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter ta_si = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
                //ta_si.Connection.ConnectionString = "Data source=" + filename;
                //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                //ta_se.Connection.ConnectionString = "Data source=" + filename;

                //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SI_SN_TableAdapter ta_seSN = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SN_TableAdapter();
                //ta_seSN.Connection.ConnectionString = "Data source=" + filename;
                //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SI_TiskTableAdapter ta_seSoupiska = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_TiskTableAdapter();
                //ta_seSoupiska.Connection.ConnectionString = "Data source=" + filename;

                bool vytisteno = false;

                //  tisk soupisu
                Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
                List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
                Dictionary<string, string> dataPaticka = new Dictionary<string, string>();


				//Gurpovani pro variantu I-Tec

				//SqlCEDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dtsi = ta_seSoupiska.GetDataByNMBRPAL(paleta.sccc);
				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dtsi = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByNMBRPAL_SI_Tisk(paleta.sscc);

				if (dtsi == null || dtsi.Count <= 0)
				{
					DialogResult dr = MessageBoxBig.Show("Žádná data pro tisk Baleni!", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}

                dataHlavicka.Add("00", paleta.sscc.Trim());

              
				bool _ptatSeNaHodnoty = true;

				if (MST_Global.Vydej_PtatSeNaPamatovani && !automat)
				{
					DialogResult dr = MessageBoxBig.Show("Vytisknout stejné rozmìry?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

					if (dr == DialogResult.Yes)
						_ptatSeNaHodnoty = false;
					else
						_ptatSeNaHodnoty = true;

				}



				if (_ptatSeNaHodnoty)
				{

					// JiS : doplneni rozmeru a vahy palety (baliku) do hlavicky k tisku ... 
					#region Rozmery baliku jako text prozatim...

					#region Sirka
					string dimensionSirka = Settings.Vydej_dimensionSirka;
					if (prvnitisk)
					{
						do
						{
							try
							{
								using (SejmiKodForm kod = new SejmiKodForm("Zadejte šíøku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionSirka, false))
								{
									if (kod.ShowDialog() == DialogResult.Cancel)
										return;
									dimensionSirka = kod.Kod;
								}

								break;
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(ex);
								continue;
							}

						} while (true);
					}

					Settings.Vydej_dimensionSirka = dimensionSirka;
					dataHlavicka.Add("DimensionWidth", dimensionSirka);
					#endregion

					#region Vyska
					string dimensionVyska = Settings.Vydej_dimensionVyska;
					if (prvnitisk)
					{
						do
						{
							try
							{
								using (SejmiKodForm kod = new SejmiKodForm("Zadejte výšku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionVyska, false))
								{
									if (kod.ShowDialog() == DialogResult.Cancel)
										return;
									dimensionVyska = kod.Kod;
								}

								break;
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(ex);
								continue;
							}

						} while (true);
					}

					Settings.Vydej_dimensionVyska = dimensionVyska;
					dataHlavicka.Add("DimensionHeight", dimensionVyska);
					#endregion

					#region Hloubka
					string dimensionHloubka = Settings.Vydej_dimensionHloubka;
					if (prvnitisk)
					{
						do
						{
							try
							{
								using (SejmiKodForm kod = new SejmiKodForm("Zadejte hloubku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionHloubka, false))
								{
									if (kod.ShowDialog() == DialogResult.Cancel)
										return;
									dimensionHloubka = kod.Kod;
								}

								break;
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(ex);
								continue;
							}

						} while (true);
					}

					Settings.Vydej_dimensionHloubka = dimensionHloubka;
					dataHlavicka.Add("DimensionDepth", dimensionHloubka);
					#endregion

					#endregion

					#region Brutto Vaha (Gross weight)

					string gross_weight = Settings.Vydej_gross_weight;
					decimal gross_weight_decimal = 0;

					if (prvnitisk)
					{
						do
						{
							try
							{

								using (SejmiKodForm kod = new SejmiKodForm("Zadej Brutto váhu", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, gross_weight, false))
								{
									if (kod.ShowDialog() == DialogResult.Cancel)
										return;

									gross_weight = kod.Kod;
								}

								break;
							}
							catch (Exception ex)
							{
								Logging.ExceptionHandler2.Handle(ex);
								continue;
							}

						} while (true);
					}


					gross_weight_decimal = decimal.Parse(gross_weight);
					Settings.Vydej_gross_weight = gross_weight.Trim();
					dataHlavicka.Add("GrossWeight", gross_weight_decimal.ToString("0.000", System.Globalization.NumberFormatInfo.InvariantInfo));

					#endregion

				}
				else
				{
					dataHlavicka.Add("DimensionWidth", Settings.Vydej_dimensionSirka);
					dataHlavicka.Add("DimensionHeight", Settings.Vydej_dimensionVyska);
					dataHlavicka.Add("DimensionDepth", Settings.Vydej_dimensionHloubka);
					dataHlavicka.Add("GrossWeight", Settings.Vydej_gross_weight);

				}


				#region Ulozeni Rozmeru

				Vydej.vydejInstance.globalObject.controller_vydej.Save_SI_BV(
					dtsi[0].CountEntries,
					paleta.sscc,
					MST_Global.UserID,
					decimal.Parse(Settings.Vydej_dimensionSirka),
					decimal.Parse(Settings.Vydej_dimensionVyska),
					decimal.Parse(Settings.Vydej_dimensionHloubka),
					decimal.Parse(Settings.Vydej_gross_weight)
					);



				#endregion

                #region Naplneni hlavicka

                // TODO : konfiguraène kvuli nekupto...
                if (false)
                {
                    //vytahne cislo objednavky z 1. zaznamu
                    // TODO : co kdyz jich je vic????

                    DataSet ds = Vydej.vydejInstance.globalObject.service_vydej.Detail(dtsi[0].SOPNUMBE.Trim());

                    // Dictionary<string, string> data = new Dictionary<string, string>();
                    //globalni parametry
                    dataHlavicka.Add("UserID", MST_Global.UserID.ToString());
                    dataHlavicka.Add("UserLoginName", MST_Global.UserLoginName);
                    //vracene hodnoty z online
                    foreach (DataColumn dcol in ds.Tables[0].Columns)
                    {
                        string key = dcol.ColumnName;
                        string value = ds.Tables[0].Rows[0][dcol].ToString();
                        if (!dataHlavicka.ContainsKey(key))
                            dataHlavicka.Add(key, value);
                    }
                }

                #endregion


                #region SOPNUMBE èisla objednavek v hlavièke

                List<IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow>> pole = dtsi.GroupBy(x => x.SOPNUMBE.Trim()).Select(x => x).ToList();

                List<string> SOPNUMBESeznam = new List<string>();

                foreach (IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow> item in pole)
                {
                    SOPNUMBESeznam.Add(item.Key.Trim());
                }

                dataHlavicka.Add("SOPNUMBEList", String.Join(", ", SOPNUMBESeznam.ToArray()));

                #endregion

                //if (SOPNUMBESeznam.Count == 1)
                //{
                    #region Adresa
                    //Konfiguraène možnost dotahovat adresu online pomoci detailu

                    string Firma = string.Empty;
                    string Utvar = string.Empty;
                    string Jmeno = string.Empty;
                    string Ulice = string.Empty;
                    string PSC = string.Empty;
                    string Obec = string.Empty;
                    string ICO = string.Empty;
                    string DIC = string.Empty;

                    DataSet data = GetAdresa(SOPNUMBESeznam[0]);

                    if (data.Tables.Count == 0 || (data.Tables.Count > 0 && data.Tables[0].Rows.Count == 0))
                    {
                        //e.Graphics.DrawString("Žádná data k dispozici", fnt, solid, x, y);
                        Logging.Log.Write("Server nevratil žadnou adresu");
                    }
                    else
                    {
                        DataTable dtAdresa = data.Tables[0];

                        if (dtAdresa.Rows.Count == 1)
                        {
                            DataRow dwAdresa = dtAdresa.Rows[0];

                            string Row_Firma = dwAdresa["Firma"] is string ? (string)dwAdresa["Firma"] : null;
                            string Row_Utvar = dwAdresa["Utvar"] is string ? (string)dwAdresa["Utvar"] : null;
                            string Row_Jmeno = dwAdresa["Jmeno"] is string ? (string)dwAdresa["Jmeno"] : null;
                            string Row_Ulice = dwAdresa["Ulice"] is string ? (string)dwAdresa["Ulice"] : null;
                            string Row_PSC = dwAdresa["PSC"] is string ? (string)dwAdresa["PSC"] : null;
                            string Row_Obec = dwAdresa["Obec"] is string ? (string)dwAdresa["Obec"] : null;

                            string Row_Firma2 = dwAdresa["Firma2"] is string ? (string)dwAdresa["Firma2"] : null;
                            string Row_Utvar2 = dwAdresa["Utvar2"] is string ? (string)dwAdresa["Utvar2"] : null;
                            string Row_Jmeno2 = dwAdresa["Jmeno2"] is string ? (string)dwAdresa["Jmeno2"] : null;
                            string Row_Ulice2 = dwAdresa["Ulice2"] is string ? (string)dwAdresa["Ulice2"] : null;
                            string Row_PSC2 = dwAdresa["PSC2"] is string ? (string)dwAdresa["PSC2"] : null;
                            string Row_Obec2 = dwAdresa["Obec2"] is string ? (string)dwAdresa["Obec2"] : null;


                            string Row_ICO = dwAdresa["ICO"] is string ? (string)dwAdresa["ICO"] : null;
                            string Row_DIC = dwAdresa["DIC"] is string ? (string)dwAdresa["DIC"] : null;


                            #region Rozpad

                            // TODO TaD dodelat logiku rozpadu na jednotlive + konfigurace

                            //if (false)
                            //{
                            //    try
                            //    {
                            //        if (!Row.IsFirma2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Firma2.Trim()))
                            //                Firma = "-";
                            //            else
                            //                Firma = Row.Firma2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsFirmaNull())
                            //                Firma = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Firma.Trim()))
                            //                    Firma = "-";
                            //                else
                            //                    Firma = Row.Firma.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Firma = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsUtvar2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Utvar2.Trim()))
                            //                Utvar = "-";
                            //            else
                            //                Utvar = Row.Utvar2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsUtvarNull())
                            //                Utvar = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Utvar.Trim()))
                            //                    Utvar = "-";
                            //                else
                            //                    Utvar = Row.Utvar.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Utvar = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsJmeno2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Jmeno2.Trim()))
                            //                Jmeno = "-";
                            //            else
                            //                Jmeno = Row.Jmeno2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsJmenoNull())
                            //                Jmeno = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Jmeno.Trim()))
                            //                    Jmeno = "-";
                            //                else
                            //                    Jmeno = Row.Jmeno.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Jmeno = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsUlice2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Ulice2.Trim()))
                            //                Ulice = "-";
                            //            else
                            //                Ulice = Row.Ulice2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsUliceNull())
                            //                Ulice = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Ulice.Trim()))
                            //                    Ulice = "-";
                            //                else
                            //                    Ulice = Row.Ulice.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Ulice = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsPSC2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.PSC2.Trim()))
                            //                PSC = "-";
                            //            else
                            //                PSC = Row.PSC2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsPSCNull())
                            //                PSC = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.PSC.Trim()))
                            //                    PSC = "-";
                            //                else
                            //                    PSC = Row.PSC.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        PSC = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsObec2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Obec2.Trim()))
                            //                Obec = "-";
                            //            else
                            //                Obec = Row.Obec2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsObecNull())
                            //                Obec = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Obec.Trim()))
                            //                    Obec = "-";
                            //                else
                            //                    Obec = Row.Obec.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Obec = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //}
                            //else
                            //{
                            #endregion

                            if (
                                !string.IsNullOrEmpty(Row_Firma2) ||
                                !string.IsNullOrEmpty(Row_Utvar2) ||
                                !string.IsNullOrEmpty(Row_Jmeno2) ||
                                !string.IsNullOrEmpty(Row_Ulice2) ||
                                !string.IsNullOrEmpty(Row_PSC2) ||
                                !string.IsNullOrEmpty(Row_Obec2)
                                )
                            {

                                Firma = string.IsNullOrEmpty(Row_Firma2) ? "-" : Row_Firma2.Trim();
                                Utvar = string.IsNullOrEmpty(Row_Utvar2) ? "-" : Row_Utvar2.Trim();
                                Jmeno = string.IsNullOrEmpty(Row_Jmeno2) ? "-" : Row_Jmeno2.Trim();
                                Ulice = string.IsNullOrEmpty(Row_Ulice2) ? "-" : Row_Ulice2.Trim();
                                PSC = string.IsNullOrEmpty(Row_PSC2) ? "-" : Row_PSC2.Trim();
                                Obec = string.IsNullOrEmpty(Row_Obec2) ? "-" : Row_Obec2.Trim();
                            }
                            else
                            {
                                Firma = string.IsNullOrEmpty(Row_Firma) ? "-" : Row_Firma.Trim();
                                Utvar = string.IsNullOrEmpty(Row_Utvar) ? "-" : Row_Utvar.Trim();
                                Jmeno = string.IsNullOrEmpty(Row_Jmeno) ? "-" : Row_Jmeno.Trim();
                                Ulice = string.IsNullOrEmpty(Row_Ulice) ? "-" : Row_Ulice.Trim();
                                PSC = string.IsNullOrEmpty(Row_PSC) ? "-" : Row_PSC.Trim();
                                Obec = string.IsNullOrEmpty(Row_Obec) ? "-" : Row_Obec.Trim();
                            }
                        }

                        //}
                    }

                    dataHlavicka.Add("Firma", Firma.Trim());
                    dataHlavicka.Add("Utvar", Utvar.Trim());
                    dataHlavicka.Add("Jmeno", Jmeno.Trim());
                    dataHlavicka.Add("Ulice", Ulice.Trim());
                    dataHlavicka.Add("PSC", PSC.Trim());
                    dataHlavicka.Add("Obec", Obec.Trim());
                    dataHlavicka.Add("ICO", ICO.Trim());
                    dataHlavicka.Add("DIC", DIC.Trim());
                    #endregion // end adresa
                //}

                #region VNDDOCNM èisla objednavek v hlavièke

                List<IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow>> VNDDOCNMGroup = dtsi.GroupBy(x => x.VNDDOCNM.Trim()).Select(x => x).ToList();

                List<string> VNDDOCNMSeznam = new List<string>();

                foreach (IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow> item in VNDDOCNMGroup)
                {
                    VNDDOCNMSeznam.Add(item.Key.Trim());
                }

                dataHlavicka.Add("VNDDOCNMList", String.Join(", ", VNDDOCNMSeznam.ToArray()));

                #endregion

                #region Suma kolko polozek je v Baliku
                decimal qty = dtsi.Sum(x => x.QTYSHPPD);

                dataHlavicka.Add("POCET", qty.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                #endregion

                #region Foreach Predloha

                foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow drsi in dtsi)
                {
                    //++cisloZaznamu;                    
                    //Logging.Log.WriteDebug("Zacatek zpracovani zaznamu c. " + cisloZaznamu);

                    string SERLTNUMSeznam = string.Empty;
                    string LLSN = string.Empty;
                    //string FontSizeSN_HEIGHT = string.Empty;
                    //string FontSizeSN_WIDTH = string.Empty;

                    Dictionary<string, string> dataRadek = new Dictionary<string, string>();
                    List<string> listSN = new List<string>();
                    Dictionary<int, string> RadkySN = new Dictionary<int, string>();
                    int CountRow = 0;
                    string tmpSN = string.Empty;

                    //var dtSN = ta_seSN.GetDataSERLTNUM(drsi.ITEMNMBR, drsi.CountEntries, drsi.NMBRPAL);
					var dtSN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataSERLTNUM_SI_SN(drsi.SOPNUMBE, drsi.ITEMNMBR, drsi.CountEntries, drsi.NMBRPAL, drsi.ORD);

                    if ((dtSN != null) && (dtSN.Count > 0))
                    {

                        foreach (var item in dtSN)
                        {
                            if (!string.IsNullOrEmpty(item.SERLTNUM.Trim()))
                                listSN.Add(item.SERLTNUM);
                        }

                        if (listSN.Count == 0)
                        {
                            tmpSN = string.Empty;

                        }
                        else
                        {
                            string result = String.Join(", ", listSN.ToArray());

                            do
                            {
                                if (result.Length > Settings.Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek)
                                {
                                    string tmpsnrow = "^FD";
                                    tmpsnrow += result.Substring(0, Settings.Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek);
                                    tmpsnrow += "^FS";
                                    RadkySN.Add(CountRow++, tmpsnrow);
                                    result = result.Substring(Settings.Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek);
                                }
                                else
                                {
                                    string tmpsnrow = "^FD";
                                    tmpsnrow += result;
                                    tmpsnrow += "^FS";
                                    RadkySN.Add(CountRow++, tmpsnrow);
                                    result = string.Empty;

                                }

                            } while (result.Length != 0);

                            int velkost = (int)Math.Ceiling((Settings.Vydej_Baleni_Tisk_Font_Size_Height * (double)CountRow));
                            LLSN = (velkost + Settings.Vydej_Baleni_Tisk_Font_Size_Medzera).ToString();

                            #region Skladani èasti SN

                            tmpSN += "^XA";
                            tmpSN += "^DFR:SN.ZPL";
                            tmpSN += "^POI";
                            tmpSN += "^XB";
                            tmpSN += "^CI31";
                            tmpSN += "^LL" + LLSN;
                            tmpSN += "^FO25,0";
                            tmpSN += "^A0N," + Settings.Vydej_Baleni_Tisk_Font_Size_Height + "," + Settings.Vydej_Baleni_Tisk_Font_Size_Width;
                            tmpSN += "^FD" + "Výr.è.:";
                            tmpSN += "^FS";

                            int FOY = 0;

                            foreach (var SNRow in RadkySN)
                            {

                                tmpSN += "^FO125," + FOY.ToString();
                                FOY = FOY + Settings.Vydej_Baleni_Tisk_Font_Size_Height;
                                tmpSN += "^A0N," + Settings.Vydej_Baleni_Tisk_Font_Size_Height + "," + Settings.Vydej_Baleni_Tisk_Font_Size_Width;
                                tmpSN += "^TBN," + Settings.Vydej_Baleni_Tisk_Font_Size_SirkaStitku.ToString() + "," + Settings.Vydej_Baleni_Tisk_Font_Size_Height;
                                tmpSN += "^FN" + SNRow.Key.ToString();
                                tmpSN += "^FS";
                            }

                            tmpSN += "^XZ";


                            tmpSN += "^XA";
                            tmpSN += "^XFR:SN.ZPL";
                            tmpSN += "^PN0";

                            foreach (var SNRow2 in RadkySN)
                            {
                                tmpSN += "^FN" + SNRow2.Key.ToString();
                                tmpSN += "^FD";
                                tmpSN += SNRow2.Value.Trim();
                                tmpSN += "^FS";
                            }

                            tmpSN += "^XZ";

                            #endregion

                        }
                    }
                    else
                    {
                        tmpSN = string.Empty;
                    }

                    dataRadek.Add("SNPart", tmpSN);

                    #region Info o polozce

                    DataSet dataitem = GetPolozka(drsi.ITEMNMBR.Trim());

                    if (dataitem.Tables.Count == 0 || (dataitem.Tables.Count > 0 && dataitem.Tables[0].Rows.Count == 0))
                    {
                        //e.Graphics.DrawString("Žádná data k dispozici", fnt, solid, x, y);
                        Logging.Log.Write("Server nevratil žadnou informaci o polozce :" + drsi.ITEMNMBR.Trim());
                    }
                    else
                    {
                        DataTable dtPolozka = dataitem.Tables[0];

                        if (dtPolozka.Rows.Count == 1)
                        {
                            DataRow dwPolozka = dtPolozka.Rows[0];

                            string Doprava = dwPolozka["Doprava"] is string ? (string)dwPolozka["Doprava"] : null;
                            dataRadek.Add("Doprava", string.IsNullOrEmpty(Doprava) ? string.Empty : Doprava.Trim());
                        }
                    }

                    #endregion

					string itemdesc = Vydej.vydejInstance.globalObject.controller_vydej.Get_ITEMDESC_SE(drsi.SOPNUMBE.Trim(), drsi.ITEMNMBR.Trim());

                    dataRadek.Add("ITEMDESC", itemdesc);

                    dataRadek.Add("CountEntries", drsi.CountEntries.ToString());
                    dataRadek.Add("SOPNUMBE", drsi.SOPNUMBE.Trim());
                    dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
                    //dataRadek.Add("ORD", drsi.ORD.ToString());
                    //dataRadek.Add("VNDDOCNM", drsi.IsVNDDOCNMNull() ? string.Empty : drsi.VNDDOCNM.Trim());
                    dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
                    dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
                    //dataRadek.Add("LOCNCODE", drsi.IsLOCNCODENull() ? string.Empty : drsi.LOCNCODE.Trim());
                    dataRadek.Add("QTYSHPPD", drsi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                    //dataRadek.Add("QTYPACK", drsi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                    //dataRadek.Add("SERLTNUM", drsi.SERLTNUM.Trim());
                    //dataRadek.Add("KOD_SW", drsi.IsKOD_SWNull() ? string.Empty : drsi.KOD_SW.Trim());
                    //dataRadek.Add("DAT_VYROBY", drsi.IsDAT_VYROBYNull() ? string.Empty : drsi.DAT_VYROBY.Trim());
                    //dataRadek.Add("REZ_1", drsi.IsREZ_1Null() ? string.Empty : drsi.REZ_1.Trim());
                    //dataRadek.Add("ODBER_ID", drsi.IsODBER_IDNull() ? string.Empty : drsi.ODBER_ID.Trim());
                    //dataRadek.Add("DATEDONE", drsi.IsDATEDONENull() ? string.Empty : drsi.DATEDONE.Trim());
                    //dataRadek.Add("TIMEDONE", drsi.IsTIMEDONENull() ? string.Empty : drsi.TIMEDONE.Trim());
                    dataRadek.Add("USER_ID", drsi.USER_ID.ToString());
                    //dataRadek.Add("DEX_ROW_ID", drsi.DEX_ROW_ID.Trim());
                    //dataRadek.Add("guid", drsi.guid.Trim());
                    dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
                    dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
                    //dataRadek.Add("PRINTED", drsi.PRINTED.ToString());
                    //dataRadek.Add("REZ_2", drsi.IsREZ_2Null() ? string.Empty : drsi.ITEMNMBR.Trim());
                    //dataRadek.Add("INPUT_MODE", drsi.INPUT_MODE.ToString());
                    dataRadek.Add("ID_TERMINAL", drsi.ID_TERMINAL.ToString());
                    dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
                    dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());

                    dataRadky.Add(dataRadek);
                }
                #endregion

                #region 20.11.2018 Old logika

                //SqlCEDBs.DataSets.Vydej.CZMST_SIDataTable dtsi_bynmbrPal = ta_si.GetDataByNMBRPAL(paleta.sccc);

                //List<IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow>> pole2 = dtsi_bynmbrPal.GroupBy(x => x.SOPNUMBE.Trim()).Select(x => x).ToList();

                //string SOPNUMBESeznam2 = string.Empty;

                //foreach (IGrouping<string, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow> item in pole2)
                //{
                //    SOPNUMBESeznam2 += item.Key.Trim() + ",";
                //}

                //dataHlavicka.Add("OBJ", SOPNUMBESeznam2);


                //decimal qty = dtsi.Sum(x => x.QTYSHPPD);

                //dataHlavicka.Add("POCET", qty.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));






                /////
                /////Tady dotahnout data na zaklade NMBRPAL je sscc
                /////
                /////paleta.sccc;
                /////



                //foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow drsi in dtsi_bynmbrPal)
                //{
                //    //++cisloZaznamu;                    
                //    //Logging.Log.WriteDebug("Zacatek zpracovani zaznamu c. " + cisloZaznamu);


                //    Dictionary<string, string> dataRadek = new Dictionary<string, string>();

                //    //object popis = ta_se.Get_ITEMDESC(drsi.SOPNUMBE.Trim(), drsi.ITEMNMBR.Trim());
                //    string itemdesc = (string)ta_se.Get_ITEMDESC(drsi.SOPNUMBE.Trim(), drsi.ITEMNMBR.Trim());

                //    if (itemdesc == null)
                //        itemdesc = "-";
                //    else
                //        itemdesc = itemdesc.Trim();

                //    dataRadek.Add("ITEMDESC", itemdesc);

                //    dataRadek.Add("CountEntries", drsi.CountEntries.ToString());
                //    dataRadek.Add("SOPNUMBE", drsi.SOPNUMBE.Trim());
                //    dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
                //    dataRadek.Add("ORD", drsi.ORD.ToString());
                //    dataRadek.Add("VNDDOCNM", drsi.IsVNDDOCNMNull() ? string.Empty : drsi.VNDDOCNM.Trim());
                //    dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
                //    dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
                //    dataRadek.Add("LOCNCODE", drsi.IsLOCNCODENull() ? string.Empty : drsi.LOCNCODE.Trim());
                //    dataRadek.Add("QTYSHPPD", drsi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                //    dataRadek.Add("QTYPACK", drsi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                //    dataRadek.Add("SERLTNUM", drsi.SERLTNUM.Trim());
                //    dataRadek.Add("KOD_SW", drsi.IsKOD_SWNull() ? string.Empty : drsi.KOD_SW.Trim());
                //    dataRadek.Add("DAT_VYROBY", drsi.IsDAT_VYROBYNull() ? string.Empty : drsi.DAT_VYROBY.Trim());
                //    dataRadek.Add("REZ_1", drsi.IsREZ_1Null() ? string.Empty : drsi.REZ_1.Trim());
                //    dataRadek.Add("ODBER_ID", drsi.IsODBER_IDNull() ? string.Empty : drsi.ODBER_ID.Trim());
                //    dataRadek.Add("DATEDONE", drsi.IsDATEDONENull() ? string.Empty : drsi.DATEDONE.Trim());
                //    dataRadek.Add("TIMEDONE", drsi.IsTIMEDONENull() ? string.Empty : drsi.TIMEDONE.Trim());
                //    dataRadek.Add("USER_ID", drsi.USER_ID.ToString());
                //    //dataRadek.Add("DEX_ROW_ID", drsi.DEX_ROW_ID.Trim());
                //    //dataRadek.Add("guid", drsi.guid.Trim());
                //    dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
                //    dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
                //    //dataRadek.Add("PRINTED", drsi.PRINTED.ToString());
                //    dataRadek.Add("REZ_2", drsi.IsREZ_2Null() ? string.Empty : drsi.ITEMNMBR.Trim());
                //    dataRadek.Add("INPUT_MODE", drsi.INPUT_MODE.ToString());
                //    dataRadek.Add("ID_TERMINAL", drsi.ID_TERMINAL.ToString());
                //    dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
                //    dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());

                //    dataRadky.Add(dataRadek);
                //} 
                #endregion

				int? pocet = null;

				if (automat)
					pocet = 1;

				vytisteno = VydejTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, pocet);

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
				if (!automat)
					ScannerStart();
            }
        }

        private void miTiskSoupis_Click(object sender, EventArgs e)
        {
			if (!Settings.Vydej_Soupis_Tisk_Enable)
				return;

            TiskSoupiska();
        }


        //private void TiskEtiketaPalListek()
        //{
        //    List<string> sopnumberlist = new List<string>();
        //    foreach (var item in listPolozekVydej.ListPolozek)
        //    {
        //        if (!sopnumberlist.Contains(item.SOPNUMBE.Trim()))
        //        {
        //            sopnumberlist.Add(item.SOPNUMBE.Trim());
        //        }
        //    }
        //    foreach (var item in sopnumberlist)
        //    {
        //        TiskEtiketaPalListek(item);
        //    }
        //}


        #region Tisky Etiket

        private void TiskEtiketyPredloha()
        {
            try
            {
                if (!MST_Global.PovolitPrintServer)
                    return;

                ScannerStop();

                Vydej_3.ListPolozekVydej.ListPolozekRow selectedRow = this.PolozkaAktualniVybrana;
                if (selectedRow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NeniVybranaPolozka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                Dictionary<string, string> data = new Dictionary<string, string>();
                //globalni parametry
                data.Add("UserID", MST_Global.UserID.ToString());
                data.Add("UserLoginName", MST_Global.UserLoginName);
                //data radku ...
                foreach (DataColumn dcol in selectedRow.Table.Columns)
                {
                    string key = dcol.ColumnName;
                    string value = selectedRow[dcol].ToString();
                    if (!data.ContainsKey(key))
                        data.Add(key, value);
                }
                //bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPredloha);
                bool vytisteno = VydejTisk.Print(data, PrinterFactory.PrinterModules.VydejPredloha);
                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", selectedRow.CountEntries, selectedRow.SOPNUMBE, selectedRow.Itemnmbr, vytisteno.ToString(), null));

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

        private void TiskEtiketyNasnimane(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow se, Guid sinewguid)
        {
            try
            {
                //ScannerStop();

                if (MST_Global.VydejEtiketaTiskPoVlozeniDotaz) //Tisk etikety
                {
                    if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3VytisknoutEtiketuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                        return;

                    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow siRow = null;
                    //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
                    //sita.Connection.ConnectionString = "Data source=" + filename;
                    //SqlCEDBs.DataSets.Vydej.CZMST_SIDataTable sidt = sita.GetDataByGuid(sinewguid);
                    var sidt = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByGuid_SI(sinewguid);
                    siRow = sidt[0];
                    //bool vytisteno = VydejTisk.Print(se, siRow, MST_Global.PrintServerTemplateNameVydejNasnimane, null);
                    bool vytisteno = VydejTisk.Print(se, siRow, PrinterFactory.PrinterModules.VydejNasnimane, null);
                    Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", siRow.CountEntries, siRow.SOPNUMBE, siRow.ITEMNMBR, vytisteno.ToString(), null));
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                //ScannerStart();
            }
        }



        private void TiskEtiketaPalListek(string sopnumber)
        {
            try
            {
                if (!MST_Global.PovolitPrintServer)
                    return;

                ScannerStop();

                while (true)
                {
                    //Vytahne se onlinem informace o detailu objednavky,
                    //prida se id uzivatele a jmeno, kdo vychystava
                    // TODO : co dalsiho a jak ? aktualne pro ICT-NEKUPTO
                    try
                    {
                        //System.Data.SqlServerCe.SqlCeDataAdapter sda = new System.Data.SqlServerCe.SqlCeDataAdapter(
                        //    "Select distinct sopnumbe from czmst_se",
                        //    "Data source=" + filename
                        //    );
                        //sda.Fill(                

                        //VydejService.VydejService vydejservice = new VydejService.VydejService();
                        //vydejservice.Timeout = MST_Global.ServiceTimeOut;
                        //vydejservice.Url = MST_Global.ServerAddress + "Vydej.asmx";
                        //vydejservice.UpdateWebServiceCredentials();

                        //vytahne cislo objednavky z 1. zaznamu
                        // TODO : co kdyz jich je vic????
                        DataSet ds = Vydej.vydejInstance.globalObject.service_vydej.Detail(sopnumber.Trim());

                        Dictionary<string, string> data = new Dictionary<string, string>();
                        //globalni parametry
                        data.Add("UserID", MST_Global.UserID.ToString());
                        data.Add("UserLoginName", MST_Global.UserLoginName);
                        //vracene hodnoty z online
                        foreach (DataColumn dcol in ds.Tables[0].Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = ds.Tables[0].Rows[0][dcol].ToString();
                            if (!data.ContainsKey(key))
                                data.Add(key, value);
                        }

                        //TODO : nejake pocty dat a dalsi podrobnosti o vydejovych datech ???

                        //bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPalListek);
                        bool vytisteno = VydejTisk.Print(data, PrinterFactory.PrinterModules.VydejPaletovylistek);
                        Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", listPolozekVydej.ListPolozek[0].CountEntries, listPolozekVydej.ListPolozek[0].SOPNUMBE, null, vytisteno.ToString(), null));

                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3TiskPaletovehoListkuDokoncen, sopnumber.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        DialogResult dr = MessageBoxBig.Show(ex.Message + "\n\n" + Fask.Localization.Localization.Vydej3ListPolozek3OpakovatTiskDokladu + "'" + sopnumber.Trim() + "'?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                        if (dr == DialogResult.No)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                if (MST_Global.PovolitPrintServer)
                    ScannerStart();
            }
        }

        //public void TiskPalListekKonecDotaz()
        //{
        //    // TODO : pred ukoncenim se optat na tisk pal.listku ... ???
        //    // TODO : pridat dotaz na tisk pal.listku pred odeslanim ...
        //    if (MST_Global.PovolitPrintServer)
        //    {
        //        DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3VytisknoutPalListekDotaz, Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
        //        if (dr == DialogResult.Yes)
        //        {
        //            TiskEtiketaPalListek();
        //        }
        //    }
        //}

        #endregion

        #region Soupis

        public void TiskSoupiskaKonecDotaz()
        {
            if (MST_Global.PovolitPrintServer)
            {
				if (!Settings.Vydej_Soupis_Tisk_Enable)
					return;

				if (MST_Global.VydejDialogTiskSoupis)
				{
					DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3VytisknoutPalListekDotaz, Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
					if (dr == DialogResult.Yes)
					{
						TiskSoupiska();
					}
				}
				else
				{
					TiskSoupiska();
				}
            }
        }

		private void TiskSoupiska()
		{
			try
			{

				ScannerStop();

				bool vytisteno = false;
				Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
				List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
				Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

				ReturnState state = TiskSoupiskaByType(out dataHlavicka, out dataRadky, out dataPaticka);

				if (state.dr != DialogResult.OK)
				{
					throw new Exception(state.Message);
				}

				vytisteno = VydejTisk.PrintSoupiskaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, null);

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



        #endregion

        #endregion

        //private _WebRefernces_Globals.VydejServiceSession vydejservice = null;
        //private DataSet data = null;
        // private delegate void DelegateNoParam();

        private DataSet GetAdresa(string SOPNUMBE)
        {
            try
            {
                //vydejservice = new _WebRefernces_Globals.VydejServiceSession();
                //vydejservice.Timeout = MST_Global.ServiceTimeOut;
                //vydejservice.Url = MST_Global.ServerAddress + "Vydej.asmx";
                //vydejservice.UpdateWebServiceCredentials();
                //return vydejservice.Detail(SOPNUMBE.Trim());
                return Vydej.vydejInstance.globalObject.service_vydej.Detail(SOPNUMBE.Trim());
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
                return null;
            }

        }

        private DataSet GetPolozka(string ITEMNMBR)
        {
            try
            {
                //vydejservice = new _WebRefernces_Globals.VydejServiceSession();
                //vydejservice.Timeout = MST_Global.ServiceTimeOut;
                //vydejservice.Url = MST_Global.ServerAddress + "Vydej.asmx";
                //vydejservice.UpdateWebServiceCredentials();
                //return vydejservice.DetailPolozka(ITEMNMBR.Trim());
                return Vydej.vydejInstance.globalObject.service_vydej.DetailPolozka(ITEMNMBR.Trim());
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
                return null;
            }

        }

        private void menuItemZmenaRezimuSN_Click(object sender, EventArgs e)
        {
            MST_Global.Vydej_HromadneSN = !MST_Global.Vydej_HromadneSN;
            miZmenaRezimuSN.Checked = MST_Global.Vydej_HromadneSN;
            UpdateStatusBar();
        }

		private void menuItemZmenaRezimuBaliku_Click(object sender, EventArgs e)
        {
			MST_Global.Vydej_HromadneBaliky = !MST_Global.Vydej_HromadneBaliky;
			miZmenaRezimuBaliku.Checked = MST_Global.Vydej_HromadneBaliky;
            UpdateStatusBar();
        }

		private void menuItemPtatSeNaPamatovani_Click(object sender, EventArgs e)
		{
			MST_Global.Vydej_PtatSeNaPamatovani = !MST_Global.Vydej_PtatSeNaPamatovani;
			menuItemPtatSeNaPamatovani.Checked = MST_Global.Vydej_PtatSeNaPamatovani;
			UpdateStatusBar();
		}
		

		private void miHromadneCK_Click(object sender, EventArgs e)
		{
			try
			{
				ScannerStop();

				Fask.Parsing.Codes.BaseCode ObjektKod = null;
				using (Forms.MultiBarcode_Scan frm = new MultiBarcode_Scan())
				{
					DialogResult dr = frm.ShowDialog();

					if (dr != DialogResult.OK)
						return;

					ObjektKod = frm.Kod;
				}

				NajdiPolozku(ObjektKod);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
				ScannerStart();
			}
		}

		#region Test Napad

		/// <summary>
		/// Vyhledava polozku na zaklade caroveho kodu
		/// </summary>
		/// <param name="EANKod"></param>
		private void NajdiPolozku(Fask.Parsing.Codes.BaseCode code)
		{
			Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "NajdiPolozku");
			Logging.Trace2.Write("Start", "SQL nalezeni polozky", tid);

			//string EANKod = code.GTIN_13;

			try
			{
				Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();

				Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByEAN(vydejData.CZMST_SE, ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode);

				if (vydejData.CZMST_SE.Count == 0)
				{ // najde polozky dle sn ... 

					string kod = string.Empty;

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSarze) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze)))
						kod = ((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;

					if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerialNumber) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN)))
						kod = ((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;


					Vydej.vydejInstance.globalObject.controller_vydej.FillBySERLNMBR_SE_SN(vydejData.CZMST_SE_SN, kod);
					//Vydej.vydejInstance.globalObject.controller_vydej.Ta_se.ClearBeforeFill = false;
					foreach (var item in vydejData.CZMST_SE_SN)
					{
						Vydej.vydejInstance.globalObject.controller_vydej.FillBySOPNUMBEITEMNMBRORD_SE(vydejData.CZMST_SE, item.SOPNUMBE, item.ITEMNMBR, item.ORD);
					}
					//Vydej.vydejInstance.globalObject.controller_vydej.Ta_se.ClearBeforeFill = true;
				}

                // Check : !!! WTF !!!
                // pokud nenajde v predloze, pokusi se hledat online, pokud je online hledani povoleno a je povoleno online hledani materialu
                Fask.MST_W.VydejService.Vydej_Items_Online.ItemsRow vydej_online_item = null;
                if (vydejData.CZMST_SE.Count == 0)
                {
                    var go = Vydej.vydejInstance.globalObject;
                    vydej_online_item = Online.Material.Online_Material_Get(null, go.sklad == null ? null : go.sklad.skl_id, EANKod);
                    // dohledani polozky z predlohy
                    if (vydej_online_item != null)
                        Vydej.vydejInstance.globalObject.controller_vydej.SE_FillByItemnmbr(vydejData.CZMST_SE, vydej_online_item.Itemnmbr);
                }

				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = null;

				if (vydejData.CZMST_SE.Count == 0)
				{
					MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3ZboziSCarKodNenalezeno, EANKod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return;
				}
				else if (vydejData.CZMST_SE.Count == 1)
				{
					SERow = vydejData.CZMST_SE[0];
				}
				else
				{
					if (MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne)
					{
						foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow sr in vydejData.CZMST_SE)
						{
							decimal nacteno = Nacteno(sr.ITEMNMBR, sr.SOPNUMBE, sr.ORD);
							if (nacteno < sr.QTYSHPPD)
							{
								SERow = sr;
								break;
							}
						}
					}
				}

				Logging.Trace2.Write("End", "", tid);

				Logging.TracId tid0 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "vratitDavku");
				Logging.Trace2.Write("Start", "SetCurrentRow(SERow)", tid0);

				SetCurrentRow(SERow);
				Logging.Trace2.Write("End", "SetCurrentRow(SERow)", tid0);

				Logging.TracId tid1 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "vratitDavku");
				Logging.Trace2.Write("Start", "PerformInsertData(vydejData, ref SERow, code)", tid1);

				PerformInsertData(vydejData, ref SERow, code, vydej_online_item);

				Logging.Trace2.Write("End", "PerformInsertData(vydejData, ref SERow, code)", tid1);


			}
			catch (System.Data.SQLite.SQLiteException ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex, "ListPolozek3.NajdiPolozku()");
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				if (MST_Global.OnScannerSound_Vydej_3)
				{
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
				}
			}
		}


		#endregion

    }
}