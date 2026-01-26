using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.Aktualizace_API.Scanner
{
    public delegate void ScannerEventHandler(object sender, ScannerEventArgs e);

    public class ScannerEventArgs : EventArgs
    {
        public ScannerEventArgs(string sBarcodeData, uint nBarcodeType, string sBarcodeName, uint nBarcodeLength)
        {
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
        private uint barcodetype = 0;
        public uint BarcodeType
        {
            get { return barcodetype; }
        }
    }

    //public class ScannerBase
    //{
    //    protected virtual void InitializeScanner() { }
    //    public virtual void TerminateScanner() { }
    //    public virtual void Enable() { }
    //    public virtual void Disable() { }
    //    public virtual event ScannerEventHandler DataReady;
    //}
    public abstract class ScannerBase
    {
        protected abstract void InitializeScanner();
        public abstract void TerminateScanner();
        public abstract void Enable();
        public abstract void Disable();
        public abstract event ScannerEventHandler DataReady;
        public abstract void EnableAllBarcodes();

        public abstract void ScannerSetting();
        public abstract void BarcodeSetting();

        public abstract void ScannerSettingSave();
        public abstract void ScannerSettingLoad();
    }


    public enum ScannerTypes
    {
        COM,
        None
    }
}
