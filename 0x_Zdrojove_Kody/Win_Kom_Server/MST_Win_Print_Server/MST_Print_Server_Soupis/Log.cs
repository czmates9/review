using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fask.Emailing;

namespace MST_Print_Server_Logging
{
    public class Log
    {

        private static bool _senderrormail = false;
        public static bool SendErrorMail
        {
            get { return _senderrormail; }
            set { _senderrormail = value; }
        }


        private static string _dirlog = "/";
        public static string Directory
        {
            get { return _dirlog; }
            set
            {
                _dirlog = value;
                if (!System.IO.Directory.Exists(_dirlog))
                    System.IO.Directory.CreateDirectory(_dirlog);
            }
        }
		private static string _filelog = "log.txt";
        public static string File
        {
            get { return _filelog; }
            set { _filelog = value; }
        }
		private static bool _doLogging = false;
        public static bool Enable
        {
            get { return _doLogging; }
            set { _doLogging = value; }
        }

        static Log()
        {            
            Directory = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath;
            Directory = Path.Combine(_dirlog, @"..\Log");

            //try
            //{
            //    _senderrormail = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendErrorEmail"]);
            //}
            //catch { }
        }

        public static void Write(string message)
        {
            Write(message, string.Empty);
        }

		public static void Write(string message, string context) {
            if (!_doLogging)
                return;

            SaveToLogFile(message, context);
		}

        private static void SaveToLogFile(string message, string context)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                sw = new System.IO.StreamWriter(System.IO.Path.Combine(_dirlog, _filelog), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + (context.Length != 0 ? "(" + context + ") " : context) + message);
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }

            try
            {
                SendEmail("Context:" + context + "\nMessage : " + message);
            }
            catch (Exception ex)
            {
                SendEmailError(ex.Message);
            }
        }

        public static void WriteException(string message)
        {
            WriteException(message, string.Empty);
        }

        public static void WriteException(string message, string context)
        {
            SaveToLogFile("Exception: " + message, context);
        }

		public static void Backup()
		{
			try 
			{
                System.IO.File.Move(System.IO.Path.Combine(_dirlog, _filelog), "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
			} 
			catch {}
		}

		public static void Delete()
		{
            System.IO.File.Delete(System.IO.Path.Combine(_dirlog, _filelog));
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
            if (_senderrormail)
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
            if (_senderrormail)
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vlozeni Tela zpravy
                    Email.Body = Msg;
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
            if (_senderrormail)
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vytvoreni zpravy
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
            if (_senderrormail)
            {
                try
                {
                    //Nacteni konfigurace z konfiguracniho souboru
                    Email.LoadConfiguration();
                    //Vlozeni Tela zpravy
                    Email.Body = Msg;
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
                string logpath = new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), System.Configuration.ConfigurationManager.AppSettings["ErrorLogFile"])).LocalPath;
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
