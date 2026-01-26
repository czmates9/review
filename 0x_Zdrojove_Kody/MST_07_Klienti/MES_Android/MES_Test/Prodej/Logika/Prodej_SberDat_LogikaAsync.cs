using Android.Content;
using Android.OS;
using Fask.Parsing.Codes;
using Fask.SQLiteDBs.DataSets;
using MES_Android._WebReferences_Globals;
using MES_Android.Classes;
using MES_Android.Prodej.Classes;
using MES_Android.ProdejService;
using MES_Android.ServerAccess;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MES_Android.Prodej.Logika
{
    public class Prodej_SberDat_LogikaAsync
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

        #region Konstruktor

        public Prodej_SberDat_LogikaAsync(Prodej_SberDat Parent, int CisloDavky, Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row Typdokladu)
        {
            this._parent = Parent;
            this._cisloDavky = CisloDavky;
            this._typdokladu = Typdokladu;
        }

        #endregion

        #region CORE cele logiky prodeje + Asynchronna metoda pro volani

        public async void pridatPolozku_NewAsync(
        Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
        BaseCode code = null,
        byte _input_mode = 0,
        string serltnum = "",
        Paleta nmbrpal = null,
        Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096Row _pracovnik = null
        )
        {
            _parent.Scanner_STOP();

            await pridatPolozku_New(
                zbozi,
                code,
                _input_mode,
                serltnum,
                nmbrpal,
                _pracovnik
                );

            _parent.Scanner_START();
        }



        public async Task pridatPolozku_New(
                Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
                BaseCode code = null,
                byte _input_mode = 0,
                string serltnum = "",
                Paleta nmbrpal = null,
                Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096Row _pracovnik = null
                )
        {

            #region Kontrola, zda je mozne vybirat i necim jinacim nez scannerem

            if (!InputModeChecker.checkInputMode(Konfigurace_Singleton.Instance.Prodej.PolozkyVyberJenScannerem, _input_mode))
            {
                await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkuJdeZadatPouzeScannerem), "Info", MessageBoxButtons.OK);
                return;
            }

            #endregion

            #region Kontrola existence objektu zbozi

            if (zbozi == null)
            {
                await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniVybranoZbozi), GetString(Resource.String.Prodej3ProdejListPridatPolozku), MessageBoxButtons.OK);
                return;
            }

            #endregion

            #region kontrola ExistenceNasnimanePolozky

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
                if (nalez)
                {
                    //TODO lokalizovat
                    string message = "Položka již byla nasnímana." + System.Environment.NewLine + "Pokračovat?";
                    var dr = await MessageBoxAsync.Show(_parent, message, "Dotaz", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            #endregion

            #region Parametry

            string rez1 = string.Empty;
            string rez2 = string.Empty;
            string rez3 = string.Empty;
            string rez4 = string.Empty;
            string sklad_id = string.Empty;
            string sklad_id_dest = string.Empty;

            #endregion

            try
            {
                //_parent.ScannerStop();

                #region Parametry lokalne

                decimal qty = 0;
                string sn = string.Empty;
                DateTime? expirace = null;
                string odb_id = string.Empty;

                #endregion

                #region Sklad / Zdrojovy sklad

                sklad_id = Get_ZdrojSklad(zbozi);

                #endregion

                #region Cilovy sklad

                // ma se prevzit id skladu
                sklad_id_dest = Get_CilSklad(sklad_id);

                #endregion

                #region sarze z parsovaneho / ze zbozi

                sn = Get_Sarze_Parse_anebo_Zbozi(zbozi, code);

                #endregion

                #region Lokace ze zbozi

                string locncode = zbozi.LOCNCODE; //28.6.2013 JiS: oprava melo by byt z zbozi a ne _zbozi

                #endregion

                #region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)

                if (Konfigurace_Singleton.Instance.Prodej.NacistSkladIDOnline)
                {
                    var state_ogs = await OnlineGetSkladAsync(
                        _typdokladu != null ? _typdokladu.doc_id : string.Empty,
                        zbozi.ITEMNMBR,
                        zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM);

                    if (state_ogs.Status == ProdejService.STATUS.ERROR) // chyba, ukoncit ...
                    {
                        return;
                    }
                    else if (string.IsNullOrEmpty(state_ogs.SKL_ID) && string.IsNullOrEmpty(state_ogs.SKL_ID_DEST))
                    {
                        await MessageBoxAsync.Show(_parent, string.Format(GetString(Resource.String.Prodej3ProdejNepodariloSeNajitIDSkladuOnlineProMaterial), zbozi.ITEMNMBR.Trim()), "Error", MessageBoxButtons.OK);
                        return;
                    }

                    sklad_id = state_ogs.SKL_ID;
                    sklad_id_dest = state_ogs.SKL_ID_DEST;
                }

                #endregion

                #region Doporucene palety

                if (_typdokladu != null && !_typdokladu.Iscfg_onl_dop_palNull() && _typdokladu.cfg_onl_dop_pal > 0)
                {
                    ProdejService.Location ds = await OnlineGetMaterialAsync(zbozi.ITEMNMBR, sklad_id, sn);
                    if (ds != null)
                    {
                        // vratily se nejake zaznamy
                        if (ds.CZMST_SkladLokace_Stav.Count > 1)
                        {

                            var retO = await ZadejVyberMaterialuAsync(ds);

                            if (retO != null && retO.status)
                            {

                                zbozi.QTY = qty = retO.QTY.HasValue ? retO.QTY.Value : 0;
                                zbozi.SERLTNUM = sn = retO.SERLTNUM.Trim();
                                zbozi.LOCNCODE = locncode = retO.LOCNCODE.Trim();

                            }
                            else
                                return;

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
                            DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListDoporucenePaletyNenalezenyPokracovatDotaz), "Otazka", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.No)
                                return;
                        }
                    }
                    else
                    {
                        // data se nepodarila nacist ze serveru, dotaz zdali pokracovat
                        DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemPokracovatDotaz), "Otazka", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region SN na davku

                // prednastaveni SN, pokud je povoleno
                if (_typdokladu != null && !_typdokladu.Iscfg_sn_na_davkuNull() && _typdokladu.cfg_sn_na_davku > 0)
                    zbozi.SERLTNUM = sn = serltnum;

                #endregion

                #region Lokace z typu dokladu

                // pokud je vyplneno locncode, automaticky se pouzije ... (nehledne na povoleni zadani zdrojove lokace
                if (_typdokladu != null && !string.IsNullOrEmpty(_typdokladu.LOCNCODE.Trim()))
                    locncode = _typdokladu.LOCNCODE.Trim();

                #endregion

                #region Lokace pred SN

                if (Konfigurace_Singleton.Instance.Prodej.ZadaniLocncodePredSN && (_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0))
                {
                    if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
                    {
                        var stat_lok_O = await GetLokaciAsync(sklad_id, Resource.String.Lokace);

                        if (stat_lok_O != null && stat_lok_O.status)
                        {
                            if (stat_lok_O.LOCNCODE == null)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                                return;
                            }
                            else
                            {
                                locncode = stat_lok_O.LOCNCODE;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        ////TaD : Dodelat nutně
                        //locncode = SejmiLocncode(zbozi, true);
                        //if (locncode == "!@")
                        //    return;

                        #region Lokace Zadani ručně hodnotu

                        string locncodeTMP = string.Empty;

                        while (true)
                        {

                            var _pppLOC = SejmiLOCNCODE_prepare(zbozi, true, locncodeTMP);
                            var ppp_data = await Get_PPP_Async(_pppLOC);

                            if (!ppp_data.status)
                                return;

                            if (ppp_data.LOCNCODE != null)
                                return;

                            locncodeTMP = ppp_data.LOCNCODE;

                            string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", locncodeTMP);

                            ///konfiguracne zobrazovat nasnimanou lokaci
                            if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                            {
                                if (await MessageBoxAsync.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                    continue;
                                else
                                    break;
                            }
                            else
                                break;

                        }

                        locncode = locncodeTMP;

                        #endregion
                    }
                }

                #endregion

                #region Sledovani na Množství

                if (zbozi.CZ_SerNum_Track == 0) //sledovano na mnozstvi
                {
                    decimal? qtytmp = Get_QTY_FromWeightCode(!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu, zbozi, code);

                    if (qtytmp.HasValue)
                    {
                        qty = qtytmp.Value;
                    }
                    else if ((_typdokladu != null && !_typdokladu.Iscfg_mnozstvi_ze_zboziNull() && _typdokladu.cfg_mnozstvi_ze_zbozi > 0) && zbozi.QTY > 0)     // prevzit mnozstvi 
                    {
                        qty = zbozi.QTY;
                    }
                    else
                    {
                        if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
                        {
                            qty = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
                        }
                        else if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                        {
                            qty = 1;
                        }
                        else
                        {

                            Prodej.Classes.Prodej_PPP_Objekt _PPP_Objekt = PPP_Mnozstvi(zbozi, sn);

                            var dectmp = Get_QTY(zbozi, code);

                            _PPP_Objekt.Kod = dectmp.HasValue ? dectmp.Value.ToString(Config.Settings.UIFormatDesCisel) : string.Empty;

                            var ppp_data = await Get_PPP_Async(_PPP_Objekt);

                            if (!ppp_data.status)
                                return;

                            if (!ppp_data.QTY.HasValue)
                                return;

                            qty = ppp_data.QTY.Value;
                        }

                        if (Konfigurace_Singleton.Instance.Prodej.KontrolaStavuSkladu)     // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
                        {
                            if (qty > zbozi.QTY)
                            {
                                var dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz), GetString(Resource.String.Prodej3ProdejListInfo), MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }

                        //qty = ppp_data.QTY.Value;
                    }
                }

                #endregion

                #region Sledovani na Sarze/SN + expirace

                else if ((zbozi.CZ_SerNum_Track == 1) || (zbozi.CZ_SerNum_Track == 2)) //sledovano na seriova cisla
                {
                    decimal? qtytmp = Get_QTY_FromWeightCode(!Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu, zbozi, code);

                    if (qtytmp.HasValue)
                    {
                        qty = qtytmp.Value;
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

                            sn = vnditnum.Substring(0, 50);
                        }
                        catch { }
                    }
                    else
                    {
                        if ((zbozi.CZ_SerNum_Track == 2) && (code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr) && !String.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
                            sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
                        else
                        {

                            Prodej.Classes.Prodej_PPP_Objekt _PPP_Objekt = new Classes.Prodej_PPP_Objekt();
                            _PPP_Objekt.Odberatel = _parent._Item.Odberatel;
                            _PPP_Objekt.Serltnum = sn;
                            _PPP_Objekt.Volajici = Volajici_ProdejPridatPolozku.SERLTNUM;
                            _PPP_Objekt.Zbozi = zbozi;

                            _PPP_Objekt.Popis = zbozi.CZ_SerNum_Track == 1 ? GetString(Resource.String.Prodej3ProdejListSerioveCislo) : GetString(Resource.String.Prodej3ProdejListSarze);
                            _PPP_Objekt.Text = zbozi.CZ_SerNum_Track == 1 ? GetString(Resource.String.Prodej3ProdejListVlozteSerioveCislo) : GetString(Resource.String.Prodej3ProdejListVlozteSarze);
                            _PPP_Objekt.CodeType = Android.Text.InputTypes.ClassText;
                            _PPP_Objekt.Len = (decimal)(zbozi.CZ_SerNum_Delka == 0 ? (_typdokladu.Iscfg_delka_SNNull() ? 0 : _typdokladu.cfg_delka_SN) : zbozi.CZ_SerNum_Delka);
                            _PPP_Objekt.CheckLen = true;
                            _PPP_Objekt.AllowEmpty = false;
                            //_PPP_Objekt.ScannerEnable = Konfigurace_Singleton.Instance.Prodej.PovolitZadaniMnozstviScannerem;

                            _PPP_Objekt.Kod = sn;

                            var ppp_data = await Get_PPP_Async(_PPP_Objekt);

                            if (!ppp_data.status)
                                return;

                            if (string.IsNullOrEmpty(ppp_data.SERLTNUM))
                                return;

                            sn = ppp_data.SERLTNUM;
                        }

                        qty = 1;


                    }

                    #region Test na kontrolu existence SN ve vystupu


                    int pocetSN = 0;
                    try
                    {
                        // 20160810 JiS - opraveno : test if (zbozi.czsernumtrac == 1 !!! <= SN ...
                        if (zbozi.CZ_SerNum_Track == 1)
                        {
                            pocetSN = System.Convert.ToInt32(_parent.mAdapter.PolozkaSeznam.mItems.Compute("Count(ITEMNMBR)", "ITEMNMBR='" + zbozi.ITEMNMBR + "' AND SERLTNUM='" + sn + "'"));
                            if (pocetSN > 0)
                            {
                                await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListSerioveCisloJizByloNasnimano), "Error", MessageBoxButtons.OK);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
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

                            if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                            {
                                qty = 1;
                            }
                            else if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeQuantity) && ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.HasValue)
                            {
                                qty = ((Fask.Parsing.Codes.Interfaces.ICodeQuantity)code).Quantity.Value;
                            }
                            else
                            {
                                Prodej.Classes.Prodej_PPP_Objekt _PPP_Objekt = PPP_Mnozstvi(zbozi, sn);

                                var dectmp = Get_QTY(zbozi, code);

                                _PPP_Objekt.Kod = dectmp.HasValue ? dectmp.Value.ToString(Config.Settings.UIFormatDesCisel) : string.Empty;

                                var ppp_data = await Get_PPP_Async(_PPP_Objekt);

                                if (!ppp_data.status)
                                    return;

                                if (!ppp_data.QTY.HasValue)
                                    return;

                                qty = ppp_data.QTY.Value;
                            }

                            if (Konfigurace_Singleton.Instance.Prodej.KontrolaStavuSkladu) // TODO: asi by to chtelo pres typ dokladu (kdyby se napr. provadel prijem pres lokacni mechanismus)
                            {
                                if (qty > zbozi.QTY)
                                {
                                    if (await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListPolozkaPreplnenaPokracovatDotaz), GetString(Resource.String.Prodej3ProdejListInfo), MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }

                            //qty = decimal.Parse(naplnpMnozstvi.Kod);
                        }

                    }


                    //TaD : TODO, porovnat s SAB aktualnym a predelat zadavaci okno Expirace
                    #region sledovani Expirace

                    if (zbozi.CZ_Expirace_Track > 0)
                    {

                        if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeExpiration) && ((Fask.Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration.HasValue)
                            expirace = ((Fask.Parsing.Codes.Interfaces.ICodeExpiration)code).Expiration;


                        DateTime expiraceLast = DateTime.Now.AddDays(30);


                        if (!expirace.HasValue)
                        {
                            expirace = expiraceLast;
                            string expirationStr = expiraceLast.ToString(DataInfo_Static.dateFormatRRMMDD);

                            while (true)
                            {
                                //var dResExpiration = InputBoxExpirace.Show(_parent, "Expirace (RRMMDD)", expirationStr, out expirationStr, true, Android.Text.InputTypes.ClassDatetime);
                                var dResExpiration = await InputBoxAsync.Show(
                                    _parent, 
                                     Title: "Expirace (RRMMDD)",
                                    Message: string.Empty,
                                    Defaultvalue: expirationStr,
                                    buttons: MessageBoxButtons.OKCancel,
                                    keyboardMode: Android.Text.InputTypes.ClassDatetime
                                    );

                                if (dResExpiration.Dialog_Result == DialogResult.Cancel)
                                    return;

                                // validace
                                try
                                {
                                    expirace = DataInfo_Static.Date_RRMMDD(dResExpiration.Value);
                                }
                                catch (Exception ex)
                                {
                                    await MessageBoxAsync.Show(_parent, String.Format("Nesprávný formát :\n {0} => {1}", "RRMMDD", expirationStr), "Expirace", MessageBoxButtons.OK);
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

                #region Lokace po SN

                if (!Konfigurace_Singleton.Instance.Prodej.ZadaniLocncodePredSN && (_typdokladu != null && !_typdokladu.Iscfg_lokaceNull() && _typdokladu.cfg_lokace > 0))
                {
                    if (string.IsNullOrEmpty(locncode.Trim()) && !_typdokladu.Iscfg_lokace_ciselnikNull() && _typdokladu.cfg_lokace_ciselnik > 0)
                    {
                        var stat_lok_O = await GetLokaciAsync(sklad_id, Resource.String.Lokace);

                        if (stat_lok_O != null && stat_lok_O.status)
                        {
                            if (stat_lok_O.LOCNCODE == null)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!");
                                return;
                            }
                            else
                            {
                                locncode = stat_lok_O.LOCNCODE;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        //TaD : Dodelat nutně
                        //locncode = SejmiLocncode(zbozi, true);
                        //if (locncode == "!@")
                        //    return;

                        #region Lokace Zadani ručně hodnotu

                        string locncodeTMP = string.Empty;

                        while (true)
                        {

                            var _pppLOC = SejmiLOCNCODE_prepare(zbozi, true, locncodeTMP);
                            var ppp_data = await Get_PPP_Async(_pppLOC);

                            if (!ppp_data.status)
                                return;

                            if (ppp_data.LOCNCODE != null)
                                return;

                            locncodeTMP = ppp_data.LOCNCODE;

                            string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", locncodeTMP);

                            ///konfiguracne zobrazovat nasnimanou lokaci
                            if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                            {
                                if (await MessageBoxAsync.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                    continue;
                                else
                                    break;
                            }
                            else
                                break;

                        }

                        locncode = locncodeTMP;

                        #endregion
                    }
                }

                #endregion

                #region Vytvoreny jeden vystupny radek

                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di = _parent.mAdapter.PolozkaSeznam.mItems.NewCZMST_DIRow();

                #endregion

                #region prepočet QTY a QTYPACK

                di.QTYSHPPD = qty * (zbozi.QTYPACK > 0 ? zbozi.QTYPACK : 1);

                #endregion

                #region Kontrola disponibility Zdrojový - online

                if (_typdokladu != null && _typdokladu.cfg_disp > 0)
                {
                    var statusDispoZdroj = await Online_DISP(zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id, string.Empty, string.Empty);
                    // Je-li disponibilni, tak pokracovat, jinak stop
                    if ((statusDispoZdroj == null) || (statusDispoZdroj.Status != ProdejService.StatusResultEnum.OK))
                    {

                        if (Konfigurace_Singleton.Instance.Prodej.DisponibilityZvuk)
                        {
                            //TODO : Android prehravani zvuku ???
                            await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundDisponibility));
                        }

                        if (Konfigurace_Singleton.Instance.Prodej.DisponibilityHlaska)
                        {
                            //Hanibal nechce hlašku ale sou tady ty informace...
                            await MessageBoxAsync.Show(_parent, "Položku nelze vydat!" + System.Environment.NewLine + statusDispoZdroj.Message, "Info", MessageBoxButtons.OK);
                        }

                        return;
                    }
                }

                #endregion

                #region Kontrola disponibility Cilový - online

                if (_typdokladu != null && _typdokladu.cfg_disp_dest > 0)
                {
                    var statusDispoCil = await Online_DISP(zbozi.ITEMNMBR, di.QTYSHPPD, sklad_id_dest, string.Empty, string.Empty);

                    // Je-li disponibilni, tak pokracovat, jinak stop
                    if ((statusDispoCil == null) || (statusDispoCil.Status != ProdejService.StatusResultEnum.OK))
                    {

                        if (Konfigurace_Singleton.Instance.Prodej.DisponibilityZvuk)
                        {
                            //TODO : Android prehravani zvuku ???
                            await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundDisponibility));
                        }

                        if (Konfigurace_Singleton.Instance.Prodej.DisponibilityHlaska)
                        {
                            //Hanibal nechce hlašku ale sou tady ty informace...
                            await MessageBoxAsync.Show(_parent, "Položku nelze vydat!" + System.Environment.NewLine + statusDispoCil.Message, "Info", MessageBoxButtons.OK);
                        }

                        return;
                    }
                }

                #endregion

                #region Online overeni zdrojove lokace

                if (_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokaceNull() && _typdokladu.cfg_onl_over_lokace > 0)
                {
                    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                    ProdejService.TypeOfRecord recordType;
                    if (!string.IsNullOrEmpty(pohyb_type))
                        recordType = (ProdejService.TypeOfRecord)Enum.Parse(typeof(ProdejService.TypeOfRecord), pohyb_type, true);
                    else recordType = ProdejService.TypeOfRecord.E;

                    ProdejService.StatusOverLokace so = await OnlineOverLokaceAsync(zbozi.ITEMNMBR, sn, expirace, locncode, sklad_id, di.QTYSHPPD, ProdejService.TYPLokace.SOURCE, recordType);
                    if (so != null)
                    {
                        switch (so.State)
                        {
                            case ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                                break;
                            case ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                                DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListPorusenoDoporucenePoradiPokracovatDotaz), "Warning", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                    return;
                                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "zdrporadi", DateTime.Now, Config.Settings.TerminalID, _parent.Uzivatel.ID.Value, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncode, ""));
                                break;
                            case ProdejService.STATUSOverLokace.ERROR:
                                await MessageBoxAsync.Show(_parent, so.Message.Trim(), "Error", MessageBoxButtons.OK);
                                return;
                            default: // neni mozne pokracovat
                                await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniMozneBratZLokace), "Ostatni", MessageBoxButtons.OK);
                                return;
                        }
                    }
                    else  // nic se nenacetlo
                    {
                        // chyba komunikace se serverem, dotaz zdali pokracovat
                        DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemOverLokaciPokracovatDotaz), "Info", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region OK Zde zacinaji doplnujici informace ...

                #region TODO Strediska 

                if (_typdokladu != null && _typdokladu.cfg_str > 0)
                {
                    //TaD: TODO Dodelat...
                    if (!StrediskoSet())
                    {
                        return;
                    }
                }

                #endregion

                #region TODO Pracovnici

                if (_typdokladu != null && _typdokladu.cfg_prac > 0)
                {
                    //TaD: TODO Dodelat...
                    if (!PracovniciSet())
                    {
                        return;
                    }
                }

                #endregion

                #region Palety

                if ((_typdokladu != null && _typdokladu.cfg_palety > 0 && nmbrpal == null))
                {
                    // kontrola, zdali je paleta null, pokud ano, nepustit dal ...
                    await MessageBoxAsync.Show(_parent, "Není vybrána paleta", "Warning", MessageBoxButtons.OK);
                    return;
                }

                #endregion

                #region REZ1

                rez1 = zbozi.IsREZ1Null() ? string.Empty : zbozi.REZ1.Trim();
                if (zbozi.CZ_Rez1_Track > 0)
                {

                    var rez1Data = await InputBoxAsync.Show(
                        _parent,
                        Title: Konfigurace_Singleton.Instance.Prodej.REZ1.PROD_NAME,
                        Message: GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty),
                        Defaultvalue: Konfigurace_Singleton.Instance.Prodej.REZ1.Pamatovat ? Config.Settings_DB.Prodej_REZ1_LastValue : rez1,
                        AllowEmpty: !Konfigurace_Singleton.Instance.Prodej.REZ1.Povinne,
                        checkLen: false,
                        len: 0,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Konfigurace_Singleton.Instance.Prodej.REZ1.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText
                        );

                    if (rez1Data.Dialog_Result == DialogResult.Cancel)
                        return;

                    rez1 = rez1Data.Value;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ1.Pamatovat)
                    {
                        //Tohle si sice bude pamatovat na terminalu posledny REZ zadany, ale Server se to nedozví... a když se z teminalu smaže
                        // konfiguračny soubor a naraje z serveru aktualni tak se data stratí...
                        Config.Settings_DB.Prodej_REZ1_LastValue = rez1;
                    }
                }

                #endregion

                #region  REZ2

                rez2 = zbozi.IsREZ2Null() ? string.Empty : zbozi.REZ2.Trim();
                if (zbozi.CZ_Rez2_Track > 0)
                {

                    var rez2Data = await InputBoxAsync.Show(
                        _parent,
                        Title: Konfigurace_Singleton.Instance.Prodej.REZ2.PROD_NAME,
                        Message: GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty),
                        Defaultvalue: Konfigurace_Singleton.Instance.Prodej.REZ2.Pamatovat ? Config.Settings_DB.Prodej_REZ2_LastValue : rez2,
                        AllowEmpty: !Konfigurace_Singleton.Instance.Prodej.REZ2.Povinne,
                        checkLen: false,
                        len: 0,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Konfigurace_Singleton.Instance.Prodej.REZ2.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText
                        );

                    if (rez2Data.Dialog_Result == DialogResult.Cancel)
                        return;

                    rez2 = rez2Data.Value;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ2.Pamatovat)
                    {
                        //Tohle si sice bude pamatovat na terminalu posledny REZ zadany, ale Server se to nedozví... a když se z teminalu smaže
                        // konfiguračny soubor a naraje z serveru aktualni tak se data stratí...
                        Config.Settings_DB.Prodej_REZ2_LastValue = rez2;
                    }
                }

                #endregion

                #region REZ3

                rez3 = zbozi.IsREZ3Null() ? string.Empty : zbozi.REZ3.Trim();
                if (zbozi.CZ_Rez3_Track > 0)
                {

                    var rez3Data = await InputBoxAsync.Show(
                        _parent,
                        Title: Konfigurace_Singleton.Instance.Prodej.REZ3.PROD_NAME,
                        Message: GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty),
                        Defaultvalue: Konfigurace_Singleton.Instance.Prodej.REZ3.Pamatovat ? Config.Settings_DB.Prodej_REZ3_LastValue : rez3,
                        AllowEmpty: !Konfigurace_Singleton.Instance.Prodej.REZ3.Povinne,
                        checkLen: false,
                        len: 0,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Konfigurace_Singleton.Instance.Prodej.REZ3.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText
                        );

                    if (rez3Data.Dialog_Result == DialogResult.Cancel)
                        return;

                    rez3 = rez3Data.Value;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ3.Pamatovat)
                    {
                        //Tohle si sice bude pamatovat na terminalu posledny REZ zadany, ale Server se to nedozví... a když se z teminalu smaže
                        // konfiguračny soubor a naraje z serveru aktualni tak se data stratí...
                        Config.Settings_DB.Prodej_REZ3_LastValue = rez3;
                    }
                }

                #endregion

                #region REZ4

                rez4 = zbozi.IsREZ4Null() ? string.Empty : zbozi.REZ4.Trim();
                if (zbozi.CZ_Rez4_Track > 0)
                {

                    var rez4Data = await InputBoxAsync.Show(
                        _parent,
                        Title: Konfigurace_Singleton.Instance.Prodej.REZ4.PROD_NAME,
                        Message: GetString(Resource.String.Prodej3ProdejListZadaniDoplnujiciHodnoty),
                        Defaultvalue: Konfigurace_Singleton.Instance.Prodej.REZ4.Pamatovat ? Config.Settings_DB.Prodej_REZ4_LastValue : rez4,
                        AllowEmpty: !Konfigurace_Singleton.Instance.Prodej.REZ4.Povinne,
                        checkLen: false,
                        len: 0,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Konfigurace_Singleton.Instance.Prodej.REZ4.Cislo ? Android.Text.InputTypes.ClassNumber : Android.Text.InputTypes.ClassText
                        );

                    if (rez4Data.Dialog_Result == DialogResult.Cancel)
                        return;

                    rez4 = rez4Data.Value;
                    if (Konfigurace_Singleton.Instance.Prodej.REZ4.Pamatovat)
                    {
                        Config.Settings_DB.Prodej_REZ4_LastValue = rez4;
                    }
                }

                #endregion

                #region Ceny

                Price price = new Price();

                zjisti_cenu(zbozi, _parent._Item.Odberatel, _parent._Item.Mena, price); // price je objekt, tedy odkazem => meni se vlastnosti ...
                nastav_cenu(di, price);

                #endregion

                #region Odberatel

                odb_id = Get_Odberatel(zbozi);

                #endregion

                #endregion

                #region Overovat pohyb

                if (Konfigurace_Singleton.Instance.Prodej.OverovatPohyb)
                {
                    decimal qtyshppdnacteno = Nacteno(zbozi.ITEMNMBR);
                    var statusOverP = await OverPohybAsync(sn, zbozi.ITEMNMBR, _parent._Item.SkladZdroj == null ? string.Empty : _parent._Item.SkladZdroj.skl_id, di.QTYSHPPD, qtyshppdnacteno);

                    if (!statusOverP.status)
                        return;
                    else if (_parent._Item.SkladZdroj != null && (di.QTYSHPPD + qtyshppdnacteno) > statusOverP.QTY)
                    {

                        // TODO : dialog s odpovedi

                        DialogResult dr = await MessageBoxAsync.Show(_parent, string.Format(GetString(Resource.String.Prodej3ProdejListMnozstviVetsiNezStavSkladuPokracovatDotaz), di.QTYSHPPD, qtyshppdnacteno, statusOverP.QTY), GetString(Resource.String.FormsSejmiKodFormDotaz), MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region zadani ciloveho skladu

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
                                await MessageBoxAsync.Show(_parent, string.Format(GetString(Resource.String.Prodej3ProdejListCilovySkladNenalezenDotaz), skl_id_dst.Trim()), "info", MessageBoxButtons.OK);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
                        return;
                    }

                    // rucni vyber skladu, pokud neni jiz zvolen
                    if (skladdest == null)
                    {

                        var skladO = await CilovySkladAsync();


                        if (!skladO.status)
                            return;

                        skladdest = skladO.Sklad;

                        if (skladdest == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrán sklad, přestože je vyžadován!");
                            return;
                        }
                    }

                    sklad_id_dest = skladdest.skl_id;
                }

                #endregion

                #region Zadani cilove lokace

                string locncodedest = string.Empty;
                PrijemService.Obecne dsdest = null;
                bool onlineKontrolaCilLokace = true;
                locncodedest = (_typdokladu == null || _typdokladu.Ispredvyplnit_locncodedestNull()) ? string.Empty : _typdokladu.predvyplnit_locncodedest.Trim();
                if (_typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
                {
                    // vybrani cilove lokace z ciselniku lokaci
                    if (string.IsNullOrEmpty(locncodedest.Trim()) && _typdokladu != null && !_typdokladu.Iscfg_lokace_dest_ciselnikNull() && _typdokladu.cfg_lokace_dest_ciselnik > 0)
                    {

                        var stat_lok_O = await GetLokaciAsync(sklad_id_dest, Resource.String.Prodej3ProdejListVyberCiloveLokace);
                        if (stat_lok_O != null && stat_lok_O.status)
                        {
                            if (stat_lok_O.LOCNCODE == null)
                            {
                                string msg = "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!";
                                await MessageBoxAsync.Show(_parent, msg, "Error", MessageBoxButtons.OK);
                                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, msg);
                                return;
                            }
                            else
                            {
                                locncodedest = stat_lok_O.LOCNCODE;
                            }
                        }
                        else
                            return;

                        if (locncodedest == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrána cílová lokace z číselníku, přestože je vyžadována!");
                            return;
                        }

                    }
                    else if (!_typdokladu.Iscfg_onl_dop_lokace_destNull() && _typdokladu.cfg_onl_dop_lokace_dest > 0)
                    {   // online doporucene cilove lokace
                        // zobrazit seznam
                        dsdest = await OnlineGetDoporuceneCiloveLokaceAsync(zbozi.ITEMNMBR, sn, sklad_id_dest); //sklad_id);
                        if (dsdest != null)
                        {
                            // vratily se nejake zaznamy
                            if (dsdest.Lokace.Count > 0)
                            {
                                onlineKontrolaCilLokace = (_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0);


                                var stat_lok_O = await GetLokaciAsync(sklad_id_dest, Resource.String.Prodej3ProdejListVyberCiloveLokace);

                                if (stat_lok_O != null && stat_lok_O.status)
                                {
                                    if (stat_lok_O.LOCNCODE == null)
                                    {
                                        string msg = "Není vybrána zdrojová lokace z číselníku, přestože je vyžadována!";
                                        await MessageBoxAsync.Show(_parent, msg, "Error", MessageBoxButtons.OK);
                                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, msg);
                                        return;
                                    }
                                    else
                                    {
                                        locncodedest = stat_lok_O.LOCNCODE;
                                    }
                                }

                                onlineKontrolaCilLokace = false;
                            }
                            else
                            {
                                // nic se nevratilo, je treba zadat rucne ...
                                //locncodedest = SejmiLocncode(zbozi, false);
                                //if (locncodedest == "!@")
                                //    return;

                                #region Lokace Zadani ručně hodnotu

                                string locncodeTMP = string.Empty;

                                while (true)
                                {

                                    var _pppLOC = SejmiLOCNCODE_prepare(zbozi, true, locncodeTMP);
                                    var ppp_data = await Get_PPP_Async(_pppLOC);

                                    if (!ppp_data.status)
                                        return;

                                    if (ppp_data.LOCNCODE != null)
                                        return;

                                    locncodeTMP = ppp_data.LOCNCODE;

                                    string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", locncodeTMP);

                                    ///konfiguracne zobrazovat nasnimanou lokaci
                                    if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                                    {
                                        if (await MessageBoxAsync.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                            continue;
                                        else
                                            break;
                                    }
                                    else
                                        break;

                                }

                                locncodedest = locncodeTMP;

                                #endregion
                            }
                        }
                        else
                        {
                            // nic se nevratilo, je treba zadat rucne ...
                            //locncodedest = SejmiLocncode(zbozi, false);
                            //if (locncodedest == "!@")
                            //    return;

                            #region Lokace Zadani ručně hodnotu

                            string locncodeTMP = string.Empty;

                            while (true)
                            {

                                var _pppLOC = SejmiLOCNCODE_prepare(zbozi, true, locncodeTMP);
                                var ppp_data = await Get_PPP_Async(_pppLOC);

                                if (!ppp_data.status)
                                    return;

                                if (ppp_data.LOCNCODE != null)
                                    return;

                                locncodeTMP = ppp_data.LOCNCODE;

                                string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", locncodeTMP);

                                ///konfiguracne zobrazovat nasnimanou lokaci
                                if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                                {
                                    if (await MessageBoxAsync.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                        continue;
                                    else
                                        break;
                                }
                                else
                                    break;

                            }

                            locncodedest = locncodeTMP;

                            #endregion

                        }
                    }
                    else
                    {
                        // lokace se vyplnuje rucne
                        //locncodedest = SejmiLocncode(zbozi, false);
                        //if (locncodedest == "!@")
                        //    return;

                        #region Lokace Zadani ručně hodnotu

                        string locncodeTMP = string.Empty;

                        while (true)
                        {

                            var _pppLOC = SejmiLOCNCODE_prepare(zbozi, true, locncodeTMP);
                            var ppp_data = await Get_PPP_Async(_pppLOC);

                            if (!ppp_data.status)
                                return;

                            if (ppp_data.LOCNCODE == null)
                                return;

                            locncodeTMP = ppp_data.LOCNCODE;

                            string message = string.Format("Nasnímána Lokace: {0} \n Pokračovat?", locncodeTMP);

                            ///konfiguracne zobrazovat nasnimanou lokaci
                            if (Konfigurace_Singleton.Instance.Prodej.DialogNasnimanaLokace)
                            {
                                if (await MessageBoxAsync.Show(_parent, message, "Nasnimana Lokace", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                    continue;
                                else
                                    break;
                            }
                            else
                                break;

                        }

                        locncodedest = locncodeTMP;

                        #endregion
                    }
                }

                #endregion

                #region Online overeni cilove lokace

                if ((_typdokladu != null && !_typdokladu.Iscfg_onl_over_lokace_destNull() && _typdokladu.cfg_onl_over_lokace_dest > 0) && onlineKontrolaCilLokace)
                {
                    string pohyb_type = _typdokladu.Iscfg_lok_mech_pohyb_typeNull() ? string.Empty : _typdokladu.cfg_lok_mech_pohyb_type;
                    ProdejService.TypeOfRecord recordType;
                    if (!string.IsNullOrEmpty(pohyb_type))
                        recordType = (ProdejService.TypeOfRecord)Enum.Parse(typeof(ProdejService.TypeOfRecord), pohyb_type, true);
                    else recordType = ProdejService.TypeOfRecord.E;

                    ProdejService.StatusOverLokace so = await OnlineOverLokaceAsync(zbozi.ITEMNMBR, sn, expirace, locncodedest, sklad_id_dest, di.QTYSHPPD, ProdejService.TYPLokace.DEST, recordType);
                    if (so != null)
                    {
                        switch (so.State)
                        {
                            case ProdejService.STATUSOverLokace.OK: // vse v poradku, mozno pokracovat
                                break;
                            case ProdejService.STATUSOverLokace.WARNING: // poruseno doporucene poradi, mozno pokracovat
                                DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListPorusenoDoporucenePoradiCilLokacePokracovatDotaz), "Warning", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                    return;
                                //Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "2", "cilporadi", DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, "", "", "Prodej", null, "", zbozi.ITEMNMBR.Trim(), locncodedest, ""));
                                break;
                            case ProdejService.STATUSOverLokace.ERROR:
                                await MessageBoxAsync.Show(_parent, so.Message.Trim(), "Error", MessageBoxButtons.OK);
                                return;
                            default: // neni mozne pokracovat
                                await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListNeniMozneUlozitNaLokaci), "Error", MessageBoxButtons.OK);
                                return;
                        }
                    }
                    else  // nic se nenacetlo
                    {
                        // chyba komunikace se serverem
                        DialogResult dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListKomunikaceServeruProblemOverCilLokaciPokracovatDotaz), "Error", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                            return;
                    }
                }

                #endregion

                #region Vyplneni DI jednoho řadku

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

                if (zbozi.IsVNDITNUMNull())
                    di.SetVNDITNUMNull();
                else
                    di.VNDITNUM = zbozi.VNDITNUM;

                di.CZ_CarKod = zbozi.CZ_CarKod;
                di.INPUT_MODE = _input_mode;

                if(zbozi.IsITEMCODENull())
                    di.SetITEMCODENull();
                else
                    di.ITEMCODE = zbozi.ITEMCODE;

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
                    //if (nmbrpal.Code != null)
                    //    di.NMBRPAL = nmbrpal.Code.Trim();

                    //di.TYPEPAL = string.Empty;  // TODO: dodelat ...
                    //                            // neni implementovano ...
                    //                            //if (nmbrpal.Code != null)
                    //                            //    di.TYPEPAL = nmbrpal.Type.Trim();

                    di.NMBRPAL = nmbrpal.sscc;
                    di.TYPEPAL = nmbrpal.ID;

                }

                if (expirace.HasValue)
                    di.EXPIRACE = expirace.Value;
                else
                    di.SetEXPIRACENull();


                #endregion

                #region Rozhodnuti Sklad/Expedice/Rozdelit

                RozhodovatSkladExpediceRozdelit(di);

                #endregion

                #region lokace

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
                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - start");
                        LokaceLog.writeBody(pohybrow, _parent.Uzivatel.ID.Value.ToString());

                        LokaceService.StatusLokace sl = DataInfo_Static.ProdejGO_Instance.servis_lokace.MoveItem(pohybrow);
                        switch (sl.State)
                        {
                            case LokaceService.States.OK:
                                break;
                            case LokaceService.States.ERROR:
                                await MessageBoxAsync.Show(_parent, "Nepodařilo se přidat záznam lokace, záznam nebude přidán!\n'" + sl.ErrorMessage + "'", "error", MessageBoxButtons.OK);
                                return;
                            default:
                                await MessageBoxAsync.Show(_parent, "Neočekávaná chyba, nepodařilo se přidat záznam do lokací. Záznam nebude přidán!", "info", MessageBoxButtons.OK);
                                return;
                        }

                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Location, "Operation:add,Mode:online,Modul:R,TypeOfRecord:" + pohyb_type + ",Function:" + this.ToString() + ".MoveItem - end");
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);

                        if (await MessageBoxAsync.Show(_parent, ex.Message + "\nPřejete si přesto uložit záznam do nasnímaných položek?", "Dotaz", MessageBoxButtons.YesNo) != DialogResult.Yes)
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
                                            dr = await MessageBoxAsync.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n" + sl.ErrorMessage + "\nPřejete si tedy záznam uložit?", "Error", MessageBoxButtons.YesNo);
                                            return;
                                        default:
                                            dr = await MessageBoxAsync.Show(_parent, "Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si tedy záznam uložit?", "Info", MessageBoxButtons.YesNo);
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
                                    if (await MessageBoxAsync.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n'" + exex.Message + "'\nPřejete si tedy záznam uložit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                #region Vlozeni jednoho radku do DI

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
                        if (DialogResult.Yes != await MessageBoxAsync.Show(_parent, ex.Message + "\nPřejete si opakovat operaci lokálního uložení?", "Information", MessageBoxButtons.YesNo))
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
                                                dr = await MessageBoxAsync.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n'" + sl.ErrorMessage + "'\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo);
                                                break;
                                            default:
                                                dr = await MessageBoxAsync.Show(_parent, "Neočekávaná chyba, nepodařilo se odstranit záznam v lokačním systému!\n\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo);
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
                                        if (await MessageBoxAsync.Show(_parent, "Nepodařilo se odstranit záznam v lokačním systému!\n" + exex.Message + "\nPřejete si opakovat operaci lokálního uložení?", "Otazka", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                #region Zvuk po napipnuti

                if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni))
                {
                    if (File.Exists(Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni)))
                    {
                        await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundUspesneVlozeni)); //ok, vlozeno pro informaci
                    }
                }

                #endregion

                #region Tisk po pridani zboží

                // Zjištění, zdali se má tisknout (podle cfg v DB)
                if (Konfigurace_Singleton.Instance.Prodej.PovolitPrintServer && (_typdokladu != null && _typdokladu.cfg_tisk > 0))
                {
                    bool? TiskSCenou = null;
                    bool vytisteno = false;

                    if (Konfigurace_Singleton.Instance.Prodej.DialogTisk)
                    {
                        var dr = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListTiskEtiketyDotaz), "Otazka", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.No)
                        {
                            _parent.UpdateForm();
                            return;
                        }
                    }

                    if (Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou)
                    {
                        if (Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou_ZobrazDialog)
                        {
                            DialogResult dres = await MessageBoxAsync.Show(_parent, GetString(Resource.String.Prodej3ProdejListTiskEtiketaSCenou), "Otazka", MessageBoxButtons.YesNo);

                            if (dres == DialogResult.Yes)
                                TiskSCenou = true;
                            else { TiskSCenou = false; }
                        }
                        else
                        {
                            TiskSCenou = !Konfigurace_Singleton.Instance.Prodej.EtiketaTiskDotazSCenou_Cena;
                        }
                    }


                    if (Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                    {
                        vytisteno = await ProdejTisk.PrintAsync(_parent,
                            row095: null,
                            rowDI: di,
                            data: null,
                            printerModule: Fask.PrinterFactory.PrinterModules.ProdejNasnimane,
                            pocetVytisku: 1,
                            TiskSCenou: TiskSCenou);
                    }
                    else
                    {
                        if (Konfigurace_Singleton.Instance.Prodej.EtiketaTisk_PrebiratMnozstvi)
                        {
                            int TiskQTY = System.Convert.ToInt32(di.QTYSHPPD);
                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Tisk Prodej , Prebirane Množstvi :" + TiskQTY.ToString());
                            //vytisteno = await ProdejTisk.PrintAsync(_parent, di, Fask.PrinterFactory.PrinterModules.ProdejNasnimane, TiskQTY, TiskSCenou);

                            vytisteno = await ProdejTisk.PrintAsync(_parent,
                                row095: null,
                                rowDI: di,
                                data: null,
                                printerModule: Fask.PrinterFactory.PrinterModules.ProdejNasnimane,
                                pocetVytisku: TiskQTY,
                                TiskSCenou: TiskSCenou);

                        }
                        else
                        {
                            //vytisteno = await ProdejTisk.PrintAsync(_parent, di, Fask.PrinterFactory.PrinterModules.ProdejNasnimane, string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety) ? (int?)null : System.Convert.ToInt32(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety), TiskSCenou);

                            vytisteno = await ProdejTisk.PrintAsync(_parent,
                                row095: null,
                                rowDI: di,
                                data: null,
                                printerModule: Fask.PrinterFactory.PrinterModules.ProdejNasnimane,
                                pocetVytisku: string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety) ? (int?)null : System.Convert.ToInt32(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety),
                                TiskSCenou: TiskSCenou);
                        }
                    }
                }

                #endregion

                _parent.UpdateForm();
            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                //_parent.Scanner_STOP();
            }
        }



        #endregion

        #region Pomocne class objekty

        private class O_Get_Online_Sklady
        {
            public ProdejService.STATUS Status;
            public string SKL_ID;
            public string SKL_ID_DEST;
        }

        public class O_GetData
        {
            public bool status;
            public decimal? QTY;
            public string SERLTNUM;
            public string LOCNCODE;
            public Sklady.CZMST093Row Sklad;
        }

        public class O_OverPohyb
        {
            public bool status;
            public decimal? QTY;
        }

        #endregion

        #region Revidovane pomocne metody, TODO Lepe okomentovat a roztridit ešte


        #region Metody pro zadani množství


        private decimal? Get_QTY_FromWeightCode(bool podminka, Zbozi.CZMST095Row zbozi, BaseCode code)
        {
            decimal? qty = null;

            if (podminka && code is Fask.Parsing.Codes.Interfaces.ICodeWeight)
            {
                qty =
                    (((Fask.Parsing.Codes.Interfaces.ICodeWeight)code).Weight
                    / (zbozi.IsWEIGHTNull() || (zbozi.WEIGHT == 0) ? 1 : zbozi.WEIGHT)
                    / (zbozi.IsQTYPACKNull() || (zbozi.QTYPACK == 0) ? 1 : zbozi.QTYPACK)
                    );
            }

            return qty;
        }



        private decimal? Get_QTY(Zbozi.CZMST095Row zbozi, BaseCode code)
        {
            if (Konfigurace_Singleton.Instance.Prodej.MnozstviREZ1Vypln && !zbozi.IsREZ1Null())
            {
                decimal ret;
                bool state = decimal.TryParse(zbozi.REZ1.Trim(), out ret);
                if (state)
                    return ret;
                else
                    return null;
            }
            else if (code is Fask.Parsing.Codes.Interfaces.ICodeWeight)
            {
                return Get_QTY_FromWeightCode(Konfigurace_Singleton.Instance.Prodej.ZobrazitDialogZadaniMnozstviParsovanehoKodu, zbozi, code);
            }
            else if (_typdokladu != null && !_typdokladu.Iscfg_predvyplnit_mnozstviNull() && _typdokladu.cfg_predvyplnit_mnozstvi > 0)
            {
                return zbozi.QTY;
            }
            else
            {
                return null;
            }
        }

        private Prodej_PPP_Objekt PPP_Mnozstvi(Zbozi.CZMST095Row zbozi, string sn)
        {
            bool baleni = zbozi.QTYPACK > 0;

            Prodej_PPP_Objekt _PPP_Objekt = new Prodej_PPP_Objekt();
            _PPP_Objekt.Odberatel = _parent._Item.Odberatel;
            _PPP_Objekt.Serltnum = sn;
            _PPP_Objekt.Volajici = Volajici_ProdejPridatPolozku.QTY;
            _PPP_Objekt.Zbozi = zbozi;

            _PPP_Objekt.Popis = baleni ? GetString(Resource.String.Prodej3ProdejListMnozstviBaleni) : GetString(Resource.String.Prodej3ProdejListMnozstvi);
            _PPP_Objekt.Text = baleni ? GetString(Resource.String.Prodej3ProdejListVlozteMnozstviBaleni) : GetString(Resource.String.Prodej3ProdejListVlozteMnozstvi);
            _PPP_Objekt.CodeType = Android.Text.InputTypes.ClassNumber;
            _PPP_Objekt.Len = 0;
            _PPP_Objekt.CheckLen = false;
            _PPP_Objekt.AllowEmpty = false;
            _PPP_Objekt.ScannerEnable = Konfigurace_Singleton.Instance.Prodej.PovolitZadaniMnozstviScannerem;

            return _PPP_Objekt;
        }

        #endregion

        #region Asynchronne metody

        private Task<ProdejService.Location> OnlineGetMaterialAsync(string itemnmbr, string skl_id, string serltnum)
        {

            TaskCompletionSource<ProdejService.Location> tcs = new TaskCompletionSource<Location>();

            ProdejService.Location ds = new ProdejService.Location();

            try
            {
                ds = DataInfo_Static.ProdejGO_Instance.servis_prodej.Online_GetMaterial(itemnmbr, skl_id, serltnum, _typdokladu != null ? _typdokladu.doc_id : string.Empty, false);
                tcs.SetResult(ds);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetException(ex);
            }


            return tcs.Task;
        }

        private Task<O_GetData> ZadejVyberMaterialuAsync(Location ds)
        {
            _parent.TCS_ZadejVyberMaterialuAsync = new TaskCompletionSource<O_GetData>();

            try
            {
                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(Prodej_VyberMaterial),
                        _parent.ResoultCode_VyberMaterial,
                        ds: ds
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                _parent.TCS_ZadejVyberMaterialuAsync.SetException(ex);
            }

            return _parent.TCS_ZadejVyberMaterialuAsync.Task;
        }

        private Task<O_GetData> GetLokaciAsync(string sklad_id_dest, int? lokText)
        {
            try
            {
                _parent.TCS_GetLokaciAsync = new TaskCompletionSource<O_GetData>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(Prodej_Lokace),
                        _parent.ResoultCode_Lokace,
                        SKL_ID: sklad_id_dest,
                        Lok_DESC: lokText
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetLokaciAsync.SetException(ex);
            }

            return _parent.TCS_GetLokaciAsync.Task;
        }

        private Task<O_GetData> Get_PPP_Async(Prodej_PPP_Objekt pPP_Objekt)
        {
            try
            {
                _parent.TCS_PPP_Async = new TaskCompletionSource<O_GetData>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(Prodej_PridatPolozku),
                        _parent.ResoultCode_PPP,
                        pPP_Objekt: pPP_Objekt
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_PPP_Async.SetException(ex);
            }

            return _parent.TCS_PPP_Async.Task;
        }

        private Task<O_GetData> CilovySkladAsync()
        {
            try
            {
                _parent.TCS_CilovySkladAsync = new TaskCompletionSource<O_GetData>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(Prodej_Sklady),
                        _parent.ResoultCode_CilovySklad
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_CilovySkladAsync.SetException(ex);
            }

            return _parent.TCS_CilovySkladAsync.Task;
        }

        private Task<StatusOverLokace> OnlineOverLokaceAsync(string itemnmbr, string serltnum, DateTime? expirace, string locncode, string skl_id, decimal qtyshppd, ProdejService.TYPLokace locationType, ProdejService.TypeOfRecord recordType)
        {
            TaskCompletionSource<StatusOverLokace> tcs = new TaskCompletionSource<StatusOverLokace>();
            StatusOverLokace so = new StatusOverLokace();
            try
            {
                so = DataInfo_Static.ProdejGO_Instance.servis_prodej.Online_OverLokace(itemnmbr, serltnum, expirace, locncode, skl_id, qtyshppd, _typdokladu != null ? _typdokladu.doc_id : string.Empty, locationType, recordType);
                tcs.SetResult(so);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        private Task<O_Get_Online_Sklady> OnlineGetSkladAsync(string doc_id, string itemnmbr, string serltnum)
        {
            TaskCompletionSource<O_Get_Online_Sklady> tcs = new TaskCompletionSource<O_Get_Online_Sklady>();
            //O_Get_Online_Sklady _Online_Sklady = new O_Get_Online_Sklady();

            string skl_id = null;
            string skl_id_dest = null;

            try
            {
                var status = DataInfo_Static.ProdejGO_Instance.servis_prodej.GetSklad(Config.Settings.TerminalID, _parent.Uzivatel.ID.Value, doc_id, itemnmbr, serltnum, out skl_id, out skl_id_dest);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetResult(new O_Get_Online_Sklady() { Status = STATUS.ERROR });
            }

            tcs.SetResult(new O_Get_Online_Sklady()
            {
                Status = STATUS.OK,
                SKL_ID = skl_id,
                SKL_ID_DEST = skl_id_dest
            });

            return tcs.Task;
        }

        private Task<PrijemService.Obecne> OnlineGetDoporuceneCiloveLokaceAsync(string itemnmbr, string serltnum, string skl_id)
        {
#warning Na prodeji se musi pouzivat Prodejni webservice => PrijemService.Online_GetDoporuceneLokace predelat do ProdejService...

            TaskCompletionSource<PrijemService.Obecne> tcs = new TaskCompletionSource<PrijemService.Obecne>();
            // TODO: do budoucna resit pres prodej.asmx, posilat typ dokladu, ...
            PrijemService.Obecne ds = new PrijemService.Obecne();

            try
            {
                ds = DataInfo_Static.ProdejGO_Instance.servis_Prijem.Online_GetDoporuceneLokace(itemnmbr, skl_id, serltnum);
                tcs.SetResult(ds);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        private Task<ProdejService.StatusResult> Online_DISP(string ITEMNMBR, decimal QTYSHPPD, string sklad_id, string LOCNCODE, string SERLTNUM)
        {
            TaskCompletionSource<ProdejService.StatusResult> tcs = new TaskCompletionSource<ProdejService.StatusResult>();
            //ProdejService.StatusResult info = null;
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

                    var info = DataInfo_Static.ProdejGO_Instance.servis_prodej.Disponibilita(disponibilita);
                    tcs.SetResult(info);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    tcs.SetException(ex);
                }
            }

            return tcs.Task;
        }

        private Task<O_OverPohyb> OverPohybAsync(string sn, string itemnmbr, string sklad_id, decimal qtyshppd, decimal qtyshppdNacteno)
        {
            TaskCompletionSource<O_OverPohyb> tcs = new TaskCompletionSource<O_OverPohyb>();

            decimal outqtyshppd;

            try
            {
                qtyshppd = qtyshppd + qtyshppdNacteno;

                ProdejService.ProdejPohyb prodejPohyb = new ProdejService.ProdejPohyb();
                prodejPohyb.Doc_id = (_typdokladu == null ? string.Empty : _typdokladu.doc_id);
                prodejPohyb.Doc_id2 = (_typdokladu == null ? string.Empty : _typdokladu.doc_id2);
                prodejPohyb.Itemnmbr = itemnmbr;
                prodejPohyb.Serltnum = sn;
                prodejPohyb.skl_id = sklad_id;
                prodejPohyb.qtyshppd = qtyshppd;

                ProdejService.StatusOverPohyb statusOP = DataInfo_Static.ProdejGO_Instance.servis_prodej.OverPohyb(prodejPohyb, Config.Settings.TerminalID, _parent.Uzivatel.ID.Value);

                if (!statusOP.PohybOK)
                {
                    tcs.SetException(new Exception(statusOP.Message));
                }

                outqtyshppd = statusOP.a_dispozice;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                outqtyshppd = -1;
                tcs.SetResult(new O_OverPohyb()
                {
                    status = false,
                    QTY = -1

                }); ;

            }


            tcs.SetResult(new O_OverPohyb()
            {
                status = true,
                QTY = outqtyshppd
            });

            return tcs.Task;

        }

        #endregion

        #region Normalne metody

        private string Get_Odberatel(Zbozi.CZMST095Row zbozi)
        {
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

            return odb_id;
        }

        private string Get_ZdrojSklad(Zbozi.CZMST095Row zbozi)
        {
            string sklad_id;

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

            return sklad_id;
        }

        private string Get_CilSklad(string sklad_id)
        {
            string sklad_id_dest = string.Empty;

            if (_typdokladu != null && !_typdokladu.Iscfg_skl_id_dest_prevzitNull() && _typdokladu.cfg_skl_id_dest_prevzit > 0)
            {
                sklad_id_dest = sklad_id;
            }
            else if (_parent._Item.SkladCil != null) // je vybran cilovy sklad
            {
                sklad_id_dest = _parent._Item.SkladCil.skl_id;
            }

            return sklad_id_dest;
        }

        private string Get_Sarze_Parse_anebo_Zbozi(Zbozi.CZMST095Row zbozi, BaseCode code)
        {
            string sn;
            if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr) && !string.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
                sn = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;
            else
                sn = zbozi.IsSERLTNUMNull() ? string.Empty : zbozi.SERLTNUM.Trim();
            return sn;
        }

        private Prodej_PPP_Objekt SejmiLOCNCODE_prepare(Zbozi.CZMST095Row zbozi, bool Zdrojova_Loc, string prefilledVal = null)
        {
            Prodej_PPP_Objekt _ppp = new Prodej_PPP_Objekt();
            try
            {
                if (_typdokladu == null)
                {
                    _ppp.Popis = Zdrojova_Loc ? GetString(Resource.String.Prodej3ProdejListLokace) : GetString(Resource.String.Prodej3ProdejListCilovaLokace);
                    _ppp.CodeType = Android.Text.InputTypes.ClassText;
                    _ppp.Len = 0;
                    _ppp.CheckLen = true;
                    _ppp.AllowEmpty = false;
                    _ppp.Zbozi = zbozi;
                    _ppp.Odberatel = _parent._Item.Odberatel;
                    _ppp.Text = GetString(Resource.String.Prodej3ProdejListVlozteSklad);
                    _ppp.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;
                    _ppp.Kod = zbozi.LOCNCODE;
                }
                else
                {

                    _ppp.Popis = Zdrojova_Loc ? GetString(Resource.String.Prodej3ProdejListLokace) : GetString(Resource.String.Prodej3ProdejListCilovaLokace);
                    _ppp.CodeType = Android.Text.InputTypes.ClassText;
                    _ppp.Len = 0;
                    _ppp.CheckLen = true;
                    _ppp.AllowEmpty = false;
                    _ppp.Zbozi = zbozi;
                    _ppp.Odberatel = _parent._Item.Odberatel;
                    _ppp.Text = Zdrojova_Loc ? GetString(Resource.String.Prodej3ProdejListVlozteLokaci) : GetString(Resource.String.Prodej3ProdejListVlozteCilovouLokaci);
                    _ppp.Volajici = Volajici_ProdejPridatPolozku.LOCNCODE;

                    if (!Zdrojova_Loc && _typdokladu != null && !_typdokladu.Iscfg_lokace_destNull() && _typdokladu.cfg_lokace_dest > 0)
                        _ppp.Kod = string.Empty;
                    else
                        _ppp.Kod = string.IsNullOrEmpty(prefilledVal) ? zbozi.LOCNCODE : prefilledVal;

                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            return _ppp;

        }


        #endregion


        private void StartAktivityNasledujici(
            Type typAktivity,
            int ResoultCode = 0,
            Location ds = null,
            string SKL_ID = null,
            int? Lok_DESC = null,
            Prodej_PPP_Objekt pPP_Objekt = null
        )
        {
            Intent intent = new Intent(_parent, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(_parent.Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (ResoultCode == _parent.ResoultCode_CilovySklad)
            {
                intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Cil);
                intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.SkladRow);
            }

            if (ds != null)
            {
                Bundle bundleLoc = new Bundle();
                bundleLoc.PutBinder(DataInfo_Static.object_Location, new WrapperForBinder<Location>(ds));
                intent.PutExtra(DataInfo_Static.Location, bundleLoc);
            }

            if (!string.IsNullOrEmpty(SKL_ID))
            {
                intent.PutExtra(DataInfo_Static.Lok_SKL_ID, SKL_ID);
            }

            if (Lok_DESC.HasValue)
            {
                intent.PutExtra(DataInfo_Static.Lok_Text, Lok_DESC.Value);
            }


            if (pPP_Objekt != null)
            {
                Bundle bundle_PPP = new Bundle();
                bundle_PPP.PutBinder(DataInfo_Static.object_PPP, new WrapperForBinder<Prodej_PPP_Objekt>(pPP_Objekt));
                intent.PutExtra(DataInfo_Static.PPP, bundle_PPP);
            }
            
            
            _parent.StartActivityForResult(intent, ResoultCode);

            
        }



        #endregion

        #region Hanibal, Sklad Expedice Rozdelit

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
        /// Metoda slouživi pro rozhodnuti zda položku dat na Sklad/Expedici alebo rozdelit
        /// </summary>
        private async void RozhodovatSkladExpediceRozdelit(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
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
                    await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundExpedice));
                }
                else if ((0 < Vysledek) && (Vysledek < PrijimaneMnozstvi))
                {
                    NaExpedici = Vysledek;
                    NaSklad = PrijimaneMnozstvi - Vysledek;
                    await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSkladExpedice));
                }
                else if (Vysledek <= 0)
                {
                    NaExpedici = 0;
                    NaSklad = PrijimaneMnozstvi;
                    await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSklad));
                }


                if (Konfigurace_Singleton.Instance.Prodej.ZobrazovatReport && !Konfigurace_Singleton.Instance.Prodej.Mnozstvi1Auto)
                {
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
                throw ex;
                //return false;
            }
            //return true;
        }



        #endregion

        #region Metody Cena

        private void zjisti_cenu(
    Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zbozi,
    Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel,
    Fask.SQLiteDBs.DataSets.Meny.CZMST097Row mena,
    //ref decimal cenasdani, 
    //ref decimal cenabezdane, 
    //ref decimal cenadan, 
    //ref bool jecenasdani, 
    //ref byte cenovahladina
    Price price
    )
        {
            // pokud je odberatel null, tak nelze zjistit cenu ... 
            if (odberatel == null)
                return;

            //using (Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter ta_zM = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter())
            //{
            //ta_zM.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikZboziDB);

            try
            {
                // zjisti cenu
                decimal cena = 0;
                decimal? cenaM = null;

                try { cena = (decimal)zbozi["PRICE" + odberatel.odb_typ.Trim()]; }
                catch { }
                //zjisti pouzitou cenovou hladinu
                price.cenovaHladina = PriceX(odberatel.odb_typ);
                //zjisti zda je cena uvedena s dani nebo bez dane
                price.jeCenaSDani = PriceXIsWithTax(odberatel.odb_typ);
                if (zbozi.IsMENA_IDNull())
                    price.mena = null;
                else
                    price.mena = zbozi.MENA_ID.Trim();

                if (mena == null && odberatel == null)
                    price.menaM = null;

                if (odberatel != null && !odberatel.Ismena_IDNull() && !String.IsNullOrEmpty(odberatel.mena_ID.Trim()))
                    price.menaM = odberatel.mena_ID.Trim();

                if (mena != null && !String.IsNullOrEmpty(mena.mena_ID.Trim()))
                    price.menaM = mena.mena_ID.Trim();

                if (price.menaM != null)
                {
                    //cenaM = ta_zM.GetPrice(zbozi.ITEMNMBR.Trim(), price.menaM, price.cenovaHladina);

                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi z = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(DataInfo_Static.CiselnikZboziDB))
                    {
                        cenaM = z.GetPrice(zbozi.ITEMNMBR.Trim(), price.menaM, price.cenovaHladina);

                    }


                    // TODO : Pokud neni nalezena cena zbozi, tak prepocitat kurzem? respektive co udelat???
                    // momentalne se neulozi cenaM, respektive bude prazdne ... 
                }

                if (!price.jeCenaSDani)
                {// v predloze je cena bez dane
                 //cenabezdane = cena;
                 //cenasdani = cena * (1 + _zbozi.TAXRATE / 100);
                 //cenadan = cenasdani - cenabezdane;
                    price.cenaBezDane = cena;
                    price.cenaDan = Math.Round(cena * (Math.Round(zbozi.TAXRATE / 100, 4)), 2);
                    price.cenaSDani = cena + price.cenaDan;

                    if (cenaM.HasValue)
                    {
                        price.cenaBezDaneM = cenaM;
                        price.cenaDanM = Math.Round(cenaM.Value * (Math.Round(zbozi.TAXRATE / 100, 4)), 2);
                        price.cenaSDaniM = cenaM + price.cenaDanM;
                    }
                }
                else
                {// v predloze je cena i s dani
                 //cenasdani = cena;
                 //cenabezdane = cena / (1 + _zbozi.TAXRATE / 100);
                 //cenadan = cenasdani - cenabezdane;
                    price.cenaSDani = cena;
                    price.cenaDan = Math.Round(cena * Math.Round((zbozi.TAXRATE / (100 + zbozi.TAXRATE)), 4), 2);
                    price.cenaBezDane = cena - price.cenaDan;

                    if (cenaM.HasValue)
                    {
                        price.cenaSDaniM = cenaM;
                        price.cenaDanM = Math.Round(cenaM.Value * Math.Round((zbozi.TAXRATE / (100 + zbozi.TAXRATE)), 4), 2);
                        price.cenaBezDaneM = cenaM - price.cenaDanM;
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle (ex.Message + "\n" + ex.StackTrace, "Prodej.Globals.zjisti_cenu");
            }
            //}
        }

        private byte PriceX(string typOdberatele)
        {
            string otyp = typOdberatele.Trim();
            if (otyp == "0") return 0;
            else if (otyp == "1") return 1;
            else if (otyp == "2") return 2;
            else if (otyp == "3") return 3;
            else if (otyp == "4") return 4;
            else if (otyp == "5") return 5;
            else return 0;
        }

        private bool PriceXIsWithTax(string typOdberatele)
        {
            string otyp = typOdberatele.Trim();
            if (otyp == "0") return Konfigurace_Singleton.Instance.Prodej.Cena.Price0IsWithTax;
            else if (otyp == "1") return Konfigurace_Singleton.Instance.Prodej.Cena.Price1IsWithTax;
            else if (otyp == "2") return Konfigurace_Singleton.Instance.Prodej.Cena.Price2IsWithTax;
            else if (otyp == "3") return Konfigurace_Singleton.Instance.Prodej.Cena.Price3IsWithTax;
            else if (otyp == "4") return Konfigurace_Singleton.Instance.Prodej.Cena.Price4IsWithTax;
            else if (otyp == "5") return Konfigurace_Singleton.Instance.Prodej.Cena.Price5IsWithTax;
            else return Konfigurace_Singleton.Instance.Prodej.Cena.Price0IsWithTax;
        }

        private void nastav_cenu(
        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di,
        //decimal cenasdani, 
        //decimal cenabezdane, 
        //decimal cenadan, 
        //bool jecenasdani, 
        //byte cenovahladina
        Price price
        )
        {
            // TODO : upravit nastaveni ceny pro cizi Meny ... czmst095M

            di.PRICEX = price.cenovaHladina;
            di.TAXAMPIE = price.cenaDan;

            if (price.mena != null)
                di.mena_ID = price.mena;
            else
                di.Setmena_IDNull();

            if (price.menaM != null)
                di.mena_IDM = price.menaM;
            else
                di.Setmena_IDMNull();

            if (price.cenaDanM.HasValue)
                di.TAXAMPIEM = price.cenaDanM.Value;
            else
                di.SetTAXAMPIEMNull();

            if (Konfigurace_Singleton.Instance.Prodej.Cena.PriceIsWithTaxEnable)
            {
                if (!Konfigurace_Singleton.Instance.Prodej.Cena.PriceIsWithTax)
                {// ve vystupu je cena bez dane
                    di.AMOUNPIE = price.cenaBezDane;
                    if (price.cenaBezDaneM.HasValue)
                        di.AMOUNPIEM = price.cenaBezDaneM.Value;
                    else
                        di.SetAMOUNPIEMNull();
                    di.WITHTAX = 0;
                }
                else
                {// ve vystupu je cena i s dani
                    di.AMOUNPIE = price.cenaSDani;
                    if (price.cenaSDaniM.HasValue)
                        di.AMOUNPIEM = price.cenaSDaniM.Value;
                    else
                        di.SetAMOUNPIEMNull();
                    di.WITHTAX = 1;
                }
            }
            else
            {
                if (price.jeCenaSDani)
                {
                    di.AMOUNPIE = price.cenaSDani;
                    if (price.cenaSDaniM.HasValue)
                        di.AMOUNPIEM = price.cenaSDaniM.Value;
                    else
                        di.SetAMOUNPIEMNull();
                    di.WITHTAX = 1;
                }
                else
                {
                    di.AMOUNPIE = price.cenaBezDane;
                    if (price.cenaBezDaneM.HasValue)
                        di.AMOUNPIEM = price.cenaBezDaneM.Value;
                    else
                        di.SetAMOUNPIEMNull();
                    di.WITHTAX = 0;
                }
            }
        }


        #endregion

        #region TODO Zrevudovat, popripade doimplemenovat naebo smazat Pomocné metody

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


        #endregion


    }
}