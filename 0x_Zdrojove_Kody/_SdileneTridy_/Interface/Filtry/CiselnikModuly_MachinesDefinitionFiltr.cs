using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class CiselnikModuly_MachinesDefinitionFiltr : FilterBase
    {

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? IsProcessed_TimeVariant { get; set; }
        //public string TimeVariant { get; set; }

        //public DateTime? DateModified_DO { get; set; }
        //public DateTime? DateModified_OD { get; set; }


        public string SKL_ID { get; set; }
        public string LOCNCODE { get; set; }

        public string IP { get; set; }

        public string Description { get; set; }

        public string id { get; set; }

        public string MType { get; set; }
     
        public int? PORT { get; set; }
        public int ID_group { get; set; }

    }
}
