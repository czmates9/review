using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.Prodej_3
{
    public class GlobalObject : IDisposable
    {
        private string cislodavkydbname = string.Empty;
        private int? davka = null;
        public int? Davka
        {
			get
			{
				return davka;
			}
			set
			{
				if (this.controller_prodej != null)
					this.controller_prodej.Dispose();
				this.controller_prodej = null;

				this.davka = value;

				if ((davka ?? 0) > 0)
				{
					try
					{

						this.controller_prodej = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(Path.Combine(Main.StorageDir, String.Format("{0}.{1}", davka.Value.ToString(), Main.Ext_Prodej)));
					}
					catch (Exception exDavka)
					{
						Logging.Log.Write(exDavka);
					}
				}
			}
        }

        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej controller_prodej = null;

        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Meny controller_meny = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi controller_zbozi = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska controller_strediska = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici controller_pracovnici = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu controller_typdokladu = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele = null;

        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Users controller_users = null;

        public _WebRefernces_Globals.ProdejServiceSession servis_prodej = null;
        public _WebRefernces_Globals.InformationServiceSession servis_information = null;
        public _WebRefernces_Globals.CiselnikServiceSession servis_ciselnik = null;
        public _WebRefernces_Globals.ConfigurationServiceSession servis_configuration = null;
        public _WebRefernces_Globals.LokaceServiceSession servis_lokace = null;
        public _WebRefernces_Globals.ExpediceSeviceSession servis_expedice = null;

        public GlobalObject()
        {
            servis_prodej = new Fask.MST_W._WebRefernces_Globals.ProdejServiceSession();
            servis_prodej.Url = MST_Global.ServerAddress + "Prodej.asmx";
            servis_prodej.Timeout = MST_Global.ServiceTimeOut;
            servis_prodej.UpdateWebServiceCredentials();

            servis_information = new _WebRefernces_Globals.InformationServiceSession();
            servis_information.Url = MST_Global.ServerAddress + "Informations.asmx";
            servis_information.Timeout = MST_Global.ServiceTimeOut;
            servis_information.UpdateWebServiceCredentials();

            servis_ciselnik = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
            servis_ciselnik.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
            servis_ciselnik.Timeout = MST_Global.ServiceTimeOut;
            servis_ciselnik.UpdateWebServiceCredentials();

            servis_configuration = new _WebRefernces_Globals.ConfigurationServiceSession();
            servis_configuration.Url = MST_Global.ServerAddress + "Configuration.asmx";
            servis_configuration.Timeout = MST_Global.ServiceTimeOut;
            servis_configuration.UpdateWebServiceCredentials();

            servis_lokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
            servis_lokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
            servis_lokace.Timeout = MST_Global.ServiceTimeOut;
            servis_lokace.UpdateWebServiceCredentials();

            servis_expedice = new _WebRefernces_Globals.ExpediceSeviceSession();
            servis_expedice.Url = MST_Global.ServerAddress + "Expedice.asmx";
            servis_expedice.Timeout = MST_Global.ServiceTimeOut;
            servis_expedice.UpdateWebServiceCredentials();

            controller_meny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Meny(Main.CiselnikMenDB);
            controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
            controller_zbozi = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB);
            controller_strediska = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska(Main.CiselnikStrediskaDB);
            controller_pracovnici = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici(Main.CiselnikPracovniciDB);
            controller_typdokladu = new Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu(Main.CiselnikTypDokladuDB);
            controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Main.CiselnikOdberateleDB);
            controller_users = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Main.CiselnikUzivateleDB);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (controller_prodej != null) controller_prodej.Dispose();
            if (controller_meny != null) controller_meny.Dispose();
            if (controller_sklady != null) controller_sklady.Dispose();
            if (controller_zbozi != null) controller_zbozi.Dispose();
            if (controller_strediska != null) controller_strediska.Dispose();
            if (controller_pracovnici != null) controller_pracovnici.Dispose();
            if (controller_typdokladu != null) controller_typdokladu.Dispose();
            if (controller_odberatele != null) controller_odberatele.Dispose();
            if (controller_users != null) controller_users.Dispose();

            controller_prodej = null;
            controller_meny = null;
            controller_sklady = null;
            controller_zbozi = null;
            controller_strediska = null;
            controller_pracovnici = null;
            controller_typdokladu = null;
            controller_odberatele = null;
            controller_users = null;
        }

        #endregion
    }
}
