using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Rady
{
    public interface IRady2_Insert : IRady2
    {
        bool Insert(Fask.Interfaces.DataSets.Rady.FASK_RADYRow zboziRow);
    }
}
