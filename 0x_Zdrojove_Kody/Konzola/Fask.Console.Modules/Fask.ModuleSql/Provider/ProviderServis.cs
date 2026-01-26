using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModuleSql
{
    // TODO: pouzivat inserty, updaty, ... pomoci sql datasetu
    public partial class Provider : Fask.Interfaces.Servis.IServis
    {
        public Fask.Interfaces.DataSets.Servis GetZdroje()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;            
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Zdroj);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow GetZdrojByID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " " + 
                    "where " + 
                    "ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Zdroj);

                if (dsServis.CZMST_Servis_Zdroj.Count > 0)
                    return dsServis.CZMST_Servis_Zdroj.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis GetFiltrovaneZdroje(Fask.Interfaces.Filtry.ZdrojeListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " zdroj " +
                //    "" +
                //    "where " +
                //    "1=1 "
                //    ;
                command.CommandText =
                    "select zdroj.*, s.ID as StavID, s.Oznaceni as StavOznaceni, o.ID as OkruhID, o.Oznaceni OkruhOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " zdroj " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM + " zs on zs.ZdrojID = zdroj.ID " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " o on o.ZdrojSeznamID = zs.ID " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " s on s.ID = zs.IDStav " +
                    "where " +
                    "1=1 ";

                if (!string.IsNullOrEmpty(filtr.ZdrojID))
                {
                    //command.CommandText += "and zdroj.ID=@id ";
                    command.CommandText += "and zdroj.ID like @id + '%' ";
                    command.Parameters.AddWithValue("@id", filtr.ZdrojID);
                }

                if (!string.IsNullOrEmpty(filtr.ZdrojOznaceni))
                {
                    command.CommandText += "and zdroj.Oznaceni like @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.ZdrojOznaceni);
                }

                if (!string.IsNullOrEmpty(filtr.ZdrojBarcode))
                {
                    //command.CommandText += "and zdroj.Barcode=@barcode ";
                    command.CommandText += "and zdroj.Barcode like @barcode + '%' ";
                    command.Parameters.AddWithValue("@barcode", filtr.ZdrojBarcode);
                }

                if (!string.IsNullOrEmpty(filtr.ZdrojType))
                {
                    //command.CommandText += "and zdroj.Type=@typ ";
                    command.CommandText += "and zdroj.Type like @typ + '%' ";
                    command.Parameters.AddWithValue("@typ", filtr.ZdrojType);
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_Servis_Zdroj);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteZdroj(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;
            System.Data.SqlClient.SqlCommand command3 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [CZMST_Servis_Zdroj]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " " + 
                    "where " + 
                    "id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                // odstraneni zaznamu z tabulky [CZMST_Servis_ZdrojStav]
                deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSTAV + " " + 
                    "where " + 
                    "IDZdroj=@id";
                command2 = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command2.Parameters.AddWithValue("@id", id);

                command2.ExecuteNonQuery();

                // odstraneni zaznamu z tabulky [CZMST_Servis_ZdrojSeznam]
                deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM + " " +
                    "where " +
                    "ZdrojID=@id";
                command3 = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command3.Parameters.AddWithValue("@id", id);

                command3.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool DuplicateZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow, string newid)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                // A) ziskani originalu stavu zdroje
                // B) ziskani originalu seznamuzdroje
                var stavZdrojeOriginal = this.GetZdrojStavByZdrojID(zdrojRow.ID);
                var seznamZdrojeOriginal = this.GetZdrojSeznam().CZMST_Servis_ZdrojSeznam.Where( x => x.ZdrojID == zdrojRow.ID);

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                //1) vytvoreni zdroje
                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " " + 
                    "(ID, Oznaceni, Misto, Barcode, Type) " + 
                    "VALUES " +
                    "(@id, @oznaceni, @misto, @barcode, @type)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", newid);
                command.Parameters.AddWithValue("@oznaceni", zdrojRow.Oznaceni);
                command.Parameters.AddWithValue("@misto", zdrojRow.IsMistoNull() ? (object)DBNull.Value : zdrojRow.Misto);
                //command.Parameters.AddWithValue("@barcode", zdrojRow.IsBarcodeNull() ? (object)DBNull.Value : zdrojRow.Barcode);
                command.Parameters.AddWithValue("@barcode", newid);
                command.Parameters.AddWithValue("@type", zdrojRow.IsTypeNull() ? (object)DBNull.Value : zdrojRow.Type);
                command.ExecuteNonQuery();

                //2) vytvoreni stavu zdroje, dle originalu
                if (stavZdrojeOriginal != null)
                { // vytvoreni stavu zdroje noveho
                    insertCmd =
                        "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSTAV + " " +
                        "([IDZdroj],[IDStav],[IDCinnost],[Modified],[IDTerminal],[IDUser],[GUID],[CinnostValue],[CinnostType],[CountEntries],[ODB_ID],[OkruhID],[CinnostOznaceni],[GPS_X],[GPS_Y],[GPS_Z]) " +
                        "VALUES " +
                        "(@IDZdroj, @IDStav ,@IDCinnost, @Modified, @IDTerminal, @IDUser, @GUID ,@CinnostValue ,@CinnostType, @CountEntries, @ODB_ID, @OkruhID, @CinnostOznaceni, @GPS_X, @GPS_Y, @GPS_Z)";

                    command.CommandText = insertCmd;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IDZdroj", newid);
                    command.Parameters.AddWithValue("@IDStav", stavZdrojeOriginal.IDStav);
                    command.Parameters.AddWithValue("@IDCinnost", stavZdrojeOriginal.IsIDCinnostNull() ? (object)DBNull.Value : stavZdrojeOriginal.IDCinnost);
                    command.Parameters.AddWithValue("@Modified",  DateTime.Now);
                    command.Parameters.AddWithValue("@IDTerminal", stavZdrojeOriginal.IsIDTerminalNull() ? (object)DBNull.Value : stavZdrojeOriginal.IDTerminal);
                    command.Parameters.AddWithValue("@IDUser", stavZdrojeOriginal.IsIDUserNull() ? (object)DBNull.Value : stavZdrojeOriginal.IDUser);
                    command.Parameters.AddWithValue("@GUID", Guid.NewGuid());
                    command.Parameters.AddWithValue("@CinnostValue", stavZdrojeOriginal.IsCinnostValueNull() ? (object)DBNull.Value : stavZdrojeOriginal.CinnostValue);
                    command.Parameters.AddWithValue("@CinnostType", stavZdrojeOriginal.IsCinnostTypeNull() ? (object)DBNull.Value : stavZdrojeOriginal.CinnostType);
                    command.Parameters.AddWithValue("@CountEntries", stavZdrojeOriginal.IsCountEntriesNull() ? (object)DBNull.Value : stavZdrojeOriginal.CountEntries);                    
                    command.Parameters.AddWithValue("@ODB_ID", stavZdrojeOriginal.IsODB_IDNull() ? (object)DBNull.Value : stavZdrojeOriginal.ODB_ID);
                    command.Parameters.AddWithValue("@OkruhID", stavZdrojeOriginal.IsOkruhIDNull() ? (object)DBNull.Value : stavZdrojeOriginal.OkruhID);
                    command.Parameters.AddWithValue("@CinnostOznaceni", stavZdrojeOriginal.IsCinnostOznaceniNull() ? (object)DBNull.Value : stavZdrojeOriginal.CinnostOznaceni);
                    command.Parameters.AddWithValue("@GPS_X", stavZdrojeOriginal.IsGPS_XNull() ? (object)DBNull.Value : stavZdrojeOriginal.GPS_X);
                    command.Parameters.AddWithValue("@GPS_Y", stavZdrojeOriginal.IsGPS_YNull() ? (object)DBNull.Value : stavZdrojeOriginal.GPS_Y);
                    command.Parameters.AddWithValue("@GPS_Z", stavZdrojeOriginal.IsGPS_ZNull() ? (object)DBNull.Value : stavZdrojeOriginal.GPS_Z);

                    command.ExecuteNonQuery();
                }

                //3) zarazeni zdroje do seznamuzdroju okruhu, dle originalu
                if (seznamZdrojeOriginal.Count() > 0)
                {
                    insertCmd =
                        "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM + " " +
                        "([ID], [ZdrojID], [Poradi], [IDStav], [IDCinnost]) " +
                        "VALUES " +
                        "(@ID, @ZdrojID, @Poradi, @IDStav, @IDCinnost)";

                    command.CommandText = insertCmd;

                    foreach (var seznamZdroj in seznamZdrojeOriginal)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@ID", seznamZdroj.ID);
                        command.Parameters.AddWithValue("@ZdrojID", newid);
                        command.Parameters.AddWithValue("@Poradi", seznamZdroj.IsPoradiNull() ? (object)DBNull.Value : seznamZdroj.Poradi);
                        command.Parameters.AddWithValue("@IDStav", seznamZdroj.IsIDStavNull() ? (object)DBNull.Value : seznamZdroj.IDStav);
                        command.Parameters.AddWithValue("@IDCinnost", seznamZdroj.IsIDCinnostNull() ? (object)DBNull.Value : seznamZdroj.IDCinnost);

                        command.ExecuteNonQuery();
                    }
                }

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " " + 
                    "set Oznaceni=@oznaceni, Barcode=@barcode, type=@type, Misto=@misto " + 
                    "where id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", zdrojRow.ID);
                command.Parameters.AddWithValue("@oznaceni", zdrojRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", zdrojRow.IsBarcodeNull() ? (object)DBNull.Value : zdrojRow.Barcode);
                command.Parameters.AddWithValue("@type", zdrojRow.IsTypeNull() ? (object)DBNull.Value : zdrojRow.Type);
                command.Parameters.AddWithValue("@misto", zdrojRow.IsMistoNull() ? (object)DBNull.Value : zdrojRow.Misto);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public bool InsertZdroj(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow zdrojRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " " + 
                    "(ID, Oznaceni, Misto, Barcode, Type) " + 
                    "VALUES " +
                    "(@id, @oznaceni, @misto, @barcode, @type)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", zdrojRow.ID);
                command.Parameters.AddWithValue("@oznaceni", zdrojRow.Oznaceni);
                command.Parameters.AddWithValue("@misto", zdrojRow.IsMistoNull() ? (object)DBNull.Value : zdrojRow.Misto);
                command.Parameters.AddWithValue("@barcode", zdrojRow.IsBarcodeNull() ? (object)DBNull.Value : zdrojRow.Barcode);
                command.Parameters.AddWithValue("@type", zdrojRow.IsTypeNull() ? (object)DBNull.Value : zdrojRow.Type);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }



        public Fask.Interfaces.DataSets.Servis GetCinnosti()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Cinnost);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow GetCinnostByID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " " +
                    "where " +
                    "ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Cinnost);

                if (dsServis.CZMST_Servis_Cinnost.Count > 0)
                    return dsServis.CZMST_Servis_Cinnost.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteCinnost(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [CZMST_Servis_Cinnost]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " " +
                    "where " +
                    "id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                // odstraneni zaznamu z tabulky [CZMST_Servis_CinnostNext]
                deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " +
                    "where " +
                    "ID=@id OR IDNext=@id";
                command2 = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command2.Parameters.AddWithValue("@id", id);

                command2.ExecuteNonQuery();
                

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateCinnost(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " " +
                    "set Oznaceni=@oznaceni, Barcode=@barcode, type=@type, typevalue=@typevalue, mandatory=@mandatory, requiredLength=@requiredLength " +
                    "where id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", cinnostRow.ID);
                command.Parameters.AddWithValue("@oznaceni", cinnostRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", cinnostRow.IsBarcodeNull() ? (object)DBNull.Value : cinnostRow.Barcode);
                command.Parameters.AddWithValue("@type", cinnostRow.TYPE);
                command.Parameters.AddWithValue("@typevalue", cinnostRow.IsTYPEVALUENull() ? (object)DBNull.Value : cinnostRow.TYPEVALUE);
                command.Parameters.AddWithValue("@mandatory", cinnostRow.Mandatory);
                command.Parameters.AddWithValue("@requiredLength", cinnostRow.IsRequiredLengthNull() ? (object)DBNull.Value : cinnostRow.RequiredLength);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertCinnost(Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " " +
                    "(ID, Oznaceni, Barcode, TYPE, TYPEVALUE, Mandatory, RequiredLength) " +
                    "VALUES " +
                    "(@id, @oznaceni, @barcode, @type, @typevalue, @mandatory, @requiredLength)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", cinnostRow.ID);
                command.Parameters.AddWithValue("@oznaceni", cinnostRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", cinnostRow.IsBarcodeNull() ? (object)DBNull.Value : cinnostRow.Barcode);
                command.Parameters.AddWithValue("@type", cinnostRow.TYPE);
                command.Parameters.AddWithValue("@typevalue", cinnostRow.IsTYPEVALUENull() ? (object)DBNull.Value : cinnostRow.TYPEVALUE);
                command.Parameters.AddWithValue("@mandatory", cinnostRow.Mandatory);
                command.Parameters.AddWithValue("@requiredLength", cinnostRow.IsRequiredLengthNull() ? (object)DBNull.Value : cinnostRow.RequiredLength);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public Fask.Interfaces.DataSets.Servis GetStavy()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Stav);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow GetStavByID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " " +
                    "where " +
                    "ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Stav);

                if (dsServis.CZMST_Servis_Stav.Count > 0)
                    return dsServis.CZMST_Servis_Stav.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteStav(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [CZMST_Servis_Stav]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " " +
                    "where " +
                    "id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                // odstraneni zaznamu z tabulky [CZMST_Servis_StavNext]                
                deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " " +
                    "where " +
                    "ID=@id OR IDNext=@id";
                command2 = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command2.Parameters.AddWithValue("@id", id);

                command2.ExecuteNonQuery();
                
                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " " +
                    "set Oznaceni=@oznaceni, Barcode=@barcode, IDCinnost=@idcinnost " +
                    "where id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", stavRow.ID);
                command.Parameters.AddWithValue("@oznaceni", stavRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", stavRow.IsBarcodeNull() ? (object)DBNull.Value : stavRow.Barcode);
                command.Parameters.AddWithValue("@idcinnost", stavRow.IsIDCinnostNull() ? (object)DBNull.Value : stavRow.IDCinnost);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " " +
                    "(ID, Oznaceni, Barcode, IDCinnost) " +
                    "VALUES " +
                    "(@id, @oznaceni, @barcode, @idcinnost)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", stavRow.ID);
                command.Parameters.AddWithValue("@oznaceni", stavRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", stavRow.IsBarcodeNull() ? (object)DBNull.Value : stavRow.Barcode);
                command.Parameters.AddWithValue("@idcinnost", stavRow.IsIDCinnostNull() ? (object)DBNull.Value : stavRow.IDCinnost);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public Fask.Interfaces.DataSets.Servis GetStavyNextByStavID(string stavID)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select s.* from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " as sn " +
                    "join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " as s on sn.IDNext=s.ID " + 
                    "where " + 
                    "sn.ID=@id";
                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@id", stavID);
                adapter.Fill(dsServis.CZMST_Servis_Stav);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteStavNext(string stavID, string stavNextID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [CZMST_Servis_Zdroj]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " " +
                    "where " +
                    "id=@id and idnext=@idnext";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", stavID);
                command.Parameters.AddWithValue("@idnext", stavNextID);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public bool InsertStavNext(string stavID, string stavNextID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // kontrola, zdali puvodni stav existuje ...
                string countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " where ID=@id";
                SqlCommand countCommand = new SqlCommand(countCommandText, connection, trans);
                countCommand.Parameters.AddWithValue("@id", stavID);

                object o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                    throw new Exception("Stav '" + stavID + "' neexistuje");
                }

                // kontrola, zdali navazny stav existuje ...
                countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " where ID=@id";
                countCommand = new SqlCommand(countCommandText, connection, trans);
                countCommand.Parameters.AddWithValue("@id", stavNextID);

                o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                    throw new Exception("Návazný stav '" + stavID + "' neexistuje");
                }

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " " +
                    "(ID, IDNext) " +
                    "VALUES " +
                    "(@id, @idnext)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", stavID);
                command.Parameters.AddWithValue("@idnext", stavNextID);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertStavNext(string stavID, Fask.Interfaces.DataSets.Servis dsStavyNext)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // kontrola, zdali puvodni stav existuje ...
                string countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " where ID=@id";
                SqlCommand countCommand = new SqlCommand(countCommandText, connection, trans);
                countCommand.Parameters.AddWithValue("@id", stavID);

                object o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                    throw new Exception("Stav '" + stavID + "' neexistuje");
                }

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " " +
                    "(ID, IDNext) " +
                    "VALUES " +
                    "(@id, @idnext)";

                foreach (Fask.Interfaces.DataSets.Servis.CZMST_Servis_StavRow stavNext in dsStavyNext.CZMST_Servis_Stav)
                {
                    countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " where ID=@id";
                    countCommand = new SqlCommand(countCommandText, connection, trans);
                    countCommand.Parameters.Clear();
                    countCommand.Parameters.AddWithValue("@id", stavNext.ID);

                    o = countCommand.ExecuteScalar();
                    if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                    {
                        throw new Exception("Návazný stav '" + stavID + "' neexistuje");
                    }

                    countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAVNEXT + " where id=@id and idnext=@idnext";
                    countCommand = new SqlCommand(countCommandText, connection, trans);
                    countCommand.Parameters.Clear();
                    countCommand.Parameters.AddWithValue("@id", stavID);
                    countCommand.Parameters.AddWithValue("@idnext", stavNext.ID);

                    o = countCommand.ExecuteScalar();
                    if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                    {
                        command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", stavID);
                        command.Parameters.AddWithValue("@idnext", stavNext.ID);

                        command.ExecuteNonQuery();
                    }
                }

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public bool InsertCinnostNext(string cinnostID, string cinnostNextID, string IDValue)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " +
                    "(ID, IDNext, IDValue) " +
                    "VALUES " +
                    "(@id, @idnext, @idvalue)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", cinnostID);
                command.Parameters.AddWithValue("@idnext", string.IsNullOrEmpty(cinnostNextID) ? (object)DBNull.Value : cinnostNextID);
                command.Parameters.AddWithValue("@idvalue", string.IsNullOrEmpty(IDValue) ? (object)DBNull.Value : IDValue);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertCinnostNext(string cinnostID, string IDValue, Fask.Interfaces.DataSets.Servis dsCinnostiNext)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // kontrola, zdali puvodni cinnost existuje ...
                string countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " where ID=@id";
                SqlCommand countCommand = new SqlCommand(countCommandText, connection, trans);
                countCommand.Parameters.AddWithValue("@id", cinnostID);

                object o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                    throw new Exception("Činnost '" + cinnostID + "' neexistuje");
                }

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " +
                    "(ID, IDNext, IDValue) " +
                    "VALUES " +
                    "(@id, @idnext, @idvalue)";
                foreach (Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow cinnostNext in dsCinnostiNext.CZMST_Servis_Cinnost)
                {
                    // kontrola, zdali navazujici cinnost existuje (pouze, pokud cinnost id neni null) ...
                    if (!cinnostNext.IsIDNull())
                    {
                        countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " where ID=@id";
                        countCommand = new SqlCommand(countCommandText, connection, trans);
                        countCommand.Parameters.Clear();
                        countCommand.Parameters.AddWithValue("@id", cinnostID);

                        o = countCommand.ExecuteScalar();
                        if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                        {
                            throw new Exception("Navazující činnost '" + cinnostID + "' neexistuje");
                        }
                    }
                    countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " + 
                    "where " +
                    "id=@id and " + 
                    (cinnostNext.IsIDNull() ? "idnext is null" : "idnext=@idnext");

                    countCommand = new SqlCommand(countCommandText, connection, trans);
                    countCommand.Parameters.Clear();
                    countCommand.Parameters.AddWithValue("@id", cinnostID);
                    countCommand.Parameters.AddWithValue("@idnext", cinnostNext.IsIDNull() ? (object)DBNull.Value : cinnostNext.ID);

                    o = countCommand.ExecuteScalar();
                    if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                    {
                        command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", cinnostID);
                        command.Parameters.AddWithValue("@idnext", cinnostNext.IsIDNull() ? (object)DBNull.Value : cinnostNext.ID);
                        command.Parameters.AddWithValue("@idvalue", string.IsNullOrEmpty(IDValue) ? (object)DBNull.Value : IDValue);

                        command.ExecuteNonQuery();
                    }
                }

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool DeleteCinnostNext(string cinnostID, string cinnostNextID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [CZMST_Servis_CinnostNext]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " +
                    "where " +
                    //"id=@id and idnext=@idnext";
                    "id=@id and " +
                    (string.IsNullOrEmpty(cinnostNextID) ? "idnext is null" : "idnext=@idnext");
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", cinnostID);
                if(!string.IsNullOrEmpty(cinnostNextID))
                    command.Parameters.AddWithValue("@idnext", cinnostNextID);
                    //command.Parameters.AddWithValue("@idnext", string.IsNullOrEmpty(cinnostNextID) ? (object)DBNull.Value : cinnostNextID);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostNextRow GetCinnostNextByID(string cinnostID, string cinnostNextID)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " " + 
                    "where " + 
                    "ID=@id and IDNext=@idnext";
                command.Parameters.AddWithValue("@id", cinnostID);
                command.Parameters.AddWithValue("@idnext", cinnostNextID);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_CinnostNext);

                if (dsServis.CZMST_Servis_CinnostNext.Count > 0)
                    return dsServis.CZMST_Servis_CinnostNext.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis GetCinnostiNextByCinnostID(string cinnostID)
        {
            //System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                connection.Open();
                //trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                
                // left join, protoze cinnost muze jit do NULL (ukonceni stavu)
                command.CommandText =
                    "select s.* from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " as sn " +
                    "join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " as s on sn.IDNext=s.ID " +
                    "where " +
                    "sn.ID=@id";
                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@id", cinnostID);
                adapter.Fill(dsServis.CZMST_Servis_Cinnost);
                
                // TODO: poresit nejak normalne vyhledavani v pripade, že je IDNext == null
                string countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOSTNEXT + " where id=@id and idnext is null";
                SqlCommand countCommand = new SqlCommand(countCommandText, connection);
                countCommand.Parameters.Clear();
                countCommand.Parameters.AddWithValue("@id", cinnostID);

                object o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                }
                else
                {
                    // zaznam existuje, pridani fiktivniho do datasetu ...
                    // přidání nového záznamu pro pro přechod do NULL (do dalšího stavu)
                    Fask.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow emptyrow = dsServis.CZMST_Servis_Cinnost.NewCZMST_Servis_CinnostRow();
                    emptyrow.SetIDNull();
                    emptyrow.Oznaceni = "Přechod do dalšího stavu";
                    emptyrow.SetBarcodeNull();
                    emptyrow.TYPE = string.Empty;
                    emptyrow.SetTYPEVALUENull();
                    emptyrow.Mandatory = 0;
                    emptyrow.SetRequiredLengthNull();
                    dsServis.CZMST_Servis_Cinnost.AddCZMST_Servis_CinnostRow(emptyrow);
                }


                //if (trans != null)
                //    trans.Commit();

                return dsServis;
            }
            catch
            {
                //try
                //{
                //    if (trans != null)
                //        trans.Rollback();
                //}
                //catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public Fask.Interfaces.DataSets.Servis GetZdrojeStavy()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select zs.*, z.Oznaceni as ZdrojOznaceni, z.Misto as ZdrojMisto, s.Oznaceni as StavOznaceni, c.Oznaceni as CinnostNazev, odb.odb_desc as OdberatelOznaceni, okruh.Oznaceni as OkruhOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSTAV + " as zs " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " z on z.ID=zs.IDZdroj " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " s on s.ID=zs.IDStav " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " odb on odb.odb_id=zs.ODB_ID " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " c on c.ID=zs.IDCinnost " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " as okruh on okruh.ID = zs.OkruhID "
                    ;

                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_ZdrojStav);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow GetZdrojStavByZdrojID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSTAV + " " +
                    "where " +
                    "IDZdroj=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_ZdrojStav);

                if (dsServis.CZMST_Servis_ZdrojStav.Count > 0)
                    return dsServis.CZMST_Servis_ZdrojStav.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteZdrojStavByZdrojID(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            
            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_zs = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
                ta_zs.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_zs.MyTransaction = trans;

                ta_zs.Delete(id);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateZdrojStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow zdrojStavRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_zs = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
                ta_zs.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_zs.MyTransaction = trans;

                ta_zs.Update(
                    zdrojStavRow.IDStav,
                    zdrojStavRow.IsIDCinnostNull() ? null : zdrojStavRow.IDCinnost,
                    zdrojStavRow.Modified,
                    zdrojStavRow.IsIDTerminalNull() ? (int?)null : zdrojStavRow.IDTerminal,
                    zdrojStavRow.IsIDUserNull() ? (int?)null : zdrojStavRow.IDUser,
                    zdrojStavRow.IsGUIDNull() ? (Guid?)null : zdrojStavRow.GUID,
                    zdrojStavRow.IsCinnostValueNull() ? null : zdrojStavRow.CinnostValue,
                    zdrojStavRow.IsCinnostTypeNull() ? null : zdrojStavRow.CinnostType,
                    zdrojStavRow.IsCountEntriesNull() ? (int?)null : zdrojStavRow.CountEntries,
                    zdrojStavRow.IsODB_IDNull() ? null : zdrojStavRow.ODB_ID,
                    zdrojStavRow.IsOkruhIDNull() ? null : zdrojStavRow.OkruhID,
                    zdrojStavRow.IsCinnostOznaceniNull() ? null : zdrojStavRow.CinnostOznaceni,
                    zdrojStavRow.IsGPS_XNull() ? (double?)null : zdrojStavRow.GPS_X,
                    zdrojStavRow.IsGPS_YNull() ? (double?)null : zdrojStavRow.GPS_Y,
                    zdrojStavRow.IsGPS_ZNull() ? (int?)null : zdrojStavRow.GPS_Z,
                    zdrojStavRow.IDZdroj
                    );

                //string updateCmd =
                //    "update " + TABLE_CZMST_Servis_ZdrojStav + " " +
                //    "set IDStav=@idstav, IDCinnost=@idcinnost, Modified=@modified, IDTerminal=@idterminal, IDUser=@iduser, GUID=@guid, CinnostValue=@cinnostvalue, CinnostType=@cinnosttype, CountEntries=@countentries, ODB_ID=@odb_id, OkruhID=@okruhid " +
                //    "where IDZdroj=@idzdroj";       // TODO: nahradit GUIDem a soucasne kontrolovat, zdali neprobehla nejaka zmena ??
                //command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                //command.Parameters.AddWithValue("@idzdroj", zdrojStavRow.IDZdroj);
                //command.Parameters.AddWithValue("@idstav", zdrojStavRow.IDStav);
                //command.Parameters.AddWithValue("@idcinnost", zdrojStavRow.IsIDCinnostNull() ? (object)DBNull.Value : zdrojStavRow.IDCinnost);
                //command.Parameters.AddWithValue("@modified", zdrojStavRow.Modified);
                //command.Parameters.AddWithValue("@idterminal", zdrojStavRow.IsIDTerminalNull() ? (object)DBNull.Value : zdrojStavRow.IDTerminal);
                //command.Parameters.AddWithValue("@iduser", zdrojStavRow.IsIDUserNull() ? (object)DBNull.Value : zdrojStavRow.IDUser);
                //command.Parameters.AddWithValue("@guid", zdrojStavRow.IsGUIDNull() ? (object)DBNull.Value : zdrojStavRow.GUID);
                //command.Parameters.AddWithValue("@cinnostvalue", zdrojStavRow.IsCinnostValueNull() ? (object)DBNull.Value : zdrojStavRow.CinnostValue);
                //command.Parameters.AddWithValue("@cinnosttype", zdrojStavRow.IsCinnostTypeNull() ? (object)DBNull.Value : zdrojStavRow.CinnostType);
                //command.Parameters.AddWithValue("@countentries", zdrojStavRow.IsCountEntriesNull() ? (object)DBNull.Value : zdrojStavRow.CountEntries);
                //command.Parameters.AddWithValue("@odb_id", zdrojStavRow.IsODB_IDNull() ? (object)DBNull.Value : zdrojStavRow.ODB_ID);
                //command.Parameters.AddWithValue("@okruhid", zdrojStavRow.IsOkruhIDNull() ? (object)DBNull.Value : zdrojStavRow.OkruhID);

                //command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertZdrojStav(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojStavRow zdrojStavRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter ta_zs = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
                ta_zs.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_zs.MyTransaction = trans;

                ta_zs.Insert(
                    zdrojStavRow.IDZdroj,
                    zdrojStavRow.IDStav,
                    zdrojStavRow.IsIDCinnostNull() ? null : zdrojStavRow.IDCinnost,
                    zdrojStavRow.Modified,
                    zdrojStavRow.IsIDTerminalNull() ? (int?)null : zdrojStavRow.IDTerminal,
                    zdrojStavRow.IsIDUserNull() ? (int?)null : zdrojStavRow.IDUser,
                    zdrojStavRow.IsGUIDNull() ? (Guid?)null : zdrojStavRow.GUID,
                    zdrojStavRow.IsCinnostValueNull() ? null : zdrojStavRow.CinnostValue,
                    zdrojStavRow.IsCinnostTypeNull() ? null : zdrojStavRow.CinnostType,
                    zdrojStavRow.IsCountEntriesNull() ? (int?)null : zdrojStavRow.CountEntries,
                    zdrojStavRow.IsODB_IDNull() ? null : zdrojStavRow.ODB_ID,
                    zdrojStavRow.IsOkruhIDNull() ? null : zdrojStavRow.OkruhID,
                    zdrojStavRow.IsCinnostOznaceniNull() ? null : zdrojStavRow.CinnostOznaceni,
                    zdrojStavRow.IsGPS_XNull() ? (double?)null : zdrojStavRow.GPS_X,
                    zdrojStavRow.IsGPS_YNull() ? (double?)null : zdrojStavRow.GPS_Y,
                    zdrojStavRow.IsGPS_ZNull() ? (int?)null : zdrojStavRow.GPS_Z                    
                    );

                //string insertCmd =
                //    "insert into " + TABLE_CZMST_Servis_ZdrojStav + " " +
                //    "(IDZdroj, IDStav, IDCinnost, Modified, IDTerminal, IDUser, GUID, CinnostValue, CinnostType, CountEntries, ODB_ID, OkruhID) " +
                //    "VALUES " +
                //    "(@idzdroj, @idstav, @idcinnost, @modified, @idterminal, @iduser, @guid, @cinnostvalue, @cinnosttype, @countentries, @odb_id, @okruhid) ";

                //command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                //command.Parameters.AddWithValue("@idzdroj", zdrojStavRow.IDZdroj);
                //command.Parameters.AddWithValue("@idstav", zdrojStavRow.IDStav);
                //command.Parameters.AddWithValue("@idcinnost", zdrojStavRow.IsIDCinnostNull() ? (object)DBNull.Value : zdrojStavRow.IDCinnost);
                //command.Parameters.AddWithValue("@modified", zdrojStavRow.Modified);
                //command.Parameters.AddWithValue("@idterminal", zdrojStavRow.IsIDTerminalNull() ? (object)DBNull.Value : zdrojStavRow.IDTerminal);
                //command.Parameters.AddWithValue("@iduser", zdrojStavRow.IsIDUserNull() ? (object)DBNull.Value : zdrojStavRow.IDUser);
                //command.Parameters.AddWithValue("@guid", zdrojStavRow.IsGUIDNull() ? (object)DBNull.Value : zdrojStavRow.GUID);
                //command.Parameters.AddWithValue("@cinnostvalue", zdrojStavRow.IsCinnostValueNull() ? (object)DBNull.Value : zdrojStavRow.CinnostValue);
                //command.Parameters.AddWithValue("@cinnosttype", zdrojStavRow.IsCinnostTypeNull() ? (object)DBNull.Value : zdrojStavRow.CinnostType);
                //command.Parameters.AddWithValue("@countentries", zdrojStavRow.IsCountEntriesNull() ? (object)DBNull.Value : zdrojStavRow.CountEntries);
                //command.Parameters.AddWithValue("@odb_id", zdrojStavRow.IsODB_IDNull() ? (object)DBNull.Value : zdrojStavRow.ODB_ID);
                //command.Parameters.AddWithValue("@okruhid", zdrojStavRow.IsOkruhIDNull() ? (object)DBNull.Value : zdrojStavRow.OkruhID);

                //command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public Fask.Interfaces.DataSets.Servis GetFiltrovanyZdrojPohyb(Fask.Interfaces.Filtry.ZdrojePohybListFiltr ZdrojPohybFiltr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis ds = new Fask.Interfaces.DataSets.Servis();

            try
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter() ;

                command.CommandText = "select zp.*, zdroj.Oznaceni as ZdrojOznaceni, zdroj.Misto as ZdrojMisto, zdroj.Type as ZdrojType, stav.Oznaceni as StavOznaceni, cinnost.Oznaceni as CinnostNazev, odberatel.odb_desc as OdberatelOznaceni, uzivatel.SECONDNAME as UzivatelPrijmeni, uzivatel.FIRSTNAME as UzivatelJmeno, okruh.Oznaceni as OkruhOznaceni " +
                    "from " +
                    Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJPOHYB + " as zp " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " as uzivatel on uzivatel.USERID = zp.IDUser " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " as zdroj on zdroj.ID = zp.IDZdroj " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " as stav on stav.ID = zp.IDStav " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " as cinnost on cinnost.ID = zp.IDCinnost " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " as odberatel on odberatel.odb_id = zp.ODB_ID " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " as okruh on okruh.ID = zp.OkruhID " +
                    "where 1=1 "
                    ;

                // hledaní zdroje
                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.ZdrojID.Trim()))
                {
                    if (ZdrojPohybFiltr.rowZdrojID != null)
                    {
                        command.CommandText += "AND zp.IDZdroj=@idzdroj ";
                    }
                    else
                    {
                        command.CommandText += "AND (zdroj.Oznaceni like '%' + @idzdroj + '%' or zp.IDZdroj like '%' + @idzdroj + '%') ";
                    }
                    command.Parameters.AddWithValue("@idzdroj", ZdrojPohybFiltr.rowZdrojID != null ? ZdrojPohybFiltr.rowZdrojID.ID.Trim() : ZdrojPohybFiltr.ZdrojID.Trim());
                }

                // hledaní stavu
                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.StavID.Trim()))
                {
                    if (ZdrojPohybFiltr.rowStavID != null)
                    {
                        command.CommandText += "AND zp.IDStav=@idstav ";
                    }
                    else
                    {
                        command.CommandText += "AND (stav.Oznaceni like '%' + @idstav + '%' or zp.IDStav like '%' + @idstav + '%') ";
                    }
                    command.Parameters.AddWithValue("@idstav", ZdrojPohybFiltr.rowStavID != null ? ZdrojPohybFiltr.rowStavID.ID.Trim() : ZdrojPohybFiltr.StavID.Trim());
                }

                // hledaní cinnosoti
                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.CinnostID.Trim()))
                {
                    if (ZdrojPohybFiltr.rowCinnostID != null)
                    {
                        command.CommandText += "AND zp.IDCinnost=@idcinnost ";
                    }
                    else
                    {
                        command.CommandText += "AND (cinnost.Oznaceni like '%' + @idcinnost + '%' or zp.IDCinnost like '%' + @idcinnost + '%') ";
                    }
                    command.Parameters.AddWithValue("@idcinnost", ZdrojPohybFiltr.rowCinnostID != null ? ZdrojPohybFiltr.rowCinnostID.ID.Trim() : ZdrojPohybFiltr.CinnostID.Trim());
                }

                // hledani podle uzivatele
                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.UzivatelID))
                {
                    if (ZdrojPohybFiltr.rowUzivatel != null)
                    {
                        command.CommandText += "AND zp.IDUser=@name ";
                        command.Parameters.AddWithValue("@name", ZdrojPohybFiltr.rowUzivatel.USERID );
                    }
                    else
                    {
                        command.CommandText += "AND zp.IDUser IN ( " +
                        "select distinct USERID as id from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " " +
                        "where firstname like '%' + @name + '%' " +
                        "union " +
                        "select distinct USERID as id from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " " +
                        "where secondname like '%' + @name + '%' " +
                        "union " +
                        "select distinct USERID as id from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS+ " " +
                        "where login like '%' + @name + '%' " +
                        //"select distinct id from " + TABLE_CZMSTPWD + " " +   // nelze, id je int
                        //"where id = @name " +
                        ") ";
                        command.Parameters.AddWithValue("@name", ZdrojPohybFiltr.UzivatelID);
                    }
                }

                // hledaní odberatele
                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.OdberatelID.Trim()))
                {
                    if (ZdrojPohybFiltr.rowOdberatelID != null)
                    {
                        command.CommandText += "AND zp.ODB_ID=@odberatel ";
                    }
                    else
                    {
                        command.CommandText += "AND (odberatel.odb_desc like '%' + @odberatel + '%' or zp.ODB_ID like '%' + @odberatel + '%') ";
                    }
                    command.Parameters.AddWithValue("@odberatel", ZdrojPohybFiltr.rowOdberatelID != null ? ZdrojPohybFiltr.rowOdberatelID.odb_id.Trim() : ZdrojPohybFiltr.OdberatelID.Trim());
                }
                
                // hledání podle datumu
                if (ZdrojPohybFiltr.DatumOd != null && ZdrojPohybFiltr.DatumDo != null)
                {
                    command.CommandText += " AND Modified between @datumOd and @datumDo ";
                    command.Parameters.AddWithValue("@datumOd", ZdrojPohybFiltr.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", ZdrojPohybFiltr.DatumDo);
                }
                else
                {
                    if (ZdrojPohybFiltr.DatumOd != null)
                    {
                        command.CommandText += " AND Modified > @datumOd ";
                        command.Parameters.AddWithValue("@datumOd", ZdrojPohybFiltr.DatumOd);
                    }
                    else if (ZdrojPohybFiltr.DatumDo != null)
                    {
                        command.CommandText += " AND Modified < @datumDo ";
                        command.Parameters.AddWithValue("@datumDo", ZdrojPohybFiltr.DatumDo);
                    }
                }

                if (ZdrojPohybFiltr.VyloucitNedefinovaneHodnoty)
                {
                    command.CommandText += "AND zp.CinnostType is not null ";
                }

                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.OkruhID.Trim()))
                {
                    command.CommandText += "AND zp.OkruhID=@okruh ";
                    command.Parameters.AddWithValue("@okruh", ZdrojPohybFiltr.OkruhID.Trim());
                }

                if (ZdrojPohybFiltr.Davka.HasValue)
                {
                    command.CommandText += "AND zp.CountEntries=@davka ";
                    command.Parameters.AddWithValue("@davka", ZdrojPohybFiltr.Davka.Value);
                }

                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.Misto))
                {
                    command.CommandText += "AND zdroj.Misto=@zdrojMisto ";
                    command.Parameters.AddWithValue("@zdrojMisto", ZdrojPohybFiltr.Misto);
                }

                if (!string.IsNullOrEmpty(ZdrojPohybFiltr.Type))
                {
                    command.CommandText += "AND zdroj.Type=@zdrojType ";
                    command.Parameters.AddWithValue("@zdrojType", ZdrojPohybFiltr.Type);
                }

                command.CommandText += "order by Modified desc ";

             
                command.Connection = connection;
                adapter.SelectCommand = command;

                ds.CZMST_Servis_ZdrojPohyb.BeginLoadData();
                //naplnim data ...
                adapter.Fill(ds.CZMST_Servis_ZdrojPohyb);

                ds.CZMST_Servis_ZdrojPohyb.EndLoadData();

                // Dotahnout zdroje, ktere nejsou dotazeny v prehledu...
                if (ZdrojPohybFiltr.VsechnyZdrojeOkruhu)
                {
                    bool dohledejvsechnyzdrojeokruhu = true;

                    // 1) zjistit vsechny zdoje a okruhy => groupby zdroj, okruh
                    var grpOkruhy = ds.CZMST_Servis_ZdrojPohyb.Where(x => !x.IsOkruhIDNull()).GroupBy(x => x.OkruhID);
                    //var grpZdroje = ds.CZMST_Servis_ZdrojPohyb.GroupBy(x => x.IDZdroj);
                    var grpZdrojeDavky = ds.CZMST_Servis_ZdrojPohyb.Where(x => !x.IsCountEntriesNull()).GroupBy(x => new { x.IDZdroj, x.CountEntries });
                    var grpDavky = ds.CZMST_Servis_ZdrojPohyb.Where(x => !x.IsCountEntriesNull()).GroupBy(x => new { x.CountEntries, x.IDTerminal, x.IDUser }); // jedna davka => jeden terminal, jeden uzivatel... ???

                    // 2) dotahnout vsechny idZdroju pro nalezene okruhy                    

                    command.Parameters.Clear();
                    string tsql =
                        "select " +
                        " o.ID O_ID, o.Oznaceni O_Oznaceni, o.Barcode O_Barcode, o.ODB_ID O_ODB_ID, o.ZdrojSeznamID, odb.odb_desc O_ODB_Oznaceni " +
                        " ,zs.ZdrojID "+
                        " ,z.Oznaceni Z_Oznaceni, z.Barcode Z_Barcode, z.Misto Z_Misto "+
                        " from CZMST_Servis_Okruh o " +
                        " left join CZMST_Servis_ZdrojSeznam zs on zs.ID=o.ZdrojSeznamID " +
                        " left join CZMST_Servis_Zdroj z on z.ID=zs.ZdrojID " +
                        " left join CZMST090 odb on odb.odb_id=o.ODB_ID " +
                        " where o.ID in ({0}) ";
                    if (grpOkruhy.Count() > 0)
                    {
                        command.CommandText =
                            String.Format(
                            tsql,
                            string.Join(",", grpOkruhy.Select(x => string.Format("'{0}'", x.Key.Trim())).ToList())
                            );
                    }
                    else if (!String.IsNullOrEmpty(ZdrojPohybFiltr.OkruhID))
                    {
                        command.CommandText = String.Format(tsql, string.Format("'{0}'", ZdrojPohybFiltr.OkruhID.Trim()));
                    }
                    else
                    { // neni co, tak to nic nebudu delat ...
                        dohledejvsechnyzdrojeokruhu = false;                        
                    }

                    if (dohledejvsechnyzdrojeokruhu)
                    {
                        //command.Parameters.AddWithValue("@seznamOkruhu", seznamOkruhu);
                        SqlDataAdapter sqlda = new SqlDataAdapter(command);
                        DataSet ds2 = new DataSet();
                        sqlda.Fill(ds2);

                        ds.CZMST_Servis_ZdrojPohyb.BeginLoadData();
                        foreach (DataRow i in ds2.Tables[0].Rows)
                        {
                            //if (grpZdroje.Where( x => x.Key == (string)i["ZdrojID"]).Count()
                            //grpZdroje.DefaultIfEmpty(null).FirstOrDefault(x => x.Key == (string)i["ZdrojID"]);
                            try
                            {
                                //var zdrojExists = grpZdroje.DefaultIfEmpty(null).FirstOrDefault(x => x.Key == (string)i["ZdrojID"]);
                                foreach (var davkaRow in grpDavky)
                                {
                                    var zdrojExists = grpZdrojeDavky.DefaultIfEmpty(null).FirstOrDefault(x => x.Key.IDZdroj == (string)i["ZdrojID"] && x.Key.CountEntries == davkaRow.Key.CountEntries);
                                    #region Vytvoreni noveho zdroje
                                    if (zdrojExists == null)
                                    { // zdroj tam neni, tak pridat
                                        var nzp = ds.CZMST_Servis_ZdrojPohyb.NewCZMST_Servis_ZdrojPohybRow();
                                        nzp.IDZdroj = (string)i["ZdrojID"];
                                        nzp.CountEntries = davkaRow.Key.CountEntries;
                                        nzp.ZdrojOznaceni = (string)i["Z_Oznaceni"];
                                        nzp.OkruhID = (string)i["O_ID"];
                                        nzp.OkruhOznaceni = (string)i["O_Oznaceni"];
                                        nzp.ODB_ID = (string)i["O_ODB_ID"];
                                        nzp.OdberatelOznaceni = (string)i["O_ODB_Oznaceni"];
                                        if (!(i["Z_Misto"] is DBNull))
                                            nzp.ZdrojMisto = (string)i["Z_Misto"];

                                        // null hodnoty => nezname
                                        nzp.Modified = DateTime.MinValue; // minimalni datum => nemuze byt null, tak aspon takto se identifikuje, ze nebylo zadano ... 
                                        nzp.IDStav = "-";
                                        nzp.IDTerminal = davkaRow.Key.IDTerminal;
                                        nzp.IDUser = davkaRow.Key.IDUser;
                                        //nzp.GUID = Guid.NewGuid(); // je to proste guid ... (jeste by mohl byt dejme tomu nejaky nuly {0000000000000} ???
                                        nzp.GUID = Guid.Empty; // empty ... :) ...

                                        ds.CZMST_Servis_ZdrojPohyb.AddCZMST_Servis_ZdrojPohybRow(nzp);
                                    }
                                    #endregion
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        ds.CZMST_Servis_ZdrojPohyb.EndLoadData();
                        ds.AcceptChanges();
                    }
                }

                return ds;
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetFiltrovanyZdrojPohyb(Fask.Interfaces.Filtry.ReportSestavaFiltr reportsestavaFiltr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            //DataSet ds = new DataSet();
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();


            try
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();


                command.CommandText = "select zp.*, zdroj.Oznaceni as ZdrojOznaceni, zdroj.Misto as ZdrojMisto, zdroj.Type as ZdrojType, stav.Oznaceni as StavOznaceni, cinnost.Oznaceni as CinnostNazev, odberatel.odb_desc as OdberatelOznaceni, uzivatel.SECONDNAME as UzivatelPrijmeni, uzivatel.FIRSTNAME as UzivatelJmeno, okruh.Oznaceni as OkruhOznaceni " +
                      "from " +
                      Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJPOHYB + " as zp " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " as uzivatel on uzivatel.USERID = zp.IDUser " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " as zdroj on zdroj.ID = zp.IDZdroj " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " as stav on stav.ID = zp.IDStav " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " as cinnost on cinnost.ID = zp.IDCinnost " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " as odberatel on odberatel.odb_id = zp.ODB_ID " +
                      "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " as okruh on okruh.ID = zp.OkruhID " +
                      "where 1=1 "
                      ;

                if (!string.IsNullOrEmpty(reportsestavaFiltr.OdberatelID.Trim()))
                {
                    if (reportsestavaFiltr.rowOdberatelID != null)
                    {
                        command.CommandText += "AND zp.ODB_ID=@odberatel ";
                    }
                    else
                    {
                        command.CommandText += "AND (odberatel.odb_desc like '%' + @odberatel + '%' or zp.ODB_ID like '%' + @odberatel + '%') ";
                    }
                    command.Parameters.AddWithValue("@odberatel", reportsestavaFiltr.rowOdberatelID != null ? reportsestavaFiltr.rowOdberatelID.odb_id.Trim() : reportsestavaFiltr.OdberatelID.Trim());
                }

                if (!string.IsNullOrEmpty(reportsestavaFiltr.OkruhID.Trim()))
                {
                    command.CommandText += "AND zp.OkruhID=@okruh ";
                    command.Parameters.AddWithValue("@okruh", reportsestavaFiltr.OkruhID.Trim());
                }

                if (reportsestavaFiltr.DatumOd != null && reportsestavaFiltr.DatumDo != null)
                {
                    command.CommandText += " AND Modified between @datumOd and @datumDo ";
                    command.Parameters.AddWithValue("@datumOd", reportsestavaFiltr.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", reportsestavaFiltr.DatumDo);
                }
                else
                {
                    if (reportsestavaFiltr.DatumOd != null)
                    {
                        command.CommandText += " AND Modified > @datumOd ";
                        command.Parameters.AddWithValue("@datumOd", reportsestavaFiltr.DatumOd);
                    }
                    else if (reportsestavaFiltr.DatumDo != null)
                    {
                        command.CommandText += " AND Modified < @datumDo ";
                        command.Parameters.AddWithValue("@datumDo", reportsestavaFiltr.DatumDo);
                    }
                }


                command.CommandText += "order by Modified desc ";

                command.Connection = connection;
                adapter.SelectCommand = command;

                //ds.Tables.Add("Data");
                //DataSet dsTmp = new DataSet();
                //adapter.Fill(dsTmp);

               // ds.Tables.Clear();
               // ds.Tables.Add("Data");

                //ds.Tables["Data"].Columns.Add("Okruh");
                //ds.Tables["Data"].Columns.Add("Zdroj");
                //ds.Tables["Data"].Columns.Add("");


                adapter.Fill(dsServis.CZMST_Servis_ZdrojPohyb);
                var data = dsServis.CZMST_Servis_ZdrojPohyb.Where( x => !x.IsCinnostTypeNull());

                SQL_Datasets.Report dsreportsestava = new SQL_Datasets.Report();
                SQL_Datasets.ReportTableAdapters.ReportSestavaTableAdapter tareportsestava = new SQL_Datasets.ReportTableAdapters.ReportSestavaTableAdapter();
                tareportsestava.Connection = new SqlConnection(ConnectionString);

                if (reportsestavaFiltr.rowOdberatelID != null && (reportsestavaFiltr.OkruhID == "" || reportsestavaFiltr.OkruhID == null))
                    tareportsestava.FillByObdid(dsreportsestava.ReportSestava, reportsestavaFiltr.rowOdberatelID.odb_id);
                else if (reportsestavaFiltr.rowOdberatelID == null && (reportsestavaFiltr.OkruhID != null && reportsestavaFiltr.OkruhID.Length > 0 ))
                    tareportsestava.FillByOkruhid(dsreportsestava.ReportSestava, reportsestavaFiltr.OkruhID);
                else if (reportsestavaFiltr.rowOdberatelID != null && reportsestavaFiltr.OkruhID != null)
                    tareportsestava.FillByOdbid_okruhid(dsreportsestava.ReportSestava, reportsestavaFiltr.rowOdberatelID.odb_id, reportsestavaFiltr.OkruhID);
                else
                    tareportsestava.Fill(dsreportsestava.ReportSestava);

                #region Po mereni

                if (reportsestavaFiltr.PoMereni)
                {

                    // tady vytvarim sloupce ...

                    //IEnumerable<IGrouping<int, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow>> dataPodavkach 
                    var dataPodavkach = data.OrderBy(x => x.CountEntries).GroupBy(x => x.CountEntries);
                    //var dataPoDavkachList = dataPodavkach.ToList();
                    //dataPoDavkachList.Count();
                    //dataPoDavkachList[0].ToList();

                    foreach (IGrouping<int, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> davka in dataPodavkach)
                    {
                        //ds.Tables[0].Columns.Add(davka.Key.ToString());
                        dsreportsestava.ReportSestava.Columns.Add(davka.Key.ToString());

                        // tady plnim sloupec
                        var dataIDzdroje = davka.GroupBy(x => x.IDZdroj);
                        //var dataIDzdrojeList = dataIDzdroje.ToList();

                        foreach (IGrouping<string, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> zdroje in dataIDzdroje)
                        {
                            // tady plnim bunku
                            var zdrojeOrdered = zdroje.OrderByDescending(x => x.Modified);

                            bool zdrojVice = zdrojeOrdered.Count() > 1;
                            var zdroj = zdroje.First();

                            string zdroj_vysledna_hodnota = (zdrojVice ? "* " : string.Empty);
                            zdroj_vysledna_hodnota += CinnostValue_Hodnota(zdroj);

                            var zdrojsource = dsreportsestava.ReportSestava.Where(x => x.ZdrojID == zdroj.IDZdroj);
                            if (zdrojsource.Count() > 0)
                            {
                                zdrojsource.First()[davka.Key.ToString()] = zdroj_vysledna_hodnota;
                            }
                        }

                    }


                }
                #endregion

                #region Po mesici
                else if (reportsestavaFiltr.PoMesici)
                {
                    var dataPoMesicich = data.GroupBy(x => new { x.Modified.Month, x.Modified.Year });

                    foreach (var davka in dataPoMesicich)
                    {
                        dsreportsestava.ReportSestava.Columns.Add(davka.Key.Month.ToString()+"-" + davka.Key.Year.ToString());

                        var dataIDzdroje = davka.GroupBy(x => x.IDZdroj);

                        foreach (IGrouping<string, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> zdroje in dataIDzdroje)
                        {
                            // tady plnim bunku
                            var zdrojeOrdered = zdroje.OrderByDescending(x => x.Modified);

                            bool zdrojVice = zdrojeOrdered.Count() > 1;
                            var zdroj = zdroje.First();

                            string zdroj_vysledna_hodnota = (zdrojVice ? "* " : string.Empty);
                            zdroj_vysledna_hodnota += CinnostValue_Hodnota(zdroj);

                            var zdrojsource = dsreportsestava.ReportSestava.Where(x => x.ZdrojID == zdroj.IDZdroj);
                            if (zdrojsource.Count() > 0)
                            {
                                zdrojsource.First()[davka.Key.Month.ToString() + "-" + davka.Key.Year.ToString()] = zdroj_vysledna_hodnota;
                            }
                        }
                    }
                }
                #endregion

                #region Po roce
                else if (reportsestavaFiltr.PoRoce)
                {
                    var dataPoRocich = data.GroupBy(x => x.Modified.Year);

                    foreach (var davka in dataPoRocich)
                    {
                        dsreportsestava.ReportSestava.Columns.Add(davka.Key.ToString());

                        var dataIDzdroje = davka.GroupBy(x => x.IDZdroj);

                        foreach (IGrouping<string, Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow> zdroje in dataIDzdroje)
                        {
                            // tady plnim bunku
                            var zdrojeOrdered = zdroje.OrderByDescending(x => x.Modified);

                            bool zdrojVice = zdrojeOrdered.Count() > 1;
                            var zdroj = zdroje.First();

                            string zdroj_vysledna_hodnota = (zdrojVice ? "* " : string.Empty);
                            zdroj_vysledna_hodnota += CinnostValue_Hodnota(zdroj);

                            var zdrojsource = dsreportsestava.ReportSestava.Where(x => x.ZdrojID == zdroj.IDZdroj);
                            if (zdrojsource.Count() > 0)
                            {
                                zdrojsource.First()[davka.Key.ToString()] = zdroj_vysledna_hodnota;
                            }
                        }
                    }
                }

                #endregion

                //ds.Clear();
                //ds.Tables.Add(dsreportsestava.ReportSestava);
                //ds.Tables[0].TableName = "Data";

                //dsreportsestava.ReportSestava.TableName = "Data";
                //for
                //return ds;
                return dsreportsestava;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string CinnostValue_Hodnota(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybRow zdroj)
        {
            switch (zdroj.CinnostType)
            {
                case "D":
                    return (zdroj.IsCinnostOznaceniNull() ? String.Empty : zdroj.CinnostOznaceni);
                case "A":
                    return zdroj.IsCinnostValueNull() ? "?" : zdroj.CinnostValue;
                case "N":
                    return zdroj.IsCinnostValueNull() ? "?" : zdroj.CinnostValue; // SUM ??
                    //zdroj_vysledna_hodnota += (zdroje.Sum( x => int.Parse(x.CinnostValue))).ToString();
                case "V":
                    return zdroj.IsCinnostValueNull() ? "?" : zdroj.CinnostValue; // ??? ano/ne???
                case "P":
                    return "Fotka";
                default:
                    return zdroj.IsCinnostValueNull() ? "?" : zdroj.CinnostValue;
            }
        }

        /// <summary>
        /// Nastavuje priznak exportovano na zdrojich s uvedenym GUID.
        /// </summary>
        /// <param name="guidsExported">Exportovane radky pohybu</param>
        /// <returns></returns>
        //public bool UpdateZdrojPohybExported(List<Guid> guidsExported, DateTime? casExportu)
        public bool UpdateZdrojPohyb(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable zdrojPohybTable)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "Update " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJPOHYB + " " +
                    "Set dateExported=@dateExported " +
                    "Where GUID=@guid"
                    ;
                var dbParameter = command.Parameters.Add("@dateExported", SqlDbType.DateTime);
                dbParameter.SourceColumn = zdrojPohybTable.dateExportedColumn.ColumnName;
                dbParameter.IsNullable = true;
                dbParameter = command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier);
                dbParameter.SourceColumn = zdrojPohybTable.GUIDColumn.ColumnName;

                adapter.UpdateCommand = command;
                //adapter.ContinueUpdateOnError = true;
                int rowsUpdated = adapter.Update(zdrojPohybTable);

                // nastaveni sloupce na datum a cas ...
                return true;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis GetOkruhy()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                // OdberatelOznaceni

                command.CommandText =
                    "select okruh.*, odberatel.odb_desc as OdberatelOznaceni from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " okruh " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST090 + " as odberatel on odberatel.odb_id=okruh.odb_id " // odberatel
                    ;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Okruh);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow GetOkruhByID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " " +
                    "where " +
                    "ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Okruh);

                if (dsServis.CZMST_Servis_Okruh.Count > 0)
                    return dsServis.CZMST_Servis_Okruh.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhDataTable GetOkruhByOBDID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " " +
                    "where " +
                    "ODB_ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Okruh);

                return dsServis.CZMST_Servis_Okruh;

                //if (dsServis.CZMST_Servis_Okruh.Count > 0)
                //    return dsServis.CZMST_Servis_Okruh.First();
                //else
                //    return null;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow GetOkruhByZdrojSeznamID(string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " " +
                    "where " +
                    "ZdrojSeznamID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Okruh);

                if (dsServis.CZMST_Servis_Okruh.Count > 0)
                    return dsServis.CZMST_Servis_Okruh.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteOkruhByID(string okruhID, string seznamID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter ta_okruh = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();
                
                ta_okruh.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_okruh.MyTransaction = trans;

                ta_okruh.Delete(okruhID);

                ta_seznam.Connection = connection;
                ta_seznam.MyTransaction = trans;
                ta_seznam.DeleteByID(seznamID);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateOkruh(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruhRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter ta_okruh = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
                ta_okruh.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_okruh.MyTransaction = trans;

                //int pocet = ta_okruh.Update(okruhRow);    // nefunguje ...
                ta_okruh.Update(
                    okruhRow.Oznaceni,
                    okruhRow.IsODB_IDNull() ? null : okruhRow.ODB_ID,
                    okruhRow.IsBarcodeNull() ? null : okruhRow.Barcode,
                    okruhRow.ZdrojSeznamID,
                    okruhRow.ID
                    );

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertOkruh(Fask.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow okruhRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter ta_okruh = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();                
                ta_okruh.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_okruh.MyTransaction = trans;

                ta_okruh.Insert(
                    okruhRow.ID,
                    okruhRow.Oznaceni,
                    okruhRow.IsODB_IDNull() ? null : okruhRow.ODB_ID,
                    okruhRow.IsBarcodeNull() ? null : okruhRow.Barcode,
                    okruhRow.ZdrojSeznamID
                    );



                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Fask.Interfaces.DataSets.Servis GetZdrojSeznam()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_ZdrojSeznam);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis GetZdrojSeznamByID(string seznamID)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                // ZdrojOznaceni, ZdrojBarcode, ZdrojType, CinnostOznaceni, StavOznaceni
                // nacteni kompletniho seznamu (muze obsahovat nekolik zaznamu/ruznych zdroju)
                command.CommandText =
                    "select zs.*, z.Oznaceni as ZdrojOznaceni, z.Barcode as ZdrojBarcode, z.Type as ZdrojType, z.Misto as ZdrojMisto, s.Oznaceni as StavOznaceni, c.Oznaceni as CinnostOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM + " zs " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " as z on z.ID=zs.ZdrojID " + // Zdroj
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_STAV + " as s on s.ID=zs.IDStav " + // Stav
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " as c on c.ID=zs.IDCinnost " + // Cinnost
                    "where " +
                    "zs.ID=@id ";

                adapter.SelectCommand = command;
                command.Parameters.AddWithValue("@id", seznamID);
                adapter.Fill(dsServis.CZMST_Servis_ZdrojSeznam);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow GetZdrojSeznamByIDAndZdrojID(string seznamID, string zdrojID)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_CINNOST + " " +
                    "where " +
                    "ID=@seznamid and ZdrojID=@zdrojid";
                command.Parameters.AddWithValue("@seznamid", seznamID);
                command.Parameters.AddWithValue("@zdrojid", zdrojID);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_ZdrojSeznam);

                if (dsServis.CZMST_Servis_ZdrojSeznam.Count > 0)
                    return dsServis.CZMST_Servis_ZdrojSeznam.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteZdrojSeznamByID(string seznamID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();

                ta_seznam.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_seznam.MyTransaction = trans;
                ta_seznam.DeleteByID(seznamID);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool DeleteZdrojSeznamByIDAndZdrojID(string seznamID, string zdrojID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();

                ta_seznam.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_seznam.MyTransaction = trans;
                ta_seznam.Delete(seznamID, zdrojID);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateZdrojSeznam(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow seznamRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();
                ta_seznam.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_seznam.MyTransaction = trans;

                //ta_seznam.Update(seznamRow);      // nefunguje ...
                ta_seznam.Update(
                    seznamRow.IsPoradiNull() ? (int?)null : seznamRow.Poradi,
                    seznamRow.IsIDStavNull() ? null : seznamRow.IDStav,
                    seznamRow.IsIDCinnostNull() ? null : seznamRow.IDCinnost,
                    seznamRow.ID,
                    seznamRow.ZdrojID
                    );


                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertZdrojSeznam(Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow seznamRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();
                ta_seznam.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_seznam.MyTransaction = trans;

                ta_seznam.Insert(
                    seznamRow.ID,
                    seznamRow.ZdrojID,
                    seznamRow.IsPoradiNull() ? (int?)null : seznamRow.Poradi,
                    seznamRow.IsIDStavNull() ? null : seznamRow.IDStav,
                    seznamRow.IsIDCinnostNull() ? null : seznamRow.IDCinnost
                    );

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public bool InsertZdrojSeznam(string seznamID, Fask.Interfaces.DataSets.Servis dsZdrojSeznam)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // kontrola, zdali puvodni okruh existuje ...
                string countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_OKRUH + " where ZdrojSeznamID=@id";
                SqlCommand countCommand = new SqlCommand(countCommandText, connection, trans);
                countCommand.Parameters.AddWithValue("@id", seznamID);

                object o = countCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                {
                    throw new Exception("Seznam '" + seznamID + "' neexistuje");
                }

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter ta_seznam = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_ZdrojSeznamTableAdapter();
                ta_seznam.Connection = connection;
                ta_seznam.MyTransaction = trans;
                
                foreach (Fask.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojSeznamRow item in dsZdrojSeznam.CZMST_Servis_ZdrojSeznam)
                {
                    // kontrola, zdali zdroj existuje ...
                    countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJ + " where ID=@id";
                    countCommand = new SqlCommand(countCommandText, connection, trans);
                    countCommand.Parameters.Clear();
                    countCommand.Parameters.AddWithValue("@id", item.ZdrojID);

                    o = countCommand.ExecuteScalar();
                    if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                    {
                        throw new Exception("Zdroj '" + item.ZdrojID + "' neexistuje");
                    }

                    // 20.5.2016 PeV: uprava, aby zdroj vubec nebyl v seznamu zdroju
                    //countCommandText = "select count(*) from " + TABLE_CZMST_Servis_ZdrojSeznam + " " +
                    //"where " +
                    //"id=@id and ZdrojID=@zdrojid";
                    countCommandText = "select count(*) from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_ZDROJSEZNAM + " " +
                    "where " +
                    "ZdrojID=@zdrojid";

                    countCommand = new SqlCommand(countCommandText, connection, trans);
                    countCommand.Parameters.Clear();
                    countCommand.Parameters.AddWithValue("@zdrojid", item.ZdrojID);

                    o = countCommand.ExecuteScalar();
                    // zaznam neexistuje, pridat ...
                    if ((o == null) || (o is DBNull) || (o == DBNull.Value) || ((int)o == 0))
                    {
                        ta_seznam.Insert(
                            seznamID,
                            item.ZdrojID,
                            item.IsPoradiNull() ? (int?)null : item.Poradi,
                            item.IsIDStavNull() ? null : item.IDStav,
                            item.IsIDCinnostNull() ? null : item.IDCinnost
                            );
                    }
                    else
                        throw new Exception("Zdroj '" + item.ZdrojID + "' již je v seznamu okruhů");
                }

                if (trans != null)
                    trans.Commit();

                
                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }


        public Fask.Interfaces.DataSets.Servis GetDynamicTableDefinition()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_DYNAMIC_TABLE_DEFINITION;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Dynamic_Table_Definition);

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow GetDynamicTableDefinitionByTypeName(string typeName)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_SERVIS_DYNAMIC_TABLE_DEFINITION + " " +
                    "where " +
                    "TypeName=@id";
                command.Parameters.AddWithValue("@id", typeName);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Dynamic_Table_Definition);

                if (dsServis.CZMST_Servis_Dynamic_Table_Definition.Count > 0)
                    return dsServis.CZMST_Servis_Dynamic_Table_Definition.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteDynamicTableDefinitionByID(string fullName, string typeName)
        {
            // 1) odstraneni definicni vazby
            // 2) odstraneni tabulky

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter ta_dynTab = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter();
                ta_dynTab.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_dynTab.MyTransaction = trans;

                ta_dynTab.Delete(fullName, typeName);

                string createcmd =
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + fullName + "]') AND type in (N'U'))" +
                    " DROP TABLE [dbo].[" + fullName + "]";

                command = new SqlCommand(createcmd, connection, trans);
                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertDynamicTableDefinition(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow)
        {
            // 1) vlozeni definicni vazby
            // 2) vytvoreni tabulky

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                SQL_Datasets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter ta_dynTab = new SQL_Datasets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter();
                ta_dynTab.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_dynTab.MyTransaction = trans;

                ta_dynTab.Insert(
                    dynTableRow.FullName,
                    dynTableRow.TypeName
                    );

                string createcmd =
                    "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + dynTableRow.FullName + "]') AND type in (N'U'))" +
                    "CREATE TABLE " + dynTableRow.FullName + "( " +
                    "   [ID] [nvarchar](20) NOT NULL, " +
                    "   [Oznaceni] [nvarchar](50) NOT NULL, " +
                    "   [Barcode] [nvarchar](50) NULL, " +
                    "CONSTRAINT [PK_" + dynTableRow.FullName + "] PRIMARY KEY CLUSTERED " + 
                    "( " +
                    "   [ID] ASC" +
                    ")" +
                    ") ON [PRIMARY]";
                command = new SqlCommand(createcmd, connection, trans);
                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Fask.Interfaces.DataSets.Servis GetDynamicTable(string tablename)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();

            try
            {
                if (string.IsNullOrEmpty(tablename))
                    throw new Exception("Informace o dynamické tabulce nejsou inicializovány! (GetDynamicTable)");

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + tablename;
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Dynamic_Table);
                //adapter.Fill(dsServis.CZMST_Servis_Dynamic_Table, dsServis.CZMST_Servis_Dynamic_Table.TableName); // nefunguje ...

                return dsServis;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteDynamicTableByID(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                if (dynTableRow == null)
                    throw new Exception("Informace o dynamické tabulce nejsou inicializovány! (DeleteDynamicTableByID)");

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z z dynamicke tabulky
                string deleteCmd =
                    "delete from " + dynTableRow.FullName + " " +
                    "where " +
                    "id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertDynamicTable(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow tableRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                if (dynTableRow == null)
                    throw new Exception("Informace o dynamické tabulce nejsou inicializovány! (InsertDynamicTable)");

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd =
                    "insert into " + dynTableRow.FullName + " " +
                    "(ID, Oznaceni, Barcode) " +
                    "VALUES " +
                    "(@id, @oznaceni, @barcode)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", tableRow.ID);
                command.Parameters.AddWithValue("@oznaceni", tableRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", tableRow.IsBarcodeNull() ? (object)DBNull.Value : tableRow.Barcode);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateDynamicTable(Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionRow dynTableRow, Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow tableRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                if (dynTableRow == null)
                    throw new Exception("Informace o dynamické tabulce nejsou inicializovány! (InsertDynamicTable)");

                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string updateCmd =
                    "update " + dynTableRow.FullName + " " +
                    "set Oznaceni=@oznaceni, Barcode=@barcode " +
                    "where id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", tableRow.ID);
                command.Parameters.AddWithValue("@oznaceni", tableRow.Oznaceni);
                command.Parameters.AddWithValue("@barcode", tableRow.IsBarcodeNull() ? (object)DBNull.Value : tableRow.Barcode);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }



        public Fask.Interfaces.DataSets.Servis.CZMST_Servis_Dynamic_TableRow GetDynamicTableByID(string tablename, string id)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Interfaces.DataSets.Servis dsServis = new Fask.Interfaces.DataSets.Servis();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select * from " + tablename + " " +
                    "where " +
                    "ID=@id";
                command.Parameters.AddWithValue("@id", id);
                adapter.SelectCommand = command;
                adapter.Fill(dsServis.CZMST_Servis_Dynamic_Table);

                if (dsServis.CZMST_Servis_Dynamic_Table.Count > 0)
                    return dsServis.CZMST_Servis_Dynamic_Table.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
