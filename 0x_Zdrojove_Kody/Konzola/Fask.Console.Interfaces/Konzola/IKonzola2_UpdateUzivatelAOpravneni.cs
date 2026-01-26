using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_UpdateUzivatelAOpravneni : IKonzola2
    {
        ///// <summary>
        ///// Edituje uživatele a oprávnění podle zadaného ID.
        ///// </summary>
        ///// <param name="id">ID uživatele</param>
        ///// <returns></returns>

        /// <summary>
        /// Edituje uživatele a oprávnění.
        /// </summary>
        /// <param name="loginRow">Uživatel, ktery se bude aktualizovat.</param>
        /// <param name="authRow">Oprávnění uživatele.</param>
        /// <returns></returns>
        bool UpdateUzivatelAOpravneni(Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginRow, Fask.Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow authRow);
    }
}
