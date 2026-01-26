using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Tisk
{
    public class Tiskarna
    {
        public static bool Tiskni(string nazevtiskarny, string sablonafilename, System.Collections.Generic.Dictionary<string, object> keyvaluedictionary, string jobName)
        {
            try
            {
                StreamReader sr = new StreamReader(sablonafilename);
                StringBuilder sb = new StringBuilder(sr.ReadToEnd());

                sr.Close();
                sr = null;

                foreach (string key in keyvaluedictionary.Keys)
                {
                    sb.Replace("$" + key + "$", keyvaluedictionary[key].ToString());
                }

                string datatoprint = sb.ToString();

                if (Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    StreamWriter sw = new StreamWriter(@".\Tisk\Print_" + Guid.NewGuid().ToString() + ".prt", false);
                    sw.Write(datatoprint);
                    sw.Close();
                    sw = null;
                }

                if (RAW_Printing.SendStringToPrinter(nazevtiskarny, datatoprint, jobName))
                    return true;
                else
                    throw new Exception("Nepodařilo se vytisknout data");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Tiskarna", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(null, ex.Message, "Tisk etikety", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
