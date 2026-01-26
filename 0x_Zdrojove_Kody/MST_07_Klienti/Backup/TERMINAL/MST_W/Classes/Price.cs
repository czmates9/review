using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    public class Price
    {
        public byte cenovaHladina = 0;
        public bool jeCenaSDani = false;

        public decimal cenaSDani = 0;
        public decimal cenaBezDane = 0;
        public decimal cenaDan = 0;
        public string mena = null;

        public decimal? cenaSDaniM = null;
        public decimal? cenaBezDaneM = null;
        public decimal? cenaDanM = null;
        public string menaM = null;
    }
}
