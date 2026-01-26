using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL.XML.Classes
{
    public class OrderResponse
    {
        public string state { get; set; }
        public string version { get; set; }

        public ImportDetails importDetails { get; set; }
        public ProducedDetails producedDetails { get; set; }
    }
}
