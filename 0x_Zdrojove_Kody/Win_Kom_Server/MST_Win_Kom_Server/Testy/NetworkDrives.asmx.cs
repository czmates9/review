using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace Fask.MST_W_Server.Testy
{
    /// <summary>
    /// Summary description for NetworkDrives
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NetworkDrives : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> GetDrives()
        {
            List<string> result = new List<string>();

            DriveInfo.GetDrives().ToList().ForEach(x =>
            {
                result.Add(String.Format("{0}, {1}", x.Name, x.DriveType));
            });

            return result;
        }


        [WebMethod]
        public List<string> MapDrive(string driveLetter, string UNCPath, string domainname, string username, string password)
        {
            MyPath.Network.Drives.MapDrive(driveLetter, UNCPath, domainname, username, password);

            List<string> result = new List<string>();

            DriveInfo.GetDrives().ToList().ForEach(x =>
            {
                result.Add(String.Format("{0}, {1}", x.Name, x.DriveType));
            });
            
            return result;
        }

        [WebMethod]
        public List<string> UnMapDrive(string driveLetter)
        {
            MyPath.Network.Drives.UnMapDrive(driveLetter);

            List<string> result = new List<string>();

            DriveInfo.GetDrives().ToList().ForEach(x => {
                result.Add(String.Format("{0}, {1}", x.Name, x.DriveType));
            });

            return result;
        }

    }
}
