using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Extensions
{
    public static class MyTimeSpanExtensions
    {
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