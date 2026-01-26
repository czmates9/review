using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.ModulePohodaXML.Database
{
    class Vydej
    {

          public int Update_row(Fask.Interfaces.DataSets.Vydej.CZMST_SERow row,string ConnectionString)
        {

            int pocet = 0;
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            //string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    {
                        //command a parametry definice
                        commandInsert.CommandText =
                            @" UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                            " SET" +
                            " CZ_Doslo = @CZ_Doslo " +
                            " WHERE  DEX_ROW_ID = @DEX_ROW_ID" +
                            " AND SOPNUMBE = @SOPNUMBE" +
                            " AND CountEntries = @CountEntries" +
                            " AND ITEMNMBR = @ITEMNMBR "
                            ;

                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", Value = 201 });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", Value = row.DEX_ROW_ID });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", Value = row.SOPNUMBE });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", Value = row.CountEntries });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", Value = row.ITEMNMBR });


                        commandInsert.Transaction = transaction;
                       pocet = commandInsert.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    throw exTransaction;
                }

                throw ex;
            }
            finally
            {
                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return pocet;
        }
   



        public static Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable GETDATA_ProductionSources(int countentries, string SKL_ID)
        {
            Pohoda_DataSets.Vydej.Production_SourcesDataTable tbl_ps = null;
            Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                //tbl_ps = ta.GetDataByCountEntries(countentries);
                tbl_ps = ta.GetDataByPSImport(countentries, SKL_ID);
                
               IOrderedEnumerable<Pohoda_DataSets.Vydej.Production_SourcesRow> pss = tbl_ps.OrderBy(x => x.ITEMNMBR);


               foreach (Pohoda_DataSets.Vydej.Production_SourcesRow item in pss)
                {
                    dt.ImportRow(item);   
                }


                return dt;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

        public static Pohoda_DataSets.Vydej.Production_SourcesDataTable GETDATA_Group_ProductionSources(int countentries, string SKL_ID)
        {
            Pohoda_DataSets.Vydej.Production_SourcesDataTable tbl_ps = null;
            try
            {
                // GRUPOVAT DATA PRO Vydejku...
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                
                tbl_ps = ta.GetDataByPSImport_Group(countentries, SKL_ID);

                return tbl_ps;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

        public static bool UPDATEDATA_ProductionSources(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps)
        {
            Pohoda_DataSets.Vydej.Production_SourcesDataTable tbl_ps = new Pohoda_DataSets.Vydej.Production_SourcesDataTable();
            try
            {
                Globals_V1.LoadConfiguration();
  
                foreach (var item in dt_ps)
                {
                    tbl_ps.ImportRow(item);
                }


                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta.Update(tbl_ps);

                return true;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                throw sqlex;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Kontroluje, zda Objecnavka existuje v předloze, respektive, vraci distinct cz_doslo pro kontrolu existence
        /// </summary>
        /// <param name="ponumber">cislo dokladu PONUMBER</param>
        /// <returns></returns>
        public static byte? CZMSTSE_SOPNUMBER_CZDOSLO(string sopnumber)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                object tmp = ta_se.ScalarQueryCZDOSLO(sopnumber);

                if(tmp is byte)
                    return (byte)tmp;
                else
                    return null;

            }
            catch (SqlException sqlex)
            {
                //Log.writeErrorLog(sqlex.Message);
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                //Log.writeErrorLog(ex.Message);
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

        public static byte? CZMSTSE_CZDOSLO_SOPNUMBE_InUse(string sopnumber)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
               object tmp_obj =  ta_se.ScalarGenerate(sopnumber);
               byte? tmp = null;


               if ((tmp_obj != null) && (tmp_obj is byte?))
                   tmp = (byte?)tmp_obj;



                if(tmp.HasValue)
                    return tmp.Value;
                else
                    return null;

                //return (int) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }
            finally
            {
            }
        }

        public static bool CZMSTSE_UPDATE_CZDOSLO(string sopnumber)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                return (int)ta_se.UpdateQuery(sopnumber) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            finally
            {
            }
        }

        public static int CZMSTSE_MAX_CountEntries(SqlConnection sqlconnection, SqlTransaction sqltransaction)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            sqlcommand = new System.Data.SqlClient.SqlCommand();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandType = System.Data.CommandType.Text;
            sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_SE";
            sqlcommand.Transaction = sqltransaction;
            object o = sqlcommand.ExecuteScalar();

            try
            {
                int countentries = Convert.ToInt32(o);
                return countentries;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return 0; //pokud nenalezeno ... ???
            }
        }

        public static bool CZMSTSE_EXIST_SOPNUMBE(string sopnumbe)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                return (int)ta_se.ScalarQuery(sopnumbe) > 0 ? true : false;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        #region Materialy z FASK_Vyroba_TP

        private static Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps = null;
        private static int _countEntries;
        private static string _SKL_ID;
        private static string _USER_ID;

        private static decimal PocetOdvedeno;
        private static Guid ProductionGUID;

        public static Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable GET_PS_from_TP(int countEntries, string SKL_ID, string userID)
        {
            //1. Poznám CountEntries(číslo výrobního příkazu) a SKL_ID (Idenfikator skladu)

            //2. je potřeba dotahnot seznam z Production
            //3. Nasledne foreach projit všechny Vyrobky a k nim dotahnut Materialy
            //4. Dotahovane materialy cpat do tabulky ProductionSources
            //5. Použit rekurzivnu metodu z Vyroba_W

            dt_ps = new Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable();
            dt_ps.Clear();

            _countEntries = countEntries;
            _SKL_ID = SKL_ID;
            _USER_ID = userID;

            //var dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);
            var dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);

            if ((dt_p != null) && (dt_p.Count > 0))
            {

                foreach (var item in dt_p)
                {
                    if (item.IsTIMESTOPNull())
                        continue;

                    PocetOdvedeno = item.qty;
                    ProductionGUID = item.GUID;

                    Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = Database.Vyroba_FASK_Vyroba_TP.GetDataBy_itemnmbrDef_IDHNULL(item.ITEMNMBR);

                    //VPP ITEMNMBR a podle toho hledam materialy
                    if (vyrobky.Count > 0)
                    {
                        LoadMaterialy3(vyrobky);
                    }
                }
            }
            else
            {
                return null;
            }

            return dt_ps; 

        }

        /// <summary>
        /// pomoci rekurze ...
        /// </summary>
        private static void LoadMaterialy3(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky)
        {
             bool PriznakNacteneMaterialy;

            if (vyrobky.Count() == 0)
            {
                // TODO : upravit hlaseni error ... 
                //MessageBox.Show("Nenalezeno ... ");
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"Nenalezeno ... ");
            }
            else if (vyrobky.Count() == 1)
            {
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
            }
            else
            { // TODO : Vyber, ktery z vyrobku / variant vyrobku ... 
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
            }
        }

        #region Parametry materialu pro rekurzi ...
        /// <summary>
        /// Slouzi pro interni vypocty v ramci rekurzivniho pruchodu stromu TP
        /// </summary>
        private class materialParams
        {
            public materialParams()
            {
            }

            public materialParams(decimal koef)
            {
                this.KoeficientSet(koef);
            }

            public materialParams(materialParams mP, decimal koef)
            {
                if (mP == null)
                {
                    mP = new materialParams();
                }

                this.KoeficientSet(koef);
            }

            public void KoeficientSet(decimal koef)
            {
                this.KoeficientNadrazeny = koef;
                this.KoeficientKumulovany *= koef;
            }

            /// <summary>
            /// Koeficient nadrazeneho uzlu
            /// </summary>
            public decimal KoeficientNadrazeny = 1;
            /// <summary>
            /// Kumulovany koeficient od 1.uzlu az do aktualni urovne...
            /// </summary>
            public decimal KoeficientKumulovany = 1;
        }

        #endregion

        /// <summary>
        /// Rekurzivni volani a 
        /// </summary>
        /// <param name="rUp"></param>
        /// <returns></returns>
        private static bool isMaterial(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPRow rUp, materialParams materialparams)
        {

            // predavani parametru materialu z vyssi urovne ... 
            if (materialparams == null)
            {
                materialparams = new materialParams();
            }
            else
            {
                decimal koefUp = 1;
                try
                {

                    string tmpKoef = string.IsNullOrEmpty(rUp.koef) ? string.Empty : rUp.koef.Trim();

                    if (tmpKoef.Contains(","))
                        tmpKoef = tmpKoef.Replace(",", ".");

                    koefUp = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                { }

                materialparams = new materialParams(materialparams, koefUp);
            }

            if ((rUp == null) || (rUp.IsID_LNull()))
                return false;

            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable rDowns = Vyroba_FASK_Vyroba_TP.GetDataByIDH(rUp.ID_L);


            if (rDowns.Count() == 0)
            {
                return true;
            }
            else
            {
                // alternace a jen nektere ... 
                // vyberu jestli jsou alternace na teto urovni .. 
                var rAlt = rDowns.GroupBy(x => x.IsalterNull() ? string.Empty : x.alter).OrderBy(x => x.Key);
                if (rAlt.Count() == 0)
                {
                    return false;
                }

                foreach (var alternace in rAlt)
                {
                    if (String.IsNullOrEmpty(alternace.Key))
                    {
                        foreach (var rDown in alternace)
                        {
                            bool isM = isMaterial(rDown, materialparams);

                            if (isM)
                            { // vlozim data
                                FillDatasetMaterialy(rDown, materialparams);
                            }
                        }
                    }
                    else
                    { // alternace
                        
                        var RowFirst = alternace.ToArray().First();

                        throw new Exception("Data pro výdejku nelze připravit. Položka:" + RowFirst.DESC_Def + "(" + RowFirst.ITEMNMBR_Def + ")" + "' obsahuje alternace!");
                        //!!!! ALTERNACE NESMI NASTAT!!!!
                    }
                }
            }


            return false;
        }


        private static void FillDatasetMaterialy(Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPRow rTP, materialParams materialparams)
        {
            //// TaD
            ////Pridani materialu do tabulky production sources
            //// na zaklade jednoh radku v tabulke FASK_Vyroba_TP
            ////


            Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable dt_FASK_ZASOBY = new Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable(); 

            Vydej.FillByITEMNMBR(dt_FASK_ZASOBY, rTP.ITEMNMBR_fol);

            if ((dt_FASK_ZASOBY == null) || (dt_FASK_ZASOBY.Count == 0))
            {
                throw new Exception("Nebylo nalezeno zboží : '" + rTP.ITEMNMBR_Def + "'");
            }

            else if (dt_FASK_ZASOBY.Count > 1)
            {
                //Tato varianta by nemnela nastat
                throw new Exception("Bylo nalezeno vic řádku zboží : '" + rTP.ITEMNMBR_Def + "'");
            }




            // predavany parametr je tabulka zbozi materialu pridavaneho 
            
            try
            {
                foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow i in dt_FASK_ZASOBY)
                {
                    Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row = dt_ps.NewProduction_SourcesRow();
                    //Data.VyrobaCEDataSet.Production_SourcesRow row = _productionSDT.NewProduction_SourcesRow();

                    row.CountEntries = _countEntries;
                    row.SOPNUMBE = string.Empty;
                    row.ITEMNAME = i.ITEMDESC.Trim();
                    row.ITEMNMBR = i.ITEMNMBR.Trim();
                    row.ITEMTYPE = string.Empty;

                    if (i.IsITEMCODENull())
                        row.SetITEMCODENull();
                    else
                        row.ITEMCODE = i.ITEMCODE.Trim();

                    row.MJ = i.MJ.Trim();

                    decimal mnozstvi = 1;
                    decimal koeficient = 1;
                    try
                    {
                        string tmpKoef = string.IsNullOrEmpty(rTP.koef) ? string.Empty : rTP.koef.Trim();

                        if (tmpKoef.Contains(","))
                            tmpKoef = tmpKoef.Replace(',', '.');

                        koeficient = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { }
                    //mnozstvi = (decimal.Parse(rTP.koef) * PocetOdvedeno);
                    mnozstvi = materialparams.KoeficientKumulovany * koeficient * PocetOdvedeno;

                    row.QTYSHPPD = mnozstvi * (i.QTYPACK == 0 ? 1 : i.QTYPACK);
                    row.QTYSHPPDMJ = mnozstvi;
                    row.QTYPACK = i.QTYPACK;

                    row.SERLTNUM = string.Empty;
                    row.SKL_ID = _SKL_ID;
                    row.LOCNCODE = string.Empty;
                    row.GUID = Guid.NewGuid();
                    row.GUID_Production = ProductionGUID;
                    row.USER_ID = _USER_ID;
                    row.TERMINAL_ID = 9999;
                    row.NMBRPAL = string.Empty;
                    row.TYPEPAL = string.Empty;
                    row.PRINTED = 0;

                    dt_ps.AddProduction_SourcesRow(row);
                }

     

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                throw ex;
            }
        }


        internal static void FillByITEMNMBR(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLADataTable dt_FASK_ZASOBY, string p)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMNMBR=@itemnmbr ";

                command.Parameters.AddWithValue("@itemnmbr", p);



                adapter.SelectCommand = command;
                adapter.Fill(dt_FASK_ZASOBY);

                //return ds;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        internal static void DELETEDATA_ProductionSources(int countEntries, string SKL_ID)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta.DeleteBy_CountEntriesANDSKL_ID(countEntries, SKL_ID);

            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                throw sqlex;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
            }
        }

        #region Komunikace Bez TableAdaptery

        public static bool UpdateCzDosloByCountEntries(byte CZ_Doslo, int CountEntries)
        {
            Globals_V1.LoadConfiguration();

            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {

                    using (var com = con.CreateCommand())
                    {
                        com.CommandType = System.Data.CommandType.Text;
                        com.CommandText = "UPDATE " +
                            Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                            " SET CZ_Doslo = '" +
                            CZ_Doslo.ToString()
                            + "' WHERE CountEntries = " +
                            CountEntries.ToString() +
                            " AND " +
                            " CZ_Doslo = '255'";

                        con.Open();
                        int tmp = com.ExecuteNonQuery();
                        return tmp > 0 ? true : false;
                    }
                }
            }
            catch (SqlException sqlex)
            {
                Logging.ExceptionHandler2.Handle(sqlex);
                return false;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        #endregion
    }
}
