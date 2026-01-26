using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.ServerAccess;

namespace Fask.Aktualizace_API.Data
{
    public class DatabaseActions
    {
        public static DateTime? UserLastAction(string UserID)
        {
            DateTime? lastUserActionDateTime = null;
            DateTime? lastUserActionDateTimeLocal = null;
            DateTime? lastUserActionDateTimeServer = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //22.11.2017 JiS - labara autonomni rezim ... InternalState.Production obsahuje vzdy posledni zaznam z tohoto stroje ...
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf);
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD);

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserID_Production(UserID);
                if (dt_lst_LProduction.Count > 0)
                    lastUserActionDateTimeLocal = dt_lst_LProduction[0].dateeve;

                // Zjisteni posledni akce uzivatele...

                try
                {
                    lastUserActionDateTimeServer = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.UserLastAction(UserID);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                }

                if (lastUserActionDateTimeLocal.HasValue)
                    lastUserActionDateTime = lastUserActionDateTimeLocal;

                if (lastUserActionDateTimeServer.HasValue)
                {
                    if (!lastUserActionDateTime.HasValue)
                        lastUserActionDateTime = lastUserActionDateTimeServer;
                    else if (lastUserActionDateTime.Value < lastUserActionDateTimeServer.Value)
                        lastUserActionDateTime = lastUserActionDateTimeServer;
                }

                return lastUserActionDateTime;

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // TODO : prepsat zpusob ukladani, nejak predavat cesty... ted je duplikovany kod ...

        //public static bool InsertProductionTmp(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt)
        //{
        //    try
        //    {
        //        if (dt == null)
        //            return false;

        //        foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow i in dt)
        //        {
        //            InsertProductionQuery(i, Constants.Production + Constants.PRD + Constants.TMP);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    } 
        //}

        public static bool insertProductionDataTable(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt)
        {
            try
            {
                if (dt == null)
                    return false;

                if (dt.Count == 0)
                    return false;
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprocache = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //taprocache.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD));
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.DeleteProductionByUserID_Production(dt.First().UserID);

                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(dt);
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.Update_Production(dt);

                //foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow item in dt)
                //{
                //    if (item == null)
                //        continue;

                //    if (item.RowState != System.Data.DataRowState.Added)
                //        continue;

                //    InsertProductionQuery(item, Constants.Production + Constants.PRD);
                //    InsertProductionQuery(item, Constants.InternalState + Constants.PRD);


                //}
                return true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool InsertProduction(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow i)
        {
            try
            {
                if (i == null)
                    return false;

                if (i.RowState != System.Data.DataRowState.Added)
                    return false;

                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(i);
                //InsertProductionQuery(i, Constants.Production + Constants.PRD);
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprocache = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //taprocache.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD));
                // je korekce
                if (!i.IsTIMECRIDNull())
                {
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.DeleteCorrectionByUserID_Production(i.UserID);
                    
                    Fask.Aktualizace_API.Data.DatabaseActions.InsertProductionCache(i);
                }
                else
                {
                    // je odvod
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.DeleteProductionByUserID_Production(i.UserID);
                    Fask.Aktualizace_API.Data.DatabaseActions.InsertProductionCache(i);
                }
                //Fask.Vyroba_P.Extensions.ProductionStateResolver.clearChache(i);

                // prozatim se resi starym  zpusobem
                //if (Settings.ModulPovolitPrehledOdvodu)
                //{
                //    InsertProductionQuery(i, Constants.ProductionSdfHist);
                //}
                return true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool InsertProductionHistory(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow i)
        {
            try
            {
                if (i == null)
                    return false;
                if (i.RowState != System.Data.DataRowState.Added)
                    return false;

                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__ProductionHist_PRD.Update_Production(i);
                //InsertProductionQuery(i, Constants.ProductionHist + Constants.PRD);

                return true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }            
        }

        /// <summary>
        /// Ulozeni do cache.
        /// </summary>
        /// <param name="i"></param>
        /// <returns>True - vlozeni probehlo, jinak False</returns>
        public static bool InsertProductionCache(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow i)
        {
            try
            {
                if (i == null)
                    return false;
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.Update_Production(i);
                //InsertProductionQuery(i, Constants.InternalState + Constants.PRD);

                return true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("DatabaseActions", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        //private static void InsertProductionQuery(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow i , string dbname)
        //{
        //    System.Data.SqlServerCe.SqlCeCommand scecommand = new System.Data.SqlServerCe.SqlCeCommand(
        //        "INSERT INTO Production " +
        //        "(CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMMJ, ORD, TIMEPREP, TIMEUNIT, TIMESTART, TIMESTOP, TIMECOR"+
        //        ",TIMECRID, TIMECRIDTYPE, loginid, machineid, dateeve, qty, qtyReal, QTYPACK, QTYPACKMJ" +
        //        ",description, BarcodeP, UserID, TermID, ISOK, GUID, TIMEMODE, TIMEPREPSTART, TIMEPREPSTOP, TIMECORSTART"+
        //        ",TIMECORSTOP, operationid, SOUBEHGUID, CORRGUID, SKL_ID, LOCNCODE) " +
        //        "VALUES "+
        //        "(@CountEntries,@SOPNUMBE,@ITEMNMBR,@ITEMTYPE,@ITEMMJ,@ORD,@TIMEPREP,@TIMEUNIT,@TIMESTART,@TIMESTOP,@TIMECOR"+
        //        ",@TIMECRID,@TIMECRIDTYPE,@loginid,@machineid,@dateeve,@qty,@qtyReal,@QTYPACK,@QTYPACKMJ"+
        //        ",@description,@BarcodeP,@UserID,@TermID,@ISOK,@GUID,@TIMEMODE,@TIMEPREPSTART,@TIMEPREPSTOP,@TIMECORSTART"+
        //        ",@TIMECORSTOP,@operationid,@SOUBEHGUID,@CORRGUID,@SKL_ID,@LOCNCODE)"
        //        );
        //    System.Data.SqlServerCe.SqlCeConnection sceconnection = new System.Data.SqlServerCe.SqlCeConnection(
        //        "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, dbname)
        //        ); 

        //    try
        //    {
        //        scecommand.Connection = sceconnection;

        //        if (!i.IsCountEntriesNull())
        //            scecommand.Parameters.AddWithValue("@CountEntries", i.CountEntries);
        //        else
        //            scecommand.Parameters.AddWithValue("@CountEntries", System.DBNull.Value);

        //        if (!i.IsSOPNUMBENull())
        //            scecommand.Parameters.AddWithValue("@SOPNUMBE", i.SOPNUMBE);
        //        else
        //            scecommand.Parameters.AddWithValue("@SOPNUMBE", System.DBNull.Value);

        //        if (!i.IsITEMNMBRNull())
        //            scecommand.Parameters.AddWithValue("@ITEMNMBR", i.ITEMNMBR);
        //        else
        //            scecommand.Parameters.AddWithValue("@ITEMNMBR", System.DBNull.Value);

        //        if (!i.IsITEMTYPENull())
        //            scecommand.Parameters.AddWithValue("@ITEMTYPE", i.ITEMTYPE);
        //        else
        //            scecommand.Parameters.AddWithValue("@ITEMTYPE", System.DBNull.Value);

        //        if (!i.IsITEMMJNull())
        //            scecommand.Parameters.AddWithValue("@ITEMMJ", i.ITEMMJ);
        //        else
        //            scecommand.Parameters.AddWithValue("@ITEMMJ", System.DBNull.Value);

        //        if (!i.IsORDNull())
        //            scecommand.Parameters.AddWithValue("@ORD", i.ORD);
        //        else
        //            scecommand.Parameters.AddWithValue("@ORD", System.DBNull.Value);

        //        if (!i.IsTIMEPREPNull())
        //            scecommand.Parameters.AddWithValue("@TIMEPREP", i.TIMEPREP);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMEPREP", System.DBNull.Value);

        //        if (!i.IsTIMEUNITNull())
        //            scecommand.Parameters.AddWithValue("@TIMEUNIT", i.TIMEUNIT);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMEUNIT", System.DBNull.Value);

        //        if (!i.IsTIMESTARTNull())
        //            scecommand.Parameters.AddWithValue("@TIMESTART", i.TIMESTART);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMESTART", System.DBNull.Value);

        //        if (!i.IsTIMESTOPNull())
        //            scecommand.Parameters.AddWithValue("@TIMESTOP", i.TIMESTOP);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMESTOP", System.DBNull.Value);

        //        if (!i.IsTIMECORNull())
        //            scecommand.Parameters.AddWithValue("@TIMECOR", i.TIMECOR);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMECOR", System.DBNull.Value);

        //        if (!i.IsTIMECRIDNull())
        //            scecommand.Parameters.AddWithValue("@TIMECRID", i.TIMECRID);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMECRID", System.DBNull.Value);

        //        if (!i.IsTIMECRIDTYPENull())
        //            scecommand.Parameters.AddWithValue("@TIMECRIDTYPE", i.TIMECRIDTYPE);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMECRIDTYPE", System.DBNull.Value);

        //        scecommand.Parameters.AddWithValue("@loginid", i.loginid);

        //        if (!i.IsmachineidNull())
        //            scecommand.Parameters.AddWithValue("@machineid", i.machineid);
        //        else
        //            scecommand.Parameters.AddWithValue("@machineid", System.DBNull.Value);

        //        scecommand.Parameters.AddWithValue("@dateeve", i.dateeve);
        //        scecommand.Parameters.AddWithValue("@qty", i.qty);
        //        scecommand.Parameters.AddWithValue("@qtyReal", i.qtyReal);

        //        if (!i.IsQTYPACKNull())
        //            scecommand.Parameters.AddWithValue("@QTYPACK", i.QTYPACK);
        //        else
        //            scecommand.Parameters.AddWithValue("@QTYPACK", System.DBNull.Value);

        //        if (!i.IsQTYPACKMJNull())
        //            scecommand.Parameters.AddWithValue("@QTYPACKMJ", i.QTYPACKMJ);
        //        else
        //            scecommand.Parameters.AddWithValue("@QTYPACKMJ", System.DBNull.Value);

        //        if (!i.IsdescriptionNull())
        //            scecommand.Parameters.AddWithValue("@description", i.description);
        //        else
        //            scecommand.Parameters.AddWithValue("@description", System.DBNull.Value);

        //        if (!i.IsBarcodePNull())
        //            scecommand.Parameters.AddWithValue("@BarcodeP", i.BarcodeP);
        //        else
        //            scecommand.Parameters.AddWithValue("@BarcodeP", System.DBNull.Value);

        //        scecommand.Parameters.AddWithValue("@UserID", i.UserID);
        //        scecommand.Parameters.AddWithValue("@TermID", i.TermID);

        //        if (!i.IsISOKNull())
        //            scecommand.Parameters.AddWithValue("@ISOK", i.ISOK);
        //        else
        //            scecommand.Parameters.AddWithValue("@ISOK", System.DBNull.Value);

        //        scecommand.Parameters.AddWithValue("@GUID", i.GUID);

        //        if (!i.IsTIMEMODENull())
        //            scecommand.Parameters.AddWithValue("@TIMEMODE", i.TIMEMODE);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMEMODE", System.DBNull.Value);

        //        if (!i.IsTIMEPREPSTARTNull())
        //            scecommand.Parameters.AddWithValue("@TIMEPREPSTART", i.TIMEPREPSTART);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMEPREPSTART", System.DBNull.Value);

        //        if (!i.IsTIMEPREPSTOPNull())
        //            scecommand.Parameters.AddWithValue("@TIMEPREPSTOP", i.TIMEPREPSTOP);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMEPREPSTOP", System.DBNull.Value);

        //        if (!i.IsTIMECORSTARTNull())
        //            scecommand.Parameters.AddWithValue("@TIMECORSTART", i.TIMECORSTART);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMECORSTART", System.DBNull.Value);

        //        if (!i.IsTIMECORSTOPNull())
        //            scecommand.Parameters.AddWithValue("@TIMECORSTOP", i.TIMECORSTOP);
        //        else
        //            scecommand.Parameters.AddWithValue("@TIMECORSTOP", System.DBNull.Value);

        //        if (!i.IsoperationidNull())
        //            scecommand.Parameters.AddWithValue("@operationid", i.operationid);
        //        else
        //            scecommand.Parameters.AddWithValue("@operationid", System.DBNull.Value);

        //        if (!i.IsSOUBEHGUIDNull())
        //            scecommand.Parameters.AddWithValue("@SOUBEHGUID", i.SOUBEHGUID);
        //        else
        //            scecommand.Parameters.AddWithValue("@SOUBEHGUID", System.DBNull.Value);

        //        if (!i.IsCORRGUIDNull())
        //            scecommand.Parameters.AddWithValue("@CORRGUID", i.CORRGUID);
        //        else
        //            scecommand.Parameters.AddWithValue("@CORRGUID", System.DBNull.Value);

        //        if (!i.IsSKL_IDNull())
        //            scecommand.Parameters.AddWithValue("@SKL_ID", i.SKL_ID);
        //        else
        //            scecommand.Parameters.AddWithValue("@SKL_ID", System.DBNull.Value);

        //        if (!i.IsLOCNCODENull())
        //            scecommand.Parameters.AddWithValue("@LOCNCODE", i.LOCNCODE);
        //        else
        //            scecommand.Parameters.AddWithValue("@LOCNCODE", System.DBNull.Value);

        //        scecommand.Connection.Open();
        //        int n = scecommand.ExecuteNonQuery();
        //        scecommand.Connection.Close();
        //        scecommand.Dispose();

        //        i.AcceptChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
