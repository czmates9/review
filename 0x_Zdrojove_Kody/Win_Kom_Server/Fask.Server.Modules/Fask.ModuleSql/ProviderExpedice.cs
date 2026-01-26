using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Expedice;
using Fask.Server.Interfaces.BarCodes;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Server.Interfaces.Expedice.IExpedice
    {
        public bool Expedice_AfterProcessedAction(Guid hlavicka)
        {
            throw new NotImplementedException();
        }

        public bool Expedice_Baleni_AfterProcessedAction(Guid hlavicka)
        {
            throw new NotImplementedException();
        }

        public ExpediceBaleniHlavicky Expedice_Baleni_GetHlavicky(Terminal terminal, User user, Sklad sklad)
        {
            throw new NotImplementedException();
        }

        public ExpediceBaleni Expedice_Baleni_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Baleni_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, ExpediceBaleniHlavicky hlavicka)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Baleni_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Baleni_Polozka_Add(Guid hlavickaID, Terminal terminal, User user, Sklad sklad, ExpediceBaleni expediceRows)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Baleni_Polozka_Del(Guid hlavickaID, Terminal terminal, User user, Guid polozkaID)
        {
            throw new NotImplementedException();
        }

        public ExpediceBaleni Expedice_Baleni_Polozka_Get(Terminal terminal, User user, string skl_id, string barcode, string itemnmbr, string serltnum)
        {
            throw new NotImplementedException();
        }

        public StatusObject Expedice_Baleni_Process(Guid hlavicka, User user, Terminal terminal, Sklad sklad, ExpediceBaleni expedicedata, ProcessState processExpediceState)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Baleni_Storno_Hlavicka(Guid hlavickaID, Terminal terminal, User user, string password)
        {
            throw new NotImplementedException();
        }

        public ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable Expedice_Baleni_Tisk(Guid guidHlavickaBaleni, string NMBRPAL)
        {
            throw new NotImplementedException();
        }

        #region IExpedice Members


        public StatusInfo Expedice_GenerateDavka(Objednavka objednavka, Sklad sklad)
        {
            Globals.LoadConfiguration();
            StatusInfo si = new StatusInfo();
            si.Description = "Expedice_GenerateDavka start";
            si.ID = 0;


            if (objednavka.ID == "prelokovani")
            {
                //logika
                Globals.LoadConfiguration();


                string stav = Classes.Expedice.Export_Prelokovani_SQL_Expedice(objednavka, sklad);

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

        public ExpediceHlavicky Expedice_GetHlavicky(Terminal terminal, User user, Sklad sklad)
        {
            throw new NotImplementedException();
        }

        public Expedice Expedice_GetPalety(Terminal terminal, User user, Sklad sklad, Guid hlavickaID)
        {
            throw new NotImplementedException();
        }

        public Expedice Expedice_GetPolozky(Terminal terminal, User user, Sklad sklad, Guid hlavickaID)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Hlavicka_Add(Terminal terminal, User user, Sklad sklad, ExpediceHlavicky hlavicka)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Hlavicka_Del(Terminal terminal, User user, Sklad sklad, Guid hlavickaID)
        {
            throw new NotImplementedException();
        }

        public Expedice Expedice_Paleta_Get(Terminal terminal, User user, string skl_id, string nmbrpal)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Polozka_Add(Guid hlavicka, Terminal terminal, User user, Sklad sklad, string nmbrpal)
        {
            throw new NotImplementedException();
        }

        public StatusInfo Expedice_Polozka_Del(Guid hlavicka, Terminal terminal, User user, string nmbrpal)
        {
            throw new NotImplementedException();
        }

        public StatusObject Expedice_Process(Guid hlavicka, User user, Terminal terminal, Sklad sklad, ProcessState processExpediceState)
        {
            throw new NotImplementedException();
        }

        public SSCC Expedice_SSCC_Generovat(Terminal terminal, User user)
        {
            throw new NotImplementedException();
        }

        public SSCC Expedice_SSCC_Get(Terminal terminal, User user, string skl_id, Guid hlavickaID, string code)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
