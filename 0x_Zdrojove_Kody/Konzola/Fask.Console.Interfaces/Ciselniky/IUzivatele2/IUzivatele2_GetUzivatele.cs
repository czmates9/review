using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_GetUzivatele : IUzivatele2
    {
        /// <summary>
        /// Vraci seznam Uzivatelu.
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Uzivatele GetUzivatele();
    }
}
