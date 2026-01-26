using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Base trida pro controllery
    /// </summary>
    public class SQLite_Controller : IDisposable
    {
        #region Properties
        /// <summary>
        /// interni spojeni na databazi davky prijmu
        /// </summary>
        protected SQLiteConnection _connection;
        public SQLiteConnection Connection
        {
            get { return _connection; }
        }

        public void InitializeConnection(SQLiteConnection sqliteconnection)
        {
            if (this._connection != null)
            {
                Connection_Close();
                //SQLiteConnection.ClearPool(_connection);
            }
            this._connection = sqliteconnection;
        }

        //public void InitializeConnection(string sqliteconnectionstring)
        //{
        //    InitializeConnection(new SQLiteConnection(sqliteconnectionstring));
        //}

        #endregion

        #region c'tors
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sqliteconnection">Spojeni na databazi davky prijmu</param>
        public SQLite_Controller(SQLiteConnection sqliteconnection)
        {
            InitializeConnection(sqliteconnection);
            //AdaptersInitialize();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sqliteConnectionstring">Cesta k souboru i s souborem a připonou</param>
        public SQLite_Controller(string SQLiteFilePath_FileName)
            : this(new SQLiteConnection(SQLite_Static.SQLiteConnectionStringFormat(SQLiteFilePath_FileName)))
        {
            //AdaptersInitialize();
        }

        // inicializace adapteru...
		//protected virtual void AdaptersInitialize()
		//{            
		//}

        #endregion

        #region IDisposable Members

        //public void Dispose()
        //{
        //    if ((_connection != null))
        //    {
        //        if ((_connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
        //        {
        //            _connection.Close();
        //        }
        //        //SQLiteConnection.ClearPool(_connection);
        //        _connection.Dispose();
        //        _connection = null;
        //    }
        //}

        public virtual void Dispose()
        {
            if ((_connection != null))
            {
                //if ((_connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //{
                //    _connection.Close();
                //}
                Connection_Close();
                //SQLiteConnection.ClearPool(_connection);
                _connection.Dispose();
                _connection = null;
            }
        }

        public virtual void DisposeObject(IDisposable disposableObject)
        {
            if (disposableObject != null) disposableObject.Dispose();
        }

        #endregion

        #region Connection management
        internal protected bool Connection_Open()
        {
			try
			{
				if (_connection == null)
					return false;


				if ((_connection.State & System.Data.ConnectionState.Broken) == System.Data.ConnectionState.Broken)
				{
					_connection.Close();
					_connection.Open();
				}
				if ((_connection.State) == System.Data.ConnectionState.Closed)
				{
					// TODO : test na existenci souboru ? // connectionbulder?

                    //if (!File.Exists(this.Connection.FileName))
                    //{
                    //    throw new FileNotFoundException(this.Connection.FileName);
                    //}

					_connection.Open();
				}

				return true;
			}
			catch (FileNotFoundException exFile)
			{
                Logging.ExceptionHandler2.Handle(exFile);
                //Logging.Log.Write(exFile);
				throw exFile;
			}
			catch (Exception exSqlite)
			{
                Logging.ExceptionHandler2.Handle(exSqlite);
                //Logging.Log.Write(exSqlite);
				return false;
			}
        }

        internal protected bool Connection_Close()
        {
            try
            {
                if (_connection == null)
                    return false;

                if (_connection.State != System.Data.ConnectionState.Closed)
                    _connection.Close();

                return true;
            }
            catch (Exception exSqlite)
            {
                Logging.ExceptionHandler2.Handle(exSqlite);
                //Logging.Log.Write(exSqlite);
                return false;
            }
        }
        #endregion

        #region Shrink

        public void Shrink()
        {
            try
            {
                this.Connection_Open();
                
                using (SQLiteCommand com = this.Connection.CreateCommand())
                {
                    com.CommandText = "vacuum;";
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                this.Connection_Close();
            }
        }

        #endregion

    }
}
