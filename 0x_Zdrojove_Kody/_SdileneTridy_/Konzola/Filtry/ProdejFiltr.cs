using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Filtry;

namespace Fask.Interfaces.Filtry
{
    public class ProdejFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }

        public DateTime? DATEDONE_DO { get; set; }
        public DateTime? DATEDONE_OD { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? Dateeve_TimeVariant { get; set; }
        public string DATEDONE_TimeVariant { get; set; }

    }
}
