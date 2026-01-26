using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class EPC
    {
        public EPC()
        {
        }

        public EPC(string epchex)
        {
            this.MemoryHex = epchex;
        }

        #region Cely obsah pameti EPCMemory tagu
        protected string memoryHex = string.Empty;
        /// <summary>
        /// Cely obsah EPCMemory v hexadecimalnim formatu
        /// </summary>
        public string MemoryHex
        {
            get { return memoryHex; }
            set
            {
                memoryHex = value;
                memoryBin = RFID.Routines_v2.StringHex2StringBin(memoryHex);
                this.decodeMemory();
            }
        }


        protected string memoryBin = string.Empty;
        /// <summary>
        /// Cely obsah EPCMemory v binarnim formatu
        /// </summary>
        public string MemoryBin
        {
            get { return memoryBin; }
            set
            {
                memoryBin = value;
                memoryHex = RFID.Routines_v2.StringBin2StringHex(memoryBin);
                this.decodeMemory();
            }
        }
        #endregion

        #region Hlavni slozky EPC Memory
        //private ushort crc = 0;
        /// <summary>
        /// CRC hodnota pri nacitani z EPC Memory
        /// 00h – 0Fh
        /// A 16-bit Cyclic Redundancy Check
        /// computed over the contents of the
        /// EPC bank.
        /// </summary>
        public ushort CRC
        {
            get { return Convert.ToUInt16(memoryBin.Range(0x00, 0x0F), 2); }
            set { MemoryBin = memoryBin.RangeReplace(0x00, 0x0F, Convert.ToString(value, 2).PadLeft(16, '0')); }
        }

        //private ushort pc = 0;
        /// <summary>
        /// 10h – 1Fh
        /// Protocol Control bits
        /// </summary>
        public ushort PC
        {
            get { return Convert.ToUInt16(memoryBin.Range(0x10, 0x1F), 2); }
            set { MemoryBin = memoryBin.RangeReplace(0x10, 0x1F, Convert.ToString(value, 2).PadLeft(16, '0')); }
        }

        //private ushort length = 0;
        /// <summary>
        /// Represents the number of 16-bit words comprising the
        /// PC field and the EPC field (below). See discussion in
        /// Section 15.1.1 for the encoding of this field.
        /// </summary>
        public int PC_Length
        {
            get { return Convert.ToUInt16(memoryBin.Range(0x10, 0x14), 2) * 16; }
            set { MemoryBin = memoryBin.RangeReplace(0x10, 0x14, Convert.ToString(value / 16, 2).PadLeft(5, '0')); }
        }

        /// <summary>
        /// Indicates whether the user memory bank is present and
        /// contains data.
        /// </summary>
        public bool PC_UMIIndicator
        {
            get { return memoryBin.Range(0x15, 0x15) == "1"; }
            set { MemoryBin = memoryBin.RangeReplace(0x15, 0x15, value ? "1" : "0"); }
        }

        /// <summary>
        /// Indicates whether an XPC is present
        /// </summary>
        public bool PC_XPCIndicator
        {
            get { return memoryBin.Range(0x16, 0x16) == "1"; }
            //set { }
        }

        /// <summary>
        /// If one, indicates a non-EPCglobal application; in
        /// particular, indicates that bits 18h – 1Fh contain the ISO
        /// Application Family Identifier (AFI) as defined in
        /// [ISO15961] and the remainder of the EPC bank contains
        /// a Unique Item Identifier (UII) appropriate for that AFI.
        /// </summary>
        /// <remarks>always zero for EPC</remarks>
        public bool PC_Toggle
        {
            get { return memoryBin.Range(0x17, 0x17) == "1"; }
            set { MemoryBin = memoryBin.RangeReplace(0x17, 0x17, value ? "1" : "0"); }
        }

        /// <summary>
        /// Bits that may guide the handling of the physical object to
        /// which the tag is affixed. (Applies to Gen2 v 1.x tags
        /// only.)
        /// </summary>
        /// <remarks>if toggle = 0</remarks>
        public byte PC_AttributeBits
        {
            get
            {
                if (!PC_Toggle)
                    return Convert.ToByte(memoryBin.Range(0x18, 0x1F), 2);
                else
                    return 0;
            }
            set
            {
                if (!PC_Toggle)
                    MemoryBin = memoryBin.RangeReplace(0x18, 0x1F, Convert.ToString(value, 2).PadLeft(8, '0'));
            }
        }

        #region Attribute Bits representation ...
        /// <summary>
        /// Attribute Bits
        /// 1Fh A “1” bit indicates the tag is affixed
        /// to hazardous material. A “0” bit
        /// provides no such indication.
        /// </summary>
        public bool PC_AttributeBits_IsHazardousMaterial
        {
            get { return (PC_AttributeBits & 0x01) == 0x01; }
            set { PC_AttributeBits |=  (byte)(value ? 0x01 : 0x00); }
        }
        #endregion

        /// <summary>
        /// An Application Family Identifier that specifies a non-
        /// EPCglobal application for which the remainder of the
        /// EPC bank is encoded
        /// </summary>
        public byte PC_AFI
        {
            get
            {
                if (PC_Toggle)
                    return Convert.ToByte(memoryBin.Range(0x18, 0x1F), 2);
                else
                    return 0;
            }
            set
            {
                if (PC_Toggle)
                    MemoryBin = memoryBin.RangeReplace(0x18, 0x1F, Convert.ToString(value, 2).PadLeft(8, '0'));
            }
        }

        /// <summary>
        /// obsah epc memory v retezci v binarni podobe ...
        /// </summary>
        //private string epc = string.Empty;
        /// <summary>
        /// 20h – end
        /// Electronic Product Code, plus filter
        /// value. The Electronic Product code
        /// is a globally unique identifier for the
        /// physical object to which the tag is
        /// affixed. The filter value provides a
        /// means to improve tag read efficiency
        /// by selecting a subset of tags of
        /// interest.
        /// </summary>
        public string EPCCode
        {
            // TODO : osetrit vyskyt XPC atributove sekce ... na pozicich x210-x21F ...
            get { return memoryBin.Substring(0x20); }
            set { MemoryBin = memoryBin.RangeReplace(0x20, memoryBin.Length - 1, value); }
        }

        /// <summary>
        /// EPC Code Header Value
        /// Rozhoduje o jaky typ kodu se jedna ...
        /// 0000 0000 - nenaprogramovany tag ...
        /// ...
        /// 0011 0000 - SGTIN-96
        /// ...
        /// </summary>
        public string EPCCodeHeaderValue
        {
            get { return EPCCode.Range(0x00, 0x07); }
            set { EPCCode = EPCCode.RangeReplace(0x00, 0x07, value); }
        }

        //private ushort xpc = 0;
        /// <summary>
        /// 210h – 21Fh
        /// Extended Protocol Control bits. If bit
        /// 16h of the EPC bank is set to one,
        /// then bits 210h – 21Fh (inclusive)contain additional protocol control bits
        /// as specified in [UHFC1G2]
        /// </summary>
        public ushort XPC
        {
            get
            {
                if (PC_XPCIndicator)
                    return Convert.ToUInt16(memoryBin.Range(0x210, 0x21F), 2);
                else
                    return 0;
            }
            set
            {
                if (PC_XPCIndicator)
                    MemoryBin = memoryBin.RangeReplace(0x210, 0x21F, Convert.ToString(value, 2).PadLeft(16, '0'));
            }
        }
        #endregion

        #region interni metody objektu
        /// <summary>
        /// decoduje obsah hlavni pameti a nastavuje odpovidajici prvky objektu
        /// </summary>
        virtual protected void decodeMemory()
        {
            // ocekavam, ze memoryHex a memoryBin jsou jiz nastaveny...
            // nastavuji se hlavni slozky epc memory ...                        
        }
        #endregion

        #region verejne metody objektu
        /// <summary>
        /// Vraci definici objektu ve formatu EPC URI
        /// </summary>
        /// <returns></returns>
        virtual public string ToEPCURI()
        {
            return string.Empty;
        }

        /// <summary>
        /// nacte data objektu z epcuri
        /// </summary>
        /// <param name="epcuri"></param>
        virtual public void FromEPCURI(string epcuri)
        {            
        }

        /// <summary>
        /// Vraci definici objektu ve formatu EPC Raw URI
        /// </summary>
        /// <returns></returns>
        virtual public string ToEPCRawURI()
        {
            return string.Empty;
        }

        /// <summary>
        /// nacte data objektu z epcrawuri
        /// </summary>
        /// <param name="epcrawuri"></param>
        virtual public void FromEPCRawURI(string epcrawuri)
        {
        }

        #endregion

        /// <summary>
        /// Pokusi se z HEX retezce, ktery obsahuje cely obsah pameti najit odpovidajici objekt EPC a vratit jeho instanci...
        /// </summary>
        /// <param name="epcstringhex"></param>
        /// <returns></returns>
        public static EPC Parse(string epcstringhex)
        {          
            EPC epc = new EPC();
            epc.MemoryHex = epcstringhex;

            if (EPC_SGTIN96.EPCCodeHeaderValueSGTIN96 == epc.EPCCodeHeaderValue)
            {
                EPC_SGTIN96 epcsgtin96 = new EPC_SGTIN96();
                epcsgtin96.MemoryHex = epcstringhex;
                return epcsgtin96;
            }

            return epc;
        }

        /// <summary>
        /// Pokus se z EPC Uri dekodovat obsah EPC pameti ...
        /// </summary>
        /// <param name="epcURI"></param>
        /// <returns>Odpovidajici EPC objekt nebo base EPC, pokud nenalezne</returns>
        /// <exception>Vyjimka v pripade, ze neodpovida EPC Formatu ...</exception>
        public static EPC FromURI(string epcURI)
        {
            EPC epc = new EPC();
            epc.FromEPCURI(epcURI);
            return epc;
        }

    }
}
