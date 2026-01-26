using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_UpdatePriorityDavka_byPriority
    {
        /// <summary>
        /// Zmeni prioritu davky podle cisla davky (kdyby byla napr. stornovana, ...).
        /// Zmena se uskutecni pouze v pripade, ze davka ma nastaven priznak CZ_Doslo = 0.
        /// </summary>
        /// <param name="countentries">Cislo davky.</param>
        /// <param name="priority">Priorita.</param>
        /// <returns></returns>
        bool UpdatePriorityDavka(int countentries, byte priority);

    }
}
