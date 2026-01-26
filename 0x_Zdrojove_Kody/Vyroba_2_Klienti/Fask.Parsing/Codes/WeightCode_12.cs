using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
	public class WeightCode_12 : BaseCode
	{
		public string prefix;
		public string id;
		public decimal weight;
		public int checkDigit;

		/// <summary>
		/// Parsovani vahoveho kodu 12 znaku
		/// </summary>
		/// <param name="ck"></param>
		/// <returns></returns>
		public static WeightCode_12 Parse(string ck)
		{
			try
			{

				WeightCode_12 wc = new WeightCode_12();
				if (ck.Length != 12)
					return null;

				wc.prefix = ck.Substring(0, 2);
				if (wc.prefix != "28" && wc.prefix != "29")
					return null;

				wc.id = ck.Substring(0, 6);
				string weight = ck.Substring(7, 5);
				wc.weight = decimal.Parse(weight.Substring(0, 5)) / 10;
				wc.checkDigit = 0;

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
