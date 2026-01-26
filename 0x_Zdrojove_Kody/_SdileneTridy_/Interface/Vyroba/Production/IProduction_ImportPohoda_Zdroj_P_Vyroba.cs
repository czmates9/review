using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_ImportPohoda_Zdroj_P_Vyroba : IProduction
    {
        string Production_ImportPohoda_Zdroj_P_Vyroba(int countEntries, string SKL_ID, string userID);
    }
}
