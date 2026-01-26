using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Fask.MST_W_Server.SQLite_Classes
{
	internal class SQLite_Helper
	{

		public SQLite_Helper()
		{ }



		public bool SQLite_CreateFile(string DB_File, string NameDB)
		{
            SQLiteConnection sqliteConnection = null;
			SQLiteCommand sqliteCommand = null;
			try
			{
				if (!System.IO.File.Exists(DB_File))
				{

					string strSqlCreateTables = System.IO.File.ReadAllText(System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, NameDB + @".sql"));

					SQLiteConnection.CreateFile(DB_File);

					sqliteConnection = new SQLiteConnection("Data source=" + DB_File + ";Version=3;New=False;Compress=True;");

					sqliteConnection.Open();
					sqliteCommand = new SQLiteCommand(strSqlCreateTables, sqliteConnection);
					sqliteCommand.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
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
