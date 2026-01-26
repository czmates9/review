using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Vloží do DB nový záznam typu lokace.
        /// </summary>
        /// <param name="row">Lokace, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row);

    }
}
