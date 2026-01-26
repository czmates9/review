using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class Rootobject_HIP
    {
        public Row_HIP[] rows { get; set; }
    }

    public class Row_HIP
    {
        public string id { get; set; }
        public string qunit { get; set; }
        public string storebatch_id { get; set; }
    }

}
