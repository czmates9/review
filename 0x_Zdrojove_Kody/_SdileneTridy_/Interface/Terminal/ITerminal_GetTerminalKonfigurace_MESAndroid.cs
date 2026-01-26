using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Terminal
{
    public interface ITerminal_GetTerminalKonfigurace_MESAndroid : ITerminal
    {
        string GetTerminalKonfigurace_MESAndroid(int TID,out string Konfigurace);
    }
}
