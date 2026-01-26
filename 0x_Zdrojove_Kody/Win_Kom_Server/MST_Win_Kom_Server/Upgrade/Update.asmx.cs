using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Fask.MST_W_Server.Upgrade
{
    /// <summary>
    /// Summary description for Update
    /// </summary>
    [WebService(Namespace = "http://fask.cz/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Update : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloUpgrade()
        {
            return "Hello Upgrade";
        }


        [WebMethod]
        public string DoIt() 
        {
            return "Do IT!!";
        
        }
    }
}
