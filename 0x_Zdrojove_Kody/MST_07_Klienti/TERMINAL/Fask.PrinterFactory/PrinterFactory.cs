using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;

namespace Fask.PrinterFactory
{
    // Singleton instance of PrinterFactory
    public sealed class PrinterFactory
    {
        private static volatile PrinterFactory instance;
        private static object syncRoot = new Object();

        public static PrinterFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PrinterFactory();
                    }
                }

                return instance;
            }
        }

        Dictionary<string, Printer> printers = new Dictionary<string, Printer>();
        /// <summary>
        /// Seznam tiskaren a jejich provideru a nastaveni ... 
        /// </summary>
        public Dictionary<string, Printer> Printers
        {
            get { return printers; }
        }
        Dictionary<PrinterModules, ModuleToPrint> templates = new Dictionary<PrinterModules, ModuleToPrint>();
        /// <summary>
        /// sezname tiskovych modulu a jejich definic
        /// </summary>
        public Dictionary<PrinterModules, ModuleToPrint> Templates
        {
            get { return templates; }
        }


        /// <summary>
        /// Privatni konstruktor. Neni mozne tvorit dalsi instance teto tridy ...
        /// </summary>
        private PrinterFactory()
        {
            LoadConfiguration();
            InitializePrinterProviders();
        }

        public void InitializePrinterProviders()
        {
            foreach (var item in printers)
            {
                if (item.Value.PrinterProvider != null)
                    item.Value.PrinterProvider.InitializePrinter();
            }
        }

        public void TerminatePrinterProviders()
        {
            foreach (var item in printers)
            {
                if (item.Value.PrinterProvider != null)
                    item.Value.PrinterProvider.TerminatePrinter();
            }
        }

        public void LoadConfiguration()
        {
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, "PrinterFactory.xml"))).LocalPath;

            XmlDocument xmldoc = null;
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(FilePath);

                //Tiskarny
                XmlNodeList printerNodes = xmldoc.SelectNodes(@"/Settings/Printers/Printer");
                if (printerNodes.Count > 0)
                {
                    foreach (XmlElement node in printerNodes)
                    {
                        Printer p = new Printer();
                        //p.PrinterType = (PrinterTypes)Enum.Parse(typeof(PrinterTypes), node.Attributes["type"].Value, true);
                        p.PrinterType = node.Attributes["type"].Value;
                        p.Library = node.Attributes["library"] != null ?
                            (new Uri(Path.Combine(AssemblyDirectoryPath, node.Attributes["library"].Value))).LocalPath 
                            : string.Empty;
                        p.Config = node.Attributes["config"] != null ?
                            (new Uri(Path.Combine(AssemblyDirectoryPath, node.Attributes["config"].Value))).LocalPath
                            : string.Empty;

                        // TODO : nacist assembly ...
                        if (!String.IsNullOrEmpty(p.Library))
                        {
                            Assembly ass = Assembly.LoadFrom(p.Library);
                            Type[] typy = ass.GetTypes();
                            foreach (Type t in typy)
                            {
                                Type[] ifaces = t.GetInterfaces();
                                foreach (Type iface in ifaces)
                                {
                                    if (iface == typeof(Fask.PrinterProvider.IPrinterProvider))
                                    {
                                        p.PrinterProvider = (Fask.PrinterProvider.IPrinterProvider)ass.CreateInstance(t.FullName);
                                        p.PrinterProvider.ConfigFilename = p.Config;
                                    }
                                }
                            }
                        }

                        // Pridat tiskarnu do kolekce ... 
                        if (!printers.ContainsKey(p.PrinterType))
                            printers.Add(p.PrinterType, p);
                        else
                            Logging.Log.Write(p.PrinterType.ToString() + " allready contained", "PrinterFactory.LoadConfiguration");
                    }
                }


                // Moduly
     

                XmlNodeList moduleNodes = xmldoc.SelectSingleNode(@"/Settings/Modules").ChildNodes;
                //foreach (XmlElement module in moduleNodes)
                foreach (XmlNode moduleNode in moduleNodes)
                {
                    if (!(moduleNode is XmlElement))
                        continue;
                    XmlElement module = moduleNode as XmlElement;

                    //foreach (XmlElement sablona in module.ChildNodes)
                    foreach (XmlNode sablonaNode in module.ChildNodes)
                    {
                        if (!(sablonaNode is XmlElement))
                            continue;
                        XmlElement sablona = sablonaNode as XmlElement;

                        //PrinterTypes pType = (PrinterTypes)Enum.Parse(typeof(PrinterTypes), sablona.Attributes["printer"].Value, true);
                        string pType = sablona.Attributes["printer"].Value;
                        string template = sablona.Attributes["template"].Value;

                        switch (module.Name)
                        {
                            case "Prijem":
                                switch (sablona.Name)
                                {
                                    case "Predloha":
                                        templates.Add(PrinterModules.PrijemPredloha, new ModuleToPrint(PrinterModules.PrijemPredloha, pType, template));
                                        break;
                                    case "Nasnimane":
                                        templates.Add(PrinterModules.PrijemNasnimane, new ModuleToPrint(PrinterModules.PrijemNasnimane, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Vydej":
                                switch (sablona.Name)
                                {
                                    case "Predloha":
                                        templates.Add(PrinterModules.VydejPredloha, new ModuleToPrint(PrinterModules.VydejPredloha, pType, template));
                                        break;
                                    case "Nasnimane":
                                        templates.Add(PrinterModules.VydejNasnimane, new ModuleToPrint(PrinterModules.VydejNasnimane, pType, template));
                                        break;
                                    case "Paletovylistek":
                                        templates.Add(PrinterModules.VydejPaletovylistek, new ModuleToPrint(PrinterModules.VydejPaletovylistek, pType, template));
                                        break;
                                    case "PaletaHlavicka":
                                        templates.Add(PrinterModules.VydejPaletaHlavicka, new ModuleToPrint(PrinterModules.VydejPaletaHlavicka, pType, template));
                                        break;
                                    case "PaletaRadek":
                                        templates.Add(PrinterModules.VydejPaletaRadek, new ModuleToPrint(PrinterModules.VydejPaletaRadek, pType, template));
                                        break;
                                    case "PaletaPaticka":
                                        templates.Add(PrinterModules.VydejPaletaPaticka, new ModuleToPrint(PrinterModules.VydejPaletaPaticka, pType, template));
                                        break;
                                    case "SoupiskaHlavicka":
                                        templates.Add(PrinterModules.VydejSoupiskaHlavicka, new ModuleToPrint(PrinterModules.VydejSoupiskaHlavicka, pType, template));
                                        break;
                                    case "SoupiskaRadek":
                                        templates.Add(PrinterModules.VydejSoupiskaRadek, new ModuleToPrint(PrinterModules.VydejSoupiskaRadek, pType, template));
                                        break;
                                    case "SoupiskaPaticka":
                                        templates.Add(PrinterModules.VydejSoupiskaPaticka, new ModuleToPrint(PrinterModules.VydejSoupiskaPaticka, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Prodej":
                                switch (sablona.Name)
                                {
                                    case "Predloha":
                                        templates.Add(PrinterModules.ProdejPredloha, new ModuleToPrint(PrinterModules.ProdejPredloha, pType, template));
                                        break;
                                    case "Nasnimane":
                                        templates.Add(PrinterModules.ProdejNasnimane, new ModuleToPrint(PrinterModules.ProdejNasnimane, pType, template));
                                        break;
                                    case "SoupisHlavicka":
                                        templates.Add(PrinterModules.ProdejSoupisHlavicka, new ModuleToPrint(PrinterModules.ProdejSoupisHlavicka, pType, template));
                                        break;
                                    case "SoupisRadek":
                                        templates.Add(PrinterModules.ProdejSoupisRadek, new ModuleToPrint(PrinterModules.ProdejSoupisRadek, pType, template));
                                        break;
                                    case "SoupisPaticka":
                                        templates.Add(PrinterModules.ProdejSoupisPaticka, new ModuleToPrint(PrinterModules.ProdejSoupisPaticka, pType, template));
                                        break;
                                    case "PaletaHlavicka":
                                        templates.Add(PrinterModules.ProdejPaletaHlavicka, new ModuleToPrint(PrinterModules.ProdejPaletaHlavicka, pType, template));
                                        break;
                                    case "PaletaRadek":
                                        templates.Add(PrinterModules.ProdejPaletaRadek, new ModuleToPrint(PrinterModules.ProdejPaletaRadek, pType, template));
                                        break;
                                    case "PaletaPaticka":
                                        templates.Add(PrinterModules.ProdejPaletaPaticka, new ModuleToPrint(PrinterModules.ProdejPaletaPaticka, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Inventura":
                                switch (sablona.Name)
                                {
                                    case "Predloha":
                                        templates.Add(PrinterModules.InventuraPredloha, new ModuleToPrint(PrinterModules.InventuraPredloha, pType, template));
                                        break;
                                    case "Nasnimane":
                                        templates.Add(PrinterModules.InventuraNasnimane, new ModuleToPrint(PrinterModules.InventuraNasnimane, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Text":
                                switch (sablona.Name)
                                {
                                    case "Volny":
                                        templates.Add(PrinterModules.TextVolny, new ModuleToPrint(PrinterModules.TextVolny, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Baleni":
                                switch (sablona.Name)
                                {
                                    case "Volny":
                                        templates.Add(PrinterModules.Baleni, new ModuleToPrint(PrinterModules.Baleni, pType, template));
                                        break;
                                    case "PrijemZbytku":
                                        templates.Add(PrinterModules.PrijemZbytku, new ModuleToPrint(PrinterModules.PrijemZbytku, pType, template));
                                        break;
                                    case "Prijem":
                                        templates.Add(PrinterModules.JimiTorePrijem, new ModuleToPrint(PrinterModules.PrijemZbytku, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Expedice":
                                switch (sablona.Name)
                                {
                                    case "SoupisHlavicka":
                                        templates.Add(PrinterModules.ExpediceSoupisHlavicka, new ModuleToPrint(PrinterModules.ExpediceSoupisHlavicka, pType, template));
                                        break;
                                    case "SoupisRadek":
                                        templates.Add(PrinterModules.ExpediceSoupisRadek, new ModuleToPrint(PrinterModules.ExpediceSoupisRadek, pType, template));
                                        break;
                                    case "SoupisPaticka":
                                        templates.Add(PrinterModules.ExpediceSoupisPaticka, new ModuleToPrint(PrinterModules.ExpediceSoupisPaticka, pType, template));
                                        break;
                                    case "PaletaHlavicka":
                                        templates.Add(PrinterModules.ExpedicePaletaHlavicka, new ModuleToPrint(PrinterModules.ExpedicePaletaHlavicka, pType, template));
                                        break;
                                    case "PaletaRadek":
                                        templates.Add(PrinterModules.ExpedicePaletaRadek, new ModuleToPrint(PrinterModules.ExpedicePaletaRadek, pType, template));
                                        break;
                                    case "PaletaPaticka":
                                        templates.Add(PrinterModules.ExpedicePaletaPaticka, new ModuleToPrint(PrinterModules.ExpedicePaletaPaticka, pType, template));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrinterFactory");
                throw ex;
            }
            finally
            {
            }
        }

        public void SaveConfiguration()
        {
            string FilePath;
            string AssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            FilePath = (new Uri(Path.Combine(AssemblyDirectoryPath, "PrinterFactory.xml"))).LocalPath;

            XmlDocument xmldoc = null;
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(FilePath);

                //Tiskarny
                // TODO : umoznit redefinici tiskaren ... ???
                //XmlNodeList printerNodes = xmldoc.SelectNodes(@"/Settings/Printers/Printer");
                //if (printerNodes.Count > 0)
                //{
                //    foreach (XmlElement node in printerNodes)
                //    {
                //        Printer p = new Printer();
                //        p.PrinterType = (PrinterTypes)Enum.Parse(typeof(PrinterTypes), node.Attributes["type"].Value, true);
                //        p.Library = node.Attributes["library"] != null ?
                //            (new Uri(Path.Combine(AssemblyDirectoryPath, node.Attributes["library"].Value))).LocalPath 
                //            : string.Empty;
                //        // TODO : nacist assembly ...
                //        if (!String.IsNullOrEmpty(p.Library))
                //        {
                //            Assembly ass = Assembly.LoadFrom(p.Library);
                //            Type[] typy = ass.GetTypes();
                //            foreach (Type t in typy)
                //            {
                //                Type[] ifaces = t.GetInterfaces();
                //                foreach (Type iface in ifaces)
                //                {
                //                    if (iface == typeof(Fask.PrinterProvider.IPrinterProvider))
                //                        p.PrinterProvider = (Fask.PrinterProvider.IPrinterProvider)ass.CreateInstance(t.FullName);
                //                }
                //            }
                //        }

                //        // Pridat tiskarnu do kolekce ... 
                //        if (!printers.ContainsKey(p.PrinterType))
                //            printers.Add(p.PrinterType, p);
                //        else
                //            Logging.Log.Write(p.PrinterType.ToString() + " allready contained", "PrinterFactory.LoadConfiguration");
                //    }
                //}

                // Moduly
                XmlNode modulesNode = xmldoc.SelectSingleNode(@"/Settings/Modules");
                foreach (ModuleToPrint m2p in templates.Values)
                {

                    try
                    {
                        XmlElement xE = null;
                        switch (m2p.PrinterModule)
                        {
                            case PrinterModules.PrijemPredloha:
                                xE = modulesNode.SelectSingleNode("Prijem").SelectSingleNode("Predloha") as XmlElement;
                                break;
                            case PrinterModules.PrijemNasnimane:
                                xE = modulesNode.SelectSingleNode("Prijem").SelectSingleNode("Nasnimane") as XmlElement;
                                break;
                            case PrinterModules.VydejPredloha:
                                xE = modulesNode.SelectSingleNode("Vydej").SelectSingleNode("Predloha") as XmlElement;
                                break;
                            case PrinterModules.VydejNasnimane:
                                xE = modulesNode.SelectSingleNode("Vydej").SelectSingleNode("Nasnimane") as XmlElement;
                                break;
                            case PrinterModules.VydejPaletovylistek:
                                xE = modulesNode.SelectSingleNode("Vydej").SelectSingleNode("Paletovylistek") as XmlElement;
                                break;
                            case PrinterModules.ProdejPredloha:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("Predloha") as XmlElement;
                                break;
                            case PrinterModules.ProdejNasnimane:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("Nasnimane") as XmlElement;
                                break;
                            case PrinterModules.ProdejSoupisHlavicka:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("SoupisHlavicka") as XmlElement;
                                break;
                            case PrinterModules.ProdejSoupisRadek:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("SoupisRadek") as XmlElement;
                                break;
                            case PrinterModules.ProdejSoupisPaticka:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("SoupisPaticka") as XmlElement;
                                break;
                            case PrinterModules.ProdejPaletaHlavicka:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("PaletaHlavicka") as XmlElement;
                                break;
                            case PrinterModules.ProdejPaletaRadek:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("PaletaRadek") as XmlElement;
                                break;
                            case PrinterModules.ProdejPaletaPaticka:
                                xE = modulesNode.SelectSingleNode("Prodej").SelectSingleNode("PaletaPaticka") as XmlElement;
                                break;
                            case PrinterModules.InventuraPredloha:
                                xE = modulesNode.SelectSingleNode("Inventura").SelectSingleNode("Predloha") as XmlElement;
                                break;
                            case PrinterModules.InventuraNasnimane:
                                xE = modulesNode.SelectSingleNode("Inventura").SelectSingleNode("Nasnimane") as XmlElement;
                                break;
                            case PrinterModules.TextVolny:
                                xE = modulesNode.SelectSingleNode("Text").SelectSingleNode("Volny") as XmlElement;
                                break;
                            case PrinterModules.Baleni:
                                xE = modulesNode.SelectSingleNode("Baleni").SelectSingleNode("Volny") as XmlElement;
                                break;
                            case PrinterModules.PrijemZbytku:
                                xE = modulesNode.SelectSingleNode("Baleni").SelectSingleNode("PrijemZbytku") as XmlElement;
                                break;
                            case PrinterModules.JimiTorePrijem:
                                xE = modulesNode.SelectSingleNode("Baleni").SelectSingleNode("Prijem") as XmlElement;
                                break;
                            case PrinterModules.ExpediceSoupisHlavicka:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("SoupisHlavicka") as XmlElement;
                                break;
                            case PrinterModules.ExpediceSoupisRadek:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("SoupisRadek") as XmlElement;
                                break;
                            case PrinterModules.ExpediceSoupisPaticka:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("SoupisPaticka") as XmlElement;
                                break;
                            case PrinterModules.ExpedicePaletaHlavicka:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("PaletaHlavicka") as XmlElement;
                                break;
                            case PrinterModules.ExpedicePaletaRadek:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("PaletaRadek") as XmlElement;
                                break;
                            case PrinterModules.ExpedicePaletaPaticka:
                                xE = modulesNode.SelectSingleNode("Expedice").SelectSingleNode("PaletaPaticka") as XmlElement;
                                break;
                            default:
                                break;
                        }

                        if (xE != null)
                        {
                            xE.Attributes["printer"].Value = m2p.PrinterType.ToString();
                            xE.Attributes["template"].Value = m2p.Template;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Write(ex, "PrinterFactory.SaveConfiguration.XmlDoc.Update");
                    }

                }

                xmldoc.Save(FilePath);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "PrinterFactory.SaveConfiguration");
                throw ex;
            }
            finally
            {
            }
        }

        public UserControl GetConfigControl(string printerType)
        {
            try
            {
                Printer p = printers[printerType];
                if (p.PrinterProvider != null)
                    return p.PrinterProvider.ConfigControlPrinter;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null;
            }            
        }

        public List<string> GetTemplates(string printerType)
        {
            try
            {
                Printer printer = printers[printerType];
                return printer.PrinterProvider.GetTemplatesList();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null; // toto je chyba ...
            }
        }

        #region PrinterProvider Factory methods ...

        public bool Print(Dictionary<string, string> data, PrinterModules printerModule, int pocet)
        {
            ModuleToPrint m2p = null;
            try
            {
                m2p = templates[printerModule];
            }
            catch
            {
                throw new Exception("Print modul '" + printerModule.ToString() + "' nenalezen");
            }

            Printer p = null;
            try
            {
                p = printers[m2p.PrinterType];
            }
            catch
            {
                throw new Exception("Tiskarna '" + m2p.PrinterType.ToString() + "' nenalezena");
            }

            return p.PrinterProvider.Print(data, m2p.Template, pocet);
        }

        public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, PrinterModules printerModule, int pocet)
        {
            ModuleToPrint m2pH = null;
            ModuleToPrint m2pR = null;
            ModuleToPrint m2pF = null;


            if (printerModule == PrinterModules.VydejPaletaHlavicka
                || printerModule == PrinterModules.VydejPaletaPaticka
                || printerModule == PrinterModules.VydejPaletaRadek)
            {
                m2pH = templates[PrinterModules.VydejPaletaHlavicka];
                m2pR = templates[PrinterModules.VydejPaletaRadek];
                m2pF = templates[PrinterModules.VydejPaletaPaticka];
            }

            if (printerModule == PrinterModules.VydejSoupiskaHlavicka
                || printerModule == PrinterModules.VydejSoupiskaPaticka
                || printerModule == PrinterModules.VydejSoupiskaRadek)
            {
                m2pH = templates[PrinterModules.VydejSoupiskaHlavicka];
                m2pR = templates[PrinterModules.VydejSoupiskaRadek];
                m2pF = templates[PrinterModules.VydejSoupiskaPaticka];
            }


            if (printerModule == PrinterModules.ProdejSoupisHlavicka
                || printerModule == PrinterModules.ProdejSoupisPaticka
                || printerModule == PrinterModules.ProdejSoupisRadek)
            {
                m2pH = templates[PrinterModules.ProdejSoupisHlavicka];
                m2pR = templates[PrinterModules.ProdejSoupisRadek];
                m2pF = templates[PrinterModules.ProdejSoupisPaticka];
            }

            if (printerModule == PrinterModules.ProdejPaletaHlavicka
                || printerModule == PrinterModules.ProdejPaletaPaticka
                || printerModule == PrinterModules.ProdejPaletaRadek)
            {
                m2pH = templates[PrinterModules.ProdejPaletaHlavicka];
                m2pR = templates[PrinterModules.ProdejPaletaRadek];
                m2pF = templates[PrinterModules.ProdejPaletaPaticka];
            }

            if (printerModule == PrinterModules.ExpediceSoupisHlavicka
                || printerModule == PrinterModules.ExpediceSoupisPaticka
                || printerModule == PrinterModules.ExpediceSoupisRadek)
            {
                m2pH = templates[PrinterModules.ExpediceSoupisHlavicka];
                m2pR = templates[PrinterModules.ExpediceSoupisRadek];
                m2pF = templates[PrinterModules.ExpediceSoupisPaticka];
            }

            if (printerModule == PrinterModules.ExpedicePaletaHlavicka
                || printerModule == PrinterModules.ExpedicePaletaPaticka
                || printerModule == PrinterModules.ExpedicePaletaRadek)
            {
                m2pH = templates[PrinterModules.ExpedicePaletaHlavicka];
                m2pR = templates[PrinterModules.ExpedicePaletaRadek];
                m2pF = templates[PrinterModules.ExpedicePaletaPaticka];
            }

            if (m2pH == null)
            {
                Logging.Log.Write("Print modul '" + printerModule.ToString() + "' nenalezen");
                return true; //aby mohl dal pokracovat, jako ze se vytisklo...
            }

            Printer p = printers[m2pH.PrinterType];
            if (p == null)
            {
                Logging.Log.Write("Tiskarna '" + m2pH.PrinterType.ToString() + "' nenalezena");
                return true; //aby mohl dal pokracovat, jako ze se vytisklo ...
            }

            return p.PrinterProvider.Print(dataHeader, dataRowList, dataFooter, m2pH.Template, m2pR.Template, m2pF.Template, pocet);
        }

        public bool Print(string text, PrinterModules printerModule, int pocet)
        {
            ModuleToPrint m2p = templates[printerModule];
            if (m2p == null)
            {
                Logging.Log.Write("Print modul '" + printerModule.ToString() + "' nenalezen");
                return true; //aby mohl dal pokracovat, jako ze se vytisklo...
            }

            Printer p = printers[m2p.PrinterType];
            if (p == null)
            {
                Logging.Log.Write("Tiskarna '" + m2p.PrinterType.ToString() + "' nenalezena");
                return true; //aby mohl dal pokracovat, jako ze se vytisklo ...
            }

            //return p.PrinterProvider.PrintText(text, m2p.Template, pocet);
            return p.PrinterProvider.Print(text, pocet);
        }

        #endregion
    }
}
