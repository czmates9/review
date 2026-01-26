using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_Events
{
    public interface IOdvod_Events_GetFiltrovanyOdvodEvents : IOdvod_Events
    {
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr);
    }
}
