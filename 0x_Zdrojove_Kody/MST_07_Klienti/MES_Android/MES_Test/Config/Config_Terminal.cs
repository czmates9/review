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
    public class Config_Terminal : Android.Support.V4.App.Fragment
    {
        View view;

        public EditText edittext_Adresa;
        public EditText edittext_TerminalID;
        public EditText edittext_TimeOut;
        public CheckBox checkbox_isHTTPS;
        public EditText edittext_API_Konstant;
        public EditText edittext_API_Autorizace;

        public EditText edittext_FormatCisel;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Config_Terminal, container, false);

            edittext_Adresa = view.FindViewById<EditText>(Resource.Id.Config_Adresa);
            edittext_TerminalID = view.FindViewById<EditText>(Resource.Id.Config_TerminalID);

            edittext_TimeOut = view.FindViewById<EditText>(Resource.Id.Config_TimeOut);
            checkbox_isHTTPS = view.FindViewById<CheckBox>(Resource.Id.Config_isHTTPS);
            edittext_API_Konstant = view.FindViewById<EditText>(Resource.Id.Config_API_Konst);
            edittext_API_Autorizace = view.FindViewById<EditText>(Resource.Id.Config_API_Auth);
            edittext_FormatCisel = view.FindViewById<EditText>(Resource.Id.Config_ForCisel);

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
            edittext_Adresa.Text = Settings.Adresa_API;
            edittext_TerminalID.Text = Settings.TerminalID.ToString();

            edittext_TimeOut.Text = Settings.TimeOut.ToString();
            checkbox_isHTTPS.Checked = Settings.isHTTPS;
            edittext_API_Konstant.Text = Settings.API_konstant;
            edittext_API_Autorizace.Text = Settings.API_Autorizace;

            edittext_FormatCisel.Text = Settings.UIFormatDesCisel;
    }


    private void SaveData()
        {
            Settings.Adresa_API = edittext_Adresa.Text;
            Settings.TerminalID = byte.Parse(edittext_TerminalID.Text);

            Settings.TimeOut = int.Parse(edittext_TimeOut.Text);
            Settings.isHTTPS = checkbox_isHTTPS.Checked ;
            Settings.API_konstant = edittext_API_Konstant.Text ;
            Settings.API_Autorizace = edittext_API_Autorizace.Text ;

            Settings.UIFormatDesCisel = edittext_FormatCisel.Text ;

            Settings.Update();
        }
    }
}