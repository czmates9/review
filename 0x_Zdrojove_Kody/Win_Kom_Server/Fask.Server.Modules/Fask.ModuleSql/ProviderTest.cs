using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql
{
	public partial class Provider : Fask.Server.Interfaces.Test.ITest,
		Fask.Server.Interfaces.Test.ITest_Komunikace
	{
		public string Komunikace()
		{
			try
			{
				string status = "OK";


				Communication.EXE_Comunication.Initialize();

				List<string> vs = new List<string>() 
				{
					"/AUTOMAT:MST_TEST"
				};

				if (!Communication.EXE_Comunication.Communicate(vs))
				{
					return "Komunikace neuspěšná...";
				}

				Communication.EXE_Comunication.Terminate();
				
				///////
				var dbytperequested = "firebird"; // dbtype z dynamickeho volani ... 
				// dbtype nebude / null ? => pouzji `mssql` type...

				var dbtype = Globals.Konfigurace.dbtypes.First(db => db.dbtype == dbytperequested);

				System.Data.Common.DbConnection dbConnection;


				if (dbtype.dbtype == "mssql")
				{
					dbConnection = new System.Data.SqlClient.SqlConnection(dbtype.dbconnectionstring);
				}
				else if (dbtype.dbtype == "firebird")
				{
					dbConnection = new NpgsqlConnection(dbtype.dbconnectionstring);
				}
				else
				{
					throw new Exception("unknown connection type");
				}
				////////

				var dbcommand = dbConnection.CreateCommand();
				dbcommand.CommandType = System.Data.CommandType.Text; // System.Data.CommandType.Text | System.Data.CommandType.TableDirect | System.Data.CommandType.StoredProcedure
				dbcommand.CommandText = "neco z procedury..."; // zdroj z dymamickeho volani ...
				dbcommand.Connection.Open();
				dbcommand.ExecuteReader();

				//Npgsql.NpgsqlDataReader()

				return status;
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return ex.Message;
			}
		}
	}
}
