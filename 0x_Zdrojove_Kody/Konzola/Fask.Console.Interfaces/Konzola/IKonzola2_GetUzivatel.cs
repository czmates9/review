using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_GetUzivatel : IKonzola2
    {
        /// <summary>
        /// Vrací informace o uživateli, který se přihlašuje do aplikace.
        /// </summary>
        /// <param name="userid">ID uživatele</param>
        /// <returns>Uživatel</returns>
        Fask.Console.Interfaces.DataSets.Konzola.FASK_LoginsRow GetUzivatel(string userid);
    }
}
