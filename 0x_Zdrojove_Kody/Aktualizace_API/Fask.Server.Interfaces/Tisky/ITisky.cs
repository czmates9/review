using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Tisky
{
    public interface ITisky : IWebModule
    {
        bool Vydej_Tisk_Soupis(Fask.DataSets.Vydej vydejDS);
        bool Prijem_Tisk_Soupis(Fask.DataSets.Prijem prijemDS);
    }
}
