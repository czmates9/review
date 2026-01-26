using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;
using Fask.Parsing.Codes.Interfaces;
using Fask.Parsing.Codes;

namespace Fask.MST_W.Vydej_3
{

	/// <summary>
	/// Enumkteré určuje typ tisku
	/// </summary>
	public enum Varianta_TiskSoupis
	{
		I_Tec,
		CARP,
		TierraVerde
	}

	public enum Potvrzeni_AnoNe
	{
		Ano,
		Ne
	}

	/// <summary>
	/// Jendá se o třidu která nese informace o stavu zda tisk proběhl korektně
	/// </summary>
	public class ReturnState
	{
		/// <summary>
		/// Objekt DialogResult který nese informace o stavu navratu
		/// </summary>
		private DialogResult _dr;
		public DialogResult dr
		{
			set{ _dr = value;}
			get { return _dr;} 
		}

		/// <summary>
		/// Stringova promenna kerá nese spravu o stavu
		/// </summary>
		public string _message;
		public string Message
		{
			set { _message = value; }
			get { return _message; }
		}

		/// <summary>
		/// Implicitny konstruktor
		/// </summary>
		public ReturnState()
		{
			dr = DialogResult.OK;
			Message = "OK";
		}

		/// <summary>
		/// kopy konstruktor
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="message"></param>
		public ReturnState(DialogResult dr, string message) :this()
		{
			this._dr = dr;
			this._message = message;
		}
	}


	public partial class ListPolozek3
	{

		#region Tisk

		private ReturnState TiskSoupiskaByType(out Dictionary<string, string> dataHlavicka, out List<Dictionary<string, string>> dataRadky, out Dictionary<string, string> dataPaticka)
		{
			dataHlavicka = new Dictionary<string, string>();
			dataRadky = new List<Dictionary<string, string>>();
			dataPaticka = new Dictionary<string, string>();

			switch (MST_Global.VydejTiskVariantaSoupis)
			{
				case Varianta_TiskSoupis.I_Tec:
					return TiskSoupiska_I_Tec(out dataHlavicka, out dataRadky, out dataPaticka);
				case Varianta_TiskSoupis.CARP:
					return TiskSoupiska_CARP(out dataHlavicka, out dataRadky, out dataPaticka);
				case Varianta_TiskSoupis.TierraVerde:
					return TiskSoupiska_TierraVerde(out dataHlavicka, out dataRadky, out dataPaticka);
				default:
					return new ReturnState();
			}

			throw new NotImplementedException();
		}

		/// <summary>
		/// Metoda pro zakaznika I-Tec s upravou tisku
		/// </summary>
		/// <param name="dataHlavicka"></param>
		/// <param name="dataRadky"></param>
		/// <param name="dataPaticka"></param>
		/// <returns></returns>
		private ReturnState TiskSoupiska_I_Tec(out Dictionary<string, string> dataHlavicka, out List<Dictionary<string, string>> dataRadky, out Dictionary<string, string> dataPaticka)
		{
			dataHlavicka = new Dictionary<string, string>();
			dataRadky = new List<Dictionary<string, string>>();
			dataPaticka = new Dictionary<string, string>();

			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dtsiSoupis = null;

			try
			{
				if (Vydej.vydejInstance.globalObject.Davka.StartsWith("S"))
				{
					return new ReturnState(DialogResult.Abort, "Davka je sloucena!");
				}

				dtsiSoupis = Vydej.vydejInstance.globalObject.controller_vydej.GetData_SI_Tisk(int.Parse(Vydej.vydejInstance.globalObject.Davka)); // TODO : ale co kdyz je sloucena??? na urovni konzole?

				foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow drsi in dtsiSoupis)
				{

					Dictionary<string, string> dataRadek = new Dictionary<string, string>();

					Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SNDataTable drSN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataSERLTNUM_SI_SN(drsi.SOPNUMBE, drsi.ITEMNMBR, drsi.CountEntries, drsi.NMBRPAL, drsi.ORD);

					if ((drSN != null) && (drSN.Count > 0))
					{
						List<string> listSN = new List<string>();

						foreach (var item in drSN)
						{
							if (!string.IsNullOrEmpty(item.SERLTNUM))
								listSN.Add(item.SERLTNUM);
						}

						if (listSN.Count == 0)
							dataRadek.Add("SERLTNUMSeznam", "-");
						else
						{

							string result = String.Join(", ", listSN.ToArray());
							dataRadek.Add("SERLTNUMSeznam", result);
						}
					}
					else
					{
						dataRadek.Add("SERLTNUMSeznam", "-");
					}

					string nmbrpal = drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim();
					var row = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByNMBRPAL_CountEntries_SI_BV_Tisk(int.Parse(Vydej.vydejInstance.globalObject.Davka), nmbrpal);

					if (row != null)
					{
						dataRadek.Add("DIMENSIONWIDTH", row.Ispal_WNull() ? string.Empty : row.pal_W.ToString("0", System.Globalization.NumberFormatInfo.InvariantInfo));
						dataRadek.Add("DIMENSIONHEIGHT", row.Ispal_HNull() ? string.Empty : row.pal_H.ToString("0", System.Globalization.NumberFormatInfo.InvariantInfo));
						dataRadek.Add("DIMENSIONDEPTH", row.Ispal_DNull() ? string.Empty : row.pal_D.ToString("0", System.Globalization.NumberFormatInfo.InvariantInfo));
						dataRadek.Add("GROSSWEIGHT", row.Ispal_WEIGHTNull() ? string.Empty : row.pal_WEIGHT.ToString("0.000", System.Globalization.NumberFormatInfo.InvariantInfo));
					}

					string itemdesc = Vydej.vydejInstance.globalObject.controller_vydej.Get_ITEMDESC_SE(drsi.SOPNUMBE.Trim(), drsi.ITEMNMBR.Trim());

					dataRadek.Add("ITEMDESC", itemdesc);
					dataRadek.Add("CountEntries", drsi.CountEntries.ToString());
					dataRadek.Add("SOPNUMBE", drsi.SOPNUMBE.Trim());
					dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
					dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
					dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
					dataRadek.Add("QTYSHPPD", drsi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					dataRadek.Add("USER_ID", drsi.USER_ID.ToString());
					dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
					dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
					dataRadek.Add("ID_TERMINAL", drsi.ID_TERMINAL.ToString());
					dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
					dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());
					dataRadky.Add(dataRadek);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.WriteTable(dtsiSoupis);
				Logging.Log.Write(ex);
				throw ex;
			}

			return new ReturnState();
		}

		/// <summary>
		/// Metoda pro zakaznika CARP s upravou tisku
		/// </summary>
		/// <param name="dataHlavicka"></param>
		/// <param name="dataRadky"></param>
		/// <param name="dataPaticka"></param>
		/// <returns></returns>
		private ReturnState TiskSoupiska_CARP(out Dictionary<string, string> dataHlavicka, out List<Dictionary<string, string>> dataRadky, out Dictionary<string, string> dataPaticka)
		{
			dataHlavicka = new Dictionary<string, string>();
			dataRadky = new List<Dictionary<string, string>>();
			dataPaticka = new Dictionary<string, string>();

			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisDataTable dtsiSoupis = null;

			try
			{
				if (Vydej.vydejInstance.globalObject.Davka.StartsWith("S"))
				{
					return new ReturnState(DialogResult.Abort, "Davka je sloucena!");
				}

				dtsiSoupis = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByCountEntries_SISE_Soupis(int.Parse(Vydej.vydejInstance.globalObject.Davka));

				#region Data do Hlavičky

				var GrupSopnumbe = dtsiSoupis.GroupBy(x => x.SOPNUMBE);

				dataHlavicka.Add("SOPNUMBE", GrupSopnumbe.First().Key);
				dataHlavicka.Add("CountEntries", GrupSopnumbe.First().First().CountEntries.ToString());

				#endregion

				foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisRow drsi in dtsiSoupis)
				{


					Dictionary<string, string> dataRadek = new Dictionary<string, string>();

					decimal QTYSHPPD_DIF = 0;

					if (!drsi.IsQTYSHPPD_SINull())
					{
						QTYSHPPD_DIF = drsi.QTYSHPPD - drsi.QTYSHPPD_SI;

						if (QTYSHPPD_DIF == 0)
							continue;
					}
					else
					{
						QTYSHPPD_DIF = drsi.QTYSHPPD;
					}

					dataRadek.Add("QTYSHPPD_DIF", QTYSHPPD_DIF.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));

					dataRadek.Add("ITEMCODE", drsi.IsITEMCODENull() ? string.Empty : drsi.ITEMCODE);
					dataRadek.Add("ITEMDESC", drsi.IsITEMDESCNull() ? string.Empty : drsi.ITEMDESC);
					dataRadek.Add("CountEntries", drsi.IsCountEntriesNull() ? string.Empty : drsi.CountEntries.ToString());
					dataRadek.Add("SOPNUMBE", drsi.IsSOPNUMBENull() ? string.Empty : drsi.SOPNUMBE.Trim());
					dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
					dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
					dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
					dataRadek.Add("USER_ID", drsi.IsUSER_IDNull() ? string.Empty : drsi.USER_ID.ToString());
					dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
					dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
					dataRadek.Add("ID_TERMINAL", drsi.IsID_TERMINALNull() ? string.Empty : drsi.ID_TERMINAL.ToString());
					dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
					dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());

					dataRadky.Add(dataRadek);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.WriteTable(dtsiSoupis);
				Logging.Log.Write(ex);
				throw ex;
			}

			return new ReturnState();
		}

		/// <summary>
		/// Metoda pro zakaznika TierraVerde s upravou tisku
		/// </summary>
		/// <param name="dataHlavicka"></param>
		/// <param name="dataRadky"></param>
		/// <param name="dataPaticka"></param>
		/// <returns></returns>
		private ReturnState TiskSoupiska_TierraVerde(out Dictionary<string, string> dataHlavicka, out List<Dictionary<string, string>> dataRadky, out Dictionary<string, string> dataPaticka)
		{
			dataHlavicka = new Dictionary<string, string>();
			dataRadky = new List<Dictionary<string, string>>();
			dataPaticka = new Dictionary<string, string>();

			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisDataTable dtsiSoupis = null;

			try
			{
				if (Vydej.vydejInstance.globalObject.Davka.StartsWith("S"))
				{
					return new ReturnState(DialogResult.Abort, "Davka je sloucena!");
				}

				dtsiSoupis = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByCountEntries_SISE_Soupis(int.Parse(Vydej.vydejInstance.globalObject.Davka));

				#region Data do Hlavičky

				var GrupSopnumbe = dtsiSoupis.GroupBy(x => x.SOPNUMBE);

				dataHlavicka.Add("SOPNUMBE", GrupSopnumbe.First().Key);
				dataHlavicka.Add("CountEntries", GrupSopnumbe.First().First().CountEntries.ToString());

				#endregion

				foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisRow drsi in dtsiSoupis)
				{


					Dictionary<string, string> dataRadek = new Dictionary<string, string>();

					decimal QTYSHPPD_DIF = 0;

					if (!drsi.IsQTYSHPPD_SINull())
					{
						QTYSHPPD_DIF = drsi.QTYSHPPD - drsi.QTYSHPPD_SI;
					}
					else
					{
						QTYSHPPD_DIF = drsi.QTYSHPPD;
					}

					dataRadek.Add("QTYSHPPD_DIF", QTYSHPPD_DIF.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
					dataRadek.Add("QTYSHPPD", drsi.QTYSHPPD.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));

					dataRadek.Add("ITEMCODE", drsi.IsITEMCODENull() ? string.Empty : drsi.ITEMCODE);
					dataRadek.Add("ITEMDESC", drsi.IsITEMDESCNull() ? string.Empty : drsi.ITEMDESC);
					dataRadek.Add("CountEntries", drsi.IsCountEntriesNull() ? string.Empty : drsi.CountEntries.ToString());
					dataRadek.Add("SOPNUMBE", drsi.IsSOPNUMBENull() ? string.Empty : drsi.SOPNUMBE.Trim());
					dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
					dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
					dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
					dataRadek.Add("USER_ID", drsi.IsUSER_IDNull() ? string.Empty : drsi.USER_ID.ToString());
					dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
					dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
					dataRadek.Add("ID_TERMINAL", drsi.IsID_TERMINALNull() ? string.Empty : drsi.ID_TERMINAL.ToString());
					dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
					dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());

					dataRadky.Add(dataRadek);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.WriteTable(dtsiSoupis);
				Logging.Log.Write(ex);
				throw ex;
			}

			return new ReturnState();
		}
		
		
		#endregion

		#region VydejLogika

		private bool vkladaniSN(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow, string idOdberatele, BaseCode code, VydejService.Vydej_Items_Online.ItemsRow online_item)
		{
			return vkladaniSN(VERow, idOdberatele, code, online_item, null);
		}

		/// <summary>
		/// vkladani - silena logika - rozdeleni pro Dobre Podlahy
		/// </summary>
		/// <param name="VERow"></param>
		/// <param name="idOdberatele"></param>
		/// <param name="code"></param>
		/// <param name="online_item"></param>
        /// <param name="lokaceItem">informace o polozce z online lokacniho mechanismu - pro Dobre Podlahy</param>
		/// <returns></returns>
        ///// <param name="SELTNUM"></param>
        ///// <param name="LOCNCODE"></param>
        private bool vkladaniSN(
							Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
							string idOdberatele,
							BaseCode code,
							VydejService.Vydej_Items_Online.ItemsRow online_item,
                            Classes.LokaceItem lokaceItem
                            //string SELTNUM,
                            //string LOCNCODE
							)
		{

			if (MST_Global.Vydej_TypSPrelokovanim && 
                !vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && 
                vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
			{
				return vkladaniSN_Prelokovanim(VERow, idOdberatele, code, online_item, lokaceItem);
			}
			else
			{
				return vkladaniSN_Default(VERow, idOdberatele, code, online_item, lokaceItem);
			}
		}


		/// <summary>
		/// Zpracuje polozku zvolenou z predlohu (snimaniSN atd.)
		/// </summary>
		/// <returns>Vraci, zda bylo vlozeno alespon jedno SN</returns>
		private bool vkladaniSN_Default(
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
			string idOdberatele,
			BaseCode code,
			VydejService.Vydej_Items_Online.ItemsRow online_item
            ,Classes.LokaceItem lokaceItem
            //string SELTNUM,
            //string LOCNCODE
			)
		{
			// TODO : osetrit praci s vydej_online_item ... 
			// - prevzeti informaci o expiraci, sarzi, polozce, atd ...
			// - kombinace s code x vydej_online_item ... 

			// TODO : FIFO/FEFO kontroly

			SejmiKodForm skf = new SejmiKodForm();

			//SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
			//SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter sesnta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
			//sita.Connection.ConnectionString = "Data source=" + filename;
			//sesnta.Connection = sita.Connection;

			try
			{
				//sita.Connection.Open(); //Otevre connection pro rychlejsi pristup a zapisy do db ...
				Vydej.vydejInstance.globalObject.controller_vydej.Connection_Open(); // <- asi nechci mit porad otevrene spojeni?

				//počet položek už načtenych
				//decimal Quantity = Nacteno(filename, VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD, true);
				decimal Quantity = Vydej.vydejInstance.globalObject.controller_vydej.Nacteno(VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD, true);

				bool vlozenoSN = false;

				#region Preplneni polozky
				if (!MST_Global.vydejPovolitPreplneniPolozky)
				{
					if (Quantity >= VERow.QTYSHPPD)
					{
						MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyNazevZakazano, VERow.ITEMDESC.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						return vlozenoSN;
					}
				}
				#endregion

				#region Kontrola naplneni polozky
				if (vydejDataParametry.Parametry[0].CONFIG_POKRDOHLED)
				{
					//TaD Vracena podminka, duvod aby to fungovalo jen v připade že množstvi je požadovano
					if ((decimal)Quantity >= VERow.QTYSHPPD)
					{
						// podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
						if ((decimal)Quantity > VERow.QTYSHPPD)
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaPreplneniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
								== DialogResult.Yes)
								return vlozenoSN;
						}
						else
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaVyplnenoMnozstviSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
								== DialogResult.Yes)
								return vlozenoSN;
						}
					}
				}
				#endregion

				string baliciList = "";
				bool baliciListGenerovan = false;
				bool dalsiSN = true;
				string skl_id = string.Empty;
				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow = vydejDataParametry.CZMST_SI.NewCZMST_SIRow();
				VIRowREZ2_SetDefault(VIRow);

				int PoradiSN = 0;
				List<string> listSN = null;

				while (dalsiSN)
				{
					#region Vychozi hodnoty a verifikace

					VIRow.SERLTNUM = string.Empty;

                    if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.LOCNCODE))
                        VIRow.LOCNCODE = lokaceItem.LOCNCODE.Trim();
					else
						VIRow.SERLTNUM = string.Empty;

					VIRow.KOD_SW = string.Empty;
					VIRow.DAT_VYROBY = string.Empty;
					//VIRow.REZ_1 = MST_Global.VydejTypOznaceniPalety ? this.Paleta : string.Empty;
					VIRow.REZ_1 = string.Empty;
					string rez1 = string.Empty;
					string rez2 = string.Empty;

					#region ID Skladu
					// nacteni id skladu
					if (MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu)
						skl_id = _sklad != null ? _sklad.skl_id : string.Empty;
					else
						skl_id = VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID;
					VIRow.SKL_ID = skl_id;
					#endregion

					// vychozi hodnota pro mnozstvi
					decimal mnozstvi = 0;
					#region Verfikace mnozstvi => vychozi hodnota
					{
						if (code is Parsing.Codes.Interfaces.ICodeQuantity)
						{
							mnozstvi = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity ?? 0;
						}
						else if (online_item != null) // je-li polozka dotazena online, tak nastavim vychozi mnozstvi skladove => muze byt pozdeji zmeneno hodnotou z car.kodu polozky
						{
							mnozstvi = online_item.Qty;
						}
						else if (MST_Global.Vydej_MnozstviAutoJedna)
						{
							mnozstvi = 1;
						}
					}
					#endregion

					// vychozi hodnota pro Sarzi/SN
					string sn = string.Empty;
					#region Verfikace Sarze/SN online X scan => vychozi hodnota
					{
						string sn_online = string.Empty;
						string sn_code = string.Empty;
						sn_online = online_item != null ? online_item.Serltnum ?? string.Empty : string.Empty;

						if (VERow.CZ_SerNum_Track == 1)
						{
							if (code is Parsing.Codes.Interfaces.ICodeSerialNumber)
								sn_code = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN ?? string.Empty;
						}
						else if (VERow.CZ_SerNum_Track == 2)
						{
							if (code is Parsing.Codes.Interfaces.ICodeSarze)
								sn_code = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze ?? string.Empty;
						}
						else if (VERow.CZ_SerNum_Track == 11)
						{
							if (code is Parsing.Codes.Interfaces.ICodeSerialNumber)
								sn_code = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN ?? string.Empty;
						}

						
						if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR))
							sn_code = lokaceItem.SERLNMBR.Trim();

						if ((!string.IsNullOrEmpty(sn_online)) && (!string.IsNullOrEmpty(sn_code)))
						{
							if (sn_online != sn_code)
							{
								MessageBoxBig.Show("Šarže/SN online('" + sn_online + "') X scan('" + sn_code + "') se neshodují", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return false;
							}
							else
								sn = sn_online;
						}
						else if (!string.IsNullOrEmpty(sn_online))
						{
							sn = sn_online;
						}
						else if (!string.IsNullOrEmpty(sn_code))
						{
							sn = sn_code;
						}
					}
					#endregion

					// 26.2.2020 ColorProfi JiS
					// doplnen parametr PE.CZ_Expirace_Track a PI.Expirace
					// zadavani expirace, kde kdy ? 
					// jak s fenixem ? a funkncnosti expirace do nejake rezervy?
					DateTime? expirace = null;  // vychozi nastaveni expirace neni ...                    
					#region Verfikace expirace => vychozi hodnota
					{
						DateTime? expirace_online = null;
						if ((online_item != null) && (!online_item.IsExpirationNull()))
							expirace_online = online_item.Expiration;

						DateTime? expirace_code = null;
						if ((code is ICodeExpiration))
							expirace_code = ((ICodeExpiration)code).Expiration;

						if (expirace_online.HasValue && expirace_code.HasValue)
						{
							if (expirace_online.Value.Date != expirace_code.Value.Date)
							{
								MessageBoxBig.Show("Expirace online('" + expirace_online.Value.Date.ToString(Main.dateFormatRRMMDD) + "') X scan('" + expirace_code.Value.ToString(Main.dateFormatRRMMDD) + "') se neshodují", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return false;
							}
							else
								expirace = expirace_online;
						}
						else if (expirace_code.HasValue)
						{
							expirace = expirace_code;
						}
						else if (expirace_online.HasValue)
						{
							expirace = expirace_online;
						}
					}
					#endregion

					#endregion

					#region Lokace zadani
					if (MST_Global.vydejZadaniLocncodePredSN)
					{
						if (!ZadaniLocncode(VERow, VIRow))
							break;
					}
					#endregion

					// bude snimat SN (overi i snimani DV a SW)
					if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
					{
						#region Sarze/SN
						if (MST_Global.Vydej_HromadneSN)
						{
							#region Hromadne SN generovani
							if ((VERow.CZ_SerNum_Track == 1) && (listSN == null))
							{
								listSN = HromadneSN(VERow.ITEMDESC, Quantity, VERow.QTYSHPPD);

								if (listSN == null)
									break;
							}

							if (PoradiSN > (listSN.Count - 1))
							{
								dalsiSN = false;
								continue;
							}


							sn = listSN[PoradiSN++];
							#endregion
						}

						// nastaveni vychozi hodnoty sn po verifikaci
						VIRow.SERLTNUM = sn;
						// pokud se jedna o sarze, preskocit vsechny kontroly
						// TODO : byse melo kontrolovat, pokud je predloha sarzi, ze nasnimana sarze je v predpisu a pokud neni, tak upozornit a pripadne nechat zmenit
						if (
							(VERow.CZ_SerNum_Track == 2)
							&&
							( // sarze nactena car.kodem nebo zvolena/zadana online dotazem
								((code is Parsing.Codes.Interfaces.ICodeSarze) && !string.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze) )
								||
								(online_item != null)
							)
							)
						{
							// toto je jiz nastaveno kontrolou vyse => sn = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr ?? string.Empty;
							// preskoci se zadavani
						}
                        else if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR))
						{
							VIRow.SERLTNUM = sn;
							// preskoci se zadavani TaD 3.11.2020
						}
						else
						{
							//while (true)
							//{
							//Kontrola ze jde o cislo sarze a chceme overovat vuci zbozi
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//...musi byt nastaven sloupec k navraceni
								string returnValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU.Trim();
								if (returnValueColumnName == string.Empty)
								{
									MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
									return vlozenoSN;
								}

								//Vse je v poradku, zviditelnim btn pro zobrazeni alternativ k sarzim
								sejmiFormSN.btnZobrazitAlternativyVisible = true;
								sejmiFormSN.returnValueColumnName = returnValueColumnName;
								sejmiFormSN.vybiratZboziJenScannerem = MST_Global.VydejZboziVyberJenScannerem;
								sejmiFormSN.vyhledavatZboziDleSloupce = MST_Global.VydejZboziVyhledaniDleSloupce;
							}

							sejmiFormSN.SetDefaultValues();
							sejmiFormSN.Popis = VERow.CZ_SerNum_Track == 2 ? Fask.Localization.Localization.Vydej3ListPolozek3SejmiSarze : (string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SNName));// "Sejmi " + (VERow.CZ_SerNum_Track == 2 ? "Šarže" : MST_Global.SNName);
							sejmiFormSN.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
							sejmiFormSN.Len = 0;
							sejmiFormSN.CheckLen = false;
							sejmiFormSN.AllowEmpty = false;
							sejmiFormSN.veRow = VERow;
							sejmiFormSN.viRow = VIRow;
							sejmiFormSN.Kod = sn;
							sejmiFormSN.vynulujVybraneZbozi();
							//SejmiKodInfoForm3 sejmiSNForm = new SejmiKodInfoForm3(
							//    "Sejmi " + MST_Global.SNName,
							//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
							//    VERow);

							//sejmiFormSN.SESN = sesnta.GetDataByCountEntriesITEMNMBR(VERow.CountEntries, VERow.ITEMNMBR.Trim());

							//sejmiFormSN.SESN = sesnta.GetDataByKeyNacteno(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);
							sejmiFormSN.SESN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByKeyNacteno_SE_SN(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);


							//Jen kvuli finally bloku
							try
							{
								if (!MST_Global.Vydej_HromadneSN)
								{
									if (sejmiFormSN.ShowDialog() == DialogResult.Cancel)
									{// chce prerusit snimani
										if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{// test, zda je nasnimane pozadovane mnozstvi
											if ((decimal)Quantity < VERow.QTYSHPPD)
											{
												if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
													== DialogResult.Yes)
													return vlozenoSN;
												else
													continue;
												//break;
											}
											//else
											//    break;
											return vlozenoSN;
										}
										//else
										//    break;
										return vlozenoSN;
									}
								}
							}
							catch { }
							finally
							{
								//A schovam tlacitko
								sejmiFormSN.btnZobrazitAlternativyVisible = false;
							}

							// kontrola shody s Carovym kodem (MN)
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_SN_CARKOD)
							{
								if (VERow.CZ_CarKod.Trim() == sejmiFormSN.Kod || (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()) == sejmiFormSN.Kod)
								{
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmutyKodJeStejnyJako, MST_Global.MNName), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}
							}

							//kontrola predlohy SN
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_PREDLOHA_SN)
							{
								Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow sesnrow = sejmiFormSN.SESNSelected;
								if (sesnrow != null) //polozka nenalezena => neni v predloze => dotaz
								{
									if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoVPredlozePokracovatDotaz, MST_Global.SNName, sejmiForm.Kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
										continue;
								}
								//else //if (sesndt.Count > 0) //polozka je v predloze, tak pokracuji
								//{
								//}
							}

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola duplicity SN
							#region kontrola duplicity sn
							if (vydejDataParametry.Parametry[0].CONFIG_DUPLIC_SN)
							{
								int sernumCount = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Get_Pocet_Duplicit_SN_Count(VERow.ITEMNMBR.Trim(), VERow.SOPNUMBE.Trim(), VERow.ORD, sejmiFormSN.Kod);
								//if (VIRows.Length > 0)
								if (sernumCount > 0)
								{
									// Pouze uporozni a umozni pokracovat ...
									if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SNJizNasnimanoUlozitDotaz, MST_Global.SNName), Fask.Localization.Localization.Vydej3ListPolozek3Upozorneni, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning))
										continue;
								}
							}
							#endregion

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola delky (nedela se v SejmiKod formulari, protoze se ma provest az po ostatnich kontrolach
							#region kontrola delky
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA)
							{
								if (VERow.CZ_SerNum_Delka > 0)
								{
									if (sejmiFormSN.Kod.Length > VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeDelsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											== DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
									else if (sejmiFormSN.Kod.Length < VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeKratsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											 == DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
								}
							}
							#endregion

							//Pokud jedna se o cislo sarze a je v parametrech davky, ze se ma opirat o ciselnik, ...
							#region Opirani o ciselnik zbozi ...
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//Pokud uz radek nebyl nacten pomoci tlacitka zobrazit alternativy na sejmiKodForm3
								if (sejmiFormSN.VybraneZbozi == null)
								{
									//Musi byt navracen sloupec pro kontrolu
									string confirmValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU.Trim();
									if (confirmValueColumnName == string.Empty)
									{
										MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										return false;
									}

									try
									{
										//Kurzor
										Cursor.Current = Cursors.WaitCursor;

										Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = Vydej.vydejInstance.globalObject.controller_zbozi.GetTableByConfirmValue(VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), confirmValueColumnName, sejmiFormSN.Kod);

										//Kurzor zpet
										Cursor.Current = Cursors.Default;

										//Rozhodovani dle cetnosti zaznamu - jeden, ok
										if (table.Rows.Count == 1)
										{
											//Beru nastaveny sloupec
											sejmiFormSN.Kod = table.Rows[0][sejmiFormSN.returnValueColumnName].ToString().Trim();
										}
										//Zobrazim vyber
										else if (table.Rows.Count > 1)
										//else //if (table.Rows.Count > 1)
										{
											//cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod)" +
											//        " UNION " +
											//        " SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum)";
											//cmd.CommandText = cmdText;
											//reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
											//table.Clear();
											//table.Load(reader);

											using (ListZbozi lz = new ListZbozi(MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
											{
												//Byl vybran nejaky radek
												lz.showTable(table);
												if (lz.ShowDialog() == DialogResult.OK)
												{
													sejmiFormSN.Kod = lz.vybraneZbozi[sejmiFormSN.returnValueColumnName].ToString().Trim();
													sejmiFormSN.InputMode = lz.input_mode;
												}
												else
												{
													//Zobrazeni
													if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NevybranaMoznaVariantaOpakovatDotaz, Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
													{
														//Cyklim znovu
														continue;
													}
													else
													{
														return false;
													}
												}
											}
										}
										else /* table.Rows.Count < 1, tzn. 0 = zadny zaznam */
										{
											//Zobrazeni
											if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoZboziKCarKodOpakovatDotaz, VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), sejmiFormSN.Kod, confirmValueColumnName), Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
											{
												//Cyklim znovu
												continue;
											}
											else
											{
												return false;
											}
										}
									}
									catch (Exception ex)
									{
										//Kurzor zpet - v pripade chyby
										Cursor.Current = Cursors.Default;
										//Zobrazeni chyby a zalogovani
										MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										Logging.Log.Write(ex);
										return false;
									}
									finally
									{
									}
								}
							}
							#endregion

							VIRow.SERLTNUM = sejmiFormSN.Kod;
						}

						// TODO : sem umistit kontroly zadane Sarze/SN (predelat z vetve zadani sn
						// - kontrola duplicity
						// - kontrola delky

						//Vezme kod z formu a metodu ziskani - ta prebije dosavadni metodu
						_input_mode = sejmiFormSN.InputMode;

						if (VERow.CZ_SerNum_Track == 2)
						{// sejme mnozstvi k SN
							string rez_2 = VIRow.REZ_2;
							if (sejmiMnozstvi(ref mnozstvi, Quantity, VERow, VIRow, sejmiFormSN.SESNSelected, ref rez_2, code) != 0)
								break;
							VIRow.REZ_2 = rez_2;

							if (!MST_Global.vydejPovolitPreplneniPolozky)
							{
								if (mnozstvi + Quantity > VERow.QTYSHPPD)
								{
									MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
									continue;
								}
							}

							//VIRow.QTYPACK = (decimal)mnozstvi;

							// pokud je skryto pozadovane mnozstvi a je disproporce, zazada o potvrzeni
							if (vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI)
							{
								if (((decimal)(Quantity + mnozstvi)) != VERow.QTYSHPPD)
								{
									using (Fask.MST_W.Vydej_3.PotvrditMnozstviForm potrvditMnozstviForm =
										new Fask.MST_W.Vydej_3.PotvrditMnozstviForm(VERow.QTYSHPPD, Quantity + mnozstvi))
									{
										if (potrvditMnozstviForm.ShowDialog() == DialogResult.Cancel)
											continue; // bude opakovat zadavani
									}
								}
							}
						}
						else
							mnozstvi = (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1);

						Quantity += mnozstvi;

						//    //Pokud sem kod dojde, uz se  nebude cyklit
						//    break;
						//}
						#endregion
					} // end if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
					// nebude snimat SN, jen zada pocet (DV SW a REZ_1 jsou "")
					else if (VERow.CZ_SerNum_Track == 0)
					{
						#region Mnozstvi
						//START - NEW
						if (vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
						{
							////...musi byt nastaven sloupec k navraceni
							//string returnValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU.Trim();
							//if (returnValueColumnName == string.Empty)
							//{
							//    MessageBox.Show("V parametrech dávky je nutné vyplnit název sloupce jehož hodnota se má zaznamenat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
							//    return false;
							//}

							try
							{
								//Kurzor
								Cursor.Current = Cursors.WaitCursor;

								Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = Vydej.vydejInstance.globalObject.controller_zbozi.GetTableByBoth(VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()));

								//Kurzor zpet
								Cursor.Current = Cursors.Default;

								//Rozhodovani dle cetnosti zaznamu - jeden, ok
								//if (table.Rows.Count == 1)
								if (table.Rows.Count >= 1)
								{
									//Beru hodnotu z nastaveneho sloupce
									// JiS : zadne serltnum, neni zamena ..., existuje, tak dal...
									//VIRow.SERLTNUM = table.Rows[0][returnValueColumnName].ToString().Trim();
								}
								//Zobrazim vyber
								//else if (table.Rows.Count > 1)
								//else //if (table.Rows.Count > 1)
								//{
								//    cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod)"+ 
								//        " UNION " +
								//        " SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum)";
								//    cmd.CommandText = cmdText;
								//    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
								//    table.Clear();
								//    table.Load(reader);

								//    //using (ListZbozi lz = new ListZbozi(MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
								//    using (ListZbozi lz = new ListZbozi(VERow.CZ_CarKod.Trim(), VERow.VNDITNUM.Trim(), MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
								//    {
								//        while (true)
								//        {
								//            //Byl vybran nejaky radek
								//            lz.showTable(table);
								//            if (lz.ShowDialog() == DialogResult.OK)
								//            {
								//                VIRow.SERLTNUM = lz.vybraneZbozi[returnValueColumnName].ToString().Trim();
								//                _input_mode = lz.input_mode;
								//                break;
								//            }
								//            else
								//            {
								//                //Zobrazeni
								//                if (MessageBoxBig.Show("Nebyla vybrána žádná z možných variant zboží.\nPřejete si volbu opakovat?", Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
								//                {
								//                    //Cyklim znovu
								//                    continue;
								//                }
								//                else
								//                {
								//                    return false;
								//                }
								//            }
								//        }
								//    }
								//}
								else /* table.Rows.Count < 1, tzn. 0 = zadny zaznam */
								{
									//Zobrazeni
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoZboziKCarKod, VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim())), Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
									//konec
									return false;
								}
							}
							catch (Exception ex)
							{
								//Kurzor zpet - v pripade chyby
								Cursor.Current = Cursors.Default;
								//Zobrazeni chyby a zalogovani
								MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
								Logging.Log.Write(ex);
								return false;
							}
							finally
							{
							}
						}
						//END - NEW

						string rez_2 = VIRow.REZ_2;

						if (MST_Global.Vydej_MnozstviAutoJedna)
						{
							mnozstvi = 1;
						}
						else
						{
							if (sejmiMnozstvi(ref mnozstvi, Quantity, VERow, VIRow, null, ref rez_2, code) != 0)
								break;
						}

						VIRow.REZ_2 = rez_2;

						if (!MST_Global.vydejPovolitPreplneniPolozky)
						{
							if (mnozstvi + Quantity > VERow.QTYSHPPD)
							{
								MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								continue;
							}
						}

						//VIRow.QTYPACK = (decimal)mnozstvi;

						// pokud je skryto pozadovane mnozstvi a je disproporce, zazada o potvrzeni
						if (vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI)
						{
							if (((decimal)(Quantity + mnozstvi)) != VERow.QTYSHPPD)
							{
								using (Fask.MST_W.Vydej_3.PotvrditMnozstviForm potrvditMnozstviForm =
									new Fask.MST_W.Vydej_3.PotvrditMnozstviForm(VERow.QTYSHPPD, Quantity + mnozstvi))
								{
									if (potrvditMnozstviForm.ShowDialog() == DialogResult.Cancel)
										continue; // bude opakovat zadavani
								}
							}
						}

						Quantity += mnozstvi;
						#endregion
					}
					if ((VERow.CZ_SerNum_Track == 11))
					{
						#region Sarze/SN
						if (MST_Global.Vydej_HromadneSN)
						{
							#region Hromadne SN generovani
							if ((VERow.CZ_SerNum_Track == 1) && (listSN == null))
							{
								listSN = HromadneSN(VERow.ITEMDESC, Quantity, VERow.QTYSHPPD);

								if (listSN == null)
									break;
							}

							if (PoradiSN > (listSN.Count - 1))
							{
								dalsiSN = false;
								continue;
							}


							sn = listSN[PoradiSN++];
							#endregion
						}

						// nastaveni vychozi hodnoty sn po verifikaci
						VIRow.SERLTNUM = sn;
						// pokud se jedna o sarze, preskocit vsechny kontroly
						// TODO : byse melo kontrolovat, pokud je predloha sarzi, ze nasnimana sarze je v predpisu a pokud neni, tak upozornit a pripadne nechat zmenit
						if (
							(VERow.CZ_SerNum_Track == 2)
							&&
							( // sarze nactena car.kodem nebo zvolena/zadana online dotazem
								((code is Parsing.Codes.Interfaces.ICodeSarze) && !string.IsNullOrEmpty(((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
								||
								(online_item != null)
							)
							)
						{
							// toto je jiz nastaveno kontrolou vyse => sn = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr ?? string.Empty;
							// preskoci se zadavani
						}
						else if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR))
						{
							VIRow.SERLTNUM = sn;
							// preskoci se zadavani TaD 3.11.2020
						}
						else
						{
							//while (true)
							//{
							//Kontrola ze jde o cislo sarze a chceme overovat vuci zbozi
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//...musi byt nastaven sloupec k navraceni
								string returnValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU.Trim();
								if (returnValueColumnName == string.Empty)
								{
									MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
									return vlozenoSN;
								}

								//Vse je v poradku, zviditelnim btn pro zobrazeni alternativ k sarzim
								sejmiFormSN.btnZobrazitAlternativyVisible = true;
								sejmiFormSN.returnValueColumnName = returnValueColumnName;
								sejmiFormSN.vybiratZboziJenScannerem = MST_Global.VydejZboziVyberJenScannerem;
								sejmiFormSN.vyhledavatZboziDleSloupce = MST_Global.VydejZboziVyhledaniDleSloupce;
							}

							sejmiFormSN.SetDefaultValues();
							sejmiFormSN.Popis = VERow.CZ_SerNum_Track == 2 ? Fask.Localization.Localization.Vydej3ListPolozek3SejmiSarze : (string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SNName));// "Sejmi " + (VERow.CZ_SerNum_Track == 2 ? "Šarže" : MST_Global.SNName);
							sejmiFormSN.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
							sejmiFormSN.Len = 0;
							sejmiFormSN.CheckLen = false;
							sejmiFormSN.AllowEmpty = true;
							sejmiFormSN.veRow = VERow;
							sejmiFormSN.viRow = VIRow;
							sejmiFormSN.Kod = sn;
							sejmiFormSN.vynulujVybraneZbozi();
							//SejmiKodInfoForm3 sejmiSNForm = new SejmiKodInfoForm3(
							//    "Sejmi " + MST_Global.SNName,
							//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
							//    VERow);

							//sejmiFormSN.SESN = sesnta.GetDataByCountEntriesITEMNMBR(VERow.CountEntries, VERow.ITEMNMBR.Trim());

							//sejmiFormSN.SESN = sesnta.GetDataByKeyNacteno(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);
							sejmiFormSN.SESN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByKeyNacteno_SE_SN(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);


							//Jen kvuli finally bloku
							try
							{
								if (!MST_Global.Vydej_HromadneSN)
								{
									if (sejmiFormSN.ShowDialog() == DialogResult.Cancel)
									{// chce prerusit snimani
										if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{// test, zda je nasnimane pozadovane mnozstvi
											if ((decimal)Quantity < VERow.QTYSHPPD)
											{
												if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
													== DialogResult.Yes)
													return vlozenoSN;
												else
													continue;
												//break;
											}
											//else
											//    break;
											return vlozenoSN;
										}
										//else
										//    break;
										return vlozenoSN;
									}
								}
							}
							catch { }
							finally
							{
								//A schovam tlacitko
								sejmiFormSN.btnZobrazitAlternativyVisible = false;
							}

							// kontrola shody s Carovym kodem (MN)
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_SN_CARKOD)
							{
								if (VERow.CZ_CarKod.Trim() == sejmiFormSN.Kod || (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()) == sejmiFormSN.Kod)
								{
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmutyKodJeStejnyJako, MST_Global.MNName), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}
							}

							//kontrola predlohy SN
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_PREDLOHA_SN)
							{
								Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow sesnrow = sejmiFormSN.SESNSelected;
								if (sesnrow != null) //polozka nenalezena => neni v predloze => dotaz
								{
									if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoVPredlozePokracovatDotaz, MST_Global.SNName, sejmiForm.Kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
										continue;
								}
								//else //if (sesndt.Count > 0) //polozka je v predloze, tak pokracuji
								//{
								//}
							}

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola duplicity SN
							#region kontrola duplicity sn
							if (!string.IsNullOrEmpty(sejmiFormSN.Kod))
							{
								if (vydejDataParametry.Parametry[0].CONFIG_DUPLIC_SN)
								{
									int sernumCount = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Get_Pocet_Duplicit_SN_Count(VERow.ITEMNMBR.Trim(), VERow.SOPNUMBE.Trim(), VERow.ORD, sejmiFormSN.Kod);
									//if (VIRows.Length > 0)
									if (sernumCount > 0)
									{
										// Pouze uporozni a umozni pokracovat ...
										if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SNJizNasnimanoUlozitDotaz, MST_Global.SNName), Fask.Localization.Localization.Vydej3ListPolozek3Upozorneni, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning))
											continue;
									}
								} 
							}
							#endregion

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola delky (nedela se v SejmiKod formulari, protoze se ma provest az po ostatnich kontrolach
							#region kontrola delky
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA)
							{
								if (VERow.CZ_SerNum_Delka > 0)
								{
									if (sejmiFormSN.Kod.Length > VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeDelsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											== DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
									else if (sejmiFormSN.Kod.Length < VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeKratsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											 == DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
								}
							}
							#endregion

							//Pokud jedna se o cislo sarze a je v parametrech davky, ze se ma opirat o ciselnik, ...
							#region Opirani o ciselnik zbozi ...
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//Pokud uz radek nebyl nacten pomoci tlacitka zobrazit alternativy na sejmiKodForm3
								if (sejmiFormSN.VybraneZbozi == null)
								{
									//Musi byt navracen sloupec pro kontrolu
									string confirmValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU.Trim();
									if (confirmValueColumnName == string.Empty)
									{
										MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										return false;
									}

									try
									{
										//Kurzor
										Cursor.Current = Cursors.WaitCursor;

										Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = Vydej.vydejInstance.globalObject.controller_zbozi.GetTableByConfirmValue(VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), confirmValueColumnName, sejmiFormSN.Kod);

										//Kurzor zpet
										Cursor.Current = Cursors.Default;

										//Rozhodovani dle cetnosti zaznamu - jeden, ok
										if (table.Rows.Count == 1)
										{
											//Beru nastaveny sloupec
											sejmiFormSN.Kod = table.Rows[0][sejmiFormSN.returnValueColumnName].ToString().Trim();
										}
										//Zobrazim vyber
										else if (table.Rows.Count > 1)
										//else //if (table.Rows.Count > 1)
										{
											//cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod)" +
											//        " UNION " +
											//        " SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum)";
											//cmd.CommandText = cmdText;
											//reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
											//table.Clear();
											//table.Load(reader);

											using (ListZbozi lz = new ListZbozi(MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
											{
												//Byl vybran nejaky radek
												lz.showTable(table);
												if (lz.ShowDialog() == DialogResult.OK)
												{
													sejmiFormSN.Kod = lz.vybraneZbozi[sejmiFormSN.returnValueColumnName].ToString().Trim();
													sejmiFormSN.InputMode = lz.input_mode;
												}
												else
												{
													//Zobrazeni
													if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NevybranaMoznaVariantaOpakovatDotaz, Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
													{
														//Cyklim znovu
														continue;
													}
													else
													{
														return false;
													}
												}
											}
										}
										else /* table.Rows.Count < 1, tzn. 0 = zadny zaznam */
										{
											//Zobrazeni
											if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoZboziKCarKodOpakovatDotaz, VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), sejmiFormSN.Kod, confirmValueColumnName), Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
											{
												//Cyklim znovu
												continue;
											}
											else
											{
												return false;
											}
										}
									}
									catch (Exception ex)
									{
										//Kurzor zpet - v pripade chyby
										Cursor.Current = Cursors.Default;
										//Zobrazeni chyby a zalogovani
										MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										Logging.Log.Write(ex);
										return false;
									}
									finally
									{
									}
								}
							}
							#endregion

							VIRow.SERLTNUM = sejmiFormSN.Kod;
						}

						// TODO : sem umistit kontroly zadane Sarze/SN (predelat z vetve zadani sn
						// - kontrola duplicity
						// - kontrola delky

						//Vezme kod z formu a metodu ziskani - ta prebije dosavadni metodu
						_input_mode = sejmiFormSN.InputMode;

						if (VERow.CZ_SerNum_Track == 2)
						{// sejme mnozstvi k SN
							string rez_2 = VIRow.REZ_2;
							if (sejmiMnozstvi(ref mnozstvi, Quantity, VERow, VIRow, sejmiFormSN.SESNSelected, ref rez_2, code) != 0)
								break;
							VIRow.REZ_2 = rez_2;

							if (!MST_Global.vydejPovolitPreplneniPolozky)
							{
								if (mnozstvi + Quantity > VERow.QTYSHPPD)
								{
									MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
									continue;
								}
							}

							//VIRow.QTYPACK = (decimal)mnozstvi;

							// pokud je skryto pozadovane mnozstvi a je disproporce, zazada o potvrzeni
							if (vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI)
							{
								if (((decimal)(Quantity + mnozstvi)) != VERow.QTYSHPPD)
								{
									using (Fask.MST_W.Vydej_3.PotvrditMnozstviForm potrvditMnozstviForm =
										new Fask.MST_W.Vydej_3.PotvrditMnozstviForm(VERow.QTYSHPPD, Quantity + mnozstvi))
									{
										if (potrvditMnozstviForm.ShowDialog() == DialogResult.Cancel)
											continue; // bude opakovat zadavani
									}
								}
							}
						}
						else
							mnozstvi = (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1);

						Quantity += mnozstvi;

						//    //Pokud sem kod dojde, uz se  nebude cyklit
						//    break;
						//}
						#endregion
					}


					// generovani baliciho kodu
					// TODO : Co ma spravne delat generovani baliciho listu ???
					else // if (VERow.CZ_SerNum_Track == 9)
					{
						MessageBoxBig.Show("Dosud nepřevedeno do sql varianty ...");
						//    baliciList = VERow.CZ_CarKod; // uschova si kod baliciho listu

						//    // prepise VERow skutecnou predlohou
						//    if (generBalList(out VERow) != 0)
						//        break; // prerusil generovani

						//    baliciListGenerovan = true;
						//    continue; // zopakuje uz pro zmeneny sinstruct
					}

					#region Lokace zadani
					if (!MST_Global.vydejZadaniLocncodePredSN)
					{
						if (!ZadaniLocncode(VERow, VIRow))
							break;
					}
					#endregion

					#region Vkladani doplnujicich informaci
					if (VERow.CZ_SW_Track == 1)
					{
						#region Softwarova verze
						sejmiForm.SetDefaultValues();
						sejmiForm.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SWName);   // "Sejmi " + MST_Global.SWName;
						sejmiForm.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						sejmiForm.Len = VERow.CZ_SW_Delka;
						sejmiForm.CheckLen = vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA;
						sejmiForm.AllowEmpty = false;
						sejmiForm.veRow = VERow;
						sejmiForm.viRow = VIRow;
						sejmiForm.Kod = string.Empty;

						//SejmiKodInfoForm3 sejmiSWForm = new SejmiKodInfoForm3(
						//    "Sejmi " + MST_Global.SWName,
						//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
						//    VERow.CZ_SW_Delka,
						//    vydejData.Parametry[0].CONFIG_KONT_DELKA,
						//    false,
						//    VERow
						//    );
						bool opakuj;
						DialogResult res = DialogResult.No;
						do
						{
							opakuj = false;
							if (sejmiForm.ShowDialog() == DialogResult.Cancel)
							{
								if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
								{// test, zda je nasnimane pozadovane mnozstvi
									if ((decimal)Quantity < VERow.QTYSHPPD)
									{
										res = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
										if (res == DialogResult.No)
											opakuj = true;
									}
								}
								else
									break;
							}
						} while (opakuj);
						if (res == DialogResult.Yes)
							break;
						VIRow.KOD_SW = sejmiForm.Kod;
						#endregion
					}

					if (VERow.CZ_DatVyr_Track == 1)
					{
						#region Datum vyroby
						sejmiForm.SetDefaultValues();
						sejmiForm.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.DVName);       // "Sejmi " + MST_Global.DVName;
						sejmiForm.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						sejmiForm.Len = VERow.CZ_DatVyr_Delka;
						sejmiForm.CheckLen = vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA;
						sejmiForm.AllowEmpty = false;
						sejmiForm.veRow = VERow;
						sejmiForm.viRow = VIRow;
						sejmiForm.Kod = string.Empty;
						//SejmiKodInfoForm3 sejmiDVForm = new SejmiKodInfoForm3(
						//    "Sejmi " + MST_Global.DVName,
						//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
						//    VERow.CZ_DatVyr_Delka,
						//    vydejData.Parametry[0].CONFIG_KONT_DELKA,
						//    false, 
						//    VERow
						//    );
						bool opakuj;
						DialogResult res = DialogResult.No;
						do
						{
							opakuj = false;
							if (sejmiForm.ShowDialog() == DialogResult.Cancel)
							{
								if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
								{// test, zda je nasnimane pozadovane mnozstvi
									if ((decimal)Quantity < VERow.QTYSHPPD)
									{
										res = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
										if (res == DialogResult.No)
											opakuj = true;
									}
								}
								else
									break;
							}
						} while (opakuj);
						if (res == DialogResult.Yes)
							break;
						VIRow.DAT_VYROBY = sejmiForm.Kod;
						#endregion
					}

					// ToDo : TaD Rez1 a REZ2

					//Tod v konfiguraci udelat ukladani nazvu...

					#region Rezerva 1
					if (VERow.CZ_REZ1_TRACK > 0) //pozadovano zadani hodnoty rez1
					{
						skf.Popis = MST_Global.REZ1_VYDE_NAME;
						skf.CodeType = MST_Global.VydejRez1Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
						skf.Len = (int)Fask.SQLiteDBs.Columns.Vydej.ColumnsInfo_CZMST_SI["REZ_1"].MaxLength;
						skf.CheckLen = !MST_Global.VydejRez1Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
						skf.AllowEmpty = !MST_Global.VydejRez1Povinne;
						skf.Kod = MST_Global.VydejRez1Pamatovat ? Settings.VydejRez1LastValue : string.Empty;

						if (skf.ShowDialog() == DialogResult.Cancel)
							return false;
						rez1 = skf.Kod;
						VIRow.REZ_1 = rez1;
						if (MST_Global.VydejRez1Pamatovat) Settings.VydejRez1LastValue = rez1;
					}
					#endregion

					#region Rezerva 2
					//16.1.2017 JiS pozadavek na zadani hodnoty rez2
					if (VERow.CZ_REZ2_TRACK > 0)
					{
						skf.Popis = MST_Global.REZ2_VYDE_NAME;
						skf.CodeType = MST_Global.VydejRez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
						skf.Len = (int)Fask.SQLiteDBs.Columns.Vydej.ColumnsInfo_CZMST_SI["REZ_2"].MaxLength;
						skf.CheckLen = !MST_Global.VydejRez2Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
						skf.AllowEmpty = !MST_Global.VydejRez2Povinne;
						skf.Kod = MST_Global.VydejRez2Pamatovat ? Settings.VydejRez2LastValue : string.Empty;

						if (skf.ShowDialog() == DialogResult.Cancel)
							return false;
						rez2 = skf.Kod;
						VIRow.REZ_2 = rez2;
						if (MST_Global.VydejRez2Pamatovat) Settings.VydejRez2LastValue = rez2;
					}
					#endregion

					#endregion

					#region Expirace
					//POUŽITELNOST DO
					//17 použitelnost do (RRMMDD) n2+n6
					//Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
					//či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
					// => parametr prijem_pi.cz_expirace_track > 0 => vyzadovat zadani expirace, jinak bez expirace (expirace = null)
					if (VERow.CZ_Expirace_Track > 0)
					{
						if (!expirace.HasValue)
						{
							#region Expirace zadani
							expirace = expiraceLast;
							string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);
							while (true)
							{
								var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
								if (dResExpiration == DialogResult.Cancel)
									return false;

								// validace
								try
								{
									expirace = MST_W.Main.Date_RRMMDD(expirationStr);
									//expirace = DateTime.ParseExact(expirationStr, Main.dateFormatRRMMDD, System.Globalization.DateTimeFormatInfo.InvariantInfo);
								}
								catch (Exception ex)
								{
									MessageBoxBig.Show(String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}

								// pokud az tady, tak koncim a nastavim posledni pouzitou expiraci
								expiraceLast = expirace.Value;
								break;
							}
							#endregion
						}
					}
					else
					{   // pokud polozka neni sledovana na expirace, tak se expirace vynuluje
						expirace = null;
					}
					#endregion

                    #region FEFO/FIFO kontrola
                    // 21.04.2021 JiS - presunuto ze zacatku az sem ... ale kontolovat, jen pokud se vede na sarze 
                    // FEFO/FIFO check
                    if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
                    {
                        if (MST_Global.Vydej_FIFOFEFO_Online)
                        {
                            var sita = Vydej.vydejInstance.globalObject.controller_vydej.SI_GetDataByItemnmbr(VERow.ITEMNMBR);
                            if (!Online.Checks.OnlineFIFOFEFOCheck(VERow.ITEMNMBR, skl_id, VIRow.SERLTNUM, expirace, VERow.CZ_SerNum_Track, VERow.CZ_Expirace_Track, sita))
                                return false;



                        }

						if (MST_Global.Vydej_ExpiraceCheck_Online)
						{
							#region online kontrola vhodnodnosti pouzite expirace

							//if (VERow.CZ_Expirace_Track == 1)
							//{
								if (expirace.HasValue)
								{
									if (!Online.Checks.OnlineOverExpiraci(VERow.ITEMNMBR, skl_id, VIRow.SERLTNUM, expirace.Value))
										return false;
								}
							//}

							#endregion
						}

                    }
                    #endregion


                    vlozenoSN = true;

					//if (MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu)
					//    VIRow.SKL_ID = _sklad != null ? _sklad.skl_id : string.Empty;
					//else
					//    VIRow.SKL_ID = VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID;


					#region Hromadne Generovani palet

					int hromadneBaliky = 1;
					bool prvnitisk = true;

					if (MST_Global.Vydej_HromadneBaliky)
					{
						if (VERow.CZ_SerNum_Track != 0)
							hromadneBaliky = 1;
						else
						{
							using (Dialogs.VydejDialogPocetVydavanych frmPocetBalikuKVydeji = new Dialogs.VydejDialogPocetVydavanych())
							{
								frmPocetBalikuKVydeji.I_Itemnmbr = VERow.ITEMNMBR.Trim();
								frmPocetBalikuKVydeji.I_Itemdesc = VERow.ITEMDESC.Trim();
								frmPocetBalikuKVydeji.I_Itemcode = VERow.ITEMCODE.Trim();
								frmPocetBalikuKVydeji.I_MnozstviZadane = mnozstvi;
								frmPocetBalikuKVydeji.O_PocetJednotek = hromadneBaliky;

								if (DialogResult.Cancel == frmPocetBalikuKVydeji.ShowDialog())
									return false;

								hromadneBaliky = frmPocetBalikuKVydeji.O_PocetJednotek;
							}
						}


						#region Kontrola přeplnení

						// TODO, tady udelat přepočet množství*počet baliku a to porovnat s předlohou, pokud je to přeplneno zobrazit hlášku a nepustit?


						if (!MST_Global.vydejPovolitPreplneniPolozky)
						{

							decimal PocetVBalikoch = mnozstvi * hromadneBaliky;
							decimal PocetVydano = Quantity - mnozstvi;

							if (PocetVBalikoch + PocetVydano > VERow.QTYSHPPD)
							{
								MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								continue;
							}
						}



						#endregion

					}

					#endregion


						
					do
					{

						Guid newguid = Guid.NewGuid();

						#region SSCC a typ palety
						string typepal = string.Empty;
						string nmbrpal = string.Empty;
						try
						{
							//string[] split;
							if (MST_Global.VydejTypOznaceniPalety)
							{
								//split = this.Paleta.Split(new char[] { ':' });
								typepal = this._paleta.ID;
								nmbrpal = this._paleta.sscc;
							}
						}
						catch (Exception ex)
						{
							Logging.Log.WriteDebug(ex.Message);
						}
						#endregion

						#region Lokacni mechanismus - online
						// online ulozeni do lokacniho mechanismu
						if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
						{
							//lokaceService
							Cursor.Current = Cursors.WaitCursor;
							Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
							pohybrow.ITEMNMBR = VERow.ITEMNMBR;
							pohybrow.DOCUMENT_NUMBER = VERow.SOPNUMBE;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
							pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.V;  // vydej
							pohybrow.POHYB_SRC = "V";
							pohybrow.SOURCE = "T";      // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
							pohybrow.QTYSHPPD = (decimal)mnozstvi;
							pohybrow.SERLTNUM = VIRow.SERLTNUM;
							pohybrow.SKL_ID_SRC = skl_id;   //VIRow.IsSKL_IDNull() ? string.Empty : VIRow.SKL_ID;
							pohybrow.SKL_ID_DST = string.Empty;
							pohybrow.LOCNCODE_SRC = VIRow.IsLOCNCODENull() ? string.Empty : VIRow.LOCNCODE;
							pohybrow.LOCNCODE_DST = string.Empty;
							pohybrow.UserID = MST_Global.UserID;
							pohybrow.TermID = MST_Global.TerminalID;
							pohybrow.guid = newguid;
							//pohybrow.dateeveS = ...   // datum serveru se vyplnuje az na serveru
							pohybrow.Expiration = expirace;
							pohybrow.ITEMDESC = VERow.IsITEMDESCNull() ? string.Empty : VERow.ITEMDESC;
							pohybrow.CountEntries = VERow.CountEntries;
							pohybrow.dateeveT = DateTime.Now;   // datum terminalu

							try
							{
								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - start", "LocationLog");
								Classes.LokaceLog.writeBody(pohybrow);

								Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.MoveItem(pohybrow);
								Cursor.Current = Cursors.Default;
								switch (sl.State)
								{
									case Fask.MST_W.LokaceService.States.OK:
										break;
									case Fask.MST_W.LokaceService.States.ERROR:
										MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										//Quantity -= mnozstvi;
										//continue;
										return false;
									default:
										MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										return false;
								}

								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - end", "LocationLog");
							}
							catch (Exception ex)
							{
								Logging.Log.WriteDebug(ex.Message);
								Cursor.Current = Cursors.Default;
								if (MessageBoxBig.Show(ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
								{
									//Promenna ridici cyklus
									bool state = true;

									//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
									while (state)
									{
										try
										{
											// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
											// Volani sluzby pro odstraneni a kontrola navratveho stavu.
											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

											Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
											DialogResult dr = DialogResult.No;
											switch (sl.State)
											{
												case Fask.MST_W.LokaceService.States.OK:
													dr = DialogResult.Yes;
													break;
												case Fask.MST_W.LokaceService.States.ERROR:
													dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
												default:
													dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
											}

											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

											// pokud ho chce ulozit, odejde z cyklu
											if (dr == DialogResult.Yes)
												break;
										}
										//Nejaka online chyba
										catch (Exception exex)
										{
											Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
											Cursor.Current = Cursors.Default;
											if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
											{
												//Ukonceni cyklu - chce zaznam ulozit
												break;
											}
										}
									}
								}
							}
						}
						#endregion

						#region Ulozeni dat do vystupni databaze CZMST_SI
						/* ulozeni dat */
						// prida vydanou polozku do tabulky
						while (true)
						{
							try
							{
								//sita.InsertQuery(
								Vydej.vydejInstance.globalObject.controller_vydej.InsertQuery_SI(
									VERow.CountEntries, //int.Parse(Path.GetFileNameWithoutExtension(filename)),
									VERow.SOPNUMBE,
									VERow.ITEMNMBR,
									VERow.ORD,
									VERow.VNDDOCNM,
									(baliciListGenerovan ? baliciList : (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim())),
									VERow.CZ_CarKod,
									VIRow.LOCNCODE,
									(decimal)mnozstvi,
									VERow.QTYPACK,
									VERow.QTYPACK == 0 ? mnozstvi : mnozstvi / VERow.QTYPACK,    // mnozstvimj = mnozstviZMJ / qtypack |qtypack<>0
									VIRow.SERLTNUM,
									VIRow.KOD_SW,
									VIRow.DAT_VYROBY,
									VIRow.REZ_1,
									this.OdberatelID,
									DateTime.Now.ToString("yyyyMMdd"),
									DateTime.Now.ToString("HHmmss"),
									MST_Global.UserID,
									VERow.DEX_ROW_ID,
									newguid,
									typepal,
									nmbrpal,
									false,
									VIRow.REZ_2,
									_input_mode,
									MST_Global.TerminalID,
									skl_id,     //VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID,
									VERow.IsMJNull() ? string.Empty : VERow.MJ,
									VERow.IsITEMCODENull() ? string.Empty : VERow.ITEMCODE, // ITEMCODE => musi se dodelat ... // VERow.itemcode                                
									VERow.IsWEIGHTNull() ? (decimal?)null : VERow.WEIGHT          // WEIGHT => musi se dodelat ... // VERow.Weight
									, expirace
								);
								break;
							}
							catch (Exception ex)
							{
								Logging.Log.Write("sita.insert," + ex.Message, "Vydej");
								if (DialogResult.Yes != MessageBoxBig.Show(ex.Message + "\n\nPřejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
								{
									#region Lokacni mechanismus - online -> zruseni online pohybu
									// pokud ne, dojde online odmazani ...
									if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
									{
										//Promenna ridici cyklus
										bool state = true;

										//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
										while (state)
										{
											try
											{
												//Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
												//Volani sluzby pro odstraneni a kontrola navratveho kodu.
												Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newguid.ToString(), "LocationLog");

												Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(newguid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
												DialogResult dr = DialogResult.No;
												switch (sl.State)
												{
													case Fask.MST_W.LokaceService.States.OK:
														dr = DialogResult.Yes;
														break;
													case Fask.MST_W.LokaceService.States.ERROR:
														dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + sl.ErrorMessage + "'\nPřejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
														break;
													default:
														dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
														break;
												}

												Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

												// pokud ho chce ulozit, odejde z cyklu
												if (dr == DialogResult.Yes)
													break;
											}
											//Nejaka online chyba
											catch (Exception exex)
											{
												Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
												Cursor.Current = Cursors.Default;
												//if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + exex.Message + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
												if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + exex.Message + "\nPřejete si opakovat operaci lokálního uložení?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
												{
													//Ukonceni cyklu - chce zaznam ulozit
													break;
												}
											}
										}
									}
									else
									{
										//Tady si nejsem jist navratovou hodnotou, ale neni zapla lokace a nepodarilo se ulozit, ale uzivatel to nechce resit
										return false;
									}
									#endregion
								}
							}
						}
						#endregion

						// aktualizace mnozstvi v pameti pro zobrazeni
						this.UpdateDataGrid(mnozstvi, VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD);
						TiskEtiketyNasnimane(VERow, newguid);

						if (MST_Global.Vydej_HromadneBaliky)
						{
							#region Generovani baliku
							
							// Zde generovat SSCC a tisk
							ZmenaPalety(true, prvnitisk);

							if(!prvnitisk)
								Quantity += mnozstvi;

							prvnitisk = false;
							#endregion
						}

						hromadneBaliky--; 

					} while (hromadneBaliky > 0);

					
					// TODO : dodelat zvukovou knihovnu...
					//try
					//{
					//    //MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, MST_W.Properties.Resources.
					//    Stream audiostream = new MemoryStream(Properties.Resources.sound_LASER);
					//    SoundPlayer sp = new SoundPlayer(audiostream);
					//    sp.Play();
					//}
					//catch { }



					baliciListGenerovan = false;

					// Nastaveni default hodnoty, pokud doslo korektne k ulozeni...
					VIRowREZ2_SetDefault(VIRow);

					// pokud je v paramtru nastaveno, bude se zadavat MN po kazdem
					// sejmuti SN
					dalsiSN = (!vydejDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE);

					if (MST_Global.Vydej_HromadneSN)
					{
						dalsiSN = true;
					}


					// zkontroluje, zda je nacten pozadovany pocet
					if (Quantity < VERow.QTYSHPPD)
						continue;   // jeste neni nasnimane vse

					// vnitrni kolecko => pokracovat nebo ukoncit
					if (dalsiSN == false)
						continue;


					// zde se dostane jen v pripade, ze uz mame nasnimany pozadovany pocet
					// TODO : toto je duplicitni -> optimalizovat
					#region Kontrola naplneni polozky
					if (vydejDataParametry.Parametry[0].CONFIG_PTATSE_NEANO)
					{
						if (Quantity > VERow.QTYSHPPD)
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaPreplneniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
								== DialogResult.Yes)
								dalsiSN = false;
						}
						else
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaKompletniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
								== DialogResult.Yes)
								dalsiSN = false;

						}
					}
					#endregion

				} //konec while dalsiSN
				return vlozenoSN;
			}
			finally
			{
				//if (sita.Connection.State == ConnectionState.Open)
				//    sita.Connection.Close();
				Vydej.vydejInstance.globalObject.controller_vydej.Connection_Close(); //<- nechci mit otevrene spojeni ... ???
			}
		}

		/// <summary>
		/// Zpracuje polozku zvolenou z predlohu (snimaniSN atd.)
		/// </summary>
		/// <returns>Vraci, zda bylo vlozeno alespon jedno SN</returns>
		private bool vkladaniSN_Prelokovanim(
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
			string idOdberatele,
			BaseCode code,
			VydejService.Vydej_Items_Online.ItemsRow online_item,
            Classes.LokaceItem lokaceItem
            //string SELTNUM,
            //string LOCNCODE
			)
		{
			// TODO : osetrit praci s vydej_online_item ... 
			// - prevzeti informaci o expiraci, sarzi, polozce, atd ...
			// - kombinace s code x vydej_online_item ... 

			// TODO : FIFO/FEFO kontroly

			SejmiKodForm skf = new SejmiKodForm();

			//SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
			//SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter sesnta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
			//sita.Connection.ConnectionString = "Data source=" + filename;
			//sesnta.Connection = sita.Connection;

			try
			{
				//sita.Connection.Open(); //Otevre connection pro rychlejsi pristup a zapisy do db ...
				Vydej.vydejInstance.globalObject.controller_vydej.Connection_Open(); // <- asi nechci mit porad otevrene spojeni?

				//počet položek už načtenych
				//decimal Quantity = Nacteno(filename, VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD, true);
				decimal Quantity = Vydej.vydejInstance.globalObject.controller_vydej.Nacteno(VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD, true);

				bool vlozenoSN = false;

				#region Preplneni polozky

				if (!MST_Global.vydejPovolitPreplneniPolozky)
				{
					if (Quantity >= VERow.QTYSHPPD)
					{
						MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyNazevZakazano, VERow.ITEMDESC.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
						return vlozenoSN;
					}
				}

				#endregion

				#region Kontrola naplneni polozky

				if (vydejDataParametry.Parametry[0].CONFIG_POKRDOHLED)
				{
					//TaD Vracena podminka, duvod aby to fungovalo jen v připade že množstvi je požadovano
					if ((decimal)Quantity >= VERow.QTYSHPPD)
					{
						// podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
						if ((decimal)Quantity > VERow.QTYSHPPD)
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaPreplneniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
								== DialogResult.Yes)
								return vlozenoSN;
						}
						else
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaVyplnenoMnozstviSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
								== DialogResult.Yes)
								return vlozenoSN;
						}
					}
				}

				#endregion

				string baliciList = "";
				bool baliciListGenerovan = false;
				bool dalsiSN = true;
				string skl_id = string.Empty;
				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow = vydejDataParametry.CZMST_SI.NewCZMST_SIRow();
				VIRowREZ2_SetDefault(VIRow);

				decimal QTY_FromLokZdroj = 0;
				string LokaceCil = string.Empty;
				decimal QTYZmenaLokace = 0;
                // JiS : Task #46 (DP) : predvyplneni QTY_FromLokZdroj z online lokacni polozky
                if (lokaceItem != null) 
                    QTY_FromLokZdroj = lokaceItem.QTY ?? 0;

				int PoradiSN = 0;
				List<string> listSN = null;

				while (dalsiSN)
				{
					#region Vychozi hodnoty a verifikace

					VIRow.SERLTNUM = string.Empty;

					if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.LOCNCODE))
                        VIRow.LOCNCODE = lokaceItem.LOCNCODE;
					else
						VIRow.SERLTNUM = string.Empty;

					VIRow.KOD_SW = string.Empty;
					VIRow.DAT_VYROBY = string.Empty;
					//VIRow.REZ_1 = MST_Global.VydejTypOznaceniPalety ? this.Paleta : string.Empty;
					VIRow.REZ_1 = string.Empty;
					string rez1 = string.Empty;
					string rez2 = string.Empty;

					#region ID Skladu
					// nacteni id skladu
					if (MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu)
						skl_id = _sklad != null ? _sklad.skl_id : string.Empty;
					else
						skl_id = VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID;
					VIRow.SKL_ID = skl_id;

					#endregion

					// vychozi hodnota pro mnozstvi
					decimal mnozstvi = 0;
					#region Verfikace mnozstvi => vychozi hodnota
					{
						if (code is Parsing.Codes.Interfaces.ICodeQuantity)
						{
							mnozstvi = ((Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity ?? 0;
						}
						else if (online_item != null) // je-li polozka dotazena online, tak nastavim vychozi mnozstvi skladove => muze byt pozdeji zmeneno hodnotou z car.kodu polozky
						{
							mnozstvi = online_item.Qty;
						}
					}
					#endregion

					// vychozi hodnota pro Sarzi/SN
					string sn = string.Empty;
					#region Verfikace Sarze/SN online X scan => vychozi hodnota
					{
						string sn_online = string.Empty;
						string sn_code = string.Empty;
						sn_online = online_item != null ? online_item.Serltnum ?? string.Empty : string.Empty;
						
						if ((VERow.CZ_SerNum_Track == 2) && code is Parsing.Codes.Interfaces.ICodeSarze)
							sn_code = ((Parsing.Codes.Interfaces.ICodeSarze)code).Sarze ?? string.Empty;
						else if ((VERow.CZ_SerNum_Track == 1) && code is Parsing.Codes.Interfaces.ICodeSerialNumber)
							sn_code = ((Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN ?? string.Empty;

                        if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR))
                            sn_code = lokaceItem.SERLNMBR.Trim();

						if ((!string.IsNullOrEmpty(sn_online)) && (!string.IsNullOrEmpty(sn_code)))
						{
							if (sn_online != sn_code)
							{
								MessageBoxBig.Show("Šarže/SN online('" + sn_online + "') X scan('" + sn_code + "') se neshodují", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return false;
							}
							else
								sn = sn_online;
						}
						else if (!string.IsNullOrEmpty(sn_online))
						{
							sn = sn_online;
						}
						else if (!string.IsNullOrEmpty(sn_code))
						{
							sn = sn_code;
						}
					}
					#endregion

					// 26.2.2020 ColorProfi JiS
					// doplnen parametr PE.CZ_Expirace_Track a PI.Expirace
					// zadavani expirace, kde kdy ? 
					// jak s fenixem ? a funkncnosti expirace do nejake rezervy?
					DateTime? expirace = null;  // vychozi nastaveni expirace neni ...                    
					#region Verfikace expirace => vychozi hodnota
					{
						DateTime? expirace_online = null;
						if ((online_item != null) && (!online_item.IsExpirationNull()))
							expirace_online = online_item.Expiration;

						DateTime? expirace_code = null;
						if ((code is ICodeExpiration))
							expirace_code = ((ICodeExpiration)code).Expiration;

						if (expirace_online.HasValue && expirace_code.HasValue)
						{
							if (expirace_online.Value.Date != expirace_code.Value.Date)
							{
								MessageBoxBig.Show("Expirace online('" + expirace_online.Value.Date.ToString(Main.dateFormatRRMMDD) + "') X scan('" + expirace_code.Value.ToString(Main.dateFormatRRMMDD) + "') se neshodují", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return false;
							}
							else
								expirace = expirace_online;
						}
						else if (expirace_code.HasValue)
						{
							expirace = expirace_code;
						}
						else if (expirace_online.HasValue)
						{
							expirace = expirace_online;
						}
					}
					#endregion

					#endregion

					#region Lokace zadani Pred SN

					if (MST_Global.vydejZadaniLocncodePredSN)
					{
						if (!ZadaniLocncode(VERow, VIRow))
							break;
					}

					#endregion

					#region Množství vytažené z Lokace

					sejmiForm.SetDefaultValues();
					sejmiForm.Popis = Fask.Localization.Localization.Vydej3ListPolozek3Mnozstviodebranezlokace;
					sejmiForm.CodeType = SejmiKodForm.TypeOfCode.Numeric;
					sejmiForm.Len = 0;
					sejmiForm.CheckLen = false;
					sejmiForm.AllowEmpty = false;
					sejmiForm.veRow = VERow;
					sejmiForm.viRow = VIRow;
                    // JiS : Task #46 : DP - predvyplnit mmostvi z lokace
                    if (lokaceItem != null)
                        sejmiForm.Kod = QTY_FromLokZdroj.ToString(Settings.UIFormatDesCisel);

					if (sejmiForm.ShowDialog() == DialogResult.Cancel)
						return false;

					QTY_FromLokZdroj = Decimal.Parse(sejmiForm.Kod);

					
					#endregion

					#region Typ Sledovani

					// bude snimat SN (overi i snimani DV a SW)
					if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
					{
						#region Sarze/SN

						if (MST_Global.Vydej_HromadneSN)
						{
							#region Hromadne SN generovani
							if ((VERow.CZ_SerNum_Track == 1) && (listSN == null))
							{
								listSN = HromadneSN(VERow.ITEMDESC, Quantity, VERow.QTYSHPPD);

								if (listSN == null)
									break;
							}

							if (PoradiSN > (listSN.Count - 1))
							{
								dalsiSN = false;
								continue;
							}


							sn = listSN[PoradiSN++];
							#endregion
						}

						// nastaveni vychozi hodnoty sn po verifikaci
						VIRow.SERLTNUM = sn;
						// pokud se jedna o sarze, preskocit vsechny kontroly
						// TODO : byse melo kontrolovat, pokud je predloha sarzi, ze nasnimana sarze je v predpisu a pokud neni, tak upozornit a pripadne nechat zmenit
						if (
							(VERow.CZ_SerNum_Track == 2)
							&&
							( // sarze nactena car.kodem nebo zvolena/zadana online dotazem
								(code is Parsing.Codes.Interfaces.ICodeSarze)
								||
								(online_item != null)
							)
							)
						{
							// toto je jiz nastaveno kontrolou vyse => sn = ((Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr ?? string.Empty;
							// preskoci se zadavani
						}
                        else if ((lokaceItem != null) && !string.IsNullOrEmpty(lokaceItem.SERLNMBR))
						{
							VIRow.SERLTNUM = sn;
							// preskoci se zadavani TaD 3.11.2020
						}
						else
						{
							//while (true)
							//{
							//Kontrola ze jde o cislo sarze a chceme overovat vuci zbozi
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//...musi byt nastaven sloupec k navraceni
								string returnValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU.Trim();
								if (returnValueColumnName == string.Empty)
								{
									MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
									return vlozenoSN;
								}

								//Vse je v poradku, zviditelnim btn pro zobrazeni alternativ k sarzim
								sejmiFormSN.btnZobrazitAlternativyVisible = true;
								sejmiFormSN.returnValueColumnName = returnValueColumnName;
								sejmiFormSN.vybiratZboziJenScannerem = MST_Global.VydejZboziVyberJenScannerem;
								sejmiFormSN.vyhledavatZboziDleSloupce = MST_Global.VydejZboziVyhledaniDleSloupce;
							}

							sejmiFormSN.SetDefaultValues();
							sejmiFormSN.Popis = VERow.CZ_SerNum_Track == 2 ? Fask.Localization.Localization.Vydej3ListPolozek3SejmiSarze : (string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SNName));// "Sejmi " + (VERow.CZ_SerNum_Track == 2 ? "Šarže" : MST_Global.SNName);
							sejmiFormSN.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
							sejmiFormSN.Len = 0;
							sejmiFormSN.CheckLen = false;
							sejmiFormSN.AllowEmpty = false;
							sejmiFormSN.veRow = VERow;
							sejmiFormSN.viRow = VIRow;
							sejmiFormSN.Kod = sn;
							sejmiFormSN.vynulujVybraneZbozi();
							//SejmiKodInfoForm3 sejmiSNForm = new SejmiKodInfoForm3(
							//    "Sejmi " + MST_Global.SNName,
							//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
							//    VERow);

							//sejmiFormSN.SESN = sesnta.GetDataByCountEntriesITEMNMBR(VERow.CountEntries, VERow.ITEMNMBR.Trim());

							//sejmiFormSN.SESN = sesnta.GetDataByKeyNacteno(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);
							sejmiFormSN.SESN = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByKeyNacteno_SE_SN(VERow.CountEntries, VERow.SOPNUMBE, VERow.ITEMNMBR, VERow.ORD);


							//Jen kvuli finally bloku
							try
							{
								if (!MST_Global.Vydej_HromadneSN)
								{
									if (sejmiFormSN.ShowDialog() == DialogResult.Cancel)
									{// chce prerusit snimani
										if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{// test, zda je nasnimane pozadovane mnozstvi
											if ((decimal)Quantity < VERow.QTYSHPPD)
											{
												if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
													== DialogResult.Yes)
													return vlozenoSN;
												else
													continue;
												//break;
											}
											//else
											//    break;
											return vlozenoSN;
										}
										//else
										//    break;
										return vlozenoSN;
									}
								}
							}
							catch { }
							finally
							{
								//A schovam tlacitko
								sejmiFormSN.btnZobrazitAlternativyVisible = false;
							}

							// kontrola shody s Carovym kodem (MN)
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_SN_CARKOD)
							{
								if (VERow.CZ_CarKod.Trim() == sejmiFormSN.Kod || (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()) == sejmiFormSN.Kod)
								{
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmutyKodJeStejnyJako, MST_Global.MNName), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}
							}

							//kontrola predlohy SN
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_PREDLOHA_SN)
							{
								Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow sesnrow = sejmiFormSN.SESNSelected;
								if (sesnrow != null) //polozka nenalezena => neni v predloze => dotaz
								{
									if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoVPredlozePokracovatDotaz, MST_Global.SNName, sejmiForm.Kod), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
										continue;
								}
								//else //if (sesndt.Count > 0) //polozka je v predloze, tak pokracuji
								//{
								//}
							}

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola duplicity SN
							#region kontrola duplicity sn
							if (vydejDataParametry.Parametry[0].CONFIG_DUPLIC_SN)
							{
								int sernumCount = Vydej.vydejInstance.globalObject.controller_vydej.CZMST_SI_Get_Pocet_Duplicit_SN_Count(VERow.ITEMNMBR.Trim(), VERow.SOPNUMBE.Trim(), VERow.ORD, sejmiFormSN.Kod);
								//if (VIRows.Length > 0)
								if (sernumCount > 0)
								{
									// Pouze uporozni a umozni pokracovat ...
									if (DialogResult.No == MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SNJizNasnimanoUlozitDotaz, MST_Global.SNName), Fask.Localization.Localization.Vydej3ListPolozek3Upozorneni, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning))
										continue;
								}
							}
							#endregion

							// TODO : prenest do verifikacni casti za tento blok a revidovat
							// kontrola delky (nedela se v SejmiKod formulari, protoze se ma provest az po ostatnich kontrolach
							#region kontrola delky
							if (vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA)
							{
								if (VERow.CZ_SerNum_Delka > 0)
								{
									if (sejmiFormSN.Kod.Length > VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeDelsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											== DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
									else if (sejmiFormSN.Kod.Length < VERow.CZ_SerNum_Delka)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3KodJeKratsiNezXUlozitDotaz, VERow.CZ_SerNum_Delka), Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
											 == DialogResult.No)
										{
											sejmiFormSN.Kod = "";
											continue;
										}
									}
								}
							}
							#endregion

							//Pokud jedna se o cislo sarze a je v parametrech davky, ze se ma opirat o ciselnik, ...
							#region Opirani o ciselnik zbozi ...
							if (VERow.CZ_SerNum_Track == 2 && vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
							{
								//Pokud uz radek nebyl nacten pomoci tlacitka zobrazit alternativy na sejmiKodForm3
								if (sejmiFormSN.VybraneZbozi == null)
								{
									//Musi byt navracen sloupec pro kontrolu
									string confirmValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU.Trim();
									if (confirmValueColumnName == string.Empty)
									{
										MessageBox.Show(Fask.Localization.Localization.Vydej3ListPolozek3VParametrechChybiNazevSloupce, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										return false;
									}

									try
									{
										//Kurzor
										Cursor.Current = Cursors.WaitCursor;

										Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = Vydej.vydejInstance.globalObject.controller_zbozi.GetTableByConfirmValue(VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), confirmValueColumnName, sejmiFormSN.Kod);

										//Kurzor zpet
										Cursor.Current = Cursors.Default;

										//Rozhodovani dle cetnosti zaznamu - jeden, ok
										if (table.Rows.Count == 1)
										{
											//Beru nastaveny sloupec
											sejmiFormSN.Kod = table.Rows[0][sejmiFormSN.returnValueColumnName].ToString().Trim();
										}
										//Zobrazim vyber
										else if (table.Rows.Count > 1)
										//else //if (table.Rows.Count > 1)
										{
											//cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod)" +
											//        " UNION " +
											//        " SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum)";
											//cmd.CommandText = cmdText;
											//reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
											//table.Clear();
											//table.Load(reader);

											using (ListZbozi lz = new ListZbozi(MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
											{
												//Byl vybran nejaky radek
												lz.showTable(table);
												if (lz.ShowDialog() == DialogResult.OK)
												{
													sejmiFormSN.Kod = lz.vybraneZbozi[sejmiFormSN.returnValueColumnName].ToString().Trim();
													sejmiFormSN.InputMode = lz.input_mode;
												}
												else
												{
													//Zobrazeni
													if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3NevybranaMoznaVariantaOpakovatDotaz, Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
													{
														//Cyklim znovu
														continue;
													}
													else
													{
														return false;
													}
												}
											}
										}
										else /* table.Rows.Count < 1, tzn. 0 = zadny zaznam */
										{
											//Zobrazeni
											if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoZboziKCarKodOpakovatDotaz, VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()), sejmiFormSN.Kod, confirmValueColumnName), Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
											{
												//Cyklim znovu
												continue;
											}
											else
											{
												return false;
											}
										}
									}
									catch (Exception ex)
									{
										//Kurzor zpet - v pripade chyby
										Cursor.Current = Cursors.Default;
										//Zobrazeni chyby a zalogovani
										MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
										Logging.Log.Write(ex);
										return false;
									}
									finally
									{
									}
								}
							}
							#endregion

							VIRow.SERLTNUM = sejmiFormSN.Kod;
						}

						// TODO : sem umistit kontroly zadane Sarze/SN (predelat z vetve zadani sn
						// - kontrola duplicity
						// - kontrola delky

						//Vezme kod z formu a metodu ziskani - ta prebije dosavadni metodu
						_input_mode = sejmiFormSN.InputMode;

						if (VERow.CZ_SerNum_Track == 2)
						{// sejme mnozstvi k SN
							string rez_2 = VIRow.REZ_2;
							if (sejmiMnozstvi_Prelok(ref mnozstvi, Quantity, VERow, VIRow, sejmiFormSN.SESNSelected, ref rez_2, code, QTY_FromLokZdroj) != 0)
								break;
							VIRow.REZ_2 = rez_2;

							if (!MST_Global.vydejPovolitPreplneniPolozky)
							{
								if (mnozstvi + Quantity > VERow.QTYSHPPD)
								{
									MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
									continue;
								}
							}

							//VIRow.QTYPACK = (decimal)mnozstvi;

							// pokud je skryto pozadovane mnozstvi a je disproporce, zazada o potvrzeni
							if (vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI)
							{
								if (((decimal)(Quantity + mnozstvi)) != VERow.QTYSHPPD)
								{
									using (Fask.MST_W.Vydej_3.PotvrditMnozstviForm potrvditMnozstviForm =
										new Fask.MST_W.Vydej_3.PotvrditMnozstviForm(VERow.QTYSHPPD, Quantity + mnozstvi))
									{
										if (potrvditMnozstviForm.ShowDialog() == DialogResult.Cancel)
											continue; // bude opakovat zadavani
									}
								}
							}
						}
						else
							mnozstvi = (VERow.QTYPACK > 0 ? VERow.QTYPACK : 1);

						Quantity += mnozstvi;

						//    //Pokud sem kod dojde, uz se  nebude cyklit
						//    break;
						//}
						#endregion
					} // end if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
					// nebude snimat SN, jen zada pocet (DV SW a REZ_1 jsou "")
					else if (VERow.CZ_SerNum_Track == 0)
					{
						#region Mnozstvi
						//START - NEW
						if (vydejDataParametry.Parametry[0].CONFIG_POUZIT_CISELNIK_ZBOZI)
						{
							////...musi byt nastaven sloupec k navraceni
							//string returnValueColumnName = vydejDataParametry.Parametry[0].CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU.Trim();
							//if (returnValueColumnName == string.Empty)
							//{
							//    MessageBox.Show("V parametrech dávky je nutné vyplnit název sloupce jehož hodnota se má zaznamenat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
							//    return false;
							//}

							try
							{
								//Kurzor
								Cursor.Current = Cursors.WaitCursor;

								Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = Vydej.vydejInstance.globalObject.controller_zbozi.GetTableByBoth(VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim()));

								//Kurzor zpet
								Cursor.Current = Cursors.Default;

								//Rozhodovani dle cetnosti zaznamu - jeden, ok
								//if (table.Rows.Count == 1)
								if (table.Rows.Count >= 1)
								{
									//Beru hodnotu z nastaveneho sloupce
									// JiS : zadne serltnum, neni zamena ..., existuje, tak dal...
									//VIRow.SERLTNUM = table.Rows[0][returnValueColumnName].ToString().Trim();
								}
								//Zobrazim vyber
								//else if (table.Rows.Count > 1)
								//else //if (table.Rows.Count > 1)
								//{
								//    cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod)"+ 
								//        " UNION " +
								//        " SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum)";
								//    cmd.CommandText = cmdText;
								//    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
								//    table.Clear();
								//    table.Load(reader);

								//    //using (ListZbozi lz = new ListZbozi(MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
								//    using (ListZbozi lz = new ListZbozi(VERow.CZ_CarKod.Trim(), VERow.VNDITNUM.Trim(), MST_Global.VydejZboziVyberJenScannerem, MST_Global.VydejZboziVyhledaniDleSloupce))
								//    {
								//        while (true)
								//        {
								//            //Byl vybran nejaky radek
								//            lz.showTable(table);
								//            if (lz.ShowDialog() == DialogResult.OK)
								//            {
								//                VIRow.SERLTNUM = lz.vybraneZbozi[returnValueColumnName].ToString().Trim();
								//                _input_mode = lz.input_mode;
								//                break;
								//            }
								//            else
								//            {
								//                //Zobrazeni
								//                if (MessageBoxBig.Show("Nebyla vybrána žádná z možných variant zboží.\nPřejete si volbu opakovat?", Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.Yes)
								//                {
								//                    //Cyklim znovu
								//                    continue;
								//                }
								//                else
								//                {
								//                    return false;
								//                }
								//            }
								//        }
								//    }
								//}
								else /* table.Rows.Count < 1, tzn. 0 = zadny zaznam */
								{
									//Zobrazeni
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3NenalezenoZboziKCarKod, VERow.CZ_CarKod.Trim(), (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim())), Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
									//konec
									return false;
								}
							}
							catch (Exception ex)
							{
								//Kurzor zpet - v pripade chyby
								Cursor.Current = Cursors.Default;
								//Zobrazeni chyby a zalogovani
								MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
								Logging.Log.Write(ex);
								return false;
							}
							finally
							{
							}
						}
						//END - NEW

						string rez_2 = VIRow.REZ_2;


						if (sejmiMnozstvi_Prelok(ref mnozstvi, Quantity, VERow, VIRow, null, ref rez_2, code, QTY_FromLokZdroj) != 0)
							break;

						VIRow.REZ_2 = rez_2;

						if (!MST_Global.vydejPovolitPreplneniPolozky)
						{
							if (mnozstvi + Quantity > VERow.QTYSHPPD)
							{
								MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PreplneniPolozkyZakazano, Fask.Localization.Localization.Vydej3ListPolozek3Info, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
								continue;
							}
						}

						//VIRow.QTYPACK = (decimal)mnozstvi;

						// pokud je skryto pozadovane mnozstvi a je disproporce, zazada o potvrzeni
						if (vydejDataParametry.Parametry[0].CONFIG_SKRYT_MNOZSTVI)
						{
							if (((decimal)(Quantity + mnozstvi)) != VERow.QTYSHPPD)
							{
								using (Fask.MST_W.Vydej_3.PotvrditMnozstviForm potrvditMnozstviForm =
									new Fask.MST_W.Vydej_3.PotvrditMnozstviForm(VERow.QTYSHPPD, Quantity + mnozstvi))
								{
									if (potrvditMnozstviForm.ShowDialog() == DialogResult.Cancel)
										continue; // bude opakovat zadavani
								}
							}
						}

						Quantity += mnozstvi;
						#endregion
					}


					// generovani baliciho kodu
					// TODO : Co ma spravne delat generovani baliciho listu ???
					else // if (VERow.CZ_SerNum_Track == 9)
					{
						MessageBoxBig.Show("Dosud nepřevedeno do sql varianty ...");
						//    baliciList = VERow.CZ_CarKod; // uschova si kod baliciho listu

						//    // prepise VERow skutecnou predlohou
						//    if (generBalList(out VERow) != 0)
						//        break; // prerusil generovani

						//    baliciListGenerovan = true;
						//    continue; // zopakuje uz pro zmeneny sinstruct
					}

                    #endregion

					#region Lokace zadani PO SN

					if (!MST_Global.vydejZadaniLocncodePredSN)
					{
						if (!ZadaniLocncode(VERow, VIRow))
							break;
					}

					#endregion

					#region Vkladani doplnujicich informaci

					if (VERow.CZ_SW_Track == 1)
					{
						#region Softwarova verze
						sejmiForm.SetDefaultValues();
						sejmiForm.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.SWName);   // "Sejmi " + MST_Global.SWName;
						sejmiForm.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						sejmiForm.Len = VERow.CZ_SW_Delka;
						sejmiForm.CheckLen = vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA;
						sejmiForm.AllowEmpty = false;
						sejmiForm.veRow = VERow;
						sejmiForm.viRow = VIRow;
						sejmiForm.Kod = string.Empty;

						//SejmiKodInfoForm3 sejmiSWForm = new SejmiKodInfoForm3(
						//    "Sejmi " + MST_Global.SWName,
						//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
						//    VERow.CZ_SW_Delka,
						//    vydejData.Parametry[0].CONFIG_KONT_DELKA,
						//    false,
						//    VERow
						//    );
						bool opakuj;
						DialogResult res = DialogResult.No;
						do
						{
							opakuj = false;
							if (sejmiForm.ShowDialog() == DialogResult.Cancel)
							{
								if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
								{// test, zda je nasnimane pozadovane mnozstvi
									if ((decimal)Quantity < VERow.QTYSHPPD)
									{
										res = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
										if (res == DialogResult.No)
											opakuj = true;
									}
								}
								else
									break;
							}
						} while (opakuj);
						if (res == DialogResult.Yes)
							break;
						VIRow.KOD_SW = sejmiForm.Kod;
						#endregion
					}

					if (VERow.CZ_DatVyr_Track == 1)
					{
						#region Datum vyroby
						sejmiForm.SetDefaultValues();
						sejmiForm.Popis = string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SejmiSN, MST_Global.DVName);       // "Sejmi " + MST_Global.DVName;
						sejmiForm.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						sejmiForm.Len = VERow.CZ_DatVyr_Delka;
						sejmiForm.CheckLen = vydejDataParametry.Parametry[0].CONFIG_KONT_DELKA;
						sejmiForm.AllowEmpty = false;
						sejmiForm.veRow = VERow;
						sejmiForm.viRow = VIRow;
						sejmiForm.Kod = string.Empty;
						//SejmiKodInfoForm3 sejmiDVForm = new SejmiKodInfoForm3(
						//    "Sejmi " + MST_Global.DVName,
						//    SejmiKodInfoForm3.TypeOfCode.AlphaNumeric,
						//    VERow.CZ_DatVyr_Delka,
						//    vydejData.Parametry[0].CONFIG_KONT_DELKA,
						//    false, 
						//    VERow
						//    );
						bool opakuj;
						DialogResult res = DialogResult.No;
						do
						{
							opakuj = false;
							if (sejmiForm.ShowDialog() == DialogResult.Cancel)
							{
								if (vydejDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
								{// test, zda je nasnimane pozadovane mnozstvi
									if ((decimal)Quantity < VERow.QTYSHPPD)
									{
										res = MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaNeniKompletniUkoncitDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
										if (res == DialogResult.No)
											opakuj = true;
									}
								}
								else
									break;
							}
						} while (opakuj);
						if (res == DialogResult.Yes)
							break;
						VIRow.DAT_VYROBY = sejmiForm.Kod;
						#endregion
					}

					// ToDo : TaD Rez1 a REZ2

					//Tod v konfiguraci udelat ukladani nazvu...

					#region Rezerva 1
					if (VERow.CZ_REZ1_TRACK > 0) //pozadovano zadani hodnoty rez1
					{
						skf.Popis = MST_Global.REZ1_VYDE_NAME;
						skf.CodeType = MST_Global.VydejRez1Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
						skf.Len = (int)Fask.SQLiteDBs.Columns.Vydej.ColumnsInfo_CZMST_SI["REZ_1"].MaxLength;
						skf.CheckLen = !MST_Global.VydejRez1Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
						skf.AllowEmpty = !MST_Global.VydejRez1Povinne;
						skf.Kod = MST_Global.VydejRez1Pamatovat ? Settings.VydejRez1LastValue : string.Empty;

						if (skf.ShowDialog() == DialogResult.Cancel)
							return false;
						rez1 = skf.Kod;
						VIRow.REZ_1 = rez1;
						if (MST_Global.VydejRez1Pamatovat) Settings.VydejRez1LastValue = rez1;
					}
					#endregion

					#region Rezerva 2
					//16.1.2017 JiS pozadavek na zadani hodnoty rez2
					if (VERow.CZ_REZ2_TRACK > 0)
					{
						skf.Popis = MST_Global.REZ2_VYDE_NAME;
						skf.CodeType = MST_Global.VydejRez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
						skf.Len = (int)Fask.SQLiteDBs.Columns.Vydej.ColumnsInfo_CZMST_SI["REZ_2"].MaxLength;
						skf.CheckLen = !MST_Global.VydejRez2Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
						skf.AllowEmpty = !MST_Global.VydejRez2Povinne;
						skf.Kod = MST_Global.VydejRez2Pamatovat ? Settings.VydejRez2LastValue : string.Empty;

						if (skf.ShowDialog() == DialogResult.Cancel)
							return false;
						rez2 = skf.Kod;
						VIRow.REZ_2 = rez2;
						if (MST_Global.VydejRez2Pamatovat) Settings.VydejRez2LastValue = rez2;
					}
					#endregion

					#endregion

					#region Expirace

					//POUŽITELNOST DO
					//17 použitelnost do (RRMMDD) n2+n6
					//Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
					//či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
					// => parametr prijem_pi.cz_expirace_track > 0 => vyzadovat zadani expirace, jinak bez expirace (expirace = null)
					if (VERow.CZ_Expirace_Track > 0)
					{
						if (!expirace.HasValue)
						{
							#region Expirace zadani
							expirace = expiraceLast;
							string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);
							while (true)
							{
								var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
								if (dResExpiration == DialogResult.Cancel)
									return false;

								// validace
								try
								{
									expirace = MST_W.Main.Date_RRMMDD(expirationStr);
									//expirace = DateTime.ParseExact(expirationStr, Main.dateFormatRRMMDD, System.Globalization.DateTimeFormatInfo.InvariantInfo);
								}
								catch (Exception ex)
								{
									MessageBoxBig.Show(String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									continue;
								}

								// pokud az tady, tak koncim a nastavim posledni pouzitou expiraci
								expiraceLast = expirace.Value;
								break;
							}
							#endregion
						}
					}
					else
					{   // pokud polozka neni sledovana na expirace, tak se expirace vynuluje
						expirace = null;
					}

					#endregion

                    #region FEFO/FIFO kontrola
                    // 21.04.2021 JiS - presunuto ze zacatku az sem ... ale kontolovat, jen pokud se vede na sarze 
                    // FEFO/FIFO check
                    if ((VERow.CZ_SerNum_Track == 1) || (VERow.CZ_SerNum_Track == 2))
                    {
                        if (MST_Global.Vydej_FIFOFEFO_Online)
                        {
                            var sita = Vydej.vydejInstance.globalObject.controller_vydej.SI_GetDataByItemnmbr(VERow.ITEMNMBR);
                            if (!Online.Checks.OnlineFIFOFEFOCheck(VERow.ITEMNMBR, skl_id, VIRow.SERLTNUM, expirace, VERow.CZ_SerNum_Track, VERow.CZ_Expirace_Track, sita))
                                return false;
                        }

						if (MST_Global.Vydej_ExpiraceCheck_Online)
						{
							#region online kontrola vhodnodnosti pouzite expirace

							//if (VERow.CZ_Expirace_Track == 1)
							//{
								if (expirace.HasValue)
								{
									if (!Online.Checks.OnlineOverExpiraci(VERow.ITEMNMBR, skl_id, VIRow.SERLTNUM, expirace.Value))
										return false;
								}
							//}

							#endregion
						}
                    }
                    #endregion

					vlozenoSN = true;

					#region SSCC a typ palety

					string typepal = string.Empty;
					string nmbrpal = string.Empty;
					try
					{
						//string[] split;
						if (MST_Global.VydejTypOznaceniPalety)
						{
							//split = this.Paleta.Split(new char[] { ':' });
							typepal = this.Paleta.ID;
							nmbrpal = this.Paleta.sscc;
						}
					}
					catch (Exception ex)
					{
						Logging.Log.WriteDebug(ex.Message);
					}

					#endregion

					//if (MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu)
					//    VIRow.SKL_ID = _sklad != null ? _sklad.skl_id : string.Empty;
					//else
					//    VIRow.SKL_ID = VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID;



					#region Zadani cilove Lokace

					LokaceCil = ZadaniLocncode_Cilove(VERow, VIRow);

					if (LokaceCil == null)
						break;

					#endregion

					#region Množství pro přelokovani, potvrdit

					sejmiForm.SetDefaultValues();
					sejmiForm.Popis = Fask.Localization.Localization.Vydej3ListPolozek3Mnozstvidocilovelokace; // TODO Lokalizovat??
					sejmiForm.CodeType = SejmiKodForm.TypeOfCode.Numeric;
					sejmiForm.Len = 0;
					sejmiForm.CheckLen = false;
					sejmiForm.AllowEmpty = false;
					sejmiForm.veRow = VERow;
					sejmiForm.viRow = VIRow;

					QTYZmenaLokace = 0;
					QTYZmenaLokace = QTY_FromLokZdroj - mnozstvi;

					if (QTYZmenaLokace > 0)
					{
                        sejmiForm.Kod = QTYZmenaLokace.ToString(Settings.UIFormatDesCisel);
                        // JiS : Task #46 - DP : zamezit moznost zmeny mnozstvi v dialogu
                        if (lokaceItem != null)
                            sejmiForm.KodReadOnly(lokaceItem.QTY != null);

						if (sejmiForm.ShowDialog() == DialogResult.Cancel)
							return false;

                        QTYZmenaLokace = Decimal.Parse(sejmiForm.Kod);
                    }                    
                    
                    if (QTYZmenaLokace < 0)
                    {
                        // TODO : lokalizce 
                        throw new Exception(String.Format("Množství zbývající k přelokování vyšlo záporné: {0} !!!" + System.Environment.NewLine + "Nelze pokračovat", QTYZmenaLokace));
                    }

					#endregion


					Guid newguid = Guid.NewGuid();

					#region Lokacni mechanismus - online

					// online ulozeni do lokacniho mechanismu
					if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
					{
						#region Vydej z Lokace

						//lokaceService
						Cursor.Current = Cursors.WaitCursor;
						Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
						pohybrow.ITEMNMBR = VERow.ITEMNMBR;
						pohybrow.DOCUMENT_NUMBER = VERow.SOPNUMBE;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
						pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.V;  // vydej
						pohybrow.POHYB_SRC = "V";
						pohybrow.SOURCE = "T";      // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
						pohybrow.QTYSHPPD = (decimal)mnozstvi;
						pohybrow.SERLTNUM = VIRow.SERLTNUM;
						pohybrow.SKL_ID_SRC = skl_id;   //VIRow.IsSKL_IDNull() ? string.Empty : VIRow.SKL_ID;
						pohybrow.SKL_ID_DST = string.Empty;
						pohybrow.LOCNCODE_SRC = VIRow.IsLOCNCODENull() ? string.Empty : VIRow.LOCNCODE;
						pohybrow.LOCNCODE_DST = string.Empty;
						pohybrow.UserID = MST_Global.UserID;
						pohybrow.TermID = MST_Global.TerminalID;
						pohybrow.guid = newguid;
						//pohybrow.dateeveS = ...   // datum serveru se vyplnuje az na serveru
						pohybrow.Expiration = expirace;
						pohybrow.ITEMDESC = VERow.IsITEMDESCNull() ? string.Empty : VERow.ITEMDESC;
						pohybrow.CountEntries = VERow.CountEntries;
						pohybrow.dateeveT = DateTime.Now;   // datum terminalu

						try
						{
							Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - start", "LocationLog");
							Classes.LokaceLog.writeBody(pohybrow);

							Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.MoveItem(pohybrow);
							Cursor.Current = Cursors.Default;
							switch (sl.State)
							{
								case Fask.MST_W.LokaceService.States.OK:
									break;
								case Fask.MST_W.LokaceService.States.ERROR:
									MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									Quantity -= mnozstvi;
									continue;
								//return false;
								default:
									MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
									return false;
							}

							Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - end", "LocationLog");
						}
						catch (Exception ex)
						{
							Logging.Log.WriteDebug(ex.Message);
							Cursor.Current = Cursors.Default;
							if (MessageBoxBig.Show(ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
							{
								//Promenna ridici cyklus
								bool state = true;

								//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
								while (state)
								{
									try
									{
										// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
										// Volani sluzby pro odstraneni a kontrola navratveho stavu.
										Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

										Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
										DialogResult dr = DialogResult.No;
										switch (sl.State)
										{
											case Fask.MST_W.LokaceService.States.OK:
												dr = DialogResult.Yes;
												break;
											case Fask.MST_W.LokaceService.States.ERROR:
												dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
												return false;
											default:
												dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
												return false;
										}

										Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

										// pokud ho chce ulozit, odejde z cyklu
										if (dr == DialogResult.Yes)
											break;
									}
									//Nejaka online chyba
									catch (Exception exex)
									{
										Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
										Cursor.Current = Cursors.Default;
										if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
										{
											//Ukonceni cyklu - chce zaznam ulozit
											break;
										}
									}
								}
							}
						} 

						#endregion

						#region Prevod do Cilove lokace
						//lokaceService

						if (QTYZmenaLokace > 0)
						{
							Cursor.Current = Cursors.WaitCursor;
							Fask.MST_W.LokaceService.LokacePohyb pohybrowPrijem = new Fask.MST_W.LokaceService.LokacePohyb();

							pohybrowPrijem.ITEMNMBR = VERow.ITEMNMBR;
							pohybrowPrijem.DOCUMENT_NUMBER = string.Empty;
							pohybrowPrijem.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.D;
							pohybrowPrijem.POHYB_SRC = "D";
							pohybrowPrijem.SOURCE = "T";
							pohybrowPrijem.QTYSHPPD = (decimal)QTYZmenaLokace;
							pohybrowPrijem.SERLTNUM = VIRow.SERLTNUM;
							pohybrowPrijem.SKL_ID_SRC = skl_id;
							pohybrowPrijem.SKL_ID_DST = skl_id;
							pohybrowPrijem.LOCNCODE_SRC = VIRow.IsLOCNCODENull() ? string.Empty : VIRow.LOCNCODE;
							pohybrowPrijem.LOCNCODE_DST = LokaceCil;
							pohybrowPrijem.UserID = MST_Global.UserID;
							pohybrowPrijem.TermID = MST_Global.TerminalID;
							pohybrowPrijem.guid = newguid;
							pohybrowPrijem.Expiration = expirace;
							pohybrowPrijem.ITEMDESC = VERow.IsITEMDESCNull() ? string.Empty : VERow.ITEMDESC;
							pohybrowPrijem.CountEntries = VERow.CountEntries;
							pohybrowPrijem.dateeveT = DateTime.Now;   // datum terminalu

							try
							{
								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.D + ",Function:" + this.ToString() + ".MoveItem - start", "LocationLog");
								Classes.LokaceLog.writeBody(pohybrowPrijem);

								Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.MoveItem(pohybrowPrijem);
								Cursor.Current = Cursors.Default;
								switch (sl.State)
								{
									case Fask.MST_W.LokaceService.States.OK:
										break;
									case Fask.MST_W.LokaceService.States.ERROR:
										{
											MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

											//Promenna ridici cyklus
											bool state = true;

											//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
											while (state)
											{
												try
												{
													// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
													// Volani sluzby pro odstraneni a kontrola navratveho stavu.
													Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

													Fask.MST_W.LokaceService.StatusLokace sla = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
													DialogResult dr = DialogResult.No;
													switch (sla.State)
													{
														case Fask.MST_W.LokaceService.States.OK:
															dr = DialogResult.Yes;
															break;
														case Fask.MST_W.LokaceService.States.ERROR:
															dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
															return false;
														default:
															dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
															return false;
													}

													Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

													// pokud ho chce ulozit, odejde z cyklu
													if (dr == DialogResult.Yes)
														break;
												}
												//Nejaka online chyba
												catch (Exception exex)
												{
													Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
													Cursor.Current = Cursors.Default;
													if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
													{
														//Ukonceni cyklu - chce zaznam ulozit
														break;
													}
												}
											}
										}
										continue;
									//return false;
									default:
										{
											MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

											//Promenna ridici cyklus
											bool state = true;

											//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
											while (state)
											{
												try
												{
													// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
													// Volani sluzby pro odstraneni a kontrola navratveho stavu.
													Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

													Fask.MST_W.LokaceService.StatusLokace sla = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
													DialogResult dr = DialogResult.No;
													switch (sla.State)
													{
														case Fask.MST_W.LokaceService.States.OK:
															dr = DialogResult.Yes;
															break;
														case Fask.MST_W.LokaceService.States.ERROR:
															dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
															return false;
														default:
															dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
															return false;
													}

													Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

													// pokud ho chce ulozit, odejde z cyklu
													if (dr == DialogResult.Yes)
														break;
												}
												//Nejaka online chyba
												catch (Exception exex)
												{
													Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
													Cursor.Current = Cursors.Default;
													if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
													{
														//Ukonceni cyklu - chce zaznam ulozit
														break;
													}
												}
											}
											return false;
										}
								}

								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".MoveItem - end", "LocationLog");
							}
							catch (Exception ex)
							{
								#region catch
								Logging.Log.WriteDebug(ex.Message);
								Cursor.Current = Cursors.Default;
								if (MessageBoxBig.Show(ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) != DialogResult.Yes)
								{
									//Promenna ridici cyklus
									bool state = true;

									//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
									while (state)
									{
										try
										{
											// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
											// Volani sluzby pro odstraneni a kontrola navratveho stavu.
											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

											Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
											DialogResult dr = DialogResult.No;
											switch (sl.State)
											{
												case Fask.MST_W.LokaceService.States.OK:
													dr = DialogResult.Yes;
													break;
												case Fask.MST_W.LokaceService.States.ERROR:
													dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
												default:
													dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
											}

											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

											// pokud ho chce ulozit, odejde z cyklu
											if (dr == DialogResult.Yes)
												break;
										}
										//Nejaka online chyba
										catch (Exception exex)
										{
											Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
											Cursor.Current = Cursors.Default;
											if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
											{
												//Ukonceni cyklu - chce zaznam ulozit
												break;
											}
										}
									}
								}
								#endregion
							}
						}
						#endregion

					}

					#endregion

					#region Ulozeni dat do vystupni databaze CZMST_SI

					/* ulozeni dat */
					// prida vydanou polozku do tabulky
					while (true)
					{
						try
						{
							//sita.InsertQuery(
							Vydej.vydejInstance.globalObject.controller_vydej.InsertQuery_SI(
								VERow.CountEntries, //int.Parse(Path.GetFileNameWithoutExtension(filename)),
								VERow.SOPNUMBE,
								VERow.ITEMNMBR,
								VERow.ORD,
								VERow.VNDDOCNM,
								(baliciListGenerovan ? baliciList : (VERow.IsVNDITNUMNull() ? string.Empty : VERow.VNDITNUM.Trim())),
								VERow.CZ_CarKod,
								VIRow.LOCNCODE,
								(decimal)mnozstvi,
								VERow.QTYPACK,
								VERow.QTYPACK == 0 ? mnozstvi : mnozstvi / VERow.QTYPACK,    // mnozstvimj = mnozstviZMJ / qtypack |qtypack<>0
								VIRow.SERLTNUM,
								VIRow.KOD_SW,
								VIRow.DAT_VYROBY,
								VIRow.REZ_1,
								this.OdberatelID,
								DateTime.Now.ToString("yyyyMMdd"),
								DateTime.Now.ToString("HHmmss"),
								MST_Global.UserID,
								VERow.DEX_ROW_ID,
								newguid,
								typepal,
								nmbrpal,
								false,
								VIRow.REZ_2,
								_input_mode,
								MST_Global.TerminalID,
								skl_id,     //VERow.IsSKL_IDNull() ? string.Empty : VERow.SKL_ID,
								VERow.IsMJNull() ? string.Empty : VERow.MJ,
								VERow.IsITEMCODENull() ? string.Empty : VERow.ITEMCODE, // ITEMCODE => musi se dodelat ... // VERow.itemcode                                
								VERow.IsWEIGHTNull() ? (decimal?)null : VERow.WEIGHT          // WEIGHT => musi se dodelat ... // VERow.Weight
								, expirace
							);
							break;
						}
						catch (Exception ex)
						{
							Logging.Log.Write("sita.insert," + ex.Message, "Vydej");
							if (DialogResult.Yes != MessageBoxBig.Show(ex.Message + "\n\nPřejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
							{
								#region Lokacni mechanismus - online -> zruseni online pohybu
								// pokud ne, dojde online odmazani ...
								if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
								{
									//Promenna ridici cyklus
									bool state = true;

									//Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
									while (state)
									{
										try
										{
											// Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
											// Volani sluzby pro odstraneni a kontrola navratveho stavu.
											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newguid, "LocationLog");

											Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(newguid, Fask.MST_W.LokaceService.ModulName.DEFREGMENTACE);
											DialogResult dr = DialogResult.No;
											switch (sl.State)
											{
												case Fask.MST_W.LokaceService.States.OK:
													dr = DialogResult.Yes;
													break;
												case Fask.MST_W.LokaceService.States.ERROR:
													dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
												default:
													dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return false;
											}

											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:V,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

											// pokud ho chce ulozit, odejde z cyklu
											if (dr == DialogResult.Yes)
												break;
										}
										//Nejaka online chyba
										catch (Exception exex)
										{
											Logging.Log.Write("Chyba při mazání lokací : " + exex.Message);
											Cursor.Current = Cursors.Default;
											if (MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.Yes)
											{
												//Ukonceni cyklu - chce zaznam ulozit
												break;
											}
										}
									}
								}
								else
								{
									//Tady si nejsem jist navratovou hodnotou, ale neni zapla lokace a nepodarilo se ulozit, ale uzivatel to nechce resit
									return false;
								}
								#endregion
							}
						}
					}

					#endregion


					// TODO : dodelat zvukovou knihovnu...
					//try
					//{
					//    //MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, MST_W.Properties.Resources.
					//    Stream audiostream = new MemoryStream(Properties.Resources.sound_LASER);
					//    SoundPlayer sp = new SoundPlayer(audiostream);
					//    sp.Play();
					//}
					//catch { }

					// aktualizace mnozstvi v pameti pro zobrazeni
					this.UpdateDataGrid(mnozstvi, VERow.ITEMNMBR, VERow.SOPNUMBE, VERow.ORD);

					baliciListGenerovan = false;

					// Nastaveni default hodnoty, pokud doslo korektne k ulozeni...
					VIRowREZ2_SetDefault(VIRow);

					#region Tisk

					TiskEtiketyNasnimane(VERow, newguid);

					#endregion

					// pokud je v paramtru nastaveno, bude se zadavat MN po kazdem
					// sejmuti SN
					dalsiSN = (!vydejDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE);

					if (MST_Global.Vydej_HromadneSN)
					{
						dalsiSN = true;
					}


					// zkontroluje, zda je nacten pozadovany pocet
					if (Quantity < VERow.QTYSHPPD)
						continue;   // jeste neni nasnimane vse

					// vnitrni kolecko => pokracovat nebo ukoncit
					if (dalsiSN == false)
						continue;


					// zde se dostane jen v pripade, ze uz mame nasnimany pozadovany pocet
					// TODO : toto je duplicitni -> optimalizovat

					#region Kontrola naplneni polozky

					if (vydejDataParametry.Parametry[0].CONFIG_PTATSE_NEANO)
					{
						if (Quantity > VERow.QTYSHPPD)
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaPreplneniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning)
								== DialogResult.Yes)
								dalsiSN = false;
						}
						else
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Vydej3ListPolozek3PolozkaKompletniSnimatJineDotaz, Fask.Localization.Localization.Vydej3ListPolozek3Dotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
								== DialogResult.Yes)
								dalsiSN = false;

						}
					}

					#endregion

				} //konec while dalsiSN
				return vlozenoSN;
			}
			finally
			{
				//if (sita.Connection.State == ConnectionState.Open)
				//    sita.Connection.Close();
				Vydej.vydejInstance.globalObject.controller_vydej.Connection_Close(); //<- nechci mit otevrene spojeni ... ???
			}
		}
		
		#endregion

		#region PomocneMetody

		private string ZadaniLocncode_Cilove(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow, Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow)
		{
			string LOC = string.Empty;

				if (MST_Global.VydejLocationPouzitCiselnik)
				{
					using (Forms.FormLokaceVyber flokace = new FormLokaceVyber(_sklad != null ? _sklad.skl_id : string.Empty))
					{
						flokace.Owner = this;
						if (flokace.ShowDialog() == DialogResult.Cancel)
							return null;

						LOC = flokace.Lokace.LOCNCODE.Trim();

						return LOC;
					}
				}
				else
				{
					sejmiFormLokace.SetDefaultValues();
					sejmiFormLokace.Popis = Fask.Localization.Localization.Vydej3ListPolozek3Zadejcilovoulokaci;
					sejmiFormLokace.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
					sejmiFormLokace.Len = 0;
					sejmiFormLokace.CheckLen = false;
					sejmiFormLokace.AllowEmpty = false;
					sejmiFormLokace.veRow = VERow;
					sejmiFormLokace.viRow = VIRow;
					sejmiFormLokace.ScannerCheckOnly = vydejDataParametry.Parametry[0].CONFIG_LOCNCODE_OVERIT_SCANEREM;
					// TODO: zobrazit tlacitko lokaci ...
					sejmiFormLokace.btnZobrazitAlternativniLokaceVisible = vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() ? false : vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT;

					if (sejmiFormLokace.ShowDialog() == DialogResult.Cancel)
						return null;

					LOC = sejmiFormLokace.Kod;

					return LOC;
				}

			return LOC;
		}

		/// <summary>
		/// Vyzve k vlozeni mnozstvi
		/// </summary>
		/// <param name="mnozstvi">Zde vrati vlozene mnozstvi</param>
		/// <param name="QTYPACK">Mnozstvi v baleni</param>
		/// <returns>-1 pri chybe, 0 pri OK</returns>
		private int sejmiMnozstvi_Prelok(
			ref decimal mnozstvi,
			decimal Nacteno,
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow VERow,
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow VIRow,
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNRow SESNRow,
			ref string rez_2,
			Parsing.Codes.BaseCode code,
			decimal QTY_FromLokZdroj
			)
		{


            while (true)
            {
                int o = 0;
                if (MST_Global.VydejRozsireni1Rezerva2Zadavat)
                {
                    o = sejmiMnozstvi4(ref mnozstvi, Nacteno, VERow, VIRow, SESNRow, ref rez_2, code);
                }
                else
                {
                    o = sejmiMnozstvi3(ref mnozstvi, Nacteno, VERow, VIRow, SESNRow, code);
                }

                if (mnozstvi > QTY_FromLokZdroj)
                {
                    string msg = string.Format(
                        "Upozornění!" +
                        Environment.NewLine +
                        "Zadané množství: {0}" +
                        Environment.NewLine +
                        "je větší než odebrané z lokace: {1}", mnozstvi, QTY_FromLokZdroj);

                    MessageBoxBig.Show(msg, "Upozornění", MessageBoxButtons.OK);
                    continue;
                }
                return o;
            }
		}

		#endregion

		#region Smazani v predlohe pomoci BACK anebo menu smazat


		private void SmazPolozku(Vydej_3.ListPolozekVydej.ListPolozekRow polozkyRow)
		{
			if (MST_Global.Vydej_TypSPrelokovanim && !vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
			{
				SmazPolozku_Prelokovani(polozkyRow);
			}
			else
			{
				SmazPolozku_Default(polozkyRow);
			}
		}



		private void SmazPolozku_Default(Vydej_3.ListPolozekVydej.ListPolozekRow polozkyRow)
		{
			try
			{
				ScannerStop();

				if (polozkyRow == null)
					return;

				if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SmazatNasnimanaDataDotaz, polozkyRow.Nazov, polozkyRow.PocetNasnim.ToString(Settings.UIFormatDesCisel)), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.No
					)
					return;

				// online ulozeni do lokacniho mechanismu
				if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
				{
					#region lokace
					var sidata = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByItemnmbrSopnumbeOrd_SI(polozkyRow.Itemnmbr, polozkyRow.SOPNUMBE, polozkyRow.ORD);
					foreach (var siitem in sidata)
					{
						Cursor.Current = Cursors.WaitCursor;

						//Volani sluzby a kontrola navratveho kodu.
						Logging.Log.WriteAdvanced(string.Empty, string.Empty);
						Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + siitem.guid, "LocationLog");
						// odstraneni online zaznamu
						Fask.MST_W.LokaceService.StatusLokace sl = Vydej.vydejInstance.globalObject.service_lokace.DeleteRecordByGuid(siitem.guid, Fask.MST_W.LokaceService.ModulName.VYDEJ);

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
						Vydej.vydejInstance.globalObject.controller_vydej.DeleteQueryByGuid_SI(siitem.guid);

						Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:V,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.V + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");
						siitem.Delete();
						polozkyRow.Ostava = polozkyRow.Mnozstvo;
					}
					#endregion
				}
				else
				{
					int raff = Vydej.vydejInstance.globalObject.controller_vydej.DeleteByItemnmbrSopnumbeOrd_SI(polozkyRow.Itemnmbr, polozkyRow.SOPNUMBE, polozkyRow.ORD);
					polozkyRow.Ostava = polozkyRow.Mnozstvo;
					//stavVydeje();
				}
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				stavVydeje();
				ScannerStart();
			}
		}

		private void SmazPolozku_Prelokovani(Vydej_3.ListPolozekVydej.ListPolozekRow polozkyRow)
		{
			try
			{
				ScannerStop();

				if (polozkyRow == null)
					return;

				//if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3SmazatNasnimanaDataDotaz, polozkyRow.Nazov, polozkyRow.PocetNasnim.ToString(Settings.UIFormatDesCisel)), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
				//    == DialogResult.No
				//    )
				//    return;

				// online ulozeni do lokacniho mechanismu
				if (!vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
				{
					#region lokace

					var SerNumTrack = Vydej.vydejInstance.globalObject.controller_vydej.Get_CZSerNumTrack_SE(polozkyRow.SOPNUMBE, polozkyRow.Itemnmbr);


					var sidata = Vydej.vydejInstance.globalObject.controller_vydej.GetDataByItemnmbrSopnumbeOrd_SI(polozkyRow.Itemnmbr, polozkyRow.SOPNUMBE, polozkyRow.ORD);
					foreach (var siitem in sidata)
					{

						var itemdesc = Vydej.vydejInstance.globalObject.controller_vydej.Get_ITEMDESC_SE(polozkyRow.SOPNUMBE, polozkyRow.Itemnmbr);
						
						
						string msg = string.Format(
							"Příjmout položku: {0}" + 
							Environment.NewLine + 
							"ID(Kód): {1}({2})" + 
							Environment.NewLine +
							"Množství: {3}" +
							Environment.NewLine +
							"Šarže: {4}" +
							Environment.NewLine
							, itemdesc, siitem.ITEMNMBR, siitem.ITEMCODE, siitem.QTYSHPPD, siitem.SERLTNUM);

						DialogResult dr = MessageBoxBig.Show(msg, "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);

						if (dr == DialogResult.Yes)
						{

							Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dtSE = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable();
							var SErow = dtSE.NewCZMST_SERow();
							SErow.ITEMNMBR = polozkyRow.Itemnmbr;
							SErow.ITEMDESC = polozkyRow.Nazov.Trim();
							SErow.VNDITNUM = (polozkyRow.IsVNDITNUMNull() ? string.Empty : polozkyRow.VNDITNUM.Trim());
							SErow.QTYPACK = polozkyRow.QTYPACK;
							SErow.CZ_CarKod = polozkyRow.CZ_CarKod.Trim();
							SErow.SOPNUMBE = polozkyRow.SOPNUMBE.Trim();
							SErow.ORD = polozkyRow.ORD;
							SErow.Note = polozkyRow.Note.Trim();
							SErow.CZ_SerNum_Track = SerNumTrack;

							SErow.QTYSHPPD = siitem.QTYSHPPD;

							sejmiFormLokace.SetDefaultValues();
							sejmiFormLokace.Popis = Fask.Localization.Localization.Vydej3ListPolozek3Zadejcilovoulokaci;
							sejmiFormLokace.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
							sejmiFormLokace.Len = 0;
							sejmiFormLokace.CheckLen = false;
							sejmiFormLokace.AllowEmpty = false;
							sejmiFormLokace.veRow = SErow;
							sejmiFormLokace.viRow = siitem;
							sejmiFormLokace.ScannerCheckOnly = vydejDataParametry.Parametry[0].CONFIG_LOCNCODE_OVERIT_SCANEREM;
							// TODO: zobrazit tlacitko lokaci ...
							sejmiFormLokace.btnZobrazitAlternativniLokaceVisible = vydejDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() ? false : vydejDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT;

							if (sejmiFormLokace.ShowDialog() == DialogResult.Cancel)
								break;

							string LOC = sejmiFormLokace.Kod;

							var stav = Prijem(
								siitem.ITEMNMBR,
								string.Empty,
								siitem.SOPNUMBE,
								siitem.QTYSHPPD,
								siitem.SERLTNUM,
								siitem.SKL_ID,
								LOC,
								Guid.NewGuid(),
								null,
								siitem.CountEntries
								);

							if (!stav)
								break;

							Vydej.vydejInstance.globalObject.controller_vydej.DeleteQueryByGuid_SI(siitem.guid);

							siitem.Delete();
							polozkyRow.Ostava = polozkyRow.Mnozstvo;
						}
						else
						{
							break;
						}

					}
					#endregion
				}
				else
				{
					int raff = Vydej.vydejInstance.globalObject.controller_vydej.DeleteByItemnmbrSopnumbeOrd_SI(polozkyRow.Itemnmbr, polozkyRow.SOPNUMBE, polozkyRow.ORD);
					polozkyRow.Ostava = polozkyRow.Mnozstvo;
					//stavVydeje();
				}
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				stavVydeje();
				ScannerStart();
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


		#endregion
	}
}
