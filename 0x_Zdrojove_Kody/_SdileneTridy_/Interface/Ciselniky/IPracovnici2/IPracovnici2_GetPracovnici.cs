using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_GetPracovnici : IPracovnici2
    {
        /// <summary>
        /// Vraci seznam Pracovniku.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Pracovnici GetPracovnici();
    }
}
