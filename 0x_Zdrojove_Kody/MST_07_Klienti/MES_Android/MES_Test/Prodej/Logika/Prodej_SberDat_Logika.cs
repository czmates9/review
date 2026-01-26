using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fask.Parsing.Codes;
using Fask.SQLiteDBs.DataSets;
using MES_Android._WebReferences_Globals;
using MES_Android.Classes;
using MES_Android.ProdejService;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MES_Android.MessageBox;

namespace MES_Android.Prodej.Logika
{
    public class Prodej_SberDat_Logika
    {

        #region Parametry

        private Prodej_SberDat _parent;
        public Prodej_SberDat Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row _typdokladu;
        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row Typdokladu
        {
            get { return _typdokladu; }
            set { _typdokladu = value; }
        }



        private int _cisloDavky;
        public int CisloDavky
        {
            get { return _cisloDavky; }
            set { _cisloDavky = value; }
        }

        private string GetString(int ID)
        {
            return _parent.Resources.GetString(ID);
        }

        #endregion

        public Prodej_SberDat_Logika(Prodej_SberDat Parent, int CisloDavky, Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row Typdokladu)
		{
			this._parent = Parent;
			this._cisloDavky = CisloDavky;
			this._typdokladu = Typdokladu;
		}


            /// <summary>
            /// Metoda pro pridani položky 
            /// TADY je cela logika volneho pohybu,
            /// TODO rozudelit ma menší submetody aby to bylo prehlednejší
            /// </summary>
            /// <param name="zbozi"></param>
            /// <param name="code"></param>
            public  void pridatPolozku(
			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
			BaseCode code = null,
			byte _input_mode = 0,
			string serltnum = "",
			ExpediceService.SSCC nmbrpal = null,
			Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096Row _pracovnik = null
			)
		{
         
			#region OK Kontrola, zda je mozne vybirat i necim jinacim nez scannerem

			if (!InputModeChecker.checkInputMode(Konfigurace_Singleton.Instance.Prodej.PolozkyVyberJenScannerem, _input_mode))
			{
				MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkuJdeZadatPouzeScannerem), "Info", MessageBoxButtons.OK);
				return;
			}

			#endregion

			#region OK Kontrola existence objektu zbozi

			if (zbozi == null)
			{
				MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniVybranoZbozi), GetString(Resource.String.Prodej3ProdejListPridatPolozku), MessageBoxButtons.OK);
				return;
			}

            #endregion

            #region OK kontrola ExistenceNasnimanePolozky

            if (Konfigurace_Singleton.Instance.Prodej.ExistenceNasnimanePolozky)
            {
				bool nalez = false;
                foreach (object diRowO in _parent.mAdapter.PolozkaSeznam.mItems.Rows)
                {
					Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diRow = (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow)diRowO;

					string carkodDI = diRow.IsCZ_CarKodNull() ? string.Empty : diRow.CZ_CarKod.Trim();
					string carkodZbozi = zbozi.IsCZ_CarKodNull() ? string.Empty : zbozi.CZ_CarKod.Trim();

					if (carkodDI == carkodZbozi)
					{
						nalez = true;
						break;
					}
				}


                //if (_parent.mAdapter.PolozkaSeznam.mItems.Any(x => x.CZ_CarKod == zbozi.CZ_CarKod))
				if(nalez)
				{
                    //TODO lokalizovat
                    string message = "Položka již byla nasnímana." + System.Environment.NewLine + "Pokračovat?";
                    if (MessageBox.Show(_parent, message, "Dotaz", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
            }

            #endregion

            #region OK Parametry + okna

            string rez1 = string.Empty;
			string rez2 = string.Empty;
			string rez3 = string.Empty;
			string rez4 = string.Empty;
			string sklad_id = string.Empty;
			string sklad_id_dest = string.Empty;

			#endregion

			try
			{
				_parent.ScannerStop();

				#region OK Sklad / Zdrojovy sklad

				if (_typdokladu != null && !_typdokladu.Iscfg_skladyNull() && _typdokladu.cfg_sklady > 0)
				{
					if (_parent._Item.SkladZdroj == null)
						sklad_id = string.Empty;
					else
						sklad_id = _parent._Item.SkladZdroj.skl_id.Trim();
				}
				else
				{
					// posle se id skladu ze zbozi
					sklad_id = zbozi.IsSKL_IDNull() ? string.Empty : zbozi.SKL_ID;
				}

				#endregion

				#region OK Cilovy sklad

				// ma se prevzit id skladu
				if (_typdokladu != null && !_typdokladu.Iscfg_skl_id_dest_prevzitNull() && _typdokladu.cfg_skl_id_dest_prevzit > 0)
				{
					sklad_id_dest = sklad_id;
				}
				else if (_parent._Item.SkladCil != null) // je vybran cilovy sklad
				{
					sklad_id_dest = _parent._Item.SkladCil.skl_id;
				}

				#endregion

				#region OK Parametry lokalne

				decimal qty = 0;
				string sn = string.Empty;
				DateTime? expirace = null;

				#endregion

				#region OK sarze z parsovaneho / ze zbozi

				if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr) && !String.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
					sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
				else
					sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();

				#endregion

				#region OK Lokace ze zbozi

				string locncode = zbozi.LOCNCODE; //28.6.2013 JiS: oprava melo by byt z zbozi a ne _zbozi

				#endregion

				#region OK zjisteni ID zdrojoveho a ciloveho skladu online (ANC)
				// nacteni skladu online ...
				if (Konfigurace_Singleton.Instance.Prodej.NacistSkladIDOnline)
				{
					ProdejService.STATUS status = OnlineGetSklad(
						_typdokladu != null ? _typdokladu.doc_id : string.Empty, 
						zbozi.ITEMNMBR, 
						zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM, 
						out sklad_id, 
						out sklad_id_dest);

					if (status == ProdejService.STATUS.ERROR) // chyba, ukoncit ...
						return;
					else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
					{
						MessageBox.Show(_parent,string.Format("Nepodařilo se načíst ID skladu online pro materiál '{0}'", zbozi.ITEMNMBR.Trim()), "Error", MessageBoxButtons.OK);
						return;
					}
				}
                #endregion

                #region OK Doporucene palety

                if (_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) 
                {
                    ProdejService.Location ds = OnlineGetMaterial(zbozi.ITEMNMBR, sklad_id, sn);
                    if (ds != null)
                    {
                        // vratily se nejake zaznamy
                        if (ds.CZMST_SkladLokace_Stav.Count > 1)
                            {
                            string LOCNCODE = string.Empty;
                            string SERLTNUM = string.Empty;
                            decimal QTY = 0;

                            ZadejVyberMaterialu(
                                ds,
                                out QTY, 
                                out SERLTNUM,
                                out LOCNCODE
                                );

                            zbozi.QTY = qty = QTY;
                            zbozi.SERLTNUM = sn = SERLTNUM.Trim();
                            zbozi.LOCNCODE = locncode = LOCNCODE.Trim();

                            //using (ProdejVyberMaterialuList pvpl = new ProdejVyberMaterialuList(ds, _typdokladu.doc_id))
                            //{
                            //    if (pvpl.ShowDialog() == DialogResult.OK)
                            //    {
                            //        zbozi.QTY = qty = pvpl.Qtyshppd;
                            //        zbozi.SERLTNUM = sn = pvpl.Serltnum.Trim();
                            //        zbozi.LOCNCODE = locncode = pvpl.Locncode.Trim();
                            //        //zbozi.EXPIRACE = expirace = pvpl.Expirace;
                            //    }
                            //}
                        }
                        else if (ds.CZMST_SkladLokace_Stav.Count == 1)
                        {
                            zbozi.QTY = qty = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;
                            zbozi.SERLTNUM = sn = ds.CZMST_SkladLokace_Stav[0].SERLTNUM;
                            zbozi.LOCNCODE = locncode = ds.CZMST_SkladLokace_Stav[0].LOCNCODE;
                            //zbozi.EXPIRACE = expirace = ds.CZMST_SkladLokace_Stav[0].EXPIRATION;
                        }
                        else
                        {
                            // nebyl nalezen material, dotaz zdali presto pokracovat ...
                            DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListDoporucenePaletyNenalezenyPokracovatDotaz), "Otazka", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.No)
                                return;
                        }
                    }
                    else
                    {
                        // data se nepodarila nacist ze serveru, dotaz zdali pokracovat
                        DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemPokracovatDotaz), "Otazka", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region OK SN na davku

                // prednastaveni SN, pokud je povoleno
                if (_typdokladu != null && !_typdokladu.Iscfg_sn_na_davkuNull() && _typdokladu.cfg_sn_na_davku > 0)
					zbozi.SERLTNUM = sn = serltnum;

				#endregion

				#region OK Lokace z typu dokladu

				// pokud je vyplneno locncode, automaticky se pouzije ... (nehledne na povoleni zadani zdrojove lokace
				if (_typdokladu != null && !string.IsNullOrEmpty(_typdokladu.LOCNCODE.Trim()))
					locncode = _typdokladu.LOCNCODE.Trim();

                #endregion

                #region OK Lokace pred SN

                ////string locncode = string.Empty;
                ////string locncode = _zbozi.LOCNCODE; //25.4.2012 JiS: locncode se prednastavi z vybraneho zbozi
                //string locncode = zbozi.LOCNCODE; //28.6.2013 JiS: oprava melo by byt z zbozi a ne _zbozi
                if (Konfigurace_Singleton.Instance.Prodej.ZadaniLocncodePredSN && (_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0))
                {
                    if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
                    {
                        //                // rucni vyber lokace, pokud neni jiz zvolen
                        //                using (FormLokaceVyber fsv = new FormLokaceVyber(sklad_id))
                        //                {
                        //                    //fsv.Text = "Výběr lokace";
                        //                    if (fsv.ShowDialog(_parent) == DialogResult.Cancel)
                        //                        return;

                        //                    locncode = fsv.Lokace.LOCNCODE;

                        //                    if (locncode == null)
                        //                    {
                        //                        //Fask.Logging.ExceptionHandler2.Handle("Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                        //Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error ,"Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                        //return;
                        //                    }
                        //                }

                        locncode = GetLokaci(sklad_id, "Lokace");

                        if (locncode == null)
                        {
                            //Fask.Logging.ExceptionHandler2.Handle("Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                            return;
                        }

                    }
                    else //if (!string.IsNullOrEmpty(locncode.Trim()))                 
                    {
                        locncode = SejmiLocncode(zbozi, true);
                        if (locncode == "!@")
                            return;
                    }
                }

                #endregion

                #region OK Sledovani na Množství

                if (zbozi.CZ_SerNum_Track == 0) //sledovano na mnozstvi
                {
                    if (!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                    {
                        WeightCode wc = (WeightCode)code;

                        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        qty =
                            (wc.weight
                            / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                            / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                            );
                    }
                    if (!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode_12)
                    {
                        WeightCode_12 wc = (WeightCode_12)code;

                        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        qty =
                            (wc.weight
                            / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                            / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                            );
                    }
                    else if ((_typdokladu != null && !_typdokladu.Iscfg_mnozstvi_ze_zboziNull() && _typdokladu.cfg_mnozstvi_ze_zbozi > 0) && zbozi.QTY > 0)     // prevzit mnozstvi 
                    {
                        qty = zbozi.QTY;
                    }
                    else
                    {
                       var naplnpMnozstvi = new ProdejPridatPolozku(GetString(Resource.String.Prodej3ProdejListMnozstvi), Android.Text.InputTypes.ClassNumber, 0, false, false, "", null, null, Konfigurace_Singleton.Instance.Prodej.PovolitZadaniMnozstviScannerem);
                        bool baleni = zbozi.QTYPACK > 0;
                        naplnpMnozstvi.Odberatel = _parent._Item.Odberatel;
                        naplnpMnozstvi.Zbozi = zbozi;
                        naplnpMnozstvi.Serltnum = sn;
                        naplnpMnozstvi.Text = baleni ? GetString(Resource.String.Prodej3ProdejListVlozteMnozstviBaleni) : GetString(Resource.String.Prodej3ProdejListVlozteMnozstvi);
                        naplnpMnozstvi.Popis = baleni ? GetString(Resource.String.Prodej3ProdejListMnozstviBaleni) : GetString(Resource.String.Prodej3ProdejListMnozstvi);
                        naplnpMnozstvi.Volajici = Volajici_ProdejPridatPolozku.QTY;

                        if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
                        {
                            naplnpMnozstvi.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString();
                        }
                        else if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                        {
                            naplnpMnozstvi.Kod = "1";
                        }
                        else
                        {
                            if (Konfigurace_Singleton.Instance.Prodej.MnozstviREZ1Vypln && !zbozi.IsREZ1Null())
                            {
                                naplnpMnozstvi.Kod = zbozi.REZ1.Trim();
                            }
                            else if (code is WeightCode)
                            {
                                WeightCode wc = (WeightCode)code;
                                naplnpMnozstvi.Kod =
                                    (wc.weight
                                    / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                                    / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                                    ).ToString(Config.Settings.UIFormatDesCisel);
                            }
                            else if (code is WeightCode_12)
                            {
                                WeightCode_12 wc = (WeightCode_12)code;
                                naplnpMnozstvi.Kod =
                                    (wc.weight
                                    / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                                    / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                                    ).ToString(Config.Settings.UIFormatDesCisel);
                            }
                            else if (_typdokladu != null && !_typdokladu.Iscfg_predvyplnit_mnozstviNull() && _typdokladu.cfg_predvyplnit_mnozstvi > 0)   //_typdokladu.cfg_predvyplnit_mnozstvi >0 .... dodelat
                                                                                                                                                         //else if (((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety) && qtyDopPal != 0)
                                naplnpMnozstvi.Kod = zbozi.QTY.ToString(Config.Settings.UIFormatDesCisel);
                            else
                                naplnpMnozstvi.Kod = "";

                            naplnpMnozstvi.CodeType = Android.Text.InputTypes.ClassNumber;
                            if (naplnpMnozstvi.ShowDialog(_parent) == DialogResult.Cancel)
                                return;
                        }

                        if (Konfigurace_Singleton.Instance.Prodej.KontrolaStavuSkladu)     // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
                        {
                            if (System.Convert.ToDecimal(naplnpMnozstvi.Kod) > zbozi.QTY)
                            {
                                if (MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz), GetString(Resource.String.Prodej3ProdejListInfo), MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }

                        qty = decimal.Parse(naplnpMnozstvi.Kod);
                    }
                }

                #endregion

                #region OK Sledovani na Sarze/SN

                else if ((zbozi.CZ_SerNum_Track == 1) || (zbozi.CZ_SerNum_Track == 2)) //sledovano na seriova cisla
                {
                    if (!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode)
                    {
                        WeightCode wc = (WeightCode)code;

                        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        qty =
                            (wc.weight
                            / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                            / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                            );
                    }
                    if (!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu && code is WeightCode_12)
                    {
                        WeightCode_12 wc = (WeightCode_12)code;

                        //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        qty =
                            (wc.weight
                            / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                            / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                            );
                    }
                    else if (_typdokladu != null && _typdokladu.cfg_mn2sn > 0) 
                    {
                        qty = 1;
                        //Pokud se toto vklada, je nutne zmensit rozsah na delku sn( char 21)
                        //sn = _zbozi.VNDITNUM;
                        string vnditnum = zbozi.VNDITNUM.Trim();
                        try
                        {
                            // Tohle stejne fungovat nebude, ani na MSTCE, protože Columns se nijak neinicializuje
                            //if (vnditnum.Length > (int)Fask.SQLiteDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength)
                            //	sn = vnditnum.Substring(0, (int)Fask.SQLiteDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength);
                            //else
                            //	sn = vnditnum;
                        }
                        catch { }
                    }
                    else
                    {
                        if ((zbozi.CZ_SerNum_Track == 2) && (code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr) && !String.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
                            sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
                        else
                        {

                            var naplnpSerialNumber = new ProdejPridatPolozku(GetString(Resource.String.Prodej3ProdejListSerioveCislo), Android.Text.InputTypes.ClassText, 0, true, false, "", null, null);
                            naplnpSerialNumber.CodeType = Android.Text.InputTypes.ClassText;
                            naplnpSerialNumber.Odberatel = _parent._Item.Odberatel;
                            naplnpSerialNumber.Zbozi = zbozi;
                            naplnpSerialNumber.Popis = zbozi.CZ_SerNum_Track == 1 ? GetString(Resource.String.Prodej3ProdejListSerioveCislo) : GetString(Resource.String.Prodej3ProdejListSarze);
                            naplnpSerialNumber.Text = zbozi.CZ_SerNum_Track == 1 ? GetString(Resource.String.Prodej3ProdejListVlozteSerioveCislo) : GetString(Resource.String.Prodej3ProdejListVlozteSarze);
                            //naplnpSerialNumber.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["SERLTNUM"].MaxLength;
                            naplnpSerialNumber.Len = (decimal)(zbozi.CZ_SerNum_Delka == 0 ? (_typdokladu.Iscfg_delka_SNNull() ? 0 : _typdokladu.cfg_delka_SN ) : zbozi.CZ_SerNum_Delka);
                            naplnpSerialNumber.Serltnum = sn;
                            //naplnpSerialNumber.Kod = "";
                            //naplnpSerialNumber.Kod = ((_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0) || Prodej.Globals.DoporucenePalety) ? sn : string.Empty;
                            naplnpSerialNumber.Kod = sn;
                            naplnpSerialNumber.Volajici = Volajici_ProdejPridatPolozku.SERLTNUM;
                            if (naplnpSerialNumber.ShowDialog(_parent) == DialogResult.Cancel)
                                return;

                            sn = naplnpSerialNumber.Kod;
                        }

                        qty = 1;


                    }

                    #region OK Test na kontrolu existence SN ve vystupu


                    int pocetSN = 0;
                    try
                    {
                        // 20160810 JiS - opraveno : test if (zbozi.czsernumtrac == 1 !!! <= SN ...
                        if (zbozi.CZ_SerNum_Track == 1)
                        {
                            pocetSN = System.Convert.ToInt32(_parent.mAdapter.PolozkaSeznam.mItems.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + zbozi.ITEMNMBR + "' AND SERLTNUM='" + sn + "'"));
                            if (pocetSN > 0)
                            {
                                MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListSerioveCisloJizByloNasnimano), "Error", MessageBoxButtons.OK);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
                        return;
                    }

                    #endregion

                    //sledovano na sarze
                    if (zbozi.CZ_SerNum_Track == 2)
                    {// sejme mnozstvi k SN                        
                     // ma se prebirat mnozstvi ze zbozi a soucasne je ruzne od 0
                        if ((_typdokladu != null && !_typdokladu.Iscfg_mnozstvi_ze_zboziNull() && _typdokladu.cfg_mnozstvi_ze_zbozi > 0) && zbozi.QTY > 0)     // prevzit mnozstvi 
                        {
                            qty = zbozi.QTY;
                        }
                        else   // ma se zadat mnozstvi
                        {
                           var naplnpMnozstvi = new ProdejPridatPolozku(GetString(Resource.String.Prodej3ProdejListMnozstvi), Android.Text.InputTypes.ClassNumber, 0, false, false, "", null, null, Konfigurace_Singleton.Instance.Prodej.PovolitZadaniMnozstviScannerem);
                            bool baleni = zbozi.QTYPACK > 0;
                            naplnpMnozstvi.Odberatel = _parent._Item.Odberatel;
                            naplnpMnozstvi.Zbozi = zbozi;
                            naplnpMnozstvi.Serltnum = sn;
                            naplnpMnozstvi.Text = GetString(Resource.String.Prodej3ProdejListVlozteMnozstvi);
                            naplnpMnozstvi.Popis = baleni ? GetString(Resource.String.Prodej3ProdejListMnozstviBaleni) : GetString(Resource.String.Prodej3ProdejListMnozstvi);
                            naplnpMnozstvi.Volajici = Volajici_ProdejPridatPolozku.QTY;
                            //naplnpMnozstvi.Kod = "";
                            //if (naplnpMnozstvi.ShowDialog() == DialogResult.Cancel)
                            //    return;
                            if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                            {
                                naplnpMnozstvi.Kod = "1";
                            }
                            else if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
                            {
                                naplnpMnozstvi.Kod = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value.ToString();
                            }
                            else
                            {
                                if (Konfigurace_Singleton.Instance.Prodej.MnozstviREZ1Vypln && !zbozi.IsREZ1Null())
                                {
                                    naplnpMnozstvi.Kod = zbozi.REZ1.Trim();
                                }
                                else if (code is WeightCode)
                                {
                                    WeightCode wc = (WeightCode)code;
                                    naplnpMnozstvi.Kod =
                                    (wc.weight
                                        / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                                        / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                                        ).ToString(Config.Settings.UIFormatDesCisel);
                                }
                                else if (code is WeightCode_12)
                                {
                                    WeightCode_12 wc = (WeightCode_12)code;
                                    naplnpMnozstvi.Kod =
                                    (wc.weight
                                        / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                                        / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                                        ).ToString(Config.Settings.UIFormatDesCisel);
                                }
                                else if (_typdokladu != null && !_typdokladu.Iscfg_predvyplnit_mnozstviNull() && _typdokladu.cfg_predvyplnit_mnozstvi > 0)   //_typdokladu.cfg_predvyplnit_mnozstvi >0 .... dodelat
                                    naplnpMnozstvi.Kod = zbozi.QTY.ToString(Config.Settings.UIFormatDesCisel);
                                else
                                    naplnpMnozstvi.Kod = "";

                                naplnpMnozstvi.CodeType = Android.Text.InputTypes.ClassNumber;
                                if (naplnpMnozstvi.ShowDialog(_parent) == DialogResult.Cancel)
                                    return;
                            }

                            if (Konfigurace_Singleton.Instance.Prodej.KontrolaStavuSkladu) // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
                            {
                                if (System.Convert.ToDecimal(naplnpMnozstvi.Kod) > zbozi.QTY)
                                {
                                    if (MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz), GetString(Resource.String.Prodej3ProdejListInfo), MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }

                            qty = decimal.Parse(naplnpMnozstvi.Kod);
                        }

                    }
                    #region OK sledovani Expirace
                    if (zbozi.CZ_Expirace_Track > 0)
                    {

                        if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeExpiration) && ((Fask.Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration.HasValue)
                            expirace = ((Fask.Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration;


						DateTime expiraceLast = DateTime.Now.AddDays(30);


						if (!expirace.HasValue)
                        {
                            //string expirationStr = expiraceLast.ToString(Main.expirationFormat);
                            //string expirationStr = string.Empty;
                            expirace = expiraceLast;
                            string expirationStr = expiraceLast.ToString(DataInfo_Static.dateFormatRRMMDD);

                            while (true)
                            {
                                var dResExpiration = InputBoxExpirace.Show(_parent, "Expirace (RRMMDD)", expirationStr, out expirationStr, true, Android.Text.InputTypes.ClassDatetime);
                                if (dResExpiration == DialogResult.Cancel)
                                    return;

                                // validace
                                try
                                {
                                    expirace = DataInfo_Static.Date_RRMMDD(expirationStr);
                                    //expirace = DateTime.ParseExact(expirationStr, Main.dateFormatRRMMDD, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(_parent, String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK);
                                    continue;
                                }

                                expiraceLast = expirace.Value;
                                break;
                            }
                        }
                    }

                    #endregion

                }
                #endregion

                #region OK Lokace pred SN

                if (!Konfigurace_Singleton.Instance.Prodej.ZadaniLocncodePredSN && (_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0))
                {
                    if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
                    {
                        //// rucni vyber lokace, pokud neni jiz zvolena
                        //using (FormLokaceVyber fsv = new FormLokaceVyber(sklad_id))
                        //{
                        //    //fsv.Text = "Výběr lokace";
                        //    if (fsv.ShowDialog(_parent) == DialogResult.Cancel)
                        //        return;

                        //    locncode = fsv.Lokace.LOCNCODE;

                        //    if (locncode == null)
                        //    {
                        //        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                        //        return;
                        //    }
                        //}

                        locncode = GetLokaci(sklad_id, "Lokace");

                        if (locncode == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                            return;
                        }

                    }
                    else // if (!string.IsNullOrEmpty(locncode))
                    {
                        locncode = SejmiLocncode(zbozi, true);
                        if (locncode == "!@")
                            return;
                    }
                }

                #endregion

                #region OK prepočet QTY a QTYPACK

                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = _parent.mAdapter.PolozkaSeznam.mItems.NewCZMST_DIRow();
				di.QTYSHPPD = qty * (zbozi.QTYPACK > 0 ? zbozi.QTYPACK : 1);

				#endregion

				#region OK Kontrola disponibility Zdrojový - online

				if (_typdokladu != null && _typdokladu.cfg_disp > 0) 
				{
					if (!Online_DISP( zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id, string.Empty, string.Empty))
						return;
				}

				#endregion

				#region OK Kontrola disponibility Cilový - online

				if (_typdokladu != null && _typdokladu.cfg_disp_dest > 0)
				{
					if (!Online_DISP( zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id_dest, string.Empty, string.Empty))
						return;
				}

                #endregion

                #region OK Online overeni zdrojove lokace

                if (_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokaceNull() && _typdokladu.cfg_onl_over_lokace > 0)
                {
                    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                    ProdejService.TypeOfRecord recordType;
                    if (!string.IsNullOrEmpty(pohyb_type))
                        recordType = (ProdejService.TypeOfRecord)Enum.Parse(typeof(ProdejService.TypeOfRecord), pohyb_type, true);
                    else recordType = ProdejService.TypeOfRecord.E;

                    ProdejService.StatusOverLokace so = OnlineOverLokace(zbozi.ITEMNMBR, sn, expirace, locncode, sklad_id, di.QTYSHPPD, ProdejService.TYPLokace.SOURCE, recordType);
                    if (so != null)
                    {
                        switch (so.State)
                        {
                            case ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                                break;
                            case ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                                DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListPorusenoDoporucenePoradiPokracovatDotaz), "Warning", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                    return;
                                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "zdrporadi", DateTime.Now, Config.Settings.TerminalID, _parent.Uzivatel.ID.Value, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncode, ""));
                                break;
                            case ProdejService.STATUSOverLokace.ERROR:
                                MessageBox.Show(_parent, so.Message.Trim(), "Error", MessageBoxButtons.OK);
                                return;
                            default: // neni mozne pokracovat
                                MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniMozneBratZLokace), "Ostatni", MessageBoxButtons.OK);
                                return;
                        }
                    }
                    else  // nic se nenacetlo
                    {
                        // chyba komunikace se serverem, dotaz zdali pokracovat
                        DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemOverLokaciPokracovatDotaz), "Info", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region OK Zde zacinaji doplnujici informace ...

                #region OK Strediska 

                if (_typdokladu != null && _typdokladu.cfg_str > 0) 
				{
					if (!StrediskoSet())
					{
						return;
					}
				}

				#endregion

				#region OK Pracovnici

				if (_typdokladu != null && _typdokladu.cfg_prac > 0) 
				{
					if (!PracovniciSet())
					{
						return;
					}
				}

				#endregion

				#region OK Palety

				if ((_typdokladu != null && _typdokladu.cfg_palety > 0 && nmbrpal == null))
				{
					// kontrola, zdali je paleta null, pokud ano, nepustit dal ...
					MessageBox.Show(_parent, "Není vybrána paleta", "Warning", MessageBoxButtons.OK);
					return;
				}

                #endregion

                //TODO: Pridelat do configProdej, kde je číslovani davek, tak i ukladani REZ nazvu a pamatovani posledni hodnoty

                #region OK REZ1

                //Doplnit hodnotu rez1
                // 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
                rez1 = zbozi.IsREZ1Null() ? string.Empty : zbozi.REZ1.Trim();
                if (zbozi.CZ_Rez1_Track > 0)
                {
					var skf = new SejmiKodForm();
					skf.CodeType = Konfigurace_Singleton.Instance.Prodej.REZ1.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText;
                    skf.AllowEmpty = !Konfigurace_Singleton.Instance.Prodej.REZ1.Povinne;
                    skf.CheckLen = false;
                    skf.Kod = Konfigurace_Singleton.Instance.Prodej.REZ1.Pamatovat ? Konfigurace_Singleton.Instance.Prodej.REZ1.LastValue : rez1;
                    skf.Len = 0;
                    //skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_1"].MaxLength;
                    skf.Popis = Konfigurace_Singleton.Instance.Prodej.REZ1.PROD_NAME;
                    skf.Text = GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty); //"Zadání doplňující hodnoty";
                    if (skf.ShowDialog(_parent) == DialogResult.Cancel)
                        return;
                    rez1 = skf.Kod;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ1.Pamatovat)
                        Konfigurace_Singleton.Instance.Prodej.REZ1.LastValue = rez1;
                }

                #endregion

                #region OK REZ 2

                //Doplnit hodnotu rez2
                // 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
                rez2 = zbozi.IsREZ2Null() ? string.Empty : zbozi.REZ2.Trim();
                if (zbozi.CZ_Rez2_Track > 0)
                {
					var skf = new SejmiKodForm();
					skf.CodeType = Konfigurace_Singleton.Instance.Prodej.REZ2.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText;
					skf.AllowEmpty = !Konfigurace_Singleton.Instance.Prodej.REZ2.Povinne;
                    skf.CheckLen = false;
                    skf.Kod = Konfigurace_Singleton.Instance.Prodej.REZ2.Pamatovat ? Konfigurace_Singleton.Instance.Prodej.REZ2.LastValue : rez2;
                    skf.Len = 0;
                    //skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_2"].MaxLength;
                    skf.Popis = Konfigurace_Singleton.Instance.Prodej.REZ2.PROD_NAME;
                    skf.Text = GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty); //"Zadání doplňující hodnoty";                  
                    if (skf.ShowDialog(_parent) == DialogResult.Cancel)
                        return;
                    rez2 = skf.Kod;

                    if (Konfigurace_Singleton.Instance.Prodej.REZ2.Pamatovat) Konfigurace_Singleton.Instance.Prodej.REZ2.LastValue = rez2;
                }

                #endregion

                #region OK REZ3

                //Doplnit hodnotu rez3
                // 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
                rez3 = zbozi.IsREZ3Null() ? string.Empty : zbozi.REZ3.Trim();
                if (zbozi.CZ_Rez3_Track > 0)
                {
					var skf = new SejmiKodForm();
					skf.CodeType = Konfigurace_Singleton.Instance.Prodej.REZ3.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText;
					skf.AllowEmpty = !Konfigurace_Singleton.Instance.Prodej.REZ3.Povinne;
                    skf.CheckLen = false;
                    skf.Kod = Konfigurace_Singleton.Instance.Prodej.REZ3.Pamatovat ? Konfigurace_Singleton.Instance.Prodej.REZ3.LastValue : rez3;
                    skf.Len = 0;
                    //skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_3"].MaxLength;
                    skf.Popis = Konfigurace_Singleton.Instance.Prodej.REZ3.PROD_NAME;
                    skf.Text = GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty); //"Zadání doplňující hodnoty";
                    if (skf.ShowDialog(_parent) == DialogResult.Cancel)
                        return;
                    rez3 = skf.Kod;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ3.Pamatovat) Konfigurace_Singleton.Instance.Prodej.REZ3.LastValue = rez3;
                }

                #endregion

                #region OK REZ4

                //Doplnit hodnotu rez4
                // 27.2.2015 JiS (MST_HO Slovensko) rez se nastavi ze zbozi, pripadne se zmeni
                rez4 = zbozi.IsREZ4Null() ? string.Empty : zbozi.REZ4.Trim();
                if (zbozi.CZ_Rez4_Track > 0)
                {
					var skf = new SejmiKodForm();
					skf.CodeType = Konfigurace_Singleton.Instance.Prodej.REZ4.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText;
					skf.AllowEmpty = !Konfigurace_Singleton.Instance.Prodej.REZ4.Povinne;
                    skf.CheckLen = false;
                    skf.Kod = Konfigurace_Singleton.Instance.Prodej.REZ4.Pamatovat ? Konfigurace_Singleton.Instance.Prodej.REZ4.LastValue : rez4;
                    skf.Len = 0;
                    //skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["REZ_4"].MaxLength;
                    skf.Popis = Konfigurace_Singleton.Instance.Prodej.REZ4.PROD_NAME;
                    skf.Text = GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty); //"Zadání doplňující hodnoty";
                    if (skf.ShowDialog(_parent) == DialogResult.Cancel)
                        return;
                    rez4 = skf.Kod;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ4.Pamatovat) Konfigurace_Singleton.Instance.Prodej.REZ4.LastValue = rez4;
                }

                #endregion

                #region OK Ceny

                Price price = new Price();

				GlobalObject.zjisti_cenu(zbozi, _parent._Item.Odberatel, _parent._Item.Mena, price); // price je objekt, tedy odkazem => meni se vlastnosti ...
                GlobalObject.nastav_cenu(di, price);

				#endregion

				#region OK Odberatel

				string odb_id = string.Empty;
				if (_typdokladu == null)
				{
					if (_parent._Item.Odberatel != null)
						odb_id = _parent._Item.Odberatel.odb_id;
					else
						odb_id = string.Empty;
				}
				else
				{
					if (_parent._Item.Odberatel != null)
						odb_id = _parent._Item.Odberatel.odb_id;
					else
						odb_id = zbozi.IsODB_IDNull() ? string.Empty : zbozi.ODB_ID;
				}
				#endregion

				#endregion

				#region OK Overovat pohyb

				if (Konfigurace_Singleton.Instance.Prodej.OverovatPohyb)
				{
					decimal outshppd = 0;
					decimal qtyshppdnacteno = Nacteno(zbozi.ITEMNMBR);
					if (!OverPohyb(sn, zbozi.ITEMNMBR, _parent._Item.SkladZdroj == null ? string.Empty : _parent._Item.SkladZdroj.skl_id, di.QTYSHPPD, qtyshppdnacteno, out outshppd))
						return;
					else if (_parent._Item.SkladZdroj != null && (di.QTYSHPPD + qtyshppdnacteno) > outshppd)
					{

						// TODO : dialog s odpovedi

						//DialogResult dr = MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Prodej3ProdejListMnozstviVetsiNezStavSkladuPokracovatDotaz, di.QTYSHPPD, qtyshppdnacteno, outshppd), this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
						//if (dr == DialogResult.No)
						//	return;
					}
				}

                #endregion

                #region OK zadani ciloveho skladu

                if (string.IsNullOrEmpty(sklad_id_dest.Trim()) && _typdokladu != null && !_typdokladu.Iscfg_skl_id_destNull() && _typdokladu.cfg_skl_id_dest > 0)
                {
                    Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row skladdest = null;//_skladCil;
                    string skl_id_dst = !_typdokladu.Ispredvyplnit_skl_id_destNull() ? _typdokladu.predvyplnit_skl_id_dest.Trim() : string.Empty;

                    try
                    {
                        // najiti skladu, pokud je v typu dokladu
                        if (!string.IsNullOrEmpty(skl_id_dst))
                        {
                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(skl_id_dst);
                            if (dt_sklady.Count > 0)
                                skladdest = dt_sklady[0];
                            else
                            {
                                MessageBox.Show(_parent, string.Format(GetString(Resource.String.Prodej3ProdejListCilovySkladNenalezenDotaz), skl_id_dst.Trim()), "info", MessageBoxButtons.OK);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
                        return;
                    }

                    // rucni vyber skladu, pokud neni jiz zvolen
                    if (skladdest == null)
                    {

                        skladdest = ZadejCilovySklad();


                        if (skladdest == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrán sklad, přestože je vyžadován!");
                            return;
                        }

                        //using (FormSkladVyber fsv = new FormSkladVyber())
                        //{
                        //    fsv.Text = GetString(Resource.String.Prodej3ProdejMainVyberCilovehoSkladu);
                        //    if (fsv.ShowDialog(_parent) == DialogResult.Cancel)
                        //        return;

                        //    skladdest = fsv.Sklad;

                        //    if (skladdest == null)
                        //    {
                        //        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrán sklad, přestože je vyžadován!");
                        //        return;
                        //    }
                        //}

                    }

                    sklad_id_dest = skladdest.skl_id;
                }

                #endregion

                #region OK Zadani cilove lokace

                string locncodedest = string.Empty;
                PrijemService.Obecne dsdest = null;
                bool onlineKontrolaCilLokace = true;
                locncodedest = (_typdokladu == null || _typdokladu.Ispredvyplnit_locncodedestNull()) ? string.Empty : _typdokladu.predvyplnit_locncodedest.Trim();
                if (_typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
                {
                    // vybrani cilove lokace z ciselniku lokaci
                    if (string.IsNullOrEmpty(locncodedest.Trim()) && _typdokladu != null && !_typdokladu.Iscfg_lokace_dest_ciselnikNull() && _typdokladu.cfg_lokace_dest_ciselnik > 0)
                    {
                        //// rucni vyber cilove lokace, pokud neni jiz zvolen
                        //using (FormLokaceVyber fsv = new FormLokaceVyber(sklad_id_dest))
                        //{
                        //    fsv.Text = GetString(Resource.String.Prodej3ProdejListVyberCiloveLokace);
                        //    if (fsv.ShowDialog(_parent) == DialogResult.Cancel)
                        //        return;

                        //    locncodedest = fsv.Lokace.LOCNCODE;

                        //    if (fsv.Lokace == null)
                        //    {
                        //         Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána cílová lokace z číselníku, přestože je vyžadována!");
                        //        return;
                        //    }
                        //}

                        locncodedest = GetLokaci(sklad_id_dest, GetString(Resource.String.Prodej3ProdejListVyberCiloveLokace));

                        if (locncodedest == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána cílová lokace z číselníku, přestože je vyžadována!");
                            return;
                        }

                    }
                    else if (!_typdokladu.Iscfg_onl_dop_lokace_destNull() && _typdokladu.cfg_onl_dop_lokace_dest > 0) 
                    {   // online doporucene cilove lokace
                        // zobrazit seznam
                        dsdest = OnlineGetDoporuceneCiloveLokace(zbozi.ITEMNMBR, sn, sklad_id_dest); //sklad_id);
                        if (dsdest != null)
                        {
                            // vratily se nejake zaznamy
                            if (dsdest.Lokace.Count > 0)
                            {
                                onlineKontrolaCilLokace = (_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0) ;
                                //using (PrijemVyberLokaceList pvpl = new PrijemVyberLokaceList(dsdest, sn, zbozi.ITEMNMBR, onlineKontrolaCilLokace, sklad_id_dest, zbozi.QTY)) //_sklad != null ? _sklad.skl_id : string.Empty))
                                //{
                                //    pvpl.Text = GetString(Resource.String.Prodej3ProdejListVyberCiloveLokace);  //"Výběr cílové lokace";
                                //    if (pvpl.ShowDialog(_parent) == DialogResult.OK)
                                //    {
                                //        //locncode = pvpl._lokaceRow.LOCNCODE;
                                //        locncodedest = pvpl.ResLocncode;
                                //        onlineKontrolaCilLokace = false;
                                //    }
                                //    else
                                //        return;
                                //}

                                locncodedest = GetLokaci(sklad_id_dest, GetString(Resource.String.Prodej3ProdejListVyberCiloveLokace));
                                onlineKontrolaCilLokace = false;
                            }
                            else
                            {
                                // nic se nevratilo, je treba zadat rucne ...
                                locncodedest = SejmiLocncode(zbozi, false);
                                if (locncodedest == "!@")
                                    return;
                            }
                        }
                        else
                        {
                            // nic se nevratilo, je treba zadat rucne ...
                            locncodedest = SejmiLocncode(zbozi, false);
                            if (locncodedest == "!@")
                                return;
                        }
                    }
                    else
                    {
                        // lokace se vyplnuje rucne
                        locncodedest = SejmiLocncode(zbozi, false);
                        if (locncodedest == "!@")
                            return;
                    }
                }

                #endregion

                #region ok Online overeni cilove lokace

                if ((_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0) && onlineKontrolaCilLokace)
                {
                    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                    ProdejService.TypeOfRecord recordType;
                    if (!string.IsNullOrEmpty(pohyb_type))
                        recordType = (ProdejService.TypeOfRecord)Enum.Parse(typeof(ProdejService.TypeOfRecord), pohyb_type, true);
                    else recordType = ProdejService.TypeOfRecord.E;

                    ProdejService.StatusOverLokace so = OnlineOverLokace(zbozi.ITEMNMBR, sn, expirace, locncodedest, sklad_id_dest, di.QTYSHPPD, ProdejService.TYPLokace.DEST, recordType);
                    if (so != null)
                    {
                        switch (so.State)
                        {
                            case ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                                break;
                            case ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                                DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListPorusenoDoporucenePoradiCilLokacePokracovatDotaz), "Warning", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                    return;
                                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "cilporadi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncodedest, ""));
                                break;
                            case ProdejService.STATUSOverLokace.ERROR:
                                MessageBox.Show(_parent, so.Message.Trim(), "Error", MessageBoxButtons.OK);
                                return;
                            default: // neni mozne pokracovat
                                MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniMozneUlozitNaLokaci), "Error", MessageBoxButtons.OK);
                                return;
                        }
                    }
                    else  // nic se nenacetlo
                    {
                        // chyba komunikace se serverem
                        DialogResult dr = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemOverCilLokaciPokracovatDotaz), "Error", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region OK Vyplneni DI jednoho řadku

                DateTime dtnow = DateTime.Now;
				Guid newGuid = Guid.NewGuid();
				di.CountEntries = _parent._Item.CisloDavky.Value;
				//di.ItemDescription = zbozi.ITEMDESC;
				di.ITEMNMBR = zbozi.ITEMNMBR;
				di.LOCNCODE = locncode;
				di.LOCNCODEDEST = locncodedest;
				di.ODB_ID = odb_id;
				di.STR_ID = (_parent._Item.Stredisko == null ? string.Empty : _parent._Item.Stredisko.str_id);
				di.PRAC_ID = (_pracovnik == null ? string.Empty : _pracovnik.prac_id);
				di.DOC_ID = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
				di.DOC_ID2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
				di.QTYPACK = zbozi.QTYPACK;
				di.REZ_1 = rez1;
				di.REZ_2 = rez2;
				di.REZ_3 = rez3;
				di.REZ_4 = rez4;
				di.SERLTNUM = sn;
				di.DATEDONE = dtnow.ToString("yyyyMMdd");
				di.TIMEDONE = dtnow.ToString("HHmmss");
				di.USER_ID = _parent.Uzivatel.ID.Value;
				di.guid = newGuid;
				di.ITEMDESC = zbozi.IsITEMDESCNull() ? string.Empty : zbozi.ITEMDESC;
				di.VNDITNUM = zbozi.VNDITNUM;
				di.CZ_CarKod = zbozi.CZ_CarKod;
				di.INPUT_MODE = _input_mode;
				di.ITEMCODE = zbozi.IsITEMCODENull() ? string.Empty : zbozi.ITEMCODE;   // 3.6.2016 PeV: Doplneno, neprobihalo nastaveni ITEMCODE
				di.ID_TERMINAL = Config.Settings.TerminalID;
				if (zbozi.IsWEIGHTNull())
					di.SetWEIGHTNull();
				else
					di.WEIGHT = zbozi.WEIGHT;

				di.SKL_ID_DEST = sklad_id_dest;

				di.PRINTED = false;
				di.SKL_ID = sklad_id;
				di.MJ = zbozi.MJ;
				di.QTYSHPPDMJ = qty;

				if (nmbrpal != null)
				{
					if (nmbrpal.Code != null)
						di.NMBRPAL = nmbrpal.Code.Trim();

					di.TYPEPAL = string.Empty;  // TODO: dodelat ...
												// neni implementovano ...
												//if (nmbrpal.Code != null)
												//    di.TYPEPAL = nmbrpal.Type.Trim();
				}

				if (expirace.HasValue)
					di.EXPIRACE = expirace.Value;
				else
					di.SetEXPIRACENull();


				#endregion

				#region OK Rozhodnuti Sklad/Expedice/Rozdelit

				RozhodovatSkladExpediceRozdelit(di);

                #endregion

                #region OK lokace

                // online ulozeni do lokacniho mechanismu, pokud je lokacni mechanismus povolen a jsou zapnute online pohyby
                if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0 &&
                    !_typdokladu.Iscfg_lok_mech_online_pohybyNull() && _typdokladu.cfg_lok_mech_online_pohyby > 0)
                {

                    LokaceService.LokacePohyb pohybrow = new LokaceService.LokacePohyb();
                    pohybrow.ITEMNMBR = zbozi.ITEMNMBR;
                    pohybrow.DOCUMENT_NUMBER = _typdokladu.doc_id;  // pokud je prijem, vydej dle predlohy, bude obsahovat hodnotu SOPNUMBE(PONUMBE) (hodnoty cisla dokladu IS), pokud prodej, tak doc_id
                    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                    LokaceService.TypeOfRecord recordType = (LokaceService.TypeOfRecord)Enum.Parse(typeof(LokaceService.TypeOfRecord), pohyb_type, true);
                    pohybrow.POHYB_TYPE = recordType;
                    pohybrow.POHYB_SRC = "R";   // zdroj pohybu, modul, ktery provedl pohyb (P - prijem, V - vydej, R - prodej)
                    pohybrow.SOURCE = "T";
                    pohybrow.QTYSHPPD = di.QTYSHPPD;
                    pohybrow.SERLTNUM = sn;
                    pohybrow.SKL_ID_SRC = sklad_id;

                    pohybrow.SKL_ID_DST = sklad_id_dest;

                    pohybrow.LOCNCODE_SRC = locncode;
                    pohybrow.LOCNCODE_DST = locncodedest;       // lokace, kam se prevadi ...
                    pohybrow.UserID = _parent.Uzivatel.ID.Value;
                    pohybrow.TermID = Config.Settings.TerminalID;
                    pohybrow.guid = newGuid;
                    pohybrow.Expiration = expirace;
                    pohybrow.ITEMDESC = zbozi.IsITEMDESCNull() ? string.Empty : zbozi.ITEMDESC;
                    pohybrow.CountEntries = _parent._Item.CisloDavky.Value;
                    pohybrow.dateeveT = dtnow;   // datum terminalu


                    try
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location ,"Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - start");
                        LokaceLog.writeBody(pohybrow, _parent.Uzivatel.ID.Value.ToString());

                        LokaceService.StatusLokace sl = DataInfo_Static.ProdejGO_Instance.servis_lokace.MoveItem(pohybrow);
                        switch (sl.State)
                        {
                            case LokaceService.States.OK:
                                break;
                            case LokaceService.States.ERROR:
                                MessageBox.Show(_parent, "Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", "error", MessageBoxButtons.OK);
                                return;
                            default:
                                MessageBox.Show(_parent, "Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", "info", MessageBoxButtons.OK);
                                return;
                        }

						Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - end");
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        if (MessageBox.Show(_parent, ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo) != DialogResult.Yes)
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
									Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + pohybrow.guid);

                                    LokaceService.StatusLokace sl = DataInfo_Static.ProdejGO_Instance.servis_lokace.DeleteRecordByGuid(pohybrow.guid, recordType);
                                    DialogResult dr = DialogResult.No;
                                    switch (sl.State)
                                    {
                                        case LokaceService.States.OK:
                                            dr = DialogResult.Yes;
                                            break;
                                        case LokaceService.States.ERROR:
                                            dr = MessageBox.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", "Error", MessageBoxButtons.YesNo);
                                            return;
                                        default:
                                            dr = MessageBox.Show(_parent, "Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", "Info", MessageBoxButtons.YesNo);
                                            return;
                                    }

									Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end");

                                    // pokud ho chce ulozit, odejde z cyklu
                                    if (dr == DialogResult.Yes)
                                        break;
                                }
                                //Nejaka online chyba
                                catch (Exception exex)
                                {
                                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Chyba při mazání lokací : " + exex.Message);
                                    if (MessageBox.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                di.DEX_ROW_ID = 0;

                #region OK Vlozeni jednoho radku do DI

                //_parent.RunOnUiThread(() => {
                //    _parent.mAdapter.AddItem(di);
                //});
                

                //_prodejTable.CZMST_DI.AddCZMST_DIRow(di);

                //try
                //{
                //	dataGridNasnimane.CurrentCell = new DataGridCell(_prodejTable.CZMST_DI.Count - 1, 0);
                //}
                //catch { }

                while (true)
                {
                    try
                    {
                        using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(Path.Combine(DataInfo_Static.PathDir, _parent._Item.CisloDavky.Value.ToString() + DataInfo_Static.PriponaDI)))
                        {
                            conPro.DI_DirectInsert(di);

                        }

                        //Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.DI_DirectInsert(di);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle("dita.insert," + ex.Message, "Prodej");
                        if (DialogResult.Yes != MessageBox.Show(_parent, ex.Message + "\nPřejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo))
                        {
                            #region lokace
                            // pokud ne, dojde online odmazani ...
                            if (_typdokladu != null && !_typdokladu.Iscfg_lok_mechNull() && _typdokladu.cfg_lok_mech > 0 &&
                                !_typdokladu.Iscfg_lok_mech_online_pohybyNull() && _typdokladu.cfg_lok_mech_online_pohyby > 0)
                            {
                                //Promenna ridici cyklus
                                bool state = true;

                                string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                                LokaceService.TypeOfRecord recordType = (LokaceService.TypeOfRecord)Enum.Parse(typeof(LokaceService.TypeOfRecord), pohyb_type, true);

                                //Dokud se odmazani nepovede, nebo si uzivatel nezada, ze chce ulozit pro offline zpracovani
                                while (state)
                                {
                                    try
                                    {
                                        //Nepreji se pokracovat - mohlo se ulozit - musim vyzkouset odmazat
                                        //Volani sluzby pro odstraneni a kontrola navratveho kodu.
                                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:del,Mode:online,Modul:R,TypeOfRecord:" + recordType.ToString() + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + newGuid.ToString());

                                        LokaceService.StatusLokace sl = DataInfo_Static.ProdejGO_Instance.servis_lokace.DeleteRecordByGuid(newGuid, recordType);
                                        DialogResult dr = DialogResult.No;
                                        switch (sl.State)
                                        {
                                            case LokaceService.States.OK:
                                                dr = DialogResult.Yes;
                                                break;
                                            case LokaceService.States.ERROR:
                                                dr = MessageBox.Show(_parent,"Nepodařilo se odstranit záznam v lokačním systému!\n'" + sl.ErrorMessage + "'\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo);
                                                break;
                                            default:
                                                dr = MessageBox.Show(_parent,"Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo);
                                                break;
                                        }

                                        // pokud ho chce ulozit, odejde z cyklu
                                        if (dr == DialogResult.Yes)
                                            break;
                                    }
                                    //Nejaka online chyba
                                    catch (Exception exex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Chyba při mazání lokací : " + exex.Message);
                                        if (MessageBox.Show(_parent,"Nepodařilo se odstranit záznam v lokačním systému!\n" + exex.Message + "\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                #region OK Zvuk po napipnuti

                if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni))
                {
                    if (File.Exists(Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni)))
                    {
                        MySystem.Audio.PlaySound(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni)); //ok, vlozeno pro informaci
                    }
                }

                #endregion

                #region OK Tisk po pridani zboží

                // Zjištění, zdali se má tisknout (podle cfg v DB)
                if (Konfigurace_Singleton.Instance.Prodej.PovolitPrintServer && (_typdokladu != null && _typdokladu.cfg_tisk > 0))
				{
					TiskEtikety(di);
				}

				#endregion

				_parent.UpdateForm();
			}
			catch (Exception ex)
			{
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
			}
			finally
			{
				_parent.ScannerStart();
				//this.Show();
			}
		}



        #region Pomocné metody

        private string GetLokaci(string sklad_id_dest, string v)
        {
            try
            {

                _parent._IsCurrentlyInConfirmProcess_Lokace = true;

                Action messageBoxDelegate = () => {
                    StartAktivityNasledujici(
                        typeof(Prodej_Lokace),
                        _parent.ResoultCode_Lokace,
                        Text: v
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);
                while (_parent._IsCurrentlyInConfirmProcess_Lokace)
                {
                    Thread.Sleep(1000);
                }

                return _parent.Lokace_LOCNCODE;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }


        private void ZadejVyberMaterialu(Location ds, out decimal qTY, out string sERLTNUM, out string lOCNCODE)
        {

            qTY = 0;
            sERLTNUM = string.Empty;
            lOCNCODE = string.Empty;

            try
            {

                _parent._IsCurrentlyInConfirmProcess_ZadejVyberMaterial = true;

                Action messageBoxDelegate = () => {
                    StartAktivityNasledujici(
                        typeof(Prodej_VyberMaterial), 
                        _parent.ResoultCode_VyberMaterial,
                        ds: ds
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);
                while (_parent._IsCurrentlyInConfirmProcess_ZadejVyberMaterial)
                {
                    Thread.Sleep(1000);
                }

                qTY = _parent.VyberMaterial_QTY;
                sERLTNUM = _parent.VyberMaterial_SERLTNUM;
                lOCNCODE = _parent.VyberMaterial_LOCNCODE;

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return;
            }
        }

        private Sklady.CZMST093Row ZadejCilovySklad()
        {
            try
            {
                _parent._IsCurrentlyInConfirmProcess_ZadejCilovySklad = true;
                //Action messageBoxDelegate = () => MessageBox.Show(((Activity)MainForm.MainActivity), callback, message, caption, buttons);

                Action messageBoxDelegate = () => {
                    StartAktivityNasledujici(typeof(Prodej_Sklady), _parent.ResoultCode_CilovySklad);
                };

                _parent.RunOnUiThread(messageBoxDelegate);
                while (_parent._IsCurrentlyInConfirmProcess_ZadejCilovySklad)
                {
                    Thread.Sleep(1000);
                }

                return _parent.CilovySklad;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        private void StartAktivityNasledujici(Type typAktivity, 
            int ResoultCode = 0, 
            bool finish = false, 
            string menaID = null,
            Location ds = null,
            string SKL_ID = null,
            string Text = null
            )
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(_parent, typAktivity);


            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new UsersWrapperForBinder(_parent.Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            //if (!string.IsNullOrEmpty(menaID))
            //{
            //    intent.PutExtra(DataInfo_Static.MenaID, menaID);
            //}

            //if (ResoultCode == ResultCode_SkladyZdroj)
            //{
            //    intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Zdroj);
            //}
            if (ResoultCode == _parent.ResoultCode_CilovySklad)
            {
                intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Cil);
                intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.SkladRow);
            }

            if (ds != null)
            {
                Bundle bundleLoc = new Bundle();
                bundleLoc.PutBinder(DataInfo_Static.object_Location, new LocationWrapperForBinder(ds));
                intent.PutExtra(DataInfo_Static.Location, bundleLoc);
            }

            if (!string.IsNullOrEmpty(SKL_ID))
            {
                intent.PutExtra(DataInfo_Static.Lok_SKL_ID, SKL_ID);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                intent.PutExtra(DataInfo_Static.Lok_Text, Text);
            }

            if (finish)
            {
                _parent.StartActivity(intent);
                _parent.Finish();
            }
            else
            {
                _parent.StartActivityForResult(intent, ResoultCode);
            }
        }

        /// <summary>
        /// Online ziskani ID zdrojoveho a ciloveho skladu.
        /// </summary>
        /// <param name="doc_id">typ dokladu.</param>
        /// <param name="itemnmbr">itemnmbr.</param>
        /// <param name="serltnum">serltnum.</param>
        /// <param name="skl_id">skl_id (out parameter)</param>
        /// <param name="skl_id_dest">skl_id_dest (out parameter)</param>
        /// <returns>STATUS (OK, ERROR)</returns>
        private ProdejService.STATUS OnlineGetSklad( string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
		{
			ProdejService.STATUS status = ProdejService.STATUS.ERROR;
			skl_id = string.Empty;
			skl_id_dest = string.Empty;

			try
			{

                ProdejServiceSession prodejService = new ProdejServiceSession();
				prodejService.Url = Config.Settings.Adresa + "Prodej.asmx";
				prodejService.Timeout = Config.Settings.TimeOut;
				prodejService.UpdateWebServiceCredentials();

				status = prodejService.GetSklad(Config.Settings.TerminalID, _parent.Uzivatel.ID.Value, doc_id, itemnmbr, serltnum, out skl_id, out skl_id_dest);

			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);

				return ProdejService.STATUS.ERROR;
			}

			return ProdejService.STATUS.OK;
		}

		/// <summary>
		/// Metoda pro zadavani lokace.
		/// </summary>
		/// <param name="zbozi">radek zbozi</param>
		/// <param name="sourceLoc">True - zdrojova lokace, False - cilova lokace</param>
		/// <returns>Nalezena lokace</returns>
		private string SejmiLocncode(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool sourceLoc)
		{
			return SejmiLocncode(zbozi, sourceLoc, null);
		}

		/// <summary>
		/// Metoda pro zadavani lokace.
		/// </summary>
		/// <param name="zbozi">radek zbozi</param>
		/// <param name="sourceLoc">True - zdrojova lokace, False - cilova lokace</param>
		/// <param name="prefilledVal">Predvyplnena hodnota v textboxu</param>
		/// <returns>Nalezena lokace</returns>
		private string SejmiLocncode(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi, bool sourceLoc, string prefilledVal)
		{
            #region 23.4.2018 TaD nova uprava ANC

            while (true)
            {
                ProdejPridatPolozku naplnpLocnCode = null;
                if (_typdokladu == null)
                {
                    naplnpLocnCode = new ProdejPridatPolozku(sourceLoc ? GetString(Resource.String.Prodej3ProdejListLokace) : GetString(Resource.String.Prodej3ProdejListCilovaLokace), Android.Text.InputTypes.ClassText, 0, true, false, "", null, null);
                    naplnpLocnCode.Odberatel = _parent._Item.Odberatel;
                    naplnpLocnCode.Zbozi = zbozi;
                    naplnpLocnCode.CodeType = Android.Text.InputTypes.ClassText;
                    naplnpLocnCode.Text = GetString(Resource.String.Prodej3ProdejListVlozteSklad);  // "Vložte sklad";
                                                                                                        //naplnpLocnCode.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["LOCNCODE"].MaxLength;
                    naplnpLocnCode.Kod = zbozi.LOCNCODE;
                    naplnpLocnCode.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;
                    if (naplnpLocnCode.ShowDialog(_parent) == DialogResult.Cancel)
                        return "!@";

                    //return naplnpLocnCode.Kod;
                }
                else
                {
                    // 8.4.2016 PeV: doc_typ 1 zakomentovan
                    //if (_typdokladu.doc_typ.Trim() == "1")
                    //{
                    //    if (_odberatel == null)
                    //        return string.Empty;
                    //    else
                    //        return _odberatel.odb_typ;
                    //}
                    //else
                    //{
                    //  locncode = zbozi.LOCNCODE;
                   naplnpLocnCode = new ProdejPridatPolozku(sourceLoc ? GetString(Resource.String.Prodej3ProdejListLokace) : GetString(Resource.String.Prodej3ProdejListCilovaLokace), Android.Text.InputTypes.ClassText, 0, true, false, "", null, null);
                    naplnpLocnCode.Odberatel = _parent._Item.Odberatel;
                    naplnpLocnCode.Zbozi = zbozi;
                    naplnpLocnCode.CodeType = Android.Text.InputTypes.ClassText;
                    //naplnpLocnCode.Text = "Vložte sklad";
                    naplnpLocnCode.Text = sourceLoc ? GetString(Resource.String.Prodej3ProdejListVlozteLokaci) : GetString(Resource.String.Prodej3ProdejListVlozteCilovouLokaci);
                    //naplnpLocnCode.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["LOCNCODE"].MaxLength;
                    //naplnpLocnCode.Kod = zbozi.LOCNCODE;
                    // zadani ciclove lokace
                    naplnpLocnCode.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;
                    if (!sourceLoc && _typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
                        naplnpLocnCode.Kod = string.Empty;
                    else
                        naplnpLocnCode.Kod = string.IsNullOrEmpty(prefilledVal) ? zbozi.LOCNCODE : prefilledVal;

                    if (naplnpLocnCode.ShowDialog(_parent   ) == DialogResult.Cancel)
                        return "!@";


                    //}
                }

                //bool config_Show_locncode = true;
                string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", naplnpLocnCode.Kod);

                ///konfiguracne zobrazovat nasnimanou lokaci
                if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                {
                    if (MessageBox.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return naplnpLocnCode.Kod;
                    }
                }
                else
                {
                    return naplnpLocnCode.Kod;
                }

            }

            #endregion

            //return "TODO Dodelat";
        }

		private bool Online_DISP(
			string ITEMNMBR, 
			decimal QTYSHPPD, 
			string sklad_id, 
			string LOCNCODE, 
			string SERLTNUM)
		{

			ProdejService.StatusResult info = null;
			bool opakovat;
			decimal mnozstvi;

			object MnozstviPredOBJ;

			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(Path.Combine(DataInfo_Static.PathDir, this.CisloDavky + DataInfo_Static.PriponaDI)))
            {
				MnozstviPredOBJ = conPro.SUM_QTYSHPPD_By_ITEMNMBR_from_DI(ITEMNMBR);
			}

			//object MnozstviPredOBJ = Prodej_3.ProdejMain.prodejInstance.globalObject.controller_prodej.SUM_QTYSHPPD_By_ITEMNMBR_from_DI(ITEMNMBR);
			decimal? MnozstviPredDEC = null;

			//BUG Tady je asi chyba pri objektu decimal...
			if ((MnozstviPredOBJ != null) && ((MnozstviPredOBJ is Int64) || (MnozstviPredOBJ is Double)))
			{
				MnozstviPredDEC = System.Convert.ToDecimal(MnozstviPredOBJ);
			}

			opakovat = true;


			if (MnozstviPredDEC.HasValue)
				mnozstvi = QTYSHPPD + (decimal)MnozstviPredDEC;
			else
				mnozstvi = QTYSHPPD;

			while (opakovat)
			{
				opakovat = false;
				try
				{

					ProdejService.Disponibilita disponibilita = new ProdejService.Disponibilita();

					disponibilita.DOC_ID = _typdokladu.doc_id;
					disponibilita.DOC_ID2 = _typdokladu.doc_id2;

					disponibilita.ITEMNMBR = ITEMNMBR;
					disponibilita.QTY = mnozstvi;

					disponibilita.SKL_ID = sklad_id;
					disponibilita.LOCNCODE = LOCNCODE;

					disponibilita.SERLTNUM = SERLTNUM;

					info = DataInfo_Static.ProdejGO_Instance.servis_prodej.Disponibilita(disponibilita);
				}
				catch (Exception ex)
				{
					// TODO : jak na dialog??
					//if (MessageBox.Show(ex.Message, "Error", MessageBoxButton.RetryCancel, MessageBoxBigIcon.Warning) ==
					//	DialogResult.Retry)
					//{
					//	opakovat = true;
					//}
					//else
					//{
					//	return false;
					//}
				}
			}

			// Je-li disponibilni, tak pokracovat, jinak stop
			if ((info == null) || (info.Status != ProdejService.StatusResultEnum.OK))
			{

				if (Konfigurace_Singleton.Instance.Prodej.DisponibilityZvuk)
				{
					//TODO : Android prehravani zvuku ???
					MySystem.Audio.PlaySound(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundDisponibility));
				}

				if (Konfigurace_Singleton.Instance.Prodej.DisponibilityHlaska)
				{
					//Hanibal nechce hlašku ale sou tady ty informace...
					MessageBox.Show(_parent ,"Položku nelze vydat!" + System.Environment.NewLine + info.Message, "Info", MessageBoxButtons.OK);
				}

				return false;
			}


			return true;
		}

		/// <summary>
		/// Metoda pro zmenu stredisek
		/// </summary>
		/// <returns></returns>
		private bool StrediskoSet()
		{

			//try
			//{
			//	_parent.ScannerStop();

			//	//Stredisko se zadava k davce a musi byt nastaveno na null
			//	if (!Prodej.Globals.StrediskoKPolozce && _stredisko != null)
			//	{
			//		//Pokud se zadava stredisko k polozce a stredisko neni null,
			//		//tak bylo vybrano a pouzije se toto
			//		return true;
			//	}

			//	if (Prodej.Globals.StrediskoText)
			//	{
			//		if (skf == null) skf = new SejmiKodForm();
			//		skf.AllowEmpty = false;
			//		skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
			//		skf.CheckLen = false;
			//		skf.Kod = string.Empty;
			//		skf.Len = 0;
			//		//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["STR_ID"].MaxLength;
			//		skf.Popis = Fask.Localization.Localization.Prodej3ProdejListStredisko;  //"Středisko";
			//		skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; //"Zadání doplňující hodnoty";
			//		if (skf.ShowDialog() == DialogResult.Cancel)
			//			return false;

			//		string strediskoid = skf.Kod;

			//		StrediskoTextSet(strediskoid);

			//	}
			//	else
			//	{
			//		if (pvsForm == null) pvsForm = new ProdejVyberStrediska2();

			//		if (pvsForm.ShowDialog() == DialogResult.Cancel)
			//			return false;

			//		_stredisko = pvsForm.Stredisko;
			//	}

			//	StrediskoMenuStateUpdate();

			//	return true;

			//}
			//catch (Exception ex)
			//{
			//	Fask.Logging.ExceptionHandler2.Handle(ex);
			//	MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButton.OK, MB_OK);
			//	return false;
			//}
			//finally
			//{
			//	_parent.ScannerStart();
			//}

			return true;
		}

		/// <summary>
		/// Metoda pro zmenu Pracovniku
		/// </summary>
		/// <returns></returns>
		private bool PracovniciSet()
		{
			//try
			//{

			//	ScannerStop();

			//	//Stredisko se zadava k davce a musi byt nastaveno na null
			//	if (!Prodej.Globals.PracovniciKPolozce && _pracovnik != null)
			//	{
			//		//Pokud se zadava stredisko k polozce a stredisko neni null,
			//		//tak bylo vybrano a pouzije se toto
			//		return true;
			//	}

			//	if (Prodej.Globals.PracovniciText)
			//	{
			//		if (skf == null) skf = new SejmiKodForm();
			//		skf.AllowEmpty = false;
			//		skf.CodeType = SejmiKodForm.TypeOfCode.AlphaNumeric;
			//		skf.CheckLen = false;
			//		skf.Kod = string.Empty;
			//		skf.Len = 0;
			//		//skf.MaxLength = (int)SqlCEDBs.Columns.Prodej.ColumnsInfo_CZMST_DI["PRAC_ID"].MaxLength;
			//		skf.Popis = Fask.Localization.Localization.Prodej3ProdejListPracovnik;  // "Pracovník";
			//		skf.Text = Fask.Localization.Localization.Prodej3ProdejListZadaniDoplnujiciHodnoty; // "Zadání doplňující hodnoty";
			//		if (skf.ShowDialog() == DialogResult.Cancel)
			//			return false;

			//		string pracovnikid = skf.Kod;

			//		PracovniciTextSet(pracovnikid);

			//	}
			//	else
			//	{
			//		if (pvpForm == null) pvpForm = new ProdejVyberPracovnika2();

			//		if (pvpForm.ShowDialog() == DialogResult.Cancel)
			//			return false;

			//		_pracovnik = pvpForm.Pracovnik;
			//	}

			//	PracovniciMenuStateUpdate();

			//	return true;

			//}
			//catch (Exception ex)
			//{
			//	Fask.Logging.ExceptionHandler2.Handle(ex);
			//	MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
			//	return false;
			//}
			//finally
			//{
			//	//if (scannerWasEnabled)
			//	ScannerStart();
			//}
			return true;
		}


		/// <summary>
		/// Metoda pro vypočet načteno
		/// </summary>
		/// <param name="ITEMNMBR">ID polozky</param>
		/// <returns>decimal počet načteno</returns>
		public decimal Nacteno(string ITEMNMBR)
		{
			decimal nacteno = 0;
			foreach (object diO in _parent.mAdapter.PolozkaSeznam.mItems.Rows)
			{

				Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow)diO;

				if (di.ITEMNMBR == ITEMNMBR)
				{
					nacteno += di.QTYSHPPD;
				}
			}
			return nacteno;
		}

				/// <summary>
		/// Metoda pro online ověření typu pohybu, zdali je možné pokračovat v ukládání dat.
		/// </summary>
		/// <param name="sn">sériové číslo</param>
		/// <param name="itemnmbr">FASK_ZASOBY.Row.ITEMNMBR</param>
		/// <param name="sklad_id">id skladu</param>
		/// <param name="qtyshppd">mnozstvi</param>
		/// <param name="qtyshppdNacteno">Doposud nactene mnozstvi</param>
		/// <returns></returns>
		private bool OverPohyb(
			string sn, 
			string itemnmbr, 
			string sklad_id, 
			decimal qtyshppd, 
			decimal qtyshppdNacteno, 
			out decimal outqtyshppd)
		{
			try
			{
				qtyshppd = qtyshppd + qtyshppdNacteno;
				ProdejServiceSession prodejService = new ProdejServiceSession();
				prodejService.Url = Config.Settings.Adresa + "Prodej.asmx";
				prodejService.Timeout = Config.Settings.TimeOut;
				prodejService.UpdateWebServiceCredentials();

				ProdejService.ProdejPohyb prodejPohyb = new ProdejService.ProdejPohyb();
				prodejPohyb.Doc_id = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
				prodejPohyb.Doc_id2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
				prodejPohyb.Itemnmbr = itemnmbr;
				prodejPohyb.Serltnum = sn;
				prodejPohyb.skl_id = sklad_id;
				prodejPohyb.qtyshppd = qtyshppd;

				ProdejService.StatusOverPohyb statusOP = prodejService.OverPohyb(prodejPohyb, Config.Settings.TerminalID, _parent.Uzivatel.ID.Value);

				if (!statusOP.PohybOK)
					throw new Exception(statusOP.Message);
				outqtyshppd = statusOP.a_dispozice;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				//Fask.Logging.ExceptionHandler2.Handle(ex.Message, "Prodej_3.ProdejList, OverPohyb");
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
				outqtyshppd = -1;
				return false;
			}
			return true;
		}

		/// <summary>
		/// Metoda pro tisk Etikety
		/// </summary>
		/// <param name="di"></param>
		/// <returns></returns>
		private bool TiskEtikety(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
		{

            bool? TiskSCenou = null;
            bool vytisteno = false;

            if (Konfigurace_Singleton.Instance.Prodej.DialogTisk)
            {
                if (DialogResult.No == MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListTiskEtiketyDotaz), "Otazka", MessageBoxButtons.YesNo))
                    return false;
            }


            if (Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou)
            {
                if (Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou_ZobrazDialog)
                {
                    DialogResult dres = MessageBox.Show(_parent, GetString(Resource.String.Prodej3ProdejListTiskEtiketaSCenou), "Otazka", MessageBoxButtons.YesNo);

                    if (dres == DialogResult.Yes)
                        TiskSCenou = true;
                    else { TiskSCenou = false; }
                }
                else
                {
                    TiskSCenou = !Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou_Cena;
                }
            }

            Thread confirmThread = null;


            if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
            {
                confirmThread = new Thread(() => {
                    vytisteno = ProdejTisk.Print(_parent, di, Fask.PrinterFactory.PrinterModules.ProdejNasnimane, 1, TiskSCenou);
                });
                
            }
            else
            {
                if (Konfigurace_Singleton.Instance.Prodej.EtiketaTisk_PrebiratMnozstvi)
                {
                    confirmThread = new Thread(() => {
                        int TiskQTY = System.Convert.ToInt32(di.QTYSHPPD);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Tisk Prodej , Prebirane Množstvi :" + TiskQTY.ToString());
                    vytisteno = ProdejTisk.Print(_parent, di, Fask.PrinterFactory.PrinterModules.ProdejNasnimane, TiskQTY, TiskSCenou);
                    });
                }
                else
                {
                    confirmThread = new Thread(() =>
                    {
                        vytisteno = ProdejTisk.Print(_parent, di, Fask.PrinterFactory.PrinterModules.ProdejNasnimane, string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety) ? (int?)null : System.Convert.ToInt32(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety), TiskSCenou);
                    });
                }
            }

            confirmThread.Start();
            confirmThread.Join();

            //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "1", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", di.CountEntries, null, null, vytisteno.ToString(), null));

            return vytisteno;
            //return true;
		}

		/// <summary>
		/// Metoda pro online overeni GetMaterial (??lok mech??)
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="serltnum">SN / sarze</param>
		/// <returns>Dataset Location</returns>
		private ProdejService.Location OnlineGetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			ProdejService.Location ds = new ProdejService.Location();

			try
			{
				ProdejServiceSession prodejService = new ProdejServiceSession();
				prodejService.Url = Config.Settings.Adresa + "Prodej.asmx";
				prodejService.Timeout = Config.Settings.TimeOut;
				prodejService.UpdateWebServiceCredentials();

				ds = prodejService.Online_GetMaterial(itemnmbr, skl_id, serltnum, _typdokladu != null ? _typdokladu.doc_id : string.Empty, false);
			}
			catch (Exception ex)
			{
				//Fask.Logging.ExceptionHandler2.Handle(ex.Message, "Prodej.ProdejList, OnlineGetPalety");
				Fask.Logging.ExceptionHandler2.Handle(ex);
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);

				return null;
			}
			return ds;
		}

		/// <summary>
		/// Metoda pro online overeni lokace
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="serltnum">SN/ sarže</param>
		/// <param name="locncode">Lokace</param>
		/// <param name="skl_id">ID Skladu</param>
		/// <param name="locationType">'S' - overovani zdrojove lokace, 'D' - overovani cilove lokace</param>
		/// <returns>naplnen StatusOverLokace</returns>
		private ProdejService.StatusOverLokace OnlineOverLokace(string itemnmbr, string serltnum, DateTime? expirace, string locncode, string skl_id, decimal qtyshppd, ProdejService.TYPLokace locationType, ProdejService.TypeOfRecord recordType)
		{
			ProdejService.StatusOverLokace so;
			try
			{
				ProdejServiceSession prodejService = new ProdejServiceSession();
				prodejService.Url = Config.Settings.Adresa + "Prodej.asmx";
				prodejService.Timeout = Config.Settings.TimeOut;
				prodejService.UpdateWebServiceCredentials();

				so = prodejService.Online_OverLokace(itemnmbr, serltnum, expirace, locncode, skl_id, qtyshppd, _typdokladu != null ? _typdokladu.doc_id : string.Empty, locationType, recordType);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				//Fask.Logging.ExceptionHandler2.Handle(ex.Message, "Prodej.ProdejList, OnlineOverLokace");
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
				return null;
			}
			return so;
		}


		[Obsolete("Na prodeji se musi pouzivat Prodejni webservice => PrijemService.Online_GetDoporuceneLokace predelat do ProdejService...", false)]
		/// <summary>
		/// Metoda pro online zisteni cilove lokace
		/// </summary>
		/// <param name="itemnmbr">ID polozky</param>
		/// <param name="serltnum">SN/ sarze</param>
		/// <param name="skl_id">ID skladu</param>
		/// <returns>naplnen dataset obecne</returns>
		private PrijemService.Obecne OnlineGetDoporuceneCiloveLokace(string itemnmbr, string serltnum, string skl_id)
		{
			// TODO: do budoucna resit pres prodej.asmx, posilat typ dokladu, ...
			PrijemService.Obecne ds = new PrijemService.Obecne();

			try
			{
				ds = DataInfo_Static.ProdejGO_Instance.servis_Prijem.Online_GetDoporuceneLokace(itemnmbr, skl_id, serltnum);
			}
			catch (Exception ex)
			{

				Fask.Logging.ExceptionHandler2.Handle(ex);
				//Logging.Log.Write(ex.Message, "Prodej.ProdejList, OnlineGetDoporuceneLokace");
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);

				return null;
			}
			return ds;
		}
		#endregion

		#region Hanibal, Sklad Expedice Rozdelit

		/// <summary>
		/// Metoda slouživi pro rozhodnuti zda položku dat na Sklad/Expedici alebo rozdelit
		/// </summary>
		private void RozhodovatSkladExpediceRozdelit(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
		{
			decimal Nasnimano = Nacteno(di.ITEMNMBR);
			//if (true)
			if (_typdokladu != null && !_typdokladu.Iscfg_NavrhNull() && _typdokladu.cfg_Navrh > 0)
			{

				decimal PrijimaneMnozstvi = di.QTYSHPPDMJ;

				decimal MnozstviDodavatelePozadovano = 0; ;
				decimal MnozstviDodavateleDodano = 0;
				decimal MnozstviDodavateleDodat = 0;
				decimal MnozstviOdberateliPozadovano = 0;
				decimal MnozstviOdberatelumDodano = 0;
				decimal MnozstviOdberatelumDodat = 0;
				decimal Vysledek = 0;

				//OnlineGetSkladExpedice(PERow.ITEMNMBR, ppp.Kod, "0", PERow.SERLTNUM);
				OnlineGetSkladExpedice(
									di.ITEMNMBR,
									PrijimaneMnozstvi,
									Nasnimano,
									out MnozstviDodavatelePozadovano,
									out MnozstviDodavateleDodano,
									out MnozstviDodavateleDodat,
									out MnozstviOdberateliPozadovano,
									out MnozstviOdberatelumDodano,
									out MnozstviOdberatelumDodat,
									out Vysledek
									);

				decimal NaSklad = 0;
				decimal NaExpedici = 0;


				if (Vysledek >= PrijimaneMnozstvi)
				{ // ma se jeste dodat "Vysledek" a prijimam mene nez dodavam ...
					NaExpedici = PrijimaneMnozstvi;
					NaSklad = 0;
					MySystem.Audio.PlaySound(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundExpedice));
				}
				else if ((0 < Vysledek) && (Vysledek < PrijimaneMnozstvi))
				{
					NaExpedici = Vysledek;
					NaSklad = PrijimaneMnozstvi - Vysledek;
					MySystem.Audio.PlaySound(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSkladExpedice));
				}
				else if (Vysledek <= 0)
				{
					NaExpedici = 0;
					NaSklad = PrijimaneMnozstvi;
					MySystem.Audio.PlaySound(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSklad));
				}


				if (Konfigurace_Singleton.Instance.Prodej.ZobrazovatReport && !Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
				{

					//TaD sledovani na mnozstvi 22.05.2018 Hanibal 
					using (FormReport frmrep = new FormReport())
					{
						//frmrep.PERow = PERow;
						//frmrep.ds = ds;


						frmrep.CZ_CarKod = di.CZ_CarKod;
						frmrep.ITEMDESC = di.ITEMDESC;
						frmrep.ITEMNMBR = di.ITEMNMBR;


						frmrep.NaExpedici = NaExpedici;
						frmrep.NaSklad = NaSklad;

						frmrep.MnozstviDodavatelePozadovano = MnozstviDodavatelePozadovano;
						frmrep.MnozstviDodavateleDodano = MnozstviDodavateleDodano;
						frmrep.MnozstviDodavateleDodat = MnozstviDodavateleDodat;
						frmrep.MnozstviOdberateliPozadovano = MnozstviOdberateliPozadovano;
						frmrep.MnozstviOdberatelumDodano = MnozstviOdberatelumDodano;
						frmrep.MnozstviOdberatelumDodat = MnozstviOdberatelumDodat;


						if (frmrep.ShowDialog(_parent) == DialogResult.Cancel)
							return;

					}
				}

			}
		}

		/// <summary>
		/// Metoda pro poskladani objektu a online zavolani a nasledne rozparsovani objektu... 
		/// </summary>
		/// <param name="ITEMNMBR">ID položky</param>
		/// <param name="MnozstviZadane">Mnozstvi Zadane</param>
		/// <param name="MnozstviNasnimane">Mnozstvi Nasnimane</param>
		/// <param name="MnozstviDodavatelePozadovano"> Mnozstvi Dodavatele Pozadovano</param>
		/// <param name="MnozstviDodavateleDodano">Mnozstvi Dodavatele Dodano</param>
		/// <param name="MnozstviDodavateleDodat">Mnozstvi Dodavatele Dodat</param>
		/// <param name="MnozstviOdberateliPozadovano">Mnozstvi Odberateli Pozadovano</param>
		/// <param name="MnozstviOdberatelumDodano">Mnozstvi Odberatelum Dodano</param>
		/// <param name="MnozstviOdberatelumDodat">Mnozstvi Odberatelum Dodat</param>
		/// <param name="Vysledek">Vysledek</param>
		/// <returns>true- OK, False- chyba</returns>
		private void OnlineGetSkladExpedice(
						string ITEMNMBR,
						decimal MnozstviZadane,
						decimal MnozstviNasnimane,
						out decimal MnozstviDodavatelePozadovano,
						out decimal MnozstviDodavateleDodano,
						out decimal MnozstviDodavateleDodat,
						out decimal MnozstviOdberateliPozadovano,
						out decimal MnozstviOdberatelumDodano,
						out decimal MnozstviOdberatelumDodat,
						out decimal Vysledek
						)
		{
			//DataSet ds = new DataSet();// PrijemService.Obecne();

			MnozstviDodavatelePozadovano =
			MnozstviDodavateleDodano =
			MnozstviDodavateleDodat =
			MnozstviOdberateliPozadovano =
			MnozstviOdberatelumDodano =
			MnozstviOdberatelumDodat =
			Vysledek = 0;

			try
			{
		
				ProdejService.VstupniObjekt ObjektIN = new ProdejService.VstupniObjekt();

				ObjektIN.DOC_ID = _typdokladu.doc_id;
				ObjektIN.DOC_ID2 = _typdokladu.doc_id2;
				ObjektIN.SKL_ID = _typdokladu.SKL_ID;

				ObjektIN.ITEMNMBR = ITEMNMBR.Trim();
				ObjektIN.MnozstviNasnimane = MnozstviNasnimane;
				ObjektIN.MnozstviZadane = MnozstviZadane;

				ProdejService.VystupniObjekt ObjektOUT = DataInfo_Static.ProdejGO_Instance.servis_prodej.Online_UniverzalnyDotazNaCokoliv(ObjektIN);

				MnozstviDodavatelePozadovano = ObjektOUT.MnozstviDodavatelePozadovano;
				MnozstviDodavateleDodano = ObjektOUT.MnozstviDodavateleDodano;
				MnozstviDodavateleDodat = ObjektOUT.MnozstviDodavateleDodat;
				MnozstviOdberateliPozadovano = ObjektOUT.MnozstviOdberateliPozadovano;
				MnozstviOdberatelumDodano = ObjektOUT.MnozstviOdberatelumDodano;
				MnozstviOdberatelumDodat = ObjektOUT.MnozstviOdberatelumDodat;
				Vysledek = ObjektOUT.Vysledek;


			}

			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				//Logging.Log.Write(ex.Message, "Prijem.PrijemList, OnlineGetSkladExpedice");
				MessageBox.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);

				//return false;
			}
			//return true;
		}


		#endregion

	}
}