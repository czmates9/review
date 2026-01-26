using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class EPSGlobal_sgtin96
    {
        //EPC URI: urn:epc:id:sgtin:d2d3…d(L+1).d1d(L+2)d(L+3)…d13.s1s2…sK
        //GS1 Element String: (01)d1d2…d14 (21)s1s2…sK
        //where 1 ≤ K ≤ 20.
        //To find the GS1 element string corresponding to an SGTIN EPC URI:
        //1. Number the digits of the first two components of the EPC as shown above. Note that there will
        //always be a total of 13 digits.
        //2. Number the characters of the serial number (third) component of the EPC as shown above. Each
        //si corresponds to either a single character or to a percent-escape triplet consisting of a % character
        //followed by two hexadecimal digit characters.
        //3. Calculate the check digit d14 = (10 – ((3(d1 + d3 + d5 + d7 + d9 + d11 + d13) + (d2 + d4 + d6 + d8 + d10 +
        //d12)) mod 10)) mod 10.
        //4. Arrange the resulting digits and characters as shown for the GS1 Element String. If any si in the
        //EPC URI is a percent-escape triplet %xx, in the GS1 Element String replace the triplet with the
        //corresponding character according to Table A-1 (For a given percent-escape triplet %xx, find the
        //row of Table A-1 that contains xx in the “Hex Value” column; the “Graphic Symbol” column then
        //gives the corresponding character to use in the GS1 Element String.)

        //GTIN-12 and GTIN-13
        //To find the EPC URI corresponding to the combination of a GTIN-12 or GTIN-13 and a serial number,
        //first convert the GTIN-12 or GTIN-13 to a 14-digit number by adding two or one leading zero characters,
        //respectively, as shown in [GS1GS14.0] Section 3.3.2.
        //Example:
        //GTIN-12: 614141 12345 2
        //Corresponding 14-digit number: 0 0614141 12345 2
        //Corresponding SGTIN-EPC: urn:epc:id:sgtin:0614141.012345.Serial
        //Example:
        //GTIN-13: 0614141 12345 2
        //Corresponding 14-digit number: 0 0614141 12345 2
        //Corresponding SGTIN-EPC: urn:epc:id:sgtin:0614141.012345.Serial
        //In these examples, spaces have been added to the GTIN strings for clarity, but are never encoded.

        // znaku = 
        // EPC [128b] : 128b = 16byte (v hexa stringu je to 32 znaku)
        //  - CRC[16b] = 2byte (v hexa stringu je to 4 znaku)
        //  - PC[16b] = Header[8b] + attributtes[8b] = [16b] = 2byte (v hexa stringu je to 4 znaku)
        //  - EPC data[96b] = ... 12byte (v hexa stringu je to 24 znaku)
        //      Header [8b]
        //      Filter [3b]
        //      Partision table [44b]
        //          Partition [3b]
        //          GS1 Company Prefix [20-40b]
        //          Indicator/itemreference [24-4b]
        //      Serial [38]

        // CRC => 16bit

        public const int sgting_96_epc_memory_length = 128; // 128b : crc=16b, pc=16b (=32b), epc_data=96b => 32+96=128
        public string TAG_CRC = "0000"; // HEX : 16b/8=2byte*2=4znaky
        public string TAG_PC = "4000"; // HEX : 16b/8=2byte*2=4znaky
        // Header value pro SGTIN-96
        //public const ushort Header = 0x80; // binarne: 1000 0000 (0x80) az 1011 1111 (0xBF)
        public const ushort HeaderEPCData = 0x30; // binarne: 0011 0000 hexa: 0x30 8 bit

        // EAN (dle GS1) = CompanyPrefix + ItemRef ...
        public string EAN = "8594008043876"; // Pro Pepsi-Colu ... 13 znaku s checkdigit ...
        public uint SerialNumber = 1; // 1 = vychozi hodnota ...
        public string FilterValue = "3"; //

        public string EAN14 { get { return EAN.PadLeft(14, '0'); } }
        public string CompanyPrefix { get { return EAN14.Substring(1, 7); } }
        public string ItemRef { get { return EAN14.Substring(8, 5); } } // bez check digit
        public string Indicator { get { return EAN14.Substring(0, 1); } }
        public int CheckDigit
        {
            get
            {
                return (10 - ((
                    3*(
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

        public string URIstring
        {
            get
            {
                return
                    "urn:epc:tag:sgtin-96:" +
                    FilterValue + "." +
                    CompanyPrefix + "." + 
                    Indicator + ItemRef + "." +
                    SerialNumber.ToString();
            }
        }

        public string EPCDataStringBinary
        {
            get
            {
                //string tag_Crc = "0000".PadLeft(4, '0'); // HEX : 16b/8=2byte*2=4znaky
                //string tag_Header = "4000".PadLeft(4, '0'); // HEX : 16b/8=2byte*2=4znaky (length[5b], umi[1b], xpc[1b], toggle[1b], attr[8b] = [16b])
                //string epc_Header = Convert.ToString(HeaderEPCData, 16).PadLeft(2,'0'); // HEX : 8b/8=1byte*2=2znaky
                //string epc_Filter = "011"; // BIN
                //string epc_Partition = "101"; //BIN 5=>CompPrefix[24b=7length], Indicator a ItemRef[20b=6length]
                //string epc_CompanyPrefix = uint.Parse(CompanyPrefix).ToString("X").PadLeft(6, '0'); // HEX : 24b/8=3byte*2=6znaku
                //string epc_IndicatorItemRef = uint.Parse(Indicator + ItemRef).ToString("X").PadLeft(5, '0'); // HEX : 20b/8=2.5byte(~3byte)=>5znaku
                //string epc_Serial = SerialNumber.ToString("X").PadLeft(38, '0');

                string tag_Crc = Convert.ToString(Convert.ToUInt16(TAG_CRC, 16), 2).PadLeft(16, '0'); // BIN : 16b/8=2byte*2=4znaky
                string tag_Header = Convert.ToString(Convert.ToUInt16(TAG_PC, 16), 2).PadLeft(16, '0'); // BIN : 16b/8=2byte*2=4znaky (length[5b], umi[1b], xpc[1b], toggle[1b], attr[8b] = [16b])
                string epc_Header = Convert.ToString(HeaderEPCData, 2).PadLeft(8,'0'); // BIN : 8b/8=1byte*2=2znaky
                string epc_Filter = "011"; // BIN
                string epc_Partition = "101"; //BIN 5=>CompPrefix[24b=7length], Indicator a ItemRef[20b=6length]
                string epc_CompanyPrefix = Convert.ToString(uint.Parse(CompanyPrefix), 2).PadLeft(24, '0'); // BIN : 24b/8=3byte*2=6znaku
                string epc_IndicatorItemRef = Convert.ToString(uint.Parse(Indicator + ItemRef),2).PadLeft(20, '0'); // BIN : 20b/8=2.5byte*2(~3byte)=>5znaku
                string epc_Serial = Convert.ToString(SerialNumber, 2).PadLeft(38, '0'); // BIN : 38b/8=4.75byte(~5byte)=>10znaku???

                //string databinary = tag_Crc + tag_Header + epc_Header + epc_Filter + epc_Partition + epc_CompanyPrefix + epc_IndicatorItemRef + epc_Serial;
                // pouze vraci data pro epc memory cast datovou ...
                // crc si dopocitava tag ...
                // TODO : tag_header se bude nastavovat samostatne ...
                string databinary = 
                    //tag_Crc + tag_Header + // delka 8 znaku => 8/2*8
                    epc_Header + epc_Filter + epc_Partition + epc_CompanyPrefix + epc_IndicatorItemRef + epc_Serial;
                if ((databinary.Length + 8 / 2 * 8) != sgting_96_epc_memory_length)
                    throw new Exception("EPC Memory data length is not 128 bits");               

                return databinary;
            }
        }


        public string EPCDataStringHex
        {
            get
            {
                // converze na hexa format ... musi z toho vzniknout reteze o 32 znacich, z toho 8 hlavicky(crc a pc) a 24 data pro zapis ...
                return Routines_v2.StringBin2StringHex(this.EPCDataStringBinary);
            }
        }

    }
}
