using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.Vydej_3
{
    /// <summary>
    /// Formular pro list variant baleni(Model Numbers)
    /// </summary>
    public partial class ListMNForm3 : System.Windows.Forms.Form
    {
        #region Premenne a typy
        delegate void DelegateStringStringFloat(string s1, string s2, float f1);
        delegate void DelegateString(string kod);

        enum LeftRight {Left, Right};

        private Schema.Sklad nasklade;
        private Timer timer;
        private Fask.SQLiteDBs.DataSets.Vydej vydejData;
        private int chosenRowIndex;
        private Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow chosenRow;

        //Typ vyberu - vice viz Classes.Enums (0 == nesnastaveno, ale nemelo by byt - znaci chybu v programu)
        private byte _input_mode = 0;

        #endregion

        #region Vlastnosti
        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow ChosenRow
        {
            get
            {
                return chosenRow;
            }
        }
        #endregion

        #region Constructors
        public ListMNForm3(Fask.SQLiteDBs.DataSets.Vydej vydejData, bool nacistVisible)
        {
            InitializeComponent();

            this.nacist_l.Visible = nacistVisible;
			this.MJ_l.Visible = nacistVisible;
            this.vydejData = vydejData;
            this.chosenRowIndex = 0;
            this.chosenRow = this.vydejData.CZMST_SE[chosenRowIndex];
            this.KeyPreview = true;

            try
            {
                nasklade = new Schema.Sklad();
                nasklade.ReadXml(Main.CiselnikSkladuFileName);
            }
            catch { }

            timer = new Timer();
            timer.Interval = 60000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }
        public ListMNForm3(Fask.SQLiteDBs.DataSets.Vydej vydejData, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow vybrata, bool nacistVisible)
        {
            InitializeComponent();

            this.nacist_l.Visible = nacistVisible;
			this.MJ_l.Visible = nacistVisible;
            this.vydejData = vydejData;
            this.chosenRow = vybrata;
            this.chosenRowIndex = vydejData.CZMST_SE.Rows.IndexOf(this.chosenRow);
            this.KeyPreview = true;

            try
            {
                nasklade = new Fask.MST_W.Schema.Sklad();
                nasklade.ReadXml(Main.CiselnikSkladuFileName);
            }
            catch { }

            timer = new Timer();
            timer.Interval = 60000;
            timer.Tick+=new EventHandler(timer_Tick);
            timer.Enabled = true;
        }
        #endregion

        #region Functions
        private void dohledejPolozku(string CZ_CarKod)
        {// bude dohledavat polozku podle sejmuteho CZ_CarKod
            try
            {
                Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow[] VERows = (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow[])vydejData.CZMST_SE.Select("CZ_CarKod='" + CZ_CarKod + "' OR vnditnum='" + CZ_CarKod + "'", null, DataViewRowState.CurrentRows);
                if (VERows.Length == 0)
                {// kod nebyl nalezen

                    //if (vydejData.Parametry[0].CONFIG_NOVA_KARTA)
                    //{// zalozi novou kartu
                    //    NovaKartaForm novaKartaForm = new NovaKartaForm();
                    //    if (novaKartaForm.ShowDialog() == DialogResult.Cancel)
                    //        return;

                    //    chosenRow = vydejData.CZMST_SE.NewCZMST_SERow();
                    //    chosenRow.CZ_SerNum_Track = novaKartaForm.SerNum_Track;

                    //    // vyplnim zbyle polozky
                    //    chosenRow.SOPNUMBE = "Neuvedeno";       // vyplni SOPNUMBE, aby se dalo listovat
                    //    chosenRow.ITEMNMBR = "Nová položka";    // vyplni ITEMNMBR, aby se dalo listovat
                    //    chosenRow.ITEMDESC = "";
                    //    chosenRow.VNDDOCNM = "";
                    //    chosenRow.VNDITNUM = "";
                    //    chosenRow.ORD = -1;                     // indikuje novou kartu
                    //    chosenRow.CZ_CarKod = CZ_CarKod;        // pokud se bude zadavat balici list, pak je v CZ_CarKod docasne ulozen jeho kod
                    //    chosenRow.LOCNCODE = "";
                    //    chosenRow.QTYSHPPD = 9999; // aby nervala kontrola poctu
                    //    chosenRow.QTYPACK = 0;
                    //    chosenRow.CZ_DatVyr_Delka = 0;
                    //    chosenRow.CZ_SerNum_Delka = 0;
                    //    chosenRow.CZ_SW_Delka = 0;
                    //    chosenRow.CZ_Doslo = 0;

                    //    if (chosenRow.CZ_SerNum_Track != 9)
                    //    {// zjistim, jestli chce zadavat DV a SW. Nebude delat, pokud generuje nove baleni
                    //        if (MessageBoxBig.Show("Vyžadovat vložení: " + MST_Global.DVName + "?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    //            == DialogResult.Yes)
                    //            chosenRow.CZ_DatVyr_Track = 1;
                    //        else
                    //            chosenRow.CZ_DatVyr_Track = 0;

                    //        if (MessageBoxBig.Show("Vyžadovat vložení: " + MST_Global.SWName + "?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    //            == DialogResult.Yes)
                    //            chosenRow.CZ_SW_Track = 1;
                    //        else
                    //            chosenRow.CZ_SW_Track = 0;
                    //    }
                    //    else
                    //    {
                    //        chosenRow.CZ_DatVyr_Track = 0;
                    //        chosenRow.CZ_SW_Track = 0;
                    //    }

                    //}
                    //else
                    //{
                    //    MessageBoxBig.Show("Sejmutý kód nenalezen!");
                    //    return;
                    //}

                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListMNForm3SejmutyKodNenalezen);
                    return;
                }
                else if (VERows.Length == 1) //prave jeden, tak ho vybere a pokracuje ukoncenim dialogu
                {
                    chosenRow = VERows[0];
                    chosenRowIndex = vydejData.CZMST_SE.Rows.IndexOf(chosenRow);
                }
                else
                {
                    if (MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne)
                    {
                        /* vyhleda prvni nekompletni polozku s danym ITEMNMBR+SOPNUMBE+ORD,
                           pro niz neni jeste nasnimano vse. Pokud takovou nenajde, vrati
                           posledni strukturu s klicem ITEMNMBR+SOPNUMBE+ORD, ktera je v predloze */
                        foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow row in VERows)
                        {
                            //if (Vydej.VydejFormInstance.Nacteno(row.ITEMNMBR, row.SOPNUMBE, row.ORD)
                            //    < row.QTYSHPPD)
                            if (Vydej_3.ListPolozek3.Instance.Nacteno(row.ITEMNMBR, row.SOPNUMBE, row.ORD)
                                < row.QTYSHPPD)
                            {
                                chosenRow = row;
                                chosenRowIndex = vydejData.CZMST_SE.Rows.IndexOf(chosenRow);
                                break;
                            }
                            // zadny neplny nenasel, skoci teda na ten, ktery sejmul
                            chosenRow = VERows[0];
                            chosenRowIndex = vydejData.CZMST_SE.Rows.IndexOf(chosenRow);
                        }
                    }
                    else //nastavi jen filtr na nalezene a aktualizuje pohled ... ???
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListMNForm3NalezenoViceZaznamuNelzePokracovat, VERows.Length), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        // TODO : nastavit na filtr a aktualizovat pohled ...
                        return;
                    }
                }

                finalize();
                this.DialogResult = DialogResult.OK;
            }
            catch { }
            finally
            {
                if (MST_Global.OnScannerSound_Vydej_3)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }

        }
        private void updateForm()
        {

            index_l.Text = "-";
            ItemNmbr_l.Text = "-";
            ItemDesc_l.Text = "-";
            VNDITNUM_l.Text = "-";
            baleni_l.Text = "Balení: -";
            nacist_l.Text = "Naèíst: -";
			MJ_l.Text = "MJ: -";
            nacteno_l.Text = "Naèteno: -";
            snimatSN_l.Text = MST_Global.SNCode + ": -";
            snimatSW_l.Text = "-";
            snimatDV_l.Text = "-";
            davka_l.Text = "Dávka: -";
            sklad_l.Text = "Sklad: -";
            //1.Text = "Režim: -";
            sopnumbe_l.Text = "Obj.: -";
            CZCarKod_l.Text = "Lokace: -";
            nasklade_l.Text = "-";
            nasklade_l.ForeColor = Color.Black;
            datumnasklade_l.Text = "-";

            try
            {
                index_l.Text = "" + (chosenRowIndex + 1) + " / " + vydejData.CZMST_SE.Count;
                ItemNmbr_l.Text = chosenRow.ITEMNMBR;
                ItemDesc_l.Text = chosenRow.ITEMDESC;
                VNDITNUM_l.Text = "È.k. :" + (chosenRow.IsVNDITNUMNull() ? string.Empty : chosenRow.VNDITNUM);
                baleni_l.Text = "Balení: " + chosenRow.QTYPACK.ToString(Settings.UIFormatDesCisel);
                baleni_l.Visible = (chosenRow.QTYPACK > 0);
                nacist_l.Text = "Naèíst: " + chosenRow.QTYSHPPD.ToString(Settings.UIFormatDesCisel);
				MJ_l.Text = "MJ: " + (chosenRow.IsMJNull() ? "-" : chosenRow.MJ);

                decimal nactenokusu = Vydej_3.ListPolozek3.Instance.Nacteno(chosenRow.ITEMNMBR, chosenRow.SOPNUMBE, chosenRow.ORD);
                nacteno_l.Text = "Naèteno: " + nactenokusu.ToString(Settings.UIFormatDesCisel);
                nacteno_l.ForeColor = (nactenokusu == chosenRow.QTYSHPPD) ? Color.Green : SystemColors.ControlText;

                //snimatSN_l.Text = "Snímat " + Program.vydejForm.SNCode + ": ";
                snimatSN_l.Text = MST_Global.SNCode + ": ";
                if (chosenRow.CZ_SerNum_Track == 0)
                    snimatSN_l.Text += "NE";
                else if (chosenRow.CZ_SerNum_Track == 1)
                    snimatSN_l.Text += "ANO";
                else
                    snimatSN_l.Text = "Šarže";
                //snimatSW_l.Text = "Snímat " + Program.vydejForm.SWCode + ": " +
                snimatSW_l.Text = MST_Global.SWCode + ": " +
                    (chosenRow.CZ_SW_Track == 1 ? "ANO" : "NE");
                //snimatDV_l.Text = "Snímat " + Program.vydejForm.DVCode + ": " +
                snimatDV_l.Text = MST_Global.DVCode + ": " +
                    (chosenRow.CZ_DatVyr_Track == 1 ? "ANO" : "NE");

                davka_l.Text = "Dávka: " + chosenRow.CountEntries.ToString();
                sklad_l.Text = MST_Global.LC_NAME + ": " + chosenRow.LOCNCODE;
                //rezim_l.Text = "Režim: " + (rezimListovani == VydejForm.RezimListovani.Vsechno ? "Všechno" : "Neúp./Pøep.");
                //rezim_l.Text = "Režim: " + (rezimListovani == VydejForm.RezimListovani.Vsechno ? "Vše" : "Nedokonèené");
                sopnumbe_l.Text = "Obj.: " + chosenRow.SOPNUMBE.Trim();
                CZCarKod_l.Text = "CZ è.k.: " + chosenRow.CZ_CarKod.Trim(); //VNDITNUM je tam schvalne !!! nechapu proc>???

                nasklade_l.Text = "";
                nasklade_l.ForeColor = Color.Black;
                datumnasklade_l.Text = "";
            }
            catch
            {
            }

            //CHECKITEMSTATE
            try
            {
                Schema.Sklad.SkladMnozstviRow skladmnrow = nasklade.SkladMnozstvi.FindByITEMNMBRLocation(chosenRow.ITEMNMBR, chosenRow.LOCNCODE);
                nasklade_l.Text = "Kusù: " + skladmnrow.QTY.ToString(Settings.UIFormatDesCisel);
                datumnasklade_l.Text = skladmnrow.LastDownloadSuccess.ToString("g");
                if (skladmnrow.LastDownloadSuccess < skladmnrow.LastDownloadTry)
                {
                    nasklade_l.ForeColor = Color.Red;
                }
                else
                {
                    nasklade_l.ForeColor = Color.Blue;
                }
            }
            catch { }
        }
        ///// <summary>
        ///// Najde nejblizsi polozky pri listovani v rezimu Nedokoncene
        ///// </summary>
        ///// <param name="actualIndex">Aktualni index (vyhledava vcetne nej!)</param>
        ///// <param name="smer">Smer listovani</param>
        ///// <returns>-1, pokud nenajde, jinak hodnotu indexu</returns>
        //private int najdiNejblizsi(int actualIndex, LeftRight smer)
        //{
        //    VydejService.Vydej.CZMST_SERow row = vydejData.CZMST_SE[actualIndex];
        //    decimal Quantity = VydejForm.VydejFormInstance.Nacteno(row.ITEMNMBR, row.SOPNUMBE, row.ORD);
        //    while (row.QTYSHPPD == Quantity)
        //    {
        //        if (smer == LeftRight.Left)
        //        {
        //            if (actualIndex == 0)
        //                return -1;
        //            actualIndex--;
        //        }
        //        else
        //        {
        //            if (actualIndex == vydejData.CZMST_SE.Rows.Count - 1)
        //                return -1;
        //            actualIndex++;
        //        }
        //        row = vydejData.CZMST_SE[actualIndex];
        //        Quantity = VydejForm.VydejFormInstance.Nacteno(row.ITEMNMBR, row.SOPNUMBE, row.ORD);
        //    }
        //    return actualIndex;
        //}
        private void performUpKeyPress()
        {
            chosenRowIndex = 0;
            chosenRow = vydejData.CZMST_SE[chosenRowIndex];
            //if (rezimListovani == VydejForm.RezimListovani.Nedokoncene)
            //{
            //    int newIndex;
            //    if ((newIndex = najdiNejblizsi(chosenRowIndex, LeftRight.Right)) != -1)
            //        chosenRowIndex = newIndex;
            //    else
            //    {
            //        MessageBoxBig.Show("Seznam je prázdný!");
            //        rezimListovani = VydejForm.RezimListovani.Vsechno;
            //    }
            //}
        }
        private void performEnterPress(bool end)
        {
            _input_mode = Fask.MST_W.Classes.InputModeChecker.setInputMode(Fask.MST_W.Classes.InputModeChecker._input_modes.INPUT_ENTER);

            if (vydejData.Parametry[0].CONFIG_SNIMEJ_PRI_LISTU)
            {// nalistovanou polozku potvrdi sejmutim kodu
                ScannerStop();

                try
                {
                    using (SejmiKodForm sejmiMNForm = new SejmiKodForm(
                        string.Format(Fask.Localization.Localization.Vydej3ListMNForm3PotvrdPolozkuSejmutimKodu, MST_Global.MNCode),
                        SejmiKodForm.TypeOfCode.AlphaNumeric,
                        0,
                        vydejData.Parametry[0].CONFIG_KONT_DELKA,
                        false
                        ))
                    {

                        if (sejmiMNForm.ShowDialog() == DialogResult.Cancel)
                            return;

                        if (sejmiMNForm.Kod != vydejData.CZMST_SE[chosenRowIndex].CZ_CarKod.Trim())
                        {
                            MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListMNForm3KodSeNeshoduje);
                            return;
                        }
                    }

                }
                finally
                {
                    ScannerStart();
                }
            }
            else if (MST_Global.VydejPolozkyVyberJenScannerem)
            {
                //Kontrola, zda je mozne zadavat i jinak nez scannerem
                if (!Fask.MST_W.Classes.InputModeChecker.checkInputMode(MST_Global.VydejPolozkyVyberJenScannerem, _input_mode))
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListMNForm3PolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return;
                }
            }

            if (end)
            {
                finalize();
                this.DialogResult = DialogResult.OK;
            }
        }
        private void checkitemstate(string itemnmbr, string location)
        {
            try
            {
                object parameters = (object)(new string[] { itemnmbr, location });
                Vydej.vydejInstance.globalObject.service_information.BeginMnozstviNaSklade_Itemnumber_Location(itemnmbr, location, new AsyncCallback(this.checkitemstateend), parameters);
            }
            catch
            {
            }
        }
        private void checkitemstateend(IAsyncResult ares)
        {
            string[] parameters = (string[])ares.AsyncState;
            try
            {
                float number = Vydej.vydejInstance.globalObject.service_information.EndMnozstviNaSklade_Itemnumber_Location(ares);
                this.BeginInvoke(new DelegateStringStringFloat(this.checkitemstateUI), new object[] { parameters[0], parameters[1], number });
            }
            catch
            {
                this.BeginInvoke(new DelegateStringStringFloat(this.checkitemstateUI), new object[] { parameters[0], parameters[1], float.NaN });
            }
        }
        private void checkitemstateUI(string itemnmbr, string location, float mnozstvi)
        {
            //CHECKITEMSTATE
            DateTime dtnow = DateTime.Now;
            try
            {
                //float mnozstvi = vydejservice.MnozstviNaSklade_Itemnumber_Location(itemnmbr, location);
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
            updateForm();
        }
        #endregion

        #region Events
        void timer_Tick(object sender, EventArgs e)
        {
            //CHECKITEMSTATE
            checkitemstate(chosenRow.ITEMNMBR, chosenRow.LOCNCODE);
            updateForm();
        }
        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            string kod = e.BarcodeData.Trim();
            _input_mode = Fask.MST_W.Classes.InputModeChecker.setInputMode(Fask.MST_W.Classes.InputModeChecker._input_modes.INPUT_SCANNER);
            if (kod != string.Empty)
                this.BeginInvoke(new DelegateString(dohledejPolozku), new object[] { kod });
        }
        private void hlavniMenu_but_Click(object sender, EventArgs e)
        {
            finalize();
            this.DialogResult = DialogResult.Cancel;
        }
        private void ListMNForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                chosenRowIndex = 0;
                chosenRow = vydejData.CZMST_SE[chosenRowIndex];
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                chosenRowIndex = vydejData.CZMST_SE.Count - 1;
                chosenRow = vydejData.CZMST_SE[chosenRowIndex];
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                if (chosenRowIndex < vydejData.CZMST_SE.Count - 1)
                {
                    chosenRowIndex++;
                    chosenRow = vydejData.CZMST_SE[chosenRowIndex];
                }
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                if (chosenRowIndex > 0)
                {
                    chosenRowIndex--;
                    chosenRow = vydejData.CZMST_SE[chosenRowIndex];
                }
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                performEnterPress(true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListMNForm3NavratDoMenuDotaz, Fask.Localization.Localization.Vydej3ListMNForm3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.Yes)
                {
                    finalize();
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else if (e.KeyCode == Keys.D0)
            {// umozni vlozit MN rucne
                string dohledejkod = string.Empty;
                try
                {
                    ScannerStop();

                    using (SejmiKodForm sejmiMNForm = new SejmiKodForm(
                        string.Format(Fask.Localization.Localization.Vydej3ListMNForm3SejmiCK, MST_Global.MNName),
                        SejmiKodForm.TypeOfCode.AlphaNumeric,
                        0,
                        false,
                        false))
                    {

                        if (sejmiMNForm.ShowDialog() == DialogResult.Cancel)
                            return;

                        dohledejkod = sejmiMNForm.Kod;
                    }
                }
                finally
                {
                    ScannerStart();
                }

                dohledejPolozku(dohledejkod);
            }
            else if (e.KeyCode == Keys.D8)
            {
                ScannerStop();
                using (Fask.MST_W.Vydej_3.Detail detail = new Fask.MST_W.Vydej_3.Detail(chosenRow.SOPNUMBE))
                {
                    detail.ShowDialog();
                }
                ScannerStart();
            }
            else if (e.KeyCode == Keys.D9)
            {
                //CHECKITEMSTATE
                checkitemstate(chosenRow.ITEMNMBR, chosenRow.LOCNCODE);
            }
            else
            {
                return;
            }

            e.Handled = true;
            updateForm();
        }

        private void ListMNForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            ScannerStart();
            this.updateForm();
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

        private void ListMNForm_Closing(object sender, CancelEventArgs e)
        {
            finalize();
        }

        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            ScannerFinalize();
            if (timer != null)
            {
                timer.Enabled = false;
                timer = null;
            }
            if (nasklade != null)
            {
                nasklade.AcceptChanges();
                nasklade.WriteXml(Main.CiselnikSkladuFileName);
                nasklade = null;
            }
        }
        private void ok_but_Click(object sender, EventArgs e)
        {
            //upraveno na true
            performEnterPress(true);
        }
        #endregion

        private void ListMNForm3_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ListMNForm3_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

    }
}