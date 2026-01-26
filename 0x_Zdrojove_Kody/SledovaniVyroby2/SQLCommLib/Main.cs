using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using Fask.Logging;

namespace SQLCommLib
{
    public class Main : ICommDatabase.ICommDatabase
    {

        #region Parameters

        private string _configFileName = "SQLCommLib.xml";
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

        #region FASK_EVENTS

        #region GetEvents NOT Implemented

        public ICommDatabase.DSVyroba GetEvents()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DeleteEvent NOT Implemented

        public int DeleteEvent(int rowID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region EventsInsert Implemented

        //public bool EventsInsert(string loginid, ref System.Data.SqlClient.SqlTransaction transaction, string connectionString, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material, string machineID)
        //{
        //    FASK_EventsTableAdapter eta = new FASK_EventsTableAdapter();

        //    try
        //    {
        //        //0.Zapocati transakce
        //        eta.Connection.ConnectionString = connectionString;
        //        eta.Connection.Open();
        //        transaction = eta.Connection.BeginTransaction();

        //        //1.Ziskani dataadapteru z tableadapteru
        //        SqlDataAdapter adapter = getDataAdapter(eta);

        //        //2. Nastaveni connection
        //        adapter.InsertCommand.Connection = transaction.Connection;

        //        //3. Nastaveni transakce
        //        adapter.InsertCommand.Transaction = transaction;

        //        //4. Pouziti
        //        eta.Insert(
        //            loginid,
        //            "",
        //            DateTime.Now,
        //            0,
        //            0,
        //            "",
        //            barcodeReaded,
        //            string.Empty,
        //            "",
        //            string.Empty,
        //            Guid.NewGuid(),
        //            "",
        //            null,
        //            "",
        //            scan1,
        //            scan2,
        //            scan3,
        //            "",
        //            material);

        //        return true;
        //    }
        //    //Pokud neco nevyjde, uzavru spojeni s db a posilam vyjimku dal
        //    catch (Exception ex)
        //    {
        //        if (eta.Connection.State == System.Data.ConnectionState.Open) eta.Connection.Close();
        //        throw ex;
        //    }
        //}

        public bool EventsInsert
            (string loginid,
            ref SqlTransaction transaction,
            string connectionString,
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
            string machineID,
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

            System.Data.SqlClient.SqlConnection conn = null;

            try
            {

                DateTime cas = DateTime.Now;
                Guid guid = System.Guid.NewGuid();

                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {


                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"INSERT INTO " + TableName_FASK_Events + " (" +
                            " loginid, " +
                            " dateeve, " +
                            " qty, " +
                            " qtyReal, " +
                            " description, " +
                            " barcodeReaded, " +
                            " barcodeSended, " +
                            " zakazka, " +
                            " popis, " +
                            " faskGUID, " +
                            " reportType, " +
                            " isProcessed, " +
                            " IDO, " +
                            " scan1, " +
                            " scan2, " +
                            " scan3, " +
                            " sensor, " +
                            " material, " +
                            " machineid" +
                            " VPH, " +
                            " VPPol, " +
                            " EAN_IS, " +
                            " IS_ID, " +
                            " NMBRPAL, " +
                            " status, " +
                            " QTYPACK, " +
                            " PackType, " +
                            " WEIGHT, " +
                            " BarcodeT, " +
                            " REZ_1, " +
                            " REZ_2, " +
                            " REZ_3, " +
                            " REZ_4, " +
                            " REZ_5 " +
                            " ) " +
                            " VALUES (" +
                            " @loginid, " +
                            " @dateeve, " +
                            " @qty, " +
                            " @qtyReal, " +
                            " @description, " +
                            " @barcodeReaded, " +
                            " @barcodeSended, " +
                            " @zakazka, " +
                            " @popis, " +
                            " @faskGUID, " +
                            " @reportType, " +
                            " @isProcessed, " +
                            " @IDO, " +
                            " @scan1, " +
                            " @scan2, " +
                            " @scan3, " +
                            " @sensor, " +
                            " @material, " +
                            " @machineid, " +
                            " @VPH, " +
                            " @VPPol, " +
                            " @EAN_IS, " +
                            " @IS_ID, " +
                            " @NMBRPAL, " +
                            " @status, " +
                            " @QTYPACK, " +
                            " @PackType, " +
                            " @WEIGHT, " +
                            " @BarcodeT, " +
                            " @REZ_1, " +
                            " @REZ_2, " +
                            " @REZ_3, " +
                            " @REZ_4, " +
                            " @REZ_5 " +
                            " )";

                        cmd.Parameters.AddWithValue("@loginid", loginid);
                        cmd.Parameters.AddWithValue("@machineid", machineID);
                        cmd.Parameters.AddWithValue("@dateeve", cas);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@qtyReal", qtyreal);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@barcodeReaded", barcodeReaded);
                        cmd.Parameters.AddWithValue("@barcodeSended", barcodeSended);
                        cmd.Parameters.AddWithValue("@zakazka", zakazka);
                        cmd.Parameters.AddWithValue("@popis", popis);
                        cmd.Parameters.AddWithValue("@faskGUID", guid);
                        cmd.Parameters.AddWithValue("@reportType", reportType);
                        cmd.Parameters.AddWithValue("@isProcessed", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IDO", IDO);
                        cmd.Parameters.AddWithValue("@scan1", scan1);
                        cmd.Parameters.AddWithValue("@scan2", scan2);
                        cmd.Parameters.AddWithValue("@scan3", scan3);
                        cmd.Parameters.AddWithValue("@sensor", sensor);
                        cmd.Parameters.AddWithValue("@material", material);

                        cmd.Parameters.AddWithValue("@VPH", VPH);
                        cmd.Parameters.AddWithValue("@VPPol", VPPol.HasValue ? VPPol.Value : -1);
                        cmd.Parameters.AddWithValue("@EAN_IS", EAN_IS);
                        cmd.Parameters.AddWithValue("@IS_ID", IS_ID);
                        cmd.Parameters.AddWithValue("@NMBRPAL", NMBRPAL);
                        cmd.Parameters.AddWithValue("@status", status.HasValue ? status.Value : 0);

                        cmd.Parameters.AddWithValue("@QTYPACK", QTYPACK);
                        cmd.Parameters.AddWithValue("@PackType", PackType);
                        cmd.Parameters.AddWithValue("@WEIGHT", WEIGHT == null ? DBNull.Value : (object)WEIGHT.Value);

                        cmd.Parameters.AddWithValue("@BarcodeT", BarcodeT);

                        cmd.Parameters.AddWithValue("@REZ_1", string.IsNullOrEmpty(REZ_1) ? (object)DBNull.Value : REZ_1);
                        cmd.Parameters.AddWithValue("@REZ_2", string.IsNullOrEmpty(REZ_2) ? (object)DBNull.Value : REZ_2);
                        cmd.Parameters.AddWithValue("@REZ_3", string.IsNullOrEmpty(REZ_3) ? (object)DBNull.Value : REZ_3);
                        cmd.Parameters.AddWithValue("@REZ_4", string.IsNullOrEmpty(REZ_4) ? (object)DBNull.Value : REZ_4);
                        cmd.Parameters.AddWithValue("@REZ_5", string.IsNullOrEmpty(REZ_5) ? (object)DBNull.Value : REZ_5);

                        conn.Open();

                        return cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                }

                //return eventsta.InsertQueryNew(loginid, DateTime.Now, qty, qtyreal, description, barcodeReaded, barcodeSended, zakazka, popis, System.Guid.NewGuid(), reportType, null, "") > 0 ? true : false;
                //return eventsta.Insert(loginid, cas, qty, qtyreal, description, barcodeReaded, barcodeSended, zakazka, popis, guid, reportType, null, IDO, scan1, scan2, scan3, sensor, material, string.Empty) > 0 ? true : false;
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

        #region EventsErrInsert NOT Implemented

        public int EventsErrInsert(string loginid, string LocalConnectionString, decimal qty, decimal qtyreal, string description, string barcodeReaded, string barcodeSended, string zakazka, string popis, string reportType, string IDO, string scan1, string scan2, string scan3, string sensor, string material)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Update_FASK_Events NOT Implemented

        public int Update_FASK_Events(object data)
        {
            //Vyuziva pouze Vrtacka, až bude potreba, doimplementovat
            throw new Exception();
        }

        #endregion

        #endregion

        #region FASK_UserEvents

        #region GetUserEvents NOT Implemented
        ICommDatabase.DSVyroba ICommDatabase.ICommDatabase.GetUserEvents()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DeleteUserEvent NOT Implemented

        public int DeleteUserEvent(int rowID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserEventsInsert Implemented

        //public bool UserEventsInsert(string loginid, string machineid, string statusID, string localConnection, string rez1, string rez2)
        //{
        //    try
        //    {
        //        FASK_UserEventsTableAdapter ueta = new FASK_UserEventsTableAdapter();
        //        //ueta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        ueta.Connection.ConnectionString = localConnection;
        //        ueta.Insert(
        //            //Configuration.Config.LoginID, 
        //            loginid,
        //            //Configuration.Config.config.Main[0].MachineID,
        //            machineid,
        //            DateTime.Now,
        //            statusID,
        //            System.Guid.NewGuid()
        //            );
        //        //OK
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool UserEventsInsert(string loginid, string machineid, string statusID, string localConnection, string rez1, string rez2)
        {

            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;


                        cmd.CommandText = @"INSERT INTO [" + TableName_FASK_UserEvents + "]" +
                            " ( " +
                            " [loginid], [machineid], [dateeve], [statusid], [faskGUID], [rez_1], [rez_2]" +
                            " )" +
                            " VALUES " +
                            " ( " +
                            " @loginid, @machineid, @dateeve, @statusid, @faskGUID, @rez_1, @rez_2" +
                            " ) ";

                        cmd.Parameters.AddWithValue("@loginid", loginid);
                        cmd.Parameters.AddWithValue("@machineid", machineid);
                        cmd.Parameters.AddWithValue("@dateeve", DateTime.Now);
                        cmd.Parameters.AddWithValue("@statusid", statusID);
                        cmd.Parameters.AddWithValue("@faskGUID", System.Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@statusid", statusID);
                        cmd.Parameters.AddWithValue("@rez_1", rez1);
                        cmd.Parameters.AddWithValue("@rez_2", rez2);

                        conn.Open();

                        return cmd.ExecuteNonQuery() > 1 ? true : false;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion

        #region UserEventsErrInsert NOT Implemented

        public int UserEventsErrInsert(int id, string loginid, string machineid, string statusID, string localConnection)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region LogUser NOT Implemented

        public bool LogUser(string name, string pass)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetMachines NOT Implemented

        public ICommDatabase.DSVyroba GetMachines()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CurrentStateInsert Implemented

        //public void CurrentStateInsert(DateTime dt, ref System.Data.SqlClient.SqlTransaction transaction, string connectionString, string machine, bool operationFree, string operationBarcode, string scan1res, string scan2res, string scan3res, string sensorValue, string orderNumber, string material, string polozka)
        //{
        //    FASK_CurrentStateTableAdapter csta = new FASK_CurrentStateTableAdapter();

        //    try
        //    {
        //        csta.Connection.ConnectionString = connectionString;

        //        //Pokud neni null, ukladame pomoci transakce - pripad, ze se uklada zaraz do tabulky FASK_Events i teto
        //        if (transaction != null)
        //        {
        //            //0.Spojeni
        //            csta.Connection.Open();

        //            //1.Ziskani dataadapteru z tableadapteru
        //            Type tableAdapterType = csta.GetType();
        //            SqlDataAdapter adapter = (SqlDataAdapter)tableAdapterType.GetProperty("Adapter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(csta, null);

        //            //2. Nastaveni connection
        //            adapter.InsertCommand.Connection = transaction.Connection;

        //            //3. Nastaveni transakce
        //            adapter.InsertCommand.Transaction = transaction;
        //        }

        //        //4. pouziti
        //        csta.Insert(
        //            machine,
        //            (operationFree ? "F" : "M"),
        //            dt,
        //            scan1res,
        //            scan2res,
        //            scan3res,
        //            sensorValue,
        //            operationBarcode,
        //            orderNumber,
        //            material,
        //            polozka);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (csta.Connection.State == System.Data.ConnectionState.Open) csta.Connection.Close();
        //        throw ex;
        //    }
        //}

        public void CurrentStateInsert(
            DateTime dt,
            ref System.Data.SqlClient.SqlTransaction transaction,
            string connectionString,
            string machine,
            bool operationFree,
            string operationBarcode,
            string scan1res,
            string scan2res,
            string scan3res,
            string sensorValue,
            string orderNumber,
            string material,
            string polozka)
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;


                        cmd.CommandText = @"INSERT INTO [" + TableName_FASK_CurrentState + "] " +
                            " ( " +
                            " [machinetype], [operationtype], [dateeve], [barcode], [scan1], [scan2], [scan3], [sensor], [zakazka], [material], [polozka] " +
                            " ) " + " VALUES ( " +
                            " @machinetype, @operationtype, @dateeve, @barcode, @scan1, @scan2, @scan3, @sensor, @zakazka, @material, @polozka" +
                            " ) ";

                        cmd.Parameters.AddWithValue("@machinetype", machine);
                        cmd.Parameters.AddWithValue("@operationtype", (operationFree ? "F" : "M"));
                        cmd.Parameters.AddWithValue("@dateeve", dt);
                        cmd.Parameters.AddWithValue("@barcode", scan1res);
                        cmd.Parameters.AddWithValue("@scan1", scan2res);
                        cmd.Parameters.AddWithValue("@scan2", scan3res);
                        cmd.Parameters.AddWithValue("@scan3", sensorValue);
                        cmd.Parameters.AddWithValue("@sensor", operationBarcode);
                        cmd.Parameters.AddWithValue("@zakazka", orderNumber);
                        cmd.Parameters.AddWithValue("@material", material);
                        cmd.Parameters.AddWithValue("@polozka", polozka);

                        conn.Open();

                        if (transaction != null)
                        {
                            cmd.Transaction = transaction;
                        }

                        cmd.ExecuteNonQuery();
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

        #region CZPRO_VPH

        #region Update_CZPRO_VPH NOT Implemented
        public int Update_CZPRO_VPH(object data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete_CZPRO_VPH NOT implemented

        public int Delete_CZPRO_VPH()
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion

        #region CZPRO_VPP 

        #region Update_CZPRO_VPP NOT Impelemented

        public int Update_CZPRO_VPP(object data)
        {
            throw new NotImplementedException();
        } 

        #endregion

        #region Delete_CZPRO_VPP NOT implemented

        public int Delete_CZPRO_VPP()
        {
            throw new NotImplementedException(); 
        }

        #endregion

        #region Exist_VPH NOT Implemented

        public bool Exist_VPH(string SOPNUMBE)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get_VPP NOT Implemented

        public ICommDatabase.DSVyroba.CZPRO_VPPRow Get_VPP(string BarcodeP, string SOPNUMBE)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion

        #region FASK_Logins

        #region Update_FASK_Logins NOT Implemented
        public int Update_FASK_Logins(object data)
        {
            SqlTransaction transaction = null;
            SqlConnection conn = null;


            try
            {
                int result = 0;

                using (conn = new SqlConnection(connectionString))
                {
                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    //using (var commandUpdate = this.Connection.CreateCommand())
                    using (var commandDelete = conn.CreateCommand())
                    using (var commandSelect = conn.CreateCommand())
                    {
                        InitializeCommandInsert(commandInsert);
                        //InitializeCommandUpdate_CZMST_Servis_ZdrojSeznam(commandUpdate);
                        InitializeCommandDelete(commandDelete);
                        InitializeCommandSelect(commandSelect);

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            //adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as System.Data.DataSet;
                            var dataIsDataTable = data as System.Data.DataTable;
                            var dataIsDataRow = data as System.Data.DataRow;
                            var dataIsDataRowArray = data as System.Data.DataRow[];

                            //if (data is System.Data.DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
                            else if (dataIsDataRowArray != null)
                                result = adapter.Update(dataIsDataRowArray);
                            else
                                throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
                        }
                    }

                    transaction.Commit();


                }
                return result;
            }
            catch (Exception ex)
            {
                //Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    //Logging.ExceptionHandler2.Handle(exTransaction);
                    ExceptionHandler2.Handle(exTransaction);
                    throw ex;
                }

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

        #region Inicialize metody

        public static void InitializeCommandInsert(System.Data.SqlClient.SqlCommand command)
        {
            command.CommandText = "INSERT INTO " + ICommDatabase.Constants.Common.TableName_FASK_Logins + " ( " +
                " USERID, firstname, surname, psswd, CREATED, VALIDFROM, VALIDTO, RFID " +
                " ) VALUES ( " +
                " @USERID, @firstname, @surname, @psswd, @CREATED, @VALIDFROM, @VALIDTO, @RFID " +
                " ) ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@firstname", DbType = System.Data.DbType.String, SourceColumn = "firstname" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@surname", DbType = System.Data.DbType.String, SourceColumn = "surname" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@psswd", DbType = System.Data.DbType.String, SourceColumn = "psswd" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CREATED", DbType = System.Data.DbType.DateTime, SourceColumn = "CREATED" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VALIDFROM", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDFROM" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VALIDTO", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDTO" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RFID", DbType = System.Data.DbType.String, SourceColumn = "RFID" });

        }

        public static void InitializeCommandUpdate(System.Data.SqlClient.SqlCommand command)
        {
            command.CommandText = "UPDATE " + ICommDatabase.Constants.Common.TableName_FASK_Logins + " SET " +
                " [USERID] = @USERID," +
                " [firstname] = @firstname," +
                " [surname] = @surname," +
                " [psswd] = @psswd," +
                " [CREATED] = @CREATED" +
                " [VALIDFROM] = @VALIDFROM" +
                " [VALIDTO] = @VALIDTO" +
                " [RFID] = @RFID" +
                " WHERE [USERID] = @USERID_O ";


            command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@firstname", DbType = System.Data.DbType.String, SourceColumn = "firstname" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@surname", DbType = System.Data.DbType.String, SourceColumn = "surname" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@psswd", DbType = System.Data.DbType.String, SourceColumn = "psswd" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CREATED", DbType = System.Data.DbType.DateTime, SourceColumn = "CREATED" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VALIDFROM", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDFROM" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VALIDTO", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDTO" });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RFID", DbType = System.Data.DbType.String, SourceColumn = "RFID" });


            command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID_O", DbType = System.Data.DbType.Int32, SourceColumn = "USERID", SourceVersion = System.Data.DataRowVersion.Original });

        }

        public static void InitializeCommandDelete(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + ICommDatabase.Constants.Common.TableName_FASK_Logins + " WHERE USERID = @USERID";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@USERID",
                DbType = System.Data.DbType.String,
                SourceColumn = "USERID",
                SourceVersion = System.Data.DataRowVersion.Original
            });
        }

        public static void InitializeCommandSelect(SqlCommand command)
        {
            command.CommandText = "Select * from " + ICommDatabase.Constants.Common.TableName_FASK_Logins;
        }

        #endregion


        #endregion

        #region Delete_FASK_Logins NOT Implemented
        public int Delete_FASK_Logins()
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"Delete from FASK_Logins";
                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion

        #region FASK_Machines

        #region Update_FASK_Machines NOT Implemented
        public int Update_FASK_Machines(object data)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete_FASK_Machines Implemented

        public int Delete_FASK_Machines()
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"Delete from FASK_Machines";
                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion

        #region FASK_MachineType

        #region Update_FASK_MachineType !!! TODO !!!
        public int Update_FASK_MachineType(object data)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region Delete_FASK_MachineType Implemented

        public int Delete_FASK_MachineType()
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"Delete from FASK_MachineType";
                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion

        #region FASK_Operations_Next

        #region Update_FASK_Operations_Next !!! TODO !!!

        public int Update_FASK_Operations_Next(object data)
        {
            throw new NotImplementedException();
        } 

        #endregion

        #region Delete_FASK_Operations_Next Implemented
        public int Delete_FASK_Operations_Next()
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"Delete from FASK_Operations_Next";
                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion

        #region FASK_Operations

        #region Update_FASK_Operations !!! TODO !!!
        public int Update_FASK_Operations(object data)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region Delete_FASK_Operations Implemented
        public int Delete_FASK_Operations()
        {
            System.Data.SqlClient.SqlConnection conn = null;

            try
            {
                using (conn = new System.Data.SqlClient.SqlConnection(this.connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"Delete from FASK_Operations";
                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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

        #endregion
    }
}
