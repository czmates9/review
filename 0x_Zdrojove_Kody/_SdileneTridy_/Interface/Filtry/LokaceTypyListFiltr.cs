using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class LokaceTypyListFiltr : FilterBase
    {


        /// <summary>
        /// Typ lokace
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Je prijmova lokace.
        /// </summary>
        public bool is_receive { get; set; }

        /// <summary>
        /// Je vychozi lokace.
        /// </summary>
        public bool is_default { get; set; }

        /// <summary>
        /// Je bezna/normalni lokace.
        /// </summary>
        public bool is_normal { get; set; }
    }
}
