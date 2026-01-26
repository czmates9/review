using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class SkladyListFiltr : FilterBase
    {


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
