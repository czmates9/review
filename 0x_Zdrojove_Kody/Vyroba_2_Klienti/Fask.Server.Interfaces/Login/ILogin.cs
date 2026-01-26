using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Login
{
    public interface ILogin
    {
        bool Login_OnlineLogin(User uzivatel, Terminal terminal, string hash, ref int uzivatelID);

        string Login_GetHash(User uzivatel, Terminal terminal);

        Fask.DataSets.Uzivatele Login_GetKatalogUzivatele(Terminal terminal, ref StatusObject so);
    }
}
