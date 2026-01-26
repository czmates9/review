using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_GetSklady : ISklady2
    {
        /// <summary>
        /// Vraci seznam skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Sklady GetSklady();
    }
}
