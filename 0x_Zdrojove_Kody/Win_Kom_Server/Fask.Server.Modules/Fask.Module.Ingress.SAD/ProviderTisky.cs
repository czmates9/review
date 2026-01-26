using Fask.DataSets;
using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.SQL.Constants;
using Fask.Server.Interfaces.Vyroba;
using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fask.Module.Ingres.SAD.Classes;
using Fask.Constants;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Vyroba.Odvod_MachineStateSet;
using Fask.Interfaces.Filtry;
using Fask.WEBAPI.API_BusinessObjects;
using Fask.Interfaces.Vyroba.Odvod_TiskoveSablony;

namespace Fask.Module.Ingres.SAD
{
    public partial class Provider :
       Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona
    {
        public int TiskovaSablonaDelete_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Delete(Globals.Konfigurace.ConnectionString[0].FASKDB, row);
        }

        public int TiskovaSablonaEdit_DB(Vyroba.FASK_FORMULARERow row)
        {
            //throw new NotImplementedException();
            return Database.Tisk_FASK_FORMULARE.Update(row, Globals.Konfigurace.ConnectionString[0].FASKDB);
        }

        public int TiskovaSablonaInsert_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Insert(Globals.Konfigurace.ConnectionString[0].FASKDB, row);
        }
    }
}
