using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ZboziVyrobaListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu
        /// </summary>
        public string MaterialID { get; set; }

        /// <summary>
        /// Vyplneny nazev materialu
        /// </summary>
        public string MaterialNazev { get; set; }

        /// <summary>
        /// Vyplneny kod materialu (itemcode)
        /// </summary>
        public string MaterialItemcode { get; set; }

        /// <summary>
        /// Vyplneny carovy kod
        /// </summary>
        public string MaterialBarcode { get; set; }

        /// <summary>
        /// Zobrazeni duplicitnich carovych kodu
        /// </summary>
        public bool ZobrazitDuplicitniCaroveKody { get; set; }
    }
}
