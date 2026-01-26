using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Fask.SQL.Database
{
    class Prijem
    {
        [Obsolete("Problem s vicenasobnym pristupem ... ", false)]
        public static int CZMSTPE_MAX_CountEntries()
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            try
            {
                sqlcommand = new System.Data.SqlClient.SqlCommand();
                sqlcommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcommand.CommandType = System.Data.CommandType.Text;
                sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_PE";

                //sqlcommand.CommandTimeout = Settings.CommandTimeout;

                sqlcommand.Connection.Open();

                int countentries = Convert.ToInt32(sqlcommand.ExecuteScalar());
                return countentries;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return 0;
            }
            finally
            {
                if (sqlcommand != null && sqlcommand.Connection.State == System.Data.ConnectionState.Open)
                    sqlcommand.Connection.Close();
            }
        }

        /// <summary>
        /// Vyvola zjisteni maximalniho cisla davky v PE
        /// </summary>
        /// <param name="sqlconnection">Otevrene Connection</param>
        /// <param name="sqltransaction">Aktivni transakce</param>
        /// <returns>maximalni cislo davky nebo 0, pokud neexistuje ... ???</returns>
        /// <exception cref="SqlException">Komunikacni chyba s databazi</exception>
        public static int CZMSTPE_MAX_CountEntries(SqlConnection sqlconnection, SqlTransaction sqltransaction)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            sqlcommand = new System.Data.SqlClient.SqlCommand();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandType = System.Data.CommandType.Text;
            sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_PE";
            sqlcommand.Transaction = sqltransaction;
            object o = sqlcommand.ExecuteScalar();

            try
            {
                int countentries = Convert.ToInt32(o);
                return countentries;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return 0; //pokud nenalezeno ... ???
            }
        }

        public static bool CZMSTPE_EXIST_PONNUMBER(string ponumber, string itemnmbr, int ord)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            try
            {
                sqlcommand = new System.Data.SqlClient.SqlCommand();
                sqlcommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcommand.CommandType = System.Data.CommandType.Text;
                sqlcommand.CommandText = "SELECT CountEntries FROM CZMST_PE WHERE PONUMBER=@ponumber AND ITEMNMBR=@itemnmbr AND ORD=@ord";
                sqlcommand.Parameters.AddWithValue("@ponumber", ponumber);
                sqlcommand.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                sqlcommand.Parameters.AddWithValue("@ord", ord);

                //sqlcommand.CommandTimeout = Settings.CommandTimeout;

                sqlcommand.Connection.Open();

                string lokace = Convert.ToString(sqlcommand.ExecuteScalar());

                if (lokace.Length > 0)
                    return true; //status: 0=neuspech, 1=ok
                else
                    return false;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if (sqlcommand != null && sqlcommand.Connection.State == System.Data.ConnectionState.Open)
                    sqlcommand.Connection.Close();
            }
        }



        public static bool CZMSTPE_PONUMBER_EXIST(string ponumber)
        {
            try
            {

                Datasets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                ta_pe.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                return ta_pe.ScalarQuery(ponumber) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Kontroluje, zda prijemka existuje, respektive, vraci distinct cz_doslo pro kontrolu existence
        /// </summary>
        /// <param name="ponumber">cislo dokladu PONUMBER</param>
        /// <returns></returns>
        public static byte? CZMSTPE_PONUMBER_CZDOSLO(string ponumber)
        {
            try
            {

                Datasets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                ta_pe.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                return ta_pe.ScalarQueryCZDOSLO(ponumber);
            }
            catch (SqlException sqlex)
            {
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static bool CZMSTPE_UPDATE_CZDOSLO(string ponumber)
        {
            try
            {

                Datasets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                ta_pe.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                return ta_pe.UpdateQuery(ponumber) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
            }
        }
        
        public static int CZMSTPE_CountEntries_SOPNUMBE(string ponumbe)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            try
            {
                sqlcommand = new System.Data.SqlClient.SqlCommand();
                sqlcommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcommand.CommandType = System.Data.CommandType.Text;
                sqlcommand.CommandText = "SELECT CountEntries FROM CZMST_PE WHERE PONUMBER=@ponumbe";
                sqlcommand.Parameters.AddWithValue("@ponumbe", ponumbe); ;

                //sqlcommand.CommandTimeout = Settings.CommandTimeout;

                sqlcommand.Connection.Open();

                int cntent = Convert.ToInt32(sqlcommand.ExecuteScalar());

                return cntent;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
            finally
            {
                if (sqlcommand != null && sqlcommand.Connection.State == System.Data.ConnectionState.Open)
                    sqlcommand.Connection.Close();
            }
        }



    //    public static Datasets.Prijem.CZMST_PIDataTable GETDATA_CZMSTPI_DT(int countentries)
    //    {
    //        Datasets.Prijem.CZMST_PIDataTable tbl_pi = null;
    //        try
    //        {

    //            Datasets.PrijemTableAdapters.CZMST_PITableAdapter ta = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
    //            ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
    //            tbl_pi = ta.GetDataByCountEntries(countentries);
    //            return tbl_pi;
    //        }
    //        catch (SqlException sqlex)
    //        {
				//Fask.Logging.ExceptionHandler2.Handle(sqlex);
    //            return null;
    //        }
    //        catch (Exception ex)
    //        {
				//Fask.Logging.ExceptionHandler2.Handle(ex);
    //            return null;
    //        }
    //        finally
    //        {
    //        }
    //    }

        public static Datasets.Prijem.CZMST_PIDataTable GETDATA_CZMSTPI_DT(int countentries)
        {
            Datasets.Prijem tbl_pi = new Datasets.Prijem();
            try
            {
                try
                {
                    Globals_V1.LoadConfiguration();

                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT * FROM CZMST_PI " +
                                " WHERE CountEntries = " + countentries.ToString();


                            com.CommandType = System.Data.CommandType.Text;

                            using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                ada.SelectCommand = com;
                                ada.Fill(tbl_pi, tbl_pi.CZMST_PI.TableName);

                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

                return tbl_pi.CZMST_PI;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                tbl_pi = new Datasets.Prijem();
                return tbl_pi.CZMST_PI;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tbl_pi = new Datasets.Prijem();
                return tbl_pi.CZMST_PI;
            }
            finally
            {
            }
        }

        //    public static Datasets.Prijem GETDATA_CZMSTPI_DS(int countentries)
        //    {
        //        Datasets.Prijem ds = null;
        //        try
        //        {

        //            Datasets.PrijemTableAdapters.CZMST_PITableAdapter ta = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
        //            ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
        //            ta.FillByCountEntries(ds.CZMST_PI,countentries);
        //            return ds;
        //        }
        //        catch (SqlException sqlex)
        //        {
        //Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //            return null;
        //        }
        //        catch (Exception ex)
        //        {
        //Fask.Logging.ExceptionHandler2.Handle(ex);
        //            return null;
        //        }
        //        finally
        //        {
        //        }
        //    }

        public static Datasets.Prijem GETDATA_CZMSTPI_DS_GroupBy_CountEntries(int countentries)
        {
            Datasets.Prijem tbl_pi = new Datasets.Prijem();
            try
            {


                try
                {
                    Globals_V1.LoadConfiguration();

                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT " +
                                " CountEntries, PONUMBER, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, SERLTNUM, Expirace, 0 AS INPUT_MODE, SUM(QTYSHPPDMJ) AS QTYSHPPDMJ, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, AttributeToSN" + 
                                " FROM CZMST_PI " +  
                                " WHERE CountEntries = " + countentries.ToString() +
                                " GROUP BY CountEntries, PONUMBER, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, SERLTNUM, Expirace, AttributeToSN";


                            com.CommandType = System.Data.CommandType.Text;

                            using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                ada.SelectCommand = com;
                                ada.Fill(tbl_pi, tbl_pi.CZMST_PI.TableName);

                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }



                //Datasets.PrijemTableAdapters.CZMST_PITableAdapter ta = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                //ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                //tbl_pi = ta.GetDataByGroupBy_CountEntries(countentries);
                return tbl_pi;
            }
            catch (SqlException sqlex)
            {
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static Datasets.Prijem GETDATA_CZMSTPI_DS_By_CountEntries(int countentries)
        {
            Datasets.Prijem tbl_pi = new Datasets.Prijem();
            try
            {


                try
                {
                    Globals_V1.LoadConfiguration();

                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT * FROM CZMST_PI WHERE CountEntries = " + countentries.ToString();


                            com.CommandType = System.Data.CommandType.Text;

                            using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                ada.SelectCommand = com;
                                ada.Fill(tbl_pi, tbl_pi.CZMST_PI.TableName);

                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }



                //Datasets.PrijemTableAdapters.CZMST_PITableAdapter ta = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                //ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                //tbl_pi = ta.GetDataByGroupBy_CountEntries(countentries);
                return tbl_pi;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        public static void GETDATA_CZMSTPE_DS_By_CountEntries(Datasets.Prijem.CZMST_PEDataTable tbl_pe, int countentries)
        {
            try
            {

                try
                {
                    Globals_V1.LoadConfiguration();

                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT * FROM CZMST_PE WHERE CountEntries = " + countentries.ToString();


                            com.CommandType = System.Data.CommandType.Text;

                            using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                ada.SelectCommand = com;
                                ada.Fill(tbl_pe);

                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            finally
            {
            }
        }


        //public static Datasets.Prijem GETDATA_CZMSTPI_DS_GroupBy_CountEntries(int countentries)
        //{
        //    Datasets.Prijem ds = new Datasets.Prijem();
        //    try
        //    {
        //        Datasets.PrijemTableAdapters.CZMST_PITableAdapter ta = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
        //        ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
        //        ta.FillByGropuBy_CountEntries(ds.CZMST_PI, countentries);
        //        return ds;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}

        public static Datasets.Prijem.CZMST_PIHDataTable GETDATA_CZMSTPIH(int countentries)
        {
            Datasets.Prijem.CZMST_PIHDataTable tbl_pih = null;
            try
            {

                Datasets.PrijemTableAdapters.CZMST_PIHTableAdapter tah = new Datasets.PrijemTableAdapters.CZMST_PIHTableAdapter();
                tah.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                tbl_pih = tah.GetDataByCountEntries(countentries);
                return tbl_pih;
            }
            catch (SqlException sqlex)
            {
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }


        public static int Update_CZMST_PI(object data, SqlConnection connection, SqlTransaction trans)
        {
            try
            {
                int result = 0;

                using (var commandInsert = connection.CreateCommand())
                using (var commandSelect = connection.CreateCommand())
                {
                    commandInsert.Transaction = trans;
                    commandSelect.Transaction = trans;

                    InitializeCommandInsert_PI(commandInsert);
                    InitializeCommandSelect_PI(commandSelect);

                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.SelectCommand = commandSelect;

                        var dataIsDataSet = data as System.Data.DataSet;
                        var dataIsDataTable = data as System.Data.DataTable;
                        var dataIsDataRow = data as System.Data.DataRow;
                        var dataIsDataRowArray = data as System.Data.DataRow[];


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

                return result;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_PI(SqlCommand command)
        {
            command.CommandText = "INSERT INTO " + Constants.Common.TABLE_CZMST_PI + " (" +
                " [CountEntries], [PONUMBER], [ORD], [ITEMNMBR], [VNDDOCNM], " + 
                " [VNDITNUM], [LOCNCODE], [QTYSHPPD], [QTYPACK], [SERLTNUM], " + 
                " [KOD_SW], [DAT_VYROBY], [DATEDONE], [TIMEDONE], [CZ_CarKod], " + 
                " [REZ_1], [REZ_2], [USER_ID], [GUID], [INPUT_MODE], " + 
                " [ID_TERMINAL], [MJ], [QTYSHPPDMJ], [SKL_ID], [NMBRPAL], " +
                " [WEIGHT], [TYPEPAL], [ITEMCODE], [Expirace], [AttributeToSN]" + 
                " ) VALUES ( " + 
                " @CountEntries, @PONUMBER, @ORD, @ITEMNMBR, @VNDDOCNM, " + 
                " @VNDITNUM, @LOCNCODE, @QTYSHPPD, @QTYPACK, @SERLTNUM, " + 
                " @KOD_SW, @DAT_VYROBY, @DATEDONE, @TIMEDONE, @CZ_CarKod, " + 
                " @REZ_1, @REZ_2, @USER_ID, @GUID, @INPUT_MODE, " +
                " @ID_TERMINAL, @MJ, @QTYSHPPDMJ, @SKL_ID, @NMBRPAL, " +
                " @WEIGHT, @TYPEPAL, @ITEMCODE, @Expirace, @AttributeToSN " + 
                " )";


            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });         
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, SourceColumn = "AttributeToSN", SourceVersion = System.Data.DataRowVersion.Current });
            

        }

        public static void InitializeCommandSelect_PI(SqlCommand command)
        {
            command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_PI;
        }


        #endregion


    }
}
