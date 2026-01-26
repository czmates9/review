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

using System.IO;
using Android.Graphics;
using Android.Views.InputMethods;

namespace MES_Android.Classes.DetailZasoby_Dialog
{

    public class Dialog_DetailZasoby : Android.Support.V4.App.DialogFragment
    {

        TextView textView_DDZ_ITEMCODE;
        TextView textView_DDZ_ITEMDESC;

        TextView textview_DDZ_11;
        TextView textview_DDZ_12;

        TextView textview_DDZ_21;
        TextView textview_DDZ_22;

        TextView textview_DDZ_31;
        TextView textview_DDZ_32;

        TextView textview_DDZ_41;
        TextView textview_DDZ_42;

        TextView textview_DDZ_51;
        TextView textview_DDZ_52;

        public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row Row;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_DetailZasoby, container, false);

            textView_DDZ_ITEMCODE = view.FindViewById<TextView>(Resource.Id.textView_DDZ_ITEMCODE);
            textView_DDZ_ITEMDESC = view.FindViewById<TextView>(Resource.Id.textView_DDZ_ITEMDESC);


            textview_DDZ_11 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_11);
            textview_DDZ_12 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_12);

            textview_DDZ_21 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_21);
            textview_DDZ_22 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_22);

            textview_DDZ_31 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_31);
            textview_DDZ_32 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_32);

            textview_DDZ_41 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_41);
            textview_DDZ_42 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_42);

            textview_DDZ_51 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_51);
            textview_DDZ_52 = view.FindViewById<TextView>(Resource.Id.textview_DDZ_52);

            ShowValues();

            return view;

        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }

        private void ShowValues()
        {
            if (Row == null)
            {
                textView_DDZ_ITEMDESC.Text = "ERR, nenalezene data k zobrazen!";
                return;
            }

            textView_DDZ_ITEMCODE.SetText(GetString_Spannable(Resource.String.text_ITEMCODE, Row.IsITEMCODENull() ? "-" : Row.ITEMCODE.Trim()), TextView.BufferType.Spannable);
            textView_DDZ_ITEMDESC.Text = Row.IsITEMDESCNull() ? "-" : Row.ITEMDESC.Trim();


            string labelSNTrack = "Množství";

            if (Row.CZ_SerNum_Track == 0)
                labelSNTrack = "Množství";
            else if (Row.CZ_SerNum_Track == 1)
                labelSNTrack = "SN";
            else if (Row.CZ_SerNum_Track == 2)
                labelSNTrack = "Šarže";

            string labelPRICE;
            try { labelPRICE = ((decimal)Row["PRICE" + Row.ODB_ID.Trim()]).ToString(Config.Settings.UIFormatDesCisel); }
            catch { labelPRICE = "-"; }


            textview_DDZ_11.SetText(GetString_Spannable(Resource.String.text_CZ_CarKod, Row.IsVNDITNUMNull() ? string.Empty : Row.VNDITNUM.Trim()), TextView.BufferType.Spannable);
            textview_DDZ_12.SetText(GetString_Spannable(Resource.String.text_MJ, Row.MJ.Trim()), TextView.BufferType.Spannable);

            textview_DDZ_21.SetText(GetString_Spannable(Resource.String.text_CZ_SerNum_Track, labelSNTrack), TextView.BufferType.Spannable);
            textview_DDZ_22.SetText(GetString_Spannable(Resource.String.text_ITEMNMBR, Row.ITEMNMBR), TextView.BufferType.Spannable);

            textview_DDZ_31.SetText(GetString_Spannable(Resource.String.text_QTYPACK, Row.QTYPACK.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);
            textview_DDZ_32.SetText(GetString_Spannable(Resource.String.text_QTYSHPPD, Row.QTY.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);

            textview_DDZ_41.SetText(GetString_Spannable(Resource.String.text_LOCNCODE, Row.IsLOCNCODENull() ? "-" : Row.LOCNCODE.Trim()), TextView.BufferType.Spannable);
            textview_DDZ_42.SetText(GetString_Spannable(Resource.String.text_WEIGHT, Row.IsWEIGHTNull() ? "-" : Row.WEIGHT.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);

            textview_DDZ_51.SetText(GetString_Spannable(Resource.String.text_PRICE, labelPRICE), TextView.BufferType.Spannable);
            textview_DDZ_52.SetText(GetString_Spannable(Resource.String.text_TAXRATE, Row.IsTAXRATENull() ? "-" : Row.TAXRATE.ToString(Config.Settings.UIFormatDesCisel) + " %"), TextView.BufferType.Spannable);
        }

        public Android.Text.SpannableString GetString_Spannable(int ID, params object[] parametry)
        {
            var S = string.Format(this.Resources.GetString(ID), parametry);

            Android.Text.SpannableString span = new Android.Text.SpannableString(S);
            int index = S.IndexOf(':');

            string pom = S.Substring(index + 1).Trim();

            if (index > 0 && pom == "-")
            {
                span.SetSpan(new Android.Text.Style.ForegroundColorSpan(Color.Red), index + 1, S.Length, 0);
                span.SetSpan(new Android.Text.Style.StyleSpan(TypefaceStyle.Bold), index + 1, S.Length, 0);
            }
            else if (index > 0)
            {
                span.SetSpan(new Android.Text.Style.ForegroundColorSpan(Color.DarkSeaGreen), index + 1, S.Length, 0);
                span.SetSpan(new Android.Text.Style.StyleSpan(TypefaceStyle.Bold), index + 1, S.Length, 0);
            }

            return span;
        }

    }
}