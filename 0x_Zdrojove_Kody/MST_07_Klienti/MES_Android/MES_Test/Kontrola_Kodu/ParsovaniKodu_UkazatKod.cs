using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using System;
using MES_Android.Classes;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android.Listner;
using Fask.Interfaces;

using System.Linq;
using Android.Graphics;
using Android.Content;
using Android.Runtime;
using Android.Views;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Fask.Parsing.Codes;

namespace MES_Android.Kontrola_Kodu
{
    [Activity(Label = "ParsovaniKodu_UkazatKod")]
    public class ParsovaniKodu_UkazatKod : Base_Aktivita
    {
        #region Parametry

        public TextView Kod;
        public TextView TypKod;
        public TextView Poznamka;

        public ListView listView;
        public List<string> ListHodnot;

        //Fask.Scanner.Zebra_EMDK.Zebra_EMDK scanner_Zebra = null;

        Fask.Parsing.Config Config = null;

        #endregion

        #region Metody aktivity, override

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.ParKod_UkazKod);

                this.DataWedge_Scanner_Disable();

                #region Presmission

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                #endregion


                listView = FindViewById<ListView>(Resource.Id.ParKod_UkazKod_ListHodnot);
                Kod = FindViewById<TextView>(Resource.Id.ParKod_UkazKod_txtKod);
                TypKod = FindViewById<TextView>(Resource.Id.ParKod_UkazKod_txtTypKod);
                Poznamka = FindViewById<TextView>(Resource.Id.ParKod_UkazKod_Poznamka);

                TypKod.Text = "Prosím nasnímejte první kód";

                Kod.Text = string.Empty;
                Poznamka.Text = string.Empty;

                var KonfBundle = Intent?.GetBundleExtra(DataInfo_Static.ParsingConfig);
                var KonfBinder =  KonfBundle?.GetBinder(DataInfo_Static.object_ParsingConfig);
                if (KonfBinder != null)
                {
                    Config = ((WrapperForBinder<Fask.Parsing.Config>)KonfBinder).getData();
                }


                //try
                //{
                //    scanner_Zebra = new Fask.Scanner.Zebra_EMDK.Zebra_EMDK();
                //    scanner_Zebra.ScannerEvent += Scanner_Zebra_ScannerEvent;
                //    scanner_Zebra.StatusEvent += Scanner_StatusEvent;
                //}
                //catch (Exception exZebra)
                //{
                //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exZebra);
                //}

                Scanner_START();

                ListHodnot = new List<string>();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Escape)
            {
                Scanner_STOP();
                Intent intent = new Intent(this, typeof(ParsovaniKodu_Konfigurace));
                this.StartActivity(intent);
                this.Finish();
            }

            return base.OnKeyDown(keyCode, e);
        }

        public override void OnBackPressed()
        {
            Scanner_STOP();
            Intent intent = new Intent(this, typeof(ParsovaniKodu_Konfigurace));
            this.StartActivity(intent);
            this.Finish();
        }

        #endregion

        #region životny cyklus aplikace

        //protected override void OnResume()
        //{
        //    scanner_Zebra?.StartScanner();
        //    base.OnResume();
        //}

        //protected override void OnPause()
        //{
        //    scanner_Zebra?.StopScanner();
        //    base.OnPause();
        //}

        //protected override void OnDestroy()
        //{
        //    scanner_Zebra?.KillScanner();
        //    base.OnDestroy();
        //}

        #endregion

        #region Zebra scanner

        public void Scanner_START()
        {
            Zebra_Scanner.BarcodeScanner.getInstance(this);
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent += Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent += Scanner_StatusEvent;
        }

        public void Scanner_STOP()
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;
        }

        private void Scanner_Zebra_ScannerEvent(object sender, ScannerEventArgs e)
        {
            this.RunOnUiThread(() =>
            {
                LogikaPoNaskenovani(this, e.Data, e.LabelType);
            });


        }

        private void Scanner_StatusEvent(object sender, StatusEventArgs e)
        {
            //Zde zasila z eventu stav scanneru....
        }



        #endregion


        #region Pomocne metody pro vypisovani testu

        private void ShowMsg_NeniParsovanyKod()
        {
            this.RunOnUiThread(() => {
                Poznamka.Text = string.Format("Nejedná se o parsovaný kód.");
            });
            
        }

        private void ShowMsg_txt(string txt)
        {
            this.RunOnUiThread(()=> {
                Poznamka.Text = txt.Trim();
            }); 
        }

        private void ShowKod_txt(string txt)
        {
            this.RunOnUiThread(() => {
                Kod.Text = txt.Trim();
            });
        }

        private void ShowKod_txt(Android.Text.SpannableString span)
        {
            this.RunOnUiThread(() => {
                Kod.SetText(span, TextView.BufferType.Spannable);
            });
        }

        private void ShowTypKod_txt(string txt)
        {
            this.RunOnUiThread(() => {
                TypKod.Text = txt.Trim();
            });
        }

        public Task ShowKody(string Kodstring, string TypKodstring)
        {
            try
            {

                return Task.Run(() =>
                {

                    //Kod.Text = string.Empty;
                    //TypKod.Text = "Prosím nasnímejte kód";

                    ShowKod_txt(string.Empty);
                    ShowTypKod_txt("Prosím nasnímejte kód");

                    if (string.IsNullOrEmpty(Kodstring))
                    {
                        //TypKod.Text = "Nasnímaný kód je prázdný!";
                        ShowTypKod_txt("Nasnímaný kód je prázdný!");
                        return;
                    }

                    //TypKod.Text = TypKodstring;
                    ShowTypKod_txt(TypKodstring);



                    if (Kodstring.Contains((char)29))
                    {
                        string znak = ((char)29).ToString();
                        Kodstring = Kodstring.Replace(znak, @"\F");
                    }


                    //int index = Kodstring.IndexOf(@"\");
                    List<int> lst = new List<int>();
                    for (int i = 0; i < Kodstring.Length; i++)
                    {
                        if (Kodstring[i] == '\\')
                        {
                            lst.Add(i);
                        }
                    }

                    if (lst.Count > 0)
                    {
                        var span = new Android.Text.SpannableString(Kodstring);
                        foreach (int index in lst)
                        {
                            string sub = Kodstring.Substring(index, 2);
                            if (sub == @"\F")
                            {
                                span.SetSpan(new Android.Text.Style.ForegroundColorSpan(Color.Red), index, index + 2, 0);
                            }
                        }

                        if (span.Count() <= 0)
                        {
                            ShowKod_txt(Kodstring);
                            //Kod.Text = Kodstring;
                        }
                        else
                        {
                            ShowKod_txt(span);
                            //Kod.SetText(span, TextView.BufferType.Spannable);

                        }
                    }
                    else
                    {
                        ShowKod_txt(Kodstring);
                        //Kod.Text = Kodstring;
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void ShowMessage(string msg, string Nadpis)
        {
            this.RunOnUiThread(()=> { 
            ShowMessageAsync(msg, Nadpis);
            });
        }

        private async void ShowMessageAsync(string msg, string Nadpis)
        {
            await MessageBoxAsync.Show(this, msg, Nadpis, MessageBoxButtons.OK);
        }

        private void ShowKodes(List<Row_Code> row_Codes)
        {
            this.RunOnUiThread(() => {
                ShowKodesAsync(row_Codes);
            });
        }

        private async void ShowKodesAsync(List<Row_Code> row_Codes)
        {
            BaseCode bc = null;

            using (ListKoduAsync listKodu = new ListKoduAsync())
            {
                var baseCode = await listKodu.Show(this, row_Codes);
                bc = baseCode.BaseCode;
            }

            SetParamsOfCode(this, bc);
        }

        #endregion

        #region Pomocne metody

        private void SetNewAdapter(AppCompatActivity parent)
        {
            try
            {
                parent.RunOnUiThread(()=> {
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListHodnot);
                    listView.Adapter = adapter;
                });

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        private async void LogikaPoNaskenovani(AppCompatActivity parent, string data, string labelType)
        {

            try
            {
                List<Task> tasks = new List<Task>();

                tasks.Add(SaveToFileCode(data, labelType));
                tasks.Add(ShowKody(data, labelType));

                await Task.WhenAll(tasks);

               var baseCodesAfterClear = await ParsujKodV3(parent, data);


                ListHodnot.Clear();

                if (baseCodesAfterClear.Count == 0)
                {
                    ShowMsg_NeniParsovanyKod();
                }
                else if (baseCodesAfterClear.Count == 1)
                {
                    BaseCode baseCode = baseCodesAfterClear.First();
                    SetParamsOfCode(parent, baseCode);
                }
                else
                {
                    List<Row_Code> row_Codes = new List<Row_Code>();

                    foreach (BaseCode item in baseCodesAfterClear)
                    {

                        row_Codes.Add(new Row_Code() { 
                            BaseCode = item,
                            Code = item.Nazev
                        });
                    }

                    ShowKodes(row_Codes);

                    //string msg = "Nalezeno vicero variant!";
                    ////await MessageBoxAsync.Show(parent, msg, "Info", MessageBoxButtons.OK);
                    //ShowMessage(msg, "Info");
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowMessage(ex.Message, "Error");
                //await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
            }

        }

        private void SetParamsOfCode(AppCompatActivity parent, BaseCode baseCode)
        {
            if (baseCode != null)
            {
                if (baseCode is Fask.Parsing.Codes.BarcodeSlashSarze)
                {
                    ShowMsg_txt("Jedná se o BarcodeSlashSarze parsovaný kód.");
                    Fask.Parsing.Codes.BarcodeSlashSarze kk = (Fask.Parsing.Codes.BarcodeSlashSarze)baseCode;

                    string txt0 = String.Format("({0}){1}", "Barcode", kk.barcode);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "Sarze", kk.sarze);
                    ListHodnot.Add(txt1);

                }
                else if (baseCode is Fask.Parsing.Codes.FenixBarcodeObal)
                {
                    ShowMsg_txt("Jedná se o FenixBarcodeObal parsovaný kód.");
                    Fask.Parsing.Codes.FenixBarcodeObal kk = (Fask.Parsing.Codes.FenixBarcodeObal)baseCode;

                    string txt0 = String.Format("({0}){1}", "LOT", kk.LOT);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "MAT_ID", kk.MAT_ID);
                    ListHodnot.Add(txt1);
                    string txt3 = String.Format("({0}){1}", "PURCHASEFORMNUMBER", kk.PURCHASEFORMNUMBER);
                    ListHodnot.Add(txt3);
                    string txt4 = String.Format("({0}){1}", "QTY", kk.QTY);
                    ListHodnot.Add(txt4);
                }
                else if (baseCode is Fask.Parsing.Codes.GS1)
                {
                    ShowMsg_txt("Jedná se o GS1 parsovaný kód.");
                    Fask.Parsing.Codes.GS1 kk = (Fask.Parsing.Codes.GS1)baseCode;

                    foreach (var item in kk.AIList)
                    {
                        string txt = String.Format("({0}){1}", item.Key, item.Value);
                        ListHodnot.Add(txt);
                    }
                }
                else if (baseCode is Fask.Parsing.Codes.GS1_StriktniNorma)
                {
                    ShowMsg_txt("Jedná se o GS1_StriktniNorma parsovaný kód.");
                    Fask.Parsing.Codes.GS1_StriktniNorma kk = (Fask.Parsing.Codes.GS1_StriktniNorma)baseCode;

                    foreach (var item in kk.Result)
                    {
                        string txt = String.Format("({0}){1}", item.Key.AI, item.Value);
                        ListHodnot.Add(txt);
                    }
                }
                else if (baseCode is Fask.Parsing.Codes.HIBC)
                {
                    ShowMsg_txt("Jedná se o HIBC parsovaný kód.");
                    Fask.Parsing.Codes.HIBC kk = (Fask.Parsing.Codes.HIBC)baseCode;

                    string txt0 = String.Format("({0}){1}", "DATE", kk.DATE);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "LOT", kk.LOT);
                    ListHodnot.Add(txt1);
                    string txt2 = String.Format("({0}){1}", "MAT_ID", kk.MAT_ID);
                    ListHodnot.Add(txt2);
                    string txt3 = String.Format("({0}){1}", "QTY", kk.QTY);
                    ListHodnot.Add(txt3);
                    string txt4 = String.Format("({0}){1}", "SERIALNUMBER", kk.SERIALNUMBER);
                    ListHodnot.Add(txt4);
                    string txt5 = String.Format("({0}){1}", "SUPPLIER_CODE", kk.SUPPLIER_CODE);
                    ListHodnot.Add(txt5);
                }
                else if (baseCode is Fask.Parsing.Codes.SABNeznamyKod)
                {
                    ShowMsg_txt("Jedná se o SABNeznamyKod parsovaný kód.");
                    Fask.Parsing.Codes.SABNeznamyKod kk = (Fask.Parsing.Codes.SABNeznamyKod)baseCode;

                    string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "Expiration", kk.Expiration);
                    ListHodnot.Add(txt1);
                    string txt2 = String.Format("({0}){1}", "Quantity", kk.Quantity);
                    ListHodnot.Add(txt2);
                    string txt3 = String.Format("({0}){1}", "Serltnmbr", kk.Serltnmbr);
                    ListHodnot.Add(txt3);
                }
                else if (baseCode is Fask.Parsing.Codes.SAB_AustralianNorm)
                {
                    ShowMsg_txt("Jedná se o SAB_AustralianNorm parsovaný kód.");
                    Fask.Parsing.Codes.SAB_AustralianNorm kk = (Fask.Parsing.Codes.SAB_AustralianNorm)baseCode;

                    string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "Expiration", kk.Expiration);
                    ListHodnot.Add(txt1);
                    string txt3 = String.Format("({0}){1}", "Serltnmbr", kk.Serltnmbr);
                    ListHodnot.Add(txt3);
                }
                else if (baseCode is Fask.Parsing.Codes.SAB_GS1_Zavorky)
                {
                    ShowMsg_txt("Jedná se o SAB_GS1_Zavorky parsovaný kód.");
                    Fask.Parsing.Codes.SAB_GS1_Zavorky kk = (Fask.Parsing.Codes.SAB_GS1_Zavorky)baseCode;

                    foreach (var item in kk.AIList)
                    {
                        string txt = String.Format("({0}){1}", item.Key, item.Value);
                        ListHodnot.Add(txt);
                    }
                }
                else if (baseCode is Fask.Parsing.Codes.SAB_GS1_BALTON)
                {
                    ShowMsg_txt("Jedná se o SAB_GS1_BALTON parsovaný kód.");
                    Fask.Parsing.Codes.SAB_GS1_BALTON kk = (Fask.Parsing.Codes.SAB_GS1_BALTON)baseCode;

                    foreach (var item in kk.AIList)
                    {
                        string txt = String.Format("({0}){1}", item.Key, item.Value);
                        ListHodnot.Add(txt);
                    }
                }
                else if (baseCode is Fask.Parsing.Codes.WeightCode)
                {
                    ShowMsg_txt("Jedná se o WeightCode parsovaný kód.");
                    Fask.Parsing.Codes.WeightCode kk = (Fask.Parsing.Codes.WeightCode)baseCode;

                    string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "Weight", kk.Weight);
                    ListHodnot.Add(txt1);
                }
                else if (baseCode is Fask.Parsing.Codes.WeightCode_12)
                {
                    ShowMsg_txt("Jedná se o WeightCode_12 (Vlach) parsovaný kód.");
                    Fask.Parsing.Codes.WeightCode_12 kk = (Fask.Parsing.Codes.WeightCode_12)baseCode;

                    string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
                    ListHodnot.Add(txt0);
                    string txt1 = String.Format("({0}){1}", "Weight", kk.Weight);
                    ListHodnot.Add(txt1);
                }
                else
                {
                    ShowMsg_NeniParsovanyKod();
                }
            }
            else
            {
                ShowMsg_NeniParsovanyKod();
            }

            SetNewAdapter(parent);
        }

        private Task SaveToFileCode(string Data, string LabelType)
        {

            try
            {
                return Task.Run(()=> { 

                string msg = string.Empty;

                int cnt = 0;

                foreach (char c in Data)
                {
                    msg += string.Format("Znak v pořadí:'{0}' je '{1}' a v HEX:'{2}'", cnt, c, ((int)c).ToString("X2"));
                    msg += System.Environment.NewLine;
                    cnt++;
                }

                string FileName = LabelType + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";

                string PathToFile = System.IO.Path.Combine(Classes.DataInfo_Static.ParseCodeDir, FileName);
                Fask.Logging.ExceptionHandler2.Handle(msg, PathToFile);

                });
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        private async Task<List<Fask.Parsing.Codes.BaseCode>> ParsujKodV3(AppCompatActivity parent, string kodstring)
        {
            try
            {
                TaskCompletionSource<List<Fask.Parsing.Codes.BaseCode>> tcs = new TaskCompletionSource<List<BaseCode>>();

                List<Fask.Parsing.Codes.BaseCode> baseCodes = null;

                Fask.Parsing.ParsingFactory factory = new Fask.Parsing.ParsingFactory();

                baseCodes = await factory.ParseAsync(new List<string>() { kodstring }, Config);


                List<BaseCode> baseCodesAfterClear = new List<BaseCode>();
                foreach (var item in baseCodes)
                {
                    if (item != null)
                        baseCodesAfterClear.Add(item);
                }

                tcs.SetResult(baseCodesAfterClear);

                return tcs.Task.Result;

            }
            catch (Exception ex)
            {
                ShowMsg_NeniParsovanyKod();
                return null;
            }
        }



        #endregion

        //private void ParsujKodV2(string kodstring)
        //{
        //    try
        //    {
        //        Fask.Parsing.Codes.BaseCode baseCode = null;

        //        baseCode = Fask.Parsing.ParsingFactory.Parse(kodstring, Config);

        //        if (baseCode != null)
        //        {
        //            if (baseCode is Fask.Parsing.Codes.BarcodeSlashSarze)
        //            {
        //                ShowMsg_txt("Jedná se o BarcodeSlashSarze parsovaný kód.");
        //                Fask.Parsing.Codes.BarcodeSlashSarze kk = (Fask.Parsing.Codes.BarcodeSlashSarze)baseCode;

        //                string txt0 = String.Format("({0}){1}", "Barcode", kk.barcode);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "Sarze", kk.sarze);
        //                ListHodnot.Add(txt1);

        //            }
        //            else if (baseCode is Fask.Parsing.Codes.FenixBarcodeObal)
        //            {
        //                ShowMsg_txt("Jedná se o FenixBarcodeObal parsovaný kód.");
        //                Fask.Parsing.Codes.FenixBarcodeObal kk = (Fask.Parsing.Codes.FenixBarcodeObal)baseCode;

        //                string txt0 = String.Format("({0}){1}", "LOT", kk.LOT);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "MAT_ID", kk.MAT_ID);
        //                ListHodnot.Add(txt1);
        //                string txt3 = String.Format("({0}){1}", "PURCHASEFORMNUMBER", kk.PURCHASEFORMNUMBER);
        //                ListHodnot.Add(txt3);
        //                string txt4 = String.Format("({0}){1}", "QTY", kk.QTY);
        //                ListHodnot.Add(txt4);
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.GS1)
        //            {
        //                ShowMsg_txt("Jedná se o GS1 parsovaný kód.");
        //                Fask.Parsing.Codes.GS1 kk = (Fask.Parsing.Codes.GS1)baseCode;

        //                foreach (var item in kk.AIList)
        //                {
        //                    string txt = String.Format("({0}){1}", item.Key, item.Value);
        //                    ListHodnot.Add(txt);
        //                }
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.HIBC)
        //            {
        //                ShowMsg_txt("Jedná se o HIBC parsovaný kód.");
        //                Fask.Parsing.Codes.HIBC kk = (Fask.Parsing.Codes.HIBC)baseCode;

        //                string txt0 = String.Format("({0}){1}", "DATE", kk.DATE);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "LOT", kk.LOT);
        //                ListHodnot.Add(txt1);
        //                string txt2 = String.Format("({0}){1}", "MAT_ID", kk.MAT_ID);
        //                ListHodnot.Add(txt2);
        //                string txt3 = String.Format("({0}){1}", "QTY", kk.QTY);
        //                ListHodnot.Add(txt3);
        //                string txt4 = String.Format("({0}){1}", "SERIALNUMBER", kk.SERIALNUMBER);
        //                ListHodnot.Add(txt4);
        //                string txt5 = String.Format("({0}){1}", "SUPPLIER_CODE", kk.SUPPLIER_CODE);
        //                ListHodnot.Add(txt5);
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.SABNeznamyKod)
        //            {
        //                ShowMsg_txt("Jedná se o SABNeznamyKod parsovaný kód.");
        //                Fask.Parsing.Codes.SABNeznamyKod kk = (Fask.Parsing.Codes.SABNeznamyKod)baseCode;

        //                string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "Expiration", kk.Expiration);
        //                ListHodnot.Add(txt1);
        //                string txt2 = String.Format("({0}){1}", "Quantity", kk.Quantity);
        //                ListHodnot.Add(txt2);
        //                string txt3 = String.Format("({0}){1}", "Serltnmbr", kk.Serltnmbr);
        //                ListHodnot.Add(txt3);
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.SAB_AustralianNorm)
        //            {
        //                ShowMsg_txt("Jedná se o SAB_AustralianNorm parsovaný kód.");
        //                Fask.Parsing.Codes.SAB_AustralianNorm kk = (Fask.Parsing.Codes.SAB_AustralianNorm)baseCode;

        //                string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "Expiration", kk.Expiration);
        //                ListHodnot.Add(txt1);
        //                string txt3 = String.Format("({0}){1}", "Serltnmbr", kk.Serltnmbr);
        //                ListHodnot.Add(txt3);
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.SAB_GS1_Zavorky)
        //            {
        //                ShowMsg_txt("Jedná se o SAB_GS1_Zavorky parsovaný kód.");
        //                Fask.Parsing.Codes.SAB_GS1_Zavorky kk = (Fask.Parsing.Codes.SAB_GS1_Zavorky)baseCode;

        //                foreach (var item in kk.AIList)
        //                {
        //                    string txt = String.Format("({0}){1}", item.Key, item.Value);
        //                    ListHodnot.Add(txt);
        //                }
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.WeightCode)
        //            {
        //                ShowMsg_txt("Jedná se o WeightCode parsovaný kód.");
        //                Fask.Parsing.Codes.WeightCode kk = (Fask.Parsing.Codes.WeightCode)baseCode;

        //                string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "Weight", kk.Weight);
        //                ListHodnot.Add(txt1);
        //            }
        //            else if (baseCode is Fask.Parsing.Codes.WeightCode_12)
        //            {
        //                ShowMsg_txt("Jedná se o WeightCode_12 (Vlach) parsovaný kód.");
        //                Fask.Parsing.Codes.WeightCode_12 kk = (Fask.Parsing.Codes.WeightCode_12)baseCode;

        //                string txt0 = String.Format("({0}){1}", "Barcode", kk.Barcode);
        //                ListHodnot.Add(txt0);
        //                string txt1 = String.Format("({0}){1}", "Weight", kk.Weight);
        //                ListHodnot.Add(txt1);
        //            }
        //            else
        //            {
        //                ShowMsg_NeniParsovanyKod();
        //            }
        //        }
        //        else
        //        {
        //            ShowMsg_NeniParsovanyKod();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMsg_NeniParsovanyKod();
        //        //throw ex;
        //    }
        //}



    }
}