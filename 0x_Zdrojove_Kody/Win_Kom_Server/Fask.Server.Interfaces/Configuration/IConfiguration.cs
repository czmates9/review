using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Configuration
{
	/// <summary>
	/// Interface pro možne načtení konfigurace pomoci providera
	/// </summary>
    public interface IConfiguration
    {
		/// <summary>
		/// Metoda pro načtení konfigurace
		/// </summary>
		/// <returns>True- OK, False- chyba</returns>
        bool LoadConfiguration();
    }
}
