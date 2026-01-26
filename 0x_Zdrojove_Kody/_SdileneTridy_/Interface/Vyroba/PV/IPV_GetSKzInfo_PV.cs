using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_GetSKzInfo_PV : IPV
    {

        void GetSKzInfo_PV(string ITEMNMBR, out Fask.Interfaces.DataSets.Vyroba.Pohoda_SKz_VPPRow p);
    }
}
