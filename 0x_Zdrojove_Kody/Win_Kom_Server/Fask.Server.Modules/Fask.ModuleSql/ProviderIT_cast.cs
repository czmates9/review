using Fask.DataSets;
using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.SQL.Constants;
using Fask.Server.Interfaces.Vyroba;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fask.ModuleSql.Classes;
using Fask.Constants;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Vyroba.Odvod_MachineStateSet;
using Fask.Interfaces.Filtry;
using Fask.WEBAPI.API_BusinessObjects;
using Fask.Interfaces.Vyroba.Odvod_TiskoveSablony;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Interfaces.IT_cast.IIT_cast,
        Fask.Interfaces.IT_cast.IIT_cast_Events_Archivace,
        Fask.Interfaces.IT_cast.IIT_cast_Production_Archivace
    {
        public int ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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

        public int Production_ArchivaceProcedura(DateTime? OD, DateTime? DO)
        {
            int pocetZaznamu = 0;

            SqlConnection conn = null;
            SqlCommand command = null;

            try
            {
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
    }
}
