using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GetHlavickaByCountEntries
    {
        /// <summary>
        /// vraci hlavicku podle CountEntries (nevraci se podle SOPNUMBE, kdyby nahodou doslo k stornu davky a byla tam vicekrat ...)
        /// </summary>
        /// <param name="sopnumbe"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vydej.HlavickyRow GetHlavickaByCountEntries(int CountEntries);

    }
}
