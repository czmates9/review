using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.Production_SN
{
    public interface IProduction_SN_GetFiltrovanyProduction_SNList : IProduction_SN
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProduction_SNList(Fask.Interfaces.Filtry.Production_SNListFiltr filtr);
    }
}
