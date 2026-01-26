using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.Tisky.ITisky2,
        Fask.Interfaces.Tisky.ITisk_TiskovaSablona
    {
        public int TiskovaSablonaDelete_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Delete(ConnectionString, row);
        }

        public int TiskovaSablonaEdit_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.Update(row, ConnectionString);
        }

        public int TiskovaSablonaInsert_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Insert(ConnectionString, row);
        }
    }
}
