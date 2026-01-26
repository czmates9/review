using MST_Print_Server_ZPL_Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Tisky
{
    public interface ITisky2_EtiketaTisk : ITisky2
    {
        bool EtiketaTisk( int terminalID, string templateName, TiskParams printerParams, Dictionary<string, string> data, int pocetVytisku);
    }
}
