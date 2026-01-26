using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_GetDataBySOUBEHGUID : IProduction
    {
        Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID( Guid SOUBEHGUID);
    }
}
