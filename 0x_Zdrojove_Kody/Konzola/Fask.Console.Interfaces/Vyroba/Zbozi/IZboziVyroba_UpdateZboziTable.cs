using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Zbozi
{
    public interface IZboziVyroba_UpdateZboziTable : IZboziVyroba
    {
        int UpdateZboziTable(Fask.Console.Interfaces.DataSets.Vyroba.FASK_CONS_095DataTable dt);
    }
}
