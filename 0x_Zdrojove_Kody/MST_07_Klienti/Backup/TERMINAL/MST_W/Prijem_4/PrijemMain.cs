using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using System.Reflection;
using System.Linq;

namespace Fask.MST_W.Prijem_4
{
    // TODO : upravit design formulare ... 
    // - pridat status bar?
    // - zobrazit vybrany sklad
    // - nejake menu?
    // - nejak jinak udelat?

	public partial class PrijemMain : System.Windows.Forms.Form
    {
        /// <summary>
        /// Globalni instance modulu Prijem
        /// </summary>
        internal static PrijemMain prijemInstance = null;

		private Fask.MST_W.Classes.Paleta rez_1 = null; //dodatecna hodnota, slouzi k uvadeni poctu palet

        /// <summary>
        /// Globalni objekt pro instanci modulu prijem
        /// </summary>
        internal GlobalObject globalObject = new GlobalObject();

        /// <summary>
        /// Constuktor
        /// </summary>
        public PrijemMain()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Udalost pred zobrazenim dialogu 
        /// </summary>
        private void PrijemMain_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;
                
                prijemInstance = this;

                //Nacteni globalni konfigurace Prijmu
                try
                {
                    Globals.Load(Main.ConfigModulesFileName);
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message);
                }

                Hlavicky.LoadHlavicky();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            VyberSkladu();
        }

        /// <summary>
        /// Vyber globalniho skladu pro prijem
        /// </summary>
        private void VyberSkladu()
        {
            try
            {
                if (!Globals.SkladPouzit)
                {
                    globalObject.sklad = null;
                    return;
                }

                if (this.InvokeRequired)
                {
                    this.BeginInvoke((Action)delegate()
                    {
                        VyberSkladu();
                    }
                    );
                    return;
                }

                #region Vyber povinneho skladu
                // TODO : vyber predvybraneho skladu

                if (!String.IsNullOrEmpty(Globals.SkladID))
                {
                    try
                    {
                        var dt_sklady = prijemInstance.globalObject.controller_sklady.GetDataBySkl_id(Globals.SkladID);
                        if (dt_sklady.Count > 0)
                        {
                            prijemInstance.globalObject.sklad = dt_sklady[0];
                            return;
                        }
                        else
                        {
                            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, Globals.SkladID.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            //this.Close();
                            //return;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Logging.Log.Write(ex);


                        Logging.Log.Write(ex);
                        //if ((ex.Message == @"The database file cannot be found. Check the path to the database. [ Data Source = \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]")
                        //    || (ex.Message == @" [ \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]"))

						//unable to open database file
                        if (ex.Message.Contains(@"Data\Sklady.sdf"))
                            MessageBoxBigTimeout.Show("Nenalezen èíselník 'Sklady.sdf'." + Environment.NewLine + "Pøed pokraèováním zaktualizujte èíselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        else
                            MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);





                        this.Close();
                        return;
                    }
                }

                using (Forms.FormSkladVyber fsklad = new FormSkladVyber())
                {
                    while (true)
                    {
                        if (fsklad.ShowDialog() == DialogResult.Cancel)
                        {
                            DialogResult dlgresOpakovat = MessageBoxBig.Show(Localization.Localization.Prijem4PrijemMainSkladNenastavenOpakovatDotaz, Fask.Localization.Localization.Prijem4PrijemMainSklad, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1, Color.Red, true);
                            if (dlgresOpakovat == DialogResult.Cancel)
                            {
                                this.Close();
                                return;
                            }
                            else
                                continue;
                        }
                        else
                        {
                            prijemInstance.globalObject.sklad = fsklad.Sklad;
                            if (prijemInstance.globalObject.sklad == null)
                                continue;
                            else
                                break;
                        }
                    }
                }
                #endregion

            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Aktualizace textu v zahlavi okna 
        /// </summary>
        private void UpdateUI()
        {
            try
            {
                if (this.IsDisposed)
                    return;

                this.Text = "Pøíjem ";
                if (prijemInstance.globalObject.sklad != null)
                    this.Text += " " + prijemInstance.globalObject.sklad.skl_id.Trim() + ":" + prijemInstance.globalObject.sklad.skl_desc.Trim();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// akce tlacitka Konec
        /// </summary>
        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformEnd();
        }

        /// <summary>
        /// Metoda, ktera se vola pred ukoncenim dialogu
        /// </summary>
        private void PerformEnd()
        {
            try
            {
                //if (Globals.PrijemDialogOpusteniModulu)
                //{
                //    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainMenuNavratDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                //== DialogResult.Yes)
                //    {
                //        this.DialogResult = DialogResult.OK;
                //    }
                //}
                //else
                //{
                //    this.DialogResult = DialogResult.OK;
                //}

                if (Globals.PrijemDialogOpusteniModulu)
                {
                    var dialogResult = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainMenuNavratDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                    if (dialogResult == DialogResult.No)
                        return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        /// <summary>
        /// Akce pri stisknuti nejake klavesy
        /// </summary>
        private void PrijemMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformEnd();
            }
            else if (e.KeyCode == Keys.D1)
            { // zpracuj davku
                //buttonDavka_Click(null, null);
                this.zpracujDavku();
            }
            else if (e.KeyCode == Keys.D2)
            { // stahni davku
                //stahniDavku_but_Click(null, null);
                stahniDavku();
            }
            else if (e.KeyCode == Keys.D3)
            { // odeslat davku
                //odesliHotovouDavku_but_Click(null, null);
                odesliHotovouDavku();
            }
            else if (e.KeyCode == Keys.D4)
            { // vratit davku
                //vratitDavku_but_Click(null, null);
                vratitDavku();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Vraci seznam cisel davek ve zvolene davce. Sloucena davka zacina prefixem "S"
        /// </summary>
        /// <param name="vybranaDavkaFileName">plna cesta k vybrane "sloucene" davce</param>
        /// <returns>Seznam davek ve sloucene, pokud neni sloucena, pak vraci sve cislo</returns>
        public List<string> GetSloucenaDavkaListDavek(string vybranaDavkaFileName)
        {
            List<string> seznamDavek = new List<string>();
            string cisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);
            if (cisloDavky.StartsWith("S"))
            {
                Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable dtS = new Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable(); // na zacatku to bude prazdne
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem controller_prijem_sloucene = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(vybranaDavkaFileName))
                {
					dtS = controller_prijem_sloucene.GetData_Sloucene();
                }

                foreach (Fask.SQLiteDBs.DataSets.Prijem.SlouceneRow row in dtS)
                {
                    string fileName = System.IO.Path.Combine(Main.StorageDir, row.CountEntries.ToString() + "." + Main.Ext_Prijem);
                    if (!seznamDavek.Contains(fileName))
                        seznamDavek.Add(fileName);
                }
            }
            else
                seznamDavek.Add(vybranaDavkaFileName);

            return seznamDavek;
        }

        /// <summary>
        /// Presune nasnimane polozky ze sloucene davky do cilove davky
        /// </summary>
        /// <param name="counentriesSourceFilePath">cesta ke sloucene davce</param>
        /// <param name="countentriesDestinationFilePath">cesta k cilove davce</param>
        public void ParseSloucenaDavka(string counentriesSourceFilePath, string countentriesDestinationFilePath)
        {
            if (counentriesSourceFilePath == countentriesDestinationFilePath)
                return; // jde o stejny soubor, tak se nebude nic nikam presouvat

            string cisloDavky = Path.GetFileNameWithoutExtension(counentriesSourceFilePath);

            // presouva se pouze, pokud je zdroj sloucena davka
            if (cisloDavky.StartsWith("S")) 
            {
                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dtPI = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem controller_prijem_sloucena = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(counentriesSourceFilePath))
                {
                    dtPI = controller_prijem_sloucena.GetDataByCountEntries_PI(int.Parse(Path.GetFileNameWithoutExtension(countentriesDestinationFilePath)));
                }

                foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow row in dtPI)
                {
                    row.SetAdded();
                }

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem controller_prijem_destination = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(countentriesDestinationFilePath))
                {
                    controller_prijem_destination.CZMST_PI_DeleteAndUpdate(dtPI);
                }
            }
        }

        /// <summary>
        /// akce kliku na button vratit davku
        /// </summary>
        private void vratitDavku_but_Click(object sender, EventArgs e)
        {
            vratitDavku();
        }

        /// <summary>
        /// Akce vraceni davky na server
        /// </summary>
        private void vratitDavku()
        {
            try
            {
                List<string> filenames = new List<string>();
                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                string cisloDavky = string.Empty;
                string vybranaDavkaFileName = string.Empty;
                Hlavicky.Synchronize();
                using (PrijemDavkyList pdl = new PrijemDavkyList(Hlavicky.Davky, false))
                {
                    Cursor.Current = Cursors.Default;
                    if (pdl.ShowDialog() == DialogResult.Cancel) return;

                    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainVratitDavkuDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                        == DialogResult.No)
                        return;
                    vybranaDavkaFileName = pdl.FileName;
                    cisloDavky = pdl.Davka;
                }

                //seznam davek ve sloucene davce
                filenames = GetSloucenaDavkaListDavek(vybranaDavkaFileName);

                bool deleteSloucenaDavka = true;
                //for (int i = 0; i < filenames.Count; i++)
                foreach (var filename in filenames)
                {
                    //import dat ze sloucene davky do jednotlivych davek...
                    //ParseSloucenaDavka(vybranaDavkaFileName, filenames[i]);
                    ParseSloucenaDavka(vybranaDavkaFileName, filename);

                    string actualCisloDavky = Path.GetFileNameWithoutExtension(filename);

                    #region Test zda jiz neco je nasnimano. Pokud tam neco je tak dotaz nebo neumoznit, pokud je to s lokacemi...
                    Fask.SQLiteDBs.DataSets.Prijem paramsdata = new Fask.SQLiteDBs.DataSets.Prijem();
                    int? result_pi_count = null;

                    using (var controller_prijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(filename))
                    {
                        #region old count all pi
                        //using (var sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + filename))
                        //{
                        //    using (
                        //        var scecommand = new System.Data.SQLite.SQLiteCommand(
                        //            "Select Count(*) From CZMST_PI",
                        //            sqlConnection
                        //            ))
                        //    {
                        //        try
                        //        {
                        //            scecommand.Connection.Open();
                        //            result_pi_count = scecommand.ExecuteScalar();
                        //        }
                        //        finally
                        //        {
                        //            if (scecommand.Connection.State == ConnectionState.Open)
                        //                scecommand.Connection.Close();
                        //        }
                        //    }
                        //}
                        #endregion
                        result_pi_count = controller_prijem.CZMST_PI_Count_All();

                        #region old code parametry fill
                        //using (var sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + filenames[i]))
                        //{
                        //    // nacteni parametru
                        //    using (System.Data.SQLite.SQLiteDataAdapter paramsadapter = new System.Data.SQLite.SQLiteDataAdapter(
                        //        "Select * From Parametry",
                        //        sqlConnection
                        //    ))
                        //    {
                        //        paramsadapter.Fill(paramsdata, paramsdata.Parametry.TableName);
                        //    }
                        //}
                        #endregion
                        controller_prijem.Fill_Param(paramsdata.Parametry);
                    }

                    //if (result_pi_count != null && ((int)result_pi_count) > 0)
                    if ((result_pi_count ?? 0) > 0)
                    {
                        if (paramsdata.Parametry.Count > 0 && !paramsdata.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && paramsdata.Parametry[0].CONFIG_LOKACE_POVOLIT)
                        {
                            MessageBoxBig.Show("Dávku nelze vrátit, protože obsahuje nasnímané položky!", "Dotaz", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                            return;
                        }
                        else
                            if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainVratitDavkuPolozkyAnulovatDotaz,
                                Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                                return;
                    }
                    #endregion

                    //nic mazat nechci, vracim to co jsem ulozil, server si s tim poradi,\
                    //musi, protoze mohou byt online zapisy, ktere se musi odmazat ... !!!

                    #region Odesilani dat
                    if (odesliDavku(actualCisloDavky, true))
                    {
                        DeleteDataFromSloucena(vybranaDavkaFileName, actualCisloDavky);

                        Hlavicky.HlavickaDelete(actualCisloDavky);
                        File.Delete(filename);
                    }
                    else
                    {
                        deleteSloucenaDavka = false;
                    }
                    #endregion

                }
                
                if (deleteSloucenaDavka)
                    SmazatSloucenaDavka(vybranaDavkaFileName);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        /// <summary>
        /// Smaze sloucenou davku, pokud v ni nezustaly zadne data v pe a pi
        /// </summary>
        /// <param name="vybranaDavkaFileName">sloucena davka</param>
        /// <param name="cisloDavky"></param>
        private void SmazatSloucenaDavka(string vybranaDavkaFileName)
        {
            string cisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);
            if (cisloDavky.StartsWith("S"))
            {
                #region old code
                //if ((GetCountPEData(vybranaDavkaFileName) <= 0) && (GetCountPIData(vybranaDavkaFileName) <= 0))
                //{
                //    System.Diagnostics.Debug.Assert("S1" == cisloDavky);
                //    Hlavicky.HlavickaDelete("S1");
                //    File.Delete(vybranaDavkaFileName);
                //}
                #endregion

                int? count_pe = null;
                int? count_pi = null;
                using (var controller_prijem_davka = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(vybranaDavkaFileName))
                {
                    count_pe = controller_prijem_davka.CountQuery_PE();
                    count_pi = controller_prijem_davka.CountQuery_PI();
                }
                if (((count_pe ?? 0) <= 0) && ((count_pi ?? 0) <= 0))
                {
                    System.Diagnostics.Debug.Assert("S1" == cisloDavky);
                    Hlavicky.HlavickaDelete("S1");
                    File.Delete(vybranaDavkaFileName);
                }
            }
        }

        /// <summary>
        /// Smaze Data ze sloucene davky z PE a PI
        /// </summary>
        /// <param name="vybranaDavkaFileName">sloucena davka</param>
        private void DeleteDataFromSloucena(string vybranaDavkaFileName, string countEntries)
        {
            string cisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);
            if (cisloDavky.StartsWith("S"))
            {
                using (var controller_prijem_sloucena = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(vybranaDavkaFileName))
                {
                    controller_prijem_sloucena.DeleteQueryByCountEntries_PI(Convert.ToInt32(countEntries));
                    controller_prijem_sloucena.DeleteQuery_PE(Convert.ToInt32(countEntries));
                }

            }
        }

        /// <summary>
        /// akce po kliku na odeslat button
        /// </summary>
        private void odesliHotovouDavku_but_Click(object sender, EventArgs e)
        {
            odesliHotovouDavku();
        }

        /// <summary>
        /// Akce odeslani davky 
        /// </summary>
        private void odesliHotovouDavku()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                Hlavicky.Synchronize();

                string vybranaDavkaFileName;
                using (PrijemDavkyList pdl = new PrijemDavkyList(Hlavicky.Davky, false))
                {
                    Cursor.Current = Cursors.Default;
                    if (pdl.ShowDialog() == DialogResult.Cancel) return;
                    //pdlDavka = pdl.Davka;
                    vybranaDavkaFileName = pdl.FileName;
                }

                odesliHotovouDavku(vybranaDavkaFileName);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        //private void odeslatDavku(string pdlDavka, ref List<string> filenames, string vybranaDavkaFileName, ref bool deleteSloucenaDavka)
        internal bool odesliHotovouDavku(string vybranaDavkaFileName)
        {
            try
            {
                //seznam davek ve sloucene davce
                List<string> filenames = GetSloucenaDavkaListDavek(vybranaDavkaFileName);

                bool deleteSloucenaDavka = true;
                //for (int i = 0; i < filenames.Count; i++)
                foreach (var filename in filenames)
                {
                    //import dat ze sloucene davky do jednotlivych davek...
                    ParseSloucenaDavka(vybranaDavkaFileName, filename);

                    #region kontrola dokoncenosti...
                    //using (PrijemList lp3 = new PrijemList(filenames[i]))
                    //{
                    //    //if (!stavVydeje(filename))
                    //    if (!lp3.kontrolaDokoncenosti())
                    //    {
                    //        if (Messaa geBoxBig.Show("Tato dávka ješte není dokonèena. Opravdu ji chcete odeslat?",
                    //            "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                    //            return;
                    //    }
                    //}
                    #endregion

                    string countEntries = Path.GetFileNameWithoutExtension(filename);
                    if (odesliDavku(countEntries, false))
                    {
                        DeleteDataFromSloucena(vybranaDavkaFileName, countEntries);

                        Hlavicky.HlavickaDelete(countEntries);
                        File.Delete(filename);
                    }
                    else
                        deleteSloucenaDavka = false;
                }
                if (deleteSloucenaDavka)
                    SmazatSloucenaDavka(vybranaDavkaFileName);

                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Odeslani davky
        /// </summary>
        /// <param name="vybranadavka">davka k odeslani</param>
        /// <param name="uvolnit">True=vratit, False=zpracovat</param>
        /// <returns></returns>
        public bool odesliDavku(string vybranadavka, bool uvolnit)
        {
            string vybranadavkaFileName = vybranadavka + "." + Main.Ext_Prijem;
            try
            {
                Fask.SQLiteDBs.DataSets.Prijem dsprijem = new Fask.SQLiteDBs.DataSets.Prijem();

                using (var controller_prijem_davka_k_odeslani = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(vybranadavkaFileName))
                {
                    #region Zmnena data dokladu : dotaz na zmenu data prijmu, pokud je povoleno v konfiguraci terminalu
                    // dotaz na zmenu data prijmu, pokud je povoleno v konfiguraci terminalu
                    if (Globals.ZmenaDataDokladu)
                    {
                        DateTime dt = DateTime.Now;
                        if (DialogResult.Cancel == DateInputBox.Show(Fask.Localization.Localization.Prijem4PrijemMainZadaniDatumuDokladu, dt, out dt))
                        {
                            return false;
                        }
                        var pih_dt = controller_prijem_davka_k_odeslani.GetDataByCountentries_PIH(Convert.ToInt32(vybranadavka));
                        if (pih_dt.Count == 0)
                            pih_dt.AddCZMST_PIHRow(Convert.ToInt32(vybranadavka), dt);
                        else
                            pih_dt[0].DATUMDOKLADU = dt;
                        controller_prijem_davka_k_odeslani.Update_PIH(pih_dt);
                    }
                    #endregion

                    #region kontrola, zdali jsou vyplneny lokace u vsech zaznamu
                    // kontrola, zdali jsou vyplneny lokace u vsech zaznamu
                    if (!uvolnit && Prijem_4.Globals.KontrolaVyplneniLokaciPredOdeslanim)
                    {
                        //SqlCEDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pi_da = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
                        //pi_da.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(Main.DataDir, vybranadavka + "." + Main.PrijemExt));
                        //int? nevyplnene_lokace = pi_da.CountLocncodeEmpty();
                        //pi_da.Dispose();

                        int? nevyplnene_lokace = controller_prijem_davka_k_odeslani.CountLocncodeEmpty_PI();
                        // nevyly vyplneny lokace
                        //if (!nevyplnene_lokace.HasValue || nevyplnene_lokace.Value > 0)
                        if ((nevyplnene_lokace ?? 0) > 0)
                        {
                            DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainLokaceNevyplnenyPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                            if (dr == DialogResult.No)
                                return false;
                        }
                    }
                    #endregion

                    #region Tisk etiket pred odeslanim
                    // TODO : tisk etiket nasminanych pred odeslanim ... 
                    // 1) dotahnout detail dokladu objednavky
                    //   - pokud je povoleno online funkce detail objednavky
                    // TODO: !!!    // 1a) dotahnout detail polozky objednavky
                    //   - pokud je povoleno online funkce detail polozky objednavky
                    // 2) zobrazit nejake info o poctu tisku
                    // 3) vytisknout vsechny s nejakym infem o tisku ... ???
                    if (!uvolnit && Globals.EtiketyTiskPredOdeslanimDotaz)
                    {
                        Fask.SQLiteDBs.DataSets.Prijem prijemds = new Fask.SQLiteDBs.DataSets.Prijem();

                        controller_prijem_davka_k_odeslani.Fill_PI(prijemds.CZMST_PI);

                        int pocetetiket = 0;
                        int pocetvytisteno = 0;
                        int pocetnevytisteno = 0;

                        pocetetiket = prijemds.CZMST_PI.Count;

                        DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainTiskPocetEtiketDotaz, pocetetiket), Fask.Localization.Localization.Prijem4PrijemMainTiskNasnimanychPolozek, MessageBoxButtons.YesNoCancel, MessageBoxBigIcon.Question);
                        if (dr == DialogResult.Cancel)
                            return false;
                        else if (dr == DialogResult.Yes)
                        { // tisknout ...
                            string tiskmessagemain = Fask.Localization.Localization.Prijem4PrijemMainTiskEtiketPrijemky;
                            //zacatek tisku
                            Program.mstw.mbw.BeginPracujiForm(tiskmessagemain);

                            foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi_r in prijemds.CZMST_PI)
                            {
                                //SqlCEDBs.DataSets.Prijem.CZMST_PEDataTable pe_dt = pe_da.GetDataByKey(pi_r.PONUMBER, pi_r.ITEMNMBR, pi_r.ORD);
                                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable pe_dt = controller_prijem_davka_k_odeslani.GetDataByKey_PE(pi_r.PONUMBER, pi_r.ITEMNMBR, pi_r.ORD);
                                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe_r = null;
                                if (pe_dt.Count > 0)
                                    pe_r = pe_dt[0];

                                //if (PrijemTisk.Print(pe_r, pi_r, MST_Global.PrintServerTemplateNamePrijemNasnimane, 1))
                                if (PrijemTisk.Print(pe_r, pi_r, PrinterFactory.PrinterModules.PrijemNasnimane, 1))
                                {
                                    // TODO : pokud vytisteno, tak pridat udalost vytisteni ...
                                    pocetvytisteno++;
                                }
                                else
                                {
                                    pocetnevytisteno++;
                                }

                                //Program.mstw.mbw.Zprava = tiskmessagemain + 
                                //    "\nCelkem: " + pocetetiket + 
                                //    "\nVytisknuto: " + pocetvytisteno + 
                                //    "\nNetisknuto: " + pocetnevytisteno;
                                Program.mstw.mbw.Zprava = string.Format(Fask.Localization.Localization.Prijem4PrijemMainTiskEtiketPrubeh, tiskmessagemain, pocetetiket, pocetvytisteno, pocetnevytisteno);

                            }

                            //Konec tisku
                            Program.mstw.mbw.EndPracujiForm();

                            //DialogResult drHotovo = MessageBoxBig.Show("Konec Tisku etiket" +
                            //        "\nCelkem: " + pocetetiket +
                            //        "\nVytisknuto: " + pocetvytisteno +
                            //        "\nNetisknuto: " + pocetnevytisteno +
                            //        "\n\nPokraèovat odesláním dávky?",
                            //        "Tisk etiket nasnímaných položek",
                            //        MessageBoxButtons.YesNo,
                            //        MessageBoxBigIcon.Question);
                            DialogResult drHotovo = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainTiskEtiketVysledek, pocetetiket, pocetvytisteno, pocetnevytisteno),
                                    Fask.Localization.Localization.Prijem4PrijemMainTiskNasnimanychPolozek,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxBigIcon.Question);
                            if (drHotovo == DialogResult.No)
                                return false;
                        }
                        else if (dr == DialogResult.No)
                        {
                        }
                    }
                    #endregion

                    #region Odeslani fotek
                    // pokud se odesilaji data a je povolene foceni na prijmu, dojde k odeslani fotek
                    if (!uvolnit && Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
                    {
                        Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemMainOdesilaniFotografii);
                        ImagesSynchronize();
                        Program.mstw.mbw.EndPracujiForm();
                    }
                    #endregion

                    #region nacteni fotek davky pro pozdejsi smazani
                    // davka se vraci, probehne odstraneni fotek
                    if (uvolnit && Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
                    {
                        //SqlCEDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter _pif_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter();
                        //_pif_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(Main.StorageDir, vybranadavka + "." + Main.PrijemExt));
                        //_pif_ta.Fill(dsprijem.CZMST_PI_F);
                        //_pif_ta.Dispose();
                        controller_prijem_davka_k_odeslani.Fill_PIF(dsprijem.CZMST_PI_F);
                    }
                    #endregion
                } // using (var controller_prijem...

                #region Odeslani davky
                //Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemMainOdesilaniDatPrijemky);
                if (PrijemServiceOperations.SendData(Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem, int.Parse(vybranadavka), uvolnit))
                {
                    Program.mstw.mbw.EndPracujiForm();
                    if (Prijem_4.Globals.PrijemDialogUspesnehoOdeslaniDavky)
                        MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainDataDavkyOdeslana, vybranadavka), Fask.Localization.Localization.Prijem4PrijemMainOdesilaniDat, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    Hlavicky.HlavickaDelete(vybranadavka);

                    // pokud se vraci davka, dojde ke smazani dat z CZMST_PI_F a fotek
                    // odstraneni fotek, pokud se davka uspesne vratila
                    if (uvolnit && Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
                    {
                        foreach (var item in dsprijem.CZMST_PI_F)
                        {
                            string filepath = Path.Combine(Main.ImagesDir, item.IMG_NAME.Trim());
                            if (File.Exists(filepath))
                                File.Delete(filepath);
                        }
                    }

                    if (!uvolnit) //neuzavirat pokud se davka vraci ...
                    {
                        // todo : vice terminaly ... ???
                        prijemuzavritdavku(int.Parse(vybranadavka));
                    }

                    return true;
                }
                #endregion
                return false;

            }
            catch (Exception ex)
            {
                //Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex.Message + ex.StackTrace, "Prijem odesliDavku");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                //Program.mstw.mbw.EndPracujiForm();
            }
        }

        /// <summary>
        /// Provede uzavøení dávky na potvrzení uživatelem, pokud je nastaven režim Pøíjem více terminály
        /// </summary>
        /// <param name="cislodavky">èíslo dávky k uzevøení</param>
        private void prijemuzavritdavku(int cislodavky)
        {
            try
            {
                if (!Globals.ViceTerminaly)
                    return;

                if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainUzavritDavkuDotaz, cislodavky), Fask.Localization.Localization.Prijem4PrijemMainPrijemka, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                    return;

                string pswd = string.Empty;

                if (InputBox.Show(Fask.Localization.Localization.Prijem4PrijemMainZadejteHeslo, pswd, out pswd) != DialogResult.OK)
                    return;

                PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.FinishPrijemka(MST_Global.TerminalID, cislodavky.ToString(), pswd);

                if (so.StatusText == "OK" && !so.Exception)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainPrijemkaUspesneUzavrena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                }
                else
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainNastalaChyba, so.StatusText), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Prijem4PrijemMainUzavreniPrijemky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// akce pro kliknuti na tlacitko stahni davku
        /// </summary>
        private void stahniDavku_but_Click(object sender, EventArgs e)
        {
            stahniDavku();
        }

        /// <summary>
        /// Akce stažení dávky ze serveru
        /// </summary>
        private void stahniDavku()
        {
            try
            {
                string pdlDavka;
                string filename;
                bool result = stahniDavku(out pdlDavka, out filename);

                if (result && Prijem_4.Globals.DavkaOtevritIhnedPoStazeni) // TODO: Konfigurace
                {
                    if (pdlDavka.StartsWith("S"))
                    {
                        // JoZ: seznam davek ve sloucene davce
                        using (ListSlouceneDavkyForm listSlouceneForm = new ListSlouceneDavkyForm(filename))
                        {
                            string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);
                            listSlouceneForm.FileNames = fileNames;
                            if (listSlouceneForm.ShowDialog() == DialogResult.Cancel)
                                return;
                        }
                    }

                    zpracujDavku(pdlDavka);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }
        /// <summary>
        /// stahne seznam davek ze serveru
        /// </summary>
        /// <returns>true - OK, false - chyba</returns>
        private bool stahniDavku(out string pdlDavka, out string filenam)
        {
            
            pdlDavka = string.Empty;
            filenam = string.Empty;

            PrijemService.PrijemDavky prijemdavky = PrijemServiceOperations.GetHeads(Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem);
            if (prijemdavky == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniSeznamuDavek, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }


            string vybranadavka = string.Empty;
            PrijemService.PrijemDavky.HlavickyRow hr = null;

            //Odstraneni exitujicich souboru z hlavicek
            try
            {
                string[] filenames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);
                foreach (string filename in filenames)
                {
                    PrijemService.PrijemDavky.HlavickyRow[] hrows =
                        (PrijemService.PrijemDavky.HlavickyRow[])prijemdavky.Hlavicky.Select("CountEntries='" + Path.GetFileNameWithoutExtension(filename)+"'");
                    foreach (PrijemService.PrijemDavky.HlavickyRow hrow in hrows)
                    {
                        hrow.Delete();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return false;
            }
            
            prijemdavky.AcceptChanges();
            // Zapise schema do souboru pokud se pocet sloupcu lisi.
            Hlavicky.WriteSchemaWithDynamicColumns(prijemdavky);

            using (PrijemDavkyList pdl = new PrijemDavkyList(prijemdavky, true, Prijem_4.Globals.GenerovatNenalezenouPrijemku, Prijem_4.Globals.NezrealizovanePrijemky))
            {
                if (pdl.ShowDialog() == DialogResult.Cancel)
                    return false;

                vybranadavka = pdl.Davka;
                hr = pdl.SelectedRow;
                hr.Sloucena = false;

                pdlDavka = pdl.Davka;
                filenam = pdl.FileName;
            }

            if (!PrijemServiceOperations.GetData(Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem, int.Parse(vybranadavka)))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniDatDavky, Fask.Localization.Localization.Prijem4PrijemMainStazeniDat);
                return false;
            }
            else
            {
                try
                {
                    Hlavicky.HlavickaAdd(hr);

                    ////zpracujDavku(vybranadavka); zruseno kvuli uprave a podobnosti s vydejem...
                    string pdlDavkaTMP = pdlDavka;
                    string filenamTMP = filenam;

                    if (!Slouceni(hr, out pdlDavka, out filenam))
                    {
                        pdlDavka = pdlDavkaTMP;
                        filenam = filenamTMP;
                    } 

                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    if ((ex.Message == @"The database file cannot be found. Check the path to the database. [ Data Source = \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]")
                        || (ex.Message == @" [ \Application\mst_w_6.1_alfa_test\Data\Sklady.sdf ]"))
                        MessageBoxBigTimeout.Show("Nenalzen èíselník 'Sklady.sdf'." + Environment.NewLine + "Pred pokraèovaním zaktualizujte èíselníky.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    else
                        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Importovani dat ze zdroje do cile
        /// </summary>
        /// <param name="sourceFilePath">zdrojova davka</param>
        /// <param name="destinationFilePath">cilova davka (sloucena)</param>
        /// <param name="countEntries">cislo davky dat pro prekopirovani</param>
        public void ImportDataFromTo(string sourceFilePath, string destinationFilePath, string countEntries)
        {
            #region old import pe
            //public void ImportPEDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, string countEntries)
            //{
            //    using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter peta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter())
            //    {
            //        peta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath);

            //        peta.Connection.Open();
            //        Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable pedt = peta.GetDataByCountEntries(int.Parse(countEntries));
            //        peta.Connection.Close();

            //        foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow row in pedt)
            //            row.SetAdded();

            //        peta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath);
            //        peta.Connection.Open();
            //        peta.Update(pedt);
            //        peta.Connection.Close();
            //    }
            //    //peta.Dispose();
            //}
            #endregion
            #region old import pi
            //public void ImportPIDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, string countEntries)
            //{
            //    using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pita = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter())
            //    {
            //        pita.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath);

            //        pita.Connection.Open();
            //        Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable pidt = pita.GetDataByCountEntries(int.Parse(countEntries));
            //        pita.Connection.Close();

            //        foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow row in pidt)
            //            row.SetAdded();

            //        pita.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath);
            //        pita.Connection.Open();
            //        pita.Update(pidt);
            //        pita.Connection.Close();
            //    }
            //    //pita.Dispose();
            //}
            #endregion
            #region old import parametry
            //public void ImportParametryDataFromTo(string sourceFilePath, string destinationFilePath)
            //{
            //    using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter seta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter())
            //    {
            //        seta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath);

            //        seta.Connection.Open();
            //        Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable sedt = seta.GetData();
            //        seta.Connection.Close();

            //        foreach (Fask.SQLiteDBs.DataSets.Prijem.ParametryRow row in sedt)
            //            row.SetAdded();

            //        seta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath);
            //        seta.Connection.Open();
            //        seta.Update(sedt);
            //        seta.Connection.Close();
            //    }
            //    //seta.Dispose();
            //}
            #endregion

            // Mely byse importovat i ostatni tablky, ktere jsou soucasti prijmove predlohy
            using (var controller_prijem_source = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(sourceFilePath))
            using (var controller_prijem_destin = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(destinationFilePath))
            {
                int countEntriesInt = Convert.ToInt32(countEntries);
                IDbTransaction transaction = null;
                try
                {
                    controller_prijem_source.Connection_Open();
                    controller_prijem_destin.Connection_Open();

                    transaction = controller_prijem_destin.Connection.BeginTransaction();

                    // PE
                    using (var dt = controller_prijem_source.GetDataByCountEntries_PE(countEntriesInt))
                    {
                        dt.ToList().ForEach(x => x.SetAdded());
                        controller_prijem_destin.Update_PE(dt);
                    }

                    // PE_SN
                    using (var dt = controller_prijem_source.GetData_PE_SN())
                    {
                        dt.Where(x => x.CountEntries != countEntriesInt).ToList().ForEach(x => x.Delete());
                        dt.AcceptChanges();
                        dt.ToList().ForEach(x => x.SetAdded());
                        controller_prijem_destin.Update_PE_SN(dt);
                    }
                    
                    // PI
                    using (var dt = controller_prijem_source.GetDataByCountEntries_PI(countEntriesInt))
                    {
                        dt.ToList().ForEach(x => x.SetAdded());
                        controller_prijem_destin.Update_PI(dt);
                    }

                    // Parametry
                    using (var dt = controller_prijem_source.GetData_Param())
                    {
                        dt.ToList().ForEach(x => x.SetAdded());
                        controller_prijem_destin.Update_Param(dt);
                    }

                    transaction.Commit();
                    transaction = null;
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (transaction != null)
                            transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Logging.Log.Write(exRollback);
                    }
                    throw ex;
                }
                finally
                {
                    controller_prijem_source.Connection_Close();
                    controller_prijem_destin.Connection_Close();
                }
            }
        }

        /// <summary>
        /// Provede slouceni stazene davky/zvolene davky do sloucene davky
        /// </summary>
        /// <param name="chosenRow"></param>
        /// <param name="pdlDavka"></param>
        /// <param name="filenam"></param>
        /// <returns></returns>
        private bool Slouceni(PrijemService.PrijemDavky.HlavickyRow chosenRow, out string pdlDavka, out string filenam) //, out string pdlDavka)
        {
            pdlDavka = string.Empty;
            filenam = string.Empty;

            if (!Globals.SlucovaniDavek)
                return false;

            if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainSloucitDavkuDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
                return false;

            string fileName = Path.Combine(MST_Global.Storage, "S1" + "." + Main.Ext_Prijem);

            if (!File.Exists(fileName))
            { //vytvorime novy datatemplate...
                string srcFile = Path.Combine(Main.SQLiteDBsDir, "Prijem.prd");
                File.Copy(srcFile, fileName, false);

                //a radek do seznamu hlavicek...
                PrijemService.PrijemDavky d = new Fask.MST_W.PrijemService.PrijemDavky();
                PrijemService.PrijemDavky.HlavickyRow drow = d.Hlavicky.AddHlavickyRow("S1", "0", "", 0, 0, false);
                Hlavicky.HlavickaAdd(drow);
            }

            string sourceFilePath = Path.Combine(MST_Global.Storage, chosenRow.CountEntries + "." + Main.Ext_Prijem);

            //Prijem_4.PrijemMain.prijemInstance.ImportPEDataByCountEntriesFromTo(sourceFilePath, fileName, chosenRow.CountEntries);
            //Prijem_4.PrijemMain.prijemInstance.ImportPIDataByCountEntriesFromTo(sourceFilePath, fileName, chosenRow.CountEntries);
            //Prijem_4.PrijemMain.prijemInstance.ImportParametryDataFromTo(sourceFilePath, fileName);
            Prijem_4.PrijemMain.prijemInstance.ImportDataFromTo(sourceFilePath, fileName, chosenRow.CountEntries);

            Hlavicky.SetSloucena(chosenRow, true);

            pdlDavka = "S1";
            filenam = Path.Combine(Main.StorageDir, pdlDavka + "." + Main.Ext_Prijem);

            return true;
        }


        /// <summary>
        /// akce kliku na zpracuj davka : nalezeni a zobrazeni davek prijemek na disku
        /// </summary>
        private void buttonDavka_Click(object sender, EventArgs e)
        {
            zpracujDavku();
        }

        private void zpracujDavku()
        {
            try
            {
                // najde vsechny davky na disku
                string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);
                //string fileName;
                if (fileNames.Length == 0)
                {
                    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainNaDiskuNejsouDavkyStahnoutDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                        == DialogResult.No)
                        return;
                    else
                    {
                        string fname = string.Empty;
                        string pdl2Davka = string.Empty;

                        //if(!Prijem_4.Globals.DavkaOtevritIhnedPoStazeni)
                        //{
                        //    if (!stahniDavku(out fname, out pdl2Davka))
                        //        return;
                        //} 
                        //else
                        //{
                        //    stahniDavku_but_Click(null, null);
                        //    return;
                        //}

                        // pokud se chce davka po stazeni ihned otervrit, tak jinak
                        if (Prijem_4.Globals.DavkaOtevritIhnedPoStazeni)
                        {
                            //this.BeginInvoke((Action)delegate() { this.stahniDavku_but_Click(null, null); });
                            this.BeginInvoke((Action)delegate() { this.stahniDavku(); });
                            return;
                        }
                        // jinak se stahne seznam a zobrazi se ...
                        // po uspesnem stazeni se pokracuje do vyberu davek k otervreni lokalne
                        if (!stahniDavku(out fname, out pdl2Davka))
                            return;
                    }
                }

                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                string pdlDavka = "";
                Hlavicky.Synchronize();
                string filename = "";
                string vybranadavka;
                PrijemService.PrijemDavky.HlavickyRow hr = null;

                using (PrijemDavkyList pdl = new PrijemDavkyList(Hlavicky.Davky, false, Prijem_4.Globals.GenerovatNenalezenouPrijemku, Prijem_4.Globals.NezrealizovanePrijemky))
                {
                    Cursor.Current = Cursors.Default;
                    if (pdl.ShowDialog() == DialogResult.Cancel) return;
                    pdlDavka = pdl.Davka;
                    filename = pdl.FileName;
                    vybranadavka = pdl.Davka;

                    hr = pdl.SelectedRow;
                    hr.Sloucena = false;
                }

                // stahnuti davky, pokud jeste neexistuje
                string filepath = Path.Combine(Main.StorageDir, pdlDavka + "." + Main.Ext_Prijem);
                if (!File.Exists(filepath))
                {
                    if (!PrijemServiceOperations.GetData(Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem, int.Parse(vybranadavka)))
                    {
                        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniDatDavky, Fask.Localization.Localization.Prijem4PrijemMainStazeniDat);
                        return;
                    }
                    else // pokud se podarilo stahnout a davka se nema otevrit ihned po stazeni
                        if (!Prijem_4.Globals.DavkaOtevritIhnedPoStazeni)
                        {
                            //buttonDavka_Click(null, null);
                            this.BeginInvoke((Action)delegate() { zpracujDavku(); });
                            return;
                        }
                }

                if (pdlDavka.StartsWith("S"))
                {
                    // JoZ: seznam davek ve sloucene davce
                    using (ListSlouceneDavkyForm listSlouceneForm = new ListSlouceneDavkyForm(filename))
                    {
                        fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem);
                        listSlouceneForm.FileNames = fileNames;
                        if (listSlouceneForm.ShowDialog() == DialogResult.Cancel)
                            return;
                    }
                }

                zpracujDavku(pdlDavka);
            }
            catch (Exception exZpracujDavku)
            {
                Logging.Log.Write(exZpracujDavku);
                MessageBoxBig.Show(exZpracujDavku.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
            }
        }

        /// <summary>
        /// Hlavni kod pro otevreni davky a zpracovani ... 
        /// </summary>
        /// <param name="davka"></param>
        private void zpracujDavku(string davka)
        {
            if (MST_Global._MemoryChecked)
            {
                //long mem = GC.GetTotalMemory(true);
                MySystem.Memory.MEMORYSTATUS status = new Fask.MST_W.MySystem.Memory.MEMORYSTATUS();
                MySystem.Memory.GlobalMemoryStatus(ref status);

                uint mem = status.dwAvailVirtual;


                long mem_size_MiB = long.Parse(MST_Global._MemorySize);
                uint mem_size_KiB = (uint)(mem_size_MiB * 1024 * 1024);

                if (mem < mem_size_KiB)
                {
                    MessageBoxBig.Show(string.Format("Nelze spravovat davku,\n Malo pameti: '{0}MiB'", (mem / 1024) / 1024), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

                    return;
                }
            }

            try
            {
                //nastaveni objektu pro davku 
                this.globalObject.Davka = davka;

                #region 1.Existujici zaznam z PI
                string di_sklid = null;

                //SqlCEDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pi_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
                //pi_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(Main.StorageDir, davka + "." + Main.PrijemExt));
                #region old code zjisteni pouziteho id skladu
                //using (System.Data.SQLite.SQLiteCommand scecommand = pi_ta.Connection.CreateCommand())
                //{
                //    try
                //    {
                //        //scecommand = pi_ta.Connection.CreateCommand();
                //        scecommand.CommandText = "Select * from czmst_pi";
                //        scecommand.Connection.Open();
                //        System.Data.SQLite.SQLiteDataReader sdr = scecommand.ExecuteReader();

                //        if (sdr.Read())
                //        {
                //            di_sklid = Convert.ToString(sdr["SKL_ID"] is System.DBNull ? string.Empty : sdr["SKL_ID"]).Trim();
                //        }

                //        // nenacitat z czmst_se, muze se prepisovat sklad (MST_Global.PrevzitIDSkladuZCiselnikuSkladu)
                //        //if (String.IsNullOrEmpty(di_sklid))
                //        //{ // nebylo nalezeno v PI, tak se podivam do PE ...
                //        //    scecommand.CommandText = "Select * from czmst_pe";
                //        //    sdr = scecommand.ExecuteReader();
                //        //    if (sdr.Read())
                //        //    {
                //        //        di_sklid = Convert.ToString(sdr["SKL_ID"] is System.DBNull ? string.Empty : sdr["SKL_ID"]).Trim();
                //        //    }
                //        //}

                //    }
                //    catch (Exception ex)
                //    {
                //        Logging.Log.Write(ex);
                //    }
                //    finally
                //    {
                //        if (scecommand != null && scecommand.Connection.State == ConnectionState.Open)
                //            scecommand.Connection.Close();
                //    }
                //}
                #endregion

                var firsPiRow = this.globalObject.controller_prijem.CZMST_PI_Get_One(0);
                if (firsPiRow != null)
                    di_sklid = firsPiRow.IsSKL_IDNull() ? string.Empty : firsPiRow.SKL_ID.Trim();

                #endregion

                try
                {
                    #region vyber skladu
                    // zadavani skladu, pokud je povolen
                    Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
                    if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
                    {
                        // prevzeti vybraneho skladu pri otevirani davky, pokud je zvolen
                        sklad = this.globalObject.sklad;

                        // CHECK : tato sekvence je nejaka divna ... 

                        // nacteni id skladu z jiz nactenych dat, pokud je pouze jedno id skladu na davku/prijemku
                        //if (di_sklid != null && di_sklid != string.Empty && Prijem_4.Globals.FiltrCiselnikSkladuOnlyOne)
                        if (sklad == null && !string.IsNullOrEmpty(di_sklid) && Prijem_4.Globals.FiltrCiselnikSkladuOnlyOne)
                        {
                            try
                            {
                                #region old code pro zjisteni skladu
                                //using (Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter())
                                //{
                                //    ta_sklad.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikSkladyDB);
                                //    Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(di_sklid);
                                //    if (dt_sklady.Count > 0)
                                //        sklad = dt_sklady[0];
                                //    else
                                //    {
                                //        MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                //    }
                                //}
                                #endregion

                                var dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(di_sklid);
                                if (dt_sklady.Count > 0)
                                    sklad = dt_sklady[0];
                                else
                                {
                                    MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log.Write(ex);
                                MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                return;
                            }
                        }

                        if (sklad == null && !string.IsNullOrEmpty(Prijem_4.Globals.SkladID))
                        {
                            try
                            {
                                #region old code
                                //using (Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter())
                                //{
                                //    ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
                                //    Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(Prijem_4.Globals.SkladID);
                                //    if (dt_sklady.Count > 0)
                                //        sklad = dt_sklady[0];
                                //    else
                                //    {
                                //        MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                //    }
                                //}
                                #endregion

                                var dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(Prijem_4.Globals.SkladID);
                                if (dt_sklady.Count > 0)
                                    sklad = dt_sklady[0];
                                else
                                {
                                    MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                                }

                            }
                            catch (Exception ex)
                            {
                                Logging.Log.Write(ex);
                                MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                return;
                            }
                        }

                        if (sklad == null)
                        {
                            using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                            {
                                if (fsv.ShowDialog() == DialogResult.Cancel)
                                    return;

                                sklad = fsv.Sklad;

                                if (sklad == null)
                                {
                                    Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    // nacist prijemparams
                    // urcit, zdali je povolena prijmova lokace a nasledne umoznit vyber ...
                    Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry = new Fask.SQLiteDBs.DataSets.Prijem();
                    #region old code
                    //using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
                    //    "Select * from Parametry",
                    //    "Data source=" + Path.Combine(Main.StorageDir, davka + "." + Main.PrijemExt)
                    //    ))
                    //{

                    //    try
                    //    {
                    //        sda.Fill(prijemDataParametry, prijemDataParametry.Parametry.TableName);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Logging.Log.Write(ex);
                    //        MessageBoxBig.Show(ex.ToString());
                    //        return;
                    //    }
                    //}
                    #endregion
                    this.globalObject.controller_prijem.Fill_Param(prijemDataParametry.Parametry);

                    // vyber prijmove lokace
                    Fask.MST_W.LokaceService.Location.CZMST_SkladLokace_MapaRow prijmovalokace = null;
                    if (prijemDataParametry.Parametry.Count > 0 && !prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_PRIJMOVANull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA)
                    {
                        using (PrijemVyberPrijmoveLokaceList pvp = new PrijemVyberPrijmoveLokaceList(sklad))
                        {
                            if (pvp.ShowDialog() == DialogResult.Cancel)
                                return;

                            prijmovalokace = pvp.PrijmovaLokace;
                        }
                    }


					if (MST_Global.VydejTypOznaceniPalety)
					{
						//rez_1 = string.Empty;
						using (TypOznaceniPaletyForm typoznpal = new TypOznaceniPaletyForm())
						{
							if (typoznpal.ShowDialog() == DialogResult.Cancel)
								return;
							rez_1 = typoznpal.TypOznaceni;
						}

					}



                    Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                    //using (PrijemList pl = new PrijemList(davka, sklad, prijemDataParametry, prijmovalokace))
                    using (PrijemList pl = new PrijemList(sklad, prijemDataParametry, prijmovalokace))
                    {
						if (rez_1 == null)
							pl.Paleta = new Fask.MST_W.Classes.Paleta();
						else
							pl.Paleta = rez_1;

						//pl.Paleta = (rez_1 == null ? null : rez_1);
                        Cursor.Current = Cursors.Default;
                        pl.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                }
            }
            finally
            {
                // uzavreni objektu davky
                this.globalObject.Davka = null;
            }
        }

        /// <summary>
        /// akce na stisk klavesy pri focusu na buttonu
        /// </summary>
        private void buttonDavka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.buttonDavka_Click(null, null);
                this.zpracujDavku();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// akce na stisk klavesy pri focusu na buttonu
        /// </summary>
        private void odesliHotovouDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.odesliHotovouDavku_but_Click(null, null);
                this.odesliHotovouDavku();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// akce na stisk klavesy pri focusu na buttonu
        /// </summary>
        private void vratitDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.vratitDavku_but_Click(null, null);
                vratitDavku();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// akce na stisk klavesy pri focusu na buttonu
        /// </summary>
        private void stahniDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.stahniDavku_but_Click(null, null);
                this.stahniDavku();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// akce na stisk klavesy pri focusu na buttonu
        /// </summary>
        private void buttonKonec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.buttonKonec_Click(null, null);
                PerformEnd();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        /// <summary>
        /// akce pri aktivaci formulare. aktivuje se rozlozeni klavesnice v defaultnim modu
        /// </summary>
        private void PrijemMain_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        /// <summary>
        /// akce pri deaktivaci formulare, dojde k ulozeni akutalniho nastaveni pro toto okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrijemMain_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        /// <summary>
        /// Synchronizace obrazku na server
        /// </summary>
        private void ImagesSynchronize()
        {
            try
            {
                string[] images = Directory.GetFiles(Main.ImagesDir);

                foreach (var imageFile in images)
                {
                    //lock (SejmiImageForm.ImageLockObject)
                    //{
                    //byte[] imageData = MySystem.FileOperations.DBLoad(imageFile);

                    //read imageData / if not locked ... 
                    byte[] imageData;
                    {
                        FileStream fs = null;
                        //byte[] data = new byte[0];
                        try
                        {
                            fs = new FileStream(
                                imageFile,
                                FileMode.Open,
                                FileAccess.ReadWrite,
                                FileShare.None);
                            long fileLen = (new FileInfo(imageFile)).Length;
                            imageData = new byte[fileLen];
                            int bytesRead = fs.Read(imageData, 0, (int)fileLen);
                        }
                        finally
                        {
                            if (fs != null)
                            {
                                fs.Close();
                                fs = null;
                            }
                        }
                    }

                    ServisModuleWService.StatusObject so = this.globalObject.service_serviceModule.ImageArchivate(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(imageFile), imageData);
                    if (so.StatusText == "OK")
                    {
                        File.Delete(imageFile);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        /// <summary>
        /// Ukoncovani dialogu => destrukce globalnich objektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrijemMain_Closing(object sender, CancelEventArgs e)
        {
            prijemInstance = null;
            if (globalObject != null)
            {
                this.globalObject.sklad = null;
                this.globalObject.Dispose();
                this.globalObject = null;
            }
        }

    }
}