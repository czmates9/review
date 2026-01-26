using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Terminal
{
    public interface ITerminal_GetTerminalAll : ITerminal
    {
        Fask.Interfaces.DataSets.Terminal GetTerminalAll();
    }
}
