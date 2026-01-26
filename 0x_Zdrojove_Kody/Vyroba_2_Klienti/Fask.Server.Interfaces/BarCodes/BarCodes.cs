using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.BarCodes
{
    public class BarCodes
    {
        /// <summary>
        /// Vypocet kontrolniho cisla : Modulo 10
        /// </summary>
        /// <param name="newSSCC"></param>
        /// <returns></returns>
        /// <remarks>Mod 10 Check Digit
        ///The calculations for determining the Mod 10 Check Digit character are as follows:
        ///1. Start at the first position and add the value of every other position together.
        ///0 + 2 + 4 + 6 + 8 + 0 = 20
        ///2. The result of Step 1 is multiplied by 3.
        ///20 x 3 = 60
        ///3. Start at the second position and add the value of every other position together.
        ///1 + 3 + 5 + 7 + 9 = 25
        ///4. The results of steps 1 and 3 are added together.
        ///60 + 25 = 85
        ///5. The check character (12th character) is the smallest number which, when added to the
        ///result in step 4, produces a multiple of 10.
        ///85 + X = 90 (next higher multiple of 10)
        ///X = 5 Check Character
        ///</remarks>
        public static string CountParity_Modulo10(string barcode)
        {
            int sumLiche = 0;
            int sumSude = 0;

            // index : hodnota
            // 0,1 : "0"
            // 2-19 : cisla
            // 20 : kontrolni cislo
            for (int i = 2; i < barcode.Length; i++)
            {
                if ((i + 1) % 2 == 0) //Sude poradove cislo
                    sumSude += int.Parse(barcode[i].ToString());
                else //je liche poradove cislo
                    sumLiche += int.Parse(barcode[i].ToString());
            }

            return ((10 - (sumLiche * 3 + sumSude) % 10) % 10).ToString();

        }
    }
}
