using Fask.Logging;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fask.MST_W_Server.BL
{
    public class VyrobaBL
    {

        #region lokalni promenne
        //public enum ProcessVydejState
        //{
        //    Uvolnit,
        //    Zpracovat,
        //    ZpracovatAPokracovat
        //}

        private Fask.Server.Interfaces.IWebModule provider = null;
        //const string VydejDBFileExtension = @".vi";

        #endregion

        #region private metody

        /// <summary>
        /// Metoda pro kontrolu licence
        /// </summary>
        /// <returns>True - Licence je validni, False- Licence neni validni</returns>
        private bool isLicenseValid()
        {
            Licensing.License lic = HttpContext.Current.Application[Constants.Common.license] as Licensing.License;

            // Zkontrolujte, zda lic není null
            if (lic != null)
            {
                // Zkontrolujte, zda licence je platná a nevypršela
                if (!lic.isValid || lic.isExpirated)
                    return false;

                return true;
            }

            // V případě, že lic je null, vrátíme false (neplatná licence)
            return false;
        }

        /// <summary>
        /// Metoda Wraper pro odchyceni vyjimky a ulozeni vysledku volani ...
        /// </summary>
        /// <param name="sopnumbe">číslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <param name="so">reference na StatusObject</param>
        private void GenerateDavkaWrapper(string sopnumbe, string skl_id, ref StatusObject so)
        {
            try
            {
                so.StatusText = "Generování dávky dokladu '" + sopnumbe + "'";
                so.Write();

                int result = GenerateDavka(sopnumbe, skl_id);

                so.StatusText = result.ToString();
                so.Finished = true;
                so.Write();

            }
            catch (Exception ex)
            {
                so.Exception = true;
                so.Finished = true;
                so.StatusText = ex.Message;
                so.Write();
            }
        }


        #endregion

        #region konstruktory
        public VyrobaBL()
        {

        }

        public VyrobaBL(Fask.Server.Interfaces.IWebModule provider)
        {
            this.provider = provider;
        }
        #endregion

        #region inicializace provideru
        public void Initialize(Func<string, string> map_path_function)
        {

            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = string.Empty;// Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Vyroba;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
                    providerAssemblyPath = providerAssemblyPathGlobal;

                if (!String.IsNullOrEmpty(providerAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(map_path_function(@"~/" + providerAssemblyPath));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.IWebModule).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.IWebModule)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
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

        #region Generovani davky z terminalu pro Vydej


        // 19.4.2016 JiS
        // Uprava pro dlouhotrvajici operace.
        // 1) terminal vola GenerateDavkaRequest a vraci se mu StatusObject podle ktereho se dale ridi proces stahovani
        //      => tato metoda ridi stav stahovani pres StatusObject (rozsireno o prvek finished
        // 2) terminal nasledne v cyklu se dotazuje metodou GenerateDavkaStatus na StatuObject pro generovani teto davky
        //      => konci se bud vyjimkou SO.Exception = true ve SO.StatusText je text vyjimky
        //      => nebo SO.Finished = true ve SO.StatusText je ID nove generovane davky
        // Pozn: a) GenerateDavka je puvodni webova metoda, ktera je nyni neverejna (muze byt i privatni)
        //       b) GenerateDavkaWrapper je obalka pro volani puvodni metody GenerateDavka, 
        //          ktera zachycuje a zpracovava vyjimky puvodni metody pro StatusObject
        //       c) GenerateDavkaRequest vyvolava GenerateDavkaWrapper v novem vlakne
        //
        // Proces:
        //  terminal <-> GenerateDavkaRequest -> new Thread(GenerateDavkaWrapper) -> GenerateDavka
        //  terminal <-> GenerateDavkaStatus

        /// <summary>
        /// Metoda která započne připravu generovaní předlohy IS do Našich struktur
        /// </summary>
        /// <param name="sopnumbe">číslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [Description("Metoda která započne připravu generovaní předlohy IS do Našich struktur")]
        public StatusObject GenerateDavkaRequest(string sopnumbe, string skl_id)
        {
            StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VyrobaGenerateDavka, sopnumbe, skl_id);
            if (so.Exists)
            {
                return so; //vrati informaci o stavu provedeni. Pokud se chce generovat znovu, tak se musi nejprve provest smazani statusu ... 
            }
            // kdyz neexistuje, tak je to novy pozadavek ...

            so.Finished = so.Exception = false;
            so.StatusText = "Příprava dávky dokladu '" + sopnumbe + "'";
            so.Write();

            // Vyvolani noveho threadu ...
            new System.Threading.Thread(() => GenerateDavkaWrapper(sopnumbe, skl_id, ref so)).Start();

            return so;
        }



        /// <summary>
        /// Metoda která provadí samotne generování dokladu z IS do našich struktur
        /// </summary>
        /// <param name="sopnumbe">číslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <returns> ID chyby </returns>
        [Description("Metoda která provadí samotne generování dokladu z IS do našich struktur")]
        public int GenerateDavka(string sopnumbe, string skl_id)
        {
            TracId tracid = new TracId(null, null, null, "Vyroba.GenerateDavka:" + (sopnumbe ?? string.Empty) + "," + (skl_id ?? string.Empty));
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion

                #region sleep test
                //try
                //{
                //    int sleep = 1000; // 1s
                //    sleep *= 60; // 1min
                //    sleep *= 30; // 0,5h
                //    Debug.WriteLine("Sleep start:" + DateTime.Now.ToString());
                //    System.Threading.Thread.Sleep(sleep);
                //    Debug.WriteLine("Sleep stop:" + DateTime.Now.ToString());
                //}
                //catch (Exception ex)
                //{
                //    Log.writeErrorLog("Prijem generate davka sleep error:\n" + ex.Message);
                //}
                #endregion


                string statusinfo = string.Empty;

                try
                {
                    if ((provider != null) && (provider is Fask.Server.Interfaces.Vyroba.IVyroba_00))
                    {
                        Objednavka objednavka = new Objednavka();
                        Sklad sklad = new Sklad();

                        objednavka.ID = sopnumbe;
                        objednavka.CisloDavky = string.Empty;

                        sklad.ID = skl_id;

                        StatusInfo si = (provider as Fask.Server.Interfaces.Vyroba.IVyroba_00).Vyroba_GenerateDavka(objednavka, sklad);
                        return si.ID;
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Sopnumbe:'" + sopnumbe);
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                    throw ex;
                }

                string msg = "Provider v VyrobaBL.cs > 'GenerateDavka(string sopnumbe, string skl_id)' nenastaven.";
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.NotImplemented, msg);
                #region trace
                Trac.Write(msg, tracid);
                #endregion
                throw new Exception(msg);

            }
            finally
            {
                #region trace
                Trac.Write("End", tracid);
                #endregion
            }
        }

        /// <summary>
        /// Metoda která vrací status o prubehu generování
        /// </summary>
        /// <param name="sopnumbe">číslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [Description("Metoda která vrací status o prubehu generování")]
        public StatusObject GenerateDavkaStatus(string sopnumbe, string skl_id)
        {
            StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VyrobaGenerateDavka, sopnumbe, skl_id);
            if (so.Exists)
                return so;
            else
                return null;
        }

        /// <summary>
        /// Metoda která slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)
        /// </summary>
        /// <param name="sopnumbe">číslo objednávky</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <returns>StatusObjekt - informace o zpracování</returns>
        [Description("Metoda která slouzi terminalu ke smazani statusu a znovu generovani prikazu(davky)")]
        public StatusObject GenerateDavkaStatusDelete(string sopnumbe, string skl_id)
        {
            StatusObject so = StatusObject.Create(MyPath.Path.StatusObjectDirectory, StatusObject.Operations.VyrobaGenerateDavka, sopnumbe, skl_id);
            so.Delete();
            return so;
        }



        #endregion


    }
}