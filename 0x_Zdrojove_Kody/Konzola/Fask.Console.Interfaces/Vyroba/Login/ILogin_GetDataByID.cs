using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Login
{
    public interface ILogin_GetDataByID : ILogin
    {
        //FillLogin
        Fask.Console.Interfaces.DataSets.Vyroba.LoginsDataTable GetDataByID(string ID);

    }
}
