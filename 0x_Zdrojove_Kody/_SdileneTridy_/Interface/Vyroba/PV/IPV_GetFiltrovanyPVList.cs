using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_GetFiltrovanyPVList : IPV
    {

         Fask.Interfaces.DataSets.Vyroba_Planovani GetFiltrovanyPVList(Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr);
    }
}
