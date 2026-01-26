using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_DeleteSkladLokace_Mapa : ISkladLokace_Mapa2
    {
        /// <summary>
        /// Smaže lokaci.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_Mapa(string skl_id, string locncode);
    }
}
