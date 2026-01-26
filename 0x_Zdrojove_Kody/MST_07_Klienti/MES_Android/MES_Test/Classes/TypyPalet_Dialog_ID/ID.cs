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

namespace MES_Android.Classes.TypyPalet_Dialog_ID
{
    public static class ID_List
    {
        public static List<IDTypyPalet> ID_TypyPalet;


        private static string _typyPalet;
        public static string TypyPalet
        {
            set
            {
                _typyPalet = value;
            }
        }

        static ID_List()
        {
            ID_TypyPalet = new List<IDTypyPalet>();
            _typyPalet = String.Empty;

        }
    }


    public class IDTypyPalet
    {
        public string ID { set; get; }
        public string Name { set; get; }


    }
}