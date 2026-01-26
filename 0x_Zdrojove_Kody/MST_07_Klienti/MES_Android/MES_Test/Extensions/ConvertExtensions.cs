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

namespace MES_Android.Extensions
{

    public class HelpObject : Java.Lang.Object
    {
        public System.Object Object { get; set; }
    }


    public static  class ConvertExtensions
    {

        public static HelpObject Object_2_JObject(this Object o)
        {
            HelpObject obj = new HelpObject();
            obj.Object = o;
            return obj;
        }

        public static Object JObject_2_Object(this HelpObject o)
        {
            return o.Object;
        }

    }
}