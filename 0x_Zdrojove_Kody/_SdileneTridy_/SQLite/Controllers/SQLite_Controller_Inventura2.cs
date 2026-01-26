using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Base trida pro controllery
	/// </summary>
	public class SQLite_Controller_Inventura2 : SQLite_Controller
	{
		#region TableAdaptery

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter ta_hlavicky;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter Ta_hlavicky
		//{
		//    get
		//    {
		//        if (ta_hlavicky == null)
		//        {
		//            ta_hlavicky = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.HLAVICKYTableAdapter();
		//            ta_hlavicky.Connection = this.Connection;
		//        }
		//        return ta_hlavicky;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter ta_inventur;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter Ta_inventur
		//{
		//    get
		//    {
		//        if (ta_inventur == null)
		//        {
		//            ta_inventur = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter();
		//            ta_inventur.Connection = this.Connection;
		//        }
		//        return ta_inventur;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter ta_kancl;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter Ta_kancl
		//{
		//    get
		//    {
		//        if (ta_kancl == null)
		//        {
		//            ta_kancl = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter();
		//            ta_kancl.Connection = this.Connection;
		//        }
		//        return ta_kancl;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter ta_lokace;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter Ta_lokace
		//{
		//    get
		//    {
		//        if (ta_lokace == null)
		//        {
		//            ta_lokace = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter();
		//            ta_lokace.Connection = this.Connection;
		//        }
		//        return ta_lokace;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter ta_majetek;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter Ta_majetek
		//{
		//    get
		//    {
		//        if (ta_majetek == null)
		//        {
		//            ta_majetek = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter();
		//            ta_majetek.Connection = this.Connection;
		//        }
		//        return ta_majetek;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter ta_osoby;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter Ta_osoby
		//{
		//    get
		//    {
		//        if (ta_osoby == null)
		//        {
		//            ta_osoby = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter();
		//            ta_osoby.Connection = this.Connection;
		//        }
		//        return ta_osoby;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter ta_parametry;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter Ta_parametry
		//{
		//    get
		//    {
		//        if (ta_parametry == null)
		//        {
		//            ta_parametry = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter();
		//            ta_parametry.Connection = this.Connection;
		//        }
		//        return ta_parametry;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter ta_Queries;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter Ta_Queries
		//{
		//    get
		//    {
		//        if (ta_Queries == null)
		//        {
		//            ta_Queries = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter();
		//            ta_Queries.Connection = this.Connection;
		//        }
		//        return ta_Queries;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter ta_USCTR;
		//internal Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter Ta_USCTR
		//{
		//    get
		//    {
		//        if (ta_USCTR == null)
		//        {
		//            ta_USCTR = new Fask.SQLiteDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter();
		//            ta_USCTR.Connection = this.Connection;
		//        }
		//        return ta_USCTR;
		//    }
		//}


		#endregion
		
		#region c'tors
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteconnection">Spojeni na databazi davky Inventura</param>
		public SQLite_Controller_Inventura2(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteConnectionstring">Conectionstring na databazi davky Inventura</param>
		public SQLite_Controller_Inventura2(string sqliteFilename)
			: base(sqliteFilename)
		{
		}


		public override void Dispose()
		{
			//this.DisposeObject(ta_hlavicky);
			//this.DisposeObject(ta_inventur);
			//this.DisposeObject(ta_kancl);
			//this.DisposeObject(ta_lokace);
			//this.DisposeObject(ta_majetek);
			//this.DisposeObject(ta_osoby);
			//this.DisposeObject(ta_parametry);
			//this.DisposeObject(ta_Queries);
			//this.DisposeObject(ta_USCTR);

			//ta_hlavicky = null;
			//ta_inventur = null;
			//ta_kancl = null;
			//ta_lokace = null;
			//ta_majetek = null;
			//ta_osoby = null;
			//ta_parametry = null;
			//ta_Queries = null;
			//ta_USCTR = null;

			base.Dispose();
		}

		#endregion


		#region Metody misto TableAdapteru

		#region Hlavicky

		public Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYDataTable GetData_Hlavicky()
		{
			Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYDataTable();
			Fill_Hlavicky(dt);
			return dt;
		}

		public int Fill_Hlavicky(Fask.SQLiteDBs.DataSets.Inventura2.HLAVICKYDataTable dt)
		{
			try
			{
				dt.Clear();
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM HLAVICKY";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return ReturnValue;
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

		public int Insert_Hlavicky(int ID, Guid GUID)
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
						adapter.InsertCommand.CommandText = @"INSERT INTO HLAVICKY (ID, GUID) VALUES (@ID, @GUID)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID });
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

		#endregion

		#region Inventur

		public int? MaxID_Inventur()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT MAX(ID) FROM INVENTUR";
					command.CommandType = System.Data.CommandType.Text;

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

		public int Update_Inventur(object data)
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
					InitializeCommandInsert_INVENTUR(commandInsert);
					InitializeCommandUpdate_INVENTUR(commandUpdate);
					InitializeCommandDelete_INVENTUR(commandDelete);
					InitializeCommandSelect_INVENTUR(commandSelect);

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

		public void InitializeCommandInsert_INVENTUR(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO INVENTUR (ID, KATEGORIE, I_CISLO, NAZEV, STRED, OSOBA, LOKACE1, LOKACE2, KANCELAR, EAN, KUSU, KLIC_LOK, ID_INV, OS_ZPR, ID_TERM, CAS_ZPR, ID_MAJETEK) " + 
				" VALUES " + 
				" (@ID,@KATEGORIE,@I_CISLO,@NAZEV,@STRED,@OSOBA,@LOKACE1,@LOKACE2,@KANCELAR,@EAN,@KUSU,@KLIC_LOK,@ID_INV,@OS_ZPR,@ID_TERM,@CAS_ZPR,@ID_MAJETEK)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KATEGORIE", DbType = System.Data.DbType.String, SourceColumn = "KATEGORIE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@I_CISLO", DbType = System.Data.DbType.String, SourceColumn = "I_CISLO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED", DbType = System.Data.DbType.String, SourceColumn = "STRED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA", DbType = System.Data.DbType.String, SourceColumn = "OSOBA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCELAR", DbType = System.Data.DbType.String, SourceColumn = "KANCELAR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KUSU", DbType = System.Data.DbType.Decimal, SourceColumn = "KUSU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_INV", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_INV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OS_ZPR", DbType = System.Data.DbType.String, SourceColumn = "OS_ZPR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERM", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_TERM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CAS_ZPR", DbType = System.Data.DbType.DateTime, SourceColumn = "CAS_ZPR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_MAJETEK", DbType = System.Data.DbType.Int32, SourceColumn = "ID_MAJETEK" });
		}

		public void InitializeCommandUpdate_INVENTUR(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE INVENTUR " + 
				"SET" + 

				" KATEGORIE = @KATEGORIE," + 
				" I_CISLO = @I_CISLO," + 
				" NAZEV = @NAZEV," + 
				" STRED = @STRED," + 
				" OSOBA = @OSOBA," + 
				" LOKACE1 = @LOKACE1," + 
				" LOKACE2 = @LOKACE2," + 
				" KANCELAR = @KANCELAR," + 
				" EAN = @EAN," + 
				" KUSU = @KUSU," + 
				" KLIC_LOK = @KLIC_LOK," + 
				" ID_INV = @ID_INV," + 
				" OS_ZPR = @OS_ZPR," + 
				" ID_TERM = @ID_TERM," + 
				" CAS_ZPR = @CAS_ZPR," + 
				" ID_MAJETEK = @ID_MAJETEK" + 
				" WHERE (ID = @ID)";
			 
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KATEGORIE", DbType = System.Data.DbType.String, SourceColumn = "KATEGORIE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@I_CISLO", DbType = System.Data.DbType.String, SourceColumn = "I_CISLO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED", DbType = System.Data.DbType.String, SourceColumn = "STRED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA", DbType = System.Data.DbType.String, SourceColumn = "OSOBA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCELAR", DbType = System.Data.DbType.String, SourceColumn = "KANCELAR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KUSU", DbType = System.Data.DbType.Decimal, SourceColumn = "KUSU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_INV", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_INV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OS_ZPR", DbType = System.Data.DbType.String, SourceColumn = "OS_ZPR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERM", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_TERM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CAS_ZPR", DbType = System.Data.DbType.DateTime, SourceColumn = "CAS_ZPR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_MAJETEK", DbType = System.Data.DbType.Int32, SourceColumn = "ID_MAJETEK" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
		}

		public void InitializeCommandDelete_INVENTUR(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM INVENTUR WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_INVENTUR(SQLiteCommand command)
		{
			command.CommandText = "Select * from INVENTUR";
		}


		#endregion

		public int DeleteByID_MAJETEK_Inventur(int ID_MAJETEK)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM INVENTUR WHERE (ID_MAJETEK = @ID_MAJETEK)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_MAJETEK", DbType = System.Data.DbType.Int32, Value = ID_MAJETEK, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

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

		public int Delete_Inventur(int ID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM INVENTUR WHERE (ID = @ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

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

		public int Fill_Inventur(Fask.SQLiteDBs.DataSets.Inventura2.INVENTURDataTable dt)
		{
			try
			{
				dt.Clear();
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM INVENTUR";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return ReturnValue;
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

		#region kancl

		public Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable GetDataByKANCL_Kancl(string KANCL)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM KANCL WHERE (KANCL = @KANCL)";

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCL", DbType = System.Data.DbType.String, Value = KANCL == null ? (object)DBNull.Value : KANCL });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public int Update_KANCL(object data)
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
					InitializeCommandInsert_KANCL(commandInsert);
					InitializeCommandUpdate_KANCL(commandUpdate);
					InitializeCommandDelete_KANCL(commandDelete);
					InitializeCommandSelect_KANCL(commandSelect);

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

		public void InitializeCommandInsert_KANCL(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [KANCL] (" + 
				" [KANCL], [STRE], [TEXT], [NAZEV], [EAN]" + 
				" ) VALUES (" +
				" @KANCL, @STRE, @TEXT, @NAZEV, @EAN" + 
				" )";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCL", DbType = System.Data.DbType.String, SourceColumn = "KANCL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRE", DbType = System.Data.DbType.String, SourceColumn = "STRE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TEXT", DbType = System.Data.DbType.String, SourceColumn = "TEXT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
		}

		public void InitializeCommandUpdate_KANCL(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [KANCL] SET " +
				" [KANCL] = @KANCL, " +
				" [STRE] = @STRE, " +
				" [TEXT] = @TEXT, " +
				" [NAZEV] = @NAZEV, " +
				" [EAN] = @EAN " +
				" WHERE [KANCL] = @KANCL_P";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCL", DbType = System.Data.DbType.String, SourceColumn = "KANCL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRE", DbType = System.Data.DbType.String, SourceColumn = "STRE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TEXT", DbType = System.Data.DbType.String, SourceColumn = "TEXT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCL_P", DbType = System.Data.DbType.String, SourceColumn = "KANCL", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_KANCL(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [KANCL] WHERE (([KANCL] = @KANCL))";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@KANCL",
				DbType = System.Data.DbType.String,
				SourceColumn = "KANCL",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_KANCL(SQLiteCommand command)
		{
			command.CommandText = "Select * from KANCL";
		}


		#endregion


		#endregion

		#region Lokace

		public Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable GetDataByKLIC_LOK_Lokace(int KLIC_LOK)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM LOKACE WHERE (KLIC_LOK = @KLIC_LOK)";
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, Value = KLIC_LOK });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public int Update_LOKACE(object data)
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
					InitializeCommandInsert_LOKACE(commandInsert);
					InitializeCommandUpdate_LOKACE(commandUpdate);
					InitializeCommandDelete_LOKACE(commandDelete);
					InitializeCommandSelect_LOKACE(commandSelect);

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

		public void InitializeCommandInsert_LOKACE(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [LOKACE] (" + 
				" [LOKACE1], [LOKACE2], [EANL], [NAZEV], [KLIC_LOK], [KLIC_KAT], [KLIC_K_OLD]" + 
				" ) VALUES ( " +
				" @LOKACE1, @LOKACE2, @EANL, @NAZEV, @KLIC_LOK, @KLIC_KAT, @KLIC_K_OLD" + 
				" )";
			 
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EANL", DbType = System.Data.DbType.String, SourceColumn = "EANL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_KAT", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_KAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_K_OLD", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_K_OLD" });

		}

		public void InitializeCommandUpdate_LOKACE(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [LOKACE] SET " +
				" [LOKACE1] = @LOKACE1," +
				" [LOKACE2] = @LOKACE2," +
				" [EANL] = @EANL," +
				" [NAZEV] = @NAZEV," +
				" [KLIC_LOK] = @KLIC_LOK," +
				" [KLIC_KAT] = @KLIC_KAT," +
				" [KLIC_K_OLD] = @KLIC_K_OLD" +
				" WHERE [KLIC_LOK] = @KLIC_LOK_P";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EANL", DbType = System.Data.DbType.String, SourceColumn = "EANL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_KAT", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_KAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_K_OLD", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_K_OLD" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK_P", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK", SourceVersion = DataRowVersion.Original});
		}

		public void InitializeCommandDelete_LOKACE(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [LOKACE] WHERE [KLIC_LOK] = @KLIC_LOK";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@KLIC_LOK",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "KLIC_LOK",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_LOKACE(SQLiteCommand command)
		{
			command.CommandText = "Select * from LOKACE";
		}


		#endregion


		#endregion

		#region majetek

		public Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable GetDataByID_Majetek(int ID)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM MAJETEK WHERE (ID = @ID)";
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable GetDataByEAN_Majetek(string EAN)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.MAJETEKDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM MAJETEK WHERE (EAN = @EAN)";
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, Value = EAN == null ? (object)DBNull.Value : EAN });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public int UpdateNactenoByID_Majetek(decimal? NACTENO, int ID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.UpdateCommand = command;
						adapter.UpdateCommand.Connection = this.Connection;

						adapter.UpdateCommand.Connection = this.Connection;
						adapter.UpdateCommand.CommandText = "UPDATE MAJETEK SET NACTENO = NACTENO + @NACTENO WHERE (ID = @ID)";
						adapter.UpdateCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NACTENO", DbType = System.Data.DbType.Decimal, Value = NACTENO.HasValue ? NACTENO.Value : (object)DBNull.Value });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID });

						#endregion

						int returnValue = adapter.UpdateCommand.ExecuteNonQuery();
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

		public int Update_MAJETEK(object data)
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
					InitializeCommandInsert_MAJETEK(commandInsert);
					InitializeCommandUpdate_MAJETEK(commandUpdate);
					InitializeCommandDelete_MAJETEK(commandDelete);
					InitializeCommandSelect_MAJETEK(commandSelect);

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

		public void InitializeCommandInsert_MAJETEK(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [MAJETEK] " + 
				"([ID], [KATEGORIE], [I_CISLO], [NAZEV], [STRED]," + 
				" [OSOBA], [LOKACE1], [LOKACE2], [KANCELAR], [EAN]," + 
				" [KUSU], [KLIC_LOK], [ID_INV], [ID_TERM])" +
				" VALUES " +
				" (@ID, @KATEGORIE, @I_CISLO, @NAZEV, @STRED," +
				" @OSOBA, @LOKACE1, @LOKACE2, @KANCELAR, @EAN," +
				" @KUSU, @KLIC_LOK, @ID_INV, @ID_TERM)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KATEGORIE", DbType = System.Data.DbType.String, SourceColumn = "KATEGORIE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@I_CISLO", DbType = System.Data.DbType.String, SourceColumn = "I_CISLO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED", DbType = System.Data.DbType.String, SourceColumn = "STRED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCELAR", DbType = System.Data.DbType.String, SourceColumn = "KANCELAR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KUSU", DbType = System.Data.DbType.Decimal, SourceColumn = "KUSU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_INV", DbType = System.Data.DbType.String, SourceColumn = "ID_INV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERM", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_TERM" });

		}

		public void InitializeCommandUpdate_MAJETEK(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE [MAJETEK] SET " +
				" [ID] = @ID, " +
				" [KATEGORIE] = @KATEGORIE, " +
				" [I_CISLO] = @I_CISLO, " +
				" [NAZEV] = @NAZEV, " +
				" [STRED] = @STRED, " +
				" [OSOBA] = @OSOBA, " +
				" [LOKACE1] = @LOKACE1, " +
				" [LOKACE2] = @LOKACE2, " +
				" [KANCELAR] = @KANCELAR, " +
				" [EAN] = @EAN, " +
				" [KUSU] = @KUSU, " +
				" [KLIC_LOK] = @KLIC_LOK, " +
				" [ID_INV] = @ID_INV, " +
				" [ID_TERM] = @ID_TERM " +
				" WHERE (([ID] = @ID_P))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KATEGORIE", DbType = System.Data.DbType.String, SourceColumn = "KATEGORIE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@I_CISLO", DbType = System.Data.DbType.String, SourceColumn = "I_CISLO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED", DbType = System.Data.DbType.String, SourceColumn = "STRED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCELAR", DbType = System.Data.DbType.String, SourceColumn = "KANCELAR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KUSU", DbType = System.Data.DbType.Decimal, SourceColumn = "KUSU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_INV", DbType = System.Data.DbType.String, SourceColumn = "ID_INV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERM", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_TERM" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_P", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_MAJETEK(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM MAJETEK WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_MAJETEK(SQLiteCommand command)
		{
			command.CommandText = "Select * from MAJETEK";
		}


		#endregion



		#endregion

		#region osoby

		public Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable GetDataByOSOBA_ZODP_Osoby(int OSOBA_ZODP)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM LOKACE WHERE (KLIC_LOK = @KLIC_LOK)";
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP", DbType = System.Data.DbType.Int32, Value = OSOBA_ZODP });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public int Update_OSOBY(object data)
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
					InitializeCommandInsert_OSOBY(commandInsert);
					InitializeCommandUpdate_OSOBY(commandUpdate);
					InitializeCommandDelete_OSOBY(commandDelete);
					InitializeCommandSelect_OSOBY(commandSelect);

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

		public void InitializeCommandInsert_OSOBY(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [OSOBY] (" + 
				" [OSOBA_ZODP], [TITUL], [PRIJMENI], [JMENO]" + 
				" ) VALUES (" +
				" @OSOBA_ZODP, @TITUL, @PRIJMENI, @JMENO)";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TITUL", DbType = System.Data.DbType.String, SourceColumn = "TITUL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRIJMENI", DbType = System.Data.DbType.String, SourceColumn = "PRIJMENI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@JMENO", DbType = System.Data.DbType.String, SourceColumn = "JMENO" });

		}

		public void InitializeCommandUpdate_OSOBY(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [OSOBY] SET " +
				" [OSOBA_ZODP] = @OSOBA_ZODP," +
				" [TITUL] = @TITUL," +
				" [PRIJMENI] = @PRIJMENI," +
				" [JMENO] = @JMENO " +
				"WHERE [OSOBA_ZODP] = @OSOBA_ZODP_P";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TITUL", DbType = System.Data.DbType.String, SourceColumn = "TITUL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRIJMENI", DbType = System.Data.DbType.String, SourceColumn = "PRIJMENI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@JMENO", DbType = System.Data.DbType.String, SourceColumn = "JMENO" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP_P", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
		}

		public void InitializeCommandDelete_OSOBY(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [OSOBY] WHERE [OSOBA_ZODP] = @OSOBA_ZODP";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@OSOBA_ZODP",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "OSOBA_ZODP",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_OSOBY(SQLiteCommand command)
		{
			command.CommandText = "Select * from OSOBY";
		}


		#endregion


		#endregion

		#region parametry

		public virtual Fask.SQLiteDBs.DataSets.Inventura2.ParametryDataTable GetData_Parametry()
		{
			Fask.SQLiteDBs.DataSets.Inventura2.ParametryDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.ParametryDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM Parametry";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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
					InitializeCommandInsert_Params(commandInsert);
					//InitializeCommandUpdate_Params(commandUpdate);
					//InitializeCommandDelete_Params(commandDelete);
					InitializeCommandSelect_Params(commandSelect);

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

		public void InitializeCommandInsert_Params(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO [Parametry] (" + 
				" [CFG_UpozornitNaPrebytek], [CFG_DalsiPolozkuBezDotazu], [CFG_KontrolaUplnostiPolozky]," + 
				" [CFG_PoZadaniSNZpetNaMN], [CFG_PredvyplnitMnozstvi], [CFG_PredvyplnitMnozstviZbyvajici]," + 
				" [CFG_PredvyplnitMnozstviOJedna], [CFG_PovolitDuplicituSN], [CFG_MnozstviScannerem]," + 
				" [CFG_PosunNaDalsiPolozku], [CFG_PovolitZaporneMnozstvi], [CFG_KontrolaUplnosti]," + 
				" [CFG_PovolitZmenuLokace]" + 
				" ) VALUES (" +
				" @CFG_UpozornitNaPrebytek, @CFG_DalsiPolozkuBezDotazu, @CFG_KontrolaUplnostiPolozky," +
				" @CFG_PoZadaniSNZpetNaMN, @CFG_PredvyplnitMnozstvi, @CFG_PredvyplnitMnozstviZbyvajici," +
				" @CFG_PredvyplnitMnozstviOJedna, @CFG_PovolitDuplicituSN, @CFG_MnozstviScannerem," +
				" @CFG_PosunNaDalsiPolozku, @CFG_PovolitZaporneMnozstvi, @CFG_KontrolaUplnosti," +
				" @CFG_PovolitZmenuLokace)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_UpozornitNaPrebytek", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_UpozornitNaPrebytek" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_DalsiPolozkuBezDotazu", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_DalsiPolozkuBezDotazu" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_KontrolaUplnostiPolozky", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_KontrolaUplnostiPolozky" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PoZadaniSNZpetNaMN", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PoZadaniSNZpetNaMN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstvi", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstvi" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstviZbyvajici", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstviZbyvajici" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstviOJedna", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstviOJedna" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitDuplicituSN", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitDuplicituSN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_MnozstviScannerem", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_MnozstviScannerem" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PosunNaDalsiPolozku", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PosunNaDalsiPolozku" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitZaporneMnozstvi", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitZaporneMnozstvi" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_KontrolaUplnosti", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_KontrolaUplnosti" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitZmenuLokace", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitZmenuLokace" });


		}

		//public void InitializeCommandUpdate_Params(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE INVENTUR " +
		//		"SET" +

		//		" KATEGORIE = @KATEGORIE," +
		//		" I_CISLO = @I_CISLO," +
		//		" NAZEV = @NAZEV," +
		//		" STRED = @STRED," +
		//		" OSOBA = @OSOBA," +
		//		" LOKACE1 = @LOKACE1," +
		//		" LOKACE2 = @LOKACE2," +
		//		" KANCELAR = @KANCELAR," +
		//		" EAN = @EAN," +
		//		" KUSU = @KUSU," +
		//		" KLIC_LOK = @KLIC_LOK," +
		//		" ID_INV = @ID_INV," +
		//		" OS_ZPR = @OS_ZPR," +
		//		" ID_TERM = @ID_TERM," +
		//		" CAS_ZPR = @CAS_ZPR," +
		//		" ID_MAJETEK = @ID_MAJETEK" +
		//		" WHERE (ID = @ID)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KATEGORIE", DbType = System.Data.DbType.String, SourceColumn = "KATEGORIE" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@I_CISLO", DbType = System.Data.DbType.String, SourceColumn = "I_CISLO" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED", DbType = System.Data.DbType.String, SourceColumn = "STRED" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA", DbType = System.Data.DbType.String, SourceColumn = "OSOBA" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE1", DbType = System.Data.DbType.String, SourceColumn = "LOKACE1" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOKACE2", DbType = System.Data.DbType.String, SourceColumn = "LOKACE2" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KANCELAR", DbType = System.Data.DbType.String, SourceColumn = "KANCELAR" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KUSU", DbType = System.Data.DbType.Decimal, SourceColumn = "KUSU" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_LOK", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_LOK" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_INV", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_INV" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OS_ZPR", DbType = System.Data.DbType.String, SourceColumn = "OS_ZPR" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERM", DbType = System.Data.DbType.Decimal, SourceColumn = "ID_TERM" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CAS_ZPR", DbType = System.Data.DbType.DateTime, SourceColumn = "CAS_ZPR" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_MAJETEK", DbType = System.Data.DbType.Int32, SourceColumn = "ID_MAJETEK" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
		//}

		//public void InitializeCommandDelete_Params(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM INVENTUR WHERE (ID = @ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@ID",
		//		DbType = System.Data.DbType.Int32,
		//		SourceColumn = "ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_Params(SQLiteCommand command)
		{
			command.CommandText = "Select * from Parametry";
		}


		#endregion


		#endregion

		#region queries

		public int? ZbyvaPolozek()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM MAJETEK WHERE (KUSU > NACTENO)";
					command.CommandType = System.Data.CommandType.Text;

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

		#endregion

		#region UCSTR

		public virtual Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable GetDataByStredisko_Stredisko(string stredisko)
		{
			Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt = new Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM UCSTR WHERE stredisko=@stredisko";
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@stredisko", DbType = System.Data.DbType.String, Value = stredisko == null ? (object)DBNull.Value : stredisko });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						return dt;
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

		public int Update_UCSTR(object data)
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
					InitializeCommandInsert_UCSTR(commandInsert);
					InitializeCommandUpdate_UCSTR(commandUpdate);
					InitializeCommandDelete_UCSTR(commandDelete);
					InitializeCommandSelect_UCSTR(commandSelect);

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

		public void InitializeCommandInsert_UCSTR(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [UCSTR] (" + 
				" [STREDISKO], [NAZEV], [UCETNI]," + 
				" [STRED2], [CINNOST], [ZAK]," + 
				" [AKTIVNI], [KLIC_STA], [CASZAPSANI]," + 
				" [STRUKT], [STRUKTSEZN]" + 
				" ) VALUES ( " +
				" @STREDISKO, @NAZEV, @UCETNI," +
				" @STRED2, @CINNOST, @ZAK," +
				" @AKTIVNI, @KLIC_STA, @CASZAPSANI," +
				" @STRUKT, @STRUKTSEZN" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STREDISKO", DbType = System.Data.DbType.String, SourceColumn = "STREDISKO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UCETNI", DbType = System.Data.DbType.String, SourceColumn = "UCETNI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED2", DbType = System.Data.DbType.String, SourceColumn = "STRED2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CINNOST", DbType = System.Data.DbType.String, SourceColumn = "CINNOST" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZAK", DbType = System.Data.DbType.String, SourceColumn = "ZAK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@AKTIVNI", DbType = System.Data.DbType.Byte, SourceColumn = "AKTIVNI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_STA", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_STA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CASZAPSANI", DbType = System.Data.DbType.DateTime, SourceColumn = "CASZAPSANI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRUKT", DbType = System.Data.DbType.Byte, SourceColumn = "STRUKT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRUKTSEZN", DbType = System.Data.DbType.String, SourceColumn = "STRUKTSEZN" });

		}

		public void InitializeCommandUpdate_UCSTR(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [UCSTR] SET " +
				" [STREDISKO] = @STREDISKO," +
				" [NAZEV] = @NAZEV," +
				" [UCETNI] = @UCETNI," +
				" [STRED2] = @STRED2," +
				" [CINNOST] = @CINNOST," +
				" [ZAK] = @ZAK," +
				" [AKTIVNI] = @AKTIVNI," +
				" [KLIC_STA] = @KLIC_STA," +
				" [CASZAPSANI] = @CASZAPSANI," +
				" [STRUKT] = @STRUKT," +
				" [STRUKTSEZN] = @STRUKTSEZN" +
				" WHERE [STREDISKO] = @STREDISKO_P";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STREDISKO", DbType = System.Data.DbType.String, SourceColumn = "STREDISKO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAZEV", DbType = System.Data.DbType.String, SourceColumn = "NAZEV" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UCETNI", DbType = System.Data.DbType.String, SourceColumn = "UCETNI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRED2", DbType = System.Data.DbType.String, SourceColumn = "STRED2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CINNOST", DbType = System.Data.DbType.String, SourceColumn = "CINNOST" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZAK", DbType = System.Data.DbType.String, SourceColumn = "ZAK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@AKTIVNI", DbType = System.Data.DbType.Byte, SourceColumn = "AKTIVNI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KLIC_STA", DbType = System.Data.DbType.Int32, SourceColumn = "KLIC_STA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CASZAPSANI", DbType = System.Data.DbType.DateTime, SourceColumn = "CASZAPSANI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRUKT", DbType = System.Data.DbType.Byte, SourceColumn = "STRUKT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STRUKTSEZN", DbType = System.Data.DbType.String, SourceColumn = "STRUKTSEZN" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@STREDISKO_P", DbType = System.Data.DbType.String, SourceColumn = "STREDISKO" });

		}

		public void InitializeCommandDelete_UCSTR(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [UCSTR] WHERE [STREDISKO] = @STREDISKO";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@STREDISKO",
				DbType = System.Data.DbType.String,
				SourceColumn = "STREDISKO",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_UCSTR(SQLiteCommand command)
		{
			command.CommandText = "Select * from UCSTR";
		}


		#endregion


		#endregion

		#endregion

		#region Ostatne metody

		/// <summary>
		/// Pokud existuji nasnimana data, vraci true, jinak false
		/// </summary>
		public bool dataExists()
		{
			bool dataexists = false;

			using (var sqlcomm = new System.Data.SQLite.SQLiteCommand())
			{
				this.Connection_Open();
				try
				{
					sqlcomm.CommandText = "Select I_CISLO from INVENTUR";
					sqlcomm.Connection = this.Connection;
					using (System.Data.SQLite.SQLiteDataReader sqlreader = sqlcomm.ExecuteReader(CommandBehavior.SingleRow))
					{
						dataexists = sqlreader.Read();
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
			return dataexists;
		}

		#region LoadMetody

		public void LoadStrediska(Fask.SQLiteDBs.DataSets.Inventura2.UCSTRDataTable dt_UCSTR, string select, int indexStart, int indexEnd)
		{
			try
			{
				Connection_Open();

				using (var scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = select;
					using (var sceresultset = scecommand.ExecuteReader())
					{
						dt_UCSTR.BeginLoadData();
						dt_UCSTR.Clear();

						// najeti na startovaci index pro sqlite
						for (int index = 0; index <= indexStart; index++)
						{
							if (!sceresultset.Read())
								break;
						}

						int i = indexStart;
						//for (int i = indexStart; i < indexEnd; i++)
						do
						{
							_Routines.LoadRowFromReader(sceresultset, dt_UCSTR);

							i++;
							if (!sceresultset.Read())
								break;
						} while (i < indexEnd);

						dt_UCSTR.EndLoadData();

					}
				}
			}
			catch (Exception exObecna)
			{
				Logging.ExceptionHandler2.Handle(exObecna);
			}
			finally
			{
				Connection_Close();
			}
		}

		public void LoadOSOBY(Fask.SQLiteDBs.DataSets.Inventura2.OSOBYDataTable dt_OSOBY, string select, int indexStart, int indexEnd)
		{
			try
			{
				Connection_Open();

				using (var scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = select;
					using (var sceresultset = scecommand.ExecuteReader())
					{
						dt_OSOBY.BeginLoadData();
						dt_OSOBY.Clear();

						// najeti na startovaci index pro sqlite
						for (int index = 0; index <= indexStart; index++)
						{
							if (!sceresultset.Read())
								break;
						}

						int i = indexStart;
						//for (int i = indexStart; i < indexEnd; i++)
						do
						{
							_Routines.LoadRowFromReader(sceresultset, dt_OSOBY);

							i++;
							if (!sceresultset.Read())
								break;
						} while (i < indexEnd);

						dt_OSOBY.EndLoadData();

					}
				}
			}
			catch (Exception exObecna)
			{
				Logging.ExceptionHandler2.Handle(exObecna);
			}
			finally
			{
				Connection_Close();
			}
		}

		public void LoadLokace(Fask.SQLiteDBs.DataSets.Inventura2.LOKACEDataTable dt_LOKACE, string select, int indexStart, int indexEnd)
		{
			try
			{
				Connection_Open();

				using (var scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = select;
					using (var sceresultset = scecommand.ExecuteReader())
					{
						dt_LOKACE.BeginLoadData();
						dt_LOKACE.Clear();

						// najeti na startovaci index pro sqlite
						for (int index = 0; index <= indexStart; index++)
						{
							if (!sceresultset.Read())
								break;
						}

						int i = indexStart;
						//for (int i = indexStart; i < indexEnd; i++)
						do
						{
							_Routines.LoadRowFromReader(sceresultset, dt_LOKACE);

							i++;
							if (!sceresultset.Read())
								break;
						} while (i < indexEnd);

						dt_LOKACE.EndLoadData();

					}
				}
			}
			catch (Exception exObecna)
			{
				Logging.ExceptionHandler2.Handle(exObecna);
			}
			finally
			{
				Connection_Close();
			}
		}

		public void LoadKancl(Fask.SQLiteDBs.DataSets.Inventura2.KANCLDataTable dt_KANCL, string select, int indexStart, int indexEnd)
		{
			try
			{
				Connection_Open();

				using (var scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = select;
					using (var sceresultset = scecommand.ExecuteReader())
					{
						dt_KANCL.BeginLoadData();
						dt_KANCL.Clear();

						// najeti na startovaci index pro sqlite
						for (int index = 0; index <= indexStart; index++)
						{
							if (!sceresultset.Read())
								break;
						}

						int i = indexStart;
						//for (int i = indexStart; i < indexEnd; i++)
						do
						{
							_Routines.LoadRowFromReader(sceresultset, dt_KANCL);

							i++;
							if (!sceresultset.Read())
								break;
						} while (i < indexEnd);

						dt_KANCL.EndLoadData();

					}
				}
			}
			catch (Exception exObecna)
			{
				Logging.ExceptionHandler2.Handle(exObecna);
			}
			finally
			{
				Connection_Close();
			}
		}


		#endregion

		#endregion
	}
}
