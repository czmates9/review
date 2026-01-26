using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Prodej
{
    public interface IProdej2_ImportDavka : IProdej2
    {
        StatusInfo ImportDavka(Objednavka objednavka, Sklad sklad, bool Grupuj);
    }
}
