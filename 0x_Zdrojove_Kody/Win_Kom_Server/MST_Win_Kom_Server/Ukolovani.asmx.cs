using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.Common;
using Fask.Logging;
using System.Reflection;
using Fask.MST_W_Server.Constants;
using Fask.MST_W_Server.SQLite_Classes;
using System.Data;
using System.Net.Mail;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Webova sluzba pro prenos ukolu ...
    /// </summary>
    [WebService(Namespace = "http://ukolovani.fask.cz/", Description="Služba pro přenos úkolů")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Ukolovani : System.Web.Services.WebService
    {
		#region Pomocná třída

		/// <summary>
		/// Pomocna třida Ukol
		/// </summary>
		public class Ukol
		{
			public int id = -1;
			public string nazev = string.Empty;
			public string state = string.Empty;
			public DateTime? datechanged = null;
			public int? useridchanged = null;
			public string note = string.Empty;
			public DateTime? datenotify = null;
			public DateTime? datefinished = null;
		} 

		#endregion

        Fask.Server.Interfaces.Ukolovani.IUkolovani provider = null;

		#region Konstruktor

		public Ukolovani()
		{
			// Inicializuje objektove rozhrani ...
			try
			{
				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
				string providerAssemblyPath = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider_Ukolovani;
				string providerAssemblyPathGlobal = Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.Providers[0].Provider;
				if (String.IsNullOrEmpty(providerAssemblyPath))
					providerAssemblyPath = providerAssemblyPathGlobal;

				if (!String.IsNullOrEmpty(providerAssemblyPath))
				{
					if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
					{
						Assembly providerAssemlby = Assembly.LoadFrom(Server.MapPath(providerAssemblyPath));
						Type[] types = providerAssemlby.GetTypes();
						foreach (Type t in types)
						{
							try
							{
								if (typeof(Fask.Server.Interfaces.Ukolovani.IUkolovani).IsAssignableFrom(t))
								{
									provider = (Fask.Server.Interfaces.Ukolovani.IUkolovani)providerAssemlby.CreateInstance(t.FullName);
									if (provider != null)
									{
										if (provider is Fask.Server.Interfaces.Configuration.IConfiguration)
										{
											((Fask.Server.Interfaces.Configuration.IConfiguration)provider).LoadConfiguration();
										}
										break;
									}
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

		#region Puvodne treba zrevidovat

		/// <summary>
		/// Priprava databaze ukolu pro stazeni a naslednou synchronizaci ...
		/// </summary>
		/// <param name="terminalid">id terminalu</param>
		/// <param name="userid">id uzivatele</param>
		/// <param name="filename">nazev souboru pro generovani aktualizace/synchronizace</param>
		/// <returns>OK, pokud probehlo a pripravila se databaze</returns>
		[WebMethod(Description = "Připraví databázi z aktuálních dat na serveru pro přenos do terminálu")]
		public StatusObject PrepareDB(byte terminalid, int userid, string filename)
		{
			StatusObject so = new StatusObject();

			try
			{

				string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, terminalid.ToString() + Common.Backslash + filename);

				if (!Directory.Exists(Path.GetDirectoryName(dstFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(dstFile));


				SQLite_Helper helper = new SQLite_Helper();
				if (!helper.SQLite_CreateFile(dstFile, Common.Ukoly))
				{
					throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Common.Ukoly);
				}


				Fask.DataSets.Ukoly ukoly = null;
				if (provider != null && (provider is Fask.Server.Interfaces.Ukolovani.IUkolovani)) //nove rozhrani objektove ...
				{
					Fask.Server.Interfaces.Classes.Terminal terminal = new Fask.Server.Interfaces.Classes.Terminal();
					Fask.Server.Interfaces.Classes.User user = new User();

					terminal.ID = terminalid;
					user.ID = userid;

					ukoly = provider.GetUkoly(terminal, user);

				}
				else //stare rozhrani neobjektove 
				{
					string msg = "Provider v Ukolovani.asmx > 'PrepareDB(byte terminalid, int userid, string filename)' nenastaven.";
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
					throw new Exception(msg);
				}

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly ConUkoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(dstFile))
				{
					ConUkoly.CZ_UKOL_STATE_Update(ukoly.CZ_UKOL_STATE);
					ConUkoly.CZ_UKOL_Update(ukoly.CZ_UKOL);
					ConUkoly.CZ_UKOL_UZIV_Update(ukoly.CZ_UKOL_UZIV);
					ConUkoly.Shrink();
				}

				//SQLite_Classes.SQLite_Helper.Shrink("Data source=" + dstFile);

				Fask.Compressing.Zip.Compress(dstFile);

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Terminál ID:" + terminalid.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				so.Exception = true;
				so.StatusText = ex.Message;
				return so;
			}

			so.SetOK();
			return so;
		}

		/// <summary>
		/// Aktualizace ukolu
		/// </summary>
		/// <param name="terminalid">id terminalu</param>
		/// <param name="userid">id uzivatele</param>
		/// <param name="ukol">Parametry ukolu k aktualizaci</param>
		/// <returns>OK, pokud projde, jinak vyjimka. Pri ok, muze terminal data ulozit a nemusi znovu synchronizovat</returns>
		[WebMethod(Description = "Aktualizace stavu ůkolu")]
		public StatusObject Update(byte terminalid, int userid, ref Ukol ukol)
		{
			return null;
			//ukol.useridchanged = userid;
			//ukol.datechanged = DateTime.Now;

			//StatusObject so = new StatusObject();
			//string select = "SELECT * FROM " + TABLE_CZ_UKOL_UZIV + " WITH (NOLOCK)"; 
			//// aktualizace ukolu(ů) [muze jich byt vice]
			//string update = "Update " + TABLE_CZ_UKOL_UZIV +
			//    " SET STATE=@state" +
			//    ", DateChanged=@datechanged" +
			//    ", UserIDChanged=@useridchanged" +
			//    ", NOTE=@note" +
			//    ", DateNotify=@datenotify" +
			//    ", DateFinished=@datefinished" +
			//    " WHERE ID=@id";

			//XConnection conn = new XConnection(xdb, xconnstring);
			//XDataAdapter xda = new XDataAdapter(xdb, select, conn.DatabaseConnection);

			//xda.UpdateCommand = (new XDatabase.XCommand(xdb, update, xda.SelectCommand.Connection)).DatabaseCommand;
			//xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@state", ukol.state)).DatabaseParameter);
			//if (ukol.datechanged.HasValue)
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datechanged", ukol.datechanged.Value)).DatabaseParameter);
			//else
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datechanged", System.DBNull.Value)).DatabaseParameter);
			//xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@useridchanged", ukol.useridchanged)).DatabaseParameter);
			//if (ukol.datenotify.HasValue)
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datenotify", ukol.datenotify.Value)).DatabaseParameter);
			//else
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datenotify", System.DBNull.Value)).DatabaseParameter);
			//if (ukol.datefinished.HasValue)
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datefinished", ukol.datechanged.Value)).DatabaseParameter);
			//else
			//    xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@datefinished", System.DBNull.Value)).DatabaseParameter);

			//xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@note", ukol.note)).DatabaseParameter);
			//xda.UpdateCommand.Parameters.Add((new XParameter(xdb, "@id", ukol.id)).DatabaseParameter);

			//    //Nacteni ukolu ...
			//int result = 0;
			//try
			//{
			//    xda.UpdateCommand.Connection.Open();
			//    result = xda.UpdateCommand.ExecuteNonQuery();
			//    // \TODO : doplnit transakce ...

			//    #region Action after data processed
			//    if (aDP_Action_Asynch)
			//    {
			//        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(AfterProcessedAction));
			//        thread.Start((int)ukol.id);
			//    }
			//    else
			//    {
			//        //if (!AfterProcessedAction((int)ukol.id, conn, null))
			//        //{
			//        //    throw new Exception("TasksService: AfterProcessAction not succeded on terminal=" + terminalid + " and ukol_uziv=" + ukol.id);
			//        //}
			//    }
			//    #endregion

			//    if (result > 0)
			//        so.SetOK();
			//    else
			//        so.StatusText = "Nepodařila se aktualizace záznamu";

			//}
			//catch (Exception ex)
			//{
			//    Log.writeErrorLog(ex.Message + "\n" + ex.StackTrace);
			//    so.Exception = true;
			//    so.StatusText = ex.Message;
			//    //throw ex;
			//}
			//finally
			//{
			//    if ((xda.UpdateCommand.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
			//        xda.UpdateCommand.Connection.Close();
			//}

			//return so;
		}

		/// <summary>
		/// Vlozeni noveho ukolu
		/// </summary>
		/// <param name="terminalid">id terminalu</param>
		/// <param name="userid">id uzivatele</param>
		/// <param name="ukol">ukol pro vlozeni</param>
		/// <returns>OK, pokud projde vlozeni. Po vlozeni je vracen objekt ukol s naplnenym ID z databaze, terminal muze lokalne ulozit</returns>
		[WebMethod(Description = "Vložení nového úkolu z terminálu")]
		public StatusObject Insert(byte terminalid, int userid, ref Ukol ukol)
		{
			StatusObject so = new StatusObject();

			// Vlozeni noveho ukolu
			// - do ukol.ID je ulozeno id nove vlozeneho ukolu, aby ho mohl terminal zavest u sebe bez nutnosti synchronizace...

			so.SetOK();
			return so;
		}

		#endregion

		#region Prenesene z Vyroby

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[WebMethod(Description = "Vraci seznam kontaktu na servisaku")]
		public Fask.DataSets.Ukoly GetServiceMan()
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Ukolovani.IUkolovani))
				{
					return provider.GetServiceMan();
				}

				string msg = "Provider v Ukolovani.asmx > 'GetServiceMan()' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
				throw new Exception(msg);

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

		/// <summary>
		/// Metoda slouzici pro odeslani emailu a odeslani SMS servisakovy
		/// </summary>
		/// <param name="UserID">ID uzivatele</param>
		/// <param name="MachineID">ID stroje</param>
		/// <param name="From"> Email odesilatele</param>
		/// <param name="To"> Email prijemce</param>
		/// <param name="Subject"> Predmet do odesilaneho emailu</param>
		/// <returns> bool hodnota ktera data najevo ci se odeslal email uspesne</returns>
		[WebMethod(Description = "Odeslani Emailu pomoci programu")]
		public bool SendEmailToServiceMan(FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable UserID, byte TerminalID, string MachineID, string To, string Subject)
		{
			return SendEmailToServiceManFull(UserID, TerminalID, MachineID, To, Subject, string.Empty);
		}

		/// <summary>
		/// Metoda slouzici pro odeslani emailu a odeslani SMS servisakovy
		/// </summary>
		/// <param name="UserID">ID uzivatele</param>
		/// <param name="MachineID">ID stroje</param>
		/// <param name="From"> Email odesilatele</param>
		/// <param name="To"> Email prijemce</param>
		/// <param name="Subject"> Predmet do odesilaneho emailu</param>
		/// <param name="poznamka"> Poznamka odesilana v emailu</param>
		/// <returns> bool hodnota ktera data najevo ci se odeslal email uspesne</returns>
		[WebMethod(Description = "Odeslani Emailu pomoci programu + poznamka")]
		public bool SendEmailToServiceManFull(FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable UserID, byte TerminalID, string MachineID, string To, string Subject, string poznamka)
		{
			try
			{
				if ((provider != null) && (provider is Fask.Server.Interfaces.Ukolovani.IUkolovani))
				{
					Fask.Server.Interfaces.Classes.User user = null;
					
					if ((UserID != null) && (UserID.Count > 0))
					{
						user = new User();
						user.ID = int.Parse(UserID[0].USERID);
						user.firstname = UserID[0].firstname;
						user.secondname = UserID[0].surname;
					}

					return provider.SendEmailToServiceMan(user, TerminalID, MachineID, To, Subject, poznamka);
				}

				string msg = "Provider v Ukolovani.asmx > 'SendEmailToServiceManFull(FASK.Logins.DataSets.Pristupy.FASK_LoginsDataTable UserID, byte TerminalID, string MachineID, string To, string Subject, string poznamka)' nenastaven.";
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
				throw new Exception(msg);

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex;
			}
		}

		#endregion

		#endregion




	}
}
