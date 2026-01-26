using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Fask.ModulePohodaXML.Pohoda_DataSets;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_DeleteOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_UpdateOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_ImportOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetFiltrovaneOdberatele
    {
        #region IOdberatele2

        #region IOdberatele2_DeleteOdberatel Members

        public bool DeleteOdberatel(string id)
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

                // odstraneni zaznamu z tabulky [CZMST090]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " " +
                    "where " +
                    "odb_id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

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

        #region IOdberatele2_GetOdberatelByID Members

        public Fask.Interfaces.DataSets.Odberatele.CZMST090Row GetOdberatelByID(string id)
        {

            try
            {
                Fask.Interfaces.DataSets.Odberatele dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();

                Fask.Interfaces.Filtry.AdresarListFiltr filter = new Fask.Interfaces.Filtry.AdresarListFiltr();
                filter.id = id;
                dsOdberatele =  GetFiltrovaneOdberatele(filter);

                if (dsOdberatele.CZMST090.Count > 0)
                    return dsOdberatele.CZMST090.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IOdberatele2_GetOdberatele Members

        public Fask.Interfaces.DataSets.Odberatele GetOdberatele()
        {
            Fask.Interfaces.Filtry.AdresarListFiltr filter = new Fask.Interfaces.Filtry.AdresarListFiltr();
            return GetFiltrovaneOdberatele(filter);
        }

        #endregion

        #region IOdberatele2_InsertOdberatel Members

        public bool InsertOdberatel(Fask.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow)
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

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " " +
                    "(odb_id, odb_desc, odb_typ, odb_carcode, odb_ico, mena_ID, odb_misto, odb_ulice, odb_cisloOr, odb_psc, odb_dic, odb_Odberatel, odb_Dodavatel) " +
                    "VALUES " +
                    "(@odb_id, @odb_desc, @odb_typ, @odb_carcode, @odb_ico, @mena_ID, @odb_misto, @odb_ulice, @odb_cisloOr, @odb_psc, @odb_dic, @odb_Odberatel, @odb_Dodavatel)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@odb_id", odberatelRow.odb_id);
                command.Parameters.AddWithValue("@odb_desc", odberatelRow.Isodb_descNull() ? (object)DBNull.Value : odberatelRow.odb_desc);
                command.Parameters.AddWithValue("@odb_typ", odberatelRow.Isodb_typNull() ? (object)DBNull.Value : odberatelRow.odb_typ);
                command.Parameters.AddWithValue("@odb_carcode", odberatelRow.Isodb_carcodeNull() ? (object)DBNull.Value : odberatelRow.odb_carcode);
                command.Parameters.AddWithValue("@odb_ico", odberatelRow.Isodb_icoNull() ? (object)DBNull.Value : odberatelRow.odb_ico);
                command.Parameters.AddWithValue("@mena_ID", odberatelRow.Ismena_idNull() ? (object)DBNull.Value : odberatelRow.mena_id);
                command.Parameters.AddWithValue("@odb_misto", odberatelRow.Isodb_mistoNull() ? (object)DBNull.Value : odberatelRow.odb_misto);
                command.Parameters.AddWithValue("@odb_ulice", odberatelRow.Isodb_uliceNull() ? (object)DBNull.Value : odberatelRow.odb_ulice);
                command.Parameters.AddWithValue("@odb_cisloOr", odberatelRow.Isodb_cisloOrNull() ? (object)DBNull.Value : odberatelRow.odb_cisloOr);
                command.Parameters.AddWithValue("@odb_psc", odberatelRow.Isodb_pscNull() ? (object)DBNull.Value : odberatelRow.odb_psc);
                command.Parameters.AddWithValue("@odb_dic", odberatelRow.Isodb_dicNull() ? (object)DBNull.Value : odberatelRow.odb_dic);
                command.Parameters.AddWithValue("@odb_Odberatel", odberatelRow.Isodb_OdberatelNull() ? (object)DBNull.Value : odberatelRow.odb_Odberatel);
                command.Parameters.AddWithValue("@odb_Dodavatel", odberatelRow.Isodb_DodavatelNull() ? (object)DBNull.Value : odberatelRow.odb_Dodavatel);

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

        #region IOdberatele2_UpdateOdberatel Members

        public bool UpdateOdberatel(Fask.Interfaces.DataSets.Odberatele.CZMST090Row odberatelRow)
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

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " " +
                    "set odb_desc=@odb_desc, odb_typ=@odb_typ, odb_carcode=@odb_carcode, odb_ico=@odb_ico, mena_ID=@mena_ID, odb_misto=@odb_misto, odb_ulice=@odb_ulice, odb_cisloOr=@odb_cisloOr, odb_psc=@odb_psc, odb_dic=@odb_dic, odb_Odberatel=@odb_Odberatel, odb_Dodavatel=@odb_Dodavatel " +
                    "where odb_id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", odberatelRow.odb_id);
                command.Parameters.AddWithValue("@odb_desc", odberatelRow.Isodb_descNull() ? (object)DBNull.Value : odberatelRow.odb_desc);
                command.Parameters.AddWithValue("@odb_typ", odberatelRow.Isodb_typNull() ? (object)DBNull.Value : odberatelRow.odb_typ);
                command.Parameters.AddWithValue("@odb_carcode", odberatelRow.Isodb_carcodeNull() ? (object)DBNull.Value : odberatelRow.odb_carcode);
                command.Parameters.AddWithValue("@odb_ico", odberatelRow.Isodb_icoNull() ? (object)DBNull.Value : odberatelRow.odb_ico);
                command.Parameters.AddWithValue("@mena_ID", odberatelRow.Ismena_idNull() ? (object)DBNull.Value : odberatelRow.mena_id);
                command.Parameters.AddWithValue("@odb_misto", odberatelRow.Isodb_mistoNull() ? (object)DBNull.Value : odberatelRow.odb_misto);
                command.Parameters.AddWithValue("@odb_ulice", odberatelRow.Isodb_uliceNull() ? (object)DBNull.Value : odberatelRow.odb_ulice);
                command.Parameters.AddWithValue("@odb_cisloOr", odberatelRow.Isodb_cisloOrNull() ? (object)DBNull.Value : odberatelRow.odb_cisloOr);
                command.Parameters.AddWithValue("@odb_psc", odberatelRow.Isodb_pscNull() ? (object)DBNull.Value : odberatelRow.odb_psc);
                command.Parameters.AddWithValue("@odb_dic", odberatelRow.Isodb_dicNull() ? (object)DBNull.Value : odberatelRow.odb_dic);
                command.Parameters.AddWithValue("@odb_Odberatel", odberatelRow.Isodb_OdberatelNull() ? (object)DBNull.Value : odberatelRow.odb_Odberatel);
                command.Parameters.AddWithValue("@odb_Dodavatel", odberatelRow.Isodb_DodavatelNull() ? (object)DBNull.Value : odberatelRow.odb_Dodavatel);

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

        #region IOdberatele2_ImportOdberatel Members

        public string ImportOdberatel()
        {

            //Datasets.DatabasePohodaTableAdapters.ADTableAdapter  ad_ta = null;
            DatabasePohoda.ADDataTable ad_dt = new DatabasePohoda.ADDataTable();
            Odberatele odberatele = new Odberatele();
            Pohoda_DataSets.DatabasePohoda.sCMenyDataTable Menydt = new DatabasePohoda.sCMenyDataTable();

            try
            {
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                //ad_ta = new Datasets.DatabasePohodaTableAdapters.ADTableAdapter();
                //ad_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;


                //so.Write("export zahajen");
                //TODO: vybirat jen nektere sloupce
                //ad_dt = ad_ta.GetData();
                ad_dt = Database.Pohoda.AD_GetData();

                Menydt = Database.Pohoda.sCMeny_GetDataByKod("CZK");

                int? IDMeny = null;

                if ((Menydt != null) && (Menydt.Count > 0))
                {
                    IDMeny = Menydt.First().ID;
                }


                Pohoda_DataSets.OdberateleTableAdapters.CZMST090TableAdapter CZMST_090TableAdapter = new Pohoda_DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
                CZMST_090TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


                CZMST_090TableAdapter.DeleteQuery();


                odberatele = new Odberatele();

                //string odb_id;          //ID as [odb_id]
                //string odb_desc;        //Firma as [odb_desc]
                //string odb_typ;         //0 as [odb_typ]
                //string odb_barcode;     //Cislo as [odb_carcode]
                //string odb_ico;         //ICO as [odb_ico]
                //string mena_id;         //RefCM as [mena_ID]   anebo CZK jak domaci




                //string odb_misto;       //,Obec as [odb_misto]
                //string odb_ulice;       //,Ulice as [odb_ulice]
                //string odb_cisloOr;     //,null as [odb_cisloOr]
                //string odb_psc;         //,PSC as[odb_psc]
                //string odb_dic;         //,DIC as [odb_dic]
                //bool odb_Odberatel;     //,P2 as [odb_Odberatel]
                //bool odb_Dodavatel;     //,P1 as [odb_Dodavatel]


                //so.Write("export polozek z pohody do databaze");

                int odb_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_id"].MaxLength;
                int odb_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_desc"].MaxLength;
                int odb_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_carcode"].MaxLength;
                int odb_ico_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_ico"].MaxLength;
                int mena_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["mena_ID"].MaxLength;

                int odb_misto_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_misto"].MaxLength;       //,Obec as [odb_misto]
                int odb_ulice_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_ulice"].MaxLength;        //,Ulice as [odb_ulice]
                int odb_psc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_psc"].MaxLength;         //,PSC as[odb_psc]
                int odb_dic_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_dic"].MaxLength;          //,DIC as [odb_dic]


                #region OLD
                //int odb_id_MaxLength = odberatele.CZMST090.odb_idColumn.MaxLength;
                //int odb_desc_MaxLength = odberatele.CZMST090.odb_descColumn.MaxLength;
                //int odb_carcode_MaxLength = odberatele.CZMST090.odb_carcodeColumn.MaxLength;
                //int odb_ico_MaxLength = odberatele.CZMST090.odb_icoColumn.MaxLength;
                //int mena_ID_MaxLength = odberatele.CZMST090.odb_idColumn.MaxLength;

                //int odb_misto_MaxLength = odberatele.CZMST090.odb_mistoColumn.MaxLength;       //,Obec as [odb_misto]
                //int odb_ulice_MaxLength = odberatele.CZMST090.odb_uliceColumn.MaxLength;        //,Ulice as [odb_ulice]
                //int odb_cisloOr_MaxLength = odberatele.CZMST090.odb_cisloOrColumn.MaxLength;      //,null as [odb_cisloOr]
                //int odb_psc_MaxLength = odberatele.CZMST090.odb_pscColumn.MaxLength;         //,PSC as[odb_psc]
                //int odb_dic_MaxLength = odberatele.CZMST090.odb_dicColumn.MaxLength;          //,DIC as [odb_dic]
                //int odb_Odberatel_MaxLength = odberatele.CZMST090.odb_OdberatelColumn.MaxLength;      //,P2 as [odb_Odberatel]
                //int odb_Dodavatel_MaxLength = odberatele.CZMST090.odb_DodavatelColumn.MaxLength;      //,P1 as [odb_Dodavatel] 
                #endregion


                foreach (var item in ad_dt)
                {

                    var Row = odberatele.CZMST090.NewCZMST090Row();

                    Row.odb_id = item.ID.ToString();
                    Row.odb_desc = item.IsFirmaNull() ? "" : (item.Firma.Length > odb_desc_MaxLength ? item.Firma.Remove(odb_desc_MaxLength) : item.Firma);
                    Row.odb_typ = "0";
                    Row.odb_carcode = item.IsCisloNull() ? "" : (item.Cislo.Length > odb_carcode_MaxLength ? item.Cislo.Remove(odb_carcode_MaxLength) : item.Cislo); 
                    Row.odb_ico = item.IsICONull() ? "" : (item.ICO.Length > odb_ico_MaxLength ? item.ICO.Remove(odb_id_MaxLength) : item.ICO);


                    if (item.IsRefCMNull())
                    {
                        if (IDMeny.HasValue)
                            Row.mena_ID = IDMeny.Value.ToString();
                        else
                            Row.Setmena_IDNull();
                    }
                    else 
                    {
                        if (item.IsRefCMNull())
                            Row.Setmena_IDNull();
                        else
                            Row.mena_ID = item.RefCM.ToString(); 
                    }


                    Row.odb_misto = item.IsObecNull() ? "" : (item.Obec.Length > odb_misto_MaxLength ? item.Obec.Remove(odb_misto_MaxLength) : item.Obec);
                    Row.odb_ulice = item.IsUliceNull() ? "" : (item.Ulice.Length > odb_ulice_MaxLength ? item.Ulice.Remove(odb_ulice_MaxLength) : item.Ulice);
                    Row.Setodb_cisloOrNull();
                    Row.odb_psc = item.IsPSCNull() ? "" : (item.PSC.Length > odb_psc_MaxLength ? item.PSC.Remove(odb_psc_MaxLength) : item.PSC);
                    Row.odb_dic = item.IsDICNull() ? "" : (item.DIC.Length > odb_dic_MaxLength ? item.DIC.Remove(odb_dic_MaxLength) : item.DIC); ;


                    if (item.IsP2Null())
                        Row.Setodb_OdberatelNull();
                    else
                        Row.odb_Odberatel = item.P2;

                    if (item.IsP1Null())
                        Row.Setodb_DodavatelNull();
                    else
                        Row.odb_Dodavatel = item.P1;


                    odberatele.CZMST090.AddCZMST090Row(Row);
                }



                CZMST_090TableAdapter.Update(odberatele.CZMST090);

                //so.Write("export se provedl uspesne");
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                Fask.Logging.ExceptionHandler2.Handle(ad_dt);
                Fask.Logging.ExceptionHandler2.Handle(odberatele);

                return ex.Message;

            }
            return "OK";
        }


        #endregion

       

        #region IOdberatele2_GetFiltrovaneOdberatele Members

        public Fask.Interfaces.DataSets.Odberatele GetFiltrovaneOdberatele(Fask.Interfaces.Filtry.AdresarListFiltr filter)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Odberatele dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                string comnd = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST090;

                comnd += " where 1=1";


                if (!string.IsNullOrEmpty(filter.carcode))
                {
                    comnd += " AND odb_carcode='" + filter.carcode.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(filter.desc))
                {
                    comnd += " AND odb_desc like '" + filter.desc.Trim() + "%' ";
                }

                if (!string.IsNullOrEmpty(filter.id))
                {
                    comnd += " AND odb_id=" + filter.id.Trim();
                }
                if (!string.IsNullOrEmpty(filter.typ))
                {
                    comnd += " AND odb_typ='" + filter.typ.Trim() + "' ";
                }

                command.CommandText = comnd;
                adapter.SelectCommand = command;
                adapter.Fill(dsOdberatele.CZMST090);

                return dsOdberatele;
            }
            catch
            {
                throw;
            }
        }

        public List<Tuple<string, string, bool>> GetOdberateleTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST090 + "'";

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

        #endregion
    }
}
