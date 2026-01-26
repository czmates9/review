using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : Fask.Interfaces.Prijem.IPrijem2,
        Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPI,
        Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPE
    {
        #region IPrijem2_GetFiltrovaneDavkyPI Members

        public Fask.Interfaces.DataSets.Prijem GetFiltrovaneDavkyPI(Fask.Interfaces.Filtry.PrijemNasnimaneFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prijem dsPrijem = new Fask.Interfaces.DataSets.Prijem();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_PI +
                    " WHERE" +
                    " 1=1 ";



                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }

                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
                }

                if (filtr.PONUMBER != null && !string.IsNullOrEmpty(filtr.PONUMBER.Trim()))
                {
                    command.CommandText += "AND PONUMBER=@PONUMBER ";
                    command.Parameters.AddWithValue("@PONUMBER", filtr.PONUMBER);
                }

                command.CommandText += "order by CountEntries, DEX_ROW_ID";

                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsPrijem, dsPrijem.CZMST_PI.TableName);

                return dsPrijem;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IPrijem2_GetFiltrovaneDavkyPE Members

        public Fask.Interfaces.DataSets.Prijem GetFiltrovaneDavkyPE(Fask.Interfaces.Filtry.PrijemDavkyFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prijem dsPrijem = new Fask.Interfaces.DataSets.Prijem();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_PE +
                    " WHERE" +
                    " 1=1 ";


                command.CommandText += " AND ( 1!=1 ";


                if (filtr.UvolneneDavky)
                {
                    command.CommandText += "OR CZ_Doslo=0 ";
                }

                if (filtr.NEUvolneneDavky)
                {
                    command.CommandText += "OR CZ_Doslo=255 ";
                }

                if (filtr.StazeneDavky)
                {
                    command.CommandText += "OR ( CZ_Doslo > 0 AND CZ_Doslo < 100 ) ";
                }


                if (filtr.SpracovaneDavky)
                {
                    command.CommandText += "OR ( CZ_Doslo > 100 AND CZ_Doslo < 200 ) ";
                }


                if (filtr.MrtveDavky)
                {
                    command.CommandText += "OR ( CZ_Doslo=201 OR CZ_Doslo=100 ) ";
                }

                command.CommandText += ")";

                //}


                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }

                // Sopnumbe
                if (filtr.PONUMBER != null && !string.IsNullOrEmpty(filtr.PONUMBER.Trim()))
                {
                    command.CommandText += "AND PONUMBER=@PONUMBER ";
                    command.Parameters.AddWithValue("@PONUMBER", filtr.PONUMBER);
                }

                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
                }


                command.CommandText += " order by CountEntries, DEX_ROW_ID";



                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsPrijem, dsPrijem.CZMST_PE.TableName);

                return dsPrijem;
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
