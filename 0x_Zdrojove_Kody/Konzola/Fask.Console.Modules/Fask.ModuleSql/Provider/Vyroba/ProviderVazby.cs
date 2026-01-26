using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.SkladLokace;
using System.Data.SqlClient;


namespace Fask.ModuleSql
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
        Fask.Interfaces.Vazby.IVazby2_DeleteMaterial

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
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //TODO MaR zakomentovano 30.8.2023
                //command.CommandText =
                //    " SELECT Z.*, S.skl_desc as SKL_DESC " +
                //    " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as S" +
                //    " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z" +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

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
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
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

                " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_TP + " as tp " +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z ON Z.ITEMNMBR = tp.ITEMNMBR_fol" +
            " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = Z.SKL_ID" +
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
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            //command.CommandText =
            //    "SELECT DISTINCT tp.ID_H, tp.PUO, tp.ID_L, tp.ID, tp.ITEMNMBR_Def ITEMNMBR, tp.DESC_Def ITEMDESC, tp.MJ_Def MJ" +
            //    " FROM FASK_Vyroba_TP AS tp " +
            //    " WHERE (tp.ID_H IS NULL)";

            command.CommandText =
                    "SELECT DISTINCT tp.ID_H, tp.PUO, tp.ID_L, tp.ID, tp.ITEMNMBR_Def ITEMNMBR, tp.DESC_Def ITEMDESC, tp.MJ_Def MJ, sklad.SKL_ID as SKL_ID , sklad.skl_desc as SKL_DESC, Z.ITEMCODE as ITEMCODE, Z.VNDITNUM as VNDITNUM" +
                    " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_TP + " as tp " +
                    " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z ON Z.ITEMNMBR = tp.ITEMNMBR_Def" +
                    " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = Z.SKL_ID" +
                    " WHERE (tp.ID_H IS NULL)";

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

            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);


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
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            object tmp = tpta.Scalar_MAX_IDL();

            //string oout = null;

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
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            object tmp = tpta.Scalar_Vyrobky_Count(ITEMNMBR);

            return (int?)tmp;
        }

        #endregion

        #region IVazby2_DeleteVyrobek Members

        public void DeleteVyrobek(string ITEMNMBR)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM FASK_Vyroba_TP WHERE(ITEMNMBR_Def = @ITEMNMBR)";

                        comm.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion

        #region IVazby2_UpdateEdit Members

        public void UpdateEdit(string koef, string ID_USER, DateTime? dateedit, string alternaiva, string ITEMNMBR_Def, string ITEMNMBR_fol)
        {
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            tpta.UpdateEdit(koef, ID_USER, dateedit, alternaiva, ITEMNMBR_Def, ITEMNMBR_fol);
        }

        #endregion

        #region IVazby2_Fill_onlyALTERtable Members

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_onlyALTERtable(string ITEMNMBR_Def)
        {

            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            var local_dt = tpta.GetDataBy_onlyALTERtable(ITEMNMBR_Def);

            foreach (var item in local_dt)
            {
                dt.ImportRow(item);
            }

            return dt;
        }

        #endregion

        #region IVazby2_Fill_only_Koef_and_Alter Members

        public Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable Fill_only_Koef_and_Alter(string ITEMNMBR_Def, string ITEMNMBR_fol)
        {

            Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.FASK_Vyroba_TPDataTable();

            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter tpta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.FASK_Vyroba_TPTableAdapter();
            tpta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

            var local_dt = tpta.GetDataBy_only_Koef_and_Alter(ITEMNMBR_Def, ITEMNMBR_fol);

            foreach (var item in local_dt)
            {
                dt.ImportRow(item);
            }

            return dt;
        }

        #endregion

        #region IVazby2_DeleteMaterial Members

        public void DeleteMaterial(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM FASK_Vyroba_TP WHERE (ID = @ID)";

                        comm.Parameters.AddWithValue("@ID", ID);

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #endregion
    }
}
