using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;


namespace Fask.MST_W.Vydej_3
{
	public partial class ListNasnimForm3
	{

		SejmiKodInfoForm3 sejmiFormLokace = new SejmiKodInfoForm3(string.Empty, SejmiKodForm.TypeOfCode.AlphaNumeric, null);

		private void smaz()
		{
			try
			{
				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;

				if ((MST_Global.Vydej_TypSPrelokovanim) && (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry[0].CONFIG_LOKACE_POVOLIT))
				{
					smaz_Prelokovani();
				}
				else
				{
					smaz_Default();
				}
			}
			catch (System.Exception ex)
			{

				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}



		private void smaz_Default()
		{
			try
			{
				if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListNasnimForm3SmazatPolozkuDotaz, Fask.Localization.Localization.Vydej3ListNasnimForm3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
						== DialogResult.No)
					return;

				decimal mnozstvi_polozky = vydejActual.QTYSHPPD;
				string itemnmbr = vydejActual.ITEMNMBR.Trim();
				string sopnumbe = vydejActual.SOPNUMBE.Trim();

				#region lokace
				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;
				if (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull()
					&&
					vydejDataParametry[0].CONFIG_LOKACE_POVOLIT)
				{
					// guid, ktery se maze
					Guid g = vydejActual.guid;


					Cursor.Current = Cursors.WaitCursor;

					//Volani sluzby a kontrola navratveho kodu.
					Logging.Log.WriteAdvanced(string.Empty, string.Empty);
					Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:" + Fask.MST_W.LokaceService.TypeOfRecord.D + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + g.ToString(), "LocationLog");
					// odstraneni online zaznamu
					Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(g, Fask.MST_W.LokaceService.ModulName.VYDEJ);

					Cursor.Current = Cursors.Default;
					switch (sl.State)
					{
						case Fask.MST_W.LokaceService.States.OK:
							break;
						case Fask.MST_W.LokaceService.States.ERROR:
							MessageBoxBig.Show("Nepodařilo se odstranit záznamy lokací - nasnímané množství nebude smazáno! - " + sl.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						default:
							MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznamy lokací - nasnímané množství nebude smazáno!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
					}

					Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:" + Fask.MST_W.LokaceService.TypeOfRecord.D + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");
				}
				#endregion

				int ord = vydejActual.ORD;
				vydejActual.Delete();
				Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Update_One(vydejActual);
				//Vydej.vydejInstance.globalObject.controller_vydej.Ta_si.DeleteQueryByGuid(vydejActual.guid);
				ListPolozek3.Instance.UpdateDataGrid(-mnozstvi_polozky, itemnmbr, sopnumbe, ord);
				countItems--;
				if (indexItem > countItems)
					indexItem = countItems;
				LoadRow(indexItem);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}


		private void smaz_Prelokovani()
		{
			try
			{

				var SE_dt = Vydej.vydejInstance.globalObject.controller_vydej.GetDataBySOPNUMBEITEMNMBRORD_SE(vydejActual.SOPNUMBE, vydejActual.ITEMNMBR, vydejActual.ORD);
				var SE_Row = SE_dt.First();

				string msg = string.Format(
							"Příjmout položku: {0}" +
							Environment.NewLine +
							"ID(Kód): {1}({2})" +
							Environment.NewLine +
							"Množství: {3}" +
							Environment.NewLine +
							"Šarže: {4}" +
							Environment.NewLine
							, SE_Row.ITEMDESC, vydejActual.ITEMNMBR, vydejActual.ITEMCODE, vydejActual.QTYSHPPD, vydejActual.SERLTNUM);

				if (MessageBoxBig.Show("Chcete příjmout tuto položku? ", Fask.Localization.Localization.Vydej3ListNasnimForm3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
						== DialogResult.No)
					return;

				decimal mnozstvi_polozky = vydejActual.QTYSHPPD;
				string itemnmbr = vydejActual.ITEMNMBR.Trim();
				string sopnumbe = vydejActual.SOPNUMBE.Trim();

				#region lokace

				var vydejDataParametry = Vydej.vydejInstance.globalObject.vydejData.Parametry;

				if (!vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry[0].CONFIG_LOKACE_POVOLIT)
				{

					DialogResult dr = MessageBoxBig.Show(msg, "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

					if (dr == DialogResult.Yes)
					{

						sejmiFormLokace.SetDefaultValues();
						sejmiFormLokace.Popis = Fask.Localization.Localization.Vydej3ListPolozek3Zadejcilovoulokaci;
						sejmiFormLokace.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						sejmiFormLokace.Len = 0;
						sejmiFormLokace.CheckLen = false;
						sejmiFormLokace.AllowEmpty = false;
						sejmiFormLokace.veRow = SE_Row;
						sejmiFormLokace.viRow = vydejActual;
						sejmiFormLokace.ScannerCheckOnly = vydejDataParametry[0].CONFIG_LOCNCODE_OVERIT_SCANEREM;
						// TODO: zobrazit tlacitko lokaci ...
						sejmiFormLokace.btnZobrazitAlternativniLokaceVisible = vydejDataParametry[0].IsCONFIG_LOKACE_POVOLITNull() ? false : vydejDataParametry[0].CONFIG_LOKACE_POVOLIT;

						if (sejmiFormLokace.ShowDialog() == DialogResult.Cancel)
							return;

						string LOC = sejmiFormLokace.Kod;

						var stav = Prijem(
							itemnmbr,
							string.Empty,
							vydejActual.SOPNUMBE,
							mnozstvi_polozky,
							vydejActual.SERLTNUM,
							vydejActual.SKL_ID,
							LOC,
							Guid.NewGuid(),
							null,
							vydejActual.CountEntries
							);

						if (!stav)
							return;

					}
				}
				#endregion

				int ord = vydejActual.ORD;
				vydejActual.Delete();
				Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Update_One(vydejActual);
				//Vydej.vydejInstance.globalObject.controller_vydej.Ta_si.DeleteQueryByGuid(vydejActual.guid);
				ListPolozek3.Instance.UpdateDataGrid(-mnozstvi_polozky, itemnmbr, sopnumbe, ord);
				countItems--;
				if (indexItem > countItems)
					indexItem = countItems;
				LoadRow(indexItem);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}


		#region Pomocne metody

		private bool Prijem(
			string ITEMNMBR,
			string ITEMDESC,
			string SOPNUMBE,
			decimal QTY,
			string SERLTNUM,
			string SKL_ID,
			string LOCNCODE,
			Guid newguid,
			DateTime? expirace,
			int? CountEntries
			)
		{
			try
			{
				Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
				pohybrow.ITEMNMBR = ITEMNMBR;
				pohybrow.DOCUMENT_NUMBER = SOPNUMBE;
				pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.P;
				pohybrow.POHYB_SRC = "P";
				pohybrow.SOURCE = "T";
				pohybrow.QTYSHPPD = QTY;
				pohybrow.SERLTNUM = SERLTNUM;
				pohybrow.SKL_ID_SRC = SKL_ID;
				pohybrow.SKL_ID_DST = string.Empty;
				pohybrow.LOCNCODE_SRC = LOCNCODE;
				pohybrow.LOCNCODE_DST = string.Empty;
				pohybrow.UserID = MST_Global.UserID;
				pohybrow.TermID = MST_Global.TerminalID;
				pohybrow.guid = newguid;
				pohybrow.Expiration = expirace;
				pohybrow.ITEMDESC = ITEMDESC;
				pohybrow.CountEntries = CountEntries;
				pohybrow.dateeveT = DateTime.Now;

				try
				{
					Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - start", "LocationLog");
					Classes.LokaceLog.writeBody(pohybrow);

					Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.AddRecord(pohybrow);
					Cursor.Current = Cursors.Default;

					switch (sl.State)
					{
						case Fask.MST_W.LokaceService.States.OK:
							return true;
						case Fask.MST_W.LokaceService.States.ERROR:
							MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return false;
						default:
							MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return false;
					}

					Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - end", "LocationLog");
				}
				catch (Exception ex)
				{
					Logging.Log.WriteDebug(ex.Message);
					Cursor.Current = Cursors.Default;

					return false;
				}

				return true;
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}


		#endregion

	}
}
