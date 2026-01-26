using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Fask.MST_W_Server
{
    /// <summary>
    /// Summary description for VyrobaTest
    /// </summary>
    [WebService(Namespace = "http://VyrobaTest.fask.cz/", Description = "Služba pro Vyrobu testy.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VyrobaTest : System.Web.Services.WebService
    {

        [WebMethod(Description = "Jedna se o testovaci zavolani připravy vyrobneho souboru" )]
        public string VyrobaDBDateTimePrepareZip_Test(byte IDTerminalu)
        {
            try
            {

                Vyroba vyroba = new Vyroba();

                DateTime dateTime = DateTime.Now.AddMonths(-1);

                vyroba.VyrobaDBDateTimePrepareZip(IDTerminalu, dateTime);

                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
