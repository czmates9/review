using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FASK.SledovaniVyroby.Module.ZZS.Constants
{
    public static class Common
    {
        public static string StorageDir = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;
        public const string Ext_Prodej = "di";

        public static string CiselnikZboziDB { get { return Path.Combine(StorageDir, @"SQLiteDBs\Zbozi.prd"); } } // TODO : prejmenovat CiselnikKatalogZboziDB na CiselnikZboziDB
        public static string CiselnikOdberateleDB { get { return Path.Combine(StorageDir, @"SQLiteDBs\Odberatele.prd"); } }
        public static string CiselnikStrediskaDB { get { return Path.Combine(StorageDir, @"SQLiteDBs\Strediska.prd"); } }
        public static string CiselnikMenDB { get { return Path.Combine(StorageDir, @"SQLiteDBs\Meny.prd"); } }
        public static string CiselnikTypDokladuDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\TypDokladu.prd"); } }
        public static string CiselnikUzivateleDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Uzivatele.prd"); } }
        public static string CiselnikSkladyDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Sklady.prd"); } }
        public static string CiselnikLokaceDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Lokace.prd"); } }
        public static string CiselnikPracovniciDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Pracovnici.prd"); } }
        public static string CiselnikTiskarnyDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Tiskarny.prd"); } }
        public static string CiselnikUkolyDB { get { return Path.Combine(StorageDir,@"SQLiteDBs\Ukoly.prd"); } }
        public static string CiselnikUkolyDBSynch { get { return Path.Combine(StorageDir,@"SQLiteDBs\Ukoly_synch.prd"); } }
        //dodelat nazvy logovacich souboru

    }
}
