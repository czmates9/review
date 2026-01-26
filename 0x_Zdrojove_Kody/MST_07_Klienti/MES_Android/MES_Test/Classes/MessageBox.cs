using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace MES_Android
{

    public static partial class MessageBox
    {
        private static Android.App.AlertDialog.Builder ad;

        public static bool IsOpenMessageBox;

        public static void Show(Context context, string Message, string Title, MessageBoxButtons btn, EventHandler<DialogClickEventArgs> Positive_handle, EventHandler<DialogClickEventArgs> Negative_handle, EventHandler<DialogClickEventArgs> Neutral_handle)
        {

            if (MessageBox.IsOpenMessageBox)
                return;

            MessageBox.IsOpenMessageBox = true;
            ad = new Android.App.AlertDialog.Builder(context);
            ad.SetTitle(Title);
            ad.SetMessage(Message);
            ad.SetCancelable(false);

            switch (btn)
            {
                case MessageBoxButtons.OK:
                    ad.SetPositiveButton("OK", Positive_handle);
                    break;
                case MessageBoxButtons.OKCancel:
                    ad.SetPositiveButton("OK", Positive_handle);
                    ad.SetNegativeButton("Zrušit", Negative_handle);
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    ad.SetPositiveButton("Přerušit", Positive_handle);
                    ad.SetNegativeButton("Znovu", Negative_handle);
                    ad.SetNeutralButton("Ignorovat", Neutral_handle);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    ad.SetPositiveButton("Ano", Positive_handle);
                    ad.SetNegativeButton("Ne", Negative_handle);
                    ad.SetNeutralButton("Zrušit", Neutral_handle);
                    break;
                case MessageBoxButtons.YesNo:
                    ad.SetPositiveButton("Ano", Positive_handle);
                    ad.SetNegativeButton("Ne", Negative_handle);
                    break;
                case MessageBoxButtons.RetryCancel:
                    ad.SetPositiveButton("Znovu", Positive_handle);
                    ad.SetNegativeButton("Zrušit", Negative_handle);
                    break;
                default:
                    break;
            }

            //ad.SetIcon(Android.Graphics.Drawables.Drawable.CreateFromPath(System.IO.Path.Combine(@"/sdcard/Documents/TD", @"icon.png")));

            ad.Show();

        }

        public static void Show(Context context, string Message, string Title, MessageBoxButtons btn, EventHandler<DialogClickEventArgs> Positive_handle, EventHandler<DialogClickEventArgs> Negative_handle)
        {

            Show(context, Message, Title, btn, Positive_handle, Negative_handle, null);
        }

        public static void Show(Context context, string Message, string Title, MessageBoxButtons btn, EventHandler<DialogClickEventArgs> Positive_handle)
        {

            Show(context, Message, Title, btn, Positive_handle, null, null);
        }

        //public static async Task<string> Show()
        //{
        //    Thread.Sleep(10000);

        //    return "OK";
        //}


        public static void Dispose()
        {
            if (MessageBox.IsOpenMessageBox)
            {
                ad.Dispose();
                ad = null;
                MessageBox.IsOpenMessageBox = false;
            }
        }

        #region EXAMPLE EXIT EVENT

        //private void MB_OK(object sender, DialogClickEventArgs e)
        //{

        //    MessageBox.Dispose();
        //}
        #endregion

        #region MyRegion

        public static DialogResult Show(AppCompatActivity _CurrentContext, string message, string caption, MessageBoxButtons buttons)
        {

            _IsCurrentlyInConfirmProcess = true;

            if (_CurrentContext != null)
            {
                EventHandler<DialogClickEventArgs> callback_OK = OnConfirmCallBack_OK;
                EventHandler<DialogClickEventArgs> callback_Cancel = OnConfirmCallBack_Cancel;
                EventHandler<DialogClickEventArgs> callback_Abort = OnConfirmCallBack_Abort;
                EventHandler<DialogClickEventArgs> callback_Retry = OnConfirmCallBack_Retry;
                EventHandler<DialogClickEventArgs> callback_Ignore = OnConfirmCallBack_Ignore;
                EventHandler<DialogClickEventArgs> callback_Yes = OnConfirmCallBack_Yes;
                EventHandler<DialogClickEventArgs> callback_No = OnConfirmCallBack_No;


                Action messageBoxDelegate = null;


                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_OK);
                        break;
                    case MessageBoxButtons.OKCancel:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_OK, callback_Cancel);
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_Abort, callback_Retry, callback_Ignore);
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_Yes, callback_No, callback_Cancel);
                        break;
                    case MessageBoxButtons.YesNo:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_Yes, callback_No);
                        break;
                    case MessageBoxButtons.RetryCancel:
                        messageBoxDelegate = () => MessageBox.Show(_CurrentContext, message, caption, buttons, callback_Retry, callback_Cancel);
                        break;
                }

                _CurrentContext.RunOnUiThread(messageBoxDelegate);
                while (_IsCurrentlyInConfirmProcess)
                {
                    Thread.Sleep(1000);
                }
            }

            MessageBox.Dispose();
            return _ConfirmBoxResult;

        }


        private static void OnConfirmCallBack_OK(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.OK;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_Cancel(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Cancel;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_Abort(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Abort;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_Retry(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Retry;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_Ignore(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Ignore;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_Yes(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Yes;
            _IsCurrentlyInConfirmProcess = false;
        }

        private static void OnConfirmCallBack_No(object sender, DialogClickEventArgs e)
        {
            _ConfirmBoxResult = DialogResult.No;
            _IsCurrentlyInConfirmProcess = false;
        }


        private static DialogResult _ConfirmBoxResult = DialogResult.None;
        private static bool _IsCurrentlyInConfirmProcess = true;


        #endregion

    }
}