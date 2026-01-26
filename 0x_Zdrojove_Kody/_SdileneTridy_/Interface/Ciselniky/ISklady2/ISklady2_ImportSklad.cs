using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_ImportSklad : ISklady2
    {
        string ImportSkladu();
        List<Tuple<string, string, bool>> GetSkladyTableInfo();
    }
}
