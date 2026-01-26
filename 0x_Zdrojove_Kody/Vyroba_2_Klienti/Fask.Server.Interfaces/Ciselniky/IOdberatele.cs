using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;

namespace Fask.Server.Interfaces.Ciselniky
{
    public interface IOdberatele
    {
        Fask.DataSets.Odberatele KatalogOdberatele(Terminal terminal, Sklad sklad);


        StatusInfo KatalogOdberateleExport(Terminal terminal, ref StatusObject so);
    }
}
