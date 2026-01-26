using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : 
        Fask.Interfaces.Ciselniky.Sklady.ISklady2,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_DeleteSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetFiltrovaneSklady,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_UpdateSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_ImportSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill
    {

        #region ISklady2

        #region ISklady2_DeleteSklad Members

        public bool DeleteSklad(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    connection.Open();
                    trans = connection.BeginTransaction();

                    //    connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);
                    //connection.Open();

                    //Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter();

                    //ta_sklady.Connection = connection;
                    //trans = connection.BeginTransaction(IsolationLevel.Serializable);
                    //ta_sklady.MyTransaction = trans;

                    //ta_sklady.Delete(id);
                    Database.Ciselniky.Delete_ByID_CZMST093(connection, trans, id);

                    if (trans != null)
                        trans.Commit();
                }

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

        #region ISklady2_GetFiltrovaneSklady Members

        public Fask.Interfaces.DataSets.Sklady GetFiltrovaneSklady(Fask.Interfaces.Filtry.SkladyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.skl_id))
                {
                    command.CommandText += "and sklady.skl_id=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.skl_id);
                }

                if (!string.IsNullOrEmpty(filtr.skl_desc))
                {
                    command.CommandText += "and sklady.skl_desc like '%' + @skl_desc + '%' ";
                    command.Parameters.AddWithValue("@skl_desc", filtr.skl_desc);
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST093);

                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ISklady2_GetSkladByID Members

        public Fask.Interfaces.DataSets.Sklady.CZMST093Row GetSkladByID(string id)
        {
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

            try
            {
                Fask.Interfaces.Filtry.SkladyListFiltr filtr = new Fask.Interfaces.Filtry.SkladyListFiltr();
                filtr.skl_id = id;

                ds =  GetFiltrovaneSklady(filtr);

                if (ds.CZMST093.Count > 0)
                    return ds.CZMST093.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISklady2_GetSklady Members

        public Fask.Interfaces.DataSets.Sklady GetSklady()
        {
            Fask.Interfaces.Filtry.SkladyListFiltr filtr = new Fask.Interfaces.Filtry.SkladyListFiltr();


            return GetFiltrovaneSklady(filtr);
        }

        #endregion

        #region ISklady2_InsertSklad Members

        #region old 23.10.2024
        //public bool InsertSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        Globals_V1.LoadConfiguration();

        //        connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        connection.Open();

        //        Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter();
        //        ta_sklady.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_sklady.MyTransaction = trans;

        //        #region old MaR 23.10.2024
        //        //ta_sklady.Insert(
        //        //    skladRow.skl_id,
        //        //    skladRow.Isskl_descNull() ? null : skladRow.skl_desc,
        //        //    skladRow.Isskl_typNull() ? null : skladRow.skl_typ,
        //        //    skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode
        //        //    ); 
        //        #endregion

        //        ta_sklady.Insert(
        //           skladRow.skl_id.Trim(),
        //           skladRow.Isskl_descNull() ? null : skladRow.skl_desc.Trim(),
        //           skladRow.Isskl_typNull() ? null : skladRow.skl_typ.Trim(),
        //           skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode.Trim()
        //           );

        //        if (trans != null)
        //            trans.Commit();

        //        return true;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            if (trans != null)
        //                trans.Rollback();
        //        }
        //        catch { }
        //        throw;
        //    }
        //    finally
        //    {
        //        if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
        //            connection.Close();
        //    }
        //} 

        #endregion

        #region new 23.10.2024

        public bool InsertSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                trans = connection.BeginTransaction(IsolationLevel.Serializable);

                // SQL dotaz s parametry
                string query = @"
            INSERT INTO [dbo].[CZMST093]
            ([skl_id], [skl_desc], [skl_typ], [skl_carcode])
            VALUES (@skl_id, @skl_desc, @skl_typ, @skl_carcode)";

                using (var command = new System.Data.SqlClient.SqlCommand(query, connection, trans))
                {
                    // Nastavení parametrů dotazu
                    command.Parameters.AddWithValue("@skl_id", (skladRow.skl_id.Trim()));
                    command.Parameters.AddWithValue("@skl_desc", skladRow.Isskl_descNull() ? string.Empty : (skladRow.skl_desc.Trim()));
                    command.Parameters.AddWithValue("@skl_typ", skladRow.Isskl_typNull() ? string.Empty : (skladRow.skl_typ.Trim()));
                    command.Parameters.AddWithValue("@skl_carcode", skladRow.Isskl_carcodeNull() ? string.Empty : (skladRow.skl_carcode.Trim()));

                    // Provádění SQL příkazu
                    command.ExecuteNonQuery();
                }

                // Commit transakce
                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                // Rollback transakce v případě chyby
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


        private string RemoveInvisibleChars(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Včetně speciálních znaků, jako jsou různé typy neviditelných mezer
            string output = input.Replace("\u00A0", ""); // Non-breaking space
            output = System.Text.RegularExpressions.Regex.Replace(output, @"\p{C}+", ""); // Odstranění kontrolních znaků
            return output.Trim();
        }


        #endregion

        #endregion

        #region ISklady2_UpdateSklad Members

        #region old 23.10.2024
        //public bool UpdateSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        Globals_V1.LoadConfiguration();
        //        connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        connection.Open();

        //        Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter();
        //        ta_sklady.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_sklady.MyTransaction = trans;

        //        #region old MaR 23.10.2024
        //        //int pocet = ta_okruh.Update(okruhRow);    // nefunguje ...
        //        //ta_sklady.Update(
        //        //    skladRow.Isskl_descNull() ? null : skladRow.skl_desc,
        //        //    skladRow.Isskl_typNull() ? null : skladRow.skl_typ,
        //        //    skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode,
        //        //    skladRow.skl_id
        //        //    ); 
        //        #endregion

        //        ta_sklady.Update(
        //      skladRow.Isskl_descNull() ? null : skladRow.skl_desc.Trim(),
        //      skladRow.Isskl_typNull() ? string.Empty : skladRow.skl_typ.Trim(),
        //      skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode.Trim(),
        //      skladRow.skl_id
        //      );

        //        if (trans != null)
        //            trans.Commit();

        //        return true;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            if (trans != null)
        //                trans.Rollback();
        //        }
        //        catch { }
        //        throw;
        //    }
        //    finally
        //    {
        //        if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
        //            connection.Close();
        //    }
        //} 
        #endregion


        public bool UpdateSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                trans = connection.BeginTransaction(IsolationLevel.Serializable);

                // SQL příkaz pro aktualizaci záznamu
                string query = @"
            UPDATE [dbo].[CZMST093]
            SET [skl_desc] = @skl_desc,
                [skl_typ] = @skl_typ,
                [skl_carcode] = @skl_carcode
            WHERE [skl_id] = @skl_id";

                using (var command = new System.Data.SqlClient.SqlCommand(query, connection, trans))
                {
                    // Přidání parametrů
                    command.Parameters.AddWithValue("@skl_id", (skladRow.skl_id.Trim()));
                    //command.Parameters.AddWithValue("@skl_desc", skladRow.Isskl_descNull() ? (object)DBNull.Value : RemoveInvisibleChars(skladRow.skl_desc.Trim()));
                    //command.Parameters.AddWithValue("@skl_typ", skladRow.Isskl_typNull() ? (object)DBNull.Value : RemoveInvisibleChars(skladRow.skl_typ.Trim()));
                    //command.Parameters.AddWithValue("@skl_carcode", skladRow.Isskl_carcodeNull() ? (object)DBNull.Value : RemoveInvisibleChars(skladRow.skl_carcode.Trim()));

                    command.Parameters.AddWithValue("@skl_desc", skladRow.Isskl_descNull() ? string.Empty : (skladRow.skl_desc.Trim()));
                    command.Parameters.AddWithValue("@skl_typ", skladRow.Isskl_typNull() ? string.Empty : (skladRow.skl_typ.Trim()));
                    command.Parameters.AddWithValue("@skl_carcode", skladRow.Isskl_carcodeNull() ? string.Empty : (skladRow.skl_carcode.Trim()));

                    // Spuštění příkazu UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kontrola, zda byl záznam aktualizován
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Záznam s tímto skl_id nebyl nalezen.");
                    }
                }

                // Commit transakce
                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                // Rollback transakce v případě chyby
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

        #region ISklady2_Vyroba_Fill Members

        public void Sklady_Vyroba_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //Globals_V1.LoadConfiguration();
            //Pohoda_DataSets.VyrobaDataSet.CZMST093DataTable dt = new Pohoda_DataSets.VyrobaDataSet.CZMST093DataTable();

            //Pohoda_DataSets.VyrobaDataSetTableAdapters.CZMST093TableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.CZMST093TableAdapter();
            //ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //ta.Fill(dt);

            //foreach (var item in dt)
            //{
            //    ds.CZMST093.ImportRow(item);
            //}

            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093;

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds.CZMST093);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return;
            }


        }

        #endregion


        #endregion

        #region ISklady2_ImportSklad Members

        public string ImportSkladu()
        {
            //Datasets.DatabasePohodaTableAdapters.sSkladTableAdapter  ssklad_ta = null;
            SqlTransaction trans = null;
            SqlConnection connection = null;
            try
            {
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                //ssklad_ta = new Datasets.DatabasePohodaTableAdapters.sSkladTableAdapter();
                //ssklad_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;


                //so.Write("export zahajen");
                //TODO: vybirat jen nektere sloupce
                //Datasets.DatabasePohoda.sSkladDataTable ssklad_dt = ssklad_ta.GetData();
                Pohoda_DataSets.DatabasePohoda.sSkladDataTable ssklad_dt = Database.Pohoda.sSklad_GetData();

                using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    connection.Open();
                    trans = connection.BeginTransaction();

                    //    Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter CZMST_093TableAdapter = new Pohoda_DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                    //CZMST_093TableAdapter.Connection.ConnectionString = Globals.ConnectionString;

                    //CZMST_093TableAdapter.DeleteQuery();
                    Database.Ciselniky.Delete_CZMST093(connection, trans);

                    Pohoda_DataSets.Sklady sklady = new Pohoda_DataSets.Sklady();

                    string skl_id;
                    string skl_desc;
                    string skl_typ;
                    string skl_carcode;


                    int skl_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_id"].MaxLength;
                    int skl_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_desc"].MaxLength;
                    int skl_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_typ"].MaxLength;
                    int skl_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_carcode"].MaxLength;


                    //so.Write("export polozek z pohody do databaze");
                    //SKz - brat "NAZEV"- ITEMDESC, "EAN" - VNDITNUM, "ID" - do ITEMNMBR, "RefStruc" - LOCNCODE, "IDS" - CZ_CarCode
                    //Skz - "RelSKzVC" - "2" Sarze a "1" vyrobni cislo, "MJ" - do MJ,
                    foreach (var item in ssklad_dt)
                    {
                        skl_id = item.ID.ToString();
                        //skl_desc = item.IsSTextNull() ? "" : item.SText;
                        if (item.IsIDSNull())
                            skl_desc = item.IsSTextNull() ? "" : item.SText.Trim();
                        else
                            skl_desc = item.IDS.Trim();
                        skl_typ = "";
                        skl_carcode = item.IsIDSNull() ? "" : item.IDS.Trim();

                        if (skl_id.Length > skl_id_MaxLength)
                            skl_id = skl_id.Remove(skl_id_MaxLength);

                        if (skl_desc.Length > skl_desc_MaxLength)
                            skl_desc = skl_desc.Remove(skl_desc_MaxLength);

                        if (skl_typ.Length > skl_typ_MaxLength)
                            skl_typ = skl_typ.Remove(skl_typ_MaxLength);

                        if (skl_carcode.Length > skl_carcode_MaxLength)
                            skl_carcode = skl_carcode.Remove(skl_carcode_MaxLength);

                        sklady.CZMST093.AddCZMST093Row(skl_id, skl_desc, skl_typ, skl_carcode);

                    }

                    //CZMST_093TableAdapter.Update(sklady.CZMST093);
                    Database.Ciselniky.Update_CZMST093(sklady.CZMST093, connection, trans);

                    if (trans != null)
                        trans.Commit();
                }
                //so.Write("export se provedl uspesne");
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();

                return ex.Message;

            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return "OK";
        }

        public List<Tuple<string, string, bool>> GetSkladyTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST093 + "'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ZasobyTableInfo info = new ZasobyTableInfo();
                        info.ColumnName = reader["COLUMN_NAME"].ToString();
                        info.DataType = reader["DATA_TYPE"].ToString();
                        info.IsNullable = reader["IS_NULLABLE"].ToString() == "YES";
                        tableInfo.Add(new Tuple<string, string, bool>(info.ColumnName, info.DataType, info.IsNullable));
                    }

                    reader.Close();
                }
            }

            return tableInfo;
        }

        #endregion
    }
}
