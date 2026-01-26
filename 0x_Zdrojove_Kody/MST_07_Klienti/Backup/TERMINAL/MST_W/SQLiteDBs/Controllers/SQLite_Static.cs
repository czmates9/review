using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace Fask.SQLiteDBs.Controllers
{
    public class SQLite_Static
    {
        private static string SQLiteConnectionString
        {
            get
            {
                // konfiguracne?
                StringBuilder sbConnection = new StringBuilder();
                sbConnection.Append("FailIfMissing=True;");
                sbConnection.Append("Data Source={0};");
                return sbConnection.ToString();
            }
        }

        public static string SQLiteConnectionStringFormat(string PathToFile)
        {
            return String.Format(SQLiteConnectionString, PathToFile);
        }

    }

}