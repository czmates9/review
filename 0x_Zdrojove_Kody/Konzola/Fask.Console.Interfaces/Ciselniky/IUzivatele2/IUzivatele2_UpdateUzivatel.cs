using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_UpdateUzivatel : IUzivatele2
    {
        /// <summary>
        /// Aktualizuje informace o Uzivateli.
        /// </summary>
        /// <param name="zdrojRow">Uzivatel, ktery se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow);
    }
}
