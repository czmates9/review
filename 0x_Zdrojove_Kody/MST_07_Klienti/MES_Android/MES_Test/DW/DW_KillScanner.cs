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

namespace MES_Android
{
    public static class DW_KillScanner_Extension
    {
        public static void DataWedge_Scanner_Disable(this AppCompatActivity parent)
        {
            try
            {
                Intent dwIntent = new Intent();
                dwIntent.SetAction("com.symbol.datawedge.api.ACTION");
                dwIntent.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "DISABLE_PLUGIN");
                parent.SendBroadcast(dwIntent);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        public static void DataWedge_Scanner_Enable(this AppCompatActivity parent)
        {
            try
            {
                Intent dwIntent = new Intent();
                dwIntent.SetAction("com.symbol.datawedge.api.ACTION");
                dwIntent.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "ENABLE_PLUGIN");
                parent.SendBroadcast(dwIntent);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

    }
}