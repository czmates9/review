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

namespace MES_Android.Config
{
    public class Config_Scannery : Android.Support.V4.App.Fragment
    {
        View view;

        public CheckBox checkbox_Zebra;
        public CheckBox checkbox_ZXing;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Config_Scannery, container, false);

            checkbox_Zebra = view.FindViewById<CheckBox>(Resource.Id.Config_Zebra);
            checkbox_ZXing = view.FindViewById<CheckBox>(Resource.Id.Config_ZXing);


            LoadData();

            return view;
        }


        public override void OnDestroyView()
        {
            //Ulozit vše
            SaveData();

            base.OnDestroyView();
        }

        private void LoadData()
        {
            Settings.Initialize();
            checkbox_Zebra.Checked = Settings.ONOFF_ZEBRA;
            checkbox_ZXing.Checked = Settings.ONOFF_ZXing;

        }


    private void SaveData()
        {
            Settings.ONOFF_ZEBRA = checkbox_Zebra.Checked;
            Settings.ONOFF_ZXing = checkbox_ZXing.Checked;

            Settings.Update();
        }
    }
}