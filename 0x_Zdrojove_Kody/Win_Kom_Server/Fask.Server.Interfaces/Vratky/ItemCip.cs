using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Fask.Server.Interfaces.Vratky
{
    /// <summary>
    /// Objekt nalezenych seriovych cisel a odpovidajici polozce systemu
    /// </summary>
    /// <remarks>
    /// Jestlize seriove cislo neni nalezeno v IS, je ITEMNMBR, ITEMDESC nastaveno na hodnotu <b>null</b>
    /// </remarks>
    public class ItemCip
    {
        /// <summary>
        /// Cislo polozky
        /// </summary>
        public string Itemnmbr { get; set; }

        /// <summary>
        /// Nazev polozky
        /// </summary>
        public string Itemdesc { get; set; }

        /// <summary>
        /// Mnozstvi nalezenych polozek k seriovemu cislu
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Seznam nalezenych seriovych cisel
        /// </summary>       
        public List<string> Serltnums { get; set; }

		// \TODO : do budoucna bude nutne rozsirit takto:
        //public System.Collections.Generic.KeyValuePair<string, decimal> SerltnumQuantityCollection { get; set; }
    }
}
