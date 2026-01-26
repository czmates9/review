using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.Odvod_Events
{
    public interface IOdvod_Events_StornoEvent_OnlineCheck : IOdvod_Events
    {
        int? StornoEvent_OnlineCheck(Guid G);
    }
}
