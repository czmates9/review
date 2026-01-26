using Fask.Vyroba_P.MySystem;
using Fask.Vyroba_P.ServerAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Vyroba_P
{
    public class GlobalObject : IDisposable
    {
        //Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject

        // Plná cesta    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.Ta_P
        // Plná cesta    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_

        //Controllery
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_InternalState controller_InternalState;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Production_PRD;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Production_PRDTMP;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__ProductionHist_PRD;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Vyroba_PRD;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__Vyroba_PRDTMP;
        public Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba controller_Vyroba__InternalState_PRD;


        //Webobe sluzby
        public _WebRefernces_Globals.UkolovaniServiceSession ukolovaniService;
        public _WebRefernces_Globals.WebServiceVyrobaSession vyrobaServis;
        public _WebRefernces_Globals.WebServiceTiskSession tiskServis;
        public WebServiceVyroba.CustomVyroba CustomvyrobaServis;

        public GlobalObject()
        {

            controller_InternalState = new Fask.SQLiteDBs.Controllers.SQLite_Controller_InternalState(Path.Combine(MyPath.DataDirectory, Constants.InternalState + Constants.PRD));
            controller_Vyroba__Production_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD));
            controller_Vyroba__Production_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP));
            controller_Vyroba__ProductionHist_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD));
            controller_Vyroba__Vyroba_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD));
            controller_Vyroba__Vyroba_PRDTMP = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD + Constants.TMP));
            controller_Vyroba__InternalState_PRD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD));
            
            try
            {
                ukolovaniService = new _WebRefernces_Globals.UkolovaniServiceSession();
                ukolovaniService.Url = Settings.WebServiceAddressVyroba + Constants.Ukolovani_asmx;
                ukolovaniService.Timeout = Settings.TimeOut;
                ukolovaniService.UpdateWebServiceCredentials();

                vyrobaServis = new _WebRefernces_Globals.WebServiceVyrobaSession();
                vyrobaServis.Url = Settings.WebServiceAddressVyroba + Constants.Vyroba_asmx;
                vyrobaServis.Timeout = Settings.ProductionOnlineTimeout;
                vyrobaServis.UpdateWebServiceCredentials();

                tiskServis = new _WebRefernces_Globals.WebServiceTiskSession();
                tiskServis.Url = Settings.WebServiceAddressVyroba + Constants.Tisk_asmx;
                tiskServis.Timeout = Settings.ProductionOnlineTimeout;
                tiskServis.UpdateWebServiceCredentials();

                CustomvyrobaServis = new WebServiceVyroba.CustomVyroba();
                CustomvyrobaServis.Url = Settings.WebServiceAddressVyroba + Constants.Vyroba_asmx;
                CustomvyrobaServis.Timeout = Settings.ProductionOnlineTimeout;
                CustomvyrobaServis.UpdateWebServiceCredentials();

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
            }

        }


        #region IDisposable Members

        public void Dispose()
        {
            if (controller_InternalState != null) controller_InternalState.Dispose();

            if (controller_Vyroba__Production_PRD != null) controller_Vyroba__Production_PRD.Dispose();
            if (controller_Vyroba__Production_PRDTMP != null) controller_Vyroba__Production_PRDTMP.Dispose();
            if (controller_Vyroba__ProductionHist_PRD != null) controller_Vyroba__ProductionHist_PRD.Dispose();
            if (controller_Vyroba__Vyroba_PRD != null) controller_Vyroba__Vyroba_PRD.Dispose();
            if (controller_Vyroba__Vyroba_PRDTMP != null) controller_Vyroba__Vyroba_PRDTMP.Dispose();
            if (controller_Vyroba__InternalState_PRD != null) controller_Vyroba__InternalState_PRD.Dispose();
            

            controller_InternalState = null;
            controller_Vyroba__Production_PRD = null;
            controller_Vyroba__Production_PRDTMP = null;
            controller_Vyroba__ProductionHist_PRD = null;
            controller_Vyroba__Vyroba_PRD = null;
            controller_Vyroba__Vyroba_PRDTMP = null;
            controller_Vyroba__InternalState_PRD = null;

            if (ukolovaniService != null) ukolovaniService.Dispose();
            if (vyrobaServis != null) vyrobaServis.Dispose();
            if (CustomvyrobaServis != null) CustomvyrobaServis.Dispose();

            ukolovaniService = null;
            vyrobaServis = null;
            CustomvyrobaServis = null;
        }

        #endregion


        public void DeleteProductionByGuid()
        {
            try
            {
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP)))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_Production())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_Production(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void DeleteUserEventsByGuid()
        {
            try
            {
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP)))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_UserEvents())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_UserEvents(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        /// <summary>
        /// Deletes data from sqlite production.prd database based on guid of row that was processed 
        /// <param name="produtionprdtmp_processed">full path to filename that was processed/sended</param>
        /// </summary>
        public void DeleteProductionByGuid(string produtionprdtmp_processed)
        {
            try
            {
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_Production())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_Production(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Deletes user events rows from produtction database, by processed/sended data
        /// </summary>
        /// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
		public void DeleteUserEventsByGuid(string produtionprdtmp_processed)
        {
            try
            {
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_UserEvents())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_UserEvents(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Deletes production_sources rows from produtction database, by processed/sended data
        /// </summary>
        /// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
        public void DeleteProduction_SourcesByGuid(string produtionprdtmp_processed)
        {
            try
            {
                // TODO : revidovat
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_Production_Sources())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_Production_Sources(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        /// <summary>
        /// Deletes production_SN rows from produtction database, by processed/sended data
        /// </summary>
        /// <param name="produtionprdtmp_processed">full filename to processed/sended file</param>
        public void DeleteProduction_SNByGuid(string produtionprdtmp_processed)
        {
            try
            {
                // TODO : revidovat
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyr = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
                using (SQLiteDBs.Controllers.SQLite_Controller_Vyroba ConVyrTMP = new SQLiteDBs.Controllers.SQLite_Controller_Vyroba(produtionprdtmp_processed))
                {
                    ConVyr.Connection_Open();
                    ConVyrTMP.Connection_Open();

                    try
                    {
                        using (var preader = ConVyrTMP.GetReaderGUID_Production_SN())
                        {

                            while (preader.Read())
                            {
                                object GUIDobject = preader[0];

                                if (GUIDobject != null && GUIDobject is Guid)
                                {
                                    Guid guid = (Guid)GUIDobject;
                                    ConVyr.DeleteByGUID_Production_SN(guid);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConVyr.Connection_Close();
                        ConVyrTMP.Connection_Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


    }
}
