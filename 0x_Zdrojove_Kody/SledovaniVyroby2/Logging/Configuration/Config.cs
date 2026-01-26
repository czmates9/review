using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using System.Windows.Forms;
using System.IO;
using Logging.Configuration;
using System.Data.SqlClient;
using System.Data;
using FASK.SledovaniVyroby.ErrorLog;
using Fask.Logging;
using ICommDatabase;

namespace FASK.SledovaniVyroby.Logging
{
    public class LoginException : Exception
    {
        public LoginException()
            : base()
        {
        }

        public LoginException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Konfigurace loginu
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// Nazev konfiguracniho souboru modulu
        /// </summary>
        private const string cfilename = "loginconfig.xml";

        /// <summary>
        /// Cesta konfiguracniho souboru modulu - vytvoren v konstruktoru
        /// </summary>
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        /// <summary>
        /// Dataset pro konfiguraci - informace o jednotlivych modulech
        /// </summary>
        public static ConfigTable config = new ConfigTable();

        /// <summary>
        /// Bezparametrovy konstruktor
        /// </summary>
        static LogConfig()
        {
            //Sestaveni cesty
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);

            //Nacteni
            Load();
        }

        /// <summary>
        /// Nacteni informace o modulech
        /// </summary>
        public static void Load()
        {
            try { config.ReadXml(ConfigFileName); }
            catch(Exception ex) 
            {
                var x = ex.Message;
            }
        }

        /// <summary>
        /// Ulozeni informaci o modulech
        /// </summary>
        public static void Save()
        {
            try { config.WriteXml(ConfigFileName); }
            catch { }
        }

        /// <summary>
        /// Signalizuje zmenu loginu - vyvolava metodu
        /// </summary>
        public static event MethodInvoker LoginChanged = null;

        /// <summary>
        /// Vyvolava metodu, pokud je zaregistrovana
        /// </summary>
        protected static void OnLoginChanged()
        {
            if (LoginChanged != null)
            {
                LoginChanged();
            }
        }

        /// <summary>
        /// ID uzivatele
        /// </summary>
        public static string LoginID { get; set; }

        /// <summary>
        /// ID Masiny
        /// </summary>
        public static string MachineID { get; set; }

        /// <summary>
        /// Typ Masiny
        /// </summary>
        public static string MachineType { get; set; }

        /// <summary>
        /// Koeficient Masiny
        /// </summary>
        public static decimal Koeficient { get; set; }

        /// <summary>
        /// Jmeno uzivatele
        /// </summary>
        public static string LoginName { get; set; }

        /// <summary>
        /// Sql connection string
        /// </summary>
        public static string SqlConnectionStringLocal { get; set; }

        /// <summary>
        /// Sql connection global
        /// </summary>
        public static string SqlConnectionStringGlobal { get; set; }

        /// <summary>
        /// adresa komunikacniho serveru
        /// </summary>
        public static string KomServer { get; set; }


        /// <summary>
        /// Zobrazovane jmeno
        /// </summary>
        public static string LoginString
        {
            get
            {
                try 
                { 
                    if(LoginID == null || LoginName == null)
                        return "<,>";
                    else
                        return "<" + LoginID.Trim() + ", " + LoginName.Trim() + ">"; 
                }
                catch { return "<,>"; }

                //try { return "<" + LoginID.Trim() + ", " + LoginName.Trim() + ">"; }
                //catch { return "<,>"; }
            }
        }

        /// <summary>
        /// Zda je uzivatel zalogovan
        /// </summary>
        /// <returns>Uspech || Neuspech</returns>
        public static bool Logged()
        {
            if (string.IsNullOrEmpty(LoginID))
                return false;
            //Ok
            return true;
        }

        /// <summary>
        /// Odlogovani
        /// </summary>
        /// <returns>Uspech || Neuspech</returns>
        public static bool LogOut()
        {
            try
            {
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.Logout, LoginID, MachineID, SqlConnectionStringLocal);
                LoginID = string.Empty;
                LoginName = string.Empty;
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                OnLoginChanged();
            }
        }

        /// <summary>
        /// Prelogovani uzivatele
        /// </summary>
        /// <returns>Uspech || Neuspech</returns>
        public static bool LogIn(bool fromModule)
        {
            //Parametr udava, zda se prelogoavani vola z modulu, pokud ano, musi se uzivatel vratit
            //do puvodniho stavu, nikoliv vynulovat!

            //SqlConnection sqlconnection = null;

            try
            {
                frmLogin ologin = new frmLogin();
                //ologin.StartPosition = FormStartPosition.CenterParent;

                if (ologin.ShowDialog() == DialogResult.Cancel)
                {
                    //Pokud neni volano z modulu, nuluji
                    if (!fromModule)
                    {
                        LoginName = string.Empty;
                        LoginID = string.Empty;
                    }
                    return true;
                }

                if (Database.Classes.Vyroba_Local.LogUser(ologin.txtID.Text.Trim(), ologin.txtPWD.Text, SqlConnectionStringLocal))
                {
                    LoginID = ologin.txtID.Text.Trim();
                    LoginName = "Dopsat";
                    Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.Login, LoginID, MachineID, SqlConnectionStringLocal);

                    //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.Login, LoginID, MachineID, SqlConnectionStringLocal);
                    return true;
                }
                else
                {
                    DialogResult drErr = MessageBox.Show("Přihlášení se nezdařilo, ID: " + ologin.txtID.Text.Trim() + ", pass: " + ologin.txtPWD.Text + " " + SqlConnectionStringLocal, "Výroba", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                    if (drErr == DialogResult.Retry)
                        return false;
                    else
                        throw new LoginException("Login canceled...");
                }

                /*JoZ: upraveno, pouzivaji se knihovny pro praci s DB....viz. vyse...
                string sqlquery = "Select * from FASK_Logins where id='" + ologin.txtID.Text.Trim() + "' and psswd='" + ologin.txtPWD.Text + "'";
                sqlconnection = new SqlConnection(SqlConnectionStringLocal);
                SqlCommand sqlcommand = new SqlCommand(sqlquery, sqlconnection);

                sqlconnection.Open();
                SqlDataReader sqldaread = sqlcommand.ExecuteReader();
                if (sqldaread.Read())
                {
                    LoginID = ((string)(sqldaread["id"])).Trim();
                    LoginName = ((string)sqldaread["firstname"]).Trim() + " " + ((string)sqldaread["surname"]).Trim();
                    Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.Login, LoginID, MachineID, SqlConnectionStringLocal);
                    return true;
                }
                else
                {
                    MessageBox.Show("Přihlášení se nezdařilo", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }*/
            }
            catch (LoginException lex)
            {
                //Log.WriteException(lex);
                ExceptionHandler2.Handle(lex);
                return true;
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                //if (sqlconnection != null && sqlconnection.State == ConnectionState.Open)
                //    sqlconnection.Close();

                OnLoginChanged();
            }
        }
    }
}
