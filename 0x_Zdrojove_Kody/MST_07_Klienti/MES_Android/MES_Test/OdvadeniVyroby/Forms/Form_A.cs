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

namespace MES_Android.OdvadeniVyroby
{
    public abstract class Form_A : IDisposable
    {

        public virtual string NameError { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        public DialogResult ShowDialog()
        {
            throw new Exception(NameError);
        }
    }
}