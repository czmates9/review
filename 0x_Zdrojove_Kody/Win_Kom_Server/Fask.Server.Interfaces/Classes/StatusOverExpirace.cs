using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public enum StatusOverExpiraceState
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

    public class StatusOverExpirace
    {
        public StatusOverExpiraceState State;   // stav (0 - vse v poradku, 1 - poruseno doporucene poradi, mozno pokracovat, 2 - chyba, neni mozne pokracovat)
        public string Message;                  // zpráva, která se zobrazí při chybě
    }
}
