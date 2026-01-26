using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Aktualizace_API.Mathematics
{
    public class Checks
    {
        /// <summary>
        /// Kontroluje rad dvou cisel.
        /// </summary>
        /// <param name="cislo1">1. cislo</param>
        /// <param name="cislo2">2. cislo</param>
        /// <returns>True: cislo1 a cislo2 maji stejny rad; False: pokud cisla maji jiny rad</returns>
        public static bool KontrolaRadu(double cislo1, double cislo2)
        {
            //return Math.Abs(Math.Log10(cislo1) - Math.Log10(cislo2)) < 1;

            double log10cislo1 = Math.Log10(Math.Abs(cislo1));
            double log10cislo2 = Math.Log10(Math.Abs(cislo2));
            log10cislo1 = Math.Floor(log10cislo1);
            log10cislo2 = Math.Floor(log10cislo2);
            double rozdil = log10cislo1 - log10cislo2;
            double abs = Math.Abs(rozdil);
            bool radtejny = abs < 1;
            return radtejny;

        }
    }
}
