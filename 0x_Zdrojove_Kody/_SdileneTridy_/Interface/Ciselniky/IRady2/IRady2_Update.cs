using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Rady
{
    public interface IRady2_Update : IRady2
    {
        bool Update(Fask.Interfaces.DataSets.Rady.FASK_RADYRow zboziRow);
    }
}
