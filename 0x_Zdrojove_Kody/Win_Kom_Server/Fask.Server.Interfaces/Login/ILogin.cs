using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Login
{
	/// <summary>
	/// Interface pro Uživatele(Login)
	/// </summary>
    public interface ILogin
    {
		/// <summary>
		/// Metoda pro online oveřeni uživatele
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="hash">Hash</param>
		/// <param name="uzivatelID">Reference na ID uživatele</param>
		/// <returns>True-OK, False- chyba</returns>
        bool Login_OnlineLogin(User uzivatel, Terminal terminal, string hash, ref int uzivatelID);

		/// <summary>
		/// Metoda pro vypočet HASH
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Vypočitany HASH</returns>
        string Login_GetHash(User uzivatel, Terminal terminal);

		/// <summary>
		/// Metoda pro dotaženi uživatele z Databaze
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Uzivatele naplnen uživatelama</returns>
        Fask.DataSets.Uzivatele Login_GetKatalogUzivatele(Terminal terminal, ref StatusObject so);
    }
}
