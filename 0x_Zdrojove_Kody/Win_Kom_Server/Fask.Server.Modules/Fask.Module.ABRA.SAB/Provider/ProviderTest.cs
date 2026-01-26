using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
	public partial class Provider : Fask.Server.Interfaces.Test.ITest,
		Fask.Server.Interfaces.Test.ITest_Komunikace
	{
		public string Komunikace()
		{
			try
			{
				IRestResponse restResponse;

				Guid G = Guid.NewGuid();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "TestKomunikace", Constants.Common.Strediska , G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(Classes.WEBAPI.ABRAComunication.REST_Type.GET, out restResponse, Constants.Common.Strediska))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return new Classes.JsonFormatter(Classes.WEBAPI.RequestResponse.LoadResponse_TestKomunikace(restResponse, G)).Format();
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return ex.Message;
			}
		}
	}
}
