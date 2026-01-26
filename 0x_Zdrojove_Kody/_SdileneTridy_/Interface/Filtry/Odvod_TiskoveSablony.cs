using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class Odvod_TiskoveSablonyFiltr : FilterBase
    {
        public string nazev_okna { get; set; }
        public string loginid { get; set; }

        public string machineid { get; set; }
        public int ord { get; set; }

        
        public string typ { get; set; }

    }
}
