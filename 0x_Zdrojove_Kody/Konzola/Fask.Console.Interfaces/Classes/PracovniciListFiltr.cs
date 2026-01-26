using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class PracovniciListFiltr
    {
        public PracovniciListFiltr()
        { }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NazevFiltru) ? string.Empty : NazevFiltru.Trim();
        }

        /// <summary>
        /// Název filtru, který se bude zobrazovat.
        /// </summary>
        public string NazevFiltru { get; set; }

        /// <summary>
        /// Vyplnene ID pracovnika (sloupec ID)
        /// </summary>
        public string Prac_ID { get; set; }

        /// <summary>
        /// Vyplneny desc pracovnika.
        /// </summary>
        public string Prac_Desc { get; set; }
    }
}
