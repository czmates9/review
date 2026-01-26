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

namespace Fask.MST_W.Forms
{
    public partial class SnimatRFID : System.Windows.Forms.Form
    {

        #region Pomocne veci-dialog pro naplneni polozky, vybrana polozka
        /// <summary>
        /// Nasnimane kody RFID scannerem.
        /// </summary>
        private System.Collections.Generic.List<Fask.MST_W.Scanner.RFIDTagData> nasnimaneKody = new List<Fask.MST_W.Scanner.RFIDTagData>();
        /// <summary>
        /// Povolit/zakazat kontrolu duplicity s listem 'ExistujiciSarze'
        /// </summary>
        public bool A_KontrolaDuplicitySarzeVDavce = false;
        /// <summary>
        /// Povoli zadat pouze zadany pocet nactenych tagu. Pokud jich je vic nebo min, dojde k zobrazeni upozorneni a nepusti to dal ...
        /// </summary>
        public bool A_OmezitPocetNactenychZaznamu = false;
        /// <summary>
        /// List jiz nactenych sarzi (kontrola, zdali jiz nejsou v DB)
        /// </summary>
        public List<string> A_ExistujiciSarze = new List<string>();

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
                //PerformKonec();
            }//enter
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
                //NelezniPolozkuANapln(this.VybranaPolozka); // najde ji a vyplni
            }
            else if (e.KeyCode == Keys.Back)
            {
                SmazRadekZTabulky(this.A_VybranaPolozka);
            }
            else if (e.KeyCode == Keys.C)// if (e.KeyCode == Keys.F1)  // F1 nefunguje ...
            {
                buttonRFIDONOFF_Click(null, null);
            }
            //else if (e.KeyCode == Keys.F2)
            //{
            //    menuItemZpracujVse_Click(null, null);
            //}
            else if (e.KeyCode == Keys.D1)
            {
                menuItemZobrazeniRezim_Click(null, null);
            }
            // TID Memory
            else if (e.KeyCode == Keys.A)
            {
                menuItemRfidTIDRead_Click(null, null);
            }
            // EPC Memory
            else if (e.KeyCode == Keys.D)
            {
                menuItemRfidEpcWrite_Click(null, null);
            }
            else if (e.KeyCode == Keys.E)
            {
                menuItemRFIDEpcRead_Click(null, null);
            }
            // User Memory
            else if (e.KeyCode == Keys.I)
            {
                menuItemRFIDUserWrite_Click(null, null);
            }
            else if (e.KeyCode == Keys.J)
            {
                menuItemRfidUserRead_Click(null, null);
            }
            else if (e.KeyCode == Keys.N)
            {
                menuItemRFIDReservedWrite_Click(null, null);
            }
            else if (e.KeyCode == Keys.O)
            {
                menuItemRFIDReservedRead_Click(null, null);
            }
            else if (e.KeyCode == Keys.S)
            {
                menuItem10_Click(null, null);
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
            MyInitializeGrid();
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeGrid()
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
            this.ScannerStart();

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
                var result = dsObecne.RFID.Where(
                    x => 
                        (x.ID.Equals(ck.TagID, StringComparison.CurrentCultureIgnoreCase))
                        );
                if (result == null || result.Count() == 0)
                {
                    //Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chimes.wav");
                    OpenNETCF.Media.SystemSounds.Hand.Play();
                    r = dsObecne.RFID.NewRFIDRow();
                    r.ID = ck.TagID;
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
                    r.EPC = ck.EPCMemory;
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

        /// <summary>
        /// Najde polozku v db  a vrati ji
        /// TODO- co když jich najde víc ?
        /// </summary>
        /// <param name="carkod"> carovy kod ze ctecky</param>
        /// <param name="mrow"> radek z tabulky</param>
        /// <returns> vrati jestli nasel jednu</returns>
        //private bool NajdiPolozku(
        //    string carkod,
        //    out Fask.SQLiteDBs.DataSets.Obecne.RFIDRow Inventura2.MAJETEKRow mrow)
        //{
        //    mrow = null;
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        // ScannerStop();
        //        //string crkd = this.PrekladDebud(carkod);
        //        //1) najit polozky
        //        Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Globals.ta_majetek.GetDataByEAN(carkod);

        //        if (dt_majetek.Count == 0) //nenalezeno
        //        {
        //            Cursor.Current = Cursors.Default;
        //            ListPolozkyDS.PolozkyNenalezeneRow pnrow = listPolozkyDS.PolozkyNenalezene.FindByEAN(carkod);
        //            if (pnrow == null)
        //            {
        //                Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chyba.wav");
        //                listPolozkyDS.PolozkyNenalezene.AddPolozkyNenalezeneRow(carkod);
        //                textBoxN.Text = (new StringBuilder(textBoxN.Text)).AppendLine(carkod).ToString();
        //            }
        //            return false;
        //        }
        //        else if (dt_majetek.Count > 1) //nalezeno vice zaznamu
        //        {
        //            Cursor.Current = Cursors.Default;
        //            // TODO : co s vice nalezenymi variantami? => nasledna akce
        //            ListPolozkyDS.PolozkyNalezeneVicekratRow pnv = listPolozkyDS.PolozkyNalezeneVicekrat.FindByEAN(carkod);
        //            if (pnv == null)
        //            {
        //                Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chyba.wav");
        //                listPolozkyDS.PolozkyNalezeneVicekrat.AddPolozkyNalezeneVicekratRow(carkod);
        //                textBoxV.Text = (new StringBuilder(textBoxV.Text)).AppendLine(carkod).ToString();
        //            }
        //            return false;
        //        }
        //        else //je pouze jedna (0 byt uz nemuze)
        //        {
        //            mrow = dt_majetek[0];
        //        }

        //        return true;
        //    }//jinak chyti vyjimku
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //        Logging.Log.Write(ex.Message, this.Text);
        //        return false;
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        ///// <summary>
        ///// Slouzi pro debu uceli jeden kod za druhy kod da 
        ///// </summary>
        ///// <param name="kod"></param>
        ///// <returns></returns>
        //private string PrekladDebud(string kod)
        //{
        //    if (kod == "139E") return "10006387";
        //    if (kod == "642") return "10006388";
        //    if (kod == "112") return "10006392";
        //    return string.Empty;
        //}

        /// <summary>
        /// Prida do tabulky novy radek
        /// </summary>
        /// <param name="mrow">radek ktery se prida</param>
        //private void PridejDoTabulky(Fask.SQLiteDBs.DataSets.Obecne.RFIDRow mrow)
        //{
        //    //Test na dokoncenost polozky.
        //    //Pridavaji se do seznamu pouze polozky, ktere jeste nejsou kompletne nasnimany.
        //    //Pokud naleznu polozku, ktera neni kompletne nasnimana, pak jeji c.k. pridam do seznamu jiz nasnimanych...
        //    if (mrow.KUSU - mrow.NACTENO <= 0) //Nic jiz nezbyva, tak nepridavam, ale hodim do seznamu jiz nasnimanych c.k.
        //    {
        //        ListPolozkyDS.PolozkyNalezeneNasnimaneRow pnnrow = listPolozkyDS.PolozkyNalezeneNasnimane.FindByEAN(mrow.EAN.Trim());
        //        if (pnnrow == null)
        //        {
        //            Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chimes.wav");
        //            listPolozkyDS.PolozkyNalezeneNasnimane.AddPolozkyNalezeneNasnimaneRow(mrow.EAN.Trim());
        //            textBoxS.Text = (new StringBuilder(textBoxS.Text)).AppendLine(mrow.EAN.Trim()).ToString();
        //        }
        //        return;
        //    }

        //    //novy radek
        //    Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow newRow = listPolozkyDS.Polozky.NewPolozkyRow();
        //    //pridavani jednotlivych veci do noveho radku postupne
        //    #region Pridavani do radku pro tabulky co zobrazuje naskenovane polozky
        //    try
        //    {
        //        newRow.I_CISLO = (string)mrow.I_CISLO;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetI_CISLONull();
        //    }

        //    try
        //    {
        //        newRow.NAZEV = (string)mrow.NAZEV;
        //    }
        //    catch (Exception)
        //    {

        //        newRow.SetNAZEVNull();
        //    }
        //    try
        //    {
        //        newRow.ID = (int)mrow.ID;
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    try
        //    {
        //        newRow.KATEGORIE = (string)mrow.KATEGORIE;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetKATEGORIENull();
        //    }


        //    try
        //    {
        //        newRow.STRED = (string)mrow.STRED;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetSTREDNull();
        //    }

        //    try
        //    {
        //        newRow.OSOBA = (int)mrow.OSOBA;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetOSOBANull();
        //    }

        //    try
        //    {
        //        newRow.LOKACE1 = (string)mrow.LOKACE1;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetLOKACE1Null();
        //    }
        //    try
        //    {
        //        newRow.LOKACE2 = (string)mrow.LOKACE2;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetLOKACE2Null();
        //    }

        //    try
        //    {
        //        newRow.KANCELAR = (string)mrow.KANCELAR;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetKANCELARNull();
        //    }

        //    try
        //    {
        //        newRow.EAN = (string)mrow.EAN;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetEANNull();
        //    }
        //    //bere e z global hodnoty - nastavu je se propridavani polozek
        //    //newRow.KUSU = pocetnactenichTagu;//da tam kolik je jich nasnimanych
        //    //newRow.NALEZENO = pocetnactenichTagu;

        //    if (!mrow.IsKUSUNull())
        //        newRow.KUSU = mrow.KUSU;

        //    try
        //    {
        //        newRow.KLIC_LOK = (int)mrow.KLIC_LOK;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetKLIC_LOKNull();
        //    }

        //    try
        //    {
        //        newRow.NACTENO = (decimal)mrow.NACTENO;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.NACTENO = 0;
        //    }

        //    try
        //    {
        //        newRow.CASNACTENO = DateTime.Now;
        //    }
        //    catch (Exception)
        //    {
        //        newRow.SetCASNACTENONull();
        //    }

        //    //ted jeste ty dalsi =====================
        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt_stredisko = Globals.ta_stredisko.GetDataByStredisko(newRow.STRED);
        //        if (dt_stredisko.Count > 0)
        //            newRow.STRED_NAZEV = dt_stredisko[0].NAZEV.Trim();
        //        else
        //            newRow.SetSTRED_NAZEVNull();
        //    }
        //    catch //(Exception ex)
        //    {
        //        newRow.SetSTRED_NAZEVNull();
        //    }

        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt_osoby = Globals.ta_osoby.GetDataByOSOBA_ZODP(newRow.OSOBA);
        //        if (dt_osoby.Count > 0)
        //            newRow.OSOBA_NAZEV = dt_osoby[0].JMENO.Trim() + " " + dt_osoby[0].PRIJMENI.Trim();
        //        else
        //            newRow.SetOSOBA_NAZEVNull();
        //    }
        //    catch //(Exception ex)
        //    {
        //        newRow.SetOSOBA_NAZEVNull();
        //    }


        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt_kancl = Globals.ta_kancl.GetDataByKANCL(newRow.KANCELAR);
        //        if (dt_kancl.Count > 0)
        //            newRow.KANCELAR_NAZEV = dt_kancl[0].TEXT.Trim();
        //        else
        //            newRow.SetKANCELAR_NAZEVNull();
        //    }
        //    catch //(Exception ex)
        //    {
        //        newRow.SetKANCELAR_NAZEVNull();
        //    }

        //    try
        //    {
        //        Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt_lokace = Globals.ta_lokace.GetDataByKLIC_LOK(newRow.KLIC_LOK);
        //        if (dt_lokace.Count > 0)
        //            newRow.LOKACE_NAZEV = dt_lokace[0].NAZEV.Trim();
        //        else
        //            newRow.SetLOKACE_NAZEVNull();
        //    }
        //    catch //(Exception ex)
        //    {
        //        newRow.SetLOKACE_NAZEVNull();
        //    }

        //    #endregion
        //    //pridam ji
        //    listPolozkyDS.Polozky.AddPolozkyRow(newRow);
        //    Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "info.wav");
        //    //Nastavit kursor na vlozenou polozku ... ??? Nebude to moc pomale pri vetsich mnozstvich???
        //    dataGrid2Polozky.UnSelectAll(); //nejdriv to musim vsechno odznacit, protoze to nezvlada automaticky posunout jiz oznacene ...
        //    dataGrid2Polozky.CurrentRow = newRow;

        //}
        #endregion

        #region Zpracovani polozek a odstraneni z tabulky
        /// <summary>
        /// aktivuje zpracování všech položek 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void menuItemZpracujVse_Click(object sender, EventArgs e)
        //{
        //    if (RFIDScannerActivationTestStopQuestion())
        //    {
        //        return;
        //    }

        //    // TODO : proverit algoritmus ukladani dat ... a zpracovani chyb(rozdilu...)

        //    //I use the more elegant “black list”:
        //    //List remList = new List();
        //    //foreach (TestClass tc in list)
        //    //    if (hasToBeDeleted) remList.Add(tc);
        //    //ListPolozkyDS polozky = listPolozkyDS.Clone();//udelam kopii
        //    List<ListPolozkyDS.PolozkyRow> rowForDelete = new List<ListPolozkyDS.PolozkyRow>();

        //    try
        //    {
        //        ScannerStop();

        //        //vytvorim si formular - stejne jako v listpolozky
        //        NaplnPolozku naplnp = this.NaplnPolozkuForm;
        //        naplnp.SetDefaultValues();
        //        naplnp.Owner = this;
        //        naplnp.Popis = "Množství";
        //        naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
        //        naplnp.AllowEmpty = false;
        //        naplnp.Text = "Množství";
        //        //naplnp.ScannerOff = true;//nebdu uz pouzivat scanner => je pouzito nize
        //        naplnp.Kod = "1";//dam tam kusy 
        //        if (Globals.active_parametry != null)
        //            naplnp.ScannerOff = !Globals.active_parametry.CFG_MnozstviScannerem;

        //        foreach (ListPolozkyDS.PolozkyRow prow in listPolozkyDS.Polozky.Rows)
        //        {
        //            Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable table = Globals.ta_majetek.GetDataByID(prow.ID);  // mrow
        //            Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = table[0];
        //            naplnp.SetDefaultValues();
        //            naplnp.Popis = "Množství";
        //            naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
        //            naplnp.AllowEmpty = false;
        //            naplnp.Text = "Množství";
        //            naplnp.MajetekRow = mrow;
        //            naplnp.Kod = "1";
        //            if (Globals.active_parametry != null)
        //                naplnp.ScannerOff = !Globals.active_parametry.CFG_MnozstviScannerem;

        //            if (!naplnp.PerformTest()) //pokud neni v porádku
        //            {
        //                MessageBoxBigTimeout.Show("Položka '" + mrow.NAZEV.Trim() + "' není v pořádku.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

        //                #region Post kontroly zadanych hodnot
        //                //if (naplnp.ShowDialog() == DialogResult.Cancel)
        //                //    return;

        //                //Kontroly zadavani hodnot ...
        //                while (true)
        //                {
        //                    if (naplnp.ShowDialog() == DialogResult.Cancel)
        //                    {
        //                        if (Globals.active_parametry != null && Globals.active_parametry.CFG_KontrolaUplnostiPolozky)
        //                        {
        //                            if (mrow.NACTENO < mrow.KUSU)
        //                            {
        //                                if (MessageBoxBig.Show("Není kompletní.\nChcete skončit zadávání této položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //                                    continue;
        //                            }
        //                        }
        //                        return;
        //                    }

        //                    if (Globals.active_parametry != null && Globals.active_parametry.CFG_KontrolaUplnostiPolozky)
        //                    {
        //                        if (mrow.NACTENO + Convert.ToDecimal(naplnp.Kod) > mrow.KUSU)
        //                        {
        //                            if (MessageBoxBig.Show("Zadané množství je větší než má být načteno, chcete pokračovat a data uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //                                continue;
        //                        }
        //                    }

        //                    if (Globals.active_parametry != null)
        //                    {
        //                        if (!Globals.active_parametry.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
        //                        {
        //                            MessageBoxBig.Show("Zadané množství musí být kladné a větší jak 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //                            continue;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToDecimal(naplnp.Kod) == 0)
        //                        {
        //                            MessageBoxBig.Show("Zadané množství musí být různé od 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //                            continue;
        //                        }
        //                    }

        //                    break; //dostane-li se až sem, tak je vše ok ...
        //                }
        //                #endregion
        //            }

        //            try
        //            {
        //                //mnozsvti co bylo zadano
        //                decimal mnozstvi = Convert.ToDecimal(naplnp.Kod);//da se jedna
        //                Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow newlokace = naplnp.Lokace;
        //                Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow newkancl = naplnp.Kancelar;
        //                Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow newosoba = naplnp.Osoba;
        //                Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow newstredisko = naplnp.Stredisko;

        //                #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

        //                try
        //                {
        //                    Cursor.Current = Cursors.WaitCursor;
        //                    _inventura2.INVENTUR.Clear(); //
        //                    //_inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();
        //                    Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow newi2row = _inventura2.INVENTUR.NewINVENTURRow();
        //                    //dam mu nove id
        //                    newi2row.ID = ((Globals.ta_inventur.MaxID() ?? 0) + 1);
        //                    //pokud nejsou null
        //                    if (!mrow.IsKATEGORIENull()) newi2row.KATEGORIE = mrow.KATEGORIE;
        //                    if (!mrow.IsI_CISLONull()) newi2row.I_CISLO = mrow.I_CISLO;
        //                    if (!mrow.IsNAZEVNull()) newi2row.NAZEV = mrow.NAZEV;
        //                    //pokud bylo strdisko null
        //                    if (newstredisko == null) newi2row.SetSTREDNull();
        //                    else newi2row.STRED = newstredisko.STREDISKO;
        //                    //osoba null tak bude nul jinak se veme z osoby
        //                    if (newosoba == null) newi2row.SetOSOBANull();
        //                    else newi2row.OSOBA = newosoba.OSOBA_ZODP;
        //                    //pokud je lokace null
        //                    if (newlokace == null)
        //                    {
        //                        newi2row.SetLOKACE1Null();
        //                        newi2row.SetLOKACE2Null();
        //                        newi2row.SetKLIC_LOKNull();
        //                    }
        //                    else //neni null
        //                    {
        //                        newi2row.LOKACE1 = newlokace.LOKACE1;
        //                        newi2row.LOKACE2 = newlokace.LOKACE2;
        //                        newi2row.KLIC_LOK = newlokace.KLIC_LOK;
        //                    }
        //                    //pokud je kandl null
        //                    if (newkancl == null) newi2row.SetKANCELARNull();
        //                    else newi2row.KANCELAR = newkancl.KANCL;
        //                    //pokud je car kod tak se priradi  jinak se tam da mnoztvi
        //                    if (!mrow.IsEANNull()) newi2row.EAN = mrow.EAN;
        //                    newi2row.KUSU = mnozstvi;
        //                    //priradim tam jeste zbyle hodnoty
        //                    if (!mrow.IsID_INVNull()) newi2row.ID_INV = Convert.ToDecimal(mrow.ID_INV);
        //                    newi2row.OS_ZPR = MST_Global.UserID.ToString();
        //                    newi2row.ID_TERM = MST_Global.TerminalID;
        //                    newi2row.CAS_ZPR = DateTime.Now;
        //                    newi2row.ID_MAJETEK = mrow.ID;
        //                #endregion

        //                    _inventura2.INVENTUR.AddINVENTURRow(newi2row);
        //                    Globals.ta_inventur.Update(newi2row);//dam update 
        //                    rowForDelete.Add(prow);
        //                    //SmazRadekZTabulky(mrow);
        //                    UpdateNacteno(newi2row);
        //                    _inventura2.INVENTUR.Clear(); //asi neni potreba
        //                }
        //                finally
        //                {
        //                    Cursor.Current = Cursors.Default;
        //                }
        //                //=======================================
        //                //list.RemoveAll(new Predicate(delegate(TestClass x) { return remList.Contains(x); }));
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            //smazu ty radky
        //            for (int i = 0; i < rowForDelete.Count; i++)
        //            {
        //                SmazRadekZTabulky(rowForDelete[i]);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //        }

        //        // TODO : oprava vypnuti scanneru pri nezobrazeni pomoci showdialog
        //        if (this._naplnPolozkuForm != null || !this._naplnPolozkuForm.IsDisposed)
        //            this._naplnPolozkuForm.ScannerOff = true;

        //        ScannerStart();
        //    }
        //    kontrolaKonce();
        //}

        //private void kontrolaKonce()
        //{
        //    if (listPolozkyDS.Polozky.Rows.Count < 1)
        //    {
        //        DialogResult dr = MessageBoxBig.Show("Přejete si znovu aktivovat snímání RFID?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
        //        if (dr == DialogResult.Yes)
        //        {
        //            this.buttonRFIDONOFF_Click(this, null);
        //        }
        //        else if (dr == DialogResult.No)
        //        {
        //            //menuItemKonec_Click(this, null);
        //            PerformKonec(false);
        //        }
        //    }
        //}

        private void menuItemZpracuj_Click(object sender, EventArgs e)
        {
            //ZpracujOznacenouPolozku();
        }

        /// <summary>
        /// Zpracuje polozku oznacenou v tabulku
        /// Stejne jako v ListPolozky
        /// </summary>
        /// <returns></returns>
        //public void ZpracujOznacenouPolozku()
        //{
        //    NelezniPolozkuANapln(VybranaPolozka);
        //}

        /// <summary>
        /// Nalezne polozku v db a radek preda dalsi funkci pro naplneni
        /// </summary>
        /// <param name="prow">radek tabulky co se bude vyplnovat</param>
        //private void NelezniPolozkuANapln(ListPolozkyDS.PolozkyRow prow)
        //{
        //    if (RFIDScannerActivationTestStopQuestion())
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        ScannerStop();

        //        //pokud nic neni
        //        if (prow == null)
        //        {
        //            MessageBoxBig.Show("Není vybrána položka!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //            return;
        //        }
        //        //podivam se do db
        //        Cursor.Current = Cursors.WaitCursor;
        //        Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Globals.ta_majetek.GetDataByID(prow.ID);
        //        Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
        //        Cursor.Current = Cursors.Default;
        //        //nic nenasel
        //        if (dt_majetek.Count == 0)
        //        {
        //            MessageBoxBig.Show("Nenalezen záznam v tabulce !", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //            return;
        //        }//je jich vic
        //        else if (dt_majetek.Count > 1)
        //        {
        //            MessageBoxBig.Show("Nalezeno více řádků se stejným ID !!!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //            return;
        //        }
        //        else//jedna
        //        {
        //            mrow = dt_majetek[0]; //je tam jen jedna polozka a je na indexu 0
        //        }

        //        //jinak je to 1:1 a muzu to pustit dal
        //        //ted ji bue vyplnovat (zadavat do ni hodnoty
        //        VyplnPolozku(mrow);

        //    }
        //    finally //da ho na spravnykurzor
        //    {
        //        ScannerStart();
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        /// <summary>
        /// Vyplneni polozky
        /// </summary>
        /// <param name="mrow">Radek co se bude vyplnovat</param>
        //private void VyplnPolozku(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        //{
        //    try
        //    {
        //        ScannerStop();

        //        //vytvorim si formular - stejne jako v listpolozky
        //        NaplnPolozku naplnp = this.NaplnPolozkuForm;
        //        naplnp.SetDefaultValues();
        //        naplnp.MajetekRow = mrow;
        //        naplnp.Owner = this;
        //        naplnp.Popis = "Množství";
        //        naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
        //        naplnp.AllowEmpty = false;
        //        naplnp.Text = "Vložte množství";
        //        naplnp.ScannerOff = true;//nebdu uz pouzivat scanner
        //        naplnp.Kod = "1"; //mrow.KUSU.ToString();//dam tam kusy 
        //        //if (Globals.active_parametry.CFG_PredvyplnitMnozstvi)
        //        //{
        //        //    if (Globals.active_parametry.CFG_PredvyplnitMnozstviOJedna)
        //        //        naplnp.Kod = "1";
        //        //    else if (Globals.active_parametry.CFG_PredvyplnitMnozstviZbyvajici)
        //        //    {
        //        //        //decimal nasnimano = Globals.ta_inventur.NasnimanoKusu(mrow.I_CISLO) ?? 0;
        //        //        //decimal zbyva = nasnimano - mrow.KUSU;
        //        //        decimal zbyva = mrow.ZBYVA;
        //        //        naplnp.Kod = (zbyva > 0 ? zbyva : 0).ToString(Settings.UIFormatDesCisel);
        //        //    }
        //        //}


        //        #region Post kontroly zadanych hodnot
        //        //if (naplnp.ShowDialog() == DialogResult.Cancel)
        //        //    return;

        //        //Kontroly zadavani hodnot ...
        //        while (true)
        //        {
        //            if (naplnp.ShowDialog() == DialogResult.Cancel)
        //            {
        //                if (Globals.active_parametry != null && Globals.active_parametry.CFG_KontrolaUplnostiPolozky)
        //                {
        //                    if (mrow.NACTENO < mrow.KUSU)
        //                    {
        //                        if (MessageBoxBig.Show("Není kompletní.\nChcete skončit zadávání této položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //                            continue;
        //                    }
        //                }
        //                return;
        //            }

        //            if (Globals.active_parametry != null && Globals.active_parametry.CFG_KontrolaUplnostiPolozky)
        //            {
        //                if (mrow.NACTENO + Convert.ToDecimal(naplnp.Kod) > mrow.KUSU)
        //                {
        //                    if (MessageBoxBig.Show("Zadané množství je větší než má být načteno, chcete pokračovat a data uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //                        continue;
        //                }
        //            }

        //            if (Globals.active_parametry != null)
        //            {
        //                if (!Globals.active_parametry.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
        //                {
        //                    MessageBoxBig.Show("Zadané množství musí být kladné a větší jak 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //                    continue;
        //                }
        //            }
        //            else
        //            {
        //                if (Convert.ToDecimal(naplnp.Kod) == 0)
        //                {
        //                    MessageBoxBig.Show("Zadané množství musí být různé od 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //                    continue;
        //                }
        //            }

        //            break; //dostane-li se až sem, tak je vše ok ...
        //        }
        //        #endregion

        //        //mnozsvti co bylo zadano
        //        decimal mnozstvi = Convert.ToDecimal(naplnp.Kod);
        //        Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow newlokace = naplnp.Lokace;
        //        Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow newkancl = naplnp.Kancelar;
        //        Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow newosoba = naplnp.Osoba;
        //        Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow newstredisko = naplnp.Stredisko;

        //        #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

        //        try
        //        {
        //            Cursor.Current = Cursors.WaitCursor;
        //            _inventura2.INVENTUR.Clear(); //asi neni potreba
        //            _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();
        //            Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow newi2row = _inventura2.INVENTUR.NewINVENTURRow();
        //            //dam mu nove id
        //            newi2row.ID = ((Globals.ta_inventur.MaxID() ?? 0) + 1);
        //            //pokud nejsou null
        //            if (!mrow.IsKATEGORIENull()) newi2row.KATEGORIE = mrow.KATEGORIE;
        //            if (!mrow.IsI_CISLONull()) newi2row.I_CISLO = mrow.I_CISLO;
        //            if (!mrow.IsNAZEVNull()) newi2row.NAZEV = mrow.NAZEV;
        //            //pokud bylo strdisko null
        //            if (newstredisko == null) newi2row.SetSTREDNull();
        //            else newi2row.STRED = newstredisko.STREDISKO;
        //            //osoba null tak bude nul jinak se veme z osoby
        //            if (newosoba == null) newi2row.SetOSOBANull();
        //            else newi2row.OSOBA = newosoba.OSOBA_ZODP;
        //            //pokud je lokace null
        //            if (newlokace == null)
        //            {
        //                newi2row.SetLOKACE1Null();
        //                newi2row.SetLOKACE2Null();
        //                newi2row.SetKLIC_LOKNull();
        //            }
        //            else //neni null
        //            {
        //                newi2row.LOKACE1 = newlokace.LOKACE1;
        //                newi2row.LOKACE2 = newlokace.LOKACE2;
        //                newi2row.KLIC_LOK = newlokace.KLIC_LOK;
        //            }
        //            //pokud je kandl null
        //            if (newkancl == null) newi2row.SetKANCELARNull();
        //            else newi2row.KANCELAR = newkancl.KANCL;
        //            //pokud je car kod tak se priradi  jinak se tam da mnoztvi
        //            if (!mrow.IsEANNull()) newi2row.EAN = mrow.EAN;
        //            newi2row.KUSU = mnozstvi;
        //            //priradim tam jeste zbyle hodnoty
        //            if (!mrow.IsID_INVNull()) newi2row.ID_INV = Convert.ToDecimal(mrow.ID_INV);
        //            newi2row.OS_ZPR = MST_Global.UserID.ToString();
        //            newi2row.ID_TERM = MST_Global.TerminalID;
        //            newi2row.CAS_ZPR = DateTime.Now;
        //            newi2row.ID_MAJETEK = mrow.ID;

        //            _inventura2.INVENTUR.AddINVENTURRow(newi2row);
        //            Globals.ta_inventur.Update(newi2row);//dam update 
        //            SmazRadekZTabulky(mrow);
        //            UpdateNacteno(newi2row);

        //            _inventura2.INVENTUR.Clear(); //asi neni potreba
        //        }
        //        finally
        //        {
        //            Cursor.Current = Cursors.Default;
        //        }
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //    }
        //    finally
        //    {
        //        ScannerStart();
        //    }
        //}

        /// <summary>
        /// Zmena nacteno v tabulce majetek
        /// </summary>
        /// <param name="i2row">radek co se menil v inventure</param>
        //private void UpdateNacteno(Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow i2row)
        //{            
        //    //Globals.ta_majetek.UpdateNactenoByI_CISLO(i2row.KUSU, i2row.I_CISLO);
        //    //Globals.ta_majetek.UpdateNactenoByZAZNAM(i2row.KUSU, i2row.I_CISLO, i2row.KATEGORIE);
        //    Globals.ta_majetek.UpdateNactenoByID(i2row.KUSU, i2row.ID_MAJETEK);
        //}

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

        /// <summary>
        /// smaze radek z tabulky
        /// </summary>
        /// <param name="prow">radek</param>
        //private void SmazRadekZTabulky(ListPolozkyDS.PolozkyRow prow)
        //{
        //    //pokud je null tk nic 
        //    if (prow == null)
        //        return;
        //    //smaze
        //    try
        //    {
        //        listPolozkyDS.Polozky.RemovePolozkyRow(prow);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
        //    }
        //}

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
                #region Nepotrebny kod ...
                ////TODO ulozeni dat a pak jejich zpracování 
                //foreach (Fask.MST_W.Scanner.RFIDTagData data in e.TagList)
                //{
                //    //textBoxRFIDScannerData.Text += DateTime.Now.ToString() + " " + data.BarcodeData + "\r\n";
                //    //if (nasnimaneKody.ContainsKey(data.BarcodeData))
                //    //{
                //    //    nasnimaneKody[data.BarcodeData] = (int)nasnimaneKody[data.BarcodeData] + 1;
                //    //}
                //    //else
                //    //{
                //    //    nasnimaneKody[data.BarcodeData] = 1;
                //    //}


                //    //if (
                //    //    (Program.mstw.RFIDUHFScanner.RemovedCodes != null)
                //    //    &&
                //    //    (Program.mstw.RFIDUHFScanner.RemovedCodes.Contains(data.TagID))
                //    //    )
                //    //{
                //    //    continue; // tento kod je v seznamu odstranenych kodu
                //    //}

                //    ////if (!nasnimaneKody.Contains( x => x.))
                //    //if (!nasnimaneKody.Exists( x => x.TagID == data.TagID))
                //    //{
                //    //    nasnimaneKody.Add(data);
                //    //}

                //    //var nalezene = nasnimaneKody.Where(x => x.TagID == data.TagID);
                //    //if (nalezene.Count() > 0)
                //    //{
                //    //    Fask.MST_W.Scanner.RFIDTagData dataSelected;

                //    //    if (nalezene.Count() <= 1)
                //    //    {
                //    //        dataSelected = nalezene.First();
                //    //    }
                //    //    else
                //    //    {
                //    //        if (!String.IsNullOrEmpty(data.TIDMemory))
                //    //            dataSelected = nalezene.First(x => x.TIDMemory == data.TIDMemory);
                //    //        else
                //    //            dataSelected = nalezene.First();
                //    //    }

                //    //    if (!String.IsNullOrEmpty(data.TIDMemory))
                //    //        dataSelected.TIDMemory = data.TIDMemory;
                //    //    if (!String.IsNullOrEmpty(data.EPCMemory))
                //    //        dataSelected.EPCMemory = data.EPCMemory;
                //    //    if (!String.IsNullOrEmpty(data.UserMemory))
                //    //        dataSelected.UserMemory = data.UserMemory;
                //    //    if (!String.IsNullOrEmpty(data.ReservedMemory))
                //    //        dataSelected.ReservedMemory = data.ReservedMemory;
                //    //    if (!String.IsNullOrEmpty(data.OpCode))
                //    //        dataSelected.OpCode = data.OpCode;
                //    //    if (!String.IsNullOrEmpty(data.OpStatus))
                //    //        dataSelected.OpStatus = data.OpStatus;

                //    //    dataSelected.CountReaded += data.CountReaded;

                //    //}
                //    //else
                //    //{
                //    //    nasnimaneKody.Add(data);
                //    //}

                //    //prida to tam, pokud neni vylouceny ...???
                //    nasnimaneKody.Add(data);
                //}
                #endregion
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
                    ScannerStop();

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
                    ScannerStart();
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
            ScannerFinalize();
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
                status = string.Format("{0}/{1}", dsObecne.RFID.Count, A_PocetZaznamu);
            else
                status = string.Format("{0}", dsObecne.RFID.Count);

            //if (SelectedSI != null)
            //    status += string.Format(",SN:{0}", SelectedSI.SERLTNUM.Trim());

            //try
            //{
            //    status += (SelectedSI != null ? ("," + SelectedSI["ITEMDESC"].ToString().Trim()) : string.Empty);
            //}
            //catch { }
            //status += "Načteno : ";
            //status += dsObecne.RFID.Count;

            status += " P:" + (bsRFID.Position + 1);

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

        //private enum ShowDataType
        //{
        //    Nalezene,
        //    Nenalezene,
        //    Vice,
        //    Splnene
        //}
        //private void ShowData(ShowDataType sdt)
        //{
        //    //StringBuilder sb = new StringBuilder();
        //    dataGrid2Polozky.Hide();
        //    textBoxN.Hide();
        //    textBoxS.Hide();
        //    textBoxV.Hide();
        //    switch (sdt)
        //    {
        //        case ShowDataType.Nenalezene:
        //            //foreach (ListPolozkyDS.PolozkyNenalezeneRow pn in listPolozkyDS.PolozkyNenalezene)
        //            //{
        //            //    sb.AppendLine(pn.EAN.Trim());
        //            //}
        //            //textBoxN.Text = sb.ToString();
        //            textBoxN.Show();
        //            textBoxN.Dock = DockStyle.Fill;
        //            break;
        //        case ShowDataType.Vice:
        //            //foreach (ListPolozkyDS.PolozkyNalezeneVicekratRow pnv in listPolozkyDS.PolozkyNalezeneVicekrat)
        //            //{
        //            //    sb.AppendLine(pnv.EAN.Trim());
        //            //}
        //            //textBoxV.Text = sb.ToString();
        //            textBoxV.Show();
        //            textBoxV.Dock = DockStyle.Fill;
        //            break;
        //        case ShowDataType.Splnene:
        //            //foreach (ListPolozkyDS.PolozkyNalezeneNasnimaneRow pns in listPolozkyDS.PolozkyNalezeneNasnimane)
        //            //{
        //            //    sb.AppendLine(pns.EAN.Trim());
        //            //}
        //            //textBoxS.Text = sb.ToString();
        //            textBoxS.Show();
        //            textBoxS.Dock = DockStyle.Fill;
        //            break;
        //        case ShowDataType.Nalezene:
        //        default:
        //            dataGrid2Polozky.Show();
        //            dataGrid2Polozky.Dock = DockStyle.Fill;
        //            break;
        //    }
        //}

        private void menuItemNalezene_Click(object sender, EventArgs e)
        {
            //ShowData(ShowDataType.Nalezene);
        }

        private void menuItemNenalezene_Click(object sender, EventArgs e)
        {
            //ShowData(ShowDataType.Nenalezene);
        }

        private void menuItemVice_Click(object sender, EventArgs e)
        {
            //ShowData(ShowDataType.Vice);
        }

        private void menuItemSplnene_Click(object sender, EventArgs e)
        {
            //ShowData(ShowDataType.Splnene);
        }

        #region Scanner Car.kodu
        delegate void ScannerEventHandlerCall(ScannerEventArgs e);
        private void OnScannerEvent(ScannerEventArgs e)
        {
            try
            {
                string ck = e.BarcodeData.Trim();
                if (ck.Length > 0)
                {
                    //if (!nasnimaneKody.Contains(ck))
                    if (!nasnimaneKody.Exists(x => x.TagID == ck))
                    {
                        Fask.MST_W.Scanner.RFIDTagData n = new Fask.MST_W.Scanner.RFIDTagData();
                        n.TagID = ck;
                        nasnimaneKody.Add(n);
                    }
                }
                PridejNasnimanePolozky();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
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

        private void menuItemInfo_Click(object sender, EventArgs e)
        {
            //// SelectedSI .. dodělat
            //bool rfidscanneractive = rfidactive;
            //try
            //{
            //    if (SelectedSI == null)
            //        return;

            //    if (rfidscanneractive)
            //        RFIDScannerStop();
            //    ScannerStop();

            //    using (Vydej_3.PolozkaNasnimDetail detail = new Fask.MST_W.Vydej_3.PolozkaNasnimDetail(SelectedSI))
            //    {
            //        detail.ShowDialog();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex, "MenuItemInfo");
            //    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            //}
            //finally
            //{
            //    if (rfidscanneractive)
            //        RFIDScannerStart();
            //    ScannerStart();
            //}
        }

        private void miPotvrdit_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

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
                //this.panelDetail.SuspendLayout();

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
                    dfEAN.Data = r.IsIDNull() ? null : r.ID;

                    dfTID.Data = r.IsTIDNull() ? null : r.TID;
                    dfTIDLen.Data = r.IsTIDLenNull() ? 0.ToString() : r.TIDLen;
                    if (!r.IsTIDNull())
                    {
                        RFID.TID tid = RFID.TID.Parse(r.TID);
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
                        RFID.EPC epc = RFID.EPC.Parse(r.EPC);
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

                    dfUSER.Data = r.IsUSERNull() ? null : r.USER;
                    dfUSERLen.Data = r.IsUSERLenNull() ? 0.ToString() : r.USERLen;
                    if (!r.IsUSERLenNull())
                    {
                        try
                        {
                            //RFID.USER user = RFID.USER.Parse(r.USER);
                            RFID.USER_512b usr512 = new USER_512b(r.USER);
                            textBoxUSER.Text = "Item Number: " + usr512.ItemNumber.Trim();
                            textBoxUSER.Text += "\r\nItem Desc: " + usr512.ItemDesc.Trim();
                            textBoxUSER.Text += "\r\nSerial :" + usr512.SerltNumber.Trim();
                        }
                        catch (Exception exUser)
                        {
                            textBoxUSER.Text = "User memory parse error:\n" + exUser.Message;
                        }
                    }

                    dfRFIDReserved.Data = r.IsRESERVEDNull() ? null : r.RESERVED;
                    dfRESERVEDLen.Data = r.IsRESERVEDLenNull() ? 0.ToString() : r.RESERVEDLen;

                    dfCountSeen.Data = r.Seen.ToString();
                    dfRSSI.Data = r.RSSI.ToString();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                //panelDetail.ResumeLayout();
            }

            UpdateRFIDUIState();
            UpdateStatusBar();
        }

        private void menuItemAllMemories_Click(object sender, EventArgs e)
        {
            menuItemAllMemories.Checked = !menuItemAllMemories.Checked;
            RFIDScannerAllMemoriesSet();
        }

        #region EPC Memory Operations ...
        private string datawriteEpcEAN = string.Empty;
        private string datawriteEpcSN = string.Empty;
        private void menuItemRfidEpcWrite_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string dataEAN = string.Empty;
                string dataSN = string.Empty;
                dataEAN = datawriteEpcEAN;
                dataSN = datawriteEpcSN;

                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow rRow = A_VybranaPolozka;
                if (rRow == null)
                    throw new Exception("No row selected");

                if (InputBox.Show("EAN", datawriteEpcEAN, out dataEAN, true)
                    == DialogResult.Cancel)
                    return;
                datawriteEpcEAN = dataEAN;

                if (InputBox.Show("Seriové číslo", datawriteEpcSN, out dataSN, true)
                    == DialogResult.Cancel)
                    return;
                datawriteEpcSN = dataSN;

                // EPC URI SGTIN96
                //RFID.EPSGlobal_sgtin96 sgtin96 = new Fask.MST_W.RFID.EPSGlobal_sgtin96();
                //sgtin96.EAN = datawriteEpcEAN;
                //sgtin96.SerialNumber = uint.Parse(datawriteEpcSN);
                RFID.EPC_SGTIN96 sgtin96 = new Fask.MST_W.RFID.EPC_SGTIN96(A_VybranaPolozka.EPC);
                sgtin96.EPCCodeHeaderValue = RFID.EPC_SGTIN96.EPCCodeHeaderValueSGTIN96;
                sgtin96.FilterValue = (byte)RFID.EPC_SGTIN96.filterValues.PointOfSale;
                sgtin96.PC_Length = 96; //96bitu pro EPC kod ...
                sgtin96.PC_Toggle = false;
                sgtin96.PC_AttributeBits = 0;
                sgtin96.PC_AttributeBits_IsHazardousMaterial = false;
                sgtin96.PartitionObject = RFID.EPC_SGTIN96.SGTIN_Partition.Default;

                sgtin96.EAN = datawriteEpcEAN;
                sgtin96.Serial = long.Parse(datawriteEpcSN);

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                string accesspassword = null;
                if (!A_VybranaPolozka.IsRESERVEDNull())
                    accesspassword = A_VybranaPolozka.RESERVED.Substring(0, A_VybranaPolozka.RESERVED.Trim().Length / 2);

                if (DialogResult.No == MessageBox.Show(
                    sgtin96.ToEPCURI() +
                    "\n" +
                    sgtin96.MemoryHex
                    , "Data k zapisu:", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    )
                    return;

                string newtagid = A_VybranaPolozka.ID; // ean nyni obsahuje tagid ... ???
                if (rfidscanner.EPCWrite(A_VybranaPolozka.ID, sgtin96.MemoryHex, accesspassword, out newtagid))
                {
                    // uspech ... pokus o nacteni tagu ...
                    A_VybranaPolozka.ID = newtagid;
                    string newepcdata = string.Empty;
                    if (rfidscanner.EPCRead(A_VybranaPolozka.ID, out newepcdata))
                    {
                        A_VybranaPolozka.EPC = newepcdata;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }
        }

        private void menuItemRFIDEpcRead_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                //rfidscanner.EPCLock(A_VybranaPolozka.EAN);
                string data = string.Empty;
                if (rfidscanner.EPCRead(A_VybranaPolozka.ID, out data))
                {
                    A_VybranaPolozka.EPC = data;
                    //datawriteEpcSN = data;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }
        }
        #endregion

        #region User Memory Operations ...
        private string datawriteUser = string.Empty;

        private void menuItemRFIDUserWrite_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string data = string.Empty;
                data = datawriteUser;
                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow rRow = A_VybranaPolozka;
                if (rRow == null)
                    throw new Exception("No row selected");


                if (InputBox.Show("Data zapsat", datawriteUser, out data)
                    == DialogResult.Cancel)
                    return;
                datawriteUser = data;

                //formatovani dat na pozadovanou velikost ...
                string d = (new StringBuilder()).Append('0', 128).ToString();
                data = d.RangeReplace(0, data.Length - 1, data);

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                rfidscanner.USERWrite(A_VybranaPolozka.ID, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }
        }

        private void menuItemRfidUserRead_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                //rfidscanner.EPCLock(A_VybranaPolozka.EAN);
                string data = string.Empty;
                if (rfidscanner.USERRead(A_VybranaPolozka.ID, out data))
                {
                    A_VybranaPolozka.USER = data;
                    datawriteUser = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }
        }

        #endregion

        #region RESERVED Memory Operations ...

        private string datawriteReserved = string.Empty;

        private void menuItemRFIDReservedWrite_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                string data = string.Empty;
                data = datawriteReserved;
                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow rRow = A_VybranaPolozka;
                if (rRow == null)
                    throw new Exception("No row selected");


                if (InputBox.Show("Data zapsat", datawriteReserved, out data)
                    == DialogResult.Cancel)
                    return;
                datawriteReserved = data;

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                rfidscanner.RESERVEDWrite(A_VybranaPolozka.ID, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logging.Log.Write(ex);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }

        }

        private void menuItemRFIDReservedRead_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                //rfidscanner.EPCLock(A_VybranaPolozka.EAN);
                string data = string.Empty;
                if (rfidscanner.RESERVEDRead(A_VybranaPolozka.ID, out data))
                {
                    A_VybranaPolozka.RESERVED = data;
                    datawriteReserved = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }

        }
        #endregion

        #region TID Memory Operations ...

        private void menuItemRfidTIDRead_Click(object sender, EventArgs e)
        {
            try
            {
                ScannerStop();

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                    return;

                //rfidscanner.EPCLock(A_VybranaPolozka.EAN);
                string data = string.Empty;
                if (rfidscanner.TIDRead(A_VybranaPolozka.ID, out data))
                {
                    A_VybranaPolozka.TID = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ScannerStart();
                UpdateUI();
            }
        }

        #endregion

        private void menuItem10_Click(object sender, EventArgs e)
        {
            WriteSelectedTagsEPC();
        }

        //private System.Threading.Thread threadWriteSelectedTagsEpc = new System.Threading.Thread(;
        OpenNETCF.ComponentModel.BackgroundWorker mbw = null;

        private void WriteSelectedTagsEPC()
        {
            if (mbw != null && mbw.IsBusy)
            {
                mbw.CancelAsync();
                return;
            }

            mbw = new OpenNETCF.ComponentModel.BackgroundWorker();
            mbw.WorkerReportsProgress = true;
            mbw.WorkerSupportsCancellation = true;

            mbw.DoWork += new OpenNETCF.ComponentModel.DoWorkEventHandler(mbw_DoWork);
            mbw.RunWorkerCompleted += new OpenNETCF.ComponentModel.RunWorkerCompletedEventHandler(mbw_RunWorkerCompleted);
            mbw.ProgressChanged += new OpenNETCF.ComponentModel.ProgressChangedEventHandler(mbw_ProgressChanged);

            mbw.RunWorkerAsync();
        }

        void mbw_ProgressChanged(object sender, OpenNETCF.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                ;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        void mbw_RunWorkerCompleted(object sender, OpenNETCF.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                mbw = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MBW work completed, error: " + ex.Message);
            }
        }

        void mbw_DoWork(object sender, OpenNETCF.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                ScannerStop();

                string dataEAN = string.Empty;
                string dataSN = string.Empty;
                dataEAN = datawriteEpcEAN;
                dataSN = datawriteEpcSN;

                Fask.SQLiteDBs.DataSets.Obecne.RFIDRow rRow = A_VybranaPolozka;
                if (rRow == null)
                    throw new Exception("No row selected");

                if (InputBox.Show("EAN", datawriteEpcEAN, out dataEAN, true)
                    == DialogResult.Cancel)
                    return;
                datawriteEpcEAN = dataEAN;

                if (InputBox.Show("1. Seriové číslo", datawriteEpcSN, out dataSN, true)
                    == DialogResult.Cancel)
                    return;
                datawriteEpcSN = dataSN;

                long serialNumber = long.Parse(datawriteEpcSN);

                // EPC URI SGTIN96
                //RFID.EPSGlobal_sgtin96 sgtin96 = new Fask.MST_W.RFID.EPSGlobal_sgtin96();
                //sgtin96.EAN = datawriteEpcEAN;
                //sgtin96.SerialNumber = uint.Parse(datawriteEpcSN);

                Scanner.ScannerRFIDMC319Z_v2 rfidscanner = Program.mstw.RFIDUHFScanner as Scanner.ScannerRFIDMC319Z_v2;
                if (rfidscanner == null)
                {
                    throw new Exception("RFID scanner not found ...");
                }

                // TODO : zadani hesla ???
                string accesspassword = null;

                //foreach (var item in A_DS_Nasnimane.RFID)
                while (A_DS_Nasnimane.RFID.Count > 0)
                {
                    if (mbw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }


                    var item = A_DS_Nasnimane.RFID[0];

                    RFID.EPC_SGTIN96 sgtin96 = new Fask.MST_W.RFID.EPC_SGTIN96(item.EPC);
                    sgtin96.EPCCodeHeaderValue = RFID.EPC_SGTIN96.EPCCodeHeaderValueSGTIN96;
                    sgtin96.FilterValue = (byte)RFID.EPC_SGTIN96.filterValues.PointOfSale;
                    sgtin96.PC_Length = 96; //96bitu pro EPC kod ...
                    sgtin96.PC_Toggle = false;
                    sgtin96.PC_AttributeBits = 0;
                    sgtin96.PC_AttributeBits_IsHazardousMaterial = false;
                    sgtin96.PartitionObject = RFID.EPC_SGTIN96.SGTIN_Partition.Default;

                    sgtin96.EAN = datawriteEpcEAN;

                    sgtin96.Serial = serialNumber;

                    //if (DialogResult.No == MessageBox.Show(
                    //    sgtin96.ToEPCURI() +
                    //    "\n" +
                    //    sgtin96.MemoryHex
                    //    , "Data k zapisu:", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    //    )
                    //    return;

                    try
                    {
                        string newtagid = item.ID; // ean obsahuje tagid ... ???
                        if (rfidscanner.EPCWrite(item.ID, sgtin96.MemoryHex, accesspassword, out newtagid))
                        {
                            //// uspech ... pokus o nacteni tagu ...
                            //item.EAN = newtagid;
                            //string newepcdata = string.Empty;
                            //if (rfidscanner.EPCRead(item.EAN, out newepcdata))
                            //{
                            //    item.EPC = newepcdata;
                            //}

                            // Pokud je uspech zapsani, tak ho z tohoto seznamu smazu ...
                            // nasledne musi dojit k novemu nacteni tagu ...
                            this.Invoke((System.Threading.ThreadStart)delegate()
                            {
                                A_DS_Nasnimane.RFID.RemoveRFIDRow(item);
                            });
                            serialNumber++; //dalsi SN bude pouzito 
                        }

                    }
                    catch (Exception ex)
                    {
                        this.Invoke((System.Threading.ThreadStart)delegate() {
                            statusBar1.Text = DateTime.Now.ToString("HH:mm:ss") + " : " + ex.Message;
                        });
                        
                    }
                    // uspani vlakna na nejakou dobu kvuli rfscanneru ...???
                    System.Threading.Thread.Sleep(100);

                }

                this.Invoke((System.Threading.ThreadStart)delegate()
                {
                    statusBar1.Text = DateTime.Now.ToString("HH:mm:ss") + " : " + "Zápis tagů dokončen";
                });
            }
            catch (Exception ex)
            {
                this.Invoke((System.Threading.ThreadStart)delegate()
                {
                    statusBar1.Text = DateTime.Now.ToString("HH:mm:ss") + " : " + ex.Message;
                });

                Logging.Log.Write(ex);
            }
            finally
            {
                this.Invoke((System.Threading.ThreadStart)delegate()
                {
                    ScannerStart();
                    //UpdateUI();
                });
            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            try
            {                
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
        }
    }
}