using Fask.Logging;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Classes
{
    class Expedice
    {

        #region 22.10.2025 MaR new pro sekci Ostatni
        public static string Export_Prelokovani_SQL_Expedice(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            //objednavka.ID==""prelokovani

            #region 22.10.2025 MaR old
            //int countentries = 0;
            //string statusinfo = string.Empty;
            //int statustoreturn = 0;

            //System.Data.SqlClient.SqlConnection adpaconnection = null;

            //         try
            //         {
            //             string ostatni_generateData = Globals.Konfigurace.Expedice[0].GenerateData_Action.Trim();
            //             if (ostatni_generateData.Length != 0)
            //             {
            //                 adpaconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            //                 SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand(ostatni_generateData);
            //                 adpacommand.CommandType = CommandType.StoredProcedure;

            //                 adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CisExpPrik", SqlDbType.NVarChar, 20));
            //                 adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CounterEntries", SqlDbType.Int, 4));
            //                 adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StatusToReturn", SqlDbType.Int, 4));

            //                 ((IDataParameter)adpacommand.Parameters["@CisExpPrik"]).Value = objednavka;
            //                 ((IDataParameter)adpacommand.Parameters["@CounterEntries"]).Direction = ParameterDirection.Output;
            //                 ((IDataParameter)adpacommand.Parameters["@StatusToReturn"]).Direction = ParameterDirection.Output;

            //                 adpacommand.Connection = adpaconnection;

            //                 adpaconnection.Open();
            //                 adpacommand.ExecuteNonQuery();

            //                 countentries = Convert.ToInt32(((IDataParameter)adpacommand.Parameters["@CounterEntries"]).Value);
            //                 statustoreturn = Convert.ToInt32(((IDataParameter)adpacommand.Parameters["@StatusToReturn"]).Value);
            //             }

            //             //if (statustoreturn > 0)
            //             //{
            //             //    si.InnerException = new Exception("chyba");
            //             //    return si;
            //             //}
            //             //else
            //             //    return si;

            //             return "OK";
            //         }
            //         catch (Exception ex)
            //         {
            //             throw ex;
            //         }
            //         finally
            //         {
            //             if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
            //                 adpaconnection.Close();
            //         } 
            #endregion


            int cisloDavky = 0;
            try
            {
                string procedureName = Globals.Konfigurace.Expedice[0].GenerateData_Action.Trim();
                if (procedureName.Length != 0)
                {
                    using (var connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                    using (var command = new System.Data.SqlClient.SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // přidej OUTPUT parametr
                        var param = command.Parameters.Add("@cisloDavky", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;

                        connection.Open();
                        command.ExecuteNonQuery();

                        // načti hodnotu po provedení procedury
                        cisloDavky = (int)param.Value;
                    }
                }

                objednavka.CisloDavky = cisloDavky.ToString();
                return "OK";
            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }

    #endregion



}
