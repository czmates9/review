using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Scanner.RFID
{
    /// <summary>
    /// Prevody mezi String a byte[]
    /// </summary>
    public class Routines
    {
        /// <summary>
        /// Byte pole ktere obsahuje hex hodnoty ktere jsou prevedeny na 
        /// retezec hex hodnotat napr  123ABC
        /// </summary>
        /// <param name="read">pole na prevod</param>
        /// <returns>retez prevedeny</returns>
        public static string ByteHexArrayToStringHex(byte[] read)
        {
            string nacteniT = string.Empty;
            nacteniT = System.BitConverter.ToString(read);
            nacteniT = nacteniT.Replace("-", "");
            int pozice = 0;
            for (int i = 0; i < nacteniT.Length; i++)
            {
                if (nacteniT[i] != '0')
                {
                    pozice = i;
                    break;
                }
            }
            nacteniT = nacteniT.Remove(0, pozice);
            return nacteniT;
        }

        /// <summary>
        /// Pole bytove obsahuje assci znaky  a prevede se na retezec
        /// </summary>
        /// <param name="read">pole</param>
        /// <returns>retezec</returns>
        public static string ByteasciiArrayToString(byte[] read)
        {
            string nactenyT = System.Text.Encoding.ASCII.GetString(read, 0, read.Length);
            nactenyT = nactenyT.Replace(Convert.ToChar(0x0).ToString(), "");//odstanim \0
            return nactenyT;
        }

        /// <summary>
        /// Prevede string ktery obsahuje hex retezec napr. 123AFCD
        /// na pole bytu 
        /// </summary>
        /// <param name="strProZapis"></param>
        /// <returns></returns>
        public static byte[] StringHexValueToByte(string strProZapis)
        {
            byte[] pole = new byte[12];//mam jich poslat 12
            string[] strPole = new string[12];
            string strPomoc = strProZapis;
            if (strPomoc.Length < 24) //pridam tam nuly abych bylo 24 znaku
            {
                string pomoc = string.Empty;
                for (int i = 0; i < (24 - strPomoc.Length); i++)
                {
                    pomoc += "0";
                }
                strProZapis = strProZapis.Insert(0, pomoc);
            }
            //ted to rozdelim na dvojce
            for (int i = 0; i < pole.Length; i++)
            {
                strPole[i] = strProZapis.Substring(i + i, 2);
            }
            //prevedu na hex hodnotu
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] = byte.Parse(strPole[i], System.Globalization.NumberStyles.HexNumber);
            }
            return pole;
        }

        /// <summary>
        /// String prevede na pole bytu o 12 bytech, zbytek bude bula
        /// </summary>
        /// <param name="strProZapis">str ktery se ma prevest</param>
        /// <returns>vrati pole bytu o 12 hodnotach</returns>
        public static byte[] StringToByteAscii(string strProZapis)
        {
            byte[] pole = System.Text.Encoding.ASCII.GetBytes(strProZapis.ToCharArray(), 0, strProZapis.Length);
            byte[] pomocpp = new byte[pole.Length];
            pole.CopyTo(pomocpp, 0);
            return pomocpp;
        }

        public static byte[] StringHexToByteHex(string str)
        {
            byte[] pole = new byte[str.Length];
            byte b = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                b = CharToByte(ch);
                pole[i] = b;
            }
            return pole;
        }

        public static byte CharToByte(char ch)
        {
            switch (ch)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'a':
                case 'A': return 10;
                case 'b':
                case 'B': return 11;
                case 'c':
                case 'C': return 12;
                case 'd':
                case 'D': return 13;
                case 'e':
                case 'E': return 14;
                case 'f':
                case 'F': return 15;                    
                default: return 0;
            }
        }

    }
}
