using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Zakladni třída.
	/// </summary>
    public partial class Provider 
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Provider()
        {
            try
            {
                Fask.Server.Interfaces.Configuration.IConfiguration iconfig = this as Fask.Server.Interfaces.Configuration.IConfiguration;
                if (iconfig != null)
                    iconfig.LoadConfiguration();

            }
            catch (Exception ex)
            {
				// \TODO : zalogovat chybu, poslat chybu logovacim mechanismem ... 
                throw ex;
            }
        }


    }
}
