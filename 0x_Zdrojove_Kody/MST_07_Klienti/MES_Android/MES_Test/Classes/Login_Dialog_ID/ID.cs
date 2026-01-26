using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MES_Android.Classes.Login_Dialog_ID
{
    public static class ID_List
    {
        public static List<IDLogins> ID_Logins;


        private static string _login;
        public static string Login
        {
            set
            {
                _login = value;
            }
        }

        static ID_List()
        {
            ID_Logins = new List<IDLogins>();
            ID_List._login = String.Empty;

        }


    }


    public class IDLogins
    {
        public string Login { set; get; }



    }
}