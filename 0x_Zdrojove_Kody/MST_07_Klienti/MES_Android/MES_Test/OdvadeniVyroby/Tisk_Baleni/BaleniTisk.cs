using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.OdvadeniVyroby.Tisk_Baleni
{
    public static class BaleniTisk
    {
        public async static Task<bool> Print(
            AppCompatActivity parent,
            Fask.PrinterFactory.PrinterModules templateName,
            DataSet data,
            bool potvrzovatPocetVytisku,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            var x = await Print(parent, templateName, data, null, potvrzovatPocetVytisku, false, pocetVytisku);
            tcs.SetResult(x);

            return tcs.Task.Result;
        }

        public async static Task<bool> Print(
            AppCompatActivity parent,
            Fask.PrinterFactory.PrinterModules templateName,
            DataSet data,
            Dictionary<string, string> dataDict,
            bool potvrzovatPocetVytisku,
            bool povolitNuloveMnozstvi,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
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
                var x = await PrintSendToPrinter(parent, templateName, printData, potvrzovatPocetVytisku, povolitNuloveMnozstvi, pocetVytisku);

                tcs.SetResult(x);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
               await MessageBoxAsync.Show(parent, ex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetException(ex);
            }

            return tcs.Task.Result;
        }


        private async static Task<bool> PrintSendToPrinter(
            AppCompatActivity parent,
            Fask.PrinterFactory.PrinterModules templateName,
            Dictionary<string, string> printData,
            bool potvrzovatPocetVytisku,
            bool povolitNuloveMnozstvi,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
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
      
                        var dr = await InputBoxAsync.Show(
                            parent,
                            Title: "Tisk",
                            Message: "Počet výtisků",
                            Defaultvalue: pocetVytiskuStr,
                            buttons: MessageBoxButtons.OKCancel,
                            keyboardMode: Android.Text.InputTypes.ClassNumber);

                        //DialogResult dr = InputBox.Show("Počet výtisku", pocetVytiskuStr, out pocetVytiskuStr);
                        if (dr.Dialog_Result == DialogResult.Cancel)
                        {
                            tcs.SetResult(false);
                            return tcs.Task.Result;
                        }
                    }

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        await MessageBoxAsync.Show(parent, ex.Message, "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    // pokud je povoleno zadani nulove mnozstvi
                    if (povolitNuloveMnozstvi && (pocetVytisku == 0))
                    {
                        tcs.SetResult(true);
                        return tcs.Task.Result;
                    }

                    if (pocetVytisku <= 0)
                    {
                        await MessageBoxAsync.Show(parent, "Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        await MessageBoxAsync.Show(parent, "Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);


                //Fask.PrinterProviderWebService.Provider printProvider = new Fask.PrinterProviderWebService.Provider();
                //printProvider.InitializePrinter();
                //printProvider.PrinterName = printerName;
                //printProvider.Print(printData, templateName, pocetVytisku ?? 1);
                printed = Fask.PrinterFactory.PrinterFactory.Instance.Print(printData, templateName, pocetVytisku ?? 1);

                printed = true; // pokud dojde az sem, tak se prohlasi, ze se povedlo vytisknout ... 

                tcs.SetResult(printed);
                

            }
            catch (WebException webex)
            {
                Fask.Logging.ExceptionHandler2.Handle(webex);
                await MessageBoxAsync.Show(parent, webex.Message, "Printing", MessageBoxButtons.OK);
                printed = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(parent, ex.Message, "Printing", MessageBoxButtons.OK);
                printed = false;
            }
            finally
            {
            }

            return tcs.Task.Result;
        }
    }
}