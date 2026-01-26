using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Ingres.Client;
using System.Text;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Lokace;

namespace Fask.Module.Ingres.SAD
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Lokace.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Lokace.ILokace
    {
        #region ILokace Members
        private string TABLE_CZMST_SKLADLOKACE_STAV = "CZMST_SkladLokace_Stav";
        private string TABLE_CZMST_SKLADLOKACE_STAVPOHYB = "CZMST_SkladLokace_StavPohyb";
        private string TABLE_CZMST_SKLADLOKACE_MAPA = "CZMST_SkladLokace_Mapa";
        private string TABLE_CZMST_SkladLokace_LokaceTypy = "CZMST_SkladLokace_LokaceTypy";
        private string TABLE_CZMST_SkladLokace_LokaceVariantySortiment = "CZMST_SkladLokace_LokaceVariantySortiment";

		/// <summary>
		/// Metoda pro pridani zaznamu do lokacniho mechanismu (vcetne pohybu).
		/// </summary>
		/// <param name="record">Trida reprezentujici zaznam=jeden radek tabulky.</param>
        public Fask.Server.Interfaces.Classes.StatusLokace Lokace_AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb record)
        {
            StatusLokace sl = new StatusLokace();
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresTransaction transaction = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new IngresCommand();
                adapter = new IngresDataAdapter();

                // Prijem
                if (record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    ProcessPrijem(record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();
                }
                // Vydej
                else if (record.POHYB_TYPE == TypeOfRecord.V)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    ProcessVydej(record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();
                }
                // Inventura
                else if (record.POHYB_TYPE == TypeOfRecord.I)
                {
                    throw new Exception("Inventura neni implementovana");
                }
                else 
                    throw new ApplicationException("typ pohybu '" + record.POHYB_TYPE + "' není implementován");

                sl.State = States.OK;
            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch { }
               
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return sl;
        }

		/// <summary>
		/// Metoda pro odstraneni zaznamu (vlozi se opacny pohyb/y).
		/// </summary>
		/// <param name="Guid">Guid zaznamu</param>
		/// <param name="ModulName">Modul o ktery se jedna (Prijem/Vydej)</param>
		/// <returns>Trida reprezentujici zaznam=jeden radek tabulky</returns>
        public Fask.Server.Interfaces.Classes.StatusLokace Lokace_DeleteRecord(Guid Guid, Fask.Server.Interfaces.Lokace.ModulName ModulName)
        {
            /*
             * Prijem - guid 1x = mazat - vlozit opacny pohyb.
             * Prijem - guid 2x = nemazat, uz je odecteno - opacny pohyb je vlozen.
             * Defregmentace guid 2x = mazat - vlozit opacny pohyb.
             * Defregmentace guid 4x = nemazat, uz je odecteno - opacny pohyb je vlozen.
             */
            StatusLokace sl = new StatusLokace();

            IngresCommand command = null;
            IngresConnection connection = null;
            IngresTransaction transaction = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                // otevreni spojeni a tvorba transakce
                connection.Open();
                transaction = connection.BeginTransaction();

                command = new IngresCommand();
                adapter = new IngresDataAdapter();
                command.Connection = connection;
                command.Transaction = transaction;

                // nacteni zaznamu z historie pohybu podle GUID
                string universalCommand = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " WHERE guid=@guid ORDER BY id DESC";

                command.CommandText = universalCommand;

                command.Parameters.AddWithValue("guid", Guid.ToString());

                Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();

                adapter.SelectCommand = command;
                adapter.Fill(data.CZMST_SkladLokace_StavPohyb);

                // kontrola, zdali byl zaznam nalezen
                if (data.CZMST_SkladLokace_StavPohyb.Rows.Count > 0)
                {
                    // pokud odpovidaji podmince na volajici modul a pocet guidu - tudiz uz nebyly smazany
                    if (((ModulName == ModulName.PRIJEM || ModulName == ModulName.VYDEJ) && data.CZMST_SkladLokace_StavPohyb.Rows.Count % 2 != 0) || (ModulName == ModulName.DEFREGMENTACE && data.CZMST_SkladLokace_StavPohyb.Rows.Count % 4 != 0))
                    {
                        // vlozeni opacnych zaznamu k soucasnym zaznamum (mazani).
                        foreach (Fask.Server.Interfaces.DataSets.Location.CZMST_SkladLokace_StavPohybRow dr in data.CZMST_SkladLokace_StavPohyb)
                        {
                            //Vytvoreni noveho zaznamu, ktery je nutný pro příjem a výdej.
                            Fask.Server.Interfaces.Lokace.LokacePohyb record = new LokacePohyb();
                            DateTime dtnow = DateTime.Now;
                            // dex_row_id neresim
                            record.ITEMNMBR = dr.ITEMNMBR;
                            record.DOCUMENT_NUMBER = dr.IsDOCUMENT_NUMBERNull() ? null : dr.DOCUMENT_NUMBER;
                            record.POHYB_SRC = dr.IsPOHYB_SRCNull() ? null : dr.POHYB_SRC;                            
                            record.QTYSHPPD = dr.QTYSHPPD;
                            record.SERLTNUM = dr.SERLTNUM;
                            record.SOURCE = dr.IsSOURCENull() ? null : dr.SOURCE;
                            record.CountEntries = dr.IsCountEntriesNull() ? (int?)null : dr.CountEntries;
                            record.SKL_ID_SRC = dr.IsSKL_ID_SRCNull() ? null : dr.SKL_ID_SRC;
                            record.SKL_ID_DST = dr.IsSKL_ID_DSTNull() ? null : dr.SKL_ID_DST;
                            record.LOCNCODE_SRC = dr.IsLOCNCODE_SRCNull() ? null : dr.LOCNCODE_SRC;
                            record.LOCNCODE_DST = dr.IsLOCNCODE_DSTNull() ? null : dr.LOCNCODE_DST;
                            record.UserID = dr.UserID;
                            record.TermID = dr.TermID;
                            record.guid = Guid;
                            record.dateeveS = dtnow;    // datum serveru
                            record.dateeveT = dtnow;    // datum terminalu stejne jako datum serveru (neznam ho ...) -> poslat si ho?
                            record.CountEntries = dr.IsCountEntriesNull() ? (int?)null : dr.CountEntries;
                            // typ neresit, podle nej se odlisuje o jaky pohyb se jedna

                            //Kontrola typu zaznamu - Prijem
                            if (dr.POHYB_TYPE == TypeOfRecord.P.ToString())
                            {
                                //Volani funkce pro opacny typ zaznamu - vydej.
                                record.POHYB_TYPE = TypeOfRecord.V;
                                ProcessVydej(record, command, connection, transaction, adapter);
                            }
                            else if (dr.POHYB_TYPE == TypeOfRecord.V.ToString())
                            {
                                //Je treba prijmat kladnou hodnotu - vydej je vzdy zapsan se zapornou hodnotou mnozstvi
                                record.QTYSHPPD *= -1;
                                //Volani funkce pro opacny typ zaznamu - prijem.
                                record.POHYB_TYPE = TypeOfRecord.P;
                                ProcessPrijem(record, command, connection, transaction, adapter);
                            }
                            else throw new ApplicationException("Neznámý typ pohybu " + dr.POHYB_TYPE);
                        }
                    }
                }

                //Potvrzeni transakce
                if (transaction != null)
                    transaction.Commit();

                sl.State = States.OK;
            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch { }                
                //Poslani vyjimky vyse.
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return sl;
        }

		/// <summary>
		/// Defregmentace (presun ze zdrojove lokace/skladu na cilovou lokaci/sklad)
		/// </summary>
		/// <param name="Record">Zaznam, ktereho se defregmentace tyka.</param>
        public Fask.Server.Interfaces.Classes.StatusLokace Lokace_MoveItem(Fask.Server.Interfaces.Lokace.LokacePohyb Record)
        {
            StatusLokace sl = new StatusLokace();            

            IngresCommand command = null;
            IngresConnection connection = null;
            IngresTransaction transaction = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                adapter = new IngresDataAdapter();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new IngresCommand();

                if (Record.POHYB_TYPE == TypeOfRecord.D)// && Source)
                {
                    /* 1) vydani material z puvodni lokace
                     * 2) prijem material na novou lokaci
                     */

                    string old_locncode_src = Record.LOCNCODE_SRC;
                    string old_locncode_dst = Record.LOCNCODE_DST;
                    string old_skl_id_src = Record.SKL_ID_SRC;
                    string old_skl_id_dst = Record.SKL_ID_DST;

                    Record.POHYB_TYPE = TypeOfRecord.V;

                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // vydej
                    ProcessVydej(Record, command, connection, transaction, adapter);

                    // obraceni skladu, lokaci a zmena na prijem
                    Record.LOCNCODE_SRC = old_locncode_dst;
                    Record.LOCNCODE_DST = old_locncode_src;
                    Record.SKL_ID_SRC = old_skl_id_dst;
                    Record.SKL_ID_DST = old_skl_id_src;
                    Record.POHYB_TYPE = TypeOfRecord.P;
                    
                    // prijem
                    ProcessPrijem(Record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();

                    sl.State = States.OK;
                }
                else if (Record.POHYB_TYPE == TypeOfRecord.V)  // vydej
                {
                    /*
                     * 1) samotny vydej ze zdrojove lokace
                     */

                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // vydej
                    ProcessVydej(Record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();

                    sl.State = States.OK;
                }
                else if (Record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) // prijem
                {
                    /*
                     * 1) samotny prijem ze zdrojove lokace
                     */

                    connection.Open();
                    transaction = connection.BeginTransaction();
                    ProcessPrijem(Record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();

                    sl.State = States.OK;
                }
                else throw new Exception("MoveItem - Pohyb není implementován : " + Record.POHYB_TYPE.ToString());
            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch { }

                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return sl;
        }

        /// <summary>
        /// Zobrazi mnozstvi materialu na jednotlivych lokaci
        /// </summary>
        /// <param name="itemnmbr">ID materialu.</param>
        /// <returns>Odpovidajici zaznamy</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();

                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                command = new IngresCommand();
                adapter = new IngresDataAdapter();

                // select, kde mnozstvi neni 0
                command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and QTYSHPPD<>0";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                command.Connection = connection;

                // naplneni datasetu
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Stav);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Zobrazi mnozstvi materialu na jednotlivych lokaci
        /// </summary>
        /// <param name="itemnmbr">ID materialu.</param>
        /// <param name="serltnum">Sarze.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="NumberOfRecords">Pocet zaznamu.</param>
        /// <returns>Odpovidajici zaznamy</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr, string serltnum, string skl_id, uint? NumberOfRecords, bool ShowEmpty)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                command = new IngresCommand();
                adapter = new IngresDataAdapter();
                connection.Open();

                // select, kde mnozstvi neni 0
                //command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and SERLTNUM=@serltnum SKL_ID=@skl_id and QTYSHPPD<>0";
                command.CommandText = "SELECT " + (NumberOfRecords.HasValue ? ("TOP " + NumberOfRecords.Value) : string.Empty) + "* FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE 1=1 " +
                    (string.IsNullOrEmpty(itemnmbr) ? string.Empty : "and ITEMNMBR=@itemnmbr ") +
                    (string.IsNullOrEmpty(serltnum) ? string.Empty : " and SERLTNUM=@serltnum ") +
                    (string.IsNullOrEmpty(skl_id) ? string.Empty : " and SKL_ID=@skl_id ") +
                    (ShowEmpty ? string.Empty : "and QTYSHPPD<>0 ") +
                    "order by LOCNCODE asc ";

                if (!string.IsNullOrEmpty(itemnmbr))
                    command.Parameters.AddWithValue("@itemnmbr", itemnmbr.Trim());

                if (!string.IsNullOrEmpty(serltnum))
                    command.Parameters.AddWithValue("@serltnum", serltnum.Trim());

                if (!string.IsNullOrEmpty(skl_id))
                    command.Parameters.AddWithValue("@skl_id", skl_id.Trim());

                command.Connection = connection;

                // naplneni datasetu
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Stav);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Zobrazi material serazeny podle expirace od nejstarsiho (tabulka get_os_ms).
		/// </summary>
		/// <param name="itemnmbr">Oznaceni materialu.</param>
		/// <param name="skl_id">ID skladu.</param>
		/// <returns>Odpovidajici zaznamy.</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowOldestMaterial(string itemnmbr, string skl_id)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();

                string strcommand = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE SKL_ID=@skl_id and ITEMNMBR=@itemnmbr AND QTYSHPPD<>0 ORDER BY EXPIRATION ASC";
                command = new IngresCommand(strcommand, connection);

                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);                

                //Naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new IngresDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Stav);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Zobrazi material serazeny podle expirace od nejstarsiho (tabulka get_os_ms).
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="NumberOfRecords">Pocet zaznamu.</param>
        /// <returns>Odpovidajici zaznamy.</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowOldestMaterial(string itemnmbr, string skl_id, uint NumberOfRecords)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();

                string strcommand = "SELECT TOP " + NumberOfRecords + " * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE SKL_ID=@skl_id and ITEMNMBR=@itemnmbr AND QTYSHPPD<>0 ORDER BY EXPIRATION ASC";
                command = new IngresCommand(strcommand, connection);

                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);                

                // naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new IngresDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Stav);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Funkce pro zjisteni zda je material na dane lokaci - pro defregmentaci.
		/// </summary>
		/// <param name="itemnmbr">Oznaceni materialu</param>
		/// <param name="locncode">ID Lokace.</param>
		/// <param name="skl_id">ID skladu.</param>
		/// <returns>Priznak existence.</returns>
        public bool Lokace_CheckMaterialInLocation(string itemnmbr, string locncode, string skl_id)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            
            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                //Kontrola jestli je na regalu umisteno vice nez 0 materialu 
                command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE ITEMNMBR=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                command.Parameters.AddWithValue("@locncode", locncode);
                command.Parameters.AddWithValue("@skl_id", skl_id);

                object rowcount = command.ExecuteScalar();

                //Kotrola existence a navrat.
                if ((rowcount != null) && (((int)rowcount) > 0)) 
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Funkce pro online kontrolu lokace.
		/// </summary>
		/// <param name="Regal">Oznaceni regalu</param>
		/// <returns>Priznak existence</returns>
        public StatusOverLokace Lokace_OverLokace(string skl_id, string locncode)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE LOCNCODE=@locncode AND SKL_ID=@skl_id";

                command.Parameters.AddWithValue("@locncode", locncode);
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                object rowcount = command.ExecuteScalar();

                // pokud lokace existuje, vraci OK, jinak false                
                if ((rowcount != null) && (((int)rowcount) > 0))
                    status.State = STATUSOverLokace.OK;
                else
                    status.State = STATUSOverLokace.ERROR;

                if (status.State == STATUSOverLokace.ERROR)
                    status.Message = "Lokace '" + locncode.Trim() + "' neexistuje ve skladu '" + (string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim()) + "'!";

                return status;
                //return rowcount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Funkce pro navrat prijmovych lokaci podle ID skladu
		/// </summary>
		/// <param name="skl_id">ID skladu</param>
		/// <returns>Prijmove lokace</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowReceiveLocations(string skl_id)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            IngresDataAdapter adapter = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();

                //string strcommand = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE SKL_ID=@skl_id AND IS_RECEIVE > 0";
                string strcommand = 
                    "SELECT mapa.* FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " mapa " + 
                    "JOIN " + TABLE_CZMST_SkladLokace_LokaceTypy + " typy on typy.TYPE=mapa.TYPE " + 
                    "WHERE mapa.SKL_ID=@skl_id and typy.IS_RECEIVE > 0";
                command = new IngresCommand(strcommand, connection);
                
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                // naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new IngresDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Mapa);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Vydej materialu.
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se vydej tyka.</param>
        /// <param name="Command">Prikaz.</param>
        /// <param name="Connection">Pripojeni.</param>
        /// <param name="Transaction">Transakce.</param>
        /// <param name="Adapter">Adapter.</param>
        private void ProcessVydej(LokacePohyb record, IngresCommand Command, IngresConnection Connection, IngresTransaction Transaction, IngresDataAdapter Adapter)
        {

            Globals.LoadConfiguration();

            if (record == null || Adapter == null || Command == null || Connection == null || Transaction == null)
                throw new ApplicationException("Některý z parametrů funkce ProcessVydej není inicializovaný.");

            // kontrola na existenci lokace
            StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, Command, Connection, Transaction);
            if(status.State == STATUSOverLokace.ERROR)
                throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

            Command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
            Command.Connection = Connection;
            Command.Transaction = Transaction;

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
            Command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
            Command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
            Command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

            Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
            Adapter.SelectCommand = Command;
            Adapter.Fill(data.CZMST_SkladLokace_Stav);

            // mnozstvi
            decimal oldQuantity = decimal.Zero, newQuantity = decimal.Zero;

            // nacteni mnozstvi z nalezeneho zaznamu
            if (data.CZMST_SkladLokace_Stav.Rows.Count > 0)
            {
                //Ziskani hodnoty mnozstvi.
                oldQuantity = data.CZMST_SkladLokace_Stav.First().QTYSHPPD;
                record.Expiration = data.CZMST_SkladLokace_Stav.First().IsEXPIRATIONNull() ? null : (DateTime?)data.CZMST_SkladLokace_Stav.First().EXPIRATION;
            }
            // nenalezen zadny material na lokaci
            else if (data.CZMST_SkladLokace_Stav.Rows.Count == 0)
            {
                throw new Exception("Materiál '" + record.ITEMNMBR.Trim() + "' nebyl nalezen na lokaci '" + record.LOCNCODE_SRC.Trim() + "'" + (string.IsNullOrEmpty(record.SKL_ID_SRC.Trim()) ? string.Empty : " ve skladu '" + record.SKL_ID_SRC.Trim() + "'"));
            }

            // nastaveni nove hodnoty mnozstvi (+kontrola preteceni).
            checked { newQuantity = oldQuantity - record.QTYSHPPD; }

            // kontrola noveho mnozstvi - jestli je < 0 (nejde vydavat do minusu).
            if (newQuantity < 0)
            {
                //18.4.2018 TaD uprava, jde vydat do minusu...
                //throw new Exception("'" + record.ITEMNMBR.Trim() + "' není možné vydávat do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);
                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn,"'" + record.ITEMNMBR.Trim() + "' vydán do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);

                //4.11.2020 Dle Zadano od JaS přidano konfiguračně přepnutí
                if (Globals.Konfigurace.LokMech[0].Vydej_PovolZapornyStav)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn,"'" + record.ITEMNMBR.Trim() + "' vydán do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);
                }
                else
                {
                    throw new Exception("'" + record.ITEMNMBR.Trim() + "' není možné vydávat do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);
                }

            }

            // aktualizace mnozstvi
            UpdateQuantity(newQuantity, record.ITEMNMBR, record.SERLTNUM, record.LOCNCODE_SRC, record.SKL_ID_SRC, Command, Connection, Transaction);

            // zjisteni dosavadniho maximalniho id z tabulky historie pohybu
            int max_id = CheckMaxID(TABLE_CZMST_SKLADLOKACE_STAVPOHYB, Command, Connection, Transaction);

            // vlozeni zaznamu do historie pohybu
            AddMovement(TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, Command, Connection, Transaction);
        }

        /// <summary>
        /// Uprava hodnoty mnozstvi materialu na lokaci.
        /// </summary>
        /// <param name="Quantity">Nova hodnota mnozstvi.</param>
        /// <param name="IdMat">ID materialu.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="Command">Prikaz.</param>
        /// <param name="Connection">Pripojeni.</param>
        /// <param name="Transaction">Transakce.</param>
        private void UpdateQuantity(decimal QTYSHPPD, string ITEMNMBR, string serltnum, string LOCNCODE, string skl_id, IngresCommand command, IngresConnection connection, IngresTransaction transaction)
        {
            //Prikaz pro zmenu hodnoty mnozstvi materialu ve cteckove pozici.
            command.CommandText = "UPDATE " + TABLE_CZMST_SKLADLOKACE_STAV + " SET QTYSHPPD=@qtyshppd, DATECHANGE=@datechange WHERE ITEMNMBR=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id AND SERLTNUM=@serltnum";
            command.Connection = connection;
            command.Transaction = transaction;

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@qtyshppd", QTYSHPPD);
            command.Parameters.AddWithValue("@itemnmbr", ITEMNMBR);
            command.Parameters.AddWithValue("@locncode", LOCNCODE);
            command.Parameters.AddWithValue("@skl_id", skl_id);
            command.Parameters.AddWithValue("@datechange", DateTime.Now);
            command.Parameters.AddWithValue("@serltnum", serltnum);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Pridani zaznamu do tabulky historie pohybu.
        /// </summary>
        /// <param name="Table">Nazev tabulky</param>
        /// <param name="Record">Data pro zaznam.</param>
        /// <param name="ID">Id zaznamu.</param>
        /// <param name="Command">Prikaz</param>
        private void AddMovement(string tablename, LokacePohyb record, int ID, IngresCommand command, IngresConnection connection, IngresTransaction transaction)
        {
            //Kontrola, jetsli jsou promenne alokovany.
            if (string.IsNullOrEmpty(tablename) || record == null || command == null)
                throw new ApplicationException("Některý z parametrů funkce AddMovement není inicializovaný.");

            command.Connection = connection;
            command.Transaction = transaction;

			#region Zalogovani pohybu

			string msg = string.Empty;

			msg += "ID,timeS,zdroj,CountEntries: " + record.id + "," + record.dateeveS + "," + record.POHYB_SRC + "," + record.CountEntries + Environment.NewLine;
			msg += "guid : " + record.guid.ToString() + Environment.NewLine;
			msg += "terminal,user: " + record.TermID + "," + record.UserID + Environment.NewLine;
			//Typ zaznamu muze byt ruzny od modulu, napr modul D=defragmentace, typy P=prijem a V=vydej
			msg += "type of record(saved to database): " + record.POHYB_TYPE.ToString() + Environment.NewLine;
			msg += "location src,skl_id_src,location dst,skl_id_dst,material,serltnum,quantity: " + record.LOCNCODE_SRC.Trim() + "," + record.SKL_ID_SRC.Trim() + "," + record.LOCNCODE_DST.Trim() + "," + record.SKL_ID_DST.Trim() + "," + record.ITEMNMBR.Trim() + "," + record.SERLTNUM.Trim() + "," + record.QTYSHPPD + Environment.NewLine;

			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Location, msg);

			#endregion

            //Pred zapsanim do db zapis do logu
            record.id = ID;

            //Inicializace prikazu.
            command.CommandText = "INSERT INTO " + tablename + 
                "(id, ITEMNMBR, DOCUMENT_NUMBER, POHYB_TYPE, POHYB_SRC, SOURCE, CountEntries, QTYSHPPD, SERLTNUM, SKL_ID_SRC, SKL_ID_DST, LOCNCODE_SRC, LOCNCODE_DST, UserID, TermID, Guid, dateeveS, dateeveT) " + 
                "VALUES " + 
                "(@id, @itemnmbr, @document_number, @pohyb_type, @pohyb_src, @source, @countentries, @qtyshppd, @serltnum, @skl_id_src, @skl_id_dst, @locncode_src, @locncode_dst, @userid, @termid, @guid, @dateeves, @dateevet)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", ID);
            command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
            command.Parameters.AddWithValue("@document_number", record.DOCUMENT_NUMBER);
            command.Parameters.AddWithValue("@pohyb_type", record.POHYB_TYPE.ToString());
            command.Parameters.AddWithValue("@pohyb_src", record.POHYB_SRC);
            command.Parameters.AddWithValue("@source", record.SOURCE);
            command.Parameters.AddWithValue("@countentries", record.CountEntries);
            
            // zmena v pripade vydeje na zapornou hodnotu mnozstvi.
            if (record.POHYB_TYPE == TypeOfRecord.V)
                command.Parameters.AddWithValue("@qtyshppd", -record.QTYSHPPD);
            else
                command.Parameters.AddWithValue("@qtyshppd", record.QTYSHPPD);

            command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
            command.Parameters.AddWithValue("@skl_id_src", record.SKL_ID_SRC);
            command.Parameters.AddWithValue("@skl_id_dst", record.SKL_ID_DST);
            command.Parameters.AddWithValue("@locncode_src", record.LOCNCODE_SRC);
            command.Parameters.AddWithValue("@locncode_dst", record.LOCNCODE_DST);
            command.Parameters.AddWithValue("@userid", record.UserID);
            command.Parameters.AddWithValue("@termid", record.TermID);
            command.Parameters.AddWithValue("@guid", record.guid.ToString());
            command.Parameters.AddWithValue("@dateeves", DateTime.Now);    // datum serveru record.dateeveS));
            command.Parameters.AddWithValue("@dateevet", record.dateeveT);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Zjisti maximalni index v tabulce a vrati o cislo vetsi. Potreba kvuli jedinecne identifikaci pri logovani.
        /// </summary>
        /// <param name="Table">Nazev tabulky.</param>
        /// <param name="Command">Prikaz.</param>
        /// <param name="Connection">Pripojeni.</param>
        /// <param name="Transaction">Transakce.</param>
        /// <returns>Maximalni index + 1.</returns>
        private int CheckMaxID(string tablename, IngresCommand command, IngresConnection connection, IngresTransaction transaction)
        {
            //Kontrola, jetsli je promenna alokovana.
            if (string.IsNullOrEmpty(tablename) || command == null || connection == null || transaction == null)
                throw new ApplicationException("Některý z parametrů funkce CheckMaxID není inicializovaný.");

            //Inicializace casti prikazu.
            command.CommandText = "Select max(id) from " + tablename;
            command.Connection = connection;
            command.Transaction = transaction;

            command.Parameters.Clear();

            //Provedeni
            object o = command.ExecuteScalar();

            //Kontrola na null v pripade ze v tabulce nebyl zadny zaznam, jinak dosavadni +1.
            if (o is System.DBNull) 
                return 1;

            return Convert.ToInt32(o) + 1;
        }

		/// <summary>
		/// Funkce pro online kontrolu lokace.
		/// </summary>
		/// <param name="itemnmbr">Oznaceni materialu.</param>
		/// <param name="serltnum">Sarze.</param>
		/// <param name="locncode">ID lokace.</param>
		/// <param name="skl_id">ID skladu.</param>
		/// <param name="qtyshppd">Mnozstvi.</param>
		/// <param name="doc_id">Typ dokladu.</param>
		/// <param name="locationType">Typ lokace (zdrojova, cilova)</param>
		/// <param name="recordType">Typ zaznamu (urcuje se z czmst092 - cfg_lok_mech_pohyb_type)</param>
		/// <returns>StatusOverLokace - nese informace o stavu</returns>
        public StatusOverLokace Lokace_OverLokace(string itemnmbr, string serltnum, string locncode, string skl_id, decimal qtyshppd, string doc_id, TYPLokace locationType, Fask.Server.Interfaces.Lokace.TypeOfRecord recordType)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                if (locationType == TYPLokace.SOURCE && !(recordType == TypeOfRecord.P))
                {
                    // overovani zdrojove lokace ... kontrola, zdali je dany material a mnozstvi na dane lokaci a skladu
                    // pokud se jedna o prijem, nedochazi k overovani, protoze staci existence lokace ...
                    command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND QTYSHPPD>=@qtyshppd AND LOCNCODE=@locncode AND SKL_ID=@skl_id";

                    command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                    command.Parameters.AddWithValue("@serltnum", serltnum);
                    command.Parameters.AddWithValue("@qtyshppd", qtyshppd);
                    command.Parameters.AddWithValue("@locncode", locncode);
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                    object rowcount = command.ExecuteScalar();

                    // pokud lokace existuje, vraci OK, jinak false
                    if ((rowcount != null) && (((int)rowcount) > 0))
                        status.State = STATUSOverLokace.OK;
                    else
                        status.State = STATUSOverLokace.ERROR;

                    if (status.State == STATUSOverLokace.ERROR)
                        status.Message = "Materiál '" + itemnmbr.Trim() + "' s množstvím '" + qtyshppd.ToString() + "' nebyl nalezen na lokaci '" + locncode.Trim() + "' ve skladu '" + (string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim()) + "'!";                    
                }
                else //if(locationType == TYPLokace.DEST) // na existenci se overuje i prijmova lokace pri prijmu
                {
                    // overovani cilove lokace ... pouze kontrola existence lokace
                    //Kontrola, jestli je lokace a sklad v DB
                    command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE LOCNCODE=@locncode AND SKL_ID=@skl_id";

                    command.Parameters.AddWithValue("@locncode", locncode);
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                    object rowcount = command.ExecuteScalar();

                    // pokud lokace existuje, vraci OK, jinak false
                    if ((rowcount != null) && ((int)rowcount > 0))
                        status.State = STATUSOverLokace.OK;
                    else
                        status.State = STATUSOverLokace.ERROR;

                    if (status.State == STATUSOverLokace.ERROR)
                        status.Message = "Lokace '" + locncode.Trim() + "' neexistuje ve skladu '" + (string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim()) + "'!";
                }

                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Zjisteni, zdali varianta lokace existuje.
		/// </summary>
		/// <param name="itemnmbr">Polozka ID</param>
		/// <param name="skl_id">Sklad ID</param>
		/// <param name="locncode">Lokace</param>
		/// <returns>True-OK, False-Chyba</returns>
        public bool Lokace_VariantySortimentExists(string itemnmbr, string skl_id, string locncode)
        {
            //TABLE_CZMST_SkladLokace_LokaceVariantySortiment
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " WHERE itemnmbr=@itemnmbr LOCNCODE=@locncode AND SKL_ID=@skl_id";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                command.Parameters.AddWithValue("@locncode", locncode);
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                object rowcount = command.ExecuteScalar();

                // pokud lokace existuje, vraci OK, jinak false
                if ((rowcount != null) && ((int)rowcount > 0))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Zjisteni, zdali varianta lokace existuje.
		/// </summary>
		/// <param name="itemnmbr">Polozka ID</param>
		/// <param name="skl_id">Sklad ID</param>
		/// <param name="locncode">Lokace</param>
		/// <returns>True-OK, False-Chyba</returns>
        public bool Lokace_VariantySortimentExistsDefault(string itemnmbr, string skl_id, string locncode)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText =
                    "SELECT lvs.*, lt.* FROM " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " lvs " +
                    " LEFT JOIN " + TABLE_CZMST_SkladLokace_LokaceTypy + " lt on lt.TYPE=lvs.TYPE " +
                    " WHERE lvs.ITEMNMBR=@itemnmbr AND lvs.SKL_ID=@skl_id AND lt.IS_DEFAULT=1";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);
                System.Data.DataSet lokaceDS = new System.Data.DataSet();
                IngresDataAdapter sda = new IngresDataAdapter(command);
                sda.Fill(lokaceDS);

                System.Data.DataTable lokaceDT = null;
                if (lokaceDS.Tables.Count > 0)
                {
                    lokaceDT = lokaceDS.Tables[0];
                }
                else
                {
                    return false; // neexistuje vychozi pozice...
                }

                // existuje ve vysledku pro polozku na sklade vychozi pozice (alespon jedna, respektive prave jedna ...)
                if (lokaceDS.Tables[0].Select().Count() > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

		/// <summary>
		/// Nastaveni varianty lokace.
		/// </summary>
		/// <param name="itemnmbr">Polozka ID</param>
		/// <param name="skl_id">Sklad ID</param>
		/// <param name="locncode">Lokace</param>
		/// <param name="type">Typ</param>
		/// <returns>True-OK, False-Chyba</returns>
        public bool Lokace_VariantySortimentNastav(byte idterminal, int userid, string itemnmbr, string skl_id, string locncode, string type)
        {
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();
            IngresTransaction sqltrans = null;

            try
            {
                Globals.LoadConfiguration();
                Fask.Server.Interfaces.DataSets.Location locationDS = new Fask.Server.Interfaces.DataSets.Location();
                Fask.Server.Interfaces.DataSets.Location.CZMST_SkladLokace_LokaceTypyRow locationRow = null;

                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                // Transakce 
                sqltrans = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = sqltrans;

                // 1) Kontrola zda lokace na sklade v mape je zavedena !!!
                command.CommandText =
                    "Select Count(*) FROM " + TABLE_CZMST_SKLADLOKACE_MAPA +
                    " WHERE SKL_ID=@skl_id AND LOCNCODE=@locncode";
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim());
                command.Parameters.AddWithValue("@locncode", locncode);
                object cnt = command.ExecuteScalar();
                //if ((cnt != null) && ((int)cnt > 0))
                //{ // ok existuje, tak pokracuju ...
                //}
                //else
                //{
                //    throw new Exception(string.Format("Lokace {0} na sklade {1} neexistuje!", locncode, skl_id));
                //}
                if (Convert.ToInt32(cnt) <= 0)
                { // neexistuje, tak vyjimka ...
                    throw new Exception(string.Format("Lokace {0} na sklade {1} neexistuje!", locncode.Trim(), skl_id.Trim()));
                }

                // 2) Kontrola, zda existuje vubec takovy typ a jake ma vlastnosti ...
                command.Parameters.Clear();
                command.CommandText =
                    "Select * from " + TABLE_CZMST_SkladLokace_LokaceTypy +
                    " WHERE TYPE=@type";
                command.Parameters.AddWithValue("@type", type);
                IngresDataAdapter sda = new IngresDataAdapter(command);
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceTypy);

                // pokud typ neni, tak chyba ... 
                if (locationDS.CZMST_SkladLokace_LokaceTypy.Count <= 0)
                {
                    throw new Exception(string.Format("Lokace typu {0} neexistuje!", type));
                }
                else
                {
                    locationRow = locationDS.CZMST_SkladLokace_LokaceTypy[0];
                }

                // 3) Vytahnout varianty pro polozku, sklad a typ => seznam lokaci(pocet)
                command.Parameters.Clear();
                command.CommandText =
                    "Select * FROM " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
                    " WHERE ITEMNMBR=@itemnmbr AND SKL_ID=@skl_id AND TYPE=@type" +
                    //" AND LOCNCODE=@locncode" + 
                    "";
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim());
                command.Parameters.AddWithValue("@type", type);
                //command.Parameters.AddWithValue("@locncode", locncode);

                sda.Fill(locationDS.CZMST_SkladLokace_LokaceVariantySortiment);

                //object cnt = (int)command.ExecuteScalar();
                //if ((cnt != null) && ((int)cnt > 0))
                // Pocet lokaci pro datny typ ...
                if (locationDS.CZMST_SkladLokace_LokaceVariantySortiment.Count > 0)
                { // jiz existuje
                    // a) je defaultni 
                    //  => pouze jedna, takze smazu vsechny vyskyty, pokud jsou pro item,sklad, type, pak vlozim
                    // b) je prijmova 
                    //  => take by mela byt jedna tedy take smazat a vlozit
                    // c) je jina
                    //  i) locncode jiz existuje => smazat a znovu vlozit (pouze konkretni locncode
                    // ii) locncode neexistuje => vlozit

                    if (locationRow.IS_DEFAULT || locationRow.IS_RECEIVE)
                    { // smazat vsechny vyskyty pro vychozi a prijmovou ...???
                        command.Parameters.Clear();
                        command.CommandText =
                            "DELETE " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
                            " WHERE ITEMNMBR=@itemnmbr AND SKL_ID=@skl_id AND TYPE=@type" +
                            //" AND LOCNCODE=@locncode" + 
                            "";
                        command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                        command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim());
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@locncode", locncode);

                        command.ExecuteNonQuery();
                    }
                    else if (locationDS.CZMST_SkladLokace_LokaceVariantySortiment.Where(
                        x => x.LOCNCODE.Trim().Equals(locncode, StringComparison.OrdinalIgnoreCase)
                            ).Count() > 0)
                    { // existuje tam zaznam s locncode ...
                        // tak ho smazu(jen ten s aktualnim locncode - ostatni ne) a pak vlozim novy zaznam s aktualnim locncode...
                        command.Parameters.Clear();
                        command.CommandText =
                            "DELETE " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
                            " WHERE ITEMNMBR=@itemnmbr AND SKL_ID=@skl_id AND TYPE=@type" +
                            " AND LOCNCODE=@locncode" +
                            "";
                        command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                        command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim());
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@locncode", locncode);

                        command.ExecuteNonQuery();
                    }

                }


                { // neexistuje, tak vlozit
                    command.Parameters.Clear();
                    command.CommandText =
                        "insert into " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
                        "(ITEMNMBR, SKL_ID, LOCNCODE, TYPE, UserID, TermID) " +
                        "VALUES " +
                        "(@itemnmbr, @skl_id, @locncode, @type, @userid, @termid)";

                    command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id.Trim());
                    command.Parameters.AddWithValue("@locncode", locncode);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@termid", idterminal);

                    command.ExecuteNonQuery();
                }

                // Pokud vse ok, tak commit transakce ...
                if (sqltrans != null)
                    sqltrans.Commit();

                return true;
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch (Exception extrans)
                {
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, extrans);
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
		/// Ziskani informaci varianty lokaci pro polozku skladu
		/// </summary>
		/// <param name="itemnmbr">Polozka ID</param>
		/// <param name="skl_id">Sklad ID</param>
		/// <returns>Location- dotažene lokace</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_VariantySortimentGet(string itemnmbr, string skl_id)
        {
            //TABLE_CZMST_SkladLokace_LokaceVariantySortiment
            IngresCommand command = null;
            IngresConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            Fask.Server.Interfaces.DataSets.Location locationDS = new Fask.Server.Interfaces.DataSets.Location();

            try
            {
                Globals.LoadConfiguration();
                command = new IngresCommand();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT * FROM " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " WHERE itemnmbr=@itemnmbr";
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                if (!String.IsNullOrEmpty(skl_id))
                {
                    command.CommandText += " AND SKL_ID=@skl_id";
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);
                }

                IngresDataAdapter sda = new IngresDataAdapter(command);
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceVariantySortiment);

                command.Parameters.Clear();
                command.CommandText = "Select * FROM " + TABLE_CZMST_SkladLokace_LokaceTypy;
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceTypy);

                return locationDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }



        #endregion

		#region Privatne metody

		private StatusOverLokace OverLokace(string skl_id, string locncode, IngresCommand command, IngresConnection connection, IngresTransaction transaction)
		{
			if (command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce OverLokace není inicializovaný.");

			StatusOverLokace status = new StatusOverLokace();

			command.Connection = connection;
			command.Transaction = transaction;
			command.CommandText = "SELECT COUNT(*) FROM " + TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE skl_id=@skl_id and locncode=@locncode";

			command.Parameters.Clear();
			command.Parameters.AddWithValue("@skl_id", skl_id);
			command.Parameters.AddWithValue("@locncode", locncode);

			//Provedeni
			object rowcount = command.ExecuteScalar();

			// pokud lokace existuje, vraci OK, jinak false
			if ((rowcount != null) && (((int)rowcount) > 0))
				status.State = STATUSOverLokace.OK;
			else
				status.State = STATUSOverLokace.ERROR;

			return status;
			//return rowcount > 0 ? true : false;
		}

		private void ProcessPrijem(LokacePohyb record, IngresCommand command, IngresConnection connection, IngresTransaction transaction, IngresDataAdapter adapter)
		{
			if (record == null || adapter == null || command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce ProcessPrijem není inicializovaný.");

			// kontrola na existenci lokace
			StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, command, connection, transaction);
			if (status.State == STATUSOverLokace.ERROR)
				throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

			// kontrola aktualniho mnozstvi na lokaci
			command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
			command.Connection = connection;
			command.Transaction = transaction;

			command.Parameters.Clear();
			command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
			command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
			command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
			command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

			Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
			adapter.SelectCommand = command;
			adapter.Fill(data.CZMST_SkladLokace_Stav);

			decimal oldQuantity = decimal.Zero, newQuantity = decimal.Zero;

			if (data.CZMST_SkladLokace_Stav.Rows.Count > 0)
			{
				// zaznam existuje ... aktualizace mnozstvi
				// zjisteni aktualniho mnozstvi na lokaci
				oldQuantity = data.CZMST_SkladLokace_Stav.First().QTYSHPPD;
				checked { newQuantity = oldQuantity + record.QTYSHPPD; }

				// aktualizace mnozstvi
				UpdateQuantity(newQuantity, record.ITEMNMBR, record.SERLTNUM, record.LOCNCODE_SRC, record.SKL_ID_SRC, command, connection, transaction);
			}
			else
			{
				// zaznam neexistuje ... je mozne vlozit novy zaznam.
				// vytvoreni prikazu pro vlozeni nove hodnoty mnozstvi materialu do regalu.
				command.CommandText = "INSERT INTO " + TABLE_CZMST_SKLADLOKACE_STAV + " (ITEMNMBR, ITEMDESC, QTYSHPPD_DEF, QTYSHPPD, SERLTNUM, SKL_ID, LOCNCODE, DATECHANGE, EXPIRATION) VALUES(@itemnmbr, @itemdesc, @qtyshppd_def, @qtyshppd, @serltnum, @skl_id, @locncode, @datechange, @expiration)";

				command.Parameters.Clear();
				command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
				command.Parameters.AddWithValue("@itemdesc", record.ITEMDESC);
				command.Parameters.AddWithValue("@qtyshppd_def", (decimal)0);     // qtyshppd_def meni pouze inventura
				//command.Parameters.AddWithValue("@qtyshppd_def", record.QTYSHPPD_DEF));
				command.Parameters.AddWithValue("@qtyshppd", record.QTYSHPPD);
				command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
				command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);
				command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
				command.Parameters.AddWithValue("@datechange", DateTime.Now);
				//command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : null));
				command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : (object)DBNull.Value);

				command.ExecuteNonQuery();
			}

			// zjisteni maximalniho id z tabulky historie pohybu
			int max_id = CheckMaxID(TABLE_CZMST_SKLADLOKACE_STAVPOHYB, command, connection, transaction);

			// vlozeni zaznamu do tabulky historie pohybu
			AddMovement(TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, command, connection, transaction);
		}

		private void Lokace_MoveItem(LokacePohyb record, IngresCommand command, IngresConnection connection, IngresTransaction transaction, IngresDataAdapter adapter)
		{
			if (record == null || adapter == null || command == null || connection == null || transaction == null)
				throw new ApplicationException("Některý z parametrů funkce Lokace_MoveItem není inicializovaný.");

			if (record.POHYB_TYPE == TypeOfRecord.D)// && Source)
			{
				/* 1) vydani material z puvodni lokace
				 * 2) prijem material na novou lokaci
				 */

				string old_locncode_src = record.LOCNCODE_SRC;
				string old_locncode_dst = record.LOCNCODE_DST;
				string old_skl_id_src = record.SKL_ID_SRC;
				string old_skl_id_dst = record.SKL_ID_DST;

				record.POHYB_TYPE = TypeOfRecord.V;
				// vydej
				ProcessVydej(record, command, connection, transaction, adapter);

				// obraceni skladu, lokaci a zmena na prijem
				record.LOCNCODE_SRC = old_locncode_dst;
				record.LOCNCODE_DST = old_locncode_src;
				record.SKL_ID_SRC = old_skl_id_dst;
				record.SKL_ID_DST = old_skl_id_src;
				record.POHYB_TYPE = TypeOfRecord.P;

				// prijem
				ProcessPrijem(record, command, connection, transaction, adapter);
			}
			else if (record.POHYB_TYPE == TypeOfRecord.V)  // vydej
			{
				/*
				 * 1) samotny vydej ze zdrojove lokace
				 */

				ProcessVydej(record, command, connection, transaction, adapter);
			}
			else if (record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) // prijem
			{
				/*
				 * 1) samotny prijem ze zdrojove lokace
				 */

				ProcessPrijem(record, command, connection, transaction, adapter);
			}
			else throw new Exception("MoveItem - Pohyb není implementován : " + record.POHYB_TYPE.ToString());

		}


		#endregion

		#region Obsolete

		///// <summary>
		///// Zobrazi mnozstvi materialu na jednotlivych lokaci
		///// </summary>
		///// <param name="itemnmbr">ID materialu.</param>
		///// <param name="itemnmbr">ID skladu.</param>
		///// <param name="NumberOfRecords">Pocet zaznamu.</param>
		///// <returns>Odpovidajici zaznamy</returns>
		//public Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr, string skl_id, uint NumberOfRecords)
		//{
		//    IngresCommand command = null;
		//    IngresConnection connection = null;
		//    IngresDataAdapter adapter = null;

		//    try
		//    {
		//        connection = new IngresConnection(Properties.Settings.Default.SqlProviderConnection);
		//        Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
		//        command = new IngresCommand();
		//        adapter = new IngresDataAdapter();
		//        connection.Open();

		//        // select, kde mnozstvi neni 0
		//        command.CommandText = "SELECT TOP " + NumberOfRecords + " * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and SKL_ID=@skl_id and QTYSHPPD<>0";

		//        command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
		//        command.Parameters.AddWithValue("@skl_id", skl_id);

		//        command.Connection = connection;

		//        // naplneni datasetu
		//        adapter.SelectCommand = command;
		//        adapter.Fill(records.CZMST_SkladLokace_Stav);

		//        return records;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//    finally
		//    {
		//        if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
		//            connection.Close();
		//    }
		//}

		
		#endregion


		#region ILokace Members

		/// <summary>
		/// Zobrazi mnozstvi materialu v regalu.
		/// </summary>
		/// <param name="itemnmbr">Oznaceni materialu.</param>
		/// <param name="serltnum">Sarze.</param>
		/// <param name="locncode">Lokace.</param>
		/// <param name="skl_id">ID skladu.</param>
		/// <returns>Odpovidajici zaznamy.</returns>
		public Fask.Server.Interfaces.DataSets.Location Lokace_GeMaterial(string itemnmbr, string serltnum, string locncode, string skl_id)
		{
			IngresCommand command = null;
			IngresConnection connection = null;
			IngresDataAdapter adapter = null;

			try
			{
                Globals.LoadConfiguration();
                connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
				connection.Open();

				Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
				command = new IngresCommand();
				adapter = new IngresDataAdapter();

				// select, kde mnozstvi neni 0
				command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and SERLTNUM=@serltnum and LOCNCODE=@locncode and SKL_ID=@skl_id and QTYSHPPD<>0";

				command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
				command.Parameters.AddWithValue("@serltnum", serltnum);
				command.Parameters.AddWithValue("@locncode", locncode);
				command.Parameters.AddWithValue("@skl_id", skl_id);

				command.Connection = connection;

				// naplneni datasetu
				adapter.SelectCommand = command;
				adapter.Fill(records.CZMST_SkladLokace_Stav);

				return records;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					connection.Close();
			}
		}

		#endregion
	}
}
