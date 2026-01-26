using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.IO;
using Fask.Logging;

namespace SQLiteCommLib.Classes
{
	internal class SQLite_Helper
	{

		//private string FilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
		//public string FilePathSQLite_prd
		//{
		//	get
		//	{
		//		return Path.Combine(FilePath, "SQLiteDatabase\\Vyroba.prd");
		//	}
		//}

		//private string FilePathSQLite_script
		//{
		//	get
		//	{
		//		return Path.Combine(FilePath, "SQLiteDatabase\\Vyroba.prd.sql");
		//	}
		//}

		public SQLite_Helper()
		{ }

		public bool SQLite_CreateFile(string FilePath)
		{
			SQLiteConnection sqliteConnection = null;
			SQLiteCommand sqliteCommand = null;
			try
			{
				if (!System.IO.File.Exists(FilePath))
				{
					var FilePathSQLite_script = FilePath + ".sql";
					string strSqlCreateTables = System.IO.File.ReadAllText(FilePathSQLite_script);

					SQLiteConnection.CreateFile(FilePath);

					sqliteConnection = new SQLiteConnection("Data source=" + FilePath + ";Version=3;New=False;Compress=True;");

					sqliteConnection.Open();
					sqliteCommand = new SQLiteCommand(strSqlCreateTables, sqliteConnection);
					sqliteCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				ExceptionHandler2.Handle(ex);
				//Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				if (sqliteCommand != null)
				{
					sqliteCommand.Dispose();
				}
				if (sqliteConnection != null)
				{
					if ((sqliteConnection.State & ConnectionState.Open) == ConnectionState.Open)
						sqliteConnection.Close();
					sqliteConnection.Dispose();    // potreba rozsirit dataset o dispose a pouzivat (jinak je problem s pristupem k souboru !!)
				}
			}
		}

		//[Obsolete("Nahradit conttrolerem")]
		//public static void Shrink(string connectionString)
		//{
		//	SQLiteConnection sqliteConnection = null;
		//	SQLiteCommand sqliteCommand = null;
		//	try
		//	{
		//		sqliteConnection = new SQLiteConnection(connectionString);

		//		sqliteConnection.Open();
		//		sqliteCommand = new SQLiteCommand("vacuum;", sqliteConnection);
		//		sqliteCommand.ExecuteNonQuery();

		//	}
		//	catch (Exception ex)
		//	{
		//		Fask.Logging.ExceptionHandler2.Handle(ex);
		//		throw ex;
		//	}
		//	finally
		//	{
		//		if (sqliteCommand != null)
		//		{
		//			sqliteCommand.Dispose();
		//		}
		//		if (sqliteConnection != null)
		//		{
		//			if ((sqliteConnection.State & ConnectionState.Open) == ConnectionState.Open)
		//				sqliteConnection.Close();

		//			sqliteConnection.Dispose();    // potreba rozsirit dataset o dispose a pouzivat (jinak je problem s pristupem k souboru !!)
		//		}
		//	}
		//}
	}
}
