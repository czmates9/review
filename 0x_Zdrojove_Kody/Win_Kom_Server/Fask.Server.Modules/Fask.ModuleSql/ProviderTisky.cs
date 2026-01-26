using Fask.DataSets;
using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.SQL.Constants;
using Fask.Server.Interfaces.Vyroba;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fask.ModuleSql.Classes;
using Fask.Constants;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Vyroba.Odvod_MachineStateSet;
using Fask.Interfaces.Filtry;
using Fask.WEBAPI.API_BusinessObjects;
using Fask.Interfaces.Vyroba.Odvod_TiskoveSablony;

namespace Fask.ModuleSql
{
    public partial class Provider :
       Fask.Server.Interfaces.Tisky.ITisk_TiskovaSablona,
         Fask.Server.Interfaces.Tisky.ITisky2_MetodaFindSOPDESC
    {
        public string TiskMetodaFindSOPDESC(string key)
        {
            string returnValue = "TEST"; // Výchozí hodnota, pokud nebude nalezen žádný záznam
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                conn.Open(); // Otevření spojení.

                using (var command = conn.CreateCommand())
                {
                    // SQL dotaz na načtení hodnoty SOPDESC
                    command.CommandText = @"
                SELECT TOP(1) H.[SOPDESC]
                FROM [Agro_fask].[dbo].[CZPRO_VPH] AS H
                LEFT JOIN [Agro_fask].[dbo].[Production] AS P
                ON H.CountEntries = P.CountEntries
                WHERE P.CountEntries = @CountEntries";

                    // Přidání parametru
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@CountEntries",
                        DbType = DbType.String,
                        Value = string.IsNullOrEmpty(key) ? (object)DBNull.Value : key
                    });

                    // Použití ExecuteScalar pro načtení jediné hodnoty
                    var result = command.ExecuteScalar();

                    // Kontrola, zda byl nalezen nějaký výsledek
                    if (result != null && result != DBNull.Value)
                    {
                        returnValue = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Zpracování výjimky
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Uzavření spojení
                    conn.Dispose(); // Uvolnění zdrojů
                }
            }

            return returnValue;
        }


        public int TiskovaSablonaDelete_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Delete(Globals.Konfigurace.ConnectionString[0].FASKDB, row);
        }

        public int TiskovaSablonaEdit_DB(Vyroba.FASK_FORMULARERow row)
        {
            //throw new NotImplementedException();
            return Database.Tisk_FASK_FORMULARE.Update(row, Globals.Konfigurace.ConnectionString[0].FASKDB);
        }

        public int TiskovaSablonaInsert_DB(Vyroba.FASK_FORMULARERow row)
        {
            return Database.Tisk_FASK_FORMULARE.FASK_FORMULARE_Insert(Globals.Konfigurace.ConnectionString[0].FASKDB, row);
        }
    }
}
