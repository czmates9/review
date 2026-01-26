using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_GetFiltrovaneUzivatele : IUzivatele2
    {

        /// <summary>
        /// Vraci filtrovane uzivatele.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Console.Interfaces.DataSets.Uzivatele GetFiltrovaneUzivatele(Fask.Console.Interfaces.Classes.UzivateleListFiltr filtr);

    }
}
