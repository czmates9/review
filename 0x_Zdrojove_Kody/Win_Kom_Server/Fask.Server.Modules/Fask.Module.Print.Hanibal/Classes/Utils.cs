using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace Fask.Module.Print.Hanibal.Classes
{
    public class Utils
    {
        // Methods
        public Utils() { }

        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        //public static string getConnectionString()
        //{
        //    return getConnectionString("");
        //}

        //public static string getConnectionString(string database)
        //{
        //    string str2;
        //    string str = "data source=%%DBSERVERNAME%%;initial catalog=%%DBNAME%%;user=%%DBUSER%%;password=%%DBPASSWORD%%;MultipleActiveResultSets=true";
        //    if (((str2 = database) != null) && (str2 == "pohoda"))
        //    {
        //        return str.Replace("%%DBSERVERNAME%%", HanibalEAN_Settings.Default.DBServerName).Replace("%%DBNAME%%", HanibalEAN_Settings.Default.DBName).Replace("%%DBUSER%%", HanibalEAN_Settings.Default.DBUser).Replace("%%DBPASSWORD%%", HanibalEAN_Settings.Default.DBPassword);
        //    }
        //    return str.Replace("%%DBSERVERNAME%%", HanibalEAN_Settings.Default.DBServerName).Replace("%%DBNAME%%", HanibalEAN_Settings.Default.DBName).Replace("%%DBUSER%%", HanibalEAN_Settings.Default.DBUser).Replace("%%DBPASSWORD%%", HanibalEAN_Settings.Default.DBPassword);
        //}

        //public static string getConnectionString(string datasource, string initialcatalog, string user, string password)
        //{
        //    string str = "data source=%%DBSERVERNAME%%;initial catalog=%%DBNAME%%;user=%%DBUSER%%;password=%%DBPASSWORD%%;MultipleActiveResultSets=true";
        //    return str.Replace("%%DBSERVERNAME%%", datasource).Replace("%%DBNAME%%", initialcatalog).Replace("%%DBUSER%%", user).Replace("%%DBPASSWORD%%", password);
        //}

        //public static SqlConnection OpenDatabase(string connectionString)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    return connection;
        //}

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if ((html == null) || (html == string.Empty))
            {
                return string.Empty;
            }
            if (allowHarmlessTags)
            {
                return Regex.Replace(html, "", " ");
            }
            return Regex.Replace(html, "<[^>]*>", " ");
        }

        public static string TruncateString(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }
    }
}
