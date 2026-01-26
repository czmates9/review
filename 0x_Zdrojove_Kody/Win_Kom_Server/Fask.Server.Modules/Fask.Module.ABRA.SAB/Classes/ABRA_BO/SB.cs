using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.ABRA_BO
{

    public class Rootobject_SB
    {
        public string id { get; set; }
        public string storecard_id { get; set; }
        public string name { get; set; }
        public bool? serialnumber { get; set; }
        
        [JsonProperty("expirationdate$date")]
        public DateTime? expirationdatedate { get; set; }


        //public DateTime expirationdatedate { get; set; }
        //public bool hidden { get; set; }
        //public string note { get; set; }
        //public int objversion { get; set; }
        //public DateTime? productiondatedate { get; set; }
        //public string specification { get; set; }

    }

}
