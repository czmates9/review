using System.Text;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.Collections.Generic;
using Fask.ScannerProvider;
using Fask.Graphic;
using Fask.MST_W.Classes;
using Fask.Parsing.Codes.Interfaces;

namespace Fask.MST_W.Inventura1_sqlce
{
	public partial class ListPolozky_sqlce
	{
        /// <summary>
        /// Posledni pouzita expirace
        /// </summary>
        DateTime expiraceLast = DateTime.Now.AddDays(30); // TODO : konfiguracne delku expirace?

		private string locncode = string.Empty;

		/// <summary>
		/// Najde polozku a oznaci ji jako aktivni v datagridu
		/// </summary>
		/// <param name="carkod">carovy kod polozky</param>
		/// <param name="lastindex">posledni nalezeny index</param>
		/// <returns>index nalezene polozky, vetsi nez posledni nalezeny index</returns>
		private bool NajdiPolozku(
			string ck,
			out ListPolozky.PolozkyRow prow,
			out Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row
			)
		{
			prow = null;
			i3row = null;

			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ScannerStop();

				//1) najit polozky
				_inventura1.CZMST_I3.Clear();
				//if ((bss is Fask.Parsing.Codes.Interfaces.ICodeBarcode))
				//{
				//    Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByCarcode_I3(_inventura1.CZMST_I3, ((Fask.Parsing.Codes.Interfaces.ICodeBarcode)bss).Barcode); //Dotaz sestaven pomoci T-SQL UNION
				//}
				//else
				//{
				//    Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByCarcode_I3(_inventura1.CZMST_I3, carkod); //Dotaz sestaven pomoci T-SQL UNION
				//}
				
				Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByCarcode_I3(_inventura1.CZMST_I3, ck); //Dotaz sestaven pomoci T-SQL UNION
				

				if (_inventura1.CZMST_I3.Count == 0)
				{ // polozky nenalezeny, hledam dle SN ...
					//if ((bss is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr))
					//{
					//    Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillBySERLNMBR_I2(_inventura1.CZMST_I2, ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)bss).Serltnmbr);
					//}
					//else
					//{
					//    Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillBySERLNMBR_I2(_inventura1.CZMST_I2, carkod);
					//}
					
					Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillBySERLNMBR_I2(_inventura1.CZMST_I2, ck);
					

					//ita_i3.ClearBeforeFill = false;
					foreach (var item in _inventura1.CZMST_I2)
					{
						Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.FillByITEMNMBR_I3(_inventura1.CZMST_I3, item.ITEMNMBR);
					}
				}

				if (_inventura1.CZMST_I3.Rows.Count < 1) //nenalezeno
				{
					Cursor.Current = Cursors.Default;
					MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaCarKodNenalezena, ck), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					return false;
				}
				else if (_inventura1.CZMST_I3.Rows.Count > 1) //nalezeno vice zaznamu
				{
					var xi1dt = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I1(_inventura1.CZMST_I3[0].ITEMNMBR);
					Cursor.Current = Cursors.Default;
					using (ListPolozkyI3_sqlce lpi3 = new ListPolozkyI3_sqlce())
					{
						lpi3.I3DT = _inventura1.CZMST_I3;
						if (xi1dt.Count > 0) lpi3.FindMJInView(xi1dt[0].ITEMNMBR, xi1dt[0].DMJ);
						if (lpi3.ShowDialog() == DialogResult.Cancel)
							return false;
						else
							i3row = lpi3.I3Selected;
					}
				}
				else //je pouze jedna (0 byt uz nemuze)
				{
					i3row = _inventura1.CZMST_I3[0];
				}

				_currentItem = -1;
				CreateResultSet(_select_All, " where ITEMNMBR='" + i3row.ITEMNMBR + "'");

				//Dohledat v i1 vybrany zaznam
				prow = this.SelectedRow;

				if (prow == null) //neco se nepovedlo
				{
					Cursor.Current = Cursors.Default;
					MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaNenalezena, i3row.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
					return false;
				}

				UpdateForm();

				if (_listPolozky.Polozky.Count > 1)
				{
					Cursor.Current = Cursors.Default;
					MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNalezenoVicePolozekVyberDotaz, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Logging.Log.Write(ex.Message, this.Text);
				return false;
			}
			finally
			{
				ScannerStart();
				Cursor.Current = Cursors.Default;
			}
		}


		private void VyplnPolozku(ListPolozky.PolozkyRow prow, Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row, Fask.Parsing.Codes.BaseCode code)
		{

			#region Kontrola,zda je mozne zadavat i jinak nez scannerem
			
			if (!InputModeChecker.checkInputMode(MST_Global.Inventura1PolozkyVyberJenScannerem, _input_mode))
			{
				MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
				return;
			} 

			#endregion

			bool lokaceZadana = false;

			try
			{
				ScannerStop();

				#region Parametry
				
				decimal qty = 0;
				string sn = string.Empty;
                DateTime? expirace = null;  // vychozi nastaveni expirace neni ...
				#endregion

				#region Vytaženi SN z parsovaneho kodu

				if (prow.CZ_SERNUM_TRACK == 1 && (code is Fask.Parsing.Codes.Interfaces.ICodeSerialNumber) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN)))
					sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;

				if (prow.CZ_SERNUM_TRACK == 2 && (code is Fask.Parsing.Codes.Interfaces.ICodeSarze) && (!string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze)))
					sn = ((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;

				#endregion

                #region Vytazeni expirace z objektu CODE
                if ((code is ICodeExpiration) && (((ICodeExpiration)code).Expiration.HasValue))
                    expirace = ((ICodeExpiration)code).Expiration;

                #endregion


				#region inventura1ZadaniLocncodePamatovatPosledni anebo inventura1ZadaniLocncodeJednou

				if (MST_Global.inventura1ZadaniLocncodePamatovatPosledni)
				{
					string locncodeNew = prow.IsLOCNCODENull() ? string.Empty : prow.LOCNCODE.Trim();
					if (!String.IsNullOrEmpty(locncodeNew))
					{
						locncode = locncodeNew;
					}
				}
				else if (!MST_Global.inventura1ZadaniLocncodeJednou)
				{
					locncode = prow.IsLOCNCODENull() ? string.Empty : prow.LOCNCODE.Trim();
				}
 
				#endregion

				#region CFG_UpozornitNaPrebytek

				if (parrow != null && parrow.CFG_UpozornitNaPrebytek)
				{//cfg_upozornit na prebytek...
					//decimal nas = Convert.ToDecimal(ita_i4.Nasnimano(prow.ITEMNMBR) ?? 0);
					if (prow.NASNIMANO > prow.QUANTITY)
					{
						MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezNaSklade, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
					}
				} 

				#endregion

				#region Cyklus vypneni položky

				bool DalsiSN = true;
				while (DalsiSN)
				{
					#region Inventura1PolozkaNasnimatPouzeJednou

					if (MST_Global.Inventura1PolozkaNasnimatPouzeJednou)
					{
						int? pocetNasnimano = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.NasnimanoPocet_I4(prow.ITEMNMBR.Trim());
						if ((pocetNasnimano ?? 0) > 0)
						{
							MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaJizBylaZadana, Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozeniPolozky, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							return;
						}
					}

					#endregion

					#region CFG_PovolitZmenuLokace

					if (parrow != null && parrow.CFG_PovolitZmenuLokace)
					{
						if (MST_Global.inventura1ZadaniLocncodePredSN)
						{
							bool zadat_lokaci = true;
							if (MST_Global.inventura1ZadaniLocncodeJednou)
								zadat_lokaci = !lokaceZadana;

							if (zadat_lokaci)
							{
								using (SejmiKodFormHledejNazev skf = new SejmiKodFormHledejNazev(Fask.Localization.Localization.Inventura1ListPolozkySqlceLokace, SejmiKodFormHledejNazev.TypeOfCode.AlphaNumeric, 0, false, false, prow.LOCNCODE, _inventura1.CZMST_I4.Columns["LOCNCODE"].MaxLength))
								{
									skf.Kod = locncode;
									if (skf.ShowDialog() == DialogResult.Cancel)
										break;
									locncode = skf.Kod;
								}
								lokaceZadana = true;
							}
						}
					}

					#endregion

					#region CZ_SERNUM_TRACK == 0

					if (prow.CZ_SERNUM_TRACK == 0) //sledovano na mnozstvi
					{
						decimal qtytmp;
						int status = GetMnozstvi(out qtytmp, code, i3row, prow);

						if (status == 1)
							continue;
						else if (status == 2)
							break;
						else if (status == 3)
							return;

						qty = qtytmp;

						#region Puvodny kod zadani mnozstvi

						//if ((code is Fask.Parsing.Codes.Interfaces.ICodeWeight) && ((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight.HasValue)
						//{
						//    decimal weight = ((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight.Value;

						//    qty =
						//        (weight
						//        / (i3row.IsWEIGHTNull() || (i3row.WEIGHT == 0) ? 1 : i3row.WEIGHT)
						//        / (i3row.IsQTYPACKNull() || (i3row.QTYPACK == 0) ? 1 : i3row.QTYPACK)
						//        );
						//}
						//if ((code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
						//{
						//    qty = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
						//}
						//else
						//{
						//    NaplnPolozku_sqlce naplnp = this.NaplnPolozkuForm;
						//    bool baleni = i3row.QTYPACK > 0;
						//    naplnp.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
						//    naplnp.PROW = prow;
						//    naplnp.I3Row = i3row;
						//    naplnp.Owner = this;
						//    //naplnp.Popis = "Množství" + (baleni ? " balení" : string.Empty);
						//    naplnp.Popis = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstvi;
						//    naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
						//    naplnp.AllowEmpty = false;
						//    //naplnp.Text = "Vložte množství" + (baleni ? " balení" : string.Empty);
						//    naplnp.Text = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstvi;
						//    naplnp.Kod = string.Empty;
						//    naplnp.ScannerOff = false;
						//    if (parrow != null)
						//    {
						//        if (!parrow.CFG_MnozstviScannerem)
						//        {
						//            naplnp.ScannerOff = true;
						//        }
						//        if (parrow.CFG_PredvyplnitMnozstvi)
						//        {
						//            if (parrow.CFG_PredvyplnitMnozstviOJedna)
						//                naplnp.Kod = "1";
						//            else if (parrow.CFG_PredvyplnitMnozstviZbyvajici)
						//                naplnp.Kod = ((prow.QUANTITY - prow.NASNIMANO) / (i3row.QTYPACK > 0 ? i3row.QTYPACK : 1)).ToString(Settings.UIFormatDesCisel);
						//        }
						//    }

						//    if (naplnp.ShowDialog() == DialogResult.Cancel)
						//    {
						//        if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
						//        {
						//            if (prow.NASNIMANO < prow.QUANTITY)
						//            {
						//                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniKompletniUkoncitDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
						//                    continue;
						//                else
						//                    break;
						//            }
						//        }
						//        return;
						//    }

						//    if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
						//    {
						//        if (prow.NASNIMANO + Convert.ToDecimal(naplnp.Kod) > prow.QUANTITY)
						//        {
						//            if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezZadanePokracovatDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
						//            {
						//                continue;
						//            }
						//        }
						//    }

						//    if (parrow != null)
						//    {
						//        if (!parrow.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
						//        {
						//            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytVetsiNez0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						//            continue;
						//        }
						//        else
						//            qty = decimal.Parse(naplnp.Kod);
						//    }
						//    else
						//    {
						//        if (Convert.ToDecimal(naplnp.Kod) == 0)
						//        {
						//            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytRuzneOd0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						//            continue;
						//        }
						//        else
						//            qty = decimal.Parse(naplnp.Kod);
						//    }
						//}

					#endregion
					}

					#endregion

					#region CZ_SerNum_Track == 1 OR CZ_SerNum_Track == 2

					else if (prow.CZ_SERNUM_TRACK == 1 || prow.CZ_SERNUM_TRACK == 2) //sledovano na seriova cisla
					{
						bool itemFoundedSN = false;
						do
						{
							if ((prow.CZ_SERNUM_TRACK == 2) && (code is Fask.Parsing.Codes.Interfaces.ICodeSarze) && !string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze))
							{
								sn = ((Fask.Parsing.Codes.Interfaces.ICodeSarze)code).Sarze;
								
								//8.6.2020 pořadavek od ZdD že to na kontrolovatr i po načteni pomoci parsovaneho kodu
								//má to byt tady< jak to ma byt?
								// konfig Parametr
								if (MST_Global.Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit)
								{
									List<string> tmp = new List<string>();
									var rows = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I2(i3row.ITEMNMBR);

									foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2Row row in rows)
									{
										if (row.SERLNMBR.Trim() == sn.Trim())
										{
											tmp.Add(sn);
										}
									}

									if (tmp.Count == 0)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1NaplnPolozkuSNKodNeniVPredlozeUlozitDotaz, sn.Trim()), Fask.Localization.Localization.Inventura1NaplnPolozkuSNDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red) == DialogResult.No)
										{
											return;
										}
									}
								}
							}
							else if ((prow.CZ_SERNUM_TRACK == 1) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerialNumber) && !string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN))
							{

								//22.6.2021 požadavek od ZdD pridat tohle i na Seriove čísla

								sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerialNumber)code).SN;

								//8.6.2020 pořadavek od ZdD že to na kontrolovatr i po načteni pomoci parsovaneho kodu
								//má to byt tady< jak to ma byt?
								// konfig Parametr
								if (MST_Global.Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit)
								{
									List<string> tmp = new List<string>();
									var rows = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I2(i3row.ITEMNMBR);

									foreach (Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2Row row in rows)
									{
										if (row.SERLNMBR.Trim() == sn.Trim())
										{
											tmp.Add(sn);
										}
									}

									if (tmp.Count == 0)
									{
										if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1NaplnPolozkuSNKodNeniVPredlozeUlozitDotaz, sn.Trim()), Fask.Localization.Localization.Inventura1NaplnPolozkuSNDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red) == DialogResult.No)
										{
											return;
										}
									}
								}
							}
							else
							{
								string sn_sarze = string.Empty;

								if (prow.CZ_SERNUM_TRACK == 1)
									sn_sarze = MST_Global.SNName;
								else if (prow.CZ_SERNUM_TRACK == 2)
									sn_sarze = "šarže";

								

								NaplnPolozkuSN naplnpsn = this.NaplnPolozkuSNForm;
								naplnpsn.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
								naplnpsn.PROW = prow;
								naplnpsn.I3Row = i3row;
								naplnpsn.Owner = this;
								//naplnpsn.Popis = MST_Global.SNName;
								naplnpsn.Popis = sn_sarze;
								naplnpsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
								naplnpsn.AllowEmpty = false;
								//naplnpsn.Text = "Vložte " + MST_Global.SNName;
								//naplnpsn.Text = string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteSN, MST_Global.SNName);
								naplnpsn.Text = string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteSN, sn_sarze);
								naplnpsn.Kod = string.Empty;
								naplnpsn.ScannerOff = false;
								//naplnpsn.I2 = ita_i2.GetDataByITEMNMBR(i3row.ITEMNMBR);
								//naplnpsn.I2 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CZMST_I2_GetDataByItemnmbr(i3row.ITEMNMBR);
								naplnpsn.I2 = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.GetDataByITEMNMBR_I2(i3row.ITEMNMBR);
								if (naplnpsn.ShowDialog() == DialogResult.Cancel)
									return;

								sn = naplnpsn.Kod;
							}

							qty = 1;

							if (parrow != null && !parrow.CFG_PovolitDuplicituSN)
							{
								if (prow.CZ_SERNUM_TRACK == 1)
								{
									bool itemFounded = false;


									itemFounded = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.dataExistsInI4_ByITEMNMBR_SERLNMBR(prow.ITEMNMBR, sn);

									if (itemFounded)
									{
										MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Inventura1ListPolozkySqlceChyba, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
										return;
									} 
								}
							}

							itemFoundedSN = false;
							if (prow.CZ_SERNUM_FIND > 0)
							{
								itemFoundedSN = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.dataExistsInI2_ByITEMNMBR_SERLNMBR(prow.ITEMNMBR, sn);
								if (itemFoundedSN)
									break;
								else
								{
									MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceSNNenalezenoOpokovat, MST_Global.SNName, sn), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
									continue;
								}
							}

							break;

						} while (true);

						if (prow.CZ_SERNUM_TRACK == 2)//sledovano na sarzi a mnozstvi
						{

							decimal qtytmp;
							int status = GetMnozstvi(out qtytmp, code, i3row, prow);

							if (status == 1)
								continue;
							else if (status == 2)
								break;
							else if (status == 3)
								return;

							qty = qtytmp;

							#region Puvodny kod zadani mnozstvi
							//if ((code is Fask.Parsing.Codes.Interfaces.ICodeWeight) && ((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight.HasValue)
							//{
							//    decimal weight = ((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight.Value;

							//    qty =
							//        (weight
							//        / (i3row.IsWEIGHTNull() || (i3row.WEIGHT == 0) ? 1 : i3row.WEIGHT)
							//        / (i3row.IsQTYPACKNull() || (i3row.QTYPACK == 0) ? 1 : i3row.QTYPACK)
							//        );
							//}
							//if ((code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
							//{
							//    qty = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
							//}
							//else
							//{
							//    #region naplneni mnozstvi

							//    NaplnPolozku_sqlce naplnp = this.NaplnPolozkuForm;
							//    bool baleni = i3row.QTYPACK > 0;
							//    naplnp.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
							//    naplnp.PROW = prow;
							//    naplnp.I3Row = i3row;
							//    naplnp.Owner = this;
							//    //naplnp.Popis = "Množství" + (baleni ? " balení" : string.Empty);
							//    naplnp.Popis = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstvi;
							//    naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
							//    naplnp.AllowEmpty = false;
							//    //naplnp.Text = "Vložte množství" + (baleni ? " balení" : string.Empty);
							//    naplnp.Text = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstvi;

							//    naplnp.Kod = string.Empty;
							//    // TODO : jak to udelat s mnozstvim SN?
							//    //if (itemFoundedSN)
							//    //{
							//    //    decimal jednotky = (decimal)polozkyReaderSN["QTY"];
							//    //    naplnp.Kod = baleni ?  .ToString(Settings.UIFormatDesCisel);
							//    //}

							//    naplnp.ScannerOff = false;
							//    if (parrow != null)
							//    {
							//        if (!parrow.CFG_MnozstviScannerem)
							//        {
							//            naplnp.ScannerOff = true;
							//        }
							//        if (parrow.CFG_PredvyplnitMnozstvi)
							//        {
							//            if (parrow.CFG_PredvyplnitMnozstviOJedna)
							//                naplnp.Kod = "1";
							//            else if (parrow.CFG_PredvyplnitMnozstviZbyvajici)
							//                //naplnp.Kod = (prow.QUANTITY - prow.NASNIMANO).ToString(Settings.UIFormatDesCisel);
							//                naplnp.Kod = ((prow.QUANTITY - prow.NASNIMANO) / (i3row.QTYPACK > 0 ? i3row.QTYPACK : 1)).ToString(Settings.UIFormatDesCisel);
							//        }
							//    }

							//    if (naplnp.ShowDialog() == DialogResult.Cancel)
							//    {
							//        if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
							//        {
							//            if (prow.NASNIMANO < prow.QUANTITY)
							//            {
							//                if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniKompletniUkoncitDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
							//                {
							//                    continue;
							//                }
							//                else
							//                {
							//                    break;
							//                }
							//            }
							//        }

							//        return;
							//    }

							//    if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
							//    {
							//        if (prow.NASNIMANO + Convert.ToDecimal(naplnp.Kod) > prow.QUANTITY)
							//        {
							//            if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezZadanePokracovatDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
							//            {
							//                continue;
							//            }
							//        }
							//    }

							//    if (parrow != null)
							//    {
							//        if (!parrow.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
							//        {
							//            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytVetsiNez0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							//            continue;
							//        }
							//        else
							//            qty = decimal.Parse(naplnp.Kod);
							//    }
							//    else
							//    {
							//        if (Convert.ToDecimal(naplnp.Kod) == 0)
							//        {
							//            MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytRuzneOd0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
							//            continue;
							//        }
							//        else
							//            qty = decimal.Parse(naplnp.Kod);
							//    }
							//    #endregion
							//}
							#endregion
						}
					}

					#endregion

					#region Neznamy CZ_SerNum_Track

					else
					{
						MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Inventura1ListPolozkySqlceSerNumTrackNeniPodporovan, prow.CZ_SERNUM_TRACK), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
						return;
					}

					#endregion


					decimal mnozstviMJ = qty;
					decimal mnozstvi = i3row.QTYPACK > 0 ? qty * i3row.QTYPACK : qty;

					#region CFG_PovolitZmenuLokace

					if (parrow != null && parrow.CFG_PovolitZmenuLokace)
					{
						if (!MST_Global.inventura1ZadaniLocncodePredSN)
						{
							bool zadat_lokaci = true;
							if (MST_Global.inventura1ZadaniLocncodeJednou)
								zadat_lokaci = !lokaceZadana;

							if (zadat_lokaci)
							{
								using (SejmiKodFormHledejNazev skf = new SejmiKodFormHledejNazev(Fask.Localization.Localization.Inventura1ListPolozkySqlceLokace, SejmiKodFormHledejNazev.TypeOfCode.AlphaNumeric, 0, false, false, prow.LOCNCODE, _inventura1.CZMST_I4.Columns["LOCNCODE"].MaxLength))
								{
									skf.Kod = locncode;
									if (skf.ShowDialog() == DialogResult.Cancel)
										break;
									locncode = skf.Kod;
								}
								lokaceZadana = true;
							}
						}
					}

					#endregion

					#region REZ hodnoty

					string rez_1 = prow.REZ1.Trim();
					if (prow.CZ_REZ_1_TRACK > 0)
					{ // TODO : zadani hodnoty rez1 ...
						using (SejmiKodForm skf = new SejmiKodForm("Dop. informace 1", MST_Global.Inventura1REZ1Nazev, MST_Global.Inventura1REZ1IsNumber ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, !MST_Global.Inventura1REZ1Mandatory, prow.REZ1, 0))
						{
							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							else
								rez_1 = skf.Kod.Trim();
						}
					}

					string rez_2 = prow.REZ2.Trim();
					if (prow.CZ_REZ_2_TRACK > 0)
					{ // TODO : zadani hodnotay rez2
						using (SejmiKodForm skf = new SejmiKodForm("Dop. informace 2", MST_Global.Inventura1REZ2Nazev, MST_Global.Inventura1REZ2IsNumber ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, !MST_Global.Inventura1REZ2Mandatory, prow.REZ2, 0))
						{
							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							else
								rez_2 = skf.Kod.Trim();
						}
					}

					#endregion

                    #region Expirace
                    //POUŽITELNOST DO
                    //17 použitelnost do (RRMMDD) n2+n6
                    //Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
                    //či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
                    // => parametr prijem_pi.cz_expirace_track > 0 => vyzadovat zadani expirace, jinak bez expirace (expirace = null)
                    if (prow.CZ_Expirace_Track > 0)
                    {
                        if (!expirace.HasValue)
                        {
                            #region Expirace zadani
                            expirace = expiraceLast;
                            // 22.7.2020 JiS : dle pozadavku ZdD predvyplnena prazdna hodnota
                            //string expirationStr = expiraceLast.ToString(Main.dateFormatRRMMDD);
                            string expirationStr = string.Empty;
                            while (true)
                            {
                                var dResExpiration = InputBoxExpirace.Show("Expirace (RRMMDD)", expirationStr, out expirationStr, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                                if (dResExpiration == DialogResult.Cancel)
                                    return;

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

                                #region Overeni expirace online ...
                                //// overeni zadane expirace
                                //var StatusExpiration = Communication.PrijemServisComunicator.OnlineOverExpiraci(PIRow.ITEMNMBR, PIRow.SERLTNUM, expirace.Value);
                                //if (StatusExpiration == null)
                                //{
                                //    var drChybaJakDal = MessageBoxBig.Show("Pokračovat bez ověření?", "Ověření Expirace", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                                //    if (drChybaJakDal == DialogResult.No)
                                //        continue;
                                //    else //if (drChybaJakDal == DialogResult.Yes)
                                //        break;
                                //}
                                //else
                                //{
                                //    switch (StatusExpiration.State)
                                //    {
                                //        case Fask.MST_W.PrijemService.StatusOverExpiraceState.ERROR:
                                //            // chyba, nelze pokracovat
                                //            MessageBoxBig.Show(StatusExpiration.Message, "Ověření Expirace", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                                //            continue;
                                //        //break;
                                //        case Fask.MST_W.PrijemService.StatusOverExpiraceState.WARNING:
                                //            var drExpiraceWarning = MessageBoxBig.Show(StatusExpiration.Message, "Ověření Expirace", MessageBoxButtons.AbortRetryIgnore, MessageBoxBigIcon.Warning);
                                //            if (drExpiraceWarning == DialogResult.Abort)
                                //                return;
                                //            else if (drExpiraceWarning == DialogResult.Retry)
                                //                continue;
                                //            else //if (drExpiraceWarning == DialogResult.Ignore)
                                //                break;
                                //        case Fask.MST_W.PrijemService.StatusOverExpiraceState.OK:
                                //        default:
                                //            // vse v poradku, pouzit a ukonci
                                //            break;
                                //    }
                                //}
                                #endregion

                                // pokud az tady, tak koncim a nastavim posledni pouzitou expiraci
                                expiraceLast = expirace.Value;
                                break;
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region Vlozeni zaznamu na vystup a aktualizace nasnimaneho mnozstvi polozky

                    bool o_checked = false;
					if (!StaticMethods.OnlineCheck(Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value, prow.ITEMNMBR, ref o_checked))
						return;

					try
					{
						Cursor.Current = Cursors.WaitCursor;

						var dti4 = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable();

						var i4n = dti4.NewCZMST_I4Row();


						i4n.CountEntries = Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.Davka.Value;
						i4n.CZ_CarKod = i3row.IsCZ_CarKodNull() ? string.Empty : i3row.CZ_CarKod;
						i4n.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
						i4n.DEX_ROW_ID = i3row.IsDEX_ROW_IDNull() ? -1 : i3row.DEX_ROW_ID;
						i4n.GUID = Guid.NewGuid();
						i4n.ID_TERMINAL = MST_Global.TerminalID;
						i4n.INPUT_MODE = _input_mode;
						i4n.ITEMCODE = prow.IsITEMCODENull() ? string.Empty : prow.ITEMCODE.Trim();
						i4n.ITEMNMBR = prow.IsITEMNMBRNull() ? string.Empty : prow.ITEMNMBR.Trim();
						i4n.LOCNCODE = String.IsNullOrEmpty(locncode) ? string.Empty : locncode.Trim();
						i4n.MJ = i3row.IsMJNull() ? string.Empty : i3row.MJ.Trim();
						i4n.O_Checked = o_checked;
						i4n.QTYPACK = i3row.IsQTYPACKNull() ? 0 : i3row.QTYPACK;
						i4n.QUANTITY = mnozstvi;
						i4n.QUANTITYMJ = mnozstviMJ;
						i4n.REZ_1 = String.IsNullOrEmpty(rez_1) ? string.Empty : rez_1.Trim();
						i4n.REZ_2 = String.IsNullOrEmpty(rez_2) ? string.Empty : rez_2.Trim();
						i4n.SERLNMBR = String.IsNullOrEmpty(sn) ? string.Empty : sn.Trim();
						i4n.skl_id = prow.IsSkladIDNull() ? string.Empty : prow.SkladID.Trim();
						i4n.TIMEDONE = DateTime.Now.ToString("HHmmss");
						i4n.USERID = MST_Global.UserID;
						i4n.VNDITNUM = i3row.IsVNDITNUMNull() ? string.Empty : i3row.VNDITNUM.Trim();
						if (!i3row.IsWEIGHTNull()) i4n.WEIGHT = i3row.WEIGHT;
                        if (expirace.HasValue) i4n.Expirace = expirace.Value;

						dti4.AddCZMST_I4Row(i4n);

						System.Diagnostics.Debug.Assert(i4n.RowState == DataRowState.Added);
						//Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.CZMST_I4_Update(i4n);
						Inventura1_sqlce.Inventura1_sqlce_Instance.globalObject.controller_inventura1.Update_I4(i4n);

						prow.NASNIMANO += mnozstvi;
					}
					finally
					{
						Cursor.Current = Cursors.Default;
					}
					#endregion


					DalsiSN = parrow != null && !parrow.CFG_PoZadaniSNZpetNaMN;

					#region Kontrola uplnosti polozky

					if (DalsiSN && (parrow != null && parrow.CFG_KontrolaUplnostiPolozky))
					{
						if (prow.NASNIMANO >= prow.QUANTITY)
						{
							//if (MessageBoxBig.Show("Položka je kompletní, chcete pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
							if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlcePolozkaDlePredlohyKompletniPreplnitDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
							{
								break;
							}
						}
					}

					#endregion
				} 

				#endregion

				#region Kontrola uplnosti davky

				if (parrow != null && parrow.CFG_KontrolaUplnosti)
				{
					bool davkaUplna = false;
					try
					{
						Cursor.Current = Cursors.WaitCursor;

						// TODO : kontrola uplnosti na velkem mnozstvi dat selhava (viz. Inventura1_sqlce.stavInventury())
						//davkaUplna = kontrolaUplnostiDavky();
					}
					finally { Cursor.Current = Cursors.Default; }

					if (davkaUplna)
					{
						if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceDavkaKompletniPokracovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.No)
						{
							this.PerformKonec(false);
						}
					}
				}

				#endregion

			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message);
			}
			finally
			{
				ScannerStart();
			}

			UpdateForm();

		}

		private int GetMnozstvi(out decimal qty, 
			Fask.Parsing.Codes.BaseCode code, 
			Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3Row i3row,
			ListPolozky.PolozkyRow prow
			) 
		{
			qty = 0;

			if (!MST_Global.Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu && (code is ICodeWeight) && ((ICodeWeight)code).Weight.HasValue)
			{
				decimal weight = ((ICodeWeight)code).Weight.Value;

				qty =
					(weight
					/ (i3row.IsWEIGHTNull() || (i3row.WEIGHT == 0) ? 1 : i3row.WEIGHT)
					/ (i3row.IsQTYPACKNull() || (i3row.QTYPACK == 0) ? 1 : i3row.QTYPACK)
					);
			}
			else if (!MST_Global.Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu && (code is ICodeQuantity) && ((ICodeQuantity)code).Quantity.HasValue)
			{
				qty = ((ICodeQuantity)code).Quantity.Value;
			}
			else
			{
				NaplnPolozku_sqlce naplnp = this.NaplnPolozkuForm;
				bool baleni = i3row.QTYPACK > 0;
				naplnp.ZobrazMnozstviNaSklade = parrow != null ? parrow.CFG_PovolitZobrazeniMnozstviNaSklade : true;
				naplnp.PROW = prow;
				naplnp.I3Row = i3row;
				naplnp.Owner = this;
				//naplnp.Popis = "Množství" + (baleni ? " balení" : string.Empty);
				naplnp.Popis = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstvi;
				naplnp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
				naplnp.AllowEmpty = false;
				//naplnp.Text = "Vložte množství" + (baleni ? " balení" : string.Empty);
				naplnp.Text = baleni ? Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstviBaleni : Fask.Localization.Localization.Inventura1ListPolozkySqlceVlozteMnozstvi;
				naplnp.Kod = string.Empty;
				naplnp.ScannerOff = false;
				if (parrow != null)
				{
					if (!parrow.CFG_MnozstviScannerem)
					{
						naplnp.ScannerOff = true;
					}

					if (parrow.CFG_PredvyplnitMnozstvi)
					{
						if (parrow.CFG_PredvyplnitMnozstviOJedna)
							naplnp.Kod = "1";
						else if (parrow.CFG_PredvyplnitMnozstviZbyvajici)
							naplnp.Kod = ((prow.QUANTITY - prow.NASNIMANO) / (i3row.QTYPACK > 0 ? i3row.QTYPACK : 1)).ToString(Settings.UIFormatDesCisel);
					}

					if ((code is ICodeWeight) && ((ICodeWeight)code).Weight.HasValue)
					{
						decimal weight = ((ICodeWeight)code).Weight.Value;

						naplnp.Kod =
							(weight
							/ (i3row.IsWEIGHTNull() || (i3row.WEIGHT == 0) ? 1 : i3row.WEIGHT)
							/ (i3row.IsQTYPACKNull() || (i3row.QTYPACK == 0) ? 1 : i3row.QTYPACK)
							).ToString(Settings.UIFormatDesCisel);
				
					}

					if ((code is ICodeQuantity) && ((ICodeQuantity)code).Quantity.HasValue)
					{
						naplnp.Kod = ((ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);
					}	
				}

				if (naplnp.ShowDialog() == DialogResult.Cancel)
				{
					if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
					{
						if (prow.NASNIMANO < prow.QUANTITY)
						{
							if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceNeniKompletniUkoncitDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
								return 1;
							else
								return 2;
						}
					}
					return 3;
				}

				if (parrow != null && parrow.CFG_KontrolaUplnostiPolozky)
				{
					if (prow.NASNIMANO + Convert.ToDecimal(naplnp.Kod) > prow.QUANTITY)
					{
						if (MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviVetsiNezZadanePokracovatDotaz, "Warning", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning) == DialogResult.No)
						{
							return 1;
						}
					}
				}

				if (parrow != null)
				{
					if (!parrow.CFG_PovolitZaporneMnozstvi && Convert.ToDecimal(naplnp.Kod) <= 0)
					{
						MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytVetsiNez0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return 1;
					}
					else
						qty = decimal.Parse(naplnp.Kod);
				}
				else
				{
					if (Convert.ToDecimal(naplnp.Kod) == 0)
					{
						MessageBoxBig.Show(Fask.Localization.Localization.Inventura1ListPolozkySqlceMnozstviMusiBytRuzneOd0, Fask.Localization.Localization.Inventura1ListPolozkySqlceInfo, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return 1;
					}
					else
						qty = decimal.Parse(naplnp.Kod);
				}
			}

			return 0;
		}
	}
}
