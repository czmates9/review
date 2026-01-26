using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class Rootobject_DIP
    {
        public Row_DIP[] rows { get; set; }
    }

    public class Row_DIP
    {
        public string id { get; set; }
        public string qunit { get; set; }
        public float? realquantity { get; set; }
        public float? unitrealquantity { get; set; }
        public bool? realquantitychanged { get; set; }
        public Row_Batch_DIP[] rows { get; set; }
    }

    public class Row_Batch_DIP
    {
        public string id { get; set; }
        public string MIPBatch_ID { get; set; }
        public string qunit { get; set; }
        public float? realquantity { get; set; }
        public float? unitrealquantity { get; set; }
    }
}
