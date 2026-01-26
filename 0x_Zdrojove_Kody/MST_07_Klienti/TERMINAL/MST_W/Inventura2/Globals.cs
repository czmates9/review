using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.MST_W.Inventura2
{
    public class Globals
    {
        //public static System.Data.SqlServerCe.SqlCeConnection active_connection = null;
		//public static System.Data.SQLite.SQLiteConnection active_connection = null;
		//private static string davka = string.Empty;
		//public static string Davka
		//{
		//    set
		//    {
		//        TerminateConnection();

		//        davka = value;

		//        //active_connection = new System.Data.SqlServerCe.SqlCeConnection(
		//        active_connection = new System.Data.SQLite.SQLiteConnection(
		//            "Data source=" + Path.Combine(Main.StorageDir, davka.Trim() + "." + Main.Ext_Inventura2)
		//            );
		//    }
		//    get
		//    {
		//        return davka;
		//    }
		//}

		//private static void TerminateConnection()
		//{
		//    if (active_connection != null && active_connection.State == System.Data.ConnectionState.Open)
		//    {
		//        active_connection.Close();
		//        active_connection.Dispose();
		//        active_connection = null;
		//    }
		//}
		//public static SqlCEDBs.DataSets.Inventura2.KANCLRow active_kancelar = null;
		//public static SqlCEDBs.DataSets.Inventura2.LOKACERow active_lokace = null;
		//public static SqlCEDBs.DataSets.Inventura2.OSOBYRow active_osoba = null;
		//public static SqlCEDBs.DataSets.Inventura2.UCSTRRow active_stredisko = null;
		//public static SqlCEDBs.DataSets.Inventura2.ParametryRow active_parametry = null;

		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter ta_inventur = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter ta_majetek = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter ta_kancl = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter ta_lokace = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter ta_osoby = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter ta_stredisko = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter ta_queries = null;
		//public static SqlCEDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter ta_parametry = null;

		//public static void Initialize()
		//{
		//    ta_inventur = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.INVENTURTableAdapter();
		//    ta_majetek = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.MAJETEKTableAdapter();
		//    ta_kancl = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.KANCLTableAdapter();
		//    ta_lokace = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.LOKACETableAdapter();
		//    ta_osoby = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.OSOBYTableAdapter();
		//    ta_stredisko = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.UCSTRTableAdapter();
		//    ta_queries = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.QueriesTableAdapter();
		//    ta_parametry = new Fask.MST_W.SqlCEDBs.DataSets.Inventura2TableAdapters.ParametryTableAdapter();

		//    ta_inventur.Connection = active_connection;
		//    ta_majetek.Connection = active_connection;
		//    ta_kancl.Connection = active_connection;
		//    ta_lokace.Connection = active_connection;
		//    ta_osoby.Connection = active_connection;
		//    ta_stredisko.Connection = active_connection;
		//    ta_queries.Connection = active_connection;
		//    ta_parametry.Connection = active_connection;
		//}

		//public static void Terminate()
		//{
		//    if (ta_inventur != null) ta_inventur = null;
		//    if (ta_majetek != null) ta_majetek = null;
		//    if (ta_kancl != null) ta_kancl = null;
		//    if (ta_lokace != null) ta_lokace = null;
		//    if (ta_osoby != null) ta_osoby = null;
		//    if (ta_stredisko != null) ta_stredisko = null;
		//    if (ta_queries != null) ta_queries = null;
		//    if (ta_parametry != null) ta_parametry = null;

		//    TerminateConnection();

		//    active_lokace = null;
		//    active_kancelar = null;
		//    active_osoba = null;
		//    active_stredisko = null;
		//    active_parametry = null;
		//}
    }
}
