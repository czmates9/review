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

namespace Fask.ModuleSql_API
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Machines.IMachines,
        Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID,
        Fask.Interfaces.Vyroba.Machines.IMachines_Fill
    {
  
        #region IMachines_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID.Machines_GetDataByID(string ID)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Machines bo = new Fask.WEBAPI.API_BusinessObjects.BO_Machines();
            Vyroba.MachinesDataTable dt = new Vyroba.MachinesDataTable();
            //int result = -1;
            // ds.Machines.Clear();

            try
            {
                #region skladani URL
                HttpValueCollection URI_param_tmp = new HttpValueCollection();
                string tmp = null;

                URI_param_tmp.Add("ID", ID);


                tmp += "?";
                tmp += URI_param_tmp.ToString();
                #endregion

                string param = "Konzola_Machines_GetDataByID" + tmp;
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Vyroba.MachinesDataTable, Vyroba.MachinesRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Vyroba.MachinesDataTable, Vyroba.MachinesRow>();
                    dt = y.GetDTFromBO(bo);

                    //foreach (var item in dt)
                    //{
                    //    ds.Machines.ImportRow(item);
                    //}

                    //ds.Machines.AcceptChanges();

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
            ////Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////var tmp = ta.GetDataByID(ID);


            ////foreach (var item in tmp)
            ////{
            ////    dt.ImportRow(item);
            ////}


            ////return dt;

            //return Vyroba_Machines.Machines_GetDataByID(ConnectionString, ID); 
            #endregion
        }

        #endregion

        #region IMachines_Fill Members

        void Fask.Interfaces.Vyroba.Machines.IMachines_Fill.Machines_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.WEBAPI.API_BusinessObjects.BO_Machines bo = new Fask.WEBAPI.API_BusinessObjects.BO_Machines();
            Vyroba.MachinesDataTable dt = new Vyroba.MachinesDataTable();
            //int result = -1;
            ds.Machines.Clear();

            try
            {

                string param = "Konzola_Machines_Fill";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Vyroba.MachinesDataTable, Vyroba.MachinesRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Machines, Fask.WEBAPI.API_BusinessObjects.BO_Machines_row, Vyroba.MachinesDataTable, Vyroba.MachinesRow>();
                    dt = y.GetDTFromBO(bo);

                    foreach (var item in dt)
                    {
                        ds.Machines.ImportRow(item);
                    }

                    ds.Machines.AcceptChanges();

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

            ////var ta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            ////ta.Connection = new SqlConnection(ConnectionString);

            ////ta.Fill(tmp.Machines);

            ////foreach (var item in tmp.Machines)
            ////{
            ////    ds.Machines.ImportRow(item);
            ////}

            //Vyroba_Machines.Machines_Fill(ConnectionString, ds); 
            #endregion
        }

        #endregion
    }
}
