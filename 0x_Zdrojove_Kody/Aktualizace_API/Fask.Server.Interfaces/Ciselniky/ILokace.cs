using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface ILokace
    {
        Fask.DataSets.Lokace KatalogLokace(Terminal terminal, Sklad sklad);
    }
}
