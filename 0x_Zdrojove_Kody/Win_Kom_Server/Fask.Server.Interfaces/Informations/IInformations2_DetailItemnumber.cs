using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Informations
{
    public interface IInformations2_DetailItemnumber : IMST
    {
        DataSet DetailItemnumber(string itemnumber, string doklad);
    }
}
