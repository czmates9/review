using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.TypyDokladu
{
    public interface ITypyDokladu2_Insert : ITypyDokladu2
    {
        bool Insert(Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row zboziRow);
    }
}
