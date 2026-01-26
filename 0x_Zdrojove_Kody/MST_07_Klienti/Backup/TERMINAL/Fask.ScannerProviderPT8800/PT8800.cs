using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Symbol.Barcode;
using Symbol;
using Fask.ScannerProvider;

namespace Fask.ScannerProviderPT8800
{
    public class ScannerProviderPT8800 : Fask.ScannerProvider.IScannerProvider
    {
        public bool ContinuousRead
        {
            get;
            set;
        }
        private Symbol.Barcode.Reader _reader = null;
        private Symbol.Barcode.ReaderData _readerData = null;

        public ScannerProviderPT8800()
        {
            //try
            //{
            //    this.InitializeScanner();
            //}
            //catch
            //{
            //}
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
                if (_reader == null)
                    return;

                ReaderParams readerParams = _reader.ReaderParameters;
                switch (readerParams.ReaderType)
                {
                    case READER_TYPE.READER_TYPE_IMAGER:
                        readerParams.ReaderSpecific.ImagerSpecific.AimType = (AIM_TYPE)value;
                        break;
                    case READER_TYPE.READER_TYPE_LASER:
                        readerParams.ReaderSpecific.LaserSpecific.AimType = (AIM_TYPE)value;
                        break;
                    case READER_TYPE.READER_TYPE_CONTACT:
                    default:
                        break;
                }

                this.Disable();
                this.Enable();
            }
            get
            {
                if (_reader == null)
                    return AIMTYPE.UNKNOWN;

                ReaderParams readerParameters = _reader.ReaderParameters;
                if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_LASER)
                {
                    return (AIMTYPE)readerParameters.ReaderSpecific.LaserSpecific.AimType;
                }
                else if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_IMAGER)
                {
                    return (AIMTYPE)readerParameters.ReaderSpecific.ImagerSpecific.AimType;
                }
                else if (readerParameters.ReaderType == READER_TYPE.READER_TYPE_CONTACT)
                {
                    return AIMTYPE.UNKNOWN;
                }
                else
                    return AIMTYPE.UNKNOWN;
            }
        }

        public Fask.ScannerProvider.IScannerProvider Scanner
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

        public void InitializeScanner()
        {
            //base.InitializeScanner();
            try
            {
                // If the scanner is already present, fail to initialize
                if (_reader != null)
                    return;

                // Create a new scanner; use the first available scanner
                _reader = new Reader();

                // Create the scanner data
                _readerData = new Symbol.Barcode.ReaderData(ReaderDataTypes.Text, ReaderDataLengths.DefaultText);

                // Create the event handler delegate
                _reader.ReadNotify += new EventHandler(_reader_ReadNotify);

                // Enable the scanner with a wait cursor
                _reader.Actions.Enable();

                // Set up the scanner
                //_reader.Parameters.Feedback.Success.BeepTime = 0;
                //_reader.Parameters.Feedback.Success.WaveFile = "\\windows\\alarm3.wav";
                //_reader.Parameters.ScanType = ScanTypes.Background;

                this.EnableAllBarcodes();

                return;
            }
            catch
            {
            }
        }

        void _reader_ReadNotify(object sender, EventArgs e)
        {
            ReaderData readerData = _reader.GetNextReaderData();

            // If successful, scan
            if (readerData.Result == Results.SUCCESS)
            {
                // Raise the scan event to the caller (with data)
                if (DataReady != null)
                {
                    DataReady(sender, new ScannerEventArgs(readerData.Text, (BarcodeType)readerData.Type, readerData.Type.ToString(), (uint)readerData.Length));
                }

                // Start the next scan
                Enable();
            }
        }

        public void TerminateScanner()
        {
            //base.TerminateScanner();
            // If you have a scanner
            if (_reader != null)
            {
                // Disable the scanner
                _reader.Actions.Disable();

                // Free it up
                _reader.Dispose();

                // Indicate that you no longer have a scanner
                _reader = null;
            }

            // If you have a scanner data object
            if (_readerData != null)
            {
                // Free it up
                _readerData.Dispose();

                // Indicate that you no longer have a scanner
                _readerData = null;
            }
        }

        public void Enable()
        {
            //base.Enable();
            try
            {
                // If you have both a scanner and data
                if ((_reader != null) && (_readerData != null))
                {
                    // Submit a scan
                    _reader.Actions.Read(_readerData);
                    _enabled = true;
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }

		//public void Enable(bool toggleSoftTrigger)
		//{
			//base.Enable();
			//try
			//{
				// If you have both a scanner and data
				//if ((_reader != null) && (_readerData != null))
				//{
					// Submit a scan
					//_reader.Actions.Read(_readerData);
					//_enabled = true;
				//}
			//}
			//catch (Exception ex)
			//{
				//string a = ex.Message;
			//}
		//}

        public void Disable()
        {
            //base.Disable();
            // If you have a scanner
            if (_reader != null)
            {
                // Cancel all pending scans
                _reader.Actions.Flush();
            }
            _enabled = false;
        }

        public event Fask.ScannerProvider.ScannerEventHandler DataReady;

        public void EnableAllBarcodes()
        {
            this._reader.Decoders.EnableAll();
        }

        public void ScannerSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            Symbol.StandardForms.ScanParamsForm.Run(this._reader);
        }

        public void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            //Symbol.StandardForms.ScanParamsForm.Run(this._reader);
            Symbol.StandardForms.EnabledDecoders.Run(this._reader);
        }

        public void ScannerSettingSave()
        {
           // throw new NotImplementedException();
        }

        public void ScannerSettingLoad()
        {
            ///throw new NotImplementedException();
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
            //throw new NotImplementedException();
        }

        public void Log_DataReady_Events()
        {
            try
            {
                Logging.Log.Write("PT8800 Scanner Target InvocationList ... Start");
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
                Logging.Log.Write("PT8800 Scanner Target InvocationList ... End");
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
