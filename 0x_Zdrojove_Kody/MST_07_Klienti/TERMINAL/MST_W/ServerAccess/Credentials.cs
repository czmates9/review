using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;

namespace Fask.MST_W.ServerAccess
{
    public static class Credentials
    {
        public static void UpdateWebServiceCredentials(this HttpWebClientProtocol webservice)
        {
            
            switch (MST_Global.ServerAccess)
            {
                case MST_Global.ServerAccessType.Credentials:
                    webservice.Credentials = new System.Net.NetworkCredential(
                        MST_Global.ServerAccessUsername, 
                        MST_Global.ServerAccessPassword,
                        MST_Global.ServerAccessDomain);
                    break;
                case MST_Global.ServerAccessType.Anonymous:
                default:
                    webservice.Credentials = new System.Net.NetworkCredential();
                    break;
            }
            webservice.PreAuthenticate = MST_Global.ServerAccessPreauthenticate;
            webservice.AllowAutoRedirect = MST_Global.ServerAccessAllowRedirection;
            webservice.EnableDecompression = MST_Global.ServerAccessAllowDecompression;
        }
    }
}
