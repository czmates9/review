using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.Forms;
using System.Windows.Forms;
using System.Net;

namespace Fask.MST_W.Expedice
{
    public class ExpediceTisk
    {
        public static bool Print(
            Dictionary<string, string> data,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetvytisku
            )
        {
            return Print(null, data, printerModule, pocetvytisku);
        }

        public static bool Print(
            Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow rowPolozka,
            PrinterFactory.PrinterModules printerModule //string templateName
            )
        {
            return Print(rowPolozka, null, printerModule, null);
        }

        public static bool Print(
            Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow rowPolozka,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
        {
            return Print(rowPolozka, null, printerModule, pocetVytisku);
        }

        public static bool Print(
            Fask.MST_W.ExpediceService.ExpediceBaleni.CZMST_Expedice_Baleni_PolozkyRow rowPolozka,
            Dictionary<string, string> data,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.KeyColumn };

                if (rowPolozka != null)
                {
                    foreach (System.Data.DataColumn dcol in rowPolozka.Table.Columns)
                    {
                        //if (!printData.Values.Rows.Contains(dcol.ColumnName.ToUpper()))
                        if (!printData.ContainsKey(dcol.ColumnName.ToUpper()))
                        {
                            //printData.Values.AddValuesRow(dcol.ColumnName.ToUpper(), row095[dcol.ColumnName].ToString().Trim());
                            printData.Add(dcol.ColumnName.ToUpper(), rowPolozka[dcol.ColumnName].ToString().Trim());
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
                return PrintSendToPrinter(printData, printerModule, pocetVytisku);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
        }

        public static bool PrintSoupisSendToPrinter(
            Dictionary<string, string> dataHlavicka,
            List<Dictionary<string, string>> dataRadky,
            Dictionary<string, string> dataPaticka,
            int? pocetVytisku
            )
        {
            bool printed = false;

            try
            {
                //Logging.Log.WriteDebug("START zadani poctu vytisku");
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    if (dr == DialogResult.Cancel)
                        return false;

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
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

                //Logging.Log.WriteDebug("END zadani poctu vytisku");
                Cursor.Current = Cursors.WaitCursor;
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
                while (true)
                {
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ExpediceSoupisHlavicka, pocetVytisku ?? 1);
                    if (!printed)
                    {
                        DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                            return false;
                    }
                    break;
                }
                return printed;
            }
            catch (WebException webex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(webex);
                MessageBoxBig.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static bool PrintPaletaSendToPrinter(
            Dictionary<string, string> dataHlavicka,
            List<Dictionary<string, string>> dataRadky,
            Dictionary<string, string> dataPaticka,
            int? pocetVytisku
            )
        {
            bool printed = false;

            try
            {
                //Logging.Log.WriteDebug("START zadani poctu vytisku");
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    if (dr == DialogResult.Cancel)
                        return false;

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
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

                //Logging.Log.WriteDebug("END zadani poctu vytisku");
                Cursor.Current = Cursors.WaitCursor;

                while (true)
                {
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ExpedicePaletaHlavicka, pocetVytisku ?? 1);
                    if (!printed)
                    {
                        DialogResult drPrint = MessageBoxBig.Show("Vytištění se nezdařilo.\nOpakovat?", "Tisk", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                        if (drPrint == DialogResult.Yes)
                            continue;
                        else
                            return false;
                    }
                    break;
                }
                return printed;
            }
            catch (WebException webex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(webex);
                MessageBoxBig.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private static bool PrintSendToPrinter(
            Dictionary<string, string> printData,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
       { 
            bool printed = false;

            try
            {
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    if (dr == DialogResult.Cancel)
                        return false;

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
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
