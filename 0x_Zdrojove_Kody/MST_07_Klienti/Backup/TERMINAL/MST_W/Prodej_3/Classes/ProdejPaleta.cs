using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Prodej_3.Classes
{
    public class ProdejPaleta
    {
        public string Typ { get; set; }
        public string Cislo { get; set; }

        public ProdejPaleta()
        {
        }

        public ProdejPaleta(string typ, string cislo)
            : this()
        {
            this.Typ = typ;
            this.Cislo = cislo;
        }
    }
}
