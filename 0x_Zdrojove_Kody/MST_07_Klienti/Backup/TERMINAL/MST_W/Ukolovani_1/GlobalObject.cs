using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;


namespace Fask.MST_W.Ukolovani_1
{
	public class GlobalObject
	{
		public _WebRefernces_Globals.UkolovaniServiceSession _ukolovaniService = null;

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly controller_ukoly;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly controller_ukolySync;

		public GlobalObject()
		{
			this.controller_ukoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDB);
			this.controller_ukolySync = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDBSynch);

			try
			{
				_ukolovaniService = new Fask.MST_W._WebRefernces_Globals.UkolovaniServiceSession();
				_ukolovaniService.Url = MST_Global.ServerAddress + "Ukolovani.asmx";
				_ukolovaniService.Timeout = MST_Global.TasksTimeout;
				_ukolovaniService.UpdateWebServiceCredentials();

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		#region Dispose

		public void Dispose()
        {
            if (controller_ukoly != null) controller_ukoly.Dispose();
            if (controller_ukolySync != null) controller_ukolySync.Dispose();

            controller_ukoly = null;
            controller_ukolySync = null;


            if (_ukolovaniService != null) _ukolovaniService.Dispose();

            _ukolovaniService = null;
        }

        #endregion

	}
}
