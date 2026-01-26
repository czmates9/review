using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Konfigurace
{
    public abstract class Global_Base
    {

        public virtual string ConfigFilePath { get; }

        public abstract System.Data.DataSet Konfigurace { get; }

        public string LoadConfiguration()
        {
            return LoadConfiguration(this.ConfigFilePath);
        }


        public string SaveConfiguration()
        {
            return SaveConfiguration(this.ConfigFilePath);
        }

        public virtual string LoadConfiguration(string FileName)
        {
            return "OK";
        }

        public virtual string SaveConfiguration(string FileName)
        {
            return "OK";
        }

        public virtual void CreateFile(string FilePath)
        {
            
        }

        public string GetPath(string FileName)
        {
            string FilePath = string.Empty;

            try
            {
                if (Path.IsPathRooted(FileName))
                    FilePath = FileName;
                else
                {
                    FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Konfigurace\Konfigurace_Soubory", FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FilePath;
        }


    }


//    using Fask.Module.ABRA.CarpServise.SQL_Datasets;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Web;

//namespace Fask.Module.ABRA.CarpServise
//    {
//        public class Globals_V1 : Server.Interfaces.Konfigurace.Global_Base
//        {

//            public override string ConfigFilePath
//            {
//                get { return "MST_ABRA_CarpServis_Config.xml"; }
//            }

//            private ABRA_V1 konfigurace;

//            public override DataSet Konfigurace
//            {
//                get
//                {
//                    if (Konfigurace == null)
//                        Konfigurace = new ABRA_V1();

//                    return Konfigurace;

//                }

//            }


//            public static Fask.Module.ABRA.CarpServise.Globals_V1 Instance
//            {
//                get
//                {
//                    if (Instance == null)
//                        Instance = new Globals_V1();

//                    return Instance;

//                }

//            }



//            public override string LoadConfiguration(string FileName)
//            {
//                try
//                {

//                    string FilePath = GetPath(FileName);

//                    if (!File.Exists(FilePath))
//                    {
//                        //Soubor neexistuje, tak vytvořit..
//                        this.CreateFile(FilePath);
//                    }

//                    if (Konfigurace == null)
//                        Konfigurace = new ABRA_V1();

//                    Konfigurace.Clear();
//                    Konfigurace.ReadXml(FilePath);

//                    return "OK";
//                }
//                catch (Exception ex)
//                {
//                    Logging.ExceptionHandler2.Handle(ex);
//                    throw ex;
//                }
//            }

//            public override string SaveConfiguration(string FileName)
//            {
//                try
//                {

//                    string FilePath = GetPath(FileName);
//                    Konfigurace.WriteXml(FilePath);

//                    return "OK";
//                }
//                catch (Exception ex)
//                {
//                    Logging.ExceptionHandler2.Handle(ex);
//                    throw ex;
//                }

//            }

//            public override void CreateFile(string FilePath)
//            {

//                try
//                {

//                    ABRA_V1 ds = new ABRA_V1();

//                    ds.ConnectionStrings.AddConnectionStringsRow(
//                        @"User=SYSDBA;Password=masterkey;Database=C:\FASK\ABRA_DB\DATAFB3.FDB;DataSource=localhost;Charset=WIN1250;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;",
//                        @"Data Source=FASKCZ-CO010\SQLEXPRESS;Initial Catalog=Abra_CARP_Servis;User ID=sa;Password=sasa"
//                        );

//                    ds.Vydej.AddVydejRow(
//                        false,
//                        "1234",
//                        @"Logs\SoDir\"
//                        );

//                    ds.Prijem.AddPrijemRow(
//                        "1234",
//                        @"Logs\SoDir\"
//                        );

//                    ds.AcceptChanges();

//                    ds.WriteXml(FilePath);

//                }
//                catch (Exception ex)
//                {
//                    Logging.ExceptionHandler2.Handle(ex);
//                    throw ex;
//                }

//            }
//        }
//    }


}



