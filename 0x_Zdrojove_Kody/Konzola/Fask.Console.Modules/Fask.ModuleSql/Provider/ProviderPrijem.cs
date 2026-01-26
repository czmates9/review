using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;

namespace Fask.ModuleSql
{
     public partial class Provider : Fask.Interfaces.Prijem.IPrijem2,
         Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPI,
         Fask.Interfaces.Prijem.IPrijem2_GetFiltrovaneDavkyPE,
         Fask.Interfaces.Prijem.IPrijem2_UpdatePE,
         Fask.Interfaces.Prijem.IPrijem2_UpdatePI,
         Fask.Interfaces.Prijem.IPrijem2_DeletePE,
         Fask.Interfaces.Prijem.IPrijem2_DeletePI

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
                connection = new SqlConnection(ConnectionString);
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

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");


                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between '19990101' and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and 25000101 ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                        int cislo = 0;
                        cislo = ((int)TimeVar);

                        if(cislo !=0 && cislo < 8 )
                        {
                            command.CommandText += " AND TIMEDONE >= @TIME_TV ";
                            command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss"));

                        }


                        //string den = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd");
                        //string cas = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss");


                        command.CommandText += " AND DATEDONE >= @dateeve_TV ";
                        command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd"));


                        }
                }



                command.CommandText += " order by CountEntries, DEX_ROW_ID";

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
                connection = new SqlConnection(ConnectionString);
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
                    command.CommandText += "OR CZ_Doslo=201 ";
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


                command.CommandText += "order by CountEntries, DEX_ROW_ID";



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


        #region PE

        public bool UpdatePE(Prijem.CZMST_PERow PERow)
        {
            return Database.Sklady_CZMST_PE.Update(PERow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }

        public int UpdatePE(Prijem.CZMST_PEDataTable PE_dt)
        {
            return Database.Sklady_CZMST_PE.Update(PE_dt, ConnectionString);
            //throw new NotImplementedException();
        }

        public bool DeletePE(int ID)
        {
            throw new NotImplementedException();
        }

        public bool DeletePE(Prijem.CZMST_PERow PERow)
        {

            PERow.Delete();

            return Database.Sklady_CZMST_PE.Update(PERow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }

        #endregion

        #region PI

        public bool UpdatePI(Prijem.CZMST_PIRow PIRow)
        {
            return Database.Sklady_CZMST_PI.Update(PIRow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }

        public bool DeletePI(int ID)
        {


            throw new NotImplementedException();
        }

        public bool DeletePI(Prijem.CZMST_PIRow PIRow)
        {

            PIRow.Delete();

            return Database.Sklady_CZMST_PI.Update(PIRow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }

     



        #endregion
    }
}
