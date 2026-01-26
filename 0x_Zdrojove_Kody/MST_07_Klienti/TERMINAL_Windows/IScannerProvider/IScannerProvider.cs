using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.MST_WINDOWS.IScannerProvider
{
    public interface IScannerProvider
    {
        bool Enabled { get; }
        string ConfigScannerPath { get; set; }


        FASK.MST_WINDOWS.IScannerProvider.IScannerProvider Scanner { get; set; }

        void InitializeScanner();
        void TerminateScanner();
        void Enable();
        void Disable();
        event ScannerEventHandler DataReady;
        void EnableAllBarcodes();

        void SaveScannerReadCount();

        void ScannerSetting();
        void BarcodeSetting();

        void ScannerSettingSave();
        void ScannerSettingLoad();
    }

    public delegate void ScannerEventHandler(object sender, ScannerEventArgs e);

    public enum Code { NoData = 1, Read, NoRead, BadRead, TooLong };

    public enum ScannerTypes
    {
        None,
        Unitech_HT660,
        Symbol_MC3090
    }

    public enum BarcodeType
    {
        Unknown = 0,
        EAN128 = 1,
        EAN13 = 2,
        EAN8 = 3,
        CODE128 = 4,
        CODE32 = 5,
        CODE39 = 6,
        CODE93 = 7,
        I2OF5 = 8
    }

    public class ScannerEventArgs : EventArgs
    {
        public ScannerEventArgs(string sBarcodeData, BarcodeType nBarcodeType, string sBarcodeName, uint nBarcodeLength, Code nScannedCodeType )
        {
            scannedCodeType = nScannedCodeType;
            barcodedata = sBarcodeData;
            barcodetype = nBarcodeType;
            barcodename = sBarcodeName;
            barcodelength = nBarcodeLength;
        }

        private string barcodedata = string.Empty;
        public string BarcodeData
        {
            get { return barcodedata; }
        }
        private uint barcodelength = 0;
        public uint BarcodeLength
        {
            get { return barcodelength; }
        }
        private string barcodename = string.Empty;
        public string BarcodeName
        {
            get { return barcodename; }
        }

        private Code scannedCodeType = Code.NoData;
        public Code ScannedCodeType
        {
            get { return scannedCodeType; }
        }

        private BarcodeType barcodetype = BarcodeType.Unknown;
        public BarcodeType BarcodeType
        {
            get { return barcodetype; }
        }

        public override string ToString()
        {
            try
            {
                return
                    this.BarcodeData.Trim() + "\r\n" +
                    this.BarcodeLength + "\r\n" +
                    this.BarcodeName.Trim() + "\r\n" +
                    this.BarcodeType.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
