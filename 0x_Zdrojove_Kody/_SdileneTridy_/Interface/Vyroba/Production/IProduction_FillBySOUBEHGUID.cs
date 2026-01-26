using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_FillBySOUBEHGUID : IProduction
    {
        void Production_FillBySOUBEHGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID);
    }
}
