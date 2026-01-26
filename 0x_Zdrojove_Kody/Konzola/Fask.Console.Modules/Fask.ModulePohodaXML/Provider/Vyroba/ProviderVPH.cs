using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.Filtry;
using Fask.Interfaces.Vyroba.Odvod_TiskoveSablony;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.VPH.IVPH,
        Fask.Interfaces.Vyroba.VPH.IVPH_Fill,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPH.IVPH_Insert,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetPotrebaMaterialu,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList,
        Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony,
        Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony,
        Fask.Interfaces.Tisky.ITisky2,
        Fask.Interfaces.Tisky.ITisk_TiskovaSablona
    {
      
        
        #region IVPH_Fill Members

        public void VPH_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals_V1.LoadConfiguration();
            Database.Vyroba_CZPRO_VPH.Fill_VPH(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds);
        }

        #endregion

        #region IVPH_Update Members

        public int Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPH.Update(dt, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IVPH_GetDataByCountEntriesSOPNUMBE Members

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPH.Get_VPH_ByCountEntriesSOPNUMBE(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, CountEntries, SOPNUMBE);
        }

        #endregion

        #region IVPH_Insert Members

        public void Insert(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row)
        {
            Globals_V1.LoadConfiguration();
            Database.Vyroba_CZPRO_VPH.Insert_VPH(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, row);
        }

        #endregion

        #region IVPH_Update_Row Members

        public int Update_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow Row)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPH.Update(Row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IVPH_GetPotrebaMaterialu Members

        public Fask.Interfaces.DataSets.Vyroba GetPotrebaMaterialu(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt_VPH, Fask.Interfaces.Filtry.PotrebaMaterialu_Filtr filtr, string USERID)
        {
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();


            foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow VPH_Row in dt_VPH)
            {

                Fask.Interfaces.DataSets.Vyroba dstmp = new Fask.Interfaces.DataSets.Vyroba();

                dstmp = GetFiltrovanyVPPList(new Interfaces.Filtry.Vyroba_VPP_Filtr() {
                    OrderBy = string.Empty,
                    CountEntries = VPH_Row.CountEntries,
                    SOPNUMBE = VPH_Row.SOPNUMBE
                });

                //FillByCountEntriesAndSOPNUMBE(dstmp,string.Empty , VPH_Row.CountEntries, VPH_Row.SOPNUMBE);

                foreach (Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow VPP_Row in dstmp.CZPRO_VPP)
                {

                    Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps = Database.Vyroba_FASK_Vyroba_TP.GET_PS_from_TP(VPP_Row);

                    foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow PS_Row in dt_ps)
                    {

                        Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow row = ds.VPH_PotrebaMaterialu.NewVPH_PotrebaMaterialuRow();


                        row.CountEntries_VP = VPH_Row.CountEntries;
                        row.ITEMNMBR_VYR= VPP_Row.ITEMNMBR;
                        row.ITEMNMBR_MAT= PS_Row.ITEMNMBR;
                        row.QTYSHPPD= PS_Row.QTYSHPPD;
                        row.SOPNUMBE_VP= VPH_Row.SOPNUMBE;
                        row.SOPDESC_VP= VPH_Row.SOPDESC;
                        row.ITEMDESC_VYR= VPP_Row.ITEMDESC;
                        row.VNDITNUM_VYR= VPP_Row.VNDITNUM;
                        row.ITEMNAME_MAT= PS_Row.ITEMNAME;
                        row.ITEMCODE_MAT= PS_Row.ITEMCODE;

                        row.SOPTYPE_VP = VPH_Row.SOPTYPE;
                        row.VNDDOCNMH_VP = VPH_Row.VNDDOCNMH;
                        row.MJ_MAT = PS_Row.MJ;

                        row.QTY_POHODA = 0;
                        row.QTY_ROZDIL = 0;

                        ds.VPH_PotrebaMaterialu.AddVPH_PotrebaMaterialuRow(row);
                    }
                }
            }


            #region Dotahovani Aktualneho stavu z IS POHODA
            //Upozorneni, asi to bude spusobovat zpomalení...

            var dt_Stav = Database.Pohoda.SKz_GetDataBy_StavZasob();

            foreach (Fask.Interfaces.DataSets.Vyroba.VPH_PotrebaMaterialuRow item in ds.VPH_PotrebaMaterialu)
            {
                int ID = 0;

                if (!string.IsNullOrEmpty(item.ITEMNMBR_MAT))
                {
                    if (!int.TryParse(item.ITEMNMBR_MAT, out ID))
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Položka neni číslo:" + item.ITEMNMBR_MAT);
                    }
                }
                else
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Položka nenalezena:" + item.ITEMNMBR_MAT);
                }

                var DataPoWhere = dt_Stav.Where(x => x.ID == ID);

                foreach (var row in DataPoWhere)
                {
                    item.QTY_POHODA = Convert.ToDecimal(row.StavZ);
                    item.QTY_ROZDIL = item.QTY_POHODA - item.QTYSHPPD;
                }
            } 

            #endregion



            return ds;

        }

        #endregion

        #region IVPH_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPHList(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_CZPRO_VPH.Get_VPHByFilter(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, filtr);
        }


        #endregion
        #region TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony
        Interfaces.DataSets.Vyroba IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony.TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(Odvod_TiskoveSablonyFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                command.CommandText =
                        "SELECT E.* FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + " as E " +
                        " WHERE " +
                        " 1=1 ";

                //if (!string.IsNullOrEmpty(filtr.nazev_okna))
                //{
                //    command.CommandText += " AND E.nazev_okna = '@nazev_okna' ";
                //    command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                #region MaR 13.11.2024 old vyhledavani
                //if (!string.IsNullOrEmpty(filtr.nazev_okna))
                //{
                //    command.CommandText += " AND E.nazev_okna = '" + filtr.nazev_okna + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.loginid))
                //{
                //    command.CommandText += " AND E.loginid = '" + filtr.loginid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.machineid))
                //{
                //    command.CommandText += " AND E.machineid = '" + filtr.machineid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.typ))
                //{
                //    command.CommandText += " AND E.typ = '" + filtr.typ + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //} 
                #endregion

                #region new 13.11.2024

                if (!string.IsNullOrEmpty(filtr.nazev_okna))
                {
                    command.CommandText += " AND E.nazev_okna = '" + filtr.nazev_okna + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.typ))
                {
                    command.CommandText += " AND E.typ = '" + filtr.typ + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                //command.CommandText += " AND (E.ord = " + filtr.ord + ") ";
                command.CommandText += " AND (E.loginid = '" + filtr.loginid + "' or E.loginid is null )";


                //if (!string.IsNullOrEmpty(filtr.loginid))
                //{
                //    command.CommandText += " AND E.loginid = '" + filtr.loginid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}


                if (!string.IsNullOrEmpty(filtr.machineid))
                {
                    command.CommandText += " AND E.machineid = '" + filtr.machineid + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                command.CommandText += " order by " + "E.ord, E.loginid asc";
                #endregion


                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_FORMULARE);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region tisky
        public int TiskovaSablonaEdit_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            //throw new NotImplementedException();

            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.Update(row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        public int TiskovaSablonaInsert_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Insert(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, row);
        }

        public int TiskovaSablonaDelete_DB(Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            Globals_V1.LoadConfiguration();
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Delete(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, row);
        }
        /// <summary>
        /// Metoda vracející cestu k šabloně ZPL na základě zadaného filtru
        /// </summary>
        /// <param name="filtr">Filtrované parametry pro získání cesty</param>
        /// <returns>Řetězec obsahující cestu k šabloně nebo prázdný řetězec, pokud není nalezena</returns>
        public string TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(Odvod_TiskoveSablonyFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataReader reader = null;
            string cesta = string.Empty;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "SELECT E.formular FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + " as E " +
                    " WHERE 1=1 ";

                #region new 13.11.2024

                if (!string.IsNullOrEmpty(filtr.nazev_okna))
                {
                    command.CommandText += " AND E.nazev_okna = @nazev_okna ";
                    command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.typ))
                {
                    command.CommandText += " AND E.typ = @typ ";
                    command.Parameters.AddWithValue("@typ", filtr.typ);
                }

                command.CommandText += " AND (E.ord = @ord) ";
                command.Parameters.AddWithValue("@ord", filtr.ord);

                command.CommandText += " AND (E.loginid = @loginid or E.loginid is null) ";
                command.Parameters.AddWithValue("@loginid", filtr.loginid);

                //if (!string.IsNullOrEmpty(filtr.machineid))
                //{
                //    command.CommandText += " AND E.machineid = @machineid ";
                //    command.Parameters.AddWithValue("@machineid", filtr.machineid);
                //}

                command.CommandText += " ORDER BY E.ord, E.loginid DESC";
                #endregion

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    cesta = reader["formular"].ToString();
                }

                return cesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
        }

        #endregion

    }
}
