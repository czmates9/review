#define MULTI_START_TEST_ENABLED

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Linq;

using Fask.Vyroba_W.ServerAccess;
using Fask.SQLiteDBs.DataSets;

namespace Fask.Vyroba_W.Forms
{
    public partial class FormMain : Form
    {
		public static FormMain Instance_FormMain = null;
		public GlobalObject globalObject = new GlobalObject();

        delegate void DelegateVoid();

        public static Fask.MST_W.Scanner.ScannerBase Scanner = null;

        private global::System.Threading.Timer timerDownload = null;
        private global::System.Threading.Timer timerUpload = null;

        private Object lockUpDownTest = new Object();

        public static bool _downloadInProgress = false;
        public static bool _uploadInProgress = false;

        private bool _adminMode = false;
        private bool AdminMode
        {
            get { return _adminMode; }
            set
            {
                _adminMode = value;
                buttonOdvadeni.Enabled = !_adminMode;
                buttonUdalosti.Enabled = !_adminMode;
            }
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void timerUploadStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((DelegateVoid)delegate() { timerUploadStart(nextrunmiliseconds); });
                return;
            }

            timerUpload.Change(nextrunmiliseconds, Settings.TimerUploadInterval);
        }

        private void timerUploadStart()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateVoid(timerUploadStart));
                return;
            }

            timerUploadStart(Settings.TimerUploadInterval);
        }

        private void timerUploadStop()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateVoid(timerUploadStop));
                return;
            }

            timerUpload.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void timerDownloadStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((DelegateVoid)delegate() { timerDownloadStart(nextrunmiliseconds); });
                return;
            }

            timerDownload.Change(nextrunmiliseconds, Settings.TimerDownloadInterval);
        }

        private void timerDownloadStart()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateVoid(timerDownloadStart));
                return;
            }

            timerDownloadStart(Settings.TimerDownloadInterval);
        }

        private void timerDownloadStop()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DelegateVoid(timerDownloadStop));
                return;
            }

            timerDownload.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.PerformClose(true);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

#if MULTI_START_TEST_ENABLED
            try
            {
                if (true)
                {
                    MySystem.Process[] procesy = MySystem.Process.GetProcesses();
                    string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    AssemblyName += ".exe";

                    int InstanceCount = 0;
                    for (int i = 0; i < procesy.Length; i++)
                    {
                        if (procesy[i].ProcessName == AssemblyName)
                            InstanceCount++;
                    }

                    if (InstanceCount > 1)
                    {
                        MessageBox.Show("Aplikace je již spuštìna.", this.Text, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
#endif

			FormMain.Instance_FormMain = this;
        

			#region Certifikat

			Globals.ServerAccessCertificateTrust = Settings.ServerAccessCertificateTrust;
			
			#endregion


            switch (Settings.ScannerType)
            {
                case Fask.MST_W.Scanner.ScannerTypes.Unitech_HT660:
                    Scanner = new Fask.MST_W.Scanner.ScannerHT660(this);
                    break;
                case Fask.MST_W.Scanner.ScannerTypes.Unitech_PA600:
                    Scanner = new Fask.MST_W.Scanner.ScannerHT660(this);
                    break;
                case Fask.MST_W.Scanner.ScannerTypes.Symbol_PT8800:
                    Scanner = new Fask.MST_W.Scanner.ScannerPT8800();
                    break;
                case Fask.MST_W.Scanner.ScannerTypes.Symbol_MC3000:
                    Scanner = new Fask.MST_W.Scanner.Symbol_MC3000();
                    break;
                case Fask.MST_W.Scanner.ScannerTypes.None:
                default:
                    Scanner = new Fask.MST_W.Scanner.ScannerNone();
                    break;
            }

            Cursor.Current = Cursors.WaitCursor;

            #region Inicializace databazovych souboru, pokud neexistuji => akutalizace aplikace
            // if file not exists - download
            // if file.tmp exists after download, copy it to file (without tmp)
            // delete file.tmp

            #region Production.prd initialization

            #region old
            //if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
            //{
            //    DownloadProductionStart();

            //    if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp)))
            //    {
            //        File.Copy(
            //                    Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp),
            //                    Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd), false
            //                    );
            //        File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp));
            //    }
            //}

            //// => ??? Proc ??? <=, mozna jako zaloha ztraty Production.prd databaze a neuspechu pri downloadproduction
            //// kopie Production DB, pokud neexistuje
            //if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp)))
            //{
            //    File.Copy(
            //                Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd),
            //                Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp), false
            //                );
            //}
            #endregion

            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
            {
                DownloadProductionStart();

                if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp)))
                {
                    File.Copy(
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp),
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd), false
                                );
                    // => nemazat, ponechat jako zalohu, kdyby se nepovedlo stahnout pri inizializaci ... //File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp));
                }
            }

            #endregion

            #region InternalState.prd initialization
            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD)))
            {
                // TODO : TaD dodelat stahovani internalstate.sdf z serveru platnou verzi tu i terminal
                DownloadInternalStateStart();

				//if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD)))
                if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD + Constants.TMP)))
                {
                    File.Copy(
								Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD + Constants.TMP),
								Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD), false
                              );
                    File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD + Constants.TMP));
                }
            }
            #endregion
            
            #endregion

            Cursor.Current = Cursors.Default;

            timerUpload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerUploadCallBack), this, Settings.TimerUploadInterval, Settings.TimerUploadInterval);
            //timerDownload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerDownloadCallBack), this, Settings.TimerDownloadInterval, Settings.TimerDownloadInterval);
            timerDownload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerDownloadCallBack), this, 2000, Settings.TimerDownloadInterval);

			Logging.Log.Enable = Settings.Loging;

            //menuItemPrihlasitOdhladit.Enabled = Settings.UEventSmenaEnabled;

            timerShown.Enabled = true;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            timerShown.Enabled = false;

            this.UpdateForm();

            LogIn();

            this.Show();
        }

        private void LogIn()
        {
            SystemTimeSynchronize();

            try
            {


                if (Settings.UEventSmenaEnabled)
                {
                    using (FormSmenaLogin fsl = new FormSmenaLogin())
                    {
                        DialogResult dr = fsl.ShowDialog();
                        if (dr == DialogResult.Cancel)
                        {
                            //this.PerformClose(false);
                            return;
                        }
                        else if (dr == DialogResult.Abort) //Vstup do administracni casti
                        {
							Logging.Log.Write("Administrative login");
                            this.AdminMode = true;
                            return;
                        }

                        try
                        {
                            InternalState.DeleteAll();
                        }
                        catch (Exception ex)
                        {
							Logging.Log.Write(ex.Message, this.Text);
                        }
                    }
                }

                //Prihlaseni uzivatele
                if (Settings.UEventPracovnikLoginEnabled)
                {
                    using (Forms.FormIDPracovnikaLogin fp = new FormIDPracovnikaLogin())
                    {
                        DialogResult dr = fp.ShowDialog();
                        if (dr == DialogResult.Cancel)
                        {
                            //this.PerformClose(false);
                            return;
                        }
                    }
                }

                if (Settings.VyberZakazkyPoPrihlaseni)
                {
                    // nastaveni aktivniho vyrobniho prikazu ...
					PerfomZmenaZakazky();

					//using (Odvadeni.FormPrikazVyber fpv = new Fask.Vyroba_W.Odvadeni.FormPrikazVyber())
					//{
					//    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter taP = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
					//    //taP.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
					//    var dtP = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetData_CZPRO_VPH();

					//    fpv.Owner = this;
					//    fpv.Text = "Pøíkaz";
					//    fpv.Prikazy = dtP.ToList();
					//    if (DialogResult.OK == fpv.ShowDialog())
					//        Globals.Zakazka = fpv.PrikazVybrany;
					//    else
					//    {
					//        Globals.Zakazka = null;
					//        return;
					//    }
					//}
                }

            }
			catch(Exception ex)
			{
				Logging.Log.Write(ex, this.Text);
			}
            finally
            {
                UpdateForm();
            }

        }

        private bool LogOut()
        {
            try
            {
                if (AdminMode)
                {
                    AdminMode = false;
                }
                else
                {
                    if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                    {
                        //// zobrazeni prehledu
                        //if (Settings.UEventPracovnikOdhlaseniShowReport)
                        //{
                        //    using (Reports.FormReportUzivatelVyrobaDen fuvd = new Fask.Vyroba_P.Reports.FormReportUzivatelVyrobaDen())
                        //    {
                        //        if (fuvd.ShowDialog() == DialogResult.Cancel)
                        //            return false;
                        //    }
                        //}
                        //else
                        //{
                        //    if (showConfirm && FlexibleMessageBox.Show(this, "Odhlásit pracovníka '" + Globals.Pracovnik.ToString() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        //        == DialogResult.No)
                        //        return false;
                        //}

                        try
                        {
                            // odhlaseni znamena odstraneni interniho zaznamu posledni operace uzivatele
                            InternalState.DeleteInternalStateLstOperationUser(Globals.Pracovnik.id);

							//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
							//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                            //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, UEventStatusTypes.SmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, Guid.NewGuid());
                            //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, Settings.UEventSmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, string.Empty, Guid.NewGuid());
							Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, Settings.MachineID, DateTime.Now, Settings.UEventPracovnikOdhlaseni, Globals.Pracovnik.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                            Globals.Pracovnik = null;
                        }
                        catch (Exception ex)
                        {
							Logging.Log.Write(ex, this.Text);
                            MessageBox.Show(ex.Message, this.Text);
                            return false;
                        }
                    }

                    if (Settings.UEventSmenaEnabled)
                    {
                        if (MessageBox.Show("Odhlásit smìnu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            == DialogResult.No)
                        {
                            return false;
                        }

                        try
                        {
							//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
							//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                            //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, UEventStatusTypes.SmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, Guid.NewGuid());
							Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, null, DateTime.Now, Settings.UEventSmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, string.Empty, Guid.NewGuid());
                            Globals.PracovnikVedouciSmeny = null;
                        }
                        catch (Exception ex)
                        {
							Logging.Log.Write(ex.Message, this.Text);
                            MessageBox.Show(ex.Message, this.Text);
                            return false;
                        }

                    }

                    // aktivni vyrobni prikaz ...
                    Globals.Zakazka = null;
                }

                return true;
            }
            catch (Exception exxxx)
            {
                MessageBox.Show(exxxx.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                UpdateForm();
            }

        }

        private void buttonUdalosti_Click(object sender, EventArgs e)
        {
            PerfomUdalosti();
        }

        private void PerfomUdalosti()
        {
            try
            {
                string idPracovnik = string.Empty;
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;

                if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                {
                    idPracovnik = Globals.Pracovnik.id;
                    pracovnik = Globals.Pracovnik;
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika fidprac = new Fask.Vyroba_W.Odvadeni.FormIDPracovnika())
                    {
                        if (fidprac.ShowDialog() == DialogResult.Cancel)
                            return;
                        idPracovnik = fidprac.Pracovnik.id;
                        pracovnik = fidprac.Pracovnik;
                    }
                }

				Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow statusRow = null;
                //string idUdalosti = string.Empty;

                //using (FormInputKod fik = new FormInputKod())
                using (Udalosti.FormUdalosti fik = new Fask.Vyroba_W.Udalosti.FormUdalosti())
                {
                    fik.Text = "Zadejte ID události";
                    //fik.Kod = string.Empty;

                    //Data.VyrobaCEDataSetTableAdapters.StatusTypesTableAdapter stta = new Fask.Vyroba_W.Data.VyrobaCEDataSetTableAdapters.StatusTypesTableAdapter();
                    //stta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);
                    //Data.VyrobaCEDataSet.StatusTypesDataTable stdt = null;

                    //string stid = string.Empty;
                    while (true)
                    {
                        if (fik.ShowDialog() == DialogResult.Cancel)
                            return;

                        //idUdalosti = fik.Kod.Trim();
                        //stdt = stta.GetDataByStatusid(idUdalosti);
                        //if (stdt.Count == 0)
                        //{
                        //    MessageBox.Show("Událost neexistuje", this.Text);
                        //    continue;
                        //}
                        //else
                        //{
                        //    stid = stdt[0].statusid;
                        //    if (stid == Settings.UEventSmenaLogin)
                        //        continue;
                        //    else if (stid == Settings.UEventSmenaLogout)
                        //        continue;

                        //    if (MessageBox.Show(
                        //        pracovnik.firstname.Trim() + " " + pracovnik.surname.Trim() + "\n" +
                        //        stdt[0].statusdesc, 
                        //        this.Text, 
                        //        MessageBoxButtons.OKCancel, 
                        //        MessageBoxIcon.Question, 
                        //        MessageBoxDefaultButton.Button1
                        //        ) == DialogResult.Cancel
                        //        )
                        //        continue;
                        //    else
                        //        break;
                        //}

                        statusRow = fik.SelectedStatusTypesRow;
                        if (statusRow.statusid == Settings.UEventSmenaLogin)
                            continue;
                        else if (statusRow.statusid == Settings.UEventSmenaLogout)
                            continue;

                        if (MessageBox.Show(
                            pracovnik.firstname.Trim() + " " + pracovnik.surname.Trim() + "\n" +
                            statusRow.statusdesc,
                            this.Text,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1
                            ) == DialogResult.Cancel
                            )
                            continue;
                        else
                            break;
                    }

					//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
					//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
					Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, null, DateTime.Now, statusRow.statusid, idPracovnik, Settings.TerminalID, string.Empty, Guid.NewGuid());

                    //if (stid == Settings.UEventPracovnikPrihlaseni) //Prihlaseni pracovnika => zapis do Internal logoper
                    if (statusRow.statusid == Settings.UEventPracovnikPrihlaseni) //Prihlaseni pracovnika => zapis do Internal logoper
                    {
                        InternalState.UpdateInternalStateLstOperationUser(idPracovnik, DateTime.Now);
                    }
                    //else if (stid == Settings.UEventPracovnikOdhlaseni)
                    else if (statusRow.statusid == Settings.UEventPracovnikOdhlaseni)
                    {
                        InternalState.DeleteInternalStateLstOperationUser(idPracovnik);
                    }
                }

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, this.Text);
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void PerformKorekce()
        {
            try
            {
                string idPracovnik = string.Empty;
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;

                if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                {
                    idPracovnik = Globals.Pracovnik.id;
                    pracovnik = Globals.Pracovnik;
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika fidprac = new Fask.Vyroba_W.Odvadeni.FormIDPracovnika())
                    {
                        if (fidprac.ShowDialog() == DialogResult.Cancel)
                            return;
                        idPracovnik = fidprac.Pracovnik.id;
                        pracovnik = fidprac.Pracovnik;
                    }
                }

                // Korekce
                // TODO : implementovat ...
                // Zjistit posledni otevrenou korekci
                // pokud neni
                // - dialog pro vytvoreni korekce
                // - pokud je dialog pro ukonceni korekce 

				Fask.SQLiteDBs.DataSets.Vyroba dsVyroba = new Fask.SQLiteDBs.DataSets.Vyroba();
				Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow correctionRow = null;
				Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsVyroba.Production.NewProductionRow();

                using (Korekce.FormKorekce formKorekce = new Fask.Vyroba_W.Korekce.FormKorekce())
                {
                    //formKorekce.Pracovnik = pracovnik;
                    formKorekce.ProductionRow = productionRow;
                    formKorekce.Correction = correctionRow;
                    formKorekce.CorretionMinimumDateTime = Data.DatabaseActions.UserLastAction(pracovnik.id);
                    if (DialogResult.Cancel == formKorekce.ShowDialog())
                        return;

					//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
					//pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

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

                    productionRow.UserID = pracovnik.id;
                    productionRow.TermID = Settings.TerminalID;

                    dsVyroba.Production.AddProductionRow(productionRow);
					Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Update_Production(productionRow);

                }                


            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, this.Text);
                MessageBox.Show(ex.Message, this.Text);
            }            
        }

        private void PerfomOdvadeni()
        {
            try
            {
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnik = null;
				Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;

                if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                {
                    idPracovnik = Globals.Pracovnik;
                }
                else
                {
                    //Zadani ID pracovnika
                    using (Odvadeni.FormIDPracovnika frmIDPracovnika = new Fask.Vyroba_W.Odvadeni.FormIDPracovnika())
                    {
                        if (frmIDPracovnika.ShowDialog() == DialogResult.Cancel)
                            return;

                        idPracovnik = frmIDPracovnika.Pracovnik;
                    }
                }

                //Zjisteni posledniho casu operace pracovnika
                //Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter lstoperta = new Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter();
                //lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                //DateTime lstopertimePracovnik = lstoperta.GetLastOperationDateTime(idPracovnik.id) ?? DateTime.MinValue;

				DateTime lstopertimePracovnik;
				lstopertimePracovnik = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_InternalState.GetLastOperationDateTime(idPracovnik.id) ?? DateTime.MinValue;
				


                //Datum a cas posledni oprace pracovnika je vetsi nez maximalni mozny cas pracovnika
                // => zadat datum a cas prichodu a zaznamenat prichod
                if (lstopertimePracovnik < (DateTime.Now - Settings.LoginUserTimeOut))
                {
                    using (Forms.FormUserLoginWithTimeInput formusertime = new FormUserLoginWithTimeInput())
                    {
                        if (formusertime.ShowDialog() == DialogResult.Cancel)
                            return;

                        //datum a cas posledni operace
                        lstopertimePracovnik = formusertime.UserLoginDateTime;
                        InternalState.UpdateInternalStateLstOperationUser(idPracovnik.id, lstopertimePracovnik);                         

                        //Zaznamenat udalost prihlaseni pracovnika
						//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
						//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
						Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, null, lstopertimePracovnik, Settings.UEventPracovnikPrihlaseni, idPracovnik.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                    }
                }

                //Zadani id stroje
                if (!String.IsNullOrEmpty(Settings.Production_MachineID_Preset))
                {
					//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taM = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
					//taM.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
					var dtM = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Machines(Settings.Production_MachineID_Preset);
                    if (dtM.Count() > 0)
                        idMachine = dtM[0];
                }

                if (idMachine == null)
                {
                    using (Odvadeni.FormIDMachine frmIDMachine = new Fask.Vyroba_W.Odvadeni.FormIDMachine())
                    {
                        if (frmIDMachine.ShowDialog() == DialogResult.Cancel)
                            return;

                        idMachine = frmIDMachine.Machine;
                    }
                }

                //Odvadeni
                using (Odvadeni.FormOdvadeni frmOdvadeni = new Fask.Vyroba_W.Odvadeni.FormOdvadeni(idPracovnik, idMachine))
                {
                    frmOdvadeni.ShowDialog();
                }

            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, this.Text);
                MessageBox.Show(ex.Message, this.Text);
            }
        }


        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
					//if (Settings.UEventSmenaEnabled && Globals.PracovnikVedouciSmeny != null)
					//{
                        //this.PerformClose(true);
                        //this.LogOut();
					//}
					//else
					//{
					//    this.PerformClose(true);
					//}

					PerformOdhlas();
                }
                else if (e.KeyCode == Keys.D1) //Odvod
                {
                    PerfomOdvadeni();
                }
                else if (e.KeyCode == Keys.D2) //Korekce
                {
                    PerformKorekce();
                }
                else if (e.KeyCode == Keys.D3) //Udalosti
                {
                    PerfomUdalosti();
                }
				else if (e.KeyCode == Keys.D4) //Zmena pøíkazu
				{
					if(Settings.PrepnutiZakazky)
						PerfomZmenaZakazky();
				}
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void PerformClose(bool question)
        {
            if (question)
            {
                if (MessageBox.Show("Ukonèit aplikaci?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                    return;
            }

            try
            {
                this.threadTimerDownload.Abort();
            }
            catch //(Exception exthreaddownload)
            {
            }

            try
            {
                this.threadTimerUpload.Abort();
            }
            catch //(Exception exthreadupload)
            {
            }

            Settings.Update();
            this.Close();
        }

        private void buttonOdvadeni_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerfomOdvadeni();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void buttonUdalosti_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerfomUdalosti();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void buttonOdvadeni_Click(object sender, EventArgs e)
        {
            this.PerfomOdvadeni();
        }

        System.Threading.Thread threadTimerDownload = null;
        private void timerDownloadCallBack(object state)
        {
            if (_downloadInProgress)
            {
                return;
            }

            //Nelze zacit stahovat, pokud bezi nahravani dat, posune se o 2 sec...
            timerDownloadStop();
            lock (lockUpDownTest)
            {
                if (_uploadInProgress)
                {
                    timerDownloadStart(2000);
                    return;
                }
                _downloadInProgress = true;
            }

            this.BeginInvoke(
                (DelegateVoid)delegate() { UpdateStatusBarInfo("Aktualizace:" + DateTime.Now.ToString()); });

            //timerDownloadThreadStart();
            threadTimerDownload = new Thread(new ThreadStart(timerDownloadThreadStart));
            threadTimerDownload.Name = "threadTimerDownload_" + DateTime.Now.TimeOfDay.ToString();
            threadTimerDownload.IsBackground = true;
            threadTimerDownload.Priority = ThreadPriority.Lowest;
            threadTimerDownload.Start();
        }

        private void timerDownloadThreadStart()
        {
            try
            {
                this.UpdateStatusBarInfo("Aktualizace: Pøíprava lokálního prostoru");

                try
                {
                    //if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrdTmp)))
                    //    File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrdTmp));
                    File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrdTmp));
                }
                catch (Exception ex)
                {
					// Logging.Log.Write(ex.Message, "timerDownloadCallBack(object state)");
                    // ok, ocekava se, ze pokud neexistuje, tak bude vyjimka, ale pak pokracuj
                }

                //Settings.LastDownload = DateTime.Now;

                this.UpdateStatusBarInfo("Aktualizace: Pøíprava databáze na serveru");

                //IAsyncResult ares = _wsvyroba.BeginVyrobaDBDateTime(Settings.TerminalID, Settings.LastDownload, new AsyncCallback(AsyncCallbackVyrobaDownloaded), null);
                //IAsyncResult ares = _wsvyroba.BeginVyrobaDBDateTime(Settings.TerminalID, Settings.LastDownload, null, null);
                IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginVyrobaDBDateTimePrepareZip(Settings.TerminalID, Settings.LastDownload, null, null);

                //Datum a cas posledniho downloadu predlohy
                DateTime lastDownload = DateTime.Now;

                //ceka na dokonceni asynchronni operace
                ares.AsyncWaitHandle.WaitOne();

                //Data predlohy - SQLCE databaze
                //byte[] data = _wsvyroba.EndVyrobaDBDateTime(ares);
                bool filedata = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndVyrobaDBDateTimePrepareZip(ares);
                if (!filedata)
                    throw new Exception("Prepare data on server was not succesfull");

                this.UpdateStatusBarInfo("Aktualizace: Stahování databáze ze serveru");
                //stazeni
                //bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(filedata, Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdfTmp), true, this);
                bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrd), false, true);
                if (!downloaded)
                    throw new Exception("Download from server was not succesfull");

                //Ulozeni
                //MySystem.FileOperations.DBSave(
                //    Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdfTmp),
                //    ref data
                //    );

                #region Reindexace databaze => pro sqlite mozno zrusit
                //{
                //    int krokucelkem = 13;
                //    int krokuprovedeno = 0;
                //    this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", krokuprovedeno, krokucelkem));

                //    //System.Data.SqlServerCe.SqlCeConnection sqlconn = new System.Data.SqlServerCe.SqlCeConnection(
                //    //    "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD + Constants.TMP)
                //    //    );
                //    //System.Data.SqlServerCe.SqlCeCommand sqlcomm = new System.Data.SqlServerCe.SqlCeCommand();
                //    //sqlcomm.Connection = sqlconn;

                //    try
                //    {
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Open();
                //        //sqlconn.Open();

                //        int rowsaff = 0;
                //        //corrects
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from Corrects where id=1";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from Corrects where id=1");
                //        //logins
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from Logins where id='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from Logins where id='1'");
                //        //machines
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from Machines where id='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from Machines where id='1'");
                //        //statustypes
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from StatusTypes where statusid='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from StatusTypes where statusid='1'");
                //        //vph
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from CZPRO_VPH where BarcodeH='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from CZPRO_VPH where BarcodeH='1'");
                //        //vpp
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from CZPRO_VPP where BarcodeP='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from CZPRO_VPP where BarcodeP='1'");

                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from CZPRO_VPP where SOPNUMBE='1' and ITEMNMBR='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from CZPRO_VPP where SOPNUMBE='1' and ITEMNMBR='1'");
                //        //095
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from FASK_CONS_095 where VNDITNUM='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from FASK_CONS_095 where VNDITNUM='1'");

                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from FASK_CONS_095 where CZ_CarKod='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery(); 
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from FASK_CONS_095 where CZ_CarKod='1'");

                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from FASK_CONS_095 where ITEMNMBR='1'";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from FASK_CONS_095 where ITEMNMBR='1'");
                //        //vyroba_TP
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from FASK_Vyroba_TP where ITEMNMBR_Def='1' and ID_H='1' ";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from FASK_Vyroba_TP where ITEMNMBR_Def='1' and ID_H='1' ");

                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //sqlcomm.CommandText = "Select Count(*) from FASK_Vyroba_TP where ID_H='1' ";
                //        //rowsaff = sqlcomm.ExecuteNonQuery();
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Reindexace_Command("Select Count(*) from FASK_Vyroba_TP where ID_H='1' ");
                //    }
                //    catch (Exception ex)
                //    {
                //        LoggingCE.Log.Write(ex);
                //    }
                //    finally
                //    {
                //        this.UpdateStatusBarInfo(String.Format("Aktualizace: Reindexace databáze [{0}/{1}]", ++krokuprovedeno, krokucelkem));
                //        //if (sqlconn != null && (sqlconn.State & ConnectionState.Open) == ConnectionState.Open)
                //        //{
                //        //    sqlconn.Close();
                //        //    sqlconn.Dispose();
                //        //    sqlconn = null;
                //        //}
                //        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Close();
                //    }
                //}
                #endregion

                this.UpdateStatusBarInfo("Aktualizace: Synchronizace odvedených kusù");

                //Pokud existuje production tmp, tak doplnit aktualni stav
                // Pokud bylo do production.prd pridano neco behem synchronizace, tak je treba zajistit doplneni do vyroba_vpp

                // productionTmpPrd jiz je pouze jako zaloha struktury, odesilani je pres production_guid.prd.tmp //if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrdTmp)))
                // !!! synchronizace: stahovani nebo odesilani dat !!! NESMI BYT SOUCASNE !!!
                if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd)))
                {
                    //Dohleda vsechny zmeny k datu posledniho downloadu...???
                    //DataTable dt = new DataTable();
                    //System.Data.SqlServerCe.SqlCeDataAdapter sqlceda = new System.Data.SqlServerCe.SqlCeDataAdapter(
                    //    "SELECT  SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO, MAX(dateeve) as LSTMod " +
                    //    "FROM Production " +
                    //    "WHERE (dateeve > @dateeve) " +
                    //    "GROUP BY SOPNUMBE, ITEMNMBR",
                    //    "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP));
                    //sqlceda.SelectCommand.Parameters.Add("@dateeve", SqlDbType.DateTime).Value = lastDownload;
                    //sqlceda.SelectCommand.Connection.Open();
                    //sqlceda.Fill(dt);
                    //sqlceda.SelectCommand.Connection.Close();

                    //var dt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRDTMP.GetProductionUpdateByDateEve_Production(lastDownload);
                    //var dt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetProductionUpdateByDateEve_Production(lastDownload);
                    var dt = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetProductionUpdate();

                    //aktualizace predlohy na zaklade aktualniho stavu
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter vppta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
                    //vppta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD + Constants.TMP);                    
                    Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Open();
                    foreach (SQLiteDBs.DataSets.Vyroba.Production_updateRow row in dt)
                    {
                        Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(row.QTYODVEDENO, row.CNTODVEDENO, row.LSTMod, row.CountEntries, row.SOPNUMBE, row.ORD, row.ITEMNMBR, row.ITEMTYPE, row.QTYPACK, row.BarcodeP);
                    }
                    Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Close();
                }

                //SynchronizeDatabases(SynchronizationType.Souborove);
                //SynchronizeDatabases(SynchronizationType.Databazove);
                //this.BeginInvoke((DelegateVoid)delegate() { SynchronizeDatabases(SynchronizationType.Souborove); });
                //SynchronizeDatabases(SynchronizationType.Souborove);    // toto musi probehnout v ramci stahovaciho vlakna
                SynchronizeDatabases();    // toto musi probehnout v ramci stahovaciho vlakna

                Settings.LastDownload = lastDownload; //Last success download time                

                //System.Data.SqlServerCe.SqlCeEngine sceengine = new System.Data.SqlServerCe.SqlCeEngine("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf));
                //try
                //{
                //    sceengine.Verify();
                //}
                //catch 
                //{
                //}
                //try
                //{
                //    sceengine.Shrink();
                //}
                //catch 
                //{
                //}
                //try
                //{
                //    sceengine.Repair(
                //sceengine.LocalConnectionString,
                //System.Data.SqlServerCe.RepairOption.RecoverAllPossibleRows
                //);

                //}
                //catch 
                //{
                //}
            }
            catch (Exception ex)
            {
				try { Logging.Log.Write(ex.Message, "AsyncCallbackVyrobaDownloaded(IAsyncResult ar)"); }
                catch { }
            }
            finally
            {
                _downloadInProgress = false;
                try { this.BeginInvoke(new DelegateUpdateForm(UpdateForm)); }
                catch { }

                //timerDownloadStart();
                try { this.BeginInvoke(new DelegateVoid(timerDownloadStart)); }
                catch { }
            }
        }

        /// <summary>
        /// Downloads Production.Prd from server
        /// </summary>
        private void DownloadProductionStart()
        {
            try
            {
                DateTime lastDownload = DateTime.Now;
                // priprava sdf na serveri
				IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginVyrobaDBProductionPrepareZip(Settings.TerminalID, lastDownload, null, null);
                //ceka na dokonceni asynchronni operace
                ares.AsyncWaitHandle.WaitOne();

                //Data predlohy - SQLCE databaze
                //byte[] data = _wsvyroba.EndVyrobaDBDateTime(ares);
				bool filedata = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndVyrobaDBProductionPrepareZip(ares);
                if (!filedata)
                    throw new Exception("Prepare data on server was not succesfull");

                //this.UpdateStatusBarInfo("Aktualizace: Stahování databáze ze serveru");
                //stazeni
                //bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(filedata, Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf), true);
				bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd), false, true);
                if (!downloaded)
                    throw new Exception("Download Production from server was not succesfull");          


            }
            catch (Exception ex)
            {
				try { Logging.Log.Write(ex.Message, "AsyncCallbackVyrobaDownloadedProduction(IAsyncResult ar)"); }
                catch { }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Downloads InternalState.prd from server
        /// </summary>
        private void DownloadInternalStateStart()
        {
            try
            {
                DateTime lastDownload = DateTime.Now;
                // priprava sdf na serveri
				IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginVyrobaDBInternalStatePrepareZip(Settings.TerminalID, lastDownload, null, null);
                //ceka na dokonceni asynchronni operace
                ares.AsyncWaitHandle.WaitOne();

                //Data predlohy - SQLCE databaze
                //byte[] data = _wsvyroba.EndVyrobaDBDateTime(ares);
				bool filedata = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndVyrobaDBInternalStatePrepareZip(ares);
                if (!filedata)
                    throw new Exception("Prepare data on server was not succesfull");
                //this.UpdateStatusBarInfo("Aktualizace: Stahování databáze ze serveru");
                //stazeni
                //bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(filedata, Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf), true);
				bool downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD), false, true);
                if (!downloaded)
                    throw new Exception("Download InternalState from server was not succesfull");


            }
            catch (Exception ex)
            {
				try { Logging.Log.Write(ex.Message, "AsyncCallbackVyrobaDownloadedInternalState(IAsyncResult ar)"); }
                catch { }
            }
            finally
            {

            }
        }


        private void SynchronizeDatabases()
        {
            this.UpdateStatusBarInfo("Aktualizace: Synchronizace databáze");
            Application.DoEvents();

            File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrd));
            File.Copy(
                Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrdTmp),
                Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrd),
                true
                );
            File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaPrdTmp));

            UpdateStatusBarInfo();
        }

        System.Threading.Thread threadTimerUpload = null;
        private void timerUploadCallBack(object state)
        {
            if (_uploadInProgress)
            {
                return;
            }

            //Nelze zacit nahravat data, pokud bezi stahovani, posune se za 2 sec...
            timerUploadStop();
            lock (lockUpDownTest)
            {
                if (_downloadInProgress)
                {
                    timerUploadStart(2000);
                    return;
                }
                _uploadInProgress = true;
            }

            //timerUploadThreadStart();
            threadTimerUpload = new Thread(new ThreadStart(timerUploadThreadStart));
            threadTimerUpload.Name = "threadTimerUpload_" + DateTime.Now.TimeOfDay.ToString();
            threadTimerUpload.IsBackground = true;
            threadTimerUpload.Priority = ThreadPriority.Lowest;
            threadTimerUpload.Start();
        }

        private void timerUploadThreadStart()
        {
            try
            {
                // delete all resistent "Production_[.*].prd.tmp files
                //FileInfo fi = new FileInfo(MySystem.MyPath.DataDirectory);
                var resistentFiles = Directory.GetFiles(MySystem.MyPath.DataDirectory, "Production_*.prd.tmp");
                foreach (var resFile in resistentFiles)
                {
                    File.Delete(resFile);
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "timerUploadThreadStart() removing resistent old Production_*.prd.tmp files");
            }

            Guid guid = Guid.NewGuid();
            string filename = Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP);

            try
            {
                try
                {
                    File.Copy(
						Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd),
						Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP),
                        true
                        );
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex.Message, "timerUploadThreadStart(object state):File.Copy");
                    throw ex;
                }


				//string URL = Settings.WebServiceAddressVyroba + "Upload.aspx";
				string Odkud = filename + Constants.ZIP;
				string Kam = Settings.TerminalID + "\\" + Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP + Constants.ZIP;

				Fask.Vyroba_W.FileTransfer.CompressFile.CompressToZip(filename, Odkud);

				if (Fask.Vyroba_W.FileTransfer.Uploading.SendFile_API(Odkud, Kam) != "OK")
					throw new Exception("Nepodaøilo se odeslat davku");

				//if (Fask.Vyroba_W.FileTransfer.Uploading.SendFileCalcTime(URL, Odkud, Kam) != "OK")
				//    throw new Exception("Nepodaøilo se odeslat davku");

				IAsyncResult ares = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.BeginProcessProductionData2(
                    Settings.TerminalID,
					guid,
                    null,
                    null
                    );

                ares.AsyncWaitHandle.WaitOne();

				bool processed = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndProcessProductionData2(ares);
                if (processed)
                {

                    //Odmazani odvedenych dat z Production                    
                    FormMain.Instance_FormMain.globalObject.DeleteProductionByGuid(filename);

                    //Odmazani odvedenych dat z Production_SN
                    FormMain.Instance_FormMain.globalObject.DeleteProduction_SNByGuid(filename);

                    //Odmazani odvedenych dat z Production_Sources
                    FormMain.Instance_FormMain.globalObject.DeleteProduction_SourcesByGuid(filename);

                    //Odmazani odvedenych dat z UserEvents
                    FormMain.Instance_FormMain.globalObject.DeleteUserEventsByGuid(filename);
                }

                this.UpdateStatusBarInfo("Data odeslána: " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                try
                {
                    Logging.Log.Write(ex.Message, "timerUploadThreadStart End");
                    this.UpdateStatusBarInfo("Odeslání dat se nezdaøilo: " + DateTime.Now.ToString() + ex.Message);
                }
                catch { }
            }
            finally
            {

                //Vzdy ho smazu, protoze uz tento tmp neni dulezity, vsechno se provedlo a uz k nemu nikdy nepristoupim...
                try { File.Delete(filename); }
                catch (Exception exDeleteFile)
                {
                    Logging.Log.Write(exDeleteFile, "timerUploadThreadStart Delete '{" + filename + "}' file");
                }

                try { _uploadInProgress = false; }
                catch { }
                //timerUploadStart();
                try { this.BeginInvoke(new DelegateVoid(timerUploadStart)); }
                catch { }
            }
        }

        private void menuItemTimeSynchronization_Click(object sender, EventArgs e)
        {
            SystemTimeSynchronize();
        }

        private void SystemTimeSynchronize()
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            try
            {
				DateTime servertime = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ServerDateTime();
                MySystem.SystemDateTime.SetDeviceTime(servertime);
            }
            catch (Exception ex)
            {
				Logging.Log.Write(ex.Message, "SystemTimeSynchronize()");
                //MessageBox.Show(ex.Message);
            }
            finally
            {
            }

            Cursor.Current = Cursors.Default;
        }

        private void FormMain_Closing(object sender, CancelEventArgs e)
        {
            Settings.Update();

            timerUploadStop();
            timerDownloadStop();

            if (threadTimerDownload != null)
            {
                try { threadTimerDownload.Join(1000); }
                catch { }
                try { threadTimerDownload.Abort(); }
                catch { }
                threadTimerDownload = null;
            }

            if (Scanner != null)
            {
                Scanner.TerminateScanner();
                Scanner = null;
            }
        }

        delegate void DelegateUpdateForm();
        private void UpdateForm()
        {
            this.UpdateStatusBarInfo();
        }

        delegate void DelegateUpdateStatusBarInfo(string text);
        private void UpdateStatusBarInfo()
        {
            UpdateStatusBarInfo("Aktualizováno:" + Settings.LastDownload.ToString());
        }
        public void UpdateStatusBarInfo(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((DelegateVoid)delegate() { UpdateStatusBarInfo(text); });
                return;
            }
            statusBarInfo.Text = text;
        }

        //private void UpdateStatusBarInfo2()
        //{
        //    // aktualizace zobrazeni stavu
        //    StringBuilder sb = new StringBuilder();
        //    //prihlaseny vedouci smeny???
        //    //prihlaseny uzivatel
        //    if (Globals.Pracovnik != null)
        //    {
        //        sb.AppendFormat("P:{0};",Globals.Pracovnik.id.Trim()); // TODO: jmeno, prijmeni???
        //    }

        //    // Aktivni zakazka ...
        //    if (Globals.Zakazka != null)
        //    {
        //        sb.AppendFormat("Z:", Globals.Zakazka.SOPNUMBE);
        //    }

        //}

        private void menuItemDownloadDatabase_Click(object sender, EventArgs e)
        {
            timerDownloadCallBack(null);
        }

        private void menuItemPrihlasitOdhlasit_Click(object sender, EventArgs e)
        {
			PerformOdhlas();
        }

		private void PerformOdhlas()
		{
			if (!this.LogOut())
				return;
			this.BeginInvoke((DelegateVoid)delegate() { LogIn(); });
		}

        private void menuItemConfiguration_Click(object sender, EventArgs e)
        {
            using (FormConfig frmConfig = new FormConfig())
            {
                if (frmConfig.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
						Logging.Log.Enable = Settings.Loging;
                    }
                    catch (Exception ex)
                    {
						Logging.Log.Write(ex.Message, this.Text + " : Logging");
                    }

                    try
                    {
						Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Url = Settings.WebServiceAddressVyroba + Constants.Vyroba_asmx;
						Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Timeout = Settings.TimeOut;
                    }
                    catch (Exception ex)
                    {
						Logging.Log.Write(ex.Message, this.Text + " : WSVyroba");
                    }

                    //menuItemPrihlasitOdhladit.Enabled = Settings.UEventSmenaEnabled;
                }
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(this.Width / 2, this.Height);
            panel1.Size = nsize;

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panel1.Width, panel1.Height / 2);
            buttonOdvadeni.Size = nsize;
            buttonTisk.Size = nsize;
        }

        private void buttonKorekce_Click(object sender, EventArgs e)
        {
            this.PerformKorekce();
        }

        private void buttonKorekce_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerformKorekce();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void menuItemOdeslatData_Click(object sender, EventArgs e)
        {
            timerUploadCallBack(null);
        }

        private void buttonTisk_Click(object sender, EventArgs e)
        {
            PerformTisk();
        }

        private void PerformTisk()
        {
            try
            {
                using (Tisk.Baleni.FormBaleniMain fbm = new Fask.Vyroba_W.Tisk.Baleni.FormBaleniMain(buttonTisk.BackColor))
                {
                    fbm.ShowDialog();
                }
            }
            catch (Exception exTisk)
            {
                Logging.ExceptionHandler2.Handle(exTisk, true);
            }
        }


		private void PerfomZmenaZakazky()
		{
			try
			{
				using (Odvadeni.FormPrikazVyber fpv = new Fask.Vyroba_W.Odvadeni.FormPrikazVyber())
				{
					var dtP = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetData_CZPRO_VPH();

					fpv.Owner = this;
					fpv.Text = "Pøíkaz";
					fpv.Prikazy = dtP.ToList();
					if (DialogResult.OK == fpv.ShowDialog())
						Globals.Zakazka = fpv.PrikazVybrany;
					else
					{
						Globals.Zakazka = null;
						return;
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

    }
}

