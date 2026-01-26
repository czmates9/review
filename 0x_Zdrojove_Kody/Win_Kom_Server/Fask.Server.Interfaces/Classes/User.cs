using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
	/// <summary>
	/// třída která nese informace o uživatelovy
	/// </summary>
    public class User
    {
		/// <summary>
		/// ID uživatele
		/// </summary>
        public int ID { get; set; }
        
		/// <summary>
		/// Login uživatele
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Heslo uživatele
		/// </summary>
        public string Password { get; set; }

		/// <summary>
		/// Administrátor, příznak
		/// </summary>
        public int ADM { get; set; }

		/// <summary>
		/// Jméno
		/// </summary>
        public string firstname { get; set; }

		/// <summary>
		/// přímení
		/// </summary>
        public string secondname { get; set; }

		/// <summary>
		/// čarový kód
		/// </summary>
        public string barcode { get; set; }
    }
}
