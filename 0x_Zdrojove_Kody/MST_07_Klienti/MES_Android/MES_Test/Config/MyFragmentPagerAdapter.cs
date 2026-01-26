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
using Android.Support.V4.App;

namespace MES_Android.Config
{
    public class MyFragmentPagerAdapter : FragmentPagerAdapter
    {

        System.Collections.Generic.IDictionary<int, MyFrament> SeznamFragments;

        public MyFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm, System.Collections.Generic.IDictionary<int, MyFrament> SeznamFragments)
            : base(fm)
        {
            try
            {
                this.SeznamFragments = SeznamFragments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override int Count
        {
            get
            {
                return this.SeznamFragments.Count;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            MyFrament mf = null;
            try
            {
                mf = this.SeznamFragments[position];
                return mf.Fragment;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            MyFrament mf = null;
            try
            {
                mf = this.SeznamFragments[position];
                return new Java.Lang.String(mf.Name);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return new Java.Lang.String("ERR");
            }
        }


    }
}