using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Insert_PVH : IPV
    {

        bool Insert_PVH(string SOPNUMBE, string SOPTYPE, string SOPDESC, string BarcodeH, byte Active, Fask.Interfaces.Classes.ZaplanovanyDoklad zaplanovanyDoklad);
    }
}
