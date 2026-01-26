using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace Fask.ModulePohodaXML.Provider.Vyroba
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck,
        Fask.Interfaces.Tisky.ITisky2,
        Fask.Interfaces.Tisky.ITisk_TiskovaSablona
    {

        #region IOdvod_Events_GetFiltrovanyOdvodEvents Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals_V1.LoadConfiguration();
               
                adapter = new SqlDataAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                command.Connection = connection;

                command.CommandText =
                        "SELECT E.*, Z.ITEMDESC FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Events + " as E " +
                        " LEFT JOIN ( SELECT ITEMNMBR, VNDITNUM, MAX(ITEMDESC) as ITEMDESC from FASK_ZASOBY GROUP BY ITEMNMBR, VNDITNUM) as Z " +
                        " ON Z.ITEMNMBR = E.IS_ID AND Z.VNDITNUM = E.EAN_IS " +
                        " WHERE " +
                        " 1=1 ";


                if (filtr.productionGUID != null)
                {
                    command.CommandText += "and E.productionGUID = @productionGUID";
                    command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);
                }


                if (filtr.Zpracovane && !filtr.NEZpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NOT NULL ";
                }
                else if (filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL ";
                }
                else if (!filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL and E.IsProcessed is NOT NULL ";

                }

                // hledání podle datumu
                if (filtr.IsProcessed_OD != null && filtr.IsProcessed_DO != null)
                {
                    command.CommandText += " AND E.IsProcessed between @IsProcessedOD and @IsProcessedDO";
                    command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                }
                else
                {
                    if (filtr.IsProcessed_OD != null)
                    {
                        command.CommandText += " AND E.IsProcessed > @IsProcessedOD";
                        command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    }
                    else if (filtr.IsProcessed_DO != null)
                    {
                        command.CommandText += " AND E.IsProcessed < @IsProcessedDO";
                        command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                    }
                }

                if (!string.IsNullOrEmpty(filtr.IsProcessed_TimeVariant))
                {
                    if (!filtr.IsProcessed_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.IsProcessed_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.IsProcessed > @IsProcessed_TV";
                        command.Parameters.AddWithValue("@IsProcessed_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (filtr.Dateeve_OD != null && filtr.Dateeve_DO != null)
                {
                    command.CommandText += " AND E.dateeve between @dateeveOD and @dateeveDO";
                    command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                }
                else
                {
                    if (filtr.Dateeve_OD != null)
                    {
                        command.CommandText += " AND E.dateeve > @dateeveOD";
                        command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    }
                    else if (filtr.Dateeve_DO != null)
                    {
                        command.CommandText += " AND E.dateeve < @dateeveDO";
                        command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                    }
                }


                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                        command.CommandText += " AND E.dateeve > @dateeve_TV";
                        command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.MachineID))
                {
                    command.CommandText += " AND E.machineid = @machineid ";
                    command.Parameters.AddWithValue("@machineid", filtr.MachineID);
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND CAST(E.[description] AS NVARCHAR(MAX)) like '" + filtr.Description + "%'";
                }

                #region Status filtr

                if (!string.IsNullOrEmpty(filtr.status))
                {
                    command.CommandText += " AND E.status in (" + filtr.status + ")";
                }

                #endregion

                if (!string.IsNullOrEmpty(filtr.PackType))
                {
                    command.CommandText += " AND E.PackType = @PackType ";
                    command.Parameters.AddWithValue("@PackType", filtr.PackType);
                }

                if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                {
                    command.CommandText += " order by " + "E." + filtr.Razeni_Column + " " + filtr.asc_desc;
                }
                else
                {
                    command.CommandText += " order by " + "E.dateeve desc";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_Events);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IOdvod_Events_CallProcedura Members

        public string CallProcedura(DateTime? OD, DateTime? DO, string Material, out int? CountEntries)
        {

            string msg = string.Empty;
            CountEntries = null;

            SqlConnection conn = null;
            SqlDataAdapter adapter = null;
            SqlCommand command = null;

            try
            {

                Globals_V1.LoadConfiguration();

                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                adapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = "fask_Events2Production_Confirm";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@from", OD);
                command.Parameters.AddWithValue("@to", DO);
                command.Parameters.AddWithValue("@material", string.IsNullOrEmpty(Material) ? string.Empty : Material.Trim());
                command.Parameters.Add("@CountEntries", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;



                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();


                object tmpCountEntries = command.Parameters["@CountEntries"].Value;
                object tmpmsg = command.Parameters["@Message"].Value;

                if (tmpCountEntries != null)
                    CountEntries = tmpCountEntries as int?;

                if (tmpmsg != null)
                    msg = tmpmsg as string;

                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        #endregion

        #region IOdvod_Events_GetMaterials Members

        public DataTable GetMaterials()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                Globals_V1.LoadConfiguration();

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "SELECT material FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Events +
                    " WHERE IsProcessed is NULL " +
                    " Group by material "
                    ;

                adapter.SelectCommand = command;
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IOdvod_Events_StornoEvent Members

        public bool StornoEvent(Interfaces.DataSets.Vyroba.FASK_EventsRow row)
        {
            SqlConnection con = null;
            SqlDataAdapter ada = null;

            try
            {
                Globals_V1.LoadConfiguration();

                Guid NovyGUID = Guid.NewGuid();


                using (con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (ada = new SqlDataAdapter())
                    {
                        using (var command = con.CreateCommand())
                        {

                            con.Open();

                            #region Insert

                            ada.InsertCommand = command;
                            ada.InsertCommand.Connection = con;

                            ada.InsertCommand.CommandText += " INSERT INTO [FASK_Events] " +
                                                            "(" +
                                                            " [loginid]" +
                                                            " ,[machineid]" +
                                                            " ,[dateeve]" +
                                                            " ,[qty]" +
                                                            " ,[qtyReal]" +
                                                            " ,[description]" +
                                                            " ,[barcodeReaded]" +
                                                            " ,[barcodeSended]" +
                                                            " ,[zakazka]" +
                                                            " ,[popis]" +
                                                            " ,[faskGUID]" +
                                                            " ,[reportType]" +
                                                            //" ,[isProcessed]" +
                                                            " ,[IDO]" +
                                                            " ,[scan1]" +
                                                            " ,[scan2]" +
                                                            " ,[scan3]" +
                                                            " ,[sensor]" +
                                                            " ,[material]" +
                                                            " ,[VPH]" +
                                                            " ,[VPPol]" +
                                                            " ,[EAN_IS]" +
                                                            " ,[IS_ID]" +
                                                            " ,[NMBRPAL]" +
                                                            " ,[status]" +
                                                            //" ,[productionGuid]" +
                                                            " ,[QTYPACK]" +
                                                            " ,[PackType]" +
                                                            " ,[WEIGHT]" +
                                                            " ) " +
                                                            " VALUES" +
                                                            " ( " +
                                                            " @loginid" +
                                                            " ,@machineid" +
                                                            " ,@dateeve" +
                                                            " ,@qty" +
                                                            " ,@qtyReal" +
                                                            " ,@description" +
                                                            " ,@barcodeReaded" +
                                                            " ,@barcodeSended" +
                                                            " ,@zakazka" +
                                                            " ,@popis" +
                                                            " ,@faskGUID" +
                                                            " ,@reportType" +
                                                            //" ,@isProcessed" +
                                                            " ,@IDO" +
                                                            " ,@scan1" +
                                                            " ,@scan2" +
                                                            " ,@scan3" +
                                                            " ,@sensor" +
                                                            " ,@material" +
                                                            " ,@VPH" +
                                                            " ,@VPPol" +
                                                            " ,@EAN_IS" +
                                                            " ,@IS_ID" +
                                                            " ,@NMBRPAL" +
                                                            " ,@status" +
                                                            // " ,@productionGuid" +
                                                            " ,@QTYPACK" +
                                                            " ,@PackType" +
                                                            " ,@WEIGHT" +
                                                            " )";


                            ada.InsertCommand.CommandType = System.Data.CommandType.Text;

                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.Int32, Value = row.loginid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, Value = row.machineid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = row.dateeve });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@qty", DbType = System.Data.DbType.Decimal, Value = row.qty });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@qtyReal", DbType = System.Data.DbType.Decimal, Value = row.qtyReal });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, Value = row.IsdescriptionNull() ? null : row.description.Trim() });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeReaded", DbType = System.Data.DbType.String, Value = row.barcodeReaded });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeSended", DbType = System.Data.DbType.String, Value = row.barcodeSended });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@zakazka", DbType = System.Data.DbType.String, Value = row.IszakazkaNull() ? null : row.zakazka });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@popis", DbType = System.Data.DbType.String, Value = row.IspopisNull() ? null : row.popis });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = NovyGUID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@reportType", DbType = System.Data.DbType.String, Value = row.reportType });
                            //ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@isProcessed", DbType = System.Data.DbType.DateTime, Value = row.IsisProcessedNull() ? null : (object)row.isProcessed });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = System.Data.DbType.String, Value = row.IsIDONull() ? null : row.IDO });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan1", DbType = System.Data.DbType.String, Value = row.Isscan1Null() ? null : row.scan1 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan2", DbType = System.Data.DbType.String, Value = row.Isscan2Null() ? null : row.scan2 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan3", DbType = System.Data.DbType.String, Value = row.Isscan3Null() ? null : row.scan3 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@sensor", DbType = System.Data.DbType.String, Value = row.IssensorNull() ? null : row.sensor });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@material", DbType = System.Data.DbType.String, Value = row.IsmaterialNull() ? null : row.material });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VPH", DbType = System.Data.DbType.String, Value = row.IsVPHNull() ? null : row.VPH });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VPPol", DbType = System.Data.DbType.Int32, Value = row.IsVPPolNull() ? null : (object)row.VPPol });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@EAN_IS", DbType = System.Data.DbType.String, Value = row.IsEAN_ISNull() ? null : row.EAN_IS });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IS_ID", DbType = System.Data.DbType.String, Value = row.IsIS_IDNull() ? null : row.IS_ID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = row.IsNMBRPALNull() ? null : row.NMBRPAL });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@status", DbType = System.Data.DbType.Int32, Value = row.status });
                            //ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@productionGuid", DbType = System.Data.DbType.Guid, Value = row.IsproductionGuidNull() ? null : (object)row.productionGuid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = row.IsQTYPACKNull() ? null : (object)row.QTYPACK });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@PackType", DbType = System.Data.DbType.String, Value = row.IsPackTypeNull() ? null : row.PackType });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = row.IsWEIGHTNull() ? null : (object)row.WEIGHT });

                            int returnValue_I = ada.InsertCommand.ExecuteNonQuery();

                            #endregion

                            if (returnValue_I == 1)
                            {

                                ada.SelectCommand = command;
                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandText = "SELECT id FROM [FASK_Events] " +
                                    " WHERE  1 = 1" +
                                    " AND faskGUID = @faskGUID ";


                                ada.SelectCommand.Parameters.Clear();
                                ada.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = NovyGUID });

                                ada.SelectCommand.CommandType = System.Data.CommandType.Text;
                                var id_O = ada.SelectCommand.ExecuteScalar();

                                int? ID = null;

                                if (id_O != null)
                                {
                                    try
                                    {
                                        ID = (int)id_O;
                                    }
                                    catch (Exception ex)
                                    {
                                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "ID FASK_Events nenalezeno!");
                                        Logging.ExceptionHandler2.Handle(ex);
                                        return false;
                                    }
                                }

                                ada.UpdateCommand = command;
                                ada.UpdateCommand.Connection = con;

                                ada.UpdateCommand.CommandText += " UPDATE [FASK_Events] " +
                                                                 " SET [IDO] = @IDO " +
                                                                 " ,[status] = 999 " +
                                                                 " WHERE 1 = 1 " +
                                                                 // AND [id] = @id_original " +
                                                                 " AND  faskGUID = @faskGUID";

                                ada.UpdateCommand.Parameters.Clear();
                                ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = System.Data.DbType.String, Value = string.Format("SN:{0}", ID) });
                                //ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@id_original", DbType = System.Data.DbType.Int32, Value = row.id });
                                ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = row.faskGUID });

                                int returnValue_U = ada.UpdateCommand.ExecuteNonQuery();

                                if (returnValue_U == 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Update do FASK_Events Neproveden!");
                                    return false;
                                }
                            }
                        }
                    }
                }





                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        #endregion

        #region IOdvod_Events_StornoEvent_OnlineCheck Members

        public int? StornoEvent_OnlineCheck(Guid G)
        {

            SqlConnection con = null;
            SqlCommand com = null;
            int? status_value = -1;

            try
            {
                Globals_V1.LoadConfiguration();

                if (G != null)
                {
                    using (con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {

                        using (com = con.CreateCommand())
                        {

                            con.Open();

                            com.Connection = con;
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT status FROM [FASK_Events] " +
                                " WHERE  1 = 1" +
                                " AND faskGUID = @faskGUID ";

                            com.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = G });

                            com.CommandType = System.Data.CommandType.Text;
                            var xx = com.ExecuteScalar();

                            status_value = (int?)xx;

                        }
                    }
                }

                return status_value;


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public int TiskovaSablonaEdit_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            //throw new NotImplementedException();

            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.Update(row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        public int TiskovaSablonaInsert_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Insert(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, row);
        }

        public int TiskovaSablonaDelete_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Delete(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, row);
        }
    }
}
