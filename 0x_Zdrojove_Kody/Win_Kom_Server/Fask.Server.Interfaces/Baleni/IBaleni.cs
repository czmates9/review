using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Baleni
{
    public interface IBaleni
    {
        DataSet GetBaleniData(string objednavkacislo, out int balikcislo);
        StatusBaleni BaleniDataCommit(string userid, byte terminalid, string objednavkacislo, int balikcislo, DateTime printedtime);
    }
}
