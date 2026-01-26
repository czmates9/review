using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Login
{
    public interface ILogin_FillLogin : ILogin
    {
        //FillLogin
        void FillLogin(Fask.Console.Interfaces.DataSets.Vyroba dt);

    }
}
