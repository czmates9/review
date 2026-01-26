using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Definition_SQL_Struncture.Dokumentace
{
	public class Filter
	{
		//Tenhle je povinny
		public string DESCNAME { get; set; }

		public string TABLE_NAME { get; set; }
		public string COLUMN_NAME { get; set; }
		public string DATA_TYPE { get; set; }
	}


    public class Databaze_Com
    {
		public static int Fill_All(Dokumentace.Dok ds, Filter filter)
		{
			try
			{
				ds.DT_Documentation.Clear();

				using (var con = new System.Data.SqlClient.SqlConnection(Settings.ConnectionString))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{

							var sel = " SELECT " +
							" col.TABLE_NAME as [TABLE], " +
							" tbl.TABLE_TYPE as [typ], " +
							" tbl.TABLE_SCHEMA as [Schema], " +
							" col.COLUMN_NAME as [COLUMN], " +
							" col.DATA_TYPE as [TYPECOL], " +
							" ISNULL(CONVERT(nvarchar(20), col.CHARACTER_MAXIMUM_LENGTH), '-') as [MAXLEN], " +
							" ISNULL(col.COLUMN_DEFAULT, 'null') as [DEFVAL] ,  " +
							" col.IS_NULLABLE as [ISNULL],   " +
							" ISNULL(CONVERT(nvarchar(20), col.NUMERIC_PRECISION), '-') as [NUMPREC], " +
							" ISNULL(CONVERT(nvarchar(20), col.NUMERIC_SCALE), '-') as [NUMSCALE], " +
							" ISNULL(CONVERT(nvarchar(20), col.DATETIME_PRECISION), '-') as [DTPREC], " +
							" ISNULL(prop.value, '') AS [DESCCOL], " +
							" prop.NAME as [DESCNAME] " +
							" FROM INFORMATION_SCHEMA.TABLES AS tbl " +
							"INNER JOIN INFORMATION_SCHEMA.COLUMNS AS col ON col.TABLE_NAME = tbl.TABLE_NAME " +
							" INNER JOIN sys.columns AS sc ON sc.object_id = object_id(tbl.table_schema + '.' + tbl.table_name) " +
							" AND sc.NAME = col.COLUMN_NAME " +
							" LEFT JOIN sys.extended_properties prop ON prop.major_id = sc.object_id " +
							" AND prop.minor_id = sc.column_id ";


							if (!string.IsNullOrEmpty(filter.DESCNAME))
							{
								sel += " AND prop.NAME = '" + filter.DESCNAME.Trim() + "' ";
							}


							sel += " WHERE 1 = 1 ";

							if (!string.IsNullOrEmpty(filter.TABLE_NAME))
							{
								sel += " AND col.TABLE_NAME = '" + filter.TABLE_NAME.Trim() +  "' ";
							}

							if (!string.IsNullOrEmpty(filter.COLUMN_NAME))
							{
								sel += " AND col.COLUMN_NAME = '" + filter.COLUMN_NAME.Trim() +  "' ";
							}

							if (!string.IsNullOrEmpty(filter.DATA_TYPE))
							{
								sel += " AND col.DATA_TYPE = '" + filter.DATA_TYPE.Trim() + "' ";
							}


							sel += " ORDER BY tbl.TABLE_TYPE, col.TABLE_NAME";
							
							
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = sel;

							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(ds, ds.DT_Documentation.TableName);
							return returnValue;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				return -1;
			}
		}

		public static Dokumentace.Dok.DESCNAMEsDataTable Get_DESCNAMEs()
		{
			try
			{
				Dokumentace.Dok.DESCNAMEsDataTable dESCNAMEsRows = new Dok.DESCNAMEsDataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Settings.ConnectionString))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = " SELECT [NAME] FROM sys.extended_properties group by [NAME] ";

							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dESCNAMEsRows);
							return dESCNAMEsRows;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				return null;
			}
		}


		public static bool Delete_Poznamka(string TypPoznamky,string Schema, string Table, string Column)
		{
			try
			{
				
				using (var con = new System.Data.SqlClient.SqlConnection(Settings.ConnectionString))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							com.CommandType = CommandType.StoredProcedure;

							com.CommandText = "sp_dropextendedproperty";

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@name",
								Value = TypPoznamky.Trim()
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter() { 
								ParameterName = "@level0type",
								Value = "Schema"
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@level1type",
								Value = "Table"
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@level2type",
								Value = "Column"
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@level0name",
								Value = Schema.Trim()
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@level1name",
								Value = Table.Trim()
							});

							com.Parameters.Add(new System.Data.SqlClient.SqlParameter()
							{
								ParameterName = "@level2name",
								Value = Column.Trim()
							});

							con.Open();

							var returnValue = com.ExecuteScalar();

							con.Close();

							return true;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				return false;
			}
		}

	}
}
