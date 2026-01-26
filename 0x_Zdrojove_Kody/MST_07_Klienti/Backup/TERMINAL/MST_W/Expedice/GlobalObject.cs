using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Expedice
{
    public class GlobalObject : IDisposable
    {

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady controller_sklady = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Sklady(Main.CiselnikSkladyDB);
        

        public GlobalObject()
        {
        }

        public void Dispose()
        {
            // zruseni controlleru
			if (controller_sklady != null)
				controller_sklady.Dispose();

            controller_sklady = null;

        }
    }
}
