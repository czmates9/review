using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Fask.Graphic;
using Fask.MST_W.Classes;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using Fask.Parsing.Codes;
using Fask.ScannerProvider;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemList : System.Windows.Forms.Form
    {
        /// <summary>
        /// Posledni pouzita expirace
        /// </summary>
        DateTime expiraceLast = DateTime.Now.AddDays(30); // TODO : konfiguracne delku expirace?

        #region pripravene dialogy
        // TODO : prejmenovat globalne na "form_..."...
        SejmiKodForm skf = new SejmiKodForm();
        PrijemZadejLokaci pzl = new PrijemZadejLokaci();
        PrijemPridatPolozku ppp = new PrijemPridatPolozku(null);
        PrijemPridatPolozkuSN pppsn = new PrijemPridatPolozkuSN(null);
        #endregion

        //private _WebRefernces_Globals.HmotnostServiceSession wsHmotnost = null;
        //private Fask.MST_W._WebRefernces_Globals.LokaceServiceSession wsLokace = null;

        //Typ vyberu - vice viz Classes.Enums (0 == nesnastaveno, ale nemelo by byt - znaci chybu v programu)
        private byte _input_mode = 0;

        // slouzi pro :
        // - testy delek rozsahu poli
        // - predavani informaci do dalsich oken pro tisky atd...
        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable _piTemp = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
        private Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry;
        private Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow prijmovalokace;

        #region Tisky pluginy
        ////Statická instance jádra.   
        //public static ApplicationCore app;
        ////Statický seznam naètených pluginù.   
        //public static System.Collections.ArrayList plugins;
        #endregion

        /// <summary>
        /// Locncode, ktere se autoamticky pouzije
        /// </summary>
        private string locncodeNaDavku = string.Empty;

        /// <summary>
        /// Cislo palety, podle ktereho se budou filtrovat a vyhledavat zaznamy
        /// </summary>
        //private string nmbrpal = string.Empty;
		public Paleta Paleta = new Paleta(); 

        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row _sklad = null;
        // TODO : !!! toto vyprat odsud ...!!!
        //private System.Data.SQLite.SQLiteConnection _davkasqlceconnection = null;        
//        private string _davka = string.Empty;
        //private string Davka
        //{
        //    set
        //    {
        //        this._davka = value;
        //        if (_davkasqlceconnection != null && _davkasqlceconnection.State == ConnectionState.Open)
        //            _davkasqlceconnection.Close();
        //        _davkasqlceconnection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(Main.StorageDir, _davka + "." + Main.Ext_Prijem));
        //        //_davkasqlcecommand = new System.Data.SQLite.SQLiteCommand();
        //        //_davkasqlcecommand.Connection = _davkasqlceconnection;
        //        _q_ta.Connection = _davkasqlceconnection;
        //        _pi_ta.Connection = _davkasqlceconnection;
        //        _pe_ta.Connection = _davkasqlceconnection;
        //        _pesn_ta.Connection = _davkasqlceconnection;
        //        _pif_ta.Connection = _davkasqlceconnection;
        //    }
        //}

        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.QueriesTableAdapter _q_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.QueriesTableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter _pi_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter _pif_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter _pe_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter _peh_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter();
        //private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter _pesn_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter();

        public enum RezimZobrazeni
        {
            List,
            Detail
        }

        public enum AktivniFiltrZobrazeni
        {
            Vse,
            Neuplne
        }


        private AktivniFiltrZobrazeni _filtrZobrazeni;
        /// <summary>
        /// Prepinani zobrazeni Vse/Neuplne (zohlednuje i cislo palety)
        /// </summary>
        public AktivniFiltrZobrazeni FiltrZobrazeni
        {
            get { return _filtrZobrazeni; }
            set
            {
                _filtrZobrazeni = value;
                SwitchFiltr();
            }
        }

        private void SwitchFiltr()
        {
            //if (filter)
            //    cZMSTPEBindingSource.Filter = "Zbyva<>0" + (string.IsNullOrEmpty(nmbrpal) ? string.Empty : (" AND NMBRPAL='" + nmbrpal + "'"));
            //else
            //    cZMSTPEBindingSource.Filter = string.IsNullOrEmpty(nmbrpal) ? string.Empty : (" NMBRPAL='" + nmbrpal + "'");

			if (Paleta != null)
			{
				switch (_filtrZobrazeni)
				{
					case AktivniFiltrZobrazeni.Neuplne:
						cZMSTPEBindingSource.Filter = "Zbyva<>0" + (string.IsNullOrEmpty(Paleta.sscc) ? string.Empty : (" AND NMBRPAL='" + Paleta.sscc + "'"));
						break;
					case AktivniFiltrZobrazeni.Vse:
					default:
						cZMSTPEBindingSource.Filter = string.IsNullOrEmpty(Paleta.sscc) ? string.Empty : (" NMBRPAL='" + Paleta.sscc + "'");
						break;
				} 
			}

            UpdateForm();
        }

        private RezimZobrazeni _rezim;
        public RezimZobrazeni Rezim
        {
            get { return _rezim; }
            set
            {
                _rezim = value;
                SwitchRezim();
            }
        }

        private void SwitchRezim()
        {
            switch (_rezim)
            {
                case RezimZobrazeni.Detail:
                    panelList.Dock = DockStyle.None;
                    panelList.Hide();
                    panelDetail.Show();
                    panelDetail.Dock = DockStyle.Fill;
                    break;
                case RezimZobrazeni.List:
                default:
                    panelDetail.Dock = DockStyle.None;
                    panelDetail.Hide();
                    panelList.Show();
                    panelList.Dock = DockStyle.Fill;
                    break;
            }
        }


        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow SelectedPE
        {
            get
            {
                try
                {
                    return (cZMSTPEBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    DataTable dt = (cZMSTPEBindingSource.List as System.Data.DataView).ToTable(false, new string[] { "PONUMBER", "ITEMNMBR", "ORD" });
                    DataRow[] perows =
                        dt.Select("PONUMBER='" + value.PONUMBER.Trim() + "' AND ITEMNMBR='" + value.ITEMNMBR.Trim() + "' AND ORD=" + value.ORD);
                    if (perows.Length > 0)
                    {
                        dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(perows[0]);
                    }
                }
                catch
                {
                }
            }
        }

        public PrijemList(
            //string davka, 
            Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad, 
            Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry, 
            Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow prijmovalokace
            )
        {
            Cursor.Current = Cursors.WaitCursor;
            //PrijemList.Instance = this;
            InitializeComponent();
            MyInitializeGrid();
            MyInitializePopisItems();

            dataGrid1.KeyScrollDown = MST_Global.DataGridScrollDown;
            dataGrid1.KeyScrollUp = MST_Global.DataGridScrollUp;

            //this.Davka = davka;
            //this.Text += " (" + _davka + ")";
            this.Text += " (" + Prijem_4.PrijemMain.prijemInstance.globalObject.Davka + ")";
            this._sklad = sklad;
            this.miTisk.Enabled = MST_Global.PovolitPrintServer;

            this.prijemDataParametry = prijemDataParametry;
            this.prijmovalokace = prijmovalokace;
            
            // povoleni zalokovani
            this.miZalokovat.Enabled = Prijem_4.Globals.PovolitZalokovani;
            if (!Prijem_4.Globals.PovolitZalokovani && menuItem1.MenuItems.Contains(this.miZalokovat))
                menuItem1.MenuItems.Remove(this.miZalokovat);

            // nastaveni lokace pouze v pripade, ze je lokace na davku povolena
            //this.miNastavitLokaci.Enabled = !Prijem_4.Globals.PovolitZalokovani && Prijem_4.Globals.LokaceNaDavkuPovolit;
            this.miNastavitLokaci.Enabled = Prijem_4.Globals.LokaceNaDavkuPovolit;
            if (!miNastavitLokaci.Enabled && menuItem1.MenuItems.Contains(this.miNastavitLokaci))
                menuItem1.MenuItems.Remove(this.miNastavitLokaci);

            this.miRezimZadavaniLokace.Enabled = Prijem_4.Globals.PovolitZmenuRezimuZadaniLokace;
            if (!miRezimZadavaniLokace.Enabled && menuItem1.MenuItems.Contains(this.miRezimZadavaniLokace))
                menuItem1.MenuItems.Remove(this.miRezimZadavaniLokace);

            Cursor.Current = Cursors.Default;
        }

        private void MyInitializePopisItems()
        {
            label_Czcarkod.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.CZ_CarKodColumn.ColumnName].HeaderText + " :";
            label_Itemnmbr.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ITEMNMBRColumn.ColumnName].HeaderText + " :";
            label_Nacteno.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.NasnimanoColumn.ColumnName].HeaderText + " :";
            label_Nazev.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.ITEMDESCColumn.ColumnName].HeaderText + " :";
            label_Ponumber.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.PONUMBERColumn.ColumnName].HeaderText + " :";
            label_Qtypack.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.QTYPACKColumn.ColumnName].HeaderText + " :";
            label_Qtyshppd.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.QTYSHPPDColumn.ColumnName].HeaderText + " :";
            label_Vnddocnm.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.VNDDOCNMColumn.ColumnName].HeaderText + " :";
            label_Vnditnum.Text = dataGrid1.TableStyles[0].GridColumnStyles[prijem.CZMST_PE.VNDITNUMColumn.ColumnName].HeaderText + " :";
        }


        private void MyInitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }


        private void PrijemList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.Timeout = prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_TIMEOUTNull() ? 20000 : prijemDataParametry.Parametry[0].CONFIG_LOKACE_TIMEOUT;

                var dt_peh = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetData_PEH();
                if (dt_peh.Count == 0)
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Insert_PEH(Prijem_4.PrijemMain.prijemInstance.globalObject.Davka, Guid.NewGuid());

                if (prijemDataParametry.Parametry[0].ENABLE_LISTSNIM)
                {
                    if (prijemDataParametry.Parametry[0].CONFIG_LISTSNIM)
                    {
                        Rezim = RezimZobrazeni.Detail;
                    }
                    else
                    {
                        Rezim = RezimZobrazeni.List;
                    }
                }
                else
                {
                    Rezim = RezimZobrazeni.Detail;
                }

                FillData();

                aktualizaceNasnimanehoMnozstviKontrola();

                //bool deleteSloucena = true;
                if (kontrolaDokoncenosti(prijemDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI))
                {
                    if (odeslatAktualniDavku())
                        return; // ukoncim, jen pokud se podari odeslat davku...
                }

                this.dataGrid1.CurrentRowIndex = 0;
                this.dataGrid1.Focus();

                this.menuItemObjednavkaDetail.Enabled = MST_Global.OnlineObjednavkaDetailPovolit;
                this.menuItemKusu.Enabled = MST_Global.OnlinePolozkaKusuPovolit;
                this.menuItemKusuNaSklade.Enabled = MST_Global.OnlinePolozkaKusuNaSkladePovolit;
                this.menuItemDetailItemnumber.Enabled = MST_Global.OnlinePolozkaDetailPovolit;

                //ZobrazVseNeuplne(Settings.PrijemZobrazeniFiltrVseNeuplne);
                FiltrZobrazeni = Settings.PrijemZobrazeniFiltrVseNeuplne2;

                // TODO : dotaz na vytisteni etiket predlohy celeho dokladu ... 
                if (MST_Global.PovolitPrintServer && Prijem_4.Globals.EtiketyTiskPoOtevreniDotaz)
                {
                    DialogResult drTisk = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketPrijemkyDotaz, Fask.Localization.Localization.Prijem4PrijemListTiskEtiket, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button1);
                    if (drTisk == DialogResult.Yes)
                    {
                        DialogResult drMnozstvi = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketZPredlohyDotaz
                            , Fask.Localization.Localization.Prijem4PrijemListTiskEtiketPocet, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question, MessageBoxDefaultButton.Button2
                            );

                        try
                        {
                            //ScannerStop();
                            int polozekpocet = this.prijem.CZMST_PE.Count;
                            int polozekcount = 0;

                            Program.mstw.mbw.BeginPracujiForm();
                            // todo : cekaci/informacni dialog mbw ... ???
                            foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe in this.prijem.CZMST_PE)
                            {
                                polozekcount++;
                                Program.mstw.mbw.Zprava = string.Format(Fask.Localization.Localization.Prijem4PrijemListTiskAktualniStav, polozekcount, polozekpocet);
                                //bool vytisteno = PrijemTisk.Print(
                                //    pe,
                                //    MST_Global.PrintServerTemplateNamePrijemPredloha, 
                                //    drMnozstvi == DialogResult.No ? 1 : (int)(pe.QTYSHPPD) );
                                bool vytisteno = PrijemTisk.Print(
                                    pe,
                                    PrinterFactory.PrinterModules.PrijemPredloha,
                                    drMnozstvi == DialogResult.No ? 1 : (int)(pe.QTYSHPPD));
                                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", pe.CountEntries, pe.PONUMBER, pe.ITEMNMBR, vytisteno.ToString(), null));
                            }

                            Program.mstw.mbw.EndPracujiForm();
                        }
                        catch (Exception ex)
                        {
                            Program.mstw.mbw.EndPracujiForm();
                            MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                        finally
                        {
                            //ScannerStart();
                        }
                    }
                    //else //jinak nic a pokracuje normalne ...
                }

                menuItemTiskAnoNE.Checked = Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz;
                mi_KonScan.Checked = Program.mstw.Scanner.ContinuousRead;

                this.ScannerStart();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        private void FillData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //SqlCEDBs.DataSets.Prijem.CZMST_PEDataTable dt_pe = this._pe_ta.GetData();
                var dt_pe = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetData_PE();

                this.prijem.CZMST_PE.BeginLoadData();
                this.prijem.CZMST_PE.Clear();
                foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow in dt_pe)
                {
                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[] rowsexist = this.FindedPE(perow.ITEMNMBR, perow.PONUMBER, perow.ORD);
                    if (rowsexist.Length == 0)
                    {
                        prijem.CZMST_PE.ImportRow(perow);
                    }
                }
                this.prijem.CZMST_PE.EndLoadData();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void aktualizaceNasnimanehoMnozstviKontrola()
        {
#if DEBUG
            int tstart, tend, tdiff = 0;
            tstart = System.Environment.TickCount;
#endif
            try
            {
                if (Prijem_4.Globals.OnlinePohyby)
                {
                    //Program.mstw.mbw.BeginPracujiForm("Probíhá online kontrola nasnímaného množství");
                    Online_Quantity(prijem);
                }
                else
                {
                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemListKontrolaNasnimMnozstvi);

                    #region Old code
                    //SqlCEDBs.DataSets.Prijem.CZMST_PI_NasnimanoDataTable dt_pi_nas = null;
                    //using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_NasnimanoTableAdapter ta_pi_nas = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_NasnimanoTableAdapter())
                    //{
                    //    ta_pi_nas.Connection = _davkasqlceconnection;
                    //    dt_pi_nas = ta_pi_nas.GetData();
                    //}

                    //for (int j = 0; j < dt_pi_nas.Count; j++)
                    //{
                    //    Program.mstw.mbw.Zprava = string.Format(Fask.Localization.Localization.Prijem4PrijemListKontrolaNasnimMnozstviProgres, (j + 1), dt_pi_nas.Count, ((float)(j + 1) / (dt_pi_nas.Count)).ToString("P"));

                    //    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_NasnimanoRow pi_nas_row = dt_pi_nas[j];

                    //    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[] perows = FindedPE(pi_nas_row.ITEMNMBR, pi_nas_row.PONUMBER, pi_nas_row.ORD);
                    //    if (perows.Length > 0)
                    //    {
                    //        if (perows[0].Nasnimano != pi_nas_row.Nasnimano)
                    //        {
                    //            for (int i = 0; i < perows.Length; i++)
                    //            {
                    //                perows[i].Nasnimano = pi_nas_row.Nasnimano;
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    prijem.CZMST_PE.BeginLoadData();
                    var dt_pi_nas = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetData_PI_Nasnimano();
                    dt_pi_nas.ToList().ForEach(x => {
                        var j = dt_pi_nas.Rows.IndexOf(x);
                        Program.mstw.mbw.Zprava = string.Format(Fask.Localization.Localization.Prijem4PrijemListKontrolaNasnimMnozstviProgres, (j + 1), dt_pi_nas.Count, ((float)(j + 1) / (dt_pi_nas.Count)).ToString("P"));
                        var pe_rows = FindedPE(x.ITEMNMBR, x.PONUMBER, x.ORD);
                        pe_rows.ToList().ForEach(y => y.Nasnimano = x.Nasnimano);
                    });
                    prijem.CZMST_PE.EndLoadData();

                }
                prijem.CZMST_PE.AcceptChanges();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }
#if DEBUG
            tend = System.Environment.TickCount;
            tdiff = tend - tstart;
            System.Diagnostics.Debug.WriteLine("NasnimanehoMnozstviKontrola: " + (new TimeSpan(tdiff)).TotalSeconds.ToString() + "[s]");
#endif
        }

        #region Scanner start stop
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
        delegate void ScannerEventHandlerCall(ScannerEventArgs e);
		private void OnScannerEvent(ScannerEventArgs e)
		{
#if DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
			try
			{
				ScannerStop();

				string ck = e.BarcodeData.Trim();

				// parsovani vahoveho kodu
				Fask.Parsing.Codes.BaseCode code = null;
				// konfigurace aplikace ...
				if (Prijem_4.Globals.ParsovaniCarovehoKoduPovolit)
				{
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


					if (code is Parsing.Codes.Interfaces.ICodeItemnmbr)
						ck = ((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr;
					else if (code is Parsing.Codes.Interfaces.ICodeBarcode)
						ck = ((Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode;

				}
#if DEBUG
                //Logging.Log.WriteDebug("Parsovani kodu: " + sw.Elapsed.ToString(), "Prijem4.PrijemList.OnScannerEvent(" + e.BarcodeData.Trim() + ")");
                System.Diagnostics.Debug.WriteLine("Parsovani kodu: " + sw.Elapsed.ToString());
                //sw.Reset(); sw.Start();
#endif

				if (ck.Length > 0)
				{
					Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow = null;
					if (NajdiPolozkuCarovyKod(ck, out prow))
					{
						//Ve funkci pro nalezeni se nastavi input mode na hledani - ale tady se to specifikuje, ze bylo vyhledano scannerem
						_input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);
						PerformInsert(prow, code);
#if DEBUG
                        //Logging.Log.WriteDebug("Vlozeni polozky: " + sw.Elapsed.ToString(), "Prijem4.PrijemList.OnScannerEvent(" + e.BarcodeData.Trim() + ")");
                        System.Diagnostics.Debug.WriteLine("Vlozeni polozky: " + sw.Elapsed.ToString());
                        //sw.Reset(); sw.Start();
#endif
					}
					else
					{
						return; // nenalezeno, tak to ukoncim, kvuli zbytecnymu dalsimu pipani => uzivatel stejne vi, ze konci ...
					}
					//UpdateForm();
				}
			}
			finally
			{
				ScannerStart();

				if (MST_Global.OnScannerSound_Prijem_4)
				{
					MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
				}
			}

		}

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new ScannerEventHandlerCall(OnScannerEvent), new object[] { e });



        }
        #endregion

        private void NajdiPolozkuNazev()
        {
            try
            {
                ScannerStop();
                string nazev = string.Empty;

                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prijem4PrijemListNajdiPolozkuNazev, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    nazev = skf.Kod;
                }

                Fask.SQLiteDBs.DataSets.Prijem ds_prijem = new Fask.SQLiteDBs.DataSets.Prijem();
                Cursor.Current = Cursors.WaitCursor;
                // 18.7.2016 PeV: uprava, aby se zohlednovalo cislo palety pri vyhledavani

				if (Paleta == null)
					Paleta = new Paleta();

				if (string.IsNullOrEmpty(Paleta.sscc))
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByItemdescpart_PE(ds_prijem.CZMST_PE, "%" + nazev + "%");
                else
					Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByItemdescpartNmbrpal_PE(ds_prijem.CZMST_PE, "%" + nazev + "%", Paleta.sscc);

                Cursor.Current = Cursors.Default;

                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow = null;

                // TODO : aktualizace mnozstvi online ...
                Online_Quantity(ds_prijem);

                if (ds_prijem.CZMST_PE.Count == 0)
                {
                    if (MST_Global.PrijemTimeDialog)
                        MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamNenalezen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.PrijemTimeDialogInterval);
                    else
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamNenalezen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (ds_prijem.CZMST_PE.Count == 1)
                {
                    PerformInsert(ds_prijem.CZMST_PE[0]);
                    return;
                }
                else
                {
                    using (PrijemVyberPolozky pvyber = new PrijemVyberPolozky())
                    {
                        pvyber.PrijemData = ds_prijem;
                        if (pvyber.ShowDialog() == DialogResult.Cancel)
                            return;
                        prow = pvyber.SelectedPE;
                    }
                }

                if (prow == null)
                {
                    if (MST_Global.PrijemTimeDialog)
                        MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamNebylVybran, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.PrijemTimeDialogInterval);
                    else
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamNebylVybran, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    SelectedPE = prow;
                    PerformInsert(prow);
                }

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void NajdiPolozkuCarovyKod()
        {
            try
            {
                ScannerStop();
                string ck = string.Empty;

                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prijem4PrijemListNajdiPolozkuCarKod, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
                    ck = skf.Kod;
                }

                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow = null;

                if (NajdiPolozkuCarovyKod(ck, out prow))
                {
                    PerformInsert(prow);
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

        private bool NajdiPolozkuCarovyKod(string carovykod, out Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow)
        {
            prow = null;

			if (Paleta == null)
				Paleta = new Paleta();

            Fask.SQLiteDBs.DataSets.Prijem ds_prijem = new Fask.SQLiteDBs.DataSets.Prijem();
            Cursor.Current = Cursors.WaitCursor;
            // 18.7.2016 PeV: uprava, aby se zohlednovalo cislo palety pri vyhledavani
			if (string.IsNullOrEmpty(Paleta.sscc))
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByBarcode_PE(ds_prijem.CZMST_PE, carovykod);
            else
				Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByBarcodeNmbrpal_PE(ds_prijem.CZMST_PE, carovykod, Paleta.sscc);

            if (ds_prijem.CZMST_PE.Count == 0)
            { // najdou se polozky z sn ...
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillBySN_PE_SN(ds_prijem.CZMST_PE_SN, carovykod);
                //Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Ta_pe.ClearBeforeFill = false;
                foreach (var item in ds_prijem.CZMST_PE_SN)
                {
                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByITEMNMBR_PE(ds_prijem.CZMST_PE, item.ITEMNMBR);
                }
            }

            Cursor.Current = Cursors.Default;

            // TODO : aktualizace mnozstvi online ...
            Online_Quantity(ds_prijem);

            if (ds_prijem.CZMST_PE.Count == 0)
            {
                if (MST_Global.PrijemTimeDialog)
                    MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListZaznamSCarKodNenalezen, carovykod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.PrijemTimeDialogInterval);
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListZaznamSCarKodNenalezen, carovykod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return false;
            }
            else if (ds_prijem.CZMST_PE.Count > 1)
            {
                if (Globals.HledaniCkAutoVyberPrvniNeuplne)
                {
                    foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe in ds_prijem.CZMST_PE)
                    {
                        if (pe.Nasnimano < pe.QTYSHPPD)
                        {
                            prow = pe;
                            break;
                        }
                    }
                }

                if (prow == null)
                {
                    if (MST_Global.PrijemTimeDialog)
                        MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListNalezenoViceZaznamuVyber, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.PrijemTimeDialogInterval);
                    else
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListNalezenoViceZaznamuVyber, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    using (PrijemVyberPolozky pvyber = new PrijemVyberPolozky())
                    {
                        pvyber.PrijemData = ds_prijem;
                        if (pvyber.ShowDialog() == DialogResult.Cancel)
                            return false;
                        prow = pvyber.SelectedPE;
                    }
                }
            }
            else
            {
                prow = ds_prijem.CZMST_PE[0];
            }
            if (prow != null)
                SelectedPE = prow;
            return true;
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            if (Globals.PrijemDialogOpusteniPrijemky)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListUkonceniPraceSPrijemkouDotaz, Fask.Localization.Localization.Prijem4PrijemListPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {

            if ((skf != null))
            {
                skf.Dispose();
                skf = null;
            }

            if ((pzl != null))
            {
                pzl.Dispose();
                pzl = null;
            }

            if ((ppp != null))
            {
                ppp.Dispose();
                ppp = null;
            }

            if ((pppsn != null))
            {
                pppsn.Dispose();
                pppsn = null;
            }

            //if (_davkasqlceconnection != null)
            //{
            //    if ((_davkasqlceconnection.State & ConnectionState.Open) == ConnectionState.Open)
            //        _davkasqlceconnection.Close();
            //    _davkasqlceconnection.Dispose();
            //}
            //if (_q_ta != null) _q_ta.Dispose();
            //if (_pi_ta != null) _pi_ta.Dispose();
            //if (_pe_ta != null) _pe_ta.Dispose();
            //if (_pesn_ta != null) _pesn_ta.Dispose();
            //if (_pif_ta != null) _pif_ta.Dispose();
            //if (_q_ta != null) _q_ta.Dispose();

            //_davkasqlceconnection = null;
            //_q_ta = null;
            //_pi_ta = null;
            //_pe_ta = null;
            //_pesn_ta = null;
            //_pif_ta = null;

            Cursor.Current = Cursors.WaitCursor;
            this.ScannerFinalize();
            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            //Settings.PrijemZobrazeniFiltrVseNeuplne = !String.IsNullOrEmpty(cZMSTPEBindingSource.Filter);
            Settings.PrijemZobrazeniFiltrVseNeuplne2 = FiltrZobrazeni;
            Cursor.Current = Cursors.Default;
        }

        private void PrijemList_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
                PerformInsert();
            }
            else if (e.KeyCode == Keys.Back)
            {
                PerformDelete();
            }
            else if (e.KeyCode == Keys.D0)
            {
                razeniNazev();
            }
            else if (e.KeyCode == Keys.D1)
            {
                //ZobrazVseNeuplne();
                FiltrZobrazeni = (FiltrZobrazeni == AktivniFiltrZobrazeni.Neuplne) ? AktivniFiltrZobrazeni.Vse : AktivniFiltrZobrazeni.Neuplne;
            }
            else if (e.KeyCode == Keys.D2)
            {
                razeniORD();
            }
            else if (e.KeyCode == Keys.D3)
            {
                PerformShowInserted();
            }
            else if (e.KeyCode == Keys.D4)
            {
                Prijem_4.Globals.MnozstviAutoJedna = !Prijem_4.Globals.MnozstviAutoJedna;
                UpdateStatusBar();
            }
            else if (e.KeyCode == Keys.D5)
            {
                menuItem17_Click(null, null);
            }
            else if (e.KeyCode == Keys.D6)
            {
                ChangeRezim();
            }
            else if (e.KeyCode == Keys.D7)
            {
                razeniOrig();
            }
            else if (e.KeyCode == Keys.D8)
            {
                if (MST_Global.OnlineObjednavkaDetailPovolit)
                    DetailPrint();
            }
            else if (e.KeyCode == Keys.D9)
            {
                miFiltrPaleta_Click(null, null);
            }
            else if (e.KeyCode == Keys.F1)
            {
                if (MST_Global.OnlinePolozkaKusuPovolit)
                    DetailPocetKusu();
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (MST_Global.OnlinePolozkaKusuNaSkladePovolit)
                    DetailPocetKusuLokace();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (MST_Global.OnlinePolozkaDetailPovolit)
                    DetailItemnumber();
            }
            else if (e.KeyCode == Keys.F4)
            {
                _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
                NajdiPolozkuCarovyKod();
            }
            else if (e.KeyCode == Keys.F5)
            {
                _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
                NajdiPolozkuNazev();
            }
            else if (e.KeyCode == Keys.F6)
            {
                NajdiPolozkuPozice();
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (Prijem_4.Globals.PovolitZalokovani)
                    Zalokovat();
            }
            else if (e.KeyCode == Keys.F8)
            {
                // nastaveni lokace, ktera se bude pouzivat
                miNastavitLokaci_Click(null, null);
            }
            else if (e.KeyCode == Keys.F9)
            {
                PerformZmenaRezimuLokace();
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ChangeRezim()
        {
            if (Rezim == RezimZobrazeni.Detail)
                Rezim = RezimZobrazeni.List;
            else
                Rezim = RezimZobrazeni.Detail;
        }

        private void PerformInsert()
        {

            //Logging.TracId id = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, int.Parse(_davka), this.Name, "PrijemList");
            //Logging.Trace2.Write("1", "1", id);


            try
            {
                //try
                //{
                //    Assembly assembly = Assembly.LoadFrom("sqlcecompact35.dll");
                //    Version ver = assembly.GetName().Version;
                //    Logging.Log.Write(string.Format("Verze:{0}:{1}:{2}:{3}", ver.Major,ver.Minor,ver.Revision,ver.Build), "Prijem List");
                //}
                //catch
                //{
                //    Logging.Log.Write("Verze Nenalezena", "Prijem List");
               
                //}
       
                //Logging.Log.Write(string.Format("Pamet .net Zacatek Pred Collect:{0}", GC.GetTotalMemory(true)), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet system Zacatek Pred Collect:{0}", MySystem.Memory.Info()), "Prijem List");
                //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                //sw.Start();
                //GC.Collect();
                //GC.Collect();
                //sw.Stop();
                //TimeSpan ts = sw.Elapsed;
                //Logging.Log.Write(string.Format("èas vykonani zacatek GC.Collect() : {0}:{1}",ts.Seconds, ts.Milliseconds), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet .net Zacatek po Collect:{0}", GC.GetTotalMemory(true)), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet system Zacatek po Collect:{0}", MySystem.Memory.Info()), "Prijem List");

                //Logging.Trace2.Write("2", "2", id);
                ScannerStop();

                // neni vybrana konkretni polozka, pouzije se aktualni aktivni a dohleda se, zda existuji varianty
                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow = SelectedPE;

                if (perow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniVybrana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                //Logging.Trace2.Write("3", "3", id);
                Fask.SQLiteDBs.DataSets.Prijem ds_prijem = new Fask.SQLiteDBs.DataSets.Prijem();
                Cursor.Current = Cursors.WaitCursor;
                //Logging.Trace2.Write("3.1", "3.1", id);  
                
                //GC.KeepAlive(

                // 27.6.2018 JiS - GC.WaitForPendingFinalizers - intermittently hangs - test
                //GC.SuppressFinalize(_pe_ta.Connection);
                //Logging.Trace2.Write("3.2", "3.2", id);
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillByKey_PE(ds_prijem.CZMST_PE, perow.PONUMBER, perow.ITEMNMBR, perow.ORD);
                //Logging.Trace2.Write("3.2", "3.2", id);
                //GC.KeepAlive(_pe_ta.Connection);
                //GC.ReRegisterForFinalize(_pe_ta.Connection);

                Cursor.Current = Cursors.Default;
                //Logging.Trace2.Write("4", "4", id);
                // TODO : aktualizace mnozstvi online ...
                Online_Quantity(ds_prijem);
                //Logging.Trace2.Write("5", "5", id);
                if (ds_prijem.CZMST_PE.Count == 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamKliceNenalezen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                else if (ds_prijem.CZMST_PE.Count > 1)
                {
                    //Logging.Trace2.Write("6", "6", id);
                    if (MST_Global.PrijemTimeDialog)
                        MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListNalezenoViceZaznamuVyber, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.PrijemTimeDialogInterval);
                    else
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListNalezenoViceZaznamuVyber, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    using (PrijemVyberPolozky pvyber = new PrijemVyberPolozky())
                    {
                        //Logging.Trace2.Write("7", "7", id);
                        pvyber.PrijemData = ds_prijem;
                        if (pvyber.ShowDialog() == DialogResult.Cancel)
                            return;
                        //Logging.Trace2.Write("8", "8", id);
                        perow = pvyber.SelectedPE;
                        //Logging.Trace2.Write("9", "9", id);

                    }
                }
                else
                {

                    //Logging.Trace2.Write("10", "10", id);
                    perow = ds_prijem.CZMST_PE[0];
                    //Logging.Trace2.Write("11", "11", id);
                }

                if (perow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPolozkaNenalezena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                //Logging.Trace2.Write("12", "12", id);
                PerformInsert(perow);
                //Logging.Trace2.Write("13", "13", id);

            }
            finally
            {
                //Logging.Trace2.Write("14", "14", id);
                ScannerStart();
                //Logging.Trace2.Write("15", "15", id);
                //Logging.Log.Write(string.Format("Pamet .net Konec Pred Collect:{0}", GC.GetTotalMemory(true)), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet system Konec Pred Collect:{0}", MySystem.Memory.Info()), "Prijem List");
                //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                //sw.Start();
                //GC.Collect();
                //GC.Collect();
                //sw.Stop();
                //TimeSpan ts = sw.Elapsed;
                //Logging.Log.Write(string.Format("èas vykonani Konec GC.Collect() : {0}:{1}", ts.Seconds, ts.Milliseconds), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet .net Konec po Collect:{0}", GC.GetTotalMemory(true)), "Prijem List");
                //Logging.Log.Write(string.Format("Pamet system Konec po Collect:{0}", MySystem.Memory.Info()), "Prijem List");
                //Logging.Trace2.Write("16", "16", id);
            }
        }

		//private void PerformInsert(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow)
		//{
		//    PerformInsert(PERow, null);
		//}

//        private void PerformInsert(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow, BaseCode code)
//        {

//            skf = new SejmiKodForm();
//            pzl = new PrijemZadejLokaci();
//            ppp = new PrijemPridatPolozku(PERow);
//            pppsn = new PrijemPridatPolozkuSN(PERow);

//            try
//            {
//#if DEBUG
//                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
//                swatch.Start();
//                System.Diagnostics.Debug.WriteLine("PerformInsert: 1" + swatch.Elapsed.ToString());
//#endif
//                //Kontrola, zda se nema povolit jen scanner
//                if (!InputModeChecker.checkInputMode(Prijem_4.Globals.PolozkyVyberJenScannerem, _input_mode))
//                {
//                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
//                    return;
//                }

//                System.Guid newguid = System.Guid.Empty;
//                try
//                {
//                    ScannerStop();

//                    //Jestlize neni vybrany, pak prisel ze scanneru a da se na vyber
//                    if (PERow == null)
//                    {
//                        return;
//                    }

//                    OnlineCheckHmotnost(PERow);


//#if DEBUG
//                    System.Diagnostics.Debug.WriteLine("PerformInsert: 2" + swatch.Elapsed.ToString());
//#endif

//                    #region Kontrola uplnosti polozky
//                    // dohledani poctu, pokud je predloha, existuje konf.soubor a je nastaven
//                    // odpovidajici parametr
//                    if (prijemDataParametry.Parametry[0].CONFIG_POKRDOHLED)
//                    {
//                        //decimal Quantity = this.Nacteno(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD);
//                        decimal Quantity = PERow.Nasnimano;
//                        if (Quantity >= PERow.QTYSHPPD)
//                        {
//                            MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "chimes.wav"));
//                            // podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
//                            DialogResult dres = MessageBoxBig.Show(
//                                (Quantity > PERow.QTYSHPPD ? Fask.Localization.Localization.Prijem4PrijemListPolozkaPreplnenaPokracovaniDotaz : Fask.Localization.Localization.Prijem4PrijemListPolozkaKompletniPokracovaniDotaz),
//                                Fask.Localization.Localization.Prijem4PrijemListKontrolaUplnosti,
//                                MessageBoxButtons.YesNo,
//                                MessageBoxBigIcon.Question
//                                );
//                            if (dres == DialogResult.No)
//                            {
//                                return;
//                            }

//                            if (!Globals.OverFillItem)
//                            {
//                                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazano, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
//                                return;
//                            }

//                        } // if (sinstruct.Quantity <=
//                    } // if((CONFIG_POKRDOHLED &
//                    #endregion

//#if DEBUG
//                    System.Diagnostics.Debug.WriteLine("PerformInsert: 3" + swatch.Elapsed.ToString());
//#endif
//                    string vnditnum = string.Empty;
//                    string locncode = string.Empty;
//                    //string rez2 = string.Empty;

//                    if (prijemDataParametry.Parametry[0].CONFIG_SNIM_PONUMBER)
//                    {
//                        skf.Popis = MST_Global.PON_NAME;
//                        skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
//                        skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["VNDITNUM"].MaxLength;
//                        skf.CheckLen = true;
//                        skf.AllowEmpty = false;
//                        skf.Kod = string.Empty;

//                        if (skf.ShowDialog() == DialogResult.Cancel)
//                            return;
//                        vnditnum = skf.Kod;
//                    }

//                    // 16.1.2017 JiS REZ2 se snima nove pro kazdy zaznam ve vnitrnim kole, dle hodnoty na predloze...
//                    //if (prijemDataParametry.Parametry[0].CONFIG_SNIM_REZ2)
//                    //{
//                    //    skf.Popis = MST_Global.REZ2_PRIJ_NAME;
//                    //    skf.CodeType = Prijem_4.Globals.Rez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
//                    //    skf.Len = Globals.SERNUM_LEN;
//                    //    skf.CheckLen = true;
//                    //    skf.AllowEmpty = !Prijem_4.Globals.Rez2Povinne;
//                    //    skf.Kod = Prijem_4.Globals.Rez2Pamatovat ? Settings.PrijemRez2LastValue : string.Empty;

//                    //    if (skf.ShowDialog() == DialogResult.Cancel)
//                    //        return;
//                    //    rez2 = skf.Kod;
//                    //    if (Prijem_4.Globals.Rez2Pamatovat) Settings.PrijemRez2LastValue = rez2;
//                    //}

//                    bool DalsiSN = true;

//                    while (DalsiSN)
//                    {
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 4" + swatch.Elapsed.ToString());
//#endif
//                        //List<string> seriova_cisla = null;
//                        newguid = System.Guid.Empty; //inicializace noveho guid...

//                        Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow PIRow = _piTemp.NewCZMST_PIRow();
//                        PIRow.VNDITNUM = vnditnum;
//                        //PIRow.REZ_2 = rez2;
//                        PIRow.guid = newguid;

//                        ppp._pi = PIRow;
//                        pppsn.PI = PIRow;

//                        Logging.Log.WriteDebug("while (DalsiSN)", "PerformInsert");
//                        //string sn = string.Empty;
//                        // 18.7.2016 PeV: uprava, aby se z predlohy vyplnovalo SN, pokud se pouziva ...
//                        string sn = string.Empty;
//                        if (code is BarcodeSlashSarze)
//                            sn = ((BarcodeSlashSarze)code).sarze;
//                        else
//                            sn = PERow.SERLTNUM.Trim();

//                        decimal qty = decimal.Zero;
//                        string sw = string.Empty;
//                        string dv = string.Empty;
//                        string rez1 = string.Empty;
//                        string rez2 = string.Empty;
//                        string skl_id = string.Empty;

//                        // lokace na davku, automaticky se pouzije ...
//                        if (miNastavitLokaci.Checked && !string.IsNullOrEmpty(locncodeNaDavku))
//                        {
//                            PERow.LOCNCODE = locncodeNaDavku;
//                        }
//                        // je povolena prijmova lokace, dojde k jejimu pouziti
//                        else if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_PRIJMOVANull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA)
//                        {
//                            PERow.LOCNCODE = prijmovalokace.IsLOCNCODENull() ? string.Empty : prijmovalokace.LOCNCODE.Trim();
//                        }

//                        if (Prijem_4.Globals.ZadaniLocncodePredSN)
//                        {
//                            // povoleno zobrazeni dialogu pro zadani lokace v prijemparams nebo na terminalu
//                            if (prijemDataParametry.Parametry[0].CONFIG_SNIM_LOCNCODE || miRezimZadavaniLokace.Checked)
//                            {
//                                pzl.Popis = MST_Global.LC_NAME;
//                                pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
//                                pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
//                                //skf.Len = Globals.LOCNCODE_LEN;
//                                //skf.CheckLen = true;
//                                pzl.AllowEmpty = false;
//                                pzl.Kod = PERow.LOCNCODE;
//                                pzl.perow = PERow;

//                                if (pzl.ShowDialog() == DialogResult.Cancel)
//                                    return;
//                                locncode = pzl.Kod;
//                                PIRow.LOCNCODE = locncode;
//                            }
//                        }

//                        // nacteni id skladu
//                        if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
//                            skl_id = _sklad != null ? _sklad.skl_id : string.Empty;
//                        else
//                            skl_id = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;

//                        #region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)
//                        // nacteni skladu online ...
//                        if (Prijem_4.Globals.NacistSkladIDOnline)
//                        {
//                            string sklad_id = string.Empty;
//                            string sklad_id_dest = string.Empty;

//                            MST_W.ProdejService.STATUS status = OnlineGetSklad(string.Empty, PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR, PERow.SERLTNUM, out sklad_id, out sklad_id_dest);
//                            if (status == Fask.MST_W.ProdejService.STATUS.ERROR) // chyba, ukoncit ...
//                                return;
//                            else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
//                            {
//                                MessageBoxBig.Show(string.Format("Nepodaøilo se naèíst ID skladu online pro materiál '{0}'", PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
//                                return;
//                            }

//                            skl_id = sklad_id_dest;
//                        }
//                        #endregion

//                        if (PERow.CZ_SerNum_Track == 0) //sledovano na mnozstvi
//                        {
//                            if (Prijem_4.Globals.MnozstviAutoJedna)
//                            {
//                                qty = 1;
//                            }
//                            else
//                            {
//                                if (!Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
//                                {
//                                    WeightCode wc = (WeightCode)code;

//                                    qty =
//                                        (wc.weight
//                                        / (PERow.IsWEIGHTNull() || (PERow.WEIGHT == 0) ? 1 : PERow.WEIGHT)
//                                        / ((PERow.QTYPACK == 0) ? 1 : PERow.QTYPACK)
//                                        );
//                                }
//                                else
//                                {
//                                    //ppp.Popis = "Množství";
//                                    ppp.Popis = PERow.QTYPACK > 0 ? Fask.Localization.Localization.Prijem4PrijemListMnozstviBaleni : Fask.Localization.Localization.Prijem4PrijemListMnozstvi; //"Množství" + (PERow.QTYPACK > 0 ? " balení" : string.Empty);
//                                    ppp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
//                                    ppp.Serltnum = string.Empty;
//                                    ppp.Len = 0;
//                                    ppp.CheckLen = false;
//                                    ppp.AllowEmpty = false;
//                                    ppp.Kod = mnozstviPredvyplnit(PERow, code);
//                                    ppp.ScannerOff = !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

//                                    if (ppp.Kod != string.Empty && !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
//                                    {
//                                        //Nezadani mnozstvi a pouziti viz vrchni konfigurace
//                                    }
//                                    else
//                                    {   //Je vyzadovano zadani mnozstvi ...
//                                        if (ppp.ShowDialog() == DialogResult.Cancel)
//                                            return;

//                                    }

//                                    qty = decimal.Parse(ppp.Kod);
//                                }
//                            }

//                        }
//                        else if (PERow.CZ_SerNum_Track == 1 || PERow.CZ_SerNum_Track == 2) //sledovano na seriova cisla nebo sarze
//                        {
//                            bool opakovat = true;
//                            while (opakovat)
//                            {
//                                opakovat = false;

//                                bool showdialogSN = true;

//                                if ((PERow.CZ_SerNum_Track == 2) && (code is BarcodeSlashSarze))
//                                {
//                                    sn = ((BarcodeSlashSarze)code).sarze;
//                                    showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
//                                }

//                                if (showdialogSN)
//                                {
//                                    //pppsn.Popis = ppp.Popis = (PERow.CZ_SerNum_Track == 1 ? "Seriové èíslo" : "Šarže");
//                                    //ppp.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
//                                    //pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
//                                    //pppsn.Len = ppp.Len = PERow.CZ_SerNum_Delka;
//                                    //pppsn.CheckLen = ppp.CheckLen = true;
//                                    //pppsn.AllowEmpty = ppp.AllowEmpty = false;
//                                    //pppsn.Kod = ppp.Kod = string.Empty;
//                                    pppsn.Popis = (PERow.CZ_SerNum_Track == 1 ? Fask.Localization.Localization.Prijem4PrijemListSerioveCislo : Fask.Localization.Localization.Prijem4PrijemListSarze);
//                                    pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
//                                    pppsn.Len = PERow.CZ_SerNum_Delka;
//                                    pppsn.CheckLen = true;
//                                    pppsn.AllowEmpty = Prijem_4.Globals.PovolitPrazdnouHodnotuSN;
//                                    pppsn.Kod = sn;     //string.Empty;

//                                    //vytazeni vsech seriovych cisel pro polozku z databaze ...
//                                    //pppsn.PESN = this._pesn_ta.GetDataByDavkaPolozka(PERow.CountEntries, PERow.ITEMNMBR);
//                                    pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

//                                    if (pppsn.ShowDialog() == DialogResult.Cancel)
//                                    {
//                                        #region Pri opusteni cteni SN upozornit na nedostatek
//                                        if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
//                                        { // test, zda je nasnimane pozadovane mnozstvi
//                                            //if (PERow.QTYSHPPD > this.Nacteno(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD))
//                                            if (PERow.QTYSHPPD > PERow.Nasnimano)
//                                            {
//                                                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
//                                                DialogResult dr = MessageBoxBig.Show(
//                                                    Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
//                                                    Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
//                                                     MessageBoxButtons.YesNo,
//                                                      MessageBoxBigIcon.Question
//                                                );
//                                                if (dr == DialogResult.No)
//                                                {
//                                                    opakovat = true;
//                                                    continue; //bude pokracovat znovu zadanim SN
//                                                }
//                                            }
//                                        }
//                                        #endregion
//                                        return;
//                                    }

//                                    sn = pppsn.Kod;
//                                }

//                                qty = 1;

//                                #region Test duplicity SN
//                                // 27.10.2017 JiS => SN ma smysl kontrolovat jen pokud jde o SN
//                                //                => Sarze duplicitni muze byt vzdy ...
//                                if ((PERow.CZ_SerNum_Track == 1) && !prijemDataParametry.Parametry[0].CONFIG_DUPLIC_SN) //test na duplicitu SN/Sarze (True=duplicita povolena)
//                                {
//                                    bool exist = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_Duplicita_SN_ByKey(prijemDataParametry.Parametry[0].CONFIG_PRIM_KEY1, PERow, sn);
//                                    if (exist)
//                                    {
//                                        if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Prijem4PrijemListChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
//                                            == DialogResult.Cancel)
//                                            return;
//                                        else
//                                        {
//                                            opakovat = true;
//                                            //break;
//                                        }
//                                    }
//                                }
//                                #endregion

//                                #region online funkce pro generovani sarze podle itemnmbr a zadane hodnoty serltnum
//                                if (Prijem_4.Globals.GenerovaniSarze)
//                                {
//                                    while (true)
//                                    {
//                                        string sn2 = string.Empty;
//                                        if (!OnlineGenerateSerltnum(PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR.Trim(), sn, skl_id, out sn2))
//                                        {
//                                            DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListGenerovaniCislaPaletyOpakovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
//                                            if (dr == DialogResult.Yes)
//                                                continue;
//                                            else
//                                                return;
//                                        }
//                                        else
//                                        {
//                                            sn = sn2;
//                                            break;
//                                        }
//                                    }
//                                }
//                                #endregion


//                            } //while (cist) ...

//                            if (PERow.CZ_SerNum_Track == 2)
//                            {
//                                if (Prijem_4.Globals.MnozstviAutoJedna)
//                                {
//                                    qty = 1;
//                                }
//                                else
//                                {
//                                    if (!Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
//                                    {
//                                        WeightCode wc = (WeightCode)code;

//                                        qty =
//                                            (wc.weight
//                                            / (PERow.IsWEIGHTNull() || (PERow.WEIGHT == 0) ? 1 : PERow.WEIGHT)
//                                            / ((PERow.QTYPACK == 0) ? 1 : PERow.QTYPACK)
//                                            );
//                                    }
//                                    else
//                                    {
//                                        //ppp.Popis = "Množství";
//                                        ppp.Popis = PERow.QTYPACK > 0 ? Fask.Localization.Localization.Prijem4PrijemListMnozstviBaleni : Fask.Localization.Localization.Prijem4PrijemListMnozstvi;// "Množství" + (PERow.QTYPACK > 0 ? " balení" : string.Empty);
//                                        ppp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
//                                        ppp.Len = 0;
//                                        ppp.Serltnum = sn;
//                                        ppp.CheckLen = false;
//                                        ppp.AllowEmpty = false;
//                                        ppp.Kod = mnozstviPredvyplnit(PERow, code);
//                                        ppp.ScannerOff = !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

//                                        if (ppp.Kod != string.Empty && !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
//                                        {
//                                            //Nezadani mnozstvi a pouziti viz vrchni konfigurace
//                                        }
//                                        else
//                                        {   //Je vyzadovano zadani mnozstvi ...
//                                            // TODO : zavira se automaticky s dialogresult != cancel...
//                                            // s dialog result NONE ???!!!!
//                                            DialogResult dpppres = ppp.ShowDialog();
//                                            if (dpppres == DialogResult.Cancel)
//                                                return;
//                                        }

//                                        qty = decimal.Parse(ppp.Kod);
//                                    }
//                                }
//                            }
//                        }
//                        // TODO : docasne zruseno kvuli obfuskaci ...
//                        #region Tisky vlastnich SN ...
//                        //else if (PERow.CZ_SerNum_Track == 5) //generuj SN
//                        //{
//                        //    PrijemGenerovatSNVybercs vyber = new PrijemGenerovatSNVybercs();
//                        //    if (vyber.ShowDialog() == DialogResult.Cancel)
//                        //        return false;

//                        //    if (vyber.GenerovatxSN == PrijemGenerovatSNVybercs.GenerovatSN.Vlastni)
//                        //    {
//                        //        PrijemGenerovaniVlastniSN vlastniSN = new PrijemGenerovaniVlastniSN();

//                        //        if (vlastniSN.ShowDialog() == DialogResult.Cancel)
//                        //            return false;

//                        //        seriova_cisla = vlastniSN.list_SN;

//                        //        try
//                        //        {
//                        //            plugins = new System.Collections.ArrayList();
//                        //            app = new ApplicationCore();

//                        //            LoadPlugins();

//                        //            if (plugins.Count > 0 && app.Dialog() == true)
//                        //            {
//                        //                TiskData data = new Fask.MST_W.TiskData();
//                        //                data.CountEntries = _davka.ToString(); //PERow.CountEntries.ToString();
//                        //                data.PONUMBER = PERow.PONUMBER;
//                        //                data.ORD = PERow.ORD.ToString();
//                        //                data.ITEMNMBR = PERow.ITEMNMBR;
//                        //                data.VNDDOCNM = PERow.VNDDOCNM;
//                        //                data.CZ_CarKod = PERow.CZ_CarKod;
//                        //                data.DAT_VYROBY = dv;
//                        //                data.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
//                        //                data.KOD_SW = sw;
//                        //                data.LOCNCODE = PERow.LOCNCODE;
//                        //                data.PONUMBER = PERow.PONUMBER;
//                        //                data.QTYPACK = PERow.QTYPACK.ToString();
//                        //                data.QTYSHPPD = PERow.QTYSHPPD.ToString();
//                        //                data.REZ_1 = rez1;
//                        //                data.REZ_2 = rez2;
//                        //                data.TIMEDONE = DateTime.Now.ToString("HHmmss");
//                        //                data.VNDITNUM = PERow.VNDITNUM;

//                        //                foreach (IPluginBase plugin in plugins)
//                        //                {
//                        //                    if (plugin.Tiskni(seriova_cisla.ToArray(), data, MST_W.MST_Global.TerminalID.ToString()) == true)
//                        //                    {
//                        //                        MessageBoxBig.Show("Úloha odeslána k tisku.");
//                        //                    }
//                        //                    break;
//                        //                }
//                        //            }
//                        //        }
//                        //        catch (Exception ex)
//                        //        {
//                        //            throw ex;
//                        //        }
//                        //    }
//                        //    else if (vyber.GenerovatxSN == PrijemGenerovatSNVybercs.GenerovatSN.Zakaznicke)
//                        //    {
//                        //        PrijemGenerovaniZakaznickeSN zakaznickeSN = new PrijemGenerovaniZakaznickeSN();
//                        //        if (zakaznickeSN.ShowDialog() == DialogResult.Cancel)
//                        //            return false;

//                        //        seriova_cisla = zakaznickeSN.list_SN;
//                        //    }

//                        //    qty = 1;
//                        //}
//                        #endregion
//                        else
//                        {
//                            MessageBox.Show(PERow.CZ_SerNum_Track.ToString(), "CZ_Sernum_Track");
//                            return;
//                        }




//                        #region Rohodovat Sklad Expedice Rozdelit
//                        if (Prijem_4.Globals.RozhodovatSkladExpedice)
//                        {

//                            decimal PrijimaneMnozstvi = qty;

//                            decimal MnozstviDodavatelePozadovano = 0; ;
//                            decimal MnozstviDodavateleDodano = 0;
//                            decimal MnozstviDodavateleDodat = 0;
//                            decimal MnozstviOdberateliPozadovano = 0;
//                            decimal MnozstviOdberatelumDodano = 0;
//                            decimal MnozstviOdberatelumDodat = 0;
//                            decimal Vysledek = 0;

//                            //OnlineGetSkladExpedice(PERow.ITEMNMBR, ppp.Kod, "0", PERow.SERLTNUM);
//                            OnlineGetSkladExpedice(
//                                                PERow.ITEMNMBR,
//                                                PrijimaneMnozstvi,
//                                                PERow.Nasnimano,
//                                                out  MnozstviDodavatelePozadovano,
//                                                out  MnozstviDodavateleDodano,
//                                                out  MnozstviDodavateleDodat,
//                                                out  MnozstviOdberateliPozadovano,
//                                                out  MnozstviOdberatelumDodano,
//                                                out  MnozstviOdberatelumDodat,
//                                                out  Vysledek
//                                                );

//                            decimal NaSklad = 0;
//                            decimal NaExpedici = 0;


//                            if (Vysledek >= PrijimaneMnozstvi)
//                            { // ma se jeste dodat "Vysledek" a prijimam mene nez dodavam ...
//                                NaExpedici = PrijimaneMnozstvi;
//                                NaSklad = 0;
//                                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundExpedice));
//                            }
//                            else if ((0 < Vysledek) && (Vysledek < PrijimaneMnozstvi))
//                            {
//                                NaExpedici = Vysledek;
//                                NaSklad = PrijimaneMnozstvi - Vysledek;
//                                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSkladExpedice));
//                            }
//                            else if (Vysledek <= 0)
//                            {
//                                NaExpedici = 0;
//                                NaSklad = PrijimaneMnozstvi;
//                                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSklad));
//                            }


//                            if (Prijem_4.Globals.ZobrazovatReport && !Prijem_4.Globals.MnozstviAutoJedna)
//                            {

//                                //TaD sledovani na mnozstvi 22.05.2018 Hanibal 
//                                using (FormReport frmrep = new FormReport())
//                                {
//                                    //frmrep.PERow = PERow;
									
//                                    frmrep.CZ_CarKod = PERow.CZ_CarKod;
//                                    frmrep.ITEMDESC = PERow.ITEMDESC;
//                                    frmrep.ITEMNMBR = PERow.ITEMNMBR;

//                                    //frmrep.ds = ds;
//                                    frmrep.NaExpedici = NaExpedici;
//                                    frmrep.NaSklad = NaSklad;

//                                    frmrep.MnozstviDodavatelePozadovano = MnozstviDodavatelePozadovano;
//                                    frmrep.MnozstviDodavateleDodano = MnozstviDodavateleDodano;
//                                    frmrep.MnozstviDodavateleDodat = MnozstviDodavateleDodat;
//                                    frmrep.MnozstviOdberateliPozadovano = MnozstviOdberateliPozadovano;
//                                    frmrep.MnozstviOdberatelumDodano = MnozstviOdberatelumDodano;
//                                    frmrep.MnozstviOdberatelumDodat = MnozstviOdberatelumDodat;


//                                    if (frmrep.ShowDialog() == DialogResult.Cancel)
//                                        return;

//                                }
//                            }
//                        }

//                        #endregion

//                        #region Fotky

//                        List<string> fotofilenames = new List<string>();
//                        // povoleni foceni
//                        if (Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
//                        {
//                            // odstraneni veskerych fotek, pokud drive byly nasnimany
//                            string[] fileNames = System.IO.Directory.GetFiles(Main.ImagesDir, @sn + "*.jpg");
//                            foreach (var item in fileNames)
//                            {
//                                File.Delete(item);
//                            }
//                            int pocet = 1;
//                            while (true)
//                            {
//                                // 18.5.2016 PeV: predelani na providera
//                                //using (SejmiImageForm sif = new SejmiImageForm())
//                                //{
//                                //    //sif.ImageFilename = sn.Trim() + "_" + pocet.ToString();
//                                //    sif.CustomFileName = sn.Trim() + "_" + pocet.ToString();
//                                //    DialogResult dr = sif.ShowDialog();
//                                //    if (dr == DialogResult.Cancel)
//                                //        break;
//                                //}

//                                DialogResult dr = Program.mstw.Photo.CaptureImage(sn + "_" + pocet.ToString());
//                                if (dr != DialogResult.OK)
//                                    break;

//                                // 18.5.2016 PeV: vysledny nazev souboru se prebira z providera
//                                //fotofilenames.Add(sn.Trim() + "_" + pocet.ToString() + ".jpg"); 
//                                fotofilenames.Add(Program.mstw.Photo.ImageFilename);
//                                pocet++;
//                            }
//                        }
//                        #endregion

//                        decimal mnozstvi = qty * (PERow.QTYPACK > 0 ? PERow.QTYPACK : 1);

//                        PIRow.QTYSHPPD = mnozstvi;
//                        PIRow.QTYSHPPDMJ = qty;
//                        PIRow.QTYPACK = PERow.QTYPACK;
//                        PIRow.SERLTNUM = sn;

//                        #region Test preplnenosti
//                        if (!Globals.OverFillItem)
//                        {
//                            Logging.Log.WriteDebug("OverFillItem Start", "PerformInsert");
//                            //decimal Quantity = this.Nacteno(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD);
//                            decimal Quantity = PERow.Nasnimano;
//                            if ((Quantity + mnozstvi) > PERow.QTYSHPPD)
//                            {
//                                DialogResult dres2 = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazanoOpakovatZadaniDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
//                                if (dres2 == DialogResult.Yes)
//                                    continue;
//                                else
//                                    return;
//                            }
//                        }
//                        #endregion
//                        #region Lokace
//                        //TODO udelat s tohoto metodu, použito vicekrat v kodu

//                        if (!Globals.ZadaniLocncodePredSN)
//                        {
//                            // povoleno zobrazeni dialogu pro zadani lokace v prijemparams nebo na terminalu
//                            if (prijemDataParametry.Parametry[0].CONFIG_SNIM_LOCNCODE || miRezimZadavaniLokace.Checked)
//                            {
//                                pzl.Popis = MST_Global.LC_NAME;
//                                pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
//                                pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
//                                //skf.Len = Globals.LOCNCODE_LEN;
//                                //skf.CheckLen = true;
//                                pzl.AllowEmpty = false;
//                                pzl.Kod = PERow.IsLOCNCODENull() ? string.Empty : PERow.LOCNCODE.Trim();
//                                pzl.perow = PERow;

//                                if (pzl.ShowDialog() == DialogResult.Cancel)
//                                    return;
//                                locncode = pzl.Kod;
//                                PIRow.LOCNCODE = locncode;
//                            }
//                        }

//                        #endregion
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 5" + swatch.Elapsed.ToString());
//#endif

//                        #region Zadani doplnujicich informaci
//                        if (PERow.CZ_SW_Track > 0)
//                        {
//                            skf.Popis = MST_Global.SWName;
//                            skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
//                            skf.Len = PERow.CZ_SW_Delka;
//                            skf.CheckLen = true;
//                            skf.AllowEmpty = false;
//                            //skf.MaxLength = (int)SqlCEDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["KOD_SW"].MaxLength;// _piTemp.KOD_SWColumn.MaxLength;
//                            skf.Kod = string.Empty;

//                            if (skf.ShowDialog() == DialogResult.Cancel)
//                                return;
//                            sw = skf.Kod;
//                            PIRow.KOD_SW = sw;
//                        }

//                        if (PERow.CZ_DatVyr_Track > 0)
//                        {
//                            skf.Popis = MST_Global.DVName;
//                            skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
//                            skf.Len = PERow.CZ_DatVyr_Delka;
//                            skf.CheckLen = true;
//                            skf.AllowEmpty = false;
//                            //skf.MaxLength = (int)SqlCEDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["DAT_VYROBY"].MaxLength;//_piTemp.DAT_VYROBYColumn.MaxLength;
//                            skf.Kod = string.Empty;

//                            if (skf.ShowDialog() == DialogResult.Cancel)
//                                return;
//                            dv = skf.Kod;
//                            PIRow.DAT_VYROBY = dv;
//                        }

//                        // 16.1.2017 JiS - uprava pro prijem LABARA dle predlohy
//                        // zadani rez1 dle polozky konfigurace na PE.CZ_REZ1_TRACK
//                        //if (prijemDataParametry.Parametry[0].CONFIG_SNIMAT_POL3)                    
//                        if (PERow.CZ_REZ1_TRACK > 0) //pozadovano zadani hodnoty rez1
//                        {
//                            skf.Popis = MST_Global.REZ1_PRIJ_NAME;
//                            skf.CodeType = Prijem_4.Globals.Rez1Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
//                            skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["REZ_1"].MaxLength;
//                            skf.CheckLen = !Prijem_4.Globals.Rez1Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
//                            skf.AllowEmpty = !Prijem_4.Globals.Rez1Povinne;
//                            skf.Kod = Prijem_4.Globals.Rez1Pamatovat ? Settings.PrijemRez1LastValue : string.Empty;

//                            if (skf.ShowDialog() == DialogResult.Cancel)
//                                return;
//                            rez1 = skf.Kod;
//                            PIRow.REZ_1 = rez1;
//                            if (Prijem_4.Globals.Rez1Pamatovat) Settings.PrijemRez1LastValue = rez1;
//                        }

//                        //16.1.2017 JiS pozadavek na zadani hodnoty rez2
//                        if (PERow.CZ_REZ2_TRACK > 0)
//                        {
//                            skf.Popis = MST_Global.REZ2_PRIJ_NAME;
//                            skf.CodeType = Prijem_4.Globals.Rez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
//                            skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["REZ_2"].MaxLength;
//                            skf.CheckLen = !Prijem_4.Globals.Rez2Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
//                            skf.AllowEmpty = !Prijem_4.Globals.Rez2Povinne;
//                            skf.Kod = Prijem_4.Globals.Rez2Pamatovat ? Settings.PrijemRez2LastValue : string.Empty;

//                            if (skf.ShowDialog() == DialogResult.Cancel)
//                                return;
//                            rez2 = skf.Kod;
//                            if (Prijem_4.Globals.Rez2Pamatovat) Settings.PrijemRez2LastValue = rez2;
//                        }

//                        #endregion

//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 6" + swatch.Elapsed.ToString());
//#endif
//                        Cursor.Current = Cursors.WaitCursor;

//                        #region sqlce insert

//                        //_davkasqlcecommand.CommandText = "select * from czmst_pi";
//                        //bool prevopened = _davkasqlcecommand.Connection.State == ConnectionState.Open;
//                        //if (!prevopened)
//                        //    _davkasqlcecommand.Connection.Open();
//                        //System.Data.SqlServerCe.SqlCeResultSet scerset = _davkasqlcecommand.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Updatable);
//                        //System.Data.SqlServerCe.SqlCeUpdatableRecord sceupd = scerset.CreateRecord();

//                        //sceupd["CountEntries"] = PERow.CountEntries;
//                        //sceupd["PONUMBER"] = PERow.PONUMBER;
//                        //sceupd["ORD"] = PERow.ORD;
//                        //sceupd["ITEMNMBR"] = PERow.ITEMNMBR;
//                        //sceupd["VNDDOCNM"] = PERow.VNDDOCNM;
//                        //sceupd["VNDITNUM"] = (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM;
//                        //sceupd["LOCNCODE"] = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
//                        //sceupd["MJ"] = PERow.IsMJNull() ? "" : PERow.MJ.Trim();
//                        //sceupd["QTYSHPPD"] = mnozstvi;
//                        //sceupd["QTYSHPPDMJ"] = qty;
//                        //sceupd["QTYPACK"] = PERow.QTYPACK;
//                        //sceupd["SERLTNUM"] = sn;
//                        //sceupd["KOD_SW"] = sw;
//                        //sceupd["DAT_VYROBY"] = dv;
//                        //sceupd["DATEDONE"] = DateTime.Now.ToString("yyyyMMdd");
//                        //sceupd["TIMEDONE"] = DateTime.Now.ToString("HHmmss");
//                        //sceupd["CZ_CarKod"] = PERow.CZ_CarKod;
//                        //sceupd["REZ_1"] = rez1;
//                        //sceupd["REZ_2"] = rez2;
//                        //sceupd["USER_ID"] = MST_Global.UserID;
//                        ////sceupd["DEX_ROW_ID"] = 0;
//                        //sceupd["guid"] = newguid = Guid.NewGuid();
//                        //sceupd["INPUT_MODE"] = _input_mode;
//                        //sceupd["ID_TERMINAL"] = MST_Global.TerminalID;
//                        //sceupd["WEIGHT"] = PERow.IsWEIGHTNull() ? 0 : PERow.WEIGHT;
//                        //sceupd["NMBRPAL"] = PERow.IsNMBRPALNull() ? string.Empty : PERow.NMBRPAL;
//                        //sceupd["TYPEPAL"] = PERow.IsTYPEPALNull() ? string.Empty : PERow.TYPEPAL;
//                        //sceupd["ITEMCODE"] = PERow.IsITEMCODENull() ? string.Empty : PERow.ITEMCODE;
//                        //sceupd["SKL_ID"] = skl_id;
//                        #endregion

//                        #region sqlite insert
//                        //_pi_ta.Insert(
//                        //    _davka,
//                        //    PERow.PONUMBER,
//                        //    PERow.ORD,
//                        //    PERow.ITEMNMBR,
//                        //    PERow.VNDDOCNM,
//                        //    (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM,
//                        //    (locncode.Length > 0) ? locncode : PERow.LOCNCODE,
//                        //    mnozstvi,
//                        //    PERow.QTYPACK,
//                        //    sn,
//                        //    sw,
//                        //    dv,
//                        //    DateTime.Now.ToString("yyyyMMdd"),
//                        //    DateTime.Now.ToString("HHmmss"),
//                        //    PERow.CZ_CarKod,
//                        //    rez1,
//                        //    rez2,
//                        //    MST_Global.UserID,
//                        //    Guid.NewGuid()
//                        //);
//                        #endregion
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 7" + swatch.Elapsed.ToString());
//#endif
//                        #region Online insert
//                        if (Globals.OnlinePohyby)
//                        {
//                            //priprava pro server
//                            PrijemService.Prijem dtPOnline = new Fask.MST_W.PrijemService.Prijem();
//                            PrijemService.Prijem.CZMST_PIRow rPOnline = dtPOnline.CZMST_PI.NewCZMST_PIRow();
//                            rPOnline.CountEntries = PERow.CountEntries;
//                            rPOnline.PONUMBER = PERow.PONUMBER;
//                            rPOnline.ORD = PERow.ORD;
//                            rPOnline.ITEMNMBR = PERow.ITEMNMBR;
//                            rPOnline.VNDDOCNM = PERow.VNDDOCNM;
//                            rPOnline.VNDITNUM = (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM;
//                            rPOnline.LOCNCODE = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
//                            rPOnline.MJ = PERow.IsMJNull() ? "" : PERow.MJ.Trim();
//                            rPOnline.QTYSHPPD = mnozstvi;
//                            rPOnline.QTYSHPPDMJ = qty;
//                            rPOnline.QTYPACK = PERow.QTYPACK;
//                            rPOnline.SERLTNUM = sn;
//                            rPOnline.KOD_SW = sw;
//                            rPOnline.DAT_VYROBY = dv;
//                            rPOnline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
//                            rPOnline.TIMEDONE = DateTime.Now.ToString("HHmmss");
//                            rPOnline.CZ_CarKod = PERow.CZ_CarKod;
//                            rPOnline.REZ_1 = rez1;
//                            rPOnline.REZ_2 = rez2;
//                            rPOnline.USER_ID = MST_Global.UserID;
//                            //sceupd["DEX_ROW_ID"] = 0;
//                            rPOnline.GUID = newguid;
//                            rPOnline.INPUT_MODE = _input_mode;
//                            rPOnline.ID_TERMINAL = MST_Global.TerminalID;
//                            rPOnline.DEX_ROW_ID = PERow.DEX_ROW_ID;
//                            if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
//                                rPOnline.SKL_ID = _sklad != null ? _sklad.skl_id : string.Empty;
//                            else
//                                rPOnline.SKL_ID = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;
//                            dtPOnline.CZMST_PI.AddCZMST_PIRow(rPOnline);

//                            while (true)
//                            {
//                                // ulozeni v pripade Online musi projit, protoze je to zavisle dale pri dohledavani
//                                // delaji se online dotazy na stav na serveru ... 
//                                try
//                                {
//                                    PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_Add(PERow.CountEntries, MST_Global.TerminalID, dtPOnline);
//                                    if (so.Exception || so.StatusText != "OK")
//                                    {
//                                        if (DialogResult.Cancel == MessageBoxBig.Show(
//                                            string.Format(Fask.Localization.Localization.Prijem4PrijemListUlozeniOnlineProblemOpakovatDotaz, so.StatusText),
//                                            Fask.Localization.Localization.Prijem4PrijemListUlozeniOnline,
//                                            MessageBoxButtons.RetryCancel,
//                                            MessageBoxBigIcon.Critical,
//                                            Color.Red))
//                                        {
//                                            return;
//                                        }
//                                    }
//                                    else
//                                    { //ulozeni se podarilo ... 
//                                        break;
//                                    }

//                                }
//                                catch (Exception e)
//                                {
//                                    if (DialogResult.Cancel == MessageBoxBig.Show(
//                                        string.Format(Fask.Localization.Localization.Prijem4PrijemListUlozeniOnlineProblemOpakovatDotaz, e.Message),
//                                        Fask.Localization.Localization.Prijem4PrijemListUlozeniOnline,
//                                         MessageBoxButtons.RetryCancel,
//                                          MessageBoxBigIcon.Critical,
//                                          Color.Red
//                                        ))
//                                    {
//                                        return;
//                                    }
//                                }
//                            }
//                        }
//                        #endregion
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 8" + swatch.Elapsed.ToString());
//#endif
//                        #region lokace
//                        // online ulozeni do lokacniho mechanismu ... probiha pouze v pripade, ze je vypnute zalokovani
//                        if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT && !Prijem_4.Globals.PovolitZalokovani)
//                        {
//                            Cursor.Current = Cursors.WaitCursor;
//                            Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
//                            pohybrow.ITEMNMBR = PERow.ITEMNMBR;
//                            pohybrow.DOCUMENT_NUMBER = PERow.PONUMBER;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
//                            pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.P;  // prijem
//                            pohybrow.POHYB_SRC = "P";
//                            pohybrow.SOURCE = "T";      // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
//                            pohybrow.QTYSHPPD = (decimal)mnozstvi;
//                            pohybrow.SERLTNUM = sn;
//                            if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
//                                pohybrow.SKL_ID_SRC = _sklad != null ? _sklad.skl_id : string.Empty;
//                            else
//                                pohybrow.SKL_ID_SRC = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;

//                            //pohybrow.SKL_ID_SRC = VIRow.IsSKL_IDNull() ? string.Empty : VIRow.SKL_ID;
//                            pohybrow.SKL_ID_DST = string.Empty;
//                            pohybrow.LOCNCODE_SRC = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
//                            pohybrow.LOCNCODE_DST = string.Empty;
//                            pohybrow.UserID = MST_Global.UserID;
//                            pohybrow.TermID = MST_Global.TerminalID;
//                            pohybrow.guid = newguid;
//                            //pohybrow.dateeveS = ...   // datum serveru se vyplnuje az na serveru
//                            pohybrow.Expiration = null;
//                            pohybrow.ITEMDESC = PERow.IsITEMDESCNull() ? string.Empty : PERow.ITEMDESC;
//                            pohybrow.CountEntries = PERow.CountEntries;
//                            pohybrow.dateeveT = DateTime.Now;   // datum terminalu

//                            try
//                            {
//                                Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - start", "LocationLog");
//                                Classes.LokaceLog.writeBody(pohybrow);

//                                Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.AddRecord(pohybrow);
//                                Cursor.Current = Cursors.Default;
//                                switch (sl.State)
//                                {
//                                    case Fask.MST_W.LokaceService.States.OK:
//                                        break;
//                                    case Fask.MST_W.LokaceService.States.ERROR:
//                                        MessageBoxBig.Show("Nepodaøilo se pøidat záznam lokace, záznam nebude pøidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
//                                        return;
//                                    default:
//                                        MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se pøidat záznam do lokací. Záznam nebude pøidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
//                                        return;
//                                }

//                                Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - end", "LocationLog");
//                            }
//                            catch (Exception ex)
//                            {
//                                Logging.Log.WriteDebug(ex.Message);
//                                Cursor.Current = Cursors.Default;
//                                if (MessageBoxBig.Show(ex.Message + "\nPøejete si pøesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
//                                {
//                                    //Promenna ridici cyklus
//                                    bool state = true;

//                                    //Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
//                                    while (state)
//                                    {
//                                        try
//                                        {
//                                            // Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
//                                            // Volani sluzby pro odstraneni a kontrola navratveho stavu.
//                                            Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

//                                            Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.PRIJEM);
//                                            DialogResult dr = DialogResult.No;
//                                            switch (sl.State)
//                                            {
//                                                case Fask.MST_W.LokaceService.States.OK:
//                                                    dr = DialogResult.Yes;
//                                                    break;
//                                                case Fask.MST_W.LokaceService.States.ERROR:
//                                                    dr = MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n" + sl.ErrorMessage + "\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
//                                                    return;
//                                                default:
//                                                    dr = MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznam v lokaèním systému!\n\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
//                                                    return;
//                                            }

//                                            Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

//                                            // pokud ho chce ulozit, odejde z cyklu
//                                            if (dr == DialogResult.Yes)
//                                                break;
//                                        }
//                                        //Nejaka online chyba
//                                        catch (Exception exex)
//                                        {
//                                            Logging.Log.Write("Chyba pøi mazání lokací : " + exex.Message);
//                                            Cursor.Current = Cursors.Default;
//                                            if (MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n'" + exex.Message + "'\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
//                                            {
//                                                //Ukonceni cyklu - chce zaznam ulozit
//                                                break;
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        #endregion
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 9" + swatch.Elapsed.ToString());
//#endif
//                        //pokud je online insert, tak musi projit online insert !!!
//                        // prida vydanou polozku do tabulky
//                        while (true)
//                        {
//                            try
//                            {
//                                //scerset.Insert(sceupd);
//                                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt_pi = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
//                                var piN = dt_pi.NewCZMST_PIRow();

//                                piN.CountEntries = PERow.CountEntries;
//                                piN.PONUMBER = PERow.PONUMBER;
//                                piN.ORD = PERow.ORD;
//                                piN.ITEMNMBR = PERow.ITEMNMBR;
//                                piN.VNDDOCNM = PERow.VNDDOCNM;
//                                piN.VNDITNUM = (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM;
//                                piN.LOCNCODE = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
//                                piN.MJ = PERow.IsMJNull() ? "" : PERow.MJ.Trim();
//                                piN.QTYSHPPD = mnozstvi;
//                                piN.QTYSHPPDMJ = qty;
//                                piN.QTYPACK = PERow.QTYPACK;
//                                piN.SERLTNUM = sn;
//                                piN.KOD_SW = sw;
//                                piN.DAT_VYROBY = dv;
//                                piN.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
//                                piN.TIMEDONE = DateTime.Now.ToString("HHmmss");
//                                piN.CZ_CarKod = PERow.CZ_CarKod;
//                                piN.REZ_1 = rez1;
//                                piN.REZ_2 = rez2;
//                                piN.USER_ID = MST_Global.UserID;
//                                piN.DEX_ROW_ID = PERow.DEX_ROW_ID;
//                                piN.guid = newguid = Guid.NewGuid();
//                                piN.INPUT_MODE = _input_mode;
//                                piN.ID_TERMINAL = MST_Global.TerminalID;
//                                piN.WEIGHT = PERow.IsWEIGHTNull() ? 0 : PERow.WEIGHT;
//                                piN.NMBRPAL = PERow.IsNMBRPALNull() ? string.Empty : PERow.NMBRPAL;
//                                piN.TYPEPAL = PERow.IsTYPEPALNull() ? string.Empty : PERow.TYPEPAL;
//                                piN.ITEMCODE = PERow.IsITEMCODENull() ? string.Empty : PERow.ITEMCODE;
//                                piN.SKL_ID = skl_id;
                                
//                                dt_pi.AddCZMST_PIRow(piN);
//                                //_pi_ta.Update(piN);
//                                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Update_PI(piN);
//                                break;
//                            }
//                            catch (Exception ex)
//                            {
//                                Logging.Log.Write("pita.insert," + ex.Message, "Prijem");
//                                if (DialogResult.Yes != MessageBoxBig.Show(ex.Message + "\n\nPøejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
//                                {
//                                    #region lokace
//                                    // pokud ne, dojde online odmazani ...
//                                    if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT && !Prijem_4.Globals.PovolitZalokovani)
//                                    {
//                                        //Promenna ridici cyklus
//                                        bool state = true;

//                                        //Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
//                                        while (state)
//                                        {
//                                            try
//                                            {
//                                                //Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
//                                                //Volani sluzby pro odstraneni a kontrola navratveho kodu.
//                                                Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newguid.ToString(), "LocationLog");

//                                                Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.DeleteRecordByGuid(newguid, Fask.MST_W.LokaceService.ModulName.PRIJEM);
//                                                DialogResult dr = DialogResult.No;
//                                                switch (sl.State)
//                                                {
//                                                    case Fask.MST_W.LokaceService.States.OK:
//                                                        dr = DialogResult.Yes;
//                                                        break;
//                                                    case Fask.MST_W.LokaceService.States.ERROR:
//                                                        dr = MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n'" + sl.ErrorMessage + "'\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
//                                                        break;
//                                                    default:
//                                                        dr = MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznam v lokaèním systému!\n\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
//                                                        break;
//                                                }

//                                                Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

//                                                // pokud ho chce ulozit, odejde z cyklu
//                                                if (dr == DialogResult.Yes)
//                                                    break;
//                                            }
//                                            //Nejaka online chyba
//                                            catch (Exception exex)
//                                            {
//                                                Logging.Log.Write("Chyba pøi mazání lokací : " + exex.Message);
//                                                Cursor.Current = Cursors.Default;
//                                                //if (MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n" + exex.Message + "\nPøejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
//                                                if (MessageBoxBig.Show("Nepodaøilo se odstranit záznam v lokaèním systému!\n" + exex.Message + "\nPøejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
//                                                {
//                                                    //Ukonceni cyklu - chce zaznam ulozit
//                                                    break;
//                                                }
//                                            }
//                                        }
//                                    }
//                                    else
//                                    {
//                                        // chyba pri ulozeni, pokud je lokacni mechanismus vypnuty
//                                        throw ex;
//                                    }
//                                    #endregion
//                                }
//                            }
//                        }

//                        // ulozeni fotek do DB
//                        foreach (var imgname in fotofilenames)
//                        {
//                            Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Insert_PIF(imgname, newguid);
//                        }

//                        //if (!prevopened)
//                        //    _davkasqlcecommand.Connection.Close();
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 10" + swatch.Elapsed.ToString());
//#endif
//                        //UpdateMnozstviAddValue(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD, mnozstvi);
//                        //PERow.Nasnimano += mnozstvi; //oprava aktualizace zbyvajiciho mnozstvi ve vnitrnim kolecku ...
//                        PERow.Nasnimano += mnozstvi; //oprava aktualizace zbyvajiciho mnozstvi ve vnitrnim kolecku ...
//                        //UpdateMnozstviAddValue(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD, mnozstvi);
//                        UpdateMnozstvi(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD, PERow.Nasnimano);
//                        Cursor.Current = Cursors.Default;
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 11" + swatch.Elapsed.ToString());
//#endif
//                        if (!prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT)
//                        {
//                            if (MST_Global.PrijemTimeDialog)
//                                MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListPolozkaUspesneZapsana, PERow.ITEMDESC.Trim()), Fask.Localization.Localization.Prijem4PrijemListVlozeni, MessageBoxButtons.OK, MessageBoxBigIcon.Information, Color.Green);
//                            else
//                                MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListPolozkaUspesneZapsana, PERow.ITEMDESC.Trim()), Fask.Localization.Localization.Prijem4PrijemListVlozeni, MessageBoxButtons.OK, MessageBoxBigIcon.Information, Color.Green);
//                        }
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 12" + swatch.Elapsed.ToString());
//#endif

//                        TiskEtikety(PERow, newguid);

//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 13" + swatch.Elapsed.ToString());
//#endif
//                        #region Kontrola uplnosti

//                        bool succ = true;
//                        if (kontrolaDokoncenosti(prijemDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI))
//                        {
//                            odeslatAktualniDavku();
//                            return;
//                        }
//                        #endregion

//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 14" + swatch.Elapsed.ToString());
//#endif
//                        DalsiSN = !prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE;

//                        // zkontroluje, zda je nacten pozadovany pocet
//                        if (PERow.Nasnimano < PERow.QTYSHPPD)
//                            continue;   // jeste neni nasnimane vse

//                        if (DalsiSN == false)
//                            continue;
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 15" + swatch.Elapsed.ToString());
//#endif

//                        #region Kontrola uplnosti polozky
//                        // dohledani poctu, pokud je predloha, existuje konf.soubor a je nastaven
//                        // odpovidajici parametr
//                        if (prijemDataParametry.Parametry[0].CONFIG_POKRDOHLED)
//                        {
//                            //decimal Quantity = this.Nacteno(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD);
//                            decimal Quantity = PERow.Nasnimano;
//                            if (Quantity >= PERow.QTYSHPPD)
//                            {
//                                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "chimes.wav"));
//                                // podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
//                                DialogResult dres = MessageBoxBig.Show(
//                                    (Quantity > PERow.QTYSHPPD ? Fask.Localization.Localization.Prijem4PrijemListPolozkaPreplnenaPokracovaniDotaz : Fask.Localization.Localization.Prijem4PrijemListPolozkaKompletniPokracovaniDotaz),
//                                    Fask.Localization.Localization.Prijem4PrijemListKontrolaUplnosti,
//                                    MessageBoxButtons.YesNo,
//                                    MessageBoxBigIcon.Question
//                                    );
//                                if (dres == DialogResult.No)
//                                {
//                                    return;
//                                }

//                                if (!Globals.OverFillItem)
//                                {
//                                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazano, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
//                                    return;
//                                }

//                            } // if (sinstruct.Quantity <=
//                        } // if((CONFIG_POKRDOHLED &
//                        #endregion

//                        //DalsiSN = !prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE;
//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 16" + swatch.Elapsed.ToString());
//#endif
//                        #region Upozornit na prebytek
//                        // TODO : upozornit na prebytek...
//                        #endregion

//#if DEBUG
//                        System.Diagnostics.Debug.WriteLine("PerformInsert: 17" + swatch.Elapsed.ToString());
//#endif
//                    } // while(DalsiSN)

//                }
//                catch (Exception ex)
//                {
//                    Logging.Log.WriteDebug(ex.Message, "Prijem_3.PerformInsert");
//                    if (MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical) == DialogResult.OK) { }

//                    return;
//                }
//                finally
//                {
//                    ScannerStart();
//                    UpdateForm();
//                    this.Show();
//#if DEBUG
//                    swatch.Stop();
//                    System.Diagnostics.Debug.WriteLine("PerformInsert trval: " + swatch.Elapsed.ToString());
//#endif
//                }
//            }
//            finally
//            {
//                if ((skf != null) && (!skf.IsDisposed))
//                {
//                    skf.Dispose();
//                    skf = null;
//                }

//                if ((pzl != null) && (!pzl.IsDisposed))
//                {
//                    pzl.Dispose();
//                    pzl = null;
//                }

//                if ((ppp != null) && (!ppp.IsDisposed))
//                {
//                    ppp.Dispose();
//                    ppp = null;
//                }

//                if ((pppsn != null) && (!pppsn.IsDisposed))
//                {
//                    pppsn.Dispose();
//                    pppsn = null;
//                }
//            }
//        }

        /// <summary>
        /// Odesle aktualni davku
        /// </summary>
        /// <returns>True=davka odeslana a jiz neexistuje, False=davka neodeslana a existuje, pokud je sloucena, mohl byt nejaky problem</returns>
        private bool odeslatAktualniDavku()
        {
            string aktualniDavakaTmp = Prijem_4.PrijemMain.prijemInstance.globalObject.Davka;
            string aktualniDavkaFileNameTmp = Prijem_4.PrijemMain.prijemInstance.globalObject.DavkaFileNameFullPath;
            Prijem_4.PrijemMain.prijemInstance.globalObject.Davka = null; //uvolneni datoveho souboru davky
            if (Prijem_4.PrijemMain.prijemInstance.odesliHotovouDavku(aktualniDavkaFileNameTmp))
            {
                this.finalize();
                DialogResult = DialogResult.OK;
                return true;
            }
            else
            {
                Prijem_4.PrijemMain.prijemInstance.globalObject.Davka = aktualniDavakaTmp; // zpatky nastavit
                return false;
            }
        }



        /// <summary>
        /// Zaktualizuje mnozstvi v nactenych datech podle klice PONUMBE, ITEMNMBR, ORD navysenim hodnoty o mnozstvi(+,-)
        /// </summary>
        /// <param name="mnozstvi">hodnota o kterou se ma mnozstvi navysit/ponizit(+/-)</param>
        public void UpdateMnozstviAddValue(string itemnmbr, string ponumber, int ord, decimal mnozstvi)
        {
            int pocet = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.UpdateNasnimanoAddValue_PE(mnozstvi, ponumber, itemnmbr, ord);
            if (pocet == 0)
            {
                Logging.Log.Write("Prijem neaktualizoval hodnotu Nasnimano u polozky:" + ponumber + "," + itemnmbr + "," + ord, "Prijem.UpdateNasnimano");
                MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListAktualizaceNasnimanehoMnozstviProblem, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[] perows = FindedPE(itemnmbr, ponumber, ord);
            foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow in perows)
            {
                prow.Nasnimano += mnozstvi;
            }
            prijem.CZMST_PE.AcceptChanges();
            UpdateForm();
        }
        /// <summary>
        /// Zaktualizuje mnozstvi v nactenych datech podle klice PONUMBE, ITEMNMBR, ORD na hodnotu mnozstvi
        /// </summary>
        /// <param name="mnozstvi">mnozstvi na ktere se nastavi</param>
        public void UpdateMnozstvi(string itemnmbr, string ponumber, int ord, decimal mnozstvi)
        {
            int pocet = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.UpdateNasnimano_PE(mnozstvi, ponumber, itemnmbr, ord);
            if (pocet == 0)
            {
                Logging.Log.Write("Prijem neaktualizoval hodnotu Nasnimano u polozky:" + ponumber + "," + itemnmbr + "," + ord, "Prijem.UpdateNasnimano");
                MessageBoxBigTimeout.Show(Fask.Localization.Localization.Prijem4PrijemListAktualizaceNasnimanehoMnozstviProblem, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[] perows = FindedPE(itemnmbr, ponumber, ord);
            foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow in perows)
            {
                prow.Nasnimano = mnozstvi;
            }
            prijem.CZMST_PE.AcceptChanges();
            UpdateForm();
        }


        /// <summary>
        /// Vyhleda vsechny nactene zaznamy dle itemnmbr, ponumber, ord
        /// </summary>
        /// <param name="itemnmbr">cislo polozky</param>
        /// <param name="ponumber">cislo objednavky</param>
        /// <param name="ord">poradi v objednavce</param>
        /// <returns>pole nalezenych odpovidajicich zobrazenych zaznamu</returns>
        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[] FindedPE(string itemnmbr, string ponumber, int ord)
        {
            //return (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow[])prijem.CZMST_PE.Select(
            //        prijem.CZMST_PE.ITEMNMBRColumn.ColumnName + "='" + itemnmbr + "'" +
            //        " AND " + prijem.CZMST_PE.PONUMBERColumn.ColumnName + "='" + ponumber + "'" +
            //        " AND " + prijem.CZMST_PE.ORDColumn.ColumnName + "=" + ord);
            return prijem.CZMST_PE.Where(x => x.ITEMNMBR == itemnmbr && x.PONUMBER == ponumber && x.ORD == ord).ToArray();
        }

        /// <summary>
        /// Provede kontrolu dokoncenosti davky
        /// </summary>
        /// <returns>True: dokonceno(ukoncit), False: nedokonceno(pokracovat)</returns>
        public bool kontrolaDokoncenosti(bool kontrola_dokoncenosti)
        {
            //if (prijemDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI)
            if(kontrola_dokoncenosti)
            {// kontrola dokoncenosti prijmu
                try
                {
                    #region Memory varianta

                    Cursor.Current = Cursors.WaitCursor;
                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow = null;
                    for (int i = 0; i < prijem.CZMST_PE.Count; i++)
                    {
                        perow = prijem.CZMST_PE[i];
                        if (perow.Zbyva > 0)
                            return false;
                    }
                    Cursor.Current = Cursors.Default;

                    //Pokud dojde az sem, tak je vse nasnimano ...
                    /*
                    DialogResult dr =
                        MessageBoxBig.Show("Dávka je kompletní.\nChcete pokraèovat ve zpracovávání této dávky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        PerformOK();
                        return true; //dokonceno, ukoncit
                    }
                    */

                    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListDavkaKompletniOdeslatDotaz, Fask.Localization.Localization.Prijem4PrijemListDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                        == DialogResult.Yes)
                    {
                        return true;
                    }

                    #endregion

                    //#region Sqlce varianta
                    //int? zbyvaPolozek = _q_ta.ZbyvaPolozek2() ?? 0;
                    //if (!zbyvaPolozek.HasValue)
                    //{
                    //    MessageBoxBigTimeout.Show("Nepodaøilo se provést kontrolu dokonèenosti", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    //    return false;
                    //}
                    //else if ((zbyvaPolozek ?? 0) == 0)
                    //{
                    //    DialogResult dr =
                    //        MessageBoxBig.Show("Dávka je kompletní.\n\nChcete pokraèovat? ve zpracovávání této dávky?", "Dávka kompletní", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    //    if (dr == DialogResult.No)
                    //    {
                    //        PerformOK();
                    //        return true; //dokonceno, ukoncit
                    //    }
                    //}
                    //else
                    //    return false; //neuplne
                    //#endregion

                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }

            return false; //nedokonceno, pokracovat
        }

        private void PrijemList_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
            this.ScannerStop();
        }

        private void UpdateForm()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Prijem4.PrijemList.UpdateForm");
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            swatch.Start();
            System.Diagnostics.Debug.WriteLine("UpdateForm(): 1" + swatch.Elapsed.ToString());
#endif
            labelItemDesc.Text =
            labelItemNmbr.Text =
            labelBARCODE.Text =
            labelPONUMBER.Text =
            labelQTYPACK.Text =
            labelQTYSHPPD.Text =
            labelCZ_SerNum_Track.Text =
            labelCZ_DatVyr_Track.Text =
            labelCZ_SW_Track.Text =
            labelNacteno.Text = 
            labelWEIGHT.Text = 
            labelNmbrpal.Text = 
            labelMJ.Text = "-";
            try
            {
                labelItemDesc.Text = SelectedPE.ITEMDESC.Trim();
                //labelItemNmbr.Text = SelectedPE.ITEMNMBR.Trim();
                labelItemNmbr.Text = SelectedPE.IsITEMCODENull() ? "-" : SelectedPE.ITEMCODE + " (" + SelectedPE.ITEMNMBR.Trim() + ")";
                labelBARCODE.Text = SelectedPE.VNDITNUM.Trim();
                labelPONUMBER.Text = SelectedPE.PONUMBER.Trim();
                labelQTYPACK.Text = SelectedPE.QTYPACK.ToString(Settings.UIFormatDesCisel);
                labelQTYSHPPD.Text = SelectedPE.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
                labelCZ_SerNum_Track.Text = SelectedPE.CZ_SerNum_Track.ToString();
                labelCZ_DatVyr_Track.Text = SelectedPE.CZ_DatVyr_Track.ToString();
                labelCZ_SW_Track.Text = SelectedPE.CZ_SW_Track.ToString();
                labelNacteno.Text = SelectedPE.Nasnimano.ToString(Settings.UIFormatDesCisel); //this.NactenoCelkem(SelectedPE.ITEMNMBR, SelectedPE.PONUMBER, SelectedPE.ORD).ToString("0.00");
                labelMJ.Text = SelectedPE.MJ.Trim();
                labelWEIGHT.Text = SelectedPE.IsWEIGHTNull() ? "-" : SelectedPE.WEIGHT.ToString(Settings.UIFormatDesCisel);
                labelNmbrpal.Text = SelectedPE.IsNMBRPALNull() ? "-" : SelectedPE.NMBRPAL.Trim();
            }
            catch
            {
            }

            
#if DEBUG
            System.Diagnostics.Debug.WriteLine("UpdateForm(): 2" + swatch.Elapsed.ToString());
#endif

            UpdateStatusBar();

#if DEBUG
            System.Diagnostics.Debug.WriteLine("UpdateForm(): end" + swatch.Elapsed.ToString());
#endif
        }

        private void UpdateStatusBar()
        {
            string tStatus = string.Empty;

            //if (String.IsNullOrEmpty(cZMSTPEBindingSource.Filter))
            //{
            //    tStatus += "F:V";
            //}
            //else
            //{
            //    tStatus += "F:N";
            //}
            // 19.7.2016 PeV: predelano na enum
            if (FiltrZobrazeni == AktivniFiltrZobrazeni.Vse)
            {
                tStatus += "F:V";
            }
            else
            {
                tStatus += "F:N";
            }

            tStatus += ",Ø:";
            if (statusInfoRazeni == string.Empty)
            {
                tStatus += "-";
            }
            else
            {
                tStatus += statusInfoRazeni;
            }

            if (Prijem_4.Globals.PovolitZmenuRezimuZadaniLokace)
            {
                if (miRezimZadavaniLokace.Checked)
                {
                    // zaskrnuto
                    // -> 1) vypnuti prijmove lokace
                    // -> 2) vypnuti lokace na davku (nastavit string.empty)
                    // -> 3) zobrazit dialog zadani lokace
                    tStatus += ",R:2";
                }
                else
                {
                    // standartni ... 
                    // nezaskrnuto
                    // -> 1) vyber prijmove lokace
                    // -> 2) vypnuti lokace na davku (nastavit string.empty)
                    // -> 3) nezobrazit dialog zadani lokace
                    tStatus += ",R:1";
                }
            }

            if (miNastavitLokaci.Checked && !string.IsNullOrEmpty(locncodeNaDavku))
            {
                tStatus += ",L:" + locncodeNaDavku;
            }
            else if (prijmovalokace != null && !prijmovalokace.IsLOCNCODENull())
            {
                tStatus += ",PL:" + prijmovalokace.LOCNCODE.Trim();
            }

			if (Paleta != null)
			{
				if (!string.IsNullOrEmpty(Paleta.sscc))
				{
					tStatus += ",Pal";
				} 
			}

            //Status rezimu zadavani mnozstvi
            //1 - zadava mnozstvi po jednom
            //0- vyzaduje zadavat mnozstvi
            if(Prijem_4.Globals.MnozstviAutoJedna)
                tStatus += ",S:1";
            else
                tStatus += ",S:0";

            statusBar.Text = tStatus;
        }


        private void menuItem2_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void menuItemPridat_Click(object sender, EventArgs e)
        {
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_ENTER);
            PerformInsert();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            PerformShowInserted();
        }

        private void PerformShowInserted()
        {
            try
            {
                ScannerStop();

                using (PrijemNasnimane pnas = new PrijemNasnimane(prijemDataParametry))
                {
                    pnas.FormPrijemList = this;
                    pnas.ShowDialog();
                }
            }
            catch
            {
            }
            finally
            {
                ScannerStart();
            }
            UpdateForm();
        }

        private void miZobrazitList_Click(object sender, EventArgs e)
        {
            ChangeRezim();
        }

        // TODO : docasne zruseno kvuli obfuskaci ...
        #region Tiskovy modul pluginy
        ///// <summary>   
        ///// Naètení pluginù.   
        ///// </summary>   
        //private void LoadPlugins()
        //{
        //    string cesta = MST_W.Main.KnihovnaTisk;
        //    try
        //    {
        //        if (File.Exists(cesta))
        //        {
        //            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(cesta);
        //            IPluginBase plugin = (IPluginBase)asm.CreateInstance("Fask.Print.Plugin");

        //            if (plugin != null)
        //                plugins.Add(plugin);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBoxBig.Show("Chyba pøi naèítání pluginu " + MST_W.Main.KnihovnaTisk + ": " + e.Message);
        //    }

        //    foreach (IPluginBase plugin in plugins)
        //    {
        //        plugin.Load(app);
        //    }
        //}

        ///// <summary>   
        ///// Rozhraní všech pluginù.    
        ///// </summary>   
        //public interface IPluginBase
        //{
        //    void Load(IApplicationBase app);

        //    bool Tiskni(string[] SN, Fask.MST_W.TiskData zaznamy, string idterminal);

        //    string Nazev
        //    {
        //        get;
        //    }

        //    string Titulek
        //    {
        //        get;
        //    }
        //}

        //public interface IApplicationBase
        //{
        //    bool Dialog();
        //}

        ///// <summary>   
        ///// Trida pro komunikaci pluginu s jádrem.   
        ///// </summary>   
        //public class ApplicationCore : IApplicationBase
        //{
        //    public ApplicationCore()
        //    {

        //    }

        //    public bool Dialog()
        //    {
        //        while (MessageBoxBig.Show("Chcete vytisknout SN?", "Print", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //        {
        //        }

        //        return true;
        //    }
        //}
        #endregion


        private void menuItem7_Click(object sender, EventArgs e)
        {
            //ZobrazVseNeuplne();
            FiltrZobrazeni = (FiltrZobrazeni == AktivniFiltrZobrazeni.Neuplne) ? AktivniFiltrZobrazeni.Vse : AktivniFiltrZobrazeni.Neuplne;
        }

        //private void ZobrazVseNeuplne(string filter)
        //{
        //    if (string.IsNullOrEmpty(filter))
        //        cZMSTPEBindingSource.Filter = string.Empty;
        //    else
        //    {
        //        try { cZMSTPEBindingSource.Filter = filter; }
        //        catch { }
        //    }
        //    UpdateForm();
        //}
        //private void ZobrazVseNeuplne(bool filter)
        //{
        //    //if (filter)
        //    //    cZMSTPEBindingSource.Filter = "Zbyva<>0";
        //    //else
        //    //    cZMSTPEBindingSource.Filter = string.Empty;
        //    if (filter)
        //        cZMSTPEBindingSource.Filter = "Zbyva<>0" + (string.IsNullOrEmpty(nmbrpal) ? string.Empty : (" AND NMBRPAL='" + nmbrpal + "'"));
        //    else
        //        cZMSTPEBindingSource.Filter = string.IsNullOrEmpty(nmbrpal) ? string.Empty : (" NMBRPAL='" + nmbrpal + "'");


        //    UpdateForm();
        //}

        //private void ZobrazVseNeuplne()
        //{
        //    //if (String.IsNullOrEmpty(cZMSTPEBindingSource.Filter))
        //    //    cZMSTPEBindingSource.Filter = "Zbyva<>0";
        //    //else
        //    //    cZMSTPEBindingSource.Filter = string.Empty;
        //    //UpdateForm();
        //    ZobrazVseNeuplne(String.IsNullOrEmpty(cZMSTPEBindingSource.Filter));
        //}

        private void dataGrid1_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
            NajdiPolozkuCarovyKod();
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            PerformDelete();
        }

        private void PerformDelete()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow prow = this.SelectedPE;

            try
            {
                ScannerStop();
                
                if (prow == null)
                    return;

                if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListSmazatNasnimaneMnozstviDotaz, prow.ITEMDESC.Trim(), prow.Nasnimano.ToString(Settings.UIFormatDesCisel)), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;

                if (Globals.OnlinePohyby)
                {
                    decimal nasnimano = prow.Nasnimano;
                    decimal odmazat = 0;
                    // TODO : online smazani ...
                    // pridat parametr Online akce do konfigurace 
                    // musi se vratit uspech z online funkce 
                    // vytahnout data z davky, ktera se maji mazat, staci v podstate jen guid...???
                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt_pi = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByKey_PI(prow.CountEntries, prow.PONUMBER, prow.ORD, prow.ITEMNMBR);
                    //priprava pro server
                    PrijemService.Prijem dtPOnline = new Fask.MST_W.PrijemService.Prijem();
                    dtPOnline.CZMST_PE.ImportRow(prow);
                    foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi in dt_pi)
                    {
                        odmazat += pi.QTYSHPPD;
                        dtPOnline.CZMST_PI.ImportRow(pi);
                    }

                    PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_Del(prow.CountEntries, MST_Global.TerminalID, dtPOnline);
                    if (so.Exception || so.StatusText != "OK")
                    {
                        MessageBoxBig.Show(so.StatusText, Fask.Localization.Localization.Prijem4PrijemListOnlineMazaniDat, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }

                    Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.DeletePI_Queries(prow.CountEntries, prow.ITEMNMBR, prow.PONUMBER, prow.ORD);
                    UpdateMnozstvi(prow.ITEMNMBR, prow.PONUMBER, prow.ORD, nasnimano-odmazat);
                }
                else
                {
                    // je povoleno foceni, dojde ke smazani fotek souvisejicich s prijemkou
                    if(Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
                    {
                        // najitivsech zaznamu pro odstraneni
                        var deletedpi_data = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByCntItemnmbrPonmbrOrd_PI(prow.CountEntries, prow.ITEMNMBR, prow.PONUMBER, prow.ORD);
                        foreach (var pirow in deletedpi_data)
                        {
                            Guid deletedGuid = pirow.guid;
                            var deletedpif_data = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByGUID_PIF(deletedGuid);
                            // smazani fotek
                            foreach (var pifrow in deletedpif_data)
                            {
                                string filepath = Path.Combine(Main.ImagesDir, pifrow.IMG_NAME.Trim());

                                if (File.Exists(filepath))
                                    File.Delete(filepath);
                            }
                            Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.DeleteByGUID_PIF(deletedGuid);
                        }
                    }

                    // odstraneni z lokacniho mechanismu
                    // zacatek
                    if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
                    {
                        #region lokace
                        var sidata = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByCntItemnmbrPonmbrOrd_PI(prow.CountEntries, prow.ITEMNMBR, prow.PONUMBER, prow.ORD);
                        foreach (var siitem in sidata)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            //Volani sluzby a kontrola navratveho kodu.
                            Logging.Log.WriteAdvanced(string.Empty, string.Empty);
                            Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + siitem.guid, "LocationLog");
                            // odstraneni online zaznamu
                            Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.DeleteRecordByGuid(siitem.guid, Fask.MST_W.LokaceService.ModulName.PRIJEM);

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

                            Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Delete_PI(siitem.guid);

                            Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");
                            siitem.Delete();
                        }
                        #endregion
                    }
                    else
                    {
                        Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.DeletePI_Queries(prow.CountEntries, prow.ITEMNMBR, prow.PONUMBER, prow.ORD);
                    }

                    // konec
                    //_q_ta.DeletePI(prow.CountEntries, prow.ITEMNMBR, prow.PONUMBER, prow.ORD);
                    //UpdateMnozstvi(prow.ITEMNMBR, prow.PONUMBER, prow.ORD, 0);
                    UpdateMnozstvi(prow.ITEMNMBR, prow.PONUMBER, prow.ORD, 0);
                }
            }
            catch (Exception ex)
            {
                if(prow != null)
                    UpdateMnozstvi(prow.ITEMNMBR, prow.PONUMBER, prow.ORD, 0);

                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {         
                ScannerStart();
            }
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SEARCH);
            NajdiPolozkuNazev();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            NajdiPolozkuPozice();
        }

        private void NajdiPolozkuPozice()
        {
            try
            {
                ScannerStop();

                int pozice = dataGrid1.CurrentRowIndex;
                using (SejmiKodForm skf = new SejmiKodForm(Fask.Localization.Localization.Prijem4PrijemListPoziceZaznamu, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, (pozice).ToString()))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;

                    pozice = int.Parse(skf.Kod);
                    if (pozice <= 0 || prijem.CZMST_PE.Count < pozice)
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListPoziceMimoRozsah, prijem.CZMST_PE.Count), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                    pozice--;
                }
                dataGrid1.CurrentRowIndex = pozice;
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

        #region Razeni
        private string statusInfoRazeni = string.Empty;
        private void razeniOrig()
        {
            statusInfoRazeni = string.Empty;
            this.cZMSTPEBindingSource.Sort = string.Empty;
            UpdateForm();
            //UpdateStatusBar();
        }

        bool serazeniNazev = false;
        private void razeniNazev()
        {
            serazeniNazev = !serazeniNazev;
            if (serazeniNazev)
            {
                statusInfoRazeni = Fask.Localization.Localization.Prijem4PrijemListRazeniNazevAZ;
                this.cZMSTPEBindingSource.Sort = "ITEMDESC ASC";
            }
            else
            {
                statusInfoRazeni = Fask.Localization.Localization.Prijem4PrijemListRazeniNazevZA;
                this.cZMSTPEBindingSource.Sort = "ITEMDESC DESC";
            }
            //UpdateStatusBar();
            UpdateForm();
        }

        bool serazeniORD = false;
        private void razeniORD()
        {
            serazeniORD = !serazeniORD;
            if (serazeniORD)
            {
                statusInfoRazeni = Fask.Localization.Localization.Prijem4PrijemListRazeniPoradi09;
                this.cZMSTPEBindingSource.Sort = "ORD ASC";
            }
            else
            {
                statusInfoRazeni = Fask.Localization.Localization.Prijem4PrijemListRazeniPoradi90;
                this.cZMSTPEBindingSource.Sort = "ORD DESC";
            }
            //UpdateStatusBar();
            UpdateForm();
        }
        #endregion

        private void menuItemRazeniNazev_Click(object sender, EventArgs e)
        {
            razeniNazev();
        }

        private void menuItemRazeniPoradi_Click(object sender, EventArgs e)
        {
            razeniORD();
        }

        private void menuItemRazeniPuvodni_Click(object sender, EventArgs e)
        {
            razeniOrig();
        }

        private void menuItemObjednavkaDetail_Click(object sender, EventArgs e)
        {
            DetailPrint();
        }

        private void menuItemObjednavkaPolozkaDetail_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Zobrazuje dialog s detaily objednavky - online dotaz
        /// </summary>
        private void DetailPrint()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow lprow = null;
            try
            {
                lprow = this.SelectedPE;
                if (lprow == null) return;
                this.ScannerStop();
                using (Detail detail = new Detail(lprow.PONUMBER.Trim(), lprow.IsSKL_IDNull() ? string.Empty : lprow.SKL_ID.Trim()))
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
        /// Zobrazuje dialog s detaily objednavky - online dotaz
        /// </summary>
        private void DetailItemPrint()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow lprow = null;
            try
            {
                lprow = this.SelectedPE;
                if (lprow == null) return;
                this.ScannerStop();
                using (DetailItem detailItem = new DetailItem(lprow.PONUMBER.Trim(), lprow.IsSKL_IDNull() ? string.Empty : lprow.SKL_ID.Trim(), lprow.ITEMNMBR.Trim(), lprow.ORD.ToString()))
                {
                    detailItem.ShowDialog();
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

        private void menuItemKusu_Click(object sender, EventArgs e)
        {
            DetailPocetKusu();
        }

        private void menuItemKusuNaSklade_Click(object sender, EventArgs e)
        {
            DetailPocetKusuLokace();
        }

        private void menuItemDetailItemnumber_Click(object sender, EventArgs e)
        {
            DetailItemnumber();
        }

        private void DetailItemnumber()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.SelectedPE;
                if (lprow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniVybrana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                string item = lprow.ITEMNMBR.Trim();
                string dokl = lprow.PONUMBER.Trim();
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

        private void DetailPocetKusu()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.SelectedPE;
                string item = lprow.ITEMNMBR.Trim();
                string itemdesc = lprow.ITEMDESC.Trim();
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

        private void DetailPocetKusuLokace()
        {
            Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow lprow = null;
            try
            {
                this.ScannerStop();
                lprow = this.SelectedPE;
                string item = lprow.ITEMNMBR.Trim();
                string lokace = lprow.LOCNCODE.Trim();
                string itemdesc = lprow.ITEMDESC.Trim();
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

        private void miTisk_Click(object sender, EventArgs e)
        {
            try
            {
				bool? TiskSCenou = null;
				bool vytisteno;

				ScannerStop();

                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe = this.SelectedPE;
                if (pe == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListZaznamNeniVybran, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

				if (Prijem_4.Globals.DialogTisk)
				{
					if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
						return;
				}


				if (Prijem_4.Globals.EtiketaTiskDotazSCenou)
				{
					if (Prijem_4.Globals.EtiketaTiskDotazSCenou_ZobrazDialog)
					{
						DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketyDotazSCenou, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

						if (dr == DialogResult.Yes)
							TiskSCenou = true;
						else { TiskSCenou = false; }
					}
					else 
					{
						TiskSCenou = !Prijem_4.Globals.EtiketaTiskDotazSCenou_Cena;
					}
				}


                //bool vytisteno = PrijemTisk.Print(pe, MST_Global.PrintServerTemplateNamePrijemPredloha);
				//bool vytisteno = PrijemTisk.Print(pe,null,null, PrinterFactory.PrinterModules.PrijemPredloha, null, TiskSCenou);

				if (Prijem_4.Globals.MnozstviAutoJedna)
					vytisteno = PrijemTisk.Print(pe, null, null, PrinterFactory.PrinterModules.PrijemPredloha, 1, TiskSCenou, pe.CZ_SerNum_Track.ToString());
				else
				{
					if (Prijem_4.Globals.EtiketaTisk_PrebiratMnozstvi)
					{
						int TiskQTY = Convert.ToInt32(pe.QTYSHPPD);
						Logging.Log.Write("Tisk Prijem Menu, Prebirane Množstvi :" + TiskQTY.ToString());
						vytisteno = PrijemTisk.Print(pe, null, null, PrinterFactory.PrinterModules.PrijemPredloha, TiskQTY, TiskSCenou, pe.CZ_SerNum_Track.ToString());
					}
					else
					{
						vytisteno = PrijemTisk.Print(pe, null, null, PrinterFactory.PrinterModules.PrijemPredloha, null, TiskSCenou, pe.CZ_SerNum_Track.ToString());
					}
				}


				
				Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", pe.CountEntries, pe.PONUMBER, pe.ITEMNMBR, vytisteno.ToString(), null));

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



        private void TiskEtikety(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, Guid pinewguid)
        {
            try
            {
                //ScannerStop();
				bool? TiskSCenou = null;

                if (Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz) //Tisk etikety
                {
					if (Prijem_4.Globals.DialogTisk)
					{
						if (DialogResult.No == MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
							return;
					}


					if (Prijem_4.Globals.EtiketaTiskDotazSCenou)
					{
						if (Prijem_4.Globals.EtiketaTiskDotazSCenou_ZobrazDialog)
						{
							DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListTiskEtiketyDotazSCenou, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

							if (dr == DialogResult.Yes)
								TiskSCenou = true;
							else { TiskSCenou = false; }
						}
						else
						{
							TiskSCenou = !Prijem_4.Globals.EtiketaTiskDotazSCenou_Cena;
						}
					}


                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow piRow = null;
                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable pidt = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByGuid_PI(pinewguid);
                    piRow = pidt[0];
                    //bool vytisteno = PrijemTisk.Print(pe, piRow, MST_Global.PrintServerTemplateNamePrijemNasnimane, null);
                    bool vytisteno;

					if (Prijem_4.Globals.MnozstviAutoJedna)
						vytisteno = PrijemTisk.Print(pe, piRow, PrinterFactory.PrinterModules.PrijemNasnimane, 1, TiskSCenou, pe.CZ_SerNum_Track.ToString());
					else
					{

						if (Prijem_4.Globals.EtiketaTisk_PrebiratMnozstvi)
						{
							int TiskQTY = Convert.ToInt32(piRow.QTYSHPPD);
							Logging.Log.Write("Tisk Prijem , Prebirane Množstvi :" + TiskQTY.ToString());
							vytisteno = PrijemTisk.Print(pe, piRow, PrinterFactory.PrinterModules.PrijemNasnimane, TiskQTY, TiskSCenou, pe.CZ_SerNum_Track.ToString());
						}
						else
						{
							vytisteno = PrijemTisk.Print(pe, piRow, PrinterFactory.PrinterModules.PrijemNasnimane, null, TiskSCenou, pe.CZ_SerNum_Track.ToString());
						}
					}


                    Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", piRow.CountEntries, piRow.PONUMBER, piRow.ITEMNMBR, vytisteno.ToString(), null));
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

        private void Online_Quantity(Fask.SQLiteDBs.DataSets.Prijem ds_prijem)
        {
            #region Online Quantity update
            if (Globals.OnlinePohyby)
            {
                // pokud je prazdne, tak nema smysl delat online dotaz ... 
                if (ds_prijem.CZMST_PE.Count <= 0) 
                    return;

                //priprava pro server
                PrijemService.Prijem dtPOnline = new Fask.MST_W.PrijemService.Prijem();
                foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow peRow in ds_prijem.CZMST_PE)
                {
                    dtPOnline.CZMST_PE.ImportRow(peRow);
                }
                ds_prijem.AcceptChanges();

                while (true)
                { // aktualizace stavu nasnimani ... 
                    try
                    {
                        PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_Quantity(0, MST_Global.TerminalID, ref dtPOnline);
                        if (so.Exception || so.StatusText != "OK")
                        {
                            if (DialogResult.Cancel == MessageBoxBig.Show(
                                string.Format(Fask.Localization.Localization.Prijem4PrijemListAktualizaceSeNezdarilaOpakovatDotaz, so.StatusText),
                                Fask.Localization.Localization.Prijem4PrijemListAktualizaceOnline,
                                MessageBoxButtons.RetryCancel,
                                MessageBoxBigIcon.Warning
                                ))
                            {
                                return;
                            }
                        }
                        else
                        { //aktualizace se podarila ... 
                            break;
                        }

                    }
                    catch (Exception e)
                    {
                        if (DialogResult.Cancel == MessageBoxBig.Show(
                            string.Format(Fask.Localization.Localization.Prijem4PrijemListAktualizaceSeNezdarilaOpakovatDotaz, e.Message),
                            Fask.Localization.Localization.Prijem4PrijemListAktualizaceOnline,
                             MessageBoxButtons.RetryCancel,
                              MessageBoxBigIcon.Warning
                            ))
                        {
                            return;
                        }
                    }
                    //pservice.Timeout += 5000; //zvetsi timeout ...
                }

                // TODO : provedeni natazeni poctu nasnimanych ... 
                foreach (var item in ds_prijem.CZMST_PE)
                {
                    object o = dtPOnline.CZMST_PI.Compute(
                        "SUM(QTYSHPPD)",
                        "CountEntries=" + item.CountEntries +
                        " and PONUMBER='" + item.PONUMBER + "'" +
                        " and ITEMNMBR='" + item.ITEMNMBR + "'" +
                        " and ORD=" + item.ORD);
                    if (o is DBNull)
                        item.Nasnimano = 0;
                    else if (o is Decimal)
                        item.Nasnimano = (decimal)o;
                    else
                        item.Nasnimano = 0;
                }
                ds_prijem.AcceptChanges();


            }
            #endregion

        }

        private void menuItemAktualizaceNasnimano_Click(object sender, EventArgs e)
        {
            aktualizaceNasnimanehoMnozstviKontrola();
        }

        private void menuItemInfo_Click(object sender, EventArgs e)
        {
            // TODO : zobrazeni informaci ... 
            MessageBoxBig.Show("Not implemented yet...");
            menuItemInfo.Enabled = false;
        }

        private void PrijemList_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void PrijemList_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        /// <summary>
        /// Online funkce pro vygenerovani noveho cisla sarze pomoci ITEMNMBR a rucne zadaneho cisla sarze.
        /// </summary>
        /// <param name="itemnmbr">Cislo polozky</param>
        /// <param name="sn">Zadane cislo sarze</param>
        /// <param name="serltnum">Nove cislo sarze</param>
        /// <returns>True v pripade uspechu, False v pripade chyby</returns>
        private bool OnlineGenerateSerltnum(string itemnmbr, string sn, string skl_id, out string serltnum)
        {
            serltnum = string.Empty;
            try
            {
                serltnum = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_GenerateSerltnum(itemnmbr, skl_id, sn);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Prijem.ProdejList, OnlineGenerateSerltnum");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            return true;
        }

        //private Fask.MST_W.PrijemService.StatusOverLokace OnlineOverLokace(string serltnum, string locncode)
        //{
        //    Fask.MST_W.PrijemService.StatusOverLokace so;
        //    try
        //    {
        //        PrijemService.PrijemService prijemService = new Fask.MST_W.PrijemService.PrijemService();
        //        prijemService.Url = MST_Global.ServerAddress + "Prijem.asmx";
        //        prijemService.Timeout = MST_Global.ServiceTimeOut;
        //        prijemService.UpdateWebServiceCredentials();

        //         //= new Fask.MST_W.PrijemService.StatusOverLokace();

        //        so = prijemService.Online_OverLokace(serltnum, locncode);
        //        //if (so.State != 0)  // nastala chyba
        //        //{
        //        //    throw new Exception(so.Error);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.Message, "Prijem.ProdejList, OnlineGenerateSerltnum");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //        return null;
        //    }
        //    return so;
        //}

        private void miZalokovat_Click(object sender, EventArgs e)
        {
            if(Prijem_4.Globals.PovolitZalokovani)
                Zalokovat();
        }

        private void Zalokovat()
        {
            try
            {
                ScannerStop();

                using (PrijemZalokovaniList pzl = new PrijemZalokovaniList(prijemDataParametry, prijmovalokace))
                {
                    //sif.ImageFilename = sn.Trim() + "_" + pocet.ToString();
                    DialogResult dr = pzl.ShowDialog();
                    // jeste probiha kontrola na vyplneni vsech lokaci
                    //if (dr == DialogResult.Cancel)
                    //    return;
                }

                // kontrola, zdali jsou vyplneny lokace u vsech zaznamu
                if (Prijem_4.Globals.KontrolaVyplneniLokaciPredOdeslanim)
                {
                    int? nevyplnene_lokace = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CountLocncodeNotIn_PI(prijmovalokace != null ? prijmovalokace.LOCNCODE : string.Empty);
                    
                    bool succ = true;
                    // byly vyplneny veskere lokace
                    if (nevyplnene_lokace.HasValue && nevyplnene_lokace.Value == 0)
                    {
                        // byla zpracovana veskera data
                        if (kontrolaDokoncenosti(true))
                        {
                            odeslatAktualniDavku();
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.WriteDebug(ex.Message, "Prijem_3.miZalokovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
                UpdateForm();
            }
        }

        private void OnlineCheckHmotnost(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow zbozi)
        {
            if (!Globals.OnlineHmotnost)
                return;

            if (zbozi == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListZboziNeniVybrano, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return;
            }

            decimal? hmotnost = null;
            try
            {
                Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemListZboziOnlineKontrolaHmotnosti);
                hmotnost = Prijem_4.PrijemMain.prijemInstance.globalObject.service_hmotnost.GetHmotnost(zbozi.ITEMNMBR);
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Prijem4PrijemListWSHmotnost, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }

            if (hmotnost.HasValue)
            {
                //DialogResult dlgResHmotnostZmena = MessageBox.Show(String.Format(" = {0}kg\nChcete zmìnit?", hmotnost.Value), "Hmotnost", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                //if (dlgResHmotnostZmena == DialogResult.No)
                //    return;
                
                // Jinak nez ve zbozi.dll 
                // Pokud je hmotnost, tak pokracuje v zadavani a dal neotravuje ...
                return;
            }

            string valueNew = string.Empty;
            string valueOld = (hmotnost.HasValue ? hmotnost.Value.ToString() : string.Empty);

            while (true)
            {
                DialogResult dlgHmotnostNew = InputBox.Show(Fask.Localization.Localization.Prijem4PrijemListHmotnost, valueOld, out valueNew, false);
                if (dlgHmotnostNew == DialogResult.Cancel)
                    return;
                try
                {
                    if (String.IsNullOrEmpty(valueNew))
                        hmotnost = null;
                    else
                        hmotnost = decimal.Parse(valueNew);
                }
                catch (Exception exValueNew)
                {
                    MessageBoxBig.Show(exValueNew.Message, Fask.Localization.Localization.Prijem4PrijemListHmotnostChyba, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    continue;
                }

                break; // ukonci zadavani a pokracuje. ...
            }

            // ulozeni hmotnosti na server...

            while (true)
            {
                try
                {
                    bool saved = Prijem_4.PrijemMain.prijemInstance.globalObject.service_hmotnost.SetHmotnost(zbozi.ITEMNMBR, hmotnost);
                    if (saved)
                        break;
                    else
                        throw new Exception(Fask.Localization.Localization.Prijem4PrijemListHmotnostUlozeniSeNezdarilo);
                }
                catch (Exception exWebSetHmotnost)
                {
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListHmotnostChybaOpakovatDotaz, exWebSetHmotnost.Message), Fask.Localization.Localization.Prijem4PrijemListHmotnostChyba, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                        == DialogResult.Yes)
                        continue;
                    else
                        return;
                }
                //break;
            }

            // vse ok ... tak se konci ...
        }

        private void miNastavitLokaci_Click(object sender, EventArgs e)
        {
            PerformNastavitLokaci();
        }

        /// <summary>
        /// Slouzi k vyberu lokace, ktera se bude automaticky pouzivat misto lokace, ktera je nastavena na predloze
        /// </summary>
        private void PerformNastavitLokaci()
        {
            if (!Prijem_4.Globals.LokaceNaDavkuPovolit)
                return;

            try
            {
                ScannerStop();

                // je povolena prijmova lokace a lokace na davku
                // -> umoznit zmenu lokace, resp. moznost prepnuti mezi prijmovou lokaci a zadanou
                if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_PRIJMOVANull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA)
                {
                    // checked = true -> volba lokace
                    // checked = false -> volba prijmove lokace

                    miNastavitLokaci.Checked = !miNastavitLokaci.Checked;

                    if (miNastavitLokaci.Checked)
                    {
                        #region rucni zadani lokace
                        // neni vyplneno, nastavit lokaci ...
                        PrijemZadejLokaci pzl = new PrijemZadejLokaci();
                        // nastaveni lokace, ktera se bude pouzivat (ne jen predvyplnovat ...
                        pzl.Popis = MST_Global.LC_NAME;
                        pzl.Text = "Lokace";
                        pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
						pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                        //skf.Len = Globals.LOCNCODE_LEN;
                        //skf.CheckLen = true;
                        pzl.AllowEmpty = false;
                        pzl.Kod = locncodeNaDavku;
                        //pzl.perow = PERow;

                        if (pzl.ShowDialog() == DialogResult.Cancel)
                        {
                            //miNastavitLokaci.Checked = false;
                            // storno -> zustane predchozi stav
                            miNastavitLokaci.Checked = !miNastavitLokaci.Checked;
                            return;
                        }

                        locncodeNaDavku = pzl.Kod;
                        #endregion
                    }
                    else
                    {
                        #region vyber prijmove lokace ze seznamu
                        // vyber prijmove lokace
                        using (PrijemVyberPrijmoveLokaceList pvp = new PrijemVyberPrijmoveLokaceList(_sklad, false))
                        {
                            if (pvp.ShowDialog() == DialogResult.Cancel)
                            {
                                // pokud lokace byla vyplnena, tak se nastavi, jinak se vyuzije prijmova
                                if (string.IsNullOrEmpty(locncodeNaDavku))
                                    miNastavitLokaci.Checked = false;
                                else
                                    miNastavitLokaci.Checked = !miNastavitLokaci.Checked;

                                return;
                            }

                            prijmovalokace = pvp.PrijmovaLokace;
                        }
                        #endregion
                    }
                }
                // je vypnuto zalokovani a soucasne je zapnuta lokace na davku
                // -> umoznist zmenu lokace
                else
                {
                    miNastavitLokaci.Checked = !miNastavitLokaci.Checked;
                    if (miNastavitLokaci.Checked)
                    {
                        #region rucni zadani lokace
                        PrijemZadejLokaci pzl = new PrijemZadejLokaci();
                        // nastaveni lokace, ktera se bude pouzivat (ne jen predvyplnovat ...
                        pzl.Popis = MST_Global.LC_NAME;
                        pzl.Text = "Lokace";
                        pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
						pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
                        //skf.Len = Globals.LOCNCODE_LEN;
                        //skf.CheckLen = true;
                        pzl.AllowEmpty = false;
                        pzl.Kod = locncodeNaDavku;
                        //pzl.perow = PERow;

                        if (pzl.ShowDialog() == DialogResult.Cancel)
                        {
                            miNastavitLokaci.Checked = false;
                            return;
                        }

                        locncodeNaDavku = pzl.Kod;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemList, miNastavitLokaci_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                UpdateStatusBar();
                ScannerStart();
            }
        }

        private void miFiltrPaleta_Click(object sender, EventArgs e)
        {
            PerformSetPaleta();
        }

        /// <summary>
        /// Zmena cisla palety. Bude dochazet k filtrovani na cislo palety.
        /// 1) vybrano cislo palety -> vyfiltruji se zaznamy a bude dochazet k vyhledavani podle cisla palety (pri zmene se predvyplni)
        /// 2) vybran prazdny retezec -> vypne se filtr
        /// 3) storno pri vyberu palety -> zustane predchozi filtr na paletu aktivni
        /// </summary>
        private void PerformSetPaleta()
        {
            try
            {
                ScannerStop();

				if (Paleta == null)
					Paleta = new Paleta();

				using (SejmiKodForm skf = new SejmiKodForm("Èíslo palety", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, Paleta.sscc, true))
                {
                    if (skf.ShowDialog() == DialogResult.Cancel)
                        return;
					Paleta.sscc = skf.Kod;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemList, PerformSetPaleta");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                FiltrZobrazeni = _filtrZobrazeni;
                UpdateStatusBar();
                ScannerStart();
            }
        }

        /// <summary>
        /// Online ziskani ID zdrojoveho a ciloveho skladu. Pouziva se pouze pro ANC.
        /// </summary>
        /// <param name="doc_id">typ dokladu.</param>
        /// <param name="itemnmbr">itemnmbr.</param>
        /// <param name="serltnum">serltnum.</param>
        /// <param name="skl_id">skl_id (out parameter)</param>
        /// <param name="skl_id_dest">skl_id_dest (out parameter)</param>
        /// <returns>STATUS (OK, ERROR)</returns>
        private Fask.MST_W.ProdejService.STATUS OnlineGetSklad(string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            // TODO: prepsat, nyni se pouziva stejna metoda jako v prodejnim modulu ... pouziva se pouze pro ANC
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
                Logging.Log.Write(ex.Message, "Prijem.PrijemList, OnlineGetSklad");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                return Fask.MST_W.ProdejService.STATUS.ERROR;
            }

            return Fask.MST_W.ProdejService.STATUS.OK;
        }

        private void miRezimZadavaniLokace_Click(object sender, EventArgs e)
        {
            PerformZmenaRezimuLokace();
        }

        /// <summary>
        /// Zmena rezimu zadavani lokaci. Momentalne metoda pocita s tim, ze se vyuzivaji prijmove lokace.
        /// 1) Režim pøíjmu na pøíjmovou lokaci (výchozí režim pøi otevøení dávky)
        /// -> Provádí se pøíjem položek na pøíjmovou lokaci bez zobrazení dialogu pro zadání lokace. Ve status baru je zobrazeno: R:1,PL:ULI5 (režim 1, pøíjmová lokace ULI5).
        /// 2) Režim pøíjmu s pøímým zalokováním na uživatelem urèenou lokaci
        /// -> Provádí se pøíjem položek na obsluhou vybranou lokaci (lokace se nastavuje pomocí klávesy F8). Zde nedochází k zobrazení dialogu pro zadání lokace. Ve status baru je zobrazeno: R:1,L:XYZ (režim 1, uživatelem zadaná lokace XYZ).
        /// 3) Režim pøíjmu s možností nastavení lokace pro každou položku s pøednastavením doporuèené lokace pro danou položku
        /// -> Provádí se pøíjem položek, kdy dochází pøi každé položce k zobrazení dialogu pro zadání lokace (pøedvyplòuje se doporuèená lokace -> pokud je zavedena pro danou položku). Ve status baru je zobrazeno: R2 (režim 2)
        /// 4) Režim pøíjmu s pøímým zalokováním na uøivatelem urèenou lokaci s pøednastavením pro každou položku (XYZ)
        /// -> Provádí se pøíjem položek, kdy dochází pøi každé položce k zobrazení dialogu pro zadání lokace (pøedvyplòuje se uživatelem zvolená lokace). Ve status baru je zobrazeno: R:2,L:XYZ (režim 2, uživatelem zadaná lokace XYZ).
        /// </summary>
        private void PerformZmenaRezimuLokace()
        {
            if (!Prijem_4.Globals.PovolitZmenuRezimuZadaniLokace)
                return;

            try
            {
                ScannerStop();

                miRezimZadavaniLokace.Checked = !miRezimZadavaniLokace.Checked;

                if (miRezimZadavaniLokace.Checked)
                {
                    // zaskrnuto
                    // -> 1) vypnuti prijmove lokace (TODO: Poresit, pokud se prijmove lokace nevyuzivaji)
                    // -> 2) vypnuti lokace na davku (nastavit string.empty)
                    // -> 3) zobrazit dialog zadani lokace

                    // 1) vyber prijmove lokace
                    // TODO: pamatovat si, zdali je povolena prijmova lokace a kdyz tak na ni prepnout ... viz oddelena verze Perlacasa, kde to je napevno
                    prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA = false;

                    // 2) vypnuti lokace na davku (nastavit string.empty)
                    if (miNastavitLokaci.Checked)
                    {
                        PerformNastavitLokaci();
                        locncodeNaDavku = string.Empty;
                    }
                }
                else
                {
                    // nezaskrnuto
                    // -> 1) vyber prijmove lokace (TODO: Poresit, pokud se prijmove lokace nevyuzivaji)
                    // -> 2) vypnuti lokace na davku (nastavit string.empty)
                    // -> 3) nezobrazit dialog zadani lokace

                    // povoleni prijmove lokace
                    // TODO: pamatovat si, zdali je povolena prijmova lokace a kdyz tak na ni prepnout ... viz oddelena verze Perlacasa, kde to je napevno
                    prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA = true;

                    // 2) vypnuti lokace na davku (nastavit string.empty)
                    miNastavitLokaci.Checked = true;
                    locncodeNaDavku = string.Empty;
                    PerformNastavitLokaci();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemList, PerformZmenaRezimuLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                UpdateStatusBar();
                ScannerStart();
            }
        }


        private bool OnlineGetSkladExpedice(
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
                return Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_GetSkladExpedice(
                     ITEMNMBR.Trim(),
                     MnozstviZadane,
                     MnozstviNasnimane,
                     out  MnozstviDodavatelePozadovano,
                     out  MnozstviDodavateleDodano,
                     out  MnozstviDodavateleDodat,
                     out  MnozstviOdberateliPozadovano,
                     out  MnozstviOdberatelumDodano,
                     out  MnozstviOdberatelumDodat,
                     out  Vysledek
                     );

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemList, OnlineGetSkladExpedice");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                return false;
            }
            //return true;
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz = !Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz;

            menuItemTiskAnoNE.Checked = Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz;

        }

		private void menuItem18_Click(object sender, EventArgs e)
		{
			mi_KonScan.Checked = !mi_KonScan.Checked;
			Program.mstw.Scanner.ContinuousRead = mi_KonScan.Checked;
		}

		private void miPaleta_Click(object sender, EventArgs e)
		{
			ZmenaPalety();
		}

		private void miTiskPalListek_Click(object sender, EventArgs e)
		{
			if (this.SelectedPE == null)
			{
				MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NeniVybranaZadnaPolozka, Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
				return;
			}

			TiskPaleta(Paleta);

			//TiskEtiketaPalListek(this.PolozkaAktualniVybrana.SOPNUMBE.Trim());
		}

		private void ZmenaPalety()
		{
			try
			{
				//this.ScannerStop();

				Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, this.Name, "ZmenaPalety");
				Logging.Trace2.Write("Start", "TypOznaceniPaletyForm volany form", tid);

				TiskPaleta(Paleta);

				//Vyber typu palet
				if (MST_Global.VydejTypOznaceniPalety)
				{
					this.ScannerStop();
					using (TypOznaceniPaletyForm typoznpal = new TypOznaceniPaletyForm())
					{
						if (typoznpal.ShowDialog() == DialogResult.Cancel)
							return;
						Paleta = typoznpal.TypOznaceni;
					}
				}
				Logging.Trace2.Write("End", "TypOznaceniPaletyForm volany form", tid);
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


    }
}