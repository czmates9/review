using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_GetFiltrovaneZbozi : IZbozi2
    {
        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Zbozi GetFiltrovaneZbozi(Fask.Interfaces.Filtry.ZboziListFiltr filtr);
    }
}