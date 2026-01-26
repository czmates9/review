using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using Fask.Server.Interfaces;
using System.Data;

namespace Fask.Module.Pohoda.I_Tec.Provider
{
	/// <summary>
	/// Provider pro Expedici, pro I-Tec
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Expedice.IExpedice
    {
		#region Nazvy tabulek

		private string TABLE_CZMST_EXPEDICE_HLAVICKA = "CZMST_Expedice_Hlavicka";
		private string TABLE_CZMST_EXPEDICE_POLOZKY = "CZMST_Expedice_Polozky";
		private string TABLE_CZMST_EXPEDICE_BALENI_HLAVICKA = "CZMST_Expedice_Baleni_Hlavicka";
		private string TABLE_CZMST_EXPEDICE_BALENI_POLOZKY = "CZMST_Expedice_Baleni_Polozky";

		//private string TABLE_CZMST_PE_SN = "CZMST_PE_SN";
		//private string TABLE_CZMST_PE = "CZMST_PE";
		private string TABLE_CZMST_PI = "CZMST_PI"; 

		#endregion

        #region Baleni Members

        /// <summary>
		/// Metoda pro přidání hlavičky do Baleni 
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavicka">Položky pro pridani</param>
        /// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Hlavicka_Add(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky hlavicka)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky.CZMST_Expedice_Baleni_HlavickaRow rowHlavicka = null;
                rowHlavicka = hlavicka.CZMST_Expedice_Baleni_Hlavicka.Count > 0 ? hlavicka.CZMST_Expedice_Baleni_Hlavicka.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Hlavička není definována, záznam nebyl přidán");

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                // 1) kontrola existence hlavicky se stejnym ID
                // -> existuje a neni dokoncena - umozni pokracovat                
                // -> existuje a je rozpracovana - umozni pokracovat
                // -> existuje a je dokoncena - neumozni pokracovat
                // -> neexistuje -> vytvori novou a umozni pokracovat
                // co kdyz existuje a jiz je dokoncena?? .. neumoznit pokracovat ...
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtHlavicka = taHlavicka.GetDataByID(hlavicka.CZMST_Expedice_Baleni_Hlavicka.First().ID);

                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaRow rowTestHlavicka = null;
                rowTestHlavicka = dtHlavicka.Count > 0 ? dtHlavicka.First() : null;

                if (rowTestHlavicka == null)
                {
                    // neexistuje, pridat ...
                    taHlavicka.Insert(
                        rowHlavicka.ID,
                        0,
                        user.ID,
                        terminal.ID,
                        rowHlavicka.IsBarcodeNull() ? string.Empty : rowHlavicka.Barcode,
                        DateTime.Now,
                        null,
                        rowHlavicka.IsSKL_IDNull() ? string.Empty : rowHlavicka.SKL_ID
                        );

                    // vse v poradku
                    si.ID = 0;
                }
                else if (rowTestHlavicka.Rozpracovano >= 2)
                {
                    // existuje a je dokoncena
                    throw new Exception("Hlavička předlohy již existuje a je dokončena.");
                }
                else
                {
                    // existuje a je rozpracovana nebo nerozpracovana
                    si.ID = 1;
                }

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                // 1 - existuje a je rozpracovana nebo nerozpracovana
                return si;

                //int result = int.Parse(so.StatusText);

                //if (result > 0)
                //    countentries = result.ToString();
                //else
                //{
                //    result = -result;
                //    string statusinfo = string.Empty;
                //    switch (result)
                //    {
                //        case 0: statusinfo = "OK"; break;
                //        case 1: statusinfo = "Již existuje"; break;
                //        case 2: statusinfo = "Neexistuje"; break;
                //        case 3: statusinfo = "Bylo nahráno"; break;
                //        default:
                //            statusinfo = "Neznámý status";
                //            break;
                //    }
                //    throw new Exception(statusinfo + " : " + val.Trim());
                //}
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception ex2)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda která smaže zaznamy balení
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavickaID">GUID Hlavičky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Hlavicka_Del(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Guid hlavickaID)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;
				// \TODO: kontrola, zdali hlavicka jiz byla dokoncena a pokud ano, vyhodit vyjimku??

                // 1) odstranit hlavicku
                taHlavicka.Delete(hlavickaID);

                // 2) nacist data hlavicky a nasledne je odstranit (vcetne smazani dat v lokacnimmechanismu)
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavickaID);
                foreach (SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyRow item in dtPolozky)
                {
                    //processDelete(item.ID, Fask.Server.Interfaces.Lokace.ModulName.VYDEJ, new SqlCommand(), connection, trans, new SqlDataAdapter());
                    taPolozky.Delete(item.ID);
                }

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {

				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavickaID.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda která vratí nalezene hlavčky pro Baleni
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uzivatel</param>
        /// <param name="sklad">Sklad</param>
		/// <returns>Dataset ExpediceBaleniHlavicky, naplnen informacema</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_GetHlavicky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                string select =
                    "select hlavicka.*, ISNULL(N.SUMQTY, 0) as SumItems " +
                    "from " + TABLE_CZMST_EXPEDICE_BALENI_HLAVICKA + " hlavicka " +
                    "LEFT JOIN ( " +
                    "   SELECT polozky2.IDH I4ITEM, COUNT(*) SUMQTY " +
                    "   from CZMST_Expedice_Baleni_Polozky polozky2 " +
                    "   group by polozky2.IDH " +
                    "   ) as N " +
                    "   ON N.I4ITEM=hlavicka.ID " +
                    "WHERE " +
                    "   hlavicka.Rozpracovano <=0 OR (hlavicka.Rozpracovano=1 and hlavicka.TermID = " + terminal.ID + ") " +
                    "ORDER BY hlavicka.DateCreated";

                Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky hlavicky = new Fask.Server.Interfaces.DataSets.ExpediceBaleniHlavicky();

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(hlavicky, hlavicky.CZMST_Expedice_Baleni_Hlavicka.TableName);
                }

                return hlavicky;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
		/// Metoda která vráti seznam položek pro Baleni
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>Dataset ExpediceBaleni, naplnena balikama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_GetPolozky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Guid hlavickaID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlTransaction trans = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                string select =
                    "select * " +
                    "from " + TABLE_CZMST_EXPEDICE_BALENI_POLOZKY + " " +
                    "where " +
                    "   IDH=@idh";
                adapter = new SqlDataAdapter();

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command = new SqlCommand(select, connection, trans);
                command.Parameters.AddWithValue("@idh", hlavickaID);
                adapter.SelectCommand = command;

                Fask.Server.Interfaces.DataSets.ExpediceBaleni polozky = new Fask.Server.Interfaces.DataSets.ExpediceBaleni();

                adapter.Fill(polozky, polozky.CZMST_Expedice_Baleni_Polozky.TableName);

                if (trans != null)
                    trans.Commit();

                return polozky;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

				

                return null;
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda kterí vrací seznam položek z balení
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="skl_id">ID Skladu</param>
        /// <param name="barcode">č.kod</param>
        /// <param name="itemnmbr"> ID položky</param>
        /// <param name="serltnum">seriove čislo/ šarže</param>
		/// <returns>Dataset ExpediceBaleni, naplneni položkama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Polozka_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string barcode, string itemnmbr, string serltnum)
        {
            // kontrola, zdali jiz neni pridana a jinak pridat ...
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            Fask.Server.Interfaces.DataSets.ExpediceBaleni dsExpedice = new Fask.Server.Interfaces.DataSets.ExpediceBaleni();

            try
            {
                #region 2016 old
                // \TODO: vytvorit tridu na primo na filtry??
                // 6.6.2016 PeV: zmena na skladani filtru a odstraneni UNION
                //string commandText =
                //    "SELECT stav.ITEMNMBR as ITEMNMBR, zbozi.ITEMDESC as ITEMDESC, zbozi.CZ_CarKod as CZ_CarKod, zbozi.VNDITNUM as VNDITNUM, stav.LOCNCODE as LOCNCODE, stav.SKL_ID as SKL_ID, stav.QTYSHPPD as QTY, zbozi.MJ as MJ, stav.SERLTNUM as SERLTNUM, zbozi.[WEIGHT] as [WEIGHT], '' as NMBRPAL, '' as TYPEPAL, zbozi.QTYPACK as QTYPACK, zbozi.CZ_SerNum_Track as CZ_SerNum_Track " +
                //    "   FROM CZMST_SkladLokace_Stav stav " + 
                //    "   left join CZMST095 zbozi on zbozi.ITEMNMBR = stav.ITEMNMBR " + 
                //    "WHERE " +
                //    "   stav.QTYSHPPD <> 0 AND zbozi.SKL_ID = @skl_id AND zbozi.CZ_CarKod = @barcode " + 
                //    "UNION " +
                //    "SELECT stav.ITEMNMBR as ITEMNMBR, zbozi.ITEMDESC as ITEMDESC, zbozi.CZ_CarKod as CZ_CarKod, zbozi.VNDITNUM as VNDITNUM, stav.LOCNCODE as LOCNCODE, stav.SKL_ID as SKL_ID, stav.QTYSHPPD as QTY, zbozi.MJ as MJ, stav.SERLTNUM as SERLTNUM, zbozi.[WEIGHT] as [WEIGHT], '' as NMBRPAL, '' as TYPEPAL, zbozi.QTYPACK as QTYPACK, zbozi.CZ_SerNum_Track as CZ_SerNum_Track " +
                //    "   FROM CZMST_SkladLokace_Stav stav " + 
                //    "   left join CZMST095 zbozi on zbozi.ITEMNMBR = stav.ITEMNMBR " + 
                //    "WHERE " +
                //    "   stav.QTYSHPPD <> 0 AND zbozi.SKL_ID = @skl_id AND zbozi.VNDITNUM = @barcode "
                //    ;

                //adapter = new SqlDataAdapter();
                //connection = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);
                //connection.Open();
                //trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //command = new SqlCommand(commandText, connection, trans);
                //command.Parameters.AddWithValue("@skl_id", skl_id);
                //command.Parameters.AddWithValue("@barcode", barcode);
                // 6.6.2016 PeV: zmena cz_sernum_track na 2

                #endregion

                Globals_V1.LoadConfiguration();

                //string commandText =
                //    "SELECT stav.ITEMNMBR as ITEMNMBR, zbozi.ITEMDESC as ITEMDESC, zbozi.CZ_CarKod as CZ_CarKod, zbozi.VNDITNUM as VNDITNUM, stav.LOCNCODE as LOCNCODE, stav.SKL_ID as SKL_ID, stav.QTYSHPPD as QTY, zbozi.MJ as MJ, stav.SERLTNUM as SERLTNUM, zbozi.[WEIGHT] as [WEIGHT], '' as NMBRPAL, '' as TYPEPAL, zbozi.QTYPACK as QTYPACK, 2 as CZ_SerNum_Track " + //zbozi.CZ_SerNum_Track as CZ_SerNum_Track " +
                //    "   FROM CZMST_SkladLokace_Stav stav " +
                //    "   left join CZMST095 zbozi on zbozi.ITEMNMBR = stav.ITEMNMBR " +
                //    "WHERE " +
                //    "   stav.QTYSHPPD <> 0 " +
                //    (string.IsNullOrEmpty(skl_id) ? string.Empty : "AND zbozi.SKL_ID = @skl_id ") +
                //    (string.IsNullOrEmpty(barcode) ? string.Empty : "AND (zbozi.CZ_CarKod = @barcode OR zbozi.VNDITNUM = @barcode) ") +
                //    (string.IsNullOrEmpty(itemnmbr) ? string.Empty : "AND stav.ITEMNMBR = @itemnmbr ") +
                //    (string.IsNullOrEmpty(serltnum) ? string.Empty : "AND stav.SERLTNUM = @serltnum ")
                //    ;

				string commandText =
							"SELECT buf.ITEMNMBR , buf.ITEMDESC , buf.CZ_CarKod, buf.VNDITNUM , '' as LOCNCODE, '' as SKL_ID, buf.QTYSHPPD as QTY, zbozi.MJ as MJ, buf.SERLTNUM, NULL as [WEIGHT], buf.NMBRBAL as NMBRBAL, '' as NMBRPAL, '' as TYPEPAL, NULL as QTYPACK, zbozi.CZ_SerNum_Track as CZ_SerNum_Track, buf.GUID as IDPol " +
							"FROM CZMST_Expedice_Baleni_Buffer buf " +
							"left join FASK_ZASOBY zbozi on zbozi.ITEMNMBR = buf.ITEMNMBR " +
							"WHERE 1=1 " +
							"and QTYSHPPD <> 0 " +
							(string.IsNullOrEmpty(barcode) ? string.Empty : "AND NMBRBAL = @barcode ");
                    //(string.IsNullOrEmpty(barcode) ? string.Empty : "AND (zbozi.CZ_CarKod = @barcode OR zbozi.VNDITNUM = @barcode) ") +
                    //(string.IsNullOrEmpty(itemnmbr) ? string.Empty : "AND stav.ITEMNMBR = @itemnmbr ") +
                    //(string.IsNullOrEmpty(serltnum) ? string.Empty : "AND stav.SERLTNUM = @serltnum ")


                adapter = new SqlDataAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command = new SqlCommand(commandText, connection, trans);

                //if (!string.IsNullOrEmpty(skl_id))
                //    command.Parameters.AddWithValue("@skl_id", skl_id);

                if (!string.IsNullOrEmpty(barcode))
                    command.Parameters.AddWithValue("@barcode", barcode);

                //if (!string.IsNullOrEmpty(itemnmbr))
                //    command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                //if (!string.IsNullOrEmpty(serltnum))
                //    command.Parameters.AddWithValue("@serltnum", serltnum);


                adapter.SelectCommand = command;

                adapter.Fill(dsExpedice, dsExpedice.Expedice_Baleni_Polozka.TableName);

                if (trans != null)
                    trans.Commit();

                return dsExpedice;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(dsExpedice.Expedice_Baleni_Polozka);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception ex2)
                {
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Rollback transakce se nezdaril.");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Metoda pro zpracovaní davky Baleni 
        /// </summary>
        /// <param name="hlavicka">GUID Hlavičky</param>
        /// <param name="user">uživatel</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="expedicedata">Data pro zpracování</param>
        /// <param name="processExpediceState">Přiznak, co se ma udelat: Uvolnit, UvolnitAZpracovat, Zpracovat</param>
        /// <returns>StatusObjekt, nese informace o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusObject Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Process(Guid hlavicka, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleni expedicedata, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)
        {
            // \TODO: je potreba posilat veskera data, kdyz se provadi online pohyby??
            Globals_V1.LoadConfiguration();
            string guidDavka = hlavicka.ToString();
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Expedice[0].StatusObjectsDirectory, guidDavka));
            StatusObject so = new StatusObject(filePath);

            //zjistit zda soubor s danym guid existuje
            if (File.Exists(filePath))
            { //soubor jiz existuje
                so = StatusObject.Load(filePath);
                if (!so.Exception)
                    return so;
            }

            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;
            //SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter taPrijem = null;
            //SQL_Datasets.PrijemTableAdapters.CZMST_PE_SNTableAdapter taPrijemSN = null;

            try
            {
            
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;
                //taPrijem = new SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                //taPrijem.Connection = connection;
                //taPrijem.MyTransaction = trans;
                //taPrijemSN = new SQL_Datasets.PrijemTableAdapters.CZMST_PE_SNTableAdapter();
                //taPrijemSN.Connection = connection;
                //taPrijemSN.MyTransaction = trans;

                bool uvolnitdavku = processExpediceState == Fask.Server.Interfaces.Expedice.ProcessState.Uvolnit;
                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");
                    // 1) nacist data hlavicky a nasledne je odstranit (vcetne smazani dat v lokacnimmechanismu)
                    SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavicka);
                    foreach (SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyRow item in dtPolozky)
                    {
                        //processDelete(item.ID, Fask.Server.Interfaces.Lokace.ModulName.VYDEJ, new SqlCommand(), connection, trans, new SqlDataAdapter());
                        taPolozky.Delete(item.ID);
                    }
                }
                else
                {
                    // zpracovat davku
                    so.Write("kontrola existence hlavicky");

                    // kontrola existence hlavicky
                    SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtHlavicka = taHlavicka.GetDataByID(hlavicka);

                    if (dtHlavicka.Count <= 0)
                        throw new Exception("Příkaz balení neexistuje!");

                    so.Write("zapsat davku");
                    // nastavi dokonceno u davky
					// \TODO: kontrola, zdali je polozka ulozena v DB => momentalne musi vsechny online pohyby projit ...
                    taHlavicka.UpdateRozpracovanoDateFinished(2, DateTime.Now, hlavicka);
                }

                #region Action after data processed
                // after process 
                if (Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action_Asynchronous)
                {
                    so.Write("commit transakce");

                    if (trans != null)
                        trans.Commit();
                    trans = null;

                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Expedice_Baleni_AfterProcessedActionAsync));
                    thread.Start(hlavicka);
                }
                else
                {
                    // pridani after process action do transakce
                    // Uvolnuje se davka -> nevola se afterProcessedAction, neuvolnuje se davka -> vola se afterProcessedAction
                    if (!uvolnitdavku)
                    {
                        if (!Expedice_Baleni_AfterProcessedAction(hlavicka, connection, trans))
                        {
							
							try
							{
								if (trans != null)
									trans.Rollback();
							}
							catch (Exception ex2)
							{
								Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
								Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
							}

                            trans = null;
                            so.Exception = true;
                            so.Write("chyba");
                            return so;
                        }
                        //else   // vse v poradku, commit udelat
                    }

                    // commit transakce
                    so.Write("commit transakce");
                    if (trans != null)
                        trans.Commit();
                }
                #endregion

                so.SetOK();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                so.Exception = true;
                so.Write(ex.Message);

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            so.SetOK();

            return so;
        }

        /// <summary>
		/// Metoda na Storno davky Baleni
        /// </summary>
        /// <param name="hlavickaID">Guid Hlavičky</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="password">Heslo</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Storno_Hlavicka(Guid hlavickaID, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string password)
        {
            // storno hlavicky
            // 1) nastavit Rozpracovano na hodnotu 3
            // 2) vratit pohyby ...
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                // kontrola hesla ...
                if (password != Globals_V1.Konfigurace.Expedice[0].HesloStornoExpedice)
                    throw new Exception("Zadané heslo je špatně!");

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                // 1) zmenit dokonceno u hlavicky na hodnotu 3 - stornovano
                taHlavicka.UpdateRozpracovanoDateFinished(3, DateTime.Now, hlavickaID);

                // 2) nacist data hlavicky a nasledne je odstranit (vcetne smazani dat v lokacnimmechanismu)
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavickaID);
                foreach (SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyRow item in dtPolozky)
                {
                    //processDelete(item.ID, Fask.Server.Interfaces.Lokace.ModulName.VYDEJ, new SqlCommand(), connection, trans, new SqlDataAdapter());
                    taPolozky.Delete(item.ID);
                }

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavickaID.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro pridani položky Baliku do davky
        /// </summary>
        /// <param name="hlavicka">Guid hlavičky</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="sklad">sklad</param>
        /// <param name="expediceRows">Položky baliku na pridani</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Polozka_Add(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceBaleni expediceRows)
        {
            // kontrola, zdali jiz neni pridana a jinak pridat ...
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter taBuffer = null;


            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                taBuffer = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter();
                taBuffer.Connection = connection;
                taBuffer.MyTransaction = trans;

                // nacteni informace o hlavicce (kvuli ID skladu)
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaRow rowHlavicka = null;
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtHlavicky = taHlavicka.GetDataByID(hlavicka);


                rowHlavicka = (dtHlavicky.Count > 0) ? dtHlavicky.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Příkaz balení neexistuje!\nZáznam nebyl přidán.");

                // kontrola, zdali jiz nebyla hlavička dokončena
                if (rowHlavicka.Rozpracovano >= 2)
                    throw new Exception("Dávka již byla dokončena. Záznam nebyl přidán.");

                // 1) pridani polozky ...
                foreach (Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow polozka in expediceRows.CZMST_Expedice_Baleni_Polozky)
                {
                    // kontrola, zdali na palete neni jina sarze pro stejne itemnmbr (pokud je, nepustit a vyhodit vyjimku)
                    // IDH, nmbrpal, itemnmbr, serltnum
                    //object pocetsn = taPolozky.CountByIDHNmbrpalItemnmbrRuzneSerltnum(hlavicka, polozka.IsNMBRPALNull() ? string.Empty : polozka.NMBRPAL, polozka.ITEMNMBR, polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM);

                    //int? pocetsn_int = 0; 

                    //if((pocetsn != null) && (pocetsn is int?))
                    //{ pocetsn_int = (int?)pocetsn; }


                    //if (pocetsn_int.HasValue && pocetsn_int > 0)
                    //    throw new Exception(string.Format("Na paletu '{0}' je možné přidat pouze položky se stejnou šarží", polozka.IsNMBRPALNull() ? string.Empty : polozka.NMBRPAL.Trim()));

                    SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtpolozky = taPolozky.GetDataByID(polozka.ID);
                    // neni v DB, pridat ...
                    if (dtpolozky.Count <= 0)
                    {
                        // pridani do tabulky Expedice_Polozky
                        taPolozky.Insert(
                            polozka.ID,
                            polozka.IDH,
                            polozka.ITEMNMBR,
                            polozka.IsITEMDESCNull() ? string.Empty : polozka.ITEMDESC,
                            polozka.IsVNDITNUMNull() ? string.Empty : polozka.VNDITNUM,
                            polozka.IsCZ_CarKodNull() ? string.Empty : polozka.CZ_CarKod,
                            polozka.IsLOCNCODENull() ? string.Empty : polozka.LOCNCODE,
                            polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID,
                            polozka.QTY,
                            polozka.IsQTYPACKNull() ? (decimal?)null : polozka.QTYPACK,
                            polozka.QTYMJ,
                            polozka.IsMJNull() ? string.Empty : polozka.MJ,
                            polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM,
                            polozka.IsWEIGHTNull() ? (decimal?)null : polozka.WEIGHT,
                            polozka.IsNMBRPALNull() ? string.Empty : polozka.NMBRPAL,
                            polozka.IsTYPEPALNull() ? string.Empty : polozka.TYPEPAL,
                            polozka.IsPRINTEDNull() ? (byte)0 : polozka.PRINTED,
                            polozka.IsNMBRBALNull() ? string.Empty : polozka.NMBRBAL,
                            polozka.IsIDPolNull() ? (Guid?)null : polozka.IDPol
                            );



                        taBuffer.Delete(polozka.IDPol);



                        // provedeni pohybu (vydeje) v lokacnim mechanismu
                        // kde vzit sklad se kterym se dela a lokace odkud se bere??
                        // sklad z hlavicky
                        // lokace v konfiguraci aplikace prozatim ...

                        //string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                        //SqlCommand countCommand = new SqlCommand(countCommandText, connection, trans);
                        //countCommand.Parameters.Clear();
                        //countCommand.Parameters.AddWithValue("@guid", polozka.ID);

                        //int guidcount = (int)countCommand.ExecuteScalar();
                        //if (guidcount % 2 == 0)
                        //{
                        //    DateTime dtnow = DateTime.Now;
                        //    Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                        //    pohybrow.ITEMNMBR = polozka.ITEMNMBR;
                        //    pohybrow.ITEMDESC = polozka.IsITEMDESCNull() ? string.Empty : polozka.ITEMDESC;
                        //    pohybrow.DOCUMENT_NUMBER = string.Empty;
                        //    pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.V;
                        //    pohybrow.POHYB_SRC = "V";
                        //    pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                        //    pohybrow.QTYSHPPD = polozka.QTY;
                        //    pohybrow.SERLTNUM = polozka.IsSERLTNUMNull() ? string.Empty : polozka.SERLTNUM;
                        //    pohybrow.SKL_ID_SRC = polozka.IsSKL_ID_SRCNull() ? string.Empty : polozka.SKL_ID_SRC;
                        //    pohybrow.SKL_ID_DST = polozka.IsSKL_IDNull() ? string.Empty : polozka.SKL_ID;
                        //    pohybrow.LOCNCODE_SRC = polozka.IsLOCNCODE_SRCNull() ? string.Empty : polozka.LOCNCODE_SRC;
                        //    pohybrow.LOCNCODE_DST = polozka.IsLOCNCODENull() ? string.Empty : polozka.LOCNCODE;
                        //    pohybrow.UserID = user.ID;
                        //    pohybrow.TermID = terminal.ID;
                        //    pohybrow.guid = polozka.ID;
                        //    pohybrow.Expiration = null;
                        //    pohybrow.CountEntries = null;
                        //    pohybrow.dateeveS = dtnow;
                        //    pohybrow.dateeveT = dtnow;
                        //    pohybrow.CountEntries = null;

                        //    ProcessVydej(pohybrow, new SqlCommand(), connection, trans, new SqlDataAdapter());
                        //}
                    }
                }

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavicka.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro smazani položky z davky baleni
        /// </summary>
        /// <param name="hlavickaID">Guid hlavičky</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="polozkaID">Guid položky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Polozka_Del(Guid hlavickaID, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Guid polozkaID)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter taBuffer = null;

            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            Fask.Module.Pohoda.I_Tec.SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtPolozky = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable();


            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                taBuffer = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter();
                taBuffer.Connection = connection;
                taBuffer.MyTransaction = trans;

                // nacteni informace o hlavicce (kvuli ID skladu)
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaRow rowHlavicka = null;
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtHlavicky = taHlavicka.GetDataByID(hlavickaID);
                rowHlavicka = (dtHlavicky.Count > 0) ? dtHlavicky.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Expediční příkaz neexistuje!\nZáznam nebyl přidán.");

                // kontrola, zdali jiz nebyla hlavička dokončena
                if (rowHlavicka.Rozpracovano >= 2)
                    throw new Exception("Dávka již byla dokončena. Záznam nebyl přidán.");


                taPolozky.FillBybyID(dtPolozky, polozkaID);

                if ((dtPolozky != null) && (dtPolozky.Count() > 0))
                {
                    var RowPolozka = dtPolozky[0];

                    taBuffer.Insert(
                        RowPolozka.ITEMNMBR,
                        RowPolozka.ITEMDESC,
                        RowPolozka.QTY,
                        RowPolozka.SERLTNUM,
                        RowPolozka.NMBRBAL,
                        RowPolozka.VNDITNUM,
                        RowPolozka.CZ_CarKod,
                        RowPolozka.IDPol
                        );

                    // 1) odstranit polozku
                    taPolozky.Delete(polozkaID);


                }



                // 2) odstranit pohyb z lokacniho mechanismu
                //processDelete(polozkaID, Fask.Server.Interfaces.Lokace.ModulName.VYDEJ, new SqlCommand(), connection, trans, new SqlDataAdapter());

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavickaID.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro generovani SSCC kodu
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <returns>Objekt SSCC s vygenerovanym kodem SSCC</returns>
        Fask.Server.Interfaces.BarCodes.SSCC Fask.Server.Interfaces.Expedice.IExpedice.Expedice_SSCC_Generovat(Terminal terminal, User user)
        {
            string ssccstring = string.Empty;
            SqlConnection conn = null;
            SqlDataAdapter adapter = null;
            SqlCommand command = null;

            try
            {
                #region OLD 13.9.2018 TaD
                //conn = new SqlConnection();
                //conn.ConnectionString = Properties.Settings.Default.ConnectionString;
                //adapter = new SqlDataAdapter();
                //command = new SqlCommand();
                //command.CommandText = Properties.Settings.Default.Expedice_SSCC_Generovat_Action; //"CZMST_inc_sscc_proc";
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                //conn.Open();
                //command.Connection = conn;

                ////SqlParameter parameterReturn = command.CreateParameter();
                ////parameterReturn.ParameterName = "@RETURN_VALUE";
                ////parameterReturn.DbType = System.Data.DbType.Int32;
                ////parameterReturn.Direction = System.Data.ParameterDirection.ReturnValue;
                ////command.Parameters.Add(parameterReturn);

                //command.Parameters.AddWithValue("sequence", DateTime.Now.Year);
                //command.Parameters.AddWithValue("terminal", terminal.ID);
                //command.Parameters.AddWithValue("count", 1);

                //SqlParameter parameterReturn = command.CreateParameter();
                //parameterReturn.ParameterName = "endSSCC";
                //parameterReturn.DbType = System.Data.DbType.Int32;
                //parameterReturn.Direction = System.Data.ParameterDirection.Output;
                //command.Parameters.Add(parameterReturn);

                //command.ExecuteNonQuery();
                //int result = (int)parameterReturn.Value;

                //Fask.Server.Interfaces.BarCodes.SSCC sscc = new Fask.Server.Interfaces.BarCodes.SSCC();
                //sscc.AI = Properties.Settings.Default.SSCC_AI;  //"00"; // 2 mista
                //sscc.CompanyPrefix = Properties.Settings.Default.SSCC_CompanyPrefix;    //"00000000";    // 8 mist
                //sscc.Year = DateTime.Now.Year.ToString();    // 4 mista
                //sscc.Number = result.ToString(Properties.Settings.Default.SSCC_Number_Format);     //result.ToString("00000"); // 5 desetinnych mist
                //// 1 misto, check digit
                //sscc.CheckDigit = Fask.Server.Interfaces.BarCodes.SSCC.GenerateParity(sscc.Code);

                #endregion

                #region NEW TaD
                Globals_V1.LoadConfiguration();
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                adapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = "CZMST_get_sscc_sequence_proc";
                //XCommand adpacommand = new XCommand(xdb, "CZMST_get_sscc_sequence_proc");
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@sequence", Globals_V1.Konfigurace.Expedice[0].Expedice_SSCC_Generovat_SEQUENCE);
                command.Parameters.AddWithValue("@terminal", terminal.ID);
                command.Parameters.AddWithValue("@count", 1);

                SqlParameter parameterReturn = command.CreateParameter();
                parameterReturn.ParameterName = "@endSSCC";
                parameterReturn.DbType = System.Data.DbType.Int32;
                parameterReturn.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(parameterReturn);

                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();

                int endSSCC = int.Parse(((IDataParameter)command.Parameters["@endSSCC"]).Value.ToString());

                command.CommandText = "SELECT dbo.CZMST_get_sscc_func(@seq_id,@sscc_count)";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@seq_id", Globals_V1.Konfigurace.Expedice[0].Expedice_SSCC_Generovat_SEQUENCE);
                command.Parameters.AddWithValue("@sscc_count", endSSCC);
                //command.Parameters.Add((new XParameter(xdb, "@seq_id", XDbType.Int)).DatabaseParameter);
                //command.Parameters.Add((new XParameter(xdb, "@sscc_count", XDbType.Int)).DatabaseParameter);
                //((IDataParameter)command.Parameters["@seq_id"]).Value = ;
                //((IDataParameter)command.Parameters["@sscc_count"]).Value = endSSCC;

                object sscc = command.ExecuteScalar();

                conn.Close();

                if (sscc is string)
                {
                    return Fask.Server.Interfaces.BarCodes.SSCC.Parse((string)sscc); // ssccOBJ = new Fask.Server.Interfaces.BarCodes.SSCC()
                    //sscc.
                }
                else
                    return null;

                //return (string)sscc;

                #endregion

                // kontrolni soucet                
                //return sscc;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((conn != null) && (conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
		/// Metoda pro najdeni konkretneho SSCC kodu u položek
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="skl_id">Sklad</param>
        /// <param name="hlavickaID">Guid hlavičky</param>
        /// <param name="code">číslo palety</param>
        /// <returns>Objekt SSCC s sscc</returns>
        Fask.Server.Interfaces.BarCodes.SSCC Fask.Server.Interfaces.Expedice.IExpedice.Expedice_SSCC_Get(Terminal terminal, User user, string skl_id, Guid hlavickaID, string code)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taPolozky = null;

            try
            {
                int nmbrpal = 0;
				// \TODO: prepsat nejak normalne, nebo uplne odstranit moznost zadani koncoveho cisla?? ...
                if (code.Length < 20)
                {
                    bool res = int.TryParse(code, out nmbrpal);
                    if (!res)
                        throw new Exception("Číslo palety musí být číslo");
                }

                Globals_V1.LoadConfiguration();

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                // nacteni vsech dat polozek
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavickaID);

                Fask.Server.Interfaces.BarCodes.SSCC sscc = null;

                // projiti vsech zaznamu, zdali maji obsahuji existujici cislo palety
                foreach (var item in dtPolozky)
                {
                    if (item.IsNMBRPALNull())
                        continue;

                    sscc = new Fask.Server.Interfaces.BarCodes.SSCC();

                    sscc = Fask.Server.Interfaces.BarCodes.SSCC.Parse(item.NMBRPAL);
                    if (code.Length < 20)   // zadana pouze cast kodu
                    {
                        if (nmbrpal.ToString("00000") == sscc.Number)
                            return sscc;
                    }
                    else if (code == sscc.Code) // zadano cele cislo palety
                        return sscc;
                }

                // nenalezeno, vraci null
                return null;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

		#region AfterActionData

		/// <summary>
		/// Metoda která se volá po zpracovani davky
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="sqlconn">SQL Connection</param>
		/// <param name="sqltran">SQL Transakce</param>
		/// <returns>True OK, False - chyba</returns>
		public bool Expedice_Baleni_AfterProcessedAction(Guid hlavicka, System.Data.SqlClient.SqlConnection sqlconn, System.Data.SqlClient.SqlTransaction sqltran)
		{
			try
			{
                Globals_V1.LoadConfiguration();
				string aDP_Action = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action;
				string aDP_Action_P1 = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action_P1;
				string aDP_Action_P2 = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action_P2;
				if (aDP_Action.Length != 0)
				{
					Routines.AfterProcessAction.ExecuteInTransaction(sqlconn, sqltran, TABLE_CZMST_EXPEDICE_BALENI_POLOZKY, hlavicka, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda se volá asynchronne po zpracovani davky
		/// </summary>
		/// <param name="hlavicka">objekt hlavičky</param>
		public void Expedice_Baleni_AfterProcessedActionAsync(object hlavicka)
		{
			try
			{
				Guid d = (Guid)hlavicka;
				Expedice_Baleni_AfterProcessedAction(d);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Společna metoda pro volani po zpracovani davky
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <returns>True- OK, False- chyba</returns>
		public bool Expedice_Baleni_AfterProcessedAction(Guid hlavicka)
		{
			try
			{
                Globals_V1.LoadConfiguration();
				string aDP_Action = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action;
				string aDP_Action_P1 = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action_P1;
				string aDP_Action_P2 = Globals_V1.Konfigurace.Expedice[0].Expedice_Baleni_AfterDataProcessed_Action_P2;
				if (aDP_Action.Length != 0)
				{
					Routines.AfterProcessAction.Execute(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, TABLE_CZMST_EXPEDICE_BALENI_POLOZKY, hlavicka, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		#endregion
        #endregion

        #region Exedice Members

        /// <summary>
		/// Metoda která vytvori hlavicku predlohy podle zaslanych dat.
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavicka">polozky pro pridani</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Hlavicka_Add(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicka)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter taHlavicka = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Server.Interfaces.DataSets.ExpediceHlavicky.CZMST_Expedice_HlavickaRow rowHlavicka = null;
                rowHlavicka = hlavicka.CZMST_Expedice_Hlavicka.Count > 0 ? hlavicka.CZMST_Expedice_Hlavicka.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Hlavička není definována, záznam nebyl přidán");

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                // 1) kontrola existence hlavicky se stejnym ID
                // -> existuje a neni dokoncena - umozni pokracovat                
                // -> existuje a je rozpracovana - umozni pokracovat
                // -> existuje a je dokoncena - neumozni pokracovat
                // -> neexistuje -> vytvori novou a umozni pokracovat
                // co kdyz existuje a jiz je dokoncena?? .. neumoznit pokracovat ...
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaDataTable dtHlavicka = taHlavicka.GetDataByID(hlavicka.CZMST_Expedice_Hlavicka.First().ID);

                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaRow rowTestHlavicka = null;
                rowTestHlavicka = dtHlavicka.Count > 0 ? dtHlavicka.First() : null;

                if (rowTestHlavicka == null)
                {
                    // neexistuje, pridat ...
                    taHlavicka.Insert(
                        rowHlavicka.ID,
                        rowHlavicka.IsPrepravceIDNull() ? string.Empty : rowHlavicka.PrepravceID,
                        rowHlavicka.IsPrepravceSPZNull() ? string.Empty : rowHlavicka.PrepravceSPZ,
                        0,
                        user.ID,
                        terminal.ID,
                        rowHlavicka.IsBarcodeNull() ? string.Empty : rowHlavicka.Barcode,
                        DateTime.Now,
                        null,
                        rowHlavicka.IsSKL_IDNull() ? string.Empty : rowHlavicka.SKL_ID
                        );

                    // vse v poradku
                    si.ID = 0;
                }
                else if (rowTestHlavicka.Rozpracovano >= 2)
                {
                    // existuje a je dokoncena
                    throw new Exception("Hlavička předlohy již byla dokončena.");
                }
                else
                {
                    // existuje a je rozpracovana nebo nerozpracovana
                    si.ID = 1;
                }

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                // 1 - existuje a je rozpracovana nebo nerozpracovana
                return si;

                //int result = int.Parse(so.StatusText);

                //if (result > 0)
                //    countentries = result.ToString();
                //else
                //{
                //    result = -result;
                //    string statusinfo = string.Empty;
                //    switch (result)
                //    {
                //        case 0: statusinfo = "OK"; break;
                //        case 1: statusinfo = "Již existuje"; break;
                //        case 2: statusinfo = "Neexistuje"; break;
                //        case 3: statusinfo = "Bylo nahráno"; break;
                //        default:
                //            statusinfo = "Neznámý status";
                //            break;
                //    }
                //    throw new Exception(statusinfo + " : " + val.Trim());
                //}
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception exx)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exx);
                }

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro smazani radku v hlavičke expedice včetne dat v predloze
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Hlavicka_Del(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Guid hlavickaID)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter taPolozky = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taBaleniHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taBaleniPolozky = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                taBaleniHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taBaleniHlavicka.Connection = connection;
                taBaleniHlavicka.MyTransaction = trans;

                taBaleniPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taBaleniPolozky.Connection = connection;
                taBaleniPolozky.MyTransaction = trans;

                // kontrola, zdali hlavicka [CZMST_Expedice_Hlavicka] je dokoncena
                // -> dokoncena - nepokracovat
                // -> nedokoncena - pokracovat
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaRow rowHlavicka = null;
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaDataTable dtHlavicky = taHlavicka.GetDataByID(hlavickaID);
                rowHlavicka = (dtHlavicky.Count > 0) ? dtHlavicky.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Expediční příkaz neexistuje!\nZáznam nebyl odstraněn.");

                // kontrola, zdali jiz nebyla hlavička dokončena/stornovana
                if (rowHlavicka.Rozpracovano >= 2)
                    throw new Exception("Dávka již byla dokončena. Záznam nebyl odstraněn.");

                // najiti dat podle ID palety z [CZMST_Expedice_Baleni_Polozky]
                SQL_Datasets.Expedice.CZMST_Expedice_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavickaID);

                // naplneni [CZMST_Expedice_Polozky]
                foreach (var rowPolozka in dtPolozky)
                {
                    taBaleniPolozky.Insert(
                        rowPolozka.ID,
                        rowPolozka.IDHB,    // PeV: parovani s puvodni hlavickou objednavky //hlavicka,
                        rowPolozka.ITEMNMBR,
                        rowPolozka.IsITEMDESCNull() ? string.Empty : rowPolozka.ITEMDESC,
                        rowPolozka.IsVNDITNUMNull() ? string.Empty : rowPolozka.VNDITNUM,
                        rowPolozka.IsCZ_CarKodNull() ? string.Empty : rowPolozka.CZ_CarKod,
                        rowPolozka.IsLOCNCODENull() ? string.Empty : rowPolozka.LOCNCODE,
                        rowPolozka.IsSKL_IDNull() ? string.Empty : rowPolozka.SKL_ID,
                        rowPolozka.QTY,
                        rowPolozka.IsQTYPACKNull() ? 0 : rowPolozka.QTYPACK,
                        rowPolozka.QTYMJ,
                        rowPolozka.MJ,
                        rowPolozka.SERLTNUM,
                        rowPolozka.IsWEIGHTNull() ? (decimal?)null : rowPolozka.WEIGHT,
                        rowPolozka.IsNMBRPALNull() ? string.Empty : rowPolozka.NMBRPAL,
                        rowPolozka.IsTYPEPALNull() ? string.Empty : rowPolozka.TYPEPAL,
                        rowPolozka.IsPRINTEDNull() ? (byte)0 : rowPolozka.PRINTED,
                        rowPolozka.IsNMBRPALNull() ? string.Empty : rowPolozka.NMBRPAL,
                         rowPolozka.IsIDPolNull() ? (Guid?)null : rowPolozka.IDPol
                    );
                }

                // odstraneni dat [CZMST_Expedice_Polozky]

                taPolozky.DeleteByIDH(hlavickaID);

                taHlavicka.Delete(hlavickaID);
				// \TODO: kontrola, zdali paleta jiz neni v seznamu?? ... nemelo by byt treba, v transakci se maze ...

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception ex2)
                {
					Fask.Logging.ExceptionHandler2.Handle( Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavickaID.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda ktera vraci hlavičky z expedice
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="sklad">Sklad</param>
		/// <returns>Dataset ExpediceHlavicky, naplnen hlavičkama</returns>
        Fask.Server.Interfaces.DataSets.ExpediceHlavicky Fask.Server.Interfaces.Expedice.IExpedice.Expedice_GetHlavicky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                string select =
                    "select hlavicka.*, ISNULL(Y.SumItems, 0) as SumItems " +
                    "from " + TABLE_CZMST_EXPEDICE_HLAVICKA + " hlavicka " +
                    "LEFT JOIN ( " +
                    "   SELECT IDH, COUNT(*) SumItems " +
                    "   FROM  " +
                    "   ( " +
                    "       SELECT IDH, NMBRPAL, COUNT(*) as sumcount " +
                    "       from CZMST_Expedice_Polozky polozky2 " +
                    "       group by IDH, NMBRPAL  " +
                    "   ) X " +
                    "   GROUP BY IDH " +
                    ") as Y " +
                    "ON Y.IDH=hlavicka.ID " +
                    "WHERE " +
                    "   hlavicka.Rozpracovano <=0 OR (hlavicka.Rozpracovano=1 and hlavicka.TermID = " + terminal.ID + ") " +
                    "ORDER BY hlavicka.DateCreated";

                Fask.Server.Interfaces.DataSets.ExpediceHlavicky hlavicky = new Fask.Server.Interfaces.DataSets.ExpediceHlavicky();

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(hlavicky, hlavicky.CZMST_Expedice_Hlavicka.TableName);
                }

                return hlavicky;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        /// <summary>
		/// Metoda která vrací palety na expedici
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="sklad">SKlad</param>
        /// <param name="hlavickaID">Guid hlavičky</param>
		/// <returns>Dataset Expedice naplnen paletama</returns>
        Fask.Server.Interfaces.DataSets.Expedice Fask.Server.Interfaces.Expedice.IExpedice.Expedice_GetPalety(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Guid hlavickaID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlTransaction trans = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
				// \TODO: poresit MJ, pouzit MIN(MJ) nebo neco jineho??
                string select =
                    "select distinct polozky.IDH, polozky.NMBRPAL, polozky.TYPEPAL, ISNULL(N.SUMITEMS, 0) as SumItems, ISNULL(N.SUMWEIGHT, 0) as SumWeight, '' as Barcode, 'kg' as MJ " +
                    "from " + TABLE_CZMST_EXPEDICE_POLOZKY + " polozky " +
                    "LEFT JOIN (  " +
                    "   SELECT polozky2.NMBRPAL I4ITEM, COUNT(*) SUMITEMS, SUM(QTY*WEIGHT) SUMWEIGHT " +
                    "   from " + TABLE_CZMST_EXPEDICE_POLOZKY + " polozky2  " +
                    "   group by polozky2.NMBRPAL " +
                    ") as N  " +
                    "ON N.I4ITEM=polozky.NMBRPAL " +
                    "where " +
                    "   polozky.IDH=@idh";
                adapter = new SqlDataAdapter();

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command = new SqlCommand(select, connection, trans);
                command.Parameters.AddWithValue("@idh", hlavickaID);
                adapter.SelectCommand = command;

                Fask.Server.Interfaces.DataSets.Expedice polozky = new Fask.Server.Interfaces.DataSets.Expedice();

                adapter.Fill(polozky, polozky.Expedice_Palety.TableName);

                if (trans != null)
                    trans.Commit();

                return polozky;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception ex2)
                {
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                return null;
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda která vraci palety podle čísla palety
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">Uživatel</param>
        /// <param name="skl_id">Sklad</param>
        /// <param name="nmbrpal">číslo palety</param>
		/// <returns>Expedice, naplnene položkama pro paletu </returns>
        Fask.Server.Interfaces.DataSets.Expedice Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Paleta_Get(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string skl_id, string nmbrpal)
        {
            // kontrola, zdali jiz neni pridana a jinak pridat ...
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            Fask.Server.Interfaces.DataSets.Expedice dsExpedice = new Fask.Server.Interfaces.DataSets.Expedice();


            try
            {
                Globals_V1.LoadConfiguration();
				// \TODO: poresit MJ, pouzit MIN(MJ) nebo neco jineho??
                string commandText =
                    "select distinct polozky.IDH, polozky.NMBRPAL, polozky.TYPEPAL, ISNULL(N.SUMITEMS, 0) as SumItems, ISNULL(N.SUMWEIGHT, 0) as SumWeight, '' as Barcode, 'kg' as MJ " +
                    "   from " + TABLE_CZMST_EXPEDICE_BALENI_POLOZKY + " polozky " +
                    "LEFT JOIN (   " +
                    "   SELECT polozky2.NMBRPAL I4ITEM, COUNT(*) SUMITEMS, SUM(QTY*WEIGHT) SUMWEIGHT " +
                    "   from " + TABLE_CZMST_EXPEDICE_BALENI_POLOZKY + " polozky2 " +
                    "   group by polozky2.NMBRPAL " +
                    ") as N  " +
                    "ON N.I4ITEM=polozky.NMBRPAL " +
                    "WHERE 1=1 " +
                    (string.IsNullOrEmpty(nmbrpal) ? string.Empty : " AND polozky.NMBRPAL = @nmbrpal ")
                    ;


                adapter = new SqlDataAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command = new SqlCommand(commandText, connection, trans);

                if (!string.IsNullOrEmpty(nmbrpal))
                    command.Parameters.AddWithValue("@nmbrpal", nmbrpal);

                adapter.SelectCommand = command;

                adapter.Fill(dsExpedice, dsExpedice.Expedice_Palety.TableName);

                if (trans != null)
                    trans.Commit();

                return dsExpedice;
            }
            catch (Exception ex)
            {
				
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch (Exception ex2)
                {
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                }

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro dotaženi položek z expedice
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="hlavickaID">Guid Hlavičky</param>
		/// <returns>Expedice, dotažena data</returns>
        Fask.Server.Interfaces.DataSets.Expedice Fask.Server.Interfaces.Expedice.IExpedice.Expedice_GetPolozky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, Guid hlavickaID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlTransaction trans = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                string select =
                    "select * " +
                    "from " + TABLE_CZMST_EXPEDICE_POLOZKY + " " +
                    "where " +
                    "   IDH=@idh " +
                    "order by nmbrpal";
                adapter = new SqlDataAdapter();

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command = new SqlCommand(select, connection, trans);
                command.Parameters.AddWithValue("@idh", hlavickaID);
                adapter.SelectCommand = command;

                Fask.Server.Interfaces.DataSets.Expedice polozky = new Fask.Server.Interfaces.DataSets.Expedice();

                adapter.Fill(polozky, polozky.CZMST_Expedice_Polozky.TableName);

                if (trans != null)
                    trans.Commit();

                return polozky;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                return null;
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro zpracovaní davky expedce na serveru
        /// </summary>
        /// <param name="hlavicka">Guid hlavičky</param>
        /// <param name="user">uživatel</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="sklad">Sklad</param>
		/// <param name="processExpediceState">příznak co se ma stat: Uvolnit, ZpracovatAPokracovat, Zpracovat</param>
		/// <returns>StatusObject - nese informace o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusObject Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Process(Guid hlavicka, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Expedice.ProcessState processExpediceState)
        {
            Globals_V1.LoadConfiguration();
            string guidDavka = hlavicka.ToString();
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Expedice[0].StatusObjectsDirectory, guidDavka));
            StatusObject so = new StatusObject(filePath);

            //zjistit zda soubor s danym guid existuje
            if (File.Exists(filePath))
            { //soubor jiz existuje
                so = StatusObject.Load(filePath);
                if (!so.Exception)
                    return so;
            }

            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter taPolozky = null;
            // SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter taPrijem = null;
            //SQL_Datasets.PrijemTableAdapters.CZMST_PE_SNTableAdapter taPrijemSN = null;

            try
            {
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;
                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;
                //taPrijem = new SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                //taPrijem.Connection = connection;
                //taPrijem.MyTransaction = trans;
                //taPrijemSN = new SQL_Datasets.PrijemTableAdapters.CZMST_PE_SNTableAdapter();
                //taPrijemSN.Connection = connection;
                //taPrijemSN.MyTransaction = trans;

                bool uvolnitdavku = processExpediceState == Fask.Server.Interfaces.Expedice.ProcessState.Uvolnit;
                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");
                    // 1) nacist data hlavicky a nasledne je odstranit (vcetne smazani dat v lokacnimmechanismu)
                }
                else
                {
                    // zpracovat davku
                    so.Write("kontrola existence hlavicky");

                    // kontrola existence hlavicky
                    SQL_Datasets.Expedice.CZMST_Expedice_HlavickaDataTable dtHlavicka = taHlavicka.GetDataByID(hlavicka);
                    SQL_Datasets.Expedice.CZMST_Expedice_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDH(hlavicka);

                    if (dtHlavicka.Count <= 0)
                        throw new Exception("Expediční příkaz neexistuje!");
                    //throw new Exception("Expediční příkaz nebylo možné zpracovat.\nExpediční příkaz neexistuje!");

					// \TODO: kontrola, zdali jiz nebyl expedicni prikaz zpracovan (DateFinished != null)

                    so.Write("zapsat davku");
                    // nastavi dokonceno u davky
					// \TODO: kontrola, zdali je polozka ulozena v DB => momentalne musi vsechny online pohyby projit ...
                    taHlavicka.UpdateRozpracovanoDateFinished(2, DateTime.Now, hlavicka);

                    if (Globals_V1.Konfigurace.Expedice[0].Expedice_AfterData_Generate_Prijemka)
                    {
                        so.Write("generovat prijemku");
                        //int? countentries = taPrijem.ScalarMaxCountEntries();


                        //countentries = countentries.HasValue ? (countentries + 1) : 1;
                        //string ponumber = countentries.ToString();      // TODO: dodelat ...

						// \TODO: zkontrolovat existenci dat, neexistujici pridat=> momentalne musi vsechny online pohyby projit, takze neni treba ...
                        //SQL_Datasets.Prijem.CZMST_PEDataTable dtPrijem = new SQL_Datasets.Prijem.CZMST_PEDataTable();
                        //SQL_Datasets.Prijem.CZMST_PERow rowPrijem = null;
                        //SQL_Datasets.Prijem.CZMST_PE_SNDataTable dtPrijemSN = new SQL_Datasets.Prijem.CZMST_PE_SNDataTable();
                        //SQL_Datasets.Prijem.CZMST_PE_SNRow rowPrijemSN = null;

                        //int ord = 0;
                        foreach (var rowPolozka in dtPolozky)
                        {
                            // kontrola, zdali itemnmbr a paleta uz neni pridana
                            // pocet polozek podle itemnmbr a nmbrpal
                            //int count = dtPrijem.Where(x => x.ITEMNMBR == rowPolozka.ITEMNMBR && x.NMBRPAL == rowPolozka.NMBRPAL).Count();
                            // pocet polozek podle itemnmbr
                            //int count = dtPrijem.Where(x => x.ITEMNMBR == rowPolozka.ITEMNMBR).Count();

                            // nenalezeno, mozne pridat
                            //    if (count == 0)
                            //    {
                            //        rowPrijem = dtPrijem.NewCZMST_PERow(); ;
                            //        rowPrijem.CountEntries = countentries.Value;
                            //        rowPrijem.PONUMBER = ponumber;     // dodelat ...
                            //        rowPrijem.ITEMDESC = rowPolozka.IsITEMDESCNull() ? string.Empty : rowPolozka.ITEMDESC;
                            //        rowPrijem.ORD = --ord;
                            //        rowPrijem.ITEMNMBR = rowPolozka.ITEMNMBR;
                            //        rowPrijem.VNDDOCNM = string.Empty;
                            //        rowPrijem.VNDITNUM = rowPolozka.IsVNDITNUMNull() ? string.Empty : rowPolozka.VNDITNUM;
                            //        rowPrijem.CZ_CarKod = rowPolozka.IsCZ_CarKodNull() ? string.Empty : rowPolozka.CZ_CarKod;
                            //        rowPrijem.SKL_ID = rowPolozka.IsSKL_IDNull() ? string.Empty : rowPolozka.SKL_ID;
                            //        rowPrijem.LOCNCODE = string.Empty;  // nastavit??
                            //        rowPrijem.MJ = rowPolozka.MJ;
                            //        // suma mnozstvi podle itemnmbr a nmbrpal
                            //        decimal sum = dtPolozky.Where(x => x.ITEMNMBR == rowPolozka.ITEMNMBR && x.NMBRPAL == rowPolozka.NMBRPAL).Sum(x => x.QTY);
                            //        // suma mnozstvi podle itemnmbr
                            //        //decimal sum = dtPolozky.Where(x => x.ITEMNMBR == rowPolozka.ITEMNMBR).Sum(x => x.QTY);
                            //        rowPrijem.QTYSHPPD = sum;
                            //        rowPrijem.QTYPACK = rowPolozka.IsQTYPACKNull() ? 0 : rowPolozka.QTYPACK;
                            //        rowPrijem.CZ_DatVyr_Track = 0;
                            //        rowPrijem.CZ_DatVyr_Delka = 0;
                            //        rowPrijem.CZ_SerNum_Track = 0;// 15.7.2016 PeV: po konzultaci s JiS prozatim zmeneno na CZ_SerNumTrack = 0 (puvodne bylo 2) ... ;  // TODO: dotahnout??
                            //        rowPrijem.CZ_SerNum_Delka = 0;
                            //        rowPrijem.CZ_SW_Track = 0;
                            //        rowPrijem.CZ_SW_Delka = 0;
                            //        rowPrijem.CZ_Doslo = 0;
                            //        if (rowPolozka.IsWEIGHTNull())
                            //            rowPrijem.SetWEIGHTNull();
                            //        else
                            //            rowPrijem.WEIGHT = rowPolozka.WEIGHT;

                            //        if (rowPolozka.IsNMBRPALNull())
                            //            rowPrijem.SetNMBRPALNull();
                            //        else
                            //            rowPrijem.NMBRPAL = rowPolozka.NMBRPAL;   // neni jedinecne cislo palety
                            //        //rowPrijem.NMBRPAL = string.Empty;

                            //        if (rowPolozka.IsTYPEPALNull())
                            //            rowPrijem.SetTYPEPALNull();
                            //        else
                            //            rowPrijem.TYPEPAL = rowPolozka.TYPEPAL;

                            //        rowPrijem.ITEMCODE = string.Empty;

                            //        rowPrijem.SERLTNUM = rowPolozka.SERLTNUM;

                            //        dtPrijem.AddCZMST_PERow(rowPrijem);
                            //    }

							//    // \TODO: dotahnout sarzi
                            //    // plneni czmst_pe_sn
                            //    if (!string.IsNullOrEmpty(rowPolozka.SERLTNUM.Trim()))
                            //    {
                            //        count = dtPrijemSN.Where(x => x.ITEMNMBR.Trim() == rowPolozka.ITEMNMBR.Trim() && x.SERLNMBR.Trim() == rowPolozka.SERLTNUM.Trim()).Count();
                            //        if (count == 0)
                            //        {
                            //            rowPrijemSN = dtPrijemSN.NewCZMST_PE_SNRow();
                            //            rowPrijemSN.CountEntries = countentries.Value;
                            //            rowPrijemSN.ITEMNMBR = rowPolozka.ITEMNMBR;
                            //            rowPrijemSN.SERLNMBR = rowPolozka.SERLTNUM;

                            //            dtPrijemSN.AddCZMST_PE_SNRow(rowPrijemSN);
                            //        }
                            //    }
                            //}

                            //so.Write("zapsat pe");
                            //taPrijem.Update(dtPrijem);

                            //so.Write("zapsat pe_sn");
                            //taPrijemSN.Update(dtPrijemSN);
                        }
                    }
                }

                #region Action after data processed
                // after process 
                if (Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action_Asynchronous)
                {
                    so.Write("commit transakce");

                    if (trans != null)
                        trans.Commit();
                    trans = null;

                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Expedice_AfterProcessedActionAsync));
                    thread.Start(hlavicka);
                }
                else
                {
                    // pridani after process action do transakce
                    // Uvolnuje se davka -> nevola se afterProcessedAction, neuvolnuje se davka -> vola se afterProcessedAction
                    if (!uvolnitdavku)
                    {
                        if (!Expedice_AfterProcessedAction(hlavicka, connection, trans))
                        {

							try
							{
								if (trans != null)
									trans.Rollback();
							}
							catch (Exception ex2)
							{
								Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
								Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
							}

                            trans = null;
                            so.Exception = true;
                            so.Write("chyba");
                            return so;
                        }
                        //else   // vse v poradku, commit udelat
                    }

                    // commit transakce
                    so.Write("commit transakce");
                    if (trans != null)
                        trans.Commit();
                }
                #endregion

                so.SetOK();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                so.Exception = true;
                so.Write(ex.Message);

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }


            so.SetOK();
            return so;
        }

		#region AfterProcedAction

		/// <summary>
		/// MEtoda se volá pro zpracovani davky
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <param name="sqlconn">SQL Connection</param>
		/// <param name="sqltran">SQL Transakce</param>
		/// <returns>True - OK, False - chyba</returns>
		public bool Expedice_AfterProcessedAction(Guid hlavicka, System.Data.SqlClient.SqlConnection sqlconn, System.Data.SqlClient.SqlTransaction sqltran)
		{
			try
			{
                Globals_V1.LoadConfiguration();
				string aDP_Action = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action;
				string aDP_Action_P1 = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action_P1;
				string aDP_Action_P2 = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action_P2;
				if (aDP_Action.Length != 0)
				{
					Routines.AfterProcessAction.ExecuteInTransaction(sqlconn, sqltran, TABLE_CZMST_PI, hlavicka, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// MEtoda se volá pro zpracovani davky asynchronne
		/// </summary>
		/// <param name="hlavicka">Objekt hlavičky</param>
		public void Expedice_AfterProcessedActionAsync(object hlavicka)
		{
			try
			{
				Guid d = (Guid)hlavicka;
				Expedice_AfterProcessedAction(d);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// MEtoda se volá pro zpracovani davky Společna metoda
		/// </summary>
		/// <param name="hlavicka">Guid hlavičky</param>
		/// <returns>True - OK, False - chyba</returns>
		public bool Expedice_AfterProcessedAction(Guid hlavicka)
		{
			try
			{
                Globals_V1.LoadConfiguration();
				string aDP_Action = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action;
				string aDP_Action_P1 = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action_P1;
				string aDP_Action_P2 = Globals_V1.Konfigurace.Expedice[0].Expedice_AfterDataProcessed_Action_P2;
				if (aDP_Action.Length != 0)
				{
					Routines.AfterProcessAction.Execute(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, TABLE_CZMST_PI, hlavicka, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion

        /// <summary>
		/// Metoda pro pridany položky na paletu
        /// </summary>
        /// <param name="hlavicka">Guid hlavičky</param>
        /// <param name="terminal">Terminal</param>
        /// <param name="user">uživatel</param>
        /// <param name="sklad">Sklad</param>
        /// <param name="nmbrpal">číslo palety</param>
		/// <returns>StatusInfo - objekt ktery nese info o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Polozka_Add(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, Fask.Server.Interfaces.Classes.Sklad sklad, string nmbrpal)
        {
            // kontrola, zdali jiz neni pridana a jinak pridat ...
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter taPolozky = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taBaleniHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taBaleniPolozky = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                taBaleniHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taBaleniHlavicka.Connection = connection;
                taBaleniHlavicka.MyTransaction = trans;

                taBaleniPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taBaleniPolozky.Connection = connection;
                taBaleniPolozky.MyTransaction = trans;

                // kontrola, zdali hlavicka [CZMST_Expedice_Hlavicka] je dokoncena
                // -> dokoncena - nepokracovat
                // -> nedokoncena - pokracovat
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaRow rowHlavicka = null;
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaDataTable dtHlavicky = taHlavicka.GetDataByID(hlavicka);
                rowHlavicka = (dtHlavicky.Count > 0) ? dtHlavicky.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Záznam nebyl přidán.\nExpediční příkaz neexistuje!");

                // kontrola, zdali jiz nebyla hlavička dokončena/stornovana
                if (rowHlavicka.Rozpracovano >= 2)
                    throw new Exception("Záznam nebyl přidán.\nDávka již byla dokončena.");

                // najiti dat podle ID palety z [CZMST_Expedice_Baleni_Polozky]
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_PolozkyDataTable dtBaleniPolozky = taBaleniPolozky.GetDataByNmbrpal(nmbrpal);
                if (dtBaleniPolozky.Count == 0)
                    throw new Exception(string.Format("Paleta '{0}' neexistuje nebo neobsahuje žádné položky", nmbrpal));

                // kontrola, zdali hlavicka [CZMST_Expedice_Baleni_Hlavicka] je dokoncena
                // -> dokonceno - pokracovat
                // -> nedokonceno - nepokracovat
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtBaleniHlavicky = taBaleniHlavicka.GetDataByID(dtBaleniPolozky.First().IDH);
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaRow rowBaleniHlavicka = null;
                rowBaleniHlavicka = (dtBaleniHlavicky.Count > 0) ? dtBaleniHlavicky.First() : null;
                if (rowBaleniHlavicka == null)
                    throw new Exception("Příkaz balení neexistuje!\nZáznam nebyl přidán.");

                // kontrola, zdali jiz nebyla hlavička dokončena
                if (rowBaleniHlavicka.Rozpracovano != 2)
                    throw new Exception(string.Format("Balení palety '{0}' nebylo dokončeno.\nZáznam nebyl přidán.", nmbrpal));

                // naplneni [CZMST_Expedice_Polozky]
                foreach (var rowPolozka in dtBaleniPolozky)
                {
                    taPolozky.Insert(
                        rowPolozka.ID,
                        hlavicka,
                        rowPolozka.ITEMNMBR,
                        rowPolozka.IsITEMDESCNull() ? string.Empty : rowPolozka.ITEMDESC,
                        rowPolozka.IsVNDITNUMNull() ? string.Empty : rowPolozka.VNDITNUM,
                        rowPolozka.IsCZ_CarKodNull() ? string.Empty : rowPolozka.CZ_CarKod,
                        rowPolozka.IsLOCNCODENull() ? string.Empty : rowPolozka.LOCNCODE,
                        rowPolozka.IsSKL_IDNull() ? string.Empty : rowPolozka.SKL_ID,
                        rowPolozka.QTY,
                        rowPolozka.IsQTYPACKNull() ? 0 : rowPolozka.QTYPACK,
                        rowPolozka.QTYMJ,
                        rowPolozka.MJ,
                        rowPolozka.SERLTNUM,
                        rowPolozka.IsWEIGHTNull() ? (decimal?)null : rowPolozka.WEIGHT,
                        rowPolozka.IsNMBRPALNull() ? string.Empty : rowPolozka.NMBRPAL,
                        rowPolozka.IsTYPEPALNull() ? string.Empty : rowPolozka.TYPEPAL,
                        rowPolozka.IsPRINTEDNull() ? (byte)0 : rowPolozka.PRINTED,
                        rowPolozka.IDH,
                        rowPolozka.IsIDPolNull() ? Guid.Empty : rowPolozka.IDPol,
                        rowPolozka.IsNMBRBALNull() ? string.Empty : rowPolozka.NMBRBAL
                        );
                }

                // odstraneni dat [CZMST_Expedice_Baleni_Polozky]
                taBaleniPolozky.DeleteByNmbrpal(nmbrpal);

				// \TODO: kontrola, zdali paleta jiz neni v seznamu?? ... nemelo by byt treba, v transakci se maze ...

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavicka.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
		/// Metoda pro smazani položky z palety
        /// </summary>
        /// <param name="hlavicka">Guid hlavičky</param>
        /// <param name="terminal">ID Terminalu</param>
        /// <param name="user">ID uživatele</param>
        /// <param name="nmbrpal">číslo palety</param>
		/// <returns>StatusInfo, objekt nese informace o stavu</returns>
        Fask.Server.Interfaces.Classes.StatusInfo Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Polozka_Del(Guid hlavicka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.User user, string nmbrpal)
        {
            SqlConnection connection = null;
            SqlTransaction trans = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter taHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter taPolozky = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taBaleniHlavicka = null;
            SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter taBaleniPolozky = null;
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                taHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_HlavickaTableAdapter();
                taHlavicka.Connection = connection;
                taHlavicka.MyTransaction = trans;

                taPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_PolozkyTableAdapter();
                taPolozky.Connection = connection;
                taPolozky.MyTransaction = trans;

                taBaleniHlavicka = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                taBaleniHlavicka.Connection = connection;
                taBaleniHlavicka.MyTransaction = trans;

                taBaleniPolozky = new SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_PolozkyTableAdapter();
                taBaleniPolozky.Connection = connection;
                taBaleniPolozky.MyTransaction = trans;

                // kontrola, zdali hlavicka [CZMST_Expedice_Hlavicka] je dokoncena
                // -> dokoncena - nepokracovat
                // -> nedokoncena - pokracovat
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaRow rowHlavicka = null;
                SQL_Datasets.Expedice.CZMST_Expedice_HlavickaDataTable dtHlavicky = taHlavicka.GetDataByID(hlavicka);
                rowHlavicka = (dtHlavicky.Count > 0) ? dtHlavicky.First() : null;

                if (rowHlavicka == null)
                    throw new Exception("Expediční příkaz neexistuje!\nZáznam nebyl odstraněn.");

                // kontrola, zdali jiz nebyla hlavička dokončena/stornovana
                if (rowHlavicka.Rozpracovano >= 2)
                    throw new Exception("Dávka již byla dokončena. Záznam nebyl odstraněn.");

                // najiti dat podle ID palety z [CZMST_Expedice_Baleni_Polozky]
                SQL_Datasets.Expedice.CZMST_Expedice_PolozkyDataTable dtPolozky = taPolozky.GetDataByIDHAndNmbrpal(hlavicka, nmbrpal);
                if (dtPolozky.Count == 0)
                    throw new Exception(string.Format("Paleta '{0}' neexistuje nebo neobsahuje žádné položky", nmbrpal));

                // neni potreba ...
                // kontrola, zdali hlavicka [CZMST_Expedice_Baleni_Hlavicka] je dokoncena
                // -> dokonceno - pokracovat
                // -> nedokonceno - nepokracovat
                //Fask.Module.SBKomplet.Steinex.SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaDataTable dtBaleniHlavicky = taBaleniHlavicka.GetDataByID(dtBaleniPolozky.First().IDH);
                //Fask.Module.SBKomplet.Steinex.SQL_Datasets.Expedice.CZMST_Expedice_Baleni_HlavickaRow rowBaleniHlavicka = null;
                //rowBaleniHlavicka = (dtBaleniHlavicky.Count > 0) ? dtBaleniHlavicky.First() : null;
                //if (rowBaleniHlavicka == null)
                //    throw new Exception("Záznam nebyl přidán.\nPříkaz balení neexistuje!");

                //// kontrola, zdali jiz nebyla hlavička dokončena
                //if (rowBaleniHlavicka.Rozpracovano != 2)
                //    throw new Exception("Dávka balení nebyla dokončena. Záznam nebyl přidán.");

                // naplneni [CZMST_Expedice_Polozky]
                foreach (var rowPolozka in dtPolozky)
                {
                    taBaleniPolozky.Insert(
                        rowPolozka.ID,
                        rowPolozka.IDHB,    // PeV: parovani s puvodni hlavickou objednavky //hlavicka,
                        rowPolozka.ITEMNMBR,
                        rowPolozka.IsITEMDESCNull() ? string.Empty : rowPolozka.ITEMDESC,
                        rowPolozka.IsVNDITNUMNull() ? string.Empty : rowPolozka.VNDITNUM,
                        rowPolozka.IsCZ_CarKodNull() ? string.Empty : rowPolozka.CZ_CarKod,
                        rowPolozka.IsLOCNCODENull() ? string.Empty : rowPolozka.LOCNCODE,
                        rowPolozka.IsSKL_IDNull() ? string.Empty : rowPolozka.SKL_ID,
                        rowPolozka.QTY,
                        rowPolozka.IsQTYPACKNull() ? 0 : rowPolozka.QTYPACK,
                        rowPolozka.QTYMJ,
                        rowPolozka.MJ,
                        rowPolozka.SERLTNUM,
                        rowPolozka.IsWEIGHTNull() ? (decimal?)null : rowPolozka.WEIGHT,
                        rowPolozka.IsNMBRPALNull() ? string.Empty : rowPolozka.NMBRPAL,
                        rowPolozka.IsTYPEPALNull() ? string.Empty : rowPolozka.TYPEPAL,
                        rowPolozka.IsPRINTEDNull() ? (byte)0 : rowPolozka.PRINTED,
                        rowPolozka.IsNMBRBALNull() ? string.Empty : rowPolozka.NMBRBAL,
                        rowPolozka.IsIDPolNull() ? Guid.Empty : rowPolozka.IDPol
                    );
                }

                // odstraneni dat [CZMST_Expedice_Polozky]
                taPolozky.DeleteByIDHAndNmbrpal(hlavicka, nmbrpal);

				// \TODO: kontrola, zdali paleta jiz neni v seznamu?? ... nemelo by byt treba, v transakci se maze ...

                if (trans != null)
                    trans.Commit();

                // 0 - vse v poradku
                si.ID = 0;
                return si;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				try
				{
					if (trans != null)
						trans.Rollback();
				}
				catch (Exception ex2)
				{
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, "Rollback transakce se nezdaril\nIDH: '" + hlavicka.ToString() + "'");
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
				}

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }


		/// <summary>
		/// Metoda pro Tisk z Baleni
		/// </summary>
		/// <param name="guidHlavickaBaleni">Guid hlavičky</param>
		/// <param name="NMBRPAL">číslo palety</param>
		/// <returns>Data pro tisk</returns>
        Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable Fask.Server.Interfaces.Expedice.IExpedice.Expedice_Baleni_Tisk(Guid guidHlavickaBaleni, string NMBRPAL)
        {
            Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable dtOUT = new Fask.Server.Interfaces.DataSets.ExpediceBaleni.CZMST_Expedice_Baleni_Polozky_TISKDataTable();

            try
            {
                Globals_V1.LoadConfiguration();
                SQL_Datasets.Expedice.CZMST_Expedice_Baleni_Polozky_TISKDataTable dt = null;

                SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_TISK_TableAdapter ta = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_TISK_TableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				//SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_TISK_TableAdapter taNMBMBAL = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_TISK_TableAdapter();
				//taNMBMBAL.Connection = new SqlConnection(Properties.Settings.Default.ConnectionString);

                SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_SNTableAdapter taSN = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_Polozky_SNTableAdapter();
                taSN.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter taSI = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter();
                taSI.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


                SQL_Datasets.PohodaDataSetTableAdapters.POHODA_OdberatelTableAdapter taODBER = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.PohodaDataSetTableAdapters.POHODA_OdberatelTableAdapter();
                taODBER.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

				//SQL_Datasets.PohodaDataSetTableAdapters.SKzTableAdapter taSKz = new Fask.Module.Pohoda.I_Tec.SQL_Datasets.PohodaDataSetTableAdapters.SKzTableAdapter();
				//taSKz.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);



                dt = ta.GetData_TISK_ByIDH_NMBRPAL(NMBRPAL.Trim(), guidHlavickaBaleni);


                //var countNMBRBAL = taNMBMBAL.GetData_TISK_ByIDH_NMBRPAL(NMBRPAL.Trim(), guidHlavickaBaleni);

				int PocetBalikuNaPalete = Database.Pohoda.GetPocetBaliku(NMBRPAL.Trim(), guidHlavickaBaleni);

				//if ((countNMBRBAL != null) && (countNMBRBAL.Count > 0))
				//{
				//    PocetBalikuNaPalete = countNMBRBAL.Count;
				//}

				#region Dotazeni všech VNDDOCNM v balikoch...

				string VNDDOCNMList = Database.Pohoda.GetAllVNDDOCNM(NMBRPAL.Trim(), guidHlavickaBaleni);


				
				#endregion
              
                dtOUT.Clear();

                //int FontWidth = Properties.Settings.Default.Tisk_Font_Size_Width;
                //int FontHeight = Properties.Settings.Default.Tisk_Font_Size_Height;

                foreach (SQL_Datasets.Expedice.CZMST_Expedice_Baleni_Polozky_TISKRow item in dt)
                {
                    string Doprava = string.Empty;

                    #region Vypocet SN

                    string SERLTNUMSeznam = string.Empty;
                    string LLSN = string.Empty;
                    //string FontSizeSN_HEIGHT = string.Empty;
                    //string FontSizeSN_WIDTH = string.Empty;

                    List<string> listSN = new List<string>();
                    Dictionary<int, string> RadkySN = new Dictionary<int, string>();
                    int CountRow = 0;
                    string tmpSN = string.Empty;

                    var dtSN = taSN.GetDataByITEMNMBR_NMBPAL(item.ITEMNMBR.Trim(), item.NMBRPAL.Trim());

                    if ((dtSN != null) && (dtSN.Count > 0))
                    {
                        //List<string> listSN = new List<string>();

                        foreach (var SN in dtSN)
                        {
                            if (!string.IsNullOrEmpty(SN.SERLTNUM.Trim()))
                                listSN.Add(SN.SERLTNUM.Trim());
                        }

                        if (listSN.Count == 0)
                        {
                            // Zadne SN
                            tmpSN = string.Empty;
                            //SERLTNUMSeznam = "-";
                            //LLSN = (FontHeight + Properties.Settings.Default.Tisk_Font_Size_Medzera).ToString();
                            //FontSizeSN_HEIGHT = FontHeight.ToString();
                            //FontSizeSN_WIDTH = FontWidth.ToString();
                        }

                        else
                        {

                            string result = String.Join(", ", listSN.ToArray());
                            

                            //string tmp = string.Empty;

                            do
                            {
                                if (result.Length > Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_ZnakuNaRadek)
                                {
                                    string tmpsnrow = "^FD";
                                    tmpsnrow += result.Substring(0, Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_ZnakuNaRadek);
                                    tmpsnrow += "^FS";
                                    RadkySN.Add(CountRow++, tmpsnrow);
                                    result = result.Substring(Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_ZnakuNaRadek);
                                }
                                else
                                {
                                    string tmpsnrow = "^FD";
                                    tmpsnrow += result;
                                    tmpsnrow += "^FS";
                                    RadkySN.Add(CountRow++, tmpsnrow);
                                    result = string.Empty;

                                }

                            } while (result.Length != 0);


                            // = result;


                            //decimal radku = (int)Math.Ceiling((double)result.Length / Properties.Settings.Default.Tisk_Font_Size_ZnakuNaRadek);
                            //decimal radku = (decimal)result.Length / (decimal)Properties.Settings.Default.Tisk_Font_Size_ZnakuNaRadek;

                            //int velkost = ((FontHeight + Properties.Settings.Default.Tisk_Font_Size_Medzera) * radku;
                            //int velkost = (int)Math.Ceiling(((Properties.Settings.Default.Tisk_Font_Size_Height + Properties.Settings.Default.Tisk_Font_Size_Medzera) * (decimal)CountRow));
                            int velkost = (int)Math.Ceiling((Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height * (decimal)CountRow));

                         
                            LLSN = (velkost + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Medzera).ToString();
                            //FontSizeSN_HEIGHT = FontHeight.ToString();
                            //FontSizeSN_WIDTH = FontWidth.ToString();


                            #region Skladani časti SN

                            tmpSN += "^XA";
                            tmpSN += "^DFR:SN.ZPL";
                            tmpSN += "^POI";
                            tmpSN += "^XB";
                            tmpSN += "^CI31";
                            tmpSN += "^LL" + LLSN;
                            tmpSN += "^FO25,0";
                            tmpSN += "^A0N," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height + "," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Width;
                            tmpSN += "^FD" + "Výr.č.:";
                            tmpSN += "^FS";

                            int FOY = 0;

                            foreach (var SNRow in RadkySN)
                            {

                                tmpSN += "^FO125," + FOY.ToString();
                                FOY = FOY + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height ;
                                tmpSN += "^A0N," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height + "," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Width;
                                tmpSN += "^TBN," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_SirkaStitku.ToString() + "," + Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height;
                                tmpSN += "^FN" + SNRow.Key.ToString();
                                tmpSN += "^FS";
                            }

                            tmpSN += "^XZ";


                            tmpSN += "^XA";
                            tmpSN += "^XFR:SN.ZPL";
                            tmpSN += "^PN0";

                            foreach (var SNRow2 in RadkySN)
                            {
                                tmpSN += "^FN" + SNRow2.Key.ToString();
                                tmpSN += "^FD";
                                tmpSN += SNRow2.Value.Trim();
                                tmpSN += "^FS";
                            }

                            tmpSN += "^XZ";

                            #endregion

                        }
                    }
                    else
                    {
                        tmpSN = string.Empty;
                        //SERLTNUMSeznam = "-";
                        //LLSN = (FontHeight + Properties.Settings.Default.Tisk_Font_Size_Medzera).ToString();
                        //FontSizeSN_HEIGHT = FontHeight.ToString();
                        //FontSizeSN_WIDTH = FontWidth.ToString();
                    }

                    #endregion


					Doprava = Pohoda.I_Tec.Database.Pohoda.GetDoprava(int.Parse(item.ITEMNMBR.Trim()));

					//var SKz = taSKz.GetDatabyID(int.Parse(item.ITEMNMBR.Trim()));

					//if ((SKz != null) && (SKz.Count > 0))
					//{
					//   var SKzRow = SKz.First();

					//    Doprava = SKzRow.IsDopravaNull() ? string.Empty : SKzRow.Doprava.Trim();
					//}

                    var sidt = taSI.GetDataByGUID(item.IDPol);

                    Fask.Module.Pohoda.I_Tec.SQL_Datasets.PohodaDataSet.POHODA_OdberatelDataTable odbdt = null;
                    string sopnumbe = string.Empty;
                    string vnddocnm = string.Empty;

                    if ((sidt != null) && (sidt.Count > 0))
                    {
                        sopnumbe = sidt[0].SOPNUMBE.Trim();
                        vnddocnm = sidt[0].VNDDOCNM.Trim();

                        odbdt = taODBER.GetDataOdberatel(sopnumbe);

                        if ((odbdt != null) && (odbdt.Count > 0))
                        {
                            var Row = odbdt[0];

                            string Firma = string.Empty;
                            string Utvar = string.Empty;
                            string Jmeno = string.Empty;
                            string Ulice = string.Empty;
                            string PSC = string.Empty;
                            string Obec = string.Empty;
                            string ICO = string.Empty;
                            string DIC = string.Empty;

                            if (Globals_V1.Konfigurace.Tisk[0].Tisk_Adresa_Jednotlivo)
                            {

                                try
                                {
                                    if (!Row.IsFirma2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.Firma2.Trim()))
                                            Firma = "-";
                                        else
                                            Firma = Row.Firma2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsFirmaNull())
                                            Firma = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.Firma.Trim()))
                                                Firma = "-";
                                            else
                                                Firma = Row.Firma.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Firma = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                                try
                                {
                                    if (!Row.IsUtvar2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.Utvar2.Trim()))
                                            Utvar = "-";
                                        else
                                            Utvar = Row.Utvar2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsUtvarNull())
                                            Utvar = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.Utvar.Trim()))
                                                Utvar = "-";
                                            else
                                                Utvar = Row.Utvar.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utvar = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                                try
                                {
                                    if (!Row.IsJmeno2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.Jmeno2.Trim()))
                                            Jmeno = "-";
                                        else
                                            Jmeno = Row.Jmeno2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsJmenoNull())
                                            Jmeno = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.Jmeno.Trim()))
                                                Jmeno = "-";
                                            else
                                                Jmeno = Row.Jmeno.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Jmeno = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                                try
                                {
                                    if (!Row.IsUlice2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.Ulice2.Trim()))
                                            Ulice = "-";
                                        else
                                            Ulice = Row.Ulice2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsUliceNull())
                                            Ulice = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.Ulice.Trim()))
                                                Ulice = "-";
                                            else
                                                Ulice = Row.Ulice.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Ulice = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                                try
                                {
                                    if (!Row.IsPSC2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.PSC2.Trim()))
                                            PSC = "-";
                                        else
                                            PSC = Row.PSC2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsPSCNull())
                                            PSC = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.PSC.Trim()))
                                                PSC = "-";
                                            else
                                                PSC = Row.PSC.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    PSC = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                                try
                                {
                                    if (!Row.IsObec2Null())
                                    {
                                        if (string.IsNullOrEmpty(Row.Obec2.Trim()))
                                            Obec = "-";
                                        else
                                            Obec = Row.Obec2.Trim();
                                    }
                                    else
                                    {
                                        if (Row.IsObecNull())
                                            Obec = "-";
                                        else
                                        {
                                            if (string.IsNullOrEmpty(Row.Obec.Trim()))
                                                Obec = "-";
                                            else
                                                Obec = Row.Obec.Trim();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Obec = "-";
									Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                }

                            }
                            else
                            {

                                if (
                                    !Row.IsFirma2Null() ||
                                    !Row.IsUtvar2Null() ||
                                    !Row.IsJmeno2Null() ||
                                    !Row.IsUlice2Null() ||
                                    !Row.IsPSC2Null() ||
                                    !Row.IsObec2Null()
                                    )
                                {

                                    Firma = Row.IsFirma2Null() ? "-" : Row.Firma2.Trim();
                                    Utvar = Row.IsUtvar2Null() ? "-" : Row.Utvar2.Trim();
                                    Jmeno = Row.IsJmeno2Null() ? "-" : Row.Jmeno2.Trim();
                                    Ulice = Row.IsUlice2Null() ? "-" : Row.Ulice2.Trim();
                                    PSC = Row.IsPSC2Null() ? "-" : Row.PSC2.Trim();
                                    Obec = Row.IsObec2Null() ? "-" : Row.Obec2.Trim();
                                }
                                else
                                {
                                    Firma = Row.IsFirmaNull() ? "-" : Row.Firma.Trim();
                                    Utvar = Row.IsUtvarNull() ? "-" : Row.Utvar.Trim();
                                    Jmeno = Row.IsJmenoNull() ? "-" : Row.Jmeno.Trim();
                                    Ulice = Row.IsUliceNull() ? "-" : Row.Ulice.Trim();
                                    PSC = Row.IsPSCNull() ? "-" : Row.PSC.Trim();
                                    Obec = Row.IsObecNull() ? "-" : Row.Obec.Trim();
                                }

                            }



                            try
                            {
                                if ((Row.IsICONull()) || (string.IsNullOrEmpty(Row.ICO.Trim())))
                                    ICO = "-";
                                else
                                    ICO = Row.ICO.Trim();
                            }
                            catch (Exception ex)
                            {
                                ICO = "-";
								Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }


                            try
                            {
                                if ((Row.IsDICNull()) || (string.IsNullOrEmpty(Row.DIC.Trim())))
                                    DIC = "-";
                                else
                                    DIC = Row.DIC.Trim();
                            }
                            catch (Exception ex)
                            {
                                DIC = "-";
								Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }




                            dtOUT.AddCZMST_Expedice_Baleni_Polozky_TISKRow(
                                item.ITEMNMBR,
                                item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC,
                                item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM,
                                item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod,
                                item.MJ,
                                item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL,
                                item.IsQTYNull() ? 0 : item.QTY,
                                SERLTNUMSeznam,
                                LLSN,
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height.ToString(),
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Width.ToString(),
                                Firma,
                                Utvar,
                                Jmeno,
                                Ulice,
                                PSC,
                                Obec,
                                ICO,
                                DIC,
                                sopnumbe,
                                vnddocnm,
                                PocetBalikuNaPalete,
                                Doprava,
                                tmpSN,
								VNDDOCNMList
                                );

                            //if (


                            //    (!odbdt[0].IsFirma2Null() && (odbdt[0].Firma != odbdt[0].Firma2)) &&
                            //    (!odbdt[0].IsObec2Null() && (odbdt[0].Obec != odbdt[0].Obec2)) &&
                            //    (!odbdt[0].IsUlice2Null() && (odbdt[0].Ulice != odbdt[0].Ulice2))
                            //    )
                            //{
                            //    dtOUT.AddCZMST_Expedice_Baleni_Polozky_TISKRow(
                            //        item.ITEMNMBR,
                            //        item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC,
                            //        item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM,
                            //        item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod,
                            //        item.MJ,
                            //        item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL,
                            //        item.IsQTYNull() ? 0 : item.QTY,
                            //        SERLTNUMSeznam,
                            //        LLSN,
                            //        FontSizeSN_HEIGHT,
                            //        FontSizeSN_WIDTH,
                            //        odbdt[0].IsFirma2Null() ? "-" : odbdt[0].Firma2,
                            //        odbdt[0].IsUtvar2Null() ? "-" : odbdt[0].Utvar2,
                            //        odbdt[0].IsJmeno2Null() ? "-" : odbdt[0].Jmeno2,
                            //        odbdt[0].IsUlice2Null() ? "-" : odbdt[0].Ulice2,
                            //        odbdt[0].IsPSC2Null() ? "-" : odbdt[0].PSC2,
                            //        odbdt[0].IsObec2Null() ? "-" : odbdt[0].Obec2,
                            //        odbdt[0].IsICONull() ? "-" : odbdt[0].ICO,
                            //        odbdt[0].IsDICNull() ? "-" : odbdt[0].DIC,
                            //        sopnumbe
                            //        );
                            //}
                            //else
                            //{
                            //dtOUT.AddCZMST_Expedice_Baleni_Polozky_TISKRow(
                            //item.ITEMNMBR,
                            //item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC,
                            //item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM,
                            //item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod,
                            //item.MJ,
                            //item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL,
                            //item.IsQTYNull() ? 0 : item.QTY,
                            //SERLTNUMSeznam,
                            //LLSN,
                            //FontSizeSN_HEIGHT,
                            //FontSizeSN_WIDTH,
                            //odbdt[0].IsFirmaNull() ? "-" : odbdt[0].Firma,
                            //odbdt[0].IsUtvarNull() ? "-" : odbdt[0].Utvar,
                            //odbdt[0].IsJmenoNull() ? "-" : odbdt[0].Jmeno,
                            //odbdt[0].IsUliceNull() ? "-" : odbdt[0].Ulice,
                            //odbdt[0].IsPSCNull() ? "-" : odbdt[0].PSC,
                            //odbdt[0].IsObecNull() ? "-" : odbdt[0].Obec,
                            //odbdt[0].IsICONull() ? "-" : odbdt[0].ICO,
                            //odbdt[0].IsDICNull() ? "-" : odbdt[0].DIC,
                            //sopnumbe
                            //);
                            //}
                        }

                        else
                        {

                            dtOUT.AddCZMST_Expedice_Baleni_Polozky_TISKRow(
                                item.ITEMNMBR,
                                item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC,
                                item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM,
                                item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod,
                                item.MJ,
                                item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL,
                                item.IsQTYNull() ? 0 : item.QTY,
                                SERLTNUMSeznam,
                                LLSN,
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height.ToString(),
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Width.ToString(),
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                sopnumbe,
                                vnddocnm,
                                PocetBalikuNaPalete,
                                Doprava,
                                tmpSN,
				VNDDOCNMList
                                );
                        }

                    }


                    else
                    {

                        dtOUT.AddCZMST_Expedice_Baleni_Polozky_TISKRow(
                            item.ITEMNMBR,
                            item.IsITEMDESCNull() ? string.Empty : item.ITEMDESC,
                            item.IsVNDITNUMNull() ? string.Empty : item.VNDITNUM,
                            item.IsCZ_CarKodNull() ? string.Empty : item.CZ_CarKod,
                            item.MJ,
                            item.IsNMBRPALNull() ? string.Empty : item.NMBRPAL,
                            item.IsQTYNull() ? 0 : item.QTY,
                            SERLTNUMSeznam,
                            LLSN,
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Height.ToString(),
                                Globals_V1.Konfigurace.Tisk[0].Tisk_Font_Size_Width.ToString(),
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            PocetBalikuNaPalete,
                            Doprava,
                            tmpSN,
							VNDDOCNMList
                            );
                    }

                }

                dtOUT.AcceptChanges();

            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtOUT;
		}

        public StatusInfo Expedice_GenerateDavka(Objednavka objednavka, Sklad sklad)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
