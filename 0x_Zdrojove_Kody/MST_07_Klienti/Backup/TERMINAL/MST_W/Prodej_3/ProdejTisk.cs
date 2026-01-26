using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Fask.MST_W.Forms;
using System.Windows.Forms;

namespace Fask.MST_W.Prodej_3
{
    public class ProdejTisk
    {

        public static bool Print(
            Dictionary<string, string> data, 
            PrinterFactory.PrinterModules printerModule //string templateName
            )
        {
            return Print(null, null, data, printerModule, null, null, null);
        }

        public static bool Print(
            Dictionary<string, string> data,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetvytisku
            )
        {
            return Print(null, null, data, printerModule, pocetvytisku, null, null);
        }

        public static bool Print(
            Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row095, 
            PrinterFactory.PrinterModules printerModule //string templateName
            )
        {            
            return Print(row095, null, null, printerModule, null, null, null);
        }

        public static bool Print(
            Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row095,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
        {
            return Print(row095, null, null, printerModule, pocetVytisku, null, null);
        }

		public static bool Print(
			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row095,
			PrinterFactory.PrinterModules printerModule,
			int? pocetVytisku,
			bool ? TiskSCenou,
			string SerNumTrack
			)
		{
			return Print(row095, null, null, printerModule, pocetVytisku, TiskSCenou, SerNumTrack);
		}

        public static bool Print(
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI, 
            PrinterFactory.PrinterModules printerModule //string templateName
            )
        {
            return Print(null, rowDI, null, printerModule, null, null, null);
        }

        public static bool Print(
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI,
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku
            )
        {
            return Print(null, rowDI, null, printerModule, pocetVytisku, null, null);
        }

		public static bool Print(
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI,
			PrinterFactory.PrinterModules printerModule, //string templateName,
			int? pocetVytisku,
			bool? TiskSCenou,
			string SerNumTrack
			)
		{
			return Print(null, rowDI, null, printerModule, pocetVytisku, TiskSCenou, SerNumTrack);
		}

        public static bool Print(
            Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row095, 
            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow rowDI, 
            Dictionary<string, string> data, 
            PrinterFactory.PrinterModules printerModule, //string templateName,
            int? pocetVytisku,
			bool? TiskSCenou,
			string SerNumTrack
            )
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.KeyColumn };

				//if (Prodej.Globals.EtiketaTiskDotazSCenou)
				//{
				if (TiskSCenou != null)
				{
					printData.Add("TISKCENA", TiskSCenou.ToString());
				}

				if (SerNumTrack != null)
				{
					printData.Add("CZ_SERNUM_TRACK", SerNumTrack);
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
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka, pocetVytisku ?? 1);
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
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.ProdejPaletaHlavicka, pocetVytisku ?? 1);
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

				if ((!pocetVytisku.HasValue) || (pocetVytisku > 100))
				{


					string pocetVytiskuStr = pocetVytisku == null ? "1" : pocetVytisku.ToString();
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

				}

                Cursor.Current = Cursors.WaitCursor;
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
