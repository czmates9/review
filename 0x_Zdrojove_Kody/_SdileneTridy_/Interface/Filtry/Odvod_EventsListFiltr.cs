using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Odvod_EventsListFiltr : FilterBase
    {
        public DateTime? IsProcessed_OD { get; set; }
        public DateTime? IsProcessed_DO { get; set; }
        public Guid? productionGUID { get; set; }

        public bool Zpracovane { get; set; }
        public bool NEZpracovane { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? IsProcessed_TimeVariant { get; set; }
        public string IsProcessed_TimeVariant { get; set; }

        public DateTime? Dateeve_DO { get; set; }
        public DateTime? Dateeve_OD { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? Dateeve_TimeVariant { get; set; }
        public string Dateeve_TimeVariant { get; set; }


        public string Razeni_Column { get; set; }
        public string asc_desc { get; set; }


        public string MachineID { get; set; }
        public string Description { get; set; }
        public string status { get; set; }
        public string PackType { get; set; }

    }
}
