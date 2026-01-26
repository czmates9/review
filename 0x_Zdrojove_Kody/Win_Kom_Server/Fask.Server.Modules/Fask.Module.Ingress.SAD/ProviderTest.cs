using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD
{
	public partial class Provider : Fask.Server.Interfaces.Test.ITest,
		Fask.Server.Interfaces.Test.ITest_Komunikace
	{
		public string Komunikace()
		{
			try
			{
				string status = "OK";


				Communication.EXE_Comunication.Initialize();

				List<string> vs = new List<string>() 
				{
					"/AUTOMAT:MST_TEST"
				};

				if (!Communication.EXE_Comunication.Communicate(vs))
				{
					return "Komunikace neuspěšná...";
				}

				Communication.EXE_Comunication.Terminate();

				return status;
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return ex.Message;
			}
		}
	}
}
