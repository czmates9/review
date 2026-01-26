using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{
    public class Rootobject_PRV
    {
        public string displayname { get; set; }
        public string id { get; set; }
        public Row_PRV[] rows { get; set; }
    }

    public class Row_PRV
    {
        public string id { get; set; }
        public float? quantity { get; set; }
        public DocRowBatches_PRV[] DocRowBatches { get; set; }

        // Nová property pro cílovou lokaci (název slaď podle ABRA BO)
        public string StorePlace_ID { get; set; }
        // pokud by ABRA uměla pracovat s kódem, můžeš si nechat i:
        // public string StorePlace_Code { get; set; }
    }

    public class DocRowBatches_PRV
    {
        public string StoreBatch_ID { get; set; }
        public float? quantity { get; set; }
    }
}
