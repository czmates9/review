using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data;

namespace Fask.PrinterProviderWebService
{
    public class Provider : Fask.PrinterProvider.IPrinterProvider
    {
        private PrintServerTiskService.Tisk printServerTiskService = null;
        private PrintServerTestService.Test printServerTestService = null;
		private PrintServerTiskTestService.TiskTest printServerTiskTestService = null;
        private DSConfigPrinterProviderWebService configuration = null;

        private string _ConfigFilename = string.Empty;
        public string ConfigFilename
        {
            get
            {
                if (String.IsNullOrEmpty(_ConfigFilename))
                    return Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase, ".xml");
                else
                    return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), _ConfigFilename);
            }
            set
            {
                _ConfigFilename = value;
            }
        }

        private void ConfigurationLoad()
        {
            if (configuration == null)
            {
                configuration = new DSConfigPrinterProviderWebService();
                if (File.Exists(ConfigFilename))
                {
                    try { configuration.ReadXml(ConfigFilename); }
                    catch (Exception ex) 
                    {
                        string err = ex.Message; 
                    }
                }

                if (configuration.Params.Count == 0)
                {
                    configuration.Params.AddParamsRow(
                        @"http://192.168.1.101/MST_Win_Kom_Server_7",
                        string.Empty,
                        10000,
                        string.Empty,
                        false
                        );
                    configuration.AcceptChanges();
                    ConfigurationSave();
                }
            }
        }

        private void ConfigurationSave()
        {
            if (configuration == null)
            {
                configuration = new DSConfigPrinterProviderWebService();
            }

            configuration.WriteXml(ConfigFilename);
        }

        public DSConfigPrinterProviderWebService.ParamsRow Params
        {
            get
            {
                ConfigurationLoad();
                return configuration.Params[0]; //vzdy ocekavam ze tam bude prave jeden radek !!!
            }
        }

        #region IPrinterProvider Members

        ConfigControl configControl = null;
        public UserControl ConfigControlPrinter
        {
            get
            {
                if (configControl == null || configControl.IsDisposed)
                {
                    configControl = new ConfigControl();
                    configControl.printServerTestService = printServerTestService;
                    configControl.printServerTiskService = printServerTiskService;
					configControl.printServerTiskTestService = printServerTiskTestService;
                    configControl.textBoxAddress.Text = Params.Address;
                    configControl.textBoxName.Text = Params.Name;
                    configControl.textBoxTimeout.Text = Params.Timeout.ToString();
                    configControl.checkBoxOneWayPrint.Checked = Params.OneWayPrint;
                    configControl.comboBoxPrinterName.Text = Params.PrinterName;
                    configControl.buttonSave.Click += new EventHandler(buttonSave_Click);
                }
                return configControl;
            }
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            this.TerminatePrinter();

            Button bsave = sender as Button;
            Params.Address = configControl.textBoxAddress.Text;
            Params.Name = configControl.textBoxName.Text;
            try
            {
                Params.Timeout = int.Parse(configControl.textBoxTimeout.Text);
                configControl.textBoxTimeout.BackColor = SystemColors.Window;
            }
            catch
            {
                configControl.textBoxTimeout.BackColor = Color.Red;
                return;
            }
            Params.OneWayPrint = configControl.checkBoxOneWayPrint.Checked;
            Params.PrinterName = configControl.comboBoxPrinterName.Text;

            this.InitializePrinter();

            ConfigurationSave();
            //bsave.Enabled = false;
        }

        public void InitializePrinter()
        {
			// Tady je chyba když je na IIS server nastaven na HTTPS 
            string surl = string.Empty;
            if (!Params.Address.StartsWith("http://"))
                surl += "http://";
            surl += Params.Address;
            if (!surl.EndsWith("/"))
                surl += "/";

            if (printServerTiskService == null)
                printServerTiskService = new Fask.PrinterProviderWebService.PrintServerTiskService.Tisk();
            printServerTiskService.Url = surl + "Tisk.asmx";
            printServerTiskService.Timeout = Params.Timeout;

            if (printServerTestService == null)
                printServerTestService = new Fask.PrinterProviderWebService.PrintServerTestService.Test();
            printServerTestService.Url = surl + "Test.asmx";
            printServerTestService.Timeout = Params.Timeout;

			if (printServerTiskTestService == null)
				printServerTiskTestService = new Fask.PrinterProviderWebService.PrintServerTiskTestService.TiskTest();
			printServerTiskTestService.Url = surl + "TiskTest.asmx";
			printServerTiskTestService.Timeout = Params.Timeout;

        }

        public void TerminatePrinter()
        {
            //try { printServerTestService.Dispose(); }
            //catch { }
            //try { printServerTiskService.Dispose(); }
            //catch { }
            //printServerTestService = null;
            //printServerTiskService = null;
        }

        public List<string> GetTemplatesList()
        {
            List<string> tempaltesList = new List<string>();
            try
            {
                DataTable dtLabels = printServerTestService.Labels();
                foreach (DataRow item in dtLabels.Rows)
                {
                    tempaltesList.Add((string)item["Name"]);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            
            return tempaltesList;
        }

        public string GetPrinterName()
        {
            return Params.PrinterName;
        }

        private PrintServerTiskService.TiskParams prepareTiskParams()
        {
            PrintServerTiskService.TiskParams tiskParams = new Fask.PrinterProviderWebService.PrintServerTiskService.TiskParams();
            tiskParams.CONFIG_NAME = Params.PrinterName; // to je vse ???
            return tiskParams;
        }

        private PrintServerTiskService.DSValues prepareTiskValues(Dictionary<string, string> data)
        {
            PrintServerTiskService.DSValues tiskValues = new Fask.PrinterProviderWebService.PrintServerTiskService.DSValues();
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
            if (!Params.OneWayPrint)
                return printServerTiskService.Etiketa(0, template, prepareTiskParams(), prepareTiskValues(data), pocet);
            else
                printServerTiskService.EtiketaBezNavratu(0, template, prepareTiskParams(), prepareTiskValues(data), pocet);

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
            List<PrintServerTiskService.DSValues> dataRows = new List<Fask.PrinterProviderWebService.PrintServerTiskService.DSValues>();

            // pridani veskerych zaznamu do datarows
            foreach (var item in dataRowList)
            {
                dataRows.Add(prepareTiskValues(item));
            }

            if (!Params.OneWayPrint)
                return printServerTiskService.Soupis(
                    0,
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
                    0,
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
