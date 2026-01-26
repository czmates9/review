using System;
using System.IO;
namespace Fask.Vyroba_W.Data {


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
                Logging.Log.Write(ex.Message, "UpdateInternalStateLstOperationUser");
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
                Logging.Log.Write(ex.Message, "DeleteInternalStateLstOperationUser");
            }
        }
    }
}
