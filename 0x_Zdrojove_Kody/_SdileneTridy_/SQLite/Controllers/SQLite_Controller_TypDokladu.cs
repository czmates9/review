using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik typu dokladu : CZMST092
    /// </summary>
    public class SQLite_Controller_TypDokladu : SQLite_Controller
    {
        //private Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter ta_typdokladu = null;
        //internal Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter Ta_typdokladu
        //{
        //    get
        //    {
        //        if (ta_typdokladu == null)
        //        {
        //            ta_typdokladu = new Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter();
        //            ta_typdokladu.Connection = this.Connection;
        //        }
        //        return ta_typdokladu;
        //    }
        //}

        #region c'tors
        //public SQLite_Controller_TypDokladu()
        //    : base(Main.CiselnikTypDokladuDB)
        //{
        //}

        public SQLite_Controller_TypDokladu(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_TypDokladu(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

        //protected override void AdaptersInitialize()
        //{
        //    base.AdaptersInitialize();

        //    ta_typdokladu = new Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter();

        //    ta_typdokladu.Connection = this.Connection;

        //}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
            //if (ta_typdokladu != null)
            //    ta_typdokladu.Dispose();

            //this.DisposeObject(ta_typdokladu);

            //this.ta_typdokladu = null;

            base.Dispose();
        }


        #region Metody


        public int Fill(Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable dataTable)
        {
            try
            {
                Connection_Open();
                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM CZMST092";
                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        int returnValue = adapter.Fill(dataTable);
                        return returnValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                Connection_Close();
            }

        }

        public object GetLokMech_Status(string doc_id, string doc_id2)
        {
            try
            {
                Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT cfg_lok_mech FROM CZMST092 WHERE (doc_id = @doc_id) AND (doc_id2 = @doc_id2)";
                    command.CommandType = global::System.Data.CommandType.Text;

                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id", DbType = System.Data.DbType.String, Value = doc_id == null ? (object)DBNull.Value : doc_id });
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id2", DbType = System.Data.DbType.String, Value = doc_id2 == null ? (object)DBNull.Value : doc_id2 });

                    object returnValue = command.ExecuteScalar();

                    if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                    {
                        return null;
                    }
                    else
                    {
                        return ((object)(returnValue));
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
            finally
            {
                Connection_Close();
            }
        }

        public object GetDesc(string doc_id, string doc_id2)
        {
            try
            {
                Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT doc_desc FROM CZMST092 WHERE (doc_id = @doc_id) AND (doc_id2 = @doc_id2)";
                    command.CommandType = global::System.Data.CommandType.Text;

                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id", DbType = System.Data.DbType.String, Value = doc_id == null ? (object)DBNull.Value : doc_id });
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id2", DbType = System.Data.DbType.String, Value = doc_id2 == null ? (object)DBNull.Value : doc_id2 });

                    object returnValue = command.ExecuteScalar();

                    if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                    {
                        return null;
                    }
                    else
                    {
                        return ((object)(returnValue));
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
            finally
            {
                Connection_Close();
            }
        }

        internal int Update(object data)
        {
            SQLiteTransaction transaction = null;
            try
            {
                int result = 0;
                Connection_Open();

                transaction = this.Connection.BeginTransaction();

                using (var commandInsert = this.Connection.CreateCommand())
                //using (var commandUpdate = this.Connection.CreateCommand())
                //using (var commandDelete = this.Connection.CreateCommand())
                using (var commandSelect = this.Connection.CreateCommand())
                {
                    InitializeCommandInsert(commandInsert);
                    //InitializeCommandUpdate_CZMST095(commandUpdate);
                    //InitializeCommandDelete_CZMST095(commandDelete);
                    InitializeCommandSelect(commandSelect);

                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        //adapter.DeleteCommand = commandDelete;
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
                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    //Logging.Log.Write(exTransaction);
                    Logging.ExceptionHandler2.Handle(exTransaction);
                }

                throw ex;
            }
            finally
            {
                Connection_Close();
            }
        }

        #region Inicialize metody

        public void InitializeCommandInsert(SQLiteCommand command)
        {
            command.CommandText = "INSERT INTO CZMST092 ( " +
                " doc_id," + " doc_id2, " + " doc_desc," + " doc_typ," + " doc_carcode, " +
                " DEX_ROW_ID," + " LOCNCODE," + " cfg_odb," + " cfg_str," + " cfg_disp, " +
                " cfg_disp_dest," + " cfg_mn2sn," + " cfg_prac," + " cfg_palety," + " cfg_paleta_id," +
                " cfg_zakazka_id," + " cfg_mena_id," + " cfg_tisk," + " cfg_prevod_sklad," + " cfg_tisk_soupis," +
                " SKL_ID," + " cfg_lokace," + " cfg_lokace_ciselnik," + " cfg_lokace_dest," + " cfg_onl_dop_pal," + 
                " cfg_onl_over_lokace," + " cfg_onl_over_lokace_dest," + " cfg_mnozstvi_ze_zbozi, " + " cfg_predvyplnit_mnozstvi," + " cfg_skl_id_dest," + 
                " predvyplnit_skl_id_dest," + " cfg_lok_mech," + " cfg_lok_mech_pohyb_type, " + " cfg_skl_id_dest_prevzit," + " cfg_lokace_dest_ciselnik," + 
                " predvyplnit_locncodedest," + " cfg_sklady," + " cfg_onl_dop_lokace_dest, " + " cfg_generovat_sn," + " cfg_parsovat_ck," + 
                " cfg_sn_na_davku," + " cfg_lok_mech_online_pohyby," + " cfg_onl_palety_generovat, " + " cfg_tisk_palety," + " cfg_sklady_zmena," + 
                " cfg_delka_SN," + " cfg_Navrh, "
                + " cfg_sarze_ONOFF, " + " cfg_sn_ONOFF, " + " cfg_expirace_ONOFF, " + " cfg_AttributeToSN_ONOFF" +
                " ) VALUES ( " +
                " @doc_id," + " @doc_id2," + " @doc_desc," + " @doc_typ," + " @doc_carcode, " + 
                " @DEX_ROW_ID," + " @LOCNCODE," + " @cfg_odb," + " @cfg_str," + " @cfg_disp, " + 
                " @cfg_disp_dest," + " @cfg_mn2sn," + " @cfg_prac, " + " @cfg_palety," + " @cfg_paleta_id," + 
                " @cfg_zakazka_id," + " @cfg_mena_id," + " @cfg_tisk, " + " @cfg_prevod_sklad," + " @cfg_tisk_soupis," + 
                " @SKL_ID," + " @cfg_lokace," + " @cfg_lokace_ciselnik, " + " @cfg_lokace_dest," + " @cfg_onl_dop_pal," + 
                " @cfg_onl_over_lokace," + " @cfg_onl_over_lokace_dest," + " @cfg_mnozstvi_ze_zbozi, " + " @cfg_predvyplnit_mnozstvi," + " @cfg_skl_id_dest," + 
                " @predvyplnit_skl_id_dest," + " @cfg_lok_mech," + " @cfg_lok_mech_pohyb_type, " + " @cfg_skl_id_dest_prevzit," + " @cfg_lokace_dest_ciselnik," + 
                " @predvyplnit_locncodedest," + " @cfg_sklady," + " @cfg_onl_dop_lokace_dest, " + " @cfg_generovat_sn," + " @cfg_parsovat_ck," + 
                " @cfg_sn_na_davku," + " @cfg_lok_mech_online_pohyby," + " @cfg_onl_palety_generovat, " + " @cfg_tisk_palety," + " @cfg_sklady_zmena," + 
                " @cfg_delka_SN," + " @cfg_Navrh, " +
                " @cfg_sarze_ONOFF, " + "@cfg_sn_ONOFF, " + "@cfg_expirace_ONOFF, " + " @cfg_AttributeToSN_ONOFF " +
                " )";

            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id", DbType = System.Data.DbType.String, SourceColumn = "doc_id", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_desc", DbType = System.Data.DbType.String, SourceColumn = "doc_desc", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_typ", DbType = System.Data.DbType.String, SourceColumn = "doc_typ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_carcode", DbType = System.Data.DbType.String, SourceColumn = "doc_carcode", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_odb", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_odb", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_str", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_str", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_disp", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_disp", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_disp_dest", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_disp_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_mn2sn", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_mn2sn", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_prac", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_prac", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_palety", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_palety", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_paleta_id", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_paleta_id", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_zakazka_id", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_zakazka_id", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_mena_id", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_mena_id", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@doc_id2", DbType = System.Data.DbType.String, SourceColumn = "doc_id2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_tisk", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_tisk", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_prevod_sklad", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_prevod_sklad", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_tisk_soupis", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_tisk_soupis", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lokace", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lokace", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lokace_ciselnik", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lokace_ciselnik", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lokace_dest", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lokace_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_onl_dop_pal", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_onl_dop_pal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_onl_over_lokace", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_onl_over_lokace", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_onl_over_lokace_dest", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_onl_over_lokace_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_mnozstvi_ze_zbozi", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_mnozstvi_ze_zbozi", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_predvyplnit_mnozstvi", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_predvyplnit_mnozstvi", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_skl_id_dest", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_skl_id_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@predvyplnit_skl_id_dest", DbType = System.Data.DbType.String, SourceColumn = "predvyplnit_skl_id_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lok_mech", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lok_mech", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lok_mech_pohyb_type", DbType = System.Data.DbType.String, SourceColumn = "cfg_lok_mech_pohyb_type", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_skl_id_dest_prevzit", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_skl_id_dest_prevzit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lokace_dest_ciselnik", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lokace_dest_ciselnik", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@predvyplnit_locncodedest", DbType = System.Data.DbType.String, SourceColumn = "predvyplnit_locncodedest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_sklady", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_sklady", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_onl_dop_lokace_dest", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_onl_dop_lokace_dest", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_generovat_sn", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_generovat_sn", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_parsovat_ck", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_parsovat_ck", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_sn_na_davku", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_sn_na_davku", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_lok_mech_online_pohyby", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_lok_mech_online_pohyby", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_onl_palety_generovat", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_onl_palety_generovat", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_tisk_palety", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_tisk_palety", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_sklady_zmena", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_sklady_zmena", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_delka_SN", DbType = System.Data.DbType.Int32, SourceColumn = "cfg_delka_SN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_Navrh", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_Navrh", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_sarze_ONOFF", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_sarze_ONOFF", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_sn_ONOFF", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_sn_ONOFF", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_expirace_ONOFF", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_expirace_ONOFF", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@cfg_AttributeToSN_ONOFF", DbType = System.Data.DbType.Byte, SourceColumn = "cfg_AttributeToSN_ONOFF", SourceVersion = DataRowVersion.Current });
            
        }

        //public void InitializeCommandUpdate(SQLiteCommand command)
        //{
        //	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

        //	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
        //}

        //public void InitializeCommandDelete(SQLiteCommand command)
        //{
        //	command.CommandText = "DELETE FROM CZMST_SE WHERE (guid = @guid)";

        //	command.Parameters.Add(new SQLiteParameter()
        //	{
        //		ParameterName = "@guid",
        //		DbType = System.Data.DbType.Guid,
        //		SourceColumn = "guid",
        //		SourceVersion = System.Data.DataRowVersion.Original
        //	});
        //}

        public void InitializeCommandSelect(SQLiteCommand command)
        {
            command.CommandText = "SELECT * FROM CZMST092";
        }


        #endregion


        #endregion



        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable GetDataByDocID_DocID2(string Doc_ID, string Doc_ID2)
        {
            Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable dt = new Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable();
            try
            {
                Connection_Open();

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                {
                    using (var command = this.Connection.CreateCommand())
                    {
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.Connection = this.Connection;
                        adapter.SelectCommand.CommandText = "SELECT * FROM CZMST092 WHERE doc_id=@doc_id AND doc_id2=@doc_id2";

                        var param = new System.Data.SQLite.SQLiteParameter();
                        param.ParameterName = "@doc_id";
                        param.DbType = System.Data.DbType.String;

                        if (Doc_ID == null)
                            param.Value = DBNull.Value;
                        else
                            param.Value = Doc_ID;

                        command.Parameters.Add(param);

                        var param2 = new System.Data.SQLite.SQLiteParameter();
                        param2.ParameterName = "@doc_id2";
                        param2.DbType = System.Data.DbType.String;

                        if (Doc_ID2 == null)
                            param2.Value = DBNull.Value;
                        else
                            param2.Value = Doc_ID2;

                        command.Parameters.Add(param2);

                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
                Connection_Close();
            }
        }


    }
}
