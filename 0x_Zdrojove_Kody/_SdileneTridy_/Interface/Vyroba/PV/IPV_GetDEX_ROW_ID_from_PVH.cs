using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_GetDEX_ROW_ID_from_PVH : IPV
    {

        int? GetDEX_ROW_ID_from_PVH(string SOPNUMBE, Fask.Interfaces.Classes.ZaplanovanyDoklad zaplanovanyDoklad);
    }
}
