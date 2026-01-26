using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using Fask.Logging;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Servis.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Servis.IServis
    {
        private string TABLE_CZMST_Servis_Predloha = "CZMST_Servis_Predloha";
        //private string TABLE_CZMST_Servis_ZdrojStav = "CZMST_Servis_ZdrojStav";
        private string TABLE_CZMST_Servis_ZdrojPohyb = "CZMST_Servis_ZdrojPohyb";
        //private string TABLE_CZMST_Servis_Okruh = "CZMST_Servis_Okruh";
        //private string TABLE_CZMST_Servis_ZdrojSeznam = "CZMST_Servis_ZdrojSeznam";
        #region IServis Members

        /// <summary>
        /// Naplni cisleniky ... 
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public Fask.DataSets.Servis Servis_Prepare_Ciselniky(Terminal terminal, ref StatusObject so)
        {
            Globals.LoadConfiguration();
            Fask.DataSets.Servis servisDS = new Fask.DataSets.Servis();

            SqlConnection sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;

            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcomm;

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_Stav";
            sqlda.Fill(servisDS.CZMST_Servis_Stav);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_StavNext";
            sqlda.Fill(servisDS.CZMST_Servis_StavNext);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_Zdroj";
            sqlda.Fill(servisDS.CZMST_Servis_Zdroj);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_Cinnost";
            sqlda.Fill(servisDS.CZMST_Servis_Cinnost);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_CinnostNext";
            sqlda.Fill(servisDS.CZMST_Servis_CinnostNext);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_Dynamic_Table_Definition";
            sqlda.Fill(servisDS.CZMST_Servis_Dynamic_Table_Definition);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_Okruh";
            sqlda.Fill(servisDS.CZMST_Servis_Okruh);

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_ZdrojSeznam";
            sqlda.Fill(servisDS.CZMST_Servis_ZdrojSeznam);

            return servisDS;
        }

        public Fask.DataSets.Servis Servis_Prepare_Stavy(Terminal terminal, ref StatusObject so)
        {
            Globals.LoadConfiguration();
            Fask.DataSets.Servis servisDS = new Fask.DataSets.Servis();

            SqlConnection sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;

            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcomm;

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_ZdrojStav";
            sqlda.Fill(servisDS.CZMST_Servis_ZdrojStav);

            return servisDS;
        }

        public Fask.DataSets.Servis Servis_Prepare_Dynamic_Table(Terminal terminal, ref StatusObject so, string tableName)
        {
            Globals.LoadConfiguration();
            Fask.DataSets.Servis servisDS = new Fask.DataSets.Servis();

            SqlConnection sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;

            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcomm;

            sqlda.SelectCommand.CommandText = "Select * from " + tableName;
            sqlda.Fill(servisDS.CZMST_Servis_Dynamic_Table);

            return servisDS;
        }

        public StatusInfo Servis_ProcessState(ref Fask.Server.Interfaces.Servis.Zdroj zdroj)
        {
            StatusInfo si = new StatusInfo();

            // \TODO : provest v transakci
            //Pohyby
            //1) zjistit zda neni guid jiz evidovano
            //2) ulozit do pohybu
            //Stav
            //3) ulozit do stavu, pokud je datum a cas modified vetsi nez posledni v pohybech
            //Navrat
            //4) pokud neukladam do stavu, tak je ve stavu novejsi a ten musim vratit pro pripadne zmenu stavu na terminalu ... 
            Globals.LoadConfiguration();
            SqlConnection sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter ta_spohyb = new Fask.ModuleSql.SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
            ta_spohyb.Connection = sqlconn;
            sqlconn.Open();
            SqlTransaction sqltrans = sqlconn.BeginTransaction();

            ta_spohyb.Transaction = sqltrans;

            try
            {
                SQL_Datasets.Servis.CZMST_Servis_ZdrojPohybDataTable dt_spohyb = ta_spohyb.GetDataByGUID(zdroj.GUID);
                if (dt_spohyb != null && dt_spohyb.Count > 0) // existuje
                {
                    si.Description = "GUID Exists";
                }
                else
                { // neexistuje, tak ulozit ...
                    ta_spohyb.Insert(
                        zdroj.IDZdroj,
                        zdroj.IDStav,
                        zdroj.IDCinnost,
                        zdroj.Modified,
                        zdroj.IDTerminal,
                        zdroj.IDUser,
                        zdroj.GUID,
                        zdroj.CinnostValue,
                        zdroj.CinnostType,
                        zdroj.CountEntries.HasValue ? zdroj.CountEntries.Value : (int?)null,
                        zdroj.ODB_ID,
                        zdroj.OkruhID,
                        zdroj.CinnostOznaceni,
                        zdroj.GPS_X.HasValue ? zdroj.GPS_X.Value : (double?)null,
                        zdroj.GPS_Y.HasValue ? zdroj.GPS_Y.Value : (double?)null,
                        zdroj.GPS_Z.HasValue ? zdroj.GPS_Z.Value : (int?)null
                        );
                }

                //zjistit stav zdroje
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_szdroj = new Fask.ModuleSql.SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
                ta_szdroj.Connection = sqlconn;
                ta_szdroj.Transaction = sqltrans;
                SQL_Datasets.Servis.CZMST_Servis_ZdrojStavDataTable dt_szdroj = ta_szdroj.GetDataByIDZdroj(zdroj.IDZdroj);
                if (dt_szdroj != null && dt_szdroj.Count > 0)
                { // existuje => pripadny update
                    SQL_Datasets.Servis.CZMST_Servis_ZdrojStavRow r_szdroj = dt_szdroj[0];
                    // r_szdroj -> zdroj, ktery je aktualne v tabulce ZdrojStav
                    // zdroj -> zdroj z terminalu
                    if (!r_szdroj.IsGUIDNull() && r_szdroj.GUID == zdroj.GUID)
                    { // je to stejne, tak nic nemusim delat ...
                        si.Description = "Same GUID";
                    }
                    else if (r_szdroj.Modified <= zdroj.Modified)
                    { // tento je starsi (zdroj v tabulce ZdrojStav je starsi nez na terminalu)
                        si.Description = "Is newer";
                        r_szdroj.IDStav = zdroj.IDStav;
                        if (zdroj.IDCinnost != null)
                            r_szdroj.IDCinnost = zdroj.IDCinnost;
                        r_szdroj.Modified = zdroj.Modified;
                        r_szdroj.IDTerminal = zdroj.IDTerminal;
                        r_szdroj.IDUser = zdroj.IDUser;
                        r_szdroj.GUID = zdroj.GUID;
                        if (zdroj.CinnostType != null)
                            r_szdroj.CinnostType = zdroj.CinnostType;
                        if (zdroj.CinnostValue != null)
                            r_szdroj.CinnostValue = zdroj.CinnostValue;
                        if (zdroj.CountEntries.HasValue)
                            r_szdroj.CountEntries = zdroj.CountEntries.Value;
                        if (zdroj.ODB_ID != null)
                            r_szdroj.ODB_ID = zdroj.ODB_ID;
                        if (zdroj.OkruhID != null)
                            r_szdroj.OkruhID = zdroj.OkruhID;
                        if (zdroj.CinnostOznaceni != null)
                            r_szdroj.CinnostOznaceni = zdroj.CinnostOznaceni;
                        if (zdroj.GPS_X.HasValue)
                            r_szdroj.GPS_X = zdroj.GPS_X.Value;
                        if (zdroj.GPS_Y.HasValue)
                            r_szdroj.GPS_Y = zdroj.GPS_Y.Value;
                        if (zdroj.GPS_Z.HasValue)
                            r_szdroj.GPS_Z = zdroj.GPS_Z.Value;

                        ta_szdroj.Update(r_szdroj);
                    }
                    else
                    { // zmeni se zdroj a vrati se tento novejsi ...
                        si.Description = "Is old";
                        zdroj.IDStav = r_szdroj.IDStav;
                        zdroj.IDCinnost = r_szdroj.IsIDCinnostNull() ? null : r_szdroj.IDCinnost;
                        zdroj.Modified = r_szdroj.Modified;
                        zdroj.IDTerminal = r_szdroj.IsIDTerminalNull() ? (byte)0 : (byte)r_szdroj.IDTerminal;
                        zdroj.IDUser = r_szdroj.IsIDUserNull() ? 0 : r_szdroj.IDUser;
                        zdroj.GUID = r_szdroj.IsGUIDNull() ? Guid.Empty : r_szdroj.GUID;
                        zdroj.CinnostType = r_szdroj.IsCinnostTypeNull() ? null : r_szdroj.CinnostType;
                        zdroj.CinnostValue = r_szdroj.IsCinnostValueNull() ? null : r_szdroj.CinnostValue;
                        zdroj.CountEntries = r_szdroj.IsCountEntriesNull() ? (int?)null : r_szdroj.CountEntries;
                        zdroj.ODB_ID = r_szdroj.IsODB_IDNull() ? null : r_szdroj.ODB_ID;
                        zdroj.OkruhID = r_szdroj.IsOkruhIDNull() ? null : r_szdroj.OkruhID;
                        zdroj.CinnostOznaceni = r_szdroj.IsCinnostOznaceniNull() ? null : r_szdroj.CinnostOznaceni;
                        zdroj.GPS_X = r_szdroj.IsGPS_XNull() ? (double?)null : r_szdroj.GPS_X;
                        zdroj.GPS_Y = r_szdroj.IsGPS_YNull() ? (double?)null : r_szdroj.GPS_Y;
                        zdroj.GPS_Z = r_szdroj.IsGPS_ZNull() ? (int?)null : r_szdroj.GPS_Z;
                    }
                }
                else
                { // neexistuje => vlozit
                    si.Description = "Is new entry";
                    ta_szdroj.Insert(
                        zdroj.IDZdroj,
                        zdroj.IDStav,
                        zdroj.IDCinnost,
                        zdroj.Modified,
                        zdroj.IDTerminal,
                        zdroj.IDUser,
                        zdroj.GUID,
                        zdroj.CinnostValue,
                        zdroj.CinnostType,
                        zdroj.CountEntries.HasValue ? zdroj.CountEntries.Value : (int?)null,
                        zdroj.ODB_ID,
                        zdroj.OkruhID,
                        zdroj.CinnostOznaceni,
                        zdroj.GPS_X.HasValue ? zdroj.GPS_X : (double?)null,
                        zdroj.GPS_Y.HasValue ? zdroj.GPS_Y : (double?)null,
                        zdroj.GPS_Z.HasValue ? zdroj.GPS_Z : (int?)null
                        );
                }

                if (sqltrans != null)
                    sqltrans.Commit();

            }
            catch (Exception exception)
            {
                if (sqltrans != null)
                    sqltrans.Rollback();

                throw exception;
            }
            finally
            {
                if (sqlconn != null && (sqlconn.State & ConnectionState.Open) == ConnectionState.Open)
                    sqlconn.Close();
            }

            return si;
        }

        public Fask.DataSets.Servis Servis_ZdrojHistory(Terminal terminal, User user, Fask.Server.Interfaces.Servis.Zdroj zdroj, int pocetZaznamu)
        {
            Globals.LoadConfiguration();
            Fask.DataSets.Servis servisDS = new Fask.DataSets.Servis();

            SqlConnection sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;

            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcomm;

            sqlda.SelectCommand.CommandText = "Select * from CZMST_Servis_ZdrojPohyb " + 
                "WHERE IDZdroj=@zdroj";
            sqlda.SelectCommand.Parameters.AddWithValue("@zdroj", zdroj.IDZdroj);
            sqlda.Fill(servisDS.CZMST_Servis_ZdrojPohyb);

            return servisDS;
            
        }

        public void UpdateZdrojPohyb(Fask.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtZdrojStav)
        {            
            System.Data.SqlClient.SqlConnection sqlconn = null;
            System.Data.SqlClient.SqlTransaction sqltrans = null;

            try
            {
                Globals.LoadConfiguration();
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlconn.Open();
                sqltrans = sqlconn.BeginTransaction();
                UpdateZdrojPohyb(dtZdrojStav, sqlconn, sqltrans);
                
                if (sqltrans != null)
                    sqltrans.Commit();

                //tap.Update(dtProduction.Select());
            }
            catch (Exception ex)
            {
                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch
                {
                    //Fask.Logging.Log.writeErrorLog(sqltransex.Message + "\n Rollback transakce neuspel ...");
                }
                throw ex;
            }
            finally
            {
                if ((sqlconn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    sqlconn.Close();
            }
        }

        private void UpdateZdrojPohyb(Fask.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dtZdrojStav, SqlConnection sqlconn, SqlTransaction sqltrans)
        {
            Globals.LoadConfiguration();
            SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter tap = new Fask.ModuleSql.SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();

            tap.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                //sqlconn = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);
                tap.Connection = sqlconn;
                //sqlconn.Open();
                //sqltrans = sqlconn.BeginTransaction();

                //tap.Adapter.InsertCommand.Transaction = sqltrans;

                tap.Transaction = sqltrans;
                //zjistit stav zdroje
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_szdroj = new Fask.ModuleSql.SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
                ta_szdroj.Connection = sqlconn;
                ta_szdroj.Transaction = sqltrans;

                foreach (Fask.DataSets.Servis.CZMST_Servis_ZdrojPohybRow item in dtZdrojStav.Select())
                {
                    //var data = tap.GetDataByGUID(item.GUID);
                    var data = tap.CountByGUID(item.GUID);
                    // zaznam jeste nebyl vlozen
                    if (data.HasValue && data.Value == 0)
                    {
                        // kontrola ZdrojStav a pripadna uprava ...
                        SQL_Datasets.Servis.CZMST_Servis_ZdrojStavDataTable dt_szdroj = ta_szdroj.GetDataByIDZdroj(item.IDZdroj);
                        if (dt_szdroj != null && dt_szdroj.Count > 0)
                        { // existuje => pripadny update
                            SQL_Datasets.Servis.CZMST_Servis_ZdrojStavRow r_szdroj = dt_szdroj[0];  // aktualni ZdrojStav
                            //if (!r_szdroj.IsGUIDNull() && r_szdroj.GUID == zdroj.GUID)
                            //{ // je to stejne, tak nic nemusim delat ...
                            //    si.Description = "Same GUID";
                            //}
                            //else 
                            if (r_szdroj.Modified <= item.Modified)
                            { // tento (aktualni) je starsi
                                //si.Description = "Is newer";
                                r_szdroj.IDStav = item.IDStav;
                                //if (item.IDCinnost != null)
                                //    r_szdroj.IDCinnost = item.IDCinnost;
                                r_szdroj.IDCinnost = item.IsIDCinnostNull() ? null : item.IDCinnost;
                                r_szdroj.Modified = item.Modified;
                                r_szdroj.IDTerminal = item.IDTerminal;
                                r_szdroj.IDUser = item.IDUser;
                                r_szdroj.GUID = item.GUID;
                                //if (item.CinnostType != null)
                                //    r_szdroj.CinnostType = item.CinnostType;
                                //if (item.CinnostValue != null)
                                //    r_szdroj.CinnostValue = item.CinnostValue;
                                r_szdroj.CinnostType = item.IsCinnostTypeNull() ? null : item.CinnostType;
                                r_szdroj.CinnostValue = item.IsCinnostValueNull() ? null : item.CinnostValue;
                                //r_szdroj.SetCountEntriesNull()
                                if (item.IsCountEntriesNull())
                                    r_szdroj.SetCountEntriesNull();
                                else
                                    r_szdroj.CountEntries = item.CountEntries;
                                r_szdroj.ODB_ID = item.IsODB_IDNull() ? null : item.ODB_ID;
                                r_szdroj.OkruhID = item.IsOkruhIDNull() ? null : item.OkruhID;
                                r_szdroj.CinnostOznaceni = item.IsCinnostOznaceniNull() ? null : item.CinnostOznaceni;
                                if (item.IsGPS_XNull())
                                    r_szdroj.SetGPS_XNull();
                                else
                                    r_szdroj.GPS_X = item.GPS_X;

                                if(item.IsGPS_YNull())
                                    r_szdroj.SetGPS_YNull();
                                else
                                    r_szdroj.GPS_Y =  item.GPS_Y;

                                if(item.IsGPS_ZNull())
                                    r_szdroj.SetGPS_ZNull();
                                else
                                    r_szdroj.GPS_Z =  item.GPS_Z;

                                ta_szdroj.Update(r_szdroj);
                            }
                            //else  // aktualni je novejsi, neupdatovat ...
                            //{ // zmeni se zdroj a vrati se tento novejsi ...
                            //    //si.Description = "Is old";
                            //    item.IDStav = r_szdroj.IDStav;
                            //    item.IDCinnost = r_szdroj.IsIDCinnostNull() ? null : r_szdroj.IDCinnost;
                            //    item.Modified = r_szdroj.Modified;
                            //    item.IDTerminal = r_szdroj.IsIDTerminalNull() ? (byte)0 : (byte)r_szdroj.IDTerminal;
                            //    item.IDUser = r_szdroj.IsIDUserNull() ? 0 : r_szdroj.IDUser;
                            //    item.GUID = r_szdroj.IsGUIDNull() ? Guid.Empty : r_szdroj.GUID;
                            //    item.CinnostType = r_szdroj.IsCinnostTypeNull() ? null : r_szdroj.CinnostType;
                            //    item.CinnostValue = r_szdroj.IsCinnostValueNull() ? null : r_szdroj.CinnostValue;
                            //}
                        }
                        else
                        { // neexistuje => vlozit
                            //si.Description = "Is new entry";
                            ta_szdroj.Insert(
                                item.IDZdroj,
                                item.IDStav,
                                item.IsIDCinnostNull() ? null : item.IDCinnost,
                                item.Modified,
                                item.IDTerminal,
                                item.IDUser,
                                item.GUID,
                                item.IsCinnostValueNull() ? null : item.CinnostValue,
                                item.IsCinnostTypeNull() ? null : item.CinnostType,
                                item.IsCountEntriesNull() ? (int?)null : item.CountEntries,
                                item.IsODB_IDNull() ? null : item.ODB_ID,
                                item.IsOkruhIDNull() ? null : item.OkruhID,
                                item.IsCinnostOznaceniNull() ? null : item.CinnostOznaceni,
                                item.IsGPS_XNull() ? (double?)null : item.GPS_X,
                                item.IsGPS_YNull() ? (double?)null : item.GPS_Y,
                                item.IsGPS_ZNull() ? (int?)null : item.GPS_Z
                                );
                        }

                        // pridani zaznamu do ZdrojPohyb
                        tap.Update(item);
                    }
                    //else   // TODO: zaznam jiz je v DB, zalogovat ...
                    //Fask.Logging.Log.writeErrorLog("GUID " + item.GUID + " is already in database");
                }

                //if (sqltrans != null)
                //    sqltrans.Commit();

                //tap.Update(dtProduction.Select());
            }
            catch (Exception ex)
            {
                //try
                //{
                //    if (sqltrans != null)
                //        sqltrans.Rollback();
                //}
                //catch (Exception sqltransex)
                //{
                //    //Fask.Logging.Log.writeErrorLog(sqltransex.Message + "\n Rollback transakce neuspel ...");
                //}
                throw ex;
            }
            //finally
            //{
            //    if ((tap.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
            //        tap.Connection.Close();
            //}
        }

        public StatusInfo Servis_GenerateDavka(User user, string document_number, string odb_id, string okruhid)
        {
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            si.Created = DateTime.Now;
            si.Description = "Getting...";
            //SqlTransaction trans = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            SqlCommand command = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                adapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = Globals.Konfigurace.Servis[0].GenerateServiska_Action; //"fask_Servis_Generate_Serviska";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Connection = connection;

                SqlParameter parameterReturn = command.CreateParameter();
                parameterReturn.ParameterName = "@RETURN_VALUE";
                parameterReturn.DbType = DbType.Int32;
                parameterReturn.Direction = System.Data.ParameterDirection.ReturnValue;
                command.Parameters.Add(parameterReturn);

                command.Parameters.AddWithValue(Globals.Konfigurace.Servis[0].GenerateServiska_Action_P1, document_number);
                command.Parameters.AddWithValue(Globals.Konfigurace.Servis[0].GenerateServiska_Action_P2, odb_id);
                command.Parameters.AddWithValue(Globals.Konfigurace.Servis[0].GenerateServiska_Action_P3, okruhid);
                command.Parameters.AddWithValue(Globals.Konfigurace.Servis[0].GenerateServiska_Action_P4, user.ID);    // vyzkouset !!

                // output CountEntries
                SqlParameter outcountentries = command.CreateParameter();
                outcountentries.ParameterName = Globals.Konfigurace.Servis[0].GenerateServiska_Action_P5;   // CountEntries
                outcountentries.DbType = DbType.Int32;
                //parameterText.Value = string.Empty;
                //outmessage.Size = 100;
                outcountentries.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(outcountentries);

                // output message
                SqlParameter outmessage = command.CreateParameter();
                outmessage.ParameterName = Globals.Konfigurace.Servis[0].GenerateServiska_Action_P6;   // message
                outmessage.DbType = DbType.String;
                //parameterText.Value = string.Empty;
                outmessage.Size = 100;
                outmessage.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(outmessage);
                
                command.ExecuteNonQuery();

                // 0 = OK
                // 1.. = Chyba
                int result = (int)parameterReturn.Value;

                // nastala chyba
                if (result != 0)
                {
                    if (outmessage.Value == DBNull.Value)
                        throw new Exception("Neznámá chyba při generování dávky.");
                    else
                        throw new Exception((string)outmessage.Value);
                }

                //si.ID = maxCountEntries;
                si.ID = (int)outcountentries.Value;
                si.Description = "OK";                
            }
            catch
            {
                // vyhozeni vyjimky (aby se dostala na terminal)
                //si.ID = -10;
                //si.Description = "Exception";
                //si.InnerException = ex;
                throw;
            }
            finally
            {
                if (connection != null && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }

            return si;
        }

        public bool Servis_GetDavkaReceived(Davka davka, Terminal terminal)
        {
            Globals.LoadConfiguration();
            string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_Servis_Predloha + " where CountEntries=" + davka.ID.Value + " AND (Rozpracovano<=0 OR Rozpracovano=" + terminal.ID + ")";
            string update = "Update " + TABLE_CZMST_Servis_Predloha + " set Rozpracovano=" + terminal.ID + " where countentries=" + davka.ID.Value;
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            System.Data.SqlClient.SqlCommand commAllowed = new System.Data.SqlClient.SqlCommand(selectCount, connection);
            System.Data.SqlClient.SqlCommand commUpdate = new System.Data.SqlClient.SqlCommand(update, connection);
            System.Data.SqlClient.SqlTransaction trans = null;

            int res = 0;
            try
            {
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                commAllowed.Transaction = trans;
                object r = commAllowed.ExecuteScalar();
                if (r == null)
                    throw new Exception("Dávka nenalezena.");
                if (r is int && ((int)r) <= 0)
                    throw new Exception("Dávka se již zpracovává.");

                commUpdate.Transaction = trans;
                res = commUpdate.ExecuteNonQuery();
                if (trans != null)
                    trans.Commit();

            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Servis_GetDavkaReceived : Dávka: " + davka.ID.Value + ", Terminál ID:" + terminal.ID + ")");
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                if (trans != null)
                    trans.Rollback();

                //throw ex;
                return false;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return (res > 0);
        }

        public Fask.DataSets.Servis Servis_GetDavka(Davka davka, Terminal terminal)
        {
            Globals.LoadConfiguration();
            // \TODO: generovat data davky pomoci procedury ??
            string select = "SELECT * FROM " + TABLE_CZMST_Servis_Predloha + " where CountEntries=" + davka.ID;
            Fask.DataSets.Servis servis = new Fask.DataSets.Servis();

            // naplneni predlohy
            using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
            {
                sql.Fill(servis, servis.CZMST_Servis_Predloha.TableName);
            }

            if (servis.CZMST_Servis_Predloha.Count < 1)
                throw new Exception("Dávka '" + davka.ID + "' neexistuje na serveru.");

            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                adapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = Globals.Konfigurace.Servis[0].GenerateServiska_Data_Action; //"fask_Servis_Generate_Serviska";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Connection = connection;

                command.Parameters.AddWithValue(Globals.Konfigurace.Servis[0].GenerateServiska_Data_Action_P1, davka.ID);

                adapter.SelectCommand = command;
                adapter.Fill(servis, servis.CZMST_Servis_ZdrojStav.TableName);
            }
            catch (Exception ex)
            {
                string pom = string.Empty;
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
            // naplneni tabulky ZdrojStav
            //select = "select zdrojstav.IDZdroj, zdrojstav.IDStav, zdrojstav.IDCinnost, zdrojstav.Modified, zdrojstav.IDTerminal, zdrojstav.IDUser, zdrojstav.GUID, zdrojstav.CinnostValue, zdrojstav.CinnostType, predloha.CountEntries, okruh.ODB_ID, predloha.OkruhID " + 
            //    "from " + TABLE_CZMST_Servis_Predloha + " as predloha " + 
            //    "join " + TABLE_CZMST_Servis_Okruh + " as okruh on okruh.ID = predloha.OkruhID " + 
            //    "join " + TABLE_CZMST_Servis_ZdrojSeznam + " as seznam on seznam.ID = okruh.ZdrojSeznamID " + 
            //    "join " + TABLE_CZMST_Servis_ZdrojStav + " as zdrojstav on zdrojstav.IDZdroj=seznam.ZdrojID " + 
            //    "where " +
            //    "predloha.CountEntries=" + davka.ID
            //    ;

            //using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Properties.Settings.Default.SqlProviderConnection))
            //{
            //    sql.Fill(servis, servis.CZMST_Servis_ZdrojStav.TableName);
            //}

            return servis;
        }

        public StatusObject Servis_Process(Davka davka, Terminal terminal, Fask.DataSets.Servis servisdata, Fask.Server.Interfaces.Servis.ProcessState processServisState)
        {
            Globals.LoadConfiguration();
            // pokud se davka neotevrela, je tabulka CZMST_PEH prazdna, dojde k vygenerovani noveho GUIDu
            string guidDavka = string.Empty;
            guidDavka = Guid.NewGuid().ToString();            

            string filePath = Path.Combine(Globals.Konfigurace.Servis[0].PathStateDataFile, guidDavka);

            StatusObject so = new StatusObject(filePath);

            //zjistit zda soubor s danym guid existuje
            if (File.Exists(filePath))
            { //soubor jiz existuje
                so = StatusObject.Load(filePath);
                if (!so.Exception)
                    return so;
            }
            //pokud ne, projit normalne dal
            System.Data.SqlClient.SqlConnection sqlconn = null;
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            bool uvolnitdavku = processServisState == Fask.Server.Interfaces.Servis.ProcessState.Uvolnit;

            try
            {
                sqlconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                so.Write("vytvareni connection");
                sqlconn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");
					// \TODO: poresit uvolnovani davky ?? ... data se odesilaji neustale
                    //throw new Exception("Dávku není možné uvolnit.");
                    string updateuvolnit = "Update " + TABLE_CZMST_Servis_Predloha + " set Rozpracovano=0 where CountEntries=" + davka.ID;
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, sqlconn, sqltrans);
                    int rows = comm.ExecuteNonQuery();
                }
                else //zapsat davku
                {
                    so.Write("zapsat davku");
                    so.Write("Update databaze");
                                        
                    sqltrans = sqlconn.BeginTransaction();
                    string updatePE = "Update " + TABLE_CZMST_Servis_Predloha + " set Rozpracovano=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatePE, sqlconn, sqltrans);

                    UpdateZdrojPohyb(servisdata.CZMST_Servis_ZdrojPohyb, sqlconn, sqltrans);

                    int rows = command.ExecuteNonQuery();

                    if (sqltrans != null)
                        sqltrans.Commit();

                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch (Exception sqltransex)
                {
					Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, sqltransex);
                }
                throw ex;
                //so.Exception = true;
                //so.Write(ex.Message);
            }
            finally
            {
                if ((sqlconn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    sqlconn.Close();
            }

            #region Action after data processed
            if (Globals.Konfigurace.Servis[0].AfterDataProcessed_Action_Asynchronous)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Servis_AfterProcessedActionAsync));
                thread.Start(davka);
            }
            else
            {
                if (!Servis_AfterProcessedAction(davka))
                {
                    so.Exception = true;
                    so.Write("chyba");
                    return so;
                }
            }
            #endregion

            so.SetOK();

            return so;
        }

        public void Servis_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Servis_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Servis_AfterProcessedAction(Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();
                string aDP_Action = Globals.Konfigurace.Servis[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals.Konfigurace.Servis[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals.Konfigurace.Servis[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_CZMST_Servis_ZdrojPohyb, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Server.Interfaces.DataSets.ServisDavky Servis_GetDavky(Terminal terminal)
        {
            try
            {
                Globals.LoadConfiguration();
                //select a.CountEntries, a.DOCUMENT_NUMBER, COUNT(s.ID) as CntItems, a.OkruhID 
                //from CZMST_Servis_Predloha a
                //    INNER JOIN CZMST_Servis_Okruh o on o.ID = a.OkruhID
                //    INNER JOIN CZMST_Servis_ZdrojSeznam s on s.ID = o.ZdrojSeznamID
                //where 
                //    a.Rozpracovano <=0 OR a.Rozpracovano=1
                //group by a.CountEntries, a.DOCUMENT_NUMBER, s.ID, a.OkruhID
                //order by a.CountEntries

                string select =
                    "select a.CountEntries, a.DOCUMENT_NUMBER, COUNT(s.ID) as CntItems, '' as OkruhID, a.ODB_ID as ODB_ID, '' as OkruhOznaceni, odberatel.odb_desc as OdbOznaceni " +
                    "from " + TABLE_CZMST_Servis_Predloha + " a " +
                    "   INNER JOIN CZMST_Servis_Okruh o on o.ODB_ID = a.ODB_ID  " +
                    "   INNER JOIN CZMST_Servis_ZdrojSeznam s on s.ID = o.ZdrojSeznamID " +
                    "   LEFT JOIN CZMST090 odberatel on odberatel.odb_id = a.ODB_ID " +
                    "where " +
                    "   a.Rozpracovano <=0 OR a.Rozpracovano=" + terminal.ID + " " +
                    "group by a.CountEntries, a.DOCUMENT_NUMBER, a.ODB_ID, odberatel.odb_desc " +
                    "order by a.CountEntries ";
                //string select =
                //    "select a.CountEntries, a.DOCUMENT_NUMBER, COUNT(s.ID) as CntItems, a.OkruhID, o.ODB_ID as ODB_ID, o.Oznaceni as OkruhOznaceni, odberatel.odb_desc as OdbOznaceni " +
                //    "from " + TABLE_CZMST_Servis_Predloha + " a " +
                //    "   INNER JOIN CZMST_Servis_Okruh o on o.ID = a.OkruhID " +
                //    "   INNER JOIN CZMST_Servis_ZdrojSeznam s on s.ID = o.ZdrojSeznamID " +
                //    "   LEFT JOIN CZMST090 odberatel on odberatel.odb_id = o.odb_id " +
                //    "where " +
                //    "   a.Rozpracovano <=0 OR a.Rozpracovano=" + terminal.ID + " " +
                //    "group by a.CountEntries, a.DOCUMENT_NUMBER, s.ID, a.OkruhID, o.ODB_ID, o.Oznaceni, odberatel.odb_desc " +
                //    "order by a.CountEntries ";

                Fask.Server.Interfaces.DataSets.ServisDavky volnedavky = new Fask.Server.Interfaces.DataSets.ServisDavky();

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    sql.Fill(volnedavky, volnedavky.Hlavicky.TableName);
                }

                return volnedavky;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

        #region IServis Members


        public StatusObject Servis_Storno_Davka(Davka davka, User user, Terminal terminal, string password)
        {
            Globals.LoadConfiguration();
            StatusObject so = new StatusObject();
            
            if (password != Globals.Konfigurace.Servis[0].HesloStornoServiska)
            {
                so.StatusText = "Zadané heslo je špatně!";
                so.Exception = true;
                return so;
            }

            SQL_Datasets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter _ta_predloha = new Fask.ModuleSql.SQL_Datasets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter();
            
            try
            {
                _ta_predloha.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                
                _ta_predloha.UpdateRozpracovanoByCountEntries(100, davka.ID.Value);

                so.StatusText = "OK";
                return so;
            }
            finally
            {
                if (_ta_predloha != null && _ta_predloha.Connection.State == System.Data.ConnectionState.Open)
                    _ta_predloha.Connection.Close();
            }

            //return so;
        }

        #endregion
    }
}
