using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Events
{
    public interface IEvents
    {
        public bool synchronize();
        public bool add(Event euser);
    }
}
