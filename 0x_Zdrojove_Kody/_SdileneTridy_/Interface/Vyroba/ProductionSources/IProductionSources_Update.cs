using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources_Update : IProductionSources
    {
        void Update(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt);

    }
}
