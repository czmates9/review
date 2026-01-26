using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Classes
{
    public class GenerovaniSN
    {

        // Info o polozke
        public int CountEntries;
        public string SOPNUMBE;
        public string ITEMNMBR;
        public string ITEMDESC;
        public string ITEMMJ;
        public string VNDITNUM;
        public decimal QTYSHPPD;
        public decimal QTYPACK;
        public int TIMEMODE;
        public float TIMEPREP;
        public float TIMEUNIT;
        public byte SerNumT;
        public byte BarcodeT;


        //Info z generovani
        public int OD;
        public int POCET;
        public int N;
        public string PREFIX;

        public byte CZ_Rez1_Track;
        public byte CZ_Rez2_Track;
        public byte CZ_Rez3_Track;
        public byte CZ_Rez4_Track;
        public byte CZ_Rez5_Track;

        public decimal? WEIGHT_TARA;
        public decimal? WEIGHT_NETTO;
        public decimal? WEIGHT_TOL_PLUS;
        public decimal? WEIGHT_TOL_MINUS;

        public string QTYPACKMJ;

    }
}
