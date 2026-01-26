using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface ITypDokladu
    {
        Fask.DataSets.TypDokladu KatalogTypDokladu(Terminal terminal, Sklad sklad);
    }
}
