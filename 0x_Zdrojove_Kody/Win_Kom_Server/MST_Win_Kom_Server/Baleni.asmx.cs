using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using Fask.Server.Interfaces.Classes;
//using Vyroba.Classes;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Summary description for Baleni
    /// </summary>
    [WebService(Namespace = "http://Baleni.fask.cz/", Description = "Služba pro baleni")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Baleni : System.Web.Services.WebService
    {


        #region Parametry

        Fask.Server.Interfaces.Baleni.IBaleni provider = null;

        #endregion

        #region C'tor

        public Baleni()
        {
            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Baleni;
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
                                if (typeof(Fask.Server.Interfaces.Baleni.IBaleni).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Baleni.IBaleni)providerAssemlby.CreateInstance(t.FullName);
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
        }

        #endregion


        /// <summary>
        /// Vraci data, ktera se budou tisknout.
        /// </summary>
        /// <param name="objednavkacislo">Cislo objednavky.</param>
        /// <param name="balikcislo">Poradove cislo baliku, ktere vraci procedura</param>
        /// <returns>Obecny dataset s daty, ktere se poslou na tiskarnu.</returns>
        [WebMethod(Description = "Vraci data, ktera se budou tisknout")]
        public DataSet GetBaleniData(string objednavkacislo, out int balikcislo)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Baleni.IBaleni))
                {
                    return provider.GetBaleniData(objednavkacislo, out balikcislo);
                }

                string msg = "Provider v Baleni.asmx > 'GetBaleniData(string objednavkacislo, out int balikcislo)' nenastaven.";
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
        /// Potvrzeni o vytisknuti baliku.
        /// </summary>
        /// <param name="userid">Login uzivatele.</param>
        /// <param name="terminalid">Terminal ID.</param>
        /// <param name="objednavkacislo">Cislo objednavky.</param>
        /// <param name="balikcislo">Cislo baliku.</param>
        /// <param name="printedtime">Datum tisku.</param>
        /// <returns>Status (OK, ERROR)</returns>
        [WebMethod(Description = "Potvrzeni o vytisknuti baliku")]
        public StatusBaleni BaleniDataCommit(string userid, byte terminalid, string objednavkacislo, int balikcislo, DateTime printedtime)
        {
            try
            {
                if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IProduction))
                {
                    return provider.BaleniDataCommit(userid, terminalid, objednavkacislo, balikcislo, printedtime);
                }

                string msg = "Provider v Baleni.asmx > 'BaleniDataCommit(string userid, byte terminalid, string objednavkacislo, int balikcislo, DateTime printedtime)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
                throw new Exception(msg);

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

    }
}
