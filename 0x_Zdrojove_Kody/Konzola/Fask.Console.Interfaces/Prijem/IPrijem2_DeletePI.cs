using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
   public interface IPrijem2_DeletePI : IPrijem2
    {
        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeletePI(int ID);

        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeletePI(Fask.Interfaces.DataSets.Prijem.CZMST_PIRow PIRow);
    }
}
