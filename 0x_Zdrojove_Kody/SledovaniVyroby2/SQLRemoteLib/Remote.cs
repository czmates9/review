using ICommDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using Fask.Logging;
using Fask.WEBAPI.API_BusinessObjects;

namespace SQLRemoteLib
{
    public class Remote : ICommDatabase.ISQLDatabase
    { 
        #region Parameters

        private string _configFileName = "SQLRemoteLib.xml";
        private string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;Persist Security Info=True;User ID=sa;Password=sasa";

        //public static string NadopConnectionString = "Data Source=C:\\Fask\\Vyroba.sdf";

        private const string TableName_FASK_Events = "FASK_Events";
        private const string TableName_FASK_CurrentState = "FASK_CurrentState";
        private const string TableName_FASK_UserEvents = "FASK_UserEvents";

        #endregion

        #region LoadConfig

        public void LoadConfiguration()
        {
            LoadConfiguration(_configFileName);
        }

        public void LoadConfiguration(string FileName)
        {
            //Zjisteni cesty ke konfiguracnimu souboru.
            string configFilePath = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), FileName))).LocalPath;

            //Vytvoreni xml dokumentu
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configFilePath);

            //connectionString = "Data Source=" + LoadElement(xmldoc, "/Database/Path");
            connectionString = LoadElement(xmldoc, "/Database/Path");

        }

        private static string LoadElement(XmlDocument XmlDoc, string NodeName)
        {
            string nodeValue = string.Empty;

            //Konkretni uzel
            XmlElement configNode = XmlDoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                //Vlozeni obsahu uzlu.
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }

        #endregion

        #region FASK_Events

        public DSVyroba.FASK_EventsDataTable GetFASK_Events_ByStatus(int MachineID, int Status, List<string> descFilter)
        {
            ICommDatabase.DSVyroba.FASK_EventsDataTable DT = new DSVyroba.FASK_EventsDataTable();
            //SqlCommand comm = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze
            //int result = 0;
            //string descriptionValue_1 = "automaticke ulozeni";
            //string descriptionValue_2 = "xxx";
            //string descriptionValue_3 = "xxx";
            try
            {
                using (conn = new SqlConnection(connectionString))
                {


                    using (var com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;

                        ////zkouska vyber korektni zaznamy status 0
                        //com.CommandText = "SELECT * FROM FASK_Events" +
                        //    " WHERE 1 = 1" +
                        //    " and status = '" + Status + "'"  + 
                        //    " and productionGuid is not null " +
                        //    " and machineid = '" + MachineID + "'";

                        //com.CommandText = "SELECT * FROM FASK_Events" +
                        //    " WHERE 1 = 1" +
                        //    " and productionGuid is not null " +
                        //    " and machineid = '" + MachineID + "'";

                        com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_Events +
                           " WHERE 1 = 1" +
                           " and productionGuid is not null ";

                        if (MachineID == -1)
                        {

                        }
                        else
                        {
                            com.CommandText += " and machineid = '" + MachineID + "'";
                        }


                        if (Status == 21 && MachineID != -1)
                        {
                            int Status2 = Status + 1;
                            com.CommandText +=
                            " and (status = '" + Status + "'" +
                            " OR status = '" + Status2 + "')";
                        }
                        else
                        {
                            com.CommandText += " and status = '" + Status + "'";
                        }


                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";

                        //"' and(" +
                        //" description like '" + descriptionValue_1 + "'" +
                        //" or description like '" + descriptionValue_2 +
                        //"' or description like '" + descriptionValue_3 +
                        //"' )" +
                        //"order by dateeve desc";


#if false
                        if (NMBRPAL_val == string.Empty || NMBRPAL_val == null)
                        {
                            com.CommandText = "SELECT TOP (1) * FROM FASK_Events WHERE (machineid = '" + MachineID + "' and status = '" + Status + "' and productionGuid is not NULL) order by dateeve desc";

                        }
                        else
                        {
                            com.CommandText = "SELECT TOP (1) * FROM FASK_Events WHERE (machineid = '" + MachineID + "' and status = '" + Status + "' and NMBRPAL = '" + NMBRPAL_val + "' and productionGuid is not NULL) order by dateeve desc";

                        } 
#endif


                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

#if false
                            if (DT.Count == 1)
                            {
                                return DT.First();
                            }
                            else
                                return null; 
#endif
                        }
                    }
                }

                var w = DT.Count;

                return DT;



            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                //ErrorLog.Log.Write(ex);
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        #endregion

        #region FASK_Logins

        public bool Fill_FASK_Logins( DSVyroba.FASK_LoginsDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                try
                {
                    using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {

                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_Logins;
                            conn.Open();

                            using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(dataTable);
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    //Log.WriteException(ex);
                    return false;
                }
                finally
                {
                    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        #region FASK_Machine

        public bool Fill_FASK_Machine(DSVyroba.FASK_MachinesDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                try
                {
                    using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {

                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_Machines;
                            conn.Open();

                            using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(dataTable);
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    //Log.WriteException(ex);
                    return false;
                }
                finally
                {
                    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        #region FASK_MachineType

        public bool Fill_FASK_MachineType(DSVyroba.FASK_MachineTypeDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                try
                {
                    using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {

                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_MachinesType;
                            conn.Open();

                            using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(dataTable);
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    //Log.WriteException(ex);
                    return false;
                }
                finally
                {
                    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

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

        public DSVyroba.CZPRO_VPPDataTable getVyroba_CZPRO_VPP(int MachineID)
        {
            SqlConnection conn = null;
            ICommDatabase.DSVyroba.CZPRO_VPPDataTable dt = new ICommDatabase.DSVyroba.CZPRO_VPPDataTable();
            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_CZPRO_VPP_View;

                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = comm;
                            int returnValue = adapter.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        #endregion

        #region CZPRO_VPH

        public DSVyroba.CZPRO_VPHDataTable getVyroba_CZPRO_VPH(int MachineID)
        {
            SqlConnection conn = null;
            ICommDatabase.DSVyroba.CZPRO_VPHDataTable dt = new ICommDatabase.DSVyroba.CZPRO_VPHDataTable();
            try
            {
                using (conn = new SqlConnection( connectionString))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_CZPRO_VPH_View;

                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = comm;
                            int returnValue = adapter.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        #endregion

        #region FASK_Events

        public int FASK_EventsInsert
            (
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

            System.Data.SqlClient.SqlConnection conn = null;

            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"INSERT INTO " + ICommDatabase.Constants.Common.TableName_FASK_Events + " (" +
                            " loginid, machineid, dateeve, qty, qtyReal, " +
                            " description, barcodeReaded, barcodeSended, zakazka, popis, " +
                            " faskGUID, reportType, IDO, scan1, scan2, " +
                            " scan3, sensor, material, VPH, VPPol, " +
                            " EAN_IS, IS_ID, NMBRPAL, status, QTYPACK, " +
                            " PackType, WEIGHT, " +
                            " BarcodeT, REZ_1, REZ_2, REZ_3, REZ_4, REZ_5" + 
                            " ) VALUES ( " +
                            " @loginid, @machineid, @dateeve, @qty, @qtyReal, " +
                            " @description, @barcodeReaded, @barcodeSended, @zakazka, @popis, " +
                            " @faskGUID, @reportType, @IDO, @scan1, @scan2, " +
                            " @scan3, @sensor, @material, @VPH, @VPPol, " +
                            " @EAN_IS, @IS_ID, @NMBRPAL, @status, @QTYPACK, " +
                            " @PackType, @WEIGHT, " +
                            " @BarcodeT, @REZ_1, @REZ_2, @REZ_3, @REZ_4, @REZ_5" +
                            " )";



                        cmd.Parameters.AddWithValue("@loginid", string.IsNullOrEmpty(loginid) ? string.Empty : (object)loginid.Trim());
                        cmd.Parameters.AddWithValue("@machineid", string.IsNullOrEmpty(machineid) ? string.Empty : (object)machineid.Trim());
                        cmd.Parameters.AddWithValue("@dateeve", dateeve);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@qtyReal", qtyReal);

                        cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description.Trim());
                        cmd.Parameters.AddWithValue("@barcodeReaded", string.IsNullOrEmpty(barcodeReaded) ? string.Empty : (object)barcodeReaded.Trim());
                        cmd.Parameters.AddWithValue("@barcodeSended", string.IsNullOrEmpty(barcodeSended) ? string.Empty : (object)barcodeSended.Trim());
                        cmd.Parameters.AddWithValue("@zakazka", string.IsNullOrEmpty(zakazka) ? DBNull.Value : (object)zakazka.Trim());
                        cmd.Parameters.AddWithValue("@popis", string.IsNullOrEmpty(popis) ? DBNull.Value : (object)popis.Trim());

                        cmd.Parameters.AddWithValue("@faskGUID", faskGUID);
                        cmd.Parameters.AddWithValue("@reportType", string.IsNullOrEmpty(reportType) ? string.Empty : (object)reportType.Trim());
                        cmd.Parameters.AddWithValue("@IDO", string.IsNullOrEmpty(IDO) ? string.Empty : (object)IDO.Trim());
                        cmd.Parameters.AddWithValue("@scan1", string.IsNullOrEmpty(scan1) ? DBNull.Value : (object)scan1.Trim());
                        cmd.Parameters.AddWithValue("@scan2", string.IsNullOrEmpty(scan2) ? DBNull.Value : (object)scan2.Trim());

                        cmd.Parameters.AddWithValue("@scan3", string.IsNullOrEmpty(scan3) ? DBNull.Value : (object)scan3.Trim());
                        cmd.Parameters.AddWithValue("@sensor", string.IsNullOrEmpty(sensor) ? string.Empty : (object)sensor.Trim());
                        cmd.Parameters.AddWithValue("@material", string.IsNullOrEmpty(material) ? DBNull.Value : (object)material.Trim());
                        cmd.Parameters.AddWithValue("@VPH", string.IsNullOrEmpty(VPH) ? DBNull.Value : (object)VPH.Trim());
                        cmd.Parameters.AddWithValue("@VPPol", VPPol.HasValue ? (object)VPPol.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@EAN_IS", string.IsNullOrEmpty(EAN_IS) ? DBNull.Value : (object)EAN_IS.Trim());
                        cmd.Parameters.AddWithValue("@IS_ID", string.IsNullOrEmpty(IS_ID) ? DBNull.Value : (object)IS_ID.Trim());
                        cmd.Parameters.AddWithValue("@NMBRPAL", string.IsNullOrEmpty(NMBRPAL) ? DBNull.Value : (object)NMBRPAL.Trim());
                        cmd.Parameters.AddWithValue("@status", status.HasValue ? (object)status.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@QTYPACK", QTYPACK);

                        cmd.Parameters.AddWithValue("@PackType", string.IsNullOrEmpty(PackType) ? DBNull.Value : (object)PackType.Trim());
                        cmd.Parameters.AddWithValue("@WEIGHT", WEIGHT.HasValue ? (object)WEIGHT.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@BarcodeT", BarcodeT);
                        cmd.Parameters.AddWithValue("@REZ_1", string.IsNullOrEmpty(REZ_1) ? DBNull.Value : (object)REZ_1.Trim());
                        cmd.Parameters.AddWithValue("@REZ_2", string.IsNullOrEmpty(REZ_2) ? DBNull.Value : (object)REZ_2.Trim());
                        cmd.Parameters.AddWithValue("@REZ_3", string.IsNullOrEmpty(REZ_3) ? DBNull.Value : (object)REZ_3.Trim());
                        cmd.Parameters.AddWithValue("@REZ_4", string.IsNullOrEmpty(REZ_4) ? DBNull.Value : (object)REZ_4.Trim());
                        cmd.Parameters.AddWithValue("@REZ_5", string.IsNullOrEmpty(REZ_5) ? DBNull.Value : (object)REZ_5.Trim());

                        conn.Open();

                        return cmd.ExecuteNonQuery() > 0 ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public int? FASK_Events_CountGUID( Guid Guid, int MachineID)
        {
            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT COUNT(*) FROM FASK_Events WHERE faskGUID = @Guid";

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@Guid", DbType = System.Data.DbType.Guid, Value = Guid });


                        conn.Open();

                        object returnValue = comm.ExecuteScalar();

                        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                        {
                            return null;
                        }
                        else
                        {
                            return (int)returnValue;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        #endregion

        #region FASK_UserEvents

        public int? FASK_UserEvents_CountGUID(Guid Guid, int MachineID)
        {
            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT COUNT(*) FROM " + ICommDatabase.Constants.Common.TableName_FASK_UserEvents + " WHERE faskGUID = @Guid";

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@Guid", DbType = System.Data.DbType.Guid, Value = Guid });


                        conn.Open();

                        object returnValue = comm.ExecuteScalar();

                        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                        {
                            return null;
                        }
                        else
                        {
                            return (int)returnValue;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public bool FASK_UserEventsInsert(
            string loginid,
            string machineid,
            System.DateTime dateeve,
            string statusid,
            System.Guid faskGUID,
            string rez_1,
            string rez_2
            )
        {

            System.Data.SqlClient.SqlConnection conn = null;

            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = " INSERT INTO " +
                                        ICommDatabase.Constants.Common.TableName_FASK_UserEvents +
                                        " (loginid, machineid, dateeve, statusid, faskGUID, rez_1, rez_2" +
                                        " ) VALUES( " +
                                        " @loginid, @machineid, @dateeve, @statusid, @faskGUID, @rez_1, @rez_2)";


                        cmd.Parameters.AddWithValue("@loginid", string.IsNullOrEmpty(loginid) ? string.Empty : (object)loginid.Trim());
                        cmd.Parameters.AddWithValue("@machineid", string.IsNullOrEmpty(machineid) ? DBNull.Value : (object)machineid.Trim());
                        cmd.Parameters.AddWithValue("@dateeve", dateeve);
                        cmd.Parameters.AddWithValue("@statusid", string.IsNullOrEmpty(statusid) ? DBNull.Value : (object)statusid.Trim());
                        cmd.Parameters.AddWithValue("@faskGUID", faskGUID);
                        cmd.Parameters.AddWithValue("@rez_1", string.IsNullOrEmpty(rez_1) ? DBNull.Value : (object)rez_1.Trim());
                        cmd.Parameters.AddWithValue("@rez_2", string.IsNullOrEmpty(rez_2) ? DBNull.Value : (object)rez_2.Trim());

                        conn.Open();

                        return cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        #endregion

        #region FASK_Operations

        public bool Fill_FASK_Operations( DSVyroba.FASK_OperationsDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                try
                {
                    using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {

                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_Operations;
                            conn.Open();

                            using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(dataTable);
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    //Log.WriteException(ex);
                    return false;
                }
                finally
                {
                    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

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

        public bool Fill_FASK_Operations_Next(DSVyroba.FASK_Operations_NextDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                try
                {
                    using (conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {

                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT * FROM " + ICommDatabase.Constants.Common.TableName_FASK_Operations_Next;
                            conn.Open();

                            using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(dataTable);
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    //Log.WriteException(ex);
                    return false;
                }
                finally
                {
                    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                string LOG = ex.Message.ToString();
                return false;
            }
        }


        #endregion

        #region Pomocne metody z AGRO modulu

        public  string ReturnSarze(int MachineID, string smenaID, string userID, string linkaID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(connectionString);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.CommandText = "fask_vyroba_GetSarze";
                sqlComm.Parameters.AddWithValue("@smenaID", smenaID);
                sqlComm.Parameters.AddWithValue("@userID", userID);
                sqlComm.Parameters.AddWithValue("@linkaID", linkaID);

                sqlComm.Connection.Open();

                object o = sqlComm.ExecuteScalar();
                if (o is string)
                {
                    return (string)o;
                }
                else
                {
                    return string.Empty;
                }
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }

        public  bool ReturnID(int MachineID, string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(connectionString);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;
                //sqlComm.CommandText = "SELECT id FROM Fask_Logins WHERE " + "id = " + inID;
                //sqlComm.CommandText = "IF (EXISTS (SELECT id FROM Fask_Logins WHERE " + "id = " + inID + ")) RETURN 1 ELSE RETURN 0" ;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverId(@id)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inID)
                        return true;
                    else
                        return false;
                }



            }
            catch (Exception ex)
            {
                //FASK.SledovaniVyroby.ErrorLog.Log.Write(ex.Message.ToString());
                ExceptionHandler2.Handle(ex);
                //sql prikaz sa nevykonal tak to hodi throw heslo nenalezeno nebo se nepripojilo k serveru
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

        public  bool ReturnHeslo(int MachineID, string inHESLO, string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(connectionString);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;
                //sqlComm.CommandText = "SELECT psswd FROM Fask_Logins WHERE " + "psswd = " + inHESLO;
                //sqlComm.CommandText = "IF (EXISTS (SELECT psswd FROM Fask_Logins WHERE " + "psswd = " + inHESLO + ")) RETURN 1 ELSE RETURN 0" ;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverHeslo(@id,@heslo)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@heslo", inHESLO));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inHESLO)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                //FASK.SledovaniVyroby.ErrorLog.Log.Write(ex.Message.ToString());
                ExceptionHandler2.Handle(ex);
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                //sql prikaz sa nevykonal tak to hodi throw heslo nenalezeno nebo se nepripojilo k serveru
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

        #endregion

        #region Not Implemented, only in API

        public FASK_Events_row SledovaniVyroby_data(int MachineID, Filtr_FASK_Events filtr_Events)
        {
            throw new NotImplementedException();
        }

        public void SV_Logs_insert(int MachineID, DSVyroba.FASK_EventsErrDataTable bo)
        {
            throw new NotImplementedException();
        }

        public bool SV_ZmenaStatusu(int MachineID, int status)
        {
            throw new NotImplementedException();
        }

        public bool FaskEvents_WriteToRow(int MachineID, FASK_Events_row o, int StatusNew, string desc)
        {
            throw new NotImplementedException();
        }

        public bool FaskEvents_Update(int MachineID, Guid? G, int StatusNew)
        {
            throw new NotImplementedException();
        }

        #region Archivace

  
        public int DeaktivaceFE(int MachineID, int Status, DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci, int BocediID)
        {
            throw new NotImplementedException();
        }

        public DSVyroba.FASK_Events_archivaceDataTable LoadFE(int MachineID,  Filtr_FASK_Events filtr_Events)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion
    }
}
