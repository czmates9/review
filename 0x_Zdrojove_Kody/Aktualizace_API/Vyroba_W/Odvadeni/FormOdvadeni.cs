using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Vyroba_W.Forms;
using Fask.Vyroba_W.Extensions;
using System.IO;
using System.Linq;
using Fask.Parsing.Codes;

using Fask.Vyroba_W.ServerAccess;
using Fask.SQLiteDBs.DataSets;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormOdvadeni : Form
	{

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

		#region Parametry

		TIMEMODES odvodProcessedMode = TIMEMODES.Unknown;
		TIMESTATE odvodProcessedState = TIMESTATE.Unknown;

		Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik = null;
		Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine = null;
		
		#endregion

		#region C'tor a eventy formu

		/// <summary>
		/// Konstruktor
		/// </summary>
		public FormOdvadeni()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="idpracovnik"></param>
		/// <param name="idmachine"></param>
		public FormOdvadeni(Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik, Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine)
			: this()
		{
			this.idpracovnik = idpracovnik;
			this.idmachine = idmachine;
		}

		private void FormOdvadeni_KeyDown(object sender, KeyEventArgs e)
		{
			Handle_KeyDown(sender, e);
		}

		private void FormOdvadeni_Load(object sender, EventArgs e)
		{
			this.Size = Screen.PrimaryScreen.WorkingArea.Size;
			panelButtons_Resize(null, null);
			textBoxVyrobniOperaceFocusAll();

			if (Globals.Zakazka != null)
			{
				if (!string.IsNullOrEmpty(Globals.Zakazka.SOPNUMBE))
					this.Text = "Odvádìní výroby  Pøíkaz: '" + Globals.Zakazka.SOPNUMBE.Trim() + "'";
			}

		}

		#endregion

		#region Key Down handler

		public void Handle_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Shift && !e.Control && !e.Alt)
			{
				if (e.KeyCode == Keys.Escape)
				{
					PerformCancel();
				}
				else if (e.KeyCode == Keys.Enter)
				{
					this.BeginInvoke((Action)(() => { this.PerformOK(null); }));
				}
				else
					return;
			}
			else
				return;

			e.Handled = true;
		}

		#endregion

		#region Scanner

        private bool scannerenabled = true;

        private void ScannerFinalize()
        {
            scannerenabled = false;
            ScannerStop();
        }

        private void ScannerStart()
        {
            if (!scannerenabled)
                return;

            try
            {
                FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
				return;

			string CKout = e.BarcodeData.Trim();
			BaseCode code = null;

			code = Parsing.ParsingFactory.Parse(CKout, Settings.Parsing_Config);

			if (code is WeightCode)
				CKout = ((WeightCode)code).id;
			else if (code is Fask.Parsing.Codes.WeightCode_12)
				CKout = ((WeightCode_12)code).id; 
			

			this.textBoxVyrobniOperace.Text = CKout;
            textBoxVyrobniOperaceFocusAll();

            //this.PerformOK();
			this.BeginInvoke((Action)(() => { this.PerformOK(code); }));
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
		}

		#endregion

		#region Perform Metody

		public void PerformCancel()
        {
            ScannerFinalize();
            DialogResult = DialogResult.Cancel;
        }

		public void PerformOK(BaseCode code)
        {
            this.odvodProcessedMode = TIMEMODES.Unknown;
            this.odvodProcessedState = TIMESTATE.Unknown;

			Fask.SQLiteDBs.DataSets.Vyroba vyrobaDS = new Fask.SQLiteDBs.DataSets.Vyroba();
			Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt = new Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable();

            // Seznam produkce pro ulozeni ...
			List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow> productionRowList = new List<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ScannerStop();

                // Check korekce neni treba, protoze se aktualne vkladaji korekce vzdy parove ...
                // TODO : pokud se zmeni zpusob zadavani korekci, tak se musi kontrolovat ... 
                //if (CheckCorrectionsOpenedNoProduction())
                //{
                //    return;
                //}

                //Dohledani vyrobni operace podle zadaneho caroveho kodu operace
				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter vppta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
				//vppta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                string cisloOperace = this.textBoxVyrobniOperace.Text;
                //Data.VyrobaCEDataSet.CZPRO_VPPDataTable dtvpp = vppta.GetDataByBarcodeP(cisloOperace);
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillByBarcodeP_CZPRO_VPP(vyrobaDS.CZPRO_VPP, cisloOperace);
                
                // 15.1.2019 - dotazeni informace online ze serveru ... 
                Fask.Vyroba_W.WebServiceVyroba.ProductionState productionStateEnabled_Online = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Unknown;
                Methods.Vyroba_Online.CheckOperation( vyrobaDS, cisloOperace, out productionStateEnabled_Online);

                var vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => false);
                if (Globals.Zakazka == null)
                {
                    // TODO : vyber odpovidajici/pozadovane operace ... ???

                    vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => true);
                }
                else
                {
                    vyrobnioperace = vyrobaDS.CZPRO_VPP.Where(x => x.CountEntries == Globals.Zakazka.CountEntries && x.SOPNUMBE == Globals.Zakazka.SOPNUMBE);
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
                    using (FormOperaceVyber fpv = new FormOperaceVyber())
                    {
                        fpv.Owner = this;
                        fpv.Operace = vyrobnioperace.ToList();
                        if (DialogResult.Cancel == fpv.ShowDialog())
                            return;
                        rowvpp = fpv.OperaceVybrana;
                    }
                }

                // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
                // 6.11.2014 -> telefonicky pozadavek JaS ???
                // 17.4.2015 -> uplne odstraneni kontroly na timestop (melo byt != misto ==)
                //if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
                //{
                if (Settings.Odvadeni_CasNecinnosti)
                {
                    //Posledni datum a cas akce uzivatele
                    //DateTime? userLastAction = UserLastAction();
                    DateTime? userLastAction = Data.DatabaseActions.UserLastAction(idpracovnik.id);
                    if (userLastAction != null && userLastAction < DateTime.Now - Settings.Production_UserMaxTimeSpanNoAction)
                    { // uzivatel neco delal dele nez je nastavena necinnost, => musi zadat korekci mimo vyrobu ???
                        DialogResult drNecinnost = MessageBox.Show(
                            "Neèinnost trvala déle než " + Settings.Production_UserMaxTimeSpanNoAction.ToStringHHmm() + "[HH:mm]" + "\n" +
                            "Celkem " + (DateTime.Now - userLastAction.Value).ToStringHHmm() + "[HH:mm]",
                            "Pokraèovat dále nebo ukonèit pro zadání korekce mimo výrobu?",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Exclamation,
                             MessageBoxDefaultButton.Button1
                            );
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
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.FillBySOPNUMBE_CZPRO_VPH(vyrobaDS.CZPRO_VPH, rowvpp.SOPNUMBE);

				Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph = vyrobaDS.CZPRO_VPH[0];

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
				//pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                //Dohledani poctu odvedenych kusu z production tabulky k poslednimu datu modifikace zaznamu v tabulce VPP
                decimal? qtyodvedeno = null;
                if (!Settings.Vyroba_Online)
                {
					qtyodvedeno = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.QTY_Production(rowvpp.CountEntries, rowvpp.SOPNUMBE,rowvpp.ORD, rowvpp.ITEMNMBR, rowvpp.ITEMTYPE,rowvpp.QTYPACK, rowvpp.LSTMod, rowvpp.BarcodeP);
                }

                //Pokud je pocet odvedeno vetsi nez je pozadovano, pak zobrazi informaci o tom zda pokracovat a preplnit vyrobu(mohou byt zmetky)
                //Rezijni zakazka ma nulove mnozstvi, v pripade potvrzeni 0 se jedna o rezijni zakazku nebo korekci pro opravu vyroby
                //if ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) >= rowvpp.QTYSHPPD)
                if ((rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)) > rowvpp.QTYSHPPD) //pozadavek na zadani nuloveho mnozstvi rezijni zakazky
                {
                    Cursor.Current = Cursors.Default;
                    MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChimes));
                    if (MessageBox.Show("Položka již byla odvedena.\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                        == DialogResult.No)
                        return;
                    //throw new Exception("Položka již byla odvedena");
                    Cursor.Current = Cursors.WaitCursor;
                }

                //Dohledani posledni 2 akci production
				Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dtL_LastProductionUserMachine = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserIDMachineID_R2_Production(idpracovnik.id, idmachine.id);

                // Zjisteni posledni akce uzivatele...
                WebServiceVyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
                if (!Settings.Vyroba_Online)
                {
                    // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
                    // 6.11.2014 -> telefonicky pozadavek JaS ???
                    // 17.4.2015 -> uplne odstraneni kontroly na timestop (melo byt != misto ==)
                    //if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
                    //{
                    try
                    {
						dsR_LastProductionUserMachine = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
                    }
                    catch (Exception ews)
                    {
						Fask.Logging.Log.Write(ews);
                    }
                    //nepovedlo se stazeni ... 
                    if (dsR_LastProductionUserMachine == null)
                    { // TODO : osetrit nejak ... 
                        if (DialogResult.No == MessageBox.Show("Nezdarilo se zjisteni posledniho stavu uzivatele ze serveru. Pokracovat?", "Online stav", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
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
                if (!Settings.Vyroba_Online)
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
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Unknown:
                            lActualTimeState = TIMESTATE.Unknown;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Priprava_Start:
                            lActualTimeState = TIMESTATE.Nezahajeno;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Priprava_Stop:
                            lActualTimeState = TIMESTATE.Priprava_Zahajena;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Odvod_Start:
                            if (lActualTimeMode == TIMEMODES.Stop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else if (lActualTimeMode == TIMEMODES.StartStop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else if (lActualTimeMode == TIMEMODES.StartStartStop)
                                lActualTimeState = TIMESTATE.Priprava_Zahajena;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Odvod_Stop:
                            if (lActualTimeMode == TIMEMODES.Stop)
                                lActualTimeState = TIMESTATE.Nezahajeno;
                            else
                                lActualTimeState = TIMESTATE.Odvod_Zahajen;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Korekce_Start:
                            lActualTimeState = TIMESTATE.Odvod_Zahajen;
                            break;
                        case Fask.Vyroba_W.WebServiceVyroba.ProductionState.Korekce_Stop:
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

                if (lActualTimeMode == TIMEMODES.Stop)
                {
                    if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                    {
                        MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí VÝROBA\n" +
                            (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                            (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                            (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                            , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                    {
                        MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí PØÍPRAVA\n" +
                            (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                            (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                            (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                            , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    DialogResult drStop = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, psdt, code);
                    if (drStop == DialogResult.Cancel)
                        return;
                }
                else if (lActualTimeMode == TIMEMODES.StartStop)
                {
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni operace
                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí VÝROBA\n" +
                                (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí PØÍPRAVA\n" +
                                (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        DialogResult drStartOdvod = StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp);
                        if (drStartOdvod == DialogResult.Cancel)
                            return;

                        odvodProcessedMode = lActualTimeMode;
                        odvodProcessedState = TIMESTATE.Odvod_Zahajen;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Zahajen)
                    { // provest odvod operace
                        DialogResult drStopOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, psdt, code);
                        if (drStopOdvod == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod
                        DialogResult drStopOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, psdt, code);
                        if (drStopOdvod == DialogResult.Cancel)
                            return;
                    }
                    else
                    { // nedefinovany stav pro tento mod
						Logging.Log.Write("Nedefinovany stav VÝROBY (Start/Stop)");
                        return;
                    }
                }
                else if (lActualTimeMode == TIMEMODES.StartStartStop)
                {
                    if (lActualTimeState == TIMESTATE.Nezahajeno)
                    { // provest zahajeni pripravy

                        if (lLastTimeState == TIMESTATE.Odvod_Zahajen)
                        {
                            MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí VÝROBA\n" +
                                (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        else if (lLastTimeState == TIMESTATE.Priprava_Zahajena)
                        {
                            MessageBox.Show("Nelze pokraèovat.\nNebyla dokonèena pøedchozí PØÍPRAVA\n" +
                                (lastProductionRowTmp.IsSOPNUMBENull() ? "?" : lastProductionRowTmp.SOPNUMBE) + "\n" +
                                (lastProductionRowTmp.IsITEMNMBRNull() ? "-" : lastProductionRowTmp.ITEMNMBR) + "\n" +
                                (lastProductionRowTmp.IsBarcodePNull() ? "-" : lastProductionRowTmp.BarcodeP)
                                , "Odvod zahájen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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

                        // automaticke zahajeni vyroby ...
                        if (Settings.StopPripravaStartVyrobaIhned)
                        {
							Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowNext = productionDataSet.Production.NewProductionRow();
                            DialogResult drOdvod = StartOdvod(rowvph, rowvpp, productionRowNext, productionRow);
                            if (drOdvod == DialogResult.OK) //ok zahajena vyroba
                                productionRowList.Add(productionRowNext);
                            else if (drOdvod == DialogResult.Cancel)
                            { // vyroba nezahajena, ale pokracuje se ulozenim, protoze se musi ulozit ukonceni pripravy...
                            }
                        }
                    }
                    else if (lActualTimeState == TIMESTATE.Priprava_Dokoncena)
                    { // provest zahajeni odvodu nebo znovu pripravu ???
                        DialogResult drQ = MessageBox.Show("Zahájit VÝROBU(Ano) nebo opakovat PØÍPRAVU(Ne)?", "Výroba/Pøíprava", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            DialogResult drOdvod = StartOdvod(rowvph, rowvpp, productionRow, lastProductionRowTmp);
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
                        DialogResult drOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, psdt, code);
                        if (drOdvod == DialogResult.Cancel)
                            return;
                    }
                    else if (lActualTimeState == TIMESTATE.Odvod_Dokoncen)
                    { // provest dalsi odvod nebo zacatek noveho odvodu nebo pripravu?
                        DialogResult drQ = MessageBox.Show("Pokraèovat odvodem VÝROBY(Ano) nebo provést opìtovné zahájení PØÍPRAVY(Ne)?", "Výroba/Pøíprava", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (drQ == DialogResult.Cancel)
                            return;
                        else if (drQ == DialogResult.Yes)
                        {
                            DialogResult drOdvod = StopOdvod(qtyodvedeno, rowvph, rowvpp, productionRow, lastProductionRowTmp, out pocetOdvedeno, psdt, code);
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
						Logging.Log.Write("Nedefinovany stav vyroby (Start/Stop)");
                        return;
                    }
                }
                else
                { // jiny stav neni definovan ... 
					Logging.Log.Write("Nedefinovany stav vyroby...");
                    return;
                }

                #endregion

                Cursor.Current = Cursors.WaitCursor;

                //pridani do production

				foreach (Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow pRow in productionRowList)
                {
                    productionDataSet.Production.AddProductionRow(pRow);

                    //Online funkce pro zapis vyroby na strane partnera ... 
                    // CHECK : Pokud bude vice zaznamu a nekde uprostred to neprojde, tak co se pak bude dit ???
                    //          => vyroba a korekce v nich ...
                    if (!Methods.Vyroba_Online.Online_Vyroba_Zapis_Odvod(pRow))
                        return;

                    //Ulozeni odvedenych dat do db
                    //Provedeni rozpadu na zapsani korekce a vyroby
                    if (pRow.IsTIMECRIDNull())
                    {
                        //pta.Update(pRow);
                        //provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
                        {
                            #region old
                            //pta.Insert(
                            //    pRow.CountEntries,
                            //    pRow.SOPNUMBE,
                            //    pRow.ITEMNMBR,
                            //    pRow.ITEMTYPE,
                            //    pRow.ITEMMJ,
                            //    pRow.ORD,
                            //    pRow.TIMEPREP,
                            //    pRow.TIMEUNIT,
                            //    pRow.IsTIMESTARTNull() ? (DateTime?)null : pRow.TIMESTART,
                            //    pRow.IsTIMESTOPNull() ? (DateTime?)null : pRow.TIMESTOP,
                            //    pRow.IsTIMECORNull() ? (float?)null : pRow.TIMECOR,
                            //    pRow.IsTIMECRIDNull() ? (int?)null : pRow.TIMECRID,
                            //    pRow.loginid,
                            //    pRow.machineid,
                            //    pRow.dateeve,
                            //    pRow.qty,
                            //    pRow.qtyReal,
                            //    pRow.QTYPACK,
                            //    pRow.QTYPACKMJ,
                            //    pRow.description,
                            //    pRow.BarcodeP,
                            //    pRow.UserID,
                            //    pRow.TermID,
                            //    pRow.IsISOKNull() ? (DateTime?)null : pRow.ISOK,
                            //    pRow.GUID,
                            //    pRow.TIMEMODE,
                            //    pRow.IsTIMEPREPSTARTNull() ? (DateTime?)null : pRow.TIMEPREPSTART,
                            //    pRow.IsTIMEPREPSTOPNull() ? (DateTime?)null : pRow.TIMEPREPSTOP,
                            //    pRow.IsTIMECORSTARTNull()? (DateTime?)null : pRow.TIMECORSTART,
                            //    pRow.IsTIMECORSTOPNull() ? (DateTime?)null : pRow.TIMECORSTOP
                            //    //pRow.IsSKL_IDNull() ? null : pRow.SKL_ID,
                            //    //pRow.IsLOCNCODENull() ? null : pRow.LOCNCODE
                            //    );

                            //System.Data.SqlServerCe.SqlCeCommandBuilder scecbuilder = new System.Data.SqlServerCe.SqlCeCommandBuilder(pta._Adapter);
                            //var ssscommand =  scecbuilder.GetInsertCommand();
                            ////System.Data.SqlServerCe.SqlCeParameter[] a = new System.Data.SqlServerCe.SqlCeParameter[pta._Adapter.InsertCommand.Parameters.Count];
                            ////pta._Adapter.InsertCommand.Parameters.CopyTo(a, 0);
                            ////ssscommand.Parameters.AddRange(a);
                            //foreach (System.Data.SqlServerCe.SqlCeParameter p in ssscommand.Parameters)
                            //{
                            //    p.Value = pRow[p.SourceColumn];
                            //}
                            //pta.Insert();
                            //pta.Update(

                            #endregion

                            #region TaD test Insert ale OutOfMemoryException

                            //pta.Insert(
                            //    pRow.IsCountEntriesNull() ? (int?)null : pRow.CountEntries,
                            //    pRow.IsSOPNUMBENull() ? null : pRow.SOPNUMBE,
                            //    pRow.IsITEMNMBRNull() ? null : pRow.ITEMNMBR,
                            // pRow.IsITEMTYPENull() ? null : pRow.ITEMTYPE,
                            // pRow.IsITEMMJNull() ? null : pRow.ITEMMJ,
                            // pRow.IsORDNull() ? (int?)null : pRow.ORD,
                            // pRow.IsTIMEPREPNull() ? (float?)null : pRow.TIMEPREP,
                            // pRow.IsTIMEUNITNull() ? (float?)null : pRow.TIMEUNIT,
                            // pRow.IsTIMESTARTNull() ? (DateTime?)null : pRow.TIMESTART,
                            // pRow.IsTIMESTOPNull() ? (DateTime?)null : pRow.TIMESTOP,
                            // pRow.IsTIMECORNull() ? (float?)null : pRow.TIMECOR,
                            // pRow.IsTIMECRIDNull() ? (int?)null : pRow.TIMECRID,
                            // pRow.loginid,
                            // pRow.IsmachineidNull() ? null : pRow.machineid,
                            // pRow.dateeve,
                            // pRow.qty,
                            // pRow.qtyReal,
                            // pRow.IsQTYPACKNull() ? (decimal?)null : pRow.QTYPACK,
                            // pRow.IsQTYPACKMJNull() ? null : pRow.QTYPACKMJ,
                            // pRow.IsdescriptionNull() ? null : pRow.description,
                            // pRow.IsBarcodePNull() ? null : pRow.BarcodeP,
                            // pRow.UserID,
                            // pRow.TermID,
                            // pRow.IsISOKNull() ? (DateTime?)null : pRow.ISOK,
                            // pRow.GUID,
                            // pRow.IsTIMEMODENull() ? (int?)null : pRow.TIMEMODE ,
                            // pRow.IsTIMEPREPSTARTNull() ? (DateTime?)null : pRow.TIMEPREPSTART,
                            // pRow.IsTIMEPREPSTOPNull() ? (DateTime?)null : pRow.TIMEPREPSTOP,
                            // pRow.IsTIMECORSTARTNull() ? (DateTime?)null : pRow.TIMECORSTART,
                            // pRow.IsTIMECORSTOPNull() ? (DateTime?)null : pRow.TIMECORSTOP,
                            // pRow.IsoperationidNull() ? null : pRow.operationid,
                            // pRow.IsSOUBEHGUIDNull() ? (Guid?)null : pRow.SOUBEHGUID,
                            // pRow.IsCORRGUIDNull() ? (Guid?)null : pRow.CORRGUID,
                            // pRow.IsTIMECRIDTYPENull() ? (byte?)null : pRow.TIMECRIDTYPE,
                            // pRow.IsSKL_IDNull() ? null : pRow.SKL_ID,
                            // pRow.IsITEMDESCNull() ? null : pRow.ITEMDESC,
                            // pRow.IsLOCNCODENull() ? null : pRow.LOCNCODE
                            // );

                            #endregion


							#region ResourceSet

							//SqlCeConnection conn = null;

							//try
							//{
							//    conn = new SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD));
							//    conn.Open();

							//    SqlCeCommand cmd = conn.CreateCommand();
							//    cmd.CommandType = CommandType.TableDirect;
							//    //cmd.CommandText = "SELECT * FROM Production";
							//    cmd.CommandText = "Production";

							//    //SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable);
							//    SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable);
							//    SqlCeUpdatableRecord rec = rs.CreateRecord();

							//    Fask.SQLiteDBs.DataSets.Vyroba dsv = new Fask.SQLiteDBs.DataSets.Vyroba();

							//    if (!pRow.IsCountEntriesNull())
							//        rec.SetInt32(rec.GetOrdinal(dsv.Production.CountEntriesColumn.ColumnName), pRow.CountEntries);
							//    if (!pRow.IsSOPNUMBENull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.SOPNUMBEColumn.ColumnName), pRow.SOPNUMBE);
							//    if (!pRow.IsITEMNMBRNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.ITEMNMBRColumn.ColumnName), pRow.ITEMNMBR);
							//    if (!pRow.IsITEMTYPENull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.ITEMTYPEColumn.ColumnName), pRow.ITEMTYPE);
							//    if (!pRow.IsITEMMJNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.ITEMMJColumn.ColumnName), pRow.ITEMMJ);
							//    if (!pRow.IsITEMDESCNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.ITEMDESCColumn.ColumnName), pRow.ITEMDESC);
							//    if (!pRow.IsORDNull())
							//        rec.SetInt32(rec.GetOrdinal(dsv.Production.ORDColumn.ColumnName), pRow.ORD);
							//    if (!pRow.IsTIMEMODENull())
							//        rec.SetInt32(rec.GetOrdinal(dsv.Production.TIMEMODEColumn.ColumnName), pRow.TIMEMODE);
							//    if (!pRow.IsTIMEPREPSTARTNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMEPREPSTARTColumn.ColumnName), pRow.TIMEPREPSTART);
							//    if (!pRow.IsTIMEPREPSTOPNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMEPREPSTOPColumn.ColumnName), pRow.TIMEPREPSTOP);
							//    if (!pRow.IsTIMEPREPNull())
							//        rec.SetFloat(rec.GetOrdinal(dsv.Production.TIMEPREPColumn.ColumnName), pRow.TIMEPREP);
							//    if (!pRow.IsTIMEUNITNull())
							//        rec.SetFloat(rec.GetOrdinal(dsv.Production.TIMEUNITColumn.ColumnName), pRow.TIMEUNIT);
							//    if (!pRow.IsTIMESTARTNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMESTARTColumn.ColumnName), pRow.TIMESTART);
							//    if (!pRow.IsTIMESTOPNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMESTOPColumn.ColumnName), pRow.TIMESTOP);
							//    if (!pRow.IsTIMECORSTARTNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMECORSTARTColumn.ColumnName), pRow.TIMECORSTART);
							//    if (!pRow.IsTIMECORSTOPNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.TIMECORSTOPColumn.ColumnName), pRow.TIMECORSTOP);
							//    if (!pRow.IsTIMECORNull())
							//        rec.SetFloat(rec.GetOrdinal(dsv.Production.TIMECORColumn.ColumnName), pRow.TIMECOR);
							//    if (!pRow.IsTIMECRIDNull())
							//        rec.SetInt32(rec.GetOrdinal(dsv.Production.TIMECRIDColumn.ColumnName), pRow.TIMECRID);
							//    if (!pRow.IsTIMECRIDTYPENull())
							//        rec.SetByte(rec.GetOrdinal(dsv.Production.TIMECRIDTYPEColumn.ColumnName), pRow.TIMECRIDTYPE);
							//    rec.SetString(rec.GetOrdinal(dsv.Production.loginidColumn.ColumnName), pRow.loginid);
							//    if (!pRow.IsmachineidNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.machineidColumn.ColumnName), pRow.machineid);
							//    if (!pRow.IsoperationidNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.operationidColumn.ColumnName), pRow.operationid);
							//    rec.SetDateTime(rec.GetOrdinal(dsv.Production.dateeveColumn.ColumnName), pRow.dateeve);
							//    rec.SetDecimal(rec.GetOrdinal(dsv.Production.qtyColumn.ColumnName), pRow.qty);
							//    rec.SetDecimal(rec.GetOrdinal(dsv.Production.qtyRealColumn.ColumnName), pRow.qtyReal);
							//    if (!pRow.IsQTYPACKNull())
							//        rec.SetDecimal(rec.GetOrdinal(dsv.Production.QTYPACKColumn.ColumnName), pRow.QTYPACK);
							//    if (!pRow.IsQTYPACKMJNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.QTYPACKMJColumn.ColumnName), pRow.QTYPACKMJ);
							//    if (!pRow.IsdescriptionNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.descriptionColumn.ColumnName), pRow.description);
							//    if (!pRow.IsBarcodePNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.BarcodePColumn.ColumnName), pRow.BarcodeP);
							//    rec.SetString(rec.GetOrdinal(dsv.Production.UserIDColumn.ColumnName), pRow.UserID);
							//    rec.SetByte(rec.GetOrdinal(dsv.Production.TermIDColumn.ColumnName), pRow.TermID);
							//    if (!pRow.IsISOKNull())
							//        rec.SetDateTime(rec.GetOrdinal(dsv.Production.ISOKColumn.ColumnName), pRow.ISOK);
							//    rec.SetGuid(rec.GetOrdinal(dsv.Production.GUIDColumn.ColumnName), pRow.GUID);
							//    if (!pRow.IsSOUBEHGUIDNull())
							//        rec.SetGuid(rec.GetOrdinal(dsv.Production.SOUBEHGUIDColumn.ColumnName), pRow.SOUBEHGUID);
							//    if (!pRow.IsCORRGUIDNull())
							//        rec.SetGuid(rec.GetOrdinal(dsv.Production.CORRGUIDColumn.ColumnName), pRow.CORRGUID);
							//    if (!pRow.IsSKL_IDNull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.SKL_IDColumn.ColumnName), pRow.SKL_ID);
							//    if (!pRow.IsLOCNCODENull())
							//        rec.SetString(rec.GetOrdinal(dsv.Production.LOCNCODEColumn.ColumnName), pRow.LOCNCODE);

							//    rs.Insert(rec);
							//}
							//catch (Exception e)
							//{
							//    MessageBox.Show(e.Message);
							//}
							//finally
							//{
							//    conn.Close();
							//}



							#endregion


							//Nechapu tu šilenost z resoultsetama....

							
							if (pRow.RowState != DataRowState.Added)
								pRow.SetAdded(); // pokud to hodi chu tak je treba zmenit vlastnost RowState toho øadku... 


							Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(pRow);

                            #region

                            //                            string loginid = pRow.loginid;
                            //                            DateTime dateeve = pRow.dateeve;
                            //                            decimal qty = pRow.qty;
                            //                            decimal qtyReal = pRow.qtyReal;
                            //                             string UserID =   pRow.UserID;
                            //                            byte TermID =    pRow.TermID;
                            //                            Guid GUID =    pRow.GUID;
                            //                             string machineid =   pRow.IsmachineidNull() ? null : pRow.machineid;
                            //                               decimal? QTYPACK = pRow.IsQTYPACKNull() ? (decimal?)null : pRow.QTYPACK;
                            //                              string QTYPACKMJ =  pRow.IsQTYPACKMJNull() ? null : pRow.QTYPACKMJ;
                            //                              string description  = pRow.IsdescriptionNull() ? null : pRow.description;
                            //                              string BarcodeP =  pRow.IsBarcodePNull() ? null : pRow.BarcodeP;
                            //                              DateTime? ISOK =  pRow.IsISOKNull() ? (DateTime?)null : pRow.ISOK;


                            //                              System.Data.SqlServerCe.SqlCeConnection sqlconn = new System.Data.SqlServerCe.SqlCeConnection(
                            //                                  "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf)
                            //                                  );
                            //                              System.Data.SqlServerCe.SqlCeCommand sqlcomm = new System.Data.SqlServerCe.SqlCeCommand();
                            //                              sqlcomm.Connection = sqlconn;


                            //                              string comandText = "INSERT [Production] " +
                            //                                  "([CountEntries], " +
                            //                                  "[SOPNUMBE], " +
                            //                                  "[ITEMNMBR],  " +
                            //                                  "[ITEMTYPE], " +
                            //                                  "[ITEMMJ], " +
                            //                                  "[ITEMDESC], " +
                            //                                  "[ORD], " +
                            //                                  "[TIMEMODE],  " +
                            //                                  "[TIMEPREPSTART], " +
                            //                                  "[TIMEPREPSTOP], " +
                            //                                  "[TIMEPREP], " +
                            //                                  "[TIMEUNIT], " +
                            //                                  "[TIMESTART], " +
                            //                                  "[TIMESTOP], " +
                            //                                  "[TIMECORSTART], " +
                            //                                  "[TIMECORSTOP], " +
                            //                                  "[TIMECOR], " +
                            //                                  "[TIMECRID], " +
                            //                                  "[TIMECRIDTYPE], " +
                            //                                  "[loginid], " +
                            //                                  "[machineid], " +
                            //                                  "[operationid], " +
                            //                                  "[dateeve], " +
                            //                                  "[qty], " +
                            //                                  "[qtyReal], " +
                            //                                  "[QTYPACK], " +
                            //                                  "[QTYPACKMJ], " +
                            //                                  "[description], " +
                            //                                  "[BarcodeP], " +
                            //                                  "[UserID], " +
                            //                                  "[TermID], " +
                            //                                  "[ISOK], " +
                            //                                  "[GUID], " +
                            //                                  "[SOUBEHGUID], " +
                            //                                  "[CORRGUID], " +
                            //                                  "[SKL_ID], " +
                            //                                  "[LOCNCODE]" +
                            //                                  ")" +
                            //                                  String.Format(" VALUES(" +
                            //                                  "{0},"+
                            //                                  "{1},"+
                            //                                  "{2},"+
                            //                                  "{3},"+
                            //                                  "{4},"+
                            //                                  "{5},"+
                            //                                  "{6},"+
                            //                                  "{7},"+
                            //                                  "{8},"+
                            //                                  "{9},"+
                            //                                  "{10},"+
                            //                                  "{11},"+
                            //                                  "{12},"+
                            //                                  "{13},"+
                            //                                  "{14},"+
                            //                                  "{15},"+
                            //                                  "{16},"+
                            //                                  "{17},"+
                            //                                  "{18},"+
                            //                                  "{19},"+
                            //                                  "{20},"+
                            //                                  "{21},"+
                            //                                  "{22},"+
                            //                                  "{23},"+
                            //                                  "{24},"+
                            //                                  "{25},"+
                            //                                  "{26},"+
                            //                                  "{27},"+
                            //                                  "{28},"+
                            //                                  "{29},"+
                            //                                  "{30},"+
                            //                                  "{31},"+
                            //                                  "{32},"+
                            //                                  "{33},"+
                            //                                  "{34},"+
                            //                                  "{35},"+
                            //                                  "{36})",
                            //                             pRow.IsCountEntriesNull() ? (int?)null : pRow.CountEntries,
                            //                             pRow.IsSOPNUMBENull() ? "''" : pRow.SOPNUMBE,
                            //                             pRow.IsITEMNMBRNull() ? "''" : pRow.ITEMNMBR,
                            //                             pRow.IsITEMTYPENull() ? "''" : pRow.ITEMTYPE,
                            //                             pRow.IsITEMMJNull() ? "''" : pRow.ITEMMJ,
                            //                             pRow.IsITEMDESCNull() ? "''" : pRow.ITEMDESC,
                            //                             pRow.IsORDNull() ? (int?)null : pRow.ORD,
                            //                             pRow.IsTIMEMODENull() ? (int?)null : pRow.TIMEMODE,
                            //                             pRow.IsTIMEPREPSTARTNull() ? (DateTime?)null : pRow.TIMEPREPSTART,
                            //                             pRow.IsTIMEPREPSTOPNull() ? (DateTime?)null : pRow.TIMEPREPSTOP,
                            //                             pRow.IsTIMEPREPNull() ? (float?)null : pRow.TIMEPREP,
                            //                             pRow.IsTIMEUNITNull() ? (float?)null : pRow.TIMEUNIT,
                            //                             pRow.IsTIMESTARTNull() ? (DateTime?)null : pRow.TIMESTART,
                            //                             pRow.IsTIMESTOPNull() ? (DateTime?)null : pRow.TIMESTOP,
                            //                             pRow.IsTIMECORSTARTNull() ? (DateTime?)null : pRow.TIMECORSTART,
                            //                             pRow.IsTIMECORSTOPNull() ? (DateTime?)null : pRow.TIMECORSTOP,                             
                            //                             pRow.IsTIMECORNull() ? (float?)null : pRow.TIMECOR,
                            //                             pRow.IsTIMECRIDNull() ? (int?)null : pRow.TIMECRID,
                            //                             pRow.IsTIMECRIDTYPENull() ? (byte?)null : pRow.TIMECRIDTYPE,
                            //                             pRow.loginid,
                            //                             pRow.IsmachineidNull() ? "''" : pRow.machineid,
                            //                             pRow.IsoperationidNull() ? "''" : pRow.operationid,
                            //                             pRow.dateeve,
                            //                             pRow.qty,
                            //                             pRow.qtyReal,
                            //                             pRow.IsQTYPACKNull() ? (decimal?)null : pRow.QTYPACK,
                            //                             pRow.IsQTYPACKMJNull() ? "''" : pRow.QTYPACKMJ,
                            //                             pRow.IsdescriptionNull() ? "''" : pRow.description,
                            //                             pRow.IsBarcodePNull() ? "''" : pRow.BarcodeP,
                            //                             pRow.UserID,
                            //                             pRow.TermID,
                            //                             pRow.IsISOKNull() ? (DateTime?)null : pRow.ISOK,
                            //                             pRow.GUID,
                            //                             pRow.IsSOUBEHGUIDNull() ? (Guid?)null : pRow.SOUBEHGUID, 
                            //                             pRow.IsCORRGUIDNull() ? (Guid?)null : pRow.CORRGUID,
                            //                             pRow.IsSKL_IDNull() ? "''" : pRow.SKL_ID,
                            //                             pRow.IsLOCNCODENull() ? "''" : pRow.LOCNCODE
                            //                             );

                            //                              sqlcomm.CommandText = comandText;


                            //                              sqlconn.Open();

                            //                              int rowsaff = sqlcomm.ExecuteNonQuery();




                            //                              if (sqlconn != null && (sqlconn.State & ConnectionState.Open) == ConnectionState.Open)
                            //                              {
                            //                                  sqlconn.Close();
                            //                                  sqlconn.Dispose();
                            //                                  sqlconn = null;
                            //                              }

                            //#region Test vkladat po castech
                            //                            //        pta.Insert_Firtst(
                            //                            //    loginid,
                            //                            //    dateeve,
                            //                            //    qty,
                            //                            //    qtyReal,
                            //                            //    UserID,
                            //                            //    TermID,
                            //                            //    GUID,
                            //                            //    machineid,
                            //                            //    QTYPACK,
                            //                            //    QTYPACKMJ,
                            //                            //    description,
                            //                            //    BarcodeP,
                            //                            //    ISOK
                            //                            //    );

                            //                            //pta.Update_ODidUP_byGUID(
                            //                            //    pRow.IsTIMECRIDNull() ? (int?)null : pRow.TIMECRID,
                            //                            //    pRow.IsTIMECORNull() ? (float?)null : pRow.TIMECOR,
                            //                            //    pRow.IsTIMESTOPNull() ? (DateTime?)null : pRow.TIMESTOP,
                            //                            //    pRow.IsTIMEUNITNull() ? (float?)null : pRow.TIMEUNIT,
                            //                            //    pRow.IsTIMESTARTNull() ? (DateTime?)null : pRow.TIMESTART,
                            //                            //    pRow.IsTIMEPREPNull() ? (float?)null : pRow.TIMEPREP,
                            //                            //    pRow.IsORDNull() ? (int?)null : pRow.ORD,
                            //                            //    pRow.IsITEMMJNull() ? null : pRow.ITEMMJ,
                            //                            //    pRow.IsITEMTYPENull() ? null : pRow.ITEMTYPE,
                            //                            //    pRow.IsITEMNMBRNull() ? null : pRow.ITEMNMBR,
                            //                            //    pRow.IsCountEntriesNull() ? (int?)null : pRow.CountEntries,
                            //                            //    pRow.IsSOPNUMBENull() ? null : pRow.SOPNUMBE,
                            //                            //    pRow.GUID
                            //                            //    );


                            //                            //pta.Update_ODguidDOWN_ByGUID(
                            //                            //     pRow.IsTIMEMODENull() ? (int?)null : pRow.TIMEMODE,
                            //                            //     pRow.IsTIMEPREPSTARTNull() ? (DateTime?)null : pRow.TIMEPREPSTART,
                            //                            //     pRow.IsTIMEPREPSTOPNull() ? (DateTime?)null : pRow.TIMEPREPSTOP,
                            //                            //     pRow.IsTIMECORSTARTNull() ? (DateTime?)null : pRow.TIMECORSTART,
                            //                            //     pRow.IsTIMECORSTOPNull() ? (DateTime?)null : pRow.TIMECORSTOP,
                            //                            //     pRow.IsoperationidNull() ? null : pRow.operationid,
                            //                            //     pRow.IsSOUBEHGUIDNull() ? (Guid?)null : pRow.SOUBEHGUID,
                            //                            //      pRow.IsCORRGUIDNull() ? (Guid?)null : pRow.CORRGUID,
                            //                            //      pRow.IsTIMECRIDTYPENull() ? (byte?)null : pRow.TIMECRIDTYPE,
                            //                            //      pRow.IsSKL_IDNull() ? null : pRow.SKL_ID,
                            //                            //      pRow.IsLOCNCODENull() ? null : pRow.LOCNCODE,
                            //                            //      pRow.IsITEMDESCNull() ? null : pRow.ITEMDESC,
                            //                            //      pRow.GUID
                            //                            //    ); 
                            //    #endregion

                            //                            /*
                            //                            pta.Insert_BezITEMDESC(
                            //                             pRow.IsCountEntriesNull() ? (int?)null : pRow.CountEntries,
                            //                             pRow.IsSOPNUMBENull() ? null : pRow.SOPNUMBE,
                            //                             pRow.IsITEMNMBRNull() ? null : pRow.ITEMNMBR,
                            //                             pRow.IsITEMTYPENull() ? null : pRow.ITEMTYPE,
                            //                             pRow.IsITEMMJNull() ? null : pRow.ITEMMJ,
                            //                             pRow.IsORDNull() ? (int?)null : pRow.ORD,
                            //                             pRow.IsTIMEPREPNull() ? (float?)null : pRow.TIMEPREP,
                            //                             pRow.IsTIMEUNITNull() ? (float?)null : pRow.TIMEUNIT,
                            //                             pRow.IsTIMESTARTNull() ? (DateTime?)null : pRow.TIMESTART,
                            //                             pRow.IsTIMESTOPNull() ? (DateTime?)null : pRow.TIMESTOP,
                            //                             pRow.IsTIMECORNull() ? (float?)null : pRow.TIMECOR,
                            //                             pRow.IsTIMECRIDNull() ? (int?)null : pRow.TIMECRID,
                            //                             pRow.loginid,
                            //                             pRow.IsmachineidNull() ? null : pRow.machineid,
                            //                             pRow.dateeve,
                            //                             pRow.qty,
                            //                             pRow.qtyReal,
                            //                             pRow.IsQTYPACKNull() ? (decimal?)null : pRow.QTYPACK,
                            //                             pRow.IsQTYPACKMJNull() ? null : pRow.QTYPACKMJ,
                            //                             pRow.IsdescriptionNull() ? null : pRow.description,
                            //                             pRow.IsBarcodePNull() ? null : pRow.BarcodeP,
                            //                             pRow.UserID,
                            //                             pRow.TermID,
                            //                             pRow.IsISOKNull() ? (DateTime?)null : pRow.ISOK,
                            //                             pRow.GUID,
                            //                             pRow.IsTIMEMODENull() ? (int?)null : pRow.TIMEMODE,
                            //                             pRow.IsTIMEPREPSTARTNull() ? (DateTime?)null : pRow.TIMEPREPSTART,
                            //                             pRow.IsTIMEPREPSTOPNull() ? (DateTime?)null : pRow.TIMEPREPSTOP,
                            //                             pRow.IsTIMECORSTARTNull() ? (DateTime?)null : pRow.TIMECORSTART,
                            //                             pRow.IsTIMECORSTOPNull() ? (DateTime?)null : pRow.TIMECORSTOP,
                            //                             pRow.IsoperationidNull() ? null : pRow.operationid
                            //                             );

                            //                            pta.UpdateITEMDESC_byGUID(
                            //                                pRow.IsITEMDESCNull() ? null : pRow.ITEMDESC, 
                            //                                pRow.IsLOCNCODENull() ? null : pRow.LOCNCODE,
                            //                                pRow.IsSKL_IDNull() ? null : pRow.SKL_ID,
                            //                                pRow.IsTIMECRIDTYPENull() ? (byte?)null : pRow.TIMECRIDTYPE,
                            //                                pRow.IsCORRGUIDNull() ? (Guid?)null : pRow.CORRGUID,
                            //                                pRow.IsSOUBEHGUIDNull() ? (Guid?)null : pRow.SOUBEHGUID,                            
                            //                                pRow.GUID);
                            //                            */
                            #endregion

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
						recordsUpdated = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(correctionRow);

                        recordsUpdated = 0;
                        //provest ulozeni odvodu, jen pokud neni hodnota 0 a neni to timestop!
                        if (!(pRow.qty == 0 && !pRow.IsTIMESTOPNull()))
							recordsUpdated = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(pRow);
                    }

                    //nastaveni posledniho casu odvodu
                    Settings.LastProductionDateTime = productionRow.dateeve;


                    //ulozeni modifiovaneho odvodu do predlohy
                    var ctrl_vyroba_prd = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD;
                    try
                    {
                        ctrl_vyroba_prd.Connection_Open();
                        int raff = ctrl_vyroba_prd.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(pocetOdvedeno, 1, productionRow.dateeve, rowvpp.CountEntries, rowvpp.SOPNUMBE, rowvpp.ORD, rowvpp.ITEMNMBR, rowvpp.ITEMTYPE, rowvpp.QTYPACK, rowvpp.BarcodeP);
                    }
                    catch (Exception e)
                    {
						Logging.Log.Write(e.Message, this.Text);
                    }
                    finally
                    {
                        ctrl_vyroba_prd.Connection_Close();
                    }

                    //Aktualizace interni databaze s informaci o poslednim odvodu uzivatele
                    InternalState.UpdateInternalStateLstOperationUser(productionRow.UserID, productionRow.dateeve);
                }

                //ulozeni dataset product sources

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter taProductionSources = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.Production_SourcesTableAdapter();
				//taProductionSources.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD));

				int res = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production_Sources(psdt);


                //Vycisteni vstupniho pole - nema smysl, po uspesnem odvodu se okno uzavira
                //this.textBoxVyrobniOperace.Text = string.Empty;
                //textBoxVyrobniOperaceFocusAll();
                Settings.Update();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                MessageBox.Show(ex.Message, this.Text);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ScannerStart();
                textBoxVyrobniOperaceFocusAll();
            }

            #region Znovu Spousteni automaticky ...
            if (Settings.StopVyrobaPoStartVyrobaIhned)
            {
                // TODO : provest akce => provedeni akci
                // => automaticky provest stopodvod po startodvod...
                // automaticky provest stopodvod ...
                if (this.odvodProcessedMode == TIMEMODES.StartStop && this.odvodProcessedState == TIMESTATE.Odvod_Zahajen)
                { // automaticke zahajeni prikazu
                    // Provede:
                    // - naplanovani znovu performok po dokonceni teto operace
                    // - dialog se neukonci a zustane tam puvodni cislo operace

                    //this.BeginInvoke
                    //    ((Action)delegate
                    //    {
                    //        this.PerformOK();
                    //    });
                    this.BeginInvoke((Action)(() => { this.PerformOK(null); }));
                    return;
                }
            }
            #endregion

            //Odvod dokoncen => zastavit scanner a konec
            ScannerFinalize();
            DialogResult = DialogResult.OK;
		}

		#endregion

		#region OLD, co to je ??,  Tato metoda existuje v databaseactions ...

		// Tato metoda existuje v databaseactions ...
        //private DateTime? UserLastAction()
        //{
        //    DateTime? lastUserActionDateTime = null;
        //    DateTime? lastUserActionDateTimeLocal = null;
        //    DateTime? lastUserActionDateTimeServer = null;
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;

        //        Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter pta = new Fask.Vyroba_W.Data.VyrobaCEDataSetTableAdapters.ProductionTableAdapter();
        //        pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf);

        //        Data.VyrobaCEDataSet.ProductionDataTable dt_lst_LProduction = pta.GetDataByUserID(idpracovnik.id);
        //        if (dt_lst_LProduction.Count > 0)
        //            lastUserActionDateTimeLocal = dt_lst_LProduction[0].dateeve;

        //        // Zjisteni posledni akce uzivatele...
        //        try
        //        {
        //            lastUserActionDateTimeServer = vyrobaS.UserLastAction(idpracovnik.id);
        //        }
        //        catch (Exception ews)
        //        {
		//            Logging.Log.Write(ews);
        //        }

        //        if (lastUserActionDateTimeLocal.HasValue)
        //            lastUserActionDateTime = lastUserActionDateTimeLocal;

        //        if (lastUserActionDateTimeServer.HasValue)
        //        {
        //            if (!lastUserActionDateTime.HasValue)
        //                lastUserActionDateTime = lastUserActionDateTimeServer;
        //            else if (lastUserActionDateTime.Value < lastUserActionDateTimeServer.Value)
        //                lastUserActionDateTime = lastUserActionDateTimeServer;
        //        }

        //        return lastUserActionDateTime;

        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
		//        Logging.Log.Write(ex);

        //        return null;
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

#endregion

		#region Metoda, která nemá nikde reference

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

				Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt_lst_LProduction = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetDataByUserIDMachineIDNULL_Production(idpracovnik.id);
				Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow lst_LProductionRow = null;
                if (dt_lst_LProduction.Count > 0)
                    lst_LProductionRow = dt_lst_LProduction[0];

                // Zjisteni posledni akce uzivatele...
                WebServiceVyroba.VyrobaDataSet ds_lst_RProduction = null;
                WebServiceVyroba.VyrobaDataSet.ProductionRow lst_RProductionRow = null;
                try
                {
					ds_lst_RProduction = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProductionLastAction(idpracovnik.id, string.Empty, true, 1);
                    if (ds_lst_RProduction.Production.Count > 0)
                        lst_RProductionRow = ds_lst_RProduction.Production[0];
                }
                catch (Exception ews)
                {
					Logging.Log.Write(ews);
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
						Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable cdt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(existingProductionRow.TIMECRID);
						Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow crow = null;
                        if (cdt.Count > 0)
                        {
                            crow = cdt[0];
                        }
                        MessageBox.Show(
                            "Existuje nedokonèená korekce:" +
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
				Logging.Log.Write(ex);
                DialogResult drCorr = MessageBox.Show("Nepodaøilo se zjistit, zda existuje nedokonèená korekce mimo výrobu\nPokraèovat?", "Korekce", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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

		#endregion

        private void textBoxVyrobniOperaceFocusAll()
        {
            try
            {
                this.textBoxVyrobniOperace.Focus();
                this.textBoxVyrobniOperace.SelectAll();
            }
            catch { }
        }

		#region Metody pro tlaèítka

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.BeginInvoke((Action)(() => { this.PerformOK(null); }));
		}

		private void buttonStorno_Click(object sender, EventArgs e)
		{
			this.PerformCancel();
		}

		private void panelButtons_Resize(object sender, EventArgs e)
		{
			Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
			buttonStorno.Size = nsize;
		}

		#endregion

        #region Stavy odvadeni

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
            DialogResult dr = MessageBox.Show("Zahájit pøípravu operace " + rowvpp.ITEMDESC + "?", "Pøíprava Start", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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
            DialogResult dr = MessageBox.Show("Ukonèit pøípravu operace " + rowvpp.ITEMDESC + "?", "Pøírava Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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

        private DialogResult StartOdvod(
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast
            )
        {
            DialogResult dr = MessageBox.Show("Zahájit operaci " + rowvpp.ITEMDESC + "?", "Výroba Start", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Cancel)
                return DialogResult.Cancel;

            ProductionRowFill(0, rowvpp, productionRow, 0);

            productionRow.TIMESTART = DateTime.Now;

            // pridani noveho soubehguid
            productionRow.SOUBEHGUID = Guid.NewGuid();

            return DialogResult.OK;
        }

        // Stop odvod operace ...
        private DialogResult StopOdvod(
            decimal? qtyodvedeno,
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow rowvph,
			Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow rowvpp,
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow,
			Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowLast,
            out decimal pocetOdvedeno,
			Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable psdt,
			BaseCode code
            )
        {

			byte SerNumTrack = rowvpp.SerNumT;
            //Pozadavek na potvrzeni poctu odvedenych kusu            
            pocetOdvedeno = 0; //decimal pocetOdvedeno = 0;
            decimal pocetZbyva = (rowvpp.QTYSHPPD - (rowvpp.QTYODVEDENO + (qtyodvedeno ?? 0)));
            Cursor.Current = Cursors.Default;

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


            if (Settings.Production_Material_Enter && Settings.Production_Material_PredVyrobou)
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
                        return DialogResult.Cancel;
                    }
                }
            }


            Cursor.Current = Cursors.Default;


			if (code != null)
			{
				if (code is WeightCode)
					pocetOdvedeno = ((WeightCode)code).weight;
				else if (code is Fask.Parsing.Codes.WeightCode_12)
					pocetOdvedeno = ((WeightCode_12)code).weight;
			}
			else
			{

				if (Settings.Production_Sarze_SN_Enable)
				{
					if (SerNumTrack == 1)
					{
						pocetOdvedeno = 1;
					}
					else
					{
						using (FormInputQuantity frmKod = new FormInputQuantity())
						{
							frmKod.Text = "Potvrïte/opravte poèet odvedených kusù";
							if (Settings.Odvadeni_production_onlyPositive)
							{
								frmKod.Kod = pocetZbyva < 0 ? "0" : pocetZbyva.ToString("0.####");
							}
							else
							{
								frmKod.Kod = pocetZbyva.ToString("0.####");
							}

							while (true)
							{
								if (frmKod.ShowDialog() == DialogResult.Cancel)
								{
									return DialogResult.Cancel;
								}
								//zadany pocet odvedenych kusu
								pocetOdvedeno = decimal.Parse(frmKod.Kod);
								//Pocet odvedenych je vetsi nez zbyva => dotaz na potvrzeni
								if (pocetOdvedeno > pocetZbyva)
								{
									if (Settings.Odvadeni_production_NeupozornovatNaVetsiPocet)
									{
										MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundDotaz));
										if (MessageBox.Show("Je zadán vìtší poèet k odvedení, než je požadováno\nZbývá: " + pocetZbyva.ToString("0.00") + "\nZadáno:" + pocetOdvedeno.ToString("0.00") + "\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
											== DialogResult.No)
											continue;
									}
								}
								break;
							}
						}
					}
				}
				else
				{

					using (FormInputQuantity frmKod = new FormInputQuantity())
					{
						frmKod.Text = "Potvrïte/opravte poèet odvedených kusù";
						if (Settings.Odvadeni_production_onlyPositive)
						{
							frmKod.Kod = pocetZbyva < 0 ? "0" : pocetZbyva.ToString("0.####");
						}
						else
						{
							frmKod.Kod = pocetZbyva.ToString("0.####");
						}

						while (true)
						{
							if (frmKod.ShowDialog() == DialogResult.Cancel)
							{
								return DialogResult.Cancel;
							}
							//zadany pocet odvedenych kusu
							pocetOdvedeno = decimal.Parse(frmKod.Kod);
							//Pocet odvedenych je vetsi nez zbyva => dotaz na potvrzeni
							if (pocetOdvedeno > pocetZbyva)
							{
								if (Settings.Odvadeni_production_NeupozornovatNaVetsiPocet)
								{
									MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundDotaz));
									if (MessageBox.Show("Je zadán vìtší poèet k odvedení, než je požadováno\nZbývá: " + pocetZbyva.ToString("0.00") + "\nZadáno:" + pocetOdvedeno.ToString("0.00") + "\n\nChcete pokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
										== DialogResult.No)
										continue;
								}
							}
							break;
						}
					}
				}
			}
            //Cursor.Current = Cursors.WaitCursor;

            ProductionRowSetQuantity(productionRow, pocetOdvedeno);

			#region 2.9.2020 TaD , øešení šarží

			if (Settings.Production_Sarze_SN_Enable)
			{

				//Zde bude logika ohlednì šarži a SN...
				string sn = string.Empty;

				if(SerNumTrack == 1)
				{
					using (FormInput_Production_SN frmPSN = new FormInput_Production_SN())
					{
						frmPSN.ProductionRow = productionRow;
						if (frmPSN.ShowDialog() == DialogResult.Cancel)
						{
							return DialogResult.Cancel;
						}

						pocetOdvedeno = frmPSN.QTY_SN;
						ProductionRowSetQuantity(productionRow, pocetOdvedeno);
					}
				}
				else if (SerNumTrack == 2)
				{
					string[] SNs = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Generate_SarzeOnline(
						string.Empty,
						Globals.Pracovnik.id,
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

						Fask.SQLiteDBs.DataSets.Vyroba.Production_SNDataTable dtPSN = new Vyroba.Production_SNDataTable();
						
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

						Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production_SN(dtPSN);

					}
				}
			}
			else
			{
				productionRow.SetSERLTNUMNull();
			}

			
			#endregion

			

            // TODO : dialog vyberu skladu
            if (Settings.Production_Destination_SKLID_Enter)
            {
				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
				//taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);

                //Zadani ciloveho skladu a cilove lokace ...
                using (FormInputKod fik = new FormInputKod())
                {
                    fik.Text = "Zadejte cílový sklad";
                    if (!productionRow.IsSKL_IDNull()
                        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                        )
                    {
						var listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(productionRow.SKL_ID);
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
						var listSklady = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                        if (listSklady.Count() == 0)
                        {
                            if (DialogResult.Cancel == MessageBox.Show("Sklad '" + fik.Kod + "' nenalezen!", "Cílová sklad", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
                                return DialogResult.Cancel;
                        }
                        else
                        {
                            fik.Kod = listSklady.First().skl_id.Trim(); // Neni toto opaènì??
                            break;
                        }
                    }
                    productionRow.SKL_ID = fik.Kod;
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
                using (FormInputKod fik = new FormInputKod())
                {
                    fik.Text = "Zadejte cíl. lokaci";
                    if (!productionRow.IsSKL_IDNull()
                        && !productionRow.IsLOCNCODENull()
                        && !string.IsNullOrEmpty(productionRow.SKL_ID)
                        && !string.IsNullOrEmpty(productionRow.LOCNCODE)
                        )
                    {
						var listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(productionRow.SKL_ID, productionRow.LOCNCODE);
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
						var listLokace = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(productionRow.SKL_ID, fik.Kod);
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
            //Zobrazeni doby trvani operace, doplneni korekci a potvrzeni dokonceni operace

            if (Settings.OdvadeniPotvrzeniOperace)
            {
                using (FormOperacePotvrzeni frmOKorekce = new FormOperacePotvrzeni())
                {
                    frmOKorekce.PocetOdvedeno = pocetOdvedeno;
                    frmOKorekce.Pracovnik = this.idpracovnik;
                    frmOKorekce.Machine = this.idmachine;
                    frmOKorekce.VPP = rowvpp;
                    frmOKorekce.ProductionRow = productionRow;
                    frmOKorekce.ProductionSDT = psdt;
                    if (frmOKorekce.ShowDialog() == DialogResult.Cancel)
                    {
                        return DialogResult.Cancel;
                    }
                }
            }





            if (Settings.Production_Material_Enter && Settings.Production_Material_PoVyrobe)
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
                        return DialogResult.Cancel;
                    }
                }
            }

			#region Zde bude TISK

			if (Settings.Production_Tisk_Etiketa_Enable)
			{
				do
				{
					bool vytisteno = false;
					try
					{
						vytisteno = Tisk.OdvedeniTisk.Print(productionRow, PrinterFactory.PrinterModules.VyrobaOdvedene, null);
					}
					catch (System.Exception ex)
					{
						DialogResult dr = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

						if (dr == DialogResult.Yes)
							continue;
						else
							break;
					}

					break;

				} while (true);
			}

			#endregion


            return DialogResult.OK;
        }

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

            productionRow.ITEMDESC = rowvpp.ITEMDESC;
        }
        #endregion
    }
}