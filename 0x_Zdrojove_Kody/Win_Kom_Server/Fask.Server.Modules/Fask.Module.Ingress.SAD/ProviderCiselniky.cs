using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using Ingres.Client;

namespace Fask.Module.Ingres.SAD
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část čísleník.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Ciselniky.ICiselniky
    {
        private string TABLE_CZMST090 = "CZMST090";
        private string TABLE_CZMST091 = "CZMST091";
        private string TABLE_CZMST092 = "CZMST092";
        private string TABLE_CZMST093 = "CZMST093";
        private string TABLE_CZMST094 = "CZMST094";
		private string TABLE_FASK_ZASOBY = "FASK_ZASOBY";
        private string TABLE_FASK_ZASOBY_STAV = "FASK_ZASOBY_STAV";
        private string TABLE_CZMST096 = "CZMST096";
        private string TABLE_CZMST_DI = "CZMST_DI";
        //private string TABLE_CZMST_EVENTSTYPES = "CZMST_EventsTypes";
        //private string TABLE_CZMST_TISKARNA = "CZMST_TISKARNA";

        #region IStrediska Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Střediska pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Strediska naplnen datama</returns>
        public Fask.Interfaces.DataSets.Strediska KatalogStrediska(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            string select = "SELECT * FROM " + TABLE_CZMST091; // +" where str_typ like '" + sklad + "%' order by dex_row_id";

            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            //Nacteni dat prijemky z databaze                    
            Fask.Interfaces.DataSets.Strediska strediska = new Fask.Interfaces.DataSets.Strediska();
            xda.Fill(strediska, strediska.CZMST091.TableName);

            //Ulozeni dat prijemky pro terminal
            foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row srow in strediska.CZMST091)
            {
                srow.SetAdded();
            }

            return strediska;
        }

        #endregion

        #region IZbozi Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Zboží pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Zbozi naplnen datama</returns>
        public Fask.Interfaces.DataSets.Zbozi KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            string select = "SELECT * FROM " + TABLE_FASK_ZASOBY + " where SKL_ID like '" + sklad.ID + "%'";

            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.Interfaces.DataSets.Zbozi zbozi = new Fask.Interfaces.DataSets.Zbozi();
            xda.Fill(zbozi, zbozi.FASK_ZASOBY.TableName);

            foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBYRow srow in zbozi.FASK_ZASOBY)
            {
                srow.SetAdded();
            }

            return zbozi;
        }

        #endregion

        #region ITypDokladu Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Typy Dokladu pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset TypDokladu naplnen datama </returns>
        public Fask.DataSets.TypDokladu KatalogTypDokladu(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            string select = "SELECT * FROM " + TABLE_CZMST092 + " Where SKL_ID like '" + sklad.ID + "%'";


            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.DataSets.TypDokladu typdokladu = new Fask.DataSets.TypDokladu();
            xda.Fill(typdokladu, typdokladu.CZMST092.TableName);

            foreach (DataSets.TypDokladu.CZMST092Row srow in typdokladu.CZMST092)
            {
                srow.SetAdded();
            }

			typdokladu.CZMST092.AcceptChanges();
            return typdokladu;
        }

        #endregion

        #region IOdberatele Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Odběratele pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Odberatele naplnen datama</returns>
        public Fask.Interfaces.DataSets.Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            string select = "SELECT * FROM " + TABLE_CZMST090; // +" where odb_typ like '" + sklad + "%' order by dex_row_id";


            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.Interfaces.DataSets.Odberatele odberatele = new Fask.Interfaces.DataSets.Odberatele();
            xda.Fill(odberatele, odberatele.CZMST090.TableName);

            foreach (Fask.Interfaces.DataSets.Odberatele.CZMST090Row srow in odberatele.CZMST090)
            {
                srow.SetAdded();
            }

            return odberatele;
        }

        #endregion

        #region IPracovnici Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Precovnici pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Pracovnici naplnen datama</returns>
        public Fask.DataSets.Pracovnici KatalogPracovnici(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            string select = "SELECT * FROM " + TABLE_CZMST096; // +" where str_typ like '" + sklad + "%' order by dex_row_id";


            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.DataSets.Pracovnici pracovnici = new Fask.DataSets.Pracovnici();
            xda.Fill(pracovnici, pracovnici.CZMST096.TableName);

            foreach (DataSets.Pracovnici.CZMST096Row srow in pracovnici.CZMST096)
            {
                srow.SetAdded();
            }

            return pracovnici;
        }

        #endregion

        #region ISklady Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Sklady pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Sklady naplnen datama</returns>
        public Fask.Interfaces.DataSets.Sklady KatalogSklady(Fask.Server.Interfaces.Classes.Terminal terminal)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            string select = "SELECT * FROM " + TABLE_CZMST093;

            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.Interfaces.DataSets.Sklady sklady = new Fask.Interfaces.DataSets.Sklady();
            xda.Fill(sklady, sklady.CZMST093.TableName);

            foreach (Fask.Interfaces.DataSets.Sklady.CZMST093Row srow in sklady.CZMST093)
            {
                srow.SetAdded();
            }

            return sklady;
        }

		/// <summary>
		/// NOT IMPLEMENTED Metoda pro export Sklady z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogSkladyExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            if (so == null)
                so = new Fask.Server.Interfaces.Classes.StatusObject();
            so.Exception = true;
            so.StatusText = "Not implemented";

            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            si.ID = -1;
            si.Description = "Not implemented";
            return si;
        }

        #endregion

        #region ILokace Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Lokace pro terminal 
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Lokace, dataset naplnen daty</returns>
        public Fask.DataSets.Lokace KatalogLokace(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            IngresConnection conn = null;

            conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            string select = "SELECT * FROM " + TABLE_CZMST094;
            // naplneni czmst094
            IngresDataAdapter xda = new IngresDataAdapter(select, conn);

            Fask.DataSets.Lokace lokace = new Fask.DataSets.Lokace();
            xda.Fill(lokace, lokace.CZMST094.TableName);

            foreach (DataSets.Lokace.CZMST094Row srow in lokace.CZMST094)
            {
                srow.SetAdded();
            }

            // naplneni CZMST_SkladLokace_LokaceTypy
            select = "SELECT * FROM " + TABLE_CZMST_SkladLokace_LokaceTypy;

            xda = new IngresDataAdapter(select, conn);

            xda.Fill(lokace, lokace.CZMST_SkladLokace_LokaceTypy.TableName);

            foreach (DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow srow in lokace.CZMST_SkladLokace_LokaceTypy)
            {
                srow.SetAdded();
            }

            return lokace;
        }

        public StatusInfo KatalogLokaceExport(Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IZbozi Members

        /// <summary>
        /// NOT IMPLEMENTED Metoda pro export Zásob z IS do SQL DB
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="so">reference na StatusObjekt</param>
        /// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogZboziExport(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            Fask.Server.Interfaces.Classes.StatusInfo statusInfo = new Fask.Server.Interfaces.Classes.StatusInfo();
            try
            {
                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IOdberatele Members

		/// <summary>
		/// NOT IMPLEMENTED - Metoda pro export Odběratele z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogOdberateleExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            Fask.Server.Interfaces.Classes.StatusInfo statusInfo = new Fask.Server.Interfaces.Classes.StatusInfo();
            try
            {
                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IStrediska Members


		/// <summary>
		/// NOT IMPLEMENT Metoda pro export Střediska z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogStrediskaExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            Fask.Server.Interfaces.Classes.StatusInfo statusInfo = new Fask.Server.Interfaces.Classes.StatusInfo();
            try
            {
                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IPracovnici Members


		/// <summary>
		/// NOT IMPLEMENTED - Metoda pro export Precovnici z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogPracovniciExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
            Fask.Server.Interfaces.Classes.StatusInfo statusInfo = new Fask.Server.Interfaces.Classes.StatusInfo();
            try
            {
                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IMeny Members

        /// <summary>
        /// NOT IMPLEMENTED - Metoda pro přípravu dat pro Souboru Měny pro terminal, 
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <returns>Dataset Meny naplnen datama</returns>
        public Fask.DataSets.Meny KatalogMen(Fask.Server.Interfaces.Classes.Terminal terminal)
        {

            return new Fask.DataSets.Meny();
        }

        /// <summary>
        /// Metoda pro export Měny z IS do SQL DB
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="so">reference na StatusObjekt</param>
        /// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogMenExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {

            Fask.Server.Interfaces.Classes.StatusInfo statusInfo = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StatusInfo KatalogTypDokladuExport(Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
