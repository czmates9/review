
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FASK.SledovaniVyroby.Module.ZZS.Constants;

namespace FASK.SledovaniVyroby.Module.ZZS
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

                        this.controller_prodej = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(Path.Combine(Common.StorageDir, String.Format(@"SQLiteDBs\{0}.{1}", davka.Value.ToString(), Common.Ext_Prodej)));
                    }
                    catch (Exception exDavka)
                    {
                       // Logging.Log.Write(exDavka);
                    }
                }
            }
        }


        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej controller_prodej = null;

        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi controller_zbozi = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici controller_pracovnici = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu controller_typdokladu = null;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele = null;



        public GlobalObject()
        {
          

            controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Common.CiselnikSkladyDB);
            controller_zbozi = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Common.CiselnikZboziDB);
            controller_pracovnici = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Pracovnici(Common.CiselnikPracovniciDB);
            controller_typdokladu = new Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu(Common.CiselnikTypDokladuDB);
            controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Common.CiselnikOdberateleDB);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (controller_prodej != null) controller_prodej.Dispose();
            if (controller_sklady != null) controller_sklady.Dispose();
            if (controller_zbozi != null) controller_zbozi.Dispose();
            if (controller_pracovnici != null) controller_pracovnici.Dispose();
            if (controller_typdokladu != null) controller_typdokladu.Dispose();
            if (controller_odberatele != null) controller_odberatele.Dispose();

            controller_prodej = null;
            controller_sklady = null;
            controller_zbozi = null;
            controller_pracovnici = null;
            controller_typdokladu = null;
            controller_odberatele = null;
        }

        #endregion
    }
}

