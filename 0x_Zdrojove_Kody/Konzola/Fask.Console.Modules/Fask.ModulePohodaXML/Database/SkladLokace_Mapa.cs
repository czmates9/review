using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Classes
{
    class SkladLokace_Mapa
    {
        public static void GenerujLokaci(Fask.Interfaces.Classes.GenerovaniLokaci gl)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();

                using (connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {

                    command = new System.Data.SqlClient.SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.CommandText = "FASK_GeneratorLokaci";

                    var pSKL_ID = command.Parameters.AddWithValue("@SKL_ID", gl.SKL_ID);
                    var pTYPE = command.Parameters.AddWithValue("@TYPE", gl.TYPE);
                    var pOD = command.Parameters.AddWithValue("@OD", gl.OD);
                    var pPocet = command.Parameters.AddWithValue("@Pocet", gl.POCET);
                    var pN = command.Parameters.AddWithValue("@N", gl.N);
                    var pPrefix = command.Parameters.AddWithValue("@prefix", gl.PREFIX);

                    pSKL_ID.SqlDbType = System.Data.SqlDbType.NVarChar;
                    pSKL_ID.Precision = 20;
                    pSKL_ID.Scale = 0;

                    pTYPE.SqlDbType = System.Data.SqlDbType.NVarChar;
                    pTYPE.Precision = 2;
                    pTYPE.Scale = 0;

                    pOD.SqlDbType = System.Data.SqlDbType.Int;
                    pPocet.SqlDbType = System.Data.SqlDbType.Int;
                    pN.SqlDbType = System.Data.SqlDbType.Int;

                    pPrefix.SqlDbType = System.Data.SqlDbType.NVarChar;
                    pPrefix.Precision = 50;
                    pPrefix.Scale = 0;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
