using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class SkladLokaceMapaListFiltr
    {
        public SkladLokaceMapaListFiltr()
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
        /// lokace
        /// </summary>
        public string locncode { get; set; }

        /// <summary>
        /// sklad
        /// </summary>
        public string skl_id { get; set; }
    }
}
