using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.MySystem
{
	/// <summary>
	/// Trida obsahujici metody pro zalogovani/odlogovani operatora
	/// </summary>
	public class LogOperator
	{
		public const int LoginLen = 15;
		public const int PasswdLen = 15;

		private static string operatorLogin = "";
        private static decimal userID = -1;

		/******************************************************************************************
										 Vlastni vyjimky
		******************************************************************************************/
		/// <summary>
		/// Trida vyjimek volana v pripade, ze je poskozen soubor hesel
		/// </summary>
		public class PasswdFileCorruptedException : System.IO.IOException
		{
			public PasswdFileCorruptedException(string m) : base(m)
			{}
		}

		/// <summary>
		/// Trida vyjimek volana v pripade, ze se nepodari najit zadany login
		/// </summary>
		public class LoginNotFoundException : System.IO.IOException
		{
			public LoginNotFoundException(string m) : base(m)
			{}
		}

		/// <summary>
		/// Trida vyjimek volana v pripade, ze bylo zadano spatne heslo
		/// </summary>
		public class IncorrectPasswordException : System.IO.IOException
		{
			public IncorrectPasswordException(string m) : base(m)
			{}
		}

		/******************************************************************************************
											Promenne
		******************************************************************************************/
		/// <summary>
		/// Handle na log soubor
		/// </summary>
		private static FileStream logFile = null;

		/******************************************************************************************
											Metody
		******************************************************************************************/
		/// <summary>
		/// Login aktualne prihlaseneho operatora
		/// </summary>
		public static string OperatorLogin
		{
			get{return operatorLogin;}
		}

        /// <summary>
        /// Vrati userID uzivatele
        /// </summary>
        public static decimal UserID
        {
            get
            {
                return userID;
            }
        }

		/// <summary>
		/// Zalogovani uzivatele.
		/// </summary>
		/// <param name="passwdFileName">Uplne jmeno souboru hesel</param>
		/// <param name="logFileName">Uplne jmeno logovaciho souboru</param>
		/// <param name="userLogin">Login uzivatele</param>
		/// <param name="userPasswd">Heslo uzivatele</param>
		/// <returns>Vraci 0 kdyz se zaloguje, jinak -1.</returns>
		/// <exception cref="FileNotFoundException">Neni -li nalezen soubor</exception>
		/// <exception cref="PasswdFileCorruptedException">Soubor s hesly je poskozen</exception>
		/// <exception cref="IOException">Chyba pri zapisu/cteni souboru</exception>
		/// <exception cref="LoginNotFoundException">Nebyl -li v souboru s hesly nalezen zadany login</exception>
		/// <exception cref="IncorrectPasswordException">Nespravne heslo</exception>
        /// <exception cref="XmlException">Chyba XML</exception>
        public static void Login(string passwdFileName, string logFileName,
            string userLogin, string userPasswd)
        {
            _WebRefernces_Globals.LoginServiceSession loginser = new Fask.MST_W._WebRefernces_Globals.LoginServiceSession();
            loginser.Timeout = MST_Global.ServiceTimeOut;
            loginser.Url = MST_Global.ServerAddress + "LoginService.asmx";
            loginser.UpdateWebServiceCredentials();

            //FileStream passwdFileStream;
            BinaryWriter bw;
            //SqlCEDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter users_ta = new Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();

            bool loginFound = false;

            if (!File.Exists(passwdFileName))
            { //stahneme databazi
                try
                {
                    Program.mstw.mbw.BeginPracujiForm("Aktualizace pøístupù");

                    LoginService.StatusObject so = loginser.GetKatalogUzivatele(MST_Global.TerminalID);
                    if (so.Exception)
                        throw new Exception("Aktualizace pøístupù:\n" + so.StatusText);

                    _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogUzivatelu(loginser);

                    Program.mstw.mbw.EndPracujiForm();
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    Program.mstw.mbw.EndPracujiForm();
                    throw new Exception();
                }
                #region oldcode
                /*
                ConfigurationService.Configuration configurations = new Fask.MST_W.ConfigurationService.Configuration();
                configurations.Timeout = MST_Global.ServiceTimeOut;
                configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
                configurations.UpdateWebServiceCredentials();

                string pwd = null;

                try
                {
                    pwd = configurations.GetPasswords();
                }
                catch //(Exception ex)
                {
                }

                if (pwd != null)
                {
                    StreamWriter sw = null;
                    try
                    {
                        sw = System.IO.File.CreateText(MST_W.Main.PasswordsFileName);
                        sw.Write(pwd);
                        sw.Close();
                        sw = null;
                    }
                    catch 
                    {
                    }
                    finally
                    {
                        if (sw != null)
                            sw.Close();
                    }
                }*/
                #endregion
            }

            //users_ta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + passwdFileName);
            //users_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + passwdFileName);
			Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable user_dt = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();
			try
			{
				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users usr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Main.CiselnikUzivateleDB))
				{
					user_dt = usr.GetDataByLogin(userLogin);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
            

            if (user_dt.Count <= 0)
                loginFound = false;
            else if (user_dt.Count > 1)
                throw new Exception("Nalezeno více stejných loginù");
            else
                loginFound = true;

            #region oldcode
            //XmlTextReader tr = new XmlTextReader(passwdFileStream);
            /*
            while (!tr.EOF)
            {// Vyhledam zadany login
                if (tr.MoveToContent() == XmlNodeType.Element && tr.Name == "login")
                {
                    tr.Read();
                    if (tr.ReadContentAsString() == userLogin)
                    {
                        loginFound = true;
                        break;
                    }
                }
                else
                    tr.Read();
            }
            */
            #endregion

            if (!loginFound)
            {// login nenalezen
                try
                {   //online overeni...
                    int id = 0;
                    if (loginser.OnlineLogin(MST_Global.TerminalID, -1, userLogin, userPasswd, "", ref id))
                    {
                        userID = id;
                        return;
                    }
                    else
                        throw new LoginNotFoundException("Login \'" + userLogin + "\' nenalezen v databázi operátorù ani se jej nepodaøilo ovìøit online!\nZadejte znovu.");
                }
                catch
                {
                    throw new LoginNotFoundException("Login \'" + userLogin + "\' nenalezen v databázi operátorù!\nZadejte znovu.");
                }
            }

            operatorLogin = userLogin;

            if (!user_dt[0].IsPwdNull() && user_dt[0].Pwd.Trim().Length > 0)
            { //je zadano heslo v databazi (neni hashovano), overime zda jsou obe hesla stejna...
                if (user_dt[0].Pwd.Trim() == userPasswd)
                    userID = user_dt[0].ID;
                else
                    throw new IncorrectPasswordException("Zadáno nesprávné heslo!\nZadejte znovu.");
            }
            else if (!user_dt[0].IsHashNull() && user_dt[0].Hash.Trim().Length > 0)
            {
                try
                {
                    string hashUserPsw = loginser.GetHash(MST_Global.TerminalID, userLogin, userPasswd);
                    if (user_dt[0].Hash.Trim() == hashUserPsw)
                        userID = user_dt[0].ID;
                    else
                        throw new IncorrectPasswordException("Zadáno nesprávné heslo!\nZadejte znovu.");
                }
                catch
                {
                    throw new IncorrectPasswordException("Nepodaøilo se vytvoøit HASH zadaného hesla!\nZadejte znovu.");
                }

            }

            #region oldcode
            // login nalezen, porovna heslo
            //while (!(tr.MoveToContent() == XmlNodeType.Element && tr.Name == "password")) 
            //    tr.Read();
            /*
            tr.Read();
            if (tr.ReadContentAsString() != userPasswd)
            {// spatne heslo
                passwdFileStream.Close();
                logFile.Close();
                throw new IncorrectPasswordException("Zadáno nesprávné heslo!\nZadejte znovu.");
            }
            */


            // heslo platne, nacte userID
            /*while (!(tr.MoveToContent() == XmlNodeType.Element && tr.Name == "id")) 
                tr.Read();

            tr.Read();
            userID = tr.ReadContentAsDecimal();
            */
            #endregion

            // zjisti datum a cas
            DateTime dt = DateTime.Now;
            string logRec;
            logFile = File.Open(logFileName, FileMode.Append, FileAccess.Write);
            logRec = (new StringBuilder(75)).Append(String.Format("{0,2:D}-{1,2:D}-{2,4:D} {3,2:D}:{4,2:D}:{5,2:D} ",
                dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second)).Append(userLogin + " prihlasen\n").ToString();

            bw = new BinaryWriter(logFile);
            bw.Write(logRec);

            //passwdFileStream.Close();
            logFile.Close();
        }

		/// <summary>
		/// Odloguje uzivatele
		/// </summary>
		/// <param name="logFileName">Uplne jmeno logovaciho souboru</param>
		/// <exception cref="FileNotFoundException">Neni -li nalezen soubor</exception>
		/// <exception cref="IOException">Chyba pri zapisu/cteni souboru</exception>
		public static void Logout(string logFileName)
		{
			BinaryWriter bw;

			operatorLogin = null;

			try
			{
				logFile = File.Open(logFileName, FileMode.Append, FileAccess.Write);
			}
			catch (FileNotFoundException e)
			{
				throw new FileNotFoundException("Nenalezen soubor: "+ e.Message);
			}

			// zjisti datum a cas
			DateTime dt = DateTime.Now;
			string logRec;

			logRec = (new StringBuilder(75)).Append(String.Format("{0,2:D}-{1,2:D}-{2,4:D} {3,2:D}:{4,2:D}:{5,2:D} ",
				dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second)).Append("odhlasen\n").ToString();

			bw = new BinaryWriter(logFile);
			bw.Write(logRec);

			logFile.Close();

		}
		
		/// <summary>
		/// Koduje/dekoduje pole znaku
		/// </summary>
		/// <param name="arrOfChar">Pole znaku ke kodovani</param>
		/// <param name="LenOfPasswd">Delka hesla v poli znaku</param>
		/// <returns>Kodovane pole znaku</returns>
		private static byte[] CodeArrOfBytes(byte[] arrOfBytes, int LenOfPasswd)
		{
			for (int i = 0; i < LenOfPasswd; i++)
				arrOfBytes[i] = (byte)(0xAB ^ arrOfBytes[i]); 
			return arrOfBytes;
		}

        public static List<string> GetLogins(string passwdFileName)
        {
            List<string> logins = new List<string>();
            try
            {
				Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users usr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Main.CiselnikUzivateleDB))
					{
						dt = usr.GetData();
					}
				}
				catch (Exception ex)
				{
					Logging.Log.Write(ex);
				}

                foreach (Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow row in dt)
                {
                    logins.Add(row.Login.Trim());
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            return logins;
        }
	}
}
