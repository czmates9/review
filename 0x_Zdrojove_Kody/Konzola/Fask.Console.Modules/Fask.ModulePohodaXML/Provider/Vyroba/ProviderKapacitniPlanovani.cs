using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider.Vyroba
{
     public partial class Provider :
         Fask.Interfaces.Vyroba.KapacitniPlanovani.IKapacitniPlanovani,
         Fask.Interfaces.Vyroba.KapacitniPlanovani.IKapacitniPlanovani_GetMatProVyr,
         Fask.Interfaces.Vyroba.KapacitniPlanovani.IKapacitniPlanovani_Get_Vypocet
    {


        #region IKapacitniPlanovani_GetMatProVyr Members

        public Fask.Interfaces.DataSets.Vyroba GetMatProVyr(Fask.Interfaces.Filtry.Vyroba_KapPla_MatProVyr_Filtr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            
            Fask.Interfaces.DataSets.Vyroba vyr = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals_V1.LoadConfiguration();

                using (connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {

                    command = new System.Data.SqlClient.SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    adapter = new System.Data.SqlClient.SqlDataAdapter();

                    if (command.CommandType == CommandType.StoredProcedure)
                    {
                        command.CommandText = "FASK_procGet_KapPlan_MatProVyr";

                        var pItemnmbr = command.Parameters.AddWithValue("@ITEMNMBR", string.Empty);
                        var pSOPNUMBE = command.Parameters.AddWithValue("@SOPNUMBE", string.Empty);

                        pItemnmbr.SqlDbType = System.Data.SqlDbType.NVarChar;
                        pItemnmbr.Precision = 32;
                        pItemnmbr.Scale = 0;

                        pSOPNUMBE.SqlDbType = System.Data.SqlDbType.NVarChar;
                        pSOPNUMBE.Precision = 40;
                        pSOPNUMBE.Scale = 0;

                        adapter.SelectCommand = command;

                        adapter.Fill(vyr, vyr.FASK_Vyroba_KP_MatProVyr.TableName);

                        

                    }
                    else if (command.CommandType == CommandType.Text)
                    {
                        //...
                    }
                    else
                    {
                        throw new Exception("Neznámý typ příkazu: " + command.CommandType.ToString());
                    }
                }

                return vyr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region IKapacitniPlanovani_Get_Vypocet Members

        public Fask.Interfaces.DataSets.Vyroba Get_Vypocet(Fask.Interfaces.DataSets.Vyroba ds)
        {
             Classes.Vyroba.VypocetZustatek(ds);

             return ds;
        }

        #endregion
    }
}
