using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_DeleteSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetFiltrovanySkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment
    {
        #region ISkladLokace_LVS2

        #region ISkladLokace_LVS2_DeleteSkladLokace_LokaceVariantySortiment Members

        public bool DeleteSkladLokace_LokaceVariantySortiment(string skl_id, string locncode, string itemnmbr)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_LokaceVariantySortiment]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT + " " +
                    "where " +
                    "SKL_ID=@skl_id and LOCNCODE=@locncode and itemnmbr=@itemnmbr";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region ISkladLokace_LVS2_GetFiltrovanySkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokace_LokaceVariantySortiment(Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                // TODO: upravit join na FASK_ZASOBY tak, aby pouzil pouze prvni nalezenou polozku
                command.CommandText =
                    "select sortiment.*, zbozi.ITEMDESC as ZboziITEMDESC, zbozi.ITEMCODE as ZboziITEMCODE, zbozi.CZ_CarKod as ZboziCZ_CarKod, zbozi.VNDITNUM as ZboziVNDITNUM, typy.IS_RECEIVE as TypIS_RECEIVE, typy.IS_DEFAULT as TypIS_DEFAULT, typy.IS_NORMAL as TypIS_Normal " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT + " sortiment " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " zbozi on zbozi.ITEMNMBR=sortiment.ITEMNMBR " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY + " typy on typy.TYPE=sortiment.TYPE " +
                    "where " +
                    "1=1 ";

                if (!string.IsNullOrEmpty(filtr.MaterialID))
                {
                    command.CommandText += "and sortiment.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "and zbozi.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "and (zbozi.CZ_CarKod=@barcode or zbozi.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialTypLokace))
                {
                    command.CommandText += "and sortiment.[TYPE]=@typ ";
                    command.Parameters.AddWithValue("@typ", filtr.MaterialTypLokace);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialSKLID))
                {
                    command.CommandText += "and sortiment.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.MaterialSKLID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialLocncode))
                {
                    command.CommandText += "and sortiment.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.MaterialLocncode);
                }

                command.CommandText += "order by sortiment.SKL_ID, sortiment.LOCNCODE ";

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_LokaceVariantySortiment);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceVariantySortiment()
        {
            try
            {
                Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr filtr = new Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr();
                return GetFiltrovanySkladLokace_LokaceVariantySortiment(filtr);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr Members

        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(string skl_id, string locncode, string itemnmbr)
        {
            try
            {
                Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr filtr = new Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr();
                filtr.MaterialSKLID = skl_id;
                filtr.MaterialLocncode = locncode;
                filtr.MaterialID = itemnmbr;
                Fask.Interfaces.DataSets.SkladLokace ds = GetFiltrovanySkladLokace_LokaceVariantySortiment(filtr);
                if (ds.CZMST_SkladLokace_LokaceVariantySortiment.Count > 0)
                    return ds.CZMST_SkladLokace_LokaceVariantySortiment.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS InsertSkladLokace_LokaceVariantySortiment(byte TermID, Fask.Interfaces.DataSets.Inventura.CZMST_I4Row row, string type)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                command = new System.Data.SqlClient.SqlCommand();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter ta_varianty = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter();
                ta_varianty.Connection = connection;
                command.Connection = connection;

                // Transakce
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_varianty.MyTransaction = trans;
                command.Transaction = trans;

                // 1) Kontrola zda lokace na sklade v mape je zavedena
                command.CommandText =
                    "Select Count(*) FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " " +
                    "WHERE SKL_ID=@skl_id AND LOCNCODE=@locncode";
                command.Parameters.AddWithValue("@skl_id", row.SKL_ID);
                command.Parameters.AddWithValue("@locncode", row.LOCNCODE);
                object cnt = command.ExecuteScalar();
                if (Convert.ToInt32(cnt) <= 0)
                { // neexistuje, tak vyjimka ...
                    throw new Exception(string.Format("Lokace '{0}' na skladě '{1}' neexistuje!", row.LOCNCODE.Trim(), row.SKL_ID.Trim()));
                }

                // 2) Kontrola, zda existuje vubec takovy typ a jake ma vlastnosti ...
                command.Parameters.Clear();
                command.CommandText =
                    "Select COUNT(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY +
                    " WHERE TYPE=@type";
                command.Parameters.AddWithValue("@type", type);
                cnt = command.ExecuteScalar();
                if (Convert.ToInt32(cnt) <= 0)
                { // neexistuje, tak vyjimka ...
                    throw new Exception(string.Format("Lokace typu '{0}' neexistuje!", type));
                }

                // 16.5.2016 PeV: zakomentovano, nekontrolovat nad daty v DB (aby bylo mozne priradit lokaci v pripade, ze jsou nasnimany ruzne lokace pro stejnou polozku)
                // 3) Kontrola, zdali material, sklad neni v inventure na nekolika ruznych lokacich
                //command.Parameters.Clear();
                //command.CommandText =
                //    "Select COUNT(*) from " + TABLE_CZMST_I4 +
                //    " WHERE " + 
                //    "ITEMNMBR=@itemnmbr AND " + 
                //    "SKL_ID=@skl_id AND " + 
                //    "LOCNCODE<>@locncode";
                //command.Parameters.AddWithValue("@itemnmbr", row.ITEMNMBR);
                //command.Parameters.AddWithValue("@skl_id", row.SKL_ID);
                //command.Parameters.AddWithValue("@locncode", row.LOCNCODE);
                //cnt = command.ExecuteScalar();
                //if (Convert.ToInt32(cnt) > 0)
                //{ // neexistuje, tak vyjimka ...
                //    return Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS.ERROR_VICE_LOKACI;                    
                //}

                // 4) Kontrola, zdali jiz je ulozen v tabulce LokaceVariantySortiment
                command.Parameters.Clear();
                command.CommandText =
                    "Select COUNT(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT + " " +
                    "WHERE " +
                    "ITEMNMBR=@itemnmbr AND " +
                    "SKL_ID=@skl_id";
                command.Parameters.AddWithValue("@itemnmbr", row.ITEMNMBR);
                command.Parameters.AddWithValue("@skl_id", row.SKL_ID);
                cnt = command.ExecuteScalar();
                if (Convert.ToInt32(cnt) > 0)
                { // jiz existuje, provede se update ...
                    command.Parameters.Clear();
                    command.CommandText =
                        "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT + " " +
                        "SET TYPE = @TYPE, LOCNCODE = @locncode " +
                        "WHERE " +
                        //"(SKL_ID = @skl_id) AND (LOCNCODE = @locncode) AND (ITEMNMBR = @itemnmbr)"
                        "(SKL_ID = @skl_id) AND (ITEMNMBR = @itemnmbr)"
                        ;

                    command.Parameters.AddWithValue("@TYPE", type);
                    command.Parameters.AddWithValue("@locncode", row.LOCNCODE);
                    command.Parameters.AddWithValue("@skl_id", row.SKL_ID);
                    command.Parameters.AddWithValue("@itemnmbr", row.ITEMNMBR);
                    command.ExecuteNonQuery();
                    //ta_varianty.Update(
                    //    type,
                    //    row.SKL_ID,
                    //    row.LOCNCODE,
                    //    row.ITEMNMBR
                    //    );
                }
                else
                { // neexistuje, provede se insert
                    ta_varianty.Insert(
                    row.ITEMNMBR,
                    row.SKL_ID,
                    row.LOCNCODE,
                    type,
                    null,
                    TermID,
                    DateTime.Now
                    );
                }

                if (trans != null)
                    trans.Commit();

                return Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS.OK;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row Members

        public bool InsertSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter ta_varianty = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter();
                ta_varianty.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_varianty.MyTransaction = trans;

                ta_varianty.Insert(
                    row.ITEMNMBR,
                    row.SKL_ID,
                    row.LOCNCODE,
                    row.TYPE,
                    row.IsUserIDNull() ? (int?)null : row.UserID,
                    row.IsTermIDNull() ? (int?)null : row.TermID,
                    DateTime.Now
                    );

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment Members

        public bool UpdateSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row, string locncodeold)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string textCommand =
                        "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACEVARIANTYSORTIMENT + " " +
                        "SET TYPE = @TYPE, LOCNCODE = @locncode " +
                        "WHERE " +
                    //"(SKL_ID = @skl_id) AND (LOCNCODE = @locncode) AND (ITEMNMBR = @itemnmbr)"
                        "(SKL_ID = @skl_id) AND (ITEMNMBR = @itemnmbr) AND LOCNCODE = @locncodeold"
                        ;
                command = new System.Data.SqlClient.SqlCommand(textCommand, connection, trans);
                command.Parameters.AddWithValue("@TYPE", row.TYPE);
                command.Parameters.AddWithValue("@locncodeold", locncodeold);
                command.Parameters.AddWithValue("@locncode", row.LOCNCODE);
                command.Parameters.AddWithValue("@skl_id", row.SKL_ID);
                command.Parameters.AddWithValue("@itemnmbr", row.ITEMNMBR);
                command.ExecuteNonQuery();

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
            // 16.5.2016 PeV: prepsano kvuli editaci lokace ...
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter ta_varianty = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter();
            //    ta_varianty.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_varianty.MyTransaction = trans;

            //    ta_varianty.Update(
            //        row.TYPE,
            //        row.SKL_ID,
            //        row.LOCNCODE,
            //        row.ITEMNMBR
            //        );

            //    if (trans != null)
            //        trans.Commit();

            //    return true;
            //}
            //catch
            //{
            //    try
            //    {
            //        if (trans != null)
            //            trans.Rollback();
            //    }
            //    catch { }
            //    throw;
            //}
            //finally
            //{
            //    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
            //        connection.Close();
            //}
        }

        #endregion

        #endregion
   
    }
}
