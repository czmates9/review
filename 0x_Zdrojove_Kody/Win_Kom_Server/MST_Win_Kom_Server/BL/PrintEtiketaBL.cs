using Fask.ModuleSql;
using Fask.Server.Interfaces;
using Fask.Server.Interfaces.DataSets;
using MST_Print_Server_ZPL_Printing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;

namespace Fask.MST_W_Server.BL
{
    public class PrintEtiketaBL
    {
        private IMST provider;
        private readonly Func<string, string> map_path_function;

        public PrintEtiketaBL(Func<string, string> map_path_function)
        {
            this.map_path_function = map_path_function;
            Initialize();
        }

        private void Initialize()
        {
            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Tisky2;
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
                                    //provider = (Fask.Server.Interfaces.Vydej.IVydej)providerAssemlby.CreateInstance(t.FullName);
                                    provider = (Fask.Server.Interfaces.IMST)providerAssemlby.CreateInstance(t.FullName);
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
                        //return config;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }



        public bool GenericEtiketaPrint(int terminalID, ref string templateName, TiskParams printerParams, ref Fask.Server.Interfaces.DataSets.DSValues data, int pocetVytisku)
        {
            Tracing.TracId _tracid = new Fask.Tracing.TracId(null, terminalID, null, "Tisk.Etiketa");

            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                Tracing.Trac.Write(">>>>>>>>> Etiketa : start", _tracid);

                bool succed = false;


                #region Provider

                Tracing.Trac.Write("Etiketa : provider.tiskmetodaetiketa : start", _tracid);

                if ((provider != null) && (provider is Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa))
                {
                    try
                    {

                        // nova funkce cez provider pro HANIBAL
                        if (!((Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa)provider).TiskMetodaEtiketa(terminalID, ref templateName, ref data, pocetVytisku))
                            throw new Exception("chyba...");
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        throw ex;
                    }
                }


                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, templateName);
                }

                Tracing.Trac.Write("Etiketa : provider.tiskmetodaetiketa : end", _tracid);

                #region Prekopisovani dat z datasetu do dictionery plus zalogovani

                Tracing.Trac.Write("Etiketa : zpracovani parametru tisku : start", _tracid);

                StringBuilder logstring = new StringBuilder();

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    logstring.AppendLine("*************** Etiketa ***************)");
                    logstring.AppendLine("TerminalID   : " + terminalID);
                    //logstring.AppendLine("TemplateName : " + templateName);
                }

                string printerType = LoadPrinterFromXML(printerParams);
                if (!string.IsNullOrEmpty(printerType))
                    printerParams.CONFIG_TYPE = printerType;

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    logstring.AppendLine("==== Printer params ====");
                    logstring.AppendLine(" Name:" + printerParams.CONFIG_NAME);
                    logstring.AppendLine("   IP:" + printerParams.CONFIG_IP + ":" + printerParams.CONFIG_IP_PORT);
                    logstring.AppendLine("  COM:" + printerParams.CONFIG_COM);
                    logstring.AppendLine(" TYPE:" + printerParams.CONFIG_TYPE);
                }

                #region pridani hodnot do dat
                prepareTiskValues_new(data);
                #endregion

                //Pridani parametru poctu vytisku do datovych poli ...
                if (data.Values.FindByKey("PocetVytisku") == null)
                    data.Values.AddValuesRow("PocetVytisku", pocetVytisku.ToString());
                data.AcceptChanges();

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    logstring.AppendLine("==== Values =====");
                }

                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow vrow in data.Values)
                {
                    if (!dict.ContainsKey(vrow.Key))
                    {
                        dict.Add(vrow.Key, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].RemoveDiacritic ? RemoveDiacriticsFromString(vrow.Value) : vrow.Value);
                        if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                            logstring.AppendLine(vrow.Key + ":" + vrow.Value);
                    }
                }

                if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, logstring.ToString());
                }

                Tracing.Trac.Write("Etiketa : zpracovani parametru tisku : end", _tracid);

                #endregion

                Tracing.Trac.Write("Etiketa : graficky provider inicializace : start", _tracid);

                string providerGraphicsAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Graphics;
                if (!string.IsNullOrEmpty(providerGraphicsAssemblyPath))
                    providerGraphicsAssemblyPath = map_path_function(@"~/"+providerGraphicsAssemblyPath);

                Tracing.Trac.Write("Etiketa : graficky provider inicializace : end", _tracid);


                Tracing.Trac.Write("Etiketa : zpracovani sablony : start", _tracid);

                string serverpath = map_path_function(@"~/" + Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].TemplateDirectory);
                string templateFullPath = Path.Combine(serverpath, templateName.Trim());


                MST_Print_Server_ZPL_Printing.StringComposingEtiketa scetiketa = new MST_Print_Server_ZPL_Printing.StringComposingEtiketa(
                    templateFullPath,
                    printerParams,
                    dict, //tyto data pridani prace s key a value, rozdelit ITEMDESC po 60znacich
                    providerGraphicsAssemblyPath,
                    1); // pocet vytisku se presunul do dat etikety a tiskne se pouze jedna tiskova uloha ... // pocetVytisku);

                succed = scetiketa.Compose();

                Tracing.Trac.Write("Etiketa : zpracovani sablony : end", _tracid);

                Tracing.Trac.Write("Etiketa : odeslani ulohy na tiskarnu : start", _tracid);

                MST_Print_Server_ZPL_Printing.StringPrinting sprint = new MST_Print_Server_ZPL_Printing.StringPrinting(printerParams);
                sprint.PrintEncodingPage = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].PrintEncodingPage;
                sprint.user = new User(
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserName,
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserPassword,
                    Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].DomainName);
                int count = sprint.Print(scetiketa.FinalStrings, "MST Print: " + templateName);
                succed = count == 0;

                Tracing.Trac.Write("Etiketa : odeslani ulohy na tiskarnu : end", _tracid);

                #endregion

                return succed;
            }
            finally
            {
                Tracing.Trac.Write("<<<<<<<<< Etiketa : end", _tracid);
            }
        }

        public void SoupisBezNavratu(int terminalID, TiskParams printerParams, Fask.Server.Interfaces.DataSets.DSValues dataHeader, List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, Fask.Server.Interfaces.DataSets.DSValues dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            try
            {
                Soupis(terminalID, printerParams, ref dataHeader, ref dataRowList, ref dataFooter, templateHeader, templateRow, templateFooter, pocet);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public bool Soupis(int terminalID, TiskParams printerParams, ref Fask.Server.Interfaces.DataSets.DSValues dataHeader, ref List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, ref Fask.Server.Interfaces.DataSets.DSValues dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            Tracing.TracId _tracid = new Fask.Tracing.TracId(null, terminalID, null, "Tisk.Soupis");

            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                Tracing.Trac.Write(">>>>>>>>> Soupis : start", _tracid);



                Tracing.Trac.Write("Etiketa : provider.tiskmetodasoupis : start", _tracid);

                if ((provider != null) && (provider is Fask.Server.Interfaces.Tisky.ITisky2_MetodaSoupis))
                {
                    try
                    {

                        // nova funkce cez provider pro CARP
                        if (!((Fask.Server.Interfaces.Tisky.ITisky2_MetodaSoupis)provider).TiskMetodaSoupis(ref dataHeader, ref dataRowList, ref dataFooter))
                            throw new Exception("Chyba tisk provider! viz. Log!");
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                        throw ex;
                    }
                }

                Tracing.Trac.Write("Etiketa : provider.tiskmetodasoupis : end", _tracid);

                //bool succed = false;


                // hlavička
                Dictionary<string, string> dataHead = new Dictionary<string, string>();
                foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow vrow in dataHeader.Values)
                {
                    if (!dataHead.ContainsKey(vrow.Key))
                    {
                        dataHead.Add(vrow.Key, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].RemoveDiacritic ? RemoveDiacriticsFromString(vrow.Value) : vrow.Value);
                    }
                }

                // řádky
                List<Dictionary<string, string>> dataRows = new List<Dictionary<string, string>>();
                // projití všech řádků
                foreach (Fask.Server.Interfaces.DataSets.DSValues row in dataRowList)
                {
                    Dictionary<string, string> dataRadek = new Dictionary<string, string>();
                    // projití všech dat v řádku
                    foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow rowdata in row.Values)
                    {
                        if (!dataRadek.ContainsKey(rowdata.Key))
                        {
                            dataRadek.Add(rowdata.Key, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].RemoveDiacritic ? RemoveDiacriticsFromString(rowdata.Value) : rowdata.Value);
                        }
                    }
                    dataRows.Add(dataRadek);
                }

                // patička
                Dictionary<string, string> dataFoot = new Dictionary<string, string>();
                foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow vrow in dataFooter.Values)
                {
                    if (!dataFoot.ContainsKey(vrow.Key))
                    {
                        dataFoot.Add(vrow.Key, Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].RemoveDiacritic ? RemoveDiacriticsFromString(vrow.Value) : vrow.Value);
                    }
                }

                return Soupis(terminalID, printerParams, dataHead, dataRows, dataFoot, templateHeader, templateRow, templateFooter, pocet);
            }
            finally
            {
                Tracing.Trac.Write("<<<<<<<<< Soupis : end", _tracid);
            }
        }



        #region Private metody




        /// <summary>
        /// Metoda ktera vezme data, Zaloguje a vybere format tisku a provede tisk. 
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="printerParams">Objekt Informaci o tiskarne</param>
        /// <param name="dataHead">Data pro hlavičku</param>
        /// <param name="dataRows">Data pro řádky</param>
        /// <param name="dataFoot">Data do patičky</param>
        /// <param name="templateHeader">nazev Template pro Hlavičku</param>
        /// <param name="templateRow">nazev Template pro řadky</param>
        /// <param name="templateFooter">nazev Template pro patičku</param>
        /// <param name="pocet">počet vytisku</param>
        /// <returns>True - Vytisklo, False - chyba</returns>
        private bool Soupis(
                int terminalID,
                MST_Print_Server_ZPL_Printing.TiskParams printerParams,
                Dictionary<string, string> dataHead,
                List<Dictionary<string, string>> dataRows,
                Dictionary<string, string> dataFoot,
                string templateHeader,
                string templateRow,
                string templateFooter,
                int pocet)
        {
            bool succed = false;

            Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

            StringBuilder logstring = new StringBuilder();
            if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
            {
                logstring.AppendLine("*************** Soupis ***************)");
                logstring.AppendLine("TerminalID   : " + terminalID);
                logstring.AppendLine("TemplateHeaderName : " + templateHeader);
                logstring.AppendLine("TemplateRowName : " + templateRow);
                logstring.AppendLine("TemplateFooterName : " + templateFooter);
            }

            string printerType = LoadPrinterFromXML(printerParams);
            if (!string.IsNullOrEmpty(printerType))
                printerParams.CONFIG_TYPE = printerType;

            if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
            {
                logstring.AppendLine("==== Printer params ====");
                logstring.AppendLine(" Name:" + printerParams.CONFIG_NAME);
                logstring.AppendLine("   IP:" + printerParams.CONFIG_IP + ":" + printerParams.CONFIG_IP_PORT);
                logstring.AppendLine("  COM:" + printerParams.CONFIG_COM);
                logstring.AppendLine(" TYPE:" + printerParams.CONFIG_TYPE);
            }

            //Pridani parametru poctu vytisku do datovych poli ...
            if (!dataFoot.ContainsKey("PocetVytisku"))
            {
                dataFoot.Add("PocetVytisku", pocet.ToString());
            }
            else if (String.IsNullOrEmpty(dataFoot["PocetVytisku"]))
            {
                dataFoot["PocetVytisku"] = pocet.ToString();
            }

            if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
            {
                logstring.AppendLine("==== Values =====");
                logstring.AppendLine("==== Header values =====");
                foreach (var i in dataHead)
                {
                    logstring.AppendLine(i.Key + ":" + i.Value);
                }

                logstring.AppendLine("==== Rows values =====");
                foreach (var i in dataRows)
                {
                    foreach (var ir in i)
                    {
                        logstring.AppendLine(dataRows.IndexOf(i).ToString() + ":" + ir.Key + ":" + ir.Value);
                    }
                }

                foreach (var i in dataFoot)
                {
                    logstring.AppendLine(i.Key + ":" + i.Value);
                }

                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, logstring.ToString());
            }

            try
            {
                string serverpath = map_path_function(@"~/" + Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Directories[0].TemplateDirectory);
                string templateHeaderFullPath = Path.Combine(serverpath, templateHeader.Trim());
                string templateRowFullPath = Path.Combine(serverpath, templateRow.Trim());
                string templateFooterFullPath = Path.Combine(serverpath, templateFooter.Trim());

                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, "Tisk:" + printerParams.CONFIG_TYPE);

                // tisk ZPL
                if (string.IsNullOrEmpty(printerParams.CONFIG_TYPE))
                {
                    MST_Print_Server_ZPL_Printing.StringComposingSoupis scetiketa = new MST_Print_Server_ZPL_Printing.StringComposingSoupis(
                        templateHeaderFullPath,
                        templateRowFullPath,
                        templateFooterFullPath,
                        printerParams,
                        dataHead,
                        dataRows,
                        dataFoot,
                        1);  // pocet vytisku se presunul do dat etikety a tiskne se pouze jedna tiskova uloha ... // pocetVytisku);

                    succed = scetiketa.Compose();

                    MST_Print_Server_ZPL_Printing.StringPrinting sprint = new MST_Print_Server_ZPL_Printing.StringPrinting(printerParams);
                    sprint.PrintEncodingPage = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].PrintEncodingPage;
                    sprint.user = new User(
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserName,
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserPassword,
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].DomainName);
                    string finalString = string.Join(string.Empty, scetiketa.FinalStrings.ToArray());
                    List<string> finalList = new List<string>();
                    finalList.Add(finalString);
                    int count = sprint.Print(finalList, "MST Print: " + templateHeader + ":" + templateRow + ":" + templateFooter);
                    succed = count == 0;
                }
                else  // tisk rdlc
                {
                    string pompath = Path.Combine(serverpath, printerParams.CONFIG_TYPE); // cesta do slozky (A4, A5, ...)
                    string finalReportPath = Path.Combine(pompath, Path.GetFileNameWithoutExtension(templateHeader)) + ".rdlc"; // cesta k souboru rdlc + pridani pripony

                    MST_Print_Server_Soupis.SoupisCreator screator = new MST_Print_Server_Soupis.SoupisCreator(printerParams, dataHead, dataRows, dataFoot, null, pocet, finalReportPath);
                    screator.user = new User(
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserName,
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].UserPassword,
                        Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.PaletovyListek[0].DomainName);
                    succed = screator.Print();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return succed;
        }

        /// <summary>
        /// Metoda ktera z textu odstraní diakritiku
        /// </summary>
        /// <param name="s">Text na upravu</param>
        /// <returns>Upraveny text</returns>
        private static string RemoveDiacriticsFromString(string s)
        {
            s = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(s[i]) != UnicodeCategory.NonSpacingMark) sb.Append(s[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Metoda ktera načte informace o tiskarne z ConfigPrinter.xml
        /// </summary>
        /// <param name="printerParams"></param>
        /// <returns></returns>
        private string LoadPrinterFromXML(MST_Print_Server_ZPL_Printing.TiskParams printerParams)
        {
            try
            {

                string rootpath = map_path_function(@"~/");
                string filepath = Path.Combine(rootpath, @"ConfigPrinter.xml");

                if (!File.Exists(filepath))
                    return string.Empty;

                // to co se chce tisknout nějak jinak než standartně, tak zde bude uloženo
                // terminnál pošle data o tom, že se chce tisknout na určité tiskárně
                // v xml proběhne kontrola, zdali je v seznamu daná tiskárna
                // pokud je, dojde načtení typu a podle toho se rozhodne co tisknout ...
                XmlDocument xmldoc = null;
                xmldoc = new XmlDocument();
                xmldoc.Load(filepath);

                //Tiskarny
                XmlNodeList printerNodes = xmldoc.SelectNodes(@"/Settings/Printers/Printer");
                if (printerNodes.Count > 0)
                {
                    foreach (XmlElement node in printerNodes)
                    {
                        if (node.Attributes["name"].Value == printerParams.CONFIG_NAME)
                        {
                            printerParams.HEIGHT_PAPER_SIZE = Convert.ToInt32(node.Attributes["height"].Value);
                            printerParams.WIDTH_PAPER_SIZE = Convert.ToInt32(node.Attributes["width"].Value);
                            XmlAttribute attrKind = node.Attributes["kind"];
                            if ((attrKind != null) && (!String.IsNullOrEmpty(attrKind.Value)))
                            {
                                printerParams.PAPER_KIND = attrKind.Value;
                            }

                            return node.Attributes["type"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return string.Empty;
        }



        #endregion



        public Fask.Server.Interfaces.DataSets.DSValues TransformDataValues(Dictionary<string, string> data)
        {
            DSValues dataTarget = new DSValues();
            data.ToList().ForEach(v =>
            {
                dataTarget.Values.AddValuesRow(v.Key, v.Value);
            });
            dataTarget.AcceptChanges();
            return dataTarget;
        }
        #region old
//        /// <summary>
//        /// Modifies data collection with new values ...
//        /// </summary>
//        /// <param name="data">Data values to be printed</param>
//        /// <remarks>MaR 9.9.2024 priprava dat na sablonu</remarks>
//        private void prepareTiskValues_new(Fask.Server.Interfaces.DataSets.DSValues data)
//        {

//            string neco = data.Values.Count.ToString();
//            // Seznam změn, které budeme aplikovat po dokončení procházení kolekce
//            var newRows = new List<(string Key, string Value)>();

//            foreach (var item in data.Values)
//            {
//                string key = item.Key;
//                string value = item.Value;

//                // Kontrola, zda už klíč existuje, pokud ano, přeskočíme
//                if (data.Values.Any(row => row.Key == key))
//                {
//                    if (item.Key == "ITEMDESC")
//                    {

//                        string itemDesc_1 = string.Empty;
//                        string itemDesc_2 = string.Empty;

//                        if (value.Length > 60)
//                        {
//                            itemDesc_1 = value.Substring(0, 60);
//                            itemDesc_2 = value.Substring(60);
//                        }
//#if DEBUG
//                        else
//                        {
//                            itemDesc_1 = value;
//                            itemDesc_2 = "TEST MaR 10.9.2024";
//                        }
//#endif


//                        // Přidání prvních 60 znaků
//                        data.Values.AddValuesRow($"{key}_1", itemDesc_1);

//                        // Přidání zbytku jako nový řádek s modifikovaným klíčem nebo jinou identifikací
//                        data.Values.AddValuesRow($"{key}_2", itemDesc_2);
//                    }
//                    else if (item.Key == "CountEntries")
//                    {

//                        string SOPDESC = string.Empty;

//                        //metoda dohledani SOPDESC
//                        SOPDESC = FindSOPDESC(item.Key);
//                        data.Values.AddValuesRow($"SOPDESC", SOPDESC);

//                    }


//                    continue; // Klíč už existuje, přeskočíme tento záznam
//                }
//                else
//                {
//                    data.Values.AddValuesRow(key, value);
//                }

//            }

//            data.Values.EndLoadData();
//            data.AcceptChanges();
//        } 
        #endregion


        private void prepareTiskValues_new(Fask.Server.Interfaces.DataSets.DSValues data)
        {
            // Seznam změn, které budeme aplikovat po dokončení procházení kolekce
            var newRows = new List<(string Key, string Value)>();

            foreach (var item in data.Values)
            {
                string key = item.Key;
                string value = item.Value;

                // Přidání původního záznamu
                if (item.Key == "ITEMDESC")
                {
                  //  newRows.Add((key, value)); // Přidání do seznamu změn

                    string itemDesc_1 = string.Empty;
                    string itemDesc_2 = string.Empty;

                    if (value.Length > 60)
                    {
                        itemDesc_1 = value.Substring(0, 60);
                        itemDesc_2 = value.Substring(60);
                    }
#if DEBUG
#if false
                    else
                    {
                        itemDesc_1 = value;
                        itemDesc_2 = "TEST MaR 10.9.2024";
                    } 
#endif
#endif

                    // Přidání prvních 60 znaků
                    if (!data.Values.Any(row => row.Key == $"{key}_1"))
                    {
                        newRows.Add(($"{key}_1", itemDesc_1)); // Přidání do seznamu změn
                    }

                    // Přidání zbytku jako nový řádek s modifikovaným klíčem nebo jinou identifikací
                    if (!data.Values.Any(row => row.Key == $"{key}_2"))
                    {
                        newRows.Add(($"{key}_2", itemDesc_2)); // Přidání do seznamu změn
                    }
                }
                else if (item.Key == "CountEntries")
                {
                    // metoda dohledani SOPDESC
                    string SOPDESC = FindSOPDESC(item.Value.ToString());

                    // Přidání dohledaného SOPDESC, pokud neexistuje
                    if (!data.Values.Any(row => row.Key == "SOPDESC"))
                    {
                        newRows.Add(("SOPDESC", SOPDESC)); // Přidání do seznamu změn
                    }
                }
                else
                {
                    // Přidání jiných klíčů a hodnot, pokud klíč neexistuje
                    if (!data.Values.Any(row => row.Key == key))
                    {
                        newRows.Add((key, value)); // Přidání do seznamu změn
                    }
                }
            }

            // Aplikace všech změn po dokončení iterace
            foreach (var row in newRows)
            {
                data.Values.AddValuesRow(row.Key, row.Value);
            }

            data.Values.EndLoadData();
            data.AcceptChanges();
        }


        private string FindSOPDESC(string key)
        {

            string SOPDESC = string.Empty;

#if DEBUG
#if false

            SOPDESC = "TEST 1 MaR 10.9.2024";  
#endif
#endif

            SOPDESC = TiskMetodaFindSOPDESC_sql_procedura(key);


            return SOPDESC;

        }



        public string TiskMetodaFindSOPDESC_sql(string key)
        {
            string returnValue = string.Empty;

#if DEBUG

#if false
            returnValue = "TEST 2 MaR 10.9.2024"; // Výchozí hodnota, pokud nebude nalezen žádný záznam  

#endif
#endif

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                conn.Open(); // Otevření spojení.

                using (var command = conn.CreateCommand())
                {
                    // SQL dotaz na načtení hodnoty SOPDESC
                    command.CommandText = @"
                SELECT TOP(1) H.[SOPDESC]
                FROM [Agro_fask].[dbo].[CZPRO_VPH] AS H
                LEFT JOIN [Agro_fask].[dbo].[Production] AS P
                ON H.CountEntries = P.CountEntries
                WHERE P.CountEntries = @CountEntries";

                    // Předpokládáme, že 'key' obsahuje hodnotu, která se dá převést na int.
                    if (int.TryParse(key, out int parsedKey))
                    {
                        // Přidání parametru s typem Int32
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@CountEntries",
                            DbType = DbType.Int32, // Použití správného datového typu
                            Value = parsedKey
                        });

                        // Použití ExecuteScalar pro načtení jediné hodnoty
                        var result = command.ExecuteScalar();

                        // Kontrola, zda byl nalezen nějaký výsledek
                        if (result != null && result != DBNull.Value)
                        {
                            returnValue = result.ToString();
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Zadaný klíč není platné celé číslo.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Zpracování výjimky
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Uzavření spojení
                    conn.Dispose(); // Uvolnění zdrojů
                }
            }

            return returnValue;
        }


        public string TiskMetodaFindSOPDESC_sql_procedura(string key)
        {
            string returnValue = string.Empty;

#if DEBUG

#if false
            returnValue = "TEST 2 MaR 10.9.2024"; // Výchozí hodnota pro debug

#endif
#endif

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                conn.Open(); // Otevření spojení.

                using (var command = conn.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure; // Nastavení typu příkazu na proceduru
                    command.CommandText = "[dbo].[sp_GetSOPDESC]"; // Název uložené procedury

                    if (int.TryParse(key, out int parsedKey))
                    {
                        // Přidání parametru s typem Int32
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@CountEntries",
                            DbType = DbType.Int32, // Použití správného datového typu
                            Value = parsedKey
                        });

                        // Použití ExecuteScalar pro načtení jediné hodnoty
                        var result = command.ExecuteScalar();

                        // Kontrola, zda byl nalezen nějaký výsledek
                        if (result != null && result != DBNull.Value)
                        {
                            returnValue = result.ToString();
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Zadaný klíč není platné celé číslo.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Zpracování výjimky
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Uzavření spojení
                    conn.Dispose(); // Uvolnění zdrojů
                }
            }

            return returnValue;
        }


    }
}