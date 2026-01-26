using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
   public interface IVydej2_DeleteSE : IVydej2
    {
        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeleteSE(int ID);
    }
}
