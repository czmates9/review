using Fask.ModuleSql;
using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Fask.MST_W_Server.Controllers
{
    public class Tisk_Controller : ApiController
    {


        

        [HttpPost]
		[Route("api/Tisk/Etiketa")]
		public HttpResponseMessage Etiketa([FromBody] Fask.WEBAPI.API_BusinessObjects.Tisk_Etiketa TiskInformace)
		{
 

            try
            {
                //Fask.MST_W_Server.Tisk tisk = new Tisk();

                //bool status = tisk.Etiketa(
                //    TiskInformace.TerminalID,
                //    TiskInformace.TemplateName,
                //    TiskInformace.PrinterParams,
                //    prepareTiskValues_new(TiskInformace.Data),
                //    TiskInformace.PocetVytisku
                //    );

                var providerEtiketaBL = new BL.PrintEtiketaBL(System.Web.Hosting.HostingEnvironment.MapPath);  // todo opravit provider
                                                                                                               //providerEtiketaBL.Initialize();
                                                                                                               // separate params from TiskInformace to GenericEtiketaPrint
                DSValues data = providerEtiketaBL.TransformDataValues(TiskInformace.Data);
                var providerResult = providerEtiketaBL.GenericEtiketaPrint(
                    TiskInformace.TerminalID,
                    ref TiskInformace.TemplateName,
                    TiskInformace.PrinterParams,
                    ref data,
                    TiskInformace.PocetVytisku
                    ); // todo add params
                if (providerResult)
                    return Request.CreateResponse<string>(HttpStatusCode.OK, "OK");
                else
                    return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Error");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/agrocs")]
        public HttpResponseMessage Leonardo_TEST([FromBody] Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FASK_Events_row)
        {
            try
            {
                // TODO případna možnost tisk paletoveho štítku

                //Fask.WEBAPI.API_BusinessObjects.Tisk_Etiketa TiskInformace = new WEBAPI.API_BusinessObjects.Tisk_Etiketa() {
                //    PocetVytisku = 1,
                //    PrinterParams = new MST_Print_Server_ZPL_Printing.TiskParams() { CONFIG_NAME = "" },
                //    TemplateName = "",
                //    TerminalID = 99,
                //    Data = new Dictionary<string, string>()
                //};



                //Fask.MST_W_Server.Tisk tisk = new Tisk();

                //bool status = tisk.Etiketa(
                //    TiskInformace.TerminalID,
                //    TiskInformace.TemplateName,
                //    TiskInformace.PrinterParams,
                //    prepareTiskValues(TiskInformace.Data),
                //    TiskInformace.PocetVytisku
                //    );

                //if (status)
                    return Request.CreateResponse<string>(HttpStatusCode.OK, "OK");
                //else
                //    return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Error");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse<Exception>(HttpStatusCode.BadRequest, ex);
            }
        }

        #region Private MEthods

        private DSValues prepareTiskValues(Dictionary<string, string> data)
        {
            DSValues tiskValues = new DSValues();
            tiskValues.Values.BeginLoadData();
            foreach (var item in data)
            {
                //pokud itemdesc bude nad 60znaku vem jeho znaky za 60. znakem a vloz je do promenne itemdesc_2

                tiskValues.Values.AddValuesRow(item.Key, item.Value);
            }
            tiskValues.Values.EndLoadData();
            tiskValues.AcceptChanges();
            return tiskValues;
        }

//        //MaR 9.9.2024 priprava dat na sablonu
//        private DSValues prepareTiskValues_new(Dictionary<string, string> data)
//        {
//            DSValues tiskValues = new DSValues();
//            tiskValues.Values.BeginLoadData();

//            foreach (var item in data)
//            {
//                string key = item.Key;
//                string value = item.Value;

//                // Přidání původního záznamu
//                if (item.Key == "ITEMDESC")
//                {
//                    tiskValues.Values.AddValuesRow(key, value);



//                    string itemDesc_1 = string.Empty;
//                    string itemDesc_2 = string.Empty;

//                    if (value.Length > 60)
//                    {
//                         itemDesc_1 = value.Substring(0, 60);
//                         itemDesc_2 = value.Substring(60);
//                    }
//#if DEBUG
//                    else
//                    {
//                        itemDesc_1 = value;
//                        itemDesc_2 = "TEST MaR 10.9.2024";
//                    } 
//#endif


//                    // Přidání prvních 60 znaků
//                    tiskValues.Values.AddValuesRow($"{key}_1", itemDesc_1);

//                    // Přidání zbytku jako nový řádek s modifikovaným klíčem nebo jinou identifikací
//                    tiskValues.Values.AddValuesRow($"{key}_2", itemDesc_2);
//                }
//                else if(item.Key == "CountEntries")
//                {
//                    tiskValues.Values.AddValuesRow(key, value);

//                    string SOPDESC = string.Empty;

//                    //metoda dohledani SOPDESC
//                    SOPDESC = FindSOPDESC(item.Key);
//                    tiskValues.Values.AddValuesRow($"SOPDESC", SOPDESC);

//                }
//                else
//                {

//                    tiskValues.Values.AddValuesRow(key, value);
//                }
//            }

//            tiskValues.Values.EndLoadData();
//            tiskValues.AcceptChanges();
//            return tiskValues;
//        }

//        private string FindSOPDESC(string key)
//        {

//            string SOPDESC = string.Empty;

//            SOPDESC = "TEST 1 MaR 10.9.2024";

//            SOPDESC = TiskMetodaFindSOPDESC(key);
               

//            return SOPDESC;

//        }



//        public string TiskMetodaFindSOPDESC(string key)
//        {
//            string returnValue = "TEST 2 MaR 10.9.2024"; // Výchozí hodnota, pokud nebude nalezen žádný záznam
//            SqlConnection conn = null;

//            try
//            {
//                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
//                conn.Open(); // Otevření spojení.

//                using (var command = conn.CreateCommand())
//                {
//                    // SQL dotaz na načtení hodnoty SOPDESC
//                    command.CommandText = @"
//                SELECT TOP(1) H.[SOPDESC]
//                FROM [Agro_fask].[dbo].[CZPRO_VPH] AS H
//                LEFT JOIN [Agro_fask].[dbo].[Production] AS P
//                ON H.CountEntries = P.CountEntries
//                WHERE P.CountEntries = @CountEntries";

//                    // Přidání parametru
//                    command.Parameters.Add(new SqlParameter()
//                    {
//                        ParameterName = "@CountEntries",
//                        DbType = DbType.String,
//                        Value = string.IsNullOrEmpty(key) ? (object)DBNull.Value : key
//                    });

//                    // Použití ExecuteScalar pro načtení jediné hodnoty
//                    var result = command.ExecuteScalar();

//                    // Kontrola, zda byl nalezen nějaký výsledek
//                    if (result != null && result != DBNull.Value)
//                    {
//                        returnValue = result.ToString();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Zpracování výjimky
//                Fask.Logging.ExceptionHandler2.Handle(ex);
//            }
//            finally
//            {
//                if (conn != null)
//                {
//                    conn.Close(); // Uzavření spojení
//                    conn.Dispose(); // Uvolnění zdrojů
//                }
//            }

//            return returnValue;
//        }



        #endregion

    }
}
