using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Login
{
	/// <summary>
	/// Interface pro Tvorbu souboru pro Terminal
	/// </summary>
    public interface ILoginTerminal  : ILogin
    {

		/// <summary>
		/// Metoda pro nachystani souboru uživatele podle ID TErminalu
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="uzivatele">Uživatele</param>
		/// <param name="dstFile">Cesta k souboru</param>
		/// <returns>True-OK, False- chyba</returns>
        bool Login_GetKatalogUzivatele(byte terminal, Fask.DataSets.Uzivatele uzivatele, out string dstFile);

    }
}
