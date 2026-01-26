using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;
using System.IO;

namespace Fask.MST_W.ServisModule
{
	public class GlobalObject
	{
		public _WebRefernces_Globals.ServisModuleWServiceSession webServiceModule = null;
		public _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;


		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav controller_servis_ZdrojeStav = null;
		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky controller_servis_Ciselniky = null;
		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb controller_servis_ZdrojePohyb = null;

		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav Controller_servis_ZdrojeStav
        {
            get { return controller_servis_ZdrojeStav; }
        }
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky Controller_servis_Ciselniky
        {
            get { return controller_servis_Ciselniky; } 
        }
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb Controller_servis_ZdrojePohyb
        {
            get { return controller_servis_ZdrojePohyb; }
        }

		private Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele controller_odberatele = null;
		public Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele Controller_odberatele
        {
            get
            {
                return this.controller_odberatele;
            }
        }



		private int? davka;
		public int? Davka
		{
			get
			{
				return davka;
			}
			set
			{
				if (this.controller_servis_ZdrojeStav != null)
					this.controller_servis_ZdrojeStav.Dispose();
				this.controller_servis_ZdrojeStav = null;

				this.davka = value;

				if ((davka ?? 0) > 0)
				{
					try
					{
						this.controller_servis_ZdrojeStav = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(davka.Value.ToString() + "." + Main.Ext_ServisI);
					}
					catch (Exception exDavka)
					{
						Logging.Log.Write(exDavka);
					}
				}
				else
				{
					try
					{
						this.controller_servis_ZdrojeStav = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(Main.ServisZdrojeStavDB);
					}
					catch (Exception exDavka)
					{
						Logging.Log.Write(exDavka);
					}
				}
			}
		}



		public GlobalObject()
		{
			this.controller_servis_ZdrojeStav = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojeStav(Main.ServisZdrojeStavDB);
			this.controller_servis_ZdrojePohyb = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_ZdrojePohyb(Main.ServisZdrojePohybDB);
			this.controller_servis_Ciselniky = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Servis_Ciselniky(Main.ServisCiselnikDB);

			this.controller_odberatele = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Odberatele(Main.CiselnikOdberateleDB);


			try
			{
				webServiceModule = new Fask.MST_W._WebRefernces_Globals.ServisModuleWServiceSession();
				webServiceModule.Url = MST_Global.ServerAddress + "Servis.asmx";
				webServiceModule.Timeout = MST_Global.ServisTimeoutSynchronize;
				webServiceModule.UpdateWebServiceCredentials();

				ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
				ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
				ciselnikS.Timeout = MST_Global.ServiceTimeOut;
				ciselnikS.UpdateWebServiceCredentials();

			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (controller_servis_ZdrojeStav != null) controller_servis_ZdrojeStav.Dispose();
			if (controller_servis_ZdrojePohyb != null) controller_servis_ZdrojePohyb.Dispose();
            if (controller_servis_ZdrojeStav != null) controller_servis_ZdrojeStav.Dispose();

			if (controller_odberatele != null) controller_odberatele.Dispose();

			controller_servis_ZdrojeStav = null;
			controller_servis_ZdrojePohyb = null;
            controller_servis_Ciselniky = null;
            
            controller_odberatele = null;

			if (webServiceModule != null) webServiceModule.Dispose();
			if (ciselnikS != null) ciselnikS.Dispose();

			webServiceModule = null;
			ciselnikS = null;
		}

		#endregion


	}
}
