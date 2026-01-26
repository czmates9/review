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
using System.Threading;

namespace MES_Android.Classes_ProgresDialog
{
    public class Dialog_Progress : Android.Support.V4.App.DialogFragment
    {

        private string _zprava = string.Empty;
        public string Zprava
        {
            get { return _zprava; }
            set { _zprava = value; }
        }

        private TextView mTextView;
        private ProgressBar mProgressBar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_Progress, container, false);

            mTextView = view.FindViewById<TextView>(Resource.Id.txt_Dialog_Progress);
            mTextView.Text = _zprava;

            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.Dialog_progressBar);

            int progressBarStatus = 0;
            bool increm = true;

            ///run thread for increase progress bar
            new Thread(new ThreadStart(delegate {
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
                    mProgressBar.SecondaryProgress =  progressBarStatus + 40;
                    Thread.Sleep(10);//// slep foe 100 ms
                }
            })).Start();

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}