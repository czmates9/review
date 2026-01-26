using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Vazby_P_PS_Filtr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu
        /// </summary>
        public string MaterialITEMNMBR { get; set; }

        /// <summary>
        /// Vyplneny nazev materialu
        /// </summary>
        public string MaterialITEMDESC { get; set; }

        /// <summary>
        /// Vyplneny kod materialu (itemcode)
        /// </summary>
        public string MaterialVNDITNUM { get; set; }

        /// <summary>
        /// Vyplneny carovy kod
        /// </summary>
        public string MaterialCarKod { get; set; }

        /// <summary>
        /// Lokace materialu
        /// </summary>
        public string MaterialLocncode { get; set; }

        /// <summary>
        /// Sklad materialu
        /// </summary>
        public string MaterialSklID { get; set; }

        /// <summary>
        /// MJ materialu
        /// </summary>
        public string MaterialMJ { get; set; }

        /// <summary>
        /// Potvrzeni priznak
        /// </summary>
        public bool PotvrzeniPriznak { get; set; }

        /// <summary>
        /// CountEntries
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// SOPNUMBE
        /// </summary>
        public string SOPNUMBE { get; set; }

        /// <summary>
        /// SKL_ID
        /// </summary>
        public string SKL_ID { get; set; }

    }
}
