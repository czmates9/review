using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Vyroba_W.Data
{
    public class DatabaseActions
    {
        public static DateTime? UserLastAction(string UserID)
        {
            DateTime? lastUserActionDateTime = null;
            DateTime? lastUserActionDateTimeLocal = null;
            DateTime? lastUserActionDateTimeServer = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
				//pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

				Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserID_Production(UserID);
                if (dt_lst_LProduction.Count > 0)
                    lastUserActionDateTimeLocal = dt_lst_LProduction[0].dateeve;

                // Zjisteni posledni akce uzivatele...
                try
                {
					if (Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis != null)
						lastUserActionDateTimeServer = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.UserLastAction(UserID);
                    else
                        lastUserActionDateTimeServer = null;
                }
                catch (Exception ews)
                {
					Logging.Log.Write(ews);
                }

                if (lastUserActionDateTimeLocal.HasValue)
                    lastUserActionDateTime = lastUserActionDateTimeLocal;

                if (lastUserActionDateTimeServer.HasValue)
                {
                    if (!lastUserActionDateTime.HasValue)
                        lastUserActionDateTime = lastUserActionDateTimeServer;
                    else if (lastUserActionDateTime.Value < lastUserActionDateTimeServer.Value)
                        lastUserActionDateTime = lastUserActionDateTimeServer;
                }

                return lastUserActionDateTime;

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);

                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
