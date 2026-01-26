using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Server.Interfaces.Inventura.IInventura
    {

        #region IInventura Members


        public StatusInfo Inventura_GenerateDavka(Objednavka objednavka, Sklad sklad)
        {
            Globals.LoadConfiguration();
            StatusInfo si = new StatusInfo();
            si.Description = "Inventura_GenerateDavka start";
            si.ID = 0;


            if (objednavka.ID == "prelokovani")
            {
                //logika
                Globals.LoadConfiguration();


                string stav = Classes.Inventura.Export_Prelokovani_SQL_Inventura(objednavka, sklad);

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
                        si.ID = -1; // nebo jiná defaultní hodnota / error handling
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

        #endregion


    }
}
