using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Login
{
    public interface ILogin_Update_Row: ILogin
    {
        int Update_Row(Fask.Console.Interfaces.DataSets.Vyroba.LoginsRow row);
    }
}
