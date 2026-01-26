using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa : ISkladLokace_Mapa2
    {
                /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_Mapa(Fask.Interfaces.Filtry.SkladLokaceMapaListFiltr filtr);

    }
}
