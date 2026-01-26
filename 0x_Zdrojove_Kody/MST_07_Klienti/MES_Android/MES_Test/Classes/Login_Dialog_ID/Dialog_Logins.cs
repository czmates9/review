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
    public class OnLoginsEventArgs : EventArgs
    {
        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }



        public OnLoginsEventArgs(string Login) : base()
        {
            this._login = Login;
        }
    }

    public class Dialog_Logins : Android.Support.V4.App.DialogFragment
    {

        //private TextView txtID;

        private ListView mListView;
        private LinearLayout mContainer;

        private FASK_ListeViewAdapter_ID mAdapter;

        public event EventHandler<OnLoginsEventArgs> mOnLoginsComplete;

        public Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_Logins, container, false);

            //view init
            mListView = view.FindViewById<ListView>(Resource.Id.listView_Tabulka_Logins);
            mContainer = view.FindViewById<LinearLayout>(Resource.Id.llContainer_Tabulka_Logins);


            ID_List.ID_Logins.Clear();

            for (int i = 0; i < dt.Count; i++)
            {
                IDLogins id = new IDLogins();
                id.Login = dt[i].Login.ToString();
                ID_List.ID_Logins.Add(id);
            }


            mAdapter = new FASK_ListeViewAdapter_ID(view.Context, Resource.Layout.Dialog_Logins_row, ID_List.ID_Logins);
            mListView.Adapter = mAdapter;
            mListView.ItemClick += MListView_Click;

            return view;

        }

        private void MListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            mOnLoginsComplete.Invoke(this, new OnLoginsEventArgs(ID_List.ID_Logins[e.Position].Login));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }


    }
}