using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Fask.MST_W.SQLite_Classes
{
	internal class SQLite_Helper
	{

		public SQLite_Helper()
		{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database_fullfilename">cilovy nazev databazoveho souboru</param>
        /// <param name="script_for_create_database">nazev skriptu pro vytvoreni databaze</param>
        /// <returns></returns>
		public bool SQLite_CreateFile(string database_fullfilename, string script_for_create_database)
		{
			try
			{
                if (!System.IO.File.Exists(database_fullfilename))
				{
					string readContents;
                    using (StreamReader streamReader = new StreamReader(script_for_create_database, Encoding.UTF8))
					{
						readContents = streamReader.ReadToEnd();
					}

					//string strSqlCreateTables = System.IO.File.ReadAllText(System.IO.Path.Combine(Fask.MyPath.Path.SQLiteDBsDirectory, NameDB + @".sql"));

                    SQLiteConnection.CreateFile(database_fullfilename);

                    using (var sqliteConnection = new SQLiteConnection("Data source=" + database_fullfilename + ";Version=3;New=False;Compress=True;"))
                    {
                        try
                        {
                            sqliteConnection.Open();
                            using (var sqliteCommand = new SQLiteCommand(readContents, sqliteConnection))
                            {
                                sqliteCommand.ExecuteNonQuery();
                            }
                        }
                        finally
                        {
                            if ((sqliteConnection.State & ConnectionState.Open) == ConnectionState.Open)
                                sqliteConnection.Close();
                        }
                    }
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
			}
		}

	}
}
