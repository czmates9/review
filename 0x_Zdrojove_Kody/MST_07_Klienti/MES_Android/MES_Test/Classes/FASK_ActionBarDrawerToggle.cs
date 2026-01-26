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

namespace MES_Android.Classes
{

    public class FASK_ActionBarDrawerToggle : Android.Support.V7.App.ActionBarDrawerToggle
    {
        private Activity mActivity;
        private int mOpenDrawerContentDescRes;
        private int mCloseDrawerContentDescRes;
        private Android.Support.V7.Widget.Toolbar mToolbar;


        public FASK_ActionBarDrawerToggle(Activity Activity, Android.Support.V4.Widget.DrawerLayout DrawerLayout, Android.Support.V7.Widget.Toolbar Toolbar, int OpenDrawerContentDescRes, int CloseDrawerContentDescRes)
            : base(Activity, DrawerLayout, Toolbar, OpenDrawerContentDescRes, CloseDrawerContentDescRes)
        {

            mActivity = Activity;
            mOpenDrawerContentDescRes = OpenDrawerContentDescRes;
            mCloseDrawerContentDescRes = CloseDrawerContentDescRes;
            mToolbar = Toolbar;
        }

        public FASK_ActionBarDrawerToggle(Activity activity, Android.Support.V4.Widget.DrawerLayout DrawerLayout, Android.Support.V7.Widget.Toolbar Toolbar, int OpenDrawerContentDescRes, int CloseDrawerContentDescRes, bool drawerTitle)
            : base(activity, DrawerLayout, Toolbar, OpenDrawerContentDescRes, CloseDrawerContentDescRes)
        {
            mActivity = activity;
            if (drawerTitle)
            {
                mOpenDrawerContentDescRes = OpenDrawerContentDescRes;
                mCloseDrawerContentDescRes = CloseDrawerContentDescRes;
            }
            mToolbar = Toolbar;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            try
            {
                base.OnDrawerOpened(drawerView);
                //mToolbar.SetTitle(mOpenDrawerContentDescRes);
                //mActivity.ActionBar.SetTitle(mOpenDrawerContentDescRes);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override void OnDrawerClosed(View drawerView)
        {
            try
            {
                base.OnDrawerClosed(drawerView);
                //mToolbar.SetTitle(mCloseDrawerContentDescRes);
                //mActivity.ActionBar.SetTitle(mCloseDrawerContentDescRes);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            try
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}