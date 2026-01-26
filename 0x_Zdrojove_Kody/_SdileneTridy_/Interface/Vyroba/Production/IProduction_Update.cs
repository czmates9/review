using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_Update : IProduction
    {
        void Production_Update(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt);
    }
}
