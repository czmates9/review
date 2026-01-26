using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_UpdatePriorityDavka_byUserID
    {
        /// <summary>
        /// Zmeni prioritu davky na osobu podle cisla davky (kdyby byla napr. stornovana, ...).
        /// Zmena se uskutecni pouze v pripade, ze davka ma nastaven priznak CZ_Doslo = 0.
        /// </summary>
        /// <param name="countentries">Cislo davky.</param>
        /// <param name="userID">ID uzivatele. Pokud je null, nastavi se na null.</param>
        /// <returns></returns>
        bool UpdatePriorityDavka(int countentries, int? userID);
    }
}
