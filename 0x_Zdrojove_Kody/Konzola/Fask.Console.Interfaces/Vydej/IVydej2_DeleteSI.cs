using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
   public interface IVydej2_DeleteSI : IVydej2
    {
        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeleteSI(int ID);

        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeleteSI(Fask.Interfaces.DataSets.Vydej.CZMST_SIRow SIRow);
    }
}
