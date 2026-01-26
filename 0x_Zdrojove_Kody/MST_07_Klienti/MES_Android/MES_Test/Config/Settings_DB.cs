using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.SQLiteDBs_Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MES_Android.Config
{
    public static class Settings_DB
    {

        #region Inicializace

        public static void Initialize()
        {

            try
            {

                //Kontrola zda existije Settings.prd soubor
                if (!File.Exists(Classes.DataInfo_Static.SettingsPRD))
                {
                    // Pokud neexistuje Settings.prd , tak kontrola zda existuje script Settings.sql 
                    if (!File.Exists(Classes.DataInfo_Static.SettingsSQL))
                    {
                        // Pokud neexistuje Setting.sql, tak je potřeba pomoci API ho ziskat ze serveru
                        Download_Settings_SQL();
                    }

                    SQLite_Helper helper = new SQLite_Helper();
                    if (!helper.SQLite_CreateFile(Classes.DataInfo_Static.SettingsPRD, Classes.DataInfo_Static.SettingsSQL))
                    {
                        throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + Classes.DataInfo_Static.SettingsFileName);
                    }
                }


                var tmp1 = Prodej_REZ1_LastValue;
                tmp1 = Prodej_REZ2_LastValue;
                tmp1 = Prodej_REZ3_LastValue;
                tmp1 = Prodej_REZ4_LastValue;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        private static bool Download_Settings_SQL()
        {
            try
            {

                RestSharp.IRestResponse response;
                API_Server.API_Komunikator.Communicate(API_Server.API_Komunikator.REST_Type.GET, out response, "SettingsSQLTemplate");
                string SQLTemplateObsah = response.Content;

                if (string.IsNullOrEmpty(SQLTemplateObsah))
                {
                    throw new Exception("Problém, nebyl nalezen SQL Script pro založení lokalní databáze!");
                }

                MES_Android.SQLite_ProdejScript script = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.SQLite_ProdejScript>(SQLTemplateObsah);

                if (!Directory.Exists(MES_Android.Classes.DataInfo_Static.SQLiteDBsDir))
                    Directory.CreateDirectory(MES_Android.Classes.DataInfo_Static.SQLiteDBsDir);

                File.WriteAllText(Classes.DataInfo_Static.SettingsSQL, script.Script);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }

            return true;
        }

        #endregion

        #region Pomocne Parametry

        private static string GetValue(string Key, string defaultValue)
        {
            try
            {
                string Val = null;

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings Con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings(Classes.DataInfo_Static.SettingsPRD))
                {
                    Val = Con.GetValueByKEY(Key);
                }

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings Con2 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings(Classes.DataInfo_Static.SettingsPRD))
                {

                    if (Val == null)
                    {
                        int tmp = Con2.Insert_Settings(Key, defaultValue);

                        if (tmp == 1)
                            return defaultValue;
                        else
                            throw new Exception("Neco je špatne s Settings.prd");
                    }
                    else
                        return Val;
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw new Exception("Neco je špatne s Settings.prd");
            }
        }

        private static void SetValue(string Key, string Value)
        {
            try
            {
                int Val = 0;
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings Con = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings(Classes.DataInfo_Static.SettingsPRD))
                {
                    Val = Con.Update_Settings(Key, Value);
                }

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings Con2 = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Settings(Classes.DataInfo_Static.SettingsPRD))
                {
                    if (Val == 0)
                    {
                        int tmp = Con2.Insert_Settings(Key, Value);

                        if (tmp == 1)
                            return;
                        else
                            throw new Exception("Neco je špatne s Settings.prd");
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw new Exception("Neco je špatne s Settings.prd");
            }
        }

        #endregion



        #region Parametry


        public static string Prodej_REZ1_LastValue
        {
            get { return GetValue("Prodej_REZ1_LastValue", string.Empty); }
            set { SetValue("Prodej_REZ1_LastValue", value); }
        }

        public static string Prodej_REZ2_LastValue
        {
            get { return GetValue("Prodej_REZ2_LastValue", string.Empty); }
            set { SetValue("Prodej_REZ2_LastValue", value); }
        }

        public static string Prodej_REZ3_LastValue
        {
            get { return GetValue("Prodej_REZ3_LastValue", string.Empty); }
            set { SetValue("Prodej_REZ3_LastValue", value); }
        }

        public static string Prodej_REZ4_LastValue
        {
            get { return GetValue("Prodej_REZ4_LastValue", string.Empty); }
            set { SetValue("Prodej_REZ4_LastValue", value); }
        }

        public static string LastProductionUserID
        {
            get { return GetValue("OV_LastProductionUserID", string.Empty); }
            set { SetValue("OV_LastProductionUserID", value); }
        }

        public static DateTime LastProductionDateTime
        {
            get {
                try
                {
                    return DateTime.Parse(GetValue("OV_LastProductionDateTime", DateTime.Now.ToString(NumberFormatInfo.InvariantInfo)));
                }
                catch { return DateTime.Now; }
            }
            set { SetValue("OV_LastProductionDateTime", value.ToString(NumberFormatInfo.InvariantInfo)); }
        }


        
        #endregion
    }
}