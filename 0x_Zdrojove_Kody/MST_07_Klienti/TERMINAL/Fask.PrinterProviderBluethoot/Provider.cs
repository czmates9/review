using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.PrinterProviderBluetooth
{
    public class Provider : Fask.PrinterProvider.IPrinterProvider
    {
        #region IPrinterProvider Members

        public string Address { get; set; }

        public string Name{ get; set; }

        public int Timeout { get; set; }

        public string Encoding{ get; set; }

        public void InitializePrinter()
        {
        }

        public void TerminatePrinter()
        {
        }

        ConfigControl configControl = null;
        public UserControl ConfigControlPrinter
        {
            get
            {
                if (configControl == null)
                {
                    configControl = new ConfigControl();
                }
                return configControl;
            }
        }


        public bool Print(Dictionary<string, string> data, string template, int pocet)
        {
            throw new NotImplementedException();
        }

        public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            throw new NotImplementedException();
        }

        public bool PrintHeader(Dictionary<string, string> dataHeader, string template, int pocet)
        {
            throw new NotImplementedException();
        }

        public bool PrintFooter(Dictionary<string, string> dataFooter, string template, int pocet)
        {
            throw new NotImplementedException();
        }

        public bool PrintRow(Dictionary<string, string> dataRow, string template, int pocet)
        {
            throw new NotImplementedException();
        }

        public bool PrintText(string text, string template, int pocet)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
