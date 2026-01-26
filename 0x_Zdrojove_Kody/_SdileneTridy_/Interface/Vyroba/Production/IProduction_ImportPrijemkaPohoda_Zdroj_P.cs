using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_ImportPrijemkaPohoda_Zdroj_P : IProduction
    {
        string Production_ImportPrijemkaPohoda_Zdroj_P(int countEntries, string userID, string SKL_ID);
    }
}
