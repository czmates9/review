using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SQLite;
using Fask.Logging;

namespace FASK.Palety_SSCC.SQLite.Classes
{
    public class Database
    {


        private string FilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
        public string FilePathSQLite_prd
        {
            get
            {
                return Path.Combine(FilePath, "SQLite_SSCC\\SSCC.prd");
            }
        }

        private string FilePathSQLite_script
        {
            get
            {
                return Path.Combine(FilePath, "SQLite_SSCC\\SSCC.sql");
            }
        }


        //private string FilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
        //private string FilePathSDF
        //{
        //    get
        //    {
        //        return Path.Combine(FilePath, "SQLCE_SSCC\\SSCC.sdf");
        //    }
        //}

        private string connectionString
        {
            get
            {
                return string.Format("Data Source={0};Version=3;New=False;Compress=True;", FilePathSQLite_prd);
            }
        }


        public bool Insert_CZMST_SSCC_PARAMETERS(
            int ID_SSCC,
            string DESC_SSCC,
            int LV,
            int GCP,
            int GCP_count)
        {
            //FASK_EventsTableAdapter eventsta = null;
            //System.Data.SqlServerCe.SqlCeConnection conn = null;
            SQLiteConnection conn = null;

            try
            {

                conn = new SQLiteConnection(connectionString);
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @" INSERT INTO [CZMST_SSCC_PARAMETERS] " +
                    " ( " +
                    " [ID_SSCC] ,[DESC_SSCC] ,[LV] ,[GCP] ,[GCP_count] " +
                    " ) VALUES " +
                    " ( " +
                    " @ID_SSCC, @DESC_SSCC, @LV, @GCP, @GCP_count " +
                    " ) ";

                cmd.Parameters.AddWithValue("@ID_SSCC", ID_SSCC);
                cmd.Parameters.AddWithValue("@DESC_SSCC", DESC_SSCC);
                cmd.Parameters.AddWithValue("@LV", LV);
                cmd.Parameters.AddWithValue("@GCP", GCP);
                cmd.Parameters.AddWithValue("@GCP_count", GCP_count);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public bool Insert_CZMST_SSCC_SEQUENCE(
            int seq_id,
            int sequence_count)
        {
            //FASK_EventsTableAdapter eventsta = null;
            SQLiteConnection conn = null;

            try
            {

                conn = new SQLiteConnection(connectionString);
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @" INSERT INTO [CZMST_SSCC_SEQUENCE] " +
                    " ( " +
                    " [seq_id] ,[sequence_count] " +
                    " ) VALUES " +
                    " ( " +
                    " @seq_id, @sequence_count " +
                    " ) ";

                cmd.Parameters.AddWithValue("@seq_id", seq_id);
                cmd.Parameters.AddWithValue("@sequence_count", sequence_count);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        public bool Update_CZMST_SSCC_SEQUENCE(
            int seq_id,
            int sequence_count)
        {
            //FASK_EventsTableAdapter eventsta = null;
            SQLiteConnection conn = null;

            try
            {

                conn = new SQLiteConnection(connectionString);
                SQLiteCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @" UPDATE [CZMST_SSCC_SEQUENCE] " +
                    " SET [sequence_count] = @sequence_count " +
                    " WHERE [seq_id] = @seq_id ";

                cmd.Parameters.AddWithValue("@seq_id", seq_id);
                cmd.Parameters.AddWithValue("@sequence_count", sequence_count);

                conn.Open();

                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }



        public int Fill_CZMST_SSCC_SEQUENCE_ByID(DataSets.DS_SSCC.CZMST_SSCC_SEQUENCEDataTable dataTable, int ID)
        {

            SQLiteConnection _connection = null;
            SQLiteCommand _comm = null;
            SQLiteDataAdapter _ada = null;
            int returnValue = 0;

            try
            {
                using (_connection = new SQLiteConnection(connectionString))
                {

                    if ((_connection.State & System.Data.ConnectionState.Broken) == System.Data.ConnectionState.Broken)
                    {
                        _connection.Close();
                        _connection.Open();
                    }
                    if ((_connection.State) == System.Data.ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    using (_comm = _connection.CreateCommand())
                    {
                        _comm.CommandText = "SELECT * FROM CZMST_SSCC_SEQUENCE where seq_id = '" + ID.ToString() + "'";
                        _comm.CommandType = CommandType.Text;

                        using (_ada = new SQLiteDataAdapter())
                        {
                            _ada.SelectCommand = _comm;
                             returnValue = _ada.Fill(dataTable);
                            //return returnValue;
                        }
                    }

                    if ((_connection.State & ConnectionState.Open) == ConnectionState.Open)
                    {
                        _connection.Close();
                        _connection.Dispose();
                    }
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            
            }
        }


        public int Fill_CZMST_SSCC_PARAMETERS_ByID(DataSets.DS_SSCC.CZMST_SSCC_PARAMETERSDataTable dataTable, int ID)
        {

            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(connectionString);

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT * FROM CZMST_SSCC_PARAMETERS where ID_SSCC = '" + ID.ToString() + "'";
                        using (var adapter = new SQLiteDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            int returnValue = adapter.Fill(dataTable);
                            return returnValue;
                        }
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

    }

}