using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.PrinterFactory
{
    public class Printer
    {
        //public PrinterTypes PrinterType { get; set; }
        public string PrinterType { get; set; }
        public string Library { get; set; }
        public string Config { get; set; }

        /// <summary>
        /// Objekt komunikace s tiskarnou ...
        /// </summary>
        public Fask.PrinterProvider.IPrinterProvider PrinterProvider { get; set; }
    }
}
