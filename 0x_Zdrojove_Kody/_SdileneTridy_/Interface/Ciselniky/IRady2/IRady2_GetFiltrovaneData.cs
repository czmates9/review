using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Rady
{
    public interface IRady2_GetFiltrovaneData : IRady2
    {
        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Rady GetFiltrovaneData(Filtry.RadyListFiltr filtr);
    }
}
