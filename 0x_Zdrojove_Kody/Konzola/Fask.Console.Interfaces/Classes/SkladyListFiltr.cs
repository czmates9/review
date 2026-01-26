using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class SkladyListFiltr
    {
        public SkladyListFiltr()
        { }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NazevFiltru) ? string.Empty : NazevFiltru.Trim();
        }

        /// <summary>
        /// Název filtru, který se bude zobrazovat.
        /// </summary>
        public string NazevFiltru { get; set; }

        /// <summary>
        /// Vyplnene ID uzivatele (sloupec ID)
        /// </summary>
        public string skl_id { get; set; }

        /// <summary>
        /// Vyplneny login uzivatel.
        /// </summary>
        public string skl_desc { get; set; }
    }
}
