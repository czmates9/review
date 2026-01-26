using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using System.Data;
using Fask.Aktualizace_API.Extensions;
using System.IO;
using System.Drawing;
using System.Net;
using Fask.Aktualizace_API.Forms;
using FASK.Palety_SSCC.SQLite.Classes;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku;

namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormOdvadeni
    {

        SSCC_Generator generatorSSCC = new SSCC_Generator();


        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (this.textBoxVyrobniOperace.Text.Trim().Length == 0)
            {
                FlexibleMessageBox.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            //Materialy
            Fask.SQLiteDBs.DataSets.Vyroba psds = new Fask.SQLiteDBs.DataSets.Vyroba();

            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt = psds.Production_Sources;

            // Seznam produkce pro ulozeni ...
            List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> productionRowList = new List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ScannerStop();

                if (CheckCorrectionsOpenedNoProduction())
                {
                    return;
                }

                //Posledni datum a cas akce uzivatele z Production... 
                DateTime? userLastAction = UserLastAction();

                // 7.10. ale je nutne jeste kombinovat s akci prihlaseni uzivatele ...
                DateTime? userLastActionEvent = Fask.SQLiteDBs.DataSets.InternalState.GetInternalStateLstOperationUser(idpracovnik.id);
                // aktualizace posledni akce uzivatele
                // userlastactionevent != null => porovnat s userlastactionevent ...
                // pokud je posledni akce uzivatele v udalostech vetsi nez posledni akce v production, tak se pouzije pro test tato informace ...
                if (userLastActionEvent.HasValue && (userLastActionEvent > userLastAction))
                {
                    userLastAction = userLastActionEvent;
                }


                //Pokud je zpozdeni uzivatele, tak dojde k nastaveni start casu operace na posledni akci uzivatele, pokud existuje, jinak akutalni cas...
                DateTime? userLastActionStartTimeSet = null;
                if (Settings.NecinnostTrvalaDeleNezDotaz && userLastAction != null && userLastAction < DateTime.Now - Settings.Production_UserMaxTimeSpanNoAction)
                { // uzivatel neco delal dele nez je nastavena necinnost, => musi zadat korekci mimo vyrobu ???
                    DialogResult drNecinnost = FlexibleMessageBox.Show(this,
                        "Nečinnost trvala déle než " + Settings.Production_UserMaxTimeSpanNoAction.ToStringHHmm() + "\n" +
                        "Celkem " + (DateTime.Now - userLastAction.Value).ToStringHHmm() +
                        "\nUkončit pro zadání korekce mimo výrobu?",
                        this.Text,
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation,
                         MessageBoxDefaultButton.Button1
                        );
                    if (drNecinnost == DialogResult.Yes)
                        return;
                }


                // TODO : pridat nastaveni pro automaticky posun zacatku start operace ???
                if (userLastAction != null && userLastAction < DateTime.Now - Settings.Production_UserMaxTimeSpanNoAction)
                {
                    userLastActionStartTimeSet = userLastAction.Value.AddMilliseconds(1);
                }


                #region vyber VP kdyz je vice aktivnich a ma stejnou polozku MaR
                //Dohledani vyrobni operace podle zadaneho caroveho kodu operace
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter vppta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
                //vppta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtvpp = null;

                if (Settings.VyberZakazkyPoPrihlaseni)
                {
                    if (Globals.Zakazka == null)
                        dtvpp = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeP_CZPRO_VPP(this.textBoxVyrobniOperace.Text);
                    else
                        dtvpp = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodePandSOPNUMBEandCountEntries_CZPRO_VPP(this.textBoxVyrobniOperace.Text, Globals.Zakazka.SOPNUMBE, Globals.Zakazka.CountEntries);

                }
                else
                {
                    dtvpp = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeP_CZPRO_VPP(this.textBoxVyrobniOperace.Text);

                }



                //Vyrobni operace nenalezena => konec
                if (dtvpp.Rows.Count == 0)
                    throw new Exception("Výrobní operace nenalezena");

                //Nastavit vyrobni operaci(existuje max jedna s danym carovym kodem)
                //Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp = dtvpp[0]; //--zmena MaR 21.3.2022
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp = null;

                if (dtvpp.Rows.Count == 1)
                {
                    rowvpp = dtvpp[0];
                }
                else if(dtvpp.Rows.Count > 1)
                {
                    //vyber VP
                    //poslat promennou dtvpp do datagridu
                    //pridani formulare TODO MaR
                    using (frmVyberVP frm = new frmVyberVP(dtvpp, "Výběr VP", this.textBoxVyrobniOperace.Text))
                    {
                        frm.WindowState = FormWindowState.Maximized;
                        var dr = frm.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            if (frm.dtvpp_OUT == null || string.IsNullOrEmpty(frm.dtvpp_OUT.ITEMNMBR))
                                return;

                            //naplnit rowvpp vybranym radkem
                            rowvpp = frm.dtvpp_OUT;
                        }
                        else
                        {
                            return;
                        }
                    }

                }


                #endregion

                //Nalezeni hlavicky objednavky 
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter vphta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                //vphta.Connection.ConnectionString = vppta.Connection.ConnectionString;
                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dtvph = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByCountEntriesSopnumbe_CZPRO_VPH(rowvpp.CountEntries, rowvpp.SOPNUMBE);

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = dtvph[0];

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter ptaInternal = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //ptaInternal.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD);

                // pozor na idmachine.id != null pri nastaveni konfigurace nevyplneni cislo stroje!!
                //Zadani id stroje
#if false
                Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;
                if (!Settings.OdvadeniPozadovatZadaniStroje)
                {
                    if (!String.IsNullOrEmpty(Settings.MachineID))
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taMachines = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
                        //taMachines.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                        Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dtMachines = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Machines(Settings.MachineID);
                        if (dtMachines.Count > 0)
                            idMachine = dtMachines[0];
                        else
                            throw new Exception("Stroj s ID='" + Settings.MachineID + "' nenalezen!");
                    }
                    else
                    {
                        using (Odvadeni.FormIDMachine frmIDMachine = new Fask.Vyroba_P.Odvadeni.FormIDMachine())
                        {
                            if (frmIDMachine.ShowDialog(this) == DialogResult.Cancel)
                                return;

                            idMachine = frmIDMachine.Machine;
                        }
                    }
                } 
#endif



                //Dohledani posledni 2 akci production
                //22.11.2017 JiS - labara autonomni rezim ... InternalState.Production obsahuje vzdy posledni zaznam z tohoto stroje ...
                //Data.VyrobaCEDataSet.ProductionDataTable dtL_LastProductionUserMachine = pta.GetDataByUserIDMachineID(idpracovnik.id, idmachine.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);

                //Dohledani poctu odvedenych kusu z production tabulky k poslednimu datu modifikace zaznamu v tabulce VPP
                decimal? qtyodvedeno = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.QTY_Production(rowvpp.CountEntries, rowvpp.SOPNUMBE, rowvpp.ORD, rowvpp.ITEMNMBR, rowvpp.ITEMTYPE, rowvpp.QTYPACK, rowvpp.LSTMod, rowvpp.BarcodeP);
                //decimal? qtyodvedeno = null;
                //if (o != null && !(o is System.DBNull))
                //    qtyodvedeno = (decimal)o;
                string nazevVPP = rowvpp.ITEMDESC + Environment.NewLine + Environment.NewLine;
                //Pokud je pocet odvedeno vetsi nez je pozadovano, pak zobrazi informaci o tom zda pokracovat a preplnit vyrobu(mohou byt zmetky)
                //Rezijni zakazka ma nulove mnozstvi, v pripade potvrzeni 0 se jedna o rezijni zakazku nebo korekci pro opravu vyroby
                //if ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) >= rowvpp.QTYSHPPD)
                if (Settings.OdvadeniPolozkaJizBylaOdvedena && ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) > rowvpp.QTYSHPPD)) //pozadavek na zadani nuloveho mnozstvi rezijni zakazky
                {
                    Cursor.Current = Cursors.Default;
                    MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChimes));


                    if (FlexibleMessageBox.Show(this, nazevVPP + "Položka již byla odvedena.\n\nChcete pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                        == DialogResult.No)
                        return;
                    //throw new Exception("Položka již byla odvedena");
                    Cursor.Current = Cursors.WaitCursor;
                }

                // Zjisteni posledni akce uzivatele...
                WebServiceVyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
                try
                {
                    dsR_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ews);
                }
                //nepovedlo se stazeni ... 
                if (dsR_LastProductionUserMachine == null)
                { // TODO : osetrit nejak ... 
                    if (DialogResult.No == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění posledního stavu uživatele ze serveru.\nPokračovat?", "Online stav", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                    {
                        return;
                    }
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
                //TIMEMODES lLastTimeMode = TIMEMODES.Unknown; //???
                TIMESTATE lLastTimeState = TIMESTATE.Nezahajeno;
                DateTime? lLastTimeStateDateTime = null;
                bool? lLastTimeStateRemote = null;
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp = productionDataSet.Production.NewProductionRow();


                int pocetNaLokale = dtL_LastProductionUserMachine.Count;

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

                // 23.9.2015 PeV - uprava, aby se stav nemenil pri odvadeni StartStartStop (timemode 2)
                // sledovani castecnych odvodu
                if (((!Settings.OdvadeniSledovatCastecneOdvody) && (lActualTimeState == TIMESTATE.Odvod_Dokoncen)) && ((lActualTimeMode == TIMEMODES.StartStop))) //|| (lActualTimeMode == TIMEMODES.StartStartStop)))
                {
                    // nesleduje se na castecne odvody
                    lActualTimeState = TIMESTATE.Nezahajeno;
                    lLastTimeState = TIMESTATE.Odvod_Dokoncen;
                }

                // zobrazit dialog/prehled o vybrane operace a prikazu
                #region zobrazit dialog/prehled o vybrane operace a prikazu
                try
                {
                    if (Settings.OdvadeniPrehled)
                    {
                        using (FormOdvadeniPrehled fop = new FormOdvadeniPrehled())
                        {
                            Cursor.Current = Cursors.Default;
                            fop._VPH = rowvph;
                            fop._VPP = rowvpp;
                            fop._TimeStateActual = lActualTimeState;
                            fop._TimeStateLast = lLastTimeState;
                            fop._LastProduction = lastProductionRowTmp;
                            DialogResult drFOP = fop.ShowDialog();
                            if (drFOP == DialogResult.Cancel)
                                return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion


                ////nahraje posledni stav do productionRow, ktere se nasledne predava pro zmenu/pridani hodnot stavu ... 
                //ProductionRowUpdate(productionRow, lastProductionRowTmp);
                #region STOP mod
                if (lActualTimeMode == TIMEMODES.Stop)
                {
                    if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                    {
                        FlexibleMessageBox.Show(this,
                            "Nelze pokračovat." +
                            "\nNebyla dokončena předchozí VÝROBA " +
                            "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                            "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                            "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                            "!",
                            "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                    {
                        FlexibleMessageBox.Show(this,
                            "Nelze pokračovat." +
                            "\nNebyla dokončena předchozí PŘÍPRAVA " +
                            "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                            "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                            "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                            "!",
                            "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    DialogResult drStop = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, productionRowList, productionDataSet, psdt);
                    if (drStop == DialogResult.Cancel)
                        return;
                }
                #endregion
                #region STARTSTOP mod
                else if (lActualTimeMode == TIMEMODES.StartStop)
                {
                    // po dokonceni odvodu v rezimu START/STOP se zepta na opetovne zahajeni
                    // predchozi stav je odvod zahajen (lActualTimeState == TIMESTATE.Odvod_Dokoncen) && (lActualTimeMode == TIMEMODES.StartStop)) 
                    // predchozi stav je odvod dokoncen (lActualTimeState == TIMESTATE.Odvod_Dokoncen) && (lActualTimeMode == TIMEMODES.StartStop)) 
                    //if (Settings.OdvadeniUkonceniStartStopPovolitVlozeniStart && (lActualTimeMode == TIMEMODES.StartStop) && ((lActualTimeState == TIMESTATE.Odvod_Zahajen|| lActualTimeState == TIMESTATE.Odvod_Dokoncen))) //&& (lActualTimeState == TIMESTATE.Odvod_Dokoncen) && )

                    //{
                    //    if (DialogResult.Yes == FlexibleMessageBox.Show("Pokračovat stejnou výrobou?", "Zahájení odvádění", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                    //    {

                    //    }

                    //}
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni operace
                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            FlexibleMessageBox.Show(this,
                                "Nelze pokračovat." +
                                "\nNebyla dokončena předchozí VÝROBA " +
                                "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                                "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                                "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                                "!",
                                "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            FlexibleMessageBox.Show(this,
                                "Nelze pokračovat." +
                                "\nNebyla dokončena předchozí PŘÍPRAVA " +
                                "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                                "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                                "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                                "!",
                                "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        DialogResult drStartOdvod = StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp, userLastActionStartTimeSet);

                        if (drStartOdvod == DialogResult.Cancel)
                            return;
                        // Celkove shrnuti zadane zakazky
                        if (Settings.OdvadeniSouhrnPoStartzakazky)
                        {
                            using (FormOperaceShrnuti frmsh = new FormOperaceShrnuti())
                            {
                                frmsh.Pracovnik = this.idpracovnik;
                                frmsh.Machine = this.idmachine;
                                frmsh.VPP = rowvpp;
                                frmsh.ProductionRow = productionRow;
                                frmsh.ZbyvaKusu = rowvpp.QTYSHPPD - (rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0));

                                if (frmsh.ShowDialog() != DialogResult.OK)
                                    return;
                            }
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Zahajen)
                    { // provest odvod operace
                        DialogResult drStopOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, productionRowList, productionDataSet, psdt);
                        if (drStopOdvod == DialogResult.Cancel)
                            return;

                        if (Settings.OdvadeniUkonceniStartStopPovolitVlozeniStart)
                        {
                            if (DialogResult.Yes == FlexibleMessageBox.Show(this,
                                "Pokračovat stejnou výrobou?",
                                "Zahájení odvádění", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                            {
                                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowZahajeni = productionDataSet.Production.NewProductionRow();
                                //tato production se prida hned, protoze se uz nemeni...
                                productionRowList.Add(productionRowZahajeni);

                                StartOpetovnyOdvod(rowvph, rowvpp, productionRowZahajeni, productionRow);
                            }
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod
                        DialogResult drStopOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, productionRowList, productionDataSet, psdt);
                        if (drStopOdvod == DialogResult.Cancel)
                            return;

                        if (Settings.OdvadeniUkonceniStartStopPovolitVlozeniStart)
                        {
                            if (DialogResult.Yes == FlexibleMessageBox.Show(this,
                                "Pokračovat stejnou výrobou?",
                                "Zahájení odvádění", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                            {
                                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowZahajeni = productionDataSet.Production.NewProductionRow();
                                //tato production se prida hned, protoze se uz nemeni...
                                productionRowList.Add(productionRowZahajeni);

                                StartOpetovnyOdvod(rowvph, rowvpp, productionRowZahajeni, productionRow);
                            }
                        }
                    }
                    else
                    { // nedefinovany stav pro tento mod
                        Logging.ExceptionHandler2.Handle( Logging.LogLevel.Error ,"Nedefinovaný stav výroby (Start/Stop)");
                        return;
                    }
                }
                #endregion
                #region STARTSTARTSTOP mod
                else if (lActualTimeMode == TIMEMODES.StartStartStop)
                {
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni pripravy

                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            FlexibleMessageBox.Show(this,
                                "Nelze pokračovat." +
                                "\nNebyla dokončena předchozí VÝROBA " +
                                "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                                "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                                "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                                "!",
                                "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            FlexibleMessageBox.Show(this,
                                "Nelze pokračovat." +
                                "\nNebyla dokončena předchozí PŘÍPRAVA " +
                                "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                                "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                                "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                                "!",
                                "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        DialogResult drPriprava = StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drPriprava == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Priprava_Zahajena)
                    { // provest dokonceni pripravy
                        DialogResult drPriprava = StopPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drPriprava == DialogResult.Cancel)
                            return;

                        // TODO : doplnit do konfigurace automaticke zahajeni vyroby ...
                        if (Settings.StopPripravaStartVyrobaIhned)
                        {
                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowNext = productionDataSet.Production.NewProductionRow();
                            DialogResult drOdvod = StartOdvod(rowvph, rowvpp, productionRowNext, productionRow, userLastActionStartTimeSet);
                            if (drOdvod == DialogResult.OK) //ok zahajena vyroba
                                productionRowList.Add(productionRowNext);
                            else if (drOdvod == DialogResult.Cancel)
                            { // vyroba nezahajena, ale pokracuje se ulozenim, protoze se musi ulozit ukonceni pripravy...
                            }
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Priprava_Dokoncena)
                    { // provest zahajeni odvodu nebo znovu pripravu ???
                        DialogResult drQ = FlexibleMessageBox.Show(this,
                            "Zahájit VÝROBU(Ano) nebo opakovat PŘÍPRAVU(Ne)?",
                            "Výroba/Příprava", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            DialogResult drOdvod = StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp, userLastActionStartTimeSet);
                            if (drOdvod == DialogResult.Cancel)
                                return;
                        }
                        else if (drQ == DialogResult.No)
                        {
                            DialogResult drPriprava = StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                            if (drPriprava == DialogResult.Cancel)
                                return;
                        }
                        else
                        { //nedefinovano ... 
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Zahajen)
                    { // provest odvod operace
                        DialogResult drOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, productionRowList, productionDataSet, psdt);
                        if (drOdvod == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod nebo zacatek noveho odvodu nebo pripravu?
                        DialogResult drQ = FlexibleMessageBox.Show(this,
                            "Pokračovat odvodem VÝROBY(Ano) nebo provést opětovné zahájení PŘÍPRAVY(Ne)?",
                            "Výroba/Příprava", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            DialogResult drOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, productionRowList, productionDataSet, psdt);
                            if (drOdvod == DialogResult.Cancel)
                                return;
                        }
                        else if (drQ == DialogResult.No) //Provest zahajeni?
                        {
                            DialogResult drPriprava = StartPriprava(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                            if (drPriprava == DialogResult.Cancel)
                                return;
                        }
                    }
                    else
                    { // nedefinovany stav pro tento mod
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Nedefinovaný stav výroby (Start/Stop)");
                        return;
                    }
                }
                #endregion
                else
                { // jiny stav neni definovan ... 
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Nedefinovaný stav výroby...");
                    return;
                }

                #endregion

                Cursor.Current = Cursors.WaitCursor;

                // historie
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprohist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //taprohist.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD));
                //pridani do production

                //foreach (Data.VyrobaCEDataSet.ProductionRow pRow in productionRowList)
                foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow pRow in productionRowList.OrderBy(x => x.dateeve)) // serazeni podle data od nejmensiho ...
                {
                    productionDataSet.Production.AddProductionRow(pRow);

                    //Ulozeni odvedenych dat do db
                    //Provedeni rozpadu na zapsani korekce a vyroby
                    if (pRow.IsTIMECRIDNull())
                    {
                        //pta.Update(pRow);
                        //Settings.PovolitOdvadeniMnozstviNula
                        //provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        //if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
                        //if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()) || Settings.PovolitOdvadeniMnozstviNula)

                        // 20.9.2016 JiS => toto je divne ... 
                        // zde se uklada pouze odvod vyroby, resp stop(TIMESTOP), start/stop(TIMESTART, TIMESTOP), start/start/stop(TIMEPREPSTART, TIMEPREPSTOP, TIMESTART, TIMESTOP)
                        if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()) || Settings.PovolitOdvadeniMnozstviNula)
                        {
                            //pta.Update(pRow);
                            Data.DatabaseActions.InsertProduction(pRow);

                            // pridani do historie
                            if (Settings.ModulPovolitPrehledOdvodu)
                            {
                                pRow.SetAdded();
                                //taprohist.Update(pRow);
                                Data.DatabaseActions.InsertProductionHistory(pRow);
                            }

                            //ulozeni modifikovaneho odvodu do predlohy
                            try
                            {
                                //vzit data z pRow na tisk???

                                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Connection_Open();
                                int raff = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(pRow.qty, 1, pRow.dateeve, pRow.CountEntries, pRow.SOPNUMBE, pRow.ORD, pRow.ITEMNMBR, pRow.ITEMTYPE, pRow.QTYPACK, pRow.BarcodeP);
                                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Connection_Close();
                            }
                            catch (Exception e)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                            }

                        }

                    }
                    else
                    {
                        // 1.9.2016 JiS - odstraneno => novy zpusob zadavani korekci ...
                        #region Stary zpusob zadavani korekci => odstraneno 1.9.2016 JiS
                        //Data.VyrobaCEDataSet.ProductionRow correctionRow = productionDataSet.Production.NewProductionRow();

                        //correctionRow.CountEntries = pRow.CountEntries;
                        //correctionRow.SOPNUMBE = pRow.SOPNUMBE;
                        //correctionRow.ITEMNMBR = pRow.ITEMNMBR;
                        //correctionRow.ITEMTYPE = pRow.ITEMTYPE;
                        //if (!pRow.IsITEMMJNull())
                        //    correctionRow.ITEMMJ = pRow.ITEMMJ;
                        //correctionRow.ORD = pRow.ORD;
                        //correctionRow.loginid = pRow.loginid;
                        //correctionRow.machineid = pRow.machineid;
                        //correctionRow.UserID = pRow.UserID;
                        //correctionRow.TermID = pRow.TermID;
                        //correctionRow.dateeve = pRow.dateeve;
                        //correctionRow.QTYPACK = pRow.QTYPACK;
                        //if (!pRow.IsQTYPACKMJNull())
                        //    correctionRow.QTYPACKMJ = pRow.QTYPACKMJ;
                        //correctionRow.description = pRow.description;
                        //correctionRow.BarcodeP = pRow.BarcodeP;
                        //correctionRow.qty = 0;
                        //correctionRow.qtyReal = 0;
                        //correctionRow.GUID = Guid.NewGuid();
                        //// pridani soubeh guid
                        ////correctionRow.SOUBEHGUID = Guid.NewGuid();
                        //correctionRow.TIMECRID = pRow.TIMECRID;
                        //if (!pRow.IsTIMECORNull())
                        //    correctionRow.TIMECOR = pRow.TIMECOR;
                        //if (!pRow.IsTIMECORSTOPNull())
                        //    correctionRow.TIMECORSTOP = pRow.TIMECORSTOP;
                        //if (!pRow.IsTIMECORSTARTNull())
                        //    correctionRow.TIMECORSTART = pRow.TIMECORSTART;

                        //pRow.SetTIMECRIDNull();
                        //pRow.SetTIMECORNull();
                        //pRow.SetTIMECORSTARTNull();
                        //pRow.SetTIMECORSTOPNull();
                        //pRow.dateeve = DateTime.Now; //o par ms se posune ... ???

                        //int recordsUpdated = 0;
                        //productionDataSet.Production.AddProductionRow(correctionRow);
                        ////recordsUpdated = pta.Update(correctionRow);
                        //Data.DatabaseActions.InsertProduction(correctionRow);
                        //// pridani do historie
                        //if (Settings.ModulPovolitPrehledOdvodu)
                        //{
                        //    correctionRow.SetAdded();
                        //    //taprohist.Update(pRow);
                        //    Data.DatabaseActions.InsertProductionHistory(correctionRow);
                        //}

                        //recordsUpdated = 0;

                        ////Settings.PovolitOdvadeniMnozstviNula
                        ////provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        ////if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
                        //if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()) || Settings.PovolitOdvadeniMnozstviNula)
                        //{
                        //    //recordsUpdated = pta.Update(pRow);
                        //    Data.DatabaseActions.InsertProduction(pRow);
                        //    // pridani do historie
                        //    if (Settings.ModulPovolitPrehledOdvodu)
                        //    {
                        //        pRow.SetAdded();
                        //        //taprohist.Update(pRow);
                        //        Data.DatabaseActions.InsertProductionHistory(pRow);
                        //    }
                        //}
                        #endregion

                        // 1.9.2016 JiS - zde jsou jiz jen zaznamy s korekcemi ...
                        //recordsUpdated = pta.Update(correctionRow);
                        Data.DatabaseActions.InsertProduction(pRow);
                        // pridani do historie
                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            pRow.SetAdded();
                            //taprohist.Update(pRow);
                            Data.DatabaseActions.InsertProductionHistory(pRow);
                        }

                    }

                    //nastaveni posledniho casu odvodu
                    // ??? proc toto ??? 
                    //if (productionRow.dateeve > Settings.LastProductionDateTime)
                    //{
                    //    Settings.LastProductionDateTime = productionRow.dateeve;
                    //    //Aktualizace interni databaze s informaci o poslednim odvodu uzivatele
                    //    Data.InternalStateDataSet.UpdateInternalStateLstOperationUser(productionRow.UserID, productionRow.dateeve);
                    //}

                    // posledni cas odvodu je proste poseldni cas odvodu ...
                    Settings.LastProductionDateTime = DateTime.Now;
                    //Aktualizace interni databaze s informaci o poslednim odvodu uzivatele
                    Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(productionRow.UserID, Settings.LastProductionDateTime);

                }

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter taProductionSources = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter();
                //taProductionSources.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD));
                //taProductionSources.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf);
                //taProductionSources.Connection.Open();
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production_Sources(psdt);


                //Vycisteni vstupniho pole - nema smysl, po uspesnem odvodu se okno uzavira
                //this.textBoxVyrobniOperace.Text = string.Empty;
                //textBoxVyrobniOperaceFocusAll();

                Settings.Update();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.textBoxVyrobniOperace.SelectAll();
                this.textBoxVyrobniOperace.Focus();
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ScannerStart();
                textBoxVyrobniOperaceFocusAll();
            }

            //Odvod dokoncen => zastavit scanner a konec
            ScannerStop();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Posledni akce uzivatele z tabulky Production ...
        /// </summary>
        /// <returns></returns>
        private DateTime? UserLastAction()
        {
            DateTime? lastUserActionDateTime = null;
            DateTime? lastUserActionDateTimeLocal = null;
            DateTime? lastUserActionDateTimeServer = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserID_Production(idpracovnik.id);
                if (dt_lst_LProduction.Count > 0)
                    lastUserActionDateTimeLocal = dt_lst_LProduction[0].dateeve;

                // Zjisteni posledni akce uzivatele...
                try
                {
                    lastUserActionDateTimeServer = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.UserLastAction(idpracovnik.id);
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ews);
                }

                if (lastUserActionDateTimeLocal.HasValue)
                    lastUserActionDateTime = lastUserActionDateTimeLocal;


                if (lastUserActionDateTimeServer.HasValue)
                {
                    if (!lastUserActionDateTime.HasValue)
                        lastUserActionDateTime = lastUserActionDateTimeServer;

                    else if (lastUserActionDateTime.Value < lastUserActionDateTimeServer.Value)
                        lastUserActionDateTime = lastUserActionDateTimeServer;

                }

                return lastUserActionDateTime;

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// check if exists opened no production correction
        /// </summary>
        /// <returns>True if exists, else false</returns>
        private bool CheckCorrectionsOpenedNoProduction()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lst_LProductionRow = null;
                if (dt_lst_LProduction.Count > 0)
                    lst_LProductionRow = dt_lst_LProduction[0];

                // Zjisteni posledni akce uzivatele...
                WebServiceVyroba.VyrobaDataSet ds_lst_RProduction = null;
                WebServiceVyroba.VyrobaDataSet.ProductionRow lst_RProductionRow = null;
                try
                {
                    ds_lst_RProduction = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(idpracovnik.id, string.Empty, true, 1);
                    if (ds_lst_RProduction.Production.Count > 0)
                        lst_RProductionRow = ds_lst_RProduction.Production[0];
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ews);
                }

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow existingProductionRow = dt_lst_LProduction.NewProductionRow();

                bool? remote = null;

                if (lst_RProductionRow != null && lst_LProductionRow != null)
                { // existuje na obou a remote je vetsi
                    remote = lst_RProductionRow.dateeve >= lst_LProductionRow.dateeve;
                }
                else if (lst_RProductionRow != null)
                    remote = true;
                else if (lst_LProductionRow != null)
                    remote = false;
                else
                    remote = null;

                if (remote.HasValue)
                {
                    if (remote.Value)
                        ProductionRowUpdate(existingProductionRow, lst_RProductionRow);
                    else
                        ProductionRowUpdate(existingProductionRow, lst_LProductionRow);
                }
                else
                    existingProductionRow = null;

                if (existingProductionRow == null)
                    return false;
                else
                {
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter cta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
                    //cta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                    if (existingProductionRow.IsTIMECORSTOPNull())
                    {
                        Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable cdt = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(existingProductionRow.TIMECRID);
                        Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow crow = null;
                        if (cdt.Count > 0)
                        {
                            crow = cdt[0];
                        }
                        FlexibleMessageBox.Show(this,
                            "Existuje nedokončená korekce:" +
                            "\n" + (crow == null ? "-" : crow.desc) +
                            "\n" + (existingProductionRow.IsTIMECORSTARTNull() ? "-" : existingProductionRow.TIMECORSTART.ToString())
                            , "Korekce"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Asterisk
                            , MessageBoxDefaultButton.Button1
                            );

                        return true;
                    }
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                DialogResult drCorr = FlexibleMessageBox.Show(this, "Nepodařilo se zjistit, zda existuje nedokončená korekce mimo výrobu\nPokračovat?", "Korekce", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                if (drCorr == DialogResult.No)
                    return true;
                else
                    return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Zjisti v jakem stavu se aktualne vyroba uzivatele nachazi ... 
        /// </summary>
        /// <param name="dtL_LastProductionUser">Lokalni zaznam produkce</param>
        /// <param name="dsR_LastProductionUser">Remote zaznam produkce</param>
        /// <param name="lAktualTimeState">Vysledny zjisteny stav v jakem se produkce uzivatele nachazi</param>
        /// <param name="lAktualTimeStateDateTime">Datum a cas posledniho zaznamu produkce uzivatele</param>
        /// <param name="isRemote">Informace, zda byly hodnoty ziskany z Remote=true nebo z Lokal=false. Pokud neni zadny zaznam, tak je null</param>
        private void ResolveAktualTimeState(Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUser,
            WebServiceVyroba.VyrobaDataSet dsR_LastProductionUser,
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
                WebServiceVyroba.VyrobaDataSet.ProductionRow rPRow = dsR_LastProductionUser.Production[0];
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

        private void ProductionRowUpdate(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow toProductionRow, WebServiceVyroba.VyrobaDataSet.ProductionRow fromProductionRow)
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
        private DialogResult StartPriprava(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = FlexibleMessageBox.Show(this, "Zahájit přípravu operace " + rowvpp.ITEMDESC + "?", "Příprava Start", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Color.Yellow);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMEPREPSTART = DateTime.Now;
            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();

            return DialogResult.OK;
        }

        // Stop priprava operace
        private DialogResult StopPriprava(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = FlexibleMessageBox.Show(this, "Ukončit přípravu operace " + rowvpp.ITEMDESC + "?", "Přírava Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Color.LightBlue);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMEPREPSTART = productionRowLast.TIMEPREPSTART;
            productionRow.TIMEPREPSTOP = DateTime.Now;
            productionRow.TIMEPREP = (float)(productionRow.TIMEPREPSTOP - productionRow.TIMEPREPSTART).TotalMinutes;
            // pridani SOUBEHGUID
            if (!productionRowLast.IsSOUBEHGUIDNull())
                productionRow.SOUBEHGUID = productionRowLast.SOUBEHGUID;

            return DialogResult.OK;
        }

        private DialogResult StartOdvod(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast,
            DateTime? timestart
            )
        {
            if (Settings.ZahajeniZakazkyDotaz)
            {
                DialogResult dr = FlexibleMessageBox.Show(this, "Zahájit operaci " + rowvpp.ITEMDESC + "?", "Výroba Start", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Color.LightGreen);
                if (dr == DialogResult.Cancel)
                    return DialogResult.Cancel;
            }
            ProductionRowFill(0, rowvpp, productionRow, 0);

            // jak napravit, kdyz je cas vetsi nez minimalni delka pro korekce ???
            if (!timestart.HasValue)
                productionRow.TIMESTART = DateTime.Now;
            else
                productionRow.TIMESTART = timestart.Value;

            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();
            return DialogResult.OK;
        }

        private void StartOpetovnyOdvod(
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            ProductionRowFill(0, rowvpp, productionRow, 0);
            DateTime dt = DateTime.Now.AddMilliseconds(1);

            productionRow.dateeve = dt;
            productionRow.TIMESTART = dt;
            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();
        }


        //logika odvadeni, zaverecny bod pri kterem se vyvola tisk --- Matous
        // Stop odvod operace ...
        private DialogResult StopOdvod(
            decimal? qtyodvedeno,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast,
            out decimal pocetOdvedeno,
            List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> productionList,
            Fask.SQLiteDBs.DataSets.Vyroba productionDataset,
            Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt
            )
        {

            //Matous - data na tisk, promenna
            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow_Tisk;


           //Pozadavek na potvrzeni poctu odvedenych kusu            
           pocetOdvedeno = 0; //decimal pocetOdvedeno = 0;
            decimal pocetJizOdvedeno = (rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0));
            decimal pocetZbyva = (rowvpp.QTYSHPPD - pocetJizOdvedeno);


            Cursor.Current = Cursors.WaitCursor;

            ProductionRowFill(qtyodvedeno, rowvpp, productionRow, pocetOdvedeno);

            if (!productionRowLast.IsTIMESTARTNull())
                productionRow.TIMESTART = productionRowLast.TIMESTART;
            productionRow.TIMESTOP = DateTime.Now;

            // pridani SOUBEHGUID
            if (!productionRowLast.IsSOUBEHGUIDNull())
                productionRow.SOUBEHGUID = productionRowLast.SOUBEHGUID;

            // Test na pripravny cas ...
            if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
            {
                productionRow.SOUBEHGUID = Guid.NewGuid();
                //productionRow.TIMEPREP = (rowvpp.CNTODVEDENO > 0 || (qtyodvedeno ?? 0) > 0) ? 0 : rowvpp.TIMEPREP;
                productionRow.TIMEPREP = (rowvpp.CNTODVEDENO > 0 || (pocetJizOdvedeno > 0)) ? 0 : rowvpp.TIMEPREP;
                productionRow.SetTIMESTARTNull();
            }
            else if (rowvpp.TIMEMODE == (int)TIMEMODES.StartStop)
            {
                //productionRow.TIMEPREP = ((rowvpp.CNTODVEDENO > 0) || ((qtyodvedeno ?? 0) > 0)) ? 0 : rowvpp.TIMEPREP;
                // cntodvedeno bylo delano pro Stop operaci...
                // v ramci startStop je ale cntodvedeno vetsi jak 0 pri zahajeni... 
                // zde je dulezite, zda jiz bylo neco odvedeno ...
                productionRow.TIMEPREP = (pocetJizOdvedeno > 0) ? 0 : rowvpp.TIMEPREP;
            }
            else if (rowvpp.TIMEMODE == (int)TIMEMODES.StartStartStop)
            {
                // priprava jiz byla resena v ramci pripravy...
                productionRow.TIMEPREP = 0;
            }

            Cursor.Current = Cursors.Default;

            //SerNum_track pro 1 .. výrobní číslo
            string sarzeHodnota = string.Empty;

            using (FormInputQuantity frmKod = new FormInputQuantity(rowvph, rowvpp))
                {
                    frmKod.Text = "Potvrďte/opravte počet odvedených kusů";
                    if (Settings.PredvyplnitZbyvajiciMnozstvi)
                    {
                        if(pocetZbyva < 0)
                            frmKod.Kod = "0";
                        else
                        frmKod.Kod = pocetZbyva.ToString("0.####");
                    }
                        

                    if(rowvpp.SerNumT == 2)
                    frmKod.sarzeEnable = true;

                    while (true)
                    {
                        if (frmKod.ShowDialog(this) == DialogResult.Cancel)
                        {
                            return DialogResult.Cancel;
                        }
                        //zadany pocet odvedenych kusu
                        pocetOdvedeno = decimal.Parse(frmKod.Kod);

                    if (rowvpp.SerNumT == 2)
                        sarzeHodnota = frmKod.Sarze;

                    //Pocet odvedenych je vetsi nez zbyva => dotaz na potvrzeni
                    if (Settings.OdvadeniZadanVetsiPocetKOdvedeniDotaz && (pocetOdvedeno > pocetZbyva))
                        {
                            MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundDotaz));
                            if (FlexibleMessageBox.Show(this, rowvpp.ITEMDESC + Environment.NewLine + Environment.NewLine + "Je zadán větší počet k odvedení, než je požadováno\nZbývá: " + pocetZbyva.ToString("0.00") + "\nZadáno:" + pocetOdvedeno.ToString("0.00") + "\n\nChcete pokračovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.No)
                                continue;
                        }
                        break;
                    }
                } 
            

            ProductionRowSetQuantity(productionRow, pocetOdvedeno);

            if (rowvpp.SerNumT == 2)
                productionRow.SERLTNUM = sarzeHodnota;


            // TODO : dialog vyberu skladu
            if (Settings.Production_Destination_SKLID_Enter)
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                //Zadani ciloveho skladu a cilove lokace ...
                using (Forms.FormInputKod fik = new Forms.FormInputKod())
                {
                    fik.Text = "Zadejte cílový sklad";
                    if (!productionRow.IsSKL_IDNull()
                        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                        )
                    {
                        var listSklady = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(productionRow.SKL_ID);
                        if (listSklady.Count() > 0)
                            fik.Kod = listSklady.First().skl_carcode;
                    }
                    else
                        fik.Kod = Settings.Production_Destination_SKLID;

                    while (true)
                    {
                        fik.Kod = fik.Kod;
                        if (DialogResult.Cancel == fik.ShowDialog())
                            return DialogResult.Cancel;

                        // Test na existenci id cil. skladu ...
                        var listSklady = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                        if (listSklady.Count() == 0)
                        {
                            if (DialogResult.Cancel == MessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                return DialogResult.Cancel;
                        }
                        else
                        {
                            fik.Kod = listSklady.First().skl_id.Trim();
                            break;
                        }
                    }
                    productionRow.SKL_ID = fik.Kod; // Matous - data pro tisk
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Settings.Production_Destination_SKLID))
                {
                    productionRow.SKL_ID = Settings.Production_Destination_SKLID;
                }
            }

            // TODO : dialog vyberu lokace, dle skladu
            if (Settings.Production_Destination_LOCNCODE_Enter)
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
                //taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                //zadani cilove lokace
                using (Forms.FormInputKod fik = new Forms.FormInputKod())
                {
                    fik.Text = "Zadejte cíl. lokaci";
                    if (!productionRow.IsSKL_IDNull()
                        && !productionRow.IsLOCNCODENull()
                        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                        && !string.IsNullOrEmpty(productionRow.LOCNCODE)
                        )
                    {
                        var listLokace = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(productionRow.SKL_ID, productionRow.LOCNCODE);
                        if (listLokace.Count() > 0)
                            fik.Kod = listLokace.First().Barcode;
                    }
                    else
                        fik.Kod = Settings.Production_Destination_LOCNCODE;

                    while (true)
                    {
                        fik.Kod = fik.Kod;
                        if (DialogResult.Cancel == fik.ShowDialog())
                            return DialogResult.Cancel;

                        // Test na existenci id cil. lokace ...
                        var listLokace = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(productionRow.SKL_ID, fik.Kod);
                        if (listLokace.Count() == 0)
                        {
                            if (DialogResult.Cancel == MessageBox.Show("Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                return DialogResult.Cancel;
                        }
                        else
                        {
                            fik.Kod = listLokace.First().LOCNCODE.Trim();
                            break;
                        }
                    }

                    productionRow.LOCNCODE = fik.Kod;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Settings.Production_Destination_LOCNCODE))
                {
                    productionRow.LOCNCODE = Settings.Production_Destination_LOCNCODE;
                }
            }

            #region REZ hodnoty

            if(rowvpp.CZ_REZ1_Track > 0)
            {
                var tmp = GetRezHodnotu(1);
                if (tmp.Item2 != null)
                    return tmp.Item2.Value;
                else
                {
                    productionRow.REZ_1 = tmp.Item1;
                }
            }

            if (rowvpp.CZ_REZ2_Track > 0)
            {
                var tmp = GetRezHodnotu(2);
                if (tmp.Item2 != null)
                    return tmp.Item2.Value;
                else
                {
                    productionRow.REZ_2 = tmp.Item1;
                }
            }

            if (rowvpp.CZ_REZ3_Track > 0)
            {
                var tmp = GetRezHodnotu(3);
                if (tmp.Item2 != null)
                    return tmp.Item2.Value;
                else
                {
                    productionRow.REZ_3 = tmp.Item1;
                }
            }

            if (rowvpp.CZ_REZ4_Track > 0)
            {
                var tmp = GetRezHodnotu(4);
                if (tmp.Item2 != null)
                    return tmp.Item2.Value;
                else
                {
                    productionRow.REZ_4 = tmp.Item1;
                }
            }

            if (rowvpp.CZ_REZ5_Track > 0)
            {
                var tmp = GetRezHodnotu(5);
                if (tmp.Item2 != null)
                    return tmp.Item2.Value;
                else
                {
                    productionRow.REZ_5 = tmp.Item1;
                }
            }


            #endregion

            if (Settings.Production_Material_Enter && Settings.Production_Material_PredVyrobou)
            {
                //Rozpad materialu
                using (Materialy.FormMaterial frmMaterial = new Materialy.FormMaterial())
                {
                    frmMaterial.ProductionRow = productionRow;
                    frmMaterial.ProductionSDT = psdt;
                    frmMaterial.mnozstviVyrobku = pocetOdvedeno;

                    
                    //frmMaterial.PSDataTable = psdt;
                    //frmMaterial.VyrobaDatasetPS.Production_Sources = psdt;
                    frmMaterial.types = Materialy.ShowTypes.StornoOK;
                    if (frmMaterial.ShowDialog() == DialogResult.Cancel)
                    {
                        return DialogResult.Cancel;
                    }
                }
            }

            // 22.11.2017 - labara - kompletne potlaci zobrazovani potvrzeni operace, zadavani korekci a pripadne materialu ... ???
            if (Settings.OdvadeniPotvrzeniOperace)
            {
                //1.9.2016 JiS - uprava:
                // 1) zadavani vice korekci
                // 2) kontrola na celkovy cas dle jednotkoveho(normovaneho) casu
                using (FormOperacePotvrzeni2 frmOKorekce = new FormOperacePotvrzeni2())
                {
                    frmOKorekce.Pracovnik = this.idpracovnik;
                    frmOKorekce.Machine = this.idmachine;
                    frmOKorekce.VPP = rowvpp;
                    frmOKorekce.ProductionRow = productionRow;
                    frmOKorekce.ProductionSDT = psdt;
                    //frmOKorekce.ZbyvaKusu = rowvpp.QTYSHPPD - (rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0));
                    frmOKorekce.ZbyvaKusu = pocetZbyva;

                    if (frmOKorekce.ShowDialog(this) == DialogResult.Cancel)
                    {
                        return DialogResult.Cancel;
                    }

                    // TODO : ulozit korekce do vystupu k ulozeni ...
                    //frmOKorekce.Corrections 
                    List<Korekce.Korekce> korekce = frmOKorekce.Korekce;
                    if (korekce != null && korekce.Count > 0)
                    {
                        // pridani korekci do seznamu ... 
                        foreach (var itemKorekce in korekce.OrderBy(x => x.dateeve))
                        {
                            var productionCorrection = productionDataset.Production.NewProductionRow();
                            productionCorrection.ItemArray = productionRow.ItemArray;

                            productionCorrection.qty = 0;
                            productionCorrection.qtyReal = 0;
                            productionCorrection.GUID = Guid.NewGuid();

                            // pridani soubeh guid
                            //productionCorrection.SOUBEHGUID = Guid.NewGuid();
                            productionCorrection.SetSOUBEHGUIDNull(); //??? melo by byt soubeh stejne jako production ...???
                            productionCorrection.SetTIMEPREPNull();
                            productionCorrection.SetTIMEPREPSTARTNull();
                            productionCorrection.SetTIMEPREPSTOPNull();
                            productionCorrection.SetTIMESTARTNull();
                            productionCorrection.SetTIMESTOPNull();
                            productionCorrection.SetTIMEUNITNull();

                            productionCorrection.CORRGUID = Guid.NewGuid();
                            productionCorrection.TIMECRID = itemKorekce.id;
                            productionCorrection.TIMECOR = Convert.ToSingle(itemKorekce.delka.TotalMinutes);
                            productionCorrection.TIMECORSTOP = itemKorekce.stop;
                            productionCorrection.TIMECORSTART = itemKorekce.start;
                            if (itemKorekce.type != null)
                                productionCorrection.TIMECRIDTYPE = Convert.ToByte(itemKorekce.type);

                            //productionCorrection.dateeve = DateTime.Now; //posun o par ms
                            productionCorrection.dateeve = itemKorekce.dateeve; //nastavi se cas vytvoreni korekce...
                            productionCorrection.description = itemKorekce.note;

                            //productionList.Add(productionCorrection);
                            productionList.Insert(0, productionCorrection); // vlozi se na 1.pozici(pred konec odvodu...)
                        }
                    }
                    productionRow.dateeve = DateTime.Now; //posun o par ms

                }

            }


            if (Settings.Production_Material_Enter && Settings.Production_Material_PoVyrobe)
            {

                //Rozpad materialu
                using (Materialy.FormMaterial frmMaterial = new Materialy.FormMaterial())
                {
                    frmMaterial.ProductionRow = productionRow;
                    frmMaterial.ProductionSDT = psdt;
                    frmMaterial.mnozstviVyrobku = pocetOdvedeno;
                    frmMaterial.types = Materialy.ShowTypes.StornoOK;
                    if (frmMaterial.ShowDialog() == DialogResult.Cancel)
                    {
                        return DialogResult.Cancel;
                    }
                }
            }

            //TODO MaR - doplnit logiku tisku, dat debug a overit data... MaR
            if(true)// productionRow_Tisk = productionRow;
            {
                //konfiguracne ON/OFF - generovani SSCC
                if (Settings.SSCC_Generovani_auto)//konfig on/off
                {
                    string sscc_generovana_hodnota = generatorSSCC.Get_Next_SSCC(int.Parse(Settings.MachineID), 1, 0); //  konfigurace ID stroje, vyresit LV v SSCC!!! 

                    productionRow.NMBRPAL = sscc_generovana_hodnota;
                    productionRow.SetTYPEPALNull();
                    productionRow.SetPackTypeNull();
                    productionRow.SetstatusNull();
                    productionRow.SetWEIGHTNull();
                    productionRow.SetSTORNOGUIDNull();
                }
                else
                {
                    productionRow.SetNMBRPALNull();
                    productionRow.SetTYPEPALNull();
                    productionRow.SetPackTypeNull();
                    productionRow.SetstatusNull();
                    productionRow.SetWEIGHTNull();
                    productionRow.SetSTORNOGUIDNull();
                }

                Classes.PrintPaleta pr = new Classes.PrintPaleta();
                pr.PerformPaletaTisk(productionRow); //metoda pro tisk

            }

            return DialogResult.OK;
        }

        private (string,DialogResult?) GetRezHodnotu(int v)
        {

            string txt = string.Empty;

            switch (v)
            {
                case 1:
                    txt = Settings.Odvadeni_REZ_1_Lokalizace;
                    break;
                case 2:
                    txt = Settings.Odvadeni_REZ_2_Lokalizace;
                    break;
                case 3:
                    txt = Settings.Odvadeni_REZ_3_Lokalizace;
                    break;
                case 4:
                    txt = Settings.Odvadeni_REZ_4_Lokalizace;
                    break;
                case 5:
                    txt = Settings.Odvadeni_REZ_5_Lokalizace;
                    break;
                default:
                    txt = string.Format("Zadejte REZ{0} hodnotu",v);
                    break;
            }

            using (Forms.FormInputKod fik = new Forms.FormInputKod())
            {
                fik.Text = txt;

                while (true)
                {
                    fik.Kod = fik.Kod;
                    if (DialogResult.Cancel == fik.ShowDialog())
                        return (null, DialogResult.Cancel);
                    else if (string.IsNullOrEmpty(fik.Kod))
                        continue;
                    else
                        break;
                }

                return (fik.Kod,null);
            }
        }

        //#region Tisk stitku
        //protected void PerformPaletaTisk(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow_data)
        //{
        //    try
        //    {
        //        if (!Settings.TiskPalety)
        //            return;

        //        while (true)
        //        {
        //            try
        //            {
        //                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt = new SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
        //                Dictionary<string, string> data = new Dictionary<string, string>();

        //                //skladani caroveho kodu GS1 --START---------------------
        //                //zadat key
        //                //zadat value

        //                string GS1_KOD_1_1D = string.Empty;
        //                string GS1_KOD_1_TX = string.Empty;
        //                string GS1_KOD_2_1D = string.Empty;
        //                string GS1_KOD_2_TX = string.Empty;
        //                string SSCC = string.Empty;
        //                string SSCC_bez_nul = string.Empty;
        //                string WEIGHT = string.Empty;






        //                if (productionRow_data != null)
        //                {
        //                    //------------START-DATA----------------
        //                    //dotahovat data SSCC a WEIGHT

        //                    if (!productionRow_data.IsWEIGHTNull())
        //                    {
        //                        WEIGHT = productionRow_data.WEIGHT.ToString();
        //                    }

        //                    if (!data.ContainsKey("WEIGHT"))
        //                        data.Add("WEIGHT", WEIGHT);

        //                    if (!productionRow_data.IsNMBRPALNull())
        //                    {
        //                        //SSCC a WEIGHT doplnit logiku dohledani
        //                        //SSCC a WEIGHT natvrdo zadano
        //                        SSCC = productionRow_data.NMBRPAL;
        //                        SSCC_bez_nul = SSCC.Substring(2);
        //                    }

        //                    if (!data.ContainsKey("SSCC"))
        //                        data.Add("SSCC", SSCC_bez_nul);

        //                    //------------END-DATA----------------

        //                    GS1_KOD_1_1D = "02" + productionRow_data.BarcodeP.PadLeft(14,'0') + "37" + productionRow_data.qty.ToString("0000") + "";
        //                    GS1_KOD_1_TX = "(02)" + productionRow_data.BarcodeP.PadLeft(14, '0') + "(37)" + productionRow_data.qty.ToString("0000") + ""; //(02) (37)
        //                    GS1_KOD_2_1D = SSCC; //SSCC neni v production
        //                    GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
        //                }




        //                if (!data.ContainsKey("GS1_KOD_1_1D"))
        //                    data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

        //                if (!data.ContainsKey("GS1_KOD_1_TX"))
        //                    data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

        //                if (!data.ContainsKey("GS1_KOD_2_1D"))
        //                    data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

        //                if (!data.ContainsKey("GS1_KOD_2_TX"))
        //                    data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

        //                //skladani caroveho kodu GS1 --END---------------------


        //                foreach (DataColumn dcol in dt.Columns)
        //                {
        //                    string key = dcol.ColumnName;
        //                    string value = productionRow_data[dcol.ColumnName].ToString();
        //                    if (!data.ContainsKey(key))
        //                        data.Add(key, value);
        //                }

        //                //TODO : nejake pocty dat a dalsi podrobnosti o vydejovych datech ???

        //                //bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPalListek);
        //                bool vytisteno = PrintSendToPrinterFactory(data, Settings.TiskNazevSablony, null);
        //               // Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", listPolozekVydej.ListPolozek[0].CountEntries, listPolozekVydej.ListPolozek[0].SOPNUMBE, null, vytisteno.ToString(), null));

        //                //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3TiskPaletovehoListkuDokoncen, sopnumber.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        //                return;
        //            }
        //            catch (Exception ex)
        //            {
        //               // Logging.Log.Write(ex);
        //                //DialogResult dr = MessageBoxBig.Show(ex.Message + "\n\n" + Fask.Localization.Localization.Vydej3ListPolozek3OpakovatTiskDokladu + "'" + sopnumber.Trim() + "'?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
        //                //if (dr == DialogResult.No)
        //                //    return;
        //            }
        //        }


        //    }
        //    catch ( Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        ////public static bool Print(Dictionary<string, string> data, string templatename)
        ////{
        ////    try
        ////    {
        ////        //return Print(printParams, printData, templatename, 1);
        ////        return PrintSendToPrinterFactory(data, templatename, null);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //Logging.Log.Write(ex);
        ////        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        ////        return false;
        ////    }
        ////    finally
        ////    {
        ////    }
        ////}


        //private  bool PrintSendToPrinterFactory(
        //    Dictionary<string, string> printData,
        //    string templateName,
        //    int? pocetVytisku
        //    )
        //{
        //    bool printed = false;

        //    try
        //    {
        //        string pocetVytiskuStr = pocetVytisku.ToString();

        //        do
        //        {
        //            if (!Settings.TiskMnozstviJednaAutomaticky)
        //            {
        //                if (string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit))
        //                {
        //                    //TODO pridat novy form s dotazem na počet vytisku
        //                    using (FormInputTiskMnozstvi frm = new FormInputTiskMnozstvi())
        //                    {
        //                        //frm.WindowState = FormWindowState.Maximized;
        //                        frm.WindowState = FormWindowState.Normal;

        //                        DialogResult dr = frm.ShowDialog();
        //                        if (dr == DialogResult.OK)
        //                        {
        //                            pocetVytiskuStr = frm.Kod;

        //                            //return true;
        //                        }
        //                        else if (dr == DialogResult.Cancel)
        //                            return false;
        //                    }
        //                }
        //                else if (!string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit) && int.Parse(Settings.TiskMnozstviPredvyplnit) <= 0)
        //                {
        //                    //TODO pridat novy form s dotazem na počet vytisku
        //                    using (FormInputTiskMnozstvi frm = new FormInputTiskMnozstvi())
        //                    {
        //                        //frm.WindowState = FormWindowState.Maximized;
        //                        frm.WindowState = FormWindowState.Normal;

        //                        DialogResult dr = frm.ShowDialog();
        //                        if (dr == DialogResult.OK)
        //                        {
        //                            pocetVytiskuStr = frm.Kod;

        //                            //return true;
        //                        }
        //                        else if (dr == DialogResult.Cancel)
        //                            return false;
        //                    }
        //                }
        //                else if(!string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit) && int.Parse(Settings.TiskMnozstviPredvyplnit) > 0)
        //                {
        //                    pocetVytiskuStr = Settings.TiskMnozstviPredvyplnit;
        //                }
        //            }
        //            else if(Settings.TiskMnozstviJednaAutomaticky)
        //            {
        //                pocetVytiskuStr = "1";
        //            }


        //            //DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        //            //if (dr == DialogResult.Cancel)
        //            //    return false;

        //            try
        //            {
        //                pocetVytisku = int.Parse(pocetVytiskuStr);
        //            }
        //            catch (Exception ex)
        //            {
        //                FlexibleMessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);


        //                continue;
        //            }

        //            if (pocetVytisku <= 0)
        //            {
        //                FlexibleMessageBox.Show("Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                continue;
        //            }
        //            if (pocetVytisku > 100)
        //            {
        //                FlexibleMessageBox.Show("Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                continue;
        //            }

        //            //pokud az sem, tak ok ... pustit do tisku
        //            break;
        //        } while (true);

        //        Cursor.Current = Cursors.WaitCursor;

        //        printed = Print(printData, templateName, pocetVytisku ?? 1);

        //        return printed;
        //    }
        //    catch (WebException webex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        Logging.ExceptionHandler2.Handle(webex);
        //        FlexibleMessageBox.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return printed;
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        Logging.ExceptionHandler2.Handle(ex);
        //        FlexibleMessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return printed;
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        //private bool Print(Dictionary<string, string> printData, string templateName, int pocet)
        //{

        //    if (!Settings.Tisk_OneWayPrint)
        //        return Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.tiskServis.Etiketa(0, templateName, prepareTiskParams(), prepareTiskValues(printData), pocet);
        //    else
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.tiskServis.EtiketaBezNavratu(0, templateName, prepareTiskParams(), prepareTiskValues(printData), pocet);

        //    return true;
        //}

        //private WebServiceTisk.TiskParams prepareTiskParams()
        //{
        //    WebServiceTisk.TiskParams tiskParams = new WebServiceTisk.TiskParams();
        //    tiskParams.CONFIG_NAME = Settings.TiskNazevTiskarny; // to je vse ???
        //    return tiskParams;
        //}

        //private WebServiceTisk.DSValues prepareTiskValues(Dictionary<string, string> data)
        //{
        //    WebServiceTisk.DSValues tiskValues = new WebServiceTisk.DSValues();
        //    tiskValues.Values.BeginLoadData();
        //    foreach (var item in data)
        //    {
        //        tiskValues.Values.AddValuesRow(item.Key, item.Value);
        //    }
        //    tiskValues.Values.EndLoadData();
        //    tiskValues.AcceptChanges();
        //    return tiskValues;
        //}

        //#endregion

        private void ProductionRowSetQuantity(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow, decimal pocetOdvedeno)
        {
            productionRow.qty = pocetOdvedeno;
            productionRow.qtyReal = pocetOdvedeno;
        }

        private void ProductionRowSetQuantitySarze(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow, decimal pocetOdvedeno,int sarze)
        {
            productionRow.qty = pocetOdvedeno;
            productionRow.qtyReal = pocetOdvedeno;
            productionRow.SERLTNUM = sarze.ToString();
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
            productionRow.loginid = Settings.LastProductionUserID;
            productionRow.machineid = idmachine.id;
            productionRow.ORD = rowvpp.ORD;
            productionRow.qty = pocetOdvedeno;
            productionRow.qtyReal = pocetOdvedeno;
            productionRow.QTYPACK = rowvpp.QTYPACK;
            if (!rowvpp.IsQTYPACKMJNull())
                productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
            productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
            productionRow.TIMEMODE = rowvpp.TIMEMODE;
            productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

            productionRow.UserID = idpracovnik.id;
            productionRow.TermID = Settings.TerminalID;

            //4.6.2021 TaD pridal
            if (rowvpp.IsITEMDESCNull())
                productionRow.SetITEMDESCNull();
            else
                productionRow.ITEMDESC = string.IsNullOrEmpty(rowvpp.ITEMDESC) ? string.Empty : rowvpp.ITEMDESC.Trim();

        }

        public void DeleteOperace()
        {
            #region kontrola stavu
            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);

                // Zjisteni posledni akce uzivatele...
                WebServiceVyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
                try
                {
                    dsR_LastProductionUserMachine = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
                }
                catch (Exception ews)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ews);
                }
                //nepovedlo se stazeni ... 
                if (dsR_LastProductionUserMachine == null)
                { // TODO : osetrit nejak ... 
                    if (DialogResult.No == FlexibleMessageBox.Show(this, "Nezdařilo se zjisteni posledniho stavu uzivatele ze serveru. Pokracovat?", "Online stav", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                    { return; }
                }

                TIMESTATE lLastTimeState = TIMESTATE.Nezahajeno;
                DateTime? lLastTimeStateDateTime = null;
                bool? lLastTimeStateRemote = null;

                Fask.SQLiteDBs.DataSets.Vyroba productionDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lastProductionRowTmp = productionDataSet.Production.NewProductionRow();

                ResolveAktualTimeState(
                    dtL_LastProductionUserMachine,
                    dsR_LastProductionUserMachine,
                    ref lLastTimeState,
                    ref lLastTimeStateDateTime,
                    out lLastTimeStateRemote,
                    lastProductionRowTmp
                    );

                if (lLastTimeStateRemote == null)
                {
                    FlexibleMessageBox.Show(this, "Nebyl nalezen záznam.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    return;
                }

                switch (lLastTimeState)
                {
                    case TIMESTATE.Unknown:
                        FlexibleMessageBox.Show(this, "Neznámy stav.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        return;
                    case TIMESTATE.Nezahajeno:
                        FlexibleMessageBox.Show(this, "Nelze zrušit, nebylo nic zahájeno.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        return;
                    case TIMESTATE.Korekce_Zahajena:
                        break;
                    case TIMESTATE.Korekce_Dokoncena:
                        break;
                    case TIMESTATE.Priprava_Zahajena:
                        if (FlexibleMessageBox.Show(this, "Zrušit přípravu?\n" +
                            " Číslo zaznamu:" +
                            "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                            "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                            "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                            "!",
                             this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        { return; }
                        DeletePriprava(lLastTimeStateRemote, lLastTimeStateDateTime, lastProductionRowTmp);
                        break;
                    case TIMESTATE.Priprava_Dokoncena:
                        FlexibleMessageBox.Show(this, "Nelze zrušit, příprava byla dokončena.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        return;
                    case TIMESTATE.Odvod_Zahajen:
                        if (FlexibleMessageBox.Show(this, "Zrušit odvod?\n" +
                            " Číslo záznamu:" +
                            "\n'" + (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) +
                            "\n:" + (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) +
                            "\n>" + (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP) +
                            "!",
                            this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        { return; }
                        DeleteOdvod(lLastTimeStateRemote, lLastTimeStateDateTime, lastProductionRowTmp);
                        break;
                    case TIMESTATE.Odvod_Dokoncen:
                        FlexibleMessageBox.Show(this, "Nelze zrušit, odvod byl dokončen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        return;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

            }

            #endregion
        }





        private void DeletePriprava(bool? remote, DateTime? lLastTimeStateDateTime, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow PRow)
        {
            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnik;

                using (Odvadeni.FormHeslo frmHeslo = new Fask.Aktualizace_API.Odvadeni.FormHeslo())
                {
                    if (frmHeslo.ShowDialog(this) == DialogResult.Cancel)
                        return;

                    idPracovnik = frmHeslo.Pracovnik;
                }

                //if (idPracovnik.VS == 0)
                //{
                //    FlexibleMessageBox.Show(this, "Uživatel nemá pravo editace.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                //    return;
                //}

                Fask.SQLiteDBs.DataSets.Vyroba dsl = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow row = dsl.Production.NewProductionRow();

                if (remote == false)
                {

                    Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable Production = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();

                    //pta.FillByBarcodePUserIDdateeve(Production, cislooperace, idPracovnik.id, (DateTime)userLastAction);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.FillByBarcodePUserIDdateeve_Production(Production, PRow.BarcodeP, idPracovnik.id, (DateTime)lLastTimeStateDateTime);

                    #region naplneni radku datasetu + kontroly na null

                    if (Production[0].IsCountEntriesNull())
                        row.SetCountEntriesNull();
                    else
                        row.CountEntries = Production[0].CountEntries;

                    if (Production[0].IsSOPNUMBENull())
                        row.SetSOPNUMBENull();
                    else
                        row.SOPNUMBE = Production[0].SOPNUMBE;

                    if (Production[0].IsITEMNMBRNull())
                        row.SetITEMNMBRNull();
                    else
                        row.ITEMNMBR = Production[0].ITEMNMBR;

                    if (Production[0].IsITEMTYPENull())
                        row.SetITEMTYPENull();
                    else
                        row.ITEMTYPE = Production[0].ITEMTYPE;

                    if (Production[0].IsITEMMJNull())
                        row.SetITEMMJNull();
                    else
                        row.ITEMMJ = Production[0].ITEMMJ;

                    if (Production[0].IsORDNull())
                        row.SetORDNull();
                    else
                        row.ORD = Production[0].ORD;

                    //if (ds.Production[0].IsTIMEPREPNull())
                    //    row.SetTIMEPREPNull();
                    //else
                    row.TIMEPREP = 0;

                    if (Production[0].IsTIMEUNITNull())
                        row.SetTIMEUNITNull();
                    else
                        row.TIMEUNIT = Production[0].TIMEUNIT;

                    if (Production[0].IsTIMESTARTNull())
                        row.SetTIMESTARTNull();
                    else
                        row.TIMESTART = Production[0].TIMESTART;

                    if (Production[0].IsTIMESTOPNull())
                        row.SetTIMESTOPNull();
                    else
                        row.TIMESTOP = Production[0].TIMESTOP;

                    if (Production[0].IsTIMECORNull())
                        row.SetTIMECORNull();
                    else
                        row.TIMECOR = Production[0].TIMECOR;

                    if (Production[0].IsTIMECRIDNull())
                        row.SetTIMECRIDNull();
                    else
                        row.TIMECRID = Production[0].TIMECRID;

                    row.loginid = Production[0].loginid;

                    if (Production[0].IsmachineidNull())
                        row.IsmachineidNull();
                    else
                        row.machineid = Production[0].machineid;

                    row.dateeve = DateTime.Now;
                    row.qty = Production[0].qty;
                    row.qtyReal = Production[0].qtyReal;

                    if (Production[0].IsQTYPACKNull())
                        row.IsQTYPACKNull();
                    else
                        row.QTYPACK = Production[0].QTYPACK;

                    if (Production[0].IsQTYPACKMJNull())
                        row.IsQTYPACKMJNull();
                    else
                        row.QTYPACKMJ = Production[0].QTYPACKMJ;

                    //if (Production[0].IsdescriptionNull())
                        //row.IsdescriptionNull();
                    //else
                    row.description = "Příprava byla zrušena:" + idPracovnik.firstname + " " + idPracovnik.surname + "(" + idPracovnik.id + ")";

                    if (Production[0].IsBarcodePNull())
                        row.IsBarcodePNull();
                    else
                        row.BarcodeP = Production[0].BarcodeP;

                    row.UserID = Production[0].UserID;
                    row.TermID = Production[0].TermID;

                    if (Production[0].IsISOKNull())
                        row.SetISOKNull();
                    else
                        row.ISOK = Production[0].ISOK;

                    row.GUID = Guid.NewGuid();

                    if (Production[0].IsTIMEMODENull())
                        row.IsTIMEMODENull();
                    else
                        row.TIMEMODE = Production[0].TIMEMODE;

                    if (Production[0].IsTIMEPREPSTARTNull())
                        row.SetTIMEPREPSTARTNull();
                    else
                        row.TIMEPREPSTART = Production[0].TIMEPREPSTART;

                    //if (ds.Production[0].IsTIMEPREPSTOPNull())
                    //    row.SetTIMEPREPSTOPNull();
                    //else
                    row.TIMEPREPSTOP = Production[0].TIMEPREPSTART.AddMilliseconds(10);

                    if (Production[0].IsTIMECORSTARTNull())
                        row.SetTIMECORSTARTNull();
                    else
                        row.TIMECORSTART = Production[0].TIMECORSTART;

                    if (Production[0].IsTIMECORSTOPNull())
                        row.SetTIMECORSTOPNull();
                    else
                        row.TIMECORSTOP = Production[0].TIMECORSTOP;

                    if (Production[0].IsoperationidNull())
                        row.SetoperationidNull();
                    else
                        row.operationid = Production[0].operationid;

                    if (Production[0].IsSOUBEHGUIDNull())
                        row.SetSOUBEHGUIDNull();
                    else
                        row.SOUBEHGUID = Production[0].SOUBEHGUID;

                    if (Production[0].IsCORRGUIDNull())
                        row.SetCORRGUIDNull();
                    else
                        row.CORRGUID = Production[0].CORRGUID;

                    if (Production[0].IsTIMECRIDTYPENull())
                        row.SetTIMECRIDTYPENull();
                    else
                        row.TIMECRIDTYPE = Production[0].TIMECRIDTYPE;

                    row.SKL_ID = string.Empty;
                    row.LOCNCODE = string.Empty;

                    #endregion

                    dsl.Production.AddProductionRow(row);
                    Data.DatabaseActions.InsertProduction(row);

                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        row.SetAdded();
                        //taprohist.Update(productionRow);
                        Data.DatabaseActions.InsertProductionHistory(row);
                    }
                    //USER EVENT 
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(
                        idPracovnik.id,
                        idmachine.id,
                        DateTime.Now,
                        Settings.UEventZruseniPriprava,
                        idPracovnik.id,
                        Settings.TerminalID,
                        string.Empty,
                        Guid.NewGuid());

                    FlexibleMessageBox.Show(this, "Příprava byla zrušena.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                }


                else if (remote == true)
                {
                    

                    Fask.Aktualizace_API.WebServiceVyroba.VyrobaDataSet ds = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_OpenedProduction(idPracovnik.id, idmachine.id);

                    #region naplneni radku datasetu + kontroly na null

                    if (ds.Production[0].IsCountEntriesNull())
                        row.SetCountEntriesNull();
                    else
                        row.CountEntries = ds.Production[0].CountEntries;

                    if (ds.Production[0].IsSOPNUMBENull())
                        row.SetSOPNUMBENull();
                    else
                        row.SOPNUMBE = ds.Production[0].SOPNUMBE;

                    if (ds.Production[0].IsITEMNMBRNull())
                        row.SetITEMNMBRNull();
                    else
                        row.ITEMNMBR = ds.Production[0].ITEMNMBR;

                    if (ds.Production[0].IsITEMTYPENull())
                        row.SetITEMTYPENull();
                    else
                        row.ITEMTYPE = ds.Production[0].ITEMTYPE;

                    if (ds.Production[0].IsITEMMJNull())
                        row.SetITEMMJNull();
                    else
                        row.ITEMMJ = ds.Production[0].ITEMMJ;

                    if (ds.Production[0].IsORDNull())
                        row.SetORDNull();
                    else
                        row.ORD = ds.Production[0].ORD;

                    //if (ds.Production[0].IsTIMEPREPNull())
                    //    row.SetTIMEPREPNull();
                    //else
                    row.TIMEPREP = 0;

                    if (ds.Production[0].IsTIMEUNITNull())
                        row.SetTIMEUNITNull();
                    else
                        row.TIMEUNIT = ds.Production[0].TIMEUNIT;

                    if (ds.Production[0].IsTIMESTARTNull())
                        row.SetTIMESTARTNull();
                    else
                        row.TIMESTART = ds.Production[0].TIMESTART;

                    if (ds.Production[0].IsTIMESTOPNull())
                        row.SetTIMESTOPNull();
                    else
                        row.TIMESTOP = ds.Production[0].TIMESTOP;

                    if (ds.Production[0].IsTIMECORNull())
                        row.SetTIMECORNull();
                    else
                        row.TIMECOR = ds.Production[0].TIMECOR;

                    if (ds.Production[0].IsTIMECRIDNull())
                        row.SetTIMECRIDNull();
                    else
                        row.TIMECRID = ds.Production[0].TIMECRID;

                    row.loginid = ds.Production[0].loginid;

                    if (ds.Production[0].IsmachineidNull())
                        row.IsmachineidNull();
                    else
                        row.machineid = ds.Production[0].machineid;

                    row.dateeve = DateTime.Now;
                    row.qty = ds.Production[0].qty;
                    row.qtyReal = ds.Production[0].qtyReal;

                    if (ds.Production[0].IsQTYPACKNull())
                        row.IsQTYPACKNull();
                    else
                        row.QTYPACK = ds.Production[0].QTYPACK;

                    if (ds.Production[0].IsQTYPACKMJNull())
                        row.IsQTYPACKMJNull();
                    else
                        row.QTYPACKMJ = ds.Production[0].QTYPACKMJ;

                    //if (ds.Production[0].IsdescriptionNull())
                        //row.IsdescriptionNull();
                    //else
                    row.description = "Příprava byla zrušena:" + idPracovnik.firstname + " " + idPracovnik.surname + "(" + idPracovnik.id + ")";

                    if (ds.Production[0].IsBarcodePNull())
                        row.IsBarcodePNull();
                    else
                        row.BarcodeP = ds.Production[0].BarcodeP;

                    row.UserID = ds.Production[0].UserID;
                    row.TermID = ds.Production[0].TermID;

                    if (ds.Production[0].IsISOKNull())
                        row.SetISOKNull();
                    else
                        row.ISOK = ds.Production[0].ISOK;

                    row.GUID = Guid.NewGuid();

                    if (ds.Production[0].IsTIMEMODENull())
                        row.IsTIMEMODENull();
                    else
                        row.TIMEMODE = ds.Production[0].TIMEMODE;

                    if (ds.Production[0].IsTIMEPREPSTARTNull())
                        row.SetTIMEPREPSTARTNull();
                    else
                        row.TIMEPREPSTART = ds.Production[0].TIMEPREPSTART;

                    //if (ds.Production[0].IsTIMEPREPSTOPNull())
                    //    row.SetTIMEPREPSTOPNull();
                    //else
                    row.TIMEPREPSTOP = ds.Production[0].TIMEPREPSTART.AddMilliseconds(10);

                    if (ds.Production[0].IsTIMECORSTARTNull())
                        row.SetTIMECORSTARTNull();
                    else
                        row.TIMECORSTART = ds.Production[0].TIMECORSTART;

                    if (ds.Production[0].IsTIMECORSTOPNull())
                        row.SetTIMECORSTOPNull();
                    else
                        row.TIMECORSTOP = ds.Production[0].TIMECORSTOP;

                    if (ds.Production[0].IsoperationidNull())
                        row.SetoperationidNull();
                    else
                        row.operationid = ds.Production[0].operationid;

                    if (ds.Production[0].IsSOUBEHGUIDNull())
                        row.SetSOUBEHGUIDNull();
                    else
                        row.SOUBEHGUID = ds.Production[0].SOUBEHGUID;

                    if (ds.Production[0].IsCORRGUIDNull())
                        row.SetCORRGUIDNull();
                    else
                        row.CORRGUID = ds.Production[0].CORRGUID;

                    if (ds.Production[0].IsTIMECRIDTYPENull())
                        row.SetTIMECRIDTYPENull();
                    else
                        row.TIMECRIDTYPE = ds.Production[0].TIMECRIDTYPE;

                    row.SKL_ID = string.Empty;
                    row.LOCNCODE = string.Empty;

                    #endregion



                    dsl.Production.AddProductionRow(row);
                    Data.DatabaseActions.InsertProduction(row);

                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        row.SetAdded();
                        //taprohist.Update(productionRow);
                        Data.DatabaseActions.InsertProductionHistory(row);
                    }

                    //USER EVENT 
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(
                        idpracovnik.id,
                        idmachine.id,
                        DateTime.Now,
                        Settings.UEventZruseniPriprava,
                        idPracovnik.id,
                        Settings.TerminalID,
                        string.Empty,
                        Guid.NewGuid());


                    FlexibleMessageBox.Show(this, "Příprava byla zrušen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                }
            }                        
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

            }
        }

        private void DeleteOdvod(bool? remote, DateTime? lLastTimeStateDateTime, Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow PRow)
        {
            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnikLocal;

                using (Odvadeni.FormHeslo frmHeslo = new Fask.Aktualizace_API.Odvadeni.FormHeslo())
                {
                    if (frmHeslo.ShowDialog(this) == DialogResult.Cancel)
                        return;

                    idPracovnikLocal = frmHeslo.Pracovnik;
                }

                //if (idPracovnik.VS == 0)
                //{
                //    FlexibleMessageBox.Show(this, "Uzivatel nema pravo menit.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                //    return;
                //}

                Fask.SQLiteDBs.DataSets.Vyroba dsl = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow row = dsl.Production.NewProductionRow();

                if (remote == false)
                {
                    Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable Production = new Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable();

                    //pta.FillByBarcodePUserIDdateeve(Production, cislooperace, idPracovnik.id, (DateTime)userLastAction);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.FillByBarcodePUserIDdateeve_Production(Production, PRow.BarcodeP, idPracovnikLocal.id, (DateTime)lLastTimeStateDateTime);

                    #region naplneni radku datasetu + kontroly na null
                    if (Production[0].IsCountEntriesNull())
                        row.SetCountEntriesNull();
                    else
                        row.CountEntries = Production[0].CountEntries;

                    if (Production[0].IsSOPNUMBENull())
                        row.SetSOPNUMBENull();
                    else
                        row.SOPNUMBE = Production[0].SOPNUMBE;

                    if (Production[0].IsITEMNMBRNull())
                        row.SetITEMNMBRNull();
                    else
                        row.ITEMNMBR = Production[0].ITEMNMBR;

                    if (Production[0].IsITEMTYPENull())
                        row.SetITEMTYPENull();
                    else
                        row.ITEMTYPE = Production[0].ITEMTYPE;

                    if (Production[0].IsITEMMJNull())
                        row.SetITEMMJNull();
                    else
                        row.ITEMMJ = Production[0].ITEMMJ;

                    if (Production[0].IsORDNull())
                        row.SetORDNull();
                    else
                        row.ORD = Production[0].ORD;

                    if (Production[0].IsTIMEPREPNull())
                        row.SetTIMEPREPNull();
                    else
                        row.TIMEPREP = Production[0].TIMEPREP;

                    row.TIMEUNIT = Production[0].TIMEUNIT;
                    row.TIMESTART = Production[0].TIMESTART;
                    row.TIMESTOP = Production[0].TIMESTART.AddMilliseconds(10);
                    if (Production[0].IsTIMECORNull())
                        row.SetTIMECORNull();
                    else
                        row.TIMECOR = Production[0].TIMECOR;

                    if (Production[0].IsTIMECRIDNull())
                        row.SetTIMECRIDNull();
                    else
                        row.TIMECRID = Production[0].TIMECRID;

                    row.loginid = Production[0].loginid;

                    if (Production[0].IsmachineidNull())
                        row.IsmachineidNull();
                    else
                        row.machineid = Production[0].machineid;

                    row.dateeve = DateTime.Now;
                    row.qty = Production[0].qty;
                    row.qtyReal = Production[0].qtyReal;

                    if (Production[0].IsQTYPACKNull())
                        row.IsQTYPACKNull();
                    else
                        row.QTYPACK = Production[0].QTYPACK;

                    if (Production[0].IsQTYPACKMJNull())
                        row.IsQTYPACKMJNull();
                    else
                        row.QTYPACKMJ = Production[0].QTYPACKMJ;

                    //if (Production[0].IsdescriptionNull())
                        //row.IsdescriptionNull();
                    //else
                        row.description = "Odvod byl zrušen:" + idPracovnikLocal.firstname + " " + idPracovnikLocal.surname + "(" + idPracovnikLocal.id + ")";

                    if (Production[0].IsBarcodePNull())
                        row.IsBarcodePNull();
                    else
                        row.BarcodeP = Production[0].BarcodeP;

                    row.UserID = Production[0].UserID;
                    row.TermID = Production[0].TermID;

                    if (Production[0].IsISOKNull())
                        row.SetISOKNull();
                    else
                        row.ISOK = Production[0].ISOK;

                    row.GUID = Guid.NewGuid();
                    row.TIMEMODE = Production[0].TIMEMODE;

                    if (Production[0].IsTIMEPREPSTARTNull())
                        row.SetTIMEPREPSTARTNull();
                    else
                        row.TIMEPREPSTART = Production[0].TIMEPREPSTART;

                    if (Production[0].IsTIMEPREPSTOPNull())
                        row.SetTIMEPREPSTOPNull();
                    else
                        row.TIMEPREPSTOP = Production[0].TIMEPREPSTOP;

                    if (Production[0].IsTIMECORSTARTNull())
                        row.SetTIMECORSTARTNull();
                    else
                        row.TIMECORSTART = Production[0].TIMECORSTART;

                    if (Production[0].IsTIMECORSTOPNull())
                        row.SetTIMECORSTOPNull();
                    else
                        row.TIMECORSTOP = Production[0].TIMECORSTOP;

                    if (Production[0].IsoperationidNull())
                        row.SetoperationidNull();
                    else
                        row.operationid = Production[0].operationid;

                    if (Production[0].IsSOUBEHGUIDNull())
                        row.SetSOUBEHGUIDNull();
                    else
                        row.SOUBEHGUID = Production[0].SOUBEHGUID;

                    if (Production[0].IsCORRGUIDNull())
                        row.SetCORRGUIDNull();
                    else
                        row.CORRGUID = Production[0].CORRGUID;

                    if (Production[0].IsTIMECRIDTYPENull())
                        row.SetTIMECRIDTYPENull();
                    else
                        row.TIMECRIDTYPE = Production[0].TIMECRIDTYPE;

                    row.SKL_ID = string.Empty;
                    row.LOCNCODE = string.Empty;
                    #endregion

                    dsl.Production.AddProductionRow(row);
                    Data.DatabaseActions.InsertProduction(row);

                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        row.SetAdded();
                        //taprohist.Update(productionRow);
                        Data.DatabaseActions.InsertProductionHistory(row);
                    }

                    //USER EVENT 
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(
                        idPracovnikLocal.id,
                        idmachine.id,
                        DateTime.Now,
                        Settings.UEventZruseniOdvod,
                        idPracovnikLocal.id,
                        Settings.TerminalID,
                        string.Empty,
                        Guid.NewGuid());

                    FlexibleMessageBox.Show(this, "Odvod byl zrušen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                }
                else if (remote == true)
                {
                    Fask.Aktualizace_API.WebServiceVyroba.VyrobaDataSet ds = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_OpenedProduction(idPracovnikLocal.id, idmachine.id);

                    #region naplneni radku datasetu + kontroly na null
                    if (ds.Production[0].IsCountEntriesNull())
                        row.SetCountEntriesNull();
                    else
                        row.CountEntries = ds.Production[0].CountEntries;

                    if (ds.Production[0].IsSOPNUMBENull())
                        row.SetSOPNUMBENull();
                    else
                        row.SOPNUMBE = ds.Production[0].SOPNUMBE;

                    if (ds.Production[0].IsITEMNMBRNull())
                        row.SetITEMNMBRNull();
                    else
                        row.ITEMNMBR = ds.Production[0].ITEMNMBR;

                    if (ds.Production[0].IsITEMTYPENull())
                        row.SetITEMTYPENull();
                    else
                        row.ITEMTYPE = ds.Production[0].ITEMTYPE;

                    if (ds.Production[0].IsITEMMJNull())
                        row.SetITEMMJNull();
                    else
                        row.ITEMMJ = ds.Production[0].ITEMMJ;

                    if (ds.Production[0].IsORDNull())
                        row.SetORDNull();
                    else
                        row.ORD = ds.Production[0].ORD;

                    if (ds.Production[0].IsTIMEPREPNull())
                        row.SetTIMEPREPNull();
                    else
                        row.TIMEPREP = ds.Production[0].TIMEPREP;

                    row.TIMEUNIT = ds.Production[0].TIMEUNIT;
                    row.TIMESTART = ds.Production[0].TIMESTART;
                    row.TIMESTOP = ds.Production[0].TIMESTART.AddMilliseconds(10);

                    if (ds.Production[0].IsTIMECORNull())
                        row.SetTIMECORNull();
                    else
                        row.TIMECOR = ds.Production[0].TIMECOR;

                    if (ds.Production[0].IsTIMECRIDNull())
                        row.SetTIMECRIDNull();
                    else
                        row.TIMECRID = ds.Production[0].TIMECRID;

                    row.loginid = ds.Production[0].loginid;

                    if (ds.Production[0].IsmachineidNull())
                        row.IsmachineidNull();
                    else
                        row.machineid = ds.Production[0].machineid;

                    row.dateeve = DateTime.Now;
                    row.qty = ds.Production[0].qty;
                    row.qtyReal = ds.Production[0].qtyReal;

                    if (ds.Production[0].IsQTYPACKNull())
                        row.IsQTYPACKNull();
                    else
                        row.QTYPACK = ds.Production[0].QTYPACK;

                    if (ds.Production[0].IsQTYPACKMJNull())
                        row.IsQTYPACKMJNull();
                    else
                        row.QTYPACKMJ = ds.Production[0].QTYPACKMJ;

                    //if (ds.Production[0].IsdescriptionNull())
                        //row.IsdescriptionNull();
                    //else
                        row.description = "Odvod byl zrušen:" + idPracovnikLocal.firstname + " " + idPracovnikLocal.surname + "(" + idPracovnikLocal.id + ")";

                    if (ds.Production[0].IsBarcodePNull())
                        row.IsBarcodePNull();
                    else
                        row.BarcodeP = ds.Production[0].BarcodeP;

                    row.UserID = ds.Production[0].UserID;
                    row.TermID = ds.Production[0].TermID;

                    if (ds.Production[0].IsISOKNull())
                        row.SetISOKNull();
                    else
                        row.ISOK = ds.Production[0].ISOK;

                    row.GUID = Guid.NewGuid();
                    row.TIMEMODE = ds.Production[0].TIMEMODE;

                    if (ds.Production[0].IsTIMEPREPSTARTNull())
                        row.SetTIMEPREPSTARTNull();
                    else
                        row.TIMEPREPSTART = ds.Production[0].TIMEPREPSTART;

                    if (ds.Production[0].IsTIMEPREPSTOPNull())
                        row.SetTIMEPREPSTOPNull();
                    else
                        row.TIMEPREPSTOP = ds.Production[0].TIMEPREPSTOP;

                    if (ds.Production[0].IsTIMECORSTARTNull())
                        row.SetTIMECORSTARTNull();
                    else
                        row.TIMECORSTART = ds.Production[0].TIMECORSTART;

                    if (ds.Production[0].IsTIMECORSTOPNull())
                        row.SetTIMECORSTOPNull();
                    else
                        row.TIMECORSTOP = ds.Production[0].TIMECORSTOP;

                    if (ds.Production[0].IsoperationidNull())
                        row.SetoperationidNull();
                    else
                        row.operationid = ds.Production[0].operationid;

                    if (ds.Production[0].IsSOUBEHGUIDNull())
                        row.SetSOUBEHGUIDNull();
                    else
                        row.SOUBEHGUID = ds.Production[0].SOUBEHGUID;

                    if (ds.Production[0].IsCORRGUIDNull())
                        row.SetCORRGUIDNull();
                    else
                        row.CORRGUID = ds.Production[0].CORRGUID;

                    if (ds.Production[0].IsTIMECRIDTYPENull())
                        row.SetTIMECRIDTYPENull();
                    else
                        row.TIMECRIDTYPE = ds.Production[0].TIMECRIDTYPE;

                    row.SKL_ID = string.Empty;
                    row.LOCNCODE = string.Empty;
                    #endregion

                    dsl.Production.AddProductionRow(row);
                    Data.DatabaseActions.InsertProduction(row);

                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        row.SetAdded();
                        //taprohist.Update(productionRow);
                        Data.DatabaseActions.InsertProductionHistory(row);
                    }

                    //USER EVENT 
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(
                        idPracovnikLocal.id,
                        idmachine.id,
                        DateTime.Now,
                        Settings.UEventZruseniOdvod,
                        idPracovnikLocal.id,
                        Settings.TerminalID,
                        string.Empty,
                        Guid.NewGuid());

                    FlexibleMessageBox.Show(this, "Odvod byl zrušen.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
        }
    }


}
