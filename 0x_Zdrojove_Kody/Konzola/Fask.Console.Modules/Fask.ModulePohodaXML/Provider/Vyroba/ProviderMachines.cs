using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.Filtry;
using Fask.ModulePohodaXML.Database;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Machines.IMachines,
        Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID,
        Fask.Interfaces.Vyroba.Machines.IMachines_Fill,
        Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet,
         Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets,
        Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description,
         Fask.Interfaces.Vyroba.Machines.IMachines_IudCommand,
         Fask.Interfaces.Vyroba.Machines.IMachines_GetFilterData
    {
  
        #region IMachines_GetDataByID Members

        Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID.Machines_GetDataByID(string ID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByID(ID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dt = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {
                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Machines +
                                " WHERE id = '" + ID + "'";

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        #endregion

        #region IMachines_Fill Members

        void Fask.Interfaces.Vyroba.Machines.IMachines_Fill.Machines_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            //Globals_V1.LoadConfiguration();
            //ds.Machines.Clear();
            //Pohoda_DataSets.VyrobaDataSet tmp = new Pohoda_DataSets.VyrobaDataSet();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //ta.Fill(tmp.Machines);

            //foreach (var item in tmp.Machines)
            //{
            //    ds.Machines.ImportRow(item);
            //}

            try
            {
                Globals_V1.LoadConfiguration();
                
                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {
                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Machines;

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds.Machines);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ;
            }

        }



        #endregion



        #region stavy stroju
        public Interfaces.DataSets.Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet(Odvod_MachineStateSetListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;


                if (filtr.zaznam)
                {
                    command.CommandText = "SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSet + " as E " +
                                         " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                                         " WHERE " +
                                         " 1=1 ";
                }
                else
                {
                    command.CommandText = "SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSetHistory + " as E " +
                                            " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                                            " WHERE " +
                                            " 1=1 ";
                }


                #region dohledani podle S0-S11
                for (int i = 0; i <= 11; i++)
                {
                    var property = typeof(Odvod_MachineStateSetListFiltr).GetProperty($"S{i}");
                    var value = property?.GetValue(filtr) as int?;

                    if (value.HasValue)
                    {
                        command.CommandText += $" AND E.S{i} = @S{i} "; //--spravne
                        command.Parameters.AddWithValue($"@S{i}", value.Value);
                    }
                }

                #endregion


                if (filtr.DateModified_OD != null && filtr.DateModified_DO != null)
                {
                    command.CommandText += " AND E.DateModified between @DateModifiedOD and @DateModifiedDO";
                    command.Parameters.AddWithValue("@DateModifiedOD", filtr.DateModified_OD);
                    command.Parameters.AddWithValue("@DateModifiedDO", filtr.DateModified_DO);
                }
                else
                {
                    if (filtr.DateModified_OD != null)
                    {
                        command.CommandText += " AND E.DateModified > @DateModifiedOD";
                        command.Parameters.AddWithValue("@DateModifiedOD", filtr.DateModified_OD);
                    }
                    else if (filtr.DateModified_DO != null)
                    {
                        command.CommandText += " AND E.DateModified < @DateModifiedDO";
                        command.Parameters.AddWithValue("@DateModifiedDO", filtr.DateModified_DO);
                    }
                }



                if (!string.IsNullOrEmpty(filtr.AdamIP))
                {
                    command.CommandText += " AND E.IP = @IP ";
                    command.Parameters.AddWithValue("@IP", filtr.AdamIP);
                }

                if (!string.IsNullOrEmpty(filtr.TimeVariant))
                {
                    if (!filtr.TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.DateModified > @DateModified_TV";
                        command.Parameters.AddWithValue("@DateModified_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.CisloSluzby))
                {
                    command.CommandText += " AND MD.ID_group in (" + filtr.CisloSluzby + ")";
                }


                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND MD.Description in ('" + filtr.Description + "')";
                }


                command.CommandText += " order by " + "E.DateModified desc";


                adapter.SelectCommand = command;
                adapter.Fill(ds.MachineStateSetHistory);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> LoadStrojNameFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    //string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;
                    string query = "SELECT DISTINCT name FROM " + Fask.SQL.Constants.Common.TABLE_Machines;//new
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }

        public Interfaces.DataSets.Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(Odvod_MachineStateSetListFiltr filtr)
        {
            try
            {
                var ds = new Fask.Interfaces.DataSets.Vyroba();

                using (var connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                using (var command = new System.Data.SqlClient.SqlCommand())
                using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.FASK_MachineStateSetHistory_Analyza_Odvodu";



                    // === Datum od / do – posílej jen když má něco nastaveno ===
                    if (filtr.DateModified_OD != null)
                        command.Parameters.Add("@DateModifiedOD", SqlDbType.DateTime).Value = filtr.DateModified_OD;

                    if (filtr.DateModified_DO != null)
                        command.Parameters.Add("@DateModifiedDO", SqlDbType.DateTime).Value = filtr.DateModified_DO;

                    // === TimeVariant -> DateModified_TV ===
                    DateTime? tvDate = null;
                    if (!string.IsNullOrEmpty(filtr.TimeVariant) && !filtr.TimeVariant.Contains("unknow"))
                    {
                        var arr = filtr.TimeVariant.Split(';');
                        var timeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)
                            Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                        tvDate = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, timeVar);
                    }
                    if (tvDate.HasValue)
                        command.Parameters.Add("@DateModified_TV", SqlDbType.DateTime).Value = tvDate.Value;

                    // === CisloSluzby – jen když něco je ===
                    if (!string.IsNullOrEmpty(filtr.CisloSluzby))
                        command.Parameters.Add("@CisloSluzby", SqlDbType.NVarChar, -1).Value = filtr.CisloSluzby;

                    // === Description ===
                    if (!string.IsNullOrEmpty(filtr.Description))
                        command.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = filtr.Description;

                    // === S0–S11 – posílej jen ty, které mají hodnotu ===
                    for (int i = 0; i <= 11; i++)
                    {
                        var prop = typeof(Odvod_MachineStateSetListFiltr).GetProperty($"S{i}");
                        var valueObj = prop?.GetValue(filtr);

                        if (valueObj != null)
                        {
                            int value = (int)valueObj;              // property je int? → uvnitř int
                            string paramName = "@S" + i;
                            command.Parameters.Add(paramName, SqlDbType.Int).Value = value;
                        }
                    }

                    // === Zdroj – jen když něco je ===
                    if (!string.IsNullOrEmpty(filtr.Zdroj))
                        command.Parameters.Add("@Filtr_Zdroj", SqlDbType.NVarChar, -1).Value = filtr.Zdroj;

                    // === StrojSklad – jen když něco je ===
                    if (!string.IsNullOrEmpty(filtr.StrojSklad))
                        command.Parameters.Add("@Filtr_StrojSklad", SqlDbType.NVarChar, -1).Value = filtr.StrojSklad;

                    // === StrojLokace – jen když něco je ===
                    if (!string.IsNullOrEmpty(filtr.StrojLokace))
                        command.Parameters.Add("@Filtr_StrojLokace", SqlDbType.NVarChar, -1).Value = filtr.StrojLokace;

                    adapter.SelectCommand = command;
                    adapter.Fill(ds.MachineStateSetHistory_Analyza_Odvodu);
                    int pocet = ds.MachineStateSetHistory_Analyza_Odvodu.Count();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> LoadStrojDescriptionFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;
                    //string query = "SELECT DISTINCT name FROM " + Fask.SQL.Constants.Common.TABLE_Machines;//new
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }


        #endregion
        #region IUD

        public bool DeleteMachines(Interfaces.DataSets.Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku = Vyroba_Machines.DeleteById(MachinesRow.id, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            if (pocetUpdateRadku > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertMachines(Interfaces.DataSets.Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku = Vyroba_Machines.Update(MachinesRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            if (pocetUpdateRadku > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateMachines(Interfaces.DataSets.Vyroba.MachinesRow MachinesRow)
        {
            int pocetUpdateRadku = 0;
            pocetUpdateRadku = Vyroba_Machines.Update(MachinesRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            if (pocetUpdateRadku > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public Fask.Interfaces.DataSets.Vyroba Machines_GetFilterData(Odvod_MachineStateSetListFiltr filtr)
        {
            return Vyroba_Machines.Machines_GetFilterData(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, filtr);
        }
    }
}
