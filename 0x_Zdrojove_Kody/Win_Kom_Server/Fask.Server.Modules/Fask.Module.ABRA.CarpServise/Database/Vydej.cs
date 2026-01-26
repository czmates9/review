using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.Module.ABRA.CarpServise.Database
{
	class Vydej
	{

		public static byte? CZMSTSE_SOPNUMBER_CZDOSLO(string sopnumber)
		{
			System.Data.SqlClient.SqlConnection conn = null;
			try
			{

				conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				string select = "SELECT DISTINCT CZ_Doslo " +
					"FROM CZMST_SE " +
					"WHERE (SOPNUMBE ='" + sopnumber + "') AND (CZ_Doslo < 100)";

				SqlCommand cmd = new SqlCommand(select, conn);
				cmd.Connection.Open();
				object tmp = cmd.ExecuteScalar();

				if ((tmp != null) && (tmp is byte?))
					return (byte?)tmp;
				else
					return null;
			}
			catch (SqlException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
				if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					conn.Close();
			}
		}

		public static bool CZMSTSE_UPDATE_CZDOSLO(string sopnumber)
		{
			//System.Data.SqlClient.SqlConnection conn = null;
			try
			{
				using (var connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					string select = "UPDATE CZMST_SE " +
									"SET CZ_Doslo = 201 " +
									"WHERE (SOPNUMBE = '" + sopnumber + "') AND (CZ_Doslo = 0) ";

					var command = new SqlCommand(select, connection);

					connection.Open();
					int tmp = command.ExecuteNonQuery();

					return tmp > 0 ? true : false;
				}
			}
			catch (SqlException sqlex)
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
				//if ((conn != null) && ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
				//	conn.Close();
			}
		}

		public static bool UpdateCzDosloByCountEntries(byte CZ_Doslo, int CountEntries)
		{
			try
			{
				using (var connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					string select = "UPDATE CZMST_SE SET CZ_Doslo = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";

					using (var cmd = new SqlCommand(select, connection))
					{
						connection.Open();
						int tmp = cmd.ExecuteNonQuery();
						return tmp > 0 ? true : false;
					}
				}
			}
			catch (SqlException sqlex)
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

		public static SQL_Datasets.Vydej.CZMST_SIDataTable GetDataByCountEntries(int CountEntries)
		{

			SQL_Datasets.Vydej.CZMST_SIDataTable dataTable = new SQL_Datasets.Vydej.CZMST_SIDataTable();


			System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter();
			try
			{
				da.SelectCommand = new SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText =
					@"SELECT CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, VNDITNUM, CZ_CarKod, SKL_ID, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, " +
					" DAT_VYROBY, REZ_1, REZ_2, ODBER_ID, DATEDONE, TIMEDONE, USER_ID, TYPEPAL, NMBRPAL, PRINTED, DEX_ROW_ID, GUID, INPUT_MODE, ID_TERMINAL, " +
					" MJ FROM CZMST_SI WHERE (CountEntries = '" + CountEntries.ToString() + "')";


				da.SelectCommand.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				da.SelectCommand.Connection.Open();
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(dataTable);
				Logging.ExceptionHandler2.Handle(ex);
			}

			return dataTable;

		}

		public static int CZMSTSE_MAX_CountEntries()
		{
			System.Data.SqlClient.SqlCommand sqlcommand = null;
			try
			{
				sqlcommand = new System.Data.SqlClient.SqlCommand();
				sqlcommand.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				sqlcommand.CommandType = System.Data.CommandType.Text;
				sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_SE";
				sqlcommand.Connection.Open();
				object o = sqlcommand.ExecuteScalar();


				try
				{

					int countentries = Convert.ToInt32(o);
					return countentries;
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle(e);
					return 0; //pokud nenalezeno ... ???
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				if ((sqlcommand.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					sqlcommand.Connection.Close();
			}


		}


	}
}
