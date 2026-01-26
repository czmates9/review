using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources_ImportVydejkaPohoda : IProductionSources
    {

        Fask.Interfaces.Classes.StatusInfo_Dispo ImportVydejkaPohoda(int countEntries, string SKL_ID, string userID, bool smazat_PS, bool PreskocDisp);
    }
}
