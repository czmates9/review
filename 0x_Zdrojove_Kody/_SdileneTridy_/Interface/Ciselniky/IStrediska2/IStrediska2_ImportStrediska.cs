using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_ImportStrediska : IStrediska2
    {
        string ImportStrediska();
        List<Tuple<string, string, bool>> GetStrediskaTableInfo();
    }
}
