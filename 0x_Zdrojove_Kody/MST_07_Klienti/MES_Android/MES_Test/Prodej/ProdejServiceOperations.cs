using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using MES_Android.ProdejService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Prodej
{
    public partial class ProdejServiceOperations : IDisposable
    {
        public enum Operation
        {
            SendData
        }

        private Operation operation;
        private _WebReferences_Globals.ProdejServiceSession _pservice = null;
        public IAsyncResult aresult = null;
        private int davka = -1; //Davka

        //private string _description;
        //public string Description
        //{
        //    get { return _description; }
        //    set { _description = value; }
        //}

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userLogin;
        public string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; }
        }


        //private Classes_ProgresDialog.Dialog_Progress dialog;

        //private Android.Support.V4.App.FragmentTransaction _transaction { get; set; }
        private AppCompatActivity _parent { get; set; }

        //private ProdejServiceOperations(AppCompatActivity parent, Android.Support.V4.App.FragmentTransaction Transaction)
        //{
        //    _transaction = Transaction;
        //    _parent = parent;
        //}

        public ProdejServiceOperations()
        {
        }

        private ProdejServiceOperations(AppCompatActivity parent, _WebReferences_Globals.ProdejServiceSession pservice, Operation operation)
        {
            _parent = parent;
            //this._transaction = Transaction;
            this._pservice = pservice;
            this.operation = operation;

        }

        public Task<bool> SendData( AppCompatActivity parent, _WebReferences_Globals.ProdejServiceSession pservice, int davka, int userID, string UserLogin)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            try
            {
                using (ProdejServiceOperations pso = new ProdejServiceOperations(parent, pservice, Operation.SendData))
                {
                    ProgressDialog_Infinity.Message = "Odesílají se data dávky č." + davka.ToString();
                    pso.davka = davka;
                    pso.UserID = userID;
                    pso.UserLogin = UserLogin;
                    
                    pso.ProcessRequest(tcs);


                    return tcs.Task;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRequest(TaskCompletionSource<bool> tcs)
        {
            //string filename = string.Empty;

            try
            {
                switch (operation)
                {

                    case Operation.SendData:
                        try
                        {

                            #region Puvodne
                            //string url = Config.Settings.Adresa + "Upload.aspx";

                            //filename = Path.Combine(DataInfo_Static.PathDir, davka.ToString() + DataInfo_Static.PriponaDI);
                            //string Zip_File = filename + ".zip";

                            //MES_Android.CompressFile.CompressToZip(filename, Zip_File);

                            //string KamNaServer = Config.Settings.TerminalID.ToString() + "\\" + Path.GetFileName(filename) + ".zip";

                            //if (Uploading.SendFileCalcTime(url, Zip_File, KamNaServer, Uploading.Co.Prodej) != "OK")
                            //{
                            //    throw new Exception("davku se nepodarilo odeslat");
                            //}; 

                            #endregion

                            #region Test send file, neuspech...

                            //var filename = Path.Combine(DataInfo_Static.PathDir, davka.ToString() + DataInfo_Static.PriponaDI);
                            //string Zip_File = filename + ".zip";
                            //MES_Android.CompressFile.CompressToZip(filename, Zip_File);

                            //MES_Android.Classes.DataInfo_Static.API_GO_Instance.REST_POST_FILE(filename, "application/zip");

                            #endregion

                            //TODO: Predelat na stream

                            #region BASE64 test FUNGUJE

                            //IRestResponse restResponse;
                            //var filename = Path.Combine(DataInfo_Static.PathDir, davka.ToString() + DataInfo_Static.PriponaDI);
                            //string Zip_File = filename + ".zip";
                            //string KamNaServer = Config.Settings.TerminalID.ToString() + "\\" + Path.GetFileName(filename) + ".zip";


                            //MES_Android.CompressFile.CompressToZip(filename, Zip_File);
                            //string base64 = null;

                            //using (FileStream zip = new FileStream(Zip_File, FileMode.Open))
                            //{
                            //    var zipBytes = new byte[zip.Length];
                            //    zip.Read(zipBytes, 0, (int)zip.Length);
                            //    base64 = System.Convert.ToBase64String(zipBytes);
                            //}

                            //UploadFileObject fileObject = new UploadFileObject()
                            //{
                            //    PathFileOnServer = KamNaServer,
                            //    Base64File = base64
                            //};

                            //var JSON = JSON_Class.Serialize_JSON(fileObject);

                            //if (!Communicate(REST_Type.POST, out restResponse, "upload", JSON))
                            //{
                            //    throw new Exception("Odeslani souboru se nezdařilo");
                            //}

                            //if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                            //{
                            //    throw new Exception("Odeslani souboru se nezdařilo");
                            //}

                            #endregion


                            #region Stream

                            var filename = Path.Combine(DataInfo_Static.PathDir, davka.ToString() + DataInfo_Static.PriponaDI);
                            string Zip_File = filename + ".zip";
                            string KamNaServer = Config.Settings.TerminalID.ToString() + "\\" + Path.GetFileName(filename) + ".zip";

                            MES_Android.CompressFile.CompressToZip(filename, Zip_File);


                            MES_Android.Classes.DataInfo_Static.API_GO_Instance.REST_POST_SendFile(Zip_File, KamNaServer);

                            if (File.Exists(Zip_File))
                            {
                                File.Delete(Zip_File);
                            }

                            #endregion

                            var so = _pservice.ProcessProdejDB2(this.davka, Config.Settings.TerminalID, this._userID, _userLogin);
                            //_pservice.ProcessProdejDB2Completed += ProcessProdejDB2End;
                            ProcessProdejDB2Process(tcs, so);
                        }
                        catch (Exception ex)
                        {
                            tcs.SetException(ex);
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetException(ex);
            }
            finally
            {
                try
                {
                    if (_pservice != null)
                    {
                        var tmppservice = _pservice;
                        _pservice = null;
                        if (tmppservice != null)
                            tmppservice.Abort();
                    }
                }
                catch(Exception ex)
                {
                    tcs.SetException(ex);
                }
            }
        }


        private void ProcessProdejDB2Process(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            string filename = Path.Combine(DataInfo_Static.PathDir, davka.ToString() + DataInfo_Static.PriponaDI);

            try
            {
                if (_pservice == null) //jestlize je pservice null, pak doslo k abortu, a znamena to ze nema dal nic resit
                {
                    return;
                }

                if (done.StatusText == "OK" && !done.Exception)
                {
                    File.Delete(filename);
                    File.Delete(filename + ".zip");
                    tcs.SetResult(true);
                }
                else
                {
                    var Message = "Nastala chyba při odesílání dat: " + done.StatusText;
                    //dialog.Zprava = "Nastala chyba při odesílání dat: " + done.StatusText;
                    File.Delete(filename + ".zip");

                    //throw new Exception("Nepodařilo se odeslat data");
                    tcs.SetException(new Exception(Message));
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    File.Delete(filename + ".zip");
                }
                catch
                { }
            }
        }

        public void Dispose()
        {
            //Nekdy se možna hodí
        }
    }
}