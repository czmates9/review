
using System;
using System.Collections;

namespace FASK.SledovaniVyroby.Counters.Kuebler_716
{
    /// <summary>
    /// Implementace protokolu s citacem Kuebler Codix 716. 
    /// Cela instrukcni sada a popis znaku i formatu instrukci v dodatecnem dokumentu k citaci.
    /// </summary>
    static class ESCProtocol
    {
        //Znak ESC
        internal static char ESC = (char)0x1B;
        //Znak STX
        internal static char STX = (char)0x02;
        //Znak CR
        internal static char CR = (char)0x0D;
        //Znak LF
        internal static char LF = (char)0x0A;

        //Zakoncovaci sekvence CRLF (bez string.Empty pise error)
        internal static string CRLF = string.Empty + CR + LF;

        /// <summary>
        /// Pozadavek byl OK, odpoved je OK (nevraci se data)
        /// </summary>
        internal static string OK = CRLF;

        /// <summary>
        /// Nekde se stala chyba, citac vratil chybu
        /// </summary>
        internal static string ERROR = 'F' + CRLF;

        /// <summary>
        /// Cteni soucasne hodnoty z citace.
        /// </summary>
        internal static string CurrentCounterValue = ESC + "0" + CRLF;

        /// <summary>
        /// Nastaveni hodnoty citace na nulu, v pripade pricitani, nebo na predvolbu, v pripade odecitani.
        /// </summary>
        internal static string ResetCounterValue = ESC + "Z" + CRLF;
    }
}
