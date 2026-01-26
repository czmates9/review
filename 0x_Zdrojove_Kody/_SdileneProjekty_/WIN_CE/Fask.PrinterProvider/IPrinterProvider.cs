using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.PrinterProvider
{
    public interface IPrinterProvider
    {
        //string Address { get; set; }
        //string Name { get; set; }
        //int Timeout { get; set; }
        //string Encoding { get; set; }

        string ConfigFilename { get; set; }

        void InitializePrinter();
        void TerminatePrinter();

        /// <summary>
        /// Seznam sablon pro nastaveni tisku
        /// </summary>
        /// <returns>Seznam nazvu souboru pro templates</returns>
        List<string> GetTemplatesList();

        /// <summary>
        /// Nastavena tiskarna providera
        /// </summary>
        /// <returns>Nazev tiskarny</returns>
        string GetPrinterName();

        UserControl ConfigControlPrinter { get; }

        bool Print(Dictionary<string, string> data, string template, int pocet);

        bool Print(
            Dictionary<string, string> dataHeader,
            System.Collections.Generic.List<Dictionary<string, string>> dataRowList,
            Dictionary<string, string> dataFooter,
            string templateHeader,
            string templateRow,
            string templateFooter,
            int pocet
            );

        //bool PrintText(string textToPrint, string template, int pocet);
        bool Print(string textToPrint, int pocet);
    }
}
