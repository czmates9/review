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
    public static class MyDateTimeExtensions
    {
        public static DateTime ChangeTime(
    this DateTime dateTime,
    int? Year = null,
    int? Month = null,
    int? Day = null,
    int? Hour = null,
    int? Minute = null
    )
        {
            int Year_L = dateTime.Year;
            int Month_L = dateTime.Month;
            int Day_L = dateTime.Day;
            int Hours_L = dateTime.Hour;
            int Minutes_L = dateTime.Minute;

            if (Year.HasValue)
            {
                if (dateTime.Year != Year.Value)
                    Year_L = Year.Value; 
            }

            if (Month.HasValue)
            {
                if (dateTime.Month != Month.Value)
                    Month_L = Month.Value; 
            }

            if (Day.HasValue)
            {
                if (dateTime.Day != Day.Value)
                    Day_L = Day.Value; 
            }

            if (Hour.HasValue)
            {
                if (dateTime.Hour != Hour.Value)
                    Hours_L = Hour.Value; 
            }

            if (Minute.HasValue)
            {
                if (dateTime.Minute != Minute.Value)
                    Minutes_L = Minute.Value; 
            }

            return new DateTime(
                Year_L,
                Month_L,
                Day_L,
                Hours_L,
                Minutes_L,
                0);
        }
    }
}