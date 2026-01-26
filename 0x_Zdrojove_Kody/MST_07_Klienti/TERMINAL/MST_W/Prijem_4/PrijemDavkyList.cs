using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.Graphic;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemDavkyList : System.Windows.Forms.Form
    {
        private PrijemService.PrijemDavky davky = null;
        private DataView pohled = null;
        // Prijem_4.Globals.GenerovatNenalezenouPrijemku
        public bool GenerovatNenalezenouPrijemku = false;
        // Prijem_4.Globals.NezrealizovanePrijemky
        private bool NezrealizovanePrijemky = false;

        public PrijemService.PrijemDavky.HlavickyRow SelectedRow
        {
            get
            {
                try
                {
                    DataRowView dr = (DataRowView)hlavickyBindingSource.Current;
                    PrijemService.PrijemDavky.HlavickyRow irow = dr.Row as PrijemService.PrijemDavky.HlavickyRow;
                    return irow;
                }
                catch
                {
                    return null;
                }
            }
        }
 
        public string Davka
        {
            get
            {
                try
                {
                    PrijemService.PrijemDavky.HlavickyRow irow = SelectedRow;
                    if (irow != null)
                        return irow.CountEntries;
                    else
                        return string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

         //private string fileName = string.Empty;
        /// <summary>
        /// Vrati jmeno souboru se zvolenou davkou
        /// </summary>
         public string FileName
         {
             get
             {
                 //return fileName;
                 try
                 {
                     CurrencyManager cm = (CurrencyManager)dgI1.BindingContext[dgI1.DataSource];
                     DataRowView drv = cm.Current as DataRowView;
                     PrijemService.PrijemDavky.HlavickyRow hrow = drv.Row as PrijemService.PrijemDavky.HlavickyRow;
                     if (hrow == null)
                         return string.Empty;

                     string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);

                     foreach (string file in fileNames)
                     {
                         string davkaf = System.IO.Path.GetFileNameWithoutExtension(file);
                         if (davkaf == hrow.CountEntries.ToString())
                             return file;
                     }
                     return string.Empty;
                 }
                 catch
                 {
                     return string.Empty;
                 }

             }

         }

        public PrijemDavkyList(PrijemService.PrijemDavky prijemky, bool generovatDataPrikazu)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();      

            this.KeyPreview = true;
            this.davky = prijemky;
            this.updateForm();

            InitializeGridDynamicColumns(prijemky);
            MyInitializeGrid();

            this.menuItemObjednavkaDetail.Enabled = MST_Global.OnlineObjednavkaDetailPovolit;
            this.menuItem2.Enabled = generovatDataPrikazu;
            this.menuItem3.Enabled = this.menuItem2.Enabled;
            this.menuItemUzavrit.Enabled = this.menuItem3.Enabled;
            
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prijemky"></param>
        /// <param name="generovatDataPrikazu"></param>
        /// <param name="generovatNenalezenouPrijemku">Pokud prijemka neni nalezena pomoci caroveho kodu, probehne pokus o stazeni</param>
        /// <param name="nezrealizovanePrijemky">Urceni, zdali se ma povolit zobrazeni seznamu nezrealizovanych prijemek</param>
        public PrijemDavkyList(PrijemService.PrijemDavky prijemky, bool generovatDataPrikazu, bool generovatNenalezenouPrijemku, bool NezrealizovanePrijemky)
            : this(prijemky, generovatDataPrikazu)
        {
            this.GenerovatNenalezenouPrijemku = generovatNenalezenouPrijemku;
            this.menuItemNezrealizovanePrij.Enabled = NezrealizovanePrijemky;
            this.NezrealizovanePrijemky = NezrealizovanePrijemky;
        }

        private void InitializeGridDynamicColumns(PrijemService.PrijemDavky prijemky)
        {            
            foreach (DataColumn dcol in prijemky.Hlavicky.Columns)
            {
                if (!this.dataGridTableStyle1.GridColumnStyles.Contains(dcol.ColumnName))
                {
                    DataGrid2TextBoxColumn du = new DataGrid2TextBoxColumn();
                    du.MappingName = dcol.ColumnName;
                    du.HeaderText = dcol.ColumnName;
                    du.NullText = "-";
                    du.Width = 45;
                    du.Grid = this.dgI1;
                    this.dataGridTableStyle1.GridColumnStyles.Add(du);
                }
            }
        }

        //Odsraneno, protoze je jiz nepouzivane ... 
        //public PrijemDavkyList(string[] filenames)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    InitializeComponent();
        //    MyInitializeGrid();
        //    this.KeyPreview = true;
        //    this.davky = new Fask.MST_W.PrijemService.PrijemDavky();
        //    foreach (string filename in filenames)
        //    {
        //        try
        //        {
        //            int davka = Convert.ToInt32(System.IO.Path.GetFileNameWithoutExtension(filename));
        //            PrijemService.PrijemDavky.HlavickyRow hr = davky.Hlavicky.NewHlavickyRow();
        //            hr.CountEntries = davka;
        //            davky.Hlavicky.AddHlavickyRow(hr);
        //        }
        //        catch { }
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        #region Scanner
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
        delegate void UpdateUIDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        void UpdateUI(Fask.ScannerProvider.ScannerEventArgs e)
        {
            try
            {
                string kod = e.BarcodeData.Trim();
                int index = hlavickyBindingSource.Find(this.davky.Hlavicky.PONUMBERColumn.ColumnName, kod);
                if (index < 0)
                {
                    // ma probehnout generovani prijemky, pokud neni nalezena v seznamu
                    if (GenerovatNenalezenouPrijemku)
                    {
                        // pokud davka existuje, neni mozne pokracovat ...
                        string davka;
                        if (DavkaExistuje(kod, out davka))
                        {
                            MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaJizBylaStazena, kod, davka), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            return;
                        }

                        DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaNenalezenaVygenerovat, kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.No)
                            return;

                        // generovani davky
                        GenerovatDataPrikazu(kod, true);
                        return;
                    }
                    else
                    {
                        MessageBoxBig.Show(String.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaNenalezena, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                }
                dgI1.CurrentRowIndex = index;
                PerformOK();
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                if (MST_Global.OnScannerSound_Prijem_4)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new UpdateUIDelegate(UpdateUI), new object[] { e });
        }
        #endregion

        private void MyInitializeGrid()
        {
            this.dgI1.InitializeTableGridColumnStyles(Settings.UIFormatDesCisel);
            this.dgI1.Font = new Font(this.dgI1.Font.Name, Settings.UIGridFont, this.dgI1.Font.Style);
            this.dgI1.Load(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
        }

        private void PrijemDavkyList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.D5)
            {
                if (MST_Global.OnlineObjednavkaDetailPovolit)
                    DetailObjednavka();
            }
            else if (e.KeyCode == Keys.D6)
            { // TODO : doplnit do konfigurace modulu prijem ...
                if(this.menuItem2.Enabled)
                    GenerovatDataPrikazu();
            }
            else if (e.KeyCode == Keys.D7)
            {
                if (this.menuItem3.Enabled)
                    PrijemkaStorno();
            }
            else if (e.KeyCode == Keys.D8)
            {
                if (this.menuItemUzavrit.Enabled)
                    PrijemkaUzavrit();
            }
            else if (e.KeyCode == Keys.D9)
            {
                if (NezrealizovanePrijemky)
                    PrijemNezrealizovanePrijemky();
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

        private void PrijemDavkyList_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            updateForm();
            this.dgI1.CurrentRowIndex = 0;

            this.ScannerStart();
        }

        private void updateForm()
        {
            pohled = new DataView();
            pohled.Table = davky.Hlavicky;

            if (!this.menuItem2.Enabled) //nejsme ve stahovani davek
                pohled.RowFilter = "Sloucena = 'False'";

            hlavickyBindingSource.DataSource = pohled;

            //hlavickyBindingSource.DataSource = davky;
            //hlavickyBindingSource.DataMember = davky.Hlavicky.TableName;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PerformOK()
        {
            if (SelectedRow != null)
            {
                finalize();
                DialogResult = DialogResult.OK;
            }
        }

        private void finalize()
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;

            this.dgI1.Save(Path.Combine(Main.ConfigDir, this.GetType().ToString()));
            this.ScannerFinalize();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width / 2 - 1, panel1.Height);
            buttonStorno.Size = nsize;
            //buttonOK.Size = nsize;
        }

        private void menuItemObjednavkaDetail_Click(object sender, EventArgs e)
        {
            DetailObjednavka();
        }

        private void DetailObjednavka()
        {
            PrijemService.PrijemDavky.HlavickyRow hrow = null;
            try
            {
                this.ScannerStop();
                hrow = (this.hlavickyBindingSource.Current as DataRowView).Row as PrijemService.PrijemDavky.HlavickyRow;
                using (Detail detail = new Detail(hrow.PONUMBER.Trim(), hrow.IsSKL_IDNull() ? string.Empty : hrow.SKL_ID.Trim()))
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            GenerovatDataPrikazu();
        }


        private void GenerovatDataPrikazu()
        {
            GenerovatDataPrikazu(null, false);
        }

        private string generovatdataprikazuposlednizadane = string.Empty;
        /// <summary>
        /// Vygenerovani prijemky a nasledne stazeni do terminalu.
        /// </summary>
        /// <param name="ponumber">Cislo objednavky (pokud je vyplneno, tak se nezobrazuje inputbox s pozadavkem na zadani</param>
        /// <param name="disableRowFilter">Po pridani vygenerovaneho prikazu dojde k vypnuti filtru v pohledu, aby se mohl dany zaznam najit (jinak by byl vyfiltrovany a nesel zvolit)</param>
        private void GenerovatDataPrikazu(string ponumber, bool disableRowFilter)
        {
            try
            {
                ScannerStop();

                string val = string.Empty;

                // pokud je vyplneno cislo objednavky, tak se jiz nemusi zadavat
                if (string.IsNullOrEmpty(ponumber))
                {
                    if (!String.IsNullOrEmpty(generovatdataprikazuposlednizadane))
                        val = generovatdataprikazuposlednizadane;
                    if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteCisloPrikazu, val, out val, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.Cancel)
                        return;
                    generovatdataprikazuposlednizadane = val.Trim();
                }
                else 
                    val = ponumber;

                string countentries = "";
                try
                {
                    // TODO : cisla skladu ... !!!
                    string sklad = string.Empty;
                    if (Prijem_4.Globals.GenerovaniPrikazuZadatSklad)
                    {
                        if (Prijem_4.Globals.SkladPouzit)
                        {
                            if (!String.IsNullOrEmpty(Prijem_4.Globals.SkladID))
                            {
                                sklad = Prijem_4.Globals.SkladID;
                            }
                            else if (Prijem_4.PrijemMain.prijemInstance.globalObject.sklad != null)
                            {
                                sklad = Prijem_4.PrijemMain.prijemInstance.globalObject.sklad.skl_id.Trim();
                            }
                            else
                            {
                                Logging.Log.Write("Je vyzadovan nastaveny sklad, ale neni nastaven", "Prijem.GenrovatDataPrikazu");
                                throw new Exception(Fask.Localization.Localization.Prijem4PrijemDavkyListSkladNenastaven);
                            }
                        }
                        else
                        {
                            if (DialogResult.Cancel == InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListCisloSkladu, sklad, out sklad, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric))
                                return;
                        }
                    }


                    // 19.4.2016 JiS => Dlouhotrvajici zpracovani generovani dat davky                    
                    #region Generovani nove davky serverem a cekani na vysledek

                    #region puvodni kod generovani
                    //int result = pservice.GenerateDavka(val.Trim(), sklad);
                    #endregion

                    //1) zjistit aktualni status generovani ...
                    Fask.MST_W.PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.GenerateDavkaStatus(val.Trim(), sklad);
                    if (so == null)
                    { // status neexistuje => 1. volani, tak pokracuje...
                    }
                    else if (so.Exception)
                    { // nastala vyjimka pri priprave
                        // => zobrazit informaci o vyjimce a dotaz, zda znovu generovat ANO / NE
                        // => NE: return
                        // => ANO: smazat status na serveru a pokracovat
                        DialogResult dlgResStatusEx = MessageBoxBig.Show(
                            so.StatusText +
                            "\n" + "Generovat dávku dokladu '" + val.Trim() + "' znovu?",
                            "Pøíjem - generování dávky",
                            MessageBoxButtons.YesNo,
                            MessageBoxBigIcon.Warning);
                        if (dlgResStatusEx == DialogResult.No)
                            return;
                        else
                            so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.GenerateDavkaStatusDelete(val.Trim(), sklad);
                    }
                    else if (so.Finished)
                    {
                        // generovani dokladu jiz skoncilo
                        // => zobrazit informaci o cisle vygenerovane davky a zda generovat znovu?
                        // => NE: return
                        // => ANO: smazat status na serveru a pokracovat
                        DialogResult dlgResStatusFin = MessageBoxBig.Show(
                            "Již generováno do dávky: '" + so.StatusText + "'" +
                            "\n" + "Generovat dávku dokladu '" + val.Trim() + "' znovu?",
                            "Pøíjem - generování dávky",
                            MessageBoxButtons.YesNo,
                            MessageBoxBigIcon.Warning);
                        if (dlgResStatusFin == DialogResult.No)
                            return;
                        else
                            so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.GenerateDavkaStatusDelete(val.Trim(), sklad);
                    }
                    else
                    {
                        // Gnerovani jeste probiha ...
                        // => pokracovat a cekat na dokonceni ...
                    }

                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Vydej3ListVydejekForm3GenerujiDataDavky);

                    so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.GenerateDavkaRequest(val.Trim(), sklad);
                    // cekat dokud nenastane vyjimka nebo neni dokonceno
                    while (!(so.Exception || so.Finished))
                    {
                        Program.mstw.mbw.Zprava = so.StatusText;
                        System.Threading.Thread.Sleep(1000); // 1sec nic nedelani ...
                        so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.GenerateDavkaStatus(val.Trim(), sklad);
                        if (so == null)
                            throw new Exception("Status generování dávky dokladu '" + val.Trim() + "' nenalezen!");
                    }

                    if (so.Exception)
                    {
                        throw new Exception(so.StatusText);
                    }

                    // pokud dojde az sem, tak je finished...
                    // zde se ocekava, ze v statustext bude cislo nove davky jako integer...
                    int result = int.Parse(so.StatusText);

                    if (result > 0)
                        countentries = result.ToString();
                    else
                    {
                        result = -result;
                        string statusinfo = string.Empty;
                        switch (result)
                        {
                            case 0: statusinfo = "OK"; break;
                            case 1: statusinfo = "Již existuje"; break;
                            case 2: statusinfo = "Neexistuje"; break;
                            case 3: statusinfo = "Bylo nahráno"; break;
                            default:
								statusinfo = "Neznámý status: '-" + result.ToString() + "'";
                                break;
                        }
                        throw new Exception(statusinfo + " : " + val.Trim());
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }
                finally
                {
                    Program.mstw.mbw.EndPracujiForm();
                }

                try
                {
                    PrijemService.PrijemDavky.HlavickyRow hrow = davky.Hlavicky.NewHlavickyRow();
                    hrow.CountEntries = countentries;
                    hrow.PONUMBER = val.Trim();
                    davky.Hlavicky.AddHlavickyRow(hrow);

                    if (disableRowFilter)
                        pohled.RowFilter = string.Empty;

                    //updateForm();
                    // start refresh

                    //DataView pohled = new DataView();
                    //pohled.Table = davky.Hlavicky;

                    //if (!this.menuItem2.Enabled) //nejsme ve stahovani davek
                    //    pohled.RowFilter = "Sloucena = 'False'";

                    //hlavickyBindingSource.DataSource = pohled;

                    // end 


                    //DataTable dt = pohled.ToTable(false, new string[] { _vydejky.Hlavicky.CountEntriesColumn.ColumnName });
                    //DataRow[] drows = dt.Select("CountEntries=" + countentries);
                    //if (drows.Length > 0)
                    //{
                    //    dataGrid1.CurrentRowIndex = dt.Rows.IndexOf(drows[0]);
                    //    this.PerformStahnout();
                    //}

                    //int index = hlavickyBindingSource.Find(this.davky.Hlavicky.PONUMBERColumn.ColumnName, val);
                    int index = hlavickyBindingSource.Find(this.davky.Hlavicky.CountEntriesColumn.ColumnName, countentries);
                    if (index < 0)
                    {
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaCisloNenalezena, countentries), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                    dgI1.CurrentRowIndex = index;
                    PerformOK();

                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                }

            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            PrijemkaStorno();
        }

        private void PrijemkaStorno()
        {
            try
            {
                ScannerStop();

                if (SelectedRow == null)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListNeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }

                if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListStornovatDavkuDotaz, SelectedRow.CountEntries), Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                string pswd = string.Empty;

                if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteHeslo, pswd, out pswd) != DialogResult.OK)
                    return;

                PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.StornoPrijemka(MST_Global.TerminalID, SelectedRow.CountEntries, pswd);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemkaUspesneStornovana, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    PrijemService.PrijemDavky.HlavickyRow[] hrows = (PrijemService.PrijemDavky.HlavickyRow[])this.davky.Hlavicky.Select("CountEntries='" + SelectedRow.CountEntries + "'");
                    this.davky.Hlavicky.RemoveHlavickyRow(hrows[0]);

                    updateForm();
                }
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListChybaStornovaniPrijemky, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Prijem4PrijemDavkyListStornoPrijemky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void PrijemkaUzavrit()
        {
            if (SelectedRow == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListNeniVybranaDavka, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            try
            {
                ScannerStop();

                if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListUzavritDavkuDotaz, SelectedRow.CountEntries), Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                string pswd = string.Empty;

                if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteHeslo, pswd, out pswd) != DialogResult.OK)
                    return;

                PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.FinishPrijemka(MST_Global.TerminalID, SelectedRow.CountEntries, pswd);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListPrijemkaUspesneUzavrena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    PrijemService.PrijemDavky.HlavickyRow[] hrows = (PrijemService.PrijemDavky.HlavickyRow[])this.davky.Hlavicky.Select("CountEntries='" + SelectedRow.CountEntries + "'");
                    this.davky.Hlavicky.RemoveHlavickyRow(hrows[0]);

                    updateForm();
                }
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListChybaUzavreniPrijemky, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Prijem4PrijemDavkyListUzavreniPrijemky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItemUzavrit_Click(object sender, EventArgs e)
        {
            PrijemkaUzavrit();
        }

        private void PrijemDavkyList_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void PrijemDavkyList_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void menuItemNezrealizovanePrij_Click(object sender, EventArgs e)
        {
            PrijemNezrealizovanePrijemky();
        }

        /// <summary>
        /// Kontrola, zdali davka jiz nebyla stazena. 
        /// Prochazi veskere lokalni databaze (prijemky) a hleda v nich ponumber.
        /// </summary>
        /// <param name="ponumber">Hledany doklad.</param>
        /// <param name="davka">out parametr. Pokud davka nalezena, vraci cislo davky.</param>
        /// <returns>True, pokud davka jiz existuje, jinak false.</returns>
        private bool DavkaExistuje(string ponumber, out string davka)
        {
            davka = string.Empty;

            try
            {
                string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);
                if (fileNames.Length >= 0)
                {
                    //SqlCEDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter pe_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();

                    // projiti vsech prijemek
                    //for (int x = 0; x < fileNames.Length; x++)
                    foreach (string f in fileNames)
                    {
                        //pi_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, davka + "." + Main.PrijemIExtData);                        
                        //pe_ta.Connection.ConnectionString = "Data source=" + fileNames[x];
                        using (var controller_prijem_davka = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(f))
                        {
                            int? count = Convert.ToInt32(controller_prijem_davka.CountQueryPonumber_PE(ponumber));
                            if (count.HasValue && count.Value > 0)
                            {
                                //davka = System.IO.Path.GetFileNameWithoutExtension(fileNames[x]);
                                davka = System.IO.Path.GetFileNameWithoutExtension(f);
                                return true;
                            }
                        }                    

                        //if (vydejky.Hlavicky[i].CountEntries.ToString() == davkaf)
                        //{
                        //    vydejky.Hlavicky[i].Delete();
                        //    break;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                // chyba, vraci se true
                return true;
            }
            return false;
        }

        private void PrijemNezrealizovanePrijemky()
        {
            try
            {
                ScannerStop();
                //Fask.MST_W.PrijemService.Obecne ds = OnlineGetDoporuceneLokace();

                string ponumber;

                //if (ds != null)
                //{
                    //if (ds.Prijemky.Count > 0)
                    //{
                        using (PrijemNezrealizovanePrijemkyList pnp = new PrijemNezrealizovanePrijemkyList())
                        {
                            //sif.ImageFilename = sn.Trim() + "_" + pocet.ToString();
                            DialogResult dr = pnp.ShowDialog();
                            if (dr == DialogResult.Cancel)
                                return;

                            ponumber = pnp.ponumber;
                        }

                        if (!string.IsNullOrEmpty(ponumber))
                            GenerovatDataPrikazu(ponumber, true);

                   // }
                    //else
                    //{
                    //MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListNezrealPrijemkaNenalezena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                   // }
                //}
            }
            catch (Exception ex)
            {
                Logging.Log.WriteDebug(ex.Message, "Prijem_3.miZalokovat");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                ScannerStart();
            }
        }

    }
}