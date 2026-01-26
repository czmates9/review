using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources_GetFiltrovanyProductionSourcesList : IProductionSources
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionSourcesList(Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr);
    }
}
