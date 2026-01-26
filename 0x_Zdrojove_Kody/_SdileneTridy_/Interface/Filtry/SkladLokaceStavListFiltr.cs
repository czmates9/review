using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class SkladLokaceStavListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu
        /// </summary>
        public string MaterialITEMNMBR { get; set; }

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
        /// Sarze materialu
        /// </summary>
        public string MaterialSerltnum { get; set; }

        /// <summary>
        /// Lokace materialu
        /// </summary>
        public string MaterialLocncode { get; set; }

        /// <summary>
        /// Sklad materialu
        /// </summary>
        public string MaterialSklID { get; set; }

        /// <summary>
        /// ID pracovnika
        /// </summary>
        public string MaterialPracID { get; set; }

        /// <summary>
        /// Zobrazí se pouze nenulový stav
        /// </summary>
        public bool PouzeNenulovyStav { get; set; }
    }
}
