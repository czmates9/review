using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{

    public class Rootobject_PRP
    {
        public string displayname { get; set; }
        public string id { get; set; }
        public Row_PRP[] rows { get; set; }

    }

    public class Row_PRP
    {

        public string displayname { get; set; }
        public string id { get; set; }
        public string parent_id { get; set; }
        public float quantity { get; set; }
        public string store_id { get; set; }
        public string storecard_id { get; set; }
    }

}
