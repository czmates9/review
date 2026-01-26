using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MES_Android.Classes
{

    public static class ProgressDialog_Infinity 
    {

        private static Android.App.AlertDialog alertDialog;
        private static Android.App.AlertDialog.Builder ad;
        private static bool isOpenProgressDialog_Infinity = false;

        private static ProgressBar mProgressBar;
        private static TextView mTextView;

        private static Thread ProgressThread;

        private static Context _context1 = null;
        public static string Message
        {
            set
            {
                (_context1 as AppCompatActivity).RunOnUiThread(() => {
                    //alertDialog.SetTitle(value);
                    mTextView.Text = value;
                });
            }
        }

        public static void Show(Context context, string Message = "čekejte prosím...")
        {
            try
            {
                _context1 = context;
                if (!ProgressDialog_Infinity.isOpenProgressDialog_Infinity)
                {

                    ProgressDialog_Infinity.isOpenProgressDialog_Infinity = true;

                    LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                    View formElementsView = inflater.Inflate(Resource.Layout.Dialog_Progress, null, false);
                    mProgressBar = (ProgressBar)formElementsView.FindViewById(Resource.Id.Dialog_progressBar);
                    mTextView = (TextView)formElementsView.FindViewById(Resource.Id.txt_Dialog_Progress);
                    
                    ad = new Android.App.AlertDialog.Builder(context);
                    ad.SetCancelable(false);
                    ad.SetView(formElementsView);

                    mTextView.Text = Message;

                    int progressBarStatus = 0;
                    bool increm = true;

                    ProgressThread = new Thread(() => {

                        while (true)
                        {

                            if (increm)
                            {
                                progressBarStatus++;
                            }
                            else
                            {
                                progressBarStatus--;
                            }

                            if (progressBarStatus == 100)
                            {
                                increm = false;

                                var x = mProgressBar.SecondaryProgressTintList;
                            }

                            if (progressBarStatus == -40)
                                increm = true;

                            mProgressBar.Progress = progressBarStatus;
                            mProgressBar.SecondaryProgress = progressBarStatus + 40;
                            Thread.Sleep(10);//// slep foe 10 ms
                        }

                    });

                    ProgressThread.Start();

                    /////run thread for increase progress bar
                    //new Thread(new ThreadStart(delegate {
                    //    while (true)
                    //    {

                    //        if (increm)
                    //        {
                    //            progressBarStatus++;
                    //        }
                    //        else
                    //        {
                    //            progressBarStatus--;
                    //        }

                    //        if (progressBarStatus == 100)
                    //        {
                    //            increm = false;

                    //            var x = mProgressBar.SecondaryProgressTintList;
                    //        }

                    //        if (progressBarStatus == -40)
                    //            increm = true;

                    //        mProgressBar.Progress = progressBarStatus;
                    //        mProgressBar.SecondaryProgress = progressBarStatus + 40;
                    //        Thread.Sleep(10);//// slep foe 10 ms
                    //    }
                    //})).Start();

                    alertDialog = ad.Show();

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw ex;
            }

        }

        public static void Dispose()
        {
            try
            {
                _context1 = null;

                if (ProgressThread != null && ProgressThread.IsAlive)
                {
                    ProgressThread.Abort();
                    ProgressThread = null;
                }

                if (alertDialog != null)
                {
                    alertDialog.Dismiss();
                    alertDialog.Dispose();
                    alertDialog = null;
                }

                if (ad != null)
                {
                    ad.Dispose();
                    ad = null;
                }

                ProgressDialog_Infinity.isOpenProgressDialog_Infinity = false;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

    }
}