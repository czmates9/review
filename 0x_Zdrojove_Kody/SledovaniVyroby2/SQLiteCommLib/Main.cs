using ICommDatabase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using Fask.Logging;
using SQLiteCommLib.Classes;
using System.IO;

namespace SQLiteCommLib
{
    public class Main : ICommDatabase.ICommDatabase
    {
        #region Parameters

        private string _configFileName = "SQLiteCommLib.xml";
        private string _pathTo_PRD_File = "C:\\Fask\\Vyroba.prd";

        private const string TableName_FASK_Events = "FASK_Events";
        private const string TableName_FASK_UserEvents = "FASK_UserEvents";
        //private const string TableName_FASK_UserEventsErr = "FASK_UserEventsErr";
        //private const string TableName_FASK_EventsErr = "FASK_EventsErr";
        private const string TableName_FASK_Machines = "FASK_Machines";
        private const string TableName_FASK_Logins = "FASK_Logins";
        private const string TableName_CZPRO_VPH = "CZPRO_VPH";
        private const string TableName_CZPRO_VPP = "CZPRO_VPP";


        #endregion

        #region LoadConfig

        public void LoadConfiguration()
        {
            LoadConfiguration(_configFileName);
        }

        public void LoadConfiguration(string FileName)
        {
            var codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var codebasepath = System.IO.Path.GetDirectoryName(codebase);
            //Zjisteni cesty ke konfiguracnimu souboru.
            string configFilePath = (new Uri(System.IO.Path.Combine(codebasepath, FileName))).LocalPath;

            //Vytvoreni xml dokumentu
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configFilePath);

            var databasepath = LoadElement(xmldoc, "/Database/Path");
            //connectionString = "Data Source=" + (new Uri(System.IO.Path.Combine(codebasepath, databasepath))).LocalPath;
            _pathTo_PRD_File = (new Uri(System.IO.Path.Combine(codebasepath, databasepath))).LocalPath;

            //Kontrola zda soubor PRD existuje
            //Pokud ne tak vytvořit nový pomoci SQL Scriptu
            // Pokud script neexistuje tak stahnout z serveru pomoci API akualný

            SQLite_Helper helper = new SQLite_Helper();
            if (!File.Exists(_pathTo_PRD_File))
            {
                if (!helper.SQLite_CreateFile(_pathTo_PRD_File))
                {
                    throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro SSCC");
                }
            }

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

        #region GetEvents Implemented

        public DSVyroba GetEvents()
        {

            DSVyroba ds = new DSVyroba();
            //string CommandText = "SELECT " +
            //                    " id, " +
            //                    " loginid, " +
            //                    " machineid, " +
            //                    " dateeve, " +
            //                    " qty, " +
            //                    " qtyReal, " +
            //                    " description, " +
            //                    " barcodeReaded, " +
            //                    " barcodeSended, " +
            //                    " zakazka, " +
            //                    " popis, " +
            //                    " faskGUID, " +
            //                    " reportType, " +
            //                    " isProcessed, " +
            //                    " IDO, " +
            //                    " scan1, " +
            //                    " scan2," +
            //                    " scan3, " +
            //                    " sensor, " +
            //                    " material, " +
            //                    " VPH, " +
            //                    " VPPol, " +
            //                    " EAN_IS, " +
            //                    " IS_ID, " +
            //                    " NMBRPAL, " +
            //                    " status, " +
            //                    " productionGuid, " +
            //                    " QTYPACK, " +
            //                    " PackType, " +
            //                    " WEIGHT " +
            //                    " FROM " +
            //                    TableName_FASK_Events;

            string CommandText = "SELECT * FROM " + TableName_FASK_Events;


            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                con.Fill_Universal(ds, CommandText, ds.FASK_Events.TableName);
            }

            return ds;
        }

        #endregion

        #region DeleteEvent Implemented

        public int DeleteEvent(int rowID)
        {
            string CommandText = "DELETE FROM [" + TableName_FASK_Events + "] WHERE [id] = " + rowID.ToString();
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal(CommandText);
            }

            return ret;
        }

        #endregion

        #region EventsInsert Implemented

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


                DateTime cas = DateTime.Now;
                Guid guid = System.Guid.NewGuid();

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                
                var res = con.Fask_EventsInsert(
                    loginid,
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
                    machineID,
                    VPH,
                    VPPol,
                    EAN_IS,
                    IS_ID,
                    NMBRPAL,
                    status,
                    QTYPACK,
                    PackType,
                    WEIGHT,
                    cas,
                    guid,
                    cas,
                    BarcodeT,
                    REZ_1,
                    REZ_2,
                    REZ_3,
                    REZ_4,
                    REZ_5
                    );

                return res > 0 ? true : false;
            }


            //System.Data.SqlServerCe.SqlCeConnection conn = null;

            //try
            //{

            //    DateTime cas = DateTime.Now;
            //    Guid guid = System.Guid.NewGuid();

            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {
            //        using (var cmd = conn.CreateCommand())
            //        {

            //            cmd.Connection = conn;
            //            cmd.CommandType = CommandType.Text;
            //            cmd.CommandText = @"INSERT INTO " + TableName_FASK_Events + " (" +
            //                " loginid, " +
            //                " dateeve, " +
            //                " qty, " +
            //                " qtyReal, " +
            //                " description, " +
            //                " barcodeReaded, " +
            //                " barcodeSended, " +
            //                " zakazka, " +
            //                " popis, " +
            //                " faskGUID, " +
            //                " reportType, " +
            //                " isProcessed, " +
            //                " IDO, " +
            //                " scan1, " +
            //                " scan2, " +
            //                " scan3, " +
            //                " sensor, " +
            //                " material, " +
            //                " machineid, " +
            //                " VPH, " +
            //                " VPPol, " +
            //                " EAN_IS, " +
            //                " IS_ID, " +
            //                " NMBRPAL, " +
            //                " status, " +
            //                " QTYPACK, " +
            //                " PackType, " +
            //                " WEIGHT " +
            //                " ) " +
            //                " VALUES (" +
            //                " @loginid, " +
            //                " @dateeve, " +
            //                " @qty, " +
            //                " @qtyReal, " +
            //                " @description, " +
            //                " @barcodeReaded, " +
            //                " @barcodeSended, " +
            //                " @zakazka, " +
            //                " @popis, " +
            //                " @faskGUID, " +
            //                " @reportType, " +
            //                " @isProcessed, " +
            //                " @IDO, " +
            //                " @scan1, " +
            //                " @scan2, " +
            //                " @scan3, " +
            //                " @sensor, " +
            //                " @material, " +
            //                " @machineid, " +
            //                " @VPH, " +
            //                " @VPPol, " +
            //                " @EAN_IS, " +
            //                " @IS_ID, " +
            //                " @NMBRPAL, " +
            //                " @status, " +
            //                " @QTYPACK, " +
            //                " @PackType, " +
            //                " @WEIGHT " +
            //                " )";

            //            cmd.Parameters.AddWithValue("@loginid", loginid);
            //            cmd.Parameters.AddWithValue("@machineid", machineID);
            //            cmd.Parameters.AddWithValue("@dateeve", cas);
            //            cmd.Parameters.AddWithValue("@qty", qty);
            //            cmd.Parameters.AddWithValue("@qtyReal", qtyreal);
            //            cmd.Parameters.AddWithValue("@description", description);
            //            cmd.Parameters.AddWithValue("@barcodeReaded", barcodeReaded);
            //            cmd.Parameters.AddWithValue("@barcodeSended", barcodeSended);
            //            cmd.Parameters.AddWithValue("@zakazka", zakazka);
            //            cmd.Parameters.AddWithValue("@popis", popis);
            //            cmd.Parameters.AddWithValue("@faskGUID", guid);
            //            cmd.Parameters.AddWithValue("@reportType", reportType);
            //            cmd.Parameters.AddWithValue("@isProcessed", DateTime.Now);
            //            cmd.Parameters.AddWithValue("@IDO", IDO);
            //            cmd.Parameters.AddWithValue("@scan1", scan1);
            //            cmd.Parameters.AddWithValue("@scan2", scan2);
            //            cmd.Parameters.AddWithValue("@scan3", scan3);
            //            cmd.Parameters.AddWithValue("@sensor", sensor);
            //            cmd.Parameters.AddWithValue("@material", material);

            //            cmd.Parameters.AddWithValue("@VPH", VPH);
            //            cmd.Parameters.AddWithValue("@VPPol", VPPol.HasValue ? VPPol.Value : -1);
            //            cmd.Parameters.AddWithValue("@EAN_IS", EAN_IS);
            //            cmd.Parameters.AddWithValue("@IS_ID", IS_ID);
            //            cmd.Parameters.AddWithValue("@NMBRPAL", NMBRPAL);
            //            cmd.Parameters.AddWithValue("@status", status.HasValue ? (object)status.Value : DBNull.Value);

            //            cmd.Parameters.AddWithValue("@QTYPACK", QTYPACK);
            //            cmd.Parameters.AddWithValue("@PackType", PackType == null ? DBNull.Value : (object)PackType);
            //            cmd.Parameters.AddWithValue("@WEIGHT", WEIGHT == null ? DBNull.Value : (object)WEIGHT.Value);


            //            conn.Open();

            //            return cmd.ExecuteNonQuery() > 0 ? true : false;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //Log.Write("EventsInsert:");
            //    ExceptionHandler2.Handle(ex);
            //    return false;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}
        }

        #endregion

        #region EventsErrInsert Implemented

        public int EventsErrInsert(
            string loginid, 
            string LocalConnectionString, 
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
            string material)
        {

            DateTime cas = DateTime.Now;
            Guid guid = System.Guid.NewGuid();

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
       
                return con.Fask_EventsErrInsert(
                    loginid,
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
                    "agroVyroba",
                    cas,
                    null,
                    guid
                    );

            }

            //System.Data.SqlServerCe.SqlCeConnection conn = null;

            //try
            //{

            //    DateTime cas = DateTime.Now;
            //    Guid guid = System.Guid.NewGuid();

            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {
            //        using (var cmd = conn.CreateCommand())
            //        {

            //            cmd.Connection = conn;
            //            cmd.CommandType = CommandType.Text;

            //            cmd.CommandText = @" INSERT INTO [" + TableName_FASK_EventsErr + "] " +
            //                " ( " +
            //                " [loginid], " +
            //                " [machineid], " +
            //                " [dateeve], " +
            //                " [qty], " +
            //                " [qtyReal], " +
            //                " [description], " +
            //                " [barcodeReaded], " +
            //                " [barcodeSended], " +
            //                " [zakazka], " +
            //                " [popis], " +
            //                " [faskGUID], " +
            //                " [reportType], " +
            //                " [isProcessed], " +
            //                " [IDO], " +
            //                " [scan1], " +
            //                " [scan2], " +
            //                " [scan3], " +
            //                " [sensor] " +
            //                " ) VALUES (" +
            //                " @loginid, " +
            //                " @machineid, " +
            //                " @dateeve, " +
            //                " @qty, " +
            //                " @qtyReal, " +
            //                " @description, " +
            //                " @barcodeReaded, " +
            //                " @barcodeSended, " +
            //                " @zakazka, " +
            //                " @popis, " +
            //                " @faskGUID, " +
            //                " @reportType, " +
            //                " @isProcessed, " +
            //                " @IDO, " +
            //                " @scan1, " +
            //                " @scan2, " +
            //                " @scan3, " +
            //                " @sensor " +
            //                " )";

            //            cmd.Parameters.AddWithValue("@loginid", loginid);
            //            cmd.Parameters.AddWithValue("@machineid", "agroVyroba");
            //            cmd.Parameters.AddWithValue("@dateeve", cas);
            //            cmd.Parameters.AddWithValue("@qty", qty);
            //            cmd.Parameters.AddWithValue("@qtyReal", qtyreal);
            //            cmd.Parameters.AddWithValue("@description", description);
            //            cmd.Parameters.AddWithValue("@barcodeReaded", barcodeReaded);
            //            cmd.Parameters.AddWithValue("@barcodeSended", barcodeSended);
            //            cmd.Parameters.AddWithValue("@zakazka", zakazka);
            //            cmd.Parameters.AddWithValue("@popis", popis);
            //            cmd.Parameters.AddWithValue("@faskGUID", guid);
            //            cmd.Parameters.AddWithValue("@reportType", reportType);
            //            cmd.Parameters.AddWithValue("@isProcessed", DBNull.Value);
            //            cmd.Parameters.AddWithValue("@IDO", IDO);
            //            cmd.Parameters.AddWithValue("@scan1", scan1);
            //            cmd.Parameters.AddWithValue("@scan2", scan2);
            //            cmd.Parameters.AddWithValue("@scan3", scan3);
            //            cmd.Parameters.AddWithValue("@sensor", sensor);

            //            conn.Open();

            //            return cmd.ExecuteNonQuery();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //Log.Write("EventsInsert:");
            //    ExceptionHandler2.Handle(ex);
            //    return -1;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}
        }


        #endregion

        #region Update_FASK_Events

        public int Update_FASK_Events(object data)
        {
            //Vyuziva pouze Vrtacka, až bude potreba, doimplementovat
            throw new Exception();
        }

        #endregion

        #endregion

        #region FASK_UserEvents

        #region GetUserEvents Implemented

        public DSVyroba GetUserEvents()
        {
            
            DSVyroba ds = new DSVyroba();
            string CommandText = "SELECT " +
                  " id, loginid, machineid, dateeve, statusid, faskGUID, rez_1, rez_2 " +
                  " FROM " +
                  TableName_FASK_UserEvents;


            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                con.Fill_Universal(ds, CommandText, ds.FASK_UserEvents.TableName);
            }

    
            return ds;
        }


        #endregion

        #region DeleteUserEvent NOT Implemented

        public int DeleteUserEvent(int rowID)
        {
            string CommandText = "DELETE FROM [" + TableName_FASK_UserEvents + "] WHERE [id] = " + rowID.ToString();
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal(CommandText);
            }

            return ret;
        }


        #endregion

        #region  UserEventsInsert Implemented

        public bool UserEventsInsert(
            string loginid, 
            string machineid, 
            string statusID, 
            string localConnection, 
            string rez1, 
            string rez2)
        {


            DateTime cas = DateTime.Now;
            Guid guid = System.Guid.NewGuid();

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {

                var res =  con.Fask_UserEventsInsert(
                    loginid,
                    machineid,
                    statusID,
                    rez1,
                    rez2,
                    cas,
                    guid
                    );

                return res > 1 ? true : false;
            }


            //System.Data.SqlServerCe.SqlCeConnection conn = null;

            //try
            //{
            //    //Log.Write(this.connectionString);
            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {
            //        using (var cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandType = System.Data.CommandType.Text;


            //            cmd.CommandText = @"INSERT INTO [" + TableName_FASK_UserEvents + "]" +
            //                " ( " +
            //                " [loginid], [machineid], [dateeve], [statusid], [faskGUID], [rez_1], [rez_2]" +
            //                " )" +
            //                " VALUES " +
            //                " ( " +
            //                " @loginid, @machineid, @dateeve, @statusid, @faskGUID, @rez_1, @rez_2" +
            //                " ) ";

            //            cmd.Parameters.AddWithValue("@loginid", loginid);
            //            cmd.Parameters.AddWithValue("@machineid", machineid);
            //            cmd.Parameters.AddWithValue("@dateeve", DateTime.Now);
            //            cmd.Parameters.AddWithValue("@statusid", statusID);
            //            cmd.Parameters.AddWithValue("@faskGUID", System.Guid.NewGuid());
            //            //cmd.Parameters.AddWithValue("@statusid", statusID);
            //            cmd.Parameters.AddWithValue("@rez_1", rez1);
            //            cmd.Parameters.AddWithValue("@rez_2", rez2);

            //            conn.Open();

            //            return cmd.ExecuteNonQuery() > 1 ? true : false;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandler2.Handle(ex);
            //    return false;
            //}
            //finally
            //{
            //    try
            //    {
            //        if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //        {
            //            conn.Close();
            //            conn.Dispose();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ExceptionHandler2.Handle(ex);
            //    }
            //}
        }
        #endregion

        #region UserEventsErrInsert Implemented

        public int UserEventsErrInsert(
            int id, 
            string loginid, 
            string machineid, 
            string statusID, 
            string localConnection)
        {

            DateTime cas = DateTime.Now;
            Guid guid = System.Guid.NewGuid();

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {

                return con.Fask_UserEventsErrInsert(
                    id,
                    loginid,
                    machineid,
                    statusID,
                    cas,
                    guid
                    );

            }

            //System.Data.SqlServerCe.SqlCeConnection conn = null;

            //try
            //{
            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {
            //        using (var cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandType = System.Data.CommandType.Text;

            //            cmd.CommandText = @"INSERT INTO [" + TableName_FASK_UserEvents + "]" +
            //                " ( " +
            //                "  [loginid], [machineid], [dateeve], [statusid], [faskGUID]" +
            //                " )" +
            //                " VALUES " +
            //                " ( " +
            //                "  @loginid, @machineid, @dateeve, @statusid, @faskGUID " +
            //                " ) ";

            //            //cmd.Parameters.AddWithValue("@id", id);
            //            cmd.Parameters.AddWithValue("@loginid", loginid);
            //            cmd.Parameters.AddWithValue("@machineid", machineid);
            //            cmd.Parameters.AddWithValue("@dateeve", DateTime.Now);
            //            cmd.Parameters.AddWithValue("@statusid", statusID);
            //            cmd.Parameters.AddWithValue("@faskGUID", System.Guid.NewGuid());

            //            conn.Open();

            //            return cmd.ExecuteNonQuery();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandler2.Handle(ex);
            //    return -1;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}
        }


        #endregion

        #endregion

        #region LogUser Implemented

        public bool LogUser(string name, string pass)
        {

            DSVyroba ds = new DSVyroba();
            string CommandText = "SELECT COUNT(USERID) AS Expr1 FROM " + TableName_FASK_Logins + 
                " WHERE (USERID = '" + name.Trim() + "') " + 
                " AND (psswd = '" + pass.Trim() + "')";
            object returnValue;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                returnValue = con.Fill_Scalar_Universal( CommandText);
            }

            if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
            {
                return false;
            }
            else
            {
                Int64 pp = (Int64)returnValue;
                return pp > 0 ? true : false; ;
            }
        }

        #endregion

        #region GetMachines Implemented

        public DSVyroba GetMachines()
        {

            DSVyroba ds = new DSVyroba();
            string CommandText = "SELECT [id], [machinetype], [name], [description], [koeficient] FROM [" + TableName_FASK_Machines + "]";

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                con.Fill_Universal(ds, CommandText, ds.FASK_Machines.TableName);
            }

            return ds;
        }

        #endregion

        #region CurrentStateInsert NOT Implemented

        public void CurrentStateInsert(
            DateTime dt, 
            ref SqlTransaction transaction, 
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
            throw new NotImplementedException();
        }



        #endregion

        #region CZPRO_VPH 

        #region Update_CZPRO_VPH Implemented

        public int Update_CZPRO_VPH(object data)
        {

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                return con.Update_CZPRO_VPH(data, true);
            }


            //System.Data.SqlServerCe.SqlCeTransaction transaction = null;
            //System.Data.SqlServerCe.SqlCeConnection conn = null;
            //int result = 0;
            //try
            //{
            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {

            //        conn.Open();

            //        transaction = conn.BeginTransaction();

            //        using (var commandInsert = conn.CreateCommand())
            //        using (var commandSelect = conn.CreateCommand())
            //        {
            //            InitializeCommandInsert_CZPRO_VPH(commandInsert);
            //            InitializeCommandSelect_CZPRO_VPH(commandSelect);

            //            using (var adapter = new SqlCeDataAdapter())
            //            {
            //                adapter.InsertCommand = commandInsert;
            //                adapter.SelectCommand = commandSelect;

            //                var dataIsDataSet = data as System.Data.DataSet;
            //                var dataIsDataTable = data as System.Data.DataTable;
            //                var dataIsDataRow = data as System.Data.DataRow;
            //                var dataIsDataRowArray = data as System.Data.DataRow[];

            //                if (dataIsDataSet != null)
            //                    result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
            //                else if (dataIsDataTable != null)
            //                    result = adapter.Update(dataIsDataTable);
            //                else if (dataIsDataRow != null)
            //                    result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
            //                else if (dataIsDataRowArray != null)
            //                    result = adapter.Update(dataIsDataRowArray);
            //                else
            //                    throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
            //            }
            //        }

            //        transaction.Commit();
            //    }

            //    return result;
            //}
            //catch (Exception ex)
            //{

            //    try
            //    {
            //        if (transaction != null)
            //            transaction.Rollback();
            //    }
            //    catch (Exception exTransaction)
            //    {
            //        throw exTransaction;
            //    }

            //    throw ex;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}

        }

        #region Inicialize metody

        //public void InitializeCommandInsert_CZPRO_VPH(SqlCeCommand command)
        //{
        //    command.CommandText = @"INSERT INTO " + TableName_CZPRO_VPH +
        //        " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, DEX_ROW_ID) " +
        //        "VALUES" +
        //        " (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod, @DEX_ROW_ID)";

        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SOPTYPE", DbType = System.Data.DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SOPDESC", DbType = System.Data.DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@VNDDOCNMH", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@BarcodeH", DbType = System.Data.DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@DateProd", DbType = System.Data.DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@Rez1", DbType = System.Data.DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@Rez2", DbType = System.Data.DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });

        //}

        //public void InitializeCommandSelect_CZPRO_VPH(SqlCeCommand command)
        //{
        //    command.CommandText = "Select * from " + TableName_CZPRO_VPH;
        //}


        #endregion

        #endregion

        #region Delete_CZPRO_VPH Implemented

        public int Delete_CZPRO_VPH()
        {
            string CommandText = "DELETE FROM [" + TableName_CZPRO_VPH + "]";
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal(CommandText);
            }

            return ret;
        }

        #endregion

        #region Exist_VPH Implemented

        public bool Exist_VPH(string SOPNUMBE)
        {

            DSVyroba ds = new DSVyroba();
            string CommandText = "SELECT * FROM " + TableName_CZPRO_VPH + " WHERE SOPNUMBE = '" + SOPNUMBE.Trim() + "'";

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                con.Fill_Universal(ds, CommandText, ds.CZPRO_VPH.TableName);
            }

            if (ds.CZPRO_VPH != null && ds.CZPRO_VPH.Count == 1)
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #endregion

        #region CZPRO_VPP 

        #region Update_CZPRO_VPP Implemented

        public int Update_CZPRO_VPP(object data)
        {
            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                return con.Update_CZPRO_VPP(data, true);
            }

            //System.Data.SqlServerCe.SqlCeTransaction transaction = null;
            //System.Data.SqlServerCe.SqlCeConnection conn = null;
            //int result = 0;
            //try
            //{

            //    using (conn = new System.Data.SqlServerCe.SqlCeConnection(this._pathTo_PRD_File))
            //    {

            //        conn.Open();
            //        transaction = conn.BeginTransaction();

            //        using (var commandInsert = conn.CreateCommand())
            //        using (var commandSelect = conn.CreateCommand())
            //        {
            //            InitializeCommandInsert_CZPRO_VPP(commandInsert);
            //            InitializeCommandSelect_CZPRO_VPP(commandSelect);

            //            using (var adapter = new SqlCeDataAdapter())
            //            {
            //                adapter.InsertCommand = commandInsert;
            //                adapter.SelectCommand = commandSelect;

            //                var dataIsDataSet = data as System.Data.DataSet;
            //                var dataIsDataTable = data as System.Data.DataTable;
            //                var dataIsDataRow = data as System.Data.DataRow;
            //                var dataIsDataRowArray = data as System.Data.DataRow[];

            //                if (dataIsDataSet != null)
            //                    result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
            //                else if (dataIsDataTable != null)
            //                    result = adapter.Update(dataIsDataTable);
            //                else if (dataIsDataRow != null)
            //                    result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
            //                else if (dataIsDataRowArray != null)
            //                    result = adapter.Update(dataIsDataRowArray);
            //                else
            //                    throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
            //            }
            //        }

            //        transaction.Commit();
            //    }
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        if (transaction != null)
            //            transaction.Rollback();
            //    }
            //    catch (Exception exTransaction)
            //    { 
            //        throw ex;
            //    }

            //    throw ex;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}
        }

        //#region Inicialize metody

        //public void InitializeCommandInsert_CZPRO_VPP(SqlCeCommand command)
        //{
        //    command.CommandText = @"INSERT INTO " + TableName_CZPRO_VPP +
        //        " (" +
        //        " [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMDESC]," +
        //        " [ITEMMJ], [VNDDOCNMP], [VNDITNUM], [ORD], [BarcodeP]," +
        //        " [LOCNCODE], [QTYSHPPD], [QTYPACK], [QTYPACKMJ], [TIMEPREP]," +
        //        " [TIMEUNIT], [DtProdT], [DtProdL], [SerNumT], [SerNumL]," +
        //        " [VerT], [VerL], [TermID], [LSTMod], [QTYODVEDENO]," +
        //        " [CNTODVEDENO], [TIMEMODE], [DEX_ROW_ID]" +
        //        " ) VALUES ( " +
        //        " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC," +
        //        " @ITEMMJ, @VNDDOCNMP, @VNDITNUM, @ORD, @BarcodeP," +
        //        " @LOCNCODE, @QTYSHPPD, @QTYPACK, @QTYPACKMJ, @TIMEPREP," +
        //        " @TIMEUNIT, @DtProdT, @DtProdL, @SerNumT, @SerNumL," +
        //        " @VerT, @VerL, @TermID, @LSTMod, @QTYODVEDENO," +
        //        " @CNTODVEDENO, @TIMEMODE, @DEX_ROW_ID)";

        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@VNDDOCNMP", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@DtProdT", DbType = System.Data.DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@DtProdL", DbType = System.Data.DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SerNumT", DbType = System.Data.DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@SerNumL", DbType = System.Data.DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@VerT", DbType = System.Data.DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@VerL", DbType = System.Data.DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Int16, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@QTYODVEDENO", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYODVEDENO", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@CNTODVEDENO", DbType = System.Data.DbType.Decimal, SourceColumn = "CNTODVEDENO", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlCeParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });

        //}

        //public void InitializeCommandSelect_CZPRO_VPP(SqlCeCommand command)
        //{
        //    command.CommandText = "Select * from " + TableName_CZPRO_VPP;
        //}


        //#endregion

        #endregion

        #region Delete_CZPRO_VPP implemented

        public int Delete_CZPRO_VPP()
        {
            string CommandText = "DELETE FROM [" + TableName_CZPRO_VPP + "]";
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal( CommandText);
            }

            return ret;
        }

        #endregion

        #region Get_VPP Implemented

        public DSVyroba.CZPRO_VPPRow Get_VPP(string BarcodeP, string SOPNUMBE)
        {

            DSVyroba ds = new DSVyroba();
            string CommandText = "SELECT * FROM " + TableName_CZPRO_VPP +
                            " WHERE BarcodeP = '" + BarcodeP.Trim() + "'" +
                            " AND " +
                            " SOPNUMBE = '" + SOPNUMBE + "'";

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                con.Fill_Universal(ds, CommandText, ds.CZPRO_VPP.TableName);
            }

            if (ds.CZPRO_VPP != null && ds.CZPRO_VPP.Count == 1)
            {
                return ds.CZPRO_VPP.First();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region FASK_Logins

        #region Update_FASK_Logins Implemented
        public int Update_FASK_Logins(object data)
        {
            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                return con.Update_FASK_Logins(data);
            }
        }
        #endregion

        #region Delete_FASK_Logins Implemented
        public int Delete_FASK_Logins()
        {
            string CommandText = "DELETE FROM [" + TableName_FASK_Logins + "]";
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal(CommandText);
            }

            return ret;
        }
        #endregion

        #endregion

        #region FASK_Machines

        #region Update_FASK_Machines Implemented
        public int Update_FASK_Machines(object data)
        {
            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                return con.Update_FASK_Machines(data);
            }
        }
        #endregion

        #region Delete_FASK_Machines Implemented
        public int Delete_FASK_Machines()
        {
            string CommandText = "DELETE FROM [" + TableName_FASK_Machines + "]";
            int ret = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(_pathTo_PRD_File))
            {
                ret = con.Delete_Universal(CommandText);
            }

            return ret;
        }
        #endregion

        #endregion

        #region FASK_MachineType

        #region Update_FASK_MachineType NOT Implemented
        public int Update_FASK_MachineType(object data)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete_FASK_MachineType NOT Implemented

        public int Delete_FASK_MachineType()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region FASK_Operations_Next

        #region Update_FASK_Operations_Next NOT Implemented

        public int Update_FASK_Operations_Next(object data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete_FASK_Operations_Next NOT Implemented

        public int Delete_FASK_Operations_Next()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region FASK_Operations

        #region Update_FASK_Operations NOT Implemented
        public int Update_FASK_Operations(object data)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete_FASK_Operations NOT Implemented
        public int Delete_FASK_Operations()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
