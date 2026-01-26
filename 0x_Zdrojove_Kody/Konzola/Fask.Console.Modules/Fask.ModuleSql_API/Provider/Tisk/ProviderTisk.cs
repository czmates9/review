using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.RestSharp.API;
using RestSharp;
using Fask.Logging;
using System.Net;
using Fask.Interfaces.DataSets;
using Fask.ModuleSql_API.Classes;
using static Fask.ModuleSql_API.Classes.Comunication;
using Fask.WEBAPI;

namespace Fask.ModuleSql_API
{
    public partial class Provider :
        Fask.Interfaces.Tisky.ITisky2,
        Fask.Interfaces.Tisky.ITisk_TiskovaSablona
    {
        public int TiskovaSablonaDelete_DB(Vyroba.FASK_FORMULARERow row)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable dt = new Vyroba.FASK_FORMULAREDataTable();
            // Vytvořte nový řádek pro cílovou datatabulku
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow newRow = dt.NewFASK_FORMULARERow();

            // Kopírování hodnot ze stávajícího řádku do nového řádku
            newRow.id = row.id;
            newRow.nazev_okna = row.Isnazev_oknaNull() ? string.Empty : row.nazev_okna;
            newRow.nazev = row.IsnazevNull() ? string.Empty : row.nazev;
            newRow.ord = row.ord;
            newRow.typ = row.IstypNull() ? string.Empty : row.typ;
            newRow.loginid = row.IsloginidNull() ? string.Empty : row.loginid;
            newRow.machineid = row.IsmachineidNull() ? string.Empty : row.machineid;
            newRow.formular = row.IsformularNull() ? string.Empty : row.formular;


            // Přidání nového řádku do cílové tabulky
            dt.Rows.Add(newRow);

            Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE();
            int result;

            try
            {
                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_Tisk_TiskovaSablonaDelete_DB";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);


                    return result;
                }

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }

        }

        public int TiskovaSablonaEdit_DB(Vyroba.FASK_FORMULARERow row)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable dt = new Vyroba.FASK_FORMULAREDataTable();
            // Vytvořte nový řádek pro cílovou datatabulku
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow newRow = dt.NewFASK_FORMULARERow();

            // Kopírování hodnot ze stávajícího řádku do nového řádku
            newRow.id = row.id;
            newRow.nazev_okna = row.Isnazev_oknaNull() ? string.Empty : row.nazev_okna;
            newRow.nazev = row.IsnazevNull() ? string.Empty : row.nazev;
            newRow.ord = row.ord;
            newRow.typ = row.IstypNull() ? string.Empty : row.typ;
            newRow.loginid = row.IsloginidNull() ? string.Empty : row.loginid;
            newRow.machineid = row.IsmachineidNull() ? string.Empty : row.machineid;
            newRow.formular = row.IsformularNull() ? string.Empty : row.formular;


            // Přidání nového řádku do cílové tabulky
            dt.Rows.Add(newRow);

            Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE();
            int result;

            try
            {
                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_Tisk_TiskovaSablonaEdit_DB";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);


                    return result;
                }

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }

            // return Database.Vyroba_CZPRO_VPH.Update(dt, ConnectionString);
            //return -1;
        }

        public int TiskovaSablonaInsert_DB(Vyroba.FASK_FORMULARERow row)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULAREDataTable dt = new Vyroba.FASK_FORMULAREDataTable();
            // Vytvořte nový řádek pro cílovou datatabulku
            Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow newRow = dt.NewFASK_FORMULARERow();

            // Kopírování hodnot ze stávajícího řádku do nového řádku
           // newRow.id = row.id;
            newRow.nazev_okna = row.Isnazev_oknaNull() ? string.Empty : row.nazev_okna;
            newRow.nazev = row.IsnazevNull() ? string.Empty : row.nazev;
            newRow.ord = row.ord;
            newRow.typ = row.IstypNull() ? string.Empty : row.typ;
            newRow.loginid = row.IsloginidNull() ? string.Empty : row.loginid;
            newRow.machineid = row.IsmachineidNull() ? string.Empty : row.machineid;
            newRow.formular = row.IsformularNull() ? string.Empty : row.formular;


            // Přidání nového řádku do cílové tabulky
            dt.Rows.Add(newRow);

            Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE bo = new Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE();
            int result;

            try
            {
                //dt na bo
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow> x = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE, Fask.WEBAPI.API_BusinessObjects.BO_FASK_FORMULARE_row, Vyroba.FASK_FORMULAREDataTable, Vyroba.FASK_FORMULARERow>();
                bo = x.GetBOFromDT(dt);

                string param = "Konzola_Tisk_TiskovaSablonaInsert_DB";
                string JSON = "";
                IRestResponse restResponse;
                JSON = JSON_Class.Serialize_JSON(bo);

                if (!Communicate(Terminal_ID, REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(new Exception("Komunikace s IS se nezdarila"));
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);


                    return result;
                }

                return -1;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return -1;
            }
        }
    }
}
