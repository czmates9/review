using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.ModulePohodaXML.Database;

namespace Fask.ModulePohodaXML.Provider
{

    public partial class Provider :
        Fask.Interfaces.Vazby.IVazby2,
        Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyAddVyrobky,
        Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyMaterialy,
        Fask.Interfaces.Vazby.IVazby2_GetFiltrovanyVazbyVyrobky,
        Fask.Interfaces.Vazby.IVazby2_Insert_VazbyAddVyrobky,
        Fask.Interfaces.Vazby.IVazby2_Scalar_MAX_IDL,
        Fask.Interfaces.Vazby.IVazby2_Scalar_Vyrobky_Count,
        Fask.Interfaces.Vazby.IVazby2_DeleteVyrobek,
        Fask.Interfaces.Vazby.IVazby2_UpdateEdit,
        Fask.Interfaces.Vazby.IVazby2_Fill_onlyALTERtable,
        Fask.Interfaces.Vazby.IVazby2_Fill_only_Koef_and_Alter,
        Fask.Interfaces.Vazby.IVazby2_DeleteMaterial,
        Fask.Interfaces.Vazby.IVazby2_Import_TP2PS,
        Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_GetFiltrovaneData,
        Fask.Interfaces.Vazby.IVazby2_Modifikace_TP_SetZamenZdrojCil
    {
        public string ITEMNMBR_Def { get; set; }
        public string rowvyrobek_ID_L { get; set; }


        #region IVazby2_GetFiltrovanyVazbyAddVyrobky Members

        public Fask.Interfaces.DataSets.Zbozi GetFiltrovanyVazbyAddVyrobky(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
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

                command.CommandText =
                    " SELECT Z.*, S.skl_desc as SKL_DESC " +
                    " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z" +
                    " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as S ON S.skl_id = Z.SKL_ID " +
                    " WHERE " +
                    " 1=1 "
                    ;

                // TODO jak sem dostat itemnmbr??
                //command.Parameters.AddWithValue("@In_ITEMNMBR", ITEMNMBR_Materialy);

                if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
                {
                    command.CommandText += "AND Z.ITEMNMBR like '%' + @filtr_ITEMNMBR + '%' ";
                    command.Parameters.AddWithValue("@filtr_ITEMNMBR", filtr.MaterialITEMNMBR);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialSklID))
                {
                    command.CommandText += "AND Z.SKL_ID = @filtr_SKL_ID ";
                    command.Parameters.AddWithValue("@filtr_SKL_ID", filtr.MaterialSklID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialLocncode))
                {
                    command.CommandText += "AND Z.LOCNCODE = @filtr_LOCNCODE ";
                    command.Parameters.AddWithValue("@filtr_LOCNCODE", filtr.MaterialLocncode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialVNDITNUM))
                {
                    command.CommandText += "AND Z.VNDITNUM = @filtr_VNDITNUM ";
                    command.Parameters.AddWithValue("@filtr_VNDITNUM", filtr.MaterialVNDITNUM);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
                {
                    command.CommandText += "AND Z.ITEMDESC like '%' + @filtr_ITEMDESC + '%' ";
                    command.Parameters.AddWithValue("@filtr_ITEMDESC", filtr.MaterialITEMDESC);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialCarKod))
                {
                    command.CommandText += "AND Z.CZ_CarKod = @filtr_CZ_CarKod ";
                    command.Parameters.AddWithValue("@filtr_CZ_CarKod", filtr.MaterialCarKod);
                }


                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_ZASOBY_KONZOLA);

                return ds;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region IVazby2_GetFiltrovanyVazbyMaterialy Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazbyMaterialy(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            //command.CommandText =
            //        "SELECT DISTINCT ID, ID_H, ID_L, koef, ID_USER, dateedit, [alter], ITEMNMBR_fol ITEMNMBR, DESC_fol ITEMDESC, MJ_fol MJ, PUO " +
            //        "FROM FASK_Vyroba_TP " +
            //        "WHERE (ITEMNMBR_Def = @itemnmbr) AND (ID_H = @rowvyrobek_ID_L) ";

            command.CommandText =
            "SELECT DISTINCT tp.ID, tp.ID_H, tp.ID_L, tp.koef, tp.ID_USER, tp.dateedit, tp.[alter], tp.ITEMNMBR_fol ITEMNMBR, tp.DESC_fol ITEMDESC, tp.MJ_fol MJ, tp.PUO , sklad.SKL_ID as SKL_ID , sklad.skl_desc as SKL_DESC" +

            " , Z.ITEMCODE" +
            " , Z.VNDITNUM" +

            " , POHODA_CleneniSklad.Vetev1 " +
            " , POHODA_CleneniSklad.Vetev2 " +
            " , POHODA_CleneniSklad.Vetev3 " +
            " , POHODA_CleneniSklad.Vetev4 " +
            " , POHODA_CleneniSklad.Vetev5 " +
            " , POHODA_CleneniSklad.Vetev6 " +
            " , POHODA_CleneniSklad.Vetev7 " +

            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_TP + " as tp " +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z ON Z.ITEMNMBR = tp.ITEMNMBR_fol" +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = Z.SKL_ID" +

            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKz as POHODA_Zasoby ON POHODA_Zasoby.ID = Z.ITEMNMBR " +
            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKSt as POHODA_CleneniSklad ON POHODA_CleneniSklad.RefSklad = sklad.skl_id AND POHODA_CleneniSklad.ID =  POHODA_Zasoby.RefStruct " +

            " WHERE (tp.ITEMNMBR_Def = @itemnmbr) AND (tp.ID_H = @rowvyrobek_ID_L) ";


            // TODO jak sem dostat itemnmbr??
            command.Parameters.AddWithValue("@itemnmbr", ITEMNMBR_Def);
            command.Parameters.AddWithValue("@rowvyrobek_ID_L", rowvyrobek_ID_L);


            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += "AND tp.ITEMNMBR_fol like '%' + @filtr_ITEMNMBR + '%' ";
                command.Parameters.AddWithValue("@filtr_ITEMNMBR", filtr.MaterialITEMNMBR);
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += "AND tp.DESC_fol like '%' + @filtr_ITEMDESC + '%' ";
                command.Parameters.AddWithValue("@filtr_ITEMDESC", filtr.MaterialITEMDESC);
            }

            if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            {
                command.CommandText += "AND tp.MJ_fol like '%' + @MJ + '%' ";
                command.Parameters.AddWithValue("@MJ", filtr.MaterialMJ);
            }


            adapter.SelectCommand = command;
            adapter.Fill(ds.FASK_Vyroba_TP_Material);

            return ds;

        }


        #endregion

        #region IVazby2_GetFiltrovanyVazbyVyrobky Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazbyVyrobky(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr)
        {

            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            command.CommandText =
                "SELECT DISTINCT tp.ID_H, tp.PUO, tp.ID_L, tp.ID, tp.ITEMNMBR_Def ITEMNMBR, tp.DESC_Def ITEMDESC, tp.MJ_Def MJ, sklad.SKL_ID as SKL_ID , sklad.skl_desc as SKL_DESC" +

                " , Z.ITEMCODE" +
                " , Z.VNDITNUM" +

                " , POHODA_CleneniSklad.Vetev1 " +
                " , POHODA_CleneniSklad.Vetev2 " +
                " , POHODA_CleneniSklad.Vetev3 " +
                " , POHODA_CleneniSklad.Vetev4 " +
                " , POHODA_CleneniSklad.Vetev5 " +
                " , POHODA_CleneniSklad.Vetev6 " +
                " , POHODA_CleneniSklad.Vetev7 " +

            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_TP + " as tp " +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z ON Z.ITEMNMBR = tp.ITEMNMBR_Def" +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = Z.SKL_ID" +

            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKz as POHODA_Zasoby ON POHODA_Zasoby.ID = Z.ITEMNMBR " +
            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKSt as POHODA_CleneniSklad ON POHODA_CleneniSklad.RefSklad = sklad.skl_id AND POHODA_CleneniSklad.ID =  POHODA_Zasoby.RefStruct " +

            " WHERE (tp.ID_H IS NULL)";

            // TODO jak sem dostat itemnmbr??
            //command.Parameters.AddWithValue("@In_ITEMNMBR", ITEMNMBR_Materialy);

            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += " AND tp.ITEMNMBR_Def like '%' + @filtr_ITEMNMBR + '%' ";
                command.Parameters.AddWithValue("@filtr_ITEMNMBR", filtr.MaterialITEMNMBR);
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += " AND tp.DESC_Def like '%' + @filtr_ITEMDESC + '%' ";
                command.Parameters.AddWithValue("@filtr_ITEMDESC", filtr.MaterialITEMDESC);
            }

            if (!string.IsNullOrEmpty(filtr.MaterialCarKod))
            {
                command.CommandText += " AND tp.MJ_Def like '%' + @filtr_MJ + '%' ";
                command.Parameters.AddWithValue("@filtr_MJ", filtr.MaterialCarKod);
            }


            adapter.SelectCommand = command;
            adapter.Fill(ds.FASK_Vyroba_TP_Vyrobek);

            return ds;
        }


        #endregion

        #region IVazby2_Insert_VazbyAddVyrobky Members

        public int Insert_VazbyAddVyrobky(
            string ID_H,
            string ID_L,
            string ITEMNMBR_Def,
            string DESC_Def,
            string MJ_Def,
            string ITEMNMBR_fol,
            string DESC_Fol,
            string MJ_Fol,
            string koef,
            string ID_USER,
            DateTime? dateedit,
            string alter,
            string PUO)
        {
            Globals_V1.LoadConfiguration();
            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


            return tpta.Insert(
                ID_H,
                ID_L,
                ITEMNMBR_Def,
                DESC_Def,
                MJ_Def,
                ITEMNMBR_fol,
                DESC_Fol,
                MJ_Fol,
                koef,
                ID_USER,
                dateedit,
                alter,
                PUO
                );
        }

        #endregion

        #region IVazby2_Scalar_MAX_IDL Members

        public int? Scalar_MAX_IDL()
        {
            Globals_V1.LoadConfiguration();
            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            object tmp = tpta.Scalar_MAX_IDL();

            if ((tmp != null) && (tmp is int))
            {
                return tmp as int?;
            }
            else
                return null;


        }

        #endregion

        #region IVazby2_Scalar_Vyrobky_Count Members

        public int? Scalar_Vyrobky_Count(string ITEMNMBR)
        {
            Globals_V1.LoadConfiguration();
            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            object tmp = tpta.Scalar_Vyrobky_Count(ITEMNMBR);

            if ((tmp != null) && (tmp is int?))
            {
                return tmp as int?;
            }
            else
                return null;

        }

        #endregion

        #region IVazby2_DeleteVyrobek Members

        public void DeleteVyrobek(string ITEMNMBR)
        {

            //Globals_V1.LoadConfiguration();
            //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_VyrobekTableAdapter taTPvyrobek = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_VyrobekTableAdapter();
            //taTPvyrobek.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //taTPvyrobek.DeleteQuery(ITEMNMBR);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_FASK_Vyroba_TP.DeleteVyrobek(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ITEMNMBR);

        }

        #endregion

        #region IVazby2_UpdateEdit Members


        public void UpdateEdit(string koef, string ID_USER, DateTime? dateedit, string alternaiva, string ITEMNMBR_Def, string ITEMNMBR_fol)
        {
            Globals_V1.LoadConfiguration();
            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            tpta.UpdateEdit(koef, ID_USER, dateedit, alternaiva, ITEMNMBR_Def, ITEMNMBR_fol);
        }

        #endregion

        #region IVazby2_Fill_onlyALTERtable Members

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_onlyALTERtable(string ITEMNMBR_Def)
        {
            Globals_V1.LoadConfiguration();
            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable tmp_dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_TPDataTable dt = tpta.GetDataBy_onlyALTERtable(ITEMNMBR_Def);

            foreach (var item in dt)
            {
                tmp_dt.ImportRow(item);
            }


            return tmp_dt;
        }

        #endregion

        #region IVazby2_Fill_only_Koef_and_Alter Members

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_only_Koef_and_Alter(string ITEMNMBR_Def, string ITEMNMBR_fol)
        {
            Globals_V1.LoadConfiguration();
            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable tmp_dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);



            Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_TPDataTable dt = tpta.GetDataBy_only_Koef_and_Alter(ITEMNMBR_Def, ITEMNMBR_fol);

            foreach (var item in dt)
            {
                tmp_dt.ImportRow(item);
            }


            return tmp_dt;

        }

        #endregion

        #region IVazby2_DeleteMaterial Members

        public void DeleteMaterial(int ID)
        {
            //Globals_V1.LoadConfiguration();
            //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter taTPmaterial = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_TP_MaterialTableAdapter();
            //taTPmaterial.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //taTPmaterial.DeleteQuery(ID);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_FASK_Vyroba_TP.DeleteMaterial(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ID);
        }

        #endregion

        #region IVazby2_ImportVydejkaPohoda Members

        void Fask.Interfaces.Vazby.IVazby2_Import_TP2PS.Import_TP2PS(int countEntries, string SKL_ID, string userID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps = null;
                dt_ps = Database.Vydej.GET_PS_from_TP(countEntries, SKL_ID, userID);
                Database.Vydej.UPDATEDATA_ProductionSources(dt_ps);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region IVazby2_Modifikace_TP_GetFiltrovaneData Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovaneData(Fask.Interfaces.Filtry.VazbyModifikace_TP filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                #region Puvodny dotaz
                //                #region Command

                ////                SELECT TP.ITEMNMBR_Def, TP.DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID
                ////FROM
                ////    (Select T.ITEMNMBR_Def,T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as  SKL_ID_Fol  
                ////        from FASK_Vyroba_TP as T left join FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR  
                ////            where ITEMNMBR_fol is not null 
                ////            --AND ITEMNMBR_Def = @ITEMNMBR_Def
                ////            --Group by T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol,zas.ITEMCODE, zas.VNDITNUM, zas.SKL_ID 
                ////            ) as TP
                ////left JOIN
                ////    (select ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID from FASK_ZASOBY where SKL_ID = @SKL_ID) as Z
                ////ON TP.DESC_Fol = Z.ITEMDESC 


                //                #endregion

                //                command.CommandText = " SELECT TP.ITEMNMBR_Def, TP.DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID";

                //                command.CommandText += " FROM";
                //                command.CommandText += " (SELECT T.ITEMNMBR_Def,T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol";
                //                command.CommandText += " FROM FASK_Vyroba_TP as T left join FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR";
                //                command.CommandText += " WHERE ITEMNMBR_fol is not null";

                //                if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                //                {
                //                    command.CommandText += " AND ITEMNMBR_Def ='" + filtr.ITEMNMBR.Trim() + "' ";
                //                }


                //                command.CommandText += " ) as TP";

                //                command.CommandText += " LEFT JOIN ";
                //                command.CommandText += "(select ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID from FASK_ZASOBY where SKL_ID = '" + filtr.SKL_ID.Trim() + "') as Z";
                //                command.CommandText += " ON TP.DESC_Fol = Z.ITEMDESC "; 
                #endregion

                #region Dotaz rozšiřen o duplicity


                #region Dotaz

// SELECT 
//    duplicity.pocet, TP.ITEMNMBR_Def, TP.DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID 
// FROM 
//    (
//        SELECT T.ITEMNMBR_Def,T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol 
//        FROM FASK_Vyroba_TP as T left join FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR 
//        WHERE ITEMNMBR_fol is not null 
//    ) as TP 
//    LEFT JOIN (select ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID from FASK_ZASOBY where SKL_ID = @SKlad) as Z 
//        ON TP.DESC_Fol = Z.ITEMDESC 
//    left join ( 
//        SELECT DESC_Fol, DESC_Def, COUNT(DESC_Fol) pocet FROM (
//            SELECT TP.ITEMNMBR_Def, TP.DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID 
//            FROM (SELECT T.ITEMNMBR_Def,T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol 
//            FROM FASK_Vyroba_TP as T left join FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR 
//            WHERE ITEMNMBR_fol is not null ) as TP 
//            LEFT JOIN (select ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID from FASK_ZASOBY where SKL_ID = @SKlad) as Z 
//            ON TP.DESC_Fol = Z.ITEMDESC 
//        ) as FULLSELECT
//        Group by DESC_Fol, DESC_Def
//        --having COUNT(DESC_Fol) > 1
//        ) duplicity on duplicity.DESC_Def=tp.DESC_Def and duplicity.DESC_Fol=tp.DESC_Fol
//--where duplicity.pocet > 1

                #endregion

                command.CommandText = "	SELECT"; //1
                command.CommandText += " TP.ID as ID_Zdroj, duplicity.pocet as PocetVyskytu, TP.ITEMNMBR_Def, TP.DESC_Def, duplicity.SKL_ID_Def, duplicity.SKL_DESC_Def, TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol, Z.ITEMCODE, TP.VNDITNUM_Fol, Z.VNDITNUM, TP.MJ_Fol, Z.MJ, TP.SKL_ID_Fol, Z.SKL_ID, skladZdroj.skl_desc as SKL_DESC_Fol, skladcil.skl_desc as SKL_DESC "; //2
                command.CommandText += " FROM";//3
                command.CommandText += " (";//4
                command.CommandText += " SELECT";//5
                command.CommandText += " T.ID, T.ITEMNMBR_Def, T.DESC_Def, T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol , zas_Vyr.SKL_ID as SKL_ID_Def"; //6
                command.CommandText += " FROM FASK_Vyroba_TP as T"; //7
                command.CommandText += " LEFT JOIN FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR";//8
                command.CommandText += " LEFT JOIN FASK_ZASOBY as zas_Vyr ON T.ITEMNMBR_Def = zas_Vyr.ITEMNMBR"; //9
                command.CommandText += " WHERE ITEMNMBR_fol is not null"; //10

                if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                {
                    command.CommandText += " AND ITEMNMBR_Def ='" + filtr.ITEMNMBR.Trim() + "' "; //11
                }

                command.CommandText += " ) as TP";//12
                command.CommandText += " LEFT JOIN";//13
                command.CommandText += " (";//14
                command.CommandText += " SELECT ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID"; //15
                command.CommandText += " FROM FASK_ZASOBY";//16
                command.CommandText += " WHERE SKL_ID = '" + filtr.SKL_ID.Trim() + "'";//17
                command.CommandText += " ) as Z  ON TP.DESC_Fol = Z.ITEMDESC";//18
                command.CommandText += " LEFT JOIN";//19
                command.CommandText += " (";//20
                command.CommandText += " SELECT DESC_Fol, DESC_Def, COUNT(DESC_Fol) pocet , SKL_DESC_Def, SKL_ID_Def";//21
                command.CommandText += " FROM";//22
                command.CommandText += " (";//23
                command.CommandText += " SELECT";//24
                command.CommandText += " TP.ID, TP.ITEMNMBR_Def, TP.DESC_Def,  TP.SKL_ID_Def, TP.SKL_DESC_Def,  TP.ITEMNMBR_fol, Z.ITEMNMBR, TP.DESC_Fol, Z.ITEMDESC, TP.ITEMCODE_Fol ,Z.ITEMCODE,TP.VNDITNUM_Fol ,Z.VNDITNUM, TP.MJ_Fol, Z.MJ , TP.SKL_ID_Fol, Z.SKL_ID";//25
                command.CommandText += " FROM";//26
                command.CommandText += " (";//27
                command.CommandText += " SELECT T.ID, T.ITEMNMBR_Def,T.DESC_Def,  zas_Vyr.SKL_ID as SKL_ID_Def, S_TP.skl_desc as SKL_DESC_Def ,T.ITEMNMBR_fol, T.DESC_Fol, T.MJ_Fol, zas.ITEMCODE as ITEMCODE_Fol, zas.VNDITNUM as VNDITNUM_Fol, zas.SKL_ID as SKL_ID_Fol  ";//28
                command.CommandText += " FROM FASK_Vyroba_TP as T";//29
                command.CommandText += " LEFT JOIN FASK_ZASOBY as zas ON T.ITEMNMBR_fol = zas.ITEMNMBR";//30
                command.CommandText += " LEFT JOIN FASK_ZASOBY as zas_Vyr ON T.ITEMNMBR_Def = zas_Vyr.ITEMNMBR";//31
                command.CommandText += " LEFT JOIN CZMST093 S_TP on S_TP.skl_id = zas_Vyr.SKL_ID";//32
                command.CommandText += " WHERE ITEMNMBR_fol is not null ";//33
                command.CommandText += " ) as TP";//34
                command.CommandText += " LEFT JOIN";//35
                command.CommandText += " (";//36
                command.CommandText += " SELECT ITEMNMBR, ITEMDESC, MJ, ITEMCODE, VNDITNUM, SKL_ID";//37
                command.CommandText += " FROM FASK_ZASOBY";//38
                command.CommandText += " WHERE SKL_ID = '" + filtr.SKL_ID.Trim() + "'";//39
                command.CommandText += " ) as Z  ON TP.DESC_Fol = Z.ITEMDESC";//40
                command.CommandText += " ) as FULLSELECT";//41
                command.CommandText += " Group by DESC_Fol, DESC_Def, SKL_DESC_Def, SKL_ID_Def";//42
                command.CommandText += " ) duplicity on duplicity.DESC_Def=tp.DESC_Def and duplicity.DESC_Fol=tp.DESC_Fol and duplicity.SKL_ID_Def = tp.SKL_ID_Def";//43
                

                //Rozšiřeno o popis skladu
                command.CommandText += " LEFT JOIN CZMST093 skladZdroj on skladZdroj.skl_id = TP.SKL_ID_Fol";//44
                command.CommandText += " LEFT JOIN CZMST093 skladcil on skladcil.skl_id = Z.SKL_ID";//45


                if (filtr.Shodne)
                {
                    command.CommandText += " WHERE Z.SKL_ID is not null ";
                }


                if (filtr.NEShodne)
                {
                    command.CommandText += " WHERE Z.SKL_ID is null ";
                }

                #endregion

                adapter.SelectCommand = command;
                adapter.Fill(ds.Modifikace_TP);

                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IVazby2_Modifikace_TP_SetZamenZdrojCil Members

        public Fask.Interfaces.Classes.StatusInfo SetZamenZdrojCil(Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable dt)
        {
            Fask.Interfaces.Classes.StatusInfo si = new Fask.Interfaces.Classes.StatusInfo();

            try
            {
                List<DataProZamenu> list = new List<DataProZamenu>();

                foreach (Fask.Interfaces.DataSets.Vyroba.Modifikace_TPRow item in dt)
                {
                    list.Add(new DataProZamenu(item.ID_Zdroj, item.ITEMNMBR,item.ITEMDESC, item.MJ));
                }

                Vazby.Update_FASK_Vyroba_TP_MaterialyZamena(list);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            si.ID = 0;

            return si;
        }

        #endregion



    }
}
