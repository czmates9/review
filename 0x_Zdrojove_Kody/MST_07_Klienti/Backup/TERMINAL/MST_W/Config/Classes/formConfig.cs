using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Fask.MST_W.Extensions;

namespace Fask.MST_W.Config
{
	public partial class formConfig
	{
		#region Tiskove šablony konfigurace

		//vazba typ tiskarny na sablony
		Dictionary<string, List<string>> printerTemplatesList = new Dictionary<string, List<string>>();


		private void printerTempatesFill(string printerType, ComboBox cmb2fill)
		{
			cmb2fill.BeginUpdate();
			cmb2fill.Items.Clear();
			List<string> templates = printerTemplatesList[printerType];
			foreach (var item in templates)
			{
				cmb2fill.Items.Add(item);
			}
			cmb2fill.EndUpdate();
		}

		#endregion

		#region Konfig_Load

		private void Kongif_Load_Servis()
		{
			// konfigurace modulu 
			cfgServisPovolit.Checked = MST_Global.ServisEnable;
			cfgServisZobrazitVMST.Checked = MST_Global.ServisShowInMST;
			cfgServisTimeoutOnline.Text = MST_Global.ServisTimeoutOnline.ToString();
			cfgServisTimeoutSynchronize.Text = MST_Global.ServisTimeoutSynchronize.ToString();
			cfgServisAutoUpdateInterval.Text = MST_Global.ServisAutoUpdateInterval.ToString();
			cfgServisPovolitFotoaparat.Checked = MST_Global.ServisAllowCamera;
			cfgServisDavkoveZpracovani.Checked = MST_Global.ServisDavkoveZpracovani;
			cfgServisUkonceniStavuNavrat.Checked = MST_Global.ServisUkonceniStavuNavrat;
			cfgServisPozadovatPotvrzeniZmenyCinnosti.Checked = MST_Global.ServisPozadovatPotvrzeniZmenyCinnosti;
			cfgServisPozadovatPotvrzeniZmenyStavu.Checked = MST_Global.ServisPozadovatPotvrzeniZmenyStavu;
			cfgServisPrehratZvukyPriPotvrzeniVoleb.Checked = MST_Global.ServisPrehratZvukPoVyberuMoznosti;
			cfgServisStatusBarZobrazitNazevStavu.Checked = MST_Global.ServisStatusBarZobrazitNazevStavu;
			cfgServisStatusBarZobrazitTypZdroje.Checked = MST_Global.ServisStatusBarZobrazitTypZdroje;
			cfgServisStatusBarIdZdroj.Checked = MST_Global.ServisStatusBarZobrazitIdZdroje;
			cfgServisStatusBarZobrazitOznaceniZdroje.Checked = MST_Global.ServisStatusBarZobrazitOznaceniZdroje;
			cfgServisZobrazitInfoOUkonceniStavu.Checked = MST_Global.ServisZobrazitInformaciOUkonceniStavu;
			cfgServisDavkaOtevritPoStazeni.Checked = MST_Global.ServisDavkaOtevritIhnedPoStazeni;
			cfgServisFiltrZobrazeniPovolit.Checked = MST_Global.ServisFiltrZobrazeniPovolit;
			cfgServisAutomatickaZmenaStavuPovolit.Checked = MST_Global.ServisAutomatickaZmenaStavuPovolit;
			cfgServisSynchZdrojePoVyberuPovolit.Checked = MST_Global.ServisSynchronizaceZdrojePoVyberuPovolit;
			cfgServisVyberOdberatele.Checked = MST_Global.ServisVyberOdberatelPovolit;
			cfgServisVyberOkruhu.Checked = MST_Global.ServisVyberOkruhPovolit;
			cfgServisOdeslaniDatNaPozadiPovolit.Checked = MST_Global.ServisOdeslaniDatNaPozadiPovolit;
			cfgServisOdeslaniDatPoUkonceniZadavaniPovolit.Checked = MST_Global.ServisOdeslaniDatPoUkonceniZadavaniPovolit;

			#region TaD Dialogy Servis

			chk_Servis_DialogOpusteniModulu.Checked = MST_Global.Servis_DialogOpusteniModulu;

			#endregion

		}

		private void Kongif_Load_Events()
		{
			eventsEnable.Checked = MST_Global.EventsEnable;
			eventsShowInMST.Checked = MST_Global.EventsShowInMST;
			eventsOnline.Checked = MST_Global.EventsOnline;
			eventsOnlineTimeout.Text = MST_Global.EventsOnlineTimeout.ToString();
			eventsOnlineConfirm.Checked = MST_Global.EventsOnlineConfirm;
			eventsSynchronizationInterval.Text = MST_Global.EventsSynchronizationInterval.ToString();

		}

		private void Kongif_Load_Expedice()
		{
			cfgExpediceAllow.Checked = MST_Global.Expedice;
			cfgExpediceShowInMain.Checked = MST_Global.ExpediceShowInMST;
			cfgExpediceName.Text = MST_Global.ExpediceName;

			// Expedice
			Expedice.Globals.Load(Main.ConfigModulesFileName);
			cfgExpediceSkladID.Text = Expedice.Globals.SkladID;
			cfgExpedicePovolitParsovaniCK.Checked = Expedice.Globals.PovolitParsovaniCK;
			cfgExpedicePovolitPredvyplneniMnozstvi.Checked = Expedice.Globals.PovolitPredvyplneniMnozstvi;
			cfgExpediceZobrazitZadaniSarzeJednou.Checked = Expedice.Globals.ZobrazitZadaniSarzePouzeJednou;
			cfgExpediceZobrazitMnozstviUParsKodu.Checked = Expedice.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu;
			cfgExpedicePovolitTiskPalet.Checked = Expedice.Globals.PovolitTiskPalet;
			cfgExpedicePovolitTiskSoupisu.Checked = Expedice.Globals.PovolitTiskSoupisu;
			#region TaD Dialogy Expedice

			chk_Expedice_DialogOpusteniModulu.Checked = Expedice.Globals.Expedice_DialogOpusteniModulu;

			#endregion
		}

		private void Kongif_Load_Ukoly()
		{
			cfgUkolyPovolit.Checked = MST_Global.TasksEnable;
			cfgUkolyZobrazitMST.Checked = MST_Global.TasksShowInMST;
			cfgUkolyTimeout.Text = MST_Global.TasksTimeout.ToString();
			cfgUkolySynchronizationInterval.Text = MST_Global.TasksSynchronizationInterval.ToString();
			cfgUkolyNotificationShowInterval.Text = MST_Global.TasksNotifyDialogShowSeconds.ToString();
		}

		private void Kongif_Load_Inventura()
		{
			cfgInventuraShowInMain.Checked = MST_Global.Inventura1ShowInMST;
			cfg_inventuraZadaniLokacePredSN.Checked = MST_Global.inventura1ZadaniLocncodePredSN;
			cfg_inventuraZadaniLokaceJednou.Checked = MST_Global.inventura1ZadaniLocncodeJednou;
			cfg_inventuraZadaniLokacePamatovatPosledni.Checked = MST_Global.inventura1ZadaniLocncodePamatovatPosledni;
			cfg_inventura1PolozkaNasnimatPouzeJednou.Checked = MST_Global.Inventura1PolozkaNasnimatPouzeJednou;
			chckInventura1PouzitCiselnikSkladu.Checked = MST_Global.Inventura1PouzitCiselnikSkladu;
			cfg_inventuraOnlineKontrola.Checked = MST_Global.Inventura1OnlineKontrola;
			cfgInventura1OnlineTimeout.Text = MST_Global.Inventura1OnlineTimeout.ToString();
			cfgInventura1ChunkTimeout.Text = MST_Global.Inventura1ChunkTimeout.ToString();
			cfgInventura1PolozkyVyberJenScannerem.Checked = MST_Global.Inventura1PolozkyVyberJenScannerem;
			cfgInventura1ParsovaniCarovehoKoduPovolit.Checked = MST_Global.Inventura1ParsovaniCarovehoKoduPovolit;
			cfgInventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit.Checked = MST_Global.Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit;
			cfgInventura1REZ1Nazev.Text = MST_Global.Inventura1REZ1Nazev;
			cfgInventura1REZ1IsNumber.Checked = MST_Global.Inventura1REZ1IsNumber;
			cfgInventura1REZ1Mandatory.Checked = MST_Global.Inventura1REZ1Mandatory;
			cfgInventura1REZ2Nazev.Text = MST_Global.Inventura1REZ2Nazev;
			cfgInventura1REZ2IsNumber.Checked = MST_Global.Inventura1REZ2IsNumber;
			cfgInventura1REZ2Mandatory.Checked = MST_Global.Inventura1REZ2Mandatory;

			Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu.Checked = MST_Global.Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu;

			#region TaD Dialogy Inventura 1
			chk_Inventura1_DialogOpusteniModulu.Checked = MST_Global.Inventura1_DialogOpusteniModulu;

			#endregion

		}

		private void Kongif_Load_Inventura2()
		{
			cfgInventura2ShowInMain.Checked = MST_Global.Inventura2ShowInMST;
			cfgInventura2DotazKancelar.Checked = MST_Global.Inventura2DotazKancl;
			cfgInventura2DotazLokace.Checked = MST_Global.Inventura2DotazLokace;
			cfgInventura2DotazOsoba.Checked = MST_Global.Inventura2DotazOsoba;
			cfgInventura2DotazStredisko.Checked = MST_Global.Inventura2DotazStredisko;

			#region TaD Dialogy Inventura 2

			chk_Inventura2_DialogOpusteniModulu.Checked = MST_Global.Inventura2_DialogOpusteniModulu;

			#endregion

		}

		private void Kongif_Load_Vydej()
		{
			cfgVydejLocationQuestion.Checked = MST_Global.VydejLocationQuestion;
			cfgVydejItemTypeQuestion.Checked = MST_Global.VydejItemTypeQuestion;
			cfgVydejTypyPaletNone.Checked = true;
			cfgVydejTypOznaceniPalety.Checked = MST_Global.VydejTypOznaceniPalety;
			cfgVydejTypyPoctyPalet.Checked = MST_Global.VydejTypyPoctyPalet;
			cfgVydejMaxDavek.Value = MST_Global.VydejMaxPocetDavek;
			cfgVydejPocetDavekZobraz.Value = MST_Global.VydejPocetDavekZobraz;
			cfg_povolitPreplneniPolozky.Checked = MST_Global.vydejPovolitPreplneniPolozky;
			cfg_VydejZadaniLokacePredSN.Checked = MST_Global.vydejZadaniLocncodePredSN;
			txt_vydejPrefix.Text = MST_Global.VydejSequence.ToString();
			cfgVydejLocationPouzitCiselnik.Checked = MST_Global.VydejLocationPouzitCiselnik;
			cfgVydejLocationFiltrovatData.Checked = MST_Global.VydejLocationFiltrovatData;
			cfgVydejLocationAllowAutocommit.Checked = MST_Global.VydejLocationAllowAutocommit;
			cfgVydejPokracovatNaJinemTerminalu.Checked = MST_Global.VydejPokracovatNaJinemTerminalu;
			chk_VydejHledaniCkAutoVyberPrvniNeuplne.Checked = MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne;
			chkVydejRozsireni1Rezerva2Zadavat.Checked = MST_Global.VydejRozsireni1Rezerva2Zadavat;
			txtVydejRozsireni1Rezerva2Nazev.Text = MST_Global.VydejRozsireni1Rezerva2Nazev;
			txtVydejRozsireni1Rezerva2Default.Text = MST_Global.VydejRozsireni1Rezerva2Default;
			cfgVydejZboziPouzitCiselnik.Checked = MST_Global.VydejZboziPouzitCiselnik;
			cfgVydejZboziJenScannerem.Checked = MST_Global.VydejZboziVyberJenScannerem;
			cfgVydejZboziVyhledavatDle.SelectedItem = MST_Global.VydejZboziVyhledaniDleSloupce;
			cfgVydejPovolitRazeniVydejek.Checked = MST_Global.VydejPovolitRazeniVydejek;
			cfgVydejStahovatPouzeNejvyssiPriority.Checked = MST_Global.VydejStahnoutNejvyssiPrioritu;
			cfgVydejSlucovaniDavek.Checked = MST_Global.VydejSlucovaniDavek;
			cfgVydejHromadneVyplneni.Checked = MST_Global.VydejHromadneVyplneni;
			cfgVydejParsovaniCarovehoKoduPovolit.Checked = MST_Global.VydejParsovaniCarovehoKoduPovolit;
			cfgVydejHromadneZadavaniSN.Checked = MST_Global.Vydej_HromadneSN;
			cfgVydejHromadneZadavaniSN_N.Text = MST_Global.Vydej_HromadneSN_N.ToString();

			#region TaD Dialogy Vydej

			chk_Vydej_DialogUspesnehoOdeslani.Checked = MST_Global.VydejDialogUspesnehoOdeslaniDavky;
			chk_Vydej_DialogOpusteniModulu.Checked = MST_Global.VydejDialogOpusteniModulu;
			chk_Vydej_DialogOpusteniVydejky.Checked = MST_Global.VydejDialogOpusteniVydejky;
			chk_Vydej_DialogNasnimaniPolozky.Checked = MST_Global.VydejTimeDialogNasnimana;
			tb_VydejInterval.Text = MST_Global.VydejTimeDialogInterval.ToString();
			chk_Vydej_DialogDavkaNenalezenaVygenerovat.Checked = MST_Global.VydejDialogDavkaNenalezenaVygenerovat;
			chk_Vydej_DialogNaDiskuNejsouDavkyStahnout.Checked = MST_Global.VydejDialogNaDiskuNejsouDavkyStahnout;

			#endregion

			// vydej sklady
			cfgVydejSkladPouzit.Checked = MST_Global.VydejSkladPouzit;
			cfgVydejSkladID.Text = MST_Global.VydejSkladID;
			cfgVydejPrevzitIDSkladZCisSklad.Checked = MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu;
			cfgVydejCiselnikSkladuOnlyOne.Checked = MST_Global.VydejFiltrCiselnikSkladuOnlyOne;
			cfgVydejGenPrikPozadovatSklad.Checked = MST_Global.VydejGenerovaniPrikazuZadatSklad;
			chckVydejObjednavkaDetailOnline.Checked = MST_Global.VydejObjednavkaDetail;
			chckVydejPocetKusuNaSkladeOnline.Checked = MST_Global.VydejPocetKusuNaSkladeOnline;
			chckVydejPocetKusuPolozkaOnline.Checked = MST_Global.VydejPocetKusuOnline;
			chckVydejPolozkaDetailOnline.Checked = MST_Global.VydejPolozkaDetailOnline;
			chckVydejPovolitZmenuOdberatele.Checked = MST_Global.VydejPovolitZmenuOdberatele;
			chckVydejGenerovatDataPrikazuOnline.Checked = MST_Global.VydejGenerovatDataPrikazuOnline;
			cfgVydejEtiketaTiskPoVlozeniDotaz.Checked = MST_Global.VydejEtiketaTiskPoVlozeniDotaz;
			cfgVydejPolozkyVyberJenScannerem.Checked = MST_Global.VydejPolozkyVyberJenScannerem;
			cfgVydejPovolitVyberTiskarnyDokladu.Checked = MST_Global.VydejVyberTiskarnyDokladu;
			cfgVydejPovolitVyberPracovnika.Checked = MST_Global.VydejVyberPracovnika;
			cfgVydejDavkyVyberJenScannerem.Checked = MST_Global.VydejDavkyVyberJenScannerem;
			cfgVydejPovolitKontrolaSNPredloha.Checked = MST_Global.VydejPovolitKontrolaSNPredloha;
			cfgVydejGenNenalezPrijumku.Checked = MST_Global.VydejGenerovatNenalezenouVydejku;
			cfgVydejPovolitOcipovani.Checked = MST_Global.VydejPovolitOcipovani;
			cfg_VydejRez1Cislo.Checked = MST_Global.VydejRez1Cislo;
			cfg_VydejRez1Povinne.Checked = MST_Global.VydejRez1Povinne;
			cfg_VydejRez1Pamatovat.Checked = MST_Global.VydejRez1Pamatovat;
			cfg_VydejRez2Cislo.Checked = MST_Global.VydejRez2Cislo;
			cfg_VydejRez2Povinne.Checked = MST_Global.VydejRez2Povinne;
			cfg_VydejRez2Pamatovat.Checked = MST_Global.VydejRez2Pamatovat;
			cb_VydejTiskVariantaSoupis.EnumForComboBox(typeof(Fask.MST_W.Vydej_3.Varianta_TiskSoupis));
			cb_VydejTiskVariantaSoupis.SelectedItem = MST_Global.VydejTiskVariantaSoupis.ToString();

            chkVydejItemsOnline.Checked = MST_Global.Vydej_Items_Online;
			chkVydej_FIFOFEFO_Online.Checked = MST_Global.Vydej_FIFOFEFO_Online;
			chkVydej_ExpiraceCheck_Online.Checked = MST_Global.Vydej_ExpiraceCheck_Online;
			chkVydej_TypSPrelokovanim.Checked = MST_Global.Vydej_TypSPrelokovanim;

			chkVydej_MnozstviAutoJedna.Checked = MST_Global.Vydej_MnozstviAutoJedna;

			cb_Vydej_Pokracovat_AnoNe.EnumForComboBox(typeof(Fask.MST_W.Vydej_3.Potvrzeni_AnoNe));
			cb_Vydej_Pokracovat_AnoNe.SelectedItem = MST_Global.Vydej_PokracovatJinyTerm_AnoNe.ToString();
			chk_Vydej_DialogTiskSoupis.Checked = MST_Global.VydejDialogTiskSoupis;
			chk_Vydej_DialogPokracovatNaJinemTerm.Checked = MST_Global.VydejDialogPokracovatNaJinemTerm;

			cb_Vydej_TiskSoupisMN_Auto1.Checked = MST_Global.VydejTiskSoupisMN_Auto1;

			#region F10 možnosti

			rbVydej_F10_ZobrazitAlternativyLokaci.Checked = MST_Global.F10_ZobrazitAlternativyLokaci;
			rbVydej_F10_TiskPalListku.Checked = MST_Global.F10_TiskPalListku;

			#endregion
		}

		private void Kongif_Load_Prijem()
		{
			cfgPrijemTimeDialog.Checked = MST_Global.PrijemTimeDialog;
			cfgPrijemTimeDialogInterval.Value = MST_Global.PrijemTimeDialogInterval;

			if (MST_Global.PrijemModel == MST_Global.MODEL_MEMORY)
			{
				rbPrijemModelMemory.Checked = true;
			}
			else if (MST_Global.PrijemModel == MST_Global.MODEL_CODEBOOK)
			{
				rbPrijemModelCodebook.Checked = true;
			}

			Prijem_4.Globals.Load(Main.ConfigModulesFileName);
			cfgPrijemOverFillItem.Checked = Prijem_4.Globals.OverFillItem;
			cfgPrijemZadaniLokacePredSN.Checked = Prijem_4.Globals.ZadaniLocncodePredSN;

			cfg_PrijemRez1Cislo.Checked = Prijem_4.Globals.Rez1Cislo;
			cfg_PrijemRez1Povinne.Checked = Prijem_4.Globals.Rez1Povinne;
			cfg_PrijemRez1Pamatovat.Checked = Prijem_4.Globals.Rez1Pamatovat;
			cfg_PrijemRez2Cislo.Checked = Prijem_4.Globals.Rez2Cislo;
			cfg_PrijemRez2Povinne.Checked = Prijem_4.Globals.Rez2Povinne;
			cfg_PrijemRez2Pamatovat.Checked = Prijem_4.Globals.Rez2Pamatovat;
			nuPrijemZobrazitZaznamu.Value = Prijem_4.Globals.ZobrazitZaznamu;

			#region TaD Dialogy Prijem

			chk_Prijem_DialogUspesnehoOdeslani.Checked = Prijem_4.Globals.PrijemDialogUspesnehoOdeslaniDavky;

			chk_Prijem_DialogOpusteniModulu.Checked = Prijem_4.Globals.PrijemDialogOpusteniModulu;
			chk_Prijem_DialogOpusteniPrijemky.Checked = Prijem_4.Globals.PrijemDialogOpusteniPrijemky;
			chk_Prijem_DialogOpusteniZalokovani.Checked = Prijem_4.Globals.PrijemDialogOpusteniZalokovani;
			chk_Prijem_DialogTiskSCenou.Checked = Prijem_4.Globals.EtiketaTiskDotazSCenou;

			chk_Prijem_DialogTiskSCenou_Cena.Checked = Prijem_4.Globals.EtiketaTiskDotazSCenou_Cena;
			chk_Prijem_DialogTiskSCenou_ZobrazDialog.Checked = Prijem_4.Globals.EtiketaTiskDotazSCenou_ZobrazDialog;

			chk_Prijem_DialogTiskSCenou_CheckStateChanged(null, null);

			#endregion


			cfg_REZ1_PRIJ_NAME.Text = MST_Global.REZ1_PRIJ_NAME;
			cfg_REZ2_PRIJ_NAME.Text = MST_Global.REZ2_PRIJ_NAME;

			cfg_REZ1_VYDE_NAME.Text = MST_Global.REZ1_VYDE_NAME;
			cfg_REZ2_VYDE_NAME.Text = MST_Global.REZ2_VYDE_NAME;

			chk_PrijemHledaniCkAutoVyberPrvniNeuplne.Checked = Prijem_4.Globals.HledaniCkAutoVyberPrvniNeuplne;
			cfgPrijemEtiketaTiskPoVlozeniDotaz.Checked = Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz;
			cfgPrijemEtiketyTiskPredOdeslanimDotaz.Checked = Prijem_4.Globals.EtiketyTiskPredOdeslanimDotaz;
			cfgPrijemEtiketaTiskPoOtevreniDotaz.Checked = Prijem_4.Globals.EtiketyTiskPoOtevreniDotaz;
			cfgPrijemPolozkyVyberJenScannerem.Checked = Prijem_4.Globals.PolozkyVyberJenScannerem;

			cfgPrijemTiskEtiketyPrebiratMnozstvi.Checked = Prijem_4.Globals.EtiketaTisk_PrebiratMnozstvi;


			cfgPrijemSlucovaniDavek.Checked = Prijem_4.Globals.SlucovaniDavek;
			cfgPrijemViceTerminaly.Checked = Prijem_4.Globals.ViceTerminaly;
			chkPrijemOnlinePohyby.Checked = Prijem_4.Globals.OnlinePohyby;
			cfgPrijemZmenaDataPrijmu.Checked = Prijem_4.Globals.ZmenaDataDokladu;
			cfgPrijemGenPrikPozadovatSklad.Checked = Prijem_4.Globals.GenerovaniPrikazuZadatSklad;
			cfgPrijemPovolitFoceniPriPridaniPolozky.Checked = Prijem_4.Globals.PovolitFoceniPriPridaniPolozky;
			chckPrijemOnlineGenerovatSarzi.Checked = Prijem_4.Globals.GenerovaniSarze;
			chckPrijemOnlineOverovatLokaci.Checked = Prijem_4.Globals.OverovatLokaci;
			cfgPrijemDavkaOtevritPoStazeni.Checked = Prijem_4.Globals.DavkaOtevritIhnedPoStazeni;
			cfgPrijemPovolitZalokovani.Checked = Prijem_4.Globals.PovolitZalokovani;
			cfgPrijemZalokovaniPozadovatZadaniMnozstvi.Checked = Prijem_4.Globals.ZalokovaniPozadovatZadaniMnozstvi;
			chckPrijemOnlineDoporuceneLokace.Checked = Prijem_4.Globals.DoporuceneLokace;
			cfgPrijemKontrolaSNsPredlohou.Checked = Prijem_4.Globals.KontrolovatSNsPredlohou;
			cfgPrijemZobrazitDialogZadaniSN.Checked = Prijem_4.Globals.ZobrazitDialogZadaniSN;
			cfgPrijemPovolitPrazdnouHodnotuSN.Checked = Prijem_4.Globals.PovolitPrazdnouHodnotuSN;
			cfgPrijemGenNenalezPrijumku.Checked = Prijem_4.Globals.GenerovatNenalezenouPrijemku;
			cfgPrijemKontrolaLokaciPredOdeslanim.Checked = Prijem_4.Globals.KontrolaVyplneniLokaciPredOdeslanim;
			cfgPrijemParsovaniCarovehoKoduPovolit.Checked = Prijem_4.Globals.ParsovaniCarovehoKoduPovolit;
			cfgPrijemLokaceNaDavkuPovolit.Checked = Prijem_4.Globals.LokaceNaDavkuPovolit;
			cfgPrijemZobrazitMnozstviUParsKodu.Checked = Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu;
			cfgPrijemPovolitZmenuRezimuLokace.Checked = Prijem_4.Globals.PovolitZmenuRezimuZadaniLokace;

			cfgPrijemZobrazovatReport.Checked = Prijem_4.Globals.ZobrazovatReport;
			cfgPrijemMnozstviAutoJedna.Checked = Prijem_4.Globals.MnozstviAutoJedna;
			cfgPrijemRozhodovatSkladExpedice.Checked = Prijem_4.Globals.RozhodovatSkladExpedice;

			txt_prijemPrefix.Text = Prijem_4.Globals.SequenceSSCC.ToString();
			cfgPrijemTypOznaceniPalety.Checked = Prijem_4.Globals.TypOznaceniPalety;
			cfgPrijemTypyPoctyPalet.Checked = Prijem_4.Globals.TypyPoctyPalet;

			cfgPrijemDialogTisk.Checked = Prijem_4.Globals.DialogTisk;

			// Prijem sklady
			cfgPrijemSkladPouzit.Checked = Prijem_4.Globals.SkladPouzit;
			cfgPrijemSkladID.Text = Prijem_4.Globals.SkladID;
			cfgPrijemPrevzitIDSkladZCisSklad.Checked = Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu;
			cfgPrijemCiselnikSkladuOnlyOne.Checked = Prijem_4.Globals.FiltrCiselnikSkladuOnlyOne;
			chckPrijemOnlineDoplneniVychoziLokace.Checked = Prijem_4.Globals.DoplneniVychoziLokacePovolit;

			chckPrijemOnlineNezrealizovanePrijemky.Checked = Prijem_4.Globals.NezrealizovanePrijemky;
			chkPrijemOnlineHmotnost.Checked = Prijem_4.Globals.OnlineHmotnost;
			chkPrijemNacistSkladIDOnline.Checked = Prijem_4.Globals.NacistSkladIDOnline;

			chkPrijemOnline_NerealizovanePrijekyCarKody.Checked = Prijem_4.Globals.NerealizovanePrijekyCarKody;

			Prijem_4.Globals.Load(Main.ConfigModulesFileName);

			chckPrijemObjednavkaDetailPovolit.Checked = MST_Global.OnlineObjednavkaDetailPovolit;
			chckPrijemOnlinePolozkaDetailPovolit.Checked = MST_Global.OnlinePolozkaDetailPovolit;
			chckPrijemPolozkaKusuNaSkladePovolit.Checked = MST_Global.OnlinePolozkaKusuNaSkladePovolit;
			chckPrijemOnlinePolozkaKusuPovolit.Checked = MST_Global.OnlinePolozkaKusuPovolit;


		}

		private void Kongif_Load_Prodej()
		{
			Prodej.Globals.Load(Main.ConfigModulesFileName);

			cfgProdejCenaVystup.Checked = Prodej.Globals.PriceIsWithTax;
			//cfgProdejCenaVystupPovolit.Checked = Prodej.Globals.PriceIsWithTaxEnable;
			cfgProdejCenaVystupPovolit.CheckState = Prodej.Globals.PriceIsWithTaxEnable ? CheckState.Checked : CheckState.Unchecked;
			cfgProdejCena0.Checked = Prodej.Globals.Price0IsWithTax;
			cfgProdejCena1.Checked = Prodej.Globals.Price1IsWithTax;
			cfgProdejCena2.Checked = Prodej.Globals.Price2IsWithTax;
			cfgProdejCena3.Checked = Prodej.Globals.Price3IsWithTax;
			cfgProdejCena4.Checked = Prodej.Globals.Price4IsWithTax;
			cfgProdejCena5.Checked = Prodej.Globals.Price5IsWithTax;
			cfgProdejOdberatele.Checked = Prodej.Globals.Odberatel;
			cfgZadaniLokace.Checked = Prodej.Globals.prodejPovolitZadaniLocncode;
			cfgProdejOneOdbAuto.Checked = Prodej.Globals.OneOdberatelAutoSelect;
			cfgProdejOneOdberatelOnly.Checked = Prodej.Globals.OneOdberatelOnly;
			cfgProdejPrefix.Text = Prodej.Globals.Prefix.ToString();
			cfgProdejNextNumber.Value = Prodej.Globals.NextNumber;
			//cfgProdejRangeEnable.Checked = Prodej.Globals.RangeEnable;
			cfgProdejRangeEnable.CheckState = Prodej.Globals.RangeEnable ? CheckState.Checked : CheckState.Unchecked;
			cfgProdejStrediska.Checked = Prodej.Globals.Strediska;
			cfgProdejPracovniciPovolit.Checked = Prodej.Globals.Pracovnici;
			cfgProdejNovaPolozkaPridat.Checked = Prodej.Globals.PovolitNovouPolozku;
			cfgProdejNovaPolozkaPridatDotaz.Checked = Prodej.Globals.DotazPridatNovaPolozka;
			cfgProdejPozadovatHesloOtevreniRozpracovaneDavky.Checked = Prodej.Globals.PozadovatHesloProOtevreniRozpracovaneDavky;
			cfgProdejNaplnenaDavkaEditaceHeslo.Text = Prodej.Globals.HesloEditaceNaplnenaDavka;
			cfgProdejZadaniMnozstviScanneremPovolit.Checked = Prodej.Globals.PovolitZadaniMnozstviScannerem;
			cfgProdejZobrazitMnozstviUParsKodu.Checked = Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu;
			cfgProdejPovolitCeny.Checked = Prodej.Globals.PovolitCeny;
			cfgProdejNacistNazevSkladuPriHledani.Checked = Prodej.Globals.NacistNazevSkladuPriVyhledavani;

			chckProdejOnlinePocetKusu.Checked = Prodej.Globals.OnlinePocetKusu;
			chckProdejOnlinePocetKusuSklad.Checked = Prodej.Globals.OnlinePocetKusuSklad;
			chckProdejOnlinePolozkaDetail.Checked = Prodej.Globals.OnlineDetailPolozka;

			cfgProdejSkladID.Text = Prodej.Globals.SkladID;
			cfgProdejDisponibilityCheck.Checked = Prodej.Globals.DisponibilityCheck;
			cfgProdejDisponibilityHlaska.Checked = Prodej.Globals.DisponibilityHlaska;
			cfgProdejDisponibilityZvuk.Checked = Prodej.Globals.DisponibilityZvuk;
			cfgProdejTypDokladu.Checked = Prodej.Globals.TypDokladu;
			cfgProdejRucniVolbaOdberatele.Checked = Prodej.Globals.RucniVolbaOdberatele;
			cfgProdejVnditnum2Serltnum.Checked = Prodej.Globals.Vnditnum2Serltnum;
			cfgProdejKontrolaStavuSkladu.Checked = Prodej.Globals.KontrolaStavuSkladu;
			cfg_prodejZadaniLocncodePredSN.Checked = Prodej.Globals.prodejZadaniLocncodePredSN;
			cfgProdejStrediskoKPolozce.Checked = Prodej.Globals.StrediskoKPolozce;
			cfgProdejStrediskoText.Checked = Prodej.Globals.StrediskoText;
			cfgProdejStrediskoJednoNaDavku.Checked = Prodej.Globals.StrediskoJednoNaDavku;
			cfgProdejPracovniciKPolozce.Checked = Prodej.Globals.PracovniciKPolozce;
			cfgProdejPracovniciJedenNaDavku.Checked = Prodej.Globals.PracovniciJedenNaDavku;
			cfgProdejPracovniciHolyText.Checked = Prodej.Globals.PracovniciText;
			cfgProdejPracovniciVyberJenScannerem.Checked = Prodej.Globals.PracovniciVyberJenScannerem;
			cfg_ProdejRez1Cislo.Checked = Prodej.Globals.Rez1Cislo;
			cfg_ProdejRez1Povinne.Checked = Prodej.Globals.Rez1Povinne;
			cfg_ProdejRez1Pamatovat.Checked = Prodej.Globals.Rez4Pamatovat;
			cfg_ProdejRez2Cislo.Checked = Prodej.Globals.Rez2Cislo;
			cfg_ProdejRez2Povinne.Checked = Prodej.Globals.Rez2Povinne;
			cfg_ProdejRez2Pamatovat.Checked = Prodej.Globals.Rez4Pamatovat;
			cfg_ProdejRez3Cislo.Checked = Prodej.Globals.Rez3Cislo;
			cfg_ProdejRez3Povinne.Checked = Prodej.Globals.Rez3Povinne;
			cfg_ProdejRez3Pamatovat.Checked = Prodej.Globals.Rez4Pamatovat;
			cfg_ProdejRez4Cislo.Checked = Prodej.Globals.Rez4Cislo;
			cfg_ProdejRez4Povinne.Checked = Prodej.Globals.Rez4Povinne;
			cfg_ProdejRez4Pamatovat.Checked = Prodej.Globals.Rez4Pamatovat;
			chk_ProdejFiltrCiselnikSkladu.Checked = Prodej.Globals.FiltrCiselnikSkladu;
			chk_ProdejPouzitSklady.Checked = Prodej.Globals.PouzitSklady;
			chk_ProdejFiltrCiselnikSkladuOnlyOne.Checked = Prodej.Globals.FiltrCiselnikSkladuOnlyOne;
			chk_ProdejZobrazovatListPolozek.Checked = Prodej.Globals.ZobrazovatListPolozek;
			chk_ProdejOverovatPohyb.Checked = Prodej.Globals.OverovatPohyb;
			chk_ProdejOverovatZdrojLokaci.Checked = Prodej.Globals.OverovatZdrojovouLokaci;
			chk_ProdejOverovatCilLokaci.Checked = Prodej.Globals.OverovatCilovouLokaci;
			chk_ProdejDoporuceneCilLokace.Checked = Prodej.Globals.DoporuceneCiloveLokace;
			chk_ProdejDoporucenePalety.Checked = Prodej.Globals.DoporucenePalety;
			chk_ProdejNacistSkladIDOnline.Checked = Prodej.Globals.NacistSkladIDOnline;
			chk_ProdejPovolitTiskEtikety.Checked = Prodej.Globals.PovolitTiskEtikety;
			txt_ProdejTiskEtiketyPredvyplneneMnozstvi.Text = Prodej.Globals.PredvyplneneMnozstviEtikety;
			txt_ProdejTiskSoupisuPredvyplneneMnozstvi.Text = Prodej.Globals.PredvyplneneMnozstviSoupisu;
			chk_ProdejPovolitTiskSoupisu.Checked = Prodej.Globals.PovolitTiskSoupisu;
			chk_ProdejTiskSoupisuPriUzavreniDavky.Checked = Prodej.Globals.TiskSoupisuPriUzavreniDavky;
			chk_ProdejTiskEtiketyPoPridaniZbozi.Checked = Prodej.Globals.TiskEtiketyPoPridaniZbozi;

			chk_ProdejTiskEtiketyPrebiratMnozstvi.Checked = Prodej.Globals.EtiketaTisk_PrebiratMnozstvi;


			chk_ProdejPovolitPrevodMeziSklady.Checked = Prodej.Globals.PovolitPrevodMeziSklady;
			chk_Prodej_ExistenceNasnimanePolozky.Checked = Prodej.Globals.ExistenceNasnimanePolozky;
			cfgProdejMnozstvi1Auto.Checked = Prodej.Globals.Mnozstvi1Auto;
			cfgProdejZobrazovatReport.Checked = Prodej.Globals.ZobrazovatReport;
			cfgProdejMnozstviREZ1Vypln.Checked = Prodej.Globals.MnozstviREZ1Vypln;
			cfgProdejFindDefaultMJ.Checked = Prodej.Globals.FindDefaultMJ;
			nuProdejGridOdberatele.Value = Prodej.Globals.GridViewRowCount;
			cfgProdejPolozkyVyberJenScannerem.Checked = Prodej.Globals.PolozkyVyberJenScannerem;
			cfgProdejPolozkyVyhledatPomociSarze.Checked = Prodej.Globals.PolozkyVyhledatPomociSarze;
			cfgProdejAktualizaceZboziPredVyberemDavky.Checked = Prodej.Globals.AktualizaceZboziPredVyberemDavky;
			cfgProdejPaletyPovolit.Checked = Prodej.Globals.PaletyPovolit;
			chk_ProdejFiltrDodavatel.Checked = Prodej.Globals.FiltrDodavatele;
			cfgProdejExportCiselnikuPovolit.Checked = Prodej.Globals.PovolitExportCiselniku;

			#region TaD Dialogy Prodej

			chk_Prodej_DialogUspesnehoOdeslani.Checked = Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky;
			chk_Prodej_DialogOpusteniModulu.Checked = Prodej.Globals.ProdejDialogOpusteniModulu;
			chk_Prodej_DialogSmazaniDavky.Checked = Prodej.Globals.ProdejDialogSmazaniDavky;
			chk_Prodej_DialogUkonceniZpracobaniDavky.Checked = Prodej.Globals.ProdejDialogUkonceniZpracobaniDavky;
			chk_Prodej_DialogNasnimanaLokace.Checked = Prodej.Globals.ProdejDialogNasnimanaLokace;
			chk_Prodej_EtiketaTiskDotazSCenou.Checked = Prodej.Globals.EtiketaTiskDotazSCenou;

			chk_Prodej_EtiketaTiskDotazSCenou_Cena.Checked = Prodej.Globals.EtiketaTiskDotazSCenou_Cena;
			chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog.Checked = Prodej.Globals.EtiketaTiskDotazSCenou_ZobrazDialog;

			chk_Prodej_DialogTisk.Checked = Prodej.Globals.DialogTisk;

			chk_Prodej_EtiketaTiskDotazSCenou_CheckStateChanged(null, null);

			#endregion

			#region F10 možnosti

			rbProdej_OnlinePocetKusuSklad.Checked = Prodej.Globals.F10_OnlinePocetKusuSklad;
			rbProdej_ZobrazitAlternativyLokaci.Checked = Prodej.Globals.F10_ZobrazitAlternativyLokaci;
			rbProdej_TiskEtiketa.Checked = Prodej.Globals.F10_TiskEtiketa;
			

			#endregion

			cfgProdejPovolitParsovaniMnozstvi.Checked = Prodej.Globals.PovolitParsovaniMnozstvi;
			cfgProdejPovolitParsovaniSarze.Checked = Prodej.Globals.PovolitParsovaniSarze;

			cfg_REZ1_PROD_NAME.Text = MST_Global.REZ1_PROD_NAME;
			cfg_REZ2_PROD_NAME.Text = MST_Global.REZ2_PROD_NAME;
			cfg_REZ3_PROD_NAME.Text = MST_Global.REZ3_PROD_NAME;
			cfg_REZ4_PROD_NAME.Text = MST_Global.REZ4_PROD_NAME;

		}

		#endregion

		#region Konfig_Save

		private void Konfig_Save_Print()
		{
			//Nastaveni zpet parametru pro printserver...
			PrinterFactory.PrinterFactory pFactory = PrinterFactory.PrinterFactory.Instance;
			Dictionary<Fask.PrinterFactory.PrinterModules, Fask.PrinterFactory.ModuleToPrint> templates = pFactory.Templates;
			foreach (Fask.PrinterFactory.PrinterModules key in templates.Keys)
			{
				Fask.PrinterFactory.ModuleToPrint m2p = templates[key];
				switch (key)
				{
					case Fask.PrinterFactory.PrinterModules.PrijemPredloha:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypePrijemPredloha.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNamePrijemPredloha.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.PrijemNasnimane:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypePrijemNasnimane.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNamePrijemNasnimane.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.VydejPredloha:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeVydejPredloha.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameVydejPredloha.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.VydejNasnimane:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeVydejNasnimane.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameVydejNasnimane.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.VydejPaletovylistek:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeVydejPalListek.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameVydejPalListek.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejPredloha:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejPredloha.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejPredloha.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejNasnimane:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejNasnimane.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejNasnimane.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejSoupisHlavicka:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejSoupisListHlavicka.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejSoupisRadek:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejSoupisListRadek.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejSoupisPaticka:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejSoupisList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejSoupisListPaticka.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejPaletaHlavicka:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejPaletyListHlavicka.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejPaletaRadek:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejPaletyListRadek.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.ProdejPaletaPaticka:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeProdejPaletyList.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameProdejPaletyListPaticka.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.InventuraPredloha:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeInventuraPredloha.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameInventuraPredloha.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.InventuraNasnimane:
						m2p.PrinterType = (string)cfgPrintServerPrinterTypeInventuraNasnimane.SelectedItem;
						m2p.Template = cfgPrintServerTemplateNameInventuraNasnimane.Text;
						break;
					case Fask.PrinterFactory.PrinterModules.TextVolny:
						// TODO : doplnit pro volny text ...
						//m2p.PrinterType = (string)cfgPrintServerPrinterType.SelectedItem;
						//m2p.Template = cfgPrintServerTemplateName.Text;
						break;
					default:
						break;
				}
			}
			MST_Global.PovolitPrintServer = chckPovolitPrintServer.Checked;
			pFactory.SaveConfiguration();
		}

		private void Konfig_Save_Servis()
		{

			MST_Global.ServisEnable = cfgServisPovolit.Checked;
			MST_Global.ServisShowInMST = cfgServisZobrazitVMST.Checked;
			MST_Global.ServisTimeoutOnline = int.Parse(cfgServisTimeoutOnline.Text);
			MST_Global.ServisTimeoutSynchronize = int.Parse(cfgServisTimeoutSynchronize.Text);
			MST_Global.ServisAutoUpdateInterval = int.Parse(cfgServisAutoUpdateInterval.Text);
			MST_Global.ServisAllowCamera = cfgServisPovolitFotoaparat.Checked;
			MST_Global.ServisDavkoveZpracovani = cfgServisDavkoveZpracovani.Checked;
			MST_Global.ServisUkonceniStavuNavrat = cfgServisUkonceniStavuNavrat.Checked;
			MST_Global.ServisPozadovatPotvrzeniZmenyCinnosti = cfgServisPozadovatPotvrzeniZmenyCinnosti.Checked;
			MST_Global.ServisPozadovatPotvrzeniZmenyStavu = cfgServisPozadovatPotvrzeniZmenyStavu.Checked;
			MST_Global.ServisPrehratZvukPoVyberuMoznosti = cfgServisPrehratZvukyPriPotvrzeniVoleb.Checked;
			MST_Global.ServisStatusBarZobrazitNazevStavu = cfgServisStatusBarZobrazitNazevStavu.Checked;
			MST_Global.ServisStatusBarZobrazitTypZdroje = cfgServisStatusBarZobrazitTypZdroje.Checked;
			MST_Global.ServisStatusBarZobrazitIdZdroje = cfgServisStatusBarIdZdroj.Checked;
			MST_Global.ServisStatusBarZobrazitOznaceniZdroje = cfgServisStatusBarZobrazitOznaceniZdroje.Checked;
			MST_Global.ServisZobrazitInformaciOUkonceniStavu = cfgServisZobrazitInfoOUkonceniStavu.Checked;
			MST_Global.ServisDavkaOtevritIhnedPoStazeni = cfgServisDavkaOtevritPoStazeni.Checked;
			MST_Global.ServisFiltrZobrazeniPovolit = cfgServisFiltrZobrazeniPovolit.Checked;
			MST_Global.ServisAutomatickaZmenaStavuPovolit = cfgServisAutomatickaZmenaStavuPovolit.Checked;
			MST_Global.ServisSynchronizaceZdrojePoVyberuPovolit = cfgServisSynchZdrojePoVyberuPovolit.Checked;
			MST_Global.ServisVyberOdberatelPovolit = cfgServisVyberOdberatele.Checked;
			MST_Global.ServisVyberOkruhPovolit = cfgServisVyberOkruhu.Checked;
			MST_Global.ServisOdeslaniDatNaPozadiPovolit = cfgServisOdeslaniDatNaPozadiPovolit.Checked;
			MST_Global.ServisOdeslaniDatPoUkonceniZadavaniPovolit = cfgServisOdeslaniDatPoUkonceniZadavaniPovolit.Checked;

			#region TaD Dialogy Servis

			MST_Global.Servis_DialogOpusteniModulu = chk_Servis_DialogOpusteniModulu.Checked;

			#endregion
		}

		private void Konfig_Save_Expedice()
		{
			Expedice.Globals.SkladID = cfgExpediceSkladID.Text;
			Expedice.Globals.PovolitParsovaniCK = cfgExpedicePovolitParsovaniCK.Checked;
			Expedice.Globals.PovolitPredvyplneniMnozstvi = cfgExpedicePovolitPredvyplneniMnozstvi.Checked;
			Expedice.Globals.ZobrazitZadaniSarzePouzeJednou = cfgExpediceZobrazitZadaniSarzeJednou.Checked;
			Expedice.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu = cfgExpediceZobrazitMnozstviUParsKodu.Checked;
			Expedice.Globals.PovolitTiskPalet = cfgExpedicePovolitTiskPalet.Checked;
			Expedice.Globals.PovolitTiskSoupisu = cfgExpedicePovolitTiskSoupisu.Checked;

			#region TaD Dialogy Expedice
			Expedice.Globals.Expedice_DialogOpusteniModulu = chk_Expedice_DialogOpusteniModulu.Checked;
			#endregion

			if (!Expedice.Globals.Save(Main.ConfigModulesFileName))
				throw new Exception("Nepodařilo se uložit konfiguraci modulu Expedice");
		}

		private void Konfig_Save_Prijem()
		{
			//Prijem.Globals.OverFillItem = cfgPrijemOverFillItem.Checked;

			//if (!Prijem.Globals.Save(Main.ConfigModulesFileName))
			//    throw new Exception("Nepodařilo se uložit konfiguraci modulu příjmu");

			Prijem_4.Globals.OverFillItem = cfgPrijemOverFillItem.Checked;

			if (cfgDelkaKontrolySN.Text.Trim() != "")
				Prijem_4.Globals.DelkaKontrolySN = Convert.ToInt32(cfgDelkaKontrolySN.Text.Trim());
			if (cfgDatum.Text.Trim() != "")
				Prijem_4.Globals.Datum = Convert.ToDateTime(cfgDatum.Text.Trim());
			if (cfgPocetVlastCisSN.Text.Trim() != "")
				Prijem_4.Globals.PocetCislicVlastniSN = Convert.ToInt32(cfgPocetVlastCisSN.Text.Trim());
			if (cfgPosledniGenSN.Text.Trim() != "")
				Prijem_4.Globals.PosledniGenSN = Convert.ToInt32(cfgPosledniGenSN.Text.Trim());
			if (cfgMaxPocetZakazSN.Text.Trim() != "")
				Prijem_4.Globals.MaxPocetZakazSN = Convert.ToInt32(cfgMaxPocetZakazSN.Text.Trim());
			if (cfgMaxDelkaZakazSN.Text.Trim() != "")
				Prijem_4.Globals.MaxDelkaZakazSN = Convert.ToInt32(cfgMaxDelkaZakazSN.Text.Trim());

			Prijem_4.Globals.ZadaniLocncodePredSN = cfgPrijemZadaniLokacePredSN.Checked;

			Prijem_4.Globals.Rez1Cislo = cfg_PrijemRez1Cislo.Checked;
			Prijem_4.Globals.Rez1Povinne = cfg_PrijemRez1Povinne.Checked;
			Prijem_4.Globals.Rez1Pamatovat = cfg_PrijemRez1Pamatovat.Checked;
			Prijem_4.Globals.Rez2Cislo = cfg_PrijemRez2Cislo.Checked;
			Prijem_4.Globals.Rez2Povinne = cfg_PrijemRez2Povinne.Checked;
			Prijem_4.Globals.Rez2Pamatovat = cfg_PrijemRez2Pamatovat.Checked;
			Prijem_4.Globals.ZobrazitZaznamu = Convert.ToInt32(nuPrijemZobrazitZaznamu.Value);

			Prijem_4.Globals.HledaniCkAutoVyberPrvniNeuplne = chk_PrijemHledaniCkAutoVyberPrvniNeuplne.Checked;
			Prijem_4.Globals.EtiketaTiskPoVlozeniDotaz = cfgPrijemEtiketaTiskPoVlozeniDotaz.Checked;
			Prijem_4.Globals.EtiketyTiskPredOdeslanimDotaz = cfgPrijemEtiketyTiskPredOdeslanimDotaz.Checked;
			Prijem_4.Globals.EtiketyTiskPoOtevreniDotaz = cfgPrijemEtiketaTiskPoOtevreniDotaz.Checked;
			Prijem_4.Globals.PolozkyVyberJenScannerem = cfgPrijemPolozkyVyberJenScannerem.Checked;
			Prijem_4.Globals.SlucovaniDavek = cfgPrijemSlucovaniDavek.Checked;
			Prijem_4.Globals.ViceTerminaly = cfgPrijemViceTerminaly.Checked;

			Prijem_4.Globals.EtiketaTisk_PrebiratMnozstvi = cfgPrijemTiskEtiketyPrebiratMnozstvi.Checked;

			Prijem_4.Globals.OnlinePohyby = chkPrijemOnlinePohyby.Checked;
			Prijem_4.Globals.ZmenaDataDokladu = cfgPrijemZmenaDataPrijmu.Checked;
			Prijem_4.Globals.GenerovaniPrikazuZadatSklad = cfgPrijemGenPrikPozadovatSklad.Checked;
			Prijem_4.Globals.PovolitFoceniPriPridaniPolozky = cfgPrijemPovolitFoceniPriPridaniPolozky.Checked;
			Prijem_4.Globals.GenerovaniSarze = chckPrijemOnlineGenerovatSarzi.Checked;
			Prijem_4.Globals.OverovatLokaci = chckPrijemOnlineOverovatLokaci.Checked;
			Prijem_4.Globals.DavkaOtevritIhnedPoStazeni = cfgPrijemDavkaOtevritPoStazeni.Checked;
			Prijem_4.Globals.PovolitZalokovani = cfgPrijemPovolitZalokovani.Checked;
			Prijem_4.Globals.ZalokovaniPozadovatZadaniMnozstvi = cfgPrijemZalokovaniPozadovatZadaniMnozstvi.Checked;
			Prijem_4.Globals.DoporuceneLokace = chckPrijemOnlineDoporuceneLokace.Checked;
			Prijem_4.Globals.KontrolovatSNsPredlohou = cfgPrijemKontrolaSNsPredlohou.Checked;
			Prijem_4.Globals.ZobrazitDialogZadaniSN = cfgPrijemZobrazitDialogZadaniSN.Checked;
			Prijem_4.Globals.PovolitPrazdnouHodnotuSN = cfgPrijemPovolitPrazdnouHodnotuSN.Checked;
			Prijem_4.Globals.GenerovatNenalezenouPrijemku = cfgPrijemGenNenalezPrijumku.Checked;
			Prijem_4.Globals.NezrealizovanePrijemky = chckPrijemOnlineNezrealizovanePrijemky.Checked;
			Prijem_4.Globals.KontrolaVyplneniLokaciPredOdeslanim = cfgPrijemKontrolaLokaciPredOdeslanim.Checked;
			Prijem_4.Globals.ParsovaniCarovehoKoduPovolit = cfgPrijemParsovaniCarovehoKoduPovolit.Checked;
			Prijem_4.Globals.LokaceNaDavkuPovolit = cfgPrijemLokaceNaDavkuPovolit.Checked;
			Prijem_4.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu = cfgPrijemZobrazitMnozstviUParsKodu.Checked;
			Prijem_4.Globals.PovolitZmenuRezimuZadaniLokace = cfgPrijemPovolitZmenuRezimuLokace.Checked;

			Prijem_4.Globals.ZobrazovatReport = cfgPrijemZobrazovatReport.Checked;
			Prijem_4.Globals.MnozstviAutoJedna = cfgPrijemMnozstviAutoJedna.Checked;
			Prijem_4.Globals.RozhodovatSkladExpedice = cfgPrijemRozhodovatSkladExpedice.Checked;
			Prijem_4.Globals.DialogTisk = cfgPrijemDialogTisk.Checked;

			Prijem_4.Globals.SequenceSSCC = Convert.ToInt32(txt_vydejPrefix.Text.Trim());
			Prijem_4.Globals.TypOznaceniPalety = cfgPrijemTypOznaceniPalety.Checked;
			Prijem_4.Globals.TypyPoctyPalet = cfgPrijemTypyPoctyPalet.Checked;

			Prijem_4.Globals.NerealizovanePrijekyCarKody = chkPrijemOnline_NerealizovanePrijekyCarKody.Checked;
			// Prijem Sklady
			Prijem_4.Globals.SkladPouzit = cfgPrijemSkladPouzit.Checked;
			Prijem_4.Globals.SkladID = cfgPrijemSkladID.Text;
			Prijem_4.Globals.PrevzitIDSkladuZCiselnikuSkladu = cfgPrijemPrevzitIDSkladZCisSklad.Checked;
			Prijem_4.Globals.FiltrCiselnikSkladuOnlyOne = cfgPrijemCiselnikSkladuOnlyOne.Checked;
			Prijem_4.Globals.DoplneniVychoziLokacePovolit = chckPrijemOnlineDoplneniVychoziLokace.Checked;

			Prijem_4.Globals.OnlineHmotnost = chkPrijemOnlineHmotnost.Checked;
			Prijem_4.Globals.NacistSkladIDOnline = chkPrijemNacistSkladIDOnline.Checked;

			#region TaD Prijem Dialogy

			Prijem_4.Globals.PrijemDialogUspesnehoOdeslaniDavky = chk_Prijem_DialogUspesnehoOdeslani.Checked;
			Prijem_4.Globals.PrijemDialogOpusteniModulu = chk_Prijem_DialogOpusteniModulu.Checked;
			Prijem_4.Globals.PrijemDialogOpusteniPrijemky = chk_Prijem_DialogOpusteniPrijemky.Checked;
			Prijem_4.Globals.PrijemDialogOpusteniZalokovani = chk_Prijem_DialogOpusteniZalokovani.Checked;
			Prijem_4.Globals.EtiketaTiskDotazSCenou = chk_Prijem_DialogTiskSCenou.Checked;

			Prijem_4.Globals.EtiketaTiskDotazSCenou_Cena = chk_Prijem_DialogTiskSCenou_Cena.Checked;
			Prijem_4.Globals.EtiketaTiskDotazSCenou_ZobrazDialog = chk_Prijem_DialogTiskSCenou_ZobrazDialog.Checked;

			#endregion

			if (!Prijem_4.Globals.Save(Main.ConfigModulesFileName))
				throw new Exception("Nepodařilo se uložit konfiguraci modulu příjmu");

			if (!Prijem_4.Globals.Save(Main.ConfigModulesFileName))
				throw new Exception("Nepodařilo se uložit konfiguraci modulu příjmu");
		}

		private void Konfig_Save_Prodej()
		{
			Prodej.Globals.OnlinePocetKusu = chckProdejOnlinePocetKusu.Checked;
			Prodej.Globals.OnlinePocetKusuSklad = chckProdejOnlinePocetKusuSklad.Checked;
			Prodej.Globals.OnlineDetailPolozka = chckProdejOnlinePolozkaDetail.Checked;

			Prodej.Globals.PriceIsWithTax = cfgProdejCenaVystup.Checked;
			Prodej.Globals.PriceIsWithTaxEnable = cfgProdejCenaVystupPovolit.Checked;
			Prodej.Globals.Price5IsWithTax = cfgProdejCena5.Checked;
			Prodej.Globals.Price4IsWithTax = cfgProdejCena4.Checked;
			Prodej.Globals.Price3IsWithTax = cfgProdejCena3.Checked;
			Prodej.Globals.Price2IsWithTax = cfgProdejCena2.Checked;
			Prodej.Globals.Price1IsWithTax = cfgProdejCena1.Checked;
			Prodej.Globals.Price0IsWithTax = cfgProdejCena0.Checked;
			Prodej.Globals.Odberatel = cfgProdejOdberatele.Checked;
			Prodej.Globals.prodejPovolitZadaniLocncode = cfgZadaniLokace.Checked;
			Prodej.Globals.OneOdberatelAutoSelect = cfgProdejOneOdbAuto.Checked;
			Prodej.Globals.OneOdberatelOnly = cfgProdejOneOdberatelOnly.Checked;
			Prodej.Globals.NextNumber = Convert.ToInt32(cfgProdejNextNumber.Value);
			Prodej.Globals.Prefix = Convert.ToInt32(cfgProdejPrefix.Text);
			Prodej.Globals.RangeEnable = cfgProdejRangeEnable.Checked;
			Prodej.Globals.TypDokladu = cfgProdejTypDokladu.Checked;
			Prodej.Globals.RucniVolbaOdberatele = cfgProdejRucniVolbaOdberatele.Checked;
			Prodej.Globals.Strediska = cfgProdejStrediska.Checked;
			Prodej.Globals.Pracovnici = cfgProdejPracovniciPovolit.Checked;
			Prodej.Globals.PovolitNovouPolozku = cfgProdejNovaPolozkaPridat.Checked;
			Prodej.Globals.DotazPridatNovaPolozka = cfgProdejNovaPolozkaPridatDotaz.Checked;
			Prodej.Globals.PozadovatHesloProOtevreniRozpracovaneDavky = cfgProdejPozadovatHesloOtevreniRozpracovaneDavky.Checked;
			Prodej.Globals.HesloEditaceNaplnenaDavka = cfgProdejNaplnenaDavkaEditaceHeslo.Text;
			Prodej.Globals.PovolitZadaniMnozstviScannerem = cfgProdejZadaniMnozstviScanneremPovolit.Checked;
			Prodej.Globals.ZobrazitDialogZadaniMnozstviParsovanehoKodu = cfgProdejZobrazitMnozstviUParsKodu.Checked;
			Prodej.Globals.PovolitCeny = cfgProdejPovolitCeny.Checked;
			Prodej.Globals.NacistNazevSkladuPriVyhledavani = cfgProdejNacistNazevSkladuPriHledani.Checked;
			Prodej.Globals.SkladID = cfgProdejSkladID.Text.Trim();
			Prodej.Globals.DisponibilityCheck = cfgProdejDisponibilityCheck.Checked;
			Prodej.Globals.DisponibilityHlaska = cfgProdejDisponibilityHlaska.Checked;
			Prodej.Globals.DisponibilityZvuk = cfgProdejDisponibilityZvuk.Checked;
			Prodej.Globals.Vnditnum2Serltnum = cfgProdejVnditnum2Serltnum.Checked;
			Prodej.Globals.KontrolaStavuSkladu = cfgProdejKontrolaStavuSkladu.Checked;
			Prodej.Globals.prodejZadaniLocncodePredSN = cfg_prodejZadaniLocncodePredSN.Checked;
			Prodej.Globals.StrediskoKPolozce = cfgProdejStrediskoKPolozce.Checked;
			Prodej.Globals.StrediskoText = cfgProdejStrediskoText.Checked;
			Prodej.Globals.StrediskoJednoNaDavku = cfgProdejStrediskoJednoNaDavku.Checked;
			Prodej.Globals.PracovniciJedenNaDavku = cfgProdejPracovniciJedenNaDavku.Checked;
			Prodej.Globals.PracovniciKPolozce = cfgProdejPracovniciKPolozce.Checked;
			Prodej.Globals.PracovniciText = cfgProdejPracovniciHolyText.Checked;
			Prodej.Globals.PracovniciVyberJenScannerem = cfgProdejPracovniciVyberJenScannerem.Checked;
			Prodej.Globals.Rez1Cislo = cfg_ProdejRez1Cislo.Checked;
			Prodej.Globals.Rez1Povinne = cfg_ProdejRez1Povinne.Checked;
			Prodej.Globals.Rez1Pamatovat = cfg_ProdejRez1Pamatovat.Checked;
			Prodej.Globals.Rez2Cislo = cfg_ProdejRez2Cislo.Checked;
			Prodej.Globals.Rez2Povinne = cfg_ProdejRez2Povinne.Checked;
			Prodej.Globals.Rez2Pamatovat = cfg_ProdejRez2Pamatovat.Checked;
			Prodej.Globals.Rez3Cislo = cfg_ProdejRez3Cislo.Checked;
			Prodej.Globals.Rez3Povinne = cfg_ProdejRez3Povinne.Checked;
			Prodej.Globals.Rez3Pamatovat = cfg_ProdejRez3Pamatovat.Checked;
			Prodej.Globals.Rez4Cislo = cfg_ProdejRez4Cislo.Checked;
			Prodej.Globals.Rez4Povinne = cfg_ProdejRez4Povinne.Checked;
			Prodej.Globals.Rez4Pamatovat = cfg_ProdejRez4Pamatovat.Checked;
			Prodej.Globals.FiltrCiselnikSkladu = chk_ProdejFiltrCiselnikSkladu.Checked;
			Prodej.Globals.PouzitSklady = chk_ProdejPouzitSklady.Checked;
			Prodej.Globals.ZobrazovatListPolozek = chk_ProdejZobrazovatListPolozek.Checked;
			Prodej.Globals.FiltrCiselnikSkladuOnlyOne = chk_ProdejFiltrCiselnikSkladuOnlyOne.Checked;
			Prodej.Globals.Mnozstvi1Auto = cfgProdejMnozstvi1Auto.Checked;
			Prodej.Globals.ZobrazovatReport = cfgProdejZobrazovatReport.Checked;
			Prodej.Globals.MnozstviREZ1Vypln = cfgProdejMnozstviREZ1Vypln.Checked;
			Prodej.Globals.FindDefaultMJ = cfgProdejFindDefaultMJ.Checked;
			Prodej.Globals.GridViewRowCount = Convert.ToInt32(nuProdejGridOdberatele.Value);
			Prodej.Globals.PolozkyVyberJenScannerem = cfgProdejPolozkyVyberJenScannerem.Checked;
			Prodej.Globals.PolozkyVyhledatPomociSarze = cfgProdejPolozkyVyhledatPomociSarze.Checked;
			Prodej.Globals.AktualizaceZboziPredVyberemDavky = cfgProdejAktualizaceZboziPredVyberemDavky.Checked;
			Prodej.Globals.PaletyPovolit = cfgProdejPaletyPovolit.Checked;
			Prodej.Globals.FiltrDodavatele = chk_ProdejFiltrDodavatel.Checked;
			Prodej.Globals.PovolitExportCiselniku = cfgProdejExportCiselnikuPovolit.Checked;
			Prodej.Globals.OverovatPohyb = chk_ProdejOverovatPohyb.Checked;
			Prodej.Globals.OverovatZdrojovouLokaci = chk_ProdejOverovatZdrojLokaci.Checked;
			Prodej.Globals.OverovatCilovouLokaci = chk_ProdejOverovatCilLokaci.Checked;
			Prodej.Globals.DoporuceneCiloveLokace = chk_ProdejDoporuceneCilLokace.Checked;
			Prodej.Globals.DoporucenePalety = chk_ProdejDoporucenePalety.Checked;
			Prodej.Globals.NacistSkladIDOnline = chk_ProdejNacistSkladIDOnline.Checked;
			Prodej.Globals.PovolitTiskEtikety = chk_ProdejPovolitTiskEtikety.Checked;
			Prodej.Globals.PredvyplneneMnozstviEtikety = txt_ProdejTiskEtiketyPredvyplneneMnozstvi.Text;
			Prodej.Globals.PredvyplneneMnozstviSoupisu = txt_ProdejTiskSoupisuPredvyplneneMnozstvi.Text;
			Prodej.Globals.PovolitTiskSoupisu = chk_ProdejPovolitTiskSoupisu.Checked;
			Prodej.Globals.TiskSoupisuPriUzavreniDavky = chk_ProdejTiskSoupisuPriUzavreniDavky.Checked;
			Prodej.Globals.TiskEtiketyPoPridaniZbozi = chk_ProdejTiskEtiketyPoPridaniZbozi.Checked;
			Prodej.Globals.EtiketaTisk_PrebiratMnozstvi = chk_ProdejTiskEtiketyPrebiratMnozstvi.Checked;


			Prodej.Globals.PovolitPrevodMeziSklady = chk_ProdejPovolitPrevodMeziSklady.Checked;
			Prodej.Globals.ExistenceNasnimanePolozky = chk_Prodej_ExistenceNasnimanePolozky.Checked;

			#region TaD Prodej Dialogy

			Prodej.Globals.ProdejDialogUspesnehoOdeslaniDavky = chk_Prodej_DialogUspesnehoOdeslani.Checked;
			Prodej.Globals.ProdejDialogOpusteniModulu = chk_Prodej_DialogOpusteniModulu.Checked;
			Prodej.Globals.ProdejDialogSmazaniDavky = chk_Prodej_DialogSmazaniDavky.Checked;
			Prodej.Globals.ProdejDialogUkonceniZpracobaniDavky = chk_Prodej_DialogUkonceniZpracobaniDavky.Checked;
			Prodej.Globals.ProdejDialogNasnimanaLokace = chk_Prodej_DialogNasnimanaLokace.Checked;
			Prodej.Globals.EtiketaTiskDotazSCenou = chk_Prodej_EtiketaTiskDotazSCenou.Checked;

			Prodej.Globals.EtiketaTiskDotazSCenou_Cena = chk_Prodej_EtiketaTiskDotazSCenou_Cena.Checked;
			Prodej.Globals.EtiketaTiskDotazSCenou_ZobrazDialog = chk_Prodej_EtiketaTiskDotazSCenou_ZobrazDialog.Checked;

			Prodej.Globals.DialogTisk = chk_Prodej_DialogTisk.Checked;

			#endregion

			#region F10 možnosti

			Prodej.Globals.F10_OnlinePocetKusuSklad = rbProdej_OnlinePocetKusuSklad.Checked;
			Prodej.Globals.F10_ZobrazitAlternativyLokaci = rbProdej_ZobrazitAlternativyLokaci.Checked;
			Prodej.Globals.F10_TiskEtiketa = rbProdej_TiskEtiketa.Checked;

			#endregion

			Prodej.Globals.PovolitParsovaniMnozstvi = cfgProdejPovolitParsovaniMnozstvi.Checked;
			Prodej.Globals.PovolitParsovaniSarze = cfgProdejPovolitParsovaniSarze.Checked;



			if (!Prodej.Globals.Save(Main.ConfigModulesFileName))
				throw new Exception("Nepodařilo se uložit konfiguraci modulu prodeje");
		}

		#region Konfig_Save_MST_Global

		private void Konfig_Save_MST_Global()
		{
			Konfig_Save_MST_Global_Events();
			Konfig_Save_MST_Global_Expedice();
			Konfig_Save_MST_Global_Tasks();
			Konfig_Save_MST_Global_Inventura();
			Konfig_Save_MST_Global_Inventura2();
			Konfig_Save_MST_Global_Vydej();
			Konfig_Save_MST_Global_OnScannerSound();
			Konfig_Save_MST_Global_Online();
			Konfig_Save_MST_Global_Prijem();

			if (!MST_Global.Save(Main.ConfigModulesFileName))
				throw new Exception("Nepodařilo se uložit globální konfiguraci modulů");
		}

		private void Konfig_Save_MST_Global_OnScannerSound()
		{
			MST_Global.OnScannerSound_Inventura1_sqlc = cbOnScannerInvetura1.Checked;
			MST_Global.OnScannerSound_Inventura2 = cbOnScannerInventura2.Checked;
			MST_Global.OnScannerSound_Expedice = cbOnScannerExpedice.Checked;
			MST_Global.OnScannerSound_Online = cbOnScannerOnline.Checked;
			MST_Global.OnScannerSound_Prijem_4 = cbOnScannerPrijem.Checked;
			MST_Global.OnScannerSound_Prodej_3 = cbOnScannerProdej.Checked;
			MST_Global.OnScannerSound_Vydej_3 = cbOnScannerVydej.Checked;
			MST_Global.OnScannerSound_ServisModul = cbOnScannerServis.Checked;
		}

		private void Konfig_Save_MST_Global_Vydej()
		{
			MST_Global.VydejLocationQuestion = cfgVydejLocationQuestion.Checked;
			MST_Global.VydejItemTypeQuestion = cfgVydejItemTypeQuestion.Checked;
			MST_Global.VydejTypOznaceniPalety = cfgVydejTypOznaceniPalety.Checked;
			MST_Global.VydejTypyPoctyPalet = cfgVydejTypyPoctyPalet.Checked;
			MST_Global.VydejSequence = Convert.ToInt32(txt_vydejPrefix.Text.Trim());

			MST_Global.VydejPocetDavekZobraz = Convert.ToInt32(cfgVydejPocetDavekZobraz.Value);
			MST_Global.VydejMaxPocetDavek = Convert.ToInt32(cfgVydejMaxDavek.Value);
			MST_Global.VydejLocationPouzitCiselnik = cfgVydejLocationPouzitCiselnik.Checked;
			MST_Global.VydejLocationFiltrovatData = cfgVydejLocationFiltrovatData.Checked;
			MST_Global.VydejLocationAllowAutocommit = cfgVydejLocationAllowAutocommit.Checked;
			MST_Global.VydejPokracovatNaJinemTerminalu = cfgVydejPokracovatNaJinemTerminalu.Checked;
			MST_Global.VydejHledaniCkAutoVyberPrvniNeuplne = chk_VydejHledaniCkAutoVyberPrvniNeuplne.Checked;
			MST_Global.VydejRozsireni1Rezerva2Zadavat = chkVydejRozsireni1Rezerva2Zadavat.Checked;
			MST_Global.VydejRozsireni1Rezerva2Nazev = txtVydejRozsireni1Rezerva2Nazev.Text;
			MST_Global.VydejRozsireni1Rezerva2Default = txtVydejRozsireni1Rezerva2Default.Text;
			MST_Global.VydejZboziPouzitCiselnik = cfgVydejZboziPouzitCiselnik.Checked;
			MST_Global.VydejZboziVyberJenScannerem = cfgVydejZboziJenScannerem.Checked;
			MST_Global.VydejZboziVyhledaniDleSloupce = cfgVydejZboziVyhledavatDle.SelectedItem.ToString();
			MST_Global.VydejPovolitRazeniVydejek = cfgVydejPovolitRazeniVydejek.Checked;
			MST_Global.VydejStahnoutNejvyssiPrioritu = cfgVydejStahovatPouzeNejvyssiPriority.Checked;
			MST_Global.VydejSlucovaniDavek = cfgVydejSlucovaniDavek.Checked;
			MST_Global.VydejHromadneVyplneni = cfgVydejHromadneVyplneni.Checked;
			MST_Global.VydejParsovaniCarovehoKoduPovolit = cfgVydejParsovaniCarovehoKoduPovolit.Checked;

			MST_Global.Vydej_HromadneSN = cfgVydejHromadneZadavaniSN.Checked;
			try
			{
				MST_Global.Vydej_HromadneSN_N = int.Parse(cfgVydejHromadneZadavaniSN_N.Text);
			}
			catch (Exception ex)
			{
				MST_Global.Vydej_HromadneSN_N = 5;
			}
			#region TaD Dialogy Vydej
			MST_Global.VydejDialogUspesnehoOdeslaniDavky = chk_Vydej_DialogUspesnehoOdeslani.Checked;
			MST_Global.VydejDialogOpusteniModulu = chk_Vydej_DialogOpusteniModulu.Checked;
			MST_Global.VydejDialogOpusteniVydejky = chk_Vydej_DialogOpusteniVydejky.Checked;

			MST_Global.VydejTimeDialogNasnimana = chk_Vydej_DialogNasnimaniPolozky.Checked;
			MST_Global.VydejDialogDavkaNenalezenaVygenerovat = chk_Vydej_DialogDavkaNenalezenaVygenerovat.Checked;
			MST_Global.VydejDialogNaDiskuNejsouDavkyStahnout = chk_Vydej_DialogNaDiskuNejsouDavkyStahnout.Checked;
			try
			{
				MST_Global.VydejTimeDialogInterval = decimal.Parse(tb_VydejInterval.Text);
			}
			catch
			{
				MST_Global.VydejTimeDialogInterval = 3;
			}


			MST_Global.VydejDialogTiskSoupis = chk_Vydej_DialogTiskSoupis.Checked;
			MST_Global.VydejDialogPokracovatNaJinemTerm = chk_Vydej_DialogPokracovatNaJinemTerm.Checked;
			MST_Global.Vydej_PokracovatJinyTerm_AnoNe = (Fask.MST_W.Vydej_3.Potvrzeni_AnoNe)Enum.Parse(typeof(Fask.MST_W.Vydej_3.Potvrzeni_AnoNe), (string)cb_Vydej_Pokracovat_AnoNe.SelectedItem, true);

			MST_Global.VydejTiskSoupisMN_Auto1 = cb_Vydej_TiskSoupisMN_Auto1.Checked;

			#endregion

			// vydej sklady
			MST_Global.VydejSkladPouzit = cfgVydejSkladPouzit.Checked;
			MST_Global.VydejSkladID = cfgVydejSkladID.Text;
			MST_Global.VydejPrevzitIDSkladuZCiselnikuSkladu = cfgVydejPrevzitIDSkladZCisSklad.Checked;
			MST_Global.VydejFiltrCiselnikSkladuOnlyOne = cfgVydejCiselnikSkladuOnlyOne.Checked;
			MST_Global.VydejGenerovaniPrikazuZadatSklad = cfgVydejGenPrikPozadovatSklad.Checked;

			MST_Global.VydejObjednavkaDetail = chckVydejObjednavkaDetailOnline.Checked;
			MST_Global.VydejPocetKusuNaSkladeOnline = chckVydejPocetKusuNaSkladeOnline.Checked;
			MST_Global.VydejPocetKusuOnline = chckVydejPocetKusuPolozkaOnline.Checked;
			MST_Global.VydejPolozkaDetailOnline = chckVydejPolozkaDetailOnline.Checked;
			MST_Global.VydejPovolitZmenuOdberatele = chckVydejPovolitZmenuOdberatele.Checked;
			MST_Global.VydejGenerovatDataPrikazuOnline = chckVydejGenerovatDataPrikazuOnline.Checked;
			MST_Global.VydejEtiketaTiskPoVlozeniDotaz = cfgVydejEtiketaTiskPoVlozeniDotaz.Checked;
			MST_Global.VydejPolozkyVyberJenScannerem = cfgVydejPolozkyVyberJenScannerem.Checked;
			MST_Global.VydejVyberTiskarnyDokladu = cfgVydejPovolitVyberTiskarnyDokladu.Checked;
			MST_Global.VydejVyberPracovnika = cfgVydejPovolitVyberPracovnika.Checked;
			MST_Global.VydejDavkyVyberJenScannerem = cfgVydejDavkyVyberJenScannerem.Checked;
			MST_Global.VydejPovolitKontrolaSNPredloha = cfgVydejPovolitKontrolaSNPredloha.Checked;
			MST_Global.VydejGenerovatNenalezenouVydejku = cfgVydejGenNenalezPrijumku.Checked;
			MST_Global.VydejPovolitOcipovani = cfgVydejPovolitOcipovani.Checked;

			MST_Global.VydejRez1Cislo = cfg_VydejRez1Cislo.Checked;
			MST_Global.VydejRez1Povinne = cfg_VydejRez1Povinne.Checked;
			MST_Global.VydejRez1Pamatovat = cfg_VydejRez1Pamatovat.Checked;
			MST_Global.VydejRez2Cislo = cfg_VydejRez2Cislo.Checked;
			MST_Global.VydejRez2Povinne = cfg_VydejRez2Povinne.Checked;
			MST_Global.VydejRez2Pamatovat = cfg_VydejRez2Pamatovat.Checked;

			MST_Global.VydejTiskVariantaSoupis = (Fask.MST_W.Vydej_3.Varianta_TiskSoupis)Enum.Parse(typeof(Fask.MST_W.Vydej_3.Varianta_TiskSoupis), (string)cb_VydejTiskVariantaSoupis.SelectedItem, true);

            // vydej online
            MST_Global.Vydej_Items_Online = chkVydejItemsOnline.Checked;
			MST_Global.Vydej_FIFOFEFO_Online = chkVydej_FIFOFEFO_Online.Checked;
			MST_Global.Vydej_ExpiraceCheck_Online = chkVydej_ExpiraceCheck_Online.Checked;
			MST_Global.Vydej_TypSPrelokovanim = chkVydej_TypSPrelokovanim.Checked;

			MST_Global.Vydej_MnozstviAutoJedna = chkVydej_MnozstviAutoJedna.Checked;

			#region F10 možnosti

			MST_Global.F10_ZobrazitAlternativyLokaci = rbVydej_F10_ZobrazitAlternativyLokaci.Checked;
			MST_Global.F10_TiskPalListku = rbVydej_F10_TiskPalListku.Checked;

			#endregion

		}

		private void Konfig_Save_MST_Global_Inventura2()
		{

			MST_Global.Inventura2ShowInMST = cfgInventura2ShowInMain.Checked;

			MST_Global.Inventura2DotazKancl = cfgInventura2DotazKancelar.Checked;
			MST_Global.Inventura2DotazLokace = cfgInventura2DotazLokace.Checked;
			MST_Global.Inventura2DotazOsoba = cfgInventura2DotazOsoba.Checked;
			MST_Global.Inventura2DotazStredisko = cfgInventura2DotazStredisko.Checked;

			#region TaD Dialogy Inventura2

			MST_Global.Inventura2_DialogOpusteniModulu = chk_Inventura2_DialogOpusteniModulu.Checked;

			#endregion
		}

		private void Konfig_Save_MST_Global_Inventura()
		{
			MST_Global.Inventura1ShowInMST = cfgInventuraShowInMain.Checked;
			MST_Global.inventura1ZadaniLocncodePredSN = cfg_inventuraZadaniLokacePredSN.Checked;
			MST_Global.inventura1ZadaniLocncodeJednou = cfg_inventuraZadaniLokaceJednou.Checked;
			MST_Global.inventura1ZadaniLocncodePamatovatPosledni = cfg_inventuraZadaniLokacePamatovatPosledni.Checked;
			MST_Global.Inventura1PolozkaNasnimatPouzeJednou = cfg_inventura1PolozkaNasnimatPouzeJednou.Checked;
			MST_Global.Inventura1OnlineKontrola = cfg_inventuraOnlineKontrola.Checked;
			MST_Global.Inventura1OnlineTimeout = int.Parse(cfgInventura1OnlineTimeout.Text);
			MST_Global.Inventura1ChunkTimeout = int.Parse(cfgInventura1ChunkTimeout.Text);
			MST_Global.Inventura1PouzitCiselnikSkladu = chckInventura1PouzitCiselnikSkladu.Checked;
			MST_Global.Inventura1ParsovaniCarovehoKoduPovolit = cfgInventura1ParsovaniCarovehoKoduPovolit.Checked;
			MST_Global.Inventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit = cfgInventura1KontrolaSarzeUParsovanehoCarovehoKoduPovolit.Checked;
			MST_Global.Inventura1PolozkyVyberJenScannerem = cfgInventura1PolozkyVyberJenScannerem.Checked;
			MST_Global.Inventura1REZ1Nazev = cfgInventura1REZ1Nazev.Text.Trim();
			MST_Global.Inventura1REZ1IsNumber = cfgInventura1REZ1IsNumber.Checked;
			MST_Global.Inventura1REZ1Mandatory = cfgInventura1REZ1Mandatory.Checked;
			MST_Global.Inventura1REZ2Nazev = cfgInventura1REZ2Nazev.Text.Trim();
			MST_Global.Inventura1REZ2IsNumber = cfgInventura1REZ2IsNumber.Checked;
			MST_Global.Inventura1REZ2Mandatory = cfgInventura1REZ2Mandatory.Checked;

			MST_Global.Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu = Inventura1ZobrazitDialogZadaniMnozstviParsovanehoKodu.Checked;

			#region TaD Dialogy Inventura1

			MST_Global.Inventura1_DialogOpusteniModulu = chk_Inventura1_DialogOpusteniModulu.Checked;

			#endregion
		}

		private void Konfig_Save_MST_Global_Prijem()
		{
			MST_Global.PrijemTimeDialog = cfgPrijemTimeDialog.Checked;
			MST_Global.PrijemTimeDialogInterval = cfgPrijemTimeDialogInterval.Value;
		}

		private void Konfig_Save_MST_Global_Online()
		{
			MST_Global.OnlineObjednavkaDetailPovolit = chckPrijemObjednavkaDetailPovolit.Checked;
			MST_Global.OnlinePolozkaDetailPovolit = chckPrijemOnlinePolozkaDetailPovolit.Checked;
			MST_Global.OnlinePolozkaKusuNaSkladePovolit = chckPrijemPolozkaKusuNaSkladePovolit.Checked;
			MST_Global.OnlinePolozkaKusuPovolit = chckPrijemOnlinePolozkaKusuPovolit.Checked;
		}

		private void Konfig_Save_MST_Global_Events()
		{
			MST_Global.EventsEnable = eventsEnable.Checked;
			MST_Global.EventsShowInMST = eventsShowInMST.Checked;
			MST_Global.EventsOnline = eventsOnline.Checked;
			MST_Global.EventsOnlineTimeout = int.Parse(eventsOnlineTimeout.Text);
			MST_Global.EventsSynchronizationInterval = int.Parse(eventsSynchronizationInterval.Text);
		}

		private void Konfig_Save_MST_Global_Expedice()
		{
			// Expedice
			MST_Global.Expedice = cfgExpediceAllow.Checked;
			MST_Global.ExpediceShowInMST = cfgExpediceShowInMain.Checked;
			MST_Global.ExpediceName = cfgExpediceName.Text;
		}

		private void Konfig_Save_MST_Global_Tasks()
		{
			MST_Global.TasksEnable = cfgUkolyPovolit.Checked;
			MST_Global.TasksShowInMST = cfgUkolyZobrazitMST.Checked;
			MST_Global.TasksTimeout = int.Parse(cfgUkolyTimeout.Text);
			MST_Global.TasksSynchronizationInterval = int.Parse(cfgUkolySynchronizationInterval.Text);
			MST_Global.TasksNotifyDialogShowSeconds = int.Parse(cfgUkolyNotificationShowInterval.Text);
		}

		#endregion

		#endregion

	}
}
