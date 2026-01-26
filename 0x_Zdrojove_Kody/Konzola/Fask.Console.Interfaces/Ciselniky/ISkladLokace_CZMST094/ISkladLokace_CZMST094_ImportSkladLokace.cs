using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_ImportSkladLokace : ISkladLokace_CZMST094
    {
        string ImportSkladLokace_CZMST094();
        List<Tuple<string, string, bool>> GetSkladLokace_CZMST094TableInfo();

    }
}


