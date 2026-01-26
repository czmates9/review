using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Modifikace_TP_GetFiltrovaneData : IVazby2
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovaneData(Fask.Interfaces.Filtry.VazbyModifikace_TP filtr);
    }
}
