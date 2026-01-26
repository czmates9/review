using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Scanner
{
    public class ScannerCOM : ScannerBase
    {
        private static string ScannerFileName = "Scanner.xml";
        #region SerialPort settings serializer ...
        /// Class that will be serialized and deserialized. 
        /// </summary> 
        [Serializable]
        public class ComPortSettings
        {

            #region Class variables

            private string comPortName;
            private int bitsPerSecond;
            private int dataBits;
            private Parity parity;
            private StopBits stopBits;
            private int readTimeout;

            #endregion

            #region Constructors

            /// <summary> 
            /// Creates ComPortSettings with default values. 
            /// </summary> 
            public ComPortSettings()
            {
                this.comPortName = "COM1";
                this.bitsPerSecond = 9600;
                this.dataBits = 8;
                this.parity = Parity.None;
                this.stopBits = StopBits.One;
                this.readTimeout = 1000;
            }

            #endregion

            #region Properties

            public string ComPortName
            {
                get
                {
                    return this.comPortName;
                }
                set
                {
                    this.comPortName = value;
                }
            }

            public int ComPortBitsPerSecond
            {
                get
                {
                    return this.bitsPerSecond;
                }
                set
                {
                    this.bitsPerSecond = value;
                }
            }

            public int ComPortDataBits
            {
                get
                {
                    return this.dataBits;
                }
                set
                {
                    this.dataBits = value;
                }
            }

            public Parity ComPortParity
            {
                get
                {
                    return this.parity;
                }
                set
                {
                    this.parity = value;
                }
            }

            public StopBits ComPortStopBits
            {
                get
                {
                    return this.stopBits;
                }
                set
                {
                    this.stopBits = value;
                }
            }

            public int ComReadTimeOut
            {
                get { return this.readTimeout; }
                set { this.readTimeout = value; }
            }

            #endregion
        } 
        #endregion

        System.IO.Ports.SerialPort scannerPort = null;

        public ScannerCOM()
        {
            this.InitializeScanner();
        }

        protected override void InitializeScanner()
        {
            scannerPort = new System.IO.Ports.SerialPort();
            ScannerSettingLoad();            
            scannerPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(scannerPort_DataReceived);
            scannerPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(scannerPort_DataReceived);
        }

        void scannerPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //var buffer = new byte[scannerPort.BytesToRead];
                //var bytesread = scannerPort.Read(buffer, 0, buffer.Length);
                //scannerPort.DiscardInBuffer();
                //string barcode = ASCIIEncoding.ASCII.GetString(buffer);

                string barcode = string.Empty;
                while (scannerPort.BytesToRead > 0)
                {
                    try
                    {
                        barcode = scannerPort.ReadLine();
                    }
                    catch (Exception exscanner)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exscanner);
                        barcode = scannerPort.ReadExisting();
                    }
                    //SendCode(sender, tmpdt, ref tmpNewCode, pom);
                }

                OnDataReady(sender, barcode);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            try
            {
                scannerPort.DiscardInBuffer();

            }
            catch (Exception exscannerdiscard)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exscannerdiscard);
            }
        }

        public virtual void OnDataReady(object sender, string barcode)
        {
            if (barcode != string.Empty)
            {
                if (this.DataReady != null)
                {
                    this.DataReady(sender, new ScannerEventArgs(barcode, 0, string.Empty, Convert.ToUInt32(barcode.Length)));
                }
            }
        }

        public override void TerminateScanner()
        {
            if (scannerPort != null)
            {
                if (scannerPort.IsOpen)
                    scannerPort.Close();
            }
            scannerPort = null;
        }

        public override void Enable()
        {
            if (scannerPort != null)
            {
                if (!scannerPort.IsOpen)
                {
                   

                    scannerPort.Open();
                    if (scannerPort.BytesToRead > 0)
                        scannerPort.DiscardInBuffer();
                }
            }
        }

        public override void Disable()
        {
            if (scannerPort != null)
            {
                if (scannerPort.IsOpen)
                {
                    scannerPort.DiscardInBuffer();
                    scannerPort.Close();
                }
            }
        }

        public override event ScannerEventHandler DataReady;

        public override void EnableAllBarcodes()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSetting()
        {
            ScannerSettingLoad();
            using (FormScannerCOMSettings fscanner = new FormScannerCOMSettings())
            {
                fscanner.SerialPort = this.scannerPort;
                if (fscanner.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            ScannerSettingSave();
        }

        public override void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSettingSave()
        {
            try
            {
                
                ComPortSettings comsettings = new ComPortSettings();
                comsettings.ComPortBitsPerSecond = scannerPort.BaudRate;
                comsettings.ComPortDataBits = scannerPort.DataBits;
                comsettings.ComPortName = scannerPort.PortName;
                comsettings.ComPortParity = scannerPort.Parity;
                comsettings.ComPortStopBits = scannerPort.StopBits;
                comsettings.ComReadTimeOut = scannerPort.ReadTimeout;

                XmlSerializer xmlser = new XmlSerializer(typeof(ComPortSettings));
                using (StreamWriter txtwrite = new StreamWriter(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ScannerFileName), false))
                {
                    xmlser.Serialize(txtwrite, comsettings);
                }
                
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(null, ex.Message, "Saving scanner settings", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }

        public override void ScannerSettingLoad()
        {
             StreamReader txtread = null;
            try
            {

                using (txtread = new StreamReader(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ScannerFileName)))
                {

                    ComPortSettings comsettings = new ComPortSettings();
                    XmlSerializer xmlser = new XmlSerializer(typeof(ComPortSettings));
                    comsettings = (ComPortSettings)xmlser.Deserialize(txtread);


                    scannerPort.BaudRate = comsettings.ComPortBitsPerSecond;
                    scannerPort.DataBits = comsettings.ComPortDataBits;
                    scannerPort.PortName = comsettings.ComPortName;
                    scannerPort.Parity = comsettings.ComPortParity;
                    scannerPort.StopBits = comsettings.ComPortStopBits;
                    scannerPort.ReadTimeout = comsettings.ComReadTimeOut;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }
    }
}
