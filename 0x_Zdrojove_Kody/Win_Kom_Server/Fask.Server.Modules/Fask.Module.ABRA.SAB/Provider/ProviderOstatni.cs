using Fask.DataSets;
using Fask.Logging;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Vydej;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : Server.Interfaces.Ostatni.IOstatni
    {
        public StatusInfo Ostatni_GenerateDavka(Objednavka objednavka, Sklad sklad)
        {
			StatusInfo si = new StatusInfo();
			si.Description = "Ostatni_GenerateDavka start";
			si.ID = 0;

			// 1) test na rozpracovany doklad 
			//22.10.2025 MaR zde to nedava smysl

			//byte? czdoslo = Database.Vydej.CZMSTSE_SOPNUMBER_CZDOSLO(objednavka.ID);

			//if (czdoslo.HasValue)
			//{ //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
			//	if (czdoslo.Value > 0)
			//	{
			//		si.ID = -1;
			//		si.Description = string.Format("Existuje rozpracovaná dávka pro doklad '{0}' na terminálu č.:{1}", objednavka.ID, czdoslo.Value); //"Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
			//		si.InnerException = new Exception(si.Description);
			//		Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, si.Description);
			//		return si;
			//	}
			//	else if (czdoslo.Value == 0) //je pripravena ke zpracovani => uzavrit ... 
			//	{
			//		Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(objednavka.ID);
			//	}
			//}


			
			//Zda je to Dodací list


			if (objednavka.ID == "prelokovani")
			{
				//logika
				Globals_V1.LoadConfiguration();


				string stav = Classes.ABRA.Export_Prelokovani_ABRA_Ostatni(objednavka, sklad);

				//rozhodnuti na vysledny stav
				if (stav != "OK")
				{
					si.ID = -10;
					si.Description = stav;
					si.InnerException = new Exception(si.Description);
					return si;
				}
				else
				{
					if (!string.IsNullOrEmpty(objednavka.CisloDavky) && int.TryParse(objednavka.CisloDavky, out int id))
					{
						si.ID = id;
					}
					else
					{
						si.ID = 0; // nebo jiná defaultní hodnota / error handling
					}

					//25.9.2025 MaR zakomentoval aby nehazelo vyjimku
					//si.ID = int.Parse(objednavka.CisloDavky);

					si.Description = "OK";
					si.InnerException = null;
				}

			}
			// 2) nepodporovany typ transakce
			else
			{
				//si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
				si.Description = "Transakce '" + objednavka.ID + "' nenalezena.";
				if (sklad != null)
					si.Description += "\nSklad " + sklad.ID;
				si.InnerException = new Exception(si.Description);
				si.ID = -10;
				throw new Exception(si.Description);
			}

			return si;
		}
    }
}
