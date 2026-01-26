using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Module.Ingres.SAD
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Konfigurace.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Configuration.IConfiguration
    {
        #region IConfiguration Members

		/// <summary>
		/// Metoda pro načtení konfigurace
		/// </summary>
		/// <returns>True- OK, False- chyba</returns>
        public bool LoadConfiguration()
        {
            //throw new NotImplementedException();
            return true;
        }

        #endregion
    }
}
