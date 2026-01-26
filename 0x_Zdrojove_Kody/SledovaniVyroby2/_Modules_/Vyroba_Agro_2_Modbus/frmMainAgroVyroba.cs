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
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes;
using Vyroba_Agro_Modbus;
using FASK.SledovaniVyroby.ModuleIfc;
using Fask.Emailing;
using FASK.SledovaniVyroby.IScannerProvider;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus
{
    public partial class frmMainAgroVyroba : Form, IModuleConnector
    {
        //Promenne pro praci s databazi - inicializace v konstruktoru
        public FASK.SledovaniVyroby.IScannerProvider.IScannerProvider Scanner = null;
        private string scannerTypeName = string.Empty;
        private System.Threading.Timer timerRefreshUI = null;

        private DataVyroba dataVyroba = null;
        private ADAM.ADAM_60XX adam = null;

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

        public void ReturnPortsToPreviousState()
        {
        }

        public void ClosePorts()
        {
            try
            {
                TimerRefreshUIOff();
                timerRefreshUI = null;

                dataVyroba.TimerTestyClose();
                CloseTerminateScanner();
                AdamStop();
                //Log.Write("Ukonceni modulu");
                ExceptionHandler2.Handle("Ukonceni modulu", "Log_Vyroba_Agro_Modbus_2", "txt");
            }
            catch
            { }
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;

            if (dataVyroba.StavVyroba != DataVyroba.VyrobaStavy.LogIDSmena)
            {
                message = "Pro vypnutí aplikace je nutné provést odhlášení směny!";
                return false;
            }
            else
            {
                dataVyroba.VyrobaActualDataSave();
                return true;
            }
        }

        #endregion

        public frmMainAgroVyroba()
        {
            try
            {
                InitializeComponent();

                InitUserInfo();

                //Log.Write("Spusteni modulu");
                ExceptionHandler2.Handle("Spusteni modulu", "Log_Vyroba_Agro_Modbus_2", "txt");

                this.lblMachineID.Text = Logging.LogConfig.MachineID.Trim();

                //nacteni konfigurace - importovat konfiguracni parametry ze stare verze, pytle...
                LoadConfiguration();

                AgroConfig.pathDllLibraryDatabase = Logging.LogConfig.SqlConnectionStringLocal;

                //Instance datasetu
                //dataVyroba = new DataVyroba(4, AgroConfig.config.Agro[0].PotvrzovaciCidloCislo, Scanner);
                dataVyroba = new DataVyroba(6); //, AgroConfig.config.Agro[0].PotvrzovaciCidloCislo);

                var cfgPortP2PAdam = AgroConfig.config.Agro[0].IsPortP2PAdamNull() ? 1025 : AgroConfig.config.Agro[0].PortP2PAdam;


                adam = new ADAM.ADAM_60XX(
                    AgroConfig.config.Agro[0].IPAdresaAdam,
                    cfgPortP2PAdam,
                    //AgroConfig.config.Agro[0].PotvrzovaciCidloCislo,
                    //AgroConfig.config.Agro[0].PaletaCidloCislo, 
                    AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam, 
                    AgroConfig.config.Agro[0].AdamTCPTimeout,
                    AgroConfig.config.Agro[0].CisloReleLinkaAdam, 
                    AgroConfig.config.Agro[0].CisloReleHoukackaAdam,
                    AgroConfig.config.Agro[0].CisloRelePaletizatorAdam,
                    AgroConfig.config.Agro[0].CisloReleServisAdam,
                    AgroConfig.config.Agro[0].MinHighSignalWidthAdam, 
                    AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz
                    );

                dataVyroba.Adam = adam;

                dataVyroba.InformationUC = informationUC1;
                dataVyroba.KeyboardUC = keyboardUC1;
                dataVyroba.MainForm = this;

                //// nastaveni stavu vyroby na vychozi stav
                //// musi se volat az existuje popisovac okna ... 
                //dataVyroba.StavVyroba = DataVyroba.VyrobaStavy.LogIDSmena;

                LoadAssembliesScanner();

                AdamStart();

                ScannerStart();

                ShowPrihlasenyUzivatel();

                timerRefreshUI = new System.Threading.Timer(TimerRefreshUICallback, null, 100, -1);

                //full = new FullScreen(this);
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);

            }
        }

        private void frmMainAgroVyroba_FormClosing(object sender, FormClosingEventArgs e)
        {
            // okno se ukoncuje ...             
        }

        private void frmMainAgroVyroba_Load(object sender, EventArgs e)
        {

            // nastaveni stavu vyroby na vychozi stav
            // musi se volat az existuje popisovac okna ... 
            dataVyroba.StavVyroba = DataVyroba.VyrobaStavy.LogIDSmena;            
            
            // nastavit odvod mimo smenu ...
            dataVyroba.SmenaID = AgroConfig.config.Agro[0].OdvodMimoSmenuID.ToString();

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

        private void TimerRefreshUIOff()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void TimerRefreshUIOn()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(1000, System.Threading.Timeout.Infinite);
        }

        private void TimerRefreshUICallback(object state)
        {
            System.Threading.Thread.CurrentThread.Name = "TimerRefreshUI " + DateTime.Now.ToString();

            this.RefreshUI();
        }

        private void RefreshUI()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.RefreshUI();
                });
                return;
            }

            try
            {
                TimerRefreshUIOff();

                updateForm(); // aktualizace formu v casovem intervalu 1s

            }
            finally
            {
                TimerRefreshUIOn();
            }
        }

        internal void vyroba_ZmenaStavu(DataVyroba.VyrobaStavy novystav)
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
                    return;
                }

                #region ADAM HW

                #region Zjisteni savu ADAM
                bool[] DIData = null;
                bool[] DOData = null;
                int[] DIValues = null;

                try
                {
                    if (adam != null)
                    {
                        if (adam.AdamTCP_Connected)
                        {
                            labelAdamStavTCP.Text = "Připojen";
                            labelAdamStavTCP.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            labelAdamStavTCP.Text = "Odpojen";
                            labelAdamStavTCP.ForeColor = Color.DarkRed;
                        }

                        if (adam.AdamP2P_Started)
                        {
                            labelAdamStavP2P.Text = "Spuštěn";
                            labelAdamStavP2P.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            labelAdamStavP2P.Text = "Zastaven";
                            labelAdamStavP2P.ForeColor = Color.DarkRed;
                        }

                        // vytazeni dat z adam
                        DIData = adam.DIStatusLast;
                        DOData = adam.DOStatusLast;
                        DIValues = adam.DIValuesLast;
                    }
                }
                catch (Exception ex)
                {
                    // TODO : vyjimka !!!
                    //Log.Write(ex.Message.ToString());
                    //Exceptions.Handler.ErrorHandle(ex.Message, "frmMainAgroVyroba.RefreshUI");
                    ExceptionHandler2.Handle(ex.Message, "frmMainAgroVyroba.RefreshUI", false);

                }
                #endregion

                #region Zobrazeni hodnot z adam
                try
                {
                    if ((DIValues != null) && (DIValues.Length > 0))
                    {
                        lCounter0.Text = DIValues[0].ToString();
                        lCounter1.Text = DIValues[1].ToString();
                        lCounter2.Text = DIValues[2].ToString();
                        lCounter3.Text = DIValues[3].ToString();
                        lCounter4.Text = DIValues[4].ToString();
                        lCounter5.Text = DIValues[5].ToString();
                    }
                }
                catch
                {
                }

                if (DIData != null && DIData.Length > 0)
                {
                    #region nastaveni labelu
                    if (DIData[0])
                    {
                        lblDI0.Text = "1";
                        lblDI0.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI0.Text = "0";
                        lblDI0.ForeColor = Color.Red;
                    }

                    if (DIData[1])
                    {
                        lblDI1.Text = "1";
                        lblDI1.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI1.Text = "0";
                        lblDI1.ForeColor = Color.Red;
                    }

                    if (DIData[2])
                    {
                        lblDI2.Text = "1";
                        lblDI2.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI2.Text = "0";
                        lblDI2.ForeColor = Color.Red;
                    }

                    if (DIData[3])
                    {
                        lblDI3.Text = "1";
                        lblDI3.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI3.Text = "0";
                        lblDI3.ForeColor = Color.Red;
                    }

                    if (DIData[4])
                    {
                        lblDI4.Text = "1";
                        lblDI4.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI4.Text = "0";
                        lblDI4.ForeColor = Color.Red;
                    }


                    if (DIData[5])
                    {
                        lblDI5.Text = "1";
                        lblDI5.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDI5.Text = "0";
                        lblDI5.ForeColor = Color.Red;
                    }
                    #endregion
                }


                if (DOData != null && DOData.Length > 0)
                {
                    #region nastaveni labelu
                    if (DOData[0])
                    {
                        lblRL0.Text = "1";
                        lblRL0.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL0.Text = "0";
                        lblRL0.ForeColor = Color.Red;
                    }

                    if (DOData[1])
                    {
                        lblRL1.Text = "1";
                        lblRL1.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL1.Text = "0";
                        lblRL1.ForeColor = Color.Red;
                    }

                    if (DOData[2])
                    {
                        lblRL2.Text = "1";
                        lblRL2.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL2.Text = "0";
                        lblRL2.ForeColor = Color.Red;
                    }

                    if (DOData[3])
                    {
                        lblRL3.Text = "1";
                        lblRL3.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL3.Text = "0";
                        lblRL3.ForeColor = Color.Red;
                    }

                    if (DOData[4])
                    {
                        lblRL4.Text = "1";
                        lblRL4.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL4.Text = "0";
                        lblRL4.ForeColor = Color.Red;
                    }


                    if (DOData[5])
                    {
                        lblRL5.Text = "1";
                        lblRL5.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblRL5.Text = "0";
                        lblRL5.ForeColor = Color.Red;
                    }
                    #endregion
                }
                #endregion

                #endregion

                #region Zobrazeni informaci o nactenych hodnotach poctu

                // actual datarow by mel byt vzdy ... ???
                var actDR = dataVyroba.AktualDataRow;
                //if (actDR == null)
                {
                    ClearInfoLabels();
                }

                if (actDR != null)
                {
                    // carovy kod ... 
                    if (String.IsNullOrEmpty(actDR.barcodeReaded))
                        lblBarcodeActual.Text = "Nenalezeno";
                    else
                        lblBarcodeActual.Text = actDR.barcodeReaded;

                    //lblReadCodeCnt.Text = actDR.qty.ToString();
                    //lblNoReadCodeCnt.Text = actDR.qtyReal.ToString();
                    //lblReadCodeNoCnt.Text = actDR.NoCntScanRead.ToString();
                    //lblNoReadCodeNoCnt.Text = actDR.NoCntScanNoRead.ToString();
                    //lblSumaZapocteno.Text = (actDR.qty + actDR.qtyReal).ToString();
                    //lblSumaNezapocteno.Text = (actDR.NoCntScanRead + actDR.NoCntScanNoRead).ToString();

                    //lblSensorZapocteno.Text = (actDR.qty + actDR.qtyReal).ToString();
                    //lblSenzorNezapocteno.Text = actDR.NoCntSensor.ToString();

                    //lblSumaRead.Text = (actDR.qty + actDR.NoCntScanRead).ToString();
                    //lblSumaNoRead.Text = (actDR.qtyReal + actDR.NoCntScanNoRead).ToString();
                    //lblSumaVse.Text =
                    //    (
                    //    (actDR.qty + actDR.qtyReal) +
                    //    (actDR.NoCntScanRead + actDR.NoCntScanNoRead)
                    //    ).ToString();
                    //lblSumaSensor.Text = (actDR.qty + actDR.qtyReal + actDR.NoCntSensor).ToString();

                    try
                    {

                        //TODO 
                        // tady je nejaka chyba, která se zobrazuje nahodně....
                        // objekt neni nastaven na instanci objektu...
                        // provizorne ošetřeno tak že zabaleno do try catch, a loguje chybu ale inak pokračuje dál...
                        // potřeba pořadne ošetřit všechny stavy

                        //#warning AAAAA Hele tady je BIG problem...

                        int pocetPytlu = (int)(actDR.qty + actDR.qtyReal);
                        //int Zbyva = -pocetPytlu;
                        ////int Zbyva = (int)(dataVyroba.VPP_Row.QTYSHPPD - pocetPytlu - dataVyroba.VPP_Row.QTYODVEDENO);
                        //if (dataVyroba.VPP_Row != null)
                        //{
                        //    Zbyva = (int)(dataVyroba.VPP_Row.QTYSHPPD - pocetPytlu); 
                        //}

                        labelPytluPocet.Text = pocetPytlu.ToString();
                        labelPaletPocet.Text = (actDR.IspocetPaletNull() ? 0 : actDR.pocetPalet).ToString();

                        labelPytluSmenaPocet.Text = dataVyroba.VyrobaDataHistoryInMemory.FASK_Events.Sum(x => x.qty + x.qtyReal).ToString();
                        labelPaletSmenaPocet.Text = dataVyroba.VyrobaDataHistoryInMemory.FASK_Events.Sum(x => x.IspocetPaletNull() ? 0 : x.pocetPalet).ToString();

                        //if (dataVyroba.VPP_Row != null)
                        //{
                        //    labelPlanMnPocet.Text = ((int)(dataVyroba.VPP_Row.QTYSHPPD)).ToString(); 
                        //}

                        if (dataVyroba.VPP_Row != null)
                        {
                            labelPlanMnPocet.Text = dataVyroba.VPP_Row.QTYPACK.ToString("#");
                            
                            int pocetP = actDR.IspocetPaletNull() ? 0 : actDR.pocetPalet;
                            int QTYpacktmp = (int)(dataVyroba.VPP_Row.IsQTYPACKMJNull() ? 0 : dataVyroba.VPP_Row.QTYPACK);

                            int x = pocetPytlu - (pocetP * QTYpacktmp);
                            labelZbyvaPocet.Text = x.ToString();
                        }
                        

                    }
                    catch (System.Exception ex)
                    {
                        //Log.Write(ex.Message, "PlneniLabels>ex.Message>updateForm()");
                        //Log.Write(ex.StackTrace, "PlneniLabels>ex.StackTrace>ex.updateForm()");
                        ExceptionHandler2.Handle(ex);
                    }

                }
                else
                {
                }

                #endregion

                #region Zobrazeni informaci o aktualnim odvodu
                var odvod = dataVyroba._odvodVyroby;
                if (odvod != null)
                {
                    lOdvodBarcodeOdeslat.Text = odvod.BarcodeActual ?? string.Empty;
                    lOdvodReadOdeslat.Text = odvod.CodeReadCnt.ToString();
                    lOdvodNoReadOdeslat.Text = odvod.CodeNoReadCnt.ToString();
                }
                else
                {
                    lOdvodBarcodeOdeslat.Text =
                    lOdvodReadOdeslat.Text =
                    lOdvodNoReadOdeslat.Text = "-";
                }
                lOdvodUlozitZa.Text = "tts: " + dataVyroba.Time2Save() + " [s]";
                #endregion

                #region aktualni smena
                lblIDSmena.Text = dataVyroba.SmenaID;
                #endregion

                #region Stav scanu
                var scan = dataVyroba.Vyroba_Scan;
                if (scan != null)
                {
                    lblScannerCodeValue.Text = scan.ReadBarcode;
                    if (scan.ReadResult == Code.NoData)
                    {
                        lblScannerCode.Text = "X";
                        lblScannerCode.ForeColor = Color.Black;
                    }
                    else if (scan.ReadResult == Code.NoRead)
                    {
                        lblScannerCode.Text = "N";
                        lblScannerCode.ForeColor = Color.Red;
                    }
                    else if (scan.ReadResult == Code.Read)
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

                    try
                    {
                        var lastScanRead = (DateTime.Now - scan.ReadTime);
                        //lblTimeScannerLastRead.Text = lastScanRead.TotalSeconds.ToString("#0");
                        lblTimeScannerLastRead.Text = String.Format("lr: {0:.} [s]", lastScanRead.TotalSeconds);
                    }
                    catch
                    {
                        lblTimeScannerLastRead.Text = "Error";
                    }
                }
                else
                {
                    lblScannerCodeValue.Text =
                    lblScannerCode.Text = "-";
                    lblScannerCode.ForeColor = Color.Red;
                    
                    lblTimeScannerLastRead.Text = "-";
                }
                #endregion

                //#region Stav potvrzovaciho cidla
                //{
                //    var sensor = dataVyroba.Vyroba_SensorPytel;
                //    if (sensor != null)
                //    {
                //        if (sensor.Active)
                //        {
                //            lblSensorState.Text = "1";
                //            lblSensorState.ForeColor = Color.Green;
                //        }
                //        else
                //        {
                //            lblSensorState.Text = "0";
                //            lblSensorState.ForeColor = Color.Black;
                //        }
                //    }
                //    else
                //    {
                //        lblSensorState.Text = "-";
                //        lblSensorState.ForeColor = Color.Red;
                //    }
                //}
                //#endregion

                #region Aktualni pytel varianta
                if (dataVyroba.ActualPytel != null)
                    lblAktualniPytel.Text = dataVyroba.ActualPytel.Nazev;
                else
                    lblAktualniPytel.Text = "Neznámý";
                #endregion


            }
            catch (Exception ex)
            {
                //Log.Write(ex.Message, "ex.Message>updateForm()");
                //Log.Write(ex.StackTrace, "ex.StackTrace>ex.updateForm()");

                ExceptionHandler2.Handle(ex);
                //lblReadCodeCnt.Text = lblNoReadCodeCnt.Text = "-";
                MessageBox.Show("Nastala chyba (problem): " + ex.Message + "\n " + (dataVyroba.AktualDataRow == null ? "null" : dataVyroba.AktualDataRow.RowState.ToString()), "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ClearInfoLabels()
        {
            lblBarcodeActual.Text = "Nenalezeno";

            //lblReadCodeCnt.Text =
            //lblNoReadCodeCnt.Text =
            //lblReadCodeNoCnt.Text =
            //lblNoReadCodeNoCnt.Text =
            //lblSumaZapocteno.Text =
            //lblSumaNezapocteno.Text =

            //lblSensorZapocteno.Text =
            //lblSenzorNezapocteno.Text =

            //lblSumaRead.Text =
            //lblSumaNoRead.Text =
            //lblSumaVse.Text =
            //lblSumaSensor.Text = "-";
            labelPaletPocet.Text =
                labelPaletSmenaPocet.Text =
                labelPytluPocet.Text =
                labelPytluSmenaPocet.Text =
                labelZbyvaPocet.Text =
                labelPlanMnPocet.Text =
                "-";
        }

        internal void operace_Zmena()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { operace_Zmena(); });
                return;
            }

            //Log.Write("operace_Zmena() - Houkacka OFF");
            ExceptionHandler2.Handle("operace_Zmena() - Houkacka OFF", "Log_Vyroba_Agro_Modbus_2", "txt");
            //this.dataVyroba.SetLinkaState(false);  //vypneme linku
            //AGRO_HOUKACKA - OFF + RESET
            //this.dataVyroba.HoukackaRizeni(DataVyroba.HoukackaStav.OFF);
            //this.dataVyroba.HoukackaRizeni(DataVyroba.HoukackaStav.RESET);//vynulujeme citac

            //this.tabControl1.SelectedIndex = 2;
            this.tabControl1.SelectedTab = this.tabPageHistory;

            //NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabControl1.TabPages[2].Width, this.tabControl1.TabPages[2].Height);
            //this.tabControl1.TabPages[2].DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabControl1.TabPages[2].ClientRectangle);
            //NotificationMail.bitmapstavodhlaseni.Save("bitmapstavodhlaseni.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabPageHistory.Width, this.tabPageHistory.Height);
            this.tabPageHistory.DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabPageHistory.ClientRectangle);
            NotificationMail.bitmapstavodhlaseni.Save("bitmapstavodhlaseni.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //NotificationMail.bitmapstavodhlaseni_vyroba = new Bitmap(this.tabControl1.TabPages[0].Width, this.tabControl1.TabPages[0].Height);
            //this.tabControl1.TabPages[0].DrawToBitmap(NotificationMail.bitmapstavodhlaseni_vyroba, this.tabControl1.TabPages[0].ClientRectangle);
            //NotificationMail.bitmapstavodhlaseni_vyroba.Save("bitmapstavodhlaseni_vyroba.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            NotificationMail.bitmapstavodhlaseni_vyroba = new Bitmap(this.tabPageData.Width, this.tabPageData.Height);
            this.tabPageData.DrawToBitmap(NotificationMail.bitmapstavodhlaseni_vyroba, this.tabPageData.ClientRectangle);
            NotificationMail.bitmapstavodhlaseni_vyroba.Save("bitmapstavodhlaseni_vyroba.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            this.buttonStateRead_Click(null, null);
            this.buttonStateSave_Click(null, null);
            //this.tabControl1.TabPages[2].Show();
        }

        private void ShowPrihlasenyUzivatel()
        {
            if (this.InvokeRequired)
            {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                this.BeginInvoke(new MethodInvoker(() => { ShowPrihlasenyUzivatel(); }));
            }
            else
            {
                this.lblIDSmena.Text = dataVyroba.SmenaID;
            }

        }

        private void LoadConfiguration()
        {
            //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
            if (AgroConfig.config.Agro.Rows.Count == 0)
            {
                AgroConfig.config.Agro.AddAgroRow(0, 1, "1", 60, "998", "192.168.1.3", 1025,
                    @"d:\_w\MES-Projekt\0x_Zdrojove_Kody\SledovaniVyroby2\!Build!\Vyroba\Debug\Pytle.xml ", 
                    120, 350, 100, 5, 5, 2, 10, 4, 1,2, 2, 3, 100, false, false, string.Empty, string.Empty, false, 1, -1, 2, 1000);

                AgroConfig.config.Agro_OdvadeniKontrola.AddAgro_OdvadeniKontrolaRow(true,
                    1, true, true,
                    3, true, true,
                    5, true, true);

                AgroConfig.config.Agro_OdvadeniKontrola_NORead.AddAgro_OdvadeniKontrola_NOReadRow(true,
                    1, true, true,
                    3, true, true,
                    5, true, true);
                //Ulozeni
                AgroConfig.Save();
            }

            try
            {
                //Vlozeni do textboxuu
                txtPrihlasenaSmenaPosledni.Text = AgroConfig.config.Agro[0].PosledniPrihlasenaSmena;
                txtAckPytelSensor.Text = AgroConfig.config.Agro[0].PotvrzovaciCidloCislo.ToString();
                txtAckPaletaSensor.Text = AgroConfig.config.Agro[0].PaletaCidloCislo.ToString();
                txtIDMimoSmenu.Text = AgroConfig.config.Agro[0].OdvodMimoSmenuID;
                txtZahaleniGenerate.Text = AgroConfig.config.Agro[0].ZahaleniStartSekund.ToString();
                txtIPAdresaAdam.Text = AgroConfig.config.Agro[0].IPAdresaAdam;
                txtP2PPort.Text = AgroConfig.config.Agro[0].IsPortP2PAdamNull() ? (1025).ToString() : AgroConfig.config.Agro[0].PortP2PAdam.ToString();
                txtPathToPytleConfigFile.Text = AgroConfig.config.Agro[0].PathToPytleConfigFile;
                txtIntervalUkladaniDat.Text = AgroConfig.config.Agro[0].IntervalUkladaniDat.ToString();
                txtTimerPeriodAdam.Text = AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam.ToString();
                txtAdamTCPTimeout.Text = AgroConfig.config.Agro[0].AdamTCPTimeout.ToString();
                txtPocetNepruchoduReleHoukacky.Text = AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky.ToString();
                txtCidloHlidaniStavLinky.Text = AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz.ToString();
                txtPocetPruchZmenDoProhaz.Text = AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz.ToString();

                txtAdamReleHoukacka.Text = AgroConfig.config.Agro[0].CisloReleHoukackaAdam.ToString();
                txtAdamReleLinka.Text = AgroConfig.config.Agro[0].CisloReleLinkaAdam.ToString();
                txtAdamRelePaletizator.Text = AgroConfig.config.Agro[0].CisloRelePaletizatorAdam.ToString();
                txtAdamReleServis.Text = AgroConfig.config.Agro[0].CisloReleServisAdam.ToString();

                txtMinHighSignalWidth.Text = AgroConfig.config.Agro[0].MinHighSignalWidthAdam.ToString();

                 txtCasSepnutiProRelePaletizatorAdam.Text = AgroConfig.config.Agro[0].CasSepnutiProRelePaletizatorAdam.ToString();

                txtPocetU36.Text = AgroConfig.config.Agro[0].MaxPocetUdalosti36.ToString();
                txtInterU36.Text = AgroConfig.config.Agro[0].CasovyIntervalUdalosti36.ToString();
                txtPocetU11.Text = AgroConfig.config.Agro[0].MaxPocetUdalosti11.ToString();
                txtInterU11.Text = AgroConfig.config.Agro[0].CasovyIntervalUdalosti11.ToString();

                checkBoxEmailKonecSmeny.Checked = AgroConfig.config.Agro[0].KonecSmenyEmailSend;
                txtSarzePassword.Text = AgroConfig.config.Agro[0].IsSarzePasswordNull() ? string.Empty : Encoding.Default.GetString(Convert.FromBase64String(AgroConfig.config.Agro[0].SarzePassword));

                checkBoxPovolitNuloveOdvody.Checked = AgroConfig.config.Agro[0].PovolitNuloveOdvody;

                txtZdrojBarcodeSended.Text = AgroConfig.config.Agro[0].ZapisovanyKodDoBarcodeSended.ToString();

                #region READ

                chb_Odvadeni_Kontrola_Pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Kontrola_Pouzit;

                chb_Odvadeni_Var_1_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pokracuj;
                chb_Odvadeni_Var_1_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pouzit;
                tb_Odvadeni_Var_1_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pocet.ToString();

                chb_Odvadeni_Var_2_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pokracuj;
                chb_Odvadeni_Var_2_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pouzit;
                tb_Odvadeni_Var_2_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pocet.ToString();

                chb_Odvadeni_Var_3_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pokracuj;
                chb_Odvadeni_Var_3_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pouzit;
                tb_Odvadeni_Var_3_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pocet.ToString();

                #endregion

                #region NO READ

                chb_Odvadeni_NoRead_Kontrola_Pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Kontrola_Pouzit;

                chb_Odvadeni_NoRead_Var_1_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pokracuj;
                chb_Odvadeni_NoRead_Var_1_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pouzit;
                tb_Odvadeni_NoRead_Var_1_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pocet.ToString();

                chb_Odvadeni_NoRead_Var_2_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pokracuj;
                chb_Odvadeni_NoRead_Var_2_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pouzit;
                tb_Odvadeni_NoRead_Var_2_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pocet.ToString();

                chb_Odvadeni_NoRead_Var_3_pokracuj.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pokracuj;
                chb_Odvadeni_NoRead_Var_3_pouzit.Checked = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pouzit;
                tb_Odvadeni_NoRead_Var_3_pocet.Text = AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pocet.ToString();

                #endregion
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


        #region ADAM reakce na udalosti
        private void AdamStart()
        {
            try
            {
                if (adam != null)
                {
                    adam.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady);
                    adam.DataReady += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady);
                    adam.Start();
                }

            }
            catch { }
        }

        private void AdamStop()
        {
            try
            {
                if (adam != null)
                {
                    adam.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady);
                    adam.Stop();
                }

            }
            catch { }
        }
        private void Adam_DataReady(FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady), new object[] { e.Data });
            }
            catch (Exception ex)
            {
               // Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex.Message, "Adam_DataReady", false);
            }
        }

        private void AdamSensorDataReady(int sensor)
        {
            if (dataVyroba.StavVyroba != DataVyroba.VyrobaStavy.SERVIS)
            {
                // zde se reaguje na zmenu stavu cidla
                // pokud je to potvrzovaci cidlo, tak se provede akce potvrzeni            
                if (sensor == AgroConfig.config.Agro[0].PotvrzovaciCidloCislo)
                {
                    dataVyroba.SensorPytelAck();
                    updateForm();
                    // nakonec se zvedne interni citac sepnuti vsech cidel pro dany senzor
                    dataVyroba.inkrementSensorAcitvatedCount(sensor);
                }

                // pokud jde o sensor pro palety, provede se inkrement paletoveho citace
                if (sensor == AgroConfig.config.Agro[0].PaletaCidloCislo)
                {
                    dataVyroba.SensorPaletaAck(sensor);
                    updateForm();
                }
            }
        }
        #endregion

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

            if (dataVyroba.StavVyroba != DataVyroba.VyrobaStavy.SERVIS)
            {
                dataVyroba.ScannerActivate(barcode, e.ScannedCodeType);
                dataVyroba.inkrementScannerAcitvatedCount(e.ScannedCodeType);
            }
            updateForm();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 2)
            if (tabControl1.SelectedTab == tabPageHistory)
            {
                fASKEventsBindingSource.DataSource = dataVyroba.VyrobaDataHistoryInMemory.FASK_Events;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if ((AgroConfig.config.Agro[0].IsPasswordNull() || String.IsNullOrEmpty(AgroConfig.config.Agro[0].Password)) && !String.IsNullOrEmpty(txtPassword.Text))
            AgroConfig.config.Agro[0].Password = Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None);

            AgroConfig.config.Agro[0].PosledniPrihlasenaSmena = txtPrihlasenaSmenaPosledni.Text.Trim();
            AgroConfig.config.Agro[0].PotvrzovaciCidloCislo = int.Parse(txtAckPytelSensor.Text);
            AgroConfig.config.Agro[0].PaletaCidloCislo = int.Parse(txtAckPaletaSensor.Text);
            AgroConfig.config.Agro[0].OdvodMimoSmenuID = txtIDMimoSmenu.Text;
            AgroConfig.config.Agro[0].ZahaleniStartSekund = int.Parse(txtZahaleniGenerate.Text);
            AgroConfig.config.Agro[0].IPAdresaAdam = txtIPAdresaAdam.Text;
            AgroConfig.config.Agro[0].PortP2PAdam = int.Parse(txtP2PPort.Text);
            AgroConfig.config.Agro[0].PathToPytleConfigFile = txtPathToPytleConfigFile.Text;
            AgroConfig.config.Agro[0].IntervalUkladaniDat = int.Parse(txtIntervalUkladaniDat.Text);
            AgroConfig.config.Agro[0].TimerPeriodSensorsCheckAdam = int.Parse(txtTimerPeriodAdam.Text);
            AgroConfig.config.Agro[0].AdamTCPTimeout = int.Parse(txtAdamTCPTimeout.Text);
            AgroConfig.config.Agro[0].PocetNepruchoduSepnutiReleHoukacky = int.Parse(txtPocetNepruchoduReleHoukacky.Text);
            AgroConfig.config.Agro[0].CidloHlidaniStavLinkyProhaz = int.Parse(txtCidloHlidaniStavLinky.Text);
            AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz = int.Parse(txtPocetPruchZmenDoProhaz.Text);

            AgroConfig.config.Agro[0].CisloReleHoukackaAdam = int.Parse(txtAdamReleHoukacka.Text);
            AgroConfig.config.Agro[0].CisloReleLinkaAdam = int.Parse(txtAdamReleLinka.Text);
            AgroConfig.config.Agro[0].CisloRelePaletizatorAdam = int.Parse(txtAdamRelePaletizator.Text);
            AgroConfig.config.Agro[0].CisloReleServisAdam = int.Parse(txtAdamReleServis.Text);

            AgroConfig.config.Agro[0].MinHighSignalWidthAdam = int.Parse(txtMinHighSignalWidth.Text);

            AgroConfig.config.Agro[0].CasSepnutiProRelePaletizatorAdam = double.Parse(txtCasSepnutiProRelePaletizatorAdam.Text);

            AgroConfig.config.Agro[0].MaxPocetUdalosti11 = int.Parse(txtPocetU11.Text);
            AgroConfig.config.Agro[0].CasovyIntervalUdalosti11 = int.Parse(txtInterU11.Text);
            AgroConfig.config.Agro[0].MaxPocetUdalosti36 = int.Parse(txtPocetU36.Text);
            AgroConfig.config.Agro[0].CasovyIntervalUdalosti36 = int.Parse(txtInterU36.Text);

            AgroConfig.config.Agro[0].KonecSmenyEmailSend = checkBoxEmailKonecSmeny.Checked;

            AgroConfig.config.Agro[0].PovolitNuloveOdvody = checkBoxPovolitNuloveOdvody.Checked;

            AgroConfig.config.Agro[0].ZapisovanyKodDoBarcodeSended = int.Parse(txtZdrojBarcodeSended.Text);

            AgroConfig.config.Agro[0].SarzePassword = Convert.ToBase64String(Encoding.Default.GetBytes(txtSarzePassword.Text), Base64FormattingOptions.None);


            #region Read

            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Kontrola_Pouzit = chb_Odvadeni_Kontrola_Pouzit.Checked;

            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pokracuj = chb_Odvadeni_Var_1_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pouzit = chb_Odvadeni_Var_1_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_1_pocet = int.Parse(tb_Odvadeni_Var_1_pocet.Text);

            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pokracuj = chb_Odvadeni_Var_2_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pouzit = chb_Odvadeni_Var_2_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_2_pocet = int.Parse(tb_Odvadeni_Var_2_pocet.Text);

            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pokracuj = chb_Odvadeni_Var_3_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pouzit = chb_Odvadeni_Var_3_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola[0].Odvadeni_Var_3_pocet = int.Parse(tb_Odvadeni_Var_3_pocet.Text);

            #endregion

            #region NO Read

            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Kontrola_Pouzit = chb_Odvadeni_NoRead_Kontrola_Pouzit.Checked;

            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pokracuj = chb_Odvadeni_NoRead_Var_1_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pouzit = chb_Odvadeni_NoRead_Var_1_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_1_pocet = int.Parse(tb_Odvadeni_NoRead_Var_1_pocet.Text);

            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pokracuj = chb_Odvadeni_NoRead_Var_2_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pouzit = chb_Odvadeni_NoRead_Var_2_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_2_pocet = int.Parse(tb_Odvadeni_NoRead_Var_2_pocet.Text);

            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pokracuj = chb_Odvadeni_NoRead_Var_3_pokracuj.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pouzit = chb_Odvadeni_NoRead_Var_3_pouzit.Checked;
            AgroConfig.config.Agro_OdvadeniKontrola_NORead[0].Odvadeni_Var_3_pocet = int.Parse(tb_Odvadeni_NoRead_Var_3_pocet.Text);

            #endregion

            //Ulozeni do xml
            AgroConfig.Save();

            groupBox_KontrolaKodu.Enabled =
                groupBox_KontrolaKodu_NOREAD.Enabled=
            groupBoxOdvadeni.Enabled =
            groupBoxAdam.Enabled =
                groupBoxEmail.Enabled =
                groupBoxObecne.Enabled =
                groupBoxUdalosti.Enabled =
                buttonUlozitNastaveni.Enabled = false;
            txtPassword.Text = string.Empty;

        }


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

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
//#if DEBUG
            if (e.Handled)
                return;

            if (e.KeyChar == 'p')
            { // zobrazeni pracovniku ... 
                StringBuilder sbp = new StringBuilder();
                foreach (var p in this.dataVyroba.PracovniciGet())
                {
                    if (!String.IsNullOrEmpty(p))
                        sbp.AppendLine(p);
                }
                MessageBox.Show(sbp.ToString(), "Pracovnici", MessageBoxButtons.OK);
            }

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
            else if (e.KeyChar == '7')
            {
                FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs sea =
                    new FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs(
                    "8594005003507", FASK.SledovaniVyroby.IScannerProvider.BarcodeType.EAN13, "EAN13", 13, FASK.SledovaniVyroby.IScannerProvider.Code.Read);
                this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { sea });
            }
            else if (e.KeyChar == '6')
            {
                FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs sea =
                    new FASK.SledovaniVyroby.IScannerProvider.ScannerEventArgs(
                    "859????0:3507", FASK.SledovaniVyroby.IScannerProvider.BarcodeType.EAN13, "EAN13", 13, FASK.SledovaniVyroby.IScannerProvider.Code.Read);
                this.BeginInvoke(new BarcodeReadedDelegate(ScannerDataReceived), new object[] { sea });
            }
            else if (e.KeyChar == '1')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady), new object[] { 0 }); //sensor 1
            }
            else if (e.KeyChar == '2')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady), new object[] { 1 }); //sensor 2
            }
            else if (e.KeyChar == '3')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady), new object[] { 2 }); //sensor 3
            }
            else if (e.KeyChar == '4')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady), new object[] { 3 }); //sensor 4
            }
            else if (e.KeyChar == 'm')
            {
                //NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabControl1.TabPages[2].Width, this.tabControl1.TabPages[2].Height);
                //this.tabControl1.TabPages[2].DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabControl1.TabPages[2].ClientRectangle);
                //NotificationMail.bitmapstavodhlaseni.Save("bitmapstavodhlaseni.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                NotificationMail.bitmapstavodhlaseni = new Bitmap(this.tabPageHistory.Width, this.tabPageHistory.Height);
                this.tabPageHistory.DrawToBitmap(NotificationMail.bitmapstavodhlaseni, this.tabPageHistory.ClientRectangle);
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
                   // Log.Write(ex.Message.ToString());
                    ExceptionHandler2.Handle(ex);
                }
            }
            else
            {
                return;
            }

            statusLabel.Text += e.KeyChar;

            //e.Handled = true;
//#endif
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
               // FASK.SledovaniVyroby.ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!AgroConfig.config.Agro[0].IsPasswordNull() && !String.IsNullOrEmpty(AgroConfig.config.Agro[0].Password))
            {
                if (AgroConfig.config.Agro[0].Password != Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None))
                    return;
            }

            groupBox_KontrolaKodu.Enabled =
                groupBox_KontrolaKodu_NOREAD.Enabled=
            groupBoxOdvadeni.Enabled =
            groupBoxAdam.Enabled =
                groupBoxEmail.Enabled =
                groupBoxObecne.Enabled =
                groupBoxUdalosti.Enabled =
                buttonUlozitNastaveni.Enabled = true;

        }

        private void txtZahaleniGenerate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSarzePassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void txtCidloHlidaniStavLinky_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtPocetNepruchoduReleHoukacky_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtIntervalUkladaniDat_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void txtPathToPytleConfigFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtPocetPruchZmenDoProhaz_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIDMimoSmenu_TextChanged(object sender, EventArgs e)
        {

        }

        private void nevim_Click(object sender, EventArgs e)
        {

        }

        private void TextChanged_Event(object sender, EventArgs e)
        {
            if(sender != null)
            {
                if(!string.IsNullOrEmpty(((TextBox)sender).Text))
                {
                    try
                    {
                        var xxx = int.Parse(((TextBox)sender).Text);
                        ((TextBox)sender).BackColor = Color.FromArgb(255,255,255,255);
                    }
                    catch
                    {
                        ((TextBox)sender).BackColor = Color.PaleVioletRed;
                    }
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.FromArgb(255, 255, 255, 255);
                }
            }
        }

        public bool IsReadyToShow(out string message)
        {
            message = "ok";
            return true;
        }
    }
}