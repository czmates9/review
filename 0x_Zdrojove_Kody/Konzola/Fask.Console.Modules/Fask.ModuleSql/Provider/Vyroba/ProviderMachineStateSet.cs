using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.ModuleSql
{
    public partial class Provider :
          Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet,
           Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets,
        Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description
    {
        public List<string> LoadStrojDescriptionFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    //19.1.2026 MaR zmena na nazev STROJ..
                    // Dotaz na získání unikátních hodnot ze sloupce description
                     string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;//old
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

        public List<string> LoadStrojNameFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    //19.1.2026 MaR zmena na nazev STROJ..
                    // Dotaz na získání unikátních hodnot ze sloupce description
                   // string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;//old
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


        public Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet(Odvod_MachineStateSetListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                
                if(filtr.zaznam)
                {
                    command.CommandText ="SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSet + " as E " +
                                         " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                                         " WHERE " +
                                         " 1=1 ";
                }
                else
                {
                    command.CommandText =   "SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSetHistory + " as E " +
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
                        command.CommandText += $" AND E.S{i} = @S{i} ";
                        command.Parameters.AddWithValue($"@S{i}", value.Value);
                    }
                }

                #endregion

                #region old S0-S11
                //if (filtr.S0.HasValue)
                //{
                //    command.CommandText += " AND E.S0 = @S0 ";
                //    command.Parameters.AddWithValue("@S0", filtr.S0.Value);
                //}
                //if (filtr.S1.HasValue)
                //{
                //    command.CommandText += " AND E.S1 = @S1 ";
                //    command.Parameters.AddWithValue("@S1", filtr.S1.Value);
                //}
                //if (filtr.S2.HasValue)
                //{
                //    command.CommandText += " AND E.S2 = @S2 ";
                //    command.Parameters.AddWithValue("@S2", filtr.S2.Value);
                //}
                //if (filtr.S3.HasValue)
                //{
                //    command.CommandText += " AND E.S3 = @S3 ";
                //    command.Parameters.AddWithValue("@S3", filtr.S3.Value);
                //}
                //if (filtr.S4.HasValue)
                //{
                //    command.CommandText += " AND E.S4 = @S4 ";
                //    command.Parameters.AddWithValue("@S4", filtr.S4.Value);
                //}
                //if (filtr.S5.HasValue)
                //{
                //    command.CommandText += " AND E.S5 = @S5 ";
                //    command.Parameters.AddWithValue("@S5", filtr.S5.Value);
                //}
                //if (filtr.S6.HasValue)
                //{
                //    command.CommandText += " AND E.S6 = @S6 ";
                //    command.Parameters.AddWithValue("@S6", filtr.S6.Value);
                //}
                //if (filtr.S7.HasValue)
                //{
                //    command.CommandText += " AND E.S7 = @S7 ";
                //    command.Parameters.AddWithValue("@S7", filtr.S7.Value);
                //}
                //if (filtr.S8.HasValue)
                //{
                //    command.CommandText += " AND E.S8 = @S8 ";
                //    command.Parameters.AddWithValue("@S8", filtr.S8.Value);
                //}
                //if (filtr.S9.HasValue)
                //{
                //    command.CommandText += " AND E.S9 = @S9 ";
                //    command.Parameters.AddWithValue("@S9", filtr.S9.Value);
                //}
                //if (filtr.S10.HasValue)
                //{
                //    command.CommandText += " AND E.S10 = @S10 ";
                //    command.Parameters.AddWithValue("@S10", filtr.S10.Value);
                //}
                //if (filtr.S11.HasValue)
                //{
                //    command.CommandText += " AND E.S11 = @S11 ";
                //    command.Parameters.AddWithValue("@S11", filtr.S11.Value);
                //} 
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

        public Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(Odvod_MachineStateSetListFiltr filtr)
        {
            try
            {
                var ds = new Fask.Interfaces.DataSets.Vyroba();

                using (var connection = new System.Data.SqlClient.SqlConnection(ConnectionString))
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

    }
}
