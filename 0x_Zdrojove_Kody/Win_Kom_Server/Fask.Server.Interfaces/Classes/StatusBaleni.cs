using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// třída která nese informace o Balení
	/// </summary>
    public class StatusBaleni
    {
		/// <summary>
		/// Přiznak o stavu :
		///  vse OK 
		///  error
		/// </summary>
        public STATUS Result { get; set; }

		/// <summary>
		/// Poznámka
		/// </summary>
        public string Message { get; set; }
    }
}
