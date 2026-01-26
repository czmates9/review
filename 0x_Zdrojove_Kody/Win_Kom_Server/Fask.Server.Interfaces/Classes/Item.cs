using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Třída která nese informace o položce
	/// </summary>
    public class Item
    {
		/// <summary>
		/// ID položky
		/// </summary>
        public string ID { get; set; }
        
		/// <summary>
		/// Typ položky
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// pořadí položky
		/// </summary>
        public string Order { get; set; }

		/// <summary>
		/// seriove číslo položky
		/// </summary>
        public string Serltnum { get; set; }

        /// <summary>
        /// Popis položky
        /// </summary>
        public string Description { get; set; }
    }
}
