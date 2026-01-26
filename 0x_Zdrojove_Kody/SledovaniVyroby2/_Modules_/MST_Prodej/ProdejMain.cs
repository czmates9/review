using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Fask.MST_W.Forms;
//using Fask.MST_W.ServerAccess;
using System.Linq;

namespace FASK.SledovaniVyroby.Module.MST_Prodej
{
    public partial class ProdejMain : System.Windows.Forms.Form
    {
        internal static ProdejMain prodejInstance = null;
        public ProdejService.ProdejService prodejs = null;
        public InformationService.InformationsService informationS = null;
        //public CiselnikService.CiselnikService ciselnikS = null;
        public _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
        public ConfigurationService.Configuration configurationS = null;
        public int cislodavky = -1; 

        public ProdejMain()
        {
            InitializeComponent();

            //this.Text = MST_Global.ProdejName;
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void ProdejMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.D1)
            {
                otevriDavku();
            }
            else if (e.KeyCode == Keys.D2)
            {
                odeslatDavku();
            }
            else if (e.KeyCode == Keys.D3)
            {
                stahnoutOdberatele();
            }
            else if (e.KeyCode == Keys.D4)
            {
                stahnoutZbozi();
            }
            else if (e.KeyCode == Keys.D5)
            {
                stahnoutStrediska();
            }
            else if (e.KeyCode == Keys.D6)
            {
                stahnoutTypDokladu();
            }
            else if (e.KeyCode == Keys.D7)
            {
                stahnoutSklady();
            }
            else if (e.KeyCode == Keys.D8)
            {
                stahnoutPracovniky();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformOK()
        {
            if (MessageBox.Show(Fask.Localization.Localization.Prodej3ProdejMainUkonceniPraceSModulemDotaz, MST_Global.ProdejName, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                return;

            DialogResult = DialogResult.OK;
        }

        private void buttonDavka_Click(object sender, EventArgs e)
        {
            otevriDavku();

        }

        private void otevriDavku()
        {
            // dotaz na aktualizaci ciselniku zbozi
            if (Prodej.Globals.AktualizaceZboziPredVyberemDavky)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceCiselnikuZboziDotaz, MST_Global.ProdejName, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.Yes)
                    stahnoutZbozi();
            }

            using (ProdejVyberDavky pvd = new ProdejVyberDavky())
            {
                pvd.HesloProOtevreniRozpracovaneDavky = Prodej.Globals.PozadovatHesloProOtevreniRozpracovaneDavky;
                if (pvd.ShowDialog() == DialogResult.Cancel)
                    return;

                this.cislodavky = pvd.CisloDavky;
            }

            #region 1.Existujici zaznam z DI

#if WindowsCE
            SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter di_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
#else
            SQLDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter di_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
#endif
            di_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);
            System.Data.SqlServerCe.SqlCeCommand scecommand = null;
            string di_docid = null;
            string di_docid2 = null;
            string di_odbid = null;
            string di_sklid = null;
            string di_strid = null;
            string di_sklid_dest = null;
            string di_serltnum = null;
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
                    di_odbid = Convert.ToString(sdr["odb_id"] is System.DBNull ? string.Empty : sdr["odb_id"]).Trim();
                    di_sklid = Convert.ToString(sdr["skl_id"] is System.DBNull ? string.Empty : sdr["skl_id"]).Trim();
                    di_strid = Convert.ToString(sdr["str_id"] is System.DBNull ? string.Empty : sdr["str_id"]).Trim();
                    di_sklid_dest = Convert.ToString(sdr["skl_id_dest"] is System.DBNull ? string.Empty : sdr["skl_id_dest"]).Trim();
                    // 6.6.2016 PeV: neprebira se, mohlo by byt vice sarzi a pak by byl zmatek, ktera je zvolena ...
                    //di_serltnum = Convert.ToString(sdr["serltnum"] is System.DBNull ? string.Empty : sdr["serltnum"]).Trim();
                }
                
            }
            catch (Exception ex)
            {
#if WindowsCE
                Logging.Log.Write(ex);
#else
                ErrorLog.Log.Write(ex.Message);
#endif
            }
            finally
            {
                if (scecommand != null && scecommand.Connection.State == ConnectionState.Open)
                    scecommand.Connection.Close();
            }
            #endregion

#if WindowsCE
            SqlCEDBs.DataSets.TypDokladu.CZMST092Row typdokladu = null;
#else
            SQLDBs.DataSets.TypDokladu.CZMST092Row typdokladu = null;
#endif

            if (Prodej.Globals.TypDokladu)
            {
                using (ProdejVyberTypuDokladu ptd = new ProdejVyberTypuDokladu(cislodavky))
                {
                    if (ptd.ShowDialog() == DialogResult.Cancel)
                        return;
                    typdokladu = ptd.TypDokladu;
                    if (typdokladu == null)
                        return;
                }
            }

            // zadani sarze na davku
            // zobrazit SN, pripadne vygenerovat?? ...
            if (typdokladu != null && !typdokladu.Iscfg_sn_na_davkuNull() && typdokladu.cfg_sn_na_davku > 0 && string.IsNullOrEmpty(di_serltnum))
            {
                // vygenerovani, pokud je povoleno, jinak rucne zadat
                if (!typdokladu.Iscfg_generovat_snNull() && typdokladu.cfg_generovat_sn > 0)
                {
                    di_serltnum = generovatSN();
                }

                SejmiKodForm skf = new SejmiKodForm();
                skf.Popis = "Šarže";
                skf.Text = "Zadaní šarže";
                skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
                //skf.MaxLength = Globals.LOCNCODE_LEN;
                //skf.Len = Globals.LOCNCODE_LEN;
                //skf.CheckLen = true;
                skf.AllowEmpty = false;
                skf.Kod = string.IsNullOrEmpty(di_serltnum) ? string.Empty : di_serltnum.Trim();

                if (skf.ShowDialog() == DialogResult.Cancel)
                    return;

                di_serltnum = skf.Kod;
            }

#if WindowsCE
            SqlCEDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
#else
            SQLDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
#endif

            if ((typdokladu != null && typdokladu.cfg_odb > 0) || Prodej.Globals.Odberatel)
            {
                using (ProdejVyberOdberatele po = new ProdejVyberOdberatele(typdokladu, cislodavky))
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

            //parametry hlavicky ...
            string zakazkaID = string.Empty;
            string paletaID = string.Empty;
            string skladID = string.Empty;
#if WindowsCE
            SqlCEDBs.DataSets.Meny.CZMST097Row mena = null;
#else
            SQLDBs.DataSets.Meny.CZMST097Row mena = null;
#endif

            if(odberatel != null && !odberatel.Ismena_IDNull() && odberatel.mena_ID.Trim().Length > 0)
            {
#if WindowsCE
                SqlCEDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter meny_ta = new Fask.MST_W.SqlCEDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter();
#else
                SQLDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter meny_ta = new SQLDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter();
#endif

                meny_ta.Connection.ConnectionString = "Data source=" + Main.CiselnikMenDB;
#if WindowsCE
                SqlCEDBs.DataSets.Meny.CZMST097DataTable dt = meny_ta.GetDataByMenaID(odberatel.mena_ID);
#else
                SQLDBs.DataSets.Meny.CZMST097DataTable dt = meny_ta.GetDataByMenaID(odberatel.mena_ID);
#endif

                if (dt.Count > 0)
                    mena = dt[0];
                else
                { // vychozi menu nastavit ... ???
                    //dt = meny_ta.GetData();
                    //var linqHlavniMena = dt.Where(m => m.mena_hlavni);
                    //if (linqHlavniMena.Count() > 0)
                    //{
                    //    mena = linqHlavniMena.First();
                    //}                        
                    
                    //dt = meny_ta.GetDataByHlavni(true);
                    //if (dt.Count > 0)
                    //{
                    //    mena = dt[0]; //nastavena hlavni mena ...
                    //}
                }
                // Update DIH ...
#if WindowsCE
                SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#else
                SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#endif
                
                
                _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);
                
#if WindowsCE
                SqlCEDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#else
                SQLDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#endif


                if (dih_dt.Count > 0)
                {
                    zakazkaID = dih_dt[0].Zakazka_ID;
                    paletaID = dih_dt[0].Paleta_ID;
                    skladID = dih_dt[0].SKL_ID;
                    _dih_ta.DeleteQuery();
                }
                _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);

            }
          
            if (typdokladu != null && !typdokladu.Iscfg_mena_idNull() && typdokladu.cfg_mena_id > 0 && mena == null)
            {
#if WindowsCE
                SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#else
                SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#endif

                _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);
#if WindowsCE
                SqlCEDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#else
                SQLDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#endif
                

                using (ProdejVyberMeny po = new ProdejVyberMeny())
                {
                    if (dih_dt.Count > 0)
                        po.SelectedMenaID = dih_dt[0].mena_ID;

                    if (po.ShowDialog() == DialogResult.Cancel)
                        return;

                    if (po.bezCiziMeny)
                        mena = null;
                    else
                        mena = po.SelectedMena;

                    //if (mena == null)
                    //{
                    //    Logging.Log.Write("Není vybrána mìna, pøestože je vyžadována!");
                    //    return;
                    //}
                }

                if (dih_dt.Count > 0)
                {
                    zakazkaID = dih_dt[0].Zakazka_ID;
                    paletaID = dih_dt[0].Paleta_ID;
                    skladID = dih_dt[0].SKL_ID;
                    _dih_ta.DeleteQuery();
                }

                _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);

            }
#if WindowsCE
            SqlCEDBs.DataSets.Sklady.CZMST093Row skladZdroj = null;
#else
            SQLDBs.DataSets.Sklady.CZMST093Row skladZdroj = null;
#endif


            // vyber zdrojoveho skladu
            //if (Prodej.Globals.FiltrCiselnikSkladu)  // PeV - 25.9.2015 uprava, povoleni skladu a filtr na sklady je zvlast
            if ((typdokladu != null && !typdokladu.Iscfg_skladyNull() && typdokladu.cfg_sklady > 0 ) || Prodej.Globals.PouzitSklady)
            {
                // SKLAD_ID je nastaven -> dohleda se sklad z ciselniku skladu (pokud nenalezeno, probehne vyber)
                // SKLAD_ID neni nastaven a neexistuje vybrany sklad -> zobrazi se ciselnik skladu a bude se prenaset skl_id z 095
                // SKLAD_ID neni nastaven a existuje vybrany sklad -> dohleda se z davky (spatne popsano, existuje only one??)

                // ma se vyuzit id zadaneho skladu
                // id skladu vyplneno v konfiguraci aplikace
                if (!string.IsNullOrEmpty(Prodej.Globals.SkladID))
                {
                    try
                    {
#if WindowsCE
                        SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.MST_W.SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#else
                        SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#endif

                        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;

#if WindowsCE
                        SqlCEDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(Prodej.Globals.SkladID);
#else
                        SQLDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(Prodej.Globals.SkladID);
#endif

                        if (dt_sklady.Count > 0)
                            skladZdroj = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, Prodej.Globals.SkladID.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return;
                    }
                }

                // sklad nacten z jiz nasnimanych dat
                if (skladZdroj == null && di_sklid != null && di_sklid != string.Empty && Prodej.Globals.FiltrCiselnikSkladuOnlyOne)
                {                    
                    try
                    {
#if WindowsCE
                        SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.MST_W.SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#else
                        SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#endif

                        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
#if WindowsCE
                        SqlCEDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid);
#else
                        SQLDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid);
#endif
                        if (dt_sklady.Count > 0)
                            skladZdroj = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return;
                    }
                }

                // predvyplneno id skladu (SKL_ID) v typu dokladu
                if (skladZdroj == null && !typdokladu.IsSKL_IDNull() && !string.IsNullOrEmpty(typdokladu.SKL_ID.Trim()))
                {
                    try
                    {
#if WindowsCE
                        SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.MST_W.SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#else
                        SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#endif

                        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
#if WindowsCE
#else
#endif
                        SQLDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(typdokladu.SKL_ID.Trim());
                        if (dt_sklady.Count > 0)
                            skladZdroj = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return;
                    }
                }

                if (skladZdroj == null)
                {
                    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                    {
                        if (fsv.ShowDialog() == DialogResult.Cancel)
                            return;

                        skladZdroj = fsv.Sklad;

                        if (skladZdroj == null)
                        {
                            Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
                            return;
                        }
                    }
                }
            }
#if WindowsCE
            SqlCEDBs.DataSets.Sklady.CZMST093Row skladCil = null;
#else
            SQLDBs.DataSets.Sklady.CZMST093Row skladCil = null;
#endif

            // zadani ciloveho skladu
            if (typdokladu != null && !typdokladu.Iscfg_skl_id_destNull() && typdokladu.cfg_skl_id_dest > 0)
            {
                // sklad se ma prevzit ze zdrojoveho skladu
                if (!typdokladu.Iscfg_skl_id_dest_prevzitNull() && typdokladu.cfg_skl_id_dest_prevzit > 0 && skladZdroj != null)
                    skladCil = skladZdroj;

                // sklad nacten z jiz nasnimanych dat
                if (skladCil == null && di_sklid_dest != null && di_sklid_dest != string.Empty && Prodej.Globals.FiltrCiselnikSkladuOnlyOne)
                {
                    try
                    {
#if WindowsCE
                        SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.MST_W.SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#else
                        SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#endif

                        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
#if WindowsCE
                        SqlCEDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid_dest);
#else
                        SQLDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid_dest);
#endif

                        if (dt_sklady.Count > 0)
                            skladCil = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladCilNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return;
                    }
                }

                // predvyplneno id skladu (SKL_ID) v typu dokladu
                if (skladCil == null && !typdokladu.Ispredvyplnit_locncodedestNull() && !string.IsNullOrEmpty(typdokladu.predvyplnit_skl_id_dest.Trim()))
                {
                    try
                    {
#if WindowsCE
                        SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.MST_W.SqlCEDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#else
                        SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new SQLDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
#endif

                        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
#if WindowsCE
                        SqlCEDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(typdokladu.predvyplnit_skl_id_dest.Trim());
#else
                        SQLDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(typdokladu.predvyplnit_skl_id_dest.Trim());
#endif

                        if (dt_sklady.Count > 0)
                            skladCil = dt_sklady[0];
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladCilNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex);
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        return;
                    }
                }

                if (skladCil == null)
                {
                    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                    {
                        fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberCilovehoSkladu;
                        if (fsv.ShowDialog() == DialogResult.Cancel)
                            return;

                        skladCil = fsv.Sklad;

                        if (skladCil == null)
                        {
                            Logging.Log.Write("Není vybrán cílový sklad, pøestože je vyžadován!");
                            return;
                        }
                    }
                }
            }
            
            if ((typdokladu != null && typdokladu.cfg_prevod_sklad > 0) || Prodej.Globals.PovolitPrevodMeziSklady)
            {
#if WindowsCE
                SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#else
                SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#endif

                _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);
#if WindowsCE
                SqlCEDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#else
                SQLDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#endif

                DialogResult dresult = DialogResult.None;
                if (dih_dt.Count > 0)
                {
                    zakazkaID = dih_dt[0].Zakazka_ID;
                    skladID = dih_dt[0].SKL_ID;
                    paletaID = dih_dt[0].Paleta_ID;
                    dresult = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainCilovySkladDriveZvolenPouzitStejnyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                }

                if (dresult != DialogResult.Yes)
                {
                    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                    {
                        fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberteCilovySklad;  // "Vyberte cílový sklad";
                        if (fsv.ShowDialog() == DialogResult.Cancel)
                            return;

                        if (fsv.Sklad == null)
                        {
                            Logging.Log.Write("Není vybrán cílový sklad, pøestože je vyžadován!");
                            return;
                        }
                        else
                        {
                            skladID = fsv.Sklad.skl_id;
                        }
                    }
                }
                    
                if (dih_dt.Count > 0)
                    _dih_ta.DeleteQuery();
                _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);
            }

            if (typdokladu != null && typdokladu.cfg_zakazka_id > 0)
            {
#if WindowsCE
                SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#else
                SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#endif

                _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);

#if WindowsCE
                SqlCEDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#else
                SQLDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#endif


                if (dih_dt.Count > 0)
                {
                    zakazkaID = dih_dt[0].Zakazka_ID;
                    skladID = dih_dt[0].SKL_ID;
                }
                if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloZakazky, zakazkaID, out zakazkaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
                {
                    if (dih_dt.Count > 0)
                    {
                        paletaID = dih_dt[0].Paleta_ID;
                        _dih_ta.DeleteQuery();
                    }

                    _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);

                    //zakazkaID  davkaprodej = value;
                }
                else
                {
                    return;
                }
            }

            if (typdokladu != null && typdokladu.cfg_paleta_id > 0)
            {
#if WindowsCE
                SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new Fask.MST_W.SqlCEDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#else
                SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter _dih_ta = new SQLDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
#endif

                _dih_ta.Connection.ConnectionString = "Data source=" + Path.Combine(Main.StorageDir, cislodavky.ToString() + "." + Main.ProdejOExt);
#if WindowsCE
                SqlCEDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#else
                SQLDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = _dih_ta.GetData();
#endif


                if (dih_dt.Count > 0)
                {
                    paletaID = dih_dt[0].Paleta_ID;
                    skladID = dih_dt[0].SKL_ID;
                }

                if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloPalety, paletaID, out paletaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
                {
                    if (dih_dt.Count > 0)
                    {
                        zakazkaID = dih_dt[0].Zakazka_ID;                        
                        _dih_ta.DeleteQuery();
                    }

                    _dih_ta.Insert(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, cislodavky);
                }
                else
                {
                    return;
                }
            }

            using (ProdejList prodejlist = new ProdejList(cislodavky, odberatel, typdokladu, skladZdroj, skladCil, di_strid, mena, di_serltnum))
            {
                prodejlist.Zobrazeni = ProdejList.ZobrazeniTyp.List;
                prodejlist.ShowDialog();
            }

            if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainOdeslatDavkuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.Yes)
            {
                odeslatDavku(cislodavky);
            }

        }

        /// <summary>
        /// Generovani SN.
        /// </summary>
        /// <returns>Vraci soucasny den v roce - 1 den</returns>
        private string generovatSN()
        {
            try
            {
                // TODO: tvorba knihovny pro generovani
                int day = DateTime.Now.DayOfYear;
                return DateTime.Now.ToString("yy") + day.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }

            return string.Empty;
        }

        private void buttonStahnoutOdberatele_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportOdberateluDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatOdberatele();
            }

            stahnoutOdberatele();
        }

        private void exportovatOdberatele()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberateleExport(ciselnikS);
        }

        private void buttonStahnoutZbozi_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportZboziDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatZbozi();
            }
            
            stahnoutZbozi();
        }

        private void exportovatZbozi()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZboziExport(ciselnikS, Prodej.Globals.Sklad);
        }


        private void stahnoutOdberatele()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberatele(ciselnikS, Prodej.Globals.Sklad);
        }

        private void stahnoutZbozi()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZbozi(ciselnikS, Prodej.Globals.Sklad);
        }

        private void exportovatSklady()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(ciselnikS);
        }

        private void stahnoutSklady()
        {
            // TODO : proc neni povoleno ... ??? je to blby, i kdyz nejsou filtry na sklady, tak byto chtelo mit sklady k dispozici pro zobrazeni informaci o nazvech skladu ...
            //if (Prodej.Globals.FiltrCiselnikSkladu)
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS);
            //else
            //    MessageBoxBig.Show("Filtry na èísla skladù nejsou povoleny", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        private void stahnoutTypDokladu()
        {
            if (Prodej.Globals.TypDokladu)
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogTypDokladu(ciselnikS, Prodej.Globals.Sklad);
            else  // prace s typy dokladu neni povolena
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainPraceSTypyDokladuNeniPovolena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        private void stahnoutStrediska()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediska(ciselnikS, Prodej.Globals.Sklad);
        }

        private void stahnoutPracovniky()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovnici(ciselnikS, Prodej.Globals.Sklad);
        }

        private void ProdejMain_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            //this.Location = new Point(0, 0);

            timerLoad.Enabled = true;
        }

        private void buttonOdeslatDavku_Click(object sender, EventArgs e)
        {
            odeslatDavku();
        }

        private void odeslatDavku()
        {
            int cd = 0;
            using (ProdejVyberDavky pvd = new ProdejVyberDavky(false))
            {
                if (pvd.ShowDialog() == DialogResult.Cancel)
                    return;
                cd = pvd.CisloDavky;
            }
            odeslatDavku(cd);
        }

        private void odeslatDavku(int cislodavkykodeslani)
        {
            int cd = cislodavkykodeslani;
            System.Data.SqlServerCe.SqlCeCommand scecommand = null;
            int pocetpolozek = 0;
            try
            {
                string davkafilename = Path.Combine(Main.StorageDir, cd.ToString() + "." + Main.ProdejOExt);
                scecommand = new System.Data.SqlServerCe.SqlCeCommand(
                    "Select count(*) from czmst_di",
                    new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.StorageDir, cd.ToString() + "." + Main.ProdejOExt))
                );
                scecommand.Connection.Open();

                pocetpolozek = (int)scecommand.ExecuteScalar();
            }
            finally
            {
                if (scecommand != null && scecommand.Connection.State == ConnectionState.Open)
                    scecommand.Connection.Close();
            }

            if (pocetpolozek <= 0)
            {
                MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaNeobsahujePolozky, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            try
            {
                Fask.MST_W.Program.mstw.mbw.BeginPracujiForm(string.Format(Fask.Localization.Localization.Prodej3ProdejMainOdesilamDavku, cd.ToString()));

                bool result = ProdejServiceOperations.SendData(prodejs, cd);
                Program.mstw.mbw.EndPracujiForm();

                if (result)
                {
                    if (Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky)
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaOdeslana, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                }
                else
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaSeNepodarilaOdeslat, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBox.Show(ex.Message, Fask.Localization.Localization.Prodej3ProdejMainChybaPriOdeslaniDavky, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }
        }

        private void Prodej_Main_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;

            try
            {
                prodejs = new Fask.MST_W.ProdejService.ProdejService();
                prodejs.Url = MST_Global.ServerAddress + "Prodej.asmx";
                prodejs.Timeout = MST_Global.ServiceTimeOut;
                prodejs.UpdateWebServiceCredentials();

                informationS = new Fask.MST_W.InformationService.InformationsService();
                informationS.Url = MST_Global.ServerAddress + "Informations.asmx";
                informationS.Timeout = MST_Global.ServiceTimeOut;
                informationS.UpdateWebServiceCredentials();

                ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
                ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                ciselnikS.Timeout = MST_Global.ServiceTimeOut;
                ciselnikS.UpdateWebServiceCredentials();

                configurationS = new Fask.MST_W.ConfigurationService.Configuration();
                configurationS.Url = MST_Global.ServerAddress + "Configuration.asmx";
                configurationS.Timeout = MST_Global.ServiceTimeOut;
                configurationS.UpdateWebServiceCredentials();

                prodejInstance = this;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Prodej3ProdejMainChyba, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                Close();
            }

            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            //Nacteni globalni konfigurace prodeje
            try
            {
                Prodej.Globals.Load(Main.ConfigModulesFileName);

                //Toto nemuze byt takto reseno, protoze muze rozhodovat az typ pouziteho dokladu ...
                // z testu upravy pro LITRA 18.1.2013 - JiS
                //this.buttonCiselikSkladu.Enabled = Prodej.Globals.FiltrCiselnikSkladu;
                //this.buttonStahnoutOdberatele.Enabled = Prodej.Globals.Odberatel;
                //this.buttonStahnoutStrediska.Enabled = Prodej.Globals.Strediska;
                //this.buttonStahnoutTypDokladu.Enabled = Prodej.Globals.TypDokladu;
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }

            // TODO : jak poresit ???
            //buttonStahnoutTypDokladu.Visible = buttonStahnoutTypDokladu.Enabled = Prodej.Globals.TypDokladu;
            //buttonStahnoutStrediska.Visible = buttonStahnoutStrediska.Enabled = Prodej.Globals.Strediska;

            Cursor.Current = Cursors.Default;
        }

        private void buttonDavka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonDavka_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonOdeslatDavku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonOdeslatDavku_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStahnoutOdberatele_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStahnoutOdberatele_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStahnoutZbozi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStahnoutZbozi_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStahnoutVse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.buttonStahnoutVse_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonKonec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonKonec_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStahnoutStrediska_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportStredisekDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatStrediska();
            }
            

            stahnoutStrediska();
        }

        private void exportovatStrediska()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediskaExport(ciselnikS);
        }

        private void buttonStahnoutTypDokladu_Click(object sender, EventArgs e)
        {
            stahnoutTypDokladu();
        }

        private void buttonStahnoutStrediska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStahnoutStrediska_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonStahnoutTypDokladu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStahnoutTypDokladu_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonCiselikSkladu_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatSklady();
            }
            

            stahnoutSklady();
        }

        private void buttonCiselikSkladu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonCiselikSkladu_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonKonfigurace_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Fask.MST_W.ConfigurationService.dsCZMSTCFG czmstcfg = configurationS.GetCZMSTCFG();

                //Vytahnuti konfiguracnich parametru prodeje a nastaveni/prenastaveni konfigurace...
                czmstcfg.CZMSTCFG.PrimaryKey = new DataColumn[] { czmstcfg.CZMSTCFG.PNAMEColumn };
                Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow cfgrow = null;
                
                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena0SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price0IsWithTax = (cfgrow.PVALUEBL == 1);
                
                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena1SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price1IsWithTax = (cfgrow.PVALUEBL == 1);
                
                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena2SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price2IsWithTax = (cfgrow.PVALUEBL == 1);

                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena3SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price3IsWithTax = (cfgrow.PVALUEBL == 1);

                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena4SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price4IsWithTax = (cfgrow.PVALUEBL == 1);

                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena5SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.Price5IsWithTax = (cfgrow.PVALUEBL == 1);

                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejVystupCenaSDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.PriceIsWithTax = (cfgrow.PVALUEBL == 1);

                cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejVystupCenaSDaniPovoleno") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
                if (cfgrow != null && !cfgrow.IsPVALUEBLNull()) 
                    Prodej.Globals.PriceIsWithTaxEnable = (cfgrow.PVALUEBL == 1);

                Prodej.Globals.Save(Main.ConfigModulesFileName);

                Cursor.Current = Cursors.Default;

                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceDokoncena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceSeNezdarila, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        private void buttonStahnoutPracovniky_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportPracovnikuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatPracovniky();
            }
            
            stahnoutPracovniky();
        }

        private void exportovatPracovniky()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovniciExport(ciselnikS);
        }

        private void buttonStahnoutPracovniky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonStahnoutPracovniky_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonCiselnikMen_Click(object sender, EventArgs e)
        {
            if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportMenDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatMeny();
            }
            

            stahnoutMeny();
        }

        private void exportovatMeny()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMenyExport(ciselnikS);
        }

        private void stahnoutMeny()
        {
            if (Prodej.Globals.FiltrCiselnikMen)
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMen(ciselnikS);
            else
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainFiltryMenyNeniPovolen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        private void ProdejMain_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ProdejMain_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void ProdejMain_Closing(object sender, CancelEventArgs e)
        {
            // prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
            Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
        }

        private void buttonStahnoutLokace_Click(object sender, EventArgs e)
        {
            stahnoutLokace();
        }

        private void stahnoutLokace()
        {
            // TODO: omezeni na urcity sklad/sklady??
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogLokace(ciselnikS, string.Empty);
        }

    }
}