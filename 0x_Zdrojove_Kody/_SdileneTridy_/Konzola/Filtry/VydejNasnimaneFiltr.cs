using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{

    public class VydejNasnimaneFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }

        /// <summary>
        /// čislo dokladu
        /// </summary>
        public string SOPNUMBE { get; set; }

    }
}
