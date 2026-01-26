using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FASK.SledovaniVyroby.ErrorLog;
using System.Windows.Forms;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class Database_AGRO2
    {
        static private ICommDatabase.ICommDatabase database = null;

        /// <summary>
        /// Ulozeni Udalosti obsluhy
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="statusid"></param>
        /// <param name="rez1"></param>
        /// <param name="rez2"></param>
        /// <returns></returns>
        //public static int InsertNewUserEvents(string loginid, string statusid, string rez1 , string rez2 )
        //{
        //    if (database == null)
        //        database = GetCommDatabaseProvider();

        //    if (database == null)
        //        return 0;

        //    return database.UserEventsInsert(loginid, Logging.LogConfig.MachineID.Trim(), statusid, "", rez1, rez2);
        //}

        /// <summary>
        /// Ulozeni Udalosti obsluhy
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="statusid"></param>
        /// <param name="rez1"></param>
        /// <param name="rez2"></param>
        /// <returns> TaD Zmena return typu z int na bool</returns>
        public static bool InsertNewUserEvents(string loginid, string statusid, string rez1, string rez2)
        {
            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return false;

            return database.UserEventsInsert(loginid, Logging.LogConfig.MachineID.Trim(), statusid, "", rez1, rez2);
        }

        /// <summary>
        /// Ulozeni dat odvodu vyroby do databaze
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="qty"></param>
        /// <param name="qtyreal"></param>
        /// <param name="description"></param>
        /// <param name="barcodeReaded"></param>
        /// <param name="barcodeSended"></param>
        /// <param name="zakazka"></param>
        /// <param name="popis"></param>
        /// <param name="reportType"></param>
        /// <param name="IDO"></param>
        /// <param name="scan1"></param>
        /// <param name="scan2"></param>
        /// <param name="scan3"></param>
        /// <param name="sensor"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        //public static int InsertNewEvents(string loginid, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material)
        //{
        //    if (qty == 0 && qtyreal == 0) //nebudeme ukladat nulove pocty
        //        return 0;

        //    if (database == null)
        //        database = GetCommDatabaseProvider();

        //    if (database == null)
        //        return -1;

        //    System.Data.SqlClient.SqlTransaction tr = null;

        //    Log.WriteDataStoreCSV(loginid, qty, qtyreal, barcodeReaded);

        //    return database.EventsInsert(loginid, ref tr, "", qty, qtyreal, description, barcodeReaded, barcodeSended, zakazka, popis, reportType, IDO, scan1, scan2, scan3, sensor, material, Logging.LogConfig.MachineID.Trim());
        //}

        /// <summary>
        /// Ulozeni dat odvodu vyroby do databaze
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="qty"></param>
        /// <param name="qtyreal"></param>
        /// <param name="description"></param>
        /// <param name="barcodeReaded"></param>
        /// <param name="barcodeSended"></param>
        /// <param name="zakazka"></param>
        /// <param name="popis"></param>
        /// <param name="reportType"></param>
        /// <param name="IDO"></param>
        /// <param name="scan1"></param>
        /// <param name="scan2"></param>
        /// <param name="scan3"></param>
        /// <param name="sensor"></param>
        /// <param name="material"></param>
        /// <returns> TaD Zmena return typu z int na bool</returns>
        public static bool InsertNewEvents(
            string loginid, 
            decimal qty, 
            decimal qtyreal, 
            string description, 
            string barcodeReaded, 
            string barcodeSended, 
            string zakazka, 
            string popis, 
            string reportType, 
            string IDO, 
            string scan1, 
            string scan2, 
            string scan3, 
            string sensor, 
            string material,
            string VPH,
            int? VPPol,
            string EAN_IS,
            string IS_ID,
            string NMBRPAL,
            int? status,
            decimal QTYPACK,
            string PackType,
            decimal? WEIGHT,
            byte BarcodeT,
            string REZ_1,
            string REZ_2,
            string REZ_3,
            string REZ_4,
            string REZ_5
            )
        {
            if (!AgroConfig.config.Agro[0].PovolitNuloveOdvody)
            {
                if (qty == 0 && qtyreal == 0) //nebudeme ukladat nulove pocty
                    return true; 
            }

            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return false;

            System.Data.SqlClient.SqlTransaction tr = null;

            ExceptionHandler2.HandleCSV(loginid, qty, qtyreal, barcodeReaded);
            ExceptionHandler2.Handle("VYKLADKA archivace --- OK", "Log_LV", "txt");

            bool succ = database.EventsInsert(
                loginid, 
                ref tr, 
                "", 
                qty, 
                qtyreal, 
                description, 
                barcodeReaded, 
                barcodeSended, 
                zakazka, 
                popis, 
                reportType, 
                IDO, 
                scan1, 
                scan2, 
                scan3, 
                sensor, 
                material, 
                Logging.LogConfig.MachineID.Trim(),
                VPH,
                VPPol,
                EAN_IS,
                IS_ID,
                NMBRPAL,
                status,
                QTYPACK,
                PackType,
                WEIGHT,
                BarcodeT,
                REZ_1,
                REZ_2,
                REZ_3,
                REZ_4,
                REZ_5
                );

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

        #region VPH a VPP

        public  static bool Exist_VPH(string SOPNUMBE)
        {
            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return false;
            
            bool succ = database.Exist_VPH(SOPNUMBE);

            return succ;
        }


        public static ICommDatabase.DSVyroba.CZPRO_VPPRow Get_VPP(string BarcodeP, string SOPNUMBE)
        {
            if (database == null)
                database = GetCommDatabaseProvider();

            if (database == null)
                return null;

            var succ = database.Get_VPP(BarcodeP, SOPNUMBE);

            return succ;
        }

        #endregion
    }
}
