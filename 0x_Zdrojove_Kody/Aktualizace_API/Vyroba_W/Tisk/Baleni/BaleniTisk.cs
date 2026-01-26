using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data;

namespace Fask.Vyroba_W.Tisk.Baleni
{
    public static class BaleniTisk
    {
        public static bool Print(
            string templateName,
            string printerName,
            DataSet data,
            bool potvrzovatPocetVytisku,
            int? pocetVytisku
            )
        {
            return Print(templateName, printerName, data, null, potvrzovatPocetVytisku, false, pocetVytisku);
        }

        public static bool Print(
            string templateName,
            string printerName,
            DataSet data,
            Dictionary<string, string> dataDict,
            bool potvrzovatPocetVytisku,
            bool povolitNuloveMnozstvi,
            int? pocetVytisku
            )
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.KeyColumn };
                if (data != null)
                {
                    if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
                    {
                        foreach (DataRow dr in data.Tables[0].Rows)//.Rows[0])
                        {
                            foreach (DataColumn dc in data.Tables[0].Columns)
                            {
                                if (!printData.ContainsKey(dc.ColumnName))
                                    printData.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                            }
                        }
                    }
                }

                if (dataDict != null)
                {
                    foreach (string key in dataDict.Keys)
                    {
                        if (!printData.ContainsKey(key))
                            printData.Add(key, dataDict[key].Trim());
                    }
                }
                return PrintSendToPrinter(templateName, printerName, printData, potvrzovatPocetVytisku, povolitNuloveMnozstvi, pocetVytisku);

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        
        private static bool PrintSendToPrinter(
            string templateName,
            string printerName,
            Dictionary<string, string> printData,
            bool potvrzovatPocetVytisku,
            bool povolitNuloveMnozstvi,
            int? pocetVytisku
            )
        {
            bool printed = false;

            // pokud je mnozstvi > 100, zobrazi se dialog pro zadani mnozstvi (kvuli zacykleni)
            if (pocetVytisku.HasValue && pocetVytisku > 100)
            {
                potvrzovatPocetVytisku = true;
            }

            try
            {
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    if (potvrzovatPocetVytisku)
                    {
                        //DialogResult dr = Fask.MST_W.Forms.InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr);
                        DialogResult dr = DialogResult.None;
                        using (Fask.Vyroba_W.Forms.FormInputKod f = new Fask.Vyroba_W.Forms.FormInputKod())
                        {
                            f.Text = "Počet výtisků";
                            f.Kod = pocetVytiskuStr;
                            dr = f.ShowDialog();
                            pocetVytiskuStr = f.Kod;
                        }
                        if (dr == DialogResult.Cancel)
                            return false;
                    }

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        continue;
                    }

                    // pokud je povoleno zadani nulove mnozstvi
                    if (povolitNuloveMnozstvi && (pocetVytisku == 0))
                    {
                        return true;
                    }

                    if (pocetVytisku <= 0)
                    {
                        MessageBox.Show("Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        MessageBox.Show("Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);

                Cursor.Current = Cursors.WaitCursor;

                Fask.PrinterProviderWebService.Provider printProvider = new Fask.PrinterProviderWebService.Provider();
                printProvider.InitializePrinter(Settings.TiskWebServiceAddress, Settings.TiskWebServiceTimeOut);
                printProvider.PrinterName = printerName;
                printProvider.Print(printData, templateName, pocetVytisku ?? 1);

                printed = true; // pokud dojde az sem, tak se prohlasi, ze se povedlo vytisknout ... 
                return printed;
                
            }
            catch (WebException webex)
            {
                Cursor.Current = Cursors.Default;
				Logging.Log.Write(webex);
                MessageBox.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return printed;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
                MessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return printed;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
