using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using MES_Android.Listner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_Android
{
    public class Base_Aktivita : AppCompatActivity
    {
        public void OnPermission_HlasicShow(IPermissionToken token)
        {

            new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(this.Resources.GetString(Resource.String.Title))
                .SetMessage(this.Resources.GetString(Resource.String.Message))
                .SetNegativeButton(Android.Resource.String.Cancel, delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetPositiveButton(Android.Resource.String.Ok, delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetOnDismissListener(new MyDismissListner(token))
                .Show();
        }

        public async void ShowErrorMessage(AppCompatActivity context, Exception ex)
        {
            await MessageBoxAsync.Show(context, ex.Message, "Error!", MessageBoxButtons.OK);
        }

        public async void ShowMessage(AppCompatActivity context, string Message, string Nadpis)
        {
            await MessageBoxAsync.Show(context, Message, Nadpis, MessageBoxButtons.OK);
        }


        private void LogStav(string NameMethod)
        {
            bool scan = false;

            if (this is Scanner_Activity)
            {
                scan = true;
            }

            string s = string.Format("{0},{1},{2},{3}" + System.Environment.NewLine,
                DateTime.Now.ToLongTimeString(),
                NameMethod,
                this.LocalClassName,
                scan
                );

            string ZivotnyCyklusDir = System.IO.Path.Combine(Classes.DataInfo_Static.PathDir, "ZivotnyCyklus.txt");
            Fask.Logging.ExceptionHandler2.Handle(s, ZivotnyCyklusDir);
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 2
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }


        /// <summary>
        /// 3 
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 4 a spatky do 3
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 5 a do 6 anebo 7
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 6 a do 2
        /// </summary>
        protected override void OnRestart()
        {
            base.OnRestart();

            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }


        /// <summary>
        /// 7
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            LogStav(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

    }
}