using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MES_Android.Prodej;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MES_Android.Classes
{
    class InputBox
    {

        private static string cas = string.Empty;


        private static DialogResult _ConfirmBoxResult = DialogResult.None;
        private static bool _IsCurrentlyInConfirmProcess = true;

        public static DialogResult Show(AppCompatActivity _CurrentContext, string caption, string defaultvalue, out string value, bool enableScanner, Android.Text.InputTypes keyboardMode)
        {
            value = string.Empty;

            if (_CurrentContext != null)
            {
                EventHandler<DialogClickEventArgs> callbackA = OnConfirmCallBackA;
                EventHandler<DialogClickEventArgs> callbackB = OnConfirmCallBackB;

                Action messageBoxDelegate = () => AddDialog.Show(_CurrentContext, caption, "Dotaz", defaultvalue, keyboardMode, callbackA, callbackB);

                _CurrentContext.RunOnUiThread(messageBoxDelegate);
                while (_IsCurrentlyInConfirmProcess)
                {
                    Thread.Sleep(1000);
                }
            }


            value = cas;
            AddDialog.Dispose();
            return _ConfirmBoxResult;
        }

        private static void OnConfirmCallBackA(object sender, DialogClickEventArgs e)
        {
            cas = AddDialog.nameEditText.Text;
            _ConfirmBoxResult = DialogResult.OK;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBackB(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Cancel;
            _IsCurrentlyInConfirmProcess = false;
        }
    }
}