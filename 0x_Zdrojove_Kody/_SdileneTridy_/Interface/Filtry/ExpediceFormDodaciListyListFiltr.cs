using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ExpediceFormDodaciListyListFiltr :FilterBase
    {

        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu.
        /// </summary>
        public string ITEMNMBR { get; set; }

        /// <summary>
        /// Vyplnene cislo palety.
        /// </summary>
        public string NMBRPAL { get; set; }

        /// <summary>
        /// Priznak dokoncenosti.
        /// </summary>
        public string Rozpracovano { get; set; }
    }
}
