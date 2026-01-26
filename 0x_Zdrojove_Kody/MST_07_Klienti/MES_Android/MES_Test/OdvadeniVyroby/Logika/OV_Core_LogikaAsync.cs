using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fask.Parsing.Codes;
using MES_Android.Classes;
using MES_Android.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.OdvadeniVyroby.Logika
{
   public class OV_Core_LogikaAsync
    {

        #region Parametry

        private OdvadeniVyroby_SberDat _parent;
        public OdvadeniVyroby_SberDat Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public DateTime? _PracovnikLoginDateTime = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _Pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _Pracovnik; }
            set
            {
                _Pracovnik = value;
                if (_Pracovnik == null)
                    _PracovnikLoginDateTime = null;
                else
                    _PracovnikLoginDateTime = DateTime.Now;
            }
        }

        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow _IDMachine = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow IDMachine
        {
            get { return _IDMachine; }
            set
            {
                _IDMachine = value;
            }
        }

        #endregion

        #region Parametry

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow Zakazka = null;

        public TIMEMODES odvodProcessedMode = TIMEMODES.Unknown;
        public TIMESTATE odvodProcessedState = TIMESTATE.Unknown;

        #endregion

        public OV_Core_LogikaAsync(
            OdvadeniVyroby_SberDat Parent, 
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow prac,
            Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow machine)
        {
            this._parent = Parent;

            Pracovnik = prac;
            IDMachine = machine;

            //Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable();
            //Pracovnik = dt.NewLoginsRow();
            //Pracovnik.firstname = Uzivatel.FIRSTNAME;
            //Pracovnik.id = Uzivatel.ID.ToString();
            //Pracovnik.psswd = Uzivatel.Pwd;
            //Pracovnik.surname = Uzivatel.SECONDNAME;

        }


        #region Enums TIMEMODES a TIMESTATE

        public enum TIMEMODES
        {
            Unknown = -1,
            Stop = 0,
            StartStop = 1,
            StartStartStop = 2
        }

        public enum TIMESTATE
        {
            Unknown = -1,
            Nezahajeno = 0,
            Korekce_Zahajena = 2,
            Korekce_Dokoncena = 3,
            Priprava_Zahajena = 4,
            Priprava_Dokoncena = 5,
            Odvod_Zahajen = 6,
            Odvod_Dokoncen = 7
        }

        #endregion

        #region PerformOK Metoda vlastne CORE cele logiky

        public async void PerformOKAsync(BaseCode code, string cisloOperace)
        {
            _parent.Scanner_STOP();
            await PerformOK(code, cisloOperace);
            _parent.Scanner_START();
        }

        public async Task PerformOK(BaseCode code, string cisloOperace)
        {
            this.odvodProcessedMode = TIMEMODES.Unknown;
            this.odvodProcessedState = TIMESTATE.Unknown;

            Fask.SQLiteDBs.DataSets.Vyroba vyrobaDS = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable();

            // Seznam produkce pro ulozeni ...
            List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> productionRowList = new List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>();

            try
            {
                //ScannerStop();

                DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.FillByBarcodeP_CZPRO_VPP(vyrobaDS.CZPRO_VPP, cisloOperace);

                // 15.1.2019 - dotazeni informace online ze serveru ... 

                MES_Android.Vyroba.ProductionState productionStateEnabled_Online = MES_Android.Vyroba.ProductionState.Unknown;
                Classes.Vyroba_Online.CheckOperation(vyrobaDS, cisloOperace, out productionStateEnabled_Online);

                var vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => false);
                if (this.Zakazka == null)
                {
                    // TODO : vyber odpovidajici/pozadovane operace ... ???

                    vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => true);
                }
                else
                {
                    vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => x.CountEntries == this.Zakazka.CountEntries && x.SOPNUMBE == this.Zakazka.SOPNUMBE);
                }

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp = null;
                //Vyrobni operace nenalezena => konec
                if (vyrobnioperace.Count() == 0)
                    throw new Exception("Výrobní operace nenalezena");
                //Nastavit vyrobni operaci(existuje max jedna s danym carovym kodem)
                else if (vyrobnioperace.Count() == 1)
                {
                    rowvpp = vyrobnioperace.First();
                }
                //else if (dtvpp.Count > 1)
                else
                {
                    // TODO : vyber vyrobniho prikazu

                    #region Android

                    Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();

                    foreach (var item in vyrobnioperace)
                    {
                        dt.ImportRow(item);
                    }

                    dt.AcceptChanges();

                    var operVPP = await GetOperace_VyberAsync(dt);

                    if (operVPP.status)
                        rowvpp = operVPP.Row;
                    else
                        return;

                    #endregion

                        #region Puvodny kod
                    //using (FormOperaceVyber fpv = new FormOperaceVyber())
                    //{
                    //    fpv.Owner = this;
                    //    fpv.Operace = vyrobnioperace.ToList();
                    //    if (DialogResult.Cancel == fpv.ShowDialog())
                    //        return;
                    //    rowvpp = fpv.OperaceVybrana;
                    //} 
                    #endregion
                }

                // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
                // 6.11.2014 -> telefonicky pozadavek JaS ???
                // 17.4.2015 -> uplne odstraneni kontroly na timestop (melo byt != misto ==)
                //if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
                //{
                if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_CasNecinnosti)
                {
                    //Posledni datum a cas akce uzivatele
                    //DateTime? userLastAction = UserLastAction();
                    DateTime? userLastAction = Data.DatabaseActions.UserLastAction(_Pracovnik.Login);
                    if (userLastAction != null && userLastAction < DateTime.Now - Konfigurace_Singleton.Instance.Vyroba.Production_UserMaxTimeSpanNoAction)
                    { // uzivatel neco delal dele nez je nastavena necinnost, => musi zadat korekci mimo vyrobu ???

                        DialogResult drNecinnost = await MessageBoxAsync.Show(_parent,
                            "Nečinnost trvala déle než " + Konfigurace_Singleton.Instance.Vyroba.Production_UserMaxTimeSpanNoAction.ToStringHHmm() + "[HH:mm]" + "\n" +
                            "Celkem " + (DateTime.Now - userLastAction.Value).ToStringHHmm() + "[HH:mm]",
                            "Pokračovat dále nebo ukončit pro zadání korekce mimo výrobu?",
                            MessageBoxButtons.OKCancel);


                        //DialogResult drNecinnost =  await MessageBoxAsync.Show(_parent,
                        //    "Nečinnost trvala déle než " + Settings.Production_UserMaxTimeSpanNoAction.ToStringHHmm() + "[HH:mm]" + "\n" +
                        //    "Celkem " + (DateTime.Now - userLastAction.Value).ToStringHHmm() + "[HH:mm]",
                        //    "Pokračovat dále nebo ukončit pro zadání korekce mimo výrobu?",
                        //     MessageBoxButtons.OKCancel,
                        //     MessageBoxIcon.Exclamation,
                        //     MessageBoxDefaultButton.Button1
                        //    );
                        if (drNecinnost == DialogResult.Cancel)
                            return;
                    }
                }

                //}

                //Nalezeni hlavicky objednavky 
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter vphta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                //vphta.Connection.ConnectionString = vppta.Connection.ConnectionString;
                //Data.VyrobaCEDataSet.CZPRO_VPHDataTable dtvph = vphta.GetDataBySOPNUMBE(rowvpp.SOPNUMBE);
                //vphta.ClearBeforeFill = false; // kvuli online dotazeni informaci ze serveru pomoci CheckOP : 17.1.2019 JiS (Kruzik)
                DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.FillBySOPNUMBE_CZPRO_VPH(vyrobaDS.CZPRO_VPH, rowvpp.SOPNUMBE);

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = vyrobaDS.CZPRO_VPH[0];

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                //Dohledani poctu odvedenych kusu z production tabulky k poslednimu datu modifikace zaznamu v tabulce VPP
                decimal? qtyodvedeno = null;
                if (!Konfigurace_Singleton.Instance.Vyroba.Vyroba_Online)
                {
                    qtyodvedeno = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.QTY_Production(rowvpp.CountEntries, rowvpp.SOPNUMBE, rowvpp.ORD, rowvpp.ITEMNMBR, rowvpp.ITEMTYPE, rowvpp.QTYPACK, rowvpp.LSTMod, rowvpp.BarcodeP);
                }

                //Pokud je pocet odvedeno vetsi nez je pozadovano, pak zobrazi informaci o tom zda pokracovat a preplnit vyrobu(mohou byt zmetky)
                //Rezijni zakazka ma nulove mnozstvi, v pripade potvrzeni 0 se jedna o rezijni zakazku nebo korekci pro opravu vyroby
                //if ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) >= rowvpp.QTYSHPPD)
                if ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) > rowvpp.QTYSHPPD) //pozadavek na zadani nuloveho mnozstvi rezijni zakazky
                {
                    //MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChimes));
                    await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundChimes));

                    if (await MessageBoxAsync.Show(_parent, "Položka již byla odvedena.\n\nChcete pokračovat?", "Odvadeni Vyroby", MessageBoxButtons.YesNo)
                        == DialogResult.No)
                        return;
                    //throw new Exception("Položka již byla odvedena");
                }

                //Dohledani posledni 2 akci production
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUserMachine = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.GetDataByUserIDMachineID_R2_Production(_Pracovnik.Login, _IDMachine.id);

                // Zjisteni posledni akce uzivatele...
                MES_Android.Vyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
                if (!Konfigurace_Singleton.Instance.Vyroba.Vyroba_Online)
                {
                    // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
                    // 6.11.2014 -> telefonicky pozadavek JaS ???
                    // 17.4.2015 -> uplne odstraneni kontroly na timestop (melo byt != misto ==)
                    //if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
                    //{
                    try
                    {
                        dsR_LastProductionUserMachine = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.ProductionLastAction(_Pracovnik.Login, _IDMachine.id, false, 1);
                    }
                    catch (Exception ews)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ews);
                    }
                    //nepovedlo se stazeni ... 
                    if (dsR_LastProductionUserMachine == null)
                    { // TODO : osetrit nejak ... 
                        if (DialogResult.No == await MessageBoxAsync.Show(_parent, "Nezdarilo se zjisteni posledniho stavu uzivatele ze serveru. Pokracovat?", "Online stav", MessageBoxButtons.YesNo))
                        {
                            return;
                        }
                    }
                    //}
                }

                Fask.SQLiteDBs.DataSets.Vyroba productionDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
                //Novy zaznam do production
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = productionDataSet.Production.NewProductionRow();
                //tato production se prida hned, protoze se uz nemeni...
                productionRowList.Add(productionRow);

                decimal pocetOdvedeno = 0;

                #region Rizeni stavu odvodu

                // casovy mod pro odvedeni operace pro dalsi akce ...
                TIMEMODES lActualTimeMode = (TIMEMODES)rowvpp.TIMEMODE;
                TIMESTATE lActualTimeState = TIMESTATE.Nezahajeno;
                // Porovnat co je novejsi a to pouzit pro rozhodnuti akce ... 
                // verifikovat povoleni vybrane nasledne akce...
                //TIMEMODES lLastTimeMode = TIMEMODES.Unknown;
                TIMESTATE lLastTimeState = TIMESTATE.Nezahajeno;
                DateTime? lLastTimeStateDateTime = null;
                bool? lLastTimeStateRemote = null;
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp = productionDataSet.Production.NewProductionRow();


                // Kontrola posledniho odvodu a aktualniho odvodu...
                // 29.1.2019 JiS -> pri online se pouziva stav odvodu dle online check ... ???
                if (!Konfigurace_Singleton.Instance.Vyroba.Vyroba_Online)
                {
                    ResolveAktualTimeState(dtL_LastProductionUserMachine, dsR_LastProductionUserMachine, ref lLastTimeState, ref lLastTimeStateDateTime, out lLastTimeStateRemote, lastProductionRowTmp);

                    //if (lastProductionRowTmp != null && !lastProductionRowTmp.IsTIMEMODENull())
                    //    lLastTimeMode = (TIMEMODES)lastProductionRowTmp.TIMEMODE;

                    // stav dle predchozi posledni akce ... 
                    if (lastProductionRowTmp != null)
                    {
                        if (!lastProductionRowTmp.IsSOPNUMBENull()
                            && !lastProductionRowTmp.IsITEMNMBRNull()
                            && lastProductionRowTmp.SOPNUMBE == rowvpp.SOPNUMBE
                            && lastProductionRowTmp.ITEMNMBR == rowvpp.ITEMNMBR
                            )
                        {
                            lActualTimeState = lLastTimeState;
                        }
                        else
                        {
                            lActualTimeState = TIMESTATE.Nezahajeno;
                        }
                    }
                    else
                    {
                        lActualTimeState = TIMESTATE.Nezahajeno;
                    }
                }
                else
                {
                    //lastProductionRowTmp = null;
                    lLastTimeStateDateTime = null;
                    lLastTimeStateRemote = null;

                    lLastTimeState = TIMESTATE.Nezahajeno;
                    lActualTimeState = TIMESTATE.Unknown;
                    switch (productionStateEnabled_Online)
                    {
                        case MES_Android.Vyroba.ProductionState.Unknown:
                            lActualTimeState = TIMESTATE.Unknown;
                            break;
                        case MES_Android.Vyroba.ProductionState.Priprava_Start:
                            lActualTimeState = TIMESTATE.Nezahajeno;
                            break;
                        case MES_Android.Vyroba.ProductionState.Priprava_Stop:
                            lActualTimeState = TIMESTATE.Priprava_Zahajena;
                            break;
                        case MES_Android.Vyroba.ProductionState.Odvod_Start:
                            if (lActualTimeMode == TIMEMODES.Stop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else if (lActualTimeMode == TIMEMODES.StartStop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else if (lActualTimeMode == TIMEMODES.StartStartStop)
                                lActualTimeState = TIMESTATE.Priprava_Zahajena;
                            break;
                        case MES_Android.Vyroba.ProductionState.Odvod_Stop:
                            if (lActualTimeMode == TIMEMODES.Stop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else
                                lActualTimeState = TIMESTATE.Odvod_Zahajen;
                            break;
                        case MES_Android.Vyroba.ProductionState.Korekce_Start:
                            lActualTimeState = TIMESTATE.Odvod_Zahajen;
                            break;
                        case MES_Android.Vyroba.ProductionState.Korekce_Stop:
                            lActualTimeState = TIMESTATE.Korekce_Zahajena;
                            break;
                        default:
                            lLastTimeState = TIMESTATE.Nezahajeno;
                            break;
                    }
                }

                // 23.9.2015 PeV - uprava, aby se stav nemenil pri odvadeni StartStartStop (timemode 2)
                // 29.8.2017 JiS - nize doplneno do vyroba_w z vyroba_p aby byla stejna funkcionalita ...
                // sledovani castecnych odvodu
                if (((!Konfigurace_Singleton.Instance.Vyroba.OdvadeniSledovatCastecneOdvody) && (lActualTimeState == TIMESTATE.Odvod_Dokoncen)) && ((lActualTimeMode == TIMEMODES.StartStop))) //|| (lActualTimeMode == TIMEMODES.StartStartStop)))
                {
                    // nesleduje se na castecne odvody
                    lActualTimeState = TIMESTATE.Nezahajeno;
                    lLastTimeState = TIMESTATE.Odvod_Dokoncen;
                }


                // zobrazit dialog/prehled o vybrane operace a prikazu
                #region zobrazit dialog/prehled o vybrane operace a prikazu
                try
                {
                    if (Konfigurace_Singleton.Instance.Vyroba.OdvadeniPrehled)
                    {
                        #region Android kod

                        var operVPP = await GetOdvadeniPrehledAsync(
                            rowvph,
                            rowvpp,
                            lActualTimeState,
                            lLastTimeState,
                            lastProductionRowTmp);

                        if (!operVPP.status)
                            return;

                        #endregion

                        #region Puvodny kod
                        //using (FormOdvadeniPrehled fop = new FormOdvadeniPrehled())
                        //{
                        //    fop._VPH = rowvph;
                        //    fop._VPP = rowvpp;
                        //    fop._TimeStateActual = lActualTimeState;
                        //    fop._TimeStateLast = lLastTimeState;
                        //    fop._LastProduction = lastProductionRowTmp;
                        //    DialogResult drFOP = fop.ShowDialog();
                        //    if (drFOP == DialogResult.Cancel)
                        //        return;
                        //} 
                        #endregion


                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion


                ////nahraje posledni stav do productionRow, ktere se nasledne predava pro zmenu/pridani hodnot stavu ... 
                //ProductionRowUpdate(productionRow, lastProductionRowTmp);

                if (lActualTimeMode == TIMEMODES.Stop)
                {
                    if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                    {
                        await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí VÝROBA\n" + "\n" +
                            "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                            "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                            "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                            , "Odvod zahájen", MessageBoxButtons.OK);
                        return;
                    }
                    else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                    {
                        await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí PŘÍPRAVA\n" + "\n" +
                            "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                           "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                           "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                            , "Odvod zahájen", MessageBoxButtons.OK);
                        return;
                    }

                    var x = await StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, psdt, code);

                    DialogResult drStop = x.dr;
                    pocetOdvedeno = x.pocetOdvedeno;

                    if (drStop == DialogResult.Cancel)
                        return;
                }
                else if (lActualTimeMode == TIMEMODES.StartStop)
                {
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni operace
                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí VÝROBA\n" + "\n" +
                                "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí PŘÍPRAVA\n" + "\n" +
                                "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK);
                            return;
                        }

                        DialogResult drStartOdvod = await StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drStartOdvod == DialogResult.Cancel)
                            return;

                        odvodProcessedMode = lActualTimeMode;
                        odvodProcessedState = TIMESTATE.Odvod_Zahajen;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Zahajen)
                    { // provest odvod operace
                        var x = await StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, psdt, code);
                        DialogResult drStopOdvod = x.dr;
                        pocetOdvedeno = x.pocetOdvedeno;

                        if (drStopOdvod == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod
                        var x = await StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, psdt, code);
                        DialogResult drStopOdvod = x.dr;
                        pocetOdvedeno = x.pocetOdvedeno;
                        if (drStopOdvod == DialogResult.Cancel)
                            return;
                    }
                    else
                    { // nedefinovany stav pro tento mod
                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Nedefinovany stav VÝROBY (Start/Stop)");
                        return;
                    }
                }
                else if (lActualTimeMode == TIMEMODES.StartStartStop)
                {
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni pripravy

                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí VÝROBA\n" + "\n" +
                                "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                 "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            await MessageBoxAsync.Show(_parent, "Nelze pokračovat.\nNebyla dokončena předchozí PŘÍPRAVA\n" + "\n" +
                                "Výr. při.: " + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                 "ID pol.: " + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                "Čár. kód: " + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK);
                            return;
                        }

                        DialogResult drPriprava = await StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drPriprava == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Priprava_Zahajena)
                    { // provest dokonceni pripravy
                        DialogResult drPriprava = await StopPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drPriprava == DialogResult.Cancel)
                            return;

                        // automaticke zahajeni vyroby ...
                        if (Konfigurace_Singleton.Instance.Vyroba.StopPripravaStartVyrobaIhned)
                        {
                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowNext = productionDataSet.Production.NewProductionRow();
                            DialogResult drOdvod = await StartOdvod(rowvph, rowvpp, productionRowNext, productionRow);
                            if (drOdvod == DialogResult.OK) //ok zahajena vyroba
                                productionRowList.Add(productionRowNext);
                            else if (drOdvod == DialogResult.Cancel)
                            { // vyroba nezahajena, ale pokracuje se ulozenim, protoze se musi ulozit ukonceni pripravy...
                            }
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Priprava_Dokoncena)
                    { // provest zahajeni odvodu nebo znovu pripravu ???
                        DialogResult drQ = await MessageBoxAsync.Show(_parent, "Zahájit VÝROBU(Ano) nebo opakovat PŘÍPRAVU(Ne)?", "Výroba/Příprava", MessageBoxButtons.YesNoCancel);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            DialogResult drOdvod = await StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                            if (drOdvod == DialogResult.Cancel)
                                return;
                        }
                        else if (drQ == DialogResult.No)
                        {
                            DialogResult drPriprava = await StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                            if (drPriprava == DialogResult.Cancel)
                                return;
                        }
                        else
                        { //nedefinovano ... 
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Zahajen)
                    { // provest odvod operace
                        var x = await StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, psdt, code);
                        DialogResult drOdvod = x.dr;
                        pocetOdvedeno = x.pocetOdvedeno;
                        if (drOdvod == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod nebo zacatek noveho odvodu nebo pripravu?
                        DialogResult drQ = await MessageBoxAsync.Show(_parent, "Pokračovat odvodem VÝROBY(Ano) nebo provést opětovné zahájení PŘÍPRAVY(Ne)?", "Výroba/Příprava", MessageBoxButtons.YesNoCancel);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            var x = await StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, psdt, code);
                            DialogResult drOdvod = x.dr;
                            pocetOdvedeno = x.pocetOdvedeno;
                            if (drOdvod == DialogResult.Cancel)
                                return;
                        }
                        else if (drQ == DialogResult.No) //Provest zahajeni?
                        {
                            DialogResult drPriprava = await StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                            if (drPriprava == DialogResult.Cancel)
                                return;
                        }
                    }
                    else
                    { // nedefinovany stav pro tento mod
                        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Nedefinovany stav vyroby (Start/Stop)");
                        return;
                    }
                }
                else
                { // jiny stav neni definovan ... 
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Nedefinovany stav vyroby...");
                    return;
                }

                #endregion


                //pridani do production

                foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow pRow in productionRowList)
                {
                    productionDataSet.Production.AddProductionRow(pRow);

                    //Online funkce pro zapis vyroby na strane partnera ... 
                    // CHECK : Pokud bude vice zaznamu a nekde uprostred to neprojde, tak co se pak bude dit ???
                    //          => vyroba a korekce v nich ...
                    var stat =await Classes.Vyroba_Online.Online_Vyroba_Zapis_Odvod(_parent, pRow);
                    if (!stat)
                        return;

                    //Ulozeni odvedenych dat do db
                    //Provedeni rozpadu na zapsani korekce a vyroby
                    if (pRow.IsTIMECRIDNull())
                    {
                        //pta.Update(pRow);
                        //provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
                        {

                            if (pRow.RowState != DataRowState.Added)
                                pRow.SetAdded(); // pokud to hodi chu tak je treba zmenit vlastnost RowState toho řadku... 


                            DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production(pRow);

                        }
                    }
                    else
                    { // ulozeni korekci ... 
                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow correctionRow = productionDataSet.Production.NewProductionRow();

                        correctionRow.CountEntries = pRow.CountEntries;
                        correctionRow.SOPNUMBE = pRow.SOPNUMBE;
                        correctionRow.ITEMNMBR = pRow.ITEMNMBR;
                        correctionRow.ITEMTYPE = pRow.ITEMTYPE;

                        correctionRow.ITEMDESC = pRow.ITEMDESC;

                        if (!pRow.IsITEMMJNull())
                            correctionRow.ITEMMJ = pRow.ITEMMJ;
                        correctionRow.ORD = pRow.ORD;
                        correctionRow.loginid = pRow.loginid;
                        correctionRow.machineid = pRow.machineid;
                        correctionRow.UserID = pRow.UserID;
                        correctionRow.TermID = pRow.TermID;
                        correctionRow.dateeve = pRow.dateeve;
                        correctionRow.QTYPACK = pRow.QTYPACK;
                        if (!pRow.IsQTYPACKMJNull())
                            correctionRow.QTYPACKMJ = pRow.QTYPACKMJ;
                        correctionRow.description = pRow.description;
                        correctionRow.BarcodeP = pRow.BarcodeP;
                        correctionRow.qty = 0;
                        correctionRow.qtyReal = 0;
                        correctionRow.GUID = Guid.NewGuid();

                        correctionRow.TIMECRID = pRow.TIMECRID;
                        if (!pRow.IsTIMECORNull())
                            correctionRow.TIMECOR = pRow.TIMECOR;
                        if (!pRow.IsTIMECORSTOPNull())
                            correctionRow.TIMECORSTOP = pRow.TIMECORSTOP;
                        if (!pRow.IsTIMECORSTARTNull())
                            correctionRow.TIMECORSTART = pRow.TIMECORSTART;

                        pRow.SetTIMECRIDNull();
                        pRow.SetTIMECORNull();
                        pRow.SetTIMECORSTARTNull();
                        pRow.SetTIMECORSTOPNull();
                        pRow.dateeve = DateTime.Now; //o par ms se posune ... ???

                        int recordsUpdated = 0;
                        productionDataSet.Production.AddProductionRow(correctionRow);
                        recordsUpdated = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production(correctionRow);

                        recordsUpdated = 0;
                        //provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
                            recordsUpdated = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production(pRow);
                    }

                    //nastaveni posledniho casu odvodu
                    Config.Settings_DB.LastProductionDateTime = productionRow.dateeve;


                    //ulozeni modifiovaneho odvodu do predlohy
                    var ctrl_vyroba_prd = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD;
                    try
                    {
                        ctrl_vyroba_prd.Connection_Open();
                        int raff = ctrl_vyroba_prd.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(pocetOdvedeno, 1, productionRow.dateeve, rowvpp.CountEntries, rowvpp.SOPNUMBE, rowvpp.ORD, rowvpp.ITEMNMBR, rowvpp.ITEMTYPE, rowvpp.QTYPACK, rowvpp.BarcodeP);
                    }
                    catch (Exception e)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(e);
                    }
                    finally
                    {
                        ctrl_vyroba_prd.Connection_Close();
                    }

                    //Aktualizace interni databaze s informaci o poslednim odvodu uzivatele
                    Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(productionRow.UserID, productionRow.dateeve);
                }

                //ulozeni dataset product sources

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter taProductionSources = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter();
                //taProductionSources.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD));

                int res = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production_Sources(psdt);

            }
            catch (Exception ex)
            {
                //MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundChyba));
                await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                //ScannerStart();
                textBoxVyrobniOperaceFocusAll();

                #region Dotaz Odeslat


                if (Konfigurace_Singleton.Instance.Vyroba.Production_Odeslat_Po_Odvedeni)
                {

                    if(Konfigurace_Singleton.Instance.Vyroba.Production_Odeslat_Po_Odvedeni_Dotaz)
                    {
                        DialogResult dr = await MessageBoxAsync.Show(_parent, "Přejete si odeslat nasnímaná data?", "Dotaz", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                            await timerUploadThreadStart();
                    }
                    else
                    {
                        await timerUploadThreadStart();
                    }
                }

                #endregion
            }
        }

        #endregion

        #region Pomocne metody

        private void textBoxVyrobniOperaceFocusAll()
        {
            try
            {
                //Maty: udelat tak aby v parent byl edittext a na zaklade tohoto se provedl focus
                _parent.textBoxVyrobniOperace.RequestFocus();
                _parent.textBoxVyrobniOperace.SelectAll();

                if(!Konfigurace_Singleton.Instance.Vyroba.Odvadeni_PamatovatBarcodeP)
                {
                    _parent.textBoxVyrobniOperace.Text = string.Empty;
                }

            }
            catch { }
        }

        /// <summary>
        /// Zjisti v jakem stavu se aktualne vyroba uzivatele nachazi ... 
        /// </summary>
        /// <param name="dtL_LastProductionUser">Lokalni zaznam produkce</param>
        /// <param name="dsR_LastProductionUser">Remote zaznam produkce</param>
        /// <param name="lAktualTimeState">Vysledny zjisteny stav v jakem se produkce uzivatele nachazi</param>
        /// <param name="lAktualTimeStateDateTime">Datum a cas posledniho zaznamu produkce uzivatele</param>
        /// <param name="isRemote">Informace, zda byly hodnoty ziskany z Remote=true nebo z Lokal=false. Pokud neni zadny zaznam, tak je null</param>
        private void ResolveAktualTimeState(
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUser,
            Vyroba.VyrobaDataSet dsR_LastProductionUser,
            ref TIMESTATE lAktualTimeState,
            ref DateTime? lAktualTimeStateDateTime,
            out bool? isRemote,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp
            )
        {
            lAktualTimeState = TIMESTATE.Nezahajeno;
            lAktualTimeStateDateTime = null;
            isRemote = null;

            // Nejprve se rozhodne dle remote, nasledne dle lokal, pokud je cas vetsi ... 
            if (dsR_LastProductionUser != null && dsR_LastProductionUser.Production.Count > 0)
            {
                Vyroba.VyrobaDataSet.ProductionRow rPRow = dsR_LastProductionUser.Production[0];
                lAktualTimeStateDateTime = rPRow.dateeve;
                isRemote = true;

                ProductionRowUpdate(lastProductionRowTmp, rPRow);

            }

            if (dtL_LastProductionUser != null && dtL_LastProductionUser.Count > 0
                && (lAktualTimeStateDateTime == null || dtL_LastProductionUser[0].dateeve > lAktualTimeStateDateTime)
                )
            {
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lPRow = dtL_LastProductionUser[0];
                lAktualTimeStateDateTime = lPRow.dateeve;
                isRemote = false;

                ProductionRowUpdate(lastProductionRowTmp, lPRow);
            }

            if (lAktualTimeStateDateTime.HasValue) //neco se vubec zadalo ... 
            {
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lr = lastProductionRowTmp; //abyto bylo kratsi ... 

                //jedna se o korekci casu 
                if (lr.IsTIMEMODENull())
                {
                    if (lr.IsTIMECORSTARTNull())
                    { //korekce casu, ktera nebyla zahajena
                        lAktualTimeState = TIMESTATE.Nezahajeno;
                    }
                    else if (!lr.IsTIMECORSTARTNull() && lr.IsTIMECORSTOPNull())
                    { // korekce casu, ktera byla dokoncena.
                        lAktualTimeState = TIMESTATE.Korekce_Zahajena;
                    }
                    else
                    { // korekce casu, ktera byla dokoncena.
                        lAktualTimeState = TIMESTATE.Korekce_Dokoncena;
                    }
                }
                else //if (!lr.IsTIMEMODENull()) //jedna se o pripravu nebo vyrobu
                {
                    if (lr.IsTIMEPREPSTARTNull()) //jde o vyrobu ...
                    {
                        if (lr.IsTIMESTARTNull() && lr.IsTIMESTOPNull())
                            lAktualTimeState = TIMESTATE.Nezahajeno;
                        else if (lr.IsTIMESTARTNull() && !lr.IsTIMESTOPNull())
                            lAktualTimeState = TIMESTATE.Odvod_Dokoncen; //toto je vec stavu STOP(0)
                        else if (!lr.IsTIMESTARTNull() && lr.IsTIMESTOPNull())
                            lAktualTimeState = TIMESTATE.Odvod_Zahajen;
                        else
                            lAktualTimeState = TIMESTATE.Odvod_Dokoncen;

                    }
                    else if (!lr.IsTIMEPREPSTARTNull()) //neco s pripravou
                    {
                        if (lr.IsTIMEPREPSTOPNull())
                            lAktualTimeState = TIMESTATE.Priprava_Zahajena;
                        else
                            lAktualTimeState = TIMESTATE.Priprava_Dokoncena;
                    }
                }
            }
        }


        private static void ProductionRowSetNulls(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp)
        {
            //lastProductionRowTmp.SetBarcodePNull();
            foreach (DataColumn item in lastProductionRowTmp.Table.Columns)
            {
                if (item.AllowDBNull)
                    lastProductionRowTmp[item] = global::System.Convert.DBNull;
            }
        }

        private void ProductionRowUpdate(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow toProductionRow, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow fromProductionRow)
        {
            if (toProductionRow == null)
                return;

            ProductionRowSetNulls(toProductionRow);

            if (fromProductionRow == null)
                return;

            DataColumnCollection toColumns = toProductionRow.Table.Columns;
            foreach (DataColumn item in fromProductionRow.Table.Columns)
            {
                if (toColumns.Contains(item.ColumnName))
                    toProductionRow[item.ColumnName] = fromProductionRow[item.ColumnName];
            }
        }

        private void ProductionRowUpdate(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow toProductionRow, Vyroba.VyrobaDataSet.ProductionRow fromProductionRow)
        {
            ProductionRowSetNulls(toProductionRow);

            DataColumnCollection dstColumns = toProductionRow.Table.Columns;
            foreach (DataColumn item in fromProductionRow.Table.Columns)
            {
                if (dstColumns.Contains(item.ColumnName))
                    toProductionRow[item.ColumnName] = fromProductionRow[item.ColumnName];
            }
        }

        // Start priprava operace
        private async Task<DialogResult> StartPriprava(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = await MessageBoxAsync.Show(_parent, "Zahájit přípravu operace " + rowvpp.ITEMDESC + "?", "Příprava Start", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMEPREPSTART = DateTime.Now;

            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();

            return DialogResult.OK;
        }

        // Stop priprava operace
        private async Task<DialogResult> StopPriprava(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = await MessageBoxAsync.Show(_parent, "Ukončit přípravu operace " + rowvpp.ITEMDESC + "?", "Přírava Stop", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMEPREPSTART = productionRowLast.IsTIMEPREPSTARTNull() ? DateTime.Now : productionRowLast.TIMEPREPSTART;
            productionRow.TIMEPREPSTOP = DateTime.Now;
            productionRow.TIMEPREP = (float)(productionRow.TIMEPREPSTOP - productionRow.TIMEPREPSTART).TotalMinutes;

            // pridani SOUBEHGUID
            if (!productionRowLast.IsSOUBEHGUIDNull())
                productionRow.SOUBEHGUID = productionRowLast.SOUBEHGUID;

            return DialogResult.OK;
        }

        private async Task<DialogResult> StartOdvod(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = await MessageBoxAsync.Show(_parent, "Zahájit operaci " + rowvpp.ITEMDESC + "?", "Výroba Start", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMESTART = DateTime.Now;

            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();

            return DialogResult.OK;
        }

        // Stop odvod operace ...
        private async Task<(DialogResult dr, decimal pocetOdvedeno)> StopOdvod(
            decimal? qtyodvedeno,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast,
            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt,
            BaseCode code
            )
        {

            byte SerNumTrack = rowvpp.SerNumT;
            //Pozadavek na potvrzeni poctu odvedenych kusu            
            decimal pocetOdvedeno = 0; //decimal pocetOdvedeno = 0;
            decimal pocetZbyva = (rowvpp.QTYSHPPD - (rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)));

            ProductionRowFill(qtyodvedeno, rowvpp, productionRow, pocetOdvedeno);

            if (!productionRowLast.IsTIMESTARTNull())
                productionRow.TIMESTART = productionRowLast.TIMESTART;
            productionRow.TIMESTOP = DateTime.Now;

            // pridani SOUBEHGUID
            if (!productionRowLast.IsSOUBEHGUIDNull())
                productionRow.SOUBEHGUID = productionRowLast.SOUBEHGUID;

            if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
            {
                productionRow.SOUBEHGUID = Guid.NewGuid();
                productionRow.TIMEPREP = (rowvpp.CNTODVEDENO > 0 || (qtyodvedeno ?? 0) > 0) ? 0 : rowvpp.TIMEPREP;
                productionRow.SetTIMESTARTNull();
            }
            else if (rowvpp.TIMEMODE == (int)TIMEMODES.StartStop)
            {
                productionRow.TIMEPREP = (rowvpp.CNTODVEDENO > 0 || (qtyodvedeno ?? 0) > 0) ? 0 : rowvpp.TIMEPREP;
            }
            else if (rowvpp.TIMEMODE == (int)TIMEMODES.StartStartStop)
            {
            }


            if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Enter && Konfigurace_Singleton.Instance.Vyroba.Production_Material_PredVyrobou)
            {


                //Rozpad materialu
                using (FormMaterial frmMaterial = new FormMaterial())
                {
                    frmMaterial.pocetodvedeno = 0;
                    frmMaterial.ProductionRow = productionRow;
                    frmMaterial.ProductionSDT = psdt;
                    frmMaterial.types = ShowTypes.StornoOK;
                    if (frmMaterial.ShowDialog() == DialogResult.Cancel)
                    {
                        return (DialogResult.Cancel, pocetOdvedeno);
                    }
                }
            }

            if (code != null)
            {
                if (code is WeightCode)
                    pocetOdvedeno = ((WeightCode)code).weight;
                else if (code is Fask.Parsing.Codes.WeightCode_12)
                    pocetOdvedeno = ((WeightCode_12)code).weight;
            }
            else
            {

                if (Konfigurace_Singleton.Instance.Vyroba.Production_Sarze_SN_Enable && SerNumTrack == 1)
                {
                    pocetOdvedeno = 1;
                }
                else
                {

                    #region Puvodny kod

                    //using (FormInputQuantity frmKod = new FormInputQuantity())
                    //{
                    //    frmKod.Text = "Potvrďte/opravte počet odvedených kusů";
                    //    if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_onlyPositive)
                    //    {
                    //        frmKod.Kod = pocetZbyva < 0 ? "0" : pocetZbyva.ToString("0.####");
                    //    }
                    //    else
                    //    {
                    //        frmKod.Kod = pocetZbyva.ToString("0.####");
                    //    }

                    //    while (true)
                    //    {
                    //        if (frmKod.ShowDialog() == DialogResult.Cancel)
                    //        {
                    //            return (DialogResult.Cancel, pocetOdvedeno);
                    //        }
                    //        //zadany pocet odvedenych kusu
                    //        pocetOdvedeno = decimal.Parse(frmKod.Kod);
                    //        //Pocet odvedenych je vetsi nez zbyva => dotaz na potvrzeni
                    //        if (pocetOdvedeno > pocetZbyva)
                    //        {
                    //            if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_NeupozornovatNaVetsiPocet)
                    //            {
                    //                //MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundDotaz));
                    //                await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundDotaz));
                    //                if (await MessageBoxAsync.Show(_parent, "Je zadán větší počet k odvedení, než je požadováno\nZbývá: " + pocetZbyva.ToString("0.00") + "\nZadáno:" + pocetOdvedeno.ToString("0.00") + "\n\nChcete pokračovat?", "Dotaz", MessageBoxButtons.YesNo)
                    //                    == DialogResult.No)
                    //                    continue;
                    //            }
                    //        }
                    //        break;
                    //    }
                    //}

                    #endregion

                    #region Android novy

                    string Kod_qty = "";
                    var Text_qty = _parent.GetString(Resource.String.InputQuantity_PredavanyText);

                    if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_onlyPositive)
                    {
                        Kod_qty = pocetZbyva < 0 ? "0" : pocetZbyva.ToString("0.####");
                    }
                    else
                    {
                        Kod_qty = pocetZbyva.ToString("0.####");
                    }

                    if (!Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_VyplnovatMnozstvi)
                    {
                        Kod_qty = string.Empty;
                    }

                    while (true)
                    {
                        var stat_lok_O = await GetFormInputQuantityAsync(Text_qty, Kod_qty);

                        if (stat_lok_O.status)
                        {
                            //zadany pocet odvedenych kusu
                            pocetOdvedeno = decimal.Parse(stat_lok_O.Kod);
                            //Pocet odvedenych je vetsi nez zbyva => dotaz na potvrzeni
                            if (pocetOdvedeno > pocetZbyva)
                            {
                                if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_NeupozornovatNaVetsiPocet)
                                {
                                    //MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundDotaz));
                                    await MySystem.Audio.PlaySoundAsync(_parent, Path.Combine(DataInfo_Static.SoundDir, DataInfo_Static.SoundDotaz));
                                    if (await MessageBoxAsync.Show(_parent, "Je zadán větší počet k odvedení, než je požadováno\nZbývá: " + pocetZbyva.ToString("0.00") + "\nZadáno:" + pocetOdvedeno.ToString("0.00") + "\n\nChcete pokračovat?", "Dotaz", MessageBoxButtons.YesNo)
                                        == DialogResult.No)
                                        continue;
                                }
                            }
                            break;
                        }
                        else
                        {
                            return (DialogResult.Cancel, pocetOdvedeno);
                        }
                    }

                    #endregion
                }
            }
            //Cursor.Current = Cursors.WaitCursor;

            ProductionRowSetQuantity(productionRow, pocetOdvedeno);
            #region Zadani vahy

            if (Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_ZadatVahu)
            {

                string Kod_vaha = "";
                var Text_vaha = _parent.GetString(Resource.String.InputWeight_PredavanyText);

                while (true)
                {
                    var stat_lok_O = await GetFormInputWeightAsync(Text_vaha, Kod_vaha);

                    if (stat_lok_O.status)
                    {
                        //zadany pocet odvedenych kusu
                        var vaha = decimal.Parse(stat_lok_O.Kod);
                        productionRow.WEIGHT = vaha;

                        break;
                    }
                    else
                    {
                        return (DialogResult.Cancel, pocetOdvedeno);
                    }
                } 
            }

            #endregion


            #region 2.9.2020 TaD , řešení šarží

            if (Konfigurace_Singleton.Instance.Vyroba.Production_Sarze_SN_Enable)
            {

                //Zde bude logika ohledně šarži a SN...
                string sn = string.Empty;

                if (SerNumTrack == 1)
                {
                    using (FormInput_Production_SN frmPSN = new FormInput_Production_SN())
                    {
                        frmPSN.ProductionRow = productionRow;
                        if (frmPSN.ShowDialog() == DialogResult.Cancel)
                        {
                            return (DialogResult.Cancel, pocetOdvedeno);
                        }

                        pocetOdvedeno = frmPSN.QTY_SN;
                        ProductionRowSetQuantity(productionRow, pocetOdvedeno);
                    }
                }
                else if (SerNumTrack == 2)
                {
                    string[] SNs = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.Generate_SarzeOnline(
                        string.Empty,
                        _Pracovnik.Login,
                        string.Empty,
                        pocetOdvedeno,
                        productionRow.ITEMNMBR
                        );

                    if (SNs == null || SNs.Count() != 1)
                    {
                        // chyba...??
                    }
                    else
                    {
                        sn = SNs[0];
                    }

                    if (string.IsNullOrEmpty(sn))
                        productionRow.SetSERLTNUMNull();
                    else
                    {
                        productionRow.SERLTNUM = sn;

                        Fask.SQLiteDBs.DataSets.Vyroba.Production_SNDataTable dtPSN = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SNDataTable();

                        var row = dtPSN.NewProduction_SNRow();

                        row.GUID_Production = productionRow.GUID;
                        row.GUID = Guid.NewGuid();
                        row.SERLNMBR = productionRow.SERLTNUM;
                        row.ITEMNMBR = productionRow.ITEMNMBR;
                        row.QTY = productionRow.qty;
                        row.SetExpiraceNull();
                        row.REZ_1 = string.Empty;
                        row.REZ_2 = string.Empty;
                        row.REZ_3 = string.Empty;
                        row.REZ_4 = string.Empty;
                        dtPSN.AddProduction_SNRow(row);

                        DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production_SN(dtPSN);

                    }
                }
            }
            else
            {
                productionRow.SetSERLTNUMNull();
            }


            #endregion



            // TODO : dialog vyberu skladu
            if (Konfigurace_Singleton.Instance.Vyroba.Production_Destination_SKLID_Enter)
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                #region Android Kod

                string SKL_tmp = string.Empty;

                if (!productionRow.IsSKL_IDNull() && !string.IsNullOrEmpty(productionRow.SKL_ID))
                {
                    var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(productionRow.SKL_ID);
                    if (listSklady.Count() > 0)
                        SKL_tmp = listSklady.First().skl_carcode;
                }
                else
                    SKL_tmp = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_SKLID;

                while (true)
                {
                    var dResExpiration = await InputBoxAsync.Show(
                        _parent,
                         Title: "Zadejte cílový sklad",
                        Message: string.Empty,
                        Defaultvalue: SKL_tmp,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Android.Text.InputTypes.ClassText
                        );

                    if (dResExpiration.Dialog_Result == DialogResult.Cancel)
                        return (DialogResult.Cancel, pocetOdvedeno);

                    // Test na existenci id cil. skladu ...
                    var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(dResExpiration.Value);
                    if (listSklady.Count() == 0)
                    {
                        if (DialogResult.Cancel == await MessageBoxAsync.Show(_parent, "Sklad '" + dResExpiration.Value + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel))
                            return (DialogResult.Cancel, pocetOdvedeno);
                    }
                    else
                    {
                        SKL_tmp = listSklady.First().skl_id.Trim(); // Neni toto opačně??
                        break;
                    }

                    break;
                }

                productionRow.SKL_ID = SKL_tmp;

                #endregion


                #region Stary Kod

                ////Zadani ciloveho skladu a cilove lokace ...
                //using (FormInputKod fik = new FormInputKod())
                //{
                //    fik.Text = "Zadejte cílový sklad";
                //    if (!productionRow.IsSKL_IDNull()
                //        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                //        )
                //    {
                //        var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(productionRow.SKL_ID);
                //        if (listSklady.Count() > 0)
                //            fik.Kod = listSklady.First().skl_carcode;
                //    }
                //    else
                //        fik.Kod = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_SKLID;

                //    while (true)
                //    {
                //        fik.Kod = fik.Kod;
                //        if (DialogResult.Cancel == fik.ShowDialog())
                //            return (DialogResult.Cancel, pocetOdvedeno);

                //        // Test na existenci id cil. skladu ...
                //        var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                //        if (listSklady.Count() == 0)
                //        {
                //            if (DialogResult.Cancel == await MessageBoxAsync.Show(_parent, "Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel))
                //                return (DialogResult.Cancel, pocetOdvedeno);
                //        }
                //        else
                //        {
                //            fik.Kod = listSklady.First().skl_id.Trim(); // Neni toto opačně??
                //            break;
                //        }
                //    }
                //    productionRow.SKL_ID = fik.Kod;
                //}

                #endregion
            }
            else
            {
                if (!String.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Destination_SKLID))
                {
                    productionRow.SKL_ID = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_SKLID;
                }
            }

            // TODO : dialog vyberu lokace, dle skladu
            if (Konfigurace_Singleton.Instance.Vyroba.Production_Destination_LOCNCODE_Enter)
            {

                #region Android kod

                string LOK_tmp = string.Empty;

                if (!productionRow.IsSKL_IDNull()
                    && !productionRow.IsLOCNCODENull()
                    && !string.IsNullOrEmpty(productionRow.SKL_ID)
                    && !string.IsNullOrEmpty(productionRow.LOCNCODE)
                    )
                {
                    var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(productionRow.SKL_ID, productionRow.LOCNCODE);
                    if (listLokace.Count() > 0)
                        LOK_tmp = listLokace.First().Barcode;
                }
                else
                    LOK_tmp = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_LOCNCODE;

                while (true)
                {
                    var dResExpiration = await InputBoxAsync.Show(
                        _parent,
                         Title: "Zadejte cíl. lokaci",
                        Message: string.Empty,
                        Defaultvalue: LOK_tmp,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Android.Text.InputTypes.ClassText
                        );

                    if (dResExpiration.Dialog_Result == DialogResult.Cancel)
                        return (DialogResult.Cancel, pocetOdvedeno);

                    // Test na existenci id cil. lokace ...
                    var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(productionRow.SKL_ID, dResExpiration.Value);
                    if (listLokace.Count() == 0)
                    {
                        if (DialogResult.Cancel == await MessageBoxAsync.Show(_parent, "Lokace '" + dResExpiration.Value + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel))
                            return (DialogResult.Cancel, pocetOdvedeno);
                    }
                    else
                    {
                        LOK_tmp = listLokace.First().LOCNCODE.Trim();
                        break;
                    }
                }

                productionRow.LOCNCODE = LOK_tmp;

                #endregion

                //zadani cilove lokace
                #region Puvodny kod
                //using (FormInputKod fik = new FormInputKod())
                //{
                //    fik.Text = "Zadejte cíl. lokaci";
                //    if (!productionRow.IsSKL_IDNull()
                //        && !productionRow.IsLOCNCODENull()
                //        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                //        && !string.IsNullOrEmpty(productionRow.LOCNCODE)
                //        )
                //    {
                //        var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(productionRow.SKL_ID, productionRow.LOCNCODE);
                //        if (listLokace.Count() > 0)
                //            fik.Kod = listLokace.First().Barcode;
                //    }
                //    else
                //        fik.Kod = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_LOCNCODE;

                //    while (true)
                //    {
                //        fik.Kod = fik.Kod;
                //        if (DialogResult.Cancel == fik.ShowDialog())
                //            return (DialogResult.Cancel, pocetOdvedeno);

                //        // Test na existenci id cil. lokace ...
                //        var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(productionRow.SKL_ID, fik.Kod);
                //        if (listLokace.Count() == 0)
                //        {
                //            if (DialogResult.Cancel == await MessageBoxAsync.Show(_parent, "Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel))
                //                return (DialogResult.Cancel, pocetOdvedeno);
                //        }
                //        else
                //        {
                //            fik.Kod = listLokace.First().LOCNCODE.Trim();
                //            break;
                //        }
                //    }

                //    productionRow.LOCNCODE = fik.Kod;
                //} 
                #endregion
            }
            else
            {
                if (!String.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Destination_LOCNCODE))
                {
                    productionRow.LOCNCODE = Konfigurace_Singleton.Instance.Vyroba.Production_Destination_LOCNCODE;
                }
            }
            //Zobrazeni doby trvani operace, doplneni korekci a potvrzeni dokonceni operace

            if (Konfigurace_Singleton.Instance.Vyroba.OdvadeniPotvrzeniOperace)
            {

                #region Android kod

                var operVPP = await GetOperacePotvrzeniAsync(
                    pocetOdvedeno,
                    this._Pracovnik,
                    this._IDMachine,
                    rowvpp,
                    productionRow,
                    psdt
                    );

                if (!operVPP.status)
                    return (DialogResult.Cancel, pocetOdvedeno);

                #endregion

                #region Puvodny kod

                //using (FormOperacePotvrzeni frmOKorekce = new FormOperacePotvrzeni())
                //{
                //    frmOKorekce.PocetOdvedeno = pocetOdvedeno;
                //    frmOKorekce.Pracovnik = this._Pracovnik;
                //    frmOKorekce.Machine = this._IDMachine;
                //    frmOKorekce.VPP = rowvpp;
                //    frmOKorekce.ProductionRow = productionRow;
                //    frmOKorekce.ProductionSDT = psdt;
                //    if (frmOKorekce.ShowDialog() == DialogResult.Cancel)
                //    {
                //        return (DialogResult.Cancel, pocetOdvedeno);
                //    }
                //} 
                #endregion
            }

            if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Enter && Konfigurace_Singleton.Instance.Vyroba.Production_Material_PoVyrobe)
            {

                //Rozpad materialu
                using (FormMaterial frmMaterial = new FormMaterial())
                {
                    frmMaterial.pocetodvedeno = pocetOdvedeno;
                    frmMaterial.ProductionRow = productionRow;
                    frmMaterial.ProductionSDT = psdt;
                    frmMaterial.types = ShowTypes.StornoOK;
                    if (frmMaterial.ShowDialog() == DialogResult.Cancel)
                    {
                        return (DialogResult.Cancel, pocetOdvedeno);
                    }
                }
            }

            #region Vyplneni hodnot pro tisk

                //konfiguracne ON/OFF - generovani SSCC
                if (Konfigurace_Singleton.Instance.Vyroba.Production_SSCC_Generovani_auto)//konfig on/off
                {
                    string sscc_generovana_hodnota = MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.vydejServis.Terminal_GetSSCCCode(Konfigurace_Singleton.Instance.Vyroba.Vyroba_SSCC_Sequence, 1, Config.Settings.TerminalID).ToString();
                //string sscc_generovana_hodnota = generatorSSCC.Get_Next_SSCC(int.Parse(Settings.MachineID), 1, 0); //  konfigurace ID stroje, vyresit LV v SSCC!!! 

                    productionRow.NMBRPAL = sscc_generovana_hodnota;
                    productionRow.SetTYPEPALNull();
                    productionRow.SetPackTypeNull();
                    productionRow.SetstatusNull();
                    
                    productionRow.SetSTORNOGUIDNull();

                if(!Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_ZadatVahu)
                    productionRow.SetWEIGHTNull();

                }
            else
                {
                    productionRow.SetNMBRPALNull();
                    productionRow.SetTYPEPALNull();
                    productionRow.SetPackTypeNull();
                    productionRow.SetstatusNull();
                    productionRow.SetSTORNOGUIDNull();

                if (!Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_ZadatVahu)
                    productionRow.SetWEIGHTNull();

            }

            #endregion


            #region Zde bude TISK

            if (Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_Etiketa_Enable)
            {
                do
                {
                    bool vytisteno = false;
                    try
                    {
                        vytisteno = await Classes.OdvedeniTisk.Print(_parent, productionRow, Fask.PrinterFactory.PrinterModules.VyrobaOdvedene, null);
                    }
                    catch (System.Exception ex)
                    {
                        DialogResult dr = await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                            continue;
                        else
                            break;
                    }

                    break;

                } while (true);
            }

            #endregion

            #region Zde bude TISK Paletovy štitek

            if (Konfigurace_Singleton.Instance.Vyroba.Production_Tisk_Paletovylistek_Enable)
            {
                do
                {
                    bool vytisteno = false;
                    try
                    {
                        vytisteno = await Classes.OdvedeniTisk.PrintPaleta(_parent, productionRow);
                    }
                    catch (System.Exception ex)
                    {
                        DialogResult dr = await MessageBoxAsync.Show(_parent, ex.Message, "Error", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                            continue;
                        else
                            break;
                    }

                    break;

                } while (true);
            }



            #endregion

            return (DialogResult.OK, pocetOdvedeno);
        }



        #endregion

        #region Production fill pomocne metody 

        private void ProductionRowSetQuantity(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow, decimal pocetOdvedeno)
        {
            productionRow.qty = pocetOdvedeno;
            productionRow.qtyReal = pocetOdvedeno;
        }

        private void ProductionRowFill(decimal? qtyodvedeno, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow, decimal pocetOdvedeno)
        {
            productionRow.CountEntries = rowvpp.CountEntries;
            productionRow.BarcodeP = rowvpp.BarcodeP;
            productionRow.dateeve = DateTime.Now;
            productionRow.description = string.Empty;
            productionRow.GUID = Guid.NewGuid();
            productionRow.ITEMNMBR = rowvpp.ITEMNMBR;
            productionRow.ITEMTYPE = rowvpp.ITEMTYPE;
            if (!rowvpp.IsITEMMJNull())
                productionRow.ITEMMJ = rowvpp.ITEMMJ;
            productionRow.loginid = Config.Settings_DB.LastProductionUserID;
            productionRow.machineid = _IDMachine.id;
            productionRow.ORD = rowvpp.ORD;
            productionRow.qty = pocetOdvedeno;
            productionRow.qtyReal = pocetOdvedeno;
            productionRow.QTYPACK = rowvpp.QTYPACK;
            if (!rowvpp.IsQTYPACKMJNull())
                productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
            productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
            productionRow.TIMEMODE = rowvpp.TIMEMODE;
            productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

            productionRow.UserID = _Pracovnik.Login;
            productionRow.TermID = Config.Settings.TerminalID;

            productionRow.ITEMDESC = rowvpp.ITEMDESC;
        }
        #endregion

        #region Start nasleduji aktivity

        private void StartAktivityNasledujici(
            Type typAktivity,
            int ResoultCode = 0,
            string Text_QTY = null,
            string Kod_QTY = null,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtVPP = null,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = null,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp = null,
            TIMESTATE? lActualTimeState = null,
            TIMESTATE? lLastTimeState = null,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp = null,
            decimal? PocetOdvedeno = null,
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik = null,
            Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine = null,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP_ROW = null,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow = null,
            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT = null,
            string Text_Weight = null,
            string Kod_Weight = null
            )
        {
            Intent intent = new Intent(_parent, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(_parent.Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            #region GetFormInputQuantityAsync

            if (Text_QTY != null)
                intent.PutExtra(DataInfo_Static.OV_QTY_Text, Text_QTY);

            if (Kod_QTY != null)
                intent.PutExtra(DataInfo_Static.OV_QTY_Kod, Kod_QTY);

            #endregion

            #region GetOperace_VyberAsync

            if (dtVPP != null)
            {
                Bundle bundleVPP = new Bundle();
                bundleVPP.PutBinder(DataInfo_Static.object_OV_dt_VPP, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable>(dtVPP));
                intent.PutExtra(DataInfo_Static.OV_dt_VPP, bundleVPP);
            }

            #endregion

            #region GetOdvadeniPrehledAsync

            if (rowvph != null)
            {
                Bundle bundle_rowvph = new Bundle();
                bundle_rowvph.PutBinder(DataInfo_Static.object_OV_row_VPH, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>(rowvph));
                intent.PutExtra(DataInfo_Static.OV_row_VPH, bundle_rowvph);
            }

            if (rowvpp != null)
            {
                Bundle bundle_rowvpp = new Bundle();
                bundle_rowvpp.PutBinder(DataInfo_Static.object_OV_row_VPP, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>(rowvpp));
                intent.PutExtra(DataInfo_Static.OV_row_VPP, bundle_rowvpp);
            }

            if (lActualTimeState.HasValue)
            {
                Bundle bundle_lActualTimeState = new Bundle();
                bundle_lActualTimeState.PutBinder(DataInfo_Static.object_OV_lActualTimeState, new WrapperForBinder<TIMESTATE>(lActualTimeState.Value));
                intent.PutExtra(DataInfo_Static.OV_lActualTimeState, bundle_lActualTimeState);
            }

            if (lLastTimeState.HasValue)
            {
                Bundle bundle_lLastTimeState = new Bundle();
                bundle_lLastTimeState.PutBinder(DataInfo_Static.object_OV_lLastTimeState, new WrapperForBinder<TIMESTATE>(lLastTimeState.Value));
                intent.PutExtra(DataInfo_Static.OV_lLastTimeState, bundle_lLastTimeState);
            }

            if (lastProductionRowTmp != null)
            {
                Bundle bundle_lastProductionRowTmp = new Bundle();
                bundle_lastProductionRowTmp.PutBinder(DataInfo_Static.object_OV_row_lastProductionRowTmp, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>(lastProductionRowTmp));
                intent.PutExtra(DataInfo_Static.OV_row_lastProductionRowTmp, bundle_lastProductionRowTmp);
            }

            #endregion

            #region GetOperacePotvrzeniAsync

            if (PocetOdvedeno.HasValue)
            {
                Bundle bundle_PocetOdvedeno = new Bundle();
                bundle_PocetOdvedeno.PutBinder(DataInfo_Static.object_OV_OP_PocetOdvedeno, new WrapperForBinder<decimal>(PocetOdvedeno.Value));
                intent.PutExtra(DataInfo_Static.OV_OP_PocetOdvedeno, bundle_PocetOdvedeno);
            }

            if (Pracovnik != null)
            {
                Bundle bundle_Pracovnik = new Bundle();
                bundle_Pracovnik.PutBinder(DataInfo_Static.object_OV_OP_Pracovnik, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow>(Pracovnik));
                intent.PutExtra(DataInfo_Static.OV_OP_Pracovnik, bundle_Pracovnik);
            }

            if (Machine != null)
            {
                Bundle bundle_Machine = new Bundle();
                bundle_Machine.PutBinder(DataInfo_Static.object_OV_OP_Machine, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>(Machine));
                intent.PutExtra(DataInfo_Static.OV_OP_Machine, bundle_Machine);
            }

            if (VPP_ROW != null)
            {
                Bundle bundle_VPP_ROW = new Bundle();
                bundle_VPP_ROW.PutBinder(DataInfo_Static.object_OV_OP_VPP_ROW, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>(VPP_ROW));
                intent.PutExtra(DataInfo_Static.OV_OP_VPP_ROW, bundle_VPP_ROW);
            }

            if (ProductionRow != null)
            {
                Bundle bundle_ProductionRow = new Bundle();
                bundle_ProductionRow.PutBinder(DataInfo_Static.object_OV_OP_ProductionRow, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>(ProductionRow));
                intent.PutExtra(DataInfo_Static.OV_OP_ProductionRow, bundle_ProductionRow);
            }

            if (ProductionSDT != null)
            {
                Bundle bundle_ProductionSDT = new Bundle();
                bundle_ProductionSDT.PutBinder(DataInfo_Static.object_OV_OP_ProductionSDT, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable>(ProductionSDT));
                intent.PutExtra(DataInfo_Static.OV_OP_ProductionSDT, bundle_ProductionSDT);
            }

            #endregion

            #region GetFormInputWeightAsync

            if (Text_Weight != null)
                intent.PutExtra(DataInfo_Static.OV_Weight_Text , Text_QTY);

            if (Kod_Weight != null)
                intent.PutExtra(DataInfo_Static.OV_Weight_Kod, Kod_QTY);

            #endregion

            _parent.StartActivityForResult(intent, ResoultCode);
        }

        #endregion

        #region Starty oken pro zadni/vyber hodnot

        private Task<O_GetInputQuantityAsync> GetFormInputQuantityAsync(string text, string Kod)
        {
            try
            {
                _parent.TCS_GetInputQuantityAsync = new TaskCompletionSource<O_GetInputQuantityAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(InputQuantity),
                        ResoultCode: _parent.ResoultCode_InputQuantityAsync,
                        Text_QTY: text,
                        Kod_QTY: Kod
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetInputQuantityAsync.SetException(ex);
            }

            return _parent.TCS_GetInputQuantityAsync.Task;
        }

        private Task<O_GetOperaceVyberAsync> GetOperace_VyberAsync(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtVPP)
        {
            try
            {
                _parent.TCS_GetOperaceVyberAsync = new TaskCompletionSource<O_GetOperaceVyberAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(OperaceVyber),
                        ResoultCode: _parent.ResoultCode_OperaceVyberAsync,
                        dtVPP: dtVPP
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetOperaceVyberAsync.SetException(ex);
            }

            return _parent.TCS_GetOperaceVyberAsync.Task;
        }


        private Task<O_GetOdvadeniPrehledAsync> GetOdvadeniPrehledAsync(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph, 
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            TIMESTATE lActualTimeState,
            TIMESTATE lLastTimeState,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp
            )
        {
            try
            {
                _parent.TCS_GetOdvadeniPrehledAsync = new TaskCompletionSource<O_GetOdvadeniPrehledAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(OdvadeniPrehled),
                        ResoultCode: _parent.ResoultCode_OdvadeniPrehledAsync,
                        rowvph: rowvph,
                        rowvpp: rowvpp,
                        lActualTimeState: lActualTimeState,
                        lLastTimeState: lLastTimeState,
                        lastProductionRowTmp: lastProductionRowTmp
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetOdvadeniPrehledAsync.SetException(ex);
            }

            return _parent.TCS_GetOdvadeniPrehledAsync.Task;
        }


        private Task<O_GetOperacePotvrzeniAsync> GetOperacePotvrzeniAsync(
            decimal PocetOdvedeno,
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik,
            Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP_ROW,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT
            )
        {

            try
            {
                _parent.TCS_GetOperacePotvrzeniAsync = new TaskCompletionSource<O_GetOperacePotvrzeniAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(OperacePotvrzeni),
                        ResoultCode: _parent.ResoultCode_OperacePotvrzeniAsync,
                        PocetOdvedeno: PocetOdvedeno,
                        Pracovnik: Pracovnik,
                        Machine: Machine,
                        VPP_ROW: VPP_ROW,
                        ProductionRow: ProductionRow,
                        ProductionSDT: ProductionSDT
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                _parent.TCS_GetOperacePotvrzeniAsync.SetException(ex);
            }

            return _parent.TCS_GetOperacePotvrzeniAsync.Task;
        }


        private Task<O_GetInputWeightAsync> GetFormInputWeightAsync(string text, string Kod)
        {
            try
            {
                _parent.TCS_GetInputWeightAsync = new TaskCompletionSource<O_GetInputWeightAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(InputWeight),
                        ResoultCode: _parent.ResoultCode_InputWeightAsync,
                        Text_Weight: text,
                        Kod_Weight: Kod
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetInputWeightAsync.SetException(ex);
            }

            return _parent.TCS_GetInputWeightAsync.Task;
        }

        #endregion

        #region Pomocne class objekty

        public abstract class O_Get_XXX_Async
        {
            public bool status;
        }

        public class O_GetInputQuantityAsync : O_Get_XXX_Async
        {
            public string Kod;
        }

        public class O_GetOperaceVyberAsync : O_Get_XXX_Async
        {
            public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow Row;
        }

        public class O_GetOdvadeniPrehledAsync : O_Get_XXX_Async
        {

        }

        public class O_GetOperacePotvrzeniAsync : O_Get_XXX_Async
        {

        }

        public class O_GetInputWeightAsync : O_Get_XXX_Async
        {
            public string Kod;
        }


        #endregion

        #region Send data

        private async Task<bool> timerUploadThreadStart()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            try
            {
                ProgressDialog_Infinity.Show(_parent);

                await Task.Delay(5000);

                var resistentFiles = Directory.GetFiles(DataInfo_Static.Production_ALL_TemplateDB);
                foreach (var resFile in resistentFiles)
                {
                    File.Delete(resFile);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart() removing resistent old Production_*.prd.tmp files");
            }

            Guid guid = Guid.NewGuid();
            var NameFile_PRD_TMP = DataInfo_Static.Production_ALL + guid.ToString("N") + DataInfo_Static.PriponaPRD + DataInfo_Static.PriponaTMP;
            var NameFile_PRD_TMP_ZIP = NameFile_PRD_TMP + DataInfo_Static.PriponaZIP;
            string PathToFile_TMP = Path.Combine(DataInfo_Static.PathDir, NameFile_PRD_TMP);
            string PathToFile_ZIP = Path.Combine(DataInfo_Static.PathDir, NameFile_PRD_TMP_ZIP);

            try
            {
                try
                {
                    File.Copy(
                        DataInfo_Static.ProductionDB,
                        PathToFile_TMP,
                        true
                        );
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart(object state):File.Copy");
                    throw ex;
                }


                //string URL = Settings.WebServiceAddressVyroba + "Upload.aspx";
                string Odkud = PathToFile_ZIP;
                string KamNaServer = Config.Settings.TerminalID + "\\" + NameFile_PRD_TMP_ZIP;


                CompressFile.CompressToZip(PathToFile_TMP, PathToFile_ZIP);

                if (DataInfo_Static.API_GO_Instance.SendFile_API(Odkud, KamNaServer) != "OK")
                {
                    await MessageBoxAsync.Show(_parent, "Nepodařilo se odeslat davku", "Error", MessageBoxButtons.OK);
                    throw new Exception("Nepodařilo se odeslat davku");
                }


                bool processed = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.ProcessProductionData2(
                    Config.Settings.TerminalID,
                    guid
                    );

                if (processed)
                {

                    //Odmazani odvedenych dat z Production                    
                    DataInfo_Static.VyrobaGO_Instance.DeleteProductionByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z Production_SN
                    DataInfo_Static.VyrobaGO_Instance.DeleteProduction_SNByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z Production_Sources
                    DataInfo_Static.VyrobaGO_Instance.DeleteProduction_SourcesByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z UserEvents
                    DataInfo_Static.VyrobaGO_Instance.DeleteUserEventsByGuid(PathToFile_TMP);
                }

                ProgressDialog_Infinity.Message = "Data odeslána: " + DateTime.Now.ToString();
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                try
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart End");
                    ProgressDialog_Infinity.Message = "Odeslání dat se nezdařilo: " + DateTime.Now.ToString() + ex.Message;
                    tcs.SetException(ex);
                }
                catch { }
            }
            finally
            {
                ProgressDialog_Infinity.Dispose();
                //Vzdy ho smazu, protoze uz tento tmp neni dulezity, vsechno se provedlo a uz k nemu nikdy nepristoupim...
                try
                {
                    File.Delete(PathToFile_TMP);
                    File.Delete(PathToFile_ZIP);
                }
                catch (Exception exDeleteFile)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exDeleteFile);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart Delete '{" + PathToFile_TMP + "}' file");
                }

                //try { _uploadInProgress = false; }
                //catch { }
                ////timerUploadStart();
                //try { this.BeginInvoke(new DelegateVoid(timerUploadStart)); }
                //catch { }
            }

            return tcs.Task.Result;

        }



        #endregion

    }
}