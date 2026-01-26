using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
   public interface IPrijem2_UpdatePI : IPrijem2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdatePI(Fask.Interfaces.DataSets.Prijem.CZMST_PIRow PERow);
    }
}
