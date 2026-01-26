using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Services.Protocols;

using MES_Android.Config;

namespace MES_Android.ServerAccess
{
    public static class Credentials
    {
        public static void UpdateWebServiceCredentials(this HttpWebClientProtocol webservice)
        {

            switch (Settings.ServerAccess)
            {
                case Settings.ServerAccessType.Credentials:
                    webservice.Credentials = new System.Net.NetworkCredential(
                        Settings.ServerAccessUsername,
                        Settings.ServerAccessPassword,
                        Settings.ServerAccessDomain);
                    break;
                case Settings.ServerAccessType.Anonymous:
                default:
                    webservice.Credentials = new System.Net.NetworkCredential();
                    break;
            }
            webservice.PreAuthenticate = Settings.ServerAccessPreauthenticate;
            webservice.AllowAutoRedirect = Settings.ServerAccessAllowRedirection;
            webservice.EnableDecompression = Settings.ServerAccessAllowDecompression;
        }
    }
}