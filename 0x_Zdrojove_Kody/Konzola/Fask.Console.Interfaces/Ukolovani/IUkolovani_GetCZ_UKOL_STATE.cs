using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_GetCZ_UKOL_STATE : IUkolovani
    {

        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_STATEDataTable GetCZ_UKOL_STATE();

    }
}
