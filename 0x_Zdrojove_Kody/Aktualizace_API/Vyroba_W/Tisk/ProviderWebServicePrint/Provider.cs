using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data;

using Fask.Vyroba_W.ServerAccess;

namespace Fask.PrinterProviderWebService
{
    public class Provider //: Fask.PrinterProvider.IPrinterProvider
    {
        /// <summary>
        /// Nazev tiskarny na kterou se ma tisknout
        /// </summary>
        public string PrinterName = string.Empty;
        /// <summary>
        /// Zda se jedna o jednosmerny tisk (default false->obousmerna komunikace)
        /// </summary>
        public bool OneWayPrint = false;

        private Fask.Vyroba_W._WebRefernces_Globals.WebServiceTiskSession printServerTiskService = null;

        public void InitializePrinter(string printerServiceAddress, int printerServiceTimeout)
        {
            string surl = string.Empty;
            if (!printerServiceAddress.StartsWith("http://"))
                surl += "http://";
            surl += printerServiceAddress;
            if (!surl.EndsWith("/"))
                surl += "/";

            if (printServerTiskService == null)
				printServerTiskService = new Fask.Vyroba_W._WebRefernces_Globals.WebServiceTiskSession();
            printServerTiskService.Url = surl + "Tisk.asmx";
            printServerTiskService.Timeout = printerServiceTimeout;
			printServerTiskService.UpdateWebServiceCredentials();
        }

        #region IPrinterProvider Members

        private Fask.Vyroba_W.WebServiceTisk.TiskParams prepareTiskParams()
        {
            Fask.Vyroba_W.WebServiceTisk.TiskParams tiskParams = new Fask.Vyroba_W.WebServiceTisk.TiskParams();
            tiskParams.CONFIG_NAME = PrinterName; // to je vse ???
            return tiskParams;
        }

        private Fask.Vyroba_W.WebServiceTisk.DSValues prepareTiskValues(Dictionary<string, string> data)
        {
            Fask.Vyroba_W.WebServiceTisk.DSValues tiskValues = new Fask.Vyroba_W.WebServiceTisk.DSValues();
            tiskValues.Values.BeginLoadData();
            foreach (var item in data)
            {
                tiskValues.Values.AddValuesRow(item.Key, item.Value);
            }
            tiskValues.Values.EndLoadData();
            tiskValues.AcceptChanges();
            return tiskValues;
        }

        public bool Print(Dictionary<string, string> data, string template, int pocet)
        {
            if (!OneWayPrint)
                return printServerTiskService.Etiketa(Fask.Vyroba_W.Settings.TerminalID, template, prepareTiskParams(), prepareTiskValues(data), pocet);
            else
                printServerTiskService.EtiketaBezNavratu(Fask.Vyroba_W.Settings.TerminalID, template, prepareTiskParams(), prepareTiskValues(data), pocet);

            return true;
        }

        /// <summary>
        /// Tisk soupisu.
        /// </summary>
        /// <param name="dataHeader">Data v hlavicky.</param>
        /// <param name="dataRowList">Data radku.</param>
        /// <param name="dataFooter">Data paticky.</param>
        /// <param name="templateHeader">Sablona hlavicky.</param>
        /// <param name="templateRow">Sablona radku.</param>
        /// <param name="templateFooter">Sablona paticky.</param>
        /// <param name="pocet">Pocet vytisku.</param>
        /// <returns>True pokud tisk probehl spravne jinak False.</returns>
        public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            List<Fask.Vyroba_W.WebServiceTisk.DSValues> dataRows = new List<Fask.Vyroba_W.WebServiceTisk.DSValues>();

            // pridani veskerych zaznamu do datarows
            foreach (var item in dataRowList)
            {
                dataRows.Add(prepareTiskValues(item));
            }

            if (!OneWayPrint)
                return printServerTiskService.Soupis(
                    Fask.Vyroba_W.Settings.TerminalID,
                    prepareTiskParams(),
                    prepareTiskValues(dataHeader),
                    dataRows.ToArray(),
                    prepareTiskValues(dataFooter),
                    templateHeader,
                    templateRow,
                    templateFooter,
                    pocet);
            else
                printServerTiskService.SoupisBezNavratu(
                    Fask.Vyroba_W.Settings.TerminalID,
                    prepareTiskParams(),
                    prepareTiskValues(dataHeader),
                    dataRows.ToArray(),
                    prepareTiskValues(dataFooter),
                    templateHeader,
                    templateRow,
                    templateFooter,
                    pocet);
            return true;
        }


        public bool Print(string textToPrint, int pocet)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
