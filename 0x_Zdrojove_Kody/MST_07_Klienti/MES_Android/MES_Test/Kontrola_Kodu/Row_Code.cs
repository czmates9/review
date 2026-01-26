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

namespace MES_Android.Kontrola_Kodu
{
    public class Row_Code
    {
        public string Code { set; get; }
        public Fask.Parsing.Codes.BaseCode BaseCode { set; get; }
    }

    public class Row_List
    {
        public List<Row_Code> Row_Codes;


        //private  string _ID;
        //public  string ID
        //{
        //    set
        //    {
        //        _ID = value;
        //    }
        //}

         public Row_List()
        {
            Row_Codes = new List<Row_Code>();
           // this.ID = String.Empty;

        }


    }



}