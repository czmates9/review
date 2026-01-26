using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///using Fask.MST_W.ServerAccess;
using System.IO;
using FASK.MST_WINDOWS.Main.ServerAccess;
using FASK.MST_WINDOWS.Main.Configuration;

namespace FASK.MST_WINDOWS.Main
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
                        Fask.Logging.ExceptionHandler2.Handle(exDavka);
                    }
                }
            }
        }

        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej controller_prodej = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi controller_zbozi = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska controller_strediska = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici controller_pracovnici = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu controller_typdokladu = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Users controller_users = null;

        public _WebRefernces_Globals.ProdejServiceSession servis_prodej = null;
        public _WebRefernces_Globals.CiselnikServiceSession servis_ciselnik = null;


        public GlobalObject()
        {
            servis_prodej = new _WebRefernces_Globals.ProdejServiceSession();
            servis_prodej.Url = Config.Main_KomServer + "Prodej.asmx";
            servis_prodej.Timeout = MST_Global.ServiceTimeOut;
            servis_prodej.UpdateWebServiceCredentials();

            servis_ciselnik = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
            servis_ciselnik.Url = Config.Main_KomServer + "Ciselnik.asmx";
            servis_ciselnik.Timeout = MST_Global.ServiceTimeOut;
            servis_ciselnik.UpdateWebServiceCredentials();

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
            if (controller_sklady != null) controller_sklady.Dispose();
            if (controller_zbozi != null) controller_zbozi.Dispose();
            if (controller_strediska != null) controller_strediska.Dispose();
            if (controller_pracovnici != null) controller_pracovnici.Dispose();
            if (controller_typdokladu != null) controller_typdokladu.Dispose();
            if (controller_odberatele != null) controller_odberatele.Dispose();
            if (controller_users != null) controller_users.Dispose();

            controller_prodej = null;
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

