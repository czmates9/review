using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W
{
    public class TiskData
    {
        public TiskData()
        {

        }

        public string CountEntries
        {
            get { return _CountEntries; }
            set { _CountEntries = value; }
        }
        public string PONUMBER
        {
            get { return _PONUMBER; }
            set { _PONUMBER = value; }
        }
        public string ORD
        {
            get { return _ORD; }
            set { _ORD = value; }
        }
        public string ITEMNMBR
        {
            get { return _ITEMNMBR; }
            set { _ITEMNMBR = value; }
        }
        public string VNDDOCNM
        {
            get { return _VNDDOCNM; }
            set { _VNDDOCNM = value; }
        }
        public string VNDITNUM
        {
            get { return _VNDITNUM; }
            set { _VNDITNUM = value; }
        }
        public string LOCNCODE
        {
            get { return _LOCNCODE; }
            set { _LOCNCODE = value; }
        }
        public string QTYSHPPD
        {
            get { return _QTYSHPPD; }
            set { _QTYSHPPD = value; }
        }
        public string QTYPACK
        {
            get { return _QTYPACK; }
            set { _QTYPACK = value; }
        }
        public string SERLTNUM
        {
            get { return _SERLTNUM; }
            set { _SERLTNUM = value; }
        }
        public string KOD_SW
        {
            get { return _KOD_SW; }
            set { _KOD_SW = value; }
        }
        public string DAT_VYROBY
        {
            get { return _DAT_VYROBY; }
            set { _DAT_VYROBY = value; }
        }
        public string DATEDONE
        {
            get { return _DATEDONE; }
            set { _DATEDONE = value; }
        }
        public string TIMEDONE
        {
            get { return _TIMEDONE; }
            set { _TIMEDONE = value; }
        }
        public string CZ_CarKod
        {
            get { return _CZ_CarKod; }
            set { _CZ_CarKod = value; }
        }
        public string REZ_1
        {
            get { return _REZ_1; }
            set { _REZ_1 = value; }
        }
        public string REZ_2
        {
            get { return _REZ_2; }
            set { _REZ_2 = value; }
        }

        private string _CountEntries = null;
        private string _PONUMBER = null;
        private string _ORD = null;
        private string _ITEMNMBR = null;
        private string _VNDDOCNM = null;
        private string _VNDITNUM = null;
        private string _LOCNCODE = null;
        private string _QTYSHPPD = null;
        private string _QTYPACK = null;
        private string _SERLTNUM = null;
        private string _KOD_SW = null;
        private string _DAT_VYROBY = null;
        private string _DATEDONE = null;
        private string _TIMEDONE = null;
        private string _CZ_CarKod = null;
        private string _REZ_1 = null;
        private string _REZ_2 = null;

        public List<string> ToList()
        {
            List<string> seznam = new List<string>();
            seznam.Add(CountEntries);
            seznam.Add(PONUMBER);
            seznam.Add(ORD);
            seznam.Add(ITEMNMBR);
            seznam.Add(VNDDOCNM);
            seznam.Add(VNDITNUM);
            seznam.Add(LOCNCODE);
            seznam.Add(QTYSHPPD);
            seznam.Add(QTYPACK);
            seznam.Add(SERLTNUM);
            seznam.Add(KOD_SW);
            seznam.Add(DAT_VYROBY);
            seznam.Add(DATEDONE);
            seznam.Add(TIMEDONE);
            seznam.Add(CZ_CarKod);
            seznam.Add(REZ_1);
            seznam.Add(REZ_2);

            return seznam;
        }

        public static TiskData Parse(string line)
        {
            try
            {
                string[] data = new string[0];
                CSV csv = new CSV();
                csv.Parse(line, ref data);
                TiskData odb = new TiskData();
                odb.CountEntries = data[0];
                odb.PONUMBER = data[1];
                odb.ORD = data[2];
                odb.ITEMNMBR = data[3];
                odb.VNDDOCNM = data[4];
                odb.VNDITNUM = data[5];
                odb.LOCNCODE = data[6];
                odb.QTYSHPPD = data[7];
                odb.QTYPACK = data[8];
                odb.SERLTNUM = data[9];
                odb.KOD_SW = data[10];
                odb.DAT_VYROBY = data[11];
                odb.DATEDONE = data[12];
                odb.TIMEDONE = data[13];
                odb.CZ_CarKod = data[14];
                odb.REZ_1 = data[15];
                odb.REZ_2 = data[16];
                return odb;
            }
            catch { return null; }
        }

        public string ToCsv()
        {
            CSV csv = new CSV();
            return csv.ToCsv(
                new string[] { 
                    this.CountEntries,
                    this.PONUMBER,
                    this.ORD,
                    this.ITEMNMBR,
                    this.VNDDOCNM,
                    this.VNDITNUM,
                    this.LOCNCODE,
                    this.QTYSHPPD,
                    this.QTYPACK,
                    this.SERLTNUM,
                    this.KOD_SW,
                    this.DAT_VYROBY,
                    this.DATEDONE,
                    this.TIMEDONE,
                    this.CZ_CarKod,
                    this.REZ_1,
                    this.REZ_2
                }
            );
        }
    }
}
