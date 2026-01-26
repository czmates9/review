using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    static class InputModeChecker
    {
        //Typ, jakym byla polozka nasnimana/vybrana v ramci modulu - listu polozek - 
        //bud scannerem == 1 == INPUT_SCANNER, 
        //rucne eneterm nebo pes menu == 2 == INPUT_ENTER 
        //nebo nejakym zpusobem vyhledani == 4 == INPUT_SEARCH,
        //kdy je vyhledana polozka primo zpracovavana - pokud se jeste vybira, opet se situace opakuje - enter, scanner
        //Pridavani pres menu neni nijak omezeno, jen enterem - dle nastaveni dle upravy nekupto
        public enum _input_modes { INPUT_SCANNER = 1, INPUT_ENTER, INPUT_SEARCH};
        
        //Funkce pro nastaveni vstupniho modu - nastaveny mod je vracen
        public static byte setInputMode(_input_modes mode)
        {
            return ((byte)mode);
        }

        //Funkce pro ziskani vstupniho modu
        private static _input_modes getInputMode(byte mode)
        {
            switch(mode)
            {
                case (byte)_input_modes.INPUT_ENTER:
                    return _input_modes.INPUT_ENTER;
                case (byte)_input_modes.INPUT_SCANNER:
                    return _input_modes.INPUT_SCANNER;
                case (byte)_input_modes.INPUT_SEARCH:
                    return _input_modes.INPUT_SEARCH;
            }

            //Vychozi hodnota - chybna, mela by byt vzdy > 0
            return 0;
        }

        //Metoda pro kontrolu vstupniho modu pri vyberu polozky - jednodusi pouziti
        public static bool checkInputMode(bool scannerOnly, byte mode)
        {
            _input_modes imode = getInputMode(mode);
            return checkInputMode(scannerOnly, imode);
        }

        //Metoda pro kontrolu vstupniho modu pri vyberu polozky
        private static bool checkInputMode(bool scannerOnly, InputModeChecker._input_modes mode)
        {
            //Pokud je vyzadovan scanner a neni scanner nebo vyhledani dle scanneru (jinak to nejde - dle nazvu nejde primo pridat a dle pozice je irelevantni, bude se odstranovat)
            //tak chyba - nemuze pokracovat
            if (scannerOnly && (mode != _input_modes.INPUT_SCANNER && mode != _input_modes.INPUT_SEARCH))
            {
                return false;
            }
            //Jakoliv jinak muze pokracovat
            return true;
        }
    }
}
