using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public enum STATUSPolozkaRes
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }


    public class StatusPolozka
    {
        public STATUSPolozkaRes Result { get; set; }
        public string Message { get; set; }
    }
}
