// TODO : parsovani hodnot z pameti tagu ...

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Fask.MST_W.Forms;
using System.IO;
using Fask.ScannerProvider;
using Fask.Graphic;
using Fask.MST_W.RFID;

namespace Fask.MST_W.Vydej_3.RFID
{
    public partial class ZapisRFID : System.Windows.Forms.Form
    {

        #region Pomocne veci-dialog pro naplneni polozky, vybrana polozka
        //private string davkafilename = string.Empty;

        /// <summary>
        /// Background worker pro zapis tagu do cipu ...
        /// </summary>
        OpenNETCF.ComponentModel.BackgroundWorker bwZapisTagu = null;

        /// <summary>
        /// Nasnimane kody RFID scannerem.
        /// </summary>
        private System.Collections.Generic.List<Fask.MST_W.Scanner.RFIDTagData> nasnimaneKody = new List<Fask.MST_W.Scanner.RFIDTagData>();

        /// <summary>
        /// Vrati vybrarou polozku ze seznamu pokud neni tak null
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow A_VybranaPolozka
        {
            get
            {
                try
                {
                    return (bsCZMSTSIRFID.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Vyledna nasnimana data.
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vydej A_DS_Zapsat
        {
            get
            {
                return dsVydej;
            }
            set
            {
                // TODO : jak se to poziva ...
                dsVydej = value;
                bsCZMSTSIRFID.DataSource = dsVydej;
                bsCZMSTSIRFID.DataMember = dsVydej.CZMST_SI_RFID.TableName;
            }
        }

        /// <summary>
        /// Vysledne zapsane tagy ...
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vydej A_DS_Zapsane
        {
            get
            {
                return dsVydejZapsano;
            }
            //set
            //{
            //    // TODO : jak se to poziva ...
            //    dsVydej = value;
            //    bsCZMSTSIRFID.DataSource = dsVydej;
            //    bsCZMSTSIRFID.DataMember = dsVydej.CZMST_SI_RFID.TableName;
            //}
        }

        public string A_EAN
        {
            get { return this.ean; }
            set { this.ean = value; }
        }

        //tlacitka polozkyBindingSource.
        /// <summary>
        /// Pri stisknu tlatitek - nefunguje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnimatRFID_KeyDown(object sender, KeyEventArgs e)
        {
            //pokud stiskl excape
            if (e.KeyCode == Keys.Escape)
            {
                PerformKonec();
            }//enter
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Back)
            {
                SmazRadekZTabulky(this.A_VybranaPolozka);
            }
            else if (e.KeyCode == Keys.D1)
            {
                menuItemZobrazeniRezim_Click(null, null);
            }
            else if (e.KeyCode == Keys.D3)
            {
                miRFIDZapsatInformace_Click(null, null);
            }
            else
                return;
            //jak dojde sem tak se neco pouzilo z podminek krom posledni
            e.Handled = true;

        }

        #endregion

        #region inicializace
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ZapisRFID() //(string davkafilename)
        {
            Cursor.Current = Cursors.WaitCursor;

            //this.davkafilename = davkafilename;

            InitializeComponent();
            MyGridInitialize();

            bwZapisTagu = new OpenNETCF.ComponentModel.BackgroundWorker();
            bwZapisTagu.WorkerReportsProgress = true;
            bwZapisTagu.WorkerSupportsCancellation = true;
            bwZapisTagu.DoWork += new OpenNETCF.ComponentModel.DoWorkEventHandler(bwZapisTagu_DoWork);
            bwZapisTagu.RunWorkerCompleted += new OpenNETCF.ComponentModel.RunWorkerCompletedEventHandler(bwZapisTagu_RunWorkerCompleted);
            bwZapisTagu.ProgressChanged += new OpenNETCF.ComponentModel.ProgressChangedEventHandler(bwZapisTagu_ProgressChanged);
            bwZapisTagu.Disposed += new EventHandler(bwZapisTagu_Disposed);

            Cursor.Current = Cursors.Default;
        }


        private void MyGridInitialize()
        {
            this.dataGridZapsat.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGridZapsat.Font = new Font(this.dataGridZapsat.Font.Name, Settings.UIGridFont, this.dataGridZapsat.Font.Style);
            this.dataGridZapsat.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_" + dataGridZapsat.Name));


            this.dataGridZapsano.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGridZapsano.Font = new Font(this.dataGridZapsano.Font.Name, Settings.UIGridFont, this.dataGridZapsano.Font.Style);
            this.dataGridZapsano.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_" + dataGridZapsano.Name));
        }

        private void MyGridSave()
        {
            this.dataGridZapsat.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_" + dataGridZapsat.Name));
            this.dataGridZapsano.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString() + "_" + dataGridZapsano.Name));
        }


        /// <summary>
        /// Vola se pri load jen nastavi jmeno okna atd a veci pro data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnimatRFID_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            //Cursor.Current = Cursors.WaitCursor;
            //z list polozky
            //this.Text += " " + MST_Global.Inventura2Name.Trim();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            //menuItemInfo.Enabled = SelectedSI != null ? true : false;
            //this.ShowData(ShowDataType.Nalezene);

            this.dataGridZapsat.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGridZapsat.KeyScrollUp = MST_Global.DataGridScrollUp;
            this.dataGridZapsat.Focus();

            //Nastaveni default sortu podle casu nacteni od nejnovejsiho po nejstarsi...
            //pokud ovsem neni nastaven jiny uzivatelsky sort...
            //tedy pouze pri 1.inicializaci ...
            //if (this.dataGrid2Polozky.Sort == string.Empty)
            //{
            //    this.dataGrid2Polozky.Sort = "CASNACTENO desc";
            //}

            //Cursor.Current = Cursors.Default;


            #region nastaveni vychoziho zobrazeni panelu ...
            panelListZapsat.Dock = DockStyle.Fill;
            panelListZapsano.Dock = DockStyle.Fill;

            panelListZapsat.Show();
            panelListZapsano.Hide();

            dataGridZapsat.Focus();

            #endregion

        }
        #endregion



        #region Pridani dat do dialogu
        /// <summary>
        /// Odstrani radek z tabulky Id jsou stejna
        /// </summary>
        /// <param name="mrow">radek co se bude mazat</param>
        private void SmazRadekZTabulky(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow mrow)
        {
            // listPolozkyDS.Polozky.
            //.ID
            try
            {
                if (mrow == null)
                    return;
                //Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow rowForDelete = listPolozkyDS.Polozky.FindByID(mrow.ID);
                //listPolozkyDS.Polozky.RemovePolozkyRow(rowForDelete);
                dsVydej.CZMST_SI_RFID.RemoveCZMST_SI_RFIDRow(mrow);
                dsVydej.CZMST_SI_RFID.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
            finally
            {
                UpdateUI();
            }
        }

        #endregion


        #region Metody na ukonceni dialogu
        /// <summary>
        /// Stiskn tlacitka na konec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();//pokud chci otazku
        }

        /// <summary>
        /// Metoda pro ukonceni
        /// </summary>
        /// <param name="question"></param>
        private void PerformKonec()
        {
            //bool question = true;

            //// kontrola, zdali se pocty zaznamu nelisi, pokud ano, zobrazi se upozorneni
            //if (OmezitPocetNactenychZaznamu)
            //{
            //    if (dsObecne.RFID.Count > PocetZaznamu)
            //    {
            //        dr = MessageBoxBig.Show(string.Format("Data nebudou uložena, počet načtených kódů ({0}) je větší než počet požadovaných ({1}).\nPřesto ukončit?", dsObecne.RFID.Count, PocetZaznamu), "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
            //        if (dr == DialogResult.No)
            //            return;
            //        question = false;
            //    }
            //    else if (dsObecne.RFID.Count < PocetZaznamu)
            //    {
            //        dr = MessageBoxBig.Show(string.Format("Data nebudou uložena, počet načtených kódů ({0}) je menší než počet požadovaných ({1}).\nPřesto ukončit?", dsObecne.RFID.Count, PocetZaznamu), "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
            //        if (dr == DialogResult.No)
            //            return;
            //        question = false;
            //    }
            //}


            //TODO Priat ze ma naskenovane polozky!!
            if (MessageBoxBig.Show("Opravdu ukončit snímání RFID?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            // TODO : ulozit data do vystupu ...
            if (dsVydej.CZMST_SI_RFID.Count > 0)
            {
                if (DialogResult.No == 
                    MessageBoxBig.Show("Nejsou zapsány všechny záznamy!\nOpravdu ukončit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
                    )
                {
                    return;
                }
            }

            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            MyGridSave();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Smaze vybranou polozku z tabulky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemSmazat_Click(object sender, EventArgs e)
        {
            SmazRadekZTabulky(A_VybranaPolozka);
        }

        private void UpdateStatusBar()
        {
            string status = string.Empty;
            status = string.Format("{0}", dsVydej.CZMST_SI_RFID.Count);

            status += " P:" + (bsCZMSTSIRFID.Position + 1);

            this.statusBar1.Text = status;
        }

        private void UpdateRFIDUIState()
        {
        }


        private void SnimatRFID_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void SnimatRFID_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void SnimatRFID_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
        }

        private void miPotvrdit_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void SwitchZobrazeni()
        {
            if (panelListZapsat.Visible)
            {
                panelListZapsat.Hide();
                panelListZapsano.Show();
            }
            else
            {
                panelListZapsat.Show();
                panelListZapsano.Hide();
            }

            dataGridZapsat.Focus();
        }

        private void menuItemZobrazeniRezim_Click(object sender, EventArgs e)
        {
            SwitchZobrazeni();
        }

        private void dataGrid2Polozky_CurrentRowIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            UpdateRFIDUIState();
            UpdateStatusBar();
        }

        #region Zapis tagu do cipu ...
        
        string ean = string.Empty;
        string sarze = string.Empty;

        private void miRFIDZapsatInformace_Click(object sender, EventArgs e)
        {
            if (miRFIDZapsatInformace.Checked && bwZapisTagu.IsBusy)
            {
                bwZapisTagu.CancelAsync();
                miRFIDZapsatInformace.Checked = false;
            }
            else
            {

                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow vybranaPolozka = A_VybranaPolozka;
                if (vybranaPolozka == null)
                {
                    MessageBoxBig.Show("Není vybrána položka", "Zápis RFID");
                    return;
                }

                // zjistit info o polozce, zda je na sarze ... ???
                //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter tase = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                //tase.Connection.ConnectionString = "Data source=" + this.davkafilename;

                //SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable tblse = tase.GetDataByITEMNMBR(vybranaPolozka.ITEMNMBR.Trim());
                //SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable tblse = tase.GetDataBySOPNUMBEITEMNMBRORD(vybranaPolozka.DOCUMENTNMBR, vybranaPolozka.ITEMNMBR, vybranaPolozka.ORD);
                var tblse = Vydej.vydejInstance.globalObject.controller_vydej.GetDataBySOPNUMBEITEMNMBRORD_SE(vybranaPolozka.DOCUMENTNMBR, vybranaPolozka.ITEMNMBR, vybranaPolozka.ORD);
                if (tblse.Count == 0)
                {
                    MessageBoxBig.Show("Nebyla nalezena položka v předloze!", "Zápis RFID");
                    return;
                }

                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow SERow = tblse[0];

                string eanvalue = ean; //aby se nastaveny ean nezmenil pri cancelu inputboxu ...                 
                if (DialogResult.Cancel == InputBox.Show("EAN", eanvalue, out eanvalue, true))
                    return;
                ean = eanvalue.Trim();

                var eanlist = tblse.Where( x => (x.VNDITNUM.Trim().Equals(ean)) || (x.CZ_CarKod.Trim().Equals(ean)));
                if (eanlist.Count() == 0)
                {
                    if (DialogResult.No == MessageBoxBig.Show("EAN nenalezen v předloze položky.\nPokračovat a zapsat EAN:" + ean + "?", "Zápis RFID", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning))
                        return;
                }

                if (SERow.CZ_SerNum_Track > 0)
                {
                    // todo : vyber dialog sarze ... 
                    using (SejmiKodInfoFormSN sejmiFormSN = new SejmiKodInfoFormSN(string.Empty, SejmiKodFormDropdown.TypeOfCode.AlphaNumeric, null))
                    {
                        sejmiFormSN.SetDefaultValues();
                        sejmiFormSN.Popis = SERow.CZ_SerNum_Track == 2 ? Fask.Localization.Localization.Vydej3ListPolozek3SejmiSarze : (string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SNName));// "Sejmi " + (VERow.CZ_SerNum_Track == 2 ? "Šarže" : MST_Global.SNName);
                        sejmiFormSN.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
                        sejmiFormSN.Len = 0;
                        sejmiFormSN.CheckLen = false;
                        sejmiFormSN.AllowEmpty = false;
                        sejmiFormSN.veRow = SERow;
                        sejmiFormSN.viRow = null; // neni zname ... ??? => pripadne na SI_RFID ...
                        sejmiFormSN.Kod = string.Empty;
                        sejmiFormSN.vynulujVybraneZbozi();
                        //SejmiKodInfoForm3 sejmiSNForm = new SejmiKodInfoForm3(
                        //    "Sejmi " + MST_Global.SNName,
                        //    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
                        //    VERow);

                        //sejmiFormSN.SESN = sesnta.GetDataByCountEntriesITEMNMBR(VERow.CountEntries, VERow.ITEMNMBR.Trim());
                        //SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter sesnta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
                        //sesnta.Connection.ConnectionString = tase.Connection.ConnectionString;

                        //sejmiFormSN.SESN = sesnta.GetDataByKeyNacteno(SERow.CountEntries, SERow.SOPNUMBE, SERow.ITEMNMBR, SERow.ORD);
                        sejmiFormSN.SESN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByKeyNacteno_SE_SN(SERow.CountEntries, SERow.SOPNUMBE, SERow.ITEMNMBR, SERow.ORD);

                        if (DialogResult.Cancel == sejmiFormSN.ShowDialog())
                            return;
                        sarze = sejmiFormSN.Kod;
                        //if (DialogResult.Cancel == InputBox.Show("Šarže/SN", sarze, out sarze, true))
                        //    return;
                    }
                }
                else
                {
                    sarze = string.Empty;
                }

                bwZapisTagu.RunWorkerAsync();
                miRFIDZapsatInformace.Checked = true;
            }
        }

        private void ZapisTagu()
        {
            try
            {
                Scanner.ScannerRFIDMC319Z_v2 rfidScanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidScanner == null)
                {
                    MessageBoxBig.Show("Neni Motorola RFID scanner1!");
                    return;
                }

                int exceptioncount = 0;
                int numberItemAll = bsCZMSTSIRFID.Count;
                Cursor.Current = Cursors.WaitCursor;
                while (true)
                {
                    try
                    {

                        if (this.bwZapisTagu.CancellationPending)
                        {
                            return;
                        }

                        Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDRow vybranaPolozka = A_VybranaPolozka;

                        // Uz neni co zapisovat ...
                        if (vybranaPolozka == null)
                        {
                            return;
                        }

                        vybranaPolozka.SERLNMBR = sarze;

                        if (vybranaPolozka.IsO_M_TIDNull() || String.IsNullOrEmpty(vybranaPolozka.O_M_TID))
                        {
                            string memory = string.Empty;
                            if (!rfidScanner.TIDRead(vybranaPolozka.M_ID, out memory))
                                throw new Exception("TID Memory nenactena !!!");

                            vybranaPolozka.M_TID = memory;
                            vybranaPolozka.O_M_TID = memory;
                        }

                        if (vybranaPolozka.IsO_M_EPCNull() || String.IsNullOrEmpty(vybranaPolozka.O_M_EPC))
                        {
                            string memory = string.Empty;
                            try
                            {
                                if (!rfidScanner.EPCRead(vybranaPolozka.M_ID, out memory))
                                    throw new Exception("EPC Memory nenactena !!!");

                                vybranaPolozka.M_EPC = memory;
                                vybranaPolozka.O_M_EPC = memory;
                            }
                            catch (Exception ex)
                            { // nemusi byt nacteno ... 
                                Logging.Log.Write(ex);
                            }
                        }

                        if (vybranaPolozka.IsO_M_USERNull() || String.IsNullOrEmpty(vybranaPolozka.O_M_USER))
                        {
                            string memory = string.Empty;
                            try
                            {
                                if (!rfidScanner.USERRead(vybranaPolozka.M_ID, out memory))
                                    throw new Exception("USER Memory nenactena !!!");

                                vybranaPolozka.M_USER = memory;
                                vybranaPolozka.O_M_USER = memory;
                            }
                            catch (Exception ex)
                            { // nemusi byt nacteno ... 
                                Logging.Log.Write(ex);
                            }
                        }

                        if (vybranaPolozka.IsO_M_RESERVEDNull() || String.IsNullOrEmpty(vybranaPolozka.O_M_RESERVED))
                        {
                            string memory = string.Empty;
                            try
                            {
                                if (!rfidScanner.RESERVEDRead(vybranaPolozka.M_ID, out memory))
                                    throw new Exception("RESERVED Memory nenactena !!!");
                             
                                vybranaPolozka.M_RESERVED = memory;
                                vybranaPolozka.O_M_RESERVED = memory;
                            }
                            catch (Exception ex)
                            { // nemusi byt nacteno ... 
                                Logging.Log.Write(ex);
                            }
                        }

                        #region Priprava EPC obsahu
                        MST_W.RFID.EPC_SGTIN96 sgtin96 = new MST_W.RFID.EPC_SGTIN96();
                        sgtin96.EPCCodeHeaderValue = MST_W.RFID.EPC_SGTIN96.EPCCodeHeaderValueSGTIN96;
                        sgtin96.FilterValue = (byte)MST_W.RFID.EPC_SGTIN96.filterValues.PointOfSale;
                        sgtin96.PC_Length = 96; //96bitu pro EPC kod ...
                        sgtin96.PC_Toggle = false;
                        sgtin96.PC_AttributeBits = 0;
                        sgtin96.PC_AttributeBits_IsHazardousMaterial = false;
                        sgtin96.PartitionObject = MST_W.RFID.EPC_SGTIN96.SGTIN_Partition.Default;

                        sgtin96.EAN = ean;
                        sgtin96.Serial = vybranaPolozka.SEQUENCENMBR;
                        #endregion

                        vybranaPolozka.M_EPC = sgtin96.MemoryHex;

                        #region Priprava User obsahu
                        MST_W.RFID.USER_512b user512b = new USER_512b();
                        user512b.ItemNumber = vybranaPolozka.ITEMNMBR.Trim();
                        user512b.SerltNumber = vybranaPolozka.SERLNMBR.Trim();                        
                        // TODO : doplnit nazev polozky ...
                        // prozatim tam je itemnumber... ()
                        string itemdesc2write = vybranaPolozka.ITEMDESC.Trim();
                        if (itemdesc2write.Length > 39)
                        {
                            itemdesc2write = itemdesc2write.Substring(0, 39);
                        }
                        user512b.ItemDesc = itemdesc2write;
                        #endregion

                        vybranaPolozka.M_USER = user512b.MemoryHex;

                        string newtagid = string.Empty;
                        try
                        {
                            if (!rfidScanner.USERWrite(vybranaPolozka.M_ID, vybranaPolozka.M_USER))
                            {
                                throw new Exception("Error: user data ID:" + vybranaPolozka.M_ID);
                            }

                            if (!rfidScanner.EPCWrite(vybranaPolozka.M_ID, vybranaPolozka.M_EPC, string.Empty, out newtagid))
                            {

                            }
                            else
                            {
                                vybranaPolozka.M_ID = newtagid;
                                vybranaPolozka.AcceptChanges();
                                this.Invoke((System.Threading.ThreadStart)delegate()
                                {
                                    vybranaPolozka.SetAdded();
                                    dsVydejZapsano.CZMST_SI_RFID.ImportRow(vybranaPolozka);
                                    dsVydej.CZMST_SI_RFID.RemoveCZMST_SI_RFIDRow(vybranaPolozka);
                                });

                            }

                            this.bwZapisTagu.ReportProgress(
                                100 - (int)Math.Floor(((bsCZMSTSIRFID.Count / (float)numberItemAll) * 100))
                                );
                        }
                        catch (Exception ex)
                        {
                            exceptioncount++;
                            Logging.Log.Write(ex);
                            this.Invoke((System.Threading.ThreadStart)delegate()
                            {
                                statusBar1.Text = "E(" + exceptioncount + "):" + ex.Message;
                            });
                        }

                        System.Threading.Thread.Sleep(100);


                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        this.Invoke((System.Threading.ThreadStart)delegate()
                        {
                            statusBar1.Text = "E(" + exceptioncount + "):" + ex.Message;
                        });
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            finally
            {
                miRFIDZapsatInformace.Checked = false;
            }
        }

        void bwZapisTagu_Disposed(object sender, EventArgs e)
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                statusBar1.Text = "bw disposed...";
            });            
        }

        void bwZapisTagu_ProgressChanged(object sender, OpenNETCF.ComponentModel.ProgressChangedEventArgs e)
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                statusBar1.Text = "Zapsano " + e.ProgressPercentage.ToString() + "%";
            });
        }

        void bwZapisTagu_RunWorkerCompleted(object sender, OpenNETCF.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // konec info ... 
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                MessageBoxBig.Show("Konec zapisu...");
            });
        }

        void bwZapisTagu_DoWork(object sender, OpenNETCF.ComponentModel.DoWorkEventArgs e)
        {
            ZapisTagu();
        }

        #endregion

        private void miKonecAUloz_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }
    }
}