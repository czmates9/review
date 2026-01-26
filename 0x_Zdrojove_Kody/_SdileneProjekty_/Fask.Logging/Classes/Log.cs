using System;
using System.IO;
//using Fask.Emailing;
using System.Text;

namespace Fask.Logging
{
	/// <summary>
	/// Summary description for LogError.
	/// </summary>
	internal class Log
	{
        /// <summary>
        /// Staticka promenna urcujici, zdali se ma pri chybe odesilat email.
        /// </summary>
        public static string SendErrorEmail = string.Empty;

        private static bool _doLogging = true;
        public static bool Enable
        {
            get { return _doLogging; }
            set { _doLogging = value; }
        }

        public static string ErrorDataFileDirectory = string.Empty;
        public static string ProcessedDataFileDirectory = string.Empty;
        public static string ErrorLogFile = string.Empty;
        public static string RootPath = string.Empty;
        

        private Log()
		{
		}

		public static bool writeErrorData(string data, string specification)
		{
			System.IO.StreamWriter sw = null;
            try
            {
                string errordatapath = ErrorDataFileDirectory;

                if (!System.IO.Directory.Exists(errordatapath))
                    System.IO.Directory.CreateDirectory(errordatapath);

                sw = System.IO.File.CreateText(Path.Combine(errordatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"));
                sw.Write(data);
                sw.Close();
                sw = null;

                string msg = string.Empty;
                CreateBodyText("Specification: " + specification, data, ref msg);
                SendEmail(msg);

            }
            catch
            {
                return false;
            }
			finally
			{
				if (sw != null) 
				{
					sw.Close();
					sw = null;
				}
			}
			return true;
		}

        public static bool writeErrorData(FileInfo fi, string specification)
        {
            try
            {

                string errordatapath = ErrorDataFileDirectory;

                if (!System.IO.Directory.Exists(errordatapath))
                    System.IO.Directory.CreateDirectory(errordatapath);

                string dstFile = Path.Combine(errordatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data");
                fi.CopyTo(dstFile);

                SendEmail(dstFile, false);
            }
            catch
            {
                return false;
            }
            finally
            {
            }
            return true;
        }

        public static bool writeErrorData(byte[] data, string specification)
        {
            try
            {

                string errordatapath = ErrorDataFileDirectory;
                if (!System.IO.Directory.Exists(errordatapath))
                    System.IO.Directory.CreateDirectory(errordatapath);

                FileStream fs = null;
                try
                {
                    fs = new FileStream(
                        Path.Combine(errordatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"),
                        FileMode.Create,
                        FileAccess.Write
                        );
                    fs.Write(data, 0, data.Length);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                }

                //Odeslani emailu
                SendEmail(errordatapath, false);

                return true;
            }
            catch
            {
                return false;
            }
            //return true;
        }

		public static bool writeErrorData(System.Data.DataRow[] datarows, string specification)
		{
			try
			{
				string errordatapath = ErrorDataFileDirectory;

				if (!System.IO.Directory.Exists(errordatapath))
					System.IO.Directory.CreateDirectory(errordatapath);

				System.Data.DataSet dataset = new System.Data.DataSet();
				dataset.Tables.Add(specification);
				foreach (var r in datarows)
				{
					dataset.Tables[0].ImportRow(r);
				}
				dataset.WriteXml(Path.Combine(errordatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"), System.Data.XmlWriteMode.DiffGram);

				//Odeslani emailu
				SendEmail(errordatapath, false);

			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool writeErrorData(System.Data.DataSet dataset, string specification) {
			try {

                string errordatapath = ErrorDataFileDirectory;

				if (!System.IO.Directory.Exists(errordatapath))
					System.IO.Directory.CreateDirectory(errordatapath);

				dataset.WriteXml(Path.Combine(errordatapath,DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"));

                //Odeslani emailu
                SendEmail(errordatapath, false);

			} catch {
				return false;
			}
			return true;
		}


        public static bool writeErrorData(System.Data.DataSet ds)
        {
            foreach (System.Data.DataTable item in ds.Tables)
            {
                writeErrorData(item);
            }

            return true;
        }


        public static bool writeErrorData(System.Data.DataTable table)
        {
            try
            {
                if (table.HasErrors)
                {
                 writeErrorLog("Has errors: " + table.HasErrors.ToString());
                 writeErrorLog("Table Name:" + table.TableName);
                    if (table.HasErrors)
                    {
                        foreach (System.Data.DataRow row in table.Rows)
                        {
                            if (row.HasErrors)
                                writeErrorLog("\t " + row.RowError);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

		public static bool writeOKData(string data, string specification)
		{
			System.IO.StreamWriter sw = null;
			try 
			{

                string processeddatapath = ProcessedDataFileDirectory;

				if (!System.IO.Directory.Exists(processeddatapath))
					System.IO.Directory.CreateDirectory(processeddatapath);

				sw = System.IO.File.CreateText(Path.Combine(processeddatapath,DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"));
				sw.Write(data);
				sw.Close();
				sw = null;
			} 
			catch 
			{
				return false;
			}
			finally
			{
				if (sw != null) 
				{
					sw.Close();
					sw = null;
				}
			}
			return true;
		}

        public static bool writeOKData(FileInfo fi, string specification)
        {
            try
            {
                //string processeddatapath = //System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"];
                //    (new Uri(
                //    System.IO.Path.Combine(
                //        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                //        System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"])
                //        )).LocalPath;
                string processeddatapath = ProcessedDataFileDirectory;

                if (!System.IO.Directory.Exists(processeddatapath))
                    System.IO.Directory.CreateDirectory(processeddatapath);

                string dstFile = Path.Combine(processeddatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data");
                fi.CopyTo(dstFile, true);
            }
            catch
            {
                return false;
            }
            finally
            {
            }
            return true;
        }

        public static bool writeOKData(byte[] data, string specification)
        {
            try
            {
                //string processeddatapath = //System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"];
                //    (new Uri(
                //    System.IO.Path.Combine(
                //        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                //        System.Configuration.ConfigurationManager.AppSettings["ProcessedDataFileDirectory"])
                //        )).LocalPath;
                string processeddatapath = ProcessedDataFileDirectory;

                if (!System.IO.Directory.Exists(processeddatapath))
                    System.IO.Directory.CreateDirectory(processeddatapath);

                FileStream fs = null;

                try
                {
                    fs = new FileStream(
                        Path.Combine(processeddatapath, DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"),
                        FileMode.Create,
                        FileAccess.Write
                        );
                    fs.Write(data, 0, data.Length);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Flush();
                        fs.Close();
                        fs = null;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

		public static bool writeOKData(System.Data.DataSet dataset, string specification)
		{
			try 
			{

                string processeddatapath = ProcessedDataFileDirectory;

				if (processeddatapath == null || processeddatapath == string.Empty)
					return true;

				if (!System.IO.Directory.Exists(processeddatapath))
					System.IO.Directory.CreateDirectory(processeddatapath);

				dataset.WriteXml(Path.Combine(processeddatapath,DateTime.Now.ToString("yyyyMMddHHmmss_f") + "." + specification + ".data"));
			} 
			catch 
			{
				return false;
			}
			return true;
		}

		public static bool writeError(string messagedata, string filename) {
			System.IO.StreamWriter sw = null;
			try {

                string errordatapath = ErrorDataFileDirectory;

				if (!System.IO.Directory.Exists(errordatapath))
					System.IO.Directory.CreateDirectory(errordatapath);

				sw = System.IO.File.CreateText(System.IO.Path.Combine(errordatapath, filename));
				sw.Write(messagedata);
				sw.Close();
				sw = null;

                //Odeslani emailu
                string msg = string.Empty;
                CreateBodyText("Filename: " + filename, messagedata, ref msg);
                SendEmail(errordatapath, false);

			} catch (Exception ex) {
				if (sw != null) {
					sw.Close();
					sw = null;
				}
				throw ex;
			}
			return true;
		}

        /// <summary>
        /// Zapsani vyjimky do logu pro obecne vyjimky, ktere nejsou dale upresneny
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool writeErrorLog(Exception ex)
        {
            try
            {
                StringBuilder sbError = new StringBuilder();

                sbError.AppendLine(string.Format("{0}:{1}\n{2}", ex.Message, ex.Source, ex.StackTrace));

                writeErrorLog(sbError.ToString());

                return true;
            }
            catch (Exception exWrite)
            {
                writeErrorLog("Chyba zapisu vyjimky: " + exWrite.Message);
                return false;
            }
        }

		public static bool writeErrorLog(string errormessage)
		{
			System.IO.StreamWriter sw = null;
			try 
			{
                //string logpath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"];

                //string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"])).LocalPath;
                string logpath = ErrorLogFile;

                string dirpath = System.IO.Path.GetDirectoryName(logpath);
                if (!System.IO.Directory.Exists(dirpath))
                    System.IO.Directory.CreateDirectory(dirpath);

				if (logpath == null || logpath == string.Empty)
					return true;

				sw = new StreamWriter(logpath, true);
				sw.WriteLine(DateTime.Now.ToString("G") + " : " + errormessage);
				sw.Close();
				sw = null;

                //Odeslani emailu
                SendEmail(errormessage);

				return true;
			}
			catch 
			{
				return false;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw = null;
				}
			}
		}

        public static bool writeErrorLog(string modul, string method, string message)
        {
            return writeErrorLog(modul + "." + method + ":\n" + message);
        }

        public static bool writeLog(string message, string context)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                //string logpath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"];

                //string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"])).LocalPath;
                string logpath = ErrorLogFile;
                string dirpath = System.IO.Path.GetDirectoryName(logpath);
                if (!System.IO.Directory.Exists(dirpath))
                    System.IO.Directory.CreateDirectory(dirpath);
                logpath = System.IO.Path.Combine(dirpath, "log.txt");

                if (logpath == null || logpath == string.Empty)
                    return true;

                sw = new StreamWriter(logpath, true);
                sw.WriteLine(DateTime.Now.ToString("G") + " : " + context + " >> " + message);
                sw.Close();
                sw = null;

                //Vytvoreni zpravy a Odeslani emailu
                string msg = string.Empty;
                CreateBodyText("Filename: " + "log.txt" + "\nContext: " + context, message, ref msg);
                SendEmail(msg);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }
        }

        public static bool writeLicenceLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                //System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"]
                //string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), @"Logs\license_log.txt")).LocalPath;
                string logpath = Path.Combine(RootPath, @"Logs\license_log.txt");
                string dirpath = System.IO.Path.GetDirectoryName(logpath);
                if (!System.IO.Directory.Exists(dirpath))
                    System.IO.Directory.CreateDirectory(dirpath);

                if (File.Exists(logpath))
                    File.WriteAllText(logpath, String.Empty); //smazani dat...
                else
                {
                    //Vytvoreni zpravy a Odeslani emailu
                    string msg = string.Empty;
                    CreateBodyText("Filename: " + dirpath + "\n", message, ref msg);
                    SendEmail(msg);
                }

                if (logpath == null || logpath == string.Empty)
                    return true;

                sw = new StreamWriter(logpath, true);
                sw.WriteLine(DateTime.Now.ToString("G") + " >> " + message);
                sw.Close();
                sw = null;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }
        }

        public static bool DeleteLicenceLog()
        {
            try
            {
                //System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"]
                //string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), @"Logs\license_log.txt")).LocalPath;
                string logpath = Path.Combine(RootPath, @"Logs\license_log.txt");

                if (File.Exists(logpath))
                    File.Delete(logpath);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region Dohledovani Emailem
        /// <summary>
        /// Metoda pro vytvoreni zpravy z kontextu a dat.
        /// </summary>
        /// <param name="Context">Popis kontextu.</param>
        /// <param name="Data">Telo zpravy.</param>
        /// <param name="Msg"></param>
        private static void CreateBodyText(string Str, string Data, ref string Msg)
        {
            //if (System.Configuration.ConfigurationManager.AppSettings["SendErrorEmail"].ToUpper() == "TRUE")
            if (SendErrorEmail.ToUpper() == "TRUE")
            {
                Msg = Str + "\n" + "Data: " + Data + "\n";
            }
        }

        /// <summary>
        /// Metoda pro odeslani emailu bez prilohy v pripade chyby.
        /// </summary>
        /// <param name="Msg"></param>
        public static void SendEmail(string Msg)
        {
            //Odesilame pouze jestli je to povolene v konfiguraci
            //if (System.Configuration.ConfigurationManager.AppSettings["SendErrorEmail"].ToUpper() == "TRUE")
            if (SendErrorEmail.ToUpper() == "TRUE")
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vlozeni Tela zpravy
                    Email.Body = Environment.UserDomainName + ":" + Environment.MachineName + "\n" + Msg;
                    //Vytvoreni zpravy
                    Email.CreateEmailMessage();
                    //Odeslani
                    Email.SendEmailMessage();
                }
                catch (Exception ex)
                {
                    //Pokus o zapis chyby pri posilani emailu do logu (BEZ DALSIHO POSILANI EMAILU).
                    SendEmailError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metoda pro odeslani emailu s prilohou bez tela v pripade chyby.
        /// </summary>
        /// <param name="FileName">Jmeno souboru pro prilohu.</param>
        /// <param name="Nevyuzito">Parametr na rozliseni signatury funkce.</param>
        public static void SendEmail(string FileName, bool Nevyuzito)
        {
            //Odesilame pouze jestli je to povolene v konfiguraci
            //if (System.Configuration.ConfigurationManager.AppSettings["SendErrorEmail"].ToUpper() == "TRUE")
            if (SendErrorEmail.ToUpper() == "TRUE")
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vytvoreni zpravy
                    Email.Body = Environment.UserDomainName + ":" + Environment.MachineName;
                    Email.CreateEmailMessage();
                    //Vlozeni Prilohy
                    Email.AddAttachment(FileName);
                    //Odeslani
                    Email.SendEmailMessage();
                }
                catch (Exception ex)
                {
                    //Pokus o zapis chyby pri posilani emailu do logu (BEZ DALSIHO POSILANI EMAILU).
                    SendEmailError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metoda pro odeslani emailu s prilohou v pripade chyby.
        /// </summary>
        /// <param name="Msg">Telo zpravy.</param>
        /// <param name="FileName">Jmeno souboru pro prilohu.</param>
        public static void SendEmail(string Msg, string FileName)
        {
            //Odesilame pouze jestli je to povolene v konfiguraci
            //if (System.Configuration.ConfigurationManager.AppSettings["SendErrorEmail"].ToUpper() == "TRUE")
            if (SendErrorEmail.ToUpper() == "TRUE")
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vlozeni Tela zpravy
                    Email.Body = Environment.UserDomainName + ":" + Environment.MachineName + "\n" + Msg;
                    //Vytvoreni zpravy
                    Email.CreateEmailMessage();
                    //Pridani prilohy
                    Email.AddAttachment(FileName);
                    //Odeslani
                    Email.SendEmailMessage();
                }
                catch (Exception ex)
                {
                    //Pokus o zapis chyby pri posilani emailu do logu (BEZ DALSIHO POSILANI EMAILU).
                    SendEmailError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Metoda pro zapis pri chybe v posilani emailu.
        /// </summary>
        /// <param name="ErrorMsg">Text chyby.</param>
        //Neni mozne pouzit standardni logovaci metodu,protoze by mohlo dojit opet k chybe - zacykleni.
        public static void SendEmailError(string ErrorMsg)
        {
            //Zapisovac streamu.
            System.IO.StreamWriter sw = null;

            try
            {
                //Cesta k logovacimu souboru pro chyby
                //string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"])).LocalPath;
                string logpath = ErrorLogFile;

                //Cesta k adresari
                string dirpath = System.IO.Path.GetDirectoryName(logpath);
                //Kontrola existence -> vytvoreni
                if (!System.IO.Directory.Exists(dirpath)) System.IO.Directory.CreateDirectory(dirpath);
                //Kontrola spravnosti
                if (logpath == null || logpath == string.Empty) return;

                //Zapis chyby
                sw = new StreamWriter(logpath, true);
                /* Pro vyhledani v souboru: find : Email :*/
                sw.WriteLine(DateTime.Now.ToString("G") + " : Email : " + ErrorMsg);
            }
            catch {/*Nepodarilo se zapsat info o neuspesnosti emailu.*/}
            //Uzavreni streamu.
            finally { if (sw != null) sw.Close(); }
        }
        #endregion
    }
}
