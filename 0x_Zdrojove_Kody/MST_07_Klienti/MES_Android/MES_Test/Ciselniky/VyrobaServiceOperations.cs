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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Ciselniky
{
    /// <summary>
    /// Trida, ktera zabezpecuje komunikaci se servrem. Synchronizace souboru prd vyroby
    /// </summary>
    class VyrobaServiceOperations
    {

        #region Parametry

        public enum Operation
        {
            //TODO MaR tabulky vyroby - doplnit?!
            Unknown,
            OdvadeniVyroby,
            Production,
            InternalState
        }

        private LoginServiceSession _lservice = null;
        private Operation _operation;

        private Android.Support.V7.App.AppCompatActivity _parent { get; set; }

        #endregion

        #region Konstruktory

        public VyrobaServiceOperations()
        {

        }


        public VyrobaServiceOperations(Android.Support.V7.App.AppCompatActivity parent, Operation operation)
        {
            this._operation = operation;
            this._parent = parent;
        }

        #endregion

        #region Entering methods

        public Task<bool> KatalogVyroba(
            TaskCompletionSource<bool> tcs,
            Android.Support.V7.App.AppCompatActivity parent,
            Operation operation,
            string Message)
        {
            try
            {
                VyrobaServiceOperations cso = null;


                cso = new VyrobaServiceOperations(parent, operation);


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

        public Task<bool> KatalogProduction(
    TaskCompletionSource<bool> tcs,
    Android.Support.V7.App.AppCompatActivity parent,
    Operation operation,
    string Message)
        {
            try
            {
                VyrobaServiceOperations cso = null;


                cso = new VyrobaServiceOperations(parent, operation);


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

        public Task<bool> KatalogInternalState(
TaskCompletionSource<bool> tcs,
Android.Support.V7.App.AppCompatActivity parent,
Operation operation,
string Message)
        {
            try
            {
                VyrobaServiceOperations cso = null;


                cso = new VyrobaServiceOperations(parent, operation);


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
        #endregion

        private void ProcessRequest(TaskCompletionSource<bool> tcs)
        {
            bool so_b = false;

            try
            {
                switch (_operation)
                {
                    case Operation.OdvadeniVyroby:
                        so_b = Classes.DataInfo_Static.VyrobaGO_Instance.vyrobaServis.VyrobaDBDateTimePrepareZip(Config.Settings.TerminalID, DateTime.Now);
                        if(so_b)
                        {
                            ProcessKatalogVyrobaEnd(tcs);
                        }
                        else
                        {
                            tcs.SetException(new Exception("Prepare data on server was not succesfull"));
                        }
                        break;
                    case Operation.Production:
                        so_b = Classes.DataInfo_Static.VyrobaGO_Instance.vyrobaServis.VyrobaDBProductionPrepareZip(Config.Settings.TerminalID, DateTime.Now);
                        if (so_b)
                        {
                            ProcessKatalogProductionEnd(tcs);
                        }
                        else
                        {
                            tcs.SetException(new Exception("Prepare data on server was not succesfull"));
                        }
                        break;
                    case Operation.InternalState:
                        so_b = Classes.DataInfo_Static.VyrobaGO_Instance.vyrobaServis.VyrobaDBInternalStatePrepareZip(Config.Settings.TerminalID, DateTime.Now);
                        if (so_b)
                        {
                            ProcessKatalogInternalStateEnd(tcs);
                        }
                        else
                        {
                            tcs.SetException(new Exception("Prepare data on server was not succesfull"));
                        }
                        break;
                    case Operation.Unknown:
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

        #region Stazeni



        #region Vyroba

        private void ProcessKatalogVyrobaEnd(TaskCompletionSource<bool> tcs)
        {

            try
            {

                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.OdvadeniVyrobyDB + Classes.DataInfo_Static.PriponaTMP);

                SynchronizacePrd();


                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.menu_OV_DB_download, Classes.DataInfo_Static.OdvadeniVyrobyDB);
                }

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
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


        private void ProcessKatalogProductionEnd(TaskCompletionSource<bool> tcs)
        {

            try
            {

                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.ProductionDBTMP);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.menu_OV_DB_upload, Classes.DataInfo_Static.ProductionDB);  //co sem dat?
                }

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        private void ProcessKatalogInternalStateEnd(TaskCompletionSource<bool> tcs)
        {

            try
            {

                Routines.DownloadDecompressDelete(Classes.DataInfo_Static.InternalStateDB + Classes.DataInfo_Static.PriponaTMP);

                if (this._parent is ICiselniky_Datum)
                {
                    ((ICiselniky_Datum)this._parent).SetDateToMenu(Resource.String.menu_OV_DB_upload, Classes.DataInfo_Static.InternalStateDB);  //co sem dat?
                }

                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        #endregion

        #endregion


        #endregion
    }
}