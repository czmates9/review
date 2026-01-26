using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro Vyrobu
    /// </summary>
    public class SQLite_Controller_Vyroba : SQLite_Controller
    {


		#region c'tors

		public SQLite_Controller_Vyroba(string SQLiteFilePath_FileName)
            : base(SQLiteFilePath_FileName)
        {
        }

        public SQLite_Controller_Vyroba(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

        #endregion

        public override void Dispose()
        {
			base.Dispose();
        }


		#region Kom Server

		#region Update metody

		#region CZPRO_VPH OK

		internal int Update_CZPRO_VPH(object data, bool use_DexRowID)
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
					InitializeCommandInsert_CZPRO_VPH(commandInsert, use_DexRowID);
					//InitializeCommandUpdate_CZPRO_VPH(commandUpdate);
					//InitializeCommandDelete_CZPRO_VPH(commandDelete);
					InitializeCommandSelect_CZPRO_VPH(commandSelect, use_DexRowID);

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

		public void InitializeCommandInsert_CZPRO_VPH(SQLiteCommand command, bool use_DexRowID)
		{
			command.CommandText = @"INSERT INTO CZPRO_VPH (" +
				" CountEntries, SOPNUMBE, SOPTYPE, " +
				" SOPDESC, VNDDOCNMH, BarcodeH, " +
				" LOCNCODE, DateProd, Rez1, " +
				" Rez2, TermID, LSTMod, USERID ";

			if (use_DexRowID)
				command.CommandText += ", DEX_ROW_ID";

			command.CommandText += " ) VALUES ( " +
				" @CountEntries, @SOPNUMBE, @SOPTYPE," +
				" @SOPDESC, @VNDDOCNMH, @BarcodeH," +
				" @LOCNCODE, @DateProd, @Rez1," +
				" @Rez2, @TermID, @LSTMod, @USERID";

			if (use_DexRowID)
				command.CommandText += ",@DEX_ROW_ID";

			command.CommandText += ")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPTYPE", DbType = System.Data.DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPDESC", DbType = System.Data.DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNMH", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeH", DbType = System.Data.DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateProd", DbType = System.Data.DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Rez1", DbType = System.Data.DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Rez2", DbType = System.Data.DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID", SourceVersion = DataRowVersion.Current });

			if (use_DexRowID)
				command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
		}

		//public void InitializeCommandUpdate_CZPRO_VPH(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZPRO_VPH(SQLiteCommand command)
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

		public void InitializeCommandSelect_CZPRO_VPH(SQLiteCommand command, bool use_DexRowID)
		{
			command.CommandText = "Select * from CZPRO_VPH";
		}


		#endregion

		#endregion

		#region CZPRO_VPP OK

		internal int Update_CZPRO_VPP(object data, bool use_DexRowID)
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
					InitializeCommandInsert_CZPRO_VPP(commandInsert, use_DexRowID);
					//InitializeCommandUpdate_CZPRO_VPP(commandUpdate);
					//InitializeCommandDelete_CZPRO_VPP(commandDelete);
					InitializeCommandSelect_CZPRO_VPP(commandSelect, use_DexRowID);

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

		public void InitializeCommandInsert_CZPRO_VPP(SQLiteCommand command, bool use_DexRowID)
		{
			command.CommandText = @"INSERT INTO [CZPRO_VPP] (" +
				" [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMDESC]," +
				" [ITEMMJ], [VNDDOCNMP], [VNDITNUM], [ORD], [BarcodeP]," +
				" [LOCNCODE], [QTYSHPPD], [QTYPACK], [QTYPACKMJ], [TIMEPREP]," +
				" [TIMEUNIT], [DtProdT], [DtProdL], [SerNumT], [SerNumL]," +
				" [VerT], [VerL], [TermID], [LSTMod], [QTYODVEDENO]," +
				" [CNTODVEDENO], [TIMEMODE], [CZ_REZ1_Track], [CZ_REZ2_Track], [CZ_REZ3_Track]," +
				" [CZ_REZ4_Track], [CZ_REZ5_Track], [WEIGHT_TARA], [WEIGHT_NETTO], [WEIGHT_TOL_PLUS], [WEIGHT_TOL_MINUS], [BarcodeT] ";

			if (use_DexRowID)
				command.CommandText += ", [DEX_ROW_ID]";

			command.CommandText += " ) VALUES ( " +
				" @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC," +
				" @ITEMMJ, @VNDDOCNMP, @VNDITNUM, @ORD, @BarcodeP," +
				" @LOCNCODE, @QTYSHPPD, @QTYPACK, @QTYPACKMJ, @TIMEPREP," +
				" @TIMEUNIT, @DtProdT, @DtProdL, @SerNumT, @SerNumL," +
				" @VerT, @VerL, @TermID, @LSTMod, @QTYODVEDENO," +
				" @CNTODVEDENO, @TIMEMODE, @CZ_REZ1_Track, @CZ_REZ2_Track, @CZ_REZ3_Track," +
				" @CZ_REZ4_Track, @CZ_REZ5_Track, @WEIGHT_TARA, @WEIGHT_NETTO, @WEIGHT_TOL_PLUS, @WEIGHT_TOL_MINUS, @BarcodeT ";

			if (use_DexRowID)
				command.CommandText += ",@DEX_ROW_ID";

			command.CommandText += ")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNMP", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DtProdT", DbType = System.Data.DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DtProdL", DbType = System.Data.DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SerNumT", DbType = System.Data.DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SerNumL", DbType = System.Data.DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VerT", DbType = System.Data.DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VerL", DbType = System.Data.DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Int16, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYODVEDENO", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYODVEDENO", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CNTODVEDENO", DbType = System.Data.DbType.Decimal, SourceColumn = "CNTODVEDENO", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current  });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ3_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ4_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ5_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ5_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_TARA", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TARA", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_NETTO", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_NETTO", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_TOL_PLUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_PLUS", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_TOL_MINUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_MINUS", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeT", DbType = System.Data.DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Current });

			if (use_DexRowID)
				command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
		}

		//public void InitializeCommandUpdate_CZPRO_VPP(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZPRO_VPP(SQLiteCommand command)
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

		public void InitializeCommandSelect_CZPRO_VPP(SQLiteCommand command, bool use_DexRowID)
		{
			command.CommandText = "Select * from CZPRO_VPP";
		}


		#endregion

		#endregion

		#region Corrects OK

		internal int Update_Corrects(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Corrects(commandInsert);
					InitializeCommandUpdate_Corrects(commandUpdate);
					InitializeCommandDelete_Corrects(commandDelete);
					InitializeCommandSelect_Corrects(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Corrects(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO Corrects (" + 
				" id, [desc], TMFrom, TMTo, Production, ProductionType" + 
				") VALUES (" + 
				"@id, @desc, @TMFrom, @TMTo, @Production, @ProductionType" + 
				")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@desc", DbType = System.Data.DbType.String, SourceColumn = "desc", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TMFrom", DbType = System.Data.DbType.Single, SourceColumn = "TMFrom", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TMTo", DbType = System.Data.DbType.Single, SourceColumn = "TMTo", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Production", DbType = System.Data.DbType.Byte, SourceColumn = "Production", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ProductionType", DbType = System.Data.DbType.Byte, SourceColumn = "ProductionType", SourceVersion = DataRowVersion.Current });

		}

		public void InitializeCommandUpdate_Corrects(SQLiteCommand command)
		{
			command.CommandText = "UPDATE Corrects SET " + 
				"[desc] = @desc, TMFrom = @TMFrom, TMTo = @TMTo, Production = @Production, ProductionType = @ProductionType " + 
				" WHERE (id = @id)"; 
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@desc", DbType = System.Data.DbType.String, SourceColumn = "desc", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TMFrom", DbType = System.Data.DbType.Single, SourceColumn = "TMFrom", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TMTo", DbType = System.Data.DbType.Single, SourceColumn = "TMTo", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Production", DbType = System.Data.DbType.Byte, SourceColumn = "Production", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ProductionType", DbType = System.Data.DbType.Byte, SourceColumn = "ProductionType", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

		}

		public void InitializeCommandDelete_Corrects(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM Corrects WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_Corrects(SQLiteCommand command)
		{
			command.CommandText = "Select ROWID as id, * from Corrects";
		}

		#endregion

		#endregion

		#region Logins OK

		internal int Update_Logins(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Logins(commandInsert);
					InitializeCommandUpdate_Logins(commandUpdate);
					InitializeCommandDelete_Logins(commandDelete);
					InitializeCommandSelect_Logins(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Logins(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO Logins (" + 
				"id, firstname, surname, psswd, VS" + 
				") VALUES (" + 
				"@id, @firstname, @surname, @psswd, @VS" + 
				")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@firstname", DbType = System.Data.DbType.String, SourceColumn = "firstname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@surname", DbType = System.Data.DbType.String, SourceColumn = "surname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@psswd", DbType = System.Data.DbType.String, SourceColumn = "psswd", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VS", DbType = System.Data.DbType.Byte, SourceColumn = "VS", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_Logins(SQLiteCommand command)
		{
			command.CommandText = "UPDATE Logins SET " + 
				" id = @id, firstname = @firstname, surname = @surname, psswd = @psswd, VS = @VS " + 
				" WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@firstname", DbType = System.Data.DbType.String, SourceColumn = "firstname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@surname", DbType = System.Data.DbType.String, SourceColumn = "surname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@psswd", DbType = System.Data.DbType.String, SourceColumn = "psswd", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VS", DbType = System.Data.DbType.Byte, SourceColumn = "VS", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandDelete_Logins(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM Logins WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.String,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_Logins(SQLiteCommand command)
		{
			command.CommandText = "Select * from Logins";
		}


		#endregion

		#endregion

		#region Machines OK

		internal int Update_Machines(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Machines(commandInsert);
					InitializeCommandUpdate_Machines(commandUpdate);
					InitializeCommandDelete_Machines(commandDelete);
					InitializeCommandSelect_Machines(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Machines(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [Machines] ([id], [name], [description]" + 
				") VALUES (" +
				"@id, @name, @description" + 
				")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", DbType = System.Data.DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_Machines(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [Machines] SET " +
				" [id] = @id, [name] = @name, [description] = @description " +
				" WHERE (([id] = @id_orig))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", DbType = System.Data.DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id_orig", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_Machines(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM Machines WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.String,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_Machines(SQLiteCommand command)
		{
			command.CommandText = "Select * from Machines";
		}


		#endregion

		#endregion

		#region Operations OK

		internal int Update_Operations(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Operations(commandInsert);
					InitializeCommandUpdate_Operations(commandUpdate);
					InitializeCommandDelete_Operations(commandDelete);
					InitializeCommandSelect_Operations(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Operations(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [Operations] ( " + 
				"[id], [name], [description]" + 
				") VALUES (" +
				"@id, @name, @description" + 
				")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", DbType = System.Data.DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });

		}

		public void InitializeCommandUpdate_Operations(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [Operations] SET" +
				" [id] = @id, [name] = @name, [description] = @description " +
				" WHERE (([id] = @id_orig))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", DbType = System.Data.DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id_orig", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_Operations(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM Operations WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.String,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_Operations(SQLiteCommand command)
		{
			command.CommandText = "Select * from Operations";
		}


		#endregion

		#endregion

		#region VMachinesOperations OK

		internal int Update_VMachinesOperations(object data)
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
					InitializeCommandInsert_VMachinesOperations(commandInsert);
					//InitializeCommandUpdate_VMachinesOperations(commandUpdate);
					//InitializeCommandDelete_VMachinesOperations(commandDelete);
					InitializeCommandSelect_VMachinesOperations(commandSelect);

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

		public void InitializeCommandInsert_VMachinesOperations(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [VMachinesOperations] (" + 
				" [machineid], [operationid] " + 
				" ) VALUES (" +
				" @machineid, @operationid" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@operationid", DbType = System.Data.DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });

		}

		//public void InitializeCommandUpdate_VMachinesOperations(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_VMachinesOperations(SQLiteCommand command)
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

		public void InitializeCommandSelect_VMachinesOperations(SQLiteCommand command)
		{
			command.CommandText = "Select * from VMachinesOperations";
		}


		#endregion

		#endregion

		#region StatusTypes OK

		internal int Update_StatusTypes(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_StatusTypes(commandInsert);
					InitializeCommandUpdate_StatusTypes(commandUpdate);
					InitializeCommandDelete_StatusTypes(commandDelete);
					InitializeCommandSelect_StatusTypes(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_StatusTypes(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [StatusTypes] (" + 
				" [statusid], [statusdesc]" +
				" ) VALUES (" +
				" @statusid, @statusdesc" + 
				" )"; 

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = System.Data.DbType.String, SourceColumn = "statusid", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusdesc", DbType = System.Data.DbType.String, SourceColumn = "statusdesc", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_StatusTypes(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [StatusTypes] SET " +
				" [statusid] = @statusid," +
				" [statusdesc] = @statusdesc " + 
				" WHERE (([statusid] = @statusid_orig))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = System.Data.DbType.String, SourceColumn = "statusid", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusdesc", DbType = System.Data.DbType.String, SourceColumn = "statusdesc", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid_orig", DbType = System.Data.DbType.String, SourceColumn = "statusid", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_StatusTypes(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM StatusTypes WHERE (statusid = @statusid)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@statusid",
				DbType = System.Data.DbType.String,
				SourceColumn = "statusid",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_StatusTypes(SQLiteCommand command)
		{
			command.CommandText = "Select * from StatusTypes";
		}


		#endregion

		#endregion

		#region FASK_CONS_095 OK

		internal int Update_FASK_CONS_095(object data)
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
					InitializeCommandInsert_FASK_CONS_095(commandInsert);
					//InitializeCommandUpdate_FASK_CONS_095(commandUpdate);
					//InitializeCommandDelete_FASK_CONS_095(commandDelete);
					InitializeCommandSelect_FASK_CONS_095(commandSelect);

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

		public void InitializeCommandInsert_FASK_CONS_095(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO FASK_CONS_095 ( " + 
				" ITEMNMBR, ITEMDESC, VNDITNUM, CZ_CarKod, LOCNCODE," + 
				" SKL_ID, QTY, QTYPACK, MJ, DMJ," + 
				" TAXRATE, PRICE0, PRICE1, PRICE2, PRICE3, " + 
				" PRICE4, PRICE5, CZ_SerNum_Track, CZ_SerNum_Delka, CZ_Rez1_Track," + 
				" CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track, REZ1, ITEMCODE," + 
				" ODB_ID, TIMEMODE, TIMEPREP, TIMEUNIT, TIMEFROM," + 
				" TIMETO, LSTMod, loginid " + 
				" ) VALUES ( " + 
				" @ITEMNMBR, @ITEMDESC, @VNDITNUM, @CZ_CarKod, @LOCNCODE," + 
				" @SKL_ID, @QTY, @QTYPACK, @MJ, @DMJ, " + 
				" @TAXRATE, @PRICE0, @PRICE1, @PRICE2, @PRICE3," + 
				" @PRICE4, @PRICE5, @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_Rez1_Track," + 
				" @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track, @REZ1, @ITEMCODE," + 
				" @ODB_ID, @TIMEMODE, @TIMEPREP, @TIMEUNIT, @TIMEFROM," + 
				" @TIMETO, @LSTMod, @loginid)";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, SourceColumn = "DMJ" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXRATE" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE0" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE1" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE2" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE3" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE4" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE5" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka" , SourceVersion = DataRowVersion.Current });
		    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez1_Track" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez2_Track" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez3_Track" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez4_Track" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "REZ1" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEFROM" , SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMETO" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod" , SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "loginid" , SourceVersion = DataRowVersion.Current });

		}

		//public void InitializeCommandUpdate_FASK_CONS_095(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_FASK_CONS_095(SQLiteCommand command)
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

		public void InitializeCommandSelect_FASK_CONS_095(SQLiteCommand command)
		{
			command.CommandText = "Select * from FASK_CONS_095";
		}


		#endregion

		#endregion

		#region CZMST093 OK

		internal int Update_CZMST093(object data)
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
					InitializeCommandInsert_CZMST093(commandInsert);
					//InitializeCommandUpdate_CZMST093(commandUpdate);
					//InitializeCommandDelete_CZMST093(commandDelete);
					InitializeCommandSelect_CZMST093(commandSelect);

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

		public void InitializeCommandInsert_CZMST093(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST093 (" + 
				" skl_id, skl_desc, skl_typ, skl_carcode, DEX_ROW_ID " +
				" ) VALUES (" + 
				" @skl_id, @skl_desc, @skl_typ, @skl_carcode, @DEX_ROW_ID " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, SourceColumn = "skl_id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_desc", DbType = System.Data.DbType.String, SourceColumn = "skl_desc", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_typ", DbType = System.Data.DbType.String, SourceColumn = "skl_typ", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_carcode", DbType = System.Data.DbType.String, SourceColumn = "skl_carcode", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
		}

		//public void InitializeCommandUpdate_CZMST093(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZMST093(SQLiteCommand command)
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

		public void InitializeCommandSelect_CZMST093(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST093";
		}


		#endregion

		#endregion

		#region CZMST094

		internal int Update_CZMST094(object data)
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
					InitializeCommandInsert_CZMST094(commandInsert);
					//InitializeCommandUpdate_CZMST094(commandUpdate);
					//InitializeCommandDelete_CZMST094(commandDelete);
					InitializeCommandSelect_CZMST094(commandSelect);

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

		public void InitializeCommandInsert_CZMST094(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST094 (" + 
				" SKL_ID, LOCNCODE, TYPE, Description, Barcode, DEX_ROW_ID " + 
				" ) VALUES ( " + 
				" @SKL_ID, @LOCNCODE, @TYPE, @Description, @Barcode, @DEX_ROW_ID " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
		}

		//public void InitializeCommandUpdate_CZMST094(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZMST094(SQLiteCommand command)
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

		public void InitializeCommandSelect_CZMST094(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST094";
		}


		#endregion

		#endregion

		#region FASK_Vyroba_TP

		internal int Update_FASK_Vyroba_TP(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_FASK_Vyroba_TP(commandInsert);
					InitializeCommandUpdate_FASK_Vyroba_TP(commandUpdate);
					InitializeCommandDelete_FASK_Vyroba_TP(commandDelete);
					InitializeCommandSelect_FASK_Vyroba_TP(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_FASK_Vyroba_TP(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO FASK_Vyroba_TP " +
				" (ID_H, " +
				" ID_L," +
				" ITEMNMBR_Def," +
				" DESC_Def," +
				" MJ_Def," +
				" ITEMNMBR_fol," +
				" DESC_Fol," +
				" MJ_Fol," +
				" koef," +
				" ID_USER," +
				" dateedit," +
				" ID," +
				" [alter]," +
				" PUO) " +
				" VALUES (@ID_H,@ID_L,@ITEMNMBR_Def,@DESC_Def,@MJ_Def,@ITEMNMBR_fol,@DESC_Fol,@MJ_Fol,@koef,@ID_USER,@dateedit,@ID,@alter,@PUO)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_H", DbType = System.Data.DbType.String, SourceColumn = "ID_H", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_L", DbType = System.Data.DbType.String, SourceColumn = "ID_L", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR_Def", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DESC_Def", DbType = System.Data.DbType.String, SourceColumn = "DESC_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ_Def", DbType = System.Data.DbType.String, SourceColumn = "MJ_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR_fol", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR_fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DESC_Fol", DbType = System.Data.DbType.String, SourceColumn = "DESC_Fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ_Fol", DbType = System.Data.DbType.String, SourceColumn = "MJ_Fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@koef", DbType = System.Data.DbType.String, SourceColumn = "koef", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_USER", DbType = System.Data.DbType.String, SourceColumn = "ID_USER", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateedit", DbType = System.Data.DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@alter", DbType = System.Data.DbType.String, SourceColumn = "alter", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PUO", DbType = System.Data.DbType.String, SourceColumn = "PUO", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_FASK_Vyroba_TP(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE FASK_Vyroba_TP " +
				" SET ID_H = @ID_H," +
				" ID_L = @ID_L," +
				" ITEMNMBR_Def = @ITEMNMBR_Def," +
				" PUO = @PUO," +
				" [alter] = @alter," +
				" dateedit = @dateedit," +
				" ID_USER = @ID_USER," +
				" koef = @koef," +
				" MJ_Fol = @MJ_Fol," +
				" DESC_Fol = @DESC_Fol," +
				" ITEMNMBR_fol = @ITEMNMBR_fol," +
				" MJ_Def = @MJ_Def," +
				" DESC_Def = @DESC_Def " +
				" WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_H", DbType = System.Data.DbType.String, SourceColumn = "ID_H", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_L", DbType = System.Data.DbType.String, SourceColumn = "ID_L", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR_Def", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PUO", DbType = System.Data.DbType.String, SourceColumn = "PUO", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@alter", DbType = System.Data.DbType.String, SourceColumn = "alter", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateedit", DbType = System.Data.DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_USER", DbType = System.Data.DbType.String, SourceColumn = "ID_USER", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@koef", DbType = System.Data.DbType.String, SourceColumn = "koef", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ_Fol", DbType = System.Data.DbType.String, SourceColumn = "MJ_Fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DESC_Fol", DbType = System.Data.DbType.String, SourceColumn = "DESC_Fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR_fol", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR_fol", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ_Def", DbType = System.Data.DbType.String, SourceColumn = "MJ_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DESC_Def", DbType = System.Data.DbType.String, SourceColumn = "DESC_Def", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID", SourceVersion = DataRowVersion.Original});
		}

		public void InitializeCommandDelete_FASK_Vyroba_TP(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM FASK_Vyroba_TP WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_FASK_Vyroba_TP(SQLiteCommand command)
		{
			command.CommandText = "Select * from FASK_Vyroba_TP";
		}


		#endregion

		#endregion

		#endregion

		#region Fill metody

		internal int Fill_Production(DataSets.Vyroba.ProductionDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Production ";
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

		internal int Fill_Production_Sources(DataSets.Vyroba.Production_SourcesDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Production_Sources ";

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
				// Další obecná výjimka
				Logging.ExceptionHandler2.Handle(ex);
				throw;
			}
			finally
			{
				Connection_Close();
			}
		}

	

		internal int Fill_UserEvents(DataSets.Vyroba.UserEventsDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id,* FROM UserEvents ";
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

		internal int Fill_Production_SN(DataSets.Vyroba.Production_SNDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Production_SN ";
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

		#endregion

		#endregion

		public void Void_Command()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "";
					
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

		public int Reindexace_Command(string Command)
		{
			try
			{
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = Command;
					int rowsaff = command.ExecuteNonQuery();
					return rowsaff;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		#region Vyroba_P

		#region Corrects

		public Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable GetDataByProduction_Corrects(byte Production)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Corrects WHERE Production = @Production";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Production", DbType = System.Data.DbType.Byte, Value = Production });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable GetDataByID_Corrects(int id)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Corrects WHERE id = @id";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, Value = id });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public int Fill_Corrects(Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Corrects";

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

		#endregion

		#region CZMST093

		public Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable GetDataBySklid_CZMST093(string SKL_ID)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST093 WHERE SKL_ID = @SKL_ID";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable GetDataByBarcode_CZMST093(string skl_carcode)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable();

				Connection_Open();

                #region old 22.10.2024
                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM CZMST093 WHERE (skl_carcode = @skl_carcode)";

                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_carcode", DbType = System.Data.DbType.String, Value = skl_carcode == null ? (object)DBNull.Value : skl_carcode });
                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        int returnValue = adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                #endregion

                #region new 22.10.2024
                //using (var command = this.Connection.CreateCommand())
                //{
                //	command.CommandText = "SELECT * FROM CZMST093 WHERE (skl_id = @skl_id)";

                //	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, Value = skl_carcode == null ? (object)DBNull.Value : skl_carcode });
                //	using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                //	{
                //		adapter.SelectCommand = command;
                //		int returnValue = adapter.Fill(dataTable);
                //		return dataTable;
                //	}
                //}
                #endregion


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

		#endregion

		#region CZMST094

		public Fask.SQLiteDBs.DataSets.Vyroba.CZMST094DataTable GetDataBySklidLocncode_CZMST094(string SKL_ID, string LOCNCODE)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZMST094DataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST094 WHERE SKL_ID = @SKL_ID AND LOCNCODE = @LOCNCODE";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE }); 
					
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())

					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZMST094DataTable GetDataBySklidBarcode_CZMST094(string SKL_ID, string Barcode)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZMST094DataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST094 WHERE SKL_ID = @SKL_ID AND Barcode = @Barcode";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, Value = Barcode == null ? (object)DBNull.Value : Barcode });
					
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		#endregion

		#region CZPRO_VPH

		public int Fill_CZPRO_VPH(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dataTable)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZPRO_VPH";

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


		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable GetData_CZPRO_VPH()
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZPRO_VPH";

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public int FillBySOPNUMBE_CZPRO_VPH(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dataTable,string SOPNUMBE)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZPRO_VPH WHERE SOPNUMBE = @SOPNUMBE";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByBarcodeH_CZPRO_VPH(string BarcodeH)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZPRO_VPH WHERE (BarcodeH =@BarcodeH)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeH", DbType = System.Data.DbType.String, Value = BarcodeH == null ? (object)DBNull.Value : BarcodeH });
					
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSopnumbe_CZPRO_VPH(int CountEntries, string SOPNUMBE)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZPRO_VPH WHERE CountEntries = @CountEntries AND SOPNUMBE = @SOPNUMBE";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries  });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public int BlokovatPrikazByCountEntriesSopnumbe_CZPRO_VPH(byte TermID, int CountEntries, string SOPNUMBE)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "UPDATE CZPRO_VPH SET TermID = @TermID " + 
						"WHERE CountEntries = @CountEntries) and (SOPNUMBE = @SOPNUMBE) ";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, Value = TermID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries , SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE, SourceVersion = DataRowVersion.Original });

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		#endregion

		#region CZPRO_VPP

		public int UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(decimal QTYODVEDENO, decimal CNTODVEDENO, System.DateTime LSTMod, int CountEntries, string SOPNUMBE, int ORD, string ITEMNMBR, string ITEMTYPE, decimal QTYPACK, string BarcodeP)
		{
			try
			{
			
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"UPDATE CZPRO_VPP SET" + 
						" QTYODVEDENO = QTYODVEDENO + @QTYODVEDENO," + 
						" CNTODVEDENO = CNTODVEDENO + @CNTODVEDENO," +
						" LSTMod = @LSTMod" +
						" WHERE (CountEntries = @CountEntries)" + 
						" AND (SOPNUMBE = @SOPNUMBE)" +
						" AND (ORD = @ORD)" + 
						" AND (ITEMNMBR = @ITEMNMBR)" +
						" AND (ITEMTYPE = @ITEMTYPE)" +
						" AND  (QTYPACK = @QTYPACK)" +
						" AND  (BarcodeP = @BarcodeP)" +
						" AND LSTMod < @LSTMod" +
						"";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYODVEDENO", DbType = System.Data.DbType.Decimal, Value = QTYODVEDENO });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CNTODVEDENO", DbType = System.Data.DbType.Decimal, Value = CNTODVEDENO });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime , Value = LSTMod });

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, Value = ITEMTYPE == null ? (object)DBNull.Value : ITEMTYPE, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK, SourceVersion = DataRowVersion.Original });

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP, SourceVersion = DataRowVersion.Original });
					

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
			
			}
		}

		public int BlokovatOperaceByCountEntriesSopnumbe_CZPRO_VPP(byte TermID, int CountEntries, string SOPNUMBE)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "UPDATE CZPRO_VPP SET TermID = @TermID " + 
						" WHERE (CountEntries = @CountEntries) AND (SOPNUMBE = @SOPNUMBE)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, Value = TermID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE, SourceVersion = DataRowVersion.Original });

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByBarcodeP_CZPRO_VPP(string BarcodeP)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();
			FillByBarcodeP_CZPRO_VPP(dataTable, BarcodeP);
			return dataTable;
		}

		public int FillByBarcodeP_CZPRO_VPP(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dataTable, string BarcodeP)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZPRO_VPP WHERE (BarcodeP = @BarcodeP)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP == null ? (object)DBNull.Value : BarcodeP });

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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByBarcodePandSOPNUMBEandCountEntries_CZPRO_VPP(string BarcodeP, string SOPNUMBE, int CountEntries)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZPRO_VPP WHERE BarcodeP = @BarcodeP AND SOPNUMBE = @SOPNUMBE AND CountEntries = @CountEntries";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP == null ? (object)DBNull.Value : BarcodeP });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries  });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbe_CZPRO_VPP(int CountEntries, string SOPNUMBE)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZPRO_VPP WHERE SOPNUMBE = @SOPNUMBE AND CountEntries = @CountEntries";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		#endregion

		#region CZMST_095

		public int FillByBarcode_CZMST_095(Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dataTable, string barcode)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
                    if (!string.IsNullOrEmpty(barcode))
                    {
                        command.CommandText = @"SELECT * FROM FASK_CONS_095 WHERE (VNDITNUM = @barcode) " +
                                        " UNION " +
                                        " SELECT * FROM FASK_CONS_095 AS FASK_CONS_095_1 WHERE (CZ_CarKod = @barcode)";

                        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@barcode", DbType = System.Data.DbType.String, Value = barcode == null ? (object)DBNull.Value : barcode });

                    }
                    else
                    {
						command.CommandText = @"SELECT * FROM FASK_CONS_095 ";

					}

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

		public int FillByITEMNMBR_CZMST_095(bool ClearBeforeFill, Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dataTable, string ITEMNMBR)
		{
			try
			{
				if(ClearBeforeFill)
					dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM FASK_CONS_095 WHERE ITEMNMBR = @ITEMNMBR";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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


		public int SKL_ID_ITEMNMBR_CZMST_095( Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dataTable, string ITEMNMBR, string VNDITNUM)
		{
			try
			{
				
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM FASK_CONS_095 WHERE ITEMNMBR = @ITEMNMBR AND VNDITNUM = @VNDITNUM";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });

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


		#endregion

		#region Logins

		public int Fill_Logins(Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Logins";

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

		public Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable GetDataByID_Logins(string ID)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Logins WHERE ID = @ID";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String , Value = ID == null ? (object)DBNull.Value : ID });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		#endregion

		#region Machines

		public int Fill_Machines(Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Machines";

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

		public Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable GetDataByID_Machines(string ID)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Machines WHERE ID = @ID";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		#endregion

		#region Operations

		public int Fill_Operations(Fask.SQLiteDBs.DataSets.Vyroba.OperationsDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Operations";

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

		#endregion

		#region Production

		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable GetDataByUserIDMachineIDNULL_Production(string UserID)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
			FillByUserIDMachineIDNULL_Production(dataTable, UserID);
			return dataTable;
		}

		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable GetDataByUserIDMachineID_Production(string UserID, string MachineID)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
			FillByUserIDMachineID_Production(dataTable, UserID, MachineID);
			return dataTable;
		}

		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable GetDataByUserIDMachineID_R2_Production(string UserID, string MachineID)
		{
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dataTable = new DataSets.Vyroba.ProductionDataTable();
			try
			{
		
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT ROWID as id, * FROM Production WHERE (UserID = @UserID) AND (machineid = @MachineID) ORDER BY dateeve DESC LIMIT 1";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MachineID", DbType = System.Data.DbType.String, Value = MachineID == null ? (object)DBNull.Value : MachineID });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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


		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable GetDataByUserID_Production(string UserID)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT ROWID as id,* FROM Production WHERE (UserID = @UserID) ORDER BY dateeve DESC LIMIT 1";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable GetData_Production()
		{ 
			var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
			Fill_Production(dataTable);
			return dataTable;
		}


		//MaR vratit zmeny
		public int FillByUserIDMachineID_Production(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dataTable, string UserID, string MachineID)
		{
            try
            {
                dataTable.Clear();
                Connection_Open();



				using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = @"SELECT ROWID as id, * FROM Production WHERE (UserID = @UserID) AND (machineid = @MachineID) ORDER BY dateeve DESC";
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MachineID", DbType = System.Data.DbType.String, Value = MachineID == null ? (object)DBNull.Value : MachineID });



                    //command.CommandText = @"SELECT * FROM Production"; ;
                    //               command.CommandText += " WHERE (UserID = " + UserID + " )";
                    //               command.CommandText += " AND (machineid = " + MachineID + " )";
                    //               command.CommandText += " ORDER BY dateeve DESC";


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

		public int FillByUserIDMachineID_Production_zal(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dataTable, string UserID, string MachineID)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();

				var datatable2 = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();


				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM Production";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MachineID", DbType = System.Data.DbType.String, Value = MachineID == null ? (object)DBNull.Value : MachineID });



					//command.CommandText = @"SELECT * FROM Production"; ;
					//               command.CommandText += " WHERE (UserID = " + UserID + " )";
					//               command.CommandText += " AND (machineid = " + MachineID + " )";
					//               command.CommandText += " ORDER BY dateeve DESC";


					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						// int returnValue = adapter.Fill(dataTable);
						int returnValue = adapter.Fill(datatable2);
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




		public int FillByUserIDMachineIDNULL_Production(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dataTable, string UserID)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT ROWID as id, * FROM Production WHERE (UserID = @UserID) AND (machineid IS NULL) ORDER BY dateeve DESC LIMIT 1";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

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

		public int FillByBarcodePUserIDdateeve_Production(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dataTable, string BarcodeP, string UserID, System.DateTime dateeve)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT ROWID as id, * FROM Production WHERE (BarcodeP = @BarcodeP) AND (UserID = @UserID) AND (dateeve = @dateeve)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP == null ? (object)DBNull.Value : BarcodeP });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.String, Value = dateeve  });

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

		public int DeleteOlderThanDateeve_Production(System.DateTime datum)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM Production where dateeve < @datum";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@datum", DbType = System.Data.DbType.DateTime, Value = datum });

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public int DeleteProductionByUserID_Production(string UserID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM Production WHERE TIMECRID is null and (UserID = @UserID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public int DeleteCorrectionByUserID_Production(string UserID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM Production WHERE TIMECRID is not null AND (UserID = @UserID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public int? NotUploadedCnt_Production(int? Countentries, string SOPNUMBE)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM Production WHERE Countentries=@Countentries and SOPNUMBE=@SOPNUMBE";
					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Countentries", DbType = System.Data.DbType.Int32, Value = Countentries.HasValue ? Countentries.Value : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int)pp;
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

		public decimal? QTY_Production( int? CountEntries, string SOPNUMBE, int? ORD, string ITEMNMBR, string ITEMTYPE, decimal? QTYPACK, System.DateTime dateeve, string BarcodeP)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = @"SELECT SUM(qty) FROM Production " + 
						" WHERE " +
						" (CountEntries = @CountEntries)" + 
						" AND (SOPNUMBE = @SOPNUMBE)" + 
						" AND (ORD = @ORD)" + 
						" AND (ITEMNMBR = @ITEMNMBR)" + 
						" AND (ITEMTYPE = @itemtype)" +
						" AND (QTYPACK = @QTYPACK)" + 
						" AND (dateeve > @dateeve)" +
						" AND (BarcodeP = @BarcodeP)" +
						" GROUP BY CountEntries, SOPNUMBE, ORD, ITEMNMBR, ITEMTYPE, QTYPACK, BarcodeP";

					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries.HasValue ? CountEntries.Value : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD.HasValue ? ORD.Value : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, Value = ITEMTYPE == null ? (object)DBNull.Value : ITEMTYPE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Int32, Value = QTYPACK.HasValue ? QTYPACK.Value : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = dateeve });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP == null ? (object)DBNull.Value : BarcodeP });

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						decimal pp = Convert.ToDecimal(returnValue);
						return pp;
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

		#region Update

		internal int Update_Production(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Production(commandInsert);
					InitializeCommandUpdate_Production(commandUpdate);
					//InitializeCommandDelete_Production(commandDelete);
					InitializeCommandSelect_Production(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Production(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [Production] (" + 
				"[CountEntries]," +
				" [SOPNUMBE]," +
				" [ITEMNMBR]," +
				" [ITEMTYPE]," +
				" [ITEMMJ]," +
				" [ORD]," +
				" [TIMEPREP]," +
				" [TIMEUNIT]," +
				" [TIMESTART]," +
				" [TIMESTOP]," +
				" [TIMECOR]," +
				" [TIMECRID]," +
				" [loginid]," +
				" [machineid]," +
				" [dateeve]," +
				" [qty]," +
				" [qtyReal]," +
				" [QTYPACK]," +
				" [QTYPACKMJ]," +
				" [description]," +
				" [BarcodeP]," +
				" [UserID]," +
				" [TermID]," +
				" [ISOK]," +
				" [GUID]," +
				" [TIMEMODE]," +
				" [TIMEPREPSTART]," +
				" [TIMEPREPSTOP]," +
				" [TIMECORSTART]," +
				" [TIMECORSTOP]," +
				" [operationid]," +
				" [SOUBEHGUID]," +
				" [CORRGUID]," +
				" [TIMECRIDTYPE]," +
				" [SKL_ID]," +
				" [LOCNCODE]," +
				" [SERLTNUM]," +
				" [ITEMDESC]," +
				" [NMBRPAL]," +
				" [TYPEPAL]," +
				" [PackType]," +
				" [status]," +
				" [WEIGHT]," +
				" [STORNOGUID], " +
				" [REZ_1], " +
				" [REZ_2], " +
				" [REZ_3], " +
				" [REZ_4], " +
				" [REZ_5], " +
				" [WEIGHT_OLD] " +
				")" +
				" VALUES " +
				"(@CountEntries," +
				" @SOPNUMBE," +
				" @ITEMNMBR," +
				" @ITEMTYPE," +
				" @ITEMMJ," +
				" @ORD," +
				" @TIMEPREP," +
				" @TIMEUNIT," +
				" @TIMESTART," +
				" @TIMESTOP," +
				" @TIMECOR," +
				" @TIMECRID," +
				" @loginid," +
				" @machineid," +
				" @dateeve," +
				" @qty," +
				" @qtyReal," +
				" @QTYPACK," +
				" @QTYPACKMJ," +
				" @description," +
				" @BarcodeP," +
				" @UserID," +
				" @TermID," +
				" @ISOK," +
				" @GUID," +
				" @TIMEMODE," +
				" @TIMEPREPSTART," +
				" @TIMEPREPSTOP," +
				" @TIMECORSTART," +
				" @TIMECORSTOP," +
				" @operationid," +
				" @SOUBEHGUID," +
				" @CORRGUID," +
				" @TIMECRIDTYPE," +
				" @SKL_ID," +
				" @LOCNCODE," +
				" @SERLTNUM," +
				" @ITEMDESC," +
				" @NMBRPAL," +
				" @TYPEPAL," +
				" @PackType," +
				" @status," +
				" @WEIGHT," +
				" @STORNOGUID," +
				" @REZ_1, " +
				" @REZ_2, " +
				" @REZ_3, " +
				" @REZ_4, " +
				" @REZ_5, " +
				" @WEIGHT_OLD " +
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMESTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMESTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMESTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMESTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECOR", DbType = System.Data.DbType.Single, SourceColumn = "TIMECOR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECRID", DbType = System.Data.DbType.Int32, SourceColumn = "TIMECRID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "loginid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "machineid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, SourceColumn = "dateeve" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@qty", DbType = System.Data.DbType.Decimal, SourceColumn = "qty" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@qtyReal", DbType = System.Data.DbType.Decimal, SourceColumn = "qtyReal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "UserID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "TermID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ISOK", DbType = System.Data.DbType.DateTime, SourceColumn = "ISOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREPSTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEPREPSTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREPSTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEPREPSTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECORSTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMECORSTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECORSTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMECORSTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@operationid", DbType = System.Data.DbType.String, SourceColumn = "operationid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOUBEHGUID", DbType = System.Data.DbType.Guid, SourceColumn = "SOUBEHGUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CORRGUID", DbType = System.Data.DbType.Guid, SourceColumn = "CORRGUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECRIDTYPE", DbType = System.Data.DbType.Byte, SourceColumn = "TIMECRIDTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PackType", DbType = System.Data.DbType.String, SourceColumn = "PackType" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@status", DbType = System.Data.DbType.Int32, SourceColumn = "status" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STORNOGUID", DbType = System.Data.DbType.Guid, SourceColumn = "STORNOGUID" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, SourceColumn = "REZ_3" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, SourceColumn = "REZ_4" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_5", DbType = System.Data.DbType.String, SourceColumn = "REZ_5" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_OLD", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_OLD" });

		}

		public void InitializeCommandUpdate_Production(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE [Production] SET" +
				" [CountEntries] = @CountEntries," +
				" [SOPNUMBE] = @SOPNUMBE," +
				" [ITEMNMBR] = @ITEMNMBR," +
				" [ITEMTYPE] = @ITEMTYPE," +
				" [ITEMMJ] = @ITEMMJ," +
				" [ORD] = @ORD," +
				" [TIMEPREP] = @TIMEPREP," +
				" [TIMEUNIT] = @TIMEUNIT," +
				" [TIMESTART] = @TIMESTART," +
				" [TIMESTOP] = @TIMESTOP," +
				" [TIMECOR] = @TIMECOR," +
				" [TIMECRID] = @TIMECRID," +
				" [loginid] = @loginid," +
				" [machineid] = @machineid," +
				" [dateeve] = @dateeve," +
				" [qty] = @qty," +
				" [qtyReal] = @qtyReal," +
				" [QTYPACK] = @QTYPACK," +
				" [QTYPACKMJ] = @QTYPACKMJ," +
				" [description] = @description," +
				" [BarcodeP] = @BarcodeP," +
				" [UserID] = @UserID," +
				" [TermID] = @TermID," +
				" [ISOK] = @ISOK," +
				" [GUID] = @GUID," +
				" [TIMEMODE] = @TIMEMODE," +
				" [TIMEPREPSTART] = @TIMEPREPSTART," +
				" [TIMEPREPSTOP] = @TIMEPREPSTOP," +
				" [TIMECORSTART] = @TIMECORSTART," +
				" [TIMECORSTOP] = @TIMECORSTOP," +
				" [operationid] = @operationid," +
				" [SOUBEHGUID] = @SOUBEHGUID," +
				" [CORRGUID] = @CORRGUID," +
				" [TIMECRIDTYPE] = @TIMECRIDTYPE," +
				" [SKL_ID] = @SKL_ID," +
				" [LOCNCODE] = @LOCNCODE," +
				" [SERLTNUM] = @SERLTNUM," +
				" [ITEMDESC] = @ITEMDESC," +
				" [NMBRPAL] = @NMBRPAL," +
				" [TYPEPAL] = @TYPEPAL," +
				" [PackType] = @PackType," +
				" [status] = @status," +
				" [WEIGHT] = @WEIGHT," +
				" [STORNOGUID] = @STORNOGUID," +
				" [REZ_1] = @REZ_1," +
				" [REZ_2] = @REZ_2," +
				" [REZ_3] = @REZ_3," +
				" [REZ_4] = @REZ_4," +
				" [REZ_5] = @REZ_5," +
				" [WEIGHT_OLD] = @WEIGHT_OLD" +
				" WHERE (([id] = @id))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMESTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMESTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMESTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMESTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECOR", DbType = System.Data.DbType.Single, SourceColumn = "TIMECOR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECRID", DbType = System.Data.DbType.Int32, SourceColumn = "TIMECRID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "loginid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "machineid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, SourceColumn = "dateeve" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@qty", DbType = System.Data.DbType.Decimal, SourceColumn = "qty" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@qtyReal", DbType = System.Data.DbType.Decimal, SourceColumn = "qtyReal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "UserID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "TermID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ISOK", DbType = System.Data.DbType.DateTime, SourceColumn = "ISOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREPSTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEPREPSTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEPREPSTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEPREPSTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECORSTART", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMECORSTART" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECORSTOP", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMECORSTOP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@operationid", DbType = System.Data.DbType.String, SourceColumn = "operationid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOUBEHGUID", DbType = System.Data.DbType.Guid, SourceColumn = "SOUBEHGUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CORRGUID", DbType = System.Data.DbType.Guid, SourceColumn = "CORRGUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMECRIDTYPE", DbType = System.Data.DbType.Byte, SourceColumn = "TIMECRIDTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PackType", DbType = System.Data.DbType.String, SourceColumn = "PackType" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@status", DbType = System.Data.DbType.Int32, SourceColumn = "status" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STORNOGUID", DbType = System.Data.DbType.Guid, SourceColumn = "STORNOGUID" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, SourceColumn = "REZ_3" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, SourceColumn = "REZ_4" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_5", DbType = System.Data.DbType.String, SourceColumn = "REZ_5" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT_OLD", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_OLD" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

		}

		public void InitializeCommandDelete_Production(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [Production] WHERE [id] = @id";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_Production(SQLiteCommand command)
		{
			command.CommandText = "Select ROWID as id, * from Production";
		}


		#endregion


		#endregion

		public int Delete_Production()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM Production ";

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public int DeleteByGUID_Production(Guid GUID)
		{
			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM Production WHERE GUID=@GUID";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
					int responseValue = command.ExecuteNonQuery();

					return responseValue;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}
		}

		public System.Data.SQLite.SQLiteDataReader GetReaderGUID_Production() 
		{
			System.Data.SQLite.SQLiteDataReader dr = null;

			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT guid FROM production";
					dr = command.ExecuteReader();
				}

				return dr;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}

		}

		public int? GetCount_Production()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT count(*) FROM production";
					command.CommandType = System.Data.CommandType.Text;

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int)pp;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.Production_updateDataTable GetProductionUpdateByDateEve_Production(DateTime dateeve)
		{
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.Production_updateDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT " +
						" CountEntries," +
						" SOPNUMBE," +
						" ORD," +
						" ITEMNMBR," +
						" ITEMTYPE," +
						" QTYPACK," +
						" SUM(qty) AS QTYODVEDENO," +
						" Count(*) AS CNTODVEDENO," +
						" MAX(dateeve) as LSTMod, " +
						" BarcodeP " +
						"FROM Production " +
						"WHERE (dateeve > @dateeve) " +
						"GROUP BY CountEntries, SOPNUMBE, ORD, ITEMNMBR, ITEMTYPE, QTYPACK, BarcodeP";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = dateeve });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

        public Fask.SQLiteDBs.DataSets.Vyroba.Production_updateDataTable GetProductionUpdate()
        {
            try
            {
                var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.Production_updateDataTable();

                Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
					command.CommandText = "SELECT " +
						" CountEntries," +
						" SOPNUMBE," +
						" ORD," +
						" ITEMNMBR," +
						" ITEMTYPE," +
						" QTYPACK," +
						//" SUM(qty) AS QTYODVEDENO," +
						" qty AS QTYODVEDENO," +
						//" Count(*) AS CNTODVEDENO," +
						" 1 AS CNTODVEDENO," +
						//" MAX(dateeve) as LSTMod " +
						" dateeve as LSTMod " +
						" FROM Production " +
						//"WHERE (dateeve > @dateeve) " +
						//"GROUP BY CountEntries, SOPNUMBE, ORD, ITEMNMBR, ITEMTYPE, QTYPACK"+
						" Order by dateeve asc" +
						"";

                    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = LSTMod });

                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        int returnValue = adapter.Fill(dataTable);
                        return dataTable;
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

		public int FillProduction_HistoryFilter_Production(Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable dataTable,
			int ModulPrehledPocetHodinHistorie,
			DateTime? filtrDatumDo,
			DateTime? filtrDatumOd,
			bool filtrNedokonceneZakazky)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id, * FROM Production";

					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

					if (filtrDatumDo == null && filtrDatumOd == null)
					{
						command.CommandText += " WHERE dateeve > @datum ";
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@datum", DbType = System.Data.DbType.DateTime, Value = DateTime.Now.AddHours(-ModulPrehledPocetHodinHistorie) });
					}

					if (filtrNedokonceneZakazky)
					{
						if (filtrDatumDo != null && filtrDatumOd != null)
						{
							command.CommandText += " WHERE ";
						}
						else
							command.CommandText += " AND ";

						command.CommandText += " SOUBEHGUID NOT IN (" +
						" SELECT distinct SOUBEHGUID FROM Production " +
						" WHERE " +
						" (SOUBEHGUID IS NOT NULL AND TIMESTOP IS NOT NULL) " +
						" OR " +
						" (SOUBEHGUID IS NOT NULL AND TIMEPREPSTOP IS NOT NULL) " +
						" )";
					}

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

		public Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable GetProduction_HistoryFilter2_Production(int ModulPrehledPocetHodinHistorie, DateTime? filtrDatumDo, DateTime? filtrDatumOd)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable();
			try
			{
			

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ROWID as id,* FROM Production";

					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });

					if (filtrDatumDo == null && filtrDatumOd == null)
					{
						command.CommandText += "WHERE dateeve > @datum ";
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@datum", DbType = System.Data.DbType.DateTime, Value = DateTime.Now.AddHours(-ModulPrehledPocetHodinHistorie) });
					}


					if (filtrDatumDo != null && filtrDatumOd != null)
					{
						command.CommandText += "WHERE ";
					}
					else
						command.CommandText += "AND ";

					command.CommandText += "TIMESTART IS NULL AND CORRGUID NOT IN (" +
											"SELECT distinct CORRGUID FROM Production " +
											"WHERE " +
											"(CORRGUID IS NOT NULL and TIMECORSTOP IS NOT NULL) " +
											")";


					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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


		#endregion

		#region Production_Sources

		internal int Update_Production_Sources(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Production_Sources(commandInsert);
					InitializeCommandUpdate_Production_Sources(commandUpdate);
					//InitializeCommandDelete_Production_Sources(commandDelete);
					InitializeCommandSelect_Production_Sources(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_Production_Sources(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [Production_Sources] (" + 
				" [CountEntries]," + 
				" [SOPNUMBE]," + 
				" [ITEMNAME]," + 
				" [ITEMNMBR]," + 
				" [ITEMTYPE]," + 
				" [ITEMCODE]," + 
				" [LOCNCODE]," + 
				" [MJ]," + 
				" [QTYSHPPD]," + 
				" [QTYPACK]," + 
				" [SERLTNUM]," + 
				" [QTYSHPPDMJ]," + 
				" [GUID_Production]," + 
				" [GUID]," + 
				" [USER_ID]," + 
				" [TERMINAL_ID]," + 
				" [WEIGHT]," + 
				" [NMBRPAL]," + 
				" [TYPEPAL]," + 
				" [PRINTED]," + 
				" [SKL_ID])" + 
				" VALUES " +
				"(@CountEntries," +
				" @SOPNUMBE," +
				" @ITEMNAME," +
				" @ITEMNMBR," +
				" @ITEMTYPE," +
				" @ITEMCODE," +
				" @LOCNCODE," +
				" @MJ," +
				" @QTYSHPPD," +
				" @QTYPACK," +
				" @SERLTNUM," +
				" @QTYSHPPDMJ," +
				" @GUID_Production," +
				" @GUID," +
				" @USER_ID," +
				" @TERMINAL_ID," +
				" @WEIGHT," +
				" @NMBRPAL," +
				" @TYPEPAL," +
				" @PRINTED," +
				" @SKL_ID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNAME", DbType = System.Data.DbType.String, SourceColumn = "ITEMNAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID_Production", DbType = System.Data.DbType.Guid, SourceColumn = "GUID_Production" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.String, SourceColumn = "USER_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TERMINAL_ID", DbType = System.Data.DbType.Int32, SourceColumn = "TERMINAL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.String, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });

		}

		public void InitializeCommandUpdate_Production_Sources(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE Production_Sources SET " + 
				" CountEntries = @CountEntries," + 
				" SOPNUMBE = @SOPNUMBE," + 
				" ITEMNAME = @ITEMNAME," + 
				" ITEMNMBR = @ITEMNMBR," + 
				" ITEMTYPE = @ITEMTYPE," + 
				" ITEMCODE = @ITEMCODE," + 
				" LOCNCODE = @LOCNCODE," + 
				" MJ = @MJ," + 
				" QTYSHPPD = @QTYSHPPD," + 
				" QTYSHPPDMJ = @QTYSHPPDMJ," + 
				" QTYPACK = @QTYPACK," + 
				" SERLTNUM = @SERLTNUM," +
				" GUID_Production = @GUID_Production," +
				" GUID = @GUID," +
				" USER_ID = @USER_ID," +
				" TERMINAL_ID = @TERMINAL_ID," +
				" WEIGHT = @WEIGHT," +
				" NMBRPAL = @NMBRPAL," +
				" TYPEPAL = @TYPEPAL," +
				" PRINTED = @PRINTED," + 
				" SKL_ID = @SKL_ID";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNAME", DbType = System.Data.DbType.String, SourceColumn = "ITEMNAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID_Production", DbType = System.Data.DbType.Guid, SourceColumn = "GUID_Production" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.String, SourceColumn = "USER_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TERMINAL_ID", DbType = System.Data.DbType.Int32, SourceColumn = "TERMINAL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.String, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });

		}

		//public void InitializeCommandDelete_Production_Sources(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM Production_Sources";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@id",
		//		DbType = System.Data.DbType.Int32,
		//		SourceColumn = "id",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_Production_Sources(SQLiteCommand command)
		{
			command.CommandText = "Select * from Production_Sources";
		}


		#endregion

		public int DeleteByGUID_Production_Sources(Guid GUID)
		{
			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM production_Sources WHERE GUID=@GUID";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
					int responseValue = command.ExecuteNonQuery();

					return responseValue;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}
		}

		public System.Data.SQLite.SQLiteDataReader GetReaderGUID_Production_Sources()
		{
			System.Data.SQLite.SQLiteDataReader dr = null;

			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT guid FROM production_Sources";
					dr = command.ExecuteReader();
				}

				return dr;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}

		}


		#endregion

		#region StatusTypes

		public int Fill_StatusTypes(Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesDataTable dataTable)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM StatusTypes";

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


		#endregion

		#region UserEvents

		#region Update

		internal int Update_UserEvents(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_UserEvents(commandInsert);
					InitializeCommandUpdate_UserEvents(commandUpdate);
					InitializeCommandDelete_UserEvents(commandDelete);
					InitializeCommandSelect_UserEvents(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public void InitializeCommandInsert_UserEvents(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO UserEvents" + 
				" (loginid," + 
				" machineid," + 
				" dateeve," + 
				" statusid," + 
				" UserID," + 
				" TermID," + 
				" REZ1," + 
				" GUID)" + 
				" VALUES " + 
				" ( @loginid," + 
				" @machineid," + 
				" @dateeve," + 
				" @statusid," + 
				" @UserID," + 
				" @TermID," + 
				" @REZ1," + 
				" @GUID )";
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "ID_H" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "ID_L" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, SourceColumn = "ITEMNMBR_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = System.Data.DbType.String, SourceColumn = "DESC_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "MJ_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "ITEMNMBR_fol" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "DESC_Fol" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "MJ_Fol" });

		}

		public void InitializeCommandUpdate_UserEvents(SQLiteCommand command)
		{
			command.CommandText = "UPDATE UserEvents SET " + 
				" loginid = @loginid," + 
				" machineid = @machineid," + 
				" dateeve = @dateeve," + 
				" statusid = @statusid," + 
				" UserID = @UserID," + 
				" TermID = @TermID," + 
				" REZ1 = @REZ1," + 
				" GUID = @GUID " + 
				" WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "ID_H" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "ID_L" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, SourceColumn = "ITEMNMBR_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = System.Data.DbType.String, SourceColumn = "DESC_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "MJ_Def" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, SourceColumn = "ITEMNMBR_fol" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "DESC_Fol" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "MJ_Fol" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

		}

		public void InitializeCommandDelete_UserEvents(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM UserEvents WHERE (id = @id)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@id",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "id",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_UserEvents(SQLiteCommand command)
		{
			command.CommandText = "Select ROWID as id,* from UserEvents";
		}


		#endregion

		#endregion

		public virtual int Insert_UserEvents(
			string loginid, 
			string machineid, 
			System.DateTime dateeve, 
			string statusid, 
			string UserID, 
			byte TermID, 
			string REZ1, 
			System.Guid GUID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = "INSERT INTO UserEvents" +
															" (loginid," +
															" machineid," +
															" dateeve," +
															" statusid," +
															" UserID," +
															" TermID," +
															" REZ1," +
															" GUID)" +
															" VALUES " +
															" ( @loginid," +
															" @machineid," +
															" @dateeve," +
															" @statusid," +
															" @UserID," +
															" @TermID," +
															" @REZ1," +
															" @GUID )";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = loginid == null ? (object)DBNull.Value : loginid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, Value = machineid == null ? (object)DBNull.Value : machineid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = dateeve });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = System.Data.DbType.String, Value = statusid == null ? (object)DBNull.Value : statusid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Byte, Value = TermID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, Value = REZ1 == null ? (object)DBNull.Value : REZ1 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		public int Delete_UserEvents()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM userevents ";

					int responseValue = command.ExecuteNonQuery();

					return responseValue;
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

		public int DeleteByGUID_UserEvents(Guid GUID)
		{
			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM UserEvents WHERE GUID=@GUID";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
					int responseValue = command.ExecuteNonQuery();

					return responseValue;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}
		}

		public System.Data.SQLite.SQLiteDataReader GetReaderGUID_UserEvents()
		{
			System.Data.SQLite.SQLiteDataReader dr = null;

			try
			{
				//Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT guid FROM UserEvents";
					dr = command.ExecuteReader();
				}

				return dr;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}

		}


		#endregion

		#region VMachinesOperations

		public int FillByMachineID_VMachinesOperations(Fask.SQLiteDBs.DataSets.Vyroba.VMachinesOperationsDataTable dataTable, string machineid)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM VMachinesOperations WHERE (machineid = @machineid)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, Value = machineid == null ? (object)DBNull.Value : machineid });

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

		#endregion

		#region Production_SN

		#region Update

		internal int Update_Production_SN(object data)
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
					InitializeCommandInsert_Production_SN(commandInsert);
					//InitializeCommandUpdate_Production_SN(commandUpdate);
					//InitializeCommandDelete_Production(commandDelete);
					InitializeCommandSelect_Production_SN(commandSelect);

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

		public void InitializeCommandInsert_Production_SN(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [Production_SN] (" +
				" [GUID_Production]," +
				" [GUID]," +
				" [SERLNMBR]," +
				" [ITEMNMBR]," +
				" [QTY]," +
				" [Expirace]," +
				" [REZ_1]," +
				" [REZ_2]," +
				" [REZ_3]," +
				" [REZ_4])" +
				" VALUES " +
				"(@GUID_Production," +
				" @GUID," +
				" @SERLNMBR, " +
				" @ITEMNMBR, " +
				" @QTY, " +
				" @Expirace, " +
				" @REZ_1, " +
				" @REZ_2, " +
				" @REZ_3, " +
				" @REZ_4)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID_Production", DbType = System.Data.DbType.Guid, SourceColumn = "GUID_Production" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, SourceColumn = "REZ_3" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, SourceColumn = "REZ_4" });

		}

		//public void InitializeCommandUpdate_Production(SQLiteCommand command)
		//{
			//command.CommandText = @"UPDATE [Production] SET" +
			//    " [CountEntries] = @CountEntries," +
			//    " [SOPNUMBE] = @SOPNUMBE," +
			//    " WHERE (([id] = @id))";

			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });

		//}

		//public void InitializeCommandDelete_Production(SQLiteCommand command)
		//{
			//command.CommandText = "DELETE FROM [Production] WHERE [id] = @id";

			//command.Parameters.Add(new SQLiteParameter()
			//{
			//    ParameterName = "@id",
			//    DbType = System.Data.DbType.Int32,
			//    SourceColumn = "id",
			//    SourceVersion = System.Data.DataRowVersion.Original
			//});
		//}

		public void InitializeCommandSelect_Production_SN(SQLiteCommand command)
		{
			command.CommandText = "Select * from Production_SN";
		}


		#endregion


		#endregion

        public int DeleteByGUID_Production_SN(Guid GUID)
        {
            try
            {
                //Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Production_SN WHERE GUID=@GUID";
                    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
                    int responseValue = command.ExecuteNonQuery();

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                //Connection_Close();
            }
        }

        public System.Data.SQLite.SQLiteDataReader GetReaderGUID_Production_SN()
        {
            System.Data.SQLite.SQLiteDataReader dr = null;

            try
            {
                //Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = @"SELECT GUID FROM Production_SN";
                    dr = command.ExecuteReader();
                }

                return dr;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                //Connection_Close();
            }

        }

		
		#endregion

		#endregion

		#region FASK_Vyroba_TP

		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(string itemnmbr_def)
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
			try
			{

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					//command.CommandText = "SELECT * FROM FASK_Vyroba_TP where (itemnmbr_def=@itemnmbr_def) and (id_h is not null)";
					command.CommandText = "SELECT *, FC.CZ_CarKod, FC.CZ_SerNum_Track, FC.SKL_ID FROM FASK_Vyroba_TP as FV ";
					command.CommandText += "left join FASK_CONS_095 as FC on FC.itemnmbr=FV.itemnmbr_fol";

					command.CommandText += " where (FV.itemnmbr_def=@itemnmbr_def) and (FV.id_h is not null)";


					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr_def", DbType = System.Data.DbType.String, Value = itemnmbr_def == null ? (object)DBNull.Value : itemnmbr_def });
			
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable GetDataBy_ITEMNMBR_fol_IDHNULL_FASK_Vyroba_TP(string itemnmbr_def)
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
			try
			{

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM FASK_Vyroba_TP where (ITEMNMBR_fol=@ITEMNMBR_fol) and (id_h is not null)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR_fol", DbType = System.Data.DbType.String, Value = itemnmbr_def == null ? (object)DBNull.Value : itemnmbr_def });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public virtual Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable GetDataByIDH_FASK_Vyroba_TP(string id_h)
		{
			Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable dataTable = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable();
			try
			{

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM FASK_Vyroba_TP where id_h=@id_h";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id_h", DbType = System.Data.DbType.String, Value = id_h == null ? (object)DBNull.Value : id_h });

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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


		#endregion



		#region Sledovani Vyroby

		public int Fill_Universal(DataSet ds, string SQLScript, string TableName)
		{
			try
			{
				ds.Tables[TableName].Clear();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = SQLScript;

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(ds, TableName);
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

		public int Delete_Universal(string SQLScript)
		{
			try
			{ 
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = SQLScript;
					int returnValue = command.ExecuteNonQuery();
					return returnValue;
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

		public object Fill_Scalar_Universal(string SQLScript)
		{
			try
			{

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = SQLScript;
					return command.ExecuteScalar();
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

		#region Fask_EventsInsert

		public int Fask_EventsInsert(
			string loginid,
			decimal qty,
			decimal qtyReal,
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
			string machineid,
			string VPH,
			int? VPPol,
			string EAN_IS,
			string IS_ID,
			string NMBRPAL,
			int? status,
			decimal QTYPACK,
			string PackType,
			decimal? WEIGHT,
			DateTime dateeve,
			Guid faskGUID,
			DateTime isProcessed,
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
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = @"INSERT INTO FASK_Events (" +
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
						" machineid, " +
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

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = loginid == null ? (object)DBNull.Value : loginid });

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime,  Value = dateeve});
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@qty", DbType = DbType.Decimal,  Value = qty });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@qtyReal", DbType = DbType.Decimal,  Value = qtyReal });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = DbType.String,  Value = string.IsNullOrEmpty(description) ? (object)DBNull.Value : description.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@barcodeReaded", DbType = DbType.String,  Value = string.IsNullOrEmpty(barcodeReaded) ? string.Empty : barcodeReaded.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@barcodeSended", DbType = DbType.String,  Value = string.IsNullOrEmpty(barcodeSended) ? string.Empty : barcodeSended.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@zakazka", DbType = DbType.String,  Value = string.IsNullOrEmpty(zakazka) ? (object)DBNull.Value : zakazka.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@popis", DbType = DbType.String,  Value = string.IsNullOrEmpty(popis) ? (object)DBNull.Value : popis.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid,  Value = faskGUID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@reportType", DbType = DbType.String,  Value = string.IsNullOrEmpty(reportType) ? string.Empty : reportType.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@isProcessed", DbType = DbType.DateTime,  Value = isProcessed});
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDO", DbType = DbType.String,  Value = string.IsNullOrEmpty(IDO) ? string.Empty : IDO.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan1", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan1) ? (object)DBNull.Value : scan1.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan2", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan2) ? (object)DBNull.Value : scan2.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan3", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan3) ? (object)DBNull.Value : scan3.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@sensor", DbType = DbType.String,  Value = string.IsNullOrEmpty(sensor) ? (object)DBNull.Value : sensor.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@material", DbType = DbType.String,  Value = string.IsNullOrEmpty(material) ? (object)DBNull.Value : material.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = DbType.String,  Value = string.IsNullOrEmpty(machineid) ? (object)DBNull.Value : machineid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@VPH", DbType = DbType.String,  Value = string.IsNullOrEmpty(VPH) ? (object)DBNull.Value : VPH.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@VPPol", DbType = DbType.Int32,  Value = VPPol.HasValue ? VPPol.Value : -1 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN_IS", DbType = DbType.String,  Value = string.IsNullOrEmpty(EAN_IS) ? (object)DBNull.Value : EAN_IS.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IS_ID", DbType = DbType.String,  Value = string.IsNullOrEmpty(IS_ID) ? (object)DBNull.Value : IS_ID.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = DbType.String,  Value = string.IsNullOrEmpty(NMBRPAL) ? (object)DBNull.Value : NMBRPAL.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@status", DbType = DbType.Int32,  Value = status.HasValue ? (object)status.Value : DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@productionGuid", DbType = DbType.Guid,  Value = loginid == null ? (object)DBNull.Value : loginid});
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal,  Value = QTYPACK });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PackType", DbType = DbType.String,  Value = string.IsNullOrEmpty(PackType) ? (object)DBNull.Value : PackType.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = DbType.Decimal,  Value = WEIGHT.HasValue  ? (object)WEIGHT.Value : DBNull.Value });

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@BarcodeT", DbType = DbType.Byte, Value = BarcodeT });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = DbType.String, Value = string.IsNullOrEmpty(REZ_1) ? (object)DBNull.Value : REZ_1.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = DbType.String, Value = string.IsNullOrEmpty(REZ_2) ? (object)DBNull.Value : REZ_2.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_3", DbType = DbType.String, Value = string.IsNullOrEmpty(REZ_3) ? (object)DBNull.Value : REZ_3.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_4", DbType = DbType.String, Value = string.IsNullOrEmpty(REZ_4) ? (object)DBNull.Value : REZ_4.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_5", DbType = DbType.String, Value = string.IsNullOrEmpty(REZ_5) ? (object)DBNull.Value : REZ_5.Trim() });


						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#endregion

		#region Fask_EventsErrInsert

		public int Fask_EventsErrInsert(
			string loginid,
			decimal qty,
			decimal qtyReal,
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
			string machineid,
			DateTime dateeve,
			DateTime? isProcessed,
			Guid faskGUID
			)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = @" INSERT INTO [FASK_EventsErr] " +
							" ( " +
							" [loginid], " +
							" [machineid], " +
							" [dateeve], " +
							" [qty], " +
							" [qtyReal], " +
							" [description], " +
							" [barcodeReaded], " +
							" [barcodeSended], " +
							" [zakazka], " +
							" [popis], " +
							" [faskGUID], " +
							" [reportType], " +
							" [isProcessed], " +
							" [IDO], " +
							" [scan1], " +
							" [scan2], " +
							" [scan3], " +
							" [sensor] " +
							" ) VALUES (" +
							" @loginid, " +
							" @machineid, " +
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
							" @sensor " +
							" )";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = loginid == null ? (object)DBNull.Value : loginid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = DbType.String,  Value = string.IsNullOrEmpty(machineid) ? (object)DBNull.Value : machineid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime,  Value = dateeve });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@qty", DbType = DbType.Decimal, Value = qty });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@qtyReal", DbType = DbType.Decimal,  Value = qtyReal });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = DbType.String,  Value = string.IsNullOrEmpty(description) ? (object)DBNull.Value : description.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@barcodeReaded", DbType = DbType.String,  Value = string.IsNullOrEmpty(barcodeReaded) ? string.Empty : barcodeReaded.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@barcodeSended", DbType = DbType.String,  Value = string.IsNullOrEmpty(barcodeSended) ? string.Empty : barcodeSended.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@zakazka", DbType = DbType.String,  Value = string.IsNullOrEmpty(zakazka) ? (object)DBNull.Value : zakazka.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@popis", DbType = DbType.String,  Value = string.IsNullOrEmpty(popis) ? (object)DBNull.Value : popis.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid,  Value = faskGUID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@reportType", DbType = DbType.String,  Value = string.IsNullOrEmpty(reportType) ? string.Empty : reportType.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@isProcessed", DbType = DbType.DateTime,  Value = isProcessed.HasValue ? (object)isProcessed.Value : DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDO", DbType = DbType.String, Value = string.IsNullOrEmpty(IDO) ? (object)DBNull.Value : IDO.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan1", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan1) ? (object)DBNull.Value : scan1.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan2", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan2) ? (object)DBNull.Value : scan2.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@scan3", DbType = DbType.String,  Value = string.IsNullOrEmpty(scan3) ? (object)DBNull.Value : scan3.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@sensor", DbType = DbType.String,  Value = string.IsNullOrEmpty(sensor) ? (object)DBNull.Value : sensor.Trim() });
						

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#endregion

		#region Fask_UserEventsErrInsert

		public int Fask_UserEventsInsert(
			string loginid,
			string machineid,
			string statusid,
			string rez_1,
			string rez_2,
			DateTime dateeve,
			Guid faskGUID
			)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = @"INSERT INTO [FASK_UserEvents]" +
							" ( " +
							" [loginid], [machineid], [dateeve], [statusid], [faskGUID], [rez_1], [rez_2]" +
							" )" +
							" VALUES " +
							" ( " +
							" @loginid, @machineid, @dateeve, @statusid, @faskGUID, @rez_1, @rez_2" +
							" ) ";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = loginid == null ? (object)DBNull.Value : loginid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = DbType.String, Value = string.IsNullOrEmpty(machineid) ? (object)DBNull.Value : machineid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime, Value = dateeve });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = DbType.String, Value = string.IsNullOrEmpty(statusid) ? (object)DBNull.Value : statusid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid, Value = faskGUID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@rez_1", DbType = DbType.String, Value = string.IsNullOrEmpty(rez_1) ? (object)DBNull.Value : rez_1.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@rez_2", DbType = DbType.String, Value = string.IsNullOrEmpty(rez_2) ? (object)DBNull.Value : rez_2.Trim() });

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#endregion

		#region Fask_UserEventsErrInsert

		public int Fask_UserEventsErrInsert(
			int id,
			string loginid,
			string machineid,
			string statusid,
			DateTime dateeve,
			Guid faskGUID
			)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = @"INSERT INTO [FASK_UserEventsErr]" +
							" ( " +
							"  [id],[loginid], [machineid], [dateeve], [statusid], [faskGUID]" +
							" )" +
							" VALUES " +
							" ( " +
							"  @id, @loginid, @machineid, @dateeve, @statusid, @faskGUID " +
							" ) ";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, Value = id });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = loginid == null ? string.Empty : loginid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = DbType.String, Value = string.IsNullOrEmpty(machineid) ? string.Empty : machineid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime, Value = dateeve });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@statusid", DbType = DbType.String, Value = string.IsNullOrEmpty(statusid) ? string.Empty : statusid.Trim() });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid, Value = faskGUID });

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#endregion

		#region Update_FASK_Logins

		internal int Update_FASK_Logins(object data)
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
					InitializeCommandInsert_FASK_Logins(commandInsert);
					//InitializeCommandUpdate_CZPRO_VPH(commandUpdate);
					//InitializeCommandDelete_CZPRO_VPH(commandDelete);
					InitializeCommandSelect_FASK_Logins(commandSelect);

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

		public void InitializeCommandInsert_FASK_Logins(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [FASK_Logins] (" +
				" [USERID] " +
				" ,[firstname] " +
				" ,[surname] " +
				" ,[psswd] " +
				" ,[CREATED] " +
				" ,[VALIDFROM] " +
				" ,[VALIDTO] " +
				" ,[RFID]";

			command.CommandText += " ) VALUES ( ";

				command.CommandText += 
				" @USERID " +
				" ,@firstname " +
				" ,@surname " +
				" ,@psswd " +
				" ,@CREATED " +
				" ,@VALIDFROM " +
				" ,@VALIDTO " +
				" ,@RFID";

			command.CommandText += ")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.String, SourceColumn = "USERID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@firstname", DbType = System.Data.DbType.String, SourceColumn = "firstname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@surname", DbType = System.Data.DbType.String, SourceColumn = "surname", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@psswd", DbType = System.Data.DbType.String, SourceColumn = "psswd", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CREATED", DbType = System.Data.DbType.DateTime, SourceColumn = "CREATED", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VALIDFROM", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDFROM", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VALIDTO", DbType = System.Data.DbType.DateTime, SourceColumn = "VALIDTO", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@RFID", DbType = System.Data.DbType.String, SourceColumn = "RFID", SourceVersion = DataRowVersion.Current });
			
		}

		//public void InitializeCommandUpdate_FASK_Logins(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_FASK_Logins(SQLiteCommand command)
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

		public void InitializeCommandSelect_FASK_Logins(SQLiteCommand command)
		{
			command.CommandText = "Select * from FASK_Logins";
		}


		#endregion

		#endregion

		#region Update_FASK_Machines

		internal int Update_FASK_Machines(object data)
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
					InitializeCommandInsert_FASK_Machines(commandInsert);
					//InitializeCommandUpdate_CZPRO_VPH(commandUpdate);
					//InitializeCommandDelete_CZPRO_VPH(commandDelete);
					InitializeCommandSelect_FASK_Machines(commandSelect);

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

		public void InitializeCommandInsert_FASK_Machines(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [FASK_Machines] (" +
				" [id] " +
				" ,[machinetype] " +
				" ,[name] " +
				" ,[description] " +
				" ,[koeficient] " ;

			command.CommandText += " ) VALUES ( ";

			command.CommandText +=
			" @id " +
			" ,@machinetype " +
			" ,@name " +
			" ,@description " +
			" ,@koeficient " ;

			command.CommandText += ")";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", DbType = System.Data.DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machinetype", DbType = System.Data.DbType.String, SourceColumn = "machinetype", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", DbType = System.Data.DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@koeficient", DbType = System.Data.DbType.Decimal, SourceColumn = "koeficient", SourceVersion = DataRowVersion.Current });

		}

		//public void InitializeCommandUpdate_FASK_Machines(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_FASK_Machines(SQLiteCommand command)
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

		public void InitializeCommandSelect_FASK_Machines(SQLiteCommand command)
		{
			command.CommandText = "Select * from FASK_Machines";
		}


		#endregion

		#endregion

		#endregion

	}
}
