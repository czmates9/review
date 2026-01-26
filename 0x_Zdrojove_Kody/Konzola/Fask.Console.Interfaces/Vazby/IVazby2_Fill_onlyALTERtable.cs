using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Fill_onlyALTERtable : IVazby2
    {

        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_onlyALTERtable(string ITEMNMBR_Def);

    }
}
