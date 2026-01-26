using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.SledovaniVyroby.ChipScanners.DSRS2333
{
    /// <summary>
    /// Implementace protokolu se scanerem cipu DSRS2333. 
    /// Cela instrukcni sada a popis znaku i formatu instrukci v dodatecnem dokumentu ke scanneru.
    /// </summary>
    static class DSRS2333Protocol
    {
        //Znak * - startovaci znak dotazu
        internal static char STAR = '*';
        //Znak > - startovaci znak odpovedi
        internal static char QT = '>';
        //Znak # - koncovy znak
        internal static char SLASH = '#';

        /// <summary>
        /// Pozadavek byl OK, odpoved je OK - pozadavek se provedl (nevraci se data)
        /// Pozn: ':' = CRC = QT xor 'O' xor 'K' (obdobne vsude)
        /// </summary>
        internal static string OK = QT + "OK:" + SLASH;

        /// <summary>
        /// Zablokovani scanneru.
        /// </summary>
        internal static string BlockChipScanner = STAR + "Bh" + SLASH;

        /// <summary>
        /// Odblokovani scanneru.
        /// </summary>
        internal static string UnblockChipScanner = STAR + "Oe" + SLASH;

        /// <summary>
        /// Dotaz (Query) na identifikaci scanneru cipu
        /// </summary>
        internal static string IdentifyQ = STAR + "Ic" + SLASH;

        /// <summary>
        /// Odpoved (Response) na identifikaci scanneru cipu
        /// </summary>
        internal static string IdentifyR = QT + "2333?" + SLASH;

        /// <summary>
        /// Pozadavek na nacteni kodu
        /// </summary>
        internal static string GetCode = STAR + "Dn" + SLASH;

        /// <summary>
        /// Vypocita hodnotu CRC daneho retezce a vraci ji jako znak
        /// </summary>
        /// <param name="str">Znaky, ze kterych ma byt crc spocteno</param>
        /// <param name="crc">Znak reprezentujici crc</param>
        /// <returns></returns>
        internal static bool calculateCRC(string str, ref char crc)
        {
            //Neni z ceho pocitat
            if(str == string.Empty) return false;

            //Pruchod a pocitani
            foreach (char ch in str) crc ^= ch;

            //Ok
            return true;
        }
    }
}
