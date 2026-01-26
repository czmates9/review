using System;
using System.IO;
namespace Fask.Vyroba_P.Data
{


    partial class InternalStateDataSet
    {
        public static void UpdateInternalStateLstOperationUser(string userID, DateTime lastProduction)
        {
            //Aktualizace interniho stavu konci operaci pro uzivatele...
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
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
                Fask.Logging.ExceptionHandler2.Handle("InternalStateDataSet", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void DeleteInternalStateLstOperationUser(string userID)
        {
            //Aktualizace interniho stavu konci operaci pro uzivatele...
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                louta.DeleteUserID(userID);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("InternalStateDataSet", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static DateTime? GetInternalStateLstOperationUser(string userID)
        {
            try
            {
                //Zjisteni posledniho casu operace pracovnika
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter lstoperta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                //return lstoperta.GetLastOperationDateTime(userID) ?? DateTime.MinValue;
                return (DateTime?)lstoperta.GetLastOperationDateTime(userID);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("InternalStateDataSet", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
        }

        public static int DeleteAll()
        {
            try
            {
                Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                return louta.DeleteAll();
            }
            catch (Exception ex)
            //catch
            {
                Fask.Logging.ExceptionHandler2.Handle("InternalStateDataSet", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }
    }
}
