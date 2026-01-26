using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FASK.SledovaniVyroby.ErrorLog;
using System.Windows.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class Database
    {
        static private ICommDatabase.ICommDatabase database = null;

        public static bool InsertNewUserEvents(string loginid, string statusid, string rez1 , string rez2 )
        {
            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return false;

            return database.UserEventsInsert(loginid, Logging.LogConfig.MachineID.Trim(), statusid, "", rez1,rez2);
        }

        public static bool InsertNewEvents(string loginid, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material)
        {
            if (qty == 0 && qtyreal == 0) //nebudeme ukladat nulove pocty
                return true;

            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return false;

            System.Data.SqlClient.SqlTransaction tr = null;

            Log.WriteDataStoreCSV(loginid, qty, qtyreal, barcodeReaded);

            bool succ = database.EventsInsert(loginid, ref tr, "", qty, qtyreal, description, barcodeReaded, barcodeSended, zakazka, popis, reportType, IDO, scan1, scan2, scan3, sensor, material, Logging.LogConfig.MachineID.Trim());

            return succ;
        }

        private static ICommDatabase.ICommDatabase GetCommDatabaseProvider()
        {
            try
            {
                ICommDatabase.ICommDatabase provider = null;
                //"C:\\FASK\\SVN\\Nadop\\SledovaniVyroby\\!Build!\\Vyroba\\Debug\\SQLCECommLib.dll"
                string path = AgroConfig.pathDllLibraryDatabase;
                Assembly ass = Assembly.LoadFrom(path);
                Type[] types = ass.GetTypes();

                foreach (Type t in types)
                {
                    try
                    {
                        provider = (ICommDatabase.ICommDatabase)ass.CreateInstance(t.FullName);
                        if (provider != null)
                            break;
                    }
                    catch
                    { }
                }

                provider.LoadConfiguration();

                return provider;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }
}
