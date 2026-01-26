using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Fask.ScannerProvider;

namespace Fask.ScannerProviderSerialPort
{
    public class ScannerProviderSerialPort: Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        private System.IO.Ports.SerialPort serialport = null;
        private string path = string.Empty;

        public ScannerProviderSerialPort()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configDir">parametr Main.ConfigDir</param>
        public ScannerProviderSerialPort(string configDir)
        {
            path = Path.Combine(configDir, "ScannerSerialPortSettings.xml");
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

        public string ConfigScanner
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

        public AIMTYPE AimType
        {
            set
            {
                return;
            }
            get
            {
                return AIMTYPE.UNKNOWN;
            }
        }

        void serialport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string data = string.Empty;
            while (serialport.BytesToRead > 0)
            {
                data = serialport.ReadLine();
                // Raise the scan event to the caller (with data)
                if (DataReady != null && data != string.Empty)
                {
                    DataReady(sender, new ScannerEventArgs(data, 0, "", (uint)data.Length));
                }
            }
        }

        public void InitializeScanner()
        {
            TerminateScanner();
            serialport = new System.IO.Ports.SerialPort("COM1");
            serialport.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);
            serialport.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialport_DataReceived);
            ScannerSettingLoad();
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

        public void Enable()
        {
            if (serialport != null && !serialport.IsOpen)
                serialport.Open();
        }

		//public void Enable(bool toggleSoftTrigger)
		//{
			//if (serialport != null && !serialport.IsOpen)
				//serialport.Open();
		//}

        public void Disable()
        {
            if (serialport != null && serialport.IsOpen)
                serialport.Close();
        }

        public event Fask.ScannerProvider.ScannerEventHandler DataReady;

        public void EnableAllBarcodes()
        {
            //throw new NotImplementedException();
        }

        public void ScannerSetting()
        {
            using (ScannerSerialPortSettingsForm frm = new ScannerSerialPortSettingsForm())
            {
                frm.SPPortName = serialport.PortName;
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                serialport.PortName = frm.SPPortName;
                ScannerSettingSave();
            }
        }

        public void BarcodeSetting()
        {
            //throw new NotImplementedException();
        }

        public void ScannerSettingSave()
        {
            try
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Write(this.serialport.PortName);
                sw.Flush();
                sw.Close();
                sw = null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "ScannerSerialPort", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }

        public void ScannerSettingLoad()
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                this.serialport.PortName = sr.ReadLine();
                sr.Close();
                sr = null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "ScannerSerialPort", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
            //throw new NotImplementedException();
        }

        public void Log_DataReady_Events()
        {
            try
            {
                Logging.Log.Write("SerialPort Scanner Target InvocationList ... Start");
                foreach (var dlgt in this.DataReady.GetInvocationList())
                {
                    try
                    {
                        Logging.Log.Write(
                            String.Format("{0},{1},{2},{3}",
                            dlgt.ToString(),
                            dlgt.Method,
                            dlgt.Target.ToString(),
                            dlgt.Target.GetType().ToString()
                            )
                        );

                    }
                    catch (Exception exLogging)
                    {
                        Logging.Log.Write(exLogging);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                Logging.Log.Write("SerialPort Scanner Target InvocationList ... End");
            }
        }

        public int SuccessBeepTime { get; set; }

        public Delegate[] InvocationList()
        {
            if (this.DataReady != null)
                return this.DataReady.GetInvocationList();

            return new Delegate[] { };
        }

}
}
