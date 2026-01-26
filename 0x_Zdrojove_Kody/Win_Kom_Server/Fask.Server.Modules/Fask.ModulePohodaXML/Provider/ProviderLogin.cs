using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Fask.SQL
{
    public partial class Provider : Fask.Server.Interfaces.Login.ILogin
    {
        #region ILogin Members

		/// <summary>
		/// Metoda pro online oveřeni uživatele
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="hash">Hash</param>
		/// <param name="uzivatelID">Reference na ID uživatele</param>
		/// <returns>True-OK, False- chyba</returns>
        public bool Login_OnlineLogin(Fask.Server.Interfaces.Classes.User uzivatel, Fask.Server.Interfaces.Classes.Terminal terminal, string hash, ref int uzivatelID)
        {
			throw new NotImplementedException();
        }

		/// <summary>
		/// Metoda pro dotaženi uživatele z Databaze
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Uzivatele naplnen uživatelama</returns>
        public Fask.DataSets.Uzivatele Login_GetKatalogUzivatele(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
			throw new NotImplementedException();

			
        }

		/// <summary>
		/// Metoda pro vypočet HASH
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Vypočitany HASH</returns>
        public string Login_GetHash(Fask.Server.Interfaces.Classes.User uzivatel, Fask.Server.Interfaces.Classes.Terminal terminal)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
