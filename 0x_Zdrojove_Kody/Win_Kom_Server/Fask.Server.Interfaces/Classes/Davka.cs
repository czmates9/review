using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Třída dávka, která nese informace o dávce.
	/// </summary>
    public class Davka
    {
		/// <summary>
		/// ID Dávky
		/// </summary>
        public int? ID { get; set; }

		/// <summary>
		/// Popis dávky
		/// </summary>
        public string Description { get; set; }
    }
}
