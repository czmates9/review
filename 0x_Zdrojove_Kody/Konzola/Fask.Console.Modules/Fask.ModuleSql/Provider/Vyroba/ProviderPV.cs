using System;
using System.Collections.Generic;
using System.Data;
//using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;
using Fask.Interfaces.Vyroba.PV;
using Fask.Logging;
//using Fask.POHODA.Disponibility;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.PV.IPV,
        Fask.Interfaces.Vyroba.PV.IPV_GetFiltrovanyPVList,
        Fask.Interfaces.Vyroba.PV.IPV_ImportPV,
                                              Fask.Interfaces.Vyroba.PV.IPV_GetZaplanovani,
                                              Fask.Interfaces.Vyroba.PV.IPV_Insert_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_Edit_Zaplanovane,
                                              Fask.Interfaces.Vyroba.PV.IPV_Get_Zaplanovane,
                                              Fask.Interfaces.Vyroba.PV.IPV_Update_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_Delete_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_Edit_QTY_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_Update_QTY_ByID_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_Insert_PVH,
                                              Fask.Interfaces.Vyroba.PV.IPV_Edit_RefPVH_in_PVP,
                                              Fask.Interfaces.Vyroba.PV.IPV_GetDEX_ROW_ID_from_PVH,
                                              Fask.Interfaces.Vyroba.PV.IPV_Zaplanovani_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_GetSKzInfo_PV,
                                              Fask.Interfaces.Vyroba.PV.IPV_FillByRefPVH,
                                              Fask.Interfaces.Vyroba.PV.IPV_FillPVH,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_FillStav,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetStav_ParamsNastaveni,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertVariantu,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_InitEmptyParams,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_InsertParams,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_DeleteVariantu,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateParams,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetRow_Parametry,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateVariantu,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_Disponibilita,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_GetDetailStav,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_UpdateNaplanovane,
                                              Fask.Interfaces.Vyroba.PV.IPV_Navrh_PARAMS_OnLine_FIFO_OBJ
    {

        #region Import dat

        public string ImportPV()
        {
            ExportKatalogPV_Procedura();


            return "OK";
        }

        private string ExportKatalogPV_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_PlanovaniVyroby");
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

            return "OK";
        }

        public List<Tuple<string, string, bool>> GetPVTableInfo()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPV_GetFiltrovanyPVList Members

        public Fask.Interfaces.DataSets.Vyroba_Planovani GetFiltrovanyPVList(Fask.Interfaces.Filtry.Vyroba_PV_Filtr filtr)
        {
            
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba_Planovani vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba_Planovani();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            command.CommandText = string.Empty;

            #region Puvodny

            //command.CommandText += "Select p.* , h.SOPNUMBE, h.SOPDESC from FASK_Vyroba_PVP as p ";
            //command.CommandText += "left join FASK_Vyroba_PVH as h ON h.DEX_ROW_ID = p.Ref_PVH ";

            #endregion


            #region Novy

            #region Puvodny SQL


            //SELECT
            //p.*, h.SOPNUMBE, h.SOPDESC, h.TypDok
            //, ID_ROW =
            //CASE h.TypDok
            //    WHEN 'Vydej' THEN se.DEX_ROW_ID
            //    WHEN 'Vyroba' THEN vpp.DEX_ROW_ID
            //END
            //FROM
            //(

            ///*************/
            //--Varianta NIC
            ///**************/

            //SELECT
            //0 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 0
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is null

            //UNION

            ///*************/
            //--Varianta, Neoznačeno, ale nachystano množství cele
            ///**************/

            //SELECT
            //1 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 0
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRPS_QTY = 0

            //UNION

            ///*************/
            //--Varianta, Neoznačeno, ale nachystano množství NE cele
            ///**************/

            //SELECT
            //2 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 0
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRPS_QTY != 0

            //UNION

            ///*************/
            //--Varianta Označeno, ale CELE
            ///**************/

            //SELECT
            //3 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 1
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRPS_QTY = 0

            //UNION

            ///*************/
            //--Varianta Označeno, ale NE CELE
            ///**************/

            //SELECT
            //4 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 1
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRPS_QTY != 0

            //UNION

            ///*************/
            //--Varianta Zaplanovano, CELE
            ///**************/

            //SELECT
            //5 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 1
            //AND VP_PRDCT_QTY is not null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRDCT_QTY = 0

            //UNION
            ///*************/
            //--Varianta Zaplanovano, ale NE CELE
            ///**************/

            //SELECT
            //6 as SP, *
            //FROM FASK_Vyroba_PVP
            //where VP_PRPS = 1
            //AND VP_PRDCT_QTY is null
            //AND VP_PRPS_QTY is not null
            //AND QTY - VP_PRDCT_QTY != 0



            //) as p
            //left join FASK_Vyroba_PVH as h ON h.DEX_ROW_ID = p.Ref_PVH
            //left join CZMST_SE as se ON se.CountEntries = h.SOPNUMBE AND se.ITEMNMBR = p.ITEMNMBR
            //left join CZPRO_VPP as vpp ON vpp.SOPNUMBE = h.SOPNUMBE AND vpp.ITEMNMBR = p.ITEMNMBR
            //where 1 = 1

            //Order by DEX_ROW_ID 

            #endregion


            #region SQL Script


//SELECT
//p.*, h.SOPNUMBE, h.SOPDESC, h.TypDok
//, ID_ROW =
//CASE h.TypDok
//WHEN 'Vydej' THEN se.DEX_ROW_ID
//WHEN 'Vyroba' THEN vpp.DEX_ROW_ID
//END
//,dbo.fask_func_planovani_VydejFilterToSI(p.ITEMNMBR, null, null) as Import_To_SI
//, zas.MJ
//FROM
//(

//SELECT
//0 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 0
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is null
//UNION

//SELECT
//1 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 0
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRPS_QTY = 0
//UNION

//SELECT
//2 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 0
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRPS_QTY != 0
//UNION

//SELECT
//3 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 1
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRPS_QTY = 0
//UNION

//SELECT
//4 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 1
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRPS_QTY != 0
//UNION

//SELECT
//5 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 1
//AND VP_PRDCT_QTY is not null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRDCT_QTY = 0
//UNION

//SELECT
//6 as _TYPE_ROW, *
//FROM FASK_Vyroba_PVP
//where VP_PRPS = 1
//AND VP_PRDCT_QTY is null
//AND VP_PRPS_QTY is not null
//AND QTY - VP_PRDCT_QTY != 0

//) as p
//left join FASK_Vyroba_PVH as h ON h.DEX_ROW_ID = p.Ref_PVH
//left join CZMST_SE as se ON se.CountEntries = h.SOPNUMBE AND se.ITEMNMBR = p.ITEMNMBR
//left join CZPRO_VPP as vpp ON vpp.SOPNUMBE = h.SOPNUMBE AND vpp.ITEMNMBR = p.ITEMNMBR
//left join FASK_ZASOBY as zas ON zas.ITEMNMBR = p.ITEMNMBR

//where 1 = 1

//order by DEX_ROW_ID

            #endregion

            command.CommandText += " SELECT " +
            " p.*, h.SOPNUMBE, h.SOPDESC, h.TypDok " +
            " , ID_ROW = " +
            " CASE h.TypDok " +
            " WHEN '" + Fask.Interfaces.Classes.ZaplanovanyDoklad.Vydej + "' THEN se.DEX_ROW_ID " +
            " WHEN '" + Fask.Interfaces.Classes.ZaplanovanyDoklad.Vyroba + "' THEN vpp.DEX_ROW_ID " +
            " END " +
            " ,dbo.fask_func_planovani_VydejFilterToSI(p.ITEMNMBR, null, null) as Import_To_SI " +
            " , zas.MJ " +
            " FROM  " +
            " (";

            //Zacatek
            /***********

            /*************/
            //--Varianta NIC
            /**************/
            command.CommandText += " SELECT " +
            " 0 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 0 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is null " +
            " UNION ";


            /*************/
            //--Varianta, Neoznačeno, ale nachystano množství cele
            /**************/
            command.CommandText += " SELECT " +
            " 1 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 0 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRPS_QTY = 0 " +
            " UNION ";


            /*************/
            //--Varianta, Neoznačeno, ale nachystano množství NE cele
            /**************/
            command.CommandText += " SELECT " +
            " 2 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 0 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRPS_QTY != 0 " +
            " UNION ";


            /*************/
            //--Varianta Označeno, ale CELE
            /**************/
            command.CommandText += " SELECT " +
            " 3 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 1 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRPS_QTY = 0 " +
            " UNION ";


            /*************/
            //--Varianta Označeno, ale NE CELE
            /**************/
            command.CommandText += " SELECT " +
            " 4 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 1 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRPS_QTY != 0 " +
            " UNION ";


            /*************/
            //--Varianta Zaplanovano, CELE
            /**************/
            command.CommandText += " SELECT " +
            " 5 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 1 " +
            " AND VP_PRDCT_QTY is not null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRDCT_QTY = 0 " +
            " UNION ";


            /*************/
            //--Varianta Zaplanovano, ale NE CELE
            /**************/
            command.CommandText += " SELECT " +
            " 6 as _TYPE_ROW, * " +
            " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +
            " where VP_PRPS = 1 " +
            " AND VP_PRDCT_QTY is null " +
            " AND VP_PRPS_QTY is not null " +
            " AND QTY - VP_PRDCT_QTY != 0 " +

            /*********/
            //Konec

            " ) as p " +
            " left join " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVH + " as h ON h.DEX_ROW_ID = p.Ref_PVH " +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SE + " as se ON se.CountEntries = h.SOPNUMBE AND se.ITEMNMBR = p.ITEMNMBR " +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP + " as vpp ON vpp.SOPNUMBE = h.SOPNUMBE AND vpp.ITEMNMBR = p.ITEMNMBR " +
            " left join " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as zas ON zas.ITEMNMBR = p.ITEMNMBR ";
            #endregion




            command.CommandText += " where 1 = 1 ";


            //filtr.OBJ = tb_SOPNUMBE.Text.Trim();
            //filtr.ITEMNMBR = tb_ITEMNMBR.Text.Trim();
            //filtr.Firma = tb_Firma.Text.Trim();
            //filtr.Kod = tb_Kod.Text.Trim();
            //filtr.SOPNUMBE = tb_VyrZak.Text.Trim();

            //filtr.DatumDO = dtp_DatumDo.Value;
            //filtr.DatumOD = dtp_DatumOd.Value;

            //filtr.NEzaplanovane = chb_NEzap.Checked;
            //filtr.zaplanovane = chb_Zap.Checked;


            if (filtr.OBJ != null && !string.IsNullOrEmpty(filtr.OBJ.Trim()))
            {
                //command.CommandText += "AND p.OBJ_NMBR=@OBJ_NMBR ";
                command.CommandText += "AND p.OBJ_NMBR like @OBJ_NMBR + '%' ";
                command.Parameters.AddWithValue("@OBJ_NMBR", filtr.OBJ);
            }

            if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
            {
                command.CommandText += "AND p.ITEMNMBR=@ITEMNMBR ";
                command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
            }

            if (filtr.Firma != null && !string.IsNullOrEmpty(filtr.Firma.Trim()))
            {
                command.CommandText += "AND p.OBJ_COMPANY like @OBJ_COMPANY + '%' ";
                command.Parameters.AddWithValue("@OBJ_COMPANY", filtr.Firma);
            }

            if (filtr.Kod != null && !string.IsNullOrEmpty(filtr.Kod.Trim()))
            {
                command.CommandText += "AND p.ITEMCODE like @ITEMCODE + '%' ";
                //command.CommandText += "AND p.ITEMCODE=@ITEMCODE ";
                command.Parameters.AddWithValue("@ITEMCODE", filtr.Kod);
            }

            if (filtr.SOPNUMBE != null && !string.IsNullOrEmpty(filtr.SOPNUMBE.Trim()))
            {
                command.CommandText += "AND h.SOPNUMBE=@SOPNUMBE ";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE);
            }

            if (filtr.zaplanovane == false && filtr.NEzaplanovane == false)
            {
                //NIC nemuže najit 
                command.CommandText += "AND 1 = 2 ";
            }
            else if (filtr.zaplanovane == true && filtr.NEzaplanovane == false)
            {
                command.CommandText += "AND Ref_PVH is not null ";
            }
            else if (filtr.zaplanovane == false && filtr.NEzaplanovane == true)
            {
                command.CommandText += "AND Ref_PVH is null ";
            }


            if (filtr.DatumDO_Check == true && filtr.DatumOD_Check == true)
            {
                command.CommandText += "AND ( p.OBJ_DATE_FROM <= @OBJ_DATE_TO AND p.OBJ_DATE_TO >= @OBJ_DATE_FROM ) ";
                command.Parameters.AddWithValue("@OBJ_DATE_FROM", filtr.DatumOD);
                command.Parameters.AddWithValue("@OBJ_DATE_TO", filtr.DatumDO);
            }
            else if (filtr.DatumDO_Check == false && filtr.DatumOD_Check == true)
            {
                command.CommandText += "AND p.OBJ_DATE_TO >= @OBJ_DATE_FROM ";
                command.Parameters.AddWithValue("@OBJ_DATE_FROM", filtr.DatumOD);
            }
            else if (filtr.DatumDO_Check == true && filtr.DatumOD_Check == false)
            {
                command.CommandText += "AND p.OBJ_DATE_FROM <= @OBJ_DATE_TO ";
                command.Parameters.AddWithValue("@OBJ_DATE_TO", filtr.DatumDO);
            }

            if (filtr.Zaplanovano_OD != null && filtr.Zaplanovano_DO != null)
            {
                command.CommandText += " AND p.DATE_ZAPLANOVANI between @DATE_ZAPLANOVANIOD and @DATE_ZAPLANOVANIDO";
                command.Parameters.AddWithValue("@DATE_ZAPLANOVANIOD", filtr.Zaplanovano_OD);
                command.Parameters.AddWithValue("@DATE_ZAPLANOVANIDO", filtr.Zaplanovano_DO);
            }
            else
            {
                if (filtr.Zaplanovano_OD != null)
                {
                    command.CommandText += " AND p.DATE_ZAPLANOVANI > @DATE_ZAPLANOVANIOD";
                    command.Parameters.AddWithValue("@DATE_ZAPLANOVANIOD", filtr.Zaplanovano_OD);
                }
                else if (filtr.Zaplanovano_DO != null)
                {
                    command.CommandText += " AND p.DATE_ZAPLANOVANI < @DATE_ZAPLANOVANIDO";
                    command.Parameters.AddWithValue("@DATE_ZAPLANOVANIDO", filtr.Zaplanovano_DO);
                }
            }


            if (!string.IsNullOrEmpty(filtr.Zaplanovano_TimeVariant))
            {
                if (!filtr.Zaplanovano_TimeVariant.Contains("unknow"))
                {

                    var arr = filtr.Zaplanovano_TimeVariant.Split(';');
                    Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                    command.CommandText += " AND p.DATE_ZAPLANOVANI > @DATE_ZAPLANOVANI_TV";
                    command.Parameters.AddWithValue("@DATE_ZAPLANOVANI_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                }
            }


            //if (filtr.DatumDO_Check)
            //{
            //    command.CommandText += "AND p.OBJ_DATE_TO=@OBJ_DATE_TO ";
            //    string doo = filtr.DatumDO.ToShortDateString();
            //    command.Parameters.AddWithValue("@OBJ_DATE_TO", doo);
            //}

            //if (filtr.DatumOD_Check)
            //{
            //    command.CommandText += "AND p.OBJ_DATE_FROM=@OBJ_DATE_FROM ";
            //    string ood = filtr.DatumOD.ToShortDateString();
            //    command.Parameters.AddWithValue("@OBJ_DATE_FROM", ood);
            //}


            //command.CommandText += " order by OBJ_NMBR "; // JaS si to rozmyslel...
            command.CommandText += " order by DEX_ROW_ID ";
            



            vyrobaDataSet1.FASK_Vyroba_PVP.Clear();
            vyrobaDataSet1.FASK_Vyroba_PVP.AcceptChanges();
            vyrobaDataSet1.FASK_Vyroba_PVP.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.FASK_Vyroba_PVP);
            vyrobaDataSet1.FASK_Vyroba_PVP.EndLoadData();

            return vyrobaDataSet1;
        }

        public Vyroba_Planovani GetZaplanovani(Vyroba_PV_Zap_Filtr filter)
        {
            var ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

            try
            {
                using (var conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                using (var da = new System.Data.SqlClient.SqlDataAdapter("dbo.FASK_proc_EXPORT_SQL_FASK_NacteniOPlanu", conn))
                {
                    da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    // Bool
                    da.SelectCommand.Parameters.AddWithValue("@Nezaplanovane", filter.Nezaplanovane);
                    // tady doplnit nově:
                    da.SelectCommand.Parameters.AddWithValue("@PohodaE1", false /* nebo false podle verze Pohody */);

                    // Datum OD (o.DatOd)
                    da.SelectCommand.Parameters.AddWithValue("@OD_DatumOD", filter.OD_DatumOD);
                    da.SelectCommand.Parameters.AddWithValue("@OD_DatumDO", filter.OD_DatumDO);
                    da.SelectCommand.Parameters.AddWithValue("@OD_DatumOD_Check", filter.OD_DatumOD_Check);
                    da.SelectCommand.Parameters.AddWithValue("@OD_DatumDO_Check", filter.OD_DatumDO_Check);

                    // Datum DO (o.DatDo)
                    da.SelectCommand.Parameters.AddWithValue("@DO_DatumOD", filter.DO_DatumOD);
                    da.SelectCommand.Parameters.AddWithValue("@DO_DatumDO", filter.DO_DatumDO);
                    da.SelectCommand.Parameters.AddWithValue("@DO_DatumOD_Check", filter.DO_DatumOD_Check);
                    da.SelectCommand.Parameters.AddWithValue("@DO_DatumDO_Check", filter.DO_DatumDO_Check);

                    // Datum ZAP (o.Datum)
                    da.SelectCommand.Parameters.AddWithValue("@ZAP_DatumOD", filter.ZAP_DatumOD);
                    da.SelectCommand.Parameters.AddWithValue("@ZAP_DatumDO", filter.ZAP_DatumDO);
                    da.SelectCommand.Parameters.AddWithValue("@ZAP_DatumOD_Check", filter.ZAP_DatumOD_Check);
                    da.SelectCommand.Parameters.AddWithValue("@ZAP_DatumDO_Check", filter.ZAP_DatumDO_Check);

                    // pomocná funkce – NULL → "", proc si to ošetří sama
                    string Txt(string s) => s ?? string.Empty;

                    // Textové filtry
                    da.SelectCommand.Parameters.AddWithValue("@OBJ", Txt(filter.OBJ));
                    da.SelectCommand.Parameters.AddWithValue("@Kod", Txt(filter.Kod));
                    da.SelectCommand.Parameters.AddWithValue("@Firma", Txt(filter.Firma));
                    da.SelectCommand.Parameters.AddWithValue("@FormaUhrady", Txt(filter.FormaUhrady));

                    // UserParam_1–5
                    da.SelectCommand.Parameters.AddWithValue("@UserParam_1", Txt(filter.UserParam_1));
                    da.SelectCommand.Parameters.AddWithValue("@UserParam_2", Txt(filter.UserParam_2));
                    da.SelectCommand.Parameters.AddWithValue("@UserParam_3", Txt(filter.UserParam_3));
                    da.SelectCommand.Parameters.AddWithValue("@UserParam_4", Txt(filter.UserParam_4));
                    da.SelectCommand.Parameters.AddWithValue("@UserParam_5", Txt(filter.UserParam_5));

                    da.Fill(ds.PV_Zaplanovani);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("GetZaplanovani", "GetZaplanovani", ex);
                Fask.Logging.ExceptionHandler2.Handle(ds);

                // vrátím prázdný dataset (není null, jen bez řádků)
                return null;
            }

            return ds;
        }


        public bool Insert(string OBJ_NMBR, string OBJ_DESC, string OBJ_TYPE, string OBJ_COMPANY, DateTime? OBJ_DATE_FROM, DateTime? OBJ_DATE_TO, int? OBJ_ORD, int? OBJ_ITEM_ORD, string ITEMNMBR, string ITEMDESC, string ITEMCODE, decimal QTY, DateTime? DATE_ZAPLANOVANI, bool VP_PRPS, decimal? VP_PRPS_QTY, string VP_PRPS_SOPNUMBE, decimal? VP_PRDCT_QTY, int? USERID, int? Ref_PVH)
        {
            throw new NotImplementedException();
        }

        public bool Edit_Zaplanovane(int ORD_OBJpol, decimal? QTY, int? Flag)
        {
            throw new NotImplementedException();
        }

        public decimal? Get_Zaplanovane(int ORD_OBJpol)
        {
            throw new NotImplementedException();
        }

        public bool Update(Vyroba_Planovani.FASK_Vyroba_PVPRow dt)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Vyroba_Planovani.FASK_Vyroba_PVPRow dt, decimal JizZaplanovano)
        {
            throw new NotImplementedException();
        }

        public bool Edit_QTY(Vyroba_Planovani.FASK_Vyroba_PVPRow row, decimal? QTY, bool Flag, string VP_PRPS_SOPNUMBE)
        {
            throw new NotImplementedException();
        }

        public bool Update_QTY_ByID_PV(int ID, decimal? VP_PRPS_QTY, bool VP_PRPS, string VP_PRPS_SOPNUMBE)
        {
            throw new NotImplementedException();
        }

        public bool Insert_PVH(string SOPNUMBE, string SOPTYPE, string SOPDESC, string BarcodeH, byte Active, ZaplanovanyDoklad zaplanovanyDoklad)
        {
            throw new NotImplementedException();
        }

        public bool Edit_RefPVH_in_PVP(int DEXROWID_PVP, int? RefPVH)
        {
            throw new NotImplementedException();
        }

        public int? GetDEX_ROW_ID_from_PVH(string SOPNUMBE, ZaplanovanyDoklad zaplanovanyDoklad)
        {
            throw new NotImplementedException();
        }

        public bool Zaplanovani_PV(Vyroba_Planovani.FASK_Vyroba_PVPRow row, int DexRowID_PVH, decimal JizZaplanovano)
        {
            throw new NotImplementedException();
        }

        public void GetSKzInfo_PV(string ITEMNMBR, out Vyroba.Pohoda_SKz_VPPRow p)
        {
            throw new NotImplementedException();
        }

        public bool FillByRefPVH(Vyroba_Planovani ds, int Ref_PVH)
        {
            throw new NotImplementedException();
        }

        public bool FillPVH(Vyroba_Planovani ds)
        {
            throw new NotImplementedException();
        }

        public bool FillStav(Vyroba_Planovani ds)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani GetStav_ParamsNastaveni(Guid G)
        {
            throw new NotImplementedException();
        }

        public void InsertVariantu(Guid G, string nazev)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani InitEmptyParams(Guid G)
        {
            throw new NotImplementedException();
        }

        public void InsertParams(Vyroba_Planovani dtParams)
        {
            throw new NotImplementedException();
        }

        public void DeleteVariantu(Guid G)
        {
            throw new NotImplementedException();
        }

        public void UpdateParams(Vyroba_Planovani dtParams)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow GetRow_Parametry(Guid G)
        {
            throw new NotImplementedException();
        }

        public void UpdateVariantu(Guid G, string nazev)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable PARAMS_Disponibilita(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani GetDetailStav(string ITEMCODE)
        {
            throw new NotImplementedException();
        }

        public void UpdateNaplanovane(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        {
            throw new NotImplementedException();
        }

        public Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable PARAMS_OnLine_FIFO_OBJ(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 24.11.2025 MaR doimplementovat pokud JaS zada

        //#region IPV_GetZaplanovani Members

        //public Fask.Interfaces.DataSets.Vyroba_Planovani GetZaplanovani(Fask.Interfaces.Filtry.Vyroba_PV_Zap_Filtr filter)
        //{



        //    Fask.Interfaces.DataSets.Vyroba_Planovani ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();

        //    var da = new System.Data.SqlClient.SqlDataAdapter();
        //    try
        //    {
        //        da.SelectCommand = new System.Data.SqlClient.SqlCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = @"SELECT " +

        //            "op.RefSKz as ITEMNMBR " +
        //            ", op.SText as ITEMDESC " +
        //            ", op.Kod as ITEMCODE " +

        //            ", o.Firma as OBJ_COMPANY " +

        //            ", o.Cislo as OBJ_NMBR " +
        //            ", o.SText as OBJ_DESC " +
        //            ", o.RelTpObj as OBJ_TYPE " +

        //            ", op.Mnozstvi as QTY " +
        //            ", op.VPrQTY as QTY_Zaplanovano " +

        //            ", ( op.Mnozstvi - ISNULL(op.VPrQTY, 0)) as QTY_Zbyva " +

        //            ", o.ID as OBJ_ORD " +
        //            ", op.ID as OBJ_ITEM_ORD " +

        //            ", o.DatOd as OBJ_DATE_FROM " +
        //            ", o.DatDo as OBJ_DATE_TO " +

        //            ", o.Datum as OBJ_DATE_ZAPL " +

        //            ", o.RelForUh as OBJ_ForUh " +
        //            ", fu.IDS as OBJ_ForUh_IDS " +

        //            ", x.UserParam_1" + " as UserParam_1" +
        //            ", x.UserParam_2" + " as UserParam_2" +
        //            ", x.UserParam_3" + " as UserParam_3" +
        //            ", x.UserParam_4" + " as UserParam_4" +
        //            ", x.UserParam_5" + " as UserParam_5" +

        //            " from " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda + ".dbo.OBJ as o " +
        //            " left join " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda + ".dbo.OBJpol as op ON op.RefAg = o.ID " +
        //            " left join " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda + ".dbo.sFormUh as fu ON fu.ID = o.RelForUh " +

        //            " left join (SELECT ORD, UserParam_1, UserParam_2, UserParam_3, UserParam_4, UserParam_5  FROM FASK_Get_Planovani_NacteniUserParams()) x on x.ORD = op.ID " +

        //            " where RelTpObj = 1 " +
        //            " AND o.Vyrizeno = 0 " +
        //            " AND op.RefSKz IS NOT NULL ";

        //        if (filter.Nezaplanovane)
        //        {
        //            da.SelectCommand.CommandText += "AND  ((op.VPrQTY is null) OR ( op.VPrQTY <> op.Mnozstvi))  ";
        //        }

        //        if (filter.OBJ != null && !string.IsNullOrEmpty(filter.OBJ.Trim()))
        //        {
        //            da.SelectCommand.CommandText += "AND o.Cislo like @Cislo + '%'";
        //            da.SelectCommand.Parameters.AddWithValue("@Cislo", filter.OBJ);
        //        }

        //        if (filter.Kod != null && !string.IsNullOrEmpty(filter.Kod.Trim()))
        //        {
        //            da.SelectCommand.CommandText += "AND op.Kod like @Kod + '%' ";
        //            da.SelectCommand.Parameters.AddWithValue("@Kod", filter.Kod);
        //        }

        //        if (filter.Firma != null && !string.IsNullOrEmpty(filter.Firma.Trim()))
        //        {
        //            da.SelectCommand.CommandText += "AND o.Firma like @Firma + '%' ";
        //            da.SelectCommand.Parameters.AddWithValue("@Firma", filter.Firma);
        //        }

        //        if (filter.FormaUhrady != null && !string.IsNullOrEmpty(filter.FormaUhrady.Trim()))
        //        {
        //            da.SelectCommand.CommandText += "AND o.RelForUh in (" + filter.FormaUhrady + ") ";
        //        }

        //        #region Datum OD

        //        if (filter.OD_DatumDO_Check == true && filter.OD_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND  o.DatOd BETWEEN @ADatOd AND @ADatDo ";
        //            da.SelectCommand.Parameters.AddWithValue("@ADatOd", filter.OD_DatumOD);
        //            da.SelectCommand.Parameters.AddWithValue("@ADatDo", filter.OD_DatumDO);
        //        }
        //        else if (filter.OD_DatumDO_Check == true && filter.OD_DatumOD_Check == false)
        //        {
        //            da.SelectCommand.CommandText += "AND o.DatOd <= @ADatOd ";
        //            da.SelectCommand.Parameters.AddWithValue("@ADatOd", filter.OD_DatumDO);
        //        }
        //        else if (filter.OD_DatumDO_Check == false && filter.OD_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND o.DatOd >= @ADatOd ";
        //            da.SelectCommand.Parameters.AddWithValue("@ADatOd", filter.OD_DatumOD);
        //        }

        //        #endregion

        //        #region Datum DO

        //        if (filter.DO_DatumDO_Check == true && filter.DO_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND  o.DatDo BETWEEN @BDatDoOd AND @BDatDoDo ";
        //            da.SelectCommand.Parameters.AddWithValue("@BDatDoOd", filter.DO_DatumOD);
        //            da.SelectCommand.Parameters.AddWithValue("@BDatDoDo", filter.DO_DatumDO);
        //        }
        //        else if (filter.DO_DatumDO_Check == true && filter.DO_DatumOD_Check == false)
        //        {
        //            da.SelectCommand.CommandText += "AND o.DatDo <= @BDatDo ";
        //            da.SelectCommand.Parameters.AddWithValue("@BDatDoDo", filter.DO_DatumDO);
        //        }
        //        else if (filter.DO_DatumDO_Check == false && filter.DO_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND o.DatDo >= @BDatDoDo ";
        //            da.SelectCommand.Parameters.AddWithValue("@BDatDoDo", filter.DO_DatumOD);
        //        }

        //        #endregion

        //        #region Datum ZAP

        //        if (filter.ZAP_DatumDO_Check == true && filter.ZAP_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND  o.Datum BETWEEN @ADatum AND @BDatum ";
        //            da.SelectCommand.Parameters.AddWithValue("@ADatum", filter.ZAP_DatumOD);
        //            da.SelectCommand.Parameters.AddWithValue("@BDatum", filter.ZAP_DatumDO);
        //        }
        //        else if (filter.ZAP_DatumDO_Check == true && filter.ZAP_DatumOD_Check == false)
        //        {
        //            da.SelectCommand.CommandText += "AND o.Datum <= @Datum ";
        //            da.SelectCommand.Parameters.AddWithValue("@Datum", filter.ZAP_DatumDO);
        //        }
        //        else if (filter.ZAP_DatumDO_Check == false && filter.ZAP_DatumOD_Check == true)
        //        {
        //            da.SelectCommand.CommandText += "AND o.Datum >= Datum ";
        //            da.SelectCommand.Parameters.AddWithValue("@Datum", filter.ZAP_DatumOD);
        //        }

        //        #endregion


        //        if (filter.UserParam_1 != null && !string.IsNullOrEmpty(filter.UserParam_1.Trim()))
        //        {
        //            da.SelectCommand.CommandText += " AND x.UserParam_1 = @UserParam_1";
        //            da.SelectCommand.Parameters.AddWithValue("@UserParam_1", filter.UserParam_1);
        //        }

        //        if (filter.UserParam_2 != null && !string.IsNullOrEmpty(filter.UserParam_2.Trim()))
        //        {
        //            da.SelectCommand.CommandText += " AND x.UserParam_1 = @UserParam_1";
        //            da.SelectCommand.Parameters.AddWithValue("@UserParam_1", filter.UserParam_5);
        //        }

        //        if (filter.UserParam_3 != null && !string.IsNullOrEmpty(filter.UserParam_3.Trim()))
        //        {
        //            da.SelectCommand.CommandText += " AND x.UserParam_1 = @UserParam_1";
        //            da.SelectCommand.Parameters.AddWithValue("@UserParam_1", filter.UserParam_3);
        //        }

        //        if (filter.UserParam_4 != null && !string.IsNullOrEmpty(filter.UserParam_4.Trim()))
        //        {
        //            da.SelectCommand.CommandText += " AND x.UserParam_1 = @UserParam_1";
        //            da.SelectCommand.Parameters.AddWithValue("@UserParam_1", filter.UserParam_4);
        //        }

        //        if (filter.UserParam_5 != null && !string.IsNullOrEmpty(filter.UserParam_5.Trim()))
        //        {
        //            da.SelectCommand.CommandText += " AND x.UserParam_1 = @UserParam_1";
        //            da.SelectCommand.Parameters.AddWithValue("@UserParam_1", filter.UserParam_5);
        //        }

        //        da.SelectCommand.CommandText += "order by o.Cislo ";



        //        da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

        //        da.Fill(ds.PV_Zaplanovani);


        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle("GetFiltrovanyPVList", " GetFiltrovanyPVList", ex);
        //        Fask.Logging.ExceptionHandler2.Handle(ds);
        //        return null;
        //    }

        //    return ds;

        //}

        //#endregion

        //#region IPV_Insert_PV Members

        //public bool Insert(
        //            string OBJ_NMBR,
        //            string OBJ_DESC,
        //            string OBJ_TYPE,
        //            string OBJ_COMPANY,
        //            DateTime? OBJ_DATE_FROM,
        //            DateTime? OBJ_DATE_TO,
        //            int? OBJ_ORD,
        //            int? OBJ_ITEM_ORD,
        //            string ITEMNMBR,
        //            string ITEMDESC,
        //            string ITEMCODE,
        //            decimal QTY,
        //            DateTime? DATE_ZAPLANOVANI,
        //            bool VP_PRPS,
        //            decimal? VP_PRPS_QTY,
        //            string VP_PRPS_SOPNUMBE,
        //            decimal? VP_PRDCT_QTY,
        //            int? USERID,
        //    int? Ref_PVH)
        //{
        //    try
        //    {


        //        using (SqlConnection connection = new SqlConnection(ConnectionString))
        //        {
        //            String query  = @"INSERT INTO [FASK_Vyroba_PVP] (" + 
        //                " [OBJ_NMBR]," + " [OBJ_DESC]," + " [OBJ_TYPE]," + " [OBJ_COMPANY]," + " [OBJ_DATE_FROM]," +
        //                " [OBJ_DATE_TO]," + " [OBJ_ORD]," + " [OBJ_ITEM_ORD]," + " [ITEMNMBR]," + " [ITEMDESC]," +
        //                " [ITEMCODE]," + " [QTY]," + " [DATE_ZAPLANOVANI]," + " [VP_PRPS]," + " [VP_PRPS_QTY]," +
        //                " [VP_PRPS_SOPNUMBE]," + " [VP_PRDCT_QTY]," + " [USERID]," + " [Ref_PVH]" +
        //                " ) VALUES (" +
        //                " @OBJ_NMBR," + " @OBJ_DESC," + " @OBJ_TYPE," + " @OBJ_COMPANY," + " @OBJ_DATE_FROM," +
        //                " @OBJ_DATE_TO," + " @OBJ_ORD," + " @OBJ_ITEM_ORD," + " @ITEMNMBR," + " @ITEMDESC," +
        //                " @ITEMCODE," + " @QTY," + " @DATE_ZAPLANOVANI," + " @VP_PRPS," + " @VP_PRPS_QTY," +
        //                " @VP_PRPS_SOPNUMBE," + " @VP_PRDCT_QTY," + " @USERID," + " @Ref_PVH" + 
        //                " )";

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@OBJ_NMBR", OBJ_NMBR == null ? (object)DBNull.Value : OBJ_NMBR);
        //                command.Parameters.AddWithValue("@OBJ_DESC", OBJ_DESC == null ? (object)DBNull.Value : OBJ_DESC);
        //                command.Parameters.AddWithValue("@OBJ_TYPE", OBJ_TYPE == null ? (object)DBNull.Value : OBJ_TYPE);
        //                command.Parameters.AddWithValue("@OBJ_COMPANY", OBJ_COMPANY == null ? (object)DBNull.Value : OBJ_COMPANY);
        //                command.Parameters.AddWithValue("@OBJ_DATE_FROM", OBJ_DATE_FROM.HasValue ? OBJ_DATE_FROM.Value :(object)DBNull.Value);

        //                command.Parameters.AddWithValue("@OBJ_DATE_TO", OBJ_DATE_TO.HasValue ? OBJ_DATE_TO.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@OBJ_ORD", OBJ_ORD.HasValue ? OBJ_ORD.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@OBJ_ITEM_ORD", OBJ_ITEM_ORD.HasValue ? OBJ_ITEM_ORD.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR);
        //                command.Parameters.AddWithValue("@ITEMDESC", ITEMDESC == null ? (object)DBNull.Value : ITEMDESC);

        //                command.Parameters.AddWithValue("@ITEMCODE", ITEMCODE == null ? (object)DBNull.Value : ITEMCODE);
        //                command.Parameters.AddWithValue("@QTY", QTY );
        //                command.Parameters.AddWithValue("@DATE_ZAPLANOVANI", DATE_ZAPLANOVANI.HasValue ? DATE_ZAPLANOVANI.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@VP_PRPS", VP_PRPS );
        //                command.Parameters.AddWithValue("@VP_PRPS_QTY", VP_PRPS_QTY.HasValue ? VP_PRPS_QTY.Value : (object)DBNull.Value);

        //                command.Parameters.AddWithValue("@VP_PRPS_SOPNUMBE", VP_PRPS_SOPNUMBE == null ? (object)DBNull.Value : VP_PRPS_SOPNUMBE);
        //                command.Parameters.AddWithValue("@VP_PRDCT_QTY", VP_PRDCT_QTY.HasValue ? VP_PRDCT_QTY.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@USERID", USERID.HasValue ? USERID.Value : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@Ref_PVH", Ref_PVH.HasValue ? Ref_PVH.Value : (object)DBNull.Value);

        //                connection.Open();
        //                int result = command.ExecuteNonQuery();
        //                connection.Close();
        //                // Check Error
        //                if (result < 0)
        //                    throw new Exception("Nevložen zaznam do ");
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Edit_Zaplanovane Members

        //public bool Edit_Zaplanovane(int ORD_OBJpol, decimal? QTY, int? Flag)
        //{
        //    try
        //    {
        //        Database.Pohoda.OBJPol_Update_VPrQTY(QTY, ORD_OBJpol);
        //        Database.Pohoda.OBJPol_Update_RefVPrPVF(Flag, ORD_OBJpol);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Get_Zaplanovane Members

        //public decimal? Get_Zaplanovane(int ORD_OBJpol)
        //{
        //    try
        //    {
        //        return Database.Pohoda.OBJPol_Get_VPrQTY(ORD_OBJpol);
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return null;
        //    }

        //}

        //#endregion

        //#region IPV_Update_PV Members

        //public bool Update(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow Row)
        //{
        //    try
        //    {




        //            Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();

        //            ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

        //            ta.Update(Row);



        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Delete_PV Members

        //public bool Delete(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow Row, decimal JizZaplanovano)
        //{

        //    //TODO, pomoci transakce udelat promazani...


        //    try
        //    {


        //        Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();
        //        ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        ta.DeleteByID(Row.DEX_ROW_ID);

        //        decimal? QTYInPohoda = Database.Pohoda.OBJPol_Get_VPrQTY(Row.OBJ_ITEM_ORD);

        //        if (QTYInPohoda.HasValue)
        //        {
        //            Edit_Zaplanovane(Row.OBJ_ITEM_ORD, JizZaplanovano, 1);
        //        }
        //        else
        //        {
        //            Edit_Zaplanovane(Row.OBJ_ITEM_ORD, null, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Edit_QTY_PV Members

        //public bool Edit_QTY(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, decimal? QTY, bool Flag, string VP_PRPS_SOPNUMBE)
        //{
        //    try
        //    {

        //        Update_QTY_ByID_PV(row.DEX_ROW_ID, QTY, Flag, VP_PRPS_SOPNUMBE);

        //        //Edit_Zaplanovane(row.OBJ_ITEM_ORD, QTY, 1);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;

        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Update_QTY_ByID_PV Members

        //public bool Update_QTY_ByID_PV(int ID, decimal? VP_PRPS_QTY,bool VP_PRPS, string VP_PRPS_SOPNUMBE)
        //{
        //    try
        //    {
        //        //

        //        //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();
        //        //ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        //ta.EditBy_DexRowID(VP_PRPS_QTY, VP_PRPS, ID);



        //        using (SqlConnection connection = new SqlConnection(ConnectionString))
        //        {
        //            String query = "UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVP +  
        //                " SET VP_PRPS_QTY = @VP_PRPS_QTY," +
        //                " VP_PRPS_SOPNUMBE = @VP_PRPS_SOPNUMBE, " +
        //                " VP_PRPS = @VP_PRPS " +
        //                " WHERE (DEX_ROW_ID = @ID)";

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@ID", ID);
        //                command.Parameters.AddWithValue("@VP_PRPS_QTY", VP_PRPS_QTY.HasValue ? VP_PRPS_QTY : (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@VP_PRPS", VP_PRPS);
        //                command.Parameters.AddWithValue("@VP_PRPS_SOPNUMBE", string.IsNullOrEmpty(VP_PRPS_SOPNUMBE) ? (object)DBNull.Value : VP_PRPS_SOPNUMBE);

        //                connection.Open();
        //                int result = command.ExecuteNonQuery();
        //                connection.Close();
        //                // Check Error
        //                if (result < 0)
        //                    throw new Exception("Nevložen zaznam do ");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Insert_PVH Members

        //public bool Insert_PVH(string SOPNUMBE, string SOPTYPE, string SOPDESC, string BarcodeH, byte Active, Fask.Interfaces.Classes.ZaplanovanyDoklad zaplanovanyDoklad)
        //{
        //    try
        //    {

        //        Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_Insert_PVH(SOPNUMBE, SOPTYPE, SOPDESC, BarcodeH, Active, zaplanovanyDoklad);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Edit_RefPVH_in_PVP Members

        //public bool Edit_RefPVH_in_PVP(int DEXROWID_PVP, int? RefPVH)
        //{
        //    try
        //    {


        //        Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();
        //        ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        ta.Edit_ByDexRowID_RefPVH(RefPVH, DEXROWID_PVP);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_GetDEX_ROW_ID_from_PVH Members

        //public int? GetDEX_ROW_ID_from_PVH(string SOPNUMBE, Fask.Interfaces.Classes.ZaplanovanyDoklad zaplanovanyDoklad)
        //{
        //    try
        //    {
        //        //

        //        //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVHTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVHTableAdapter();
        //        //ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        //return ta.GetDEXROWID_BySOPNUMBE(SOPNUMBE);

        //        return Database.Vyroba_FASK_Vyroba_PVH.GetDEXROWID_BySOPNUMBE(SOPNUMBE);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return null;
        //    }

        //}

        //#endregion

        //#region IPV_Zaplanovani_PV Members

        //public bool Zaplanovani_PV(Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVPRow row, int DexRowID_PVH, decimal JizZaplanovano)
        //{
        //    //TODO, predelat mo6no na pevno psane SQL + Transakce....


        //    try
        //    {
        //        decimal QTY_VolneDoZaplanovani = row.QTY - JizZaplanovano;

        //        if (QTY_VolneDoZaplanovani > row.VP_PRPS_QTY)
        //        {
        //            //Pokud množstvi na objednavke je vetší
        //            // množstvi ktere se chce zaplanovat tak 
        //            // Napr :
        //            //          QTY = 50;
        //            //          VP_PRPS_QTY = 20;
        //            //          JizZaplanovano = 10;
        //            //          QTY > VP_PRPS_QTY  >> TRUE
        //            //          QTYZaplanovano = 20 = 50 - 20;


        //            decimal QTYZaplanovano = QTY_VolneDoZaplanovani - row.VP_PRPS_QTY; // Vy

        //            this.Update_VP_PRDCT_QTY_Ref_PVH_ByID(row.VP_PRPS_QTY, DexRowID_PVH, row.DEX_ROW_ID); // Provede update tabulkz FASK_Vyroba_PVP where DEX_ROW_ID, provede upravz hodnot VP_PRDCT_QTY (kolko je na vyrobnim přikazu) a hodnota Ref_PVH co je odkaz na ID Hlavičky PVH 

        //            this.Insert(
        //            row.IsOBJ_NMBRNull() ? null : row.OBJ_NMBR,
        //            row.IsOBJ_DESCNull() ? null : row.OBJ_DESC,
        //            row.IsOBJ_TYPENull() ? null : row.OBJ_TYPE,
        //            row.IsOBJ_COMPANYNull() ? null : row.OBJ_COMPANY,
        //            row.IsOBJ_DATE_FROMNull() ? (DateTime?)null : row.OBJ_DATE_FROM,
        //            row.IsOBJ_DATE_TONull() ? (DateTime?)null : row.OBJ_DATE_TO,
        //            row.IsOBJ_ORDNull() ? (int?)null : row.OBJ_ORD,
        //            row.IsOBJ_ITEM_ORDNull() ? (int?)null : row.OBJ_ITEM_ORD,
        //            row.ITEMNMBR,
        //            row.IsITEMDESCNull() ? null : row.ITEMDESC,
        //            row.IsITEMCODENull() ? null : row.ITEMCODE,
        //            row.QTY,// ? (decimal?)null : row.VP_PRPS_QTY,
        //            DateTime.Now,
        //            row.IsVP_PRPS_QTYNull() ? true : row.VP_PRPS,
        //            QTYZaplanovano < 0 ? 0 : QTYZaplanovano,
        //            row.IsVP_PRPS_SOPNUMBENull() ? string.Empty : row.VP_PRPS_SOPNUMBE,
        //            null,
        //            row.IsUSERIDNull() ? (int?)null : row.USERID,
        //            null);

        //        }
        //        else if (QTY_VolneDoZaplanovani < row.VP_PRPS_QTY)
        //        {
        //            //Pokud množstvi na objednavke je mensi jak
        //            // množstvi ktere se chce zaplanovat tak 
        //            // Napr :
        //            //          QTY = 50;
        //            //          VP_PRPS_QTY = 70;
        //            //          JizZaplanovano = 10;
        //            //          QTY < VP_PRPS_QTY  >> TRUE
        //            //          QTYZaplanovano = 10 = 70 - (50 - 10)  ;


        //            decimal QTYZaplanovano = row.VP_PRPS_QTY - QTY_VolneDoZaplanovani;

        //            this.Update_VP_PRDCT_QTY_Ref_PVH_ByID(QTY_VolneDoZaplanovani, DexRowID_PVH, row.DEX_ROW_ID);

        //            this.Insert(
        //            row.IsOBJ_NMBRNull() ? null : row.OBJ_NMBR,
        //            row.IsOBJ_DESCNull() ? null : row.OBJ_DESC,
        //            row.IsOBJ_TYPENull() ? null : row.OBJ_TYPE,
        //            row.IsOBJ_COMPANYNull() ? null : row.OBJ_COMPANY,
        //            row.IsOBJ_DATE_FROMNull() ? (DateTime?)null : row.OBJ_DATE_FROM,
        //            row.IsOBJ_DATE_TONull() ? (DateTime?)null : row.OBJ_DATE_TO,
        //            row.IsOBJ_ORDNull() ? (int?)null : row.OBJ_ORD,
        //            row.IsOBJ_ITEM_ORDNull() ? (int?)null : row.OBJ_ITEM_ORD,
        //            row.ITEMNMBR,
        //            row.IsITEMDESCNull() ? null : row.ITEMDESC,
        //            row.IsITEMCODENull() ? null : row.ITEMCODE,
        //            row.QTY,// ? (decimal?)null : row.VP_PRPS_QTY,
        //            DateTime.Now,
        //            row.VP_PRPS,
        //            row.IsVP_PRPS_QTYNull() ? (decimal?)null : row.VP_PRPS_QTY,
        //            row.IsVP_PRPS_SOPNUMBENull() ? string.Empty : row.VP_PRPS_SOPNUMBE,
        //            QTYZaplanovano,
        //            row.IsUSERIDNull() ? (int?)null : row.USERID,
        //            DexRowID_PVH);

        //        }
        //        else if (QTY_VolneDoZaplanovani == row.VP_PRPS_QTY)
        //        {
        //            this.Update_VP_PRDCT_QTY_Ref_PVH_ByID(row.VP_PRPS_QTY, DexRowID_PVH, row.DEX_ROW_ID);
        //        }

        //   }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }

        //    return true;
        //}


        //public bool Update_VP_PRDCT_QTY_Ref_PVH_ByID(decimal? VP_PRDCT_QTY, int? Ref_PVH, int ID)
        //{
        //    try
        //    {


        //        Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();
        //        ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        ta.Update_VP_PRDCT_QTY_Ref_PVH_ByID(VP_PRDCT_QTY, Ref_PVH, ID);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_GetSKzInfo_PV Members

        //public void GetSKzInfo_PV(string ITEMNMBR, out Fask.Interfaces.DataSets.Vyroba.Pohoda_SKz_VPPRow p)
        //{
        //    //p = null;



        //    Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        //    try
        //    {
        //        da.SelectCommand = new System.Data.OleDb.OleDbCommand();
        //        da.SelectCommand.CommandType = System.Data.CommandType.Text;

        //        da.SelectCommand.CommandText = @"SELECT RefVPrTIMEMODE as TIMEMODE , VPrTIMEPREP as TIMEPREP, VPrTIMEUNIT as TIMEUNIT, EAN as VNDITNUM, EAN as BarcodeP, MJ as MJ from SKz " +
        //            "WHERE ID = " + ITEMNMBR;

        //        da.SelectCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

        //        da.Fill(ds.Pohoda_SKz_VPP);

        //        if ((ds.Pohoda_SKz_VPP != null) && (ds.Pohoda_SKz_VPP.Count > 0))
        //        {
        //            p = ds.Pohoda_SKz_VPP.First();
        //        }
        //        else
        //            p = null;


        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle("GetFiltrovanyPVList", " GetFiltrovanyPVList", ex);
        //        Fask.Logging.ExceptionHandler2.Handle(ds);
        //        p = null;
        //    }

        //    //return ds;
        //}

        //#endregion

        //#region IPV_FillByRefPVH Members

        //public bool FillByRefPVH(Fask.Interfaces.DataSets.Vyroba_Planovani ds, int Ref_PVH)
        //{
        //    try
        //    {


        //        Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_PVPDataTable dt = new Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_PVPDataTable();

        //        Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVPTableAdapter();
        //        ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        ta.FillByRefPVH(dt, Ref_PVH);

        //        ds.FASK_Vyroba_PVP.Clear();

        //        foreach (var item in dt)
        //        {
        //            ds.FASK_Vyroba_PVP.ImportRow(item);
        //            //ds.FASK_Vyroba_PVP.AddFASK_Vyroba_PVPRow(
        //            //    item.OBJ_NMBR,
        //            //    item.OBJ_DESC,
        //            //    item.OBJ_TYPE,
        //            //    item.OBJ_COMPANY,
        //            //    item.OBJ_DATE_FROM,
        //            //    item.OBJ_DATE_TO,
        //            //    item.OBJ_ORD,
        //            //    item.OBJ_ITEM_ORD,
        //            //    item.ITEMNMBR,
        //            //    item.ITEMDESC,
        //            //    item.ITEMCODE,
        //            //    item.QTY,
        //            //    item.DATE_ZAPLANOVANI,
        //            //    item.VP_PRPS,
        //            //    item.VP_PRPS_QTY,
        //            //    item.VP_PRDCT_QTY,
        //            //    item.USERID,
        //            //    item.Ref_PVH,
        //            //    null,
        //            //    null);
        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_FillPVH Members

        //public bool FillPVH(Fask.Interfaces.DataSets.Vyroba_Planovani ds)
        //{
        //    try
        //    {
        //        //

        //        //Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_PVHDataTable dt = new Pohoda_DataSets.VyrobaDataSet.FASK_Vyroba_PVHDataTable();

        //        //Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVHTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.FASK_Vyroba_PVHTableAdapter();
        //        //ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
        //        //ta.Fill(dt);

        //        //ds.FASK_Vyroba_PVH.Clear();

        //        //foreach (var item in dt)
        //        //{
        //        //    ds.FASK_Vyroba_PVH.ImportRow(item);
        //        //}

        //        Database.Vyroba_FASK_Vyroba_PVH.Fill_PVH(ds);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

        //#region IPV_Navrh_FillStav Members

        //public bool FillStav(Vyroba_Planovani ds)
        //{
        //    try
        //    {



        //        ds.FASK_PLANOVANI.Clear();

        //        //ds.FASK_PLANOVANI.AddFASK_PLANOVANIRow("Prazdný navrh", Guid.NewGuid());

        //        using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
        //        {
        //            using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter())
        //            {
        //                using (da.SelectCommand = con.CreateCommand())
        //                {
        //                    da.SelectCommand.CommandType = System.Data.CommandType.Text;
        //                    da.SelectCommand.CommandText = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI + " ORDER BY [DESC]";
        //                    da.Fill(ds, ds.FASK_PLANOVANI.TableName);

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return false;
        //    }
        //    return true;
        //}


        //#endregion

        //#region IPV_Navrh_GetStav_ParamsNastaveni Members


        //public Vyroba_Planovani GetStav_ParamsNastaveni(Guid G)
        //{
        //    try
        //    {
        //        Vyroba_Planovani ds = new Vyroba_Planovani();


        //        using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
        //        {
        //            using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter())
        //            {
        //                using (da.SelectCommand = con.CreateCommand())
        //                {
        //                    da.SelectCommand.CommandType = System.Data.CommandType.Text;
        //                    da.SelectCommand.CommandText = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_View +
        //                        " WHERE GUID_PLANOVANI = @GUID_PLANOVANI";
        //                    da.SelectCommand.Parameters.AddWithValue("@GUID_PLANOVANI", G);

        //                    da.Fill(ds, ds.FASK_PLANOVANI_PARAMS_Nastaveni.TableName);

        //                }
        //            }
        //        }

        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable dt = new Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable();

        //        foreach (System.Data.DataColumn item in dt.Columns)
        //        {
        //            bool stav = ds.FASK_PLANOVANI_PARAMS_Nastaveni.Any(x => x.ColumnName_Original == item.ColumnName);

        //            if (!stav)
        //            {
        //                string NAME = Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_NameColumn(item.ColumnName);

        //                ds.FASK_PLANOVANI_PARAMS_Nastaveni.AddFASK_PLANOVANI_PARAMS_NastaveniRow(
        //                    NAME,
        //                    false,
        //                    G,
        //                    item.ColumnName
        //                    );


        //            }
        //        }


        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return null;
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_InsertVariantu Members

        //public void InsertVariantu(Guid G, string nazev)
        //{
        //    try
        //    {
        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANIDataTable dt = new Vyroba_Planovani.FASK_PLANOVANIDataTable();
        //        dt.AddFASK_PLANOVANIRow(nazev, G);

        //        Database.Vyroba_FASK_Vyroba_PVH.Update_FASK_PLANOVANI(dt);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_InitEmptyParams Members

        //public Vyroba_Planovani InitEmptyParams(Guid G)
        //{
        //    try
        //    {
        //        Vyroba_Planovani dsOUT = new Vyroba_Planovani();

        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable dt = new Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable();
        //        var row = dt.NewFASK_PLANOVANI_PARAMSRow();

        //        foreach (System.Data.DataColumn column in dt.Columns)
        //        {
        //            string NAME = string.Empty;

        //            NAME = Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_NameColumn(column.ColumnName);

        //            if (string.IsNullOrEmpty(NAME))
        //            {
        //                //Tady bude vznikal lokalizace pokud nebude nalezena na serveru, default...

        //                if (column.ColumnName == nameof(row.Disponibilita))
        //                {
        //                    NAME = "Disponibilita";
        //                }
        //                else if (column.ColumnName == nameof(row.Disponibilita_JenPlneVykriteObj))
        //                {
        //                    NAME = "Disponibilita jěn plně vykryté Obj";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani))
        //                {
        //                    NAME = "Slučování";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoOdberately))
        //                {
        //                    NAME = "Slučování po odběrateli";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi))
        //                {
        //                    NAME = "Slučování po množství";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_10))
        //                {
        //                    NAME = "Slučování po množství 10";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_20))
        //                {
        //                    NAME = "Slučování po množství 20";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_30))
        //                {
        //                    NAME = "Slučování po množství 30";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_40))
        //                {
        //                    NAME = "Slučování po množství 40";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_50))
        //                {
        //                    NAME = "Slučování po množství 50";
        //                }
        //                else if (column.ColumnName == nameof(row.OnLine_FIFO_OBJ))
        //                {
        //                    NAME = "Online fifo objednávky ";
        //                }
        //                else if (column.ColumnName == nameof(row.Slucovani_PoMnozstvi_Objednavka))
        //                {
        //                    NAME = "Slučování po množství (Objednávka)";
        //                }

        //                Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_Insert_NameColumn(column.ColumnName, NAME);
        //            }

        //            dsOUT.FASK_PLANOVANI_PARAMS_Nastaveni.AddFASK_PLANOVANI_PARAMS_NastaveniRow(NAME, false, G, column.ColumnName);
        //        }


        //        return dsOUT;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_InsertParams Members

        //public void InsertParams(Vyroba_Planovani dtParams)
        //{
        //    try
        //    {
        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMS_DBDataTable dt = new Vyroba_Planovani.FASK_PLANOVANI_PARAMS_DBDataTable();

        //        foreach (var item in dtParams.FASK_PLANOVANI_PARAMS_Nastaveni)
        //        {

        //            //Database.Vyroba.Get_FASK_PLANOVANI_Insert_Parametr(
        //            //    item.GUID_PLANOVANI,
        //            //    item.ColumnName_Original,
        //            //    item.Value
        //            //    );

        //            dt.AddFASK_PLANOVANI_PARAMS_DBRow(
        //                item.GUID_PLANOVANI,
        //                item.ColumnName_Original,
        //                item.Value
        //                );
        //        }

        //        Database.Vyroba_FASK_Vyroba_PVH.Update_FASK_PLANOVANI_PARAMS(dt);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_DeleteVariantu Members

        //public void DeleteVariantu(Guid G)
        //{
        //    Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_DeleteVariantu(G);
        //    Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_DeleteParametry(G);
        //}

        //#endregion

        //#region IPV_Navrh_UpdateParams Members

        //public void UpdateParams(Vyroba_Planovani dtParams)
        //{
        //    try
        //    {
        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_PLANOVANI_PARAMS_DBDataTable dt = new Vyroba_Planovani.FASK_PLANOVANI_PARAMS_DBDataTable();


        //        foreach (var item in dtParams.FASK_PLANOVANI_PARAMS_Nastaveni)
        //        {
        //            //Database.Vyroba.Get_FASK_PLANOVANI_Update_Parametr(
        //            //    item.GUID_PLANOVANI,
        //            //    item.ColumnName_Original,
        //            //    item.Value
        //            //    );

        //            dt.AddFASK_PLANOVANI_PARAMS_DBRow(
        //                item.GUID_PLANOVANI,
        //                item.ColumnName_Original,
        //                item.Value
        //                );

        //          var rr = dt.Where(x => x.GUID_PLANOVANI == item.GUID_PLANOVANI && x.ColumnName == item.ColumnName_Original).First();
        //            rr.AcceptChanges();

        //            switch (item.RowState)
        //            {
        //                case System.Data.DataRowState.Added:
        //                    rr.SetAdded();
        //                    break;
        //                case System.Data.DataRowState.Deleted:
        //                    rr.Delete();
        //                    break;
        //                case System.Data.DataRowState.Modified:
        //                    rr.SetModified();
        //                    break;
        //                default:
        //                    break;
        //            }

        //        }

        //        Database.Vyroba_FASK_Vyroba_PVH.Update_FASK_PLANOVANI_PARAMS(dt);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_GetRow_Parametry Members

        //public Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow GetRow_Parametry(Guid G)
        //{
        //    try
        //    {
        //        Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable dt = new Vyroba_Planovani.FASK_PLANOVANI_PARAMSDataTable();
        //        Vyroba_Planovani.FASK_PLANOVANI_PARAMSRow row = dt.NewFASK_PLANOVANI_PARAMSRow();

        //        var dtable = GetStav_ParamsNastaveni(G);

        //        foreach (var item in dtable.FASK_PLANOVANI_PARAMS_Nastaveni)
        //        {
        //            row[item.ColumnName_Original] = item.Value;
        //        }

        //        return row;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }

        //}

        //#endregion

        //#region IPV_Navrh_UpdateVariantu Members

        //public void UpdateVariantu(Guid G, string nazev)
        //{
        //    try
        //    {

        //        Database.Vyroba_FASK_Vyroba_PVH.Get_FASK_PLANOVANI_Update_Variantu(
        //            G,
        //            nazev
        //            );

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_PARAMS_Disponibilita Members

        //public Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable PARAMS_Disponibilita(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        //{
        //    try
        //    {



        //        Fask.POHODA.Disponibility.ValidateData validateData = new Fask.POHODA.Disponibility.ValidateData();
        //        //IOrderedEnumerable<Vyroba_Planovani.FASK_Vyroba_PVPRow> dtDIOrder = dt.OrderBy(x =>  new { ITEMNMBR = x.ITEMNMBR, DEX_ROW_ID = x.DEX_ROW_ID });

        //        var dtDIOrder = dt.OrderBy(x => x.ITEMNMBR).ThenBy(x => x.DEX_ROW_ID);

        //        foreach (var item in dtDIOrder)
        //        {
        //            var row = validateData.DataDisp.NewDataDispRow();
        //            row.ITEMNMBR = item.ITEMNMBR;
        //            row.SKL_ID = Database.Pohoda.SKz_GetIDSkladuByIDPolozky(item.ITEMNMBR);
        //            row.QTY = item.QTY;
        //            row.SOPNUMBE = item.OBJ_NMBR;

        //            row.ORD = item.OBJ_ORD;


        //            row.SetSKz_RezerNull();
        //            row.SetSKz_StavZNull();
        //            row.SetOBJ_RezerNull();

        //            validateData.DataDisp.AddDataDispRow(row);
        //        }

        //        Fask.POHODA.Disponibility.CheckDisp dispo = new Fask.POHODA.Disponibility.CheckDisp();

        //        Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable dtDispo = dispo.KontrolaDisponibilityDT(validateData, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

        //        Fask.Interfaces.DataSets.Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable dt_ChechDispo = new Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable();

        //        foreach (var RadekDispo in dtDispo)
        //        {

        //            if (RadekDispo.IsREZ_JANull() || string.IsNullOrEmpty(RadekDispo.REZ_JA))
        //            {
        //                if ((RadekDispo.STAV_SKLAD >= 0) && (RadekDispo.ZBUDE >= 0) && (RadekDispo.STAV_SKLAD >= RadekDispo.ZBUDE))
        //                {
        //                    dt_ChechDispo.AddFASK_Vyroba_PVP_DispoRow(
        //                        RadekDispo.ITEMNMBR,
        //                        RadekDispo.SOPNUMBE,
        //                        RadekDispo.ORD,
        //                        true,
        //                        RadekDispo.STAV_SKLAD_PRED
        //                        );
        //                }
        //                else
        //                {
        //                    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
        //                    dt_ChechDispo.AddFASK_Vyroba_PVP_DispoRow(
        //                        RadekDispo.ITEMNMBR,
        //                        RadekDispo.SOPNUMBE,
        //                        RadekDispo.ORD,
        //                        false,
        //                        RadekDispo.STAV_SKLAD_PRED
        //                        );
        //                    //info = new InfoValidace("ERR", item);
        //                }
        //            }
        //            else
        //            {
        //                if ((RadekDispo.STAV_SKLAD >= 0))
        //                {
        //                    //Tady projde každa položka ktera je disponibilny
        //                    dt_ChechDispo.AddFASK_Vyroba_PVP_DispoRow(
        //                        RadekDispo.ITEMNMBR,
        //                        RadekDispo.SOPNUMBE,
        //                        RadekDispo.ORD,
        //                        true,
        //                        RadekDispo.STAV_SKLAD_PRED
        //                        );

        //                }
        //                else
        //                {
        //                    //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
        //                    //info = new InfoValidace("ERR", item);
        //                    dt_ChechDispo.AddFASK_Vyroba_PVP_DispoRow(
        //                        RadekDispo.ITEMNMBR,
        //                        RadekDispo.SOPNUMBE,
        //                        RadekDispo.ORD,
        //                        false,
        //                        RadekDispo.STAV_SKLAD_PRED
        //                        );

        //                }
        //            }

        //        }

        //        return dt_ChechDispo;

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //}

        //#endregion

        //#region IPV_Navrh_PARAMS_OnLine_FIFO_OBJ Members

        //public Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable PARAMS_OnLine_FIFO_OBJ(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        //{
        //    Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable dtOUT = new Vyroba_Planovani.FASK_Vyroba_PVP_DispoDataTable();


        //    try
        //    {
        //        foreach (Vyroba_Planovani.FASK_Vyroba_PVPRow item in dt)
        //        {
        //            if (Database.Vyroba_FASK_Vyroba_PVH.Get_Info_FIFO_OBJ(item.ITEMNMBR, item.OBJ_ITEM_ORD, item.QTY))
        //            {
        //                dtOUT.AddFASK_Vyroba_PVP_DispoRow(
        //                            item.ITEMNMBR,
        //                            item.OBJ_NMBR,
        //                            item.OBJ_ORD,
        //                            true,
        //                            0
        //                            );
        //            }
        //            else
        //            {
        //                dtOUT.AddFASK_Vyroba_PVP_DispoRow(
        //                            item.ITEMNMBR,
        //                            item.OBJ_NMBR,
        //                            item.OBJ_ORD,
        //                            false,
        //                            0
        //                            );
        //            }
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }

        //    return dtOUT;
        //}

        //#endregion

        //#region IPV_Navrh_FillDetailStav Members

        //public Vyroba_Planovani GetDetailStav(string ITEMCODE)
        //{
        //    try
        //    {
        //        Vyroba_Planovani ds = new Vyroba_Planovani();

        //        ds = Database.Pohoda.SKz_GetStavDetail(ITEMCODE);

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        return null;
        //    }

        //}

        //#endregion

        //#region IPV_Navrh_UpdateNaplanovane Members

        //public void UpdateNaplanovane(Vyroba_Planovani.FASK_Vyroba_PVPDataTable dt)
        //{
        //    try
        //    {
        //        foreach (var item in dt)
        //        {
        //            this.Update_QTY_ByID_PV(item.DEX_ROW_ID, item.VP_PRPS_QTY, item.VP_PRPS, item.VP_PRPS_SOPNUMBE);
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}



        //#endregion

        #endregion


    }
}
