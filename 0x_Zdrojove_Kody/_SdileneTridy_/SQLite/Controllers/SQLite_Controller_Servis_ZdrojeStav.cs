using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controller pro Servis stavy Zdroju, pripadne davka StavZdroju
	/// </summary>
	public class SQLite_Controller_Servis_ZdrojeStav : SQLite_Controller
	{

        #region Table adapters

        #region Hlavni stavy zdroju nebo davka se zdroji a jejich stavu ke zpracovani

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter taZdrojeStav = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter TaZdrojeStav
		//{
		//    get
		//    {
		//        if (taZdrojeStav == null)
		//        {
		//            taZdrojeStav = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
		//            taZdrojeStav.Connection = this.Connection;
		//        }
		//        return taZdrojeStav;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter taZdrojeStavTmp = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter TaZdrojeStavTmp
		//{
		//    get
		//    {
		//        if (taZdrojeStavTmp == null)
		//        {
		//            taZdrojeStavTmp = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter();
		//            taZdrojeStavTmp.Connection = this.Connection;
		//        }
		//        return taZdrojeStavTmp;
		//    }
		//}


		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter taPredloha = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter TaPredloha
		//{
		//    get
		//    {
		//        if (taPredloha == null)
		//        {
		//            taPredloha = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter();
		//            taPredloha.Connection = this.Connection;
		//        }
		//        return taPredloha;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.ParametryTableAdapter taParametry = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.ParametryTableAdapter TaParametry
		//{
		//    get
		//    {
		//        if (taParametry == null)
		//        {
		//            taParametry = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.ParametryTableAdapter();
		//            taParametry.Connection = this.Connection;
		//        }
		//        return taParametry;
		//    }
		//}

        #endregion

        #endregion

        #region c'tors

        public SQLite_Controller_Servis_ZdrojeStav(string sqliteFileName)
			: base(sqliteFileName)
		{
        }

		#endregion

		public override void Dispose()
		{

			//this.DisposeObject(taZdrojeStav);
			//this.DisposeObject(taZdrojeStavTmp);
			//this.DisposeObject(taPredloha);
            //this.DisposeObject(taParametry);

			//this.taZdrojeStav = null;
			//this.taZdrojeStavTmp = null;
			//this.taPredloha = null;
            //this.taParametry = null;

			base.Dispose();
		}

		#region Metody

		#region ZdrojStav

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable GetDataByIDZdroj_ZdrojStav(string IDZdroj)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_Servis_ZdrojStav WHERE (IDZdroj = @IDZdroj)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj });

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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable GetData_ZdrojStav()
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_Servis_ZdrojStav";
				
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

		#region Update

		public int Update_ZdrojStav(object data)
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
					InitializeCommandInsert_ZdrojStav(commandInsert);
					InitializeCommandUpdate_ZdrojStav(commandUpdate);
					InitializeCommandDelete_ZdrojStav(commandDelete);
					InitializeCommandSelect_ZdrojStav(commandSelect);

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
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
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

		public void InitializeCommandInsert_ZdrojStav(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_Servis_ZdrojStav (" +
					" IDZdroj," +
					" IDStav," +
					" IDCinnost," +
					" Modified," +
					" IDTerminal," +
					" IDUser," +
					" GUID," +
					" CinnostValue," +
					" CinnostType," +
					" CountEntries," +
					" ODB_ID," +
					" OkruhID," +
					" CinnostOznaceni," +
					" GPS_X," +
					" GPS_Y," +
					" GPS_Z" +
					") VALUES (" +
					" @IDZdroj," +
					" @IDStav," +
					" @IDCinnost," +
					" @Modified," +
					" @IDTerminal," +
					" @IDUser," +
					" @GUID," +
					" @CinnostValue," +
					" @CinnostType," +
					" @CountEntries," +
					" @ODB_ID," +
					" @OkruhID," +
					" @CinnostOznaceni," +
					" @GPS_X," +
					" @GPS_Y," +
					" @GPS_Z " +
					" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, SourceColumn = "IDZdroj" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, SourceColumn = "IDStav" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, SourceColumn = "Modified" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.Int32, SourceColumn = "IDTerminal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.Int32, SourceColumn = "IDUser" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, SourceColumn = "CinnostValue" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, SourceColumn = "CinnostType" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, SourceColumn = "OkruhID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, SourceColumn = "CinnostOznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, SourceColumn = "GPS_X" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double, SourceColumn = "GPS_Y" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, SourceColumn = "GPS_Z" });
		}

		public void InitializeCommandUpdate_ZdrojStav(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE CZMST_Servis_ZdrojStav SET " +
				" IDZdroj = @IDZdroj," +
				" IDStav = @IDStav," +
				" IDCinnost = @IDCinnost," +
				" Modified = @Modified," +
				" IDTerminal = @IDTerminal," +
				" IDUser = @IDUser," +
				" CinnostValue = @CinnostValue," +
				" CinnostType = @CinnostType," +
				" CountEntries = @CountEntries," +
				" ODB_ID = @ODB_ID," +
				" OkruhID = @OkruhID," +
				" CinnostOznaceni = @CinnostOznaceni," +
				" GPS_X = @GPS_X," +
				" GPS_Y = @GPS_Y," +
				" GPS_Z = @GPS_Z " +
				"WHERE (IDZdroj = @IDZdroj)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, SourceColumn = "IDZdroj", SourceVersion = DataRowVersion.Original });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, SourceColumn = "IDStav" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, SourceColumn = "Modified" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.Int32, SourceColumn = "IDTerminal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.Int32, SourceColumn = "IDUser" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, SourceColumn = "CinnostValue" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, SourceColumn = "CinnostType" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, SourceColumn = "OkruhID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, SourceColumn = "CinnostOznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, SourceColumn = "GPS_X" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double, SourceColumn = "GPS_Y" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, SourceColumn = "GPS_Z" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
		}

		public void InitializeCommandDelete_ZdrojStav(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_Servis_ZdrojStav WHERE (IDZdroj = @IDZdroj)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@IDZdroj",
				DbType = System.Data.DbType.String,
				SourceColumn = "IDZdroj",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_ZdrojStav(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_ZdrojStav";
		}


		#endregion

		#endregion

		public int Update_ZdrojStav(
			string IDStav,
			string IDCinnost,
			DateTime Modified,
			int? IDTerminal,
			int? IDUser,
			Guid? GUID,
			string CinnostValue,
			string CinnostType,
			int? CountEntries,
			string ODB_ID,
			string OkruhID,
			string CinnostOznaceni,
			double? GPS_X,
			double? GPS_Y,
			int? GPS_Z,
			string IDZdroj)
		{

			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"UPDATE CZMST_Servis_ZdrojStav SET " +
						" IDZdroj = @IDZdroj," +
						" IDStav = @IDStav," +
						" IDCinnost = @IDCinnost," +
						" Modified = @Modified," +
						" IDTerminal = @IDTerminal," +
						" IDUser = @IDUser," +
						" CinnostValue = @CinnostValue," +
						" CinnostType = @CinnostType," +
						" CountEntries = @CountEntries," +
						" ODB_ID = @ODB_ID," +
						" OkruhID = @OkruhID," +
						" CinnostOznaceni = @CinnostOznaceni," +
						" GPS_X = @GPS_X," +
						" GPS_Y = @GPS_Y," +
						" GPS_Z = @GPS_Z " +
						"WHERE (IDZdroj = @IDZdroj)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, Value = IDStav == null ? (object)DBNull.Value : IDStav });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, Value = IDCinnost == null ? (object)DBNull.Value : IDCinnost });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, Value = Modified });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.String, Value = IDTerminal.HasValue ? IDTerminal : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.String, Value = IDUser.HasValue ? IDUser : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, Value = CinnostValue == null ? (object)DBNull.Value : CinnostValue });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, Value = CinnostType == null ? (object)DBNull.Value : CinnostType });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, Value = CountEntries.HasValue ? CountEntries : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, Value = OkruhID == null ? (object)DBNull.Value : OkruhID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, Value = CinnostOznaceni == null ? (object)DBNull.Value : CinnostOznaceni });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, Value = GPS_X.HasValue ? GPS_X : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double, Value = GPS_Y.HasValue ? GPS_Y : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, Value = GPS_Z.HasValue ? GPS_Z : (object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.String, Value = GUID.HasValue ? GUID : (object)DBNull.Value });
					
					return command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				this.Connection_Close();
			}
		}

		#endregion

		#region ZdrojStavTmp

		public int? CountDokonceno_ZdrojStavTmp()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_Servis_ZdrojStavTmp WHERE (Rozpracovano >=100)";
					command.CommandType = System.Data.CommandType.Text;

					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj });

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int?)pp;
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

		public int? CountRozpracovano_ZdrojStavTmp()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_Servis_ZdrojStavTmp WHERE (Rozpracovano > 0)";
					command.CommandType = System.Data.CommandType.Text;

					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj });

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int?)pp;
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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable GetDataByZdrojID_ZdrojStavTmp(string ZdrojID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_ZdrojStavTmp WHERE (ZdrojID=@ZdrojID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZdrojID", DbType = System.Data.DbType.String, Value = ZdrojID == null ? (object)DBNull.Value : ZdrojID });

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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable GetData_ZdrojStavTmp()
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojStavTmpDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_ZdrojStavTmp";

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

		public int Update_ZdrojStavTmp(byte Rozpracovano, DateTime? Dokonceno, string ZdrojID)
		{

			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "UPDATE CZMST_Servis_ZdrojStavTmp SET " + 
						" Rozpracovano = @Rozpracovano," + 
						" Dokonceno = @Dokonceno " + 
						" WHERE (ZdrojID = @ZdrojID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Rozpracovano", DbType = System.Data.DbType.Byte, Value = Rozpracovano  });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Dokonceno", DbType = System.Data.DbType.DateTime, Value = Dokonceno.HasValue ? Dokonceno.Value  :(object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZdrojID", DbType = System.Data.DbType.String, Value = ZdrojID == null ? (object)DBNull.Value : ZdrojID, SourceVersion = DataRowVersion.Original });

					return command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				this.Connection_Close();
			}
		}

		public int Insert_ZdrojStavTmp(string ZdrojID, byte Rozpracovano, DateTime? Dokonceno)
		{
 			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "INSERT INTO CZMST_Servis_ZdrojStavTmp " +
					"(ZdrojID, Rozpracovano, Dokonceno) " +
					" VALUES (@ZdrojID,@Rozpracovano,@Dokonceno)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Rozpracovano", DbType = System.Data.DbType.Byte, Value = Rozpracovano  });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Dokonceno", DbType = System.Data.DbType.DateTime, Value = Dokonceno.HasValue ? Dokonceno.Value  :(object)DBNull.Value });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZdrojID", DbType = System.Data.DbType.String, Value = ZdrojID == null ? (object)DBNull.Value : ZdrojID });

					return command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				this.Connection_Close();
			}
		}

		#endregion

		#region Predloha

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable GetData_Predloha()
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_PredlohaDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Predloha";
					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TypeName", DbType = System.Data.DbType.String, Value = TypeName == null ? (object)DBNull.Value : TypeName });

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


		#region Update_Params

		public int Update_Servis_Predloha(object data)
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
					InitializeCommandInsert_Predloha(commandInsert);
					//InitializeCommandUpdate_Predloha(commandUpdate);
					//InitializeCommandDelete_Predloha(commandDelete);
					InitializeCommandSelect_Predloha(commandSelect);

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
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
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

		public void InitializeCommandInsert_Predloha(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Predloha (" + 
				" CountEntries, DOCUMENT_NUMBER, Rozpracovano, OkruhID, UserID, Barcode, ODB_ID" + 
				" ) VALUES ( " + 
				" @CountEntries,@DOCUMENT_NUMBER,@Rozpracovano,@OkruhID,@UserID,@Barcode,@ODB_ID" +
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DOCUMENT_NUMBER", DbType = System.Data.DbType.String, SourceColumn = "DOCUMENT_NUMBER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Rozpracovano", DbType = System.Data.DbType.Byte, SourceColumn = "Rozpracovano" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, SourceColumn = "OkruhID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, SourceColumn = "UserID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" });

		}

		//public void InitializeCommandUpdate_Predloha(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_Predloha(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_Predloha(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Predloha";
		}


		#endregion


		#endregion



		#endregion

		#region Parametry NENI


		#region Update_Params

		public int Update_Params(object data)
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
					//InitializeCommandUpdate(commandUpdate);
					//InitializeCommandDelete(commandDelete);
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
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
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
			command.CommandText = "INSERT INTO Parametry ( " + 
				" CONFIG_KONT_DOKONCENOSTI " + 
				" ) VALUES ( " + 
				" @CONFIG_KONT_DOKONCENOSTI " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DOKONCENOSTI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DOKONCENOSTI" });

		}

		//public void InitializeCommandUpdate(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from Parametry";
		}


		#endregion


		#endregion


		#endregion

		#endregion

	}
}
