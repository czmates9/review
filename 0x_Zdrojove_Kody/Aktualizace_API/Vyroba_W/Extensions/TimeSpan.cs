using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_W.Extensions
{
    public static class MyTimeSpanExtensions
    {
        //public static string ToStringHHmm(this TimeSpan t)
        //{
        //    return t.Hours.ToString("00") + ":" + t.Minutes.ToString("00");
        //}

        public static string ToStringHHmm(this TimeSpan ts)
        {
            string znamenko = "";
            TimeSpan ts2format = ts;
            if (ts < TimeSpan.Zero)
            {
                ts2format = ts.Duration();
                znamenko = "-";
            }

            return string.Format(znamenko + "{1:00}:{2:00}", znamenko, (int)ts2format.TotalHours, ts2format.Minutes);
        }

        public static string ToStringHHmmss(this TimeSpan ts)
        {
            string znamenko = "";
            TimeSpan ts2format = ts;
            if (ts < TimeSpan.Zero)
            {
                ts2format = ts.Duration();
                znamenko = "-";
            }

            return string.Format(znamenko + "{1:00}:{2:00}:{3:00}", znamenko, (int)ts2format.TotalHours, ts2format.Minutes, ts2format.Seconds);
        }

    }
}
