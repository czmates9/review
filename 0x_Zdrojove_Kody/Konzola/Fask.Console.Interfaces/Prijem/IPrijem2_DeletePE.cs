using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prijem
{
   public interface IPrijem2_DeletePE : IPrijem2
    {
        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeletePE(int ID);

        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeletePE(Fask.Interfaces.DataSets.Prijem.CZMST_PERow PERow);
    }
}
