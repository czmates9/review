using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Print.Hanibal.Classes
{
    public class Database
    {

        public bool Get_Data(
            string SOPNUMBE, 
            out string PDoklad,
            out string Pozn,
            out string IDS
            )
        {

            PDoklad = string.Empty;
            Pozn = string.Empty;
            IDS = string.Empty;

            try
            {
                Globals_V1.LoadConfiguration();
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB)) 
                {

                    string SQL = "SELECT  O.PDoklad as PDoklad, O.Pozn as Pozn, UZ.IDS as IDS from OBJ as O" +
                    " LEFT JOIN sVPULpol as UZ ON UZ.ID = O.RefVPrDoprava " +
                    " WHERE Cislo = ? ";


                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(SQL, connection); 
                    command.Parameters.Add("?", System.Data.OleDb.OleDbType.VarChar).Value = SOPNUMBE;
                    command.Connection.Open();
                    System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        PDoklad = Convert.ToString(reader["PDoklad"]);
                        Pozn = Convert.ToString(reader["Pozn"]);
                        IDS = Convert.ToString(reader["IDS"]);
                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "### ERROR - Objednavka s SOPNUMBE: " + SOPNUMBE + " nebyla v DB nalezena");
                        return false;
                    }
                    
                    reader.Close();

                    command.Connection.Close();
                }

                return true;
            }
            catch (Exception exception)
            {

                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "### DB ERROR - probl\x00e9m se spojen\x00edm do datab\x00e1ze POHODA");
                Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
                return false;
            }
        }

    }
}
