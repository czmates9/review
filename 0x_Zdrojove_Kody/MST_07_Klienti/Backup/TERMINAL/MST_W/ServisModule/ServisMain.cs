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

namespace Fask.MST_W.ServisModule
{
    // TODO : upravit design formulare ... 
    // - pridat status bar?
    // - zobrazit vybrany sklad
    // - nejake menu?
    // - nejak jinak udelat?

    public partial class ServisMain : System.Windows.Forms.Form
    {
        internal static ServisMain servisI = null;
        //public _WebRefernces_Globals.ServisModuleWServiceSession servisS = null;
        //public string sqlfilename = string.Empty;
        //public _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;

        public ServisMain()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        private void ServisMain_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;
                try
                {
					//servisS = new _WebRefernces_Globals.ServisModuleWServiceSession();
					//servisS.Url = MST_Global.ServerAddress + "Servis.asmx";
					//servisS.Timeout = MST_Global.ServiceTimeOut;
					//servisS.UpdateWebServiceCredentials();

                    servisI = this;

					//ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
					//ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
					//ciselnikS.Timeout = MST_Global.ServiceTimeOut;
					//ciselnikS.UpdateWebServiceCredentials();

					//System.Data.SQLite.SQLiteConnection sqlConnection = null;

					//sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.globalObject .ServisCiselnikDB);
					//Globals._taDynTabDef.Connection = sqlConnection;

					//Globals._webServiceModule = new Fask.MST_W._WebRefernces_Globals.ServisModuleWServiceSession();
					//Globals._webServiceModule.Url = MST_Global.ServerAddress + "Servis.asmx";
					//Globals._webServiceModule.Timeout = MST_Global.ServisTimeoutSynchronize;
					//Globals._webServiceModule.UpdateWebServiceCredentials();
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Prijem4PrijemMainChyba);
                    this.Close();
                }

                Hlavicky.LoadHlavicky();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformEnd();
        }

        private void PerformEnd()
        {
            if (MST_Global.Servis_DialogOpusteniModulu)
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainMenuNavratDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
            == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void PrijemMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PerformEnd();
            }
            else if (e.KeyCode == Keys.D1)
            {
                buttonDavka_Click(null, null);
            }
            else if (e.KeyCode == Keys.D2)
            {
                stahniDavku_but_Click(null, null);
            }
            else if (e.KeyCode == Keys.D3)
            {
                odesliHotovouDavku_but_Click(null, null);
            }
            else if (e.KeyCode == Keys.D4)
            {
                vratitDavku_but_Click(null, null);
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        //public List<string> GetSloucenaDavkaListDavek(string vybranaDavkaFileName)
        //{
        //    List<string> seznamDavek = new List<string>();
        //    string cisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);
        //    if (cisloDavky.StartsWith("S"))
        //    {
        //        Fask.SQLiteDBs.DataSets.PrijemTableAdapters.SlouceneTableAdapter st = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.SlouceneTableAdapter();
        //        st.Connection.ConnectionString = "Data source=" + vybranaDavkaFileName;
        //        st.Connection.Open();
        //        Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable dtS = st.GetData();
        //        st.Connection.Close();

        //        foreach (Fask.SQLiteDBs.DataSets.Prijem.SlouceneRow row in dtS)
        //        {
        //            string fileName = System.IO.Path.Combine(Main.StorageDir, row.CountEntries.ToString() + "." + Main.PrijemIExtData);
        //            if (!seznamDavek.Contains(fileName))
        //                seznamDavek.Add(fileName);
        //        }
        //    }
        //    else
        //        seznamDavek.Add(vybranaDavkaFileName);

        //    return seznamDavek;
        //}

        //public void ParseSloucenaDavka(string selectedDavkaFilePath, string countentries)
        //{
        //    string cisloDavky = Path.GetFileNameWithoutExtension(selectedDavkaFilePath);

        //    if (cisloDavky.StartsWith("S"))
        //    {

        //        Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dtPI = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();

        //        Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter setaSource = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
        //        setaSource.Connection.ConnectionString = "Data source=" + selectedDavkaFilePath;
        //        setaSource.Connection.Open();
        //        dtPI = setaSource.GetDataByCountEntries(int.Parse(Path.GetFileNameWithoutExtension(countentries)));
        //        setaSource.Connection.Close();

        //        foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow row in dtPI)
        //        {
        //            row.SetAdded();
        //        }

        //        setaSource.Connection.ConnectionString = "Data source=" + countentries;
        //        setaSource.Connection.Open();

        //        setaSource.DeleteQuery();

        //        setaSource.Update(dtPI);

        //        if (setaSource.Connection.State == ConnectionState.Open)
        //            setaSource.Connection.Close();
        //    }
        //}

        private void vratitDavku_but_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
            //string cisloDavky = string.Empty;
            string vybranaDavkaFileName = string.Empty;
            Hlavicky.Synchronize();
            using (ServisDavkyList pdl = new ServisDavkyList(Hlavicky.Davky, false))
            {
                Cursor.Current = Cursors.Default;
                if (pdl.ShowDialog() == DialogResult.Cancel) return;

                if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainVratitDavkuDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;
                vybranaDavkaFileName = pdl.FileName;
                //cisloDavky = pdl.Davka;
            }
            string actualCisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);

            //Globals.Davka = actualCisloDavky;
			Globals.globalObject.Davka = Convert.ToInt32(actualCisloDavky);

            // kontrola, zdali neni jiz zorpracovana ...
            //System.Data.SQLite.SQLiteConnection sqlConnection = null;
            //sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojeStavDB);
            //Globals._taZdrojeStavTmp.Connection = sqlConnection;

			int? rozpracovano = null;
			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav ConSer = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(Path.Combine(Main.StorageDir, Globals.globalObject.Davka + "." + Main.Ext_ServisI)))
			{
				//ConSer.Connection = new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat());
				rozpracovano = ConSer.CountRozpracovano_ZdrojStavTmp();
			}

            if (rozpracovano != null && rozpracovano.HasValue && rozpracovano.Value > 0)
            {
                MessageBoxBig.Show("Vybranou dávku '" + actualCisloDavky + "' není možné vrátit, jelikož je již rozpracováná", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            //string cisloDavky = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);

            //seznam davek ve sloucene davce
            //filenames = GetSloucenaDavkaListDavek(vybranaDavkaFileName);


            //bool deleteSloucenaDavka = true;
            //for (int i = 0; i < filenames.Count; i++)
            //{
            //    //import dat ze sloucene davky do jednotlivych davek...
            //    ParseSloucenaDavka(vybranaDavkaFileName, filenames[i]);
            //    string actualCisloDavky = Path.GetFileNameWithoutExtension(filenames[i]);

            //    System.Data.SqlServerCe.SqlCeCommand scecommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select Count(*) From CZMST_PI",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + filenames[i])
            //    );

            //    object result = null;

            //    try
            //    {
            //        scecommand.Connection.Open();
            //        result = scecommand.ExecuteScalar();
            //    }
            //    finally
            //    {
            //        if (scecommand.Connection.State == ConnectionState.Open)
            //            scecommand.Connection.Close();
            //    }

            //    // nacteni parametru
            //    System.Data.SqlServerCe.SqlCeDataAdapter paramsadapter = new System.Data.SqlServerCe.SqlCeDataAdapter(
            //        "Select * From Parametry",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + filenames[i])
            //    );
            //    Fask.SQLiteDBs.DataSets.Prijem paramsdata = new Fask.SQLiteDBs.DataSets.Prijem();

            //    paramsadapter.Fill(paramsdata, paramsdata.Parametry.TableName);

            //    if (result != null && ((int)result) > 0)
            //    //if (vydejData.CZMST_SI.Rows.Count > 0)
            //    {
            //        if (paramsdata.Parametry.Count > 0 && !paramsdata.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && paramsdata.Parametry[0].CONFIG_LOKACE_POVOLIT)
            //        {
            //            MessageBoxBig.Show("Dávku nelze vrátit, protože obsahuje nasnímané položky!", "Dotaz", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            //            return;
            //        }
            //        else
            //            if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainVratitDavkuPolozkyAnulovatDotaz,
            //                Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
            //                return;
            //    }
            //    //nic mazat nechci, vracim to co jsem ulozil, server si s tim poradi,\
            //    //musi, protoze mohou byt online zapisy, ktere se musi odmazat ... !!!
            //    //// vymaze veskere vydane polozky
            //    ////vydejData.RejectChanges();
            //    //scecommand.CommandText = "Delete from czmst_pi";
            //    //try
            //    //{
            //    //    scecommand.Connection.Open();
            //    //    scecommand.ExecuteNonQuery();
            //    //}
            //    //finally
            //    //{
            //    //    if (scecommand.Connection.State == ConnectionState.Open)
            //    //        scecommand.Connection.Close();
            //    //}

            //    Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemMainPrijemkaDataNacitani);
            //    byte[] dataToUpload = new byte[0];
            //    try
            //    {
            //        dataToUpload = MySystem.FileOperations.DBLoad(filenames[i]);
            //    }
            //    catch (Exception ex)
            //    {
            //        Program.mstw.mbw.EndPracujiForm();
            //        MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaCteniDavky + ex.Message);
            //        return;
            //    }
            //    finally
            //    {
            //        Program.mstw.mbw.EndPracujiForm();
            //    }

                //Program.mstw.mbw.BeginPracujiForm("Odesílají se data pøíjemky");

                if (odesliDavku(actualCisloDavky, true))
                {

                    //DeleteDataFromSloucena(vybranaDavkaFileName, cisloDavky, actualCisloDavky);

                    Hlavicky.HlavickaDelete(actualCisloDavky);
                    File.Delete(actualCisloDavky);
                }
                //else
                //{
                //    deleteSloucenaDavka = false;
                //}

            //}
            //if(deleteSloucenaDavka)
            //    SmazatSloucenaDavka(vybranaDavkaFileName, cisloDavky);

         
        }


        //private void SmazatSloucenaDavka(string vybranaDavkaFileName, string cisloDavky)
        //{
        //    if (cisloDavky.StartsWith("S"))
        //    {
        //        if ((GetCountPEData(vybranaDavkaFileName) <= 0) && (GetCountPIData(vybranaDavkaFileName) <= 0))
        //        {
        //            Hlavicky.HlavickaDelete("S1");
        //            File.Delete(vybranaDavkaFileName);
        //        }
        //    }
        //}

        //private void DeleteDataFromSloucena(string vybranaDavkaFileName, string cisloDavky, string countEntries)
        //{
        //    if (cisloDavky.StartsWith("S"))
        //    {
        //        DeletePEData(vybranaDavkaFileName, countEntries);
        //        DeletePIData(vybranaDavkaFileName, countEntries);
        //    }
        //}


        //public void DeletePIData(string sloucenaDavka, string countEntries)
        //{
        //    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter setaSource = null;

        //    try
        //    {
        //        setaSource = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
        //        setaSource.Connection.ConnectionString = "Data source=" + sloucenaDavka;
        //        setaSource.Connection.Open();
        //        setaSource.DeleteQueryByCountEntries(int.Parse(countEntries));
        //    }
        //    finally
        //    {
        //        if (setaSource.Connection.State == ConnectionState.Open)
        //            setaSource.Connection.Close();
        //    }
        //}

        //public void DeletePEData(string sloucenaDavka, string countEntries)
        //{

        //    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter setaSource = null;
        //    try
        //    {
        //        setaSource = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
        //        setaSource.Connection.ConnectionString = "Data source=" + sloucenaDavka;
        //        setaSource.Connection.Open();
        //        setaSource.DeleteQuery(int.Parse(countEntries));
        //    }
        //    finally
        //    {
        //        if (setaSource.Connection.State == ConnectionState.Open)
        //            setaSource.Connection.Close();
        //    }
        //}

        //public int GetCountPEData(string sloucenaDavka)
        //{
        //    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter setaSource = null;
        //    try
        //    {
        //        setaSource = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
        //        setaSource.Connection.ConnectionString = "Data source=" + sloucenaDavka;
        //        setaSource.Connection.Open();
        //        int i = (int?)setaSource.CountQuery() ?? 0;
        //        return i;
        //    }
        //    finally
        //    {
        //        if (setaSource.Connection.State == ConnectionState.Open)
        //            setaSource.Connection.Close();
        //    }
        //}


        //public int GetCountPIData(string sloucenaDavka)
        //{
        //    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter setaSource = null;
        //    try
        //    {
        //        setaSource = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
        //        setaSource.Connection.ConnectionString = "Data source=" + sloucenaDavka;
        //        setaSource.Connection.Open();
        //        int i = (int?)setaSource.CountQuery() ?? 0;
        //        return i;
        //    }
        //    finally
        //    {
        //        if (setaSource.Connection.State == ConnectionState.Open)
        //            setaSource.Connection.Close();
        //    }
        //}

        private void odesliHotovouDavku_but_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                string pdlDavka = "";
                Hlavicky.Synchronize();

                List<string> filenames = new List<string>();
                string vybranaDavkaFileName;
                //bool deleteSloucenaDavka = true;
                using (ServisDavkyList pdl = new ServisDavkyList(Hlavicky.Davky, false))
                {
                    Cursor.Current = Cursors.Default;
                    if (pdl.ShowDialog() == DialogResult.Cancel) return;
                    pdlDavka = pdl.Davka;
                    vybranaDavkaFileName = pdl.FileName;
                }

                //seznam davek ve sloucene davce
                //filenames = GetSloucenaDavkaListDavek(vybranaDavkaFileName);
                string countEntries = Path.GetFileNameWithoutExtension(vybranaDavkaFileName);
                //for (int i = 0; i < filenames.Count; i++)
                //{
                //    Logging.Log.Write("ParseSloucenaDavka : " + filenames[i], "Prijem.odesliHotovouDavku_but_Click");
                //    //import dat ze sloucene davky do jednotlivych davek...
                //    ParseSloucenaDavka(vybranaDavkaFileName, filenames[i]);

                //    /* ToDo: kontrola dokoncenosti...
                //    using (PrijemList lp3 = new PrijemList(filenames[i]))
                //    {
                //        //if (!stavVydeje(filename))
                //        if (!lp3.kontrolaDokoncenosti())
                //        {
                //            if (Messaa geBoxBig.Show("Tato dávka ješte není dokonèena. Opravdu ji chcete odeslat?",
                //                "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
                //                return;
                //        }
                //    }
                //    */

                //    string countEntries = Path.GetFileNameWithoutExtension(filenames[i]);
                    Logging.Log.Write("Odesilani davky=" + countEntries, "Prijem.Vydej.odesliHotovouDavku_but_Click");
                    if (odesliDavku(countEntries, false))
                    {
                        //Logging.Log.Write("DeleteDataFromSloucena=" + vybranaDavkaFileName + " : cisloDavky=" + pdlDavka + " : countEntries=" + countEntries, "Vydej_3.Vydej.odeslatDavku");
                        //DeleteDataFromSloucena(vybranaDavkaFileName, pdlDavka, countEntries);
                    }
                //    else
                //        deleteSloucenaDavka = false;
                //}
                //if (deleteSloucenaDavka)
                //    SmazatSloucenaDavka(vybranaDavkaFileName, pdlDavka);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show("odesliHotovouDavku_but_Click:" + ex.Message);
                Logging.Log.Write(ex, ex.StackTrace);
            }
        }

        public bool odesliDavku(string vybranadavka, bool uvolnit)
        {
            string datadavky = string.Empty;

            try
            {
                // TODO: nacteni servis params a provest kontrolu dokoncenosti??

                // pokud se odesilaji data a je povolene foceni na prijmu, dojde k odeslani fotek
                if (!uvolnit)
                {
                    // odeslani fotografii
                    //Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.Prijem4PrijemMainOdesilaniFotografii);
                    ImagesSynchronize();
                    //Program.mstw.mbw.EndPracujiForm();
                }

                //Program.mstw.mbw.BeginPracujiForm("Odesílají se data dávky.");

                if (ServisServiceOperations.SendData(Globals.globalObject.webServiceModule , int.Parse(vybranadavka), uvolnit))
                {
                    //Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainDataDavkyOdeslana, vybranadavka), Fask.Localization.Localization.Prijem4PrijemMainOdesilaniDat, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    Hlavicky.HlavickaDelete(vybranadavka);

                    // po uspesnem odeslani dat smazat data z ZdrojPohyb
                    if (!uvolnit)
                    {
						//System.Data.SQLite.SQLiteConnection sqlConnection = null;
						//sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojePohybDB);
						//Globals._taZdrojePohyb.Connection = sqlConnection;

                        //Globals._taZdrojePohyb.DeleteAllQuery();

						Globals.globalObject.Controller_servis_ZdrojePohyb.DeleteAllQuery_ZdrojePohyb();

                    }

                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                //Program.mstw.mbw.EndPracujiForm();
                Logging.Log.Write(ex.Message + ex.StackTrace, "Servis odesliDavku");
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }
        }

        private void stahniDavku_but_Click(object sender, EventArgs e)
        {
            string pdlDavka;
            string filename;
            bool result = stahniDavku(out pdlDavka, out filename);

            if (result && MST_Global.ServisDavkaOtevritIhnedPoStazeni) // TODO: Konfigurace
            {
                //if (pdlDavka.StartsWith("S"))
                //{
                //    // JoZ: seznam davek ve sloucene davce
                //    using (ListSlouceneDavkyForm listSlouceneForm = new ListSlouceneDavkyForm(filename))
                //    {
                //        string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.PrijemIExtData);
                //        listSlouceneForm.FileNames = fileNames;
                //        if (listSlouceneForm.ShowDialog() == DialogResult.Cancel)
                //            return;
                //    }
                //}

                zpracujDavku(Convert.ToInt32(pdlDavka));
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

            ServisModuleWService.ServisDavky servisdavky = ServisServiceOperations.GetHeads(Globals.globalObject.webServiceModule);
            if (servisdavky == null)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniSeznamuDavek, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }

            int vybranadavka = 0;
            ServisModuleWService.ServisDavky.HlavickyRow hr = null;

            //Odstraneni exitujicich souboru z hlavicek
            try
            {
                string[] filenames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_ServisI);
                foreach (string filename in filenames)
                {
                    ServisModuleWService.ServisDavky.HlavickyRow[] hrows =
                        (ServisModuleWService.ServisDavky.HlavickyRow[])servisdavky.Hlavicky.Select("CountEntries='" + Path.GetFileNameWithoutExtension(filename) + "'");
                    foreach (ServisModuleWService.ServisDavky.HlavickyRow hrow in hrows)
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
            
            servisdavky.AcceptChanges();
            // Zapise schema do souboru pokud se pocet sloupcu lisi.
            Hlavicky.WriteSchemaWithDynamicColumns(servisdavky);

            using (ServisDavkyList pdl = new ServisDavkyList(servisdavky, true))
            {
                if (pdl.ShowDialog() == DialogResult.Cancel)
                    return false;

                vybranadavka = int.Parse(pdl.Davka);
                hr = pdl.SelectedRow;
                //hr.Sloucena = false;

                pdlDavka = pdl.Davka;
                filenam = pdl.FileName;
            }

            // stahnuti dat davky
			if (!ServisServiceOperations.GetData(Globals.globalObject.webServiceModule, vybranadavka))
            {
                MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniDatDavky, Fask.Localization.Localization.Prijem4PrijemMainStazeniDat);
                return false;
            }
            else
            {
                try
                {
                    // doplneni hlavicky ...
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele ConOdb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Main.CiselnikOdberateleDB))
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav ConSer = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(Convert.ToInt32(pdlDavka) + "." + Main.Ext_ServisI))
					{
						try
						{
							//Globals.Davka = pdlDavka;
							// nacteni docnumber, zde neni mozne, davka jeste neni stazena  ...
							//System.Data.SqlServerCe.SqlCeConnection sqlConnection = null;
							//System.Data.SQLite.SQLiteConnection sqlConnection = null;
							//sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojeStavDB);
							//Globals._taPredloha.Connection = sqlConnection;
							//SqlCEDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable dtPredloha = Globals._taPredloha.GetData();
							//SqlCEDBs.DataSets.Servis.CZMST_Servis_PredlohaRow rowPredloha = null;

							//ConSer.TaPredloha.Connection = ConSer.Connection;
							Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable dtPredloha = ConSer.GetData_Predloha();
							Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaRow rowPredloha = null;


							rowPredloha = dtPredloha.Count > 0 ? dtPredloha[0] : null;
							if (rowPredloha != null)
							{
								hr.DOCUMENT_NUMBER = rowPredloha.IsDOCUMENT_NUMBERNull() ? string.Empty : rowPredloha.DOCUMENT_NUMBER;
								hr.OkruhID = rowPredloha.OkruhID;
								hr.Barcode = rowPredloha.IsBarcodeNull() ? string.Empty : rowPredloha.Barcode;

								// nacteni nazvu odberatele
								if (!rowPredloha.IsODB_IDNull())
								{
									//SqlCEDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter taOdberatel = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
									//sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikOdberateleDB);
									//taOdberatel.Connection = sqlConnection;
									//SqlCEDBs.DataSets.Odberatele.CZMST090DataTable dtOdberatel = taOdberatel.GetDataByOdbid(rowPredloha.ODB_ID);
									//SqlCEDBs.DataSets.Odberatele.CZMST090Row rowOdberatel = null;

									Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dtOdberatel = ConOdb.GetDataByOdbid(rowPredloha.ODB_ID);
									Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row rowOdberatel = null;

									rowOdberatel = dtOdberatel.Count > 0 ? dtOdberatel[0] : null;
									if (rowOdberatel != null && !rowOdberatel.Isodb_descNull())
										hr.OdbOznaceni = rowOdberatel.odb_desc;
								}

								//sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisCiselnikDB);
								//Globals._taCiselnikOkruh.Connection = sqlConnection;
								//SqlCEDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dtOkruh = Globals._taCiselnikOkruh.GetDataByID(rowPredloha.OkruhID);
								//SqlCEDBs.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruh = null;

								//SqlCEDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dtOkruh = ConSer.TaCiselnikOkruh.GetDataByID(rowPredloha.OkruhID);
                                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dtOkruh = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Okruh(rowPredloha.OkruhID);
								Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruh = null;


								rowOkruh = dtOkruh.Count > 0 ? dtOkruh[0] : null;
								if (rowOkruh != null)
								{
									hr.OkruhOznaceni = rowOkruh.Oznaceni;
									// 28.6.2016 PeV: Odstranìno, data se nacitaji rovnou z predlohy
									//if(!rowOkruh.IsODB_IDNull())
									//{
									//    //Main.CiselnikOdberateleDB
									//    Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter taOdberatel = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();                                    
									//    sqlConnection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Main.CiselnikOdberateleDB);
									//    taOdberatel.Connection = sqlConnection;
									//    Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dtOdberatel = taOdberatel.GetDataByOdbid(rowOkruh.ODB_ID);
									//    Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row rowOdberatel = null;

									//    rowOdberatel = dtOdberatel.Count > 0 ? dtOdberatel[0] : null;
									//    if(rowOdberatel != null && !rowOdberatel.Isodb_descNull())
									//        hr.OdbOznaceni = rowOdberatel.odb_desc;                                    
									//}
								}
							}
						}
						catch { } 
					}

                    Hlavicky.HlavickaAdd(hr);

                    ////zpracujDavku(vybranadavka); zruseno kvuli uprave a podobnosti s vydejem...

                    //Slouceni(hr);
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "stahniDavku");
                    return false;
                }
            }

            return true;
        }


		//public void ImportPEDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, string p)
		//{
		//    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter peta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
		//    peta.Connection.ConnectionString = "Data source=" + sourceFilePath;

		//    peta.Connection.Open();
		//    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable pedt = peta.GetDataByCountEntries(int.Parse(p));
		//    peta.Connection.Close();

		//    foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow row in pedt)
		//        row.SetAdded();

		//    peta.Connection.ConnectionString = "Data source=" + destinationFilePath;
		//    peta.Connection.Open();
		//    peta.Update(pedt);
		//    peta.Connection.Close();
		//}

		//public void ImportPIDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, string p)
		//{
		//    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pita = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
		//    pita.Connection.ConnectionString = "Data source=" + sourceFilePath;

		//    pita.Connection.Open();
		//    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable pidt = pita.GetDataByCountEntries(int.Parse(p));
		//    pita.Connection.Close();

		//    foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow row in pidt)
		//        row.SetAdded();

		//    pita.Connection.ConnectionString = "Data source=" + destinationFilePath;
		//    pita.Connection.Open();
		//    pita.Update(pidt);
		//    pita.Connection.Close();
		//}

		//public void ImportParametryDataFromTo(string sourceFilePath, string destinationFilePath)
		//{
		//    Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter seta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter();
		//    seta.Connection.ConnectionString = "Data source=" + sourceFilePath;

		//    seta.Connection.Open();
		//    Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable sedt = seta.GetData();
		//    seta.Connection.Close();

		//    foreach (Fask.SQLiteDBs.DataSets.Prijem.ParametryRow row in sedt)
		//        row.SetAdded();

		//    seta.Connection.ConnectionString = "Data source=" + destinationFilePath;
		//    seta.Connection.Open();
		//    seta.Update(sedt);
		//    seta.Connection.Close();

		//}

        //private void Slouceni(PrijemService.PrijemDavky.HlavickyRow chosenRow)
        //{
        //    if (!Globals.SlucovaniDavek)
        //        return;

        //    if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainSloucitDavkuDotaz, Fask.Localization.Localization.Prijem4PrijemMainDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
        //        return;

        //    string fileName = Path.Combine(MST_Global.Storage, "S1" + "." + Main.PrijemIExtData);

        //    if (!File.Exists(fileName))
        //    { //vytvorime novy datatemplate...
        //        string srcFile = Path.Combine(Main.SqlCEDBsDir, "Prijem.sdf");
        //        File.Copy(srcFile, fileName, false);

        //        //a radek do seznamu hlavicek...
        //        PrijemService.PrijemDavky d = new Fask.MST_W.PrijemService.PrijemDavky();
        //        PrijemService.PrijemDavky.HlavickyRow drow = d.Hlavicky.AddHlavickyRow("S1", "0", "", 0, 0, false);
        //        Hlavicky.HlavickaAdd(drow);
        //    }

        //    string sourceFilePath = Path.Combine(MST_Global.Storage, chosenRow.CountEntries + "." + Main.PrijemIExtData);

        //     Prijem_4.PrijemMain.prijemI.ImportPEDataByCountEntriesFromTo(sourceFilePath, fileName, chosenRow.CountEntries);

        //     Prijem_4.PrijemMain.prijemI.ImportPIDataByCountEntriesFromTo(sourceFilePath, fileName, chosenRow.CountEntries);

            
        //     Prijem_4.PrijemMain.prijemI.ImportParametryDataFromTo(sourceFilePath, fileName);

        //     Hlavicky.SetSloucena(chosenRow, true);
        //}


        private void buttonDavka_Click(object sender, EventArgs e)
        {
            // najde vsechny davky na disku
            string[] fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_ServisI);
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
                    
                    if (!MST_Global.ServisDavkaOtevritIhnedPoStazeni)
                    {
                        if (!stahniDavku(out fname, out pdl2Davka))
                            return;
                    } 
                    else
                    {
                        stahniDavku_but_Click(null, null);
                        return;
                    }
                }
            }

            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
            string pdlDavka = "";
            Hlavicky.Synchronize();
            string filename = "";
            int vybranadavka;
            ServisModuleWService.ServisDavky.HlavickyRow hr = null;

            using (ServisDavkyList pdl = new ServisDavkyList(Hlavicky.Davky, false))
            {
                Cursor.Current = Cursors.Default;
                if (pdl.ShowDialog() == DialogResult.Cancel) return;
                pdlDavka = pdl.Davka;
                filename = pdl.FileName;
                vybranadavka = int.Parse(pdl.Davka);

                hr = pdl.SelectedRow;
                //hr.Sloucena = false;
            }

            // stahnuti davky, pokud jeste neexistuje
            string filepath = Path.Combine(Main.StorageDir, pdlDavka + "." + Main.Ext_ServisI);
            if (!File.Exists(filepath))
            {
                if (!ServisServiceOperations.GetData(Globals.globalObject.webServiceModule, vybranadavka))
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemMainChybaStazeniDatDavky, Fask.Localization.Localization.Prijem4PrijemMainStazeniDat);
                    return;
                }
                else // pokud se podarilo stahnout a davka se nema otevrit ihned po stazeni
                    if (!MST_Global.ServisDavkaOtevritIhnedPoStazeni)
                    {
                        buttonDavka_Click(null, null);
                        return;
                    }
            }
 
            //if (pdlDavka.StartsWith("S"))
            //{
            //    // JoZ: seznam davek ve sloucene davce
            //    using (ListSlouceneDavkyForm listSlouceneForm = new ListSlouceneDavkyForm(filename))
            //    {
            //        fileNames = Directory.GetFiles(Main.StorageDir, "*." + Main.PrijemIExtData);
            //        listSlouceneForm.FileNames = fileNames;
            //        if (listSlouceneForm.ShowDialog() == DialogResult.Cancel)
            //            return;
            //    }
            //}

			zpracujDavku(Convert.ToInt32(pdlDavka));
        }

        /// <summary>
        /// Zpracovani davky.
        /// </summary>
        /// <param name="davka">Cislo vybrane davky.</param>
        private void zpracujDavku(int? davka)
        {
            #region 1.Existujici zaznam z ZdrojStav
            // do budoucna mozna bude potreba ...
            #endregion

            try
            {
                Globals.globalObject.Davka = davka;

                //System.Data.SQLite.SQLiteConnection sqlConnection = null;                
                //sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisZdrojeStavDB);
                //Globals._taPredloha.Connection = sqlConnection;
                //Globals.globalObject.Controller_servis.TaPredloha.Connection = Globals.globalObject.Controller_servis.Connection;
				Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable dtPredloha = Globals.globalObject.Controller_servis_ZdrojeStav.GetData_Predloha();
                
                // nacteni predlohy
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaRow predlohaRow = dtPredloha.Count > 0 ? dtPredloha[0] : null;



                //sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Globals.ServisCiselnikDB);
                //Globals._taCiselnikOkruh.Connection = sqlConnection;
                //SqlCEDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter taOdberatel = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
                //sqlConnection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikOdberateleDB);
                //taOdberatel.Connection = sqlConnection;

                // nacteni okruhu
                Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhRow okruhRow = null;
                if (predlohaRow != null)
                {
                    //SqlCEDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dtOkruh = Globals._taCiselnikOkruh.GetDataByID(predlohaRow.OkruhID);
					Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dtOkruh = Globals.globalObject.Controller_servis_Ciselniky.GetDataByID_Okruh(predlohaRow.OkruhID);
                    okruhRow = dtOkruh.Count > 0 ? dtOkruh[0] : null;
                }

                // nacteni odberatele                
                Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatelRow = null;                
                if (okruhRow != null && !okruhRow.IsODB_IDNull())
                {
					Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dtOdberatel = null;

					dtOdberatel = Globals.globalObject.Controller_odberatele.GetDataByOdbid(okruhRow.ODB_ID);
					

                    //SqlCEDBs.DataSets.Odberatele.CZMST090DataTable dtOdberatel = taOdberatel.GetDataByOdbid(okruhRow.ODB_ID);
                    odberatelRow = dtOdberatel.Count > 0 ? dtOdberatel[0] : null;
                }

                Cursor.Current = Cursors.WaitCursor; Application.DoEvents();
                //this.sqlfilename = Path.Combine(Main.StorageDir, davka + "." + Main.Ext_ServisI);

                using (ServisList pl = new ServisList(odberatelRow, okruhRow))
                {
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

        private void odesliHotovouDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.odesliHotovouDavku_but_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void vratitDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.vratitDavku_but_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void stahniDavku_but_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.stahniDavku_but_Click(null, null);
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

        private void ServisMain_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ServisMain_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void ServisMain_Closing(object sender, CancelEventArgs e)
        {
            servisI = null;
        }

		//private void buttonStahnoutOdberatele_Click(object sender, EventArgs e)
		//{

		//}

		//private void buttonStahnoutOdberatele_KeyDown(object sender, KeyEventArgs e)
		//{

		//}

        private void buttonStahnoutOdberatele_Click_1(object sender, EventArgs e)
        {
            if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportOdberateluDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
            {
                exportovatOdberatele();
            }

            stahnoutOdberatele();
        }

        private void exportovatOdberatele()
        {
			_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberateleExport(Globals.globalObject.ciselnikS);
        }

        private void stahnoutOdberatele()
        {
			_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberatele(Globals.globalObject.ciselnikS, Prodej.Globals.SkladID);
        }

        private void buttonStahnoutZdrojStav_Click(object sender, EventArgs e)
        {
            // aktualizace dat zdroju, stavu, stavnext ...
            try
            {
                ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Ciselniky(MST_Global.TerminalID);
                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return; // TODO : wait ... 
                }

                // stahnout data do tmp ... 
                FileTransfer.Routines.DownloadDecompressDelete(Main.ServisCiselnikDB);

                downloadDynamicTables();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

        /// <summary>
        /// Probìhne stáhnutí veškerých dynamických tabulek. Výsledné jméno souboru je: "Servis_Ciselniky" + "FullName v definici dynamických tabulek"
        /// </summary>
        private void downloadDynamicTables()
        {
            string path = string.Empty;

            foreach (Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow row in Globals.globalObject.Controller_servis_Ciselniky.GetData_DynTabDef())
            {
                path = Path.Combine(Main.DataDir, "Servis_Ciselniky" + row.FullName + ".sdf");
                // pøekopírování prázdné DB, kdyby nastala chyba
                //if (!File.Exists(path))
                //File.Copy(
                //    Path.Combine(Main.SqlCEDBsDir, Globals.ServisCiselnikFileName),
                //    path
                //    );

                ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.Prepare_Dynamic_Table(MST_Global.TerminalID, row.FullName);

                if (so.StatusText != "OK")
                {
                    MessageBoxBig.Show(so.StatusText, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                    return; // TODO : wait ... 
                }

                // stahnout data do tmp ... 
                FileTransfer.Downloading.DownloadFileFromServer(path);
            }
        }

        /// <summary>
        /// Odesilani fotografii. Pokud nastane chyba (napr. spojeni), tak dale nepokracuje.
        /// </summary>
        private void ImagesSynchronize()
        {
            try
            {
				//_WebRefernces_Globals.ServisModuleWServiceSession webService = new _WebRefernces_Globals.ServisModuleWServiceSession();
				//webService.Url = MST_Global.ServerAddress + "Servis.asmx";
				//webService.Timeout = MST_Global.ServiceTimeOut;
				//webService.UpdateWebServiceCredentials();

                string[] images = Directory.GetFiles(Main.ImagesDir);

                int pocetfotek = images.Length;
                int pocetfotekcount = 1;

                foreach (var imageFile in images)
                {
                    Program.mstw.mbw.BeginPracujiForm("Odesílání fotografie " + (pocetfotekcount++) + " / " + pocetfotek);

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

                    ServisModuleWService.StatusObject so = Globals.globalObject.webServiceModule.ImageArchivate(MST_Global.TerminalID, MST_Global.UserID, Path.GetFileName(imageFile), imageData);
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
            finally
            {
                Program.mstw.mbw.EndPracujiForm();
            }
        }
    }
}