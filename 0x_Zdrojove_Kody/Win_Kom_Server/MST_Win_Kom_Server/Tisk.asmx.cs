using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using MST_Print_Server_ZPL_Printing;
using System.Xml;
using Fask.BarCodeGraphics;
using Fask.Logging;
using System.Reflection;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Služba pro tisk.
    /// </summary>
    [WebService(Namespace = "http://Tisk.fask.cz/", Description = "Služba pro tisk.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Tisk : System.Web.Services.WebService
    {
       
        #region WebMetody

        /// <summary>
        /// Proběhne tisk etikety bez čekání na výsledek tisku
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="templateName">Nazev šablony</param>
        /// <param name="printerParams">Objekt Informaci o tiskarne</param>
        /// <param name="data">Data pro tisk</param>
        /// <param name="pocetVytisku">počet vytisku</param>
        [WebMethod(Description = "Proběhne tisk etikety bez čekání na výsledek tisku")]
        [SoapDocumentMethod(OneWay = true)]
        public void EtiketaBezNavratu(int terminalID, string templateName, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Fask.Server.Interfaces.DataSets.DSValues data, int pocetVytisku)
        {
            try
            {
                Etiketa(terminalID, templateName, printerParams, data, pocetVytisku);
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Univerzalni metoda pro tisk Etikety
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="templateName">Nazev šablony</param>
        /// <param name="printerParams">Objekt Informaci o tiskarne</param>
        /// <param name="data">Data pro tisk</param>
        /// <param name="pocetVytisku">počet vytisku</param>
        /// <returns>True - Vytisklo, False - chyba</returns>
        [WebMethod(Description = "Univerzalni metoda pro tisk Etikety")]
        public bool Etiketa(int terminalID, string templateName, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Fask.Server.Interfaces.DataSets.DSValues data, int pocetVytisku)
        {
            var printEtiketaBL = new BL.PrintEtiketaBL(Server.MapPath);          
            return printEtiketaBL.GenericEtiketaPrint(terminalID, ref templateName, printerParams, ref data, pocetVytisku);

        }

        /// <summary>
        /// Proběhne tisk etikety bez čekání na výsledek tisku.
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="printerParams">Objekt Informaci o tiskarne</param>
        /// <param name="dataHeader">Data pro hlavičku</param>
        /// <param name="dataRowList">Data pro řádky</param>
        /// <param name="dataFooter">Data do patičky</param>
        /// <param name="templateHeader">nazev Template pro Hlavičku</param>
        /// <param name="templateRow">nazev Template pro řadky</param>
        /// <param name="templateFooter">nazev Template pro patičku</param>
        /// <param name="pocet">počet vytisku</param>
        [WebMethod(Description = "Proběhne tisk etikety bez čekání na výsledek tisku.")]
        [SoapDocumentMethod(OneWay = true)]
        public void SoupisBezNavratu(int terminalID, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Fask.Server.Interfaces.DataSets.DSValues dataHeader, List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, Fask.Server.Interfaces.DataSets.DSValues dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            var printEtiketaBL = new BL.PrintEtiketaBL(Server.MapPath);
            printEtiketaBL.SoupisBezNavratu(terminalID, printerParams, dataHeader, dataRowList, dataFooter, templateHeader, templateRow, templateFooter, pocet);
        }

        /// <summary>
        /// Metoda ktera provede normalizaci a zjednoceni dat, a zavola metodu pro tisk soupisu
        /// </summary>
        /// <param name="terminalID">ID Terminalu</param>
        /// <param name="printerParams">Objekt Informaci o tiskarne</param>
        /// <param name="dataHeader">Data pro hlavičku</param>
        /// <param name="dataRowList">Data pro řádky</param>
        /// <param name="dataFooter">Data do patičky</param>
        /// <param name="templateHeader">nazev Template pro Hlavičku</param>
        /// <param name="templateRow">nazev Template pro řadky</param>
        /// <param name="templateFooter">nazev Template pro patičku</param>
        /// <param name="pocet">počet vytisku</param>
        /// <returns>True - Vytisklo, False - chyba</returns>
        [WebMethod(Description = "Metoda ktera provede normalizaci a zjednoceni dat, a zavola metodu pro tisk soupisu")]
        public bool Soupis(int terminalID, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Fask.Server.Interfaces.DataSets.DSValues dataHeader, List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, Fask.Server.Interfaces.DataSets.DSValues dataFooter, string templateHeader, string templateRow, string templateFooter, int pocet)
        {
            var printEtiketaBL = new BL.PrintEtiketaBL(Server.MapPath);
            return printEtiketaBL.Soupis(terminalID, printerParams, ref dataHeader, ref dataRowList, ref dataFooter, templateHeader, templateRow, templateFooter, pocet);
        }

      
        #endregion


    }
}
