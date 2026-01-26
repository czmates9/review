using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.TypyDokladu
{
    public interface ITypyDokladu2_GetFiltrovaneData : ITypyDokladu2
    {
        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.TypyDokladu GetFiltrovaneData(Fask.Interfaces.Filtry.TypDokladuList filtr);
    }
}
