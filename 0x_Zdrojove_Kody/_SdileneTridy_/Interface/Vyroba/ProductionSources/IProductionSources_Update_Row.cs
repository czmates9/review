using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources_Update_Row : IProductionSources
    {
        void Update_Row(Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow Row);

    }
}
