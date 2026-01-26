using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// Enum který definuje stavy
	/// </summary>
    public enum StatusResultEnum
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

	/// <summary>
	/// Třída která nese informace o tisku soupisu
	/// </summary>
    public class StatusResult
    {
        /// <summary>
		/// Přiznak o stavu
        /// 0 - vse OK 
        /// 1 - warning
		/// 2 - error
        /// </summary>
        public StatusResultEnum Status;

		/// <summary>
		/// Správa o stavu
		/// </summary>
        public string Message;

		/// <summary>
		/// Metoda pro nastavení chyby
		/// </summary>
		/// <param name="msg">Poznámka</param>
		public void SetERROR(string msg)
		{
			this.Status = StatusResultEnum.ERROR;
			this.Message = msg;
		}

		/// <summary>
		/// Metoda pro nastavení že je vše OK
		/// </summary>
		/// <param name="msg">Poznámka</param>
		public void SetOK(string msg)
		{
			this.Status = StatusResultEnum.OK;
			this.Message = msg;
		}

		/// <summary>
		/// Metoda pro nastavení upozornení.
		/// </summary>
		/// <param name="msg">Poznámka</param>
		public void SetWARNING(string msg)
		{
			this.Status = StatusResultEnum.WARNING;
			this.Message = msg;
		}
    }
}
