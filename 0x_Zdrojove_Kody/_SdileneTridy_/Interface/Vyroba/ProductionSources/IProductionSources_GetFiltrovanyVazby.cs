using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources_GetFiltrovanyVazby : IProductionSources
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr);
    }
}
