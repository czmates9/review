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
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;
using Fask.ModuleSql_API.Classes;

namespace Fask.ModuleSql_API
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Groups.IGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID,
        Fask.Interfaces.Vyroba.Groups.IGroups_Insert,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update_Row

    {
     
        #region IGroups_FillGroups Members

        public void Groups_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
            Vyroba.GroupsDataTable dt = new Vyroba.GroupsDataTable();
            //int result = -1;
            ds.Groups.Clear();

            try
            {

                string param = "Konzola_Groups_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Groups.ImportRow(item);
                    }

                    ds.Groups.AcceptChanges();

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
            ////SQL_Datasets.VyrobaDataSet tmp = new SQL_Datasets.VyrobaDataSet();

            ////var taGroups = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            ////taGroups.Connection = new SqlConnection(ConnectionString);

            ////taGroups.Fill(tmp.Groups);

            ////foreach (var item in tmp.Groups)
            ////{
            ////    ds.Groups.ImportRow(item);
            ////}

            //Vyroba_Groups.Groups_Fill(ConnectionString, ds); 
            #endregion

        }

        #endregion

        #region IGroups_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID.Groups_GetDataByID(string ID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
            Vyroba.GroupsDataTable dt = new Vyroba.GroupsDataTable();
            //int result = -1;
            // ds.Groups.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("ID", ID);



                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Groups_GetDataByID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow>();
                    dt = y.GetDTFromBO(bo);

                    //foreach (var item in dt)
                    //{
                    //    ds.Groups.ImportRow(item);
                    //}

                    //ds.Groups.AcceptChanges();

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
            ////Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.GetDataByID(ID);


            ////foreach (var item in tmp)
            ////{
            ////    dt.ImportRow(item);
            ////}


            //return Vyroba_Groups.Groups_GetData(ConnectionString); 
            #endregion
        }

        #endregion

        #region IGroups_Update Members

        public void Groups_Update(Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
            // Vyroba.GroupsDataTable dt = new Vyroba.GroupsDataTable();
            bool result = false;

            try
            {

                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_Groups_Update";
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
            ////var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            ////lta.Connection = new SqlConnection(ConnectionString);

            ////lta.Update(dt.ToArray());

            //Vyroba_Groups.Update(dt, ConnectionString); 
            #endregion
        }

        #endregion

        #region IGroups_Update_Row Members

        public void Groups_Update_Row(Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsRow)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Groups bo = new Fask.WEBAPI.API_BusinessObjects.BO_Groups();
            Vyroba.GroupsDataTable dt = new Vyroba.GroupsDataTable();
            bool result = false;

            try
            {

                //dt.AddCZPRO_VPHRow(row);
                dt.AcceptChanges();
                dt.ImportRow(groupsRow);


                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Groups, Fask.WEBAPI.API_BusinessObjects.BO_Groups_row, Vyroba.GroupsDataTable, Vyroba.GroupsRow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_VPP_Update_Row";
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
            ////var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            ////lta.Connection = new SqlConnection(ConnectionString);

            ////lta.Update(groupsRow);

            //Vyroba_Groups.Update(groupsRow, ConnectionString); 
            #endregion
        }

        #endregion

        #region IGroups_Insert Members

        public void Groups_Insert(string id, string Name, string Description)
        {
            int result;

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                if (id != null)
                    URI_param_tmp.Add("id", id);

                if (Name != null)
                    URI_param_tmp.Add("Name", Name);

                if (Description != null)
                    URI_param_tmp.Add("Description", Description);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Groups_Insert_Values" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);

                    //TODO MaR naplnit dt z bo

                    //return result;
                }

               // return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
               // return -1;
            }

            #region old sql
            ////var lta = new SQL_Datasets.VyrobaDataSetTableAdapters.GroupsTableAdapter();
            ////lta.Connection = new SqlConnection(ConnectionString);

            ////lta.Insert(id, Name, Description);

            //Vyroba_Groups.Groups_Insert(ConnectionString, Name, Description); 
            #endregion
        }

        #endregion
    }
}
