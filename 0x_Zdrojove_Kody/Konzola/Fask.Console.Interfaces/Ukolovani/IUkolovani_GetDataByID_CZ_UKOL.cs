using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{

    public interface IUkolovani_GetDataByID_CZ_UKOL : IUkolovani
    {

        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLDataTable GetDataByID(int ID);
    }
}
