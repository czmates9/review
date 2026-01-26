using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_GetDataByUkolIDAndUserID_CZ_UKOL_UZIV : IUkolovani
    {

        Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable GetDataByUkolIDAndUserID(int UkolID, int UserID);

    }
}
