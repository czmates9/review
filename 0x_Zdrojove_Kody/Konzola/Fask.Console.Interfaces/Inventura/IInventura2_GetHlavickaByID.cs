using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_GetHlavickaByID : IInventura2
    {
        /// <summary>
        /// Vraci cislo davky podle ID
        /// </summary>
        /// <param name="countentries"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Inventura.CZMST_I1HRow GetHlavickaByID(int countentries);
    }
}
