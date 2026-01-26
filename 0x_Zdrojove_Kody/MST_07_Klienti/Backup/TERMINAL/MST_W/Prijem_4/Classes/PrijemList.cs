using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Fask.Graphic;
using Fask.MST_W.Classes;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;
using Fask.Parsing.Codes;
using Fask.ScannerProvider;
using Fask.Parsing.Codes.Interfaces;

namespace Fask.MST_W.Prijem_4
{
	public partial class PrijemList
	{
		#region Vkladaci logika

		/// <summary>
		/// Metod pro vložení nasnimaneho zaznamu
		/// </summary>
		/// <param name="PERow">řadek z předlohy</param>
		private void PerformInsert(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow)
		{
			PerformInsert(PERow, null);
		}

		/// <summary>
		/// Metod pro vložení nasnimaneho zaznamu
		/// </summary>
		/// <param name="PERow"></param>
		/// <param name="code"></param>
		private void PerformInsert(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow, BaseCode code)
		{

			skf = new SejmiKodForm();
			pzl = new PrijemZadejLokaci();
			ppp = new PrijemPridatPolozku(PERow);
			pppsn = new PrijemPridatPolozkuSN(PERow);

			try
			{
				#region Kontrola, zda se nema povolit jen scanner

				if (!InputModeChecker.checkInputMode(Prijem_4.Globals.PolozkyVyberJenScannerem, _input_mode))
				{
					MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPolozkuJdeZadatPouzeSejmutimCK, Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
					return;
				}

				#endregion

				System.Guid newguid = System.Guid.Empty;

				try
				{
					ScannerStop();

					//Jestlize neni vybrany, pak prisel ze scanneru a da se na vyber
					if (PERow == null)
					{
						return;
					}

					#region Online kontrola Hmotnosti

					OnlineCheckHmotnost(PERow);

					#endregion

					#region Kontrola uplnosti polozky
					// dohledani poctu, pokud je predloha, existuje konf.soubor a je nastaven
					// odpovidajici parametr
					if (prijemDataParametry.Parametry[0].CONFIG_POKRDOHLED)
					{
						decimal Quantity = PERow.Nasnimano;
						if (Quantity >= PERow.QTYSHPPD)
						{
							MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "chimes.wav"));
							// podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
							DialogResult dres = MessageBoxBig.Show(
								(Quantity > PERow.QTYSHPPD ? Fask.Localization.Localization.Prijem4PrijemListPolozkaPreplnenaPokracovaniDotaz : Fask.Localization.Localization.Prijem4PrijemListPolozkaKompletniPokracovaniDotaz),
								Fask.Localization.Localization.Prijem4PrijemListKontrolaUplnosti,
								MessageBoxButtons.YesNo,
								MessageBoxBigIcon.Question
								);
							if (dres == DialogResult.No)
							{
								return;
							}

							if (!Globals.OverFillItem)
							{
								MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazano, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
								return;
							}

						} // if (sinstruct.Quantity <=
					} // if((CONFIG_POKRDOHLED &
					#endregion

					#region Konfiguračne zadavani PONUMBER CONFIG_SNIM_PONUMBER

					string vnditnum = string.Empty;
					string locncode = string.Empty;

					if (prijemDataParametry.Parametry[0].CONFIG_SNIM_PONUMBER)
					{
						skf.Popis = MST_Global.PON_NAME;
						skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["VNDITNUM"].MaxLength;
						skf.CheckLen = true;
						skf.AllowEmpty = false;
						skf.Kod = string.Empty;

						if (skf.ShowDialog() == DialogResult.Cancel)
							return;
						vnditnum = skf.Kod;
					}

					#endregion

					#region Cyklus pro možnost hromadne zadavat SN

					bool DalsiSN = true;

					while (DalsiSN)
					{

						#region Parametry + novy řadek PI

						newguid = System.Guid.Empty; //inicializace noveho guid...

						Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow PIRow = _piTemp.NewCZMST_PIRow();
						PIRow.VNDITNUM = vnditnum;
						PIRow.guid = newguid;

						ppp._pi = PIRow;
						pppsn.PI = PIRow;

						Logging.Log.WriteDebug("while (DalsiSN)", "PerformInsert");

						decimal qty = decimal.Zero;
						string sw = string.Empty;
						string dv = string.Empty;
						string rez1 = string.Empty;
						string rez2 = string.Empty;
						string skl_id = string.Empty;

						// 26.2.2020 ColorProfi JiS
						// doplnen parametr PE.CZ_Expirace_Track a PI.Expirace
						// zadavani expirace, kde kdy ? 
						// jak s fenixem ? a funkncnosti expirace do nejake rezervy?
						DateTime? expirace = null;  // vychozi nastaveni expirace neni ...

						string sarza = string.Empty;
						string sn = string.Empty;

						#endregion

						#region Vytaženi šarže bud z objektu CODE alebo z predlohy PE

						//TaD 13.9.2022 Důvod odtranení logiky ICodeSerltnmbr

						//if ((code is ICodeSerltnmbr) && !string.IsNullOrEmpty(((ICodeSerltnmbr)code).Serltnmbr))
						//    sn = ((ICodeSerltnmbr)code).Serltnmbr;
						//else
						//    sn = PERow.SERLTNUM.Trim();

						sn = PERow.SERLTNUM.Trim();

						#endregion

						#region Vytazeni expirace z objektu CODE
						if ((code is ICodeExpiration) && (((ICodeExpiration)code).Expiration.HasValue))
							expirace = ((ICodeExpiration)code).Expiration;

						#endregion

						#region Predvyplneni Lokace...

						// lokace na davku, automaticky se pouzije ...
						if (miNastavitLokaci.Checked && !string.IsNullOrEmpty(locncodeNaDavku))
						{
							PERow.LOCNCODE = locncodeNaDavku;
						}
						// je povolena prijmova lokace, dojde k jejimu pouziti
						else if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_PRIJMOVANull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_PRIJMOVA)
						{
							PERow.LOCNCODE = prijmovalokace.IsLOCNCODENull() ? string.Empty : prijmovalokace.LOCNCODE.Trim();
						}

						#endregion

						#region ZadaniLocncodePredSN

						if (Prijem_4.Globals.ZadaniLocncodePredSN)
						{
							// povoleno zobrazeni dialogu pro zadani lokace v prijemparams nebo na terminalu
							if (prijemDataParametry.Parametry[0].CONFIG_SNIM_LOCNCODE || miRezimZadavaniLokace.Checked)
							{
								pzl.Popis = MST_Global.LC_NAME;
								pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
								pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
								pzl.AllowEmpty = false;
								pzl.Kod = PERow.LOCNCODE;
								pzl.perow = PERow;

								if (pzl.ShowDialog() == DialogResult.Cancel)
									return;
								locncode = pzl.Kod;
								PIRow.LOCNCODE = locncode;
							}
						}

						#endregion

						#region Nacteni ID skladu

						if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
							skl_id = _sklad != null ? _sklad.skl_id : string.Empty;
						else
							skl_id = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;

						#endregion

						#region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)

						// nacteni skladu online ...
						if (Prijem_4.Globals.NacistSkladIDOnline)
						{
							string sklad_id = string.Empty;
							string sklad_id_dest = string.Empty;

							MST_W.ProdejService.STATUS status = OnlineGetSklad(string.Empty, PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR, PERow.SERLTNUM, out sklad_id, out sklad_id_dest);
							if (status == Fask.MST_W.ProdejService.STATUS.ERROR) // chyba, ukoncit ...
								return;
							else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
							{
								MessageBoxBig.Show(string.Format("Nepodařilo se načíst ID skladu online pro materiál '{0}'", PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
								return;
							}

							skl_id = sklad_id_dest;
						}

						#endregion

						#region CZ_SerNum_Track == 0

						if (PERow.CZ_SerNum_Track == 0) //sledovano na mnozstvi
						{
							#region Predvyplneni mnozstvi

							decimal? qtytmp = GetMnozstvi(string.Empty, code, PERow);

							if (qtytmp.HasValue)
								qty = qtytmp.Value;
							else
								return;

							#endregion
						}

						#endregion

						#region CZ_SerNum_Track == 1 OR CZ_SerNum_Track == 2

						else if (PERow.CZ_SerNum_Track == 1 || PERow.CZ_SerNum_Track == 2) //sledovano na seriova cisla nebo sarze
						{
							#region Cyklus pro vyplneni SN

							bool opakovat = true;
							while (opakovat)
							{
								opakovat = false;

								bool showdialogSN = true;

								#region Vyplneni SN

								#region Puvodny kod

								//if ((PERow.CZ_SerNum_Track == 2) && (code is ICodeSerltnmbr) && !string.IsNullOrEmpty(((ICodeSerltnmbr)code).Serltnmbr))
								//{
								//    sn = ((ICodeSerltnmbr)code).Serltnmbr;
								//    showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								//}
								
								#endregion

								#region TaD 13.9.2022 

								//SN Seriove číslo
								if ((PERow.CZ_SerNum_Track == 1) && (code is ICodeSerialNumber) && !string.IsNullOrEmpty(((ICodeSerialNumber)code).SN))
								{
									sn = ((ICodeSerialNumber)code).SN;
									showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}

								//šarže
								if ((PERow.CZ_SerNum_Track == 2) && (code is ICodeSarze) && !string.IsNullOrEmpty(((ICodeSarze)code).Sarze))
								{
									sn = ((ICodeSarze)code).Sarze;
									showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}
								
								#endregion



								#endregion

								#region Zobrazeni dialogu zadani SN/ šarže

								if (showdialogSN)
								{
									pppsn.Popis = (PERow.CZ_SerNum_Track == 1 ? Fask.Localization.Localization.Prijem4PrijemListSerioveCislo : Fask.Localization.Localization.Prijem4PrijemListSarze);
									pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
									pppsn.Len = PERow.CZ_SerNum_Delka;
									pppsn.CheckLen = true;
									pppsn.AllowEmpty = Prijem_4.Globals.PovolitPrazdnouHodnotuSN;
									pppsn.Kod = sn;

									//vytazeni vsech seriovych cisel pro polozku z databaze ...
									pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

									if (pppsn.ShowDialog() == DialogResult.Cancel)
									{
										#region Pri opusteni cteni SN upozornit na nedostatek
										if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{ // test, zda je nasnimane pozadovane mnozstvi
											if (PERow.QTYSHPPD > PERow.Nasnimano)
											{
												MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
												DialogResult dr = MessageBoxBig.Show(
													Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
													Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
													 MessageBoxButtons.YesNo,
													  MessageBoxBigIcon.Question
												);
												if (dr == DialogResult.No)
												{
													opakovat = true;
													continue; //bude pokracovat znovu zadanim SN
												}
											}
										}
										#endregion
										return;
									}

									sn = pppsn.Kod;
								}

								#endregion

								qty = 1;

								#region Test duplicity SN

								// 27.10.2017 JiS => SN ma smysl kontrolovat jen pokud jde o SN
								//                => Sarze duplicitni muze byt vzdy ...
								if ((PERow.CZ_SerNum_Track == 1) && !prijemDataParametry.Parametry[0].CONFIG_DUPLIC_SN) //test na duplicitu SN/Sarze (True=duplicita povolena)
								{
									bool exist = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_Duplicita_SN_ByKey(prijemDataParametry.Parametry[0].CONFIG_PRIM_KEY1, PERow, sn);
									if (exist)
									{
										if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Prijem4PrijemListChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
											== DialogResult.Cancel)
											return;
										else
										{
											opakovat = true;
											//break;
										}
									}
								}

								#endregion

								#region online funkce pro generovani sarze podle itemnmbr a zadane hodnoty serltnum

								if (Prijem_4.Globals.GenerovaniSarze)
								{
									while (true)
									{
										string sn2 = string.Empty;
										if (!OnlineGenerateSerltnum(PERow.IsITEMNMBRNull() ? string.Empty : PERow.ITEMNMBR.Trim(), sn, skl_id, out sn2))
										{
											DialogResult dr = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListGenerovaniCislaPaletyOpakovatDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
											if (dr == DialogResult.Yes)
												continue;
											else
												return;
										}
										else
										{
											sn = sn2;
											break;
										}
									}
								}

								#endregion


							} //while (cist) ... 

							#endregion

							#region CZ_SerNum_Track == 2 šarže => zadani mnozstvi

							if (PERow.CZ_SerNum_Track == 2)
							{
								decimal? qtytmp = GetMnozstvi(sn, code, PERow);

								if (qtytmp.HasValue)
									qty = qtytmp.Value;
								else
									return;
							}

							#endregion

						}

						#endregion

						#region CZ_SerNum_Track = 10

						else if (PERow.CZ_SerNum_Track == 10)
						{
							#region Cyklus pro vyplneni SN

							bool opakovat_2 = true;
							while (opakovat_2)
							{
								opakovat_2 = false;

								bool showdialogSN = true;

								#region Vyplneni SN

								if ((code is ICodeSerialNumber) && !string.IsNullOrEmpty(((ICodeSerialNumber)code).SN))
								{
									sn = ((ICodeSerialNumber)code).SN;
									showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}

								#endregion

								#region Zobrazeni dialogu zadani SN

								if (showdialogSN)
								{
									pppsn.Popis =  Fask.Localization.Localization.Prijem4PrijemListSerioveCislo;
									pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
									pppsn.Len = PERow.CZ_SerNum_Delka;
									pppsn.CheckLen = true;
									pppsn.AllowEmpty = Prijem_4.Globals.PovolitPrazdnouHodnotuSN;
									//pppsn.Kod = sn;

									//vytazeni vsech seriovych cisel pro polozku z databaze ...
									pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

									if (pppsn.ShowDialog() == DialogResult.Cancel)
									{
										#region Pri opusteni cteni SN upozornit na nedostatek
										if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{ // test, zda je nasnimane pozadovane mnozstvi
											if (PERow.QTYSHPPD > PERow.Nasnimano)
											{
												MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
												DialogResult dr = MessageBoxBig.Show(
													Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
													Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
													 MessageBoxButtons.YesNo,
													  MessageBoxBigIcon.Question
												);
												if (dr == DialogResult.No)
												{
													opakovat_2 = true;
													continue; //bude pokracovat znovu zadanim SN
												}
											}
										}
										#endregion

										return;
									}

									sn = pppsn.Kod;
								}

								#endregion

								qty = 1;

								#region Test duplicity SN

								// 27.10.2017 JiS => SN ma smysl kontrolovat jen pokud jde o SN
								//                => Sarze duplicitni muze byt vzdy ...
								if (!prijemDataParametry.Parametry[0].CONFIG_DUPLIC_SN) //test na duplicitu SN/Sarze (True=duplicita povolena)
								{
									bool exist = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_Duplicita_SN_ByKey(prijemDataParametry.Parametry[0].CONFIG_PRIM_KEY1, PERow, sn);
									if (exist)
									{
										if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Prijem4PrijemListChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
											== DialogResult.Cancel)
											return;
										else
										{
											opakovat_2 = true;
											//break;
										}
									}
								}

								#endregion

								#region Zadani sarze

								bool showdialogSarze = true;


								#region Vyplneni Sarze

								if ((code is ICodeSarze) && !string.IsNullOrEmpty(((ICodeSarze)code).Sarze))
								{
									sarza = ((ICodeSarze)code).Sarze;
									showdialogSarze = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}

								#endregion

								#region Zobrazeni dialogu zadani šarže

								if (showdialogSarze)
								{
									pppsn.Popis = Fask.Localization.Localization.Prijem4PrijemListSarze;
									pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
									pppsn.Len = PERow.CZ_SerNum_Delka;
									pppsn.CheckLen = true;
									pppsn.AllowEmpty = Prijem_4.Globals.PovolitPrazdnouHodnotuSN;
									pppsn.Kod = sarza;

									//vytazeni vsech seriovych cisel pro polozku z databaze ...
									pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

									if (pppsn.ShowDialog() == DialogResult.Cancel)
									{
										#region Pri opusteni cteni SN upozornit na nedostatek
										if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{ // test, zda je nasnimane pozadovane mnozstvi
											if (PERow.QTYSHPPD > PERow.Nasnimano)
											{
												MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
												DialogResult dr = MessageBoxBig.Show(
													Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
													Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
													 MessageBoxButtons.YesNo,
													  MessageBoxBigIcon.Question
												);
												if (dr == DialogResult.No)
												{
													opakovat_2 = true;
													continue; //bude pokracovat znovu zadanim SN
												}
											}
										}
										#endregion

										return;
									}

									sarza = pppsn.Kod;
								}

								#endregion


								#endregion


							} //while (cist) ... 

							#endregion

						}
						#endregion


						#region CZ_SerNum_Track = 11

						else if (PERow.CZ_SerNum_Track == 11)
						{
							#region Cyklus pro vyplneni SN

							bool opakovat_2 = true;
							while (opakovat_2)
							{
								opakovat_2 = false;

								bool showdialogSN = true;

								#region Vyplneni SN

								if ((code is ICodeSerialNumber) && !string.IsNullOrEmpty(((ICodeSerialNumber)code).SN))
								{
									sn = ((ICodeSerialNumber)code).SN;
									showdialogSN = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}

								#endregion

								#region Zobrazeni dialogu zadani SN

								if (showdialogSN)
								{
									pppsn.Popis = Fask.Localization.Localization.Prijem4PrijemListSerioveCislo;
									pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
									pppsn.Len = PERow.CZ_SerNum_Delka;
									pppsn.CheckLen = true;
									pppsn.AllowEmpty = true;

									//vytazeni vsech seriovych cisel pro polozku z databaze ...
									pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

									if (pppsn.ShowDialog() == DialogResult.Cancel)
									{
										#region Pri opusteni cteni SN upozornit na nedostatek
										if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{ // test, zda je nasnimane pozadovane mnozstvi
											if (PERow.QTYSHPPD > PERow.Nasnimano)
											{
												MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
												DialogResult dr = MessageBoxBig.Show(
													Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
													Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
													 MessageBoxButtons.YesNo,
													  MessageBoxBigIcon.Question
												);
												if (dr == DialogResult.No)
												{
													opakovat_2 = true;
													continue; //bude pokracovat znovu zadanim SN
												}
											}
										}
										#endregion

										return;
									}

									sn = pppsn.Kod;
								}

								#endregion

								qty = 1;

								#region Test duplicity SN

								// 27.10.2017 JiS => SN ma smysl kontrolovat jen pokud jde o SN
								//                => Sarze duplicitni muze byt vzdy ...
								if (!string.IsNullOrEmpty(sn))
								{
									if (!prijemDataParametry.Parametry[0].CONFIG_DUPLIC_SN) //test na duplicitu SN/Sarze (True=duplicita povolena)
									{
										bool exist = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_Duplicita_SN_ByKey(prijemDataParametry.Parametry[0].CONFIG_PRIM_KEY1, PERow, sn);
										if (exist)
										{
											if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListSerioveCisloJizByloNasnimano, Fask.Localization.Localization.Prijem4PrijemListChyba, MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning)
												== DialogResult.Cancel)
												return;
											else
											{
												opakovat_2 = true;
												//break;
											}
										}
									} 
								}

								#endregion

								#region Zadani sarze

								bool showdialogSarze = true;


								#region Vyplneni Sarze

								if ((code is ICodeSarze) && !string.IsNullOrEmpty(((ICodeSarze)code).Sarze))
								{
									sarza = ((ICodeSarze)code).Sarze;
									showdialogSarze = Prijem_4.Globals.ZobrazitDialogZadaniSN;
								}

								#endregion

								#region Zobrazeni dialogu zadani šarže

								if (showdialogSarze)
								{
									pppsn.Popis = Fask.Localization.Localization.Prijem4PrijemListSarze;
									pppsn.CodeType = SejmiKodFormDropdown.TypeOfCode.AlphaNumeric;
									pppsn.Len = PERow.CZ_SerNum_Delka;
									pppsn.CheckLen = true;
									pppsn.AllowEmpty = true;
									pppsn.Kod = sarza;

									//vytazeni vsech seriovych cisel pro polozku z databaze ...
									pppsn.PESN = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByDavkaPolozka_PE_SN(PERow.CountEntries, PERow.ITEMNMBR);

									if (pppsn.ShowDialog() == DialogResult.Cancel)
									{
										#region Pri opusteni cteni SN upozornit na nedostatek
										if (prijemDataParametry.Parametry[0].CONFIG_KONT_UPL_POL)
										{ // test, zda je nasnimane pozadovane mnozstvi
											if (PERow.QTYSHPPD > PERow.Nasnimano)
											{
												MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "dotaz.wav"));
												DialogResult dr = MessageBoxBig.Show(
													Fask.Localization.Localization.Prijem4PrijemListPolozkaNeniKompletniKonecSnimaniDotaz,
													Fask.Localization.Localization.Prijem4PrijemListSnimaniSN,
													 MessageBoxButtons.YesNo,
													  MessageBoxBigIcon.Question
												);
												if (dr == DialogResult.No)
												{
													opakovat_2 = true;
													continue; //bude pokracovat znovu zadanim SN
												}
											}
										}
										#endregion

										return;
									}

									sarza = pppsn.Kod;
								}

								#endregion


								#endregion


							} //while (cist) ... 

							#endregion

						}
						#endregion


						#region Neznamy CZ_SerNum_Track

						else
						{
							MessageBox.Show(PERow.CZ_SerNum_Track.ToString(), "CZ_Sernum_Track");
							return;
						}

						#endregion

						#region Rohodovat Sklad Expedice Rozdelit

						if (Prijem_4.Globals.RozhodovatSkladExpedice)
						{

							decimal PrijimaneMnozstvi = qty;

							decimal MnozstviDodavatelePozadovano = 0; ;
							decimal MnozstviDodavateleDodano = 0;
							decimal MnozstviDodavateleDodat = 0;
							decimal MnozstviOdberateliPozadovano = 0;
							decimal MnozstviOdberatelumDodano = 0;
							decimal MnozstviOdberatelumDodat = 0;
							decimal Vysledek = 0;

							OnlineGetSkladExpedice(
												PERow.ITEMNMBR,
												PrijimaneMnozstvi,
												PERow.Nasnimano,
												out  MnozstviDodavatelePozadovano,
												out  MnozstviDodavateleDodano,
												out  MnozstviDodavateleDodat,
												out  MnozstviOdberateliPozadovano,
												out  MnozstviOdberatelumDodano,
												out  MnozstviOdberatelumDodat,
												out  Vysledek
												);

							decimal NaSklad = 0;
							decimal NaExpedici = 0;


							if (Vysledek >= PrijimaneMnozstvi)
							{ // ma se jeste dodat "Vysledek" a prijimam mene nez dodavam ...
								NaExpedici = PrijimaneMnozstvi;
								NaSklad = 0;
								MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundExpedice));
							}
							else if ((0 < Vysledek) && (Vysledek < PrijimaneMnozstvi))
							{
								NaExpedici = Vysledek;
								NaSklad = PrijimaneMnozstvi - Vysledek;
								MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSkladExpedice));
							}
							else if (Vysledek <= 0)
							{
								NaExpedici = 0;
								NaSklad = PrijimaneMnozstvi;
								MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, Settings.PrijemSoundSklad));
							}


							if (Prijem_4.Globals.ZobrazovatReport && !Prijem_4.Globals.MnozstviAutoJedna)
							{

								//TaD sledovani na mnozstvi 22.05.2018 Hanibal 
								using (FormReport frmrep = new FormReport())
								{
									frmrep.CZ_CarKod = PERow.CZ_CarKod;
									frmrep.ITEMDESC = PERow.ITEMDESC;
									frmrep.ITEMNMBR = PERow.ITEMNMBR;
									frmrep.NaExpedici = NaExpedici;
									frmrep.NaSklad = NaSklad;

									frmrep.MnozstviDodavatelePozadovano = MnozstviDodavatelePozadovano;
									frmrep.MnozstviDodavateleDodano = MnozstviDodavateleDodano;
									frmrep.MnozstviDodavateleDodat = MnozstviDodavateleDodat;
									frmrep.MnozstviOdberateliPozadovano = MnozstviOdberateliPozadovano;
									frmrep.MnozstviOdberatelumDodano = MnozstviOdberatelumDodano;
									frmrep.MnozstviOdberatelumDodat = MnozstviOdberatelumDodat;


									if (frmrep.ShowDialog() == DialogResult.Cancel)
										return;

								}
							}
						}

						#endregion

						#region Fotky

						List<string> fotofilenames = new List<string>();
						// povoleni foceni
						if (Prijem_4.Globals.PovolitFoceniPriPridaniPolozky)
						{
							// odstraneni veskerych fotek, pokud drive byly nasnimany
							string[] fileNames = System.IO.Directory.GetFiles(Main.ImagesDir, @sn + "*.jpg");
							foreach (var item in fileNames)
							{
								File.Delete(item);
							}
							int pocet = 1;
							while (true)
							{
								DialogResult dr = Program.mstw.Photo.CaptureImage(sn + "_" + pocet.ToString());
								if (dr != DialogResult.OK)
									break;

								// 18.5.2016 PeV: vysledny nazev souboru se prebira z providera
								fotofilenames.Add(Program.mstw.Photo.ImageFilename);
								pocet++;
							}
						}
						#endregion

						decimal mnozstvi = qty * (PERow.QTYPACK > 0 ? PERow.QTYPACK : 1);

						PIRow.QTYSHPPD = mnozstvi;
						PIRow.QTYSHPPDMJ = qty;
						PIRow.QTYPACK = PERow.QTYPACK;
						PIRow.SERLTNUM = sn;

						#region Test preplnenosti

						if (!Globals.OverFillItem)
						{
							Logging.Log.WriteDebug("OverFillItem Start", "PerformInsert");
							decimal Quantity = PERow.Nasnimano;
							if ((Quantity + mnozstvi) > PERow.QTYSHPPD)
							{
								DialogResult dres2 = MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazanoOpakovatZadaniDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
								if (dres2 == DialogResult.Yes)
									continue;
								else
									return;
							}
						}
						#endregion

						#region Lokace
						//TODO udelat s tohoto metodu, použito vicekrat v kodu

						if (!Globals.ZadaniLocncodePredSN)
						{
							// povoleno zobrazeni dialogu pro zadani lokace v prijemparams nebo na terminalu
							if (prijemDataParametry.Parametry[0].CONFIG_SNIM_LOCNCODE || miRezimZadavaniLokace.Checked)
							{
								pzl.Popis = MST_Global.LC_NAME;
								pzl.CodeType = PrijemZadejLokaci.TypeOfCode.AlphaNumeric;
								pzl.MaxLength = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["LOCNCODE"].MaxLength;
								pzl.AllowEmpty = false;
								pzl.Kod = PERow.IsLOCNCODENull() ? string.Empty : PERow.LOCNCODE.Trim();
								pzl.perow = PERow;

								if (pzl.ShowDialog() == DialogResult.Cancel)
									return;
								locncode = pzl.Kod;
								PIRow.LOCNCODE = locncode;
							}
						}

						#endregion

						#region Zadani doplnujicich informaci

						if (PERow.CZ_SW_Track > 0)
						{
							skf.Popis = MST_Global.SWName;
							skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
							skf.Len = PERow.CZ_SW_Delka;
							skf.CheckLen = true;
							skf.AllowEmpty = false;
							skf.Kod = string.Empty;

							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							sw = skf.Kod;
							PIRow.KOD_SW = sw;
						}

						if (PERow.CZ_DatVyr_Track > 0)
						{
							skf.Popis = MST_Global.DVName;
							skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
							skf.Len = PERow.CZ_DatVyr_Delka;
							skf.CheckLen = true;
							skf.AllowEmpty = false;
							skf.Kod = string.Empty;

							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							dv = skf.Kod;
							PIRow.DAT_VYROBY = dv;
						}

						// 16.1.2017 JiS - uprava pro prijem LABARA dle predlohy                  
						if (PERow.CZ_REZ1_TRACK > 0) //pozadovano zadani hodnoty rez1
						{
							skf.Popis = MST_Global.REZ1_PRIJ_NAME;
							skf.CodeType = Prijem_4.Globals.Rez1Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
							skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["REZ_1"].MaxLength;
							skf.CheckLen = !Prijem_4.Globals.Rez1Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
							skf.AllowEmpty = !Prijem_4.Globals.Rez1Povinne;
							skf.Kod = Prijem_4.Globals.Rez1Pamatovat ? Settings.PrijemRez1LastValue : string.Empty;

							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							rez1 = skf.Kod;
							PIRow.REZ_1 = rez1;
							if (Prijem_4.Globals.Rez1Pamatovat) Settings.PrijemRez1LastValue = rez1;
						}

						//16.1.2017 JiS pozadavek na zadani hodnoty rez2
						if (PERow.CZ_REZ2_TRACK > 0)
						{
							skf.Popis = MST_Global.REZ2_PRIJ_NAME;
							skf.CodeType = Prijem_4.Globals.Rez2Cislo ? SejmiKodForm.TypeOfCode.Numeric : SejmiKodForm.TypeOfCode.AlphaNumeric;
							skf.Len = (int)Fask.SQLiteDBs.Columns.Prijem.ColumnsInfo_CZMST_PI["REZ_2"].MaxLength;
							skf.CheckLen = !Prijem_4.Globals.Rez2Cislo; // Pokud to neni cislo, tak se bude kontrolovat delka na 21 znaku ...
							skf.AllowEmpty = !Prijem_4.Globals.Rez2Povinne;
							skf.Kod = Prijem_4.Globals.Rez2Pamatovat ? Settings.PrijemRez2LastValue : string.Empty;

							if (skf.ShowDialog() == DialogResult.Cancel)
								return;
							rez2 = skf.Kod;
							if (Prijem_4.Globals.Rez2Pamatovat) Settings.PrijemRez2LastValue = rez2;
						}

						#endregion

						#region Expirace
						//POUŽITELNOST DO
						//17 použitelnost do (RRMMDD) n2+n6
						//Použitelnost do… (Expiration Date – USE BY či EXPIRY) označuje finální limit spotřeby
						//či použití produktu. V sektoru zdravotnictví se používá pro vyjádření data exspirace.
						// => parametr prijem_pi.cz_expirace_track > 0 => vyzadovat zadani expirace, jinak bez expirace (expirace = null)
						if (!PERow.IsCZ_Expirace_TrackNull() && PERow.CZ_Expirace_Track > 0)
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

						Cursor.Current = Cursors.WaitCursor;

						#region SSCC a typ palety

						string typepal = string.Empty;
						string nmbrpal = string.Empty;
						try
						{
							//string[] split;
							if (Globals.TypOznaceniPalety)
							{
								//split = this.Paleta.Split(new char[] { ':' });
								typepal = this.Paleta.ID;
								nmbrpal = this.Paleta.sscc;
							}
							else
							{
								nmbrpal = PERow.IsNMBRPALNull() ? string.Empty : PERow.NMBRPAL;
								typepal = PERow.IsTYPEPALNull() ? string.Empty : PERow.TYPEPAL;
							}
						}
						catch (Exception ex)
						{
							Logging.Log.WriteDebug(ex.Message);
						}

						#endregion

						#region Online insert

						if (Globals.OnlinePohyby)
						{
							//priprava pro server
							PrijemService.Prijem dtPOnline = new Fask.MST_W.PrijemService.Prijem();
							PrijemService.Prijem.CZMST_PIRow rPOnline = dtPOnline.CZMST_PI.NewCZMST_PIRow();
							rPOnline.CountEntries = PERow.CountEntries;
							rPOnline.PONUMBER = PERow.PONUMBER;
							rPOnline.ORD = PERow.ORD;
							rPOnline.ITEMNMBR = PERow.ITEMNMBR;
							rPOnline.VNDDOCNM = PERow.VNDDOCNM;
							rPOnline.VNDITNUM = (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM;
							rPOnline.LOCNCODE = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
							rPOnline.MJ = PERow.IsMJNull() ? "" : PERow.MJ.Trim();
							rPOnline.QTYSHPPD = mnozstvi;
							rPOnline.QTYSHPPDMJ = qty;
							rPOnline.QTYPACK = PERow.QTYPACK;
							rPOnline.SERLTNUM = sn;
							rPOnline.KOD_SW = sw;
							rPOnline.DAT_VYROBY = dv;
							rPOnline.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
							rPOnline.TIMEDONE = DateTime.Now.ToString("HHmmss");
							rPOnline.CZ_CarKod = PERow.CZ_CarKod;
							rPOnline.REZ_1 = rez1;
							rPOnline.REZ_2 = rez2;
							rPOnline.USER_ID = MST_Global.UserID;
							rPOnline.GUID = newguid;
							rPOnline.INPUT_MODE = _input_mode;
							rPOnline.ID_TERMINAL = MST_Global.TerminalID;
							rPOnline.DEX_ROW_ID = PERow.DEX_ROW_ID;

							rPOnline.NMBRPAL = nmbrpal;
							rPOnline.TYPEPAL= typepal;

							if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
								rPOnline.SKL_ID = _sklad != null ? _sklad.skl_id : string.Empty;
							else
								rPOnline.SKL_ID = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;
							if (expirace.HasValue)
								rPOnline.Expirace = expirace.Value;
							dtPOnline.CZMST_PI.AddCZMST_PIRow(rPOnline);

							while (true)
							{
								// ulozeni v pripade Online musi projit, protoze je to zavisle dale pri dohledavani
								// delaji se online dotazy na stav na serveru ... 
								try
								{
									PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_Add(PERow.CountEntries, MST_Global.TerminalID, dtPOnline);
									if (so.Exception || so.StatusText != "OK")
									{
										if (DialogResult.Cancel == MessageBoxBig.Show(
											string.Format(Fask.Localization.Localization.Prijem4PrijemListUlozeniOnlineProblemOpakovatDotaz, so.StatusText),
											Fask.Localization.Localization.Prijem4PrijemListUlozeniOnline,
											MessageBoxButtons.RetryCancel,
											MessageBoxBigIcon.Critical,
											Color.Red))
										{
											return;
										}
									}
									else
									{ //ulozeni se podarilo ... 
										break;
									}

								}
								catch (Exception e)
								{
									if (DialogResult.Cancel == MessageBoxBig.Show(
										string.Format(Fask.Localization.Localization.Prijem4PrijemListUlozeniOnlineProblemOpakovatDotaz, e.Message),
										Fask.Localization.Localization.Prijem4PrijemListUlozeniOnline,
										 MessageBoxButtons.RetryCancel,
										  MessageBoxBigIcon.Critical,
										  Color.Red
										))
									{
										return;
									}
								}
							}
						}

						#endregion

						#region Lokačni mechanizmus

						// online ulozeni do lokacniho mechanismu ... probiha pouze v pripade, ze je vypnute zalokovani
						if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT && !Prijem_4.Globals.PovolitZalokovani)
						{
							Cursor.Current = Cursors.WaitCursor;
							Fask.MST_W.LokaceService.LokacePohyb pohybrow = new Fask.MST_W.LokaceService.LokacePohyb();
							pohybrow.ITEMNMBR = PERow.ITEMNMBR;
							pohybrow.DOCUMENT_NUMBER = PERow.PONUMBER;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS)
							pohybrow.POHYB_TYPE = Fask.MST_W.LokaceService.TypeOfRecord.P;  // prijem
							pohybrow.POHYB_SRC = "P";
							pohybrow.SOURCE = "T";      // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
							pohybrow.QTYSHPPD = (decimal)mnozstvi;
							pohybrow.SERLTNUM = sn;
							if (Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu)
								pohybrow.SKL_ID_SRC = _sklad != null ? _sklad.skl_id : string.Empty;
							else
								pohybrow.SKL_ID_SRC = PERow.IsSKL_IDNull() ? string.Empty : PERow.SKL_ID;

							pohybrow.SKL_ID_DST = string.Empty;
							pohybrow.LOCNCODE_SRC = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
							pohybrow.LOCNCODE_DST = string.Empty;
							pohybrow.UserID = MST_Global.UserID;
							pohybrow.TermID = MST_Global.TerminalID;
							pohybrow.guid = newguid;
							pohybrow.Expiration = expirace;
							pohybrow.ITEMDESC = PERow.IsITEMDESCNull() ? string.Empty : PERow.ITEMDESC;
							pohybrow.CountEntries = PERow.CountEntries;
							pohybrow.dateeveT = DateTime.Now;   // datum terminalu

							try
							{
								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - start", "LocationLog");
								Classes.LokaceLog.writeBody(pohybrow);

								Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.AddRecord(pohybrow);
								Cursor.Current = Cursors.Default;
								switch (sl.State)
								{
									case Fask.MST_W.LokaceService.States.OK:
										break;
									case Fask.MST_W.LokaceService.States.ERROR:
										MessageBoxBig.Show("Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										return;
									default:
										MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
										return;
								}

								Logging.Log.WriteAdvanced("Operation:add,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".AddRecord - end", "LocationLog");
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
											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid, "LocationLog");

											Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.DeleteRecordByGuid(pohybrow.guid, Fask.MST_W.LokaceService.ModulName.PRIJEM);
											DialogResult dr = DialogResult.No;
											switch (sl.State)
											{
												case Fask.MST_W.LokaceService.States.OK:
													dr = DialogResult.Yes;
													break;
												case Fask.MST_W.LokaceService.States.ERROR:
													dr = MessageBoxBig.Show("Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return;
												default:
													dr = MessageBoxBig.Show("Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
													return;
											}

											Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

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

						#region Insert do PI

						//pokud je online insert, tak musi projit online insert !!!
						// prida vydanou polozku do tabulky
						while (true)
						{
							try
							{
								Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt_pi = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
								var piN = dt_pi.NewCZMST_PIRow();

								piN.CountEntries = PERow.CountEntries;
								piN.PONUMBER = PERow.PONUMBER;
								piN.ORD = PERow.ORD;
								piN.ITEMNMBR = PERow.ITEMNMBR;
								piN.VNDDOCNM = PERow.VNDDOCNM;
								piN.VNDITNUM = (vnditnum.Length > 0) ? vnditnum : PERow.VNDITNUM;
								piN.LOCNCODE = (locncode.Length > 0) ? locncode : PERow.LOCNCODE;
								piN.MJ = PERow.IsMJNull() ? "" : PERow.MJ.Trim();
								piN.QTYSHPPD = mnozstvi;
								piN.QTYSHPPDMJ = qty;
								piN.QTYPACK = PERow.QTYPACK;
								piN.SERLTNUM = sn;
								piN.KOD_SW = sw;
								piN.DAT_VYROBY = dv;
								piN.DATEDONE = DateTime.Now.ToString("yyyyMMdd");
								piN.TIMEDONE = DateTime.Now.ToString("HHmmss");
								piN.CZ_CarKod = PERow.CZ_CarKod;
								piN.REZ_1 = rez1;
								piN.REZ_2 = rez2;
								piN.USER_ID = MST_Global.UserID;
								piN.DEX_ROW_ID = PERow.DEX_ROW_ID;
								piN.guid = newguid = Guid.NewGuid();
								piN.INPUT_MODE = _input_mode;
								piN.ID_TERMINAL = MST_Global.TerminalID;
								piN.WEIGHT = PERow.IsWEIGHTNull() ? 0 : PERow.WEIGHT;
								//piN.NMBRPAL = PERow.IsNMBRPALNull() ? string.Empty : PERow.NMBRPAL;
								//piN.TYPEPAL = PERow.IsTYPEPALNull() ? string.Empty : PERow.TYPEPAL;

								piN.NMBRPAL = nmbrpal;
								piN.TYPEPAL = typepal;

								piN.ITEMCODE = PERow.IsITEMCODENull() ? string.Empty : PERow.ITEMCODE;
								piN.SKL_ID = skl_id;
								if (expirace.HasValue)
									piN.Expirace = expirace.Value;

								if (!string.IsNullOrEmpty(sarza))
									piN.AttributeToSN = sarza;
								else
									piN.SetAttributeToSNNull();

								dt_pi.AddCZMST_PIRow(piN);

								Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Update_PI(piN);
								break;
							}
							catch (Exception ex)
							{
								Logging.Log.Write("pita.insert," + ex.Message, "Prijem");
								if (DialogResult.Yes != MessageBoxBig.Show(ex.Message + "\n\nPřejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
								{
									#region lokace
									// pokud ne, dojde online odmazani ...
									if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT && !Prijem_4.Globals.PovolitZalokovani)
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
												Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newguid.ToString(), "LocationLog");

												Fask.MST_W.LokaceService.StatusLokace sl = Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.DeleteRecordByGuid(newguid, Fask.MST_W.LokaceService.ModulName.PRIJEM);
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

												Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:R,TypeOfRecord:P,Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");

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
										// chyba pri ulozeni, pokud je lokacni mechanismus vypnuty
										throw ex;
									}
									#endregion
								}
							}
						}

						#endregion

						#region ulozeni fotek do DB

						foreach (var imgname in fotofilenames)
						{
							Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Insert_PIF(imgname, newguid);
						}

						#endregion

						PERow.Nasnimano += mnozstvi; //oprava aktualizace zbyvajiciho mnozstvi ve vnitrnim kolecku ...
						UpdateMnozstvi(PERow.ITEMNMBR, PERow.PONUMBER, PERow.ORD, PERow.Nasnimano);

						Cursor.Current = Cursors.Default;

						#region CONFIG_MNOZSTVI_ZADAVAT

						if (!prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT)
						{
							if (MST_Global.PrijemTimeDialog)
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListPolozkaUspesneZapsana, PERow.ITEMDESC.Trim()), Fask.Localization.Localization.Prijem4PrijemListVlozeni, MessageBoxButtons.OK, MessageBoxBigIcon.Information, Color.Green);
							else
								MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemListPolozkaUspesneZapsana, PERow.ITEMDESC.Trim()), Fask.Localization.Localization.Prijem4PrijemListVlozeni, MessageBoxButtons.OK, MessageBoxBigIcon.Information, Color.Green);
						}

						#endregion

						#region Tisk Etikety

						TiskEtikety(PERow, newguid);

						#endregion

						#region Kontrola Dokoncenosti

						bool succ = true;

						if (kontrolaDokoncenosti(prijemDataParametry.Parametry[0].CONFIG_KONT_DOKONCENOSTI))
						{
							odeslatAktualniDavku();
							return;
						}
						#endregion

						DalsiSN = !prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE;

						// zkontroluje, zda je nacten pozadovany pocet
						if (PERow.Nasnimano < PERow.QTYSHPPD)
							continue;   // jeste neni nasnimane vse

						if (DalsiSN == false)
							continue;


						#region Kontrola uplnosti polozky

						// dohledani poctu, pokud je predloha, existuje konf.soubor a je nastaven
						// odpovidajici parametr
						if (prijemDataParametry.Parametry[0].CONFIG_POKRDOHLED)
						{
							decimal Quantity = PERow.Nasnimano;
							if (Quantity >= PERow.QTYSHPPD)
							{
								MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "chimes.wav"));
								// podle predlohy uz jsou nacteny vsechny polozky, pokracovat?
								DialogResult dres = MessageBoxBig.Show(
									(Quantity > PERow.QTYSHPPD ? Fask.Localization.Localization.Prijem4PrijemListPolozkaPreplnenaPokracovaniDotaz : Fask.Localization.Localization.Prijem4PrijemListPolozkaKompletniPokracovaniDotaz),
									Fask.Localization.Localization.Prijem4PrijemListKontrolaUplnosti,
									MessageBoxButtons.YesNo,
									MessageBoxBigIcon.Question
									);
								if (dres == DialogResult.No)
								{
									return;
								}

								if (!Globals.OverFillItem)
								{
									MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemListPreplneniZakazano, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
									return;
								}

							} // if (sinstruct.Quantity <=
						} // if((CONFIG_POKRDOHLED &

						#endregion

					} // while(DalsiSN) 

					#endregion

				}
				catch (Exception ex)
				{
					Logging.Log.WriteDebug(ex.Message, "Prijem_3.PerformInsert");
					if (MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical) == DialogResult.OK) { }

					return;
				}
				finally
				{
					ScannerStart();
					UpdateForm();
					this.Show();
				}
			}
			finally
			{
				#region finally uvolnení objektu...

				if ((skf != null) && (!skf.IsDisposed))
				{
					skf.Dispose();
					skf = null;
				}

				if ((pzl != null) && (!pzl.IsDisposed))
				{
					pzl.Dispose();
					pzl = null;
				}

				if ((ppp != null) && (!ppp.IsDisposed))
				{
					ppp.Dispose();
					ppp = null;
				}

				if ((pppsn != null) && (!pppsn.IsDisposed))
				{
					pppsn.Dispose();
					pppsn = null;
				}

				#endregion
			}
		}

		/// <summary>
		/// Metoda pro doplneni množstvi
		/// </summary>
		/// <param name="sn"></param>
		/// <param name="code"></param>
		/// <param name="PERow"></param>
		/// <returns></returns>
		private decimal? GetMnozstvi(string sn, BaseCode code, Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow)
		{
			if (Prijem_4.Globals.MnozstviAutoJedna)
			{
				return 1;
			}
			else
			{

				if (!Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && (code is ICodeWeight) && ((ICodeWeight)code).Weight.HasValue)
				{
					decimal weight = ((ICodeWeight)code).Weight.Value;

					return (weight
							/ (PERow.IsWEIGHTNull() || (PERow.WEIGHT == 0) ? 1 : PERow.WEIGHT)
							/ ((PERow.QTYPACK == 0) ? 1 : PERow.QTYPACK)
							);
				}
				if (!Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu && (code is ICodeQuantity) && ((ICodeQuantity)code).Quantity.HasValue)
				{
					return ((ICodeQuantity)code).Quantity.Value;
				}
				else
				{
					ppp.Popis = PERow.QTYPACK > 0 ? Fask.Localization.Localization.Prijem4PrijemListMnozstviBaleni : Fask.Localization.Localization.Prijem4PrijemListMnozstvi;// "Množství" + (PERow.QTYPACK > 0 ? " balení" : string.Empty);
					ppp.CodeType = SejmiKodForm.TypeOfCode.Numeric;
					ppp.Len = 0;
					ppp.Serltnum = sn;
					ppp.CheckLen = false;
					ppp.AllowEmpty = false;
					ppp.Kod = mnozstviPredvyplnit(PERow, code);
					ppp.ScannerOff = !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_SCANNEREM;

					if (ppp.Kod != string.Empty && !prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_ZADAVAT && prijemDataParametry.Parametry[0].CONFIG_ZADAT_MN_POKAZDE)
					{
						//Nezadani mnozstvi a pouziti viz vrchni konfigurace
					}
					else
					{
						DialogResult dpppres = ppp.ShowDialog();

						if (dpppres == DialogResult.Cancel)
							return null;
					}

					return decimal.Parse(ppp.Kod);
				}
			}
		}

		/// <summary>
		/// predvyplneni mnozstvi do hodnoty Kod dialogu "ppp" pri zadavani mnozstvi ...
		/// </summary>
		/// <param name="PERow">radek PE</param>
		/// <returns>textovou hodnotu mnozstvi pro predvyplneni</returns>
		private string mnozstviPredvyplnit(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow)
		{
			return mnozstviPredvyplnit(PERow, null);
		}

		/// <summary>
		/// predvyplneni mnozstvi do hodnoty Kod dialogu "ppp" pri zadavani mnozstvi ...
		/// </summary>
		/// <param name="PERow">radek PE</param>
		/// <returns>textovou hodnotu mnozstvi pro predvyplneni</returns>
		private string mnozstviPredvyplnit(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow, BaseCode code)
		{
			string kodInit = string.Empty;

			if ((code is ICodeWeight) && ((ICodeWeight)code).Weight.HasValue)
			{
				decimal weight = ((ICodeWeight)code).Weight.Value;

				kodInit =
					(weight
					/ (PERow.IsWEIGHTNull() || (PERow.WEIGHT == 0) ? 1 : PERow.WEIGHT)
					/ ((PERow.QTYPACK == 0) ? 1 : PERow.QTYPACK)
					).ToString(Settings.UIFormatDesCisel);
			}
			if ((code is ICodeQuantity) && ((ICodeQuantity)code).Quantity.HasValue)
			{
				kodInit = ((ICodeQuantity)code).Quantity.Value.ToString(Settings.UIFormatDesCisel);
			}
			else if (prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT)
			{
				if (prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA)
					kodInit = 1.ToString(Settings.UIFormatDesCisel);
				else if (prijemDataParametry.Parametry[0].CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI)
				{
					decimal mn = ((PERow.Zbyva) / (PERow.QTYPACK > 0 ? PERow.QTYPACK : 1));
					if (mn > 0)
						kodInit = mn.ToString(Settings.UIFormatDesCisel);
				}
			}

			return kodInit;
		}

		#endregion

		#region Tisk palety

		public void TiskPaleta(Paleta paleta)
		{
			try
			{
				ScannerStop();


				if (!MST_Global.PovolitPrintServer)
					return;

				//if (!Settings.Vydej_Baleni_Tisk_Enable)
				//    return;

				if (Paleta == null)
					Paleta = new Paleta();

				DialogResult dr = MessageBoxBig.Show("Tisknout Potisk palety?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
				if (dr == DialogResult.No)
				{
					return;
				}

				bool vytisteno = false;

				//  tisk soupisu
				Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
				List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
				Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

				dataHlavicka.Add("00", paleta.sscc.Trim());

				#region JiS : doplneni rozmeru a vahy palety (baliku) do hlavicky k tisku ...

				#region Rozmery baliku jako text prozatim...

				#region Sirka
				string dimensionSirka = Settings.Vydej_dimensionSirka;
				do
				{
					try
					{
						using (SejmiKodForm kod = new SejmiKodForm("Zadejte šířku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionSirka, false))
						{
							if (kod.ShowDialog() == DialogResult.Cancel)
								return;
							dimensionSirka = kod.Kod;
						}

						break;
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(ex);
						continue;
					}

				} while (true);

				Settings.Vydej_dimensionSirka = dimensionSirka;
				dataHlavicka.Add("DimensionWidth", dimensionSirka);
				#endregion

				#region Vyska
				string dimensionVyska = Settings.Vydej_dimensionVyska;
				do
				{
					try
					{
						using (SejmiKodForm kod = new SejmiKodForm("Zadejte výšku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionVyska, false))
						{
							if (kod.ShowDialog() == DialogResult.Cancel)
								return;
							dimensionVyska = kod.Kod;
						}

						break;
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(ex);
						continue;
					}

				} while (true);

				Settings.Vydej_dimensionVyska = dimensionVyska;
				dataHlavicka.Add("DimensionHeight", dimensionVyska);
				#endregion

				#region Hloubka
				string dimensionHloubka = Settings.Vydej_dimensionHloubka;
				do
				{
					try
					{
						using (SejmiKodForm kod = new SejmiKodForm("Zadejte hloubku", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, true, dimensionHloubka, false))
						{
							if (kod.ShowDialog() == DialogResult.Cancel)
								return;
							dimensionHloubka = kod.Kod;
						}

						break;
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(ex);
						continue;
					}

				} while (true);

				Settings.Vydej_dimensionHloubka = dimensionHloubka;
				dataHlavicka.Add("DimensionDepth", dimensionHloubka);
				#endregion


				#endregion

				#region Brutto Vaha (Gross weight)

				string gross_weight = Settings.Vydej_gross_weight;
				decimal gross_weight_decimal = 0;

				do
				{
					try
					{

						using (SejmiKodForm kod = new SejmiKodForm("Zadej Brutto váhu", SejmiKodForm.TypeOfCode.Numeric, 0, false, false, gross_weight, false))
						{
							if (kod.ShowDialog() == DialogResult.Cancel)
								return;

							gross_weight = kod.Kod;
							gross_weight_decimal = decimal.Parse(gross_weight);
						}

						break;
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle(ex);
						continue;
					}

				} while (true);

				Settings.Vydej_gross_weight = gross_weight.Trim();
				dataHlavicka.Add("GrossWeight", gross_weight_decimal.ToString("0.000", System.Globalization.NumberFormatInfo.InvariantInfo));

				#endregion

				#endregion

				Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dtsi = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable();

				#region Foreach Predloha

				foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskRow drsi in dtsi)
				{
					string SERLTNUMSeznam = string.Empty;
					string LLSN = string.Empty;

					Dictionary<string, string> dataRadek = new Dictionary<string, string>();

					string itemdesc = "";

					dataRadek.Add("ITEMDESC", itemdesc);

					dataRadek.Add("CountEntries", drsi.CountEntries.ToString());
					dataRadek.Add("SOPNUMBE", drsi.SOPNUMBE.Trim());
					dataRadek.Add("ITEMNMBR", drsi.IsITEMNMBRNull() ? string.Empty : drsi.ITEMNMBR.Trim());
					//dataRadek.Add("ORD", drsi.ORD.ToString());
					//dataRadek.Add("VNDDOCNM", drsi.IsVNDDOCNMNull() ? string.Empty : drsi.VNDDOCNM.Trim());
					dataRadek.Add("VNDITNUM", drsi.IsVNDITNUMNull() ? string.Empty : drsi.VNDITNUM.Trim());
					dataRadek.Add("CZ_CarKod", drsi.IsCZ_CarKodNull() ? string.Empty : drsi.CZ_CarKod.Trim());
					//dataRadek.Add("LOCNCODE", drsi.IsLOCNCODENull() ? string.Empty : drsi.LOCNCODE.Trim());
					dataRadek.Add("QTYSHPPD", drsi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					//dataRadek.Add("QTYPACK", drsi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
					//dataRadek.Add("SERLTNUM", drsi.SERLTNUM.Trim());
					//dataRadek.Add("KOD_SW", drsi.IsKOD_SWNull() ? string.Empty : drsi.KOD_SW.Trim());
					//dataRadek.Add("DAT_VYROBY", drsi.IsDAT_VYROBYNull() ? string.Empty : drsi.DAT_VYROBY.Trim());
					//dataRadek.Add("REZ_1", drsi.IsREZ_1Null() ? string.Empty : drsi.REZ_1.Trim());
					//dataRadek.Add("ODBER_ID", drsi.IsODBER_IDNull() ? string.Empty : drsi.ODBER_ID.Trim());
					//dataRadek.Add("DATEDONE", drsi.IsDATEDONENull() ? string.Empty : drsi.DATEDONE.Trim());
					//dataRadek.Add("TIMEDONE", drsi.IsTIMEDONENull() ? string.Empty : drsi.TIMEDONE.Trim());
					dataRadek.Add("USER_ID", drsi.USER_ID.ToString());
					//dataRadek.Add("DEX_ROW_ID", drsi.DEX_ROW_ID.Trim());
					//dataRadek.Add("guid", drsi.guid.Trim());
					dataRadek.Add("TYPEPAL", drsi.IsTYPEPALNull() ? string.Empty : drsi.TYPEPAL.Trim());
					dataRadek.Add("NMBRPAL", drsi.IsNMBRPALNull() ? string.Empty : drsi.NMBRPAL.Trim());
					//dataRadek.Add("PRINTED", drsi.PRINTED.ToString());
					//dataRadek.Add("REZ_2", drsi.IsREZ_2Null() ? string.Empty : drsi.ITEMNMBR.Trim());
					//dataRadek.Add("INPUT_MODE", drsi.INPUT_MODE.ToString());
					dataRadek.Add("ID_TERMINAL", drsi.ID_TERMINAL.ToString());
					dataRadek.Add("SKL_ID", drsi.IsSKL_IDNull() ? string.Empty : drsi.SKL_ID.Trim());
					dataRadek.Add("MJ", drsi.IsMJNull() ? string.Empty : drsi.MJ.Trim());

					dataRadky.Add(dataRadek);
				}
				#endregion



				vytisteno = PrijemTisk.PrintPaletaSendToPrinter(dataHlavicka, dataRadky, dataPaticka, null);

			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
			finally
			{
				ScannerStart();
			}
		}

		private void TiskEtiketaPalListek(string ponumber)
		{
			try
			{
				if (!MST_Global.PovolitPrintServer)
					return;

				ScannerStop();

				while (true)
				{
					//Vytahne se onlinem informace o detailu objednavky,
					//prida se id uzivatele a jmeno, kdo vychystava
					// TODO : co dalsiho a jak ? aktualne pro ICT-NEKUPTO
					try
					{

						//vytahne cislo objednavky z 1. zaznamu
						// TODO : co kdyz jich je vic????
						DataSet ds = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Detail(ponumber.Trim(), "");

						Dictionary<string, string> data = new Dictionary<string, string>();
						//globalni parametry
						data.Add("UserID", MST_Global.UserID.ToString());
						data.Add("UserLoginName", MST_Global.UserLoginName);
						//vracene hodnoty z online
						foreach (DataColumn dcol in ds.Tables[0].Columns)
						{
							string key = dcol.ColumnName;
							string value = ds.Tables[0].Rows[0][dcol].ToString();
							if (!data.ContainsKey(key))
								data.Add(key, value);
						}

						//TODO : nejake pocty dat a dalsi podrobnosti o vydejovych datech ???

						//bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPalListek);
						bool vytisteno = PrijemTisk.Print(data, PrinterFactory.PrinterModules.PrijemPaletovylistek);
						//Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", listPolozekVydej.ListPolozek[0].CountEntries, listPolozekVydej.ListPolozek[0].SOPNUMBE, null, vytisteno.ToString(), null));

						MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3TiskPaletovehoListkuDokoncen, ponumber.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
						return;
					}
					catch (Exception ex)
					{
						Logging.Log.Write(ex);
						DialogResult dr = MessageBoxBig.Show(ex.Message + "\n\n" + Fask.Localization.Localization.Vydej3ListPolozek3OpakovatTiskDokladu + "'" + ponumber.Trim() + "'?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
						if (dr == DialogResult.No)
							return;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}
			finally
			{
				if (MST_Global.PovolitPrintServer)
					ScannerStart();
			}
		}

		
		#endregion

	}
}
