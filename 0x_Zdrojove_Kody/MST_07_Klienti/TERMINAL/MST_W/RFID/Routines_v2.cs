using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    /// <summary>
    /// Prevody mezi String a byte[]
    /// </summary>
    public class Routines_v2
    {
        public static byte C2B(string bytestring)
        {
            return byte.Parse(bytestring, System.Globalization.NumberStyles.HexNumber);
        }
        public static char B2C(byte b)
        {
            return Convert.ToChar(b);
        }
        
        /// <summary>
        /// Prevede retezec na byte[]
        /// </summary>
        /// <param name="stringHex">retezec, ktery obashuje hexa informace</param>
        /// <returns>byte[]</returns>
        public static byte[] StringHex2Byte(string stringHex)
        {
            //byte[] dataBytes = Encoding.Convert(Encoding.Default, Encoding.ASCII, Encoding.Default.GetBytes(data));
            byte[] data = new byte[stringHex.Length / 2];

            for (int i = 0; i < data.Length; i++)
            {
                //data[i] = C2B(stringHex[2 * i]);
                data[i] = C2B(stringHex.Substring(2 * i, 2));
            }

            return data;
        }

        /// <summary>
        /// Prevede retezec na odpovidajici interpretaci retezce z hexa kodu v kodovani ASCII
        /// </summary>
        /// <param name="stringHex">retezec, ktery obashuje hexa informace</param>
        /// <returns>retezec znaku v kodovani ASCII</returns>
        public static string StringHex2String(string stringHex)
        {
            return Byte2String(StringHex2Byte(stringHex));
        }
        
        /// <summary>
        /// Prevede data v kodovani ASCII na retezec v kodovani Default
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Byte2String(byte[] data)
        {
            //return Encoding.ASCII.GetString(data, 0, data.Length);
            byte[] dataNew = Encoding.Convert(Encoding.ASCII, Encoding.Default, data);
            return Encoding.Default.GetString(dataNew, 0, dataNew.Length);
        }

        /// <summary>
        /// Prevede retezec na byte[] v kodovani ASCII
        /// </summary>
        /// <param name="stringData">Retezec k prevodu na byte[]</param>
        /// <returns>byt[] v kodovani ASCII</returns>
        public static byte[] String2Byte(string stringData)
        {
            return Encoding.Convert(Encoding.Default, Encoding.ASCII, Encoding.Default.GetBytes(stringData));
        }

        public static string String2StringHex(string stringData)
        {
            StringBuilder sb = new StringBuilder();
            byte[] data = String2Byte(stringData);
            for (int i = 0; i < data.Length; i++)
            {
                //sb.Append(B2C(data[i]));
                sb.Append(Convert.ToString(data[i], 16));
            }
            return sb.ToString();
        }

        public static string StringBin2StringHex(string stringBin)
        {
            if (stringBin == null)
                throw new ArgumentNullException("stringBin");
            if (stringBin.Length % 8 != 0)
                throw new ArgumentException("The length must be a multiple of 8", "stringBin");

            var hex = Enumerable.Range(0, stringBin.Length / 8)
                             .Select(i => stringBin.Substring(8 * i, 8))
                             .Select(s => Convert.ToByte(s, 2))
                             .Select(b => b.ToString("x2"));
            return String.Join(null, hex.ToArray<string>());
        }

        public static string StringHex2StringBin(string stringHex)
        {
            if (stringHex == null)
                throw new ArgumentNullException("stringHex");
            if (stringHex.Length % 2 != 0)
                throw new ArgumentException("The length must be a multiple of 2", "stringHex");

            var bin = Enumerable.Range(0, stringHex.Length / 2)
                             .Select(i => stringHex.Substring(2 * i, 2))
                             .Select(s => Convert.ToByte(s, 16))
                             .Select(b => Convert.ToString(b, 2).PadLeft(8, '0'));
            return String.Join(null, bin.ToArray<string>());
        }

    }
}
