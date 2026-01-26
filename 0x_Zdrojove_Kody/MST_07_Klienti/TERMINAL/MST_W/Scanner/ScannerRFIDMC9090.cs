using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID2;
using System.Reflection;
using System.IO;
using System.Xml;
using Fask.Logging;
using System.Threading;

namespace Fask.MST_W.Scanner
{
    public class ScannerRFIDMC9090 : ScannerBaseRFID
    {
        public override event ScannerEventRFIDHandler DataReady;
        public override event RFIDTagHandler RFIDTagEvent;

        IRFIDReader deviceReader = null;
        //private ReaderModel m_ReaderModel = Symbol.RFID2.ReaderModel.MC9090;
        List<RFIDBarcodeData> barCodes = new List<RFIDBarcodeData>(); //sem se budou ukladat kody

        System.Threading.Timer timer = null; //new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);

        public ScannerRFIDMC9090()
        {
            this.InitializeScanner();
        }

        public override bool Enabled
        {
            get
            {
                if (deviceReader == null)
                    return false;

                return deviceReader.ReaderStatus == ReaderStatus.ONLINE;
            }
        }

        //private void CreateFile(string path)
        //{
        //    try
        //    {
        //        XmlTextWriter wrXmlConfig = new XmlTextWriter(@path, Encoding.UTF8);
        //        wrXmlConfig.WriteStartDocument();

        //        wrXmlConfig.WriteStartElement("ReaderConfig");
        //        wrXmlConfig.WriteAttributeString("xmlns", "xsi", null, @"http://www.w3.org/2001/XMLSchema-instance");
        //        wrXmlConfig.WriteStartElement("ComPortSettings");


        //        wrXmlConfig.WriteStartElement("COMPort");

        //        if (m_ReaderModel == ReaderModel.MC9090)
        //        {
        //            wrXmlConfig.WriteString("COM7");
        //        }
        //        //else
        //        //{
        //        //   wrXmlConfig.WriteString("COM3");
        //        //}

        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteStartElement("BaudRate");
        //        wrXmlConfig.WriteString("57600");
        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteEndElement();
        //        wrXmlConfig.WriteStartElement("ReaderInfo");

        //        wrXmlConfig.WriteStartElement("Model");
        //        wrXmlConfig.WriteString("MC9090");
        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteStartElement("StartingQ");
        //        wrXmlConfig.WriteValue(deviceReader == null ? 6 : deviceReader.Gen2Settings[0].StartingQ);
        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteStartElement("StartingQWrite");
        //        wrXmlConfig.WriteValue(deviceReader == null ? 6 : deviceReader.Gen2Settings[0].StartingQ);
        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteEndElement();
        //        wrXmlConfig.WriteEndElement();

        //        wrXmlConfig.WriteEndDocument();
        //        wrXmlConfig.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //        throw ex;
        //    }
        //}

        protected override void InitializeScanner()
        {
            TerminateScanner();

            string strPath = ConfigurationPath();
            string configStreamStr = string.Empty;

            try
            {
                //if (!File.Exists(strPath))
                //{
                //    CreateFile(strPath);
                //}

                //StreamReader readerStream = new StreamReader(strPath);
                //Stream configStream = readerStream.BaseStream;

                //byte[] configBytes = new byte[Convert.ToInt32(configStream.Length)];

                //configStream.Read(configBytes, 0, configBytes.Length);
                //configStreamStr = System.Text.Encoding.UTF8.GetString(configBytes, 0, configBytes.Length);

                deviceReader = ReaderFactory.CreateReader(ReaderModel.MC9090, configStreamStr);
                //deviceReader.Gen2Settings[0].StartingQ = 6;

                ConfigurationLoad(strPath);
            }
            catch (Exception ex)
            {
//                if (m_ReaderModel == ReaderModel.MC9090)
//                {
//                    configStreamStr = @"<?xml version='1.0' ?>
//                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
//                                        <ComPortSettings>
//                                        <COMPort>COM7</COMPort>
//                                        <BaudRate>57600</BaudRate>
//                                        </ComPortSettings>
//                                        <ReaderInfo>
//                                        <Model>MC9090</Model>
//                                        <StartingQ>6</StartingQ>
//                                        <StartingQWrite>6</StartingQWrite>
//                                        </ReaderInfo>
//                                        </ReaderConfig>";
//                }
//                else
//                {
//                    configStreamStr = @"<?xml version='1.0' ?>
//                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
//                                        <ComPortSettings>
//                                        <COMPort>COM3</COMPort>
//                                        <BaudRate>57600</BaudRate>
//                                        </ComPortSettings>
//                                        <ReaderInfo>
//                                        <Model>RD5000</Model>
//                                        <StartingQ>6</StartingQ>
//                                        <StartingQWrite>6</StartingQWrite>
//                                        </ReaderInfo>
//                                        </ReaderConfig>";
//                }
//                deviceReader = ReaderFactory.CreateReader(m_ReaderModel, configStreamStr);               
                Log.Write(ex);
                throw ex;
            }

            SetupEvents();

            timer = new Timer(new TimerCallback(ProcessTags), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void ConfigurationLoad(string strPath)
        {
            try
            {
                MC9090.MC9090Configuration mc9090config = new Fask.MST_W.Scanner.MC9090.MC9090Configuration();
                mc9090config.ReadXml(strPath);
                byte startingQ = mc9090config.Reader[0].StartingQ;
                foreach (Gen2Parameters g2param in deviceReader.Gen2Settings)
                {
                    g2param.StartingQ = startingQ;
                }

                foreach (MC9090.MC9090Configuration.AntenaRow antrow in mc9090config.Antena)
                {
                    AntennaConfig antconfig = deviceReader.Antennas[int.Parse(antrow.AntenaName)];
                    antconfig.RxPower = antrow.RxPower;
                    antconfig.TxPower = antrow.TxPower;
                    antconfig.IsEnabled = antrow.Enabled;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private void ConfigurationSave(string strPath)
        {
            try
            {
                MC9090.MC9090Configuration mc9090config = new Fask.MST_W.Scanner.MC9090.MC9090Configuration();
                mc9090config.Reader.AddReaderRow(deviceReader.Gen2Settings[0].StartingQ);
                for (int i = 0; i < deviceReader.NoOfAntenna; i++)
                {
                    AntennaConfig antconf = deviceReader.Antennas[i];
                    mc9090config.Antena.AddAntenaRow(
                        i.ToString(),
                        antconf.TxPower,
                        antconf.RxPower,
                        antconf.IsEnabled
                        );
                }
                mc9090config.AcceptChanges();
                mc9090config.WriteXml(strPath, System.Data.XmlWriteMode.IgnoreSchema);

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private string ConfigurationPath()
        {
            string strPath = Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            strPath = strPath.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, @"MC9090.Config");
            return strPath;
        }

        private void SetupEvents()
        {
            deviceReader.TagEvent += new ReaderEventHandler(deviceReader_TagEvent);
        }

        private void ProcessTags(object state)
        {
            try
            {
                List<RFIDBarcodeData> barCodesTmp;
                lock (barCodes)
                {
                    barCodesTmp = barCodes;
                    barCodes = new List<RFIDBarcodeData>();
                }

                if (barCodesTmp.Count > 0)
                {
                    // Vyvolani udalosti nacteni kodu...
                    if (this.DataReady != null)
                    {
                        this.DataReady(
                            this,
                            new ScannerRFIDEventArgs(barCodesTmp));
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        void deviceReader_TagEvent(object sender, ReaderEventArgs args)
        {
            IEnumerable<IRFIDTag> Tags = null;
            byte[] tag_ID = null;
            string nactenyTag = string.Empty;//prazdny
            try
            {
                if (args != null)
                {
                    Tags = args.Reader.Tags;

                    foreach (IRFIDTag tag in Tags)
                    {
                        tag_ID = null;
                        tag_ID = tag.TagID;
                        nactenyTag = string.Empty;

                        try
                        {
                            if (MST_Global.RFIDUkladatNacitatText)
                            {
                                nactenyTag = RFID.Routines.ByteasciiArrayToString(tag_ID);
                            }
                            else
                            {
                                nactenyTag = RFID.Routines.ByteHexArrayToStringHex(tag_ID);
                            }
                        }
                        catch
                        {
                        }

                        if (
                            (Settings.RemovedRFIDCodes != null)
                            &&
                            (Settings.RemovedRFIDCodes.Contains(nactenyTag))
                            )
                        {
                            continue;
                        }

                        if (nactenyTag != string.Empty)
                        {
                            RFIDBarcodeData bdata = new RFIDBarcodeData(nactenyTag, (uint)(tag.TagType), tag.TagType.ToString(), (uint)tag_ID.Length);
                            lock (barCodes)
                            {
                                if (!barCodes.Contains(bdata))
                                    barCodes.Add(bdata);
                            }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //Log.Write(ex);
            }            
        }

        public override void TerminateScanner()
        {
            this.Disable();
            deviceReader = null;
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public override void Enable()
        {
            if (deviceReader != null)
                deviceReader.Connect();            
        }

        public override void Disable()
        {
            if (deviceReader != null)
                deviceReader.Disconnect();
        }

        public override void StartScan()
        {
            timer.Change(1000, 1000);
            if (deviceReader != null)
                deviceReader.ReadMode = ReadMode.AUTONOMOUS;
        }

        public override void StopScan()
        {
            if (deviceReader != null)
                deviceReader.ReadMode = ReadMode.ONDEMAND;
            timer.Change(500, Timeout.Infinite);
        }

        public override int Power
        {
            get
            {
                return 0;
            }
            set
            {
                ;
            }
        }

        public override List<int> PowerLevels
        {
            get { return new List<int>(); }
        }

        public override void SetSource(string sourceName)
        {
            ;
        }

        public override List<string> GetSources()
        {
            return new List<string>();
        }

        public override void Configure()
        {
            using (MC9090.FrmConfigurationMain frmMain = new MC9090.FrmConfigurationMain(deviceReader))
            {
                frmMain.ShowDialog();
            }

            try
            {
                string configpath = ConfigurationPath();
                //this.CreateFile(configpath);
                this.ConfigurationSave(configpath);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "RFID configuration");
            }
        }

        //public override List<string> RemovedCodes
        //{
        //    get;
        //    set;
        //}

    }
}
