using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Data;
using System.Data.Common;
using Fask.Logging;

using System.Data.SqlClient;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba přenosu dat EventsService
    /// </summary>
	[WebService(Namespace = "http://EventsService.fask.cz/", Description = "Služba přenosu dat EventsService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EventsService : System.Web.Services.WebService
    {

		#region Konstruktor
		/// <summary>
		/// Konstruktor
		/// </summary>
		public EventsService()
		{
			
		} 
		#endregion

		#region WebMetody

		/// <summary>
		/// Metoda pro zpracování udalosti na serveru
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="eventData">Data pro zpracovaní</param>
		/// <returns>True- OK, False, Chyba</returns>
		[WebMethod(Description = "Metoda pro zpracování udalosti na serveru")]
		public bool ProcessEventData(byte idterminal, Fask.DataSets.Events eventData)
		{
			SqlTransaction iTrans1 = null;
			SqlConnection xConnection1 = new SqlConnection();
			SqlDataAdapter xDataAdapter1 = new SqlDataAdapter();

			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();


                xConnection1 = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
				xConnection1.Open();

				//Vlozeni do databaze
				//xConnection1.Open();
				iTrans1 = xConnection1.BeginTransaction();

				xDataAdapter1.SelectCommand.Transaction = iTrans1;
				xDataAdapter1.InsertCommand.Transaction = iTrans1;

				//xDataAdapterDI.DatabaseDataAdapter.InsertCommand.Prepare();
				xDataAdapter1.ContinueUpdateOnError = true;
				xDataAdapter1.Update(eventData.CZMST_EventsUser.Select(null, null, DataViewRowState.Added));



				if (iTrans1 != null)
					iTrans1.Commit();

				Fask.Logging.ExceptionHandler2.Handle(eventData.CZMST_EventsUser);

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);


				try
				{
					if (iTrans1 != null)
						iTrans1.Rollback();
				}
				catch (System.Exception ex2)
				{
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}



				return false;
			}
			finally
			{
				try
				{
					if (xConnection1.State == ConnectionState.Open)
						xConnection1.Close();
				}
				catch (Exception ex)
				{
					//Log.writeErrorLog("Events", "xConnection1.close()", ex.Message);
					Fask.Logging.ExceptionHandler2.Handle( this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}
			}


			return true;

		}

		/// <summary>
		/// Metoda pro zpracovaní soboru jen podle jeho jmena a ID Terminalu
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="filename">pouze jmeno souboru</param>
		/// <returns></returns>
		[WebMethod(Description = "Metoda pro zpracovaní soboru jen podle jeho jmena a ID Terminalu")]
		public bool ProcessEventFileName(byte idterminal, string filename)
		{
			string dstFile = Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, idterminal.ToString() + @"\" + filename);
			return ProcessEventFile(idterminal, dstFile);
		}

		/// <summary>
		/// Metoda pro Zpracovani udalosti z souboru
		/// </summary>
		/// <param name="idterminal">ID Terminalu</param>
		/// <param name="dstFile">Plna cesta k souboru</param>
		/// <returns>True-OK, False, chyba</returns>
		[WebMethod(Description = "Metoda pro Zpracovani udalosti z souboru")]
		public bool ProcessEventFile(byte idterminal, string dstFile)
		{
			FileStream fs = new FileStream(dstFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] data = new byte[(new FileInfo(dstFile)).Length];
			fs.Read(data, 0, data.Length);
			fs.Close();
			fs = null;

			SqlTransaction iTrans1 = null;
			SqlConnection xConnection1 = null;
			SqlDataAdapter xDataAdapter1 = new SqlDataAdapter();

			try
			{
                Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();
                xConnection1 = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);
				xConnection1.Open();

				Fask.DataSets.Events eventData = new Fask.DataSets.Events();
				Fask.SQLiteDBs.DataSets.EventsUser eventDataCE = new Fask.SQLiteDBs.DataSets.EventsUser();

				//Fask.SQLiteDBs.DataSets.EventsTableAdapters.CZMST_EventsUserTableAdapter eta = new Fask.MST_W_Server.SQLiteDBs.DataSets.EventsTableAdapters.CZMST_EventsUserTableAdapter();
				//eta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + dstFile);
				//try
				//{
				//	eta.Fill(eventDataCE.CZMST_EventsUser);
				//}
				//catch (Exception ex)
				//{
				//	Logging.ExceptionHandler2.Handle(ex);
				//}
				//finally
				//{
				//	if (eta != null)
				//	{
				//		if ((eta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				//			eta.Connection.Close();
				//		eta.Dispose();
				//	}
				//} 

				using (Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsUser ConEvUs = new Fask.SQLiteDBs.Controllers.SQLite_Controller_EventsUser(dstFile))
				{
					ConEvUs.Fill(eventDataCE.CZMST_EventsUser);
				}

				eventData.CZMST_EventsUser.BeginLoadData();
				foreach (Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserRow er in eventDataCE.CZMST_EventsUser)
				{
					Fask.DataSets.Events.CZMST_EventsUserRow ner = eventData.CZMST_EventsUser.NewCZMST_EventsUserRow();

					ner.eguid = er.eguid;
					ner.eid = er.eid;
					ner.etype = er.etype;
					ner.etime = er.etime;
					ner.termid = er.termid;
					ner.userid = er.userid;
					if (!er.IsloginidNull())
						ner.loginid = er.loginid;
					if (!er.IsmachineidNull())
						ner.machineid = er.machineid;
					if (!er.IsmodulNull())
						ner.modul = er.modul;
					if (!er.IscountentriesNull())
						ner.countentries = er.countentries;
					if (!er.IsdocnmbrNull())
						ner.docnmbr = er.docnmbr;
					if (!er.IsitemnmbrNull())
						ner.itemnmbr = er.itemnmbr;
					if (!er.IsREZ1Null())
						ner.REZ1 = er.REZ1;
					if (!er.IsREZ2Null())
						ner.REZ2 = er.REZ2;
					//);
					eventData.CZMST_EventsUser.AddCZMST_EventsUserRow(ner);
				}
				eventData.CZMST_EventsUser.EndLoadData();


				//Vlozeni do databaze
				iTrans1 = xConnection1.BeginTransaction();

				xDataAdapter1.SelectCommand.Transaction = iTrans1;
				xDataAdapter1.InsertCommand.Transaction = iTrans1;

				xDataAdapter1.ContinueUpdateOnError = true;
				xDataAdapter1.Update(eventData.CZMST_EventsUser.Select(null, null, DataViewRowState.Added));


				if (iTrans1 != null)
					iTrans1.Commit();

				Fask.Logging.ExceptionHandler2.Handle(eventData.CZMST_EventsUser);

				//if (eventData.CZMST_EventsUser.HasErrors)
				//{
				//    Log.writeErrorLog("EventService", "ProcessEventFile", "File '" + dstFile + "' has errors");
				//    Log.writeErrorData(eventData, idterminal + "." + "events");
				//}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (iTrans1 != null)
						iTrans1.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}
				
				string file = Fask.Logging.ExceptionHandler2.Handle(data, idterminal + "." + "events");
				Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, file);

				throw ex;
			}
			finally
			{
				try
				{
					if (xConnection1.State == ConnectionState.Open)
						xConnection1.Close();
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}

				try
				{
					//Log.writeOKData(data, idterminal.ToString() + "." + "events");
					File.Delete(dstFile);
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}
			}

			return true;

		}


		#endregion







    }
}
