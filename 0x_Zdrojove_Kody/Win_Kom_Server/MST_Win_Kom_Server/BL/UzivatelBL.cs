using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fask.MST_W_Server.BL
{
    public class UzivatelBL
    {


        #region lokalni promenne

        private Fask.Server.Interfaces.Login.ILogin provider = null;


        #endregion

        #region konstruktor
        public UzivatelBL()
        {

        }

        public UzivatelBL(Fask.Server.Interfaces.Login.ILogin provider)
        {
            this.provider = provider;
        }
        #endregion

        #region inicializace
        public void Initialize(Func<string, string> map_path_function)
        {

            // Inicializuje objektove rozhrani ...
            try
            {
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Login;
                string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
                if (String.IsNullOrEmpty(providerAssemblyPath))
                    providerAssemblyPath = providerAssemblyPathGlobal;

                if (!String.IsNullOrEmpty(providerAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        Assembly providerAssemlby = Assembly.LoadFrom(map_path_function(@"~/" + providerAssemblyPath));
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                if (typeof(Fask.Server.Interfaces.Login.ILogin).IsAssignableFrom(t))
                                {
                                    provider = (Fask.Server.Interfaces.Login.ILogin)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda pro otestovaní vygenerovaní souboru pro uživatele
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>True - OK, False - Chyba</returns>
		public bool GetKatalogUzivateleTest(byte idterminal)
		{

			StatusObject so = new StatusObject();

			so = GetKatalogUzivatele(idterminal);

			if ((so.StatusText != "OK") && (so.Exception == true))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Metoda pro nachystani souboru uživatele podle ID TErminalu
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
		public StatusObject GetKatalogUzivatele(byte idterminal)
		{

			//Logika ukladani dat z Serveru do ctecky
			//
			//1. inicialozovat provider pro ctecku KAM sa ma ukladat data // .sdf, .sqlite....
			//2. naplnit dataset pro ulozeni do vystupneho souboru
			//2.1 budto cez provider pro druh z jakeho SQL serveru se to stahuje
			//2.2 ked je to provider tak stahne data z provideru
			//2.3 ked neni provider tak stahne z microsoft SQL defaultneho
			//3. rozhodovani ci se jedna o provider nebo nebo ne
			//4. pokud je to provider tak provede ulozeni do konkretneho souboru
			//4.1
			//5. pokud neni provider tak ulozi defaultne do sdf souboru
			//
			StatusObject so = new StatusObject();
			string dstFile = string.Empty; // 

			//providerterminal = InitProvider.InitProviderTerminal(idterminal, TypProvider.Login);

			try
			{

				Fask.DataSets.Uzivatele uzivatele = GetUzivatele(idterminal, ref so);

				if (so == null)
					so = new StatusObject();

				dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Uzivatele + Common.PRD);
				string statusFile = dstFile + Common.SO;

				//StatusObject so = new StatusObject(statusFile);
				if (so.Exists)
				{
					so.Read();
					so.StatusText = "File is still preparing\n" + so.StatusText;
					so.Exception = true;
					return so;
				}

				//TaD 11.12.2019 KOntrolovat pouze tento soubor anebo i .zip??  a kontrolovat to stejne všude? udelat nejakou spole4nou jednu metodu pro volani?
				if (File.Exists(dstFile))
				{
					so.StatusText = "File already prepared\n" + "Ready to download";
					so.Exception = false;
					return so;
				}

				so.Write("Preparing template");

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Uzivatele))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Uzivatele);
				}

				//tady byla priprava dat

				Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt_ce_users = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();

				foreach (Fask.DataSets.Uzivatele.UsersRow pr in uzivatele.Users)
				{
					Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow row = dt_ce_users.NewUsersRow();

					row.EAN = pr.IsEANNull() ? string.Empty : pr.EAN;
					row.FIRSTNAME = pr.IsFIRSTNAMENull() ? string.Empty : pr.FIRSTNAME;
					row.Hash = pr.IsHashNull() ? string.Empty : pr.Hash;
					row.ID = pr.ID;
					row.Login = pr.Login;
					row.Pwd = pr.IsPwdNull() ? string.Empty : pr.Pwd;
					row.SECONDNAME = pr.IsSECONDNAMENull() ? string.Empty : pr.SECONDNAME;

					dt_ce_users.AddUsersRow(row);

				}

				//so.Write("Filling data to database");


				//SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter zta = new Fask.MST_W_Server.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();
				//zta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	zta.Connection.Open();
				//	var Ztransakce = zta.Connection.BeginTransaction();
				//	zta.Update(dt_ce_users);
				//	Ztransakce.Commit();
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (zta != null)
				//	{
				//		if ((zta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			zta.Connection.Close();
				//		zta.Dispose();
				//	}
				//}


				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

				//so.Write("Shrinking database");

				//SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);


				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users ConUsers = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(dstFile))
					{

						so.Write("Filling data to database");
						ConUsers.Update(dt_ce_users);

						so.Write("Shrinking database");
						ConUsers.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;

			}
			catch (Exception ex)
			{

				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.SetException(ex);
				return so;
			}
			finally
			{
				so.Delete();
			}

		}

		/// <summary>
		/// Metoda pro nachystani souboru uživatele podle ID TErminalu
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="prava">Seznam Prav</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
		public StatusObject GetKatalogUzivateleSPravama(byte idterminal, string[] prava)
		{

			//Logika ukladani dat z Serveru do ctecky
			//
			//1. inicialozovat provider pro ctecku KAM sa ma ukladat data // .sdf, .sqlite....
			//2. naplnit dataset pro ulozeni do vystupneho souboru
			//2.1 budto cez provider pro druh z jakeho SQL serveru se to stahuje
			//2.2 ked je to provider tak stahne data z provideru
			//2.3 ked neni provider tak stahne z microsoft SQL defaultneho
			//3. rozhodovani ci se jedna o provider nebo nebo ne
			//4. pokud je to provider tak provede ulozeni do konkretneho souboru
			//4.1
			//5. pokud neni provider tak ulozi defaultne do sdf souboru
			//
			StatusObject so = new StatusObject();
			string dstFile = string.Empty; // 

			//providerterminal = InitProvider.InitProviderTerminal(idterminal, TypProvider.Login);

			try
			{

				Fask.DataSets.Uzivatele uzivatele = GetUzivatele(idterminal, new List<string>(prava), ref so);

				if (so == null)
					so = new StatusObject();

				dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + Common.Backslash + Common.Uzivatele + Common.PRD);
				string statusFile = dstFile + Common.SO;

				//StatusObject so = new StatusObject(statusFile);
				if (so.Exists)
				{
					so.Read();
					so.StatusText = "File is still preparing\n" + so.StatusText;
					so.Exception = true;
					return so;
				}

				//TaD 11.12.2019 KOntrolovat pouze tento soubor anebo i .zip??  a kontrolovat to stejne všude? udelat nejakou spole4nou jednu metodu pro volani?
				if (File.Exists(dstFile))
				{
					so.StatusText = "File already prepared\n" + "Ready to download";
					so.Exception = false;
					return so;
				}

				so.Write("Preparing template");

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));

				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Uzivatele))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Uzivatele);
				}

				//tady byla priprava dat

				Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt_ce_users = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();

				foreach (Fask.DataSets.Uzivatele.UsersRow pr in uzivatele.Users)
				{
					Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow row = dt_ce_users.NewUsersRow();

					row.EAN = pr.IsEANNull() ? string.Empty : pr.EAN;
					row.FIRSTNAME = pr.IsFIRSTNAMENull() ? string.Empty : pr.FIRSTNAME;
					row.Hash = pr.IsHashNull() ? string.Empty : pr.Hash;
					row.ID = pr.ID;
					row.Login = pr.Login;
					row.Pwd = pr.IsPwdNull() ? string.Empty : pr.Pwd;
					row.SECONDNAME = pr.IsSECONDNAMENull() ? string.Empty : pr.SECONDNAME;

					dt_ce_users.AddUsersRow(row);

				}

				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users ConUsers = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(dstFile))
					{

						so.Write("Filling data to database");
						ConUsers.Update(dt_ce_users);

						so.Write("Shrinking database");
						ConUsers.Shrink();
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

				so.Write("Compressing database");
				Fask.Compressing.Zip.Compress(dstFile);

				so.Write("File succesfully prepared");

				so.SetOK();
				return so;

			}
			catch (Exception ex)
			{

				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.SetException(ex);
				return so;
			}
			finally
			{
				so.Delete();
			}

		}


		/// <summary>
		/// Metoda pro vypočet HASH
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="login">login</param>
		/// <param name="password">Heslo</param>
		/// <returns></returns>
		public string GetHash(byte idterminal, string login, string password)
		{
			try
			{
				if (provider != null)
				{
					try
					{
						Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
						Fask.Server.Interfaces.Classes.User uzivatel = new User();

						terminal.ID = idterminal;
						uzivatel.Login = login;
						uzivatel.Password = password;

						return provider.Login_GetHash(
							uzivatel,
							terminal
							);
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						throw ex;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			string msg = "Provider v LoginService.asmx > 'GetHash(byte idterminal, string login, string password)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);

		}

		/// <summary>
		/// Metoda pro online oveřeni uživatele
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="idUser">ID uživatele</param>
		/// <param name="login">Login</param>
		/// <param name="password">Heslo</param>
		/// <param name="hashPassword">Hash Hesla</param>
		/// <param name="userID">reference na ID</param>
		/// <returns>True-OK, False- chyba</returns>
		public bool OnlineLogin(byte idterminal, int idUser, string login, string password, string hashPassword, ref int userID)
		{
			try
			{
				#region TaD - puvodni řešeni cez providera
				//if (provider != null)
				//{
				//    try
				//    {
				//        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
				//        Fask.Server.Interfaces.Classes.User uzivatel = new User();

				//        terminal.ID = idterminal;
				//        uzivatel.Login = login;
				//        uzivatel.ID = idUser;
				//        uzivatel.Password = password;

				//        return provider.Login_OnlineLogin(
				//            uzivatel,
				//            terminal,
				//            hashPassword,
				//            ref userID
				//            );
				//    }
				//    catch (Exception ex)
				//    {
				//        Log.writeErrorLog(ex.Message);
				//    }
				//} 
				#endregion

				#region TaD Nove řešeni pomoci třidy FASK.Logins


				FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim("M_");

				if ((pris != null) && (pris.FASK_Logins.Count > 0))
				{

					foreach (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow item in pris.FASK_Logins)
					{
						if ((item.USERID.Trim() == login) && (item.psswd.Trim() == password.Trim()))
						{
							return true;
						}
					}

					return false;
				}


				#endregion
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			string msg = "Provider v LoginService.asmx > 'OnlineLogin(byte idterminal, int idUser, string login, string password, string hashPassword, ref int userID)' nenastaven.";
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
			throw new Exception(msg);
		}

		#endregion

		#region Private metody

		/// <summary>
		/// Metoda pro dotaženi uživatele z Databaze
		/// </summary>
		/// <param name="idterminal">ID TErminalu</param>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>Dataset Uzivatele, naplnen uživatelama</returns>
		private Fask.DataSets.Uzivatele GetUzivatele(byte idterminal, ref StatusObject so)
		{
			#region TaD 21.8.2019 Puvodny spusob dotažení uživatele pomoci providera

			//Fask.DataSets.Uzivatele users = null;
			//try
			//{
			//    if (provider != null && (provider is Fask.Server.Interfaces.Login.ILogin)) //nove rozhrani objektove ...
			//    {
			//        Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();

			//        terminal.ID = idterminal;

			//        Fask.DataSets.Uzivatele usersProvider = ((Fask.Server.Interfaces.Login.ILogin)provider).Login_GetKatalogUzivatele(terminal, ref so);
			//        users = new Fask.DataSets.Uzivatele();
			//        foreach (Fask.DataSets.Uzivatele.UsersRow row in usersProvider.Users)
			//        {
			//            users.Users.ImportRow(row);
			//        }

			//        return users;
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Log.writeErrorLog("GetUzivatele");
			//    Log.writeErrorLog(ex);
			//}

			//string msg = "Provider v LoginService.asmx > 'GetUzivatele(byte idterminal, ref StatusObject so )' nenastaven.";
			//Log.writeErrorLog(msg);
			//throw new Exception(msg); 

			#endregion

			#region TaD 21.8.2019 Nový spusob pomoci třídy FASK.Logins

			Fask.DataSets.Uzivatele users = new Fask.DataSets.Uzivatele();

			FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim("M_");


			if ((pris != null) && (pris.FASK_Logins.Count > 0))
			{

				foreach (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow item in pris.FASK_Logins)
				{

					int ID = 0;

					if (int.TryParse(item.USERID, out ID))
					{
						users.Users.AddUsersRow(
							ID,
							item.USERID,
							item.psswd,
							string.Empty,
							item.USERID,
							item.firstname,
							item.surname);
					}
					else
					{
						Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Uživatel:'" + item.USERID + "' nemá ID jak číslo(int)!");
					}
				}
			}


			return users;
			#endregion

		}


		/// <summary>
		/// Metoda pro dotaženi uživatele z Databaze
		/// </summary>
		/// <param name="idterminal">ID TErminalu</param>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>Dataset Uzivatele, naplnen uživatelama</returns>
		private Fask.DataSets.Uzivatele GetUzivatele(byte idterminal, List<string> prava, ref StatusObject so)
		{

			#region TaD 21.8.2019 Nový spusob pomoci třídy FASK.Logins

			Fask.DataSets.Uzivatele users = new Fask.DataSets.Uzivatele();

			foreach (string pravoJedno in prava)
			{
				FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim(pravoJedno);


				if ((pris != null) && (pris.FASK_Logins.Count > 0))
				{

					foreach (FASK.Logins.DataSets.Pristupy.FASK_LoginsRow item in pris.FASK_Logins)
					{

						int ID = 0;

						if (int.TryParse(item.USERID, out ID))
						{

							var tmp = users.Users.Any(x => x.ID == ID);

							if (!tmp)
							{
								users.Users.AddUsersRow(
									ID,
									item.USERID,
									item.psswd,
									string.Empty,
									item.USERID,
									item.firstname,
									item.surname);
							}
						}
						else
						{
							Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Uživatel:'" + item.USERID + "' nemá ID jak číslo(int)!");
						}
					}
				}
			}


			return users;
			#endregion

		}


		#endregion
	}
}