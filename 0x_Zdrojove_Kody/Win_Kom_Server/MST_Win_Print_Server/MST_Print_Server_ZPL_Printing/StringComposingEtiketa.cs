using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Fask.Logging;

namespace MST_Print_Server_ZPL_Printing
{
    public class StringComposingEtiketa : StringComposing
    {
        // current position key

        string templateName;
        MST_Print_Server_ZPL_Printing.TiskParams printerParams;
        int pocetVytisku;

        public StringComposingEtiketa(string templateName, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Dictionary<string, string> data, string providerGraphicsAssemblyPath, int pocetVytisku)
        {
            //connstring = conn;

            this.templateName = templateName;
            this.printerParams = printerParams;
            this.pocetVytisku = pocetVytisku;
            keys = data;

            raw_printing_object = new RAW_Printing();
            raw_printing_object.PrintParams = printerParams;

            // nacteni providera, pokud je vyplnen
            try
            {
                if (!String.IsNullOrEmpty(providerGraphicsAssemblyPath))
                {
                    if (provider == null) //inicializace se provede pouze pokud nebyla provedena ... 
                    {
                        System.Reflection.Assembly providerAssemlby = System.Reflection.Assembly.LoadFrom(providerGraphicsAssemblyPath);
                        Type[] types = providerAssemlby.GetTypes();
                        foreach (Type t in types)
                        {
                            try
                            {
                                //t.IsAssignableFrom(typeof(Fask.Server.Interfaces.Vydej.IVydej);
                                if (typeof(Fask.BarCodeGraphics.IBarCodeGraphics).IsAssignableFrom(t))
                                {
                                    provider = (Fask.BarCodeGraphics.IBarCodeGraphics)providerAssemlby.CreateInstance(t.FullName);
                                    if (provider != null)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
								Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            }
                        }
                        //return config;
                    }
                }
            }
            catch (Exception ex) 
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
			}
        }

        public bool Compose()
        {
            finalStrings = new List<string>();

            if (!File.Exists(templateName))
            {
                throw new System.Net.WebException("Šablona neexistuje");
                //return false; //sablona neexistuje ...
            }

            string sTemplate = string.Empty;
            try
            {
                StreamReader reader = File.OpenText(templateName);
                sTemplate = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            string etiketa = Replace(sTemplate);

            for (int i = 0; i < pocetVytisku; i++)
            {
                finalStrings.Add(etiketa);
            }
  
            return true;
        }

        private void KeyDelete(string key)
        {
            KeyDelete(this.keys, key);
        }

        private void KeyDelete(Dictionary<string, string> dict, string key)
        {
            if (dict.Keys.Contains(key))
                dict.Remove(key);
        }

        private void KeyUpdate(string key, string value)
        {
            KeyUpdate(this.keys, key, value);
        }

        private void KeyUpdate(Dictionary<string, string> dict, string key, string value)
        {
            if (dict.Keys.Contains(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }


    }
}
