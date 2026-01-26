using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.Filtry;
using Fask.ModuleSql_API;

namespace Fask.ModuleSql
{
    public partial class Provider : 
        // nove interface ... 
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi,

        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams,

        //Fask.Interfaces.Ciselniky.IUzivatele2,
        //Fask.Interfaces.Ciselniky.IUzivatele2_DeleteUzivatel,
        //Fask.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele,
        //Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID,
        //Fask.Interfaces.Ciselniky.IUzivatele2_GetUzivatele,
        //Fask.Interfaces.Ciselniky.IUzivatele2_InsertUzivatel,
        //Fask.Interfaces.Ciselniky.IUzivatele2_UpdateUzivatel,

        Fask.Interfaces.Ciselniky.Sklady.ISklady2,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_DeleteSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetFiltrovaneSklady,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSkladByID,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_InsertSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_UpdateSklad,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_ImportSklad,

        Fask.Interfaces.Ciselniky.Strediska.IStrediska2,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_DeleteStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediska,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_UpdateStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska,

        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_DeleteSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_UpdateSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_ImportSkladLokace_Mapa,

        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_DeletePracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetFiltrovanePracovniky,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_InsertPracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_UpdatePracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_SynchronizacePracovniciAD,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_ImportPracovnici,

        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_DeleteOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatelByID,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetOdberatele,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_InsertOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_UpdateOdberatel,
        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_ImportOdberatel,

        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy,

        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_DeleteSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetFiltrovanySkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row,
        Fask.Interfaces.Ciselniky.SkladLokace_LVS.ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment,

        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_GetFiltrovaneData,

        Fask.Interfaces.Ciselniky.Lokace.ILokace2,
        Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill,

        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetFiltrovaneStrediska,

        Fask.Interfaces.Ciselniky.Odberatele.IOdberatele2_GetFiltrovaneOdberatele,


        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetFiltrovaneSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_DeleteSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_UpdateSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_ImportSkladLokace,
        Fask.Interfaces.Vazby.IVazby2_ImportVyrobky


    {

        #region IUzivatele2

        //#region IUzivatele2_DeleteUzivatel Members

        //public bool DeleteUzivatel(string id)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        connection.Open();

        //        SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
        //        ta_uzivatele.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_uzivatele.MyTransaction = trans;

        //        ta_uzivatele.Delete(Convert.ToInt32(id));

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

        //#endregion

        //#region IUzivatele2_GetFiltrovaneUzivatele Members

        //public Fask.Interfaces.DataSets.Uzivatele GetFiltrovaneUzivatele(Fask.Interfaces.Filtry.UzivateleListFiltr filtr)
        //{
        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;
        //    Fask.Interfaces.DataSets.Uzivatele ds = new Fask.Interfaces.DataSets.Uzivatele();

        //    try
        //    {
        //        adapter = new System.Data.SqlClient.SqlDataAdapter();
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        command = new System.Data.SqlClient.SqlCommand();
        //        command.Connection = connection;

        //        command.CommandText =
        //            "select * from " + Fask.SQL.Constants.Common.TABLE_CZMSTPWD + " uzivatele " +
        //            "where " +
        //            "1=1 "
        //            ;

        //        if (!string.IsNullOrEmpty(filtr.UserID))
        //        {
        //            command.CommandText += "and uzivatele.ID=@id ";
        //            command.Parameters.AddWithValue("@id", filtr.UserID);
        //        }

        //        if (!string.IsNullOrEmpty(filtr.UserLogin))
        //        {
        //            command.CommandText += "and uzivatele.LOGIN=@login ";
        //            command.Parameters.AddWithValue("@login", filtr.UserLogin);
        //        }

        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds.CZMSTPWD);

        //        return ds;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //#endregion

        //#region IUzivatele2_GetUzivatelByID Members

        //public Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow GetUzivatelByID(int id)
        //{
        //    try
        //    {
        //        Fask.Interfaces.Filtry.UzivateleListFiltr filtr = new Fask.Interfaces.Filtry.UzivateleListFiltr();
        //        filtr.UserID = id.ToString();

        //        Fask.Interfaces.DataSets.Uzivatele ds = GetFiltrovaneUzivatele(filtr);

        //        if (ds.CZMSTPWD.Count > 0)
        //            return ds.CZMSTPWD.First();
        //        else
        //            return null;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //#endregion

        //#region IUzivatele2_GetUzivatele Members

        //public Fask.Interfaces.DataSets.Uzivatele GetUzivatele()
        //{
        //    try
        //    {
        //        Fask.Interfaces.Filtry.UzivateleListFiltr filtr = new Fask.Interfaces.Filtry.UzivateleListFiltr();
        //        return GetFiltrovaneUzivatele(filtr);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //#endregion

        //#region IUzivatele2_InsertUzivatel Members

        //public bool InsertUzivatel(Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        connection.Open();

        //        SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
        //        ta_uzivatele.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_uzivatele.MyTransaction = trans;

        //        ta_uzivatele.Insert(
        //            uzivatelRow.LOGIN,
        //            uzivatelRow.PASSWD,
        //            uzivatelRow.ID,
        //            uzivatelRow.IsADMNull() ? (short?)null : uzivatelRow.ADM,
        //            uzivatelRow.FIRSTNAME,
        //            uzivatelRow.SECONDNAME,
        //            uzivatelRow.IsHASHNull() ? string.Empty : uzivatelRow.HASH,
        //            uzivatelRow.IsEANNull() ? string.Empty : uzivatelRow.EAN,
        //            uzivatelRow.CODE
        //            );


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

        //#endregion

        //#region IUzivatele2_UpdateUzivatel Members

        //public bool UpdateUzivatel(Fask.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        connection.Open();

        //        SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new SQL_Datasets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
        //        ta_uzivatele.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_uzivatele.MyTransaction = trans;

        //        ta_uzivatele.Update(
        //            uzivatelRow.LOGIN,
        //            uzivatelRow.PASSWD,
        //            uzivatelRow.IsADMNull() ? (short?)null : uzivatelRow.ADM,
        //            uzivatelRow.FIRSTNAME,
        //            uzivatelRow.SECONDNAME,
        //            uzivatelRow.IsHASHNull() ? string.Empty : uzivatelRow.HASH,
        //            uzivatelRow.IsEANNull() ? string.Empty : uzivatelRow.EAN,
        //            uzivatelRow.CODE,
        //            uzivatelRow.ID
        //            );


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

        //#endregion


        #endregion

        #region IZbozi2

        /// <summary>
        /// Metoda pro smazani řadku z FASK_ZBOZI a FASK_ZBOZI_PARAMETRY 
        /// </summary>
        /// <param name="id_ZBOZI">ID řadku v FASK_ZBOZI</param>
        /// <param name="ID_Params">ID řadku v FASK_ZBOZI_PARAMETRY</param>
        /// <returns>True-OK, False- chyba</returns>
        public bool DeleteZbozi(int id_ZBOZI, int? ID_Params)
        {
            #region Old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{

            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    Fask.ModuleSql.SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_FASK_ZASOBY = new Fask.ModuleSql.SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
            //    ta_FASK_ZASOBY.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_FASK_ZASOBY.MyTransaction = trans;
            //    ta_FASK_ZASOBY.Delete(id_ZBOZI);

            //    if (ID_Params.HasValue)
            //    {

            //        Fask.ModuleSql.SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_FASK_ZASOBYParam = new Fask.ModuleSql.SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
            //        ta_FASK_ZASOBYParam.Connection = connection;
            //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //        ta_FASK_ZASOBYParam.MyTransaction = trans;
            //        ta_FASK_ZASOBYParam.Delete(ID_Params.Value);
            //    }



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
            #endregion

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY; 
                    comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";
                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }

                if (ID_Params.HasValue)
                {
                    using (SqlCommand comm = connection.CreateCommand())
                    {
                        comm.Transaction = trans;
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY;
                        comm.CommandText += " WHERE (DEX_ROW_ID = '" + ID_Params + "')";
                        comm.ExecuteNonQuery();

                        if (trans != null)
                            trans.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Metoda která podle zadaných filtrú dotahne FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY
        /// </summary>
        /// <param name="filtr">Filtr s podminkama</param>
        /// <returns>Dataset Zbozi naplnen zbožím</returns>
        public Fask.Interfaces.DataSets.Zbozi GetFiltrovaneZbozi(Fask.Interfaces.Filtry.ZboziListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = " SELECT Z.DEX_ROW_ID as DEX_ROW_ID_Zbozi ,Z.ITEMNMBR ,Z.ITEMDESC ,Z.ITEMCODE ,Z.VNDITNUM ,Z.CZ_CarKod ,Z.LOCNCODE ,Z.SKL_ID " +
                " ,Z.QTY ,Z.QTYPACK ,Z.MJ ,Z.DMJ ,Z.TAXRATE ,Z.PRICE0 ,Z.PRICE1 ,Z.PRICE2 ,Z.PRICE3 ,Z.PRICE4 ,Z.PRICE5 ,Z.CZ_SerNum_Track " +
                " ,Z.CZ_SerNum_Delka ,Z.CZ_Rez1_Track ,Z.CZ_Rez2_Track ,Z.CZ_Rez3_Track ,Z.CZ_Rez4_Track ,Z.REZ1 ,Z.REZ2 ,Z.REZ3 ,Z.REZ4 ,Z.ODB_ID ,Z.mena_ID ,Z.SERLTNUM ,Z.WEIGHT ,Z.TIMEFROM ,Z.TIMETO ,Z.LSTMod ,Z.loginid " +
                " ,P.DEX_ROW_ID as DEX_ROW_ID_PARAMETRY ,P.VPrFVTS ,P.VPrFPTS ,P.VPrFDTS ,P.VPrFITS ,P.VPrFXTS ,P.RefVPrFVTS ,P.RefVPrFPTS ,P.RefVPrFDTS ,P.RefVPrFITS ,P.RefVPrFXTS ,P.VPrTIMEPREP ,P.VPrTIMEUNIT ,P.RefVPrTIMEMODE " +
                " ,S.skl_desc as SKL_DESC" +
                " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z " +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY + " as P ON Z.ITEMNMBR = P.ITEMNMBR" +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as S ON S.skl_id = Z.SKL_ID";


                command.CommandText += " where 1=1 ";

                if (!string.IsNullOrEmpty(filtr.MaterialID))
                {
                    command.CommandText += "AND Z.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "AND Z.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "AND Z.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "AND (Z.CZ_CarKod=@barcode or Z.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (!string.IsNullOrEmpty(filtr.SKL_ID))
                {
                    command.CommandText += "AND (Z.SKL_ID=@SKL_ID) ";
                    command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID);
                }

                if (filtr.ZobrazitDuplicitniCaroveKody)
                {
                    command.CommandText +=
                        "AND Z.VNDITNUM IN ( " +
                        "   SELECT VNDITNUM " +
                        "   FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " " +
                        "   where VNDITNUM <> '' " +
                        "   group by VNDITNUM " +
                        "   HAVING COUNT(*) > 1" +
                        ") "
                        ;
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_ZASOBY_ALL_KONZOLA);

                return ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Fask.Logging.ExceptionHandler2.Handle(ds);
                throw ex;
            }
        }

        /// <summary>
        /// Metoda která vratí všechno zboži z tabulky FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY 
        /// </summary>
        /// <returns>Dataset Zbozi naplnen zbožím</returns>
        public Fask.Interfaces.DataSets.Zbozi GetZbozi()
        {
            try
            {
                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                return GetFiltrovaneZbozi(filtr);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Metoda která vratí jeden řadek zboži z tabulky FASK_ZBOZI left join FASK_ZBOZI_PARAMETRY
        /// </summary>
        /// <param name="id">ID Zboží</param>
        /// <returns>DataRow jeden řadek zboží</returns>
        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow GetZboziByID(string id)
        {

            try
            {

                if (string.IsNullOrEmpty(id))
                    return null;

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                filtr.MaterialID = id;
                Fask.Interfaces.DataSets.Zbozi ds = GetFiltrovaneZbozi(filtr);



                if ((ds != null) && (ds.FASK_ZASOBY_ALL_KONZOLA.Count > 0))
                {
                    return ds.FASK_ZASOBY_ALL_KONZOLA.First();
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Metoda pro Insert zboží do FASK_ZASOBY
        /// </summary>
        /// <param name="zboziRow">řadek co se vloží</param>
        /// <returns></returns>
        public bool InsertZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + "([ITEMNMBR], [ITEMDESC], [ITEMCODE], [VNDITNUM], [CZ_CarKod], [LOCNCODE], [SKL_ID], [QTY], [QTYPACK], [MJ], [DMJ], [TAXRATE], [PRICE0], [PRICE1], [PRICE2], [PRICE3], [PRICE4], [PRICE5], [CZ_SerNum_Track], [CZ_SerNum_Delka], [CZ_Rez1_Track], [CZ_Rez2_Track], [CZ_Rez3_Track], [CZ_Rez4_Track], [REZ1], [REZ2], [REZ3], [REZ4], [ODB_ID], [mena_ID], [SERLTNUM], [WEIGHT], [TIMEFROM], [TIMETO], [LSTMod], [loginid]" +
                        " ) VALUES (" +
                        " @ITEMNMBR, @ITEMDESC, @ITEMCODE, " +
                        " @VNDITNUM, @CZ_CarKod, @LOCNCODE, " +
                        " @SKL_ID, @QTY, @QTYPACK, " +
                        " @MJ, @DMJ, @TAXRATE, " +
                        " @PRICE0, @PRICE1, @PRICE2, " +
                        " @PRICE3, @PRICE4, @PRICE5, " +
                        " @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_Rez1_Track, " +
                        " @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track, " +
                        " @REZ1, @REZ2, @REZ3, " +
                        " @REZ4, @ODB_ID, @mena_ID, " +
                        " @SERLTNUM, @WEIGHT, @TIMEFROM, " +
                        " @TIMETO, @LSTMod, @loginid)";


                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMDESCNull() ? (object)DBNull.Value : zboziRow.ITEMDESC });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMCODENull() ? (object)DBNull.Value : zboziRow.ITEMCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsVNDITNUMNull() ? (object)DBNull.Value : zboziRow.VNDITNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = zboziRow.IsCZ_CarKodNull() ? (object)DBNull.Value : zboziRow.CZ_CarKod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsSKL_IDNull() ? (object)DBNull.Value : zboziRow.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, Value = zboziRow.QTY});
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsQTYPACKNull() ? (object)DBNull.Value : zboziRow.QTYPACK });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.MJ) ? string.Empty : zboziRow.MJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.DMJ) ? string.Empty : zboziRow.DMJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsTAXRATENull() ? (object)DBNull.Value : zboziRow.TAXRATE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE0Null() ? (object)DBNull.Value : zboziRow.PRICE0 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE1Null() ? (object)DBNull.Value : zboziRow.PRICE1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE2Null() ? (object)DBNull.Value : zboziRow.PRICE2 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE3Null() ? (object)DBNull.Value : zboziRow.PRICE3 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE4Null() ? (object)DBNull.Value : zboziRow.PRICE4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE5Null() ? (object)DBNull.Value : zboziRow.PRICE5 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_SerNum_Track }); // not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, Value  = zboziRow.CZ_SerNum_Delka });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, Value  = zboziRow.CZ_Rez1_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, Value  = zboziRow.CZ_Rez2_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez3_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, Value  = zboziRow.CZ_Rez4_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ1Null() ? (object)DBNull.Value : zboziRow.REZ1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ2Null() ? (object)DBNull.Value : zboziRow.REZ2 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ3Null() ? (object)DBNull.Value : zboziRow.REZ3 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ4Null() ? (object)DBNull.Value : zboziRow.REZ4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsODB_IDNull() ? (object)DBNull.Value : zboziRow.ODB_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = zboziRow.Ismena_IDNull() ? (object)DBNull.Value : zboziRow.mena_ID });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsSERLTNUMNull() ? (object)DBNull.Value : zboziRow.SERLTNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsWEIGHTNull() ? (object)DBNull.Value : zboziRow.WEIGHT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMEFROMNull() ? (object)DBNull.Value : zboziRow.TIMEFROM });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMETONull() ? (object)DBNull.Value : zboziRow.TIMETO });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsLSTModNull() ? (object)DBNull.Value : zboziRow.LSTMod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = zboziRow.IsloginidNull() ? (object)DBNull.Value : zboziRow.loginid });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }


            #region old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_Zbozi = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
            //    ta_Zbozi.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_Zbozi.MyTransaction = trans;

            //    ta_Zbozi.Insert(
            //        zboziRow.ITEMNMBR,
            //        zboziRow.IsITEMDESCNull() ? null : zboziRow.ITEMDESC,
            //        zboziRow.IsITEMCODENull() ? null : zboziRow.ITEMCODE,
            //        zboziRow.IsVNDITNUMNull() ? null : zboziRow.VNDITNUM,
            //        zboziRow.IsCZ_CarKodNull() ? null : zboziRow.CZ_CarKod,
            //        zboziRow.IsLOCNCODENull() ? null : zboziRow.LOCNCODE,
            //        zboziRow.IsSKL_IDNull() ? null : zboziRow.SKL_ID,
            //        zboziRow.QTY,
            //        zboziRow.IsQTYPACKNull() ? (decimal?)null : zboziRow.QTYPACK,
            //        zboziRow.MJ,
            //        zboziRow.DMJ,
            //        zboziRow.IsTAXRATENull() ? (decimal?)null : zboziRow.TAXRATE,
            //        zboziRow.IsPRICE0Null() ? (decimal?)null : zboziRow.PRICE0,
            //        zboziRow.IsPRICE1Null() ? (decimal?)null : zboziRow.PRICE1,
            //        zboziRow.IsPRICE2Null() ? (decimal?)null : zboziRow.PRICE2,
            //        zboziRow.IsPRICE3Null() ? (decimal?)null : zboziRow.PRICE3,
            //        zboziRow.IsPRICE4Null() ? (decimal?)null : zboziRow.PRICE4,
            //        zboziRow.IsPRICE5Null() ? (decimal?)null : zboziRow.PRICE5,
            //        zboziRow.CZ_SerNum_Track,
            //        zboziRow.CZ_SerNum_Delka,
            //        zboziRow.CZ_Rez1_Track,
            //        zboziRow.CZ_Rez2_Track,
            //        zboziRow.CZ_Rez3_Track,
            //        zboziRow.CZ_Rez4_Track,
            //        zboziRow.IsREZ1Null() ? null : zboziRow.REZ1,
            //        zboziRow.IsREZ2Null() ? null : zboziRow.REZ2,
            //        zboziRow.IsREZ3Null() ? null : zboziRow.REZ3,
            //        zboziRow.IsREZ4Null() ? null : zboziRow.REZ4,
            //        zboziRow.IsODB_IDNull() ? null : zboziRow.ODB_ID,
            //        zboziRow.Ismena_IDNull() ? null : zboziRow.mena_ID,
            //        zboziRow.IsSERLTNUMNull() ? null : zboziRow.SERLTNUM,
            //        zboziRow.IsWEIGHTNull() ? (decimal?)null : zboziRow.WEIGHT,
            //        zboziRow.IsTIMEFROMNull() ? (DateTime?)null : zboziRow.TIMEFROM,
            //        zboziRow.IsTIMETONull() ? (DateTime?)null : zboziRow.TIMETO,
            //        zboziRow.IsLSTModNull() ? (DateTime?)null : zboziRow.LSTMod,
            //        zboziRow.IsloginidNull() ? null : zboziRow.loginid
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
            #endregion
        }

        /// <summary>
        /// Metoda pro update pouze FASK_ZBOZI
        /// </summary>
        /// <param name="zboziRow">Radek pro Update</param>
        /// <returns>True-OK, False- chyba</returns>
        public bool UpdateZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " SET" +
                        "  ITEMNMBR = @ITEMNMBR, ITEMDESC = @ITEMDESC, ITEMCODE = @ITEMCODE," +
                        " VNDITNUM = @VNDITNUM, LOCNCODE = @LOCNCODE, CZ_CarKod = @CZ_CarKod," +
                        " SKL_ID = @SKL_ID, QTY = @QTY, QTYPACK = @QTYPACK," +
                        " MJ = @MJ, DMJ = @DMJ, TAXRATE = @TAXRATE," +
                        " PRICE0 = @PRICE0, PRICE1 = @PRICE1, PRICE2 = @PRICE2," +
                        " PRICE3 = @PRICE3, PRICE4 = @PRICE4, PRICE5 = @PRICE5, " +
                        "CZ_SerNum_Track = @CZ_SerNum_Track, CZ_SerNum_Delka = @CZ_SerNum_Delka, CZ_Rez1_Track = @CZ_Rez1_Track," +
                        " CZ_Rez2_Track = @CZ_Rez2_Track, CZ_Rez3_Track = @CZ_Rez3_Track, CZ_Rez4_Track = @CZ_Rez4_Track," +
                        " REZ1 = @REZ1, REZ2 = @REZ2, REZ3 = @REZ3," +
                        " REZ4 = @REZ4, ODB_ID = @ODB_ID, mena_ID = @mena_ID," +
                        " SERLTNUM = @SERLTNUM, WEIGHT = @WEIGHT, TIMEFROM = @TIMEFROM," +
                        " TIMETO = @TIMETO, LSTMod = @LSTMod, loginid = @loginid" +
                        " WHERE (DEX_ROW_ID = @DEX_ROW_ID)";



                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMDESCNull() ? (object)DBNull.Value : zboziRow.ITEMDESC });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMCODENull() ? (object)DBNull.Value : zboziRow.ITEMCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsVNDITNUMNull() ? (object)DBNull.Value : zboziRow.VNDITNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = zboziRow.IsCZ_CarKodNull() ? (object)DBNull.Value : zboziRow.CZ_CarKod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsSKL_IDNull() ? (object)DBNull.Value : zboziRow.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, Value = zboziRow.QTY });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsQTYPACKNull() ? (object)DBNull.Value : zboziRow.QTYPACK });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.MJ) ? string.Empty : zboziRow.MJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.DMJ) ? string.Empty : zboziRow.DMJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsTAXRATENull() ? (object)DBNull.Value : zboziRow.TAXRATE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE0Null() ? (object)DBNull.Value : zboziRow.PRICE0 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE1Null() ? (object)DBNull.Value : zboziRow.PRICE1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE2Null() ? (object)DBNull.Value : zboziRow.PRICE2 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE3Null() ? (object)DBNull.Value : zboziRow.PRICE3 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE4Null() ? (object)DBNull.Value : zboziRow.PRICE4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE5Null() ? (object)DBNull.Value : zboziRow.PRICE5 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_SerNum_Track }); // not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, Value = zboziRow.CZ_SerNum_Delka });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez1_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez2_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez3_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez4_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ1Null() ? (object)DBNull.Value : zboziRow.REZ1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ2Null() ? (object)DBNull.Value : zboziRow.REZ2 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ3Null() ? (object)DBNull.Value : zboziRow.REZ3 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ4Null() ? (object)DBNull.Value : zboziRow.REZ4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsODB_IDNull() ? (object)DBNull.Value : zboziRow.ODB_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = zboziRow.Ismena_IDNull() ? (object)DBNull.Value : zboziRow.mena_ID });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsSERLTNUMNull() ? (object)DBNull.Value : zboziRow.SERLTNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsWEIGHTNull() ? (object)DBNull.Value : zboziRow.WEIGHT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMEFROMNull() ? (object)DBNull.Value : zboziRow.TIMEFROM });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMETONull() ? (object)DBNull.Value : zboziRow.TIMETO });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsLSTModNull() ? (object)DBNull.Value : zboziRow.LSTMod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = zboziRow.IsloginidNull() ? (object)DBNull.Value : zboziRow.loginid });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value =  zboziRow.DEX_ROW_ID_Zbozi });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }

            #region old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_zbozi = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
            //    ta_zbozi.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_zbozi.MyTransaction = trans;

            //    ta_zbozi.Update(
            //    zboziRow.ITEMNMBR,
            //    zboziRow.IsITEMDESCNull() ? null : zboziRow.ITEMDESC,
            //    zboziRow.IsITEMCODENull() ? null : zboziRow.ITEMCODE,
            //    zboziRow.IsVNDITNUMNull() ? null : zboziRow.VNDITNUM,
            //    zboziRow.IsLOCNCODENull() ? null : zboziRow.LOCNCODE,
            //    zboziRow.IsCZ_CarKodNull() ? null : zboziRow.CZ_CarKod,
            //    zboziRow.IsSKL_IDNull() ? null : zboziRow.SKL_ID,
            //    zboziRow.QTY,
            //    zboziRow.IsQTYPACKNull() ? (decimal?)null : zboziRow.QTYPACK,
            //    zboziRow.MJ,
            //    zboziRow.DMJ,
            //    zboziRow.IsTAXRATENull() ? (decimal?)null : zboziRow.TAXRATE,
            //    zboziRow.IsPRICE0Null() ? (decimal?)null : zboziRow.PRICE0,
            //    zboziRow.IsPRICE1Null() ? (decimal?)null : zboziRow.PRICE1,
            //    zboziRow.IsPRICE2Null() ? (decimal?)null : zboziRow.PRICE2,
            //    zboziRow.IsPRICE3Null() ? (decimal?)null : zboziRow.PRICE3,
            //    zboziRow.IsPRICE4Null() ? (decimal?)null : zboziRow.PRICE4,
            //    zboziRow.IsPRICE5Null() ? (decimal?)null : zboziRow.PRICE5,
            //    zboziRow.CZ_SerNum_Track,
            //    zboziRow.CZ_SerNum_Delka,
            //    zboziRow.CZ_Rez1_Track,
            //    zboziRow.CZ_Rez2_Track,
            //    zboziRow.CZ_Rez3_Track,
            //    zboziRow.CZ_Rez4_Track,
            //    zboziRow.IsREZ1Null() ? null : zboziRow.REZ1,
            //    zboziRow.IsREZ2Null() ? null : zboziRow.REZ2,
            //    zboziRow.IsREZ3Null() ? null : zboziRow.REZ3,
            //    zboziRow.IsREZ4Null() ? null : zboziRow.REZ4,
            //    zboziRow.IsODB_IDNull() ? null : zboziRow.ODB_ID,
            //    zboziRow.Ismena_IDNull() ? null : zboziRow.mena_ID,
            //    zboziRow.IsSERLTNUMNull() ? null : zboziRow.SERLTNUM,
            //    zboziRow.IsWEIGHTNull() ? (decimal?)null : zboziRow.WEIGHT,
            //    zboziRow.IsTIMEFROMNull() ? (DateTime?)null : zboziRow.TIMEFROM,
            //    zboziRow.IsTIMETONull() ? (DateTime?)null : zboziRow.TIMETO,
            //    zboziRow.IsLSTModNull() ? (DateTime?)null : zboziRow.LSTMod,
            //    zboziRow.IsloginidNull() ? null : zboziRow.loginid,
            //    zboziRow.DEX_ROW_ID_Zbozi
            //    );


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
            #endregion
        }

        #region Parametry

        /// <summary>
        /// Metoda pro inser do FASK_ZASOBY_PARAMETRY
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        public bool InsertZboziParams(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                        " ([ITEMNMBR], [VPrFVTS], [VPrFPTS]," +
                        " [VPrFDTS], [VPrFITS], [VPrFXTS]," +
                        " [RefVPrFVTS], [RefVPrFPTS], [RefVPrFDTS]," +
                        " [RefVPrFITS], [RefVPrFXTS], [VPrTIMEPREP]," +
                        " [VPrTIMEUNIT], [RefVPrTIMEMODE])" +
                        " VALUES (@ITEMNMBR, @VPrFVTS, @VPrFPTS," +
                        " @VPrFDTS, @VPrFITS, @VPrFXTS," +
                        " @RefVPrFVTS, @RefVPrFPTS, @RefVPrFDTS," +
                        " @RefVPrFITS, @RefVPrFXTS, @VPrTIMEPREP," +
                        " @VPrTIMEUNIT, @RefVPrTIMEMODE)";

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }

            #region old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{

            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_Zbozi = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
            //    ta_Zbozi.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_Zbozi.MyTransaction = trans;

            //    ta_Zbozi.Insert(
            //        zboziRow.ITEMNMBR,
            //        zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
            //        zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
            //        zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
            //        zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
            //        zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
            //        zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
            //        zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
            //        zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
            //        zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
            //        zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
            //        zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
            //        zboziRow.IsVPrTIMEUNITNull() ? (double?)null : zboziRow.VPrTIMEUNIT,
            //        zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE
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
                #endregion
            }


        /// <summary>
        /// Metoda pro Inser/Update Parametru...
        /// </summary>
        /// <param name="zboziRow"></param>
        /// <returns></returns>
        public bool UpdateZboziParams(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            if (GetParametrybyID(zboziRow.ITEMNMBR))
            {
                return UpdateParametry(zboziRow);
            }
            else
            {
                System.Data.SqlClient.SqlTransaction trans = null;
                System.Data.SqlClient.SqlConnection connection = null;

                try
                {
                    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                    connection.Open();
                    trans = connection.BeginTransaction();

                    using (SqlCommand comm = connection.CreateCommand())
                    {
                        comm.Transaction = trans;
                        //comm.CommandType = System.Data.CommandType.Text;
                        //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                        //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                        comm.CommandType = global::System.Data.CommandType.Text;
                        comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                            " ([ITEMNMBR], [VPrFVTS], [VPrFPTS]," +
                            " [VPrFDTS], [VPrFITS], [VPrFXTS]," +
                            " [RefVPrFVTS], [RefVPrFPTS], [RefVPrFDTS]," +
                            " [RefVPrFITS], [RefVPrFXTS], [VPrTIMEPREP]," +
                            " [VPrTIMEUNIT], [RefVPrTIMEMODE])" +
                            " VALUES (@ITEMNMBR, @VPrFVTS, @VPrFPTS," +
                            " @VPrFDTS, @VPrFITS, @VPrFXTS," +
                            " @RefVPrFVTS, @RefVPrFPTS, @RefVPrFDTS," +
                            " @RefVPrFITS, @RefVPrFXTS, @VPrTIMEPREP," +
                            " @VPrTIMEUNIT, @RefVPrTIMEMODE)";

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                        comm.ExecuteNonQuery();

                        if (trans != null)
                            trans.Commit();
                    }


                    return true;
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(ex);

                    try
                    {
                        if (trans != null)
                            trans.Rollback();
                    }
                    catch
                    { }
                    throw;

                }
                finally
                {
                    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                        connection.Close();
                }

                #region old
                //System.Data.SqlClient.SqlTransaction trans = null;
                //System.Data.SqlClient.SqlConnection connection = null;

                //try
                //{

                //connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                //connection.Open();

                //SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_Zbozi = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                //ta_Zbozi.Connection = connection;
                //trans = connection.BeginTransaction(IsolationLevel.Serializable);
                //ta_Zbozi.MyTransaction = trans;

                //ta_Zbozi.Insert(
                //    zboziRow.ITEMNMBR,
                //    zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
                //    zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
                //    zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
                //    zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
                //    zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
                //    zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
                //    zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
                //    zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
                //    zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
                //    zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
                //    zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
                //    zboziRow.IsVPrTIMEUNITNull() ? (double?)null : zboziRow.VPrTIMEUNIT,
                //    zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE
                //    );

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
                #endregion
            }
        }


        private bool UpdateParametry(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                        " SET ITEMNMBR = @ITEMNMBR, VPrFVTS = @VPrFVTS, VPrFPTS = @VPrFPTS," +
                        " VPrFDTS = @VPrFDTS, VPrFITS = @VPrFITS, VPrFXTS = @VPrFXTS," +
                        " RefVPrFVTS = @RefVPrFVTS, RefVPrFPTS = @RefVPrFPTS, RefVPrFDTS = @RefVPrFDTS," +
                        " RefVPrFITS = @RefVPrFITS, RefVPrFXTS = @RefVPrFXTS, VPrTIMEPREP = @VPrTIMEPREP," +
                        " VPrTIMEUNIT = @VPrTIMEUNIT, RefVPrTIMEMODE = @RefVPrTIMEMODE" +
                        " WHERE (DEX_ROW_ID = @DEX_ROW_ID)";

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value = zboziRow.DEX_ROW_ID_PARAMETRY });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }

            #region old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{

            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_zbozi = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
            //ta_zbozi.Connection = connection;
            //trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //ta_zbozi.MyTransaction = trans;


            //ta_zbozi.Update(
            //    zboziRow.ITEMNMBR,
            //    zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
            //    zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
            //    zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
            //    zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
            //    zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
            //    zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
            //    zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
            //    zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
            //    zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
            //    zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
            //    zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
            //    zboziRow.IsVPrTIMEUNITNull() ? (double?)null : zboziRow.VPrTIMEUNIT,
            //    zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE,
            //    zboziRow.DEX_ROW_ID_PARAMETRY
            //    );


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
            #endregion
        }

        private bool GetParametrybyID(string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            try
            {
                
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY;

                command.CommandText += " WHERE ITEMNMBR = " + ITEMNMBR;

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_ZASOBY_PARAMETRY_KONZOLA);

                if ((ds != null) && (ds.FASK_ZASOBY_PARAMETRY_KONZOLA != null) && (ds.FASK_ZASOBY_PARAMETRY_KONZOLA.Count > 0))
                {
                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Fask.Logging.ExceptionHandler2.Handle(ds);
                throw ex;
            }
        }


        #endregion

        #region Import Zbozi

        public string ImportZbozi()
        {
            ExportKatalogZasoby_Procedura();



            return "OK";
        }

        private string ExportKatalogZasoby_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_ZASOBY");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;

                adpacommand.Parameters.Add((new SqlParameter("@ExportTypFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportSkladFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportovatPouzeAktivniPolozky", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EXZas_DotahovatAlternativniDodavatele", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceSarzi", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceVyrobnichCisel", SqlDbType.Bit)));

                //((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky;
                //((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele;
                //((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi;
                //((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel;

                ((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = "";
                ((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = "";
                ((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = true;



                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }



        #endregion

        #endregion

        #region ISklady2

        #region ISklady2_DeleteSklad Members

        public bool DeleteSklad(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter();

                ta_sklady.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_sklady.MyTransaction = trans;

                ta_sklady.Delete(id);

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

        #region ISklady2_GetFiltrovaneSklady Members

        public Fask.Interfaces.DataSets.Sklady GetFiltrovaneSklady(Fask.Interfaces.Filtry.SkladyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISklady2_GetSkladByID Members

        public Fask.Interfaces.DataSets.Sklady.CZMST093Row GetSkladByID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " " +
                    "where " +
                    "skl_id=@skl_id";
                command.Parameters.AddWithValue("@skl_id", id);
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST093);

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
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();
            Database.Ciselnik cis = new Database.Ciselnik();
            cis.Fill_Universal(ConnectionString,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093,
                ds.CZMST093
                );
            return ds;
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
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        connection.Open();

        //        SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter();
        //        ta_sklady.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_sklady.MyTransaction = trans;

        //        ta_sklady.Insert(
        //            skladRow.skl_id,
        //            skladRow.Isskl_descNull() ? null : skladRow.skl_desc,
        //            skladRow.Isskl_typNull() ? null : skladRow.skl_typ,
        //            skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode
        //            );

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

        public bool InsertSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

        #endregion

        #region ISklady2_UpdateSklad Members

        #region old MaR 23.10. 2024
        //public bool UpdateSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        //{
        //    System.Data.SqlClient.SqlTransaction trans = null;
        //    System.Data.SqlClient.SqlConnection connection = null;

        //    try
        //    {
        //        connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        connection.Open();

        //        SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = new SQL_Datasets.SkladyTableAdapters.CZMST093TableAdapter();
        //        ta_sklady.Connection = connection;
        //        trans = connection.BeginTransaction(IsolationLevel.Serializable);
        //        ta_sklady.MyTransaction = trans;

        //        //int pocet = ta_okruh.Update(okruhRow);    // nefunguje ...
        //        ta_sklady.Update(
        //            skladRow.Isskl_descNull() ? null : skladRow.skl_desc,
        //            skladRow.Isskl_typNull() ? null : skladRow.skl_typ,
        //            skladRow.Isskl_carcodeNull() ? null : skladRow.skl_carcode,
        //            skladRow.skl_id
        //            );

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

        public bool UpdateSklad(Fask.Interfaces.DataSets.Sklady.CZMST093Row skladRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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


        #endregion

        #region ISklady2_Vyroba_Fill Members

        public void Sklady_Vyroba_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        { 
            Database.Ciselnik cis = new Database.Ciselnik();
            cis.Fill_Universal(ConnectionString,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093,
                ds.CZMST093
                );
        }

        #endregion

        #endregion

        #region IStrediska2

  

        #region IStrediska2_GetStrediska Members

        public Fask.Interfaces.DataSets.Strediska GetStrediska()
        {
            Fask.Interfaces.DataSets.Strediska ds = new Fask.Interfaces.DataSets.Strediska();

            Database.Ciselnik cis = new Database.Ciselnik();
            cis.Fill_Universal(ConnectionString,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST091,
                ds.CZMST091
                );

            return ds;
        }

        #endregion

        #region IStrediska2_InsertStredisko Members

        public bool InsertStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "(str_id, str_desc, str_typ, str_carcode, skl_id, odb_id) " +
                    "VALUES " +
                    "(@str_id, @str_desc, @str_typ, @str_carcode, @skl_id, @odb_id)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@str_id", strediskoRow.str_id);
                command.Parameters.AddWithValue("@str_desc", strediskoRow.Isstr_descNull() ? (object)DBNull.Value : strediskoRow.str_desc);
                command.Parameters.AddWithValue("@str_typ", strediskoRow.Isstr_typNull() ? (object)DBNull.Value : strediskoRow.str_typ);
                command.Parameters.AddWithValue("@str_carcode", strediskoRow.Isstr_carcodeNull() ? (object)DBNull.Value : strediskoRow.str_carcode);
                command.Parameters.AddWithValue("@skl_id", strediskoRow.Isskl_idNull() ? (object)DBNull.Value : strediskoRow.skl_id);
                command.Parameters.AddWithValue("@odb_id", strediskoRow.Isodb_idNull() ? (object)DBNull.Value : strediskoRow.odb_id);


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

        #region IStrediska2_UpdateStredisko Members

        public bool UpdateStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "set str_desc=@str_desc, str_typ=@str_typ, str_carcode=@str_carcode, skl_id=@skl_id, odb_id=@odb_id " +
                    "where str_id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", strediskoRow.str_id);
                command.Parameters.AddWithValue("@str_desc", strediskoRow.Isstr_descNull() ? (object)DBNull.Value : strediskoRow.str_desc);
                command.Parameters.AddWithValue("@str_typ", strediskoRow.Isstr_typNull() ? (object)DBNull.Value : strediskoRow.str_typ);
                command.Parameters.AddWithValue("@str_carcode", strediskoRow.Isstr_carcodeNull() ? (object)DBNull.Value : strediskoRow.str_carcode);
                command.Parameters.AddWithValue("@skl_id", strediskoRow.Isskl_idNull() ? (object)DBNull.Value : strediskoRow.skl_id);
                command.Parameters.AddWithValue("@odb_id", strediskoRow.Isodb_idNull() ? (object)DBNull.Value : strediskoRow.odb_id);

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


        #region IStrediska2_DeleteStredisko Members

        public bool DeleteStredisko(string id)
        {
            //throw new NotImplementedException();

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "where str_id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
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

        #region IStrediska2_GetStrediskoByID Members

        public Fask.Interfaces.DataSets.Strediska.CZMST091Row GetStrediskoByID(string id)
        {
            try
            {
                Fask.Interfaces.DataSets.Strediska dsStr = new Fask.Interfaces.DataSets.Strediska();

                Fask.Interfaces.Filtry.StrediskaListFiltr filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();
                filtr.str_id = id;
                dsStr = GetFiltrovaneStrediska(filtr);

                if (dsStr.CZMST091.Count > 0)
                    return dsStr.CZMST091.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region ISkladLokace_Mapa2

        #region ISkladLokace_Mapa2_DeleteSkladLokace_Mapa Members

        public bool DeleteSkladLokace_Mapa(string skl_id, string locncode)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_Mapa]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " " +
                    "where " +
                    "skl_id=@skl_id and locncode=@locncode";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);

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

        #region ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_Mapa(Fask.Interfaces.Filtry.SkladLokaceMapaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.skl_id))
                {
                    command.CommandText += "and m.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.skl_id);
                }

                //if (!string.IsNullOrEmpty(filtr.skl_id))
                //{
                //    command.CommandText += "and m.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                if (!string.IsNullOrEmpty(filtr.locncode))
                {
                    command.CommandText += "and m.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.locncode);
                }

                command.CommandText += "order by m.skl_id asc, m.locncode asc;";

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_GetSkladLokace_Mapa Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_Mapa()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "order by m.skl_id asc, m.locncode asc ";
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode Members

        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow GetSkladLokace_MapaBySklIDAndLocncode(string skl_id, string locncode)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "where " +
                    "m.SKL_ID=@skl_id AND m.locncode=@locncode";
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                if (ds.CZMST_SkladLokace_Mapa.Count > 0)
                    return ds.CZMST_SkladLokace_Mapa.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_InsertSkladLokace_Mapa Members

        public bool InsertSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter ta_mapa = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter();
                ta_mapa.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_mapa.MyTransaction = trans;

                ta_mapa.Insert(
                    row.IsSKL_IDNull() ? null : row.SKL_ID,
                    row.IsLOCNCODENull() ? null : row.LOCNCODE,
                    row.IsTYPENull() ? null : row.TYPE,
                    row.IsBarcodeNull() ? null : row.Barcode,
                    row.IsDescriptionNull() ? null : row.Description
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

        #region ISkladLokace_Mapa2_UpdateSkladLokace_Mapa Members

        public bool UpdateSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter ta_mapa = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter();
                ta_mapa.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_mapa.MyTransaction = trans;

                ta_mapa.Update(
                    row.IsTYPENull() ? null : row.TYPE,
                    row.IsBarcodeNull() ? null : row.Barcode,
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IsSKL_IDNull() ? null : row.SKL_ID,
                    row.IsLOCNCODENull() ? null : row.LOCNCODE
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

        #endregion

        #region IPracovnici2

        #region IPracovnici2_DeletePracovnici Members

        public bool DeletePracovnici(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Delete(id);

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

        #region IPracovnici2_GetFiltrovanePracovniky Members

        public Fask.Interfaces.DataSets.Pracovnici GetFiltrovanePracovniky(Fask.Interfaces.Filtry.PracovniciListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Pracovnici ds = new Fask.Interfaces.DataSets.Pracovnici();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST096 + " pracovnici " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.Prac_ID))
                {
                    command.CommandText += "and pracovnici.prac_id=@prac_id ";
                    command.Parameters.AddWithValue("@prac_id", filtr.Prac_ID);
                }

                if (!string.IsNullOrEmpty(filtr.Prac_Desc))
                {
                    command.CommandText += "and pracovnici.prac_desc like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.Prac_Desc);
                }



                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST096);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IPracovnici2_GetPracovnici Members

        public Fask.Interfaces.DataSets.Pracovnici GetPracovnici()
        {
            try
            {
                Fask.Interfaces.Filtry.PracovniciListFiltr filtr = new Fask.Interfaces.Filtry.PracovniciListFiltr();
                return GetFiltrovanePracovniky(filtr);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IPracovnici2_GetPracovnikByID Members

        public Fask.Interfaces.DataSets.Pracovnici.CZMST096Row GetPracovnikByID(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return null;

                Fask.Interfaces.Filtry.PracovniciListFiltr filtr = new Fask.Interfaces.Filtry.PracovniciListFiltr();
                filtr.Prac_ID = id;
                Fask.Interfaces.DataSets.Pracovnici ds = GetFiltrovanePracovniky(filtr);

                if (ds.CZMST096.Count > 0)
                    return ds.CZMST096.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IPracovnici2_InsertPracovnici Members

        public bool InsertPracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Insert(
                    PracovniciRow.prac_id,
                    PracovniciRow.prac_desc,
                    PracovniciRow.prac_typ,
                    PracovniciRow.prac_carcode);


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

        #region IPracovnici2_UpdatePracovnici Members

        public bool UpdatePracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new SQL_Datasets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Update(
                    PracovniciRow.prac_id,
                    PracovniciRow.prac_desc,
                    PracovniciRow.prac_carcode,
                    PracovniciRow.prac_typ);


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

        #region IPracovnici2_SynchronizacePracovniciAD Members

        public void SynchronizacePracovniciAD()
        {

            Pracovnici.DeleteAll(ConnectionString);

            using (System.DirectoryServices.AccountManagement.PrincipalContext context = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, Properties.Settings.Default.DomenaAD.Trim()))
            {

                Fask.Interfaces.DataSets.Pracovnici ds1 = new Fask.Interfaces.DataSets.Pracovnici();

                using (System.DirectoryServices.AccountManagement.PrincipalSearcher searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher(new System.DirectoryServices.AccountManagement.UserPrincipal(context)))
                {

                    foreach (System.DirectoryServices.AccountManagement.Principal result in searcher.FindAll())
                    {

                        System.DirectoryServices.DirectoryEntry de = result.GetUnderlyingObject() as System.DirectoryServices.DirectoryEntry;
                        Fask.ModuleSql.Classes.AD.ADUserDetail detrail = Fask.ModuleSql.Classes.AD.ADUserDetail.GetUser(de);


                        if (!string.IsNullOrEmpty(detrail.DISPLAYNAME))
                        {
                            Fask.Interfaces.DataSets.Pracovnici.CZMST096Row row = ds1.CZMST096.NewCZMST096Row();

                            row.prac_id = detrail.ID; // ID  30
                            row.prac_desc = detrail.DISPLAYNAME; // 40
                            row.prac_typ = "-"; // 3
                            row.prac_carcode = detrail.ID; // 21 

                            ds1.CZMST096.AddCZMST096Row(row);

                            InsertPracovnici(row);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region IOdberatele2

        #region IOdberatele2_DeleteOdberatel Members

        public bool DeleteOdberatel(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Odberatele dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " " +
                    "where " +
                    "odb_id=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsOdberatele.CZMST090);

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
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Odberatele dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST090;
                adapter.SelectCommand = command;
                adapter.Fill(dsOdberatele.CZMST090);

                return dsOdberatele;
            }
            catch
            {
                throw;
            }
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
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

            #endregion

        #region ISkladLokace_LokaceTypy2

        #region ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy Members

        public bool DeleteSkladLokace_LokaceTypy(string type)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_LokaceTypy]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY + " " +
                    "where " +
                    "[TYPE]=@type";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@type", type);

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

        #region ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_LokaceTypy(Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY + " typy " +
                    "where " +
                    "1=1 "
                    ;

                // typ lokace
                if (!string.IsNullOrEmpty(filtr.type))
                {
                    command.CommandText += "and typy.[TYPE]=@typ ";
                    command.Parameters.AddWithValue("@typ", filtr.type);
                }

                #region Logika zaškrtavatek

                command.CommandText += " AND ( 1!=1 ";


                // je prijmova lokace
                if (filtr.is_receive)
                {
                    command.CommandText += "OR typy.IS_RECEIVE=@isreceive ";
                    command.Parameters.AddWithValue("@isreceive", filtr.is_receive);
                }

                // je vychozi lokace
                if (filtr.is_default)
                {
                    command.CommandText += "OR typy.IS_DEFAULT=@isdefault ";
                    command.Parameters.AddWithValue("@isdefault", filtr.is_default);

                }

                // je bezna/normalni lokace
                if (filtr.is_normal)
                {
                    command.CommandText += "OR typy.IS_NORMAL=@isnormal ";
                    command.Parameters.AddWithValue("@isnormal", filtr.is_normal);

                }
                command.CommandText += ")";
                #endregion

                
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceTypy()
        {
            try
            {
                Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr = new Fask.Interfaces.Filtry.LokaceTypyListFiltr();
                filtr.is_default = true;
                filtr.is_normal = true;
                filtr.is_receive = true;
                return GetFiltrovaneSkladLokace_LokaceTypy(filtr);
            }
            catch
            {
                throw;
            }

            // 10.5.2016 PeV: predelano na skladani filtru ...
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            //try
            //{
            //    adapter = new System.Data.SqlClient.SqlDataAdapter();
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    command = new System.Data.SqlClient.SqlCommand();
            //    command.Connection = connection;

            //    command.CommandText =
            //        "select * " +
            //        "from " + TABLE_CZMST_SkladLokace_LokaceTypy;
            //    adapter.SelectCommand = command;
            //    adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

            //    return ds;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType Members

        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow GetSkladLokace_LokaceTypyByType(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                    return null;

                Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr = new Fask.Interfaces.Filtry.LokaceTypyListFiltr();
                filtr.type = type;
                filtr.is_default = true;
                filtr.is_normal = true;
                filtr.is_receive = true;
                Fask.Interfaces.DataSets.SkladLokace ds = GetFiltrovaneSkladLokace_LokaceTypy(filtr);
                if (ds.CZMST_SkladLokace_LokaceTypy.Count > 0)
                    return ds.CZMST_SkladLokace_LokaceTypy.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }

            // 10.5.2016 PeV: zakomentovano, zbytecna duplicita kodu
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;

            //try
            //{
            //    Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
            //    adapter = new System.Data.SqlClient.SqlDataAdapter();
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    command = new System.Data.SqlClient.SqlCommand();
            //    command.Connection = connection;
            //    command.CommandText =
            //        "select * " +
            //        "from " + TABLE_CZMST_SkladLokace_LokaceTypy + " " + 
            //        "where " +
            //        "[TYPE]=@type";
            //    command.Parameters.AddWithValue("@type", type);
            //    adapter.SelectCommand = command;
            //    adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

            //    if (ds.CZMST_SkladLokace_LokaceTypy.Count > 0)
            //        return ds.CZMST_SkladLokace_LokaceTypy.First();
            //    else
            //        return null;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy Members

        public bool InsertSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter ta_typy = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
                ta_typy.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_typy.MyTransaction = trans;

                ta_typy.Insert(
                    row.IsTYPENull() ? string.Empty : row.TYPE,
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IS_RECEIVE,
                    row.IS_DEFAULT,
                    row.IS_NORMAL
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

        #region ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy Members

        public bool UpdateSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter ta_typy = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
                ta_typy.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_typy.MyTransaction = trans;

                ta_typy.Update(
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IS_RECEIVE,
                    row.IS_DEFAULT,
                    row.IS_NORMAL,
                    row.IsTYPENull() ? string.Empty : row.TYPE
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

        #endregion

        #region ISkladLokace_LokaceVariantySortiment2

        #region ISkladLokace_LokaceVariantySortiment2_DeleteSkladLokace_LokaceVariantySortiment Members

        public bool DeleteSkladLokace_LokaceVariantySortiment(string skl_id, string locncode, string itemnmbr)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

        #region ISkladLokace_LokaceVariantySortiment2_GetFiltrovanySkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokace_LokaceVariantySortiment(Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

        #region ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceVariantySortiment()
        {
            try
            {
                Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr filtr = new Fask.Interfaces.Filtry.LokaceVariantySortimentListFiltr();
                return GetFiltrovanySkladLokace_LokaceVariantySortiment(filtr);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region ISkladLokace_LokaceVariantySortiment2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr Members

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

        #region ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment Members

        public Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS InsertSkladLokace_LokaceVariantySortiment(byte TermID, Fask.Interfaces.DataSets.Inventura.CZMST_I4Row row, string type)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                command = new System.Data.SqlClient.SqlCommand();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter ta_varianty = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter();
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

        #region ISkladLokace_LokaceVariantySortiment2_InsertSkladLokace_LokaceVariantySortiment_Row Members

        public bool InsertSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter ta_varianty = new SQL_Datasets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceVariantySortimentTableAdapter();
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

        #region ISkladLokace_LokaceVariantySortiment2_UpdateSkladLokace_LokaceVariantySortiment Members

        public bool UpdateSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row, string locncodeold)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

        #region ITypyDokladu2

        #region ITypyDokladu2_GetFiltrovaneData Members

        public Fask.Interfaces.DataSets.TypyDokladu GetFiltrovaneData(Fask.Interfaces.Filtry.TypDokladuList filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.TypyDokladu ds = new Fask.Interfaces.DataSets.TypyDokladu();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    " select * from " + Fask.SQL.Constants.Common.TABLE_CZMST092  +
                    " where " +
                    " 1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.DocID))
                {
                    command.CommandText += "and doc_id=@doc_id ";
                    command.Parameters.AddWithValue("@doc_id", filtr.DocID);
                }



                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST092);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion 
        #endregion

        #region ILokace2

        #region ILokace2_Fill Members

        public void Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Database.Ciselnik cis = new Database.Ciselnik();
            cis.Fill_Universal(ConnectionString,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST094,
                ds.CZMST094
                );
        }



        #endregion

        #endregion

        #region Strediska
        public Interfaces.DataSets.Strediska GetFiltrovaneStrediska(StrediskaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Strediska ds = new Fask.Interfaces.DataSets.Strediska();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                string comnd = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST091;

                comnd += " where 1=1";


                if (!string.IsNullOrEmpty(filtr.str_carcode))
                {
                    comnd += " AND str_carcode='" + filtr.str_carcode.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(filtr.str_desc))
                {
                    comnd += " AND str_desc like '" + filtr.str_desc.Trim() + "%' ";
                }

                if (!string.IsNullOrEmpty(filtr.str_id))
                {
                    comnd += " AND str_id=" + filtr.str_id.Trim();
                }
                if (!string.IsNullOrEmpty(filtr.str_typ))
                {
                    comnd += " AND str_typ='" + filtr.str_typ.Trim() + "' ";
                }


                command.CommandText = comnd;
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST091);

                return ds;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region Odberatele
        public Interfaces.DataSets.Odberatele GetFiltrovaneOdberatele(AdresarListFiltr filter)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Odberatele dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

        public List<Tuple<string, string, bool>> GetZasobyTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +"'";

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



        #region Importovane akce
        public string ImportSkladu()
        {

            //throw new NotImplementedException();

            ExportKatalogSkladu_Procedura();

            return "OK";
        }


        private string ExportKatalogSkladu_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_SKLADY");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;


                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }



        public string ImportStrediska()
        {
           
                ExportKatalogStrediska_Procedura();
            

            return "OK";

        }

        private string ExportKatalogStrediska_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_STREDISKA");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;


                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }


        public string ImportOdberatel()
        {
         
           
                ExportKatalogOdberatele_Procedura();
            

            return "OK";
        }


     

        private string ExportKatalogOdberatele_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_ODBERATELE");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;

                //adpacommand.Parameters.Add((new SqlParameter("@ExportTypFilter", SqlDbType.NVarChar, 100)));
                //adpacommand.Parameters.Add((new SqlParameter("@ExportSkladFilter", SqlDbType.NVarChar, 100)));
                //adpacommand.Parameters.Add((new SqlParameter("@ExportovatPouzeAktivniPolozky", SqlDbType.Bit)));
                //adpacommand.Parameters.Add((new SqlParameter("@EXZas_DotahovatAlternativniDodavatele", SqlDbType.Bit)));
                //adpacommand.Parameters.Add((new SqlParameter("@EvidenceSarzi", SqlDbType.Bit)));
                //adpacommand.Parameters.Add((new SqlParameter("@EvidenceVyrobnichCisel", SqlDbType.Bit)));

                //((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky;
                //((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele;
                //((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi;
                //((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel;

                //((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = "";
                //((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = "";
                //((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = true;
                //((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = true;
                //((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = true;
                //((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = true;



                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }

        public List<Tuple<string, string, bool>> GetOdberateleTableInfo()
        {

            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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

        public List<Tuple<string, string, bool>> GetStrediskaTableInfo()
        {

            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST091 + "'";

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

        public string ImportSkladLokace_Mapa()
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, string, bool>> GetSkladLokace_MapaTableInfo()
        {

            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + "'";

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

        public List<Tuple<string, string, bool>> GetSkladyTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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



        #region Lokace CZMST094
        public Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_CZMST094(SkladLokaceCZMST094ListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select m.* " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " m " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.skl_id))
                {
                    command.CommandText += "and m.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.skl_id);
                }

                //if (!string.IsNullOrEmpty(filtr.skl_id))
                //{
                //    command.CommandText += "and m.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                if (!string.IsNullOrEmpty(filtr.locncode))
                {
                    command.CommandText += "and m.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.locncode);
                }

                command.CommandText += "order by m.skl_id asc, m.locncode asc;";

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST094);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteSkladLokace_CZMST094(string skl_id, string locncode)
        {

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_Mapa]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " " +
                    "where " +
                    "skl_id=@skl_id and locncode=@locncode";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);

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

        public bool InsertSkladLokace_CZMST094(Interfaces.DataSets.SkladLokace.CZMST094Row row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST094 + "([SKL_ID],[LOCNCODE],[TYPE],[Description],[Barcode]" +
                        " ) VALUES (" +
                        " @SKL_ID, @LOCNCODE, @TYPE, @Description, " +
                        " @Barcode)";


                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(row.SKL_ID) ? throw new Exception("SKL_ID is null!") : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = row.IsSKL_IDNull() ? (object)DBNull.Value : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = row.LOCNCODE });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, Value = row.IsTYPENull() ? (object)DBNull.Value : row.TYPE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, Value = row.IsDescriptionNull() ? (object)DBNull.Value : row.Description });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, Value = row.IsBarcodeNull() ? (object)DBNull.Value : row.Barcode });
                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });


                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateSkladLokace_CZMST094(Interfaces.DataSets.SkladLokace.CZMST094Row row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    //comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST094 + "([SKL_ID],[LOCNCODE],[TYPE],[Description],[Barcode]" +
                    //    " ) VALUES (" +
                    //    " @SKL_ID, @LOCNCODE, @TYPE, @Description, " +
                    //    " @Barcode)";


                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " SET" +
                        "  SKL_ID = @SKL_ID, TYPE = @TYPE, Description = @Description," +
                        " Barcode = @Barcode, LOCNCODE = @LOCNCODE" +
                        " WHERE (LOCNCODE = @LOCNCODE)";


                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(row.SKL_ID) ? throw new Exception("SKL_ID is null!") : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = row.IsSKL_IDNull() ? (object)DBNull.Value : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = row.LOCNCODE });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, Value = row.IsTYPENull() ? (object)DBNull.Value : row.TYPE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, Value = row.IsDescriptionNull() ? (object)DBNull.Value : row.Description });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, Value = row.IsBarcodeNull() ? (object)DBNull.Value : row.Barcode });
                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });


                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Interfaces.DataSets.SkladLokace.CZMST094Row GetSkladLokaceByLOCNCODE_CZMST094(string LOCNCODE)
        {
            try
            {
                if (string.IsNullOrEmpty(LOCNCODE))
                    return null;

                Fask.Interfaces.Filtry.SkladLokaceCZMST094ListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceCZMST094ListFiltr();
                filtr.locncode = LOCNCODE;
                Fask.Interfaces.DataSets.SkladLokace ds = GetFiltrovaneSkladLokace_CZMST094(filtr);

                if (ds.CZMST094.Count > 0)
                    return ds.CZMST094.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ImportSkladLokace_CZMST094()
        {
            ExportKatalogLokace_CZMST094_Procedura();


            return "OK";
        }

        private string ExportKatalogLokace_CZMST094_Procedura()
        {
                 System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_LOKACE");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;


                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            //return "OK";
        }



        public List<Tuple<string, string, bool>> GetSkladLokace_CZMST094TableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST094 + "'";

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
        public string ImportVyrobky()
        {

            ExportKatalogVyrobkySQL_Procedura();

            return "OK";
        }

        private string ExportKatalogVyrobkySQL_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);

                using (var adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_VazbaMat", adpaconnection))
                {
                    adpacommand.CommandType = CommandType.StoredProcedure;
                    adpacommand.CommandTimeout = 1000;

                    // 1) vytvořit parametry
                    adpacommand.Parameters.Add(new SqlParameter("@TypyVyrobku", SqlDbType.NVarChar, -1));
                    adpacommand.Parameters.Add(new SqlParameter("@ListVyrobku", SqlDbType.NVarChar, -1));
                    adpacommand.Parameters.Add(new SqlParameter("@TypyMaterialu", SqlDbType.NVarChar, -1));

                    // 2) poslat prázdné řetězce, ne null
                    adpacommand.Parameters["@TypyVyrobku"].Value = "5";// string.Empty;
                    adpacommand.Parameters["@ListVyrobku"].Value = string.Empty;
                    adpacommand.Parameters["@TypyMaterialu"].Value = "1";// string.Empty;

                    adpaconnection.Open();
                    var odpoved = adpacommand.ExecuteNonQuery();
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }

        public string ImportPracovnici()
        {
            ExportKatalogPracovniciSQL_Procedura();

            return "OK";
        }

        private string ExportKatalogPracovniciSQL_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);

                using (var adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_Pracovnici", adpaconnection))
                {
                    adpacommand.CommandType = CommandType.StoredProcedure;
                    adpacommand.CommandTimeout = 1000;

                    //// 1) vytvořit parametry
                    //adpacommand.Parameters.Add(new SqlParameter("@TypyVyrobku", SqlDbType.NVarChar, -1));
                    //adpacommand.Parameters.Add(new SqlParameter("@ListVyrobku", SqlDbType.NVarChar, -1));
                    //adpacommand.Parameters.Add(new SqlParameter("@TypyMaterialu", SqlDbType.NVarChar, -1));

                    //// 2) poslat prázdné řetězce, ne null
                    //adpacommand.Parameters["@TypyVyrobku"].Value = "5";// string.Empty;
                    //adpacommand.Parameters["@ListVyrobku"].Value = string.Empty;
                    //adpacommand.Parameters["@TypyMaterialu"].Value = "1";// string.Empty;

                    adpaconnection.Open();
                    var odpoved = adpacommand.ExecuteNonQuery();
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }
    }


    #region pomocne tridy
    public class ZasobyTableInfo
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
    }

    public class ZasobyData
    {
        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
    #endregion
}
