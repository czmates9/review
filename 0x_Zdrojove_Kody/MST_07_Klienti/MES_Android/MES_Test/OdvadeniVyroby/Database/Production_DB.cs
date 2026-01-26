using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.SQLiteDBs_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MES_Android.OdvadeniVyroby.Database
{
    public static class Production_DB
    {

        #region Inicializace

        public static void Initialize()
        {

            try
            {

                //Kontrola zda existije Settings.prd soubor
                if (!File.Exists(MES_Android.Classes.DataInfo_Static.ProductionDB))
                {
                    // Pokud neexistuje Settings.prd , tak kontrola zda existuje script Settings.sql 
                    if (!File.Exists(MES_Android.Classes.DataInfo_Static.ProductionScript))
                    {
                        // Pokud neexistuje Setting.sql, tak je potřeba pomoci API ho ziskat ze serveru
                        Download_Production_SQL();
                    }

                    SQLite_Helper helper = new SQLite_Helper();
                    if (!helper.SQLite_CreateFile(MES_Android.Classes.DataInfo_Static.ProductionDBTMP, MES_Android.Classes.DataInfo_Static.ProductionScript))
                    {
                        throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + MES_Android.Classes.DataInfo_Static.Production);
                    }

                    synchronizeFile();

                }
                
           }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        private static void synchronizeFile()
        {
            if (File.Exists(MES_Android.Classes.DataInfo_Static.ProductionDBTMP))
            {
                File.Copy(
                            MES_Android.Classes.DataInfo_Static.ProductionDBTMP,
                            MES_Android.Classes.DataInfo_Static.ProductionDB, false
                            );
                // => nemazat, ponechat jako zalohu, kdyby se nepovedlo stahnout pri inizializaci ... //File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp));
            }
        }

        private static bool Download_Production_SQL()
        {
            try
            {

                RestSharp.IRestResponse response;
                API_Server.API_Komunikator.Communicate(API_Server.API_Komunikator.REST_Type.GET, out response, "ProductionSQLTemplate");
                string SQLTemplateObsah = response.Content;

                if (string.IsNullOrEmpty(SQLTemplateObsah))
                {
                    throw new Exception("Problém, nebyl nalezen SQL Script pro založení lokalní databáze!");
                }

                MES_Android.SQLite_ProdejScript script = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.SQLite_ProdejScript>(SQLTemplateObsah);

                if (!Directory.Exists(MES_Android.Classes.DataInfo_Static.SQLiteDBsDir))
                    Directory.CreateDirectory(MES_Android.Classes.DataInfo_Static.SQLiteDBsDir);

                File.WriteAllText(MES_Android.Classes.DataInfo_Static.ProductionScript, script.Script);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }

            return true;
        }

        #endregion

    }
}