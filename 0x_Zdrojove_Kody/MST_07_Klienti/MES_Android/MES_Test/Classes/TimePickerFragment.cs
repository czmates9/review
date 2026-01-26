using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Classes
{

    public class TimePickerFragment : Android.Support.V4.App.DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(TimePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                           this,
                                                           currently.Hour,
                                                           currently.Minute,
                                                           true);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(
                DateTime.MinValue.Year,
                DateTime.MinValue.Month,
                DateTime.MinValue.Day,
                hourOfDay,
                minute,
                0
                );
            Log.Debug(TAG, selectedDate.ToLongTimeString());
            _dateSelectedHandler(selectedDate);
        }
    }
}