using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class USER
    {
        protected string _memoryHex = string.Empty;
        public string MemoryHex
        {
            get { return _memoryHex; }
            set
            {
                this._memoryHex = value;
                this._memoryBin = Routines_v2.StringHex2StringBin(this._memoryHex);
            }
        }
            

        protected string _memoryBin = string.Empty;
        public string MemoryBin
        {
            get { return this._memoryBin; }
            set
            {
                this._memoryBin = value;
                this._memoryHex = Routines_v2.StringBin2StringHex(this._memoryBin);
            }
        }

        public USER()
        {
        }

        public USER(string userstringHex)
        {
            this.MemoryHex = userstringHex;
        }

        public static USER Parse(string userstringhex)
        {
            return new USER(userstringhex);
        }
    }
}
