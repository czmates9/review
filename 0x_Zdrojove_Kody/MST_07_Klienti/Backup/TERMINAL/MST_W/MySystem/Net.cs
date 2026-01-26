using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.MySystem
{
	class Net
	{
		public static string HostName
		{
			get
			{
				try
				{
					return System.Net.Dns.GetHostName();

				}
				catch
				{
					return "localhost";
				}
			}
		}

		public static string IPAddresses
		{
			get
			{
				try
				{
					StringBuilder ips = new StringBuilder();
					var hostEntry = System.Net.Dns.GetHostEntry(HostName);
					hostEntry.AddressList.Reverse().ToList().ForEach(x =>
					{
						if (ips.Length != 0)
							ips.Append(", ");
						ips.Append(x.ToString());
					});
					return ips.ToString();
				}
				catch
				{
					return "?";
				}
			}
		}

		public static string Info
		{
			get { return HostName + ": " + IPAddresses; }
		}
	}
}
