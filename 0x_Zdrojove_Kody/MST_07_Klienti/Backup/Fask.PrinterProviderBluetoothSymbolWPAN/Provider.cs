using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Fask.PrinterProviderBluetoothSymbolWPAN
{
    public class Provider : Fask.PrinterProvider.IPrinterProvider
    {
        private Symbol.WPAN.Bluetooth.Bluetooth bluetooth = null;
        private Symbol.WPAN.Bluetooth.RemoteDevice remotedevice = null;
        private System.IO.Ports.SerialPort serialport = null;
        private DSConfigPrinterProviderBluetoothSymbolWPAN configuration = null;

        private string TemplatesDirectory
        {
            get
            {
                string templateDir = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                templateDir = Path.Combine(ConfigDirectory, templateDir);
                return templateDir;
            }
        }

        private string ConfigDirectory
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
        }

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
                configuration = new DSConfigPrinterProviderBluetoothSymbolWPAN();
                if (File.Exists(ConfigFilename))
                {
                    try { configuration.ReadXml(ConfigFilename); }
                    catch { }
                }

                if (configuration.Params.Count == 0)
                {
                    configuration.Params.AddParamsRow(
                        string.Empty,
                        string.Empty,
                        10000,
                        "windows-1250",
                        "0000"
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
                configuration = new DSConfigPrinterProviderBluetoothSymbolWPAN();
            }
            
            configuration.WriteXml(ConfigFilename);
        }

        public DSConfigPrinterProviderBluetoothSymbolWPAN.ParamsRow Params
        {
            get
            {
                ConfigurationLoad();
                return configuration.Params[0]; //vzdy ocekavam ze tam bude prave jeden radek !!!
            }
        }


        #region IPrinterProvider Members

        public List<string> GetTemplatesList()
        {
            List<string> templatesList = new List<string>();

            if (!Directory.Exists(TemplatesDirectory))
                Directory.CreateDirectory(TemplatesDirectory);

            try
            {
                foreach (string templ in Directory.GetFiles(TemplatesDirectory))
                {
                    templatesList.Add(Path.GetFileName(templ));
                }                
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            return templatesList;
        }

        public string GetPrinterName()
        {
            return Params.Name;
        }

        ConfigControl configControl = null;
        public UserControl ConfigControlPrinter
        {
            get
            {
                if (configControl == null || configControl.IsDisposed)
                {
                    configControl = new ConfigControl();
                    configControl.Bloetooth = this.bluetooth;
                    configControl.textBoxAddress.Text = Params.IsAddressNull() ? string.Empty : Params.Address;
                    configControl.textBoxName.Text = Params.IsNameNull() ? string.Empty : Params.Name;
                    configControl.textBoxTimeout.Text = Params.IsTimeoutNull() ? 10000.ToString() : Params.Timeout.ToString();
                    configControl.textBoxEncoding.Text = Params.IsEncodingNull() ? System.Text.Encoding.Default.WebName : Params.Encoding;
                    configControl.textBoxPIN.Text = Params.IsPINNull() ? string.Empty : Params.PIN;
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
            try
            {
                Params.Encoding = System.Text.Encoding.GetEncoding(configControl.textBoxEncoding.Text).WebName;
                configControl.textBoxEncoding.BackColor = SystemColors.Window;
            }
            catch
            {
                configControl.textBoxEncoding.BackColor = Color.Red;
                return;
            }
            Params.PIN = configControl.textBoxPIN.Text;

            this.InitializePrinter();

            ConfigurationSave();
            //bsave.Enabled = false;
        }

        public void InitializePrinter()
        {
            try
            {
                //Logging.Log.WriteDebug("START InitializePrinter");
                TerminatePrinter();

                //Logging.Log.WriteDebug("bluetooth is " + (bluetooth == null ? "null" : "not null"), "InitializePrinter");
                if (bluetooth == null)
                    bluetooth = new Symbol.WPAN.Bluetooth.Bluetooth();

                //Logging.Log.WriteDebug("remotedevice is " + (remotedevice == null ? "null" : "not null"), "InitializePrinter");
                if (remotedevice == null)
                {
                    remotedevice = new Symbol.WPAN.Bluetooth.RemoteDevice(
                        this.Params.Name,
                        this.Params.Address,
                        string.Empty // TODO : sluzba ... ???
                        );
                    bluetooth.RemoteDevices.Add(remotedevice);
                }

                //Logging.Log.WriteDebug("remotedevice is paired: " + remotedevice.IsPaired.ToString(), "InitializePrinter");
                if (!remotedevice.IsPaired)
                {
                    //remotedevice.Pair("0000"); // TODO : auth pin ... 
                    //remotedevice.Pair(
                    if (Params.IsPINNull() || String.IsNullOrEmpty(Params.PIN))
                        remotedevice.Pair();
                    else
                        remotedevice.Pair(Params.PIN);
                }

                if (remotedevice.IsPaired)
                {
                    int port = remotedevice.LocalComPort;
                    if (serialport != null && serialport.IsOpen)
                    {
                        serialport.Close();
                        serialport.Dispose();
                        serialport = null;
                    }
                    serialport = new System.IO.Ports.SerialPort("COM" + port, 19200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    //serialport.Handshake = System.IO.Ports.Handshake.RequestToSendXOnXOff;
                    serialport.Handshake = System.IO.Ports.Handshake.RequestToSend;
                    serialport.Encoding = System.Text.Encoding.GetEncoding(this.Params.Encoding);
                    serialport.WriteTimeout = this.Params.Timeout;
                    serialport.ReadTimeout = this.Params.Timeout;
                    serialport.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);
                    serialport.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);
                }
                //Logging.Log.WriteDebug("END InitializePrinter");
            }
            catch (Exception ex)
            {
                //Logging.Log.WriteDebug("Exception", "InitializePrinter");
                Logging.Log.Write(ex, this.GetType().ToString() + ":InitializePrinter");
            }
        }

        void serialport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            // Todo ... 
            string data = serialport.ReadExisting();
            Logging.Log.Write(data, this.GetType().ToString() + ":serialport_DataReceived");
        }

        public void TerminatePrinter()
        {
            StringBuilder sbDebug = new StringBuilder();
            try
            {
                //Logging.Log.WriteDebug("START TerminatePrinter");
                //Logging.Log.WriteDebug("serialport", "TerminatePrinter");
                sbDebug.AppendLine("serialport");
                if (serialport != null)
                {
                    Logging.Log.WriteDebug("serialport not null", "TerminatePrinter");
                    sbDebug.AppendLine(" != null");
                    if (serialport.IsOpen)
                    {
                        //Logging.Log.WriteDebug("serialport is open", "TerminatePrinter");
                        sbDebug.AppendLine(" IsOpen");
                        serialport.Close();
                        //Logging.Log.WriteDebug("serialport is closed", "TerminatePrinter");
                        sbDebug.AppendLine(" Closed");
                    }

                    sbDebug.AppendLine("Disposing");
                    //Logging.Log.WriteDebug("serialport is disposing", "TerminatePrinter");
                    serialport.Dispose();
                    //Logging.Log.WriteDebug("serialport is disposed", "TerminatePrinter");
                    sbDebug.AppendLine("Disposed");
                    serialport = null;
                    sbDebug.AppendLine(" = null");
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Bluethoot Terminate : " + sbDebug.ToString());
            }

            sbDebug = new StringBuilder();
            try
            {
                sbDebug.AppendLine("remote device");
                if (remotedevice != null)
                {
                    sbDebug.AppendLine(" IsComPortOpened");
                    if (remotedevice.IsComPortOpened)
                    {
                        sbDebug.AppendLine("  =True");
                        remotedevice.ClosePort();
                        sbDebug.AppendLine("  ClosePort");
                    }

                    sbDebug.AppendLine(" IsPaired");
                    if (remotedevice.IsPaired)
                    {
                        sbDebug.AppendLine("  UnPair"); 
                        remotedevice.UnPair();
                        sbDebug.AppendLine("  UnPaired");
                    }

                    sbDebug.AppendLine(" bluethoot");
                    if (bluetooth != null)
                    {
                        sbDebug.AppendLine("  != null");
                        bluetooth.RemoteDevices.Delete(remotedevice);
                        sbDebug.AppendLine("  remotedevice deleted");
                    }

                    sbDebug.AppendLine(" remotedevice");
                    remotedevice = null;
                    sbDebug.AppendLine("  =null");
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Bluethoot Terminate : " + sbDebug.ToString());
            }

            sbDebug = new StringBuilder();
            try
            {
                sbDebug.AppendLine("bluethoot");
                if (this.bluetooth != null)
                {
                    sbDebug.AppendLine(" !=null");
                    this.bluetooth.Dispose();
                    sbDebug.AppendLine(" Disposed");
                    this.bluetooth = null;
                    sbDebug.AppendLine(" =null");
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Bluethoot Terminate : " + sbDebug.ToString());
            }
            //Logging.Log.WriteDebug("END TerminatePrinter");
        }

        //private void TryWriteData(byte[] dataToSend, uint timeout)
        private bool TryWriteData(string dataToSend, int timeout)
        {
            StringBuilder sbDebug = new StringBuilder();
            try
            {
                //Logging.Log.WriteDebug("START TryWriteData");
                sbDebug.AppendLine("TryWriteData");
                
                sbDebug.AppendLine(" TryOpenPort");
                
                if (!TryOpenPort())
                    return false;

                sbDebug.AppendLine(" serialport.write");
                //remotedevice.Write(dataToSend, timeout);
                //serialport.WriteTimeout = timeout;
                //serialport.Write(
                //serialport.Write(dataToSend);
                int blen = 1000;
                int bpos = 0;
                char[] buffer = dataToSend.ToCharArray();
                while (bpos < buffer.Length)
                {                    
                    serialport.Write(buffer, bpos, bpos + blen > buffer.Length ? buffer.Length - bpos : blen);
                    bpos += blen;
                } 
                
                
                sbDebug.AppendLine(" data sended");
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, this.GetType().ToString() + ":TryWriteData" + sbDebug.ToString());
                return false;
            }
            finally
            {
                try
                {
                    serialport.Close();
                }
                catch (Exception exserialport)
                {
                    Logging.Log.Write(exserialport, this.GetType().ToString() + ":TryWriteData - serialport.close");
                }
                //Logging.Log.WriteDebug("END TryWriteData");
            }
        }

        private bool TryOpenPort()
        {
            StringBuilder sbDebug = new StringBuilder();
            try
            {
                //Logging.Log.WriteDebug("START TryOpenPort");
                sbDebug.AppendLine("remotedevice");
                //Logging.Log.WriteDebug("remotedevice", "TryOpenPort");
                if (remotedevice == null || !remotedevice.IsPaired)
                { // inicializace ...
                    //Logging.Log.WriteDebug("remotedevice==null", "TryOpenPort");
                    sbDebug.AppendLine(" ==null && !IsPaired => InitializePrinter");
                    this.InitializePrinter();
                    //Logging.Log.WriteDebug("Inicialized printer", "TryOpenPort");
                    sbDebug.AppendLine(" initialized printer");
                }

                //if (!remotedevice.IsComPortOpened)
                //{
                //    remotedevice.OpenPort();
                //}
                sbDebug.AppendLine(" serialport");
                //Logging.Log.WriteDebug("serialport", "TryOpenPort");
                if (serialport != null && !serialport.IsOpen)
                {
                    //Logging.Log.WriteDebug("serialport not null and closed", "TryOpenPort");
                    sbDebug.AppendLine("  !=null && !IsOpen");
                    serialport.Open();
                    //Logging.Log.WriteDebug("serial port opened", "TryOpenPort");
                    sbDebug.AppendLine("  port opened");
                }

                //Logging.Log.WriteDebug("END TryOpenPort");
                return true;
            }
            catch (Exception ex)
            { //neco se nepodarilo ...     
                //Logging.Log.WriteDebug("Exception TryOpenPort");
                Logging.Log.Write(ex, this.GetType().ToString() + ":TryOpenPort " + sbDebug.ToString());
                return false;
            }
        }

        private StringBuilder ReadTemplate(string template, string mena_id)
        {
            if (!String.IsNullOrEmpty(mena_id))
            {
                string templateMena = template + "." + mena_id;
                string templateMenaPath = Path.Combine(TemplatesDirectory, templateMena);
                if (File.Exists(templateMenaPath))
                {
                    template = templateMena;
                }
            }
            return ReadTemplate(template);
        }

        private StringBuilder ReadTemplate(string template)
        {
            string templatePath = Path.Combine(TemplatesDirectory, template);
            StreamReader sr = new StreamReader(templatePath);
            StringBuilder sb = new StringBuilder(sr.ReadToEnd());
            sr.Close();
            return sb;
        }

        /// <summary>
        /// Prepise nalezene klice pomoci regularniho vyrazu => \$(\w+)((,)(\d+))?\$
        /// $[id](,[delka])?$
        /// [id] = identifikator
        /// [delka] = maximalni delka retezce (nemusi byt definovano, pak vraci cely retezec)
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="template">Template</param>
        /// <returns>Novy objekt s nahrazenymi parametry</returns>
        private StringBuilder ReplaceTemplateKeys(Dictionary<string, string> data, StringBuilder template)
        {
            StringBuilder sbNew = new StringBuilder(template.ToString());

            // RegEx 
            // => \$(\w+|.+,\d+)\$
            // => \$\w+(,\d+)?\$
            // => \$(\w+)((,)(\d+))?\$ 
            //  Group[0] = cely match
            //  Group[1] = identifikator (\w+)
            //  Group[2] = postfix ((,)(\d+))?
            //  Group[3] = carka (,)
            //  Group[4] = delka (\d+)

            // Obsahuje odpovidajici matche
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$(\w+)((,)(\d+))?\$");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string tresult = string.Empty;
                string key = match.Groups[1].Value;
                if (!data.ContainsKey(key))
                { //klic v datech nenalezen, nahradim do sablony prazdnym retezcem ...
                    tresult = string.Empty;
                }
                else
                { //klic nalezen, tak se ho pokusim naformatovat ...
                    tresult = data[key];
                    int? trim = null;
                    try { trim = int.Parse(match.Groups[4].Value); }
                    catch { }
                    if (trim.HasValue && trim.Value > 0)
                    {
                        tresult = tresult.Substring(0, tresult.Length < trim.Value ? tresult.Length : trim.Value);
                    }
                }

                // finalni nahrazeni matche vysledkem formatovani ...
                sbNew.Replace(match.Value, tresult);
            }

            return sbNew;
        }

        public bool Print(Dictionary<string, string> data, string template, int pocet)
        {
            string mena_id = string.Empty;
            if (data.ContainsKey("mena_id_print"))
                mena_id = data["mena_id_print"];

            StringBuilder sb = ReadTemplate(template, mena_id);
            
            //foreach (var item in data)
            //{
            //    sb.Replace("$" + item.Key + "$", item.Value);
            //}

            sb = ReplaceTemplateKeys(data, sb);

            return this.Print(sb.ToString(), pocet);
        }

        public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            //Logging.Log.WriteDebug("START public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)");
            string mena_id_H = string.Empty;
            if (dataHeader.ContainsKey("mena_id_print"))
                mena_id_H = dataHeader["mena_id_print"];
            string mena_id_R = string.Empty;
            if (dataRowList.Count > 0 && dataRowList[0].ContainsKey("mena_id_print"))
                mena_id_R = dataRowList[0]["mena_id_print"];
            string mena_id_F = string.Empty;
            if (dataFooter.ContainsKey("mena_id_print"))
                mena_id_F = dataFooter["mena_id_print"];

            StringBuilder sbHTemplate = ReadTemplate(templateHeader, mena_id_H);
            StringBuilder sbRTemplate = ReadTemplate(templateRow, mena_id_R);
            StringBuilder sbFTemplate = ReadTemplate(templateFooter, mena_id_F);
            //StringBuilder sbH = new StringBuilder(sbHTemplate.ToString());
            //StringBuilder sbR = new StringBuilder();
            //StringBuilder sbF = new StringBuilder(sbFTemplate.ToString());
            StringBuilder sbH = null;
            StringBuilder sbR = null;
            StringBuilder sbF = null;

            //foreach (var item in dataHeader)
            //{
            //    sbH.Replace("$" + item.Key + "$", item.Value);
            //}
            //Logging.Log.WriteDebug("START sbH = ReplaceTemplateKeys(dataHeader, sbHTemplate);");
            sbH = ReplaceTemplateKeys(dataHeader, sbHTemplate);
            //Logging.Log.WriteDebug("END sbH = ReplaceTemplateKeys(dataHeader, sbHTemplate);");
            // po radcich se pridava ...
            sbR = new StringBuilder();
            //Logging.Log.WriteDebug("START foreach (var dataRow in dataRowList;, pocet zaznamu: " + dataRowList.Count);
            //int cisloRadku = 0;
            foreach (var dataRow in dataRowList)
            {
                //cisloRadku++;
                //Logging.Log.WriteDebug("ReplaceTemplateKeys - Zacatek zpracovani zaznamu c. " + cisloRadku);

                //StringBuilder sbRCopy = new StringBuilder(sbRTemplate.ToString());
                //foreach (var item in dataRow)
                //{
                //    sbRCopy.Replace("$" + item.Key + "$", item.Value);
                //}
                StringBuilder sbRCopy = ReplaceTemplateKeys(dataRow, sbRTemplate);
                sbR.Append(sbRCopy.ToString());
            }
            //Logging.Log.WriteDebug("END foreach (var dataRow in dataRowList;");
            //foreach (var item in dataFooter)
            //{
            //    sbF.Replace("$" + item.Key + "$", item.Value);
            //}
            //mbw.BeginPracujiForm("Načítání zápatí");
            sbF = ReplaceTemplateKeys(dataFooter, sbFTemplate);

            StringBuilder sbPrint = new StringBuilder();
            if (sbH != null)
                sbPrint.Append(sbH.ToString());
            if (sbR != null)
                sbPrint.Append(sbR.ToString());
            if (sbF != null)
                sbPrint.Append(sbF.ToString());
            //Logging.Log.WriteDebug(sbR.ToString(), "sbR.ToString()");
            //Logging.Log.WriteDebug("END public bool Print(Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRowList, Dictionary<string, string> dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)");
            //return true;
            //Logging.Log.WriteDebug("START BLUETOOTH TISK");
            //bool printres = this.Print(sbPrint.ToString(), pocet);
            return this.Print(sbPrint.ToString(), pocet);
            //Logging.Log.WriteDebug("END BLUETOOTH TISK");
            //return printres;
        }

        public bool Print(string text, int pocet)
        {
            for (int i = 0; i < pocet; i++)
            {
                if (!TryWriteData(text, this.Params.Timeout))
                    return false;
            }
            return true;
        }

        #endregion
    }
}
