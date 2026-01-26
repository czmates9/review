using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Fask.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Fask.MST_W_Server
{
	/// <summary>
	/// třida pro dočasný spusob volby typu terminalu
	/// </summary>
	public static class Terminal_Komnikace
	{
		/// <summary>
		/// Metoda ktera ukladá IP terminalu a čas když se pripojit k serveru
		/// Pri každem pripojeni se updatne čas, anebo se uloží nový zaznam s IP pokud neexistuje
		/// </summary>
		/// <param name="IP">IP terminalu</param>
		/// <param name="TermID">ID Terminálu</param>
		internal static void InsertTerminalsByIP2(string IP, string TermID, string TermTyp)
		{
			SqlConnection xConnection1 = null;
			SqlCommand xCommand = null;
			try
			{

				//TODO predelat, nejaku proceduru která zabespečí praci s tymto.... lepe udržovatelne pak

				InsertTyp(TermID, TermTyp);

				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();


                xConnection1 = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB);

				xCommand = new SqlCommand();

				xConnection1.Open();

				xCommand.Connection = xConnection1;
				xCommand.CommandTimeout = 1000;
				xCommand.CommandType = CommandType.Text;

				xCommand.CommandText = @"Update CZMST_TERMINAL_AKT SET DATEREQ=@DATEREQ WHERE IP=@IP AND ID_TERMINAL=@ID_TERMINAL";

				SqlParameterCollection dbparamsA = xCommand.Parameters;
				dbparamsA.AddWithValue("@DATEREQ", DateTime.Now);
				dbparamsA.AddWithValue("@IP", IP);
				dbparamsA.AddWithValue("@ID_TERMINAL", TermID);


				int updatedRows = xCommand.ExecuteNonQuery();
				if (updatedRows > 0)
					return;

				xCommand.CommandText = @"INSERT INTO  CZMST_TERMINAL_AKT " +
					" (ID_TERMINAL, IP, DATEREQ) " +
					" VALUES (@ID_TERMINAL, @IP, @DATEREQ)";

						int insertedRows = xCommand.ExecuteNonQuery();
				if (insertedRows > 0)
					return;
				else
				{
					Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Not inserted IP:" + IP);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.MST_W_Server.Terminal_Komnikace", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
			finally
			{
				try
				{
					if ((xConnection1.State & ConnectionState.Open) == ConnectionState.Open)
						xConnection1.Close();
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Fask.MST_W_Server.Terminal_Komnikace", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				}
			}
		}

		private static void InsertTyp(string TermID, string TermTyp)
        {

            try
            {

				Konfigurace.Classes.Globals_Konfig_WebConfig.LoadConfiguration();


				using (var conn = new SqlConnection(Konfigurace.Classes.Globals_Konfig_WebConfig.Konfigurace.ConnectionString[0].FASKDB))
				{

                    using (var comm = conn.CreateCommand())
                    {
                        
                        conn.Open();

						comm.CommandTimeout = 1000;
						comm.CommandType = CommandType.Text;

						comm.CommandText = @"Update CZMST_TERMINAL_DEFINITION SET DB_TYPE=@DB_TYPE WHERE ID_TERMINAL=@ID_TERMINAL";

						SqlParameterCollection dbparamsA = comm.Parameters;
                        dbparamsA.AddWithValue("@ID_TERMINAL", TermID);
                        dbparamsA.AddWithValue("@DB_TYPE", TermTyp);


                        int updatedRows = comm.ExecuteNonQuery();
                        if (updatedRows > 0)
                            return;


						comm.CommandText = @"INSERT INTO  CZMST_TERMINAL_DEFINITION " +
					" (ID_TERMINAL, DB_TYPE) " +
					" VALUES (@ID_TERMINAL, @DB_TYPE)";


						int insertedRows = comm.ExecuteNonQuery();
						if (insertedRows > 0)
							return;
						else
						{
							Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error, "Not inserted DB_TYPE:" + TermTyp);
						}

					}
				}

			}
			catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }




	}
}
