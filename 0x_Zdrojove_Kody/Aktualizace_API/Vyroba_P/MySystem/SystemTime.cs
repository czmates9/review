using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Fask.Aktualizace_API.MySystem
{
    public class SystemDateTime
    {
        //'System time structure used to pass to P/Invoke...
        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        //'P/Invoke dec for setting the system time...
        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);

        public static void SetDeviceTime(DateTime newDate)
        {
            //'Populate structure...
            //'Substitute <YOUR DATE OBJECT> with your date object returned via GPRS...

            SYSTEMTIME st;
            st.year = (short)newDate.Year;
            st.month = (short)newDate.Month;
            st.dayOfWeek = (short)newDate.DayOfWeek;
            st.day = (short)newDate.Day;
            st.hour = (short)newDate.Hour;
            st.minute = (short)newDate.Minute;
            st.second = (short)newDate.Second;
            st.milliseconds = (short)newDate.Millisecond;

            //'Set the new time...
            SetLocalTime(ref st);
        }
    }
}
