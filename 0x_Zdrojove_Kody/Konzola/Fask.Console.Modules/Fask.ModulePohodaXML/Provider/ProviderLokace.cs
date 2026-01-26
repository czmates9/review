using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Interfaces.SkladLokace;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider
    {
        private string TABLE_CZMST_SKLADLOKACE_STAV = "CZMST_SkladLokace_Stav";
        //private string TABLE_CZMST_SKLADLOKACE_STAVPOHYB = "CZMST_SkladLokace_StavPohyb";
        //private string TABLE_CZMST_SKLADLOKACE_MAPA = "CZMST_SkladLokace_Mapa";
        private string TABLE_CZMST_SkladLokace_LokaceTypy = "CZMST_SkladLokace_LokaceTypy";
        private string TABLE_CZMST_SkladLokace_LokaceVariantySortiment = "CZMST_SkladLokace_LokaceVariantySortiment";


        public Fask.Interfaces.DataSets.Location Lokace_VariantySortimentGet(string itemnmbr, string skl_id)
        {
            //TABLE_CZMST_SkladLokace_LokaceVariantySortiment
            SqlCommand command = null;
            SqlConnection connection = null;
            StatusOverLokace status = new StatusOverLokace();

            Fask.Interfaces.DataSets.Location locationDS = new Fask.Interfaces.DataSets.Location();

            try
            {

                Globals_V1.LoadConfiguration();
                command = new SqlCommand();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                connection.Open();

                command.Connection = connection;

                // overovani cilove lokace ... pouze kontrola existence lokace
                //Kontrola, jestli je lokace a sklad v DB
                command.CommandText = "SELECT * FROM " + TABLE_CZMST_SkladLokace_LokaceVariantySortiment + " WHERE itemnmbr=@itemnmbr";
                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                if (!String.IsNullOrEmpty(skl_id))
                {
                    command.CommandText += " AND SKL_ID=@skl_id";
                    command.Parameters.AddWithValue("@skl_id", string.IsNullOrEmpty(skl_id) ? string.Empty : skl_id);
                }

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceVariantySortiment);

                command.Parameters.Clear();
                command.CommandText = "Select * FROM " + TABLE_CZMST_SkladLokace_LokaceTypy;
                sda.Fill(locationDS.CZMST_SkladLokace_LokaceTypy);

                return locationDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Zobrazi mnozstvi materialu na jednotlivych lokaci
        /// </summary>
        /// <param name="itemnmbr">ID materialu.</param>
        /// <returns>Odpovidajici zaznamy</returns>
        public Fask.Interfaces.DataSets.Location Lokace_ShowMaterial(string itemnmbr)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Fask.Interfaces.DataSets.Location records = new Fask.Interfaces.DataSets.Location();
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                // select, kde mnozstvi neni 0
                command.CommandText = "SELECT * FROM " + TABLE_CZMST_SKLADLOKACE_STAV + " WHERE ITEMNMBR=@itemnmbr and QTYSHPPD<>0";

                command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                command.Connection = connection;

                // naplneni datasetu
                adapter.SelectCommand = command;
                adapter.Fill(records.CZMST_SkladLokace_Stav);

                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && (connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

    }
}
