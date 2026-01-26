using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.OdvadeniVyroby.Data
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

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.GetDataByUserID_Production(UserID);
                if (dt_lst_LProduction.Count > 0)
                    lastUserActionDateTimeLocal = dt_lst_LProduction[0].dateeve;

                // Zjisteni posledni akce uzivatele...
                try
                {
                    if (DataInfo_Static.VyrobaGO_Instance.vyrobaServis != null)
                        lastUserActionDateTimeServer = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.UserLastAction(UserID);
                    else
                        lastUserActionDateTimeServer = null;
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ews);
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

                Fask.Logging.ExceptionHandler2.Handle(ex);

                return null;
            }
        }
    }
}