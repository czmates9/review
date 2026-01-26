using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class Rootobject_PR
    {
        public string displayname { get; set; }
        public string id { get; set; }
        public Row_PR[] rows { get; set; }
    }

    public class Row_PR
    {
        public string id { get; set; }
        public float? quantity { get; set; }
        public DocRowBatches_PR[] DocRowBatches { get; set; }
    }

    public class DocRowBatches_PR
    {
        public bool? NewBatch { get; set; }

        [JsonProperty("NewBatchExpirationDate$DATE")]
        public DateTime? NewBatchExpirationDate { get; set; }

        public string NewBatchName { get; set; }
        
        public string StoreBatch_ID { get; set; }

        public float? quantity { get; set; }
    }
}
