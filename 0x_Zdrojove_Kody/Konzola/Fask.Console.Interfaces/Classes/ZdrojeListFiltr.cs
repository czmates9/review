using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class ZdrojeListFiltr
    {
        public ZdrojeListFiltr()
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
