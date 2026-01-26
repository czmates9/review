using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes.Common
{
    public class Dates
    {
		#region GS1 standard formát datumu

		public const string DateFormat = "yyMMdd";

		public static DateTime Date_RRMMDD(string date)
		{
			System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("cs-CZ");
			cultureInfo.Calendar.TwoDigitYearMax = 2099;
			return DateTime.ParseExact(date, DateFormat, cultureInfo);
		} 

		#endregion

		#region Australský formát datumu

		public const string DateFormatAustralia = "dd/MM/yy";

		public static DateTime Date_DD_Slash_MM_Slash_YY(string date)
		{
			try
			{
				//System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("cs-CZ");
				System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-AU");
				cultureInfo.Calendar.TwoDigitYearMax = 2099;
				return DateTime.ParseExact(date, DateFormatAustralia, cultureInfo);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		} 

		#endregion

		#region SAB_GS1_BELDICO tkz. belgická prasárna

		public const string DateFormat_Beldico = "yyyyMMdd";

		public static DateTime Date_RRRRMM(string date)
		{
			System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("cs-CZ");
			cultureInfo.Calendar.TwoDigitYearMax = 2099;
			return DateTime.ParseExact(date, DateFormat_Beldico, cultureInfo);
		}

		#endregion


    }
}
