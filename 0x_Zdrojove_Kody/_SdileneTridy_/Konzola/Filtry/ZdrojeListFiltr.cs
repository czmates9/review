using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ZdrojeListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID uzivatele (sloupec ID)
        /// </summary>
        public string ZdrojID { get; set; }

        /// <summary>
        /// Oznaceni zdroje.
        /// </summary>
        public string ZdrojOznaceni { get; set; }

        /// <summary>
        /// Carovy kod zdroje.
        /// </summary>
        public string ZdrojBarcode { get; set; }

        /// <summary>
        /// Typ zdroje.
        /// </summary>
        public string ZdrojType { get; set; }
    }
}
