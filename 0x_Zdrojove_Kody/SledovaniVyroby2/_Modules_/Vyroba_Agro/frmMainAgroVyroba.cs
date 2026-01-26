using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using System.IO;
using System.Threading;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;
using Vyroba_Agro;
using FASK.SledovaniVyroby.ModuleIfc;
using SQLCECommLib.DSVyrobaSQLCETableAdapters;
using Fask.Emailing;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public partial class frmMainAgroVyroba : Form, IModuleConnector
    {
        //Promenne pro praci s databazi - inicializace v konstruktoru
        public FASK.SledovaniVyroby.IScannerProvider.IScannerProvider Scanner = null;
        private string scannerTypeName = string.Empty;
        private System.Threading.Timer timerRefreshUI = null;

        private InformationUC operace = null;
        private KeyboardUC klavesnice = null;

        private DataVyroba dataVyroba = null;
        private Adam60XX adam = null;

        //private Classes.FullScreen full = null;

        public frmMainAgroVyroba()
        {
            try
            {
                InitializeComponent();

                InitUserInfo();

                Log.Write("Spusteni modulu");

                this.lblMachineID.Text = Logging.LogConfig.MachineID.Trim();

                //nacteni konfigurace - importovat konfiguracni parametry ze stare verze, pytle...
                LoadConfiguration();

                AgroConfig.pathDllLibraryDatabase = Logging.LogConfig.SqlConnectionStringLocal;

                //Instance datasetu
                //dataVyroba = new DataVyroba(4, AgroConfig.config.Agro[0].PotvrzovaciCidloCislo, Scanner);
                dataVyroba = new DataVyroba(4, AgroConfig.config.Agro[0].PotvrzovaciCidloCislo);

                adam = new Adam60XX(
                    AgroConfig.config.Agro[0].IPAdresaAdam, 
                    AgroConfig.config.Agro[0].PotvrzovaciCidloCislo, 
                    AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam, 
                    AgroConfig.config.Agro[0].AdamTCPTimeout,
                    AgroConfig.config.Agro[0].CisloReleLinkaAdam, 
                    AgroConfig.config.Agro[0].CisloReleHoukackaAdam, 
                    AgroConfig.config.Agro[0].MinHighSignalWidthAdam, 
                    AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz
                    );

                AdamStart();

                dataVyroba.setAdam(adam);

                operace = new InformationUC(dataVyroba);
                panelInformation.Controls.Add(operace);
                operace.Zmena += new InformationUC.ZmenaEventHandler(operace_Zmena);

                klavesnice = new KeyboardUC(operace);
                panelKeyboard.Controls.Add(klavesnice);

                dataVyroba.SetStavVyroba(DataVyroba.VyrobaStavy.LogIDSmena);
                dataVyroba.ZmenaStavu += new DataVyroba.StavZmenaEventHandler(vyroba_ZmenaStavu);


                LoadAssembliesScanner();

                ScannerStart();

                ShowPrihlasenyUzivatel();

                timerRefreshUI = new System.Threading.Timer(TimerCallback, null, 0, 1000);

                //full = new FullScreen(this);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void InitUserInfo()
        {
            lblDI0.Text = "X";
            lblDI0.ForeColor = Color.Red;
            lblDI1.Text = "X";
            lblDI1.ForeColor = Color.Red;
            lblDI2.Text = "X";
            lblDI2.ForeColor = Color.Red;
            lblDI3.Text = "X";
            lblDI3.ForeColor = Color.Red;
            lblDI4.Text = "X";
            lblDI4.ForeColor = Color.Red;
            lblDI5.Text = "X";
            lblDI5.ForeColor = Color.Red;

            lblRL0.Text = "X";
            lblRL0.ForeColor = Color.Red;
            lblRL1.Text = "X";
            lblRL1.ForeColor = Color.Red;
            lblRL2.Text = "X";
            lblRL2.ForeColor = Color.Red;
            lblRL3.Text = "X";
            lblRL3.ForeColor = Color.Red;
            lblRL4.Text = "X";
            lblRL4.ForeColor = Color.Red;
            lblRL5.Text = "X";
            lblRL5.ForeColor = Color.Red;

            lblScannerCode.Text = "X";
            lblScannerCode.ForeColor = Color.Red;
            lblScannerCodeValue.Text = string.Empty;

        }

        private void OffTimer()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(System.Threading.Timeout.Infinite, 1000);
        }

        private void OnTimer()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(1000, 1000);
        }

        private void TimerCallback(object state)
        {
            OffTimer();

            RefreshAdamData();

            OnTimer();
        }

        private void RefreshAdamData()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.RefreshAdamData();
                });
                return;
            }

            bool[] DIData = null;

            bool[] RLData = null;

            try
            {
                if (adam != null)
                {
                    DIData = adam.getData();
                    RLData = adam.getRLData();
                }

            }
            catch (Exception ex)
            {
                // TODO : vyjimka !!!
                Log.Write(ex.Message.ToString());
            }

            if (DIData != null && DIData.Length > 0)
            {
                #region nastaveni labelu
                if (DIData[0])
                {
                    lblDI0.Text = "X";
                    lblDI0.ForeColor = Color.Green;
                }
                else
                {
                    lblDI0.Text = "X";
                    lblDI0.ForeColor = Color.Red;
                }

                if (DIData[1])
                {
                    lblDI1.Text = "X";
                    lblDI1.ForeColor = Color.Green;
                }
                else
                {
                    lblDI1.Text = "X";
                    lblDI1.ForeColor = Color.Red;
                }

                if (DIData[2])
                {
                    lblDI2.Text = "X";
                    lblDI2.ForeColor = Color.Green;
                }
                else
                {
                    lblDI2.Text = "X";
                    lblDI2.ForeColor = Color.Red;
                }

                if (DIData[3])
                {
                    lblDI3.Text = "X";
                    lblDI3.ForeColor = Color.Green;
                }
                else
                {
                    lblDI3.Text = "X";
                    lblDI3.ForeColor = Color.Red;
                }

                if (DIData[4])
                {
                    lblDI4.Text = "X";
                    lblDI4.ForeColor = Color.Green;
                }
                else
                {
                    lblDI4.Text = "X";
                    lblDI4.ForeColor = Color.Red;
                }


                if (DIData[5])
                {
                    lblDI5.Text = "X";
                    lblDI5.ForeColor = Color.Green;
                }
                else
                {
                    lblDI5.Text = "X";
                    lblDI5.ForeColor = Color.Red;
                }
                #endregion
            }


            if (RLData != null && RLData.Length > 0)
            {
                #region nastaveni labelu
                if (RLData[0])
                {
                    lblRL0.Text = "X";
                    lblRL0.ForeColor = Color.Green;
                }
                else
                {
                    lblRL0.Text = "X";
                    lblRL0.ForeColor = Color.Red;
                }

                if (!RLData[1])
                {
                    lblRL1.Text = "X";
                    lblRL1.ForeColor = Color.Green;
                }
                else
                {
                    lblRL1.Text = "X";
                    lblRL1.ForeColor = Color.Red;
                }

                if (RLData[2])
                {
                    lblRL2.Text = "X";
                    lblRL2.ForeColor = Color.Green;
                }
                else
                {
                    lblRL2.Text = "X";
                    lblRL2.ForeColor = Color.Red;
                }

                if (RLData[3])
                {
                    lblRL3.Text = "X";
                    lblRL3.ForeColor = Color.Green;
                }
                else
                {
                    lblRL3.Text = "X";
                    lblRL3.ForeColor = Color.Red;
                }

                if (RLData[4])
                {
                    lblRL4.Text = "X";
                    lblRL4.ForeColor = Color.Green;
                }
                else
                {
                    lblRL4.Text = "X";
                    lblRL4.ForeColor = Color.Red;
                }


                if (RLData[5])
                {
                    lblRL5.Text = "X";
                    lblRL5.ForeColor = Color.Green;
                }
                else
                {
                    lblRL5.Text = "X";
                    lblRL5.ForeColor = Color.Red;
                }
                #endregion
            }

            lblScannerCode.Text = "X";
            lblScannerCode.ForeColor = Color.Black;
        }

        void operace_Zmena()
        {
            Log.Write("operace_Zmena() - Houkacka OFF");
            this.dataVyroba.SetLinkaState(false);  //vypneme linku
            this.dataVyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
            this.dataVyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);//vynulujeme citac

            this.tabControl1.SelectedIndex = 2;
            
            NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabControl1.TabPages[2].Width, this.tabControl1.TabPages[2].Height);
            this.tabControl1.TabPages[2].DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabControl1.TabPages[2].ClientRectangle);
            NotificationMail.bitmapstavodhlaseni.Save("bitmapstavodhlaseni.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            this.buttonStateRead_Click(null, null);
            this.buttonStateSave_Click(null, null);
            //this.tabControl1.TabPages[2].Show();
        }

        private void vyroba_ZmenaStavu(DataVyroba.VyrobaStavy novystav)
        {
            updateForm();
        }


        private void updateForm()
        {
            try
            {
                if (this.InvokeRequired)
                {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                    this.BeginInvoke(new MethodInvoker(() => { updateForm(); }));
                }
                else
                {
                    if (dataVyroba.BarcodeActual.Equals(""))
                        lblBarcodeActual.Text = "NONE";
                    else
                        lblBarcodeActual.Text = dataVyroba.BarcodeActual;
                    

                    if (dataVyroba.aktualDataRow != null)
                    {
                        if (dataVyroba.BarcodeActual == string.Empty)
                        {
                            lblReadCodeCnt.Text = "0";

                            lblNoReadCodeCnt.Text = dataVyroba._list_NoRead_Count > 0 ? dataVyroba._list_NoRead_Count.ToString() : "0";
                        }
                        else
                        {
                            if (dataVyroba.aktualDataRow.RowState != DataRowState.Deleted &&
                                dataVyroba.aktualDataRow.RowState != DataRowState.Detached)
                            {
                                lblReadCodeCnt.Text = dataVyroba.aktualDataRow.qty.ToString();
                                lblNoReadCodeCnt.Text = dataVyroba.aktualDataRow.qtyReal.ToString();
                            }
                        }

                    }
                    else
                    {
                        if (dataVyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.LogIDPracovnik || dataVyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.LogIDSmena)
                        {
                            lblReadCodeCnt.Text = dataVyroba.CodeReadCnt.ToString();
                            lblNoReadCodeCnt.Text = dataVyroba.CodeNoReadCnt.ToString();
                        }
                        else
                        {
                            lblReadCodeCnt.Text = "0";
                            lblNoReadCodeCnt.Text = dataVyroba._list_NoRead_Count.ToString();
                        }
                    }

                    lblIDSmena.Text = dataVyroba.GetSmenaId();

                    //lblCodeNoReadCnt.Text = dataVyroba.CodeNoReadCnt.ToString();
                    //lblCodeReadCnt.Text = dataVyroba.CodeReadCnt.ToString();

                    if (dataVyroba.ActualPytel != null)
                        lblAktualniPytel.Text = dataVyroba.ActualPytel.Nazev;
                }
            }
            catch (Exception ex)
            {
                lblReadCodeCnt.Text = lblNoReadCodeCnt.Text = "-";
                MessageBox.Show("Nastala chyba (problem): " + ex.Message +"\n " + (dataVyroba.aktualDataRow == null? "null" : dataVyroba.aktualDataRow.RowState.ToString()), "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ShowPrihlasenyUzivatel()
        {
            if (this.InvokeRequired)
            {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                this.BeginInvoke(new MethodInvoker(() => { ShowPrihlasenyUzivatel(); }));
            }
            else
            {
                this.lblIDSmena.Text = dataVyroba.GetSmenaId();
            }

        }

        private void LoadConfiguration()
        {
            //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
            if (AgroConfig.config.Agro.Rows.Count == 0)
            {
                AgroConfig.config.Agro.AddAgroRow(0, "1", 60, "998", "192.168.1.3", "", 120, 350, 100, 5, 5, 2, 10, 4, 1,2, 2, 3, 100, false, false, string.Empty, string.Empty);

                //Ulozeni
                AgroConfig.Save();
            }

            try
            {
                //Vlozeni do textboxuu
                txtPrihlasenaSmenaPosledni.Text = AgroConfig.config.Agro[0].PosledniPrihlasenaSmena;
                txtAckSensor.Text = AgroConfig.config.Agro[0].PotvrzovaciCidloCislo.ToString();
                txtIDMimoSmenu.Text = AgroConfig.config.Agro[0].OdvodMimoSmenuID;
                txtZahaleniGenerate.Text = AgroConfig.config.Agro[0].ZahaleniStartSekund.ToString();
                txtIPAdresaAdam.Text = AgroConfig.config.Agro[0].IPAdresaAdam;
                txtPathToPytleConfigFile.Text = AgroConfig.config.Agro[0].PathToPytleConfigFile;
                txtIntervalUkladaniDat.Text = AgroConfig.config.Agro[0].IntervalUkladaniDat.ToString();
                txtTimerPeriodAdam.Text = AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam.ToString();
                txtAdamTCPTimeout.Text = AgroConfig.config.Agro[0].AdamTCPTimeout.ToString();
                txtPocetNepruchoduReleHoukacky.Text = AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky.ToString();
                txtCidloHlidaniStavLinky.Text = AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz.ToString();
                txtPocetPruchZmenDoProhaz.Text = AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz.ToString();

                txtAdamReleHoukacka.Text = AgroConfig.config.Agro[0].CisloReleHoukackaAdam.ToString();
                txtAdamReleLinka.Text = AgroConfig.config.Agro[0].CisloReleLinkaAdam.ToString();
                txtMinHighSignalWidth.Text = AgroConfig.config.Agro[0].MinHighSignalWidthAdam.ToString();

                txtPocetU36.Text = AgroConfig.config.Agro[0].MaxPocetUdalosti36.ToString();
                txtInterU36.Text = AgroConfig.config.Agro[0].CasovyIntervalUdalosti36.ToString();
                txtPocetU11.Text = AgroConfig.config.Agro[0].MaxPocetUdalosti11.ToString();
                txtInterU11.Text = AgroConfig.config.Agro[0].CasovyIntervalUdalosti11.ToString();

                checkBoxEmailKonecSmeny.Checked = AgroConfig.config.Agro[0].KonecSmenyEmailSend;
                txtSarzePassword.Text = AgroConfig.config.Agro[0].IsSarzePasswordNull() ? string.Empty : Encoding.Default.GetString(Convert.FromBase64String(AgroConfig.config.Agro[0].SarzePassword));
            }
            catch { }
        }

        private void LoadAssembliesScanner()
        {
            Scanner = FASK.SledovaniVyroby.ScannerFactory.ScannerFactory.Init();
            scannerTypeName = FASK.SledovaniVyroby.ScannerFactory.ScannerFactory.GetScannerTypeName();
        }

        private void CloseTerminateScanner()
        {
            if (Scanner != null)
            {
                ScannerStop();

                Scanner.Disable();
                Scanner.TerminateScanner();
            }
        }


        private void AdamStart()
        {
            try
            {
                if (adam != null)
                {
                    adam.DataReady -= new Adam60XX.AdamEventHandler(Adam_DataReady);
                    adam.DataReady += new Adam60XX.AdamEventHandler(Adam_DataReady);
                }

            }
            catch { }

            try
            {
                adam.Adam60XXTimerWorkingEvent -= new Adam60XX.Adam60XXTimerWorkingHandler(adam_Adam60XXTimerWorkingEvent);
                adam.Adam60XXTimerWorkingEvent += new Adam60XX.Adam60XXTimerWorkingHandler(adam_Adam60XXTimerWorkingEvent);
            }
            catch 
            {
                
                throw;
            }
        }

        void adam_Adam60XXTimerWorkingEvent()
        {
            // TODO : doplnit sledovani timeru adama a indikace na obrazovce ...
            //this.BeginInvoke((MethodInvoker)delegate() {
            //    this.updateForm();
            //});
        }

        private void AdamStop()
        {
            try
            {
                if (adam != null)
                {
                    adam.DataReady -= new Adam60XX.AdamEventHandler(Adam_DataReady);
                    adam.close();
                }

            }
            catch { }

            try
            {
                adam.Adam60XXTimerWorkingEvent -= new Adam60XX.Adam60XXTimerWorkingHandler(adam_Adam60XXTimerWorkingEvent);
            }
            catch 
            {

                throw;
            }
        }

        private void Adam_DataReady(FASK.SledovaniVyroby.Module.Vyroba_Agro.Adam60XX.AdamEventHandlerArgs e)
        {
            this.BeginInvoke(new AdamSensorDelegate(AdamScannerDataReady), new object[] { e.Data });
        }

        private void AdamScannerDataReady(int sensor)
        {
            // zde se reaguje na zmenu stavu cidla
            // pokud je to potvrzovaci cidlo, tak  ze provede akce potvrzeni            
            if (sensor == AgroConfig.config.Agro[0].PotvrzovaciCidloCislo)
                dataVyroba.SensorAck(true);

            // nakonec se zvedne interni citac sepnuti vsech cidel pro dany senzor
            dataVyroba.inkrementSensorAcitvatedCount(sensor);

            updateForm();
        }

        private void ScannerStart()
        {
            try
            {
                if (Scanner != null)
                {
                    Scanner.DataReady -= new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.DataReady += new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.Enable();
                }

            }
            catch { }
        }

        private void ScannerStop()
        {
            try
            {
                if (Scanner != null)
                {
                    Scanner.DataReady -= new FASK.SledovaniVyroby.IScannerProvider.ScannerEventHandler(Scanner_DataReady);
                    Scanner.Disable();
                }

            }
            catch { }
        }

        void Scanner_DataReady(object sender, FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { e });
        }

        delegate void BarcodeReadedDelegate(FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e);


        delegate void AdamSensorDelegate(int e);

        //Prijem dat
        private void ScannerDataReceived(FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData;

            lblScannerCodeValue.Text = barcode;
            if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.NoData)
            {
                lblScannerCode.Text = "X";
                lblScannerCode.ForeColor = Color.Black;
            }
            if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.NoRead)
            {
                lblScannerCode.Text = "N";
                lblScannerCode.ForeColor = Color.Red;
            }
            else if (e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.Read)
            {
                lblScannerCode.Text = "R";
                lblScannerCode.ForeColor = Color.Green;
            }
            else 
                //if (
                //e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.BadRead
                //||
                //e.ScannedCodeType == FASK.SledovaniVyroby.IScannerProvider.Code.TooLong
                //)
            {
                lblScannerCode.Text = "E";
                lblScannerCode.ForeColor = Color.DarkOrange;
            } 

            dataVyroba.ScannerActivate(barcode, e.ScannedCodeType);

            dataVyroba.inkrementScannerAcitvatedCount();

            updateForm();
        }

        #region IModuleConnector Members

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                fASKEventsBindingSource.DataSource = dataVyroba.GetDataVyrobaHistory().FASK_Events;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if ((AgroConfig.config.Agro[0].IsPasswordNull() || String.IsNullOrEmpty(AgroConfig.config.Agro[0].Password)) && !String.IsNullOrEmpty(txtPassword.Text))
            AgroConfig.config.Agro[0].Password = Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None);

            AgroConfig.config.Agro[0].PosledniPrihlasenaSmena = txtPrihlasenaSmenaPosledni.Text.Trim();
            AgroConfig.config.Agro[0].PotvrzovaciCidloCislo = int.Parse(txtAckSensor.Text);
            AgroConfig.config.Agro[0].OdvodMimoSmenuID = txtIDMimoSmenu.Text;
            AgroConfig.config.Agro[0].ZahaleniStartSekund = int.Parse(txtZahaleniGenerate.Text);
            AgroConfig.config.Agro[0].IPAdresaAdam = txtIPAdresaAdam.Text;
            AgroConfig.config.Agro[0].PathToPytleConfigFile = txtPathToPytleConfigFile.Text;
            AgroConfig.config.Agro[0].IntervalUkladaniDat = int.Parse(txtIntervalUkladaniDat.Text);
            AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam = int.Parse(txtTimerPeriodAdam.Text);
            AgroConfig.config.Agro[0].AdamTCPTimeout = int.Parse(txtAdamTCPTimeout.Text);
            AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky = int.Parse(txtPocetNepruchoduReleHoukacky.Text);
            AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz = int.Parse(txtCidloHlidaniStavLinky.Text);
            AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz = int.Parse(txtPocetPruchZmenDoProhaz.Text);

            AgroConfig.config.Agro[0].CisloReleHoukackaAdam = int.Parse(txtAdamReleHoukacka.Text);
            AgroConfig.config.Agro[0].CisloReleLinkaAdam = int.Parse(txtAdamReleLinka.Text);
            AgroConfig.config.Agro[0].MinHighSignalWidthAdam = int.Parse(txtMinHighSignalWidth.Text);

            AgroConfig.config.Agro[0].MaxPocetUdalosti11 = int.Parse(txtPocetU11.Text);
            AgroConfig.config.Agro[0].CasovyIntervalUdalosti11 = int.Parse(txtInterU11.Text);
            AgroConfig.config.Agro[0].MaxPocetUdalosti36 = int.Parse(txtPocetU36.Text);
            AgroConfig.config.Agro[0].CasovyIntervalUdalosti36 = int.Parse(txtInterU36.Text);

            AgroConfig.config.Agro[0].KonecSmenyEmailSend = checkBoxEmailKonecSmeny.Checked;
            
            AgroConfig.config.Agro[0].SarzePassword = Convert.ToBase64String(Encoding.Default.GetBytes(txtSarzePassword.Text), Base64FormattingOptions.None);


            //Ulozeni do xml
            AgroConfig.Save();

            groupBoxAdam.Enabled =
                groupBoxEmail.Enabled =
                groupBoxObecne.Enabled =
                groupBoxUdalosti.Enabled =
                buttonUlozitNastaveni.Enabled = false;
            txtPassword.Text = string.Empty;

        }

        #region IModuleConnector Members


        public void ReturnPortsToPreviousState()
        {
        }

        public void ClosePorts()
        {
            try
            {
                OffTimer();
                timerRefreshUI = null;

                dataVyroba.CloseTimer();
                CloseTerminateScanner();
                AdamStop();
                Log.Write("Ukonceni modulu");
            }
            catch
            {}
        }

        #endregion

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        #region IModuleConnector Members


        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;

            if (dataVyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.LogIDSmena)
            {
                message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
                return false;
            }
            else
            {
                dataVyroba.SaveActualDataLogScreen();
                return true;
            }
        }

        #endregion
        private bool houkackaState = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (houkackaState)
            {
                adam.vypniHoukacku();
                houkackaState = false;
            }
            else
            {
                adam.zapniHoukacku();
                houkackaState = true;
            }
        }

        private void frmMainAgroVyroba_KeyPress(object sender, KeyPressEventArgs e)
        {
#if DEBUG
            if (e.Handled)
                return;

            if (e.KeyChar == '-')
            {
                statusLabel.Text = string.Empty;
            }
            else if (e.KeyChar == '0') // simulace noread scanner
            {
                FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs sea =
                    new FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs(
                    string.Empty, FASK.SledovaniVyroby.IScannerProvider.BarcodeType.Unknown, string.Empty, 0, FASK.SledovaniVyroby.IScannerProvider.Code.NoRead);
                this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { sea });
            }
            else if (e.KeyChar == '9')
            {
                FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs sea =
                    new FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs(
                    "1234567890123", FASK.SledovaniVyroby.IScannerProvider.BarcodeType.EAN13, "EAN13", 13, FASK.SledovaniVyroby.IScannerProvider.Code.Read);
                this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { sea });
            }
            else if (e.KeyChar == '8')
            {
                FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs sea =
                    new FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs(
                    "3210987654321", FASK.SledovaniVyroby.IScannerProvider.BarcodeType.EAN13, "EAN13", 13, FASK.SledovaniVyroby.IScannerProvider.Code.Read);
                this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { sea });
            }
            else if (e.KeyChar == '1')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamScannerDataReady), new object[] { 0 }); //sensor 1
            }
            else if (e.KeyChar == '2')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamScannerDataReady), new object[] { 1 }); //sensor 2
            }
            else if (e.KeyChar == '3')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamScannerDataReady), new object[] { 2 }); //sensor 3
            }
            else if (e.KeyChar == '4')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamScannerDataReady), new object[] { 3 }); //sensor 4
            }
            else if (e.KeyChar == 'm')
            {
                NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabControl1.TabPages[2].Width, this.tabControl1.TabPages[2].Height);
                this.tabControl1.TabPages[2].DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabControl1.TabPages[2].ClientRectangle);
                NotificationMail.bitmapstavodhlaseni.Save("bitmapstavodhlaseni.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                NotificationMail.SendEmail("Vyroba AGRO Test");
            }
            else if (e.KeyChar == 'x')
            {
                try
                {
                    new Thread((ThreadStart)delegate
                    {
                        //this.BeginInvoke((MethodInvoker)delegate{
                        throw new Exception("MyException", new OutOfMemoryException("Test out of memory"));
                        //});
                    }).Start();

                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message.ToString());
                }
            }
            else
            {
                return;
            }

            statusLabel.Text += e.KeyChar;

            //e.Handled = true;
#endif
        }

        private void buttonStateRead_Click(object sender, EventArgs e)
        {
            listBoxStav.Text = dataVyroba.GetStateInternal();
        }

        private void buttonStateSave_Click(object sender, EventArgs e)
        {
            try
            {
                string dts = DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "");
                string dirname = "InternalSates";
                if (!Directory.Exists(dirname))
                    Directory.CreateDirectory(dirname);
                string filename = Path.Combine(dirname, dts + "_" + Guid.NewGuid().ToString() + ".state.txt");
                NotificationMail.filenamestavodhlaseni = Path.GetFullPath(filename);
                StreamWriter sw = new StreamWriter(filename);
                sw.Write(listBoxStav.Text);
                sw.Close();
                sw = null;
            }
            catch (Exception ex)
            {
                FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!AgroConfig.config.Agro[0].IsPasswordNull() && !String.IsNullOrEmpty(AgroConfig.config.Agro[0].Password))
            {
                if (AgroConfig.config.Agro[0].Password != Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None))
                    return;
            }

            groupBoxAdam.Enabled =
                groupBoxEmail.Enabled =
                groupBoxObecne.Enabled =
                groupBoxUdalosti.Enabled =
                buttonUlozitNastaveni.Enabled = true;

        }

    }
}