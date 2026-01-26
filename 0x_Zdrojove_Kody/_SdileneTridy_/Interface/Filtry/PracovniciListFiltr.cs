using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class PracovniciListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID pracovnika (sloupec ID)
        /// </summary>
        public string Prac_ID { get; set; }

        /// <summary>
        /// Vyplneny desc pracovnika.
        /// </summary>
        public string Prac_Desc { get; set; }
    }
}
