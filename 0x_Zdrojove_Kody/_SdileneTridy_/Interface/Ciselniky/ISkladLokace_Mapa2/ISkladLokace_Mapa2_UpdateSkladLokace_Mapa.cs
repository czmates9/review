using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_UpdateSkladLokace_Mapa : ISkladLokace_Mapa2
    {
        /// <summary>
        /// Aktualizuje informace o lokaci.
        /// </summary>
        /// <param name="row">Lokace, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row);

    }
}
