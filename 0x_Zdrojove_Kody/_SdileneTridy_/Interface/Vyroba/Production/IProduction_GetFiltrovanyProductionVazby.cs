using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_GetFiltrovanyProductionVazby : IProduction
    {

        Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr);
    }
}
