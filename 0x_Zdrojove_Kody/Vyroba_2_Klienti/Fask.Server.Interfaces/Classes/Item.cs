using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public class Item
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Order { get; set; }
        public string Serltnum { get; set; }

        // nove pro vratky
        public string Description { get; set; }
    }
}
