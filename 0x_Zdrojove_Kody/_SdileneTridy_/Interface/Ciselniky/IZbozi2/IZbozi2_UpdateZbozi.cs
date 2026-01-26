using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_UpdateZbozi : IZbozi2
    {
        /// <summary>
        /// Aktualizuje informace o zbozi.
        /// </summary>
        /// <param name="zdrojRow">Zbozi, ktere se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow);
    }
}
