using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    public class Vyroba_Production_Sources
    {

        public static int Update(object data, string CS)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            int result = 0;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(CS))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    using (var commandUpdate = conn.CreateCommand())
                    using (var commandDelete = conn.CreateCommand())
                    using (var commandSelect = conn.CreateCommand())
                    {

                        commandInsert.Transaction = transaction;
                        commandUpdate.Transaction = transaction;
                        commandDelete.Transaction = transaction;
                        commandSelect.Transaction = transaction;


                        InitializeCommandInsert_Production_Sources(commandInsert);
                        InitializeCommandUpdate_Production_Sources(commandUpdate);
                        InitializeCommandDelete_Production_Sources(commandDelete);
                        InitializeCommandSelect_Production_Sources(commandSelect);

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as DataSet;
                            var dataIsDataTable = data as DataTable;
                            var dataIsDataRow = data as DataRow;
                            var dataIsDataRowArray = data as DataRow[];

                            //if (data is DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new DataRow[] { dataIsDataRow });
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

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    throw exTransaction;
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

        private static void InitializeCommandInsert_Production_Sources(SqlCommand command)
        {

            command.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Production_Sources + " ( " +
                " CountEntries, SOPNUMBE, ITEMNAME, ITEMNMBR, ITEMTYPE, " +
                " SKL_ID, ITEMCODE, LOCNCODE, MJ, QTYSHPPD, " +
                " QTYSHPPDMJ, QTYPACK, SERLTNUM,  GUID_Production, GUID, " +
                " USER_ID, TERMINAL_ID, WEIGHT, NMBRPAL, TYPEPAL, " +
                " PRINTED, ISOK, idVS, dateedit" +
                " ) VALUES ( " +
                " @CountEntries, @SOPNUMBE, @ITEMNAME, @ITEMNMBR, @ITEMTYPE, " +
                " @SKL_ID, @ITEMCODE, @LOCNCODE, @MJ, @QTYSHPPD, " +
                " @QTYSHPPDMJ, @QTYPACK, @SERLTNUM, @GUID_Production, @GUID, " +
                " @USER_ID, @TERMINAL_ID, @WEIGHT, @NMBRPAL, @TYPEPAL, " +
                " @PRINTED, @ISOK, @idVS, @dateedit" +
                " )";

 

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current  }); ;
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNAME", DbType = DbType.String, SourceColumn = "ITEMNAME", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = DbType.String, SourceColumn = "ITEMCODE", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = DbType.String, SourceColumn = "MJ", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID_Production", DbType = DbType.Guid, SourceColumn = "GUID_Production", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = DbType.String, SourceColumn = "USER_ID", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TERMINAL_ID", DbType = DbType.Int32, SourceColumn = "TERMINAL_ID", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = DbType.Byte, SourceColumn = "PRINTED", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current  });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current  });

        }

        private static void InitializeCommandUpdate_Production_Sources(SqlCommand command)
        {

            command.CommandText = @"UPDATE Production_Sources SET " + 
                " CountEntries = @CountEntries, " +
                " SOPNUMBE = @SOPNUMBE, " +
                " ITEMNAME = @ITEMNAME, " +
                " ITEMNMBR = @ITEMNMBR, " +
                " ITEMTYPE = @ITEMTYPE, " +
                " ITEMCODE = @ITEMCODE, " +
                " LOCNCODE = @LOCNCODE, " +
                " SKL_ID = @SKL_ID, " +
                " MJ = @MJ, " +
                " QTYSHPPD = @QTYSHPPD, " +
                " QTYSHPPDMJ = @QTYSHPPDMJ, " +
                " QTYPACK = @QTYPACK, " +
                " SERLTNUM = @SERLTNUM, " +
                " USER_ID = @USER_ID, " +
                " TERMINAL_ID = @TERMINAL_ID, " +
                " WEIGHT = @WEIGHT, " +
                " NMBRPAL = @NMBRPAL, " +
                " TYPEPAL = @TYPEPAL, " +
                " PRINTED = @PRINTED, " +
                " ISOK = @ISOK, " +
                " dateedit = @dateedit, " +
                " idVS = @idVS " +
                " WHERE (GUID = @GUID)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current }); ;
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNAME", DbType = DbType.String, SourceColumn = "ITEMNAME", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = DbType.String, SourceColumn = "ITEMCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = DbType.String, SourceColumn = "MJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });           
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = DbType.String, SourceColumn = "USER_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TERMINAL_ID", DbType = DbType.Int32, SourceColumn = "TERMINAL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = DbType.Byte, SourceColumn = "PRINTED", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_Production_Sources(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Production_Sources + " WHERE (GUID = @Original_GUID)";
            
            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_GUID",
                DbType = DbType.Guid,
                SourceColumn = "GUID",
                SourceVersion = DataRowVersion.Original

            });

        }

        private static void InitializeCommandSelect_Production_Sources(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production_Sources;
        }


        #endregion


    }
}
