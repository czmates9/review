using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    /// <summary>
    /// Objekt pro predavani informaci o zvolene lokaci
    /// </summary>
    public class LokaceItem
    {
        /// <summary>
        /// Cislo
        /// </summary>
        public string ITEMNMBR { get; set; }

        /// <summary>
        /// Sarze
        /// </summary>
        public string SERLNMBR { get; set; }

        /// <summary>
        /// Mnozstvi
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// Lokace
        /// </summary>
        public string LOCNCODE { get; set; }

        // ... pripadne dalsi hodnoty
    }
}
