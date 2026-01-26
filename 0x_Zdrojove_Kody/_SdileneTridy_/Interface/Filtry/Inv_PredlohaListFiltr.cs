using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Filtry
{
    public class Inv_PredlohaListFiltr : FilterBase
    {

        public string CountEntries { get; set; }

        public string ITEMNMBR { get; set; }

        public string LOCNCODE { get; set; }

        public string SKL_ID { get; set; }

        public bool ZobrazitAlternativnyCaroveKody { get; set; }

        public bool ZobrazitSarze { get; set; }

        public bool ZobrazitZakladni { get; set; }

        public int alternativaRazeni { get; set; }

    }
}
