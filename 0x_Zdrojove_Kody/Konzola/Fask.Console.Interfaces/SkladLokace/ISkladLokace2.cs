using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.SkladLokace
{

        /// <summary>
        /// Určení, jaký typ lokace se ověřuje
        /// 'S' - zdrojova, zdali je mozne z lokace vzit
        /// 'D' - cilova, zdali je mozne na lokaci umistit
        /// </summary>
        public enum TYPLokace
        {
            SOURCE = 'S',
            DEST = 'D'
        }

        public enum STATUSOverLokace
        {
            OK = 0,
            WARNING = 1,
            ERROR = 2
        }

        public class StatusOverLokace
        {
            public STATUSOverLokace State;              // stav (0 - vse v poradku, 1 - poruseno doporucene poradi, mozno pokracovat, 2 - chyba, neni mozne pokracovat)
            public string Message;            // zpráva, která se zobrazí při chybě
        }

        /// <summary>
        /// Typ, podle ktreho se rozhoduje, co se bude ukladat do tabulky pohybu.
        /// </summary>
        public enum TypeOfRecord
        {
            /// <summary>
            /// Empty
            /// </summary>
            E/*mpty*/,
            /// <summary>
            /// Prijem
            /// </summary>
            P/*rijem*/,
            /// <summary>
            /// Vydej
            /// </summary>
            V/*ydej*/,
            /// <summary>
            /// Inventura
            /// </summary>
            I/*nventura*/,
            /// <summary>
            /// Defregmentace
            /// </summary>
            D/*efregmentace*/
        };

        /// <summary>
        /// Oznaceni modulu, ktery funkci vola.
        /// </summary>
        public enum ModulName { PRIJEM, DEFREGMENTACE, VYDEJ };

        public interface ISkladLokace2 : IMES
        {
        }
}
