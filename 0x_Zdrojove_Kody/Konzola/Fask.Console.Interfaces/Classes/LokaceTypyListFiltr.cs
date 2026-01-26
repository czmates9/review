using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class LokaceTypyListFiltr
    {
        public LokaceTypyListFiltr()
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
        /// Typ lokace
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Je prijmova lokace.
        /// </summary>
        public bool? is_receive { get; set; }

        /// <summary>
        /// Je vychozi lokace.
        /// </summary>
        public bool? is_default { get; set; }

        /// <summary>
        /// Je bezna/normalni lokace.
        /// </summary>
        public bool? is_normal { get; set; }
    }
}
