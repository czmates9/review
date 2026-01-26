using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Vyroba.Login
{
    public interface ILogin_Insert : ILogin
    {
        void Insert(string id, string firstname, string surname, string psswd, byte VS);
    }
}
