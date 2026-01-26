using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;

using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;
using Fask.Logging;
using Fask.MST_W_Server.SQLite_Classes;
using Fask.MST_W_Server.Constants;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Summary description for Vyroba
    /// </summary>
    [WebService(Namespace = "http://Vyroba.fask.cz/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Vyroba : System.Web.Services.WebService
    {

        #region Parametry

        Fask.Server.Interfaces.Vyroba.IProduction provider = null;

        #endregion

        #region C'tor

        public Vyroba()
        {
            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Vyroba;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
                    providerAssemblyPath = providerAssemblyPathGlobal;

                if (!String.IsNullOrEmpty(providerAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.Vyroba.IProduction).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Vyroba.IProduction)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                    {
                                        if (provider is Fask.Server.Interfaces.Configuration.IConfiguration)
                                        {
                                            ((Fask.Server.Interfaces.Configuration.IConfiguration)provider).LoadConfiguration();
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            if (provider != null)
                provider.AllowUserProductionOnMoreMachines = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0].AllowUserProductionOnMoreMachines;
        }

        #endregion

        #region WebMetody

        #region TaD Napsano

        [WebMethod(Description ="Metoda sloužící pro online generovaní šarže")]
        public List<string> Generate_SarzeOnline(string smenaID, string userID, string linkaID, decimal qty, string ITEMNMBR)
        {
            List<string> s = new List<string>();

            try
            {

                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    s.Add(provider.ReturnSarze(smenaID, userID, linkaID, qty, ITEMNMBR));
                    return s;
                }
                else
                {
                    throw new NotImplementedException("Provider");
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }


            return s;

        }

        #endregion

            #region Overene že sa použivaju, a když je potreba tak zmodifikovane....

            #region v Projektu Vyroba_P je volani nazvano jak CustomVyroba a volaji se tyto metody...

        [WebMethod]
        public string Production_OpenedCorrection_Json(string UserID, string MachineID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = Production_OpenedCorrection(UserID, MachineID);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(ds_vyroba);
                return output;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        [WebMethod]
        public string Production_OpenedProduction_Json(string UserID, string MachineID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = Production_OpenedProduction(UserID, MachineID);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(ds_vyroba);
                return output;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion



        /// <summary>
        /// Metoda která vrací èi zadané heslo do parametru je stejné jak heslo ktere je nastavene v konfiguraci na serveru.... 
        /// Heslo je pro všechny terminaly/tablety stejné...
        /// </summary>
        /// <param name="idterminal">ID Terminalu</param>
        /// <param name="pass">Heslo na skontrolovani</param>
        /// <returns>Bool True-heslo je OK, False- heslo neni OK</returns>
        [WebMethod(Description = "Metoda pro ovìøení hesla na ukonèení aplikace.")]
        public bool ApplicationEndPass(byte idterminal, string pass)
        {
            try
            {
                string password = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0].ApplicationEndPass;
                return (pass.Trim() == password.Trim());
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        /// <summary>
        /// Metoda pro synchronizaci èasu. Vraci jaky je èas na serveru
        /// </summary>
        /// <returns>Vrací èas</returns>
        [WebMethod(Description = "Metoda pro synchronizaci èasu. Vraci jaky je èas na serveru")]
        public DateTime ServerDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Metoda sloužici pro Uzavøeni, Blokovani anebo ODblokovani pøíkazu
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="vyrobniprikaz">struktura(proè to neni trida?) nese informace o Vyrobnim pøikazu</param>
        /// <param name="pozadavekBlokace">Enum pro to co se ma udelat (Ukavøit, BLokovat, Odblokovat)</param>
        /// <returns>True - OK, False- neco je zle</returns>
        [WebMethod(Description = "Metoda sloužici pro Uzavøeni, Blokovani anebo ODblokovani pøíkazu")]
        public bool VyrobniPrikazBlokace(byte terminalID, Fask.Server.Interfaces.Classes_Vyroba.VyrobniPrikazHlavicka vyrobniprikaz, Fask.Server.Interfaces.Classes_Vyroba.BlokaceTyp pozadavekBlokace)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.VyrobniPrikazBlokace(terminalID, vyrobniprikaz, pozadavekBlokace);
                }

                string msg = "Provider v Vyroba.asmx > 'VyrobniPrikazBlokace(byte terminalID, Fask.Server.Interfaces.Classes_Vyroba.VyrobniPrikazHlavicka vyrobniprikaz, Fask.Server.Interfaces.Classes_Vyroba.BlokaceTyp pozadavekBlokace)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vratí všechny otevøene Productions 
        /// </summary>
        /// <returns>Dataset Production.DataServices.VyrobaDataSet.ProductionDataTable filled with all opened productions</returns>
        [WebMethod(Description = "Metoda která vratí všechny otevøene Productions")]
        public Fask.Interfaces.DataSets.Vyroba Production_AllOpenedProductions()
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.AllOpenedProductions();
                }

                string msg = "Provider v Vyroba.asmx > 'Production_AllOpenedProductions()' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vrací historii dat z DB za pomoci filteru
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="nLastActions"></param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda která vrací historii dat z DB za pomoci filteru")]
        public Fask.Interfaces.DataSets.Vyroba Production_HistoryFilter(Fask.Server.Interfaces.Classes_Vyroba.FiltersHistory filters, int nLastActions)
        {
            try
            {
                
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.ProductionHistoryFilter(filters, nLastActions);
                }

                string msg = "Provider v Vyroba.asmx > 'Production_HistoryFilter(Fask.Server.Interfaces.Classes_Vyroba.FiltersHistory filters, int nLastActions)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vraci informaci o case pro uzivatele ...
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="datetimeLogin"></param>
        /// <param name="datetimeLastOperation"></param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda která vraci informaci o case pro uzivatele ...")]
        public Fask.Server.Interfaces.Classes_Vyroba.Report_UserDay Get_Report_UserDay(string UserID, DateTime datetimeLogin, DateTime datetimeLastOperation)
        {
            try
            {
                
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.Get_Report_UserDay(UserID, datetimeLogin, datetimeLastOperation);
                }

                string msg = "Provider v Vyroba.asmx > 'Get_Report_UserDay(string UserID, DateTime datetimeLogin, DateTime datetimeLastOperation)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vrací poslední producion actions
        /// </summary>
        /// <param name="UserID">User id action</param>
        /// <param name="MachineID">MachineID (string.empty=all machines</param>
        /// <param name="Corrections">With corrections</param>
        /// <param name="nLastActions">n records</param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda která vrací poslední producion actions")]
        public Fask.Interfaces.DataSets.Vyroba ProductionLastAction(string UserID, string MachineID, bool? Corrections, int nLastActions)
        {
            try
            {
                
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.ProductionLastAction(UserID, MachineID, Corrections, nLastActions);
                }

                string msg = "Provider v Vyroba.asmx > 'ProductionLastAction(string UserID, string MachineID, bool? Corrections, int nLastActions)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vrací poslední user action datetime(dateeve)
        /// </summary>
        /// <param name="UserID">User id for production</param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda která vrací poslední user action datetime(dateeve)")]
        public DateTime? UserLastAction(string UserID)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.UserLastAction(UserID);
                }

                string msg = "Provider v Vyroba.asmx > 'UserLastAction(string UserID)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vrací poslední otevøený Production pro uživatele a stroj
        /// </summary>
        /// <param name="userID">User</param>
        /// <param name="MachineID">Machine</param>
        /// <returns>Dataset Production.DataServices.VyrobaDataSet.ProductionDataTable filled with last opened production for user and machine</returns>
        [WebMethod(Description = "Metoda která vrací poslední otevøený Production pro uživatele a stroj")]
        public Fask.Interfaces.DataSets.Vyroba Production_OpenedProduction(string UserID, string MachineID)
        {
            try
            {
                
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.OpenedProduction(UserID, MachineID);
                }

                string msg = "Provider v Vyroba.asmx > 'Production_OpenedProduction(string UserID, string MachineID)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda ktera vrací Production podle filtru
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda ktera vrací Production podle filtru")]
        public Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni Production_Filter(Fask.Server.Interfaces.Classes_Vyroba.FiltersHistory filters)
        {
            try
            {
                
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.Production_Filter(filters);
                }

                string msg = "Provider v Vyroba.asmx > 'Production_Filter(Fask.Server.Interfaces.Classes_Vyroba.FiltersHistory filters)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vrací poslední otevøený Corrections pro uživatele a stroj
        /// </summary>
        /// <param name="userID">User</param>
        /// <param name="MachineID">Machine</param>
        /// <returns>Dataset Production.DataServices.VyrobaDataSet.ProductionDataTable filled with last opened corrections for user and machine</returns>
        [WebMethod(Description = "Metoda která vrací poslední otevøený Corrections pro uživatele a stroj")]
        public Fask.Interfaces.DataSets.Vyroba Production_OpenedCorrection(string UserID, string MachineID)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.OpenedCorrection(UserID, MachineID);
                }

                string msg = "Provider v Vyroba.asmx > 'Production_OpenedCorrection(string UserID, string MachineID)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Metoda pro zpracovani odeslanych dat...
        /// </summary>
        /// <param name="terminalID"></param>
        /// /// <param name="guid"></param>
        /// <returns></returns>
        [WebMethod(Description = "Metoda pro zpracovani odeslanych dat... s zip...")]
        public bool ProcessProductionData2(byte terminalID, Guid guid)
        {
            try
            {

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Production + "_" + guid.ToString("N") + Common.PRD + Common.TMP);
                string dstFileZip = dstFile + Common.ZIP;


                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

                //odzipovat
                Fask.Compressing.Zip.Decompress(dstFileZip);


                // odvadeni v hlavnim vlakne
                if (Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0].ProcessProductionDataMainThread)
                {
                    return ProcessProductionData(new ProcessProductionObject(terminalID, dstFile));
                }
                else
                {
                    System.Threading.Thread threadProcess = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ProcessProductionDataThreaded));
                    threadProcess.IsBackground = true;
                    threadProcess.Start(new ProcessProductionObject(terminalID, dstFile));

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }


        #endregion

        #region NUTNO PREDELAT


        #region Staženi do terminalu

        /// <summary>
        /// Metoda která vytvoøí SQLite databazi naplnenou datama z DB a zazipuje
        /// 
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="lastDateTime">Datum poslednej aktualizace?</param>
        /// <returns>true - OK, False - Blbo</returns>
        [WebMethod(Description = "Metoda která vytvoøí SQLite databazi naplnenou datama z DB a zazipuje")]
        public bool VyrobaDBDateTimePrepareZip(byte terminalID, DateTime lastDateTime)
        {
            //FileStream fs = null;
           // string filename = string.Empty;

            try
            {
                // naplneni daty
                //byte[] data = VyrobaDBDateTime(terminalID, lastDateTime);

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Vyroba + Common.PRD + Common.TMP);

                try
                {
                    //string srcFile = Path.Combine(rootpath, @"SQLiteDBs\VyrobaCE.sdf");
                
                    if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                        Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


                    if (!File.Exists(dstFile)) //Jestlize soubor jiz existuje, pak ho jen znovu poslat a negenerovat.
                    {
                        Fask.Interfaces.DataSets.Vyroba vds = new Fask.Interfaces.DataSets.Vyroba();
                        try
                        {
                            SQLite_Helper helper = new SQLite_Helper();
                            if (!helper.SQLite_CreateFile(dstFile, Common.Vyroba))
                            {
                                throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Vyroba);
                            }

                            provider.GetTables(vds);

                            foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow vphr in vds.CZPRO_VPH)
                            {
                                vphr.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow vppr in vds.CZPRO_VPP)
                            {
                                vppr.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.CorrectsRow vppr in vds.Corrects)
                            {
                                vppr.SetAdded();
                            }



                            #region Naèteni uživatelu

                            FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim("V_");


                            if ((pris != null) && (pris.FASK_Logins.Count > 0))
                            {
                                vds.Logins.Clear();

                                foreach (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow item in pris.FASK_Logins)
                                {
                                    byte VS = FASK.Logins.Uzivatel.Instance.GetUzivatele_VedouciSmeny(item);

                                    vds.Logins.AddLoginsRow(
                                        item.USERID,
                                        item.firstname,
                                        item.surname,
                                        item.psswd,
                                        VS);
                                }

                                //vds.Logins.AcceptChanges();

                                //foreach (VyrobaDataSet.LoginsRow vppr in vds.Logins)
                                //{
                                //    vppr.SetAdded();
                                //}

                            }

                            #endregion


                            foreach (Fask.Interfaces.DataSets.Vyroba.MachinesRow vppr in vds.Machines)
                            {
                                vppr.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.StatusTypesRow vppr in vds.StatusTypes)
                            {
                                vppr.SetAdded();
                            }

                            foreach (var item in vds.Operations)
                            {
                                item.SetAdded();
                            }

                            foreach (var item in vds.VMachinesOperations)
                            {
                                item.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.FASK_CONS_095Row item in vds.FASK_CONS_095)
                            {
                                item.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.CZMST093Row item in vds.CZMST093)
                            {
                                item.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.CZMST094Row item in vds.CZMST094)
                            {
                                item.SetAdded();
                            }

                            foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPRow item in vds.FASK_Vyroba_TP)
                            {
                                item.SetAdded();
                            }

                            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(dstFile))
                            {
                                ConVyr.Update_CZPRO_VPH(vds.CZPRO_VPH.Select(null, null, DataViewRowState.Added), false);
                                ConVyr.Update_CZPRO_VPP(vds.CZPRO_VPP.Select(null, null, DataViewRowState.Added), false);
                                ConVyr.Update_Corrects(vds.Corrects.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_Logins(vds.Logins.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_Machines(vds.Machines.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_Operations(vds.Operations.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_VMachinesOperations(vds.VMachinesOperations.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_StatusTypes(vds.StatusTypes.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_FASK_CONS_095(vds.FASK_CONS_095.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_CZMST093(vds.CZMST093.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_CZMST094(vds.CZMST094.Select(null, null, DataViewRowState.Added));
                                ConVyr.Update_FASK_Vyroba_TP(vds.FASK_Vyroba_TP.Select(null, null, DataViewRowState.Added));

                                ConVyr.Shrink();
                            }

                        }
                        catch (Exception ex)
                        {

                            Fask.Logging.ExceptionHandler2.Handle(vds.FASK_Vyroba_TP);
                            Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                            // pokud nastala chyba pri naplnovani ciselniku, tak dojde ke smazani ciselniku (aby nedoslo pri dalsim volani metody VyrobaDBDateTimePrepare ke stahnuti prazdneho ciselniku)
                            try
                            {
                                if (File.Exists(dstFile))
                                    File.Delete(dstFile);
                            }
                            catch (Exception ex2)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "VyrobaDBDateTime: " + Common.Vyroba + Common.PRD + " delete problem, " + dstFile + "\n" + ex2.Message);
                            }

                            throw ex;
                        }
                    }

                    //Prenos databazoveho souboru slqce prijemky pro temrinal
                    //long fileLength = (new FileInfo(dstFile)).Length;

                    //byte[] dbdata = new byte[fileLength];

                    //FileStream fs = null;
                    //try
                    //{
                    //    fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                    //    int bytesread = fs.Read(dbdata, 0, (int)fileLength);
                    //    fs.Close();
                    //    fs = null;
                    //    if (bytesread != (int)fileLength)
                    //        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "VyrobaDB: bytesread != (int)fileLength");

                    //}
                    //finally
                    //{
                    //    if (fs != null)
                    //    {
                    //        fs.Close();
                    //        fs = null;
                    //    }
                    //}

                   // File.Delete(dstFile);

                    //return dbdata;
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }

                //filename = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Vyroba + Common.PRD);

                //fs = new FileStream(filename, FileMode.Create);
                //fs.Write(data, 0, data.Length);
                //fs.Flush();

                //if (fs != null)
                //{
                //    fs.Close();
                //    fs = null;
                //}


                if (File.Exists(dstFile + Common.ZIP))
                    File.Delete(dstFile + Common.ZIP);

                Fask.Compressing.Zip.Compress(dstFile);

                //File.Delete(filename);

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                //if (File.Exists(filename + Common.ZIP))
                //    File.Delete(filename + Common.ZIP);

                return false;
            }
            finally
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //    fs = null;
                //}

                //if (File.Exists(filename))
                //    File.Delete(filename);

            }
            return true;
        }

        /// <summary>
        /// Vytvori InternalState datovou predlohu pro terminal
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="lastDateTime"></param>
        /// <returns></returns>
        [WebMethod(Description = "Vytvori InternalState datovou predlohu pro terminal")]
        public bool VyrobaDBInternalStatePrepareZip(byte terminalID, DateTime lastDateTime)
        {

            FileStream fs = null;
            string filename = string.Empty;

            try
            {
                // naplneni daty
                byte[] data = VyrobaDBInternalState(terminalID, lastDateTime);

                filename = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.InternalState + Common.PRD + Common.TMP);

                using (fs = new FileStream(filename, FileMode.Create))
                {
                    
                    fs.Write(data, 0, data.Length);
                    fs.Flush();

                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    } 
                }


                if (File.Exists(filename + Common.ZIP))
                    File.Delete(filename + Common.ZIP);

                Fask.Compressing.Zip.Compress(filename);

                File.Delete(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                if (File.Exists(filename + Common.ZIP))
                    File.Delete(filename + Common.ZIP);

                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                if (File.Exists(filename))
                    File.Delete(filename);
            }
            return true;
        }

        /// <summary>
        /// Vytvori production datovou predlohu pro terminal
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="lastDateTime"></param>
        /// <returns></returns>
        [WebMethod(Description = "Vytvori production datovou predlohu pro terminal")]
        public bool VyrobaDBProductionPrepareZip(byte terminalID, DateTime lastDateTime)
        {
            FileStream fs = null;
            string filename = string.Empty;

            try
            {
                //string filename = terminalID.ToString() + "_VyrobaCE.sdf";
                // naplneni daty
                byte[] data = VyrobaDBProduction(terminalID, lastDateTime);

                //filename = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "Production.sdf.tmp");
                filename = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Production + Common.PRD + Common.TMP);
                using (fs = new FileStream(filename, FileMode.Create))
                {
                    //fs = new FileStream(Path.Combine(UploadPath, filename), FileMode.Create);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();

                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    } 
                }


                if (File.Exists(filename + Common.ZIP))
                    File.Delete(filename + Common.ZIP);

                Fask.Compressing.Zip.Compress(filename);
                //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                //{
                //    zip.AddFile(filename, "");
                //    zip.Comment = "Made in FASK";
                //    zip.Save(filename + ".zip");
                //    zip.Dispose();
                //}
                File.Delete(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                if (File.Exists(filename + Common.ZIP))
                    File.Delete(filename + Common.ZIP);

                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                if (File.Exists(filename))
                    File.Delete(filename);

                //if (File.Exists(filename + ".zip"))
                //    File.Delete(filename + ".zip");

            }
            return true;
        }

        #endregion

        ///// <summary>
        ///// Metoda pro zpracovani odeslanych dat...
        ///// </summary>
        ///// <param name="terminalID"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //[WebMethod(Description = "Metoda pro zpracovani odeslanych dat...")]
        //public bool ProcessProductionData(byte terminalID, byte[] data)
        //{
        //    try
        //    {
        //        //string dstFile = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\Production_" + Guid.NewGuid().ToString("N") + ".sdf");
        //        string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Production + "_" + Guid.NewGuid().ToString("N") + Common.PRD);
        //        if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
        //            Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

        //        FileStream fs = null;
        //        try
        //        {
        //            fs = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.Read);
        //            fs.Write(data, 0, data.Length);
        //            fs.Flush();
        //            fs.Close();
        //            fs = null;
        //        }
        //        finally
        //        {
        //            if (fs != null)
        //            {
        //                fs.Flush();
        //                fs.Close();
        //                fs = null;
        //            }
        //        }

        //        // odvadeni v hlavnim vlakne
        //        if (Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Vyroba[0].ProcessProductionDataMainThread)
        //        {
        //            return ProcessProductionData(new ProcessProductionObject(terminalID, dstFile));
        //        }
        //        else
        //        {
        //            System.Threading.Thread threadProcess = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ProcessProductionDataThreaded));
        //            threadProcess.IsBackground = true;
        //            threadProcess.Start(new ProcessProductionObject(terminalID, dstFile));

        //            //ProcessProductionDataThreaded(new ProcessProductionObject(terminalID, dstFile));

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //}

        #endregion

        /// <summary>
        /// Slouzi pouze k predani schematu pro update webreference
        /// </summary>
        /// <returns>Allways null</returns>
        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba VyrobaCEDataSetSchema()
        {
            return null;
        }

        //[WebMethod]
        //public byte[] VyrobaDB(byte terminalID)
        //{
        //    try
        //    {
        //        return VyrobaDBDateTime(terminalID, DateTime.MinValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //}

        [WebMethod]
        public byte[] VyrobaDBInternalState(byte terminalID, DateTime lastDateTime)
        {
            try
            {
                //string srcFile = "už neexistuje...";
                //string dstFile = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "InternalState.sdf");

                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.InternalState + Common.PRD);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

                if (!File.Exists(dstFile)) //Jestlize soubor jiz existuje, pak ho jen znovu poslat a negenerovat.
                {
                    try
                    {
                        SQLite_Helper helper = new SQLite_Helper();
                        if (!helper.SQLite_CreateFile(dstFile, Common.InternalState))
                        {
                            throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.InternalState);
                        }
                    }
                    catch (Exception ex)
                    {

                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                        // pokud nastala chyba pri naplnovani ciselniku, tak dojde ke smazani ciselniku (aby nedoslo pri dalsim volani metody VyrobaDBDateTimePrepare ke stahnuti prazdneho ciselniku)
                        try
                        {
                            if (File.Exists(dstFile))
                                File.Delete(dstFile);
                        }
                        catch (Exception ex2)
                        {
                            Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                        }

                        throw ex;
                    }
                }

                //Prenos databazoveho souboru slqce prijemky pro temrinal
                long fileLength = (new FileInfo(dstFile)).Length;

                byte[] dbdata = new byte[fileLength];

                FileStream fs = null;
                try
                {
                    fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                    int bytesread = fs.Read(dbdata, 0, (int)fileLength);
                    fs.Close();
                    fs = null;
                    if (bytesread != (int)fileLength)
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "VyrobaDB: bytesread != (int)fileLength");

                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }

                File.Delete(dstFile);

                return dbdata;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        [WebMethod]
        public byte[] VyrobaDBProduction(byte terminalID, DateTime lastDateTime)
        {
            try
            {
                //string srcFile = "Už neexistuje..";
                //string dstFile = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "Production.sdf");
                string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Production + Common.PRD);

                if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

                if (!File.Exists(dstFile)) //Jestlize soubor jiz existuje, pak ho jen znovu poslat a negenerovat.
                {
                    try
                    {
                        //File.Copy(srcFile, dstFile, true); //Pokud soubor jiz existuje, tak se prepise

                        SQLite_Helper helper = new SQLite_Helper();
                        if (!helper.SQLite_CreateFile(dstFile, Common.Production))
                        {
                            throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Production);
                        }

                    }
                    catch (Exception ex)
                    {

                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                        // pokud nastala chyba pri naplnovani ciselniku, tak dojde ke smazani ciselniku (aby nedoslo pri dalsim volani metody VyrobaDBDateTimePrepare ke stahnuti prazdneho ciselniku)
                        try
                        {
                            if (File.Exists(dstFile))
                                File.Delete(dstFile);
                        }
                        catch (Exception ex2)
                        {
                            Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                        }

                        throw ex;
                    }
                }

                //Prenos databazoveho souboru slqce prijemky pro temrinal
                long fileLength = (new FileInfo(dstFile)).Length;

                byte[] dbdata = new byte[fileLength];

                FileStream fs = null;
                try
                {
                    using (fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        int bytesread = fs.Read(dbdata, 0, (int)fileLength);
                        fs.Close();
                        fs = null;
                        if (bytesread != (int)fileLength)
                            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "VyrobaDB: bytesread != (int)fileLength");
                    }
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }

                File.Delete(dstFile);

                return dbdata;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Vytvori InternalState datovou predlohu pro terminal
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="lastDateTime"></param>
        /// <returns></returns>
        [WebMethod]
        public string VyrobaDBInternalStatePrepare(byte terminalID, DateTime lastDateTime)
        {
            //string filename = terminalID.ToString() + "_VyrobaCE.sdf";
            // naplneni daty
            byte[] data = VyrobaDBInternalState(terminalID, lastDateTime);

            //string filename = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "InternalState.sdf");
            string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.InternalState + Common.PRD);
            FileStream fs = null;
            try
            {
                //fs = new FileStream(Path.Combine(UploadPath, filename), FileMode.Create);
                fs = new FileStream(dstFile, FileMode.Create);
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return Path.GetFileName(dstFile);
        }

        /// <summary>
        /// Vytvori production datovou predlohu pro terminal
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="lastDateTime"></param>
        /// <returns></returns>
        [WebMethod]
        public string VyrobaDBProductionPrepare(byte terminalID, DateTime lastDateTime)
        {
            //string filename = terminalID.ToString() + "_VyrobaCE.sdf";
            // naplneni daty
            byte[] data = VyrobaDBProduction(terminalID, lastDateTime);

            //string filename = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "Production.sdf");
            string filename = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Production + Common.PRD);
            FileStream fs = null;
            try
            {
                //fs = new FileStream(Path.Combine(UploadPath, filename), FileMode.Create);
                fs = new FileStream(filename, FileMode.Create);
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return Path.GetFileName(filename);
        }


        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba VyrobaTables()
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba vyrobads = new Fask.Interfaces.DataSets.Vyroba();
                provider.GetTables(vyrobads);
                return vyrobads;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba VyrobaHlavicky(byte terminalID)
        {
            try
            {
                return provider.GetHlavicky(terminalID);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba VyrobaPolozky(Fask.Server.Interfaces.Classes_Vyroba.VyrobniPrikazHlavicka hlavicka)
        {
            try
            {
                return provider.GetPolozky(hlavicka);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }



        /// <summary>
        /// Pokusí se opìtovnì nahrát všechny soubory Production s pøíponou SDF znovu do databáze
        /// </summary>
        /// <param name="terminalID">virtuální ID terminálu, který operaci provádí</param>
        /// <param name="foldername">Název adreáøe ze kterého se opìtovný import provádí, relativní cesta k ROOT adreáøi serveru nebo absolutní cesta</param>
        /// <returns></returns>
        [WebMethod]
        public bool ProcessProductionData_AllFromDirectory(byte terminalID, string foldername)
        {
            try
            {
                string dirPath = Path.Combine(Fask.MyPath.Path.RootPath, foldername);

                if (Directory.Exists(dirPath))
                {
                    string[] files = Directory.GetFiles(dirPath, "*.sdf", SearchOption.TopDirectoryOnly);
                    foreach (string file in files)
                    {
                        ProcessProductionDataThreaded((object)(new ProcessProductionObject(terminalID, file)));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
                //throw ex;
            }
        }

        //[WebMethod]
        //public void ProcessProductionDataUploaded(byte terminalID, string filename)
        //{
        //    FileStream fs = null;
        //    //string fp = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + filename);
        //    string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + filename);
        //    try
        //    {
        //        FileInfo fi = new FileInfo(dstFile);
        //        fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read);
        //        byte[] data = new byte[fi.Length];
        //        int bytesReaded = fs.Read(data, 0, data.Length);
        //        fs.Close();
        //        fs = null;
        //        ProcessProductionData(terminalID, data);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs = null;
        //        }
        //        File.Delete(Path.Combine(dstFile, filename));
        //    }
        //}


        [Obsolete("Nahrazeno metodou [Production_HistoryFilters]", false)]
        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba Production_History(string UserID, string MachineID, string sopnumbe, DateTime? dateFrom, DateTime? dateTo, int nLastActions)
        {
            try
            {
                return provider.ProductionHistory(UserID, MachineID, sopnumbe, dateFrom, dateTo, nLastActions);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }
       

        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba Production_Soubeh(Guid soubehGUID)
        {
            try
            {
                return provider.Soubeh(soubehGUID);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }


        //15.1.2019 - funkionalita online pro reseni MTJ pro fy Kruzik
        /// <summary>
        /// Vraci data s hlavickou operace a radkem vyrobniho prikazu ...
        /// </summary>
        /// <param name="operaceID">zadany carovy kod operace</param>
        /// <param name="terminalID">cislo terminalu, ktery akci vyvolava</param>
        /// <param name="productionStateEnabled">Vraci informaci o nasledujicim povolenem stavu ... (zpusobuje prepis hodnoty pri odvodu ... )</param>
        /// <returns>Data hlavicky prikazu a radku operace</returns>
        [WebMethod]
        public Fask.Interfaces.DataSets.Vyroba Vyroba_Online_CheckOperation(string operaceID, string terminalID, out Fask.Server.Interfaces.Classes_Vyroba.ProductionState productionStateEnabled)
        {
            try
            {
                return provider.Vyroba_Online_CheckOperation(operaceID, terminalID, out productionStateEnabled);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        //17.1.2019 - funkionalita online pro reseni MTJ pro fy Kruzik
        /// <summary>
        /// Provadi online zapis odvodu vyroby do externiho reseni ...
        /// </summary>
        /// <param name="productionObject">Objekt produkce</param>
        /// <param name="message">navratova zprava</param>
        /// <returns>Data hlavicky prikazu a radku operace</returns>
        [WebMethod]
        public bool Vyroba_Online_WriteOperation(Fask.Server.Interfaces.Classes_Vyroba.ProductionObject productionObject, out string message)
        {
            try
            {
                return provider.Vyroba_Online_WriteOperation(productionObject, out message);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion

        #region Private metody


        //private string VyrobaDBDateTimePrepare(byte terminalID, DateTime lastDateTime)
        //{
        //    //string filename = terminalID.ToString() + "_VyrobaCE.sdf";
        //    // naplneni daty
        //    byte[] data = VyrobaDBDateTime(terminalID, lastDateTime);

        //    //string filename = Path.Combine(rootpath, @"SQLiteDBs\" + terminalID.ToString() + @"\" + "VyrobaCE.sdf");
        //    string filename = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalID.ToString() + Common.Backslash + Common.Vyroba + Common.PRD);
        //    FileStream fs = null;
        //    try
        //    {
        //        //fs = new FileStream(Path.Combine(UploadPath, filename), FileMode.Create);
        //        fs = new FileStream(filename, FileMode.Create);
        //        fs.Write(data, 0, data.Length);
        //        fs.Flush();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs = null;
        //        }
        //    }
        //    return Path.GetFileName(filename);
        //}


        private class ProcessProductionObject
        {
            public byte terminalID;
            public string dataPath;
            public ProcessProductionObject(byte terminalID, string dataPath)
            {
                this.terminalID = terminalID;
                this.dataPath = dataPath;
            }
        }

        private bool ProcessProductionData(object o)
        {
            ProcessProductionObject ppo = o as ProcessProductionObject;
            if (ppo == null)
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "ProcessProductionDataThreaded(object o): parameter is null");
                return false;
            }

            List<Exception> exceptions = new List<Exception>();
            //IDbTransaction iTrans1 = null;
            try
            {
                Fask.Interfaces.DataSets.Vyroba vds = new Fask.Interfaces.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba vdsce = new Fask.SQLiteDBs.DataSets.Vyroba();

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(ppo.dataPath))
                {
                    ConVyr.Fill_Production(vdsce.Production);
                    ConVyr.Fill_Production_Sources(vdsce.Production_Sources);
                    ConVyr.Fill_UserEvents(vdsce.UserEvents);
                    ConVyr.Fill_Production_SN(vdsce.Production_SN);
                }

                foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow prow in vdsce.Production)
                {
                    prow.SetAdded();
                    vds.Production.ImportRow(prow);
                }

                provider.UpdateProduction(vds.Production);

                foreach (Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow psrow in vdsce.Production_Sources)
                {
                    psrow.SetAdded();

                    #region osetreni delky ITEMNAME
                    int delkaPodretezce = 51;

                    if (!string.IsNullOrEmpty(psrow.ITEMNAME) && psrow.ITEMNAME.Length >= delkaPodretezce)
                    {
                        psrow.ITEMNAME = psrow.ITEMNAME.Substring(0, delkaPodretezce);
                    } 
                    #endregion

                    vds.Production_Sources.ImportRow(psrow);
                }

                provider.UpdateProduction_Sources(vds.Production_Sources);

                foreach (Fask.SQLiteDBs.DataSets.Vyroba.UserEventsRow uerow in vdsce.UserEvents)
                {
                    uerow.SetAdded();
                    vds.UserEvents.ImportRow(uerow);
                }
                provider.UpdateUserEvents(vds.UserEvents);

                foreach (Fask.SQLiteDBs.DataSets.Vyroba.Production_SNRow prow in vdsce.Production_SN)
                {
                    prow.SetAdded();
                    vds.Production_SN.ImportRow(prow);
                }

                provider.UpdateProduction_SN(vds.Production_SN);

                // 11.8.2010 JiS: zaloha korektne zpracovanych dat se delat nebude, protoze to zabira prilis mnoho prostoru,
                // mozna to nechat konfiguracne, ale prijde mi to zbytecne ...,
                // pokud se data ulozi korektne, tak neni duvod, je v podstate uchovavat. ...
                /*
                try
                {
                    //Presune zpracovana data do processed adresare
                    string dstDir = Path.Combine(rootpath, System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"]);
                    if (!Directory.Exists(dstDir))
                        Directory.CreateDirectory(dstDir);

                    File.Move(ppo.dataPath, Path.Combine(dstDir, Path.GetFileName(ppo.dataPath)));
                }
                catch (Exception ex)
                {
                    Log.writeErrorLog("ProcessProductionData: " + ex.Message);
                    File.Delete(ppo.dataPath);
                    Log.writeErrorLog("ProcessProductionData: File deleted(" + ppo.dataPath + ")");
                }
                */

                File.Delete(ppo.dataPath);
                return true;

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //string dstDir = Path.Combine(rootpath, System.Configuration.ConfigurationManager.AppSettings["ErrorDataFileDirectory"]);
                try
                {
                    if (!Directory.Exists(Fask.MyPath.Path.ErrorDataFileDirectory))
                        Directory.CreateDirectory(Fask.MyPath.Path.ErrorDataFileDirectory);
                    File.Move(ppo.dataPath, Path.Combine(Fask.MyPath.Path.ErrorDataFileDirectory, ppo.terminalID + "_" + Path.GetFileName(ppo.dataPath)));
                }
                catch (Exception ex2)
                {
                    Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                exceptions.Insert(0, ex);
                Informations.SendMailStatic(
                    "\nProcessProductionData" +
                    "\n           Datum: " + DateTime.Now.ToString() +
                    "\n        Terminál: " + ppo.terminalID +
                    "\n   Datový soubor: " + ppo.dataPath +
                    "\n-----------------" +
                    "\n      Exceptions: " +
                    "\n" + Logging.Classes.ListException2String.ToString(exceptions)
                );

                return false;

            }
            finally
            {
            }
        }

        private void ProcessProductionDataThreaded(object o)
        {
            ProcessProductionData(o);
        }


        #endregion

        //[WebMethod]
        //public bool DeleteProcessedDirectory()
        //{
        //    try
        //    {
        //        Directory.Delete(System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"], true);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Informations.SendMailStatic("Exception Clearing Processed Directory : \n" + ex.Message + "\n" + ex.StackTrace);
        //        Log.writeErrorLog(ex.Message);
        //        return false;
        //    }
        //}

        //[WebMethod]
        //public bool DeleteErrorDirectory()
        //{
        //    try
        //    {
        //        Directory.Delete(System.Configuration.ConfigurationManager.AppSettings["ErrorDataFileDirectory"], true);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Informations.SendMailStatic("Exception Clearing Error Directory : \n" + ex.Message + "\n" + ex.StackTrace);
        //        Log.writeErrorLog(ex.Message);
        //        return false;
        //    }
        //}

        //[WebMethod]
        //public bool DeleteLogFile()
        //{
        //    try
        //    {
        //        File.Delete(System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"]);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Informations.SendMailStatic("Exception Clearing Error Directory : \n" + ex.Message + "\n" + ex.StackTrace);
        //        Log.writeErrorLog(ex.Message);
        //        return false;
        //    }
        //}

        //[WebMethod]
        //public Production.DataServices.VyrobaDataSet.ProductionDataTable OpenedCorrection_test(string userID, string MachineID)
        //[WebMethod]
        //public string OpenedCorrection_test(string userID, string MachineID)
        //{
        //    try
        //    {
        //        Production.DataServices.VyrobaDataSet ds_vyroba = new Production.DataServices.VyrobaDataSet();
        //        //Net.VyrobaDataSetTableAdapters.ProductionTableAdapter ta_production = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.ProductionTableAdapter();
        //        System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
        //        ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
        //            "Select top 1 * " + //prvni vyskyt
        //            "From Production " +
        //            "Where isnull(UserID,'')=@UserID " +
        //            "AND isnull(MachineID,'')=@MachineID " +
        //            "AND TIMECRID is not null " + //jedna se o korekci ... 
        //            "Order by dateeve desc" +
        //            ""
        //            );

        //        //ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
        //        //    //"Select top 1 [CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMMJ],[ORD],[TIMEMODE],[TIMEPREPSTART],[TIMEPREPSTOP],[TIMEPREP],[TIMEUNIT],[TIMESTART],[TIMESTOP],[TIMECORSTART],[TIMECORSTOP],[TIMECOR],[TIMECRID],[id],[loginid],[machineid],[operationid],[dateeve],[qty],[qtyReal],[QTYPACK],[QTYPACKMJ],[description],[BarcodeP],[UserID],[TermID],[ISOK],[GUID],[SOUBEHGUID],[qtyOld],[idVS],[dateedit],[CORRGUID] " + //prvni vyskyt
        //        //    "Select top 1 [CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMMJ],[ORD],[TIMEMODE],[TIMEPREPSTART],[TIMEPREPSTOP],[TIMEPREP],[TIMEUNIT],[TIMESTART],[TIMESTOP],[TIMECORSTART],[TIMECORSTOP],[TIMECOR],[TIMECRID],[id],[loginid],[machineid],[operationid],[dateeve],[qty],[qtyReal],[QTYPACK],[QTYPACKMJ],[description],[BarcodeP],[UserID],[TermID],[ISOK],[GUID],[SOUBEHGUID],[dateedit],[CORRGUID] " + //prvni vyskyt
        //        //    "From Production " +
        //        //    "Where isnull(UserID,'')=@UserID " +
        //        //    "AND isnull(MachineID,'')=@MachineID " +
        //        //    "AND TIMECRID is not null " + //jedna se o korekci ... 
        //        //    "Order by dateeve desc" +
        //        //    ""
        //        //    );

        //        ta_production.SelectCommand.Parameters.AddWithValue("@UserID", userID);
        //        ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

        //        ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(this._ProductionDataServicesConnectionString);
        //        ta_production.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
        //        ta_production.Fill(ds_vyroba.Production);

        //        string output = Newtonsoft.Json.JsonConvert.SerializeObject(ds_vyroba);
        //        return output;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
