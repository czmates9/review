using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MES_Android.Prodej
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

                        string FileName = Path.Combine(DataInfo_Static.PathDir, davka.Value.ToString() + DataInfo_Static.PriponaDI);
                        this.controller_prodej = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(FileName);
                    }
                    catch (Exception exDavka)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(exDavka);
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

        public _WebReferences_Globals.ProdejServiceSession servis_prodej = null;
        public _WebReferences_Globals.InformationServiceSession servis_information = null;
        public _WebReferences_Globals.CiselnikServiceSession servis_ciselnik = null;
        public _WebReferences_Globals.ConfigurationServiceSession servis_configuration = null;
        public _WebReferences_Globals.LokaceServiceSession servis_lokace = null;
        public _WebReferences_Globals.ExpediceSeviceSession servis_expedice = null;
        public _WebReferences_Globals.PrijemServiceSession servis_Prijem = null;

        public GlobalObject()
        {

            servis_prodej = new _WebReferences_Globals.ProdejServiceSession();
            servis_prodej.Url = Config.Settings.Adresa + "Prodej.asmx";
            servis_prodej.Timeout = Config.Settings.TimeOut;
            servis_prodej.UpdateWebServiceCredentials();

            servis_information = new _WebReferences_Globals.InformationServiceSession();
            servis_information.Url = Config.Settings.Adresa + "Informations.asmx";
            servis_information.Timeout = Config.Settings.TimeOut;
            servis_information.UpdateWebServiceCredentials();

            servis_ciselnik = new _WebReferences_Globals.CiselnikServiceSession();
            servis_ciselnik.Url = Config.Settings.Adresa + "Ciselnik.asmx";
            servis_ciselnik.Timeout = Config.Settings.TimeOut;
            servis_ciselnik.UpdateWebServiceCredentials();

            servis_configuration = new _WebReferences_Globals.ConfigurationServiceSession();
            servis_configuration.Url = Config.Settings.Adresa + "Configuration.asmx";
            servis_configuration.Timeout = Config.Settings.TimeOut;
            servis_configuration.UpdateWebServiceCredentials();

            servis_lokace = new _WebReferences_Globals.LokaceServiceSession();
            servis_lokace.Url = Config.Settings.Adresa + "Lokace.asmx";
            servis_lokace.Timeout = Config.Settings.TimeOut;
            servis_lokace.UpdateWebServiceCredentials();

            servis_expedice = new _WebReferences_Globals.ExpediceSeviceSession();
            servis_expedice.Url = Config.Settings.Adresa + "Expedice.asmx";
            servis_expedice.Timeout = Config.Settings.TimeOut;
            servis_expedice.UpdateWebServiceCredentials();

            servis_Prijem = new _WebReferences_Globals.PrijemServiceSession();
            servis_Prijem.Url = Config.Settings.Adresa + "Prijem.asmx";
            servis_Prijem.Timeout = Config.Settings.TimeOut;
            servis_Prijem.UpdateWebServiceCredentials();

            controller_meny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Meny(DataInfo_Static.CiselnikMenyDB);
            controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(DataInfo_Static.CiselnikSkladyDB);
            controller_zbozi = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(DataInfo_Static.CiselnikZboziDB);
            controller_strediska = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Strediska(DataInfo_Static.CiselnikStrediskaDB);
            controller_pracovnici = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici(DataInfo_Static.CiselnikPracovniciDB);
            controller_typdokladu = new Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu(DataInfo_Static.CiselnikTypDokladuDB);
            controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(DataInfo_Static.CiselnikOdberateleDB);
            controller_users = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(DataInfo_Static.CiselnikUzivateleDB);
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