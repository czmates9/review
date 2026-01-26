using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.Inventura2
{
	public class GlobalObject : IDisposable
	{

		public Fask.SQLiteDBs.DataSets.Inventura2.KANCLRow active_kancelar = null;
		public Fask.SQLiteDBs.DataSets.Inventura2.LOKACERow active_lokace = null;
		public Fask.SQLiteDBs.DataSets.Inventura2.OSOBYRow active_osoba = null;
		public Fask.SQLiteDBs.DataSets.Inventura2.UCSTRRow active_stredisko = null;
		public Fask.SQLiteDBs.DataSets.Inventura2.ParametryRow active_parametry = null;


		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2 controller_inventura2;

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska controller_strediska = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska(Main.CiselnikStrediskaDB);


		public _WebRefernces_Globals.Inventura2ServiceSession inventuraclassService;

		public GlobalObject()
		{
			this.controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
			try
			{
				inventuraclassService = new _WebRefernces_Globals.Inventura2ServiceSession();
				inventuraclassService.Url = MST_Global.ServerAddress + "Inventura2.asmx";
				inventuraclassService.Timeout = MST_Global.ServiceTimeOut;
				inventuraclassService.UpdateWebServiceCredentials();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		public void Dispose()
		{
			// zruseni controlleru
			if (controller_inventura2 != null) controller_inventura2.Dispose();
			if (controller_sklady != null) controller_sklady.Dispose();
			if (controller_strediska != null) controller_strediska.Dispose();

			controller_inventura2 = null;
			controller_sklady = null;
			controller_strediska = null;

			// zruseni webservices
			if (inventuraclassService != null) inventuraclassService.Dispose();

			inventuraclassService = null;
		}


		private int? davka = null;
		/// <summary>
		/// Aktualni cislo davky.
		/// </summary>
		/// <remarks>Pokud je mensi 0 nebo null, tak davka neni zvolena a controler_prijem neni nastaven</remarks>
		public int? Davka
		{
			get
			{
				return davka;
			}
			set
			{
				if (this.controller_inventura2 != null)
					this.controller_inventura2.Dispose();
				this.controller_inventura2 = null;

				this.davka = value;

				if ((davka ?? 0) > 0)
				{
					try
					{
						this.controller_inventura2 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura2(Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Inventura2)));
					}
					catch (Exception exDavka)
					{
						Logging.Log.Write(exDavka);
					}
				}
			}
		}
	}
}
