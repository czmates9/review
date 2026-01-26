using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_EventsErr
{
    public interface IOdvod_EventsErr_GetFiltrovanyOdvodEvents : IOdvod_EventsErr
    {
        Fask.Interfaces.DataSets.Vyroba EventsErr_GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsErrListFiltr filtr);
    }
}
