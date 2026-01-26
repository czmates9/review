using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using System.Data;
using Fask.Vyroba_P.Extensions;
using System.IO;

namespace Fask.Vyroba_P.OdvadeniNadop
{
    public partial class FormOdvadeni
    {

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug ,"Zahajeni - PerformOK() " + "Fask.Vyroba_P.OdvadeniNadop");
            textBoxVyrobniOperaceFocusAll();

            if (this.textBoxVyrobniOperace.Text.Trim().Length == 0)
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Musite zadat hodnotu zacatek "+ "Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Musite zadat hodnotu konec "+ "Fask.Vyroba_P.OdvadeniNadop");
                return;
            }

            Fask.SQLiteDBs.DataSets.Vyroba dsV = new Fask.SQLiteDBs.DataSets.Vyroba();

            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow zakazka = null;
            zakazka = _1NajdiZakazku(this.textBoxVyrobniOperace.Text.Trim());
            if (zakazka == null)
                return;

            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter tapro = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //tapro.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf));
            //Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter tacor = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter();
            //tacor.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf));
            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter taprohist = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //taprohist.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfHist));

            // TODO : Kontrola Corrections
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zahajeni kontroly korekci "+"Fask.Vyroba_P.OdvadeniNadop");
            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdtAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);
                pdt.PrimaryKey = new DataColumn[] { pdt.GUIDColumn };
                pdtAll.PrimaryKey = new DataColumn[] { pdtAll.GUIDColumn };
                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ziskani korekci ze serveru - zahajeni "+ "Fask.Vyroba_P.OdvadeniNadop");
                    //WebServiceVyroba.VyrobaDataSet dsW = vyrobaS.Production_OpenedCorrection(idpracovnik.id, string.Empty);
                    //WebServiceVyroba.VyrobaDataSet dsWAll = vyrobaS.Production_OpenedCorrection(idpracovnik.id, idmachine.id);
                    WebServiceVyroba.VyrobaDataSet dsW = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, string.Empty);
                    WebServiceVyroba.VyrobaDataSet dsWAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, idmachine.id);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ziskani korekci ze serveru - konec "+ "Fask.Vyroba_P.OdvadeniNadop");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Korekce merge - zahajeni "+ "Fask.Vyroba_P.OdvadeniNadop");
                    dsW.Production.PrimaryKey = new DataColumn[] { dsW.Production.GUIDColumn };
                    dsWAll.Production.PrimaryKey = new DataColumn[] { dsWAll.Production.GUIDColumn };

                    pdt.Merge(dsW.Production, false, MissingSchemaAction.Ignore);
                    pdt.Merge(dsWAll.Production, false, MissingSchemaAction.Ignore);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Korekce merge - konec "+ "Fask.Vyroba_P.OdvadeniNadop");
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz konec " + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                        if (dr == DialogResult.Cancel)
                            return;
                    }
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Merge vseho dohromady " + "Fask.Vyroba_P.OdvadeniNadop");
                pdt.Merge(pdtAll, false, MissingSchemaAction.Ignore);  

                // naplneni dat z cache
                //pdt.Merge(pdtCache1, true, MissingSchemaAction.Ignore);
                //pdt.Merge(pdtCache2, true, MissingSchemaAction.Ignore);

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Linq - serazeni dle casu udalosti" + "Fask.Vyroba_P.OdvadeniNadop");
                var prowsdesc = pdt.OrderByDescending(p => p.dateeve);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Test count>0 " +  "Fask.Vyroba_P.OdvadeniNadop");
                if (prowsdesc.Count() > 0)
                { // je otevrena korekce ...

                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Existuje otevrena korekce " + "Fask.Vyroba_P.OdvadeniNadop");

                    var prowcorrection = prowsdesc.First();

                    if (!prowcorrection.IsTIMECRIDNull()
                        && !prowcorrection.IsTIMECORSTARTNull()
                        && prowcorrection.IsTIMECORSTOPNull()
                        )
                    {

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ziskani korekce a jeji ukonceni "+ "Fask.Vyroba_P.OdvadeniNadop");

                        var correctionrow = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(prowsdesc.First().TIMECRID).First(); ;

                        //if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo))
                        //{
                        //    return;
                        //}
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zobrazeni dialogu: Ukončit existující korekci '" + correctionrow.desc.Trim() + "'? dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'? dotaz konec " + dr.ToString() +  " Fask.Vyroba_P.OdvadeniNadop");
                        if (dr == DialogResult.No)
                        {
                            return;
                        }

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Naplneni productionRow pro ulozeni "+ "Fask.Vyroba_P.OdvadeniNadop");

                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                        productionRow.description = prowcorrection.description;
                        productionRow.GUID = Guid.NewGuid();
                        productionRow.loginid = Settings.LastProductionUserID;
                        productionRow.qty = prowcorrection.qty;
                        productionRow.qtyReal = prowcorrection.qtyReal;
                        productionRow.UserID = idpracovnik.id;
                        if (!prowcorrection.IsmachineidNull())
                            productionRow.machineid = prowcorrection.machineid;
                        productionRow.TermID = Settings.TerminalID;
                        if (!prowcorrection.IsCORRGUIDNull())
                            productionRow.CORRGUID = prowcorrection.CORRGUID;
                        productionRow.TIMECRID = prowcorrection.TIMECRID;
                        productionRow.TIMECORSTART = prowcorrection.TIMECORSTART;
                        productionRow.TIMECORSTOP = DateTime.Now;
                        productionRow.TIMECOR = (float)(productionRow.TIMECORSTOP - productionRow.TIMECORSTART).TotalMinutes;
                        productionRow.dateeve = DateTime.Now;

                        dsV.Production.AddProductionRow(productionRow);

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni productionRow do vystupu zahajeni" + "Fask.Vyroba_P.OdvadeniNadop");

                        //tapro.Connection.Open();
                        //tapro.Update(productionRow);
                        //tapro.Connection.Close();
                        Data.DatabaseActions.InsertProduction(productionRow);


                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni productionRow do vystupu konec" + "Fask.Vyroba_P.OdvadeniNadop");

                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            productionRow.SetAdded();
                            // pridani do historie
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridani productionRow do historie zahajeni" + "Fask.Vyroba_P.OdvadeniNadop");

                            //taprohist.Connection.Open();
                            //taprohist.Update(productionRow);
                            //taprohist.Connection.Close();
                            Data.DatabaseActions.InsertProductionHistory(productionRow);
                         
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridani productionRow do historie konec" + "Fask.Vyroba_P.OdvadeniNadop");
                        }
                        if (correctionrow.id == Settings.PrestavkaID)
                        {
                            this.buttonKonecPrestavky.Enabled = false;
                            this.buttonZahajeniPrestavky.Enabled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), kontrola korekce dialog "+ "Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), kontrola korekce dialog konec "+ "Fask.Vyroba_P.OdvadeniNadop");
                return;
            }


            // TODO : nalezeni posledniho pohybu a nastaveni stavu operace(i) ...

            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nacteni produkce z lokalu a ze serveru "+ "Fask.Vyroba_P.OdvadeniNadop");

            // 1) nacteni produkce z lokalu a 1 ze serveru...
            try
            {

                Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.FillByUserIDMachineID_Production(dsV.Production, idpracovnik.id, idmachine.id);

                WebServiceVyroba.VyrobaDataSet dsVweb = null;

                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Online otevrena produkce ze serveru zahajeni "+ "Fask.Vyroba_P.OdvadeniNadop");

                    //dsVweb = vyrobaS.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
                    //dsVweb = vyrobaS.Production_OpenedProduction(idpracovnik.id, idmachine.id);
                    dsVweb = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedProduction2(idpracovnik.id, idmachine.id);

                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Online otevrena produkce ze serveru konec "+ "Fask.Vyroba_P.OdvadeniNadop");
                }
                catch (Exception exWeb)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu zakázky ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //    return;
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu zakázky ze serveru... Pokračovat? dotaz "+"Fask.Vyroba_P.OdvadeniNadop");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu zakázky ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu zakázky ze serveru... Pokračovat? dotaz konec " + dr.ToString()+ "  Fask.Vyroba_P.OdvadeniNadop");
                        if(dr == DialogResult.Cancel)
                            return;
                    }
                }

                if (dsVweb != null)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Merge produkce " + "Fask.Vyroba_P.OdvadeniNadop");
                    // nastaveni primarnich klicu kvuli merge
                    dsV.Production.PrimaryKey = new DataColumn[] { dsV.Production.GUIDColumn };
                    dsVweb.Production.PrimaryKey = new DataColumn[] { dsVweb.Production.GUIDColumn };
                    //if(dsVweb.Production.Count > 0 && 

                    // Ted mam platna data v dsV.Production
                    dsV.Production.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                }

                // nacteni dat z cache
                //taprocache.FillByUserIDMachineID(dsV.Production, idpracovnik.id, idmachine.id);
                //Data.VyrobaCEDataSet.ProductionDataTable pdtCache3 = taprocache.GetDataByUserIDMachineID(idpracovnik.id, idmachine.id);
                //dsV.Production.Merge(pdtCache3, false, MissingSchemaAction.Ignore);

            }
            catch (Exception ex2)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), kontrola produkce dialog " + "Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex2.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), kontrola produkce dialog konec " + "Fask.Vyroba_P.OdvadeniNadop");
                return;
            }

            // 2. Kontrola zda je otevrena jina zakazka
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Kontrola otevrenosti zakazky " + "Fask.Vyroba_P.OdvadeniNadop");
            try
            {
                if (dsV.Production.Count > 0)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production neco obsahuje => linq serazeni desc dle dateeve " + "Fask.Vyroba_P.OdvadeniNadop");

                    var pDateeveOrderbyDesc = dsV.Production.OrderByDescending(x => x.dateeve);
                    var xRow = pDateeveOrderbyDesc.First();
                    if (//xRow.SOPNUMBE != zakazka.SOPNUMBE
                        //&& 
                        !xRow.IsTIMESTARTNull()
                        &&
                        xRow.IsTIMESTOPNull()
                        )
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nalezeny otevrene produkce => dotaz ukoncit " + "Fask.Vyroba_P.OdvadeniNadop");

                        // TODO : Ukoncit zakazku/ky?
                        DialogResult drukoncit = DialogResult.None;
                        // id stroje se shoduje
                        if (xRow.machineid.Trim() == idmachine.id.Trim())
                        {
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukončit zakázku(y)?, ukoncit zakazku dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                            drukoncit = FlexibleMessageBox.Show(this, "Ukončit zakázku(y)?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukončit zakázku(y)?: " + drukoncit.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                            if (drukoncit == DialogResult.No)
                                return;
                        }
                        else
                        {
                            // zakazka byla zahajena na jinem stroji ...
                            if (Settings.PovolitUkonceniZJinehoStroje)
                            {
                                // je povoleno ukonceni
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. Ukončit zakázku(y)? dotaz " +  "Fask.Vyroba_P.OdvadeniNadop");
                                drukoncit = FlexibleMessageBox.Show(this, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. \nUkončit zakázku(y)?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. Ukončit zakázku(y)?: " + drukoncit.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                                if (drukoncit == DialogResult.No)
                                    return;
                            }
                            else
                            {
                                // chyba, neni povoleno ukonceni
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. Není možné pokračovat. dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                                FlexibleMessageBox.Show(this, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. \nNení možné pokračovat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. Není možné pokračovat. Ukonceni " +  "Fask.Vyroba_P.OdvadeniNadop");
                                return;
                            }
                        }

                        
                        //else //ukoncit a pokracovat ... 

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncovani zakazky(zakazek) " + "Fask.Vyroba_P.OdvadeniNadop");

                        if (xRow.IsSOUBEHGUIDNull())
                        { // neni soubeh, tak ukoncit jen normalne jednu
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Neni soubeh => ukonceni jedne " + "Fask.Vyroba_P.OdvadeniNadop");

                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nP = dsV.Production.NewProductionRow();
                            NewProductionPrepare(nP, null);
                            nP.machineid = xRow.IsmachineidNull() ? null : xRow.machineid;
                            nP.CountEntries = xRow.CountEntries;
                            nP.SOPNUMBE = xRow.SOPNUMBE;
                            nP.TIMESTART = xRow.TIMESTART;
                            nP.TIMESTOP = DateTime.Now;
                            nP.operationid = xRow.operationid;
                            dsV.Production.AddProductionRow(nP);
                        }
                        else
                        { // je soubeh, tak ukoncit vice ... 
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Je soubeh => ukonceni vice zahajeni " +  "Fask.Vyroba_P.OdvadeniNadop");

                            List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> resSoubeh = dsV.Production.Where(x => !x.IsSOUBEHGUIDNull() && (x.SOUBEHGUID == xRow.SOUBEHGUID)).ToList();
                            //List<Data.VyrobaCEDataSet.ProductionRow> resSoubeh = dsV.Production.Where(x => !x.IsSOUBEHGUIDNull() && (x.SOUBEHGUID == xRow.SOUBEHGUID)).GroupBy(test => test.GUID).Select(grp => grp.First()).ToList();
                            DateTime dtnow = DateTime.Now;
                            foreach (var item in resSoubeh)
                            {
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncovani zakazky " + item.SOPNUMBE.ToString() +" Fask.Vyroba_P.OdvadeniNadop");

                                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nP = dsV.Production.NewProductionRow();
                                NewProductionPrepare(nP, null);
                                nP.machineid = xRow.IsmachineidNull() ? null : xRow.machineid;
                                nP.CountEntries = item.CountEntries;
                                nP.SOPNUMBE = item.SOPNUMBE;
                                nP.TIMESTART = item.TIMESTART;
                                nP.TIMESTOP = dtnow;
                                nP.operationid = item.operationid;
                                nP.SOUBEHGUID = item.SOUBEHGUID;
                                dsV.Production.AddProductionRow(nP);
                            }
                        }

                        var drAdded = dsV.Production.Select(null, null, DataViewRowState.Added);

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni produkce do vystupu - celkem zaznamu:" + drAdded.Count().ToString() + "(" + dsV.Production.Count.ToString() + ")" + " Fask.Vyroba_P.OdvadeniNadop");

                        //Update production
                        //tapro.Update(dsV.Production);
                        
                        //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection open", "Fask.Vyroba_P.OdvadeniNadop");
                        //try { tapro.Connection.Open(); }
                        //catch { }
                        //try
                        //{
                        Data.DatabaseActions.insertProductionDataTable(dsV.Production);
                            //foreach (var item in dsV.Production)
                            //{
                            //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "item : " + item.RowState + " : " + (string)item["SOPNUMBE"], "Fask.Vyroba_P.OdvadeniNadop");
                            //    //tapro.Update(item);
                            //    Data.DatabaseActions.InsertProduction(item);
                            //}
                        //}
                        //catch { }
                        //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection close", "Fask.Vyroba_P.OdvadeniNadop");
                        //try { tapro.Connection.Close(); }
                        //catch { }

                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni - konec " + "Fask.Vyroba_P.OdvadeniNadop");

                        // pridani do historie
                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridani do historie "+"Fask.Vyroba_P.OdvadeniNadop");

                            foreach (var item in drAdded)
                            {
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridani do historie : " + (string)item["SOPNUMBE"] + " Fask.Vyroba_P.OdvadeniNadop");

                                item.SetAdded();
                            }

                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "History update" + " Fask.Vyroba_P.OdvadeniNadop");

                            //taprohist.Update(drAdded);
                            //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection open", "Fask.Vyroba_P.OdvadeniNadop");
                            //try { taprohist.Connection.Open(); }
                            //catch { }
                            //try
                            //{
                                foreach (var item in drAdded)
                                {
                                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "item : " + item.RowState + " : " + (string)item["SOPNUMBE"] +  " Fask.Vyroba_P.OdvadeniNadop");
                                    //taprohist.Update(item);
                                    Data.DatabaseActions.InsertProductionHistory(item as Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow);
                                }
                            //}
                            //catch { }
                            
                            //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection open", "Fask.Vyroba_P.OdvadeniNadop");
                            //try { taprohist.Connection.Close(); }
                            //catch { }


                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "History updatec - konec" +  " Fask.Vyroba_P.OdvadeniNadop");

                        }
                    }

                    //if (xRow.SOPNUMBE == zakazka.SOPNUMBE)
                    //{ // konec doslo k ukonceni zahajene zakazky ...
                    //    //Odvod dokoncen => zastavit scanner a konec
                    //    ScannerStop();
                    //    DialogResult = DialogResult.OK;
                    //    return;
                    //}
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Priprava nove produkce" + " Fask.Vyroba_P.OdvadeniNadop");

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nProduction = dsV.Production.NewProductionRow();

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "NewProductionPrepare(nProduction, zakazka)" +  " Fask.Vyroba_P.OdvadeniNadop");

                NewProductionPrepare(nProduction, zakazka);

                ScannerStop();

                Guid soubehGuid = Guid.NewGuid();
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "using FormOperace " + "Fask.Vyroba_P.OdvadeniNadop");
                using (FormOperace frmOperace = new FormOperace(
                    idpracovnik,
                    idmachine,
                    zakazka,
                    dsV.Production
                    ))
                {
                    frmOperace.Production = nProduction;
                    if (DialogResult.Cancel == frmOperace.ShowDialog(this))
                        return;
                    frmOperace.Production.SOUBEHGUID = soubehGuid;
                    nProduction = frmOperace.Production; //??? toto byt nemusi ...
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "using FormOperace - konec " + "Fask.Vyroba_P.OdvadeniNadop");

                List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> nPlist = new List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>();
                nPlist.Add(nProduction);

                // TODO : pridat soubezne zakazky ...
                // a) pouze pokud se jedna o start zakazky

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Přidat další zakázku do souběhu? dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                DialogResult drSouheh = FlexibleMessageBox.Show(this, "Přidat další zakázku do souběhu?", "Souběh", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Přidat další zakázku do souběhu?: " + drSouheh.ToString()  + " Fask.Vyroba_P.OdvadeniNadop");

                if (drSouheh == DialogResult.Cancel)
                    return;
                else if (drSouheh == DialogResult.Yes)
                {
                    while (true)
                    {
                        Fask.Vyroba_P.OdvadeniNadop.FormInputKodWithVPHListWithVPHList fkod = new Fask.Vyroba_P.OdvadeniNadop.FormInputKodWithVPHListWithVPHList();
                        fkod.Text = "Zadejte zakázku :";
                        // zobrazit list
                        fkod.Vypis = nPlist.OrderByDescending(x => x.dateeve).ToList();
                        if (fkod.ShowDialog(this) == DialogResult.Cancel)
                            break;
                        else
                        {
                            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow zaksoubeh = _1NajdiZakazku(fkod.Kod);
                            if (zaksoubeh != null)
                            {
                                // test zda jiz neni zakazka v listu pridana ... 
                                bool allreadyadded = false;
                                foreach (var z in nPlist)
                                {
                                    if (z.SOPNUMBE == zaksoubeh.SOPNUMBE && z.CountEntries == zaksoubeh.CountEntries)
                                    {
                                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zakázka je již přidána k souběhu dotaz " + "Fask.Vyroba_P.OdvadeniNadop");
                                        FlexibleMessageBox.Show(this, "Zakázka je již přidána k souběhu", "Souběh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zakázka je již přidána k souběhu dotaz konec " + "Fask.Vyroba_P.OdvadeniNadop");
                                        allreadyadded = true;
                                        break;
                                    }
                                }

                                // pokud je jiz pridana, tak prerusi vlozeni ...
                                if (allreadyadded)
                                    continue;

                                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nSProduction = dsV.Production.NewProductionRow();
                                NewProductionPrepare(nSProduction, zaksoubeh);
                                nSProduction.TIMESTART = nProduction.TIMESTART;
                                nSProduction.operationid = nProduction.operationid;
                                nPlist.Add(nSProduction);
                            }
                        }
                    }

                    //Guid soubehGuid = Guid.NewGuid();
                    foreach (var item in nPlist)
                    {
                        item.SOUBEHGUID = soubehGuid;
                    }
                }
                //else if (drSouheh == DialogResult.No)
                //    ;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Soubehy konec " + "Fask.Vyroba_P.OdvadeniNadop");

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridani soubehu do produkce" + " Fask.Vyroba_P.OdvadeniNadop");
                dsV.AcceptChanges();
                foreach (var nP in nPlist)
                {
                    dsV.Production.AddProductionRow(nP);
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni produkce do db - start" + " Fask.Vyroba_P.OdvadeniNadop");

                var drAdded2 = dsV.Production.Select(null, null, DataViewRowState.Added);
                
                //tapro.Update(dsV.Production); // ulozeni do vystupu...
                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection open", "Fask.Vyroba_P.OdvadeniNadop");
                //try { tapro.Connection.Open(); }
                //catch { }
                //try
                //{
                Data.DatabaseActions.insertProductionDataTable(dsV.Production);
                    //foreach (var item in dsV.Production)
                    //{
                    //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "item : " + item.RowState + " : " + (string)item["SOPNUMBE"], "Fask.Vyroba_P.OdvadeniNadop");
                    //    //tapro.Update(item);
                    //    Data.DatabaseActions.InsertProduction(item);
                    //}
                //}
                //catch { }
                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection close", "Fask.Vyroba_P.OdvadeniNadop");
                //try { tapro.Connection.Close(); }
                //catch { }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni produkce do db - konec " + "Fask.Vyroba_P.OdvadeniNadop");

                
                // pridani do historie
                if (Settings.ModulPovolitPrehledOdvodu)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni produkce do historie" + " Fask.Vyroba_P.OdvadeniNadop");
                    foreach (var item in drAdded2)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "item : " + item.RowState + " : " + (string)item["SOPNUMBE"] + " Fask.Vyroba_P.OdvadeniNadop");
                        item.SetAdded();
                    }
                    //taprohist.Update(drAdded2);
                    
                    //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection open", "Fask.Vyroba_P.OdvadeniNadop"); 
                    //try { taprohist.Connection.Open(); }
                    //catch { }
                    //try
                    //{
                        foreach (var item in dsV.Production)
                        {
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "item : " + item.RowState + " : " + (string)item["SOPNUMBE"] + " Fask.Vyroba_P.OdvadeniNadop");
                            //taprohist.Update(item);
                            Data.DatabaseActions.InsertProductionHistory(item);
                        }
                    //}
                    //catch { }
                    //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Connection close", "Fask.Vyroba_P.OdvadeniNadop");
                    //try { taprohist.Connection.Close(); }
                    //catch { }

                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ulozeni produkce do historie - konec" + " Fask.Vyroba_P.OdvadeniNadop");

                }
            }
            catch (Exception ex2)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), odvadeni dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex2.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOK(), odvadeni dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }
            //tapro.Update(dsV.Production); // ulozeni do vystupu...
            //Odvod dokoncen => zastavit scanner a konec
            //ScannerStop();
            DialogResult = DialogResult.OK;
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Konec PerformOK()" +  " Fask.Vyroba_P.OdvadeniNadop");

        }

        private void NewProductionPrepare(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nP, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow zakazka)
        {
            nP.CountEntries = zakazka == null ? 0 : zakazka.CountEntries;
            nP.SOPNUMBE = zakazka == null ? string.Empty : zakazka.SOPNUMBE;
            nP.loginid = Globals.PracovnikVedouciSmeny == null ? string.Empty : Globals.PracovnikVedouciSmeny.id;
            nP.dateeve = DateTime.Now;
            nP.description = string.Empty;
            nP.GUID = Guid.NewGuid();
            nP.UserID = idpracovnik.id;
            nP.machineid = idmachine.id;
            nP.ORD = 0;
            nP.qty = 0;
            nP.QTYPACK = 0;
            nP.QTYPACKMJ = string.Empty;
            nP.qtyReal = 0;
            nP.TermID = Settings.TerminalID;
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _1NajdiZakazku(string p)
        {
            //Najit vyr.prikaz
            try
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter tavph = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                //tavph.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dtVPH = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeH_CZPRO_VPH(p);

                if (dtVPH.Count == 0)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zakázka nenalezena dotaz" + " Fask.Vyroba_P.OdvadeniNadop._1NajdiZakazku(string p)");
                    FlexibleMessageBox.Show(this, "Zakázka nenalezena", this.Text, MessageBoxButtons.OK);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zakázka nenalezena dotaz konec" + " Fask.Vyroba_P.OdvadeniNadop._1NajdiZakazku(string p)");
                    return null;
                }
                else if (dtVPH.Count > 1)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Více zakázek! dotaz" + " Fask.Vyroba_P.OdvadeniNadop._1NajdiZakazku(string p)");
                    FlexibleMessageBox.Show(this, "Více zakázek!", this.Text, MessageBoxButtons.OK);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Více zakázek! dotaz konec" + " Fask.Vyroba_P.OdvadeniNadop._1NajdiZakazku(string p)");
                    // TODO : Výběr
                    return null;
                }
                else
                {
                    return dtVPH[0];
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception _1NajdiZakazku, odvadeni dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception _1NajdiZakazku, odvadeni dialog konec" +  " Fask.Vyroba_P.OdvadeniNadop");
                return null;
            }
        }

        /// <summary>
        /// Zahájení korekce: "Přestávka"
        /// </summary>
        public void PerformZahajeniPrestavky()
        {
            // 1) zjištění, jestli je otevřená korekce
            // 2) pokud ano, ukončit existující?
            // 3) zahájit korekci přestávka
            // 4) přechod do volby pracovníka (podle volby v nastavení)

            Fask.SQLiteDBs.DataSets.Vyroba dsV = new Fask.SQLiteDBs.DataSets.Vyroba();
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter crrta = null;
            Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable crrdt = null;
            // zjištění předchozí korekce

            Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow correctionRow = null;
            //pta.FillByUserIDMachineIDNULL(dsV.Production, pracovnik.id);
            try
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "PerformZahajeniPrestavky START, uzivatel: " + idpracovnik.id);

                // zjištění stavu korekce ze serveru
                try
                {
                    dsV.Production.PrimaryKey = new DataColumn[] { dsV.Production.GUIDColumn };

                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "nacteni dat z cache");
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.FillByUserIDMachineIDNULL_Production(dsV.Production, idpracovnik.id);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "nacteni dat z webu, timeout je: " + Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Timeout);

                    //WebServiceVyroba.VyrobaDataSet dsVWeb = vyrobaS.Production_OpenedCorrection(idpracovnik.id, string.Empty); //korekce mimo vyrobu ...
                    WebServiceVyroba.VyrobaDataSet dsVWeb = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, string.Empty); //korekce mimo vyrobu ...
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "nacteni dat z webu - dokonceno");
                    
                    if (dsVWeb != null)
                    {
                        dsVWeb.Production.PrimaryKey = new DataColumn[] { dsVWeb.Production.GUIDColumn };
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "dsvWeb - merge");
                        dsV.Production.Merge(dsVWeb.Production, false, MissingSchemaAction.Ignore);
                    }
                }
                catch (Exception exWeb)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekce ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //{
                    //    return;
                    //}
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)                    
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekce ze serveru... Pokračovat? dotaz" + " Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekce ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekce ze serveru... Pokračovat? dotaz konec " + dr.ToString()  + " Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                        
                        if(dr == DialogResult.Cancel)
                            return;
                    }
                }

                // zjištění, jestli je otevřená korekce
                bool korekceStart = true; //zahajit korekci
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Kontrola, zdali je jiz otevrena korekce START");
                var correctionOpened = dsV.Production.OrderByDescending(p => p.dateeve);
                if (correctionOpened.Count() > 0
                    && correctionOpened.First().IsTIMECORSTOPNull()
                    && !correctionOpened.First().IsTIMECRIDNull()
                    && !correctionOpened.First().IsTIMECORSTARTNull()
                    )
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Korekce je jiz otevrena");
                    Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable cdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(correctionOpened.First().TIMECRID);
                    if (cdt.Count <= 0)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Otevřená korekce s ID='" + correctionOpened.First().TIMECRID + "' nenalezena! dotaz" +  " Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                        FlexibleMessageBox.Show(this, "Otevřená korekce s ID='" + correctionOpened.First().TIMECRID + "' nenalezena!", "Korekce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Otevřená korekce s ID='" + correctionOpened.First().TIMECRID + "' nenalezena! dotaz konec" + " Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                        return;
                    }
                    correctionRow = cdt[0];
                    korekceStart = false; // ukoncit korekci
                }
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Kontrola, zdali je jiz otevrena korekce END");

                // korekce již existuje, ukončit ...
                if (!korekceStart)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncovani korekce START");
                    //if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukončit korekci '" + correctionRow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    //{
                    //    return;
                    //}
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukončit korekci '" + correctionRow.desc.Trim() + "'? dotaz " + "Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                    DialogResult dr = FlexibleMessageBox.Show(this, "Ukončit korekci '" + correctionRow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukončit korekci '" + correctionRow.desc.Trim() + "'? dotaz konec " + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop.PerformZahajeniPrestavky()");
                    if(dr == DialogResult.No)
                    {
                        return;
                    }

                    Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                    Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowCorrection = correctionOpened.First();
                    productionRow.description = productionRowCorrection.description;
                    productionRow.GUID = Guid.NewGuid();
                    productionRow.loginid = Settings.LastProductionUserID;
                    productionRow.qty = productionRowCorrection.qty;
                    productionRow.qtyReal = productionRowCorrection.qtyReal;
                    if (!productionRowCorrection.IsCORRGUIDNull())
                        productionRow.CORRGUID = productionRowCorrection.CORRGUID;
                    productionRow.UserID = idpracovnik.id;
                    productionRow.TermID = Settings.TerminalID;
                    productionRow.TIMECRID = productionRowCorrection.TIMECRID;
                    productionRow.TIMECORSTART = productionRowCorrection.TIMECORSTART;
                    productionRow.TIMECORSTOP = DateTime.Now;
                    productionRow.TIMECOR = (float)(productionRow.TIMECORSTOP - productionRow.TIMECORSTART).TotalMinutes;
                    productionRow.dateeve = DateTime.Now;

                    dsV.Production.AddProductionRow(productionRow);
                    //tapro.Update(productionRow);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vlozeni do Production");
                    Data.DatabaseActions.InsertProduction(productionRow);
                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vlozeni do ProductionHist");
                        productionRow.SetAdded();
                        // pridani do historie
                        //taprohist.Update(productionRow);
                        Data.DatabaseActions.InsertProductionHistory(productionRow);
                    }
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncovani korekce END");
                }

            // Zahájení korekce přestávka            
                pracovnik = Globals.Pracovnik;

                //crrta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
                //crrta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "zahajeni korekce prestavka START");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Najiti korekce podle ID :" + Settings.PrestavkaID);
                crrdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(Settings.PrestavkaID);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nalezeno zaznamu korekci s predchozim id: " + crrdt.Count);
                if (crrdt.Count == 1)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zahajovani korekce dotaz");
                    //if (DialogResult.No == FlexibleMessageBox.Show(this, "Zahájit korekci '" + crrdt[0].desc + "'?", "Korekce", MessageBoxButtons.YesNo))
                    //    return;
                    DialogResult dr = FlexibleMessageBox.Show(this, "Zahájit korekci '" + crrdt[0].desc + "'?", "Korekce", MessageBoxButtons.YesNo);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zahajovani korekce dotaz konec " + dr.ToString());
                    if (dr == DialogResult.No)
                        return;
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Tvorba productionRow START");
                    Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                    productionRow.TIMECRID = crrdt[0].id;   // načtení id korekce
                    //_productionRow.TIMECOR = Convert.ToSingle(tsDelka.TotalMinutes);
                    //_productionRow.TIMECORSTOP = dtStop;
                    DateTime dtnow = DateTime.Now;
                    productionRow.TIMECORSTART = dtnow;
                    productionRow.dateeve = dtnow;

                    //productionRow.CountEntries = rowvpp.CountEntries;
                    //productionRow.BarcodeP = rowvpp.BarcodeP;
                    //productionRow.dateeve = DateTime.Now;
                    productionRow.description = string.Empty;
                    productionRow.GUID = Guid.NewGuid();
                    //productionRow.ITEMNMBR = rowvpp.ITEMNMBR;
                    //productionRow.ITEMTYPE = rowvpp.ITEMTYPE;
                    //if (!rowvpp.IsITEMMJNull())
                    //    productionRow.ITEMMJ = rowvpp.ITEMMJ;
                    productionRow.loginid = Settings.LastProductionUserID;
                    productionRow.CORRGUID = Guid.NewGuid();
                    //productionRow.machineid = idmachine.id;
                    //productionRow.ORD = rowvpp.ORD;
                    productionRow.qty = 0; // pocetOdvedeno;
                    productionRow.qtyReal = 0; // pocetOdvedeno;
                    //productionRow.QTYPACK = rowvpp.QTYPACK;
                    //if (!rowvpp.IsQTYPACKMJNull())
                    //    productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
                    //productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
                    //productionRow.TIMEMODE = rowvpp.TIMEMODE;
                    //productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

                    productionRow.UserID = idpracovnik.id;
                    productionRow.TermID = Settings.TerminalID;
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Tvorba productionRow END");

                    dsV.Production.AddProductionRow(productionRow);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Pridan productionRow do Dsv");
                    //pta.Connection.Open();
                    //tapro.Update(productionRow);
                    //pta.Connection.Close();
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukladani ProductionRow do databaze Production START");
                    Data.DatabaseActions.InsertProduction(productionRow);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukladani ProductionRow do databaze Production END");
                    if (Settings.ModulPovolitPrehledOdvodu)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukladani ProductionRow do databaze ProductionHist START");
                        // pridani do historie
                        productionRow.SetAdded();
                        //taprohist.Connection.Open();
                        //taprohist.Update(productionRow);
                        //taprohist.Connection.Close();
                        Data.DatabaseActions.InsertProductionHistory(productionRow);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukladani ProductionRow do databaze ProductionHist END");
                    }
                    this.buttonZahajeniPrestavky.Enabled = false;
                    this.buttonKonecPrestavky.Enabled = true;
                }
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "zahajeni korekce prestavka END");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformZahajeniPrestavky, odvadeni dialog" +  " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformZahajeniPrestavky, odvadeni dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }
            if (Settings.OdvadeniOdhlasitUzivatelePriZahajeniPrestavky)
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "PerformZahajeniPrestavky END - Settings.OdvadeniOdhlasitUzivatelePriZahajeniPrestavky is true");
                ScannerStop();
                DialogResult = DialogResult.Retry;
            }
            else
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "PerformZahajeniPrestavky END - Settings.OdvadeniOdhlasitUzivatelePriZahajeniPrestavky is false");
                ScannerStop();
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Ukončení korekce: "Přestávka"
        /// </summary>
        public void PerformKonecPrestavky()
        {
            // 1) ukončení korekce
            // 2) přechod do volby pracovníka (podle volby v nastavení)
            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter tapro = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //tapro.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf));
            //Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter tacor = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter();
            //tacor.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf));
            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter taprohist = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //taprohist.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfHist));

            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba dsV = new Fask.SQLiteDBs.DataSets.Vyroba();
                // načtení dat podle ID uživatele a kde je ID Machine null
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdtAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);
                pdt.PrimaryKey = new DataColumn[] { pdt.GUIDColumn };
                pdtAll.PrimaryKey = new DataColumn[] { pdtAll.GUIDColumn };
                try
                {
                    //WebServiceVyroba.VyrobaDataSet dsW = vyrobaS.Production_OpenedCorrection(idpracovnik.id, string.Empty);
                    //WebServiceVyroba.VyrobaDataSet dsWAll = vyrobaS.Production_OpenedCorrection(idpracovnik.id, idmachine.id);
                    WebServiceVyroba.VyrobaDataSet dsW = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, string.Empty);
                    WebServiceVyroba.VyrobaDataSet dsWAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, idmachine.id);

                    dsW.Production.PrimaryKey = new DataColumn[] { dsW.Production.GUIDColumn };
                    dsWAll.Production.PrimaryKey = new DataColumn[] { dsWAll.Production.GUIDColumn };                    

                    pdt.Merge(dsW.Production, false, MissingSchemaAction.Ignore);
                    pdt.Merge(dsWAll.Production, false, MissingSchemaAction.Ignore);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //    return;                    
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformKonecPrestavky, odvadeni dotaz" +  " Fask.Vyroba_P.OdvadeniNadop");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformKonecPrestavky, odvadeni dotaz konec " + dr.ToString() +  " Fask.Vyroba_P.OdvadeniNadop");
                        if(dr == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }

                pdt.Merge(pdtAll, false, MissingSchemaAction.Ignore);                

                // seřazení dat uživatele podle datumu
                var prowsdesc = pdt.OrderByDescending(p => p.dateeve);
                if (prowsdesc.Count() > 0)
                { // je otevrena korekce ...
                    var prowcorrection = prowsdesc.First();
                    if (!prowcorrection.IsTIMECRIDNull()
                        && !prowcorrection.IsTIMECORSTARTNull()
                        && prowcorrection.IsTIMECORSTOPNull()
                        )
                    {
                        var correctionrow = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(prowsdesc.First().TIMECRID).First(); ;
                        if (correctionrow.id == Settings.PrestavkaID)
                        {
                            //if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo))
                            //{
                            //    return;
                            //}
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncit existujici korekci dotaz" + " Fask.Vyroba_P.OdvadeniNadop.PerformKonecPrestavky");
                            DialogResult dr = FlexibleMessageBox.Show(this, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo);
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Ukoncit existujici korekci dotaz konec " + dr.ToString() +  " Fask.Vyroba_P.OdvadeniNadop.PerformKonecPrestavky");
                            if(dr == DialogResult.No)
                            {
                                return;
                            }

                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                            productionRow.description = prowcorrection.description;
                            productionRow.GUID = Guid.NewGuid();
                            productionRow.loginid = Settings.LastProductionUserID;
                            productionRow.qty = prowcorrection.qty;
                            productionRow.qtyReal = prowcorrection.qtyReal;
                            productionRow.UserID = idpracovnik.id;
                            if (!prowcorrection.IsmachineidNull())
                                productionRow.machineid = prowcorrection.machineid;
                            productionRow.TermID = Settings.TerminalID;
                            if (!prowcorrection.IsCORRGUIDNull())
                                productionRow.CORRGUID = prowcorrection.CORRGUID;
                            productionRow.TIMECRID = prowcorrection.TIMECRID;
                            productionRow.TIMECORSTART = prowcorrection.TIMECORSTART;
                            productionRow.TIMECORSTOP = DateTime.Now;
                            productionRow.TIMECOR = (float)(productionRow.TIMECORSTOP - productionRow.TIMECORSTART).TotalMinutes;
                            productionRow.dateeve = DateTime.Now;

                            dsV.Production.AddProductionRow(productionRow);
                            //tapro.Update(productionRow);
                            Data.DatabaseActions.InsertProduction(productionRow);
                            // pridani do historie
                            if (Settings.ModulPovolitPrehledOdvodu)
                            {
                                productionRow.SetAdded();
                                //taprohist.Update(productionRow);
                                Data.DatabaseActions.InsertProductionHistory(productionRow);
                            }
                            this.buttonZahajeniPrestavky.Enabled = true;
                            this.buttonKonecPrestavky.Enabled = false;
                        }  
                    }
                }      
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Main Exception PerformKonecPrestavky, odvadeni dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Main Exception PerformKonecPrestavky, odvadeni dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }
            if (Settings.OdvadeniOdhlasitUzivatelePriUkonceniPrestavky)
            {
                ScannerStop();
                DialogResult = DialogResult.Retry;
            }
            else
            {
                ScannerStop();
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Návrat poslední aktivní korekce (pokud je aktivní).
        /// </summary>
        /// <returns>Popis (desc) poslední korekce, pokud není tak null</returns>
        public Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow getLastCorrectionRow()
        {
            //Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter tapro = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
            //tapro.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf));
            //Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter tacor = new Fask.Vyroba_P.Data.VyrobaCEDataSetTableAdapters.CorrectsTableAdapter();
            //tacor.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf));
            
            try
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "getLastCorrectionRow() START");
                // načtení dat podle ID uživatele a kde je ID Machine null
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdtAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);
                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "get data from server start");
                    //WebServiceVyroba.VyrobaDataSet dsW = vyrobaS.Production_OpenedCorrection(idpracovnik.id, string.Empty);
                    //WebServiceVyroba.VyrobaDataSet dsWAll = vyrobaS.Production_OpenedCorrection(idpracovnik.id, idmachine.id);
                    WebServiceVyroba.VyrobaDataSet dsW = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, string.Empty);
                    WebServiceVyroba.VyrobaDataSet dsWAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, idmachine.id);
                    pdt.Merge(dsW.Production, true, MissingSchemaAction.Ignore);
                    pdt.Merge(dsWAll.Production, true, MissingSchemaAction.Ignore);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "dsW.Production.count is " + dsW.Production.Count);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "dsWAll.Production is " + dsWAll.Production.Count);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //    return null;
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz" + " Fask.Vyroba_P.OdvadeniNadop.GetLastCorrectionRow");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz konec " + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop.GetLastCorrectionRow");
                        if(dr == DialogResult.Cancel)
                            return null;
                    }
                }
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "pdtAll merge");
                pdt.Merge(pdtAll, true, MissingSchemaAction.Ignore);


                // seřazení dat uživatele podle datumu
                var prowsdesc = pdt.OrderByDescending(p => p.dateeve);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "prowsdesc.Count()" + prowsdesc.Count());
                if (prowsdesc.Count() > 0)
                { // je otevrena korekce ...

                    var prowcorrection = prowsdesc.First();
                    if (!prowcorrection.IsTIMECRIDNull()
                        && !prowcorrection.IsTIMECORSTARTNull()
                        && prowcorrection.IsTIMECORSTOPNull()
                        )
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "prowsdesc row find");
                        var correctionrow = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(prowsdesc.First().TIMECRID).First();
                        // návrat jména korekce
                        //return correctionrow.desc.Trim();
                        return correctionrow;
                    }
                }

                this.buttonZahajeniPrestavky.Enabled = true;
                this.buttonKonecPrestavky.Enabled = false;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception getLastCorrectionRow dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception getLastCorrectionRow dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
            }
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "getLastCorrectionRow() END, return null");
            return null;
        }

        /// <summary>
        /// Odchod z pracoviště. Ukončení korekcí, zakázek a odhlášení.
        /// </summary>
        public void PerformOdchodZPRacoviste()
        {
            // 1) ukončení korekce (pokud existuje)
            // 2) ukončení zakázky/zakázek (pokud existují)
            // 3) přechod do volby pracovníka (podle volby v nastavení)

            Fask.SQLiteDBs.DataSets.Vyroba dsV = new Fask.SQLiteDBs.DataSets.Vyroba();
            try
            {
                //if (FlexibleMessageBox.Show("Odhlásit pracovníka '" + idpracovnik.firstname + " " + idpracovnik.surname + "'" + 
                //    "\na ukončit jeho veškeré zakázky/korekce?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                //            == DialogResult.No)
                //    return;
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Odhlasit pracovnika, PerformOdchodZPracoviste dotaz" +  " Fask.Vyroba_P.OdvadeniNadop");
                DialogResult dr = FlexibleMessageBox.Show(this, "Odhlásit pracovníka '" + idpracovnik.firstname + " " + idpracovnik.surname + "'" +
                    "\na ukončit jeho veškeré zakázky/korekce?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Odhlasit pracovnika, PerformOdchodZPracoviste dotaz konec " + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                if(dr == DialogResult.No)
                    return;

                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdtAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.GetDataByUserIDMachineID_Production(idpracovnik.id, idmachine.id);
                pdt.PrimaryKey = new DataColumn[] { pdt.GUIDColumn };
                pdtAll.PrimaryKey = new DataColumn[] { pdtAll.GUIDColumn };
                try
                {
                    //WebServiceVyroba.VyrobaDataSet dsW = vyrobaS.Production_OpenedCorrection(idpracovnik.id, string.Empty);
                    //WebServiceVyroba.VyrobaDataSet dsWAll = vyrobaS.Production_OpenedCorrection(idpracovnik.id, idmachine.id);
                    WebServiceVyroba.VyrobaDataSet dsW = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, string.Empty);
                    WebServiceVyroba.VyrobaDataSet dsWAll = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedCorrection2(idpracovnik.id, idmachine.id);

                    dsW.Production.PrimaryKey = new DataColumn[] { dsW.Production.GUIDColumn };
                    dsWAll.Production.PrimaryKey = new DataColumn[] { dsWAll.Production.GUIDColumn };

                    pdt.Merge(dsW.Production, false, MissingSchemaAction.Ignore);
                    pdt.Merge(dsWAll.Production, false, MissingSchemaAction.Ignore);
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //    return;
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                        dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dialog konec" + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                        if(dr == DialogResult.Cancel)
                            return;
                    }
                }

                pdt.Merge(pdtAll, false, MissingSchemaAction.Ignore);               

                var prowsdesc = pdt.OrderByDescending(p => p.dateeve);
                if (prowsdesc.Count() > 0)
                { // je otevrena korekce ...
                    var prowcorrection = prowsdesc.First();

                    if (!prowcorrection.IsTIMECRIDNull()
                        && !prowcorrection.IsTIMECORSTARTNull()
                        && prowcorrection.IsTIMECORSTOPNull()
                        )
                    {

                        var correctionrow = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(prowsdesc.First().TIMECRID).First(); ;
                        //if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukončit existující korekci '" + correctionrow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo))
                        //{
                        //    return;
                        //}

                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                        productionRow.description = prowcorrection.description;
                        productionRow.GUID = Guid.NewGuid();
                        productionRow.loginid = Settings.LastProductionUserID;
                        productionRow.qty = prowcorrection.qty;
                        productionRow.qtyReal = prowcorrection.qtyReal;
                        productionRow.UserID = idpracovnik.id;
                        if (!prowcorrection.IsmachineidNull())
                            productionRow.machineid = prowcorrection.machineid;
                        productionRow.TermID = Settings.TerminalID;
                        if (!prowcorrection.IsCORRGUIDNull())
                            productionRow.CORRGUID = prowcorrection.CORRGUID;
                        productionRow.TIMECRID = prowcorrection.TIMECRID;
                        productionRow.TIMECORSTART = prowcorrection.TIMECORSTART;
                        productionRow.TIMECORSTOP = DateTime.Now;
                        productionRow.TIMECOR = (float)(productionRow.TIMECORSTOP - productionRow.TIMECORSTART).TotalMinutes;
                        productionRow.dateeve = DateTime.Now;

                        dsV.Production.AddProductionRow(productionRow);
                        //tapro.Update(productionRow);
                        Data.DatabaseActions.InsertProduction(productionRow);
                        // pridani do historie
                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            productionRow.SetAdded();
                            //taprohist.Update(productionRow);
                            Data.DatabaseActions.InsertProductionHistory(productionRow);
                        }

                        if (correctionrow.id == Settings.PrestavkaID)
                        {
                            this.buttonKonecPrestavky.Enabled = false;
                            this.buttonZahajeniPrestavky.Enabled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste dialog" +  " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }

            // nacteni produkce z lokalu a 1 ze serveru...
            try
            {

                Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__InternalState_PRD.FillByUserIDMachineID_Production(dsV.Production, idpracovnik.id, idmachine.id);

                WebServiceVyroba.VyrobaDataSet dsVweb = null;

                try
                {
                    //dsVweb = vyrobaS.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
                    //dsVweb = vyrobaS.Production_OpenedProduction(idpracovnik.id, idmachine.id);
                    dsVweb = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.CustomvyrobaServis.Production_OpenedProduction2(idpracovnik.id, idmachine.id);
                }
                catch (Exception exWeb)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                    //if (Settings.ZobrazovatChybySynchronizaceDatabaze && DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu zakázky ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    //{
                    //    return;
                    //}
                    if (Settings.ZobrazovatChybySynchronizaceDatabaze)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz" + " Fask.Vyroba_P.OdvadeniNadop");
                        DialogResult dr = FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekcí ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nezdařilo se zjištění stavu korekcí ze serveru... Pokračovat? dotaz konec" + dr.ToString() + " Fask.Vyroba_P.OdvadeniNadop");
                        if (dr == DialogResult.Cancel)
                            return;
                    }
                }

                if (dsVweb != null)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Merge produkce" + " Fask.Vyroba_P.OdvadeniNadop");
                    // nastaveni primarnich klicu kvuli merge
                    dsV.Production.PrimaryKey = new DataColumn[] { dsV.Production.GUIDColumn };
                    dsVweb.Production.PrimaryKey = new DataColumn[] { dsVweb.Production.GUIDColumn };

                    // Ted mam platna data v dsV.Production
                    dsV.Production.Merge(dsVweb.Production, false, MissingSchemaAction.Ignore);
                }

            }
            catch (Exception ex2)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex2);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste2 dialog" + " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex2.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste2 dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }

            try
            {
                // 2. Kontrola zda je otevrena jina zakazka
                if (dsV.Production.Count > 0)
                {
                    var pDateeveOrderbyDesc = dsV.Production.OrderByDescending(x => x.dateeve);
                    var xRow = pDateeveOrderbyDesc.First();
                    if (//xRow.SOPNUMBE != zakazka.SOPNUMBE
                        //&& 
                        !xRow.IsTIMESTARTNull()
                        &&
                        xRow.IsTIMESTOPNull()
                        )
                    {
                        DialogResult drukoncit = DialogResult.None;
                        if(xRow.machineid.Trim() != idmachine.id.Trim())
                        {
                            // zakazka byla zahajena na jinem stroji ...
                            if (Settings.PovolitUkonceniZJinehoStroje)
                            {
                                // je povoleno ukonceni
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. Ukončit zakázku(y)? dotaz" + " Fask.Vyroba_P.OdvadeniNadop");
                                drukoncit = FlexibleMessageBox.Show(this, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. \nUkončit zakázku(y)?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Chystáte se ukončit zakázku(y), která byla zahájena na jiném stroji. Ukončit zakázku(y)? dotaz konec " + drukoncit.ToString()  + " Fask.Vyroba_P.OdvadeniNadop");
                                if (drukoncit == DialogResult.No)
                                    return;
                            }
                            else
                            {
                                // chyba, neni povoleno ukonceni
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. Není možné pokračovat. dotaz" + " Fask.Vyroba_P.OdvadeniNadop");
                                FlexibleMessageBox.Show(this, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. \nNení možné pokračovat.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Nebyla ukončena zakázka(y) na stroji '" + xRow.machineid.Trim() + "'. Není možné pokračovat. dotaz konec" + " Fask.Vyroba_P.OdvadeniNadop");
                                return;
                            }
                        }

                        if (xRow.IsSOUBEHGUIDNull())
                        { // neni soubeh, tak ukoncit jen normalne jednu
                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nP = dsV.Production.NewProductionRow();
                            NewProductionPrepare(nP, null);
                            nP.machineid = xRow.IsmachineidNull() ? null : xRow.machineid;
                            nP.CountEntries = xRow.CountEntries;
                            nP.SOPNUMBE = xRow.SOPNUMBE;
                            nP.TIMESTART = xRow.TIMESTART;
                            nP.TIMESTOP = DateTime.Now;
                            nP.operationid = xRow.operationid;
                            dsV.Production.AddProductionRow(nP);                            
                        }
                        else
                        { // je soubeh, tak ukoncit vice ... 
                            List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> resSoubeh = dsV.Production.Where(x => !x.IsSOUBEHGUIDNull() && (x.SOUBEHGUID == xRow.SOUBEHGUID)).ToList();
                            //List<Data.VyrobaCEDataSet.ProductionRow> resSoubeh = dsV.Production.Where(x => !x.IsSOUBEHGUIDNull() && (x.SOUBEHGUID == xRow.SOUBEHGUID)).GroupBy(test => test.GUID).Select(grp => grp.First()).ToList();
                            DateTime dtnow = DateTime.Now;
                            foreach (var item in resSoubeh)
                            {
                                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow nP = dsV.Production.NewProductionRow();
                                NewProductionPrepare(nP, null);
                                nP.machineid = xRow.IsmachineidNull() ? null : xRow.machineid;
                                nP.CountEntries = item.CountEntries;
                                nP.SOPNUMBE = item.SOPNUMBE;
                                nP.TIMESTART = item.TIMESTART;
                                nP.TIMESTOP = dtnow;
                                nP.operationid = item.operationid;
                                nP.SOUBEHGUID = item.SOUBEHGUID;
                                dsV.Production.AddProductionRow(nP);
                            }
                        }
                        //Update production
                        var drAdded = dsV.Production.Select(null, null, DataViewRowState.Added);
                        //tapro.Update(dsV.Production);
                        Data.DatabaseActions.insertProductionDataTable(dsV.Production);
                        // old
                        //foreach (Data.VyrobaCEDataSet.ProductionRow item in dsV.Production)
                        //{
                        //    Data.DatabaseActions.InsertProduction(item);
                        //}

                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            // pridani do historie
                            foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow item in drAdded)
                            {
                                item.SetAdded();
                                Data.DatabaseActions.InsertProductionHistory(item);
                            }
                            //taprohist.Update(drAdded);
                            //Data.DatabaseActions.InsertProductionHistory(
                        }
                    }

                    //if (xRow.SOPNUMBE == zakazka.SOPNUMBE)
                    //{ // konec doslo k ukonceni zahajene zakazky ...
                    //    //Odvod dokoncen => zastavit scanner a konec
                    //    ScannerStop();
                    //    DialogResult = DialogResult.OK;
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste3 dialog" +  " Fask.Vyroba_P.OdvadeniNadop");
                FlexibleMessageBox.Show(this, ex.Message);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Exception PerformOdchodZPracoviste3 dialog konec" + " Fask.Vyroba_P.OdvadeniNadop");
                return;
            }

            // vrací retry kvůli opětovnému loginu
            if (Settings.OdvadeniOdhlasitUzivatelePriOdchoduZPracoviste)
            {
                ScannerStop();
                DialogResult = DialogResult.Retry;
            }
            else
            {
                ScannerStop();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
