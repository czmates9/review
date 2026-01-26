using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.MST_WINDOWS.ModuleIfc;

using FASK.MST_WINDOWS.ErrorLog;
using System.IO;
using System.Threading;
//using FASK.MST_WINDOWS.Module.Vyroba_Agro.Classes;
//using Vyroba_Agro;
//using FASK.MST_WINDOWS.ModuleIfc;
//using SQLCECommLib.DSVyrobaSQLCETableAdapters;
using System.Reflection;
//using FASK.MST_WINDOWS.Module.ZZS.Forms;
//using Fask.Emailing;


namespace FASK.MST_WINDOWS.Module.ZZS
{
    public partial class frmMainZZS : Form, IModuleConnector
    {



        private int _cislodavky = 0;
        public int CisloDavky
        {
            get { return _cislodavky; }
            set { _cislodavky = value; }
        }

        public const string ProdejOExt = "di";

        RFID_Data rfdi_data;

        public frmMainZZS()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            FlexibleMessageBox.FONT = new Font("Tahoma", 30, FontStyle.Regular);
        }


        #region IModuleConnector Members

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }


        public void ReturnPortsToPreviousState()
        {
            //throw new NotImplementedException();
        }

        public void ClosePorts()
        {
            if (rfdi_data != null)
                rfdi_data.ClosePorts();
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;
            try
            {
                

                if (rfdi_data == null)
                    return true;

                if (rfdi_data.IsReadyToClose(out message))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            //if (dataVyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.LogIDSmena)
            //{
            //    message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
            //    return false;
            //}
            //else
            //{
            //    dataVyroba.SaveActualDataLogScreen();

            //}
        }

        #endregion

        #region Resize

        private void panelRight_Resize(object sender, EventArgs e)
        {
            button3.Height = panelRight.Height / 2;
            button4.Visible = false;
            //button4.Height = panelRight.Height / 2;
        }

        private void panelLeft_Resize(object sender, EventArgs e)
        {
            button1.Height = panelLeft.Height / 2;
            button2.Visible = false;
            //button2.Height = panelLeft.Height / 2;
        }

        private void frmMainZZS_Resize(object sender, EventArgs e)
        {
            // sirka, vyska
            panelLeft.Size = new Size(this.Width / 2, this.Height);
            menuStrip1.Visible = false;

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            SpracujPohyb(TypPohybu.Predani_dodavateli_sluzeb_prani, button1.Text);
        }

        void rfdi_data_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            //throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SpracujPohyb(TypPohybu.Prevzeti_od_dodavatele, button3.Text);
        }

        private void SpracujPohyb(TypPohybu typ, string typName)
        {
            try
            {

                //string configFilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIDataTable dtDI = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Prodej.CZMST_DIDataTable();

                string davkaprodej = string.Empty;
                string dstFile = string.Empty;

                //string CiselnikSkladyDB = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikSkladyDB);
                //string CiselnikTypDokladuDB = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikTypDokladuDB);

                //string CiselnikZboziDB = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikKatalogZboziDB);
                //string CiselnikOdberateleDB = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikOdberateleDB);
                //string CiselnikPracovniciDB = Path.Combine(configFilePath, @"SQLCEDB\" + Properties.Settings.Default.CiselnikPracovniciDB);


                string SkladID = "";
                string di_sklid = null;
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092Row typdokladu;

                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092DataTable dt_typDokladu = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladu.CZMST092DataTable();
                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter ta_092 = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter();
                ta_092.Connection.ConnectionString = "Data source=" + MyPath.TypDokladuDirectory;

                string predani = Properties.Settings.Default.Predani_dodavateli_sluzeb_prani;
                string prevzeti = Properties.Settings.Default.Prevzeti_od_dodavatele;

                if (!File.Exists(MyPath.SkladyDirectory) || !File.Exists(MyPath.TypDokladuDirectory) || !File.Exists(MyPath.ZboziDirectory) || !File.Exists(MyPath.OdberateleDirectory) || !File.Exists(MyPath.PracovniciDirectory))
                {
                    FlexibleMessageBox.Show(this, "Je potřebné aktualizovat číselníky!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    return;
                }

                switch (typ)
                {
                    case TypPohybu.Predani_dodavateli_sluzeb_prani:
                        ta_092.FillByKey1(dt_typDokladu, predani);
                        break;
                    case TypPohybu.Prevzeti_od_dodavatele:
                        ta_092.FillByKey1(dt_typDokladu, prevzeti);
                        break;
                    default:
                        break;
                }



                if (dt_typDokladu == null || dt_typDokladu.Count == 0)
                {
                    //Davka nenalezena
                    //File.Delete(dstFile);

                    FlexibleMessageBox.Show(this, "Nenalezené typy dokladu. Je potřebné aktualizovat číselník typu dokladu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    
                    return;
                        //{
                    //    AktualizovatCiselniky();

                    //    SpracujPohyb(typ, typName);
                    //    return;
                    //}

                   //return;

                }
                if (dt_typDokladu.Count > 1)
                {
                    //error vickrat definovany typ dokladu
                    //File.Delete(dstFile);
                    FlexibleMessageBox.Show(this,"Chyba, jeden doklad definován vícekrát.", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    return;
                }





                typdokladu = dt_typDokladu[0];


                try
                {
                    #region Generovani Davky


                    string[] fileNames = Directory.GetFiles(MyPath.SQLCEDBSDirectory, "*." + ProdejOExt);

                    if (fileNames.Count() != 0)
                    {
                        davkaprodej = Path.GetFileNameWithoutExtension(fileNames[0]);


                        #region ZZS k nicemu  1.Existujici zaznam z DI

                        FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter di_ta = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
                        di_ta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.SQLCEDBSDirectory, davkaprodej.ToString() + "." + ProdejOExt);
                        System.Data.SqlServerCe.SqlCeCommand scecommand = null;
                        string di_docid = null;
                        string di_docid2 = null;
                        //string di_odbid = null;
                        //string di_sklid = null;
                        //string di_strid = null;
                        //string di_sklid_dest = null;
                        //string di_serltnum = null;
                        try
                        {
                            scecommand = di_ta.Connection.CreateCommand();
                            scecommand.CommandText = "Select * from czmst_di";
                            scecommand.Connection.Open();
                            System.Data.SqlServerCe.SqlCeDataReader sdr = scecommand.ExecuteReader();

                            if (sdr.Read())
                            {
                                di_docid = Convert.ToString(sdr["doc_id"] is System.DBNull ? string.Empty : sdr["doc_id"]).Trim();
                                di_docid2 = Convert.ToString(sdr["doc_id2"] is System.DBNull ? string.Empty : sdr["doc_id2"]).Trim();
                                //di_odbid = Convert.ToString(sdr["odb_id"] is System.DBNull ? string.Empty : sdr["odb_id"]).Trim();
                                //di_sklid = Convert.ToString(sdr["skl_id"] is System.DBNull ? string.Empty : sdr["skl_id"]).Trim();
                                //di_strid = Convert.ToString(sdr["str_id"] is System.DBNull ? string.Empty : sdr["str_id"]).Trim();
                                //di_sklid_dest = Convert.ToString(sdr["skl_id_dest"] is System.DBNull ? string.Empty : sdr["skl_id_dest"]).Trim();
                                // 6.6.2016 PeV: neprebira se, mohlo by byt vice sarzi a pak by byl zmatek, ktera je zvolena ...
                                //di_serltnum = Convert.ToString(sdr["serltnum"] is System.DBNull ? string.Empty : sdr["serltnum"]).Trim();
                            }

                            if ((typdokladu.doc_id.Trim() == di_docid.Trim()) && (typdokladu.doc_id2.Trim() == di_docid2.Trim())) 
                            {
                                di_ta.Fill(dtDI);
                            }
                            else
                            {
                                FlexibleMessageBox.Show(this, "Davka nesedí s typem dokladu.", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Log.Write(ex);
                        }
                        finally
                        {
                            if (scecommand != null && scecommand.Connection.State == ConnectionState.Open)
                                scecommand.Connection.Close();
                        }
                        #endregion


                    }
                    else
                    {
                        GetNewDavka( ref davkaprodej, ref dstFile);
                    }

                    #endregion

                    #region Nacteni Dat podle konkretneho typu dokladu



                    // dotaz na aktualizaci ciselniku zbozi
                    //if (AktualizaceZboziPredVyberemDavky)
                    //{
                    //if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceCiselnikuZboziDotaz, MST_Global.ProdejName, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    //== DialogResult.Yes)
                    //    stahnoutZbozi();
                    //}

                    //TaD neni potrebne po kazden kliku na tlacitko je nova davka
                    //using (ProdejVyberDavky pvd = new ProdejVyberDavky())
                    //{
                    //    pvd.HesloProOtevreniRozpracovaneDavky = PozadovatHesloProOtevreniRozpracovaneDavky;
                    //    if (pvd.ShowDialog() == DialogResult.Cancel)
                    //        return;

                    //    this.cislodavky = pvd.CisloDavky;
                    //}



                    //Napevno pro kazde  tlacitko bude jeden typ dokladu zvoleny
                    //Dohledat podle nazvu zadaneho pro dane tla4itko







                    //if (TypDokladu)
                    //{
                    //    using (ProdejVyberTypuDokladu ptd = new ProdejVyberTypuDokladu(cislodavky))
                    //    {
                    //        if (ptd.ShowDialog() == DialogResult.Cancel)
                    //            return;
                    //        typdokladu = ptd.TypDokladu;
                    //        if (typdokladu == null)
                    //            return;
                    //    }
                    //}

                    // zadani sarze na davku
                    // zobrazit SN, pripadne vygenerovat?? ...

                    #region  ZZS nevyuziva seriove cisla
                    //if (typdokladu != null && !typdokladu.Iscfg_sn_na_davkuNull() && typdokladu.cfg_sn_na_davku > 0 && string.IsNullOrEmpty(di_serltnum))
                    //{
                    //    // vygenerovani, pokud je povoleno, jinak rucne zadat
                    //    if (!typdokladu.Iscfg_generovat_snNull() && typdokladu.cfg_generovat_sn > 0)
                    //    {
                    //        di_serltnum = generovatSN();
                    //    }

                    //    SejmiKodForm skf = new SejmiKodForm();
                    //    skf.Popis = "Šarže";
                    //    skf.Text = "Zadaní šarže";
                    //    skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                    //    //skf.MaxLength = Globals.LOCNCODE_LEN;
                    //    //skf.Len = Globals.LOCNCODE_LEN;
                    //    //skf.CheckLen = true;
                    //    skf.AllowEmpty = false;
                    //    skf.Kod = string.IsNullOrEmpty(di_serltnum) ? string.Empty : di_serltnum.Trim();

                    //    if (skf.ShowDialog() == DialogResult.Cancel)
                    //        return;

                    //    di_serltnum = skf.Kod;
                    //}

                    #endregion

                    #region ODBERATEL // Nepouziva se pro ZZS

                    //FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Odberatele.CZMST090Row odberatel = null;
                    //if ((typdokladu != null && typdokladu.cfg_odb > 0) || Odberatel)
                    //{
                    //    using (ProdejVyberOdberatele po = new ProdejVyberOdberatele(typdokladu, cislodavky))
                    //    {
                    //        if (po.ShowDialog() == DialogResult.Cancel)
                    //            return;

                    //        odberatel = po.Odberatel;

                    //        if (odberatel == null)
                    //        {
                    //            ErrorLog.Log.Write("Není vybrán odběratel, přestože je vyžadován!");
                    //            return;
                    //        }
                    //    }
                    //}

                    #endregion



                    //parametry hlavicky ...
                    string zakazkaID = string.Empty;
                    string paletaID = string.Empty;
                    string skladID = string.Empty;

                    #region MENY // Nepouziva se pro ZZS
                    //FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Meny.CZMST097Row mena = null;

                    //if (odberatel != null && !odberatel.Ismena_IDNull() && odberatel.mena_ID.Trim().Length > 0)
                    //{
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.MenyTableAdapters.CZMST097TableAdapter meny_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.MenyTableAdapters.CZMST097TableAdapter();
                    //    meny_ta.Connection.ConnectionString = "Data source=" + Main.CiselnikMenDB;
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Meny.CZMST097DataTable dt = meny_ta.GetDataByMenaID(odberatel.mena_ID);
                    //    if (dt.Count > 0)
                    //        mena = dt[0];
                    //    else
                    //    { // vychozi menu nastavit ... ???
                    //        //dt = meny_ta.GetData();
                    //        //var linqHlavniMena = dt.Where(m => m.mena_hlavni);
                    //        //if (linqHlavniMena.Count() > 0)
                    //        //{
                    //        //    mena = linqHlavniMena.First();
                    //        //}                        

                    //        //dt = meny_ta.GetDataByHlavni(true);
                    //        //if (dt.Count > 0)
                    //        //{
                    //        //    mena = dt[0]; //nastavena hlavni mena ...
                    //        //}
                    //    }

                    //    // Update DIH ...
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                    //    _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(StorageDir, cislodavky.ToString() + "." + ProdejOExt);
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();

                    //    if (dih_dt.Count > 0)
                    //    {
                    //        zakazkaID = dih_dt[0].Zakazka_ID;
                    //        paletaID = dih_dt[0].Paleta_ID;
                    //        skladID = dih_dt[0].SKL_ID;
                    //        _dih_ta.DeleteQuery();
                    //    }
                    //    _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);

                    //}

                    //if (typdokladu != null && !typdokladu.Iscfg_mena_idNull() && typdokladu.cfg_mena_id > 0 && mena == null)
                    //{
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                    //    _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(StorageDir, cislodavky.ToString() + "." + ProdejOExt);
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();

                    //    using (ProdejVyberMeny po = new ProdejVyberMeny())
                    //    {
                    //        if (dih_dt.Count > 0)
                    //            po.SelectedMenaID = dih_dt[0].mena_ID;

                    //        if (po.ShowDialog() == DialogResult.Cancel)
                    //            return;

                    //        if (po.bezCiziMeny)
                    //            mena = null;
                    //        else
                    //            mena = po.SelectedMena;

                    //        //if (mena == null)
                    //        //{
                    //        //    ErrorLog.Log.Write("Není vybrána měna, přestože je vyžadována!");
                    //        //    return;
                    //        //}
                    //    }

                    //    if (dih_dt.Count > 0)
                    //    {
                    //        zakazkaID = dih_dt[0].Zakazka_ID;
                    //        paletaID = dih_dt[0].Paleta_ID;
                    //        skladID = dih_dt[0].SKL_ID;
                    //        _dih_ta.DeleteQuery();
                    //    }

                    //    _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);

                    //}

                    #endregion

                    FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row skladZdroj = null;

                    // vyber zdrojoveho skladu
                    //if (Prodej.Globals.FiltrCiselnikSkladu)  // PeV - 25.9.2015 uprava, povoleni skladu a filtr na sklady je zvlast
                    if ((typdokladu != null && !typdokladu.Iscfg_skladyNull() && typdokladu.cfg_sklady > 0)) //|| PouzitSklady
                    {
                        // SKLAD_ID je nastaven -> dohleda se sklad z ciselniku skladu (pokud nenalezeno, probehne vyber)
                        // SKLAD_ID neni nastaven a neexistuje vybrany sklad -> zobrazi se ciselnik skladu a bude se prenaset skl_id z 095
                        // SKLAD_ID neni nastaven a existuje vybrany sklad -> dohleda se z davky (spatne popsano, existuje only one??)

                        // ma se vyuzit id zadaneho skladu
                        // id skladu vyplneno v konfiguraci aplikace
                        if (!string.IsNullOrEmpty(SkladID))
                        {
                            try
                            {
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                                ta_sklad.Connection.ConnectionString = "Data source=" + MyPath.SkladyDirectory;
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(SkladID);
                                if (dt_sklady.Count > 0)
                                    skladZdroj = dt_sklady[0];
                                else
                                {
                                    FlexibleMessageBox.Show(this, string.Format("Odpovídající sklad s ID '{0}' nenalezen! Vyberte jiný ze seznamu", SkladID.Trim()), "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Log.Write(ex);
                                FlexibleMessageBox.Show(this, ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // sklad nacten z jiz nasnimanych dat
                        #region ZZS nepouziva se

                        //if (skladZdroj == null && di_sklid != null && di_sklid != string.Empty && FiltrCiselnikSkladuOnlyOne)
                        //{
                        //    try
                        //    {
                        //        FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                        //        ta_sklad.Connection.ConnectionString = "Data source=" + CiselnikSkladyDB;
                        //        FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid);
                        //        if (dt_sklady.Count > 0)
                        //            skladZdroj = dt_sklady[0];
                        //        else
                        //        {
                        //            FASK.MST_WINDOWS.Module.ZZS.Forms.MessageBoxBigTimeout.Show(string.Format("Odpovídající sklad s ID '{0}' nenalezen! Vyberte jiný ze seznamu", di_sklid), "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLog.Log.Write(ex);
                        //        FASK.MST_WINDOWS.Module.ZZS.Forms.MessageBoxBigTimeout.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        //        return;
                        //    }
                        //} 
                        #endregion

                        // predvyplneno id skladu (SKL_ID) v typu dokladu
                        if (skladZdroj == null && !typdokladu.IsSKL_IDNull() && !string.IsNullOrEmpty(typdokladu.SKL_ID.Trim()))
                        {
                            try
                            {
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                                ta_sklad.Connection.ConnectionString = "Data source=" + MyPath.SkladyDirectory;
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(typdokladu.SKL_ID.Trim());
                                if (dt_sklady.Count > 0)
                                    skladZdroj = dt_sklady[0];
                                else
                                {
                                    //FASK.MST_WINDOWS.Module.ZZS.Forms.MessageBoxBigTimeout.Show(string.Format("Odpovídající sklad s ID '{0}' nenalezen! Vyberte jiný ze seznamu", di_sklid), "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Log.Write(ex);
                                FlexibleMessageBox.Show(this, ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }


                        #region Sklad musi bzt pevne definovat v Typu dokladu odkud dam
                        //if (skladZdroj == null)
                        //{
                        //    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                        //    {
                        //        if (fsv.ShowDialog() == DialogResult.Cancel)
                        //            return;

                        //        skladZdroj = fsv.Sklad;

                        //        if (skladZdroj == null)
                        //        {
                        //            ErrorLog.Log.Write("Není vybrán sklad, přestože je vyžadován!");
                        //            return;
                        //        }
                        //    }
                        //}
                        #endregion
                    }

                    FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093Row skladCil = null;
                    // zadani ciloveho skladu
                    if (typdokladu != null && !typdokladu.Iscfg_skl_id_destNull() && typdokladu.cfg_skl_id_dest > 0)
                    {
                        // sklad se ma prevzit ze zdrojoveho skladu
                        if (!typdokladu.Iscfg_skl_id_dest_prevzitNull() && typdokladu.cfg_skl_id_dest_prevzit > 0 && skladZdroj != null)
                            skladCil = skladZdroj;

                        // sklad nacten z jiz nasnimanych dat
                        #region  ZZS nepoziva se
                        //if (skladCil == null && di_sklid_dest != null && di_sklid_dest != string.Empty) //&& Prodej.Globals.FiltrCiselnikSkladuOnlyOne
                        //{
                        //    try
                        //    {
                        //        FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                        //        ta_sklad.Connection.ConnectionString = "Data source=" + CiselnikSkladyDB;
                        //        FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid_dest);
                        //        if (dt_sklady.Count > 0)
                        //            skladCil = dt_sklady[0];
                        //        else
                        //        {
                        //            FASK.MST_WINDOWS.Module.ZZS.Forms.MessageBoxBigTimeout.Show(string.Format("Odpovídající cílový sklad s ID '{0}' nenalezen! Vyberte jiný ze seznamu", di_sklid), "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLog.Log.Write(ex);
                        //        FASK.MST_WINDOWS.Module.ZZS.Forms.MessageBoxBigTimeout.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        //        return;
                        //    }
                        //} 
                        #endregion

                        // predvyplneno id skladu (SKL_ID) v typu dokladu
                        if (skladCil == null && !typdokladu.Ispredvyplnit_locncodedestNull() && !string.IsNullOrEmpty(typdokladu.predvyplnit_skl_id_dest.Trim()))
                        {
                            try
                            {
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                                ta_sklad.Connection.ConnectionString = "Data source=" + MyPath.SkladyDirectory;
                                FASK.MST_WINDOWS.Main.SQLCEDBS.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(typdokladu.predvyplnit_skl_id_dest.Trim());
                                if (dt_sklady.Count > 0)
                                    skladCil = dt_sklady[0];
                                else
                                {
                                    FlexibleMessageBox.Show(this, string.Format("Odpovídající cílový sklad s ID '{0}' nenalezen! Vyberte jiný ze seznamu", di_sklid), "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Log.Write(ex);
                                FlexibleMessageBox.Show(this, ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        #region ZZS musi byt napevno v typu pohybu definovat cilovy sklad
                        //if (skladCil == null)
                        //{
                        //    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                        //    {
                        //        fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberCilovehoSkladu;
                        //        if (fsv.ShowDialog() == DialogResult.Cancel)
                        //            return;

                        //        skladCil = fsv.Sklad;

                        //        if (skladCil == null)
                        //        {
                        //            ErrorLog.Log.Write("Není vybrán cílový sklad, přestože je vyžadován!");
                        //            return;
                        //        }
                        //    }
                        //} 
                        #endregion
                    }


                    #region ZZS prevod medzi sklady ne
                    //if ((typdokladu != null && typdokladu.cfg_prevod_sklad > 0)) //    || Prodej.Globals.PovolitPrevodMeziSklady
                    //{
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                    //    _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(StorageDir, cislodavky.ToString() + "." + ProdejOExt);
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
                    //    DialogResult dresult = DialogResult.None;
                    //    if (dih_dt.Count > 0)
                    //    {
                    //        zakazkaID = dih_dt[0].Zakazka_ID;
                    //        skladID = dih_dt[0].SKL_ID;
                    //        paletaID = dih_dt[0].Paleta_ID;
                    //        dresult = MessageBoxBig.Show("Cílový sklad byl již dříve zvolen, chcete ho použít?", "Info", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    //    }

                    //    if (dresult != DialogResult.Yes)
                    //    {
                    //        using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                    //        {
                    //            fsv.Text = "Vyberte cílový sklad";
                    //            if (fsv.ShowDialog() == DialogResult.Cancel)
                    //                return;

                    //            if (fsv.Sklad == null)
                    //            {
                    //                ErrorLog.Log.Write("Není vybrán cílový sklad, přestože je vyžadován!");
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                skladID = fsv.Sklad.skl_id;
                    //            }
                    //        }
                    //    }

                    //    if (dih_dt.Count > 0)
                    //        _dih_ta.DeleteQuery();
                    //    _dih_ta.Insert(zakazkaID, paletaID, "", skladID, cislodavky);
                    //} 
                    #endregion

                    #region ZZS nerezim zakazka ID

                    //if (typdokladu != null && typdokladu.cfg_zakazka_id > 0)
                    //{
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                    //    _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(StorageDir, cislodavky.ToString() + "." + ProdejOExt);
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();

                    //    if (dih_dt.Count > 0)
                    //    {
                    //        zakazkaID = dih_dt[0].Zakazka_ID;
                    //        skladID = dih_dt[0].SKL_ID;
                    //    }
                    //    if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloZakazky, zakazkaID, out zakazkaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
                    //    {
                    //        if (dih_dt.Count > 0)
                    //        {
                    //            paletaID = dih_dt[0].Paleta_ID;
                    //            _dih_ta.DeleteQuery();
                    //        }

                    //        _dih_ta.Insert(zakazkaID, paletaID, "", skladID, cislodavky);

                    //        //zakazkaID  davkaprodej = value;
                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //} 
                    #endregion



                    #region ZZS nema palety
                    //if (typdokladu != null && typdokladu.cfg_paleta_id > 0)
                    //{
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
                    //    _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(StorageDir, cislodavky.ToString() + "." + ProdejOExt);
                    //    FASK.MST_WINDOWS.Module.ZZS.SQLCEDB.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();

                    //    if (dih_dt.Count > 0)
                    //    {
                    //        paletaID = dih_dt[0].Paleta_ID;
                    //        skladID = dih_dt[0].SKL_ID;
                    //    }

                    //    if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloPalety, paletaID, out paletaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
                    //    {
                    //        if (dih_dt.Count > 0)
                    //        {
                    //            zakazkaID = dih_dt[0].Zakazka_ID;
                    //            _dih_ta.DeleteQuery();
                    //        }

                    //        _dih_ta.Insert(zakazkaID, paletaID, "", skladID, cislodavky);
                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }
                    //} 
                    #endregion


                    #region netreba asi sa vykonava uz v danem formu ??
                    //using (ProdejList prodejlist = new ProdejList(cislodavky, "", typdokladu, skladZdroj, skladCil, di_strid, "", di_serltnum))
                    //{
                    //    prodejlist.Zobrazeni = ProdejList.ZobrazeniTyp.List;
                    //    prodejlist.ShowDialog();
                    //}

                    #endregion

                    //
                    //if (MessageBoxBig.Show("Odeslat dávku?", "Info", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    //    == DialogResult.Yes)
                    //{
                    //    odeslatDavku(cislodavky);
                    //}

                    #endregion

                    //Potrebne dostat do RFID_Data
                    //TypPohybu typpohyb;
                    // int cislodavky;
                    //SQLCEDB.DataSets.Odberatele.CZMST090Row odberatel;

                    int davka = int.Parse(davkaprodej);

                    rfdi_data = new RFID_Data(typ,typName, davka.ToString(), Orientation.Horizontal , null , typdokladu, skladZdroj, skladCil, dtDI);
                    //rfdi_data.typPohyb = typ;
                    //rfdi_data.CisloDavky = davkaprodej;


                    this.Hide();
                    rfdi_data.MdiParent = this.MdiParent;
                    rfdi_data.Show();
                    rfdi_data.WindowState = FormWindowState.Maximized;
                    rfdi_data.FormClosed += new FormClosedEventHandler(rfdi_data_FormClosed);

                }
                catch (Exception ex)
                {
                    ErrorLog.Log.Write(ex);
                    FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //throw;
            }

        }



        private void GetNewDavka( ref string davkaprodej, ref string dstFile)
        {
            #region Vytvoreni davky

            //if (Prodej.Globals.RangeEnable)
            //{
            Config.CiselneRady cr = new Config.CiselneRady();
            davkaprodej = cr.ProdejGetNext();
            //}
            //else
            //{
            //    string value;
            //    if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyZadejteCisloDavky, "", out value, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
            //    {
            //        davkaprodej = value;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            if (!KontrolaCislaDavky(davkaprodej))
            {
                //MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejVyberDavkyChybaVCisleDavky);
                return;
            }

            int davkaprodeji = int.Parse(davkaprodej);



            dstFile = Path.Combine(MyPath.SQLCEDBSDirectory,  davkaprodeji.ToString() + "." + ProdejOExt);

            //string dstFile = Path.Combine(Main.StorageDir, davkaprodeji.ToString() + "." + Main.ProdejOExt);

            try
            {
                if (File.Exists(dstFile))
                {
                    //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejVyberDavkyDavkaJizExistuje, davkaprodeji), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                else
                {
                    //string srcFile = Path.Combine(configFilePath, @"SQLCEDB\Prodej.sdf");
                    File.Copy(MyPath.ProdejDirectory, dstFile, false);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log.Write(ex);
                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return;
            }

            #endregion
        }

        private bool KontrolaCislaDavky(string cislodavky)
        {
            try
            {
                _cislodavky = int.Parse(cislodavky);
                if (_cislodavky < 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }






        private void button2_Click(object sender, EventArgs e)
        {
            int oCount = 3;
            double sqrt = Math.Sqrt(oCount);
            double celing = Math.Ceiling(sqrt);
            int nN = (int)celing;
        }



        #region Otevrit Davku


        #endregion
    }
}
