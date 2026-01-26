using System;
using System.IO;
using Fask.Vyroba_W;
using Fask.Vyroba_W.MySystem;

namespace Fask.SQLiteDBs.DataSets
{

	partial class InternalState
	{
		partial class LstOperationUserDataTable
		{
		}

		public static void UpdateInternalStateLstOperationUser(string userID, DateTime lastProduction)
		{
			//Aktualizace interniho stavu konci operaci pro uzivatele...
			try
			{
				//InternalStateTableAdapters.LstOperationUserTableAdapter louta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//louta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				InternalState.LstOperationUserDataTable loudt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetDataByUserID(userID);
				if (loudt.Count == 0)
					loudt.AddLstOperationUserRow(userID, lastProduction);
				else
					loudt[0].LastOper = lastProduction;
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.Update(loudt);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "UpdateInternalStateLstOperationUser");
			}
		}

		public static void DeleteInternalStateLstOperationUser(string userID)
		{
			//Aktualizace interniho stavu konci operaci pro uzivatele...
			try
			{
				//InternalStateTableAdapters.LstOperationUserTableAdapter louta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//louta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.DeleteUserID(userID);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "DeleteInternalStateLstOperationUser");
			}
		}

		public static DateTime? GetInternalStateLstOperationUser(string userID)
		{
			try
			{
				//Zjisteni posledniho casu operace pracovnika
				//InternalStateTableAdapters.LstOperationUserTableAdapter lstoperta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				return Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetLastOperationDateTime(userID) ?? DateTime.MinValue;

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex.Message, "GetInternalStateLstOperationUser");
				return null;
			}
		}

		public static int DeleteAll()
		{
			try
			{
				//InternalStateTableAdapters.LstOperationUserTableAdapter louta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//louta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				return Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.DeleteAll();
			}
			//catch (Exception ex)
			catch
			{
				return 0;
			}
		}
	}
}