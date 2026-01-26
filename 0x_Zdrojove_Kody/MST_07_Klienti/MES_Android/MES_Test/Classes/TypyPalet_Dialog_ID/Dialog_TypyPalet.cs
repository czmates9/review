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
    public class OnTypyPaletEventArgs : EventArgs
    {
        private string _typyPalet_ID;

        public string TypyPalet_ID
        {
            get { return _typyPalet_ID; }
            set { _typyPalet_ID = value; }
        }

        private string _typyPalet_Name;

        public OnTypyPaletEventArgs(string TypyPalet_ID) : base()
        {
            this._typyPalet_ID = TypyPalet_ID;
        }
    }

    public class Dialog_TypyPalet : Android.Support.V4.App.DialogFragment
    {

        //private TextView txtID;

        private ListView mListView;
        private LinearLayout mContainer;

        private FASK_ListeViewAdapter_ID mAdapter;

        public event EventHandler<OnTypyPaletEventArgs> mOnTypyPaletComplete;

        public Schema.TypyPalet.PaletyDataTable dt;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_TypyPalet, container, false);

            //view init
            mListView = view.FindViewById<ListView>(Resource.Id.listView_Tabulka_TypyPalet);
            mContainer = view.FindViewById<LinearLayout>(Resource.Id.llContainer_Tabulka_TypyPalet);


            ID_List.ID_TypyPalet.Clear();

            for (int i = 0; i < dt.Count; i++)
            {
                IDTypyPalet id = new IDTypyPalet();
                id.ID = dt[i].ID.ToString();
                id.Name = dt[i].Name.ToString();
                ID_List.ID_TypyPalet.Add(id);
            }


            mAdapter = new FASK_ListeViewAdapter_ID(view.Context, Resource.Layout.Dialog_TypyPalet_row, ID_List.ID_TypyPalet);
            mListView.Adapter = mAdapter;
            mListView.ItemClick += MListView_Click;

            return view;

        }

        private void MListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            mOnTypyPaletComplete.Invoke(this, new OnTypyPaletEventArgs(ID_List.ID_TypyPalet[e.Position].ID));
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