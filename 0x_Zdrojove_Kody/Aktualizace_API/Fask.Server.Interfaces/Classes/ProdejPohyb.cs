using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.Server.Interfaces.Classes
{
    public class ProdejPohyb
    {
        // sériové číslo
        public string Serltnum { get; set; }
        public string Doc_id { get; set; }
        public string Doc_id2 { get; set; }
        // číslo položky
        public string Itemnmbr { get; set; }

        // overpohyb2
        public string skl_id { get; set; }
        public decimal qtyshppd { get; set; }
    }
}
