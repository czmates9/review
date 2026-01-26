using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using Fask.Vyroba_P;

namespace Fask.Vyroba_P.ServerAccess
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
