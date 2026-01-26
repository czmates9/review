using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_ImportOdberatel : IOdberatele2
    {
        string ImportOdberatel();
        List<Tuple<string, string, bool>> GetOdberateleTableInfo();
    }
}
