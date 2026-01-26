using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Graphic;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.ServisModule
{
    public partial class ServisDavkyList : System.Windows.Forms.Form
    {
        private ServisModuleWService.ServisDavky davky = null;
        private DataView pohled = null;
        // Prijem_4.Globals.GenerovatNenalezenouPrijemku
        public bool GenerovatNenalezenouPrijemku = false;
        // Prijem_4.Globals.NezrealizovanePrijemky
        
        public ServisModuleWService.ServisDavky.HlavickyRow SelectedRow
        {
            get
            {
                try
                {
                    DataRowView dr = (DataRowView)hlavickyBindingSource.Current;
                    ServisModuleWService.ServisDavky.HlavickyRow irow = dr.Row as ServisModuleWService.ServisDavky.HlavickyRow;
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
                    ServisModuleWService.ServisDavky.HlavickyRow irow = SelectedRow;
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

         private string fileName = string.Empty;
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
                     ServisModuleWService.ServisDavky.HlavickyRow hrow = drv.Row as ServisModuleWService.ServisDavky.HlavickyRow;
                     if (hrow == null)
                         return string.Empty;

                     string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_ServisI);

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

        public ServisDavkyList(ServisModuleWService.ServisDavky davky)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                InitializeComponent();

                this.KeyPreview = true;
                this.davky = davky;
                this.updateForm();

                InitializeGridDynamicColumns(davky);
                MyInitializeGrid();

                this.menuItemStornovat.Enabled = this.menuItemGenerovat.Enabled;
                //this.menuItemUzavrit.Enabled = this.menuItemStornovat.Enabled;

                // prepnuti na numerickou klavesnici
                MST_W.Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, this.Text);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        public ServisDavkyList(ServisModuleWService.ServisDavky davky, bool generovatDataPrikazu):this(davky)
        {
            this.menuItemGenerovat.Enabled = generovatDataPrikazu;
            this.menuItemStornovat.Enabled = generovatDataPrikazu;
        }

        private void InitializeGridDynamicColumns(ServisModuleWService.ServisDavky davky)
        {            
            foreach (DataColumn dcol in davky.Hlavicky.Columns)
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
                //int index = hlavickyBindingSource.Find(this.davky.Hlavicky.DOCUMENT_NUMBERColumn.ColumnName, kod);
                int index = hlavickyBindingSource.Find(this.davky.Hlavicky.BarcodeColumn.ColumnName, kod);
                if (index < 0)
                {
                    MessageBoxBig.Show(String.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListDavkaNenalezena, kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
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
                if (MST_Global.OnScannerSound_ServisModul)
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
            //else if (e.KeyCode == Keys.D5)
            //{
            //    if (MST_Global.OnlineObjednavkaDetailPovolit)
            //        DetailObjednavka();
            //}
            else if (e.KeyCode == Keys.D6)
            { // TODO : doplnit do konfigurace modulu prijem ...
                if(this.menuItemGenerovat.Enabled)
                    GenerovatDataPrikazu();
            }
            else if (e.KeyCode == Keys.D7)
            {
                if (this.menuItemStornovat.Enabled)
                    DavkaStorno();
            }
            //else if (e.KeyCode == Keys.D8)
            //{
            //    if (this.menuItemUzavrit.Enabled)
            //        ServiskaUzavrit();
            //}
            //else if (e.KeyCode == Keys.D9)
            //{
            //    if (NezrealizovanePrijemky)
            //        PrijemNezrealizovanePrijemky();
            //}
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

            //if (!this.menuItem2.Enabled) //nejsme ve stahovani davek
            //    pohled.RowFilter = "Sloucena = 'False'";

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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            GenerovatDataPrikazu();
        }


        private void GenerovatDataPrikazu()
        {
            GenerovatDataPrikazu(null, false);
        }

        /// <summary>
        /// Vygenerovani prijemky a nasledne stazeni do terminalu.
        /// </summary>
        /// <param name="docnumber">Cislo objednavky (pokud je vyplneno, tak se nezobrazuje inputbox s pozadavkem na zadani</param>
        /// <param name="disableRowFilter">Po pridani vygenerovaneho prikazu dojde k vypnuti filtru v pohledu, aby se mohl dany zaznam najit (jinak by byl vyfiltrovany a nesel zvolit)</param>
        private void GenerovatDataPrikazu(string docnumber, bool disableRowFilter)
        {
            try
            {
                ScannerStop();

                string val = string.Empty;

                // TODO: konfiguracne umoznit generovani dat pomoci doc number (resp. zadani, ...)
                // pokud je vyplneno cislo objednavky, tak se jiz nemusi zadavat
                //if (string.IsNullOrEmpty(ponumber))
                //{
                //    if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteCisloPrikazu, val, out val, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.Cancel)
                //        return;
                //}
                //else 
                //    val = ponumber;
                // TODO: vyber odberatele a vyber okruhu

                // vyber odberatele
                Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
                if (MST_Global.ServisVyberOdberatelPovolit)
                {
                    using (Fask.MST_W.Forms.FormOdberateleVyber po = new FormOdberateleVyber())
                    {
                        if (po.ShowDialog() == DialogResult.Cancel)
                            return;

                        odberatel = po.Odberatel;

                        if (odberatel == null)
                        {
                            Logging.Log.Write("Není vybrán odbìratel, pøestože je vyžadován!");
                            return;
                        }
                    }
                }

                // TODO: konfiguracne
                // vyber okruhu
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow okruh = null;
                if (MST_Global.ServisVyberOkruhPovolit)
                {
                    using (Fask.MST_W.ServisModule.ServisOkruhList po = new ServisOkruhList(odberatel))
                    {
                        if (po.ShowDialog() == DialogResult.Cancel)
                            return;

                        okruh = po.Okruh;

                        if (okruh == null)
                        {
                            Logging.Log.Write("Není vybrán okruh, pøestože je vyžadován!");
                            return;
                        }
                    }
                }

                _WebRefernces_Globals.ServisModuleWServiceSession pservice = new _WebRefernces_Globals.ServisModuleWServiceSession();
                pservice.Url = MST_Global.ServerAddress + "Servis.asmx";
                pservice.Timeout = MST_Global.ServiceTimeOut;
                pservice.UpdateWebServiceCredentials();

                int? countentries = null;
                try
                {
                    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemDavkyListGenerujiDataDavky);

                    int result = pservice.GenerateServiska(MST_Global.TerminalID, MST_Global.UserID, val, odberatel != null ? odberatel.odb_id : string.Empty, okruh != null ? okruh.ID : string.Empty);
                    if (result > 0)
                        countentries = result;
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
                                statusinfo = "Neznámý status";
                                break;
                        }
                        throw new Exception(statusinfo + " : " + val.Trim());
                    }
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return;
                }

                Program.mstw.mbw.EndPracujiForm();

                try
                {
                    ServisModuleWService.ServisDavky.HlavickyRow hrow = davky.Hlavicky.NewHlavickyRow();

                    Globals.globalObject.Davka = countentries;
                    hrow.DOCUMENT_NUMBER = val.Trim();
                    
                    hrow.CountEntries = countentries.HasValue ? countentries.Value.ToString() : null;
                    if (odberatel != null)
                        hrow.ODB_ID = odberatel.odb_id;

                    if (okruh != null)
                        hrow.OkruhID = okruh.ID;
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
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Generování dávky", MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            DavkaStorno();
        }

        private void DavkaStorno()
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

                _WebRefernces_Globals.ServisModuleWServiceSession pservice = new _WebRefernces_Globals.ServisModuleWServiceSession();
                pservice.Url = MST_Global.ServerAddress + "Servis.asmx";
                pservice.Timeout = MST_Global.ServiceTimeOut;
                pservice.UpdateWebServiceCredentials();

                string pswd = string.Empty;

                if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemDavkyListZadejteHeslo, pswd, out pswd) != DialogResult.OK)
                    return;

                ServisModuleWService.StatusObject so = pservice.StornoServiska(MST_Global.TerminalID, MST_Global.UserID, SelectedRow.CountEntries, pswd);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    MessageBoxBig.Show("Dávka byla úspìšnì stornována.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    ServisModuleWService.ServisDavky.HlavickyRow[] hrows = (ServisModuleWService.ServisDavky.HlavickyRow[])this.davky.Hlavicky.Select("CountEntries='" + SelectedRow.CountEntries + "'");
                    this.davky.Hlavicky.RemoveHlavickyRow(hrows[0]);

                    updateForm();
                }
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemDavkyListChybaStornovaniPrijemky, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, "Storno dávky", MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                ScannerStart();
            }
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

        ///// <summary>
        ///// Kontrola, zdali davka jiz nebyla stazena. 
        ///// Prochazi veskere lokalni databaze (prijemky) a hleda v nich ponumber.
        ///// </summary>
        ///// <param name="ponumber">Hledany doklad.</param>
        ///// <param name="davka">out parametr. Pokud davka nalezena, vraci cislo davky.</param>
        ///// <returns>True, pokud davka jiz existuje, jinak false.</returns>
        //private bool DavkaExistuje(string ponumber, out string davka)
        //{
        //    davka = string.Empty;

        //    try
        //    {
        //        string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_ServisI);
        //        if (fileNames.Length >= 0)
        //        {
        //            Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter pe_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();

                    

        //            // projiti vsech prijemek
        //            for (int x = 0; x < fileNames.Length; x++)
        //            {
        //                //pi_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, davka + "." + Main.PrijemIExtData);                        
        //                pe_ta.Connection.ConnectionString = "Data source=" + fileNames[x];

        //                int? count = pe_ta.CountQueryPonumber(ponumber);
        //                if (count.HasValue && count.Value > 0)
        //                {
        //                    davka = System.IO.Path.GetFileNameWithoutExtension(fileNames[x]);
        //                    return true;
        //                }
        //                //if (vydejky.Hlavicky[i].CountEntries.ToString() == davkaf)
        //                //{
        //                //    vydejky.Hlavicky[i].Delete();
        //                //    break;
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
        //        // chyba, vraci se true
        //        return true;
        //    }
        //    return false;
        //}

    }
}