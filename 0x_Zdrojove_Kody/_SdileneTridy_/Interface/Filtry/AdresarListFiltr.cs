using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class AdresarListFiltr : FilterBase
    {

        public string id { get; set; }
        public string desc { get; set; }
        public string typ { get; set; }
        public string carcode { get; set; }
    }
}
