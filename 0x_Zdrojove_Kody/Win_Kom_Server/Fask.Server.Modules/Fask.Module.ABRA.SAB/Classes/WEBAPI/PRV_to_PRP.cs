using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.WEBAPI
{
    public class PRV_to_PRP
    {
        [JsonProperty("params")]
        public Params_PRP _params { get; set; }
    }

    public class Params_PRP
    {
        public string DocQueue_ID { get; set; }
        public string Store_ID { get; set; }
    }
}
