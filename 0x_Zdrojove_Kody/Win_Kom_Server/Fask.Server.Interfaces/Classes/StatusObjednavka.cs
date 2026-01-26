using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Enum o stavu
	/// </summary>
    public enum STATUS
    {
        OK,
        ERROR
    }

	/// <summary>
	/// třída která nese informace o stavu objednávky
	/// </summary>
    public class StatusObjednavka
    {
		/// <summary>
		/// Přiznak o stavu
		///  vse OK 
		///  error
		/// </summary>
        public STATUS Result { get; set; }

		/// <summary>
		/// poznámka
		/// </summary>
        public string Message { get; set; }
    }
}
