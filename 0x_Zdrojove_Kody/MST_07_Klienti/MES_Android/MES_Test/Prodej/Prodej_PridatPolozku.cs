using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener.Multi;
using Fask.SQLiteDBs.DataSets;
using MES_Android._WebReferences_Globals;
using MES_Android.Ciselniky;
using MES_Android.Classes;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Android.Graphics;
using Android.Views.InputMethods;

namespace MES_Android.Prodej
{
    public enum Volajici_ProdejPridatPolozku
    {
        LOCNCODE,
        QTY,
        SERLTNUM,
        Unknow
    }

    [Activity(Label = "@string/Prodej_PridatPolozku", Theme = "@style/AppTheme")]
    public class Prodej_PridatPolozku : Scanner_Activity
    {
        #region Parametry

        //private Volajici_ProdejPridatPolozku _volajici = Volajici_ProdejPridatPolozku.Unknow;
        //public Volajici_ProdejPridatPolozku Volajici
        //{
        //    get { return _volajici; }
        //    set { _volajici = value; }
        //}

        Users Uzivatel = null;


        EditText editText_PPP_Kod;

        Button button_PPP_Storno;
        Button button_PPP_OK;

        TextView textView_PPP_popis;

        TextView textView_PPP_ITEMCODE;
        TextView textView_PPP_ITEMDESC;

        TextView textview_PPP_11;
        TextView textview_PPP_12;

        TextView textview_PPP_21;
        TextView textview_PPP_22;

        TextView textview_PPP_31;
        TextView textview_PPP_32;

        TextView textview_PPP_41;
        TextView textview_PPP_42;

        TextView textview_PPP_51;
        TextView textview_PPP_52;


        public string Kod
        {
            set {
                this.RunOnUiThread(()=> {
                    editText_PPP_Kod.Text = value;
                });
                
            }
            get
            {
                string tmpK = null;
                this.RunOnUiThread(() => {
                    tmpK =  editText_PPP_Kod.Text;
                });
                return tmpK;
            }
        }

        protected Classes.Prodej_PPP_Objekt _ppp_data = null;
        public Classes.Prodej_PPP_Objekt PPP_data
        {
            get
            {
                return _ppp_data;
            }
            set
            {
                _ppp_data = value;
            }
        }

        private string Text = "Error";

        public decimal maxlength = 0;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {

                SetContentView(Resource.Layout.Prodej_PridatPolozku);

                this.DataWedge_Scanner_Disable();

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                var bundleU = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binderU = bundleU?.GetBinder(DataInfo_Static.object_User);
                if (binderU != null)
                {
                    Uzivatel = ((WrapperForBinder<Users>)binderU).getData();
                }

                var bundleppp = Intent?.GetBundleExtra(DataInfo_Static.PPP);
                var binderppp = bundleppp?.GetBinder(DataInfo_Static.object_PPP);
                if (binderppp != null)
                {
                    PPP_data = ((WrapperForBinder<Classes.Prodej_PPP_Objekt>)binderppp).getData();
                }

                button_PPP_OK = this.FindViewById<Button>(Resource.Id.button_PPP_OK);
                button_PPP_Storno = this.FindViewById<Button>(Resource.Id.button_PPP_Storno);

                button_PPP_OK.Click += Button_PPP_OK_Click;
                button_PPP_Storno.Click += Button_PPP_Storno_Click;

                editText_PPP_Kod = this.FindViewById<EditText>(Resource.Id.editText_PPP_Kod);

                textView_PPP_popis = this.FindViewById<TextView>(Resource.Id.textView_PPP_popis);

                textView_PPP_ITEMCODE = this.FindViewById<TextView>(Resource.Id.textView_PPP_ITEMCODE);
                textView_PPP_ITEMDESC = this.FindViewById<TextView>(Resource.Id.textView_PPP_ITEMDESC);


                textview_PPP_11 = this.FindViewById<TextView>(Resource.Id.textview_PPP_11);
                textview_PPP_12 = this.FindViewById<TextView>(Resource.Id.textview_PPP_12);

                textview_PPP_21 = this.FindViewById<TextView>(Resource.Id.textview_PPP_21);
                textview_PPP_22 = this.FindViewById<TextView>(Resource.Id.textview_PPP_22);

                textview_PPP_31 = this.FindViewById<TextView>(Resource.Id.textview_PPP_31);
                textview_PPP_32 = this.FindViewById<TextView>(Resource.Id.textview_PPP_32);

                textview_PPP_41 = this.FindViewById<TextView>(Resource.Id.textview_PPP_41);
                textview_PPP_42 = this.FindViewById<TextView>(Resource.Id.textview_PPP_42);

                textview_PPP_51 = this.FindViewById<TextView>(Resource.Id.textview_PPP_51);
                textview_PPP_52 = this.FindViewById<TextView>(Resource.Id.textview_PPP_52);


                textView_PPP_popis.Text = _ppp_data.Popis;

                ShowValues();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void Button_PPP_Storno_Click(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(Prodej_Davky));
        }

        private async void Button_PPP_OK_Click(object sender, EventArgs e)
        {

            try
            {

                if (await isRightCode(Kod))
                {

                    switch (_ppp_data.Volajici)
                    {
                        case Volajici_ProdejPridatPolozku.LOCNCODE:
                            StartAktivityNasledujici(typeof(Prodej_Davky), LOCNCODE: editText_PPP_Kod.Text);
                            break;
                        case Volajici_ProdejPridatPolozku.QTY:
                            StartAktivityNasledujici(typeof(Prodej_Davky), qty: decimal.Parse(editText_PPP_Kod.Text));
                            break;
                        case Volajici_ProdejPridatPolozku.SERLTNUM:
                            StartAktivityNasledujici(typeof(Prodej_Davky), SN: editText_PPP_Kod.Text);
                            break;
                        default:
                            await MessageBoxAsync.Show(this, "Neznamy volatel", "Error", MessageBoxButtons.OK);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            
        }

        private Task<bool> isRightCode(string Kod)
        {

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            // TODO : dalsi kontroly ??? + TEST !!!
            //Fask.Parsing.Codes.WeightCode wcode = Parsing.ParsingFactory.Parse(this.Kod) as Fask.Parsing.Codes.WeightCode;
            var code = Fask.Parsing.ParsingFactory.Parse(Kod, new Fask.Parsing.Config());

            if ((_ppp_data.Zbozi != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeWeight) && (code is Fask.Parsing.Codes.Interfaces.ICodeItemnmbr))
            {
                if (_ppp_data.Zbozi.ITEMNMBR.Trim() != (((Fask.Parsing.Codes.Interfaces.ICodeItemnmbr)code).Itemnmbr ?? string.Empty))
                {
                    MessageBoxAsync.Show(this,"Není stejné zboží.","", MessageBoxButtons.OK);
                    tcs.SetResult(false);
                    return tcs.Task;
                }

                // TODO : jak spravne nastavit mnozstvi?
                this.Kod =
                    (((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight ?? 0
                    / (_ppp_data.Zbozi.IsWEIGHTNull() || (_ppp_data.Zbozi.WEIGHT == 0) ? 1 : _ppp_data.Zbozi.WEIGHT)
                    / (_ppp_data.Zbozi.IsQTYPACKNull() || (_ppp_data.Zbozi.QTYPACK == 0) ? 1 : _ppp_data.Zbozi.QTYPACK)
                    ).ToString(Config.Settings.UIFormatDesCisel);

                tcs.SetResult(true);
                return tcs.Task;
            }



            #region TaD Novy kod


            switch (_ppp_data.Volajici)
            {
                case Volajici_ProdejPridatPolozku.LOCNCODE:
                    return base_isRightCode(Kod);
                case Volajici_ProdejPridatPolozku.QTY:
                    if (!KodIsQTY(code))
                    {
                        tcs.SetResult(false);
                        return tcs.Task;
                        
                    }
                    break;
                case Volajici_ProdejPridatPolozku.SERLTNUM:
                    if (!KodIsSERLTNUM(code))
                    {
                        tcs.SetResult(false);
                        return tcs.Task;
                    }
                    break;
                case Volajici_ProdejPridatPolozku.Unknow:
                default:
                    return base_isRightCode(Kod);
            }

            #endregion
            return base_isRightCode(Kod);
        }

        //private async Task<DialogResult> ShowMessage(string Nadpis, string Text)
        //{
        //    TaskCompletionSource<DialogResult> tcs = new TaskCompletionSource<DialogResult>();
        //    var dr = await MessageBoxAsync.Show(this, Text, Nadpis, MessageBoxButtons.YesNoCancel);
        //    tcs.SetResult(dr);

        //    return tcs.Task.Result;

        //}

        //private DialogResult ShowMessageA(string Nadpis, string Text)
        //{
        //    var x =  Task.Run(()=> {
        //        return ShowMessage(Nadpis, Text);
        //    });


        //}

        private async Task<bool> base_isRightCode(string Kod)
        {

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();


            if (!_ppp_data.AllowEmpty && Kod.Trim().Length == 0)
            {
                await MessageBoxAsync.Show(this, Resources.GetString(Resource.String.FormsSejmiKodFormVlozteKod), this.Text, MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            else if (Kod.Trim().Length == 0)
            {
                // TODO : upozorneni ze je prazdne a zda pokracovat 
                DialogResult drEmpty = await MessageBoxAsync.Show(this, "Není zadána hodnota.\nPokračovat?", this.Text, MessageBoxButtons.YesNo);
                if (drEmpty == DialogResult.No)
                    tcs.SetResult(false);
            }

            if (_ppp_data.CodeType == Android.Text.InputTypes.ClassNumber)
            {
                decimal qty = 0;
                try
                {
                    qty = decimal.Parse(Kod);
                }
                catch
                {
                    await MessageBoxAsync.Show(this, Resources.GetString(Resource.String.FormsSejmiKodFormSmiteZadavatPouzeCisla), this.Text, MessageBoxButtons.OK);
                    tcs.SetResult(false);
                }

                if (qty < -999999999 || 999999999 < qty)
                {
                    await MessageBoxAsync.Show(this, Resources.GetString(Resource.String.FormsSejmiKodFormCisloJeMimoRozsah), this.Text, MessageBoxButtons.OK);
                    tcs.SetResult(false);
                }
            }

            if (maxlength > 0)
            {
                if (Kod.Trim().Length > maxlength)
                {
                    await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.FormsSejmiKodFormKodJeMensiNezX), maxlength), this.Text, MessageBoxButtons.OK);
                    tcs.SetResult(false);
                }
            }

            if ((_ppp_data.Len > 0) && _ppp_data.CheckLen)
            {
                if (Kod.Trim().Length > _ppp_data.Len)
                {
                    if (await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.FormsSejmiKodFormKodJeDelsiNezXUlozitDotaz), _ppp_data.Len), Resources.GetString(Resource.String.FormsSejmiKodFormDotaz), MessageBoxButtons.YesNo)
                        == DialogResult.No)
                        tcs.SetResult(false);
                }
                else if (Kod.Trim().Length < _ppp_data.Len)
                {
                    if (await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.FormsSejmiKodFormKodJeKratsiNezXUlozitDotaz), _ppp_data.Len), Resources.GetString(Resource.String.FormsSejmiKodFormDotaz), MessageBoxButtons.YesNo)
                        == DialogResult.No)
                        tcs.SetResult(false);
                }
            }

            tcs.SetResult(true);

            return tcs.Task.Result;
        }




        private bool KodIsQTY(Fask.Parsing.Codes.BaseCode code)
        {
            try
            {
                //if (!Fask.MST_W.Prodej.Globals.PovolitParsovaniMnozstvi)
                //    return true;

                if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeQuantity))
                {
                    string QTYtmp = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue ? string.Empty : ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Config.Settings.UIFormatDesCisel);

                    if (string.IsNullOrEmpty(QTYtmp))
                    {
                        return true;
                    }

                    if ((code is Fask.Parsing.Codes.Interfaces.IPocetNasnimanychVariant))
                    {
                        int pocet = ((Fask.Parsing.Codes.Interfaces.IPocetNasnimanychVariant)code).PocetNasnimanychVariant;

                        if (pocet > 1)
                        {
                            this.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Config.Settings.UIFormatDesCisel);
                            return true;
                        }
                    }

                    string msg = string.Format("Nalezen parsovaný kód s výsledkem: '{0}'" +
                        System.Environment.NewLine +
                        "Původní kód je: '{1}'" +
                        System.Environment.NewLine +
                        "Přejete si použít parsovaný kód? ",
                        QTYtmp,
                        this.Kod
                        );

                    //DialogResult dr = await MessageBoxAsync.Show(this, msg, "Dotaz", MessageBoxButtons.YesNoCancel);
                    DialogResult dr = DialogResult.Yes;

                    if (dr == DialogResult.Yes)
                    {
                        this.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue ? string.Empty : ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString(Config.Settings.UIFormatDesCisel);
                        return true;
                    }
                    else if ((dr == DialogResult.Cancel))
                    {
                        return false;
                    }
                    else return true;
                }
                else
                {
                    //Nic se nedeje
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        private bool KodIsSERLTNUM(Fask.Parsing.Codes.BaseCode code)
        {
            try
            {

                // Novy parametr!!!!
                //if (!Fask.MST_W.Prodej.Globals.PovolitParsovaniSarze)
                //    return true;


                if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr))
                {
                    if (string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
                    {
                        return true;
                    }

                    if ((code is Fask.Parsing.Codes.Interfaces.IPocetNasnimanychVariant))
                    {
                        int pocet = ((Fask.Parsing.Codes.Interfaces.IPocetNasnimanychVariant)code).PocetNasnimanychVariant;

                        if (pocet > 1)
                        {
                            this.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
                            return true;
                        }
                    }

                    string msg = string.Format("Nalezen parsovaný kód s výsledkem: '{0}'" +
                        System.Environment.NewLine +
                        "Původní kód je: '{1}'" +
                        System.Environment.NewLine +
                        "Přejete si použít parsovaný kód? ",
                        ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr,
                        this.Kod
                        );

                    //DialogResult dr = MessageBox.Show(this, msg, "Dotaz", MessageBoxButtons.YesNoCancel);
                    DialogResult dr = DialogResult.Yes;

                    if (dr == DialogResult.Yes)
                    {
                        this.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
                        return true;
                    }
                    else if ((dr == DialogResult.Cancel))
                    {
                        return false;
                    }
                    else return true;
                }
                else
                {
                    //Nic se nedeje
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        private void ShowValues()
        {
            if (_ppp_data == null)
            {
                textView_PPP_ITEMDESC.Text = "ERR, nenalezene data k zobrazen!";
                return;
            }

            textView_PPP_ITEMCODE.SetText(GetString_Spannable(Resource.String.text_ITEMCODE, _ppp_data.Zbozi.IsITEMCODENull() ? "-" : _ppp_data.Zbozi.ITEMCODE.Trim()), TextView.BufferType.Spannable);
            textView_PPP_ITEMDESC.Text = _ppp_data.Zbozi.IsITEMDESCNull() ? "-" : _ppp_data.Zbozi.ITEMDESC.Trim();


            string labelSNTrack = "Množství";

            if (_ppp_data.Zbozi.CZ_SerNum_Track == 0)
                labelSNTrack = "Množství";
            else if (_ppp_data.Zbozi.CZ_SerNum_Track == 1)
                labelSNTrack = "SN";
            else if (_ppp_data.Zbozi.CZ_SerNum_Track == 2)
                labelSNTrack = "Šarže";

            string labelPRICE;
            try { labelPRICE = ((decimal)_ppp_data.Zbozi["PRICE" + _ppp_data.Odberatel.odb_typ.Trim()]).ToString(Config.Settings.UIFormatDesCisel); }
            catch { labelPRICE = "-"; }


            textview_PPP_11.SetText(GetString_Spannable(Resource.String.text_CZ_CarKod, _ppp_data.Zbozi.IsVNDITNUMNull() ? string.Empty : _ppp_data.Zbozi.VNDITNUM.Trim()), TextView.BufferType.Spannable);
            textview_PPP_12.SetText(GetString_Spannable(Resource.String.text_MJ, _ppp_data.Zbozi.MJ.Trim()), TextView.BufferType.Spannable);

            textview_PPP_21.SetText(GetString_Spannable(Resource.String.text_CZ_SerNum_Track, labelSNTrack), TextView.BufferType.Spannable);            
            textview_PPP_22.SetText(GetString_Spannable(Resource.String.text_ITEMNMBR, _ppp_data.Zbozi.ITEMNMBR), TextView.BufferType.Spannable);

            textview_PPP_31.SetText(GetString_Spannable(Resource.String.text_QTYPACK, _ppp_data.Zbozi.QTYPACK.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);
            textview_PPP_32.SetText(GetString_Spannable(Resource.String.text_QTYSHPPD, _ppp_data.Zbozi.QTY.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);

            textview_PPP_41.SetText(GetString_Spannable(Resource.String.text_LOCNCODE, _ppp_data.Zbozi.IsLOCNCODENull() ? "-" : _ppp_data.Zbozi.LOCNCODE.Trim()), TextView.BufferType.Spannable);
            textview_PPP_42.SetText(GetString_Spannable(Resource.String.text_WEIGHT, _ppp_data.Zbozi.IsWEIGHTNull() ? "-" : _ppp_data.Zbozi.WEIGHT.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);

            textview_PPP_51.SetText(GetString_Spannable(Resource.String.text_PRICE, labelPRICE), TextView.BufferType.Spannable);
            textview_PPP_52.SetText(GetString_Spannable(Resource.String.text_TAXRATE, _ppp_data.Zbozi.IsTAXRATENull() ? "-" : _ppp_data.Zbozi.TAXRATE.ToString(Config.Settings.UIFormatDesCisel) + " %"), TextView.BufferType.Spannable);
        }

        public Android.Text.SpannableString GetString_Spannable(int ID, params object[] parametry)
        {
            var S = string.Format(this.Resources.GetString(ID), parametry);

            Android.Text.SpannableString span = new Android.Text.SpannableString(S);
            int index = S.IndexOf(':');

            string pom = S.Substring(index+1).Trim();

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

        private void StartAktivityNasledujici(Type typAktivity, string SN = null, string LOCNCODE = null, decimal? qty = null)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (!string.IsNullOrEmpty(SN) || !string.IsNullOrEmpty(LOCNCODE) || qty.HasValue)
            {
                Logika.Prodej_SberDat_LogikaAsync.O_GetData o_GetData = new Logika.Prodej_SberDat_LogikaAsync.O_GetData() { 
                    status = true,
                    SERLTNUM = SN,
                    QTY = qty,
                    LOCNCODE = LOCNCODE
                };

                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_PPP_out, new WrapperForBinder<Logika.Prodej_SberDat_LogikaAsync.O_GetData>(o_GetData));
                intent.PutExtra(DataInfo_Static.PPP_out, bundle);

                //scanner_Zebra?.KillScanner();

                this.SetResult(Result.Ok, intent);
                this.Finish();
            }
            else
            {
                //scanner_Zebra?.KillScanner();
                this.SetResult(Result.Canceled, intent);
                this.Finish();
            }
        }

        #region OnBackPress

        public override void OnBackPressed()
        {
            StartAktivityNasledujici(typeof(Prodej_Davky));
        }

        #endregion

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            //if (keyCode == Keycode.Enter)
            //{
            //    StartAktivityNasledujici(typeof(Prodej_Davky));
            //}
            //else if (keyCode == Keycode.Escape)
            //{
            //    StartAktivityNasledujici(typeof(Prodej_Davky));
            //}
            
            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky));
            }

            return base.OnKeyDown(keyCode, e);

        }

        protected override void ScannerData(Fask.Interfaces.ScannerEventArgs e)
        {
            string s = e.Data;

        }

    }
}