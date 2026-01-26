using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Aktualizace_API.OdvadeniNadop
{
    public class Enums
    {
        public enum TIMEMODES
        {
            Unknown = -1,
            Stop = 0,
            StartStop = 1,
            StartStartStop = 2
        }

        public enum TIMESTATE
        {
            Nezahajeno,
            Korekce_Zahajena,
            Korekce_Dokoncena,
            Priprava_Zahajena,
            Priprava_Dokoncena,
            Odvod_Zahajen,
            Odvod_Dokoncen
        }
    }
}
