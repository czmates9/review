using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Fask.MST_W.RFID
{
    public class TID
    {
        private string TIDMemoryHex = string.Empty;
        private string TIDMemoryBin = string.Empty;
        public TID(string sHex)
        {
            TIDMemoryHex = sHex;
            TIDMemoryBin = Routines_v2.StringHex2StringBin(TIDMemoryHex);

            this.CI = Convert.ToByte(TIDMemoryBin.Substring(0x00, 8), 2);
            this.MDID = Convert.ToUInt16(TIDMemoryBin.Substring(0x08, 12), 2);
            this.MN = Convert.ToUInt16(TIDMemoryBin.Substring(0x14, 12), 2);

            if (!XTIDHeaderSegmentPresent) //pokud neni xtdi, pak konec (nastavuje se v property TMN)
                return;

            this.XTIDHeaderSegment = TIDMemoryBin.Substring(0x20, 16).PadLeft(16, '0');

            if (SerialNumberSegmentPresent > 0)
            {
                this.SerialNumberSegment = TIDMemoryBin.Substring(0x30, SerialNumberSegmentPresent);
                //this.SerialNumberSegment = TIDMemoryBin.Substring(0x30, 48);
            }
            if (OptionalCommandSupportSegmentPresent)
                this.OptionalCommandSupportSegment = TIDMemoryBin.Substring(0x60, 16);
            if (BlockWriteAndBlockEraseSegmentPresent)
                this.BlockWriteAndBlockEraseSegment = TIDMemoryBin.Substring(0x70, 64);
            if (UserMemoryAndBlockPermaLockSegmentPresent)
                this.UserMemoryAndBlockPermaLockSegment = TIDMemoryBin.Substring(0xB0, 32);
        }

        public static TID Parse(string hexTIDMemory)
        {
            return new TID(hexTIDMemory);
        }

        public string UserMemoryAndBlockPermaLockSegment { get; set; }
        public string BlockWriteAndBlockEraseSegment { get; set; }
        public string OptionalCommandSupportSegment { get; set; }
        public string SerialNumberSegment { get; set; }
        public string XTIDHeaderSegment { get; set; }

        public bool UserMemoryAndBlockPermaLockSegmentPresent
        {
            get
            {
                if (!XTIDHeaderSegmentPresent)
                    return false;

                return this.XTIDHeaderSegment.Substring(16 - 1 - 10, 1) != "0";
            }
        }
        public bool BlockWriteAndBlockEraseSegmentPresent
        {
            get
            {
                if (!XTIDHeaderSegmentPresent)
                    return false;

                return this.XTIDHeaderSegment.Substring(16 - 1 - 11, 1) != "0";
            }
        }

        public bool OptionalCommandSupportSegmentPresent
        {
            get
            {
                if (!XTIDHeaderSegmentPresent)
                    return false;

                return this.XTIDHeaderSegment.Substring(16 - 1 - 12, 1) != "0";
            }
        }
        /// <summary>
        /// 0 = neni serialnumber
        /// >0 = je a jak je dlouhe ... L = 48 + 16 * (Value - 1)
        /// </summary>
        public int SerialNumberSegmentPresent
        {
            get
            {
                if (!XTIDHeaderSegmentPresent)
                    return 0;

                byte serialization = Convert.ToByte(this.XTIDHeaderSegment.Substring(16 - 1 - 15, 3), 2);
                if (serialization == 0x00)
                    return serialization;
                else
                    return 48 + 16 * (serialization - 1);
            }
        }
        public bool ExtendedHeaderPresent
        {
            get
            {
                if (!XTIDHeaderSegmentPresent)
                    return false;

                return this.XTIDHeaderSegment.Substring(16 - 1 - 0, 1) != "0";
            }
        }
        public bool XTIDHeaderSegmentPresent
        {
            get { return (MDID & 0x800) == 0x800; }
        }

        /// <summary>
        /// Class identifier "E2h" ...
        /// </summary>        
        public byte CI { get; protected set; }
        /// <summary>
        /// Tag mask designer identifier 08h-13h obtainable from EPCGlobal...
        /// </summary>
        public ushort MDID { get; protected set; }
        public string MDID_Model
        {
            get
            {
                string mdid_model = string.Empty;
                //ushort mdid = Convert.ToUInt16(this.MDID, 16);
                if (MDIDs.models.ContainsKey(this.MDID))
                {
                    mdid_model = MDIDs.models[this.MDID];
                }
                else
                {
                    mdid_model = "Unknown";
                }
                return mdid_model;
            }
        }

        /// <summary>
        /// Tag model number 14h-1Fh [hex]
        /// </summary>
        public ushort MN
        {
            get;
            protected set;
        }

        #region Optional Command Support Segment
        /// <summary>
        /// This five bit field shall indicate the maximum size that can be 
        /// programmed into the first five bits of the PC.
        /// </summary>
        public int MaxEPCSize
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return 0;

                return Convert.ToInt32(OptionalCommandSupportSegment.Substring(15 - 4, 5), 2);
            }
        }

        /// <summary>
        /// If this bit is set the tag supports recommissioning as specified in [UHFC1G2].
        /// </summary>
        public bool RecommSupport
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 5, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If this bit is set the it indicates that the tag supports the access command.
        /// </summary>
        public bool Access
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 6, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If this bit is set it means that the tag supports lock bits for each memory
        /// bank rather than the simplest implementation of a single lock bit for the
        /// entire tag.
        /// </summary>
        public bool SeparateLockbits
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 7, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If this bit is set it means that the tag automatically sets its user memory
        /// indicator bit in the PC word.
        /// </summary>
        public bool AutoUMISupport
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 8, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If this bit is set it indicates that the tag supports phase jitter modulation.
        /// This is an optional modulation mode supported only in Gen 2 HF tags.
        /// </summary>
        public bool PJMSupport
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 9, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If set this indicates that the tag supports the BlockErase command.
        /// How the tag supports the BlockErase command is described in
        /// Section 16.2.4. A manufacture may choose to set this bit, but not
        /// include the BlockWrite and BlockErase field if how to use the command
        /// needs further explanation through a database lookup.
        /// </summary>
        public bool BlockEraseSupported
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 10, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If set this indicates that the tag supports the BlockWrite command.
        /// How the tag supports the BlockErase command is described in
        /// Section 16.2.4. A manufacture may choose to set this bit, but not
        /// include the BlockWrite and BlockErase field if how to use the command
        /// needs further explanation through a database lookup.
        /// </summary>
        public bool BlockWriteSupported
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 11, 1), 2) == 0x01;
            }
        }

        /// <summary>
        /// If set this indicates that the tag supports the BlockPermaLock
        /// command. How the tag supports the BlockPermaLock command is
        /// described in Section 16.2.5. A manufacture may choose to set this bit,
        /// but not include the BlockPermaLock and User Memory field if how to
        /// use the command needs further explanation through a database
        /// lookup.
        /// </summary>
        public bool BlockPermaLockSupported
        {
            get
            {
                if (!OptionalCommandSupportSegmentPresent)
                    return false;

                return Convert.ToByte(OptionalCommandSupportSegment.Substring(15 - 12, 1), 2) == 0x01;
            }
        }
        #endregion

        #region BlockWrite and BlockErase Segment

        /// <summary>
        /// Max block size that the tag supports for the BlockWrite command. This
        /// value should be between 1-255 if the BlockWrite command is
        /// described in this field.
        /// </summary>
        public int BlockWriteSize
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 7, 8));
            }
        }

        /// <summary>
        /// This bit is used to indicate if the tag supports BlockWrite commands
        /// with variable sized blocks.
        /// If the value is zero the tag only supports writing blocks exactly the
        /// maximum block size indicated in bits [7-0].
        /// If the value is one the tag supports writing blocks less than the
        /// maximum block size indicated in bits [7-0].
        /// </summary>
        public bool VariableSizeBlockWrite
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 8, 1)) == 0x01;
            }
        }

        /// <summary>
        /// This indicates the starting word address of the first full block that may
        /// be written to using BlockWrite in the EPC memory bank.
        /// </summary>
        public int BlockWriteEPCAddressOffset
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 16, 8));
            }
        }

        /// <summary>
        /// This bit is used to indicate if the tag memory architecture has hard
        /// block boundaries in the EPC memory bank.
        /// If the value is zero the tag has hard block boundaries in the EPC
        /// memory bank. The tag will not accept BlockWrite commands that start
        /// in one block and end in another block. These block boundaries are
        /// determined by the max block size and the starting address of the first
        /// full block. All blocks have the same maximum size.
        /// If the value is one the tag has no block boundaries in the EPC memory
        /// bank. It will accept all BlockWrite commands that are within the
        /// memory bank.
        /// </summary>
        public bool NoBlockWriteEPCAddressAlignment
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 17, 1)) == 0x01;
            }
        }

        /// <summary>
        /// This indicates the starting word address of the first full block that may
        /// be written to using BlockWrite in the User memory.
        /// </summary>
        public int BlockWriteUserAddressOffset
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 25, 8));
            }
        }

        /// <summary>
        /// This bit is used to indicate if the tag memory architecture has hard
        /// block boundaries in the USER memory bank.
        /// If the value is zero the tag has hard block boundaries in the USER
        /// memory bank. The tag will not accept BlockWrite commands that start
        /// in one block and end in another block. These block boundaries are
        /// determined by the max block size and the starting address of the first
        /// full block. All blocks have the same maximum size.
        /// If the value is one the tag has no block boundaries in the USER
        /// memory bank. It will accept all BlockWrite commands that are within
        /// the memory bank.
        /// </summary>
        public bool NoBlockWriteUserAddressAlignment
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 26, 1)) == 0x01;
            }
        }

        /// <summary>
        /// Max block size that the tag supports for the BlockErase command.
        /// This value should be between 1-255 if the BlockErase command is
        /// described in this field.
        /// </summary>
        public int SizeOfBlockErase
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 39, 8));
            }
        }

        /// <summary>
        /// This bit is used to indicate if the tag supports BlockErase commands
        /// with variable sized blocks.
        /// If the value is zero the tag only supports erasing blocks exactly the
        /// maximum block size indicated in bits [39-32].
        /// If the value is one the tag supports erasing blocks less than the
        /// maximum block size indicated in bits [39-32].
        /// </summary>
        public bool VariableSizeBlockErase
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 40, 1)) == 0x01;
            }
        }

        /// <summary>
        /// This indicates the starting address of the first full block that may be
        /// erased in EPC memory bank.
        /// </summary>
        public int BlockEraseEPCAddressOffset
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 48, 8));
            }
        }

        /// <summary>
        /// This bit is used to indicate if the tag memory architecture has hard
        /// block boundaries in the EPC memory bank.
        /// If the value is zero the tag has hard block boundaries in the EPC
        /// memory bank. The tag will not accept BlockErase commands that start
        /// in one block and end in another block. These block boundaries are
        /// determined by the max block size and the starting address of the first
        /// full block. All blocks have the same maximum size.
        /// If the value is one the tag has no block boundaries in the EPC memory
        /// bank. It will accept all BlockErase commands that are within the
        /// memory bank.
        /// </summary>
        public bool NoBlockEraseEPCAddressAlignment
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 49, 1)) == 0x01;
            }
        }

        /// <summary>
        /// This indicates the starting address of the first full block that may be
        /// erased in User memory bank.
        /// </summary>
        public int BlockEraseUserAddressOffset
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(BlockWriteAndBlockEraseSegment.Substring(63 - 57, 8));
            }
        }

        /// <summary>
        /// Bit 58: This bit is used to indicate if the tag memory architecture has
        /// hard block boundaries in the USER memory bank.
        /// If the value is zero the tag has hard block boundaries in the USER
        /// memory bank. The tag will not accept BlockErase commands that start
        /// in one block and end in another block. These block boundaries are
        /// determined by the max block size and the starting address of the first
        /// full block. All blocks have the same maximum size.
        /// If the value is one the tag has no block boundaries in the USER
        /// memory bank. It will accept all BlockErase commands that are within
        /// the memory bank.
        /// </summary>
        public bool NoBlockEraseUserAddressAlignment
        {
            get
            {
                if (!BlockWriteAndBlockEraseSegmentPresent)
                    return false;

                return Convert.ToByte(BlockWriteAndBlockEraseSegment.Substring(63 - 58, 1)) == 0x01;
            }
        }

        #endregion

        #region User Memory and BlockPermaLock Segment

        /// <summary>
        /// Number of 16-bit words in user memory.
        /// </summary>
        public int UserMemorySize
        {
            get
            {
                if (!UserMemoryAndBlockPermaLockSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(UserMemoryAndBlockPermaLockSegment.Substring(31 - 15, 16));
            }
        }

        /// <summary>
        /// If non-zero, the size in words of each block that may be block
        /// permalocked. That is, the block permalock feature allows blocks of
        /// N*16 bits to be locked, where N is the value of this field.
        /// If zero, then the XTID does not describe the block size for the
        /// BlockPermaLock feature. The tag may or may not support block
        /// permalocking.
        /// This field SHALL be zero if the Optional Command Support
        /// Segment (Section 16.2.3) is present and its
        /// BlockPermaLockSupported bit is zero.
        /// </summary>
        public int BlockPermaLockBlockSize
        {
            get
            {
                if (!UserMemoryAndBlockPermaLockSegmentPresent)
                    return 0; //? -1 ?

                return Convert.ToInt32(UserMemoryAndBlockPermaLockSegment.Substring(31 - 31, 16));
            }
        }
        #endregion

        #region Serialized Tag Identification (STID)
        public string STID_URI
        {
            get
            {
                string stiduri = "urn:epc:stid:";

                if (this.CI != 0xE2)
                    return string.Empty;
                if (!this.XTIDHeaderSegmentPresent)
                    return string.Empty;

                if (this.SerialNumberSegmentPresent <= 0)
                    return string.Empty;

                string snBin = this.TIDMemoryBin.Substring(48, this.SerialNumberSegmentPresent);
                string snBin2 = this.SerialNumberSegment; //???
                // zde jsou vsechny podminky splneny ...

                return
                    stiduri +
                    "x" + this.MDID.ToString("X").PadLeft(3, '0') +
                    ".x" + this.MN.ToString("X").PadLeft(3, '0') +
                    ".x" + Convert.ToUInt64(snBin, 2).ToString("X").PadLeft(this.SerialNumberSegmentPresent / 4, '0');
            }
        }
        #endregion
    }
}
