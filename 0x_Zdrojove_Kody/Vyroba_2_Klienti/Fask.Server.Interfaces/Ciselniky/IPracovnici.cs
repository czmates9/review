using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface IPracovnici
    {
        Fask.DataSets.Pracovnici KatalogPracovnici(Terminal terminal, Sklad sklad);


        StatusInfo KatalogPracovniciExport(Terminal terminal, ref StatusObject so);
    }
}
