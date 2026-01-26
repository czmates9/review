using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.PrinterFactory
{
    public class ModuleToPrint
    {
        public PrinterModules PrinterModule { get; set; }
        public string PrinterType { get; set; }
        public string Template { get; set; }

        public ModuleToPrint(PrinterModules pModule, string pType, string template)
        {
            this.PrinterModule = pModule;
            this.PrinterType = pType;
            this.Template = template;
        }
    }
}
