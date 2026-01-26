using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Parametry.IParametry2,
        Fask.Interfaces.Parametry.IParametry2_Get_ConnectionStrings
    {
        public Provider()
        { }

        public void Get_ConnectionStrings(out string DB_FASK, out string DB_POHODA)
        {
            Globals_V1.LoadConfiguration();

            IDbConnection connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            DB_FASK = connection.Database;

            IDbConnection connectionP = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
            DB_POHODA = connectionP.Database;

        }
    }


    #region pomocne tridy
    public class ZasobyTableInfo
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
    }

    public class ZasobyData
    {
        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
    #endregion
}


