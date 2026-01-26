using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_Events
{
    public interface IOdvod_Events_StornoEvent : IOdvod_Events
    {
        bool StornoEvent(Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row);
    }
}
