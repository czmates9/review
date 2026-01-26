using System;
using System.IO;
namespace Fask.Vyroba_W.Data
{

    partial class InternalStateDataSet
    {
        partial class LstOperationUserDataTable
        {
        }

        public static void UpdateInternalStateLstOperationUser(string userID, DateTime lastProduction)
        {
            //Aktualizace interniho stavu konci operaci pro uzivatele...
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_W.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                Data.InternalStateDataSet.LstOperationUserDataTable loudt = louta.GetDataByUserID(userID);
                if (loudt.Count == 0)
                    loudt.AddLstOperationUserRow(userID, lastProduction);
                else
                    loudt[0].LastOper = lastProduction;
                louta.Update(loudt);
            }
            catch (Exception ex)
            {
                LoggingCE.Log.Write(ex.Message, "UpdateInternalStateLstOperationUser");
            }
        }

        public static void DeleteInternalStateLstOperationUser(string userID)
        {
            //Aktualizace interniho stavu konci operaci pro uzivatele...
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_W.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                louta.DeleteUserID(userID);
            }
            catch (Exception ex)
            {
                LoggingCE.Log.Write(ex.Message, "DeleteInternalStateLstOperationUser");
            }
        }

        public static DateTime? GetInternalStateLstOperationUser(string userID)
        {
            try
            {
                //Zjisteni posledniho casu operace pracovnika
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter lstoperta = new Fask.Vyroba_W.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                return lstoperta.GetLastOperationDateTime(userID) ?? DateTime.MinValue;

            }
            catch (Exception ex)
            {
                LoggingCE.Log.Write(ex.Message, "GetInternalStateLstOperationUser");
                return null;
            }
        }

        public static int DeleteAll()
        {
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_W.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                return louta.DeleteAll();
            }
            //catch (Exception ex)
            catch
            {
                return 0;
            }
        }
    }
}