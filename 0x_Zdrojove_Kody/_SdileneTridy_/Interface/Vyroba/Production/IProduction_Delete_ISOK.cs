using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Production
{
    public interface IProduction_Delete_ISOK : IProduction
    {
        void Production_Delete_ISOK(Fask.Interfaces.DataSets.Vyroba.Production_OdvodRow Row);
    }    
}
