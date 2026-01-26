using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Module.Ingres.SAD
{
	/// <summary>
	/// Metoda pro upravu vtupnich parametru do SQL dotazu
	/// </summary>
    public class SQLInjection
    {
		/// <summary>
		/// Konstruktor
		/// </summary>
        private SQLInjection()
        {
        }

        /// <summary>
        /// filtruje uzivatelsky vstup proti sql-injection
        /// </summary>
        /// <param name="userinput">Vtupní řetežec</param>
        /// <returns>Upraveny retezec</returns>
        public static string Filter(string userinput)
        {
            System.Text.StringBuilder sb;

            if (userinput != null)
            {
                sb = new System.Text.StringBuilder(userinput.Trim());
            }
            else
            {
                // Nastavte sb na novou instanci nebo provedete jinou vhodnou akci v případě, že userinput je null
                sb = new System.Text.StringBuilder();
            }

            //System.Text.StringBuilder sb = new System.Text.StringBuilder(userinput.Trim());
            sb.Replace("'", "");
            sb.Replace("%", "");
            sb.Replace("-", "");
            sb.Replace(" ", "");
            return sb.ToString();
        }
    }
}
