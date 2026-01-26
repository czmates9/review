/*
podpora pro terminaly T+T Netcomm:
http://www.tt-support.com
login: jiriskriv@fask.cz
heslo: <standardni_heslo>

Poznatky:
-promenna "components" main formu musi byt inicializovana, jinak nelze registrovat "HotKey"
-Je nutne explicitne priradit port pro scanner ("COM4"), jinak opet z neznameho duvodu nefunguje prijimani nactenych car.kodu...
-Komunikuje se pomoci wrapperu TTNC SDK, ktery v podstate zabaluje primou komunikaci se seriovym portem, na kterem je instalovan HW ctecky...
-Komunikacni rozhrani nepodporuje(nebo jsem nezjistil jak) ziskani informace o Typu nacteneho kodu (EAN, CODE39, I2of5, apod...)

-jinak je v podstate pouzitelne pro MST_W (ale ty problemy stravil jsem pul dne nez jsem to rozchodil..., SDK je tedy hooodne daleko napr. za Symbolem-Motorola...)
*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.ScannerProvider;
using System.Windows.Forms;

namespace Fask.ScannerProviderTT8000
{
    public class ScannerProviderTT8000 : Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        private TTNC850SDK.TTNCPowerScanner ttncscannerpower = null;
        private TTNC850SDK.TTNCPowerTrigger ttnchotkeypower = null;

        public ScannerProviderTT8000()
        {
        }

        //private System.ComponentModel.IContainer componentsMainForm = null;
        //private IntPtr topLevelFormHandle = IntPtr.Zero;
        Form topLevelForm = null;
        public ScannerProviderTT8000(Form topLevelForm)
        {
            this.topLevelForm = topLevelForm;
            this.InitializeScanner();
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
            this.topLevelForm = topLevelForm;
        }

        private void OnScannerRead(string barcode)
        {
            if (this.DataReady != null)
            {
                this.DataReady(
                    this,
                    new ScannerEventArgs(
                        barcode,
                        0,
                        string.Empty,
                        Convert.ToUInt32(barcode.Length))
                );
            }
        }

        #region Scanning
        bool scanning = false;
        private void startScan()
        {
            if (ttncscannerpower.Enabled && !scanning)
            {
                ttncscannerpower.startScan();
                scanning = true;
            }
        }

        private void stopScan()
        {
            if (ttncscannerpower.Enabled)
            {
                ttncscannerpower.stopScan();
                scanning = false;
            }
        }

        void ttncscanner_OnScan(object sender, TTNC850SDK.ScannerEventArgs e)
        {
            stopScan();

            this.OnScannerRead(e.Barcode.Trim());
        }
        #endregion

        #region Hot Keys Events
        private bool hotkeydown = false;
        void ttnchotkey_OnHotKeyUp(object sender, TTNC850SDK.HotKeyPressedEventArgs e)
        {
            hotkeydown = false;
            stopScan();
        }
        void ttnchotkey_OnHotKeyDown(object sender, TTNC850SDK.HotKeyPressedEventArgs e)
        {
            if (!hotkeydown)
                startScan();
            hotkeydown = true;
        }
        #endregion

        public bool Enabled
        {
            get
            {
                if (ttncscannerpower == null)
                    return false;
                return ttncscannerpower.Enabled;
            }
        }

        public string ConfigScanner
        {
            get
            {
                // TODO : co toto ???
                //throw new NotImplementedException();
                return string.Empty;
            }
            set
            {
                // TODO : co toto???
                //throw new NotImplementedException();
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

        //public Fask.ScannerProvider.IScannerProvider Scanner
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public void InitializeScanner()
        {
            try
            {
                ttncscannerpower = new TTNC850SDK.TTNCPowerScanner();
                ttncscannerpower.Enabled = false;
                ttncscannerpower.Configuration = new TTNC850SDK.TTNCSymbolScanerConfigurationProvider();
                ttncscannerpower.TriggerState = TTNC850SDK.PowerScanTriggerState.TRIGGER_ONLY;
                ttncscannerpower.PortName = "COM4";
                ttncscannerpower.OnScan += new TTNC850SDK.scannerEventHandler(ttncscanner_OnScan);

                //ttnchotkeypower = new TTNC850SDK.TTNCPowerTrigger(componentsMainForm);
                ttnchotkeypower = new TTNC850SDK.TTNCPowerTrigger(new System.ComponentModel.Container());
                //ttnchotkeypower = new TTNC850SDK.TTNCPowerTrigger();
                ttnchotkeypower.AssignHandle(topLevelForm.Handle);
                bool registered = ttnchotkeypower.Register(System.Windows.Forms.Keys.Up);
                ttnchotkeypower.OnHotKeyDown += new TTNC850SDK.TTNCHotKey.HotKeyPressedEventHandler(ttnchotkey_OnHotKeyDown);
                ttnchotkeypower.OnHotKeyUp += new TTNC850SDK.TTNCHotKey.HotKeyPressedEventHandler(ttnchotkey_OnHotKeyUp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TerminateScanner()
        {
            try
            {
                //ttnchotkeypower.Unregister();
                //ttnchotkeypower.ReleaseHandle();
                //ttnchotkeypower.Dispose();
                //ttnchotkeypower = null;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                if (ttncscannerpower.Enabled)
                    ttncscannerpower.Enabled = false;
                //ttncscannerpower.Dispose();
                //ttncscannerpower = null;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            //this.componentsMainForm = null;
            this.topLevelForm = null;
        }

        public void Enable()
        {
            if (ttncscannerpower == null) return;
            if (!ttncscannerpower.Enabled)
                ttncscannerpower.Enabled = true;
        }

		//public void Enable(bool toggleSoftTrigger)
		//{
			//if (ttncscannerpower == null) return;
			//if (!ttncscannerpower.Enabled)
				//ttncscannerpower.Enabled = true;
		//}

        public void Disable()
        {
            if (ttncscannerpower == null) return;
            if (ttncscannerpower.Enabled)
                ttncscannerpower.Enabled = false;
        }

        public event Fask.ScannerProvider.ScannerEventHandler DataReady;

        public void EnableAllBarcodes()
        {
            //throw new NotImplementedException();
            return;
        }

        public void ScannerSetting()
        {
            //throw new NotImplementedException();
            return;
        }

        public void BarcodeSetting()
        {
            //throw new NotImplementedException();
            return;
        }

        public void ScannerSettingSave()
        {
            //throw new NotImplementedException();
            return;
        }

        public void ScannerSettingLoad()
        {
            //throw new NotImplementedException();
            return;
        }

        public void Log_DataReady_Events()
        {
            try
            {
                Logging.Log.Write("TT8000 Scanner Target InvocationList ... Start");
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
                Logging.Log.Write("TT8000 Scanner Target InvocationList ... End");
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
