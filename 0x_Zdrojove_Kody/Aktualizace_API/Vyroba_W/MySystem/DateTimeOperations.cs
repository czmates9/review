using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_W.MySystem
{
    public class DateTimeOperations
    {
        public static DateTime MinTime(DateTime act)
        {
            return new DateTime(act.Year, act.Month, act.Day, 0, 0, 0);
        }

        public static DateTime MaxTime(DateTime act)
        {
            return MinTime(act) + new TimeSpan(23, 59, 59);
        }

        public static DateTime FromTime(DateTime act)
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, act.Hour, act.Minute, act.Second);
        }

    }
}
