using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class UzivateleListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID uzivatele (sloupec ID)
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Vyplneny login uzivatel.
        /// </summary>
        public string UserLogin { get; set; }
    }
}
