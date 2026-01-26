using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
    public class WeightCode : BaseCode
        ,Interfaces.ICodeBarcode
        ,Interfaces.ICodeWeight
    {
        public string prefix;
        public string id;
        public decimal weight;
        public int checkDigit;

        public WeightCode(List<string> codes)
            : base(codes)
        {
        }


        public static WeightCode Parse(List<string> codes)
        {
            if (codes.Count != 1)
                return null;

            var data = codes.First();
            return Parse(data);
        }

        /// <summary>
        /// Rozparsovava vahovy kod ... 
        /// </summary>
        /// <param name="barcode">EAN 13 barcode [28,29][ZZZZZ][WWWWW][C]</param>
        /// <returns></returns>
        /// <exception></exception>
        public static WeightCode Parse(string barcode)
        {
            // pokud weight code, vratit weight code, jinak null

            // http://www.gs1-akademie.cz/info-859/archiv/info-859-cislo-46-prosinec-2014/jak-na-identifikaci-produktu-s-promennymi-jednotka-s534298774
            // 28 Z1Z2Z3Z4 KV H1H2H3H4H5
            // 
            //Význam jednotlivých pozic je následující:
            //ZX – Číslo produktu z Číselníku standardizovaného zboží a služeb vedeného GS1 Czech Republic
            //KV – Vnitřní kontrolní číslice.
            //HX – Hmotnost (kg) je explicitně vyjádřena se třemi desetinnými místy (maximální hmotnost, kterou lze zakódovat, je tedy 99,999 kg).
            //CX – Cena (Kč) je explicitně vyjádřena s jedním desetinným místem (maximální cena, kterou lze zakódovat, je tedy 9999,90 Kč).
            //K – Celková kontrolní číslice vypočítaná podle standardního algoritmu modulo 10.

            try
            {
                // pokud je vahovy kod, hledat pouze podle ID, jinak podle celeho caroveho kodu
                WeightCode wc = new WeightCode(new List<string>() { barcode });
                if (barcode.Length != 13)
                    return null;

                wc.prefix = barcode.Substring(0, 2);
                if (wc.prefix != "28" && wc.prefix != "29")
                    return null;

                // 24.6.2016 PeV: po navsteve Steinex se nepouziva ... pouzit prvnich 6 znaku jako carovy kod
                //wc.id = barcode.Substring(2, 5);
                //string weight = barcode.Substring(7, 5);
                //wc.weight = decimal.Add(decimal.Parse(weight.Substring(0, 2)), decimal.Parse(weight.Substring(2, 3)) / 1000);
                //wc.checkDigit = int.Parse(barcode.Substring(12, 1));

                wc.id = barcode.Substring(0, 6);
                string weight = barcode.Substring(7, 5);
                wc.weight = decimal.Add(decimal.Parse(weight.Substring(0, 2)), decimal.Parse(weight.Substring(2, 3)) / 1000);
                wc.checkDigit = int.Parse(barcode.Substring(12, 1));

                return wc;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }

        #region ICodeBarcode Members

        public string Barcode
        {
            get
            {
                //throw new NotImplementedException();
                return this.id; // id zbozi pro barcode => ? i itemnmbr ?
            }
        }

        #endregion

        #region ICodeWeight Members

        public decimal? Weight
        {
            get
            {
                //throw new NotImplementedException();
                return this.weight;
            }
        }

        #endregion
    }

}
