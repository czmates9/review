using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
    class Lokace
    {
		public static int Count_STAVPOHYB(SqlConnection con, Guid guid)
		{

			try
			{
				using (var com = con.CreateCommand())
				{
					com.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " WHERE guid=@guid";
					com.CommandType = System.Data.CommandType.Text;

					com.Parameters.Add(new SqlParameter() { ParameterName = "@guid", DbType = System.Data.DbType.String, Value = guid, SourceVersion = DataRowVersion.Current });


					con.Open();

					object tmp = com.ExecuteScalar();

					if ((tmp != null) && (tmp is int?))
						return (int)tmp;
					else
						return 0;
				}
			}
			catch (SqlException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return 0;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return 0;
			}
		}

		#region 22.9.2025 MaR oprava a zprovozneni transakce
		internal static ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable Lokace_Ciselnik_Pozic_ABRA_GetData(string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable();

			string baseSql;

			if (!string.IsNullOrEmpty(SKL_ID))
			{
				baseSql = @"
SELECT
    lsp.CODE,
    lsp.NAME,
    lsp.STORE_ID,
    lsp.BARCODE
FROM LogStorePositions lsp
WHERE lsp.STORE_ID = @Store_ID
ORDER BY lsp.CODE;";
			}
			else
			{
				baseSql = @"
SELECT
    lsp.CODE,
    lsp.NAME,
    lsp.STORE_ID,
    lsp.BARCODE
FROM LogStorePositions lsp
ORDER BY lsp.CODE;";
			}

			try
			{
				using (var conn = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				using (var cmd = new FirebirdSql.Data.FirebirdClient.FbCommand(baseSql, conn))
				using (var da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(cmd))
				{
					cmd.CommandType = System.Data.CommandType.Text;

					if (!string.IsNullOrEmpty(SKL_ID))
					{
						// Zvolte typ dle DB schématu (VarChar/Integer/BigInt). Zde ponecháno jako VarChar.
						cmd.Parameters.Add(new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar)
						{
							Value = SKL_ID
						});
					}

					conn.Open();
					da.Fill(table);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", "Lokace_Ciselnik_Pozic_ABRA_GetData", ex);
				Logging.ExceptionHandler2.Handle(table);
			}

			return table;
		}


		#endregion



	}
}
