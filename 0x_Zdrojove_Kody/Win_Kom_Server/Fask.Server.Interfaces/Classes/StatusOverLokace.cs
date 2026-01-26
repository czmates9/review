using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    /// <summary>
    /// Určení, jaký typ lokace se ověřuje
    /// 'S' - zdrojova, zdali je mozne z lokace vzit
    /// 'D' - cilova, zdali je mozne na lokaci umistit
    /// </summary>
    public enum TYPLokace
    {
        SOURCE = 'S',
        DEST = 'D'
    }

	/// <summary>
	/// Definuje status
	/// </summary>
    public enum STATUSOverLokace
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

	/// <summary>
	/// třída nese informace o Lokaci v lok. mechanizmu
	/// </summary>
    public class StatusOverLokace
    {
		/// <summary>
		/// stav (0 - vse v poradku, 1 - poruseno doporucene poradi, mozno pokracovat, 2 - chyba, neni mozne pokracovat)
		/// </summary>
        public STATUSOverLokace State;    
        
		/// <summary>
		/// zpráva, která se zobrazí při chybě
		/// </summary>
        public string Message;            
    }
}
