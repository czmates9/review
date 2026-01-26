using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface ISklady
    {
        Fask.DataSets.Sklady KatalogSklady(Terminal terminal);

        StatusInfo KatalogSkladyExport(Terminal terminal, ref StatusObject so);
    }
}
