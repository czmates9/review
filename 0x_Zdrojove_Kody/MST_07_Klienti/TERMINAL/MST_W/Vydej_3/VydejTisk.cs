using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Vydej_3
{
    public class VydejTisk
    {
        public static bool Print(Dictionary<string, string> data, Fask.PrinterFactory.PrinterModules templatename)
        {
            try
            {
                //return Print(printParams, printData, templatename, 1);
                return PrintSendToPrinterFactory(data, templatename, null);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
            finally
            {
            }
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow se, Fask.PrinterFactory.PrinterModules templatename)
        {
            //return Print(pe, templatename, Convert.ToInt32(pe.Zbyva));
            return Print(se, templatename, null);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow se, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                foreach (System.Data.DataColumn dcol in se.Table.Columns)
                {
                    printData.Add(dcol.ColumnName.ToUpper(), se[dcol.ColumnName].ToString().Trim());
                }                

                ////události obsluhy
                //Program.mstw.eventsUser.add(new EventsUser.EventUser());

                return PrintSendToPrinterFactory(printData, templatename, pocetVytisku);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow se, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow si, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.ValueColumn };

                foreach (System.Data.DataColumn dcol in si.Table.Columns)
                {
                    if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                    {
                        printData.Add(dcol.ColumnName.ToUpper(), si[dcol.ColumnName].ToString().Trim());
                    }
                }

                foreach (System.Data.DataColumn dcol in se.Table.Columns)
                {
                    if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                    {
                        printData.Add(dcol.ColumnName.ToUpper(), se[dcol.ColumnName].ToString().Trim());
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
                string pocetVytiskuStr = pocetVytisku.ToString();
                
                do
                {
                    //DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr);
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
				if ((!pocetVytisku.HasValue) || (pocetVytisku > 100))
				{
					//Logging.Log.WriteDebug("START zadani poctu vytisku");
					//string pocetVytiskuStr = pocetVytisku.ToString();
					string pocetVytiskuStr = pocetVytisku == null ? "1" : pocetVytisku.ToString(); //pokud nema hodnotu, tak auto 1 ...
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
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.VydejPaletaHlavicka, pocetVytisku ?? 1);
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



        public static bool PrintSoupiskaSendToPrinter(
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
                
				if (!MST_Global.VydejTiskSoupisMN_Auto1)
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
				}

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
                    printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.VydejSoupiskaHlavicka, pocetVytisku ?? 1);
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




    }
}
