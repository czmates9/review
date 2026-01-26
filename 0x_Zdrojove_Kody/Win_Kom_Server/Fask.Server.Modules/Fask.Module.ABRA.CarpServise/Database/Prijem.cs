using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.Module.ABRA.CarpServise.Database
{
	class Prijem
	{

		public static byte? CZMSTPE_PONUMBER_CZDOSLO(string sopnumber)
		{
			System.Data.SqlClient.SqlConnection conn = null;
			try
			{

				conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				string select = "SELECT DISTINCT CZ_Doslo " +
					"FROM CZMST_PE " +
					"WHERE (PONUMBER ='" + sopnumber + "') AND (CZ_Doslo < 100)";

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

		public static bool CZMSTPE_UPDATE_CZDOSLO(string sopnumber)
		{
			try
			{

				System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				string select = "UPDATE CZMST_PE " +
								"SET CZ_Doslo = 201 " +
								"WHERE (PONUMBER = '" + sopnumber + "') AND (CZ_Doslo = 0) ";

				SqlCommand cmd = new SqlCommand(select, conn);

				int tmp = cmd.ExecuteNonQuery();

				return tmp > 0 ? true : false;
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

		public static bool UpdateCzDosloByCountEntries(byte CZ_Doslo, int CountEntries)
		{
			try
			{

				System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				string select = "UPDATE CZMST_PE SET CZ_Doslo = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";

				SqlCommand cmd = new SqlCommand(select, conn);

				int tmp = cmd.ExecuteNonQuery();

				return tmp > 0 ? true : false;
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

		public static SQL_Datasets.Prijem.CZMST_PIDataTable GetDataByCountEntries(int CountEntries)
		{

			SQL_Datasets.Prijem.CZMST_PIDataTable dataTable = new SQL_Datasets.Prijem.CZMST_PIDataTable();


			System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter();
			try
			{
				da.SelectCommand = new SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText =
				@"SELECT CZ_CarKod, CountEntries, DATEDONE, DAT_VYROBY, DEX_ROW_ID, GUID, ID_TERMINAL, INPUT_MODE, ITEMNMBR, KOD_SW, LOCNCODE, MJ, ORD, " +
				" PONUMBER, QTYPACK, QTYSHPPD, QTYSHPPDMJ, REZ_1, REZ_2, SERLTNUM, TIMEDONE, USER_ID, VNDDOCNM, VNDITNUM, SKL_ID " +
				" FROM CZMST_PI " +
				" WHERE (CountEntries = '" + CountEntries.ToString() + "')";

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

		public static int CZMSTPE_MAX_CountEntries()
		{
			System.Data.SqlClient.SqlCommand sqlcommand = null;
			try
			{
				sqlcommand = new System.Data.SqlClient.SqlCommand();
				sqlcommand.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				sqlcommand.CommandType = System.Data.CommandType.Text;
				sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_PE";
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
			finally
			{
				if ((sqlcommand.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					sqlcommand.Connection.Close();
			}


		}


	}
}
