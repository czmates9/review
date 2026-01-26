using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class VazbyModifikace_TP : FilterBase
    {

        /// <summary>
        /// Vyplnene ID skladu
        /// </summary>
        public string SKL_ID { get; set; }

        /// <summary>
        /// Vyplnene ID materialu 
        /// </summary>
        public string ITEMNMBR { get; set; }


        /// <summary>
        /// priznak zda zobrazovat pouze shodně nalezene
        /// </summary>
        public bool Shodne { get; set; }


        /// <summary>
        /// priznak zda zobrazovat pouze materialy nenalezene na cilovem sklade
        /// </summary>
        public bool NEShodne { get; set; }

    }
}