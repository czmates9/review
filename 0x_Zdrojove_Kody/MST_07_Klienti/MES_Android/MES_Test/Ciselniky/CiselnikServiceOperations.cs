using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android._WebReferences_Globals;
using MES_Android.CiselnikService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Ciselniky
{
    /// <summary>
    /// Trida, ktera zabezpecuje komunikaci se servrem
    /// </summary>
    public class CiselnikServiceOperations 
    {

        #region Parametry

        public enum Operation
        {
            All,
            Zbozi,
            Odberatele,
            Strediska,
            TypDokladu,
            Sklady,
            Pracovnici,
            Lokace,
            Uzivatele,
            Meny,
            OdvadeniVyroby,
            Production,
            InternalState,
            ExportZbozi,
            ExportOdberatele,
            ExportSklady,
            ExportMeny,
            ExportStrediska,
            ExportPracovnici
        }

        private LoginServiceSession _lservice = null;
        private string _skladprefix = string.Empty;
        private Operation _operation;

        private Android.Support.V7.App.AppCompatActivity _parent { get; set; }

        List<string> _prava;

        #endregion

        #region Konstruktory

        public CiselnikServiceOperations()
        {
           
        }

        public CiselnikServiceOperations(Android.Support.V7.App.AppCompatActivity parent, Operation operation, string skladprefix) 
        {
            this._operation = operation;
            this._skladprefix = skladprefix;
            this._parent = parent;

        }

        public CiselnikServiceOperations(Android.Support.V7.App.AppCompatActivity parent, Operation operation)
        {
            this._operation = operation;
            this._skladprefix = string.Empty;
            this._parent = parent;
        }

        public CiselnikServiceOperations(
            Android.Support.V7.App.AppCompatActivity parent, 
            LoginServiceSession loginservice, 
            Operation operation,
             List<string> prava) 
        {
            this._lservice = loginservice;
            this._operation = operation;
            this._skladprefix = string.Empty;
            this._parent = parent;
            this._prava = prava;
        }

        #endregion

        #region Entering methods

        public Task<bool> KatalogCiselnik(
            TaskCompletionSource<bool> tcs,
            Android.Support.V7.App.AppCompatActivity parent,
            Operation operation,
            string Message,
            string skladprefix = null)
        {
            try
            {
                CiselnikServiceOperations cso = null;

                if (string.IsNullOrEmpty(skladprefix))
                    cso = new CiselnikServiceOperations(parent, operation);
                else
                    cso = new CiselnikServiceOperations(parent, operation, skladprefix);



                Classes.ProgressDialog_Infinity.Message = Message;

                Task.Run(() => { cso.ProcessRequest(tcs); });

                return tcs.Task;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        //public static Task<bool> KatalogPracovnici(Android.Support.V7.App.AppCompatActivity parent, Android.Support.V4.App.FragmentTransaction Transaction, CiselnikServiceSession cservice, string skladprefix)
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    using (CiselnikServiceOperations cso = new CiselnikServiceOperations(parent, Operation.Pracovnici, skladprefix))
        //    {
        //        cso.Description = "Stahuje se číselník pracovníků";
        //        if (cso.ShowDialog() == "Cancel")
        //            tcs.TrySetResult(false);
        //        else
        //            tcs.TrySetResult(true);
        //    }
        //    return tcs.Task;
        //}

        //public static Task<bool> KatalogMen(Android.Support.V7.App.AppCompatActivity parent, Android.Support.V4.App.FragmentTransaction Transaction, CiselnikServiceSession cservice)
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    using (CiselnikServiceOperations cso = new CiselnikServiceOperations(parent, Operation.Meny))
        //    {
        //        cso.Description = "Stahuje se číselník měn";
        //        if (cso.ShowDialog() == "Cancel")
        //            tcs.TrySetResult(false);
        //        else
        //            tcs.TrySetResult(true);
        //    }
        //    return tcs.Task;
        //}

        //public static Task<bool> KatalogLokace(Android.Support.V7.App.AppCompatActivity parent, Android.Support.V4.App.FragmentTransaction Transaction, CiselnikServiceSession cservice, string skladprefix)
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    using (CiselnikServiceOperations cso = new CiselnikServiceOperations(parent, Operation.Lokace, skladprefix))
        //    {
        //        cso.Description = "Stahuje se číselník lokací";
        //        if (cso.ShowDialog() == "Cancel")
        //            tcs.TrySetResult(false);
        //        else
        //            tcs.TrySetResult(true);
        //    }
        //    return tcs.Task;
        //}

        public static Task<bool> KatalogUzivatelu(
            TaskCompletionSource<bool> tcs,
            Android.Support.V7.App.AppCompatActivity parent, 
            LoginServiceSession lservice,
            List<string> prava
            )
        {
            try
            {

                CiselnikServiceOperations cso = new CiselnikServiceOperations(parent, lservice, Operation.Uzivatele, prava);
                Classes.ProgressDialog_Infinity.Message = "Stahuje se číselník uživatelů";
                Task.Run(() => { cso.ProcessRequest(tcs); });

                return tcs.Task;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }


        #endregion

        private void ProcessRequest(TaskCompletionSource<bool> tcs)
        {
            StatusObject so = null;

            try
            {
                switch (_operation)
                {
                    case Operation.Zbozi:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogZboziDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogZboziEnd(tcs, so);
                        break;
                    case Operation.Odberatele:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogOdberateleDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogOdberateleEnd(tcs, so);
                        break;
                    case Operation.Strediska:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogStrediskaDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogStrediskaEnd(tcs, so);
                        break;
                    case Operation.TypDokladu:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogTypDokladuDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogTypDokladuEnd(tcs, so);
                        break;
                    case Operation.Sklady:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogSkladyDBPrepare(Config.Settings.TerminalID);
                        ProcessKatalogSkladuEnd(tcs, so);
                        break;
                    case Operation.Pracovnici:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogPracovniciDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogPracovniciEnd(tcs, so);
                        break;
                    case Operation.Lokace:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogLokaceDBPrepare(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogLokaceEnd(tcs, so);
                        break;
                    case Operation.Meny:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogMenyDBPrepare(Config.Settings.TerminalID);
                        ProcessKatalogMenyEnd(tcs, so);
                        break;
                    case Operation.ExportZbozi:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogZboziExport(Config.Settings.TerminalID, _skladprefix);
                        ProcessKatalogZboziExportEnd(tcs, so);
                        break;
                    case Operation.ExportOdberatele:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogOdberateleExport(Config.Settings.TerminalID);
                        ProcessKatalogOdberateleExportEnd(tcs, so);
                        break;
                    case Operation.ExportSklady:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogSkladyExport(Config.Settings.TerminalID);
                        ProcessKatalogSkladyExportEnd(tcs, so);
                        break;
                    case Operation.ExportPracovnici:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogPracovniciExport(Config.Settings.TerminalID);
                        ProcessKatalogPracovniciExportEnd(tcs, so);
                        break;
                    case Operation.ExportMeny:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogMenyExport(Config.Settings.TerminalID);
                        ProcessKatalogMenyExportEnd(tcs, so);
                        break;
                    case Operation.ExportStrediska:
                        so = Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik.KatalogStrediskaExport(Config.Settings.TerminalID);
                        ProcessKatalogStrediskaExportEnd(tcs, so);
                        break;
                    case Operation.Uzivatele:
                        var soU = _lservice.GetKatalogUzivateleSPravama(Config.Settings.TerminalID, this._prava.ToArray());
                        ProcessKatalogUzivateleEnd(tcs, soU);
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

        }

        #region END metody

        #region Exporty

        private void ProcessKatalogStrediskaExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ProcessKatalogMenyExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ProcessKatalogPracovniciExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ProcessKatalogSkladyExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ProcessKatalogOdberateleExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ProcessKatalogZboziExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            ExportEnd(tcs, done);
        }

        private void ExportEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Stazeni

        #region Meny

        private void ProcessKatalogMenyEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {

                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikMenyDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Meny, Classes.DataInfo_Static.CiselnikMenyDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Meny, Resource.String.Meny, Classes.DataInfo_Static.CiselnikMenyDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Meny_Pro, Resource.String.Meny, Classes.DataInfo_Static.CiselnikMenyDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Lokace
        
        private void ProcessKatalogLokaceEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikLokaceDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Lokace, Classes.DataInfo_Static.CiselnikLokaceDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Lokace, Resource.String.Lokace, Classes.DataInfo_Static.CiselnikLokaceDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Lokace_Pro, Resource.String.Lokace, Classes.DataInfo_Static.CiselnikLokaceDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Pracovnici

        private void ProcessKatalogPracovniciEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikPracovniciDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Pracovnici, Classes.DataInfo_Static.CiselnikPracovniciDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Pracovnici, Resource.String.Pracovnici, Classes.DataInfo_Static.CiselnikPracovniciDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Pracovnici_Pro, Resource.String.Pracovnici, Classes.DataInfo_Static.CiselnikPracovniciDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Sklady

        private void ProcessKatalogSkladuEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikSkladyDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Sklady, Classes.DataInfo_Static.CiselnikSkladyDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Sklady, Resource.String.SKlady, Classes.DataInfo_Static.CiselnikSkladyDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Sklady_Pro, Resource.String.SKlady, Classes.DataInfo_Static.CiselnikSkladyDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion 

        #region Typy Dokladu

        private void ProcessKatalogTypDokladuEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikTypDokladuDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.TypyDokladu, Classes.DataInfo_Static.CiselnikTypDokladuDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_TypyDokladu, Resource.String.TypyDokladu, Classes.DataInfo_Static.CiselnikTypDokladuDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_TypyDokladu_Pro, Resource.String.TypyDokladu, Classes.DataInfo_Static.CiselnikTypDokladuDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion 

        #region Strediska

        private void ProcessKatalogStrediskaEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikStrediskaDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Strediska, Classes.DataInfo_Static.CiselnikStrediskaDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Strediska, Resource.String.Strediska, Classes.DataInfo_Static.CiselnikStrediskaDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Strediska_Pro, Resource.String.Strediska, Classes.DataInfo_Static.CiselnikStrediskaDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Odberatele


        private void ProcessKatalogOdberateleEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {
            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                
                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikOdberateleDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Odberatele, Classes.DataInfo_Static.CiselnikOdberateleDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Odberatele, Resource.String.Odberatele, Classes.DataInfo_Static.CiselnikOdberateleDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Odberatele_Pro, Resource.String.Odberatele, Classes.DataInfo_Static.CiselnikOdberateleDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #region Zbozi

        private void ProcessKatalogZboziEnd(TaskCompletionSource<bool> tcs, StatusObject done)
        {

            try
            {
                if (done.Exception)
                    throw new Exception(done.StatusText);
                if (!done.Finished)
                    throw new Exception(done.StatusText);

                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikZboziDB);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.Zasoby, Classes.DataInfo_Static.CiselnikZboziDB);
                }

                //if (this._parent is Activity_HlavneMenu)
                //{
                //    ((Activity_HlavneMenu)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Zasoby, Resource.String.Zasoby, Classes.DataInfo_Static.CiselnikZboziDB);
                //}
                //else if (this._parent is Prodej.Prodej_Davky)
                //{
                //    ((Prodej.Prodej_Davky)this._parent).SetDateToMenu(Resource.Id.nav_Ciselnik_Zasoby_Pro, Resource.String.Zasoby, Classes.DataInfo_Static.CiselnikZboziDB);
                //}

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #endregion

        private void ProcessKatalogUzivateleEnd(TaskCompletionSource<bool> tcs, LoginService.StatusObject so)
        {

            try
            {
                if (_lservice == null) //Doslo k abrotu, tak ven
                {
                    return;
                }

                if (so.Exception)
                    throw new Exception(so.StatusText);
                if (!so.Finished)
                    throw new Exception(so.StatusText);

                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikUzivateleDB);

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                tcs.SetException(ex);
            }
        }

        #endregion


    }
}