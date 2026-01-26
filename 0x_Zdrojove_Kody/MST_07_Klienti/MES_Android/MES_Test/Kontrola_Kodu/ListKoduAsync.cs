using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Kontrola_Kodu
{
    public class ListKoduAsync : IDisposable
    {
        public  TaskCompletionSource<Row_Code> tcs;
        public  Context _context;
        public  Android.App.AlertDialog ad = null;

        private  ListeViewAdapter_Row mAdapter;
        private  ListView mListView;
        private  LinearLayout mContainer;

        private  Row_List row_List;

        public void Dispose()
        {
            if (ad != null)
            {
                ad.Hide();
                ad.Dispose();
                ad = null;
            }
        }

        public  Task<Row_Code> Show(
                        Context _CurrentContext,
                        List<Row_Code> vs)
        {

            _context = _CurrentContext;
            tcs = new TaskCompletionSource<Row_Code>();

            if (_CurrentContext != null)
            {

                LayoutInflater inflater = (LayoutInflater)_CurrentContext.GetSystemService(Context.LayoutInflaterService);
                View formElementsView = inflater.Inflate(Resource.Layout.Dialog_ParsingCodes, null, false);

                mListView = formElementsView.FindViewById<ListView>(Resource.Id.listView_Tabulka_ParsingCodes);
                mContainer = formElementsView.FindViewById<LinearLayout>(Resource.Id.llContainer_Tabulka_ParsingCodes);


                row_List = new Row_List();

                row_List.Row_Codes.AddRange(vs);

                mAdapter = new ListeViewAdapter_Row(_CurrentContext, Resource.Layout.Dialog_ParsingCodes_Row, row_List.Row_Codes);
                mListView.Adapter = mAdapter;
                mListView.ItemClick += MListView_Click;

                using (var adBuilder = new Android.App.AlertDialog.Builder(_CurrentContext, Resource.Style.Theme_AppCompat_DayNight_DialogWhenLarge))
                {
                    adBuilder.SetCancelable(false);

                    adBuilder.SetView(formElementsView);
                    ad = adBuilder.Show();
                }
            }

            return tcs.Task;
        }


        private void MListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            //mOnLoginsComplete.Invoke(this, new OnLoginsEventArgs(ID_List.ID_Logins[e.Position].ID));
            tcs.SetResult(row_List.Row_Codes[e.Position]);

        }
    }
}