using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Odvod_MachineStateSetListFiltr : FilterBase
    {

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? IsProcessed_TimeVariant { get; set; }
        public string TimeVariant { get; set; }

        public DateTime? DateModified_DO { get; set; }
        public DateTime? DateModified_OD { get; set; }


        public string CisloSluzby { get; set; }

        public string Zdroj { get; set; }
        public string StrojSklad { get; set; }
        public string StrojLokace { get; set; }

        public string AdamIP { get; set; }

        public string Description { get; set; }

        public string id { get; set; }

        public string name { get; set; }
        /// <summary>
        /// Aktuální záznam, aktuální/historie
        /// </summary>
        /// return nic
        public bool zaznam { get; set; }
        public int? S0 { get; set; }
        public int? S1 { get; set; }
        public int? S2 { get; set; }
        public int? S3 { get; set; }
        public int? S4 { get; set; }
        public int? S5 { get; set; }
        public int? S6 { get; set; }
        public int? S7 { get; set; }
        public int? S8 { get; set; }
        public int? S9 { get; set; }
        public int? S10 { get; set; }
        public int? S11 { get; set; }

    }
}
