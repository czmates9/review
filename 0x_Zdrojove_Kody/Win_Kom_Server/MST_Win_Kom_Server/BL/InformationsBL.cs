using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fask.MST_W_Server.BL
{
    public class InformationsBL
    {


        #region lokalni promenne

        public Fask.Server.Interfaces.IMST provider = null;
      

        #endregion

        #region konstruktor
        public InformationsBL()
        {

        }

        public InformationsBL(Fask.Server.Interfaces.IMST provider)
        {
            this.provider = provider;
        }
        #endregion

        #region inicializace
        public void Initialize(Func<string, string> map_path_function)
        {

            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Informations;
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
                                if (typeof(Fask.Server.Interfaces.IMST).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.IMST)providerAssemlby.CreateInstance(t.FullName);
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



        #region privatni metody

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

        #endregion


        public float? MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)
        {
            TracId tracid = new TracId(null, null, null, "MnozstviNaSklade_Itemnumber_Location");
            try
            {
                #region trace
                Trac.Write("Start", tracid);
                #endregion


                if ((provider != null) && (provider is Fask.Server.Interfaces.IMST))
                {
                    float? mnozstvi = null;

                    #region trace
                    Trac.Write("Provider start", tracid);
                    #endregion

                    try
                    {
                        mnozstvi = (provider as Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade_Itemnumber_Location).MnozstviNaSklade_Itemnumber_Location(ItemNumber, Location);



                        return mnozstvi;  
                    
                    
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        #region trace
                        Trac.Write(ex, tracid);
                        #endregion
                        throw ex;
                    }
                    finally
                    {
                        #region trace
                        Trac.Write("Provider end", tracid);
                        #endregion
                    }
                }

                string msg = "Provider v Informations.asmx > 'MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)' nenastaven.";
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



        #region online metody


        public Fask.Server.Interfaces.DataSets.Location Online_GetMaterial(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
            Fask.Server.Interfaces.DataSets.Location dsLocation = new Fask.Server.Interfaces.DataSets.Location();

            try
            {
                if (provider != null)
                {

                    return (provider as Fask.Server.Interfaces.Informations.IInformations2_FEFOFIFO).FEFOFIFO_Online(itemnmbr ?? string.Empty, skl_id ?? string.Empty, serltnum ?? string.Empty, doc_id ?? string.Empty, locncode);
                    //return provider.Prodej_Online_GetMaterial(itemnmbr ?? string.Empty, skl_id ?? string.Empty, serltnum ?? string.Empty, doc_id ?? string.Empty, locncode);
                }
                //1.8.2025 MaR zakomentovano, protoze je to docasne prebrano z MST_06 a zde nejsou prozatim dostupne komponenty
                #region docasne zakomentovano, prebrano z MST_06
                //else
                //{


                //    string prijem_generateData = System.Configuration.ConfigurationManager.AppSettings["Prodej_GetLokaci_Action"].Trim();
                //    if (prijem_generateData.Length != 0)
                //    {
                //        xConnection1 = new XConnection(xdb, xconnstring);
                //        XCommand adpacommand = new XCommand(xdb, prijem_generateData);
                //        adpacommand.CommandType = CommandType.StoredProcedure;

                //        // parametry
                //        adpacommand.Parameters.Add((new XParameter(xdb, "@Itemnmbr", XDbType.NVarChar, 31)).DatabaseParameter);
                //        adpacommand.Parameters.Add((new XParameter(xdb, "@Skl_id", XDbType.NVarChar, 20)).DatabaseParameter);
                //        adpacommand.Parameters.Add((new XParameter(xdb, "@Serltnum", XDbType.NVarChar, 21)).DatabaseParameter);
                //        adpacommand.Parameters.Add((new XParameter(xdb, "@doc_id", XDbType.NVarChar, 12)).DatabaseParameter);

                //        // hodnoty vstupnich parametru
                //        ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
                //        ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;
                //        ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value = serltnum;
                //        ((IDataParameter)adpacommand.Parameters["@doc_id"]).Value = doc_id;

                //        adpacommand.Connection = xConnection1.DatabaseConnection;
                //        xConnection1.Open();

                //        XDataAdapter xda = new XDataAdapter(xdb);
                //        xda.SelectCommand = adpacommand.DatabaseCommand;

                //        ((DbDataAdapter)xda.DatabaseDataAdapter).Fill(ds, ds.Palety.TableName);

                //        // import do noveho datasetu
                //        if (ds != null && ds.Palety.Count > 0)
                //        {
                //            DateTime dtnow = DateTime.Now;
                //            foreach (var paletyrow in ds.Palety)
                //            {
                //                Fask.Server.Interfaces.DataSets.Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                //                newrow.ITEMNMBR = paletyrow.IsITEMNMBRNull() ? string.Empty : paletyrow.ITEMNMBR;
                //                newrow.ITEMDESC = string.Empty;
                //                newrow.QTYSHPPD_DEF = paletyrow.QTYSHPPD;
                //                newrow.QTYSHPPD = paletyrow.QTYSHPPD;
                //                newrow.SERLTNUM = paletyrow.IsSERLTNUMNull() ? string.Empty : paletyrow.SERLTNUM;
                //                newrow.SKL_ID = paletyrow.IsSKL_IDNull() ? string.Empty : paletyrow.SKL_ID;
                //                newrow.LOCNCODE = paletyrow.IsLOCNCODENull() ? string.Empty : paletyrow.LOCNCODE;
                //                newrow.DATECHANGE = dtnow;
                //                newrow.SetEXPIRATIONNull();

                //                dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                //            }
                //        }
                //    }
                #endregion

                return dsLocation;

            }
            catch (Exception ex)
            {
                //Log.writeErrorLog(ex.Message);
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            //1.8.2025 MaR zakomentovano, protoze je to docasne prebrano z MST_06 a zde nejsou prozatim dostupne komponenty
            //finally
            //{
            //    if (xConnection1 != null && (xConnection1.State & ConnectionState.Open) == ConnectionState.Open)
            //        xConnection1.Close();
            //}
        }


        #endregion

    }
}