using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.SQL.Datasets;

namespace Fask.SQL.Database
{
    class Pohoda
	{

		#region Pro každou tabulku ručne definovane dotazy
		// Uprava z duvodu měneni struktur pod rukama ze strany Pohody se prešlo k tomuto spusobu komunikace s DB

		#region Fill UNIVERSAL

		private static void Fill_Universal(System.Data.DataTable dt, string SQL)
		{
			try
			{
				Globals_V1.LoadConfiguration();

				using (var con = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = SQL;
						com.CommandType = System.Data.CommandType.Text;

						using (var ada = new System.Data.OleDb.OleDbDataAdapter())
						{
							ada.SelectCommand = com;
							ada.Fill(dt);

						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		#endregion

		#region Template


		//public static DatabasePohoda.XXX_DataTable XXX_GetData(int? RefSKz, string Cislo)
		//{
		//    DatabasePohoda.XXX_DataTable dataTable = new DatabasePohoda.XXX_DataTable();
		//    XXX_Fill(dataTable, RefSKz, Cislo);
		//    return dataTable;
		//}

		//public static void XXX_Fill(DatabasePohoda.XXX_DataTable dataTable, string XXX)
		//{

		//    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
		//    try
		//    {
		//        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
		//        da.SelectCommand.CommandType = System.Data.CommandType.Text;

		//        da.SelectCommand.CommandText = @"";
		//        da.SelectCommand.Parameters.AddWithValue("?", XXX);

		//        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

		//        da.Fill(dataTable);
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.writeErrorLog("Pohoda Tabulky Definice SQL Dotazu", " XXX_Fill", ex.Message);
		//        Log.writeErrorData(dataTable);
		//    }

		//}



		#endregion

		#region OBJpol

		#region OBJPol Get/Fill  DataBy_RefAg_RefSKz

		/// <summary>
		/// Get Data z DB Pohoda z tabulky OBJPol where Cislo a RefSKz
		/// </summary>
		/// <param name="RefSKz">Reference na stav skladu</param>
		/// <param name="Cislo">SOPNUMBE číslo objednavky</param>
		/// <returns>Tabulka OBJPol</returns>
		public static DatabasePohoda.OBJpolDataTable OBJPol_GetDataBy_RefAg_RefSKz(int? RefSKz, string Cislo)
		{
			DatabasePohoda.OBJpolDataTable dataTable = new DatabasePohoda.OBJpolDataTable();
			OBJPol_FillBy_RefAg_RefSKz(dataTable, RefSKz, Cislo);
			return dataTable;
		}

		/// <summary>
		/// Fill Data z DB Pohoda z tabulky OBJPol where Cislo a RefSKz
		/// </summary>
		/// <param name="dataTable">Tabulka OBJPol</param>
		/// <param name="RefSKz">Reference na stav skladu</param>
		/// <param name="Cislo">SOPNUMBE číslo objednavky</param>
		public static void OBJPol_FillBy_RefAg_RefSKz(DatabasePohoda.OBJpolDataTable dataTable, int? RefSKz, string Cislo)
		{
			//this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT p.ID, p.Mnozstvi, p.Dodano, p.KcJedn, p.RelSzDPH, p.ProcentoDPH, p.CmJedn FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", RefSKz);
				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillBy_RefAg_RefSKz", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}
			
		}

		#endregion

		#region OBJPol Get/Fill  DataBy_ID

		/// <summary>
		/// Get Data z DB Pohoda z tabulky OBJPol where Cislo a RefSKz
		/// </summary>
		/// <param name="RefSKz">Reference na stav skladu</param>
		/// <param name="Cislo">SOPNUMBE číslo objednavky</param>
		/// <returns>Tabulka OBJPol</returns>
		public static DatabasePohoda.OBJpolDataTable OBJPol_GetDataBy_ID(int ID)
		{
			DatabasePohoda.OBJpolDataTable dataTable = new DatabasePohoda.OBJpolDataTable();
			OBJPol_FillBy_ID(dataTable, ID);
			return dataTable;
		}

		/// <summary>
		/// Fill Data z DB Pohoda z tabulky OBJPol where ID
		/// </summary>
		/// <param name="dataTable">Tabulka OBJPol</param>
		/// <param name="ID">ID řadku</param>
		public static void OBJPol_FillBy_ID(DatabasePohoda.OBJpolDataTable dataTable, int ID)
		{
			//this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT p.ID, p.Mnozstvi, p.Dodano, p.KcJedn, p.RelSzDPH, p.ProcentoDPH, p.CmJedn FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.ID = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillBy_ID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion


		#region OBJPol Get/Fill ByRefAg

		/// <summary>
		/// Get Data z DB Pohoda z tabulky OBJPol where RefAg
		/// </summary>
		/// <param name="RefAg">Reference na OBJ</param>
		/// <returns>Tabulka OBJPol</returns>
		public static DatabasePohoda.OBJpolDataTable OBJPol_GetDataByRefAg(int? RefAg)
		{
			DatabasePohoda.OBJpolDataTable dataTable = new DatabasePohoda.OBJpolDataTable();
			OBJPol_FillByRefAg(dataTable, RefAg);
			return dataTable;
		}

		/// <summary>
		/// Fill Data z DB Pohoda z tabulky OBJPol where RefAg 
		/// </summary>
		/// <param name="dataTable">Tabulka OBJPol</param>
		/// <param name="RefAg">Reference na OBJ</param>
		public static void OBJPol_FillByRefAg(DatabasePohoda.OBJpolDataTable dataTable, int? RefAg)
		{
			//this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				da.SelectCommand.CommandText = @"SELECT ID, RefSKz, Mnozstvi, Dodano, MJ, MJKoef, KcJedn, Sleva, RelSzDPH,  SDph, CmJedn, RefStr, RefCin, CisloZAK FROM dbo.OBJpol WHERE (RefAg = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", RefAg);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillByRefAg", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}
		
		}

		/// <summary>
		/// Fill Data z DB Pohoda z tabulky OBJPol where RefAg 
		/// </summary>
		/// <param name="dataTable">Tabulka OBJPol</param>
		/// <param name="RefAg">Reference na OBJ</param>
		public static void OBJPol_FillByID(DatabasePohoda.OBJpolDataTable dataTable, int? ID)
		{
			//this._commandCollection[1].CommandText = @"SELECT p.ID, p.RefAg, p.RefSKz, p.RefSKz0, p.RefPol, p.RelAgID, p.SText, p.Pozn, p.Kod, p.VCislo, p.SKzVC, p.Mnozstvi, p.Dodano, p.DodBefor, p.MJ, p.MJKoef, p.KcJedn, p.Sleva, p.RelSzDPH, p.ProcentoDPH, p.SDph, p.Kc, p.KcDPH, p.CmJedn, p.Cm, p.CmDPH, p.RelPk, p.PDP, p.MOSSDruh, p.RelTypPolEET, p.DICPover, p.RefStr, p.RefCin, p.CisloZAK, p.DatCreate, p.DatSave, p.OrderFld FROM dbo.OBJpol AS p LEFT OUTER JOIN dbo.OBJ AS o ON o.ID = p.RefAg WHERE (p.RefSKz = ?) AND (o.Cislo = ?)";
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				da.SelectCommand.CommandText = @"SELECT ID, RefSKz, Mnozstvi, Dodano, MJ, MJKoef, KcJedn, Sleva, RelSzDPH,  SDph, CmJedn, RefStr, RefCin, CisloZAK FROM dbo.OBJpol WHERE (ID = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_FillByRefAg", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion

		#region Update

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pohodaDS"></param>
		public static int OBJPol_Update(DatabasePohoda pohodaDS,System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{

			Fask.Logging.ExceptionHandler2.Handle(
											Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
											"OBJPol_Update",
											 $"zacatek update zaznamu");


			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			
			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			

			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			//da.UpdateCommand.CommandTimeout
			da.UpdateCommand.CommandText = @"UPDATE OBJpol" +
				" SET "+
				//" RefSKz = ? ," +
				//" Mnozstvi = ? ," +
				//" Dodano = ? ," +
				//" MJ = ? ," +
				//" MJKoef = ? ," +
				//" KcJedn = ? ," +
				//" Sleva = ? ," +
				//" RelSzDPH = ? ," +
				//" SDph = ? ," +
				//" CmJedn = ? ," +
				//" RefStr = ? ," +
				//" RefCin = ? ," +
				//" CisloZAK = ? ," +
				//" ProcentoDPH = ? " +
				//" WHERE (ID = ?)";
				" Dodano = ? " +
				" WHERE (ID = ?)";

			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefSKz", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Mnozstvi", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Dodano", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("MJ", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("MJKoef", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("KcJedn", System.Data.OleDb.OleDbType.Currency));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Sleva", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelSzDPH", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("SDph", System.Data.OleDb.OleDbType.Binary));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CmJedn", System.Data.OleDb.OleDbType.Currency));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefStr", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCin", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CisloZAK", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ProcentoDPH", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));			

			//da.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Dodano", global::System.Data.OleDb.OleDbType.Double, 8, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Dodano", global::System.Data.DataRowVersion.Current, false, null));
			//da.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ID", global::System.Data.OleDb.OleDbType.Integer, 4, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ID", global::System.Data.DataRowVersion.Original, false, null));

			var pDodano = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Dodano", System.Data.OleDb.OleDbType.Double));
			pDodano.SourceColumn = "Dodano";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			// Loguj změněné hodnoty před aktualizací
			foreach (System.Data.DataRow row in pohodaDS.OBJpol.Rows)
			{
				if (row.RowState == System.Data.DataRowState.Modified)
				{
					double dodano = row["Dodano"] != DBNull.Value ? Convert.ToDouble(row["Dodano"]) : 0;
					int id = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : -1;

					// Použij Fask.Logging.ExceptionHandler2 pro logování hodnot
				
					Fask.Logging.ExceptionHandler2.Handle(
					Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
					"OBJPol_Update",
					$"Update řádku s ID={id}, Dodano={dodano}");
				}
			}


			int result = 0;
			try
			{
				result = da.Update(pohodaDS.OBJpol);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJPol_Update", ex);
				//Log.writeErrorData(dataTable);
			}
			return result;
		}

        #endregion

        #endregion

        #region OBJ

        /// <summary>
        /// Metoda sloužici pro Update uživatele ktery provedl zaznam
        /// </summary>
        /// <param name="Creator">Tvořitel</param>
        /// <param name="Original_Cislo">SOPNUMBE číslo objednavky</param>
        /// <returns></returns>
        public static int OBJ_UpdateCreatorByCislo(string Creator, string Original_Cislo)
		{
			//this._commandCollection[3].CommandText = "UPDATE       OBJ\r\nSET                Creator = ?\r\nWHERE        (Cislo = ?)";

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
				da.UpdateCommand.CommandType = System.Data.CommandType.Text;
				//da.UpdateCommand.CommandTimeout
				da.UpdateCommand.CommandText = @"UPDATE OBJ SET Creator = ? WHERE (Cislo = ?)";

				da.UpdateCommand.Parameters.AddWithValue("?", Creator);
				da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


				da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.UpdateCommand.Connection.Open();
				result = da.UpdateCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_UpdateCreatorByCislo", ex);
				//Log.writeErrorData(dataTable);
			}
			finally 
			{
				da.UpdateCommand.Connection.Close();
			}
			return result;

		}

		/// <summary>
		/// Metoda pro vraceni dat z OBJ
		/// </summary>
		/// <param name="Cislo">SOPNUMBE číslo objednavky</param>
		/// <returns></returns>
		public static DatabasePohoda.OBJDataTable OBJ_GetDataByCislo(string Cislo)
		{
			DatabasePohoda.OBJDataTable dataTable = new DatabasePohoda.OBJDataTable();
			OBJ_FillByCislo(dataTable, Cislo);
			return dataTable;
		}

		/// <summary>
		/// Metoda pro vraceni dat z OBJ 
		/// </summary>
		/// <param name="dataTable">Tabulka OBJ pro naplneni</param>
		/// <param name="Cislo">SOPNUMBE číslo objednavky</param>
		/// <returns>počet nalezenych radku</returns>
		public static int OBJ_FillByCislo(DatabasePohoda.OBJDataTable dataTable, string Cislo)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int response = 0;
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT ID, RefCin, RefStr, CisloZAK, SText, HistSzDPH,RefCM, CmKurs, Vyrizeno, BDodano, TrvalyDok, RefAD, Firma, Utvar, Jmeno, Ulice, PSC, Obec, ICO, DIC, ICDPH, Email, Firma2, Utvar2, Jmeno2, Ulice2, PSC2, Obec2, Email2, Fax, Pozn, Pozn2, DICRegDPHEU ";

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					da.SelectCommand.CommandText += ", RefVPrSkladCil, RefVPrVysDoklad ";
				}
				
				da.SelectCommand.CommandText += " FROM dbo.OBJ WHERE (Cislo = ?)";

				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				 response = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_FillByCislo", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}
			return response;
		}

		/// <summary>
		/// Update Tabulky OBJ
		/// </summary>
		/// <param name="pohodaDS"></param>
		public static int OBJ_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{
			Fask.Logging.ExceptionHandler2.Handle(
											Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
											"OBJ_Update",
											 $"zacatek update zaznamu");

			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			
			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			//da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			//da.UpdateCommand.CommandTimeout
			da.UpdateCommand.CommandText = @"UPDATE OBJ" +
				" SET " + 
				//" RefCin = ? ," +
				//" RefStr = ? ," +
				//" CisloZAK = ? ," +
				//" SText = ? ," +
				//" HistSzDPH = ? ," +
				//" RefCM = ? ," +
				//" CmKurs = ? ," +
				//" Vyrizeno = ? ," +
				//" BDodano = ? ," +
				//" TrvalyDok = ? ," +
				//" RefAD = ? ," +
				//" Firma = ? ," +
				//" Utvar = ? ," +
				//" Jmeno = ? ," +
				//" Ulice = ? ," +
				//" PSC = ? ," +
				//" Obec = ? ," +
				//" ICO = ? ," +
				//" DIC = ? ," +
				//" ICDPH = ? ," +
				//" Email = ? ," +
				//" Firma2 = ? ," +
				//" Utvar2 = ? ," +
				//" Jmeno2 = ? ," +
				//" PSC2 = ? ," +
				//" Obec2 = ? ," +
				//" Email2 = ? ," +
				//" Fax = ? ," +
				//" Pozn = ? ," +
				//" Pozn2 = ? ," +
				//" DICRegDPHEU = ? " +
				//" WHERE (ID = ?)";

				" Vyrizeno = ? ," +
				" BDodano = ? " +
				" WHERE (ID = ?)";

			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCin", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefStr", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CisloZAK", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("SText", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("HistSzDPH", System.Data.OleDb.OleDbType.Binary));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefCM", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("CmKurs", System.Data.OleDb.OleDbType.Double));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Vyrizeno", System.Data.OleDb.OleDbType.Binary));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("BDodano", System.Data.OleDb.OleDbType.Binary));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("TrvalyDok", System.Data.OleDb.OleDbType.Binary));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefAD", System.Data.OleDb.OleDbType.Integer));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Firma", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Jmeno", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Ulice", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PSC", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Obec", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ICO", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIC", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ICDPH", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Email", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Firma2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Utvar2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Jmeno2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PSC2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Obec2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Email2", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Fax", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Pozn", System.Data.OleDb.OleDbType.LongVarWChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Pozn2", System.Data.OleDb.OleDbType.LongVarWChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("DICRegDPHEU", System.Data.OleDb.OleDbType.VarChar));
			//da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));


			var pVyrizeno = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("Vyrizeno", System.Data.OleDb.OleDbType.Boolean));
			pVyrizeno.SourceColumn = "Vyrizeno";

			var pBDodano = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("BDodano", System.Data.OleDb.OleDbType.Boolean));
			pBDodano.SourceColumn = "BDodano";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				result = da.Update(pohodaDS.OBJ);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJ_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}

		#endregion

		#region sCRady

		//public virtual DatabasePohoda.sCRadyDataTable sCRady_GetDataBy_RokDokladObsahtextu(global::System.Nullable<int> Rok, global::System.Nullable<int> RelCrAg, string SText) {
		//public virtual int sCRady_FillBy_RokDokladObsahtextu(DatabasePohoda.sCRadyDataTable dataTable, global::System.Nullable<int> Rok, global::System.Nullable<int> RelCrAg, string SText) {


		public static DatabasePohoda.sCRadyDataTable sCRady_GetDataBy_RokDokladObsahtextu(int? Rok, int? RelCrAg, string SText)
		{
			DatabasePohoda.sCRadyDataTable dataTable = new DatabasePohoda.sCRadyDataTable();
			sCRady_FillBy_RokDokladObsahtextu(dataTable, Rok, RelCrAg, SText);
			return dataTable;
		}


		public static int sCRady_FillBy_RokDokladObsahtextu(DatabasePohoda.sCRadyDataTable dataTable, int? Rok, int? RelCrAg, string SText)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT ID, IDS FROM sCRady" +
					" WHERE (Rok = ?) AND (RelCrAg = ?) AND (SText LIKE ?)";

				da.SelectCommand.Parameters.AddWithValue("?", Rok);
				da.SelectCommand.Parameters.AddWithValue("?", RelCrAg);
				da.SelectCommand.Parameters.AddWithValue("?", SText);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCRady_FillBy_RokDokladObsahtextu", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}
			return result;
		}

		#endregion

		#region sPrelom

		public static DatabasePohoda.sPrelomDataTable sPrelom_GetData(string UsIDS)
		{
			DatabasePohoda.sPrelomDataTable dataTable = new DatabasePohoda.sPrelomDataTable();
			sPrelom_Fill(dataTable, UsIDS);
			return dataTable;
		}

		public static int sPrelom_Fill(DatabasePohoda.sPrelomDataTable dataTable, string UsIDS)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;
				//da.SelectCommand.CommandTimeout
				da.SelectCommand.CommandText = @"SELECT ID, UsIDS, IsPrelom, UsRok FROM sPrelom" +
					" WHERE (UsIDS = ?)";

				da.SelectCommand.Parameters.AddWithValue("?", UsIDS);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sPrelom_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}
			return result;
		}

		#endregion

		#region OBJCislo

		//public virtual int Fill(DatabasePohoda.OBJCisloDataTable dataTable, string Cislo)

		public static int OBJCislo_Fill(DatabasePohoda.OBJCisloDataTable dataTable, string Cislo)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				string Command = string.Empty;

				Command += @"SELECT OBJ.Cislo AS OBJ_Cislo, SKz.ID AS SKz_ID, SKz.Nazev AS SKz_Nazev, SKz.EAN AS SKz_EAN, SKz.IDS AS SKz_IDS, SKz.RefSklad AS SKz_RefSklad, OBJpol.Mnozstvi AS OBJ_Mnozstvi, OBJpol.MJ AS OBJPol_MJ,OBJpol.MJKoef AS OBJpol_MJKoef, SKz.RelSKzVC AS SKz_RelSKzVC, OBJpol.ID AS OBJpol_ID, SKz.SText AS SKz_SText, SKz.RelSkTyp as SKz_RelSkTyp ";

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					Command += ", SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS ";
				}

				Command += " FROM dbo.OBJ LEFT OUTER JOIN";
				Command += " dbo.OBJpol ON OBJpol.RefAg = OBJ.ID LEFT OUTER JOIN";
				Command += " dbo.SKz ON SKz.ID = OBJpol.RefSKz";
				Command += " WHERE (OBJ.Cislo = ?)";

				da.SelectCommand.CommandText = Command;

				#region Original command
				//            SELECT        OBJ.Cislo AS OBJ_Cislo, SKz.ID AS SKz_ID, SKz.Nazev AS SKz_Nazev, SKz.EAN AS SKz_EAN, SKz.IDS AS SKz_IDS, SKz.RefSklad AS SKz_RefSklad, OBJpol.Mnozstvi AS OBJ_Mnozstvi, OBJpol.MJ AS OBJPol_MJ, 
				//                         OBJpol.MJKoef AS OBJpol_MJKoef, SKz.RelSKzVC AS SKz_RelSKzVC, OBJpol.ID AS OBJpol_ID, SKz.SText AS SKz_SText, SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, 
				//                         SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, 
				//                         SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS
				//FROM            dbo.OBJ LEFT OUTER JOIN
				//                         dbo.OBJpol ON OBJpol.RefAg = OBJ.ID LEFT OUTER JOIN
				//                         dbo.SKz ON SKz.ID = OBJpol.RefSKz
				//WHERE        (OBJ.Cislo = ?) 
				#endregion

				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " OBJCislo_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}


		#endregion

		#region SKzParametry


		public static DatabasePohoda.SKzParametryDataTable SKzParametry_GetDataByParamNameSKzID(string IDS, int? ID)
		{
			DatabasePohoda.SKzParametryDataTable dataTable = new DatabasePohoda.SKzParametryDataTable();
			SKzParametry_FillByParamNameSKzID(dataTable, IDS, ID);
			return dataTable;
		}

		public static int SKzParametry_FillByParamNameSKzID(DatabasePohoda.SKzParametryDataTable dataTable, string IDS, int? ID)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				Globals_V1.LoadConfiguration();

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT skz.ID AS zID, skz.IDS AS zIDS, srp.ValText AS pValText, sp.ID AS pID, " + 
					"sp.IDS AS pIDS, sp.SText AS pSText, sp.Delka AS pDelka, sp.RelTyp AS pRelTyp " +
					"FROM ((SKz skz LEFT OUTER JOIN SkRefParam srp ON srp.RefAg = skz.ID) LEFT OUTER JOIN SkParam sp ON sp.ID = srp.RefParam) " + 
					"WHERE (sp.IDS = ?) AND (skz.ID = ?)";


				da.SelectCommand.Parameters.AddWithValue("?", IDS);
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzParametry_FillByParamNameSKzID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		#endregion

		#region SKPPpol

		//public virtual int FillByRefAg(DatabasePohoda.SKPPpolDataTable dataTable, global::System.Nullable<int> RefAg) {

		public static int SKPPpol_FillByRefAg(DatabasePohoda.SKPPpolDataTable dataTable, int? RefAg)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT ID, RefPol, RelAgID, Mnozstvi, RefSKz, VCislo FROM SKPPpol where RefAg=?";


				da.SelectCommand.Parameters.AddWithValue("?", RefAg);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPPpol_FillByRefAg", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		public static int SKPPpol_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{
			Fask.Logging.ExceptionHandler2.Handle(
											Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
											"SKPPpol_Update",
											 $"zacatek update zaznamu");


			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandText = @"UPDATE SKPPpol" +
				" SET " +
				" RefPol = ? ," +
				" RelAgID = ? " +
				" WHERE (ID = ?)";


			var pRefPol = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefPol", System.Data.OleDb.OleDbType.Integer));
			pRefPol.SourceColumn = "RefPol";

			var pRelAgID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelAgID", System.Data.OleDb.OleDbType.Integer));
			pRelAgID.SourceColumn = "RelAgID";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				result = da.Update(pohodaDS.SKPPpol);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPPpol_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}

		#endregion

		#region SKzBuf

		public static int SKzBuf_FillByRefSkz(DatabasePohoda.SKzBufDataTable dataTable, int? RefSKz)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT ID, RefSKz, ObjedP, ObjedV FROM SKzBuf WHERE (RefSKz = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", RefSKz);
				
				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
	
				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzBuf_FillByRefSkz", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		public static int SKzBuf_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{

			Fask.Logging.ExceptionHandler2.Handle(
											Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
											"SKzBuf_Update",
											 $"zacatek update zaznamu");


			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			//da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandText = @"UPDATE SKzBuf" +
				" SET " +
				" RefSKz = ? ," +
				" ObjedP = ? ," +
				" ObjedV = ? " +
				" WHERE (ID = ?)";


			var pRefSKz = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefSKz", System.Data.OleDb.OleDbType.Integer));
			pRefSKz.SourceColumn = "RefSKz";

			var pObjedP = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedP", System.Data.OleDb.OleDbType.Double));
			pObjedP.SourceColumn = "ObjedP";

			var pObjedV = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedV", System.Data.OleDb.OleDbType.Double));
			pObjedV.SourceColumn = "ObjedV";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				result = da.Update(pohodaDS.SKzBuf);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzBuf_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}

		#endregion

		#region SKzObjedP
		

		public static int SKzObjedP_FillByID(DatabasePohoda.SKzObjedPDataTable dataTable, int ID)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT ID, ObjedP FROM SKz WHERE (ID = ?)";
				
				
				da.SelectCommand.Parameters.AddWithValue("?", ID);
				
				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
	
				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedP_FillByID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		public static int SKzObjedP_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{
			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandText = @"UPDATE SKz" +
				" SET " +
				" ObjedP = ? " +
				" WHERE (ID = ?)";


			var pObjedP = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedP", System.Data.OleDb.OleDbType.Double));
			pObjedP.SourceColumn = "ObjedP";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				result = da.Update(pohodaDS.SKz);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedP_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}



		#endregion

		#region SKzObjedV


		public static int SKzObjedV_FillByID(DatabasePohoda.SKzObjedVDataTable dataTable, int ID)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT ID, ObjedV FROM SKz WHERE (ID = ?)";

				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedV_FillByID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		public static int SKzObjedV_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{
			Fask.Logging.ExceptionHandler2.Handle(
											Logging.LogLevel.Debug, "Pohoda Tabulky Definice SQL Dotazu",
											"SKzObjedV_Update",
											 $"zacatek update zaznamu");


			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			//da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandText = @"UPDATE SKz" +
				" SET " +
				" ObjedV = ? " +
				" WHERE (ID = ?)";


			var pObjedV = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ObjedV", System.Data.OleDb.OleDbType.Double));
			pObjedV.SourceColumn = "ObjedV";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				//21.10.2019 TaD Chyba pri update tabulky SKz , špatny zdroj DataTable
				//result = da.Update(pohodaDS.SKz);
				result = da.Update(pohodaDS.SKzObjedV);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzObjedV_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}



		#endregion

		#region SKPVpol

		public static int SKPVpol_FillByRefAg(DatabasePohoda.SKPVpolDataTable dataTable, int? RefAg)
		{
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{

				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT ID, RefPol, RelAgID, Mnozstvi, RefSKz FROM SKPVpol where RefAg=?";


				da.SelectCommand.Parameters.AddWithValue("?", RefAg);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				result = da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPVpol_FillByRefAg", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

			return result;
		}

		public static int SKPVpol_Update(DatabasePohoda pohodaDS, System.Data.OleDb.OleDbConnection connection, System.Data.OleDb.OleDbTransaction Trans)
		{
			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();

			da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
			//da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandType = System.Data.CommandType.Text;
			da.UpdateCommand.Connection = connection;
			da.UpdateCommand.Transaction = Trans;
			da.UpdateCommand.CommandText = @"UPDATE SKPVpol" +
				" SET " +
				" RefPol = ? ," +
				" RelAgID = ? " +
				" WHERE (ID = ?)";


			var pRefPol = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RefPol", System.Data.OleDb.OleDbType.Integer));
			pRefPol.SourceColumn = "RefPol";

			var pRelAgID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("RelAgID", System.Data.OleDb.OleDbType.Integer));
			pRelAgID.SourceColumn = "RelAgID";

			var pID = da.UpdateCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer));
			pID.SourceColumn = "ID";
			pID.SourceVersion = System.Data.DataRowVersion.Original;

			int result = 0;
			try
			{
				result = da.Update(pohodaDS.SKPPpol);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPVpol_Update", ex);
				Fask.Logging.ExceptionHandler2.Handle(pohodaDS);
			}
			return result;
		}

		#endregion

		#region AD

		public static DatabasePohoda.ADDataTable AD_GetData()
		{
			DatabasePohoda.ADDataTable dataTable = new DatabasePohoda.ADDataTable();
			AD_Fill(dataTable);
			return dataTable;
		}

		public static void AD_Fill(DatabasePohoda.ADDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, Cislo, Firma, ICO FROM AD";

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " AD_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		public static DatabasePohoda.ADDataTable AD_GetDataByID(int ID)
		{
			DatabasePohoda.ADDataTable dataTable = new DatabasePohoda.ADDataTable();
			AD_FillByID(dataTable, ID);
			return dataTable;
		}

		public static void AD_FillByID(DatabasePohoda.ADDataTable dataTable, int ID)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, Cislo, Firma, ICO, RefCM FROM AD where ID=?";
				da.SelectCommand.Parameters.AddWithValue("?", ID);
				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " AD_FillByID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		public static void AD_FillByICO(DatabasePohoda.ADDataTable dataTable, string ICO)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, Cislo, Firma, ICO FROM AD where ICO=?";
				da.SelectCommand.Parameters.AddWithValue("?", ICO);
				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " AD_FillByICO", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion

		#region pPK_Predkontace

		public static DatabasePohoda.pPK_PredkontaceDataTable pPK_Predkontace_GetDataByID(int ID)
		{
			DatabasePohoda.pPK_PredkontaceDataTable dataTable = new DatabasePohoda.pPK_PredkontaceDataTable();
			pPK_Predkontace_FillByID(dataTable, ID);
			return dataTable;
		}

		public static void pPK_Predkontace_FillByID(DatabasePohoda.pPK_PredkontaceDataTable dataTable, int ID)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT p.IDS FROM dbo.pPK AS p LEFT OUTER JOIN dbo.AD AS a ON a.RelPkFV = p.ID WHERE (a.ID = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " pPK_Predkontace_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion

		#region Kontrola

		public static DatabasePohoda.KontrolaDataTable Kontrola_GetData_ITEMNMBR_SOPNUMBE(string Cislo, int ID)
		{
			DatabasePohoda.KontrolaDataTable dataTable = new DatabasePohoda.KontrolaDataTable();
			Kontrola_Fill_ITEMNMBR_SOPNUMBE(dataTable, Cislo, ID);
			return dataTable;
		}

		public static void Kontrola_Fill_ITEMNMBR_SOPNUMBE(DatabasePohoda.KontrolaDataTable dataTable, string Cislo, int ID)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT o.Rezer AS OBJ_Rezer, s.Rezer AS SKz_Rezer, s.StavZ AS SKz_StavZ, s.ObjedP AS SKz_ObjedP " + 
												"FROM dbo.OBJ AS o LEFT OUTER JOIN dbo.OBJpol AS p ON p.RefAg = o.ID " + 
												"LEFT OUTER JOIN dbo.SKz AS s ON s.ID = p.RefSKz " + 
												"WHERE (o.Cislo = ?) AND (s.ID = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", Cislo);
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Kontrola_Fill_ITEMNMBR_SOPNUMBE", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion

		#region SKMPPolVazba


		public static DatabasePohoda.SKMPPolVazbaDataTable SKMPPolVazba_GetData_RefAg_RefSKz(int? RefAg, int? RefSKz)
		{
			DatabasePohoda.SKMPPolVazbaDataTable dataTable = new DatabasePohoda.SKMPPolVazbaDataTable();
			SKMPPolVazba_FillBy_RefAg_RefSKz(dataTable, RefAg, RefSKz);
			return dataTable;
		}

		public static void SKMPPolVazba_FillBy_RefAg_RefSKz(DatabasePohoda.SKMPPolVazbaDataTable dataTable, int? RefAg, int? RefSKz)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT RefSKz, RefSKz1 from SKMPpol "  + 
												"WHERE RefAg=? and RefSKz=? " + 
												"GROUP BY RefSKz, RefSKz1";

				da.SelectCommand.Parameters.AddWithValue("?", RefAg);
				da.SelectCommand.Parameters.AddWithValue("?", RefSKz);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKMPPolVazba_FillBy_RefAg_RefSKz", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		#endregion

		#region SKzNC


		public static DatabasePohoda.SKzNCDataTable SKzNC_GetDataSKzID(int? RefAg)
		{
			DatabasePohoda.SKzNCDataTable dataTable = new DatabasePohoda.SKzNCDataTable();
			SKzNC_FillBySKzID(dataTable, RefAg);
			return dataTable;
		}

		public static void SKzNC_FillBySKzID(DatabasePohoda.SKzNCDataTable dataTable, int? RefAg)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT RefAg, Firma, EAN, MJEAN FROM SKzNC WHERE (RefAg = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", RefAg);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzNC_FillBySKzID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#region sSklad

		public static DatabasePohoda.sSkladDataTable sSklad_GetData()
		{
			DatabasePohoda.sSkladDataTable dataTable = new DatabasePohoda.sSkladDataTable();
			sSklad_Fill(dataTable);
			return dataTable;
		}

		public static void sSklad_Fill(DatabasePohoda.sSkladDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, IDS, SText, Reklam, Pozn FROM sSklad";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sSklad_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}



		#endregion

		#region SKzAlternatives

		#region Fill a GetData
		public static DatabasePohoda.SKzAlternativesDataTable SKzAlternatives_GetData()
		{
			DatabasePohoda.SKzAlternativesDataTable dataTable = new DatabasePohoda.SKzAlternativesDataTable();
			SKzAlternatives_Fill(dataTable);
			return dataTable;
		}

		public static void SKzAlternatives_Fill(DatabasePohoda.SKzAlternativesDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKz.RefSklad, SKzNC.RefAD AS NCRefAD FROM (SKz INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg)";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_Fill", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

        #endregion

        #region Fill a GetData by Aktivni

        public static DatabasePohoda.SKzAlternativesDataTable SKzAlternatives_GetDataByAktivni()
		{
			DatabasePohoda.SKzAlternativesDataTable dataTable = new DatabasePohoda.SKzAlternativesDataTable();
			SKzAlternatives_FillByAktivni(dataTable);
			return dataTable;
		}

		public static void SKzAlternatives_FillByAktivni(DatabasePohoda.SKzAlternativesDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
					"FROM (SKz INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
					"WHERE (SKz.Odbyt <> 0)";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillByAktivni", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}
		
		#endregion

		#region ExportovatPouzeAktivniPolozky a Zbozi_DotahovatAlternativniDodavatele


		/// <summary>
		/// SKzAlternatives_FillBy_ExportovatPouzeAktivniPolozky_DotahovatAlternativniDodavatele
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="ExportSkladFilter"></param>
		/// <param name="ExportTypFilter"></param>
		public static void SKzAlternatives_FillBy_EPAP_DAD(DatabasePohoda.SKzAlternativesDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
								"SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
								"FROM SKz ((" +
								"INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg ) " +
								"INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
								"WHERE (SKz.Odbyt <> 0)  AND (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillBy_EPAP_DAD", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}
		

		#endregion

		#region Zbozi_DotahovatAlternativniDodavatele


		/// <summary>
		/// SKzAlternatives_FillBy_DotahovatAlternativniDodavatele
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="ExportSkladFilter"></param>
		/// <param name="ExportTypFilter"></param>
		public static void SKzAlternatives_FillBy_DAD(DatabasePohoda.SKzAlternativesDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                                "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                                "FROM SKz ((" +
                                "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
                                "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                                "WHERE (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzAlternatives_FillBy_DAD", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#endregion

		#region sCMeny

		#region GetData a Fill  Aktivni
		public static DatabasePohoda.sCMenyDataTable sCMeny_GetDataByAktivni()
		{
			DatabasePohoda.sCMenyDataTable dataTable = new DatabasePohoda.sCMenyDataTable();
			sCMeny_FillByAktivni(dataTable);
			return dataTable;
		}

		public static void sCMeny_FillByAktivni(DatabasePohoda.sCMenyDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, Sel, Pouzit, Kod, IDS, Zeme, Mnozstvi, DenEUR, DatDen, KoefEUR, Oznacil, Ucetni, Creator, Pozn, NullCheck_Kod FROM sCMeny WHERE (Pouzit = 1)";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCMeny_FillByAktivni", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}
		
		#endregion

		public static DatabasePohoda.sCMenyDataTable sCMeny_GetDataByKod(string Kod)
		{
			DatabasePohoda.sCMenyDataTable dataTable = new DatabasePohoda.sCMenyDataTable();
			sCMeny_FillByKod(dataTable, Kod);
			return dataTable;
		}

		public static void sCMeny_FillByKod(DatabasePohoda.sCMenyDataTable dataTable, string Kod)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, Sel, Pouzit, Kod, IDS, Zeme, Mnozstvi, DenEUR, DatDen, KoefEUR, Oznacil, Ucetni, Creator, Pozn, NullCheck_Kod FROM sCMeny WHERE (Kod = ?)";
				da.SelectCommand.Parameters.AddWithValue("?", Kod);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCMeny_FillByKod", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}
		
		
		#endregion

		#region FakturaCislo


		public static DatabasePohoda.FakturaCisloDataTable FakturaCislo_GetDataByNoMSTParams(string Cislo)
		{
			DatabasePohoda.FakturaCisloDataTable dataTable = new DatabasePohoda.FakturaCisloDataTable();
			FakturaCislo_FillByNoMSTParams(dataTable, Cislo);
			return dataTable;
		}

		public static void FakturaCislo_FillByNoMSTParams(DatabasePohoda.FakturaCisloDataTable dataTable, string Cislo)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				string command = string.Empty;

				command += @"SELECT FA.ID AS FA_ID, FA.RelCR AS FA_RelCR, FA.Cislo AS FA_Cislo, FA.VarSym AS FA_VarSym, FA.SText AS FA_SText, FApol.ID AS FApol_ID, FApol.SText AS FApol_SText, FApol.Mnozstvi AS FApol_Mnozstvi, ";
				command += @"FApol.Prenes AS FApol_Prenes, FApol.MJ AS FApol_MJ, FApol.MJKoef AS FApol_MJKoef, FApol.Kod AS FApol_Kod, SKz.ID AS SKz_ID, SKz.IDS AS SKz_IDS, SKz.EAN AS SKz_EAN, SKz.RelSKzVC AS SKz_RelSKzVC, ";
				command += @"SKz.MJ2 AS Skz_MJ2, SKz.MJ3 AS Skz_MJ3, SKz.MJ2Koef AS Skz_MJ2Koef, SKz.MJ3Koef AS Skz_MJ3Koef, SKz.RefSklad AS Skz_RefSklad, SKz.SText AS SKz_SText ";

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					command += " ,SKz.RefVPrFXTS AS SKz_RefVPrFXTS, SKz.RefVPrFDTS AS SKz_RefVPrFDTS, SKz.RefVPrFITS AS SKz_RefVPrFITS, SKz.RefVPrFPTS AS SKz_RefVPrFPTS, SKz.RefVPrFVTS AS SKz_RefVPrFVTS, SKz.VPrFXTS AS SKz_VPrFXTS, SKz.VPrFDTS AS SKz_VPrFDTS, SKz.VPrFITS AS SKz_VPrFITS, SKz.VPrFPTS AS SKz_VPrFPTS, SKz.VPrFVTS AS SKz_VPrFVTS ";
				}

				command += "FROM ((FA LEFT OUTER JOIN FApol ON FApol.RefAg = FA.ID) LEFT OUTER JOIN SKz ON SKz.ID = FApol.RefSKz) WHERE (FA.Cislo = ?)";

				da.SelectCommand.CommandText = command;
				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " FakturaCislo_FillByNoMSTParams", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#region SKPP


		public static DatabasePohoda.SKPPDataTable SKPP_GetDataByCislo(string Cislo)
		{
			DatabasePohoda.SKPPDataTable dataTable = new DatabasePohoda.SKPPDataTable();
			SKPP_FillByCislo(dataTable, Cislo);
			return dataTable;
		}

		public static void SKPP_FillByCislo(DatabasePohoda.SKPPDataTable dataTable, string Cislo)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID FROM SKPP where cislo=?";

				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPP_FillByCislo", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		public static int SKPP_UpdateCreatorByCislo(string Creator, string Original_Cislo)
		{
		
			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
				da.UpdateCommand.CommandType = System.Data.CommandType.Text;
				//da.UpdateCommand.CommandTimeout
				da.UpdateCommand.CommandText = @"UPDATE SKPP SET Creator = ? WHERE (Cislo = ?)";

				da.UpdateCommand.Parameters.AddWithValue("?", Creator);
				da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


				da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.UpdateCommand.Connection.Open();
				result = da.UpdateCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPP_UpdateCreatorByCislo", ex);
				//Log.writeErrorData(dataTable);
			}
			finally
			{
				da.UpdateCommand.Connection.Close();
			}
			return result;

		}


		#endregion

		#region SKPV


		public static DatabasePohoda.SKPVDataTable SKPV_GetDataByCislo(string Cislo)
		{
			DatabasePohoda.SKPVDataTable dataTable = new DatabasePohoda.SKPVDataTable();
			SKPV_FillByCislo(dataTable, Cislo);
			return dataTable;
		}

		public static void SKPV_FillByCislo(DatabasePohoda.SKPVDataTable dataTable, string Cislo)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID FROM SKPV where cislo=?";
				da.SelectCommand.Parameters.AddWithValue("?", Cislo);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPV_FillByCislo", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		public static int SKPV_UpdateCreatorByCislo(string Creator, string Original_Cislo)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			int result = 0;
			try
			{
				da.UpdateCommand = new System.Data.OleDb.OleDbCommand();
				da.UpdateCommand.CommandType = System.Data.CommandType.Text;
				//da.UpdateCommand.CommandTimeout
				da.UpdateCommand.CommandText = @"UPDATE SKPV SET Creator = ? WHERE (Cislo = ?)";

				da.UpdateCommand.Parameters.AddWithValue("?", Creator);
				da.UpdateCommand.Parameters.AddWithValue("?", Original_Cislo);


				da.UpdateCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				da.UpdateCommand.Connection.Open();
				result = da.UpdateCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKPV_UpdateCreatorByCislo", ex);
				//Log.writeErrorData(dataTable);
			}
			finally
			{
				da.UpdateCommand.Connection.Close();
			}
			return result;

		}


		#endregion

		#region SKz


		#region Aktivni

		public static DatabasePohoda.SKzDataTable SKz_GetDataByAktivniPolozky()
		{
			DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
			SKz_FillByAktivniPolozky(dataTable);
			return dataTable;
		}

		public static void SKz_FillByAktivniPolozky(DatabasePohoda.SKzDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, StavZ, RefSklad, RefAD, MJ2, MJ3, MJ2Koef, MJ3Koef " +
				"FROM SKz WHERE (Odbyt <> 0)";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByAktivniPolozky", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}
		
		#endregion

		#region by ID

		public static DatabasePohoda.SKzDataTable SKz_GetDataByID(int? ID)
		{
			DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
			SKz_FillByID(dataTable, ID);
			return dataTable;
		}

		public static void SKz_FillByID(DatabasePohoda.SKzDataTable dataTable, int? ID)
		{

			Globals_V1.LoadConfiguration();

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				string comand = string.Empty;

				comand += "SELECT ID, RefSklad, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, MJ2, MJ3, MJ2Koef, MJ3Koef, StavZ, RefAD ";

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					comand += ", VPrFVTS, VPrFPTS, VPrFITS, VPrFDTS, VPrFXTS, RefVPrFVTS, RefVPrFPTS, RefVPrFITS, RefVPrFDTS, RefVPrFXTS, VPrCZExpTrackIS, VPrCZSerNumTrIS, VPrCZSNumTrIGN ";
				}

				comand += "FROM SKz WHERE (ID = ?)";

				da.SelectCommand.CommandText = comand;
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByID", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}

		
		#endregion

		#region Optimalize

		public static DatabasePohoda.SKzDataTable SKz_GetDataByOptimalize()
		{
			DatabasePohoda.SKzDataTable dataTable = new DatabasePohoda.SKzDataTable();
			SKz_FillByOptimalize(dataTable);
			return dataTable;
		}

		public static void SKz_FillByOptimalize(DatabasePohoda.SKzDataTable dataTable)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ID, RefStruct, RelSKzVC, IDS, EAN, Nazev, MJ, StavZ, RefSklad, RefAD, MJ2, MJ3, MJ2Koef, MJ3Koef FROM SKz";
				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillByOptimalize", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#region ExportovatPouzeAktivniPolozky a Zbozi_DotahovatAlternativniDodavatele


		/// <summary>
		/// SKzAlternatives_FillBy_ExportovatPouzeAktivniPolozky_DotahovatAlternativniDodavatele
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="ExportSkladFilter"></param>
		/// <param name="ExportTypFilter"></param>
		public static void SKz_FillBy_EPAP_DAD(DatabasePohoda.SKzDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
							"SKz.MJ2Koef, SKz.MJ3Koef " +
							"FROM SKz " +
							"INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
							"WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillBy_EPAP_DAD", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#region Zbozi_DotahovatAlternativniDodavatele


		/// <summary>
		/// SKzAlternatives_FillBy_DotahovatAlternativniDodavatele
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="ExportSkladFilter"></param>
		/// <param name="ExportTypFilter"></param>
		public static void SKz_FillBy_DAD(DatabasePohoda.SKzDataTable dataTable, string ExportSkladFilter, string ExportTypFilter)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
							"SKz.MJ2Koef, SKz.MJ3Koef " +
							"FROM SKz " +
							"INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
							"WHERE (s.IDS IN (" + ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + ExportTypFilter + "))";

				//da.SelectCommand.Parameters.AddWithValue("?", XXX);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKz_FillBy_DAD", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#endregion

		#region sCKurspol

		internal static DatabasePohoda.sCKurspolDataTable sCKurspol_GetDataByKod(int ID)
		{
            Globals_V1.LoadConfiguration();
			DatabasePohoda.sCKurspolDataTable dataTable = new DatabasePohoda.sCKurspolDataTable();
			sCKurspol_FillByKod(dataTable ,ID);
			return dataTable;
		}



		public static void sCKurspol_FillByKod(DatabasePohoda.sCKurspolDataTable dataTable, int ID)
		{

			System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
			try
			{
				da.SelectCommand = new System.Data.OleDb.OleDbCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText = @"SELECT ck.ID , ck.Kod ,ck.NBs from sCKurspol as ck " +
												" INNER join sCMeny as cm ON ck.Kod = cm.Kod " +
												" Where " +
												" cm.ID = ? " + 
												" AND " +
												" (ck.ID = ( SELECT MAX(ck.ID) from sCKurspol as ck  INNER join sCMeny as cm ON ck.Kod = cm.Kod Where cm.ID = ? ))";

				da.SelectCommand.Parameters.AddWithValue("?", ID);
				da.SelectCommand.Parameters.AddWithValue("?", ID);

				da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sCKurspol_FillByKod", ex);
				Fask.Logging.ExceptionHandler2.Handle(dataTable);
			}

		}


		#endregion

		#region  sSTR



				public static DatabasePohoda.sSTRDataTable sSTR_GetData()
				{
					DatabasePohoda.sSTRDataTable dataTable = new DatabasePohoda.sSTRDataTable();
					sSTR_Fill(dataTable);
					return dataTable;
				}

				public static void sSTR_Fill(DatabasePohoda.sSTRDataTable dataTable)
				{
					string pom = Globals_V1.LoadConfiguration();

					if (pom != "OK")
						throw new Exception("Nezdařilo se načteni konfigurace.");

					System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
					try
					{
						da.SelectCommand = new System.Data.OleDb.OleDbCommand();
						da.SelectCommand.CommandType = System.Data.CommandType.Text;

						da.SelectCommand.CommandText = @"select ID, SText, IDS from sSTR ";
						//da.SelectCommand.Parameters.AddWithValue("?", XXX);

						da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

						da.Fill(dataTable);
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " sSTR_Fill", ex);
						Fask.Logging.ExceptionHandler2.Handle(dataTable);
					}
				}

		#endregion

		#region SKzINV

		public static int SKzINV_UpdateQuery(
			System.Data.OleDb.OleDbConnection connection,
			System.Data.OleDb.OleDbTransaction Trans,
			decimal? StavZsk,
			decimal? StavZroz,
			int? Original_RefSKz,
			int? Original_RefSklad,
			int? Original_RefAg)
		{
			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");



			using (var com = connection.CreateCommand())
			{

				com.CommandType = System.Data.CommandType.Text;


				com.Connection = connection;
				com.Transaction = Trans;

				com.CommandText = "UPDATE SKzInv SET " +
					"StavZsk = ?, StavZroz = ? - StavZ, Audit = 1 " +
					" WHERE (RefSKz = ?) AND (RefSklad = ?) AND (RefAg = ?)";

				com.Parameters.AddWithValue("?", StavZsk.HasValue ? StavZsk : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", StavZroz.HasValue ? StavZroz : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefSKz.HasValue ? Original_RefSKz : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefSklad.HasValue ? Original_RefSklad : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefAg.HasValue ? Original_RefAg : (object)DBNull.Value);

				int result = 0;
				try
				{
					result = com.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzINV_UpdateQuery", ex);
					//Log.Write(dataTable);
				}
				return result;
			}
		}

		public static int SKzINV_UpdateQueryNotAudit(
					System.Data.OleDb.OleDbConnection connection,
					System.Data.OleDb.OleDbTransaction Trans,
					decimal? StavZsk,
					decimal? StavZroz,
					int? Original_RefSKz,
					int? Original_RefSklad,
					int? Original_RefAg)
		{
			if (connection == null)
				throw new Exception("Neni nastaven objekt connection");



			using (var com = connection.CreateCommand())
			{

				com.CommandType = System.Data.CommandType.Text;


				com.Connection = connection;
				com.Transaction = Trans;

				com.CommandText = "UPDATE SKzInv " +
					"SET StavZsk = ?, StavZroz = ? - StavZ, Audit = 1" +
					" WHERE (RefSKz = ?) AND (Audit <> 1) AND (RefSklad = ?) AND (RefAg = ?)";


				com.Parameters.AddWithValue("?", StavZsk.HasValue ? StavZsk : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", StavZroz.HasValue ? StavZroz : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefSKz.HasValue ? Original_RefSKz : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefSklad.HasValue ? Original_RefSklad : (object)DBNull.Value);
				com.Parameters.AddWithValue("?", Original_RefAg.HasValue ? Original_RefAg : (object)DBNull.Value);

				int result = 0;
				try
				{
					result = com.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " SKzINV_UpdateQuery", ex);
					//Log.Write(dataTable);
				}
				return result;
			}
		}

		public static Datasets.Inventura.SKzInvDataTable SKzInvNezauct()
		{
			try
			{
				Datasets.Inventura.SKzInvDataTable polozky = new Datasets.Inventura.SKzInvDataTable();
				string script = "";
				script = @"SELECT SKzInv.ID, SKzInv.RefAg, SKzInv.RefSKz, SKzInv.RefSklad, SKzInv.RelSKzVC, SKzInv.IDS, SKzInv.EAN, SKzInv.Nazev, SKzInv.MJ, SKzInv.StavZ, SKzInv.PLU ";



				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					script += ", SKz.VPrFXTS AS SKz_VPrFXTS, " +
						" SKz.RefVPrFVTS AS SKz_RefVPrFVTS, " +
						" SKz.RefVPrFPTS AS SKz_RefVPrFPTS, " +
						" SKz.RefVPrFITS AS SKz_RefVPrFITS, " +
						" SKz.RefVPrFXTS AS SKz_RefVPrFXTS, " +
						" SKz.RefVPrFDTS AS SKz_RefVPrFDTS, " +
						" SKz.VPrFDTS AS SKz_VPrFDTS, " +
						" SKz.VPrFITS AS SKz_VPrFITS, " +
						" SKz.VPrFPTS AS SKz_VPrFPTS, " +
						" SKz.VPrFVTS AS SKz_VPrFVTS ";

				}



				script += " FROM dbo.SKzInv INNER JOIN dbo.SKzInvLst ON SKzInv.RefAg = SKzInvLst.ID LEFT OUTER JOIN dbo.SKz ON SKz.ID = SKzInv.RefSKz " +
						" WHERE (SKzInvLst.Zauct <= 0)";


				Fill_Universal(polozky, script);
				return polozky;

			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		public static Datasets.Inventura.SKzInvLstDataTable SKzInvList()
		{
			//Pohoda_Datasets.Inventura.SKzInvLstDataTable polozky = null;
			try
			{

				Datasets.Inventura.SKzInvLstDataTable polozky = new Datasets.Inventura.SKzInvLstDataTable();
				Fill_Universal(polozky, "SELECT ID, UsrOrder, Sel, RefSklad, Datum, SText, Zauct, OldDt, Pozn, DatCreate, DatSave, Oznacil, Ucetni, Creator FROM SKzInvLst");
				return polozky;


			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		public static Datasets.Inventura.SKzInvAlternativniDodavateleDataTable SKzInvNezauctAlternativy()
		{
			Datasets.Inventura.SKzInvAlternativniDodavateleDataTable polozky = null;
			try
			{
				polozky = new Datasets.Inventura.SKzInvAlternativniDodavateleDataTable();
				Fill_Universal(polozky, @"SELECT SKzInv.IDS, SKzInv.EAN, SKzInv.Nazev, SKzInv.MJ, SKzInv.PLU, SKzNC.RefAD AS NCDodavatelID, SKzNC.Firma AS NCFirma, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, SKzInv.RefAg, SKzInv.RefSKz FROM (( SKzInv INNER JOIN SKz ON SKzInv.RefSKz = SKz.ID ) INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg )");
				return polozky;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		public static Datasets.Inventura.SKzInvDataTable SKzInv()
		{
			Datasets.Inventura.SKzInvDataTable polozky = null;
			try
			{
				polozky = new Datasets.Inventura.SKzInvDataTable();
				string script;
				script = @"SELECT SI.ID, SI.UsrOrder, SI.Sel, SI.RefAg, SI.RefSKz, SI.RelAgID, SI.RefDokl, SI.RelSkTyp, SI.RelSkDruh, SI.AUcet, SI.RelZcTp, SI.RefStruct, SI.RefSklad, SI.RelSKzVC, SI.IDS, SI.EAN, SI.Nazev, SI.SText, SI.MJ, SI.StavZ, SI.StavZsk, " +
						 " SI.StavZroz, SI.VNakup, SI.VNakupC, SI.VNakupJ, SI.PLU, SI.Pozn, SI.Audit, SI.Prenes, SI.PrenesInvSPol, SI.Oznacil, SI.Ucetni, SI.Creator ";
						 

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					script += ", SKz.VPrFXTS AS SKz_VPrFXTS, " +
						" SKz.RefVPrFVTS AS SKz_RefVPrFVTS, " +
						" SKz.RefVPrFPTS AS SKz_RefVPrFPTS, " +
						" SKz.RefVPrFITS AS SKz_RefVPrFITS, " +
						" SKz.RefVPrFXTS AS SKz_RefVPrFXTS, " +
						" SKz.RefVPrFDTS AS SKz_RefVPrFDTS, " +
						" SKz.VPrFDTS AS SKz_VPrFDTS, " +
						" SKz.VPrFITS AS SKz_VPrFITS, " +
						" SKz.VPrFPTS AS SKz_VPrFPTS, " +
						" SKz.VPrFVTS AS SKz_VPrFVTS ";

				}

				script += " FROM dbo.SKzInv AS SI LEFT OUTER JOIN dbo.SKz ON SKz.ID = SI.RefSKz";

				Fill_Universal(polozky, script);
				return polozky;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		public static Datasets.Inventura.SKzInvSeznamyPolDataTable SKzInvSeznamyPol()
		{
			Datasets.Inventura.SKzInvSeznamyPolDataTable polozky = null;
			try
			{

				polozky = new Datasets.Inventura.SKzInvSeznamyPolDataTable();
				Fill_Universal(polozky, "SELECT ID, Sel, RefAg, RefInvPol, RefSKz, RefSKz0, SText, Pozn, Kod, VCislo, SKzVC, Mnozstvi, MJ, MJKoef, BPrenes, OrderFld FROM SKzInvSeznamyPol");
				return polozky;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		#endregion

		#region SKzVC

		#region Update_SKzVC

		public static int Update_SKzVC(System.Data.OleDb.OleDbConnection connPOH, System.Data.OleDb.OleDbTransaction transPOH, string iTEMNMBR, string sERLTNUM, string attributeToSN)
        {
            int result = 0;


            if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
            {
                System.Data.OleDb.OleDbDataAdapter da = null;

                try
                {

                    using (var com = new System.Data.OleDb.OleDbCommand())
                    {
                        com.Connection = connPOH;
                        com.Transaction = transPOH;

                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = @"UPDATE SKzVC SET VPrSarzeKSN = ? WHERE (RefAg = ?) AND (VCislo = ?)";

                        com.Parameters.AddWithValue("?", attributeToSN);
                        com.Parameters.AddWithValue("?", iTEMNMBR);
                        com.Parameters.AddWithValue("?", sERLTNUM);

                        using (da = new System.Data.OleDb.OleDbDataAdapter())
                        {
                            da.UpdateCommand = com;
                            result = da.UpdateCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Update_SKzVC", ex);
                    //Log.writeErrorData(dataTable);
                }
            }
            return result;
        }

		#endregion

		#region Update_SKzVC

		public static int Update_SKzVC(System.Data.OleDb.OleDbConnection connPOH, System.Data.OleDb.OleDbTransaction transPOH, string iTEMNMBR, string sERLTNUM, DateTime expirace)
		{
			int result = 0;


			if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				System.Data.OleDb.OleDbDataAdapter da = null;

				try
				{

					using (var com = new System.Data.OleDb.OleDbCommand())
					{
						com.Connection = connPOH;
						com.Transaction = transPOH;

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = @"UPDATE SKzVC SET VPrExspiraceKSN = ? WHERE (RefAg = ?) AND (VCislo = ?)";

						com.Parameters.AddWithValue("?", expirace);
						com.Parameters.AddWithValue("?", iTEMNMBR);
						com.Parameters.AddWithValue("?", sERLTNUM);

						using (da = new System.Data.OleDb.OleDbDataAdapter())
						{
							da.UpdateCommand = com;
							result = da.UpdateCommand.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Update_SKzVC", ex);
					//Log.writeErrorData(dataTable);
				}
			}
			return result;
		}

		#endregion

		#region Update_SKzVC2

		public static int Update_SKzVC(System.Data.OleDb.OleDbConnection connPOH, System.Data.OleDb.OleDbTransaction transPOH, string ID, string attributeToSN)
		{
			int result = 0;


			if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				System.Data.OleDb.OleDbDataAdapter da = null;

				try
				{

					using (var com = new System.Data.OleDb.OleDbCommand())
					{
						com.Connection = connPOH;
						com.Transaction = transPOH;

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = @"UPDATE SKzVC SET VPrSarzeKSN = ? WHERE (ID = ?)";

						com.Parameters.AddWithValue("?", attributeToSN);
						com.Parameters.AddWithValue("?", ID);

						using (da = new System.Data.OleDb.OleDbDataAdapter())
						{
							da.UpdateCommand = com;
							result = da.UpdateCommand.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Update_SKzVC", ex);
					//Log.writeErrorData(dataTable);
				}
			}
			return result;
		}

		#endregion

		#region Update_SKzVC2

		public static int Update_SKzVC(System.Data.OleDb.OleDbConnection connPOH, System.Data.OleDb.OleDbTransaction transPOH, string ID, DateTime expirace)
		{
			int result = 0;


			if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				System.Data.OleDb.OleDbDataAdapter da = null;

				try
				{

					using (var com = new System.Data.OleDb.OleDbCommand())
					{
						com.Connection = connPOH;
						com.Transaction = transPOH;

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = @"UPDATE SKzVC SET VPrExspiraceKSN = ? WHERE (ID = ?)";

						com.Parameters.AddWithValue("?", expirace);
						com.Parameters.AddWithValue("?", ID);

						using (da = new System.Data.OleDb.OleDbDataAdapter())
						{
							da.UpdateCommand = com;
							result = da.UpdateCommand.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle("Pohoda Tabulky Definice SQL Dotazu", " Update_SKzVC", ex);
					//Log.writeErrorData(dataTable);
				}
			}
			return result;
		}

		#endregion

		#region Get_AttributeToSN

		public static DatabasePohoda.SKzVCDataTable SKzVC_AttributeToSN(string ITEMNMBR, string SERLTNUM)
		{
			DatabasePohoda.SKzVCDataTable polozky = null;
			try
			{
				polozky = new DatabasePohoda.SKzVCDataTable();

				string SQL;
				SQL = @"select ID, RefAg, VCislo, StavVC, " +
					" RelSKzVc, DatExp ";
				
				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					SQL += ", VPrSarzeKSN, VPrExspiraceKSN ";
				}

				SQL += " from SKzVC where 1 = 1";

				if(!string.IsNullOrEmpty(ITEMNMBR))
                {
					SQL += " AND RefAg = '" + ITEMNMBR.Trim() + "' ";
				}

				if (!string.IsNullOrEmpty(SERLTNUM))
				{
					SQL += " AND VCislo = '" + SERLTNUM.Trim() + "' ";
				}


				Fill_Universal(polozky, SQL);

				return polozky;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}


		#endregion

		#endregion

		#region SKMP


		internal static DatabasePohoda.SKMPPol_SarzeDataTable getSKMP_ByCislo(string cislo)
		{
			DatabasePohoda.SKMPPol_SarzeDataTable polozky = null;
			try
			{
				polozky = new DatabasePohoda.SKMPPol_SarzeDataTable();

				string SQL;

				SQL = @"SELECT P.VPrSarzeKSN, V.ID, P.VPrExspiraceKSN from SKMP as H " +
						" LEFT JOIN SKMPpol as P ON P.RefAg = H.ID " +
						" LEFT JOIN SKzVC as V ON V.RefAg = P.RefSKz1 AND V.VCislo = P.VCislo" +
						" WHERE 1 = 1";

				if (!string.IsNullOrEmpty(cislo))
				{
					SQL += " AND Cislo = '" + cislo.Trim() + "' ";
				}

				Fill_Universal(polozky, SQL);

				return polozky;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		#endregion

		#endregion

		#region 28.11.2018 Puvodni Metody s dotazama

		[Obsolete("Nepoužívá se...")]
		public static DatabasePohoda.OBJpolRow OBJpol_ROW(int RefSKz)
		{
			/// TaD 28.11.2018 Metoda obdahuje GetData ale nikde se nepoužívá

			//DatabasePohoda.OBJpolDataTable tbl_OBJpol = null;
			//try
			//{

			//    DatabasePohodaTableAdapters.OBJpolTableAdapter ta_OBJpol = new DatabasePohodaTableAdapters.OBJpolTableAdapter();
			//    ta_OBJpol.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
			//    tbl_OBJpol = ta_OBJpol.GetData();
			//    DatabasePohoda.OBJpolRow[] pom = ( DatabasePohoda.OBJpolRow[])tbl_OBJpol.Select("RefSKz=" + RefSKz);
			//    return pom.Length == 0 ? null : pom[0];
			//}
			//catch (SqlException sqlex)
			//{
			//    return null;
			//}
			//catch (Exception ex)
			//{
			return null;
			//}
			//finally
			//{
			//}
		}

		/// <summary>
		/// Testuje, zda cislo je existujici cislo faktury
		/// </summary>
		/// <param name="cislo">cislo faktury</param>
		/// <returns>True: pokud existuje faktura s timto cislem</returns>
		public static bool FA_Exists(string cislo)
		{

			// \TODO : test na radu dokladu ??? 
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from FA where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleCommand.Connection.Open();

				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo faktury
				else
					return true; // existuje cislo faktury

			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}


		/// <summary>
		/// Zjistuje, zda jsou na dokladu pouze polozky skladu
		/// </summary>
		/// <param name="cislo">Cislo faktury, ktera se ma proverit</param>
		/// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
		/// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
		/// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
		public static bool FA_JedenSklad(string cislo, string sklad)
		{
			// SQL => test, zda na dokladu existuji polozky z jinych skladu
			// => vraci pocet jinych skladu nez je urceny z polozek dokladu
			// \TODO: co pokud je textova polozka?
			// => nema sklad a nebude nalezena ...
			//1) vrati ID dokladu ...
			// Select ID from FA where Cislo=? 
			//2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
			//select count(RefSklad) as JineSklady
			//from SKz
			//where ID in (
			//    select RefSKz
			//    from FApol
			//    where RefAg in (
			//        select ID from FA 
			//        where Cislo='162000002'
			//        )
			//    )
			//and RefSklad <> 1

			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .1");
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from FA where Cislo=?",
					oleConnection
					);

				oleCommand.Parameters.AddWithValue("?", cislo);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .2");
				oleConnection.Open();
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .3");
				object o = oleCommand.ExecuteScalar();
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .4");
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo faktury '" + cislo + "'");
				}

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .5");
				int FA_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .6");
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from FApol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .7");
				oleCommand.Parameters.AddWithValue("?", FA_ID);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .8");
				o = oleCommand.ExecuteScalar();
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .9");
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .10");
					int pocetPolozekZJinychSkladu = (int)o;
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .11");
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Faktura '" + cislo + "' obsahuje položky z více skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .12");
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FA_JedenSklad: .13");
			}

		}


		// dale jak rozlisit prijem/vydej ???
		public static bool Prevod_Exists(string cislo)
		{

			// \TODO : test na radu dokladu ??? 
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from SKMP where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo prevodky
				else
					return true; // existuje cislo prevodky
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}


		/// <summary>
		/// Testuje, zda cislo je existujici cislo prevodky pro vybrany cilovy sklad
		/// </summary>
		/// <param name="cislo">cislo prevodky</param>
		/// <param name="sklad">id ciloveho skladu</param>
		/// <returns>True: pokud existuje prevodka s timto cislem pro zvoleny sklad</returns>
		public static bool Prevod_Exists(string cislo, string sklad)
		{
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				// \TODO sjednotit s Prevod_Exists(string cislo)
				// \TODO : test na radu dokladu ??? 

				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from SKMP where Cislo=? and RefSkladC=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleCommand.Parameters.AddWithValue("?", sklad);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo prevodky
				else
					return true; // existuje cislo prevodky
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		/// <summary>
		/// Zjistuje, zda jsou na dokladu pouze polozky jednoho zdrojoveho skladu
		/// </summary>
		/// <param name="cislo">Cislo prevodky, ktera se ma proverit</param>
		/// <param name="sklad">Cislo zdrojoveho skladu, ktery a jen ma byt na dokladu</param>
		/// <returns>Exception(False):jestlize obsahuje polozky jineho zdrojoveho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
		/// <exception>Vyjimka: jestlize obsahuje polozky jineho zdrojoveho skladu</exception>
		public static bool Prevod_JedenZdrojSklad(string cislo, string sklad)
		{
			// SQL => test, zda na dokladu existuji polozky z jinych skladu
			// => vraci pocet jinych skladu nez je urceny z polozek dokladu
			// \TODO: co pokud je textova polozka?
			// => nema sklad a nebude nalezena ...
			//1) vrati ID obj ...
			// Select ID from SKMP where Cislo=? 
			//2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
			//select count(RefSklad) as JineSklady
			//from SKz
			//where ID in (
			//    select RefSKz
			//    from SKMPpol
			//    where RefAg in (
			//        select ID from SKMP 
			//        where Cislo='162000002'
			//        )
			//    )
			//and RefSklad <> 1

			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from SKMP where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo převodky '" + cislo + "'");
				}

				oleCommand.Parameters.Clear();
				int SKMP_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from SKMPpol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				oleCommand.Parameters.AddWithValue("?", SKMP_ID);
				o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					int pocetPolozekZJinychSkladu = (int)o;
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Převodka '" + cislo + "' obsahuje položky z jiných skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}


		/// <summary>
		/// Testuje, zda cislo je existujici cislo objednavky
		/// </summary>
		/// <param name="cislo">cislo prevodky</param>
		/// <returns>True: pokud existuje prevodka s timto cislem</returns>
		public static bool OBJ_Exists(string cislo)
		{
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				// \TODO : test na radu dokladu ??? 

				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from OBJ where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo prevodky
				else
					return true; // existuje cislo prevodky
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		/// <summary>
		/// Testuje, zda Objednavka s uvedenym cislem je Rezervovana
		/// </summary>
		/// <param name="cislo">cislo objednavky</param>
		/// <returns>True: pokud je objednavka rezervovana, False: neni rezervovana</returns>
		public static bool OBJ_Reserved(string cislo)
		{
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				// \TODO : test na radu dokladu ??? 

				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Rezer from OBJ where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo obje
				else if (o is bool)
				{
					return (bool)o;
				}
				else
				{
					return false;
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}


		#region 28.11.2018 TaD Zakomentovano, nepoužíva se prešlo se na Faktury kvuli rezervacim
		/// <summary>
		/// Provede odrezervovani objednavky => Obj. prijate...
		/// </summary>
		/// <param name="listObjednavek"></param>
		/// <returns></returns>
		//public static bool OBJ_OdRezervovat(List<string> listObjednavek)
		//{

		//if (listObjednavek == null) //null by byt nemel...
		//    return true;

		//if ((listObjednavek != null) && (listObjednavek.Count <= 0)) //pokud je list prazdny, tak nic nemusim resit ... 
		//    return true; 

		//// !!! toto plati jen pro Objednavky Prijate !!!
		//// ??? Trvaly doklad ??? => jde rezervovat? (doufam ze ne ...)
		////      - pokud nejde rezervovat, tak nerezervovat
		//// !!! v transakci od zacatku ... !!!
		//// 1) dotahnout objednavky 
		//// 2) dotahnout polozky objednavek
		//// 3) dotahnout polozky z objednavek z SKz a SKzBuf
		////      => ?overit zda SKz a SKzBuf pro dane polozky maji stejne hodnoty ObjedP, ObjedV, Rezer, Reklam,...
		////      => ?overit zda neni Foul<>0 -> toto pak znamena, ze nekdo s tim pracuje? a hodnoty nejsou komitnuty...?
		//// 4) objednavka nastavit Rezer=0
		//// 5) kazdou polozku zjistit nedodane mnozstvi
		//// 6) pro kazdou polozku na skladove zasobe z Rezer prevest do ObjedP
		////      => Skz.ObjedP += Nedodane
		////      => Skz.Rezer  -= Nedodane
		//// 7) upravit SkzBuf - rezer a objedp
		//// 7) ulozit zmeny

		//System.Data.OleDb.OleDbConnection oledbConnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
		//System.Data.OleDb.OleDbTransaction oledbTransaction = null;

		//try
		//{

		//    // Inicializace
		//    oledbConnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
		//    oledbConnection.Open();
		//    oledbTransaction = oledbConnection.BeginTransaction(System.Data.IsolationLevel.Serializable);

		//    DatabasePohoda pohodaDS = new DatabasePohoda();

		//    // Dotazeni polozek objednavek
		//    DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new DatabasePohodaTableAdapters.OBJTableAdapter();
		//    DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new DatabasePohodaTableAdapters.OBJpolTableAdapter();

		//    objAdapter.Connection = oledbConnection;
		//    objpolAdapter.Connection = oledbConnection;

		//    objAdapter.Transaction = oledbTransaction;
		//    objpolAdapter.Transaction = oledbTransaction;

		//    objAdapter.ClearBeforeFill = false;
		//    objpolAdapter.ClearBeforeFill = false;

		//    // dotazeni hlavicek objednavek
		//    listObjednavek.ForEach(x => objAdapter.FillByCislo(pohodaDS.OBJ, x));
		//    // dotazeni polozek objednavek pro kazdou hlavicku, pokud neni Trvaly doklad (nemuze byt vyrizen ani rezervovan...)
		//    //pohodaDS.OBJ.ToList().ForEach(x => objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID));
		//    foreach (var objHlavicka in pohodaDS.OBJ)
		//    {
		//        // trvale doklady preskakuji
		//        // vyrizene preskakuji
		//        // nerezervovane preskakuji (ale zde by meli byt jen rezervovane ...)
		//        if (!objHlavicka.TrvalyDok && !objHlavicka.Vyrizeno && objHlavicka.Rezer)
		//        {
		//            objHlavicka.Rezer = false; // odrezervace 
		//            //objpolAdapter.FillByRefAg(pohodaDS.OBJpol, objHlavicka.ID);
		//            Database.Pohoda.OBJPol_FillByRefAg(pohodaDS.OBJpol, objHlavicka.ID);
		//        }
		//    }


		//    DatabasePohodaTableAdapters.SKzObjedPTableAdapter skzObjedPAdapter = new DatabasePohodaTableAdapters.SKzObjedPTableAdapter();
		//    DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new DatabasePohodaTableAdapters.SKzBufTableAdapter();

		//    skzObjedPAdapter.Connection = oledbConnection;
		//    skzBufAdapter.Connection = oledbConnection;

		//    skzObjedPAdapter.Transaction = oledbTransaction;
		//    skzBufAdapter.Transaction = oledbTransaction;

		//    skzObjedPAdapter.ClearBeforeFill = false;
		//    skzBufAdapter.ClearBeforeFill = false;

		//    // dotazeni polozek a zmena rezer a objedp
		//    foreach (var objPol in pohodaDS.OBJpol)
		//    {
		//        DatabasePohoda.SKzObjedPRow skzObjedPRow = pohodaDS.SKzObjedP.FindByID(objPol.RefSKz);
		//        var skzBufRows = pohodaDS.SKzBuf.Where(x => x.RefSKz == objPol.RefSKz);

		//        if (skzObjedPRow == null)
		//        {
		//            skzObjedPAdapter.FillByID(pohodaDS.SKzObjedP, objPol.RefSKz);
		//            skzObjedPRow = pohodaDS.SKzObjedP.FindByID(objPol.RefSKz);
		//        }
		//        if (skzBufRows.Count() <= 0)
		//        {
		//            skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, objPol.RefSKz);
		//            skzBufRows = pohodaDS.SKzBuf.Where(x => x.RefSKz == objPol.RefSKz);
		//        }

		//        double zbyvadodat = (objPol.Mnozstvi - objPol.Dodano);
		//        if (skzObjedPRow == null)
		//            Log.writeErrorLog("Fask.ModulePohodaXML", "OBJ_OdRezervovat", "Nenalezena karta zasoby (SKz): " + objPol.RefSKz);
		//        else
		//        {
		//            //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
		//            skzObjedPRow.Rezer -= zbyvadodat;
		//            skzObjedPRow.ObjedP += zbyvadodat;
		//        }

		//        if (skzBufRows.Count() <= 0)
		//            Log.writeErrorLog("Fask.ModulePohodaXML", "OBJ_OdRezervovat", "Nenalezena karta zasoby (SKzBuf): " + objPol.RefSKz);
		//        else
		//        {
		//            foreach (var skzBufRow in skzBufRows)
		//            {
		//                skzBufRow.Rezer -= zbyvadodat;
		//                skzBufRow.ObjedP += zbyvadodat;
		//            }
		//        }
		//    }

		//    // Ulozit zmeny
		//    objAdapter.Update(pohodaDS.OBJ);
		//    objpolAdapter.Update(pohodaDS.OBJpol);
		//    skzObjedPAdapter.Update(pohodaDS.SKzObjedP);
		//    skzBufAdapter.Update(pohodaDS.SKzBuf);

		//    try
		//    {
		//        if (oledbTransaction != null)
		//            oledbTransaction.Commit();
		//    }
		//    catch (Exception exOledbCommit)
		//    {
		//        throw exOledbCommit;
		//    }

		//    return true;
		//}
		//catch (Exception ex)
		//{
		//    Logging.Log.writeErrorLog(ex.Message);

		//    try
		//    {
		//        if (oledbTransaction != null)
		//            oledbTransaction.Rollback();
		//    }
		//    catch (Exception exOledbRollback)
		//    {
		//        Logging.Log.writeErrorLog(exOledbRollback.Message);
		//        throw exOledbRollback;  // nelze dale pokracovat
		//        //return false; // nelze dale pokracovat
		//    }

		//    return false;
		//}
		//finally
		//{
		//    if ((oledbConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
		//        oledbConnection.Close();
		//} 

		//}


		/// <summary>
		/// Provede odrezervovani objednavky => Obj. prijate...
		/// </summary>
		/// <param name="listObjednavek"></param>
		/// <returns></returns>
		//public static bool OBJ_ZaRezervovat(List<string> listObjednavek)
		//{
		//    if (listObjednavek == null) //null by byt nemel...
		//        return true;

		//    if ((listObjednavek != null) && (listObjednavek.Count <= 0)) //pokud je list prazdny, tak nic nemusim resit ... 
		//        return true; 

		//    // !!! toto plati jen pro Objednavky Prijate !!!
		//    // ??? Trvaly doklad ??? => jde rezervovat? (doufam ze ne ...)
		//    //      - pokud nejde rezervovat, tak nerezervovat
		//    // !!! v transakci od zacatku ... !!!
		//    // !!! Pokud je uplne dodano, tak se nedeji zadne dalsi rezervace, protoze uplnym dodanim jsou rezervace zruseny...
		//    // !!! Pokud je castecne dodano, ale vyrizeno, tak rezervace nenastavovat, protoze vyrizenim jsou rezervace zruseny...
		//    // 1) dotahnout objednavky 
		//    // 2) dotahnout polozky objednavek
		//    // 3) dotahnout polozky z objednavek z SKz a SKzBuf
		//    //      => ?overit zda SKz a SKzBuf pro dane polozky maji stejne hodnoty ObjedP, ObjedV, Rezer, Reklam,...
		//    //      => ?overit zda neni Foul<>0 -> toto pak znamena, ze nekdo s tim pracuje? a hodnoty nejsou komitnuty...?
		//    // 4) objednavka nastavit Rezer=1
		//    // 5) kazdou polozku zjistit nedodane mnozstvi
		//    // 6) pro kazdou polozku na skladove zasobe z ObjedP prevest do Rezer
		//    //      => Skz.ObjedP -= Nedodane
		//    //      => Skz.Rezer  += Nedodane
		//    // 7) ulozit zmeny

		//    System.Data.OleDb.OleDbConnection oledbConnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
		//    System.Data.OleDb.OleDbTransaction oledbTransaction = null;

		//    try
		//    {

		//        // Inicializace
		//        oledbConnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
		//        oledbConnection.Open();
		//        oledbTransaction = oledbConnection.BeginTransaction(System.Data.IsolationLevel.Serializable);

		//        DatabasePohoda pohodaDS = new DatabasePohoda();

		//        // Dotazeni polozek objednavek
		//        DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new DatabasePohodaTableAdapters.OBJTableAdapter();
		//        DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new DatabasePohodaTableAdapters.OBJpolTableAdapter();

		//        objAdapter.Connection = oledbConnection;
		//        objpolAdapter.Connection = oledbConnection;

		//        objAdapter.Transaction = oledbTransaction;
		//        objpolAdapter.Transaction = oledbTransaction;

		//        objAdapter.ClearBeforeFill = false;
		//        objpolAdapter.ClearBeforeFill = false;

		//        // dotazeni hlavicek objednavek
		//        listObjednavek.ForEach(x => objAdapter.FillByCislo(pohodaDS.OBJ, x));
		//        // dotazeni polozek objednavek pro kazdou hlavicku, pokud neni Trvaly doklad (nemuze byt vyrizen ani rezervovan...)
		//        //pohodaDS.OBJ.ToList().ForEach(x => objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID));
		//        foreach (var objHlavicka in pohodaDS.OBJ)
		//        {
		//            // trvale doklady preskakuji
		//            // vyrizene preskakuji
		//            // rezervovane preskakuji (ale zde by meli byt jen rezervovane ...)
		//            if (!objHlavicka.TrvalyDok && !objHlavicka.Vyrizeno && !objHlavicka.Rezer)
		//            {
		//                objHlavicka.Rezer = true;
		//                objpolAdapter.FillByRefAg(pohodaDS.OBJpol, objHlavicka.ID);
		//            }
		//        }


		//        DatabasePohodaTableAdapters.SKzObjedPTableAdapter skzObjedPAdapter = new DatabasePohodaTableAdapters.SKzObjedPTableAdapter();
		//        DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new DatabasePohodaTableAdapters.SKzBufTableAdapter();

		//        skzObjedPAdapter.Connection = oledbConnection;
		//        skzBufAdapter.Connection = oledbConnection;

		//        skzObjedPAdapter.Transaction = oledbTransaction;
		//        skzBufAdapter.Transaction = oledbTransaction;

		//        skzObjedPAdapter.ClearBeforeFill = false;
		//        skzBufAdapter.ClearBeforeFill = false;

		//        // dotazeni polozek a zmena rezer a objedp
		//        foreach (var objPol in pohodaDS.OBJpol)
		//        {
		//            DatabasePohoda.SKzObjedPRow skzObjedPRow = pohodaDS.SKzObjedP.FindByID(objPol.RefSKz);
		//            var skzBufRows = pohodaDS.SKzBuf.Where(x => x.RefSKz == objPol.RefSKz);

		//            if (skzObjedPRow == null)
		//            {
		//                skzObjedPAdapter.FillByID(pohodaDS.SKzObjedP, objPol.RefSKz);
		//                skzObjedPRow = pohodaDS.SKzObjedP.FindByID(objPol.RefSKz);
		//            }
		//            if (skzBufRows.Count() <= 0)
		//            {
		//                skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, objPol.RefSKz);
		//                skzBufRows = pohodaDS.SKzBuf.Where(x => x.RefSKz == objPol.RefSKz);
		//            }

		//            double zbyvadodat = (objPol.Mnozstvi - objPol.Dodano);
		//            if (skzObjedPRow == null)
		//                Log.writeErrorLog("Fask.ModulePohodaXML", "OBJ_ZaRezervovat", "Nenalezena karta zasoby (SKz): " + objPol.RefSKz);
		//            else
		//            {
		//                //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
		//                skzObjedPRow.Rezer += zbyvadodat;
		//                skzObjedPRow.ObjedP -= zbyvadodat;
		//            }

		//            if (skzBufRows.Count() <= 0)
		//                Log.writeErrorLog("Fask.ModulePohodaXML", "OBJ_ZaRezervovat", "Nenalezena karta zasoby (SKzBuf): " + objPol.RefSKz);
		//            else
		//            {
		//                foreach (var skzBufRow in skzBufRows)
		//                {
		//                    skzBufRow.Rezer += zbyvadodat;
		//                    skzBufRow.ObjedP -= zbyvadodat;
		//                }
		//            }
		//        }

		//        // Kontrola uplneho vykryti
		//        // Pokud je objednavka plne vykryta, priznak rezervovano zrusit ...
		//        foreach (var objrow in pohodaDS.OBJ)
		//        {
		//            bool plnevykryto = true;
		//            foreach (var objpolrow in pohodaDS.OBJpol.Where(x => x.RefAg == objrow.ID))
		//            {
		//                if (objpolrow.Mnozstvi != objpolrow.Dodano)
		//                {
		//                    plnevykryto = false;
		//                    break;
		//                }
		//            }
		//            if (plnevykryto)
		//            {
		//                objrow.Rezer = false;
		//            }
		//        }

		//        // Ulozit zmeny
		//        objAdapter.Update(pohodaDS.OBJ);
		//        objpolAdapter.Update(pohodaDS.OBJpol);
		//        skzObjedPAdapter.Update(pohodaDS.SKzObjedP);
		//        skzBufAdapter.Update(pohodaDS.SKzBuf);

		//        try
		//        {
		//            if (oledbTransaction != null)
		//                oledbTransaction.Commit();
		//        }
		//        catch (Exception exOledbCommit)
		//        {
		//            throw exOledbCommit;
		//        }

		//        return true;
		//    }
		//    catch (Exception ex)
		//    {
		//        Logging.Log.writeErrorLog(ex.Message);

		//        try
		//        {
		//            if (oledbTransaction != null)
		//                oledbTransaction.Rollback();
		//        }
		//        catch (Exception exOledbRollback)
		//        {
		//            Logging.Log.writeErrorLog(exOledbRollback.Message);
		//            throw exOledbRollback;  // nelze dale pokracovat
		//            //return false; // nelze dale pokracovat
		//        }

		//        return false;
		//    }
		//    finally
		//    {
		//        if ((oledbConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
		//            oledbConnection.Close();
		//    }
		//}

		#endregion


		/// <summary>
		/// Zjistuje, zda jsou na dokladu pouze polozky skladu
		/// </summary>
		/// <param name="cislo">Cislo objednavky, ktera se ma proverit</param>
		/// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
		/// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
		/// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
		public static bool OBJ_VYD_JedenSklad(string cislo, string sklad)
		{
			// SQL => test, zda na dokladu existuji polozky z jinych skladu
			// => vraci pocet jinych skladu nez je urceny z polozek dokladu
			// \TODO: co pokud je textova polozka?
			// => nema sklad a nebude nalezena ...
			//1) vrati ID obj ...
			// Select ID from OBJ where Cislo=? 
			//2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
			//select count(RefSklad) as JineSklady
			//from SKz
			//where ID in (
			//    select RefSKz
			//    from OBJpol
			//    where RefAg in (
			//        select ID from OBJ 
			//        where Cislo='162000002'
			//        )
			//    )
			//and RefSklad <> 1

			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from OBJ where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo objednávky '" + cislo + "'");
				}

				int OBJ_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from OBJpol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				oleCommand.Parameters.AddWithValue("?", OBJ_ID);
				o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					int pocetPolozekZJinychSkladu = (int)o;
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Objednávka '" + cislo + "' obsahuje položky z více skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		#region Vydejka

		/// <summary>
		/// Testuje, zda cislo je existujici cislo vydejky
		/// </summary>
		/// <param name="cislo">cislo vydejky</param>
		/// <returns>True: pokud existuje vydejka s timto cislem</returns>
		public static bool Vydejka_Exists(string cislo)
		{

			//  \TODO : test na radu dokladu ??? 
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from SKPV where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleCommand.Connection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo faktury
				else
					return true; // existuje cislo faktury

			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		/// <summary>
		/// Zjistuje, zda jsou na dokladu pouze polozky pozadovaneho skladu
		/// </summary>
		/// <param name="cislo">Cislo vydejky, ktera se ma proverit</param>
		/// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
		/// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
		/// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
		public static bool Vydejka_JedenSklad(string cislo, string sklad)
		{
			// SQL => test, zda na dokladu existuji polozky z jinych skladu
			// => vraci pocet jinych skladu nez je urceny z polozek dokladu
			// \TODO: co pokud je textova polozka?
			// => nema sklad a nebude nalezena ...
			//1) vrati ID dokladu ...
			// Select ID from SKPV where Cislo=? 
			//2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
			//select count(RefSklad) as JineSklady
			//from SKz
			//where ID in (
			//    select RefSKz
			//    from SKPVpol
			//    where RefAg in (
			//        select ID from SKPV 
			//        where Cislo='16SV00001'
			//        )
			//    )
			//and RefSklad <> 1

			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from SKPV where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
				}

				int VYDEJKA_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from SKPVpol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				oleCommand.Parameters.AddWithValue("?", VYDEJKA_ID);
				o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					int pocetPolozekZJinychSkladu = (int)o;
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Výdejka '" + cislo + "' obsahuje položky z více skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		#endregion

		#region Prodejka

		/// <summary>
		/// Testuje, zda cislo je existujici cislo prodejky
		/// </summary>
		/// <param name="cislo">cislo prodejky</param>
		/// <returns>True: pokud existuje prodejka s timto cislem</returns>
		public static bool Prodejka_Exists(string cislo)
		{

			// \TODO : test na radu dokladu ??? 
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select Cislo from PH where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleCommand.Connection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo faktury
				else
					return true; // existuje cislo faktury

			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		/// <summary>
		/// Zjistuje, zda jsou na dokladu pouze polozky pozadovaneho skladu
		/// </summary>
		/// <param name="cislo">Cislo dokladu, ktery se ma proverit</param>
		/// <param name="sklad">Cislo skladu, ktery a jen ma byt na dokladu</param>
		/// <returns>Exception(False):jestlize obsahuje polozky jineho skladu nez je nastaveny; True:pouze polozky daneho skladu</returns>
		/// <exception>Vyjimka: jestlize obsahuje polozky jineho skladu</exception>
		public static bool Prodejka_JedenSklad(string cislo, string sklad)
		{
			// SQL => test, zda na dokladu existuji polozky z jinych skladu
			// => vraci pocet jinych skladu nez je urceny z polozek dokladu
			// \TODO: co pokud je textova polozka?
			// => nema sklad a nebude nalezena ...
			//1) vrati ID dokladu ...
			// Select ID from SKPV where Cislo=? 
			//2) pouzije se v druhem dotazu ... (problem se zanorenim IN ...)
			//select count(RefSklad) as JineSklady
			//from SKz
			//where ID in (
			//    select RefSKz
			//    from PHpol
			//    where RefAg in (
			//        select ID from PH 
			//        where Cislo='16PH00001'
			//        )
			//    )
			//and RefSklad <> 1

			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from PH where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
				}

				int PRODEJKA_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from PHpol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				oleCommand.Parameters.AddWithValue("?", PRODEJKA_ID);
				o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					int pocetPolozekZJinychSkladu = (int)o;
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Prodejka '" + cislo + "' obsahuje položky z více skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		#endregion

		internal static bool PrjateObjednavky_JedenSklad(string cislo, string sklad)
		{
			System.Data.OleDb.OleDbConnection oleConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			try
			{
				System.Data.OleDb.OleDbCommand oleCommand = new System.Data.OleDb.OleDbCommand(
					"Select ID from OBJ where Cislo=?",
					oleConnection
					);
				oleCommand.Parameters.AddWithValue("?", cislo);
				oleConnection.Open();
				object o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					throw new Exception("Neexistuje číslo výdejky '" + cislo + "'");
				}

				int VYDEJKA_ID = (int)o; // toto musi byt ID Objednavky
				oleCommand.Parameters.Clear();
				oleCommand.CommandText =
					"Select count(RefSklad) as JineSklady " +
					"from SKz " +
					"where ID in ( " +
					"    select RefSKz " +
					"    from OBJpol " +
					"    where RefAg=? " +
					"    ) " +
					"and RefSklad <> " + sklad;
				oleCommand.Parameters.AddWithValue("?", VYDEJKA_ID);
				o = oleCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
				{
					return true;
				}
				else
				{
					int pocetPolozekZJinychSkladu = (int)o;
					if (pocetPolozekZJinychSkladu > 0)
					{
						throw new Exception("Výdejka '" + cislo + "' obsahuje položky z více skladů!");
					}
					else
					{
						return true;
					}
				}
			}
			finally
			{
				if ((oleConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					oleConnection.Close();
			}
		}

		//internal static bool OBJPol_Validace(string ITEMNMBR, decimal QTY)
		//internal static InfoValidace OBJPol_Validace(Fask.Vydej vydejdata)
		//{

		//    //VydejTableAdapters.CZMST_SITableAdapter seta = new VydejTableAdapters.CZMST_SITableAdapter();
		//    //seta.Connection = new SqlConnection(Globals.ConnectionString);

		//    //DatabasePohodaTableAdapters.KontrolaTableAdapter Kta = new DatabasePohodaTableAdapters.KontrolaTableAdapter();
		//    //Kta.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

		//    //Vydej.CZMST_SIDataTable dtSE = new Vydej.CZMST_SIDataTable();
		//    Fask.Vydej dsVydej = new Fask.Vydej();

		//    try
		//    {

		//        //var senotinsi = vydejdata.CZMST_SE.Where(x => !vydejdata.CZMST_SI.Any( y => y.ORD == x.ORD));
		//        //foreach (var se in senotinsi)
		//        //{
		//        //    vydejdata.CZMST_SI.AddCZMST_SIRow(
		//        //        se.CountEntries,
		//        //        se.SOPNUMBE,
		//        //        se.ITEMNMBR,
		//        //        se.ORD,
		//        //        se.VNDDOCNM,
		//        //        se.VNDITNUM,
		//        //        se.CZ_CarKod,
		//        //        se.SKL_ID,
		//        //        se.LOCNCODE,
		//        //        0,
		//        //        0,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        0,
		//        //        0,
		//        //        string.Empty,
		//        //        string.Empty,
		//        //        false,
		//        //        Guid.NewGuid(),
		//        //        string.Empty,
		//        //        0,
		//        //        0,
		//        //        string.Empty,
		//        //        0
		//        //        );
		//        //}

		//        //seta.FillByCountEntriesOrderBy_ITEMNMBR_ID(dtSE, CountEntries);

		//        if ((vydejdata != null) && (vydejdata.CZMST_SI != null) && (vydejdata.CZMST_SI.Count > 0))
		//        {

		//            string result;

		//            Fask.Vydej.VydekKontrola1Row PrevRow = null;

		//            #region pokus

		//            Fask.Vydej.VydekKontrola1Row Radek = dsVydej.VydekKontrola1.NewVydekKontrola1Row();

		//            bool Flag_PrevRow; // příznak zda se jedna o prvni zaznam 
		//            bool Flag_SameITEMNMBR;
		//            bool Flag_Rezervovano;

		//            decimal SKz_StavZ; // kompletny stav skladu pro danu položku
		//            decimal SKz_Rezer; // počet kolik je dohromady rezervovano na položke na sklade
		//            decimal SKz_ObjedP;  // počet kolik je dohromady na objednavkach .... ted se nepouživa

		//            decimal VolnePolozky; // jedna se o počet volnych položek ktere sou na sklade a nejsou rezervovany

		//            decimal? RezervovanoJA;
		//            decimal RezervovanoOstatni = 0;

		//            decimal PolozekZbude;

		//            decimal POhodaQTY;

		//            #endregion

		//            foreach (var item in vydejdata.CZMST_SI)
		//            {

		//                Radek = dsVydej.VydekKontrola1.NewVydekKontrola1Row();

		//                Flag_PrevRow = false; // příznak zda se jedna o prvni zaznam 
		//                Flag_SameITEMNMBR = false;
		//                Flag_Rezervovano = false;

		//                SKz_StavZ = 0; // kompletny stav skladu pro danu položku
		//                SKz_Rezer = 0; // počet kolik je dohromady rezervovano na položke na sklade
		//                SKz_ObjedP = 0;  // počet kolik je dohromady na objednavkach .... ted se nepouživa

		//                VolnePolozky = 0; // jedna se o počet volnych položek ktere sou na sklade a nejsou rezervovany

		//                RezervovanoJA = null;
		//                RezervovanoOstatni = 0;

		//                PolozekZbude = 0;

		//                POhodaQTY = GetOBJPredlohaQTY(item.ORD);

		//                if (PrevRow != null)
		//                {
		//                    Flag_PrevRow = true;

		//                    if (item.ITEMNMBR.Trim() == PrevRow.ITEMNMBR.Trim())
		//                    { Flag_SameITEMNMBR = true; }
		//                    else
		//                    { Flag_SameITEMNMBR = false; }
		//                }
		//                else
		//                {
		//                    Flag_PrevRow = false;
		//                    Flag_SameITEMNMBR = false;
		//                }

		//                //DatabasePohoda.KontrolaDataTable KontrolaDT = Kta.GetData_ITEMNMBR_SOPNUMBE(item.SOPNUMBE.Trim(), int.Parse(item.ITEMNMBR.Trim()));
		//                DatabasePohoda.KontrolaDataTable KontrolaDT = Pohoda.Kontrola_GetData_ITEMNMBR_SOPNUMBE(item.SOPNUMBE.Trim(), int.Parse(item.ITEMNMBR.Trim()));


		//                if ((KontrolaDT != null) && (KontrolaDT.Count > 0))
		//                {
		//                    SKz_StavZ = (decimal)KontrolaDT[0].SKz_StavZ;
		//                    Flag_Rezervovano = KontrolaDT[0].OBJ_Rezer;
		//                    SKz_Rezer = (decimal)KontrolaDT[0].SKz_Rezer;
		//                    SKz_ObjedP = (decimal)KontrolaDT[0].SKz_ObjedP;
		//                }
		//                else
		//                {
		//                    //Položka nenalezena ... preskakuju
		//                    Log.writeErrorLog("Položka nenalezena v Pohoda tabulkach...");
		//                    continue;
		//                }

		//                VolnePolozky = SKz_StavZ - (decimal)SKz_Rezer;

		//                if (Flag_Rezervovano)
		//                {
		//                    //RezervovanoJA = GetRezervovano(int.Parse(item.ITEMNMBR.Trim()), item.SOPNUMBE.Trim());
		//                    RezervovanoJA = POhodaQTY;

		//                    if (Flag_SameITEMNMBR)
		//                    {
		//                        RezervovanoOstatni = PrevRow.REZ_OSTATNI - POhodaQTY;
		//                        PolozekZbude = PrevRow.ZBUDE;// -item.QTYSHPPD;
		//                    }
		//                    else
		//                    {
		//                        RezervovanoOstatni = SKz_Rezer - POhodaQTY;
		//                        PolozekZbude = VolnePolozky;
		//                    }
		//                }
		//                else
		//                {
		//                    RezervovanoJA = null;
		//                    RezervovanoOstatni = SKz_Rezer;

		//                    if (Flag_SameITEMNMBR) // && string.IsNullOrEmpty(PrevRow.REZ_JA))
		//                    {
		//                        PolozekZbude = PrevRow.ZBUDE - item.QTYSHPPD;
		//                    }
		//                    else
		//                    {
		//                        PolozekZbude = VolnePolozky - item.QTYSHPPD;
		//                    }
		//                }

		//                Radek.DEX_ROW_ID = item.DEX_ROW_ID;
		//                Radek.ITEMDESC = ""; // neni potreba
		//                Radek.ITEMNMBR = item.ITEMNMBR.Trim();
		//                Radek.QTYSHPPD = item.QTYSHPPD;
		//                Radek.REZ_JA = RezervovanoJA == null ? string.Empty : ((decimal)RezervovanoJA).ToString("0.00");
		//                Radek.REZ_OSTATNI = RezervovanoOstatni;
		//                Radek.SOPNUMBE = item.SOPNUMBE.Trim();
		//                Radek.STAV_SKLAD = SKz_StavZ;
		//                Radek.ZADAT = item.QTYSHPPD;
		//                Radek.ZBUDE = PolozekZbude;
		//                Radek.QTY_OBJ_Pohoda = (decimal)POhodaQTY;


		//                PrevRow = Radek;

		//                dsVydej.VydekKontrola1.AddVydekKontrola1Row(Radek);

		//            }


		//            foreach (var item in dsVydej.VydekKontrola1)
		//            {

		//                if (item.IsREZ_JANull() || string.IsNullOrEmpty(item.REZ_JA))
		//                {
		//                    if (item.ZBUDE < 0)
		//                        return new InfoValidace("ERR", item);
		//                }
		//                else
		//                {
		//                    if (item.QTYSHPPD > item.STAV_SKLAD)
		//                        return new InfoValidace("ERR", item);
		//                }
		//            }

		//            return new InfoValidace("OK", null);

		//        }
		//        else
		//        {
		//            return new InfoValidace("Nenalezena data k validaci!", null);
		//        }


		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }

		//}

		internal static decimal GetOBJPredlohaQTY(int ORD)
		{
			System.Data.OleDb.OleDbConnection connection = null;
			System.Data.OleDb.OleDbCommand command = null;
			//System.Data.OleDb.OleDbDataAdapter adapter = null;

			try
			{

				connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				command = new System.Data.OleDb.OleDbCommand();
				//adapter = new System.Data.OleDb.OleDbDataAdapter();


				command.CommandText = "select Mnozstvi" +
					" from OBJPol " +
					" where ID = " + ORD.ToString();


				command.Connection = connection;
				//adapter.SelectCommand = command;


				connection.Open();

				object rez_jaOBJECT = command.ExecuteScalar();

				connection.Close();



				if (rez_jaOBJECT is double)
				{
					return (decimal)((double)rez_jaOBJECT);
				}
				else
				{
					throw new Exception("Nenalezena polozka na Objednavke...");
					//return null;
				}

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw new Exception("Nenalezena polozka na Objednavke...");
				//return null;

			}
		}

		#endregion

		#region FIFO/FEFO dotaz

		internal static Fask.SQL.Datasets.Vydej Get_Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			Fask.SQL.Datasets.Vydej ds = new Fask.SQL.Datasets.Vydej();

			try
			{
				Globals_V1.LoadConfiguration();
				var da = new System.Data.OleDb.OleDbDataAdapter();
				try
				{
					da.SelectCommand = new System.Data.OleDb.OleDbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					string select = GetScript_Vydej_Online_GetMaterial(itemnmbr, skl_id, serltnum);

					da.SelectCommand.CommandText = select;

					da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
					da.SelectCommand.Connection.Open();
					da.Fill(ds, ds.Items.TableName);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(ds);
					Logging.ExceptionHandler2.Handle(ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return ds;
		}
		private static string GetScript_Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			string Command = string.Empty;

			#region Puvodny
			//select
			//VC.ID as IndexXX,
			//VC.RefAg as Itemnmbr,
			//S.RefSklad as Skl_id,
			//NULL as Locncode,
			//VC.StavVC as Qty,
			//VC.VCislo as Serltnum,
			//VC.DatExp as Expiration,
			//S.VPrCZExpTrackIS as ExpTrack,
			//VC.DatSave as Prijem
			//from SKzVC as VC
			//left join SKz as S ON S.ID = VC.RefAg
			//WHERE 1 = 1
			//AND VC.StavVC > 0
			//AND VC.RefAg = '" + itemnmbr.Trim() + "'
			//AND VC.VCislo = '" + serltnum.Trim() + "'
			//AND S.RefSklad = '" + skl_id.Trim() + "'
			//ORDER BY COALESCE(iif(S.VPrCZExpTrackIS > 0, VC.DatExp, NULL), VC.DatSave)

			#endregion

			Command +=
					" SELECT " +
					" VC.ID as IndexXX, " +
					" VC.RefAg as Itemnmbr, " +
					" S.RefSklad as Skl_id, " +
					" NULL as Locncode, " +
					" VC.StavVC as Qty, " +
					" VC.VCislo as Serltnum, " +
					//" VC.VPrExspiraceKSN as Expiration, " +
					//" ISNULL(VC.VPrExspiraceKSN, convert(datetime, '2020-01-01')) as Expiration, " +
					//" ISNULL(VC.VPrExspiraceKSN, ISNULL(VC.DatExp, convert(datetime, '2020-01-01'))) as Expiration, " +
					" COALESCE(VC.VPrExspiraceKSN, VC.DatExp, convert(datetime, '2000-01-01')) as Expiration, " +
					" S.VPrCZExpTrackIS as ExpTrack, " +
					//" VC.DatSave as Prijem " +
					" COALESCE(VC.DatSave, convert(datetime, '2000-01-01')) as Prijem " + 
					" FROM SKzVC as VC " +
					" left join SKz as S ON S.ID = VC.RefAg " +
					" WHERE 1 = 1 ";

			Command += "AND VC.StavVC > 0";

			if (!string.IsNullOrEmpty(itemnmbr))
			{
				Command += " AND VC.RefAg = '" + itemnmbr.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(serltnum))
			{
				Command += " AND VC.VCislo = '" + serltnum.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(skl_id))
			{
				Command += " AND S.RefSklad = '" + skl_id.Trim() + "' ";
			}

			Command += " ORDER BY COALESCE(iif(S.VPrCZExpTrackIS > 0, VC.DatExp, NULL), VC.DatSave) ";

			return Command;
		}
	

		#endregion

		public static int? GetYearByPrelom()
		{
			int? YEAR = DateTime.Now.Year;

			DatabasePohoda.sPrelomDataTable dt_prelom = Database.Pohoda.sPrelom_GetData(Globals_V1.Konfigurace.PohodaInfo[0].Login_IDS_pohoda);

			if ((dt_prelom != null) && (dt_prelom.Count > 0))
			{
				if (dt_prelom[0].IsPrelom)
				{
					YEAR = dt_prelom[0].UsRok + 1;
				}
				else
				{
					YEAR = dt_prelom[0].UsRok;
				}

			}
			return YEAR;
		}

		public static string GetIDRadyByYearSText(Doklad typDoklad, int? YEAR)
		{
			string idsradadokladu = string.Empty;
			DatabasePohoda.sCRadyDataTable dt_crady = Database.Pohoda.sCRady_GetDataBy_RokDokladObsahtextu(
				YEAR,
				26, //prijem
				"%" + typDoklad.idsradatext + "%");

			if (dt_crady != null && dt_crady.Count > 0)
			{
				idsradadokladu = dt_crady[0].ID.ToString();
			}
			return idsradadokladu;
		}

		internal static Fask.POHODA.Disponibility.StatusInfo Prodej_Validace(DataSets.ProdejData data)
		{
            #region Validace Dat

            Globals_V1.LoadConfiguration();

			Fask.POHODA.Disponibility.ValidateData dsDisp = new POHODA.Disponibility.ValidateData();

			IOrderedEnumerable<DataSets.ProdejData.CZMST_DIRow> dtDIOrder = data.CZMST_DI.OrderBy(x => x.ITEMNMBR);

			foreach (DataSets.ProdejData.CZMST_DIRow item in dtDIOrder)
			{
				Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

				Row.ITEMNMBR = item.ITEMNMBR;
				Row.SKL_ID = item.SKL_ID;
				Row.QTY = item.QTYSHPPD;
				Row.SetSOPNUMBENull();
				Row.SetORDNull();
				Row.SetSKz_RezerNull();
				Row.SetSKz_StavZNull();
				Row.SetOBJ_RezerNull();

				dsDisp.DataDisp.AddDataDispRow(Row);
			}

			Fask.POHODA.Disponibility.CheckDisp disp = new Fask.POHODA.Disponibility.CheckDisp();
			return disp.KontrolaDisponibility(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			#endregion

		}
		
		internal static Fask.POHODA.Disponibility.StatusInfo Vydej_Validace(DataSets.Vydej vydejdata)
		{
            #region Validace Dat

            Globals_V1.LoadConfiguration();

			Fask.POHODA.Disponibility.ValidateData dsDisp = new POHODA.Disponibility.ValidateData();

			IOrderedEnumerable<DataSets.Vydej.CZMST_SIRow> dtDIOrder = vydejdata.CZMST_SI.OrderBy(x => x.ITEMNMBR);

			foreach (DataSets.Vydej.CZMST_SIRow item in dtDIOrder)
			{
				Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

				Row.ITEMNMBR = item.ITEMNMBR;
				Row.SKL_ID = item.SKL_ID;
				Row.QTY = item.QTYSHPPD;
				
				// 31.10.2019 TaD Chyba pri prebyrani SOPNUMBE a ORD 
				//Row.SetSOPNUMBENull();
				Row.SOPNUMBE = item.SOPNUMBE.Trim();

				//Row.SetORDNull();
				Row.ORD = item.ORD;

				Row.SetSKz_RezerNull();
				Row.SetSKz_StavZNull();
				Row.SetOBJ_RezerNull();

				dsDisp.DataDisp.AddDataDispRow(Row);
			}

			Fask.POHODA.Disponibility.CheckDisp disp = new Fask.POHODA.Disponibility.CheckDisp();
			return disp.KontrolaDisponibility(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
			#endregion

		}

		internal static void Get_MapingLokMechID(
			string ITEMNMBR_Zdroj, 
			string SKL_ID_Zdroj, 
			string SKL_ID_Cil,
			out string ITEMNMBR_Cil,
			out string ITEMDESC_Cil)
		{
			ITEMNMBR_Cil = string.Empty;
			ITEMDESC_Cil = string.Empty;

            try
            {

                Datasets.LokMech.MapingIDDataTable dt = new Datasets.LokMech.MapingIDDataTable();

				Globals_V1.LoadConfiguration();

				string script = " SELECT * FROM [dbo].[FASK_Get_POHODA_LokMechMapingID] ('" +
								ITEMNMBR_Zdroj +
								"','" +
								SKL_ID_Zdroj +
								"','" +
								SKL_ID_Cil +
								"')";

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = script;
						com.CommandType = System.Data.CommandType.Text;

						using (var ada = new System.Data.SqlClient.SqlDataAdapter())
						{
							ada.SelectCommand = com;
							ada.Fill(dt);

						}
					}
				}


				if (dt == null || dt.Count == 0)
					throw new Exception("Funkce na mapovaní nic nevrátila");

				if (dt.Count > 1)
					throw new Exception("Funkce na mapovaní vratila vic jak jeden zaznam.");

				ITEMDESC_Cil = dt.First().ITEMDESC;
				ITEMNMBR_Cil = dt.First().ITEMNMBR;

			}
            catch (System.Exception ex)
            {
				string msg = string.Format(
					"Mapovaci mechanizmus" + 
					Environment.NewLine +
					"ID Zdrojovej položky: '{0}'" + 
					Environment.NewLine + 
					"ID Zdrojoveho skladu:'{1}" + 
					Environment.NewLine + 
					"ID Ciloveho skladu: '{2}'", ITEMNMBR_Zdroj, SKL_ID_Zdroj, SKL_ID_Cil);

                Fask.Logging.ExceptionHandler2.Handle(ex);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, msg);
            }

		}
	}

    public class InfoValidace 
    {

        public InfoValidace(string Status, DataSets.Vydej.VydekKontrola1Row Radek) 
        {
            this.Status = Status;
            this.Radek = Radek;
        }

        public string Status;
        public DataSets.Vydej.VydekKontrola1Row Radek;


    }
}
