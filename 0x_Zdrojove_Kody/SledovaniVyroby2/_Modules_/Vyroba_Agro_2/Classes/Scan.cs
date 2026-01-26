using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FASK.SledovaniVyroby.IScannerProvider;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class Scan
    {
        private DateTime readtime;
        public DateTime ReadTime
        {
            get { return this.readtime; }
        }
        private Code readresult = FASK.SledovaniVyroby.IScannerProvider.Code.NoData;
        public Code ReadResult
        {
            get { return this.readresult; }
        }
        private string readbarcode = string.Empty;
        public string ReadBarcode
        {
            get { return this.readbarcode; }
            set { this.readbarcode = value; }
        }

        public Scan(DateTime readtime, Code readresult, string readbarcode)
        {
            this.readtime = readtime ;
            this.readresult = readresult;
            this.readbarcode = readbarcode ?? string.Empty;
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.readtime.ToString() + " R:" + this.readresult.ToString() + " K:" + this.readbarcode;
        }
    }
}
