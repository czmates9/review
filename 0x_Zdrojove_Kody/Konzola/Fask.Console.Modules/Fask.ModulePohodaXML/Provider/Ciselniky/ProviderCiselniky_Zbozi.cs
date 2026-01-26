using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Fask.ModulePohodaXML.Pohoda_DataSets;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi,

        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams,

        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetParametry,

        Fask.Interfaces.Vazby.IVazby2_ImportVyrobky

    {
     
        /// <summary>
        /// Metoda pro smazani řadku z FASK_ZBOZI a FASK_ZBOZI_PARAMETRY 
        /// </summary>
        /// <param name="id_ZBOZI">ID řadku v FASK_ZBOZI</param>
        /// <param name="ID_Params">ID řadku v FASK_ZBOZI_PARAMETRY</param>
        /// <returns>True-OK, False- chyba</returns>
        public bool DeleteZbozi(int id_ZBOZI, int? ID_Params)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_FASK_ZASOBY = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
                ta_FASK_ZASOBY.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_FASK_ZASOBY.MyTransaction = trans;
                ta_FASK_ZASOBY.Delete(id_ZBOZI);

                if (ID_Params.HasValue)
                {

                    Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_FASK_ZASOBYParam = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                    ta_FASK_ZASOBYParam.Connection = connection;
                    //trans = connection.BeginTransaction(IsolationLevel.Serializable);
                    ta_FASK_ZASOBYParam.MyTransaction = trans;
                    ta_FASK_ZASOBYParam.Delete(ID_Params.Value); 
                }



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
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = " SELECT Z.DEX_ROW_ID as DEX_ROW_ID_Zbozi ,Z.ITEMNMBR ,Z.ITEMDESC ,Z.ITEMCODE ,Z.VNDITNUM ,Z.CZ_CarKod ,Z.LOCNCODE ,Z.SKL_ID " +
                " ,Z.QTY ,Z.QTYPACK ,Z.MJ ,Z.DMJ ,Z.TAXRATE ,Z.PRICE0 ,Z.PRICE1 ,Z.PRICE2 ,Z.PRICE3 ,Z.PRICE4 ,Z.PRICE5 ,Z.CZ_SerNum_Track " +
                " ,Z.CZ_SerNum_Delka ,Z.CZ_Rez1_Track ,Z.CZ_Rez2_Track ,Z.CZ_Rez3_Track ,Z.CZ_Rez4_Track ,Z.REZ1 ,Z.REZ2 ,Z.REZ3 ,Z.REZ4 ,Z.ODB_ID ,Z.mena_ID ,Z.SERLTNUM ,Z.WEIGHT ,Z.TIMEFROM ,Z.TIMETO ,Z.LSTMod ,Z.loginid " +
                " ,P.DEX_ROW_ID as DEX_ROW_ID_PARAMETRY ,P.VPrFVTS ,P.VPrFPTS ,P.VPrFDTS ,P.VPrFITS ,P.VPrFXTS ,P.RefVPrFVTS ,P.RefVPrFPTS ,P.RefVPrFDTS ,P.RefVPrFITS ,P.RefVPrFXTS ,P.VPrTIMEPREP ,P.VPrTIMEUNIT ,P.RefVPrTIMEMODE " +
                " ,S.skl_desc as SKL_DESC" +
                " ,POHODA_Zasoby.RelSkTyp as ITEMTYPE " +
                " , POHODA_CleneniSklad.Vetev1 " +
                " , POHODA_CleneniSklad.Vetev2 " +
                " , POHODA_CleneniSklad.Vetev3 " +
                " , POHODA_CleneniSklad.Vetev4 " +
                " , POHODA_CleneniSklad.Vetev5 " +
                " , POHODA_CleneniSklad.Vetev6 " +
                " , POHODA_CleneniSklad.Vetev7 " +
                " , ITEMTYPE_Text = CASE " + 
                " WHEN RelSkTyp = 1 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 1) + "' " +
                " WHEN RelSkTyp = 2 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 2) + "' " +
                " WHEN RelSkTyp = 3 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 3) + "' " +
                " WHEN RelSkTyp = 4 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 4) + "' " +
                " WHEN RelSkTyp = 5 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 5) + "' " +
                " WHEN RelSkTyp = 6 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 6) + "' " +
                " END " +   

                " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z " +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY + " as P ON Z.ITEMNMBR = P.ITEMNMBR " +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as S ON S.skl_id = Z.SKL_ID" +

                " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKz as POHODA_Zasoby ON POHODA_Zasoby.ID = Z.ITEMNMBR " +
                " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKSt as POHODA_CleneniSklad ON POHODA_CleneniSklad.RefSklad = S.skl_id AND POHODA_CleneniSklad.ID =  POHODA_Zasoby.RefStruct ";

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
                    command.CommandText += "AND Z.ITEMCODE like  @itemcode + '%' ";
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

                if (((int)filtr.TypMaterialu) != 0) 
                {
                    command.CommandText += "AND (POHODA_Zasoby.RelSkTyp=@RelSkTyp) ";
                    command.Parameters.AddWithValue("@RelSkTyp", (int)filtr.TypMaterialu);
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

                //TaD: Požadavek od JaS pro online dotahovani typu položky...
                //Extremne to spomalovalo vyhledavani...
                //var dt = Database.Pohoda.SKz_Get_FiltrTypByID(string.Empty);

                //foreach (var item in ds.FASK_ZASOBY_ALL)
                //{
                //    int ITEMNMBTmp;

                //    if (!int.TryParse(item.ITEMNMBR, out ITEMNMBTmp))
                //    {
                //        throw new Exception("ITEMNMBR neni číslo!!");
                //    }

                //    var filtrITEMNMBR = dt.Where(x => x.ID == ITEMNMBTmp);

                //    item.ITEMTYPE = filtrITEMNMBR.First().RelSkTyp.ToString();
                //}



                ds.FASK_ZASOBY_ALL_KONZOLA.AcceptChanges();

                //if (filtr.TypMaterialu.HasValue)
                //{

                //    var dtFilter = Database.Pohoda.SKz_Get_FiltrTypByID(filtr.TypMaterialu.Value.ToString());

                //    foreach (var item in ds.FASK_ZASOBY_ALL)
                //    {


                //        int ITEMNMBTmp;

                //        if(!int.TryParse(item.ITEMNMBR, out ITEMNMBTmp))
                //        {
                //            throw new Exception("ITEMNMBR neni číslo!!");
                //        }

                //        if(!dtFilter.Any(x => x.ID == ITEMNMBTmp))
                //        {
                //            item.Delete();
                //        }
                //    }

                //    ds.FASK_ZASOBY_ALL.AcceptChanges();
                //}
                
                
                return ds;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw ex;
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
                var ds = GetFiltrovaneZbozi(filtr);

                if((ds != null) && ( ds.FASK_ZASOBY_ALL_KONZOLA.Count > 0))
                {
                    return ds.FASK_ZASOBY_ALL_KONZOLA.First();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_Zbozi = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
                ta_Zbozi.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_Zbozi.MyTransaction = trans;

                ta_Zbozi.Insert(
                    zboziRow.ITEMNMBR,
                    zboziRow.IsITEMDESCNull() ? null : zboziRow.ITEMDESC,
                    zboziRow.IsITEMCODENull() ? null : zboziRow.ITEMCODE,
                    zboziRow.IsVNDITNUMNull() ? null : zboziRow.VNDITNUM,
                    zboziRow.IsCZ_CarKodNull() ? null : zboziRow.CZ_CarKod,
                    zboziRow.IsLOCNCODENull() ? null : zboziRow.LOCNCODE,
                    zboziRow.IsSKL_IDNull() ? null : zboziRow.SKL_ID,
                    zboziRow.QTY,
                    zboziRow.IsQTYPACKNull() ? (decimal?)null : zboziRow.QTYPACK,
                    zboziRow.MJ,
                    zboziRow.DMJ,
                    zboziRow.IsTAXRATENull() ? (decimal?)null : zboziRow.TAXRATE,
                    zboziRow.IsPRICE0Null() ? (decimal?)null : zboziRow.PRICE0,
                    zboziRow.IsPRICE1Null() ? (decimal?)null : zboziRow.PRICE1,
                    zboziRow.IsPRICE2Null() ? (decimal?)null : zboziRow.PRICE2,
                    zboziRow.IsPRICE3Null() ? (decimal?)null : zboziRow.PRICE3,
                    zboziRow.IsPRICE4Null() ? (decimal?)null : zboziRow.PRICE4,
                    zboziRow.IsPRICE5Null() ? (decimal?)null : zboziRow.PRICE5,
                    zboziRow.CZ_SerNum_Track,
                    zboziRow.CZ_SerNum_Delka,
                    zboziRow.CZ_Rez1_Track,
                    zboziRow.CZ_Rez2_Track,
                    zboziRow.CZ_Rez3_Track,
                    zboziRow.CZ_Rez4_Track,
                    zboziRow.IsREZ1Null() ? null : zboziRow.REZ1,
                    zboziRow.IsREZ2Null() ? null : zboziRow.REZ2,
                    zboziRow.IsREZ3Null() ? null : zboziRow.REZ3,
                    zboziRow.IsREZ4Null() ? null : zboziRow.REZ4,
                    zboziRow.IsODB_IDNull() ? null : zboziRow.ODB_ID,
                    zboziRow.Ismena_IDNull() ? null : zboziRow.mena_ID,
                    zboziRow.IsSERLTNUMNull() ? null : zboziRow.SERLTNUM,
                    zboziRow.IsWEIGHTNull() ? (decimal?)null : zboziRow.WEIGHT,
                    zboziRow.IsTIMEFROMNull() ? (DateTime?)null : zboziRow.TIMEFROM,
                    zboziRow.IsTIMETONull() ? (DateTime?)null : zboziRow.TIMETO,
                    zboziRow.IsLSTModNull() ? (DateTime?)null : zboziRow.LSTMod,
                    zboziRow.IsloginidNull() ? null : zboziRow.loginid
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
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter ta_zbozi = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
                ta_zbozi.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_zbozi.MyTransaction = trans;

                ta_zbozi.Update(
                    zboziRow.ITEMNMBR,
                    zboziRow.IsITEMDESCNull() ? null : zboziRow.ITEMDESC,
                    zboziRow.IsITEMCODENull() ? null : zboziRow.ITEMCODE,
                    zboziRow.IsVNDITNUMNull() ? null : zboziRow.VNDITNUM,
                    zboziRow.IsLOCNCODENull() ? null : zboziRow.LOCNCODE,
                    zboziRow.IsCZ_CarKodNull() ? null : zboziRow.CZ_CarKod,
                    zboziRow.IsSKL_IDNull() ? null : zboziRow.SKL_ID,
                    zboziRow.QTY,
                    zboziRow.IsQTYPACKNull() ? (decimal?)null : zboziRow.QTYPACK,
                    zboziRow.MJ,
                    zboziRow.DMJ,
                    zboziRow.IsTAXRATENull() ? (decimal?)null : zboziRow.TAXRATE,
                    zboziRow.IsPRICE0Null() ? (decimal?)null : zboziRow.PRICE0,
                    zboziRow.IsPRICE1Null() ? (decimal?)null : zboziRow.PRICE1,
                    zboziRow.IsPRICE2Null() ? (decimal?)null : zboziRow.PRICE2,
                    zboziRow.IsPRICE3Null() ? (decimal?)null : zboziRow.PRICE3,
                    zboziRow.IsPRICE4Null() ? (decimal?)null : zboziRow.PRICE4,
                    zboziRow.IsPRICE5Null() ? (decimal?)null : zboziRow.PRICE5,
                    zboziRow.CZ_SerNum_Track,
                    zboziRow.CZ_SerNum_Delka,
                    zboziRow.CZ_Rez1_Track,
                    zboziRow.CZ_Rez2_Track,
                    zboziRow.CZ_Rez3_Track,
                    zboziRow.CZ_Rez4_Track,
                    zboziRow.IsREZ1Null() ? null : zboziRow.REZ1,
                    zboziRow.IsREZ2Null() ? null : zboziRow.REZ2,
                    zboziRow.IsREZ3Null() ? null : zboziRow.REZ3,
                    zboziRow.IsREZ4Null() ? null : zboziRow.REZ4,
                    zboziRow.IsODB_IDNull() ? null : zboziRow.ODB_ID,
                    zboziRow.Ismena_IDNull() ? null : zboziRow.mena_ID,
                    zboziRow.IsSERLTNUMNull() ? null : zboziRow.SERLTNUM,
                    zboziRow.IsWEIGHTNull() ? (decimal?)null : zboziRow.WEIGHT,
                    zboziRow.IsTIMEFROMNull() ? (DateTime?)null : zboziRow.TIMEFROM,
                    zboziRow.IsTIMETONull() ? (DateTime?)null : zboziRow.TIMETO,
                    zboziRow.IsLSTModNull() ? (DateTime?)null : zboziRow.LSTMod,
                    zboziRow.IsloginidNull() ? null : zboziRow.loginid,
                    zboziRow.DEX_ROW_ID_Zbozi
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
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_Zbozi = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ta_Zbozi.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_Zbozi.MyTransaction = trans;

                ta_Zbozi.Insert(
                    zboziRow.ITEMNMBR,
                    zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
                    zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
                    zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
                    zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
                    zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
                    zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
                    zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
                    zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
                    zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
                    zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
                    zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
                    zboziRow.IsVPrTIMEUNITNull() ? (double ?)null : zboziRow.VPrTIMEUNIT,
                    zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE
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
                    Globals_V1.LoadConfiguration();
                    connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                    connection.Open();

                    Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_Zbozi = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                    ta_Zbozi.Connection = connection;
                    trans = connection.BeginTransaction(IsolationLevel.Serializable);
                    ta_Zbozi.MyTransaction = trans;

                    ta_Zbozi.Insert(
                        zboziRow.ITEMNMBR,
                        zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
                        zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
                        zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
                        zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
                        zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
                        zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
                        zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
                        zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
                        zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
                        zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
                        zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
                        zboziRow.IsVPrTIMEUNITNull() ? (double?)null : zboziRow.VPrTIMEUNIT,
                        zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE
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
        }


        private bool UpdateParametry(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ta_zbozi = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ta_zbozi.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_zbozi.MyTransaction = trans;

 
                ta_zbozi.Update(
                    zboziRow.ITEMNMBR,
                    zboziRow.IsVPrFVTSNull() ? (bool?)null : zboziRow.VPrFVTS,
                    zboziRow.IsVPrFPTSNull() ? (bool?)null : zboziRow.VPrFPTS,
                    zboziRow.IsVPrFDTSNull() ? (bool?)null : zboziRow.VPrFDTS,
                    zboziRow.IsVPrFITSNull() ? (bool?)null : zboziRow.VPrFITS,
                    zboziRow.IsVPrFXTSNull() ? (bool?)null : zboziRow.VPrFXTS,
                    zboziRow.IsRefVPrFVTSNull() ? (int?)null : zboziRow.RefVPrFVTS,
                    zboziRow.IsRefVPrFPTSNull() ? (int?)null : zboziRow.RefVPrFPTS,
                    zboziRow.IsRefVPrFDTSNull() ? (int?)null : zboziRow.RefVPrFDTS,
                    zboziRow.IsRefVPrFITSNull() ? (int?)null : zboziRow.RefVPrFITS,
                    zboziRow.IsRefVPrFXTSNull() ? (int?)null : zboziRow.RefVPrFXTS,
                    zboziRow.IsVPrTIMEPREPNull() ? (double?)null : zboziRow.VPrTIMEPREP,
                    zboziRow.IsVPrTIMEUNITNull() ? (double ?)null : zboziRow.VPrTIMEUNIT,
                    zboziRow.IsRefVPrTIMEMODENull() ? (int?)null : zboziRow.RefVPrTIMEMODE,
                    zboziRow.DEX_ROW_ID_PARAMETRY
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

        private bool GetParametrybyID(string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY;

                command.CommandText += " WHERE ITEMNMBR = '" + ITEMNMBR + "'";

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

        #region IZbozi2_ImportZbozi Members

        public string ImportZbozi()
        {

            if (true)
            {
                ExportKatalogZasobyPohoda_Procedura();
            }
            else
            {
                return ImportZbozi_Puvodni();
            }

            return "OK";
        }

        private string ExportKatalogZasobyPohoda_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_POHODA_FASK_ZASOBY");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;

                adpacommand.Parameters.Add((new SqlParameter("@ExportTypFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportSkladFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportovatPouzeAktivniPolozky", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EXZas_DotahovatAlternativniDodavatele", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceSarzi", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceVyrobnichCisel", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@PohodaE1", SqlDbType.Bit)));


                ((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter;
                ((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter;
                ((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky;
                ((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele;
                ((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi;
                ((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel;
                ((IDataParameter)adpacommand.Parameters["@PohodaE1"]).Value = Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1;



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


        public string ImportZbozi_Puvodni()
        {

            SqlTransaction trans = null;
            SqlConnection connection = null;

            DatabasePohoda.SKzDataTable skzDataTable = new DatabasePohoda.SKzDataTable();
            DatabasePohoda.SKzAlternativesDataTable skzalternativesDT = new DatabasePohoda.SKzAlternativesDataTable();
            DatabasePohoda.SKzParametryDataTable skzparametryDT = new DatabasePohoda.SKzParametryDataTable();

            try
            {
                Globals_V1.LoadConfiguration();


                if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter) || !String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter))
                {
                    if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
                    {

                        Database.Pohoda.SKz_FillBy_EPAP_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

                        //dotazeni alternativ
                        if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
                        {
                            Database.Pohoda.SKzAlternatives_FillBy_EPAP_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
                        }
                        else
                            skzalternativesDT = new DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                    else
                    {

                        Database.Pohoda.SKz_FillBy_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

                        //dotazeni alternativ
                        if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
                        {
                            Database.Pohoda.SKzAlternatives_FillBy_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
                        }
                        else
                            skzalternativesDT = new DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
                    }
                }
                else
                {
                    if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
                    {
                        skzDataTable = Database.Pohoda.SKz_GetDataByAktivniPolozky();
                        //dotazeni alternativ
                        skzalternativesDT = Database.Pohoda.SKzAlternatives_GetDataByAktivni();
                    }
                    else
                    {
                        //skzDataTable = skz_ta.GetDataByOptimalize();
                        skzDataTable = Database.Pohoda.SKz_GetDataByOptimalize();
                        //dotazeni alternativ
                        if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
                        {
                            //skzalternativesDT = skzalternatives_ta.GetData();
                            skzalternativesDT = Database.Pohoda.SKzAlternatives_GetData();
                        }
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                }

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                #region FASK_ZASOBY delete

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter FASK_ZASOBY_TA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
                //FASK_ZASOBY_TA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                FASK_ZASOBY_TA.Connection = trans.Connection;

                FASK_ZASOBY_TA.Transaction = trans;
                FASK_ZASOBY_TA.DeleteQuery();

                #endregion

                #region FASK_ZASOBY_PARAMETRY delete

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter FASK_ZASOBY_PARAM_TA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                //FASK_ZASOBY_PARAM_TA.Connection.ConnectionString = Globals.ConnectionString;
                FASK_ZASOBY_PARAM_TA.Connection = trans.Connection;

                FASK_ZASOBY_PARAM_TA.Transaction = trans;
                FASK_ZASOBY_PARAM_TA.DeleteQuery();

                #endregion


                Pohoda_DataSets.Zbozi zbozi = new Pohoda_DataSets.Zbozi();

                string nazev;
                string lokace;
                string ean;
                string cz_carkod;
                string mj;
                string id;
                string ids;
                int sklad_id;
                string refAD;


                int polCelkem = skzDataTable.Count;
                int polProgress = 0;

                int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMDESC"].MaxLength;
                int ODB_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ODB_ID"].MaxLength;
                int LOCNCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["LOCNCODE"].MaxLength;
                int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["VNDITNUM"].MaxLength;
                int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["CZ_CarKod"].MaxLength;
                int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["MJ"].MaxLength;
                int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMNMBR"].MaxLength;

                foreach (var item in skzDataTable)
                {
                    polProgress++;

                    nazev = item.IsNazevNull() ? "" : item.Nazev;
                    ean = item.IsEANNull() ? "" : item.EAN;
                    cz_carkod = string.Empty;
                    refAD = item.IsRefADNull() ? "" : item.RefAD.ToString();
                    mj = item.IsMJNull() ? "" : item.MJ;
                    ids = item.IsIDSNull() ? "" : item.IDS;
                    id = item.ID.ToString();

                    sklad_id = item.RefSklad;

                    lokace = string.Empty;

                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }


                    if (nazev.Length > ITEMDESC_MaxLength)
                        nazev = nazev.Remove(ITEMDESC_MaxLength);

                    if (refAD.Length > ODB_ID_MaxLength)
                        refAD = refAD.Remove(ODB_ID_MaxLength);


                    if (lokace.Length > LOCNCODE_MaxLength)
                        lokace = lokace.Remove(LOCNCODE_MaxLength);

                    if (ean.Length > VNDITNUM_MaxLength)
                        ean = ean.Remove(VNDITNUM_MaxLength);

                    if (cz_carkod.Length > CZ_CarKod_MaxLength)
                        cz_carkod = cz_carkod.Remove(CZ_CarKod_MaxLength);

                    if (mj.Length > MJ_MaxLength)
                        mj = mj.Remove(MJ_MaxLength);

                    if (id.Length > ITEMNMBR_MaxLength)
                        id = id.Remove(ITEMNMBR_MaxLength);


                    byte sernumrack = 0;

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY

                    //if (Properties.Settings.Default.POHODA_E1)
                    //{
                    //    if (item.IsVPrFXTSNull())
                    //    {
                    //        sernumrack = 0;
                    //    }
                    //    else
                    //    {
                    //        if (item.VPrFXTS)
                    //        {
                    //            sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFXTSNull()) ? (int?)null : ((int?)item.RefVPrFXTS - 1), item.VPrFXTS);
                    //        }
                    //        else
                    //        {
                    //            sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFDTSNull()) ? (int?)null : ((int?)item.RefVPrFDTS - 1), item.VPrFDTS);
                    //        }
                    //    }

                    //    #region Naplnit FASK_ZASOBY_PARAMETRY

                    //    //var RowZboziParam = zbozi.FASK_ZASOBY_PARAMETRY.NewFASK_ZASOBY_PARAMETRYRow();

                    //    //RowZboziParam.ITEMNMBR = id;

                    //    //RowZboziParam.VPrFVTS = item.IsVPrFVTSNull() ? false : item.VPrFVTS;
                    //    //RowZboziParam.VPrFPTS = item.IsVPrFPTSNull() ? false : item.VPrFPTS;
                    //    //RowZboziParam.VPrFDTS = item.IsVPrFDTSNull() ? false : item.VPrFDTS;
                    //    //RowZboziParam.VPrFITS = item.IsVPrFITSNull() ? false : item.VPrFITS;
                    //    //RowZboziParam.VPrFXTS = item.IsVPrFXTSNull() ? false : item.VPrFXTS;

                    //    //RowZboziParam.RefVPrFVTS = item.IsRefVPrFVTSNull() ? 0 : item.RefVPrFVTS;
                    //    //RowZboziParam.RefVPrFPTS = item.IsRefVPrFPTSNull() ? 0 : item.RefVPrFPTS;
                    //    //RowZboziParam.RefVPrFDTS = item.IsRefVPrFDTSNull() ? 0 : item.RefVPrFDTS;
                    //    //RowZboziParam.RefVPrFITS = item.IsRefVPrFITSNull() ? 0 : item.RefVPrFITS;
                    //    //RowZboziParam.RefVPrFXTS = item.IsRefVPrFXTSNull() ? 0 : item.RefVPrFXTS;

                    //    //RowZboziParam.VPrTIMEPREP = 0; //item.VPrTIMEPREP;
                    //    //RowZboziParam.VPrTIMEUNIT = 0; // item.VPrTIMEUNIT;
                    //    //RowZboziParam.RefVPrTIMEMODE = 0; // item.RefVPrTIMEMODE;

                    //    //zbozi.FASK_ZASOBY_PARAMETRY.AddFASK_ZASOBY_PARAMETRYRow(RowZboziParam);

                    //    #endregion


                    //}
                    //else
                    //{
                    //    sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRelSKzVCNull()) ? (int?)null : (int?)item.RelSKzVC, true);
                    //}


                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                    sernumrack = Classes.Pohoda.GetPriznakSledovani_Zbozi(item.IsRelSKzVCNull() ? (int?)null : (int?)item.RelSKzVC, item);

                    //if (Properties.Settings.Default.POHODA_E1)
                    //{
                    //    sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, row);
                    //}
                    //else
                    //{
                    //    Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                    //    var dt_param = ParamTA.GetDataByITEMNMBR(itemnmbr.Trim());

                    //    if ((dt_param != null) && (dt_param.Count > 0))
                    //    {
                    //        dt_row_param = dt_param.First();
                    //    }

                    //    sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Inventura);
                    //}


                    var RowZbozi = zbozi.FASK_ZASOBY.NewFASK_ZASOBYRow();

                    RowZbozi.ITEMNMBR = id;
                    RowZbozi.ITEMDESC = nazev;
                    RowZbozi.VNDITNUM = ean;
                    RowZbozi.CZ_CarKod = cz_carkod;
                    RowZbozi.LOCNCODE = lokace;
                    RowZbozi.SKL_ID = sklad_id.ToString();
                    RowZbozi.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
                    RowZbozi.QTYPACK = 0;
                    RowZbozi.MJ = mj;
                    RowZbozi.DMJ = string.Empty;
                    RowZbozi.TAXRATE = 0;
                    RowZbozi.PRICE0 = 0;
                    RowZbozi.PRICE1 = 0;
                    RowZbozi.PRICE2 = 0;
                    RowZbozi.PRICE3 = 0;
                    RowZbozi.PRICE4 = 0;
                    RowZbozi.PRICE5 = 0;
                    RowZbozi.CZ_SerNum_Track = sernumrack;
                    RowZbozi.CZ_SerNum_Delka = 0;
                    RowZbozi.CZ_Rez1_Track = 0;
                    RowZbozi.CZ_Rez2_Track = 0;
                    RowZbozi.CZ_Rez3_Track = 0;
                    RowZbozi.CZ_Rez4_Track = 0;
                    RowZbozi.REZ1 = string.Empty;
                    RowZbozi.ITEMCODE = ids;
                    RowZbozi.ODB_ID = refAD;
                    RowZbozi.REZ2 = string.Empty;
                    RowZbozi.REZ3 = string.Empty;
                    RowZbozi.REZ4 = string.Empty;
                    RowZbozi.mena_ID = string.Empty;
                    RowZbozi.SERLTNUM = string.Empty;
                    RowZbozi.SetWEIGHTNull();

                    RowZbozi.SetTIMEFROMNull();
                    RowZbozi.SetTIMETONull();
                    RowZbozi.LSTMod = DateTime.Now;
                    RowZbozi.loginid = string.Empty;


                    zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(RowZbozi);

                }


                //vlozeni alternativ ...
                foreach (var item in skzalternativesDT)
                {
                    nazev = item.IsNazevNull() ? "" : item.Nazev;
                    ean = item.IsNCEANNull() ? "" : item.NCEAN;
                    cz_carkod = string.Empty;
                    mj = item.IsNCMJEANNull() ? "" : item.NCMJEAN;
                    ids = item.IsIDSNull() ? "" : item.IDS;
                    id = item.ID.ToString();
                    refAD = item.IsNCRefADNull() ? "" : item.NCRefAD.ToString();

                    lokace = string.Empty;
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }

                    if (nazev.Length > ITEMDESC_MaxLength)
                        nazev = nazev.Remove(ITEMDESC_MaxLength);

                    if (lokace.Length > LOCNCODE_MaxLength)
                        lokace = lokace.Remove(LOCNCODE_MaxLength);

                    if (ean.Length > VNDITNUM_MaxLength)
                        ean = ean.Remove(VNDITNUM_MaxLength);

                    if (cz_carkod.Length > CZ_CarKod_MaxLength)
                        cz_carkod = cz_carkod.Remove(CZ_CarKod_MaxLength);

                    if (mj.Length > MJ_MaxLength)
                        mj = mj.Remove(MJ_MaxLength);

                    if (id.Length > ITEMNMBR_MaxLength)
                        id = id.Remove(ITEMNMBR_MaxLength);



                    var RowZbozi = zbozi.FASK_ZASOBY.NewFASK_ZASOBYRow();

                    RowZbozi.ITEMNMBR = id;
                    RowZbozi.ITEMDESC = nazev;
                    RowZbozi.VNDITNUM = ean;
                    RowZbozi.CZ_CarKod = cz_carkod;
                    RowZbozi.LOCNCODE = lokace;
                    RowZbozi.SKL_ID = string.Empty;
                    RowZbozi.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
                    RowZbozi.QTYPACK = 0;
                    RowZbozi.MJ = mj;
                    RowZbozi.DMJ = string.Empty;
                    RowZbozi.TAXRATE = 0;
                    RowZbozi.PRICE0 = 0;
                    RowZbozi.PRICE1 = 0;
                    RowZbozi.PRICE2 = 0;
                    RowZbozi.PRICE3 = 0;
                    RowZbozi.PRICE4 = 0;
                    RowZbozi.PRICE5 = 0;
                    RowZbozi.CZ_SerNum_Track = byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString());
                    RowZbozi.CZ_SerNum_Delka = 0;
                    RowZbozi.CZ_Rez1_Track = 0;
                    RowZbozi.CZ_Rez2_Track = 0;
                    RowZbozi.CZ_Rez3_Track = 0;
                    RowZbozi.CZ_Rez4_Track = 0;
                    RowZbozi.REZ1 = string.Empty;
                    RowZbozi.REZ2 = string.Empty;
                    RowZbozi.REZ3 = string.Empty;
                    RowZbozi.REZ4 = string.Empty;
                    RowZbozi.ITEMCODE = ids;
                    RowZbozi.ODB_ID = refAD;

                    RowZbozi.mena_ID = string.Empty;
                    RowZbozi.SERLTNUM = string.Empty;
                    RowZbozi.SetWEIGHTNull();

                    RowZbozi.SetTIMEFROMNull();
                    RowZbozi.SetTIMETONull();
                    RowZbozi.LSTMod = DateTime.Now;
                    RowZbozi.loginid = string.Empty;


                    zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(RowZbozi);

                }


                FASK_ZASOBY_TA.Update(zbozi.FASK_ZASOBY);
                //FASK_ZASOBY_PARAM_TA.Update(zbozi.FASK_ZASOBY_PARAMETRY);


                if (trans != null)
                    trans.Commit();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                Fask.Logging.ExceptionHandler2.Handle(skzDataTable);
                Fask.Logging.ExceptionHandler2.Handle(skzalternativesDT);
                Fask.Logging.ExceptionHandler2.Handle(skzparametryDT);


                if (trans != null) trans.Rollback();

                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }


        #endregion




        public Fask.Interfaces.Classes.Parametry GetParametry()
        {
            Globals_V1.LoadConfiguration();
            return new Fask.Interfaces.Classes.Parametry(Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1, Globals_V1.Konfigurace.Konzola[0].PovolZaporneZasoby);
        }

        public List<Tuple<string, string, bool>> GetZasobyTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + "'";

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

        public string ImportVyrobky()
        {

            ExportKatalogVyrobkyPohoda_Procedura();
            
            return "OK";
        }

        private string ExportKatalogVyrobkyPohoda_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(
                    Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                using (var adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_POHODA_FASK_VazbaMat", adpaconnection))
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


    }
}
