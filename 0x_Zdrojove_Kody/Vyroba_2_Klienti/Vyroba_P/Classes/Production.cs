using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Vyroba_P.Classes
{
    public class Production
    {
        public Production(
            int CountEntries,
            string SOPNUMBE,
            string ITEMNMBR,
            int ORD,
            float TIMEPREP,
            float TIMEUNIT,
            System.DateTime TIMESTART,
            System.DateTime TIMESTOP,
            float TIMECOR,
            int TIMECRID,
            string loginid,
            string machineid,
            System.DateTime dateeve,
            decimal qty,
            decimal qtyReal,
            string description,
            string BarcodeP,
            string UserID,
            byte TermID,
            System.DateTime ISOK,
            System.Guid GUID,
            string ITEMTYPE,
            string ITEMMJ,
            decimal QTYPACK,
            string QTYPACKMJ)
        {
            this.CountEntries = CountEntries;
            this.SOPNUMBE = SOPNUMBE;
            this.ITEMNMBR = ITEMNMBR;
            this.ORD = ORD;
            this.TIMEPREP = TIMEPREP;
            this.TIMEUNIT = TIMEUNIT;
            this.TIMESTART = TIMESTART;
            this.TIMESTOP = TIMESTOP;
            this.TIMECOR = TIMECOR;
            this.TIMECRID = TIMECRID;
            this.loginid = loginid;
            this.machineid = machineid;
            this.dateeve = dateeve;
            this.qty = qty;
            this.qtyReal = qtyReal;
            this.description = description;
            this.BarcodeP = BarcodeP;
            this.UserID = UserID;
            this.TermID = TermID;
            this.ISOK = ISOK;
            this.GUID = GUID;
            this.ITEMTYPE = ITEMTYPE;
            this.ITEMMJ = ITEMMJ;
            this.QTYPACK = QTYPACK;
            this.QTYPACKMJ = QTYPACKMJ;
        }

        public Production(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow prow)
        {
            this.CountEntries = prow.CountEntries;
            this.SOPNUMBE = prow.IsSOPNUMBENull() ? "?" : prow.SOPNUMBE;
            this.ITEMNMBR = prow.ITEMNMBR;
            this.ORD = prow.ORD;
            this.TIMEPREP = prow.TIMEPREP;
            this.TIMEUNIT = prow.TIMEUNIT;
            this.TIMESTART = prow.IsTIMESTARTNull() ? DateTime.MinValue : prow.TIMESTART;
            this.TIMESTOP = prow.TIMESTOP;
            this.TIMECOR = prow.TIMECOR;
            this.TIMECRID = prow.IsTIMECRIDNull() ? 0 : prow.TIMECRID;
            this.loginid = prow.loginid;
            this.machineid = prow.machineid;
            this.dateeve = prow.dateeve;
            this.qty = prow.qty;
            this.qtyReal = prow.qtyReal;
            this.description = prow.IsdescriptionNull() ? "?" : prow.description;
            this.BarcodeP = prow.BarcodeP;
            this.UserID = prow.UserID;
            this.TermID = prow.TermID;
            this.ISOK = prow.IsISOKNull() ? DateTime.MinValue : prow.ISOK;
            this.GUID = prow.GUID;
            this.ITEMTYPE = prow.ITEMTYPE;
            this.ITEMMJ = prow.IsITEMMJNull() ? "?" : prow.ITEMMJ;
            this.QTYPACK = prow.QTYPACK;
            this.QTYPACKMJ = prow.IsQTYPACKMJNull() ? "?" : prow.QTYPACKMJ;
        }


        public int CountEntries{ get; set; }
        public string SOPNUMBE{ get; set; }
        public string ITEMNMBR{ get; set; }
        public int ORD{ get; set; }
        public float TIMEPREP{ get; set; }
        public float TIMEUNIT{ get; set; }
        public System.DateTime TIMESTART{ get; set; }
        public System.DateTime TIMESTOP{ get; set; }
        public float TIMECOR{ get; set; }
        public int TIMECRID{ get; set; }
        public string loginid{ get; set; }
        public string machineid{ get; set; }
        public System.DateTime dateeve{ get; set; }
        public decimal qty{ get; set; }
        public decimal qtyReal{ get; set; }
        public string description{ get; set; }
        public string BarcodeP{ get; set; }
        public string UserID{ get; set; }
        public byte TermID{ get; set; }
        public System.DateTime ISOK{ get; set; }
        public System.Guid GUID{ get; set; }
        public string ITEMTYPE{ get; set; }
        public string ITEMMJ{ get; set; }
        public decimal QTYPACK{ get; set; }
        public string QTYPACKMJ{ get; set; }

        public override string ToString()
        {
            string s = string.Empty;
            s += "CountEntries: " + this.CountEntries + "\n";
            s += "SOPNUMBE: " + this.SOPNUMBE + "\n";
            s += "ITEMNMBR: " + this.ITEMNMBR + "\n";
            s += "ORD: " + this.ORD + "\n";
            s += "TIMEPREP: " + this.TIMEPREP + "\n";
            s += "TIMEUNIT: " + this.TIMEUNIT + "\n";
            s += "TIMESTART: " + this.TIMESTART + "\n";
            s += "TIMESTOP: " + this.TIMESTOP + "\n";
            s += "TIMECOR: " + this.TIMECOR + "\n";
            s += "TIMECRID: " + this.TIMECRID + "\n";
            s += "loginid: " + this.loginid + "\n";
            s += "machineid: " + this.machineid + "\n";
            s += "dateeve: " + this.dateeve + "\n";
            s += "qty: " + this.qty + "\n";
            s += "qtyReal: " + this.qtyReal + "\n";
            s += "description: " + this.description + "\n";
            s += "BarcodeP: " + this.BarcodeP + "\n";
            s += "UserID: " + this.UserID + "\n";
            s += "TermID: " + this.TermID + "\n";
            s += "ISOK: " + this.ISOK + "\n";
            s += "GUID: " + this.GUID + "\n";
            s += "ITEMTYPE: " + this.ITEMTYPE + "\n";
            s += "ITEMMJ: " + this.ITEMMJ + "\n";
            s += "QTYPACK: " + this.QTYPACK + "\n";
            s += "QTYPACKMJ: " + this.QTYPACKMJ;
            return s;
        }
    }
}
