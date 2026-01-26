using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Fask.MST_W_Server
{
    /// <summary>
	/// Služba pro synchronizaci času Serveru a Terminalu
    /// </summary>
	[WebService(Namespace = "http://fask.cz/", Description = "Služba pro synchronizaci času Serveru a Terminalu")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SystemTime : System.Web.Services.WebService
    {
		#region Konstruktor
		
		/// <summary>
		/// Konstruktor
		/// </summary>
		public SystemTime()
		{ }
		
		#endregion

		#region webmetody
		/// <summary>
		/// Metoda která vrací aktualny čas na serveru
		/// </summary>
		/// <returns>čas na serveru</returns>
		[WebMethod(Description = "Metoda která vrací aktualny čas na serveru")]
		public DateTime GetSystemTime()
		{
			return DateTime.Now;
		} 
		#endregion
    }
}
