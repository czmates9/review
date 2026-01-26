using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
	public class WeightCode : BaseCode
    {
        public string prefix;
        public string id;
        public decimal weight;
        public int checkDigit;

		/// <summary>
		/// Rozparsovava vahovy kod ... 
		/// </summary>
		/// <param name="ck">EAN 13 barcode [28,29][ZZZZZ][WWWWW][C]</param>
		/// <returns></returns>
		public static WeightCode Parse(string ck)
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
				WeightCode wc = new WeightCode();
				if (ck.Length != 13)
					return null;

				wc.prefix = ck.Substring(0, 2);
				if (wc.prefix != "28" && wc.prefix != "29")
					return null;

				wc.id = ck.Substring(0, 6);
				string weight = ck.Substring(7, 5);
				wc.weight = decimal.Add(decimal.Parse(weight.Substring(0, 2)), decimal.Parse(weight.Substring(2, 3)) / 1000);
				wc.checkDigit = int.Parse(ck.Substring(12, 1));

				return wc;
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				return null;
			}
		}
	}
}
