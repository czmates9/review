using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.Forms;
using System.Linq;

namespace Fask.MST_W.Prodej_3
{
    public partial class ProdejMain : System.Windows.Forms.Form
    {
        internal GlobalObject globalObject = new GlobalObject();
        internal static ProdejMain prodejInstance = null;

		#region Eventy formu

		public ProdejMain()
		{
			InitializeComponent();

			this.Text = MST_Global.ProdejName;

			prodejInstance = this;
		}

		private void ProdejMain_Activated(object sender, EventArgs e)
		{
			// aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
			Components.KeyboardManager.LoadDefaultKeyboardMode();
		}

		private void ProdejMain_Deactivate(object sender, EventArgs e)
		{
			// deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
			Components.KeyboardManager.SaveDefaultKeyboardMode();
		}

		private void ProdejMain_Closing(object sender, CancelEventArgs e)
		{
			// prepnuti modu klavesnice do neznameho tak, aby v jinem formulari nedoslo k samovolne zmene modu
			Components.KeyboardManager.defaultKeyboardMode = Fask.MST_W.Components.KeyboardManager.KeyboardMode.Unknown;
		}

		private void ProdejMain_Load(object sender, EventArgs e)
		{
			// nacteni lokalizace ze souboru
			Fask.Localization.LocalizationExtensionForm.Localize(this);

			this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
			this.Size = Forms.FormLocation.ScreenResolution;
			//this.Location = new Point(0, 0);

			timerLoad.Enabled = true;

			//TaD Tady automaticky konfiguracne sputit otevreni davky bez nutnosti mackat cudlik

			//if (Settings.uia_prodej_novaDavka && MST_Global.Automatika_FirtsRun)
			//{
			//    otevriDavku();

			//}
			//else
			//{
			//    MST_Global.Automatika_FirtsRun = false;
			//}
		}

		private void ProdejMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				PerformOK();
			}
			else if (e.KeyCode == Keys.D1)
			{
				buttonDavka_Click(null, null);
			}
			else if (e.KeyCode == Keys.D2)
			{
				buttonOdeslatDavku_Click(null, null);
			}
			else if (e.KeyCode == Keys.D3)
			{
				stahnoutOdberatele();
			}
			else if (e.KeyCode == Keys.D4)
			{
				stahnoutZbozi();
			}
			else if (e.KeyCode == Keys.D5)
			{
				stahnoutStrediska();
			}
			else if (e.KeyCode == Keys.D6)
			{
				stahnoutTypDokladu();
			}
			else if (e.KeyCode == Keys.D7)
			{
				stahnoutSklady();
			}
			else if (e.KeyCode == Keys.D8)
			{
				stahnoutPracovniky();
			}
			else
			{
				return;
			}

			e.Handled = true;
		}

		private void Prodej_Main_Shown(object sender, EventArgs e)
		{
			timerLoad.Enabled = false;

			try
			{
				// inicializace global 
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, Fask.Localization.Localization.Prodej3ProdejMainChyba, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
				Close();
			}

			Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

			//Nacteni globalni konfigurace prodeje
			try
			{
				Prodej.Globals.Load(Main.ConfigModulesFileName);

				//Toto nemuze byt takto reseno, protoze muze rozhodovat az typ pouziteho dokladu ...
				// z testu upravy pro LITRA 18.1.2013 - JiS
				//this.buttonCiselikSkladu.Enabled = Prodej.Globals.FiltrCiselnikSkladu;
				//this.buttonStahnoutOdberatele.Enabled = Prodej.Globals.Odberatel;
				//this.buttonStahnoutStrediska.Enabled = Prodej.Globals.Strediska;
				//this.buttonStahnoutTypDokladu.Enabled = Prodej.Globals.TypDokladu;
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message);
			}

			// TODO : jak poresit ???
			//buttonStahnoutTypDokladu.Visible = buttonStahnoutTypDokladu.Enabled = Prodej.Globals.TypDokladu;
			//buttonStahnoutStrediska.Visible = buttonStahnoutStrediska.Enabled = Prodej.Globals.Strediska;

			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region Konec formu

		private void buttonKonec_Click(object sender, EventArgs e)
		{
			PerformOK();
		}

		private void PerformOK()
		{
			if (Prodej.Globals.ProdejDialogOpusteniModulu)
			{
				if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainUkonceniPraceSModulemDotaz, MST_Global.ProdejName, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.No)
					return;
			}

			this.finalize();

			DialogResult = DialogResult.OK;
		}

		private void buttonKonec_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonKonec_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void finalize()
		{
			if (this.globalObject != null)
				this.globalObject.Dispose();
			this.globalObject = null;
			prodejInstance = null;
		}

		#endregion

		#region Pomocne metody

		/// <summary>
		/// Generovani SN.
		/// </summary>
		/// <returns>Vraci soucasny den v roce - 1 den</returns>
		private string generovatSN()
		{
			try
			{
				// TODO: tvorba knihovny pro generovani
				int day = DateTime.Now.DayOfYear;
				return DateTime.Now.ToString("yy") + day.ToString();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			}

			return string.Empty;
		}

		#endregion

		#region Otevøi dávku

		private void buttonDavka_Click(object sender, EventArgs e)
		{
			try
			{
				otevriDavku();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		private void otevriDavku()
		{
			try
			{
				// dotaz na aktualizaci ciselniku zbozi
				if (Prodej.Globals.AktualizaceZboziPredVyberemDavky)
				{
					if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceCiselnikuZboziDotaz, MST_Global.ProdejName, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.Yes)
						stahnoutZbozi();
				}

				using (ProdejVyberDavky pvd = new ProdejVyberDavky())
				{
					pvd.HesloProOtevreniRozpracovaneDavky = Prodej.Globals.PozadovatHesloProOtevreniRozpracovaneDavky;
					if (pvd.ShowDialog() == DialogResult.Cancel)
						return;

					this.globalObject.Davka = pvd.CisloDavky;
				}

				#region 1.Existujici zaznam z DI

				string di_docid = null;
				string di_docid2 = null;
				string di_odbid = null;
				string di_sklid = null;
				string di_strid = null;
				string di_sklid_dest = null;
				string di_serltnum = null;

				var di_first_row = this.globalObject.controller_prodej.CZMST_DI_GetFirstRecord();
				if (di_first_row != null)
				{
					di_docid = di_first_row.IsDOC_IDNull() ? string.Empty : di_first_row.DOC_ID.Trim();
					di_docid2 = di_first_row.IsDOC_ID2Null() ? string.Empty : di_first_row.DOC_ID2.Trim();
					di_odbid = di_first_row.IsODB_IDNull() ? string.Empty : di_first_row.ODB_ID.Trim();
					di_sklid = di_first_row.IsSKL_IDNull() ? string.Empty : di_first_row.SKL_ID.Trim();
					di_strid = di_first_row.IsSTR_IDNull() ? string.Empty : di_first_row.STR_ID.Trim();
					di_sklid_dest = di_first_row.IsSKL_ID_DESTNull() ? string.Empty : di_first_row.SKL_ID_DEST.Trim();
					// 6.6.2016 PeV: neprebira se, mohlo by byt vice sarzi a pak by byl zmatek, ktera je zvolena ...
					//di_serltnum = di_first_row.isserltnumnull() ? string.Empty : di_first_row.SERLTNUM.Trim();
				}
				#endregion

				#region Typ Dokladu

				Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row typdokladu = null;
				if (Prodej.Globals.TypDokladu)
				{
					using (ProdejVyberTypuDokladu ptd = new ProdejVyberTypuDokladu(this.globalObject.Davka.Value))
					{
						if (ptd.ShowDialog() == DialogResult.Cancel)
							return;
						typdokladu = ptd.TypDokladu;
						if (typdokladu == null)
							return;
					}
				} 

				#endregion

				#region cfg_sn_na_davku

				// zadani sarze na davku
				// zobrazit SN, pripadne vygenerovat?? ...
				if (typdokladu != null && !typdokladu.Iscfg_sn_na_davkuNull() && typdokladu.cfg_sn_na_davku > 0 && string.IsNullOrEmpty(di_serltnum))
				{
					// vygenerovani, pokud je povoleno, jinak rucne zadat
					if (!typdokladu.Iscfg_generovat_snNull() && typdokladu.cfg_generovat_sn > 0)
					{
						di_serltnum = generovatSN();
					}

					using (SejmiKodForm skf = new SejmiKodForm())
					{
						skf.Popis = "Šarže";
						skf.Text = "Zadaní šarže";
						skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
						//skf.MaxLength = Globals.LOCNCODE_LEN;
						//skf.Len = Globals.LOCNCODE_LEN;
						//skf.CheckLen = true;
						skf.AllowEmpty = false;
						skf.Kod = string.IsNullOrEmpty(di_serltnum) ? string.Empty : di_serltnum.Trim();

						if (skf.ShowDialog() == DialogResult.Cancel)
							return;

						di_serltnum = skf.Kod;
					}
				} 

				#endregion

				#region cfg_odb

				Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
				if ((typdokladu != null && typdokladu.cfg_odb > 0) || Prodej.Globals.Odberatel)
				{
					using (ProdejVyberOdberatele po = new ProdejVyberOdberatele(typdokladu, this.globalObject.Davka.Value))
					{
						if (po.ShowDialog() == DialogResult.Cancel)
							return;

						odberatel = po.Odberatel;

						if (odberatel == null)
						{
							Logging.Log.Write("Není vybrán odbìratel, pøestože je vyžadován!");
							return;
						}
					}
				} 

				#endregion

				#region mena_ID

				//parametry hlavicky ...
				string zakazkaID = string.Empty;
				string paletaID = string.Empty;
				string skladID = string.Empty;
				Fask.SQLiteDBs.DataSets.Meny.CZMST097Row mena = null;

				if (odberatel != null && !odberatel.Ismena_IDNull() && odberatel.mena_ID.Trim().Length > 0)
				{
					Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt = this.globalObject.controller_meny.GetDataByMenaID(odberatel.mena_ID);
					if (dt.Count > 0)
						mena = dt[0];
					else
					{ // vychozi menu nastavit ... ???
						//dt = meny_ta.GetData();
						//var linqHlavniMena = dt.Where(m => m.mena_hlavni);
						//if (linqHlavniMena.Count() > 0)
						//{
						//    mena = linqHlavniMena.First();
						//}                        

						//dt = meny_ta.GetDataByHlavni(true);
						//if (dt.Count > 0)
						//{
						//    mena = dt[0]; //nastavena hlavni mena ...
						//}
					}

					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();

					if (dih_dt.Count > 0)
					{
						zakazkaID = dih_dt[0].Zakazka_ID;
						paletaID = dih_dt[0].Paleta_ID;
						skladID = dih_dt[0].SKL_ID;
						this.globalObject.controller_prodej.DeleteQuery_DIH();
					}
					this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);

				} 

				#endregion

				#region cfg_mena_id

				if (typdokladu != null && !typdokladu.Iscfg_mena_idNull() && typdokladu.cfg_mena_id > 0 && mena == null)
				{
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();

					using (ProdejVyberMeny po = new ProdejVyberMeny())
					{
						if (dih_dt.Count > 0)
							po.SelectedMenaID = dih_dt[0].mena_ID;

						if (po.ShowDialog() == DialogResult.Cancel)
							return;

						if (po.bezCiziMeny)
							mena = null;
						else
							mena = po.SelectedMena;

						//if (mena == null)
						//{
						//    Logging.Log.Write("Není vybrána mìna, pøestože je vyžadována!");
						//    return;
						//}
					}

					if (dih_dt.Count > 0)
					{
						zakazkaID = dih_dt[0].Zakazka_ID;
						paletaID = dih_dt[0].Paleta_ID;
						skladID = dih_dt[0].SKL_ID;
						this.globalObject.controller_prodej.DeleteQuery_DIH();
					}

					this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);

				} 

				#endregion

				#region skladZdroj

				Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladZdroj = null;

				// vyber zdrojoveho skladu
				//if (Prodej.Globals.FiltrCiselnikSkladu)  // PeV - 25.9.2015 uprava, povoleni skladu a filtr na sklady je zvlast
				if ((typdokladu != null && !typdokladu.Iscfg_skladyNull() && typdokladu.cfg_sklady > 0) || Prodej.Globals.PouzitSklady)
				{
					// SKLAD_ID je nastaven -> dohleda se sklad z ciselniku skladu (pokud nenalezeno, probehne vyber)
					// SKLAD_ID neni nastaven a neexistuje vybrany sklad -> zobrazi se ciselnik skladu a bude se prenaset skl_id z 095
					// SKLAD_ID neni nastaven a existuje vybrany sklad -> dohleda se z davky (spatne popsano, existuje only one??)

					// ma se vyuzit id zadaneho skladu
					// id skladu vyplneno v konfiguraci aplikace
					if (!string.IsNullOrEmpty(Prodej.Globals.SkladID))
					{
						try
						{
							Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(Prodej.Globals.SkladID);
							if (dt_sklady.Count > 0)
								skladZdroj = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, Prodej.Globals.SkladID.Trim()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Logging.Log.Write(ex);
							MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						}
					}

					// sklad nacten z jiz nasnimanych dat
					if (skladZdroj == null && di_sklid != null && di_sklid != string.Empty && Prodej.Globals.FiltrCiselnikSkladuOnlyOne)
					{
						try
						{
							Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(di_sklid);
							if (dt_sklady.Count > 0)
								skladZdroj = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Logging.Log.Write(ex);
							MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						}
					}

					// predvyplneno id skladu (SKL_ID) v typu dokladu
					if (skladZdroj == null && !typdokladu.IsSKL_IDNull() && !string.IsNullOrEmpty(typdokladu.SKL_ID.Trim()))
					{
						try
						{
							Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(typdokladu.SKL_ID.Trim());
							if (dt_sklady.Count > 0)
								skladZdroj = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Logging.Log.Write(ex);
							MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						}
					}

					if (skladZdroj == null)
					{
						using (Forms.FormSkladVyber fsv = new FormSkladVyber())
						{
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							skladZdroj = fsv.Sklad;

							if (skladZdroj == null)
							{
								Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
								return;
							}
						}
					}
				} 

				#endregion

				#region skladCil

				Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladCil = null;
				// zadani ciloveho skladu
				if (typdokladu != null && !typdokladu.Iscfg_skl_id_destNull() && typdokladu.cfg_skl_id_dest > 0)
				{
					// sklad se ma prevzit ze zdrojoveho skladu
					if (!typdokladu.Iscfg_skl_id_dest_prevzitNull() && typdokladu.cfg_skl_id_dest_prevzit > 0 && skladZdroj != null)
						skladCil = skladZdroj;

					// sklad nacten z jiz nasnimanych dat
					if (skladCil == null && di_sklid_dest != null && di_sklid_dest != string.Empty && Prodej.Globals.FiltrCiselnikSkladuOnlyOne)
					{
						try
						{
							Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(di_sklid_dest);
							if (dt_sklady.Count > 0)
								skladCil = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladCilNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Logging.Log.Write(ex);
							MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						}
					}

					// predvyplneno id skladu (SKL_ID) v typu dokladu
					if (skladCil == null && !typdokladu.Ispredvyplnit_locncodedestNull() && !string.IsNullOrEmpty(typdokladu.predvyplnit_skl_id_dest.Trim()))
					{
						try
						{
							Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = this.globalObject.controller_sklady.GetDataBySkl_id(typdokladu.predvyplnit_skl_id_dest.Trim());
							if (dt_sklady.Count > 0)
								skladCil = dt_sklady[0];
							else
							{
								MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainSkladCilNenalezenVyberteJiny, di_sklid), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
							}
						}
						catch (Exception ex)
						{
							Logging.Log.Write(ex);
							MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
							return;
						}
					}

					if (skladCil == null)
					{
						using (Forms.FormSkladVyber fsv = new FormSkladVyber())
						{
							fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberCilovehoSkladu;
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							skladCil = fsv.Sklad;

							if (skladCil == null)
							{
								Logging.Log.Write("Není vybrán cílový sklad, pøestože je vyžadován!");
								return;
							}
						}
					}
				}

				#endregion

				#region cfg_prevod_sklad

				if ((typdokladu != null && typdokladu.cfg_prevod_sklad > 0) || Prodej.Globals.PovolitPrevodMeziSklady)
				{
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();
					DialogResult dresult = DialogResult.None;
					if (dih_dt.Count > 0)
					{
						zakazkaID = dih_dt[0].Zakazka_ID;
						skladID = dih_dt[0].SKL_ID;
						paletaID = dih_dt[0].Paleta_ID;
						dresult = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainCilovySkladDriveZvolenPouzitStejnyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
					}

					if (dresult != DialogResult.Yes)
					{
						using (Forms.FormSkladVyber fsv = new FormSkladVyber())
						{
							fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberteCilovySklad;  // "Vyberte cílový sklad";
							if (fsv.ShowDialog() == DialogResult.Cancel)
								return;

							if (fsv.Sklad == null)
							{
								Logging.Log.Write("Není vybrán cílový sklad, pøestože je vyžadován!");
								return;
							}
							else
							{
								skladID = fsv.Sklad.skl_id;
							}
						}
					}

					if (dih_dt.Count > 0)
						this.globalObject.controller_prodej.DeleteQuery_DIH();
					this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);
				} 

				#endregion

				#region cfg_zakazka_id

				if (typdokladu != null && typdokladu.cfg_zakazka_id > 0)
				{
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();

					if (dih_dt.Count > 0)
					{
						zakazkaID = dih_dt[0].Zakazka_ID;
						skladID = dih_dt[0].SKL_ID;
					}
					if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloZakazky, zakazkaID, out zakazkaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
					{
						if (dih_dt.Count > 0)
						{
							paletaID = dih_dt[0].Paleta_ID;
							this.globalObject.controller_prodej.DeleteQuery_DIH();
						}

						this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);

						//zakazkaID  davkaprodej = value;
					}
					else
					{
						return;
					}
				} 

				#endregion

				#region cfg_paleta_id

				if (typdokladu != null && typdokladu.cfg_paleta_id > 0)
				{
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();

					if (dih_dt.Count > 0)
					{
						paletaID = dih_dt[0].Paleta_ID;
						skladID = dih_dt[0].SKL_ID;
					}

					if (InputBox.Show(Fask.Localization.Localization.Prodej3ProdejMainZadejteCisloPalety, paletaID, out paletaID, true, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric) == DialogResult.OK)
					{
						if (dih_dt.Count > 0)
						{
							zakazkaID = dih_dt[0].Zakazka_ID;
							this.globalObject.controller_prodej.DeleteQuery_DIH();
						}

						this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);
					}
					else
					{
						return;
					}
				} 

				#endregion

				using (ProdejList prodejlist = new ProdejList(this.globalObject.Davka.Value, odberatel, typdokladu, skladZdroj, skladCil, di_strid, mena, di_serltnum))
				{
					prodejlist.Zobrazeni = ProdejList.ZobrazeniTyp.List;
					prodejlist.ShowDialog();
				}

				if (MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainOdeslatDavkuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
					== DialogResult.Yes)
				{
					odeslatDavku(this.globalObject.Davka.Value);
				}
			}
			catch (Exception ex)
			{
				Logging.Log.Write("otevriDavku:");
				Logging.Log.Write(ex);
			}
			finally
			{
				this.globalObject.Davka = null;
			}

		}

		private void buttonDavka_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonDavka_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		#endregion

		#region Odeslat dávku

		private void buttonOdeslatDavku_Click(object sender, EventArgs e)
		{
			try
			{
				odeslatDavku();
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		private void odeslatDavku()
		{
			try
			{
				using (ProdejVyberDavky pvd = new ProdejVyberDavky(false))
				{
					if (pvd.ShowDialog() == DialogResult.Cancel)
						return;
					this.globalObject.Davka = pvd.CisloDavky;
				}
				odeslatDavku(this.globalObject.Davka.Value);
			}
			catch (Exception ex)
			{
				Logging.Log.Write(ex);
			}
		}

		private void buttonOdeslatDavku_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonOdeslatDavku_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

        private void odeslatDavku(int cislodavkykodeslani)
        {
			try
			{

            int cd = cislodavkykodeslani;
            
            int pocetpolozek = this.globalObject.controller_prodej.CZMST_DI_Count();
            if (pocetpolozek <= 0)
            {
                MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaNeobsahujePolozky, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

         
				//Fask.MST_W.Program.mstw.mbw.BeginPracujiForm(string.Format(Fask.Localization.Localization.Prodej3ProdejMainOdesilamDavku, cd.ToString()));
                bool result = ProdejServiceOperations.SendData(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_prodej, cd);
				//Program.mstw.mbw.EndPracujiForm();

                if (result)
                {
                    if (Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky)
						MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaOdeslana, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                }
                else
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejMainDavkaSeNepodarilaOdeslat, cd.ToString()), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                }
            }
            catch (Exception ex)
            {
				//Program.mstw.mbw.EndPracujiForm();
                MessageBox.Show(ex.Message, Fask.Localization.Localization.Prodej3ProdejMainChybaPriOdeslaniDavky, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
		}
		
		#endregion

		#region Konfigurace

		private void buttonKonfigurace_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				Fask.MST_W.ConfigurationService.dsCZMSTCFG czmstcfg = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_configuration.GetCZMSTCFG();

				//Vytahnuti konfiguracnich parametru prodeje a nastaveni/prenastaveni konfigurace...
				czmstcfg.CZMSTCFG.PrimaryKey = new DataColumn[] { czmstcfg.CZMSTCFG.PNAMEColumn };
				Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow cfgrow = null;

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena0SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price0IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena1SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price1IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena2SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price2IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena3SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price3IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena4SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price4IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejCena5SDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.Price5IsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejVystupCenaSDani") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.PriceIsWithTax = (cfgrow.PVALUEBL == 1);

				cfgrow = czmstcfg.CZMSTCFG.Rows.Find("ProdejVystupCenaSDaniPovoleno") as Fask.MST_W.ConfigurationService.dsCZMSTCFG.CZMSTCFGRow;
				if (cfgrow != null && !cfgrow.IsPVALUEBLNull())
					Prodej.Globals.PriceIsWithTaxEnable = (cfgrow.PVALUEBL == 1);

				Prodej.Globals.Save(Main.ConfigModulesFileName);

				Cursor.Current = Cursors.Default;

				MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceDokoncena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
			}
			catch (Exception ex)
			{
				Cursor.Current = Cursors.Default;
				Logging.Log.Write(ex);
				MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainAktualizaceSeNezdarila, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
			}
		}
		
		#endregion

		#region Odberatele

		private void buttonStahnoutOdberatele_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonStahnoutOdberatele_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void buttonStahnoutOdberatele_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportOdberateluDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatOdberatele();
			}

			stahnoutOdberatele();
		}

		private void exportovatOdberatele()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberateleExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void stahnoutOdberatele()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberatele(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		#endregion

		#region Zboži

		private void buttonStahnoutZbozi_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonStahnoutZbozi_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void buttonStahnoutZbozi_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportZboziDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatZbozi();
			}

			stahnoutZbozi();
		}

		private void exportovatZbozi()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZboziExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void stahnoutZbozi()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZbozi(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		#endregion

		#region Strediska

		private void buttonStahnoutStrediska_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonStahnoutStrediska_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void buttonStahnoutStrediska_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportStredisekDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatStrediska();
			}

			stahnoutStrediska();
		}

		private void exportovatStrediska()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediskaExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void stahnoutStrediska()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediska(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		#endregion

		#region TypDokladu

		private void stahnoutTypDokladu()
		{
			try
			{
				if (Prodej.Globals.TypDokladu)
					_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogTypDokladu(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
				else  // prace s typy dokladu neni povolena
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainPraceSTypyDokladuNeniPovolena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}
		
		private void buttonStahnoutTypDokladu_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonStahnoutTypDokladu_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void buttonStahnoutTypDokladu_Click(object sender, EventArgs e)
		{
			stahnoutTypDokladu();
		}

		#endregion

		#region Sklady

		private void buttonCiselikSkladu_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatSklady();
			}

			stahnoutSklady();
		}

		private void buttonCiselikSkladu_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonCiselikSkladu_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void exportovatSklady()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void stahnoutSklady()
		{
			try
			{
				// TODO : proc neni povoleno ... ??? je to blby, i kdyz nejsou filtry na sklady, tak byto chtelo mit sklady k dispozici pro zobrazeni informaci o nazvech skladu ...
				//if (Prodej.Globals.FiltrCiselnikSkladu)
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
				//else
				//    MessageBoxBig.Show("Filtry na èísla skladù nejsou povoleny", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		
		#endregion

		#region Pracovnici

		private void buttonStahnoutPracovniky_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportPracovnikuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatPracovniky();
			}

			stahnoutPracovniky();
		}

		private void exportovatPracovniky()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovniciExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void buttonStahnoutPracovniky_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.buttonStahnoutPracovniky_Click(null, null);
			}
			else
			{
				return;
			}
			e.Handled = true;
		}

		private void stahnoutPracovniky()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovnici(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, Prodej.Globals.SkladID);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		#endregion

		#region Meny

		private void buttonCiselnikMen_Click(object sender, EventArgs e)
		{
			if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportMenDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
			{
				exportovatMeny();
			}

			stahnoutMeny();
		}

		private void exportovatMeny()
		{
			try
			{
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMenyExport(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}

		private void stahnoutMeny()
		{
			try
			{
				if (Prodej.Globals.FiltrCiselnikMen)
					_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMen(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik);
				else
					MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainFiltryMenyNeniPovolen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}
		
		#endregion

		#region Lokace

		private void buttonStahnoutLokace_Click(object sender, EventArgs e)
		{
			stahnoutLokace();
		}

		private void stahnoutLokace()
		{
			try
			{
				// TODO: omezeni na urcity sklad/sklady??
				_WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogLokace(Prodej_3.ProdejMain.prodejInstance.globalObject.servis_ciselnik, string.Empty);

			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex);
			}
		}
		
		#endregion
    }
}