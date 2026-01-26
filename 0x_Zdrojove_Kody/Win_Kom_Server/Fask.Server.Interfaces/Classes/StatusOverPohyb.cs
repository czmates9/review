using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Třída ktere informace o Pohybu
	/// </summary>
    public class StatusOverPohyb
    {
		/// <summary>
		/// příznak zda je pohyb OK
		/// </summary>
        public bool PohybOK { get; set; }
		
		/// <summary>
		/// poznámka 
		/// </summary>
        public string Message { get; set; }

		/// <summary>
		/// Nese informaci kolik je k dispozici
		/// </summary>
        public decimal a_dispozice { get; set; }
    }
}
