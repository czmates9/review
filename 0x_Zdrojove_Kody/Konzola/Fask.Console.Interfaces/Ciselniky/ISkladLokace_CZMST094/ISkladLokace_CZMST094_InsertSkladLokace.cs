using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_InsertSkladLokace : ISkladLokace_CZMST094
    {

        /// <summary>
        /// Vloží do DB nový záznam lokace.
        /// </summary>
        /// <param name="row">Lokace, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_CZMST094(Fask.Interfaces.DataSets.SkladLokace.CZMST094Row row);

    }
}


