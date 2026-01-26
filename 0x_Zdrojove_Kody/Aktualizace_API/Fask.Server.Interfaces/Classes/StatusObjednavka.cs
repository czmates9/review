using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public enum STATUS
    {
        OK,
        ERROR
    }

    public class StatusObjednavka
    {
        public STATUS Result { get; set; }
        public string Message { get; set; }
    }
}
