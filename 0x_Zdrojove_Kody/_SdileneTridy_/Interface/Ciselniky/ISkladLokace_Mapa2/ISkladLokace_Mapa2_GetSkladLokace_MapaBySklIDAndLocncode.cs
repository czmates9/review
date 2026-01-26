using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode : ISkladLokace_Mapa2
    {
        /// <summary>
        /// Vraci lokaci podle skl_id a locncode.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow GetSkladLokace_MapaBySklIDAndLocncode(string skl_id, string locncode);

    }
}
