using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data;
using Fask.Module.MTJ.JimiTore.Baleni.Forms;

namespace Fask.Module.MTJ.JimiTore.Baleni
{
    public static class BaleniTisk
    {
        public static bool Print(DataSet data,
            PrinterFactory.PrinterModules printerModule,
            bool potvrzovatPocetVytisku,
            int? pocetVytisku
            )
        {
            return Print(null, null, data, null, printerModule, potvrzovatPocetVytisku, false, pocetVytisku);
        }

        public static bool Print(
            Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore.ZboziRow wsZboziRow,
            Fask.Module.MTJ.JimiTore.Baleni.DataSets.Vydej.ZboziRow zboziRow,
            DataSet data,
            Dictionary<string, string> dataDict,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            bool potvrzovatPocetVytisku,
            bool povolitNuloveMnozstvi,
            int? pocetVytisku
            )
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                if (wsZboziRow != null)
                {
                    foreach (System.Data.DataColumn dcol in wsZboziRow.Table.Columns)
                    {
                        if (!printData.ContainsKey(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), wsZboziRow[dcol.ColumnName].ToString().Trim());
                        }
                    }
                }

                if (zboziRow != null)
                {
                    foreach (System.Data.DataColumn dcol in zboziRow.Table.Columns)
                    {
                        if (!printData.ContainsKey(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), zboziRow[dcol.ColumnName].ToString().Trim());
                        }
                    }
                }


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
                return PrintSendToPrinter(printData, printerModule, potvrzovatPocetVytisku, povolitNuloveMnozstvi, pocetVytisku);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
        }

        
        private static bool PrintSendToPrinter(
            Dictionary<string, string> printData,
            PrinterFactory.PrinterModules printerModule, //string templateName,
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
                        DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr);
                        if (dr == DialogResult.Cancel)
                            return false;
                    }

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    // pokud je povoleno zadani nulove mnozstvi
                    if (povolitNuloveMnozstvi && (pocetVytisku == 0))
                    {
                        return true;
                    }

                    if (pocetVytisku <= 0)
                    {
                        MessageBoxBig.Show("Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        MessageBoxBig.Show("Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);

                Cursor.Current = Cursors.WaitCursor;

                printed = PrinterFactory.PrinterFactory.Instance.Print(printData, printerModule, pocetVytisku ?? 1);

                return printed;
            }
            catch (WebException webex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(webex);
                MessageBoxBig.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return printed;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return printed;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
