using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Android.Util;
using System.Threading;
using Com.Karumi.Dexter;
using Android;
using MES_Android.Listner;
using Com.Karumi.Dexter.Listener.Multi;
using System.IO;
using MES_Android.Ciselniky;

namespace MES_Android
{
    //[Activity(Label = "SplashActivity")]
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Base_Aktivita
    {
        TextView text;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.SplashScreen);

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

            text = FindViewById<TextView>(Resource.Id.splash_Text);

            SetText("Vítejte...");
            //SetText("");

        }

        public void SetText(string msg)
        {
            this.RunOnUiThread(()=> {
                text.Text = msg;
            });
        }


        protected override void OnResume()
        {
            base.OnResume();
            //Task startupWork = new Task(() => { Metoda_Pro_Start(this); });
            //startupWork.Start();

            Metoda_Pro_Start(this);
        }

        //Zakazane zrušení :D
        public override void OnBackPressed() { }

        async void Metoda_Pro_Start(AppCompatActivity parent)
        {

            try
            {

                TaskCompletionSource<bool> tcs_Settings = new TaskCompletionSource<bool>();
                TaskCompletionSource<bool> tcs_PrinterSettings = new TaskCompletionSource<bool>();
                //TaskCompletionSource<bool> tcs_Konfigurace = new TaskCompletionSource<bool>();

                await Task.Delay(500);
                SetText("Start settings...");

                if (!File.Exists(Classes.DataInfo_Static.SettingsXML))
                {
                    await MessageBoxAsync.Show(parent, "POZOR! Je vytvořená výchozí lokalní konfigurace settings.xml !", "Warning", MessageBoxButtons.OK);
                }

                //await LoadSettings(tcs_Settings);
                LoadSettings();

                SetText("Stop settings...");

                SetText("Start settings PRD...");

                if (!File.Exists(Classes.DataInfo_Static.SettingsPRD))
                {
                    await MessageBoxAsync.Show(parent, "POZOR! Je vytvořená výchozí lokalní konfigurace settings.prd !", "Warning", MessageBoxButtons.OK);
                }

                LoadSettingsPRD();

                SetText("Stop settings PRD...");

                SetText("Start print settings...");
                //await LoadPrintSettings(tcs_PrinterSettings);
                LoadPrintSettings();
                SetText("Stop print settings...");


                SetText("Start konfigurace...");
                //var result = await NacteniAsync(parent, tcs_Konfigurace);
                //var result = await NacteniKonfigurace(parent, tcs_Konfigurace);
                //var result = await NacteniKonfigurace(parent);
                var result_task = NacteniKonfigurace(parent);
                var result_bool = await result_task;
                //Task.WaitAll()
                //NacteniKonfigurace(parent);
                SetText("Stop konfigurace...");

                SetText("Start synchronizace Production");
                //metoda pro vytvoreni scriptu a prdu

                LoadProductionPRD();
                SetText("Stop synchronizace Production ");


                SetText("Start synchronizace InternalStates");
                LoadInternalStatePRD();

                //DB_synchronizace();
                SetText("Stop synchronizace InternalStates");

                SetText("Start synchronizace Výroba...");
                //Ciselniky_Helper _Helper = new Ciselniky_Helper();
                //bool state = await _Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.OdvadeniVyroby, this, Konfigurace_Singleton.Instance.Prodej.SkladID); //instance na vyrobu!!!

                //if (!state)
                //{
                //    await MessageBoxAsync.Show(this, "NEco je špatně...", "error", MessageBoxButtons.OK);
                //}

                DownloadVyrobaPRD();

                SetText("Stop synchronizace Výroba...");

                await Task.Delay(500);

                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
                this.Finish();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);

                await MessageBoxAsync.Show(this, "Aplikace bude ukončena!", "Error", MessageBoxButtons.OK);

                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

            }
        }

        #region synchronizace DB
//        private void DB_synchronizace()
//        {
//            #region Inicializace databazovych souboru, pokud neexistuji => akutalizace aplikace
//            // if file not exists - download
//            // if file.tmp exists after download, copy it to file (without tmp)
//            // delete file.tmp

//            try
//            {
//                #region Production.prd initialization

//                if (!File.Exists(Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.ProductionPrd)))
//                {
//                    DownloadProductionStart_1(CiselnikServiceOperations.Operation.Production);

//                    if (File.Exists(Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.ProductionPrdTmp)))
//                    {
//                        File.Copy(
//                                    Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.ProductionPrdTmp),
//                                    Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.ProductionPrd), false
//                                    );
//                        // => nemazat, ponechat jako zalohu, kdyby se nepovedlo stahnout pri inizializaci ... //File.Delete(Path.Combine(Classes.DataInfo_Static.PathDir, Constants.ProductionPrdTmp));
//                    }
//                }

//                #endregion

//                #region InternalState.prd initialization
//                if (!File.Exists(Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.InternalState + Classes.DataInfo_Static.PriponaPRD)))
//                {
//                    // TODO : TaD dodelat stahovani internalstate.sdf z serveru platnou verzi tu i terminal
//                    DownloadInternalStateStart_1(CiselnikServiceOperations.Operation.InternalState);

//                    //if (!File.Exists(Path.Combine(Classes.DataInfo_Static.PathDir, Constants.InternalState + Constants.PRD)))
//                    if (File.Exists(Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.InternalState + Classes.DataInfo_Static.PriponaPRD + Classes.DataInfo_Static.PriponaTMP)))
//                    {
//                        File.Copy(
//                                    Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.InternalState + Classes.DataInfo_Static.PriponaPRD + Classes.DataInfo_Static.PriponaTMP),
//                                    Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.InternalState + Classes.DataInfo_Static.PriponaPRD), false
//                                  );
//                        File.Delete(Path.Combine(Classes.DataInfo_Static.PathDir, Classes.DataInfo_Static.InternalState + Classes.DataInfo_Static.PriponaPRD + Classes.DataInfo_Static.PriponaTMP));
//                    }
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {

//                Fask.Logging.ExceptionHandler2.Handle(ex);
//            }

//            #endregion
//        }

//        private async void DownloadProductionStart_1(CiselnikServiceOperations.Operation typ)
//        {
//            try
//            {
//                // Konfigurace_Singleton.Instance.Vyroba.SkladID

//                Ciselniky_Helper _Helper = new Ciselniky_Helper();
//                bool state = await _Helper.SynchronizeCiselniky(typ, this, Konfigurace_Singleton.Instance.Prodej.SkladID); //instance na vyrobu!!!

//                if (!state)
//                {
//                    SetText("Něco je špatně...");
//                    await MessageBoxAsync.Show(this, "Něco je špatně...", "error", MessageBoxButtons.OK);
//                }

//            }
//            catch (System.Exception ex)
//            {
//                Fask.Logging.ExceptionHandler2.Handle(ex);
//            }
//        }


//        /// <summary>
//        /// Downloads Production.Prd from server
//        /// </summary>
//        private void DownloadProductionStart()
//        {
//#if false
//            try
//            {
//                DateTime lastDownload = DateTime.Now;
//                // priprava sdf na serveri
//                IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginVyrobaDBProductionPrepareZip(Settings.TerminalID, lastDownload, null, null);
//                //ceka na dokonceni asynchronni operace
//                ares.AsyncWaitHandle.WaitOne();

//                //Data predlohy - SQLCE databaze
//                //byte[] data = _wsvyroba.EndVyrobaDBDateTime(ares);
//                bool filedata = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndVyrobaDBProductionPrepareZip(ares);
//                if (!filedata)
//                    throw new Exception("Prepare data on server was not succesfull");

//                //this.UpdateStatusBarInfo("Aktualizace: Stahování databáze ze serveru");
//                //stazeni
//                //bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(filedata, Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf), true);
//                bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd), false, true);
//                if (!downloaded)
//                    throw new Exception("Download Production from server was not succesfull");


//            }
//            catch (Exception ex)
//            {
//                try { Logging.Log.Write(ex.Message, "AsyncCallbackVyrobaDownloadedProduction(IAsyncResult ar)"); }
//                catch { }
//            }
//            finally
//            {

//            } 
//#endif
//        }

//        /// <summary>
//        /// Downloads InternalState.prd from server
//        /// </summary>
//        private async void DownloadInternalStateStart_1(CiselnikServiceOperations.Operation typ)
//        {
//            try
//            {
//                // Konfigurace_Singleton.Instance.Vyroba.SkladID

//                Ciselniky_Helper _Helper = new Ciselniky_Helper();
//                bool state = await _Helper.SynchronizeCiselniky(typ, this, Konfigurace_Singleton.Instance.Prodej.SkladID); //instance na vyrobu!!!

//                if (!state)
//                {
//                    SetText("Něco je špatně...");
//                    await MessageBoxAsync.Show(this, "Něco je špatně...", "error", MessageBoxButtons.OK);
//                }

//            }
//            catch (System.Exception ex)
//            {
//                Fask.Logging.ExceptionHandler2.Handle(ex);
//            }
//        }

//        /// <summary>
//        /// Downloads InternalState.prd from server
//        /// </summary>
//        private void DownloadInternalStateStart()
//        {
//#if false
//            try
//            {
//                DateTime lastDownload = DateTime.Now;
//                // priprava sdf na serveri
//                IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginVyrobaDBInternalStatePrepareZip(Settings.TerminalID, lastDownload, null, null);
//                //ceka na dokonceni asynchronni operace
//                ares.AsyncWaitHandle.WaitOne();

//                //Data predlohy - SQLCE databaze
//                //byte[] data = _wsvyroba.EndVyrobaDBDateTime(ares);
//                bool filedata = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndVyrobaDBInternalStatePrepareZip(ares);
//                if (!filedata)
//                    throw new Exception("Prepare data on server was not succesfull");
//                //this.UpdateStatusBarInfo("Aktualizace: Stahování databáze ze serveru");
//                //stazeni
//                //bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(filedata, Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf), true);
//                bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD), false, true);
//                if (!downloaded)
//                    throw new Exception("Download InternalState from server was not succesfull");


//            }
//            catch (Exception ex)
//            {
//                try { Logging.Log.Write(ex.Message, "AsyncCallbackVyrobaDownloadedInternalState(IAsyncResult ar)"); }
//                catch { }
//            }
//            finally
//            {

//            } 
//#endif
//        }

        #endregion


        #region Funguju

        //private Task LoadSettings(TaskCompletionSource<bool> tcs)
        //{

        //    Task.Run(() =>
        //    {
        //        //Inicializace lokalní konfigurace
        //        //settings.xml

        //        Config.Settings.Initialize();
        //        tcs.SetResult(true);
        //    });

        //    return tcs.Task;
        //}

        private void LoadSettings()
        {
            Config.Settings.Initialize();
        }


        private void LoadSettingsPRD()
        {
            try
            {
                Config.Settings_DB.Initialize();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        private void LoadProductionPRD()
        {
            try
            {
                OdvadeniVyroby.Database.Production_DB.Initialize();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        private void LoadInternalStatePRD()
        {
            try
            {
                OdvadeniVyroby.Database.InternalState_DB.Initialize();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        //private Task LoadPrintSettings(TaskCompletionSource<bool> tcs)
        //{
        //    Task.Run(() =>
        //    {

        //        //Načtení a popřípade vytvoření PrinterFactory konfigurace
        //        var x = Fask.PrinterFactory.PrinterFactory.Instance;
        //        tcs.SetResult(true);
        //    });

        //    return tcs.Task;
        //}

        private void LoadPrintSettings()
        {
            Fask.PrinterFactory.PrinterFactory.CreateInstance();
        }


        #endregion

        #region Pomocne metody

        private Task<RestSharp.IRestResponse> API_Komunikace(TaskCompletionSource<RestSharp.IRestResponse> tcs)
        {
            Task.Run(() =>
            {
                string URLparam = "KonfigTerminal/TerminalID/" + Config.Settings.TerminalID.ToString();
                //Načtení konfigurace cez API z serveru
            
                RestSharp.IRestResponse response1;
                var state = API_Server.API_Komunikator.Communicate(API_Server.API_Komunikator.REST_Type.GET, out response1, URLparam);

                if (state)
                    tcs.SetResult(response1);
                else
                    tcs.SetException(new Exception("Tohle by nemnelo nikdy nastat... hmm... Aplikace bude ukončena!"));

            });

            return tcs.Task;
        }

        private Task<bool> CreateFile(TaskCompletionSource<bool> tcs, string JSON)
        {
            Task.Run(() =>
            {
                try
                {

                    if (File.Exists(Classes.DataInfo_Static.Konfigurace_JSON))
                        File.Delete(Classes.DataInfo_Static.Konfigurace_JSON);

                    Classes.JsonFormatter jsonFormatter = new Classes.JsonFormatter(JSON);

                    File.WriteAllText(Classes.DataInfo_Static.Konfigurace_JSON, jsonFormatter.Format());

                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    tcs.SetException(ex);
                }
                tcs.SetResult(true);

            });

            return tcs.Task;
        }

        #endregion

        //private Task<bool> NacteniAsync(AppCompatActivity parent, TaskCompletionSource<bool> tcs_Konfigurace)
        //{
        //    //var task = Task.Run(async() => {
        //    //    await NacteniKonfigurace(parent, tcs_Konfigurace);
        //    //});
        //    var result = NacteniKonfigurace(parent, tcs_Konfigurace);

        //    return tcs_Konfigurace.Task;
        //}


        //        private void NacteniKonfigurace(AppCompatActivity parent, TaskCompletionSource<bool> tcs_Konfigurace)
        //        private async Task<bool> NacteniKonfigurace(AppCompatActivity parent, TaskCompletionSource<bool> tcs_Konfigurace)

        private async Task<bool> NacteniKonfigurace(AppCompatActivity parent)
        {
            TaskCompletionSource<bool> tcs_Konfigurace = new TaskCompletionSource<bool>();

            try
            {
                SetText("Start API komunikace...");
                TaskCompletionSource<RestSharp.IRestResponse> tcs_API = new TaskCompletionSource<RestSharp.IRestResponse>();
                RestSharp.IRestResponse response = await API_Komunikace(tcs_API);
                SetText("Stop API komunikace...");


                string JSON = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SetText("Odpověď je OK...");
                    if (!string.IsNullOrEmpty(JSON))
                    {
                        Konfigurace konf = Newtonsoft.Json.JsonConvert.DeserializeObject<Konfigurace>(JSON);

                        if (konf != null)
                        {
                            try
                            {

                                TaskCompletionSource<bool> tcs_File = new TaskCompletionSource<bool>();
                                bool state = await CreateFile(tcs_File, JSON);

                                if (!state)
                                    throw new Exception("Nepodařilo se vytvořit konfiguraci!");

                            }
                            catch (System.Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(ex);
                                await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                                tcs_Konfigurace.SetException(ex);
                            }
                        }
                        else
                        {
                            string msg = string.Format("Nepodařilo se vytvořit konfiguraci!");
                            await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);
                            tcs_Konfigurace.SetException(new Exception(msg));
                        }
                    }
                    else
                    {
                        string msg = string.Format("Server nevrátil obsah konfigurace! Aplikace bude ukončena!");
                        await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    SetText("Odpověď je Created...");
                    string msg = "Na serveru nebyla nalezena konfigurace pro Váš terminál. POZOR, byla vytvořená výchozí!";
                    await MessageBoxAsync.Show(parent, msg, "Warning", MessageBoxButtons.OK);

                    if (!string.IsNullOrEmpty(JSON))
                    {
                        Konfigurace konf = Newtonsoft.Json.JsonConvert.DeserializeObject<Konfigurace>(JSON);

                        if (konf != null)
                        {
                            try
                            {

                                TaskCompletionSource<bool> tcs_File = new TaskCompletionSource<bool>();
                                bool state = await CreateFile(tcs_File, JSON);

                                if (!state)
                                    throw new Exception("Nepodařilo se vytvořit konfiguraci!");

                            }
                            catch (System.Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(ex);
                                await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                                tcs_Konfigurace.SetException(ex);
                            }

                        }
                        else
                        {
                            msg = string.Format("Nepodařilo se vytvořit konfiguraci!");
                            await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);
                            tcs_Konfigurace.SetException(new Exception(msg));
                        }
                    }
                    else
                    {
                        msg = string.Format("Server nevrátil obsah konfigurace! Aplikace bude ukončena!");
                        await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }
                }
                else
                {
                    string msg = string.Format("Server vrátil status: '{0}'. Aplikace bude ukončena.", response.StatusCode);
                    await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);

#if DEBUG

                    if (!File.Exists(Classes.DataInfo_Static.Konfigurace_JSON))
                    {

                        string msgE = string.Format("Pozor, nenalezen soubor konfigurace! Aplikace bude ukončena", response.StatusCode);
                        await MessageBoxAsync.Show(parent, msg, "Error", MessageBoxButtons.OK);

                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }

#else
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
                }

                tcs_Konfigurace.SetResult(true);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }

            return tcs_Konfigurace.Task.Result;
            //return tcs_Konfigurace.Task;

        }

        #region Download Vyroba.prd

        private void DownloadVyrobaPRD()
        {
            try
            {

                var so_b = Classes.DataInfo_Static.VyrobaGO_Instance.vyrobaServis.VyrobaDBDateTimePrepareZip(Config.Settings.TerminalID, DateTime.Now);
                if (so_b)
                {
                    Routines.DownloadDecompressDelete(this, Classes.DataInfo_Static.OdvadeniVyrobyDB + Classes.DataInfo_Static.PriponaTMP);
                    SynchronizacePrd();

                }
                else
                {
                    throw new Exception("Prepare data on server was not succesfull");
                }

            }
            catch (Exception ex)
            {
                
            }

        }

        private void SynchronizacePrd()
        {
            if (File.Exists(Classes.DataInfo_Static.ProductionDB))
            {
                var dt = Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.GetProductionUpdate();

                Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRDTMP.Connection_Open();
                foreach (Fask.SQLiteDBs.DataSets.Vyroba.Production_updateRow row in dt)
                {
                    Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRDTMP.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(row.QTYODVEDENO, row.CNTODVEDENO, row.LSTMod, row.CountEntries, row.SOPNUMBE, row.ORD, row.ITEMNMBR, row.ITEMTYPE, row.QTYPACK, row.BarcodeP);
                }
                Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRDTMP.Connection_Close();
            }

            SynchronizeDatabases();
        }

        private void SynchronizeDatabases()
        {

            File.Delete(Classes.DataInfo_Static.OdvadeniVyrobyDB);
            File.Copy(
                Classes.DataInfo_Static.OdvadeniVyrobyDBTMP,
               Classes.DataInfo_Static.OdvadeniVyrobyDB,
                true
                );
            File.Delete(Classes.DataInfo_Static.OdvadeniVyrobyDBTMP);

        }

        #endregion


    }

    

}