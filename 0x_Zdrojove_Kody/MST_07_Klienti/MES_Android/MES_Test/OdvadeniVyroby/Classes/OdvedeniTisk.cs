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
using System.Data;

namespace MES_Android.OdvadeniVyroby.Classes
{
    public class OdvedeniTisk
    {

		public async static Task<bool> PrintPaleta(
			AppCompatActivity _parent, 
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow_data
			)
		{

			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

			try
			{

				Dictionary<string, string> printData = new Dictionary<string, string>();

				if (productionRow_data != null) //pokud neni predano, tak se neplni ... !!! pretizene metody ... 
				{

					//skladani caroveho kodu GS1 --START---------------------
					//zadat key
					//zadat value

					string GS1_KOD_1_1D = string.Empty;
					string GS1_KOD_1_TX = string.Empty;
					string GS1_KOD_2_1D = string.Empty;
					string GS1_KOD_2_TX = string.Empty;
					string SSCC = string.Empty;
					string SSCC_bez_nul = string.Empty;
					string WEIGHT = string.Empty;

					//------------START-DATA----------------
					//dotahovat data SSCC a WEIGHT

					if (!productionRow_data.IsWEIGHTNull())
					{
						WEIGHT = productionRow_data.WEIGHT.ToString();
					}

					if (!printData.ContainsKey("WEIGHT"))
						printData.Add("WEIGHT", WEIGHT);

					if (!productionRow_data.IsNMBRPALNull())
					{
						//SSCC a WEIGHT doplnit logiku dohledani
						//SSCC a WEIGHT natvrdo zadano
						SSCC = productionRow_data.NMBRPAL;
						SSCC_bez_nul = SSCC.Substring(2);
					}

					if (!printData.ContainsKey("SSCC"))
						printData.Add("SSCC", SSCC_bez_nul);

					//------------END-DATA----------------

					GS1_KOD_1_1D = "02" + productionRow_data.BarcodeP.PadLeft(14, '0') + "37" + productionRow_data.qty.ToString("0000") + "";
					GS1_KOD_1_TX = "(02)" + productionRow_data.BarcodeP.PadLeft(14, '0') + "(37)" + productionRow_data.qty.ToString("0000") + ""; //(02) (37)
					GS1_KOD_2_1D = SSCC; //SSCC neni v production
					GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production

					if (!printData.ContainsKey("GS1_KOD_1_1D"))
						printData.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

					if (!printData.ContainsKey("GS1_KOD_1_TX"))
						printData.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

					if (!printData.ContainsKey("GS1_KOD_2_1D"))
						printData.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

					if (!printData.ContainsKey("GS1_KOD_2_TX"))
						printData.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

					//skladani caroveho kodu GS1 --END---------------------
					if (!printData.ContainsKey("SOURCE"))
						printData.Add("SOURCE", "Automat");

					Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
					foreach (DataColumn dcol in dt.Columns)
					{
						string key = dcol.ColumnName;
						string value = productionRow_data[dcol.ColumnName].ToString();
						if (!printData.ContainsKey(key))
							printData.Add(key, value);
					}

					var x = await PrintSendToPrinter(_parent, printData, Fask.PrinterFactory.PrinterModules.VyrobaPaletovylistek, null);
					tcs.SetResult(x);

				}
				else
				{
					tcs.SetResult(false);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

			return tcs.Task.Result;

		}



		public async static Task<bool> Print(AppCompatActivity _parent, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow P, Fask.PrinterFactory.PrinterModules templatename, int? pocetVytisku)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

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

					var x = await PrintSendToPrinter(_parent, printData, templatename, pocetVytisku);
					tcs.SetResult(x);

				}
				else
				{
					tcs.SetResult(false);
				}
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

			return tcs.Task.Result;
		}

		public async static Task<bool> PrintSendToPrinter(
			 AppCompatActivity _parent,
	Dictionary<string, string> printData,
	Fask.PrinterFactory.PrinterModules templateName,
	int? pocetVytisku
	)
		{

			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			bool printed = false;

			try
			{
				if ((!pocetVytisku.HasValue) || (pocetVytisku > 100))
				{
					//string pocetVytiskuStr = pocetVytisku.ToString();
					string pocetVytiskuStr = pocetVytisku == null ? "1" : pocetVytisku.ToString(); //pokud nema hodnotu, tak auto 1 ...

					do
					{

						#region MyRegion

						if (!Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviJednaAutomaticky)
						{
							if (string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit))
							{
								var dr = await InputBoxAsync.Show(
										_parent,
										Title: "Tisk",
										Message: "Počet výtisků",
										Defaultvalue: pocetVytiskuStr,
										buttons: MessageBoxButtons.OKCancel,
										keyboardMode: Android.Text.InputTypes.ClassNumber);

								pocetVytiskuStr = dr.Value;

								if (dr.Dialog_Result == DialogResult.Cancel)
									return false;
							}
							else if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit) 
								&& int.Parse(Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit) <= 0)
							{
								var dr = await InputBoxAsync.Show(
										_parent,
										Title: "Tisk",
										Message: "Počet výtisků",
										Defaultvalue: pocetVytiskuStr,
										buttons: MessageBoxButtons.OKCancel,
										keyboardMode: Android.Text.InputTypes.ClassNumber);

								pocetVytiskuStr = dr.Value;

								if (dr.Dialog_Result == DialogResult.Cancel)
									return false;
							}
							else if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit) 
								&& int.Parse(Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit) > 0)
							{
								pocetVytiskuStr = Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_MnozstviPredvyplnit;
							}
						}
						else 
						{
							pocetVytiskuStr = "1";
						}

						#endregion


						try
						{
							pocetVytisku = int.Parse(pocetVytiskuStr);
						}
						catch (Exception ex)
						{
							await MessageBoxAsync.Show(_parent, ex.Message, "Printing", MessageBoxButtons.OK);
							continue;
						}

						if (pocetVytisku <= 0)
						{
							await MessageBoxAsync.Show(_parent, "Počet Musi Byt Vetsi Nez 0", "Printing", MessageBoxButtons.OK);
							continue;
						}
						if (pocetVytisku > 100)
						{
							await MessageBoxAsync.Show(_parent, "Počet Musi Byt Mensi Nez 100", "Printing", MessageBoxButtons.OK);
							continue;
						}

						//pokud az sem, tak ok ... pustit do tisku
						break;
					} while (true);
				}


				printed = Fask.PrinterFactory.PrinterFactory.Instance.Print(printData, templateName, pocetVytisku ?? 1);
				tcs.SetResult(printed);

			}
			catch (WebException webex)
			{
				Fask.Logging.ExceptionHandler2.Handle(webex);
				tcs.SetException(webex);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				tcs.SetException(ex);
			}

			return tcs.Task.Result;
		}
	}
}