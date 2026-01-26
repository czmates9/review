using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.ModulePohodaXML.Database
{
    class Prijem
    {
        //public static Pohoda_DataSets.Prijem.ProductionDataTable GETDATA_Production(int countentries, string SKLID)
        //{
        //    Globals_V1.LoadConfiguration();
        //    Pohoda_DataSets.Prijem.ProductionDataTable tbl_production = null;
        //    try
        //    {

        //        Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter ta = new Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter();
        //        ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        tbl_production = ta.GetDataByImportProduction(countentries, SKLID);
        //        return tbl_production;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}

        //public static Pohoda_DataSets.Prijem.ProductionDataTable GETDATA_Grupuj_Production(int countentries, string SKLID)
        //{
        //    Pohoda_DataSets.Prijem.ProductionDataTable tbl_production = null;
        //    try
        //    {
        //        // GRUPUJ polozky pro Prijemku
        //        Globals_V1.LoadConfiguration();
        //        Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter ta = new Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter();
        //        ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        tbl_production = ta.GetDataByImportProduction(countentries, SKLID);
        //        return tbl_production;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}


        //public static bool UPDATEDATA_Production(Pohoda_DataSets.Prijem.ProductionDataTable dt_p, int countentries, string SKLID)
        //{
        //    //Pohoda_DataSets.Prijem.ProductionDataTable tbl_production = null;
        //    try
        //    {
        //        Globals_V1.LoadConfiguration();
        //        Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter ta = new Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter();
        //        ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        ta.Update(dt_p);
        //        return true;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //        throw sqlex;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //    }
        //}

        public static bool CZMSTPE_PONUMBER_EXIST(string ponumber)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
                ta_pe.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                return ta_pe.ScalarQuery(ponumber) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
            }
        }

        public static bool CZMSTPE_UPDATE_CZDOSLO(string ponumber)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
                ta_pe.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                return ta_pe.UpdateQuery(ponumber) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
            }
        }
        


    }
}
