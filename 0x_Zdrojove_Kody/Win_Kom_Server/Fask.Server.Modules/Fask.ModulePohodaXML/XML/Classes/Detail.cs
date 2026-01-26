using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL.XML.Classes
{
    public class Detail
    {
        public string state { get; set; }
        public int errno { get; set; }
        public string note { get; set; }
        public string XPath { get; set; }
        public string valueRequested { get; set; }
        public string valueProduced { get; set; }
    }
}
