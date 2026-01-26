using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Classes_Vyroba
{
    public class FiltersHistory
    {
        public bool filtrNedokonceneZakazky = false;
        public string filtrOsoba = string.Empty;
        public string filtrStroj = string.Empty;
        public DateTime? filtrDatumOd = null;
        public DateTime? filtrDatumDo = null;
        public string filtrZakazka = string.Empty;
    }
}
