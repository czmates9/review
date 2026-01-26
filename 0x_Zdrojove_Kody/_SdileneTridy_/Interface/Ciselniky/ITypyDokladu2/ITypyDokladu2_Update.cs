using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.TypyDokladu
{
    public interface ITypyDokladu2_Update : ITypyDokladu2
    {
        bool Update(Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row zboziRow);
    }
}
