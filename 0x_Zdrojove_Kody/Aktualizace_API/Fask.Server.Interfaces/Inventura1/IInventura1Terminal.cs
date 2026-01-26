using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Inventura1
{
    public interface IInventura1Terminal
    {
        bool Inventura_FillInventuraDB(int countentries, byte idterminal, Fask.DataSets.Inventura1 inventura);
        Fask.DataSets.Inventura1 Inventura_GetInventuraDB(int countentries, byte idterminal);
   
    }
}
