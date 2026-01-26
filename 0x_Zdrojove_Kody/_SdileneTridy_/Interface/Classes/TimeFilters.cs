using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Classes
{
    public class TimeFilters
    {

        public enum TimeVariants
        {
            unknow,
            Last_5_minutes,
            Last_15_minutes,
            Last_30_minutes,
            Last_1_hour,
            Last_3_hour,
            Last_6_hour,
            Last_12_hour,
            Last_24_hour,
            Last_2_days,
            Last_7_days,
            Last_30_days,
            Last_90_days,
            Last_6_months,
            Last_1_year,
            Last_2_year,
            Last_5_year
        }

        public TimeFilters()
        {

        }

        public static DateTime GetDateByFilter(DateTime Now, TimeVariants timeVariants)
        {
            try
            {
                switch (timeVariants)
                {
                    case TimeVariants.Last_5_minutes:
                        return Now.AddMinutes(-5);
                    case TimeVariants.Last_15_minutes:
                        return Now.AddMinutes(-15);
                    case TimeVariants.Last_30_minutes:
                        return Now.AddMinutes(-30);
                    case TimeVariants.Last_1_hour:
                        return Now.AddHours(-1);
                    case TimeVariants.Last_3_hour:
                        return Now.AddHours(-3);
                    case TimeVariants.Last_6_hour:
                        return Now.AddHours(-6);
                    case TimeVariants.Last_12_hour:
                        return Now.AddHours(-12);
                    case TimeVariants.Last_24_hour:
                        return Now.AddHours(-24);
                    case TimeVariants.Last_2_days:
                        return Now.AddDays(-2);
                    case TimeVariants.Last_7_days:
                        return Now.AddDays(-7);
                    case TimeVariants.Last_30_days:
                        return Now.AddDays(-30);
                    case TimeVariants.Last_90_days:
                        return Now.AddDays(-90);
                    case TimeVariants.Last_6_months:
                        return Now.AddMonths(-6);
                    case TimeVariants.Last_1_year:
                        return Now.AddYears(-1);
                    case TimeVariants.Last_2_year:
                        return Now.AddYears(-2);
                    case TimeVariants.Last_5_year:
                        return Now.AddYears(-5);
                    default:
                        return Now;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetMinutes(int m)
        {
            return string.Format("Posledních {0} minut", m);
        }

        private static string GetHour(int m)
        {
            return string.Format("Posledních {0} hodin", m);
        }

        private static string GetDays(int m)
        {
            return string.Format("Posledních {0} dní", m);
        }

        private static string GetMonths(int m)
        {
            return string.Format("Posledních {0} měsíců", m);
        }

        private static string GetYear(int m)
        {
            return string.Format("Posledních {0} roků", m);
        }

        public static object[] GetNumberNameRange()
        {
            return new object[]
            {
                new TimeVariantName(TimeFilters.TimeVariants.unknow, "Nevybráno"),
                new TimeVariantName(TimeFilters.TimeVariants.Last_5_minutes, GetMinutes(5)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_15_minutes, GetMinutes(15)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_30_minutes, GetMinutes(30)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_1_hour, GetHour(1)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_3_hour, GetHour(3)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_6_hour, GetHour(6)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_12_hour, GetHour(12)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_24_hour, GetHour(24)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_2_days, GetDays(2)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_7_days, GetDays(7)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_30_days, GetDays(30)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_90_days, GetDays(90)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_6_months, GetMonths(6)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_1_year, GetYear(1)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_2_year, GetYear(2)),
                new TimeVariantName(TimeFilters.TimeVariants.Last_5_year, GetYear(5))
            };
        }

    }

    public struct TimeVariantName
    {
        public TimeFilters.TimeVariants _varianta;
        public string _lokalizace;

        public TimeVariantName(TimeFilters.TimeVariants varianta, string lok)
        {
            _varianta = varianta;
            _lokalizace = lok;
        }

        public TimeFilters.TimeVariants GetTimeVarianta()
        {
            return _varianta;
        }

        public override string ToString()
        {
            return _lokalizace;
        }

        public string ToString_Filter()
        {
            return string.Format("{0};{1}", _varianta, _lokalizace);
        }
    }

}
