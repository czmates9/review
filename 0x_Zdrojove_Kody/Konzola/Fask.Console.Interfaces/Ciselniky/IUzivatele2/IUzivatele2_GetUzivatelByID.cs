using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_GetUzivatelByID : IUzivatele2
    {
        /// <summary>
        /// Vraci uzivatele podle ID.
        /// </summary>
        /// <param name="id">ID uzivatele.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow GetUzivatelByID(int id);
    }
}
