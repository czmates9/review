using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HSM.Embedded.Decoding;
using Fask.ScannerProvider;

namespace Fask.ScannerProviderDolphin60S
{
    public sealed class ScannerProviderDolphin60S : Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        private HSM.Embedded.Decoding.DecodeComponent scanner = null;
        
        public ScannerProviderDolphin60S()
        {
            //try
            //{
            //    this.InitializeScanner();
            //}
            //catch
            //{
            //}
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
            //tady se nic nedela...
        }

        public void InitializeScanner()
        {
            // 
            // scanner
            // 
            this.scanner = new HSM.Embedded.Decoding.DecodeComponent();
            this.scanner.AimerDelay = 0;
            this.scanner.DecodeMode = HSM.Embedded.Decoding.DecodeComponent.DecodeModes.Standard;
            this.scanner.ScanKey = HSM.Embedded.Decoding.DecodeComponent.ScanKeys.Scan;
            this.scanner.ScanKeyOperation = HSM.Embedded.Decoding.DecodeComponent.ScanKeyOptions.ScanBarcode;
            //this.scanner.ScanningLightsMode = HSM.Embedded.Decoding.DecodeComponent.ScanningLightsModes.AimerAndIllumination;
            this.scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.Concurrent;
            this.scanner.DecodeEvent += new HSM.Embedded.Decoding.DecodeComponent.DecodeEventHandler(this.scanner_DecodeEvent);

            this.EnableAllBarcodes();
            //          Alternatively, configure from file 
            //            string fullAssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //            this.scanner.ConfigureFromExm(String.Concat(fullAssemblyPath, @"\DecodeSettings.Exm"));
        }

        /// <summary>
        /// Event po uspesnem nacteni kodu ... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanner_DecodeEvent(object sender, HSM.Embedded.Decoding.DecodeAssembly.DecodeEventArgs e)
        {
            //-------------------------------------
            //--- Process the Decode Event Data ---
            //-------------------------------------
            try
            {
                // je vypnuty scanner, nic nevracet ...
                if (!_enabled)
                    return;

                //HSM.Embedded.Decoding.SymbologyConfigurator.AimID
                //HSM.Embedded.Decoding.SymbologyConfigurator.CodeID
                //HSM.Embedded.Decoding.SymbologyConfigurator.Symbologies.Code16K
                

                
                // nejde zjistit typ caroveho kodu
                //--- Was the Decode Attempt Successful? ---
                if (e.ResultCode == DecodeAssembly.ResultCodes.Success)
                {
                    //--- Display the Decode Data ---
                    //txtDecodeResults.Text = String.Concat(e.Message,
                    //                        "\r\n\r\n Code ID: ", e.CodeId,
                    //                        "\r\n   Aim ID: ", e.AimId,
                    //                        "\r\nAim Mod: ", e.AimModifier,
                    //                        "\r\n  Length: ", e.Length.ToString(),
                    //                        "\r\n   Result: ", e.ResultCode.ToString());
                    //sbStatus.Text = "Success";
                    //barcode = e.Message;
                    // Raise the scan event to the caller (with data)
                    if (DataReady != null)
                    {
                        BarcodeType type = GetBarcodeType(e.CodeId);
                        //DataReady(sender, new ScannerEventArgs(readerData.Text, GetBarcodeType(readerData.Type), readerData.Type.ToString(), (uint)readerData.Length));
                        //DataReady(sender, new ScannerEventArgs(e.Message, BarcodeType.Unknown, BarcodeType.Unknown.ToString(), (uint)e.Message.Length));
                        DataReady(sender, new ScannerEventArgs(e.Message, type, type.ToString(), (uint)e.Message.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private BarcodeType GetBarcodeType(string codeid)
        {
            try
            {
                HSM.Embedded.Decoding.SymbologyConfigurator.CodeID code = (SymbologyConfigurator.CodeID)((int)Convert.ToChar(codeid));
                
                switch (code)
                {
                    case SymbologyConfigurator.CodeID.EAN:
                        return BarcodeType.EAN13;
                    case SymbologyConfigurator.CodeID.EAN8:
                        return BarcodeType.EAN8;
                    case SymbologyConfigurator.CodeID.Code128:
                        return BarcodeType.CODE128;
                    case SymbologyConfigurator.CodeID.Code32:
                        return BarcodeType.CODE32;
                    case SymbologyConfigurator.CodeID.Code39:
                        return BarcodeType.CODE39;
                    case SymbologyConfigurator.CodeID.Code93:
                        return BarcodeType.CODE93;
                    default: return BarcodeType.Unknown;
                }
            }
            catch
            {
                return BarcodeType.Unknown;
            }
        }

        public void TerminateScanner()
        {
            try
            {
                if (scanner != null)
                {
                    // TODO: pridat??
                    scanner.DecodeEvent -= new DecodeComponent.DecodeEventHandler(this.scanner_DecodeEvent);
                    //scanner.Disconnect();
                    scanner.Dispose();
                    scanner = null;
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
                if (scanner != null)
                {
                    this.scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.Concurrent;
                    scanner.Device.AimerOn(true);
                    //scanner.Device.IlluminationOn(false);
                    //scanner.Disconnect();
                    //scanner.Connect();
                    //scanner.DecodeEvent += new DecodeComponent.DecodeEventHandler(this.scanner_DecodeEvent);
                    //scanner.Enabled = true;
                    //scanner.Connect();
                    //scanner.Connect();
                    //scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.
                    //scanner.Device.AimerOn(true);
                    //scanner.Device.IlluminationOn(true);
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
				//if (scanner != null)
				//{
					//this.scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.Concurrent;
					//scanner.Device.AimerOn(true);
					//scanner.Device.IlluminationOn(false);
					//scanner.Disconnect();
					//scanner.Connect();
					//scanner.DecodeEvent += new DecodeComponent.DecodeEventHandler(this.scanner_DecodeEvent);
					//scanner.Enabled = true;
					//scanner.Connect();
					//scanner.Connect();
					//scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.
					//scanner.Device.AimerOn(true);
					//scanner.Device.IlluminationOn(true);
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
            if (scanner != null)
            {
                this.scanner.ScanningLightsMode = DecodeComponent.ScanningLightsModes.None;
                scanner.Device.AimerOn(false);
                //scanner.Device.IlluminationOn(false);
                //scanner.DecodeEvent -= new DecodeComponent.DecodeEventHandler(this.scanner_DecodeEvent);
                //scanner.Disconnect();
                //scanner.Enabled = false;
                //if (false)
                //{
                //    scanner.Device.AimerOn(false);
                //    scanner.Device.IlluminationOn(false);
                //}
                //scanner.Disconnect();
            }
            _enabled = false;
        }

        public event Fask.ScannerProvider.ScannerEventHandler DataReady;

        public void EnableAllBarcodes()
        {
            // Set symbologies to decode
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.Aztec, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.CodaBar, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.Code39, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.Code128, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.DataMatrix, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.EAN13, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.GS1_128, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.Int25, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.MicroPDF, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.PDF417, true);
            this.scanner.EnableSymbology(SymbologyConfigurator.Symbologies.UPCA, true);
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
                Logging.Log.Write("Dolphin60S Scanner Target InvocationList ... Start");
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
                Logging.Log.Write("Dolphin60S Scanner Target InvocationList ... End");
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
