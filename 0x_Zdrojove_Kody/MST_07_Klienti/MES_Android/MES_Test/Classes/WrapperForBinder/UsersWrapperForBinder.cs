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
    public class UsersWrapperForBinder : Binder
    {

        private Users mData;

        public UsersWrapperForBinder(Users data)
        {
            mData = data;
        }

        public Users getData()
        {
            return mData;
        }
    }
}