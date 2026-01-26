using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Fask.Interfaces;
using MES_Android.Extensions;
using MES_Android.Prodej;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MES_Android.Classes
{
    public class InputBoxAsync
    {
        public static TaskCompletionSource<InputBoxAsync_Result> tcs;
        public static EditText nameEditText;


        //private static Fask.Scanner.Zebra_EMDK.Zebra_EMDK scanner_Zebra = null;

        public static Context _context;

        public static Android.App.AlertDialog ad = null;

        public static Task<InputBoxAsync_Result> Show(
            Context _CurrentContext, 
            string Title = null, 
            string Message = null,
            string Defaultvalue = null,
            bool AllowEmpty = false,
            bool checkLen = false,
            decimal len = 0,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            Android.Text.InputTypes keyboardMode = Android.Text.InputTypes.ClassText
            )
        {

            _context = _CurrentContext;
            tcs = new TaskCompletionSource<InputBoxAsync_Result>();

            //scanner_Zebra = new Fask.Scanner.Zebra_EMDK.Zebra_EMDK(_context);
            //scanner_Zebra.ScannerEvent += Scanner_Zebra_ScannerEvent;
            //scanner_Zebra.StatusEvent += Scanner_StatusEvent;

            Zebra_Scanner.BarcodeScanner.getInstance(_context);
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent += Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent += Scanner_StatusEvent;


            if (_CurrentContext != null)
            {

                LayoutInflater inflater = (LayoutInflater)_CurrentContext.GetSystemService(Context.LayoutInflaterService);
                View formElementsView = inflater.Inflate(Resource.Layout.InputBox, null, false);
                nameEditText = (EditText)formElementsView.FindViewById(Resource.Id.InputBox_EditText);
                //var nameEditText = (EditText)formElementsView.FindViewById(Resource.Id.InputBox_EditText);

                var mgr = (Android.Views.InputMethods.InputMethodManager)_CurrentContext.GetSystemService(Context.InputMethodService);
                mgr.ShowSoftInput(formElementsView, Android.Views.InputMethods.ShowFlags.Forced);

                nameEditText.SetBackgroundResource(Resource.Drawable.focus_border_style);
                nameEditText.SetRawInputType(keyboardMode);
                nameEditText.SetMaxLines(1);
                nameEditText.RequestFocus();

                nameEditText.FocusChange += NameEditText_FocusChange;

                if (!string.IsNullOrEmpty(Defaultvalue))
                    nameEditText.Text = Defaultvalue;
                

                using (var adBuilder = new Android.App.AlertDialog.Builder(_CurrentContext, Resource.Style.Theme_AppCompat_DayNight_DialogWhenLarge))
                {
                    if(!string.IsNullOrEmpty(Title))
                        adBuilder.SetTitle(Title);


                    if (!string.IsNullOrEmpty(Message))
                        adBuilder.SetMessage(Message);

                    adBuilder.SetCancelable(false);

                    //ad.SetPositiveButton("OK", OnConfirmCallBackA);
                    //ad.SetNegativeButton("Zrušit", OnConfirmCallBackB);
                    switch (buttons)
                    {
                        case MessageBoxButtons.OK:
                            adBuilder.SetPositiveButton("OK", OnConfirmCallBackOK);
                            break;
                        case MessageBoxButtons.OKCancel:
                            adBuilder.SetPositiveButton("OK", OnConfirmCallBackOK);
                            adBuilder.SetNegativeButton("Zrušit", OnConfirmCallBackCancel);
                            break;
                        case MessageBoxButtons.AbortRetryIgnore:
                            adBuilder.SetPositiveButton("Přerušit", OnConfirmCallBackAbort);
                            adBuilder.SetNegativeButton("Znovu", OnConfirmCallBackRetry);
                            adBuilder.SetNeutralButton("Ignorovat", OnConfirmCallBackIgnore);
                            break;
                        case MessageBoxButtons.YesNoCancel:
                            adBuilder.SetPositiveButton("Ano", OnConfirmCallBackYes);
                            adBuilder.SetNegativeButton("Ne", OnConfirmCallBackNo);
                            adBuilder.SetNeutralButton("Zrušit", OnConfirmCallBackCancel);
                            break;
                        case MessageBoxButtons.YesNo:
                            adBuilder.SetPositiveButton("Ano", OnConfirmCallBackYes);
                            adBuilder.SetNegativeButton("Ne", OnConfirmCallBackNo);
                            break;
                        case MessageBoxButtons.RetryCancel:
                            adBuilder.SetPositiveButton("Znovu", OnConfirmCallBackRetry);
                            adBuilder.SetNegativeButton("Zrušit", OnConfirmCallBackCancel);
                            break;
                        default:
                            break;
                    }

                    adBuilder.SetView(formElementsView);

                    ad = adBuilder.Show();

                    InputMethodManager imm = (InputMethodManager)_CurrentContext.GetSystemService(Context.InputMethodService);
                    imm.ToggleSoftInput(ShowFlags.Forced, 0);
                }
            }

            return tcs.Task;
        }

        private static void NameEditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((EditText)sender).HideKeyboard();
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.focus_border_style);
            }
            else
            {
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.lost_focus_style);

            }
        }

        #region Scanner

        private static void Scanner_Zebra_ScannerEvent(object sender, ScannerEventArgs e)
        {
            try
            {

                //nameEditText.Text = e.Data;
                //var adactivity = ad.Context as AppCompatActivity;
                var adactivity = _context as AppCompatActivity;
                adactivity?.RunOnUiThread(() => { nameEditText.Text = e.Data; });
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }        
        }

        private static void Scanner_StatusEvent(object sender, StatusEventArgs e)
        {
            //Zde zasila z eventu stav scanneru....
        } 
        #endregion

        private static void OnConfirmCallBackOK(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result() { 
                Dialog_Result = DialogResult.OK,
                Value = nameEditText.Text
            });
        }

        private static void OnConfirmCallBackCancel(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.Cancel
            });
        }

        private static void OnConfirmCallBackAbort(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.Abort
            });
        }

        private static void OnConfirmCallBackRetry(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.OK,
                Value = nameEditText.Text
            });
        }

        private static void OnConfirmCallBackIgnore(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.Ignore
            });
        }

        private static void OnConfirmCallBackYes(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.OK,
                Value = nameEditText.Text
            });
        }

        private static void OnConfirmCallBackNo(object sender, DialogClickEventArgs e)
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;

            tcs.SetResult(new InputBoxAsync_Result()
            {
                Dialog_Result = DialogResult.No
            });
        }



    }

    public class InputBoxAsync_Result
    {
        public DialogResult Dialog_Result;
        
        private string _value = null;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}