using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface IZbozi
    {
        Fask.DataSets.Zbozi KatalogZbozi(Terminal terminal, Sklad sklad, ref StatusObject so);

        StatusInfo KatalogZboziExport(Terminal terminal, Sklad sklad, ref StatusObject so);
    }
}
