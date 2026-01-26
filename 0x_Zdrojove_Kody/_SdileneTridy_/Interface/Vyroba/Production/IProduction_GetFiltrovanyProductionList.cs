using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_GetFiltrovanyProductionList : IProduction
    {

        Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionList(Fask.Interfaces.Filtry.ProductionListFiltr filtr);
    }
}
