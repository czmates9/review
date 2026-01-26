using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface IStrediska
    {
        Fask.DataSets.Strediska KatalogStrediska(Terminal terminal, Sklad sklad);

        StatusInfo KatalogStrediskaExport(Terminal terminal, ref StatusObject so);
    }
}
