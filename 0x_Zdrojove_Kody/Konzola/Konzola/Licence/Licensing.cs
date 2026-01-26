using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Konzola.Licence
{
    public class Licensing
    {
        private const string licensepassword = "fask!pro159";
        public const string licensedemo = "DEMO";

        private string _licence = licensedemo;
        public string Licence
        {
            get { return _licence; }
            set { this._licence = value; }
        }

        private bool _islicensed = false;
        public bool IsLicensed
        {
            get { return _islicensed; }
            set { this._islicensed = value; }
        }

        #region Jednotlove moduly

        private DateTime _expiration_Date;
        public DateTime Expiration_Date { set { this._expiration_Date = value; } get { return this._expiration_Date; } }

        private string _kontakt = "fask.cz";
        public string Kontakt { set { this._kontakt = value; } get { return this._kontakt; } }

        private string _status = string.Empty;
        public string Status { get { return this._status; } }

        private Konzola.KonzolaLicence _licenceObjekt = null;
        public Konzola.KonzolaLicence LicenceObjekt 
        { 
            get 
            { 
                return this._licenceObjekt; 
            }
            set
            {
               this._licenceObjekt = value;
            }
        }

        #endregion

        public Licensing()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            try
            {

                #region Puvodni Licence
                //StreamReader sr = new StreamReader(Path.Combine(Main.WrkDir, "mst_w.ini"));
                //string license = sr.ReadToEnd();

                //_licence = Fask.Encryption.RijndaelWrapper.Decrypt(license, licensepassword);
                //_islicensed = true;

                #endregion

                LicenceObjekt = new KonzolaLicence();


                #region nova licence
                string _dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                this._status = CheckLicense(Path.Combine(_dir, "konzola.ini"));

                if (this._status != "OK")
                { this._islicensed = false; }

                #endregion
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private string CheckLicense(string PathLicence)
        {

            try
            {

                int stav = overeniLicence(PathLicence, licensepassword);

                //test podpisu
                //license = Fask.Encryption.RijndaelWrapper.Encrypt(license, licensepassword);
                if (stav == -1)
                { return "Nenalezen licenční soubor!"; }


                switch (stav)
                {
                    case -1:
                        return "Nenalezen licenční soubor!";
                    //break;
                    case -2:
                        return "Neznámá chyba!";
                    //break;
                    case -3:
                        return "Licence neni podepsána!";
                    //break;
                    case -4:
                        return "Licence neni validn!";
                        //break;
                }


                XmlDocument doc = new XmlDocument();

                string license = Get_Licence.GetFileContents(PathLicence);
                //string podpis = string.Empty;
                //license = GetXML(license, out podpis);
                doc.LoadXml(license);

                string company = doc.GetElementsByTagName("company")[0].InnerText;
                string contact = doc.GetElementsByTagName("contact")[0].InnerText;
                string expiration = doc.GetElementsByTagName("expiration")[0].InnerText;
                string created = doc.GetElementsByTagName("created")[0].InnerText;


                //if (license != podpis)
                //{
                //    //btn_checkLicence.BackColor = Color.Red;
                //    //MessageBox.Show("Licence je nevalidní!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return "Licence je nevalidní!";
                //}



                //test zda aktualni cas neni mensi nez cas vytvoreni licence
                DateTime vytvoreniLicence = DateTime.ParseExact(created, "dd.MM.yyyy", null);
                if (vytvoreniLicence > DateTime.Now)
                {
                    //btn_checkLicence.BackColor = Color.Red;
                    //MessageBox.Show("Licence je nevalidní!" + created, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return "Licence je nevalidní!";

                }

                //test expirace licence



                switch (isExpirated(expiration))
                {
                    case -1:
                        this._islicensed = true;
                        break;
                    case -2:
                        this._islicensed = true;
                        this._expiration_Date = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);
                        break;
                    case -3:
                        this._expiration_Date = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);
                        //btn_checkLicence.BackColor = Color.Red;
                        //MessageBox.Show("Vypršela platnost licence!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "Vypršela platnost licence!";
                    case -5:
                        return "Chyba expirace licence!";
                    default:
                        break;
                }

                ReadStatus(doc);


                //btn_checkLicence.BackColor = Color.Green;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return "OK";

        }

        private void ReadStatus(XmlDocument doc)
        {
            try
            {
                XmlNodeList companyList = doc.GetElementsByTagName("company");
                XmlNodeList contactList = doc.GetElementsByTagName("contact");
                XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
                XmlNodeList createdList = doc.GetElementsByTagName("created");
                XmlNodeList Ukolovani = doc.GetElementsByTagName("Ukolovani");
                XmlNodeList Planovani = doc.GetElementsByTagName("Planovani");
                XmlNodeList Planovani_PlanyVV = doc.GetElementsByTagName("Planovani_PlanyVV");
                XmlNodeList Planovani_Kapac = doc.GetElementsByTagName("Planovani_Kapac");
                XmlNodeList Vyroba = doc.GetElementsByTagName("Vyroba");
                XmlNodeList VyrobaCiselniky = doc.GetElementsByTagName("VyrobaCiselniky");
                XmlNodeList VyrobaCiselnikySkupiny = doc.GetElementsByTagName("VyrobaCiselnikySkupiny");
                XmlNodeList VyrobaCiselnikyZasoby = doc.GetElementsByTagName("VyrobaCiselnikyZasoby");
                XmlNodeList VyrobaCiselnikyVazbyMaterialy = doc.GetElementsByTagName("VyrobaCiselnikyVazbyMaterialy");
                XmlNodeList VyrobaRozbory = doc.GetElementsByTagName("VyrobaRozbory");
                XmlNodeList VyrobaRozboryPlanVyroby = doc.GetElementsByTagName("VyrobaRozboryPlanVyroby");
                XmlNodeList VyrobaRozboryOdvadeniStroju = doc.GetElementsByTagName("VyrobaRozboryOdvadeniStroju");
                XmlNodeList VyrobaRozboryVyrobky = doc.GetElementsByTagName("VyrobaRozboryVyrobky");
                XmlNodeList VyrobaRozboryVyrobkySN = doc.GetElementsByTagName("VyrobaRozboryVyrobkySN");
                XmlNodeList VyrobaRozboryMaterialy = doc.GetElementsByTagName("VyrobaRozboryMaterialy");
                XmlNodeList VyrobaTransakce = doc.GetElementsByTagName("VyrobaTransakce");
                XmlNodeList VyrobaTransakceOdvodPOHODA = doc.GetElementsByTagName("VyrobaTransakceOdvodPOHODA");
                XmlNodeList VyrobaTransakceVyrobnyPrikaz = doc.GetElementsByTagName("VyrobaTransakceVyrobnyPrikaz");
                XmlNodeList Sklady = doc.GetElementsByTagName("Sklady");
                XmlNodeList SkladyCiselniky = doc.GetElementsByTagName("SkladyCiselniky");
                XmlNodeList SkladyCiselnikySklady = doc.GetElementsByTagName("SkladyCiselnikySklady");
                XmlNodeList SkladyCiselnikyStrediska = doc.GetElementsByTagName("SkladyCiselnikyStrediska");
                XmlNodeList SkladyCiselnikyMapaLokaci = doc.GetElementsByTagName("SkladyCiselnikyMapaLokaci");
                XmlNodeList SkladyCiselnikyVariantyLokaciMaterialu = doc.GetElementsByTagName("SkladyCiselnikyVariantyLokaciMaterialu");
                XmlNodeList SkladyCiselnikyTypyLokaci = doc.GetElementsByTagName("SkladyCiselnikyTypyLokaci");
                XmlNodeList SkladyCiselnikyZasoby = doc.GetElementsByTagName("SkladyCiselnikyZasoby");
                XmlNodeList SkladyCiselnikyAdresar = doc.GetElementsByTagName("SkladyCiselnikyAdresar");
                XmlNodeList SkladyTransakce = doc.GetElementsByTagName("SkladyTransakce");
                XmlNodeList SkladyTransakcePrP = doc.GetElementsByTagName("SkladyTransakcePrP");
                XmlNodeList SkladyTransakceVyP = doc.GetElementsByTagName("SkladyTransakceVyP");
                XmlNodeList SkladyTransakceExpedice = doc.GetElementsByTagName("SkladyTransakceExpedice");
                XmlNodeList SkladyTransakceVolnyPohyb = doc.GetElementsByTagName("SkladyTransakceVolnyPohyb");
                XmlNodeList SkladyTransakcePrevod = doc.GetElementsByTagName("SkladyTransakcePrevod");
                XmlNodeList SkladyTransakceInventura = doc.GetElementsByTagName("SkladyTransakceInventura");
                XmlNodeList SkladyRozbory = doc.GetElementsByTagName("SkladyRozbory");
                XmlNodeList SkladyRozboryPohyby = doc.GetElementsByTagName("SkladyRozboryPohyby");
                XmlNodeList SkladyRozboryStavy = doc.GetElementsByTagName("SkladyRozboryStavy");
                XmlNodeList SkladyRozboryInv = doc.GetElementsByTagName("SkladyRozboryInv");
                XmlNodeList SkladyRozboryInv_Stav = doc.GetElementsByTagName("SkladyRozboryInv_Stav");
                XmlNodeList SkladyRozboryLokMech = doc.GetElementsByTagName("SkladyRozboryLokMech");
                XmlNodeList SkladyRozboryLokMec_Stavy = doc.GetElementsByTagName("SkladyRozboryLokMec_Stavy");
                XmlNodeList StavSkladu = doc.GetElementsByTagName("StavSkladu");
                XmlNodeList Servis = doc.GetElementsByTagName("Servis");
                XmlNodeList IT_Cast = doc.GetElementsByTagName("IT_Cast");
                XmlNodeList IPTerminalu = doc.GetElementsByTagName("IPTerminalu");
                XmlNodeList TypyDokladu = doc.GetElementsByTagName("TypyDokladu");
                XmlNodeList Fask_Rady = doc.GetElementsByTagName("Fask_Rady");
                XmlNodeList Ostatni = doc.GetElementsByTagName("Ostatni");

                string company = companyList[0].InnerText;
                string contact = contactList[0].InnerText;
                string expiration = expirationList[0].InnerText;
                string created = createdList[0].InnerText;


                _licenceObjekt.Ukolovani = bool.Parse(Ukolovani[0].InnerText);
                _licenceObjekt.Planovani = bool.Parse(Planovani[0].InnerText);
                _licenceObjekt.Planovani_PlanyVV = bool.Parse(Planovani_PlanyVV[0].InnerText);
                _licenceObjekt.Planovani_Kapac = bool.Parse(Planovani_Kapac[0].InnerText);
                _licenceObjekt.Vyroba = bool.Parse(Vyroba[0].InnerText);
                _licenceObjekt.VyrobaCiselniky = bool.Parse(VyrobaCiselniky[0].InnerText);
                _licenceObjekt.VyrobaCiselnikySkupiny = bool.Parse(VyrobaCiselnikySkupiny[0].InnerText);
                _licenceObjekt.VyrobaCiselnikyZasoby = bool.Parse(VyrobaCiselnikyZasoby[0].InnerText);
                _licenceObjekt.VyrobaCiselnikyVazbyMaterialy = bool.Parse(VyrobaCiselnikyVazbyMaterialy[0].InnerText);
                _licenceObjekt.VyrobaRozbory = bool.Parse(VyrobaRozbory[0].InnerText);
                _licenceObjekt.VyrobaRozboryPlanVyroby = bool.Parse(VyrobaRozboryPlanVyroby[0].InnerText);
                _licenceObjekt.VyrobaRozboryOdvadeniStroju = bool.Parse(VyrobaRozboryOdvadeniStroju[0].InnerText);
                _licenceObjekt.VyrobaRozboryVyrobky = bool.Parse(VyrobaRozboryVyrobky[0].InnerText);
                _licenceObjekt.VyrobaRozboryVyrobkySN = bool.Parse(VyrobaRozboryVyrobkySN[0].InnerText);
                _licenceObjekt.VyrobaRozboryMaterialy = bool.Parse(VyrobaRozboryMaterialy[0].InnerText);
                _licenceObjekt.VyrobaTransakce = bool.Parse(VyrobaTransakce[0].InnerText);
                _licenceObjekt.VyrobaTransakceOdvodPOHODA = bool.Parse(VyrobaTransakceOdvodPOHODA[0].InnerText);
                _licenceObjekt.VyrobaTransakceVyrobnyPrikaz = bool.Parse(VyrobaTransakceVyrobnyPrikaz[0].InnerText);
                _licenceObjekt.Sklady = bool.Parse(Sklady[0].InnerText);
                _licenceObjekt.SkladyCiselniky = bool.Parse(SkladyCiselniky[0].InnerText);
                _licenceObjekt.SkladyCiselnikySklady = bool.Parse(SkladyCiselnikySklady[0].InnerText);
                _licenceObjekt.SkladyCiselnikyStrediska = bool.Parse(SkladyCiselnikyStrediska[0].InnerText);
                _licenceObjekt.SkladyCiselnikyMapaLokaci = bool.Parse(SkladyCiselnikyMapaLokaci[0].InnerText);
                _licenceObjekt.SkladyCiselnikyVariantyLokaciMaterialu = bool.Parse(SkladyCiselnikyVariantyLokaciMaterialu[0].InnerText);
                _licenceObjekt.SkladyCiselnikyTypyLokaci = bool.Parse(SkladyCiselnikyTypyLokaci[0].InnerText);
                _licenceObjekt.SkladyCiselnikyZasoby = bool.Parse(SkladyCiselnikyZasoby[0].InnerText);
                _licenceObjekt.SkladyCiselnikyAdresar = bool.Parse(SkladyCiselnikyAdresar[0].InnerText);
                _licenceObjekt.SkladyTransakce = bool.Parse(SkladyTransakce[0].InnerText);
                _licenceObjekt.SkladyTransakcePrP = bool.Parse(SkladyTransakcePrP[0].InnerText);
                _licenceObjekt.SkladyTransakceVyP = bool.Parse(SkladyTransakceVyP[0].InnerText);
                _licenceObjekt.SkladyTransakceExpedice = bool.Parse(SkladyTransakceExpedice[0].InnerText);
                _licenceObjekt.SkladyTransakceVolnyPohyb = bool.Parse(SkladyTransakceVolnyPohyb[0].InnerText);
                _licenceObjekt.SkladyTransakcePrevod = bool.Parse(SkladyTransakcePrevod[0].InnerText);
                _licenceObjekt.SkladyTransakceInventura = bool.Parse(SkladyTransakceInventura[0].InnerText);
                _licenceObjekt.SkladyRozbory = bool.Parse(SkladyRozbory[0].InnerText);
                _licenceObjekt.SkladyRozboryPohyby = bool.Parse(SkladyRozboryPohyby[0].InnerText);
                _licenceObjekt.SkladyRozboryStavy = bool.Parse(SkladyRozboryStavy[0].InnerText);
                _licenceObjekt.SkladyRozboryInv = bool.Parse(SkladyRozboryInv[0].InnerText);
                _licenceObjekt.SkladyRozboryInv_Stav = bool.Parse(SkladyRozboryInv_Stav[0].InnerText);
                _licenceObjekt.SkladyRozboryLokMech = bool.Parse(SkladyRozboryLokMech[0].InnerText);
                _licenceObjekt.SkladyRozboryLokMec_Stavy = bool.Parse(SkladyRozboryLokMec_Stavy[0].InnerText);
                _licenceObjekt.StavSkladu = bool.Parse(StavSkladu[0].InnerText);
                _licenceObjekt.Servis = bool.Parse(Servis[0].InnerText);
                _licenceObjekt.IT_Cast = bool.Parse(IT_Cast[0].InnerText);
                _licenceObjekt.IPTerminalu = bool.Parse(IPTerminalu[0].InnerText);
                _licenceObjekt.TypyDokladu = bool.Parse(TypyDokladu[0].InnerText);
                _licenceObjekt.Fask_Rady = bool.Parse(Fask_Rady[0].InnerText);
                _licenceObjekt.Ostatni = bool.Parse(Ostatni[0].InnerText);

                this._licence = company;
                this._kontakt = contact;
                this._expiration_Date = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);

                //if (dtExpirationRead.Value == Settings.LicenseKonzolaExpiredDate)
                //    chkLicenceNeomezenaRead.Checked = true;
                //else
                //    chkLicenceNeomezenaRead.Checked = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
                throw ex;
            }
        }


        //private void ReadStatus(XmlDocument doc)
        //{
        //    try
        //    {
        //        XmlNodeList numberTerminalList = doc.GetElementsByTagName("numberTerminal");
        //        XmlNodeList typeTerminalList = doc.GetElementsByTagName("typeTerminal");
        //        XmlNodeList companyList = doc.GetElementsByTagName("company");
        //        XmlNodeList contactList = doc.GetElementsByTagName("contact");
        //        XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
        //        XmlNodeList createdList = doc.GetElementsByTagName("created");

        //        XmlNodeList node_Inventura1 = doc.GetElementsByTagName("Inventura1");
        //        XmlNodeList node_Inventura2 = doc.GetElementsByTagName("Inventura2");
        //        XmlNodeList node_Events = doc.GetElementsByTagName("Events");
        //        XmlNodeList node_Expedice = doc.GetElementsByTagName("Expedice");
        //        XmlNodeList node_Prijem = doc.GetElementsByTagName("Prijem");
        //        XmlNodeList node_Prodej = doc.GetElementsByTagName("Prodej");
        //        XmlNodeList node_Servis = doc.GetElementsByTagName("Servis");
        //        XmlNodeList node_Tasks = doc.GetElementsByTagName("Tasks");
        //        XmlNodeList node_Vydej = doc.GetElementsByTagName("Vydej");
        //        XmlNodeList node_PaletoveListky = doc.GetElementsByTagName("PaletoveListky");

        //        string numberTerminal = numberTerminalList[0].InnerText;
        //        string typeTerminal = typeTerminalList[0].InnerText;
        //        string company = companyList[0].InnerText;
        //        string contact = contactList[0].InnerText;
        //        string expiration = expirationList[0].InnerText;
        //        string created = createdList[0].InnerText;

        //        this._inventura1 = bool.Parse(node_Inventura1[0].InnerText);
        //        this._inventura2 = bool.Parse(node_Inventura2[0].InnerText);
        //        this._events = bool.Parse(node_Events[0].InnerText);
        //        this._expedice = bool.Parse(node_Expedice[0].InnerText);
        //        this._prijem = bool.Parse(node_Prijem[0].InnerText);
        //        this._prodej = bool.Parse(node_Prodej[0].InnerText);
        //        this._servis = bool.Parse(node_Servis[0].InnerText);
        //        this._tasks = bool.Parse(node_Tasks[0].InnerText);
        //        this._vydej = bool.Parse(node_Vydej[0].InnerText);
        //        this._paletoveListky = bool.Parse(node_PaletoveListky[0].InnerText);

        //        this._licence = company;
        //        this._kontakt = contact;
        //        this._expiration_Date = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);

        //        //if (dtExpirationRead.Value == Settings.LicenseCteckaExpiredDate)
        //        //    chkLicenceNeomezenaRead.Checked = true;
        //        //else
        //        //    chkLicenceNeomezenaRead.Checked = false;
        //    }
        //    catch (Exception ex)
        //    {

        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        //MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
        //        return;
        //    }

        //}


        /// <summary>
        /// vraci hodnoty podle typu trvani
        /// </summary>
        /// <param name="p"></param>
        /// <returns>
        /// -1 pokud je casovo neomedzena
        /// -2 platna
        /// -3 pokud uz cas vyprsel
        /// -4 exception
        /// -5 pokud je praznde tak je chyba
        /// </returns>
        private static int isExpirated(string p)
        {
            try
            {
                // Pokud je prazdne, tak je licence casove neomezena ...
                if (String.IsNullOrEmpty(p))
                    return -5;

                DateTime dateLicence = DateTime.ParseExact(p, "dd.MM.yyyy", null);

                // pokud je rok 9999 tak je neobmedzena
                if (dateLicence.Year == 9999)
                    return -1;

                if (dateLicence > DateTime.Now)
                    return -2;
                else
                    return -3;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -4;
            }
        }

        /// <summary>
        /// Slouzi pro overeni zadaneho klice s klicem, ktery se nachazi v souboru "License.xml"
        /// </summary>
        /// <param name="path">Cesta k souboru s licenci (s klicem)</param>
        /// <returns> integer:
        /// 0 - vse ok, klice jsou stejne
        /// -1 - soubor license.xml nenalezen v zadane LcicencePath
        /// -2 - neznama chyba
        /// -3 - licence neni podepsana (chybi Authority node)
        /// -4 - zadany klic (key) neodpovida licenci v license.xml
        /// </returns>
        /// 
        public int overeniLicence(string LicencePath, string licensepassword)
        {
            try
            {
                //licensepassword = ;

                string licenseInFile = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                string license = string.Empty;

                //string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), txtFileOvereni.Text.Trim());


                if (!File.Exists(LicencePath))
                    return -1;


                xdoc.Load(LicencePath);

                XmlNodeList xmlnode = xdoc.GetElementsByTagName("License");

                for (int i = 0; i < xmlnode.Count; i++)
                {
                    XmlAttributeCollection xmlattrc = xmlnode[i].Attributes;

                    for (int a = 0; a < xmlnode[i].ChildNodes.Count; a++)
                    {
                        if (xmlnode[i].ChildNodes[a].Name == "Authority")
                        {
                            licenseInFile = xmlnode[i].ChildNodes[a].InnerText;
                        }
                    }
                }


                string decryptLicense = String.Empty;
                decryptLicense = Fask.Encryption.RijndaelWrapper.Decrypt(licenseInFile, licensepassword);

                string fileLicence = Get_Licence.GetFileContents(LicencePath);
                int index = fileLicence.IndexOf("  <Authority>");

                if (index <= 0)
                    return -3;

                fileLicence = fileLicence.Remove(index, licenseInFile.Length + 27);

                //string EncryptLicense = Fask.Encryption.RijndaelWrapper.Encrypt(fileLicence, licensepassword);

                if (fileLicence == decryptLicense)
                {
                    //UpdateUI2(xdoc);
                    return 0;
                }
                else
                    return -4;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -2;
            }
        }

    }

    public static class Get_Licence
    {

        /// <summary>
        /// Slouzi pro nacteni souboru licence a pak moznost pridat klic na zasifrovany
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetFileContents(string FileName)
        {
            try
            {
                return GetFileContents(FileName, 5000);
            }
            catch { throw; }
        }

        public static string GetFileContents(string FileName, int TimeOut)
        {
            StreamReader Reader = null;
            int StartTime = System.Environment.TickCount;
            try
            {
                bool Opened = false;
                while (!Opened)
                {
                    try
                    {
                        if (System.Environment.TickCount - StartTime >= TimeOut)
                            throw new System.IO.IOException("File opening timed out");
                        Reader = File.OpenText(FileName);
                        Opened = true;
                    }
                    catch (System.IO.IOException e)
                    {
                        throw e;
                    }
                }
                string Contents = Reader.ReadToEnd();
                Reader.Close();
                return Contents;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (Reader != null)
                {
                    Reader.Close();
                    Reader.Dispose();
                }
            }
        }
    }
}
