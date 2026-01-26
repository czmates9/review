using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Fask.MST_W.Licence
{
    internal class Licensing
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

        private bool _inventura1 = false;
        public bool Inventura1 { set { this._inventura1 = value; } get { return this._inventura1; } }

        private bool _inventura2 = false;
        public bool Inventura2 { set { this._inventura2 = value; } get { return this._inventura2; } }

        private bool _events = false;
        public bool Events { set { this._events = value; } get { return this._events; } }

        private bool _expedice = false;
        public bool Expedice { set { this._expedice = value; } get { return this._expedice; } }

        private bool _prijem = false;
        public bool Prijem { set { this._prijem = value; } get { return this._prijem; } }

        private bool _prodej = false;
        public bool Prodej { set { this._prodej = value; } get { return this._prodej; } }

        private bool _servis = false;
        public bool Servis { set { this._servis = value; } get { return this._servis; } }

        private bool _tasks = false;
        public bool Tasks { set { this._tasks = value; } get { return this._tasks; } }

        private bool _vydej = false;
        public bool Vydej { set { this._vydej = value; } get { return this._vydej; } }

        private bool _paletoveListky = false;
        public bool PaletoveListky { set { this._paletoveListky = value; } get { return this._paletoveListky; } }


        private string _kontakt = "fask.cz";
        public string Kontakt { set { this._kontakt = value; } get { return this._kontakt; } }

        private string _terminalID ;
        public string TerminalID { set { this._terminalID = value; } get { return this._terminalID; } }

        private string _terminalType = "SQLCE";
        public string TerminalType { set { this._terminalType = value; } get { return this._terminalType; } }

        private string _status = string.Empty;
        public string Status { get { return this._status; } }

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


                #region nova licence

                this._status = CheckLicense(Path.Combine(Main.WrkDir, "mst_w.ini"));

                if (this._status != "OK")
                { this._islicensed = false; }

                #endregion
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Licensing");
            }
        }


        private string CheckLicense(string PathLicence)
        {

            try
            {
                XmlDocument doc = new XmlDocument();

                string license = Get_Licence.GetFileContents(PathLicence);
                //string podpis = string.Empty;
                //license = GetXML(license, out podpis);
                doc.LoadXml(license);
                
                string numberTerminal = doc.GetElementsByTagName("numberTerminal")[0].InnerText;
                string typeTerminal = doc.GetElementsByTagName("typeTerminal")[0].InnerText;
                string company = doc.GetElementsByTagName("company")[0].InnerText;
                string contact = doc.GetElementsByTagName("contact")[0].InnerText;
                string expiration = doc.GetElementsByTagName("expiration")[0].InnerText;
                string created = doc.GetElementsByTagName("created")[0].InnerText;

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
                Logging.Log.Write(ex);
            }

            return "OK";

        }

        private void ReadStatus(XmlDocument doc)
        {
            try
            {
                XmlNodeList numberTerminalList = doc.GetElementsByTagName("numberTerminal");
                XmlNodeList typeTerminalList = doc.GetElementsByTagName("typeTerminal");
                XmlNodeList companyList = doc.GetElementsByTagName("company");
                XmlNodeList contactList = doc.GetElementsByTagName("contact");
                XmlNodeList expirationList = doc.GetElementsByTagName("expiration");
                XmlNodeList createdList = doc.GetElementsByTagName("created");

                XmlNodeList node_Inventura1 = doc.GetElementsByTagName("Inventura1");
                XmlNodeList node_Inventura2 = doc.GetElementsByTagName("Inventura2");
                XmlNodeList node_Events = doc.GetElementsByTagName("Events");
                XmlNodeList node_Expedice = doc.GetElementsByTagName("Expedice");
                XmlNodeList node_Prijem = doc.GetElementsByTagName("Prijem");
                XmlNodeList node_Prodej = doc.GetElementsByTagName("Prodej");
                XmlNodeList node_Servis = doc.GetElementsByTagName("Servis");
                XmlNodeList node_Tasks = doc.GetElementsByTagName("Tasks");
                XmlNodeList node_Vydej = doc.GetElementsByTagName("Vydej");
                XmlNodeList node_PaletoveListky = doc.GetElementsByTagName("PaletoveListky");

                string numberTerminal = numberTerminalList[0].InnerText;
                string typeTerminal = typeTerminalList[0].InnerText;
                string company = companyList[0].InnerText;
                string contact = contactList[0].InnerText;
                string expiration = expirationList[0].InnerText;
                string created = createdList[0].InnerText;

                this._inventura1 = bool.Parse(node_Inventura1[0].InnerText);
                this._inventura2 = bool.Parse(node_Inventura2[0].InnerText);
                this._events = bool.Parse(node_Events[0].InnerText);
                this._expedice = bool.Parse(node_Expedice[0].InnerText);
                this._prijem = bool.Parse(node_Prijem[0].InnerText);
                this._prodej = bool.Parse(node_Prodej[0].InnerText);
                this._servis = bool.Parse(node_Servis[0].InnerText);
                this._tasks = bool.Parse(node_Tasks[0].InnerText);
                this._vydej = bool.Parse(node_Vydej[0].InnerText);
                this._paletoveListky = bool.Parse(node_PaletoveListky[0].InnerText);

                this._licence = company;
                this._kontakt = contact;
                this._terminalID = numberTerminal;
                this._terminalType= typeTerminal;

                this._expiration_Date = DateTime.ParseExact(expiration, "dd.MM.yyyy", null);

                //if (dtExpirationRead.Value == Settings.LicenseCteckaExpiredDate)
                //    chkLicenceNeomezenaRead.Checked = true;
                //else
                //    chkLicenceNeomezenaRead.Checked = false;
            }
            catch (Exception ex)
            {
                
                Logging.Log.Write(ex);
                //MessageBox.Show("Nepovedlo se načíst všechny informace z licence, některá informace chybí!");
                return;
            }
        
        }


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
                if (dateLicence.Year == 9999 )
                    return -1;

                if (dateLicence > DateTime.Now)
                    return -2;
                else
                    return -3;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
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
                Logging.Log.Write(ex);
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
