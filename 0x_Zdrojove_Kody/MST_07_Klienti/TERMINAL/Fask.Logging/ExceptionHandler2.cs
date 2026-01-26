using System;
//using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Logging
{
	public class ExceptionHandler2
	{
		//Trida pro dočasne premosteni Puvodneho Logovani, a pokročileho logovani z Konzole a serveru


		public static bool Handle(Exception exeption)
		{
			Logging.Log.Write(exeption);
			return true;
		}

	}
}
