using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class VydejHlavickyFiltr
    {
        public VydejHlavickyFiltr()
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
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// sopnumbe
        /// </summary>
        public string Sopnumbe { get; set; }

        /// <summary>
        /// Rozpracovano (ma byt int)
        /// </summary>
        public string Rozpracovano { get; set; }

        /// <summary>
        /// Priority (ma byt byte)
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Zobrazeni pouze nestazene davky
        /// </summary>
        public bool ZobrazitPouzeNestazeneDavky { get; set; }
    }
}
