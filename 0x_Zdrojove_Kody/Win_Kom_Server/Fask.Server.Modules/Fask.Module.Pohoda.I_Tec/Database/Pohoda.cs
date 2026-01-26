using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;

namespace Fask.Module.Pohoda.I_Tec.Database
{
	/// <summary>
	/// Trida pro komunikaci s DB
	/// </summary>
	class Pohoda
	{

		/// <summary>
		/// Metoda která vrací počet balíku
		/// </summary>
		/// <param name="NMBRPAL">číslo Palety</param>
		/// <param name="IDH">Guid ID Hlavičky</param>
		/// <returns>počet baliku</returns>
		public static int GetPocetBaliku(string NMBRPAL, Guid IDH)
		{
			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			int result = 0;
			try
			{
				Globals_V1.LoadConfiguration();
				da.SelectCommand = new System.Data.SqlClient.SqlCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "select count(*) from ( " +
				"select NMBRBAL from CZMST_Expedice_Baleni_Polozky " +
				"WHERE (NMBRPAL = @NMBRPAL) AND (IDH = @IDH) " +
				"Group by NMBRBAL " +
				") x";

				da.SelectCommand.Parameters.AddWithValue("NMBRPAL", NMBRPAL);
				da.SelectCommand.Parameters.AddWithValue("IDH", IDH);

				da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				da.SelectCommand.Connection.Open();
				object count = da.SelectCommand.ExecuteScalar();

				if (count != null && (count is int))
				{
					result = (int)count;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.Module.Pohoda.I_Tec.Database.Pohoda", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
			finally 
			{

				da.SelectCommand.Connection.Close();
			}

			return result;

		}

        internal static System.Data.DataSet Tisk_GetData(string value)
        {
			System.Data.SqlClient.SqlConnection conn = null;
			System.Data.SqlClient.SqlCommand comm = null;
			System.Data.SqlClient.SqlDataAdapter ada = null;
			System.Data.DataSet ds = new System.Data.DataSet();
			try
			{
				Globals_V1.LoadConfiguration();

				using (conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {

					using (comm = conn.CreateCommand())
                    {

						comm.CommandType = System.Data.CommandType.StoredProcedure;
						comm.CommandText = "FASK_procGetAdresa";
						comm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBE", value));

                        using (ada = new System.Data.SqlClient.SqlDataAdapter(comm))
                        {
							ada.Fill(ds);
							return ds;
						}	
					}
                }
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

        /// <summary>
        /// Metoda která vrací Z DB Pohody z SKz stloupec Doprava pro položku dle ID
        /// </summary>
        /// <param name="ID">ID položky</param>
        /// <returns>Hodnota uložena v stloupci Doprava</returns>
        public static string GetDoprava(int ID)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			SQL_Datasets.PohodaDataSet.SKzDataTable dt = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.PohodaDataSet.SKzDataTable();
			string Doprava = string.Empty;
			try
			{
				Globals_V1.LoadConfiguration();
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT Doprava FROM SKz" +
					" WHERE (ID = ?)";

				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.Fill(dt);

				if ((dt != null) && (dt.Count > 0))
				{
					Doprava =  dt.First().Doprava;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Fask.Module.Pohoda.I_Tec.Database.Pohoda", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Fask.Logging.ExceptionHandler2.Handle(dt);
			}

			return Doprava;
		}


		internal static string GetAllVNDDOCNM(string NMBRPAL, Guid IDH)
		{

			try
			{
				Globals_V1.LoadConfiguration();
				SQL_Datasets.ExpediceTableAdapters.CZMST_SI_VNDDOCNMListTableAdapter ta = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.ExpediceTableAdapters.CZMST_SI_VNDDOCNMListTableAdapter();
				ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


				SQL_Datasets.Expedice.CZMST_SI_VNDDOCNMListDataTable dt =  ta.GetDataVNDDOCNMList(NMBRPAL, IDH);

				if (dt != null && (dt.Count > 0))
				{
					List<string> lst = new List<string>();

					foreach (SQL_Datasets.Expedice.CZMST_SI_VNDDOCNMListRow item in dt)
					{
						string Value = item.IsVNDDOCNMNull() ? string.Empty : item.VNDDOCNM;

						if (!string.IsNullOrEmpty(Value))
						{
							lst.Add(Value.Trim());
						}
					}

					string OutData = String.Join(", ", lst.ToArray());

					return OutData;
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(  LogLevel.Error,"I-Tec provider Tisk Expedice GetAllVNDDOCNM");
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
			}

			return string.Empty;
		}
	}
}
