using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{

    public static class AddDialog
    {
        static AlertDialog adinstance;
        private static Android.App.AlertDialog.Builder ad;
        public static EditText nameEditText;
        public static ScrollView nameScrollView;


        public static bool isOpenAddDialog;

        private static Android.Views.InputMethods.InputMethodManager mgr;

        public static void Show(Context context, string Message, string Title, string EditTextText, Android.Text.InputTypes inputtype, EventHandler<DialogClickEventArgs> Positive_handle, EventHandler<DialogClickEventArgs> Negative_handle)
        {
            try
            {

                if (!AddDialog.isOpenAddDialog)
                {

                    AddDialog.isOpenAddDialog = true;

                    LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                    View formElementsView = inflater.Inflate(Resource.Layout.InputBox, null, false);
                    nameEditText = (EditText)formElementsView.FindViewById(Resource.Id.InputBox_EditText);

                    mgr = (Android.Views.InputMethods.InputMethodManager)context.GetSystemService(Context.InputMethodService);
                    mgr.ShowSoftInput(formElementsView, Android.Views.InputMethods.ShowFlags.Forced);

                    nameEditText.SetBackgroundResource(Resource.Drawable.focus_border_style);

                    nameEditText.SetRawInputType(inputtype);


                    nameEditText.Text = EditTextText;
                    ad = new Android.App.AlertDialog.Builder(context, Resource.Style.Theme_AppCompat_DayNight_DialogWhenLarge);
                    ad.SetCancelable(false);
                    ad.SetTitle(Title);
                    ad.SetMessage(Message);
                    nameEditText.SetMaxLines(1);

                    ad.SetPositiveButton("OK", Positive_handle);
                    ad.SetNegativeButton("Zrušit", Negative_handle);

                    ad.SetView(formElementsView);

                    adinstance = ad.Show();

                    nameEditText.RequestFocus();
                    InputMethodManager imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                    imm.ToggleSoftInput(ShowFlags.Forced, 0); 
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        public static void Dispose()
        {
            ad.Dispose();
            ad = null;
            AddDialog.isOpenAddDialog = false;
        }

    }
}