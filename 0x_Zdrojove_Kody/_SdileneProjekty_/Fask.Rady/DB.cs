using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;

namespace Fask.Rady
{
	internal static class DB
	{
		#region Template
		//public static Datasets.DatabasePohoda.XXX_DataTable XXX_GetData(int? RefSKz, string Cislo)
		//{
		//    Datasets.DatabasePohoda.XXX_DataTable dataTable = new Fask.ModulePohodaXML.Datasets.DatabasePohoda.XXX_DataTable();
		//    XXX_Fill(dataTable, RefSKz, Cislo);
		//    return dataTable;
		//}

		//public static void XXX_Fill(Datasets.DatabasePohoda.XXX_DataTable dataTable, string XXX)
		//{

		//    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
		//    try
		//    {
		//        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
		//        da.SelectCommand.CommandType = System.Data.CommandType.Text;

		//        da.SelectCommand.CommandText = @"";
		//        da.SelectCommand.Parameters.AddWithValue("?", XXX);

		//        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Fask.ModulePohodaXML.Globals.ConnectionStringPohodaDB);

		//        da.Fill(dataTable);
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.writeErrorLog("Pohoda Tabulky Definice SQL Dotazu", " XXX_Fill", ex.Message);
		//        Log.writeErrorData(dataTable);
		//    }

		//}
		
		#endregion

		private static string _connectionString;
		public static  string ConnectionString
		{
			set { _connectionString = value; }
			get { return _connectionString; }
		}


		#region SQL

		public static DS_Rady.FASK_RADYDataTable SQL_FASK_RADY_GetData()
		{
			DS_Rady.FASK_RADYDataTable dataTable = new DS_Rady.FASK_RADYDataTable();
			SQL_FASK_RADY_Fill(dataTable);
			return dataTable;
		}

		public static void SQL_FASK_RADY_Fill(DS_Rady.FASK_RADYDataTable dataTable)
		{

			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT * FROM FASK_RADY";

				da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(_connectionString);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.Rady.DB", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		public static void SQL_FASK_RADY_Fill_CustomSQLQuery(string SQL_Dotaz,DS_Rady.FASK_RADYDataTable dataTable)
		{

			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				da.SelectCommand.CommandText = SQL_Dotaz;
				da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(_connectionString);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.Rady.DB", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		} 
		#endregion


	}
}
