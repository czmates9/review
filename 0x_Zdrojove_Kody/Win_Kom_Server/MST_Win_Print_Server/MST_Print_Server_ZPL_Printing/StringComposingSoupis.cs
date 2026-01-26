using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fask.Logging;

namespace MST_Print_Server_ZPL_Printing
{
    public class StringComposingSoupis : StringComposing
    {
        string templateHeaderName;
        string templateRowName;
        string templateFooterName;
        MST_Print_Server_ZPL_Printing.TiskParams printerParams;
        int pocetVytisku;

        public StringComposingSoupis(string templateHeaderName, string templateRowName, string templateFooterName, MST_Print_Server_ZPL_Printing.TiskParams printerParams, Dictionary<string, string> dataHeader, List<Dictionary<string, string>> dataRows,Dictionary<string, string> dataFooter, int pocetVytisku)
        {
            this.templateHeaderName = templateHeaderName;
            this.templateRowName = templateRowName;
            this.templateFooterName = templateFooterName;

            this.printerParams = printerParams;
            this.pocetVytisku = pocetVytisku;

            keysHeader = dataHeader;
            keysRows = dataRows;
            keysFooter = dataFooter;
            //keys = data;

            raw_printing_object = new RAW_Printing();
            raw_printing_object.PrintParams = printerParams;
        }

        public bool Compose()
        {
            finalStrings = new List<string>();

            if (!File.Exists(templateHeaderName))
            {
                throw new System.Net.WebException("Šablona '" + templateHeaderName + "' neexistuje");
                //return false; //sablona neexistuje ...
            }
            if (!File.Exists(templateRowName))
            {
                throw new System.Net.WebException("Šablona '" + templateRowName + "' neexistuje");
                //return false; //sablona neexistuje ...
            }
            if (!File.Exists(templateFooterName))
            {
                throw new System.Net.WebException("Šablona '" + templateFooterName + "' neexistuje");
                //return false; //sablona neexistuje ...
            }

            string sTemplateHeader = string.Empty;
            string sTemplateRow = string.Empty;
            string sTemplateFooter = string.Empty;

            try
            {
                
                // načtení všech šablon
                //StreamReader readerHeader = File.OpenText(templateHeaderName);
                //sTemplateHeader = readerHeader.ReadToEnd();
                //readerHeader.Close();
                sTemplateHeader = ReadTemplate(templateHeaderName);

                //StreamReader readerRow = File.OpenText(templateRowName);
                //sTemplateRow = readerRow.ReadToEnd();
                //readerRow.Close();
                sTemplateRow = ReadTemplate(templateRowName);

                //StreamReader readerFooter = File.OpenText(templateFooterName);
                //sTemplateFooter = readerFooter.ReadToEnd();
                //readerFooter.Close();
                sTemplateFooter = ReadTemplate(templateFooterName);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            string data = string.Empty;

            // nahrazení dat v hlavičce
            data = Replace(sTemplateHeader, keysHeader);
            finalStrings.Add(data);

            // nahrazení dat v řádcích
            foreach (Dictionary<string, string> rows in keysRows)
            {
                data = Replace(sTemplateRow, rows);
                finalStrings.Add(data);
            }

            // nahrazení dat v patičce
            data = Replace(sTemplateFooter, keysFooter);
            finalStrings.Add(data);

            //for (int i = 0; i < pocetVytisku; i++)
            //{
            //    finalStrings.Add(etiketa);
            //}
  
            return true;
        }

        private string ReadTemplate(string templatePath)
        {
            StreamReader sr = new StreamReader(templatePath);
            string sb = sr.ReadToEnd();
            sr.Close();
            return sb;
        }

        //private void KeyDelete(string key)
        //{
        //    KeyDelete(this.keys, key);
        //}

        //private void KeyDelete(Dictionary<string, string> dict, string key)
        //{
        //    if (dict.Keys.Contains(key))
        //        dict.Remove(key);
        //}

        //private void KeyUpdate(string key, string value)
        //{
        //    KeyUpdate(this.keys, key, value);
        //}

        //private void KeyUpdate(Dictionary<string, string> dict, string key, string value)
        //{
        //    if (dict.Keys.Contains(key))
        //        dict[key] = value;
        //    else
        //        dict.Add(key, value);
        //}
    }
}
