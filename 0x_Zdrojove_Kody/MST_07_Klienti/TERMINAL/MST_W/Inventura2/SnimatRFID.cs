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
namespace Fask.MST_W.Inventura2
{
    public partial class SnimatRFID : System.Windows.Forms.Form
    {
        #region inicializace
        /// <summary>
        /// 
        /// </summary>
        public SnimatRFID()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            MyInitializeGrid();
            Cursor.Current = Cursors.Default;
        }

        private void MyInitializeGrid()
        {
            this.dataGrid2Polozky.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dataGrid2Polozky.Font = new Font(this.dataGrid2Polozky.Font.Name, Settings.UIGridFont, this.dataGrid2Polozky.Font.Style);
            this.dataGrid2Polozky.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        /// <summary>
        /// Vola se pri load jen nastavi jmeno okna atd a veci pro data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnimatRFID_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //z list polozky
            this.Text += " " + MST_Global.Inventura2Name.Trim();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            this.ShowData(ShowDataType.Nalezene);

            this.dataGrid2Polozky.KeyScrollDown = MST_Global.DataGridScrollDown;
            this.dataGrid2Polozky.KeyScrollUp = MST_Global.DataGridScrollUp;
            this.dataGrid2Polozky.Focus();

			//if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Closed)
			//{
			//    Globals.active_connection.Open();
			//}
            this.buttonRFIDONOFF_Click(null, null);
            this.ScannerStart();

            //Nastaveni default sortu podle casu nacteni od nejnovejsiho po nejstarsi...
            //pokud ovsem neni nastaven jiny uzivatelsky sort...
            //tedy pouze pri 1.inicializaci ...
            if (this.dataGrid2Polozky.Sort == string.Empty)
            {
                this.dataGrid2Polozky.Sort = "CASNACTENO desc";
            }

            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Pomocne veci-dialog pro naplneni polozky, vybrana polozka

        //Dataset, ktery obsahuje natazena data z databaze (minimum)
        private Fask.SQLiteDBs.DataSets.Inventura2 _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();

        //Hashtable nasnimaneKody = new Hashtable();
        System.Collections.Generic.List<string> nasnimaneKody = new List<string>();

        //formular pro naplneni polozky

        private NaplnPolozku _naplnPolozkuForm = null;
        private NaplnPolozku NaplnPolozkuForm
        {
            get
            {
                if (_naplnPolozkuForm == null)
                    _naplnPolozkuForm = new NaplnPolozku();
                if (_naplnPolozkuForm.IsDisposed)
                    _naplnPolozkuForm = new NaplnPolozku();
                return _naplnPolozkuForm;
            }
        }

        /// <summary>
        /// Vrati vybrarou polozku ze seznamu pokud neni tak null
        /// </summary>
        public ListPolozkyDS.PolozkyRow VybranaPolozka
        {
            get
            {
                try
                {
                    return (polozkyBindingSource.Current as DataRowView).Row as ListPolozkyDS.PolozkyRow;
                }
                catch
                {
                    return null;
                }
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
                PerformKonec(true);
            }//enter
            else if (e.KeyCode == Keys.Enter)
            {
                NelezniPolozkuANapln(this.VybranaPolozka); // najde ji a vyplni
            }
            else if (e.KeyCode == Keys.Back)
            {
                SmazRadekZTabulky(this.VybranaPolozka);
            }
            else if (e.KeyCode == Keys.F1)
            {
                buttonRFIDONOFF_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                menuItemZpracujVse_Click(null, null);
            }
            else
                return;
            //jak dojde sem tak se neco pouzilo z podminek krom posledni
            e.Handled = true;

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
            UpdateStatusBar();

            return true;
        }

        /// <summary>
        /// Pokusi se nalest polozku pro dany kod
        /// </summary>
        private void NajdiPolozkuCarovyKod(string ck)
        {
            try
            {//najde pomoci car kodu a prida
                DataRow[] rows = listPolozkyDS.Polozky.Select("EAN='" + ck.Trim() + "'");
                if (rows.Length == 0)
                {
                    Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
                    if (NajdiPolozku(ck, out mrow))
                        PridejDoTabulky(mrow);
                }
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
        private bool NajdiPolozku(
            string carkod,
            out Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        {
            mrow = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // ScannerStop();
                //string crkd = this.PrekladDebud(carkod);
                //1) najit polozky
                Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByEAN_Majetek(carkod);

                if (dt_majetek.Count == 0) //nenalezeno
                {
                    Cursor.Current = Cursors.Default;
                    ListPolozkyDS.PolozkyNenalezeneRow pnrow = listPolozkyDS.PolozkyNenalezene.FindByEAN(carkod);
                    if (pnrow == null)
                    {
                        Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chyba.wav");
                        listPolozkyDS.PolozkyNenalezene.AddPolozkyNenalezeneRow(carkod);
                        textBoxN.Text = (new StringBuilder(textBoxN.Text)).AppendLine(carkod).ToString();
                    }
                    return false;
                }
                else if (dt_majetek.Count > 1) //nalezeno vice zaznamu
                {
                    Cursor.Current = Cursors.Default;
                    // TODO : co s vice nalezenymi variantami? => nasledna akce
                    ListPolozkyDS.PolozkyNalezeneVicekratRow pnv = listPolozkyDS.PolozkyNalezeneVicekrat.FindByEAN(carkod);
                    if (pnv == null)
                    {
                        Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chyba.wav");
                        listPolozkyDS.PolozkyNalezeneVicekrat.AddPolozkyNalezeneVicekratRow(carkod);
                        textBoxV.Text = (new StringBuilder(textBoxV.Text)).AppendLine(carkod).ToString();
                    }
                    return false;
                }
                else //je pouze jedna (0 byt uz nemuze)
                {
                    mrow = dt_majetek[0];
                }

                return true;
            }//jinak chyti vyjimku
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Logging.Log.Write(ex.Message, this.Text);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

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
        private void PridejDoTabulky(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        {
            //Test na dokoncenost polozky.
            //Pridavaji se do seznamu pouze polozky, ktere jeste nejsou kompletne nasnimany.
            //Pokud naleznu polozku, ktera neni kompletne nasnimana, pak jeji c.k. pridam do seznamu jiz nasnimanych...
            if (mrow.KUSU - mrow.NACTENO <= 0) //Nic jiz nezbyva, tak nepridavam, ale hodim do seznamu jiz nasnimanych c.k.
            {
                ListPolozkyDS.PolozkyNalezeneNasnimaneRow pnnrow = listPolozkyDS.PolozkyNalezeneNasnimane.FindByEAN(mrow.EAN.Trim());
                if (pnnrow == null)
                {
                    Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "chimes.wav");
                    listPolozkyDS.PolozkyNalezeneNasnimane.AddPolozkyNalezeneNasnimaneRow(mrow.EAN.Trim());
                    textBoxS.Text = (new StringBuilder(textBoxS.Text)).AppendLine(mrow.EAN.Trim()).ToString();
                }
                return;
            }

            //novy radek
            Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow newRow = listPolozkyDS.Polozky.NewPolozkyRow();
            //pridavani jednotlivych veci do noveho radku postupne
            #region Pridavani do radku pro tabulky co zobrazuje naskenovane polozky
            try
            {
                newRow.I_CISLO = (string)mrow.I_CISLO;
            }
            catch (Exception)
            {
                newRow.SetI_CISLONull();
            }

            try
            {
                newRow.NAZEV = (string)mrow.NAZEV;
            }
            catch (Exception)
            {

                newRow.SetNAZEVNull();
            }
            try
            {
                newRow.ID = (int)mrow.ID;
            }
            catch (Exception)
            {
            }

            try
            {
                newRow.KATEGORIE = (string)mrow.KATEGORIE;
            }
            catch (Exception)
            {
                newRow.SetKATEGORIENull();
            }


            try
            {
                newRow.STRED = (string)mrow.STRED;
            }
            catch (Exception)
            {
                newRow.SetSTREDNull();
            }

            try
            {
                newRow.OSOBA = (int)mrow.OSOBA;
            }
            catch (Exception)
            {
                newRow.SetOSOBANull();
            }

            try
            {
                newRow.LOKACE1 = (string)mrow.LOKACE1;
            }
            catch (Exception)
            {
                newRow.SetLOKACE1Null();
            }
            try
            {
                newRow.LOKACE2 = (string)mrow.LOKACE2;
            }
            catch (Exception)
            {
                newRow.SetLOKACE2Null();
            }

            try
            {
                newRow.KANCELAR = (string)mrow.KANCELAR;
            }
            catch (Exception)
            {
                newRow.SetKANCELARNull();
            }

            try
            {
                newRow.EAN = (string)mrow.EAN;
            }
            catch (Exception)
            {
                newRow.SetEANNull();
            }
            //bere e z global hodnoty - nastavu je se propridavani polozek
            //newRow.KUSU = pocetnactenichTagu;//da tam kolik je jich nasnimanych
            //newRow.NALEZENO = pocetnactenichTagu;

            if (!mrow.IsKUSUNull())
                newRow.KUSU = mrow.KUSU;

            try
            {
                newRow.KLIC_LOK = (int)mrow.KLIC_LOK;
            }
            catch (Exception)
            {
                newRow.SetKLIC_LOKNull();
            }

            try
            {
                newRow.NACTENO = (decimal)mrow.NACTENO;
            }
            catch (Exception)
            {
                newRow.NACTENO = 0;
            }

            try
            {
                newRow.CASNACTENO = DateTime.Now;
            }
            catch (Exception)
            {
                newRow.SetCASNACTENONull();
            }

            //ted jeste ty dalsi =====================
            try
            {
				Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt_stredisko = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByStredisko_Stredisko(newRow.STRED);
                if (dt_stredisko.Count > 0)
                    newRow.STRED_NAZEV = dt_stredisko[0].NAZEV.Trim();
                else
                    newRow.SetSTRED_NAZEVNull();
            }
            catch //(Exception ex)
            {
                newRow.SetSTRED_NAZEVNull();
            }

            try
            {
				Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt_osoby = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByOSOBA_ZODP_Osoby(newRow.OSOBA);
                if (dt_osoby.Count > 0)
                    newRow.OSOBA_NAZEV = dt_osoby[0].JMENO.Trim() + " " + dt_osoby[0].PRIJMENI.Trim();
                else
                    newRow.SetOSOBA_NAZEVNull();
            }
            catch //(Exception ex)
            {
                newRow.SetOSOBA_NAZEVNull();
            }


            try
            {
				Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt_kancl = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKANCL_Kancl(newRow.KANCELAR);
                if (dt_kancl.Count > 0)
                    newRow.KANCELAR_NAZEV = dt_kancl[0].TEXT.Trim();
                else
                    newRow.SetKANCELAR_NAZEVNull();
            }
            catch //(Exception ex)
            {
                newRow.SetKANCELAR_NAZEVNull();
            }

            try
            {
				Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt_lokace = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByKLIC_LOK_Lokace(newRow.KLIC_LOK);
                if (dt_lokace.Count > 0)
                    newRow.LOKACE_NAZEV = dt_lokace[0].NAZEV.Trim();
                else
                    newRow.SetLOKACE_NAZEVNull();
            }
            catch //(Exception ex)
            {
                newRow.SetLOKACE_NAZEVNull();
            }

            #endregion
            //pridam ji
            listPolozkyDS.Polozky.AddPolozkyRow(newRow);
            Fask.MST_W.MySystem.Audio.PlaySound(Main.SoundDir + "info.wav");
            //Nastavit kursor na vlozenou polozku ... ??? Nebude to moc pomale pri vetsich mnozstvich???
            dataGrid2Polozky.UnSelectAll(); //nejdriv to musim vsechno odznacit, protoze to nezvlada automaticky posunout jiz oznacene ...
            dataGrid2Polozky.CurrentRow = newRow;
            
        }
        #endregion

        #region Zpracovani polozek a odstraneni z tabulky
        /// <summary>
        /// aktivuje zpracování všech položek 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemZpracujVse_Click(object sender, EventArgs e)
        {
            if (RFIDScannerActivationTestStopQuestion())
            {
                return;
            }

            // TODO : proverit algoritmus ukladani dat ... a zpracovani chyb(rozdilu...)

            //I use the more elegant “black list”:
            //List remList = new List();
            //foreach (TestClass tc in list)
            //    if (hasToBeDeleted) remList.Add(tc);
            //ListPolozkyDS polozky = listPolozkyDS.Clone();//udelam kopii
            List<ListPolozkyDS.PolozkyRow> rowForDelete = new List<ListPolozkyDS.PolozkyRow>();

            try
            {
                ScannerStop();

                //vytvorim si formular - stejne jako v listpolozky
                NaplnPolozku naplnp = this.NaplnPolozkuForm;
                naplnp.SetDefaultValues();
                naplnp.Owner = this;
                naplnp.Popis = "Množství";
                naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                naplnp.AllowEmpty = false;
                naplnp.Text = "Množství";
                //naplnp.ScannerOff = true;//nebdu uz pouzivat scanner => je pouzito nize
                naplnp.Kod = "1";//dam tam kusy 
				if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
					naplnp.ScannerOff = !Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_MnozstviScannerem;

                foreach (ListPolozkyDS.PolozkyRow prow in listPolozkyDS.Polozky.Rows)
                {
					Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable table = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByID_Majetek(prow.ID);  // mrow
                    Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = table[0];
                    naplnp.SetDefaultValues();
                    naplnp.Popis = "Množství";
                    naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                    naplnp.AllowEmpty = false;
                    naplnp.Text = "Množství";
                    naplnp.MajetekRow = mrow;
                    naplnp.Kod = "1";
					if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
						naplnp.ScannerOff = !Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_MnozstviScannerem;

                    if (!naplnp.PerformTest()) //pokud neni v porádku
                    {
                        MessageBoxBigTimeout.Show("Položka '" +mrow.NAZEV.Trim() + "' není v pořádku.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        
                        #region Post kontroly zadanych hodnot
                        //if (naplnp.ShowDialog() == DialogResult.Cancel)
                        //    return;

                        //Kontroly zadavani hodnot ...
                        while (true)
                        {
                            if (naplnp.ShowDialog() == DialogResult.Cancel)
                            {
								if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                                {
                                    if (mrow.NACTENO < mrow.KUSU)
                                    {
                                        if (MessageBoxBig.Show("Není kompletní.\nChcete skončit zadávání této položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                            continue;
                                    }
                                }
                                return;
                            }

							if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                            {
                                if (mrow.NACTENO + Convert.ToDecimal(naplnp.Kod) > mrow.KUSU)
                                {
                                    if (MessageBoxBig.Show("Zadané množství je větší než má být načteno, chcete pokračovat a data uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                        continue;
                                }
                            }

							if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
                            {
								if (!Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
                                {
                                    MessageBoxBig.Show("Zadané množství musí být kladné a větší jak 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                    continue;
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(naplnp.Kod) == 0)
                                {
                                    MessageBoxBig.Show("Zadané množství musí být různé od 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                    continue;
                                }
                            }

                            break; //dostane-li se až sem, tak je vše ok ...
                        }
                        #endregion
                    }

                    try
                    {
                        //mnozsvti co bylo zadano
                        decimal mnozstvi = Convert.ToDecimal(naplnp.Kod);//da se jedna
                        Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow newlokace = naplnp.Lokace;
                        Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow newkancl = naplnp.Kancelar;
                        Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow newosoba = naplnp.Osoba;
                        Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow newstredisko = naplnp.Stredisko;

                        #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

                        try
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            _inventura2.INVENTUR.Clear(); //
                            //_inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();
                            Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow newi2row = _inventura2.INVENTUR.NewINVENTURRow();
                            //dam mu nove id
							newi2row.ID = ((Inventura2.Inventura2_Instance.globalObject.controller_inventura2.MaxID_Inventur() ?? 0) + 1);
                            //pokud nejsou null
                            if (!mrow.IsKATEGORIENull()) newi2row.KATEGORIE = mrow.KATEGORIE;
                            if (!mrow.IsI_CISLONull()) newi2row.I_CISLO = mrow.I_CISLO;
                            if (!mrow.IsNAZEVNull()) newi2row.NAZEV = mrow.NAZEV;
                            //pokud bylo strdisko null
                            if (newstredisko == null) newi2row.SetSTREDNull();
                            else newi2row.STRED = newstredisko.STREDISKO;
                            //osoba null tak bude nul jinak se veme z osoby
                            if (newosoba == null) newi2row.SetOSOBANull();
                            else newi2row.OSOBA = newosoba.OSOBA_ZODP;
                            //pokud je lokace null
                            if (newlokace == null)
                            {
                                newi2row.SetLOKACE1Null();
                                newi2row.SetLOKACE2Null();
                                newi2row.SetKLIC_LOKNull();
                            }
                            else //neni null
                            {
                                newi2row.LOKACE1 = newlokace.LOKACE1;
                                newi2row.LOKACE2 = newlokace.LOKACE2;
                                newi2row.KLIC_LOK = newlokace.KLIC_LOK;
                            }
                            //pokud je kandl null
                            if (newkancl == null) newi2row.SetKANCELARNull();
                            else newi2row.KANCELAR = newkancl.KANCL;
                            //pokud je car kod tak se priradi  jinak se tam da mnoztvi
                            if (!mrow.IsEANNull()) newi2row.EAN = mrow.EAN;
                            newi2row.KUSU = mnozstvi;
                            //priradim tam jeste zbyle hodnoty
                            if (!mrow.IsID_INVNull()) newi2row.ID_INV = Convert.ToDecimal(mrow.ID_INV);
                            newi2row.OS_ZPR = MST_Global.UserID.ToString();
                            newi2row.ID_TERM = MST_Global.TerminalID;
                            newi2row.CAS_ZPR = DateTime.Now;
                            newi2row.ID_MAJETEK = mrow.ID;
                        #endregion

                            _inventura2.INVENTUR.AddINVENTURRow(newi2row);
							Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Update_Inventur(newi2row);//dam update 
                            rowForDelete.Add(prow);
                            //SmazRadekZTabulky(mrow);
                            UpdateNacteno(newi2row);
                            _inventura2.INVENTUR.Clear(); //asi neni potreba
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                        //=======================================
                        //list.RemoveAll(new Predicate(delegate(TestClass x) { return remList.Contains(x); }));
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
            finally
            {
                try
                {
                    //smazu ty radky
                    for (int i = 0; i < rowForDelete.Count; i++)
                    {
                        SmazRadekZTabulky(rowForDelete[i]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                }

                // TODO : oprava vypnuti scanneru pri nezobrazeni pomoci showdialog
                if (this._naplnPolozkuForm != null || !this._naplnPolozkuForm.IsDisposed)
                    this._naplnPolozkuForm.ScannerOff = true;

                ScannerStart();
            }
            kontrolaKonce();
        }

        private void kontrolaKonce()
        {
            if (listPolozkyDS.Polozky.Rows.Count < 1)
            {
                DialogResult dr = MessageBoxBig.Show("Přejete si znovu aktivovat snímání RFID?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.buttonRFIDONOFF_Click(this, null);
                }
                else if (dr == DialogResult.No)
                {
                    //menuItemKonec_Click(this, null);
                    PerformKonec(false);
                }
            }
        }

        private void menuItemZpracuj_Click(object sender, EventArgs e)
        {
            ZpracujOznacenouPolozku();
        }

        /// <summary>
        /// Zpracuje polozku oznacenou v tabulku
        /// Stejne jako v ListPolozky
        /// </summary>
        /// <returns></returns>
        public void ZpracujOznacenouPolozku()
        {
            NelezniPolozkuANapln(VybranaPolozka);
        }

        /// <summary>
        /// Nalezne polozku v db a radek preda dalsi funkci pro naplneni
        /// </summary>
        /// <param name="prow">radek tabulky co se bude vyplnovat</param>
        private void NelezniPolozkuANapln(ListPolozkyDS.PolozkyRow prow)
        {
            if (RFIDScannerActivationTestStopQuestion())
            {
                return;
            }

            try
            {
                ScannerStop();
                
                //pokud nic neni
                if (prow == null)
                {
                    MessageBoxBig.Show("Není vybrána položka!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                //podivam se do db
                Cursor.Current = Cursors.WaitCursor;
				Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt_majetek = Inventura2.Inventura2_Instance.globalObject.controller_inventura2.GetDataByID_Majetek(prow.ID);
                Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow = null;
                Cursor.Current = Cursors.Default;
                //nic nenasel
                if (dt_majetek.Count == 0)
                {
                    MessageBoxBig.Show("Nenalezen záznam v tabulce !", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return;
                }//je jich vic
                else if (dt_majetek.Count > 1)
                {
                    MessageBoxBig.Show("Nalezeno více řádků se stejným ID !!!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                else//jedna
                {
                    mrow = dt_majetek[0]; //je tam jen jedna polozka a je na indexu 0
                }

                //jinak je to 1:1 a muzu to pustit dal
                //ted ji bue vyplnovat (zadavat do ni hodnoty
                VyplnPolozku(mrow);

            }
            finally //da ho na spravnykurzor
            {
                ScannerStart();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Vyplneni polozky
        /// </summary>
        /// <param name="mrow">Radek co se bude vyplnovat</param>
        private void VyplnPolozku(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        {
            try
            {
                ScannerStop();

                //vytvorim si formular - stejne jako v listpolozky
                NaplnPolozku naplnp = this.NaplnPolozkuForm;
                naplnp.SetDefaultValues();
                naplnp.MajetekRow = mrow;
                naplnp.Owner = this;
                naplnp.Popis = "Množství";
                naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
                naplnp.AllowEmpty = false;
                naplnp.Text = "Vložte množství";
                naplnp.ScannerOff = true;//nebdu uz pouzivat scanner
                naplnp.Kod = "1"; //mrow.KUSU.ToString();//dam tam kusy 
                //if (Globals.active_parametry.CFG_PredvyplnitMnozstvi)
                //{
                //    if (Globals.active_parametry.CFG_PredvyplnitMnozstviOJedna)
                //        naplnp.Kod = "1";
                //    else if (Globals.active_parametry.CFG_PredvyplnitMnozstviZbyvajici)
                //    {
                //        //decimal nasnimano = Globals.ta_inventur.NasnimanoKusu(mrow.I_CISLO) ?? 0;
                //        //decimal zbyva = nasnimano - mrow.KUSU;
                //        decimal zbyva = mrow.ZBYVA;
                //        naplnp.Kod = (zbyva > 0 ? zbyva : 0).ToString(Settings.UIFormatDesCisel);
                //    }
                //}

                
                #region Post kontroly zadanych hodnot
                //if (naplnp.ShowDialog() == DialogResult.Cancel)
                //    return;

                //Kontroly zadavani hodnot ...
                while (true)
                {
                    if (naplnp.ShowDialog() == DialogResult.Cancel)
                    {
						if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                        {
                            if (mrow.NACTENO < mrow.KUSU)
                            {
                                if (MessageBoxBig.Show("Není kompletní.\nChcete skončit zadávání této položky?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                    continue;
                            }
                        }
                        return;
                    }

					if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null && Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_KontrolaUplnostiPolozky)
                    {
                        if (mrow.NACTENO + Convert.ToDecimal(naplnp.Kod) > mrow.KUSU)
                        {
                            if (MessageBoxBig.Show("Zadané množství je větší než má být načteno, chcete pokračovat a data uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                                continue;
                        }
                    }

					if (Inventura2.Inventura2_Instance.globalObject.active_parametry != null)
                    {
						if (!Inventura2.Inventura2_Instance.globalObject.active_parametry.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
                        {
                            MessageBoxBig.Show("Zadané množství musí být kladné a větší jak 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            continue;
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(naplnp.Kod) == 0)
                        {
                            MessageBoxBig.Show("Zadané množství musí být různé od 0!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            continue;
                        }
                    }

                    break; //dostane-li se až sem, tak je vše ok ...
                }
                #endregion

                //mnozsvti co bylo zadano
                decimal mnozstvi = Convert.ToDecimal(naplnp.Kod);
                Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow newlokace = naplnp.Lokace;
                Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow newkancl = naplnp.Kancelar;
                Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow newosoba = naplnp.Osoba;
                Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow newstredisko = naplnp.Stredisko;

                #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    _inventura2.INVENTUR.Clear(); //asi neni potreba
                    _inventura2 = new Fask.SQLiteDBs.DataSets.Inventura2();
                    Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow newi2row = _inventura2.INVENTUR.NewINVENTURRow();
                    //dam mu nove id
					newi2row.ID = ((Inventura2.Inventura2_Instance.globalObject.controller_inventura2.MaxID_Inventur() ?? 0) + 1);
                    //pokud nejsou null
                    if (!mrow.IsKATEGORIENull()) newi2row.KATEGORIE = mrow.KATEGORIE;
                    if (!mrow.IsI_CISLONull()) newi2row.I_CISLO = mrow.I_CISLO;
                    if (!mrow.IsNAZEVNull()) newi2row.NAZEV = mrow.NAZEV;
                    //pokud bylo strdisko null
                    if (newstredisko == null) newi2row.SetSTREDNull();
                    else newi2row.STRED = newstredisko.STREDISKO;
                    //osoba null tak bude nul jinak se veme z osoby
                    if (newosoba == null) newi2row.SetOSOBANull();
                    else newi2row.OSOBA = newosoba.OSOBA_ZODP;
                    //pokud je lokace null
                    if (newlokace == null)
                    {
                        newi2row.SetLOKACE1Null();
                        newi2row.SetLOKACE2Null();
                        newi2row.SetKLIC_LOKNull();
                    }
                    else //neni null
                    {
                        newi2row.LOKACE1 = newlokace.LOKACE1;
                        newi2row.LOKACE2 = newlokace.LOKACE2;
                        newi2row.KLIC_LOK = newlokace.KLIC_LOK;
                    }
                    //pokud je kandl null
                    if (newkancl == null) newi2row.SetKANCELARNull();
                    else newi2row.KANCELAR = newkancl.KANCL;
                    //pokud je car kod tak se priradi  jinak se tam da mnoztvi
                    if (!mrow.IsEANNull()) newi2row.EAN = mrow.EAN;
                    newi2row.KUSU = mnozstvi;
                    //priradim tam jeste zbyle hodnoty
                    if (!mrow.IsID_INVNull()) newi2row.ID_INV = Convert.ToDecimal(mrow.ID_INV);
                    newi2row.OS_ZPR = MST_Global.UserID.ToString();
                    newi2row.ID_TERM = MST_Global.TerminalID;
                    newi2row.CAS_ZPR = DateTime.Now;
                    newi2row.ID_MAJETEK = mrow.ID;

                    _inventura2.INVENTUR.AddINVENTURRow(newi2row);
					Inventura2.Inventura2_Instance.globalObject.controller_inventura2.Update_Inventur(newi2row);//dam update 
                    SmazRadekZTabulky(mrow);
                    UpdateNacteno(newi2row);

                    _inventura2.INVENTUR.Clear(); //asi neni potreba
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
            finally
            {
                ScannerStart();
            }
        }

        /// <summary>
        /// Zmena nacteno v tabulce majetek
        /// </summary>
        /// <param name="i2row">radek co se menil v inventure</param>
        private void UpdateNacteno(Fask.SQLiteDBs.DataSets.Inventura2.INVENTURRow i2row)
        {            
            //Globals.ta_majetek.UpdateNactenoByI_CISLO(i2row.KUSU, i2row.I_CISLO);
            //Globals.ta_majetek.UpdateNactenoByZAZNAM(i2row.KUSU, i2row.I_CISLO, i2row.KATEGORIE);
			Inventura2.Inventura2_Instance.globalObject.controller_inventura2.UpdateNactenoByID_Majetek(i2row.KUSU, i2row.ID_MAJETEK);
        }

        /// <summary>
        /// Odstrani radek z tabulky Id jsou stejna
        /// </summary>
        /// <param name="mrow">radek co se bude mazat</param>
        private void SmazRadekZTabulky(Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKRow mrow)
        {
            // listPolozkyDS.Polozky.
            //.ID
            try
            {
                Fask.MST_W.Inventura2.ListPolozkyDS.PolozkyRow rowForDelete = listPolozkyDS.Polozky.FindByID(mrow.ID);
                listPolozkyDS.Polozky.RemovePolozkyRow(rowForDelete);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        /// <summary>
        /// smaze radek z tabulky
        /// </summary>
        /// <param name="prow">radek</param>
        private void SmazRadekZTabulky(ListPolozkyDS.PolozkyRow prow)
        {
            //pokud je null tk nic 
            if (prow == null)
                return;
            //smaze
            try
            {
                listPolozkyDS.Polozky.RemovePolozkyRow(prow);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
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
                    this.UpdateStatusBar();
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
                    this.UpdateStatusBar();
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
            
            UpdateRFIDUIState();
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
                Program.mstw.RFIDUHFScanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                Program.mstw.RFIDUHFScanner.DataReady += new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                Program.mstw.RFIDUHFScanner.StartScan();
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
                Program.mstw.RFIDUHFScanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventRFIDHandler(RFIDScanner_DataReady);
                rfidactive = false;
            }
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
        private delegate void RFIDProcessDataDelegate(Fask.MST_W.Scanner.ScannerRFIDEventArgs e);
        void RFIDProcessData(Fask.MST_W.Scanner.ScannerRFIDEventArgs e)
        {
            try
            {
                //TODO ulozeni dat a pak jejich zpracování 
                foreach (Fask.MST_W.Scanner.RFIDBarcodeData data in e.BarcodeData)
                {
                    //textBoxRFIDScannerData.Text += DateTime.Now.ToString() + " " + data.BarcodeData + "\r\n";
                    //if (nasnimaneKody.ContainsKey(data.BarcodeData))
                    //{
                    //    nasnimaneKody[data.BarcodeData] = (int)nasnimaneKody[data.BarcodeData] + 1;
                    //}
                    //else
                    //{
                    //    nasnimaneKody[data.BarcodeData] = 1;
                    //}


                    if ((Settings.RemovedRFIDCodes != null) && (Settings.RemovedRFIDCodes.Contains(data.BarcodeData)))
                        continue; // tento kod je v seznamu odstranenych kodu

                    if (!nasnimaneKody.Contains(data.BarcodeData))
                    {
                        nasnimaneKody.Add(data.BarcodeData);
                    }
                }
                PridejNasnimanePolozky();
                //UpdateStatusBar();
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
        void RFIDScanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerRFIDEventArgs e)
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
            PerformKonec(true);//pokud chci otazku
        }

        /// <summary>
        /// Metoda pro ukonceni
        /// </summary>
        /// <param name="question"></param>
        private void PerformKonec(bool question)
        {

            //TODO Priat ze ma naskenovane polozky!!
            if (question && MessageBoxBig.Show("Opravdu ukončit snímání RFID?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return;
            
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            RFIDScannerStop();
            ScannerFinalize();
            if (_naplnPolozkuForm != null && !_naplnPolozkuForm.IsDisposed)
            {
                _naplnPolozkuForm.ScannerOff = true;
                _naplnPolozkuForm.Dispose();
                _naplnPolozkuForm = null;
            }
            //asi to nebudu uzavirat
            //if (Globals.active_connection != null && Globals.active_connection.State == ConnectionState.Open)
            //    Globals.active_connection.Close();
            this.dataGrid2Polozky.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
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
            SmazRadekZTabulky(VybranaPolozka);
        }

        private void UpdateStatusBar()
        {
            string status = string.Empty;
            status += "RFID: " + (rfidactive ? "A" : "N");
            status += ", N:" + listPolozkyDS.PolozkyNenalezene.Count;
            status += ", V:" + listPolozkyDS.PolozkyNalezeneVicekrat.Count;
            status += ", S:" + listPolozkyDS.PolozkyNalezeneNasnimane.Count;
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

        private enum ShowDataType
        {
            Nalezene,
            Nenalezene,
            Vice,
            Splnene
        }
        private void ShowData(ShowDataType sdt)
        {
            //StringBuilder sb = new StringBuilder();
            dataGrid2Polozky.Hide();
            textBoxN.Hide();
            textBoxS.Hide();
            textBoxV.Hide();
            switch (sdt)
            {
                case ShowDataType.Nenalezene:
                    //foreach (ListPolozkyDS.PolozkyNenalezeneRow pn in listPolozkyDS.PolozkyNenalezene)
                    //{
                    //    sb.AppendLine(pn.EAN.Trim());
                    //}
                    //textBoxN.Text = sb.ToString();
                    textBoxN.Show();
                    textBoxN.Dock = DockStyle.Fill;
                    break;
                case ShowDataType.Vice:
                    //foreach (ListPolozkyDS.PolozkyNalezeneVicekratRow pnv in listPolozkyDS.PolozkyNalezeneVicekrat)
                    //{
                    //    sb.AppendLine(pnv.EAN.Trim());
                    //}
                    //textBoxV.Text = sb.ToString();
                    textBoxV.Show();
                    textBoxV.Dock = DockStyle.Fill;
                    break;
                case ShowDataType.Splnene:
                    //foreach (ListPolozkyDS.PolozkyNalezeneNasnimaneRow pns in listPolozkyDS.PolozkyNalezeneNasnimane)
                    //{
                    //    sb.AppendLine(pns.EAN.Trim());
                    //}
                    //textBoxS.Text = sb.ToString();
                    textBoxS.Show();
                    textBoxS.Dock = DockStyle.Fill;
                    break;
                case ShowDataType.Nalezene:
                default:
                    dataGrid2Polozky.Show();
                    dataGrid2Polozky.Dock = DockStyle.Fill;
                    break;
            }
        }

        private void menuItemNalezene_Click(object sender, EventArgs e)
        {
            ShowData(ShowDataType.Nalezene);
        }

        private void menuItemNenalezene_Click(object sender, EventArgs e)
        {
            ShowData(ShowDataType.Nenalezene);
        }

        private void menuItemVice_Click(object sender, EventArgs e)
        {
            ShowData(ShowDataType.Vice);
        }

        private void menuItemSplnene_Click(object sender, EventArgs e)
        {
            ShowData(ShowDataType.Splnene);
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
                    if (!nasnimaneKody.Contains(ck))
                    {
                        nasnimaneKody.Add(ck);
                    }
                }
                PridejNasnimanePolozky();
                //UpdateStatusBar();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Inventura2)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
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
        }

    }
}