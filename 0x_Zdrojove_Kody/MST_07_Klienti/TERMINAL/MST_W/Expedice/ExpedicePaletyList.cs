using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using Fask.Graphic;
using Fask.Parsing.Codes;

namespace Fask.MST_W.Expedice
{
    public partial class ExpedicePaletyList : Form
    {
        private SejmiKodForm skf = null;
        
        // nactena data z online metody
        Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        //private ExpediceService.Expedice dsExpedice = new Fask.MST_W.ExpediceService.Expedice();
        private _WebRefernces_Globals.ExpediceSeviceSession wsExpedice = null;

        /// <summary>
        /// PPriznak tisku.
        /// </summary>
        private bool printed = false;

        public ExpedicePaletyList(Fask.MST_W.ExpediceService.ExpediceHlavicky.CZMST_Expedice_HlavickaRow Hlavicka, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            InitializeComponent();
            try
            {
                this.sklad = sklad;
                this._Hlavicka = Hlavicka;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberPrijmoveLokaceList load");
            }
        }

        /// <summary>
        /// Vybrana hlavicka
        /// </summary>
        private Fask.MST_W.ExpediceService.ExpediceHlavicky.CZMST_Expedice_HlavickaRow _Hlavicka = null;

        
        private Fask.MST_W.ExpediceService.Expedice.Expedice_PaletyRow _Polozka
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsPolozky].Current)).Row as Fask.MST_W.ExpediceService.Expedice.Expedice_PaletyRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void PrijemVyberPrijmoveLokaceList_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                wsExpedice = new _WebRefernces_Globals.ExpediceSeviceSession();
                wsExpedice.Url = MST_Global.ServerAddress + "Expedice.asmx";
                wsExpedice.Timeout = MST_Global.ServiceTimeOut;
                wsExpedice.UpdateWebServiceCredentials();

                //Cursor.Current = Cursors.WaitCursor;
                InitializeDataGridView();

                InitializeGrid();

                bsPolozky.DataSource = dsExpedice.Expedice_Palety;
                dataGrid1.DataSource = bsPolozky;

                panelButtons_Resize(null, null);
            }
            catch (Exception ex)
            {
                //Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                timerLoad.Enabled = true;
            }
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsExpedice.Expedice_Palety.TableName;

            DataGrid2TextBoxColumn dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "SSCC palety";
            dgtbc.MappingName = dsExpedice.Expedice_Palety.NMBRPALColumn.ColumnName;
            dgtbc.NullText = "-";
            dgtbc.Width = 150;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Typ palety";
            dgtbc.MappingName = dsExpedice.Expedice_Palety.TYPEPALColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Položek";
            dgtbc.MappingName = dsExpedice.Expedice_Palety.SumItemsColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Váha";
            dgtbc.MappingName = dsExpedice.Expedice_Palety.SumWeightColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dataGrid1.TableStyles.Add(ts);
        }

        private void InitializeGrid()
        {
            this.dataGrid1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid1.Font = new Font(this.dataGrid1.Font.Name, Settings.UIGridFont, this.dataGrid1.Font.Style);
            this.dataGrid1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PrijemVyberPrijmoveLokaceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformDokoncitDavku();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Back)
            {
                PerformDeletePolozka();
            }
            //else if (e.KeyCode == Keys.F1)
            //{
            //    miZobrazitStavSkladu_Click(null, null);
            //}
            else if (e.KeyCode == Keys.F2)
            {
                miAktualizovat_Click(null, null);
            }
            else if (e.KeyCode == Keys.D4)
            {
                if (Expedice.Globals.PovolitTiskSoupisu)
                    PerformTiskSoupis();
            }
            //else if (e.KeyCode == Keys.F4)
            //{
            //    miNajitBarcode_Click(null, null);
            //}
            //else if (e.KeyCode == Keys.F5)
            //{
            //    miNajitItemnmbr_Click(null, null);
            //}
            //else if (e.KeyCode == Keys.F7)
            //{
            //    miPaletaZmenit_Click(null, null);
            //}
            else
                return;

            e.Handled = true;
        }

        private void PerformDokoncitDavku()
        {
            try
            {
                ScannerStop();

                // kontrola, zdali byl prepravni list vytisknut
                if (Expedice.Globals.PovolitTiskSoupisu && !printed)
                {
                    DialogResult dr = MessageBoxBig.Show("Nebyl proveden tisk přepravního listu.\nChcete přepravní list vytisknout?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                    if (dr == DialogResult.Cancel)
                        return;
                    else if (dr == DialogResult.Yes)
                    {
                        bool print = PerformTiskSoupis();
                        // chyba tisku, neodesilat data ...
                        if (!print)
                            return;
                    }
                }
                
                if (MessageBoxBig.Show("Opravdu chcete odeslat data dávky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                Cursor.Current = Cursors.WaitCursor;

                wsExpedice.Expedice_Process(
                    MST_Global.TerminalID,
                    MST_Global.UserID,
                    sklad != null ? sklad.skl_id : string.Empty,
                    _Hlavicka.ID, 
                    Fask.MST_W.ExpediceService.ProcessState.Zpracovat
                    );

                Cursor.Current = Cursors.Default;                
                MessageBoxBig.Show(string.Format("Data dávky odeslána"), Fask.Localization.Localization.Prijem4PrijemMainOdesilaniDat, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                // ukonceni scanneru
                //ScannerFinalize();
                finalize();
                this.DialogResult = DialogResult.OK;
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

        private void PerformCancel()
        {
            finalize();
            
            //if (skf != null)
            //{
            //    try
            //    {
            //        skf.Dispose();
            //        skf = null;
            //    }
            //    catch { }
            //}

            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {
            ScannerFinalize();

            this.dataGrid1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        #region scanner
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

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdipolozku(string kod)
        {
            try
            {
                ScannerStop();

                string ck = kod.Trim();

                Cursor.Current = Cursors.WaitCursor;
                Fask.MST_W.ExpediceService.Expedice tables = wsExpedice.Expedice_Paleta_Get(MST_Global.TerminalID, MST_Global.UserID, Expedice.Globals.SkladID, kod);
                Cursor.Current = Cursors.Default;

                if (tables.Expedice_Palety.Count == 0)
                {
                    MessageBoxBig.Show(string.Format("Paleta nebyla nalezena online", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Expedice_Palety.Count > 1)
                {
                    // vzit prvni nalezenou ... TODO: predelat??
                    PerformPridatPolozku(tables.Expedice_Palety[0]);
                }
                else
                {
                    PerformPridatPolozku(tables.Expedice_Palety[0]);
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
            if (MST_Global.OnScannerSound_Expedice)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }

            }

        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show("Opravdu chcete ukončit zadávání?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) 
                == DialogResult.No)
                return;

            PerformCancel();
        }

        private void PerformPridatPolozku(Fask.MST_W.ExpediceService.Expedice.Expedice_PaletyRow polozka)
        {
            if (polozka == null)
            {
                MessageBoxBig.Show("Není vybrána položka", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            try
            {
                ScannerStop();

                bool state = true;
                while (state)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int res = wsExpedice.Expedice_Polozka_Add(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID, polozka.NMBRPAL);

                        dsExpedice.Expedice_Palety.ImportRow(polozka);
                        dsExpedice.Expedice_Palety.AcceptChanges();

                        Cursor.Current = Cursors.Default;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, PerformPridatPolozku");
                        if (MessageBoxBig.Show(string.Format("Položku se nepodařilo přidat online.\n{0}\nOpakovat?", ex.Message), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                            != DialogResult.Yes)
                            return;
                    }
                }
                int pos = bsPolozky.Find(dsExpedice.Expedice_Palety.NMBRPALColumn.ColumnName, polozka.NMBRPAL);

                dataGrid1.CurrentRowIndex = pos;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                UpdateStatusBar();
                ScannerStart();
            }
        }

        private void miDynamickaTabulka_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void zpet_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click_1(object sender, EventArgs e)
        {

        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformDokoncitDavku();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show("Opravdu chcete ukončit zadávání?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;

            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                dsExpedice = OnlineGetPalety(sklad != null ? sklad.skl_id : string.Empty);
                //if (hlavicky != null)
                //{
                //    bsHlavicky = new BindingSource();
                //    bsHlavicky.DataSource = hlavicky.CZMST_Expedice_Hlavicka;
                //    dataGrid1.DataSource = bsHlavicky;
                //}
                if (dsExpedice == null)
                    dsExpedice = new Fask.MST_W.ExpediceService.Expedice();

                bsPolozky = new BindingSource();
                bsPolozky.DataSource = dsExpedice.Expedice_Palety;
                dataGrid1.DataSource = bsPolozky;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení položek se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
            try
            {
                dataGrid1.Focus();
                dataGrid1.CurrentRowIndex = dataGrid1.CurrentRowIndex;
            }
            catch
            {
            }
        }

        private void ExpediceVyberHlavickyList_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            miTisk.Enabled = miTiskSoupis.Enabled = Expedice.Globals.PovolitTiskSoupisu;

            try
            {
                // online nacteni dat
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, ExpediceVyberHlavickyList_Shown");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

            ScannerStart();

            Cursor.Current = Cursors.Default;
        }

        private void miOdstranitPrikaz_Click(object sender, EventArgs e)
        {
            PerformDeletePolozka();
        }

        /// <summary>
        /// metoda pro odstraneni hlavicky
        /// </summary>
        private void PerformDeletePolozka()
        {
            if (_Polozka == null)
                return;

            try
            {
                ScannerStop();

                if (MessageBoxBig.Show(string.Format("Opravdu chcete odstranit paletu '{0}'?", (_Polozka.IsNMBRPALNull() ? string.Empty : _Polozka.NMBRPAL)), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                int res = wsExpedice.Expedice_Polozka_Del(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID, _Polozka.NMBRPAL);

                dsExpedice.Expedice_Palety.RemoveExpedice_PaletyRow(_Polozka);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformDeletePolozka");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                UpdateStatusBar();
                ScannerStart();
            }
        }

        private void miZobrazitStavSkladu_Click(object sender, EventArgs e)
        {
        }

        private void miPaletaGenerovat_Click(object sender, EventArgs e)
        {
        }

        private void UpdateStatusBar()
        {
            try
            {
                sbInfo.Text = string.Empty;
                //sbInfo.Text = "N:" + polozky.CZMST_Expedice_Baleni_Polozky.Count;

                //sbInfo.Text += ", P:" + (nmbrpal != null ? nmbrpal.Number : "-");

                //if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou)
                //    sbInfo.Text += ", SN:" + (string.IsNullOrEmpty(serltnum) ? "-" : serltnum);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, UpdateStatusBar");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void miPaletaZmenit_Click(object sender, EventArgs e)
        {

        }

        private void miPaletaZmenit_Click_1(object sender, EventArgs e)
        {

        }

        private void miNajit_Click(object sender, EventArgs e)
        {
            try
            {
                PerformHledatKodPalety();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, miNajit_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformHledatKodPalety()
        {
            try
            {
                ScannerStop();
                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Kód palety", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte SSCC kód palety";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                        return;

                    kod = skf.Kod;
                }


                najdipolozku(kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, miNajit_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally 
            {
                ScannerStart();
            }
        }

        private void miTiskSoupis_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Expedice.Globals.PovolitTiskSoupisu)
                    return;

                PerformTiskSoupis();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.BaleniPolozkyList, miTiskPaleta_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private Fask.MST_W.ExpediceService.Expedice OnlineGetPalety(string skl_id)
        {
            try
            {
                return wsExpedice.Expedice_GetPalety(MST_Global.TerminalID, MST_Global.UserID, skl_id, _Hlavicka.ID);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, OnlineGetPalety");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
        }

        // 12.7.2016 PeV: nepouziva se, tisknout se pouze palety a ne polozky na paletach
        // 1.12.2016 JiS: opet se chce pouzivat, => ale uprava i tiskoveho reportu na serveru ...
        private Fask.MST_W.ExpediceService.Expedice OnlineGetPolozky(string skl_id)
        {
            try
            {
                return wsExpedice.Expedice_GetPolozky(MST_Global.TerminalID, MST_Global.UserID, skl_id, _Hlavicka.ID);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePaletyList, OnlineGetPolozky");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
        }

        private bool PerformTiskSoupis()
        {
            try
            {
                bool vytisteno = false;

                ScannerStop();
                if (_Polozka == null)
                {
                    MessageBoxBig.Show("Není vybrán nasnímaný záznam", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return false;
                }

                // 12.7.2016 PeV: nepouziva se, tisknout se pouze palety a ne polozky na paletach
                // 1.12.2016 JiS: opet se chce pouzivat, => ale uprava i tiskoveho reportu na serveru ...
                // ziskani polozek online
                Fask.MST_W.ExpediceService.Expedice polozky = OnlineGetPolozky(sklad != null ? sklad.skl_id : string.Empty);

                //  tisk soupisu
                Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
                List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
                Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

                // naplneni dat hlavicky
                foreach (System.Data.DataColumn dcol in _Hlavicka.Table.Columns)
                {
                    if (!dataHlavicka.ContainsKey(dcol.ColumnName.ToUpper()))
                    {
                        dataHlavicka.Add(dcol.ColumnName.ToUpper(), _Hlavicka[dcol.ColumnName].ToString().Trim());
                    }
                }


                // 1.12.2016 JiS ??? dataHlavicka.Add("NMBRPAL", (_Polozka != null && _Polozka.IsNMBRPALNull()) ? string.Empty : _Polozka.NMBRPAL.Trim());
				//using (Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter taUziv = new Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter())
				//{
				//    //taUziv.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Main.CiselnikUzivateleDB);
				//    taUziv.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUzivateleDB);
				//    var dtUziv = taUziv.GetDataByLogin(MST_Global.UserLoginName);
				//    if (dtUziv.Count > 0)
				//    {
				//        dataHlavicka.Add("LOGIN", MST_Global.UserLoginName ?? string.Empty);
				//        dataHlavicka.Add("FIRSTNAME", dtUziv.First().IsFIRSTNAMENull() ? string.Empty : dtUziv.First().FIRSTNAME.Trim());
				//        dataHlavicka.Add("SECONDNAME", dtUziv.First().IsSECONDNAMENull() ? string.Empty : dtUziv.First().SECONDNAME.Trim());
				//    }
				//}

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users usr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Main.CiselnikUzivateleDB))
				{
					var dtUziv = usr.GetDataByLogin(MST_Global.UserLoginName);
					if (dtUziv.Count > 0)
					{
						dataHlavicka.Add("LOGIN", MST_Global.UserLoginName ?? string.Empty);
						dataHlavicka.Add("FIRSTNAME", dtUziv.First().IsFIRSTNAMENull() ? string.Empty : dtUziv.First().FIRSTNAME.Trim());
						dataHlavicka.Add("SECONDNAME", dtUziv.First().IsSECONDNAMENull() ? string.Empty : dtUziv.First().SECONDNAME.Trim());
					}
				}


                // naplneni dat polozek
                foreach (DataRow row in polozky.CZMST_Expedice_Polozky)
                //foreach (DataRow row in dsExpedice.Expedice_Palety)
                {
                    Dictionary<string, string> dataRadek = new Dictionary<string, string>();

                    // naplneni dat polozek
                    foreach (System.Data.DataColumn dcol in row.Table.Columns)
                    {
                        if (!dataRadek.ContainsKey(dcol.ColumnName.ToUpper()))
                        {
                            //dataRadek.Add(dcol.ColumnName.ToUpper(), _Hlavicka[dcol.ColumnName].ToString().Trim());
                            dataRadek.Add(dcol.ColumnName.ToUpper(), row[dcol.ColumnName].ToString().Trim());
                        }
                    }

                    dataRadky.Add(dataRadek);
                }

                // naplneni dat paticky

                // 30.6.2016 PeV: na zadost JaS automaticke predvyplneni mnozstvi tisku 1, TODO: konfiguracne ...
                // tisk
                return printed = vytisteno = ExpediceTisk.PrintSoupisSendToPrinter(dataHlavicka, dataRadky, dataPaticka, 1);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PerformTiskSoupis");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}