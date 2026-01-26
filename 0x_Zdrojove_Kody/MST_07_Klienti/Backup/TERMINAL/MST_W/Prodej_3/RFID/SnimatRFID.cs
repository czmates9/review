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

namespace Fask.MST_W.Prodej_3.RFID
{
    public partial class SnimatRFID : System.Windows.Forms.Form
    {

        #region Pomocne veci-dialog pro naplneni polozky, vybrana polozka
        private int _rssiMinimum = -30; //???
        public int A_RSSIMinimum
        {
            get { return _rssiMinimum; }
            set { _rssiMinimum = value; }
        }

        /// <summary>
        /// Nasnimane kody RFID scannerem.
        /// </summary>
        private System.Collections.Generic.List<Fask.MST_W.Scanner.RFIDTagData> nasnimaneKody = new List<Fask.MST_W.Scanner.RFIDTagData>();
        /// <summary>
        /// Povoli zadat pouze zadany pocet nactenych tagu. Pokud jich je vic nebo min, dojde k zobrazeni upozorneni a nepusti to dal ...
        /// </summary>
        public bool A_OmezitPocetNactenychZaznamu = false;

        private int _pocetZaznamu;
        /// <summary>
        /// Pocet kodu, ktere se maji nacist.
        /// </summary>
        public int A_PocetZaznamu
        {
            get
            {
                return _pocetZaznamu;
            }
            set
            {
                _pocetZaznamu = value;
            }
        }

        /// <summary>
        /// Vrati vybrarou polozku ze seznamu pokud neni tak null
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Obecne.RFIDRow A_VybranaPolozka
        {
            get
            {
                try
                {
                    return (bsRFID.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Obecne.RFIDRow;
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
        public Fask.SQLiteDBs.DataSets.Obecne A_DS_Nasnimane
        {
            get
            {
                return dsObecne;
            }
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
            else if (e.KeyCode == Keys.C)// if (e.KeyCode == Keys.F1)  // F1 nefunguje ...
            {
                buttonRFIDONOFF_Click(null, null);
            }
            else if (e.KeyCode == Keys.D1)
            {
                menuItemZobrazeniRezim_Click(null, null);
            }
            else
                return;
            //jak dojde sem tak se neco pouzilo z podminek krom posledni
            e.Handled = true;

        }

        #endregion

        private Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2 rfidScannerInstance = null;


        #region inicializace
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SnimatRFID()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            InitializeDataGridView();
            MyGridInitialize();
            Cursor.Current = Cursors.Default;
        }

        private void MyGridInitialize()
        {
            this.dataGrid2Polozky.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid2Polozky.Font = new Font(this.dataGrid2Polozky.Font.Name, Settings.UIGridFont, this.dataGrid2Polozky.Font.Style);
            this.dataGrid2Polozky.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void MyGridSave()
        {
            this.dataGrid2Polozky.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void InitializeDataGridView()
        {
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dsObecne.RFID.TableName;       // _katalogZbozi.CZMST095.TableName;

            DataGrid2TextBoxColumn dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "ID";
            dg.MappingName = dsObecne.RFID.IDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 150;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "Tag ID";
            dg.MappingName = dsObecne.RFID.TIDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 150;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "EPC";
            dg.MappingName = dsObecne.RFID.EPCColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 150;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "USER";
            dg.MappingName = dsObecne.RFID.USERColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 150;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "RESERVED";
            dg.MappingName = dsObecne.RFID.RESERVEDColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 150;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "TID Len";
            dg.MappingName = dsObecne.RFID.TIDLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "EPC Len";
            dg.MappingName = dsObecne.RFID.EPCLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "USER Len";
            dg.MappingName = dsObecne.RFID.USERLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "RESERVED Len";
            dg.MappingName = dsObecne.RFID.RESERVEDLenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "Seen";
            dg.MappingName = dsObecne.RFID.SeenColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dg = new DataGrid2TextBoxColumn();
            dg.HeaderText = "RSSI";
            dg.MappingName = dsObecne.RFID.RSSIColumn.ColumnName;   // _katalogZbozi.CZMST095.ITEMDESCColumn.ColumnName;
            dg.NullText = "-";
            dg.Width = 50;
            ts.GridColumnStyles.Add(dg);

            dataGrid2Polozky.TableStyles.Add(ts);
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

            this.dataGrid2Polozky.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid2Polozky.KeyScrollUp = MST_Global.DataGridScrollUp;
            this.dataGrid2Polozky.Focus();

            #region nastaveni vychoziho zobrazeni panelu ...
            panelList.Dock = DockStyle.Fill;
            panelDetail.Dock = DockStyle.Fill;

            panelList.Show();
            panelDetail.Hide();

            dataGrid2Polozky.Focus();

            #endregion

            if ((Program.mstw.RFIDUHFScanner != null) && (Program.mstw.RFIDUHFScanner is Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2))
            {
                this.rfidScannerInstance = Program.mstw.RFIDUHFScanner as Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2;
                this.rfidScannerInstance.RFIDScannerStarted -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStartedHandler(rfidScannerInstance_RFIDScannerStarted);
                this.rfidScannerInstance.RFIDScannerStopped -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStoppedHandler(rfidScannerInstance_RFIDScannerStopped);
                this.rfidScannerInstance.RFIDScannerStarted += new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStartedHandler(rfidScannerInstance_RFIDScannerStarted);
                this.rfidScannerInstance.RFIDScannerStopped += new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStoppedHandler(rfidScannerInstance_RFIDScannerStopped);
                this.rfidScannerInstance.TriggerEnabled = true;
            }

            try
            {
                if (Program.mstw.RFIDUHFScanner != null)
            {
                Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                Program.mstw.RFIDUHFScanner.RFIDTagEvent += new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
            }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
        }

            this.RFIDScannerAllMemoriesSet();
            this.buttonRFIDONOFF_Click(null, null);
        }

        void rfidScannerInstance_RFIDScannerStarted()
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                this.rfidactive = true;
                UpdateUI();
            });
        }

        void rfidScannerInstance_RFIDScannerStopped()
        {
            this.BeginInvoke((System.Threading.ThreadStart)delegate()
            {
                this.rfidactive = false;
                UpdateUI();
            });
        }

        #endregion



        #region Pridani dat do dialogu
        //private int pocetnactenichTagu = 0;
        /// <summary>
        /// Zpracuje nasnimane poslozky z RFID scaneru.
        /// Postupne je bude projizdet a kontrolovat zda existuji.
        /// Pokud ne informuje o tom a bude je pridavat do datagridu. 
        /// </summary>
        public bool PridejNasnimanePolozky()
        {
            while (nasnimaneKody.Count > 0)
            {
                NajdiPolozkuCarovyKod(nasnimaneKody[0]);
                try { nasnimaneKody.RemoveAt(0); }
                catch { }
            }
            UpdateUI();

            return true;
        }

        /// <summary>
        /// Pokusi se nalest polozku pro dany kod
        /// </summary>
        private void NajdiPolozkuCarovyKod(Fask.MST_W.Scanner.RFIDTagData ck)
        {
            try
            {//najde pomoci car kodu a prida
                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow r = null;
                
                var result = dsObecne.RFID.Where( x => (x.ID.Equals(ck.TagID, StringComparison.CurrentCultureIgnoreCase)));

                if (result == null || result.Count() == 0)
                {
                    //Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chimes.wav");


                    //TaD 13.2.2018
                    // ID is DBNull 

                    OpenNETCF.Media.SystemSounds.Hand.Play();
                    r = dsObecne.RFID.NewRFIDRow();
                    r.ID  = String.IsNullOrEmpty(ck.TagID) ? "" : ck.TagID;

                    //if (ck.EPCMemory == null)
                    //    r.SetEPCNull();
                    //else
                    //    r.EPC = ck.EPCMemory;

                    //if (ck.TIDMemory == null)
                    //    r.SetTIDNull();
                    //else
                    //    r.TID = ck.TIDMemory;

                    //if (ck.ReservedMemory == null)
                    //    r.SetRESERVEDNull();
                    //else
                    //    r.RESERVED = ck.ReservedMemory;

                    //if (ck.UserMemory == null)
                    //    r.SetUSERNull();
                    //else
                    //    r.USER = ck.UserMemory;


                    //if (ck.RSSI == null)
                    //    r.SetRSSINull();
                    //else
                    //    r.RSSI = ck.RSSI;



                    dsObecne.RFID.AddRFIDRow(r);

                    

                    
                }
                else
                {
                    r = result.First();
                }

                if (!String.IsNullOrEmpty(ck.TagID))
                    r.ID = ck.TagID;
                if (!String.IsNullOrEmpty(ck.TIDMemory))
                    r.TID = ck.TIDMemory;
                if (!string.IsNullOrEmpty(ck.EPCMemory))
                {
                    #region test read EPC as SGTIN96
                    //MST_W.RFID.EPC_SGTIN96 sgtin96 = new EPC_SGTIN96(ck.EPCMemory);

                    //string text = String.Format(
                    //"sgtin96.CompanyPrefix;{0};" + Environment.NewLine +
                    //"sgtin96.CRC;{1};" + Environment.NewLine +
                    //"sgtin96.EAN;{2};" + Environment.NewLine +
                    //"sgtin96.EPCCode;{3};" + Environment.NewLine +
                    //"sgtin96.EPCCodeHeaderValue;{4};" + Environment.NewLine +
                    //"sgtin96.FilterValue;{5};" + Environment.NewLine +
                    //"sgtin96.GTIN;{6};" + Environment.NewLine +
                    //"sgtin96.CheckDigit;{7};" + Environment.NewLine +
                    //"sgtin96.Indicator;{8};" + Environment.NewLine +
                    //"sgtin96.IndicatorXItemRef;{9};" + Environment.NewLine +
                    //"sgtin96.ItemRef;{10};" + Environment.NewLine +
                    //"sgtin96.MemoryBin;{11};" + Environment.NewLine +
                    //"sgtin96.MemoryHex;{12};" + Environment.NewLine +
                    //"sgtin96.Partition;{13};" + Environment.NewLine +
                    //"sgtin96.PC;{14};" + Environment.NewLine +
                    //"sgtin96.PC_AFI;{15};" + Environment.NewLine +
                    //"sgtin96.PC_AttributeBits;{16};" + Environment.NewLine +
                    //"sgtin96.PC_AttributeBits_IsHazardousMaterial;{17};" + Environment.NewLine +
                    //"sgtin96.PC_Length;{18};" + Environment.NewLine +
                    //"sgtin96.PC_Toggle;{19};" + Environment.NewLine +
                    //"sgtin96.PC_UMIIndicator;{20};" + Environment.NewLine +
                    //"sgtin96.PC_XPCIndicator;{21};" + Environment.NewLine +
                    //"sgtin96.Serial;{22};" + Environment.NewLine +
                    //"sgtin96.XPC;{23}",
                    //sgtin96.CompanyPrefix,
                    //sgtin96.CRC,
                    //sgtin96.EAN,
                    //sgtin96.EPCCode,
                    //sgtin96.EPCCodeHeaderValue,
                    //sgtin96.FilterValue,
                    //sgtin96.GTIN,
                    //string.Empty,//sgtin96.CheckDigit, // Exception OutOfRange
                    //sgtin96.Indicator,
                    //sgtin96.IndicatorXItemRef,
                    //sgtin96.ItemRef,
                    //sgtin96.MemoryBin,
                    //sgtin96.MemoryHex,
                    //sgtin96.Partition,
                    //sgtin96.PC,
                    //sgtin96.PC_AFI,
                    //sgtin96.PC_AttributeBits,
                    //sgtin96.PC_AttributeBits_IsHazardousMaterial,
                    //sgtin96.PC_Length,
                    //sgtin96.PC_Toggle,
                    //sgtin96.PC_UMIIndicator,
                    //sgtin96.PC_XPCIndicator,
                    //sgtin96.Serial,
                    //sgtin96.XPC);
#endregion

                    r.EPC = ck.EPCMemory;
                }
                    
                if (!string.IsNullOrEmpty(ck.ReservedMemory))
                    r.RESERVED = ck.ReservedMemory;
                if (!string.IsNullOrEmpty(ck.UserMemory))
                    r.USER = ck.UserMemory;

                r.Seen += ck.CountReaded;
                r.RSSI = ck.RSSI;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        #endregion

        #region Zpracovani polozek a odstraneni z tabulky

        /// <summary>
        /// Odstrani radek z tabulky Id jsou stejna
        /// </summary>
        /// <param name="mrow">radek co se bude mazat</param>
        private void SmazRadekZTabulky(Fask.SQLiteDBs.DataSets.Obecne.RFIDRow mrow)
        {
            // listPolozkyDS.Polozky.
            //.ID
            try
            {
                if (mrow == null)
                    return;
                //Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow rowForDelete = listPolozkyDS.Polozky.FindByID(mrow.ID);
                //listPolozkyDS.Polozky.RemovePolozkyRow(rowForDelete);
                dsObecne.RFID.RemoveRFIDRow(mrow);
                dsObecne.RFID.AcceptChanges();
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

        #region Aktivace/deaktivace a data ze skeneru
        /// <summary>
        /// Zapnuti rfid ctecky a cteni dat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRFIDONOFF_Click(object sender, EventArgs e)
        {
            if (rfidactive)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.RFIDScannerStop();
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Logging.Log.Write(ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

                PridejNasnimanePolozky(); // pridani polozek do datagridu
            }
            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.RFIDScannerStart();
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Logging.Log.Write(ex);
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }

            menuItemAllMemories.Enabled = !rfidactive;

            UpdateUI();
        }

        private bool rfidactive = false;
        /// <summary>
        /// Aktivace RFID skeneru
        /// </summary>
        private void RFIDScannerStart()
        {
            if (Program.mstw.RFIDUHFScanner != null)
            {
                //Program.mstw.RFIDUHFScanner.DataReady += new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                //Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                //Program.mstw.RFIDUHFScanner.RFIDTagEvent += new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                //Program.mstw.RFIDUHFScanner.AllMemories = allmemories;
                Program.mstw.RFIDUHFScanner.StartScan();
                if (this.rfidScannerInstance == null)
                    rfidactive = true;
            }
        }
        /// <summary>
        /// Deaktivace RFID skeneru
        /// </summary>
        private void RFIDScannerStop()
        {
            if (Program.mstw.RFIDUHFScanner != null)
            {
                Program.mstw.RFIDUHFScanner.StopScan();
                //Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                if (this.rfidScannerInstance == null)
                    rfidactive = false;
            }
        }

        /// <summary>
        /// nastaveni parametru pro uhf scanner ... 
        /// </summary>
        private void RFIDScannerAllMemoriesSet()
        {
            if (Program.mstw.RFIDUHFScanner != null)
                Program.mstw.RFIDUHFScanner.AllMemories = menuItemAllMemories.Checked;
        }

        /// <summary>
        /// Provede test, zda je rfid scanner aktivni, pokud ano, tak se zepta na jeho ukonceni
        /// </summary>
        /// <returns>True scanner neni aktivni, False scanner je neaktivni</returns>
        private bool RFIDScannerActivationTestStopQuestion()
        {
            if (rfidactive)
            {
                if (DialogResult.Yes == MessageBoxBig.Show("RFID scanner je aktivní.\n\nUkončit snímání RFID?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                    buttonRFIDONOFF_Click(null, null);
            }
            return rfidactive;
        }

        //private delegate void MethodInvoker();
        private delegate void RFIDProcessDataDelegate(Fask.MST_W.Scanner.RFIDTagDataEventArgs e);
        void RFIDProcessData(Fask.MST_W.Scanner.RFIDTagDataEventArgs e)
        {
            try
            {              
                nasnimaneKody.AddRange(e.TagList);
                PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Pridani dat do hash tabulky kde je ulozen barcode jako klic a kolikrat byl nasniman
        /// </summary>
        /// <param name="sender">co to poslalo</param>
        /// <param name="e">obsahuje seznam polozek</param>
        void RFIDScanner_DataReady(object sender, Fask.MST_W.Scanner.RFIDTagDataEventArgs e)
        {
            RFIDProcessDataDelegate rfiddel = new RFIDProcessDataDelegate(RFIDProcessData);
            this.BeginInvoke(rfiddel, new object[] { e });
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
            if (A_OmezitPocetNactenychZaznamu)
            {
                bool rfidscanneractive = rfidactive;
                try
                {
                    if (rfidscanneractive)
                        RFIDScannerStop();

                    if (A_DS_Nasnimane.RFID.Count > A_PocetZaznamu)
                    {
                        MessageBoxBig.Show(string.Format("Data nebudou uložena, počet načtených kódů ({0}) je větší než počet požadovaných ({1}).", A_DS_Nasnimane.RFID.Count, A_PocetZaznamu), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }
                    else if (A_DS_Nasnimane.RFID.Count < A_PocetZaznamu)
                    {
                        MessageBoxBig.Show(string.Format("Data nebudou uložena, počet načtených kódů ({0}) je menší než počet požadovaných ({1}).", A_DS_Nasnimane.RFID.Count, A_PocetZaznamu), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }
                }
                finally
                {
                    if (rfidscanneractive)
                        RFIDScannerStart();
                }
            }

            if (A_DS_Nasnimane.RFID.Count == 0)
            {
                MessageBoxBig.Show("Nebyl načtený žádný kód!", "Chyba", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                return;
            }
            else
            {
                if (MessageBoxBig.Show("Potvrdit a ukončit snímání RFID?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                    return;
            }
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            RFIDScannerStop();
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
            if (A_OmezitPocetNactenychZaznamu)
                status = string.Format("Počet:{0}/{1}", dsObecne.RFID.Count, A_PocetZaznamu);
            else
                status = string.Format("Počet:{0}", dsObecne.RFID.Count);

            //status += " Pozice:" + (bsRFID.Position + 1);


            this.statusBar1.Text = status;
        }

        private void UpdateRFIDUIState()
        {
            if (rfidactive)
            {
                toolBarButtonRFID.ImageIndex = 0;
                toolBarButtonRFID.ToolTipText = "RFID active";
            }
            else
            {
                toolBarButtonRFID.ImageIndex = 1;
                toolBarButtonRFID.ToolTipText = "RFID notactive";

            }
            menuItemRFIDON.Checked = rfidactive;
            menuItemAllMemories.Enabled = !rfidactive;
        }


        /// <summary>
        /// Pracuje s tlacitky toolbaru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonRFID)
            {
                buttonRFIDONOFF_Click(null, e);
            }
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

            try
            {
      

                if (this.rfidScannerInstance != null)
                {
                    this.rfidScannerInstance.RFIDScannerStarted -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStartedHandler(rfidScannerInstance_RFIDScannerStarted);
                    this.rfidScannerInstance.RFIDScannerStopped -= new Fask.MST_W.Scanner.ScannerRFIDMC319Z_v2.RFIDScannerStoppedHandler(rfidScannerInstance_RFIDScannerStopped);
                    this.rfidScannerInstance.TriggerEnabled = false;
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
            Program.mstw.RFIDUHFScanner.RFIDTagEvent -= new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
                //Program.mstw.RFIDUHFScanner.RFIDTagEvent += new Fask.MST_W.Scanner.RFIDTagHandler(RFIDScanner_DataReady);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }


        private void miPotvrdit_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void SwitchZobrazeni()
        {
            if (panelList.Visible)
            {
                panelList.Hide();
                panelDetail.Show();
            }
            else
            {
                panelList.Show();
                panelDetail.Hide();
            }

            dataGrid2Polozky.Focus();
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
                dfEAN.Data = "-";
                dfTID.Data = "-";
                dfEPC.Data = "-";
                dfUSER.Data = "-";
                dfTIDLen.Data = "-";
                dfEPCLen.Data = "-";
                dfUSERLen.Data = "-";
                dfRESERVEDLen.Data = "-";
                dfCountSeen.Data = "-";
                dfRSSI.Data = "-";

                textBoxTID.Text = string.Empty;
                textBoxEPC.Text = string.Empty;
                textBoxUSER.Text = string.Empty;

                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow r = ((System.Data.DataRowView)(bsRFID.Current)).Row as Fask.SQLiteDBs.DataSets.Obecne.RFIDRow;
                if (r != null)
                {
                    dfEAN.Data = r.IsIDNull() ? null : r.ID.Trim();

                    dfTID.Data = r.IsTIDNull() ? null : r.TID;
                    dfTIDLen.Data = r.IsTIDLenNull() ? 0.ToString() : r.TIDLen;
                    if (!r.IsTIDNull())
                    {
                        MST_W.RFID.TID tid = MST_W.RFID.TID.Parse(r.TID);
                        textBoxTID.Text += "TID: " + r.TID;
                        textBoxTID.Text += "\r\nURI: " + tid.STID_URI;
                        textBoxTID.Text += "\r\nCID: " + tid.CI;
                        textBoxTID.Text += "\r\nMDID: " + tid.MDID.ToString("X");
                        textBoxTID.Text += "\r\nMDID Model: " + tid.MDID_Model;
                        textBoxTID.Text += "\r\nTMN: " + tid.MN;
                        if (tid.XTIDHeaderSegmentPresent)
                        {
                            textBoxTID.Text += "\r\nXTID Header: " + tid.XTIDHeaderSegment;
                            if (tid.SerialNumberSegmentPresent > 0)
                            {
                                textBoxTID.Text += "\r\nSN Length: " + tid.SerialNumberSegmentPresent;
                                textBoxTID.Text += "\r\nSerialNumber: " + tid.SerialNumberSegment;
                            }
                            if (tid.OptionalCommandSupportSegmentPresent)
                                textBoxTID.Text += "\r\nOpt.Comm.Supp.: " + tid.OptionalCommandSupportSegment;
                            if (tid.BlockWriteAndBlockEraseSegmentPresent)
                                textBoxTID.Text += "\r\nBW&BE:" + tid.BlockWriteAndBlockEraseSegment;
                            if (tid.UserMemoryAndBlockPermaLockSegmentPresent)
                                textBoxTID.Text += "\r\nUsr.Mem.&BPermaLock: " + tid.UserMemoryAndBlockPermaLockSegment;
                        }
                        else
                        {
                            textBoxTID.Text += "No XTID";
                        }
                    }

                    dfEPC.Data = r.IsEPCNull() ? null : r.EPC;
                    dfEPCLen.Data = r.IsEPCLenNull() ? 0.ToString() : r.EPCLen;
                    if (!r.IsEPCNull())
                    {
                        //RFID.EPSGlobal_sgtin96 epcsgtin96 = new Fask.MST_W.RFID.EPSGlobal_sgtin96();
                        //epcsgtin96.EPCDataStringHex = r.EPC;
                        //textBox1.Text += "EPC Memory:";
                        //textBox1.Text += "\r\n Company : " + epcsgtin96.CompanyPrefix;
                        //textBox1.
                        MST_W.RFID.EPC epc = MST_W.RFID.EPC.Parse(r.EPC);
                        textBoxEPC.Text += "EPC: " + epc.MemoryHex;
                        textBoxEPC.Text += "\r\nCRC: " + epc.CRC.ToString("X").PadLeft(4, '0');
                        textBoxEPC.Text += "\r\nPC: " + Convert.ToString(epc.PC, 2).PadLeft(16, '0');
                        textBoxEPC.Text += "\r\nLen: " + epc.PC_Length * 2 * 8;
                        textBoxEPC.Text += "\r\nToggle: " + epc.PC_Toggle;
                        textBoxEPC.Text += "\r\nUMI: " + epc.PC_UMIIndicator;
                        textBoxEPC.Text += "\r\nXPCInd: " + epc.PC_XPCIndicator;
                        textBoxEPC.Text += "\r\nXPC: " + (epc.PC_XPCIndicator ? Convert.ToString(epc.XPC, 2) : string.Empty);
                        textBoxEPC.Text += "\r\nAFI: " + Convert.ToString(epc.PC_AFI, 2);
                        textBoxEPC.Text += "\r\nATBits: " + Convert.ToString(epc.PC_AttributeBits, 2);
                    }

                }
                try
                {
                    dfUSER.Data = r.IsUSERNull() ? null : r.USER;
                    dfUSERLen.Data = r.IsUSERLenNull() ? 0.ToString() : r.USERLen;
                    
                    }
                    catch(Exception ex)
                    {
                        dfUSER.Data = "-";
                        dfUSERLen.Data = "-";
                    }

                    if (!r.IsUSERLenNull())
                    {
                        try
                        {
                            //RFID.USER user = RFID.USER.Parse(r.USER);
                            MST_W.RFID.USER_512b usr512 = new MST_W.RFID.USER_512b(r.USER);
                            textBoxUSER.Text = "Item Number: " + usr512.ItemNumber.Trim();
                            textBoxUSER.Text += "\r\nItem Desc: " + usr512.ItemDesc.Trim();
                            textBoxUSER.Text += "\r\nSerial :" + usr512.SerltNumber.Trim();
                        }
                        catch (Exception exUser)
                        {
                            textBoxUSER.Text = "User memory parse error:\n" + exUser.Message;
                        }
                    }

                    try
                    {
                        dfRFIDReserved.Data = r.IsRESERVEDNull() ? null : r.RESERVED;
                        dfRESERVEDLen.Data = r.IsRESERVEDLenNull() ? 0.ToString() : r.RESERVEDLen;

                    }
                    catch(Exception ex)
                    {
                        dfRFIDReserved.Data = "-";
                        dfRESERVEDLen.Data = "-";
                    }

                    dfCountSeen.Data = r.Seen.ToString();
                    dfRSSI.Data = r.RSSI.ToString();
                }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            UpdateRFIDUIState();
            UpdateStatusBar();
        }

        private void menuItemAllMemories_Click(object sender, EventArgs e)
        {
            menuItemAllMemories.Checked = !menuItemAllMemories.Checked;
            RFIDScannerAllMemoriesSet();
        }

        private void dataGrid2Polozky_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

    }
}