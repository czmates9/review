using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql
{
    public partial class Provider :
          Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony,
           Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony
    {
        public Vyroba TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(Odvod_TiskoveSablonyFiltr filtr)
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

                command.CommandText =
                        "SELECT E.* FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + " as E " +
                        " WHERE " +
                        " 1=1 ";

                //if (!string.IsNullOrEmpty(filtr.nazev_okna))
                //{
                //    command.CommandText += " AND E.nazev_okna = '@nazev_okna' ";
                //    command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                #region MaR 13.11.2024 old vyhledavani
                //if (!string.IsNullOrEmpty(filtr.nazev_okna))
                //{
                //    command.CommandText += " AND E.nazev_okna = '" + filtr.nazev_okna + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.loginid))
                //{
                //    command.CommandText += " AND E.loginid = '" + filtr.loginid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.machineid))
                //{
                //    command.CommandText += " AND E.machineid = '" + filtr.machineid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                //if (!string.IsNullOrEmpty(filtr.typ))
                //{
                //    command.CommandText += " AND E.typ = '" + filtr.typ + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //} 
                #endregion

                #region new 13.11.2024

                if (!string.IsNullOrEmpty(filtr.nazev_okna))
                {
                    command.CommandText += " AND E.nazev_okna = '" + filtr.nazev_okna + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.typ))
                {
                    command.CommandText += " AND E.typ = '" + filtr.typ + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                //command.CommandText += " AND (E.ord = " + filtr.ord + ") ";
                command.CommandText += " AND (E.loginid = '" + filtr.loginid + "' or E.loginid is null )";


                //if (!string.IsNullOrEmpty(filtr.loginid))
                //{
                //    command.CommandText += " AND E.loginid = '" + filtr.loginid + "' ";
                //    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}


                if (!string.IsNullOrEmpty(filtr.machineid))
                {
                    command.CommandText += " AND E.machineid = '" + filtr.machineid + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                command.CommandText += " order by " + "E.ord, E.loginid asc";
                #endregion




                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_FORMULARE);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metoda vracející cestu k šabloně ZPL na základě zadaného filtru
        /// </summary>
        /// <param name="filtr">Filtrované parametry pro získání cesty</param>
        /// <returns>Řetězec obsahující cestu k šabloně nebo prázdný řetězec, pokud není nalezena</returns>
        public string TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(Odvod_TiskoveSablonyFiltr filtr)
        {
            //Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataReader reader = null;
            string cesta = string.Empty;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "SELECT E.formular FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + " as E " +
                    " WHERE 1=1 ";

                #region new 13.11.2024

                if (!string.IsNullOrEmpty(filtr.nazev_okna))
                {
                    command.CommandText += " AND E.nazev_okna = @nazev_okna ";
                    command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.typ))
                {
                    command.CommandText += " AND E.typ = @typ ";
                    command.Parameters.AddWithValue("@typ", filtr.typ);
                }

                command.CommandText += " AND (E.ord = @ord) ";
                command.Parameters.AddWithValue("@ord", filtr.ord);

                command.CommandText += " AND (E.loginid = @loginid or E.loginid is null) ";
                command.Parameters.AddWithValue("@loginid", filtr.loginid);

                //if (!string.IsNullOrEmpty(filtr.machineid))
                //{
                //    command.CommandText += " AND E.machineid = @machineid ";
                //    command.Parameters.AddWithValue("@machineid", filtr.machineid);
                //}

                command.CommandText += " ORDER BY E.ord, E.loginid DESC";
                #endregion

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    cesta = reader["formular"].ToString();
                }

                return cesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader?.Close();
                connection?.Close();
            }
        }
    }
}
