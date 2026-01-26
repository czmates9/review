using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Fask.MST_W.Forms;

namespace Fask.Vyroba_W.Odvadeni.Tisk
{
	public class OdvedeniTisk
	{
		public static bool Print(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow P, PrinterFactory.PrinterModules templatename, int? pocetVytisku)
		{
			try
			{
				Dictionary<string, string> printData = new Dictionary<string, string>();

				if (P != null) //pokud neni predano, tak se neplni ... !!! pretizene metody ... 
				{
					foreach (System.Data.DataColumn dcol in P.Table.Columns)
					{
						if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
						{
							printData.Add(dcol.ColumnName.ToUpper(), P[dcol.ColumnName].ToString().Trim());
						}
					}

					return PrintSendToPrinter(printData, templatename, pocetVytisku);
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		private static bool PrintSendToPrinter(
	Dictionary<string, string> printData,
	PrinterFactory.PrinterModules templateName,
	int? pocetVytisku
	)
		{
			bool printed = false;

			try
			{
				if ((!pocetVytisku.HasValue) || (pocetVytisku > 100))
				{
					//string pocetVytiskuStr = pocetVytisku.ToString();
					string pocetVytiskuStr = pocetVytisku == null ? "1" : pocetVytisku.ToString(); //pokud nema hodnotu, tak auto 1 ...

					do
					{
						DialogResult dr = InputBox.Show("Počet výtisku", pocetVytiskuStr, out pocetVytiskuStr);
						if (dr == DialogResult.Cancel)
							return false;

						try
						{
							pocetVytisku = int.Parse(pocetVytiskuStr);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
							continue;
						}

						if (pocetVytisku <= 0)
						{
							MessageBox.Show("Počet Musi Byt Vetsi Nez 0", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
							continue;
						}
						if (pocetVytisku > 100)
						{
							MessageBox.Show("Počet Musi Byt Mensi Nez 100", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
							continue;
						}

						//pokud az sem, tak ok ... pustit do tisku
						break;
					} while (true);
				}

				Cursor.Current = Cursors.WaitCursor;

				printed = PrinterFactory.PrinterFactory.Instance.Print(printData, templateName, pocetVytisku ?? 1);

				return printed;
			}
			catch (WebException webex)
			{
				Cursor.Current = Cursors.Default;
				Logging.ExceptionHandler2.Handle(webex);
				return printed;
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.ExceptionHandler2.Handle(ex);
				return printed;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
	}
}
