using System;
using System.Collections.Generic;
using System.Text;
//using Symbol.Generic;
using Symbol.Barcode;
using Symbol;

namespace Fask.MST_W.Scanner
{
    public sealed class ScannerPT8800 : ScannerBase
    {
        private Symbol.Barcode.Reader _reader = null;
        private Symbol.Barcode.ReaderData _readerData = null;

        public ScannerPT8800()
        {
            try
            {
                this.InitializeScanner();
            }
            catch
            {
            }
        }

        protected override void InitializeScanner()
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

        public override void TerminateScanner()
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

        void _reader_ReadNotify(object sender, EventArgs e)
        {
            ReaderData readerData = _reader.GetNextReaderData();

            // If successful, scan
            if (readerData.Result == Results.SUCCESS)
            {
                // Raise the scan event to the caller (with data)
                if (DataReady != null)
                {
                    DataReady(sender, new ScannerEventArgs(readerData.Text, (uint)readerData.Type, readerData.Type.ToString(), (uint)readerData.Length));
                }

                // Start the next scan
                Enable();
            }
        }

        public override void Enable()
        {
            //base.Enable();
            try
            {
                // If you have both a scanner and data
                if ((_reader != null) && (_readerData != null))
                    // Submit a scan
                    _reader.Actions.Read(_readerData);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }

        public override void Disable()
        {
            //base.Disable();
            // If you have a scanner
            if (_reader != null)
            {
                // Cancel all pending scans
                _reader.Actions.Flush();
            }
        }

        public override event ScannerEventHandler DataReady;

        public override void EnableAllBarcodes()
        {
            this._reader.Decoders.EnableAll();
        }

        public override void BarcodeSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            //Symbol.StandardForms.ScanParamsForm.Run(this._reader);
            Symbol.StandardForms.EnabledDecoders.Run(this._reader);
        }

        public override void ScannerSetting()
        {
            //throw new Exception("The method or operation is not implemented.");
            Symbol.StandardForms.ScanParamsForm.Run(this._reader);
        }

        public override void ScannerSettingLoad()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ScannerSettingSave()
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
