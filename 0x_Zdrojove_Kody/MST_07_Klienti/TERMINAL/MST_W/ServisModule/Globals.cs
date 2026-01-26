using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.MST_W.ServisModule
{
    public class Globals
    {
        public static object ImageLockObject = new object();

		public static GlobalObject globalObject = new GlobalObject();

		//public const string ServisCiselnikFileName = "Servis_Ciselniky.prd";
		//public const string ServisZdrojePohybFileName = "Servis_ZdrojePohyb.prd";
		//public const string ServisZdrojePohybTmpFileName = "Servis_ZdrojePohyb.prd.tmp";
		//public const string ServisZdrojeStavFileName = "Servis_ZdrojeStav.prd";

		///// <summary>
		///// Cislo zvolene davky pri davkovem zpracovani.
		///// </summary>
		
		//public static string Davka;

		//public static string ServisCiselnikDB { get { return Path.Combine(Main.DataDir, ServisCiselnikFileName); } }
		//public static string ServisZdrojePohybDB { get { return Path.Combine(Main.DataDir, ServisZdrojePohybFileName); } }
		//public static string ServisZdrojePohybTmpDB { get { return Path.Combine(Main.DataDir, ServisZdrojePohybTmpFileName); } }
		//public static string ServisZdrojeStavDB { get { return Path.Combine(Main.DataDir, MST_Global.ServisDavkoveZpracovani ? (Davka + "." + Main.Ext_ServisI) : ServisZdrojeStavFileName); } }

		// Adaptery jen pro hlavni vlakno !!!
        // !!! Pokud je neco delano v jinych vlaknech, tak si musi vytvorit vlastni adaptery !!!
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter _taCiselnikZdroj = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter _taCiselnikStav = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter _taCiselnikOkruh = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter _taCiselnikStavNext = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter _taCiselnikCinnost = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter _taCiselnikCinnostNext = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter _taZdrojeStav = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter _taZdrojePohyb = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter _taDynTabDef = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter _taDynTab = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter _taZdrojeStavTmp = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojStavTmpTableAdapter();
		//public static Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter _taPredloha = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_PredlohaTableAdapter();

		//public static _WebRefernces_Globals.ServisModuleWServiceSession _webServiceModule = null;
    }
}
