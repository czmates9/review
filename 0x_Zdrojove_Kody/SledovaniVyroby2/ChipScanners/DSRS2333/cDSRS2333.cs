
using System;
using FASK.SledovaniVyroby.ChipScannersIfc;

namespace FASK.SledovaniVyroby.ChipScanners.DSRS2333
{
    /// <summary>
    /// Implementace pro scanner cipuu DSRS2333 
    /// </summary>
    public class cDSRS2333 : IChipScannersConnector
    {
        /// <summary>
        /// Vrati instrukci na pozadavek na identifikaci scanneru
        /// </summary>
        public string Idenfication { get { return DSRS2333Protocol.IdentifyQ; } }

        /// <summary>
        /// Vrati instrukci na pozadavek na kod cipu ze scanneru
        /// </summary>
        public string Code { get { return DSRS2333Protocol.GetCode; } }

        /// <summary>
        /// Vrati instrukci na pozadavek na zablokovani cipovych scanneru
        /// </summary>
        public string Block { get { return DSRS2333Protocol.BlockChipScanner; } }

        /// <summary>
        /// Vrati instrukci na pozadavek na odblokovani cipovych scanneru
        /// </summary>
        public string Unblock { get { return DSRS2333Protocol.UnblockChipScanner; } }

        /// <summary>
        /// Zpracovani - rozparsovani odpovedi, ktera prisla na nejakou instrukci
        /// </summary>
        /// <param name="answer">Odpoved ke zpracovani</param>
        /// <param name="instruction">Instrukce, na kterou odpoved prisla</param>
        /// <param name="note">Poznamka k vykonavani instrukce (napriklad preteceni), nebo string.Empty</param>
        /// <param name="data">Odpoved na instrukci, nebo string.Empty, pokud instrukce nevyzaduje datovou (napr hodnota citace) odpoved</param>
        /// <returns>Uspech ci neuspech</returns>
        public bool processAnswer(string answer, string instruction, ref string note, ref string data)
        {
            //Vynulovani vystupnich parametru
            note = data = string.Empty;

            //Rozdeleni dle instrukci
            if (instruction == Idenfication)
            {
                return parseIdentification(answer, ref note);
            }
            //Od scanneru muze prijit pozadavek i asynchronne - pak je instrukce razdna
            else if (instruction == Code || instruction == string.Empty)
            {
                return parseChipCode(answer, ref note, ref data);
            }
            else if (instruction == Block)
            {
                return parseBlockChipScanner(answer, ref note);
            }
            else if (instruction == Unblock)
            {
                return parseUnblockChipScanner(answer, ref note);
            }

            //Neznama instrukce
            return false;
        }

        /// <summary>
        /// Parsovani odpovedi na instrukci pro odblokovani scanneru cipu
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        private bool parseUnblockChipScanner(string answer, ref string note)
        {
            //Odpoved OK
            if (answer == DSRS2333Protocol.OK) return true;
            //Chyba
            note = note = "Unexpected answer - unblock.";
            return false;
        }

        /// <summary>
        /// Parsovani odpovedi na instrukci pro blokovani scanneru cipu
        /// </summary>
        /// <param name="answer">Odpoved, ktera se parsuje</param>
        /// <param name="note">Poznamka pri neuspechu</param>
        /// <returns>Uspech nebo neuspech</returns>
        private bool parseBlockChipScanner(string answer, ref string note)
        {
            //Odpoved OK
            if (answer == DSRS2333Protocol.OK) return true;
            //Chyba
            note = "Unexpected answer - block.";
            return false;
        }

        /// <summary>
        /// Parsovani odpovedi na instrukci pro ziskani kodu cipu
        /// </summary>
        /// <param name="answer">Odpoved, ktera se parsuje</param>
        /// <param name="data">Vysledna data, pokud nejaka jsou</param>
        /// <param name="note">Poznamka pri neuspechu</param>        
        /// <returns></returns>
        private bool parseChipCode(string answer, ref string note, ref string data)
        {
            //Kontrola delky odpovedi
            if (answer.Length != 7)
            {
                note = "Unexpected answer - bad length.";
                return false;
            }

            //Kontrola struktury odpovedi - pocatecni znak > a koncovy #
            if (answer[0] != DSRS2333Protocol.QT || answer[6] != DSRS2333Protocol.SLASH)
            {
                note = "Unexpected answer - bad message format.";
                return false;
            }

            //Vypocet CRC
            char crc = (char)0x00;
            if (!DSRS2333Protocol.calculateCRC(answer.Substring(0, 5), ref crc))
            {
                note = "Unexpected answer - calculate crc.";
                return false;
            }

            //Kontrola CRC
            if (answer[5] != crc)
            {
                note = "Unexpected answer - bad crc.";
                return false;
            }

            //Ziskani dat a vraceni v opacnem poradi
            data = charToHexaString(answer[4]) + charToHexaString(answer[3]) + charToHexaString(answer[2]) + charToHexaString(answer[1]);
            return true;
        }

        /// <summary>
        /// Parsovani odpovedi na pozadavek na identifikaci
        /// </summary>
        /// <param name="answer">Odpoved, ktera se parsuje</param>
        /// <param name="note">Poznamka pri neuspechu</param>
        /// <returns>Uspech nebo neuspech</returns>
        private bool parseIdentification(string answer, ref string note)
        {
            //Odpoved OK
            if (answer == DSRS2333Protocol.IdentifyR) return true;
            //Chyba
            note = note = "Unexpected answer - identify.";
            return false;
        }

        /// <summary>
        /// Prevede znak na hexa string o dvou znacich
        /// </summary>
        /// <param name="letter">Znak</param>
        /// <returns>Vysledny string</returns>
        private string charToHexaString(char letter)
        {
            int value = (int)letter;
            //Kazda hodnota ma mit dve cifry - u 0-F by tomu tak nebylo, pridam 0 na zacatek
            string strValue = String.Format("{0:X2}", value);
            //Vracim string
            return strValue;
        }
    }
}
