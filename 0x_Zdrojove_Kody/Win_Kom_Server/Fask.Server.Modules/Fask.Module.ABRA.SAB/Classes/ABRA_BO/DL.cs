using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class Rootobject_DL
    {
        public string displayname { get; set; }
        public string id { get; set; }
        public Row_DL[] rows { get; set; }
    }

    public class Row_DL
    {
        public string id { get; set; }
        public float? quantity { get; set; }
        public DocRowBatches_DL[] DocRowBatches { get; set; }
    }

    public class DocRowBatches_DL
    {
        public string StoreBatch_ID { get; set; }
        public float? quantity { get; set; }
    }
}
