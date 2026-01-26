using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Interfaces.DataSets;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.IT_cast.IIT_cast,
        Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace,
        Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_GetFiltrovaneData,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Insert,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Update,
        Fask.Interfaces.Ciselniky.TypyDokladu.ITypyDokladu2_Delete
    {
        #region IIT_cast

        #region IIT_cast_Events_Archivace
        public int ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(ConnectionString);
                command = new SqlCommand("FASK_Events_ArchivaceZaznamu", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DatumOd", SqlDbType.DateTime).Value = OD ?? (object)DBNull.Value;
                command.Parameters.Add("@DatumDo", SqlDbType.DateTime).Value = DO ?? (object)DBNull.Value;

                // Parametr pro výstupní hodnotu @PocetPresunutych
                SqlParameter pocetPresunutychParam = new SqlParameter("@PocetPresunutych", SqlDbType.Int);
                pocetPresunutychParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(pocetPresunutychParam);

                conn.Open();
                command.ExecuteNonQuery();

                // Získání hodnoty výstupního parametru @PocetPresunutych
                pocetZaznamu = (int)pocetPresunutychParam.Value;

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return pocetZaznamu;
        }

        public int Archivace_Fask_Event_id(int id)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(ConnectionString);
                command = new SqlCommand("FASK_Events_ArchivaceZaznamu_id", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@pom_id", SqlDbType.Int).Value = id;

                // Parametr pro výstupní hodnotu @PocetPresunutych
                SqlParameter pocetPresunutychParam = new SqlParameter("@PocetPresunutych", SqlDbType.Int);
                pocetPresunutychParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(pocetPresunutychParam);

                conn.Open();
                command.ExecuteNonQuery();

                // Získání hodnoty výstupního parametru @PocetPresunutych
                pocetZaznamu = (int)pocetPresunutychParam.Value;

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return pocetZaznamu;
        }





        #endregion

        #region IIT_cast_Production_Archivace
        public int Production_ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(ConnectionString);
                command = new SqlCommand("Production_ArchivaceZaznamu", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DatumOd", SqlDbType.DateTime).Value = OD ?? (object)DBNull.Value;
                command.Parameters.Add("@DatumDo", SqlDbType.DateTime).Value = DO ?? (object)DBNull.Value;

                // Parametr pro výstupní hodnotu @PocetPresunutych
                SqlParameter pocetPresunutychParam = new SqlParameter("@PocetPresunutych", SqlDbType.Int);
                pocetPresunutychParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(pocetPresunutychParam);

                conn.Open();
                command.ExecuteNonQuery();

                // Získání hodnoty výstupního parametru @PocetPresunutych
                pocetZaznamu = (int)pocetPresunutychParam.Value;

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return pocetZaznamu;
        }

        public int Production_ArchivaceProcedura_guid(Guid guid)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(ConnectionString);
                command = new SqlCommand("Production_ArchivaceZaznamu_guid", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@pom_guid", SqlDbType.UniqueIdentifier).Value = guid;

                // Parametr pro výstupní hodnotu @PocetPresunutych
                SqlParameter pocetPresunutychParam = new SqlParameter("@PocetPresunutych", SqlDbType.Int);
                pocetPresunutychParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(pocetPresunutychParam);

                conn.Open();
                command.ExecuteNonQuery();

                // Získání hodnoty výstupního parametru @PocetPresunutych
                pocetZaznamu = (int)pocetPresunutychParam.Value;

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return pocetZaznamu;
        }

        public bool TypyDokladu_Delete(int id)
        {
            //  throw new NotImplementedException();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Kontrola, zda záznam s daným DEX_ROW_ID existuje
                        string checkQuery = "SELECT COUNT(1) FROM [dbo].[CZMST092] WHERE [DEX_ROW_ID] = @DEX_ROW_ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@DEX_ROW_ID", id);

                            int exists = (int)checkCmd.ExecuteScalar();
                            if (exists == 0)
                            {
                                // Záznam s tímto DEX_ROW_ID neexistuje, rollback a ukončení
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Dotaz na smazání záznamu
                        string query = "DELETE FROM [dbo].[CZMST092] WHERE [DEX_ROW_ID] = @DEX_ROW_ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DEX_ROW_ID", id);

                            // Provede se DELETE dotaz
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Commit transakce, pokud byl záznam smazán
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                // Rollback, pokud nedošlo ke smazání
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log nebo další ošetření chyby
                throw ex;
            }
        }

        public bool Update(Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row Row)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Kontrola, zda záznam s daným DEX_ROW_ID existuje
                        string checkQuery = "SELECT COUNT(1) FROM [dbo].[CZMST092] WHERE [DEX_ROW_ID] = @DEX_ROW_ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@DEX_ROW_ID", Row.DEX_ROW_ID);

                            int exists = (int)checkCmd.ExecuteScalar();
                            if (exists == 0)
                            {
                                // Záznam s tímto DEX_ROW_ID neexistuje, rollback a ukončení
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Generování stringu pro dotaz s hodnotami
                        string query = GenerateSQLWithValues(Row);

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DEX_ROW_ID", Row.DEX_ROW_ID);
                            cmd.Parameters.AddWithValue("@doc_id2", Row.doc_id2);
                            cmd.Parameters.AddWithValue("@doc_desc", Row.doc_desc);
                            cmd.Parameters.AddWithValue("@doc_typ", Row.doc_typ);
                            cmd.Parameters.AddWithValue("@doc_carcode", Row.doc_carcode);
                            cmd.Parameters.AddWithValue("@LOCNCODE", Row.LOCNCODE);
                            cmd.Parameters.AddWithValue("@cfg_odb", Row.cfg_odb);
                            cmd.Parameters.AddWithValue("@cfg_str", Row.cfg_str);
                            cmd.Parameters.AddWithValue("@cfg_prac", Row.cfg_prac);
                            cmd.Parameters.AddWithValue("@cfg_mn2sn", Row.cfg_mn2sn);
                            cmd.Parameters.AddWithValue("@cfg_disp", Row.cfg_disp);
                            cmd.Parameters.AddWithValue("@cfg_palety", Row.cfg_palety);
                            cmd.Parameters.AddWithValue("@cfg_paleta_id", Row.cfg_paleta_id);
                            cmd.Parameters.AddWithValue("@cfg_zakazka_id", Row.cfg_zakazka_id);
                            cmd.Parameters.AddWithValue("@cfg_mena_id", Row.cfg_mena_id);
                            cmd.Parameters.AddWithValue("@cfg_tisk", Row.cfg_tisk);
                            cmd.Parameters.AddWithValue("@cfg_prevod_sklad", Row.cfg_prevod_sklad);
                            cmd.Parameters.AddWithValue("@cfg_tisk_soupis", Row.cfg_tisk_soupis);
                            cmd.Parameters.AddWithValue("@SKL_ID", Row.SKL_ID);
                            cmd.Parameters.AddWithValue("@cfg_lokace", Row.cfg_lokace);
                            cmd.Parameters.AddWithValue("@cfg_lokace_ciselnik", Row.cfg_lokace_ciselnik);
                            cmd.Parameters.AddWithValue("@cfg_lokace_dest", Row.cfg_lokace_dest);
                            cmd.Parameters.AddWithValue("@cfg_onl_dop_pal", Row.cfg_onl_dop_pal);
                            cmd.Parameters.AddWithValue("@cfg_onl_over_lokace", Row.cfg_onl_over_lokace);
                            cmd.Parameters.AddWithValue("@cfg_onl_over_lokace_dest", Row.cfg_onl_over_lokace_dest);
                            cmd.Parameters.AddWithValue("@cfg_mnozstvi_ze_zbozi", Row.cfg_mnozstvi_ze_zbozi);
                            cmd.Parameters.AddWithValue("@cfg_predvyplnit_mnozstvi", Row.cfg_predvyplnit_mnozstvi);
                            cmd.Parameters.AddWithValue("@cfg_skl_id_dest", Row.cfg_skl_id_dest);
                            cmd.Parameters.AddWithValue("@predvyplnit_skl_id_dest", Row.predvyplnit_skl_id_dest);
                            cmd.Parameters.AddWithValue("@cfg_lok_mech", Row.cfg_lok_mech);
                            cmd.Parameters.AddWithValue("@cfg_lok_mech_pohyb_type", Row.cfg_lok_mech_pohyb_type);
                            cmd.Parameters.AddWithValue("@cfg_skl_id_dest_prevzit", Row.cfg_skl_id_dest_prevzit);
                            cmd.Parameters.AddWithValue("@cfg_lokace_dest_ciselnik", Row.cfg_lokace_dest_ciselnik);
                            cmd.Parameters.AddWithValue("@predvyplnit_locncodedest", Row.predvyplnit_locncodedest);
                            cmd.Parameters.AddWithValue("@cfg_sklady", Row.cfg_sklady);
                            cmd.Parameters.AddWithValue("@cfg_onl_dop_lokace_dest", Row.cfg_onl_dop_lokace_dest);
                            cmd.Parameters.AddWithValue("@cfg_generovat_sn", Row.cfg_generovat_sn);
                            cmd.Parameters.AddWithValue("@cfg_parsovat_ck", Row.cfg_parsovat_ck);
                            cmd.Parameters.AddWithValue("@cfg_sn_na_davku", Row.cfg_sn_na_davku);
                            cmd.Parameters.AddWithValue("@cfg_lok_mech_online_pohyby", Row.cfg_lok_mech_online_pohyby);
                            cmd.Parameters.AddWithValue("@cfg_onl_palety_generovat", Row.cfg_onl_palety_generovat);
                            cmd.Parameters.AddWithValue("@cfg_tisk_palety", Row.cfg_tisk_palety);
                            cmd.Parameters.AddWithValue("@cfg_sklady_zmena", Row.cfg_sklady_zmena);
                            cmd.Parameters.AddWithValue("@cfg_delka_SN", Row.cfg_delka_SN);

                            cmd.Parameters.AddWithValue("@cfg_disp_dest", Row.cfg_disp_dest);
                            cmd.Parameters.AddWithValue("@cfg_Navrh", Row.cfg_Navrh);
                            cmd.Parameters.AddWithValue("@cfg_FIFO_FEFO_check", Row.cfg_FIFO_FEFO_check);
                            cmd.Parameters.AddWithValue("@cfg_sarze_ONOFF", Row.cfg_sarze_ONOFF);
                            cmd.Parameters.AddWithValue("@cfg_sn_ONOFF", Row.cfg_sn_ONOFF);
                            cmd.Parameters.AddWithValue("@cfg_expirace_ONOFF", Row.cfg_expirace_ONOFF);
                            cmd.Parameters.AddWithValue("@cfg_AttributeToSN_ONOFF", Row.cfg_AttributeToSN_ONOFF);

                            // Provede se update
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Commit transakce, pokud byl záznam aktualizován
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                // Rollback, pokud nedošlo k aktualizaci
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log nebo další ošetření chyby
                throw ex;
            }
        }


        // Funkce, která generuje SQL dotaz s hodnotami ve stringu
        private string GenerateSQLWithValues(Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row Row)
        {
            // Zde se vygeneruje celý SQL dotaz s hodnotami parametrů
            string query = $@"
    UPDATE [dbo].[CZMST092]
    SET 
        [doc_id] = '{Row.doc_id}',
        [doc_id2] = '{Row.doc_id2}',
        [doc_desc] = '{Row.doc_desc}',
        [doc_typ] = '{Row.doc_typ}',
        [doc_carcode] = '{Row.doc_carcode}',
        [LOCNCODE] = '{Row.LOCNCODE}',
        [cfg_odb] = '{Row.cfg_odb}',
        [cfg_str] = '{Row.cfg_str}',
        [cfg_prac] = '{Row.cfg_prac}',
        [cfg_mn2sn] = '{Row.cfg_mn2sn}',
        [cfg_disp] = '{Row.cfg_disp}',
        [cfg_palety] = '{Row.cfg_palety}',
        [cfg_paleta_id] = '{Row.cfg_paleta_id}',
        [cfg_zakazka_id] = '{Row.cfg_zakazka_id}',
        [cfg_mena_id] = '{Row.cfg_mena_id}',
        [cfg_tisk] = '{Row.cfg_tisk}',
        [cfg_prevod_sklad] = '{Row.cfg_prevod_sklad}',
        [cfg_tisk_soupis] = '{Row.cfg_tisk_soupis}',
        [SKL_ID] = '{Row.SKL_ID}',
        [cfg_lokace] = '{Row.cfg_lokace}',
        [cfg_lokace_ciselnik] = '{Row.cfg_lokace_ciselnik}',
        [cfg_lokace_dest] = '{Row.cfg_lokace_dest}',
        [cfg_onl_dop_pal] = '{Row.cfg_onl_dop_pal}',
        [cfg_onl_over_lokace] = '{Row.cfg_onl_over_lokace}',
        [cfg_onl_over_lokace_dest] = '{Row.cfg_onl_over_lokace_dest}',
        [cfg_mnozstvi_ze_zbozi] = '{Row.cfg_mnozstvi_ze_zbozi}',
        [cfg_predvyplnit_mnozstvi] = '{Row.cfg_predvyplnit_mnozstvi}',
        [cfg_skl_id_dest] = '{Row.cfg_skl_id_dest}',
        [predvyplnit_skl_id_dest] = '{Row.predvyplnit_skl_id_dest}',
        [cfg_lok_mech] = '{Row.cfg_lok_mech}',
        [cfg_lok_mech_pohyb_type] = '{Row.cfg_lok_mech_pohyb_type}',
        [cfg_skl_id_dest_prevzit] = '{Row.cfg_skl_id_dest_prevzit}',
        [cfg_lokace_dest_ciselnik] = '{Row.cfg_lokace_dest_ciselnik}',
        [predvyplnit_locncodedest] = '{Row.predvyplnit_locncodedest}',
        [cfg_sklady] = '{Row.cfg_sklady}',
        [cfg_onl_dop_lokace_dest] = '{Row.cfg_onl_dop_lokace_dest}',
        [cfg_generovat_sn] = '{Row.cfg_generovat_sn}',
        [cfg_parsovat_ck] = '{Row.cfg_parsovat_ck}',
        [cfg_sn_na_davku] = '{Row.cfg_sn_na_davku}',
        [cfg_lok_mech_online_pohyby] = '{Row.cfg_lok_mech_online_pohyby}',
        [cfg_onl_palety_generovat] = '{Row.cfg_onl_palety_generovat}',
        [cfg_tisk_palety] = '{Row.cfg_tisk_palety}',
        [cfg_sklady_zmena] = '{Row.cfg_sklady_zmena}',
        [cfg_delka_SN] = '{Row.cfg_delka_SN}',
        [cfg_disp_dest] = '{Row.cfg_disp_dest}',
        [cfg_Navrh] = '{Row.cfg_Navrh}',
        [cfg_FIFO_FEFO_check] = '{Row.cfg_FIFO_FEFO_check}',
        [cfg_sarze_ONOFF] = '{Row.cfg_sarze_ONOFF}',
        [cfg_sn_ONOFF] = '{Row.cfg_sn_ONOFF}',
        [cfg_expirace_ONOFF] = '{Row.cfg_expirace_ONOFF}',
        [cfg_AttributeToSN_ONOFF] = '{Row.cfg_AttributeToSN_ONOFF}'
    WHERE [DEX_ROW_ID] = '{Row.DEX_ROW_ID}';
    ";

            return query;
        }
        #endregion

        #endregion

        public bool Insert(Fask.Interfaces.DataSets.TypyDokladu.CZMST092Row Row)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = @"
        INSERT INTO [dbo].[CZMST092]
           ([doc_id]
           ,[doc_id2]
           ,[doc_desc]
           ,[doc_typ]
           ,[doc_carcode]
           ,[LOCNCODE]
           ,[cfg_odb]
           ,[cfg_str]
           ,[cfg_prac]
           ,[cfg_mn2sn]
           ,[cfg_disp]
           --,[cfg_disp_dest]
           ,[cfg_palety]
           ,[cfg_paleta_id]
           ,[cfg_zakazka_id]
           ,[cfg_mena_id]
           ,[cfg_tisk]
           ,[cfg_prevod_sklad]
           ,[cfg_tisk_soupis]
           ,[SKL_ID]
           ,[cfg_lokace]
           ,[cfg_lokace_ciselnik]
           ,[cfg_lokace_dest]
           ,[cfg_onl_dop_pal]
           ,[cfg_onl_over_lokace]
           ,[cfg_onl_over_lokace_dest]
           ,[cfg_mnozstvi_ze_zbozi]
           ,[cfg_predvyplnit_mnozstvi]
           ,[cfg_skl_id_dest]
           ,[predvyplnit_skl_id_dest]
           ,[cfg_lok_mech]
           ,[cfg_lok_mech_pohyb_type]
           ,[cfg_skl_id_dest_prevzit]
           ,[cfg_lokace_dest_ciselnik]
           ,[predvyplnit_locncodedest]
           ,[cfg_sklady]
           ,[cfg_onl_dop_lokace_dest]
           ,[cfg_generovat_sn]
           ,[cfg_parsovat_ck]
           ,[cfg_sn_na_davku]
           ,[cfg_lok_mech_online_pohyby]
           ,[cfg_onl_palety_generovat]
           ,[cfg_tisk_palety]
           ,[cfg_sklady_zmena]
           ,[cfg_delka_SN]
           ,[cfg_disp_dest]
           ,[cfg_Navrh]
           ,[cfg_FIFO_FEFO_check]
           ,[cfg_sarze_ONOFF]
           ,[cfg_sn_ONOFF]
           ,[cfg_expirace_ONOFF]
           ,[cfg_AttributeToSN_ONOFF])
     VALUES
           (@doc_id
           ,@doc_id2
           ,@doc_desc
           ,@doc_typ
           ,@doc_carcode
           ,@LOCNCODE
           ,@cfg_odb
           ,@cfg_str
           ,@cfg_prac
           ,@cfg_mn2sn
           ,@cfg_disp
          -- ,@cfg_disp_dest
           ,@cfg_palety
           ,@cfg_paleta_id
           ,@cfg_zakazka_id
           ,@cfg_mena_id
           ,@cfg_tisk
           ,@cfg_prevod_sklad
           ,@cfg_tisk_soupis
           ,@SKL_ID
           ,@cfg_lokace
           ,@cfg_lokace_ciselnik
           ,@cfg_lokace_dest
           ,@cfg_onl_dop_pal
           ,@cfg_onl_over_lokace
           ,@cfg_onl_over_lokace_dest
           ,@cfg_mnozstvi_ze_zbozi
           ,@cfg_predvyplnit_mnozstvi
           ,@cfg_skl_id_dest
           ,@predvyplnit_skl_id_dest
           ,@cfg_lok_mech
           ,@cfg_lok_mech_pohyb_type
           ,@cfg_skl_id_dest_prevzit
           ,@cfg_lokace_dest_ciselnik
           ,@predvyplnit_locncodedest
           ,@cfg_sklady
           ,@cfg_onl_dop_lokace_dest
           ,@cfg_generovat_sn
           ,@cfg_parsovat_ck
           ,@cfg_sn_na_davku
           ,@cfg_lok_mech_online_pohyby
           ,@cfg_onl_palety_generovat
           ,@cfg_tisk_palety
           ,@cfg_sklady_zmena
           ,@cfg_delka_SN
           ,@cfg_disp_dest
           ,@cfg_Navrh
           ,@cfg_FIFO_FEFO_check
           ,@cfg_sarze_ONOFF
           ,@cfg_sn_ONOFF
           ,@cfg_expirace_ONOFF
           ,@cfg_AttributeToSN_ONOFF);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@doc_id", Row.doc_id);
                        cmd.Parameters.AddWithValue("@doc_id2", Row.doc_id2);
                        cmd.Parameters.AddWithValue("@doc_desc", Row.doc_desc);
                        cmd.Parameters.AddWithValue("@doc_typ", Row.doc_typ);
                        cmd.Parameters.AddWithValue("@doc_carcode", Row.doc_carcode);
                        cmd.Parameters.AddWithValue("@LOCNCODE", Row.LOCNCODE);
                        cmd.Parameters.AddWithValue("@cfg_odb", Row.cfg_odb);
                        cmd.Parameters.AddWithValue("@cfg_str", Row.cfg_str);
                        cmd.Parameters.AddWithValue("@cfg_prac", Row.cfg_prac);
                        cmd.Parameters.AddWithValue("@cfg_mn2sn", Row.cfg_mn2sn);
                        cmd.Parameters.AddWithValue("@cfg_disp", Row.cfg_disp);
                        //  cmd.Parameters.AddWithValue("@cfg_disp_dest", Row.cfg_disp_dest);
                        cmd.Parameters.AddWithValue("@cfg_palety", Row.cfg_palety);
                        cmd.Parameters.AddWithValue("@cfg_paleta_id", Row.cfg_paleta_id);
                        cmd.Parameters.AddWithValue("@cfg_zakazka_id", Row.cfg_zakazka_id);
                        cmd.Parameters.AddWithValue("@cfg_mena_id", Row.cfg_mena_id);
                        cmd.Parameters.AddWithValue("@cfg_tisk", Row.cfg_tisk);
                        cmd.Parameters.AddWithValue("@cfg_prevod_sklad", Row.cfg_prevod_sklad);
                        cmd.Parameters.AddWithValue("@cfg_tisk_soupis", Row.cfg_tisk_soupis);
                        cmd.Parameters.AddWithValue("@SKL_ID", Row.SKL_ID);
                        cmd.Parameters.AddWithValue("@cfg_lokace", Row.cfg_lokace);
                        cmd.Parameters.AddWithValue("@cfg_lokace_ciselnik", Row.cfg_lokace_ciselnik);
                        cmd.Parameters.AddWithValue("@cfg_lokace_dest", Row.cfg_lokace_dest);
                        cmd.Parameters.AddWithValue("@cfg_onl_dop_pal", Row.cfg_onl_dop_pal);
                        cmd.Parameters.AddWithValue("@cfg_onl_over_lokace", Row.cfg_onl_over_lokace);
                        cmd.Parameters.AddWithValue("@cfg_onl_over_lokace_dest", Row.cfg_onl_over_lokace_dest);
                        cmd.Parameters.AddWithValue("@cfg_mnozstvi_ze_zbozi", Row.cfg_mnozstvi_ze_zbozi);
                        cmd.Parameters.AddWithValue("@cfg_predvyplnit_mnozstvi", Row.cfg_predvyplnit_mnozstvi);
                        cmd.Parameters.AddWithValue("@cfg_skl_id_dest", Row.cfg_skl_id_dest);
                        cmd.Parameters.AddWithValue("@predvyplnit_skl_id_dest", Row.predvyplnit_skl_id_dest);
                        cmd.Parameters.AddWithValue("@cfg_lok_mech", Row.cfg_lok_mech);
                        cmd.Parameters.AddWithValue("@cfg_lok_mech_pohyb_type", Row.cfg_lok_mech_pohyb_type);
                        cmd.Parameters.AddWithValue("@cfg_skl_id_dest_prevzit", Row.cfg_skl_id_dest_prevzit);
                        cmd.Parameters.AddWithValue("@cfg_lokace_dest_ciselnik", Row.cfg_lokace_dest_ciselnik);
                        cmd.Parameters.AddWithValue("@predvyplnit_locncodedest", Row.predvyplnit_locncodedest);
                        cmd.Parameters.AddWithValue("@cfg_sklady", Row.cfg_sklady);
                        cmd.Parameters.AddWithValue("@cfg_onl_dop_lokace_dest", Row.cfg_onl_dop_lokace_dest);
                        cmd.Parameters.AddWithValue("@cfg_generovat_sn", Row.cfg_generovat_sn);
                        cmd.Parameters.AddWithValue("@cfg_parsovat_ck", Row.cfg_parsovat_ck);
                        cmd.Parameters.AddWithValue("@cfg_sn_na_davku", Row.cfg_sn_na_davku);
                        cmd.Parameters.AddWithValue("@cfg_lok_mech_online_pohyby", Row.cfg_lok_mech_online_pohyby);
                        cmd.Parameters.AddWithValue("@cfg_onl_palety_generovat", Row.cfg_onl_palety_generovat);
                        cmd.Parameters.AddWithValue("@cfg_tisk_palety", Row.cfg_tisk_palety);
                        cmd.Parameters.AddWithValue("@cfg_sklady_zmena", Row.cfg_sklady_zmena);
                        cmd.Parameters.AddWithValue("@cfg_delka_SN", Row.cfg_delka_SN);

                        cmd.Parameters.AddWithValue("@cfg_disp_dest", Row.cfg_disp_dest);
                        cmd.Parameters.AddWithValue("@cfg_Navrh", Row.cfg_Navrh);
                        cmd.Parameters.AddWithValue("@cfg_FIFO_FEFO_check", Row.cfg_FIFO_FEFO_check);
                        cmd.Parameters.AddWithValue("@cfg_sarze_ONOFF", Row.cfg_sarze_ONOFF);
                        cmd.Parameters.AddWithValue("@cfg_sn_ONOFF", Row.cfg_sn_ONOFF);
                        cmd.Parameters.AddWithValue("@cfg_expirace_ONOFF", Row.cfg_expirace_ONOFF);
                        cmd.Parameters.AddWithValue("@cfg_AttributeToSN_ONOFF", Row.cfg_AttributeToSN_ONOFF);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
