using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Fask.MST_W.Forms;
using System.Windows.Forms;
using System.Data;

namespace Fask.MST_W.Inventura1_sqlce
{
    public class InventuraTisk
    {
        public static bool Print(ListPolozky.PolozkyRow row, Fask.PrinterFactory.PrinterModules templatename)
        {
            return Print(null, null, null, row, templatename, null);
        }

        public static bool Print(ListPolozky.PolozkyRow row, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            return Print(null, null, null, row, templatename, pocetVytisku);
        }

        public static bool Print(Dictionary<string, string> data, Fask.PrinterFactory.PrinterModules templatename)
        {
            return Print(null, null, data, null, templatename, null);
        }

        public static bool Print(Dictionary<string, string> data, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            return Print(null, null, data, null, templatename, pocetVytisku);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1Row rowI1, Fask.PrinterFactory.PrinterModules templatename)
        {
            return Print(rowI1, null, null, null, templatename, null);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1Row rowI1, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            return Print(rowI1, null, null, null, templatename, pocetVytisku);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1Row rowI1, Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4Row rowI4, Dictionary<string, string> data, ListPolozky.PolozkyRow rowPolozky, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            try
            {
                //MST_W.PrintServerService.DSValues printData = new Fask.MST_W.PrintServerService.DSValues();
                Dictionary<string, string> printData = new Dictionary<string, string>();

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.ValueColumn };

                if (rowI4 != null)
                {
                    foreach (System.Data.DataColumn dcol in rowI4.Table.Columns)
                    {
                        if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), rowI4[dcol.ColumnName].ToString().Trim());
                        }
                    }
                }

                if (rowI1 != null)
                {
                    foreach (System.Data.DataColumn dcol in rowI1.Table.Columns)
                    {
                        if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), rowI1[dcol.ColumnName].ToString().Trim());
                        }
                    }
                }

                if (data != null)
                {
                    foreach (string key in data.Keys)
                    {
                        if (!printData.Keys.Contains(key))
                            printData.Add(key, data[key].Trim());
                    }
                }

                if (rowPolozky != null)
                {
                    foreach (DataColumn dcol in rowPolozky.Table.Columns)
                    {
                        if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), rowPolozky[dcol.ColumnName].ToString().Trim());
                        }
                    }
                }

                return PrintSendToPrinterFactory(printData, templatename, pocetVytisku);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
        }

        private static bool PrintSendToPrinterFactory(
            Dictionary<string, string> printData,
            Fask.PrinterFactory.PrinterModules templateName,
            int? pocetVytisku
            )
        {
            bool printed = false;

            try
            {
                // ulozeni puvodniho stavu klavesnice
                //Components.KeyboardManager.SaveDefaultKeyboardMode();                
                string pocetVytiskuStr = pocetVytisku.ToString();
                do
                {
                    // prepnuti na numerickou klavesnici
                    Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    DialogResult dr = InputBox.Show(Fask.Localization.Localization.Inventura1InventuraTiskPocetVytisku, pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
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
                        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1InventuraTiskPocetMusiBytVetsiNez0, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        MessageBoxBig.Show(Fask.Localization.Localization.Inventura1InventuraTiskPocetMusiBytMensiNez100, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);                
                Cursor.Current = Cursors.WaitCursor;

                //printed = tiskps.Etiketa(
                //    MST_Global.TerminalID,
                //    templateName,
                //    printParams,
                //    printData,
                //    pocetVytisku ?? 1);

                printed = PrinterFactory.PrinterFactory.Instance.Print(printData, templateName, pocetVytisku ?? 1);

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
                // nacteni puvodniho stavu klavesnice
                //Components.KeyboardManager.LoadDefaultKeyboardMode();
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
