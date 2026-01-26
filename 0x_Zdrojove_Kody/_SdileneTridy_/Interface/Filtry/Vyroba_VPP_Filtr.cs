using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Vyroba_VPP_Filtr: FilterBase
    {
        public string OrderBy { get; set; }

        public int? CountEntries { get; set; }
        public string SOPNUMBE { get; set; }
    }
}
