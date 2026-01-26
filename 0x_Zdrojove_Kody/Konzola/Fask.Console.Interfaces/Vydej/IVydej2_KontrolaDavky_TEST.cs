using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_KontrolaDavky_TEST : IVydej2
    {
        /// <summary>
        /// Online kontrola davky
        /// </summary>
        /// <param name="CountEntries"> čislo davky</param>
        /// <returns> bool stav</returns>
        void KontrolaDavky_TEST(string CountEntries, out Fask.POHODA.Disponibility.ValidateData DTOut, out Fask.POHODA.Disponibility.ValidateData dsDisp, Fask.Interfaces.Classes.TypZdrojeDat zdroj);
    }
}
