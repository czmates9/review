using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Parsing.Codes
{
	public class SABNeznamyKod: 
		BaseCode, 
		Interfaces.ICodeBarcode, 
		Interfaces.ICodeSerltnmbr,
		Interfaces.ICodeExpiration,
		Interfaces.ICodeQuantity
	{
		public override string Nazev => this.GetType().Name;

		#region ICodeBarcode Members

		private string _barcode;
		public string Barcode
		{
			get { return GTIN_EANs; }
		}

		/// <summary>
		/// SAB - rozpad,  
		/// </summary>
		public string GTIN_EANs
		{
			get
			{
				try
				{
					// gtin have to have exactly 14 chars(length 14)
					string gtin = this._barcode;
					// tests
					if (string.IsNullOrEmpty(gtin))
						return string.Empty;

					// parsing
					if (gtin[0] != '0') // 1. znak je 1-9 => vratit celych 14 znaku
						return gtin;
					if (gtin[1] != '0') // 1.znak je [0], 2. znak je [1-9] => 13 znaku => EAN13
						return gtin.Substring(1);
					if (gtin[2] != '0') // 1. a 2. znak je [0], 3. znak je [1-9] => EAN12(UPC-A) vratit jako EAN3 => 13 znaku
						return gtin.Substring(1);
					if ((gtin.Substring(0, 6) == new string('0', 6))) // 1.- 6.znak je 0 => EAN8 => vratit 8 znaku
						return gtin.Substring(6);
					else
						return gtin.Substring(1);   // vrati 13 znaku => EAN13
				}
				catch (Exception exParse)
				{
					Fask.Logging.ExceptionHandler2.Handle(exParse);
					return string.Empty;
				}
			}
		}

		#endregion

		#region ICodeSerltnmbr Members

		private string _serltnmbr;
		public string Serltnmbr
		{
			get { return _serltnmbr; }
		}

		#endregion

		#region ICodeExpiration Members

		public DateTime? _expiration;
		public DateTime? Expiration
		{
			get { return _expiration; }
		}

		#endregion

		#region ICodeQuantity Members

		public decimal? _quantity;
		public decimal? Quantity
		{
			get { return _quantity; }
		}

		#endregion

		public SABNeznamyKod(List<string> codes) : base(codes)
        {
        }


		public static SABNeznamyKod Parse(List<string> codes)
        {
            if (codes.Count != 1)
                return null;

            var data = codes.First();
            return Parse(data);
        }

		public static Task<BaseCode> ParseAsync(List<string> codes)
		{
			TaskCompletionSource<BaseCode> tcs = new TaskCompletionSource<BaseCode>();

			Task.Run(() => {

				if (codes.Count != 1)
					tcs.SetResult(null);

				var data = codes.First();
				var code = Parse(data);
				tcs.SetResult((BaseCode)code);

			});

			return tcs.Task;
		}

		public static SABNeznamyKod Parse(string data)
		{
			try
			{
				SABNeznamyKod bss = new SABNeznamyKod(new List<string>() { data });


				string[] arr = data.Split(',');

				if (arr.Count() != 6)
					return null;

				bss._barcode = arr[4];
				bss._serltnmbr = arr[1];

				try
				{
					bss._expiration = DateTime.Parse(arr[3]);
				}
				catch
				{
					bss._expiration = null;
				}

				try
				{
					string txtQTY = arr[5];
					txtQTY = txtQTY.Replace("Qty-", "");
					txtQTY = txtQTY.Replace("Pcs", "");
					bss._quantity = decimal.Parse(txtQTY);
				}
				catch (Exception)
				{
					bss._quantity = null;
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
