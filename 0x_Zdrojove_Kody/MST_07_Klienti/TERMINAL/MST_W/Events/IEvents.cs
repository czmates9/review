using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Events
{
    public interface IEvents
    {
        bool synchronize();
        bool add(Event euser);
    }
}
