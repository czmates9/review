using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Rady
{
	/// <summary>
	/// Enum sloužící pro rozpoznani typu DB
	/// </summary>
	public enum TypDB 
	{
		SQL
	}


	//  *-PrP - Příjem s předlouhou
	//  *-VyP - Vydej s předlohou
	//  *-Pro - Prodej (Volný pohyb)
	//  *-SSCC - Pro generovani SSCC kodu sjednoceni řad do jedne tabulky z (CZMST_SSCC_SEQUENCE) a pravdepodobne by vazebni tabulka (CZMST_SSCC_PARAMETERS) byla ponechana
	//  *-Vyr - Výroba, odvadeni do Agendy Vyroba
	public enum Modul 
	{
		PrP,
		VyP,
		Pro,
		SSCC,
		Vyr
	}


	public class NumericalSeries
	{

		#region Parametry

		/// <summary>
		/// Connection String na konkretnu DB
		/// </summary>
		//private string _connectionString;
	
		#endregion

		#region C'Tor  pro class

		/// <summary>
		/// Kontruktor s predavanym parametrem Connection String
		/// </summary>
		/// <param name="ConnectionString">Connection String na DB</param>
		public NumericalSeries(string ConnectionString)
		{
			//this._connectionString = ConnectionString;
			DB.ConnectionString = ConnectionString;
		}

		
		#endregion



		public int? GetCiselnaRada_ID(TypDB TypDB, Modul Mod, string SKL_ID, string UserID, bool KontrolaPlatnosti)
		{
			return GetCiselnaRada_ID(TypDB, Mod,  SKL_ID, UserID,KontrolaPlatnosti, null,null,null);
		}




		/// <summary>
		/// Metoda ktera podle vtupních parametru vratí prvek Rada_ID
		/// </summary>
		/// <param name="TypDB"> Je typu Enum takže je lepší switch a jde naparsovat s Stringu</param>
		/// <param name="Modul"> o jaky modul se jedná</param>
		/// <returns>ID řady</returns>
		public int? GetCiselnaRada_ID(TypDB TypDB, Modul Mod, string SKL_ID, string UserID, bool KontrolaPlatnosti, string Modul_ID, string Modul_ID2, string Modul_Funkce)
		{
			//1 - Dotahnout z Tabulky řady pro konkterny modul podle Modul
			//1.1-Skontrolovat Platnost řad a neplatne vyhodit
			//2-Rozdelit logiku podle Modulu

			int? Rada_ID = null;
			DS_Rady.FASK_RADYDataTable dt = new DS_Rady.FASK_RADYDataTable();

			try
			{

				switch (TypDB)
				{
					case TypDB.SQL:
					default:
						DB.SQL_FASK_RADY_Fill_CustomSQLQuery(GetQuerry_SQL(Mod, SKL_ID, UserID, KontrolaPlatnosti, Modul_ID, Modul_ID2, Modul_Funkce), dt);
						break;
				}

				if((dt!= null) &&(dt.Count > 0))
				{
					if (dt.Count > 1)
					{
						var dt_sel = dt.Where(x => x.Default = true);

						if ((dt_sel.Count() > 1) || (dt_sel.Count() == 0))
						{
							Rada_ID = dt.First().Rada_ID;
						}
						else 
						{
							Rada_ID = dt_sel.First().Rada_ID;
						}
					}
					else 
					{
						Rada_ID = dt.First().Rada_ID;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			return Rada_ID;
		}


		public string GetFunkceRada(TypDB TypDB, Modul Mod, string SKL_ID, string UserID, bool KontrolaPlatnosti, string Modul_ID, string Modul_ID2)
		{
			string Funkce = null;
			DS_Rady.FASK_RADYDataTable dt = new DS_Rady.FASK_RADYDataTable();

			try
			{

				switch (TypDB)
				{
					case TypDB.SQL:
					default:
						DB.SQL_FASK_RADY_Fill_CustomSQLQuery(GetQuerry_SQL(Mod, SKL_ID, UserID, KontrolaPlatnosti, Modul_ID, Modul_ID2, null), dt);
						break;
				}

				if ((dt != null) && (dt.Count > 0))
				{
					if (dt.Count > 1)
					{
						//Nemnelo by nastat, k Modul_ID by mnel byt iba 1 radek, podle skladu
						Funkce = dt.First().Modul_Funkce;
						//var dt_sel = dt.Where(x => x.Default = true);

						//if ((dt_sel.Count() > 1) || (dt_sel.Count() == 0))
						//{
						//    Funkce = dt.First().Modul_Funkce;
						//}
						//else
						//{
						//    Funkce = dt_sel.First().Modul_Funkce;
						//}
						
					}
					else
					{
						Funkce = dt.First().Modul_Funkce;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}

			return Funkce;
		}

		public DS_Rady.FASK_RADYRow GetRadaRow(TypDB TypDB, Modul Mod, string SKL_ID, string UserID, bool KontrolaPlatnosti, string Modul_ID, string Modul_ID2)
		{
			//string Funkce = null;
			DS_Rady.FASK_RADYDataTable dt = new DS_Rady.FASK_RADYDataTable();

			try
			{

				switch (TypDB)
				{
					case TypDB.SQL:
					default:
						DB.SQL_FASK_RADY_Fill_CustomSQLQuery(GetQuerry_SQL(Mod, SKL_ID, UserID, KontrolaPlatnosti, Modul_ID, Modul_ID2, null), dt);
						break;
				}

				if ((dt != null) && (dt.Count > 0))
				{
					return dt.First();
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return null;
			}

			//return null;
			
		}


		private string GetQuerry_SQL(Modul M, string SKL_ID, string UserID, bool KontrolaPlatnosti, string Modul_ID, string Modul_ID2, string Modul_Funkce)
		{

			string DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

			string SQL = "Select * from FASK_RADY ";
			SQL += "Where 1=1 ";
			SQL += "AND Modul=" + "'" + M.ToString() + "' ";

			if (!string.IsNullOrEmpty(Modul_Funkce))
			{
				SQL += "AND Modul_Funkce=" + "'" + Modul_Funkce.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(Modul_ID))
			{
				SQL += "AND Modul_ID=" + "'" + Modul_ID.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(Modul_ID2))
			{
				SQL += "AND Modul_ID2=" + "'" + Modul_ID2.Trim() + "' ";
			}

			#region Platnost

			if (KontrolaPlatnosti)
			{
				SQL += "AND ( (PlatnostOd < '" + DateTimeNow + "') AND (PlatnostDo > '" + DateTimeNow + "')) ";
			} 

			#endregion

			#region Filtre

			if (!string.IsNullOrEmpty(SKL_ID))
			{
				SQL += "AND Filtr_SkladID=" + "'" + SKL_ID.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(UserID))
			{
				SQL += "AND Filtr_UserID=" + "'" + UserID.Trim() + "' ";
			} 

			#endregion

			return SQL;
		}
	
	}
}
