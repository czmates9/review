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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Prodej
{
    public class ProdejTisk
    {

        public async static Task<bool> PrintAsync(
            AppCompatActivity _parent,
            Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row095 = null,
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI = null,
            Dictionary<string, string> data = null,
            Fask.PrinterFactory.PrinterModules printerModule = Fask.PrinterFactory.PrinterModules.Baleni,
            int? pocetVytisku = null,
            bool? TiskSCenou = null
            )
                {
                    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

                    try
                    {

                        //Task.Run(()=>
                        //{
                        Dictionary<string, string> printData = new Dictionary<string, string>();

                        //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.KeyColumn };

                        //if (Prodej.Globals.EtiketaTiskDotazSCenou)
                        //{
                        if (TiskSCenou != null)
                        {
                            printData.Add("TISKCENA", TiskSCenou.ToString());
                        }

                        //}

                        if (rowDI != null)
                        {
                            foreach (System.Data.DataColumn dcol in rowDI.Table.Columns)
                            {
                                //if (!printData.Values.Rows.Contains(dcol.ColumnName.ToUpper()))
                                if (!printData.ContainsKey(dcol.ColumnName.ToUpper()))
                                {
                                    //printData.Values.AddValuesRow(dcol.ColumnName.ToUpper(), rowDI[dcol.ColumnName].ToString().Trim());
                                    printData.Add(dcol.ColumnName.ToUpper(), rowDI[dcol.ColumnName].ToString().Trim());
                                }
                            }
                        }

                        if (row095 != null)
                        {
                            foreach (System.Data.DataColumn dcol in row095.Table.Columns)
                            {
                                //if (!printData.Values.Rows.Contains(dcol.ColumnName.ToUpper()))
                                if (!printData.ContainsKey(dcol.ColumnName.ToUpper()))
                                {
                                    //printData.Values.AddValuesRow(dcol.ColumnName.ToUpper(), row095[dcol.ColumnName].ToString().Trim());
                                    printData.Add(dcol.ColumnName.ToUpper(), row095[dcol.ColumnName].ToString().Trim());
                                }
                            }
                        }

                        if (data != null)
                        {
                            foreach (string key in data.Keys)
                            {
                                //if (!printData.Values.Rows.Contains(key))
                                //    printData.Values.AddValuesRow(key, data[key].Trim());
                                if (!printData.ContainsKey(key))
                                    printData.Add(key, data[key].Trim());

                            }
                        }

                        var x = await PrintSendToPrinter(_parent, printData, printerModule, pocetVytisku);
                        tcs.SetResult(x);
                        //});



                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        //MessageBox.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                        tcs.SetException(ex);
                    }

                    return tcs.Task.Result;
                }

        public async static Task<bool> PrintSoupisSendToPrinter(
            AppCompatActivity _parent,
            Dictionary<string, string> dataHlavicka,
            List<Dictionary<string, string>> dataRadky,
            Dictionary<string, string> dataPaticka,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            //bool printed = false;

            try
            {
                //Logging.Log.WriteDebug("START zadani poctu vytisku");
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    var dr = await InputBoxAsync.Show(
                        _parent,
                        Title: "Tisk",
                        Message: "Počet výtisků",
                        Defaultvalue: pocetVytiskuStr,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Android.Text.InputTypes.ClassNumber);



                    if (dr.Dialog_Result == DialogResult.Cancel)
                        return false;

                    try
                    {
                        pocetVytisku = int.Parse(dr.Value);
                    }
                    catch (Exception ex)
                    {
                        await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    if (pocetVytisku <= 0)
                    {
                        await MessageBoxAsync.Show(_parent, "Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        await MessageBoxAsync.Show(_parent, "Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);

                //Logging.Log.WriteDebug("END zadani poctu vytisku");
                //for (int i = 0; i < (pocetVytisku ?? 1); i++)
                //{
                //    while (true)
                //    {
                //        printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka, 1);
                //        if (!printed)
                //        {
                //            DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                //            if (drPrint == DialogResult.Yes)
                //                continue;
                //            else
                //                return false;
                //        }
                //        break;
                //    }
                //}

                bool printed;
                while (true)
                {
                    printed = Fask.PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka, pocetVytisku ?? 1);
                    if (!printed)
                    {
                        DialogResult drPrint = await MessageBoxAsync.Show(_parent, "Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                        {
                            tcs.SetResult(false);
                            return tcs.Task.Result;
                        }
                    }
                    break;
                }

                tcs.SetResult(printed);
;            }
            catch (WebException webex)
            {
                Fask.Logging.ExceptionHandler2.Handle(webex);
                await MessageBoxAsync.Show(_parent, webex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }

            return tcs.Task.Result;

        }

        public async static Task<bool> PrintPaletaSendToPrinter(
            AppCompatActivity _parent,
            Dictionary<string, string> dataHlavicka,
            List<Dictionary<string, string>> dataRadky,
            Dictionary<string, string> dataPaticka,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            //bool printed = false;

            try
            {
                //Logging.Log.WriteDebug("START zadani poctu vytisku");
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {



                    var dr = await InputBoxAsync.Show(_parent,
                        Title: "Tisk",
                        Message: "Počet výtisků",
                        Defaultvalue: pocetVytiskuStr,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Android.Text.InputTypes.ClassNumber);



                    if (dr.Dialog_Result == DialogResult.Cancel)
                        return false;

                    try
                    {
                        pocetVytisku = int.Parse(dr.Value);
                    }
                    catch (Exception ex)
                    {
                        await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    if (pocetVytisku <= 0)
                    {
                        await MessageBoxAsync.Show(_parent, "Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        await MessageBoxAsync.Show(_parent, "Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);

                dataHlavicka.Add("PocetVytisku", pocetVytisku.ToString());

                //Logging.Log.WriteDebug("END zadani poctu vytisku");
                //for (int i = 0; i < (pocetVytisku ?? 1); i++)
                //{
                //    while (true)
                //    {
                //        printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka, 1);
                //        if (!printed)
                //        {
                //            DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                //            if (drPrint == DialogResult.Yes)
                //                continue;
                //            else
                //                return false;
                //        }
                //        break;
                //    }
                //}

                bool printed;

                while (true)
                {
                    printed = Fask.PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejPaletaHlavicka, pocetVytisku ?? 1);
                    if (!printed)
                    {
                        DialogResult drPrint = await MessageBoxAsync.Show(_parent, "Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                        {
                            tcs.SetResult(false);
                            return tcs.Task.Result;                            
                        }
                    }
                    break;
                }
                tcs.SetResult(printed);
            }
            catch (WebException webex)
            {
                Fask.Logging.ExceptionHandler2.Handle(webex);
                await MessageBoxAsync.Show(_parent, webex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            finally
            {
            }

            return tcs.Task.Result;
        }

        private async static Task<bool> PrintSendToPrinter(
            AppCompatActivity _parent,
            Dictionary<string, string> printData,
            Fask.PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            //bool printed = false;

            try
            {

                if ((!pocetVytisku.HasValue) || (pocetVytisku > 100))
                {


                    string pocetVytiskuStr = pocetVytisku == null ? "1" : pocetVytisku.ToString();
                    do
                    {
                        var dr = await InputBoxAsync.Show(_parent,
                            Title: "Tisk",
                            Message: "Počet výtisků",
                            Defaultvalue: pocetVytiskuStr,
                            buttons: MessageBoxButtons.OKCancel,
                            keyboardMode: Android.Text.InputTypes.ClassNumber);



                        if (dr.Dialog_Result == DialogResult.Cancel)
                            return false;

                        try
                        {
                            pocetVytisku = int.Parse(dr.Value);
                        }
                        catch (Exception ex)
                        {
                            await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                            continue;
                        }

                        if (pocetVytisku <= 0)
                        {
                            await MessageBoxAsync.Show(_parent, "Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK);
                            continue;
                        }
                        if (pocetVytisku > 100)
                        {
                            await MessageBoxAsync.Show(_parent, "Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK);
                            continue;
                        }

                        //pokud az sem, tak ok ... pustit do tisku
                        break;
                    } while (true);

                }


                /* JiS : priprava modulovy tisk ... 
                PrintServerService.Tisk tiskps = new Fask.MST_W.PrintServerService.Tisk();
                tiskps.Url = MST_Global.PrintServerAddress + "Tisk.asmx";
                tiskps.Timeout = MST_Global.PrintServerTimeOut;

                // tisk bez návratové hodnoty (kvůli urychlení tisku)
                //tiskps.EtiketaBezNavratu(
                //    MST_Global.TerminalID,
                //    templateName,
                //    printParams,
                //    printData,
                //    pocetVytisku ?? 1);
                printed = tiskps.Etiketa(
                    MST_Global.TerminalID,
                    templateName,
                    printParams,
                    printData,
                    pocetVytisku ?? 1);
                */

                var printed = Fask.PrinterFactory.PrinterFactory.Instance.Print(printData, printerModule, pocetVytisku ?? 1);
                tcs.SetResult(printed);
                //return printed;
            }
            catch (WebException webex)
            {
                Fask.Logging.ExceptionHandler2.Handle(webex);
                await MessageBoxAsync.Show(_parent, webex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
                tcs.SetResult(false);
            }
            finally
            {
            }

            return tcs.Task.Result;
        }

    }
}