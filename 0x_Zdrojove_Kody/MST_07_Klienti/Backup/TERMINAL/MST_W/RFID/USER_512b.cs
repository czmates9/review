using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class USER_512b : USER
    {

        public USER_512b() : base()
        {
            this.MemoryBin = (new StringBuilder()).Append('0',512).ToString();
        }

        public USER_512b(string userstringHex) : base(userstringHex)
        {
        }

        const int _itemNumberI0 = 0 * 8;
        const int _itemNumberI1 = 15 * 8 - 1;
        /// <summary>
        /// cislo polozky na 15 znaku
        /// </summary>
        public string ItemNumber
        {
            // pozice <1,10> (index [0:9])
            get
            {
                return
                    Routines_v2.StringHex2String(
                        Routines_v2.StringBin2StringHex(
                            this._memoryBin.Range(_itemNumberI0, _itemNumberI1)
                    )).Trim();
            }
            set
            {
                this.MemoryBin =
                    this.MemoryBin.RangeReplace(_itemNumberI0, _itemNumberI1,
                    Routines_v2.StringHex2StringBin(
                        Routines_v2.String2StringHex(value.PadRight((_itemNumberI1 - _itemNumberI0 + 1) / 8, ' '))
                        ));
            }
        }

        const int _serltNumberI0 = _itemNumberI1 + 1; //10 * 8;
        const int _serltNumberI1 = _serltNumberI0 + 10 * 8 - 1; //159; //2 * 10 * 8 - 1;
        /// <summary>
        /// seriove cislo polozky na 10 znaku
        /// </summary>
        public string SerltNumber
        {
            get
            {
                return
                    Routines_v2.StringHex2String(
                        Routines_v2.StringBin2StringHex(
                            this._memoryBin.Range(_serltNumberI0, _serltNumberI1)
                    )).Trim();
            }
            set
            {
                this.MemoryBin =
                    this.MemoryBin.RangeReplace(_serltNumberI0, _serltNumberI1,
                    Routines_v2.StringHex2StringBin(
                        Routines_v2.String2StringHex(value.PadRight((_serltNumberI1 - _serltNumberI0 + 1) / 8, ' '))
                        ));
            }
        }

        const int _itemDescI0 = _serltNumberI1 + 1;
        const int _itemDescI1 = 511; // do konce (512 delka)
        /// <summary>
        /// nazev polozky na 39 znaku
        /// </summary>
        public string ItemDesc
        {
            get
            {
                return
                    Routines_v2.StringHex2String(
                        Routines_v2.StringBin2StringHex(
                            this._memoryBin.Range(_itemDescI0, _itemDescI1)
                    )).Trim();
            }
            set
            {
                this.MemoryBin =
                    this.MemoryBin.RangeReplace(_itemDescI0, _itemDescI1,
                    Routines_v2.StringHex2StringBin(
                        Routines_v2.String2StringHex(value.PadRight((_itemDescI1 - _itemDescI0 + 1) / 8, ' '))
                        ));
            }
        }


        new public static USER_512b Parse(string userstringhex)
        {
            return new USER_512b(userstringhex);
        }
    }
}
