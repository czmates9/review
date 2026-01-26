using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fask.Interfaces.DataSets;

namespace Fask.Server.Interfaces.Tisky
{
    public interface ITisk_TiskovaSablona : ITisky2
    {
        int TiskovaSablonaEdit_DB(Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row);

        int TiskovaSablonaInsert_DB(Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row);

        int TiskovaSablonaDelete_DB(Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row);
    }
}

