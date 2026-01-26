using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;

namespace Fask.Module.ABRA.CarpServise
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část čísleník.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Ciselniky.ICiselniky
    {
        //private string TABLE_CZMST090 = "CZMST090";
        //private string TABLE_CZMST091 = "CZMST091";
        //private string TABLE_CZMST092 = "CZMST092";
        private string TABLE_CZMST093 = "CZMST093";
        //private string TABLE_CZMST094 = "CZMST094";
		private string TABLE_FASK_ZASOBY = "FASK_ZASOBY";
        //private string TABLE_CZMST096 = "CZMST096";
        //private string TABLE_CZMST_DI = "CZMST_DI";
        //private string TABLE_CZMST_EVENTSTYPES = "CZMST_EventsTypes";
        //private string TABLE_CZMST_TISKARNA = "CZMST_TISKARNA";



		#region IStrediska Members

		public Fask.DataSets.Strediska KatalogStrediska(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			return new Fask.DataSets.Strediska();
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

		#region IZbozi Members

		public Fask.DataSets.Zbozi KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			return new Fask.DataSets.Zbozi();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogZboziExport(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
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

		#region ITypDokladu Members

		public Fask.DataSets.TypDokladu KatalogTypDokladu(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			return new Fask.DataSets.TypDokladu();
		}

		#endregion

		#region IOdberatele Members

		public Fask.DataSets.Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			return new Fask.DataSets.Odberatele();
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

		#region ISklady Members

		public Fask.DataSets.Sklady KatalogSklady(Fask.Server.Interfaces.Classes.Terminal terminal)
		{
			System.Data.SqlClient.SqlConnection conn = null;

			conn = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection);
			string select = "SELECT * FROM " + TABLE_CZMST093;

			SqlDataAdapter xda = new SqlDataAdapter(select, conn);

			Fask.DataSets.Sklady sklady = new Fask.DataSets.Sklady();
			xda.Fill(sklady, sklady.CZMST093.TableName);

			foreach (DataSets.Sklady.CZMST093Row srow in sklady.CZMST093)
			{
				srow.SetAdded();
			}

			return sklady;
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogSkladyExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (ExportKatalogSkladyABRAFirebird(ref so) != "OK")
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
			return new Fask.DataSets.Lokace();
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

		#endregion

		#region Private metody


		/// <summary>
		/// Metoda pro export Skladu
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>OK anebo chyba</returns>
		public string ExportKatalogSkladyABRAFirebird(ref StatusObject so)
		{
			try
			{
				//string pom = string.Empty;

				//so.Write("export zahajen");
				////\TODO: vybirat jen nektere sloupce
				////Datasets.DatabasePohoda.sSkladDataTable ssklad_dt = ssklad_ta.GetData();
				SQL_Datasets.Ciselniky.CZMST093DataTable ssklad_dt = Database.ABRA.Sklad_GetData();


				SQL_Datasets.CiselnikyTableAdapters.CZMST093TableAdapter CZMST_093TableAdapter = new Fask.Module.ABRA.CarpServise.SQL_Datasets.CiselnikyTableAdapters.CZMST093TableAdapter();
				CZMST_093TableAdapter.Connection.ConnectionString = Properties.Settings.Default.SqlProviderConnection;

				CZMST_093TableAdapter.DeleteQuery();

				SQL_Datasets.Ciselniky sklady = new SQL_Datasets.Ciselniky();

				string skl_id;
				string skl_desc;
				string skl_typ;
				string skl_carcode;


				int skl_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_id"].MaxLength;
				int skl_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_desc"].MaxLength;
				int skl_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_typ"].MaxLength;
				int skl_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_carcode"].MaxLength;


				so.Write("export polozek z pohody do databaze");

				foreach (var item in ssklad_dt)
				{
					skl_id = item.skl_id;
					skl_desc = item.Isskl_descNull() ? "" : item.skl_desc.Trim();
					skl_typ = item.Isskl_typNull() ? "" : item.skl_typ.Trim();
					skl_carcode = item.Isskl_carcodeNull() ? "" : item.skl_carcode.Trim();

					sklady.CZMST093.AddCZMST093Row(skl_id, skl_desc, skl_typ, skl_carcode);

				}

				CZMST_093TableAdapter.Update(sklady.CZMST093);

				so.Write("export se provedl uspesne");
			}
			catch (Exception ex)
			{
				return ex.Message;

			}
			return "OK";
		}

		
		#endregion
	}
}
