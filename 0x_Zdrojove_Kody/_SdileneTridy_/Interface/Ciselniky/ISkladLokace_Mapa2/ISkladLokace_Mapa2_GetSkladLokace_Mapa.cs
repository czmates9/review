using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_GetSkladLokace_Mapa : ISkladLokace_Mapa2
    {

        /// <summary>
        /// Vraci mapu skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_Mapa();
    }
}
