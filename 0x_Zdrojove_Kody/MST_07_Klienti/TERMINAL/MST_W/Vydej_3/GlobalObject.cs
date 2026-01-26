using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Vydej_3
{
    public class GlobalObject : IDisposable
    {
        // Komunikacni objekty
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej controller_vydej;

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace controller_lokace;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi controller_zbozi;

        // webove sluzby
        public _WebRefernces_Globals.VydejServiceSession service_vydej;
        public _WebRefernces_Globals.InformationServiceSession service_information;
        public _WebRefernces_Globals.CiselnikServiceSession service_ciselnik;
        public _WebRefernces_Globals.LokaceServiceSession service_lokace;
        public _WebRefernces_Globals.RFIDSession service_rfid;


        public GlobalObject()
        {
			this.controller_lokace = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace(Main.CiselnikLokaceDB);
			this.controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Main.CiselnikOdberateleDB);
			this.controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
			this.controller_zbozi = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Main.CiselnikZboziDB);

            try
            {
                this.service_vydej = new _WebRefernces_Globals.VydejServiceSession();
                this.service_vydej.Url = MST_Global.ServerAddress + "Vydej.asmx";
                this.service_vydej.Timeout = MST_Global.ServiceTimeOut;
                this.service_vydej.UpdateWebServiceCredentials();

                this.service_information = new _WebRefernces_Globals.InformationServiceSession();
                this.service_information.Url = MST_Global.ServerAddress + "Informations.asmx";
                this.service_information.Timeout = MST_Global.ServiceTimeOut;
                this.service_information.UpdateWebServiceCredentials();

                this.service_ciselnik = new _WebRefernces_Globals.CiselnikServiceSession();
                this.service_ciselnik.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                this.service_ciselnik.Timeout = MST_Global.ServiceTimeOut;
                this.service_ciselnik.UpdateWebServiceCredentials();

                this.service_lokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
                this.service_lokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
                this.service_lokace.Timeout = 20000;
                this.service_lokace.UpdateWebServiceCredentials();

                this.service_rfid = new Fask.MST_W._WebRefernces_Globals.RFIDSession();
                this.service_rfid.Url = MST_Global.ServerAddress + "RFID.asmx";
                this.service_rfid.Timeout = MST_Global.ServiceTimeOut;
                this.service_rfid.UpdateWebServiceCredentials();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (controller_vydej != null) controller_vydej.Dispose();
            if (controller_lokace != null) controller_lokace.Dispose();
            if (controller_odberatele != null) controller_odberatele.Dispose();
            if (controller_sklady != null) controller_sklady.Dispose();
            if (controller_zbozi != null) controller_zbozi.Dispose();

            controller_vydej = null;
            controller_lokace = null;
            controller_odberatele = null;
            controller_sklady = null;
            controller_zbozi = null;

            if (service_ciselnik != null) service_ciselnik.Dispose();
            if (service_information != null) service_information.Dispose();
            if (service_lokace != null) service_lokace.Dispose();
            if (service_vydej != null) service_vydej.Dispose();
            if (service_rfid != null) service_rfid.Dispose();

            service_ciselnik = null;
            service_information = null;
            service_lokace = null;
            service_vydej = null;
            service_rfid = null;
        }

        #endregion

        private string davka = null;
        /// <summary>
        /// Aktualni cislo davky.
        /// </summary>
        /// <remarks>Davka muze byt i text (Sloucena => 'S...'. Pokud je null nebo empty, tak neni nastaveno a controler_prijem je null!</remarks>
        public string Davka
        {
            get
            {
                return davka;
            }
            set
            {
                if (this.davka != value)
                {
                    if (this.controller_vydej != null)
                        this.controller_vydej.Dispose();
                    this.controller_vydej = null;
                }

                this.davka = value;

                try
                {
                    if (this.davka != null)
						this.controller_vydej = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Vydej)));
                }
                catch (Exception exDavka)
                {
                    Logging.Log.Write(exDavka);
                }
            }
        }

        /// <summary>
        /// Plna cesta k soubru davky dle aktualniho StorageDirectory
        /// </summary>
        public string DavkaFileNameFullPath
        {
            get
            {
                return Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Vydej));
            }
        }
        /// <summary>
        /// Aktualni vybrany sklad
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        /// <summary>
        /// data vydeje
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Vydej vydejData = new Fask.SQLiteDBs.DataSets.Vydej();
    }
}
