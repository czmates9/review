using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using Fask.Logging;

namespace Fask.MST_W_Server.Licensing
{
    public static class Licensing
    {
        /// <summary>
        /// Heslo pro zašifrovani licence
        /// </summary>
        private const string licensepassword = "fask!pro159";

        /// <summary>
        /// Hlavni metoda sloužici pro načteni licence a nasledne rozparsovani a vraceni jako objektu Licence
        /// </summary>
        /// <param name="serverMapPath">Cesta do rootu aplikace</param>
        /// <returns></returns>
        public static License GetLicense()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                //string licensepath = new Uri(
                // System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase),
                // @"licence.ini"
                // )).LocalPath;
                string licensepath = Path.Combine(Fask.MyPath.Path.BinDirectory, @"licence.ini");
                string license = string.Empty;

                if (!File.Exists(licensepath))
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence,"Zadaný soubor s licencí neexistuje!");
					
                    return null;
                }

                //ziskame licencni klic a informace o licenci
                license = GetFileContents(licensepath);
                string podpis = string.Empty;
                license = GetXML(license, out podpis);
                doc.LoadXml(license);

                string numberTerminal = doc.GetElementsByTagName("numberTerminal")[0].InnerText;
                string company =  doc.GetElementsByTagName("company")[0].InnerText;
                string contact = doc.GetElementsByTagName("contact")[0].InnerText;
                string expiration = doc.GetElementsByTagName("expiration")[0].InnerText;
                string created = doc.GetElementsByTagName("created")[0].InnerText;

                #region validita a expirace licence

                bool isValid = false;
                //test podpisu
                license = RijndaelWrapper.Encrypt(license, licensepassword);
                if (license == podpis)
                    isValid = true;
                else
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence, "Licence je nevalidní!");

                //test zda aktualni cas neni mensi nez cas vytvoreni licence
                DateTime vytvoreniLicence = DateTime.ParseExact(created, "dd.MM.yyyy", null);
                if (vytvoreniLicence > DateTime.Now)
                {
                    isValid = false;
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence, "Licence je nevalidní!");
                }
                else
                    isValid = true;

                //test expirace licence
                bool expirated = true;
                if (isExpirated(expiration))
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence, "Vypršela platnost licence!");
                else
                    expirated = false;

                //test poctu terminalu
                if (GetNumberOfTerminals() > int.Parse(numberTerminal))
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence, "Byl překročen počet aktivních terminálů, než umožňuje licence!");
                    isValid = false;
                }

                #endregion 

				if (isValid && !expirated)
				{
					Fask.Logging.ExceptionHandler2.Handle_Delete(LogLevel.Licence);
					//Log.DeleteLicenceLog();
				}

                License lic = new License(numberTerminal, company, contact, expiration, podpis, created, isValid, expirated);

                return lic;
            }
            catch (Exception ex)
            {
                //Log.writeLicenceLog(ex.Message);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Licence, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metoda sloužici pro odeleni XML souboru od licenčneho podpisu ktery neni současti XML struktury
        /// </summary>
        /// <param name="x">Cely text ktery obsahuje licenční soubor</param>
        /// <param name="podpis">Odceleni časti licence a vraceni cez out do stringove promenne</param>
        /// <returns>Vraceni XML sobrou ktery je možne nasledne parsovat</returns>
        private static string GetXML(string x, out string podpis)
        {
            podpis = string.Empty;
            if (x.LastIndexOf("\n") > 0)
            {
                podpis = x.Substring(x.LastIndexOf('\n') + 1);
                return x.Substring(0, x.LastIndexOf("\n"));
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// Metoda sloužici pro ověřeni platnosti licence zda nevypršela.
        /// </summary>
        /// <param name="p">Predavany parametr je string ale mnel by byt DATETIME</param>
        /// <returns></returns>
        private static bool isExpirated(string p)
        {
            try
            {
                // Pokud je prazdne, tak je licence casove neomezena ...
                if (String.IsNullOrEmpty(p))
                    return false;

                DateTime dateLicence = DateTime.ParseExact(p, "dd.MM.yyyy", null);

                if (dateLicence > DateTime.Now)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(".MST_W_Server.Licensing.Licensing", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        /// <summary>
        /// Metoda sloužici na našteni celeho souboru do stringu
        /// </summary>
        /// <param name="FileName">Cesta k žadanemu souboru</param>
        /// <returns></returns>
        public static string GetFileContents(string FileName)
        {
            try
            {
                return GetFileContents(FileName, 5000);
            }
            catch { throw; }
        }

        /// <summary>
        /// Metoda sloužici na našteni celeho souboru do stringu
        /// </summary>
        /// <param name="FileName">Cesta k žadanemu souboru</param>
        /// <param name="TimeOut">TimeOut pro otevřeni souboru</param>
        /// <returns></returns>
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


        /// <summary>
        /// Metoda ktery podle nazvu složek v SQLiteDBs zisti počet aktivných terminalu
        /// </summary>
        /// <param name="serverMapPath">Cesta do rootu aplikace</param>
        /// <returns> čiselna hodnota představujici počet terminalu</returns>
        private static int GetNumberOfTerminals()
        {

            string[] files = System.IO.Directory.GetDirectories(Fask.MyPath.Path.SQLiteDBsDirectory);

            int i;
            int number = 0;
            foreach (var item in files)
            {
                string s = new System.IO.FileInfo(item).Name;

                bool isNumeric = int.TryParse(s, out i); //zkusime nazev slozky prevest na cislo, jestli to pujde, tak se jedna o slozku pro terminal
                if (isNumeric)
                    number++;
            }

            return number;


        }

    }
}
