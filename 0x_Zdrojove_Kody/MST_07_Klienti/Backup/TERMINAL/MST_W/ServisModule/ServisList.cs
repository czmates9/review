using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using System.Drawing.Imaging;
using System.Threading;


namespace Fask.MST_W.ServisModule
{
    public partial class ServisList : Form
    {
        // pomala synchronizace, nesynchronizovala vsechny zaznamy 
        //private System.Threading.Thread synchronizeThread = null;



        /// <summary>
        /// Puvodni hodnoty ZdrojStav
        /// </summary>
        private ServisModule.Data.ZdrojeStav.ZdrojStavDataTable defaultZdrojStav = new Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavDataTable();
        /// <summary>
        /// Vybrany odberatel.
        /// </summary>
        private Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row selectedOdberatel = null;
        /// <summary>
        /// Vybrany okruh.
        /// </summary>
        private Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow selectedOkruh = null;

        /// <summary>
        /// Hodnota, ktera se predvyplni pri stisknuti tlacitka zpet.
        /// </summary>
        private string cinnostValue = null;

        private Object lockUpDownTest = new Object();
        private global::System.Threading.Timer timerUpload = null;
        public static bool _uploadInProgress = false;

        /// <summary>
        /// Informace, které se budou zobrazovat ve statusbaru ostatnich formularu
        /// </summary>
        private string statusBarInfo { get; set; }
        /// <summary>
        /// ma se obnovit zaznam.
        /// </summary>
        private bool revertRecord { get; set; }

        /// <summary>
        /// určení, jaký filtr je aktivní (vše / Rozpracované)
        /// </summary>
        private bool filtrZobrazitVse = false;

        private Data.ZdrojeStav.ZdrojStavRow ZdrojStavSelected
        {
            get
            {
                try
                {
                    return ((DataRowView)this.dataGrid1.BindingContext[bsZdrojeStav].Current).Row as Data.ZdrojeStav.ZdrojStavRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        public ServisList()
        {
            InitializeComponent();

            //_zdrojeStav = new Fask.MST_W.ServisModule.Data.ZdrojeStav();
            //_zdrojeDataView = new DataView(_zdrojeStav.ZdrojStav);
            //dataGrid1.DataSource = _zdrojeDataView;


			//Globals._webServiceModule = new Fask.MST_W._WebRefernces_Globals.ServisModuleWServiceSession();
			//Globals._webServiceModule.Url = MST_Global.ServerAddress + "Servis.asmx";
			//Globals._webServiceModule.Timeout = MST_Global.ServisTimeoutSynchronize;
			//Globals._webServiceModule.UpdateWebServiceCredentials();

        }

        public ServisList(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow okruh):this()
        {
            try
            {
                //Globals.Davka = davka;
                this.selectedOdberatel = odberatel;
                this.selectedOkruh = okruh;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();
        }

        private void PerformKonec()
        {
            if (DialogResult.No == MessageBoxBig.Show("Ukončit práci?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
            {
                return;
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void ServisMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;


                // nacteni z konfigurace posledni aktivni filtr
                filtrZobrazitVse = Settings.ServisListZdrojeStavFiltrVse;
                if (!MST_Global.ServisFiltrZobrazeniPovolit)
                {
                    this.menuItemZobrazeni.Enabled = Prijem_4.Globals.PovolitZalokovani;
                    if (!Prijem_4.Globals.PovolitZalokovani && menuItem1.MenuItems.Contains(this.menuItemZobrazeni))
                        menuItem1.MenuItems.Remove(this.menuItemZobrazeni);
                }

                // pokud je davkove zpracovani, tak vypnout tlacitko pro aktualizaci zdroju
                if (MST_Global.ServisDavkoveZpracovani)
                {
                    miAktualizaceZdrojeStavy.Enabled = false;
                }

                //InitializeDatabaseFiles();

                InitializeGrid();

                ZdrojeFill();

                // aktivace filtru
                txtSearchZdroj_TextChanged(null, null);

                ScannerStart();

                // Thread pro pravidelnou synchronizaci ...
                //synchronizeThread = new System.Threading.Thread(new System.Threading.ThreadStart(ZdrojeSynchronizeThread));
                //synchronizeThread.IsBackground = true;
                //synchronizeThread.Name = "ServisSynchronization";
                //synchronizeThread.Start();

                dataGrid1.Focus();
                if (MST_Global.ServisOdeslaniDatNaPozadiPovolit)
                    timerUpload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerUploadCallBack), this, MST_Global.ServisAutoUpdateInterval, MST_Global.ServisAutoUpdateInterval);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ServisList.FormLoad()");
            }
        }

		//private void InitializeDatabaseFiles()
		//{
		//    if (!File.Exists(Globals.ServisCiselnikDB))
		//        File.Copy(
		//            Path.Combine(Main.SqlCEDBsDir, Globals.ServisCiselnikFileName),
		//            Globals.ServisCiselnikDB
		//            );

		//    if (!File.Exists(Globals.ServisZdrojePohybDB))
		//        File.Copy(
		//            Path.Combine(Main.SqlCEDBsDir, Globals.ServisZdrojePohybFileName),
		//            Globals.ServisZdrojePohybDB
		//            );

		//    if (!File.Exists(Globals.ServisZdrojeStavDB))
		//        File.Copy(
		//            Path.Combine(Main.SqlCEDBsDir, Globals.ServisZdrojeStavFileName),
		//            Globals.ServisZdrojeStavDB
		//            );

		//    // kopie tmp databaze pro odesilani dat
		//    if (!File.Exists(Globals.ServisZdrojePohybTmpDB))
		//    {
		//        File.Copy(
		//                    Globals.ServisZdrojePohybDB,
		//                    Globals.ServisZdrojePohybTmpDB, false
		//                    );
		//    }

		//    System.Data.SQLite.SQLiteConnection sqlConnection = null;

		//    //sqlConnection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Globals.ServisCiselnikDB);
		//    sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Globals.ServisCiselnikDB));
		//    Globals.globalObject.Controller_servis.TaCiselnikStav.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaCiselnikStavNext.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaCiselnikCinnost.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaCiselnikCinnostNext.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaCiselnikZdroj.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaDynTabDef.Connection = sqlConnection;

		//    sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Globals.ServisZdrojePohybDB));
		//    Globals.globalObject.Controller_servis.TaZdrojePohyb.Connection = sqlConnection;
		//    //Globals._taZdrojePohyb.Connection.Open();

		//    sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Globals.ServisZdrojeStavDB));
		//    Globals.globalObject.Controller_servis.TaZdrojeStav.Connection = sqlConnection;
		//    Globals.globalObject.Controller_servis.TaZdrojeStavTmp.Connection = sqlConnection;            
		//}

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void ZdrojeUpdate(string zdrojID)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Action)delegate
                {
                    this.ZdrojeUpdate(zdrojID);
                });
            }

            try
            {
                var zdroje = zdrojeStav.ZdrojStav.Where(z => z.ZdrojID == zdrojID);
                if (zdroje.Count() > 0)
                {
                    Data.ZdrojeStav.ZdrojStavRow zdroj = zdroje.First();
                    ZdrojeUpdate(zdroj);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void ZdrojeStavUpdate()
        {
            try
            {
                // nacist vsechny pohyby od nejnovejsiho
                // postupne aktualizovat zdrojeStav.ZdrojStav podle id zdroje
                //Globals._taZdrojePohyb.Connection.Open();
				//System.Data.SQLite.SQLiteConnection sqlConnection = null;
				//sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Globals.ServisZdrojePohybDB));
				//Globals.globalObject.Controller_servis.TaZdrojePohyb.Connection = sqlConnection;
                //Globals._taZdrojePohyb.Connection.Open();

				//sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Globals.ServisZdrojeStavDB));
				//Globals.globalObject.Controller_servis.TaZdrojeStav.Connection = sqlConnection;

				var zdrojePohyb = Globals.globalObject.Controller_servis_ZdrojePohyb.GetData_ZdrojePohyb().OrderByDescending(x => x.Modified);
                if (zdrojePohyb != null && zdrojePohyb.Count() > 0)
                {
                    foreach (var zdrojPohyb in zdrojePohyb)
                    {
                        //var zdroje = zdrojeStav.ZdrojStav.Where(z => z.ZdrojID == zdrojPohyb.IDZdroj);
						var zdroje = Globals.globalObject.Controller_servis_ZdrojeStav.GetDataByIDZdroj_ZdrojStav(zdrojPohyb.IDZdroj);//zdrojeStav.ZdrojStav.Where(z => z.ZdrojID == zdrojPohyb.IDZdroj);
                        if (zdroje != null && zdroje.Count() > 0)
                        {
                            // stazeny zdrojstav je starsi nez neodeslany zdrojpohyb ... probehne aktualizace
                            //if (zdroje.First().ZdrojModified < zdrojPohyb.Modified)
                            if (zdroje.First().Modified < zdrojPohyb.Modified)
                            {
								Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                                    zdrojPohyb.IDStav,
                                    zdrojPohyb.IsIDCinnostNull() ? null : zdrojPohyb.IDCinnost,
                                    zdrojPohyb.Modified,
                                    zdrojPohyb.IDTerminal,
                                    zdrojPohyb.IDUser,
                                    zdrojPohyb.GUID,
                                    zdrojPohyb.IsCinnostValueNull() ? null : zdrojPohyb.CinnostValue,
                                    zdrojPohyb.IsCinnostTypeNull() ? null : zdrojPohyb.CinnostType,
                                    zdrojPohyb.IsCountEntriesNull() ? (int?)null : zdrojPohyb.CountEntries,
                                    zdrojPohyb.IsODB_IDNull() ? null : zdrojPohyb.ODB_ID,
                                    zdrojPohyb.IsOkruhIDNull() ? null : zdrojPohyb.OkruhID,
                                    zdrojPohyb.IsCinnostOznaceniNull() ? null : zdrojPohyb.CinnostOznaceni,
                                    zdrojPohyb.IsGPS_XNull() ? (double?)null : zdrojPohyb.GPS_X,
                                    zdrojPohyb.IsGPS_YNull() ? (double?)null : zdrojPohyb.GPS_Y,
                                    zdrojPohyb.IsGPS_ZNull() ? (int?)null : zdrojPohyb.GPS_Z,
                                    zdrojPohyb.IDZdroj
                                    );

                                // potvrzeni zmen
                                //zdroje.First().AcceptChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "ZdrojeStavUpdate() - aktualizace zaznamu ze zdrojpohyb");
            }
            //finally
            //{
            //    try
            //    {
            //        if ((Globals._taCiselnikCinnost.Connection.State & ConnectionState.Open) == ConnectionState.Open)
            //            Globals._taCiselnikCinnost.Connection.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Logging.Log.Write(ex);
            //    }
            //}
        }

        private void ZdrojeUpdate(Data.ZdrojeStav.ZdrojStavRow zdroj)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Action)delegate
                {
                    this.ZdrojeUpdate(zdroj);
                });
                return;
            }

            try
            {
				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable dt_zstav = Globals.globalObject.Controller_servis_ZdrojeStav.GetDataByIDZdroj_ZdrojStav(zdroj.ZdrojID);
                if (dt_zstav != null && dt_zstav.Count > 0)
                {
                    Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavRow z = dt_zstav[0];

                    if (z.IsGUIDNull())
                        zdroj.SetZdrojGUIDNull();
                    else
                        zdroj.ZdrojGUID = z.GUID;

                    zdroj.ZdrojID = z.IDZdroj;
                    zdroj.ZdrojModified = z.Modified;
                    zdroj.StavID = z.IDStav;
					zdroj.StavNazev = Globals.globalObject.Controller_servis_Ciselniky.Oznaceni_Stav(z.IDStav);

                    if (z.IsIDCinnostNull())
                    {
                        zdroj.SetCinnostIDNull();
                        zdroj.SetCinnostNazevNull();
                    }
                    else
                    {
                        zdroj.CinnostID = z.IDCinnost;
						zdroj.CinnostNazev = Globals.globalObject.Controller_servis_Ciselniky.Oznaceni_Cinnost(zdroj.CinnostID);
                    }

                    if (z.IsCinnostTypeNull())
                        zdroj.SetZdrojCinnostTypeNull();
                    else
                        zdroj.ZdrojCinnostType = z.CinnostType;

                    if (z.IsCinnostValueNull())
                        zdroj.SetZdrojCinnostValueNull();
                    else
                        zdroj.ZdrojCinnostValue = z.CinnostValue;
                    
                    // CountEntries
                    if(z.IsCountEntriesNull())
                        zdroj.SetCountEntriesNull();
                    else
                        zdroj.CountEntries = z.CountEntries;

                    // odb_id
                    if (z.IsODB_IDNull())
                        zdroj.SetODB_IDNull();
                    else
                        zdroj.ODB_ID = z.ODB_ID;

                    // okruhid
                    if (z.IsOkruhIDNull())
                        zdroj.SetOkruhIDNull();
                    else
                        zdroj.OkruhID = z.OkruhID;

                    // potvrzeni zmen
                    zdroj.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Nacteni informaci pro statusbar, ktery se zobrazuje v cinnostech, stavech, ...
        /// </summary>
        /// <param name="zdroj"></param>
        private void FillStatusBarInfo(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj)
        {
            try
            {
                if (zdroj == null)
                {
                    statusBarInfo = string.Empty;
                    return;
                }

                List<string> sbInfo = new List<string>();

                // zobrazeni nazvu stavu ve status baru
                if (MST_Global.ServisStatusBarZobrazitNazevStavu)
                {
                    sbInfo.Add("S:" + (zdroj.IsStavNazevNull() ? string.Empty : zdroj.StavNazev.Trim()));
                }

                // zobrazeni id zdroje ve status baru
                if (MST_Global.ServisStatusBarZobrazitIdZdroje)
                {
                    sbInfo.Add("Z:" + (zdroj.IsZdrojIDNull() ? string.Empty : zdroj.ZdrojID.Trim()));
                }

                // zobrazeni typu zdroje ve status baru
                if (MST_Global.ServisStatusBarZobrazitTypZdroje || MST_Global.ServisStatusBarZobrazitOznaceniZdroje)
                {
					var dtZdroj = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Zdroj(zdroj.ZdrojID);
                    if (dtZdroj != null && dtZdroj.Count > 0)
                    {
                        if (MST_Global.ServisStatusBarZobrazitOznaceniZdroje)
                        {
                            sbInfo.Add("O:" + dtZdroj.First().Oznaceni.Trim());
                        }

                        if (MST_Global.ServisStatusBarZobrazitTypZdroje && !dtZdroj.First().IsTypeNull())
                        {
                            sbInfo.Add("T:" + dtZdroj.First().Type.Trim());
                        }
                    }
                }

                statusBarInfo = string.Join(", ", sbInfo.ToArray());
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "set Status bar info");
            }
        }

        /// <summary>
        /// Naplneni dat, ktere se zobrazuji v datagridu. Vola se pri loadu formulare a aktualizaci ciselniku.
        /// </summary>
        private void ZdrojeFill()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                //_zdrojeStav.ZdrojStav.Clear();
                //_zdrojeStav.AcceptChanges();
                zdrojeStav.ZdrojStav.Clear();
                zdrojeStav.AcceptChanges();

				//Globals.globalObject.Controller_servis_Ciselniky.Connection_Open();

                zdrojeStav.ZdrojStav.BeginLoadData();

				foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavRow zs in Globals.globalObject.Controller_servis_ZdrojeStav.GetData_ZdrojStav())
                {
                    //Data.ZdrojeStav.ZdrojStavRow nzs = _zdrojeStav.ZdrojStav.NewZdrojStavRow();
                    Data.ZdrojeStav.ZdrojStavRow nzs = zdrojeStav.ZdrojStav.NewZdrojStavRow();
                    nzs.ZdrojID = zs.IDZdroj;

                    nzs.ZdrojModified = zs.Modified;
                    if (zs.IsGUIDNull())
                        nzs.SetZdrojGUIDNull();
                    else
                        nzs.ZdrojGUID = zs.GUID;

                    nzs.OkruhID = zs.IsOkruhIDNull() ? null : zs.OkruhID;
                    nzs.ODB_ID = zs.IsODB_IDNull() ? null : zs.ODB_ID;
                    if (zs.IsCountEntriesNull())
                        nzs.SetCinnostIDNull();
                    else
                        nzs.CountEntries = zs.CountEntries;

                    if (zs.IsGPS_XNull())
                        nzs.SetGPS_XNull();
                    else
                        nzs.GPS_X = zs.GPS_X;

                    if (zs.IsGPS_YNull())
                        nzs.SetGPS_YNull();
                    else
                        nzs.GPS_Y = zs.GPS_Y;

                    if (zs.IsGPS_ZNull())
                        nzs.SetGPS_ZNull();
                    else
                        nzs.GPS_Z = zs.GPS_Z;

                    nzs.ZdrojCinnostType = zs.IsCinnostTypeNull() ? null : zs.CinnostType;
                    nzs.ZdrojCinnostValue = zs.IsCinnostValueNull() ? null : zs.CinnostValue;
                    nzs.StavID = zs.IDStav;
                    nzs.CinnostID = zs.IsIDCinnostNull() ? null : zs.IDCinnost;

					Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojDataTable dt_zdroj = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Zdroj(zs.IDZdroj);
                    Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojRow r_zdroj = null;
                    if (dt_zdroj.Count > 0)
                        r_zdroj = dt_zdroj[0];

                    if (r_zdroj != null)
                    {
                        if (!r_zdroj.IsBarcodeNull()) nzs.ZdrojBarcode = r_zdroj.Barcode;
                        if (!r_zdroj.IsMistoNull()) nzs.ZdrojMisto = r_zdroj.Misto;
                        nzs.ZdrojNazev = r_zdroj.Oznaceni;
                    }

					Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavDataTable dt_stav = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Stav(zs.IDStav);
                    Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavRow r_stav = null;
                    if (dt_stav.Count > 0)
                        r_stav = dt_stav[0];

                    if (r_stav != null)
                    {
                        nzs.StavNazev = r_stav.Oznaceni;
                    }

                    if (!zs.IsIDCinnostNull())
                    {
						Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable dt_cinnost = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Cinnost(zs.IDCinnost);
                        Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow r_cinnost = null;
                        if (dt_cinnost.Count > 0)
                            r_cinnost = dt_cinnost[0];

                        if (r_cinnost != null)
                        {
                            nzs.CinnostNazev = r_cinnost.Oznaceni;
                        }
                    }

                    // nacteni rozpracovano z DB
					Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable dt_zdrojstavtmp = Globals.globalObject.Controller_servis_ZdrojeStav.GetDataByZdrojID_ZdrojStavTmp(zs.IDZdroj);
                    Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpRow r_zdrojstavtmp = null;
                    if (dt_zdrojstavtmp.Count > 0)
                        r_zdrojstavtmp = dt_zdrojstavtmp[0];

                    if (r_zdrojstavtmp != null)
                    {
                        nzs.Rozpracovano = r_zdrojstavtmp.Rozpracovano;
                    }
                    
                    //_zdrojeStav.ZdrojStav.AddZdrojStavRow(nzs);
                    zdrojeStav.ZdrojStav.AddZdrojStavRow(nzs);
                }                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
				//if ((Globals.globalObject.Controller_servis_Ciselniky.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//    Globals.globalObject.Controller_servis_Ciselniky.Connection_Close();

                zdrojeStav.ZdrojStav.EndLoadData();

                UpdateForm();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Proběhne stáhnutí veškerých dynamických tabulek. Výsledné jméno souboru je: "Servis_Ciselniky" + "FullName v definici dynamických tabulek"
        /// </summary>
        private void downloadDynamicTables()
        {
            string path = string.Empty;
			foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow row in Globals.globalObject.Controller_servis_Ciselniky.GetData_DynTabDef())
            {
                path = Path.Combine(Main.DataDir, "Servis_Ciselniky" + row.FullName + ".sdf");
                // překopírování prázdné DB, kdyby nastala chyba
                //if (!File.Exists(path))
                //File.Copy(
                //    Path.Combine(Main.SqlCEDBsDir, Globals.ServisCiselnikFileName),
                //    path
                //    );

				ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Dynamic_Table(MST_Global.TerminalID, row.FullName);

                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return; // TODO : wait ... 
                }
                

                // stahnout data do tmp ... 
                FileTransfer.Downloading.DownloadFileFromServer(path);
            }
        }

        private bool ZdrojeRestore(Data.ZdrojeStav.ZdrojStavRow zdroj)
        {
            
            try
            {
                Fask.Logging.Log.Write("ZdrojeRestore(Data.ZdrojeStav.ZdrojStavRow zdroj) START");
                // 1) najit posledni zaznam z Zdroj pohyb
                // 2) smazat nejnovejsi zaznam z ZdrojPohyb
                // 3) najit predposledni zaznam (resp. jiz posledni) z ZdrojPohyb
                // 4) Udelat update podle predposledniho (resp. posledniho) zaznamu ZdrojeStav
                if (zdroj == null)
                    throw new Exception("Není možné se vrátit o krok zpět");

                if (zdroj.IsZdrojGUIDNull())
                    throw new Exception("Není dostatek dat pro vrácení o krok zpět");


                //defaultZdrojStav.First().ZdrojModified
                // najiti vsech zaznamu k danemu zdroji od posledniho aktualizovaneho zdroje
                //var dtZdrojPohyb = Globals._taZdrojePohyb.GetDataByIDZdroj(zdroj.ZdrojID);
				var dtZdrojPohyb = Globals.globalObject.Controller_servis_ZdrojePohyb.GetDataByIDZdrojModified_ZdrojePohyb(zdroj.ZdrojID, defaultZdrojStav.First().ZdrojModified);
                // kontrola, zdali existuji alespon 2 zaznamy (jeden se bude odstranovat a podle druheho se budou doplnovat data ZdrojeStav)

                // existuje alespon 1 zaznam v neodeslanych datech
                // 1) ulozit si vyplnenou hodnotu
                // 2) smazat zaznam ze ZdrojPohyb podle GUID
                // 3) 
                if (dtZdrojPohyb != null && dtZdrojPohyb.Count >= 2)
                {
                    // ulozeni GUIDu posledniho zaznamu
                    Guid lastRecordGuid = zdroj.ZdrojGUID;

                    // ulozeni zadane hodnoty pro opetovne vyplneni
                    cinnostValue = dtZdrojPohyb.First().IsCinnostValueNull() ? null : dtZdrojPohyb.First().CinnostValue;

                    // update na predposledni stav
                    //Globals._taZdrojeStav.Update(
                    //dtZdrojPohyb.First().IDStav,
                    //    //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                    //dtZdrojPohyb.First().IsIDCinnostNull() ? null : dtZdrojPohyb.First().IDCinnost,
                    //dtZdrojPohyb.First().Modified,
                    //MST_Global.TerminalID,
                    //MST_Global.UserID,
                    //dtZdrojPohyb.First().GUID,
                    //dtZdrojPohyb.First().IsCinnostValueNull() ? null : dtZdrojPohyb.First().CinnostValue,
                    //dtZdrojPohyb.First().IsCinnostTypeNull() ? null : dtZdrojPohyb.First().CinnostType,
                    //dtZdrojPohyb.First().IDZdroj
                    //);

                    // odstraneni nejnovejsiho zaznamu
                    //Globals._taZdrojePohyb.Delete(lastRecordGuid);


                    //// ulozeni GUIDu posledniho zaznamu
                    //Guid lastRecordGuid = zdroj.ZdrojGUID;

                    //// ulozeni zadane hodnoty pro opetovne vyplneni
                    //cinnostValue = dtZdrojPohyb.First().IsCinnostValueNull() ? null : dtZdrojPohyb.First().CinnostValue;

                    // update na predposledni stav
					Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                    dtZdrojPohyb[1].IDStav,
                        //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                    dtZdrojPohyb[1].IsIDCinnostNull() ? null : dtZdrojPohyb[1].IDCinnost,
                    dtZdrojPohyb[1].Modified,
                    MST_Global.TerminalID,
                    MST_Global.UserID,
                    dtZdrojPohyb[1].GUID,
                    dtZdrojPohyb[1].IsCinnostValueNull() ? null : dtZdrojPohyb[1].CinnostValue,
                    dtZdrojPohyb[1].IsCinnostTypeNull() ? null : dtZdrojPohyb[1].CinnostType,
                    dtZdrojPohyb[1].IsCountEntriesNull() ? (int?)null : dtZdrojPohyb[1].CountEntries,
                    dtZdrojPohyb[1].IsODB_IDNull() ? null : dtZdrojPohyb[1].ODB_ID,
                    dtZdrojPohyb[1].IsOkruhIDNull() ? null : dtZdrojPohyb[1].OkruhID,
                    dtZdrojPohyb[1].IsCinnostOznaceniNull() ? null : dtZdrojPohyb[1].CinnostOznaceni,
                    dtZdrojPohyb[1].IsGPS_XNull() ? (double?)null : dtZdrojPohyb[1].GPS_X,
                    dtZdrojPohyb[1].IsGPS_YNull() ? (double?)null : dtZdrojPohyb[1].GPS_Y,
                    dtZdrojPohyb[1].IsGPS_ZNull() ? (int?)null : dtZdrojPohyb[1].GPS_Z,
                    dtZdrojPohyb[1].IDZdroj
                    );

                    // odstraneni nejnovejsiho zaznamu
					Globals.globalObject.Controller_servis_ZdrojePohyb.Delete_ZdrojePohyb(lastRecordGuid);
                }
                else if (dtZdrojPohyb != null && dtZdrojPohyb.Count == 1)   // nedostatek zaznamu v neodeslanych datech, obnoveni cinnost id, ...
                {
                    Guid lastRecordGuid = zdroj.ZdrojGUID;

                    // vraceni zdrojstav na posledni zaznam
                    var dtZdrojStavDefault = defaultZdrojStav.Where(x => x.ZdrojID == zdroj.ZdrojID);
                    // existuje zaznam pro zmenu hodnot
                    if (dtZdrojStavDefault != null && dtZdrojStavDefault.Count() > 0)
                    {
                        cinnostValue = dtZdrojPohyb.First().IsCinnostValueNull() ? null : dtZdrojPohyb.First().CinnostValue;

						Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                        dtZdrojStavDefault.First().StavID,
                            //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                        dtZdrojStavDefault.First().IsCinnostIDNull() ? null : dtZdrojStavDefault.First().CinnostID,
                        dtZdrojStavDefault.First().ZdrojModified,
                        MST_Global.TerminalID,
                        MST_Global.UserID,
                        dtZdrojStavDefault.First().ZdrojGUID,
                        dtZdrojStavDefault.First().IsZdrojCinnostValueNull() ? null : dtZdrojStavDefault.First().ZdrojCinnostValue,
                        dtZdrojStavDefault.First().IsZdrojCinnostTypeNull() ? null : dtZdrojStavDefault.First().ZdrojCinnostType,
                        dtZdrojStavDefault.First().IsCountEntriesNull() ? (int?)null : dtZdrojStavDefault.First().CountEntries,
                        dtZdrojStavDefault.First().IsODB_IDNull() ? null : dtZdrojStavDefault.First().ODB_ID,
                        dtZdrojStavDefault.First().IsOkruhIDNull() ? null : dtZdrojStavDefault.First().OkruhID,
                        dtZdrojStavDefault.First().IsCinnostOznaceniNull() ? null : dtZdrojStavDefault.First().CinnostOznaceni,
                        dtZdrojStavDefault.First().IsGPS_XNull() ? (double?)null : dtZdrojStavDefault.First().GPS_X,
                        dtZdrojStavDefault.First().IsGPS_YNull() ? (double?)null : dtZdrojStavDefault.First().GPS_Y,
                        dtZdrojStavDefault.First().IsGPS_ZNull() ? (int?)null : dtZdrojStavDefault.First().GPS_Z,
                        dtZdrojStavDefault.First().ZdrojID
                        );
                        ZdrojeUpdate(zdroj);
						Globals.globalObject.Controller_servis_ZdrojePohyb.Delete_ZdrojePohyb(lastRecordGuid);
                    }
                    else   // zaznam v pomocne tabulce (puvodnich stavu) neexistuje 
                        throw new Exception("Není dostatek dat pro vrácení o krok zpět");




                    //// zustane zobrazen posledni dialog
                    //Globals._taZdrojeStav.Update(
                    //dtZdrojPohyb.First().IDStav,
                    //    //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                    //dtZdrojPohyb.First().IsIDCinnostNull() ? null : dtZdrojPohyb.First().IDCinnost,
                    //dtZdrojPohyb.First().Modified,
                    //MST_Global.TerminalID,
                    //MST_Global.UserID,
                    //dtZdrojPohyb.First().GUID,
                    //dtZdrojPohyb.First().IsCinnostValueNull() ? null : dtZdrojPohyb.First().CinnostValue,
                    //dtZdrojPohyb.First().IsCinnostTypeNull() ? null : dtZdrojPohyb.First().CinnostType,
                    //dtZdrojPohyb.First().IDZdroj
                    //);
                    //ZdrojeUpdate(zdroj);
                    //throw new Exception("Není dostatek dat pro vrácení o krok zpět");
                }
                else  // nedostatek zaznamu v neodeslanych datech
                {
                    var dtZdrojStavDefault = defaultZdrojStav.Where(x => x.ZdrojID == zdroj.ZdrojID);
                    if (dtZdrojStavDefault != null && dtZdrojStavDefault.Count() > 0)
                    {
                        //cinnostValue = dtZdrojStavDefault.First().IsZdrojCinnostValueNull() ? null : dtZdrojStavDefault.First().ZdrojCinnostValue;

						Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                        dtZdrojStavDefault.First().StavID,
                            //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                        dtZdrojStavDefault.First().IsCinnostIDNull() ? null : dtZdrojStavDefault.First().CinnostID,
                        dtZdrojStavDefault.First().ZdrojModified,
                        MST_Global.TerminalID,
                        MST_Global.UserID,
                        dtZdrojStavDefault.First().ZdrojGUID,
                        dtZdrojStavDefault.First().IsZdrojCinnostValueNull() ? null : dtZdrojStavDefault.First().ZdrojCinnostValue,
                        dtZdrojStavDefault.First().IsZdrojCinnostTypeNull() ? null : dtZdrojStavDefault.First().ZdrojCinnostType,
                        dtZdrojStavDefault.First().IsCountEntriesNull() ? (int?)null : dtZdrojStavDefault.First().CountEntries,
                        dtZdrojStavDefault.First().IsODB_IDNull() ? null : dtZdrojStavDefault.First().ODB_ID,
                        dtZdrojStavDefault.First().IsOkruhIDNull() ? null : dtZdrojStavDefault.First().OkruhID,
                        dtZdrojStavDefault.First().IsCinnostOznaceniNull() ? null : dtZdrojStavDefault.First().CinnostOznaceni,
                        dtZdrojStavDefault.First().IsGPS_XNull() ? (double?)null : dtZdrojStavDefault.First().GPS_X,
                        dtZdrojStavDefault.First().IsGPS_YNull() ? (double?)null : dtZdrojStavDefault.First().GPS_Y,
                        dtZdrojStavDefault.First().IsGPS_ZNull() ? (int?)null : dtZdrojStavDefault.First().GPS_Z,
                        dtZdrojStavDefault.First().ZdrojID
                        );
                        ZdrojeUpdate(zdroj);
                    }


                    throw new Exception("Není dostatek dat pro vrácení o krok zpět");
                }


                Fask.Logging.Log.Write("ZdrojeUpdate(zdroj) START");
                ZdrojeUpdate(zdroj);
                Fask.Logging.Log.Write("ZdrojeUpdate(zdroj) STOP");
                Fask.Logging.Log.Write("ZdrojeRestore(Data.ZdrojeStav.ZdrojStavRow zdroj) END");
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            finally
            {
                // nastaveni, aby se zaznam neobnovoval
                revertRecord = false;
            }
        }


        private void ZdrojeSave(Data.ZdrojeStav.ZdrojStavRow zdroj)
        {
            DateTime modified = DateTime.Now;
            Guid guid = Guid.NewGuid();
			Globals.globalObject.Controller_servis_ZdrojePohyb.Insert_ZdrojePohyb(
                zdroj.ZdrojID,
                zdroj.StavID,
                zdroj.IsCinnostIDNull() ? null : zdroj.CinnostID,
                modified,
                MST_Global.TerminalID,
                MST_Global.UserID,
                guid,
                zdroj.IsZdrojCinnostValueNull() ? null : zdroj.ZdrojCinnostValue, 
                zdroj.IsZdrojCinnostTypeNull() ? null : zdroj.ZdrojCinnostType,
                MST_Global.ServisDavkoveZpracovani ? Globals.globalObject.Davka : (int?)null,
                zdroj.IsODB_IDNull() ? null : zdroj.ODB_ID,
                zdroj.IsOkruhIDNull() ? null : zdroj.OkruhID,
                //selectedOdberatel != null ? selectedOdberatel.odb_id : null,
                //selectedOkruh != null ? selectedOkruh.ID : null,
                zdroj.IsCinnostOznaceniNull() ? null : zdroj.CinnostOznaceni,
                zdroj.IsGPS_XNull() ? (double?)null : zdroj.GPS_X,
                zdroj.IsGPS_YNull() ? (double?)null : zdroj.GPS_Y,
                zdroj.IsGPS_ZNull() ? (int?)null : zdroj.GPS_Z
                );

			Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                zdroj.StavID,
                //stavNext.IsIDCinnostNull() ? null : stavNext.IDCinnost,
                zdroj.IsCinnostIDNull() ? null : zdroj.CinnostID,
                modified,
                MST_Global.TerminalID,
                MST_Global.UserID,
                guid,
                zdroj.IsZdrojCinnostValueNull() ? null : zdroj.ZdrojCinnostValue, 
                zdroj.IsZdrojCinnostTypeNull() ? null : zdroj.ZdrojCinnostType,
				Globals.globalObject.Davka.HasValue ? Globals.globalObject.Davka : (int?)null ,
                zdroj.IsODB_IDNull() ? null : zdroj.ODB_ID,
                zdroj.IsOkruhIDNull() ? null : zdroj.OkruhID,
                //selectedOdberatel != null ? selectedOdberatel.odb_id : null,
                //selectedOkruh != null ? selectedOkruh.ID : null,
                zdroj.IsCinnostOznaceniNull() ? null : zdroj.CinnostOznaceni,
                zdroj.IsGPS_XNull() ? (double?)null : zdroj.GPS_X,
                zdroj.IsGPS_YNull() ? (double?)null : zdroj.GPS_Y,
                zdroj.IsGPS_ZNull() ? (int?)null : zdroj.GPS_Z,
                zdroj.ZdrojID
                );

			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtzP = Globals.globalObject.Controller_servis_ZdrojePohyb.GetDataByGUID_ZdrojePohyb(guid);

            ZdrojeUpdate(zdroj);
        }

        // pomala synchronizace, nesynchronizovala vsechny zaznamy
        //private void ZdrojeSynchronizeThread()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            ZdrojeSynchronize();

        //            ImagesSynchronize();

        //            System.Threading.Thread.Sleep(MST_Global.ServisAutoUpdateInterval);
        //        }
        //    }
        //    catch (System.Threading.ThreadAbortException taex)
        //    {
        //        // ok , to se ocekavalo ...
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //    }
        //}

        // zakomentovano, synchronizace nesynchronizovala tak jak mela
        //private object zdrojSynchronizeObjectLocker = new object();
        //private bool zdrojSynchronizeInProgress = false;
        //private void ZdrojeSynchronize()
        //{
        //    try
        //    {
        //        lock (zdrojSynchronizeObjectLocker)
        //        {
        //            zdrojSynchronizeInProgress = true;
        //            UpdateForm();
        //            Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter taZP = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
        //            taZP.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Globals.ServisZdrojePohybDB);
        //            Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtZP = taZP.GetData();
        //            ZdrojeSynchronize(dtZP);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //    }
        //    finally
        //    {
        //        zdrojSynchronizeInProgress = false;
        //        UpdateForm();
        //    }
        //}

        private void ZdrojeSynchronize(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtzP)
        {
            if (dtzP == null)
                return;

            foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybRow zP in dtzP)
            {
                try
                {
                    ZdrojeSynchronize(zP);
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                }
            }
        }

        private void ZdrojeSynchronize(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybRow zP)
        {
            ServisModuleWService.Zdroj zW = new Fask.MST_W.ServisModuleWService.Zdroj();
            zW.GUID = zP.GUID;
            zW.IDCinnost = zP.IsIDCinnostNull() ? null : zP.IDCinnost;
            zW.IDStav = zP.IDStav;
            zW.IDTerminal = (byte)zP.IDTerminal;
            zW.IDUser = zP.IDUser;
            zW.IDZdroj = zP.IDZdroj;
            zW.Modified = zP.Modified;
            zW.CinnostType = zP.IsCinnostTypeNull() ? null : zP.CinnostType;
            zW.CinnostValue = zP.IsCinnostValueNull() ? null : zP.CinnostValue;
            ServisModuleWService.StatusObject so = null;

			//SqlCEDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter taZP = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
			//taZP.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojePohybDB);

			//SqlCEDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter taZS = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
			//taZS.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojeStavDB);

            try
            {
                _WebRefernces_Globals.ServisModuleWServiceSession webServiceModule = new _WebRefernces_Globals.ServisModuleWServiceSession();
				webServiceModule.Url = Globals.globalObject.webServiceModule.Url;
                webServiceModule.Timeout = MST_Global.ServisTimeoutOnline;
                webServiceModule.UpdateWebServiceCredentials();

                so = webServiceModule.ProcessState(MST_Global.TerminalID, ref zW);
                if (so != null && so.StatusText == "OK")
                { // ulozeni se podarilo, tak pohyb smazat ...
					Globals.globalObject.Controller_servis_ZdrojePohyb.Delete_ZdrojePohyb(zP.GUID);
                }
                
                //pokud je stav zdroje novejsi nez aktualni, tak modifikovat ... 
				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable dtZS = Globals.globalObject.Controller_servis_ZdrojeStav.GetDataByIDZdroj_ZdrojStav(zW.IDZdroj);
                if (dtZS != null && dtZS.Count > 0 && dtZS[0].Modified < zW.Modified)
                {
                    dtZS[0].IDStav = zW.IDStav;
                    if (zW.IDCinnost == null)
                        dtZS[0].SetIDCinnostNull();
                    else
                        dtZS[0].IDCinnost = zW.IDCinnost;
                    
                    if (zW.GUID == null || zW.GUID == Guid.Empty)
                        dtZS[0].SetGUIDNull();
                    else
                        dtZS[0].GUID = zW.GUID;

                    if (zW.IDTerminal == 0)
                        dtZS[0].SetIDTerminalNull();
                    else
                        dtZS[0].IDTerminal = zW.IDTerminal;

                    if (zW.IDUser == 0)
                        dtZS[0].SetIDUserNull();
                    else
                        dtZS[0].IDUser = zW.IDUser;

                    dtZS[0].Modified = zW.Modified;

                    if (zW.CinnostType == null)
                        dtZS[0].SetCinnostTypeNull();
                    else
                        dtZS[0].CinnostType = zW.CinnostType;

                    if (zW.CinnostValue == null)
                        dtZS[0].SetCinnostValueNull();
                    else
                        dtZS[0].CinnostValue = zW.CinnostValue;

					Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(dtZS);
                }

                ZdrojeUpdate(zW.IDZdroj);
                //var dtZdrojStav = defaultZdrojStav.Where(x => x.ZdrojID == zP.IDZdroj);
                //if (dtZdrojStav != null && dtZdrojStav.Count() > 0)
                //{
                //    //defaultZdrojStav.Remove(dtZdrojStav.First());
                //    //defaultZdrojStav.Remove(dtZdrojStav.First());
                //    //defaultZdrojStav.First().Delete();
                //    defaultZdrojStav.Rows.Remove(dtZdrojStav.First());
                    
                //    defaultZdrojStav.AcceptChanges();
                //}

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                throw new Exception("Online synchronizace zdroje se nezdařila", ex);
            }
        }

        /// <summary>
        /// Pokusi se synchronizovat pohyb online ze serveru
        /// </summary>
        /// <param name="z">Aktualni vybrany zdroj</param>
        /// <exception>Vyjimka, pokud se nepovede online synchronizace</exception>
        private void ZdrojeSynchronize(Data.ZdrojeStav.ZdrojStavRow z)
        {
            ServisModuleWService.Zdroj zW = new Fask.MST_W.ServisModuleWService.Zdroj();
            zW.GUID = z.IsZdrojGUIDNull() ? Guid.Empty : z.ZdrojGUID;
            zW.IDCinnost = z.IsCinnostIDNull() ? null : z.CinnostID;
            zW.IDStav = z.StavID;
            zW.IDTerminal = MST_Global.TerminalID;
            zW.IDUser = MST_Global.UserID;
            zW.IDZdroj = z.ZdrojID;
            zW.Modified = z.ZdrojModified;
            zW.CinnostValue = z.IsZdrojCinnostValueNull() ? null : z.ZdrojCinnostValue;
            zW.CinnostType = z.IsZdrojCinnostTypeNull() ? null : z.ZdrojCinnostType;
            zW.CountEntries = z.IsCountEntriesNull() ? (int?)null : z.CountEntries;
            zW.ODB_ID = z.IsODB_IDNull() ? null : z.ODB_ID;
            zW.OkruhID = z.IsOkruhIDNull() ? null : z.OkruhID;


            ServisModuleWService.StatusObject so = null;
            try
            {
				so = Globals.globalObject.webServiceModule.ProcessState(MST_Global.TerminalID, ref zW);
                if (so != null && so.StatusText == "OK")
                { // aktualni stav zdroje ziskan ... 
                    //pokud je stav zdroje novejsi nez aktualni, tak modifikovat ... 
                    if (z.ZdrojModified < zW.Modified)
                    {
						Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStav(
                            zW.IDStav,
                            zW.IDCinnost,
                            zW.Modified,
                            zW.IDTerminal,
                            zW.IDUser,
                            zW.GUID,
                            zW.CinnostValue,
                            zW.CinnostType,
                            zW.CountEntries,
                            zW.ODB_ID,
                            zW.OkruhID,
                            zW.CinnostOznaceni,
                            zW.GPS_X,
                            zW.GPS_Y,
                            zW.GPS_Z,
                            zW.IDZdroj
                            );

                        ZdrojeUpdate(z);

                        //var dtZdrojStav = defaultZdrojStav.Where(x => x.ZdrojID == zW.IDZdroj);
                        //if (dtZdrojStav != null && dtZdrojStav.Count() > 0)
                        //{
                        //    //defaultZdrojStav.Remove(dtZdrojStav.First());
                        //    //defaultZdrojStav.Remove(dtZdrojStav.First());
                        //    defaultZdrojStav.Rows.Remove(dtZdrojStav.First());
                        //    defaultZdrojStav.AcceptChanges();
                        //}
                    }
                }
                else if (so != null)
                {
                    throw new Exception("Online synchronizace zdroje se nezdařila", new Exception(so.StatusText));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Online synchronizace zdroje se nezdařila.", ex);
            }
        }

        private void UpdateForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Action)delegate { this.UpdateForm(); });
                return;
            }

            try
            {


            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            try
            {
                if (MST_Global.ServisDavkoveZpracovani)
                {
                    this.Text = "Servis (" + Globals.globalObject.Davka + ")";
                }

                StringBuilder sb = new StringBuilder();
                // povoleni filtru, zobrazit vse/rozpracovane (nedokoncene)
                if (MST_Global.ServisFiltrZobrazeniPovolit)
                {
                    if (filtrZobrazitVse)
                        sb.Append("Z:V,");
                    else
                        sb.Append("Z:R,");

                    // TODO: Konfiguracne??
                    // pocet zbyvajicich zdroju
					int? pocetDokonceno = Globals.globalObject.Controller_servis_ZdrojeStav.CountDokonceno_ZdrojStavTmp();
                    sb.Append((zdrojeStav.ZdrojStav.Count - (pocetDokonceno ?? 0)) + "/" + zdrojeStav.ZdrojStav.Count + ",");
                    //zdrojeStav.ZdrojStav.Count
                }

                
                sb.Append("Zdrojů:" + bsZdrojeStav.Count);
                //sb.Append("Zdrojů:" + this.dataGrid1.VisibleRowCount);
                //sb.Append("Zdrojů:" + ((DataGrid) this.dataGrid1).VisibleRowCount. rows bsZdrojeStav.Count);

				int? pocetNeodeslanychPohybu = Globals.globalObject.Controller_servis_ZdrojePohyb.PocetPohybu_ZdrojePohyb();
                sb.Append(",Pohybů:" + (pocetNeodeslanychPohybu ?? 0));
                if (_uploadInProgress) // || zdrojSynchronizeInProgress )
                    sb.Append("(Synch)");

                // je vybrany okruh
                if (selectedOkruh != null)
                {
                    sb.Append(",Okruh:" + selectedOkruh.ID.Trim());
                }

                // je vybrany odberatel
                if (selectedOdberatel != null)
                {
                    sb.Append(",Odb:" + selectedOdberatel.odb_id.Trim());
                }

                statusBar1.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }



        delegate void DelegateString(string kod);
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                if (kod.Length <= 0)
                    return;

                this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void najdipolozku(string kod)
        {
            try
            {
                bsZdrojeStav.Filter = string.Empty;

                //System.Data.EnumerableRowCollection<Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow> zdroje;
                //if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
                //{
                //    zdroje = zdrojeStav.ZdrojStav.Where(z => z.ZdrojBarcode == kod && z.Rozpracovano < 100);
                //}
                //else
                //{
                //    zdroje = zdrojeStav.ZdrojStav.Where(z => z.ZdrojBarcode == kod);
                //}

                System.Data.EnumerableRowCollection<Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow> zdroje;
                if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
                {
                    zdroje = zdrojeStav.ZdrojStav.Where(z => !z.IsZdrojBarcodeNull() && z.ZdrojBarcode == kod && z.Rozpracovano < 100);
                }
                else
                {
                    zdroje = zdrojeStav.ZdrojStav.Where(z => !z.IsZdrojBarcodeNull() && z.ZdrojBarcode == kod);
                }

                if (zdroje.Count() == 0)
                {
                    if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
                        bsZdrojeStav.Filter = "Rozpracovano<100";   // nastaveni filtru zpet
                    
                    MessageBoxBig.Show("Nenalezen zdroj s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (zdroje.Count() > 1)
                {
                    bsZdrojeStav.Filter = "ZdrojBarcode='" + kod + "' ";
                    if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
                    {
                        bsZdrojeStav.Filter = "AND Rozpracovano<100";
                    }

                    //bsZdrojeStav.Filter = "ZdrojBarcode='" + kod + "'";
                    MessageBoxBig.Show("Nalezeno více zdrojů s kódem '" + kod + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                { // nalezen prave jeden ...
                    //bsZdrojeStav.Filter = "ZdrojBarcode='" + kod + "'";
                }

                // zde je jeden .. 
                Data.ZdrojeStav.ZdrojStavRow zdroj = zdroje.First();

                PerformInsert(zdroj);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_ServisModul)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        private void PerformInsert(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj)
        {
            try
            {
                ScannerStop();

                if (zdroj == null)
                {
                    MessageBoxBig.Show("Není vybrán zdroj", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                // Smycka pro Online zjisteni aktualniho stavu zdroje ... 
                if (MST_Global.ServisSynchronizaceZdrojePoVyberuPovolit)
                {
                    while (true)
                    {
                        try
                        {
                            ZdrojeSynchronize(zdroj);
                            break;
                        }
                        catch (Exception ex)
                        {
                            DialogResult drSynch = MessageBoxBig.Show(
                                "Opakovat pokus?\n" +
                                "Stav = '" + zdroj.StavNazev + "'" +
                                "\n" + ex.Message + (ex.InnerException == null ? "" : "\n" + ex.InnerException.Message),
                                this.Text,
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxBigIcon.Warning,
                                MessageBoxDefaultButton.Button2
                                );
                            if (drSynch == DialogResult.Cancel)
                            {
                                return;
                            }
                            else if (drSynch == DialogResult.Yes)
                            {
                                continue;
                            }
                            else //if (drSynch == DialogResult.No)
                            {
                                break;
                            }
                        }
                    }
                }
                // ulozeni pocatecniho stavu po synchronizaci
                cinnostValue = null;
                defaultZdrojStav.ImportRow(zdroj);
                defaultZdrojStav.AcceptChanges();
                try
                {
					Globals.globalObject.Controller_servis_ZdrojePohyb.Connection_Open();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "PerformInsert, ZdrojePohyb connection open");
                }
                try
                {
					Globals.globalObject.Controller_servis_ZdrojeStav.Connection_Open();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "PerformInsert, ZdrojeStav connection open");
                }

                // nastaveni rozpracovano
				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable dtZdrojStavTmp = Globals.globalObject.Controller_servis_ZdrojeStav.GetDataByZdrojID_ZdrojStavTmp(zdroj.ZdrojID);
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpRow rowtest = null;   // vybrany tmp radek ...
                if(dtZdrojStavTmp!= null && dtZdrojStavTmp.Count > 0)
                {
                    // nalezeno, ... nenastavuji na rozpracovano (i kdyz je dokonceno)
                    rowtest = dtZdrojStavTmp.First();
                    // TODO: neumoznit pokracovat, pokud je dokonceno???
                    zdroj.Rozpracovano = rowtest.Rozpracovano;
                }
                else
                {
                    // nenalezeno, vytvorim zaznam v tmp ...
                    rowtest = dtZdrojStavTmp.NewCZMST_Servis_ZdrojStavTmpRow();
                    rowtest.ZdrojID = zdroj.ZdrojID;
                    rowtest.Rozpracovano = MST_Global.TerminalID;
                    rowtest.SetDokoncenoNull();
					Globals.globalObject.Controller_servis_ZdrojeStav.Insert_ZdrojStavTmp(
                        rowtest.ZdrojID,
                        rowtest.Rozpracovano,
                        rowtest.IsDokoncenoNull() ? (DateTime?)null : rowtest.Dokonceno
                        );
                    zdroj.Rozpracovano = MST_Global.TerminalID;
                }
                

                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost = null;
                string prevCinnost = null;

                // TODO: moznost zrychleni pomoci uchovavani vytvoreneho formulare a pouze meneni textu??
                // nekonečný cyklus pro opětovné zadávání
                while (true)
                {
                    // naplneni status baru
                    FillStatusBarInfo(zdroj);

                    // zmena cinnosti
                    if (!zdroj.IsCinnostIDNull()) // porad je co tvorit ... 
                    {
                        if (zdroj.IsZdrojCinnostValueNull())
                        { // vybrat cinnost tuto cinnost
                            // je novy stav (cinnostvalue = null), dojde k hledani cinnosti, ktera je prirazena k danemu stavu
							Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable dtCinnost = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Cinnost(zdroj.CinnostID);
                            if (dtCinnost != null && dtCinnost.Count > 0)
                            {
                                cinnost = dtCinnost[0];
                            }
                            else
                                throw new Exception("Činnost '" + zdroj.CinnostID + "' nenalezena v číselníku činností!");
                        }
                        else
                        { // nabidnout vyber dalsi.
                            // PeV - preskoceni dialogu, kdyz je pouze jeden zaznam
							var dtCinnostNext = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_CinnostNext(zdroj.CinnostID);
                            if (dtCinnostNext.Count == 1 && !dtCinnostNext.First().IsIDNextNull())
                            {
                                //cinnost = Globals._taCiselnikCinnost.GetDataByID(dtCinnost.First().IDNext);
								var dtCinnost = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Cinnost(dtCinnostNext.First().IDNext);
                                if (dtCinnost.Count > 0)
                                {
                                    cinnost = dtCinnost.First();
                                }
                                else
                                    throw new Exception("Činnost s id: " + dtCinnostNext.First().ID + " nebyla nalezena");
                            }
                            else
                            {
                                // pokud je jen jedna, tak automaticky
                                // pokud neni dalsi, tak bude null => konec a dalsi stav?
                                using (ServisCinnostZmena szmena = new ServisCinnostZmena())
                                {
                                    szmena.Text = (zdroj.IsZdrojIDNull() ? string.Empty : zdroj.ZdrojID.Trim() + ": ") + szmena.Text;
                                    szmena.Zdroj = zdroj;
                                    szmena.StatusBarInfoText = statusBarInfo;

                                    DialogResult dr = szmena.ShowDialog();
                                    if (dr == DialogResult.Cancel)
                                        return;
                                    // krok zpet
                                    if (dr == DialogResult.Retry)
                                    {
                                        revertRecord = true;
                                    }
                                    
                                    cinnost = szmena.CinnostNext;
                                }
                            }
                        }
                        Logging.Log.Write("!zdroj.IsCinnostIDNull() STOP");
                    }
                    else
                    { // cinnost jiz neni, tak prechod na dalsi stav
                        cinnost = null; // <= znamena prechod do jineho stavu ...
                        zdroj.CinnostID = null;
                        zdroj.ZdrojCinnostType = null;
                        zdroj.ZdrojCinnostValue = null;
                    }

                    // vyplneni cinnosti
                    if (cinnost != null && !PerformCreateCinnost(zdroj, cinnost))
                    { // cinnost nebyla vytvorena, udelana
                        // tak konec na dotaz nebo hned zrusit ...
                        return;
                    }

                    // vraceni predchoziho zaznamu
                    if (revertRecord)
                    {
                        ZdrojeRestore(zdroj);
                        //cinnostValueRestore = ZdrojeRestore(zdroj);
                        // nastaveni na null, aby byla vybrana cinnost
                        //zdroj.ZdrojCinnostValue = null;
                        //preskocitZmenuCinnosti = true;
                        continue;
                    }
                    else
                    {
                        //cinnostValueRestore = null;
                        // ulozeni vyplnene hodnoty
                        cinnostValue = null;
                    }

                    // zmena stavu
                    if (cinnost == null)
                    {
                        if (prevCinnost != null)
                        {
                            // nastaveni rozpracovani na dokoneceno .... TODO: pridat nejaky priznak konecneho stavu??
							Globals.globalObject.Controller_servis_ZdrojeStav.Update_ZdrojStavTmp((byte)(MST_Global.TerminalID + 100), DateTime.Now, rowtest.ZdrojID);

                            zdroj.Rozpracovano = (byte)(MST_Global.TerminalID + 100);

                            // ulozeni pocatku noveho stavu ... jinak nedojde k vyberu cinnosti, ktera je prirazena k danemu stavu, ale rovnou k vyberu vsech variant
							var dtStavNext = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_StavNext(zdroj.StavID);
                            if (MST_Global.ServisAutomatickaZmenaStavuPovolit && dtStavNext.Count == 1)
                            {
								var dtStav = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Stav(dtStavNext.First().IDNext);
                                if (dtStav.Count > 0)
                                {
                                    zdroj.StavID = dtStav.First().ID;
                                    zdroj.CinnostID = dtStav.First().IsIDCinnostNull() ? null : dtStav.First().IDCinnost;
                                    zdroj.ZdrojCinnostType = null;
                                    zdroj.ZdrojCinnostValue = null;
                                    ZdrojeSave(zdroj);
                                }
                                else
                                    throw new Exception("Stav s id: " + dtStavNext.First().ID + " nebyl nalezen");
                            }

                            if (MST_Global.ServisZobrazitInformaciOUkonceniStavu)
                                MessageBoxBig.Show("Zadávání činností pro stav " + zdroj.StavNazev + " bylo dokončeno.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                            return;
                        }
                        //else ukoncitZadavani = true;
                        // 0. zjistit online aktualni stav zdroje
                        //  - pokud se nepovede, tak informovat a pripadne pokracovat s vybranym
                        //  - pokud se povede, tak akutalizovat a pokracovat
                        // 1. zobrazit informace o zdroji
                        // 2. zmena stavu
                        //  a. zobrazit vyber nasledujiciho stavu
                        //  b. doplnujici informace (cinnosti)
                        // 3. ulozit

                        // 2.a. zmena stavu 
                        // PeV - preskoceni dialogu, pokud je pouze jeden zaznam
						var dtStavNext2 = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_StavNext(zdroj.StavID);
                        // jestli ze je povoleno, nevraci se zaznam a je pouze jeden navazujici stav
                        if (MST_Global.ServisAutomatickaZmenaStavuPovolit && dtStavNext2.Count == 1)
                        {
							var dtStav = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Stav(dtStavNext2.First().IDNext);
                            if (dtStav.Count > 0)
                            {
                                zdroj.StavID = dtStav.First().ID;
                                zdroj.CinnostID = dtStav.First().IsIDCinnostNull() ? null : dtStav.First().IDCinnost;
                                zdroj.ZdrojCinnostType = null;
                                zdroj.ZdrojCinnostValue = null;
                            }
                            else
                                throw new Exception("Stav s id: " + dtStavNext2.First().ID + " nebyl nalezen");
                        }
                        else
                        {
                            using (ServisStavyZmena szmena = new ServisStavyZmena())
                            {
                                szmena.Text = (zdroj.IsZdrojIDNull() ? string.Empty : zdroj.ZdrojID.Trim() + ": ") + szmena.Text;
                                szmena.Zdroj = zdroj;
                                szmena.StatusBarInfoText = statusBarInfo;

                                DialogResult dr = szmena.ShowDialog();
                                if (dr == DialogResult.Cancel)
                                    return;
                                // krok zpet
                                if (dr == DialogResult.Retry)
                                {
                                    revertRecord = true;
                                }
                                // vraceni predchoziho zaznamu
                                if (revertRecord)
                                {
                                    ZdrojeRestore(zdroj); 
                                    //FillStatusBarInfo(zdroj);
                                    continue;
                                }
                                else
                                {
                                    cinnostValue = null;
                                }
                            }
                        }
                    }

                    ZdrojeSave(zdroj);

                    // po ulozeni nastaveni oznaceni cinnosti na null, aby nebylo stale nastaveno
                    zdroj.SetCinnostOznaceniNull();

                    if (MST_Global.ServisUkonceniStavuNavrat && cinnost != null)
                    {
                        prevCinnost = cinnost.Oznaceni;
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
                // odstraneni docasnych zaznamu
                defaultZdrojStav.Clear();
                defaultZdrojStav.AcceptChanges();

                try
                {
                    //if ((Globals.globalObject.Controller_servis_ZdrojePohyb.TaZdrojePohyb.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    //    Globals.globalObject.Controller_servis_ZdrojePohyb.TaZdrojePohyb.Connection.Close();
                    Globals.globalObject.Controller_servis_ZdrojePohyb.Connection_Close();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "PerformInsert ZdrojePohyb connection close");
                }

                try
                {
                    //if ((Globals.globalObject.Controller_servis_ZdrojeStav.TaZdrojeStav.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    //    Globals.globalObject.Controller_servis_ZdrojeStav.TaZdrojeStav.Connection.Close();
                    Globals.globalObject.Controller_servis_ZdrojeStav.Connection_Close();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "PerformInsert ZdrojeStav connection close");
                }

                try
                {
                    // pokus o odeslani dat, pokud je povoleno
                    if(MST_Global.ServisOdeslaniDatPoUkonceniZadavaniPovolit)
                        timerUploadCallBack(null);

                    // aktualizace rozpracovanych/nerozpracovanych dat
                    txtSearchZdroj_TextChanged(null, null);

                    UpdateForm();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "finalize problem");
                }
                ScannerStart();
            }

        }

        private bool PerformCreateCinnost(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            if (cinnost == null) 
                return false;

            switch (cinnost.TYPE)
            {
                case "A" : //Vytvori cinnost Zadani Textove hodnoty
                    return PerformCreateCinnostAlpha(zdroj, cinnost);
                //case "B": //Vytvori cinnost Zadani Textove hodnoty s tím, že jde zadat prázdná hodnota
                //    return PerformCreateCinnostAlpha(zdroj, cinnost, true);
                case "N": //Vytvori cinnost Zadani Ciselne hodnoty
                    return PerformCreateCinnostNumber(zdroj, cinnost);
                case "P": //Vytvori cinnost porizeni fotky
                    return PerformCreateCinnostPhoto(zdroj, cinnost);
                case "D": //Vytvori cinnost zobrazeni dynamicke databaze
                    return PerformCreateCinnostDatabase(zdroj, cinnost);
                case "V": // Vytvori cinnost zadani volby ano/ne
                    return PerformCreateCinnostVolba(zdroj, cinnost);
                default:
                    return false;
            }
        }

        private bool PerformCreateCinnostVolba(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            try
            {
                Logging.Log.Write("ServisMain", "PerformCreateCinnostVolba - try");
                using (ServisAnoNe san = new ServisAnoNe(string.Empty, cinnost.Oznaceni))
                {
                    san.StatusBarInfoText = statusBarInfo;
                    DialogResult dr = san.ShowDialog();
                    
                    if (dr == DialogResult.Cancel)
                        return false;
                    // krok zpet
                    if (dr == DialogResult.Retry)
                    {
                        revertRecord = true;
                    }
                    // ulozeni vyplnene hodnoty
                    cinnostValue = san.Volba.ToString();

                    Logging.Log.Write("ServisMain", "PerformCreateCinnostVolba - dialogresult.Yes/No");
                    zdroj.CinnostID = cinnost.ID;
                    zdroj.ZdrojCinnostType = cinnost.TYPE;
                    zdroj.ZdrojCinnostValue = san.Volba.ToString();
                }
            }
            finally
            {
                Logging.Log.Write("ServisMain", "PerformCreateCinnostVolba - finally");
            }
            return true;
        }

        private bool PerformCreateCinnostDatabase(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            try
            {
                Logging.Log.Write("ServisMain", "PerformCreateCinnostDatabase - try");
                // zjištění názvu tabulky podle typevalue
				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable dt_DynTab = Globals.globalObject.Controller_servis_Ciselniky.GetDataByTypeName_DynTabDef(cinnost.TYPEVALUE);
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow r_DynTab = null;
                if (dt_DynTab != null && dt_DynTab.Count > 0)
                    r_DynTab = dt_DynTab[0];

                if (r_DynTab != null)
                {
                    // cesta k dynamické tabulce
                    string path = Path.Combine(Main.DataDir, "Servis_Ciselniky" + r_DynTab.FullName + ".sdf");

                    if (!File.Exists(path))
                    {
                        MessageBoxBig.Show("Databázová tabulka " + r_DynTab.FullName + " není dostupná, je třeba aktualizovat číselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return false;
                    }
                    
                    //System.Data.SQLite.SQLiteConnection sqlConnection = null;

                    //sqlConnection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(path));
					//Globals.globalObject.Controller_servis_Ciselniky.TaDynTab.Connection = sqlConnection;

                    using (ServisDynamickaTabulka sDynTab = new ServisDynamickaTabulka(cinnost.Oznaceni.Trim(), r_DynTab.FullName))
                    {
                        sDynTab.StatusBarInfoText = statusBarInfo;

                        DialogResult dr = sDynTab.ShowDialog();
                        if (dr == DialogResult.Cancel)
                            return false;
                        // krok zpet
                        if (dr == DialogResult.Retry)
                        {
                            revertRecord = true;
                        }
                        // ulozeni vyplnene hodnoty
                        cinnostValue = sDynTab.DynamicTableRow.ID;
                        zdroj.ZdrojCinnostValue = sDynTab.DynamicTableRow.ID;
                        zdroj.CinnostOznaceni = sDynTab.DynamicTableRow.Oznaceni;
                    }
                    zdroj.CinnostID = cinnost.ID;
                    zdroj.ZdrojCinnostType = cinnost.TYPE;

                    return true;
                }
            }
            finally
            {
                Logging.Log.Write("ServisMain", "PerformCreateCinnostDatabase - finally");
            }
            return false;
        }

        private bool PerformCreateCinnostPhoto(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            Program.mstw.Photo.SetForm(this);   // kvuli ES400
            DialogResult dr = Program.mstw.Photo.CaptureServisImage(string.Empty, statusBarInfo);
            if (dr == DialogResult.Cancel)
                return false;
            // krok zpet
            if (dr == DialogResult.Retry)
            {
                revertRecord = true;
            }

            // ulozeni vyplnene hodnoty
            cinnostValue = Program.mstw.Photo.ImageFilename;

            zdroj.CinnostID = cinnost.ID;
            zdroj.ZdrojCinnostType = cinnost.TYPE;
            zdroj.ZdrojCinnostValue = Program.mstw.Photo.ImageFilename;

            return true;

            // 28.4.2016 PeV -> predelano na providera
            //if (this.phototype == Fask.MST_W.Components.PhotoManager.PhotoType.WinCE)
            //{
            //    using (ServisSejmiImageForm sif = new ServisSejmiImageForm())
            //    {
            //        sif.Text = statusBarInfo;
            //        DialogResult dr = sif.ShowDialog();
            //        if (dr == DialogResult.Cancel)
            //            return false;
            //        // krok zpet
            //        if (dr == DialogResult.Retry)
            //        {
            //            revertRecord = true;
            //        }
            //        // ulozeni vyplnene hodnoty
            //        cinnostValue = Path.GetFileName(sif.ImageFilename);

            //        zdroj.CinnostID = cinnost.ID;
            //        zdroj.ZdrojCinnostType = cinnost.TYPE;
            //        zdroj.ZdrojCinnostValue = Path.GetFileName(sif.ImageFilename); //Predpoklada se v Main.ImagesDir
            //        return true;
            //    }
            //}
            //else if (this.phototype == Fask.MST_W.Components.PhotoManager.PhotoType.WM)
            //{
            //    try
            //    {
            //        Microsoft.WindowsMobile.Forms.CameraCaptureDialog cameraCapture = new Microsoft.WindowsMobile.Forms.CameraCaptureDialog();
            //        cameraCapture.Owner = this;
            //        // ImageFilename = Path.Combine(Main.ImagesDir, Guid.NewGuid().ToString("N") + ".jpg");
                    
            //        // adresar pro ukladani
            //        cameraCapture.InitialDirectory = Main.ImagesDir;
 
            //        // It is necessary to end picture files with ".jpg".
            //        // Otherwise the argument is invalid.
            //        string filename = Guid.NewGuid().ToString("N") + ".jpg";
            //        cameraCapture.DefaultFileName = filename;

            //        // title
            //        cameraCapture.Title = statusBarInfo;

            //        // TODO: konfiguracne rozliseni foceni ...
            //        // 2048X1536 or a 3.1 Megapixel
            //        int resolutionWidth = 768;
            //        int resolutionHeight = 1024;
                    
            //        cameraCapture.Resolution = new Size(resolutionWidth, resolutionHeight);

            //        // Specify capture mode
            //        cameraCapture.Mode = Microsoft.WindowsMobile.Forms.CameraCaptureMode.Still;   // fotka...

            //        // Specify still quality
            //        cameraCapture.StillQuality = Microsoft.WindowsMobile.Forms.CameraCaptureStillQuality.High;  // kvalita ...

            //        // Displays the "Camera Capture" dialog box
            //        if (DialogResult.OK == cameraCapture.ShowDialog())
            //        {
            //            //filename = cameraCapture.FileName;

            //            // The method completed successfully.
            //            // u ES400 problem se zobrazenim messageboxu ihned po vyfoceni (dostane se do pozadi a aplikace zacne blbnout ...)
            //            //MessageBox.Show("Super, fotka pořízena!!\n\n" + filename,
            //            //    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

            //            // ulozeni vyplnene hodnoty
            //            cinnostValue = filename;

            //            zdroj.CinnostID = cinnost.ID;
            //            zdroj.ZdrojCinnostType = cinnost.TYPE;
            //            zdroj.ZdrojCinnostValue = filename; //Predpoklada se v Main.ImagesDir
            //            return true;
            //        }

            //        return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            //return true;
        }

        private bool PerformCreateCinnostNumber(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            using (ServisSejmiKodForm skf = new ServisSejmiKodForm(
                string.Empty, // statusbar
                cinnost.Oznaceni,
                ServisSejmiKodForm.TypeOfCode.Numeric,
                0,
                false,
                !Convert.ToBoolean(cinnost.Mandatory),      // je povinne zadat hodnotu
                string.IsNullOrEmpty(cinnostValue) ? string.Empty : cinnostValue,
                50,
                cinnost.IsRequiredLengthNull() ? false : true,
                cinnost.IsRequiredLengthNull() ? 0 : cinnost.RequiredLength
                ))
            {
                skf.StatusBarInfoText = statusBarInfo;
                DialogResult dr = skf.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return false;
                // krok zpet
                if (dr == DialogResult.Retry)
                {
                    revertRecord = true;
                }
                zdroj.CinnostID = cinnost.ID;
                zdroj.ZdrojCinnostType = cinnost.TYPE;
                zdroj.ZdrojCinnostValue = skf.Kod;

                // ulozeni vyplnene hodnoty
                cinnostValue = skf.Kod;
            }
            return true;
        }
        
        private bool PerformCreateCinnostAlpha(Fask.MST_W.ServisModule.Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostRow cinnost)
        {
            using (ServisSejmiKodForm skf = new ServisSejmiKodForm(
                zdroj.IsStavNazevNull() ? string.Empty : zdroj.StavNazev,
                cinnost.Oznaceni, 
                ServisSejmiKodForm.TypeOfCode.AlphaNumeric, 
                0, 
                false,
                !Convert.ToBoolean(cinnost.Mandatory),      // je povinne zadat hodnotu 
                string.IsNullOrEmpty(cinnostValue) ? string.Empty : cinnostValue, 
                50,
                cinnost.IsRequiredLengthNull() ? false : true,
                cinnost.IsRequiredLengthNull() ? 0 : cinnost.RequiredLength
                ))
            {
                skf.StatusBarInfoText = statusBarInfo;
                DialogResult dr = skf.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return false;
                // krok zpet
                if (dr == DialogResult.Retry)
                {
                    revertRecord = true;
                }
                // ulozeni vyplnene hodnoty
                cinnostValue = skf.Kod;

                zdroj.CinnostID = cinnost.ID;
                zdroj.ZdrojCinnostType = cinnost.TYPE;
                zdroj.ZdrojCinnostValue = skf.Kod;
            }
            return true;
        }

        //private void SaveZdroj(Data.ZdrojeStav.ZdrojStavRow zdroj, Fask.MST_W.ServisModuleWService.Zdroj zdrojNew)
        //{
        //    // 3. ulozit pohyb
        //    Fask.MST_W.ServisModuleWService.StatusObject so = null;
        //    // pokus o online zapis ...
        //    try
        //    {
        //        so = Globals._webServiceModule.ProcessState(ref zdrojNew);
        //        if (so.StatusText != "OK")
        //        {
        //            throw new Exception(so.StatusText); // hodi vyjimku, aby se dostal k ulozeni do lokalniho pohybu
        //        }
        //        else
        //        { // zapis se podaril, tak neudela zaznam do lokalniho pohybu
        //        }
        //    }
        //    catch (Exception exWeb)
        //    { // online se nepodarilo ...
        //        Logging.Log.Write(exWeb);

        //        // Ulozit do pohybu, jen pokud tento guid uz tam neni ...
        //        Fask.SQLiteDBs.DataSets.Servis_ZdrojePohyb.CZMST_Servis_ZdrojPohybDataTable dt_zdrojepohyb = Globals._taZdrojePohyb.GetDataByGUID(zdrojNew.GUID);
        //        if (dt_zdrojepohyb != null && dt_zdrojepohyb.Count > 0)
        //        { // jiz tam je, tak ho nevkladat znovu ... 
        //            // jenda se nejspis o akci odeslani lokalnich starych pohybu ...
        //        }
        //        else
        //        {
        //            Globals._taZdrojePohyb.Insert(
        //                zdrojNew.IDZdroj,
        //                zdrojNew.IDStav,
        //                zdrojNew.Modified,
        //                zdrojNew.IDTerminal,
        //                zdrojNew.IDUser,
        //                zdrojNew.GUID
        //                );
        //        }
        //    }

        //    // Aktualizovat lokalni, pokud je modified vetsi
        //    if (zdroj == null || //neni nastaven
        //        zdrojNew.Modified > zdroj.ZdrojModified
        //        )
        //    {
        //        Fask.SQLiteDBs.DataSets.Servis_ZdrojeStav ds_zdrojeStav = new Fask.SQLiteDBs.DataSets.Servis_ZdrojeStav();
        //        Globals._taZdrojeStav.FillByIDZdroj(ds_zdrojeStav.CZMST_Servis_ZdrojStav, zdrojNew.IDZdroj);
        //        if (ds_zdrojeStav.CZMST_Servis_ZdrojStav.Count == 0)
        //        { // neexistuje, tak pridam
        //            ds_zdrojeStav.CZMST_Servis_ZdrojStav.AddCZMST_Servis_ZdrojStavRow(
        //                zdrojNew.IDZdroj,
        //                zdrojNew.IDStav,
        //                zdrojNew.IDCinnost,
        //                zdrojNew.Modified,
        //                zdrojNew.IDTerminal,
        //                zdrojNew.IDUser,
        //                zdrojNew.GUID
        //                );
        //        }
        //        else 
        //        { // existuje, tak modifikuji / predokladam jeden zaznam ...
        //            Fask.SQLiteDBs.DataSets.Servis_ZdrojeStav.CZMST_Servis_ZdrojStavRow zdrojSql = ds_zdrojeStav.CZMST_Servis_ZdrojStav[0];
        //            zdrojSql.IDStav = zdrojNew.IDStav;
        //            zdrojSql.IDCinnost = zdrojNew.IDCinnost;
        //            zdrojSql.Modified = zdrojNew.Modified;
        //            zdrojSql.IDTerminal = zdrojNew.IDTerminal;
        //            zdrojSql.IDUser = zdrojNew.IDUser;
        //            zdrojSql.GUID = zdrojNew.GUID;
        //        }
        //        Globals._taZdrojeStav.Update(ds_zdrojeStav.CZMST_Servis_ZdrojStav);
        //    }

        //    // Pokud ulozeni na server proslo, tak smazu pohyb s guid ... 
        //    if (so != null && so.StatusText == "OK")
        //    { // smazat pohyb, protoze ulozeni uspelo ... 
        //        Globals._taZdrojePohyb.Delete(zdroj.ZdrojGUID);
        //    }

        //}

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
            try
            {
                //if ((Globals.globalObject.Controller_servis_ZdrojePohyb.TaZdrojePohyb.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                //    Globals.globalObject.Controller_servis_ZdrojePohyb.TaZdrojePohyb.Connection.Close();
                Globals.globalObject.Controller_servis_ZdrojePohyb.Connection_Close();

                ScannerFinalize();
                this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));

                Settings.ServisListZdrojeStavFiltrVse = filtrZobrazitVse;

                timerUploadStop();

                // pomala synchronizace, nesynchronizovala vsechny zaznamy
                //if (synchronizeThread != null)
                //{
                //    synchronizeThread.Abort();
                //    synchronizeThread.Join(5000);
                //    synchronizeThread = null;
                //}

                if (threadTimerUpload != null)
                {
                    try { threadTimerUpload.Join(1000); }
                    catch { }
                    try { threadTimerUpload.Abort(); }
                    catch { }
                    threadTimerUpload = null;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void ServisMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformInsert(this.ZdrojStavSelected);
            }
            else if (e.KeyCode == Keys.D1)
            {
                if(MST_Global.ServisFiltrZobrazeniPovolit)
                    menuItemFiltrVseRozpracovane_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void miAktualizaceCiselniky_Click(object sender, EventArgs e)
        {
            // aktualizace dat zdroju, stavu, stavnext ...
            try
            {
				ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Ciselniky(MST_Global.TerminalID);
                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return; // TODO : wait ... 
                }

                // stahnout data
				FileTransfer.Routines.DownloadDecompressDelete(Main.ServisCiselnikDB);

                downloadDynamicTables();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            

            ZdrojeFill();
        }

        private void miAktualizaceZdrojeStavy_Click(object sender, EventArgs e)
        {
            // aktualizace dat zdroju, stavu, stavnext ...
            System.Data.SQLite.SQLiteTransaction trans = null;
            System.Data.SQLite.SQLiteConnection conn = null;

            try
            {
                if (_uploadInProgress)
                {
                    MessageBoxBig.Show("Probíhá odesílání dat, prosím počkejte", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }

				ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Stavy(MST_Global.TerminalID);
                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return; // TODO : wait ... 
                }

                // 1) nacteni puvodnich stavu zdroju z ZdrojStavTmp
                // 2) stazeni ciselniku zdroju ze serveru
                // 3) naplneni stazeneho tmp souboru ZdrojStavTmp
                // 4) prepsani puvodni DB a odstraneni Tmp databazoveho souboru ...
                // 5) aktualizace zaznamu

				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable dtZdromStavTmp = Globals.globalObject.Controller_servis_ZdrojeStav.GetData_ZdrojStavTmp();
                
                // stahnout data do tmp ... 
                FileTransfer.Downloading.DownloadFileFromServer(Main.ServisZdrojeStavDB, false, false);

				//conn = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Main.ServisZdrojeStavDB + ".tmp"));                
                //SqlCEDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter taZdrojStavTmp = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter();
                //taZdrojStavTmp.Connection = conn;
                //conn.Open();
                //trans = conn.BeginTransaction(IsolationLevel.Serializable);
                //taZdrojStavTmp.MyTransaction = trans;
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav controller_zdrojestav_tmp = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(Main.ServisZdrojeStavDBTmp))
                {
                    try
                    {
                        controller_zdrojestav_tmp.Connection_Open();
                        trans = controller_zdrojestav_tmp.Connection.BeginTransaction(IsolationLevel.Serializable);
                        //controller_zdrojestav_tmp.TaZdrojeStavTmp.MyTransaction = trans;

                        foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpRow item in dtZdromStavTmp)
                        {
                            //taZdrojStavTmp.Insert(
                            controller_zdrojestav_tmp.Insert_ZdrojStavTmp(
                                item.ZdrojID,
                                item.Rozpracovano,
                                item.IsDokoncenoNull() ? (DateTime?)null : item.Dokonceno
                                );
                        }

                        if (trans != null)
                            trans.Commit();
                    }
                    catch (Exception exTrans)
                    {
                        try
                        {
                            if (trans != null)
                                trans.Rollback();
                        }
                        catch { }
                        throw exTrans;
                    }
                }

                // prepsani puvodniho souboru tmp souborem
                //string fileciselniktmp = Main.ServisZdrojeStavDB + ".tmp";
                string filetmp = Main.ServisZdrojeStavDBTmp;
                if (File.Exists(filetmp))
                {
                    // TODO : pokud je otevreny jiny controller, tak problem ... 
					File.Delete(Main.ServisZdrojeStavDB); //odmazani stareho
					File.Move(filetmp, Main.ServisZdrojeStavDB); //nahrani noveho ...
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                //if (conn != null && (conn.State == System.Data.ConnectionState.Open))
                //    conn.Close();
            }

            // aktualizace ze zdroj pohyb
            ZdrojeStavUpdate();
            ZdrojeFill();
        }

        private void txtSearchZdroj_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchZdroj.Text.Trim().Length == 0)
            {
                bsZdrojeStav.Filter = string.Empty;
                if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
                {
                    bsZdrojeStav.Filter += "Rozpracovano < 100 ";
                }
                return;
            }

            bsZdrojeStav.Filter = "ZdrojID like '%" + txtSearchZdroj.Text.Trim() + "%' ";

            if (MST_Global.ServisFiltrZobrazeniPovolit && !filtrZobrazitVse)
            {
                bsZdrojeStav.Filter += "AND Rozpracovano < 100 ";
            }
        }

        private void ImagesSynchronize()
        {
            try
            {
                _WebRefernces_Globals.ServisModuleWServiceSession webService = new _WebRefernces_Globals.ServisModuleWServiceSession();
				webService.Url = Globals.globalObject.webServiceModule.Url;
                webService.Timeout = MST_Global.ServisTimeoutSynchronize;
                webService.UpdateWebServiceCredentials();

                string[] images = Directory.GetFiles(Main.ImagesDir);

                Logging.Log.Write(String.Join("|", images), "ImageSynchronize(1)=>GetFiles)");

                foreach (var imageFile in images)
                {                    
                    //lock (SejmiImageForm.ImageLockObject)
                    //{
                        //byte[] imageData = MySystem.FileOperations.DBLoad(imageFile);

                        Logging.Log.Write(imageFile, "ImageSynchronize(2)=>Open and Load)");
                        //read imageData / if not locked ... 
                        byte[] imageData;
                        {
                            FileStream fs = null;
                            //byte[] data = new byte[0];
                            try
                            {
                                fs = new FileStream(
                                    imageFile,
                                    FileMode.Open,
                                    FileAccess.ReadWrite,
                                    FileShare.None);
                                long fileLen = (new FileInfo(imageFile)).Length;
                                imageData = new byte[fileLen];
                                int bytesRead = fs.Read(imageData, 0, (int)fileLen);
                            }
                            finally
                            {
                                if (fs != null)
                                {
                                    fs.Close();
                                    fs = null;
                                }
                            }
                        }

                        Logging.Log.Write(imageFile, "ImageSynchronize(3)=>Loaded and Sending");

                        ServisModuleWService.StatusObject so = webService.ImageArchivate(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(imageFile), imageData);
                        if (so.StatusText == "OK")
                        {
                            Logging.Log.Write(imageFile, "ImageSynchronize(4)=>Sended and Deleting");
                            File.Delete(imageFile);
                            Logging.Log.Write(imageFile, "ImageSynchronize(5)=>Deleted");
                        }
                    //} lock (Sejmi...

                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void timerUploadThreadStart()
        {
            try
            {
                try
                {
                    // pokud tmp Production neexistuje, dojde k jeho prekopirovani
					if (!File.Exists(Main.ServisZdrojePohybDBTmp))
                    {
                        File.Copy(
							Main.ServisZdrojePohybDB,
                            Main.ServisZdrojePohybDBTmp,
                            true
                            );
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "timerUploadCallBack(object state):File.Copy");
                    throw ex;
                }
                
                // naplnění Productiontmp daty
                #region ZdrojPohybTmp                
                // delete Production command

				using (var connection = new System.Data.SQLite.SQLiteConnection(SQLiteDBs.Controllers.SQLite_Static.SQLiteConnectionStringFormat(Main.ServisZdrojePohybDBTmp)))
                {
                    using (var sqlproductiontmpcmd = new System.Data.SQLite.SQLiteCommand(
                                "delete from CZMST_Servis_ZdrojPohyb",
                                connection
                                ))
                    {
                        try
                        {
                            sqlproductiontmpcmd.Connection.Open();
                            sqlproductiontmpcmd.ExecuteNonQuery();
                            sqlproductiontmpcmd.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Write(ex);
                        }
                        finally
                        {
                            if ((sqlproductiontmpcmd.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                                sqlproductiontmpcmd.Connection.Close();
                        }
                    }
                }

                Fask.SQLiteDBs.DataSets.Servis ds = new Fask.SQLiteDBs.DataSets.Servis();
                Fask.SQLiteDBs.DataSets.Servis dsTmp = new Fask.SQLiteDBs.DataSets.Servis();

                // insert ZdrojPohyb
                //using (Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter taPro = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter())
                using (var cntrlr_srvs_zrojepohyb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(Main.ServisZdrojePohybDB))
                {
					//taPro.Connection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Main.ServisZdrojePohybDB));

                    // kontrola, zdali jsou data k odeslani, pokud nejsou, tak se nic neposila ...
                    //int? pocetPohybu = taPro.PocetPohybu();
                    int? pocetPohybu = cntrlr_srvs_zrojepohyb.PocetPohybu_ZdrojePohyb();
                    if (pocetPohybu == null || pocetPohybu == 0)
                        return;

                    // defaultZdrojStav != null && Modified < defaultZdrojStav.Modified
                    // naplneni vsemi zaznamy
                    //taPro.Fill(ds.CZMST_Servis_ZdrojPohyb);

                    // naplneni pouze zaznamy, ktere se momentalne nezpracovavaji
                    if (defaultZdrojStav != null && defaultZdrojStav.Count > 0 && !defaultZdrojStav.First().IsZdrojModifiedNull())
                    {
                        //taPro.FillByModified(ds.CZMST_Servis_ZdrojPohyb, defaultZdrojStav.First().ZdrojModified);
                        cntrlr_srvs_zrojepohyb.FillByModified_ZdrojePohyb(ds.CZMST_Servis_ZdrojPohyb, defaultZdrojStav.First().ZdrojModified);
                    }
                    else  // naplneni vsemi daty
                        //taPro.Fill(ds.CZMST_Servis_ZdrojPohyb);
                        cntrlr_srvs_zrojepohyb.Fill_ZdrojePohyb(ds.CZMST_Servis_ZdrojPohyb);
                }

                foreach (var item in ds.CZMST_Servis_ZdrojPohyb)
                {
                    item.SetAdded();
                    dsTmp.CZMST_Servis_ZdrojPohyb.ImportRow(item);
                }

                //using (Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter taProTmp = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter())
                using(var cntrlr_servis_ZdrojePohybTmp = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(Main.ServisZdrojePohybDBTmp))
                {
                    //taProTmp.Connection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Main.ServisZdrojePohybTmpDB));
                    //taProTmp.Update(dsTmp.CZMST_Servis_ZdrojPohyb);
                    cntrlr_servis_ZdrojePohybTmp.Update_ZdrojePohyb(dsTmp.CZMST_Servis_ZdrojPohyb);
                }
                dsTmp.AcceptChanges();
                #endregion

                // TODO : predelani odesialni dat !!!
                // odeslani a zpracovani dat              
				bool processed = Globals.globalObject.webServiceModule.ProcessZdrojPohybData(
                                    MST_Global.TerminalID,
                                    MySystem.FileOperations.DBLoad(Main.ServisZdrojePohybDBTmp)
                                    );
                // data uspesne odeslana a pridana do databaze
                if (processed)
                {
                    //Odmazani odvedenych dat
                    #region ZdrojPohyb
                    System.Data.SQLite.SQLiteCommand sqlproductiontmp = null;
                    System.Data.SQLite.SQLiteCommand sqlproduction = null;
                    try
                    {
                        sqlproductiontmp = new System.Data.SQLite.SQLiteCommand(
                            "select guid from CZMST_Servis_ZdrojPohyb",
							new System.Data.SQLite.SQLiteConnection(SQLiteDBs.Controllers.SQLite_Static.SQLiteConnectionStringFormat(Main.ServisZdrojePohybDBTmp))
                            );
                        sqlproduction = new System.Data.SQLite.SQLiteCommand(
                            "Delete from CZMST_Servis_ZdrojPohyb where guid=@guid",
							new System.Data.SQLite.SQLiteConnection(SQLiteDBs.Controllers.SQLite_Static.SQLiteConnectionStringFormat(Main.ServisZdrojePohybDB))
                            );

                        System.Data.SQLite.SQLiteParameter sqlparamguid = sqlproduction.Parameters.Add("@guid", DbType.Guid);
                        sqlparamguid.Direction = ParameterDirection.Input;

                        sqlproductiontmp.Connection.Open();
                        sqlproduction.Connection.Open();

                        System.Data.SQLite.SQLiteDataReader preader = sqlproductiontmp.ExecuteReader();
                        while (preader.Read())
                        {
                            sqlparamguid.Value = preader[0];
                            int raff = sqlproduction.ExecuteNonQuery();
                        }
                        sqlproductiontmp.Connection.Close();
                        sqlproduction.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex, "AsyncCallbackVyrobaProcess(IAsyncResult ar) Production");
                    }
                    finally
                    {
                        if ((sqlproductiontmp != null) && ((sqlproductiontmp.Connection.State & ConnectionState.Open) == ConnectionState.Open))
                            sqlproductiontmp.Connection.Close();
                        if ((sqlproduction != null) && ((sqlproduction.Connection.State & ConnectionState.Open) == ConnectionState.Open))
                            sqlproduction.Connection.Close();

                        sqlproduction.Dispose();
                        sqlproductiontmp.Dispose();
                    }
                    #endregion

                    // TODO: osetrit, aby se fotky nenahravaly, pokud da uzivatel "krok zpet"
                    // synchronizace fotek
                    ImagesSynchronize();
                }

                //Po uspesnem provedeni to smazu
                //File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfTmp));
            }
            catch (Exception ex)
            {
                try { Logging.Log.Write(ex, "DownloadData End"); }
                catch { }
            }
            finally
            {
                try
                {
                    _uploadInProgress = false;
                }
                catch { }
                try
                {
                    //this.BeginInvoke(new DelegateUpdateStatusBarInfo(UpdateStatusBarInfo), new object[] { "Data výroby odeslána: " + DateTime.Now.ToString() });
                    UpdateForm();
                }
                catch { }
                //timerUploadStart();
                try
                {
                    //this.BeginInvoke((MethodInvoker)delegate() { timerUploadStart(); });
                    this.BeginInvoke((System.Action)delegate() { timerUploadStart(); });
                }
                catch { }
            }
        }

        private void timerUploadStart()
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke((MethodInvoker)delegate() { timerUploadStart(); });
                // System.Action
                this.BeginInvoke((System.Action)delegate() { timerUploadStart(); });
                return;
            }

            timerUploadStart(MST_Global.ServisAutoUpdateInterval);
        }

        private void timerUploadStop()
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke((MethodInvoker)delegate() { timerUploadStop(); });
                this.BeginInvoke((System.Action)delegate() { timerUploadStop(); });
                return;
            }
            if(timerUpload != null)
                timerUpload.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void timerUploadStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke((MethodInvoker)delegate() { timerUploadStart(nextrunmiliseconds); });
                this.BeginInvoke((System.Action)delegate() { timerUploadStart(nextrunmiliseconds); });
                return;
            }

            if(timerUpload != null)
                timerUpload.Change(nextrunmiliseconds, MST_Global.ServisAutoUpdateInterval);
        }

        System.Threading.Thread threadTimerUpload = null;
        private void timerUploadCallBack(object state)
        {
            try
            {
                if (_uploadInProgress)
                {
                    return;
                }

                //Nelze zacit nahravat data, pokud bezi stahovani, posune se za 2 sec...
                timerUploadStop();
                lock (lockUpDownTest)
                {
                    //if (_downloadInProgress)
                    //{
                    //    Logging.Log.WriteDebug("Download in progress", "Production Upload");

                    //    Logging.Log.WriteDebug("Timer upload restart at 2s", "Production Upload");
                    //    timerUploadStart(2000);
                    //    Logging.Log.WriteDebug("Upload returns - next in 2s", "Production Upload");
                    //    return;
                    //}
                    _uploadInProgress = true;
                }

                UpdateForm();
                //this.BeginInvoke(new DelegateUpdateStatusBarInfo(UpdateStatusBarInfo), new object[] { "Odesílají se data výroby: " + DateTime.Now.ToString() });

                //timerUploadThreadStart();
                threadTimerUpload = new Thread(new ThreadStart(timerUploadThreadStart));
                threadTimerUpload.Name = "threadTimerUpload_" + DateTime.Now.TimeOfDay.ToString();
                threadTimerUpload.IsBackground = true;
                threadTimerUpload.Priority = ThreadPriority.Lowest;
                threadTimerUpload.Start();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "timerUploadCallBack");
            }

        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            timerUploadCallBack(null);
        }

        private void ServisMain_Closing(object sender, CancelEventArgs e)
        {
            finalize();
        }

        private void menuItemFiltrVseRozpracovane_Click(object sender, EventArgs e)
        {
            try
            {
                filtrZobrazitVse = !filtrZobrazitVse;
                // zmena filtru
                txtSearchZdroj_TextChanged(null, null);
                // zmena textu ve statusbaru
                UpdateStatusBar();
                //FillStatusBarInfo(ZdrojStavSelected);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }
    }
}