using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Enum která definuje stavy
	/// </summary>
    public enum STATUSPolozkaRes
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

	/// <summary>
	/// Třída nese informace o položce
	/// </summary>
    public class StatusPolozka
    {
		/// <summary>
		/// Přiznak o stavu
		/// 0 - vse OK 
		/// 1 - warning
		/// 2 - error
		/// </summary>
        public STATUSPolozkaRes Result { get; set; }

		/// <summary>
		/// Poznámka
		/// </summary>
        public string Message { get; set; }
    }
}
