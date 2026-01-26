using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Vyroba_VPH_Filtr: FilterBase
    {
        public byte? Active { get; set; }
        public string SOPNUMBE { get; set; }
    }
}
