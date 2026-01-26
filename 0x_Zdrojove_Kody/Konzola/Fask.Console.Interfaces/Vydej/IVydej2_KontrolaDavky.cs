using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_KontrolaDavky : IVydej2
    {
        /// <summary>
        /// Online kontrola davky
        /// </summary>
        /// <param name="CountEntries"> čislo davky</param>
        /// <returns> bool stav</returns>
        Fask.Interfaces.DataSets.Vydej KontrolaDavky(int CountEntries);
    }
}
