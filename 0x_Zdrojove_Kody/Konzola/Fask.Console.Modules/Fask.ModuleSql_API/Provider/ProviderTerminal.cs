using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.RestSharp.API;
using Fask.Logging;
using System.Net;
using RestSharp;

using Fask.Interfaces.Terminal;
using Fask.Interfaces.DataSets;
using Fask.WEBAPI;
using static Fask.ModuleSql_API.Classes.Comunication;

namespace Fask.ModuleSql_API
{
    public partial class Provider : ITerminal,
            //Fask.Interfaces.ITerminal_GetTerminalAkt,
            //Fask.Interfaces.ITerminal_GetTerminalDefinition,
            ITerminal_GetTerminalAll,
            ITerminal_GetTerminalKonfigurace_MESAndroid,
            ITerminal_DeleteFileTerminalKonfigurace_MESAndroid,
            ITerminal_GetTerminalKonfigurace_MESAndroid_Validace

    {
        #region ITerminal_DeleteFileTerminalKonfigurace_MESAndroid
        public bool DeleteFileTerminalKonfigurace_MESAndroid(int TID)
        {

            try
            {
                IRestResponse restResponse;
                string param = "KonfigTerminal/DeleteFileTerminalID" + "/" + TID;


                if (!Communicate(Terminal_ID, Classes.Comunication.REST_Type.DELETE, out restResponse, param))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s Serverem se nezdarila"));
                }

                var pom = restResponse.Content;

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (restResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    ExceptionHandler2.Handle(new Exception(string.Format("Server nenasel soubor konfigurace pro terminál: '{0}' !! ", TID)));
                    return false;
                }
                else if (restResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ExceptionHandler2.Handle(new Exception(string.Format("Pro terminál:'{0}' při práci s konf. souborem na SERVERu došlo k chybě !! ", TID)));
                    return false;
                }
                else
                {
                    ExceptionHandler2.Handle(new Exception(string.Format("Server vratil neocekavanou variantu!! > '{0}'", restResponse.StatusCode)));
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        } 
        #endregion



        #region ITerminal_GetTerminalAll Members

        public Interfaces.DataSets.Terminal GetTerminalAll()
        {

            Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL bo = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL();
            Terminal.CZMST_TERMINAL_ALLDataTable dt = new Terminal.CZMST_TERMINAL_ALLDataTable();
            Terminal ds = new Terminal();
           // bool result = false;

            try
            {
              

                string param = "Konzola_GetTerminalAll";
                string JSON = "";
                IRestResponse restResponse;

                if (!Communicate(Terminal_ID, Classes.Comunication.REST_Type.GET, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bo = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL>(restResponse.Content);

                    //TODO MaR naplnit dt z bo
                    BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL_row, Terminal.CZMST_TERMINAL_ALLDataTable, Terminal.CZMST_TERMINAL_ALLRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL, Fask.WEBAPI.API_BusinessObjects.BO_CZMST_TERMINAL_ALL_row, Terminal.CZMST_TERMINAL_ALLDataTable, Terminal.CZMST_TERMINAL_ALLRow>();
                    dt = y.GetDTFromBO(bo);


                    ds.CZMST_TERMINAL_ALL.Clear();
                    foreach (var item in dt)
                    {
                        ds.CZMST_TERMINAL_ALL.ImportRow(item);
                    }

                    ds.CZMST_TERMINAL_ALL.AcceptChanges();

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
            //SqlConnection connection = null;
            //SqlCommand command = null;
            //SqlDataAdapter adapter = null;

            //Fask.Interfaces.DataSets.Terminal dsterm = new Fask.Interfaces.DataSets.Terminal();
            //try
            //{

            //    connection = new SqlConnection(ConnectionString);
            //    command = new SqlCommand();
            //    adapter = new SqlDataAdapter();

            //    command.CommandText =
            //        " SELECT" +
            //        " akt.ID_TERMINAL" +
            //        " , akt.IP" +
            //        " , akt.DATEREQ" +
            //        " , def.DB_TYPE" +
            //        " FROM CZMST_TERMINAL_AKT as akt" +
            //        " left join CZMST_TERMINAL_DEFINITION as def" +
            //        " ON def.ID_TERMINAL = akt.ID_TERMINAL";

            //    command.Connection = connection;
            //    adapter.SelectCommand = command;

            //    adapter.Fill(dsterm, dsterm.CZMST_TERMINAL_ALL.TableName);

            //    return dsterm;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //} 
            #endregion
        }

        public string GetTerminalKonfigurace_MESAndroid(int TID, out string Konfigurace)
        {

            Konfigurace = string.Empty;

            try
            {
                IRestResponse restResponse;
                string param = "KonfigTerminal/TerminalID" + "/" + TID;


                if (!Communicate(Terminal_ID, Classes.Comunication.REST_Type.GET, out restResponse, param))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s Serverem se nezdarila"));
                }

                Konfigurace = restResponse.Content;

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return "OK";
                }
                else if (restResponse.StatusCode == HttpStatusCode.Created)
                {
                    return "Created";
                }
                
                ExceptionHandler2.Handle(new Exception(string.Format("Server vratil neocekavanou variantu!! > '{0}'", restResponse.StatusCode)));
                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public bool GetTerminalKonfigurace_MESAndroid_Validace(int TID)
        {
            
            try
            {
                IRestResponse restResponse;
                string param = "KonfigTerminal/TerminalID_Validace" + "/" + TID;


                if (!Communicate(Terminal_ID, REST_Type.GET, out restResponse, param))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s Serverem se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                ExceptionHandler2.Handle(new Exception(string.Format("Validace konfigurace na SERVERu selhala!! > '{0}'", restResponse.StatusCode)));
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        #endregion
    }
}
