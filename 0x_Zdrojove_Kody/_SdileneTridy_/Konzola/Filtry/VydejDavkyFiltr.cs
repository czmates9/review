using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class VydejDavkyFiltr : FilterBase
    {


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
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }




        /// <summary>
        /// Zobrazeni  ulovnene davky CZ doslo = 0
        /// </summary>
        public bool UvolneneDavky { get; set; }

        /// <summary>
        /// Zobrazeni  NEulovnene davky CZ doslo = 255
        /// </summary>
        public bool NEUvolneneDavky { get; set; }

        /// <summary>
        /// Zobrazeni stažene davky CZ doslo = 1-99
        /// </summary>
        public bool StazeneDavky { get; set; }

        /// <summary>
        /// Zobrazeni spracovane davky CZ doslo = 101-199
        /// </summary>
        public bool SpracovaneDavky { get; set; }


        /// <summary>
        /// Zobrazeni mrtvé davky CZ doslo = 201
        /// </summary>
        public bool MrtveDavky { get; set; }





    }
}
