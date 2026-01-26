using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_InsertUzivatelAOpravneni : IKonzola2
    {
        /// <summary>
        /// Vloží nového uživatele a jeho oprávnění do DB.
        /// </summary>
        /// <param name="loginRow">Nový uživatel.</param>
        /// <param name="authRow">Oprávnění uživatele.</param>
        /// <returns></returns>
        bool InsertUzivatelAOpravneni(Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginRow, Fask.Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow authRow);

    }
}
