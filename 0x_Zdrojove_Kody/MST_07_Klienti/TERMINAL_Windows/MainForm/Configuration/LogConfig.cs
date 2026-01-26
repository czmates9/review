using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FASK.MST_WINDOWS.Main.Configuration;
using System.IO;
using System.Windows.Forms;
using Logging;
using System.Data.SqlClient;
using System.Data;
using FASK.MST_WINDOWS.ErrorLog;
using System.DirectoryServices.AccountManagement;



namespace FASK.MST_WINDOWS.Logging
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


    public class LogConfig
    {
        /// <summary>
        /// Nazev konfiguracniho souboru modulu
        /// </summary>
        //private const string cfilename = "loginconfig.xml";

        /// <summary>
        /// Cesta konfiguracniho souboru modulu - vytvoren v konstruktoru
        /// </summary>
        //private static string configfilename;
        //public static string ConfigFileName
        //{
        //    get { return configfilename; }
        //}

        /// <summary>
        /// Dataset pro konfiguraci - informace o jednotlivych modulech
        /// </summary>
        //public static ConfigTable config = new ConfigTable();

        /// <summary>
        /// Bezparametrovy konstruktor
        /// </summary>
        //static LogConfig()
        //{
        //    //Sestaveni cesty
        //    string filename = Path.GetDirectoryName(Application.ExecutablePath);
        //    configfilename = Path.Combine(filename, cfilename);

        //    //Nacteni
        //    Load();
        //}

        /// <summary>
        /// Nacteni informace o modulech
        /// </summary>
        //public static void Load()
        //{
        //    try { config.ReadXml(ConfigFileName); }
        //    catch { }
        //}


        /// <summary>
        /// Ulozeni informaci o modulech
        /// </summary>
        //public static void Save()
        //{
        //    try { config.WriteXml(ConfigFileName); }
        //    catch { }
        //}

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

        #region Promenne nactene 




        #endregion

        /// <summary>
        /// Zobrazovane jmeno
        /// </summary>
        public static string LoginString
        {
            get
            {
                try { return "<" + Config.LoginID + ", " + Config.LoginName.Trim() + ">"; }
                catch { return "<,>"; }
            }
        }


        /// <summary>
        /// Zda je uzivatel zalogovan
        /// </summary>
        /// <returns>Uspech || Neuspech</returns>
        public static bool Logged()
        {
            return Config.LoginID.HasValue;

            //if (!LoginID.HasValue)
            //    return false;
            ////Ok
            //return true;
        }

        /// <summary>
        /// Odlogovani
        /// </summary>
        /// <returns>Uspech || Neuspech</returns>
        public static bool LogOut()
        {
            try
            {

                Config.LoginID = null;
                Config.LoginName = string.Empty;
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


        #region Metoda slouzici pro overeni uzivatele

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
                frmLogin1 ologin = new frmLogin1();
                //ologin.StartPosition = FormStartPosition.CenterParent;

                if (ologin.ShowDialog() == DialogResult.Cancel)
                {
                    //Pokud neni volano z modulu, nuluji
                    if (!fromModule)
                    {
                        Config.LoginName = string.Empty;
                        Config.LoginID = null;
                    }
                    return true;
                }

                //Logovani kontrola uživatele
                Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow Uzivatel;

                if (LogUser(ologin.txtID.Text.Trim(), ologin.txtPWD.Text,out Uzivatel))
                {
                    Config.LoginID = Uzivatel.ID;
                    Config.LoginName = Uzivatel.FIRSTNAME.Trim() + " " + Uzivatel.SECONDNAME.Trim();

                    //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.Login, LoginID, MachineID, SqlConnectionStringLocal);

                    //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.Login, LoginID, MachineID, SqlConnectionStringLocal);
                    return true;
                }
                else
                {
                    DialogResult drErr = MessageBox.Show("Přihlášení se nezdařilo, Login: " + ologin.txtID.Text.Trim() , "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
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
                Log.WriteException(lex);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                return false;
            }
            finally
            {
                //if (sqlconnection != null && sqlconnection.State == ConnectionState.Open)
                //    sqlconnection.Close();

                OnLoginChanged();
            }
        }
        #endregion


        private static bool LogUser(string Login, string Heslo, out Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow uzivatel)
        {
            bool isValidAD = false;
            uzivatel = null;

            Log.Write("Overeni Uzivatele");

            if (Config.Main_OverVuciAD)
            {
                //overeni vuci active directory
                try
                {

                    Log.Write("Domena:" + Config.Main_DomenaProAD );
                    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Config.Main_DomenaProAD))
                    {
                        // validate the credentials
                        isValidAD = pc.ValidateCredentials(Login.Trim(), Heslo.Trim());

                       

                        if (!isValidAD)
                            return false;

                         Log.Write("Overeni Uzivatele : true");
                    }

                }
                catch (Exception ex)
                {
                      DialogResult drErr = MessageBox.Show("Přihlášení pomoci AD se nezdařilo pro Login: " + Login.Trim() , "MST Windows", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      Log.Write(ex);
                      return false;

                }
            }


            try
            {
                MST_WINDOWS.Main.SQLCEDBS.DataSets.UzivateleTableAdapters.UsersTableAdapter uzivatele = new Main.SQLCEDBS.DataSets.UzivateleTableAdapters.UsersTableAdapter();
                uzivatele.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data Source=" + MyPath.UzivateleDirectory);

                Log.Write("Overeni Uzivatele login : " + Login.Trim());

                var login = uzivatele.GetDataByLogin(Login.Trim());

                Log.Write("Overeni Uzivatele pocet : " + login.Count().ToString() );

                if (login.Count() > 0)
                {
                    uzivatel = login[0];

                    Log.Write("Overeni Uzivatele uzivatel : " + uzivatel.Login);

                    if (!Config.Main_OverVuciAD)
                    {
                        if ((uzivatel.Login.Trim() == Login.Trim()) && (uzivatel.Pwd.Trim() == Heslo.Trim()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }


                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                      Log.Write(ex);
                      return false;
            }

           // return false;
        }

    }
}
