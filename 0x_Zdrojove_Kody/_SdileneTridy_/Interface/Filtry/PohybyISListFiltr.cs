using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class PohybyISListFiltr : FilterBase
    {


        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu
        /// </summary>
        public string ITEMNMBR { get; set; }

        /// <summary>
        /// Vyplneny nazev materialu
        /// </summary>
        public string ITEMDESC { get; set; }

        /// <summary>
        /// Vyplneny kod materialu (itemcode)
        /// </summary>
        public string ITEMCODE { get; set; }
        
        public DateTime? DatumVytvoreni_OD { get; set; }
        public DateTime? DatumVytvoreni_DO { get; set; }
        public Fask.Interfaces.Classes.TimeFilters.TimeVariants? DatumVytvoreni_TimeVariant { get; set; }

        public DateTime? DatumUlozeni_OD { get; set; }
        public DateTime? DatumUlozeni_DO { get; set; }
        public Fask.Interfaces.Classes.TimeFilters.TimeVariants? DatumUlozeni_TimeVariant { get; set; }


    }
}
