using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.SkladLokace;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Classes;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.SkladLokace.ISkladLokace2,
        Fask.Interfaces.SkladLokace.ISkladLokace2_GetFiltrovanySkladLokaceStav,
        Fask.Interfaces.SkladLokace.ISkladLokace2_InsertStavPohyb,
        Fask.Interfaces.SkladLokace.ISkladLokace2_ImportLokMech_From_I4,
        Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_IS,
        Fask.Interfaces.SkladLokace.ISkladLokace2_GetFilt_CopareToIS_FASK,
        Fask.Interfaces.SkladLokace.ISkladLokace2_GetPohybyIS,
        Fask.Interfaces.SkladLokace.ISkladLokace2_ClearLokMech
    {
        #region ISkladLokace2_GetFiltrovanySkladLokaceStav Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokaceStav(Fask.Interfaces.Filtry.SkladLokaceStavListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                // 14.7.2016 PeV: stav.* se nepouziva, protoze itemdesc je prebirano z FASK_ZASOBY
                command.CommandText =
                    "select stav.ITEMNMBR, zbozi.ITEMDESC, stav.QTYSHPPD_DEF, stav.QTYSHPPD, stav.SERLTNUM, stav.SKL_ID, stav.LOCNCODE, stav.DATECHANGE, stav.EXPIRATION, stav.QTYSHPPD_DEF_DATE, stav.QTY_OWNER, stav.PRAC_ID_OWNER, zbozi.ITEMCODE, zbozi.VNDITNUM, zbozi.CZ_CarKod " +
                    " , zbozi.CZ_SerNum_Track " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " stav " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " zbozi on zbozi.ITEMNMBR = stav.ITEMNMBR " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
                {
                    command.CommandText += "and stav.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialITEMNMBR);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialSklID))
                {
                    command.CommandText += "and stav.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.MaterialSklID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialLocncode))
                {
                    command.CommandText += "and stav.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.MaterialLocncode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialSerltnum))
                {
                    command.CommandText += "and stav.SERLTNUM=@serltnum ";
                    command.Parameters.AddWithValue("@serltnum", filtr.MaterialSerltnum);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "and zbozi.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "and (zbozi.CZ_CarKod=@barcode or zbozi.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialPracID))
                {
                    command.CommandText += "and (stav.PRAC_ID_OWNER=@PRAC_ID_OWNER) ";
                    command.Parameters.AddWithValue("@PRAC_ID_OWNER", filtr.MaterialPracID);
                }

                if (filtr.PouzeNenulovyStav)
                {
                    command.CommandText += "and (stav.QTYSHPPD < 0) OR (stav.QTYSHPPD > 0)";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Stav);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace2_InsertStavPohyb Members

        public bool InsertStavPohyb(LokacePohyb Record)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            System.Data.SqlClient.SqlTransaction transaction = null;

            try
            {
                Globals_V1.LoadConfiguration();

                if (Record == null)
                    throw new ApplicationException("Některý z parametrů funkce Lokace_MoveItem není inicializovaný.");

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                connection.Open();
                transaction = connection.BeginTransaction();

                if (Record.POHYB_TYPE == TypeOfRecord.D)
                {
                    /* 1) vydani material z puvodni lokace
                     * 2) prijem material na novou lokaci
                     */

                    string old_locncode_src = Record.LOCNCODE_SRC;
                    string old_locncode_dst = Record.LOCNCODE_DST;
                    string old_skl_id_src = Record.SKL_ID_SRC;
                    string old_skl_id_dst = Record.SKL_ID_DST;

                    Record.POHYB_TYPE = TypeOfRecord.V;

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
                }
                else if (Record.POHYB_TYPE == TypeOfRecord.V)  // vydej
                {
                    /*
                     * 1) samotny vydej ze zdrojove lokace
                     */
                    ProcessVydej(Record, command, connection, transaction, adapter);
                }
                else if (Record.POHYB_TYPE == TypeOfRecord.P) // prijem
                {
                    /*
                     * 1) samotny prijem na zdrojovou lokaci
                     */
                    ProcessPrijem(Record, command, connection, transaction, adapter);
                }
                else throw new Exception("InsertStavPohyb - Pohyb není implementován : " + Record.POHYB_TYPE.ToString());

                if (transaction != null)
                    transaction.Commit();

                return true;
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
        }

        #endregion

        #region Interne metody

        private void ProcessPrijem(LokacePohyb record, SqlCommand command, SqlConnection connection, SqlTransaction transaction, SqlDataAdapter adapter)
        {
            if (record == null || adapter == null || command == null || connection == null || transaction == null)
                throw new ApplicationException("Některý z parametrů funkce ProcessPrijem není inicializovaný.");

            // kontrola na existenci lokace
            StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, command, connection, transaction);
            if (status.State == STATUSOverLokace.ERROR)
                throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

            // kontrola aktualniho mnozstvi na lokaci
            command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
            command.Connection = connection;
            command.Transaction = transaction;

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
            command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
            command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
            command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

            //Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
            Fask.Interfaces.DataSets.SkladLokace data = new Fask.Interfaces.DataSets.SkladLokace();
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
                command.Parameters.Clear();

                // zaznam neexistuje ... je mozne vlozit novy zaznam.
                // vytvoreni prikazu pro vlozeni nove hodnoty mnozstvi materialu do regalu.
                command.CommandText =
                    "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV +
                    " (ITEMNMBR, ITEMDESC, QTYSHPPD_DEF, QTYSHPPD, SERLTNUM, SKL_ID, LOCNCODE, DATECHANGE, EXPIRATION, QTY_OWNER, PRAC_ID_OWNER)" +
                    " VALUES" +
                    " (@itemnmbr, @itemdesc, @qtyshppd_def, @qtyshppd, @serltnum, @skl_id, @locncode, @datechange, @expiration, @qty_owner, @prac_id_owner)";

                // TODO : ? predelat na AddWithValue???
                command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
                command.Parameters.AddWithValue("@itemdesc", record.ITEMDESC);
                //command.Parameters.AddWithValue("@qtyshppd_def", (decimal)0.0));     // qtyshppd_def meni pouze inventura
                command.Parameters.AddWithValue("@qtyshppd_def", (decimal)0);
                //command.Parameters.AddWithValue("@qtyshppd_def", record.QTYSHPPD_DEF));
                command.Parameters.AddWithValue("@qtyshppd", record.QTYSHPPD);
                command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
                command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);
                command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
                command.Parameters.AddWithValue("@datechange", DateTime.Now);
                //command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : null));
                command.Parameters.AddWithValue("@expiration", record.Expiration.HasValue ? record.Expiration : (object)DBNull.Value);
                command.Parameters.AddWithValue("@qty_owner", record.QTY_OWNER);
                command.Parameters.AddWithValue("@prac_id_owner", record.PRAC_ID_OWNER);

                command.ExecuteNonQuery();
            }

            // zjisteni maximalniho id z tabulky historie pohybu
            int max_id = CheckMaxID(Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, command, connection, transaction);

            // vlozeni zaznamu do tabulky historie pohybu
            AddMovement(Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, command, connection, transaction);
        }

        private StatusOverLokace OverLokace(string skl_id, string locncode, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
        {
            if (command == null || connection == null || transaction == null)
                throw new ApplicationException("Některý z parametrů funkce OverLokace není inicializovaný.");

            StatusOverLokace status = new StatusOverLokace();

            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandText = "SELECT COUNT(*) FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " WHERE skl_id=@skl_id and locncode=@locncode";

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

        /// <summary>
        /// Vydej materialu.
        /// </summary>
        /// <param name="Record">Zaznam, ktereho se vydej tyka.</param>
        /// <param name="Command">Prikaz.</param>
        /// <param name="Connection">Pripojeni.</param>
        /// <param name="Transaction">Transakce.</param>
        /// <param name="Adapter">Adapter.</param>
        private void ProcessVydej(LokacePohyb record, SqlCommand Command, SqlConnection Connection, SqlTransaction Transaction, SqlDataAdapter Adapter)
        {
            if (record == null || Adapter == null || Command == null || Connection == null || Transaction == null)
                throw new ApplicationException("Některý z parametrů funkce ProcessVydej není inicializovaný.");

            // kontrola na existenci lokace
            StatusOverLokace status = OverLokace(record.SKL_ID_SRC, record.LOCNCODE_SRC, Command, Connection, Transaction);
            if (status.State == STATUSOverLokace.ERROR)
                throw new Exception("Lokace '" + record.LOCNCODE_SRC.Trim() + "' neexistuje ve skladu '" + record.SKL_ID_SRC.Trim() + "'!");

            Command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr AND SERLTNUM=@serltnum AND LOCNCODE=@locncode AND SKL_ID=@skl_id";
            Command.Connection = Connection;
            Command.Transaction = Transaction;

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@itemnmbr", record.ITEMNMBR);
            Command.Parameters.AddWithValue("@serltnum", record.SERLTNUM);
            Command.Parameters.AddWithValue("@locncode", record.LOCNCODE_SRC);
            Command.Parameters.AddWithValue("@skl_id", record.SKL_ID_SRC);

            //Fask.Server.Interfaces.DataSets.Location data = new Fask.Server.Interfaces.DataSets.Location();
            Fask.Interfaces.DataSets.SkladLokace data = new Fask.Interfaces.DataSets.SkladLokace();
            Adapter.SelectCommand = Command;
            Adapter.Fill(data.CZMST_SkladLokace_Stav);
            // TODO: pokud je vysledne mnozstvi 0, smazat zaznam z tabulky SKLADLOKACE_STAV?? (aby se pripadne vytvoril novy zaznam s qtyshhpd_def)
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
                throw new Exception("'" + record.ITEMNMBR.Trim() + "' není možné vydávat do mínusu!\n" + "(Výsledné množství = původní - zadané) : \n" + newQuantity + " = " + oldQuantity + " - " + record.QTYSHPPD);
            }

            // aktualizace mnozstvi
            UpdateQuantity(newQuantity, record.ITEMNMBR, record.SERLTNUM, record.LOCNCODE_SRC, record.SKL_ID_SRC, Command, Connection, Transaction);

            // zjisteni dosavadniho maximalniho id z tabulky historie pohybu
            int max_id = CheckMaxID(Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, Command, Connection, Transaction);

            // vlozeni zaznamu do historie pohybu
            AddMovement(Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAVPOHYB, record, max_id, Command, Connection, Transaction);
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
        private void UpdateQuantity(decimal QTYSHPPD, string ITEMNMBR, string serltnum, string LOCNCODE, string skl_id, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
        {
            //Prikaz pro zmenu hodnoty mnozstvi materialu ve cteckove pozici.
            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " SET QTYSHPPD=@qtyshppd, DATECHANGE=@datechange WHERE ITEMNMBR=@itemnmbr AND LOCNCODE=@locncode AND SKL_ID=@skl_id AND SERLTNUM=@serltnum";
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
        private void AddMovement(string tablename, LokacePohyb record, int ID, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
        {
            //Kontrola, jetsli jsou promenne alokovany.
            if (string.IsNullOrEmpty(tablename) || record == null || command == null)
                throw new ApplicationException("Některý z parametrů funkce AddMovement není inicializovaný.");

            command.Connection = connection;
            command.Transaction = transaction;

            //Fask.Logging.LokaceLog.writeBody(record);

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
            if (record.CountEntries.HasValue)
                command.Parameters.AddWithValue("@countentries", record.CountEntries);
            else
                command.Parameters.AddWithValue("@countentries", (object)DBNull.Value);

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
        private int CheckMaxID(string tablename, SqlCommand command, SqlConnection connection, SqlTransaction transaction)
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



        #endregion

        #region ISkladLokace2_ImportLokMech_From_I4 Members

        public int ImportLokMech_From_I4(int CountEntries)
        {
            int x = 0;
            try
            {
                Globals_V1.LoadConfiguration();
                using (var conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "FASK_proc_NaplnLokMechZ_INV";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CountEntries", CountEntries);

                        conn.Open();
                        x = command.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return x;
        }


        #endregion

        #region ISkladLokace2_GetFilt_CopareToIS_IS Members

        public SkladLokace_CompareToIS GetFilt_CopareToIS_IS(Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                command.CommandText =
                                    " SELECT " +
                                    " F.ITEMNMBR, " +
                                    " Z.ITEMCODE, " +
                                    " Z.ITEMDESC, " +
                                    " Z.VNDITNUM, " +
                                    " F.QTYSHPPD_Pohoda, " +
                                    " Z.MJ, " +
                                    " F.SERLTNUM, " +
                                    " F.EXPIRACE, " +
                                    " F.SKL_ID, " +
                                    " Sklad.skl_desc, " +
                                    " F.status, " +
                                    " F.QTYSHPPD_LokMech " + 
                                    " FROM[dbo].[FASK_Get_CompareToIS_FromIS] (" +
                                    "'" + filtr.SklID + "'" +
                                    "," + (filtr.V_0 ? "1" : "0") +
                                    "," + (filtr.V_1 ? "1" : "0") +
                                    "," + (filtr.V_2 ? "1" : "0") +
                                    "," + (filtr.V_3 ? "1" : "0") +
                                    "," + (filtr.V_4 ? "1" : "0") +
                                    "," + (filtr.V_5 ? "1" : "0") +
                                    "," + (filtr.V_6 ? "1" : "0") +
                                    ") as F " +
                                    " left join FASK_ZASOBY as Z ON Z.ITEMNMBR = F.ITEMNMBR " +
                                    " left join CZMST093 as Sklad ON Sklad.skl_id = F.SKL_ID ";

                command.CommandText += " WHERE 1 = 1 ";
;

                if (!string.IsNullOrEmpty(filtr.ITEMCODE))
                {

                    command.CommandText += " AND Z.ITEMCODE like '";

                    if (filtr.ITEMCODE_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMCODE.Trim();


                    if (filtr.ITEMCODE_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {

                    command.CommandText += " AND Z.ITEMDESC like '";

                    if (filtr.ITEMDESC_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMDESC.Trim();


                    if (filtr.ITEMDESC_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.SERLTNUM))
                {

                    command.CommandText += " AND F.SERLTNUM like '";

                    if (filtr.SERLTNUM_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.SERLTNUM.Trim();


                    if (filtr.SERLTNUM_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }


                adapter.SelectCommand = command;
                adapter.Fill(ds.SKz_Stav);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace2_GetFilt_CopareToIS_FASK Member

        public SkladLokace_CompareToIS GetFilt_CopareToIS_FASK(Fask.Interfaces.Filtry.SkladLokaceCompareToISFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                command.CommandText =
                                    " SELECT " +
                                    " F.ITEMNMBR, " +
                                    " Z.ITEMCODE, " +
                                    " Z.ITEMDESC, " +
                                    " Z.VNDITNUM, " +
                                    " F.QTYSHPPD, " +
                                    " Z.MJ, " +
                                    " F.SERLTNUM, " +
                                    " F.EXPIRACE, " +
                                    " F.SKL_ID, " +
                                    " Sklad.skl_desc, " +
                                    " F.status " +
                                    " FROM[dbo].[FASK_Get_CompareToIS_FromFASK] (" +
                                    "'" + filtr.SklID + "'" +
                                    "," + (filtr.V_0 ? "1" : "0") +
                                    "," + (filtr.V_1 ? "1" : "0") +
                                    "," + (filtr.V_2 ? "1" : "0") +
                                    "," + (filtr.V_3 ? "1" : "0") +
                                    "," + (filtr.V_4 ? "1" : "0") +
                                    "," + (filtr.V_5 ? "1" : "0") +
                                    "," + (filtr.V_6 ? "1" : "0") +
                                    ") as F " +
                                    " left join FASK_ZASOBY as Z ON Z.ITEMNMBR = F.ITEMNMBR " +
                                    " left join CZMST093 as Sklad ON Sklad.skl_id = F.SKL_ID ";

                command.CommandText += " WHERE 1 = 1 ";
                ;

                if (!string.IsNullOrEmpty(filtr.ITEMCODE))
                {

                    command.CommandText += " AND Z.ITEMCODE like '";

                    if (filtr.ITEMCODE_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMCODE.Trim();


                    if (filtr.ITEMCODE_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {

                    command.CommandText += " AND Z.ITEMDESC like '";

                    if (filtr.ITEMDESC_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.ITEMDESC.Trim();


                    if (filtr.ITEMDESC_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.SERLTNUM))
                {

                    command.CommandText += " AND F.SERLTNUM like '";

                    if (filtr.SERLTNUM_L)
                        command.CommandText += "%";

                    command.CommandText += filtr.SERLTNUM.Trim();


                    if (filtr.SERLTNUM_R)
                        command.CommandText += "%";

                    command.CommandText += "' ";

                }



                adapter.SelectCommand = command;
                adapter.Fill(ds.LokMech_Stav);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace2_GetPohybyIS Member

        public SkladLokace_CompareToIS GetPohybyIS(Fask.Interfaces.Filtry.PohybyISListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;


                command.CommandText =
                                " SELECT " +
                                " F.ITEMNMBR, " +
                                " Z.ITEMCODE," +
                                " Z.ITEMDESC, " +
                                " Z.VNDITNUM, " +
                                " Z.MJ, " +
                                " Z.SKL_ID, " +
                                " Sklad.skl_desc, " +
                                " F.TypPohybu, " +
                                " F.ZdrojPohybu, " +
                                " F.DatumPohybu, " +
                                " F.PocetNaPohybu, " +
                                " F.StavPoPohybu, " +
                                " F.CisloDokladu, " +
                                " F.KdoVytvoril, " +
                                " F.Ucetni, " +
                                " F.DatumVytvoreni, " +
                                " F.DatumUlozeni " +
                                " FROM [FASK_Get_PohybyFromPOHODA]() as F " +
                                " LEFT JOIN FASK_ZASOBY as Z ON Z.ITEMNMBR = F.ITEMNMBR " +
                                " LEFT JOIN CZMST093 as Sklad ON Sklad.skl_id = Z.SKL_ID ";

                command.CommandText += " WHERE 1 = 1 ";
                

                if (!string.IsNullOrEmpty(filtr.ITEMNMBR))
                {

                    command.CommandText += " AND F.ITEMNMBR = '";
                    command.CommandText += filtr.ITEMNMBR.Trim();
                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMCODE))
                {

                    command.CommandText += " AND Z.ITEMCODE = '";
                    command.CommandText += filtr.ITEMCODE.Trim();
                    command.CommandText += "' ";

                }

                if (!string.IsNullOrEmpty(filtr.ITEMDESC))
                {

                    command.CommandText += " AND Z.ITEMDESC like '";
                    command.CommandText += filtr.ITEMDESC.Trim();
                    command.CommandText += "%' ";

                }

                // hledání podle datumu
                if (filtr.DatumUlozeni_OD != null && filtr.DatumUlozeni_DO != null)
                {
                    command.CommandText += " AND F.DatumUlozeni between @DatumUlozeniOD and @DatumUlozeniDO";
                    command.Parameters.AddWithValue("@DatumUlozeniOD", filtr.DatumUlozeni_OD);
                    command.Parameters.AddWithValue("@DatumUlozeniDO", filtr.DatumUlozeni_DO);
                }
                else
                {
                    if (filtr.DatumUlozeni_OD != null)
                    {
                        command.CommandText += " AND F.DatumUlozeni > @DatumUlozeniOD";
                        command.Parameters.AddWithValue("@DatumUlozeniOD", filtr.DatumUlozeni_OD);
                    }
                    else if (filtr.DatumUlozeni_DO != null)
                    {
                        command.CommandText += " AND F.DatumUlozeni < @DatumUlozeniDO";
                        command.Parameters.AddWithValue("@DatumUlozeniDO", filtr.DatumUlozeni_DO);
                    }
                }

                if (filtr.DatumUlozeni_TimeVariant.HasValue)
                {
                    command.CommandText += " AND F.DatumUlozeni > @DatumUlozeni_TV";
                    command.Parameters.AddWithValue("@DatumUlozeni_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, filtr.DatumUlozeni_TimeVariant.Value));
                }

                // hledání podle datumu
                if (filtr.DatumVytvoreni_OD != null && filtr.DatumVytvoreni_DO != null)
                {
                    command.CommandText += " AND F.DatumVytvoreni between @DatumVytvoreniOD and @DatumVytvoreniDO";
                    command.Parameters.AddWithValue("@DatumVytvoreniOD", filtr.DatumVytvoreni_OD);
                    command.Parameters.AddWithValue("@DatumVytvoreniDO", filtr.DatumVytvoreni_DO);
                }
                else
                {
                    if (filtr.DatumVytvoreni_OD != null)
                    {
                        command.CommandText += " AND F.DatumVytvoreni > @DatumVytvoreniOD";
                        command.Parameters.AddWithValue("@DatumVytvoreniOD", filtr.DatumVytvoreni_OD);
                    }
                    else if (filtr.DatumVytvoreni_DO != null)
                    {
                        command.CommandText += " AND F.DatumVytvoreni < @DatumVytvoreniDO";
                        command.Parameters.AddWithValue("@DatumVytvoreniDO", filtr.DatumVytvoreni_DO);
                    }
                }

                if (filtr.DatumVytvoreni_TimeVariant.HasValue)
                {
                    command.CommandText += " AND F.DatumVytvoreni > @DatumVytvoreni_TV";
                    command.Parameters.AddWithValue("@DatumVytvoreni_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, filtr.DatumVytvoreni_TimeVariant.Value));
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.POHODA_Pohyby);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace2_ClearLokMech Member

        public void ClearLokMech()
        {
            try
            {
                Globals_V1.LoadConfiguration();
                using (var conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = conn.CreateCommand())
                    {


                        conn.Open();

                        command.CommandText = "DELETE FROM CZMST_SkladLokace_Stav";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();

                        command.CommandText = "DELETE FROM CZMST_SkladLokace_StavPohyb";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();

                        conn.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
