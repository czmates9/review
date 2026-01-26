using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.ScannerProvider;
using PsionTeklogix.Barcode;
using PsionTeklogix.Barcode.ScannerServices;

namespace Fask.ScannerProviderPSION
{
    public sealed class ScannerProviderPSION : Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        private PsionTeklogix.Barcode.Scanner scanner;
        private PsionTeklogix.Barcode.ScannerServices.ScannerServicesDriver scannerServicesDriver;
        public ScannerProviderPSION()
        {
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
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


        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
        }

        public void InitializeScanner()
        {
            try
            {
                Logging.Log.Write("scanner = new", "InitializeScanner()");
                scanner = new PsionTeklogix.Barcode.Scanner();
                scannerServicesDriver = new PsionTeklogix.Barcode.ScannerServices.ScannerServicesDriver();
                scanner.Driver = scannerServicesDriver;

                //'«Scan Result» splash screen disabled
                if (Convert.ToBoolean(this.scannerServicesDriver.GetProperty("Scs\\Scan Result")))
                {
                    Logging.Log.Write("scannerDriver\\Scan Result", "InitializeScanner()");
                    this.scannerServicesDriver.SetProperty("Scs\\Scan Result", false);
                }

                //'«Scan Indicator» splash screen disabled
                if (Convert.ToBoolean(this.scannerServicesDriver.GetProperty("Scs\\Scan Indic")))
                {
                    Logging.Log.Write("scannerDriver\\Scan Indic", "InitializeScanner()");
                    this.scannerServicesDriver.SetProperty("Scs\\Scan Indic", false);
                }

                Logging.Log.Write("EnableAllBarcodes()", "InitializeScanner()");
                this.EnableAllBarcodes();

                Logging.Log.Write("apply setting changes", "InitializeScanner()");
                this.scannerServicesDriver.ApplySettingChanges();

                Logging.Log.Write("ScanCompleteEvent", "InitializeScanner()");
                if (this.scanner != null)
                    scanner.ScanCompleteEvent += new PsionTeklogix.Barcode.ScanCompleteEventHandler(Scanner_DataReady);
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
                if (scanner != null)
                {
                    scanner.Dispose();
                    scanner = null;
                }

                if (scannerServicesDriver != null)
                {
                    scannerServicesDriver.Dispose();
                    scannerServicesDriver = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Enable()
        {
            try
            {
                if ((scanner != null) && (scannerServicesDriver != null))
                {
                    scanner.Enabled = true;
                    _enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		//public void Enable(bool toggleSoftTrigger)
		//{
			//try
			//{
				//if ((scanner != null) && (scannerServicesDriver != null))
				//{
					//scanner.Enabled = true;
					//_enabled = true;
				//}
			//}
			//catch (Exception ex)
			//{
				//throw ex;
			//}
		//}

        public void Disable()
        {
            if ((scanner != null) && (scannerServicesDriver != null))
                scanner.Enabled = false;
            _enabled = false;
        }

        public event Fask.ScannerProvider.ScannerEventHandler DataReady;

        
        // Scanner data ready to send.
        void Scanner_DataReady(object sender, ScanCompleteEventArgs e)
        {
            try
            {
                // Raise the scan event to the caller (with data)
                if (DataReady != null)
                {
                    this.DataReady(sender, new ScannerEventArgs(e.Text, GetBarcodeType(e.Symbology), e.Symbology.ToString(), (uint)e.Text.Length));
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private BarcodeType GetBarcodeType(BarcodeSymbology dType)
        {
            switch (dType)
            {
                case BarcodeSymbology.EAN128:
                    return BarcodeType.EAN128;
                case BarcodeSymbology.EAN13:
                case BarcodeSymbology.EAN13_2:
                case BarcodeSymbology.EAN13_5:
                    return BarcodeType.EAN13;
                case BarcodeSymbology.EAN8:
                    return BarcodeType.EAN8;
                case BarcodeSymbology.Code128:
                    return BarcodeType.CODE128;
                case BarcodeSymbology.Code32:
                    return BarcodeType.CODE32;
                case BarcodeSymbology.Code39:
                case BarcodeSymbology.Code39FullAscii:
                    return BarcodeType.CODE39;
                case BarcodeSymbology.Code93:
                    return BarcodeType.CODE93;
                case BarcodeSymbology.Interleaved2of5:
                    return BarcodeType.I2OF5;
                // TODO : doplnit jine typy???
                default: return BarcodeType.Unknown;
            }
        }

        public void EnableAllBarcodes()
        {
            try
            {
                scannerServicesDriver.SetProperty("Barcode\\CDB\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\C11\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\C128\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\C39\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\C93\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\D25\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\UPCA\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\UPCE\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\EAN13\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\EAN8\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\I25\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\IATA25\\Enabled", true);
                scannerServicesDriver.SetProperty("Barcode\\MSI\\Enabled", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ScannerSetting()
        {
            throw new NotImplementedException();
        }

        public void BarcodeSetting()
        {
            throw new NotImplementedException();
        }

        public void ScannerSettingSave()
        {
            throw new NotImplementedException();
        }

        public void ScannerSettingLoad()
        {
            throw new NotImplementedException();
        }

        public void Log_DataReady_Events()
        {
            try
            {
                Logging.Log.Write("PSION Scanner Target InvocationList ... Start");
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
                Logging.Log.Write("PSION Scanner Target InvocationList ... End");
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
