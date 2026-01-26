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
    public class Config_HTTPS : Android.Support.V4.App.Fragment
    {
        View view;

        RadioButton rb_Cred;
        RadioButton rb_Anon;

        EditText username;
        EditText pwd;
        EditText domena;

        CheckBox Preuth;
        CheckBox Redirect;
        CheckBox DeComp;

        RadioButton rb_OnlyInstalled;
        RadioButton rb_TrustAll;
        RadioButton rb_TrustQuery;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Config_https, container, false);

            rb_Cred = view.FindViewById<RadioButton>(Resource.Id.Config_Credential);
            rb_Anon = view.FindViewById<RadioButton>(Resource.Id.Config_Anonymous); 

            username = view.FindViewById<EditText>(Resource.Id.Config_UserName); 
            pwd = view.FindViewById<EditText>(Resource.Id.Config_Password); 
            domena = view.FindViewById<EditText>(Resource.Id.Config_Domain); 

            Preuth = view.FindViewById<CheckBox>(Resource.Id.Config_PreAuthenticate); 
            Redirect = view.FindViewById<CheckBox>(Resource.Id.Config_AllowRediretion); 
            DeComp = view.FindViewById<CheckBox>(Resource.Id.Config_AllowDeCompresion);

            rb_OnlyInstalled = view.FindViewById<RadioButton>(Resource.Id.Config_OnlyInstalled);
            rb_TrustAll = view.FindViewById<RadioButton>(Resource.Id.Config_TrustAll);
            rb_TrustQuery = view.FindViewById<RadioButton>(Resource.Id.Config_TrustQuery);

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

            //TODO here

            switch (Settings.ServerAccess)
            {
                case Settings.ServerAccessType.Anonymous:
                    rb_Anon.Checked = true;
                    break;
                case Settings.ServerAccessType.Credentials:
                    rb_Cred.Checked = true;
                    break;
                default:
                    rb_Anon.Checked = true;
                    break;
            }

            username.Text = Settings.ServerAccessUsername;
            pwd.Text = Settings.ServerAccessPassword;
            domena.Text = Settings.ServerAccessDomain;

            Preuth.Checked = Settings.ServerAccessPreauthenticate;
            Redirect.Checked = Settings.ServerAccessAllowRedirection;
            DeComp.Checked = Settings.ServerAccessAllowDecompression;


            switch (Settings.ServerAccessCertificateTrust)
            {
                case Settings.ServerAccessCertificatesTrustType.OnlyInstalled:
                    rb_OnlyInstalled.Checked = true;
                    break;
                case Settings.ServerAccessCertificatesTrustType.TrustAll:
                    rb_TrustAll.Checked = true;
                    break;
                case Settings.ServerAccessCertificatesTrustType.TrustQuery:
                    rb_TrustQuery.Checked = true;
                    break;
                default:
                    rb_TrustAll.Checked = true;
                    break;
            }




        }


    private void SaveData()
        {

            //TODO here

            if (rb_Anon.Checked)
                Settings.ServerAccess = Settings.ServerAccessType.Anonymous;
            else if (rb_Cred.Checked)
                Settings.ServerAccess = Settings.ServerAccessType.Credentials;
            else
                Settings.ServerAccess = Settings.ServerAccessType.Anonymous;


            Settings.ServerAccessUsername = username.Text;
            Settings.ServerAccessPassword = pwd.Text;
            Settings.ServerAccessDomain = domena.Text ;

            Settings.ServerAccessPreauthenticate = Preuth.Checked ;
            Settings.ServerAccessAllowRedirection = Redirect.Checked;
            Settings.ServerAccessAllowDecompression = DeComp.Checked;

            if (rb_OnlyInstalled.Checked)
                Settings.ServerAccessCertificateTrust = Settings.ServerAccessCertificatesTrustType.OnlyInstalled;
            else if (rb_TrustAll.Checked)
                Settings.ServerAccessCertificateTrust = Settings.ServerAccessCertificatesTrustType.TrustAll;
            else if (rb_TrustQuery.Checked)
                Settings.ServerAccessCertificateTrust = Settings.ServerAccessCertificatesTrustType.TrustQuery;
            else
                Settings.ServerAccessCertificateTrust = Settings.ServerAccessCertificatesTrustType.TrustAll;

            Settings.Update();
        }
    }
}