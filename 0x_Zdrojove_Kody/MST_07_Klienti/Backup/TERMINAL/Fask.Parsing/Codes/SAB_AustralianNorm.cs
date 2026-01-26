using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes
{

	class SAB_AustralianNorm :
	BaseCode,
	Interfaces.ICodeBarcode,
	//Interfaces.ICodeSerltnmbr,
	Interfaces.ICodeSarze,
	Interfaces.ICodeExpiration
	{

		#region ICodeBarcode Members

		private string _barcode;
		public string Barcode
		{
			get { return _barcode; }
		}

		#endregion

		//#region ICodeSerltnmbr Members

		////private string _serltnmbr;
		//public string Serltnmbr
		//{
		//    get { return _sarze; }
		//}

		//#endregion

		#region ICodeExpiration Members

		public DateTime? _expiration;
		public DateTime? Expiration
		{
			get { return _expiration; }
		}

		#endregion

		#region ICodeSarze Members

		private string _sarze;
		public string Sarze
		{
			get { return _sarze; }
		}

		#endregion

		public SAB_AustralianNorm(List<string> codes) : base(codes)
		{
		}


		public static SAB_AustralianNorm Parse(List<string> codes)
		{
			if (codes.Count != 1)
				return null;

			var data = codes.First();
			return Parse(data);
		}

		public static SAB_AustralianNorm Parse(string data)
		{
			try
			{
				SAB_AustralianNorm bss = new SAB_AustralianNorm(new List<string>() { data });


				string[] arr = data.Split('|');

				if (arr.Count() != 6)
					return null;

				if (arr[0].Trim() != "V2")
					return null;


				foreach (var item in arr)
				{
					if (item.Trim() == "V2")
						continue;

					string AI = item.Substring(0, 2);

					if (AI == "I:")
					{
						string REF = item.Substring(2);
						bss._barcode = REF.Trim();
					}
					else if (AI == "B:")
					{
						string LOT = item.Substring(2);
						bss._sarze = LOT.Trim();
					}
					else if (AI == "U:")
					{
						string ddmmyy = item.Substring(2);
						bss._expiration = Common.Dates.Date_DD_Slash_MM_Slash_YY(ddmmyy);
					}
					else if (AI == "S:")
					{
						//Neznamy identifikator
						continue;
					}
					else if (AI == "X:")
					{
						//Neznamy identifikator
						continue;
					}

				}

			return bss;
			}
			catch
			{
				return null;
			}
		}


	}

}