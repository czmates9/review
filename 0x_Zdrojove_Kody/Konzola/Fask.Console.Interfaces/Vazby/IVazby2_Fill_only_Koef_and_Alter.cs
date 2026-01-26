using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Fill_only_Koef_and_Alter : IVazby2
    {

        Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_only_Koef_and_Alter(string ITEMNMBR_Def, string ITEMNMBR_fol);

    }
}
