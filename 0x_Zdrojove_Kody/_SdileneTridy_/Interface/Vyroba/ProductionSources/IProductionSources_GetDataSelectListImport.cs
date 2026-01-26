using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{

    public interface IProductionSources_GetDataSelectListImport : IProductionSources
    {
        Fask.Interfaces.DataSets.Vyroba GetDataSelectListImport();
    }
}
