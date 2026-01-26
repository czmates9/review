using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetHlavicky : IInventura2
    {
        /// <summary>
        /// Vraci seznam inventurnich davek v tabulce CZMST_I1H
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Inventura GetHlavicky();
    }
}
