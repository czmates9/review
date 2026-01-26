using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ICommDatabase;
using System.Threading;
using Fask.Logging;

namespace Database.Classes
{
    public class Vyroba_Local
    {
        #region Inicializace lokalneho providera

        private static ICommDatabase.ICommDatabase database = null;

        private static ICommDatabase.ICommDatabase GetCommDatabaseProvider(string localConnection)
        {
            try
            {
                ICommDatabase.ICommDatabase provider = null;
                string path = localConnection;
                try
                {
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

                }
                catch (Exception ex)
                {
                    throw new Exception("Nenalezen dll soubor" + ex.Message);
                }

                provider.LoadConfiguration();

                return provider;
            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }


        #endregion

        #region Fask_Logins

        public static int upload_FASK_Logins(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_Logins(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_FASK_Logins(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_FASK_Logins();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static bool LogUser(string userID, string userPassword, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.LogUser(userID, userPassword);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        #region Fask_Machines

        public static int upload_FASK_Machines(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_Machines(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_FASK_Machines(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_FASK_Machines();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static ICommDatabase.DSVyroba getMachines(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.GetMachines();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        #endregion

        #region CZPRO_VPP

        public static int upload_VPP(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_CZPRO_VPP(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_VPP(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_CZPRO_VPP();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        #endregion

        #region CZPRO_VPH

        public static int upload_VPH(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_CZPRO_VPH(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_VPH(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_CZPRO_VPH();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        #endregion

        #region FASK_UserEvents

        /// <summary>
        /// Vlozeni do tabulky FASK_UserEvents
        /// </summary>
        /// <param name="status"></param>
        /// <param name="loginID"></param>
        /// <param name="machineID"></param>
        /// <param name="localConnection"></param>
        /// <returns></returns>
        public static bool UserEventsInsert(StatusTypesEnum status, string loginID, string machineID, string localConnection)
        {
            if (database == null)
                database = GetCommDatabaseProvider(localConnection);

            if (database == null)
                return false;

            return database.UserEventsInsert(loginID, machineID, ((int)status).ToString(), localConnection, "", "");
        }

        public static ICommDatabase.DSVyroba getVyrobaUserEventsData(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.GetUserEvents();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static int DeleteUserEvent(int p)
        {
            try
            {
                if (database == null)
                    return -1;

                return database.DeleteUserEvent(p);
            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        #endregion

        #region FASK_UserEventsErr

        /// <summary>
        /// Vlozeni do tabulky FASK_UserEventsErr
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="loginID"></param>
        /// <param name="machineID"></param>
        /// <param name="localConnection"></param>
        /// <returns></returns>
        public static int UserEventsErrInsert(int id, string status, string loginID, string machineID, string localConnection)
        {
            if (database == null)
                database = GetCommDatabaseProvider(localConnection);

            if (database == null)
                return -1;

            return database.UserEventsErrInsert(id, loginID, machineID, status, "");
        }

        #endregion

        #region FASK_Events

        /// <summary>
        /// Vlozeni do tabulky FASK_Events
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="transaction"></param>
        /// <param name="connectionString"></param>
        /// <param name="login"></param>
        /// <param name="machine"></param>
        /// <param name="operationName"></param>
        /// <param name="operationBarcode"></param>
        /// <param name="operationFree"></param>
        /// <param name="operationCode"></param>
        /// <param name="scan1"></param>
        /// <param name="scan2"></param>
        /// <param name="scan3"></param>
        /// <param name="sensor"></param>
        /// <param name="orderNumber"></param>
        /// <param name="material"></param>
        /// <param name="sensortotal"></param>
        /// <param name="VPH"></param>
        /// <param name="VPPol"></param>
        /// <param name="EAN_IS"></param>
        /// <param name="IS_ID"></param>
        /// <param name="NMBRPAL"></param>
        /// <param name="status"></param>
        /// <param name="QTYPACK"></param>
        /// <param name="PackType"></param>
        /// <param name="WEIGHT"></param>
        /// <param name="BarcodeT"></param>
        /// <param name="REZ_1"></param>
        /// <param name="REZ_2"></param>
        /// <param name="REZ_3"></param>
        /// <param name="REZ_4"></param>
        /// <param name="REZ_5"></param>
        public static void EventsInsert(
            DateTime dt,
            ref SqlTransaction transaction,
            string connectionString,
            string login,
            string machine,
            string operationName,
            string operationBarcode,
            bool operationFree,
            string operationCode,
            string scan1,
            string scan2,
            string scan3,
            string sensor,
            string orderNumber,
            string material,
            string sensortotal,
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
            if (database == null)
                database = GetCommDatabaseProvider(connectionString);

            if (database == null)
                throw new Exception();

            database.EventsInsert(
                login,
                ref transaction,
                connectionString,
                0,
                0,
                operationName,
                operationBarcode,
                "",
                "",
                "",
                "",
                "",
                scan1,
                scan2,
                scan3,
                sensor,
                material,
                machine,
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
        }

        public static ICommDatabase.DSVyroba getVyrobaEventsData(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.GetEvents();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static int DeleteEvent(int rowID)
        {
            try
            {
                if (database == null)
                    return -1;

                return database.DeleteEvent(rowID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int upload_FASK_Events(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_Events(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        #endregion

        #region FASK_EventsErr

        /// <summary>
        /// Vlozeni do tabulky FASK_EventsErr
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="LocalConnectionString"></param>
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
        public static int EventsErrInsert(string loginid, string LocalConnectionString, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material)
        {
            if (database == null)
                database = GetCommDatabaseProvider(LocalConnectionString);

            if (database == null)
                throw new Exception();

            return database.EventsErrInsert(loginid, LocalConnectionString, qty, qtyreal, description, barcodeReaded, barcodeSended, zakazka, popis, reportType, IDO, scan1, scan2, scan3, sensor, material);
        }

        #endregion

        #region FASK_CurrentState

        /// <summary>
        /// Vlozeni do tabulky uchovaavjici aktualni stav aplikace
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="transaction"></param>
        /// <param name="connectionString"></param>
        /// <param name="machine"></param>
        /// <param name="operationFree"></param>
        /// <param name="operationBarcode"></param>
        /// <param name="scan1res"></param>
        /// <param name="scan2res"></param>
        /// <param name="scan3res"></param>
        /// <param name="sensorValue"></param>
        /// <param name="orderNumber"></param>
        /// <param name="material"></param>
        /// <param name="polozka"></param>
        public static void CurrentStateInsert(DateTime dt, ref SqlTransaction transaction, string connectionString, string machine, bool operationFree, string operationBarcode, string scan1res, string scan2res, string scan3res, string sensorValue, string orderNumber, string material, string polozka)
        {
            if (database == null)
                database = GetCommDatabaseProvider(connectionString);

            if (database == null)
                throw new Exception();

            database.CurrentStateInsert(dt, ref transaction, connectionString, machine, operationFree, operationBarcode, scan1res, scan2res, scan3res, sensorValue, orderNumber, material, polozka);

        }

        #endregion

        #region FASK_MachineType

        public static int upload_FASK_MachineType(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_MachineType(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_FASK_MachineType(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_FASK_MachineType();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }


        #endregion

        #region FASK_Operations_Next

        public static int upload_FASK_Operations_Next(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_Operations_Next(data);
            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_FASK_Operations_Next(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_FASK_Operations_Next();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }


        #endregion

        #region FASK_Operations

        public static int upload_FASK_Operations(object data, string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Update_FASK_Operations(data);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static int Delete_FASK_Operations(string connectionString)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.Delete_FASK_Operations();

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }


        #endregion

    }
}
