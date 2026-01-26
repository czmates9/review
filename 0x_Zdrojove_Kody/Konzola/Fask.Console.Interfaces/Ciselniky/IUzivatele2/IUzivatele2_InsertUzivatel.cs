using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_InsertUzivatel : IUzivatele2
    {

        /// <summary>
        /// Vloží do DB nový záznam uzivatele.
        /// </summary>
        /// <param name="zdrojRow">Uzivatel, který se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow);
    }
}
