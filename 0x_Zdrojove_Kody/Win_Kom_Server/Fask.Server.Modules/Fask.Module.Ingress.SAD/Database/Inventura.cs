using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
	class Inventura
	{

		public static int Fill_DataSet(string select, DataSet ds, string TableName)
		{
			Globals.LoadConfiguration();
			try
			{
				using (var con = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					using (var ada = new IngresDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = select;
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(ds, TableName);
							return returnValue;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return -1;
			}
		}

		public static bool UpdateCzDosloByCountEntries(byte CZ_Doslo, int CountEntries)
		{
			try
			{

				using (IngresConnection conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					string select = "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " SET TerminalID = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";

					IngresCommand cmd = new IngresCommand(select, conn);

					conn.Open();

					int tmp = cmd.ExecuteNonQuery();

					conn.Close();

					return tmp > 0 ? true : false;
				}
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
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

		public static bool UpdateCzDosloByCountEntries(IngresConnection con, IngresTransaction tran, byte CZ_Doslo, int CountEntries)
		{
			try
			{
				int tmp = 0;

				using (var com = con.CreateCommand())
				{
					com.Transaction = tran;
					com.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_I1 + " SET TerminalID = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";
					com.CommandType = CommandType.Text;

					tmp = com.ExecuteNonQuery();

					return tmp > 0 ? true : false;
				}
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
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
