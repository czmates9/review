using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_DeleteUzivatelAOpravneni : IKonzola2
    {
        /// <summary>
        /// Smaže uživatele a jeho oprávnění podle zadaného ID.
        /// </summary>
        /// <param name="id">ID uživatele</param>
        /// <returns></returns>
        bool DeleteUzivatelAOpravneni(string id);

    }
}
