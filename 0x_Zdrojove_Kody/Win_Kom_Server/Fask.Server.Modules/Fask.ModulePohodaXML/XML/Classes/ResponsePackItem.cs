using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL.XML.Classes
{
	public class ResponsePackItem
	{
        public string version { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string note { get; set; }

        public OrderResponse orderResponse { get; set; }
	}
}
