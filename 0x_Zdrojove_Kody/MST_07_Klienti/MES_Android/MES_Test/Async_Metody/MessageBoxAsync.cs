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
    public enum MessageBoxButtons
    {
        OK = 0,
        OKCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5,
    }

    public enum DialogResult
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7,
    }

    public class MessageBoxAsync
    {
        public static TaskCompletionSource<DialogResult> tcs;



        public static Task<DialogResult> Show(Context context, string message, string caption, MessageBoxButtons buttons)
        {
            tcs = new TaskCompletionSource<DialogResult>();

            if (context != null)
            {
                using (var ad = new Android.App.AlertDialog.Builder(context))
                {
                    ad.SetTitle(caption);
                    ad.SetMessage(message);
                    ad.SetCancelable(false);

                    switch (buttons)
                    {
                        case MessageBoxButtons.OK:
                            ad.SetPositiveButton("OK", OnConfirmCallBack_OK);
                            break;
                        case MessageBoxButtons.OKCancel:
                            ad.SetPositiveButton("OK", OnConfirmCallBack_OK);
                            ad.SetNegativeButton("Zrušit", OnConfirmCallBack_Cancel);
                            break;
                        case MessageBoxButtons.AbortRetryIgnore:
                            ad.SetPositiveButton("Přerušit", OnConfirmCallBack_Abort);
                            ad.SetNegativeButton("Znovu", OnConfirmCallBack_Retry);
                            ad.SetNeutralButton("Ignorovat", OnConfirmCallBack_Ignore);
                            break;
                        case MessageBoxButtons.YesNoCancel:
                            ad.SetPositiveButton("Ano", OnConfirmCallBack_Yes);
                            ad.SetNegativeButton("Ne", OnConfirmCallBack_No);
                            ad.SetNeutralButton("Zrušit", OnConfirmCallBack_Cancel);
                            break;
                        case MessageBoxButtons.YesNo:
                            ad.SetPositiveButton("Ano", OnConfirmCallBack_Yes);
                            ad.SetNegativeButton("Ne", OnConfirmCallBack_No);
                            break;
                        case MessageBoxButtons.RetryCancel:
                            ad.SetPositiveButton("Znovu", OnConfirmCallBack_Retry);
                            ad.SetNegativeButton("Zrušit", OnConfirmCallBack_Cancel);
                            break;
                        default:
                            break;
                    }

                    //TODO ikonu
                    //db.SetIcon(Resource.Drawable.Vyroba);


                    ad.Show();
                }

            }

            return tcs.Task;

        }

        //public Task<DialogResult> Show_A(AppCompatActivity context, string message, string caption, MessageBoxButtons buttons)
        //{
        //    tcs = new TaskCompletionSource<DialogResult>();

        //    if (context != null)
        //    {
        //        using (var ad = new Android.App.AlertDialog.Builder(context))
        //        {
        //            ad.SetTitle(caption);
        //            ad.SetMessage(message);
        //            ad.SetCancelable(false);

        //            switch (buttons)
        //            {
        //                case MessageBoxButtons.OK:
        //                    ad.SetPositiveButton("OK", OnConfirmCallBack_OK);
        //                    break;
        //                case MessageBoxButtons.OKCancel:
        //                    ad.SetPositiveButton("OK", OnConfirmCallBack_OK);
        //                    ad.SetNegativeButton("Zrušit", OnConfirmCallBack_Cancel);
        //                    break;
        //                case MessageBoxButtons.AbortRetryIgnore:
        //                    ad.SetPositiveButton("Přerušit", OnConfirmCallBack_Abort);
        //                    ad.SetNegativeButton("Znovu", OnConfirmCallBack_Retry);
        //                    ad.SetNeutralButton("Ignorovat", OnConfirmCallBack_Ignore);
        //                    break;
        //                case MessageBoxButtons.YesNoCancel:
        //                    ad.SetPositiveButton("Ano", OnConfirmCallBack_Yes);
        //                    ad.SetNegativeButton("Ne", OnConfirmCallBack_No);
        //                    ad.SetNeutralButton("Zrušit", OnConfirmCallBack_Cancel);
        //                    break;
        //                case MessageBoxButtons.YesNo:
        //                    ad.SetPositiveButton("Ano", OnConfirmCallBack_Yes);
        //                    ad.SetNegativeButton("Ne", OnConfirmCallBack_No);
        //                    break;
        //                case MessageBoxButtons.RetryCancel:
        //                    ad.SetPositiveButton("Znovu", OnConfirmCallBack_Retry);
        //                    ad.SetNegativeButton("Zrušit", OnConfirmCallBack_Cancel);
        //                    break;
        //                default:
        //                    break;
        //            }

        //            //TODO ikonu
        //            //db.SetIcon(Resource.Drawable.Vyroba);

        //            context.RunOnUiThread(()=> {
        //                ad.Show();
        //            });

                    
        //        }

        //    }

        //    return tcs.Task;

        //}


        private static void OnConfirmCallBack_OK(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.OK);
        }

        private static void OnConfirmCallBack_Cancel(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.Cancel);
        }

        private static void OnConfirmCallBack_Abort(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.Abort);
        }

        private static void OnConfirmCallBack_Retry(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.Retry);
        }

        private static void OnConfirmCallBack_Ignore(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.Ignore);
        }

        private static void OnConfirmCallBack_Yes(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.Yes);
        }

        private static void OnConfirmCallBack_No(object sender, DialogClickEventArgs e)
        {
            tcs.TrySetResult(DialogResult.No);
        }

    }
}