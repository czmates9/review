using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Vratky
{
    public interface IVratka
    {
        List<ItemCip> ItemsGetBySerltnum(string sklad, List<string> serltnum, out string message);
        bool ItemsSetBySerltnum(string sklad, List<ItemCip> itemchips, out string message);
    }
}
