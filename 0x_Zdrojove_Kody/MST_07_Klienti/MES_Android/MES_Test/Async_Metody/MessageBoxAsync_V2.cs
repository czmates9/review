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
        #region Puvodni

        //public static TaskCompletionSource<DialogResult> tcs;

        //public static Task<DialogResult> Show(Context context, string message, string caption, MessageBoxButtons buttons)
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


        //            ad.Show();
        //        }

        //    }

        //    return tcs.Task;

        //}

        //private static void OnConfirmCallBack_OK(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.OK);
        //}

        //private static void OnConfirmCallBack_Cancel(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.Cancel);
        //}

        //private static void OnConfirmCallBack_Abort(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.Abort);
        //}

        //private static void OnConfirmCallBack_Retry(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.Retry);
        //}

        //private static void OnConfirmCallBack_Ignore(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.Ignore);
        //}

        //private static void OnConfirmCallBack_Yes(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.Yes);
        //}

        //private static void OnConfirmCallBack_No(object sender, DialogClickEventArgs e)
        //{
        //    tcs.TrySetResult(DialogResult.No);
        //}

        #endregion

        #region Test


        public static async Task<DialogResult> Show(AppCompatActivity context, string message, string caption, MessageBoxButtons buttons)
        {
            MessageBoxAsync messageBox = new MessageBoxAsync();
            var xx =  await messageBox.Show_V2(context, message, caption, buttons);
            return xx;
        }

        System.Threading.AutoResetEvent waitHandle;
        DialogResult dr = DialogResult.None;

        public async Task<DialogResult> Show_V2(AppCompatActivity context, string message, string caption, MessageBoxButtons buttons)
        {
            if (context != null)
            {
                using (var ad = new Android.App.AlertDialog.Builder(context, Resource.Style.AlertDialogTheme))
                //using (var ad = new Android.App.AlertDialog.Builder(context))
                {
                    ad.SetTitle(caption);
                    ad.SetMessage(message);
                    ad.SetCancelable(false);
                    var alerD = ad.Create();


                    await Task.Run(() =>
                    {

                         waitHandle = new AutoResetEvent(false);

                        //alerD.KeyPress += AlerD_KeyPress;

                        switch (buttons)
                        {
                            case MessageBoxButtons.OK:
                                alerD.SetButton((int)(DialogButtonType.Positive), "OK", OnConfirmCallBack_OK_V2);
                                break;
                            case MessageBoxButtons.OKCancel:
                                alerD.SetButton((int)(DialogButtonType.Positive), "OK", OnConfirmCallBack_OK_V2);
                                alerD.SetButton((int)(DialogButtonType.Negative), "Zrušit", OnConfirmCallBack_Cancel_V2);
                                break;
                            case MessageBoxButtons.AbortRetryIgnore:
                                alerD.SetButton((int)(DialogButtonType.Positive), "Přerušit", OnConfirmCallBack_Abort_V2);
                                alerD.SetButton((int)(DialogButtonType.Negative), "Znovu", OnConfirmCallBack_Retry_V2);
                                alerD.SetButton((int)(DialogButtonType.Neutral), "Ignorovat", OnConfirmCallBack_Ignore_V2);
                                break;
                            case MessageBoxButtons.YesNoCancel:
                                alerD.SetButton((int)(DialogButtonType.Positive), "Ano", OnConfirmCallBack_Yes_V2);
                                alerD.SetButton((int)(DialogButtonType.Negative), "Ne", OnConfirmCallBack_No_V2);
                                alerD.SetButton((int)(DialogButtonType.Neutral), "Zrušit", OnConfirmCallBack_Cancel_V2);
                                break;
                            case MessageBoxButtons.YesNo:
                                alerD.SetButton((int)(DialogButtonType.Positive), "Ano", OnConfirmCallBack_Yes_V2);
                                alerD.SetButton((int)(DialogButtonType.Negative), "Ne", OnConfirmCallBack_No_V2);
                                break;
                            case MessageBoxButtons.RetryCancel:
                                alerD.SetButton((int)(DialogButtonType.Positive), "Znovu", OnConfirmCallBack_Retry_V2);
                                alerD.SetButton((int)(DialogButtonType.Negative), "Zrušit", OnConfirmCallBack_Cancel_V2);
                                break;
                            default:
                                break;
                        }


                        //TODO ikonu
                        //alerD.SetIcon(Resource.Drawable.Vyroba);




                        context.RunOnUiThread(() =>
                        {
                            alerD.Show();
                        });

                      
                        waitHandle.WaitOne();

                    });


                }

            }

            return dr;
        }

 
        private void Btn_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.focus_button);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.lost_focus_button);
            }
        }

        private void OnConfirmCallBack_OK_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.OK;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_Cancel_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.Cancel;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_Abort_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.Abort;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_Retry_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.Retry;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_Ignore_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.Ignore;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_Yes_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.Yes;
            waitHandle.Set();
        }

        private void OnConfirmCallBack_No_V2(object sender, DialogClickEventArgs e)
        {
            dr = DialogResult.No;
            waitHandle.Set();
        }


        #endregion

    }
}