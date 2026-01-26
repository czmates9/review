using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Fask.Aktualizace_API.Classes
{
    public class VyrobniPrikaz
    {
        public VyrobniPrikaz(int CountEntries, string SOPNUMBE, string SOPTYPE, string SOPDESC, string VNDDOCNMH, string BarcodeH, string LOCNCODE, short DateProd, string Rez1, string Rez2, byte TermID, System.DateTime LSTMod)
        {
            this.CountEntries = CountEntries;
            this.SOPNUMBE = SOPNUMBE;
            this.SOPDESC = SOPDESC;
            this.SOPTYPE = SOPTYPE;
            this.VNDDOCNMH = VNDDOCNMH;
            this.BarcodeH = BarcodeH;
            this.LOCNCODE = LOCNCODE;
            this.DateProd = DateProd;
            this.Rez1 = Rez1;
            this.Rez2 = Rez2;
            this.TermID = TermID;
            this.LSTMod = LSTMod;
        }

        public VyrobniPrikaz(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow vph)
        {
            this.CountEntries = vph.CountEntries;
            this.SOPNUMBE = vph.SOPNUMBE;
            this.SOPTYPE = vph.SOPTYPE;
            this.SOPDESC = vph.IsSOPDESCNull() ? "?" : vph.SOPDESC;
            this.VNDDOCNMH = vph.IsVNDDOCNMHNull() ? "?" : vph.VNDDOCNMH;
            this.BarcodeH = vph.BarcodeH;
            this.LOCNCODE = vph.IsLOCNCODENull() ? "?" : vph.LOCNCODE;
            this.DateProd = vph.DateProd;
            this.Rez1 = vph.Rez1;
            this.Rez2 = vph.Rez2;
            this.TermID = vph.TermID;
            this.LSTMod = vph.LSTMod;
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
        [DisplayName("Typ")]
        public string SOPTYPE { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Popis")]
        public string SOPDESC { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Dod. dok.")]
        public string VNDDOCNMH { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Čár. kód")]
        public string BarcodeH { get; set; }

        [ReadOnly(true)]
        [DisplayName("Lokace")]
        public string LOCNCODE { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Datum")]
        public short DateProd { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Rez1")]
        public string Rez1 { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Rez2")]
        public string Rez2 { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Terminál ID")]
        public byte TermID { get; set; }

        [Category("")]
        [ReadOnly(true)]
        [DisplayName("Modifikováno")]
        public System.DateTime LSTMod { get; set; }
    }
}
