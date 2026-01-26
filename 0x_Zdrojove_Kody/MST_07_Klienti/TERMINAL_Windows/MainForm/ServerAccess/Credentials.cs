using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using FASK.MST_WINDOWS.Main.Configuration;

namespace FASK.MST_WINDOWS.Main.ServerAccess
{
    public static class Credentials
    {
        public static void UpdateWebServiceCredentials(this HttpWebClientProtocol webservice)
        {

            switch (Config.ServerAccess)
            {
                case Config.ServerAccessType.Credentials:
                    webservice.Credentials = new System.Net.NetworkCredential(
                        Config.ServerAccessUsername,
                        Config.ServerAccessPassword,
                        Config.ServerAccessDomain);
                    break;
                case Config.ServerAccessType.Anonymous:
                default:
                    webservice.Credentials = new System.Net.NetworkCredential();
                    break;
            }
            webservice.PreAuthenticate = Config.ServerAccessPreauthenticate;
            webservice.AllowAutoRedirect = Config.ServerAccessAllowRedirection;
            webservice.EnableDecompression = Config.ServerAccessAllowDecompression;
        }
    }
}
