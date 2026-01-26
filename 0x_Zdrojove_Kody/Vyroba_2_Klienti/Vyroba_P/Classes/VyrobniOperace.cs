using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Fask.Vyroba_P.Classes
{
    public class VyrobniOperace
    {
        public VyrobniOperace(int CountEntries,
                        string SOPNUMBE,
                        string ITEMNMBR,
                        string ITEMTYPE,
                        string ITEMDESC,
                        string VNDDOCNMP,
                        string VNDITNUM,
                        int ORD,
                        string BarcodeP,
                        string LOCNCODE,
                        decimal QTYSHPPD,
                        decimal QTYPACK,
                        float TIMEPREP,
                        float TIMEUNIT,
                        byte DtProdT,
                        short DtProdL,
                        byte SerNumT,
                        short SerNumL,
                        byte VerT,
                        short VerL,
                        byte TermID,
                        System.DateTime LSTMod,
                        decimal QTYODVEDENO,
                        decimal CNTODVEDENO,
                        string ITEMMJ,
                        string QTYPACKMJ)
        {
            this.CountEntries = CountEntries;
            this.SOPNUMBE = SOPNUMBE;
            this.ITEMNMBR = ITEMNMBR;
            this.ITEMTYPE = ITEMTYPE;
            this.ITEMDESC = ITEMDESC;
            this.VNDDOCNMP = VNDDOCNMP;
            this.VNDITNUM = VNDITNUM;
            this.ORD = ORD;
            this.BarcodeP = BarcodeP;
            this.LOCNCODE = LOCNCODE;
            this.QTYSHPPD = QTYSHPPD;
            this.QTYPACK = QTYPACK;
            this.TIMEPREP = TIMEPREP;
            this.TIMEUNIT = TIMEUNIT;
            this.DtProdT = DtProdT;
            this.DtProdL = DtProdL;
            this.SerNumT = SerNumT;
            this.SerNumL = SerNumL;
            this.VerT = VerT;
            this.VerL = VerL;
            this.TermID = TermID;
            this.LSTMod = LSTMod;
            this.QTYODVEDENO = QTYODVEDENO;
            this.CNTODVEDENO = CNTODVEDENO;
            this.ITEMMJ = ITEMMJ;
            this.QTYPACKMJ = QTYPACKMJ;
        }

        public VyrobniOperace(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow vpp)
        {            
            this.CountEntries = vpp.CountEntries;
            this.SOPNUMBE = vpp.SOPNUMBE;
            this.ITEMNMBR = vpp.ITEMNMBR;
            this.ITEMTYPE = vpp.ITEMTYPE;
            this.ITEMDESC = vpp.IsITEMDESCNull() ? "?" : vpp.ITEMDESC;
            this.VNDDOCNMP = vpp.IsVNDDOCNMPNull() ? "?" : vpp.VNDDOCNMP;
            this.VNDITNUM = vpp.IsVNDITNUMNull() ? "?" : vpp.VNDITNUM;
            this.ORD = vpp.ORD;
            this.BarcodeP = vpp.BarcodeP;
            this.LOCNCODE = vpp.IsLOCNCODENull() ? "?" : vpp.LOCNCODE;
            this.QTYSHPPD = vpp.QTYSHPPD;
            this.QTYPACK = vpp.QTYPACK;
            this.TIMEPREP = vpp.TIMEPREP;
            this.TIMEUNIT = vpp.TIMEUNIT;
            this.DtProdT = vpp.DtProdT;
            this.DtProdL = vpp.DtProdL;
            this.SerNumT = vpp.SerNumT;
            this.SerNumL = vpp.SerNumL;
            this.VerT = vpp.VerT;
            this.VerL = vpp.VerL;
            this.TermID = vpp.TermID;
            this.LSTMod = vpp.LSTMod;
            this.QTYODVEDENO = vpp.QTYODVEDENO;
            this.CNTODVEDENO = vpp.CNTODVEDENO;
            this.ITEMMJ = vpp.IsITEMMJNull() ? "?" : vpp.ITEMMJ;
            this.QTYPACKMJ = vpp.IsQTYPACKMJNull() ? "?" : vpp.QTYPACKMJ;
        }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Dávka")]
        public int CountEntries { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Příkaz č.")]
        public string SOPNUMBE { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Položka č.")]
        public string ITEMNMBR { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Typ")]
        public string ITEMTYPE { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Popis")]
        public string ITEMDESC { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Dod. dok.")]
        public string VNDDOCNMP { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Dod. č.")]
        public string VNDITNUM { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Pořadí")]
        public int ORD { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Čár. kód")]
        public string BarcodeP { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Lokace")]
        public string LOCNCODE { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Množství")]
        public decimal QTYSHPPD { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Balení")]
        public decimal QTYPACK { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Přípravný čas")]
        public float TIMEPREP { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Jednotkový čas")]
        public float TIMEUNIT { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public byte DtProdT { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public short DtProdL { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public byte SerNumT { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public short SerNumL { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public byte VerT { get; set; }

        [Category("")]
        [ReadOnly(true)]
        public short VerL { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Terminál ID")]
        public byte TermID { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Modifikováno")]
        public DateTime LSTMod { get; set; }

        [Category("")]
        [Browsable(false)]
        public int DEX_ROW_ID { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Množství odv.")]
        public decimal QTYODVEDENO { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Počet odv.")]
        public decimal CNTODVEDENO { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("MJ")]
        public string ITEMMJ { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("MJ Balení")]
        public string QTYPACKMJ { get; set; }
    }
}
