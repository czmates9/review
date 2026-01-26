using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_GetFiltrovanePracovniky : IPracovnici2
    {
        /// <summary>
        /// Vraci filtrovane pracovniky.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Pracovnici GetFiltrovanePracovniky(Fask.Interfaces.Filtry.PracovniciListFiltr filtr);

    }
}
