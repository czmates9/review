using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Trřída která nese informace o položce pro ověření pohybu 
	/// </summary>
    public class ProdejPohyb
    {
       
		/// <summary>
		/// Seriove číslo
		/// </summary>
        public string Serltnum { get; set; }
        
		/// <summary>
        /// ID Typu dokladu
        /// </summary>
		public string Doc_id { get; set; }
        
		/// <summary>
		/// ID 2 Typu dokladu
		/// </summary>
		public string Doc_id2 { get; set; }

		/// <summary>
		/// ID položky
		/// </summary>
        public string Itemnmbr { get; set; }

		/// <summary>
		/// ID Skladu
		/// </summary>
        public string skl_id { get; set; }
        
		/// <summary>
		/// množství
		/// </summary>
		public decimal qtyshppd { get; set; }
    }
}
