using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemVyberPrijmoveLokaceList : Form
    {
        // nactena data z online metody
        Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        private bool autoUseOne = true;
        //private Fask.MST_W._WebRefernces_Globals.LokaceServiceSession wsLokace = null;
        private Fask.MST_W.LokaceService.Location ds = new Fask.MST_W.LokaceService.Location();

        private BindingSource bs;

        public PrijemVyberPrijmoveLokaceList(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
        {
            InitializeComponent(); 
            try
            {
                this.sklad = sklad;                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberPrijmoveLokaceList load");
            }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="sklad">Sklad.</param>
        /// <param name="autoUseOne">Pokud je pouze jedna prijmova lokace, automaticky ji pouzit.</param>
        public PrijemVyberPrijmoveLokaceList(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad, bool autoUseOne) :this(sklad)
        {
            //InitializeComponent();
            try
            {
                this.autoUseOne = autoUseOne;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrijemVyberPrijmoveLokaceList load");
            }
        }

        /// <summary>
        /// Uzivatelem vybrana lokace.
        /// </summary>
        public Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow PrijmovaLokace { get; set; }

        // vybrany radek v seznamu doporucenych lokaci
        private Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow _prijmovaLokace
        {
            get
            {
                try
                {
                    //return ((DataRowView)(dataGrid1.BindingContext[bsServis].Current)).Row as Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableRow;
                    return ((DataRowView)(dataGrid1.BindingContext[bs].Current)).Row as Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow;
                    //return null;

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
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

                //Cursor.Current = Cursors.WaitCursor;

                CreateGridStyles();

                InitializeGrid();

                ScannerStart();
                panelButtons_Resize(null, null);

                // online nacteni dat
                PerformUpdate();

                // Pokud je pouze jedna polozka, tak ji automaticky vybrat
                // TODO : Prijem4.PrijemVyberPrijmoveLokaceList jak se chovat pro automaticky vyber, pokud je jedna?
                // ? Otazka konfigurace, zda se to ma dit
                // ? Zobrazit casovy dialog s aktualni vybranou
                // ? bez hlaseni
                if (autoUseOne && bs.Count == 1) // pocet v bindigu je 1 (po filtrech)
                { // tak automaticky vybrat ...
                    PerformOK();
                }
            }
            catch (Exception ex)
            {
                //Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void CreateGridStyles()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = ds.CZMST_SkladLokace_Mapa.TableName; 

            Fask.Graphic.DataGrid2TextBoxColumn dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Sklad ID";
            dg.MappingName = ds.CZMST_SkladLokace_Mapa.SKL_IDColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Lokace";
            dg.MappingName = ds.CZMST_SkladLokace_Mapa.LOCNCODEColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new Fask.Graphic.DataGrid2TextBoxColumn();
            dg.HeaderText = "Typ";
            dg.MappingName = ds.CZMST_SkladLokace_Mapa.TYPEColumn.ColumnName;
            dg.NullText = "-";
            //dg.SelectionShow = true;
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

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
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.F1)
            {
                miNajitLokaci_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                miAktualizovat_Click(null, null);
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformCancel()
        {
            finalize(); 
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
                bs.Filter = string.Empty;
                string selectcmd = "LOCNCODE = '" + kod + "'";
                var tables = ds.CZMST_SkladLokace_Mapa.Select(selectcmd);
                
                if (tables.Count() == 0)
                {
                    MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                    
                    //Fask.MST_W.LokaceService.Location dsLokace = new Fask.MST_W.LokaceService.Location();
                    //Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow lokaceRow = dsLokace.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();
                    //lokaceRow.SKL_ID = sklad != null ? sklad.skl_id : string.Empty;
                    //lokaceRow.LOCNCODE = kod;
                    //lokaceRow.IS_RECEIVE = 0;
                    //lokaceRow.DEX_ROW_ID = 1;
                    //lokaceRow.SetTYPENull();

                    //PrijmovaLokace = lokaceRow;
                    //finalize();
                    //DialogResult = DialogResult.OK;
                }
                else if (tables.Count() > 1)
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNalezenoViceLokaci, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else
                {
                    bs.Filter = "LOCNCODE='" + kod + "'";
                    PerformOK();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Prijem_4)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        #endregion scanner

        private void miKonec_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void PerformOK()
        {
            try
            {
                if (_prijmovaLokace == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemVyberLokaceListNeniVybranaLokace, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                PrijmovaLokace = _prijmovaLokace;

                Settings.PrijemPrijmovaLokaceDefault = PrijmovaLokace.LOCNCODE;

                finalize();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        private Fask.MST_W.LokaceService.Location OnlineGetPrijmoveLokace(string skl_id)
        {
            Fask.MST_W.LokaceService.Location lokace;
            
            try
            {
                //wsLokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
                //wsLokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
                //wsLokace.Timeout = MST_Global.ServiceTimeOut;
                //wsLokace.UpdateWebServiceCredentials();

                //lokace = wsLokace.ShowReceiveLocations(skl_id);
                lokace = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.ShowReceiveLocations(skl_id);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemVyberPrijmoveLokaceList, OnlineGetPrijmoveLokace");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            return lokace;
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemVyberPrijmoveLokaceList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                ds = OnlineGetPrijmoveLokace(sklad != null ? sklad.skl_id : string.Empty);
                if (ds != null)
                {
                    bs = new BindingSource();
                    bs.DataSource = ds.CZMST_SkladLokace_Mapa;
                    dataGrid1.DataSource = bs;

                    // najiti prijmove lokace, pokud byla zvolena
                    try
                    {
                        if (!string.IsNullOrEmpty(Settings.PrijemPrijmovaLokaceDefault))
                        {
                            int index = bs.Find("LOCNCODE", Settings.PrijemPrijmovaLokaceDefault);
                            // nemelo by tady spise byt bs.Position?
                            //dataGrid1.CurrentRowIndex = index; 
                            bs.Position = index;
                        }
                        else
                            bs.Position = 0; // na prvni polozku ... ???
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení lokací se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

        private void miNajitLokaci_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Lokace", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte lokaci";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        ScannerStart();
                        return;
                    }

                    kod = skf.Kod;
                }

                najdipolozku(kod);
                // kontrola, zdali je lokace v seznamu, pokud neni, zobrazi se dotaz
                //string selectcmd = "LOCNCODE = '" + kod + "'";
                //var tables = ds.CZMST_SkladLokace_Mapa.Select(selectcmd);
                //if (tables.Count() == 0)
                //{
                //    DialogResult dr = MessageBoxBig.Show("Lokace '" + kod + "' nebyla nalezena v seznamu.\nChcete ji přesto použít?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information);
                //    if (dr != DialogResult.Yes)
                //    {
                //        ScannerStart();
                //        return;
                //    }
                //}

                //// vytvoreni noveho zaznamu ... vyhledat a dotaz??
                //Fask.MST_W.LokaceService.Location dsLokace = new Fask.MST_W.LokaceService.Location();
                //Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow lokaceRow = dsLokace.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();
                //lokaceRow.SKL_ID = sklad != null ? sklad.skl_id : string.Empty;
                //lokaceRow.LOCNCODE = kod;
                //lokaceRow.IS_RECEIVE = 0;
                //lokaceRow.DEX_ROW_ID = 1;
                //lokaceRow.SetTYPENull();

                //PrijmovaLokace = lokaceRow;
                //finalize();
                //DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Prijem.PrijemVyberPrijmoveLokaceList, Najit lokaci");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }
    }
}