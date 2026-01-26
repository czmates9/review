using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.Prijem_4
{
    /// <summary>
    /// globalni objekt pro pristup k databazovym adapterum, webovym sluzbam, objektum, ktere jsou poplatne konkretni davce
    /// </summary>
    public class GlobalObject : IDisposable
    {
        // Komunikacni objekty
        internal Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem controller_prijem = null;
		internal Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);

        // webove sluzby
        internal _WebRefernces_Globals.PrijemServiceSession service_prijem = null;
        internal _WebRefernces_Globals.ServisModuleWServiceSession service_serviceModule = null;
        internal _WebRefernces_Globals.HmotnostServiceSession service_hmotnost = null;
        internal _WebRefernces_Globals.LokaceServiceSession servis_lokace = null;

        public GlobalObject()
        {
            this.service_prijem = new Fask.MST_W._WebRefernces_Globals.PrijemServiceSession();
            this.service_prijem.Url = MST_Global.ServerAddress + "Prijem.asmx";
            this.service_prijem.Timeout = MST_Global.ServiceTimeOut;
            this.service_prijem.UpdateWebServiceCredentials();

            this.service_serviceModule = new _WebRefernces_Globals.ServisModuleWServiceSession();
            this.service_serviceModule.Url = MST_Global.ServerAddress + "Servis.asmx";
            this.service_serviceModule.Timeout = MST_Global.ServiceTimeOut;
            this.service_serviceModule.UpdateWebServiceCredentials();

            this.service_hmotnost = new _WebRefernces_Globals.HmotnostServiceSession();
            this.service_hmotnost.Url = MST_Global.ServerAddress + "Hmotnost.asmx";
            this.service_hmotnost.Timeout = MST_Global.ServiceTimeOut;
            this.service_hmotnost.UpdateWebServiceCredentials();

            this.servis_lokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
            this.servis_lokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
            this.servis_lokace.Timeout = 20000;
            this.servis_lokace.UpdateWebServiceCredentials();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (controller_prijem != null) controller_prijem.Dispose();
            if (controller_sklady != null) controller_sklady.Dispose();

            controller_prijem = null;
            controller_sklady = null;

            if (this.service_prijem != null) this.service_prijem.Dispose();
            if (this.service_serviceModule != null) this.service_serviceModule.Dispose();
            if (this.service_hmotnost != null) this.service_hmotnost.Dispose();
            if (this.servis_lokace != null) this.servis_lokace.Dispose();

            this.service_prijem = null;
            this.service_serviceModule = null;
            this.service_hmotnost = null;
            this.servis_lokace = null;
        }

        #endregion

        #region Promenne pro davku prijmu
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
                    if (this.controller_prijem != null)
                        this.controller_prijem.Dispose();
                    this.controller_prijem = null;
                }

                this.davka = value;

                try
                {
                    if (this.davka != null)
						this.controller_prijem = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prijem(Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Prijem)));
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
                return Path.Combine(Main.StorageDir, String.Format("{0}.{1}", this.Davka, Main.Ext_Prijem));
            }
        }

        /// <summary>
        /// Aktualni zvoleny sklad pro prijem
        /// </summary>
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null;
        #endregion
    }
}
