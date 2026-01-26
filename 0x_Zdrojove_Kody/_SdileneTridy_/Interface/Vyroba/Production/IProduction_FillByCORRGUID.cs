using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_FillByCORRGUID : IProduction
    {
        void Production_FillByCORRGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID);
    }
}
