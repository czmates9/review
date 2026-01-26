using Fask.Vyroba_P;
using Fask.Vyroba_P.MySystem;
using System;
using System.IO;

namespace Fask.SQLiteDBs.DataSets
{


    partial class InternalState
    {
        public static void UpdateInternalStateLstOperationUser(string userID, DateTime lastProduction)
        {
            //Aktualizace interniho stavu konci operaci pro uzivatele...
            try
            {
                InternalState.LstOperationUserDataTable loudt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetDataByUserID(userID);
                if (loudt.Count == 0)
                    loudt.AddLstOperationUserRow(userID, lastProduction);
                else
                    loudt[0].LastOper = lastProduction;
                Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.Update(loudt);
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
                Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.DeleteUserID(userID);
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
                return Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetLastOperationDateTime(userID);

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
                return Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.DeleteAll();
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
