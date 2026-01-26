using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Lokace;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : Fask.Server.Interfaces.Lokace.ILokace
    {

        #region Implementovano a Neco dela

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
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                if (locationType == TYPLokace.SOURCE && !(recordType == TypeOfRecord.P))
                {
                    // overovani zdrojove lokace ... kontrola, zdali je dany material a mnozstvi na dane lokaci a skladu
                    // pokud se jedna o prijem, nedochazi k overovani, protoze staci existence lokace ...
                    command.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND QTYSHPPD>=@qtyshppd AND LOCNCODE=@locncode AND SKL_ID=@skl_id";

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
                    command.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE LOCNCODE=@locncode AND SKL_ID=@skl_id";

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
        /// Zobrazi mnozstvi materialu na jednotlivych lokaci
        /// </summary>
        /// <param name="itemnmbr">ID materialu.</param>
        /// <param name="serltnum">Sarze.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="NumberOfRecords">Pocet zaznamu.</param>
        /// <returns>Odpovidajici zaznamy</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr, string serltnum, string skl_id, uint? NumberOfRecords, bool ShowEmpty)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                command = new SqlCommand();
                adapter = new SqlDataAdapter();
                connection.Open();

                // select, kde mnozstvi neni 0
                command.CommandText = "SELECT " + (NumberOfRecords.HasValue ? ("TOP " + NumberOfRecords.Value) : string.Empty) + "* FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE " +
                    (string.IsNullOrEmpty(itemnmbr) ? string.Empty : "ITEMNMBR=@itemnmbr and ") +
                    (string.IsNullOrEmpty(serltnum) ? string.Empty : "SERLTNUM=@serltnum and ") +
                    (string.IsNullOrEmpty(skl_id) ? string.Empty : "SKL_ID=@skl_id and ") +
                    (ShowEmpty ? string.Empty : "QTYSHPPD<>0 ") +
                    "order by LOCNCODE asc ";

                if (!string.IsNullOrEmpty(itemnmbr))
                    command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                if (!string.IsNullOrEmpty(serltnum))
                    command.Parameters.AddWithValue("@serltnum", serltnum);

                if (!string.IsNullOrEmpty(skl_id))
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
        
        /// <summary>
        /// Metoda pro pridani zaznamu do lokacniho mechanismu (vcetne pohybu).
        /// </summary>
        /// <param name="record">Trida reprezentujici zaznam=jeden radek tabulky.</param>
        public Fask.Server.Interfaces.Classes.StatusLokace Lokace_AddRecord(Fask.Server.Interfaces.Lokace.LokacePohyb record)
        {
            StatusLokace sl = new StatusLokace();
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlDataAdapter adapter = null;

            Classes.Lokace lokace = new Classes.Lokace();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                // Prijem
                if (record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    lokace.ProcessPrijem(record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();
                }
                // Vydej
                else if (record.POHYB_TYPE == TypeOfRecord.V)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    lokace.ProcessVydej(record, command, connection, transaction, adapter);

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
        /// Funkce pro zjisteni zda je material na dane lokaci - pro defregmentaci.
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu</param>
        /// <param name="locncode">ID Lokace.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <returns>Priznak existence.</returns>
        public bool Lokace_CheckMaterialInLocation(string itemnmbr, string locncode, string skl_id)
        {
            SqlCommand command = null;
            SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                //Kontrola jestli je na regalu umisteno vice nez 0 materialu 
                command.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE ITEMNMBR=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id";

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

            SqlCommand command = null;
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlDataAdapter adapter = null;

            Classes.Lokace lokace = new Classes.Lokace();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                // otevreni spojeni a tvorba transakce
                connection.Open();
                transaction = connection.BeginTransaction();

                command = new SqlCommand();
                adapter = new SqlDataAdapter();
                command.Connection = connection;
                command.Transaction = transaction;

                // nacteni zaznamu z historie pohybu podle GUID
                string universalCommand = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " WHERE guid=@guid ORDER BY id DESC";

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
                                lokace.ProcessVydej(record, command, connection, transaction, adapter);
                            }
                            else if (dr.POHYB_TYPE == TypeOfRecord.V.ToString())
                            {
                                //Je treba prijmat kladnou hodnotu - vydej je vzdy zapsan se zapornou hodnotou mnozstvi
                                record.QTYSHPPD *= -1;
                                //Volani funkce pro opacny typ zaznamu - prijem.
                                record.POHYB_TYPE = TypeOfRecord.P;
                                lokace.ProcessPrijem(record, command, connection, transaction, adapter);
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
        /// Zobrazi mnozstvi materialu v regalu
        /// </summary>
        /// <param name="itemnmbr">ID Polozky</param>
        /// <param name="serltnum">SerioveCislo/ Sarze</param>
        /// <param name="locncode">Lokace</param>
        /// <param name="skl_id">ID skladu</param>
        /// <returns>Odpovidajici zaznamy</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_GeMaterial(string itemnmbr, string serltnum, string locncode, string skl_id)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                // select, kde mnozstvi neni 0
                command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and SERLTNUM=@serltnum and LOCNCODE=@locncode and SKL_ID=@skl_id and QTYSHPPD<>0";

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

        /// <summary>
        /// Defregmentace (presun ze zdrojove lokace/skladu na cilovou lokaci/sklad)
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se defregmentace tyka.</param>
        public Fask.Server.Interfaces.Classes.StatusLokace Lokace_MoveItem(Fask.Server.Interfaces.Lokace.LokacePohyb Record)
        {
            StatusLokace sl = new StatusLokace();

            SqlCommand command = null;
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlDataAdapter adapter = null;

            Classes.Lokace lokace = new Classes.Lokace();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new SqlDataAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();

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
                    lokace.ProcessVydej(Record, command, connection, transaction, adapter);

                    // obraceni skladu, lokaci a zmena na prijem
                    Record.LOCNCODE_SRC = old_locncode_dst;
                    Record.LOCNCODE_DST = old_locncode_src;
                    Record.SKL_ID_SRC = old_skl_id_dst;
                    Record.SKL_ID_DST = old_skl_id_src;
                    Record.POHYB_TYPE = TypeOfRecord.P;

                    // prijem
                    lokace.ProcessPrijem(Record, command, connection, transaction, adapter);

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
                    lokace.ProcessVydej(Record, command, connection, transaction, adapter);

                    if (transaction != null)
                        transaction.Commit();

                    sl.State = States.OK;
                }
                else if (Record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) // prijem
                {
                    /*
                     * 1) samotny prijem na zdrojovou lokaci
                     */

                    connection.Open();
                    transaction = connection.BeginTransaction();
                    lokace.ProcessPrijem(Record, command, connection, transaction, adapter);

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
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                // select, kde mnozstvi neni 0
                command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and QTYSHPPD<>0";

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
        /// Zobrazi material serazeny podle expirace od nejstarsiho (tabulka get_os_ms).
        /// </summary>
        /// <param name="itemnmbr">Oznaceni materialu.</param>
        /// <param name="skl_id">ID skladu.</param>
        /// <returns>Odpovidajici zaznamy.</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowOldestMaterial(string itemnmbr, string skl_id)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                string strcommand = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE SKL_ID=@skl_id and ITEMNMBR=@itemnmbr AND QTYSHPPD<>0 ORDER BY EXPIRATION ASC";
                command = new SqlCommand(strcommand, connection);

                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                //Naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new SqlDataAdapter();
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
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                string strcommand = "SELECT TOP " + NumberOfRecords + " * FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE SKL_ID=@skl_id and ITEMNMBR=@itemnmbr AND QTYSHPPD<>0 ORDER BY EXPIRATION ASC";
                command = new SqlCommand(strcommand, connection);

                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                // naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new SqlDataAdapter();
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
        /// Funkce pro navrat prijmovych lokaci podle ID skladu
        /// </summary>
        /// <param name="skl_id">ID skladu</param>
        /// <returns>Prijmove lokace</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_ShowReceiveLocations(string skl_id)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                string strcommand =
                    "SELECT mapa.* FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " mapa " +
                    "JOIN " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceTypy + " typy on typy.TYPE=mapa.TYPE " +
                    "WHERE mapa.SKL_ID=@skl_id and typy.IS_RECEIVE > 0";
                command = new SqlCommand(strcommand, connection);

                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);

                // naplneni datasetu.
                Fask.Server.Interfaces.DataSets.Location records = new Fask.Server.Interfaces.DataSets.Location();
                adapter = new SqlDataAdapter();
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
        /// Zjisteni, zdali varianta lokace existuje.
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
        /// <param name="locncode">Lokace</param>
        /// <returns>True-OK, False-Chyba</returns>
        public bool Lokace_VariantySortimentExists(string itemnmbr, string skl_id, string locncode)
        {
            //TABLE_CZMST_SkladLokace_LokaceVariantySortiment
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT COUNT(*) FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " WHERE itemnmbr=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id";

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
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText =
                    "SELECT lvs.*, lt.* FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " lvs " +
                    " LEFT JOIN " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceTypy + " lt on lt.TYPE=lvs.TYPE " +
                    " WHERE lvs.ITEMNMBR=@itemnmbr AND lvs.SKL_ID=@skl_id AND lt.IS_DEFAULT=1";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);
                command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);
                System.Data.DataSet lokaceDS = new System.Data.DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(command);
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
        /// Ziskani informaci varianty lokaci pro polozku skladu
        /// </summary>
        /// <param name="itemnmbr">Polozka ID</param>
        /// <param name="skl_id">Sklad ID</param>
        /// <returns>Location- dotažene lokace</returns>
        public Fask.Server.Interfaces.DataSets.Location Lokace_VariantySortimentGet(string itemnmbr, string skl_id)
        {
            //TABLE_CZMST_SkladLokace_LokaceVariantySortiment
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            Fask.Server.Interfaces.DataSets.Location locationDS = new Fask.Server.Interfaces.DataSets.Location();

            try
            {
                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " WHERE itemnmbr=@itemnmbr";
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                if (!String.IsNullOrEmpty(skl_id))
                {
                    command.CommandText += " AND SKL_ID=@skl_id";
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);
                }

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceVariantySortiment);

                command.Parameters.Clear();
                command.CommandText = "Select * FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceTypy;
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
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();
            SqlTransaction sqltrans = null;

            try
            {
                Fask.Server.Interfaces.DataSets.Location locationDS = new Fask.Server.Interfaces.DataSets.Location();
                Fask.Server.Interfaces.DataSets.Location.CZMST_SkladLokace_LokaceTypyRow locationRow = null;

                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                // Transakce 
                sqltrans = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = sqltrans;

                // 1) Kontrola zda lokace na sklade v mape je zavedena !!!
                command.CommandText =
                    "Select Count(*) FROM " + Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA +
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
                    "Select * from " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceTypy +
                    " WHERE TYPE=@type";
                command.Parameters.AddWithValue("@type", type);
                SqlDataAdapter sda = new SqlDataAdapter(command);
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
                    "Select * FROM " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
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
                            "DELETE " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
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
                            "DELETE " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
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
                        "insert into " + Constants.Common.TABLE_CZMST_SkladLokace_LokaceVariantySortiment +
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

        #endregion

        #region Private Metody

        private void Lokace_MoveItem(LokacePohyb record, SqlCommand command, SqlConnection connection, SqlTransaction transaction, SqlDataAdapter adapter)
        {
            Classes.Lokace lokace = new Classes.Lokace();

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
                lokace.ProcessVydej(record, command, connection, transaction, adapter);

                // obraceni skladu, lokaci a zmena na prijem
                record.LOCNCODE_SRC = old_locncode_dst;
                record.LOCNCODE_DST = old_locncode_src;
                record.SKL_ID_SRC = old_skl_id_dst;
                record.SKL_ID_DST = old_skl_id_src;
                record.POHYB_TYPE = TypeOfRecord.P;

                // prijem
                lokace.ProcessPrijem(record, command, connection, transaction, adapter);
            }
            else if (record.POHYB_TYPE == TypeOfRecord.V)  // vydej
            {
                /*
				 * 1) samotny vydej ze zdrojove lokace
				 */

                lokace.ProcessVydej(record, command, connection, transaction, adapter);
            }
            else if (record.POHYB_TYPE == Fask.Server.Interfaces.Lokace.TypeOfRecord.P) // prijem
            {
                /*
				 * 1) samotny prijem na zdrojovou lokaci
				 */

                lokace.ProcessPrijem(record, command, connection, transaction, adapter);
            }
            else throw new Exception("MoveItem - Pohyb není implementován : " + record.POHYB_TYPE.ToString());

        }

        public StatusOverLokace Lokace_OverLokace(string skl_id, string locncode)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
