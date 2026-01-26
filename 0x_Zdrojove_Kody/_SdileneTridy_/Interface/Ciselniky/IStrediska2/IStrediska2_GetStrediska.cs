using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_GetStrediska : IStrediska2
    {
        /// <summary>
        /// Vraci seznam Stredisek.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Strediska GetStrediska();
    }
}
