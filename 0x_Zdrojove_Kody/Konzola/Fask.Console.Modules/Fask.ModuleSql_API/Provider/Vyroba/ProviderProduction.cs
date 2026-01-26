using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Fask.Logging;
using RestSharp;
using System.Net;
using Fask.Interfaces.DataSets;
using Fask.RestSharp.API;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Production.IProduction,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList,
        Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_Update,

         Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby

    {

        #region IProduction_GetFiltrovanyProductionList Members

        Fask.Interfaces.DataSets.Vyroba Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList.Production_GetFiltrovanyProductionList(Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Vyroba.Production_KonzolaDataTable dt = new Vyroba.Production_KonzolaDataTable();
            Vyroba ds = new Vyroba();
            //int result = -1;
            ds.Production_Konzola.Clear();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_Production_GetFiltrovanyProductionList";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Production_Konzola.ImportRow(item);
                    }

                    ds.Production_Konzola.AcceptChanges();

                    return ds;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }


            #region old sql


            //Vyroba_Production obj = new Vyroba_Production();

            //return obj.GetFiltrovanyProductionList(ConnectionString, filtr);

            #region old

            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            //adapter = new System.Data.SqlClient.SqlDataAdapter();
            //connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //command = new System.Data.SqlClient.SqlCommand();
            //command.Connection = connection;

            //command.CommandText =
            //    "Select l.firstname, l.surname,";
            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //    command.CommandText += "z.ITEMDESC,";
            //command.CommandText +=
            //    "o.name operationName, m.name machineName, g.name groupName" +
            //    ", sklady.skl_desc skladName" +
            //    ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
            //    ", hlavicky.SOPDESC popiszakazky" +
            //    ", p.* from " + Fask.SQL.Constants.Common.TABLE_Production + " p";


            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //{
            //    command.CommandText += " left join (select distinct ITEMDESC, ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + ") z on z.itemnmbr = p.itemnmbr";
            //}

            //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = p.userid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Operations + " o on o.id = p.operationid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Machines + " m on m.id = p.machineid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " vg on l.USERID = vg.loginid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Groups + " g on vg.groupid = g.id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = p.skl_id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = p.skl_id and lokace.locncode=p.locncode" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE" +
            //    " Where ";

            //command.CommandText += "1=1 ";
            //// číslo zakázky
            //if (filtr.rowVPH != null && filtr.rowVPH.Count > 0)
            //{
            //    command.CommandText += " AND p.SOPNUMBE = @SOPNUMBE";
            //    command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH[0].SOPNUMBE.Trim());
            //}
            //else if (filtr.VyrobniPrikaz.Length > 0)
            //{
            //    command.CommandText += " AND isnull(p.SOPNUMBE, '') IN(" +
            //        "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
            //        " union " +
            //        "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPDESC like '%' + @SOPNUMBE + '%')" +
            //        ")";

            //    command.Parameters.AddWithValue("@SOPNUMBE", filtr.VyrobniPrikaz);
            //}


            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //{
            //    // hledaní zboží
            //    if (filtr.rowZbozi != null && filtr.rowZbozi.Count > 0)
            //    {
            //        command.CommandText += " AND p.ITEMNMBR=@itemdesc";
            //        command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi[0].ITEMDESC.Trim());
            //    }
            //    else if (filtr.Zbozi_itemdesc.Length > 0)
            //    {
            //        command.CommandText += " AND p.ITEMNMBR IN (" +
            //        " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
            //        " where ITEMDESC like '%' + @itemdesc + '%'" +
            //        " union" +
            //        " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
            //        " where ITEMNMBR like '' + @itemdesc + '%' " +
            //        " )";
            //        command.Parameters.AddWithValue("@itemdesc", filtr.Zbozi_itemdesc);
            //    }

            //}

            //// hledání uživatele
            //if (!string.IsNullOrEmpty(filtr.Uzivatel))
            //{
            //    if (filtr.Uzivatel != null)
            //    {
            //        command.CommandText += " AND p.UserID=@name";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND p.UserID IN (" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where firstname like '%' + @name + '%'" +
            //        " union" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where surname like '%' + @name + '%'" +
            //        " union" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where USERID = @name" +
            //        " )";
            //    }

            //    if (filtr.rowUzivatel != null && filtr.rowUzivatel.Count > 0)
            //        command.Parameters.AddWithValue("@name", filtr.rowUzivatel[0].USERID.Trim());
            //    else
            //        command.Parameters.AddWithValue("@name", filtr.Uzivatel);
            //}

            //// hledání skupiny
            //if (!string.IsNullOrEmpty(filtr.Skupina))
            //{
            //    if (filtr.Skupina != null)
            //    {
            //        command.CommandText += " AND g.name=@groupname";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(g.name, '') like '%' + @groupname + '%'";
            //    }

            //    if (filtr.rowGroups != null && filtr.rowGroups.Count > 0)
            //        command.Parameters.AddWithValue("@groupname", filtr.rowGroups[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@groupname", filtr.Skupina);
            //}

            //// hledání podle stroje
            //if (!string.IsNullOrEmpty(filtr.Stroj))
            //{
            //    if (filtr.Stroj != null)
            //    {
            //        command.CommandText += " AND m.name=@machinename";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(m.name, '') like '%' + @machinename + '%'";
            //    }

            //    if (filtr.rowMachine != null && filtr.rowMachine.Count > 0)
            //        command.Parameters.AddWithValue("@machinename", filtr.rowMachine[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@machinename", filtr.Stroj);
            //}

            //// hledání podle operace
            //if (!string.IsNullOrEmpty(filtr.Operace))
            //{
            //    if (filtr.Operace != null)
            //    {
            //        command.CommandText += " AND o.name=@operationname";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(o.name, '') like '%' + @operationname + '%'";
            //    }

            //    if (filtr.rowOperation != null && filtr.rowOperation.Count > 0)
            //        command.Parameters.AddWithValue("@operationname", filtr.rowOperation[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@operationname", filtr.Operace);

            //}

            //// hledání podle datumu
            //if (filtr.DatumOdValue.HasValue && filtr.DatumDoValue.HasValue)
            //{
            //    command.CommandText += " AND dateeve between @datumOd and @datumDo";
            //    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
            //    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            //}
            //else
            //{
            //    if (filtr.DatumOdValue.HasValue)
            //    {
            //        command.CommandText += " AND dateeve > @datumOd";
            //        command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
            //    }
            //    else if (filtr.DatumDoValue.HasValue)
            //    {
            //        command.CommandText += " AND dateeve < @datumDo";
            //        command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            //    }
            //}

            //if (!string.IsNullOrEmpty(filtr.DATEEVE_TimeVariant))
            //{
            //    if (!filtr.DATEEVE_TimeVariant.Contains("unknow"))
            //    {
            //        var arr = filtr.DATEEVE_TimeVariant.Split(';');
            //        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
            //        command.CommandText += " AND dateeve > @dateeve_TV";
            //        command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
            //    }
            //}

            //// zobrazit vsechny zakazky
            //if (filtr.OdvadeniVse)
            //{
            //    command.CommandText += " AND p.SOUBEHGUID is not null";
            //}

            //// zobrazit vsechny korekce
            //if (filtr.KorekceVse)
            //{
            //    command.CommandText += " AND p.CORRGUID is not null";
            //}

            //// zobrazit nedokonceny odvod vyroby
            //if (filtr.OdvadeniNedokoncene)
            //{
            //    command.CommandText += " AND p.SOUBEHGUID not IN (" +
            //    " select distinct SOUBEHGUID from " + Fask.SQL.Constants.Common.TABLE_Production +
            //    " where" +
            //    " SOUBEHGUID is not null and TIMESTOP is not null" +
            //    " )";
            //}
            //// zobrazit nedokoncene korekce
            //if (filtr.KorekceNedokoncene)
            //{
            //    //command.CommandText += " AND p.CORRGUID not IN (" +
            //    //" select distinct CORRGUID from Production" +
            //    //" where" +
            //    //" CORRGUID is not null and TIMECORSTOP is not null" +
            //    //" )";
            //    command.CommandText += " AND not exists ( " +
            //    " select SOUBEHGUID " +
            //    " from " + Fask.SQL.Constants.Common.TABLE_Production +
            //    " where " +
            //    " (TIMESTOP is not null or TIMEPREPSTOP is not null) " +
            //    " and " +
            //    " SOUBEHGUID=p.SOUBEHGUID " +
            //    " ) " +
            //    " and SOUBEHGUID is not null "
            //    ;
            //}

            //command.CommandText += " order by dateeve desc";
            ////Point p = Point.Empty;
            ////try
            ////{
            ////    DataGridViewCell currentCell = this.dataGridView1.CurrentCell;
            ////    p = new Point(currentCell.ColumnIndex, currentCell.RowIndex);
            ////}
            ////catch
            ////{
            ////}
            //// test
            ////int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
            ////int SelectedRowIndex = 0;
            ////if (this.dataGridView1.SelectedRows.Count > 0) SelectedRowIndex = this.dataGridView1.SelectedRows[0].Index; //Save Current Selected Row Index

            //vyrobaDataSet1.Production.Clear();
            //vyrobaDataSet1.Production.AcceptChanges();
            //vyrobaDataSet1.Production.BeginLoadData();
            //adapter.SelectCommand = command;
            //adapter.Fill(vyrobaDataSet1.Production);
            //vyrobaDataSet1.Production.EndLoadData();

            //return vyrobaDataSet1; 
            #endregion 
            #endregion
        }

        #endregion

        #region IProduction_FillByCORRGUID Members

        public void Production_FillByCORRGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Vyroba.Production_KonzolaDataTable dt = new Vyroba.Production_KonzolaDataTable();
            //int result = -1;
            ds.Production_Konzola.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CORRGUID", CORRGUID.ToString());



                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Production_FillByCORRGUID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Production_Konzola.ImportRow(item);
                    }

                    ds.Production_Konzola.AcceptChanges();

                   // return -1;
                }

               // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
               // return -1;
            }

            #region old sql
            ////SQL_Datasets.VyrobaDataSet.ProductionDataTable dt = new SQL_Datasets.VyrobaDataSet.ProductionDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.FillByCORRGUID(dt, CORRGUID);

            //Vyroba_Production.Production_FillByCORRGUID(ConnectionString, ds, CORRGUID);


            ////foreach (var item in dt)
            ////{
            ////    dt.ImportRow(item);
            ////}


            ////return dt; 
            #endregion
        }

        #endregion

        #region IProduction_FillBySOUBEHGUID Members

        public void Production_FillBySOUBEHGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Vyroba.Production_KonzolaDataTable dt = new Vyroba.Production_KonzolaDataTable();
            //int result = -1;
            ds.Production_Konzola.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("SOUBEHGUID", SOUBEHGUID.ToString());



                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Production_FillBySOUBEHGUID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Production_Konzola.ImportRow(item);
                    }

                    ds.Production_Konzola.AcceptChanges();

                    // return -1;
                }

                // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                // return -1;
            }

            #region old sql
            ////SQL_Datasets.VyrobaDataSet.ProductionDataTable dt = new SQL_Datasets.VyrobaDataSet.ProductionDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.FillBySOUBEHGUID(dt, SOUBEHGUID);


            ////foreach (var item in dt)
            ////{
            ////    dt.ImportRow(item);
            ////}

            //Vyroba_Production.Production_FillBySOUBEHGUID(ConnectionString, ds, SOUBEHGUID); 
            #endregion

        }

        #endregion

        #region IProduction_GetDataByCORRGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(Guid CORRGUID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Vyroba.Production_KonzolaDataTable dt = new Vyroba.Production_KonzolaDataTable();
            //int result = -1;
           // ds.Production.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("CORRGUID", CORRGUID.ToString());



                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Production_GetDataByCORRGUID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    //foreach (var item in dt)
                    //{
                    //    ds.Production.ImportRow(item);
                    //}

                    //ds.Production.AcceptChanges();

                     return dt;
                }

                 return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                 return null;
            }


            #region old sql
            ////Fask.Interfaces.DataSets.Vyroba.ProductionDataTable dt = new Fask.Interfaces.DataSets.Vyroba.ProductionDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.GetDataByCORRGUID(SOUBEHGUID);


            ////foreach (var item in tmp)
            ////{
            ////    dt.ImportRow(item);
            ////}


            //return Vyroba_Production.Production_GetDataByCORRGUID(ConnectionString, CORRGUID); 
            #endregion
        }

        #endregion

        #region IProduction_GetDataBySOUBEHGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(Guid SOUBEHGUID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Vyroba.Production_KonzolaDataTable dt = new Vyroba.Production_KonzolaDataTable();
            //int result = -1;
            // ds.Production.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("SOUBEHGUID", SOUBEHGUID.ToString());



                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Production_GetDataBySOUBEHGUID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    //foreach (var item in dt)
                    //{
                    //    ds.Production.ImportRow(item);
                    //}

                    //ds.Production.AcceptChanges();

                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }

            #region old sql
            ////Fask.Interfaces.DataSets.Vyroba.ProductionDataTable dt = new Fask.Interfaces.DataSets.Vyroba.ProductionDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.GetDataBySOUBEHGUID(SOUBEHGUID);


            ////foreach (var item in tmp)
            ////{
            ////    dt.ImportRow(item);
            ////}


            //return Vyroba_Production.Production_GetDataBySOUBEHGUID(ConnectionString, SOUBEHGUID); 
            #endregion
        }

        #endregion

        #region IProduction_Update Members

        public void Production_Update(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            // Vyroba.ProductionDataTable dt = new Vyroba.ProductionDataTable();
            bool result = false;

            try
            {

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_Production_Update";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            #region old sql
            ////var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            ////lta.Connection = new SqlConnection(ConnectionString);
            ////lta.Update(dt.ToArray());

            //Vyroba_Production.Update(dt, ConnectionString); 
            #endregion
        }

        #endregion

        #region IProduction_GetFiltrovanyProductionVazby Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Production bo = new Fask.WEBAPI.API_BusinessObjects.BO_Production();
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();
            Vyroba ds = new Vyroba();
            //int result = -1;
            ds.Production_Konzola.Clear();
            try
            {
                IRestResponse restResponse;
                string param = "Konzola_Production_GetFiltrovanyProductionVazby";
                string JSON = "";

                JSON = JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Production>(restResponse.Content);

                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Production, Fask.WEBAPI.API_BusinessObjects.BO_Production_row, Vyroba.Production_KonzolaDataTable, Vyroba.Production_KonzolaRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Production_Konzola.ImportRow(item);
                    }

                    ds.Production_Konzola.AcceptChanges();

                    return ds;
                }

                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }


            #region old sql
            //Vyroba_Production obj = new Vyroba_Production();

            //return obj.GetFiltrovanyProductionVazby(ConnectionString, filtr);
            #region old

            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            //adapter = new System.Data.SqlClient.SqlDataAdapter();
            //connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //command = new System.Data.SqlClient.SqlCommand();
            //command.Connection = connection;

            //command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Production + " as P ";
            //command.CommandText += " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = P.SKL_ID ";

            //command.CommandText += " WHERE ";
            //command.CommandText += "1=1 ";

            //command.CommandText += " AND ( P.TIMESTOP is not null or P.TIMEPREPSTOP is not null) ";

            //if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            //{
            //    command.CommandText += " AND P.ITEMNMBR=@ITEMNMBR";
            //    command.Parameters.AddWithValue("@ITEMNMBR", filtr.MaterialITEMNMBR.Trim());
            //}

            //if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            //{
            //    command.CommandText += " AND P.ITEMDESC like @ITEMDESC + '%'";
            //    command.Parameters.AddWithValue("@ITEMDESC", filtr.MaterialITEMDESC.Trim());
            //}

            //if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            //{
            //    command.CommandText += " AND P.ITEMMJ=@ITEMMJ";
            //    command.Parameters.AddWithValue("@ITEMMJ", filtr.MaterialMJ.Trim());
            //}

            //command.CommandText += " order by P.dateeve desc";


            //vyrobaDataSet1.Production_Odvod.Clear();
            //vyrobaDataSet1.Production_Odvod.AcceptChanges();
            //vyrobaDataSet1.Production_Odvod.BeginLoadData();
            //adapter.SelectCommand = command;
            //adapter.Fill(vyrobaDataSet1.Production_Odvod);
            //vyrobaDataSet1.Production_Odvod.EndLoadData();

            //return vyrobaDataSet1; 
            #endregion 
            #endregion
        }

        #endregion
    }
}
