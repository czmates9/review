
using System;
using System.IO.Ports;
using FASK.SledovaniVyroby.CountersIfc;

namespace FASK.SledovaniVyroby.Counters.Kuebler_716
{
    /// <summary>
    /// Implementace pro citac Kuebler Codix 716
    /// </summary>
    public class cKuebler_716 : ICounterConnector
    {
        /// <summary>
        /// Operacni mody citace
        /// </summary>
        //private string[] operatingMode = new string[] { "Count", "Timer", "Tacho" };

        /// <summary>
        /// SubOperacni mody citace pro operacni mody Time a Count
        /// </summary>
        //private string[] subOperatingModeForTimeAndCount = new string[] { "Add", "Sub", "AddAr", "SubAr" };

        /// <summary>
        /// Vrati instrukci na pozadavek o aktualni hodnotu
        /// </summary>
        public string Value { get { return ESCProtocol.CurrentCounterValue; } }

        /// <summary>
        /// Vrati instrukci na pozadavek na vynulovani dosavadni hodnoty citace
        /// </summary>
        public string Reset { get { return ESCProtocol.ResetCounterValue; } }

        /// <summary>
        /// Vrati pole stringu obsahujici operacni mody 
        /// </summary>
        //public string[] OperatingModes { get { return operatingMode; } }

        /// <summary>
        /// Vrati pole stringu obsahujici suboperacni mody 
        /// </summary>
        //public string[] SubOperatingModesForCount { get { return subOperatingModeForTimeAndCount; } }

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
            if (instruction == ESCProtocol.CurrentCounterValue)
            {
                return parseCurrentcounterValue(answer, ref note, ref data);
            }
            else if(instruction == ESCProtocol.ResetCounterValue)
            {
                return parseResetCounterValue(answer, ref note);
            }

            //Neznama instrukce
            return false;
        }

        /// <summary>
        /// Parsovani navratove hodnoty na pozadavaek na vynulovani citace
        /// </summary>
        /// <param name="answer">Odpoved ke zpracovani</param>
        /// <param name="note">Poznamka k vykonavani instrukce (napriklad preteceni), nebo string.Empty</param>
        /// <returns>Uspech, neuspech</returns>
        private bool parseResetCounterValue(string answer, ref string note)
        {
            //Parsuji pouze, zda odpoved byla OK = <CR><LF>, ci ERROR = F<CR><LF>
            if (answer == ESCProtocol.OK) return true;
            else if (answer == ESCProtocol.ERROR) return false;
            
            //Nemelo by nastat - anomalie
            note = "Unexpected answer - bad return value.";
            return false;
        }

        /// <summary>
        /// Parsovani navratove hodnoty na dotaz na aktualni hodnotu citace
        /// </summary>
        /// <param name="answer">Odpoved ke zpracovani</param>
        /// <param name="note">Poznamka k vykonavani instrukce (napriklad preteceni), nebo string.Empty</param>
        /// <param name="data">Odpoved na instrukci, nebo string.Empty, pokud instrukce nevyzaduje datovou (napr hodnota citace) odpoved</param>
        /// <returns>Uspech, neuspech</returns>
        private bool parseCurrentcounterValue(string answer, ref string note, ref string data)
        {
            //Pokud byla chyba... 
            if (answer == ESCProtocol.ERROR) return false;

            //..., nebo pokud delka odpovedi neodpovida, tak false
            if (answer.Length != 11)
            {
                //Nemelo by nastat - anomalie
                note = "Unexpected answer - bad length.";
                return false;
            }
            
            //Jinak parsuji - tvar <STX><E><+/->XXXXXX<CR><LF>
            //Kontrola struktury odpovedi - <STX>neco<CR><LF>
            if (answer[0] == ESCProtocol.STX && answer[9] == ESCProtocol.CR && answer[10] == ESCProtocol.LF)
            {
                //Pokud je <E> rovno E, pak doslo k preteceni, a pokud neni a neni ani rovno 0, pak chyba
                if (answer[1] == 'E')
                {
                    note = "Overflow.";
                }
                else if (answer[1] != '0')
                {
                    //Nemelo by nastat - anomalie
                    note = "Unexpected answer - bad carry flag.";
                    return false;
                }

                //Zbytek musi byt hodnota citace se znamenkem, prevoditelna na Int
                data = answer.Substring(2, 7); int dummy;
                if (!Int32.TryParse(data, out dummy))
                {
                    //Nemelo by nastat - anomalie
                    data = string.Empty;
                    note = "Unexpected answer - bad number format.";
                    return false;
                }
            }
            else
            {
                //Nemelo by nastat - anomalie
                note = "Unexpected answer - bad message format.";
                return false;
            }
            
            //Vse OK
            return true;
        }
    }
}
