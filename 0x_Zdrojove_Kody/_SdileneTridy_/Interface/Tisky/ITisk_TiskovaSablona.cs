using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Tisky
{
    public interface ITisk_TiskovaSablona : ITisky2
    {
        int TiskovaSablonaEdit_DB(DataSets.Vyroba.FASK_FORMULARERow row);

        int TiskovaSablonaInsert_DB(DataSets.Vyroba.FASK_FORMULARERow row);

        int TiskovaSablonaDelete_DB(DataSets.Vyroba.FASK_FORMULARERow row);

    }
}
