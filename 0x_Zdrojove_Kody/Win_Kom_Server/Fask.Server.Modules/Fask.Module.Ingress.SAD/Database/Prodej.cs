using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
    public class Prodej
    {
		public static int Update_CZMST_DIH(object data, IngresConnection connection, IngresTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_DIH(commandInsert);
					InitializeCommandSelect_DIH(commandSelect);

					using (var adapter = new IngresDataAdapter())
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

		private static void InitializeCommandInsert_DIH(IngresCommand command)
		{
			command.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_DIH + "(" +
				" Zakazka_ID,Paleta_ID,mena_ID,SKL_ID,CountEntries" +
				" ) VALUES ( " +
				" @Zakazka_ID,@Paleta_ID,@mena_ID,@SKL_ID,@CountEntries" +
				" )";

			command.Parameters.Add(new IngresParameter() { ParameterName = "@Zakazka_ID", DbType = DbType.String, SourceColumn = "Zakazka_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@Paleta_ID", DbType = DbType.String, SourceColumn = "Paleta_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@mena_ID", DbType = DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
		}

		private static void InitializeCommandSelect_DIH(IngresCommand command)
		{
			command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DIH;
		}


		#endregion

		public static int Update_CZMST_DI(object data, IngresConnection connection, IngresTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_CZMST_DI(commandInsert);
					InitializeCommandSelect_CZMST_DI(commandSelect);

					using (var adapter = new IngresDataAdapter())
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

		private static void InitializeCommandInsert_CZMST_DI(IngresCommand command)
		{
			command.CommandText =
				@"INSERT INTO " +
				Fask.SQL.Constants.Common.TABLE_CZMST_DI +
				  "(" + 
				  "  CountEntries, VNDITNUM, CZ_CarKod, ODB_ID, STR_ID " +
				  " , DOC_ID,  DOC_ID2,  SKL_ID,  PRAC_ID,  ITEMNMBR" + 
				  " , ITEMCODE, LOCNCODE, MJ, QTYSHPPD, QTYSHPPDMJ" + 
				  " , QTYPACK, SERLTNUM, TAXAMPIE, AMOUNPIE, WITHTAX" +
				  " , PRICEX, mena_ID, TAXAMPIEM, AMOUNPIEM, mena_IDM" + 
				  " , REZ_1, REZ_2, REZ_3, REZ_4, USER_ID" + 
				  " , DATEDONE, TIMEDONE, GUID, INPUT_MODE, ID_TERMINAL" + 
				  " , LOCNCODEDEST, SKL_ID_DEST, WEIGHT, NMBRPAL, TYPEPAL" + 
				  " , PRINTED, EXPIRACE, AttributeToSN" +
				  " ) VALUES ( " +
				  "  @CountEntries, @VNDITNUM, @CZ_CarKod, @ODB_ID, @STR_ID " +
				  " , @DOC_ID,  @DOC_ID2,  @SKL_ID,  @PRAC_ID,  @ITEMNMBR" +
				  " , @ITEMCODE, @LOCNCODE, @MJ, @QTYSHPPD, @QTYSHPPDMJ" +
				  " , @QTYPACK, @SERLTNUM, @TAXAMPIE, @AMOUNPIE, @WITHTAX" +
				  " , @PRICEX, @mena_ID, @TAXAMPIEM, @AMOUNPIEM, @mena_IDM" +
				  " , @REZ_1, @REZ_2, @REZ_3, @REZ_4, @USER_ID" +
				  " , @DATEDONE, @TIMEDONE, @GUID, @INPUT_MODE, @ID_TERMINAL" +
				  " , @LOCNCODEDEST, @SKL_ID_DEST, @WEIGHT, @NMBRPAL, @TYPEPAL" +
				  " , @PRINTED, @EXPIRACE, @AttributeToSN" +
				" ) ";


			command.Parameters.Add(new IngresParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@STR_ID", DbType = System.Data.DbType.String, SourceColumn = "STR_ID", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@DOC_ID", DbType = System.Data.DbType.String, SourceColumn = "DOC_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@DOC_ID2", DbType = System.Data.DbType.String, SourceColumn = "DOC_ID2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });


			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TAXAMPIE", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXAMPIE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@AMOUNPIE", DbType = System.Data.DbType.Decimal, SourceColumn = "AMOUNPIE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@WITHTAX", DbType = System.Data.DbType.Byte, SourceColumn = "WITHTAX", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@PRICEX", DbType = System.Data.DbType.Byte, SourceColumn = "PRICEX", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TAXAMPIEM", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXAMPIEM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@AMOUNPIEM", DbType = System.Data.DbType.Decimal, SourceColumn = "AMOUNPIEM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@mena_IDM", DbType = System.Data.DbType.String, SourceColumn = "mena_IDM", SourceVersion = System.Data.DataRowVersion.Current });


			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, SourceColumn = "REZ_3", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, SourceColumn = "REZ_4", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@GUID", SourceColumn = "GUID_byte", SourceVersion = System.Data.DataRowVersion.Current, IngresType = IngresType.VarBinary, Size=16 });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
			

			command.Parameters.Add(new IngresParameter() { ParameterName = "@LOCNCODEDEST", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODEDEST", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SKL_ID_DEST", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID_DEST", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@EXPIRACE", DbType = System.Data.DbType.DateTime, SourceColumn = "EXPIRACE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, SourceColumn = "AttributeToSN", SourceVersion = System.Data.DataRowVersion.Current });

		}

		private static void InitializeCommandSelect_CZMST_DI(IngresCommand command)
		{
			command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI;
		}


		#endregion
	}
}

