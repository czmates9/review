using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Login
{
    public interface ILoginTerminal
    {

        bool Login_GetKatalogUzivatele(byte terminal, Fask.DataSets.Uzivatele uzivatele);

    }
}
