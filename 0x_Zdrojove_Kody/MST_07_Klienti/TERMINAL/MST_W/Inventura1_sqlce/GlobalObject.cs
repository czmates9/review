using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.Inventura1_sqlce
{
    public class GlobalObject : IDisposable
    {
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1 controller_inventura1;

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
        
        public _WebRefernces_Globals.Inventura1ServiceSession inventuraclassService;

        public GlobalObject()
        {
			this.controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
			try
			{

				inventuraclassService = new _WebRefernces_Globals.Inventura1ServiceSession();
				inventuraclassService.Url = MST_Global.ServerAddress + "Inventura1.asmx";
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
            if (controller_inventura1 != null) controller_inventura1.Dispose();
            if (controller_sklady != null) controller_sklady.Dispose();

            controller_inventura1 = null;
            controller_sklady = null;

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
                if (this.controller_inventura1 != null)
                    this.controller_inventura1.Dispose();
                this.controller_inventura1 = null;
                
                this.davka = value;

                if ((davka ?? 0) > 0)
                {
                    try
                    {
						this.controller_inventura1 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Inventura1(Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Inventura1)));
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
