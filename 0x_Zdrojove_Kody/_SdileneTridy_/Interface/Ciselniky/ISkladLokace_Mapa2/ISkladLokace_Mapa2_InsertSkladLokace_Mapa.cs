using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_InsertSkladLokace_Mapa : ISkladLokace_Mapa2
    {
        /// <summary>
        /// Vloží do DB nový záznam lokace.
        /// </summary>
        /// <param name="row">Lokace, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row);
   
    }
}
