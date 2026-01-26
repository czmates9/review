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
using Android.Util;
using Android.Views.InputMethods;
using Java.Lang;
using System.ComponentModel;

namespace MES_Android.Extensions
{
    public static class MyEditTextExtensions
    {

        public static void ShowKeyboard(this EditText editText)
        {
            editText.RequestFocus();

            var inputMethodManager = editText.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(editText, ShowFlags.Forced);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }

        public static void HideKeyboard(this EditText editText)
        {
            editText.RequestFocus();
            var inputMethodManager = editText.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(editText.WindowToken, HideSoftInputFlags.None); // this probably needs to be set to ToogleSoftInput, forced.
        }
    }
}