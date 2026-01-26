using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_GetZaplanovani : IPV
    {

        Fask.Interfaces.DataSets.Vyroba_Planovani GetZaplanovani(Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filter);
    }
}
