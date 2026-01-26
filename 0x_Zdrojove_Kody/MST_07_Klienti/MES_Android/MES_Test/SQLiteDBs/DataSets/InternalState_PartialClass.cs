using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
				InternalState.LstOperationUserDataTable loudt = MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_InternalState.GetDataByUserID(userID);
				if (loudt.Count == 0)
					loudt.AddLstOperationUserRow(userID, lastProduction);
				else
					loudt[0].LastOper = lastProduction;
				MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_InternalState.Update(loudt);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "UpdateInternalStateLstOperationUser");
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		public static void DeleteInternalStateLstOperationUser(string userID)
		{
			//Aktualizace interniho stavu konci operaci pro uzivatele...
			try
			{
				//InternalStateTableAdapters.LstOperationUserTableAdapter louta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//louta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_InternalState.DeleteUserID(userID);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "DeleteInternalStateLstOperationUser");
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		public static DateTime? GetInternalStateLstOperationUser(string userID)
		{
			try
			{
				//Zjisteni posledniho casu operace pracovnika
				//InternalStateTableAdapters.LstOperationUserTableAdapter lstoperta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				return MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_InternalState.GetLastOperationDateTime(userID) ?? DateTime.MinValue;

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "GetInternalStateLstOperationUser");
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public static int DeleteAll()
		{
			try
			{
				//InternalStateTableAdapters.LstOperationUserTableAdapter louta = new InternalStateTableAdapters.LstOperationUserTableAdapter();
				//louta.Connection.ConnectionString = "Data source=" + Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD);
				return MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_InternalState.DeleteAll();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "DeleteAll");
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return 0;
			}
		}
	}
}