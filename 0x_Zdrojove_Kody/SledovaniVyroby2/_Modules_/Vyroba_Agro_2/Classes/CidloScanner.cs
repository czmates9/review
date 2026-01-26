using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class CidloScanner : Cidlo
    {
        private int _ScannerReadCount = 0;
        public int ScannerReadCount
        {
            get { return this._ScannerReadCount; }
            //set { this._ScannerReadCount = value; }
        }

        private int _ScannerNoReadCount = 0;
        public int ScannerNoReadCount
        {
            get { return this._ScannerNoReadCount; }
            //set { this._ScannerNoReadCount = value; }
        }        

        public CidloScanner()
            : base()
        {
            this._ScannerReadCount = 0;
            this._ScannerNoReadCount = 0;
        }

        public void IncrementScannerReadOrNORead(FASK.SledovaniVyroby.IScannerProvider.Code code)
        {
            switch (code)
            {
                case FASK.SledovaniVyroby.IScannerProvider.Code.Read:
                    this._ScannerReadCount++;
                    base.IncremetSensorCount();
                    break;
                case FASK.SledovaniVyroby.IScannerProvider.Code.NoRead:
                    this._ScannerNoReadCount++;
                    base.IncremetSensorCount();
                    break;
                case FASK.SledovaniVyroby.IScannerProvider.Code.BadRead:
                case FASK.SledovaniVyroby.IScannerProvider.Code.TooLong:
                case FASK.SledovaniVyroby.IScannerProvider.Code.NoData:
                default:
                    break;
            }
        }

        public override void Reset()
        {
            base.Reset();
            this._ScannerReadCount = 0;
            this._ScannerNoReadCount = 0;
        }
    }
}
