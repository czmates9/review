using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_Update_Row : IProduction
    {
        void Production_Update_Row(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaRow Row);
    }
}
