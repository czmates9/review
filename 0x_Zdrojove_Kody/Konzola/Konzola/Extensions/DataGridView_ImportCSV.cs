using CsvHelper;
using Fask.Console.Interfaces.API_BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Extensions
{
   public static class DataGridView_ImportCSV
    {
        #region provider
        private static Fask.Interfaces.IMES providerOdberatele = null;
        private static Fask.Interfaces.IMES providerStrediska = null;
        private static Fask.Interfaces.IMES providerSkladLokaceMapa = null;
        private static Fask.Interfaces.IMES providerSklady = null;
        private static Fask.Interfaces.IMES providerSkladLokace = null;

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private static void InitProvider()
        {
            #region odberatele
            try
            {
                if (string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerOdberatele == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2).IsAssignableFrom(t))
                            {
                                providerOdberatele = (Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerOdberatele != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerOdberatele.InitProvider();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region strediska
            try
            {
                if (!String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                {
                    if (providerStrediska == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        //Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.Interfaces.Ciselniky.Strediska.IStrediska2).IsAssignableFrom(t))
                                {
                                    providerStrediska = (Fask.Interfaces.Ciselniky.Strediska.IStrediska2)providerAssemlby.CreateInstance(t.FullName);
                                    if (providerStrediska != null)
                                        break;
                                }
                            }
                            catch { }
                        }
                        //return config;
                    }

                    providerStrediska.InitProvider();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region SkladLokaceMapa

            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerSkladLokaceMapa == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2).IsAssignableFrom(t))
                            {
                                providerSkladLokaceMapa = (Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSkladLokaceMapa != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSkladLokaceMapa.InitProvider();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region SkladLokace CZMST094


            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerSkladLokace == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094).IsAssignableFrom(t))
                            {
                                providerSkladLokace = (Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSkladLokace != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSkladLokace.InitProvider();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            #region Sklady
            try
            {
                if (String.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerSklady == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Sklady.ISklady2).IsAssignableFrom(t))
                            {
                                providerSklady = (Fask.Interfaces.Ciselniky.Sklady.ISklady2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerSklady != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerSklady.InitProvider();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
               // MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        #endregion

        private static DialogResult showPathDialog_CSV(out string filepath, bool ulozeni = true)
        {
            filepath = string.Empty;

            //object sfd = null;
            //DialogResult dr = sfd.ShowDialog();
            DialogResult dr = new DialogResult();
            try
            {


                if (ulozeni)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    //sfd = new SaveFileDialog();
                    sfd.Filter = ".csv Files (*.csv)|*.csv";
                    sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
                    sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyCSVFileName;

                    //DialogResult dr = sfd.ShowDialog();
                    dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {

                        Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                        //Settings.ExportyCSVFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        filepath = sfd.FileName;
                    }
                }
                else
                {
                    OpenFileDialog sfd = new OpenFileDialog();
                    //sfd = new OpenFileDialog();
                    sfd.Filter = ".csv Files (*.csv)|*.csv";
                    sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
                    sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyCSVFileName;

                    // DialogResult dr = sfd.ShowDialog();
                    dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {

                        Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                        //Settings.ExportyCSVFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        filepath = sfd.FileName;
                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dr;
        }
      
        #region zapis radku/zaznamu do tabulky SQL

        #region Odberatele

        private static void InsertRow_Odberatele(Odberatele_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
                #region Insert
                if (record != null)
                {
                    Fask.Interfaces.DataSets.Odberatele ds2 = new Fask.Interfaces.DataSets.Odberatele();


                    #region Odberatele

                    #region MaR dilo 23.5.2024

                    Fask.Interfaces.DataSets.Odberatele.CZMST090Row newRow_Odberatele = ds2.CZMST090.NewCZMST090Row();

                    newRow_Odberatele.odb_id = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_id;
                    newRow_Odberatele.odb_desc = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_desc;
                    newRow_Odberatele.odb_typ = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_typ;
                    newRow_Odberatele.odb_carcode = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_carcode;
                    newRow_Odberatele.odb_ico = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_ico;
                    newRow_Odberatele.mena_id = /*TextBox_ITEMNMBR.Text.Trim();*/ record.mena_ID;
                    newRow_Odberatele.odb_misto = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_misto;
                    newRow_Odberatele.odb_ulice = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_ulice;
                    newRow_Odberatele.odb_cisloOr = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_cisloOr;
                    newRow_Odberatele.odb_psc = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_psc;
                    newRow_Odberatele.odb_dic = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_dic;


                    if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Odberatel.HasValue)
                        newRow_Odberatele.Setodb_OdberatelNull();
                    else
                        newRow_Odberatele.odb_Odberatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Odberatel.Value;

                    if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Dodavatel.HasValue)
                        newRow_Odberatele.Setodb_DodavatelNull();
                    else
                        newRow_Odberatele.odb_Dodavatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Dodavatel.Value;


                    InitProvider();

                    if ((providerOdberatele != null) && (providerOdberatele is Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel))
                        ((Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel)providerOdberatele).InsertOdberatel(newRow_Odberatele);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");



                    #endregion



                    #endregion



                }
                #endregion




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region strediska
        private static void InsertRow_Strediska(Strediska_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
                #region Insert
                if (record != null)
                {
                    Fask.Interfaces.DataSets.Strediska ds2 = new Fask.Interfaces.DataSets.Strediska();


                    #region Odberatele

                    #region MaR dilo 23.5.2024

                    Fask.Interfaces.DataSets.Strediska.CZMST091Row newRow_Strediska = ds2.CZMST091.NewCZMST091Row();


                    newRow_Strediska.str_id = record.str_id;
                    newRow_Strediska.str_desc = record.str_desc;
                    newRow_Strediska.str_typ = record.str_typ;
                    newRow_Strediska.str_carcode = record.str_carcode;
                    newRow_Strediska.skl_id = record.skl_id;
                    newRow_Strediska.odb_id = record.odb_id;


                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Odberatel.HasValue)
                    //    newRow_Odberatele.Setodb_OdberatelNull();
                    //else
                    //    newRow_Odberatele.odb_Odberatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Odberatel.Value;

                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Dodavatel.HasValue)
                    //    newRow_Odberatele.Setodb_DodavatelNull();
                    //else
                    //    newRow_Odberatele.odb_Dodavatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Dodavatel.Value;


                    InitProvider();

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko))
                        ((Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko)providerStrediska).InsertStredisko(newRow_Strediska);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");



                    #endregion



                    #endregion



                }
                #endregion




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region SkladLokace_Mapa
        private static void InsertRow_SkladLokace_Mapa(SkladLokaceMapa_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
                #region Insert
                if (record != null)
                {
                    Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();


                    #region SkladLokace_Mapa

                    #region MaR dilo 23.5.2024

                    Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow newRow_CZMST_SkladLokace_Mapa = ds2.CZMST_SkladLokace_Mapa.NewCZMST_SkladLokace_MapaRow();


                    newRow_CZMST_SkladLokace_Mapa.SKL_ID = record.SKL_ID;
                    newRow_CZMST_SkladLokace_Mapa.LOCNCODE = record.LOCNCODE;
                    newRow_CZMST_SkladLokace_Mapa.TYPE = record.TYPE;
                    newRow_CZMST_SkladLokace_Mapa.Barcode = record.Barcode;
                    newRow_CZMST_SkladLokace_Mapa.Description = record.Description;
                    newRow_CZMST_SkladLokace_Mapa.SkladOznaceni = record.SkladOznaceni;


                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Odberatel.HasValue)
                    //    newRow_Odberatele.Setodb_OdberatelNull();
                    //else
                    //    newRow_Odberatele.odb_Odberatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Odberatel.Value;

                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Dodavatel.HasValue)
                    //    newRow_Odberatele.Setodb_DodavatelNull();
                    //else
                    //    newRow_Odberatele.odb_Dodavatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Dodavatel.Value;


                    InitProvider();

                    if ((providerStrediska != null) && (providerStrediska is Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa))
                        ((Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa)providerStrediska).InsertSkladLokace_Mapa(newRow_CZMST_SkladLokace_Mapa);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");



                    #endregion



                    #endregion



                }
                #endregion




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region SkladLokace CZMST094
        private static void InsertRow_SkladLokace_CZMST094(SkladLokaceCZMST094_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
              
                if (record != null)
                {
                    Fask.Interfaces.DataSets.SkladLokace ds2 = new Fask.Interfaces.DataSets.SkladLokace();

                    Fask.Interfaces.DataSets.SkladLokace.CZMST094Row newRow_CZMST094 = ds2.CZMST094.NewCZMST094Row();


                    newRow_CZMST094.SKL_ID = record.SKL_ID;
                    newRow_CZMST094.LOCNCODE = record.LOCNCODE;
                    newRow_CZMST094.TYPE = record.TYPE;
                    newRow_CZMST094.Barcode = record.Barcode;
                    newRow_CZMST094.Description = record.Description;


                    InitProvider();

                    if ((providerSkladLokace != null) && (providerSkladLokace is Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace))
                        ((Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace)providerStrediska).InsertSkladLokace_CZMST094(newRow_CZMST094);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci ISkladLokace_CZMST094_InsertSkladLokace");



                }




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool ImportzCSV_SkladLokace_CZMST094(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    //}

                    foreach (var record in csv.GetRecords<SkladLokaceCZMST094_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        InsertRow_SkladLokace_CZMST094(record);


                    }
                }




            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
       


        #endregion

        #region Sklady
        private static void InsertRow_Sklady(Sklady_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
                #region Insert
                if (record != null)
                {
                    Fask.Interfaces.DataSets.Sklady ds2 = new Fask.Interfaces.DataSets.Sklady();


                    #region SkladLokace_Mapa

                    #region MaR dilo 23.5.2024

                    Fask.Interfaces.DataSets.Sklady.CZMST093Row newRow_Sklady = ds2.CZMST093.NewCZMST093Row();


                    newRow_Sklady.skl_id = record.skl_id;
                    newRow_Sklady.skl_desc = record.skl_desc;
                    newRow_Sklady.skl_typ = record.skl_typ;
                    newRow_Sklady.skl_carcode = record.skl_carcode;
                    //newRow_Sklady.Description = record.Description;
                    //newRow_Sklady.SkladOznaceni = record.SkladOznaceni;


                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Odberatel.HasValue)
                    //    newRow_Odberatele.Setodb_OdberatelNull();
                    //else
                    //    newRow_Odberatele.odb_Odberatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Odberatel.Value;

                    //if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.odb_Dodavatel.HasValue)
                    //    newRow_Odberatele.Setodb_DodavatelNull();
                    //else
                    //    newRow_Odberatele.odb_Dodavatel = /*TextBox_ITEMNMBR.Text.Trim();*/ record.odb_Dodavatel.Value;


                    InitProvider();

                    if ((providerSklady != null) && (providerSklady is Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad))
                        ((Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad)providerSklady).InsertSklad(newRow_Sklady);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");



                    #endregion



                    #endregion



                }
                #endregion




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region import dat csv
        public static bool ImportzCSV_strediska(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    //}

                    foreach (var record in csv.GetRecords<Strediska_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        InsertRow_Strediska(record);


                    }
                }




            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool ImportzCSV_odberatele(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    //}

                    foreach (var record in csv.GetRecords<Odberatele_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        InsertRow_Odberatele(record);


                    }
                }




            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool ImportzCSV_SkladLokace_Mapa(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    //}

                    foreach (var record in csv.GetRecords<SkladLokaceMapa_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        InsertRow_SkladLokace_Mapa(record);


                    }
                }




            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        public static bool ImportzCSV_Sklady(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    //}

                    foreach (var record in csv.GetRecords<Sklady_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        InsertRow_Sklady(record);


                    }
                }




            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion

        public static bool KontrolaImportuDat(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo, string path)
        {
            bool kontrola = false;

            #region kontrola atributu CSV vuci tabulce 
            // Přečte všechny řádky ze souboru CSV
            string[] lines = System.IO.File.ReadAllLines(path);

            // Vytvoří nový DataTable pro ukládání dat z CSV
            DataTable dt = new DataTable();



            // Vytvoříme list pro ukládání hlaviček a odpovídajících atributů záznamu z DB
            var headerAttributes = new List<Tuple<string, string, int>>();
            List<string> headerParts = new List<string>();

            if (lines.Length < 2)
                throw new Exception("Not enough lines to work on ... ");

            // Rozdělení řetězce podle středníků a uložení výsledků do List<string>
            headerParts = lines[0].Split(';').ToList();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                string columnName = column.Name; // Získání názvu sloupce
                string headerText = column.HeaderText; // Získání hlavičkového textu (popisu sloupce)

                int i = 0;
                foreach (string part in headerParts)
                {


                    // Porovnání bez diakritiky
                    if (RemoveDiacritics(headerText.Trim()).Equals(RemoveDiacritics(part.Trim()), StringComparison.CurrentCultureIgnoreCase))
                    {
                        headerAttributes.Add(new Tuple<string, string, int>(part, columnName, i)); // Přidání hlavičky a odpovídajícího atributu záznamu do listu
                        Console.WriteLine(part);
                        break;
                    }
                    i++;
                }
            }

            //kontrola potrebnych parametru z listu
            var x = headerAttributes;

            int pocetPovinnych = 0;
            int pocetShod = 0;


            foreach (var item in tableInfo)
            {
                if (!item.Item3)
                {
                    pocetPovinnych++;

                }
            }


            foreach (var itemTabulky in tableInfo)
            {
                if (!itemTabulky.Item3)
                {
                    foreach (var item_CSV in headerAttributes)
                    {


                        if (itemTabulky.Item1 == item_CSV.Item2)
                        {
                            pocetShod++;
                        }

                    }


                }
            }


            //zde odecitam 2hodnoty, protoze DEX_ROW_ID se dava automaticky a CZ_Expirace_Track se vyplnuje 0 v procedure
            if ((pocetPovinnych - 2) == pocetShod)
            {
                kontrola = true;
                //uspech - kontrola je v poradku

                //dotaz na smazani dat v tabulce FASK_ZASOBY!!! dodelat!!

                //nahrani dat z CSV do tabulky FASK_ZASOBY
                //csvData;

            }
            else
            {
                //neuspech kontroly
                return false;
            }

            return kontrola;
            #endregion
        }

        // Metoda pro odstranění diakritiky ze stringu
        public static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
