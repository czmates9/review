using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Scanner
{
    public struct RFIDTagData
    {
        public string TagID;
        public string TIDMemory;
        public string EPCMemory;
        public string ReservedMemory;
        public string UserMemory;

        public int CountReaded;
        public int RSSI;

        public string OpCode;
        public string OpStatus;

    }
    public delegate void RFIDTagHandler(object sender, RFIDTagDataEventArgs e);
    public class RFIDTagDataEventArgs : EventArgs
    {
        List<RFIDTagData> _tagList = null;
        public List<RFIDTagData> TagList
        {
            get { return _tagList; }
        }

        public RFIDTagDataEventArgs(List<RFIDTagData> tagList)
        {
            _tagList = tagList;
        }
    }

    public struct RFIDBarcodeData
    {
        public RFIDBarcodeData(string sBarcodeData, uint nBarcodeType, string sBarcodeName, uint nBarcodeLength)
        {
            barcodedata = sBarcodeData;
            barcodetype = nBarcodeType;
            barcodename = sBarcodeName;
            barcodelength = nBarcodeLength;
        }

        //private string barcodedata = string.Empty;
        private string barcodedata;
        public string BarcodeData
        {
            get { return barcodedata; }
        }

        //private uint barcodelength = 0;
        private uint barcodelength;
        public uint BarcodeLength
        {
            get { return barcodelength; }
        }
        //private string barcodename = string.Empty;
        private string barcodename;
        public string BarcodeName
        {
            get { return barcodename; }
        }
        //private uint barcodetype = 0;
        private uint barcodetype;
        public uint BarcodeType
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
                    this.BarcodeType;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    public delegate void ScannerEventRFIDHandler(object sender, ScannerRFIDEventArgs e);
    public class ScannerRFIDEventArgs : EventArgs
    {
        public ScannerRFIDEventArgs(List<RFIDBarcodeData> barCodes)
        {
            //barcodedata = sBarcodeData;
            barcodedata = barCodes;
        }

        private List<RFIDBarcodeData> barcodedata = null;
        public List<RFIDBarcodeData> BarcodeData
        {
            get { return barcodedata; }
        }
    }

    public abstract class ScannerBaseRFID
    {
        public abstract bool Enabled { get; }
        public bool AllMemories { get; set; }

        protected abstract void InitializeScanner();
        public abstract void TerminateScanner();
        public abstract void Enable();
        public abstract void Disable();
        public abstract void StartScan();
        public abstract void StopScan();

        public abstract event ScannerEventRFIDHandler DataReady;
        public abstract event RFIDTagHandler RFIDTagEvent;

        //protected List<string> removedCodes = new List<string>();
        //public abstract List<string> RemovedCodes
        //{
        //    get;
        //    set;
        //}

        //public abstract void SetPower(int power);
        //public abstract int GetPower();
        protected int power = 200;
        public abstract int Power
        {
            get;
            set;
        }

        /// <summary>
        /// Vykon ... ???
        /// </summary>
        protected List<int> powerLevels = new List<int>();
        public abstract List<int> PowerLevels
        {
            get;
        }

        /// <summary>
        /// ??? Source ???
        /// </summary>
        /// <param name="sourceName"></param>
        public abstract void SetSource(String sourceName);
        public abstract List<String> GetSources();

        /// <summary>
        /// Metoda, ktera prevezme interni objekt readeru ke konfiguraci
        /// </summary>
        public abstract void Configure();
    }

   public enum ScannerRFIDTypes
   {
       None,
       //TT8000, 25.10.2016 JiS => odstraneno, nepouziva se ...
       MC9090,
       MC319Z,
       MC319Z_v2
   }
}
