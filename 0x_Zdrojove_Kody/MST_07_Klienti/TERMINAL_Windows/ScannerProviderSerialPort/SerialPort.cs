using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FASK.MST_WINDOWS.IScannerProvider;
using System.IO.Ports;
using System.Xml;
using FASK.MST_WINDOWS.ErrorLog;

namespace FASK.MST_WINDOWS.ScannerProviderSerialPort
{
    public class ScannerProviderSerialPort : FASK.MST_WINDOWS.IScannerProvider.IScannerProvider
    {
        private string _configScanner = "ScannerProviderSerialPort.xml";
        private SerialPort serialport = null;
        private string portName = "COM1";
        private int baudRate = 9600;
        private Parity parity = Parity.Even;
        private int dataBits = 8;
        private StopBits stopBits = StopBits.One;
        private int readTimeout = 1000;

        private int rs485CodeReadTimeout = 100; //timeout po korektnim cteni kodu v setinach sekund
        private int rs485CodeNoReadTimeout = 20; //timeout po nekorektnim cteni kodu v setinach sekund

        private char Code_StartChar = '-';
        private char Code_StopChar1 = '\r';
        private string Code_StopChar2 = "0A";
        private char Code_NoReadChar = '-';

        private Code rs485PrevReadResult = Code.NoData;
        private DateTime rs485DTLast = DateTime.MinValue;
        private string rs485LastCode = string.Empty;

        private const int CodeLen = 20;		//maximalni delka caroveho kodu pri cteni z rs485

        public ScannerProviderSerialPort()
        {
            try
            {
                this.InitializeScanner();
            }
            catch
            {
            }
        }

        public void InitializeScanner()
        {
            try
            {
                TerminateScanner();

                //_configScanner = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "ScannerSerialPortSettings.xml");
                ScannerSettingLoad();

                serialport = new System.IO.Ports.SerialPort(portName, baudRate, parity, dataBits, stopBits);

                serialport.ReadTimeout = readTimeout;
                serialport.NewLine = HexToString(Code_StopChar2); 

                serialport.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);
                serialport.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);

                return;
            }
            catch (Exception ex)
            {
            }
        }

        public string HexToString(string hex)
        {
            byte[] data = FromHex(hex);
            return Encoding.ASCII.GetString(data);
        }

        public byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public bool Enabled
        {
            get
            {
                if (serialport == null)
                    return false;

                return serialport.IsOpen;
            }
        }

        public void Enable()
        {
            if (serialport != null && !serialport.IsOpen)
                serialport.Open();
        }

        public void Disable()
        {
            if (serialport != null && serialport.IsOpen)
                serialport.Close();
        }

        public void TerminateScanner()
        {
            if (serialport != null && serialport.IsOpen)
            {
                serialport.Close();
                serialport.Dispose();
                serialport = null;
            }
        }

        public void ScannerSettingLoad()
        {
            //Zjisteni cesty ke konfiguracnimu souboru.
            string configFilePath = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), _configScanner))).LocalPath;

            if (!System.IO.File.Exists(configFilePath))
                return;

            //Vytvoreni xml dokumentu
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configFilePath);

            portName = LoadElement(xmldoc, "/Scanner/portName");
            baudRate = int.Parse(LoadElement(xmldoc, "/Scanner/baudRate"));
            parity = (Parity)Enum.Parse(typeof(Parity), (LoadElement(xmldoc, "/Scanner/parity")));
            dataBits = int.Parse(LoadElement(xmldoc, "/Scanner/dataBits"));
            stopBits = (StopBits)Enum.Parse(typeof(StopBits), (LoadElement(xmldoc, "/Scanner/stopBits")));
            readTimeout = int.Parse(LoadElement(xmldoc, "/Scanner/ReadTimeout"));

            rs485CodeReadTimeout = int.Parse(LoadElement(xmldoc, "/Scanner/CodeReadTimeout"));
            rs485CodeNoReadTimeout = int.Parse(LoadElement(xmldoc, "/Scanner/CodeNoReadTimeout"));

            Code_StartChar = HexToString(LoadElement(xmldoc, "/Scanner/CodeStartChar")).ToCharArray()[0];
            Code_StopChar1 = HexToString(LoadElement(xmldoc, "/Scanner/CodeStopChar")).ToCharArray()[0];
            Code_NoReadChar = HexToString(LoadElement(xmldoc, "/Scanner/CodeNoReadChar")).ToCharArray()[0];
            Code_StopChar2 = LoadElement(xmldoc, "/Scanner/CodeStopChar2");
        }

        private static string LoadElement(XmlDocument XmlDoc, string NodeName)
        {
            string nodeValue = string.Empty;

            //Konkretni uzel
            XmlElement configNode = XmlDoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                //Vlozeni obsahu uzlu.
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }

        public void EnableAllBarcodes()
        {
            //throw new NotImplementedException();
        }

        public void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public event ScannerEventHandler DataReady;

        void serialport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            DateTime tmpdt = DateTime.Now;
            string tmpNewCode = string.Empty;
            string pom = string.Empty;

            try
            {
                while (serialport.BytesToRead > 0)
                {
                    pom = serialport.ReadLine();
                    SendCode(sender, tmpdt, ref tmpNewCode, pom);
                }
            }
            catch (Exception ex)
            {
                tmpNewCode = serialport.ReadExisting();
                DataReady(sender, new ScannerEventArgs(tmpNewCode, 0, "", (uint)tmpNewCode.Length, Code.BadRead));
                return;
            }


        }

        private void SendCode(object sender, DateTime tmpdt, ref string tmpNewCode, string pom)
        {
            Code tmpR = Code.NoData;

            tmpR = GetRS485Code(pom, out tmpNewCode);

            if (rs485PrevReadResult == Code.NoData)
            {
                if (tmpR == Code.Read || tmpR == Code.NoRead)
                {
                    rs485PrevReadResult = tmpR;
                    rs485DTLast = DateTime.Now;
                    rs485LastCode = tmpNewCode;
                    //return Code.NoData; poslat noData... nebo ne???
                    //DataReady(sender, new ScannerEventArgs(tmpNewCode, 0, "", (uint)tmpNewCode.Length, status));
                }
                else
                {
                    DataReady(sender, new ScannerEventArgs(tmpNewCode, 0, "", (uint)tmpNewCode.Length, tmpR));
                }
            }
            else
            {
                //predchozi stav byl Read nebo NoRead
                //1. zjistit timeout pro read nebo noread a pustit priznak
                //2. jestlize jsem znovu nacetl read nebo noread, tak co to je?
                //long rs485diff = DateTimeDiff(rs485DTLast, tmpdt);
                TimeSpan rs485diff = tmpdt - rs485DTLast;
                if (tmpR == Code.NoData) //nenacten zadny kod, pak osetrit timeouty
                {
                    if (rs485PrevReadResult == Code.Read && rs485diff.Milliseconds >= rs485CodeReadTimeout)
                    {
                        rs485PrevReadResult = Code.NoData;
                        string newCode = rs485LastCode;
                        rs485LastCode = string.Empty; //reset lastcodu
                        DataReady(sender, new ScannerEventArgs(newCode, 0, "", (uint)newCode.Length, Code.Read));
                    }
                    else if (rs485PrevReadResult == Code.NoRead && rs485diff.Milliseconds >= rs485CodeNoReadTimeout)
                    {
                        rs485PrevReadResult = Code.NoData;
                        string newCode = rs485LastCode;
                        rs485LastCode = string.Empty; //reset lastcodu
                        DataReady(sender, new ScannerEventArgs(newCode, 0, "", (uint)newCode.Length, Code.NoRead));
                    }
                    else
                    {
                        //return tmpR; //Code_NoData posilat nebo ne???
                        //DataReady(sender, new ScannerEventArgs(newCode, 0, "", (uint)newCode.Length, Code.NoRead));
                    }
                }
                else if (tmpR == Code.Read || tmpR == Code.NoRead) //Nacetl jsem dalsi
                {
                    if (rs485PrevReadResult == Code.Read && rs485diff.Milliseconds < rs485CodeReadTimeout) //pred timoutem ignorovat, ponechat puvodni kod
                    {
                        //return Code.NoData; posilat nebo ne???
                    }
                    else if (rs485PrevReadResult == Code.Read && rs485diff.Milliseconds >= rs485CodeReadTimeout) //po timeoutu znamena
                    {
                        rs485PrevReadResult = tmpR;
                        rs485DTLast = tmpdt;
                        string newCode = rs485LastCode;
                        rs485LastCode = tmpNewCode;
                        DataReady(sender, new ScannerEventArgs(newCode, 0, "", (uint)newCode.Length, Code.Read));
                    }
                    else if (rs485PrevReadResult == Code.NoRead && rs485diff.Milliseconds < rs485CodeNoReadTimeout) //nastavit novy kod, ktery jsem nacetl
                    {
                        if (tmpR == Code.Read) // aktualni je Code_Read => cteni kodu pred timeoutem NoRead
                        {
                            rs485PrevReadResult = tmpR;
                            rs485DTLast = DateTime.Now;
                            rs485LastCode = tmpNewCode;
                        }
                        else //zde je aktualni tmpR==Code_NoRead
                        {
                            //return Code.NoData;todo...posilat nebo ne???
                        }
                    }
                    else if (rs485PrevReadResult == Code.NoRead && rs485diff.Milliseconds >= rs485CodeNoReadTimeout) //nastavit novy kod, ktery jsem nacetl
                    {
                        rs485PrevReadResult = tmpR;
                        rs485DTLast = tmpdt;
                        string newCode = rs485LastCode;
                        rs485LastCode = tmpNewCode;
                        DataReady(sender, new ScannerEventArgs(newCode, 0, "", (uint)newCode.Length, Code.NoRead));
                    }
                }
                else //zde ale dochazi k prichodu Code_BadRead nebo Code_TooLong, takze logovat???
                {
                    //return Code.NoData; todo...posilat nebo ne???
                }
            }
        }

        bool IsAllDigits(string s)
        {
            return s.All(Char.IsDigit);
        }

        private Code GetRS485Code(string pom, out string tmpNewCode)
        {

            tmpNewCode = string.Empty;

            //ocekavam znak Code_StopChar1, pokud to neni on, tak chyba cteni
            if (pom[pom.Length - 1] != Code_StopChar1)
            {
                return Code.BadRead; //??? ma se vratit chyba cteni nebo co?
            }

            //ocekavam nacteni kodu <0D>, jinak chyba ceteni
            //ToDo: upravit readLine() kodu...
            //if (tmpNewCode[tmpNewCode.Length] != Code_StopChar2)
            //    return Code.BadRead; //??? ma se vratit chyba cteni nebo co?

            //Pokud jsem az tady, tak jsem nacetl kod korektne...
            //Zjisti, co jsem vlastne dostal od scanneru. (Kod nebo NoRead?)

            Code ret = IsCorrectCode(pom);

            tmpNewCode = pom.Trim().Remove(0, 1);

            return ret;
        }

        private Code IsCorrectCode(string tmpNewCode)
        {
            if (tmpNewCode.Length < 1)
                return Code.BadRead;

            if (tmpNewCode[1] == Code_NoReadChar)
            {
                return Code.NoRead;
            }
            else
                return Code.Read;
        }

        public void ScannerSetting()
        {
            /*
            using (ScannerSerialPortSettingsForm frm = new ScannerSerialPortSettingsForm())
            {
                //frm.SPPortName = serialport.PortName;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                //serialport.PortName = frm.SPPortName;
                ScannerSettingSave();
            }

            this.ScannerSettingSave();
            */
        }

        public void ScannerSettingSave()
        {
            /*
            System.Xml.XmlWriter xmlwrite = null;
            try
            {
                xmlwrite = System.Xml.XmlWriter.Create(_configScanner);

                xmlwrite.WriteStartElement("Scanner");

                xmlwrite.WriteStartElement("CODE39");
                //xmlwrite.WriteElementString("Code32Prefix", this._reader.Decoders.CODE39.Code32Prefix.ToString());
                xmlwrite.WriteEndElement(); //CODE39


                xmlwrite.WriteEndElement(); //Scanner
                xmlwrite.Close();
                xmlwrite = null;
            }
            catch
            {
                //Log.Write(ex.Message + ex.StackTrace, Fask.ScannerProvider.ScannerTypes.Symbol_MC3000.ToString() + " : ScannerSettingsSave");
                //mesagg.Show(ex.Message, Fask.ScannerProvider.ScannerTypes.Symbol_MC3000.ToString(), System.Windows.Forms.MessageBoxButtons.OK, MsgBoxIcon.Critical, MessageBoxDefaultButton.Button1);
            }*/
        }

        #region IScannerProvider Members

        public FASK.MST_WINDOWS.IScannerProvider.IScannerProvider Scanner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ConfigScannerPath
        {
            get
            {
                return _configScanner;
            }
            set
            {
                _configScanner = value;
            }
        }

        #endregion

        #region IScannerProvider Members


        public void SaveScannerReadCount()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
