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

namespace MES_Android.Classes
{
    public class Users
    {

        public Users()
        {

        }

        public void ClearAll()
        {
            ID = null;
            Login =
            Pwd =
            Hash =
            EAN =
            FIRSTNAME =
            SECONDNAME = string.Empty;
        }


        public int? ID { get; set; }

        public string Login { get; set; }

        public string Pwd { get; set; }

        public string Hash { get; set; }

        public string EAN { get; set; }

        public string FIRSTNAME { get; set; }

        public string SECONDNAME { get; set; }

    }


}