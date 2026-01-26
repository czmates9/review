using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes.Common
{
    public class Dates
    {
        public const string DateFormat = "yyMMdd";

        public static DateTime Date_RRMMDD(string date)
        {
            return DateTime.ParseExact(date, DateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public const string DateFormatAustralia = "dd/MM/yy";

        public static DateTime Date_DD_Slash_MM_Slash_YY(string date)
        {
            return DateTime.ParseExact(date, DateFormatAustralia, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
    }
}
