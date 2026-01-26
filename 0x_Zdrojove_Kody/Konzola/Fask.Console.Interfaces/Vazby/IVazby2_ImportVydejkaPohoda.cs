using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Import_TP2PS : IVazby2
    {
        void Import_TP2PS(int countEntries, string SKL_ID, string userID);
    }
}
