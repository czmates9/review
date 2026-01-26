using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    /// <summary>
    /// The SGTIN-96 encoding allows for numeric-only
    /// serial numbers, without leading zeros, whose value is less than 238 (that is, from 0 through
    /// 274,877,906,943, inclusive).
    /// Total bits:         96
    /// EPC Header:          8
    /// Filter:              3
    /// Partition:           3
    /// GS1 Company Pr:     20-40
    /// Indicator/ItemRef:  24-4
    /// Serial:             38 (2^38)
    /// </summary>
    public class EPC_SGTIN96 : EPC
    {
        public const string EPCCodeHeaderValueSGTIN96 = "00110000";

        public EPC_SGTIN96() : base()
        {
            this.MemoryBin = (new StringBuilder()).Append('0', 128).ToString();
        }

        public EPC_SGTIN96(string epchex) : base(epchex)
        {
            //this.EPCCodeHeaderValue = EPCCodeHeaderValueSGTIN96;
            //this.PC_Length = 96;
            //this.PC_AttributeBits = 0;
            //this.PC_AttributeBits_IsHazardousMaterial = false;
            //this.PartionObject = SGTIN_Partition_Default;            
        }


        #region Prvky ze kterych se kod EPC sklada
        //protected string ean = string.Empty.PadLeft(13, '0');
        /// <summary>
        /// Ean kod pro ktery se generuje EPC
        /// </summary>
        public string EAN
        {
            get
            {
                string kod =
                    CompanyPrefix.PadLeft(PartitionObject.CompanyPrefix_Digits_L, '0') +
                    ItemRef.PadLeft(PartitionObject.IndicatorXItemRef_Digits_D - 1, '0');
                string checkdigit = Classes.BarCodes.CalculateCheckDigit(kod);
                return kod + checkdigit;                    
            }
            set
            {
                Indicator = EAN14(value).Substring(0, 1);
                CompanyPrefix = EAN14(value).Substring(1, PartitionObject.CompanyPrefix_Digits_L);
                ItemRef = EAN14(value).Substring(PartitionObject.CompanyPrefix_Digits_L + 1, PartitionObject.IndicatorXItemRef_Digits_D - 1);
            }
        }

        protected string EAN14(string ean)
        {
            // TODO : howto making ean ...
            if (ean.Length == 13 || ean.Length == 12 || ean.Length == 14) // EAN13, EAN12, EAN14
            {
                return ean.PadLeft(14, '0');
            }
            else if (ean.Length == 8) // EAN8
            {
                return ean.PadLeft(14, '0');
            }
            else
            {
                throw new RFID.EPCMemoryFormatException("Not EAN13 or EAN8");
            }            
        }

        //protected string companyPrefix = string.Empty;
        /// <summary>
        /// Company Prefix: retezec v decimalnim formatu
        /// </summary>
        public string CompanyPrefix
        {
            //get { return EAN.Substring(0, CompanyPrefixLength); }
            //set { EAN = EAN.RangeReplace(0, 0 + CompanyPrefixLength - 1, value); }
            get { return Convert.ToInt64(GTIN.Substring(3, PartitionObject.CompanyPrefix_Bits_M), 2).ToString(); }
            set
            {
                GTIN = GTIN.RangeReplace(
                    3,
                    3 + PartitionObject.CompanyPrefix_Bits_M - 1,
                    Convert.ToString(long.Parse(value), 2).PadLeft(PartitionObject.CompanyPrefix_Bits_M, '0')
                    );
            }
        }

        /// <summary>
        /// Indicator number
        /// </summary>
        public string Indicator
        {
            get { return IndicatorXItemRef.Substring(0, 1); }
            set { IndicatorXItemRef = IndicatorXItemRef.RangeReplace(0, 0, value); }
        }

        /// <summary>
        /// Item reference number: retezec v decimalnim formatu
        /// </summary>
        public string ItemRef
        {
            get { return long.Parse(IndicatorXItemRef.Substring(1)).ToString(); }
            set { IndicatorXItemRef = IndicatorXItemRef.Substring(0, 1) + Convert.ToString(long.Parse(value), 10).PadLeft(PartitionObject.IndicatorXItemRef_Digits_D - 1, '0'); }
        }

        //protected string itemRef = string.Empty;
        /// <summary>
        /// Indicaotr a Item reference number : v decimalnim formatu
        /// </summary>
        public string IndicatorXItemRef
        {
            //get { return EAN.Substring(CompanyPrefixLength, EAN.Length - CompanyPrefixLength; }
            //set { EAN = EAN.RangeReplace(CompanyPrefixLength, EAN.Length - CompanyPrefixLength - 1, value); }
            get { return Convert.ToInt64(GTIN.Substring(3 + PartitionObject.CompanyPrefix_Bits_M, PartitionObject.IndicatorXItemRef_Bits_N), 2).ToString().PadLeft(PartitionObject.IndicatorXItemRef_Digits_D, '0'); }
            set
            {
                GTIN = GTIN.RangeReplace(
                    3 + PartitionObject.CompanyPrefix_Bits_M,
                    3 + PartitionObject.CompanyPrefix_Bits_M + PartitionObject.IndicatorXItemRef_Bits_N - 1,
                    Convert.ToString(Convert.ToInt64(value, 10), 2).PadLeft(PartitionObject.IndicatorXItemRef_Bits_N, '0')
                    );
            }
        }

        /// <summary>
        /// Seriove cislo
        /// </summary>
        public long Serial
        {
            //get { return this.EPCCode.Substring(96 - 1 - 38, 38); }
            //set { this.EPCCode.RangeReplace(96 - 1 - 38, 96 - 1, value.PadLeft(38, '0')); }
            get { return Convert.ToInt64(this.EPCCode.Substring(58, 38), 2); } //8+3+3+47 = 58 (58+38=96)
            set
            {
                EPCCode = EPCCode.RangeReplace(
                    58, // 8+3+3+47 = 58 (58+38=96)
                    58 + 38 - 1,
                    Convert.ToString(value, 2).PadLeft(38, '0')
                    );
            }

        }

        #endregion

        /// <summary>
        /// typy filtru
        /// </summary>
        public enum filterValues : byte
        {
            AllOthers = 0,
            PointOfSale = 1,
            FullCaseTransport = 2,
            ReservedA = 3,
            InnerPackTradeItemGroupingHandling = 4,
            ReservedB = 5,
            UnitLoad = 6,
            UnitInsideTradeItem = 7
        }

        public struct SGTIN_Partition
        {
            public byte PartitionValue_P;
            public int CompanyPrefix_Bits_M;
            public int CompanyPrefix_Digits_L;
            public int IndicatorXItemRef_Bits_N;
            public int IndicatorXItemRef_Digits_D;
            public static SGTIN_Partition Default
            {
                get { return new SGTIN_Partition(5, 24, 7, 20, 6);  }
            }
            public SGTIN_Partition(byte P, int M, int L, int N, int D)
            {
                this.PartitionValue_P = P;
                this.CompanyPrefix_Bits_M = M;
                this.CompanyPrefix_Digits_L = L;
                this.IndicatorXItemRef_Bits_N = N;
                this.IndicatorXItemRef_Digits_D = D;
            }
        }
        //public static SGTIN_Partition SGTIN_Partition_Default = new SGTIN_Partition(5, 24, 7, 20, 6);

        /// <summary>
        /// Rozdeleni partion na casti CP a I+IR
        /// </summary>
        protected internal System.Collections.Generic.Dictionary<int, SGTIN_Partition> SGTIN_Partition_Table = new Dictionary<int, SGTIN_Partition>()
        {
            {0, new SGTIN_Partition(0, 40, 12,  4, 1) },
            {1, new SGTIN_Partition(1, 37, 11,  7, 2) },
            {2, new SGTIN_Partition(2, 34, 10, 10, 3) },
            {3, new SGTIN_Partition(3, 30,  9, 14, 4) },
            {4, new SGTIN_Partition(4, 27,  8, 17, 5) },
            {5, SGTIN_Partition.Default },
            {6, new SGTIN_Partition(6, 20,  6, 24, 7) }
        };

        /// <summary>
        /// Partition hodnota v binarnim retezci ...
        /// </summary>
        public string Partition
        {
            get { return this.GTIN.Substring(0, 3); }
            set
            {
                GTIN = GTIN.RangeReplace(0, 0 + 3 - 1, value);
            }
        }

        public SGTIN_Partition PartitionObject
        {
            get { return SGTIN_Partition_Table[Convert.ToInt32(Partition, 2)]; }
            set
            {
                Partition = Convert.ToString(value.PartitionValue_P, 2).PadLeft(3, '0');
            }
        }

        /// <summary>
        /// Filter value v binarnim retezci ...
        /// </summary>
        public byte FilterValue
        {
            get { return Convert.ToByte(this.EPCCode.Substring(8, 3)); }
            set
            {
                if (value <= 7)
                {
                    this.EPCCode = this.EPCCode.RangeReplace(8, 8 + 3 - 1, Convert.ToString(value, 2).PadLeft(3, '0'));
                }
                else
                    throw new EPCMemoryFormatException("Filter value must be in range: 0 <= filter <= 7"); 
                
            }
        }

        /// <summary>
        /// Gtin v binarnim retezci ...
        /// </summary>
        public string GTIN
        {
            get { return this.EPCCode.Substring(11, 47); }
            set { this.EPCCode = this.EPCCode.RangeReplace(11, 11 + 47 - 1, value); } // value length = 85bits
        }

        public int CheckDigit
        {
            get
            {
                return (10 - ((
                    3 * (
                        Convert.ToInt32(Indicator[0]) //d1
                        + Convert.ToInt32(CompanyPrefix[1]) //d3 
                        + Convert.ToInt32(CompanyPrefix[3]) //d5
                        + Convert.ToInt32(CompanyPrefix[5]) //d7 
                        + Convert.ToInt32(ItemRef[0]) //d9 
                        + Convert.ToInt32(ItemRef[2]) //d11
                        + Convert.ToInt32(ItemRef[4]) //d13
                        )
                    + (
                        Convert.ToInt32(CompanyPrefix[0]) //d2 
                        + Convert.ToInt32(CompanyPrefix[2]) //d4 
                        + Convert.ToInt32(CompanyPrefix[4]) //d6 
                        + Convert.ToInt32(CompanyPrefix[6]) //d8
                        + Convert.ToInt32(ItemRef[1]) //d10
                        + Convert.ToInt32(ItemRef[3]) //d12 
                        )
                      ) % 10)) % 10;
            }
        }

        protected override void decodeMemory()
        {
            base.decodeMemory();
        }

        public override string ToEPCURI()
        {
            return
                "urn:epc:tag:sgtin-96:" +
                FilterValue + "." +
                CompanyPrefix + "." +
                Indicator + ItemRef + "." +
                Serial.ToString();
        }

    }
}
