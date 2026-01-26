using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
    public class Inventura
    {
		public SQL_Datasets.Inventura.CZMST093DataTable GetData_CZMST093()
		{
			Globals_V1.LoadConfiguration();
			try
			{
				SQL_Datasets.Inventura.CZMST093DataTable dt = new SQL_Datasets.Inventura.CZMST093DataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST093;
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public SQL_Datasets.Inventura.CZMST_I1DataTable GetDataByCountEntries_I1(int CountEntries)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				SQL_Datasets.Inventura.CZMST_I1DataTable dt = new SQL_Datasets.Inventura.CZMST_I1DataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1 + " WHERE (CountEntries = " + CountEntries.ToString() + ")";
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public SQL_Datasets.Inventura.CZMST_I4DataTable GetDataByCountEntries_I4(int CountEntries)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				SQL_Datasets.Inventura.CZMST_I4DataTable dt = new SQL_Datasets.Inventura.CZMST_I4DataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I4 + " WHERE (CountEntries = " + CountEntries.ToString() + ")";
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable GetDataByCountEntries_I4_Grupa_Mnozstvi(int CountEntries)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable dt = new SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							//ada.SelectCommand.CommandText = " SELECT CountEntries, ITEMNMBR, CZ_CarKod, LOCNCODE, SKL_ID, VNDITNUM, MJ, Sum(QUANTITY), SERLNMBR, USERID, ID_TERMINAL, ITEMCODE FROM " + Constants.Common.TABLE_CZMST_I4 +
							//	" WHERE (CountEntries = " + CountEntries.ToString() + ")" +
							//	" Group by CountEntries, ITEMNMBR, CZ_CarKod, LOCNCODE, SKL_ID, VNDITNUM, MJ, SERLNMBR, USERID, ID_TERMINAL, ITEMCODE";

							ada.SelectCommand.CommandText =
							" SELECT " +
							" I4.ITEMNMBR, " +
							" I4.SKL_ID, " +
							" Sum(I4.QUANTITY) as QUANTITY, " +
							" I1.CZ_SerNum_Track,  " +
							" I3.MJ " +
							" FROM " + Constants.Common.TABLE_CZMST_I4 + " as I4"+
							" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I4.CountEntries AND I1.SKL_ID = I4.SKL_ID AND I1.ITEMNMBR = I4.ITEMNMBR " +
							" LEFT JOIN " + Constants.Common.TABLE_CZMST_I3 + " as I3 ON I3.CountEntries = I4.CountEntries AND I3.ITEMNMBR = I4.ITEMNMBR " +
							" WHERE 1 = 1" +
							" AND I3.QTYPACK = 0 " + 
							" AND I4.CountEntries = " + CountEntries.ToString() + 
							" AND I1.CZ_SerNum_Track = 0 " +
							" Group by I4.CountEntries, I4.ITEMNMBR, I4.SKL_ID, I1.CZ_SerNum_Track, I3.MJ";


							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable GetDataByCountEntries_I4_Grupa_SARZE_SN(int CountEntries, bool SarzeNeznama)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable dt = new SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable();

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;

							#region MyRegion
							ada.SelectCommand.CommandText =
							" SELECT " +
							" I4x.ITEMNMBR, " +
							" Sum(i4x.QUANTITY) as QUANTITY, " +
							" I4x.SERLNMBR, " +
							" I4x.SKL_ID, " +
							" I1.CZ_SerNum_Track, " +
							" I3.MJ, " +
							" i4x.Expirace " +
							" FROM " +
							" ( " +
							" SELECT CountEntries, SKL_ID, ITEMNMBR, SERLNMBR, QUANTITY, QUANTITYMJ, MJ, Expirace " +
							" FROM " + Constants.Common.TABLE_CZMST_I4 + " as I4 " +
							" where ";
							
							if(SarzeNeznama)
							{
								ada.SelectCommand.CommandText += " not ";
							}

								ada.SelectCommand.CommandText +=
							" exists(select 1 from " + Constants.Common.TABLE_CZMST_I2 + " i2x where i2x.CountEntries = i4.CountEntries and i2x.ITEMNMBR = I4.ITEMNMBR and i2x.SERLNMBR = I4.SERLNMBR and i2x.Expirace = I4.Expirace) " +
							" ) as i4x " +
							" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I4x.CountEntries AND I1.SKL_ID = I4x.SKL_ID AND I1.ITEMNMBR = I4x.ITEMNMBR " +
							" LEFT JOIN " + Constants.Common.TABLE_CZMST_I3 + "  as I3 ON I3.CountEntries = I4x.CountEntries AND I3.ITEMNMBR = I4x.ITEMNMBR " +
							" where 1 = 1 " +
							" AND I3.QTYPACK = 0 " + // Ale co ked tam žadna neni?? hmm pruser....
							" AND (I1.CZ_SerNum_Track in (1, 2)) " +
							" AND I4x.CountEntries = " + CountEntries.ToString() + " " +
							" Group by I4x.CountEntries, I4x.ITEMNMBR, I4x.SKL_ID, I4x.SERLNMBR, I1.CZ_SerNum_Track, I4x.Expirace, I3.MJ ";

							#endregion


							//ada.SelectCommand.CommandText = " SELECT I4.ITEMNMBR, I4.SKL_ID, Sum(I4.QUANTITY) as QUANTITY , I4.SERLNMBR, I1.CZ_SerNum_Track FROM " + Constants.Common.TABLE_CZMST_I4 + " as I4 " +
							//" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I4.CountEntries AND I1.SKL_ID = I4.SKL_ID AND I1.ITEMNMBR = I4.ITEMNMBR AND I1.ITEMCODE = I4.ITEMCODE " +
							//" INNER JOIN " + Constants.Common.TABLE_CZMST_I2 + " as I2 ON I2.CountEntries = I4.CountEntries AND I2.ITEMNMBR = I4.ITEMNMBR AND I4.SERLNMBR = I2.SERLNMBR " +
							//" WHERE " +
							//" (I4.CountEntries = " + CountEntries.ToString() + ") AND(I1.CZ_SerNum_Track = 1 OR I1.CZ_SerNum_Track = 2) " +
							//" Group by I4.CountEntries, I4.ITEMNMBR, I4.SKL_ID, I4.SERLNMBR, I1.CZ_SerNum_Track ";



							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		//public SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable GetDataByCountEntries_I4_Grupa_Mnozstvi_NeznameSarze(int CountEntries)
		//{
		//	Globals_V1.LoadConfiguration();
		//	try
		//	{
		//		SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable dt = new SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable();

		//		using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
		//		{
		//			using (var ada = new System.Data.SqlClient.SqlDataAdapter())
		//			{
		//				using (var com = con.CreateCommand())
		//				{
		//					ada.SelectCommand = com;

		//					ada.SelectCommand.CommandText =
		//						" SELECT I4.ITEMNMBR, I4.LOCNCODE, I4.SKL_ID, Sum(I4.QUANTITY) as QUANTITY , I4.SERLNMBR, I4.USERID, I1.CZ_SerNum_Track FROM " + Constants.Common.TABLE_CZMST_I4 + " as I4" +
		//						" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I4.CountEntries AND I1.SKL_ID = I4.SKL_ID AND I1.ITEMNMBR = I4.ITEMNMBR AND I1.ITEMCODE = I4.ITEMCODE" +
		//						" INNER JOIN CZMST_I2 as I2 ON I2.CountEntries = I4.CountEntries AND I2.ITEMNMBR = I4.ITEMNMBR AND  I4.SERLNMBR != I2.SERLNMBR " +
		//						" WHERE (I4.CountEntries = " + CountEntries.ToString() + ") AND ( I1.CZ_SerNum_Track = 1 OR I1.CZ_SerNum_Track = 2 ) " +
		//						" Group by I4.CountEntries, I4.ITEMNMBR, I4.CZ_CarKod, I4.LOCNCODE, I4.SKL_ID, I4.VNDITNUM, I4.MJ, I4.SERLNMBR, I4.USERID, I4.ID_TERMINAL, I4.ITEMCODE, I1.CZ_SerNum_Track";


		//					ada.SelectCommand.Connection = con;
		//					ada.SelectCommand.CommandType = CommandType.Text;

		//					int returnValue;
		//					returnValue = ada.Fill(dt);
		//					return dt;
		//				}
		//			}
		//		}
		//	}
		//	catch (System.Exception ex)
		//	{
		//		Fask.Logging.ExceptionHandler2.Handle(ex);
		//		return null;
		//	}
		//}


		public int? CZMSTI1H_MAX_CountEntries()
		{

			try
			{
				Globals_V1.LoadConfiguration();

				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = "SELECT MAX( CountEntries ) FROM " + Constants.Common.TABLE_CZMST_I1H;
						com.Connection.Open();
						object o = com.ExecuteScalar();

						try
						{
							int countentries = Convert.ToInt32(o);
							return countentries + 1; // +1 je z duvodu že tohle ID se už použije pro další dávku
						}
						catch (Exception e)
						{
							Logging.ExceptionHandler2.Handle(e);
							return 1; //pokud nenalezeno ... ???
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		internal string Get_Desc_I1H(int CountEntries)
		{
			try
			{
				Globals_V1.LoadConfiguration();

				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = "SELECT Description FROM " + Constants.Common.TABLE_CZMST_I1H + " WHERE (CountEntries = " + CountEntries.ToString() + ")";
						com.Connection.Open();
						object o = com.ExecuteScalar();

						try
						{
							if (o is string)
								return (string)o;
							else
								return null;
						}
						catch (Exception e)
						{
							Logging.ExceptionHandler2.Handle(e);
							return null; 
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

        public int? Get_CountEntries_I1H(string Description)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var com = con.CreateCommand())
                    {

                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = "SELECT CountEntries FROM " + Constants.Common.TABLE_CZMST_I1H + " WHERE (Description = '" + Description.Trim() + "')";
                        com.Connection.Open();
                        object o = com.ExecuteScalar();

                        try
                        {
                            if (o is int)
                                return (int)o;
                            else
                                return null;
                        }
                        catch (Exception e)
                        {
                            Logging.ExceptionHandler2.Handle(e);
                            return null;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

		public int? GetCountRowI4(string Description, int CountEntries)
		{
			try
			{
				Globals_V1.LoadConfiguration();

				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = " SELECT count(*)  FROM " + Constants.Common.TABLE_CZMST_I4 + " as I4 " +
											" left join " + Constants.Common.TABLE_CZMST_I1H + " as I1H on I1H.CountEntries = I4.CountEntries " +
											" where 1=1 " +
											" AND I1H.Description = '" + Description.Trim() + "' " +
											" AND I1H.CountEntries = " + CountEntries.ToString() +
											" Group by I4.CountEntries ";
						com.Connection.Open();
						object o = com.ExecuteScalar();

						try
						{
							if (o is int)
								return (int)o;
							else
								return null;
						}
						catch (Exception e)
						{
							Logging.ExceptionHandler2.Handle(e);
							return null;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public SQL_Datasets.Inventura.StavyRow Get_TerminalID_I1(string DIP)
		{
			try
			{
				SQL_Datasets.Inventura invDS = new SQL_Datasets.Inventura();

				Globals_V1.LoadConfiguration();

				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{

						com.CommandType = System.Data.CommandType.Text;
						//com.CommandText = "SELECT TerminalID FROM " + Constants.Common.TABLE_CZMST_I1 + " WHERE (CountEntries = " + CountEntries.ToString() + ")";
						com.CommandText = "SELECT I1.TerminalID , I1.CountEntries FROM " + Constants.Common.TABLE_CZMST_I1H + " as I1H " +
							" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I1H.CountEntries " +
							" WHERE (Description = '" + DIP + "') " +
							" group by I1.TerminalID, I1.CountEntries";
						com.Connection.Open();

                        using (var ada = new SqlDataAdapter())
                        {
							ada.SelectCommand = com;
							ada.Fill(invDS, invDS.Stavy.TableName);
						}
						com.Connection.Close();


						if (invDS.Stavy == null || invDS.Stavy.Count == 0)
						{
							return null;
						}
						else if (invDS.Stavy.Count == 1)
						{
							return invDS.Stavy.First();
						}
						else
						{
							if (invDS.Stavy.Any(x => x.TerminalID < 100))
							{
								var rows = invDS.Stavy.Where(x => x.TerminalID < 100);

								if (rows.Count() == 1)
								{
									return rows.First();
								}
								else
								{
									// ??? cože
									return null;
								}

							}
							else
							{
								var rows = invDS.Stavy.Where(x => x.TerminalID < 200);

								if (rows.Count() > 0)
								{
									return rows.First();
								}
								else
								{
									// ??? cože
									return invDS.Stavy.First();
								}
							}
						}

						//try
						//{
						//	if (o is byte)
						//		return (byte)o;
						//	else
						//		return null;
						//}
						//catch (Exception e)
						//{
						//	Logging.ExceptionHandler2.Handle(e);
						//	return null;
						//}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}


		public  Fask.DataSets.Inventury1 GetInventuryList_Ukoncit()
		{
			try
			{

				string select = "SELECT " +
					"I1H.CountEntries, " +
					" CAST(I1H.CountEntries as nvarchar(100)) + '  (' +  I1H.Description + ')' as [Desc]" +
					" FROM " + Constants.Common.TABLE_CZMST_I1H + " as I1H " +
					" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I1H.CountEntries " +
					" LEFT JOIN " + Constants.Common.TABLE_CZMST_I4 + " as I4 ON I4.CountEntries = I1.CountEntries " +
					" WHERE 1=1 " +
					" AND I1.TerminalID < 100 " +
					" AND I1.TerminalID != 0 " +
					" AND I1.TerminalID != 200 " +
					" GROUP BY I1H.CountEntries, I1H.Description" +
					" HAVING COUNT(I4.CountEntries) > 0";



				System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
				dataAdapter.Fill(inventury, inventury.Hlavicky_Seznam.TableName);
				inventury.AcceptChanges();

				return inventury;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Fask.DataSets.Inventury1 GetInventuryList_Prehled()
		{
			try
			{

				string select = "SELECT " +
					"CountEntries, " +
					" CAST(CountEntries as nvarchar(100)) + '  (' +  Description + ')' as [Desc]" +
					" FROM " + Constants.Common.TABLE_CZMST_I1H +
					" GROUP BY CountEntries, Description";



				System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
				dataAdapter.Fill(inventury, inventury.Hlavicky_Seznam.TableName);
				inventury.AcceptChanges();

				return inventury;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Fask.DataSets.Inventury1 GetInventuryList_ReOpen()
		{
			try
			{

				string select = "SELECT " +
					"I1H.CountEntries, " +
					" CAST(I1H.CountEntries as nvarchar(100)) + '  (' +  I1H.Description + ')' as [Desc]" +
					" FROM " + Constants.Common.TABLE_CZMST_I1H + " as I1H " +
					" LEFT JOIN " + Constants.Common.TABLE_CZMST_I1 + " as I1 ON I1.CountEntries = I1H.CountEntries " +
					" LEFT JOIN " + Constants.Common.TABLE_CZMST_I4 + " as I4 ON I4.CountEntries = I1.CountEntries " +
					" WHERE 1=1 " +
					" AND I1.TerminalID BETWEEN 101 AND 199 " +
					" GROUP BY I1H.CountEntries, I1H.Description" +
					" HAVING COUNT(I4.CountEntries) > 0";



				System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
				dataAdapter.Fill(inventury, inventury.Hlavicky_Seznam.TableName);
				inventury.AcceptChanges();

				return inventury;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#region Updats..

		#region Update I1 OK

		public int Update_I1(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I1(commandInsert);
					InitializeCommandSelect_I1(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I1(SqlCommand command)
		{
			command.CommandText = string.Empty;

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1 + " ON ";

			command.CommandText += @"INSERT INTO " + Constants.Common.TABLE_CZMST_I1 +
				" ([CountEntries], [CE_Orig], [ITEMNMBR], [CZ_CarKod], [ITEMDESC]," +
				" [LOCNCODE], [SKL_ID], [QUANTITY], [DMJ], [DATEDONE]," +
				" [IntegerValue], [TIMESPRT], [CZ_SerNum_Track], [CZ_SerNum_Find], [TerminalID]," +
				" [O_TID], [REZ_1], [REZ_2], [ITEMCODE], [CZ_REZ1_Track], [CZ_REZ2_Track], " +
				" [CZ_Expirace_Track])" +
				" VALUES " +
				" (@CountEntries, @CE_Orig, @ITEMNMBR, @CZ_CarKod, @ITEMDESC," +
				" @LOCNCODE, @SKL_ID, @QUANTITY, @DMJ, @DATEDONE," +
				" @IntegerValue, @TIMESPRT, @CZ_SerNum_Track, @CZ_SerNum_Find, @TerminalID," +
				" @O_TID, @REZ_1, @REZ_2, @ITEMCODE, @CZ_REZ1_Track, @CZ_REZ2_Track," +
				" @CZ_Expirace_Track)";


			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1 + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QUANTITY", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, SourceColumn = "DMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.DateTime, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@IntegerValue", DbType = System.Data.DbType.Int16, SourceColumn = "IntegerValue", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMESPRT", DbType = System.Data.DbType.Int16, SourceColumn = "TIMESPRT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Find", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Find", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TerminalID", DbType = System.Data.DbType.Byte, SourceColumn = "TerminalID", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@O_TID", DbType = System.Data.DbType.Byte, SourceColumn = "O_TID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I1(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1;
		}


		#endregion

		#endregion

		#region Update I1H OK

		public int Update_I1H(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I1H(commandInsert);
					InitializeCommandSelect_I1H(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I1H(SqlCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1H + " ON ";

			command.CommandText += 
				" INSERT INTO " + Constants.Common.TABLE_CZMST_I1H +
				" ( CountEntries, CE_Orig, Description, State ) " +
				" VALUES ( @CountEntries, @CE_Orig, @Description, @State ) ";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1H + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@State", DbType = System.Data.DbType.Byte, SourceColumn = "State", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I1H(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1H;
		}


		#endregion

		#endregion

		#region Update I2 OK

		public int Update_I2(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I2(commandInsert);
					InitializeCommandSelect_I2(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I2(SqlCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " ON ";

			command.CommandText += "INSERT INTO " + Constants.Common.TABLE_CZMST_I2 +
				"([CountEntries], [CE_Orig], [ITEMNMBR], [SERLNMBR], [QTY], [Expirace]) " +
				" VALUES " +
				" (@CountEntries, @CE_Orig, @ITEMNMBR, @SERLNMBR, @QTY, @Expirace)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I2(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I2;
		}

		#endregion

		#endregion


		#region Update I3 OK

		public int Update_I3(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I3(commandInsert);
					InitializeCommandSelect_I3(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I3(SqlCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " ON ";

			command.CommandText += "INSERT INTO " + Constants.Common.TABLE_CZMST_I3 +
				"([CountEntries], [CE_Orig], [ITEMNMBR], [CZ_CarKod], [QTYPACK], [MJ]," + 
				" [VENDORID], [VNDITNUM], [VENDNAME], [WEIGHT]) " + 
				" VALUES " + 
				" (@CountEntries, @CE_Orig, @ITEMNMBR, @CZ_CarKod, @QTYPACK, @MJ," + 
				" @VENDORID, @VNDITNUM, @VENDNAME, @WEIGHT)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@VENDORID", DbType = System.Data.DbType.String, SourceColumn = "VENDORID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VENDNAME", DbType = System.Data.DbType.String, SourceColumn = "VENDNAME", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I3(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I3;
		}

		#endregion

		#endregion

		#region Update I4 OK

		public int Update_I4(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I4(commandInsert);
					InitializeCommandSelect_I4(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I4(SqlCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I4 + " ON ";

			command.CommandText += @"INSERT INTO " + Constants.Common.TABLE_CZMST_I4 +
				"  ([CountEntries], [CE_Orig], [ITEMNMBR], [CZ_CarKod], [LOCNCODE]," + 
				" [SKL_ID], [VNDITNUM], [MJ], [QUANTITY], [QUANTITYMJ]," + 
				" [QTYPACK], [SERLNMBR], [DATEDONE], [TIMEDONE],[USERID]," +
				" [GUID], [O_Checked], [INPUT_MODE], [ID_TERMINAL], [ITEMCODE]," +
				" [REZ_1], [REZ_2], [WEIGHT], [Expirace]) " + 
				" VALUES " + 
				" (@CountEntries, @CE_Orig, @ITEMNMBR, @CZ_CarKod, @LOCNCODE," + 
				" @SKL_ID, @VNDITNUM, @MJ, @QUANTITY, @QUANTITYMJ," + 
				" @QTYPACK, @SERLNMBR, @DATEDONE, @TIMEDONE, @USERID," +
				" @GUID, @O_Checked, @INPUT_MODE, @ID_TERMINAL, @ITEMCODE," +
				" @REZ_1, @REZ_2, @WEIGHT, @Expirace)";


			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I4 + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QUANTITY", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QUANTITYMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITYMJ", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@O_Checked", DbType = System.Data.DbType.Boolean, SourceColumn = "O_Checked", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });
		}

		public void InitializeCommandSelect_I4(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I4;
		}

        #endregion

        #endregion

        #endregion

    }
}
