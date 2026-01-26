using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface IMeny
    {
        Fask.DataSets.Meny KatalogMen(Terminal terminal);

        StatusInfo KatalogMenExport(Terminal terminal, ref StatusObject so);
    }
}
