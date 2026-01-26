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
    public partial class ExpediceBaleniPolozkyList : Form
    {
        private SejmiKodForm skf = null;
        private ExpediceBaleniPridatPolozku naplnpMnozstvi = null;
        private ExpediceBaleniPridatPolozku naplnpSerialNumber = null;
        private Fask.MST_W.ExpediceService.SSCC nmbrpal = null; // cislo palety
        private string serltnum = string.Empty;     // sarze, se kterou se pracuje

        // nactena data z online metody
        Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        private ExpediceService.ExpediceBaleni asddsapolozky = new Fask.MST_W.ExpediceService.ExpediceBaleni();
        private _WebRefernces_Globals.ExpediceSeviceSession wsExpedice = null;

        public ExpediceBaleniPolozkyList(Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow Hlavicka, Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad)
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
        private Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow _Hlavicka = null;


        private Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow _Polozka
        {
            get
            {
                try
                {
                    return ((DataRowView)(dataGrid1.BindingContext[bsPolozky].Current)).Row as Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow;
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

                bsPolozky.DataSource = dsExpediceBaleni.CZMST_Expedice_Baleni_Polozky;
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
            ts.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.TableName;

            DataGrid2TextBoxColumn dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Položka č.";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.ITEMNMBRColumn.ColumnName;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Název";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.ITEMDESCColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 150;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Množství";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.QTYColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "SSCC palety";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.NMBRPALColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "SN";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.SERLTNUMColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Č.k.";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.VNDITNUMColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Č.k. vlastní";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.CZ_CarKodColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Lokace";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.LOCNCODEColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dgtbc = new DataGrid2TextBoxColumn();
            dgtbc.HeaderText = "Sklad ID";
            dgtbc.MappingName = asddsapolozky.CZMST_Expedice_Baleni_Polozky.SKL_IDColumn.ColumnName; ;
            dgtbc.NullText = "-";
            dgtbc.Width = 50;
            ts.GridColumnStyles.Add(dgtbc);

            dataGrid1.TableStyles.Add(ts);
        }

        //private void InitializeGridDynamicColumns(ExpediceService.ExpediceHlavicky hlavicky)
        //{
        //    foreach (DataColumn dcol in hlavicky.CZMST_Expedice_Hlavicka.Columns)
        //    {
        //        if (!this.dataGridTableStyle1.GridColumnStyles.Contains(dcol.ColumnName))
        //        {
        //            DataGrid2TextBoxColumn du = new DataGrid2TextBoxColumn();
        //            du.MappingName = dcol.ColumnName;
        //            du.HeaderText = dcol.ColumnName;
        //            du.NullText = "-";
        //            du.Width = 45;
        //            du.Grid = this.dataGrid1;
        //            this.dataGridTableStyle1.GridColumnStyles.Add(du);
        //        }
        //    }
        //}

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
            else if (e.KeyCode == Keys.F1)
            {
                miZobrazitStavSkladu_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                miAktualizovat_Click(null, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                miNajitBarcode_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                miNajitItemnmbr_Click(null, null);
            }
            else if (e.KeyCode == Keys.F7)
            {
                miPaletaZmenit_Click(null, null);
            }
            else if (e.KeyCode == Keys.D4)
            {
                if (Expedice.Globals.PovolitTiskPalet)
                {
                    PerformTiskPaletaServer();
                    //PerformTiskPaleta();
                }
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformDokoncitDavku()
        {
            try
            {
                ScannerStop();

                if (MessageBoxBig.Show("Opravdu chcete odeslat data dávky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                if (Expedice.Globals.PovolitTiskPalet)
                {
                    PerformTiskPaletaServer();
                    //PerformTiskPaleta();
                }

                Cursor.Current = Cursors.WaitCursor;
                wsExpedice.Baleni_Process(
                    MST_Global.TerminalID,
                    MST_Global.UserID,
                    sklad != null ? sklad.skl_id : string.Empty,
                    _Hlavicka.ID, asddsapolozky,
                    Fask.MST_W.ExpediceService.ProcessState.Zpracovat
                    );

                Cursor.Current = Cursors.Default;
                MessageBoxBig.Show(string.Format("Data dávky odeslána"), Fask.Localization.Localization.Prijem4PrijemMainOdesilaniDat, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                // ukonceni scanneru
                ScannerFinalize();
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

            DialogResult = DialogResult.Cancel;
        }

        private void finalize()
        {

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

                //TODO : 13.11.2018 TaD Konfiguračne asi na klavesnici tlačitkem + v konfiguraci
                
                if (false)
                {
                    this.BeginInvoke(new DelegateString(najdipolozku), new object[] { kod });
                }
                else
                {
                    this.BeginInvoke(new DelegateString(najdibaleni), new object[] { kod });
                }

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
            najdipolozku(kod, string.Empty);
        }

        /// <summary>
        /// Najití Baleni podle čárového kódu NMBRBAL
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdibaleni(string kod)
        {
            najdibaleni(kod, string.Empty);
        }

        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdibaleni(string kod, string itemnmbr)
        {
            Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka = null;

            try
            {
                ScannerStop();

                string ck = kod.Trim();

                //if (string.IsNullOrEmpty(nmbrpal))
                if (nmbrpal == null)
                    throw new Exception("Není vygenerováno číslo palety");

                // parsovani vahoveho kodu
                Fask.Parsing.Codes.BaseCode code = null;
                //if (Expedice.Globals.PovolitParsovaniCK)
                //{
                //    code = Parsing.ParsingFactory.ParseWeightCode(ck);
                //    if (code is WeightCode)
                //        ck = ((WeightCode)code).id;
                //}

                Cursor.Current = Cursors.WaitCursor;
                Fask.MST_W.ExpediceService.ExpediceBaleni tables = wsExpedice.Baleni_Polozka_Get(MST_Global.TerminalID, MST_Global.UserID, Expedice.Globals.SkladID, ck, itemnmbr, serltnum);
                Cursor.Current = Cursors.Default;

                if (tables.Expedice_Baleni_Polozka.Count == 0)
                {
                    //MessageBoxBig.Show(string.Format("Položka s čárovým kódem '{0}' nebyla nalezena", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    MessageBoxBig.Show(string.Format("Položka nebyla nalezena online", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Expedice_Baleni_Polozka.Count > 1)
                {
                    //MessageBoxBigTimeout.Show(string.Format("Nalezeno více položek online.\nVyberte ze seznamu.", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    //using (Expedice.ExpediceBaleniPolozkyVarianty form = new ExpediceBaleniPolozkyVarianty(ck, itemnmbr, serltnum, tables, sklad, ExpediceBaleniPolozkyVarianty.Zobrazeni.VARIANTY))
                    //{
                    //    DialogResult dr = form.ShowDialog();

                    //    if (dr != DialogResult.OK)
                    //        return;

                    //    polozka = form._Polozka;
                    //}
                    Program.mstw.mbw.BeginPracujiForm("Aktualizace položek v balení");

                    foreach (Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow item in tables.Expedice_Baleni_Polozka)
                    {
                        PerformPridatPolozkuzBaleni(item, code);
                    }



                    Program.mstw.mbw.EndPracujiForm();




                }
                else
                {
                    polozka = tables.Expedice_Baleni_Polozka[0];
                    PerformPridatPolozkuzBaleni(polozka, code);
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



        /// <summary>
        /// Najití položky podle čárového kódu
        /// </summary>
        /// <param name="kod">barcode</param>
        private void najdipolozku(string kod, string itemnmbr)
        {
            Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka = null;

            try
            {
                ScannerStop();

                string ck = kod.Trim();

                //if (string.IsNullOrEmpty(nmbrpal))
                if (nmbrpal == null)
                    throw new Exception("Není vygenerováno číslo palety");

                // parsovani vahoveho kodu
                Fask.Parsing.Codes.BaseCode code = null;
                if (Expedice.Globals.PovolitParsovaniCK)
                {
                    code = Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);
                    if (code is Parsing.Codes.Interfaces.ICodeItemnmbr)
                        ck = ((Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr;
                }

                Cursor.Current = Cursors.WaitCursor;
                Fask.MST_W.ExpediceService.ExpediceBaleni tables = wsExpedice.Baleni_Polozka_Get(MST_Global.TerminalID, MST_Global.UserID, Expedice.Globals.SkladID, ck, itemnmbr, serltnum);
                Cursor.Current = Cursors.Default;

                if (tables.Expedice_Baleni_Polozka.Count == 0)
                {
                    //MessageBoxBig.Show(string.Format("Položka s čárovým kódem '{0}' nebyla nalezena", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    MessageBoxBig.Show(string.Format("Položka nebyla nalezena online", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
                else if (tables.Expedice_Baleni_Polozka.Count > 1)
                {
                    MessageBoxBigTimeout.Show(string.Format("Nalezeno více položek online.\nVyberte ze seznamu.", ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    using (Expedice.ExpediceBaleniPolozkyVarianty form = new ExpediceBaleniPolozkyVarianty(ck, itemnmbr, serltnum, tables, sklad, ExpediceBaleniPolozkyVarianty.Zobrazeni.VARIANTY))
                    {
                        DialogResult dr = form.ShowDialog();

                        if (dr != DialogResult.OK)
                            return;

                        polozka = form._Polozka;
                    }

                    PerformPridatPolozku(polozka, code);
                }
                else
                {
                    polozka = tables.Expedice_Baleni_Polozka[0];
                    PerformPridatPolozku(polozka, code);
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

        private void PerformPridatPolozku(Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka, BaseCode code)
        {
            if (polozka == null)
            {
                MessageBoxBig.Show("Není vybrána položka", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (nmbrpal == null)
            {
                MessageBoxBig.Show("Není vybrána paleta", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            try
            {
                ScannerStop();

                Fask.MST_W.ExpediceService.ExpediceBaleni polozkyData = new Fask.MST_W.ExpediceService.ExpediceBaleni();
                decimal qty = 0;
                string sn = string.Empty;

                qty = polozka.QTY;
                sn = polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM;
                Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow rowPolozka = polozkyData.CZMST_Expedice_Baleni_Polozky.NewCZMST_Expedice_Baleni_PolozkyRow();

                if (polozka.CZ_SerNum_Track == 0) //sledovano na mnozstvi
                {
                    if (!Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                    {
                        WeightCode wc = (WeightCode)code;

                        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        qty =
                            (wc.weight
                            / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                            / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                            );
                    }
                    else
                    {

                        // zadani mnozstvi
                        if (naplnpMnozstvi == null) naplnpMnozstvi = new ExpediceBaleniPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", polozka, true); // TODO: konfiguracne povoleni zadani mnozstvi scannerem

                        bool baleni;

                        if (polozka.IsQTYPACKNull())
                            baleni = false;
                        else
                        {
                            baleni = polozka.QTYPACK > 0;
                        }

                        naplnpMnozstvi.Polozka = polozka;
                        naplnpMnozstvi.Serltnum = sn;
                        naplnpMnozstvi.Text = baleni ? Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
                        naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
                        if (code is WeightCode)
                        {
                            WeightCode wc = (WeightCode)code;
                            naplnpMnozstvi.Kod =
                                (wc.weight
                                / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                                / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                                ).ToString(Settings.UIFormatDesCisel);
                        }
                        else if (Expedice.Globals.PovolitPredvyplneniMnozstvi)
                        {
                            naplnpMnozstvi.Kod = polozka.QTY.ToString(Settings.UIFormatDesCisel);
                        }
                        else
                            naplnpMnozstvi.Kod = "";

                        naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                        if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
                            return;

                        qty = decimal.Parse(naplnpMnozstvi.Kod);
                    }
                }
                else if ((polozka.CZ_SerNum_Track == 1) || (polozka.CZ_SerNum_Track == 2)) //sledovano na seriova cisla
                {
                    if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou && !string.IsNullOrEmpty(serltnum))
                    {
                        sn = serltnum;
                    }
                    else
                    {
                        // zadani sn
                        if (naplnpSerialNumber == null) naplnpSerialNumber = new ExpediceBaleniPridatPolozku("Šarže"/*Fask.Localization.Localization.Prodej3ProdejListSerioveCislo*/, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", polozka);
                        naplnpSerialNumber.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                        naplnpSerialNumber.Polozka = polozka;
                        naplnpSerialNumber.Text = "Zadání šarže";//Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo;
                        //naplnpSerialNumber.MaxLength = 50; // TOTO JE ZLE.... prebirat z Fask.Columns??
                        naplnpSerialNumber.AllowEmpty = true;   // TODO: zmenit??
                        //naplnpSerialNumber.Kod = "";
                        naplnpSerialNumber.Kod = polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM;
                        if (naplnpSerialNumber.ShowDialog() == DialogResult.Cancel)
                            return;

                        sn = naplnpSerialNumber.Kod;
                    }
                    //sledovano na sarze
                    if (polozka.CZ_SerNum_Track == 2)
                    {
                        if (!Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                        {
                            WeightCode wc = (WeightCode)code;

                            //qty = decimal.Parse(naplnpMnozstvi.Kod);
                            qty =
                                (wc.weight
                                / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                                / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                                );
                        }
                        else
                        {
                            // zadani mnozstvi
                            if (naplnpMnozstvi == null) naplnpMnozstvi = new ExpediceBaleniPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", polozka, true); // TODO: konfiguracne povoleni zadani mnozstvi scannerem
                            bool baleni = polozka.QTYPACK > 0;
                            naplnpMnozstvi.Polozka = polozka;
                            naplnpMnozstvi.Serltnum = sn;
                            naplnpMnozstvi.Text = baleni ? Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
                            naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
                            if (code is WeightCode)
                            {
                                WeightCode wc = (WeightCode)code;
                                naplnpMnozstvi.Kod =
                                    (wc.weight
                                    / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                                    / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                                    ).ToString(Settings.UIFormatDesCisel);
                            }
                            else if (Expedice.Globals.PovolitPredvyplneniMnozstvi)
                            {
                                naplnpMnozstvi.Kod = polozka.QTY.ToString(Settings.UIFormatDesCisel);
                            }
                            else
                                naplnpMnozstvi.Kod = "";

                            naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                            if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
                                return;

                            qty = decimal.Parse(naplnpMnozstvi.Kod);
                        }
                    }
                }
                else
                    throw new Exception(string.Format("Neznámý typ sledování položky: {0}", polozka.CZ_SerNum_Track.ToString()));

                rowPolozka.ID = Guid.NewGuid();
                rowPolozka.IDH = _Hlavicka.ID;
                rowPolozka.ITEMNMBR = polozka.ITEMNMBR;
                rowPolozka.ITEMDESC = polozka.IsITEMDESCNull() ? string.Empty : polozka.ITEMDESC;
                rowPolozka.VNDITNUM = polozka.IsVNDITNUMNull() ? string.Empty : polozka.VNDITNUM;
                rowPolozka.CZ_CarKod = polozka.IsCZ_CarKodNull() ? string.Empty : polozka.CZ_CarKod;
                rowPolozka.LOCNCODE = string.Empty;     // TODO: poresit
                rowPolozka.SKL_ID = polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID;       // TODO: poresit
                rowPolozka.QTY = qty;
                rowPolozka.QTYPACK = polozka.IsQTYPACKNull() ? 0 : polozka.QTYPACK; // Dotahnout i qtypack??
                rowPolozka.QTYMJ = qty * (rowPolozka.QTYPACK > 0 ? rowPolozka.QTYPACK : 1);
                rowPolozka.MJ = polozka.IsMJNull() ? string.Empty : polozka.MJ;
                rowPolozka.SERLTNUM = sn;
                rowPolozka.WEIGHT = polozka.IsWEIGHTNull() ? 0 : polozka.WEIGHT;
                rowPolozka.NMBRPAL = nmbrpal.Code;
                rowPolozka.TYPEPAL = "";
                rowPolozka.SKL_ID_SRC = polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID;         // odkud beru (zdrojova lokace v lokacnim mechanismu
                rowPolozka.LOCNCODE_SRC = polozka.IsLOCNCODENull() ? string.Empty : polozka.LOCNCODE;   // odkud beru (zdrojova lokace v lokacnim mechanismu
                rowPolozka.PRINTED = 0;

                polozkyData.CZMST_Expedice_Baleni_Polozky.AddCZMST_Expedice_Baleni_PolozkyRow(rowPolozka);
                polozkyData.CZMST_Expedice_Baleni_Polozky.AcceptChanges();

                bool state = true;
                while (state)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int res = wsExpedice.Baleni_Polozka_Add(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID, polozkyData);

                        asddsapolozky.CZMST_Expedice_Baleni_Polozky.ImportRow(rowPolozka);
                        asddsapolozky.CZMST_Expedice_Baleni_Polozky.AcceptChanges();

                        // ulozeni SN pro nasledne vyuzivani, pokud to je povoleno, sleduje se na sarze a jeste sarze neni vyplnena 
                        if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou && polozka.CZ_SerNum_Track > 0 && string.IsNullOrEmpty(serltnum))
                        {
                            serltnum = sn;
                        }

                        Cursor.Current = Cursors.Default;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformPridatPolozku");
                        if (MessageBoxBig.Show(string.Format("Položku se nepodařilo přidat online.\n{0}\nOpakovat?", ex.Message), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                            != DialogResult.Yes)
                            return;
                    }
                }
                int pos = bsPolozky.Find(asddsapolozky.CZMST_Expedice_Baleni_Polozky.IDColumn.ColumnName, rowPolozka.ID);

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


        private void PerformPridatPolozkuzBaleni(Fask.MST_W.ExpediceService.ExpediceBaleni.Expedice_Baleni_PolozkaRow polozka, BaseCode code)
        {
            if (polozka == null)
            {
                MessageBoxBig.Show("Není vybrána položka", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (nmbrpal == null)
            {
                MessageBoxBig.Show("Není vybrána paleta", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            try
            {
                ScannerStop();

                Fask.MST_W.ExpediceService.ExpediceBaleni polozkyData = new Fask.MST_W.ExpediceService.ExpediceBaleni();
                decimal qty = 0;
                string sn = string.Empty;

                qty = polozka.QTY;
                sn = polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM;
                Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow rowPolozka = polozkyData.CZMST_Expedice_Baleni_Polozky.NewCZMST_Expedice_Baleni_PolozkyRow();

                //if (polozka.CZ_SerNum_Track == 0) //sledovano na mnozstvi
                //{
                //    if (!Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                //    {
                //        WeightCode wc = (WeightCode)code;

                //        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                //        qty =
                //            (wc.weight
                //            / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                //            / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                //            );
                //    }
                //    else
                //    {

                //        // zadani mnozstvi
                //        if (naplnpMnozstvi == null) naplnpMnozstvi = new ExpediceBaleniPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", polozka, true); // TODO: konfiguracne povoleni zadani mnozstvi scannerem

                //        bool baleni;

                //        if (polozka.IsQTYPACKNull())
                //            baleni = false;
                //        else
                //        {
                //            baleni = polozka.QTYPACK > 0;
                //        }

                //        naplnpMnozstvi.Polozka = polozka;
                //        naplnpMnozstvi.Serltnum = sn;
                //        naplnpMnozstvi.Text = baleni ? Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
                //        naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
                //        if (code is WeightCode)
                //        {
                //            WeightCode wc = (WeightCode)code;
                //            naplnpMnozstvi.Kod =
                //                (wc.weight
                //                / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                //                / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                //                ).ToString(Settings.UIFormatDesCisel);
                //        }
                //        else if (Expedice.Globals.PovolitPredvyplneniMnozstvi)
                //        {
                //            naplnpMnozstvi.Kod = polozka.QTY.ToString(Settings.UIFormatDesCisel);
                //        }
                //        else
                //            naplnpMnozstvi.Kod = "";

                //        naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                //        if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
                //            return;

                //        qty = decimal.Parse(naplnpMnozstvi.Kod);
                //    }
                //}
                //else if ((polozka.CZ_SerNum_Track == 1) || (polozka.CZ_SerNum_Track == 2)) //sledovano na seriova cisla
                //{
                //    if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou && !string.IsNullOrEmpty(serltnum))
                //    {
                //        sn = serltnum;
                //    }
                //    else
                //    {
                //        // zadani sn
                //        if (naplnpSerialNumber == null) naplnpSerialNumber = new ExpediceBaleniPridatPolozku("Šarže"/*Fask.Localization.Localization.Prodej3ProdejListSerioveCislo*/, SejmiKodForm.TypeOfCode.AlphaNumeric, 0, true, false, "", polozka);
                //        naplnpSerialNumber.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                //        naplnpSerialNumber.Polozka = polozka;
                //        naplnpSerialNumber.Text = "Zadání šarže";//Fask.Localization.Localization.Prodej3ProdejListVlozteSerioveCislo;
                //        naplnpSerialNumber.MaxLength = 21;
                //        naplnpSerialNumber.AllowEmpty = true;   // TODO: zmenit??
                //        //naplnpSerialNumber.Kod = "";
                //        naplnpSerialNumber.Kod = polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM;
                //        if (naplnpSerialNumber.ShowDialog() == DialogResult.Cancel)
                //            return;

                //        sn = naplnpSerialNumber.Kod;
                //    }
                //    //sledovano na sarze
                //    if (polozka.CZ_SerNum_Track == 2)
                //    {
                //        if (!Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                //        {
                //            WeightCode wc = (WeightCode)code;

                //            //qty = decimal.Parse(naplnpMnozstvi.Kod);
                //            qty =
                //                (wc.weight
                //                / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                //                / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                //                );
                //        }
                //        else
                //        {
                //            // zadani mnozstvi
                //            if (naplnpMnozstvi == null) naplnpMnozstvi = new ExpediceBaleniPridatPolozku(Fask.Localization.Localization.Prodej3ProdejListMnozstvi, SejmiKodForm.TypeOfCode.Numeric, 0, false, false, "", polozka, true); // TODO: konfiguracne povoleni zadani mnozstvi scannerem
                //            bool baleni = polozka.QTYPACK > 0;
                //            naplnpMnozstvi.Polozka = polozka;
                //            naplnpMnozstvi.Serltnum = sn;
                //            naplnpMnozstvi.Text = baleni ? Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListVlozteMnozstvi;
                //            naplnpMnozstvi.Popis = baleni ? Fask.Localization.Localization.Prodej3ProdejListMnozstviBaleni : Fask.Localization.Localization.Prodej3ProdejListMnozstvi;
                //            if (code is WeightCode)
                //            {
                //                WeightCode wc = (WeightCode)code;
                //                naplnpMnozstvi.Kod =
                //                    (wc.weight
                //                    / (polozka.IsWEIGHTNull() || (polozka.WEIGHT == 0) ? 1 : polozka.WEIGHT)
                //                    / (polozka.IsQTYPACKNull() || (polozka.QTYPACK == 0) ? 1 : polozka.QTYPACK)
                //                    ).ToString(Settings.UIFormatDesCisel);
                //            }
                //            else if (Expedice.Globals.PovolitPredvyplneniMnozstvi)
                //            {
                //                naplnpMnozstvi.Kod = polozka.QTY.ToString(Settings.UIFormatDesCisel);
                //            }
                //            else
                //                naplnpMnozstvi.Kod = "";

                //            naplnpMnozstvi.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                //            if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
                //                return;

                //            qty = decimal.Parse(naplnpMnozstvi.Kod);
                //        }
                //    }
                //}
                //else
                //    throw new Exception(string.Format("Neznámý typ sledování položky: {0}", polozka.CZ_SerNum_Track.ToString()));

                rowPolozka.ID = Guid.NewGuid();
                rowPolozka.IDH = _Hlavicka.ID;
                rowPolozka.ITEMNMBR = polozka.ITEMNMBR;
                rowPolozka.ITEMDESC = polozka.IsITEMDESCNull() ? string.Empty : polozka.ITEMDESC;
                rowPolozka.VNDITNUM = polozka.IsVNDITNUMNull() ? string.Empty : polozka.VNDITNUM;
                rowPolozka.CZ_CarKod = polozka.IsCZ_CarKodNull() ? string.Empty : polozka.CZ_CarKod;
                rowPolozka.LOCNCODE = string.Empty;     // TODO: poresit
                rowPolozka.SKL_ID = polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID;       // TODO: poresit
                rowPolozka.QTY = qty;
                rowPolozka.QTYPACK = polozka.IsQTYPACKNull() ? 0 : polozka.QTYPACK; // Dotahnout i qtypack??
                rowPolozka.QTYMJ = qty * (rowPolozka.IsQTYPACKNull() ? 0 : (rowPolozka.QTYPACK > 0 ? rowPolozka.QTYPACK : 1));
                rowPolozka.MJ = polozka.IsMJNull() ? string.Empty : polozka.MJ;
                rowPolozka.SERLTNUM = sn;
                rowPolozka.WEIGHT = polozka.IsWEIGHTNull() ? 0 : polozka.WEIGHT;
                rowPolozka.NMBRPAL = nmbrpal.Code;
                rowPolozka.TYPEPAL = "";
                rowPolozka.SKL_ID_SRC = polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID;         // odkud beru (zdrojova lokace v lokacnim mechanismu
                rowPolozka.LOCNCODE_SRC = polozka.IsLOCNCODENull() ? string.Empty : polozka.LOCNCODE;   // odkud beru (zdrojova lokace v lokacnim mechanismu
                rowPolozka.PRINTED = 0;
                rowPolozka.NMBRBAL = polozka.NMBRBAL;
                rowPolozka.IDPol = polozka.IDPol; // TaD IDPol muze byt null ale tady problem ze GUID z webreference neni nullable....

                polozkyData.CZMST_Expedice_Baleni_Polozky.AddCZMST_Expedice_Baleni_PolozkyRow(rowPolozka);
                polozkyData.CZMST_Expedice_Baleni_Polozky.AcceptChanges();

                bool state = true;
                while (state)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int res = wsExpedice.Baleni_Polozka_Add(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID, polozkyData);

                        asddsapolozky.CZMST_Expedice_Baleni_Polozky.ImportRow(rowPolozka);
                        asddsapolozky.CZMST_Expedice_Baleni_Polozky.AcceptChanges();

                        // ulozeni SN pro nasledne vyuzivani, pokud to je povoleno, sleduje se na sarze a jeste sarze neni vyplnena 
                        //if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou && polozka.CZ_SerNum_Track > 0 && string.IsNullOrEmpty(serltnum))
                        //{
                        //    serltnum = sn;
                        //}

                        Cursor.Current = Cursors.Default;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformPridatPolozku");
                        if (MessageBoxBig.Show(string.Format("Položku se nepodařilo přidat online.\n{0}\nOpakovat?", ex.Message), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical)
                            != DialogResult.Yes)
                            return;
                    }
                }
                int pos = bsPolozky.Find(asddsapolozky.CZMST_Expedice_Baleni_Polozky.IDColumn.ColumnName, rowPolozka.ID);

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

        private Fask.MST_W.ExpediceService.ExpediceBaleni OnlineGetHlavicky(string skl_id)
        {
            try
            {
                return wsExpedice.Baleni_GetPolozky(MST_Global.TerminalID, MST_Global.UserID, skl_id, _Hlavicka.ID);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, OnlineGetHlavicky");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
        }

        private void miAktualizovat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUpdate();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, Aktualizovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformUpdate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                asddsapolozky = OnlineGetHlavicky(sklad != null ? sklad.skl_id : string.Empty);
                //if (hlavicky != null)
                //{
                //    bsHlavicky = new BindingSource();
                //    bsHlavicky.DataSource = hlavicky.CZMST_Expedice_Hlavicka;
                //    dataGrid1.DataSource = bsHlavicky;
                //}
                if (asddsapolozky == null)
                    asddsapolozky = new Fask.MST_W.ExpediceService.ExpediceBaleni();

                bsPolozky = new BindingSource();
                bsPolozky.DataSource = asddsapolozky.CZMST_Expedice_Baleni_Polozky;
                dataGrid1.DataSource = bsPolozky;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show("Načtení expedičních příkazů se nezdařilo.\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
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

        private void miNajitBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Čárový kód", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte čár. kód položky";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                        return;

                    kod = skf.Kod;
                }

                najdipolozku(kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, miNajitLokaci_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void miNajitItemnmbr_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Číslo položky", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte číslo položky";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                        return;

                    kod = skf.Kod;
                }

                najdipolozku(string.Empty, kod);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, miNajitLokaci_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void ExpediceVyberHlavickyList_Shown(object sender, EventArgs e)
        {
            try
            {
                timerLoad.Enabled = false;
                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

                miTisk.Enabled = miTiskPaleta.Enabled = Expedice.Globals.PovolitTiskPalet;

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

                try
                {
                    // vygenerovani cisla palety
                    PerformGenerovatCisloPalety();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, ExpediceVyberHlavickyList_Shown");
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                }

                ScannerStart();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, ExpediceVyberHlavickyList_Shown");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
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

                if (MessageBoxBig.Show(string.Format("Opravdu chcete odstranit vybranou položku '{0}'?", (_Polozka.IsITEMDESCNull() ? string.Empty : _Polozka.ITEMDESC)), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;


                if (!string.IsNullOrEmpty(_Polozka.NMBRBAL))
                {
                    if (MessageBoxBig.Show(string.Format("Chcete odstranit vše z balení: '{0}'?", _Polozka.NMBRBAL), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                        return;



                    MessageBoxBig.Show(string.Format("Implementovat mazani..."), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);



                }
                else
                {

                    int res = wsExpedice.Baleni_Polozka_Del(MST_Global.TerminalID, MST_Global.UserID, sklad == null ? string.Empty : sklad.skl_id, _Hlavicka.ID, _Polozka.ID);

                    asddsapolozky.CZMST_Expedice_Baleni_Polozky.RemoveCZMST_Expedice_Baleni_PolozkyRow(_Polozka);
                }
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
            try
            {
                PerformZobrazitStavSkladu();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, miZobrazitStavSkladu_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        /// <summary>
        /// Zobrazeni aktualniho stavu skladu vsech polozek v lokacnim mechanismu.
        /// </summary>
        private void PerformZobrazitStavSkladu()
        {
            try
            {
                ScannerStop();

                Cursor.Current = Cursors.WaitCursor;
                Fask.MST_W.ExpediceService.ExpediceBaleni tables = wsExpedice.Baleni_Polozka_Get(MST_Global.TerminalID, MST_Global.UserID, Expedice.Globals.SkladID, string.Empty, string.Empty, string.Empty);//serltnum);
                Cursor.Current = Cursors.Default;

                using (Expedice.ExpediceBaleniPolozkyVarianty form = new ExpediceBaleniPolozkyVarianty(string.Empty, string.Empty, string.Empty, tables, sklad, ExpediceBaleniPolozkyVarianty.Zobrazeni.STAV_SKLADU))
                {
                    form.Text = "Stav skladu";

                    DialogResult dr = form.ShowDialog();

                    if (dr != DialogResult.OK)
                        return;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformDeletePolozka");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void miPaletaGenerovat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Expedice.Globals.PovolitTiskPalet)
                {
                    PerformTiskPaletaServer();
                    //PerformTiskPaleta();
                }

                PerformGenerovatCisloPalety();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, miPaletaGenerovat_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        /// <summary>
        /// Online generovani cisla palety
        /// </summary>
        private void PerformGenerovatCisloPalety()
        {
            try
            {
                ScannerStop();


                Cursor.Current = Cursors.WaitCursor;
                nmbrpal = wsExpedice.SSCC_Generovat(MST_Global.TerminalID, MST_Global.UserID, sklad != null ? sklad.skl_id : string.Empty);

                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformGenerovatCisloPalety");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateStatusBar()
        {
            try
            {
                sbInfo.Text = string.Empty;
                sbInfo.Text = "N:" + asddsapolozky.CZMST_Expedice_Baleni_Polozky.Count;


				// Počitani počtu nasnimanich baliku, ale bylo nutno upravit DataTable v References, aby bylo možno použit LINQ
				//když se provede nejaky update tak se to pregeneruje a zmizne....

				//************* Tohle je potřeba dat na Tabulku *********
				//public partial class CZMST_Expedice_Baleni_PolozkyDataTable : global::System.Data.TypedTableBase<CZMST_Expedice_Baleni_PolozkyRow>, global::System.Collections.IEnumerable
				//******************************************************
				var cnt = asddsapolozky.CZMST_Expedice_Baleni_Polozky.GroupBy(x => x.NMBRBAL);
				sbInfo.Text += ", B:" + (cnt != null ? cnt.Count().ToString() : "-");


                sbInfo.Text += ", P:" + (nmbrpal != null ? nmbrpal.Number : "-");

                if (Expedice.Globals.ZobrazitZadaniSarzePouzeJednou)
                    sbInfo.Text += ", SN:" + (string.IsNullOrEmpty(serltnum) ? "-" : serltnum);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, UpdateStatusBar");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void miPaletaZmenit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Zmena cisla davky
        /// </summary>
        private void PerformZmenitCisloPalety()
        {
            try
            {
                ScannerStop();
                // zadani cisla palety
                string kod = string.Empty;
                using (SejmiKodForm skf = new SejmiKodForm("Číslo palety", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
                {
                    skf.Text = "Zadejte číslo palety";
                    DialogResult dr = skf.ShowDialog();

                    if (dr != DialogResult.OK)
                        return;

                    kod = skf.Kod;
                }

                Cursor.Current = Cursors.WaitCursor;
                //nmbrpal = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, sklad != null ? sklad.skl_id : string.Empty, _Hlavicka.ID, pal);
                Fask.MST_W.ExpediceService.SSCC sscc = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, sklad != null ? sklad.skl_id : string.Empty, _Hlavicka.ID, kod);

                if (sscc == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxBig.Show(string.Format("Zadané číslo palety '{0}' nebylo nalezeno", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                }
                else
                    nmbrpal = sscc;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformGenerovatCisloPalety");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                UpdateStatusBar();
                ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        private void miPaletaZmenit_Click_1(object sender, EventArgs e)
        {
            try
            {
                PerformZmenitCisloPalety();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.BaleniPolozkyList, miPaletaZmenit_Click_1");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void miTiskPaleta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Expedice.Globals.PovolitTiskPalet)
                    return;

                //PerformTiskPaleta();
                PerformTiskPaletaServer();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message, "Expedice.BaleniPolozkyList, miTiskPaleta_Click");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void PerformTiskPaletaServer()
        {
            try
            {
                if (true)
                {
                    if (MessageBoxBig.Show("Tisknout paletovy listek?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                        return;
                }

                string pocetVytiskuStr = "1";
                int pocetVytisku = 1;
                do
                {
                    DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    if (dr == DialogResult.Cancel)
                        return;

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    if (pocetVytisku <= 0)
                    {
                        MessageBoxBig.Show("Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        MessageBoxBig.Show("Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);



                //PrinterFactory.PrinterFactory pf = PrinterFactory.PrinterFactory.Instance;
                //var templatesDict = pf.Templates;                

                Fask.MST_W.ExpediceService.TiskSablona tiskSablona = new Fask.MST_W.ExpediceService.TiskSablona();



                Fask.PrinterFactory.ModuleToPrint m2pH = Fask.PrinterFactory.PrinterFactory.Instance.Templates[Fask.PrinterFactory.PrinterModules.ExpedicePaletaHlavicka];
                Fask.PrinterFactory.ModuleToPrint m2pR = Fask.PrinterFactory.PrinterFactory.Instance.Templates[Fask.PrinterFactory.PrinterModules.ExpedicePaletaRadek];
                Fask.PrinterFactory.ModuleToPrint m2pF = Fask.PrinterFactory.PrinterFactory.Instance.Templates[Fask.PrinterFactory.PrinterModules.ExpedicePaletaPaticka];


                Fask.PrinterFactory.Printer p = Fask.PrinterFactory.PrinterFactory.Instance.Printers[m2pH.PrinterType];

                tiskSablona.printerName = p.PrinterProvider.GetPrinterName();
                tiskSablona.sablonaHlavicka = m2pH.Template;
                tiskSablona.sablonaRadek = m2pR.Template;
                tiskSablona.sablonaPaticka = m2pF.Template;

                
                
                //tiskSablona.printerName = pf.Printers[templatesDict[Fask.PrinterFactory.PrinterModules.ExpedicePaletaHlavicka].PrinterType].;
                //tiskSablona.sablonaHlavicka = templatesDict[Fask.PrinterFactory.PrinterModules.ExpedicePaletaHlavicka].Template;
                //tiskSablona.sablonaRadek = templatesDict[Fask.PrinterFactory.PrinterModules.ExpedicePaletaRadek].Template;
                //tiskSablona.sablonaPaticka = templatesDict[Fask.PrinterFactory.PrinterModules.ExpedicePaletaPaticka].Template;
                tiskSablona.pocetVytisku = pocetVytisku; // pocet vytisku doplnit ...

                // TODO : pocet vytisku ...

                wsExpedice.Expedice_Baleni_Tisk(_Polozka.IDH, _Polozka.NMBRPAL, tiskSablona);
            }
            catch (Exception exTisk)
            {
                Logging.Log.Write(exTisk);
                //throw;
            }
        }

        private void PerformTiskPaleta()
        {
            try
            {
                bool vytisteno = false;

                ScannerStop();
                if (_Polozka == null)
                {
                    MessageBoxBig.Show("Není vybrán nasnímaný záznam", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                //konfiguracne potlacit hlasku?
                if (true)
                {
                    if (MessageBoxBig.Show("Tisknout paletovy listek?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                        return;
                }


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

                dataHlavicka.Add("NMBRPAL", _Polozka.IsNMBRPALNull() ? string.Empty : _Polozka.NMBRPAL.Trim());

                //dataHlavicka.Add("NMBRPAL", _Polozka.IsNMBRPALNull() ? string.Empty : _Polozka.NMBRBAL.Trim());

                DataRow[] drPolozky = asddsapolozky.CZMST_Expedice_Baleni_Polozky.Select(this.asddsapolozky.CZMST_Expedice_Baleni_Polozky.NMBRPALColumn.ColumnName + "='" + (_Polozka.IsNMBRPALNull() ? "" : _Polozka.NMBRPAL) + "'");

                var drPolozkyVse = asddsapolozky.CZMST_Expedice_Baleni_Polozky.Select();
                var drPolozkyPaleta = drPolozkyVse.Where(x => x[this.asddsapolozky.CZMST_Expedice_Baleni_Polozky.NMBRPALColumn.ColumnName] == (_Polozka.IsNMBRPALNull() ? string.Empty : _Polozka.NMBRPAL));

                //var drPolozkyPaleta.GroupBy(x => new {x["ITEMNMBR"], x);

                #region TaD upravy pro I-Tec vytahovani dat

                //if (true)
                //{
                List<string> SOPNUMBESeznam = new List<string>();
                SOPNUMBESeznam.Clear();

                //decimal qtyALL = 0;

                foreach (DataRow row in drPolozky)
                {
                    object sop = row["SOPNUMBE"];

                    if ((sop != null) && (sop is string))
                    {
                        string stringSOP = (string)sop;

                        if (!string.IsNullOrEmpty(stringSOP))
                        {
                            if (!SOPNUMBESeznam.Contains(stringSOP.Trim()))
                            {
                                SOPNUMBESeznam.Add(stringSOP.Trim());
                            }
                        }
                    }

                    //object qty = row["QTYSHPPD"];

                    //if ((qty != null) && (qty is decimal))
                    //{
                    //    decimal Decimalqty = (decimal)qty;
                    //    qtyALL += Decimalqty;
                    //}


                }

                dataHlavicka.Add("OBJ", String.Join(", ", SOPNUMBESeznam.ToArray()));
                //dataHlavicka.Add("POCET", qtyALL.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));


                //}

                #endregion

                //else
                //{

                // naplneni dat polozek podle cisla palety
                foreach (DataRow row in drPolozky)
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
                //}

                // 30.6.2016 PeV: na zadost JaS automaticke predvyplneni mnozstvi tisku 1, TODO: konfiguracne ...
                // naplneni dat paticky
                //vytisteno = ExpediceTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, null);
                vytisteno = ExpediceTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, 1);
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
    }
}