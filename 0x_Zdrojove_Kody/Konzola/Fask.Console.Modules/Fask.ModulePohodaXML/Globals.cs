using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Fask.ModulePohodaXML
{
    class Globals
    {
        // TODO : konfiguraci zmenit do Settings ... 
        #region Nacteni konfigurace

        private static string _configFilePath = "MST_Pohoda_Config.xml";
        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }

        //private static void LoadDoklady(XmlDocument xmldoc, string NodeName)
        //{
        //    string nodeValue = string.Empty;

        //    Globals.listDokladu.Clear();
        //    foreach (XmlElement nDoklad in xmldoc.SelectNodes(NodeName))
        //    {
        //        Globals.listDokladu.Add(new Doklad(
        //            nDoklad.Attributes["docid"] != null ? nDoklad.Attributes["docid"].Value : null,
        //            nDoklad.Attributes["funkce"] != null ? nDoklad.Attributes["funkce"].Value : null,
        //            nDoklad.Attributes["idsradatext"] != null ? nDoklad.Attributes["idsradatext"].Value : null,
        //            nDoklad.Attributes["docid2"] != null ? nDoklad.Attributes["docid2"].Value : null,
        //            nDoklad.Attributes["sklid"] != null ? nDoklad.Attributes["sklid"].Value : null
        //            ));
        //    }
        //}

        /// <summary>
        /// Nacteni obsahu konkretniho elementu s xml souboru.
        /// </summary>
        /// <param name="XmlDoc">Konfiguracni XML dokument.</param>
        /// <param name="NodeName">Jmeno uzlu v XML dokumentu.</param>
        /// <returns>Obsah uzlu.</returns>
        private static string LoadElement(XmlDocument XmlDoc, string NodeName)
        {
            string nodeValue = string.Empty;

            //Konkretni uzel
            XmlElement configNode = XmlDoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                //Vlozeni obsahu uzlu.
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }

        /// <summary>
        /// Nacteni aktualni konfigurace s xml souboru.
        /// </summary>
        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals.ConfigFilePath);
        }

        /// <summary>
        /// Nacteni aktualni konfigurace s xml souboru.
        /// </summary>
        /// <param name="FileName">Jmeno konfiguracniho souboru.</param>
        private static string LoadConfiguration(string FileName)
        {
            try
            {
                string FilePath;

                if (Path.IsPathRooted(FileName))
                    FilePath = FileName;
                else
                {
                    //FilePath = (new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), FileName))).LocalPath;
                    //FilePath = Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, FileName);
                    string _fullFileName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    FilePath = Path.Combine(_fullFileName, FileName);
                }

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(FilePath);

                //Globals.PathToINIFile = LoadElement(xmldoc, "/Settings/PathToINIFile");
                //Globals.Login = LoadElement(xmldoc, "/Settings/Login");
                //Globals.Login_IDS = LoadElement(xmldoc, "/Settings/Login_IDS");
                //Globals.Password = LoadElement(xmldoc, "/Settings/Password");
                //Globals.PathToPohodaEXE = LoadElement(xmldoc, "/Settings/PathToPohodaEXE");
                //Globals.ConnectionString = LoadElement(xmldoc, "/Settings/ConnectionString");
                //Globals.PathToInputDirectory = LoadElement(xmldoc, "/Settings/PathToInputDirectory");
                //Globals.Delivered = bool.Parse(LoadElement(xmldoc, "/Settings/Delivered"));
                //Globals.Executed = bool.Parse(LoadElement(xmldoc, "/Settings/Executed"));
                //Globals.StatusObjednavky = bool.Parse(LoadElement(xmldoc, "/Settings/StatusObjednavky"));
                //Globals.ConnectionStringPohodaDB = LoadElement(xmldoc, "/Settings/ConnectionStringPohodaDB");
                //Globals.Zbyva = bool.Parse(LoadElement(xmldoc, "/Settings/Zbyva"));
                //Globals.ErrorLogFile = LoadElement(xmldoc, "/Settings/ErrorLogFile");
                //Globals.ICO = LoadElement(xmldoc, "/Settings/Ico");
                //Globals.ExportovatPouzeAktivniPolozky = bool.Parse(LoadElement(xmldoc, "/Settings/ExportovatPouzeAktivniPolozky"));
                //Globals.ExportSkladFilter = LoadElement(xmldoc, "/Settings/ExportSkladFilter");
                //Globals.ExportTypFilter = LoadElement(xmldoc, "/Settings/ExportTypFilter");
                //Globals.DotahovatInformaceDodavatele = bool.Parse(LoadElement(xmldoc, "/Settings/DotahovatInformaceDodavatele"));
                //Globals.KodCiziMena = LoadElement(xmldoc, "/Settings/KodCiziMena");
                //Globals.RadaCiziMenaText = LoadElement(xmldoc, "/Settings/RadaCiziMenaText");
                //Globals.IDDefaultniDodavatel = LoadElement(xmldoc, "/Settings/IDDefaultniDodavatel");
                //Globals.PathDataOutputFile = LoadElement(xmldoc, "/Settings/PathDataOutputFile");
                //Globals.PathToXSLT = LoadElement(xmldoc, "/Settings/PathToXSLT");
                //Globals.KodFormaUhrady = LoadElement(xmldoc, "/Settings/KodFormaUhrady");
                //Globals.HesloStornoPrijemka = LoadElement(xmldoc, "/Settings/HesloStornoPrijemka");
                //Globals.HesloStornoVydejka = LoadElement(xmldoc, "/Settings/HesloStornoVydejka");
                //Globals.ObjednavkaDetail = LoadElement(xmldoc, "/Settings/ObjednavkaDetail");
                //Globals.ObjednavkaDetailPolozka = LoadElement(xmldoc, "/Settings/ObjednavkaDetailPolozka");

                //LoadDoklady(xmldoc, "/Settings/doklad");

                //try
                //{
                //    Globals.InventuraPrepisovatZkontrolovanePolozky = bool.Parse(LoadElement(xmldoc, "/Settings/InventuraPrepisovatZkontrolovanePolozky"));
                //}
                //catch
                //{
                //    Globals.InventuraPrepisovatZkontrolovanePolozky = true;
                //}

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }
        }

        #endregion

        #region Parametry
        //public static List<Doklad> listDokladu = new List<Doklad>();

        //private static string _connectionString = string.Empty;
        /// <summary>
        /// Connection string na MST struktury
        /// </summary>
        //public static string ConnectionString
        //{
        //    get
        //    {
        //        return _connectionString;
        //    }
        //    set
        //    {
        //        _connectionString = value;
        //    }
        //}

        //private static string _connectionStringPohodaDB = string.Empty;
        /// <summary>
        /// Connection string na Pohodu pres OleDB rozhrani ...
        /// </summary>
        //public static string ConnectionStringPohodaDB
        //{
        //    get
        //    {
        //        return _connectionStringPohodaDB;
        //    }
        //    set
        //    {
        //        _connectionStringPohodaDB = value;
        //    }
        //}

        //private static string _ico = string.Empty;
        /// <summary>
        /// ICO firmy nad kterou se pracuje ...
        /// </summary>
        //public static string ICO
        //{
        //    get
        //    {
        //        return _ico;
        //    }
        //    set
        //    {
        //        _ico = value;
        //    }
        //}

        //private static bool _inventuraPrepisovatZkontrolovanePolozky = true;
        //public static bool InventuraPrepisovatZkontrolovanePolozky
        //{
        //    get
        //    {
        //        return _inventuraPrepisovatZkontrolovanePolozky;
        //    }
        //    set
        //    {
        //        _inventuraPrepisovatZkontrolovanePolozky = value;
        //    }
        //}

        //private static bool _exportovatPouzeAktivniPolozky = true;
        //public static bool ExportovatPouzeAktivniPolozky
        //{
        //    get
        //    {
        //        return _exportovatPouzeAktivniPolozky;
        //    }
        //    set
        //    {
        //        _exportovatPouzeAktivniPolozky = value;
        //    }
        //}

        //private static string _exportSkladFilter = string.Empty;
        //public static string ExportSkladFilter
        //{
        //    get { return _exportSkladFilter; }
        //    set { _exportSkladFilter = value; }
        //}

        //private static string _exportTypFilter = string.Empty;
        //public static string ExportTypFilter
        //{
        //    get { return _exportTypFilter; }
        //    set { _exportTypFilter = value; }
        //}

        //private static string _kodCiziMena = string.Empty;
        //public static string KodCiziMena
        //{
        //    get
        //    {
        //        return _kodCiziMena;
        //    }
        //    set
        //    {
        //        _kodCiziMena = value;
        //    }

        //}

        //private static string _RadaCiziMenaText = string.Empty;
        //public static string RadaCiziMenaText
        //{
        //    get { return _RadaCiziMenaText; }
        //    set { _RadaCiziMenaText = value; }
        //}

        //private static string _PathDataOutputFile = string.Empty;
        //public static string PathDataOutputFile
        //{
        //    get
        //    {
        //        return _PathDataOutputFile;
        //    }
        //    set
        //    {
        //        _PathDataOutputFile = value;
        //    }

        //}

        //private static string _PathToXSLT = string.Empty;
        //public static string PathToXSLT
        //{
        //    get
        //    {
        //        return _PathToXSLT;
        //    }
        //    set
        //    {
        //        _PathToXSLT = value;
        //    }

        //}



        //private static string _iDDefaultniDodavatel = string.Empty;
        //public static string IDDefaultniDodavatel
        //{
        //    get
        //    {
        //        return _iDDefaultniDodavatel;
        //    }
        //    set
        //    {
        //        _iDDefaultniDodavatel = value;
        //    }
        //}

        //private static string _kodFormaUhrady = string.Empty;
        //public static string KodFormaUhrady
        //{
        //    get
        //    {
        //        return _kodFormaUhrady;
        //    }
        //    set
        //    {
        //        _kodFormaUhrady = value;
        //    }
        //}

        //private static string _hesloStornoPrijemka = string.Empty;
        //public static string HesloStornoPrijemka
        //{
        //    get
        //    {
        //        return _hesloStornoPrijemka;
        //    }
        //    set
        //    {
        //        _hesloStornoPrijemka = value;
        //    }
        //}

        //private static string _hesloStornoVydejka = string.Empty;
        //public static string HesloStornoVydejka
        //{
        //    get
        //    {
        //        return _hesloStornoVydejka;
        //    }
        //    set
        //    {
        //        _hesloStornoVydejka = value;
        //    }
        //}

        //private static string _errorLogFile = string.Empty;
        //public static string ErrorLogFile
        //{
        //    get
        //    {
        //        return _errorLogFile;
        //    }
        //    set
        //    {
        //        _errorLogFile = value;
        //    }
        //}


        //private static string _pathToINIFile = string.Empty;
        //public static string PathToINIFile
        //{
        //    get
        //    {
        //        return _pathToINIFile;
        //    }
        //    set
        //    {
        //        _pathToINIFile = value;
        //    }
        //}

        //private static string _pathToPohodaEXE = string.Empty;
        //public static string PathToPohodaEXE
        //{
        //    get
        //    {
        //        return _pathToPohodaEXE;
        //    }
        //    set
        //    {
        //        _pathToPohodaEXE = value;
        //    }
        //}

        //private static string _pathToInputDirectory = string.Empty;
        //public static string PathToInputDirectory
        //{
        //    get
        //    {
        //        return _pathToInputDirectory;
        //    }
        //    set
        //    {
        //        _pathToInputDirectory = value;
        //    }
        //}


        //private static string _login = string.Empty;
        //public static string Login
        //{
        //    get
        //    {
        //        return _login;
        //    }
        //    set
        //    {
        //        _login = value;
        //    }
        //}

        //private static string _login_IDS = string.Empty;
        //public static string Login_IDS
        //{
        //    get
        //    {
        //        return _login_IDS;
        //    }
        //    set
        //    {
        //        _login_IDS = value;
        //    }
        //}

        //private static bool _dotahovatInformaceDodavatele = false;
        //public static bool DotahovatInformaceDodavatele
        //{
        //    get
        //    {
        //        return _dotahovatInformaceDodavatele;

        //    }
        //    set
        //    {
        //        _dotahovatInformaceDodavatele = value;

        //    }
        //}

        //private static bool _statusObjednavkycuted = false;
        //public static bool StatusObjednavky
        //{
        //    get
        //    {
        //        return _statusObjednavkycuted;

        //    }
        //    set
        //    {
        //        _statusObjednavkycuted = value;

        //    }
        //}

        //private static bool _executed = false;
        //public static bool Executed
        //{
        //    get
        //    {
        //        return _executed;

        //    }
        //    set
        //    {
        //        _executed = value;

        //    }
        //}

        //private static bool _delivered = false;
        //public static bool Delivered
        //{
        //    get
        //    {
        //        return _delivered;

        //    }
        //    set
        //    {
        //        _delivered = value;

        //    }
        //}

        //private static string _password = string.Empty;
        //public static string Password
        //{
        //    get
        //    {
        //        return _password;

        //    }
        //    set
        //    {
        //        _password = value;

        //    }
        //}

        //private static bool _zbyva = true;
        //public static bool Zbyva
        //{
        //    get
        //    {
        //        return _zbyva;

        //    }
        //    set
        //    {
        //        _zbyva = value;

        //    }
        //}

        //[System.ComponentModel.DefaultValue("Select * from Obj")]
        //public static string ObjednavkaDetail { get; set; }

        //public static string ObjednavkaDetailPolozka { get; set; }

        #endregion
    }
}
