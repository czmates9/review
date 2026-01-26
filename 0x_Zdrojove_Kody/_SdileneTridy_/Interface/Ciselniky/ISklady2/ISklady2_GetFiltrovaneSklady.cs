using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_GetFiltrovaneSklady : ISklady2
    {
        /// <summary>
        /// Vraci filtrovane sklady.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Sklady GetFiltrovaneSklady(Fask.Interfaces.Filtry.SkladyListFiltr filtr);

    }
}
