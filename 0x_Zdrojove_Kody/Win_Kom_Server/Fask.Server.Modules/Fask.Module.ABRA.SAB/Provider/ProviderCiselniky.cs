using Fask.DataSets;
using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;


namespace Fask.Module.ABRA.SAB.Provider
{
    /// <summary>
    /// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část čísleník.
    /// </summary>
    public partial class Provider : Fask.Server.Interfaces.Ciselniky.ICiselniky
    {

		#region IStrediska Members

		public Fask.Interfaces.DataSets.Strediska KatalogStrediska(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

			return new Fask.Interfaces.DataSets.Strediska();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogStrediskaExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region IZbozi Members Implementovano

		/// <summary>
		/// Metoda pro export Zásob z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		public StatusInfo KatalogZboziExport(Terminal terminal, Sklad sklad, ref StatusObject so)
		{
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (Classes.TransformerCiselniky.ExportKatalogZasobyABRAFirebird(ref so) != "OK")
				{
					statusInfo.Description = "Chyba";
					return statusInfo;
				}

				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Zboží pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Zbozi naplnen datama</returns>
		Fask.Interfaces.DataSets.Zbozi Fask.Server.Interfaces.Ciselniky.IZbozi.KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			Fask.Interfaces.DataSets.Zbozi zbozi = new Fask.Interfaces.DataSets.Zbozi();

			try
			{
				
				Database.Zbozi.Fill_Zasoby(zbozi);
				zbozi.FASK_ZASOBY.ToList().ForEach(x => x.SetAdded()); // naco?

				return zbozi;
			}
			catch (Exception ex)
			{
				so.Exception = true;
				so.Write(ex.Message);
				throw ex;
			}
		}

		#endregion

		#region ITypDokladu Members Implementovano

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Typy Dokladu pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset TypDokladu naplnen datama </returns>
		public Fask.DataSets.TypDokladu KatalogTypDokladu(Terminal terminal, Sklad sklad)
		{
			Fask.DataSets.TypDokladu td = new TypDokladu();

			try
			{
				Database.Ciselniky.Fill_TypyDokladu(td, sklad);
				//td.CZMST092.ToList().ForEach(x => x.SetAdded()); // SetAdded se provadi až na urovni zapisu do SQLite Datasetu

				return td;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(td);
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#endregion

		#region IOdberatele Members

		public Fask.Interfaces.DataSets.Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

			return new Fask.Interfaces.DataSets.Odberatele();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogOdberateleExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region IPracovnici Members

		public Fask.DataSets.Pracovnici KatalogPracovnici(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

			return new Fask.DataSets.Pracovnici();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogPracovniciExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region ISklady Members Implementovano

		public Fask.Interfaces.DataSets.Sklady KatalogSklady(Fask.Server.Interfaces.Classes.Terminal terminal)
		{
			Fask.Interfaces.DataSets.Sklady sklady = new Fask.Interfaces.DataSets.Sklady();

			try
			{
				Database.Ciselniky.Fill_Sklady(sklady);
				sklady.CZMST093.ToList().ForEach(x => x.SetAdded()); // naco?

				return sklady;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(sklady);
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogSkladyExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (Classes.TransformerCiselniky.ExportKatalogSkladyABRAFirebird(ref so) != "OK")
				{
					statusInfo.Description = "Chyba";
					return statusInfo;
				}

				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region ILokace Members

		public Fask.DataSets.Lokace KatalogLokace(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			Globals_V1.LoadConfiguration();
			System.Data.SqlClient.SqlConnection conn = null;

			conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			string select = "SELECT * FROM " + Constants.Common.TABLE_CZMST094;
			// naplneni czmst094
			SqlDataAdapter xda = new SqlDataAdapter(select, conn);

			Fask.DataSets.Lokace lokace = new Fask.DataSets.Lokace();
			xda.Fill(lokace, lokace.CZMST094.TableName);

			foreach (DataSets.Lokace.CZMST094Row srow in lokace.CZMST094)
			{
				srow.SetAdded();
			}

			// naplneni CZMST_SkladLokace_LokaceTypy
			select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceTypy;

			xda = new SqlDataAdapter(select, conn);

			xda.Fill(lokace, lokace.CZMST_SkladLokace_LokaceTypy.TableName);

			foreach (DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow srow in lokace.CZMST_SkladLokace_LokaceTypy)
			{
				srow.SetAdded();
			}

			return lokace;
		}


		public StatusInfo KatalogLokaceExport(Terminal terminal, Sklad sklad, ref StatusObject so)
		{
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				string status = string.Empty;
				if ((status = Classes.TransformerCiselniky.ExportKatalogLokaceABRAFirebird(sklad, ref so)) == "OK")
				{
					statusInfo.Description = "OK";
					statusInfo.ID = 0;
					return statusInfo;
				}
				else if(status== "ABRA_CHYBA")
                {
					statusInfo.Description = "IS ABRA chyba při transakci.";
					statusInfo.ID = -1;
					return statusInfo;
				}
				else if(status == "ABRA_NO_DATA")
                {
					statusInfo.Description = "Žádná data pro export";
					statusInfo.ID = 2;
					return statusInfo;
				}
                else
                {
					statusInfo.Description = "Chyba, viz logg soubor.";
					statusInfo.ID = -2;
					return statusInfo;
				}

				
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region IMeny Members

		public Fask.DataSets.Meny KatalogMen(Fask.Server.Interfaces.Classes.Terminal terminal)
		{

			return new Fask.DataSets.Meny();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogMenExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public StatusInfo KatalogTypDokladuExport(Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
