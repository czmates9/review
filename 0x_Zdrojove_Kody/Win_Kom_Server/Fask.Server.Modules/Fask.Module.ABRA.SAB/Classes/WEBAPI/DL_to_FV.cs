using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes.WEBAPI
{
    public class DL_to_FV
    {
        [JsonProperty("params")]
        public Params _params { get; set; }
    }

    public class Params
    {
        public string SelectedHeader { get; set; }
        public string DocQueue_ID { get; set; }
        public string SelectedRows { get; set; }
    }


    public class SelectedRows
    {
        public List<string> sr = null;

        public SelectedRows()
        {
            sr = new List<string>();
        }

        public string GetSelectedRows()
        {
            string s = null;


            if (sr.Count > 0)
            {
                s = string.Join("\n", sr);
            }

            return s;
        }

    }
}
