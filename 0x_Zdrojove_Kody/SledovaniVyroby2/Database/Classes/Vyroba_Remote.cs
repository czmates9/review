using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Fask.Logging;
using System.Reflection;
using ICommDatabase;
using ICommDatabase.Constants;

namespace Database.Classes
{
    public class Vyroba_Remote
    {


        #region Inicializace lokalneho providera

        private static ISQLDatabase database = null;

        private static ISQLDatabase GetCommDatabaseProvider(string RemoteConnection)
        {
            try
            {
                ISQLDatabase provider = null;
                string path = RemoteConnection;
                try
                {
                    Assembly ass = Assembly.LoadFrom(path);
                    Type[] types = ass.GetTypes();

                    foreach (Type t in types)
                    {
                        try
                        {
                            provider = (ISQLDatabase)ass.CreateInstance(t.FullName);
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

        #region SSCC
        //vrati zaznam s SSCC kodem
        public static DSVyroba.FASK_EventsRow GetSSCC_zaznam(string connectionString, int MachineID, int Status, int StatusNew, string TextSeparator, int PocetPruchodu, int TimeSleep)
        {
            //string par = "automaticke ulozeni paleta;presun na vozik;kbdfljbdsf";
            //int pocetPruchoduMax = 50;
            try
            {
                string par = TextSeparator;
                int pocetPruchoduMax = PocetPruchodu;
                Thread.Sleep(TimeSleep);
                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
                DSVyroba.FASK_EventsDataTable DT_out = new DSVyroba.FASK_EventsDataTable();  // vystup

                do
                {
                    pocetPruchodu++;
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;


                    DSVyroba.FASK_EventsDataTable DT_S0 = GetFASK_Events_ByStatus(connectionString, MachineID, Status, list);
                    DSVyroba.FASK_EventsDataTable DT_S1 = GetFASK_Events_ByStatus(connectionString, MachineID, StatusNew, list);


                    #region Ukazka

                    //DSVyroba.FASK_EventsDataTable DT_S0 = new DSVyroba.FASK_EventsDataTable(); // naplnit
                    //DSVyroba.FASK_EventsDataTable DT_S1 = new DSVyroba.FASK_EventsDataTable(); // naplnit


                    var c_0 = DT_S0.Count();
                    var c_1 = DT_S1.Count();

                    foreach (var item in DT_S0)
                    {

                        var dt_tmp = DT_S1.Where(x =>
                        x.barcodeReaded == item.barcodeReaded
                        && x.barcodeSended == item.barcodeSended
                        && x.machineid == item.machineid
                        && x.material == item.material
                        && x.NMBRPAL == item.NMBRPAL
                        && x.qty == item.qty
                        && x.qtyReal == item.qtyReal
                        ).ToList();

                        if (dt_tmp.Count == 0)
                        {
                            DT_out.ImportRow(item);
                        }
                    }


                } while (DT_out.Count == 0);


                if (DT_out.Count == 1)
                {
                    return DT_out.First();
                }
                else
                {
                    return DT_out.OrderByDescending(x => x.dateeve).Last();
                }
            }
            catch (Exception)
            {

                throw;
            }


            // return ;

            #endregion
        }

        private static DSVyroba.FASK_EventsDataTable GetFASK_Events_ByStatus(string connectionString, int MachineID, int Status, List<string> descFilter)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.GetFASK_Events_ByStatus( MachineID, Status, descFilter);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        #endregion

        #region FASK_Logins

        public static bool Fill_FASK_Logins(string connectionString, DSVyroba.FASK_LoginsDataTable dataTable,int MachineID, bool ClearBeforeFill = true)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.Fill_FASK_Logins(dataTable, MachineID , ClearBeforeFill);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        public static DSVyroba.FASK_LoginsDataTable Get_FASK_Logins(string connectionString, int MachineID)
        {

            DSVyroba.FASK_LoginsDataTable ds = new DSVyroba.FASK_LoginsDataTable();
            if (Fill_FASK_Logins(connectionString,  ds, MachineID))
                return ds;
            else
                return null;
        }

        #endregion

        #region Fask_Machines

        public static bool Fill_FASK_Machine(string connectionString, DSVyroba.FASK_MachinesDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {

            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.Fill_FASK_Machine(dataTable, MachineID, ClearBeforeFill);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }


        public static DSVyroba.FASK_MachinesDataTable Get_FASK_Machines(string connectionString, int MachineID)
        {
            DSVyroba.FASK_MachinesDataTable ds = new DSVyroba.FASK_MachinesDataTable();
            if (Fill_FASK_Machine(connectionString, ds, MachineID))
                return ds;
            else
                return null;
        }

        #endregion

        #region FASK_MachineTypes

        public static bool Fill_FASK_MachineType(string connectionString, DSVyroba.FASK_MachineTypeDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.Fill_FASK_MachineType( dataTable,  MachineID, ClearBeforeFill);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }           
        }


        #endregion

        #region CZPRO_VPP

        public static DSVyroba.CZPRO_VPPDataTable getVyroba_CZPRO_VPP(string sqlConnectionStringRemote, int MachineID)
        {

            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(sqlConnectionStringRemote);

                if (database == null)
                    return null;

                return database.getVyroba_CZPRO_VPP( MachineID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }


        #endregion

        #region CZPRO_VPH

        public static DSVyroba.CZPRO_VPHDataTable getVyroba_CZPRO_VPH(string sqlConnectionStringRemote, int MachineID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(sqlConnectionStringRemote);

                if (database == null)
                    return null;

                return database.getVyroba_CZPRO_VPH(MachineID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        #endregion

        #region FASK_Events

        public static int FASK_EventsInsert
            (
            string connectionString,
            string loginid,
            string machineid,
            System.DateTime dateeve,
            decimal qty,
            decimal qtyReal,
            string description,
            string barcodeReaded,
            string barcodeSended,
            string zakazka,
            string popis,
            System.Guid faskGUID,
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
            int? status,
            string NMBRPAL,
            global::System.Guid? productionGuid,
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

            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return 0;

                return database.FASK_EventsInsert(loginid,
                    machineid,
                    dateeve,
                    qty,
                    qtyReal,
                    description,
                    barcodeReaded,
                    barcodeSended,
                    zakazka,
                    popis,
                    faskGUID,
                    reportType,
                    IDO,
                    scan1,
                    scan2,
                    scan3,
                    sensor,
                    material,
                    VPH,
                    VPPol,
                    EAN_IS,
                    IS_ID,
                    status,
                    NMBRPAL,
                    productionGuid,
                    QTYPACK,
                    PackType,
                    WEIGHT,
                    BarcodeT,
                    REZ_1,
                    REZ_2,
                    REZ_3,
                    REZ_4,
                    REZ_5);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return 0;
            }
        }

        public static int? FASK_Events_CountGUID(string connectionString, Guid Guid, int MachineID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.FASK_Events_CountGUID(Guid, MachineID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static Fask.WEBAPI.API_BusinessObjects.FASK_Events_row SledovaniVyroby_data(string connectionString, int MachineID, Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.SledovaniVyroby_data(MachineID, filtr_Events);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static bool SV_ZmenaStatusu(string connectionString, int MachineID, int status)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.SV_ZmenaStatusu(MachineID, status);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        public static bool FaskEvents_WriteToRow(string connectionString, int MachineID, Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int StatusNew, string desc)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.FaskEvents_WriteToRow(MachineID, o, StatusNew, desc);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        public static bool FaskEvents_Update(string connectionString, int MachineID, Guid? G, int StatusNew)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.FaskEvents_Update(MachineID, G, StatusNew);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        #region Archivace

        public static int DeaktivaceFE(string connectionString, int MachineID, int Status, DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci, int BocediID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return -1;

                return database.DeaktivaceFE(MachineID, Status, _dataKArchivaci, BocediID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return -1;
            }
        }

        public static DSVyroba.FASK_Events_archivaceDataTable LoadFE(string connectionString, int MachineID, Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.LoadFE(MachineID, filtr_Events);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }


        #endregion

        #endregion

        #region FASK_UserEvents

        public static int? FASK_UserEvents_CountGUID(string connectionString, Guid Guid, int MachineID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.FASK_UserEvents_CountGUID(Guid,  MachineID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static bool FASK_UserEventsInsert(
            string connectionString,
            string loginid, 
            string machineid, 
            System.DateTime dateeve, 
            string statusid, 
            System.Guid faskGUID, 
            string rez_1, 
            string rez_2
            )
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.FASK_UserEventsInsert(loginid,machineid,dateeve,statusid,faskGUID,rez_1,rez_2);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }

        }

        #endregion

        #region FASK_Operations

        public static bool Fill_FASK_Operations(string connectionString, DSVyroba.FASK_OperationsDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.Fill_FASK_Operations(dataTable, MachineID, ClearBeforeFill);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }


        #endregion

        #region FASK_Operations_Next

        public static bool Fill_FASK_Operations_Next(string connectionString, DSVyroba.FASK_Operations_NextDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.Fill_FASK_Operations_Next(dataTable, MachineID, ClearBeforeFill);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }

        }


        #endregion

        #region MyRegion

        public static string ReturnSarze(string connectionString,int MachineID, string smenaID, string userID, string linkaID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return null;

                return database.ReturnSarze(MachineID, smenaID, userID, linkaID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return null;
            }
        }

        public static bool ReturnID(string connectionString, int MachineID, string inID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.ReturnID(MachineID, inID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        public static bool ReturnHeslo(string connectionString, int MachineID, string inHESLO, string inID)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return false;

                return database.ReturnHeslo(MachineID, inHESLO, inID);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }


        #endregion

        #region FASK_EventsErr

        public static void SV_Logs_insert(string connectionString,int MachineID, DSVyroba.FASK_EventsErrDataTable dt)
        {
            try
            {
                if (database == null)
                    database = GetCommDatabaseProvider(connectionString);

                if (database == null)
                    return;

                database.SV_Logs_insert(MachineID, dt);

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return ;
            }
        }

        #endregion

    }
}
