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

using Android.Views.InputMethods;


namespace MES_Android.Classes
{
    class HideAndShowKeyboard
    {

        /**
         * Shows the soft keyboard
         */
        public void showSoftKeyboard(Android.App.Activity activity, View view)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            view.RequestFocus();
            inputMethodManager.ShowSoftInput(view, 0);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);//personal line added
        }

        /**
         * Hides the soft keyboard
         */
        public void hideSoftKeyboard(Android.App.Activity activity)
        {
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}
