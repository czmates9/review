using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MST_Print_Server_ZPL_Printing;

namespace Fask.WEBAPI.API_BusinessObjects
{
    public class Tisk_Etiketa
    {
        public int TerminalID;
        public string TemplateName;
        public TiskParams PrinterParams;
        public Dictionary<string, string> Data;
        public int PocetVytisku;
    }
}
