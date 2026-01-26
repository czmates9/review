using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public class PrijemTisk
    {
        public static bool Print(Dictionary<string, string> data, PrinterFactory.PrinterModules templatename)
        {
            try
            {
                //return Print(printParams, printData, templatename, 1);
                return PrintSendToPrinter(data, templatename, null);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return false;
            }
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, PrinterFactory.PrinterModules templatename)
        {
            return Print(pe, templatename, null);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
            return Print(pe, null, templatename, pocetVytisku);
        }

        public static bool Print(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi, PrinterFactory.PrinterModules templatename, int? pocetVytisku)
        {
			return Print(pe, pi, null, templatename, pocetVytisku, null, null);
        }

		public static bool Print(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi, PrinterFactory.PrinterModules templatename, int? pocetVytisku, bool? TiskCena, string SerNumTrack)
		{
			return Print(pe, pi, null, templatename, pocetVytisku, TiskCena, SerNumTrack);
		}

		public static bool Print(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe, Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi, Dictionary<string, string> data, PrinterFactory.PrinterModules templatename, int? pocetVytisku, bool? tisksCenou, string SerNumTrack)
        {
            try
            {
                Dictionary<string, string> printData = new Dictionary<string, string>();
                if (data == null)
                    data = new Dictionary<string, string>();

                if (!data.ContainsKey("DATUMPRIJMU"))
                    data.Add("DATUMPRIJMU", DateTime.Now.ToString("d.M.yyyy"));

                // doplnění množství v jiném formátu
                if (pi != null && !data.ContainsKey("QTYSHPPDMJ2"))
                    data.Add("QTYSHPPDMJ2", pi.QTYSHPPDMJ.ToString("0.0"));

                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.ValueColumn };
                //printData.Values.PrimaryKey = new System.Data.DataColumn[] { printData.Values.KeyColumn }; // oprava key column je primarni ...

				//if (Globals.EtiketaTiskDotazSCenou)
				//{
					if (tisksCenou != null)
					{ printData.Add("TISKCENA", tisksCenou.ToString()); }

				//}

					if (SerNumTrack != null)
					{
						printData.Add("CZ_SERNUM_TRACK", SerNumTrack);
					}

                if (pi != null) //pokud neni predano, tak se neplni ... !!! pretizene metody ... 
                {
                    foreach (System.Data.DataColumn dcol in pi.Table.Columns)
                    {
                        if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), pi[dcol.ColumnName].ToString().Trim());
                        }
                        //else
                        //{
                        //    printData.Values[printData.Values.Rows.IndexOf(printData.Values.Rows.Find(dcol.ColumnName.ToUpper()))].Value = pi[dcol.ColumnName].ToString().Trim();
                        //}
                    }
                }

                if (pe != null)
                {
                    foreach (System.Data.DataColumn dcol in pe.Table.Columns)
                    {
                        if (!printData.Keys.Contains(dcol.ColumnName.ToUpper()))
                        {
                            printData.Add(dcol.ColumnName.ToUpper(), pe[dcol.ColumnName].ToString().Trim());
                        }
                        //else
                        //{
                        //    printData.Values[printData.Values.Rows.IndexOf(printData.Values.Rows.Find(dcol.ColumnName.ToUpper()))].Value = pe[dcol.ColumnName].ToString().Trim();
                        //}
                    }
                }

                if (data != null)
                {
                    foreach (string key in data.Keys)
                    {
                        if (!printData.Keys.Contains(key))
                            printData.Add(key, data[key].Trim());
                        //else
                        //    printData.Values[printData.Values.Rows.IndexOf(printData.Values.Rows.Find(key))].Value = data[key].Trim();
                    }
                }

                if (MST_Global.OnlineObjednavkaDetailPovolit) //dotahnout informace o objednavce
                {
                    string ponumber = string.Empty;
                    string sklid = string.Empty;
                    string itemnumber = string.Empty;
                    string itemorder = string.Empty;
                    string serltnum = string.Empty;

                    if (pi != null)
                    {
                        ponumber = pi.PONUMBER.Trim();
                        sklid = pi.IsSKL_IDNull() ? string.Empty : pi.SKL_ID.Trim();
                        itemnumber = pi.ITEMNMBR.Trim();
                        itemorder = pi.ORD.ToString();
                        serltnum = pi.SERLTNUM.Trim();
                    }
                    else if (pe != null)
                    {
                        ponumber = pe.PONUMBER.Trim();
                        sklid = pe.IsSKL_IDNull() ? string.Empty : pe.SKL_ID.Trim();
                        itemnumber = pe.ITEMNMBR.Trim();
                        itemorder = pe.ORD.ToString();
                    }


                    if (!string.IsNullOrEmpty(ponumber) && !string.IsNullOrEmpty(itemnumber))
                    {
                        DataSet detailObjednavky_ds = null;

                        // TODO : opakovat dokud se nezdari, nebo ukoncit ... 
                        // message??? retry/cancel ... 
                        // !!! vyse muze byt waitdialog ... nutne ukoncit ??? => overit zda messageboxbig je tom level ... ???
                        detailObjednavky_ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.DetailItem(ponumber.Trim(), sklid, itemnumber, itemorder, serltnum);

                        if (detailObjednavky_ds != null && detailObjednavky_ds.Tables.Count > 0 && detailObjednavky_ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataColumn dcol in detailObjednavky_ds.Tables[0].Columns)
                            {
                                if (!printData.Keys.Contains(dcol.ColumnName.Trim()))
                                    printData.Add(dcol.ColumnName.Trim(), detailObjednavky_ds.Tables[0].Rows[0][dcol].ToString());
                            }
                        }
                    }
                    //else // TODO : 2 dotazy nebo jen jeden ???
                    if (!string.IsNullOrEmpty(ponumber))
                    {
                        DataSet detailObjednavky_ds = null;

                        // TODO : opakovat dokud se nezdari, nebo ukoncit ... 
                        // message??? retry/cancel ... 
                        // !!! vyse muze byt waitdialog ... nutne ukoncit ??? => overit zda messageboxbig je tom level ... ???
                        detailObjednavky_ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Detail(ponumber.Trim(), sklid);

                        if (detailObjednavky_ds != null && detailObjednavky_ds.Tables.Count > 0 && detailObjednavky_ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataColumn dcol in detailObjednavky_ds.Tables[0].Columns)
                            {
                                if (!printData.Keys.Contains(dcol.ColumnName.Trim()))
                                    printData.Add(dcol.ColumnName.Trim(), detailObjednavky_ds.Tables[0].Rows[0][dcol].ToString());
                            }
                        }
                    }
                }

                return PrintSendToPrinter(printData, templatename, pocetVytisku);

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
                        DialogResult dr = InputBox.Show(Fask.Localization.Localization.Prijem4PrijemPrijemTiskPocetVytisku, pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
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
                            MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemPrijemTiskPocetMusiBytVetsiNez0, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            continue;
                        }
                        if (pocetVytisku > 100)
                        {
                            MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemPrijemTiskPocetMusiBytMensiNez100, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            continue;
                        }

                        //pokud az sem, tak ok ... pustit do tisku
                        break;
                    } while (true);                    
                } 

                Cursor.Current = Cursors.WaitCursor;
                //PrintServerService.Tisk tiskps = new Fask.MST_W.PrintServerService.Tisk();
                //tiskps.Url = MST_Global.PrintServerAddress + "Tisk.asmx";
                //tiskps.Timeout = MST_Global.PrintServerTimeOut;
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

				while (true)
				{
					printed = PrinterFactory.PrinterFactory.Instance.Print(dataHlavicka, dataRadky, dataPaticka, Fask.PrinterFactory.PrinterModules.PrijemPaletaHlavicka, pocetVytisku ?? 1);
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
