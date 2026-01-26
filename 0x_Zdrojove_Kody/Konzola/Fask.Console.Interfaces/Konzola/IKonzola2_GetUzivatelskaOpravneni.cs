using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_GetUzivatelskaOpravneni : IKonzola2
    {

        /// <summary>
        /// Vrací uživatelská oprávnění vybraného uživatele.
        /// </summary>
        /// <param name="userid">ID uživatele.</param>
        /// <returns>Oprávnění uživatele.</returns>
        Fask.Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow GetUzivatelskaOpravneni(string userid);
    }
}
